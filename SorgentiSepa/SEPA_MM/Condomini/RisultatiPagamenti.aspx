<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiPagamenti.aspx.vb"
    Inherits="Condomini_RisultatiPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Risultati Pagamenti</title>
    <style type="text/css">
        #form1
        {
            width: 778px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="font-family: Arial; font-size: 14pt; color: #801f1c;">
                <strong>Risultati Ricerca Pagamenti</strong>
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 6pt">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div id="risultPagamenti" style="width: 99%; height: 430px; overflow: auto;">
                    <asp:DataGrid Style="z-index: 105" AutoGenerateColumns="False" BackColor="White"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ID="DataGridPagamenti"
                        PageSize="22" runat="server" Width="97%" BorderColor="#CCCCCC" BorderWidth="1px"
                        CellPadding="0">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_CONDOMINIO" HeaderText="ID_CONDOMINIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_PAGAMENTO" HeaderText="ID_PAGAMENTO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="CONDOMINIO">
                                <HeaderStyle Width="12%" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_APPROVATO" HeaderText="IMPORTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="5%" Wrap="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PROGR_ANNO" HeaderText="N°/ANNO A.D.P.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="5%" Wrap="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                                <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                                    HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                                    HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VOCE_BUDGET" HeaderText="VOCE BUDGET">
                                <HeaderStyle Width="23%" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Font-Size="7pt" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE PAGAMENTO">
                                <HeaderStyle Width="33%" Font-Bold="True" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000CC" Wrap="False" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="Immagini/Img_Export_Grande.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="Immagini/Img_Stampa.png"
                                OnClientClick="document.getElementById('caricamento').style.visibility = 'visible'" />
                        </td>
                        <td>
                        <asp:ImageButton ID="btnNuovaRicerca" runat="server" ImageUrl="../NuoveImm/Img_NuovaRicerca.png"
                                OnClientClick="document.getElementById('caricamento').style.visibility = 'visible'" />
                                                   </td>
                        <td style="text-align: right">
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                
                                OnClientClick="document.getElementById('caricamento').style.visibility = 'visible'" />
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
    </table>
    <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
        top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
        z-index: 500" id="caricamento">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2" runat="server" ImageUrl="Immagini/load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('caricamento').style.visibility = 'hidden';

    </script>
    </form>
</body>
</html>
