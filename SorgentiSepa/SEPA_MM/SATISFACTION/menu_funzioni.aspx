<%@ Reference Page="~/avviso.aspx" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu_funzioni.aspx.vb" Inherits="FSA_menu_funzioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>menu_funzioni</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body background="../NuoveImm/comeblu_up.jpg">
		<form id="Form1" method="post" runat="server">
            <asp:Label ID="lblOperatore" runat="server" Font-Bold="True" Font-Size="X-Small"
                Height="16px" Style="z-index: 100; left: 69px; position: absolute; top: 33px"
                Width="230px">Label</asp:Label>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
                ForeColor="#721C1F" Height="16px" Style="z-index: 100; left: 33px; position: absolute;
                top: 33px" Width="35px">Utente</asp:Label>
            <asp:ImageButton ID="avviso" runat="server" ImageUrl="../IMG\Avviso_Pw.gif" Style="z-index: 101;
                left: 370px; position: absolute; top: 13px" Visible="False" />
            &nbsp; &nbsp; &nbsp;
            <asp:Image ID="Image1" runat="server" Height="14px" ImageUrl="~/NuoveImm/Albero_1.gif"
                Style="left: 27px; position: absolute; top: 33px" Width="4px" />
            <asp:Image ID="Image2" runat="server" Height="14px" ImageUrl="~/NuoveImm/Albero_1.gif"
                Style="left: 608px; position: absolute; top: 33px" Width="4px" 
                Visible="False" />
            <asp:Image ID="Image3" runat="server" 
                ImageUrl="~/NuoveImm/Titolo_CustomerSatisfaction.png" Style="left: 26px;
                position: absolute; top: 3px" />
            <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="arial" Font-Size="9pt"
                ForeColor="#721C1F" Style="left: 615px; position: absolute; top: 33px" 
                Visible="False">Chiudi</asp:LinkButton>
        </form>
		<script type="text/javascript">
		function spegni()
		{
		alert("L'icona di notifica password scomparirà al prossimo riavvio di SEPA@Web");
		
		}
		</script>
	</body>
</html>
