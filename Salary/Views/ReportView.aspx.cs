using prototype.App_Code;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using System.Drawing;
namespace Salary.Views
{
    public partial class ReportView : System.Web.UI.Page
    {
        string sql;
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
                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlSalType);

                sql = "select  rpt_name as  name,  sl from rpt_head where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlReport);
                ddlReport.Items.Insert(0, new ListItem("-- Select Report Name --", "0"));

                sql = "select dpt_desc as name,dpt_code as sl from psys_dept where active_yn = 2  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- All Department --", "0"));


                sql = "select prs_name as name,prs_emno as sl from pay_personnel where  co_sl = " + Session["CoSl"] + " order by prs_emno";
                mGlobal.binddropdownlist(sql, ddlEmpName);
                ddlEmpName.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
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


                Session["ReportName"] = "";

                string sql = " SELECT rpt_desc  FROM rpt_head WHERE sl = " + ddlReport.SelectedValue;
                Session["ReportName"] = "~/Reports/" + mGlobal.getResult(sql);

                Session["NoOfParameters"] = "4";

                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                Session["ParamVal1"] = Session["CoSl"].ToString();
                Session["ParamName1"] = "CoSl";
                Session["ParamVal2"] = ddlSalType.SelectedValue;
                Session["ParamName2"] = "Rlcd";
                Session["ParamVal3"] = Session["YearSl"].ToString();
                Session["ParamName3"] = "YearSl";
                Session["ParamVal4"] = yymm;
                Session["ParamName4"] = "YyyyMn";

                Session["SelectionFormulaDpcd"] = "";

                string SelectionFormula = "";

                SelectionFormula = "{pay_personnel1.CO_SL} =" + Session["CoSl"].ToString() + " and  "+
                    " {pay_reg1.PRG_YID} = " + Session["YearSl"].ToString()  + " and  {pay_reg1.PRG_RLCD} = " + ddlSalType.SelectedValue +
                    " and {pay_reg1.PRG_YYMM} = " + yymm + "";
                 
                if (ddlDept.SelectedValue != 0.ToString())
                {
                    SelectionFormula = SelectionFormula + " and  {pay_personnel1.PRS_DPCD} =" + ddlDept.SelectedValue.ToString() + " ";
                }

                Session["SelectionFormulaDpcd"] = "";// SelectionFormula;

                Session["ReportSaveAsName"] = "";
                Session["ReportSaveAsName"] = ddlReport.SelectedItem.Text;

                Response.Write("<script>");
                Response.Write("window.open('../PrintReport4Param.aspx','_blank')");
                Response.Write("</script>");
            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("Error in Print Report!.. Please Contact Support Desk " + ex.Message);
            }
            finally
            {

            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtmmyyyy.Text = "";
            ddlReport.SelectedValue = "0".ToString();
            ddlDept.SelectedValue = "0".ToString();
            ddlEmpName.SelectedValue = "0".ToString();
        }

        protected void btnLedgerView_Click(object sender, EventArgs e)
        {
            try
            {


                lblMsg.Text = "";

                Session["ReportName"] = "~/Reports/EmpLedger.rpt";

                Session["NoOfParameters"] = "4";
                 
                Session["ParamVal1"] = Session["CoSl"].ToString();
                Session["ParamName1"] = "CoSl";
                Session["ParamVal2"] = ddlSalType.SelectedValue;
                Session["ParamName2"] = "Rlcd";
                Session["ParamVal3"] = Session["YearSl"].ToString();
                Session["ParamName3"] = "YearSl";
                Session["ParamVal4"] = ddlEmpName.SelectedValue;
                Session["ParamName4"] = "EmpNo";

                Session["SelectionFormulaDpcd"] = "";
  
                Session["ReportSaveAsName"] = "";
                Session["ReportSaveAsName"] = ddlReport.SelectedItem.Text;

                Response.Write("<script>");
                Response.Write("window.open('../PrintReport4Param.aspx','_blank')");
                Response.Write("</script>");
            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("Error in Print Report!.. Please Contact Support Desk " + ex.Message);
            }
            finally
            {

            }
        }
    }
}