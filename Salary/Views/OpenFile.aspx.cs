using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Payroll.Views
{
    public partial class OpenFile : System.Web.UI.Page
    {
       // private ReportDocument rd;
        ReportDocument theReport = new ReportDocument();
        string RptGenerated_FileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateReport();
        }

        void OpenFile_Unload(object sender, EventArgs e)
        {
            //remember to clean up
            if (theReport != null)
            {
                theReport.Close();
                theReport.Dispose();
                Session["ReportName"] = null;
                GC.Collect();
            }
        }
        private void GenerateReport()
        {
            
            string ParamVal1 = null;
            string ParamName1 = null;

            string ParamVal2 = null;
            string ParamName2 = null;

            string ParamVal3 = null;
            string ParamName3 = null;

            string ParamVal4 = null;
            string ParamName4 = null;

            string ParamVal5 = null;
            string ParamName5 = null;

            //if (!(Session["NoOfParameters"].ToString() == "" || Session["NoOfParameters"] == null || Session["NoOfParameters"].ToString() == "0"))

             if ( ! (Session["NoOfParameters"] == null  ))
            {         
                if (Session["NoOfParameters"].ToString() == "1")
                {
                    ParamVal1 = Session["ParamVal1"].ToString();
                    ParamName1 = Session["ParamName1"].ToString();
                }
                else if (Session["NoOfParameters"].ToString() == "2")
                {
                    ParamVal1 = Session["ParamVal1"].ToString();
                    ParamName1 = Session["ParamName1"].ToString();

                    ParamVal2 = Session["ParamVal2"].ToString();
                    ParamName2 = Session["ParamName2"].ToString();
                }
                else if (Session["NoOfParameters"].ToString() == "3")
                {
                    ParamVal1 = Session["ParamVal1"].ToString();
                    ParamName1 = Session["ParamName1"].ToString();

                    ParamVal2 = Session["ParamVal2"].ToString();
                    ParamName2 = Session["ParamName2"].ToString();

                    ParamVal3 = Session["ParamVal3"].ToString();
                    ParamName3 = Session["ParamName3"].ToString();
                }
                else if (Session["NoOfParameters"].ToString() == "4")
                {
                    ParamVal1 = Session["ParamVal1"].ToString();
                    ParamName1 = Session["ParamName1"].ToString();

                    ParamVal2 = Session["ParamVal2"].ToString();
                    ParamName2 = Session["ParamName2"].ToString();

                    ParamVal3 = Session["ParamVal3"].ToString();
                    ParamName3 = Session["ParamName3"].ToString();

                    ParamVal4 = Session["ParamVal4"].ToString();
                    ParamName4 = Session["ParamName4"].ToString();
                }
                else if (Session["NoOfParameters"].ToString() == "5")
                {
                    ParamVal1 = Session["ParamVal1"].ToString();
                    ParamName1 = Session["ParamName1"].ToString();

                    ParamVal2 = Session["ParamVal2"].ToString();
                    ParamName2 = Session["ParamName2"].ToString();

                    ParamVal3 = Session["ParamVal3"].ToString();
                    ParamName3 = Session["ParamName3"].ToString();

                    ParamVal4 = Session["ParamVal4"].ToString();
                    ParamName4 = Session["ParamName4"].ToString();

                    ParamVal5 = Session["ParamVal5"].ToString();
                    ParamName5 = Session["ParamName5"].ToString();
                }
            }


            string reportname = null;
            reportname = Session["ReportName"].ToString();
            theReport.Load(Server.MapPath(reportname));

            if (!( Session["NoOfParameters"] == null ))
            {
                if (Session["NoOfParameters"].ToString() == "1")
                {
                    theReport.SetParameterValue(ParamName1, ParamVal1);
                }

                if (Session["NoOfParameters"].ToString() == "2")
                {
                    theReport.SetParameterValue(ParamName1, ParamVal1);
                    theReport.SetParameterValue(ParamName2, ParamVal2);
                }

                if (Session["NoOfParameters"].ToString() == "3")
                {
                    theReport.SetParameterValue(ParamName1, ParamVal1);
                    theReport.SetParameterValue(ParamName2, ParamVal2);
                    theReport.SetParameterValue(ParamName3, ParamVal3);
                }

                if (Session["NoOfParameters"].ToString() == "4")
                {
                    theReport.SetParameterValue(ParamName1, ParamVal1);
                    theReport.SetParameterValue(ParamName2, ParamVal2);
                    theReport.SetParameterValue(ParamName3, ParamVal3);
                    theReport.SetParameterValue(ParamName4, ParamVal4);
                }

                if (Session["NoOfParameters"].ToString() == "5")
                {
                    theReport.SetParameterValue(ParamName1, ParamVal1);
                    theReport.SetParameterValue(ParamName2, ParamVal2);
                    theReport.SetParameterValue(ParamName3, ParamVal3);
                    theReport.SetParameterValue(ParamName4, ParamVal4);
                    theReport.SetParameterValue(ParamName5, ParamVal5);
                }
            } 



            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.DatabaseName = "payroll";
            connectionInfo.ServerName = "payroll";

            //connectionInfo.UserID = "inhawksysadmin";
            //connectionInfo.Password = "inhawksysadmin^ivac";

            connectionInfo.UserID = "root";
            connectionInfo.Password = "mysql123";

            SetDBLogonForReport(connectionInfo, theReport);

            //"SERVER=45.34.14.88;port=8695;DATABASE=ksmb;UID=inhawksysadmin;PASSWORD=inhawksysadmin^ivac;"

            if (Session["SelectionFormula"].ToString() == "" || Session["SelectionFormula"] == null)
            {
            }
            else
            {
                theReport.RecordSelectionFormula = Session["SelectionFormula"].ToString();

            }

            
            RptGenerated_FileName = Session["ReportSaveAsName"].ToString();
 

            try
            {
              //  theReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, RptGenerated_FileName);
                CrystalReportViewer1.ReportSource = theReport;

                int exportFormatFlags = (int)(CrystalDecisions.Shared.ViewerExportFormats.PdfFormat | CrystalDecisions.Shared.ViewerExportFormats.ExcelFormat | CrystalDecisions.Shared.ViewerExportFormats.ExcelRecordFormat | CrystalDecisions.Shared.ViewerExportFormats.XLSXFormat);
                CrystalReportViewer1.AllowedExportFormats = exportFormatFlags;
            }
            catch (Exception ex)
            {
                ShowMsgBox.Show("Error in Report export Open File!.. Please Contact Support Desk " + ex.Message);
            }
            finally
            {
                     
            }
            
            


        }

        private void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument theReport)
        {
            Tables tables = theReport.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }
 
    }
}