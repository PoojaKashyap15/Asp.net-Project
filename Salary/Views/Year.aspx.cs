using System;
using prototype.App_Code;
using System.Data;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;
using System.Web.UI;
//using CrystalDecisions.Shared;
//using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;

namespace prototype.Views
{
    public partial class Year : System.Web.UI.Page
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

                    mGlobal.qs_check(Session["LoginSl"].ToString(),Session["CoSl"].ToString(), Request.QueryString["qsc"].ToString().ToUpper(),
                       out viewyn, out addyn, out edityn, out deleteyn, out printyn, out qs_check, out typ, out menu_name);

                    if (qs_check == 0.ToString())
                    {
                        //Response.Redirect("../Views/login.aspx");
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


                    Session["Mode"] = "";
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

                sql = "SELECT sl,DATE_FORMAT(start_date,'%d-%b-%Y')start_date, DATE_FORMAT(end_date,'%d-%b-%Y')end_date, descs FROM yr WHERE co_sl = '" + Session["CoSl"] + "'";

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


                if (Session["Mode"].ToString() != "Add")
                {

                    txtDescs.Text = "";
                    txtEndDate.Text = "";
                    txtStartDate.Text = "";

                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script >");
                sb.Append("$('#addModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnAddd_Click(object sender, EventArgs e)
        {

            bool IsAdded = false;
            try
            {

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();


                //cmd.CommandText = "INSERT INTO bus2(bus_sl, lookup_pickup_sl, lookup_school_sl, pick_time, ordr, co_sl, insert_on, insert_by)"
                //                 + " VALUES('" + ddBus.SelectedValue + "', '" + ddPickUp.SelectedValue + "', '" + ddSchool.SelectedValue + "', '" + txtPickTime.Text + "', '" + txtOrder.Text + "', '" + Session["CoSl"] + "', sysdate(), '" + Session["LoginName"] + "')";

                cmd.CommandText = "INSERT INTO yr(start_date,end_date,descs,co_sl,insert_on,insert_by)"
                                + " VALUES('" + Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd") + "', '" + Convert.ToDateTime(txtEndDate.Text).ToString("yyyy-MM-dd") + "', '"+ txtDescs.Text + "', '" + Session["CoSl"] + "', sysdate(), '" + Session["LoginName"] + "')";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script >");
                    sb.Append("$('#addModal').modal('hide');");
                    sb.Append(@"</script>");
                    Session["Mode"] = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
                    lblMsg.Text = "Details added successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Error while adding details";
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
        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvView.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MySqlDataReader myReader;
            int index = 0;
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

                    sql = "SELECT sl, DATE_FORMAT(start_date,'%d-%b-%Y')start_date, DATE_FORMAT(end_date,'%d-%b-%Y')end_date, descs from yr where co_sl = '" + Session["CoSl"] + "' AND sl='" + code + "'";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {

                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlDataAdapter da = default(MySqlDataAdapter);

                    da = new MySqlDataAdapter(sql, mGlobal.conDatabase);

                    DataTable detailTable = new DataTable();
                    da.Fill(detailTable);
                    dvExam.DataSource = detailTable;
                    dvExam.DataBind();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script>");
                    sb.Append("$('#detailModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);

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

                    int code = Convert.ToInt32(e.CommandArgument);

                    
                    sql = "SELECT sl, start_date, end_date, descs from yr where co_sl = '" + Session["CoSl"] + "' AND sl='" + code + "'";

                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {

                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);

                    myReader = msc.ExecuteReader();
                    if (myReader.Read())
                    {
                        lblsl.Text = myReader["sl"].ToString();

                        txtStartDateEdit.Text = Convert.ToDateTime(myReader["start_date"]).ToString("dd-MMM-yyyy");
                        txtEndDateEdit.Text = Convert.ToDateTime(myReader["end_date"]).ToString("dd-MMM-yyyy");

                        txtDescsEdit.Text = myReader["descs"].ToString();

                    }

                    myReader.Close();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script>");
                    sb.Append("$('#editModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);


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

                    int code = Convert.ToInt32(e.CommandArgument);
                    hfSl.Value = code.ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script>");
                    sb.Append("$('#deleteModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);


                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            bool IsAdded = false;

            try
            {


                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "UPDATE yr SET start_date = '" + Convert.ToDateTime(txtStartDateEdit.Text).ToString("yyyy-MM-dd") + "',end_date = '" + Convert.ToDateTime(txtEndDateEdit.Text).ToString("yyyy-MM-dd") + "',descs = '" + txtDescsEdit.Text + "', "
                                   + " co_sl = '" + Session["CoSl"] + "',update_on = sysdate(),update_by = '" + Session["LoginName"] + "' WHERE sl = '" + lblsl.Text + "'";

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
                    lblMsg.Text = "Updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Error while updating ";
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {

                bool IsDeleted = false;
                //getting key value, row id
                string code = hfSl.Value;
                //getting row field details

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "delete from yr where sl='" + code + "' ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();
                IsDeleted = cmd.ExecuteNonQuery() > 0;

                if (IsDeleted)
                {
                    lblMsg.Text = "Details has been deleted successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#deleteModal').modal('hide');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);


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
      
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PrintYN"].ToString() == "0")
                {
                    ShowMsgBox.Show("Sorry..you don't have PRINT permission!...Please contact system admin");
                    return;
                }


                sql = "SELECT sl, DATE_FORMAT(start_date,'%d-%b-%Y')start_date, DATE_FORMAT(end_date,'%d-%b-%Y')end_date, descs from yr where co_sl = '" + Session["CoSl"] + "'";

                VSF.ExportToExcel(mGlobal.conDatabase, "Year", sql);


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

        protected void btvClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/home.aspx");
        }
    }

}
