<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustodiAnomalie.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_CustodiAnomalie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Anomalie Caricamento Custodi</title>
    <style type="text/css">

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
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo" runat="server" 
                    style="font-family: Arial; font-size: 12pt; font-weight: 700; color: #990000"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btnExport" runat="server" CssClass="bottone" 
                    Text="EXPORT XLS" ToolTip="Esporta le anomalie in formato excel" />
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <asp:DataGrid ID="dgvAnomalie" runat="server" AutoGenerateColumns="False" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;
                        left: 193px; top: 54px" Width="97%" CellPadding="4" GridLines="None" 
                    ForeColor="#333333">
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundColumn DataField="TIPO_UTENZA" HeaderText="TIPO UTENZA">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_CARICAMENTO" HeaderText="DATA CARICAMENTO">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOME_FILE" HeaderText="NOME FILE">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ANNO" HeaderText="ANNO COMPETENZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MESE" HeaderText="MESE COMPETENZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_CUSTODE" HeaderText="CUSTODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </span></strong>

            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
