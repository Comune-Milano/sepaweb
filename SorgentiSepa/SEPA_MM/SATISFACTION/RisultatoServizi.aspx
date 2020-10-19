<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoServizi.aspx.vb"
    Inherits="SATISFACTION_RisultatoServizi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<head>
    <title>Elenco Servizi</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
</head>
<body style="background-attachment: fixed; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; width: 796px;" onload="javascript:nascondi();"
    onmouseover="javascript:nascondi();">
    <form id="Form1" runat="server">
    <asp:Image ID="ImmagineStatistiche" runat="server" ImageUrl="../IMG/StatisticheCUSTSAT.png"
        Style="cursor: pointer; position: absolute; top: 7px; right: 44px;" onmouseover="mostraNascondi();"
        ToolTip="Visualizza Grafici" />
    <table width="100%">
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <asp:Label ID="lblTitoloPagina" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12pt" ForeColor="Maroon" Text="Risultati Questionari - ">
                </asp:Label>
                <asp:Label ID="lblNumRis" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="Maroon"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <div id="ElQuestionari" style="overflow: auto; width: 777px; height: 360px;">
                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="10pt"></asp:Label>
                    <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="Vertical"
                        PageSize="100" Style="width: 1087px;" BorderWidth="1px" HorizontalAlign="Center"
                        BorderColor="#666666">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Wrap="False" Visible="True" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <SelectedItemStyle BackColor="Red" ForeColor="Red" />
                        <Columns>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="CODICE UNITA' IMMOBILIARE">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIVICO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_C" HeaderText="DATA DI COMPILAZIONE" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOMANDA" HeaderText="DOMANDA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RISPOSTA" HeaderText="RISPOSTA" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALORE" HeaderText="VALORE" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 15px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="TextBox3" runat="server" Width="100%" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 40%">
                        </td>
                        <td style="width: 20%">
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../NuoveImm/Img_Export_XLS.png"
                                ToolTip="Esporta" />
                        </td>
                        <td style="width: 20%">
                            <img onclick="document.location.href='RicercaServizi.aspx';" alt="" src="../NuoveImm/Img_NuovaRicerca.png"
                                style="cursor: pointer;" title="Nuova Ricerca" />
                        </td>
                        <td style="width: 20%">
                            <img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png"
                                style="cursor: pointer;" title="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
    <div id="divGrafici" style="border: 2px solid #CC0000; background-color: White; height: 397px;
        width: 780px; position: absolute; z-index: 1000; top: 60px; left: 11px;" align="center">
        <table width="80%">
            <tr>
                <td>
                    <asp:Chart ID="Chart1" runat="server" Height="293px" Width="380px">
                        <Series>
                            <asp:Series ChartType="Pie" Name="Series1">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Chart ID="Chart2" runat="server" Height="293px" Width="380px">
                        <Series>
                            <asp:Series ChartType="Pie" Name="Series2">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea2">
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="domandaSel" runat="server" Value="0" />
    <asp:HiddenField ID="contaris" runat="server" Value="0" />
    </form>
    <script type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        myOpacity = new fx.Opacity('divGrafici', { duration: 200 });
        myOpacity.hide();

        function nascondi() {
            myOpacity.hide();
        }
        function mostraNascondi() {
            myOpacity.toggle();
        }
             
    </script>
</body>
</html>
