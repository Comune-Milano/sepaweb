<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_ConsUnitaComuni.ascx.vb" Inherits="CENSIMENTO_Tab_ConsUnitaComuni" %>
<style type="text/css">
    .style1
    {
        width: 88px;
    }
    .style2
    {
        width: 270px;
    }
    .style3
    {
        width: 106px;
    }
</style>
<table style="width: 645px">
    <tr>
        <td class="style1">
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 107; left: 24px;  top: 128px" ForeColor="Black">UBICAZIONE</asp:Label>
        </td>
        <td class="style2">
        <asp:DropDownList ID="cmbUbicazione" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 108; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 128px" Width="236px">
        </asp:DropDownList>
        </td>
        <td class="style3">
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 107; left: 24px;  top: 128px" ForeColor="Black">DEST. USO</asp:Label>
        </td>
        <td>
        <asp:DropDownList ID="cmbUbicazione2" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 108; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 128px" Width="223px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style1">
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 107; left: 24px;  top: 128px" ForeColor="Black">TIPOLOGIA</asp:Label>
        </td>
        <td class="style2">
        <asp:DropDownList ID="cmbUbicazione0" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 108; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 128px" Width="235px">
        </asp:DropDownList>
        </td>
        <td class="style3">
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 107; left: 24px;  top: 128px" ForeColor="Black">STATO FISICO</asp:Label>
        </td>
        <td>
        <asp:DropDownList ID="cmbUbicazione1" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 108; left: 104px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 128px" Width="223px">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan = 4 >
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="10pt"
            ForeColor="Black" 
                Style="z-index: 114; left: 8px;top: 208px">CARATTERISTICHE E DOTAZIONI SINGOLA UNITA' COMUNE</asp:Label>
        </td>
        <td >
           </td>
        <td >
            </td>
        <td>
           </td>
    </tr>
    <tr>
        <td colspan = 4 >
            <div style="border-right: #ccccff solid; border-top: #ccccff solid; left: 0px; vertical-align: top;
                overflow: auto; border-left: #ccccff solid; width: 586px; border-bottom: #ccccff solid;
                top: 0px; height: 123px; text-align: left">
                <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="Black" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="None" Style="z-index: 105; left: 8px; vertical-align: top; top: 32px;
                    text-align: left" Width="563px">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="MediumBlue" HorizontalAlign="Left" />
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
                        <asp:TemplateColumn HeaderText="DESCRIZIONE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DETTAGLI">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE_TABELLA") %>'></asp:Label>
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
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
            </td>
        <td >
            </td>
        <td >
            </td>
        <td>
            </td>
    </tr>
    <tr>
        <td colspan = 4>
            </td>
        <td >
            </td>
        <td >
            </td>
        <td>
            &nbsp;</td>
    </tr>
</table>
