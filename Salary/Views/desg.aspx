<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="desg.aspx.cs" Inherits="prototype.Views.desg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="col-lg-12 container-padding">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="lblHeading" runat="server">Designation</asp:Label></h3>
            </div>
            <div class="panel-body">

                <div id="DivSearch" runat="server" class="form-group  col-lg-12">
                    <table class="table  table-bordered table-hover table-responsive">
                        <tbody>
                            <tr>
                                <td style="width: 20%; vertical-align: middle;">Search by Designation</td>
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


                <div class="row">
                    <div class="col-lg-12 ">
                        <%------------------------------------------------- Detaisls Grid View -------------------------------------------------%>
                        <div class="table-responsive rounded-corners">
                            <%--PageSize="12"--%>
                            <asp:Panel ID="panleGridView" runat="server" BorderStyle="None" Height="360px" Visible="true" Width="100%" BackColor="White">

                                <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" DataKeyNames="dsg_code" CssClass="table  table-hover table-striped table-bordered table-responsive "
                                    BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="false" Height="20px" EmptyDataRowStyle-BackColor="LightCyan" EmptyDataText="Sorry No Data Found!!!." EmptyDataRowStyle-Font-Bold="true"
                                    EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeader="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("dsg_code") %>' ImageUrl="~/Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle Width="3%" Height="10px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("dsg_code") %>' ImageUrl="~/Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="20px" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Del">
                                            <ItemStyle Width="3%" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtn" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("dsg_code") %>' ImageUrl="~/Attachment/DefaultPhoto/delete.ico" runat="server" Width="25" Height="20px" />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="dsg_Code" ItemStyle-Width="0%" HeaderText="Designation_code" ItemStyle-Height="8px" Visible="false" />
                                        <asp:BoundField DataField="dsg_desc" ItemStyle-Width="40%" ItemStyle-Height="8px" HeaderText="Designation" />
                                        <asp:BoundField DataField="dsg_printorder" ItemStyle-Width="40%" ItemStyle-Height="8px" HeaderText="Print Order" />
                                    <asp:BoundField DataField="Status" ItemStyle-Width="10%" ItemStyle-Height="8px" HeaderText="Status" />
                                         </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
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
                                <td style="width: 25%">Designation</td>
                                <td style="width: 75%">

                                    <asp:TextBox ID="txtdsgdesc" Width="80%" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtdsgdesc" Display="None" SetFocusOnError="true" ErrorMessage="Enter Designation"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>

                                </td>
                            </tr>

                            <tr>
                                <td>Print Order </td>
                                <td>

                                    <asp:TextBox ID="txtprintorder" class="form-control" runat="server" Width="80%"></asp:TextBox>

                                </td>
                            </tr>

                            <tr>
                                <td>Active </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtnActive" runat="server" RepeatDirection="Horizontal">
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
                            <%-- <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
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
                                        <asp:BoundField DataField="dsg_code" HeaderText="Code" Visible="false" />
                                        <asp:BoundField DataField="dsg_desc" HeaderText="Designation" />
                                        <asp:BoundField DataField="dsg_printorder" HeaderText="Print Order" />
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
        <%--Body close Here--%>
    </div>




</asp:Content>
