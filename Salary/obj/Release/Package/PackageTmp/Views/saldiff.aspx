<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="saldiff.aspx.cs" Inherits="Salary.Views.saldiff" %>

<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:roundc runat="server" ID="webheaderschool" />

    <div class="row">
        <div class="col-lg-12 container-padding ">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                    <div class="col-lg-12">
                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">
                                <tr>
                                    <td style="width: 8%;">Prev Month </td>
                                    <td style="width: 11%;">
                                        <asp:TextBox ID="txtmmyyyyprev" Width="95%" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyyprev" TargetControlID="txtmmyyyyprev" />
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyyprev" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>

                                    <td style="width: 8%;">Curr Month </td>
                                    <td style="width: 11%;">
                                        <asp:TextBox ID="txtmmyyyycur" Width="95%" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyycur" TargetControlID="txtmmyyyycur" />
                                        <asp:RequiredFieldValidator ID="rfvtxtmmyyyycur" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyycur" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvtxtmmyyyycur"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>

                                    <td style="width: 8%;">Salary Type </td>
                                    <td style="width: 14%;">
                                        <asp:DropDownList ID="ddlSalType" runat="server" Width="95%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSalType" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlSalType" Display="None" ErrorMessage="Please Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlSalTypee" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlSalType"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td style="width: 8%;">Department</td>
                                    <td style="width: 14%;">
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                    </td>

                                    <td style="width: 6%;">
                                        <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnshow_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                    <td style="width: 6%;">
                                        <asp:Button ID="btnMIS" runat="server" Text="Excel" CssClass="btn btn-primary" OnClick="btnMIS_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                    <td style="width: 6%;">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <br />
                    <br />
                    <%--  <div class="clearfix"></div>--%>
                    <%--This code used for display employeewise salary details--%>
                    <asp:Panel ID="panelViewSalary" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                        <div class="col-lg-12 ">

                            <h4><span class="label label-default">
                                <asp:Label ID="lblViewSalary" Text="Salarydetails" runat="server"></asp:Label></span></h4>
                            <div class="table-responsive  rounded-corners">
                                <asp:GridView ID="gvSalary" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="prs_Emno"  CssClass="table table-hover table-striped table-bordered "
                                    OnPageIndexChanging="gvSalary_PageIndexChanging"   HeaderStyle-BackColor="#3AC0F2" OnRowDataBound="gvSalary_RowDataBound"
                                    HeaderStyle-ForeColor="Black" RowStyle-BackColor="White" AlternatingRowStyle-BackColor="White"
                                    RowStyle-ForeColor="#3A3A3A" PageSize="10">
                                    <PagerStyle ForeColor="black" HorizontalAlign="Center" BorderColor ="Black" Height="20" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                    <Columns>
                                        <asp:BoundField DataField="prs_Emno" ItemStyle-Width="7%" ItemStyle-Height="8px" HeaderText="Emp No" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="prs_name" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="dept" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Department" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="saltype" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Salary Type" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="cod_code" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Salary Code" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="cod_desc" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Descriptions" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="prev_month" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Prev Month" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="curr_month" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Curr Month" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Difference" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Difference" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </asp:Panel>


                    <%--end of  employeewise salary details--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>




