using System;
using System.Collections.Generic;
using System.Linq;
using prototype.App_Code;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Campus.Views
{
    public partial class first : System.Web.UI.Page
    {
        String query;
        MySqlCommand mcd;
        MySqlDataReader mdr;
        string sql;
        private DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CoSl"] = 0; 
                Session["YearSl"] = 0;
                Session["LoginName"] = 0;
                Session["YearDesc"] = 0;
                Session["LoginSl"] = 0;

                ddlUserType.Items.Clear();
                ddlUserType.Items.Insert(0, new ListItem("--  User Type  --", "0"));
                ddlUserType.Items.Insert(1, new ListItem("Admin", "1"));
               // BindData();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            if (ddlUserType.SelectedItem.Text.ToUpper() == "admin".ToUpper())
            {
                UniversityLogin();
            }
            
        }





      
        
        protected void UniversityLogin()
        {

            try
            {
                
                query = "SELECT  sl,user_name as name,admin_yn,co_sl  FROM login WHERE    UPPER(user_name) = '" + txtUserName.Text.ToUpper() + "' " +
                        " and  UPPER(password) = '" + txtPassword.Text.ToUpper() + "' ";

                using (MySqlConnection connectionS = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    connectionS.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connectionS))
                    {
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                Session["LoginSl"] = dr.GetString("sl");
                                Session["CoSl"] =  dr.GetString("co_sl");
                                sql = "select pay_id from co where  sl =" + Session["CoSl"];
                                Session["PayId"] = mGlobal.getResult(sql);
                                Session["LoginName"] = txtUserName.Text.ToUpper();
                                Session["YearSl"] = "0";
                                Session["AdminYN"] = dr.GetString("admin_yn");

                                if (Session["YearSl"] == null || Session["YearSl"].ToString() == "0")
                                {
                                    sql = "select sl from yr where curdate() between start_date and end_date";
                                    Session["YearSl"] = mGlobal.getResult( sql);
                                    sql = "select descs from yr where curdate() between  start_date and end_date";
                                    Session["YearDesc"] = mGlobal.getResult( sql);
                                }
                                Session["RecNoStart"] = Session["YearDesc"] + "";
                                Response.Redirect("~/Views/Home.aspx");
                                Session.RemoveAll();
                            }
                            else
                            {
                                Session["YearSl"] = 0;
                                Session["LoginName"] = 0;
                                Session["YearDesc"] = 0;
                                Session["AdminYN"] = 0;
                                Session["LoginSl"] = 0;

                                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                                sb2.Append(@"<script>");
                                sb2.Append("alert('Invalid UserName or Password');");
                                sb2.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);

                            }
                        }
                    }
                }

 
               

            }
            catch (Exception ex)
            {
                System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                sb2.Append(@"<script>");
                sb2.Append("alert(" + ex.Message + ");");
                sb2.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
            }
            finally
            {
                
            }
        }
        private void BindData()
        {
            try
            {
                //if (Session["YearSl"] == null || Session["YearSl"].ToString() == "0")
                //{
                //    sql = "select sl from yr where curdate() between start_date and end_date";
                //    Session["YearSl"] = mGlobal.getResult(sql);
                //    sql = "select descs from yr where curdate() between  start_date and end_date";
                //    Session["YearDesc"] = mGlobal.getResult(sql);
                //}
                //sql = "SELECT   sl,DATE_FORMAT(noti_date,'%d-%b-%Y') Notification_Date,  noti_subject,  nar,  att, DATE_FORMAT(noti_end_date,'%d-%b-%Y') noti_end_date FROM noti_co WHERE   co_sl='" + Session["CoSl"] + "' AND  drange_sl='" + Session["YearSl"] + "' and noti_end_date>=sysdate()  and  noti_date<=sysdate() order by noti_date desc  ";
                //mGlobal.bindataGrid(gvView, sql);

            

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
        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          //  gvView.PageIndex = e.NewPageIndex;
          //  BindData();
        }
    }
}