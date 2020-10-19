<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabAmministratori.ascx.vb" Inherits="Condomini_TabAmministratori" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
    <style type="text/css">
        .styleMilles
        {
            font-family: Arial;
            font-size: 8pt;
            vertical-align:top;
            text-align :left;
        }
    </style>
<table style="width: 62%; height: 95px">
    <tr>
        <td style="vertical-align: top; width: 80px; height: 81px; text-align: left" colspan="0" rowspan="0">
            <table style="width: 100%" cellpadding="1" cellspacing="0">
                <tr>
                    <td 
                        class="styleMilles">
                        Cognome</td>
                    <td class="styleMilles">
                        Nome</td>
                    <td style="width: 172px">
                        </td>
                    <td style="width: 283px">
                        </td>
                </tr>
                <tr>
                    <td style="width: 178px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; font-size: 8pt;" TabIndex="1" Width="172px" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 187px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="30" Style="z-index: 107; right: 628px; left: 180px;
                            top: 72px; font-size: 8pt;" TabIndex="2" Width="174px" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 172px; vertical-align: top; text-align: left;">
                        </td>
                    <td style="width: 283px; vertical-align: top; text-align: left;">
                        </td>
                </tr>
                <tr>
                    <td class="styleMilles">
                        Codice Fiscale</td>
                    <td 
                        class="styleMilles">
                        P.IVA</td>
                    <td 
                        class="styleMilles">
                        Titolo</td>
                    <td style="vertical-align: top; width: 283px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 178px; text-align: left">
                        <asp:TextBox ID="txtCF" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="16" ReadOnly="True" Style="z-index: 107; right: 628px;
                            left: 351px; top: 72px; font-size: 8pt;" TabIndex="3" Width="170px"></asp:TextBox></td>
                    <td style="vertical-align: top; width: 187px; text-align: left">
                        <asp:TextBox ID="txtpiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="16" ReadOnly="True" Style="z-index: 107; right: 628px;
                            left: 351px; top: 72px; font-size: 8pt;" TabIndex="3" Width="170px"></asp:TextBox></td>
                    <td style="vertical-align: top; width: 172px; text-align: left" colspan="2">
                        <asp:TextBox ID="TxtTitolo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 107; right: 628px;
                            left: 180px; top: 72px; font-size: 8pt;" TabIndex="2" Width="200px" 
                            ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td 
                        class="styleMilles">
                        Indirizzo</td>
                    <td style="width: 187px; vertical-align: top; text-align: left;">
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 10px; top: 104px" Width="73px">Civico                              </asp:Label><asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 10px; top: 104px" Width="55px">Cap</asp:Label><asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 10px; top: 104px" Width="31px">Prov.</asp:Label></td>
                    <td 
                        class="styleMilles">
                        Comune</td>
                    <td 
                        class="styleMilles">
                        Telefono</td>
                </tr>
                <tr>
                    <td style="width: 178px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtIndirizzo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; font-size: 8pt;" TabIndex="1" Width="172px" ReadOnly="True"></asp:TextBox></td>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr>
                                <td>
                        <asp:TextBox ID="txtCivico" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="30" 
                            Style="z-index: 105; left: 10px; top: 72px; font-size: 8pt;" TabIndex="1"
                            Width="66px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCap" runat="server" 
                            BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="30" 
                            Style="z-index: 105; left: 10px; top: 72px; font-size: 8pt;" TabIndex="1"
                            Width="50px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProvincia" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="30" 
                            Style="z-index: 105; left: 10px; top: 72px; font-size: 8pt;" TabIndex="1"
                            Width="41px" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 172px">
                        <asp:TextBox ID="txtComune" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; font-size: 8pt;" TabIndex="1" Width="170px" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 283px">
                        <asp:TextBox ID="txtTelefono1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; font-size: 8pt;" TabIndex="1" Width="170px" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr>
                    <td 
                        class="styleMilles">
                        Telefono2</td>
                    <td 
                        class="styleMilles">
                        Cellulare</td>
                    <td class="styleMilles">
                        Fax</td>
                    <td class="styleMilles">
                        e - mail</td>
                </tr>
                <tr>
                    <td style="width: 178px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtTelefono2" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px; font-size: 8pt;" TabIndex="1" Width="100px" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 187px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtCellulare" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="30" 
                            Style="z-index: 105; left: 10px; top: 72px; font-size: 8pt;" TabIndex="1"
                            Width="100px" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 172px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtFax" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="30" Style="z-index: 107; right: 628px; left: 180px;
                            top: 72px; font-size: 8pt;" TabIndex="2" Width="100px" ReadOnly="True"></asp:TextBox></td>
                    <td style="width: 283px; vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtEmail" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="16" Style="z-index: 107; right: 628px; left: 351px;
                            top: 72px; font-size: 8pt;" TabIndex="3" Width="200px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
            </td>
    </tr>
    <tr>
    <td>
                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                Font-Underline="True" ForeColor="Blue" Text="Amministratori Precedenti" Width="369px"></asp:Label></td>

    </td>
    </tr>
    <tr>
        <td style="vertical-align: top; width: 80px; height: 81px; text-align: left">
            <div style="border-right: #ccccff 2px solid; border-top: #ccccff 2px solid; left: 0px; vertical-align: top;
                overflow: auto; border-left: #ccccff 2px solid; width: 703px; border-bottom: #ccccff 2px solid;
                top: 0px; height: 100px; text-align: left">
                <asp:DataGrid ID="DataGridAmministratori" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="687px">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="COGNOME">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NOME">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TITOLO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TITOLO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA INIZIO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INIZIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA FINE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_FINE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
        </td>
    </tr>
</table>
