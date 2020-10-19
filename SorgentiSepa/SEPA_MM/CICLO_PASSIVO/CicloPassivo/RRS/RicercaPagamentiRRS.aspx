<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPagamentiRRS.aspx.vb"
    Inherits="RRS_RicercaPagamentiRRS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script language="javascript" type="text/javascript">

    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }
   
   
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
        &nbsp;
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                    Ordini - Gestione non patrimoniale - SAL - Stampa pagamenti
                </td>
            </tr>
            <tr>
                <td>
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
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px;">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <div>
                            <table style="width: 100%">
                                <tr>
                                    <td style="height: 21px" width="10%">
                                        <asp:Label ID="lblStruttura" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            TabIndex="6" Width="110px">Struttura</asp:Label>
                                    </td>
                                    <td style="height: 21px">
                                        <telerik:RadComboBox ID="cmbStruttura" Width="70%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            Width="130px">Esercizio Finanziario</asp:Label>
                                    </td>
                                    <td style="height: 21px">
                                        <telerik:RadComboBox ID="cmbEsercizio" Width="70%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        <asp:Label ID="LblFornitore" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            Width="130px">Fornitore</asp:Label>
                                    </td>
                                    <td style="height: 21px">
                                        <telerik:RadComboBox ID="cmbFornitore" Width="70%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 21px">
                                        <asp:Label ID="LblAppalto" runat="server" Style="z-index: 100; left: 48px; top: 64px"
                                            Width="130px">Appalto</asp:Label>
                                    </td>
                                    <td style="height: 21px">
                                        <telerik:RadComboBox ID="cmbAppalto" Width="70%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Style="z-index: 100; left: 48px; top: 64px"
                                            Width="130px">Voce P.F.</asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbServizioVoce" Width="70%" AppendDataBoundItems="true"
                                            Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left">
                                        <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            Width="130px">Stato della firma</asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbStato" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="-1" Text=" " Selected="true" />
                                                <telerik:RadComboBoxItem Value="0" Text="NON FIRMATO" />
                                                <telerik:RadComboBoxItem Value="1" Text="FIRMATO CON RISERVA" />
                                                <telerik:RadComboBoxItem Value="2" Text="FIRMATO" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left">
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left">
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    &nbsp;<br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
