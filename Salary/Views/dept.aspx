<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="dept.aspx.cs" Inherits="prototype.Views.dept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var GridId = "<%=gvView.ClientID%>";
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
    <div class="col-lg-12 container-padding">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="lblHeading" runat="server">Department</asp:Label></h3>
            </div>
            <div class="panel-body">

                <div id="DivSearch" runat="server" class="form-group  col-lg-12">
                    <table class="table  table-bordered table-hover table-responsive">
                        <tbody>
                            <tr>
                                <td style="width: 20%; vertical-align: middle;">Search by Department Name </td>
                                <td style="width: 51%">

                                    <asp:TextBox ID="txtSearch" Width="100%" CssClass=" form-control" runat="server" placeholder="Search by Name or Descriptions"></asp:TextBox>

                                </td>
                                <td style="width: 7%">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search Record" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" ToolTip="Adding New Record" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                </td>
                                <td style="width: 6%">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" ToolTip="Printing All Record" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                                </td>
                                <td style="width: 6%">
                                    <asp:Button ID="btncloseMain" runat="server" Text="Close" ToolTip="Close" CssClass="btn btn-primary" OnClick="btncloseMain_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                </asp:Panel>

                <%--<asp:Panel ID="panleGridView" runat="server" BorderStyle="None" Height="360px" Visible="true" Width="100%"  BackColor="White" >--%>
                <div class="row">
                    <div class="col-lg-12">
                        <%------------------------------------------------- Detaisls Grid View -------------------------------------------------%>
                        
                            <%--PageSize="12"--%>
                            

                                <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" DataKeyNames="dpt_code" CssClass="table  table-hover table-striped table-bordered table-responsive "
                                    BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="false" Height="20px" EmptyDataRowStyle-BackColor="LightCyan" EmptyDataText="Sorry No Data Found!!!." EmptyDataRowStyle-Font-Bold="true"
                                    EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeader="true" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("dpt_code") %>' ImageUrl="~/Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle Width="3%" Height="10px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("dpt_code") %>' ImageUrl="~/Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="20px" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Del">
                                            <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtn" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("dpt_code") %>' ImageUrl="~/Attachment/DefaultPhoto/delete.ico" runat="server" Width="25" Height="20px" />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="dpt_code" ItemStyle-Width="0%" HeaderText="Designation_code" ItemStyle-Height="8px" Visible="false" />
                                        <asp:BoundField DataField="dpt_desc" ItemStyle-Width="28%" ItemStyle-Height="8px" HeaderText="Department" />
                                        <asp:BoundField DataField="dpt_hdr1" ItemStyle-Width="28%" ItemStyle-Height="8px" HeaderText="Header1" />
                                        <asp:BoundField DataField="dpt_hdr2" ItemStyle-Width="28%" ItemStyle-Height="8px" HeaderText="Header2" />
                                        <asp:BoundField DataField="Status" ItemStyle-Width="9%" ItemStyle-Height="8px" HeaderText="Status" />

                                    </Columns>
                                </asp:GridView>
                            
                        </div>
                   
                </div>
            </div>




            <%--------------------------------------------------------- Add Record Modal START------------------------------------------------------------------------------%>
            <div class="row">
                <div class="col-lg-2 "></div>
                <div class="col-lg-8 ">
                    <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White" Style="align-content: center;">
                        <table class="table table-bordered table-hover table-responsive">

                            <tr>
                                <td>Department Name</td>
                                <td>

                                    <asp:TextBox ID="txtdesc" class="form-control" runat="server" Width="70%" TextMode="MultiLine" ></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td>Header1</td>
                                <td>

                                    <asp:TextBox ID="txthdr1" class="form-control" runat="server" Width="70%"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td>Header2</td>
                                <td>

                                    <asp:TextBox ID="txthdr2" class="form-control" runat="server" Width="70%"></asp:TextBox>

                                </td>
                            </tr>                                                       
                            <tr>
                                <td>Salary Parameter</td>
                                  <td>
                                        <asp:DropDownList ID="ddlpayid" runat="server" DataValueField="id" CssClass="form-control" DataTextField="name" Width="70%"></asp:DropDownList>
                                   </td>
                            </tr>
                            <tr>
                                <td>Active</td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                        <asp:ListItem Value="0">NO</asp:ListItem>

                                    </asp:RadioButtonList>
                                </td>
                            </tr>



                        </table>

                        <div style="float: right">
                            <asp:Label ID="lblsl" Visible="true" runat="server"></asp:Label>
                            <asp:Button ID="btnAddRecord" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnAddRecord_Click"
                                ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click"
                                ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                            <asp:HiddenField ID="hfSl" runat="server" />
                        </div>

                    </asp:Panel>

                    <%--//------------------ View Recod -------------------//--%>
                    <asp:Panel ID="panelVIEW" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                        <div class="row">

                            <div class="col-lg-12 col-md-12 col-sm-12">

                                <asp:DetailsView ID="dvLookup" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="False">
                                    <Fields>
                                        <asp:BoundField DataField="dpt_code" HeaderText="Dept Code" Visible="false" />
                                        <asp:BoundField DataField="dpt_desc" HeaderText="Department Name" />
                                        <asp:BoundField DataField="dpt_hdr1" HeaderText="Header1" />
                                        <asp:BoundField DataField="dpt_hdr2" HeaderText="Header2" />
                                        <asp:BoundField DataField="dpt_payid"  HeaderText="Pay Id" />
                                        <asp:BoundField DataField="Status" HeaderText="status" />

                                    </Fields>
                                </asp:DetailsView>
                                <div style="float: right">
                                    <asp:Button ID="Button3" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <div class="col-lg-2 "></div>
            </div>
            <%-- between row ende here--%>

            <%-------------------- Panel Delete ---------------------------%>
            <asp:Panel ID="panelDelete" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                <table class="table table-bordered table-hover table-responsive">
                    <tr>
                        <td style="width: 70%">Do you want to Delete this Record..?
                    <br />
                            <br />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-info" OnClick="btnDelete_Click" />
                            <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            </div>



        </div>
        <%--Body close Here--%>
 





</asp:Content>
