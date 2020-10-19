<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_UtMillesimali.ascx.vb" Inherits="CENSIMENTO_Tab_Millesimali" %>
<table>
    <tr>
        <td style="vertical-align: top; width: 589px; height: 81px; text-align: left" colspan ="3">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 703px; top: 0px; height: 135px; text-align: left">
        <asp:DataGrid ID="DatGridUtenzaMillesim" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" EnableTheming="True" Font-Bold="False" Font-Italic="False"
            Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
            Font-Underline="False" ForeColor="Black" PageSize="5" Style="z-index: 101;
            left: 8px; top: 32px" Width="98%">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Mode="NumericPages" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CONTRATTO" HeaderText="CONTRATTO" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="###" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Font-Names="Wingdings" Text="o"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="FORNITORE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FORNITORE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FORNITORE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CONTATORE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTATORE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CONTRATTO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONTRATTO") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid></div>
            </td>
        <td style="vertical-align: top; width: 2378px; height: 81px; text-align: left">
            &nbsp;<asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png"
                Style="z-index: 103; left: 495px; top: 38px" TabIndex="1" ToolTip="Aggiungi elemento alla lista" /><br />
            <br />
            &nbsp;<asp:ImageButton ID="BtnElimina" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png"
                Style="z-index: 103; left: 495px; top: 69px" TabIndex="2" ToolTip="Elimina elemento selezionato dalla lista" /></td>
        <td style="vertical-align: top; width: 140px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" />
            <asp:HiddenField ID="HFtxtId" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; width: 589px; text-align: left; height: 22px;">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" ForeColor="Black" MaxLength="100" Style="left: 13px;
                top: 197px" Width="352px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox></td>
        <td style="vertical-align: top; width: 587px; text-align: left; height: 22px;">
<asp:ImageButton
                ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
                Style="z-index: 102; left: 440px; top: 141px; height: 12px;" TabIndex="3" 
                ToolTip="Visualizza" CausesValidation="False" /></td>
        <td style="vertical-align: top; width: 804px; height: 22px; text-align: left">
<asp:ImageButton
                    ID="ImButEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" Style="left: 527px;
                    top: 141px;" TabIndex="4" ToolTip="Esci" Visible="False" /></td>
    </tr>
</table>

