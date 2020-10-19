<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScegliAssestamento.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_ASSESTAMENTO_ScegliAssestamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Prospetto</title>
    <script type="text/javascript" language="javascript">
        var Selezionato;
    </script>
</head>
<body >
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500; text-align: center; font-size: 10px; width: 777px; height: 525px; visibility: hidden; vertical-align: top; line-height: normal; top: 15px; left: 12px; background-color: #FFFFFF;">
        <table style="height: 100%; width: 100%">
            <tr>
                <td style="width: 100%; height: 100%; vertical-align: middle; text-align: center">
                    <img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' />
                    <br />
                    <br />
                    <asp:Label runat="server" ID="lblCaricamento" Font-Names="Arial" Font-Size="10pt">caricamento in corso...</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Assestamento Esercizio Finanziario</strong></span>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Label ID="lblTitolo" runat="server" Text="Riepilogo degli Assestamenti generati nell'Esercizio Finanziario in corso"
                    Font-Bold="True" Font-Names="arial" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height:310px;">
                    <div style="width: 100%; height: 250px; overflow: auto">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="DataGridAssestamenti" runat="server" AutoGenerateColumns="False"
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105; left: 193px; top: 54px"
                                Width="97%" BorderColor="Gray" BorderWidth="1px">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ESERCIZIO" HeaderText="ES. FINANZIARIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA CREAZ.ASSESTAMENTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="STATO" HeaderText="STATO ASSEST.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
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
                <td style="height: 50px;">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Visible="False"
                    Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:TextBox ID="txtMia" runat="server" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" MaxLength="150" ReadOnly="True" Width="600px" 
                        BackColor="#FBFBFB" Style="text-align: left">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 15px;"></td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                            <td style="width: 80%"></td>
                        <td style="width: 10%">
                            <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="../../../NuoveImm/Img_Procedi.png"
                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                TabIndex="31" />
                        </td>
                        <td style="width: 10%">
                            <asp:Image ID="imgEsci" runat="server" Style="cursor: pointer" 
                            ImageUrl="../../../NuoveImm/Img_EsciCorto.png" onclick="document.location.href='../../pagina_home.aspx';" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="prosegui" runat="server" Value="0" />
    <asp:HiddenField ID="idPfMain" runat="server" Value="0" />
    <asp:HiddenField ID="idAssestamento" runat="server" Value="0" />
    <asp:HiddenField ID="idStato" runat="server" Value="0" />
    </form>
    <script type="text/javascript" language="javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
