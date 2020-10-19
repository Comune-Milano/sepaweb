<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Elettrico_Dettagli.ascx.vb" Inherits="TabElettricoDettagli" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="lblQuadroServizi" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO QUADRI SERVIZI GENERALI"
                Width="368px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 119px">
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 100px">
                <asp:DataGrid ID="DataGridServizio" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="15" Width="720px">
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
                        <asp:TemplateColumn HeaderText="Quantit&#224;">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="DIFFERENZIALE" HeaderText="Protezione Differenziale"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="A Norma">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NORMA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NORMA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="UBICAZIONE" HeaderText="Ubicazione"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SCALE_SERVITE" HeaderText="Num. Elementi Serviti"></asp:BoundColumn>
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
            <asp:TextBox ID="txtSelServizio" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td style="height: 119px">
            &nbsp; &nbsp;</td>
        <td style="height: 119px">
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggServizio" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('TabElettricoDettagli_txtAppareSE').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Servizio').style.visibility='visible';"
                            TabIndex="12" ToolTip="Aggiunge un nuovo Quadro Servizi" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaServizio" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloSE();"
                            TabIndex="13" ToolTip="Elimina il Quadro Servizi Selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriServizio" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TabElettricoDettagli_txtAppareSE').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Servizio').style.visibility='visible';"
                            TabIndex="14" ToolTip="Modifica il Quadro Servizi Selezionato" Width="60px" /></td>
                </tr>
            </table>
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblQuadriScala" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO QUADRI SCALA"
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
                height: 100px">
                <asp:DataGrid ID="DataGridScala" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="19" Width="728px">
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
                        <asp:TemplateColumn HeaderText="Quantit&#224;">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QUANTITA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="DIFFERENZIALE" HeaderText="Protezione Differenziale"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="A Norma">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NORMA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NORMA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="UBICAZIONE" HeaderText="Ubicazione"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SCALE_SERVITE" HeaderText="Num. Elementi Serviti"></asp:BoundColumn>
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
            <asp:TextBox ID="txtSelScale" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td>
        </td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggScale" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('TabElettricoDettagli_txtAppareSC').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Scale').style.visibility='visible';"
                            TabIndex="16" ToolTip="Aggiunge un nuovo Quadro Scala" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaScale" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloSC();"
                            TabIndex="18" ToolTip="Elimina il Quadro Scala selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnApriScale" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('TabElettricoDettagli_txtAppareSC').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Scale').style.visibility='visible';"
                            TabIndex="18" ToolTip="Modifica il Quadro Scala selezionato" Width="60px" /></td>
                </tr>
            </table>
            <br />
        </td>
    </tr>
</table>

<div id="DIV_Servizio" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: #dedede; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table id="TABBLE1" style="left: 130px;
        position: absolute; top: 90px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Quadro Servizi Generali</span></strong></td>
        </tr>
        <tr>
            <td>
                <table style="width: 208px">
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblQuantitaSE" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="125px">Quantità *</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtQuantitaSE" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                                TabIndex="20" Width="80px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtQuantitaSE"
                                Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                                TabIndex="303" ValidationExpression="\d+" Width="176px"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDifferenzialeSE" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="125px">Protezione Differenziale *</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbDifferenzialeSE" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="21" ToolTip="Protezione Differenziale" Width="120px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem>0.5</asp:ListItem>
                                <asp:ListItem>0.3</asp:ListItem>
                                <asp:ListItem>0.03</asp:ListItem>
                                <asp:ListItem>PARZIALE</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNormaSE" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="125px">A Norma</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbNormaSE" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="22" ToolTip="A Norma" Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUbicazioneSE" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="125px">Ubicazione</asp:Label><br />
                        </td>
                        <td>
                            <asp:TextBox ID="txtUbicazioneSE" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Height="70px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="23" TextMode="MultiLine"
                                Width="220px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblElementiServitiSE" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" TabIndex="-1" Text="Elementi Serviti" Width="125px"></asp:Label></td>
                        <td>
                            <div style="border-right: navy 1px solid; border-top: navy 1px solid; overflow: auto;
                                border-left: navy 1px solid; width: 220px; border-bottom: navy 1px solid; height: 110px">
                                <asp:CheckBoxList ID="CheckBoxScaleSE" runat="server" BorderColor="Black" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Height="40px" TabIndex="52" Width="192px">
                                </asp:CheckBoxList>
                                <asp:Label ID="lblATTENZIONE_SE" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Red" Style="z-index: 100; left: 24px; top: 32px" Width="200px">ATTENZIONE: Scegliere l'edificio dalla pagina principale per selezionare gli elementi serviti.</asp:Label></div>
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDSE" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btn_InserisciServizio" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('TabElettricoDettagli_txtAppareSE').value='0';"
                                Style="cursor: pointer" TabIndex="24" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiServizio" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Servizio').style.visibility='hidden';document.getElementById('TabElettricoDettagli_txtAppareSE').value='0';"
                                    Style="cursor: pointer" TabIndex="25" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="448px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 100px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 60px" Width="416px" />
