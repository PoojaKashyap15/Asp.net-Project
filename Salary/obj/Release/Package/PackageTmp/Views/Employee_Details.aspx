<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Employee_Details.aspx.cs" Inherits="Salary.Views.Employee_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-12 container-padding ">

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="lblHeading" runat="server">Employee Details</asp:Label></h3>
                </div>
                <div class="panel-body">

                    <div id="DivSearch" runat="server" class="form-group  ">
                        <table class="table  table-bordered table-hover table-responsive">
                            <tbody>
                                <tr>
                                    <td style="width: 20%; vertical-align: middle;">Search by Employee Name </td>
                                    <td style="width: 51%">
                                        <asp:TextBox ID="txtSearch" Width="100%" CssClass=" form-control" runat="server" placeholder="Search by Employee Name"></asp:TextBox>

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
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>


                    <div class="row">
                        <div class="col-lg-12 ">
                            <%------------------------------------------------- Detaisls Grid View -------------------------------------------------%>
                            <div class="table-responsive rounded-corners">
                                <%--PageSize="12"--%>
                                <asp:Panel ID="panleGridView" runat="server" BorderStyle="None" Height="360px" Visible="true" Width="100%" BackColor="White">
                                    <asp:GridView ID="gvView" runat="server" AutoGenerateColumns="false" DataKeyNames="PRS_EMNO" CssClass="table  table-hover table-striped table-bordered table-responsive "
                                        BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="false" Height="20px" EmptyDataRowStyle-BackColor="LightCyan" EmptyDataText="Sorry No Data Found!!!." EmptyDataRowStyle-Font-Bold="true"
                                        EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeader="true" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("PRS_EMNO") %>' ImageUrl="~/Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemStyle Width="3%" Height="10px" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("PRS_EMNO") %>' ImageUrl="~/Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="20px" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Del">
                                                <ItemStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtn" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("PRS_EMNO") %>' ImageUrl="~/Attachment/DefaultPhoto/delete.ico" runat="server" Width="25" Height="20px" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PRS_SL" ItemStyle-Width="0%" ItemStyle-Height="0px" HeaderText="Sl No" Visible="false" />
                                            <asp:BoundField DataField="PRS_EMNO" ItemStyle-Width="9%" ItemStyle-Height="8px" HeaderText="Emp No" />
                                            <asp:BoundField DataField="PRS_NAME" ItemStyle-Width="25%" ItemStyle-Height="8px" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="PRS_MOBILE" ItemStyle-Width="12%" ItemStyle-Height="8px" HeaderText="Contact Number" />
                                            <asp:BoundField DataField="DSG_DESC" ItemStyle-Width="25%" ItemStyle-Height="8px" HeaderText="Designation" />
                                            <asp:BoundField DataField="DPT_DESC" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="Department" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-2 "></div>
                    <div class="col-lg-8 ">
                        <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White" Style="align-content: center;">
                            <table class="table table-bordered table-hover table-responsive">
                                <%----------------%>
                                <tr>
                                    <td>Employee No</td>
                                    <td>
                                        <asp:TextBox ID="txtempno" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtempno" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtempno" Display="None" SetFocusOnError="true" ErrorMessage="Enter Employee No"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtempno" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtempno"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>

                                    <td>PAN NO</td>
                                    <td>
                                        <asp:TextBox ID="txtpanNo" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvtxtpanNo" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtpanNo" Display="None" SetFocusOnError="true" ErrorMessage="Enter PAN No"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtpanNo" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtpanNo"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                    </td>
                                </tr>
                                <%------------------------%>
                                <tr>
                                    <td style="width: 15%">Name</td>
                                    <td>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtname" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtname" Display="None" SetFocusOnError="true" ErrorMessage="Enter Employee Name"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtname" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtname"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>Address</td>
                                    <td>
                                        <asp:TextBox ID="txtadd" runat="server" Width="95%" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <%-------------------------%>
                                <%-------------------------%>
                                <tr>
                                    <td style="width: 15%">Gender</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbtngender" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>Date of Birth</td>
                                    <td>
                                        <asp:TextBox ID="txtdob" runat="server" CssClass="form-control" Width="95%" placeholder="Click Here"></asp:TextBox>

                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtdob" TargetControlID="txtdob" Format="dd-MMM-yyyy" />

                                        <asp:RequiredFieldValidator ID="rfvtxtdob" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtdob" Display="None" SetFocusOnError="true" ErrorMessage="Select Date"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtdob" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtdob"></ajaxToolkit:ValidatorCalloutExtender>

                                    </td>
                                </tr>
                                <%-------------------------%>
                                <tr>
                                    <td>Date of Retirement</td>
                                    <td>
                                        <asp:TextBox ID="txtdoreg" runat="server" CssClass="form-control" Width="95%" placeholder="Click Here"></asp:TextBox>

                                        <ajaxToolkit:CalendarExtender ID="CalenderExtender2" runat="server" PopupButtonID="txtdoreg" TargetControlID="txtdoreg" Format="dd-MMM-yyyy" />
                                        <asp:RequiredFieldValidator ID="rfvtxtdoreg" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtdoreg" Display="None" SetFocusOnError="true" ErrorMessage="Enter Date of Register"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtdoreg" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtdoreg"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>Residential Number</td>
                                    <td>
                                        <asp:TextBox ID="txtredno" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <%-------------------------%>
                                <tr>
                                    <td>Contact Number</td>
                                    <td>
                                        <asp:TextBox ID="txtcntno" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>Emailid</td>
                                    <td>
                                        <asp:TextBox ID="txtemail" Width="95%" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--------------------------%>
                                <tr>
                                    <td>Qualification</td>
                                    <td>
                                        <asp:TextBox ID="txtquliftn" runat="server" Width="95%" CssClass="form-control"></asp:TextBox>
                                    </td>

                                   <td>State </td>
                                    <td>
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" DataValueField="sl" Width="95%" DataTextField="Name"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlState" runat="server" InitialValue="0" ValidationGroup="vgAdd" ControlToValidate="ddlState" Display="None" SetFocusOnError="true" ErrorMessage="Select State"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlState"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <%--------------------------%>
                                <tr>
                                    <td>Department </td>
                                    <td>
                                        <asp:DropDownList ID="ddldepcode" runat="server" CssClass="form-control" DataValueField="id" Width="95%" DataTextField="Name"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddldepcode" runat="server" InitialValue="0" ValidationGroup="vgAdd" ControlToValidate="ddldepcode" Display="None" SetFocusOnError="true" ErrorMessage="Select Department"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddldepcode" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddldepcode"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>Designation</td>
                                    <td>
                                        <asp:DropDownList ID="ddldesg" runat="server" DataValueField="id" CssClass="form-control" Width="95%" DataTextField="Name"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddldesg" runat="server" InitialValue="0" ValidationGroup="vgAdd" ControlToValidate="ddldesg" Display="None" SetFocusOnError="true" ErrorMessage="Select Designation"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddldesg" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddldesg"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <%--------------------------%>
                                <tr>
                                    <td>Location</td>
                                    <td>
                                        <asp:DropDownList ID="ddlLocation" runat="server" DataValueField="id" CssClass="form-control" Width="95%" DataTextField="Name"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlLocation" runat="server" InitialValue="0" ValidationGroup="vgAdd" ControlToValidate="ddlLocation" Display="None" SetFocusOnError="true" ErrorMessage="Select Location"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlLocation" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlLocation"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>Bank</td>
                                    <td>
                                        <asp:DropDownList ID="ddlbank" runat="server" DataValueField="id" CssClass="form-control" Width="95%" DataTextField="Name"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlbank" runat="server" InitialValue="0" ValidationGroup="vgAdd" ControlToValidate="ddlbank" Display="None" SetFocusOnError="true" ErrorMessage="Select bank"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vceddlbank" PopupPosition="BottomRight" runat="server" TargetControlID="rfvddlbank"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <%--------------------------%>
                                <tr>
                                    <td>Account No</td>
                                    <td>
                                        <asp:TextBox ID="txtalvno" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>Adhar No</td>
                                    <td>
                                        <asp:TextBox ID="txtadhrno" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--------------------------%>
                                <tr>
                                    <td>PF No</td>
                                    <td>
                                        <asp:TextBox ID="txtpfno" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>ESI No</td>
                                    <td>
                                        <asp:TextBox ID="txtesino" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                <%--------------------------%>
                                <tr>
                                    <td>Basic Pay</td>
                                    <td>
                                        <asp:TextBox ID="txtbasicpay" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtbasicpay" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtbasicpay" Display="None" SetFocusOnError="true" ErrorMessage="Enter Basic pay"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtbasicpay" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtbasicpay"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td>GSLI No</td>
                                    <td>
                                        <asp:TextBox ID="txtgslino" runat="server" CssClass="form-control" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>PT</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbtnpt" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>PF</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbtnpf" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                     <td>ESI</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblESI" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td>Active</td>
                                    <td>
                                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>

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
                                 <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                                <asp:HiddenField ID="hfSl" runat="server" />
                            </div>

                        </asp:Panel>


                        <asp:Panel ID="panelVIEW" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                            <div class="row">

                                <div class="col-lg-12 col-md-12 col-sm-12">

                                    <asp:DetailsView ID="dvLookup" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="False">
                                        <Fields>
                                            <asp:BoundField DataField="PRS_EMNO" HeaderText="Employee_No" />
                                            <asp:BoundField DataField="PRS_PANN" HeaderText="PAN No" />
                                            <asp:BoundField DataField="PRS_NAME" HeaderText="Name" />
                                            <asp:BoundField DataField="PRS_ADDRESS" HeaderText="Address" />
                                            <asp:BoundField DataField="PRS_GENDER" HeaderText="Gender" />
                                            <asp:BoundField DataField="PRS_DTOB" HeaderText="DateofBirth" />
                                            <asp:BoundField DataField="PRS_DTOR" HeaderText="Date of Register " />
                                            <asp:BoundField DataField="PRS_RESPH" HeaderText="Residential No" />
                                            <asp:BoundField DataField="PRS_QLFT" HeaderText="Qualification" />
                                            <asp:BoundField DataField="PRS_MOBILE" HeaderText="Contact No" />
                                            <asp:BoundField DataField="PRS_EMAIL" HeaderText="Email id" />
                                            <asp:BoundField DataField="DPT_DESC" HeaderText="Department " />
                                            <asp:BoundField DataField="DSG_DESC" HeaderText="Designation " />
                                            <asp:BoundField DataField="LCN_DESC" HeaderText="Location" />
                                            <asp:BoundField DataField="BNK_NAME" HeaderText="Bank" />
                                            <asp:BoundField DataField="PRS_BKAC" HeaderText="Account No" />
                                             <asp:BoundField DataField="state_name" HeaderText="State" />
                                            <asp:BoundField DataField="adhar_no" HeaderText="Adhar No" />
                                            <asp:BoundField DataField="PRS_PFNO" HeaderText="PF No" />
                                            <asp:BoundField DataField="PRS_ESINO" HeaderText="ESI No" />
                                            <asp:BoundField DataField="prs_basic" HeaderText="Basic Pay" />
                                            <asp:BoundField DataField="PRS_GLSINO" HeaderText="GLSI No" />
                                            <asp:BoundField DataField="prs_pt_yn" HeaderText="PT" />
                                            <asp:BoundField DataField="prs_pf_yn" HeaderText="PF" />
                                            <asp:BoundField DataField="esi" HeaderText="ESI" />
                                            <asp:BoundField DataField="Status" HeaderText="status" />

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
                </div>
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
            <%--  Active--%>
        </div>
    </div>
</asp:Content>
