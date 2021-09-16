<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Narration.aspx.cs" Inherits="Salary.Views.Narration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:roundc runat="server" ID="webheaderschool" />


    <script type="text/javascript">
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 9 || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || (keyCode >= 96 && keyCode <= 105) || (keyCode >= 37 && keyCode <= 40))
        }
    </script>
     <script type="text/javascript">
        var GridId = "<%=gvCodes.ClientID%>";
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
                         
                        <div class="col-lg-12">
                            <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                                <table class="table table-bordered table-hover table-responsive">

                                    <tr>
                                        <td style="width: 18%;">Month - Year </td>
                                        <td style="width: 12%;">
                                            <asp:TextBox ID="txtmmyyyy" Width="95%" runat="server" class="form-control" placeholder="Click Here"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                            <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td style="width: 15%;">Select Department</td>
                                        <td style="width: 20%;">
                                            <asp:DropDownList ID="ddlName" runat="server" Width="95%" Height="32px" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDept" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlName" Display="None" ErrorMessage="Select Department Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlDept" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlDept"></ajaxToolkit:ValidatorCalloutExtender>

                                        </td>
                                        <td style="width: 15%;">Select Salary Type</td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddltype" runat="server" Width="95%" Height="32px" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddltype" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddltype" Display="None" ErrorMessage="Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddltype" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddltype"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>

                                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-info" OnClick="btnView_Click"
                                                ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnClose" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClose_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                            <div class="col-lg-12">
                                <br />
                                
                                    
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:GridView ID="gvCodes" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="PRH_EMNO" CssClass="table table-hover table-striped table-bordered "
                                                    BorderColor="Black" OnPageIndexChanging="gvCodes_PageIndexChanging" AllowPaging="false" PageSize="20" Visible="true">
                                                    
                                                    <Columns>
                                                        <asp:BoundField DataField="PRH_EMNO" ItemStyle-Width="5%" HeaderText="Emp No" />
                                                        <asp:BoundField DataField="prs_name" ItemStyle-Width="20" HeaderText="Emp Name" />
                                                        <asp:BoundField DataField="PRH_GROSS" ItemStyle-Width="5%" HeaderText="Gross" />
                                                        <asp:BoundField DataField="PRH_DED" ItemStyle-Width="5" HeaderText="Deduction" />
                                                        <asp:BoundField DataField="PRH_NET" ItemStyle-Width="5" HeaderText="Net" />
                                                        
                                                        <asp:TemplateField HeaderText="Narration">
                                                            <ItemStyle Width="55%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtnarration" runat="server" CssClass="form-control" Text='<%#Eval("PRH_NARR") %>' Enabled="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                    </Columns>
                                                </asp:GridView>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                    <div style="float: right">
                                        <br />
                                        <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnupdate_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                        <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                                    </div>
                               


                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


