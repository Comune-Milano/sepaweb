<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Idrico_Dettagli.ascx.vb" Inherits="TabDettagliIDRICO" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ELENCO SERBATOI PRE-AUTOCLAVE"
                Width="368px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 60px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="8px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="16" Width="784px">
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
                        <asp:TemplateColumn HeaderText="MATRICOLA/N&#176; FABBRICA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="VOLUME">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VOLUME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VOLUME") %>'></asp:Label>
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
                        <asp:BoundColumn DataField="PRESSIONE_BOLLA" HeaderText="PRESSIONE BOLLA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PRESSIONE_ESERCIZIO" HeaderText="PRESSIONE ESERCIZIO" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSelPreSerbatoi" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggPreSerbatoio" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('TabDettagliIDRICO_txtAppareS').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Serbatoi').style.visibility='visible';"
                            TabIndex="17" ToolTip="Aggiunge un nuovo Serbatoio Autoclave" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaPreSerbatoio" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();"
                            TabIndex="18" ToolTip="Elimina il Serbatoio Autoclave Selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriPreSerbatoio" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TabDettagliIDRICO_txtAppareS').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Serbatoi').style.visibility='visible';"
                            TabIndex="19" ToolTip="Modifica il Serbatoio Autoclave Selezionato" Width="60px" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ELENCO SERBATOI AUTOCLAVE" Width="368px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 60px">
                <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="20" Width="784px">
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
                        <asp:TemplateColumn HeaderText="MATRICOLA/N&#176; FABBRICA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="VOLUME">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VOLUME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VOLUME") %>'></asp:Label>
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
                        <asp:BoundColumn DataField="PRESSIONE_BOLLA" HeaderText="PRESSIONE BOLLA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PRESSIONE_ESERCIZIO" HeaderText="PRESSIONE ESERCIZIO" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSelSerbatoi" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
        </td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggSerbatoio" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('TabDettagliIDRICO_txtAppareS').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Serbatoi').style.visibility='visible';"
                            TabIndex="21" ToolTip="Aggiunge un nuovo Serbatoio Autoclave" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaSerbatoio" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();"
                            TabIndex="22" ToolTip="Elimina il Serbatoio Autoclave Selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriSerbatoio" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TabDettagliIDRICO_txtAppareS').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Serbatoi').style.visibility='visible';"
                            TabIndex="23" ToolTip="Modifica il Serbatoio Autoclave Selezionato" Width="60px" /></td>
                </tr>
            </table>
        </td>
    </tr>
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
                height: 60px">
                <asp:DataGrid ID="DataGrid3" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="8px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="24" Width="784px">
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
                        <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POTENZA (KW)">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
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
                        <asp:TemplateColumn HeaderText="DISCONNETTORE">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISCONNETTORE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISCONNETTORE") %>'></asp:Label>
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
        </td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px;">
                        <asp:ImageButton ID="btnAggPompe" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('TabDettagliIDRICO_txtAppareP').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Pompe').style.visibility='visible';"
                            TabIndex="25" ToolTip="Aggiunge una nuova pompa di sollevamento" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaPompa" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();"
                            TabIndex="26" ToolTip="Elimina la pompa di sollevamento selezionata" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnApriPompa" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TabDettagliIDRICO_txtAppareP').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Pompe').style.visibility='visible';"
                            TabIndex="27" ToolTip="Modifica la pompa di sollevamento selezionata" Width="60px" /></td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<div id="DIV_Serbatoi" style="width: 800px; position: absolute; height: 550px; background-color: whitesmoke; left: 0px; top: 0px; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    &nbsp;
    <table style="border-right: blue 2px; border-top: blue 2px; left: 112px;
        border-left: blue 2px; border-bottom: blue 2px; position: absolute;
        top: 96px; background-color: #ffffff; z-index: 102;">
        <tr>
            <td style="width: 525px">
                <strong><span style="color: #0000ff; font-family: Arial">
                    <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="13pt"
                        ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="504px">Gestione Serbatoi</asp:Label></span></strong></td>
        </tr>
        <tr>
            <td style="width: 525px; height: 270px;">
                <table id="Table3">
                        <tr>
                            <td>
                                <asp:Label ID="lbl_MarcaModelloS" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Marca/Modello *</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtModelloS" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="200"
                                    Style="left: 184px; top: 80px" TabIndex="28" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_MatricolaS" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Matricola/N° Fabbrica</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtMatricolaS" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="30" Style="left: 72px; top: 224px" TabIndex="29" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 68px">
                                <asp:Label ID="lbl_CaratteristicheS" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Caratteristiche Tecniche</asp:Label></td>
                            <td style="height: 68px">
                                <asp:TextBox ID="txtNoteS" runat="server" Font-Names="Arial" Font-Size="9pt" Height="64px"
                                    MaxLength="300" Style="left: 144px; top: 112px" TabIndex="30" TextMode="MultiLine"
                                    Width="400px"></asp:TextBox></td>
                        </tr>
                    </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_AnnoCostruzioneS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Anno di Costruzione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtAnnoRealizzazioneS" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="4" Style="left: 144px; top: 192px" TabIndex="31" ToolTip="aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAnnoRealizzazioneS"
                                Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                Width="110px"></asp:RegularExpressionValidator></td>
                        <td style="width: 16px">
                            &nbsp; &nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lbl_VolumeS" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="80px">Volume</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtVolumeS" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="33"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lbl_litri" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="1px"> (litri)</asp:Label>&nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtVolumeS"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_PressioneBollaS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Pressione Bolla</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPressioneBollaS" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                TabIndex="32" Width="100px"></asp:TextBox>&nbsp;
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (ate)</asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPressioneBollaS"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        <td style="width: 16px">
                            &nbsp; &nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <asp:Label ID="lbl_PressioneEsercizioS" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px" Visible="False">Pressione Esercizio</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPressioneEsercizioS" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                TabIndex="34" Width="100px" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px" Visible="False">(ate)</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPressioneEsercizioS"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDS" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox>
                <asp:TextBox ID="txtTipoSerbatoi" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 525px;">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 68%">
                    <tr>
                        <td align="right" style="height: 2px; width: 357px;">
                            <asp:ImageButton ID="btn_InserisciSerbatoio" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('TabDettagliIDRICO_txtAppareS').value='0';"
                                Style="cursor: pointer" TabIndex="35" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiSerbatoio" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Serbatoi').style.visibility='hidden';document.getElementById('TabDettagliIDRICO_txtAppareS').value='0';"
                                    Style="cursor: pointer" TabIndex="36" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="400px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="592px" />
