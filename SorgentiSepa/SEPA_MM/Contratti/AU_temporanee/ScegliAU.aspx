<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScegliAU.aspx.vb" Inherits="Contratti_AU_abusivi_ScegliAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scelta Anagrafe Utenza</title>
    <link href="../../StandardTelerik/Style/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="720000">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"></telerik:RadFormDecorator>
        <div>
            <table style="padding: 20px; width: 100%;">
                <tr>
                    <td>
                        <asp:Button ID="btnProcedi" runat="server" Text="Procedi" ToolTip="Procedi" />
                        <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
                            OnClientClick="CancelEdit();" /></td>

                </tr>
                <tr><td>&nbsp</td></tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Width="100%">Selezionare l'Anagrafe Utenza che si desidera caricare:</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rdbListaAU" runat="server" Font-Names="ARIAL" Font-Size="10pt" BackColor="#FFFFCC" width="100%">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                
            </table>
            <asp:HiddenField ID="CodContratto" runat="server" />
        </div>
        <script language="javascript" type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            };
            function CancelEdit() {
                GetRadWindow().close();
            };
        </script>
    </form>
</body>
</html>
