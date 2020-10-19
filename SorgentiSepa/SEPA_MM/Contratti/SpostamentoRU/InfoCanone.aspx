<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InfoCanone.aspx.vb" Inherits="Contratti_SpostamentoRU_InfoCanone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Info Canone</title>
    <style type="text/css">
        .stile_tabella
        {
            width: 100%;
            margin-top: 5%;
            margin-left: 15px;
        }
        .stile_tabella2
        {
            width: 650px;
            margin-left: 15px;
        }
        .pulsante
        {
            margin-left: 65%;
            margin-top: 35%;
        }
        .stileEtichette
        {
            font-family: Arial;
            font-size: 9pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="riepilogo" style="width: 800px; left: 0px; background-repeat: no-repeat;
        background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg'); z-index: 500;
        position: absolute; top: 0px; height: 457px;">
        <br />
        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp Canone
            per stipula contratto </strong>
            <asp:Label ID="lblTipoContr" runat="server" Font-Size="14pt" Font-Bold="True"></asp:Label></span><br />
        <table class="stile_tabella2">
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" Font-Size="9pt" Font-Bold="True" Width="100%" Font-Names="Arial">Informazioni importate dall'attuale posizione contrattuale. Se necessario modificare prima di procedere.</asp:Label><br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <table class="stile_tabella2" cellpadding="4" cellspacing="1">
            <tr>
                <td class="stileEtichette">
                    Canone Annuo Attuale
                </td>
                <td>
                    <asp:TextBox ID="txtCanoneAttuale" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCanoneAttuale"
                        ErrorMessage="non valido! (0,00)" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                        ValidationExpression="\b\d*,\d{2}\b" ForeColor="red"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    PG Provvedimento
                </td>
                <td>
                    <asp:TextBox ID="txtNumProvv" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Data Provvedimento
                </td>
                <td>
                    <asp:TextBox ID="txtDataProvv" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtDataProvv" ErrorMessage="non valido! (gg/mm/aaaa)" Font-Bold="True"
                        Font-Names="arial" ForeColor="red" Font-Size="9pt" Height="15px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="155px"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <table class="stile_tabella2">
            <tr>
                <td style="font-family: Arial; font-size: 10pt; font-weight: bold; color: #801f1C">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <table class="stile_tabella">
            <tr>
                <td align="center">
                    <asp:Button ID="btnChiudiContratto" runat="server" BackColor="#990000" Font-Bold="True"
                        Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="250px"
                        Visible="False" Text="CHIUDI CONTRATTO e CREA NUOVO" 
                        OnClientClick="ConfChiudiApriContr();" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp&nbsp&nbsp<asp:Image ID="imgAlert" runat="server" ImageUrl="../../IMG/Alert.gif" />
                    &nbsp;<asp:Label ID="lblAlert" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Font-Italic="True">Cliccare su Procedi per assegnare l'alloggio selezionato</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 580px;">
                    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        TabIndex="30" ToolTip="Procedi" OnClientClick="ConfAssegnazione();" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        TabIndex="31" ToolTip="Esci" OnClientClick="self.close();" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="ConfAssegn" runat="server" Value="0" />
    <asp:HiddenField ID="ConfChiudiApri" runat="server" Value="0" />
    </form>
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
        function ConfAssegnazione() {
            chiediConferma = window.confirm('Attenzione...procedendo verrà assegnato l\'alloggio selezionato. Sei sicuro di voler continuare?');
            if (chiediConferma == true) {
                document.getElementById('ConfAssegn').value = '1';
            }
            else {
                document.getElementById('ConfAssegn').value = '0';
            }
        }
        function ConfChiudiApriContr() {
            chiediConferma = window.confirm('Attenzione...procedendo verrà CHIUSO l\'attuale contratto e creato un nuovo in BOZZA. Sei sicuro di voler continuare?');
            if (chiediConferma == true) {
                document.getElementById('ConfChiudiApri').value = '1';
            }
            else {
                document.getElementById('ConfChiudiApri').value = '0';
            }
        }
    </script>
</body>
</html>
