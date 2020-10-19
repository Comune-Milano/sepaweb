<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggNucleoSingoloRU.aspx.vb"
    Inherits="Contratti_AggNucleoSingoloRU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aggiorna Nucleo</title>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
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
            margin-top: 10%;
        }
    </style>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server" target="modal">
    <div style="width: 520px;">
        <table>
            <tr>
                <td style="padding-top: 10px;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; font-weight: bold;">
                        Aggiornamento Nucleo per applicazione AU </span>
                </td>
            </tr>
        </table>
        <table class="stile_tabella" cellpadding="1" cellspacing="1">
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Text=" Selezionare il bando di riferimento"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbBando" runat="server" AutoPostBack="True" BackColor="#D8F9FC" Height="20px"
                        Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                        border-bottom: black 1px solid;" TabIndex="1" Width="350px">
                    </asp:DropDownList>
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
                    <asp:Label ID="lblMess" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="#801F1C"
                        Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="pulsante">
        <asp:ImageButton ID="btnProcedi" runat="server" CausesValidation="False" ImageUrl="../NuoveImm/Img_Procedi.png"
            TabIndex="2" ToolTip="Procedi" OnClientClick="ConfermaAggiornam();" />
        <img id="exit" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" title="Esci" style="cursor: pointer"
            onclick="CloseModal(document.getElementById('conferma').value)" />
        <asp:Button ID="btnControllaProcedim" runat="server" Style="display: none" />
    </div>
    <asp:HiddenField ID="conferma" runat="server" />
    <asp:HiddenField ID="idContr" runat="server" />
    <asp:HiddenField ID="altriProcedim" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility = 'hidden';


    function CloseModal(returnParameter) {
        window.returnValue = returnParameter;
        window.close();
    };

    function ConfermaAggiornam() {
        var chiediConferma;
        var selezionato = false;
        var msg1 = "Attenzione, procedendo verrà aggiornato il nucleo per il rapporto su cui è stata applicata l\'AU selezionata. Continuare?"
        var msg2 = "Attenzione, esiste una domanda di gestione locatari successiva che ha aggiornato il nucleo del contratto. Continuare?";
        if (document.getElementById('cmbBando').value == '-1') {
            alert('Selezionare il bando prima di procedere!');
            document.getElementById('conferma').value = '0';
        }
        else {

            if (document.getElementById('altriProcedim').value == '1') {
                chiediConferma = window.confirm(msg2);
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
            else {
                chiediConferma = window.confirm(msg1);
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
        }
    }
</script>
</html>
