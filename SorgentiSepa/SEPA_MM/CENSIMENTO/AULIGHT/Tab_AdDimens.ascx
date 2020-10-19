<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_AdDimens.ascx.vb" Inherits="CENSIMENTO_Tab_AdDimens" %>
<table style="width: 645px; height: 95px">
    <tr>
        <td style="width: 80px; vertical-align: top; height: 81px; text-align: left;">
            <div 
                
                
                style="left: 0px; vertical-align: top; overflow: auto; width: 703px; top: 0px;
                height: 135px; text-align: left; border-right: #ccccff solid; border-top: #ccccff solid; border-left: #ccccff solid; border-bottom: #ccccff solid;">
                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="None" PageSize="1" Style="z-index: 105; left: 8px; top: 32px" 
                    Width="98%" AllowSorting="True">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="TIPOLOGIA">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="VALORE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Modifica">Seleziona</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                    Text="Annulla"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID_DIMENSIONI" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
            </td>
        <td style="vertical-align: top; width: 38px; text-align: left; height: 81px;">
            &nbsp;<br />
            <br />
            &nbsp;</td>
        <td style="vertical-align: top; width: 140px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" />
            <asp:HiddenField ID="HFtxtId" runat="server" />
        </td>
    </tr>
</table>