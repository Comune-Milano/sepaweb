<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiReportSituazioneMorosita.aspx.vb" Inherits="Contabilita_Report_RisultatiReportSituazioneMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Situazione Incassi</title>
    <style type="text/css">
        .bottone
        {
            background-color: transparent;
            border-left: 4px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-size: 7pt;
            height: 11px;
            cursor: pointer;
            padding-left: 1px;
        }
        .bottone:hover
        {
            background-color: #FFF5D3;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 7pt;
            height: 11px;
            cursor: pointer;
            padding-left: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="width: 8%">
                            &nbsp;<asp:Button ID="exportIncassi" runat="server" CausesValidation="False" CssClass="bottone"
                                Text="XLS Incassi" ToolTip="Torna indietro" />
                        </td>
                        <td style="width: 8%">
                            <asp:Button ID="exportnonAttrib" runat="server" CausesValidation="False" CssClass="bottone"
                                Text="XLS Non Attribuibili" ToolTip="Torna indietro" />
                        </td>
                        <td style="width: 8%">
                            <asp:Button ID="exportExtra" runat="server" CausesValidation="False" CssClass="bottone"
                                Text="XLS ExtraMav" />
                        </td>
                        <td style="width: 8%">
                            &nbsp;
                        </td>
                        <td style="width: 72%">
                            <asp:ImageButton ID="ImageButtonEccedenze" runat="server" ImageUrl="../../NuoveImm/Img_Eccedenze.png"
                                ToolTip="Visualizza Eccedenze" OnClientClick="apriEccedenze();return false;" />
                        </td>
                        <td style="width: 10%; text-align: right">
                            <asp:ImageButton ID="ImageButtonEsci" runat="server" ImageUrl="../../NuoveImm/Img_Esci.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center;">
                <asp:Label ID="LabelErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align: center">
                <asp:Label ID="LabelTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGridIncassi" AutoGenerateColumns="False" CellPadding="0"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True" BackColor="#999999" BorderColor="#507CD1" BorderWidth="1px"
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" AllowPaging="True" PageSize="50">
                    <AlternatingItemStyle BackColor="#EEEEEE" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="COD_INTESTATARIO" HeaderText="CODICE INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUMERO BOLLETTA"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="BOLLETTAZIONE" HeaderText="BOLLETTAZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPETENZA_ACC" HeaderText="COMPETENZA ACCERTAMENTO"></asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BIMESTRE" HeaderText="BIMESTRE"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="COMPETENZA" HeaderText="COMPETENZA"></asp:BoundColumn>--%>
                        <asp:BoundColumn DataField="MACROCATEGORIA" HeaderText="MACROCATEGORIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="USI_ABITATIVI" HeaderText="USO UI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_UI" HeaderText="TIPOLOGIA UI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_RUOLO" HeaderText="IMP. RUOLO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_INGIUNZIONE" HeaderText="IMP. INGIUNTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RAT" HeaderText="RAT"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MOR" HeaderText="MOR"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE" HeaderText="TOTALE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INCASSATO" HeaderText="INCASSATO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DA_PAGARE" HeaderText="DA PAGARE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPOLOGIA_CONTRATTUALE" HeaderText="TIPO CONTRATTUALE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CLASSE_APPARTENENZA" HeaderText="CLASSE APPARTENENZA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>

                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <p style='page-break-after: always'>
    </p>
    <table width="100%">
        <tr>
            <td style="text-align: center">
                <asp:Label Text="Riepilogo incassi non attribuibili registrati " runat="server" ID="LabelTitoloNonAttribuibili"
                    Font-Bold="True" Font-Names="Arial" Font-Size="12pt" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGridIncassiNonAttribuibili" AutoGenerateColumns="False"
                    CellPadding="0" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None"
                    CellSpacing="1" Width="100%" ShowFooter="True" BackColor="#999999" BorderColor="#507CD1"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" AllowPaging="True" PageSize="50">
                    <AlternatingItemStyle BackColor="#EEEEEE" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="DATA_INCASSO" HeaderText="DATA REGISTRAZIONE" ItemStyle-Width="20%"
                            HeaderStyle-Width="20%">
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO" ItemStyle-Width="20%"
                            HeaderStyle-Width="20%">
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CAUSALE" HeaderText="CAUSALE" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTE" HeaderText="NOTE" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                            <HeaderStyle Width="20%"></HeaderStyle>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="ATTRIBUITO" HeaderText="ATTRIBUITO" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_ATTRIBUZIONE" HeaderText="DATA ATTRIBUZIONE" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMERO_ASSEGNO" HeaderText="NUMERO ASSEGNO" ItemStyle-Width="10%"
                            HeaderStyle-Width="10%">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>--%>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <p style='page-break-after: always'>
    </p>
    <table width="100%">
        <tr>
            <td style="text-align: center">
                <asp:Label Text="" runat="server" ID="LabelErroreIncassiAttribuiti" Font-Bold="True"
                    Font-Names="Arial" Font-Size="12pt" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label runat="server" ID="LabelTitoloIncassiAttribuiti" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12pt" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:DataGrid runat="server" ID="DataGridIncassiExtraMAV" AutoGenerateColumns="False"
                    CellPadding="0" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None"
                    CellSpacing="1" Width="100%" ShowFooter="True" BackColor="#999999" BorderColor="#507CD1"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" AllowPaging="True" PageSize="50">
                    <AlternatingItemStyle BackColor="#EEEEEE" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="COD_INTESTATARIO" HeaderText="COD_INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUMERO BOLLETTA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BOLLETTAZIONE" HeaderText="BOLLETTAZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPETENZA_ACC" HeaderText="COMPETENZA ACCERTAMENTO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BIMESTRE" HeaderText="BIMESTRE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COMPETENZA" HeaderText="COMPETENZA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MACROCATEGORIA" HeaderText="MACROCATEGORIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="USI_ABITATIVI" HeaderText="USO UI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_UI" HeaderText="TIPOLOGIA UI"></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO"></asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#999999" />
                    <FooterStyle BackColor="#FFFFFF" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                        HorizontalAlign="Center" />
                    <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                </asp:DataGrid>
            </td> 
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="HiddenFieldPrimoPiano" Value="0" />
    <asp:HiddenField runat="server" ID="hfCaricaDati" Value="0" />
    <asp:HiddenField runat="server" ID="hfCaricaExtraMAV" Value="0" />
    <asp:HiddenField runat="server" ID="hfcaricaNonAttribuibili" Value="0" />
    <br />
    </form>
    <script language="javascript" type="text/javascript">
        if (document.getElementById('HiddenFieldPrimoPiano').value == '0') {
            window.focus();
            self.focus();
        }
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
        }
        if (document.getElementById('divLoading5') != null) {
            document.getElementById('divLoading5').style.visibility = 'hidden';
        }
        function apriEccedenze() {
            popupWindow = window.open('VisualizzazioneEccedenze.aspx', 'Ecced', '');
            popupWindow.focus();
        }
    </script>
</body>
</html>
