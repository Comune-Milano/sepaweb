<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportMeteoriche.aspx.vb" Inherits="ReportMeteoriche" %>

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
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">DITTA INSTALLATRICE:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <asp:Label ID="lblDitta" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman';">
                    <span style="font-weight: bold; font-size: 10pt">DATA INSTALLAZIONE:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial;">
                    <asp:Label ID="lblData" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <span style="font-weight: bold; font-size: 10pt">ELEMENTI SERVITI:</span></td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial">
                    <asp:CheckBoxList ID="CheckBoxScale" runat="server" BorderColor="Black" Enabled="False"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="96px" TabIndex="4"
                        Width="272px">
                    </asp:CheckBoxList></td>
                <td style="font-size: 10pt; width: 20%; font-family: 'Times New Roman'">
                    <span style="font-weight: bold; font-size: 10pt"></span>
                </td>
                <td style="font-size: 10pt; width: 30%; font-family: Arial">
                </td>
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
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">DITTA DI GESTIONE:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblDittaGestione" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;">NUM. TELEFONICO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblTel" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Width="100%" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr style="font-weight: bold">
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">QUADRO ELETTRICO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:CheckBox ID="chkQuadro_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkQuadro_NO" runat="server" Font-Bold="False" Text="NO" TextAlign="Left" /></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';">
                    <span style="font-size: 10pt; font-weight: bold;">UBICAZIONE QUADRO:</span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    <asp:Label ID="lblUbicazioneQuadro" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
                <td style="width: 25%; font-size: 10pt; font-family: 'Times New Roman';" class="z">
                    <span style="font-size: 10pt; font-weight: bold;"></span></td>
                <td style="width: 25%; font-size: 10pt; font-family: Arial;">
                    </td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    <strong>VASCA RACCOLTA ACQUE:</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:CheckBox ID="chkVasca_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkVasca_NO" runat="server" Font-Bold="False" Text="NO" TextAlign="Left" /></td>
                <td class="z" style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    <strong>DISOLEATORE:</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:CheckBox ID="chkDisoleatore_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkDisoleatore_NO" runat="server" Font-Bold="False" Text="NO" TextAlign="Left" /></td>
            </tr>
            <tr>
                <td style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    <strong>IMPIANTO CONTINUITA'</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:CheckBox ID="chkContinuita_SI" runat="server" Font-Bold="False" Text="SI" TextAlign="Left" />
                    &nbsp;
                    <asp:CheckBox ID="chkContinuita_NO" runat="server" Font-Bold="False" Text="NO" TextAlign="Left" /></td>
                <td class="z" style="font-size: 10pt; width: 25%; font-family: 'Times New Roman'">
                    <strong>DURATA (H):</strong></td>
                <td style="font-size: 10pt; width: 25%; font-family: Arial">
                    <asp:Label ID="lblDurata" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="100%"></asp:Label></td>
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
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">ELENCO POMPE DI SOLLEVAMENTO</span></strong></td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
            TabIndex="20" Width="100%">
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
                <asp:BoundColumn DataField="MODELLO" HeaderText="MARCA/MODELLO"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="MATRICOLA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="POTENZA (KW)">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="PORTATA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PORTATA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PORTATA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="PREVALENZA">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENZA") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENZA") %>'></asp:Label>
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
