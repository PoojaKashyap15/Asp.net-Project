using prototype.App_Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Military.Views
{
    public partial class ChangePassword : System.Web.UI.Page
    {

        String query;
        MySqlCommand mcd;
        MySqlDataReader mdr;
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

                    mGlobal.qs_check(Session["LoginSl"].ToString(),Session["CoSl"].ToString(), Request.QueryString["qsc"].ToString().ToUpper(),
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

                    //lblHeading.Text = Session["MenuName"].ToString();
                   // Session["Mode"] = "";
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
                

                txtCurPassword.Text = "";
                txtNewPassword.Text = "";
                txtNewPassword2.Text = "";
                lblMsg.Text = "";
                lblSl.Text = "";
                lblUserName.Text = "";

                query = "SELECT   sl,IFNULL(user_name,'') AS user,IFNULL(password,'') AS pw FROM login  WHERE sl ='" + Session["LoginSl"] + "' and co_Sl = '" + Session["CoSl"] + "'  ";

                using (MySqlConnection connectionS = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    connectionS.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connectionS))
                    {
                        using (MySqlDataReader mdr = command.ExecuteReader())
                        {
                            if (mdr.Read())
                            {
                                lblUserName.Text = mdr.GetString("user");
                                lblSl.Text = mdr.GetString("sl");
                                hfCurrentPassword.Value = mdr.GetString("pw");
                            }
                            else
                            {
                                lblMsg.Text = "No Data Found";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                return;
                            }

                        }
                    }
                }

                 

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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            bool IsAdded = false;

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

                if (txtCurPassword.Text.Trim() != hfCurrentPassword.Value)
                {
                    lblMsg.Text = "Current Password Wrong!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (txtNewPassword.Text.Trim() != txtNewPassword2.Text.Trim())
                {
                    lblMsg.Text = "New Password and Re-Enter password do not match!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }


                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();

                {
                    cmd.CommandText = " update login set  password = '" + txtNewPassword.Text + "', " +
                     " update_by = '" + Session["LoginName"] + "',update_on = sysdate()  where sl = '" + lblSl.Text + "' and user_name = '" + lblUserName.Text + "' and password = '" + txtCurPassword.Text + "' and co_sl ='" + Session["CoSl"] + "'";
                }

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    Bind();
                    ShowMsgBox.Show("Password Updated successfully!");
                }
                else
                {
                    lblMsg.Text = "Error while updating  password";
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
            Bind();
        }

        protected void btvClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/Home.aspx");
        }
    }
}