<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportCitofonico.aspx.vb" Inherits="ReportCitofonico" %>

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
                    <img src="../../IMG/logo.gif" style="z-index: 100; left: 0px; position: static; top: 0px" /></td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 10pt"><strong>Settore Impianti</strong></span></td>
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
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">COMPLESSO:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <asp:Label ID="lblComplesso" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-weight: bold; font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">EDIFICIO:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <asp:Label ID="lblEdificio" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">DENOMINAZIONE:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <asp:Label ID="lblDenominazione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">COD. IMPIANTO:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <asp:Label ID="lblCodice" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                </td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial">
                </td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                </td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial">
                </td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    DITTA INSTALLATRICE:</td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial">
                    <asp:Label ID="lblDittaInstallatrice" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%" Font-Bold="False"></asp:Label></td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    NUM. TELEFONICO:</td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial">
                    <asp:Label ID="lblTelInstalla" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt" Font-Bold="False"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'; height: 18px">
                    DITTA DI GESTIONE:</td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial; height: 18px">
                    <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'; height: 18px">
                    NUM. TELEFONICO:</td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial; height: 18px">
                    <asp:Label ID="lblTelGestione" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
            </tr>
        </table>
        &nbsp;<table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">DETTAGLI</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridCI" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
            clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="27" Width="100%">
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
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                <asp:BoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="TASTIERA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TASTIERA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TASTIERA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="DISTRIBUZIONE" HeaderText="DISTRIBUZIONE"></asp:BoundColumn>
                <asp:BoundColumn DataField="ID_TIPO_DISTRIBUZIONE" HeaderText="ID_TIPO_DISTRIBUZIONE"
                    Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="QUANTITA'">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCALE SERVITE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALE_SERVITE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALE_SERVITE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
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
        </asp:DataGrid><br />
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">NOTE</span></strong></td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td style="height: 21px; width: 1000%;">
                    <span style="font-size: 10pt">
                    <asp:Label ID="lblNote" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></span></td>
            </tr>
        </table>
        <br />
        <br />
    </form>
</body>
</html>
