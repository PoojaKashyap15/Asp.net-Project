using System;
using System.Collections.Generic;
using System.Linq;
using prototype.App_Code;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
namespace Campus.UserControl
{
    public partial class InstituteHeader : System.Web.UI.UserControl
    {
        String query;
        MySqlCommand mcd;

        double logo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginName"] == null || Session["LoginName"].ToString() == "0")
            {
                Response.Redirect("~/Views/Login.aspx");
            }
            if (!IsPostBack)
            {
                BindInstitute();
                DataTable dt = this.GetData(0);
                // PopulateMenu(dt, 0, null);
                GetMenuData();

            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript
(Page.GetType(), "addClickBehavior",
"addClickBehavior(document.getElementById('" +
Menu1.ClientID + "'));", true);
            base.OnPreRender(e);
        }
        private void BindInstitute()
        {

            try
            {
                query = "SELECT  name,short_name,IFNULL(LENGTH(logo), 0) AS logo  FROM co WHERE  sl ='" + Session["CoSl"] + "' AND active_yn='1'";

                using (MySqlConnection connectionS = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    connectionS.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connectionS))
                    {
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                //lblInstituteName.Text = dr.GetString("name");
                                lblInsShortName.Text = dr.GetString("short_name");
                                Session["CoName"] = dr.GetString("name");
                                logo = dr.GetFloat("logo");
                                lblLoginName.Text = Session["LoginName"].ToString();
                                lblLoginTime.Text = Session["YearDesc"].ToString();  //DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");

                            }
                            else
                            {
                                // lblInstituteName.Text = "No Data Found";
                                Response.Redirect("~/Views/Login.aspx");
                            }
                        }
                    }
                }


                if (logo == 0)
                {
                  //  ImageColLogo.ImageUrl = "~/Attachment/DefaultPhoto/logo.png";
                    ImageColLogo.ImageUrl = "~/Attachment/DefaultPhoto/" + Session["CoSl"] + ".png";
                }
                else
                {
                    ImageColLogo.ImageUrl = "~/UserControl/ShowImageFirm.ashx?id=" + Session["CoSl"];
                }

            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("error in settings! contact system admin");

            }
            finally
            {


            }
        }


        private DataTable GetData(int parentMenuId)
        {

            string query = "SELECT m.sl, Title, Description, Url,add_yn,edit_yn,view_yn,print_yn FROM Menus m , permission p  WHERE m.sl = p.menus_sl and   active_yn = 1 and view_yn = 1 and user_sl = '" + Session["LoginSl"] + "' and ParentMenuId = @ParentMenuId order by m.sl";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                DataTable dt = new DataTable();
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Parameters.AddWithValue("@ParentMenuId", parentMenuId);
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }

        }



        private void PopulateMenu(DataTable dt, int parentMenuId, MenuItem parentMenuItem)
        {
            try
            {

                string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
                foreach (DataRow row in dt.Rows)
                {
                    MenuItem menuItem = new MenuItem
                    {
                        Value = row["sl"].ToString(),
                        Text = row["Title"].ToString(),
                        NavigateUrl = row["Url"].ToString(),
                        Selected = row["Url"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                    };
                    if (parentMenuId == 0)
                    {
                        Menu1.Items.Add(menuItem);
                        DataTable dtChild = this.GetData(int.Parse(menuItem.Value));
                        PopulateMenu(dtChild, int.Parse(menuItem.Value), menuItem);
                    }
                    else
                    {
                        parentMenuItem.ChildItems.Add(menuItem);
                    }
                }

            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("error in settings! contact system admin");
            }


        }

        private void GetMenuData()
        {
            DataTable table = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(constr);
            string sql = "SELECT m.sl, Title, Description, Url,add_yn,edit_yn,view_yn,print_yn,ParentMenuId FROM Menus m , permission p  WHERE m.sl = p.menus_sl and   active_yn = 1 and view_yn = 1 and user_sl = '" + Session["LoginSl"] + "'  order by  m.sl";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(table);
            DataView view = new DataView(table);
            view.RowFilter = "ParentMenuId=0";
            foreach (DataRowView row in view)
            {
                MenuItem menuItem = new MenuItem(row["Title"].ToString(),
                    row["sl"].ToString());
                menuItem.NavigateUrl = row["Url"].ToString();
                Menu1.Items.Add(menuItem);
                AddChildItems(table, menuItem);
            }
        }
        private void AddChildItems(DataTable table, MenuItem menuItem)
        {
            DataView viewItem = new DataView(table);
            viewItem.RowFilter = "ParentMenuId=" + menuItem.Value;
            foreach (DataRowView childView in viewItem)
            {
                MenuItem childItem = new MenuItem(childView["Title"].ToString(),
                childView["sl"].ToString());
                childItem.NavigateUrl = childView["Url"].ToString();
                menuItem.ChildItems.Add(childItem);
                AddChildItems(table, childItem);
            }
        }



    }


}