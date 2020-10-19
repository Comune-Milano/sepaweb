<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportSitIncassiRuoli.aspx.vb"
    Inherits="Contabilita_Report_ReportSitIncassiRuoli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Incassi Ruoli</title>
    <link type="text/css" href="../../Contratti/Pagamenti/css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="../../Contratti/Pagamenti/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Contratti/Pagamenti/js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="../../Contratti/Pagamenti/js/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <style type="text/css">
        #form1
        {
            width: 782px;
            height: 541px;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table cellpadding="1" cellspacing="0" width="98%">
        <tr>
            <td style="width: 100%; font-family: Arial; font-size: 4pt;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:Label ID="Label1" Text="Ricerca Situazione Incassi" runat="server" Font-Bold="True"
                    Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%; font-family: Arial; font-size: 3pt;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td colspan="5">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 23%">
                            <asp:Label ID="Label7" runat="server" Text="Data pagamento dal" Font-Names="Arial"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="TextBoxDataPagamentoDal" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td style="width: 2%">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="!"
                                ControlToValidate="TextBoxDataPagamentoDal" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 3%">
                            <asp:Label ID="Label13" runat="server" Text="al" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="TextBoxDataPagamentoAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td style="width: 5%">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="!"
                                ControlToValidate="TextBoxDataPagamentoAl" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 18%">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="width: 5%; text-align: center;">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 3%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 23%">
                            <asp:Label ID="Label20" runat="server" Text="Data riferim. bolletta dal" Font-Names="Arial"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="TextBoxRiferimentoDal" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td style="width: 2%">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="!"
                                ControlToValidate="TextBoxRiferimentoDal" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 3%">
                            <asp:Label ID="Label21" runat="server" Text="al" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="TextBoxRiferimentoAl" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="70px"></asp:TextBox>
                        </td>
                        <td style="width: 5%">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="!"
                                ControlToValidate="TextBoxRiferimentoAl" Font-Bold="True" Font-Names="Arial"
                                Font-Size="9pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 18%">
                            &nbsp;
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="width: 5%; text-align: center;">
                        </td>
                        <td style="width: 10%">
                        </td>
                        <td style="width: 3%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 22%">
                            <asp:Label ID="Label56" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Tipologia di incasso"></asp:Label>
                        </td>
                        <td style="width: 40%">
                            <asp:DropDownList ID="DropDownListTipoIncasso" runat="server" Font-Names="Arial"
                                Font-Size="9pt" Width="100%" onchange="IsAssegno(this);">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 15%">
                            <asp:Label ID="lblNumAssegno" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Num. Assegno"
                                Style="visibility: hidden"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxNumeroAssegno" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="40%" Style="visibility: hidden"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td style="width: 22%">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Cod. contratto"></asp:Label>
                        </td>
                        <td style="width: 40%">
                            <asp:TextBox ID="txtCodContratto" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 48%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                &nbsp
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: right">
                <table cellpadding="1" cellspacing="1" width="100%" style="margin-top: 200px;">
                    <tr>
                        <td style="text-align: right" width="90%">
                            <asp:ImageButton ID="ImageButtonAvanti" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                                ToolTip="Avanti" />
                        </td>
                        <td style="text-align: right" width="10%">
                            <asp:ImageButton ID="ImageButtonEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                ToolTip="Esci" AlternateText="Esci" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
<script type="text/javascript" language="javascript">
    if (document.getElementById('divLoading') != null) {
    }
    document.getElementById('divLoading').style.visibility = 'hidden';
    function CompletaData(e, obj) {
        var sKeyPressed;
        sKeyPressed = (window.event) ? event.keyCode : e.which;
        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                if (window.event) {
                    event.keyCode = 0;
                } else {
                    e.preventDefault();
                }
            }
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
                    }
                }
            }
        }
    }
    function IsAssegno(obj) {

        if (obj.value == 5) {
            document.getElementById('lblNumAssegno').style.visibility = 'visible';
            document.getElementById('TextBoxNumeroAssegno').style.visibility = 'visible';

        }
        else {

            document.getElementById('lblNumAssegno').style.visibility = 'hidden';
            document.getElementById('TextBoxNumeroAssegno').style.visibility = 'hidden';
            document.getElementById('TextBoxNumeroAssegno').value = '';
        };
    };
    initialize();
    function initialize() {

        $("#TextBoxDataPagamentoDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#TextBoxDataPagamentoAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });

        $("#TextBoxRiferimentoDal").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
        $("#TextBoxRiferimentoAl").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });

    }
</script>
</html>
