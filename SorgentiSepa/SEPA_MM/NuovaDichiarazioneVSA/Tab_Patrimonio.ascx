<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Patrimonio.ascx.vb"
    Inherits="ANAUT_Tab_Patrimonio" %>
<table width="97%" style="border: 1px solid #0066FF; vertical-align: top;">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">PATRIMONIO MOBILIARE</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%;">
                <tr>
                    <td style="width: 97%; vertical-align: top;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;" id="div0">
                            <asp:DataGrid ID="DataGridMob" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" Width="100%" CellPadding="1" PageSize="5">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                <HeaderStyle BackColor="#AED7FF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#800000" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDCOMP" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDMOB" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_MOB" HeaderText="TIPOLOGIA" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_INTERMEDIARIO" HeaderText="CODICE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="INTERMEDIARIO" HeaderText="INTERMEDIARIO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imgAggMob" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Patrimonio Mobiliare"
                                        Style="width: 16px; cursor: pointer;" onclick="AggiungiPatrimMob();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="ImgModifMob" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                        ToolTip="Modifica Patrimonio Mobiliare" Style="width: 16px; cursor: pointer;"
                                        onclick="ModificaPatrimMob();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnDeleteMob" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Patrimonio Mobiliare" Style="width: 16px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">PATRIMONIO IMMOBILIARE</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="width: 97%; vertical-align: top;">
                        <div style="overflow: auto; height: 70px;" id="Div1">
                            <asp:DataGrid ID="DataGridImmob" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" Width="100%" CellPadding="1" PageSize="5">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                <HeaderStyle BackColor="#CCFF99" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#800000" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDCOMP" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IDIMMOB" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_IMMOB" HeaderText="TIPO IMMOB." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_PROPR" HeaderText="TIPO PROPR." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="False"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="VALORE" HeaderText="VALORE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MUTUO" HeaderText="MUTUO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUP_UTILE" HeaderText="SUPERFICIE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PERC_PATR_IMMOBILIARE" HeaderText="% PROPRIETA" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="N_VANI" HeaderText="VANI" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="CAT_CATASTALE" HeaderText="CAT.CATASTALE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="REND_CATAST_DOMINICALE" HeaderText="ID" ReadOnly="True"
                                        Visible="False"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imgAggImm" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Immobile"
                                        Style="width: 16px; cursor: pointer;" onclick="AggiungiPatrimonioI();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="imgModImmob" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                        ToolTip="Modifica Immobile" Style="width: 16px; cursor: pointer;" onclick="ModificaPatrimonioI();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgDeleteImmob" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Immobile" Style="width: 16px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:HiddenField ID="idCompMob" runat="server" Value="0" />
<asp:HiddenField ID="idCompImmob" runat="server" Value="0" />
<asp:HiddenField ID="abi" runat="server" />
<asp:HiddenField ID="inter" runat="server" />
<asp:HiddenField ID="tipo" runat="server" />
<asp:HiddenField ID="importo" runat="server" />
<asp:HiddenField ID="idPatrMob" runat="server" Value="0" />
<asp:HiddenField ID="idPatrImmob" runat="server" Value="0" />
<asp:HiddenField ID="tipoImmob" runat="server" />
<asp:HiddenField ID="tipoPropr" runat="server" />
<asp:HiddenField ID="perc" runat="server" />
<asp:HiddenField ID="valore" runat="server" />
<asp:HiddenField ID="mutuo" runat="server" />
<asp:HiddenField ID="catastale" runat="server" />
<asp:HiddenField ID="comune" runat="server" />
<asp:HiddenField ID="vani" runat="server" />
<asp:HiddenField ID="sup" runat="server" />
<asp:HiddenField ID="pie" runat="server" />
<asp:HiddenField ID="indirizzo" runat="server" />
<asp:HiddenField ID="civico" runat="server" />
<asp:HiddenField ID="rendita" runat="server" />
