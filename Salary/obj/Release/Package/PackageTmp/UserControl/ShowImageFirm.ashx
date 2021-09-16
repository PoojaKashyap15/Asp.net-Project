<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using Campus.App_Code;

public class Handler : IHttpHandler {
    MySqlConnection conDatabase;
    MySqlCommand cmd = new MySqlCommand();
    MySqlDataReader dReader;
    public void ProcessRequest (HttpContext context)
    {

        try
        {

            conDatabase = new MySqlConnection("SERVER=localhost;DATABASE=ksmb;UID=ksmb;PASSWORD=ksmb123;Allow Zero Datetime=true;convert zero datetime=True;Connect Timeout=300000; pooling='true'; Max Pool Size=2000;Min Pool Size=20");
            cmd.CommandText = "SELECT   sl,logo FROM co WHERE  sl =?ID  AND active_yn='1'";

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conDatabase;

            MySqlParameter ImageID = new MySqlParameter("?ID", System.Data.SqlDbType.Int);
            ImageID.Value = context.Request.QueryString["ID"];
            cmd.Parameters.Add(ImageID);
             conDatabase.Open();
           dReader = cmd.ExecuteReader();
            if(dReader.IsClosed  == true )
            {
               dReader.Read();
           }
            
            context.Response.BinaryWrite((byte[])dReader["logo"]);
            dReader.Close();
            conDatabase.Close();
        }
        catch(Exception ex)
        {
            ShowMsgBox.Show("error in settings! contact system admin");
        }
        finally
        {
            dReader.Close();
            conDatabase.Close();
        }

    }



    public bool IsReusable {
        get {
            return false;
        }
    }

}