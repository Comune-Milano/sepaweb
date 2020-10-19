<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Pompe.ascx.vb" Inherits="Tab_Termico_Pompe" %>
<table id="TABBLE_LISTA" >
    <tr>
        <td>
            <asp:Label ID="lblPOMPE" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO POMPE DI CIRCOLAZIONE"
                Width="248px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div id="DIV1" style="border-right: #0000cc thin solid; border-top: #0000cc thin solid;
                visibility: visible; overflow: auto; border-left: #0000cc thin solid; width: 680px;
                border-bottom: #0000cc thin solid; height: 105px ">
                <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="8px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="21" Width="950px">
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
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MODELLO" HeaderText="MARCA/MODELLO"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="MATRICOLA">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DESCRIZIONE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POTENZA (KW)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
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
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSelPompe" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp;
        </td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td>
                        <asp:ImageButton ID="btnAggPompe" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Termico_Pompe_txtAppareP').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Pompe').style.visibility='visible';"
                            TabIndex="28" ToolTip="Aggiunge una nuova pompa di circolazione" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnEliminaPompa" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloPompe();"
                            TabIndex="29" ToolTip="Elimina la pompa di circolazione selezionata" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="height: 14px">
                        <asp:ImageButton ID="btnApriPompa" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Termico_Pompe_txtAppareP').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Pompe').style.visibility='visible';"
                            TabIndex="30" ToolTip="Modifica la pompa di circolazione selezionata" Width="60px" /></td>
                </tr>
            </table>
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ELENCO POMPE DI SOLLEVAMENTO ACQUE METEORICHE"
                Width="384px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div id="Div2" style="border-right: #0000cc thin solid; border-top: #0000cc thin solid;
                visibility: visible; overflow: auto; border-left: #0000cc thin solid; width: 680px;
                border-bottom: #0000cc thin solid; height: 105px">
                <asp:DataGrid ID="DataGrid4" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="8px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="21" Width="888px">
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
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ANNO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POTENZA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PORTATA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PORTATA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PORTATA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PREVALENZA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENZA") %>'></asp:Label>
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
            <asp:TextBox ID="txtSelPompeS" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
        </td>
        <td>
            <table style="width: 57%" id="TAB_BOTTONI">
                <tr>
                    <td>
                        <asp:ImageButton ID="btnAggPompeS" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Termico_Pompe_txtAppareS').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_PompeS').style.visibility='visible';"
                            TabIndex="28" ToolTip="Aggiunge una nuova pompa di sollevamento" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnEliminaPompaS" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloPompe();"
                            TabIndex="29" ToolTip="Elimina la pompa di sollevamento selezionata" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="height: 14px">
                        <asp:ImageButton ID="btnApriPompaS" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Termico_Pompe_txtAppareS').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_PompeS').style.visibility='visible';"
                            TabIndex="30" ToolTip="Modifica la pompa di sollevamento selezionata" Width="60px" /></td>
                </tr>
            </table>
            <br />
            <br />
        </td>
    </tr>
</table>

<br />
<div id="DIV_Pompe" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: whitesmoke; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <br />
    <table id="Table6" border="0" cellpadding="1" cellspacing="1"
        style="border-right: blue 2px; border-top: blue 2px; left: 120px;
        border-left: blue 2px; width: 45%; border-bottom: blue 2px; position: absolute;
        top: 100px; height: 248px; background-color: #ffffff; z-index: 102;">
        <tr>
            <td style="width: 404px; height: 18px; text-align: left">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Pompe di Circolazione</span></strong></td>
        </tr>
        <tr>
            <td style="width: 404px; height: 114px">
                <span style="color: #ffffff"><span style="font-size: 10pt; font-family: Arial"><strong>
                    &nbsp;<table id="Table5">
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lblMarcaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Marca/Modello *</asp:Label></td>
                            <td style="width: 156px">
                                <asp:TextBox ID="txtModelloP" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="200"
                                    Style="left: 184px; top: 80px" TabIndex="48" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                            <td style="width: 3px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px; height: 33px">
                                <asp:Label ID="lblMatricolaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Matricola</asp:Label></td>
                            <td style="width: 156px; height: 33px">
                                <asp:TextBox ID="txtMatricolaP" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="30" Style="left: 72px; top: 224px" TabIndex="49" Width="400px"></asp:TextBox></td>
                            <td style="width: 3px; height: 33px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lblCaratteristichePompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Caratteristiche Tecniche</asp:Label></td>
                            <td style="width: 156px">
                                <asp:TextBox ID="txtNoteP" runat="server" Font-Names="Arial" Font-Size="9pt" Height="64px"
                                    MaxLength="300" Style="left: 144px; top: 112px" TabIndex="50" TextMode="MultiLine"
                                    Width="400px"></asp:TextBox></td>
                            <td style="width: 3px">
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </strong></span></span><span></span>
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="font-size: 12pt; width: 404px; font-family: Times New Roman; height: 19px;
                text-align: left">
                <table style="width: 520px">
                    <tr>
                        <td style="width: 52px; height: 39px;">
                            <asp:Label ID="lblAnnoPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Anno di Costruzione</asp:Label></td>
                        <td style="height: 39px">
                            <asp:TextBox ID="txtAnnoRealizzazioneP" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="4" Style="left: 144px; top: 192px" TabIndex="51" ToolTip="aaaa" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtAnnoRealizzazioneP"
                                Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                Width="110px"></asp:RegularExpressionValidator></td>
                        <td style="width: 61px; height: 39px;">
                            <asp:Label ID="lblPotenzaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Potenza termica utile nominale</asp:Label></td>
                        <td style="height: 39px">
                            <asp:TextBox ID="txtPotenzaP" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="52"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (Kw)</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPotenzaP"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDP" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="width: 404px; height: 38px; text-align: right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciPompe" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Termico_Pompe_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="53" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_ChiudiPompe" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Pompe').style.visibility='hidden';document.getElementById('Tab_Termico_Pompe_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="54" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="376px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="616px" />
