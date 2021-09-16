<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="UserCreation.aspx.cs" Inherits="Military.Views.UserCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
  <div class="col-lg-12">
       <div class="form-group col-lg-2">  </div>
        <div class="form-group col-lg-8">  
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-bar-chart-o"></i>User Creation</h3>
                    </div>
                    <div class="panel-body">

                        <asp:Label ID="lblMsg" runat="server"  style="text-align:center"  ></asp:Label> 
  
                <table class="table table-bordered table-hover table-responsive"  id="tblGrid">        
                <tbody>
                   <tr>
                        <td style="width: 15%">User Name</td>
                        <td>
                               <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                              <asp:TextBox ID="txtName"  Width ="50%" class="form-control"  runat="server"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="User Name Required" ControlToValidate="txtName" 
                            ValidationGroup="add" ForeColor="Red" />
                                   </div>
                        </td>
                    </tr>
                    

                    <tr>
                        <td>Password</td>
                        <td>
                               <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-edit"></i></span>
                              <asp:TextBox ID="txtNewPassword"  Width ="50%" class="form-control"  runat="server" TextMode="Password"/> 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="Password Required" ControlToValidate="txtNewPassword" 
                            ValidationGroup="add" ForeColor="Red" />
                                   </div>
                        </td>
                    </tr>

                     <tr>
                         <td> Active </td>
                          <td>
                           <asp:RadioButtonList ID="rbtnActive" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">YES</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                           </asp:RadioButtonList>
                          </td>
                         </tr>

                   
                </tbody>     
                </table>
            <div class="modal-footer">
                   
                    <asp:Button ID="btnADD" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnADD_Click"
                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="add"  />
                       
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />  
                
                <asp:Button ID="btvClose" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btvClose_Click" />  
                <br />
             </div>    
                    
                          
               </div>
                </div>
            </div>
      </div>
    </div>
</asp:Content>
