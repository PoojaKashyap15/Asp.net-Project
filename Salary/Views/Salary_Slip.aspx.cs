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
using System.Net.Mail;

namespace Salary.Views
{
    public partial class Salary_Slip : System.Web.UI.Page
    {
        string sql;
        DataTable dt;
        //private string activationCode;

        //private object prs_emno;
        //private object EMPID;

        public bool IsChecked { get; private set; }

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
                    chkSelecAll.Visible = false;
                    btnEmail.Visible = false;
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
                sql = "select dpt_desc as name,dpt_code as sl from psys_dept where active_yn ='"+ 1 +"'   and co_sl = " + Session["CoSl"] + " order by dpt_desc";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- All Department --", "0"));
              
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
            Bind();
        }
       private void Bind()
        {
            try
            {
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");
               
                lblMsg.Text = "";
                if (ddlDept.SelectedValue == "0".ToString())
                {
                    sql = "select p.prs_emno,p.prs_name,pr.prg_yymm,pr.prg_amnt " +
                        " from pay_personnel p, pay_reg pr, psys_dept d " +
                        " where p.prs_emno=pr.prg_emno and p.co_sl=pr.co_sl and p.co_sl=d.co_sl and p.prs_dpcd=d.dpt_code and pr.prg_yymm='" + yymm + "' and pr.prg_yid= " + Session["YearSl"] + " " +
                        " and p.co_sl='" + Session["CoSl"] + "' and p.active_yn='" + 1 + "' and pr.prg_edcd='999' GROUP BY p.PRS_EMNO ORDER BY p.prs_name ";
                }
                else
                {
                    sql = "select p.prs_emno,p.prs_name,pr.prg_yymm,pr.prg_amnt " +
                       " from pay_personnel p, pay_reg pr, psys_dept d " +
                       " where p.prs_emno=pr.prg_emno and p.co_sl=pr.co_sl and p.co_sl=d.co_sl and p.prs_dpcd=d.dpt_code and pr.prg_yymm='" + yymm + "' and pr.prg_yid= " + Session["YearSl"] + " " +
                       " and p.prs_dpcd='"+ ddlDept.SelectedValue +"' and p.co_sl='" + Session["CoSl"] + "' and p.active_yn='" + 1 + "' and pr.prg_edcd='999' GROUP BY p.PRS_EMNO ORDER BY p.prs_name ";

                }
                
                mGlobal.bindataGrid(gvsalslip, sql);
                panelAddEdit.Visible = true;
                ddlDept.Enabled = false;
                gvsalslip.Visible = true;
               
                chkSelecAll.Visible = true;
                btnEmail.Visible = true;
                btnCloseView.Visible = true;
                //ddlexam.SelectedItem.Text = myReader["exam"].ToString();

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            ddlDept.Enabled = true;
            txtmmyyyy.Text = "";
            ddlDept.SelectedValue = "0".ToString();
            chkSelecAll.Visible = false;
            btnCloseView.Visible = false;
            btnEmail.Visible = false;
            gvsalslip.Visible = false;
            
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvsalslip.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");

                if (chkView.Checked == true)
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        gvsalslip.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        MailMessage mm = new MailMessage("rahul@inhawk.com", "rahulmehtre64@gmail.com");
                        mm.Subject = "GridView Email";
                            mm.Body = "<hr />" + sw.ToString(); ;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.UseDefaultCredentials = false;
                        smtp.Host = "smtp.yandex.com";
                        smtp.EnableSsl = true;
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        NetworkCred.UserName = "rahul@inhawk.com";
                        NetworkCred.Password = "Inhawk@123";
                        
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                    }
                }
                    //if(IsChecked==false)
                    //{
                    //    ShowMsgBox.Show("please select any one Employee");
                    //}
                                              
               
            }
            }

        }
       
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnCloseView_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
           
            chkSelecAll.Visible = false;
            btnEmail.Visible = false;
            btnCloseView.Visible = false;
            txtmmyyyy.Text = "";
            ddlDept.SelectedValue = "0".ToString();
            ddlDept.Enabled = true;
        }

        protected void gvsalslip_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvsalslip.PageIndex = e.NewPageIndex;
            Bind();
        }
        private string GetResponse1(string sURL)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response1.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response1.Close();
                readStream.Close();
                return sResponse;
            }
            catch
            {
                return "";
            }
        }
        protected void chkSelecAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvsalslip.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                if (chkSelecAll.Checked == true)
                {
                    chkView.Checked = true;
                }
                else
                {
                    chkView.Checked = false;
                }
            }
        }

        protected void gvsalslip_RowCommand(object sender, GridViewCommandEventArgs e)
        {



            if (e.CommandName == "Printpayslip")
            {
                if (Session["PrintYN"].ToString() == "0")
                {
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    sb2.Append(@"<script>");
                    sb2.Append("alert('Sorry..you dont have Print permission!...Please contact system admin');");
                    sb2.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                    return;
                }
                try { 
                lblMsg.Text = "";
                int EMPID = Convert.ToInt32(e.CommandArgument);
                    //lblSl.Text = Convert.ToString(code);


                    MySqlDataReader myReader = null;

                    sql = "select p.prs_emno,r.prg_yymm from pay_personnel p,pay_reg r where p.prs_emno=r.prg_emno and p.co_sl=r.co_sl and p.prs_emno='" + EMPID + "' GROUP BY prs_emno ";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);

                    myReader = msc.ExecuteReader();
                    if (myReader.Read())
                    {
                        ViewState["prs_emno"] = myReader["prs_emno"].ToString();
                        ViewState["prg_yymm"] = myReader["prg_yymm"].ToString();
                    }
                    myReader.Close();
                    Session["ReportName"] = "";
                Session["ReportName"] = "SalarySlip.rpt";


                Session["ReportName"] = "~/reports/" + Session["ReportName"];
                Session["NoOfParameters"] = "2";

                Session["ParamVal1"] = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyy,MM");
                Session["ParamName1"] = "yyyymm";

                Session["ParamVal2"] = EMPID;
                Session["ParamName2"] = "EMPID";


                Session["SelectionFormula"] = "";
                Session["ReportSaveAsName"] = "";
                Session["ReportSaveAsName"] = "PaySlip";

                    //Session["SelectionFormula"] = "{Salary_Slip.sl} in  [" + Salary_Slip.ToString() + "] ";
                    string script = "<script language='javascript'> window.open('OpenFile.aspx');</script>";
                ScriptManager.RegisterStartupScript(this, typeof(Salary_Slip), "PopupCP", script, false);
            }

                catch (Exception ex)
                {
                    ShowMsgBox.Show("Error in Print Report!.. Please Contact Support Desk " + ex.Message);
                }
                finally
                {
                    //btnPrint.Enabled = false;
                }
            }
        }
        
    }
}