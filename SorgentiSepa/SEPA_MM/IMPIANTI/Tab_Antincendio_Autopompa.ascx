<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Antincendio_Autopompa.ascx.vb" Inherits="Tab_Antincendio_Autopompa" %>
<table id="TABBLE_LISTA" style="width: 765px">
    <tr>
        <td>
            <asp:Label ID="lblTitoloGridI" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="Elenco ATTACCHI AUTOPOMPA"
                Width="648px" Height="15px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 250px"><asp:DataGrid ID="DataGridAUTOP" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="77" Width="656px">
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
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            <HeaderStyle Width="0px" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PIANI" HeaderText="PIANI">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BOCCA_COLLEGAMENTO" HeaderText="BOCCA DI COLLEGAMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Modifica">Seleziona</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                    Text="Annulla"></asp:LinkButton>
                            </EditItemTemplate>
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSelAUTOP" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="9pt" Height="20px" MaxLength="100" Style="left: 40px;
                top: 200px" TabIndex="-1" Width="680px" ReadOnly="True"></asp:TextBox></td>
        <td>
            &nbsp;
        </td>
        <td>
            <table>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggAUTOP" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Antincendio_Autopompa_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_AUTOPOMPA').style.visibility='visible';"
                            TabIndex="78" ToolTip="Aggiunge un nuovo attacco autopompa" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaAUTOP" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloAutopompa();"
                            TabIndex="79" ToolTip="Elimina l'attacco selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriAUTOP" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Antincendio_Autopompa_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_AUTOPOMPA').style.visibility='visible';"
                            TabIndex="80" ToolTip="Modifica l'attacco selezionato" Width="60px" /></td>
                </tr>
            </table>
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
        </td>
    </tr>
</table>
&nbsp;

<div id="DIV_AUTOPOMPA" style="width: 800px; height: 550px; display: block; left: 0px; position: absolute; top: 0px; background-color: #dedede; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <br /><table id="Table4" style="left: 120px;
        position: absolute; top: 88px; background-color: #ffffff; color: #0000ff; font-family: Arial; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;" >
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione ATTACCO AUTOPOMPA</span></strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;<table>
                    <tr>
                        <td>
                            <asp:Label ID="lblPiano" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="120px">Piano</asp:Label></td>
                        <td>
                            <div style="border-right: navy 1px solid; border-top: navy 1px solid;
                                overflow: auto; border-left: navy 1px solid; width: 300px; border-bottom: navy 1px solid;
                                height: 150px">
                            <asp:CheckBoxList ID="CheckBoxPiano" runat="server" BorderColor="Black" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Height="136px" TabIndex="81" Width="272px">
                            </asp:CheckBoxList>
                                <asp:Label ID="lblATTENZIONE_M" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Red" Style="z-index: 100; left: 24px; top: 32px" Width="256px">ATTENZIONE: Scegliere l'edificio dalla pagina principale per selezionare i piani.</asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px">
                            <asp:TextBox ID="txtIDAUTOP" runat="server" Height="16px" Style="left: 640px; top: 200px"
                                TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
                        <td style="height: 25px">
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 25px">
                            <asp:Label ID="lblBocca" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="120px">Bocca di Collegamento *</asp:Label></td>
                        <td style="height: 25px">
                            <asp:DropDownList ID="cmbBocca" runat="server" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="82" Width="60px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 21px">
                            &nbsp;<asp:ImageButton ID="btn_InserisciAUTOP" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Antincendio_Autopompa_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="83" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton
                                    ID="btn_ChiudiAUTOP" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_AUTOPOMPA').style.visibility='hidden';document.getElementById('Tab_Antincendio_Autopompa_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="84" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="368px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 88px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 64px" Width="488px" />
</div>

<asp:TextBox ID="txtAppare"         runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>


<script type="text/javascript">

if (document.getElementById('Tab_Antincendio_Autopompa_txtAppare').value!='1') {
document.getElementById('DIV_AUTOPOMPA').style.visibility='hidden';
}

</script>