<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaBolletteRuolo.aspx.vb"
    Inherits="Contratti_Pagamenti_RicercaBolletteRuolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <style type="text/css">
        #form1
        {
            width: 780px;
        }
    </style>
    <script type="text/javascript" language="javascript">

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
    </script>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <table style="width: 100%;">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Ricerca report
                    RUOLI </span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 80%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblCognome2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Cod. Contratto</asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCodContratto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDataOp" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Num. Ruolo</asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNumRuolo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDataOp0" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Data Pagamento dal</asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDataPagamento" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataPagamento"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" ValidationGroup="Auto" Display="Dynamic" Visible="False"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPeriodoal0" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                            TabIndex="-1" Visible="False">al</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataPagamentoAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataPagamentoAl"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" ValidationGroup="Auto" Display="Dynamic" Visible="False"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="trRiferimento0">
                        <td id="trRiferimento">
                            <asp:Label ID="lblPeriodo" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Periodo Riferim. Boll. dal</asp:Label>
                        </td>
                        <td>
                            <table id="trRiferimento2">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRiferimentoDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtRiferimentoDal"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" ValidationGroup="Auto" Display="Dynamic" Visible="False"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPeriodoal" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                            TabIndex="-1" Visible="False">al</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRiferiemntoAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="90px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtRiferiemntoAl"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" ValidationGroup="Auto" Display="Dynamic" Visible="False"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False">Dettaglio</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDettaglio" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSgravio" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False">Sgravio</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSgravio" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: left; vertical-align: top">
                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                                ToolTip="Avvia Ricerca" CausesValidation="False" Style="height: 20px" />
                        </td>
                        <td style="text-align: left; vertical-align: top">
                            <img onclick="document.location.href='../pagina_home.aspx';" alt="Home" src="../../NuoveImm/Img_Home.png"
                                style="cursor: pointer;" title="Torna alla pagine Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="confcsv" runat="server" Value="0" />
    <asp:HiddenField ID="datacorretta" Value="0" runat="server" />
    <asp:HiddenField ID="tipoPagamanto" runat="server" />
    </form>
</body>
<script type="text/javascript" language="javascript">

    initialize();
    function initialize() {

        $("#txtDataPagamento").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtDataPagamentoAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });

        $("#txtRiferimentoDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#txtRiferiemntoAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });

    }
    
</script>
</html>
