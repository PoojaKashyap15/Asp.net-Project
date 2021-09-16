<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Location.aspx.cs" Inherits="Payroll.Views.Location" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 container-padding">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>      
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="lblHeading" runat="server">Location</asp:Label></h3>
            </div>

            <div class="panel-body">

                <div id="DivSearch" runat="server" class="form-group  col-lg-12">
                  <%--<div id="DivSearch" runat="server" class="form-group  col-lg-12 col-md-12 col-sm-12">--%>
                    <table class="table  table-bordered table-hover table-responsive">
                        <tbody>
                            <tr>
                                <td style="width: 20%; vertical-align:middle;">Search by Name or Descriptions</td>
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
                                <div class="table-responsive rounded-corners"> <%--PageSize="12"--%>
                                    <asp:Panel ID="panleGridView" runat="server" BorderStyle="None" Height="360px" Visible="true" Width="100%"  BackColor="White" >
                                   
                                    <asp:GridView ID="gvView" runat="server"  AutoGenerateColumns="false" Width="100%" DataKeyNames="LCN_CODE" CssClass="table  table-hover table-striped table-bordered table-responsive "
                                        BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="false" Height="20px" EmptyDataRowStyle-BackColor="LightCyan" EmptyDataText="Sorry No Data Found!!!." EmptyDataRowStyle-Font-Bold="true"
                                         EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeader="true" >
                                       <Columns>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("LCN_CODE") %>' ImageUrl="~/Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemStyle Width="3%" Height="10px" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("LCN_CODE") %>' ImageUrl="~/Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Del">
                                                <ItemStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtn" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("LCN_CODE") %>' ImageUrl="~/Attachment/DefaultPhoto/delete.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="LCN_CODE" ItemStyle-Width="20%" HeaderText="LCN_CODE" ItemStyle-Height="8px" Visible="false" />
                                            <asp:BoundField DataField="LCN_DESC" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="City Name" />
                                            <asp:BoundField DataField="LCN_CTCD" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="City Code" />
                                           <asp:BoundField DataField="LCN_CTCDHRA" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="HRA Code" />
                                             <asp:BoundField DataField="LCN_CTCDCCA" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="CCA Code" />
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
            <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White" >
                <table class="table table-bordered table-hover table-responsive">
                    <tr>
                        <td>City Name  </td>
                        <td>
                            
                                <asp:TextBox ID="txtdesc" Width="80%" class="form-control" runat="server" Height="40px"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtdesc" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtdesc" Display="None" SetFocusOnError="true" ErrorMessage="Enter DESC"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtdesc" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtdesc"></ajaxToolkit:ValidatorCalloutExtender>
                            
                        </td>
                    </tr>

                    <tr>
                        <td>City Code</td>
                        <td>
                            
                                <asp:TextBox ID="txtctcd" class="form-control" runat="server" Width="80%" ></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>HRA Code</td>
                        <td>
                            
                                <asp:TextBox ID="txtctcdhra" class="form-control" runat="server" Width="80%"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>CCA Code</td>
                        <td>
                            
                           <asp:TextBox ID="txtctcdcca" class="form-control" runat="server" Width="80%" ></asp:TextBox>
                            
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
                                <asp:BoundField DataField="LCN_CODE" HeaderText="LCN_CODE" Visible="false"/>
                                <asp:BoundField DataField="LCN_DESC" HeaderText="City Name" />
                                <asp:BoundField DataField="LCN_CTCD" HeaderText="City Code" />
                                <asp:BoundField DataField="LCN_CTCDHRA" HeaderText="HRA Code" />
                                <asp:BoundField DataField="LCN_CTCDCCA" HeaderText="CCA Code" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                

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
</div><%-- between row ende here--%>
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
    




</asp:Content>
