<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettRateizz.aspx.vb" Inherits="Contratti_DettRateizz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 12pt;
            color: #0000FF;
            background-color: #FFFF99;
            text-align: center;
        }
        .style2
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
            color: #4A3C8C;
            text-align: justify;
        }
        
        .style3
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 11pt;
            color: #CC3300;
            background-color: #FFFF99;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="style1">
                PIANO DI RATEIZZAZIONE
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="lblSubtitle" runat="server" Text="Cognome" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Label ID="lblErrore" runat="server" Font-Bold="False" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblCapitale" runat="server"></asp:Label>
                <asp:Label ID="lblInteresse" runat="server"></asp:Label>
                <asp:Label ID="lblNumRate" runat="server"></asp:Label>
                <asp:Label ID="lblSingRata" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 100%; height: 420px;">
                                <asp:DataGrid ID="DataGridRate" runat="server" BackColor="White" Font-Bold="False"
                                    Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                                    Font-Strikeout="False" Font-Underline="False" GridLines="None" PageSize="24"
                                    Style="z-index: 105; left: 193px; top: 54px" Width="100%" EnableViewState="False"
                                    BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="2" CellSpacing="5"
                                    AutoGenerateColumns="False">
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <ItemStyle ForeColor="Black" BackColor="#DEDFDE" />
                                    <Columns>
                                        <asp:BoundColumn DataField="NUM_RATA" HeaderText="N° RATA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="IMPORTO_RATA" HeaderText="RATA €">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="QUOTA_CAPITALI" HeaderText="QUOTA CAPITALE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="QUOTA_INTERESSI" HeaderText="QUOTA INTERESSI">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO €">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="IMP_PAGATO" HeaderText="IMPORTO PAGATO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_BOLLETTA" HeaderText="ID_BOLLETTA" ReadOnly="True"
                                            Visible="False"></asp:BoundColumn>
                                    </Columns>
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="#E7E7FF" />
                                    <SelectedItemStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                </asp:DataGrid>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            
                            <asp:ImageButton ID="btnPrintToPdf" runat="server" ImageUrl="../NuoveImm/Printer-icon.png"
                                Style="z-index: 102; right: 805px; left: 15px; top:10px" TabIndex="-1" ToolTip="Stampa in PDF"
                                CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
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
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
