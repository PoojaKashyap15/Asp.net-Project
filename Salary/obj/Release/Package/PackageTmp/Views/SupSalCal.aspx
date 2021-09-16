<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SupSalCal.aspx.cs" Inherits="Salary.Views.SubSalCal" %>
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
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                    <div class="col-lg-2"></div>
                    <div class="col-lg-7">
                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                            <table class="table table-bordered table-hover table-responsive">
                                <tr>
                                    <td style="width: 20%;">Month - Year </td>
                                    <td style="width: 20%;">
                                        <asp:TextBox ID="txtmmyyyy" Width="90%" runat="server"  AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                        <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td style="width: 20%;">Salary Type </td>
                                    <td style="width: 40%;">
                                        <asp:DropDownList ID="ddlSalType" runat="server" Width="90%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSalType" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlSalType" Display="None" ErrorMessage="Please Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlSalTypee" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlSalType"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnshow_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">Employee Name</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlEmployee" runat="server" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDept" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlEmployee" Display="None" ErrorMessage="Please Select Employee" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlDept" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlDept"></ajaxToolkit:ValidatorCalloutExtender>
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
                    <div class="clearfix"></div>
                    <%--This code used for display employeewise salary details--%>
                    <asp:Panel ID="panelViewSalary" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                        <div class="col-lg-12 ">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-6">
                                <h4><span class="label label-default">
                                    <asp:Label ID="lblViewSalary" Text="Enter Salary Details" runat="server"></asp:Label></span></h4>
                               
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                 <asp:GridView runat="server" ID="gvDetails" ShowFooter="true" CssClass="table table-hover table-striped table-bordered " AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                    OnRowDeleting="gvDetails_RowDeleting" OnRowDataBound="gvDetails_RowDataBound">
                                    <HeaderStyle CssClass="headerstyle" />
                                    <Columns>
                                        <asp:BoundField DataField="rowid" HeaderText="SlNo" ReadOnly="true" ItemStyle-Width="10%" />
                                        <asp:TemplateField HeaderText="Descriptions">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlPayCodes" runat="server" DataTextField="name" DataValueField="sl"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlPayCodes_SelectedIndexChanged"  AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txttype" runat="server" Width="100%" CssClass="form-control"   Enabled="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Width="100%"  CssClass="form-control" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="true" />
                                    </Columns>
                                </asp:GridView>
                                                 </ContentTemplate>
                               
                                        </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="clearfix"></div>

                        <div class="col-lg-2"></div>
                          <div class="col-lg-6">
                              <br />
                        <table class="table-responsive">
                            <tr>
                                <td style="width: 30%;"> <strong>Gross</strong>   
                                                <asp:TextBox ID="txtGross" Enabled ="false"  Width="60%" runat="server"   class="form-control"></asp:TextBox>
                                </td>
                                <td style="width: 35%;"><strong>Deductions</strong>  
                                    <asp:TextBox ID="txtded" Width="60%" Enabled ="false"  runat="server"   class="form-control"></asp:TextBox>
                                </td>
                                <td style="width: 30%;"> <strong>Net</strong>   
                                     <asp:TextBox ID="txtNet" Width="60%" runat="server"   class="form-control" Enabled ="false" ></asp:TextBox></td>
                                 </tr>
                            </table>
                             
                        <div style="float: right">
                                <br />
                             <asp:Button ID="btnCal" runat="server" Text="Gross Ded Net" CssClass="btn btn-primary" OnClick="btnCal_Click"
                                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
                                    ToolTip="enter the all fileds then click !" ValidationGroup="vgAdd" />
                                
                            </div>
                               </div>
                    </asp:Panel>


                    <%--end of  employeewise salary details--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


