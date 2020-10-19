<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Nucleo.ascx.vb" Inherits="ANAUT_Tab_Nucleo" %>
<table width="97%" style="border: 1px solid #0066FF; vertical-align: top;">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">COMPONENTI DEL NUCLEO - Richiedente, componenti e altri soggetti considerati a carico IRPEF</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="vertical-align: top;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;" id="elencoComp">
                            <asp:DataGrid ID="DataGridComponenti" runat="server" AutoGenerateColumns="False"
                                Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Width="100%" CellPadding="1"
                                PageSize="5">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" Visible="False" />
                                <HeaderStyle BackColor="#FFFFB3" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#800000" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_NASC" HeaderText="DATA NASC." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="GR. PARENTELA" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PERC_INVAL" HeaderText="% INVAL." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="USL" HeaderText="ASL" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_INVALIDITA" HeaderText="TIPO INVAL." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NATURA_INVAL" HeaderText="NATURA INVAL." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IND_ACCOMP" HeaderText="IND. ACCOMP." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUOVO_COMP" HeaderText="NUOVO COMP." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_INGRESSO" HeaderText="DATA INGR." HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PROGR" HeaderText="PROGR" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imgAggComp" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Componente"
                                        Style="width: 16px; cursor: pointer;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="ImgModifica" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                        ToolTip="Modifica Componente" Style="width: 16px; cursor: pointer;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="btnDelete" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Componente" Style="width: 16px; cursor: pointer;"  />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-left:15px;">
            <asp:Label ID="lblEliminati" runat="server" CssClass="CssLabel" Font-Bold="True"
                Font-Names="Arial" Font-Size="8pt" Height="18px"></asp:Label>
        </td>
    </tr>
    <tr><td>&nbsp</td></tr>
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">SPESE DOCUMENTATE SUPERIORI A 10.000 Euro (solo componenti con indennità accompagnamento)</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="width: 97%; vertical-align: top;">
                        <div style="overflow: auto; height: 70px;" id="Div1">
                            <asp:DataGrid ID="DataGridSpese" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" PageSize="5" Width="100%" BorderStyle="Solid"
                                BorderWidth="1px" CellPadding="1">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" Visible="False" />
                                <HeaderStyle BackColor="#CCFFFF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#800000" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" HeaderStyle-HorizontalAlign="Center">
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
                                    <asp:Image ID="ImgModifySpese" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                        ToolTip="Modifica Spese" Style="width: 16px; cursor: pointer;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div>
    <asp:HiddenField ID="sola_lettura" runat="server" Value="0" />
    <asp:HiddenField ID="txtProgr" runat="server" />
    <asp:HiddenField ID="cognome" runat="server" />
    <asp:HiddenField ID="nome" runat="server" />
    <asp:HiddenField ID="data_nasc" runat="server" />
    <asp:HiddenField ID="cod_fiscale" runat="server" />
    <asp:HiddenField ID="parentela" runat="server" />
    <asp:HiddenField ID="perc_inval" runat="server" />
    <asp:HiddenField ID="asl" runat="server" />
    <asp:HiddenField ID="tipo_inval" runat="server" />
    <asp:HiddenField ID="natura_inval" runat="server" />
    <asp:HiddenField ID="ind_accomp" runat="server" />
    <asp:HiddenField ID="idCompSpese" runat="server" />
    <asp:HiddenField ID="componente" runat="server" />
    <asp:HiddenField ID="importo" runat="server" />
    <asp:HiddenField ID="descrizione" runat="server" />
    <asp:HiddenField ID="nuovoComp" runat="server" />
    <asp:HiddenField ID="dataIngr" runat="server" />
</div>
