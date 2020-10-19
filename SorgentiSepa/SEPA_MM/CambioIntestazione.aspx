<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioIntestazione.aspx.vb" Inherits="CambioIntestazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
var Uscita;
Uscita=1;


function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
      alert('Usare il tasto <Avvia Ricerca>');
      history.go(0);
      event.keyCode=0;
      }  
} 

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cambio Intestazione</title>
</head>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" defaultfocus="txtPG">
    <div>
        &nbsp;
        <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Cambio
                        Intestazione</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 528px; position: absolute; top: 313px" 
            TabIndex="3" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 102; left: 393px; position: absolute; top: 313px" 
            TabIndex="2" ToolTip="Avvia Ricerca" />
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 44px; position: absolute; top: 131px">Protocollo</asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 43px; position: absolute; top: 86px" Width="620px">Inserire il protocollo della domanda a cui si vuole cambiare l'intestatario. E' possibile scegliere tra i soli componenti maggiorenni del nucleo famigliare.</asp:Label>
        <asp:TextBox ID="txtPG" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 109;
            left: 117px; position: absolute; top: 128px" TabIndex="1"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
