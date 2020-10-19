<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSitContabile.aspx.vb"
    Inherits="RicercaSitContabile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RICERCA SIT. CONTABILE</title>
    <script type="text/javascript" src="Funzioni.js">
<!--
        var Uscita1;
        Uscita1 = 1;
// -->
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table width="100%" class="FontTelerik">
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td class="TitoloModulo">
                Report - Situazione contabile - Per struttura
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnStampa" runat="server" Style="top: 0px; left: 0px" Text="Stampa"
                                ToolTip="Stampa" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnAnnulla" runat="server" Style="top: 0px; left: 0px" Text="Esci"
                                ToolTip="Home" />
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
                            <asp:Label ID="Label4" runat="server" Width="110px" Text="Esercizio Finanziario"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="40%">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            <asp:Label ID="lblStruttura" runat="server" Width="110px" Text="Struttura"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <telerik:RadComboBox ID="cmbStruttura" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="40%">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="TitoloH1" style="text-align: left">
                            <asp:CheckBox ID="ChkStampa" runat="server" Font-Bold="True" F Text="Stampa con sottovoci"
                                Width="200px" TabIndex="3" /><br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AnnoSelezionato" runat="server" />
    </form>
</body>
</html>
