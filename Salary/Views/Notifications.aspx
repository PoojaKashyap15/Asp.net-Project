<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Notifications.aspx.cs" Inherits="Ksmb.Views.Notifications" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                </div>


                <div class="panel-body">
             <div id="DivSearch" runat="server" class="form-group  col-lg-12">
                        <table class="table  table-bordered table-hover table-responsive">
                            <tbody>
                                <tr>
                                    <td style="width: 20%">Search by Notification Subject</td>
                                    <td style="width: 49%">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>
                                            <asp:TextBox ID="txtSearch" Width="100%" CssClass=" form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td style="width: 7%">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search Record" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                    </td>
                                    <td style="width: 10%">

                                        <asp:Button ID="btnAdd" runat="server" Text="Add New Record"  CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                    </td>
                                    <td style="width: 6%">
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" ToolTip="Printing All Record"  CssClass="btn btn-primary"  OnClick="btnPrint_Click" />
                                    </td>
                                    <td style="width: 6%">
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" ToolTip="Clear All Record"  CssClass="btn btn-primary"  OnClick="btnClear_Click" />
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

                            <div class="row">
                                <div class="col-lg-12 ">
                                    <div class="table-responsive  rounded-corners">
                                        <asp:Panel ID="panleGridView" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                                            <asp:GridView ID="gvView" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="sl" CssClass="table table-hover table-striped table-bordered "
                                                BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="true" PageSize="15">
                                                <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="20" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemStyle Width="3%" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("sl") %>' ImageUrl="../Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemStyle Width="3%" Height="10px" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton2" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("sl") %>' ImageUrl="../Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="20px" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Del">
                                                        <ItemStyle Width="3%" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtn" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("sl") %>' ImageUrl="../Attachment/DefaultPhoto/Delete.ico" runat="server" Width="25" Height="20px" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="sl" ItemStyle-Width="0%" HeaderText="Sl" ItemStyle-Height="8px" Visible="false" />
                                                    <asp:BoundField DataField="Notification_Date" ItemStyle-Width="12%" ItemStyle-Height="8px" HeaderText="Start Date" />
                                                    <asp:BoundField DataField="noti_subject" ItemStyle-Width="46%" ItemStyle-Height="8px" HeaderText="Subject" />
                                                    <asp:BoundField DataField="nar" ItemStyle-Width="0%" HeaderText="nar" ItemStyle-Height="8px" Visible="false" />
                                                    <asp:BoundField DataField="noti_end_date" ItemStyle-Width="12%" ItemStyle-Height="8px" HeaderText="End Date" />

                                                </Columns>
                                            </asp:GridView>



                                        </asp:Panel>
                                    </div>
                                </div>

 
                    </div>


                    <div class="col-lg-2"></div>

                    <div class="col-lg-8">

                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">

                                <%--<tr>
                                 <td>Class<br/>
                                   <asp:DropDownList ID="DDLClass" CssClass="selectpicker form-control input-sm"  runat="server" Width ="50%" DataTextField="Text" DataValueField="ID"  ></asp:DropDownList>
                                </td>
                                </tr>--%>

                                 <tr>
                                            <td style="width:25%;">Branch</td>
                                                <td style="width:75%;">
                                                    &nbsp;</td>
                                           
                                        </tr>


                                <tr>
                                    <td>Notification Start Date</td>
                                     <td>
                                         <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtStartDate"  Width ="25%" runat="server" CssClass="form-control"  />
                                    <asp:Image ID="imgSD" runat="server" ImageUrl="~/Attachment/DefaultPhoto/clock.png" CssClass="calendar"  />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" PopupButtonID="imgSD"  Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" Text="Start Date Required" ControlToValidate="txtStartDate" 
                                    ValidationGroup="vgAdd" ForeColor="Red" />
                                             </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subject</td>
                                    <td>
                                        <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-list-alt"></i></span>
                                    <asp:TextBox ID="txtSubject"  Width ="95%" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtSubject" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtSubject" Display="None" SetFocusOnError="true" ErrorMessage="Enter notification Subject"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtSubject" runat="server" TargetControlID="rfvtxtSubject"></ajaxToolkit:ValidatorCalloutExtender>
                              </div>
                                </td>
                                </tr>

                                <tr>
                                    <td>Descriptions</td>
                                     <td>
                                         <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-list-alt"></i></span>
                                         <asp:TextBox ID="txtDescriptions" CssClass="form-control" class="form-control" runat="server" Width ="95%" Height="80px"  TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                             </td>
                                </tr>
                                <tr>
                                    <td>
                                        Attachment</td>
                                      <td>
                                        <asp:FileUpload ID="FileUploadAdd" runat="server" />
                                        <asp:HyperLink  Target ="_search" ID="HyperLinkedit" runat="server">Download</asp:HyperLink>
                                    </td>
                                </tr>

                                    <tr>
                                    <td>Notification End Date</td>
                                    <td>
                                        <div class="input-group">
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtEndDate" Width ="25%" runat="server" CssClass="form-control" />
                                    <asp:Image ID="ImageED" runat="server" ImageUrl="~/Attachment/DefaultPhoto/clock.png" CssClass="calendar"  />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="ImageED"  Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtEndDate" runat="server" Text="End Date is required" ControlToValidate="txtEndDate" 
                                    ValidationGroup="vgAdd" ForeColor="Red" />
                                            </div>
                                    </td>
                                </tr>
                                          

                            </table>
                            <div style="float: right">
                                <br />
                                <%--<asp:Label ID="lblSl" runat="server" Visible="false"></asp:Label>--%>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click"
                                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click"
                                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                            </div>
                        </asp:Panel>



                        <asp:Panel ID="panelVIEW" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                            <asp:DetailsView ID="dvView" runat="server" CssClass="table table-bordered table-hover" Font-Names="Arial" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="False" OnPageIndexChanging="dvView_PageIndexChanging">
                                <FieldHeaderStyle BackColor="LavenderBlush" Font-Bold="True" ForeColor="Black" Wrap="False" />
                                <Fields>
                                    <asp:BoundField DataField="sl" HeaderText="Code" Visible ="false" />
                                            <asp:BoundField DataField="Notification_Date" HeaderText="Notification Date" />
                                            <asp:BoundField DataField="noti_subject" HeaderText="Subject" />
                                            <asp:BoundField DataField="nar" HeaderText="Descriptions"  />
                                            <asp:HyperLinkField  HeaderText ="Attachment" DataNavigateUrlFields="att"
                                                DataNavigateUrlFormatString="~/Attachment/Notifications/{0}" 
                                                DataTextField="att"  DataTextFormatString="Download" Target="_search" />
                                            <asp:BoundField DataField="noti_end_date" HeaderText="Notification End Date" />
                                            
                                </Fields>
                            </asp:DetailsView>
                            <br />
                            <div style="float: right">
                                <asp:Button ID="Button3" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                            </div>


                        </asp:Panel>

                        <asp:Panel ID="panelDelete" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">
                                <tr>
                                    <td style="width: 70%">
                                        <h3>Do you want to Delete this Record.....? </h3>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="float: right">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-info" OnClick="btnDelete_Click" />
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:Content>