<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaManutenzioniSTR.aspx.vb"
    Inherits="MANUTENZIONI_RicercaManutenzioniSTR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
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
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                    Manutenzioni e servizi - STR - Export STR
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
                    <div style="overflow: auto;" class="FontTelerik">
                        <table>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="Label2" runat="server">Struttura</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbStruttura" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="Label4" runat="server">Esercizio Finanziario</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbEsercizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="Label3" runat="server" Width="130px">Lotto</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbLotto" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="LblComplesso" runat="server">Complesso</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbComplesso" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="LblEdificio" runat="server">Edificio</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbEdificio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="lblTipoServizio" runat="server">Servizio</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbServizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblFornitore" runat="server">Fornitore</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbFornitore" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblAppalto" runat="server">Num. Repertorio</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbAppalto" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVoce" runat="server">Codice/Voce BP</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbVoceBP" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStato" runat="server">Stato ODL</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbStato" Width="300PX" AppendDataBoundItems="true" Filter="Contains"
                                        runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                        LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server">Autorizzazione</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="DropDownListAutorizzazione" Width="200PX" AppendDataBoundItems="true"
                                        Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="1" Text="Autorizzati" />
                                            <telerik:RadComboBoxItem Value="0" Text="Non autorizzati" />
                                            <telerik:RadComboBoxItem Value="-1" Text="Tutti" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="Label1" runat="server">Data emissione ordine dal:</asp:Label>
                                </td>
                                <td>
                                    <table>
                                        <tr>
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
                                            <td>
                                                <asp:Label ID="lblDataP2" runat="server">Data emissione ordine al:</asp:Label>
                                                <telerik:RadDatePicker ID="txtDataAL" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                    DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                                    <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                                        <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
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
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
