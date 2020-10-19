<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RateizDati.aspx.vb" Inherits="RATEIZZAZIONE_RateizDati" %>

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
                .style3
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
            color: #CC3300;
            background-color: #FFFF99;
            text-align: left;
            font-style: italic;
        }
        .style2
        {
            height: 19px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <table width="100%">
        <tr>
            <td class="style1">
                <asp:Label ID="lblTitolo" runat="server" 
                    Text="SIMULAZIONE PROSPETTO RATEIZZAZIONE "></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style3" >
                <asp:Label ID="lblSubtitle" runat="server" Text="Cognome" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <div style="overflow: auto; width: 100%;
                height: 500px">
                <asp:DataGrid ID="DataGridRate" runat="server"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="None" PageSize="24" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="97%" 
                    EnableViewState="False" BorderColor="White" BorderStyle="Ridge" 
                    BorderWidth="2px" CellPadding="2" CellSpacing="5" 
                    AutoGenerateColumns="False">
                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Black" BackColor="#DEDFDE" />
                    <Columns>
                        <asp:BoundColumn DataField="NUMRATA" HeaderText="N° RATA">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="EMISSIONE" HeaderText="DATA EMISSIONE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SCADENZA" HeaderText="DATA SCADENZA">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" Wrap="False" 
                                HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTORATA" HeaderText="RATA €">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="QUOTACAPITALI" HeaderText="QUOTA CAPITALE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="QUOTAINTERESSI" HeaderText="QUOTA INTERESSI">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTORESIDUO" HeaderText="RESIDUO €.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="#E7E7FF" />
                    <SelectedItemStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                </asp:DataGrid>
            </div>
                        </span></strong>
                        </td>
                        <td style="vertical-align: top">
                <table style="border: thin ridge #C0C0C0" width="100%" id="miaTable">
                    <tr >
                        <td  colspan = "2">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" Text="RIEPILOGO" ForeColor="#FF3300"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr style="border-bottom-style: groove; border-bottom-width: 2px; border-bottom-color: #000000;">
                        <td style="border-bottom-style: groove; border-bottom-width: 1px; border-bottom-color: #000000;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" Text="Capitale €."></asp:Label>
                        </td>
                        <td style="text-align: right; border-bottom-style: groove; border-bottom-width: 1px; border-bottom-color: #000000;">
                            <asp:Label ID="lblCapitale" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="0,00" Width="60px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom-style: groove; border-bottom-width: 1px; border-bottom-color: #000000;" 
                            class="style2">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" Text="Tasso Int. %"></asp:Label>
                        </td>
                        <td style="text-align: right; border-bottom-style: groove; border-bottom-width: 1px; border-bottom-color: #000000;" 
                            class="style2">
                            <asp:Label ID="lblInteresse" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" style="text-align: right" Text="0,00" Width="60px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom-style: groove; border-bottom-width: 1px; border-bottom-color: #000000;">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" Text="Num. Rate"></asp:Label>
                        </td>
                        <td style="text-align: right; border-bottom-style: groove; border-bottom-width: 1px; border-bottom-color: #000000;">
                            <asp:Label ID="lblNumRate" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="0,00" Width="60px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" Text=" "></asp:Label>
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" Text="Imp. Rata"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblImpRata" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="0,00" Width="60px"></asp:Label>
                        </td>
                    </tr>
                </table>
                            <br />
    
                    <asp:ImageButton ID="btnPrintToPdf" runat="server" ImageUrl="~/NuoveImm/Printer-icon.png"
                        
                        
                        
                        Style="z-index: 102; right: 805px; left: 12px; top: 16px" TabIndex="-1"
                        ToolTip="Stampa in PDF" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
                    </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    </form>
                        <script  language="javascript" type="text/javascript">
                            document.getElementById('dvvvPre').style.visibility = 'hidden';
                        </script>

</body>
</html>
