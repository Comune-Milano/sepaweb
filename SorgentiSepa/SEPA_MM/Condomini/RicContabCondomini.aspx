<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicContabCondomini.aspx.vb"
    Inherits="Condomini_RicContabCondomini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Contabilità Rendicontate e non pagate</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 14pt;
            color: #800000;
        }
        
        #form1
        {
            width: 782px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');

        }
    </script>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <strong>REPORT CONTABILITA&#39; CONDOMINI</strong>
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
                        <td style="font-family: Arial; font-size: 8pt; text-decoration: underline; text-align: center;"
                            width="50%">
                            <strong style="text-decoration: underline; width: 100%;">ELENCO AMMINISTRATORI</strong>
                        </td>
                        <td style="font-family: Arial; font-size: 8pt; text-decoration: underline; text-align: center;"
                            width="50%">
                            <strong style="width: 100%">ELENCO CONDOMINI</strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <div style="height: 350px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkAmministratori" runat="server" Width="90%" CellPadding="1"
                                    CellSpacing="1" DataTextField="AMMINISTRATORE" DataValueField="ID" Font-Names="Arial"
                                    Font-Size="8pt" AutoPostBack="True" meta:resourcekey="chkAmministratoriResource1">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                        <td style="width: 30%">
                            <div style="height: 350px; width: 100%; overflow: auto;">
                                <asp:CheckBoxList ID="chkCondomini" runat="server" Width="92%" CellPadding="1" CellSpacing="1"
                                    DataTextField="DENOMINAZIONE" DataValueField="ID" Font-Names="Arial" Font-Size="8pt"
                                    meta:resourcekey="chkCondominiResource1">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelAmm" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Text="Seleziona/Deseleziona Tutti" Width="172px" meta:resourcekey="btnSelAmmResource1" />
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSelCondomini" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Text="Seleziona/Deseleziona Tutti" Width="172px" meta:resourcekey="btnSelCondominiResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: top;">
                            <table>
                                <tr>
                                    <td style="font-family: Arial; font-size: 8pt">
                                        Anno di Gestione da
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAnnoInizio" runat="server" Font-Italic="False" Font-Names="Arial"
                                            Font-Size="8pt" Width="50px" MaxLength="4"></asp:TextBox>
                                    </td>
                                    <td style="font-family: Arial; font-size: 8pt">
                                        a
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAnnoFine" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Width="50px" MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left; vertical-align: top;">
                            <table width="100%">
                                <tr>
                                    <td style="font-family: Arial; font-size: 8pt; text-align: left; vertical-align: top;">
                                        <asp:CheckBox ID="chkConsunt" runat="server" 
                                            Text="Confronto con Consuntivo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Arial; font-size: 8pt; vertical-align: top; text-align: left;">
                                        <asp:CheckBox ID="chkAnnoPrec" runat="server" 
                                            Text="Confronto con anno precedente" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            &nbsp;
                        </td>
                        <td style="text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:HiddenField ID="SelAmminist" runat="server" Value="0" />
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
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            &nbsp;
                        </td>
                        <td style="text-align: center; width: 25%;">
                            <asp:ImageButton ID="btnExportXls" runat="server" ImageUrl="../NuoveImm/Img_Export_Grande.png"
                                ToolTip="Esporta in formato XLS" meta:resourcekey="btnExportXlsResource1" />
                        </td>
                        <td style="text-align: center; width: 25%">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                ToolTip="Torna alla pagina Home" />
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
                <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AMMINISTRATORE" HeaderText="AMMINISTRATORE">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA_INIZIO">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA_FINE">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATO_BILANCIO" HeaderText="STATO BILANCIO">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NR_RATE" HeaderText="N° RATE">
                    <HeaderStyle Width="40px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="VOCE" HeaderText="VOCE">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
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
    <asp:HiddenField ID="idPianoF" runat="server" Value="0" />
    <asp:HiddenField ID="IdVoceMorosita" runat="server" Value="0" />
    </form>
</body>
</html>
