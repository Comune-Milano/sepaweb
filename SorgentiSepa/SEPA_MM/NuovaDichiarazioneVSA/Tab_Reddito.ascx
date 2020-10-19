<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Reddito.ascx.vb"
    Inherits="ANAUT_Tab_Reddito" %>
<table width="97%" style="border: 1px solid #0066FF;">
    <tr>
        <td style="padding-left: 15px;">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">REDDITI DEL NUCLEO FAMILIARE</asp:Label>
            &nbsp
            <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Numero di FIGLI/MINORI fiscalmente a carico presenti nel nucleo familiare</asp:Label>
            &nbsp
            <asp:TextBox ID="txtMinori" runat="server" Font-Names="Arial" Font-Size="8pt" Width="38px">0</asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="vertical-align: top;">
                        <div style="overflow: auto; width: 100%; height: 70px;" id="elencoComp">
                            <asp:DataGrid ID="DataGridRedditi" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" Width="100%" CellPadding="1" PageSize="5">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                <HeaderStyle BackColor="#CCFFCC" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
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
                                    <asp:BoundColumn DataField="IDREDD" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DIPENDENTE" HeaderText="DIPENDENTE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PENSIONE" HeaderText="PENSIONE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PENSIONE2" HeaderText="PENSIONE ESENTE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AUTONOMO1" HeaderText="AUTONOMO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NO_ISEE" HeaderText="ASSEGN. MANT. FIGLI E ONERI" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TOT_REDDITI" HeaderText="TOT. REDDITI" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imgAggRedd" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Reddito"
                                        Style="width: 16px; cursor: pointer;" onclick="ApriDettReddito();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="ImgModificaRedd" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                        ToolTip="Modifica Reddito" Style="width: 16px; cursor: pointer;" onclick="ModificaDettReddito();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnDeleteRedd" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Reddito" Style="width: 16px;"/>
                                    <asp:Button ID="btnCancella" runat="server" Text="Button" Style="display: none;" />
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
                Font-Size="9pt">DETRAZIONI</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="width: 97%; vertical-align: top;">
                        <div style="overflow: auto; height: 70px;" id="Div1">
                            <asp:DataGrid ID="DataGridDetraz" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" PageSize="5" Width="100%" BorderStyle="Solid"
                                BorderWidth="1px" CellPadding="1">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                <HeaderStyle BackColor="#FFDFBF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
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
                                    <asp:BoundColumn DataField="IDDETR" HeaderText="ID" ReadOnly="True" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="TIPO DETRAZIONE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TOT_DETRAZ" HeaderText="TOT. DETRAZ." HeaderStyle-HorizontalAlign="Center">
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
                                    <asp:Image ID="imgAggDetr" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Detrazione"
                                        Style="width: 16px; cursor: pointer;" onclick="AggiungiDetrazioni();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Image ID="imgModRedd" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                        ToolTip="Modifica Detrazione" Style="width: 16px; cursor: pointer;" onclick="ModificaDetrazioni();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgDeleteRedd" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Detrazione" Style="width: 16px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:HiddenField ID="idCompReddito" runat="server" />
<asp:HiddenField ID="idReddito" runat="server" />
<asp:HiddenField ID="idCompDetraz" runat="server" />
<asp:HiddenField ID="idDetraz" runat="server" />
<asp:HiddenField ID="importo" runat="server" />
<asp:HiddenField ID="tipoDetraz" runat="server" />
<script type="text/javascript">

    //    function Verifica() {

    //        SceltaFunzioneOP1('Sicuri di voler eliminare?');

    //    }

    //    function SceltaFunzioneOP1(TestoMessaggio) {
    //        $(document).ready(function () {
    //            $('#ScriptScelta').text(TestoMessaggio);
    //            $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnCancella', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
    //        });
    //    }

</script>
