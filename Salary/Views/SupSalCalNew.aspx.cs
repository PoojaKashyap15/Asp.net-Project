using System;
using prototype.App_Code;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;
using System.Configuration;

namespace Salary.Views
{
    public partial class SupSalCalNew : System.Web.UI.Page
    {
        string sql;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string viewyn = "0";
                    string addyn = "0";
                    string edityn = "0";
                    string deleteyn = "0";
                    string printyn = "0";
                    string qs_check = "0";
                    string typ = "";
                    string menu_name = "";

                    if (Request.QueryString["qsc"] == null)
                    {
                        Response.Redirect("../Views/login.aspx");
                    }

                    if (Session["CoSl"] == null)
                    {
                        Response.Redirect("../Views/login.aspx");
                    }

                    mGlobal.qs_check(Session["LoginSl"].ToString(), Session["CoSl"].ToString(), Request.QueryString["qsc"].ToString().ToUpper(),
                    out viewyn, out addyn, out edityn, out deleteyn, out printyn, out qs_check, out typ, out menu_name);

                    if (qs_check == 0.ToString())
                    {
                        Response.Redirect("../Views/login.aspx");
                    }
                    else
                    {
                        Session["AddYN"] = addyn;
                        Session["EditYN"] = edityn;
                        Session["DeleteYN"] = deleteyn;
                        Session["ViewYN"] = viewyn;
                        Session["PrintYN"] = printyn;
                        Session["TYP"] = typ;
                        Session["MenuName"] = menu_name;
                    }

                    lblHeading.Text = Session["MenuName"].ToString();

                    Session["Mode"] = "";
                    panelAddEdit.Visible = true;
                    Bind();
                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }


        private void Bind()
        {
            try
            {
                sql = "SELECT prs_emno as sl ,cast(concat(prs_name,'---',prs_emno) as char) as name FROM pay_personnel where active_yn='" + 1 + "' and co_sl='" + Session["CoSl"] + "'   order by name";
                mGlobal.binddropdownlist(sql, ddlName);
                ddlName.Items.Insert(0, new ListItem("-- Select  Name --", "0"));

                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1 and regular_yn = 0 and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlSalType);

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {

            }
        }





