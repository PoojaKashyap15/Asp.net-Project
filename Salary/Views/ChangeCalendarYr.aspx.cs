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
    public partial class ChangeCalendarYr : System.Web.UI.Page
    {
        string sql;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                  
                    if (Request.QueryString["qsc"] == null)
                    {
                        Response.Redirect("../Views/login.aspx");
                    }

                    if (Session["CoSl"] == null)
                    {
                        Response.Redirect("../Views/login.aspx");
                    }
                    
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
                // where co_sl = " + Session["CoSl"] + "
                sql = "SELECT sl, descs, DATE_FORMAT( start_date,'%d-%b-%Y')start_date, DATE_FORMAT( end_date,'%d-%b-%Y')end_date FROM yr   order by sl ";
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

        protected void gvView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
           
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName.Equals("Select"))
            {
                try
                {

                    int code = index;
                   
                    {
                        mGlobal.conDatabase.Close();
                    }
                    mGlobal.conDatabase.Open();

                    Session["YearSl"] = code ;

                    sql = "select descs from yr where sl='"+ code +"'";
                    string YrDescs = mGlobal.getResult(sql);

                    Session["YearDesc"] = YrDescs;
                    Response.Redirect("../Views/home.aspx");

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
        }
    }
}
