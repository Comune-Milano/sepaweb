<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Tutela_Verifiche2.ascx.vb" Inherits="Tab_Tutela_Verifiche2" %>
<table id="TABBLE_LISTA">
    <tr>
        <td style="height: 21px">
            <asp:Label ID="lblTitoloGrid1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="VERIFICHE ANNUALI CHIUSURA/INGRESSO SCALE"
                Width="648px" Height="15px"></asp:Label></td>
        <td style="height: 21px">
        </td>
        <td style="height: 21px">
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 55px"><asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="61" Width="680px">
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
                        <asp:BoundColumn DataField="DITTA" HeaderText="DITTA">
                            <HeaderStyle Width="25%" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="DATA">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MOTIVO">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="30%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ESITO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="VALIDITA' (mesi)">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SCADENZA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ESITO" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MESI_PREALLARME" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="0px" />
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
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100" Style="left: 40px;
                top: 200px" TabIndex="-1" Width="680px" ReadOnly="True"></asp:TextBox></td>
        <td>
        </td>
        <td><table style="width: 57%">
            <tr>
                <td style="width: 88px; height: 14px">
                    <asp:ImageButton ID="btnAgg" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Tutela_Verifiche2_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_VERIFICHE2').style.visibility='visible';"
                            TabIndex="61" ToolTip="Aggiunge un Nuova Verifica" /></td>
            </tr>
            <tr>
                <td style="width: 88px; height: 12px">
                    <asp:ImageButton ID="btnApri" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Tutela_Verifiche2_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_VERIFICHE2').style.visibility='visible';"
                            TabIndex="62" ToolTip="Modifica la Verifica Selezionata" Width="60px" /></td>
            </tr>
        </table>
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ARCHIVIO STORICO DELLE VERIFICHE  ANNUALI CHIUSURA/INGRESSO SCALE"
                Width="672px" Height="15px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 150px">
                <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="63" Width="680px">
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
                        <asp:BoundColumn DataField="DITTA" HeaderText="DITTA">
                            <HeaderStyle Width="25%" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="DATA">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MOTIVO">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="30%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ESITO">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO_DETTAGLIO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="VALIDITA' (mesi)">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_VALIDITA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SCADENZA">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_SCADENZA") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ESITO" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ESITO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MESI_PREALLARME" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MESI_PREALLARME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="0px" />
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
                            <HeaderStyle Width="0px" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSel2" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
        </td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnVisualizza" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
                            OnClientClick="document.getElementById('Tab_Tutela_Verifiche2_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_VERIFICHE2').style.visibility='visible';"
                            TabIndex="64" ToolTip="Visulizza la Verifica Selezionata" /></td>
                </tr>
            <tr>
                <td style="width: 88px">
                    <asp:ImageButton ID="btnElimina" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloVerifiche2();"
                            TabIndex="65" ToolTip="Elimina la Verifica Selezionata" Width="60px" /></td>
            </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>
&nbsp;

<div id="DIV_VERIFICHE2" style="width: 800px; height: 550px; display: block; left: 0px; position: absolute; top: 0px; background-color: #dedede; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <br /><table id="Table4" style="left: 130px; width: 50px;
        position: absolute; top: 100px; height: 248px; background-color: #ffffff; color: #0000ff; font-family: Arial; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;" >
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Verifica Annuale CHIUSURA/INGRESSO
                    Scale</span></strong></td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <table id="Table2">
                    <tr>
                        <td>
                            <asp:Label ID="lblDitta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Ditta *</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDitta" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="100"
                                TextMode="MultiLine" Width="400px" TabIndex="66"></asp:TextBox></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Osservazioni</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="64px"
                                MaxLength="4000" Style="left: 144px; top: 112px" TabIndex="67" TextMode="MultiLine"
                                Width="400px"></asp:TextBox></td>
                        <td>
                            &nbsp;<br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px">
                            <asp:Label ID="lblEsito" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Esito *</asp:Label></td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="cmbEsito" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="68" ToolTip="Pressione" Width="180px">
                                <asp:ListItem Value="1">FUNZIONA</asp:ListItem>
                                <asp:ListItem Value="0">NON FUNZIONA</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtID" runat="server" Height="16px" Style="left: 640px; top: 200px"
                                TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblData" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Data Verifica *</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtData" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                Style="left: 144px; top: 192px" TabIndex="69" ToolTip="gg/mm/aaaa" Width="70px" AutoPostBack="True"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtData"
                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                        <td style="width: 11px">
                            &nbsp;&nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblValidita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Validità (mesi) *</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtValidita" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                                TabIndex="70" Width="70px" AutoPostBack="True"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px">(mesi)</asp:Label><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                    runat="server" ControlToValidate="txtValidita" Display="Dynamic" ErrorMessage="Valore Numerico Intero"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="303" ValidationExpression="\d+"
                                    Width="120px"></asp:RegularExpressionValidator></td>
                        <td style="width: 11px">
                        </td>
                        <td>
                            <asp:Label ID="lblPreAllarme" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Pre-Allarme *</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbPreAllarme" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="71" ToolTip="Mesi per il pre allarme" Width="56px" AutoPostBack="True">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                            </asp:DropDownList>&nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px">(mesi)</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDataScadenza" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Data Prossima Verifica</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDataScadenza" runat="server" Enabled="False" Font-Names="Arial"
                                Font-Size="9pt" MaxLength="10" Style="left: 144px; top: 192px"
                                TabIndex="-1" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>&nbsp;
                            <asp:Image ID="ImageAllarm" runat="server" BackColor="Silver" BorderColor="Silver" ForeColor="LightGray"
                                ImageUrl="~/IMPIANTI/Immagini/Semaforo_Rosso.png" />&nbsp;
                            </td>
                        <td style="width: 11px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 11px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right">
                            &nbsp;<asp:ImageButton ID="btn_Inserisci" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Tutela_Verifiche2_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="72" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton
                                    ID="btn_Chiudi" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_VERIFICHE2').style.visibility='hidden';document.getElementById('Tab_Tutela_Verifiche2_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="73" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="448px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 100px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="608px" />
</div>

<asp:TextBox ID="txtAppare"         runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente2"  runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>


<script type="text/javascript">

if (document.getElementById('Tab_Tutela_Verifiche2_txtAppare').value!='1') {
document.getElementById('DIV_VERIFICHE2').style.visibility='hidden';
}

</script>