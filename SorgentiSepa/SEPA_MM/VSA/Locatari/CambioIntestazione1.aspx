<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioIntestazione1.aspx.vb"
    Inherits="CambioIntestazione1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;


    function $onkeydown() {

        if (event.keyCode == 13) {
            alert('Usare il tasto <Avvia Ricerca>');
            history.go(0);
            event.keyCode = 0;
        }
    } 

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cambio Intestazione</title>
    <style type="text/css">
        .stile_tabella
        {
            width: 100%;
            margin-top: 5%;
            margin-left: 15px;
        }
        
        .pulsante
        {
            margin-left: 65%;
            margin-top: 35%;
        }
    </style>
</head>
<body background="../../NuoveImm/SfondoMaschere.jpg">
    <form id="form1" runat="server">
    <div>
        <table style="margin-top: 20px">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; font-weight: bold;
                        top: 35px;">Cambio Intestazione </span>
                </td>
            </tr>
        </table>
        <table class="stile_tabella" cellpadding="4" cellspacing="1">
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt">Protocollo:</asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <%-- <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt">Domanda di:</asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTipo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt">Domanda attualmente intestata a:</asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt">Nuovo Intestatario:</asp:Label>
                </td>
                <td>
                    <%--<asp:DropDownList ID="cmbNucleo" runat="server" BackColor="White" Height="20px" Style="border-right: #801f1c 2px solid;
                        border-top: #801f1c 2px solid; border-left: #801f1c 2px solid;border-bottom: #801f1c 2px solid;" TabIndex="1" Width="350px">
                    </asp:DropDownList>
                    --%>
                    <asp:DropDownList ID="cmbNucleo" runat="server" BackColor="#D8F9FC" Height="20px"
                        Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                        border-bottom: black 1px solid;" TabIndex="1" Width="350px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                        ForeColor="#C00000" Visible="False">***</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="pulsante">
        <asp:ImageButton ID="btnSalva" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Procedi.png"
            TabIndex="2" ToolTip="Procedi" OnClientClick="ConfermaCambio();" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
            TabIndex="3" ToolTip="Indietro" OnClientClick="self.close();" />
    </div>
    <asp:HiddenField ID="conferma" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    function ConfermaCambio() {
        var chiediConferma
        var msg1 = "Attenzione, sei sicuro di voler cambiare l'intestario della domanda?"
        chiediConferma = window.confirm(msg1);
        if (chiediConferma == true) {

            document.getElementById('conferma').value = '1';

        }
        else {
            document.getElementById('conferma').value = '0';

        }
    }
</script>
</html>
