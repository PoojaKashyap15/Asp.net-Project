<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" Inherits="Military.Views.permission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .DisplayNone {
            display:none;
        }
    </style>
    <div class="col-lg-12">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <asp:Label ID="lblsl" runat="server" Text="" Visible="false"></asp:Label>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">User Permission</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-12 ">
                        <div class="form-group col-lg-4">
                            <label>User Name</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-exclamation-sign"></i></span>
                                <asp:DropDownList ID="ddlUserName" CssClass="selectpicker form-control input-sm" runat="server" Width="95%" DataTextField="name" DataValueField="sl"></asp:DropDownList>
                            </div>
                            <%--     <asp:RequiredFieldValidator ID="rfvddlUsername" InitialValue="0" runat="server" ValidationGroup="vgShow" ControlToValidate="ddlUserName" Display="None" ErrorMessage="Select User Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvddlUsername"></ajaxToolkit:ValidatorCalloutExtender>
                            --%>
                        </div>
                        <div class="form-group col-lg-3">
                            <label>Menus</label><br />
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-exclamation-sign"></i></span>
                                <asp:DropDownList ID="ddlMenu" CssClass="selectpicker form-control input-sm" runat="server" Width="95%" DataTextField="name" DataValueField="sl"></asp:DropDownList>
                            </div>
                            <%--       <asp:RequiredFieldValidator ID="rfvddlMenu" InitialValue="0" runat="server" ValidationGroup="vgShow" ControlToValidate="ddlMenu" Display="None" ErrorMessage="Select Menu Name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvddlMenu"></ajaxToolkit:ValidatorCalloutExtender>
                            --%>
                        </div>
                        <div class="form-group col-lg-2">
                            <br />
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" OnClick="btnShow_Click" Width="90px" ValidationGroup="vgShow" />
                          &nbsp;
                          <asp:Button ID="btnClose" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClose_Click" />
                        </div>
                        <div class="form-group col-lg-12">
                            <div class="table-responsive  rounded-corners ">
                                <asp:GridView ID="gvMenu" DataKeyNames="sl" AutoGenerateColumns="False" CellPadding="5"
                                    Width="80%"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" runat="server">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="Lavender" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="30" />
                                    <PagerStyle BackColor="#5D7B9D" ForeColor="White" HorizontalAlign="Center" Height="30" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="8" FirstPageText="First" LastPageText="Last" />
                                    <Columns>

                                        <asp:BoundField HeaderText="sl" DataField="sl" ControlStyle-Width="25%" Visible="true" ControlStyle-CssClass="DisplayNone" FooterStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" ItemStyle-CssClass="DisplayNone"> </asp:BoundField>
                                        <asp:BoundField HeaderText="Menu Name" DataField="title" ControlStyle-Width="25%"> </asp:BoundField>
                                        
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkView" runat="server" AutoPostBack="true" OnCheckedChanged="chkView_CheckedChanged" />
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkAdd" runat="server" AutoPostBack="true" OnCheckedChanged="chkAdd_CheckedChanged" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />

                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEdit" runat="server" AutoPostBack="true" OnCheckedChanged="chkEdit_CheckedChanged"/>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkDelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_CheckedChanged"/>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPrint" runat="server" AutoPostBack="true" OnCheckedChanged="chkPrint_CheckedChanged" />
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateField>


                                    </Columns>

                                </asp:GridView>


                            </div>

                            <div class="form-group col-lg-12">
                    <div style="float: right">
                        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnAdd_Click" />
                   
                    </div>
                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
