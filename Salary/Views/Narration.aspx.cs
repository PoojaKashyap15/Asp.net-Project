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
    public partial class Narration : System.Web.UI.Page
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

                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddltype);
                ddltype.Items.Insert(0, new ListItem("--Select Salary Type--", "0"));

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
               

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                sql = " select PRH_EMNO,pr.prs_name,PRH_GROSS,PRH_DED,PRH_NET,PRH_NARR from pay_reg_hdr p,pay_personnel pr where p.PRH_EMNO=pr.prs_emno and p.CO_SL='" + Session["CoSl"] + "' and  p.PRH_YID =  '" + Session["YearSl"] + "'"+
                    " and p.CO_SL=pr.CO_SL and p.PRH_YYMM='" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' and prh_dpcd = '" + ddlName.SelectedValue + "'  and prh_rlcd = " + ddltype.SelectedValue + " order by PRH_EMNO";
                mGlobal.bindataGrid(gvCodes, sql);
                gvCodes.Visible = true;
                
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
        protected void gvCodes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCodes.PageIndex = e.NewPageIndex;
            btnView_Click(sender, e);
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string sql = "";
            lblMsg.Text = "";
            bool IsAdded = false;
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
                sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' " +
            " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_rlcd = " + ddltype.SelectedValue + " and prh_yid = '" + Session["YearSl"].ToString() + "' AND prh_dpcd =  " + ddlName.SelectedValue + "";

                string lock_yn = mGlobal.getResult(sql);

                if (Convert.ToDouble(lock_yn) > 0)
                {
                    lblMsg.Text = "Sorry!.. This month locked..you cannot update narration";
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
                TextBox txtnarration = (TextBox)gvr.FindControl("txtnarration");

                    cmd.CommandText = "update pay_reg_hdr set " +
                 " PRH_NARR= '" + txtnarration.Text + "' where PRH_EMNO='"+ gvr.Cells[0].Text.ToString() + "' and Co_Sl= '" + Session["CoSl"] + "' and  PRH_DPCD='" + ddlName.SelectedValue + "' and PRH_YID='" + Session["YearSl"] + "' and PRH_YYMM='" + Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM") + "' and PRH_RLCD='" + ddltype.SelectedValue + "'";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    IsAdded = cmd.ExecuteNonQuery() > 0;
                                     
                }
                if (IsAdded)
                {
                    lblMsg.Text = " Narration Updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                    
                }
                else
                {
                    lblMsg.Text = "Error while Updating details";
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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelError.Visible = true;
            lblsl.Text = "";
            lblMsg.Text = "";
            txtmmyyyy.Text = "";
           
            ddlName.SelectedValue = "0".ToString();
            ddltype.SelectedValue = "0".ToString();
            gvCodes.Visible = false;
            ddlName.Enabled = true;
            txtmmyyyy.Enabled = true;
        }
    }
}