<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Antincendio_Dettagli.ascx.vb" Inherits="Tab_Antincendio_Dettagli" %>
<table id="TABBLE_LISTA">
    <tr>
        <td style="height: 21px">
            <asp:Label ID="lblMotopompe" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO MOTOPOMPE UNI 70"
                Width="368px"></asp:Label></td>
        <td style="height: 21px">
        </td>
        <td style="height: 21px">
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 100px">
                <asp:DataGrid ID="DataGridMotopompa" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="13" Width="736px">
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
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_EDIFICIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_EDIFICIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID_UBICAZIONE_SCALA" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_SCALA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_SCALA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="EDIFICIO" HeaderText="Ubicazione Serbatoio (Edifcio)"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SCALA" HeaderText="Ubicazione Serbatoio (Scala)"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Num. Scale Servite">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALE_SERVITE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALE_SERVITE") %>'></asp:Label>
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
            <asp:TextBox ID="txtSelMotopompa" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
        </td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggMotopompa" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Antincendio_Dettagli_txtAppareM').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_MOTOPOMPA').style.visibility='visible';"
                            TabIndex="14" ToolTip="Aggiunge una nuova Motopompa UNI 70" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaMotopompa" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloMotopompa();"
                            TabIndex="16" ToolTip="Elimina la motopompa selezionata" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriMotopompa" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Antincendio_Dettagli_txtAppareM').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_MOTOPOMPA').style.visibility='visible'; "
                            TabIndex="18" ToolTip="Modifica la motopompa selezionata" Width="60px" /></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="LabelTitoloCitofonico" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO SERBATOI ACCUMULO"
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
                height: 100px">
                <asp:DataGrid ID="DataGridSerbatoio" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="17" Width="728px">
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
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_EDIFICIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_EDIFICIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ID_UBICAZIONE_SCALA" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_SCALA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UBICAZIONE_SCALA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="EDIFICIO" HeaderText="Ubicazione Serbatoio (Edifcio)">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SCALA" HeaderText="Ubicazione Serbatoio (Scala)"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Capacit&#224;">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAPACITA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAPACITA") %>'></asp:Label>
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
            <asp:TextBox ID="txtSelSerbatoio" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggSerbatoio" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Antincendio_Dettagli_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_SERBATOIO').style.visibility='visible';"
                            TabIndex="18" ToolTip="Aggiunge un nuovo Serbatoio Accumulo" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaSerbatoio" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloSerbatoio();"
                            TabIndex="19" ToolTip="Elimina il serbatoio selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriSerbatoio" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Antincendio_Dettagli_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_SERBATOIO').style.visibility='visible'; "
                            TabIndex="20" ToolTip="Modifica il serbatoio selezionato" Width="60px" /></td>
                </tr>
            </table>
            <br />
            <br />
        </td>
    </tr>
</table>
<div id="DIV_SERBATOIO" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: #dedede; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table id="TABBLE_SERBATOIO" style="left: 130px; width: 45%;
        position: absolute; top: 100px; height: 248px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Serbatoio Accumulo</span></strong></td>
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Navy" Style="z-index: 100; left: 24px;
                    top: 32px" Width="110px">Ubicazione Serbatoio:</asp:Label></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 208px">
                    <tr>
                        <td style="height: 24px">
                            <asp:Label ID="lblUbicazioneFabb" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Fabbricato *</asp:Label></td>
                        <td style="height: 24px; width: 403px;">
                            <asp:DropDownList ID="cmbEdificioSerbatoio" runat="server" AutoPostBack="True" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 24px" TabIndex="26" Width="400px" ToolTip="Edificio">
                            </asp:DropDownList></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px">
                            <asp:Label ID="lblUbicazioneScala" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="80px">Scala *</asp:Label></td>
                        <td style="width: 403px; height: 24px">
                            <asp:DropDownList ID="cmbScalaSerbatoio" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 24px" TabIndex="27" Width="120px" ToolTip="Scala">
                            </asp:DropDownList></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 16px">
                            <asp:Label ID="lblCapacita" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Capacità</asp:Label></td>
                        <td style="height: 16px; width: 403px;">
                            <asp:TextBox ID="txtCapacita" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="28"
                                Width="100px"></asp:TextBox>
                            <asp:Label ID="lblm3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px">(litri)</asp:Label>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCapacita"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        <td style="height: 16px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px">
                            </td>
                        <td style="height: 17px; width: 403px;">
                            <asp:Label ID="lblATTENZIONE" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Red" Style="left: 0px; top: -30px" TabIndex="-1" Text="ATTENZIONE: Scegliere il complesso dalla pagina principale per poter selezionare l'ubicazione del serbatoio"
                                Width="400px"></asp:Label></td>
                        <td style="height: 17px">
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtID" runat="server" Height="16px" Style="left: 640px; top: 200px"
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
                            <asp:ImageButton ID="btn_Inserisci" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Antincendio_Dettagli_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="29" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_Chiudi" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_SERBATOIO').style.visibility='hidden';;document.getElementById('Tab_Antincendio_Dettagli_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="30" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="320px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 88px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 64px" Width="584px" />
