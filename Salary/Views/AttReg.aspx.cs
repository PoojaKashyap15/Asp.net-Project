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
    public partial class AttReg : System.Web.UI.Page
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
                sql = "SELECT dpt_code sl ,dpt_desc NAME FROM psys_dept where active_yn='" + 1 + "' and co_sl='" + Session["CoSl"] + "'   order by dpt_desc";
                mGlobal.binddropdownlist(sql, ddlName);
                ddlName.Items.Insert(0, new ListItem("-- Select  Department --", "0"));


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
            txtmmyyyy.Text = "";
            panelView.Visible = false;
            ddlName.SelectedValue = "0".ToString();
            ddlName.Enabled = true;
            txtmmyyyy.Enabled = true;

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {


                lblMsg.Text = "";
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                string yyyy = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyy");
                string mm = Convert.ToDateTime(txtmmyyyy.Text).ToString("MM");

                int month = Convert.ToInt16(mm);
                int year = Convert.ToInt16(yyyy);
                int days = DateTime.DaysInMonth(year, month);


                sql = "SELECT prs_emno,prs_name,ifnull(ATT_WORKING," + days + ") as ATT_WORKING , " +
                    " ifnull(ATT_ABSENT,'0') as ATT_ABSENT, ifnull(ATT_PRESENT, " + days + ") as ATT_PRESENT,IFNULL(ATT_CL,'0') AS ATT_CL,IFNULL(ATT_EL,'0') AS ATT_EL,LEV_CLCB,LEV_ELCB FROM pay_personnel p LEFT OUTER JOIN pay_attreg a ON  p.prs_emno = a.att_EMNO  " +
            "  AND a.att_yymm = '" + yymm + "' AND a.co_sl = '" + Session["CoSl"] + "' AND ATT_YID =  " + Session["YearSl"] + " " +
            " LEFT OUTER JOIN pay_leave pl ON p.prs_emno=pl.LEV_EMNO   " +
            " WHERE p.active_yn = 1 AND p.co_sl = '" + Session["CoSl"] + "'  AND prs_dpcd =  " + ddlName.SelectedValue + " GROUP BY pl.LEV_EMNO ORDER BY p.prs_name";
                mGlobal.bindataGrid(gvCodes, sql);
                panelView.Visible = true;
                ddlName.Enabled = false;
                txtmmyyyy.Enabled = false;

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

                if (ddlName.SelectedValue == "0".ToString())
                {
                    sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + yymm + "' " +
                   " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_yid = '" + Session["YearSl"].ToString() + "'";
                   
                }
                else
                {
                    sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + yymm + "' " +
                   " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_yid = '" + Session["YearSl"].ToString() + "' AND prh_dpcd =  " + ddlName.SelectedValue + "";
                 
                }

                string lock_yn = mGlobal.getResult(sql);

                if (Convert.ToDouble(lock_yn) > 0)
                {
                    lblMsg.Text = "Sorry!.. This month locked..you cannot calculate Attendance";
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
                    TextBox txtWorking = (TextBox)gvr.FindControl("txtWorking");
                    TextBox txtAbsent = (TextBox)gvr.FindControl("txtAbsent");
                    TextBox txtPresent = (TextBox)gvr.FindControl("txtPresent");
                    TextBox txtCL = (TextBox)gvr.FindControl("txtCL");
                    TextBox txtEL = (TextBox)gvr.FindControl("txtEL");



                    sql = "SELECT count(*) FROM pay_attreg where co_sl='" + Session["CoSl"] + "' AND  ATT_YID =  " + Session["YearSl"] + " " +
                        " and ATT_EMNO = '" + gvr.Cells[0].Text.ToString() + "' and ATT_YYMM = '" + yymm + "' AND " +
                        " ATT_DEPT = " + ddlName.SelectedValue + "";
                    empCode = mGlobal.getResult(sql);

                    if (empCode == "1".ToString())
                    {
                        cmd.CommandText = " update pay_attreg set  ATT_WORKING = " + txtWorking.Text + ", " +
                            " ATT_PRESENT = '" + txtPresent.Text + "', ATT_ABSENT = '" + txtAbsent.Text + "', ATT_CL = '" + txtCL.Text + "', ATT_EL = '" + txtEL.Text + "'," +
                            " ATT_UPDBY = '" + Session["LoginName"] + "' , ATT_UPDON = sysdate() where co_sl='" + Session["CoSl"] + "' AND  ATT_YID =  " + Session["YearSl"] + " " +
                        " and ATT_EMNO = '" + gvr.Cells[0].Text.ToString() + "' and ATT_YYMM = '" + yymm + "' AND " +
                        " ATT_DEPT = " + ddlName.SelectedValue + "";
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;
                        status = 1;

                    }
                    else  
                    {
                        cmd.CommandText = "INSERT INTO pay_attreg(ATT_YYMM,ATT_EMNO,ATT_WORKING,ATT_PRESENT,ATT_ABSENT,ATT_CL,ATT_EL,ATT_DEPT,CO_SL,ATT_YID, ATT_INSBY,ATT_INSON) VALUE " +
                          " ( '" + yymm + "','" + gvr.Cells[0].Text.ToString() + "','" + txtWorking.Text + "','" + txtPresent.Text + "','" + txtAbsent.Text + "', " +
                          " '" + txtCL.Text + "' ,'" + txtEL.Text + "'," + ddlName.SelectedValue + "," +
                          " '" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate()) ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;
                        status = 1;
                    }

                }

                if (status == 1)
                {
                    ShowMsgBox.Show(" Record Saved Sucessfully !....");
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

        protected void txtAbsent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                TextBox txt = sender as TextBox;
                GridViewRow row = txt.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                TextBox txtWorking = (TextBox)gvCodes.Rows[rowIndex].FindControl("txtWorking");
                TextBox txtAbsent = (TextBox)gvCodes.Rows[rowIndex].FindControl("txtAbsent");


                TextBox txtPresent = (TextBox)gvCodes.Rows[rowIndex].FindControl("txtPresent");
                if (Convert.ToDouble(txtAbsent.Text) <= Convert.ToDouble(txtWorking.Text))
                {
                    Double res = (Convert.ToDouble(txtWorking.Text) - Convert.ToDouble(txtAbsent.Text));
                    txtPresent.Text = Convert.ToString(res);
                }
                else
                {
                    lblMsg.Text = "Absent days always less than Working days";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
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

        protected void txtWorking_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                TextBox txt = sender as TextBox;
                GridViewRow row = txt.NamingContainer as GridViewRow;
                int rowIndex = row.RowIndex;

                TextBox txtWorking = (TextBox)gvCodes.Rows[rowIndex].FindControl("txtWorking");
                TextBox txtAbsent = (TextBox)gvCodes.Rows[rowIndex].FindControl("txtAbsent");


                TextBox txtPresent = (TextBox)gvCodes.Rows[rowIndex].FindControl("txtPresent");
                if (Convert.ToDouble(txtAbsent.Text) <= Convert.ToDouble(txtWorking.Text))
                {
                    double res = (Convert.ToDouble(txtWorking.Text) - Convert.ToDouble(txtAbsent.Text));
                    txtPresent.Text = Convert.ToString(res);
                }
                else
                {
                    lblMsg.Text = "Absent days always less than Working days";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
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