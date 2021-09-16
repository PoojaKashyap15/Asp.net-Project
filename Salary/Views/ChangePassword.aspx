<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Military.Views.ChangePassword" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
  <div class="col-lg-12">
       <div class="form-group col-lg-2">  </div>
        <div class="form-group col-lg-8">  
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="fa fa-bar-chart-o"></i>Change Password</h3>
                    </div>
                    <div class="panel-body">
  
                <table class="table table-bordered table-hover table-responsive"  id="tblGrid">        
                <tbody>
                    <tr>
                        <td>User Name</td>
                        <td>  <strong> <asp:Label ID="lblUserName"  runat="server"   ></asp:Label> </strong>
                         </td>
                    </tr>
                    <tr>
                        <td>Current Password</td>
                        <td>  <asp:TextBox ID="txtCurPassword"  Width ="95%"  TextMode ="Password" runat="server"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="*" ControlToValidate="txtCurPassword" 
                            ValidationGroup="vgAdd" ForeColor="Red" />
                        </td>
                    </tr>

                    <tr>
                        <td>New Password</td>
                        <td>  <asp:TextBox ID="txtNewPassword"  Width ="95%"  TextMode ="Password"  runat="server"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="*" ControlToValidate="txtNewPassword" 
                            ValidationGroup="vgAdd" ForeColor="Red" />
                        </td>
                    </tr>

                    <tr>
                        <td>Re-Enter Password</td>
                        <td>  <asp:TextBox ID="txtNewPassword2"  Width ="95%"  TextMode ="Password"  runat="server"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="*" ControlToValidate="txtNewPassword2" 
                            ValidationGroup="vgAdd" ForeColor="Red" />
                        </td>
                    </tr>
                </tbody>     
                </table>
            <div class="modal-footer">
                    <asp:Label ID="lblSl" Visible="false" runat="server"   ></asp:Label>
                <asp:HiddenField ID="hfCurrentPassword" runat="server" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click"
                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd"  />
                       
                    &nbsp;&nbsp;
                       
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />  

                &nbsp;&nbsp;  

                <asp:Button ID="btvClose" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btvClose_Click" />  
                <br />
             </div>    
                    <asp:Label ID="lblMsg" runat="server"  style="text-align:center"  ></asp:Label> 
                     <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToCompare="txtNewPassword2" ControlToValidate="txtNewPassword" 
                                    Operator="Equal" ErrorMessage="New Password and Re-Enter password do not match" 
                                    SetFocusOnError="True"></asp:CompareValidator>      
               </div>
                </div>
            </div>
      </div>
    </div>
</asp:Content>
