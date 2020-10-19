<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DepositoCauzionaleInte.aspx.vb"
    Inherits="Contratti_DepositoCauzionaleInte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposito Cauzionale</title>
    <script type="text/javascript">
        var Selezionato;
    </script>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-attachment: fixed; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; width: 792px;position:absolute;left:5px;">
    <form id="form1" runat="server">
    <br />
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp&nbsp<asp:Label
        ID="lblTitolo" runat="server" Text="Scheda Deposito Cauzionale"></asp:Label>
    </span></strong>
    <br />
    <br />
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 90%;">
                <asp:Button ID="btnSeleziona" runat="server" CssClass="bottone" Text="Visualizza" />
                <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" OnClientClick="self.close();" />
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow: auto;height:440px;">
        <asp:DataGrid runat="server" ID="DataGrid" AutoGenerateColumns="False" CellPadding="1"
            Font-Names="Arial" Font-Size="10pt" ForeColor="#000000" GridLines="None" CellSpacing="1"
            Width="97%" ShowFooter="True">
            <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="RAGIONE_SOCIALE" HeaderText="RAGIONE SOCIALE"></asp:BoundColumn>
                <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE"></asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#999999" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                HorizontalAlign="Center" />
            <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
            <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
        </asp:DataGrid>
    </div>
    <asp:HiddenField runat="server" ID="idIntestatario" Value="" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
