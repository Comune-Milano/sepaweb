<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneContabilePerAppalti.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_SituazioneContabilePerAppalti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Situazione Contabile Contratti</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    if (window.event) {
                        event.keyCode = 0;
                    } else {
                        e.preventDefault();
                    };
                };
            } else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                } else if (obj.value.length == 5) {
                    obj.value += "/";
                } else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            event.keyCode = 0;
                        } else {
                            e.preventDefault();
                        };
                    };
                };
            };
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div style="height: 500px; width: 100%" class="FontTelerik">
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td colspan="2" style="height: 8px">
                </td>
            </tr>
            <tr>
                <td class="TitoloModulo" colspan="2">
                    Situazione Contabile Contratti
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnStampa" runat="server" Style="top: 0px; left: 0px" Text="Ricerca"
                                    ToolTip="Ricerca" />
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
                <td style="width: 10%">
                    <asp:Label Text="Tipologia Contratto" runat="server" />
                </td>
                <td style="width: 80%">
                    <telerik:RadComboBox ID="cmbTipologiaContratto" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Text="TUTTI" Enabled="true" />
                            <telerik:RadComboBoxItem Value="1" Text="PATRIMONIALI" />
                            <telerik:RadComboBoxItem Value="2" Text="NON PATRIMONIALI" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label Text="Fornitore" runat="server" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbFornitore" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
             <tr>
                <td style="width: 10%">
                    <asp:Label Text="Direttore lavori" runat="server" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbDirLavori" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="style3" style="width: 10%">
                    <asp:Label Text="Esercizio Finanziario" runat="server" />
                </td>
                <td class="style3">
                    <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label Text="Sede Territoriale" runat="server" />
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbSedeTerritoriale" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label Text="Data Repertorio dal" runat="server" />
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadDatePicker ID="dataInizio" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                                    DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                                    <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
                                        <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                    </DateInput>
                                    <Calendar ID="Calendar3" runat="server">
                                        <SpecialDays>
                                            <telerik:RadCalendarDay Repeatable="Today">
                                                <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                            </telerik:RadCalendarDay>
                                        </SpecialDays>
                                    </Calendar>
                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </td>
                            <td>
                                &nbsp;
                                <asp:Label Text="al" runat="server" />
                                &nbsp;
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="dataFine" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label Text="CIG" runat="server" />
                </td>
                <td>
                    <asp:TextBox ID="txtCIG" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
