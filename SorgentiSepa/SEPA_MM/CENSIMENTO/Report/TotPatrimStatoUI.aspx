<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TotPatrimStatoUI.aspx.vb"
    Inherits="CENSIMENTO_Report_TotPatrimStatoUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tot. Patrimoniali per stato UI</title>
    <style type="text/css">
        .titoli_tabella
        {
            font-size: 8pt;
            font-family: Arial;
            font-weight: bold;
            color: White;
            width: 100%;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="text-align: left; height: 21px;">
                    <span style="font-family: Arial"><strong>
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../NuoveImm/Img_ExportExcel.png"
                            TabIndex="2" ToolTip="Esporta in Excel" />
                    </strong></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="font-family: Arial"><strong style="text-align: center">Totalizzazioni patrimoniali
                        per stato UI </strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTot" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%"  style="text-align: center"><tr align="center">
        <td align="center">
    <div style="width:100%; text-align:center">
    <table cellpadding="0" cellspacing="0" bgcolor="#507CD1" width="1500px" 
            border="1px">
        <tr>
            <td style="width: 300px" rowspan="3" colspan="0">
                <asp:Label ID="Label1" runat="server" CssClass="titoli_tabella">COMPLESSO</asp:Label>
            </td>
            <td style="width: 200px" rowspan="3" colspan="0">
                <asp:Label ID="Label2" runat="server" CssClass="titoli_tabella">INDIRIZZO: via e num. civico</asp:Label>
            </td>
            <td style="width: 200px" rowspan="3" colspan="0">
                <asp:Label ID="Label3" runat="server" CssClass="titoli_tabella">EDIFICIO</asp:Label>
            </td>
            <td style="width: 600px" align="center" colspan="12">
                <asp:Label ID="lbltabella" runat="server" CssClass="titoli_tabella"></asp:Label>
            </td>
            <td style="width: 200px" colspan="2" rowspan="2">
                <asp:Label ID="Label4" runat="server" CssClass="titoli_tabella">TOTALI</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 100px" colspan="2">
                <asp:Label ID="Label5" runat="server" CssClass="titoli_tabella">OCCUPATI CONTR. REGOLARE</asp:Label>
            </td>
            <td style="width: 100px" colspan="2">
                <asp:Label ID="Label6" runat="server" CssClass="titoli_tabella">OCCUPATI ABUSIVAMENTE</asp:Label>
            </td>
            <td style="width: 100px" colspan="2">
                <asp:Label ID="Label7" runat="server" CssClass="titoli_tabella">LIBERI</asp:Label>
            </td>
            <td style="width: 100px" colspan="2">
                <asp:Label ID="Label8" runat="server" CssClass="titoli_tabella">NON DEFINITE</asp:Label>
            </td>
            <td style="width: 100px" colspan="2">
                <asp:Label ID="Label22" runat="server" CssClass="titoli_tabella">NON AGIBILI</asp:Label>
            </td>
            <td style="width: 100px" colspan="2">
                <asp:Label ID="Label9" runat="server" CssClass="titoli_tabella">PARTI COMUNI - ALTRE</asp:Label>
            </td>

        </tr>
        <tr>
            <td style="width: 50px">
                <asp:Label ID="Label10" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label11" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label12" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label13" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label14" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label15" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label16" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label17" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label18" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label19" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label23" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 50px">
                <asp:Label ID="Label24" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:Label ID="Label20" runat="server" CssClass="titoli_tabella">Num.</asp:Label>
            </td>
            <td style="width: 100px">
                <asp:Label ID="Label21" runat="server" CssClass="titoli_tabella">MQ</asp:Label>
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
        CellPadding="1" ForeColor="#333333" Width="1500px" ShowHeader="False" BorderColor="Gray"
        BorderStyle="Solid">
        <AlternatingItemStyle BackColor="White" BorderStyle="None" />
        <Columns>
            <asp:BoundColumn DataField="ID_COMPLESSO" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="DENOMINAZIONE">
                <HeaderStyle Width="300px" />
                <ItemStyle Width="300px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False"/>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="INDIRIZZO">
                <HeaderStyle Width="200px" />
                <ItemStyle Width="200px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="COD_EDIFICIO">
                <HeaderStyle Width="200px" />
                <ItemStyle Width="200px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_REGOLARE">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_REGOLARE">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_ABUSIVA">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_ABUSIVA">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_LIBERA">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_LIBERA">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_INDEF">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_INDEF">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
             <asp:BoundColumn DataField="N_INAG">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_INAG">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_COMUNI">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_COMUNI">
                <HeaderStyle Width="50px" />
                <ItemStyle Width="50px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="N_TOTALE">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:BoundColumn DataField="MQ_TOTALE">
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
        </Columns>
        <EditItemStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <ItemStyle BackColor="White" Font-Names="arial" Font-Size="8pt" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:DataGrid>
    </div>
    </td></tr></table>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
