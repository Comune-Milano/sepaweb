<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU.aspx.vb" Inherits="ANAUT_CreaAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Crea A.U.</title>
    <link href="../../StandardTelerik/Style/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CssEtichetta {
            padding: 30px,30px,30px,30px;
        }
    </style>
    <script language="javascript" type="text/javascript">
    function ConfInserimentoAU() {
        var chiediConferma;
        var nomeBando = "";
        if (document.getElementById('IDBANDO').value = '1') {

            nomeBando = "2007"
        }
        if (document.getElementById('IDBANDO').value = '2') {

            nomeBando = "2009"
        }
        if (document.getElementById('IDBANDO').value = '3') {

            nomeBando = "2011"
        }

        //var msg1 = "Attenzione, sei sicuro di voler procedere con l\'inserimento dell\'Anagrafe Utenza \""+ nomeBando +"\"?";
        var msg1 = "Attenzione, sei sicuro di voler procedere con l\'inserimento dell\'Anagrafe Utenza selezionata?";

        chiediConferma = window.confirm(msg1);
        if (chiediConferma == true) {
            document.getElementById('ConfAU').value = '1';
        }
        else {
            document.getElementById('ConfAU').value = '0';
            alert('Operazione annullata!');
        }
    }
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
                        <asp:Button ID="btnProcedi" runat="server" Text="Procedi" ToolTip="Procedi" OnClientClick="ConfInserimentoAU();" />
                        <asp:Button ID="btnIndietro" runat="server" Text="Indietro" ToolTip="Indietro" Visible="false" />
                        <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False" ClientIDMode ="Static"
                            OnClientClick="CancelEdit();" /></td>

                </tr>
                <tr>
                    <td>
                        <span style="font-size: 12pt; color: #0066FF; font-family: Arial">
                            <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                        Text="Selezionare il dichiarante" ForeColor="#0066FF" BorderStyle="Solid" BorderWidth="1px"
                                        BackColor="#FFFFCC" Width="406px" Height="20px" CssClass="CssEtichetta"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="ListaInt" runat="server" Font-Names="ARIAL" Font-Size="10pt">
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Visible="False" Font-Bold="True" Font-Names="Arial"
                            Font-Size="10pt" ForeColor="Red"></asp:Label>
                    </td>
                </tr>

            </table>
        </div>
        <asp:HiddenField ID="idc" runat="server" />
        <asp:HiddenField ID="t" runat="server" />
        <asp:HiddenField ID="fase" runat="server" />
        <asp:HiddenField ID="procedi" runat="server" />
        <asp:HiddenField ID="iddich" runat="server" />
        <asp:HiddenField ID="tipo" runat="server" />
        <asp:HiddenField ID="IDA" runat="server" />
        <asp:HiddenField ID="IDCONVOCAZIONE" runat="server" />
        <asp:HiddenField ID="scheda" runat="server" />
        <asp:HiddenField ID="ConfAU" runat="server" />
        <asp:HiddenField ID="IDBANDO" runat="server" />
    </form>
</body>
    
</html>
