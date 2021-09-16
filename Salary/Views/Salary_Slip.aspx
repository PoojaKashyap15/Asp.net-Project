<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Salary_Slip.aspx.cs" EnableEventValidation="false" Inherits="Salary.Views.Salary_Slip" %>
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
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || (keyCode >= 96 && keyCode <= 105))
        }
    </script>
    <script type="text/javascript">
        var GridId = "<%=gvsalslip.ClientID%>";
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
        <div class="col-lg-10 container-padding ">
            <div class="col-lg-2"></div>

            <div class="col-lg-10">
                <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                              <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            
                            <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                        </asp:Panel>

                        <div class="col-lg-1"></div>

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
                                        <asp:DropDownList ID="ddlDept" runat="server" Width="80%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                  
                                          </td>

                                    <td style="width: 8%;">
                                        <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnshow_Click" />
                                          
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
                    <%--  <div class="clearfix"></div>--%>
                    <%--This code used for display employeewise salary details--%>
                    
                        <div class="col-lg-12 ">

                            <%--<h4><span class="label label-default">--%>
                                <%--<asp:Label ID="lblViewSalary" Text="Salarydetails" runat="server"></asp:Label></span></h4>--%>
                           
                                <asp:GridView ID="gvsalslip" runat="server" Width="100%" OnRowCommand="gvsalslip_RowCommand" AutoGenerateColumns="false" DataKeyNames="prs_emno" CssClass="table table-hover table-striped table-bordered "
                                    BorderColor="Black"  OnPageIndexChanging="gvsalslip_PageIndexChanging" AllowPaging="false"  ShowFooter="true" >
                                                                      
                                    <Columns>                                       
                                        <asp:BoundField DataField="prs_emno" ItemStyle-Width="5%" ItemStyle-Height="8px" HeaderText="Emp No"  />
                                        <asp:BoundField DataField="prs_name" ItemStyle-Width="30%" ItemStyle-Height="8px" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="PRG_YYMM" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Year/Month "   />
                                        <asp:BoundField DataField="PRG_AMNT" ItemStyle-Width="15%" ItemStyle-Height="8px" HeaderText="Net" />
                                        <asp:TemplateField HeaderText="Pay slip">
                                                    <ItemStyle Width="6%" />
                                                    <ItemTemplate>
                                                       <asp:ImageButton ID="btnpayslip" runat="server" CommandName="Printpayslip" ToolTip="Payslip" CommandArgument='<%#Eval("prs_emno") %>' ImageUrl="~/Attachment/DefaultPhoto/print.ico" Width="25" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkView" runat="server" AutoPostBack="false" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                              </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                   
                        <asp:CheckBox ID="chkSelecAll" runat="server" Text="Select All" AutoPostBack="true" OnCheckedChanged ="chkSelecAll_CheckedChanged" />
                    <div style="float: right">
                    <br />
                                   
                        <asp:Button ID="btnEmail" runat="server" Text="Email" CssClass="btn btn-info" OnClick="btnEmail_Click"/>
                                            
                         <asp:Button ID="btnCloseView" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnCloseView_Click" />
                         <%-- <asp:HiddenField ID="hfSl" runat="server" />--%>
                    </div>

                  
               
            </div>
        </div>
            </div>
    </div>
</asp:Content>
