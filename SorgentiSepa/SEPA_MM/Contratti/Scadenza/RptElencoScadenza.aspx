<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptElencoScadenza.aspx.vb"
    Inherits="Contratti_Scadenza_RptElencoScadenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">



    <title>Report Contratti In Scadenza</title>
    <style type="text/css">
        .style1
        {
            width: 993px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Height="16px" Style="z-index: 104; left: 574px; position: absolute;
        top: 14px" Visible="False" Width="525px"></asp:Label>
    <table width="100%">
        <tr>
        <td>
         <table width="100%">
         <tr>
            <td style="width:5%">
                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                    TabIndex="2" ToolTip="Esporta in Excel" Style="height: 12px" />
            </td>
             <td style="width:15%">
      
                    <asp:Menu ID="TStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                        Orientation="Horizontal" RenderingMode="List" ToolTip="Elenco Stampe ">
                        <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                        <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                            ForeColor="#0066FF" Width="200px" />
                        <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                            VerticalPadding="1px" />
                        <Items>
                            <asp:MenuItem ImageUrl="~/NuoveImm/btnStampe.png" Selectable="False" Value="">
                                <asp:MenuItem Text="Report per Comune di Competenza" Value="1"></asp:MenuItem>
                                <asp:MenuItem Text="Report per Assegnatario" Value="2"></asp:MenuItem>
                                <asp:MenuItem Text="Report per Ufficio Fiscale" Value="3"></asp:MenuItem>
                               
                            </asp:MenuItem>
                        </Items>
                    </asp:Menu>




            </td>
             <td style="width:70%">

            </td>
            </tr>
           </table>
           </td>
        </tr>
        <tr>
            <td style="vertical-align: middle; text-align: center" height="40px" align="center" 
                valign="middle">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
                    Style="font-size: small; text-align:center;">REPORT CONTRATTI IN SCADENZA ALLA DATA DEL <%=DataCompleta.Value%> </asp:Label><br />
         
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left">
                <div style="border: 4px solid #ACC8F0; width: 99.5%; height: 600px; overflow: auto;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="dgvRptScadenza" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            ForeColor="#333333" Style="z-index: 11; left: 18px; top: 81px" Width="100%" BorderColor="Gray"
                            BorderWidth="2px" ViewStateMode="Enabled">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="Aqua" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False" />
                            <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DELIBERA" HeaderText="DATA DELIBERA">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DECORRENZA" HeaderText="DATA DECORRENZA">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_SCADENZA_RINNOVO" HeaderText="DATA SC. RINNOVO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="GG_FINE_DEC" HeaderText="GG FINE DECOR.">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD U.I.">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCALA" HeaderText="SC">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CAP" HeaderText="CAP">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PROV_COMUNE" HeaderText="COMUNE">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="White" />
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
               
             <asp:Label ID="lbl_risultati" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                    Style="font-size: small; text-align:left;"></asp:Label><br />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Text="Label" Visible="False" Width="624px"></asp:Label>
    &nbsp; &nbsp;&nbsp; &nbsp;
    <br />
    <br />
    <asp:HiddenField ID="IdContratto" runat="server" Value="0" />
    <asp:HiddenField ID="CodContratto" runat="server" Value="0" />
    <asp:HiddenField ID="cap" runat="server" />
    <asp:HiddenField ID="dataRif" runat="server" />
    <asp:HiddenField ID="DataCompleta" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">

        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
<script type="text/javascript">

</script>
</html>
