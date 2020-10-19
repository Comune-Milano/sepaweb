<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Antincendio_Fumi.ascx.vb" Inherits="Tab_Antincendio_Fumi" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="lblFumi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO RILEVATORI FUMI"
                Width="368px" Height="15px"></asp:Label></td>
        <td style="width: 15px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 250px">
                <asp:DataGrid ID="DataGridF" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="40" Width="656px">
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
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPOLOGIA_FUMI" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="UBICAZIONE_CENTRALINA" HeaderText="UBICAZIONE CENTRALINA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_TIPOLOGIA_RILEVAZIONE" HeaderText="ID_TIPOLOGIA_RILEVAZIONE"
                            Visible="False"></asp:BoundColumn>
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
            <asp:TextBox ID="txtSelF" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="20px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td style="width: 15px">
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggF" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Antincendio_Fumi_txtAppareF').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_F').style.visibility='visible';"
                            TabIndex="41" ToolTip="Aggiunge un nuovo rilevatore fumi" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaF" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloFumi();"
                            TabIndex="42" ToolTip="Elimina il componente selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriF" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Antincendio_Fumi_txtAppareF').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_F').style.visibility='visible';"
                            TabIndex="43" ToolTip="Modifica il componente selezionato" Width="60px" /></td>
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

<div id="DIV_F" style="width: 800px; height: 550px; background-color: #dedede; left: 0px; position: absolute; top: 0px; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);" >
    <table style="left: 136px; width: 45%; position: absolute;
        top: 104px; height: 248px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td style="height: 21px">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Rilevatori Fumi</span></strong></td>
        </tr>
        <tr>
            <td style="height: 177px">
                <table style="width: 208px">
                    <tr>
                        <td style="height: 54px">
                            <asp:Label ID="lblRilevatori" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Tipo Rilevatore Fumi *</asp:Label></td>
                        <td style="height: 54px">
                            <asp:DropDownList ID="cmbFumi" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="44" Width="340px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                            <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Ubicazione Centralina</asp:Label></td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtUbicazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Height="100px" MaxLength="200" TabIndex="45" TextMode="MultiLine" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDF" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciF" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Antincendio_Fumi_txtAppareF').value='0';"
                                Style="cursor: pointer" TabIndex="46" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiF" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_F').style.visibility='hidden';document.getElementById('Tab_Antincendio_Fumi_txtAppareF').value='0';"
                                    Style="cursor: pointer" TabIndex="47" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="352px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 100px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="592px" />
</div>

<asp:TextBox ID="txtAppareF"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_Antincendio_Fumi_txtAppareF').value!='1') {
document.getElementById('DIV_F').style.visibility='hidden';
}

</script>
