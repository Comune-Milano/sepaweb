<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_TV_Dettagli.ascx.vb" Inherits="Tab_TV_Dettagli" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="LabelTitoloCitofonico" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO IMPIANTI TV CENTRALIZZATI"
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
                height: 280px">
                <asp:DataGrid ID="DataGridTV" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="8px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="9" Width="872px">
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
                        <asp:TemplateColumn HeaderText="ID_UBICAZIONE_EDIFICIO" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_EDIFICIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_EDIFICIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID_UBICAZIONE_SCALA" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_SCALA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_SCALA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="EDIFICIO" HeaderText="Ubicazione Centralino (Edifcio)">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SCALA" HeaderText="Ubicazione Centralino (Scala)"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DITTA_INSTALLAZIONE" HeaderText="Ditta Installatrice"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_INSTALLAZIONE" HeaderText="Data Installazione"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Centralino TV">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CENTRALINO_TV") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CENTRALINO_TV") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Impianto">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPIANTO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IMPIANTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tipo Impianto">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_IMPIANTO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_IMPIANTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Distribuzione">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISTRIBUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DISTRIBUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID_TIPO_DISTRIBUZIONE" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_TIPO_DISTRIBUZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_TIPO_DISTRIBUZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Num. Fabbricati Serviti">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABB_SERVITI") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FABB_SERVITI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Note">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
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
            <asp:TextBox ID="txtSelTV" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggTV" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_TV_Dettagli_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_TV').style.visibility='visible';document.getElementById('DIV_FabbTV').style.visibility='visible';"
                            TabIndex="10" ToolTip="Aggiunge un nuovo Impianto TV" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaTV" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloTV();"
                            TabIndex="11" ToolTip="Elimina l'impianto TV selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriTV" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_TV_Dettagli_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_TV').style.visibility='visible'; document.getElementById('DIV_FabbTV').style.visibility='visible';"
                            TabIndex="12" ToolTip="Modifica l'impianto TV selezionato" Width="60px" /></td>
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
<div id="DIV_TV" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: #dedede; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table id="TABBLE_TV" style="left: 56px;
        position: absolute; top: 80px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td style="height: 21px">
                <strong><span style="color: #0000ff; font-family: Arial">Dettagli Impianto TV</span></strong></td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblFabServiti" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="left: 0px; top: -30px" TabIndex="-1"
                                Text="Fabbricati Serviti" Width="100px"></asp:Label></td>
                        <td>
                            <div id="DIV_FabbTV" style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid;
                                overflow: auto; border-left: midnightblue 1px solid; width: 520px; border-bottom: midnightblue 1px solid;
                                position: static; height: 120px">
                                <asp:CheckBoxList ID="CheckBoxFabb" runat="server" BorderColor="Black" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Height="90%" Style="background-color: #ffffff"
                                    TabIndex="13" Visible="False" Width="98%">
                                </asp:CheckBoxList>
                                <asp:Label ID="lblATTENZIONETV" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Red" Style="left: 0px; top: -30px" TabIndex="-1" Text="ATTENZIONE: Scegliere il complesso dalla pagina principale per poter selezionare i fabbricati serviti e l'ubicazione del centralino"
                                    Width="512px"></asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUbicazioneFabbTV" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="100px">Ubicazione Centralino *</asp:Label></td>
                        <td>
                            <table style="width: 320px">
                                <tr>
                                    <td style="width: 363px">
                            <asp:DropDownList ID="cmbEdificioTV" runat="server" AutoPostBack="True" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 24px" TabIndex="14" Width="360px" ToolTip="Edificio">
                            </asp:DropDownList></td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="60px">Scala: *</asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="cmbScalaTV" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 24px" TabIndex="15" Width="80px" ToolTip="Scala">
                            </asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDitta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Ditta Installatrice</asp:Label></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDitta" runat="server" Font-Names="Arial" Font-Size="9pt"
                                            Height="40px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="16" TextMode="MultiLine"
                                            Width="360px"></asp:TextBox></td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                            <asp:Label ID="lblDataTV" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="60px">Data Installazione</asp:Label></td>
                                    <td>
                            <asp:TextBox ID="txtDataTV" runat="server" Font-Names="Arial" Font-Size="9pt" Style="left: 504px;
                                top: 152px" TabIndex="17" ToolTip="gg/mm/aaaa" Width="80px"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataTV"
                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" Width="50px"></asp:RegularExpressionValidator></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCentralinoTV" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="100px">Centralino TV</asp:Label></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                            <asp:DropDownList ID="cmbTipoCentralinoTV" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="18" ToolTip="Tipo Centralino TV" Width="200px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="MULTI BANDA">MULTI BANDA</asp:ListItem>
                                <asp:ListItem Value="MODULARE">MODULARE</asp:ListItem>
                                <asp:ListItem>MISTO</asp:ListItem>
                            </asp:DropDownList></td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                            <asp:Label ID="lblSatellitareTV" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="100px">Impianto</asp:Label></td>
                                    <td>
                            <asp:DropDownList ID="cmbImpianto" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="19" ToolTip="Tipologia Impianto TV" Width="200px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="ANALOGICO">ANALOGICO</asp:ListItem>
                                <asp:ListItem Value="DIGITALE TERRESTRE">DIGITALE TERRESTRE</asp:ListItem>
                                <asp:ListItem>SATELLITARE</asp:ListItem>
                            </asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoImpiantoTV" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="100px">Tipo Impianto</asp:Label></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                            <asp:DropDownList ID="cmbTipoImpiantoTV" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="20" ToolTip="Tipo Impianto" Width="200px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="SERIE">SERIE</asp:ListItem>
                                <asp:ListItem Value="DERIVAZIONE">DERIVAZIONE</asp:ListItem>
                            </asp:DropDownList></td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                            <asp:Label ID="lblDistribuzioneATV" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="100px">Distribuzione</asp:Label></td>
                                    <td>
                            <asp:DropDownList ID="cmbDistribuzioneTV" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 56px" TabIndex="21" ToolTip="Distribuzione dell'impianto nell'alloggio"
                                Width="200px">
                            </asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_NoteTV" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Note</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtNoteTV" runat="server" Font-Names="Arial" Font-Size="9pt" Height="50px"
                                MaxLength="300" Style="left: 144px; top: 112px" TabIndex="22" TextMode="MultiLine"
                                Width="520px"></asp:TextBox></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDTV" runat="server" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btn_InserisciTV" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_TV_Dettagli_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="23" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiTV" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_TV').style.visibility='hidden';document.getElementById('DIV_FabbTV').style.visibility='hidden';document.getElementById('Tab_TV_Dettagli_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="24" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="472px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 30px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 60px" Width="750px" />
</div>

<asp:TextBox ID="txtAppare"         runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_TV_Dettagli_txtAppare').value!='1') {
document.getElementById('DIV_TV').style.visibility='hidden';
document.getElementById('DIV_FabbTV').style.visibility='hidden';
}
</script>