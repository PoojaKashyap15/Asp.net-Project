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
    public partial class SubSalCal : System.Web.UI.Page
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
                    panelViewSalary.Visible = false;

                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }

        protected void BindGridview()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("rowid", typeof(int));
                dt.Columns.Add("Descriptions", typeof(string));
                dt.Columns.Add("type", typeof(string));
                dt.Columns.Add("Amount", typeof(string));
                DataRow dr = dt.NewRow();
                dr["rowid"] = 1;
                dr["Descriptions"] = string.Empty;
                dr["type"] = string.Empty;
                dr["Amount"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["Curtbl"] = dt;
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
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
        private void AddNewRow()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["Curtbl"] != null)
                {
                    DataTable dt = (DataTable)ViewState["Curtbl"];
                    DataRow drCurrentRow = null;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dt.Rows.Count; i++)
                        {
                            DropDownList ddlPayCodes = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("ddlPayCodes");
                            TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtAmount");
                            TextBox txttype = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txttype");
                            drCurrentRow = dt.NewRow();
                            drCurrentRow["rowid"] = i + 1;
                            dt.Rows[i - 1]["Descriptions"] = ddlPayCodes.SelectedValue;
                            dt.Rows[i - 1]["type"] = txttype.Text;
                            dt.Rows[i - 1]["Amount"] = txtAmount.Text;
                            rowIndex++;

                        }
                        dt.Rows.Add(drCurrentRow);
                        ViewState["Curtbl"] = dt;
                        gvDetails.DataSource = dt;
                        gvDetails.DataBind();

                    }
                }
                else
                {
                    Response.Write("ViewState Value is Null");
                }
                SetOldData();
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
        private void SetOldData()
        {
            try
            {

                int rowIndex = 0;
                if (ViewState["Curtbl"] != null)
                {
                    DataTable dt = (DataTable)ViewState["Curtbl"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlPayCodes = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("ddlPayCodes");
                            TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtAmount");
                            TextBox txttype = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txttype");
                            ddlPayCodes.SelectedValue = dt.Rows[i]["Descriptions"].ToString();
                            txtAmount.Text = dt.Rows[i]["Amount"].ToString();
                            txttype.Text = dt.Rows[i]["type"].ToString();
                            rowIndex++;
                        }
                    }
                }
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
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (ViewState["Curtbl"] != null)
                {
                    DataTable dt = (DataTable)ViewState["Curtbl"];
                    DataRow drCurrentRow = null;
                    int rowIndex = Convert.ToInt32(e.RowIndex);
                    if (dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowIndex]);
                        drCurrentRow = dt.NewRow();
                        ViewState["Curtbl"] = dt;
                        gvDetails.DataSource = dt;
                        gvDetails.DataBind();


                        for (int i = 0; i < gvDetails.Rows.Count - 1; i++)
                        {
                            gvDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        }

                        SetOldData();
                    }
                }
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

        protected void Binddropdown()
        {
            try
            {
                sql = " SELECT prs_emno AS sl,cast(concat(prs_name,'---',prs_emno) as char) as name  FROM  pay_personnel  where active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlEmployee);
                ddlEmployee.Items.Insert(0, new ListItem("-- Select Employee --", "0"));

                sql = "select sty_name as name,sty_id as sl from psys_saltype where active_yn = 1  and regular_yn = 0 and co_sl = " + Session["CoSl"] + "";
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


        protected void btnshow_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                BindGridview();
                //sql = "SELECT prg_edcd as rowid, prg_edcd,prg_porr,prg_amnt FROM pay_reg where prg_edcd < 500 and co_sl='" + Session["CoSl"] + "' AND  prg_YID =  " + Session["YearSl"] + " " +
                //         " and prg_rlcd = '" + ddlSalType.SelectedValue + "' and prg_YYMM = '" + yymm + "' AND " +
                //         " prg_emno = " + ddlEmployee.SelectedValue + " ";

                //mGlobal.bindataGrid(gvDetails, sql);

                panelAddEdit.Visible = true;
                panelViewSalary.Visible = true;
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            txtmmyyyy.Text = "";
            txtGross.Text = "";
            txtded.Text = "";
            txtNet.Text = "";

            ddlEmployee.SelectedValue = "0".ToString();

            DataTable ds = new DataTable();
            ds = null;
            panelViewSalary.Visible = false;

        }

        protected void btnCloseView_Click(object sender, EventArgs e)
        {
            panelAddEdit.Visible = true;
            panelViewSalary.Visible = true;
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlPayCodes = (e.Row.FindControl("ddlPayCodes") as DropDownList);

                sql = " SELECT  COD_CODE AS sl,COD_DESC AS NAME   FROM  psys_codes   where COD_CODE < 500 and active_yn = 1  and co_sl = " + Session["CoSl"] + "";
                mGlobal.binddropdownlist(sql, ddlPayCodes);

                //Add Default Item in the DropDownList
                ddlPayCodes.Items.Insert(0, new ListItem("Please select"));
            }
        }

        protected void ddlPayCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                DropDownList txt = sender as DropDownList;

                GridViewRow row = txt.NamingContainer as GridViewRow;
                int rowindex = row.RowIndex;

                DropDownList ddlPayCodes = (DropDownList)gvDetails.Rows[rowindex].Cells[1].FindControl("ddlPayCodes");
                TextBox txttype = (TextBox)gvDetails.Rows[rowindex].Cells[2].FindControl("txttype");

                txttype.Text = mGlobal.getResult(" SELECT   COD_TYPE    FROM  psys_codes  WHERE COD_CODE = " + ddlPayCodes.SelectedValue + " and co_Sl =" + Session["CoSl"] + "");

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

        private void gross_ded_net()
        {
            Int64 gross = 0;
            Int64 ded = 0;
            Int64 net = 0;
            lblMsg.Text = "";

            int rowIndex = 0;
            try
            {
                lblMsg.Text = "";

                if (ViewState["Curtbl"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["Curtbl"];
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList ddlPayCodes = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("ddlPayCodes");
                            TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtAmount");
                            TextBox txttype = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txttype");

                            if (txtAmount.Text != "")
                            {
                                if (txttype.Text == "P".ToString())
                                {
                                    gross = gross + Convert.ToInt16(txtAmount.Text);
                                }
                                else
                                {
                                    ded = ded + +Convert.ToInt16(txtAmount.Text);
                                }
                            }
                            rowIndex++;
                        }
                        net = gross - ded;
                        txtGross.Text = Convert.ToString(gross);
                        txtded.Text = Convert.ToString(ded);
                        txtNet.Text = Convert.ToString(net);
                    }

                }
                else
                {
                    Response.Write("ViewState is null");
                    return;
                }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;
            int status = 0;
            int rowIndex = 0;
            string empCode = "";
            string dpcd = "";
            try
            {
                string yymm = Convert.ToDateTime(txtmmyyyy.Text).ToString("yyyyMM");

                sql = "SELECT IFNULL(SUM(IFNULL(prh_brno,0)),0) FROM pay_reg_hdr WHERE prh_yymm = '" + yymm + "' " +
                   " AND  co_sl = '" + Session["CoSl"].ToString() + "' AND prh_yid = '" + Session["YearSl"].ToString() + "' " +
                   " prh_rlcd = " + ddlSalType.SelectedValue;
                string lock_yn = mGlobal.getResult(sql);

                if (Convert.ToDouble(lock_yn) > 0)
                {
                    lblMsg.Text = "Sorry!.. This month locked..you cannot calculate salary";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                gross_ded_net();
                if (ViewState["Curtbl"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["Curtbl"];
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }
                    mGlobal.conDatabase.Open();
                    MySqlCommand cmd = new MySqlCommand();

                    // if already salary exists or not
                    sql = "SELECT count(*) FROM pay_reg where co_sl='" + Session["CoSl"] + "' AND  prg_YID =  " + Session["YearSl"] + " " +
                       " and prg_rlcd = '" + ddlSalType.SelectedValue + "' and prg_YYMM = '" + yymm + "' AND " +
                       " prg_emno = " + ddlEmployee.SelectedValue + " and ifnull(prg_brno,0) > 0 ";
                    empCode = mGlobal.getResult(sql);
                    if (empCode == "1".ToString())
                    {
                        ShowMsgBox.Show(" Sorry you cannot Save Salary Data!.. please check menu view salary details  !....");
                        return;
                    }
                    else
                    {
                        cmd.CommandText = " delete FROM pay_reg where co_sl = '" + Session["CoSl"] + "' AND  prg_YID = " + Session["YearSl"] + " " +
                       " and prg_rlcd = '" + ddlSalType.SelectedValue + "' and prg_YYMM = '" + yymm + "' AND " +
                       " prg_emno = " + ddlEmployee.SelectedValue + " and ifnull(prg_brno,0) = 0 ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;

                        cmd.CommandText = " delete FROM pay_reg_hdr where co_sl = '" + Session["CoSl"] + "' AND  prh_YID = " + Session["YearSl"] + " " +
                      " and prh_rlcd = '" + ddlSalType.SelectedValue + "' and prh_YYMM = '" + yymm + "' AND " +
                      " prh_emno = " + ddlEmployee.SelectedValue + " and ifnull(prh_brno,0) = 0 ";

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = mGlobal.conDatabase;
                        IsAdded = cmd.ExecuteNonQuery() > 0;

                    }

                    // end of salary checking

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            DropDownList ddlPayCodes = (DropDownList)gvDetails.Rows[rowIndex].Cells[1].FindControl("ddlPayCodes");
                            TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtAmount");
                            TextBox txttype = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txttype");

                            if (txtAmount.Text != "")
                            {
                                cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson) " +
                                       " VALUES(" + ddlEmployee.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                                       "  " + ddlPayCodes.SelectedValue + ", '" + txttype.Text + "', '" + txtAmount.Text + "', " +
                                       " '" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate()) ";

                                cmd.CommandType = System.Data.CommandType.Text;
                                cmd.Connection = mGlobal.conDatabase;
                                IsAdded = cmd.ExecuteNonQuery() > 0;
                                status = 1;
                            }
                            rowIndex++;
                        }
                        if (status == 1)
                        {
                            // gross insert
                            cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson) " +
                                       " VALUES(" + ddlEmployee.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                                       "  500, 'P', '" + txtGross.Text + "','" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate()) ";

                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = mGlobal.conDatabase;
                            IsAdded = cmd.ExecuteNonQuery() > 0;

                            // ded insert
                            cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson) " +
                                      " VALUES(" + ddlEmployee.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                                      "  900, 'R', '" + txtded.Text + "','" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate()) ";

                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = mGlobal.conDatabase;
                            IsAdded = cmd.ExecuteNonQuery() > 0;

                            // net insert
                            cmd.CommandText = " INSERT INTO pay_reg(PRG_EMNO,PRG_YYMM,PRG_RLCD,PRG_EDCD,PRG_PORR,PRG_AMNT,CO_SL,PRG_YID,prg_insby,prg_inson) " +
                                      " VALUES(" + ddlEmployee.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ", " +
                                      "  999, 'N', '" + txtNet.Text + "','" + Session["CoSl"] + "'," + Session["YearSl"] + ",'" + Session["LoginName"] + "',sysdate()) ";

                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = mGlobal.conDatabase;
                            IsAdded = cmd.ExecuteNonQuery() > 0;

                            // pay_reg_hdr insert
                            sql = "SELECT prs_dpcd FROM pay_personnel  where prs_emno = " + ddlEmployee.SelectedValue + " and  co_sl ='" + Session["CoSl"] + "' ";
                            dpcd = mGlobal.getResult(sql);

                            cmd.CommandText = "INSERT INTO pay_reg_hdr(PRH_EMNO,PRH_YYMM,PRH_RLCD,PRH_LOCD,PRH_DPCD,PRH_YID,CO_SL,PRH_GROSS,PRH_DED,PRH_NET) " +
                                      " VALUES(" + ddlEmployee.SelectedValue + ",'" + yymm.ToString() + "', " + ddlSalType.SelectedValue + ",1, ' " + dpcd + "', " +
                                      "   " + Session["YearSl"] + ",'" + Session["CoSl"] + "','" + txtGross.Text + "','" + txtded.Text + "','" + txtNet.Text + "') ";

                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = mGlobal.conDatabase;
                            IsAdded = cmd.ExecuteNonQuery() > 0;

                            ShowMsgBox.Show("Salary Saved Sucessfully !....");
                        }
                    }

                }
                else
                {
                    Response.Write("ViewState is null");
                    return;
                }
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

        protected void btnCal_Click(object sender, EventArgs e)
        {
            gross_ded_net();
        }
    }
}