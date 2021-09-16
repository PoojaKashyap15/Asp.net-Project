using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using prototype.App_Code;

namespace Salary
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string sql;
                  sql = "select  cod_code sl, cod_code code, cod_Desc name, cod_code as co_sl from psys_codes ";
                mGlobal.bindataGrid(ScrollT, sql);
            }
        }
        

        protected void ScrollT_PreRender(object sender, EventArgs e)
        {
            ScrollT.UseAccessibleHeader = true;
            ScrollT.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}