<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicPre.aspx.vb" Inherits="CONS_RisultatoRicPre" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Risultato Ricerca Prenotazioni</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="1" style="z-index: 100; left: 25px; position: absolute; top: 82px">
				<tr>
					<TD vAlign="center" align="middle" bgColor="#ffffff" style="text-align: left">
                        &nbsp;
                    </td>
				</tr>
				<tr>
					<TD vAlign="center" align="middle" bgColor="#ffffff" style="text-align: left">
                        <br />
                        <asp:CheckBoxList ID="CH1" runat="server" Font-Names="ARIAL" Font-Size="8pt" Style="z-index: 100;
                            left: 0px; position: static; top: 0px">
                        </asp:CheckBoxList></td>
				</tr>
				<tr>
					<TD vAlign="center" align="middle" bgColor="#eceff2" style="text-align: center">
						<asp:label id="lbl1" runat="server" Font-Names="Arial" Font-Size="XX-Small" Font-Bold="True" BorderStyle="None" BorderColor="Black" ForeColor="Blue">Selezionare le prenotazioni da cancellare e premere il pulsante "ELIMINA"</asp:label></td>
				</tr>
				<tr>
					<TD vAlign="center" align="left" bgColor="#ffffff">
						</td>
				</tr>
				<tr>
					<TD vAlign="center" align="middle" bgColor="#eceff2" style="text-align: center">
						<asp:button id="btnVisualizza" runat="server" Width="70px" Text="Elimina" Height="32px" TabIndex="1"></asp:button>&nbsp;&nbsp;
						<asp:Button id="btnRicerca" runat="server" Text="Nuova Ricerca" Height="32px" TabIndex="2"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;
						<asp:button id="btnAnnulla" runat="server" Width="87px" Text="Home Page" Height="32px" TabIndex="3"></asp:button></td>
				</tr>
			</table>
			<asp:label id="LBLID" style="Z-INDEX: 101; LEFT: 108px; POSITION: absolute; TOP: 399px" runat="server" Width="78px" Height="21px" Visible="False">Label</asp:label>
			<asp:label id="LBLPROGR" style="Z-INDEX: 102; LEFT: 206px; POSITION: absolute; TOP: 399px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
            <asp:ListBox ID="L1" runat="server" Style="z-index: 103; left: 26px; position: absolute;
                top: 261px" Visible="False"></asp:ListBox>
            <table style="z-index: 99; left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg);
                width: 674px; position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Prenotazioni Trovate</strong></span><br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                            ForeColor="Navy" Style="z-index: 100; left: 284px; position: absolute; top: 25px"
                            Width="191px">0</asp:Label>
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
