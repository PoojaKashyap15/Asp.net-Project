using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.UI.WebControls;
 


namespace prototype.App_Code
{

    public class mGlobal
    {
        public static MySqlConnection conDatabase = new MySqlConnection("SERVER=localhost;DATABASE=payroll;UID=root;PASSWORD=mysql123;Allow Zero Datetime=true;convert zero datetime=True;Connect Timeout=300000; pooling='true'; Max Pool Size=2000;Min Pool Size=20");
        public static MySqlConnection conDatabaseSecond = new MySqlConnection("SERVER=localhost;DATABASE=payroll;UID=root;PASSWORD=mysql123;Allow Zero Datetime=true;convert zero datetime=True;Connect Timeout=300000; pooling='true'; Max Pool Size=2000;Min Pool Size=20");
        public static MySqlConnection conDatabaseThird = new MySqlConnection("SERVER=localhost;DATABASE=payroll;UID=root;PASSWORD=mysql123;Allow Zero Datetime=true;convert zero datetime=True;Connect Timeout=300000; pooling='true'; Max Pool Size=2000;Min Pool Size=20");
        public static string conDatabaseString = "server=localhost;user id=root;password=mysql123;database=payroll;Allow Zero Datetime=true;convert zero datetime=True;Connect Timeout=300000; pooling='true'; Max Pool Size=200";

        public static string rvalue;
        public static MySqlDataReader Reader;
        public static void ConnectionReset()
        {
            if (conDatabase.State == ConnectionState.Open)
            {
                conDatabase.Close();
            }

            conDatabase.Open();
        }

        public static string doQuotes(string input)
        {
            return input.Replace("'", "''");
        }

       
        public static string ISNULL(string Expression, string TruePart, string FalsePart)
        {
            if (string.IsNullOrEmpty(Expression))
            {
                return TruePart;
            }
            else
            {
                return FalsePart;
            }

        }

        public static int ISNULL(string Expression, int TruePart, int FalsePart)
        {
            if (string.IsNullOrEmpty(Expression))
            {
                return TruePart;
            }
            else
            {
                return FalsePart;
            }
        }

        public static object ISNULL(string Expression, object TruePart, object FalsePart)
        {
            if (string.IsNullOrEmpty(Expression))
            {
                return TruePart;
            }
            else
            {
                return FalsePart;
            }
        }

      

        public static void qs_check(string Loginsl, string cosl, string qs, out string viewyn, out string addyn, out string edityn, out string deleteyn, out string printyn, out string qs_check, out string typ, out string menu_name)
        {
            qs_check = "0";
            addyn = "0";
            edityn = "0";
            deleteyn = "0";
            printyn = "0";
            viewyn = "0";
            typ = "";
            menu_name = "";

            try
            {
                addyn = "0";
                edityn = "0";
                deleteyn = "0";
                printyn = "0";
                viewyn = "0";
                qs_check = "0";
                typ = "";
                menu_name = "";
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }

                //string sql = "SELECT ifnull(qs,'') as qs,m.sl, Title, Description,ifnull(typ,'') as typ, Url,add_yn,edit_yn,view_yn,print_yn,delete_yn FROM Menus m , permission p  WHERE m.sl = p.menus_sl and  " +
                //     " active_yn = 1 and view_yn = 1  and m.co_sl='" + cosl + "'  and p.user_sl = '" + Loginsl + "'  and upper(qs)='" + qs + "'";

                string sql = "SELECT ifnull(qs,'') as qs,m.sl, Title, Description,ifnull(typ,'') as typ, Url,add_yn,edit_yn,view_yn,print_yn,delete_yn FROM Menus m , permission p  WHERE m.sl = p.menus_sl and  " +
                   " active_yn = 1 and view_yn = 1    and p.user_sl = '" + Loginsl + "'  and upper(qs)='" + qs + "'";


                MySqlCommand command = new MySqlCommand(sql, mGlobal.conDatabase);
                mGlobal.conDatabase.Open();
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    qs_check = dr.GetString("qs");
                    addyn = dr.GetString("add_yn");
                    edityn = dr.GetString("edit_yn");
                    deleteyn = dr.GetString("delete_yn");
                    viewyn = dr.GetString("view_yn");
                    printyn = dr.GetString("print_yn");
                    typ = dr.GetString("typ");
                    menu_name = dr.GetString("title");
                }
                dr.Close();

