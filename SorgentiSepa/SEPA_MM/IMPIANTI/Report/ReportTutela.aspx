<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportTutela.aspx.vb" Inherits="ReportTutela" %>

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
                <td style="height: 21px; width: 20%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COMPLESSO:</span></td>
                <td style="height: 21px; width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblComplesso" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px; width: 20%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">EDIFICIO:</span></td>
                <td style="height: 21px; width: 30%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblEdificio" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">DENOMINAZIONE:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblDenominazione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 20%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">COD. IMPIANTO:</span></td>
                <td style="width: 30%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblCodice" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">DETTAGLI</span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">DITTA DI GESTIONE:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblDittaGestione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">NUM. TELEFONICO:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblTel" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">RECINZIONE:</span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblRecinzione" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">TIPO DI RECINZIONE:</span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblTipoRecinzione" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">VIDEOSORVEGLIANZA:</span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblVideo" runat="server" Font-Bold="False" Font-Names="arial"
                        Font-Size="10pt" Style="z-index: 100; left: 0px; position: static; top: 0px"
                        Width="100%"></asp:Label></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">RESPONSABILE TRATTAMENTO:</span></td>
                <td style="width: 25%; height: 21px; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblResponsabile" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold; font-style: italic;">CANCELLI BOX DOTATI
                        DI:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt"></span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman'; height: 21px;">
                    <span style="font-size: 10pt; font-weight: bold;">CANCELLO CARRABILE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial; height: 21px;">
                    <asp:Label ID="lblCancelloCarrabile" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman'; height: 21px;" class="z">
                    <span style="font-size: 10pt"><strong>QUANTITA' CANCELLI CARRABILI:</strong></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial; height: 21px;">
                    <asp:Label ID="lblNumCancelliCarrabili" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">CANCELLO AUTOMATIZZATO:</span></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblCancelloAuto" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <strong>QUANTITA' CANCELLI AUTOMATIZZATI:</strong></td>
                <td style="height: 21px; width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblNumCancelliAuto" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; page-break-before: always; position: static;
            top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt"></span></strong>
                </td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">ELENCO CANCELLI</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridCA" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="31" Width="100%">
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
                <asp:TemplateColumn HeaderText="CARRABILE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRABILE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRABILE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="AUTOMATIZZATO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AUTOMATIZZATO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AUTOMATIZZATO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="MARCA" HeaderText="MARCA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="25%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MODELLO" HeaderText="MODELLO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="25%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DITTA_MANUTENZIONE" HeaderText="DITTA MANUTENZIONE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Width="30%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
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
        </asp:DataGrid><br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">ELENCO PASSI CARRABILI</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridPasso" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="31" Width="100%">
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
                <asp:BoundColumn DataField="NUM_LICENZA" HeaderText="NUM. LICENZA">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" Width="80%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_RILASCIO" HeaderText="DATA RILASCIO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
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
        </asp:DataGrid>&nbsp;<br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">ELENCO TUTELA ALLOGGI</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGridA" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="30" Width="100%">
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
                <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="SUB">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME_SUB") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME_SUB") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ANTINTRUSIONE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANTINTRUSIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANTINTRUSIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INSTALL. ANTINTRUSIONE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_ANTINTR") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_ANTINTR") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="RIMOZIONE ANTINTRUSIONE">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_ANTINTR") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_ANTINTR") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="BLINDATA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BLINDATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BLINDATA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INSTALL. BLINDATA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_BLINDATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_BLINDATA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="LASTRATURA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LASTRATURA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LASTRATURA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INSTALL. LASTRATURA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_LASTRATURA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_LASTRATURA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="RIMOZIONE LASTRATURA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_LASTRATURA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_LASTRATURA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ID_UNITA_IMMOBILIARI" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_IMMOBILIARI") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_IMMOBILIARI") %>'></asp:Label>
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
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">VERIFICHE PERIODICHE</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="47" Width="100%">
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
                    <HeaderStyle Width="0px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DITTA" HeaderText="DITTA">
                    <HeaderStyle Width="25%" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="DATA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="OSSERVAZIONI">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="30%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="15%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="VALIDITA' (mesi)">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCADENZA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="MESI_PREALLARME" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
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
                    <HeaderStyle Width="0px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid><br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 24px; text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">VERIFICHE ANNUALI CHIUSURA/INGRESSO SCALE</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="61" Width="100%">
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
                    <HeaderStyle Width="0px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DITTA" HeaderText="DITTA">
                    <HeaderStyle Width="25%" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="DATA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="MOTIVO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="30%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="15%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="VALIDITA' (mesi)">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SCADENZA">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ESITO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="MESI_PREALLARME" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="0px" />
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
                    <HeaderStyle Width="0px" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid><br />
        <br /><table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
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
