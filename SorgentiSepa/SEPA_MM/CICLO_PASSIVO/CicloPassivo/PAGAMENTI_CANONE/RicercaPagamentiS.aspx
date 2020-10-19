<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPagamentiS.aspx.vb"
    Inherits="PAGAMENTI_CANONE_RicercaPagamentiS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script language="javascript" type="text/javascript">


   
</script>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RICERCA</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%" class="TitoloModulo">
                    <asp:Label ID="lblTitolo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 50%">
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="10%">
                                <asp:Label ID="lblStruttura" runat="server" Width="110px">Struttura</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbStruttura" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                                <asp:Label ID="Label4" runat="server" 
                                    Style="z-index: 100; left: 48px; top: 32px" Width="110px">Esercizio Finanziario</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbEsercizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                                <asp:Label ID="LblFornitore" runat="server"  Style="z-index: 100; left: 48px; top: 32px" Width="110px">Fornitore:</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbFornitore" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 21px" width="10%">
                                <asp:Label ID="Label1" runat="server" 
                                    Style="z-index: 100; left: 48px; top: 32px" Width="110px">Num. Repertorio:</asp:Label>
                            </td>
                            <td style="height: 21px">
                                <telerik:RadComboBox ID="cmbAppalto" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                                <br />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
