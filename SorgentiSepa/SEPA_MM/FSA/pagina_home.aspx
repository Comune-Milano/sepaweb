<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagina_home.aspx.vb" Inherits="FSA_pagina_home" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Pagina Principale</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		
		
<script type="text/javascript">
var Uscita;
Uscita=1;
</script>

<script type ="text/javascript">
<!--
var id,pause=0,position=0; 

function scorrevole() { 
var i,k,msg=document.getElementById('txtmessaggio').value;

k=(200/msg.length)+1;
for(i=0;i<=k;i++) msg+=' '+msg;
document.getElementById('scorrevole').value=msg.substring(position,position+100);
if(position++==200) position=0;
id=setTimeout('scorrevole()',200); } 


function ApriContatti()
{
var win=null;
window.open('Contatti.htm',null,'height=480,top=0,left=0,width=490,scrollbars=yes');
}

function ApriHelp()
{
var win=null;
win=window.open('Manuale_SEPA_WEB.pdf','Manuale');
}

//-->
</script>
    
<script language="javascript" id="clientEventHandlersJS">
<!--

function DIV1_onclick() {

}

//-->
		</script>
	</head>
	<body bgColor="#f2f5f1" onload="document.getElementById('txtmessaggio').style.visibilty='hidden';" background="../NuoveImm/sfondocopertina.jpg">
		<form id="Form1" method="post" runat="server">
            &nbsp;
            <img src="../immagini/sistemiesoluzionisrl.gif" style="z-index: 110; left: 26px; position: absolute;
                top: 497px" />
            &nbsp;
			&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                Style="z-index: 100; left: 38px; position: absolute; top: 57px" Text="VERSIONE 1.20" Font-Italic="False"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="txtmessaggio" runat="server" Style="left: 716px; position: absolute;
                top: 281px; z-index: 101;" Height="10px" Width="7px"></asp:TextBox>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="#404040" Style="z-index: 102; left: 39px; position: absolute; top: 76px"
                Text="Label" Width="314px"></asp:Label>
            &nbsp; &nbsp;
            <asp:Label ID="label33" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="#404040" Style="z-index: 106; left: 153px; cursor: pointer; position: absolute;
                top: 429px" Visible="False" Width="377px"></asp:Label>
            &nbsp;
            <asp:Label ID="lblComunicazioni" runat="server" Font-Bold="True" Font-Names="ARIAL"
                Font-Size="12pt" ForeColor="#721C1F" Style="z-index: 106; left: 153px; cursor: pointer;
                position: absolute; top: 405px" Visible="False" Width="379px">COMUNICAZIONI</asp:Label>
		</form>
	</body>
	

</html>