</div>
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
                                    TextMode="MultiLine" Width="400px" TabIndex="37"></asp:TextBox></td>
                            <td style="width: 3px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 79px; height: 33px">
                                <asp:Label ID="lblMatricolaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Matricola</asp:Label></td>
                            <td style="width: 156px; height: 33px">
                                <asp:TextBox ID="txtMatricolaP" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="30" Style="left: 72px; top: 224px" TabIndex="38" Width="400px"></asp:TextBox></td>
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
                                MaxLength="4" Style="left: 144px; top: 192px" TabIndex="39" ToolTip="aaaa" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAnnoRealizzazioneP"
                                Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                Width="110px"></asp:RegularExpressionValidator></td>
                        <td>
                            </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPotenzaPompe" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Potenza Elettrica</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPotenzaP" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="40"
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
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="42"
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
                                TabIndex="41" Width="100px"></asp:TextBox>
                            <asp:Label ID="lblm" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (m)</asp:Label><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtPrevalenzaP"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                    Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblDisconnettore" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="90px">Disconnettore</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbDisconnettore" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="43" ToolTip="Decreto Prefettizio Serbatoi" Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
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
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('TabDettagliIDRICO_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="44" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_ChiudiPompe" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Pompe').style.visibility='hidden';document.getElementById('TabDettagliIDRICO_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="45" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image2" runat="server" BackColor="White" Height="360px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="616px" />
</div>

<asp:TextBox ID="txtAppareS"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareP"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('TabDettagliIDRICO_txtAppareS').value!='1') {
document.getElementById('DIV_Serbatoi').style.visibility='hidden';
}

if (document.getElementById('TabDettagliIDRICO_txtAppareP').value!='1') {
document.getElementById('DIV_Pompe').style.visibility='hidden';
}
</script>


