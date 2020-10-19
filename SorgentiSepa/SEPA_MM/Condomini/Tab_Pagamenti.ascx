<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Pagamenti.ascx.vb" Inherits="Condomini_Tab_Pagamenti" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
<table style="width: 61%; height: 95px">
    <tr>
        <td style="vertical-align: top; height: 81px; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 703px; top: 0px; height: 260px; text-align: left">
                <asp:DataGrid ID="DataGridPagamenti" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="150%">
                    <Columns>
                        <asp:BoundColumn DataField="ID_PAGAMENTO" HeaderText="ID_PAGAMENTO" 
                            Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PROGR" HeaderText="Num. A.D.P.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA A.D.P.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_DATA_MANDATO" HeaderText="NUM./DATA MANDATO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="IMPORTO PAGATO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
            <table style="width: 50%">
                <tr>
                    <td>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="572px">Nessuna Selezione</asp:TextBox>
            <asp:HiddenField ID="txtidPagamento" runat="server" Value="0" />
            <asp:HiddenField ID="txtDescrizione" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;height: 81px; text-align: left">
    
            <table style="width:100%;">
                <tr>
                    <td>
    
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <img src="Immagini/print-icon.png" 
                            
            style="cursor: pointer;" 
                            onclick="PaymentStamp();" 
                title="Stampa il Pagamento Selezionato" /></span></strong></td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Condomini/Immagini/Excel-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px; width: 16px;" 
                            ToolTip="Esportazione excel tabella" CausesValidation="False" />
                    </td>
                </tr>
            </table>
            </td>
    </tr>
</table>
<%--    <script type="text/javascript">
        function ContrSave() {
            if (document.getElementById('Tab_Pagamenti1_txtidPagamento').value != 0) {
                if (document.getElementById('txtModificato').value == '1') {
                    alert('Prima di stampare, salvare le modifiche apportate al condominio!')
                    return;
                }

            }
        
       }
     
    </script>--%>