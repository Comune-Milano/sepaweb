<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Dettagli1.ascx.vb" Inherits="TabDettagli1" %>
    <table id="TABBLE_LISTA">
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="ELENCO GENERATORI DI CALORE" Width="368px" TabIndex="-1"></asp:Label></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div style="visibility: visible; overflow: auto; width: 680px; height: 105px; border-right: #0000cc thin solid; border-top: #0000cc thin solid; border-left: #0000cc thin solid; border-bottom: #0000cc thin solid;">
                    <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="table-layout: auto;
                        z-index: 101; left: 8px;
                        clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                        Width="900px" ForeColor="Black" TabIndex="19" Height="8px">
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
                                <HeaderStyle Width="0%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MODELLO" HeaderText="MARCA/MODELLO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="MATRICOLA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="POTENZA (KW)">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POTENZA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="FLUIDO TERMOVETTORE">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLUIDO_TERMOVETTORE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FLUIDO_TERMOVETTORE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="MARC. EFF. ENERGETICA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MARC_EFF_ENERGETICA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MARC_EFF_ENERGETICA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="20%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
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
                <asp:TextBox ID="txtSelGeneratori" runat="server" BackColor="#F2F5F1" BorderColor="White"
                    BorderStyle="None" Font-Names="Arial" Font-Size="9pt" MaxLength="100" Style="left: 40px;
                    top: 200px;" TabIndex="-1" Width="680px" ReadOnly="True" Height="15px"></asp:TextBox></td>
            <td>
                &nbsp;&nbsp;&nbsp;</td>
            <td>
                <table style="width: 57%;">
                    <tr>
                        <td style="width: 88px; height: 14px;">
                            <asp:ImageButton ID="btnAggGeneratore" runat="server" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                                ToolTip="Aggiunge un nuovo Generatore" OnClientClick="document.getElementById('TabDettagli1_txtAppareG').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Generatori').style.visibility='visible';" CausesValidation="False" TabIndex="22" /></td>
                    </tr>
                    <tr>
                        <td style="width: 88px;">
                            <asp:ImageButton ID="btnEliminaGeneratore" runat="server" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                                ToolTip="Elimina il Generatore selezionato" OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();" Height="12px" Width="60px" TabIndex="23" /></td>
                    </tr>
                    <tr>
                        <td style="width: 88px; height: 12px;">
                            <asp:ImageButton ID="btnApriGeneratore" runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                                ToolTip="Modifica il Generatore selezionato" CausesValidation="False" OnClientClick="document.getElementById('TabDettagli1_txtAppareG').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Generatori').style.visibility='visible';" Height="12px" Width="60px" TabIndex="24" /></td>
                    </tr>
                </table>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="ELENCO BRUCIATORI" Width="248px" TabIndex="-1"></asp:Label></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <div style="visibility: visible; overflow: auto; width: 680px; height: 105px; border-right: #0000cc thin solid; border-top: #0000cc thin solid; border-left: #0000cc thin solid; border-bottom: #0000cc thin solid;">
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="table-layout: auto;
                        z-index: 101; left: 8px;
                        clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                        Width="900px" ForeColor="Black" TabIndex="20" Height="1px">
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
                                <HeaderStyle Width="0%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MODELLO" HeaderText="MARCA/MODELLO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="MATRICOLA">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MATRICOLA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ANNO COSTRUZ.">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANNO_COSTRUZIONE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="FUNZIONAMENTO Min (KW)">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAMPO_FUNZIONAMENTO") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAMPO_FUNZIONAMENTO") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="20%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="FUNZIONAMENTO Max (KW)">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAMPO_FUNZIONAMENTO_MAX") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CAMPO_FUNZIONAMENTO_MAX") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
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
            <asp:TextBox ID="txtmia" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="9pt" MaxLength="100" Style="left: 40px;
                top: 200px;" Width="680px" TabIndex="-1" ReadOnly="True" Height="15px"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
                <table style="width: 57%;">
                    <tr>
                        <td style="width: 88px; height: 14px;">
                            <asp:ImageButton ID="btnAggBruciatore" runat="server" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                                ToolTip="Aggiunge un nuovo Bruciatore" OnClientClick="document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Bruciatori').style.visibility='visible';" CausesValidation="False" TabIndex="25" /></td>
                    </tr>
                    <tr>
                        <td style="width: 88px;">
                            <asp:ImageButton ID="btnEliminaBruciatore" runat="server" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                                ToolTip="Elimina il Bruciatore selezionato" OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnullo();" Height="12px" Width="60px" TabIndex="26" /></td>
                    </tr>
                    <tr>
                        <td style="width: 88px; height: 14px;">
                            <asp:ImageButton ID="btnApriBruciatore" runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                                ToolTip="Modifica il Bruciatore selezionato" CausesValidation="False" OnClientClick="document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Bruciatori').style.visibility='visible';" Height="12px" Width="60px" TabIndex="27" /></td>
                    </tr>
                </table>
                <br />
                <br />
            </td>
        </tr>
    </table>