</div>
<div id="DIV_MOTOPOMPA" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: #dedede; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table id="TABBLE_MOTOPOMPA" style="left: 130px; width: 45%;
        position: absolute; top: 100px; height: 248px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Motopompa UNI 70</span></strong></td>
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblUbicazioneM" runat="server" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Size="8pt" ForeColor="Navy" Style="z-index: 100; left: 24px;
                    top: 32px" Width="176px">Ubicazione Motopompa:</asp:Label></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 208px">
                    <tr>
                        <td style="height: 24px;">
                            <asp:Label ID="lblUbicazioneFabbM" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Fabbricato *</asp:Label></td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="cmbEdificioMotopompa" runat="server" AutoPostBack="True" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 24px" TabIndex="21" Width="400px" ToolTip="Edificio">
                            </asp:DropDownList></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px">
                            <asp:Label ID="lblUbicazioneScalaM" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Scala *</asp:Label></td>
                        <td style="height: 24px">
                            <asp:DropDownList ID="cmbScalaMotopompa" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 24px" TabIndex="22" Width="120px" ToolTip="Scala">
                            </asp:DropDownList></td>
                        <td style="height: 24px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblScaleServite" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Scale Servite:</asp:Label></td>
                        <td>
                            <div style="border-right: navy 1px solid; border-top: navy 1px solid; overflow: auto;
                                border-left: navy 1px solid; width: 400px; border-bottom: navy 1px solid; height: 150px;" id="DIV1" >
                            <asp:CheckBoxList ID="CheckBoxScale" runat="server" BorderColor="Black" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Height="120px" TabIndex="23" Width="380px">
                            </asp:CheckBoxList></div>
                            <asp:Label ID="lblATTENZIONE_M" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Red" Style="left: 0px; top: -30px" TabIndex="-1" Text="ATTENZIONE: Scegliere l'edificio dalla pagina principale per poter selezionare le scale servite e l'ubicazione della motopompa"
                                Width="400px"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 17px;">
                        </td>
                        <td style="height: 17px">
                        </td>
                        <td style="height: 17px">
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDM" runat="server" Height="16px" Style="left: 640px; top: 200px"
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
                            <asp:ImageButton ID="btn_InserisciM" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Antincendio_Dettagli_txtAppareM').value='0';"
                                Style="cursor: pointer" TabIndex="24" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton
                                    ID="btn_ChiudiM" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_MOTOPOMPA').style.visibility='hidden';;document.getElementById('Tab_Antincendio_Dettagli_txtAppareM').value='0';"
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
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="440px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 88px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 64px" Width="584px" />
</div>


<asp:TextBox ID="txtAppare"         runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareM"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_Antincendio_Dettagli_txtAppare').value!='1') {
document.getElementById('DIV_SERBATOIO').style.visibility='hidden';
}

if (document.getElementById('Tab_Antincendio_Dettagli_txtAppareM').value!='1') {
document.getElementById('DIV_MOTOPOMPA').style.visibility='hidden';
}
</script>