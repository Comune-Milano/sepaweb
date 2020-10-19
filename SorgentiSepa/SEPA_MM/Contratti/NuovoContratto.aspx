<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoContratto.aspx.vb" Inherits="Contratti_NuovoERP" %>

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
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Nuovo da ERP</title>
	</head>
	<body bgcolor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;

</script>
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Nuovo
                            Contratto da
                            <asp:Label ID="Label3" runat="server" Text="Label" Width="581px" 
                            TabIndex="8"></asp:Label></strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;&nbsp;<br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;&nbsp;
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
            <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 117; left: 349px; position: absolute;
                top: 125px" alt="Image" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 660px; position: absolute; top: 441px" TabIndex="6" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                
                Style="z-index: 101; left: 527px; position: absolute; top: 441px; height: 20px;" 
                TabIndex="5" ToolTip="Avvia Ricerca" />
									<asp:label id="lblCognome" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 102; left: 50px; position: absolute; top: 101px" Width="105px">Cognome</asp:label>
									<asp:textbox id="txtCognome" tabIndex="1" runat="server" style="z-index: 103; left: 164px; position: absolute; top: 97px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="lblNome" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 104; left: 50px; position: absolute; top: 126px">Nome</asp:label>
									<asp:textbox id="txtNome" tabIndex="2" runat="server" style="z-index: 105; left: 164px; position: absolute; top: 124px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="lblCfPiva" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 106; left: 50px; position: absolute; top: 152px" Width="108px">Codice Fiscale</asp:label>
									<asp:textbox id="txtCF" tabIndex="3" runat="server" style="z-index: 107; left: 164px; position: absolute; top: 150px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 106; left: 50px; position: absolute; top: 178px">N. Offerta</asp:Label>
            <asp:TextBox ID="txtofferta" runat="server" BorderStyle="Solid" BorderWidth="1px"
                Style="z-index: 107; left: 164px; position: absolute; top: 176px" TabIndex="4"></asp:TextBox>
            &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
		</form>
	</body>
</html>
