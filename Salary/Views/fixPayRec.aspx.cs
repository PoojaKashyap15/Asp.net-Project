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
    public partial class fixPayRec : System.Web.UI.Page
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

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                sql = "SELECT COD_CODE,COD_DESC,COD_TYPE, FIX_AMNT FROM psys_codes c LEFT OUTER JOIN pay_fixed f ON  COD_CODE = fix_edcd " +
                " AND fix_Emno =  " + ddlName.SelectedValue + "  AND f.co_Sl = '" + Session["CoSl"] + "'  WHERE UPPER(cod_calc) = UPPER('manual') AND C.ACTIVE_YN = 1 and c.co_sl = " + Session["CoSl"] + " order by cod_code";
                mGlobal.bindataGrid(gvCodes, sql);
                panelView.Visible = true;
                ddlName.Enabled = false;

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
            try
            {
                if (Session["AddYN"].ToString() == "0")
                {
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    sb2.Append(@"<script>");
                    sb2.Append("alert('Sorry..you dont have ADD permission!...Please contact system admin');");
                    sb2.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
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

                    sql = "SELECT count(*) FROM pay_fixed where co_sl='" + Session["CoSl"] + "' and fix_edcd = '" + gvr.Cells[0].Text.ToString() + "' and " +
                        " fix_emno = " + ddlName.SelectedValue + "";
                    empCode = mGlobal.getResult(sql);

                    if (empCode == "1".ToString())
                    {
                        cmd.CommandText = " update pay_fixed set FIX_PORR ='" + gvr.Cells[2].Text.ToString() + "', FIX_AMNT = " + amount + ", " +
                            " FIX_UPDBY = '" + Session["LoginName"] + "' , FIX_UPDON = sysdate() where  FIX_EMNO =  " + ddlName.SelectedValue + " " +
                            " and   FIX_EDCD =  '" + gvr.Cells[0].Text.ToString() + "' and co_sl = '" + Session["CoSl"] + "' ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;
                        status = 1;

                    }
                    else if ((txtAmount.Text) != "")
                    {
                        cmd.CommandText = "INSERT INTO pay_fixed(FIX_EMNO, FIX_EDCD, FIX_PORR, FIX_SLNO, FIX_IORN, FIX_AMNT, CO_SL, FIX_INSBY, FIX_INSON) VALUE " +
                          " ( " + ddlName.SelectedValue + ",'" + gvr.Cells[0].Text.ToString() + "','" + gvr.Cells[2].Text.ToString() + "',1,'N'," + amount + "," +
                          " '" + Session["CoSl"] + "','" + Session["LoginName"] + "',sysdate()) ";

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
    }
}