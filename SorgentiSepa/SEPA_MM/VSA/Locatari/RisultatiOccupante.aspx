<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiOccupante.aspx.vb"
    Inherits="PED_RisultatiOccupante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca U.I.</title>
</head>
<body style="background-attachment: fixed; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <form runat="server">
    <div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 65px;
            position: absolute; top: 495px" Visible="False" Width="78px" ForeColor="White">Label</asp:Label>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="15" Style="z-index: 101; left: 3px; position: absolute; top: 65px;
            width: 778px;">
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="IDCONTR" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="NOME COMP.">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD. UNITA">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTR.">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_TIPOLOGIA" HeaderText="TIPOLOGIA">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="INDIRIZZO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CIVICO" HeaderText="N. CIVICO"></asp:BoundColumn>
                
            </Columns>
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Mode="NumericPages" Wrap="False" />
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 102; left: 5px;
            position: absolute; top: 494px" Visible="False" Width="57px" ForeColor="White">Label</asp:Label>
        &nbsp;
        <table style="left: 0px; width: 792px; position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Unità Imm. Trovate<asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label"></asp:Label>
                    </strong></span>
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
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                        Style="z-index: 2; left: 7px; position: absolute; top: 364px" 
                        Width="632px">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 106; left: 572px; position: absolute; top: 403px" 
            ToolTip="Nuova Ricerca" />
        <asp:ImageButton ID="btnSeleziona" runat="server" ImageUrl="../../NuoveImm/Img_Seleziona.png"
            Style="z-index: 102; left: 445px; position: absolute; top: 404px" 
            ToolTip="Seleziona" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 718px; position: absolute; top: 403px" 
            ToolTip="Home" />
            <asp:HiddenField ID="CODUI" runat="server" />
            <asp:HiddenField ID="CODCONTR" runat="server" />
            <asp:HiddenField ID="IDCONTR" runat="server" />
            <asp:HiddenField ID="NOMEINTEST" runat="server" />
    </div>
    </form>
     <script  language="javascript" type="text/javascript">
         var Selezionato;
    </script>
</body>
</html>
