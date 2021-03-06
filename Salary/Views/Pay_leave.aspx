<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Pay_leave.aspx.cs" Inherits="Salary.Views.Pay_leave" %>

<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:roundc runat="server" ID="webheaderschool" />
    <script type="text/javascript">
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 9 || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 38 || keyCode == 40 || keyCode == 39 || (keyCode >= 96 && keyCode <= 105))
        }

    </script>
    <script type="text/javascript">
        var GridId = "<%=gvleave.ClientID%>";
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
            <div class="col-lg-1"></div>

            <div class="col-lg-10">
                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="60%" Visible="true" Width="100%" BackColor="White">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                            <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                        </asp:Panel>

                        <div class="col-lg-1"></div>

                        <div class="col-lg-12">
                            <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                                <table class="table table-bordered table-hover table-responsive">
                                    <tr>
                                        <td style="width: 5%;">Year </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false" CssClass="form-control"></asp:DropDownList>
                                        </td>

                                        <td style="width: 8%;">Department</td>
                                        <td style="width: 32%;">
                                            <asp:DropDownList ID="ddlDept" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDept" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlDept" Display="None" ErrorMessage="Please Department" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlDept" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlDept"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>

                                        <td style="width: 8%;">
                                            <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnshow_Click"
                                                ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                        </td>
                                        <td style="width: 16%;">
                                            <asp:Button ID="bntNewEmp" runat="server" Text="Add Employee" CssClass="btn btn-info" OnClick="bntNewEmp_Click"
                                                ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                        </td>
                                        <td style="width: 8%;">
                                            <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-info" OnClick="btnprint_Click"
                                                ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                        </td>
                                        <td style="width: 8%;">
                                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                        <br />
                        <br />
                        <br />

                        <div class="col-lg-12">
                            <asp:GridView ID="gvleave" runat="server" DataKeyNames="lev_emno" AutoGenerateColumns="false"  CssClass="table  table-hover table-striped table-bordered table-responsive "
                                BorderColor="Black" AllowPaging="false" Height="20px" EmptyDataRowStyle-BackColor="LightCyan" EmptyDataText="Sorry No Data Found!!!." EmptyDataRowStyle-Font-Bold="true"
                                EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeader="true" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="lev_emno" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Emp No" />
                                    <asp:BoundField DataField="prs_name" ItemStyle-Width="42%" ItemStyle-Height="8px" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="LEV_CLOB" ItemStyle-Width="8%" ItemStyle-Height="8px" HeaderText=" CL OB" />
                                    <asp:BoundField DataField="LEV_CLCB" ItemStyle-Width="8%" ItemStyle-Height="8px" HeaderText=" CL CB" />
                                    <asp:TemplateField HeaderText=" CL ADD" ControlStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtcladd" runat="server" Text='<%#Eval("LEV_CLADD") %>' Style="width: 100%;"
                                                onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LEV_ELOB" ItemStyle-Width="8%" ItemStyle-Height="8px" HeaderText=" EL OB" />
                                    <asp:BoundField DataField="LEV_ELCB" ItemStyle-Width="8%" ItemStyle-Height="8px" HeaderText=" EL CB" />


                                    <asp:TemplateField HeaderText=" EL ADD" ControlStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txteladd" runat="server" Text='<%#Eval("LEV_ELADD") %>' Style="width: 100%;"
                                                onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div style="float: right">
                                <br />

                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click"
                                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                <asp:Button ID="btnCloseView" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnCloseView_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
