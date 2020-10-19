<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoDomScadSosp.aspx.vb"
    Inherits="Contratti_ElencoDomScadSosp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Domande in Scadenza Sospese</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <table style="left: 0px; background-image: url(../Condomini/Immagini/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px;">
        <tr>
            <td>
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Domande in Scadenza Trovate </strong>
                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                </span>
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
                <%--          
              <br />
              <br />
              <br />
             <asp:TextBox ID="txtmia" runat="server" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" ReadOnly="True" Width="657px">Nessuna Selezione</asp:TextBox>
                --%>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
        Style="z-index: 101; left: 659px; position: absolute; top: 502px; height: 20px;"
        ToolTip="Esci" OnClientClick="javascript:window.close()" />
    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
        Style="z-index: 102; left: 466px; position: absolute; top: 502px" ToolTip="Visualizza"
        OnClientClick="document.getElementById('H1').value='1';" />
    <div style="overflow: auto; position: absolute; width: 776px; height: 433px; top: 57px;
        left: 14px;">
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            Style="z-index: 105; left: -4px; position: absolute; top: 7px" Width="1200px"
            CellPadding="2" CellSpacing="1" PageSize="20" AllowPaging="True">
            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemStyle ForeColor="Black" />
            <Columns>
                <asp:BoundColumn DataField="RICH" HeaderText="RICHIEDENTE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CONTRATTO_NUM" HeaderText="COD. RAPPORTO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO_DOM" HeaderText="TIPO DOMANDA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATAPRES" HeaderText="DATA SOSPENSIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATASCAD" HeaderText="DATA SCADENZA SOSP.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PGDOM" HeaderText="NUM. DOMANDA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="PGDICH" HeaderText="NUM. DICHIARAZIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" />
        </asp:DataGrid>
        <asp:HiddenField ID="H1" runat="server" Value="0" />
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        var Selezionato;
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
