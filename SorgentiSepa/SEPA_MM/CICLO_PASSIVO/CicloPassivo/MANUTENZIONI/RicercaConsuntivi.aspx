<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaConsuntivi.aspx.vb"
    Inherits="MANUTENZIONI_RicercaConsuntivi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>RICERCA</title>
    <style type="text/css">
        .style1
        {
            width: 71px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <table style="width: 100%" class="FontTelerik">
            <tr>
                <td>
                    <div>
                        &nbsp;
                        <table style="width: 100%;">
                            <tr>
                                <td class="TitoloModulo">
                                      Ordini - Manutenzioni e servizi - Consuntivazione - Selettiva
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
                                            </td>
                                            <td>
                                                &nbsp;
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
                                    <div style="overflow: auto;">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                        <div>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td class="style1">
                                                        <asp:Label ID="lblEF" runat="server" Width="150px"> Esercizio Finanziario</asp:Label>
                                                    </td>
                                                    <td style="width: 100%">
                                                        <telerik:RadComboBox ID="cmbEsercizio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                            LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        <asp:Label ID="LblFornitore" runat="server">Fornitore</asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbFornitore" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                            LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        <asp:Label ID="LblAppalto" runat="server">Contratto</asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbAppalto" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                            LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left" class="style1">
                                                        <asp:Label ID="Label1" runat="server" Width="150px">Data emissione ordine dal:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtDataDAL" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                                                            DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                                                            <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar1" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top; text-align: left" class="style1">
                                                        <asp:Label ID="lblDataP2" runat="server" Width="150px">Data emissione ordine al:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtDataAL" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                                                            MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                                                            <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar2" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
