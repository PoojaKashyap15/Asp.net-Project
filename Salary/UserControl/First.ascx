<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="First.ascx.cs" Inherits="Campus.UserControl.First" %>
<div class="container-fluid">
    <div class="row ">
        <div class="col-md-8 col-sm-6 col-xs-6">
            <asp:Image ID="ImageColLogo" runat="server" class="img-responsive pull-left" Style="height: 80px;" />
        </div>
        <div class="col-md-4  col-name hidden-xs hidden-sm std-details-hdr ">
  
            <asp:Label ID="lblInstituteName" CssClass="text-primary" runat="server" Text="Bangalore University" Font-Size="30px"></asp:Label>
       </div> 
    
        <div class="col-md-2 hidden-xs hidden-sm">
        
            <table>

                <tr>
                    <td class="std-details-hdr"></td>
                </tr>
                <tr>
                    <td class="std-details-hdr"></td>
                </tr>
            </table>

        </div>
  
  </div>
</div>


<nav class="navbar navbar-inverse">
    <div class="container-fluid">
       <marquee>  <asp:Label ID="lblSubHead" CssClass ="text-primary" runat="server" Text="Label" Font-Size="14px" Font-Bold="true" ForeColor="White"></asp:Label> </marquee>
    </div>
</nav>



