<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Mis.aspx.cs" Inherits="BUOFC.Views.Mis" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .auto-style1 {
            color: #FF3300;
        }
        .auto-style2 {
            color: #FF0000;
        }
        .auto-style3 {
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <asp:Label ID="lblHeading" runat="server" Text="Admission Fee Structure"></asp:Label></h3>
                </div>


                <div class="panel-body">

                    <div id="DivSearch" runat="server" class="form-group  col-lg-12">
                        <table class="table  table-bordered table-hover table-responsive">
                            <tbody>
                                <tr>
                                    <td style="width: 10%">Select Dates</td>
                                    <td>Start Date <span class="auto-style1">*</span></td>
                                                    <td>
                                                        <BDP:BasicDatePicker ID="BDPStartDate" runat="server"/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="BDPStartDate" Display="Dynamic" ForeColor="Red" Text="" ValidationGroup="vgAdd" SetFocusOnError="true" />
                                                    </td>
                                                    <td>End Date<span class="auto-style1"> *</span></td>
                                                    <td>
                                                        <BDP:BasicDatePicker ID="BDPEndDate" runat="server"/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="BDPEndDate" Display="Dynamic" ForeColor="Red" Text="" ValidationGroup="vgAdd" SetFocusOnError="true" />
                                                    </td>
                                    <td style="width: 10%">
                                        <asp:Button ID="btnExport" runat="server" Text="Export" ToolTip="Export to Excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                                    </td>
                                  
                                    
                                    <td style="width: 10%">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" ToolTip="Clear All Record" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                    </td>

                                    <td style="width: 10%">
                                        <asp:Button ID="btnClose" runat="server" Text="Close" ToolTip="Go to Home Page" CssClass="btn btn-primary" OnClick="btnClose_Click" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>

 
 

                        

                    </div>
                </div>
            </div>
        </div>
    
</asp:Content>


