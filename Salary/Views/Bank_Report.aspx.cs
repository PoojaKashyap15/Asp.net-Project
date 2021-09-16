using System;
using prototype.App_Code;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;

namespace Salary.Views
{
    public partial class Bank_Report : System.Web.UI.Page
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
                    Binddropdown();
                    
                    btnCloseView.Visible = false;
                    panelAddEdit.Visible = true;

                   
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

                sql = "select bnk_code as sl,bnk_name as name from psys_bank where active_yn="+ 1 +" and co_sl='"+ Session["CoSl"] +"' order by bnk_name ";
                mGlobal.binddropdownlist(sql, ddlbank);
                ddlbank.Items.Insert(0, new ListItem("--All Bank--", "0"));
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
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                //if (ddlDept.SelectedValue == "0".ToString())
                //{
                //    sql = "select p.prs_emno,p.prs_name,p.prs_bkac,ph.prh_net " + 
                //        " from pay_personnel p,pay_reg_hdr ph " + 
                //        " where p.prs_emno=ph.prh_emno and p.co_sl=ph.co_sl  and p.prs_dpcd=ph.prh_dpcd " +
                //        " and ph.prh_yid="+ Session["YearSl"] +" and p.co_sl='"+ Session["CoSl"] +"' and ph.prh_yymm='"+ yymm + "' and ph.prh_bkcd='"+ ddlbank.SelectedValue +"' group by p.prs_emno order by p.prs_emno ";
                //}
                //else
                //{
                //    sql = "select p.prs_emno,p.prs_name,p.prs_bkac,ph.prh_net " +
                //       " from pay_personnel p,pay_reg_hdr ph " +
                //       " where p.prs_emno=ph.prh_emno and p.co_sl=ph.co_sl  and p.prs_dpcd=ph.prh_dpcd " +
                //       " and ph.prh_yid=" + Session["YearSl"] + " and p.co_sl='" + Session["CoSl"] + "' and ph.prh_yymm='" + yymm + "' and " +
                //       " ph.prh_bkcd='" + ddlbank.SelectedValue + "' and p.prs_dpcd='"+ ddlDept.SelectedValue +"' group by p.prs_emno order by p.prs_emno ";

                //}

                sql = "select p.prs_emno,p.prs_name,p.prs_bkac,ph.prh_net " +
                        " from pay_personnel p,pay_reg_hdr ph " + 
                        " where p.prs_emno=ph.prh_emno and p.co_sl=ph.co_sl  and p.prs_dpcd=ph.prh_dpcd " +
                        " and ph.prh_yid="+ Session["YearSl"] +" and p.co_sl='"+ Session["CoSl"] +"' and ph.prh_yymm='"+ yymm + "' ";
                if(ddlDept.SelectedValue!="0")
                {
                    sql = sql + " and p.prs_dpcd='" + ddlDept.SelectedValue + "' ";
                }
                if(ddlbank.SelectedValue!="0")
                {
                    sql = sql + " and p.prs_bkcd='" + ddlbank.SelectedValue + "' ";
                }
                sql = sql + " group by p.prs_emno order by p.prs_name ";

                mGlobal.bindataGrid(gvbankreport, sql);
               
                gvbankreport.Visible = true;
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

        protected void btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PrintYN"].ToString() == "0")
                {
                    ShowMsgBox.Show("Sorry..you don't have PRINT permission!...Please contact system admin");
                    return;
                }

                //sql = " select code as Code,  name as Name,address,contact_person,contact_no as Description ,if(active_yn=1,'Active','Inactive') as status from  student   where co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' order by code asc ";

                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");
                sql = "select p.prs_emno,p.prs_name,p.prs_bkac,ph.prh_net " +
                        " from pay_personnel p,pay_reg_hdr ph " +
                        " where p.prs_emno=ph.prh_emno and p.co_sl=ph.co_sl  and p.prs_dpcd=ph.prh_dpcd " +
                        " and ph.prh_yid=" + Session["YearSl"] + " and p.co_sl='" + Session["CoSl"] + "' and ph.prh_yymm='" + yymm + "' ";
                if (ddlDept.SelectedValue != "0")
                {
                    sql = sql + " and p.prs_dpcd='" + ddlDept.SelectedValue + "' ";
                }
                if (ddlbank.SelectedValue != "0")
                {
                    sql = sql + " and ph.prh_bkcd='" + ddlbank.SelectedValue + "' ";
                }
                sql = sql + " group by p.prs_emno order by p.prs_emno ";
                VSF.ExportToExcel(mGlobal.conDatabase, lblHeading.Text, sql);


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
            txtmmyyyy.Text = "";
            ddlDept.SelectedValue = "0".ToString();
            ddlbank.SelectedValue = "0".ToString();
            gvbankreport.Visible = false;
            btnCloseView.Visible = false;
            
        }

        

        protected void gvSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbankreport.PageIndex = e.NewPageIndex;
            btnshow_Click();
        }

        private void btnshow_Click()
        {
            throw new NotImplementedException();
        }

        protected void btnCloseView_Click(object sender, EventArgs e)
        {
            txtmmyyyy.Text = "";
            ddlbank.SelectedValue = "0".ToString();
            ddlDept.SelectedValue = "0".ToString();
            gvbankreport.Visible = false;
            //panelAddEdit.Visible = false;
            btnCloseView.Visible = false;
        }
    }
}