</div>

<div id="DIV_PompeS" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: whitesmoke; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table style="border-right: blue 2px; border-top: blue 2px; left: 120px;
        border-left: blue 2px; width: 49%; border-bottom: blue 2px; position: absolute;
        top: 100px; height: 248px; background-color: #ffffff; z-index: 102;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Pompe di Sollevamento
                    Acque Meteoriche</span></strong></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Label ID="lblMarcaPompeS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Marca/Modello *</asp:Label></td>
                        <td style="width: 156px">
                            <asp:TextBox ID="txtModelloS" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="200"
                                Style="left: 184px; top: 80px" TabIndex="48" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                        <td style="width: 3px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 33px">
                            <asp:Label ID="lblMatricolaPompeS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Matricola</asp:Label></td>
                        <td style="width: 156px; height: 33px">
                            <asp:TextBox ID="txtMatricolaS" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="30" Style="left: 72px; top: 224px" TabIndex="49" Width="400px"></asp:TextBox></td>
                        <td style="width: 3px; height: 33px">
                        </td>
                    </tr>
                </table>
                <table style="width: 520px">
                    <tr>
                        <td style="width: 110px">
                            <asp:Label ID="lblAnnoPompeS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Anno di Costruzione</asp:Label></td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtAnnoRealizzazioneS" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="4" Style="left: 144px; top: 192px" TabIndex="51" ToolTip="aaaa" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAnnoRealizzazioneS"
                                Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                Width="110px"></asp:RegularExpressionValidator></td>
                        <td style="width: 100px">
                            <asp:Label ID="lblPotenzaPompeS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Potenza Elettrica</asp:Label></td>
                        <td style="width: 150px">
                            <asp:TextBox ID="txtPotenzaS" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="52"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lblKW" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (Kw)</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPotenzaS"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <table style="width: 520px">
                    <tr>
                        <td style="width: 110px; height: 44px;">
                            <asp:Label ID="lblPortataS" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Portata (Q)</asp:Label></td>
                        <td style="width: 150px; height: 44px;">
                            <asp:TextBox ID="txtPortataS" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="52"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lblm3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px">(m3/h)</asp:Label><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPrevalenzaS"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                    Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                    Width="80px">Valore Numerico</asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 100px; height: 44px;">
                            <asp:Label ID="lblPrevalenza" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="80px">Prevalenza (H)</asp:Label></td>
                        <td style="width: 151px; height: 44px;">
                            <asp:TextBox ID="txtPrevalenzaS" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                TabIndex="52" Width="100px"></asp:TextBox>
                            <asp:Label ID="lblm" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (m)</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPotenzaP"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <br />
                <asp:TextBox ID="txtIDS" runat="server" Height="16px" Style="left: 640px; top: 200px"
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
                            <asp:ImageButton ID="btn_InserisciPompeS" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Termico_Pompe_txtAppareS').value='0';"
                                Style="cursor: pointer" TabIndex="53" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_ChiudiPompeS" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_PompeS').style.visibility='hidden';document.getElementById('Tab_Termico_Pompe_txtAppareS').value='0';"
                                Style="cursor: pointer" TabIndex="54" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="352px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="616px" />
</div>

<br />
<asp:TextBox ID="txtAppareP"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareS"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<br />



<script type="text/javascript">


if (document.getElementById('Tab_Termico_Pompe_txtAppareP').value!='1') {
document.getElementById('DIV_Pompe').style.visibility='hidden';
}

if (document.getElementById('Tab_Termico_Pompe_txtAppareS').value!='1') {
document.getElementById('DIV_PompeS').style.visibility='hidden';
}


</script>


