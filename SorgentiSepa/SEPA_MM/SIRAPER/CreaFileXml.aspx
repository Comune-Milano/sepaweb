<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaFileXml.aspx.vb" Inherits="SIRAPER_CreaFileXml" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crea File Xml Siraper</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/menu.css" rel="stylesheet" type="text/css" />
    <script src="js/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
</head>
<body style="background-image: url('Immagini/Sfondo.png'); background-repeat: repeat-x;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server" target="modal">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr style="height: 45px">
            <td colspan="4">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;CREAZIONE
                    FILE XML SIRAPER</strong></span>
            </td>
        </tr>
        <tr style="height: 45px;">
            <td style="width: 50%">
                &nbsp;
            </td>
            <td style="width: 30%; text-align: right;">
                <asp:Button ID="btnCreaFile" runat="server" Text="Avvia Creazione File" CssClass="bottone"
                    OnClientClick="caricamentoincorso();" />
                &nbsp;
            </td>
            <td style="width: 17%; text-align: right;">
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" OnClientClick="caricamentoincorso();self.close();" />
            </td>
            <td style="width: 3%; text-align: right;">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 420px">
            <td colspan="4" style="vertical-align: top">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 2%;">
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Height="400px" Width="97%" TextMode="MultiLine"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idConnessione" runat="server" Value="" />
    <asp:HiddenField ID="sescon" runat="server" Value="" />
    <asp:HiddenField ID="idSiraper" runat="server" Value="-1" />
    <asp:HiddenField ID="idSiraperVersione" runat="server" Value="1" />
    <asp:HiddenField ID="CodIstatDefault" runat="server" Value="" />
    <script language="javascript" type="text/javascript">
        initialize();
        function initialize() {
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
    </form>
</body>
</html>
