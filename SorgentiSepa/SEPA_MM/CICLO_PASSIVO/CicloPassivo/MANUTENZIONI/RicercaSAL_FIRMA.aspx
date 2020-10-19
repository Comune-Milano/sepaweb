<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSAL_FIRMA.aspx.vb"
    Inherits="MANUTENZIONI_RicercaSAL_FIRMA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script language="javascript" type="text/javascript">

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\,]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }
</script>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>RICERCA</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="FontTelerik">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%" class="TitoloModulo">
                    Ordini - Manutenzioni e servizi - SAL - Ricerca
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
                <td style="height: 25px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="7" cellpadding="2">
                        <tr>
                            <td style="width: 20%" width="5%">
                                <asp:Label ID="Label122" runat="server" Text="Struttura"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbStruttura" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" width="5%">
                                <asp:Label ID="Label5" runat="server" Text="Esercizio Finanziario"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbEsercizio" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" width="5%">
                                <asp:Label ID="Label12" runat="server" Text="Fornitore"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbFornitore" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" width="5%">
                                <asp:Label ID="Label1" runat="server" Text="Num. Repertorio Appalto"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbAppalto" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" width="5%">
                                <asp:Label ID="Label6" runat="server" Text="Voce DGR"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbServizioVoce" Width="90%" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%" width="5%">
                                <asp:Label ID="Label7" runat="server" Text="Stato della firma"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbStato" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="" Selected="true" />
                                        <telerik:RadComboBoxItem Value="0" Text="NON FIRMATO" />
                                        <telerik:RadComboBoxItem Value="1" Text="FIRMATO CON RISERVA" />
                                        <telerik:RadComboBoxItem Value="2" Text="FIRMATO" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" F Width="130px">Codice Progetto Vision</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="DropDownListProgettoVision" Width="100px" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Bold="False"  Width="130px">Numero SAL Vision</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="DropDownListNumeroSALVision" Width="70px" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="5%">
                                <asp:Label ID="Label2" runat="server" Text="N. ADP"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtADP" runat="server" MaxLength="7" onblur="valid(this,'notnumbers');"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Text="Anno"
                                    Font-Size="9pt"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtANNO" runat="server" MaxLength="4" onblur="valid(this,'notnumbers');"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 200px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
