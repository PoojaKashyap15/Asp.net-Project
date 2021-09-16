<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Year.aspx.cs" Inherits="prototype.Views.Year" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type = "text/javascript">
        function isNumeric(keyCode)
        {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || (keyCode >= 96 && keyCode <= 105))
        }
    </script>
    <div class="col-lg-12">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="panel panel-default"> 
            <div class="panel-heading">
                <h3 class="panel-title">Year Details </h3>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-lg-12 ">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


                                <%------------------------------------------------- Detaisls Grid View -------------------------------------------------%>
                                <div class="table-responsive">

                                    <asp:GridView ID="gvView" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="sl" CssClass="table table-hover table-striped table-bordered"
                                        BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="true" PageSize="8" >
                                        <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="20" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("sl") %>' ImageUrl="~/Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="16px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("sl") %>' ImageUrl="~/Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="16px"/>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <ItemStyle Width="5%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtn" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("sl") %>' ImageUrl="~/Attachment/DefaultPhoto/Delete.ico" runat="server" Width="25" Height="16px" />

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="sl" ItemStyle-Width="0%" HeaderText="Sl" ItemStyle-Height="5px" Visible="false" /> 
                                            <asp:BoundField DataField="start_date" ItemStyle-Width="20%" ItemStyle-Height="5px" HeaderText="Start Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" /> 
                                            <asp:BoundField DataField="end_date" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="End Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"/>
                                            <asp:BoundField DataField="descs" ItemStyle-Width="55%" ItemStyle-Height="8px" HeaderText="Description" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br>
                                </br>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                


                <div class="form-group col-lg-12">
                    <div style="float: right">
                        <asp:Button ID="btnAdd" runat="server" Text="Add New Record" ToolTip="Adding New Record" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                       &nbsp;
                       <asp:Button ID="btnPrint" runat="server" Text="Print" ToolTip ="Printing All Record" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                        &nbsp;
                        <asp:Button ID="btvClose" runat="server" Text="Close" CssClass="btn btn-primary" OnClick="btvClose_Click"/>  
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--------------------------------------------------------- Add Record Modal START------------------------------------------------------------------------------%>
    <div class="modal fade" id="addModal" tabindex="-1" role="dialog" aria-labelledby="grid_addModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="grid_addModal">Add Year Details</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAdd" runat="server">
                        <ContentTemplate>
                            <table class="table table-bordered table-hover table-responsive">

                                <tr>
                                     <td>Start Date </td>
                                     <td> 
                                        <div class="input-group">
                                              <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtStartDate"  Width ="45%" runat="server" class="form-control"  />                                             
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Attachment/DefaultPhoto/clock.png" CssClass="calendar"  />
                                               </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" PopupButtonID="Image1"  Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" Text="Enter Date" ControlToValidate="txtStartDate" 
                                    ValidationGroup="vgAdd" ForeColor="Red" />
                                    </td>
                                </tr>

                                 <tr>
                                     <td>End Date </td>
                                     <td> 
                                        <div class="input-group">
                                              <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtEndDate"  Width ="45%" runat="server" class="form-control"  />                                             
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Attachment/DefaultPhoto/clock.png" CssClass="calendar"  />
                                               </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="Image2"  Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtEndDate" runat="server" Text="Enter Date" ControlToValidate="txtEndDate" 
                                    ValidationGroup="vgAdd" ForeColor="Red" />
                                    </td>
                                </tr>

                                 <tr>
                                    <td>description<br/></td> 
                                     <td>
                                          <div class="input-group">
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-list-alt"></i></span>
                                    <asp:TextBox ID="txtDescs"  Width ="95%"  runat="server" class="form-control" ></asp:TextBox>
                                              </div>
                                    <asp:RequiredFieldValidator ID="rfvtxtDescs" runat="server"  ValidationGroup="vgAdd" ControlToValidate="txtDescs" Display="None" SetFocusOnError="true" ErrorMessage="Enter Description like 2015-2016"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtDescs" runat="server" TargetControlID="rfvtxtDescs"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>

    
    
                            </table>

                            
                            <div class="modal-footer">
                                
                                <asp:Button ID="BtnAddd" runat="server" Text="ADD" CssClass="btn btn-info"  OnClick="BtnAddd_Click" ValidationGroup="vgAdd" /> 
                                 <button class="btn btn-info" data-dismiss="modal" aria-hidden="true"><span class="glyphicon glyphicon-remove-circle"></span>&nbsp;Close</button>
                               
                                 </div>
                         </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="BtnAddd" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

    </div>
    <%--------------------------------------------------------- Detail Modal Start------------------------------------------------------------------------------%>
    <div class="modal fade" id="detailModal" tabindex="-1" role="dialog" aria-labelledby="grid_detailModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="grid_detailModal">Year Details</h4>
                </div>
                <div class="modal-body">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>



                            <asp:DetailsView ID="dvExam" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="False">
                                <Fields>

                                    <asp:BoundField DataField="sl" ItemStyle-Width="0%" HeaderText="Sl" ItemStyle-Height="8px" Visible="false" />
                                    <asp:BoundField DataField="start_date" ItemStyle-Width="55%" ItemStyle-Height="8px" HeaderText="Start Date." />
                                     <asp:BoundField DataField="end_date" ItemStyle-Width="55%" ItemStyle-Height="8px" HeaderText="End Date" />
                                     <asp:BoundField DataField="descs" ItemStyle-Width="55%" ItemStyle-Height="8px" HeaderText="Description" />
                                </Fields>
                            </asp:DetailsView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvView" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal"><span class="glyphicon glyphicon-remove-sign"></span>&nbsp;Close</button>
                </div>
            </div>
        </div>
    </div>
     


    <%--------------------------------------------------------- Edit Modal START------------------------------------------------------------------------------%>
    <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="grid_addModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="grid_editModal">Edit Year Details</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upEdit" runat="server">
                        <ContentTemplate>

                             <table class="table table-bordered table-hover table-responsive">

                               <tr>
                                     <td>Start Date </td>
                                     <td> 
                                        <div class="input-group">
                                              <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtStartDateEdit"  Width ="45%" runat="server" class="form-control"  />                                             
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Attachment/DefaultPhoto/clock.png" CssClass="calendar"  />
                                               </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtStartDateEdit" PopupButtonID="Image3"  Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvtxtStartDateEdit" runat="server" Text="Enter Date" ControlToValidate="txtStartDateEdit" 
                                    ValidationGroup="vgEdit" ForeColor="Red" />
                                    </td>
                                </tr>

                                 <tr>
                                     <td>End Date </td>
                                     <td> 
                                        <div class="input-group">
                                              <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                    <asp:TextBox ID="txtEndDateEdit"  Width ="45%" runat="server" class="form-control"  />                                             
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Attachment/DefaultPhoto/clock.png" CssClass="calendar"  />
                                               </div>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDateEdit" PopupButtonID="Image4"  Format="dd-MMM-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="Enter Date" ControlToValidate="txtEndDateEdit" 
                                    ValidationGroup="vgEdit" ForeColor="Red" />
                                    </td>
                                </tr>

                                  <tr>
                                    <td>description<br/></td> <td>
                                         <div class="input-group">
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-list-alt"></i></span>
                                    <asp:TextBox ID="txtDescsEdit"  Width ="95%"  runat="server" class="form-control"></asp:TextBox>
                                             </div>
                                    <asp:RequiredFieldValidator ID="rfvtxtDescsEdit" runat="server"  ValidationGroup="vgEdit" ControlToValidate="txtDescsEdit" Display="None" SetFocusOnError="true" ErrorMessage="Enter Descriptions"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtDescsEdit" runat="server" TargetControlID="rfvtxtDescsEdit"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>

                            </table>

                            <div class="modal-footer">
                                <asp:Label ID="lblsl" Visible="false" runat="server"></asp:Label>
                                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click"
                                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgEdit" />
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true"><span class="glyphicon glyphicon-remove-circle"></span>&nbsp;Close</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>        </div>

    </div>
    <!-- Edit Modal Ends here -->

    <!-- Delete Record Modal Starts here-->

    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="grid_addModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="grid_deleteModal">Delete Year Details</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upDel" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                Are you sure you want to delete the record?
                            <asp:HiddenField ID="hfSl" runat="server" />
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-info" OnClick="btnDelete_Click" />
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true"><span class="glyphicon glyphicon-remove-circle"></span>&nbsp;Cancel</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnDelete" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>

    </div>
    <!--Delete Record Modal Ends here -->

</asp:Content>
