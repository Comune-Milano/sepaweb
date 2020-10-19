<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoIndiretti.aspx.vb" Inherits="Condomini_ElencoIndiretti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Anagrafica Condomini Amministrazione Indiretta</title>
    <script type="text/javascript">
        function Apri() {
            if (document.getElementById('txtid').value != 0) {
                window.open('ContabIndiretta.aspx?IdCond=' + document.getElementById('txtid').value, 'ContIndiretta', 'toolbar=no,menubar=no,location=no,resizable=yes,height=600,width=1000,scrollbars=no,status=yes');
            }
            else {
                alert('Selezionare un condominio da visualizzare!');
            }
        }
        var Selezionato;
    </script>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Elenco Anagrafico
        Condomini
        Amm. Indiretta
        <asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label"></asp:Label></span></strong>
    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" ReadOnly="True"
        Style="z-index: 2; left: 13px; position: absolute; top: 528px" Width="632px">Nessuna Selezione</asp:TextBox>
    &nbsp;
    <table style="width: 100%;">
        <tr>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 3px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 6pt">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <div style="left: 582px; overflow: auto; width: 781px; height: 446px">
                        <asp:DataGrid ID="DataGridCondom" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="762px"
                            CellPadding="1" CellSpacing="1">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONDOMINIO" HeaderText="COD. CONDOMINIO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CITTA" HeaderText="CITTA'"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AMMINIST" HeaderText="AMMINISTRATORE"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
                    </div>
                </span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png"
        Style="z-index: 107; left: 9px; position: absolute; top: 551px" ToolTip="Esporta in Excel" />
    <img alt="" src="../NuoveImm/Img_Visualizza.png" style="position: absolute; top: 552px;
        left: 581px; cursor: pointer; right: 271px;" onclick="Apri();" title="Visualizza" />
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:HiddenField ID="txtid" runat="server" Value="0" />
    </span></strong>
    </form>
</body>
</html>
