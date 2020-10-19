<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioIntestazione.aspx.vb"
    Inherits="CambioIntestazione" %>

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
<head runat="server">
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
            margin-left: 60%;
            margin-top: 15%;
        }
    </style>
</head>
<body background="../../NuoveImm/SfondoMaschere.jpg">
    
    <form id="form1" runat="server" defaultbutton="btnCerca" defaultfocus="txtPG">
    <div>
        <table style="margin-top: 20px">
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial; font-weight: bold;">
                        Cambio Intestazione </span>
                </td>
            </tr>
        </table>
        <table class="stile_tabella">
            <tr>
                <td width="750px" style="font-family: arial, Helvetica, sans-serif; font-size: 9pt;
                    vertical-align: top;" height="30px">
                    Inserire il protocollo della domanda a cui si vuole <u>cambiare l'intestatario</u>.
                </td>
            </tr>
            <tr>
                <td style="font-family: arial, Helvetica, sans-serif; font-size: 8pt">
                    <i>(N.B. tale funzionalità è disponibile soltanto per le domande di Revisione Canone e Ampliamento!)</i>
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
                <td width="350px">
                    <asp:Label ID="Label1" runat="server" Text="Num. protocollo" Font-Size="9pt" Font-Names="Arial"
                        Font-Bold="True" Width="130px"></asp:Label>
                    <asp:TextBox ID="txtPG" runat="server" Width="120px" TabIndex="1" 
                        BackColor="#D8F9FC"></asp:TextBox>
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
                        ForeColor="#C00000" Visible="False">Domanda non trovata!!</asp:Label>
                </td>
            </tr>
           
            
        </table>
    </div>
    <div class="pulsante">
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            TabIndex="2" ToolTip="Avvia Ricerca" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            TabIndex="3" ToolTip="Home" onclientclick="self.close();" />
    </div>
    <asp:HiddenField ID="IDmotivo" runat="server" />
    </form>
</body>

</html>
