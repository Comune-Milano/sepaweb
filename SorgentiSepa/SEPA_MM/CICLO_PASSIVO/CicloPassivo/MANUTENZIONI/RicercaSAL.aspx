<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSAL.aspx.vb" Inherits="MANUTENZIONI_RicercaSAL" %>

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
    <link href="../../../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.min.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <style type="text/css">
        
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div class="FontTelerik">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%" class="TitoloModulo">
                   Ordini - Manutenzioni e servizi - SAL - Nuovo
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
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
                    <table cellspacing="7" cellpadding="2">
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label2" runat="server">Esercizio Finanziario*</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbEsercizio" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="LblFornitore" runat="server">Fornitore*</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbFornitore" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="LblAppalto" runat="server">Appalto*</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbAppalto" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label3" runat="server">Voce DGR</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="cmbServizioVoce" Width="90%" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label4" runat="server">Progetto Vision</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="DropDownListProgettoVision" Width="90%" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label5" runat="server">Numero SAL Vision</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadComboBox ID="DropDownListNumeroSALVision" Width="90%" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label ID="Label1" runat="server">Data emissione ordine dal:</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadDatePicker ID="txtDataDAL" runat="server" WrapperTableCaption="" MaxDate="01/01/9999">
                                    <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa" Width="70px">
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
                            <td style="width: 20%">
                                <asp:Label ID="lblDataP2" runat="server">Data emissione ordine al:</asp:Label>
                            </td>
                            <td style="width: 80%">
                                <telerik:RadDatePicker ID="txtDataAL" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                    DataFormatString="{0:dd/MM/yyyy}" Width="110">
                                    <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                        Width="70px">
                                        <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                    </DateInput>
                                    <Calendar ID="Calendar2" runat="server">
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
    </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#txtDataDAL").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
            $("#txtDataAL").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        });
    </script>
</body>
</html>
