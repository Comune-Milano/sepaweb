<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportRisultatoImpianti.aspx.vb" Inherits="ASS_ReportRisultato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Report</title>
<script language="javascript" type="text/javascript">
// <!CDATA[



// ]]>
</script>
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
                <td style="height: 21px">
                    <span style="font-size: 10pt"><strong>Settore Impianti</strong></span></td>
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
                        <asp:Label ID="lblTitolo" runat="server" Style="z-index: 100; left: 0px; position: static;
                            top: 0px; font-size: 11pt; font-family: Arial;" Text="Label" Width="100%" BorderStyle="Solid" BorderWidth="1px"></asp:Label></span></strong></td>
            </tr>
        </table>
        <br />
        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="1" Style="table-layout: auto; z-index: 101; left: 0px; clip: rect(auto auto auto auto);
            direction: ltr; position: relative; top: -6px; border-collapse: separate" Width="100%">
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
                Font-Underline="False" Wrap="False" /><Columns>
                    <asp:BoundColumn DataField="ID_IMPIANTO" HeaderText="ID_IMPIANTO" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COD_COMPLESSO" HeaderText="COD. COMPLESSO"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="DENOMINAZIONE COMPLESSO">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_COMPLESSO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_COMPLESSO") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="DENOMINAZIONE EDIFICIO">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_EDIFICIO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_EDIFICIO") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                    <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA'"></asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_IMPIANTO" HeaderText="TIPO IMPIANTO">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="COD_IMPIANTO" HeaderText="COD. IMPIANTO"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="DENOMINAZIONE IMPIANTO">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_IMPIANTO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_IMPIANTO") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
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
        </asp:DataGrid>
        <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="1" Style="table-layout: auto; z-index: 101; left: 0px; clip: rect(auto auto auto auto);
            direction: ltr; position: relative; top: -6px; border-collapse: separate" Width="100%">
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
                <asp:BoundColumn DataField="ID_IMPIANTO" HeaderText="ID_IMPIANTO" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_COMPLESSO" HeaderText="COD. COMPLESSO"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="DENOMINAZIONE COMPLESSO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_COMPLESSO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_COMPLESSO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="DENOMINAZIONE EDIFICIO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_EDIFICIO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_EDIFICIO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA'"></asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO_IMPIANTO" HeaderText="TIPO IMPIANTO">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_IMPIANTO" HeaderText="COD. IMPIANTO"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="DENOMINAZIONE IMPIANTO">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_IMPIANTO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE_IMPIANTO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="NUM_IMPIANTO" HeaderText="NUM. IMPIANTO"></asp:BoundColumn>
                <asp:BoundColumn DataField="MATRICOLA" HeaderText="NUM. MATRICOLA"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_LOTTO" HeaderText="NUM. LOTTO"></asp:BoundColumn>
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
        <table width="100%">
            <tr>
                <td style="text-align: center; height: 26px;" width="100%">
                    <strong><span style="font-size: 14pt">
                        <asp:Label ID="lblTotale" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="font-size: 11pt;
                            z-index: 100; left: 0px; width: 100%; font-family: Arial; position: static; top: 0px;
                            text-align: left" Text="Label" Width="736px"></asp:Label></span></strong></td>
            </tr>
        </table>
        <br />
    </form>
</body>
</html>
