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

namespace Military.Views
{
    public partial class permission : System.Web.UI.Page
    {
        string sql;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Bind();
                btnAdd.Visible = false;
            }
        }
        private void Bind()
        {
            try
            {
                sql = "SELECT   sl ,user_name name  FROM login WHERE active_yn=1 AND co_sl='" + Session["CoSl"] + "'";
                mGlobal.binddropdownlist( sql, ddlUserName);
                ddlUserName.Items.Insert(0, new ListItem("--Select User Name--", "0"));

                sql = "SELECT   sl , title name  FROM menus WHERE ParentMenu_yn = 1 and active_yn=1 AND co_sl='" + Session["CoSl"] + "'";
                mGlobal.binddropdownlist( sql, ddlMenu);
                ddlMenu.Items.Insert(0, new ListItem("--Select Menus--", "0"));

            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("error while binding User Name and Menus!");
            }

            finally
            {

            }

        }

        protected void btnShow_Click(object sender, EventArgs e)

    {
            btnAdd.Visible = true;
            lblMsg.Text = "";
            try
            {
                if (ddlMenu.SelectedValue == "0")
                {
                    lblMsg.Text = "Select User Name!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (ddlMenu.SelectedValue=="0")
                {
                    lblMsg.Text = "Select Menu!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                gvMenu.Visible = true;
                sql = " SELECT menus1.sl, menus1.Title"
                    + " FROM  { oj(login login1 LEFT  JOIN permission permission1 ON login1.sl = permission1.user_sl AND permission1.user_sl = 1)  RIGHT OUTER  JOIN menus menus1 ON permission1.menus_sl = menus1.sl}"
                    + " WHERE menus1.ParentMenuId = '"+ ddlMenu.SelectedValue + "' AND menus1.co_sl = '" + Session["CoSl"] + "' AND menus1.active_yn = 1 ORDER BY menus1.sl";

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }
                mGlobal.conDatabase.Open();
                mGlobal.bindataGrid( gvMenu, sql);
                ddlMenu.Enabled = false;
                ddlUserName.Enabled = false;

                foreach (GridViewRow row in gvMenu.Rows)
                {
                   
                    string sl = row.Cells[0].Text;

                   

                    sql = "SELECT sl,view_yn, add_yn, edit_yn, delete_yn, print_yn FROM permission WHERE user_sl='" + ddlUserName.SelectedValue + "' AND menus_sl='" + sl + "' AND co_sl='" + Session["CoSl"] + "' ";

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
                                    int V_YN = Convert.ToInt32(myReader["view_yn"].ToString());
                                    int A_YN = Convert.ToInt32(myReader["add_yn"].ToString());
                                    int E_YN = Convert.ToInt32(myReader["edit_yn"].ToString());
                                    int D_YN = Convert.ToInt32(myReader["delete_yn"].ToString());
                                    int P_YN = Convert.ToInt32(myReader["print_yn"].ToString());

                                    CheckBox chkView = (CheckBox)row.FindControl("chkView");
                                    if (V_YN == 1)
                                    {

                                        chkView.Checked = true;
                                    }
                                    else
                                    {
                                        chkView.Checked = false;
                                    }

                                    CheckBox chkAdd = (CheckBox)row.FindControl("chkAdd");
                                    if (A_YN == 1)
                                    {

                                        chkAdd.Checked = true;
                                    }
                                    else
                                    {
                                        chkAdd.Checked = false;
                                    }

                                    CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
                                    if (E_YN == 1)
                                    {

                                        chkEdit.Checked = true;
                                    }
                                    else
                                    {
                                        chkEdit.Checked = false;
                                    }


                                    CheckBox chkDelete = (CheckBox)row.FindControl("chkDelete");
                                    if (D_YN == 1)
                                    {

                                        chkDelete.Checked = true;
                                    }
                                    else
                                    {
                                        chkDelete.Checked = false;
                                    }


                                    CheckBox chkPrint = (CheckBox)row.FindControl("chkPrint");
                                    if (P_YN == 1)
                                    {

                                        chkPrint.Checked = true;
                                    }
                                    else
                                    {
                                        chkPrint.Checked = false;
                                    }
                                }
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("error in display menu details!..");
            }
            finally
            {
                
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool IsAdded = false;
            string V_YN;
            string A_YN;
            string E_YN;
            string D_YN;
            string P_YN;
            foreach (GridViewRow row in gvMenu.Rows)
            {
                string sl = row.Cells[0].Text;

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();

                sql = "SELECT sl FROM permission WHERE user_sl='" + ddlUserName.SelectedValue + "' AND menus_sl='" + sl + "' AND co_sl='" + Session["CoSl"] + "' ";
                int permissionSL = Convert.ToInt32(mGlobal.getResult( sql));

                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                CheckBox chkAdd = (CheckBox)row.FindControl("chkAdd");
                CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
                CheckBox chkDelete = (CheckBox)row.FindControl("chkDelete");
                CheckBox chkPrint = (CheckBox)row.FindControl("chkPrint");


                if (chkView.Checked==true)
                {
                     V_YN = "1";
                }
                else
                {
                    V_YN = "0";
                }

                if (chkAdd.Checked == true)
                {
                    A_YN = "1";
                    
                }
                else
                {
                    A_YN = "0";
                }

                if (chkEdit.Checked == true)
                {
                    E_YN = "1";
                    
                }
                else
                {
                    E_YN = "0";
                }

                if (chkDelete.Checked == true)
                {
                    D_YN = "1";
                   
                }
                else
                {
                    D_YN = "0";
                }

                if (chkPrint.Checked == true)
                {
                    P_YN = "1";
                    
                }
                else
                {
                    P_YN = "0";
                }



                if (permissionSL > 0)
                {
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {

                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();


                    MySqlCommand cmd = new MySqlCommand();

                    cmd.CommandText = "UPDATE permission SET view_yn = '" + V_YN + "', add_yn = '" + A_YN + "', edit_yn = '" + E_YN + "', delete_yn = '" + D_YN + "', print_yn = '" + P_YN + "',"
                                    + " co_sl = '" + Session["CoSl"] + "',update_on = sysdate() ,update_by = '" + Session["LoginName"] + "' WHERE user_sl='" + ddlUserName.SelectedValue + "' and menus_sl='" + sl + "' and sl = '" + permissionSL + "'";

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;

                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    if (IsAdded)
                    {
                        sql = " INSERT INTO permission(menus_sl,user_sl,  view_yn, add_yn, edit_yn, delete_yn, print_yn, co_sl, insert_on, insert_by) "
                       + " SELECT DISTINCT parentmenuid, user_sl, 1 view_yn, 0 add_yn, 0 edit_yn, 0 delete_yn, 0 print_yn, '" + Session["CoSl"].ToString() + "', sysdate(), '" + Session["LogonName"] + "' "
                       + " FROM permission p, menus m WHERE m.sl = p.menus_sl AND parentmenuid > 0  AND user_sl = '" + ddlUserName.SelectedValue + "' "
                       + " AND parentmenuid NOT IN (SELECT DISTINCT menus_sl  FROM permission p, menus m  WHERE m.sl = p.menus_sl  AND user_sl = '" + ddlUserName.SelectedValue + "' ) ";

                        mGlobal.ExecuteQuery( sql);

                        sql = " INSERT INTO permission(menus_sl,user_sl,  view_yn, add_yn, edit_yn, delete_yn, print_yn, co_sl, insert_on, insert_by) "
                        + " SELECT sl, '" + ddlUserName.SelectedValue + "', 1 view_yn, 0 add_yn, 0 edit_yn, 0 delete_yn, 0 print_yn, '" + Session["CoSl"].ToString() + "', sysdate(), '" + Session["LogonName"] + "' "
                        + " FROM  menus   WHERE sl = 1 AND sl NOT IN ( SELECT DISTINCT menus_sl  FROM permission p, menus m  WHERE m.sl = p.menus_sl  AND user_sl = '" + ddlUserName.SelectedValue + "'  )   ";

                        mGlobal.ExecuteQuery( sql);
                        lblMsg.Text = "Updated successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMsg.Text = "Error while updating ";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {

                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();

                    MySqlCommand cmd = new MySqlCommand();

                    cmd.CommandText = "INSERT INTO permission(user_sl, menus_sl, view_yn, add_yn, edit_yn, delete_yn, print_yn, co_sl, insert_on, insert_by)"
                                     + "VALUES('" + ddlUserName.SelectedValue + "', '" + sl + "', '" + V_YN + "', '" + A_YN + "', '" + E_YN + "', '" + D_YN + "', '" + P_YN + "', '" + Session["CoSl"] + "', sysdate(), '" + Session["LoginName"] + "')";
                                   
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = mGlobal.conDatabase;

                    IsAdded = cmd.ExecuteNonQuery() > 0;

                    if (IsAdded)
                    {
                        sql = " INSERT INTO permission(menus_sl,user_sl,  view_yn, add_yn, edit_yn, delete_yn, print_yn, co_sl, insert_on, insert_by) "
                        + " SELECT DISTINCT parentmenuid, user_sl, 1 view_yn, 0 add_yn, 0 edit_yn, 0 delete_yn, 0 print_yn, '" + Session["CoSl"].ToString() + "', sysdate(), '" + Session["LogonName"] + "' "
                        + " FROM permission p, menus m WHERE m.sl = p.menus_sl AND parentmenuid > 0  AND user_sl = '" + ddlUserName.SelectedValue + "' "
                        + " AND parentmenuid NOT IN (SELECT DISTINCT menus_sl  FROM permission p, menus m  WHERE m.sl = p.menus_sl  AND user_sl = '" + ddlUserName.SelectedValue + "' ) ";

                        mGlobal.ExecuteQuery( sql);

                        sql = " INSERT INTO permission(menus_sl,user_sl,  view_yn, add_yn, edit_yn, delete_yn, print_yn, co_sl, insert_on, insert_by) "
                        + " SELECT sl, '" + ddlUserName.SelectedValue + "', 1 view_yn, 0 add_yn, 0 edit_yn, 0 delete_yn, 0 print_yn, '" + Session["CoSl"].ToString() + "', sysdate(), '" + Session["LogonName"] + "' "
                        + " FROM  menus   WHERE sl = 1 AND sl NOT IN ( SELECT DISTINCT menus_sl  FROM permission p, menus m  WHERE m.sl = p.menus_sl  AND user_sl = '" + ddlUserName.SelectedValue + "'  )   ";

                        mGlobal.ExecuteQuery( sql);

                        lblMsg.Text = "Added successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMsg.Text = "Error while Adding ";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
              
        }

        protected void chkView_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvMenu.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                CheckBox chkAdd = (CheckBox)row.FindControl("chkAdd");
                CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
                CheckBox chkDelete = (CheckBox)row.FindControl("chkDelete");
                CheckBox chkPrint = (CheckBox)row.FindControl("chkPrint");

                if(chkView.Checked==false)
                {
                    chkAdd.Checked = false;
                    chkEdit.Checked = false;
                    chkDelete.Checked = false;
                    chkPrint.Checked = false;
                }
            }

            }

        protected void chkAdd_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvMenu.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                CheckBox chkAdd = (CheckBox)row.FindControl("chkAdd");
                if (chkAdd.Checked == true)
                {
                    chkView.Checked = true;
                    
                }
            }
         }

        protected void chkEdit_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMenu.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
                if (chkEdit.Checked == true)
                {
                    chkView.Checked = true;

                }
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMenu.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                CheckBox chkDelete = (CheckBox)row.FindControl("chkDelete");
                if (chkDelete.Checked == true)
                {
                    chkView.Checked = true;

                }
            }
        }

        protected void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMenu.Rows)
            {
                CheckBox chkView = (CheckBox)row.FindControl("chkView");
                CheckBox chkPrint = (CheckBox)row.FindControl("chkPrint");
                if (chkPrint.Checked == true)
                {
                    chkView.Checked = true;

                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ddlMenu.Enabled = true;
            ddlUserName.Enabled = true;
            ddlMenu.ClearSelection();
            ddlUserName.ClearSelection();
            gvMenu.Visible = false;
            btnAdd.Visible = false;
        }
    }
        }
