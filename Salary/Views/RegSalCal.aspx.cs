using System;
using prototype.App_Code;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.Linq;

namespace Salary.Views
{
    public partial class RegSalCal : System.Web.UI.Page
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
                sql = "select dpt_desc as name,dpt_code as sl from psys_dept where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlDept);
                ddlDept.Items.Insert(0, new ListItem("-- All Department --", "0"));

                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1  and regular_yn = 1 and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlSalType);

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

        protected void btnCal_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");
                if (ddlDept.SelectedValue == "0".ToString())
                {
                    sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + yymm + "' " +
                   " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_yid = '" + Session["YearSl"].ToString() + "' AND   " +
                   " prh_rlcd = " + ddlSalType.SelectedValue;
                }
                else
                {
                    sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + yymm + "' " +
                   " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_yid = '" + Session["YearSl"].ToString() + "' AND prh_dpcd =  " + ddlDept.SelectedValue + " and " +
                   " prh_rlcd = " + ddlSalType.SelectedValue;
                }

                string lock_yn = mGlobal.getResult(sql);
                if (Convert.ToDouble(lock_yn) > 0)
                {
                    lblMsg.Text = "Sorry!.. This month locked..you cannot calculate salary";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                MySqlCommand commandProcess = new MySqlCommand("SalaryFrBasicALL", mGlobal.conDatabase);

                mGlobal.conDatabase.Open();
                commandProcess.CommandType = System.Data.CommandType.StoredProcedure;
                commandProcess.Parameters.AddWithValue("@pyymm", yymm);
                commandProcess.Parameters.AddWithValue("@prlcd", ddlSalType.SelectedValue);
                commandProcess.Parameters.AddWithValue("@pyid", Session["YearSl"]);
                commandProcess.Parameters.AddWithValue("@pdpcd", ddlDept.SelectedValue);
                commandProcess.Parameters.AddWithValue("@pcosl", Session["CoSl"]);

                commandProcess.CommandTimeout = 500;
                commandProcess.ExecuteNonQuery();

                ShowMsgBox.Show("Salary Calculated Successfully!....");
                Bind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                mGlobal.conDatabase.Close();
                mGlobal.conDatabase.Dispose();

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
                sql = " SELECT prs_Emno,prh_yid,PRS_EMNO,PRS_NAME,PRH_YYMM,STY_NAME AS saltype,DPT_DESC AS dept, ifnull(PRH_GROSS,0) AS gross, " +
                    " ifnull(PRH_DED,0) AS ded, ifnull(PRH_NET,0) AS net FROM pay_reg_hdr p,psys_saltype t, psys_dept d,pay_personnel e " +
                    " WHERE p.PRH_DPCD = d.DPT_CODE AND p.PRH_RLCD = t.STY_ID AND prh_emno = PRS_EMNO AND p.PRH_YYMM = " + yymm + "  " +
                    " and p.co_sl = t.co_sl and p.co_sl = d.co_sl and p.co_sl = e.co_sl " +
                    " AND p.PRH_YID =  " + Session["YearSl"] + "  " + sql2 + "  and prh_rlcd = " + ddlSalType.SelectedValue + "  AND p.CO_SL =  " + Session["CoSl"] + " order by prs_emno";

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
                                int no = dt.Rows.Count;
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

                    string sql = " SELECT CAST(CONCAT(prs_emno, ' - ', prs_name) as char) as name FROM pay_personnel WHERE CO_SL =  " + Session["CoSl"] + " and prs_emno = " + pemp_no;
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
            ddlDept.SelectedValue = "0".ToString();

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
                        subhead = "DEDCTIONS";
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
                " psys_codes c  WHERE prg_edcd = COD_CODE AND p.co_sl and c.co_sl and  prg_edcd < 500 and " +
                "  prg_emno = " + memp_no + " AND prg_rlcd =   " + ddlSalType.SelectedValue + "  " +
                 " and p.co_sl = c.co_sl  AND prg_yymm = " + yymm + " AND p.co_sl = " + Session["CoSl"] + " AND prg_yid =  " + Session["YearSl"] + " ORDER BY PRG_PORR,prg_edcd";

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


    }
}