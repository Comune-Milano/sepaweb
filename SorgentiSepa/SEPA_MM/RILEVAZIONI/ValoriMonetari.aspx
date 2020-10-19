<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="ValoriMonetari.aspx.vb" Inherits="RILEVAZIONI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 182px;
        }
        .style3
        {
            width: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Gestione Valori Monetari
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" CssClass="bottone" Text="Salva" />
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 96%;">
        <tr>
            <td style="width: 80%;">
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Text="Rilevazione Stato di fatto:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbRilievo" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Style="margin-left: 0px" AutoPostBack="True" CausesValidation="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 80%;">
                <div id="Contenitore" style="overflow: auto;">
                    <asp:DataGrid ID="DataGridVoci" runat="server" AutoGenerateColumns="False" CssClass="styleDataGrid"
                        GridLines="None" Width="100%" AllowPaging="True" PageSize="100" CellPadding="0">
                        <ItemStyle CssClass="itemDataGrid" />
                        <PagerStyle CssClass="pagerDataGrid" Mode="NumericPages" />
                        <AlternatingItemStyle CssClass="alternateDataGrid" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_RILIEVO" HeaderText="ID_RILIEVO" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALORE" HeaderText="IMPORTO AL MQ" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PRIORITA' INTERVENTO">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="IMPORTO AL MQ">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtImportoCanone" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE") %>'
                                        Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtImportoCanone"
                                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        Style="left: 144px; top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia"
                                        ValidationExpression="^\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle CssClass="headerDataGrid" />
                        <FooterStyle CssClass="footerDatagrid" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 80%;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 80%;">
                <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr style="text-align: right">
            <td style="width: 80%;" valign="bottom" align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="LBLID" runat="server" Value="" />
    <asp:HiddenField ID="esiste" runat="server" />
    <asp:HiddenField ID="txtmodificato" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table align="right">
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="style3">
                &nbsp;
            </td>
            <td align="right">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
