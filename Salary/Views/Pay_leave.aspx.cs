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
    public partial class Pay_leave : System.Web.UI.Page
    {
        string sql;
        DocumentFormat.OpenXml.Drawing.Charts.DataTable dt;
        private object txtcladd;

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
                    Binddropdown();
                    btnSave.Visible = false;
                    btnCloseView.Visible = false;
                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }
        protected void Binddropdown()
        {
            try
            {
                sql = "select dpt_desc as name,dpt_code as sl from psys_dept where active_yn = 1  and co_sl = " + Session["CoSl"] + " order by dpt_desc";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- All Department --", "0"));

                //Fill Years
                Int16 endyear = Convert.ToInt16(System.DateTime.Now.Year.ToString());
                for (int i = 2000; i <= endyear; i++)
                {
                    ddlYear.Items.Add(i.ToString());
                }
                //set current year as selected
                ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;




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
        protected void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                sql = "SELECT l.lev_emno,p.prs_name,l.lev_clob,l.lev_clcb,l.lev_cladd,l.lev_elob,l.lev_elcb,l.lev_eladd " +
                    " FROM  psys_dept d, pay_personnel p, pay_leave l  where p.prs_emno = l.lev_emno and p.co_Sl = l.co_sl  " +
                    " and  d.dpt_code = p.prs_dpcd AND d.co_sl = p.co_sl AND l.lev_year='" + Convert.ToString(ddlYear.SelectedItem) + "' " +
                   " and d.dpt_code='" + ddlDept.SelectedValue + "' and d.co_sl=p.co_sl and p.co_sl='" + Session["CoSl"] + "' and p.active_yn='" + 1 + "'  order by prs_name ";

                mGlobal.bindataGrid(gvleave, sql);
                panelAddEdit.Visible = true;
                gvleave.Visible = true;
                btnSave.Visible = true;
                btnCloseView.Visible = true;

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

        protected void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PrintYN"].ToString() == "0")
                {
                    ShowMsgBox.Show("Sorry..you don't have PRINT permission!...Please contact system admin");
                    return;
                }

                sql = "SELECT l.lev_emno,p.prs_name,l.lev_clob,l.lev_clcb,l.lev_cladd,l.lev_elob,l.lev_elcb,l.lev_eladd " +
                     " FROM  psys_dept d, pay_personnel p LEFT OUTER JOIN pay_leave l ON p.prs_emno = l.lev_emno " +
                     "WHERE d.dpt_code = p.prs_dpcd AND d.co_sl = p.co_sl AND l.lev_year='" + Convert.ToString(ddlYear.SelectedItem) + "' " +
                    " and d.dpt_code='" + ddlDept.SelectedValue + "' and d.co_sl=p.co_sl and p.co_sl='" + Session["CoSl"] + "' and p.active_yn='" + 1 + "'  order by l.lev_emno ";

                VSF.ExportToExcel(mGlobal.conDatabase, " PAY Leave", sql);


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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelError.Visible = true;
            lblsl.Text = "";
            lblMsg.Text = "";
            ddlDept.SelectedValue = "0".ToString();
            ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
            ddlDept.Enabled = true;
            gvleave.Visible = false;
            btnSave.Visible = false;
            btnCloseView.Visible = false;
        }


        protected void btnCloseView_Click(object sender, EventArgs e)
        {
            gvleave.Visible = false;
            ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
            ddlDept.SelectedValue = "0".ToString();
            btnCloseView.Visible = false;
            btnSave.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;
            //string empCode = "";
            Double clcb;
            Double clob;
            Double elcb;
            Double elob;
            string updatevalues = "";

            try
            {
                string yymm = Convert.ToString(ddlYear.SelectedItem);
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

                foreach (GridViewRow gvr in gvleave.Rows)
                {
                    clob = 0;
                    clcb = 0;
                    elob = 0;
                    elcb = 0;


                    TextBox txtclob = (TextBox)gvr.FindControl("txtclob");
                    TextBox txtclcb = (TextBox)gvr.FindControl("txtclcb");
                    TextBox txtcladd = (TextBox)gvr.FindControl("txtcladd");
                    TextBox txtelob = (TextBox)gvr.FindControl("txtelob");
                    TextBox txtelcb = (TextBox)gvr.FindControl("txtelcb");
                    TextBox txteladd = (TextBox)gvr.FindControl("txteladd");


                    clob = Convert.ToDouble(txtcladd.Text) + Convert.ToDouble(gvr.Cells[2].Text.ToString());
                    clcb = Convert.ToDouble(txtcladd.Text) + Convert.ToDouble(gvr.Cells[3].Text.ToString());

                    elob = Convert.ToDouble(txteladd.Text) + Convert.ToDouble(gvr.Cells[5].Text.ToString());
                    elcb = Convert.ToDouble(txteladd.Text) + Convert.ToDouble(gvr.Cells[5].Text.ToString());


                    updatevalues = updatevalues + "update pay_leave set  LEV_CLOB ='" + clob + "', " +
                            " LEV_CLCB = '" + clcb + "',  " +
                            " LEV_ELOB = '" + elob + "', LEV_ELCB = '" + elcb + "',lev_updby='" + Session["LoginName"] + "',lev_updon=sysdate() " +
                            " where LEV_EMNO='" + gvr.Cells[0].Text.ToString() + "' and co_sl='" + Session["CoSl"] + "' and LEV_YEAR='" + yymm + "'; ";

                }

                cmd.CommandText = updatevalues;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    btnshow_Click(sender, e);
                    lblMsg.Text = "Leave Updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                }
                else
                {
                    lblMsg.Text = "Error while Updating leave details";
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
                mGlobal.conDatabase.Close();

            }
        }

        protected void bntNewEmp_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;

            try
            {

                lblMsg.Text = "";
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                mGlobal.conDatabase.Open();
                MySqlCommand cmd = new MySqlCommand();

                string yymm = Convert.ToString(ddlYear.SelectedItem);

                cmd.CommandText = "INSERT INTO pay_leave(LEV_YEAR,LEV_EMNO,LEV_CLOB,LEV_CLCB,LEV_CLADD,LEV_ELOB,LEV_ELCB,LEV_ELADD,co_sl,lev_insby,lev_inson) " +
                     " SELECT " + yymm + ",prs_Emno,0,0,0,0,0,0,co_sl,'" + Session["LoginName"] + "',sysdate() FROM pay_personnel WHERE co_sl = '" + Session["CoSl"] + "' " +
                     " AND active_yn = 1 and prs_dpcd ='" + ddlDept.SelectedValue + "'   and prs_emno not in (select LEV_EMNO from  pay_leave where  LEV_YEAR = " + yymm + " and " +
                     " co_sl = '" + Session["CoSl"] + "')";


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                IsAdded = cmd.ExecuteNonQuery() > 0;
                if (IsAdded)
                {
                    lblMsg.Text = "New Employee Added successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
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