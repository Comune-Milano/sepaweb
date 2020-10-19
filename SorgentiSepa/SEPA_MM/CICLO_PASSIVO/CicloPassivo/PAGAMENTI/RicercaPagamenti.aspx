<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPagamenti.aspx.vb"
    Inherits="PAGAMENTI_RicercaPagamenti" %>

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
    <style type="text/css">
        .style1
        {
            width: 30px;
            height: 23px;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            width: 180px;
            height: 23px;
        }
        .style4
        {
            width: 200px;
            height: 23px;
        }
        .style5
        {
            width: 20%;
        }
        .style6
        {
            height: 21px;
            width: 20%;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="FontTelerik">
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                    Ordini - Ordini e pagamenti - Ricerca - Selettiva
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia ricerca" ToolTip="Avvia ricerca"
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
                <td>
                    <div style="overflow: auto; height: 320px">
                        <table style="width: 50%">
                            <tr>
                                <td class="style5">
                                    <asp:Label ID="lblStruttura" runat="server" Width="110px">Struttura</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbStruttura" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    <asp:Label ID="Label4" runat="server" Width="110px">Esercizio Finanziario</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEsercizio" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="LblFornitore" runat="server" Width="120px">Beneficiario</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbFornitore" Width="100%" AppendDataBoundItems="true" Filter="Contains"
                                        Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    <asp:Label ID="lblStato" runat="server" Width="120px">Stato</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbStato" Width="70%" AppendDataBoundItems="true" Filter="Contains"
                                        Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    <asp:Label ID="lblDataP1" runat="server">Data ordine dal</asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 200px">
                                               


                                                         <telerik:RadDatePicker ID="txtDataP1" runat="server" WrapperTableCaption=""
                                    MaxDate="01/01/9999" DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                                    Width="110">
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
                                                <asp:Label ID="lblDataP2" runat="server">Data ordine al</asp:Label>
                                            </td>
                                            <td style="width: 10px">&nbsp;</td>
                                            <td style="width: 200px">
                                                    <telerik:RadDatePicker ID="txtDataP2" runat="server" WrapperTableCaption=""
                                    MaxDate="01/01/9999" DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                                    Width="110">
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
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    <asp:Label ID="lblDataE1" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                        Width="120px" Visible="False">Data emissione dal</asp:Label>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtDataE1" runat="server" MaxLength="10" Style="left: 144px; top: 192px"
                                                    TabIndex="7" ToolTip="gg/mm/aaaa" Width="70px" Visible="False"></asp:TextBox><asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataE1"
                                                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                        Width="150px" Visible="False"></asp:RegularExpressionValidator>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:Label ID="lblDataE2" runat="server" Style="z-index: 104; left: 48px; top: 64px"
                                                    Width="120px" Visible="False">Data emissione al</asp:Label>
                                            </td>
                                            <td style="width: 200px">
                                                <asp:TextBox ID="txtDataE2" runat="server" MaxLength="10" Style="left: 144px; top: 192px"
                                                    TabIndex="8" ToolTip="gg/mm/aaaa" Width="70px" Visible="False"></asp:TextBox><asp:RegularExpressionValidator
                                                        ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataE2"
                                                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                        Width="150px" Visible="False"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 50%">
                            <tr>
                                <td style="height: 21px" colspan="2">
                                    &nbsp;
                                </td>
                                <td style="width: 180px">
                                </td>
                                <td style="width: 30px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 180px">
                                    &nbsp;
                                </td>
                                <td style="width: 30px">
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 200px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="width: 180px">
                                </td>
                                <td style="width: 30px">
                                </td>
                                <td>
                                </td>
                                <td style="width: 200px">
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;
                                </td>
                                <td class="style1">
                                    &nbsp;
                                </td>
                                <td class="style2">
                                    &nbsp;
                                </td>
                                <td class="style4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td style="width: 180px">
                                    &nbsp;
                                </td>
                                <td style="width: 30px">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td style="width: 200px">
                                    &nbsp;
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
