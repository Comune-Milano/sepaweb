<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSAL_RRS_FIRMA.aspx.vb"
    Inherits="RRS_RicercaSAL_RRS_FIRMA" %>

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
    <div class="fontTelerik">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="TitoloModulo">
                   Ordini - Gestione non patrimoniale - SAL - Ricerca
                </td>
            </tr>
            <tr>
                <td style="height: 25px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia la ricerca" ToolTip="Avvia la ricerca"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" CausesValidation="False">
                                </telerik:RadButton>
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
                    <table width="100%">
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label122" runat="server" Text="Struttura"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbStruttura" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label5" runat="server" Text="Esercizio Finanziario"></asp:Label>
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
                                <asp:Label ID="Label12" runat="server" Text="Fornitore"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbFornitore" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label1" runat="server" Text="Num. Repertorio Appalto"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbAppalto" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label6" runat="server" Text="Voce DGR"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbServizioVoce" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label7" runat="server" Text="Stato della firma"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbStato" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="" />
                                        <telerik:RadComboBoxItem Value="0" Text="NON FIRMATO" />
                                        <telerik:RadComboBoxItem Value="1" Text="FIRMATO CON RISERVA" />
                                        <telerik:RadComboBoxItem Value="2" Text="FIRMATO" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label2" runat="server" Text="N. ADP"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtADP" runat="server" MaxLength="7" onblur="valid(this,'notnumbers');"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Anno"></asp:Label>
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
        </table>
    </div>
    </form>
</body>
</html>
