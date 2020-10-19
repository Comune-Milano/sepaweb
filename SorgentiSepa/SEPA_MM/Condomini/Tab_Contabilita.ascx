<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Contabilita.ascx.vb"
    Inherits="Condomini_Tab_Contabilita" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
<table style="width: 90%;">
    <tr>
        <td style="vertical-align: top; width: 100%; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                width: 703px; top: 0px; height: 260px; text-align: left">
                <asp:DataGrid ID="DataGridGestione" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="691px">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PERIODO" HeaderText="PERIODO" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="STATO BILANCIO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO_BILANCIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO_BILANCIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA INIZIO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INIZIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA FINE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_FINE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
            <asp:HiddenField ID="txtidGest" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
        </td>
        <td style="vertical-align: top; text-align: left;">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Image ID="imgAddConv" runat="server" onclick="myOpacityCondomini.toggle();"
                            ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png" ToolTip="Aggiungi un Preventivo"
                            Style="width: 18px; cursor: pointer;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" ToolTip="Modifica" CausesValidation="False"
                            OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                            Style="z-index: 102; left: 392px; top: 387px; height: 18px;" OnClientClick="document.getElementById('splash').style.visibility = 'visible';DeleteConfirmCont()"
                            ToolTip="Elimina Elemento Selezionato" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="imgPrintReport" runat="server" onclick="ApriRptCont();" ImageUrl="~/Condomini/Immagini/print-icon.png"
                            ToolTip="Stampa Contabilità" Style="cursor: hand; width: 16px;" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="572px">Nessuna Selezione</asp:TextBox>
        </td>
        <td style="vertical-align: top; text-align: left;">
            &nbsp;
        </td>
    </tr>
</table>
<div id="AnnoGest" style="border: thin solid #6699ff; position: absolute; z-index: 100;
    top: 290px; left: 469px; width: 265px; height: 75px; visibility: visible; background-color: #C0C0C0;">
    <table width="88%">
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                    Text="TIPOLOGIA"></asp:Label>
            </td>
            <td>
                            <asp:DropDownList ID="cmbTipoGest" runat="server" Style="top: 109px; left: 9px; right: 481px;"
                                Font-Names="Arial" Font-Size="9pt" TabIndex="1" Width="150px" 
                    BackColor="White">
                                <asp:ListItem Value="O">ORDINARIA</asp:ListItem>
                                <asp:ListItem Value="S">STRAORDINARIA</asp:ListItem>
                            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Anno" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                    Text="ANNO"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAnnoInizio" runat="server" Width="60px" BackColor="White" TabIndex="2"
                    MaxLength="4" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td style="text-align: right">
                <asp:ImageButton ID="btnConferma" runat="server" ImageUrl="~/Condomini/Immagini/Conferma.png"
                    Style="z-index: 102; left: 392px; top: 387px" ToolTip="Conferma Anno" CausesValidation="False"
                    OnClientClick="myOpacityCondomini.toggle();" TabIndex="2" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        myOpacityCondomini = new fx.Opacity('AnnoGest', { duration: 200 });
        myOpacityCondomini.hide();

        function DeleteConfirmCont() {
            if (document.getElementById('Tab_Contabilita1_txtidGest').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('Tab_Contabilita1_txtConfElimina').value = '0';

                }
                else {
                    document.getElementById('Tab_Contabilita1_txtConfElimina').value = '1';

                }
            }
        }
    </script>
</div>
