<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/main.Master" AutoEventWireup="true" CodeBehind="Contactus.aspx.cs" Inherits="OnlineFee.Views.ContactUsStu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="col-lg-12">
      <div class="form-group col-lg-12">  
                <div class="panel panel-default">
                    <div class="panel-heading"> <h3 class="panel-title">Contact Us</h3> </div>
                    <div class="panel-body">
  
                <table class="table table-bordered table-hover table-responsive"  id="tblGrid">        
                <tbody>
                    <tr>
                        <td>Company Name</td>
                       <td><strong><asp:Label ID="lblCompanyName"  runat="server" ></asp:Label></strong> </td>
                            
                    </tr>
                    <tr>
                        <td>Name</td>
                        <td><strong> <asp:Label ID="lblName"  runat="server" Text ="vijay"   ></asp:Label></strong> </td>
                       
                    </tr>
                    <tr>
                        <td>From mail id</td>
                        <td>  <asp:TextBox ID="txtEmail"  Width ="65%"   runat="server" /> 
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="***" ControlToValidate="txtEmail" 
                            ValidationGroup="vgSubmit" ForeColor="Red" /></td>
                    </tr>
                    <tr>
                        <td>Contact No</td>
                         <td>  <asp:TextBox ID="txtContactNo"  Width ="65%"   runat="server" /> 
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Text="***" ControlToValidate="txtContactNo" 
                            ValidationGroup="vgSubmit" ForeColor="Red" /> </td>
                    </tr>

                    

                    <tr>
                        <td>Subject</td>
                        <td>  <asp:TextBox ID="txtSubject"  Width ="95%"   runat="server"/>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="***" ControlToValidate="txtSubject" 
                            ValidationGroup="vgSubmit" ForeColor="Red" /> </td>
                    </tr>
                    <tr>
                        <td>Descriptions</td>
                        <td>  <asp:TextBox ID="txtDescriptions" TextMode = "MultiLine" Width ="95%" Height="100px"  runat="server"/>  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="***" ControlToValidate="txtDescriptions" 
                            ValidationGroup="vgSubmit" ForeColor="Red" /> </td>
                    </tr>
                    <tr>
                        <td>File Attachment</td>
                        <td>  <asp:FileUpload ID="fuAttachment" runat="server" /> <asp:Label ID="Label2"  runat="server" Text =" (Optional)"></asp:Label> </td>
                    </tr>
                </tbody>     
                </table>
            <div class="modal-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="btnSubmit_Click"
                    ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgSubmit"  />
                       
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-info" OnClick="btnClear_Click" />  
                <br />
             </div>    
                    <asp:Label ID="lblMsg" runat="server"  style="text-align:center"  ></asp:Label> 
                        
               </div>
                </div>
            </div>
      </div>


    
</asp:Content>
