<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstrattoConto.aspx.vb" Inherits="Contabilita_EstrattoConto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estratto Conto</title>
</head>
<script type ="text/javascript" language="javascript" src="Tooltip.js"></script>
<body onload=init()>
    <form id="form1" runat="server">
    <div>
    
        <table style="left: 0px; 
            width: 100%; height: 135px;">
            <tr>
                <td style="left: 0px;">
                    <div style="height: 186px">
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
                        
                        
                        
                        Style="z-index: 102; right: 805px; left: 12px; top: 16px"
                        ToolTip="Stampa in PDF" />
                        <asp:ImageButton ID="ImgBtnExport" runat="server" 
                            ImageUrl="~/Contabilita/IMMCONTABILITA/Img_Export_XLS.png" 
                            style="margin-bottom: 0px" />
                        <br />
                        <br />
                        </span>Estratto Conto <span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp;<asp:Label ID="LblTitolo"
                            runat="server"></asp:Label>&nbsp;&nbsp;
                        </span>
                        </span>
                        </strong>
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                    <asp:Label ID="lblSottoTitolo"
                            runat="server" style="font-size: 10pt"></asp:Label>
                        </span>
                        </span>
                        </strong>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblSaldoInizio" runat="server" Font-Names="Courier New" 
                                        Font-Size="8pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblSaldoFine" runat="server" Font-Names="Courier New" 
                                        Font-Size="8pt"></asp:Label>
                                    <br />
                                </td>
                            </tr>
                            </table>

                        
                        </div>

                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 13px; position: absolute; top: 46px; height: 13px; width: 719px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    
                                    <asp:Label ID="TBL_ESTRATTO_CONTO" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="25" 
            Width="98%"></asp:Label>
    
        <br />
        <br />
                                    <asp:Label ID="lblInformazione" runat="server" Font-Names="Courier New" 
                                        Font-Size="8pt"></asp:Label>
    
        <br />
        <span>
        <br />
        </span>
    
                                    <asp:Label ID="TBL_ANNULLATE0" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="10pt" TabIndex="25" 
            Width="98%" Font-Bold="True">ELENCO BOLLETTE ANNULATE</asp:Label>
    
        <br />
    
                                    <br />
    
                                    <asp:Label ID="TBL_ANNULLATE" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="25" 
            Width="98%"></asp:Label>
    
    </div>
    <div id="a" style="font-family :Arial ;font-size:7pt;background-color:#CEE3F6;width: 150px; height: 49px;border: solid 1px gray; text-align: left;">
    </div>
                            <asp:HiddenField ID="txtsldinizio" runat="server" />
                        <asp:HiddenField ID="txtsldfine" runat="server" />

    </form>
</body>
<script type="text/javascript" >
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
</script>
</html>
