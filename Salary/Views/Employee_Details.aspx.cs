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

namespace Salary.Views
{
    public partial class Employee_Details : System.Web.UI.Page
    {
        string sql;
        DataTable dt;
        private string query;
        private float logo;

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
                    binddll();
                    
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
               

        private void binddll()
        {
            sql = "SELECT dsg_code id,dsg_desc NAME FROM psys_desg WHERE co_sl = " + Session["CoSl"] + " and active_yn='" + 1 + "' ORDER BY dsg_desc";
            mGlobal.binddropdownlist(sql, ddldesg);
            ddldesg.Items.Insert(0, new ListItem("--Select Designation--", "0"));

            sql = "SELECT dpt_code id,dpt_desc NAME FROM psys_dept  WHERE co_sl = " + Session["CoSl"] + " and active_yn='" + 1 + "' ORDER BY dpt_desc";
            mGlobal.binddropdownlist(sql, ddldepcode);
            ddldepcode.Items.Insert(0, new ListItem("--Select Department--", "0"));

            sql = "SELECT LCN_CODE id,LCN_DESC NAME FROM psys_locn  WHERE co_sl = " + Session["CoSl"] + " and active_yn='" + 1 + "' ORDER BY LCN_DESC";
            mGlobal.binddropdownlist(sql, ddlLocation);
            ddlLocation.Items.Insert(0, new ListItem("--Select Location--", "0"));

            sql = "SELECT bnk_code id,bnk_name NAME FROM psys_bank  WHERE co_sl = " + Session["CoSl"] + " and active_yn='" + 1 + "' ORDER BY bnk_name";
            mGlobal.binddropdownlist(sql, ddlbank);
            ddlbank.Items.Insert(0, new ListItem("--Select Bank--","0"));

            sql = "SELECT sl,state_name as name  FROM psys_state  WHERE     active_yn='" + 1 + "' ORDER BY state_name";
            mGlobal.binddropdownlist(sql, ddlState);
            ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));

