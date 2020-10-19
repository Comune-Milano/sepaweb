<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CorrelazioniUtenza.aspx.vb" Inherits="CorrelazioniUtenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SEPA@Web - Correlazioni</title>
</head>
<body>
    <form id="form1" runat="server">
    <div title="Correlazioni">
        <table id="Table1" align="center" border="1" cellpadding="1" cellspacing="1" style="z-index: 100;
            left: 0px; position: absolute; top: 0px" width="95%">
            <tr>
                <td align="middle" bgcolor="#ffffff" style="text-align: left" valign="center">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Navy" Width="95%">Elenco Correlazioni trovate</asp:Label></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="#ffffff" style="text-align: center" valign="center">
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Width="348px" PageSize="5">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False"></asp:BoundColumn>
                            <asp:EditCommandColumn CancelText="Annulla" EditText="Dettagli" HeaderText="Nucleo"
                                UpdateText="Aggiorna"></asp:EditCommandColumn>
                            <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA PR.">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="#eceff2" style="text-align: left" valign="center">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Navy" Width="378px">Composizione Nucleo</asp:Label></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="white" style="text-align: center" valign="center">
                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Width="593px">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="COGNOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COD. FISCALE">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA NASCITA">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_NASCITA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_NASCITA") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="GR. PARENTELA">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENTELA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARENTELA") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td align="middle" bgcolor="aliceblue" style="text-align: center" valign="center">
                    <asp:Button ID="btnStampa" runat="server" Height="32px" Text="Stampa" Width="70px" />
                    &nbsp; &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btnVisualizza" runat="server" Height="32px" Text="Chiudi" Width="70px" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

