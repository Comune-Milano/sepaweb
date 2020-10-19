<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoEliminati.aspx.vb"
    Inherits="VSA_ElencoEliminati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Elenco eliminati</title>
</head>
<body bgcolor="#f2f5f1">
    <script type="text/javascript">
        //document.onkeydown=$onkeydown;
        var Selezionato;
    </script>
    <form id="Form1" method="post" runat="server">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url('../../NuoveImm/SfondoMaschere.jpg');
        width: 674px; position: absolute; top: -8px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Componenti del nucleo Eliminati</strong></span><br />
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
                &nbsp;<br />
                <br />
                <br />
                <br />
                <div style="left: 20px; overflow: auto; width: 645px; position: absolute; top: 67px;
                    height: 250px">
                    <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                        PageSize="100" Style="z-index: 101; left: 483px; top: 68px" Width="643px" CellPadding="2">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Mode="NumericPages" Wrap="False" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="Navy" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_DICHIARAZIONE" HeaderText="ID" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FISCALE" HeaderText="CODICE FISCALE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MOTIVO_USC" HeaderText="MOTIVO USCITA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_USC" HeaderText="DATA USCITA"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_EsciCorto.png"
        Style="z-index: 100; left: 508px; position: absolute; top: 334px" TabIndex="11"
        ToolTip="Esci" OnClientClick="javascript:window.close();" />
    <p>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 21px; position: absolute;
            top: 273px" Visible="False" Width="525px"></asp:Label>
    </p>
    <asp:HiddenField ID="txtid" runat="server" />
    <asp:HiddenField ID="sololettura" runat="server" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <script type="text/javascript">

        var Selezionato;

        if (document.getElementById('sololettura').value == '1') {
            document.getElementById('IMG1').style.visibility = 'hidden';
            document.getElementById('IMG1').style.position = 'absolute';
            document.getElementById('IMG1').style.left = '-100px';
            document.getElementById('IMG1').style.display = 'none';
        }

    </script>
    </form>
</body>
</html>
