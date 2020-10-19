<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Verifiche.ascx.vb" Inherits="TabVerifiche" %>
<table id="TABBLE_LISTA" style="width: 750px">
    <tr>
        <td style="width: 1606px; height: 21px">
            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO VERIFICHE"
                Width="368px"></asp:Label></td>
        <td style="height: 21px">
        </td>
        <td style="width: 471px; height: 21px">
        </td>
    </tr>
    <tr>
        <td style="width: 1606px; height: 1px">
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 672px; border-bottom: #0000cc thin solid;
                height: 80px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
                    clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="24" Width="816px">
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
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FLUIDO TERMOVETTORE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLUIDO_TERMOVETTORE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLUIDO_TERMOVETTORE") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MARC. EFF. ENERGETICA">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MARC_EFF_ENERGETICA") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MARC_EFF_ENERGETICA") %>'></asp:TextBox>
                            </EditItemTemplate>
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
        </td>
        <td style="height: 1px">
            &nbsp; &nbsp;</td>
        <td style="width: 471px; height: 1px">
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggVerifiche" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('TabVerifiche_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Verifiche').style.visibility='visible';"
                            TabIndex="25" ToolTip="Aggiunge un nuovo Generatore" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaVerifica" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloVerifiche();"
                            TabIndex="26" ToolTip="Elimina il Generatore selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriVerifica" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TabVerifiche_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Verifiche').style.visibility='visible';"
                            TabIndex="27" ToolTip="Modifica il Generatore selezionato" Width="60px" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 1606px; height: 22px">
            <asp:TextBox ID="txtSelVerifica" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="10pt" MaxLength="100" Style="left: 40px;
                top: 200px; height: 20px" TabIndex="-1" Width="400px"></asp:TextBox></td>
        <td style="height: 22px">
        </td>
        <td style="width: 471px; height: 22px">
        </td>
    </tr>
</table>
<br />
<div id="DIV_Verifiche" style="display: block; left: 0px; width: 900px; position: absolute;
    top: 0px; height: 680px; background-color: whitesmoke; text-align: left">
    <span style="font-family: Arial"></span>
    <table id="TABLE1" border="0" cellpadding="1" cellspacing="1" 
        style="border-right: blue 2px solid; border-top: blue 2px solid; left: 50px;
        border-left: blue 2px solid; width: 45%; border-bottom: blue 2px solid; position: absolute;
        top: 80px; height: 248px; background-color: gainsboro">
        <tr>
            <td style="width: 404px; color: #0000ff; height: 19px; text-align: left">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Bruciatori</span></strong></td>
        </tr>
        <tr>
            <td style="width: 404px; height: 114px">
                <span style="color: #ffffff"><span style="font-size: 10pt; font-family: Arial"><strong>
                    &nbsp;<table id="Table2">
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lbl_MarcaModello" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Marca/Modello</asp:Label></td>
                            <td style="width: 403px">
                                <asp:TextBox ID="txtModello" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="200"
                                    Style="left: 184px; top: 80px" TabIndex="32" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px; height: 33px">
                                <asp:Label ID="lbl_Matricola" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Matricola</asp:Label></td>
                            <td style="width: 403px; height: 33px">
                                <asp:TextBox ID="txtMatricola" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="30" Style="left: 72px; top: 224px" TabIndex="33" Width="400px"></asp:TextBox></td>
                            <td style="height: 33px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lbl_Caratteristiche" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Caratteristiche Tecniche</asp:Label></td>
                            <td style="width: 403px">
                                <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="64px"
                                    MaxLength="300" Style="left: 144px; top: 112px" TabIndex="34" TextMode="MultiLine"
                                    Width="400px"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lbl_AnnoCostruzione" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Height="10px" Style="z-index: 100; left: 24px;
                                    top: 32px" TabIndex="-1" Width="110px">Anno di Costruzione</asp:Label></td>
                            <td style="width: 403px">
                                <asp:TextBox ID="txtAnnoRealizzazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="4" Style="left: 144px; top: 192px" TabIndex="35" ToolTip="aaaa" Width="70px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAnnoRealizzazione"
                                    Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                    Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px; height: 30px">
                                <asp:Label ID="lbl_Funzionamento" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Campo di Funzionamento (Kw)</asp:Label></td>
                            <td style="width: 403px; height: 30px">
                                <asp:TextBox ID="txtFunzionamento" runat="server" Font-Names="arial" Font-Size="9pt"
                                    MaxLength="10" Style="z-index: 102; left: 144px; top: 224px" TabIndex="36" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFunzionamento"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                    Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                            <td style="height: 30px">
                            </td>
                        </tr>
                    </table>
                </strong></span></span><span></span>
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="font-size: 12pt; width: 404px; font-family: Times New Roman; height: 19px;
                text-align: left">
                <asp:TextBox ID="txtID" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="width: 404px; height: 17px; text-align: right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciVerifica" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Verifiche').style.visibility='hidden';document.getElementById('TabVerifiche_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="37" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiVerifica" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Verifiche').style.visibility='hidden';document.getElementById('TabVerifiche_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="38" ToolTip="Esci senza salvare" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<asp:TextBox ID="txtAppare" runat="server" Style="left: 944px; position: absolute;
    top: 240px" TabIndex="-1"></asp:TextBox>
<asp:TextBox ID="txtIdComponente" runat="server" Style="left: 944px; position: absolute;
    top: 260px" TabIndex="-1"></asp:TextBox>
<asp:TextBox ID="txtannullo" runat="server" Style="left: 944px; position: absolute;
    top: 296px" TabIndex="-1"></asp:TextBox>


<script type="text/javascript">

if (document.getElementById('TabVerifiche_txtAppare').value!='1') {
document.getElementById('DIV_Verifiche').style.visibility='hidden';
}


</script>