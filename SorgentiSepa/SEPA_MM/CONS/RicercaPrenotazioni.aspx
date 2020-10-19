<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPrenotazioni.aspx.vb" Inherits="CONS_RicercaPrenotazioni" %>

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
		<title>Ricerca Prenotazioni on-line</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<script type="text/javascript">
function Attendi() {
    //var win=null;
    //LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
    //TopPosition=(screen.height) ? (screen.height-150)/2 :0;
    //LeftPosition=LeftPosition;
    //TopPosition=TopPosition;
    //parent.funzioni.aa=window.open('../loading.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }
</script>
	<body bgColor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
			<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
									<asp:label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 100; left: 83px; position: absolute; top: 145px">Protocollo</asp:label>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 101; left: 83px; position: absolute; top: 175px" Width="75px">Nominativo</asp:Label>
                                    <asp:TextBox ID="txtNome" runat="server" Style="z-index: 102; left: 166px; position: absolute;
                                        top: 173px" TabIndex="3" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
									<asp:textbox id="txtPG" tabIndex="2" runat="server" style="z-index: 103; left: 166px; position: absolute; top: 143px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
            &nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Prenotazioni on-line</strong></span><br />
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
            <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 112; left: 348px; position: absolute;
                top: 121px" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 104; left: 591px; position: absolute; top: 267px" TabIndex="8" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 105; left: 449px; position: absolute; top: 267px" TabIndex="7" />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                ForeColor="Red" Style="z-index: 106; left: 45px; position: absolute; top: 64px"
                Text="Attenzione...tutte le cancellazioni effettuate saranno memorizzate!"></asp:Label>
									<asp:label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 107; left: 82px; position: absolute; top: 118px">Codice Fiscale</asp:label>
									<asp:textbox id="txtCF" tabIndex="1" runat="server" style="z-index: 108; left: 166px; position: absolute; top: 114px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
		</form>
	</body>
</html>
