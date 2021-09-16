<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/First.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Campus.Views.first" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%@ Register Src="~/UserControl/RoundedCorner.ascx" TagPrefix="uc1" TagName="roundc" %>
    <uc1:roundc runat="server" ID="webheaderschool" />

    <div class="row">
        <div class="col-lg-12 container-padding ">

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
                        <div class="form-group col-lg-9">
     <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <%--<div class="table-responsive  rounded-corners">
                    <asp:GridView ID="gvView" runat="server" AllowPaging="true" AutoGenerateColumns="false" DataKeyNames="sl" OnPageIndexChanging="gvView_PageIndexChanging" PageSize="25" Width="100%">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="5" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Names="helvetica11" HeaderStyle-Font-Size="Large" HeaderStyle-Height="30px" HeaderText="Notifications" ItemStyle-Height="60px" ItemStyle-Width="100%">
                                <ItemTemplate>
                                    <div class="col-lg-10">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("att", "~/Attachment/Notifications/{0}") %>' Target="_search"><%#DataBinder.Eval(Container.DataItem,"noti_subject") %></asp:HyperLink>
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblNotidate" runat="server" Text='<%#Bind("Notification_Date" ,"{0:d}")%>' />
                                    </div>
                                    <div class="col-lg-12">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Nar") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>--%>
            </div>

            <div class="form-group col-lg-3">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">

                            <i class="glyphicon  glyphicon-bookmark"></i>
                            User Login

                

                        </h3>
                    </div>
                    <div class="panel-body">

                        <table class="table-responsive">
                            <tbody>
                                <tr>

                                    <td style="width: 100%">User Name:
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                            <asp:TextBox ID="txtUserName" Width="100%" class="form-control" Height ="32px" runat="server"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfvtxtUserName" runat="server" ErrorMessage="User Name is Required" Display="None" SetFocusOnError="true" ControlToValidate="txtUserName" ValidationGroup="vgAdd" ForeColor="Red" />

                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td style="width: 100%">Password:
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                            <asp:TextBox ID="txtPassword" Width="100%" class="form-control" TextMode="Password" Height ="32px" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfctxtPasswordo" runat="server" ErrorMessage="Password is Required" Display="None" SetFocusOnError="true" ControlToValidate="txtPassword" ValidationGroup="vgAdd" ForeColor="Red" />
                                        </div>

                                    </td>

                                </tr>
                                <tr>

                                    <td style="width: 100%">User Type:
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="glyphicon glyphicon-hand-right"></i></span>
                                            <asp:DropDownList ID="ddlUserType" CssClass="selectpicker form-control input-sm" runat="server" Width="95%" DataTextField="Text" DataValueField="ID" AutoPostBack="false"></asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="revddName" InitialValue="0" runat="server" ValidationGroup="vgAdd" ControlToValidate="ddlUserType" Display="None" ErrorMessage="Select User Type" SetFocusOnError="true"></asp:RequiredFieldValidator>


                                    </td>

                                </tr>
                                <tr>
                                    <td style="align-content: center">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-default" OnClick="btnSubmit_Click" ValidationGroup="vgAdd" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>



         

    
<div class="row">
  <div class="col-sm-12">
    <%--<div class="row">
      <div class="col-md-12">
        <div class="well">
          <h4 class="text-success">
               <asp:HyperLink ID="HyperLink3" runat="server"  ForeColor="Blue" 
             NavigateUrl="http://buadmission.inhawk.com/Views/Login.aspx" >PG Admission fee payment
              </asp:HyperLink> 
              </h4>
        </div>
      </div>
      <div class="col-md-12">
        <div class="well">
          <h4 class="text-danger"> 
             <asp:HyperLink ID="HyperLink2" runat="server"  ForeColor="Red" 
             NavigateUrl="http://buofc.inhawk.com/Direct/Home.aspx" >Revaluation/Photocopy fee payment
             </asp:HyperLink> 
          </h4>
        </div>
      </div>
      <div class="col-md-12">
        <div class="well">
          <h4 class="text-primary">  
                <asp:HyperLink ID="HyperLink4" runat="server"  ForeColor="Blue" 
             NavigateUrl="http://buadmission.inhawk.com/Views/Login.aspx" >Hostel fee payment
             </asp:HyperLink> 
        </div>
      </div>
    </div> --%>    <!--/row-->    
  </div><!--/col-12-->
</div><!--/row-->
 
   </div>
        </div>
    </div>

</asp:Content>