<div id="DIV_Bruciatori" style="display: block; left: 0px; width: 800px;
    position: absolute; top: 0px; height: 550px; background-color: whitesmoke; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <span style="font-family: Arial"></span>&nbsp;
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="408px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 88px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 72px" Width="608px" />
    <table id="TABLE1" border="0" cellpadding="1" cellspacing="1"
        style="border-right: blue 2px; border-top: blue 2px; left: 120px; border-left: blue 2px; border-bottom: blue 2px; position: absolute; top: 96px;
        background-color: #ffffff; z-index: 101;">
        <tr>
            <td style="width: 404px; text-align: left; color: #0000ff;">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Bruciatori</span></strong></td>
        </tr>
        <tr>
            <td style="width: 404px; height: 114px">
                <span style="color: #ffffff"><span style="font-size: 10pt; font-family: Arial"><strong>
                    &nbsp;<table id="Table2">
        <tr>
            <td>
                <asp:Label ID="lbl_MarcaModello" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px;
                    top: 32px" Width="120px">Marca/Modello *</asp:Label></td>
            <td>
                    <asp:TextBox ID="txtModello" runat="server" Font-Names="Arial" Font-Size="9pt"
                        MaxLength="200" Style="left: 184px; top: 80px"
                        TextMode="MultiLine" Width="400px" TabIndex="32"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Matricola" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px;
                    top: 32px" Width="120px">Matricola</asp:Label></td>
            <td>
                <asp:TextBox ID="txtMatricola" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="30" Style="left: 72px; top: 224px" Width="400px" TabIndex="33"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_Caratteristiche" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px;
                    top: 32px" Width="120px">Caratteristiche Tecniche</asp:Label></td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Height="64px" MaxLength="300" Style="left: 144px; top: 112px" TabIndex="34" TextMode="MultiLine"
                    Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_AnnoCostruzione" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Height="10px" Style="z-index: 100; left: 24px;
                    top: 32px" Width="120px" TabIndex="-1">Anno di Costruzione</asp:Label></td>
            <td>
                <asp:TextBox ID="txtAnnoRealizzazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Style="left: 144px; top: 192px" ToolTip="Inserire l'anno (aaaa)" Width="70px" MaxLength="4" TabIndex="35"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAnnoRealizzazione"
                    Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False"
                    Font-Names="arial" Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1"
                    ValidationExpression="^\d{4}$"
                    Width="140px"></asp:RegularExpressionValidator></td>
        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_Funzionamento" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="120px">Campo di Funzionamento Minimo</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFunzionamento" runat="server" Font-Names="arial" Font-Size="9pt"
                                    MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="36" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (Kw)</asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFunzionamento"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                    Font-Size="8pt" Style="left: 224px; top: 232px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                    Width="80px" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_FunzionamentoMax" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="120px">Campo di Funzionamento Massimo</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFunzionamentoMax" runat="server" Font-Names="arial" Font-Size="9pt"
                                    MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                    TabIndex="37" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (Kw)</asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFunzionamentoMax"
                                    Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                    Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                    Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
    </table>
                </strong></span></span><span></span>
            </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="font-size: 12pt; width: 404px; font-family: Times New Roman;
                text-align: left">

