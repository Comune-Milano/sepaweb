<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstrattoConto_Gest.aspx.vb"
    Inherits="Contabilita_EstrattoConto_Gest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Estratto Conto</title>
</head>
<script type="text/javascript" language="javascript" src="Tooltip.js"></script>
<body onload="init()">
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; width: 100%; height: 135px;">
            <tr>
                <td style="left: 0px;">
                    <div style="height: 125px">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
                                    Style="z-index: 102; right: 805px; left: 12px; top: 16px" ToolTip="Stampa in PDF" />
                                <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Contabilita/IMMCONTABILITA/Img_Export_XLS.png"
                                    Style="margin-bottom: 0px" />
                                <br />
                                <br />
                            </span>Estratto Conto <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:Label ID="LblTitolo" runat="server"></asp:Label>&nbsp;&nbsp;
                                <%--<asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Contabilita/IMMCONTABILITA/Img_Export_XLS.png"
                                    Visible="False" />--%>
                            </span></span></strong>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:Label ID="lblSottoTitolo" runat="server" Style="font-size: 10pt"></asp:Label>
                                    </span></strong>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 448px;
                        height: 13px; width: 719px;" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <%--<asp:Label ID="TBL_ESTRATTO_CONTO" runat="server" Font-Names="ARIAL" Font-Size="8pt"
            TabIndex="25" Width="98%"></asp:Label>--%>
        <table width="100%">
            <tr>
                <td>
                    <div style="overflow: auto; width: 100%;">
                        <asp:DataGrid ID="DataGridGest" runat="server" Width="100%" BackColor="White" BorderColor="#E7E7FF"
                            BorderStyle="None" BorderWidth="1px" PageSize="20" AutoGenerateColumns="False"
                            CellPadding="3" AllowPaging="True">
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <PagerStyle Mode="NumericPages" Font-Bold="true" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Wrap="False" Font-Size="7pt" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" Width="0px"
                                        BackColor="White" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_BOLL" HeaderText="NUM.BOL.">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="num_tipo" HeaderText="TIPO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="riferimento_da" HeaderText="PERIODO DAL">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="riferimento_a" HeaderText="PERIODO AL">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="data_emissione" HeaderText="DATA EMISSIONE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="importobolletta" HeaderText="IMP. EMESSO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="note" HeaderText="NOTE">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO_APPLICAZIONE" ReadOnly="True" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle Font-Bold="True" Font-Names="Courier New" Font-Size="8pt" Wrap="False"
                                BackColor="#006699" ForeColor="#F7F7F7" />
                            <ItemStyle ForeColor="black" BackColor="#E7E7FF" />
                            <PagerStyle ForeColor="#0000CC" HorizontalAlign="Left" Mode="NumericPages" BackColor="White" />
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
        <div style="display: none; z-index: 50px;">
            <asp:DataGrid ID="DataGrid1Pdf" runat="server" Width="100%" PageSize="20" AutoGenerateColumns="False"
                HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderWidth="1px" HeaderStyle-BorderColor="#959595"
                BorderColor="#959595" BorderStyle="Solid" BorderWidth="1px">
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID_BOLLETTA" ReadOnly="True" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM_BOLL" HeaderText="NUM.BOL.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="num_tipo" HeaderText="TIPO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="riferimento_da" HeaderText="PERIODO DAL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="riferimento_a" HeaderText="PERIODO AL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="data_emissione" HeaderText="DATA EMISSIONE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="importobolletta" HeaderText="IMP. EMESSO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="note" HeaderText="NOTE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="Black"
                            BorderStyle="Solid" BorderWidth="1px" BorderColor="#959595" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" ReadOnly="True" Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_APPLICAZIONE" ReadOnly="True" Visible="False"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <asp:Label ID="lblInformazione" runat="server" Font-Names="Courier New" Font-Size="8pt"></asp:Label>
        <br />
        <span>
            <br />
        </span>
    </div>
    <div id="a" style="font-family: Arial; font-size: 7pt; background-color: #CEE3F6;
        width: 150px; height: 49px; border: solid 1px gray; text-align: left;">
    </div>
    <asp:HiddenField ID="idBolletta" runat="server" Value="0" />
    <asp:HiddenField ID="txtsldinizio" runat="server" />
    <asp:HiddenField ID="txtsldfine" runat="server" />
    </form>
</body>
<script type="text/javascript">
    var t1 = null;
    var piega = "L'elenco delle bollette è "
    var sinizio = "Dovuto alla prima data inserita.";
    var sfine = "Dovuto alla seconda data inserita, se assente ad oggi.";
    var numBol = "Numero identificativo della bolletta"
    var numRata = "Numero rata della bolletta"
    var tipoR = "Tipologia della bolleta"
    var riferimento = "Periodo di date a cui è riferita la bolletta"
    var emissione = "Data in cui è stata emessa la bolletta"
    var scadenza = "Data di scadenza della bolletta"
    var bollettato = "Importo totale della bolleta"
    var riclass = "Sommatoria delle bollette riclassificate, se trattasi di bolletta di fine contratto/morosità"
    var impCont = "BOLLETT.- QUOTE SINDACALI. Se bolletta di fine contratto o morosità è BOLLETT.- RICLASSIFICATE"
    var dataPag = "Data del pagamento"
    var impIncassato = "Importo incassato"
    var saldoContab = "Differenza tra IMPORTO CONTABILE e IMPORTO INCASSATO"
    var gRitardo = "Giorni di ritardo dalla data di scadenza a quella di pagamento"
    function init() {
        t1 = new ToolTip("a", true, 40);
    }

    var Selezionato;
    function apriMorosita() {
        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('../Contratti/Dettagli.aspx?ID=' + document.getElementById('idBolletta').value, 'Dettagli', 'height=200,top=' + TopPosition + ',left=' + LeftPosition + ',width=400');
    }
</script>
</html>
