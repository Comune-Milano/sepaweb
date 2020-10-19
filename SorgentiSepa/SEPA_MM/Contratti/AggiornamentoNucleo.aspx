<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggiornamentoNucleo.aspx.vb"
    Inherits="Contratti_AggiornamentoNucleo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aggiornamento Nucleo</title>
    <style type="text/css">
        .stile_tabella
        {
            width: 100%;
            margin-top: 5%;
            margin-left: 15px;
        }
        
        .pulsante
        {
            margin-left: 70%;
            margin-top: 10%;
        }
    </style>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
    <div style="width: 520px;">
        <table>
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; font-weight: bold;
                        top: 35px;">Aggiornamento Nucleo per applicazione AU </span>
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
                    <asp:DropDownList ID="cmbBando" runat="server" BackColor="#D8F9FC" Height="20px"
                        Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                        border-bottom: black 1px solid;" TabIndex="1" Width="350px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <table id="table4" class="stile_tabella" cellpadding="1" cellspacing="1">
            <tr>
                <td style="text-align: center;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 10%; padding-left: 3px;">
                                <asp:CheckBox ID="chkSeleziona" runat="server" AutoPostBack="True" Visible="False" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblTitolo" runat="server" Text="Rapporti Utenza" Font-Names="Arial"
                                    Font-Size="10pt" Font-Bold="True" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="height: 150px; overflow: auto">
                        <asp:CheckBoxList ID="chkContratti" runat="server" Font-Names="Arial" Font-Size="9pt">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="pulsante">
        <asp:ImageButton ID="btnProcedi" runat="server" CausesValidation="False" ImageUrl="../NuoveImm/Img_Procedi.png"
            TabIndex="2" ToolTip="Procedi" OnClientClick="ConfermaAggiornam();" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
            TabIndex="3" ToolTip="Indietro" />
    </div>
    <asp:HiddenField ID="conferma" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility = 'hidden';

    function ConfermaAggiornam() {
        var chiediConferma;
        var selezionato = false;
        var msg1 = "Attenzione, procedendo verranno aggiornati i nuclei di tutti i rapporti su cui è stata applicata l\'AU selezionata. Continuare?"
        if (document.getElementById('cmbBando').value == '-1') {
            alert('Selezionare il bando prima di procedere!');
            document.getElementById('conferma').value = '0';
        }
        else {
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        selezionato = true;
                    }
                }
            }
            if (selezionato == true) {
                chiediConferma = window.confirm(msg1);
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
            else {
                alert('Selezionare almeno un contratto prima di procedere!');
                document.getElementById('conferma').value = '0';
            }
        }
    }
</script>
</html>
