<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaFornitore.aspx.vb"
    Inherits="APPALTI_RicercaFornitore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Fornitori / Ricerca</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    nascondi = 0;
    function disabilitaMinore(e) {
        var key;
        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 226)
            return false;
        else
            return true;
    };
</script>
<script type="text/javascript" src="../../CicloPassivo.js"></script>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table width="100%" class="FontTelerik">
        <tr>
            <td class="TitoloModulo">
                Fornitori - Ricerca
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia ricerca" ToolTip="Avvia ricerca fornitori" />
                        </td>
                        <td>
                            <telerik:RadButton ID="RadButtonPulisci" runat="server" Text="Pulisci filtri" ToolTip="Pulisci filtri di ricerca" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Esci" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Label ID="Label1" runat="server" Text="Codice"></asp:Label>
                        </td>
                        <td style="width: 90%">
                            <asp:TextBox ID="txtCodice" runat="server" MaxLength="20" onkeydown="return disabilitaMinore(event)"
                                Font-Names="Arial" Font-Size="9pt" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Ragione Sociale"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRagione" runat="server" onkeydown="return disabilitaMinore(event)"
                                Font-Names="Arial" Font-Size="9pt" Width="240px" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Codice Fiscale"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodiceFiscale" runat="server" MaxLength="16" onkeydown="return disabilitaMinore(event)"
                                Font-Names="Arial" Font-Size="9pt" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LBLPIVA" runat="server" Text="Partita IVA"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPIva" runat="server" MaxLength="11" onkeydown="return disabilitaMinore(event)"
                                Font-Names="Arial" Font-Size="9pt" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle">
                            <img src="../../../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Utilizzare <b>*</b> come carattere jolly nelle ricerche</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
