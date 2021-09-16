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
    public partial class UserCreation : System.Web.UI.Page
    {
        String sql;
       
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
                    Session["Mode"] = "";
                    txtNewPassword.Text = "";
                    txtName.Text = "";
                    rbtnActive.SelectedValue = "1";
                    lblMsg.Text = "";


                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }

    


    protected void btnADD_Click(object sender, EventArgs e)
        {

            bool IsAdded = false;
            lblMsg.Text = "";
            Session["Mode"] = "Add";
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

                sql = "SELECT COUNT(*) FROM login where user_name='"+ txtName.Text + "' and co_sl ='" + Session["CoSl"] + "' ";
                int count = Convert.ToInt32(mGlobal.getResult( sql));

                if (count == 0)

                {

                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {

                        mGlobal.conDatabase.Close();
                    }

                    MySqlCommand cmd = new MySqlCommand();

                    {
                        cmd.CommandText = "INSERT INTO login (user_name, PASSWORD, active_yn, co_sl, insert_on, insert_by)VALUES('"+ txtName.Text +"', '"+ txtNewPassword.Text +"', '"+rbtnActive.SelectedValue +"', '"+ Session["CoSl"] + "', sysdate(),'" + Session["LoginName"] + "')";
                    }

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;
                    mGlobal.conDatabase.Open();

                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    if (IsAdded)
                    {
                        lblMsg.Text = "New user added successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Session["Mode"] = "";

                    }
                    else
                    {
                        lblMsg.Text = "Error while adding  new user";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    lblMsg.Text = "User Name already exist";
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

                if(Session["Mode"].ToString() != "Add")
                {
                    txtName.Text = "";
                    txtNewPassword.Text = "";
                }
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtNewPassword.Text = "";
            txtName.Text = "";
            lblMsg.Text = "";
            rbtnActive.SelectedValue = "1";

        }

        protected void btvClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/Home.aspx");
        }
    }
}