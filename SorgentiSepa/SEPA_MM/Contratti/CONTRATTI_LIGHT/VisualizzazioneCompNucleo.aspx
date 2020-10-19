<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisualizzazioneCompNucleo.aspx.vb" Inherits="Contratti_CONTRATTI_LIGHT_VisualizzazioneCompNucleo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Visualizzazione componenti</title>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg');
        width: 674px; position: absolute; top: -8px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Componenti</strong></span><br />
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
                <div style="left: 11px; overflow: auto; width: 657px; position: absolute; top: 67px;
                    height: 250px">
                    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                        PageSize="100" Style="z-index: 101; left: 483px; top: 68px" Width="628px">
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
                            Font-Underline="False" Wrap="False"  BackColor="White" />
                        <AlternatingItemStyle BackColor="Gainsboro" />
                        <Columns>
                            <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PARENTELA" HeaderText="TIPO PARENTELA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="INIZIO">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_FINE" HeaderText="FINE">
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_EsciCorto.png"
                    Style="z-index: 100; left: 581px; position: absolute; top: 334px" TabIndex="11"
                    ToolTip="Esci" OnClientClick="javascript:window.close();" />
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

