<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="RegSalCal.aspx.cs" Inherits="Salary.Views.RegSalCal" %>

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
        var GridId = "<%=gvSalary.ClientID%>";
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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" Font-Bold="true" Height ="15px" runat="server"></asp:Label>
                        <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                    <div class="col-lg-2"></div>
                    <div class="col-lg-6">
                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">
                                <tr>
                                    <td style="width: 25%;">Month - Year </td>
                                    <td style="width: 50%;">
                                        <asp:TextBox ID="txtmmyyyy" Width="50%" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnshow_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Salary Type </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSalType" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="rfvddlSalType" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlSalType" Display="None" ErrorMessage="Please Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlSalTypee" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlSalType"></ajaxToolkit:ValidatorCalloutExtender>
                                         

                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Button ID="btnCal" runat="server" Text="Calculation" CssClass="btn btn-danger" OnClick="btnCal_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Department</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                     <%--   <asp:RequiredFieldValidator ID="rfvddlDept" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlDept" Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlDept" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlDept"></ajaxToolkit:ValidatorCalloutExtender>--%>

                                    </td>
                                    <td>
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
                    
                        <div class="col-lg-12 ">

                            <h4><span class="label label-default">
                                <asp:Label ID="lblViewSalary" Text="Salarydetails" runat="server"></asp:Label></span></h4>
                            
                                <asp:GridView ID="gvSalary" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="prs_Emno" CssClass="table table-hover table-striped table-bordered "
                                    BorderColor="Black" OnRowCommand="gvSalary_RowCommand" OnPageIndexChanging="gvSalary_PageIndexChanging" AllowPaging="false" ShowFooter="true" PageSize="20">
                                    
                                    <Columns>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("prs_Emno") %>' ImageUrl="../Attachment/DefaultPhoto/View.ico" runat="server" Width="25px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="prs_Emno" ItemStyle-Width="7%" ItemStyle-Height="8px" HeaderText="Emp No" />
                                        <asp:BoundField DataField="prs_name" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="dept" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Department" />
                                        <asp:BoundField DataField="prh_yymm" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Year-Month" />
                                        <asp:BoundField DataField="saltype" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Salary Type" />
                                        <asp:BoundField DataField="GROSS" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Gross" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ded" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Deduction" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="NET" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Net Salary" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                   


                    <%--end of  employeewise salary details--%>

                    <%--Display individual Employee Salary Details--%>
                    <asp:Panel ID="panelViewEmployee" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                        <div class="col-lg-12">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-6">
                                <h4><span class="label label-default">
                                    <asp:Label ID="lblEmpName" Text="Employee No & Name : 1234526 - Vijay Shenoy" runat="server"></asp:Label></span></h4>
                                <div class="table-responsive  rounded-corners ">
                                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false" OnDataBound="gvEmployee_DataBound"
                                        OnRowCreated="gvEmployee_RowCreated" CssClass="table   table-bordered ">
                                        <Columns>
                                            <asp:BoundField DataField="PRG_EDCD" HeaderText="Code" ItemStyle-Width="60" />
                                            <asp:BoundField DataField="PRG_PORR" HeaderText="Type" ItemStyle-Width="60" />
                                            <asp:BoundField DataField="descs" HeaderText="Descriptions" ItemStyle-Width="210" />
                                            <asp:BoundField DataField="PRG_AMNT" HeaderText="Amount" ItemStyle-Width="60" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" />
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

                    </asp:Panel>


                    <%--end of  Display individual Employee Salary Details--%>
                
            </div>
        </div>
    </div>
</asp:Content>



