<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Salary.Views.Dashboard" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
    <uc1:roundc runat="server" ID="webheaderschool" />



    <div class="row">
        <div class="col-lg-12">
           
            <div class="col-lg-12">
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <asp:Chart ID="chartActive_yn" runat="server" Height="300px" Width="375px" Visible="true">
                                <Titles>
                                    <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                                        Text="Employee Count By Status">
                                    </asp:Title>
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>





                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <asp:Chart ID="chart_dept_noe" runat="server" Height="300px" Width="375px" Visible="true">
                                <Titles>
                                    <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                                        Text="Employee Count By Departmnet">
                                    </asp:Title>
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>

                
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <asp:Chart ID="chartMale_female" runat="server" Height="300px" Width="375px" Visible="true">
                                <Titles>
                                    <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                                        Text="Employee Count By Gender">
                                    </asp:Title>
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>

            </div>
             <div class="col-lg-12">
                 <div class="col-lg-5"> </div>
                 <asp:Label ID="Label1" runat="server" Font-Italic="true" Font-Names="Times New Roman" Font-Bold="true" Font-Size="12pt" Text="Annual Salary Details"></asp:Label>
              
                 <div class ="clearfix"></div>
                      <div class="table-responsive  rounded-corners">
                    <asp:GridView ID="gvView" runat="server" AllowPaging="true" AutoGenerateColumns="false"  Width="100%">
                        <HeaderStyle BackColor="#81BEF7" Font-Bold="True"  HorizontalAlign="Center"  ForeColor="Black" />
                       <RowStyle BackColor="#F7F6F3" ForeColor="Black" />
                        <Columns>
                            <asp:BoundField DataField="descs" HeaderText="Descriptions" HeaderStyle-HorizontalAlign="Center" ItemStyle-Height="15pt" ItemStyle-BackColor="#81BEF7" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="apr" HeaderText="Apr" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="may" HeaderText="May" ItemStyle-BackColor="White"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="jun" HeaderText="Jun" ItemStyle-BackColor="White"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="jul" HeaderText="Jul" ItemStyle-BackColor="White"  ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="aug" HeaderText="Aug" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="sep" HeaderText="Sep" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="oct1" HeaderText="Oct" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="nov" HeaderText="Nov" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="dec1" HeaderText="Dec" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="jan" HeaderText="Jan" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="feb" HeaderText="Feb" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="mar" HeaderText="Mar" ItemStyle-BackColor="White" ItemStyle-HorizontalAlign="Right" />
                        </Columns>

                    </asp:GridView>
                </div>
                <br />
                <br />
            </div>
            <div class="col-lg-12">
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <asp:Chart ID="chart_desg_noe" runat="server" Height="300px" Width="375px" Visible="true">
                                <Titles>
                                    <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                                        Text="Employee Count By Designation">
                                    </asp:Title>
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <asp:Chart ID="chart_attd" runat="server" Height="300px" Width="375px" Visible="true">
                                <Titles>
                                    <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                                        Text="Top 5 Leave Taker">
                                    </asp:Title>

                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="2" />
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <asp:Chart ID="chart_max_salary" runat="server" Height="300px" Width="375px" Visible="true">
                                <Titles>
                                    <asp:Title Font="Times New Roman, 12pt, style=Bold, Italic" Name="Title1"
                                        Text="Gross Salary for the Year">
                                    </asp:Title>
                                </Titles>
                                <Legends>
                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                </Legends>
                                <Series>
                                    <asp:Series Name="Default" />
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                </ChartAreas>
                            </asp:Chart>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
