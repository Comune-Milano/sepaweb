<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Sollevamento_Inquilino.ascx.vb" Inherits="Tab_Sollevamento_Inquilino" %>
<table>
    <tr>
        <td>
<table style="width: 675px">
    <tr>
        <td style="width: 493px">
            &nbsp;<asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="UTILIZZO ESCLUSIVO DEL MONTASCALE (Elenco Inquilini)"
                Width="520px" Height="20px"></asp:Label></td>
    </tr>
</table>
            <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                overflow: auto; border-left: navy 1px solid; width: 765px; border-bottom: navy 1px solid;
                height: 270px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="24" Width="736px" AutoGenerateColumns="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                    <AlternatingItemStyle BackColor="#FAFFFF" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="INTESTATARIO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server"  />
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="75%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="SCALA" HeaderText="SCALA">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="5%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PIANO" HeaderText="PIANO">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="15%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Width="5%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
        </td>
    </tr>
</table>