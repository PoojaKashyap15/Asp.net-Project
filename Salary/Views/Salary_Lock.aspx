<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Salary_Lock.aspx.cs" Inherits="Salary.Views.Salary_Lock" %>

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
        <div class="col-lg-12 container-padding ">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="lblHeading" runat="server">Salary Lock</asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                    <div class="col-lg-2"></div>
                    <div class="col-lg-6">
                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">
                                <tr>
                                    <td style="width: 25%;">Month - Year </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox ID="txtmmyyyy" Width="50%" runat="server" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Salary Type </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSalType" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSalType" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlSalType" Display="None" ErrorMessage="Please Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlSalTypee" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlSalType"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Department</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td></td>

                                    <td>
                                        <asp:Button ID="btnlock" runat="server" Text="Salary Lock" CssClass="btn btn-primary" OnClick="btnlock_Click" ToolTip="enter the all fields Then click Salary Lock link!" ValidationGroup="vgAdd" />

                                        <asp:Button ID="btnunlock" runat="server" Text="Salary Unlock" CssClass="btn btn-primary" OnClick="btnunlock_Click" ToolTip="Unlock The all fields" ValidationGroup="vgAdd" />

                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>

                        </asp:Panel>

                    </div>
                    <br />
                    <br /> 
                </div>
            </div>
        </div>
    </div>
</asp:Content>