        protected void btnClose_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelError.Visible = true;
            lblsl.Text = "";
            lblMsg.Text = "";
            panelView.Visible = false;
            ddlName.SelectedValue = "0".ToString();
            ddlName.Enabled = true;
            ddlSalType.Enabled = true;
            txtmmyyyy.ReadOnly = false;
            txtmmyyyy.Text = "";
            txtded.Text = "";
            txtGross.Text = "";
            txtNet.Text = "";
            txtnarration.Text = "";

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                txtded.Text = "";
                txtGross.Text = "";
                txtNet.Text = "";
                txtnarration.Text = "";
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");
                sql = "SELECT COD_CODE,COD_DESC,COD_TYPE, prg_AMNT FROM psys_codes c LEFT OUTER JOIN pay_reg f ON  COD_CODE = prg_edcd " +
                " AND prg_Emno =  " + ddlName.SelectedValue + " and prg_rlcd = " + ddlSalType.SelectedValue + "  and prg_yymm = '" + yymm + "'  and prg_yid = '" + Session["YearSl"] + "'  AND f.co_Sl = '" + Session["CoSl"] + "'  WHERE cod_code not in (500,900,999) AND C.ACTIVE_YN = 1 and c.co_sl = " + Session["CoSl"] + " order by cod_code";
                mGlobal.bindataGrid(gvCodes, sql);
                panelView.Visible = true;
                ddlName.Enabled = false;
                ddlSalType.Enabled = false;
                txtmmyyyy.ReadOnly = true;
                sql = "SELECT prg_narr FROM pay_reg where co_sl='" + Session["CoSl"] + "' and prg_edcd = 999 and " +
                        " prg_Emno =  " + ddlName.SelectedValue + " and prg_rlcd = " + ddlSalType.SelectedValue + "  and prg_yymm = '" + yymm + "'  and prg_yid = '" + Session["YearSl"] + "'";
                txtnarration.Text = mGlobal.getResult(sql);
                gross_ded_net();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {

            }

        }

        protected void gvCodes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCodes.PageIndex = e.NewPageIndex;
            btnView_Click(sender, e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;
            string empCode = "";
            int status = 0;
            decimal amount = 0;
            string dpcd = "";
            try
            {
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                if (Session["AddYN"].ToString() == "0")
                {
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    sb2.Append(@"<script>");
                    sb2.Append("alert('Sorry..you dont have ADD permission!...Please contact system admin');");
                    sb2.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                    return;
                }

                if(Convert.ToDecimal(txtNet.Text) < 1 )
                {
                    lblMsg.Text = "please check NET Amount";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                sql = "SELECT prs_dpcd FROM pay_personnel  where prs_emno = " + ddlName.SelectedValue + " and  co_sl ='" + Session["CoSl"] + "' ";
                dpcd = mGlobal.getResult(sql);

                sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + yymm + "' " +
               " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_rlcd = " + ddlSalType.SelectedValue + " and prh_yid = '" + Session["YearSl"].ToString() + "' AND prh_dpcd =  " + dpcd + "";

                string lock_yn = mGlobal.getResult(sql);

                if (Convert.ToDouble(lock_yn) > 0)
                {
                    lblMsg.Text = "Sorry!.. This month locked..you cannot Save Salary";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                lblMsg.Text = "";

                Session["Mode"] = "Add";
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                mGlobal.conDatabase.Open();
                MySqlCommand cmd = new MySqlCommand();

                foreach (GridViewRow gvr in gvCodes.Rows)
                {
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");

                    if (String.IsNullOrEmpty(txtAmount.Text))
                    {
                        amount = 0;
                    }
                    else
                    {
                        amount = Convert.ToDecimal(txtAmount.Text);
                    }

                    sql = "SELECT count(*) FROM pay_reg where co_sl='" + Session["CoSl"] + "' and prg_edcd = '" + gvr.Cells[0].Text.ToString() + "' and " +
                        " prg_Emno =  " + ddlName.SelectedValue + " and prg_rlcd = " + ddlSalType.SelectedValue + "  and prg_yymm = '" + yymm + "'  and prg_yid = '" + Session["YearSl"] + "'";
                    empCode = mGlobal.getResult(sql);

                    if (empCode == "1".ToString())
                    {
                        cmd.CommandText = " update pay_reg set prg_AMNT = " + amount + " , prg_porr= '" + gvr.Cells[2].Text.ToString() + "',prg_narr = '" + txtnarration.Text + "'," +
                            " prg_updby = '" + Session["LoginName"] + "', prg_updon = sysdate() " +
                            "  where co_sl='" + Session["CoSl"] + "' and prg_edcd = '" + gvr.Cells[0].Text.ToString() + "' and " +
                        " prg_emno =  " + ddlName.SelectedValue + " and prg_rlcd = " + ddlSalType.SelectedValue + "  and prg_yymm = '" + yymm + "'  and prg_yid = '" + Session["YearSl"] + "'";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;
                        status = 1;

                    }
                    else if ((txtAmount.Text) != "")
                    {
                        cmd.CommandText = "INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,prg_edcd,PRG_PORR,PRG_AMNT,PRG_NARR,CO_SL,PRG_YID,prg_insby,prg_inson) VALUE " +
                          " ( " + ddlName.SelectedValue + ",'" + yymm + "', " + ddlSalType.SelectedValue + ",'" + gvr.Cells[0].Text.ToString() + "','" + gvr.Cells[2].Text.ToString() + "', " +
                          " '" + amount + "','" + txtnarration.Text + "'," +
                          " '" + Session["CoSl"] + "','" + Session["YearSl"] + "','" + Session["LoginName"] + "',sysdate()) ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;
                        status = 1;
                    }
                }

                if (status == 1)
                {
                    // delete gross ded and  net
                    cmd.CommandText = " delete from pay_reg   where co_sl='" + Session["CoSl"] + "' and prg_edcd  in (500,900,999) and " +
                  " prg_emno =  " + ddlName.SelectedValue + " and prg_rlcd = " + ddlSalType.SelectedValue + "  and prg_yymm = '" + yymm + "'  and prg_yid = '" + Session["YearSl"] + "'";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    //delete pay_reg_hdr
                    cmd.CommandText = " delete from pay_reg_hdr   where co_sl='" + Session["CoSl"] + "'   and " +
                   " prh_emno =  " + ddlName.SelectedValue + " and prh_rlcd = " + ddlSalType.SelectedValue + "  and prh_yymm = '" + yymm + "'  and prh_yid = '" + Session["YearSl"] + "'";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;
                    // gross insert
                    cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson,prg_narr) " +
                               " VALUES(" + ddlName.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                               "  500, 'P', '" + txtGross.Text + "','" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate(),'" + txtnarration.Text + "') ";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    // ded insert
                    cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson,prg_narr) " +
                              " VALUES(" + ddlName.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                              "  900, 'R', '" + txtded.Text + "','" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate(),'" + txtnarration.Text + "') ";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    // net insert
                    cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson,prg_narr) " +
                              " VALUES(" + ddlName.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                              "  999, 'N', '" + txtNet.Text + "','" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate(),'" + txtnarration.Text + "') ";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    // pay_reg_hdr insert
              
                    cmd.CommandText = "INSERT INTO pay_reg_hdr(PRH_EMNO,PRH_YYMM,PRH_RLCD,PRH_LOCD,PRH_DPCD,PRH_YID,CO_SL,PRH_GROSS,PRH_DED,PRH_NET,PRH_NARR) " +
                              " VALUES(" + ddlName.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ",1, ' " + dpcd + "', " +
                              "   " + Session["YearSl"] + ",'" + Session["CoSl"] + "','" + txtGross.Text + "','" + txtded.Text + "','" + txtNet.Text + "','" + txtnarration.Text + "') ";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;
                    ShowMsgBox.Show("Salary Saved Sucessfully !....");
                    btnClose_Click(sender, e);
                }
            }

            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }
            finally
            {
                mGlobal.conDatabase.Close();
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            gross_ded_net();
        }
        protected void gross_ded_net()
        {
            decimal gross = 0;
            decimal ded = 0;
            decimal net = 0;
            lblMsg.Text = "";
            try
            {
                foreach (GridViewRow gvr in gvCodes.Rows)
                {
                    TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                    if (txtAmount.Text != "")
                    {
                        if (gvr.Cells[2].Text.ToString() == "P".ToString())
                        {
                            gross = gross + Convert.ToDecimal(txtAmount.Text);
                        }
                        else
                        {
                            ded = ded + Convert.ToDecimal(txtAmount.Text);
                        }
                    }
                    net = gross - ded;
                    txtGross.Text = Convert.ToString(gross);
                    txtded.Text = Convert.ToString(ded);
                    txtNet.Text = Convert.ToString(net);

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
            }
        }
    }
}