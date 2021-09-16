using System;
using prototype.App_Code;
using System.Data;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
using System.Web.UI;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;

namespace Ksmb.Views
{
    public partial class Notifications : System.Web.UI.Page
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
                    panleGridView.Visible = true;
                    panelAddEdit.Visible = false;
                    panelDelete.Visible = false;
                    panelVIEW.Visible = false;
                    this.DivSearch.Visible = true;
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
              sql = "SELECT   n.sl,DATE_FORMAT(noti_date,'%d-%b-%Y') Notification_Date,  noti_subject,  n.nar,  att, DATE_FORMAT(noti_end_date,'%d-%b-%Y') noti_end_date FROM noti_co n  WHERE   n.co_sl='" + Session["CoSl"] + "' AND  drange_sl='" + Session["YearSl"] + "'  order by n.sl desc ";
                mGlobal.bindataGrid(gvView, sql);

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


        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvView.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("detail"))
            {
                try
                {
                    if (Session["ViewYN"].ToString() == "0")
                    {
                        System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                        sb2.Append(@"<script>");
                        sb2.Append("alert('Sorry..you dont have View permission!...Please contact system admin');");
                        sb2.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                        return;
                    }

                    lblMsg.Text = "";
                    int code = Convert.ToInt32(e.CommandArgument);
                    viewData(code);
                    panleGridView.Visible = false;
                    panelAddEdit.Visible = false;
                    panelDelete.Visible = false;
                    panelVIEW.Visible = true;
                    this.DivSearch.Visible = false;

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
            else if (e.CommandName.Equals("editRecord"))
            {
                try
                {
                    if (Session["EditYN"].ToString() == "0")
                    {
                        System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                        sb2.Append(@"<script>");
                        sb2.Append("alert('Sorry..you dont have Edit permission!...Please contact system admin');");
                        sb2.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                        return;
                    }

                    lblMsg.Text = "";

                    int code = Convert.ToInt32(e.CommandArgument);
                    assignData(code);

                    btnSave.Visible = false;
                    panleGridView.Visible = false;
                    panelDelete.Visible = false;
                    panelAddEdit.Visible = true;
                    btnUpdate.Visible = true;
                    this.DivSearch.Visible = false;
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
            else if (e.CommandName.Equals("deleteRecord"))
            {
                if (Session["DeleteYN"].ToString() == "0")
                {
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    sb2.Append(@"<script>");
                    sb2.Append("alert('Sorry..you dont have DELETE permission!...Please contact system admin');");
                    sb2.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                    return;
                }

                lblMsg.Text = "";
                int code = Convert.ToInt32(e.CommandArgument);
                lblsl.Text = Convert.ToString(code);
                panleGridView.Visible = false;
                panelAddEdit.Visible = false;
                panelVIEW.Visible = false;
                panelDelete.Visible = true;
                this.DivSearch.Visible = false;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
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

            HyperLinkedit.Visible = false;

            if (Session["Mode"].ToString() != "Add")
            {
                lblMsg.Text = "";
                lblsl.Text = "";
                txtDescriptions.Text = "";
                txtEndDate.Text = "";
                txtStartDate.Text = "";
                txtSubject.Text = "";
                
            }
            
            panleGridView.Visible = false;
            panelAddEdit.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            panelDelete.Visible = false;
            panelVIEW.Visible = false;
            this.DivSearch.Visible = false;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            panleGridView.Visible = true;
            panelAddEdit.Visible = false;
            panelVIEW.Visible = false;
            panelError.Visible = true;
            panelDelete.Visible = false;
            this.DivSearch.Visible = true;
            lblsl.Text = "";
            lblMsg.Text = "";
            txtEndDate.Text = "";
            txtStartDate.Text = "";
            txtSubject.Text = "";
           
            txtDescriptions.Text = "";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int r_no;
            bool IsAdded = false;
            string FileNameSave;
           
            try
            {
                lblMsg.Text = "";

                Session["FileName"] = "";
                Session["Mode"] = "Add";

                if (FileUploadAdd.HasFile)
                {
                    string FileName = Path.GetFileName(FileUploadAdd.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUploadAdd.PostedFile.FileName);
                    string FolderPath = "~/Attachment/Notifications/";

                    if (Extension.Trim() == ".pdf")
                    {
                        if (FileUploadAdd.PostedFile.ContentLength == 0)
                        {
                            lblMsg.Text = "You must select a non empty file to upload.";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //Random No Generations
                        Random random = new Random();
                        r_no = random.Next(1, 100000);
                        FileNameSave = "C" + Session["CoSl"] + "R" + r_no + FileName;
                        string FilePath = Server.MapPath(FolderPath + FileNameSave);

                        Session["FileName"] = FileNameSave;
                        FileUploadAdd.SaveAs(FilePath);
                    }
                    else
                    {
                        lblMsg.Text = "You must select a pdf file to upload.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    lblMsg.Text = "Start Date Must be always Less then end date..";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }


             

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();

                MySqlCommand cmd = new MySqlCommand();
                
                cmd.CommandText = "Insert into noti_co(co_sl,  drange_sl, noti_date,  noti_subject,  nar,  att,  noti_end_date,  insert_on,  insert_by)    " +
                    " values('" + Session["CoSl"] + "','" + Session["YearSl"] + "','" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd") + "','" + mGlobal.doQuotes(txtSubject.Text) + "'," +
                "'" + mGlobal.doQuotes(txtDescriptions.Text) + "','" + Session["FileName"].ToString() + "','" + Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd") + "',sysdate(),'" + Session["LogonName"] + "')";
                

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;


                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    lblMsg.Text = "Record Saved successfully. . .!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "";
                    Bind();
                }
                else
                {
                    lblMsg.Text = "Error while adding Record..!";
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
                panleGridView.Visible = true;
                panelAddEdit.Visible = false;
                this.DivSearch.Visible = true;
            }
        }




        private void assignData(int code)
        {
            try
            {
                lblMsg.Text = "";

                sql = "SELECT   sl,DATE_FORMAT(noti_date,'%d-%b-%Y') Notification_Date, IFNULL(LENGTH(att), 0) AS att_len, noti_subject,  nar,  att, DATE_FORMAT(noti_end_date,'%d-%b-%Y') noti_end_date FROM noti_co  WHERE   " +
                     " co_sl='" + Session["CoSl"] + "' and sl = '" + code.ToString() + "' AND  drange_sl='" + Session["YearSl"] + "'  order by sl desc ";

                using (MySqlConnection connectionS = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    connectionS.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connectionS))
                    {
                        using (MySqlDataReader myReader = command.ExecuteReader())
                        {
                            if (myReader.Read())
                            {
                                lblsl.Text = myReader["sl"].ToString();
                                lblsl.Visible = false;
                                txtStartDate.Text = myReader["Notification_Date"].ToString();
                                txtSubject.Text = myReader["noti_subject"].ToString();
                                txtDescriptions.Text = myReader["nar"].ToString();
                                txtEndDate.Text = myReader["noti_end_date"].ToString();

                                if (myReader.GetFloat("att_len") > 0)
                                {
                                    HyperLinkedit.NavigateUrl = "~/Attachment/Notifications/" + myReader["att"].ToString();
                                    HyperLinkedit.Visible = true;
                                }
                                else
                                {
                                    HyperLinkedit.Visible = false;
                                }
                            }

                        }
                    }
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


        private void viewData(int code)
        {
            try
            {
                lblMsg.Text = "";
                
                sql = "SELECT   n.sl,DATE_FORMAT(noti_date,'%d-%b-%Y') Notification_Date,  noti_subject,  n.nar,  IFNULL(LENGTH(att), 0) AS att_len,  att, DATE_FORMAT(noti_end_date,'%d-%b-%Y') noti_end_date FROM noti_co n   WHERE   " +
                  "   n.co_sl='" + Session["CoSl"] + "' and n.sl = '" + code.ToString() + "' AND  drange_sl='" + Session["YearSl"] + "'  order by n.sl desc ";

                mGlobal.bindDetailsView(dvView, sql);

                string att_len;
                sql = "SELECT  IFNULL(LENGTH(att), 0) AS att_len FROM noti_co    WHERE   " +
                  " co_sl='" + Session["CoSl"] + "' and  sl = '" + code.ToString() + "' AND  drange_sl='" + Session["YearSl"] + "'  ";

                att_len = mGlobal.getResult( sql);

                if (Convert.ToInt32(att_len.ToString()) == 0)
                {
                    dvView.Rows[4].Visible = false;
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int r_no;
            bool IsAdded = false;
            string FileNameSave;
            try
            {
                Session["FileName"] = "";
                lblMsg.Text = "";

                if (FileUploadAdd.HasFile)
                {
                    string FileName = Path.GetFileName(FileUploadAdd.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUploadAdd.PostedFile.FileName);
                    string FolderPath = "~/Attachment/Notifications/";

                    if (Extension.Trim() == ".pdf")
                    {
                        if (FileUploadAdd.PostedFile.ContentLength == 0)
                        {
                            lblMsg.Text = "You must select a non empty file to upload.";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            return;
                        }
                        //Random No Generations
                        Random random = new Random();
                        r_no = random.Next(1, 100000);
                        FileNameSave = "C" + Session["CoSl"] + "R" + r_no + FileName;
                        string FilePath = Server.MapPath(FolderPath + FileNameSave);

                        Session["FileName"] = FileNameSave;
                        FileUploadAdd.SaveAs(FilePath);
                    }
                    else
                    {
                        lblMsg.Text = "You must select a pdf file to upload.";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    lblMsg.Text = "Start Date Must Be always Less then end date..";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();

                MySqlCommand cmd = new MySqlCommand();

                if (Session["FileName"].ToString().Trim() == "")
                {
                    cmd.CommandText = " update noti_co set co_sl = '" + Session["CoSl"] + "',  drange_sl = '" + Session["YearSl"] + "',  noti_date = '" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd") + "', " +
                        " noti_subject = '" + mGlobal.doQuotes(txtSubject.Text) + "', nar = '" + mGlobal.doQuotes(txtDescriptions.Text) + "',   " +
                        " noti_end_date = '" + Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd") + "',  update_on = sysdate(), " +
                        " update_by = '" + Session["LogonName"] + "' where sl = '" + lblsl.Text + "'";
                }
                else
                {
                    cmd.CommandText = " update noti_co set co_sl = '" + Session["CoSl"] + "',  drange_sl = '" + Session["YearSl"] + "',  noti_date = '" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd") + "', " +
                         " noti_subject = '" + mGlobal.doQuotes(txtSubject.Text) + "',  nar = '" + mGlobal.doQuotes(txtDescriptions.Text) + "',  att = '" + Session["FileName"].ToString() + "', " +
                         " noti_end_date = '" + Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd") + "',  update_on = sysdate(), " +
                         " update_by = '" + Session["LogonName"] + "' where sl = '" + lblsl.Text + "'";
                }

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;

                IsAdded = cmd.ExecuteNonQuery() > 0;


                if (IsAdded)
                {
                    lblMsg.Text = "Record Updated successfully. . .!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    lblMsg.Text = "Error while Updating Record";
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
                Bind();
                mGlobal.conDatabase.Close();
                panleGridView.Visible = true;
                panelAddEdit.Visible = false;
                this.DivSearch.Visible = true;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                bool IsDeleted = false;
                string code = lblsl.Text;

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "delete from noti_co  where sl='" + code + "' and co_sl = " + Session["CoSl"] + "";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;

                IsDeleted = cmd.ExecuteNonQuery() > 0;

                if (IsDeleted)
                {
                    lblMsg.Text = "Details has been deleted successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    panleGridView.Visible = true;
                    panelDelete.Visible = false;
                    this.DivSearch.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Error while deleting details";
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
                Bind();
                mGlobal.conDatabase.Close();
            }

        }

        protected void dvView_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                sql = "SELECT   n.sl,DATE_FORMAT(noti_date,'%d-%b-%Y') Notification_Date,  noti_subject,  n.nar,  att, DATE_FORMAT(noti_end_date,'%d-%b-%Y') noti_end_date FROM noti_co n  WHERE     n.co_sl='" + Session["CoSl"] + "' AND  drange_sl='" + Session["YearSl"] + "'" +
                       " and   (upper(noti_subject) like '%" + txtSearch.Text.ToUpper() + "%')";


                mGlobal.bindataGrid(gvView, sql);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                mGlobal.conDatabase.Close();
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
                
                sql = "SELECT   n.sl, DATE_FORMAT(noti_date,'%d-%b-%Y') Notification_Date,  noti_subject,  n.nar,  IFNULL(LENGTH(att), 0) AS att_len,  att, DATE_FORMAT(noti_end_date,'%d-%b-%Y') noti_end_date FROM noti_co n   WHERE   " +
                 "  n.co_sl='" + Session["CoSl"] + "' and drange_sl='" + Session["YearSl"] + "'  order by n.sl desc ";


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
            lblMsg.Text = "";
            txtSearch.Text = "";
            Bind();
        }
    }
}