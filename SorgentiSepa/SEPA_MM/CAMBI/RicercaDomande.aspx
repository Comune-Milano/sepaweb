<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDomande.aspx.vb" Inherits="CAMBI_RicercaDomande" %>

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
		<title>RicercaDomande</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body bgColor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
									<asp:label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 100; left: 42px; position: absolute; top: 91px">Cognome</asp:label>
            <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 115; left: 293px; position: absolute;
                top: 98px" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 535px; position: absolute; top: 313px" TabIndex="8" CausesValidation="False" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 102; left: 401px; position: absolute; top: 313px" TabIndex="7" ToolTip="Avvia Ricerca" />
            &nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Domande</strong></span><br />
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
									<asp:textbox id="txtCognome" tabIndex="1" runat="server" style="z-index: 103; left: 115px; position: absolute; top: 89px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 104; left: 42px; position: absolute; top: 116px">Nome</asp:label>
									<asp:textbox id="txtNome" tabIndex="2" runat="server" style="z-index: 105; left: 115px; position: absolute; top: 114px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 106; left: 42px; position: absolute; top: 141px">Codice Fiscale</asp:label>
									<asp:textbox id="txtCF" tabIndex="3" runat="server" style="z-index: 107; left: 115px; position: absolute; top: 139px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 108; left: 42px; position: absolute; top: 167px">Protocollo</asp:label>
									<asp:textbox id="txtPG" tabIndex="4" runat="server" style="z-index: 109; left: 115px; position: absolute; top: 164px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 110; left: 42px; position: absolute; top: 194px">Stato</asp:label>
									<asp:DropDownList id="cmbStato" tabIndex="5" runat="server" Width="175px" style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 115px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 189px" BackColor="White"></asp:DropDownList>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 112; left: 42px; position: absolute; top: 220px" Width="36px">Bando</asp:Label><asp:DropDownList id="cmbBando" tabIndex="6" runat="server" Width="175px" style="z-index: 113; left: 115px; position: absolute; top: 216px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                </asp:DropDownList>
            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 117;
                left: 43px; position: absolute; top: 266px" Text="La ricerca sarà effettuata solo sulle domande acquisite dall'ente "></asp:Label>
		</form>
	</body>
</html>

