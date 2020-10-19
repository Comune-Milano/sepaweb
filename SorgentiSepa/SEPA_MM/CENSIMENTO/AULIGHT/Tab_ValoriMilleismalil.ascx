<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_ValoriMilleismalil.ascx.vb" Inherits="CENSIMENTO_Tab_ValoriMilleismalil" %>
<style type="text/css">

    .style1
    {
        width: 440px;
    }
</style>
<table style="width: 645px; height: 95px">
    <tr>
        <td style="vertical-align: top; width: 80px; height: 81px; text-align: left">
            <div style="border-right: #ccccff solid; border-top: #ccccff solid; left: 0px; vertical-align: top;
                overflow: auto; border-left: #ccccff solid; width: 703px; border-bottom: #ccccff solid;
                top: 0px; height: 135px; text-align: left">
            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="Black" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                    Font-Underline="False" PageSize="15" Style="z-index: 105; left: 7px;
                top: 36px" Width="97%" GridLines="None">
                <PagerStyle Mode="NumericPages" />
                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="MediumBlue" />
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
                    <asp:BoundColumn DataField="VALORE_MILLESIMO" HeaderText="VALORE_MILLESIMO" Visible="False">
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="DESCRIZIONE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="VALORE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE_MILLESIMO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="Modifica">Seleziona</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                Text="Annulla"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid></div>
            </td>
        <td style="vertical-align: top; width: 40px; height: 81px; text-align: left">
                    <br />
            <br />
                    <br />
            <br />
            </td>
        <td style="vertical-align: top; width: 24px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" />
            <asp:HiddenField ID="HFtxtId" runat="server" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
            <br />
            </td>
    </tr>
</table>
<asp:HiddenField ID="TextBox2" runat="server" Value="0" />

    


                        
                        


