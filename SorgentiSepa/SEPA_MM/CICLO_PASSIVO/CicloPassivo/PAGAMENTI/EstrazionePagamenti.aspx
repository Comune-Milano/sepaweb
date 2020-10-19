<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstrazionePagamenti.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_EstrazionePagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report CdP/Fatture/Pagamenti</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript">
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
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Buttons" />
    <div style="width: 100%">
        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0" class="FontTelerik">
            <tr>
                <td class="TitoloModulo" colspan="2">
                    Report - Report pagamenti
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerik:RadButton ID="btnRptCompleto" runat="server" Text="Scarica Rpt Completo"
                        ToolTip="Scarica Rpt Completo" />
                    <telerik:RadButton ID="btnRptPF" runat="server" Style="top: 0px; left: 1px" Text="Scarica Rpt per P.F"
                        ToolTip="Scarica Rpt per P.F" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindow1');}"
                        AutoPostBack="false" />
                    <telerik:RadButton ID="btnVisReport" runat="server" Style="top: 0px; left: 1px" Text="Visual. Report"
                        ToolTip="Visual. Report" />
                    <telerik:RadButton ID="btnHome" runat="server" Style="top: 0px; left: 1px" Text="Esci"
                        ToolTip="Esci" />
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="2" cellspacing="2" width="70%">
            <tr>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server" Text="Codice operazione contabile fattura"></asp:Label>
                </td>
                <td colspan="4">
                    <div style="height: 160px; overflow: auto; border: 1px solid #507cd1; background-color: White">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="100%">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Anno Piano Finanziario"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td colspan="4">
                    <telerik:RadComboBox ID="DropDownListEsercizioFinanziario" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Data CdP"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="da"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataCdPDa" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                    <asp:Label ID="Label55" runat="server" Text="a"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataCdPA" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Data pagamento"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="da"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataPagamentoDa" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                        DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
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
                <td>
                    <asp:Label ID="Label8" runat="server" Text="a"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataPagamentoA" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                        DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                        <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar4" runat="server">
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
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Data fattura"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="da"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataFatturaDa" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                        DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                        <DateInput ID="DateInput4" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar5" runat="server">
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
                    <asp:Label ID="Label6" runat="server" Text="a"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataFatturaA" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                        DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                        <DateInput ID="DateInput6" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar6" runat="server">
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
                <td>
                    <asp:Label ID="Label56" runat="server" Text="Data registrazione fattura"></asp:Label>
                </td>
                <td>
                    da
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataRegistrazioneFatturaDa" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                        DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                        <DateInput ID="DateInput7" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar7" runat="server">
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
                    a
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadDatePicker ID="TextBoxDataRegistrazioneFatturaA" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                        DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                        <DateInput ID="DateInput8" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar8" runat="server">
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
                <td colspan="7">
                    <asp:CheckBox ID="CheckBoxPagamentiFatturati" runat="server" Text="Solo pagamenti fatturati" />
                </td>
            </tr>
        </table>
    </div>
    <div style="visibility: hidden; height: 10px">
        <asp:DataGrid runat="server" ID="DataGridPagamenti" CellPadding="1" Font-Names="Arial"
            Font-Size="8pt" ForeColor="#000000" GridLines="None" CellSpacing="1" ShowFooter="false"
            AutoGenerateColumns="false" Width="99%" UseAccessibleHeader="True">
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left"
                    Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_PAGAMENTO" HeaderText="ID_PAGAMENTO" ItemStyle-HorizontalAlign="Left"
                    Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_FORNITORE" HeaderText="CODICE FORNITORE" ItemStyle-HorizontalAlign="Center"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="RAGIONE_SOCIALE" HeaderText="RAGIONE SOCIALE" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE PARTITA IVA" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUMERO_CDP" HeaderText="NUMERO CDP" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_CDP" HeaderText="DATA CDP" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="IMPONIBILE" HeaderText="IMPONIBILE" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="IVA" HeaderText="IVA" ItemStyle-HorizontalAlign="Right"
                    Visible="True" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="TOT" HeaderText="TOTALE" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_VOCE_PF" HeaderText="ID_VOCE_PF" ItemStyle-HorizontalAlign="Left"
                    Visible="false" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="ANNO_PF" HeaderText="ANNO PF" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="VOCE_PF" HeaderText="VOCE" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="IMPONIBILE_D" HeaderText="IMPONIBILE DETTAGLIO" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="IVA_D" HeaderText="IVA DETTAGLIO" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="TOTALE_D" HeaderText="TOTALE DETTAGLIO" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_FATTURA_MM" HeaderText="ID_FATTURA_MM" ItemStyle-HorizontalAlign="Left"
                    Visible="false" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUMERO_RDS" HeaderText="NUMERO RDS" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="N_FATT_FORN" HeaderText="NUMERO FATTURA FORNITORE" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_FATT" HeaderText="DATA FATTURA" ItemStyle-HorizontalAlign="Center"
                    Visible="true" HeaderStyle-CssClass="formatoData"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_REGISTRAZIONE" HeaderText="DATA REGISTRAZIONE" ItemStyle-HorizontalAlign="Center"
                    Visible="true" HeaderStyle-CssClass="formatoData"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_OP_CONT" HeaderText="CODICE OPERAZIONE CONTABILE"
                    ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_PAGAMENTO_MM" HeaderText="ID_PAGAMENTO_MM" ItemStyle-HorizontalAlign="Left"
                    Visible="false" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUMERO_PAG" HeaderText="NUMERO PAGAMENTO" ItemStyle-HorizontalAlign="Left"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_PAG" HeaderText="DATA PAGAMENTO" ItemStyle-HorizontalAlign="Center"
                    Visible="true" HeaderStyle-CssClass="formatoData"></asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="IMPORTO PAGATO" ItemStyle-HorizontalAlign="Right"
                    Visible="true" HeaderStyle-CssClass="formatoValuta"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_OP_CONTAB" HeaderText="CODICE OPERAZIONE CONTABILE"
                    ItemStyle-HorizontalAlign="Center" Visible="true" HeaderStyle-CssClass="formatoTesto">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CUP" HeaderText="CUP" ItemStyle-HorizontalAlign="Center"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
                <asp:BoundColumn DataField="CIG" HeaderText="CIG" ItemStyle-HorizontalAlign="Center"
                    Visible="true" HeaderStyle-CssClass="formatoTesto"></asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#999999" />
            <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                HorizontalAlign="Center" />
            <ItemStyle ForeColor="#000000" />
            <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
        </asp:DataGrid>
    </div>
    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="true"
        Title="Esercizi finanziari" Width="400px" Height="400px" VisibleStatusbar="false"
        Behaviors="Pin, Maximize, Move, Resize">
        <ContentTemplate>
            <table border="0" cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <asp:Button ID="ButtonScarica" Text="Estrai" runat="server" />
                        <asp:Button ID="ButtonEsci" Text="Esci" runat="server" OnClientClick="closeWindow(null, null, 'RadWindow1');return false;" />
                    </td>
                </tr>
                <tr>
                    <td style="height:10px;">
                    </td>
                </tr>
                <tr>
                    <td style="overflow: auto">
                        <fieldset>
                            <legend>Esercizi finanziari</legend>
                            <asp:CheckBoxList runat="server" ID="chkBoxList">
                        </asp:CheckBoxList>
                        </fieldset>
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    </form>
</body>
</html>
