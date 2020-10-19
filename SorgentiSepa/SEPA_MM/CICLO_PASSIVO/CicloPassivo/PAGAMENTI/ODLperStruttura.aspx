<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ODLperStruttura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ODLperStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title></title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div class="FontTelerik">
        <table width="100%">
            <tr>
                <td style="height: 5px;">
                </td>
            </tr>
            <tr>
                <td class="TitoloModulo">
                   Report - Situazione contabile - ODL
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
                                <asp:Label ID="Label1" runat="server" Text="Esercizio Finanziario"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="ddlEsercizio" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label2" runat="server" Text="Struttura"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="ddlStrutture" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
