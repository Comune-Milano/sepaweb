<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaAppalti.aspx.vb"
    Inherits="MANUTENZIONI_RicercaAppalti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Ricerca contratti</title>
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

        function ControllaPIVA(pi) {
            risultato = '0';
            if (pi == '') {
                document.getElementById('PIVA').value = '0';
                return '';
            }
            if (pi.length != 11) {
                alert("La lunghezza della partita IVA non è\n" +
                    "corretta: la partita IVA dovrebbe essere lunga\n" +
                    "esattamente 11 caratteri.\n");
                document.getElementById('PIVA').value = '1';
                return "1";
            }
            validi = "0123456789";
            for (i = 0; i < 11; i++) {
                if (validi.indexOf(pi.charAt(i)) == -1) {
                    alert("La partita IVA contiene un carattere non valido `" +
                        pi.charAt(i) + "'.\nI caratteri validi sono le cifre.\n");
                    document.getElementById('PIVA').value = '1';
                    return "1";
                }
            }
            s = 0;
            for (i = 0; i <= 9; i += 2)
                s += pi.charCodeAt(i) - '0'.charCodeAt(0);
            for (i = 1; i <= 9; i += 2) {
                c = 2 * (pi.charCodeAt(i) - '0'.charCodeAt(0));
                if (c > 9) c = c - 9;
                s += c;
            }
            if ((10 - s % 10) % 10 != pi.charCodeAt(10) - '0'.charCodeAt(0)) {
                alert("La partita IVA non è valida:\n" +
                    "il codice di controllo non corrisponde.\n");
                document.getElementById('PIVA').value = '1';
                return '1';
            }
        }

        function VerPIVA() {

            document.getElementById('PIVA').value = '0';

            if (document.getElementById('txtPIva').value != '') {
                ControllaPIVA(document.getElementById('txtPIva').value);
            }

        }

    </script>
    <style type="text/css">
        .style1 {
            height: 24px;
        }

        .style2 {
            width: 138px;
        }

        .style3 {
            height: 24px;
            width: 138px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <table style="width: 100%" class="FontTelerik">
            <tr>
                <td class="TitoloModulo">Contratti - Ricerca
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia ricerca"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnRicarica" runat="server" Text="Pulisci filtri" ToolTip="Pulisci filtri di ricerca"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Torna alla home"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 50%">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblServizio2" runat="server">Tipologia Contratto</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rdbType" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="0" Text="PATRIMONIALE" />
                                        <telerik:RadComboBoxItem Value="1" Text="NON PATRIMONIALE" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label3" runat="server">Numero repertorio</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtnumero" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50"
                                    TabIndex="1" Width="131px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <asp:Label ID="Label23" runat="server">Fornitore</asp:Label>
                            </td>
                            <td class="style1">
                                <telerik:RadComboBox ID="cmbfornitore" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="100%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label2" runat="server">Esercizio Finanziario</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbesercizio" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="100%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblStruttura" runat="server">Struttura</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbStruttura" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="100%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblLotto" runat="server">Lotto</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmblotto" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="100%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescrizione" runat="server" Font-Bold="False" 
                                    >Descrizione</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescrizione" runat="server" 
                                    Width="100%" Font-Names="ARIAL" Font-Size="8pt" MaxLength="50" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label19" runat="server">Data repertorio dal</asp:Label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerik:RadDatePicker ID="txtdatadal" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
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
                                        <td style="text-align: center">
                                            <asp:Label ID="Label20" runat="server">&nbsp;&nbsp;&nbsp;al&nbsp;&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker ID="txtdataal" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
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
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label1" runat="server">CIG</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxCIG" TabIndex="6" runat="server" MaxLength="50" ToolTip="CIG"
                                    Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label24" runat="server" Font-Bold="False" >Direttore Lavori</asp:Label>
                            </td>
                            <td>
                                                                  <telerik:RadComboBox ID="cmbDirLavori" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="100%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="vertical-align: middle">
                                <img src="../../../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Utilizzare <b>*</b> come carattere jolly nelle ricerche</i></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
