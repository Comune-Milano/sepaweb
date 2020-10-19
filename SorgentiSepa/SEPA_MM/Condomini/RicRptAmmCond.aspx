<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicRptAmmCond.aspx.vb" Inherits="Condomini_RicRptAmmCond" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Report Anagrafica Amministratori Condominiali</title>
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
            width: 779px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <strong>REPORT ANAGRAFICA AMMINISTRATORI CONDOMINIALI</strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <strong style="text-decoration: underline">SELEZIONA ALMENO UN ELEMENTO DALL&#39;
                ELENCO DEGLI AMMINISTRATORI CONDOMINIALI</strong></td>
        </tr>
        <tr>
            <td class="style2">
                <div style="height: 380px; width: 550px; overflow: auto;">
                    <asp:CheckBoxList ID="chkAmministratori" runat="server" Width="90%" 
                        CellPadding="1" CellSpacing="1" DataTextField="AMMINISTRATORE" 
                        DataValueField="ID" Font-Names="Arial" Font-Size="8pt">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnSeleziona" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt" Text="Seleziona/Deseleziona Tutti" 
                    Width="172px" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:HiddenField ID="Selezionati" runat="server" Value="0" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnExportXls" runat="server" 
                                ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png" 
                                ToolTip="Esporta in formato XLS" />
                        </td>
                        <td>
                            <img alt="Torna alla pagina HOME" src="Immagini/Img_Home.png" 
                                style="float: right; cursor:pointer "
                                onclick="document.location.href='pagina_home.aspx';" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <asp:DataGrid ID="dgvExport" runat="server" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="20" Style="z-index: 105; left: 193px; top: 54px" Width="99%" CellPadding="1"
                CellSpacing="1" Visible="False">
                <PagerStyle Mode="NumericPages" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle ForeColor="Black" />
                <Columns>
                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Width="200px" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOME" HeaderText="NOME">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Width="200px" Wrap="False"/>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="300px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TEL_1" HeaderText="TELEFONO 1">
                        <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="50px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TEL_2" HeaderText="TELEFONO 2">
                        <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="50px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CELL" HeaderText="CELLULARE">
                        <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="50px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FAX" HeaderText="FAX">
                        <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="50px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="EMAIL" HeaderText="E-MAIL">
                        <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="200px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                        <HeaderStyle  Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                             Wrap="False" Width="500px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                            Wrap="False" />
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
