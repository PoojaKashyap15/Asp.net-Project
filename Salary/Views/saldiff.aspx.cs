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
    public partial class saldiff : System.Web.UI.Page
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
                    Binddropdown();

                    panelAddEdit.Visible = true;
                    panelViewSalary.Visible = false;
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
                    sql2 = " and  prs_dpcd >= 0";
                }
                else
                {
                    sql2 = " and prs_dpcd = " + ddlDept.SelectedValue;
                }


                string yymm_prev = Convert.ToDateTime(txtmmyyyyprev.Text).ToString("yyyyMM");
                string yymm_curr = Convert.ToDateTime(txtmmyyyycur.Text).ToString("yyyyMM");

                sql = " SELECT PRS_EMNO,PRS_NAME,DPT_DESC as  dept,STY_NAME as saltype,COD_CODE,COD_DESC,SUM(prev_month) AS prev_month, SUM(curr_month)AS curr_month, " +
                " (SUM(curr_month) - SUM(prev_month)) AS Difference " +
                " FROM(SELECT  PRS_EMNO, PRS_NAME, DPT_DESC, STY_NAME, COD_CODE, COD_DESC, PRG_AMNT AS prev_month, 0 AS curr_month FROM pay_reg r, pay_personnel p, psys_dept d, psys_saltype s, psys_codes c " +
                " WHERE p.prs_emno = r.prg_emno  AND p.co_sl = r.co_sl AND prs_dpcd = DPT_CODE AND p.co_sl = d.co_sl " +
                " AND prg_rlcd = s.STY_ID AND prg_edcd = c.COD_CODE AND r.co_sl = c.co_sl " +
                " AND p.co_sl = s.co_sl AND   prg_edcd NOT IN(500, 900, 999) AND " +
                " r.co_sl = " + Session["CoSl"] + " and prg_rlcd = '" + ddlSalType.SelectedValue + "' AND prg_yid =  " + Session["YearSl"] + "  " + sql2 + " AND prg_yymm = '" + yymm_prev + "' UNION ALL " +
                "  SELECT  PRS_EMNO, PRS_NAME, DPT_DESC, STY_NAME, COD_CODE, COD_DESC, 0 AS prev_month, PRG_AMNT AS curr_month FROM pay_reg r, pay_personnel p, psys_dept d, psys_saltype s, psys_codes c " +
                "  WHERE p.prs_emno = r.prg_emno  AND p.co_sl = r.co_sl AND prs_dpcd = DPT_CODE AND p.co_sl = d.co_sl " +
                "  AND prg_rlcd = s.STY_ID AND prg_edcd = c.COD_CODE AND r.co_sl = c.co_sl " +
                "  AND p.co_sl = s.co_sl AND   prg_edcd NOT IN(500, 900, 999) AND " +
                "  r.co_sl = " + Session["CoSl"] + " and prg_rlcd = '" + ddlSalType.SelectedValue + "' AND prg_yid =  " + Session["YearSl"] + "  " + sql2 + " AND prg_yymm = '" + yymm_curr + "' " +
                " ) A GROUP BY PRS_EMNO, PRS_NAME, DPT_DESC, STY_NAME, COD_CODE, COD_DESC " +
                " HAVING(SUM(curr_month) - SUM(prev_month)) <> 0 ORDER BY prs_emno, cod_code ";



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

                            }
                        }
                    }
                }


                lblViewSalary.Text = "Salary Difference";
                panelAddEdit.Visible = true;
                panelViewSalary.Visible = true;

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



        protected void gvSalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSalary.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            txtmmyyyyprev.Text = "";
            txtmmyyyycur.Text = "";
            DataTable ds = new DataTable();
            ds = null;
            gvSalary.DataSource = ds;
            gvSalary.DataBind();
            panelAddEdit.Visible = true;
            panelViewSalary.Visible = false;

        }

        protected void btnCloseView_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelViewSalary.Visible = true;
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
                    sql2 = " and  prs_dpcd >= 0";
                }
                else
                {
                    sql2 = " and prs_dpcd = " + ddlDept.SelectedValue;
                }


                string yymm_prev = Convert.ToDateTime(txtmmyyyyprev.Text).ToString("yyyyMM");
                string yymm_curr = Convert.ToDateTime(txtmmyyyycur.Text).ToString("yyyyMM");

                sql = " SELECT PRS_EMNO,PRS_NAME,DPT_DESC as  dept,STY_NAME as saltype,COD_CODE,COD_DESC,SUM(prev_month) AS prev_month, SUM(curr_month)AS curr_month, " +
                " (SUM(curr_month) - SUM(prev_month)) AS Difference " +
                " FROM(SELECT  PRS_EMNO, PRS_NAME, DPT_DESC, STY_NAME, COD_CODE, COD_DESC, PRG_AMNT AS prev_month, 0 AS curr_month FROM pay_reg r, pay_personnel p, psys_dept d, psys_saltype s, psys_codes c " +
                " WHERE p.prs_emno = r.prg_emno  AND p.co_sl = r.co_sl AND prs_dpcd = DPT_CODE AND p.co_sl = d.co_sl " +
                " AND prg_rlcd = s.STY_ID AND prg_edcd = c.COD_CODE AND r.co_sl = c.co_sl " +
                " AND p.co_sl = s.co_sl AND   prg_edcd NOT IN(500, 900, 999) AND " +
                " r.co_sl = " + Session["CoSl"] + "  and prg_rlcd = '" + ddlSalType.SelectedValue + "'  AND prg_yid =  " + Session["YearSl"] + "  " + sql2 + " AND prg_yymm = '" + yymm_prev + "' UNION ALL " +
                "  SELECT  PRS_EMNO, PRS_NAME, DPT_DESC, STY_NAME, COD_CODE, COD_DESC, 0 AS prev_month, PRG_AMNT AS curr_month FROM pay_reg r, pay_personnel p, psys_dept d, psys_saltype s, psys_codes c " +
                "  WHERE p.prs_emno = r.prg_emno  AND p.co_sl = r.co_sl AND prs_dpcd = DPT_CODE AND p.co_sl = d.co_sl " +
                "  AND prg_rlcd = s.STY_ID AND prg_edcd = c.COD_CODE AND r.co_sl = c.co_sl " +
                "  AND p.co_sl = s.co_sl AND   prg_edcd NOT IN(500, 900, 999) AND " +
                "  r.co_sl = " + Session["CoSl"] + "  and prg_rlcd = '" + ddlSalType.SelectedValue + "'  AND prg_yid =  " + Session["YearSl"] + "  " + sql2 + " AND prg_yymm = '" + yymm_curr + "' " +
                " ) A GROUP BY PRS_EMNO, PRS_NAME, DPT_DESC, STY_NAME, COD_CODE, COD_DESC " +
                " HAVING(SUM(curr_month) - SUM(prev_month)) <> 0 ORDER BY prs_emno, cod_code ";






                using (MySqlConnection con = new MySqlConnection(mGlobal.conDatabaseString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql))
                    {
                        using (MySqlDataAdapter sda = new MySqlDataAdapter())
                        {
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataSet ds = new DataSet())
                            {
                                sda.Fill(ds);

                                //Set Name of DataTables.
                                ds.Tables[0].TableName = "Salary_Difference";

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
                                    Response.AddHeader("content-disposition", "attachment;filename=SalaryDiff.xls");
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

        protected void gvSalary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                for (int rowIndex = gvSalary.Rows.Count - 2;
                                       rowIndex >= 0; rowIndex--)
                {
                    GridViewRow gvRow = gvSalary.Rows[rowIndex];
                    GridViewRow gvPreviousRow = gvSalary.Rows[rowIndex + 1];
                    for (int cellCount = 0; cellCount < gvRow.Cells.Count;
                                                                  cellCount++)
                    {
                        if (gvRow.Cells[cellCount].Text ==
                                               gvPreviousRow.Cells[cellCount].Text)
                        {
                            if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                            {
                                gvRow.Cells[cellCount].RowSpan = 2;
                            }
                            else
                            {
                                gvRow.Cells[cellCount].RowSpan =
                                    gvPreviousRow.Cells[cellCount].RowSpan + 1;
                            }
                            gvPreviousRow.Cells[cellCount].Visible = false;
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