<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRptPgExtraMav.aspx.vb"
    Inherits="Contratti_Pagamenti_RicercaRptPgExtraMav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">

</script>
<head id="Head1" runat="server">
    <title>RICERCA</title>
    <style type="text/css">
        #form1
        {
            width: 780px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function confCSV() {
            var txtEventoDal = document.getElementById("txtEventoDal").value;
            var cmbOperatori = document.getElementById("cmbOperatori").value;
            var espressione = /^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/;
            if (!espressione.test(txtEventoDal)) {
                document.getElementById("datacorretta").value = '0';
            } else {
                document.getElementById("datacorretta").value = '1';
            }
            if (txtEventoDal == '') {
                document.getElementById("datacorretta").value = '1';
            }
            if (document.getElementById("datacorretta").value == '1') {
                if ((txtEventoDal == '') || (cmbOperatori == -1)) {

                    var chiedicsv;
                    chiedicsv = window.confirm('Attenzione...La ricerca effettuata è impossibile da visualizzare. Esportare in un file .csv?');
                    if (chiedicsv == true) {
                        document.getElementById('confcsv').value = '1';
                    }
                }
            }
        }
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
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Ricerca Report
                    Pagamenti Manuali
                    <asp:Label ID="lblTitolo" runat="server" Font-Size="14pt" Font-Names="Arial" Font-Bold="True"
                        ForeColor="#801F1C"></asp:Label></span></strong>
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
                                TabIndex="-1">Operatore</asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbOperatori" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="230px">
                                        </asp:DropDownList>
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
                                TabIndex="-1">Data Operazione</asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtEventoDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="erroredata" runat="server" ForeColor="Red"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEventoDal"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" ValidationGroup="Auto" Display="Dynamic" Visible="False"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDataOpal" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                            TabIndex="-1" Visible="False">al</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEventoAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEventoAl"
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
                            <asp:Label ID="lblDataOp0" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Data Pagamento dal</asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDataPagamento" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="70px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEventoDal"
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
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="70px"></asp:TextBox>
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
                                TabIndex="-1">Periodo di Riferimento Dal</asp:Label>
                        </td>
                        <td>
                            <table id="trRiferimento2">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtRiferimentoDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="70px"></asp:TextBox>
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
                                            Style="" Font-Names="arial" Font-Size="10pt" Width="70px"></asp:TextBox>
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
                            <asp:Label ID="lblCognome3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipo Pagamento</asp:Label>
                        </td>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbTipoPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="230px">
                                        </asp:DropDownList>
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
                            <asp:Label ID="lblSgravio" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                Visible="False">Sgravio</asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkSgravio" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" />
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
                            <asp:CheckBox ID="chkDettaglio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Text="RISULTATO DETTAGLIATO PER OPERATORE" />
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
    <script language="javascript" type="text/javascript">

        if (document.getElementById('tipoPagamanto').value == 'R') {
            
            document.getElementById('trRiferimento0').style.display = 'none';
        }
        else {

            document.getElementById('trRiferimento').style.display = 'block';
            document.getElementById('trRiferimento2').style.display = 'block';

        };
    </script>
</body>
</html>
