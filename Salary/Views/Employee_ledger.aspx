<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Employee_ledger.aspx.cs" Inherits="Salary.Views.dpt_wise_Emp_dtl" %>
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
                        <asp:Label ID="lblHeading" runat="server">Department Wise Employee Details</asp:Label></h3>
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
                                    <td>Department</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="50%" DataTextField="name" DataValueField="sl" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td>Employee Name</td>
                                    <td>
                                        <asp:DropDownList ID="ddlname" runat="server" Width="50%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                </tr>
                               
                                
                                <tr>

                                    <td></td>

                                    <td>
                                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnprint_Click" ToolTip="Print The Record" ValidationGroup="vgAdd" />

                                        <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnclear_Click"  ToolTip="Clear all fields" ValidationGroup="vgAdd" />

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
