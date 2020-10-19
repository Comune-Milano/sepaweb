<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RateizzEmesse.aspx.vb" Inherits="RATEIZZAZIONE_RateizzEmesse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rateizzazioni Contratto </title>
    <style type="text/css">
        .style1
        {
            text-align: center;
            font-family: Arial;
            font-size: 14pt;
        }
        .style2
        {
            text-align: left;
            font-family: Arial;
            font-size: 12pt;
        }
        </style>
    <script type="text/javascript">
        var Selezionato;
        function ConfirmSave() {

            var chiediConferma
            chiediConferma = window.confirm("Attenzione...\nSi è sicuri di voler annullare la rateizzazione?\nL\'operazione non è reversibile!\nProcedere?");
            if (chiediConferma == true) {
                document.getElementById('ConfermaSalva').value = '1';

            }
            else {

                document.getElementById('ConfermaSalva').value = '0';

            }
        }
    </script>
</head>
<body>
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 99%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF; visibility :visible ">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <img style="cursor: pointer" alt="Esporta in xls" src="../NuoveImm/Img_ExportExcel.png"
                                onclick="window.open('ExportGenerale.aspx?IDCONT='+ document.getElementById('idContratto').value ,'')" />
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="width: 70%">
                            &nbsp;
                        </td>
                        <td style="width: 10%; text-align: right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1" style="background-color: #FFFFCC">
                <strong>ELENCO RATEIZZAZIONI </strong>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid runat="server" ID="DataGrid" AutoGenerateColumns="False" CellPadding="1"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None" CellSpacing="1"
                    Width="100%" ShowFooter="True">
                    <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_RATEIZZAZIONE" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA INIZIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO_RAT" HeaderText="TIPO RATEIZZAZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_RATE" HeaderText="N° RATE">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOT_RAT" HeaderText="IMPORTO RATEIZZATO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ANTICIPO" HeaderText="ANTICIPO">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_MAV_EMESSI" HeaderText="M.A.V. EMESSI">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MAV_PAGATI" HeaderText="M.A.V. PAGATI">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FL_ANNULLATA" HeaderText="FL_ANNULLATA" 
                            Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="DETTAGLI">
                            <ItemTemplate>
                                <img style="cursor: pointer" onclick="window.open('DettaglioRat.aspx?ID=<%# DataBinder.Eval(Container, "DataItem.ID_RATEIZZAZIONE") %>&IDCONT=<%# DataBinder.Eval(Container, "DataItem.ID_CONTRATTO") %>');"
                                    alt="" src="Zoom-icon.png" />
                                <asp:Label runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DEBITO RATEIZZATO">
                            <ItemTemplate>
                                <img style="cursor: pointer" onclick="window.open('DettaglioDebt.aspx?ID=<%# DataBinder.Eval(Container, "DataItem.ID_RATEIZZAZIONE") %>&IDCONT=<%# DataBinder.Eval(Container, "DataItem.ID_CONTRATTO") %>');"
                                    alt="" src="ZoomDebt.png" />
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="PIANO DI RIENTRO">
                            <ItemTemplate>
                                 <img style="cursor: pointer" onclick="window.open('RateizDati.aspx?IDCONTRATTO=<%# DataBinder.Eval(Container, "DataItem.ID_CONTRATTO") %>&IDRAT=<%# DataBinder.Eval(Container, "DataItem.ID_RATEIZZAZIONE") %>','Rateizz', 'height=598,width=920,scrollbars=no');"                                    alt="" src="print-icon.png" />
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateColumn>
                    </Columns>
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
        <tr>
            <td>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnEliminaR" runat="server" ImageUrl="../NuoveImm/DelRateizzazione.png"
                                    Style="width: 32px" ToolTip="Elimina la Rateizzazione Selezionata" OnClientClick="ConfirmSave();document.getElementById('splash').style.visibility = 'visible';" />
                &nbsp;
            </td>
                            <td>
                                &nbsp;<asp:Label ID="lblButton" runat="server" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="8pt" Text="Elimina la Reteizzazione Selezionata"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="idRat" runat="server" Value="0" />
                                <asp:HiddenField ID="ConfermaSalva" runat="server" Value="0" />
                                <asp:HiddenField ID="idContratto" runat="server" Value="0" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
