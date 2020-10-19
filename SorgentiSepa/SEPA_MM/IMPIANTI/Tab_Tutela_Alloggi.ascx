<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Tutela_Alloggi.ascx.vb" Inherits="Tab_Tutela_Alloggi" %>
<table id="TABBLE_LISTA">
    <tr>
        <td>
            <asp:Label ID="lblCitofoni" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO TUTELA ALLOGGI"
                Width="368px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 59px">
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                height: 280px">
                <asp:DataGrid ID="DataGridA" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="35" Width="1500px">
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
                            <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="SUB">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME_SUB") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME_SUB") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ANTINTRUSIONE">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANTINTRUSIONE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ANTINTRUSIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INSTALL. ANTINTRUSIONE">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_ANTINTR") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_ANTINTR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RIMOZIONE ANTINTRUSIONE">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_ANTINTR") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_ANTINTR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="BLINDATA">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BLINDATA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BLINDATA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INSTALL. BLINDATA">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_BLINDATA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_BLINDATA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="LASTRATURA">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LASTRATURA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LASTRATURA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INSTALL. LASTRATURA">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_LASTRATURA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_INSTALLA_LASTRATURA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RIMOZIONE LASTRATURA">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_LASTRATURA") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RIMOZIONE_LASTRATURA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ID_UNITA_IMMOBILIARI" Visible="False">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_IMMOBILIARI") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_UNITA_IMMOBILIARI") %>'></asp:Label>
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
            <asp:TextBox ID="txtSelA" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox></td>
        <td style="height: 59px">
            &nbsp; &nbsp;</td>
        <td style="height: 59px">
            <table style="width: 57%">
                <tr>
                    <td style="width: 88px; height: 14px">
                        <asp:ImageButton ID="btnApriA" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('Tab_Tutela_Alloggi_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_A').style.visibility='visible';"
                            TabIndex="36" ToolTip="Modifica le informazioni dell'alloggio selezionato" Width="60px" /></td>
                </tr>
                <tr>
                    <td style="width: 88px;">
                        </td>
                </tr>
                <tr>
                    <td style="width: 88px;">
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
        </td>
    </tr>
</table>

<div id="DIV_A" style="width: 800px; height: 550px; background-color: #dedede; left: 0px; position: absolute; top: 0px; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);" >
    <table style="left: 70px; width: 45%; position: absolute;
        top: 100px; height: 248px; background-color: #ffffff; border-top-width: 2px; border-left-width: 2px; z-index: 102; border-left-color: blue; border-bottom-width: 2px; border-bottom-color: blue; border-top-color: blue; border-right-width: 2px; border-right-color: blue;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Tutela Alloggio</span></strong></td>
        </tr>
        <tr>
            <td style="height: 236px">
                <table style="width: 336px">
                    <tr>
                        <td>
                            <asp:Label ID="lblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="70px">Edificio:</asp:Label></td>
                        <td style="width: 14px">
                            <asp:TextBox ID="txtEdificio" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="100" Style="left: 72px; top: 224px" TabIndex="-1" Width="450px"></asp:TextBox></td>
                    </tr>
                </table>
                <table style="width: 272px">
                    <tr>
                        <td style="height: 21px">
                            <asp:Label ID="lblScala" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="70px">Scala:</asp:Label></td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtScala" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="100" Style="left: 72px; top: 224px" TabIndex="-1" Width="160px"></asp:TextBox></td>
                        <td style="height: 21px">
                            <asp:Label ID="lblPiano" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="60px">Piano:</asp:Label></td>
                        <td style="width: 6px; height: 21px;">
                            <asp:TextBox ID="txtPiano" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="100" Style="left: 72px; top: 224px" TabIndex="-1" Width="160px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblInterno" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="70px">Interno:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtInterno" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="100" Style="left: 72px; top: 224px" TabIndex="-1" Width="160px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblSub" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="60px">SUB:</asp:Label></td>
                        <td style="width: 6px">
                            <asp:TextBox ID="txtSUB" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="100" Style="left: 72px; top: 224px" TabIndex="-1" Width="160px"></asp:TextBox></td>
                    </tr>
                </table>
                <table style="width: 208px">
                    <tr>
                        <td>
                            <asp:Label ID="lblAntintrusione" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="70px">Antintrusione</asp:Label></td>
                        <td><asp:DropDownList ID="cmbAnti" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="37" ToolTip="Antintrusione" Width="56px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblDataAntiIns" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="90px">Data Installazione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDataAnt1" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                Style="left: 144px; top: 192px" TabIndex="38" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAnt1"
                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblDataAntiRim" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Data Rimozione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDataAnt2" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                Style="left: 144px; top: 192px" TabIndex="39" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataAnt2"
                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblBlindata" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="70px">Blindata</asp:Label></td>
                        <td><asp:DropDownList ID="cmbBlindata" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="40" ToolTip="Blindata" Width="56px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="S">SI</asp:ListItem>
                            <asp:ListItem Value="N">NO</asp:ListItem>
                        </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblDataBlindata" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="90px">Data Installazione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDataBlind1" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="41" ToolTip="gg/mm/aaaa"
                                Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                    runat="server" ControlToValidate="txtDataBlind1" Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)"
                                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLastratura" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="70px">Lastratura</asp:Label></td>
                        <td>
                            <asp:DropDownList ID="cmbLastratura" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 184px" TabIndex="42" ToolTip="Antintrusione" Width="56px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="S">SI</asp:ListItem>
                                <asp:ListItem Value="N">NO</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblDataLastratura1" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="90px">Data Installazione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDataLast1" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="43" ToolTip="gg/mm/aaaa"
                                Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                    runat="server" ControlToValidate="txtDataLast1" Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)"
                                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblDataLastratura2" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="80px">Data Rimozione</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDataLast2" runat="server" Font-Names="Arial" Font-Size="9pt"
                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="44" ToolTip="gg/mm/aaaa"
                                Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                    runat="server" ControlToValidate="txtDataLast2" Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)"
                                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="140px"></asp:RegularExpressionValidator></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtIDA" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_InserisciA" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Tutela_Alloggi_txtAppare').value='0';"
                                Style="cursor: pointer" TabIndex="45" ToolTip="Salva le modifiche apportate" />&nbsp;<asp:ImageButton
                                    ID="btn_ChiudiA" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_A').style.visibility='hidden';document.getElementById('Tab_Tutela_Alloggi_txtAppare').value='0';"
                                    Style="cursor: pointer" TabIndex="46" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoSchema" runat="server" BackColor="White" Height="376px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 40px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 70px" Width="696px" />
</div>

<asp:TextBox ID="txtannullo"            runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtUnita_Immobiliare"  runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppare"             runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"       runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<script type="text/javascript">

if (document.getElementById('Tab_Tutela_Alloggi_txtAppare').value!='1') {
document.getElementById('DIV_A').style.visibility='hidden';
}

</script>


