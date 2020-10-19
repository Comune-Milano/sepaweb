<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnulloDomanda.aspx.vb"
    Inherits="VSA_Locatari_AnnulloDomanda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Annullo Domanda Gest. Locatari</title>
    <style type="text/css">
        .stile_tabella
        {
            width: 100%;
            margin-top: 5%;
            margin-left: 15px;
        }
        
        .pulsante
        {
            margin-left: 80%;
            margin-top: 25%;
        }
    </style>
</head>
<body style="background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="4">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Annullo
                        Domanda Gest. Locatari</strong></span>
                </td>
            </tr>
        </table>
        <table class="stile_tabella">
            <tr>
                <td width="750px" style="font-family: arial, Helvetica, sans-serif; font-size: 9pt;
                    vertical-align: top;">
                    Inserire il protocollo della domanda e cliccare sull'icona di ricerca.
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Num. protocollo" Font-Size="9pt" Font-Names="Arial"
                                    Font-Bold="True" Width="195px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPG" runat="server" Width="120px" BackColor="#D8F9FC"></asp:TextBox>
                                <asp:ImageButton ID="btnCercaPG" runat="server" ImageUrl="../../Condomini/Immagini/Search_16x16.png"
                                    ToolTip="Ricerca Domanda" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDataEvento" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data evento"
                                    Width="180px"></asp:Label>
                            </td>
                            <td width="200px">
                                <asp:TextBox ID="txtDataEvento" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataEvento"
                                    Display="Dynamic" ErrorMessage="!" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Formato gg/mm/aaaa" Width="5px"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="180px">
                                <asp:Label ID="lblDataPr" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data presentazione"
                                    Width="180px"></asp:Label>
                            </td>
                            <td width="110px">
                                <asp:TextBox ID="txtDataPr" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataPr"
                                    Display="Dynamic" ErrorMessage="!" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="190px">
                                <asp:Label ID="lblInizio" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Inizio Validità"
                                    Width="180px"></asp:Label>
                            </td>
                            <td width="110px">
                                <asp:TextBox ID="txtDataIn" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataIn"
                                    Display="Dynamic" ErrorMessage="!" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                            </td>
                            <td width="100px">
                                <asp:Label ID="lblFine" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Fine Validità"
                                    Width="91px"></asp:Label>
                            </td>
                            <td width="200px">
                                <asp:TextBox ID="txtDataFine" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataFine"
                                    Display="Dynamic" ErrorMessage="!" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                            </td>
                            <td width="175px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInfoDomanda" runat="server" Font-Bold="False" Font-Names="Arial"
                        Font-Size="9pt" Width="630px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErr" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                        ForeColor="#C00000" Visible="False">Domanda non trovata!</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="pulsante">
        <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
            ToolTip="Procedi" OnClientClick="ConfermaAnnullo();" Visible="false" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            ToolTip="Home" OnClientClick="self.close();" />
    </div>
    <asp:HiddenField ID="confermaAnnullo" runat="server" />
    <asp:HiddenField ID="codcontratto" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    if (document.getElementById('divLoading') != null) {
        document.getElementById('divLoading').style.visibility = 'hidden';
    };
    function CompletaData(e, obj) {

        var sKeyPressed;
        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
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
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    };

    function ConfermaAnnullo() {
        var sicuro = window.confirm('Attenzione...Sei sicuro di voler annullare la domanda?');
        if (sicuro == true) {
            document.getElementById('confermaAnnullo').value = '1';
        }
        else {
            document.getElementById('confermaAnnullo').value = '0';
        }
    };

</script>
</html>
