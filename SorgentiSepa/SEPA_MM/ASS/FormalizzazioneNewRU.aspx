<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormalizzazioneNewRU.aspx.vb"
    Inherits="ASS_FormalizzazioneNewRU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;
    vertical-align: top;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr style="height: 35px; vertical-align: bottom">
            <td colspan="2">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;&nbsp;Assegnazione
                    per la stipula del Contratto</strong></span>
            </td>
        </tr>
        <tr style="height: 35px;">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 250px;">
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblProvvedimento" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt">Numero Protocollo*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtpg" runat="server" MaxLength="50" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDataProvvedimento" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt">Data Protocollo*</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDataProvvedimento" runat="server" MaxLength="10" ToolTip="dd/MM/yyyy"
                                            Width="81px" CssClass="CssMaiuscolo" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataProvvedimento"
                                            ErrorMessage="!" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Font-Names="arial" Font-Size="8pt" ForeColor="red" ToolTip="Modificare la data del Provvedimento"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 40px;">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 222px">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 60%">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
                                OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            <img alt="Esci" title="Esci dalla Maschera di Abbinamento" src="../NuoveImm/Img_Esci_AMM.png"
                                onclick="caricamentoincorso();self.close();" style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript" language="javascript">
        function caricamentoincorso() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'visible';
                    };
                }
                else {
                    alert('ATTENZIONE! Ci sono delle incongruenze nei dati della pagina!');
                };
            }
            else {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'visible';
                };
            };
        };
        function CompletaData(e, obj) {
            var sKeyPressed;
            var n;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if ((sKeyPressed < 48) || (sKeyPressed > 57)) {
                if ((sKeyPressed != 8) && (sKeyPressed != 0)) {
                    if (window.event) {
                        if (navigator.appName == 'Microsoft Internet Explorer') {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        e.preventDefault();
                    };
                };
            }
            else {
                if (obj.value.length == 0) {
                    if ((sKeyPressed < 48) || (sKeyPressed > 51)) {
                        if (window.event) {
                            if (navigator.appName == 'Microsoft Internet Explorer') {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        }
                        else {
                            e.preventDefault();
                        };
                    };
                }
                else if (obj.value.length == 1) {
                    if (obj.value == 3) {
                        if (sKeyPressed < 48 || sKeyPressed > 49) {
                            if (window.event) {
                                if (navigator.appName == 'Microsoft Internet Explorer') {
                                    event.keyCode = 0;
                                }
                                else {
                                    e.preventDefault();
                                };
                            }
                            else {
                                e.preventDefault();
                            };
                        };
                    };
                }
                else if (obj.value.length == 2) {
                    if ((sKeyPressed < 48) || (sKeyPressed > 49)) {
                        if (window.event) {
                            if (navigator.appName == 'Microsoft Internet Explorer') {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        obj.value += "/";
                    };
                }
                else if (obj.value.length == 4) {
                    n = obj.value.substr(3, 1);
                    if (n == 1) {
                        if ((sKeyPressed < 48) || (sKeyPressed > 50)) {
                            if (window.event) {
                                if (navigator.appName == 'Microsoft Internet Explorer') {
                                    event.keyCode = 0;
                                }
                                else {
                                    e.preventDefault();
                                };
                            }
                            else {
                                e.preventDefault();
                            };
                        };
                    };
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            if (navigator.appName == 'Microsoft Internet Explorer') {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        }
                        else {
                            e.preventDefault();
                        };
                    };
                };
            };
        };
        function AutoDecimal(obj, numdec) {
            if (numdec == null) numdec = 2;
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(numdec);
                if (a != 'NaN') {
                    if (numdec > 0) {
                        if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                            var decimali = a.substring(a.length, a.length - numdec);
                            var dascrivere = a.substring(a.length - (numdec + 1), 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                            };
                            risultato = dascrivere + risultato + ',' + decimali;
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace('.', ',');
                        };
                    }
                    else {
                        if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                            var dascrivere = a.substring(a.length, 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                            };
                            risultato = dascrivere + risultato;
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace('.', ',');
                        };

                    };
                }
                else {
                    document.getElementById(obj.id).value = '';
                };
            };
        };
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                };
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            };
        };
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
</body>
</html>
