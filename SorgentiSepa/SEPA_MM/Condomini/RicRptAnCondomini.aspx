<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicRptAnCondomini.aspx.vb"
    Inherits="Condomini_RicRptAnCondomini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Report Condomini</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 14pt;
            color: #800000;
        }
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
        #form1
        {
            width: 782px;
        }
        .style3
        {
            font-family: Arial;
            font-size: 8pt;
            text-decoration: underline;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <strong>REPORT ANAGRAFICA CONDOMINI</strong>
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
                        <td class="style2">
                            <strong style="text-decoration: underline; width: 25%;">ELENCO AMMINISTRATORI</strong>
                        </td>
                        <td class="style3">
                            <strong style="width: 50%">ELENCO INDIRIZZI</strong>
                        </td>
                        <td class="style3">
                            <strong style="width: 25%">ELENCO CONDOMINI</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <div style="height: 400px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkAmministratori" runat="server" Width="90%" CellPadding="1"
                                    CellSpacing="1" DataTextField="AMMINISTRATORE" DataValueField="ID" Font-Names="Arial"
                                    Font-Size="8pt" AutoPostBack="True">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td style="width: 40%">
                            <div style="height: 400px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkIndirizzi" runat="server" Width="97%" CellPadding="1" CellSpacing="1"
                                    DataTextField="descIndirizzo" DataValueField="ID" Font-Names="Arial" Font-Size="8pt"
                                    AutoPostBack="True">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td style="width: 30%">
                            <div style="height: 400px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkCondomini" runat="server" Width="92%" CellPadding="1" CellSpacing="1"
                                    DataTextField="DENOMINAZIONE" DataValueField="ID" Font-Names="Arial" 
                                    Font-Size="8pt">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelAmm" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Text="Seleziona/Deseleziona Tutti" Width="172px" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelIndirizzi" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Text="Seleziona/Deseleziona Tutti" Width="172px" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelCondomini" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Text="Seleziona/Deseleziona Tutti" Width="172px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:HiddenField ID="SelAmminist" runat="server" Value="0" />
                        </td>
                        <td style="text-align: right">
                            <asp:HiddenField ID="SelIndirizzi" runat="server" Value="0" />
                        </td>
                        <td style="text-align: right">
                            <asp:HiddenField ID="SelCondomini" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnExportXls" runat="server" ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png"
                                ToolTip="Esporta in formato XLS" />
                        </td>
                        <td>
                            <img alt="Torna alla pagina HOME" src="Immagini/Img_Home.png" style="float: right;
                                cursor: pointer" onclick="document.location.href='pagina_home.aspx';" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:DataGrid ID="dgvExport" runat="server" AutoGenerateColumns="False" BackColor="White"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="20" Style="z-index: 105; left: 193px; top: 54px" Width="99%" CellPadding="1"
            CellSpacing="1" Visible="False">
            <PagerStyle Mode="NumericPages" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <ItemStyle ForeColor="Black" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="COD_CONDOMINIO" HeaderText="COD. CONDOMINIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                        Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                        Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CITTA" HeaderText="CITTA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                        Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AMMINIST" HeaderText="AMMINISTRATORE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                        Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="data_costituzione" HeaderText="DATA COSTITUZIONE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO_GESTIONE" HeaderText="TIPO GESTIONE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GESTIONE" HeaderText="PERIODO GESTIONE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                        Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IBAN" HeaderText="IBAN">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MIL_PRES_ASS_TOT_COND" 
                    HeaderText="MILL. PRES. ASSEMBLEA">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" />
        </asp:DataGrid>
    </span></strong>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
