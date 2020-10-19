<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDomandePrecedenti.aspx.vb" Inherits="RicercaDomandePrecedenti" %>

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
		<title>Ricerca Domande Precedenti</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body bgColor="#f2f5f1" >
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
									<asp:label id="Label1" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" style="z-index: 100; left: 60px; position: absolute; top: 93px">Cognome</asp:label>
									<asp:textbox id="txtCognome" tabIndex="1" runat="server" style="z-index: 101; left: 141px; position: absolute; top: 90px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label2" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" style="z-index: 102; left: 60px; position: absolute; top: 119px">Nome</asp:label>
									<asp:textbox id="txtNome" tabIndex="2" runat="server" style="z-index: 103; left: 141px; position: absolute; top: 116px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label4" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" style="z-index: 104; left: 60px; position: absolute; top: 145px">Codice Fiscale</asp:label>
									<asp:textbox id="txtCF" tabIndex="3" runat="server" style="z-index: 105; left: 141px; position: absolute; top: 142px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label5" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" style="z-index: 106; left: 60px; position: absolute; top: 171px">Protocollo</asp:label>
									<asp:textbox id="txtPG" tabIndex="4" runat="server" style="z-index: 107; left: 141px; position: absolute; top: 168px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
            <img src="ImmMaschere/alert2_ricercad.gif" style="z-index: 111; left: 322px; position: absolute;
                top: 107px" />
            <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 108; left: 536px; position: absolute; top: 295px" TabIndex="8" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 109; left: 400px; position: absolute; top: 295px" TabIndex="7" ToolTip="Avvia Ricerca" />
            &nbsp;
            <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Domande di Bandi Precedenti</strong></span><br />
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
                        <br />
                    </td>
                </tr>
            </table>
            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 113;
                left: 59px; position: absolute; top: 225px" Text="La ricerca sarà effettuata all'interno dell'archivio ERP, ad esclusione delle domande di bando corrente."></asp:Label>
		</form>
	</body>
</html>
