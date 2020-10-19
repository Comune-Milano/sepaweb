<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRRS.aspx.vb" Inherits="RRS_RicercaRRS" %>

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
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>RICERCA</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        &nbsp;
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">
                    Ordini - Gestione non patrimoniale - Ricerca - Selettiva
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca">
                                </telerik:RadButton>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: auto;">
                        <table style="width: 100%">
                            <tr>
                                <td width="20">
                                    <asp:Label ID="Label2" runat="server" Width="130px">Struttura</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbStruttura" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="Label4" runat="server" Width="130px">Esercizio Finanziario</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="LblComplesso" runat="server" Width="130px">Complesso</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbComplesso" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="LblEdificio" runat="server" Width="130px">Edificio</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEdificio" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="lblVocePF" runat="server" ForeColor="Black" Width="130px">Voce P.F.</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbVocePF" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="LblFornitore" runat="server" Width="130px">Fornitore</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbFornitore" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="LblAppalto" runat="server" Width="130px">Num. Repertorio</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAppalto" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="40%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="lblStato" runat="server" ForeColor="Black" Width="130px">Stato ODL</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbStato" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="30%">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="20">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Black" Style="z-index: 100; left: 80px;
                                        top: 288px" Width="157px">Autorizzazione</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="DropDownListAutorizzazione" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="20%">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="-1" Text="Tutti" Selected="true" />
                                            <telerik:RadComboBoxItem Value="0" Text="Non autorizzati" />
                                            <telerik:RadComboBoxItem Value="1" Text="Autorizzati" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 48px; top: 64px">Data emissione ordine dal:</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtDataDAL" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                        <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                        </DateInput>
                                        <Calendar ID="Calendar3" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left" width="20">
                                    <asp:Label ID="lblDataP2" runat="server" Style="z-index: 104; left: 48px; top: 64px"
                                        Width="130px">Data emissione ordine al:</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="txtDataAL" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                        <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                        </DateInput>
                                        <Calendar ID="Calendar1" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today">
                                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                </telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left" width="20">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
