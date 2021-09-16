<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ReportView.aspx.cs" Inherits="Salary.Views.ReportView" %>

<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:roundc runat="server" ID="webheaderschool" />
    <script type="text/javascript">
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || (keyCode >= 96 && keyCode <= 105))
        }
    </script>

    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-body">
                    <div class="col-lg-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                            </div>
                            <div class="panel-body">
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:Panel ID="panelError" runat="server" BorderStyle="None" Visible="true" Width="100%" BackColor="White">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                                </asp:Panel>
                                <div class="col-lg-3"></div>
                                <div class="col-lg-8">
                                    <div class="col-lg-12">
                                        <h5>Select Year and  Month </h5>
                                        <asp:TextBox ID="txtmmyyyy" Width="75%" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-lg-12">
                                        <h5>Select Salary Type</h5>
                                        <asp:DropDownList ID="ddlSalType" runat="server" Width="75%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSalType" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlSalType" Display="None" ErrorMessage="Please Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlSalTypee" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlSalType"></ajaxToolkit:ValidatorCalloutExtender>
                                    </div>

                                    <div class="col-lg-12">
                                        <h5>Select Department</h5>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="75%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                    </div>

                                    <div class="col-lg-12">
                                        <h5>Select Report Name</h5>
                                        <asp:DropDownList ID="ddlReport" runat="server" Width="75%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvreport" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlReport" Display="None" ErrorMessage="Please Select Report Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomRight" runat="server" TargetControlID="rfvreport"></ajaxToolkit:ValidatorCalloutExtender>
                                    </div>


                                    <div class="col-lg-12">
                                        <br />
                                        <asp:Button ID="btnshow" runat="server" Text="View Report" CssClass="btn btn-primary" OnClick="btnshow_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </div>



                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="Label1" Text="Employee Ledger" runat="server"></asp:Label></h3>
                            </div>
                            <div class="panel-body">
                                <div class="col-lg-3"></div>
                                <div class="col-lg-8">
                                    <div class="col-lg-12">
                                        <h5>Select Employee Name</h5>
                                        <asp:DropDownList ID="ddlEmpName" runat="server" Width="75%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlEmpName" InitialValue="0" runat="server" ValidationGroup="vgLedger" ControlToValidate="ddlEmpName" Display="None" ErrorMessage="Please Select Employee Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlEmpName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-lg-12">
                                        <br />
                                        <asp:Button ID="btnLedgerView" runat="server" Text="Ledger View" CssClass="btn btn-primary" OnClick="btnLedgerView_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgLedger" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class="clearfix"></div>
                    <div style="float: right">
                        <br />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>





