﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRinnovi.aspx.vb" Inherits="RicercaRinnovi" %>

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
	<body bgColor="#f2f5f1" >
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
            &nbsp;
            <img src="ImmMaschere/alert2_ricercad.gif" style="z-index: 113; left: 325px; position: absolute;
                top: 105px" />
            <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 538px; position: absolute; top: 321px" 
                TabIndex="7" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 404px; position: absolute; top: 321px" 
                TabIndex="6" ToolTip="Avvia Ricerca" />
            &nbsp;
            <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
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
									<asp:label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 102; left: 49px; position: absolute; top: 81px">Cognome</asp:label>
									<asp:textbox id="txtCognome" tabIndex="1" runat="server" style="z-index: 103; left: 140px; position: absolute; top: 79px; text-transform:uppercase;" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 104; left: 49px; position: absolute; top: 106px;">Nome</asp:label>
									<asp:textbox id="txtNome" tabIndex="2" runat="server" style="z-index: 105; left: 140px; position: absolute; top: 104px;text-transform:uppercase;" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 106; left: 49px; position: absolute; top: 131px">Codice Fiscale</asp:label>
									<asp:textbox id="txtCF" tabIndex="3" runat="server" style="z-index: 107; left: 140px; position: absolute; top: 129px;text-transform:uppercase;" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 108; left: 49px; position: absolute; top: 156px" ForeColor="Red">Protocollo</asp:label>
									<asp:textbox id="txtPG" tabIndex="4" runat="server" style="z-index: 109; left: 140px; position: absolute; top: 154px;text-transform:uppercase;" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 110; left: 49px; position: absolute; top: 182px" Width="36px">Bando</asp:Label><asp:DropDownList id="cmbBando" tabIndex="5" runat="server" Height="20px" Width="193px" style="z-index: 111; left: 141px; position: absolute; top: 179px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                </asp:DropDownList>
            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 115;
                left: 49px; position: absolute; top: 226px" Text="La ricerca sarà effettuata all'interno dell'archivio ERP, ad esclusione delle domande di bando corrente,  delle domande che sono in fase di assegnazione o verifica dei requisiti e delle domande escluse per Morte, Unifica, Assegnatario Bando POR, Rinuncia, Sopravvenuta Assegnazione e per decoorenza dei termini temporali di validità (DS)."
                Width="557px" Font-Bold="True"></asp:Label>
            <img src="IMG/Alert.gif" style="left: 29px; position: absolute; top: 226px" />
		</form>
	</body>
</html>
