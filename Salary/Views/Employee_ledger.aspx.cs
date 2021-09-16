using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using prototype.App_Code;

namespace Salary.Views
{
    public partial class dpt_wise_Emp_dtl : System.Web.UI.Page
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
                    ddlDept_SelectedIndexChanged(sender , e);
                    panelAddEdit.Visible = true;
                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }

        private void ddlDept_SelectedIndexChanged()
        {
            throw new NotImplementedException();
        }

        protected void Binddropdown()
        {
            try
            {
                sql = "SELECT dpt_code sl ,dpt_desc NAME FROM psys_dept where co_sl='"+ Session["CoSl"] +"' and active_yn = 1   order by dpt_desc";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- Select Department --", "0"));

                //ddlDept_SelectedIndexChanged();
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
        protected void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                //string sql = "";
                lblMsg.Text = "";
                
               
                    sql = "select prs_emno,prs_name,prg_yymm,prg_rlcd,DPT_DESC,DSG_DESC,BPAY,DA,HRA,CA,MED, " +
                        " OTH,ARREARS,BONUS,SPL_ALW,VAR_ALW,GROSS,ESI,PF,PT,ADV_REC,TDS,MOBILE,LWF,EXS_PAY,DED,NET " +
                        " from vu_empledger where co_sl='"+ Session["CoSl"] +"' and prg_yid='"+ Session["YearSl"] + "' ";
                if(ddlDept.SelectedValue!="0")
                {
                    sql = sql + " and PRS_DPCD='"+ ddlDept.SelectedValue +"'";
                }
                if(ddlname.SelectedValue!="0")
                {
                    sql = sql + " and PRS_EMNO='" + ddlname.SelectedValue + "'";
                }
                sql = sql + " GROUP BY prs_name order by prs_name";
               
                VSF.ExportToExcel(mGlobal.conDatabase, lblHeading.Text, sql);
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

        protected void btnclear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            ddlname.SelectedValue = "0".ToString();
            ddlDept.SelectedValue = "0".ToString();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sql = "select prs_emno sl ,prs_name Name from pay_personnel where prs_dpcd='"+ ddlDept.SelectedValue +"' and co_sl='" + Session["CoSl"] + "' and active_yn= 1 order by prs_name";
                mGlobal.binddropdownlist(sql, ddlname);
                ddlname.Items.Insert(0, new ListItem("-- All Employee --", "0"));

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