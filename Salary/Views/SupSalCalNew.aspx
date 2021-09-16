<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SupSalCalNew.aspx.cs" Inherits="Salary.Views.SupSalCalNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:roundc runat="server" ID="webheaderschool" />
    <script type="text/javascript">
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 9 || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || (keyCode >= 96 && keyCode <= 105))
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
                        <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>

                            <asp:Label ID="lblsl" runat="server" Visible="false"></asp:Label>
                        </asp:Panel>



                        <div class="col-lg-12">
                            <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                                <table class="table table-bordered table-hover table-responsive">

                                    <tr>
                                        <td style="width: 10%;">Month - Year </td>
                                        <td style="width: 12%;">
                                            <asp:TextBox ID="txtmmyyyy" Width="95%" runat="server" AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                            <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td style="width: 10%;">Salary Type </td>
                                        <td style="width: 20%;">
                                            <asp:DropDownList ID="ddlSalType" runat="server" Width="95%" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlSalType" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlSalType" Display="None" ErrorMessage="Please Select Salary Type" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlSalTypee" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlSalType"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td>Select Name </td>
                                        <td>
                                            <asp:DropDownList ID="ddlName" runat="server" Width="90%" Height="32px" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDept" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlName" Display="None" ErrorMessage="Select Employee Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceddlDept" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlDept"></ajaxToolkit:ValidatorCalloutExtender>

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

                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">

                                <br />
                                <asp:Panel ID="panelView" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="table-responsive  rounded-corners ">
                                                <asp:GridView ID="gvCodes" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="COD_CODE" CssClass="table table-hover table-striped table-bordered "
                                                    BorderColor="Black" OnPageIndexChanging="gvCodes_PageIndexChanging" AllowPaging="true" PageSize="20">
                                                    <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="20" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:BoundField DataField="COD_CODE" ItemStyle-Width="5%" HeaderText="Code" />
                                                        <asp:BoundField DataField="COD_DESC" ItemStyle-Width="20%" HeaderText="Descriptions" />
                                                        <asp:BoundField DataField="COD_TYPE" ItemStyle-Width="10%" HeaderText="Pay or Rec" />
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemStyle Width="13%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text='<%#Eval("PRG_AMNT") %>' OnTextChanged="txtAmount_TextChanged" AutoPostBack="true"
                                                                    onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                            <h5>Narration:</h5>
                                            <asp:TextBox ID="txtnarration" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>

                                            <div class="clearfix"></div>

                                            <div class="col-lg-12">
                                                <br />
                                                <table class="table-responsive">
                                                    <tr>
                                                        <td style="width: 30%;"><strong>Gross</strong>
                                                            <asp:TextBox ID="txtGross" ReadOnly="true" Width="60%" runat="server" class="form-control"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 35%;"><strong>Deductions</strong>
                                                            <asp:TextBox ID="txtded" Width="60%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%;"><strong>Net</strong>
                                                            <asp:TextBox ID="txtNet" Width="60%" ReadOnly="true" runat="server" class="form-control"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div style="float: right">
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSave_Click"
                                            ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                                        <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>





