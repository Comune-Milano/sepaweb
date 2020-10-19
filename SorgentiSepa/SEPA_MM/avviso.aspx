<%@ Page Language="vb" AutoEventWireup="false" Inherits="CM.avviso" CodeFile="avviso.aspx.vb" EnableSessionState="ReadOnly" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>avviso</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="50%" align="center" border="0">
				<tr>
					<TD align="center">
						<asp:Panel id="P1" runat="server" Width="373px" Height="83px" BorderStyle="Outset" BackColor="White">
							<P>
								<asp:Label id="Label1" runat="server" ForeColor="Red" Font-Names="Arial" Font-Bold="True">ATTENZIONE!</asp:Label></P>
							<P>
								<asp:Label id="Label2" runat="server" Font-Names="Arial" Font-Bold="True" Font-Size="X-Small">E' consigliabile cambiare la propria password.</asp:Label>
								<asp:Label id="Label3" runat="server" Width="329px" Font-Names="Arial" Font-Bold="True" Font-Size="X-Small">Utilizzare la funzione IMPOSTA PASSWORD</asp:Label></P>
						</asp:Panel></td>
				</tr>
				<tr>
					<TD align=center>
						<P align="center">
							<asp:Button id="Button1" runat="server" Text="Chiudi" Height="29px"></asp:Button></P>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
