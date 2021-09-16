using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace OnlineFee.Views
{
    public partial class ContactUsStu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCompanyName.Text = Session["CoName"].ToString();
                lblName.Text = Session["LoginName"].ToString();
                txtEmail.Text = "";
                txtContactNo.Text = "";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string to = "vijay@inhawk.com".ToString(); // txtEmail.Text;
                string subject = txtSubject.Text;
                string body = txtDescriptions.Text;
                using (MailMessage mm = new MailMessage(to, to))
                {
                    mm.Subject =  " Login Name:" + lblName.Text + "::"+ txtSubject.Text;
                    mm.Body = txtDescriptions.Text + Environment.NewLine + " Company Name:" + lblCompanyName .Text + Environment.NewLine +  " Name:" + lblName.Text + Environment.NewLine +   Environment.NewLine + " From mail id:" + txtEmail.Text + "" +  Environment.NewLine + 
                        " Contact No:" + txtContactNo.Text;
                    if (fuAttachment.HasFile)
                    {
                        string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                        mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                    }
                    mm.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("noreplayuas@gmail.com", "uasr123$");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                    ShowMsgBox.Show("Email Sent Sucessfully..");
                    
                }
                lblName.Text = Session["LoginName"].ToString();
                lblCompanyName.Text = Session["CoName"].ToString();
                txtEmail.Text = "";
                txtContactNo.Text = "";
                txtDescriptions.Text = "";
                txtSubject.Text = "";
                

            }
            catch (Exception ex)
            {
                ShowMsgBox.Show(ex.Message); 
            }
            finally
            {

            }
        }

        private string Chr(int v)
        {
            throw new NotImplementedException();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblCompanyName.Text = Session["CoName"].ToString();
            lblName.Text = Session["LoginName"].ToString();
            txtEmail.Text = "";
            txtContactNo.Text = "";
            txtDescriptions.Text = "";
            txtSubject.Text = "";
          
            

        }
    }
}