<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Manutenzioni.ascx.vb" Inherits="Tab_Manutenzioni" %>
<table id="TABBLE_LISTA" style="width: 765px; height: 310px;">
    <tr>
        <td>
            <br />
            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="INTERVENTI DI MANUTENZIONE"
                Width="368px"></asp:Label></td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 750px; border-bottom: #0000cc thin solid;
                height: 250px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="63" Width="1500px">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_SERVIZIO" HeaderText="TIPO SERVIZIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_INTERVENTO" HeaderText="TIPO INTERVENTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ARTICOLO" HeaderText="ARTICOLO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_INIZIO_INTERVENTO" HeaderText="DATA INIZ. INTERV.">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_FINE_INTERVENTO" HeaderText="DATA FINE INTERV."></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_DOCUMENTO" HeaderText="NUM. DOCUMENTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_FATTURA" HeaderText="NUM. FATTURA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COSTO" HeaderText="COSTO TOTALE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="REVERSIBILE" HeaderText="REVERSIBILITA'"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COSTO_REVERSIBILE" HeaderText="NELLA QUOTA"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Modifica">Seleziona</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                    Text="Annulla"></asp:LinkButton>
                            </EditItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
