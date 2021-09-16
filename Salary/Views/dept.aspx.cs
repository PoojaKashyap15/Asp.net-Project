using prototype.App_Code;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prototype.Views
{
    public partial class dept : System.Web.UI.Page
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

                    Bind();
                    bindddl();
                    
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
        private void bindddl()
        {
            sql = "SELECT csl_payid AS id, CONCAT('BPAY:',BPay,'  ','DA:', DA,'  ','HRA:', HRA,'  ','CCA:',CCA,'  ','EDU:', EDU,' ','BON:', Bonus) AS NAME " +
                   " FROM vu_payid WHERE csl_payid IN( " + Session["PayId"] + ") ORDER BY csl_payid";
            mGlobal.binddropdownlist(sql, ddlpayid);
            ddlpayid.Items.Insert(0, new ListItem("Select Salary Parameter", "0"));
        }
        private void Bind()
        {
            try
            {
                sql = "SELECT dpt_code,dpt_desc,dpt_hdr1,dpt_hdr2,if(active_yn = 1, 'P','D') AS STATUS FROM psys_dept where co_sl='" + Session ["CoSl"]  +"'     order by dpt_desc ";
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
                    sql = "SELECT d.dpt_code,d.dpt_desc,d.dpt_hdr1,d.dpt_hdr2,if (d.active_yn=1,'Active','Inactive') as status,CONCAT('BPAY:',v.BPay,'  ','DA:',v.DA,'  ','HRA:',v.HRA,'  ','CCA:',v.CCA,'  ','EDU:', v.EDU,' ','BON:', v.Bonus) AS dpt_payid from psys_dept d,vu_payid v where d.dpt_payid=v.csl_payid and d.dpt_code = " + index + " and  d.co_sl = " + Session["CoSl"] + " order by d.dpt_desc ";
                    mGlobal.bindDetailsView(dvLookup, sql);
                    gvView.Visible = false;
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

                    gvView.Visible = false;
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
                    sql = "SELECT dpt_code,dpt_desc,dpt_hdr1,dpt_hdr2,dpt_payid,active_yn from psys_dept where dpt_code = " + code.ToString() + " and  co_sl = " + Session["CoSl"] + " order by dpt_desc ";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);

                    myReader = msc.ExecuteReader();
                    while (myReader.Read())
                    {
                        lblsl.Text = myReader["dpt_code"].ToString();
                        lblsl.Visible = false;
                        //txtdptcode.Text = myReader["dpt_code"].ToString();
                        txtdesc.Text = myReader["dpt_desc"].ToString();
                        txthdr2.Text = myReader["dpt_hdr2"].ToString();
                        
                        txthdr1.Text = myReader["dpt_hdr1"].ToString();

                        string payid = myReader["dpt_payid"].ToString();
                        ddlpayid.SelectedValue = payid;
                        RadioButtonList1.SelectedValue = myReader["active_yn"].ToString();
                    }

                    myReader.Close();

                    btnAddRecord.Visible = false;
                    gvView.Visible = false;
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
                lblMsg.Text = "";
                Session["Mode"] = "Add";
                if (Session["Mode"].ToString() != "Add")
                {
                    lblMsg.Text = "";
                    txtSearch.Text = "";

                    //txtdptcode.Text = "";
                    txtdesc.Text = "";
                    txthdr1.Text = "";
                    txthdr2.Text = "";
                    ddlpayid.SelectedValue = "0".ToString();
                    
                    RadioButtonList1.SelectedValue = "1";

                }

                gvView.Visible = false;
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

                sql = "SELECT dpt_code,dpt_desc,dpt_hdr1,dpt_hdr2,if(active_yn = 1, 'P','D') AS STATUS from psys_dept where co_sl = '" + Session["CoSl"] + "' and (upper(dpt_desc) like '%" + txtSearch.Text.ToUpper() + "%' ) order by dpt_desc ";
                mGlobal.bindataGrid(gvView, sql);

                
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

                sql = "SELECT dpt_code as 'Department code' ,dpt_desc as 'Description',dpt_hdr1 as 'Header1',dpt_hdr2 as 'Header2',dpt_payid as 'PayId', if(active_yn=1,'Active','Inactive') as 'status' from psys_dept where co_sl = " + Session["CoSl"] + " order by dpt_desc asc ";
                VSF.ExportToExcel(mGlobal.conDatabase, "Department", sql);


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


                if (txtdesc.Text == "")
                {
                    lblMsg.Text = "Please enter Department";
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


                cmd.CommandText = "Insert into psys_dept(dpt_desc,dpt_hdr1,dpt_hdr2,dpt_payid,active_yn,co_sl, DPT_INSBY,DPT_INSON)" +
                   "  values('" + mGlobal.doQuotes(txtdesc.Text) + "','" + mGlobal.doQuotes(txthdr1.Text) + "','" + mGlobal.doQuotes(txthdr2.Text) + "', " +
                   "'"+ ddlpayid.SelectedValue +"','" + RadioButtonList1.SelectedValue + "','" + Session["CoSl"] + "','" + Session["LogonName"] + "',SYSDATE())";

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
               
                panelAddEdit.Visible = false;
                DivSearch.Visible = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;
            try
            {


                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "update psys_dept set  dpt_desc = '" + mGlobal.doQuotes(txtdesc.Text) + "' , " +
                        " dpt_hdr1 = '" + mGlobal.doQuotes(txthdr1.Text) + "', dpt_hdr2 = '" + mGlobal.doQuotes(txthdr2.Text) + "'," +                        
                          " dpt_payid='"+ ddlpayid.SelectedValue +"' , active_yn = '" + RadioButtonList1.SelectedValue + "', " +
                          "  dpt_UPDBY = '" + Session["LogonName"] + "',dpt_UPDON = sysdate()" +
                          " where dpt_code = '" + lblsl.Text + "' and  co_sl = '" + Session["CoSl"] + "'";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {
                    lblMsg.Text = " Record Updated successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                    //txtdptcode.Text = "";
                    txtdesc.Text = "";
                    txthdr1.Text = "";
                    txthdr2.Text = "";
                    ddlpayid.SelectedValue = "0".ToString();
                    txtSearch.Text = "";
                   
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

                cmd.CommandText = "delete from psys_dept where dpt_code NOT IN(SELECT DISTINCT(prs_dpcd) FROM pay_personnel ) AND dpt_code='" + code + "' and co_sl = " + Session["CoSl"] + " ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();
                IsDeleted = cmd.ExecuteNonQuery() > 0;

                if (IsDeleted)
                {
                    lblMsg.Text = "Record  has been deleted successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                   
                    panelDelete.Visible = false;
                }
                else
                {
                    lblMsg.Text = "Error while deleting Record, This Department Attached To Employee Details";
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
                this.DivSearch.Visible = true;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
           
            panelAddEdit.Visible = false;
            panelVIEW.Visible = false;
            panelError.Visible = true;
            panelDelete.Visible = false;
            this.DivSearch.Visible = true;
            lblMsg.Text = "";

            //txtdptcode.Text = "";
            txtdesc.Text = "";
            txthdr1.Text = "";
            txthdr2.Text = "";
            txtSearch.Text = "";
            ddlpayid.SelectedValue = "0".ToString();
           
            RadioButtonList1.SelectedValue = "1";

            Bind();
        }       
        protected void btncloseMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/home.aspx");
        }
    }
}