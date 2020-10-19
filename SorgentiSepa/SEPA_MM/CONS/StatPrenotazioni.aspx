<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatPrenotazioni.aspx.vb" Inherits="CONS_StatPrenotazioni" %>

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
		<title>Statistiche Prenotazioni</title>
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
            &nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Statistiche
                            Consultazioni</strong></span><br />
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
            <img src="../ImmMaschere/alertstat.gif" style="z-index: 112; left: 66px; position: absolute;
                top: 115px" />
            <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 554px; position: absolute; top: 290px" 
                TabIndex="3" />
            <asp:ImageButton ID="btnCerca" runat="server" CausesValidation="true" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 419px; position: absolute; top: 290px" 
                TabIndex="2" />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                ForeColor="Red" Style="z-index: 102; left: 40px; position: absolute; top: 74px"
                Text="Attenzione...inserire un intervallo di date valido, nel formato GG/MM/AAAA"></asp:Label>
									<asp:label id="Label4" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" style="z-index: 103; left: 236px; position: absolute; top: 186px">Dal</asp:label>
									<asp:textbox id="txtDal" tabIndex="1" runat="server" Width="84px" style="z-index: 104; left: 259px; position: absolute; top: 182px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDal"
                                        ErrorMessage="Data Errata!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                        Style="z-index: 105; left: 259px; position: absolute; top: 209px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="88px"></asp:RegularExpressionValidator>
									<asp:label id="Label5" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" style="z-index: 106; left: 365px; position: absolute; top: 186px">Al</asp:label>
									<asp:textbox id="txtAl" tabIndex="2" runat="server" Width="84px" style="z-index: 107; left: 385px; position: absolute; top: 182px" BorderStyle="Solid" BorderWidth="1px"></asp:textbox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAl"
                                        ErrorMessage="Data Errata!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                        Style="z-index: 108; left: 385px; position: absolute; top: 209px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="123px"></asp:RegularExpressionValidator>
		</form>
	</body>
</html>
