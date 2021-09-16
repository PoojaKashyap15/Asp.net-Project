using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using prototype.App_Code;

namespace Payroll.Views
{
    public partial class Company : System.Web.UI.Page
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

                    panleGridView.Visible = true;
                    panelAddEdit.Visible = false;
                    panelDelete.Visible = false;
                    panelVIEW.Visible = false;
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
                sql = " select code,name,short_name,address,address2,city,pin,contact_no,email from co where  sl ='" + Session["CoSl"] + "'";
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();
                MySqlDataAdapter dAdapter = default(MySqlDataAdapter);

                DataSet ds = new DataSet();
                dAdapter = new MySqlDataAdapter(sql, mGlobal.conDatabase);

                dAdapter.Fill(ds);
                dt = ds.Tables[0];
                //Bind the fetched data to gridview

                gvView.DataSource = dt;
                gvView.DataBind();

                
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

        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MySqlDataReader myReader = null;
            int index = 0;
            lblMsg.Text = "";

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
                    index = Convert.ToInt32(e.CommandArgument);
                    int code = index;
                    sql = " SELECT sl,code,name,short_name,address,address2,city,pin,contact_no,email,website,profile,active_yn,if(active_yn=1,'Active','Inactive') as status from co WHERE code='" + index+ "' order by code";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }
                    mGlobal.conDatabase.Open();
                    MySqlDataAdapter da = default(MySqlDataAdapter);

                    da = new MySqlDataAdapter(sql, mGlobal.conDatabase);

                    DataTable detailTable = new DataTable();
                    da.Fill(detailTable);
                    dvLookup.DataSource = detailTable;
                    dvLookup.DataBind();

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
                    mGlobal.conDatabase.Close();
                }

            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                try
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

                    index = Convert.ToInt32(e.CommandArgument);
                    int code = index;
                    hfSl.Value = code.ToString();

                    panleGridView.Visible = false;
                    panelAddEdit.Visible = false;
                    panelVIEW.Visible = false;
                    panelDelete.Visible = true;
                    this.DivSearch.Visible = false;

                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
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
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb2.ToString(), false);
                        return;
                    }
                    index = Convert.ToInt32(e.CommandArgument);
                    int code = index;
                    sql = " select sl,code,name,short_name,address,address2,city,pin,contact_no,email,website,profile,active_yn from co where code = '" + code.ToString() + "' order by code ";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);

                    myReader = msc.ExecuteReader();
                    while (myReader.Read())
                    {
                        //txtsl.Text = myReader["sl"].ToString();
                        //txtsl.Visible = false;
                        txtcode.Text = myReader["code"].ToString();
                        txtname.Text = myReader["name"].ToString();
                        txtshname.Text = myReader["short_name"].ToString();
                        txtadr1.Text = myReader["address"].ToString();
                        txtadr2.Text = myReader["address2"].ToString();
                        txtcity.Text = myReader["city"].ToString();
                        txtpin.Text = myReader["pin"].ToString();
                        txtphone.Text = myReader["contact_no"].ToString();
                        txtmail.Text = myReader["email"].ToString();
                        txtwebsite.Text = myReader["website"].ToString();
                        //txtlogo.Text = myReader["logo"].ToString();
                        txtprofile.Text = myReader["profile"].ToString();
                        
                        //rbtnActive.SelectedValue = myReader["active_yn"].ToString();
                    }

                    myReader.Close();

                    btnAddRecord.Visible = false;
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
                    mGlobal.conDatabase.Close();
                    if (myReader != null)
                    {
                        myReader.Close();
                    }
                }

            }


        }

        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvView.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
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
                Session["Mode"] = "Add";
                if (Session["Mode"].ToString() != "Add")
                {
                    lblMsg.Text = "";
                    txtSearch.Text = "";
                    //txtid.Text = "";
                    txtcode.Text = "";
                    txtname.Text ="";
                    txtshname.Text = "";
                    txtadr1.Text = "";
                    txtadr2.Text = "";
                    txtcity.Text = "";
                    txtpin.Text = "";
                    txtphone.Text = "";
                    txtmail.Text = "";
                    txtwebsite.Text = "";
                    //txtlogo.Text = "";
                    txtprofile.Text = "";
                    
                    rbtnActive.SelectedValue = "1";

                }

                panleGridView.Visible = false;
                panelAddEdit.Visible = true;
                btnAddRecord.Visible = true;
                btnUpdate.Visible = false;
                panelDelete.Visible = false;
                panelVIEW.Visible = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                if (txtSearch.Text == "")
                {
                    Bind();
                    return;
                }

                sql = " select sl,code,name,short_name,address,address2,city,pin,contact_no,email,website,profile,active_yn from co where (upper(name) like '%"+txtSearch.Text.ToUpper()+"%')";
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                mGlobal.conDatabase.Open();
                MySqlDataAdapter dAdapter = default(MySqlDataAdapter);

                DataSet ds = new DataSet();
                dAdapter = new MySqlDataAdapter(sql, mGlobal.conDatabase);

                dAdapter.Fill(ds);
                dt = ds.Tables[0];

                gvView.DataSource = dt;
                gvView.DataBind();

                panleGridView.Visible = true;
                panelAddEdit.Visible = false;
                                
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

                sql = " select  sl,code,name,short_name,address,address2,city,pin,contact_no,email,website,profile,active_yn from co";
                VSF.ExportToExcel(mGlobal.conDatabase, Session["TYP"].ToString().ToUpper(), sql);


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

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            bool IsAdded = false;
            try
            {
                int active_yn = 0;
                Session["Mode"] = "Add";


                if (txtname.Text == "")
                {
                    lblMsg.Text = "Please enter code";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                //-----------------checking duplicate added by patel on 6-june-2016 ---------------------------------//

                //--------------------------------------- End --------------------------------------------------------//



                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }


                MySqlCommand cmd = new MySqlCommand();


                cmd.CommandText = "Insert into co(code,name,short_name,address,address2,city,pin,contact_no,email,website,profile,active_yn,insert_on,insert_by) " +
                    " values('"+ mGlobal.doQuotes(txtcode.Text) +"','" + mGlobal.doQuotes(txtname.Text) + "','"+ mGlobal.doQuotes(txtshname.Text) +"'," +
                    "'" + mGlobal.doQuotes(txtadr1.Text) + "'," + "'" + mGlobal.doQuotes(txtadr2.Text) + "'," + "'" + mGlobal.doQuotes(txtcity.Text) + "'," + "'" + mGlobal.doQuotes(txtpin.Text) + "'," + "'" + mGlobal.doQuotes(txtphone.Text) + "'," + "'" + mGlobal.doQuotes(txtmail.Text) + "'," + 
                    "'" + mGlobal.doQuotes(txtwebsite.Text) + "'," + "'" + mGlobal.doQuotes(txtprofile.Text) + "'," + rbtnActive.SelectedValue + ",sysdate(),'"+ Session["LoginName"] +"')";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {

                    lblMsg.Text = "Record Saved successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                    Bind();
                }
                else
                {
                    lblMsg.Text = "Error while adding  record";
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
                DivSearch.Visible = true;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;
            int active_yn = 0;
            try
            {
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update co set  code='"+ mGlobal.doQuotes(txtcode.Text) +"',name = '" + mGlobal.doQuotes(txtname.Text) + "',short_name='"+ mGlobal.doQuotes(txtshname.Text) + "',  address = '" + mGlobal.doQuotes(txtadr1.Text) + "',address2='" + mGlobal.doQuotes(txtadr2.Text) + "',city='" + mGlobal.doQuotes(txtcity.Text) + "',pin='" + mGlobal.doQuotes(txtpin.Text) + "',contact_no='" + mGlobal.doQuotes(txtphone.Text) + "',email='" + mGlobal.doQuotes(txtmail.Text) + "',website='" + mGlobal.doQuotes(txtwebsite.Text) + "',profile='" + mGlobal.doQuotes(txtprofile.Text) + "',active_yn='" + rbtnActive.SelectedValue + "',update_on=sysdate(),update_by='"+ Session["LoginName"] +"' where code = '" + txtcode.Text + "'";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script >");
                    sb.Append("$('#editModal').modal('hide');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);

                    lblMsg.Text = " Record Updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                }
                else
                {
                    lblMsg.Text = "Error while updating details";
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
                string code = hfSl.Value;

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "delete from co  where sl='" + code + "'";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();
                IsDeleted = cmd.ExecuteNonQuery() > 0;

                if (IsDeleted)
                {
                    lblMsg.Text = "Record  has been deleted successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    panleGridView.Visible = true;
                    panelDelete.Visible = false;
                }
                else
                {
                    lblMsg.Text = "Error while deleting Record";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            } //try

            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                Bind();
                mGlobal.conDatabase.Close();
                this.DivSearch.Visible = true;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            panleGridView.Visible = true;
            panelAddEdit.Visible = false;
            panelVIEW.Visible = false;
            panelError.Visible = true;
            panelDelete.Visible = false;
            this.DivSearch.Visible = true;
            lblMsg.Text = "";

            txtcode.Text = "";
            txtname.Text = "";
            txtshname.Text = "";
            txtadr1.Text = "";
            txtadr2.Text = "";
            txtcity.Text = "";
            txtpin.Text = "";
            txtphone.Text = "";
            txtmail.Text = "";
            txtwebsite.Text = "";
            //txtlogo.Text = "";
            txtprofile.Text = "";
            
            rbtnActive.SelectedValue = "1";


            Bind();
        }


        protected void btncloseMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/home.aspx");
        }

        protected void dvLookup_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }
    }
}