                if (qs_check == 0.ToString())
                {
                    addyn = "0";
                    edityn = "0";
                    deleteyn = "0";
                    printyn = "0";
                    viewyn = "0";
                    qs_check = "0";
                    typ = "";
                    menu_name = "";
                }



            }
            catch (Exception ex)
            {
                addyn = "0";
                edityn = "0";
                deleteyn = "0";
                printyn = "0";
                viewyn = "0";
                qs_check = "0";
                typ = "";
                menu_name = "";
                ShowMsgBox.Show(ex.Message);
            }
            finally
            {
                mGlobal.conDatabase.Close();
            }


        }

        public static bool IsValidate(String date)
        {
            DateTime Temp;
            if (date != "") //check passed date parameter is not null then check for valid date
            {
                if (DateTime.TryParse(date, out Temp) == true && Temp.Hour == 0 && Temp.Minute == 0 && Temp.Second == 0 &&
                  Temp.Millisecond == 0 && Temp > DateTime.MinValue)
                    return true;
                else
                    return false;
            }
            else //passed date parameter is null then return true
            {
                return true;
            }
        }

        //internal static void ExecuteQuery(MySqlConnection conDatabase, string sql)
        //{
        //    throw new NotImplementedException();
        //}
        public static string ExecuteQuery(MySqlConnection connect, string query)
        {
            rvalue = 0.ToString();

            MySqlCommand command = new MySqlCommand(query, connect);
            try
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }

                connect.Open();
                MySqlDataReader reader = command.ExecuteReader();
                // Call Read before accessing data.
                return rvalue;
            }

            catch (Exception ex)
            {
                ShowMsgBox.Show(ex.Message.ToString());
            }

            finally
            {
                connect.Close();
            }
            return Reader.ToString();
        }


        public static void bindcheckbox(CheckBoxList chk1, string sql)
        {
            try

            {
                using (MySqlDataAdapter ad = new MySqlDataAdapter(sql, conDatabase))
                {

                    // MySqlDataAdapter ad = default(MySqlDataAdapter);

                    DataSet ds1 = new DataSet();
                    // ad = new MySqlDataAdapter(sql, conDatabase);
                    ad.Fill(ds1);
                    chk1.DataSource = ds1.Tables[0].DefaultView;
                    chk1.DataBind();
                }
            }

            catch (Exception ex)
            {
                ShowMsgBox.Show("Please try Again");

            }

        }
        public static void binddropdownlist(string sql, DropDownList dlist)
        {
            try
            {
                using (MySqlDataAdapter ad = new MySqlDataAdapter(sql, conDatabase))
                {

                    DataSet ds1 = new DataSet();
                    ad.Fill(ds1);
                    dlist.DataSource = ds1.Tables[0].DefaultView;
                    dlist.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("Please try Again");

            }

        }
        public static void bindRadiolist(string sql, RadioButtonList rlist)
        {
            try
            {
                using (MySqlDataAdapter ad = new MySqlDataAdapter(sql, conDatabase))
                {
                    //  MySqlDataAdapter ad = default(MySqlDataAdapter);
                    DataSet ds1 = new DataSet();
                    ad.Fill(ds1);
                    rlist.DataSource = ds1.Tables[0].DefaultView;
                    rlist.DataBind();
                }

            }

            catch (Exception ex)
            {
                ShowMsgBox.Show("Please try Again");

            }
        }


        public static string getResult(string cmd1)
        {
            string queryString = cmd1;
            rvalue = 0.ToString();
            using (MySqlConnection connectionS = new MySqlConnection(conDatabaseString))
            {
                connectionS.Open();
                using (MySqlCommand command = new MySqlCommand(queryString, connectionS))
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rvalue = reader[0].ToString();
                            }
                        }
                        return rvalue;
                    }
                    catch (Exception ex)
                    {
                        ShowMsgBox.Show("Please try Again ater some time");
                    }
                    finally
                    {
                    }
                return rvalue;
            }
        }


        public static string ExecuteQuery(string query)
        {
            // On Error GoTo ErrTrap
            rvalue = 0.ToString();
            using (MySqlConnection connectionS = new MySqlConnection(conDatabaseString))
            {
                connectionS.Open();


                using (MySqlCommand command = new MySqlCommand(query, connectionS))
                    command.ExecuteNonQuery();
                return rvalue;
            }
        }


        public static void bindataGrid(GridView gridview1, string sql)
        {
            using (MySqlConnection connectionS = new MySqlConnection(conDatabaseString))
            {
                connectionS.Open();
                using (MySqlDataAdapter ad = new MySqlDataAdapter(sql, conDatabaseSecond))
                {
                    using (DataSet ds1 = new DataSet())
                    {
                        ad.Fill(ds1);
                        gridview1.DataSource = ds1.Tables[0].DefaultView;
                        gridview1.DataBind();
                    }


                }
            }
        }

        public static void bindDetailsView(DetailsView detailsView1, string sql)
        {
            using (MySqlConnection connectionS = new MySqlConnection(conDatabaseString))
            {
                connectionS.Open();
                using (MySqlDataAdapter ad = new MySqlDataAdapter(sql, conDatabaseSecond))
                {
                    using (DataSet ds1 = new DataSet())
                    {
                        ad.Fill(ds1);
                        detailsView1.DataSource = ds1.Tables[0].DefaultView;
                        detailsView1.DataBind();
                    }


                }
            }
        }

    }


}