<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagina_home.aspx.vb" Inherits="CONS_pagina_home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Pagina Principale</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		
		
<script language="javascript" type="text/javascript">
var Uscita;
Uscita=0;
</script>

<script language="javascript" type="text/javascript">
<!--
var id,pause=0,position=0; 

function scorrevole() { 
var i,k,msg=document.getElementById('txtmessaggio').value;

k=(200/msg.length)+1;
for(i=0;i<=k;i++) msg+=' '+msg;
document.getElementById('scorrevole').value=msg.substring(position,position+100);
if(position++==200) position=0;
id=setTimeout('scorrevole()',200); } 

//-->
</script>
    
<script language="javascript" id="clientEventHandlersJS">
<!--

function DIV1_onclick() {

}

//-->
		</script>
	</head>
	<script language="javascript" type="text/javascript" src="Funzioni.js">
	</script>
	<body background="../NuoveImm/sfondocopertina.jpg" onload="document.getElementById('txtmessaggio').style.visibilty='hidden';">
		<form id="Form1" method="post" runat="server">
            &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                Style="z-index: 101; left: 38px; position: absolute; top: 57px" Text="VERSIONE 1.20" Font-Italic="False"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="scorrevole" runat="server" Style="z-index: 102; left: 773px; position: absolute;
                top: 407px" Width="74px" BackColor="White" BorderColor="#E0E0E0" BorderStyle="None" Font-Names="ARIAL" Font-Size="8pt" ReadOnly="True" Font-Bold="True" Height="11px" ForeColor="#404040"></asp:TextBox>
            <asp:TextBox ID="txtmessaggio" runat="server" Style="left: 740px; position: absolute;
                top: 461px; z-index: 103;" Height="10px" Width="7px"></asp:TextBox>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="#404040" Style="z-index: 106; left: 39px; position: absolute; top: 76px"
                Text="Label" Width="314px"></asp:Label>
            <img alt="" src="../immagini/sistemiesoluzionisrl.gif" style="z-index: 108; left: 26px; position: absolute;
                top: 497px" />
		</form>
	</body>
	

</html>

