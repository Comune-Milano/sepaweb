<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDomande.aspx.vb" Inherits="CONS_RicercaDomande" %>

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
		<title>Ricerca Domande</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<script type="text/javascript">
	


function Attendi() {
   // var win=null;
   // LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
   // TopPosition=(screen.height) ? (screen.height-150)/2 :0;
   // LeftPosition=LeftPosition;
   // TopPosition=TopPosition;
   // parent.funzioni.aa=window.open('../loading.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }
</script>
	<body bgColor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
		<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
            &nbsp;
									<asp:label id="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 100; left: 247px; position: absolute; top: 121px">Codice Fiscale</asp:label>
									<asp:label id="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 101; left: 247px; position: absolute; top: 149px">Protocollo</asp:label>
									<asp:textbox id="txtCF" runat="server" style="z-index: 102; left: 324px; position: absolute; top: 118px" BorderStyle="Solid" BorderWidth="1px" Width="140px"></asp:textbox>
									<asp:textbox id="txtPG" tabIndex="1" runat="server" style="z-index: 103; left: 324px; position: absolute; top: 147px" BorderStyle="Solid" BorderWidth="1px" Width="140px"></asp:textbox>
            <img src="../ImmMaschere/alert_ricercad.gif" style="z-index: 107; left: 92px; position: absolute;
                top: 81px" />
            <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 108; left: 474px; position: absolute;
                top: 106px" />
            <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" Style="z-index: 104;
                left: 566px; position: absolute; top: 213px" CausesValidation="False" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 105; left: 430px; position: absolute; top: 213px" />
            &nbsp;&nbsp;
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
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;
		</form>
	</body>
</html>
