<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiPagamentiPerStrutturaCompleta.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiPagamentiPerStrutturaCompleta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Situazione Contabile</title>
    <script type="text/javascript">
        function ApriPagamenti(id_p, tipo) {
            var data = new Date();
            data = data.getFullYear().toString() + data.getMonth().toString() + data.getDay().toString() + data.getHours().toString() + data.getMinutes().toString() + data.getSeconds().toString();
            if (tipo == 3) {
                window.open('../MANUTENZIONI/SAL.aspx?ID=' + id_p + '&PROVENIENZA=CHIAMATA_DIRETTA', 'Dettagli' + data, 'height=560,top=0,left=0,width=800');
            }
            if (tipo == 4) {
                window.open('../PAGAMENTI/Pagamenti.aspx?ID=' + id_p + '&PROVENIENZA=CHIAMATA_DIRETTA', 'Dettagli' + data, 'height=560,top=0,left=0,width=800');
            }
            if (tipo == 6) {
                window.open('../PAGAMENTI_CANONE/Pagamenti.aspx?ID=' + id_p + '&PROVENIENZA=CHIAMATA_DIRETTA', 'Dettagli' + data, 'height=560,top=0,left=0,width=800');
            }
            if (tipo == 7) {
                window.open('../RRS/SAL_RRS.aspx?ID=' + id_p + '&PROVENIENZA=CHIAMATA_DIRETTA', 'Dettagli' + data, 'height=560,top=0,left=0,width=800');
            }
        }
        function Messaggio() {
            var pagamento = document.getElementById('Pagamento').value;
            if (pagamento == '1') {
                var chiediConferma = window.confirm('Questa operazione potrebbe richiedere alcuni minuti. Proseguire?');
                if (chiediConferma == true) {
                    document.getElementById('ConfermaPagamento').value = "1";
                    document.getElementById('Blocco').style.visibility = 'visible';
                }
            }
            document.body.style.overflow = 'hidden';
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="URLdiProvenienza" Value="" />
    <table width="100%">
        <tr>
            <td colspan="2" style="height: 7px; width: 100%">
            </td>
        </tr>
        <tr>
            <td style="text-align: left; height: 21px; width: 90%;">
                <asp:ImageButton ID="indietro" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                    alt="indietro" TabIndex="-1" ToolTip="Indietro" />
                <a href="RisultatiPagamentiPerStrutturaCompletaS.aspx"><img alt="" 
                    src="../../../NuoveImm/Img_ExportExcel.png" style="border-style: none" /></a>
                <asp:ImageButton ID="btnStampaPDF" runat="server" ImageUrl="../../../NuoveImm/Img_Stampa.png"
                    OnClientClick="javascript:Messaggio();" ToolTip="Stampa in PDF" TabIndex="-1" />
            </td>
            <td style="text-align: right; height: 21px; width: 10%;">
                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png"
                    ToolTip="Esci" TabIndex="-1" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="style1">
                <asp:Label ID="lblTitolo" runat="server" Font-Names="Arial" Font-Size="14pt" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 7px;" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblErrore" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                    CellPadding="4" ForeColor="#333333" Width="99%" BorderColor="#507CD1" BorderStyle="Solid"
                    PageSize="500">
                    <AlternatingItemStyle BackColor="WHITE" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_PAG" HeaderText="ID PAGAMENTO" Visible="False">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="4%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" Width="4%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VOCE" HeaderText="VOCE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="14%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="True" Width="14%" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE_TIPO" HeaderText="TIPO PAGAMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="True" Width="6%" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE_PAG" HeaderText="DESCRIZIONE PAGAMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="12%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="true" Width="12%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE T.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="8%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="True" Width="8%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO CONSUNTIVATO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" Width="6%" HorizontalAlign="right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="15%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="True" Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ADP" HeaderText="ADP">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_ADP" HeaderText="DATA ADP">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="6%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_ADP" HeaderText="IMPORTO ADP">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="6%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" Width="6%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MAE" HeaderText="MAE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" Width="11%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" Width="11%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_ADP" HeaderText="TIPO_ADP" Visible="False">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_PRE" HeaderText="ID PRENOTAZIONE" Visible="False">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="True" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <EditItemStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                        ForeColor="White" HorizontalAlign="Center" />
                    <ItemStyle BackColor="White" Font-Names="Arial" Font-Size="8pt" />
                    <PagerStyle BackColor="White" ForeColor="#507CD1" HorizontalAlign="Center" Mode="NumericPages"
                        Position="TopAndBottom" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="codicevoce" runat="server" />
    <asp:HiddenField ID="idPagamentoPrec" runat="server" Value="-1" />
    <asp:HiddenField ID="Pagamento" runat="server" Value="0" />
    <asp:HiddenField ID="ConfermaPagamento" runat="server" Value="0" />
    <div align='center' id='Blocco' style='visibility: hidden; position: absolute; background-color: #ffffff;
        text-align: center; width: 99%; height: 99%; top: 10px; left: 10px; z-index: 10;
        border: 1px dashed #660000; font: verdana; font-size: 10px; vertical-align: middle;'>
        <table width="100%" style="height: 100%">
            <tr>
                <td style="height: 100%; width: 100%; text-align: center;">
                    <img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' />
                    <br />
                    <asp:Label ID="label27" runat="server" Font-Names="Arial" Font-Size="9pt">caricamento in corso...</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('Blocco').style.visibility = 'hidden';
    //document.getElementById('dvvvPre').style.visibility = 'hidden';
    document.body.style.overflow = 'auto';
</script>
</html>
