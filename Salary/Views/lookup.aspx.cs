using System;
using prototype.App_Code;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
 
namespace Military.Views
{
    public partial class lookup : System.Web.UI.Page
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
                sql = " select sl,code,name,descs from lookup where co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' order by code ";
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

            if (Session["Mode"].ToString() != "Add")
            {
                lblMsg.Text = "";
                lblsl.Text = "";
                txtCode.Text = "";
                txtDescriptions.Text = "";
                txtName.Text = "";
                rbtnActive.SelectedValue = "1";
            }
            sql = "SELECT cast(lpad((ifnull(MAX(code),0)+1),6,'0') as char) FROM lookup where co_sl='" + Session["CoSl"] + "' and typ = '" + Session["TYP"].ToString().ToUpper() + "'";
            txtCode.Text = mGlobal.getResult( sql);

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
            txtName.Text = "";
            txtCode.Text = "";
            txtDescriptions.Text = "";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            bool IsAdded = false;
            try
            {
                lblMsg.Text = "";

                Session["Mode"] = "Add";
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();

                MySqlCommand cmd = new MySqlCommand();

                sql = "SELECT cast(lpad((ifnull(MAX(code),0)+1),6,'0') as char) FROM lookup where co_sl='" + Session["CoSl"] + "' and typ = '" + Session["TYP"].ToString().ToUpper() + "'";
                txtCode.Text = mGlobal.getResult( sql);

                //sql = "SELECT ifnull(MAX(sl),0)+1 FROM lookup where co_sl='" + Session["CoSl"] + "'";
                //string lookup_sl = mGlobal.getResult( sql);

                cmd.CommandText = "Insert into lookup(code,name,descs,active_yn,co_sl,typ, insert_on,  insert_by)    " +
                             " values('" + txtCode.Text + "','" + mGlobal.doQuotes(txtName.Text) + "'," +
                         "'" + mGlobal.doQuotes(txtDescriptions.Text) + "'," + rbtnActive.SelectedValue + ",'" + Session["CoSl"] + "','" + Session["TYP"].ToString().ToUpper() + "',sysdate(),'" + Session["LoginName"] + "')";

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
            MySqlDataReader myReader;

            try
            {
                lblMsg.Text = "";

                sql = " select sl,code,name,descs, active_yn from lookup where sl = " + code.ToString() + " and  co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' order by name ";
                using (MySqlConnection connectionS = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    connectionS.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connectionS))
                    {
                        using (MySqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                lblsl.Text = dr["sl"].ToString();
                                txtName.Text = dr["name"].ToString();
                                txtCode.Text = dr["code"].ToString();
                                txtDescriptions.Text = dr["descs"].ToString();
                                rbtnActive.SelectedValue = dr["active_yn"].ToString();
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


                sql = " select sl,code,name,descs,if(active_yn=1,'Active','Inactive') as status from lookup where sl = " + code + " and  co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' order by name ";
                mGlobal.bindDetailsView(dvView, sql);
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            bool IsAdded = false;

            try
            {
                lblMsg.Text = "";

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();

                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandText = "update lookup set " +
                                 " name = '" + mGlobal.doQuotes(txtName.Text) + "',code= '" + txtCode.Text + "',  descs = '" + mGlobal.doQuotes(txtDescriptions.Text) + "' , " +
                                 " active_yn = '" + rbtnActive.SelectedValue + "',  update_on = sysdate(), " +
                                 " update_by = '" + Session["LogonName"] + "' where sl = '" + lblsl.Text + "' and upper(typ)='" + Session["TYP"].ToString().ToUpper() + "' and co_sl = '" + Session["CoSl"] + "'";


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

                cmd.CommandText = "delete from lookup  where sl='" + code + "' and co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "'";
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
                sql = " select sl,name,code,descs from lookup where co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' " +
                            " and (upper(name) like '%" + txtSearch.Text.ToUpper() + "%' or upper(code) like '%" + txtSearch.Text.ToUpper() + "%') order by code ";
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

                //sql = " select code as Code,  name as Name,descs as Description ,if(active_yn=1,'Active','Inactive') as status from  lookup   where co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' order by code asc ";

                sql = " select CAST(LPAD((CODE),6,'0') AS CHAR) as Code,  name as Name,descs as Description ,if(active_yn=1,'Active','Inactive') as status from  lookup   where co_sl = " + Session["CoSl"] + " and upper(typ) = '" + Session["TYP"].ToString().ToUpper() + "' order by code asc ";

                VSF.ExportToExcel(mGlobal.conDatabase,lblHeading.Text, sql);


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