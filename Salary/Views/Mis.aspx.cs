using prototype.App_Code;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace BUOFC.Views
{
    public partial class Mis : System.Web.UI.Page
    {
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
                    BDPStartDate.SelectedDate = DateTime.Now;
                    BDPEndDate.SelectedDate = DateTime.Now;
                    this.DivSearch.Visible = true;

                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/Home.aspx");
        }



        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") == "0001.01.01")
            {
                lblMsg.Text = "Please select Start Date!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                BDPStartDate.Focus();
                return;
            }
            if (BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") == "0001.01.01")
            {
                lblMsg.Text = "Please select end Date!";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                BDPEndDate.Focus();
                return;
            }

            try
            {

                string query = "SELECT  college_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                        " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                        " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY college_name, adm_class ORDER BY college_name, adm_class; ";

                query += "SELECT  college_name,degree_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                        " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                        " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY college_name,degree_name, adm_class ORDER BY college_name,degree_name, adm_class; ";

                query += "SELECT  college_name,degree_name,course_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                        " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                        " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY college_name,degree_name,course_name, adm_class ORDER BY college_name,degree_name,course_name, adm_class; ";

                query += "SELECT  degree_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                      " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                      " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                      " GROUP BY degree_name, adm_class ORDER BY degree_name, adm_class; ";

                query += "SELECT  degree_name,course_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                        " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                        " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY degree_name,course_name, adm_class ORDER BY degree_name,course_name, adm_class; ";


                query += "SELECT  course_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                        " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                        " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY course_name, adm_class ORDER BY course_name, adm_class; ";

                query += "SELECT  rec_date,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                        " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                        " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY rec_date, adm_class ORDER BY rec_date, adm_class; ";

                query += "SELECT  rec_date,b.insert_by,COUNT(*) AS not_of_students " +
                        " FROM admission_fee_col a,admission_fee_col2 b WHERE a.sl = b.admission_fee_col_sl AND a.drange_sl = b.drange_sl " +
                        " and a.rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND a.drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY rec_date,b.insert_by ORDER BY rec_date, b.insert_by; ";

                query += "SELECT  payment_mode,payment_bank,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                         " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                           " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                           "GROUP BY payment_mode, payment_bank ORDER BY payment_mode, payment_bank;";

                query += "select college_name,degree_name,course_name,adm_class,reg_no,student_name,gender,mobile_no,rec_date,rec_no,sl_no,category_name, " +
                         " fee_amt,fee_paid,fee_bal,proc_fee,total_fee,ledger_no,ledger_date,payment_bank,payment_mode FROM  stu_admission " +
                           " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                           " ORDER BY college_name,degree_name,course_name,adm_class,reg_no,student_name;";

                query += "SELECT  rec_date,degree_name,adm_class,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                 " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                 " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                 " GROUP BY rec_date,degree_name, adm_class ORDER BY rec_date,degree_name, adm_class; ";

                query += "SELECT  b.insert_by,COUNT(*) AS not_of_students " +
                        " FROM admission_fee_col a,admission_fee_col2 b WHERE a.sl = b.admission_fee_col_sl AND a.drange_sl = b.drange_sl " +
                        " and a.rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND a.drange_sl = '" + Session["YearSl"] + "' " +
                        " GROUP BY b.insert_by ORDER BY  COUNT(*) desc; ";

                query += "SELECT  degree_name,course_name,adm_class,feecategory,COUNT(CASE UPPER(gender) WHEN  UPPER('MALE') THEN 1 END) AS Male, " +
                      " COUNT(CASE UPPER(gender) WHEN  UPPER('female') THEN 1 END) AS Female, COUNT(sl)no_of_students FROM  stu_admission " +
                      " WHERE rec_date BETWEEN '" + BDPStartDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND  '" + BDPEndDate.SelectedDate.ToString("yyyy.MM.dd") + "' AND drange_sl = '" + Session["YearSl"] + "' " +
                      " GROUP BY degree_name,course_name,feecategory, adm_class ORDER BY degree_name,course_name, adm_class,feecategory; ";


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
                                ds.Tables[0].TableName = "Col_wise";
                                ds.Tables[1].TableName = "Col_Deg_wise";
                                ds.Tables[2].TableName = "Col_Deg_Cou_wise";
                                ds.Tables[3].TableName = "Deg_wise";
                                ds.Tables[4].TableName = "Deg_Cou_wise";
                                ds.Tables[5].TableName = "Cou_wise";
                                ds.Tables[6].TableName = "Date_wise";
                                ds.Tables[7].TableName = "Datewise_insert";
                                ds.Tables[8].TableName = "PayemntMode";
                                ds.Tables[9].TableName = "Studentlist";
                                ds.Tables[10].TableName = "Date_Deg_wise";
                                ds.Tables[11].TableName = "Insert_summary";
                                ds.Tables[12].TableName = "Deg_Cou_Cat_wise";

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
                                    Response.AddHeader("content-disposition", "attachment;filename=MisReport.xls");
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            BDPStartDate.SelectedDate = DateTime.Now;
            BDPEndDate.SelectedDate = DateTime.Now;
            lblsl.Text = "";
        }
    }
}