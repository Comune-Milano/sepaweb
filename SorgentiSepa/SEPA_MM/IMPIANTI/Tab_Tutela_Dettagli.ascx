<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Tutela_Dettagli.ascx.vb" Inherits="Tab_Tutela_Dettagli" %>
<table id="TABBLE_LISTA" style="width: 765px">
    <tr>
        <td>
            <asp:Label ID="lblCancelli" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO CANCELLI"
                Width="368px" Height="30px"></asp:Label></td>
        <td style="width: 15px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 280px">
                <asp:DataGrid ID="DataGridCA" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="15" Width="680px">
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
                        <asp:TemplateColumn HeaderText="CARRABILE">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRABILE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRABILE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="AUTOMATIZZATO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AUTOMATIZZATO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AUTOMATIZZATO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="MARCA" HeaderText="MARCA">
                            <HeaderStyle Width="25%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="MODELLO" HeaderText="MODELLO">
                            <HeaderStyle Width="25%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DITTA_MANUTENZIONE" HeaderText="DITTA MANUTENZIONE">
                            <HeaderStyle Width="30%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
            </td>
        <td style="width: 15px">
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggCA" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Tutela_Dettagli_txtAppareCA').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_CA').style.visibility='visible';"
                            TabIndex="16" ToolTip="Aggiunge un nuovo cancello" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaCA" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloCA();"
                            TabIndex="17" ToolTip="Elimina il cancello selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriCA" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Tutela_Dettagli_txtAppareCA').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_CA').style.visibility='visible';"
                            TabIndex="18" ToolTip="Modifica il cancello selezionato" Width="60px" /></td>
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
    <tr>
        <td>
            <asp:TextBox ID="txtSelCA" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="20px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td style="width: 15px;">
        </td>
        <td>
        </td>
    </tr>
</table>

<div id="DIV_CA" style="width: 800px; height: 550px; background-color: #dedede; left: 0px; position: absolute; top: 0px; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);" >
    <table style="left: 130px; width: 45%; position: absolute;
        top: 100px; height: 248px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Cancelli</span></strong></td>
        </tr>
        <tr>
            <td style="height: 227px">
                <table style="width: 208px">
                    <tr>
                        <td style="height: 24px">
                            <asp:Label ID="lblCarrabile" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="100px">Carrabile</asp:Label></td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="cmbCarrabile" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="19" ToolTip="Cancello Carrabile" Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 24px">
                            <asp:Label ID="lblAutomatizzato" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Automatizzato</asp:Label></td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="cmbAutomatizzato" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="20" ToolTip="Cancello Automatizzato" Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 54px">
                            <asp:Label ID="lblMarca" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Marca *</asp:Label></td>
                        <td style="height: 54px">
                            <asp:TextBox ID="txtMarca" runat="server" Font-Names="Arial" Font-Size="9pt" Height="50px"
                                MaxLength="200" Style="left: 144px; top: 112px" TabIndex="21" TextMode="MultiLine"
                                Width="350px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblModello" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Modello</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtModello" runat="server" Font-Names="Arial" Font-Size="9pt" Height="50px"
                                MaxLength="200" Style="left: 144px; top: 112px" TabIndex="22" TextMode="MultiLine"
                                Width="350px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDitta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Ditta Manutenzione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDitta" runat="server" Font-Names="Arial" Font-Size="9pt" Height="50px"
                                MaxLength="200" Style="left: 144px; top: 112px" TabIndex="23" TextMode="MultiLine"
                                Width="350px"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDCA" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciCA" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Tutela_Dettagli_txtAppareCA').value='0';"
                                Style="cursor: pointer" TabIndex="24" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiCA" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_CA').style.visibility='hidden';document.getElementById('Tab_Tutela_Dettagli_txtAppareCA').value='0';"
                                    Style="cursor: pointer" TabIndex="25" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="400px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 100px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="536px" />
</div>

<asp:TextBox ID="txtAppareCA"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_Tutela_Dettagli_txtAppareCA').value!='1') {
document.getElementById('DIV_CA').style.visibility='hidden';
}

</script>