            sql = "SELECT csl_payid AS id, CONCAT('BPAY:',BPay,'  ','DA:', DA,'  ','HRA:', HRA,'  ','CCA:',CCA,'  ','EDU:', EDU,' ','BON:', Bonus) AS NAME " +
                   " FROM vu_payid WHERE csl_payid IN( " + Session["PayId"] + ") ORDER BY csl_payid";
            mGlobal.binddropdownlist(sql, ddlpayid);
           ddlpayid.Items.Insert(0, new ListItem("Select Salary Parameter", "0"));
        }
        private void Bind()
        {
            try
            {
                sql = "SELECT PRS_SL,PRS_EMNO,PRS_NAME,PRS_MOBILE,PRS_PANN,DSG_DESC,DPT_DESC,if(e.active_yn = 1, 'P','D') AS STATUS " + 
                    " FROM pay_personnel e,psys_desg d,psys_dept dp " +
                    " where  PRS_DECD = DSG_CODE and PRS_DPCD = DPT_CODE and e.co_sl = " + Session["CoSl"] + " and e.active_yn='"+ 1 +"'  order by prs_name ";
                mGlobal.bindataGrid(gvView, sql);
            }
            //if (p.active_yn = 1,'Active','Inactive') as status
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                 
            }

        }

        protected void decd_SelectedIndexChanged(object sender, EventArgs e)
        {


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
                    sql = "SELECT PRS_EMNO,PRS_PANN,PRS_NAME,PRS_ADDRESS,PRS_GENDER,DATE_FORMAT(PRS_DTOB,'%d-%b-%Y')PRS_DTOB,DATE_FORMAT(PRS_DTOJ,'%d-%b-%Y')PRS_DTOJ,DATE_FORMAT(PRS_DTOC,'%d-%b-%Y')PRS_DTOC,DATE_FORMAT(PRS_DTOR,'%d-%b-%Y')PRS_DTOR,PRS_MOBILE, " +
                     " PRS_EMAIL,PRS_RESPH,PRS_QLFT,PRS_DPCD, BNK_NAME,LCN_DESC,DSG_DESC,DPT_DESC,PRS_DECD,PRS_LOCD,PRS_BKCD,PRS_BKAC,adhar_no,CONCAT('BPAY:',v.BPay,'  ','DA:',v.DA,'  ','HRA:',v.HRA,'  ','CCA:',v.CCA,'  ','EDU:', v.EDU,' ','BON:', v.Bonus) AS pay_id,PRS_CLIENTREFNO,PRS_PFNO,PRS_ESINO,prs_basic, " +
                     " PRS_GLSINO, if (p.prs_pt_yn=1,'Yes','No') as prs_pt_yn,  if (p.prs_pf_yn=1,'Yes','No') prs_pf_yn, " +
                     " if (p.active_yn=1,'Active','Inactive') as status, state_name,if (p.prs_esi_yn=1,'Yes','No') as esi,p.active_yn from pay_personnel p,psys_dept d, " +
                    " psys_desg ds,psys_locn l,psys_bank b,psys_state s,vu_payid v where PRS_DPCD = DPT_CODE and p.pay_id=v.csl_payid and p.co_sl = d.co_sl and " +
                    " PRS_DECD = DSG_CODE and p.co_sl = ds.co_Sl and  PRS_LOCD = LCN_CODE  and p.co_Sl = l.co_sl and " +
                    " PRS_BKCD = BNK_CODE and p.co_sl = b.co_sl and PRS_EMNO= " + code.ToString() + " and  p.co_sl = " + Session["CoSl"] + " ORDER BY prs_name  ";

                    mGlobal.bindDetailsView(dvLookup, sql);
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);
                    
                    myReader = msc.ExecuteReader();
                    while (myReader.Read())
                    {
                        EmpPhoto.ImageUrl = "~/Attachment/" + Session["CoSl"] + "/" + myReader["prs_emno"] + ".jpg";
                    }
                    myReader.Close();
                    //<%# "~/Attachment/DefaultPhoto/photo.jpg"+ Eval("prs_sl") %>
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
                    sql = "SELECT PRS_EMNO,PRS_PANN,PRS_NAME,PRS_ADDRESS,PRS_GENDER,DATE_FORMAT(PRS_DTOB,'%d-%b-%Y')PRS_DTOB,DATE_FORMAT(PRS_DTOJ,'%d-%b-%Y')PRS_DTOJ,DATE_FORMAT(PRS_DTOC,'%d-%b-%Y')PRS_DTOC,DATE_FORMAT(PRS_DTOR,'%d-%b-%Y')PRS_DTOR,PRS_MOBILE, " +
                        " PRS_EMAIL,PRS_RESPH,PRS_QLFT,PRS_DPCD, BNK_NAME,LCN_DESC,DSG_DESC,DPT_DESC,PRS_DECD,PRS_LOCD,PRS_BKCD,PRS_BKAC,adhar_no,pay_id,PRS_CLIENTREFNO,PRS_PFNO,PRS_ESINO,prs_basic,PRS_GLSINO,prs_pt_yn,prs_pf_yn, " +
                        " if (p.active_yn=1,'Active','Inactive') as status,PRS_STATE_SL,p.active_yn,prs_esi_yn from pay_personnel p,psys_dept d, " +
                       " psys_desg ds,psys_locn l,psys_bank b where PRS_DPCD = DPT_CODE and p.co_sl = d.co_sl and "+
                       " PRS_DECD = DSG_CODE and p.co_sl = ds.co_Sl and PRS_LOCD = LCN_CODE  and p.co_Sl = l.co_sl and "+
                       " PRS_BKCD = BNK_CODE and p.co_sl = b.co_sl and PRS_EMNO= " + code.ToString() + " and  p.co_sl = " + Session["CoSl"] + " order by PRS_NAME ";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);


                    myReader = msc.ExecuteReader();
                    while (myReader.Read())
                    {

                        txtempno.Text = myReader["PRS_EMNO"].ToString();
                        txtname.Text = myReader["PRS_NAME"].ToString();
                        txtpanNo.Text = myReader["PRS_PANN"].ToString();
                        txtadd.Text = myReader["PRS_ADDRESS"].ToString();
                        rdbtngender.SelectedValue = myReader["PRS_GENDER"].ToString().ToUpper();
                        txtdob.Text = myReader["PRS_DTOB"].ToString();
                        txtdoj.Text = myReader["PRS_DTOJ"].ToString();
                        txtdop.Text = myReader["PRS_DTOC"].ToString();
                        txtdoreg.Text = myReader["PRS_DTOR"].ToString();
                        txtcntno.Text = myReader["PRS_MOBILE"].ToString();
                        txtemail.Text = myReader["PRS_EMAIL"].ToString();
                        txtredno.Text = myReader["PRS_RESPH"].ToString();
                        txtquliftn.Text = myReader["PRS_QLFT"].ToString();

                        string dptcode = myReader["PRS_DPCD"].ToString();
                        ddldepcode.SelectedValue = dptcode;

                        string desgcode = myReader["PRS_DECD"].ToString();
                        ddldesg.SelectedValue = desgcode;

                        ddlState.SelectedValue = myReader["PRS_STATE_SL"].ToString();

                        string lcncode = myReader["PRS_LOCD"].ToString();
                        ddlLocation.SelectedValue = lcncode;

                        string bnkcode = myReader["PRS_BKCD"].ToString();
                        ddlbank.SelectedValue = bnkcode;

                        txtalvno.Text = myReader["PRS_BKAC"].ToString();
                        txtadhrno.Text = myReader["adhar_no"].ToString();

                        string payid = myReader["pay_id"].ToString();
                        ddlpayid.SelectedValue = payid;

                        txtclntrefno.Text = myReader["PRS_CLIENTREFNO"].ToString();
                        txtpfno.Text = myReader["PRS_PFNO"].ToString();
                        txtesino.Text = myReader["PRS_ESINO"].ToString();
                        txtbasicpay.Text = myReader["prs_basic"].ToString();
                        txtgslino.Text = myReader["PRS_GLSINO"].ToString();

                        rdbtnpt.SelectedValue = myReader["prs_pt_yn"].ToString();
                        rdbtnpf.SelectedValue = myReader["prs_pf_yn"].ToString();
                        rblESI.SelectedValue = myReader["prs_esi_yn"].ToString(); 
                        rblStatus.SelectedValue = myReader["ACTIVE_YN"].ToString();
                        EmployeePhoto.ImageUrl = "~/Attachment/" + Session["CoSl"] + "/" + myReader["prs_emno"] + ".jpg";
                       
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
            //dropdownlist();
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

               if (Session["Mode"].ToString() != "Add")
                {
                    lblMsg.Text = "";
                    txtSearch.Text = "";
                    txtempno.Text = "";
                    txtpanNo.Text = "";
                    txtname.Text = "";
                    txtadd.Text = "";
                    rdbtngender.SelectedValue = "M";
                    txtdob.Text = "";
                    txtdoj.Text = "";
                    txtdop.Text = "";
                    txtdoreg.Text = "";
                    txtredno.Text = "";
                    txtcntno.Text = "";
                    txtemail.Text = "";                    
                    txtquliftn.Text = "";
                    ddldepcode.SelectedValue = "0".ToString();
                    ddldesg.SelectedValue = "0".ToString();
                    ddlLocation.SelectedValue = "0".ToString();
                    ddlbank.SelectedValue = "0".ToString();
                    ddlState.SelectedValue = "0".ToString();
                    txtalvno.Text = "";
                    txtadhrno.Text = "";
                    ddlpayid.SelectedValue = "0".ToString();
                    txtclntrefno.Text = "";
                    txtpfno.Text = "";
                    txtesino.Text = "";
                    txtbasicpay.Text = "";
                    txtgslino.Text = "";
                    rdbtnpt.SelectedValue = "0";
                    rdbtnpf.SelectedValue = "0";
                    rblStatus.SelectedValue = "1";
                    rblESI.SelectedValue = "0";
                   
                }
                
                panelAddEdit.Visible = true;
                btnAddRecord.Visible = true;
                btnUpdate.Visible = false;
                panelDelete.Visible = false;
                panelVIEW.Visible = false;
                this.DivSearch.Visible = false;
                gvView.Visible = false;
                //BindInstitute();
                //bindrdbtn();
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

                sql = "SELECT PRS_SL,PRS_EMNO,PRS_NAME,PRS_MOBILE,PRS_PANN,DSG_DESC,DPT_DESC,if(e.active_yn = 1, 'P','D') AS STATUS FROM pay_personnel e,psys_desg d,psys_dept dp " +
                  " where  PRS_DECD = DSG_CODE and PRS_DPCD = DPT_CODE and e.co_sl = " + Session["CoSl"] + "  " +
                  " and (upper(PRS_NAME) like '%" + txtSearch.Text.ToUpper() + "%'  or upper(PRS_emno) like '%" + txtSearch.Text.ToUpper() + "%')   order by e.ACTIVE_YN desc,prs_Emno asc ";
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

                sql = "SELECT PRS_EMNO,PRS_PANN,PRS_NAME,PRS_ADDRESS,PRS_GENDER,DATE_FORMAT(PRS_DTOB,'%d-%b-%Y')PRS_DTOB,DATE_FORMAT(PRS_DTOJ,'%d-%b-%Y')PRS_DTOJ,DATE_FORMAT(PRS_DTOC,'%d-%b-%Y')PRS_DTOC,DATE_FORMAT(PRS_DTOR,'%d-%b-%Y')PRS_DTOR,PRS_MOBILE, " +
                   " PRS_EMAIL,PRS_RESPH,PRS_QLFT,PRS_DPCD, BNK_NAME,LCN_DESC,DSG_DESC,DPT_DESC,PRS_DECD,PRS_LOCD,PRS_BKCD,PRS_BKAC,adhar_no,pay_id,PRS_CLIENTREFNO,PRS_PFNO,PRS_ESINO,prs_basic,PRS_GLSINO,prs_pt_yn,prs_pf_yn, " +
                   " if (p.active_yn=1,'Active','Inactive') as status,p.active_yn,prs_esi_yn from pay_personnel p,psys_dept d, " +
                  " psys_desg ds,psys_locn l,psys_bank b where PRS_DPCD = DPT_CODE and p.co_sl = d.co_sl and " +
                  " PRS_DECD = DSG_CODE and p.co_sl = ds.co_Sl and PRS_LOCD = LCN_CODE  and p.co_Sl = l.co_sl and " +
                  " PRS_BKCD = BNK_CODE and p.co_sl = b.co_sl and  p.co_sl = " + Session["CoSl"] + " order by PRS_NAME ";

                VSF.ExportToExcel(mGlobal.conDatabase, " PAY Personal Details", sql);


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


                if (txtempno.Text == "")
                {
                    lblMsg.Text = "Please enter Employee_no";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (rdbtngender.SelectedValue == "")
                {
                    ShowMsgBox.Show("Please select Gender");
                    return;
                }

                if (rblStatus.SelectedValue == "")
                {
                    ShowMsgBox.Show("Please select Status");
                    return;
                }




                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }
                MySqlCommand cmd = new MySqlCommand();

               cmd.CommandText = "Insert into pay_personnel(PRS_EMNO,PRS_PANN,PRS_NAME,PRS_ADDRESS,PRS_GENDER,"
                    + "PRS_DTOB,PRS_DTOJ,PRS_DTOC,PRS_DTOR,PRS_MOBILE, PRS_EMAIL,PRS_RESPH,PRS_QLFT,PRS_DPCD,PRS_DECD,"
                    + "PRS_LOCD,PRS_BKCD,PRS_BKAC, adhar_no,pay_id,PRS_CLIENTREFNO, PRS_PFNO, PRS_ESINO,prs_basic,"
                    + "PRS_GLSINO,prs_pt_yn,prs_pf_yn,PRS_INSBY,PRS_INSON,active_yn,co_sl,prs_esi_yn,PRS_STATE_SL)"
                    + " values('" + mGlobal.doQuotes(txtempno.Text) + "','"+ mGlobal.doQuotes(txtpanNo.Text) +"','" + mGlobal.doQuotes(txtname.Text) + "','" + mGlobal.doQuotes(txtadd.Text) + "','"+ rdbtngender.SelectedValue +"','" + mGlobal.doQuotes(Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd")) + "',"
                    + "'"+ mGlobal.doQuotes(Convert.ToDateTime(txtdoj.Text).ToString("yyyy-MM-dd")) +"','"+ mGlobal.doQuotes(Convert.ToDateTime(txtdop.Text).ToString("yyyy-MM-dd")) +"','" + mGlobal.doQuotes(Convert.ToDateTime(txtdoreg.Text).ToString("yyyy-MM-dd")) + "','" + mGlobal.doQuotes(txtcntno.Text) + "','" + mGlobal.doQuotes(txtemail.Text) + "','" + mGlobal.doQuotes(txtredno.Text) + "','" + mGlobal.doQuotes(txtquliftn.Text) + "',"
                    +"'" + ddldepcode.SelectedValue + "','" + ddldesg.SelectedValue + "','" + ddlLocation.SelectedValue + "',"
                    + "'" + ddlbank.SelectedValue + "','" + mGlobal.doQuotes(txtalvno.Text) + "','" + mGlobal.doQuotes(txtadhrno.Text) + "','"+ ddlpayid.SelectedValue +"','"+ mGlobal.doQuotes(txtclntrefno.Text) +"','" + mGlobal.doQuotes(txtpfno.Text) + "','" + mGlobal.doQuotes(txtesino.Text) + "','" + mGlobal.doQuotes(txtbasicpay.Text) + "',"
                    + "'" + mGlobal.doQuotes(txtgslino.Text) + "','"+ rdbtnpt.SelectedValue +"','" + rdbtnpf.SelectedValue + "','" + Session["LoginName"] + "', sysdate(),"
                    + "'" + rblStatus.SelectedValue + "','" + Session["CoSl"] + "',"+ rblESI.SelectedValue +" , " + ddlState.SelectedValue + ")";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {

                    lblMsg.Text = "Record Saved successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                    txtempno.Text = "";
                    txtpanNo.Text = "";
                    txtname.Text = "";
                    txtadd.Text = "";
                    rdbtngender.SelectedValue = "";
                    txtdob.Text = "";
                    txtdoj.Text = "";
                    txtdop.Text = "";
                    txtdoreg.Text = "";
                    txtcntno.Text = "";
                    txtemail.Text = "";
                    txtredno.Text = "";
                    txtquliftn.Text = "";
                    ddldepcode.SelectedValue = "0".ToString();
                    ddldesg.SelectedValue = "0".ToString();
                    ddlLocation.SelectedValue = "0".ToString();
                    ddlbank.SelectedValue = "0".ToString();
                    ddlState.SelectedValue = "0".ToString();
                    txtalvno.Text = "";
                    txtadhrno.Text = "";
                    ddlpayid.SelectedValue = "0".ToString();
                    txtclntrefno.Text = "";
                    txtpfno.Text = "";
                    txtesino.Text = "";
                    txtbasicpay.Text = "";
                    txtgslino.Text = "";
                    rdbtnpt.SelectedValue = "";
                    rdbtnpf.SelectedValue = "";
                    rblStatus.SelectedValue = "";
                    rblStatus.SelectedValue = "";
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
            int active_yn = 0;
            try
            {

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "update pay_personnel set" +
                        " PRS_PANN='"+ mGlobal.doQuotes(txtpanNo.Text) + "',  PRS_NAME = '" + mGlobal.doQuotes(txtname.Text) + "' , " +
                        " PRS_ADDRESS = '" + mGlobal.doQuotes(txtadd.Text) + "', PRS_GENDER = '" + rdbtngender.SelectedValue + "'," +
                       " PRS_DTOB = '" + mGlobal.doQuotes(Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd")) + "', PRS_DTOJ= '" + mGlobal.doQuotes(Convert.ToDateTime(txtdoj.Text).ToString("yyyy-MM-dd")) + "'," +
                       " PRS_DTOC= '" + mGlobal.doQuotes(Convert.ToDateTime(txtdop.Text).ToString("yyyy-MM-dd")) + "', PRS_DTOR = '" + mGlobal.doQuotes(Convert.ToDateTime(txtdoreg.Text).ToString("yyyy-MM-dd")) + "'," +
                       " PRS_MOBILE = '" + mGlobal.doQuotes(txtcntno.Text) + "', PRS_EMAIL = '" + mGlobal.doQuotes(txtemail.Text) + "', PRS_RESPH='" + mGlobal.doQuotes(txtredno.Text) + "'," +
                       " PRS_QLFT = '" + mGlobal.doQuotes(txtquliftn.Text) + "', PRS_DPCD = '" + ddldepcode.SelectedValue + "'," +
                        " PRS_DECD = '" + ddldesg.SelectedValue + "', PRS_LOCD = '" + ddlLocation.SelectedValue + "'," +
                       " PRS_BKCD = '" + ddlbank.SelectedValue + "', PRS_BKAC = '" + mGlobal.doQuotes(txtalvno.Text) + "'," +
                       " adhar_no = '" + mGlobal.doQuotes(txtadhrno.Text) + "',pay_id='"+ ddlpayid.SelectedValue + "', " +
                       " PRS_CLIENTREFNO='"+ mGlobal.doQuotes(txtclntrefno.Text) +"', PRS_PFNO = '" + mGlobal.doQuotes(txtpfno.Text) + "'," +
                       " PRS_ESINO = '" + mGlobal.doQuotes(txtesino.Text) + "', prs_basic = '" + mGlobal.doQuotes(txtbasicpay.Text) + "'," +
                        " PRS_GLSINO= '" + mGlobal.doQuotes(txtgslino.Text) + "',prs_pt_yn='" + rdbtnpt.SelectedValue + "',prs_pf_yn='" + rdbtnpf.SelectedValue + "'," +
                        "PRS_UPDBY='" + Session["LogonName"] + "',PRS_UPDON= sysdate()," +
                        "active_yn = '" + rblStatus.SelectedValue + "',prs_esi_yn = "+ rblESI.SelectedValue + ",PRS_STATE_SL = " + ddlState.SelectedValue +"  where PRS_EMNO = '" + txtempno.Text + "' and co_sl = '" + Session["CoSl"] + "'";

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
                    txtempno.Text = "";
                    txtpanNo.Text = "";
                    txtname.Text = "";
                    txtadd.Text = "";
                    txtdob.Text = "";
                    txtdoj.Text = "";
                    txtdop.Text = "";
                    txtdoreg.Text = "";
                    txtcntno.Text = "";
                    txtemail.Text = "";
                    txtredno.Text = "";
                    txtquliftn.Text = "";
                    ddldepcode.SelectedValue = "0".ToString();
                    ddldesg.SelectedValue = "0".ToString();
                    ddlLocation.SelectedValue = "0".ToString();
                    ddlbank.SelectedValue = "0".ToString();
                    ddlState.SelectedValue = "0".ToString();
                    txtalvno.Text = "";
                    txtadhrno.Text = "";
                    ddlpayid.SelectedValue = "0".ToString();
                    txtclntrefno.Text = "";
                    txtpfno.Text = "";
                    txtesino.Text = "";
                    txtbasicpay.Text = "";
                    txtgslino.Text = "";
                    rdbtnpt.SelectedValue = "";
                    rdbtnpf.SelectedValue = "";
                    rblStatus.SelectedValue = "";
                    rblESI.SelectedValue = "";
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

                cmd.CommandText = "delete from pay_personnel where PRS_EMNO='" + code + "' and co_sl = " + Session["CoSl"] + " ";
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
            gvView.Visible = true;
            panelAddEdit.Visible = false;
            panelVIEW.Visible = false;
            panelError.Visible = true;
            panelDelete.Visible = false;
            this.DivSearch.Visible = true;
            lblMsg.Text = "";
            txtempno.Text = "";
            txtpanNo.Text = "";
            txtname.Text = "";
            txtadd.Text = "";
            txtdob.Text = "";
            txtdoj.Text = "";
            txtdop.Text = "";
            txtdoreg.Text = "";
            txtcntno.Text = "";
            txtemail.Text = "";
            txtredno.Text = "";
            txtquliftn.Text = "";
            ddldepcode.SelectedValue = "0".ToString();
            ddldesg.SelectedValue = "0".ToString();
            ddlLocation.SelectedValue = "0".ToString();
            ddlbank.SelectedValue = "0".ToString();
            ddlState.SelectedValue = "0".ToString();
            txtalvno.Text = "";
            txtadhrno.Text = "";
            ddlpayid.SelectedValue = "0".ToString();
            txtclntrefno.Text = "";
            txtpfno.Text = "";
            txtesino.Text = "";
            txtbasicpay.Text = "";
            txtgslino.Text = "";
            rblStatus.SelectedValue = "1".ToString();
           
            Bind();
        }
        protected void btncloseMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/home.aspx");
        }

        //protected void btnpayid_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string code = hfSl.Value;
        //        sql = "SELECT vu_payid.*FROM vu_payid, co WHERE FIND_IN_SET(csl_payid, co.pay_id) AND csl_payid ='" + txtpayid.Text + "'";
        //        if (mGlobal.conDatabase.State == ConnectionState.Open)
        //        {
        //            mGlobal.conDatabase.Close();
        //        }

        //        mGlobal.conDatabase.Open();
        //        MySqlDataAdapter dAdapter = default(MySqlDataAdapter);

        //        DataSet ds = new DataSet();
        //        dAdapter = new MySqlDataAdapter(sql, mGlobal.conDatabase);

        //        dAdapter.Fill(ds);
        //        dt = ds.Tables[0];
        //        gridpopup.DataSource = dt;
        //        gridpopup.DataBind();
        //        gridpopup.Visible = true;
        //        popuppanel.Visible = true;
        //        //popuppanel.Visible = true;
        //        //payidpopup.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //        lblMsg.ForeColor = System.Drawing.Color.Red;
        //    }
        //    finally
        //    {
        //        mGlobal.conDatabase.Close();
        //    }
        //}

       
        //protected void gridpopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gridpopup.PageIndex = e.NewPageIndex;
        //    this.DataBind();
        //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        //}

        //protected void btnpaydetail_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string code = hfSl.Value;
        //        sql = "SELECT vu_payid.*FROM vu_payid, co WHERE FIND_IN_SET(csl_payid, co.pay_id) AND CODE =1005";
        //        if (mGlobal.conDatabase.State == ConnectionState.Open)
        //        {
        //            mGlobal.conDatabase.Close();
        //        }

        //        mGlobal.conDatabase.Open();
        //        MySqlDataAdapter dAdapter = default(MySqlDataAdapter);

        //        DataSet ds = new DataSet();
        //        dAdapter = new MySqlDataAdapter(sql, mGlobal.conDatabase);

        //        dAdapter.Fill(ds);
        //        dt = ds.Tables[0];
        //        gridpopup.DataSource = dt;
        //        gridpopup.DataBind();
        //        gridpopup.Visible = true;
        //        popuppanel.Visible = true;
        //        //popuppanel.Visible = true;
        //        //payidpopup.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //        lblMsg.ForeColor = System.Drawing.Color.Red;
        //    }
        //    finally
        //    {
        //        mGlobal.conDatabase.Close();
        //    }
        //}
    }
}
 