</div>
<div id="DIV_Scale" style="left: 0px; width: 800px; position: absolute; top: 0px;
    height: 550px; background-color: #dedede; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table id="TABBLE2" style="left: 128px;
        position: absolute; top: 88px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Quadro Scala</span></strong></td>
        </tr>
        <tr>
            <td>
                <table style="width: 208px">
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblQuantitaSC" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="125px">Quantità *</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtQuantitaSC" runat="server" Font-Names="arial" Font-Size="9pt"
                                MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                                TabIndex="26" Width="80px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantitaSC"
                                Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                                TabIndex="303" ValidationExpression="\d+" Width="176px"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDifferenzialeSC" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="125px">Protezione Differenziale *</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbDifferenzialeSC" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="27" ToolTip="Protezione Differenziale" Width="120px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem>0.5</asp:ListItem>
                                <asp:ListItem>0.3</asp:ListItem>
                                <asp:ListItem>0.03</asp:ListItem>
                                <asp:ListItem>PARZIALE</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNormaSC" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="125px">A Norma</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbNormaSC" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="28" ToolTip="A Norma" Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUbicazioneSC" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="125px">Ubicazione</asp:Label><br />
                        </td>
                        <td>
                            <asp:TextBox ID="txtUbicazioneSC" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Height="70px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="29" TextMode="MultiLine"
                                Width="220px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblElementiServitiSC" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" TabIndex="-1" Text="Elementi Serviti" Width="125px"></asp:Label><br />
                            <br />
                        </td>
                        <td>
                            <div style="border-right: navy 1px solid; border-top: navy 1px solid; overflow: auto;
                                border-left: navy 1px solid; width: 220px; border-bottom: navy 1px solid; height: 110px">
                                <asp:CheckBoxList ID="CheckBoxScaleSC" runat="server" BorderColor="Black" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Height="40px" TabIndex="52" Width="192px">
                                </asp:CheckBoxList>
                                <asp:Label ID="lblATTENZIONE_SC" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Red" Style="z-index: 100; left: 24px; top: 32px" Width="200px">ATTENZIONE: Scegliere l'edificio dalla pagina principale per selezionare gli elementi serviti.</asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:TextBox ID="txtIDSC" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btn_InserisciScala" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('TabElettricoDettagli_txtAppareSC').value='0';"
                                Style="cursor: pointer" TabIndex="30" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiScala" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Scale').style.visibility='hidden';document.getElementById('TabElettricoDettagli_txtAppareSC').value='0';"
                                    Style="cursor: pointer" TabIndex="31" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="464px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 100px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 60px" Width="416px" />
</div>

<asp:TextBox ID="txtAppareSE"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareSC"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('TabElettricoDettagli_txtAppareSE').value!='1') {
document.getElementById('DIV_Servizio').style.visibility='hidden';
}

if (document.getElementById('TabElettricoDettagli_txtAppareSC').value!='1') {
document.getElementById('DIV_Scale').style.visibility='hidden';
}
</script>
