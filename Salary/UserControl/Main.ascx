<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Main.ascx.cs" Inherits="Campus.UserControl.InstituteHeader" %>
 <script>

    function addClickBehavior(menuTable) {
        var tbody = menuTable.getElementsByTagName("TBODY")[0];
        var tr = tbody.getElementsByTagName("TR")[0];

        for (var i = 0; i < tr.childNodes.length; i++) {
            var td = tr.childNodes[i];
            if (td.tagName && td.tagName.toLowerCase() == 'td') {
                var anchor = td.getElementsByTagName("A")[0];
                if (anchor) {

                    var onClick = td.onmouseover;
                    td.onclick =
                    (function (el, method) {
                        return function (evt) {
                            method.call(el);
                            if (window.event) {
                                evt = window.event
                            }
                            evt.cancelBubble = true;
                        };
                    })(td, onClick);
                    td.onmouseover =
                    (function (el) {
                        return function () {
                            Menu_HoverRoot(el);
                        };
                    })(td);
                    //add cursor style
                    anchor.style.cursor = "default";
                    anchor.onclick = function () { return false; };
                    //td.onmouseout = null;
                }
            }
        }
    }
    function WebForm_RemoveClassName(element, className) {
        var current = element.className;
        var oldLength = -1;

        if (current) {
            while (oldLength != current.length) {
                if (current.substring
                  (current.length - className.length - 1,
                   current.length) == ' ' + className) {
                    element.className =
                     current.substring
                     (0, current.length - className.length - 1);
                    oldLength = current.length;
                    current = element.className;
                    continue;
                }
                if (current == className) {
                    element.className = "";
                    oldLength = current.length;
                    current = element.className;
                    continue;
                }
                var index = current.indexOf(' ' + className + ' ');
                if (index != -1) {
                    element.className =
                     current.substring
                     (0, index) +
                     current.substring
                      (index + className.length + 2, current.length);
                    oldLength = current.length;
                    current = element.className;
                    continue;
                }
                if (current.substring
                              (0, className.length) == className + ' ') {
                    element.className =
                     current.substring
                              (className.length + 1, current.length);
                }
                current = element.className;
                oldLength = current.length;
            }
        }
    }
    function Menu_HoverRoot(item) {
        var node = (item.tagName.toLowerCase() == "td") ?
            item :
            item.cells[0];
        var data = Menu_GetData(item);
        if (!data) {
            return null;
        }
        var nodeTable = WebForm_GetElementByTagName(node, "table");
        if (data.staticHoverClass) {
            //avoids adding the same class twice

            nodeTable.hoverClass = data.staticHoverClass;
            WebForm_AppendToClassName(nodeTable, data.staticHoverClass);

        }
        node = nodeTable.rows[0].cells[0].childNodes[0];
        if (data.staticHoverHyperLinkClass) {

            node.hoverHyperLinkClass = data.staticHoverHyperLinkClass;
            WebForm_AppendToClassName
               (node, data.staticHoverHyperLinkClass);

        }
        return node;
    }
</script>

<div class="container-fluid">
    <div class="row ">

        <a class="navbar-brand hidden-lg hidden-md" href="#">
            <asp:Label ID="lblInsShortName" runat="server" Text="MLA College, Bangalore"></asp:Label>
        </a>

        <div class="col-md-9 hidden-xs hidden-sm ">
            <div class=" col-name">
          <div class="col-md-9 col-sm-6 col-xs-6">
            <asp:Image ID="ImageColLogo" runat="server" class="img-responsive pull-left" Style="height: 60px;" />
        </div>
            </div>
            <div class="col-sub-name">
                <%--<asp:Label ID="Label1" runat="server" Text="Online Payroll"></asp:Label>--%>
            </div>
        </div>
        <div class="col-md-3 hidden-xs hidden-sm">
            <table>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="std-details-hdr"><b>Login Name : </b></td>
                    <td class="std-details">
                        <asp:Label ID="lblLoginName" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td class="std-details-hdr">
                        <b>Financial Year : </b>

                    </td>
                    <td class="std-details">
                         <asp:Label ID="lblLoginTime" runat="server" Text="Bangalore Centeral Store "></asp:Label> </td>

                </tr>
                

            </table>

        </div>
    </div>
</div>
<%--<nav class="navbar navbar-inverse">
    <div class="container-fluid">
         <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#headNavbar">
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand hidden-lg hidden-md" href="#">
                <asp:Label ID="lblInsShortNameold" runat="server" Text="Payroll"></asp:Label>
            </a>
        </div> 
        <div class="collapse navbar-collapse" id="headNavbar">
            <ul class="nav navbar-nav">
                <li class="dropdown">
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" DynamicHorizontalOffset="2"
                        RenderingMode="List" IncludeStyleBlock="false"
                        StaticMenuStyle-CssClass="nav navbar-nav" DynamicMenuStyle-CssClass="dropdown-menu">
                    </asp:Menu>
                </li>

            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="../Views/Login.aspx" class=""><span class="glyphicon glyphicon-log-out"></span>Logout</a></li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="../views/Contactus.aspx" class=""><span class="glyphicon glyphicon-phone-alt"></span></a></li>
            </ul>
        </div>
    </div>
</nav>--%>

<nav class="navbar navbar-inverse">
    <div class="container-fluid">

        <div class="collapse navbar-collapse" id="headNavbar">
            <ul class="nav navbar-nav">

               <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" DynamicHorizontalOffset="0" MaximumDynamicDisplayLevels="4"
                     RenderingMode="List" IncludeStyleBlock="false"
                    StaticMenuStyle-CssClass="nav navbar-nav" DynamicMenuStyle-CssClass="dropdown-menu">
                </asp:Menu> 

            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li><a href="../Views/Login.aspx" class=""  >Logout</a></li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
              <%--  ../views/Contactus.aspx--%>
                <li><a href="#" class=""><span class="glyphicon glyphicon-phone-alt"></span></a></li>
            </ul>
        </div>
    </div>
</nav> 




