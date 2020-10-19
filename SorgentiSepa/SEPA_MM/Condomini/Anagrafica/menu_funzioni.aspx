<%@ Reference Page="~/avviso.aspx" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_funzioni.aspx.vb" Inherits="ANAUT_menu_funzioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var aa;
var bb;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>menu_funzioni</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body style="color: #660000">
		<form id="Form1" method="post" runat="server">
            <span style="font-size: 16pt; font-family: Arial"><strong>&nbsp; Rubrica 
            Anagrafica Amministratori</strong></span>
            <asp:Image ID="Image2" runat="server" Height="14px" ImageUrl="~/NuoveImm/Albero_1.gif"
                Style="left: 427px; position: absolute; top: 29px" Width="4px" Visible="False" />
            &nbsp;
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="arial" Font-Size="9pt"
                ForeColor="#721C1F" Style="left: 443px; position: absolute; top: 29px" Visible="False">Chiudi</asp:LinkButton>
        </form>
		<script type="text/javascript">
		function spegni()
		{
		alert("L'icona di notifica password scomparirà al prossimo riavvio di SEPA@Web");
		
		}
		</script>
	</body>
</html>
