<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstrazionePatrimonio.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_EstrazionePatrimonio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Estrazione del patrimonio</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="2" cellspacing="2" width="100%" class="FontTelerik">
        <tr>
            <td class="TitoloModulo" colspan="2">
                 Manutenzioni e servizi - STR - Patrimonio
            </td>
        </tr>
        <tr>
            <td style="width: 17%">
                Estrazione dei complessi
            </td>
            <td style="width: 83%">
                <telerik:RadButton ID="RadButtonEstrazioneComplessi" runat="server" Text="Estrai complessi"
                    Width="100px">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                Estrazione degli edifici
            </td>
            <td>
                <telerik:RadButton ID="RadButtonEstrazioneEdifici" runat="server" Text="Estrai edifici"
                    Width="100px">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                Estrazione delle unità immobiliari
            </td>
            <td>
                <telerik:RadButton ID="RadButtonEstrazioniUI" runat="server" Text="Estrai UI" Width="100px">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                Estrazione degli impianti
            </td>
            <td>
                <telerik:RadButton ID="RadButtonEstrazioneImpianti" runat="server" Text="Estrai impianti"
                    Style="top: 0px; left: -1px" Width="100px">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                Estrazione delle unità comuni
            </td>
            <td>
                <telerik:RadButton ID="RadButtonEstrazioniUC" runat="server" Text="Estrai UC" Width="100px">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                Estrazione delle scale
            </td>
            <td>
                <telerik:RadButton ID="RadButtonEstrazioneScale" runat="server" Text="Estrai scale"
                    Width="100px" Style="top: 1px; left: 0px">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                Estrazione voci DGR
            </td>
            <td>
                <telerik:RadButton ID="RadButtonEstrazioneDGR" runat="server" Text="Estrai DGR" Width="100px"
                    Style="top: 1px; left: 0px">
                </telerik:RadButton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
