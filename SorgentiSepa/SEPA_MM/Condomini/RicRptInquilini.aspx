<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicRptInquilini.aspx.vb"
    Inherits="Condomini_RicRptInquilini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Report Inquilini</title>
    <script language ="javascript" type ="text/jscript" >
        function downloadFile(filePath) {
            if (document.getElementById('noClose')) {
                var a = document.getElementById('noClose').value;
                document.getElementById('noClose').value = '0'
                location.replace('' + filePath + '');
                document.getElementById('noClose').value = a

            }
            else {
                location.replace('' + filePath + '');

            }
        };
    
    </script>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>

    <form id="form1" runat="server">
    <div style="width: 800px; height: 570px">
        <table width="97%" cellpadding="0" cellspacing="0">
            <tr style="height: 27px">
                <td>
                    <asp:Label ID="lbltitle" runat="server" Text="REPORT ANAGRAFICA INQUILINI" Font-Names="arial"
                        Font-Size="14pt" ForeColor="Maroon" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                <center>
                                    <asp:Label ID="lblcondomini" runat="server" Text="ELENCO CONDOMINI" Font-Names="arial"
                                        Font-Size="8pt" Font-Bold="True" Font-Underline="True"></asp:Label>
                                </center>
                            </td>
                            <td style="width: 50%">
                                <center>
                                    <asp:Label ID="lblamministratori" runat="server" Text="ELENCO AMMINISTRATORI" Font-Names="arial"
                                        Font-Size="8pt" Font-Bold="True" Font-Underline="True"></asp:Label>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height: 450px; width: 100%; overflow: auto;">
                                    <asp:CheckBoxList ID="chkCondomini" runat="server" Width="92%" CellPadding="1" CellSpacing="1"
                                        DataTextField="DENOMINAZIONE" DataValueField="ID" Font-Names="Arial" Font-Size="8pt"
                                        AutoPostBack="true">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td>
                                <div style="height: 450px; width: 100%; overflow: auto;">
                                    <asp:CheckBoxList ID="chkAmministratori" runat="server" Width="90%" CellPadding="1"
                                        CellSpacing="1" DataTextField="AMMINISTRATORE" DataValueField="ID" Font-Names="Arial"
                                        Font-Size="8pt" AutoPostBack="True">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Button ID="btnSelCondomini" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" Text="Seleziona/Deseleziona Tutti" Width="172px" />
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnSelAmm" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    Text="Seleziona/Deseleziona Tutti" Width="172px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                    <asp:ImageButton ID="btnExportXls" runat="server" ImageUrl="../NuoveImm/Img_Export_Grande.png"
                                        ToolTip="Esporta in formato XLS" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" /></center>
                            </td>
                            <td>
                                <center>
                                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                        ToolTip="Torna alla pagina Home" /></center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="SelAmminist" runat="server" Value="0" />
    <asp:HiddenField ID="SelCondomini" runat="server" Value="0" />
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
                <asp:BoundColumn DataField="COD_UNITA" HeaderText="COD.UNITA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="120px" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SCALA" HeaderText="SCALA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATO_OCCUPAZIONE" HeaderText="STATO OCCUPAZIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AMMINISTRATORE" HeaderText="AMMINISTRATORE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RECAPITO" HeaderText="RECAPITO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATO" HeaderText="STATO CONTRATTO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POS. BILANCIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_COMP_NUCLEO" HeaderText="NUM. COMP. NUCLEO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_OSPITI" HeaderText="NUM. OSPITI">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MIL_PRO" HeaderText="MILL. PROPIETA'">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MIL_ASC" HeaderText="MILL. ASCENSORE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MIL_COMPRO" HeaderText="MILL. COMPROPIETA'">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MIL_GEST" HeaderText="MILL. GESTIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MIL_RISC" HeaderText="MILL. RISCALDAMENTO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MILL_PRES_ASS" HeaderText="MILL. PRESENZA ASS.">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" />
        </asp:DataGrid>
    </span></strong>
                    <script language="javascript" type="text/javascript">
                        document.getElementById('splash').style.visibility = 'hidden';
    </script>
    </form>

</body>
</html>
