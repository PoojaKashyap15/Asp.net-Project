<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ChangeCalendarYr.aspx.cs" Inherits="prototype.Views.ChangeCalendarYr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function isNumeric(keyCode) {
            return ((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || (keyCode >= 96 && keyCode <= 105))
        }
    </script>
    <div class="col-lg-12">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:Label ID="lblMsg" runat="server"></asp:Label>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="lblHeading" runat="server" Text="Change Calender Year"></asp:Label></h3>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-lg-12 ">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                            <ContentTemplate>


                                <div class="table-responsive">
                                    <asp:GridView ID="gvView" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="sl" CssClass="table  table-hover table-striped table-bordered "
                                        BorderColor="Black" OnRowCommand="gvView_RowCommand" AllowPaging="true" PageSize="12" Height="20px">
                                        <PagerStyle ForeColor="black" HorizontalAlign="Center" Height="20" VerticalAlign="Middle" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="First" LastPageText="Last" />
                                        <Columns>

                                            <asp:BoundField DataField="sl" ItemStyle-Width="10%" HeaderText="Sl" ItemStyle-Height="8px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="descs" ItemStyle-Width="30%" ItemStyle-Height="8px" HeaderText="Description" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" />
                                            <asp:BoundField DataField="start_date" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"/>
                                            <asp:BoundField DataField="end_date" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="End Date" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-VerticalAlign="Middle" />
                                            
                                            <asp:TemplateField HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" Height="20px" />
                                                <ItemTemplate>
                                                   
                                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select"  ToolTip="Select" CommandArgument='<%#Eval("sl") %>' Width="80px" Height="30px" CssClass="btn btn-info" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvView" EventName="RowCommand" />
                            </Triggers>

                        </asp:UpdatePanel>


                    </div>
                </div>
                </div>
            </div>
        </div>
    </asp:Content>