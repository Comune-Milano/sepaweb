<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportManutenzioni.aspx.vb" Inherits="ReportManutenzioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <img src="../../../../IMG/logo.gif" style="z-index: 100; left: 0px; position: static; top: 0px" /></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 10pt"><strong>Settore Manutenzioni</strong></span></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 21px">
                    <span style="font-size: 10pt">Tel. 02/884.64435-36 Fax 02/884.66991</span></td>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                </td>
            </tr>
        </table>
    
    </div>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">
                        <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 0px; position: static;
                            top: 0px" Text="Label" Width="595px" BorderStyle="Solid" BorderWidth="1px"></asp:Label></span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <strong>ORDINE DI SERVIZIO:</strong></td>
                <td style="font-size: 10pt; width: 15%; font-family: Arial">
                    <asp:Label ID="lblODL" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px"></asp:Label></td>
                <td style="font-weight: bold; font-size: 10pt; width: 5%; font-family: 'Times New Roman'">
                    del:</td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <asp:Label ID="lblDataOrdine" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px"></asp:Label></td>
                <td style="font-size: 10pt; width: 40%; font-family: Arial">
                </td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">UBICAZIONE:</span></td>
                <td style="font-size: 10pt; width: 90%; font-family: Arial;">
                    <asp:Label ID="lblUbicazione" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                </td>
                <td style="font-size: 10pt; width: 90%; font-family: Arial">
                </td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">SERVIZIO:</span></td>
                <td style="font-size: 10pt; width: 80%;">
                    <asp:Label ID="lblServizio" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">DETTAGLIO SERVIZIO:</span></td>
                <td style="font-size: 10pt; width: 90%; font-family: Arial;">
                    <asp:Label ID="lblDettaglioVoce" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <span style="font-weight: bold; font-size: 10pt">LOTTO:</span></td>
                <td style="font-size: 10pt; width: 90%; font-family: Arial">
                    <asp:Label ID="lblLotto" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <strong>APPALTO:</strong></td>
                <td style="font-size: 10pt; width: 90%; font-family: Arial">
                    <asp:Label ID="lblAppalto" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <strong>FORNITORE:</strong></td>
                <td style="font-size: 10pt; width: 90%; font-family: Arial">
                    <asp:Label ID="lblFornitore" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px"></asp:Label></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">ELENCO INTERVENTI</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="18" Width="100%">
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
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <HeaderStyle Width="0%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA OGGETTO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="ID_IMPIANTO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_IMPIANTO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_IMPIANTO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ID_COMPLESSO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_COMPLESSO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_COMPLESSO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ID_EDIFICIO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_EDIFICIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_EDIFICIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ID_UNITA_IMMOBILIARE" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_IMMOBILIARE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_IMMOBILIARE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ID_UNITA_COMUNE" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_COMUNE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_COMUNE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0%" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="DETTAGLIO" HeaderText="DETTAGLIO OGGETTO">
                    <HeaderStyle Width="60%" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO_PRESUNTO" HeaderText="IMPORTO PRESUNTO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IMPORTO_CONSUNTIVO" HeaderText="IMPORTO CONSUNTIVO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                            Text="Annulla"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                            Text="Modifica">Seleziona</asp:LinkButton>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid>
    </form>
</body>
</html>
