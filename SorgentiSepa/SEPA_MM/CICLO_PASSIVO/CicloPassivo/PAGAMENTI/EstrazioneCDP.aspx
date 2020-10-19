<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstrazioneCDP.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_EstrazioneCDP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estrazione Pagamenti</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div style="height: 500px">
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td colspan="2" class="TitoloModulo">
                   Report - Situazione contabile - Estrazione Pagamenti
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnStampa" runat="server" Text="Stampa" ToolTip="Stampa" />
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
                <td class="style2" style="width: 10%">
                    <asp:Label ID="Label1" runat="server" Text="Esercizio contabile"></asp:Label>
                </td>
                <td class="style3">
                    <telerik:RadComboBox ID="DropDownListEsercizioContabile" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="300px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label ID="Label3" runat="server" Text="Fornitore"></asp:Label>
                </td>
                <td style="width: 80%">
                    <telerik:RadComboBox ID="DropDownListFornitori" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label ID="Label4" runat="server" Text="Data CDP da"></asp:Label>
                </td>
                <td style="width: 80%">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadDatePicker ID="TextBoxDataCDPda" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                                <asp:Label ID="Label5" runat="server" Text="a" Width="20px" Style="text-align: center"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="TextBoxDataCDPa" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                    <asp:Label ID="Label6" runat="server" Text="Repertorio"></asp:Label>
                </td>
                <td style="width: 80%">
                    <telerik:RadComboBox ID="DropDownListRepertorio" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="20%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Label ID="Label7" runat="server" Text="Voce DGR"></asp:Label>
                </td>
                <td style="width: 80%">
                    <telerik:RadComboBox ID="DropDownListVoceDGR" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="40%">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
