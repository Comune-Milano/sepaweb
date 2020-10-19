﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Eventi_Assest.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_Eventi_Assest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Eventi Assestamento</title>
</head>
<body >
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
    <div>
        <asp:Label ID="lblTitolo" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="font-size: 11pt;
            z-index: 100; left: 0px; font-family: Arial; position: static; top: 0px" Text="Label"
            Width="100%"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="8pt" Text="Seleziona la Struttura"></asp:Label>
        <br />
            <asp:DropDownList ID="cmbStruttura" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="8pt" Height="20px" Style="border: 1px solid black; z-index: 10;"
                Width="550px" TabIndex="4" AutoPostBack="True">
            </asp:DropDownList>
        <br />
        <br />
        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="1" Style="table-layout: auto; z-index: 101; left: 16px; clip: rect(auto auto auto auto);
            direction: ltr; top: 200px; border-collapse: separate" Width="100%">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                <asp:BoundColumn DataField="VOCE" HeaderText="VOCE"></asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA ORA"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="EVENTO"></asp:BoundColumn>
                <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T."></asp:BoundColumn>
                <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_OPERATORE" HeaderText="ID_OPERATORE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_EVENTO" HeaderText="COD_EVENTO" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €.">
    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
        Wrap="False" />
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
        Wrap="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid><br />
        <asp:Label ID="lblTotale" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Style="font-size: 11pt; z-index: 100; left: 0px; width: 100%; font-family: Arial;
            position: static; top: 0px; text-align: left" Text="Label" Width="100%"></asp:Label>&nbsp;</div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>