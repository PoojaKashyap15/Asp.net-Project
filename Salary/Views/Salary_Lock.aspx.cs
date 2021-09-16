using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using prototype.App_Code;

namespace Salary.Views
{
    public partial class Salary_Lock : System.Web.UI.Page
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
                sql = "SELECT dpt_code sl ,dpt_desc NAME FROM psys_dept where active_yn = 1  and co_sl='" + Session["CoSl"] + "'   order by dpt_desc";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- All Department --", "0"));

                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlSalType);
                ddlSalType.Items.Insert(0, new ListItem("--Select Salary Type--", "0"));

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
        protected void btnlock_Click(object sender, EventArgs e)
        {
            string sql = "";
            lblMsg.Text = "";
            bool IsAdded = false;
            try
            {
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                MySqlCommand cmd = new MySqlCommand();

                if (ddlDept.SelectedValue == "0".ToString())
                {
                    sql = "update pay_reg_hdr set " +
                   " PRH_BRNO='1' where Co_Sl= '" + Session["CoSl"] + "' and  PRH_YID='" + Session["YearSl"] + "' and PRH_YYMM='" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' and PRH_RLCD='" + ddlSalType.SelectedValue + "'";

                }
                else
                {
                    sql = "update pay_reg_hdr set " +
                 " PRH_BRNO='1' where Co_Sl= '" + Session["CoSl"] + "' and  PRH_DPCD='" + ddlDept.SelectedValue + "' and PRH_YID='" + Session["YearSl"] + "' and PRH_YYMM='" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' and PRH_RLCD='" + ddlSalType.SelectedValue + "'";

                }

                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();
                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    lblMsg.Text = " Salary Details Locked successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";


                }
                else
                {
                    lblMsg.Text = "Error while locked details";
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

        protected void btnunlock_Click(object sender, EventArgs e)
        {
            string sql = "";
            lblMsg.Text = "";
            bool IsAdded = false;
            try
            {
                if (Session["AdminYN"].ToString() == "0")
                {
                    lblMsg.Text = "Sorry..you dont have Unlock permission!...Please contact system admin ";
                    lblMsg.Font.Bold = true;  
                    lblMsg.ForeColor = System.Drawing.Color.Red; return;
                }

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                MySqlCommand cmd = new MySqlCommand();

                if (ddlDept.SelectedValue == "0".ToString())
                {
                    sql = "update pay_reg_hdr set " +
                   " PRH_BRNO='0' where Co_Sl= '" + Session["CoSl"] + "' and  PRH_YID='" + Session["YearSl"] + "' and PRH_YYMM='" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' and PRH_RLCD='" + ddlSalType.SelectedValue + "'";

                }
                else
                {
                    sql = "update pay_reg_hdr set " +
                 " PRH_BRNO='0' where Co_Sl= '" + Session["CoSl"] + "' and  PRH_DPCD='" + ddlDept.SelectedValue + "' and PRH_YID='" + Session["YearSl"] + "' and PRH_YYMM='" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' and PRH_RLCD='" + ddlSalType.SelectedValue + "'";

                }

                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();
                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    lblMsg.Text = " Salary Details unlocked successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";


                }
                else
                {
                    lblMsg.Text = "Error while unlocked details";
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            txtmmyyyy.Text = "";
            ddlDept.SelectedValue = "0".ToString();
            ddlSalType.SelectedValue = "0".ToString();

            DataTable ds = new DataTable();
            ds = null;
            panelAddEdit.Visible = true;

        }
    }
}