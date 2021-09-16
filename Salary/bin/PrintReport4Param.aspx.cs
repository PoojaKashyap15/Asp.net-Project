using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using prototype.App_Code;
using MySql.Data.MySqlClient;

public partial class PrintReport2Param : System.Web.UI.Page
{
    ReportDocument theReport = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {

        GenerateReport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        theReport.Close();
        theReport.Dispose();
        GC.Collect();
    }
    private void GenerateReport()
    {
        try
        {
            string ParamVal1 = null;
            string ParamName1 = null;
            string ParamVal2 = null;
            string ParamName2 = null;
            string ParamVal3 = null;
            string ParamName3 = null;
            string ParamVal4 = null;
            string ParamName4 = null;


            ParamVal1 = Session["ParamVal1"].ToString();
            ParamName1 = Session["ParamName1"].ToString();

            ParamVal2 = Session["ParamVal2"].ToString();
            ParamName2 = Session["ParamName2"].ToString();

            ParamVal3 = Session["ParamVal3"].ToString();
            ParamName3 = Session["ParamName3"].ToString();

            ParamVal4 = Session["ParamVal4"].ToString();
            ParamName4 = Session["ParamName4"].ToString();

            string reportname = null;
            reportname = Session["ReportName"].ToString();
            theReport.Load(Server.MapPath(reportname));


            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.DatabaseName = "payroll";
            connectionInfo.UserID = "root";
            connectionInfo.Password = "mysql123";
            SetDBLogonForReport(connectionInfo, theReport);



            if (Session["SelectionFormulaDpcd"].ToString() == "" || Session["SelectionFormulaDpcd"] == null)
            {
            }
            else
            {
                theReport.RecordSelectionFormula = Session["SelectionFormulaDpcd"].ToString();
            }

            theReport.SetParameterValue(ParamName1, ParamVal1);
            theReport.SetParameterValue(ParamName2, ParamVal2);
            theReport.SetParameterValue(ParamName3, ParamVal3);
            theReport.SetParameterValue(ParamName4, ParamVal4);



            theReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, ParamVal1 + ParamVal2 + ParamVal3 + ParamVal4);

        }
        catch (Exception ex)
        {

        }
        finally
        {

        }


    }
    private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
    {
        Tables tables = reportDocument.Database.Tables;
        foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
        {
            TableLogOnInfo tableLogonInfo = table.LogOnInfo;
            tableLogonInfo.ConnectionInfo = connectionInfo;
            table.ApplyLogOnInfo(tableLogonInfo);
        }
    }
}