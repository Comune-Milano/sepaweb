<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LinkDettaglioSpesa.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_Plan_LinkDettaglioSpesa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblTitolo" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                        Font-Overline="False" Font-Size="11pt"></asp:Label>
                    -
                    <asp:Label ID="Label2" runat="server" Text="Voce Business Plan:" Font-Bold="True"
                        Font-Names="Arial" Font-Overline="False" Font-Size="11pt"></asp:Label><asp:Label
                            ID="lblVoce" runat="server" Text="" Font-Bold="True" Font-Names="Arial" Font-Overline="False"
                            Font-Size="11pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSottotitolo" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                        Font-Overline="False" Font-Size="10pt"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoDivisione" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                        Font-Overline="False" Font-Size="10pt" Font-Underline="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width:35%">
                                <asp:Label ID="Label3" runat="server" Text="Lotto:" Font-Bold="True" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="10pt"></asp:Label>
                            </td>
                            <td style="text-align:left;width:65%">
                                <asp:Label ID="lblLotto" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width:35%">
                                <asp:Label ID="Label4" runat="server" Text="Importo diviso (IVA compresa) Euro:" Font-Bold="True" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="10pt"></asp:Label>
                            </td>
                            <td style="text-align:left;width:65%">
                                <asp:Label ID="lblImporto" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid runat="server" ID="DataGrid1" AutoGenerateColumns="True" CellPadding="1"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="#000000" GridLines="None" CellSpacing="1"
                        Width="100%" ShowFooter="true">
                        <AlternatingItemStyle BackColor="#F5F5F5" ForeColor="#000000" />
                        <EditItemStyle BackColor="#999999" />
                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                            HorizontalAlign="Center" />
                        <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
                        <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="idVoce" Value="0" />
        <asp:HiddenField runat="server" ID="idLotto" Value="0" />
        <asp:HiddenField runat="server" ID="idServizio" Value="0" />
        <asp:HiddenField runat="server" ID="idPianoF" Value="0" />
        <asp:HiddenField runat="server" ID="IDVS" Value="0" />
        <asp:HiddenField runat="server" ID="P" Value="0" />
        <asp:HiddenField runat="server" ID="tipoLotto" Value="0" />
    </div>
    </form>
</body>
</html>
