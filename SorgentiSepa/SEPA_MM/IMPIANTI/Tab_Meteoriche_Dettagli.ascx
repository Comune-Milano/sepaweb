<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Meteoriche_Dettagli.ascx.vb" Inherits="Tab_Meteoriche_Dettagli" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="lblPOMPE" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ELENCO POMPE DI SOLLEVAMENTO"
                Width="248px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 250px">
                <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="8px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="13" Width="696px">
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
                        <asp:BoundColumn DataField="MODELLO" HeaderText="MARCA/MODELLO"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="MATRICOLA">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POTENZA (KW)">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PORTATA">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PORTATA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PORTATA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PREVALENZA">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENZA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
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
            <asp:TextBox ID="txtSelPompe" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px;">
                        <asp:ImageButton ID="btnAggPompe" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Meteoriche_Dettagli_txtAppareP').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Pompe').style.visibility='visible';"
                            TabIndex="14" ToolTip="Aggiunge una nuova pompa di sollevamento" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaPompa" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();"
                            TabIndex="15" ToolTip="Elimina la pompa di sollevamento selezionata" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnApriPompa" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Meteoriche_Dettagli_txtAppareP').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Pompe').style.visibility='visible';"
                            TabIndex="16" ToolTip="Modifica la pompa di sollevamento selezionata" Width="60px" /></td>
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

<div id="DIV_Pompe" style="width: 800px; position: absolute; height: 550px; background-color: whitesmoke; left: 0px; top: 0px; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    &nbsp;
    <table style="border-right: blue 2px; border-top: blue 2px; left: 120px;
        border-left: blue 2px; width: 45%; border-bottom: blue 2px; position: absolute;
        top: 100px; height: 248px; background-color: #ffffff; z-index: 102;" id="TABLE1" >
        <tr>
            <td style="height: 2px">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Pompe di Sollevamento</span></strong></td>
            <td style="height: 2px">
            </td>
            <td style="height: 2px">
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table5">
                        <tr>
                            <td style="width: 79px;">
                                <asp:Label ID="lblMarcaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Marca/Modello *</asp:Label></td>
                            <td style="width: 156px;">
                                <asp:TextBox ID="txtModelloP" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="200"
                                    TextMode="MultiLine" Width="400px" TabIndex="17"></asp:TextBox></td>
                            <td style="width: 3px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 79px; height: 33px">
                                <asp:Label ID="lblMatricolaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Matricola/Num. Fabbrica</asp:Label></td>
                            <td style="width: 156px; height: 33px">
                                <asp:TextBox ID="txtMatricolaP" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="30" Style="left: 72px; top: 224px" TabIndex="18" Width="400px"></asp:TextBox></td>
                            <td style="width: 3px; height: 33px">
                            </td>
                        </tr>
                    </table>
                <table style="width: 520px">
                    <tr>
                        <td>
                            <asp:Label ID="lblAnnoPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Anno di Costruzione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtAnnoRealizzazioneP" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="4" Style="left: 144px; top: 192px" TabIndex="19" ToolTip="aaaa" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAnnoRealizzazioneP"
                                Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                Width="110px"></asp:RegularExpressionValidator></td>
                        <td>
                            </td>
                        <td>
                            <asp:Label ID="lblTipo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="90px">Tipo</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbTipo" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 552px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 192px" TabIndex="22" Width="150px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="IMMERSIONE">IMMERSIONE</asp:ListItem>
                                <asp:ListItem Value="ALBERO GUIDA">ALBERO GUIDA</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPotenzaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Potenza Elettrica</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPotenzaP" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="20"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lblKW" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (Kw)</asp:Label><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPotenzaP"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblPortataP" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="90px">Portata (Q)</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPortataP" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="23"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lblm3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px">(m3/h)</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPortataP"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPrevalenza" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Prevalenza (H)</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPrevalenzaP" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                TabIndex="21" Width="100px"></asp:TextBox>
                            <asp:Label ID="lblm" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (m)</asp:Label><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtPrevalenzaP"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                    Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        <td>
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDP" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciPompe" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Meteoriche_Dettagli_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="24" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_ChiudiPompe" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Pompe').style.visibility='hidden';document.getElementById('Tab_Meteoriche_Dettagli_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="25" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="368px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="616px" />
</div>

<asp:TextBox ID="txtAppareP"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_Meteoriche_Dettagli_txtAppareP').value!='1') {
document.getElementById('DIV_Pompe').style.visibility='hidden';
}
</script>
