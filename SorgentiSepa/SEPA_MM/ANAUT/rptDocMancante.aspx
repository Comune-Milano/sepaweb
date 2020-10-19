<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptDocMancante.aspx.vb"
    Inherits="ANAUT_rptDocMancante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Doc.Mancante</title>
</head>
<body bgcolor="#f2f5f1">
    <script type="text/javascript">
        //document.onkeydown=$onkeydown;
    </script>
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco Doc. Mancante</strong></span><br />
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
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 539px; position: absolute; top: 507px" TabIndex="11"
        ToolTip="Home" />
        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export.png"
        Style="z-index: 100; left: 17px; position: absolute; top: 507px" TabIndex="11"
        ToolTip="Home" />
    <asp:Label ID="lblBando" runat="server" Font-Size="12pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 102; left: 15px; position: absolute; top: 60px; width: 163px;">Anagrafe Utenza</asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="cmbBando" TabIndex="1" runat="server" Height="35px" Style="border: 1px solid black;
                z-index: 111; left: 165px; position: absolute; top: 57px" Width="250px" AutoPostBack="True"
                Font-Names="arial" Font-Size="12pt">
            </asp:DropDownList>
            <div id="Contenitore" 
                
                style="position: absolute; width: 636px; height: 338px; overflow: auto; visibility: visible; top: 112px; left: 16px;">
            <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                        Font-Size="8pt" PageSize="13" Style="z-index: 105; left: 1px; " Font-Bold="False"
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                        GridLines="None" CellPadding="4" ForeColor="#333333" Width="1300px">
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#507CD1"
                            ForeColor="White"></HeaderStyle>
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="AU" HeaderText="A.U.">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PG" HeaderText="PG A.U.">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. R.U.">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME" HeaderText="NOME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOC_MANCANTE" HeaderText="DOCUMENTO MANCANTE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPONENTE_DOC_MANCANTE" 
                                HeaderText="COMPONENTE DOC. MANCANTE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE_DOC_MANC" HeaderText="NOTE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE COMPL.">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO_COR" HeaderText="TIPO IND."></asp:BoundColumn>
                            <asp:BoundColumn DataField="VIA_COR" HeaderText="IND.SPED."></asp:BoundColumn>
                            <asp:BoundColumn DataField="CIVICO_COR" HeaderText="CIVICO SPED."></asp:BoundColumn>
                            <asp:BoundColumn DataField="SCALA_COR" HeaderText="SCALA SPED."></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIANO_COR" HeaderText="PIANO SPED."></asp:BoundColumn>
                            <asp:BoundColumn DataField="LUOGO_COR" HeaderText="LUOGO SPED."></asp:BoundColumn>
                            <asp:BoundColumn DataField="CAP_COR" HeaderText="CAP_SPED"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
