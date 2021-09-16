<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="AttReg.aspx.cs" Inherits="Salary.Views.AttReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:roundc runat="server" ID="webheaderschool" />
    <script type="text/javascript">
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 9 || keyCode == 8 || keyCode == 46 || keyCode == 37 || keyCode == 39 || (keyCode >= 96 && keyCode <= 105) || keyCode == 110 || keyCode == 190 || (keyCode >= 37 && keyCode <= 40))
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


                        <div class="col-lg-11">
                            <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="true" Width="100%" BackColor="White">
                                <table class="table table-bordered table-hover table-responsive">

                                    <tr>
                                        <td style="width: 15%;">Month - Year </td>
                                        <td style="width: 20%;">
                                            <asp:TextBox ID="txtmmyyyy" Width="90%" runat="server"  AutoCompleteType="Disabled" class="form-control" placeholder="Click Here"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" Format="MMM-yyyy" PopupButtonID="txtmmyyyy" TargetControlID="txtmmyyyy" />
                                            <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtmmyyyy" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                        <td style="width: 15%;">Select Name </td>
                                        <td style="width: 25%;">
                                            <asp:DropDownList ID="ddlName" runat="server" Width="90%" Height="32px" DataTextField="name" DataValueField="sl" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDept" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlName" Display="None" ErrorMessage="Select Department Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
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

                            <div class="col-lg-12">
                                <br />
                                <asp:Panel ID="panelView" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">

                                    <div class="table-responsive  rounded-corners ">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:GridView ID="gvCodes" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="prs_emno" CssClass="table table-hover table-striped table-bordered "
                                                    BorderColor="Black" OnPageIndexChanging="gvCodes_PageIndexChanging" AllowPaging="true" PageSize="20">
                                                    <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="20" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                                    <Columns>
                                                        <asp:BoundField DataField="prs_emno" ItemStyle-Width="10%" HeaderText="Emp No" />
                                                        <asp:BoundField DataField="prs_name" ItemStyle-Width="20%" HeaderText="Emp Name" />
                                                        <asp:TemplateField HeaderText="Working">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtWorking" runat="server" CssClass="form-control" Text='<%#Eval("ATT_WORKING") %>' Enabled="true" OnTextChanged="txtWorking_TextChanged" AutoPostBack="true"
                                                                    onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Absent">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtAbsent" runat="server" CssClass="form-control" Text='<%#Eval("ATT_ABSENT") %>' OnTextChanged="txtAbsent_TextChanged" AutoPostBack="true"
                                                                    onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Present">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPresent" runat="server" CssClass="form-control" Text='<%#Eval("ATT_PRESENT") %>' Enabled="false" AutoPostBack="false"
                                                                    onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CL">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCL" runat="server" CssClass="form-control" Text='<%#Eval("ATT_CL") %>' AutoPostBack="false"
                                                                    onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EL">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtEL" runat="server" CssClass="form-control" Text='<%#Eval("ATT_EL") %>' AutoPostBack="false"
                                                                    onkeydown="return isNumeric(event.keyCode);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
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