<asp:TextBox ID="txtID"             runat="server" Style="left: 8px; position: absolute;top: 248px" Visible="False" Width="1px" TabIndex="-1" Height="8px"></asp:TextBox>
                </td>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="width: 404px; text-align: right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciBruciatori" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('TabDettagli1_txtAppare').value='0';"
                                Style="cursor: pointer" ToolTip="Salva le modifiche apportate" TabIndex="38" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiBruciatori" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Bruciatori').style.visibility='hidden';document.getElementById('TabDettagli1_txtAppare').value='0';"
                                    Style="cursor: pointer" ToolTip="Esci senza salvare" TabIndex="39" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="DIV_Generatori" style="display: block; left: 0px; width: 800px;
    position: absolute; top: 0px; height: 550px; background-color: whitesmoke; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <span style="font-family: Arial"></span>
    <table id="Table4" border="0" cellpadding="1" cellspacing="1" "
        style="border-right: blue 2px; border-top: blue 2px; left: 120px; border-left: blue 2px;
        width: 45%; border-bottom: blue 2px; position: absolute; top: 100px; height: 248px;
        background-color: #ffffff; z-index: 102;">
        <tr>
            <td style="width: 404px; height: 18px; text-align: left">
                <strong><span style="font-family: Arial; color: #0000ff;">Gestione Generatori</span></strong></td>
        </tr>
        <tr>
            <td style="width: 404px; height: 114px">
                <span style="color: #ffffff"><span style="font-size: 10pt; font-family: Arial"><strong>
                    &nbsp;<table id="Table3">
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lbl_MarcaModelloG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Marca/Modello *</asp:Label></td>
                            <td style="width: 156px">
                                <asp:TextBox ID="txtModelloG" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="200"
                                    Style="left: 184px; top: 80px" TextMode="MultiLine" Width="400px" TabIndex="39"></asp:TextBox></td>
                            <td style="width: 3px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px; height: 33px;">
                                <asp:Label ID="lbl_MatricolaG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Matricola</asp:Label></td>
                            <td style="width: 156px; height: 33px;">
                                <asp:TextBox ID="txtMatricolaG" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="30"
                                    Style="left: 72px; top: 224px" TabIndex="40" Width="400px"></asp:TextBox></td>
                            <td style="height: 33px; width: 3px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 85px">
                                <asp:Label ID="lbl_CaratteristicheG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Caratteristiche Tecniche</asp:Label></td>
                            <td style="width: 156px">
                                <asp:TextBox ID="txtNoteG" runat="server" Font-Names="Arial" Font-Size="9pt" Height="64px"
                                    MaxLength="300" Style="left: 144px; top: 112px" TabIndex="41" TextMode="MultiLine"
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
                        <td style="width: 52px">
                            <asp:Label ID="lbl_AnnoCostruzioneG" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Anno di Costruzione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtAnnoRealizzazioneG" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="4"
                                Style="left: 144px; top: 192px" TabIndex="42" ToolTip="aaaa" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAnnoRealizzazioneG"
                                Display="Dynamic" ErrorMessage="Inserire l'anno (aaaa)" Font-Bold="False" Font-Names="arial"
                                Font-Size="8pt" Style="left: 224px; top: 200px" TabIndex="-1" ValidationExpression="^\d{4}$"
                                Width="110px"></asp:RegularExpressionValidator></td>
                        <td style="width: 61px">
                            <asp:Label ID="lbl_Potenza" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="80px">Potenza termica utile nominale</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPotenza" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="43" Width="100px"></asp:TextBox>
                            <asp:Label ID="lbl_PotenzaKW" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="1px"> (Kw)</asp:Label>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPotenza"
                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                Font-Size="8pt" Style="left: 224px; top: 232px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                Width="80px" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 52px">
                            <asp:Label ID="lbl_Marcatura" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Marcatura efficienza energetica (D.P.R. n. 660/1996)</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbMarcatura" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="44" ToolTip="Marcatura efficienza energetica (D.P.R. n. 660/1996)"
                                Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 61px">
                            <asp:Label ID="lbl_Fluido" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="80px">Fluido termovettore</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFluido" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50"
                                Style="left: 72px; top: 224px" TabIndex="45" Width="200px"></asp:TextBox></td>
                    </tr>
                </table>                
<asp:TextBox ID="txtIDG"            runat="server" Style="left: 8px; position: absolute;top: 280px" Visible="False" TabIndex="-1" Width="1px" Height="8px"></asp:TextBox>
        </tr>
        <tr style="font-size: 12pt; font-family: Times New Roman">
            <td style="width: 404px; height: 38px; text-align: right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciGeneratori" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('TabDettagli1_txtAppareG').value='0';"
                                Style="cursor: pointer" ToolTip="Salva le modifiche apportate" TabIndex="46" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiGeneratori" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Generatori').style.visibility='hidden';document.getElementById('TabDettagli1_txtAppareG').value='0';"
                                    Style="cursor: pointer" ToolTip="Esci senza inserire" TabIndex="47" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="424px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="616px" />
</div>
&nbsp;
<asp:TextBox ID="txtAppare"         runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareG"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>             

<script type="text/javascript">
if (document.getElementById('TabDettagli1_txtAppare').value!='1') {
document.getElementById('DIV_Bruciatori').style.visibility='hidden';
}

if (document.getElementById('TabDettagli1_txtAppareG').value!='1') {
document.getElementById('DIV_Generatori').style.visibility='hidden';
}
</script>
