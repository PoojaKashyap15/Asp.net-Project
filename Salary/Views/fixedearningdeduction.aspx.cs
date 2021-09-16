using DocumentFormat.OpenXml.Drawing.Charts;
using MySql.Data.MySqlClient;
using prototype.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace Salary.Views
{
    public partial class Fixed_earning_and_deduction : System.Web.UI.Page
    {
        string sql;
        DocumentFormat.OpenXml.Drawing.Charts.DataTable dt;
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

                sql = "select DISTINCT(COD_CODE) sl,COD_DESC name from psys_codes where upper(COD_CALC) = upper('MANUAL') and active_yn='" + 1 + "' order by COD_DESC  ";
                VSF.binddropdownlist(mGlobal.conDatabase, sql, ddlcode);
                ddlcode.Items.Insert(0, new ListItem("-- Select Code --", "0"));

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PrintYN"].ToString() == "0")
                {
                    ShowMsgBox.Show("Sorry..you don't have PRINT permission!...Please contact system admin");
                    return;
                }

                sql = "SELECT p.prs_emno,p.prs_name,f.fix_amnt " +
                     " FROM  vu_empreg p " +
                     " LEFT OUTER JOIN pay_fixed f ON f.fix_emno = p.prs_emno AND f.co_sl = p.co_sl " +
                     "  WHERE p.prs_dpcd = '" + ddlName.SelectedValue + "' AND p.co_sl = '" + Session["CoSl"] + "' " +
                     "   GROUP BY p.prs_emno ORDER BY p.prs_name ";
                VSF.ExportToExcel(mGlobal.conDatabase, " Fix earning and deduction ", sql);


            }
            catch (Exception ex)
            {
                ShowMsgBox.Show(ex.Message);
                ShowMsgBox.Show("Error in Report!.. Please Contact Support Desk");
            }
            finally
            {

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
               
                //sql = "SELECT p.prs_emno,p.prs_name,f.fix_amnt "+
                //    " FROM psys_dept d, psys_codes c,pay_personnel p " +
                //    " LEFT OUTER JOIN pay_fixed f ON f.fix_emno = p.prs_emno AND f.co_sl = '"+ Session["CoSl"] +"' "+
                //    " WHERE p.prs_dpcd = d.dpt_code AND p.co_sl=c.co_sl and  d.co_sl=c.co_sl " +
                //    " AND d.dpt_code = '"+ ddlName.SelectedValue +"' and c.COD_CODE='"+ ddlcode.SelectedValue +"' and p.co_sl='"+ Session["CoSl"] +"' GROUP BY p.prs_emno ORDER BY p.prs_emno";

                sql = "SELECT p.prs_emno,p.prs_name,f.fix_amnt " +
                     " FROM  vu_empreg p " +
                     " LEFT OUTER JOIN pay_fixed f ON f.fix_emno = p.prs_emno AND f.co_sl = p.co_sl " +
                     "  WHERE p.prs_dpcd = '" + ddlName.SelectedValue + "' AND p.co_sl = '" + Session["CoSl"] + "' " +
                     "   GROUP BY p.prs_emno ORDER BY p.prs_name ";


                mGlobal.bindataGrid(gvCodes, sql);
                
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



        protected void btnclose_Click(object sender, EventArgs e)
        {
            //gvCodes.Visible = false;
            //ddlcode.SelectedValue = "0".ToString();
            //ddlName.SelectedValue = "0".ToString();
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

                    sql = "SELECT count(*) FROM pay_fixed where co_sl='" + Session["CoSl"] + "'  and FIX_EMNO = '" + gvr.Cells[0].Text.ToString() + "'  ";
                    empCode = mGlobal.getResult(sql);

                    if (empCode == "1".ToString())
                    {
                        cmd.CommandText = " update pay_fixed set   FIX_AMNT = " + amount + ", " +
                             " FIX_UPDBY = '" + Session["LoginName"] + "' , FIX_UPDON = sysdate() where FIX_EMNO =  '" + gvr.Cells[0].Text.ToString() + "'  " +
                             "  and co_sl = '" + Session["CoSl"] + "' ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;
                        status = 1;

                }
                    else if ((txtAmount.Text) != "")
                {
                    cmd.CommandText = "INSERT INTO pay_fixed(FIX_EMNO, FIX_SLNO, FIX_IORN, FIX_AMNT, CO_SL, FIX_INSBY, FIX_INSON,FIX_EDCD) VALUE " +
                      " ( '" + gvr.Cells[0].Text.ToString() + "',1,'N'," + amount + "," +
                      " '" + Session["CoSl"] + "','" + Session["LoginName"] + "',sysdate(),'" + ddlcode.SelectedValue + "') ";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;
                    status = 1;
                }

            }
                //if (IsAdded)
                //{
                //    lblMsg.Text = " Fix earning and Deduction Saved successfully!";
                //    lblMsg.ForeColor = System.Drawing.Color.Green;
                //    Session["Mode"] = "0";

                //}
                //else
                //{
                //    lblMsg.Text = "Error while Updating details";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}
                if (status == 1)
                {
                    ShowMsgBox.Show(" Record Saved Sucessfully !....");
                    lblMsg.Text = " Fix earning and Deduction Saved successfully!";
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
    
    

        protected void btnClear_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelError.Visible = true;
            lblsl.Text = "";
            lblMsg.Text = "";
            
            ddlName.SelectedValue = "0".ToString();
            ddlcode.SelectedValue = "0".ToString();
            ddlName.Enabled = true;
            ddlcode.Enabled = true;
        }

        protected void gvCodes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCodes.PageIndex = e.NewPageIndex;
            btnView_Click(sender, e);
        }
    }
}