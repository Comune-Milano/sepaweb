<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AggRUAbusivoDaFile.aspx.vb"
    Inherits="AMMSEPA_OperatoreSUA_AggRUAbusivoDaFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Aggiunta Rapporto Utenza Abusiva Da File</title>
    <script type="text/javascript">
        window.name = "modal";
        function ConfermaSalva() {
            var Conferma;
            Conferma = window.confirm('Attenzione...Confermi di voler salvare i dati che hanno superato la verifica?');
            if (Conferma == false) {
                document.getElementById('conferma').value = '0';
            }
            else {
                document.getElementById('conferma').value = '1';
            }
        }
    </script>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div style="width: 800px; height: 600px">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 40px">
                <td style="width: 3%">
                    &nbsp;
                </td>
                <td style="width: 97%;">
                    <asp:Label ID="lbltitle" runat="server" Text="Nuove Utenze Abusive Da File" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 90%">
                                <asp:FileUpload ID="FileUploadAbusivi" runat="server" Width="95%" />
                            </td>
                            <td style="width: 10%">
                                <asp:ImageButton ID="btnElabora" runat="server" ImageUrl="../../NuoveImm/Img_Elabora.png"
                                    ToolTip="Elabora il file" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <div style="width: 98%; height: 450px; overflow: auto">
                        <asp:DataGrid ID="dgvruabusividafile" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                            Font-Size="8pt" Width="100%" PageSize="25" BackColor="White" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            GridLines="None" AllowPaging="True" ShowFooter="True" BorderColor="Navy" BorderStyle="Solid"
                            BorderWidth="1px" Visible="False">
                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Navy" Wrap="False"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CODICE" HeaderText="CODICE CONTRATTO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                    <HeaderStyle Width="45%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="STATO" HeaderText="STATO">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="35%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" />
                            <PagerStyle Mode="NumericPages" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                <center>
                                    <asp:ImageButton ID="ImgSalva" runat="server" ImageUrl="../../NuoveImm/Img_SalvaGrande.png"
                                        ToolTip="Salva i dati inseriti" onclientclick="ConfermaSalva();" /></center>
                            </td>
                            <td style="width: 50%">
                                <center>
                                    <asp:ImageButton ID="ImgEsci" runat="server" ImageUrl="../../NuoveImm/Img_Esci_AMM.png"
                                        ToolTip="Esci" /></center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="conferma" runat="server" Value="0" />
    </form>
</body>
</html>
