<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagamentiPerStruttura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_PagamentiPerStruttura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>SITUAZIONE PAGAMENTI</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="FontTelerik">
        <table width="100%">
            <tr>
                <td style="height: 5px;">
                </td>
            </tr>
            <tr>
                <td class="TitoloModulo">
                    Report - Situazione contabile - Pagamenti
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnStampa" runat="server" Style="top: 0px; left: 0px" Text="Stampa"
                                    ToolTip="Stampa Situzione Contabile" />
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
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
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
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 250px; vertical-align: top; text-align: left" class="TitoloH1">
                    <asp:CheckBox ID="ChkPagamentiUtenze" runat="server" Text="Pagamenti Utenze" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 50%">
                            </td>
                            <td style="width: 25%">
                                &nbsp;
                            </td>
                            <td style="width: 25%">
                                &nbsp;
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
