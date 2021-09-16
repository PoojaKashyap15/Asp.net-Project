using MySql.Data.MySqlClient;
using prototype.App_Code;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Salary.Views
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string sql;
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

                    // lblHeading.Text = Session["MenuName"].ToString();

                    Session["Mode"] = "";

                    show();

                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }

        private void imgRefresh_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        private static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query);
            String constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            MySqlConnection con = new MySqlConnection(constr);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;

        }

        protected void show()
        {
            active_yn();
            gender_mf();
            dept_count();
            desig_count();
            att_count();
            max_salary();
            bind_gv();
        }
        protected void active_yn()
        {
            try
            {
                sql = string.Format("SELECT  IF ( active_yn=1,'Active','Inactive') AS STATUS,COUNT(*) AS noe  FROM pay_personnel WHERE co_sl = '" + Session["CoSl"] + "' GROUP BY active_yn ");
                DataTable dt = GetData(sql);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }
                chartActive_yn.Series[0].Points.DataBindXY(x, y);
                chartActive_yn.Series[0].ChartType = SeriesChartType.Pie;
                chartActive_yn.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                chartActive_yn.Series[0].IsValueShownAsLabel = true;
                chartActive_yn.Legends[0].Enabled = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void gender_mf()
        {
            try
            {
                sql = string.Format("SELECT  IF (UPPER(prs_gender)='M','Male','Female') AS gender,COUNT(*) AS noe  FROM pay_personnel WHERE active_yn = 1 and  " +
                    " co_sl = '" + Session["CoSl"] + "' GROUP BY prs_gender ");
                DataTable dt = GetData(sql);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }
                chartMale_female.Series[0].Points.DataBindXY(x, y);
                chartMale_female.Series[0].ChartType = SeriesChartType.Pyramid;
                chartMale_female.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                chartMale_female.Series[0].IsValueShownAsLabel = true;
                chartMale_female.Legends[0].Enabled = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void dept_count()
        {
            try
            {
                sql = string.Format("SELECT DPT_DESC,COUNT(*) FROM pay_personnel p ,psys_dept d WHERE prs_dpcd = DPT_CODE AND p.co_sl = d.co_sl " +
                    " AND p.active_yn = 1 AND p.co_sl = '" + Session["CoSl"] + "' GROUP BY DPT_DESC");
                DataTable dt = GetData(sql);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }
                chart_dept_noe.Series[0].Points.DataBindXY(x, y);
                chart_dept_noe.Series[0].ChartType = SeriesChartType.Doughnut;
                chart_dept_noe.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                chart_dept_noe.Series[0].IsValueShownAsLabel = true;
                chart_dept_noe.Legends[0].Enabled = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        protected void desig_count()
        {
            try
            {
                sql = string.Format("SELECT DSG_DESC,COUNT(*) FROM pay_personnel p,psys_desg ds WHERE PRS_DECD = DSG_CODE AND p.co_sl = ds.co_sl " +
                    " AND p.active_yn = 1 AND p.co_sl = '" + Session["CoSl"] + "' GROUP BY DSG_DESC");

                DataTable dt = GetData(sql);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }
                chart_desg_noe.Series[0].Points.DataBindXY(x, y);
                chart_desg_noe.Series[0].ChartType = SeriesChartType.StackedBar;
                chart_desg_noe.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                chart_desg_noe.Series[0].IsValueShownAsLabel = true;
                chart_desg_noe.Legends[0].Enabled = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void att_count()
        {
            try
            {
                sql = string.Format("SELECT  CAST(CONCAT(prs_emno, '-', prs_name) AS CHAR) AS NAME,SUM(ATT_ABSENT) AS absent FROM pay_personnel p ,pay_attreg a WHERE prs_emno = ATT_EMNO AND p.co_sl = a.co_sl " +
                       " AND p.active_yn = 1 AND p.co_sl =  '" + Session["CoSl"] + "' AND ATT_YID = '" + Session["YearSl"] + "' GROUP BY prs_emno, prs_name HAVING SUM(ATT_ABSENT) > 0 " +
                       "  ORDER BY SUM(ATT_ABSENT) DESC  LIMIT 5");

                DataTable dt = GetData(sql);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }
                chart_attd.Series[0].Points.DataBindXY(x, y);
                chart_attd.Series[0].ChartType = SeriesChartType.StackedBar;
                chart_attd.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                chart_attd.Series[0].IsValueShownAsLabel = true;
                chart_attd.Legends[0].Enabled = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void max_salary()
        {
            try
            {
                sql = string.Format(" SELECT prg_yymm,SUM(prg_amnt) AS amount  FROM pay_reg WHERE " +
                    " co_sl =  '" + Session["CoSl"] + "' AND prg_edcd = 500 AND prg_yid = '" + Session["YearSl"] + "'  GROUP BY prg_yymm ORDER BY prg_yymm");

                DataTable dt = GetData(sql);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }
                chart_max_salary.Series[0].Points.DataBindXY(x, y);
                chart_max_salary.Series[0].ChartType = SeriesChartType.Line;
                chart_max_salary.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                chart_max_salary.Series[0].IsValueShownAsLabel = true;
                chart_max_salary.Legends[0].Enabled = false;

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        protected void bind_gv()
        {
            string sql2 = "";
            string sql3 = "";
            string sql4 = "";
            string sql5 = "";
            try
            {
                sql2 = "SELECT 'No of Employee' AS descs, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '04') AS apr, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '05' )  AS may, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                "  AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '06' )  AS jun," +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '07' )  AS jul, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '08' )  AS aug, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '09' )  AS sep, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '10' )  AS oct1, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '11' )  AS nov, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '12' )  AS dec1, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '01' )  AS jan, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                "AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '02' )  AS feb, " +
                " (SELECT  COUNT(*) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
                "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '03' )  AS mar FROM DUAL union all ";

                sql3 = "SELECT 'Gross Salary' AS descs, " +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '04') AS apr, " +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '05' )  AS may, " +
                " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                "  AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '06' )  AS jun," +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '07' )  AS jul, " +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '08' )  AS aug, " +
                " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '09' )  AS sep, " +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '10' )  AS oct1, " +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '11' )  AS nov, " +
                " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '12' )  AS dec1, " +
                " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '01' )  AS jan, " +
                " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                "AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '02' )  AS feb, " +
                " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 500 " +
                "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '03' )  AS mar FROM DUAL union all ";

                sql4 = "SELECT 'Deductions' AS descs, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '04') AS apr, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '05' )  AS may, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            "  AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '06' )  AS jun," +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '07' )  AS jul, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '08' )  AS aug, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '09' )  AS sep, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '10' )  AS oct1, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '11' )  AS nov, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '12' )  AS dec1, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '01' )  AS jan, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            "AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '02' )  AS feb, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 900 " +
            "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '03' )  AS mar FROM DUAL union all ";

                sql5 = "SELECT 'Net Salary' AS descs, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '04') AS apr, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '05' )  AS may, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            "  AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '06' )  AS jun," +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '07' )  AS jul, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '08' )  AS aug, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '09' )  AS sep, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '10' )  AS oct1, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '11' )  AS nov, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            " AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '12' )  AS dec1, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '01' )  AS jan, " +
            " (SELECT  sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            "AND p.co_sl = '" + Session["CoSl"] + "' AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '02' )  AS feb, " +
            " (SELECT sum(prg_Amnt) FROM pay_reg p, psys_saltype s WHERE prg_rlcd = STY_ID AND p.co_sl = s.co_sl AND regular_yn = 1 AND prg_edcd = 999 " +
            "AND p.co_sl = '" + Session["CoSl"] + "'  AND prg_yid = '" + Session["YearSl"] + "' AND SUBSTR(prg_yymm, 5) = '03' )  AS mar FROM DUAL ";

                sql = sql2 + sql3 + sql4 + sql5;
                mGlobal.bindataGrid(gvView, sql);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

    }
}