<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Bank_Report.aspx.cs" Inherits="Salary.Views.Bank_Report" %>
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
    <script type="text/javascript">
        var GridId = "<%=gvbankreport.ClientID%>";
        var ScrollHeight = "400";
        window.onload = function () {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>
    <div class="row">
        <div class="col-lg-12 container-padding ">
            <div class="col-lg-2"></div>

            <div class="col-lg-12">
                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                </div>
                <div class="panel-body">
                   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                    <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                    <div class="col-lg-12">
                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">
                                <tr>
                                    <td style="width: 10%;">Month - Year </td>
                                    <td style="width: 15%;">
                                        <asp:TextBox ID="txtmmyyyy" Width="95%" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>                                    
                                    <td>Department</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                    <td>Bank</td>
                                    <td>
                                        <asp:DropDownList ID="ddlbank" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                    </td>
                                    <td style="width: 6%;">
                                        <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnshow_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                    <td style="width: 6%;">
                                        <asp:Button ID="btnexcel" runat="server" Text="Excel" CssClass="btn btn-info" OnClick="btnexcel_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                    <td style="width: 6%;">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <br />
                    <br />
                    <%--  <div class="clearfix"></div>--%>
                    <%--This code used for display employeewise salary details--%>
                        <div class="col-lg-12 ">

                            <%--<h4><span class="label label-default">
                                <asp:Label ID="lblViewSalary" Text="Salarydetails" runat="server"></asp:Label></span></h4>--%>
                           
                                <asp:GridView ID="gvbankreport" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="prs_Emno" CssClass="table table-hover table-striped table-bordered "
                                    BorderColor="Black"  OnPageIndexChanging="gvSalary_PageIndexChanging" AllowPaging="false" ShowFooter="true" PageSize="20" HorizontalAlign="Center" >
                                    
                                    <Columns>
                                        <asp:BoundField DataField="prs_Emno" ItemStyle-Width="5%" ItemStyle-Height="8px" HeaderText="Emp No" />
                                        <asp:BoundField DataField="prs_name" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="prs_bkac" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Account No" />                                      
                                      <asp:BoundField DataField="prh_net" ItemStyle-Width="8%" ItemStyle-Height="8px" HeaderText="Net Salary" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="left" />
                                 
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                                <div style="float: right">
                                    <br />
                                    <asp:Button ID="btnCloseView" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnCloseView_Click" />
                                </div>
                        </div>

                                                                      

                   
            </div>
        </div>
    </div></div>
</asp:Content>



