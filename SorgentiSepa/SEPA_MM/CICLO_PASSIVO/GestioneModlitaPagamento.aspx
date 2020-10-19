<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneModlitaPagamento.aspx.vb"
    Inherits="CICLO_PASSIVO_GestioneModlitaPagamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        .style2
        {
            font-family: Arial;
            font-size: 10pt;
            color: #000099;
            width: 775px;
        }
        .style3
        {
            width: 775px;
        }
        .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var r = {
            'special': /[\W]/g,
            'codice': /[\W\_]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g,
            'numbers': /[^\d]/g
        };
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        };

    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table style="width: 100%;">
        <tr>
            <td class="TitoloModulo ">
                Gestione - Parametri - Modalità di pagamento
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 6pt" class="style3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <strong>MODALITA&#39; PAGAMENTO</strong>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="divModPagamento">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvModalPag" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="762px" CellPadding="1"
                            CellSpacing="1">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_DATA_RIF" HeaderText="ID_DATA_RIF" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="DATA RIFERIMENTO">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="cmbModPag" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="200px">
                                        </asp:DropDownList>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <strong>TIPO PAGAMENTO</strong>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <div id="divTipoPagamento" style="overflow: auto; height: 200px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvTipoPagamento" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="97%" CellPadding="1"
                            CellSpacing="1">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="GIORNI">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtGiorni" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_GIORNI") %>'
                                            Width="50px"></asp:TextBox>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style3">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
