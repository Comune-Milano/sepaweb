<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_MorositaInquilino_Bol.ascx.vb" Inherits="Tab_Morosita_Bollette" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            &nbsp;<asp:Label ID="lblELENCO_BOLLETTE" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO BOLLETTE" Width="248px"></asp:Label>&nbsp;</td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 199px">
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 180px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="18" Width="100%" BorderColor="Blue">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            <HeaderStyle Width="0%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NUMERO_RATA" HeaderText="N&#176;/TIPO">
                            <HeaderStyle Width="5%" Font-Bold="True" Font-Italic="False" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIFERIMENTO_DA" HeaderText="PERIODO DAL">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIFERIMENTO_A" HeaderText="PERIODO AL">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                            <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                            <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" 
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PAGAMENTO" HeaderText="PAGAMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_INS_PAGAMENTO" HeaderText="DATA PAGAMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Width="10%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Width="25%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                    Text="Annulla"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Modifica">Seleziona</asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td style="height: 199px">
            </td>
        <td style="height: 199px">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnApri1" runat="server" CausesValidation="False" 
                            ImageUrl="~/MOROSITA/Immagini/DettaglioBollette1.png"
                            TabIndex="11" ToolTip="Visualizza il dettaglio della bolletta selezionata" OnClientClick="ApriBolInquilino();" /></td>
                </tr>
                <tr>
                    <td style="height: 10px">
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnSolleciti" runat="server" CausesValidation="False" 
                            ImageUrl="~/MOROSITA/Immagini/DettaglioSolleciti1.png"
                            TabIndex="11" ToolTip="Visualizza il dettaglio dei solleciti della bolletta selezionata"  OnClientClick="ApriBolSolleciti();" /></td>
                </tr>
                <tr>
                    <td style="height: 10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btn_Export" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png" /></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>

<asp:TextBox ID="txtAppare1"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdConnessione"  runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:HiddenField ID="txt_FL_BLOCCATO" runat="server" />
<asp:HiddenField ID="txtIdComponente" runat="server" />

<script type="text/javascript">

//    function controlla_div() {
////        if (document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value != "") 
////        {
////            document.getElementById('Tab_Morosita_Bollette_txtAppare1').value = '1';
////            document.getElementById('DIV_1').style.visibility = 'visible';
////        }
////        else 
////        {
////            alert('Nessuna riga selezionata!')
////        }
//    }

//    if (document.getElementById('Tab_Morosita_Bollette_txtAppare1').value != '1') {
//        document.getElementById('DIV_1').style.visibility='hidden';
//    }
    
    function ApriAnteprima() {
    
        if (document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value != '1') {
            var fin;
            fin = window.open('AnteprimaBolletta.aspx?ID=' + document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value, 'Anteprima' + document.getElementById('Tab_Morosita_Bollette_txtIdComponente').value, 'top=0,left=0,resizable=yes,scrollbars=yes');
            fin.focus();    
        }
    }
    
    
    
</script>

