using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Campus.UserControl
{
    public partial class First : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInstitute();

            }
        }

        private void BindInstitute()
        {

            try
            {
                lblInstituteName.Text = "Online Payroll System";
                lblSubHead.Text = " ";
                ImageColLogo.ImageUrl = "~/Attachment/DefaultPhoto/MainLogo.png";

            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("error in settings! contact system admin");

            }
            finally
            {

            }
        }


    }
}