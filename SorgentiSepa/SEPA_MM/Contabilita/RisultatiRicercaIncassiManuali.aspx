<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiRicercaIncassiManuali.aspx.vb"
    Inherits="Contabilita_RisultatiRicercaIncassiManuali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Risultati ricerca incassi manuali</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="../FUNCTION_FORMAT/format.css" rel="stylesheet" type="text/css" />
    <script src="../FUNCTION_FORMAT/functionJS.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Modifica(id) {
            window.showModalDialog('ModificaIncassoManuale.aspx?id=' + id, window, 'status:no;dialogWidth:500px;dialogHeight:400px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
        }
        function Elimina(id, nominativo, importo) {
            var conferma = window.confirm('Confermi di voler eliminare l\'incasso\n di ' + nominativo + ' per un importo pari a € ' + importo + '?');
            if (conferma) {
                document.getElementById('IdEliminazione').value = id;
            } else {
                document.getElementById('IdEliminazione').value = 0;
            }
        }
    </script>
</head>
<body onload="controlloBrowser();">
    <form id="form1" runat="server">
    <div id="titolo">
        Risultati ricerca incassi manuali</div>
    <div id="contenuto">
        <asp:DataGrid ID="DataGridIncassi" runat="server" AutoGenerateColumns="False" CellPadding="3"
            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" CellSpacing="3" Width="100%"
            ShowFooter="True" BorderColor="White" BorderWidth="1px">
            <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
            <Columns>
                <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO" ItemStyle-Width="10%"
                    HeaderStyle-Width="10%">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Wrap="false"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ItemStyle-Width="20%" HeaderStyle-Width="10%">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"  Wrap="false"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CAUSALE" HeaderText="CAUSALE" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                    <HeaderStyle Width="20%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"  Wrap="false"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_INCASSO" HeaderText="DATA INCASSO" ItemStyle-Width="20%"
                    HeaderStyle-Width="10%">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"  Wrap="false"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="20%" HeaderStyle-Width="40%">
                    <HeaderStyle Width="40%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"  Wrap="false"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:TemplateColumn ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButtonModifica" Height="16px" runat="server" ImageUrl="../NuoveImm/matita.png"
                            Width="16px" OnClick="ImageButtonModifica_Click" />
                        <asp:ImageButton ID="ImageButtonElimina" runat="server" Height="16px" Width="16px"
                            ImageUrl="../NuoveImm/Elimina.png" onclick="ImageButtonElimina_Click" />
                    </ItemTemplate>
                    <ItemStyle Wrap="False"></ItemStyle>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="FL_ATTRIBUITO" HeaderText="FL_ATTRIBUITO" ItemStyle-Width="10%"
                    HeaderStyle-Width="10%" Visible="False">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"  Wrap="false"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CONTEGGIO" HeaderText="CONTEGGIO" ItemStyle-Width="10%"
                    HeaderStyle-Width="10%" Visible="False">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"  Wrap="false"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#999999" />
            <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                HorizontalAlign="Center" />
            <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
            <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
        </asp:DataGrid>
    </div>
    <div id="bottoni">
    </div>
    <asp:HiddenField runat="server" ID="IdEliminazione" Value="0" />
    <asp:HiddenField runat="server" ID="IdModifica" Value="0" />
    </form>
    <script type="text/javascript">
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
    </script>
</body>
</html>
