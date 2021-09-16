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
                                <asp:Panel ID="panelView" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">

                                    <div class="table-responsive  rounded-corners ">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <asp:GridView ID="gvCodes" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="PRH_EMNO" CssClass="table table-hover table-striped table-bordered "
                                                    BorderColor="Black" OnPageIndexChanging="gvCodes_PageIndexChanging" AllowPaging="true" PageSize="20" Visible="true">
                                                    <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="20" />
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
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
                                </asp:Panel>
                            </div>



                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


