<%@ Page Language="vb" AutoEventWireup="false" Inherits="CM.RicercaDichiarazioni" CodeFile="RicercaDichiarazioni.aspx.vb" EnableSessionState="ReadOnly" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
		<title>RicercaDichiarazioni</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
	</head>
	<body bgcolor="#f2f5f1" >
<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>

		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
									<asp:label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 100; left: 91px; position: absolute; top: 122px">Nome</asp:label>
									<asp:label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 101; left: 91px; position: absolute; top: 150px">Codice Fiscale</asp:label>
									<asp:textbox id="txtCF" tabIndex="3" runat="server" style="z-index: 102; left: 170px; position: absolute; top: 147px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:textbox id="txtNome" tabIndex="2" runat="server" style="z-index: 103; left: 170px; position: absolute; top: 119px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
            &nbsp; &nbsp;
            <img src="ImmMaschere/alert2_ricercad.gif" style="z-index: 115; left: 346px; position: absolute;
                top: 106px" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 104; left: 525px; position: absolute; top: 350px" TabIndex="8" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 105; left: 390px; position: absolute; top: 350px" TabIndex="7" ToolTip="Avvia Ricerca" />
									<asp:label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 106; left: 91px; position: absolute; top: 94px">Cognome</asp:label>
									<asp:textbox id="txtCognome" tabIndex="1" runat="server" style="z-index: 107; left: 170px; position: absolute; top: 91px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 108; left: 92px; position: absolute; top: 178px">Numero</asp:label>
									<asp:textbox id="txtPG" tabIndex="4" runat="server" style="z-index: 109; left: 170px; position: absolute; top: 175px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
									<asp:label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 110; left: 93px; position: absolute; top: 206px">Stato</asp:label>
									<asp:DropDownList id="cmbStato" tabIndex="5" runat="server" Height="17px" Width="187px" AutoPostBack="True" style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 170px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 203px"></asp:DropDownList>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 112; left: 93px; position: absolute; top: 236px" Width="37px">Bando</asp:Label><asp:DropDownList id="cmbBando" tabIndex="6" runat="server" Height="17px" Width="187px" AutoPostBack="True" style="z-index: 116; left: 170px; position: absolute; top: 233px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                </asp:DropDownList>
            <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg);
                position: absolute; top: 0px; width: 674px;">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Dichiarazioni</strong></span><br />
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
		</form>
	</body>
</html>
