<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Salary.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Fixed Header</title>
    <script src="Scripts/jquery-1.2.6.js" type="text/javascript"></script>
    <script src="Scripts/webtoolkit.jscrollable.js" type="text/javascript"></script>
    <script src="Scripts/webtoolkit.scrollabletable.js" type="text/javascript"></script>    
    
    <script type="text/javascript">
        $(document).ready(function() {
            jQuery('table').Scrollable(400, 800);
        });
    </script>
    
    <style type="text/css">
			table {				
				font: normal 11px "Trebuchet MS", Verdana, Arial;								
			    background:#fff;            				
			    border:solid 1px #C2EAD6;
			}			
			
			td{			    
	            padding: 3px 3px 3px 6px;
	            color: #5D829B;
			}
			th {
				font-weight:bold;
				font-size:smaller;
	            color: #5D728A;	                        	            
	            padding: 0px 3px 3px 6px;
	            background: #CAE8EA 				
			}
		</style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
          <asp:GridView ID="ScrollT" runat="server" AutoGenerateColumns="False" DataKeyNames="sl"
              AllowPaging="False" AllowSorting="True" OnPreRender="ScrollT_PreRender" Height ="100px">
            <Columns>                           
                <asp:BoundField DataField="sl" HeaderText="sl" ReadOnly="True" SortExpression="ProductID" />
                <asp:BoundField DataField="code" HeaderText="code" SortExpression="ProductName" />
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="QuantityPerUnit" />
                <asp:BoundField DataField="co_Sl" HeaderText="co_sl" SortExpression="UnitPrice" />
            </Columns>
        </asp:GridView>
 
    </div>
    </form>
</body>
</html>