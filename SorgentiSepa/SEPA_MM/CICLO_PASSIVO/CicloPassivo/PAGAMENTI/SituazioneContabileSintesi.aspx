<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneContabileSintesi.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_SituazioneContabileSintesi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Situazione Contabile Generale per Esercizio Finanziario</title>
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
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div style="height: 500px" class="FontTelerik">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td colspan="4" class="TitoloModulo">Report - Situazione contabile - Sintesi
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style2">
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
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="style1"></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <asp:Label ID="Label1" runat="server" Text="Esercizio contabile"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="DropDownListEsercizioContabile" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                            ResolvedRenderMode="Classic" Width="40%">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <%--<asp:Label ID="Label3" runat="server" Text="Data esercizio al" Font-Names="Arial"
                        Font-Size="9pt"></asp:Label>--%>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td style="visibility: hidden">
                        <asp:TextBox ID="TextBoxAl" runat="server" Width="75px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxAl"
                            Display="Dynamic" ErrorMessage="!" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                            TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="100%"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <asp:Label ID="Label3" runat="server" Text="Data Cdp dal"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxCDPDal" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                            </tr>
                        </table>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="Label6" runat="server" Text="al"></asp:Label>
                    </td>
                    <td style="width: 55%">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxCDPAl" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                        <asp:Label ID="Label4" runat="server" Text="Data fattura dal"></asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxDataFatturaDal" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="al"></asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxDataFatturaAl" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Data reg. fattura dal"></asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxDataRegistrazioneFatturaDal" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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

                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="al" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxDataRegistrazioneFatturaAl" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%">
                        <asp:Label ID="Label5" runat="server" Text="Data pagamento dal"></asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxDataPagamentoDal" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="al"></asp:Label>
                    </td>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <telerik:RadDatePicker ID="TextBoxDataPagamentoAl" runat="server" DataFormatString="{0:dd/MM/yyyy}"
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
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: right">
        </div>
    </form>
</body>
</html>
