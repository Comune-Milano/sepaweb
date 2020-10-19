<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Rendimento.ascx.vb" Inherits="Tab_Termico_Rendimento" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO CONTROLLI DEL RENDIMENTO DI COMBUSTIONE"
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
                height: 250px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="63" Width="1100px">
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
                        <asp:BoundColumn DataField="DATA_ESAME" HeaderText="DATA CONTROLLO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ESECUTORE" HeaderText="ESEGUITO DA"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="TEMP. FUMI (&#176;C)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TEMP_FUMI") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TEMP_FUMI") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TEMP. AMB. (&#176;C)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TEMP_AMB") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TEMP_AMB") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="O2 (%)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.O2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.O2") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CO2">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO2") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO2") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="BACHARACH (n.)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BACHARACH") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BACHARACH") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CO (ppm)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="REND. DI COMBUSTIONE (%)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RENDIMENTO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RENDIMENTO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIRAGGIO (Pa)">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIRAGGIO") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIRAGGIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" HorizontalAlign="Right" />
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
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSelControlli" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                Style="left: 40px; top: 200px" TabIndex="-1" Width="680px" ReadOnly="True"></asp:TextBox></td>
        <td>
            &nbsp; &nbsp; &nbsp;</td>
        <td>
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnAggControllo" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Termico_Rendimento_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Controlli').style.visibility='visible';"
                            TabIndex="64" ToolTip="Aggiunge un nuovo Controllo" /></td>
                </tr>
                <tr>
                    <td style="width: 88px">
                        <asp:ImageButton ID="btnEliminaControllo" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloRendimento();"
                            TabIndex="65" ToolTip="Elimina il Controllo selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px; height: 12px">
                        <asp:ImageButton ID="btnApriControllo" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Termico_Rendimento_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Controlli').style.visibility='visible';"
                            TabIndex="66" ToolTip="Modifica il Controllo selezionato" Width="60px" /></td>
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

<div style="left: 0px; width: 800px; position: absolute; top: 0px; height: 550px;
    background-color: whitesmoke; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);" id="DIV_Controlli">
    <br />
    <br />
    <table id="Table4" border="0" cellpadding="1" cellspacing="1" 
        style="border-right: blue 2px; border-top: blue 2px; left: 120px;
        border-left: blue 2px; width: 34%; border-bottom: blue 2px; position: absolute;
        top: 96px; height: 248px; background-color: #ffffff; z-index: 102;">
        <tr>
            <td style="width: 404px; height: 18px; text-align: left">
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Controlli</span></strong></td>
        </tr>
        <tr>
            <td style="width: 404px; height: 114px">
                <span style="color: #ffffff"><span style="font-size: 10pt; font-family: Arial"><strong>
                    &nbsp;<table id="Table3">
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lbl_Data" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Data del Controllo *</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtData" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                    Style="left: 144px; top: 192px" TabIndex="67" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtData"
                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 33px">
                                <asp:Label ID="lbl_Esecutore" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="100px">Esecutore</asp:Label></td>
                            <td style="height: 33px">
                                <asp:TextBox ID="txtEsecutore" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    MaxLength="100" Style="left: 72px; top: 224px" TabIndex="68" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lbl_Fumi" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="100px">Temp. Fumi</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtTempiFumi" runat="server" Font-Names="arial" Font-Size="9pt"
                                    MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="69" Width="100px"></asp:TextBox>
                                <asp:Label ID="lbl_PotenzaKW" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="40px"> (°C)</asp:Label><asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                        runat="server" ControlToValidate="txtTempiFumi" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
                                        Font-Names="Arial" Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1"
                                        ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lblAmb" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Temp. Ambiente</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtTempiAmb" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                    Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="70" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (°C)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTempiAmb"
                                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                        Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lblO2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">O2</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtO2" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                    Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="71" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (%)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtO2" Display="Dynamic"
                                        ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt"
                                        Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lblCO2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">CO2</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtCO2" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                    Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="72" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (%)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCO2" Display="Dynamic"
                                        ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt"
                                        Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lblBacharach" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Bacharach</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtBacharach" runat="server" Font-Names="arial" Font-Size="9pt"
                                    MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="73" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (n.)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtBacharach"
                                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                        Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lblCO" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">CO</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtCO" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                    Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="74" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (ppm)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtCO" Display="Dynamic"
                                        ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt"
                                        Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px; height: 25px">
                                <asp:Label ID="lblRend" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Rend. di Combustione</asp:Label></td>
                            <td style="height: 25px">
                                <asp:TextBox ID="txtRendimento" runat="server" Font-Names="arial" Font-Size="9pt"
                                    MaxLength="10" Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="75" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (%)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtRendimento"
                                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                        Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 1px">
                                <asp:Label ID="lblTiraggio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Tiraggio</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTiraggio" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                    Style="z-index: 102; left: 144px; top: 224px; text-align: right;" TabIndex="76" Width="100px"></asp:TextBox>
                                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="40px"> (Pa)</asp:Label><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtTiraggio"
                                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                        Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator></td>
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
            <td style="width: 404px; height: 38px; text-align: right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciControllo" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Termico_Rendimento_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="78" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiControllo" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Controlli').style.visibility='hidden';document.getElementById('Tab_Termico_Rendimento_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="79" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="456px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 80px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="600px" />
</div>


<asp:TextBox ID="txtAppare"         runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_Termico_Rendimento_txtAppare').value!='1') {
document.getElementById('DIV_Controlli').style.visibility='hidden';
}

</script>
