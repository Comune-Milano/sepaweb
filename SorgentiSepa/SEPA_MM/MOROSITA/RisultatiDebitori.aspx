<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiDebitori.aspx.vb"
    Inherits="MOROSITA_RisultatiDebitori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
    var Uscita1;
    Uscita1 = 1;
</script>
<head id="Head1" runat="server">
    <title>RISULTATI RICERCA MOROSITA INQUILINI</title>
    <script type="text/javascript">
        function Somma(imp, obj) {
            if (imp != 0) {
                
                var a = parseFloat(imp);
                if (a != 'NaN') {
                    var b = parseFloat(document.getElementById('txtSommaParziale').value.replace(/\./gi, '').replace(',', '.'));
                    var risultato1;
                    var risultato1 = parseFloat(document.getElementById('txtRapportiSelezionati').value);
                    var risultato;
                    if (obj.checked == true) {
                        risultato = parseFloat(a + b).toFixed(2);
                        risultato1 = parseFloat(risultato1) + 1;
                    }
                    else {
                        risultato = parseFloat(b - a).toFixed(2);
                        risultato1 = parseFloat(risultato1) - 1;
                    }
                    if (risultato.substring(risultato.length - 3, 0).length >= 4) {
                        var decimali = risultato.substring(risultato.length, risultato.length - 2);
                        var dascrivere = risultato.substring(risultato.length - 3, 0);
                        var risultNew = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew;
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                        }
                        risultNew = dascrivere + risultNew + ',' + decimali;
                    }
                    else {
                        risultNew = risultato.replace('.', ',');
                    }
                    document.getElementById('txtSommaParziale').value = risultNew;
                    document.getElementById('txtRapportiSelezionati').value = risultato1;
                }
            }
        }
    </script>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 778px;">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width: 100%">
                <asp:Label runat="server" ID="Label78" Font-Bold="True" Font-Names="arial" Font-Size="14pt"
                    ForeColor="Maroon">Risultati Ricerca Intestatari</asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="lblNrisultati" runat="server" Text="" Font-Bold="True" Font-Names="arial"
                    Font-Size="14pt" ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 20px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <div style="overflow: auto; width: 770px; height: 300px">
                    <asp:DataGrid runat="server" ID="DataGrid1" AutoGenerateColumns="False" CellPadding="1" BorderWidth="1px"
                        BorderColor="#507CD1"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                        Width="1100px" AllowPaging="True" PageSize="300">
                        <AlternatingItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CODICE" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>&nbsp;
                                </ItemTemplate>
                                <HeaderStyle Width="15%"></HeaderStyle>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" HeaderStyle-Width="15%"
                                ItemStyle-Width="15%">
                                <HeaderStyle Width="15%"></HeaderStyle>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DEBITO2" HeaderText="DEBITO" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POSIZIONE_CONTRATTO" HeaderText="POSIZIONE" HeaderStyle-Width="10%"
                                ItemStyle-Width="10%">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNITA'" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_TIPOLOGIA" HeaderText="TIPO UN." ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" HeaderStyle-Width="10%"
                                ItemStyle-Width="10%">
                                <HeaderStyle Width="10%"></HeaderStyle>
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIV." ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                <HeaderStyle Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" Width="5%"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE" HeaderStyle-Width="15%"
                                ItemStyle-Width="15%">
                                <HeaderStyle Width="15%"></HeaderStyle>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                            HorizontalAlign="Center" />
                        <ItemStyle BackColor="#F9F9F9" ForeColor="#000000" />
                        <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Left" Mode="NumericPages"
                            Position="Top" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table>
                    <tr>
                        <td style="width: 15%">
                            <asp:ImageButton ID="btnSelTutti" runat="server" ImageUrl="Immagini/Img_SelezionaTutti.png"
                                ToolTip="Seleziona tutte le righe della pagina" />
                        </td>
                        <td style="width: 15%">
                            <asp:ImageButton ID="btnSelPrimi" runat="server" ImageUrl="Immagini/Img_SelPrimiCent.png"
                                ToolTip="Seleziona i primi 150 elementi" />
                        </td>
                        <td style="width: 15%">
                            <asp:ImageButton ID="btnDeselTutti" runat="server" ImageUrl="Immagini/Img_DeSelezionaTutti.png"
                                ToolTip="Deseleziona tutte le righe della pagina" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 20%">
                            <img alt="info" src="Immagini/alert_elencoIntestatari.gif" id="info" style="position: relative;
                                top: 20px" />
                        </td>
                        <td style="width: 80%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 10%"></td>
                                    <td style="width: 30%">
                                        <asp:Label ID="lblTotaleRapporti" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Maroon">
                                        </asp:Label>
                                    </td>
                                    <td style="width: 60%">
                                        <asp:Label ID="lblNTotaleRapporti" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Style="text-align: right;" Font-Size="10pt" ForeColor="Maroon" Width="30%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblTotaleMorosita" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Maroon">Totale Morosità</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotaleMorosita" runat="server" Style="text-align: right; border: 0 none transparent;
                                            background-color: transparent;" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon"
                                            ReadOnly="True" Width="30%" Wrap="False" Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label111" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon"
                                            Font-Bold="True">Rapporti Selezionati</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRapportiSelezionati" runat="server" Style="text-align: right;
                                            border: 0 none transparent; background-color: transparent;" Font-Names="Arial"
                                            Font-Size="10pt" ForeColor="Maroon" ReadOnly="True" Width="30%" Wrap="False"
                                            Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                            ForeColor="Maroon">Importo Morosità Selezione</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSommaParziale" runat="server" Style="text-align: right; border: 0 none transparent;
                                            background-color: transparent;" Font-Names="Arial" Font-Size="10pt" ForeColor="Maroon"
                                            ReadOnly="True" Width="30%" Wrap="False" Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 20%">
                            &nbsp;
                        </td>
                        <td style="width: 20%; text-align: center">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="Immagini/Img_Procedi.png"
                                ToolTip="Procedi" />
                        </td>
                        <td style="width: 20%; text-align: center">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../NuoveImm/Img_Export_XLS.png"
                                ToolTip="Esporta in Excel" />
                        </td>
                        <td style="width: 20%; text-align: center">
                            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="../NuoveImm/Img_NuovaRicerca.png"
                                ToolTip="Nuova Ricerca" />
                        </td>
                        <td style="width: 20%; text-align: center">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="SommaTotale" runat="server" Value="0" />
    <asp:HiddenField ID="SommaParziale" runat="server" Value="0" />
    <asp:HiddenField ID="Label3" runat="server" />
    <asp:HiddenField ID="LBLID" runat="server" />
    <asp:HiddenField ID="SLet" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('divLoading')!=null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
        if (document.getElementById('SLet').value == 1) {
            document.getElementById('info').style.visibility = 'hidden';
        }
    </script>
</body>
</html>
