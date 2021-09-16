using prototype.App_Code;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using System.Drawing;

namespace Salary.Views
{
    public partial class ViewSal : System.Web.UI.Page
    {
        string sql;
        DataTable dt;
        int currentId = 0;
        string currentIdNEW = "";
        decimal subTotal = 0;
        decimal total = 0;
        int subTotalRowIndex = 0;

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
                    Binddropdown();

                    panelAddEdit.Visible = true;
                    panelViewEmployee.Visible = false;
                 
                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }

        protected void Binddropdown()
        {
            try
            {
                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlSalType);

                sql = "select dpt_desc as name,dpt_code as sl from psys_dept where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- All Department --", "0"));

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


        protected void btnshow_Click(object sender, EventArgs e)
        {
            Bind();
        }

        private void Bind()
        {
            try
            {
                string sql2 = "";
                lblMsg.Text = "";
                lblViewSalary.Text = "";

                if (ddlDept.SelectedValue == "0".ToString())
                {
                    sql2 = " and  prh_dpcd >= 0";
                }
                else
                {
                    sql2 = " and prh_dpcd = " + ddlDept.SelectedValue;
                }

                
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");
                sql = " SELECT prs_Emno,prh_yid,PRS_EMNO,PRS_NAME,PRH_YYMM,STY_NAME AS saltype,DPT_DESC AS dept,ifnull (PRH_GROSS,0) AS gross, " +
                    " ifnull(PRH_DED,0) AS ded, ifnull(PRH_NET,0) AS net FROM pay_reg_hdr p,psys_saltype t, psys_dept d,pay_personnel e " +
                    " WHERE p.PRH_DPCD = d.DPT_CODE AND p.PRH_RLCD = t.STY_ID AND prh_emno = PRS_EMNO AND p.PRH_YYMM = " + yymm + "  " +
                     " and p.co_sl = t.co_sl and p.co_sl = d.co_sl and p.co_sl = e.co_sl " +
                    " AND p.PRH_YID =  " + Session["YearSl"] + "  " + sql2 + " and prh_rlcd = " + ddlSalType.SelectedValue +"  AND p.CO_SL =  " + Session["CoSl"] + " order by prs_emno";

                using (MySqlConnection con = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                sda.Fill(dt);
                                gvSalary.DataSource = dt;
                                gvSalary.DataBind();
                                int iTotalRecords = ((DataTable)(gvSalary.DataSource)).Rows.Count;
                                if (iTotalRecords > 1)
                                {
                                    //Calculate Sum and display in Footer Row
                                    decimal gross = dt.AsEnumerable().Sum(row => row.Field<decimal>("gross"));
                                    decimal ded = dt.AsEnumerable().Sum(row => row.Field<decimal>("ded"));
                                    decimal net = dt.AsEnumerable().Sum(row => row.Field<decimal>("net"));
                                    gvSalary.FooterRow.Cells[5].Text = "Total";
                                    gvSalary.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                                    gvSalary.FooterRow.Cells[5].Font.Bold = true;
                                    gvSalary.FooterRow.Cells[6].Text = gross.ToString("N2");
                                    gvSalary.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                                    gvSalary.FooterRow.Cells[6].Font.Bold = true;
                                    gvSalary.FooterRow.Cells[7].Text = ded.ToString("N2");
                                    gvSalary.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                                    gvSalary.FooterRow.Cells[7].Font.Bold = true;
                                    gvSalary.FooterRow.Cells[8].Text = net.ToString("N2");
                                    gvSalary.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                                    gvSalary.FooterRow.Cells[8].Font.Bold = true;
                                }
                            }
                        }
                    }
                }


                lblViewSalary.Text = "Salary Details for the month of " + txtmmyyyy.Text;
                panelAddEdit.Visible = true;
                panelViewEmployee.Visible = false;
               

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

        protected void gvSalary_RowCommand(object sender, GridViewCommandEventArgs e)
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

                    panelAddEdit.Visible = true;
                    panelViewEmployee.Visible = true;
                    gvSalary.Visible = false;

                    int pemp_no = Convert.ToInt32(e.CommandArgument);

                    string sql = " SELECT CAST(CONCAT(prs_emno, ' - ', prs_name) AS CHAR) as name FROM pay_personnel WHERE prs_emno = " + pemp_no + " and co_Sl =" + Session["CoSl"] + "";
                    lblEmpName.Text = "Employee No & Name :" + mGlobal.getResult(sql);
                    Bind_employee(pemp_no);

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
        }

        protected void gvSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSalary.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            txtmmyyyy.Text = "";

            DataTable ds = new DataTable();
            ds = null;
            gvSalary.DataSource = ds;
            gvSalary.DataBind();
            panelAddEdit.Visible = true;
            panelViewEmployee.Visible = false;
          

        }

        protected void btnCloseView_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelViewEmployee.Visible = false;
            gvSalary.Visible = true;
        }

        protected void gvEmployee_DataBound(object sender, EventArgs e)
        {
            try
            {
                string porr = "";
                for (int i = subTotalRowIndex; i < gvEmployee.Rows.Count; i++)
                {
                    subTotal += Convert.ToDecimal(gvEmployee.Rows[i].Cells[3].Text);
                    porr = (gvEmployee.Rows[i].Cells[1].Text);
                }
                if (porr == "P".ToString())
                {
                    this.AddTotalRow("GROSS", subTotal.ToString("N2"));
                    this.AddTotalRow("NET PAYMENT", total.ToString("N2"));
                }
                else
                {
                    this.AddTotalRow("DEDCTIONS", subTotal.ToString("N2"));
                    this.AddTotalRow("NET PAYMENT", total.ToString("N2"));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        protected void gvEmployee_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string subhead = "GROSS";
                subTotal = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
                    int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["PRG_EDCD"]);
                    string orderIdNEW = Convert.ToString(dt.Rows[e.Row.RowIndex]["PRG_PORR"]);
                    if (orderIdNEW == "P".ToString())
                    {
                        total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["PRG_AMNT"]);
                    }
                    else
                    {
                        total -= Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["PRG_AMNT"]);
                    }
                    if (orderIdNEW != currentIdNEW)
                    {
                        if (e.Row.RowIndex > 0)
                        {
                            for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                            {
                                subTotal += Convert.ToDecimal(gvEmployee.Rows[i].Cells[3].Text);
                            }

                            this.AddTotalRow(subhead, subTotal.ToString("N2"));


                            subTotalRowIndex = e.Row.RowIndex;
                        }
                        currentIdNEW = orderIdNEW;
                        subhead = "GROSS";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

        }

        private void AddTotalRow(string labelText, string value)
        {
            try
            {
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                row.BackColor = ColorTranslator.FromHtml("#F9F9F9");
                row.Font.Bold = true;
                row.Cells.AddRange(new TableCell[4] { new TableCell (),new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right},
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

                gvEmployee.Controls[0].Controls.Add(row);
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

        private void Bind_employee(int memp_no)
        {
            try
            {
                lblMsg.Text = "";
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                sql = "  SELECT PRG_EMNO, PRG_YYMM, PRG_RLCD, PRG_EDCD, PRG_PORR, PRG_AMNT  , COD_DESC AS descs FROM pay_reg p, " +
                " psys_codes c  WHERE prg_edcd = COD_CODE and p.co_sl = c.co_sl AND   prg_edcd < 500 and " +
                "  prg_emno = " + memp_no + " AND prg_rlcd =   " + ddlSalType.SelectedValue + "  AND prg_yymm = " + yymm + " AND P.CO_sL = C.CO_SL AND  " +
                "   p.co_sl = " + Session["CoSl"] + " AND prg_yid =  " + Session["YearSl"] + " ORDER BY PRG_PORR,prg_edcd";

                mGlobal.bindataGrid(gvEmployee, sql);

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

        protected void btnMIS_Click(object sender, EventArgs e)
        {

            try
            {
                string sql2 = "";
                lblMsg.Text = "";
                lblViewSalary.Text = "";

                if (ddlDept.SelectedValue == "0".ToString())
                {
                    sql2 = " and  dpt_code >= 0";
                }
                else
                {
                    sql2 = " and dpt_code = " + ddlDept.SelectedValue;
                }

                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");
                string query = "SELECT prs_emno AS emp_no,prs_name AS emp_name,dpt_desc AS Dept, DSG_DESC AS desig,BNK_NAME AS bank,prs_bkac AS ac_no,prg_yymm AS sal_month " +
            " , SUM(CASE prg_edcd WHEN 1 THEN prg_amnt END) AS Basic, " +
            " SUM(CASE prg_edcd WHEN 5 THEN prg_amnt END) AS Hra, SUM(CASE prg_edcd WHEN 500 THEN prg_amnt END) AS Gross, " +
            " SUM(CASE prg_edcd WHEN 52 THEN prg_amnt END) AS PF, SUM(CASE prg_edcd WHEN 51 THEN prg_amnt END) AS ESI, " +
            "  SUM(CASE prg_edcd WHEN 54 THEN prg_amnt END) AS PT, SUM(CASE prg_edcd WHEN 56 THEN prg_amnt END) AS TDS, " +
            " SUM(CASE prg_edcd WHEN 900 THEN prg_amnt END) AS Ded, SUM(CASE prg_edcd WHEN 999 THEN prg_amnt END) AS Net " +
            " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b " +
            " WHERE prs_dpcd = dpt_code AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE " +
            " AND prg_yid =  " + Session["YearSl"] + " " + sql2 + "  AND r.co_sl = " + Session["CoSl"] + "   AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
            " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl   " +
            " GROUP BY prs_emno,prs_name,dpt_desc, DSG_DESC,BNK_NAME,prs_bkac,prg_yymm ORDER BY dpt_desc, prs_emno; ";

                query += "SELECT  dpt_desc AS Dept,cod_code as code,cod_Desc as descriptions,SUM(prg_amnt) as amount  " +
                " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b, psys_codes c  " +
                " WHERE prs_dpcd = dpt_code  and p.co_sl = c.co_sl  AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE  " +
                " AND prg_edcd = cod_code AND prg_yid =  " + Session["YearSl"] + "  " + sql2 + "  AND r.co_sl =  " + Session["CoSl"] + "   AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
                " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl and p.co_sl = c.co_sl " +
                " GROUP BY cod_code,cod_Desc,dpt_desc ORDER BY dpt_desc,cod_code, cod_Desc; ";

                query += "SELECT dpt_code,dpt_desc AS Dept,prg_yymm AS sal_month " +
           " , SUM(CASE prg_edcd WHEN 1 THEN prg_amnt END) AS Basic, " +
           " SUM(CASE prg_edcd WHEN 5 THEN prg_amnt END) AS Hra, SUM(CASE prg_edcd WHEN 500 THEN prg_amnt END) AS Gross, " +
           " SUM(CASE prg_edcd WHEN 52 THEN prg_amnt END) AS PF, SUM(CASE prg_edcd WHEN 51 THEN prg_amnt END) AS ESI, " +
           "  SUM(CASE prg_edcd WHEN 54 THEN prg_amnt END) AS PT, SUM(CASE prg_edcd WHEN 56 THEN prg_amnt END) AS TDS, " +
           " SUM(CASE prg_edcd WHEN 900 THEN prg_amnt END) AS Ded, SUM(CASE prg_edcd WHEN 999 THEN prg_amnt END) AS Net " +
           " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b " +
           " WHERE prs_dpcd = dpt_code AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE " +
           " AND prg_yid =  " + Session["YearSl"] + " AND r.co_sl = " + Session["CoSl"] + "   " + sql2 + "   AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
            " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl  " +
           " GROUP BY dpt_code,dpt_desc ,prg_yymm   ORDER BY dpt_code, dpt_desc; ";

                query += "SELECT  prs_emno as emp_no,prs_name as emp_name,dpt_desc AS Dept,cod_code as code,cod_Desc  as descriptions,SUM(prg_amnt)  as amount " +
                " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b, psys_codes c  " +
                " WHERE prs_dpcd = dpt_code AND   p.co_sl = c.co_sl and prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE  " +
                " AND prg_edcd = cod_code AND prg_yid =  " + Session["YearSl"] + "   " + sql2 + "  AND r.co_sl =  " + Session["CoSl"] + "  AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
               " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl and p.co_sl = c.co_sl " +
                " GROUP BY prs_emno,prs_name,cod_code,cod_Desc,dpt_desc ORDER BY prs_emno,prs_name,cod_code,cod_Desc; ";

                query += "SELECT prs_emno AS emp_no,prs_name AS emp_name,dpt_desc AS Dept, DSG_DESC AS desig,BNK_NAME AS bank,prs_bkac AS ac_no,prg_yymm AS sal_month, " +
                            " SUM(CASE prg_edcd WHEN 52 THEN prg_amnt END) AS PF " +
                            " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b " +
                            " WHERE prs_dpcd = dpt_code AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE " +
                            " AND prg_yid =  " + Session["YearSl"] + "   " + sql2 + "  AND r.co_sl = " + Session["CoSl"] + "   AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
                           " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl " +
                            " GROUP BY prs_emno,prs_name,dpt_desc, DSG_DESC,BNK_NAME,prs_bkac,prg_yymm ORDER BY dpt_desc, prs_emno; ";

                query += "SELECT prs_emno AS emp_no,prs_name AS emp_name,dpt_desc AS Dept, DSG_DESC AS desig,BNK_NAME AS bank,prs_bkac AS ac_no,prg_yymm AS sal_month, " +
                                        "  SUM(CASE prg_edcd WHEN 54 THEN prg_amnt END) AS PT " +
                                        " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b " +
                                        " WHERE prs_dpcd = dpt_code AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE " +
                                        " AND prg_yid =  " + Session["YearSl"] + "   " + sql2 + "  AND r.co_sl = " + Session["CoSl"] + "   AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
                                       " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl " +
                                        " GROUP BY prs_emno,prs_name,dpt_desc, DSG_DESC,BNK_NAME,prs_bkac,prg_yymm ORDER BY  prs_emno; ";


                query += "SELECT prs_emno AS emp_no,prs_name AS emp_name,dpt_desc AS Dept, DSG_DESC AS desig,BNK_NAME AS bank,prs_bkac AS ac_no,prg_yymm AS sal_month, " +
                          "   SUM(CASE prg_edcd WHEN 51 THEN prg_amnt END) AS ESI " +
                              " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b " +
                          " WHERE prs_dpcd = dpt_code AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE " +
                          " AND prg_yid =  " + Session["YearSl"] + " AND r.co_sl = " + Session["CoSl"] + "   " + sql2 + "    AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
                        " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl " +
                          " GROUP BY prs_emno,prs_name,dpt_desc, DSG_DESC,BNK_NAME,prs_bkac,prg_yymm ORDER BY  prs_emno; ";


                query += "SELECT prs_emno AS emp_no,prs_name AS emp_name,dpt_desc AS Dept, DSG_DESC AS desig,BNK_NAME AS bank,prs_bkac AS ac_no,prg_yymm AS sal_month, " +
                                          "   SUM(CASE prg_edcd WHEN 999 THEN prg_amnt END) AS Net " +
                                          " FROM pay_personnel p,psys_dept d, pay_reg r,psys_desg ds,psys_bank b " +
                                          " WHERE prs_dpcd = dpt_code AND prs_emno = prg_emno AND prs_bkcd = BNK_CODE AND prs_Decd = DSG_CODE " +
                                          " AND prg_yid =  " + Session["YearSl"] + "   " + sql2 + "  AND r.co_sl = " + Session["CoSl"] + "   AND prg_yymm = " + yymm + " and prg_rlcd =  " + ddlSalType.SelectedValue + "  " +
                                          " and p.co_sl = d.co_sl  and p.co_sl = r.co_sl  and p.co_sl = ds.co_sl  and p.co_sl =b.co_Sl " +
                                          " GROUP BY prs_emno,prs_name,dpt_desc, DSG_DESC,BNK_NAME,prs_bkac,prg_yymm ORDER BY prs_emno; ";






                using (MySqlConnection con = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataSet ds = new DataSet())
                            {
                                sda.Fill(ds);

                                //Set Name of DataTables.
                                ds.Tables[0].TableName = "Salary_Details";
                                ds.Tables[1].TableName = "Summary";
                                ds.Tables[2].TableName = "Deptwise_Summary";
                                ds.Tables[3].TableName = "Employeewise_Summary";
                                ds.Tables[4].TableName = "PF";
                                ds.Tables[5].TableName = "PT";
                                ds.Tables[6].TableName = "ESI";
                                ds.Tables[7].TableName = "Bank Statement";

                                using (XLWorkbook wb = new XLWorkbook())
                                {
                                    foreach (DataTable dt in ds.Tables)
                                    {
                                        //Add DataTable as Worksheet.
                                        wb.Worksheets.Add(dt);
                                    }

                                    //Export the Excel file.
                                    Response.Clear();
                                    Response.Buffer = true;
                                    Response.Charset = "";
                                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                    Response.AddHeader("content-disposition", "attachment;filename=Salary.xls");
                                    using (MemoryStream MyMemoryStream = new MemoryStream())
                                    {
                                        wb.SaveAs(MyMemoryStream);
                                        MyMemoryStream.WriteTo(Response.OutputStream);
                                        Response.Flush();
                                        Response.End();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                lblMsg.Text = "Error while fetching data" + ex.Message;
            }
            finally
            {
            }
        }
    }
}