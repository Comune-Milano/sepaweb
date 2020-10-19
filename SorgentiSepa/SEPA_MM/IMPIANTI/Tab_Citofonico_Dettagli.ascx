<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Citofonico_Dettagli.ascx.vb" Inherits="Tab_Citofonico_Dettagli" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="lblCitofoni" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO CITOFONI/VIDEOCITOFONI"
                Width="368px"></asp:Label></td>
        <td style="width: 15px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 270px">
                <asp:DataGrid ID="DataGridCI" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="16px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="10" Width="680px">
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
                        Font-Underline="False" Wrap="False" /><Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TASTIERA">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TASTIERA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TASTIERA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DISTRIBUZIONE" HeaderText="DISTRIBUZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_TIPO_DISTRIBUZIONE" HeaderText="ID_TIPO_DISTRIBUZIONE"
                                Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="QUANTITA'">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SCALE SERVITE">
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
            <asp:TextBox ID="txtSelCI" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td style="width: 15px">
            &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggCI" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Citofonico_Dettagli_txtAppareCI').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_CI').style.visibility='visible';"
                            TabIndex="11" ToolTip="Aggiunge un nuovo citofono o videocitofono" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaCI" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloCI();"
                            TabIndex="12" ToolTip="Elimina il citofono o videocitofono selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriCI" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Citofonico_Dettagli_txtAppareCI').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_CI').style.visibility='visible';"
                            TabIndex="13" ToolTip="Modifica il citofono o videocitofono selezionato" Width="60px" /></td>
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
        </td>
    </tr>
</table>

<div id="DIV_CI" style="width: 800px; height: 550px; background-color: #dedede; left: 0px; position: absolute; top: 0px; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);" >
    <table style="left: 130px; width: 45%; position: absolute;
        top: 90px; height: 248px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td style="width: 431px">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Citofoni/Videocitofoni</span></strong></td>
        </tr>
        <tr>
            <td style="width: 431px; height: 309px">
                <table style="width: 300px">
                    <tr>
                        <td style="width: 153px">
                            <asp:Label ID="lblTipo" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="150px">Tipologia *</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbTipo" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="14" ToolTip="Protezione Differenziale" Width="200px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="ANALOGICO">ANALOGICO</asp:ListItem>
                                <asp:ListItem>DIGITALE</asp:ListItem>
                                <asp:ListItem Value="VIDEOCITOFONO">VIDEOCITOFONO</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 17px; width: 153px;">
                            <asp:Label ID="lblUbicazione" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="150px">Ubicazione *</asp:Label></td>
                        <td style="height: 17px"><asp:DropDownList ID="cmbUbicazione" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="15" ToolTip="Protezione Differenziale" Width="200px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="PIEDE DI STRADA">PIEDE DI STRADA</asp:ListItem>
                            <asp:ListItem Value="SCALA">SCALA</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; width: 153px;">
                            <asp:Label ID="lblTastiera" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="150px">Tipologia Tastiera</asp:Label></td>
                        <td style="height: 24px"><asp:DropDownList ID="cmbTastiera" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="16" ToolTip="Tipologia Tastiera" Width="200px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="DIGITALE">DIGITALE</asp:ListItem>
                            <asp:ListItem Value="ANALOGICO">ANALOGICO</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 153px; height: 21px">
                            <asp:Label ID="lblDistribuzioneP" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="150px">Distribuzione</asp:Label></td>
                        <td style="height: 21px">
                            <asp:DropDownList ID="cmbDistribuzioneC" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 56px" TabIndex="17" ToolTip="Distribuzione dell'impianto" Width="200px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; width: 153px;">
                            <asp:Label ID="lblNumero" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="150px">Numero Citofoni/Video Citofono</asp:Label></td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtQuantita" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="18"
                                Width="100px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                    runat="server" ControlToValidate="txtQuantita" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                    Font-Names="Arial" Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1"
                                    ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <table style="width: 300px">
                    <tr>
                        <td>
                            <asp:Label ID="lblScale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="80px">Scale Servite:</asp:Label></td>
                        <td>
                            <div id="DIV1"  style="border-right: navy 1px solid;
                                border-top: navy 1px solid; overflow: auto; border-left: navy 1px solid; width: 300px;
                                border-bottom: navy 1px solid; height: 152px">
                                <asp:CheckBoxList ID="CheckBoxScale" runat="server" BorderColor="Black" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Height="104px" TabIndex="19" Width="560px">
                                </asp:CheckBoxList></div>
                            <asp:Label ID="lblATTENZIONE" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Red" Style="left: 0px; top: -30px" TabIndex="-1" Text="ATTENZIONE: Scegliere il complesso dalla pagina principale per poter selezionare le scale servite"
                                Width="300px"></asp:Label></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDCI" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 431px">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciCI" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Citofonico_Dettagli_txtAppareCI').value='0';"
                                Style="cursor: pointer" TabIndex="20" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiCI" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_CI').style.visibility='hidden';document.getElementById('Tab_Citofonico_Dettagli_txtAppareCI').value='0';"
                                    Style="cursor: pointer" TabIndex="21" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="488px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 100px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 60px" Width="456px" />
</div>

<asp:TextBox ID="txtAppareCA"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareCI"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">


if (document.getElementById('Tab_Citofonico_Dettagli_txtAppareCI').value!='1') {
document.getElementById('DIV_CI').style.visibility='hidden';
}

</script>
