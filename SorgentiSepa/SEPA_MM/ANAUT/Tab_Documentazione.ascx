<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Documentazione.ascx.vb"
    Inherits="ANAUT_Tab_Documentazione" %>
<table width="97%">
    <tr>
        <td style="padding-left: 15px; width: 120px;">
            <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Names="Arial" Font-Size="9pt"
                Font-Bold="False">Data presentazione</asp:Label>
        </td>
        <td style="width: 250px;">
            <asp:TextBox ID="txtDataPresentaz" runat="server"></asp:TextBox>&nbsp
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataPresentaz"
                Display="Dynamic" ErrorMessage="Errore!" Font-Bold="True" Font-Names="arial"
                Font-Size="8pt" Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
            </asp:RegularExpressionValidator>
        </td>
        <td style="text-align: center;">
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">DOCUMENTAZIONE MANCANTE</asp:Label>
        </td>
    </tr>
</table>
<table width="97%">
    <tr>
        <td colspan="3">
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="border: 1px solid #0066FF; vertical-align: top; width: 100%;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;">
                            <asp:DataGrid ID="DataGridDocum" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" PageSize="5" Width="100%" BorderStyle="Solid"
                                BorderWidth="1px" CellPadding="1">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                <HeaderStyle BackColor="#ECFFB3" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
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
                                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="TIPO DOCUMENTO" HeaderStyle-HorizontalAlign="Center">
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
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Image ID="imgAggDoc" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png" ToolTip="Aggiungi Documento"
                                        Style="width: 16px; cursor: pointer;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnDeleteDoc" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Documento" Style="width: 16px;" OnClientClick="document.getElementById('provenienza').value='doc';VerificaSelezionatoDocumenti();return false;" />
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
    <tr>
        <td colspan="3" style="padding-left: 15px; text-align: center;">
            <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
                Font-Size="9pt">DOCUMENTAZIONE PRESENTATA</asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table style="margin-left: 10px; width: 99%; height: 20px;">
                <tr>
                    <td style="border: 1px solid #0066FF; vertical-align: top; width: 100%;">
                        <div style="overflow-x: hidden; overflow-y: auto; width: 100%; height: 70px;">
                            <asp:DataGrid ID="DataGridPresenti" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                                Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" PageSize="5" Width="100%" BorderStyle="Solid"
                                BorderWidth="1px" CellPadding="1">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                <HeaderStyle BackColor="#ECFFB3" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
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
                                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="TIPO DOCUMENTO" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_PRESENTAZIONE" HeaderText="DATA PRESENTAZIONE">
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgEliminaDocPresentato" runat="server" ImageUrl="~/ANAUT/img/ImgDelete.png"
                                        ToolTip="Elimina Documento Presentato" Style="width: 16px;" OnClientClick="document.getElementById('provenienza').value='docP';VerificaSelezionatoDocumentiP();return false;" />
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
<asp:Button ID="btnAggiungiDoc" runat="server" Text="Aggiungi documentazione" Style="visibility: hidden" />
<asp:HiddenField ID="idDoc" runat="server" Value="-1" />
<asp:HiddenField ID="idDocP" runat="server" Value="-1" />
<asp:HiddenField ID="descrizione" runat="server" Value="-1" />
<script type="text/javascript" language="javascript">
    function VerificaSelezionatoDocumenti() {
        if (document.getElementById('dic_Documenti1_idDoc').value == '-1' || document.getElementById('dic_Documenti1_idDoc').value == '0') {
            Avviso();
        }
        else {
            Verifica();
        }
    }
    function VerificaSelezionatoDocumentiP() {
        if (document.getElementById('dic_Documenti1_idDocP').value == '-1' || document.getElementById('dic_Documenti1_idDocP').value == '0') {
            Avviso();
        }
        else {
            Verifica();
        }
    }
</script>
