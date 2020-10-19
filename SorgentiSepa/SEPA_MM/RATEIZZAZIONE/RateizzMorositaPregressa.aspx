<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RateizzMorositaPregressa.aspx.vb" Inherits="RATEIZZAZIONE_RateizzMorositaPregressa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Rateizzazione Morosità Pregressa</title>
    <style type="text/css">
        .style1 {
            font-family: ARIAL;
            font-weight: bold;
            font-size: 12pt;
            color: black;
            text-align: center;
        }
    </style>
   <link href="style/Site.css" rel="stylesheet" type="text/css" />
    <script src="jsRateizz.js" type="text/javascript"></script>
</head>
<body>
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500;">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../NuoveImm/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="../immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <table style="width: 100%;" class="style1">
                        <tr>
                            <td>RATEIZZAZIONE MOROSITA' PREGRESSA</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>INFO CONTRATTUALI</legend>
                        <table style="width: 100%;">

                            <tr>
                                <td>TIPO CONTRATTO
                                </td>
                                <td>
                                    <asp:Label ID="cmbTipoContr" runat="server" Font-Names="Arial"
                                        Font-Size="10pt" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Underline="False">
                                    </asp:Label>
                                </td>
                                <td>COD. CONTRATTO
                                </td>
                                <td>
                                    <asp:Label ID="lblCodContratto" runat="server" Font-Names="Arial"
                                        Font-Size="10pt" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Underline="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>COD. UI
                                </td>
                                <td>
                                    <asp:Label ID="lblCodUI" runat="server" Font-Names="Arial"
                                        Font-Size="10pt" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Underline="False"></asp:Label>
                                </td>
                                <td>INDIRIZZO
                                </td>
                                <td>
                                    <asp:Label ID="lblIndirizzo" runat="server" Font-Names="Arial"
                                        Font-Size="10pt" Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Underline="False"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>INTESTATARIO
                                </td>
                                <td>
                                    <asp:Label ID="lblIntest" runat="server" Font-Names="Arial" Font-Size="10pt"
                                        Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Underline="False"></asp:Label>
                                </td>
                                <td>C.F./P.IVA
                                </td>
                                <td>
                                    <asp:Label ID="lblCfIva" runat="server" Font-Names="Arial" Font-Size="10pt"
                                        Font-Bold="True" ForeColor="Black" Font-Italic="False" Font-Underline="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <fieldset>
                                        <legend>ALTRI PIANI DI RIENTRO</legend>
                                        <div style="height: 75px; overflow: auto;">
                                            <asp:DataGrid runat="server" ID="dgvAltrRateizzi" AutoGenerateColumns="False" CellPadding="1"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                                                Width="98%" PageSize="3">
                                                <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="DATA" HeaderText="DATA"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DEBITO" HeaderText="DEBITO"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NUM_RATE" HeaderText="NUM. RATE"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    HorizontalAlign="Center" />
                                                <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                                                <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                                            </asp:DataGrid>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>INFO CONTABILI</legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDataSaldo" runat="server" Text="DATA SALDO" Font-Names="Arial"
                                        Font-Size="10pt" Font-Bold="True" ForeColor="Black" Width="100%" Font-Italic="False"
                                        Font-Underline="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataSaldo" runat="server" Font-Names="Arial"
                                        Font-Size="10pt" Width="70px" MaxLength="10"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnRefreshSaldo" runat="server"
                                        ImageUrl="../Contratti/Pagamenti/Image/refresh-icon.png"
                                        OnClientClick="caricamentoincorso();" />
                                </td>
                                <td>
                                    <asp:Label ID="lblSaldo" runat="server" Text="DATA SALDO" Font-Names="Arial" Font-Size="10pt"
                                        Font-Bold="True" ForeColor="Black" Width="100%" Font-Italic="True" Font-Underline="True"
                                        Font-Strikeout="False"></asp:Label>
                                </td>
                            </tr>
                            <tr><td>&nbsp</td></tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblFascia" runat="server" BackColor="#FFFF66" Font-Bold="True" Font-Italic="False"
                                        Font-Names="Arial" Font-Size="10pt" Font-Strikeout="False" Font-Underline="True"
                                        ForeColor="Black" Width="100%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3" style="text-align: right">
                                <asp:ImageButton ID="btnConfProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi.png"
                                    ToolTip="Conferma la selezione, e procedi con il trasferimento" OnClientClick="caricamentoincorso();" />
                                <img id="exit" alt="Esci" src="../NuoveImm/Img_Esci_AMM.png" title="Esci" style="cursor: pointer"
                                    onclick="self.close();" />
                            </td>
                        </tr>

                    </table>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="codContratto" runat="server" />
        <asp:HiddenField ID="idContratto" runat="server" />
        <asp:HiddenField ID="idAna" runat="server" />
        <asp:HiddenField ID="tipoContrINIZIALE" runat="server" Value="-1" />
        <asp:HiddenField ID="titolINIZIALE" runat="server" Value="-1" />
        <asp:HiddenField ID="idRateizz" runat="server" Value="0" />
        <asp:HiddenField ID="ConfSave" runat="server" Value="0" />
        <asp:HiddenField ID="Fascia" runat="server" Value="" />
        <asp:HiddenField ID="tipoPiano" runat="server" Value="M" />
        <asp:HiddenField ID="TipoAllegato" runat="server" Value="M" />
        <asp:HiddenField ID="sIntestatario" runat="server" Value="" />
        <asp:HiddenField ID="sCodice" runat="server" Value="" />
        <asp:HiddenField ID="CodCatContr" runat="server" Value="" />
        <asp:HiddenField ID="sDataSald" runat="server" Value="" />
        <asp:HiddenField ID="dataPres" runat="server" Value="" />
    </form>
    <script type="text/javascript">
        document.getElementById('caricamento').style.visibility = 'hidden';
    </script>
</body>
</html>
