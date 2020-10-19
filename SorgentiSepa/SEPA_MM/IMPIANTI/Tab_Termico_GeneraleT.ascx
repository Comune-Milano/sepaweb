<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_GeneraleT.ascx.vb" Inherits="TabGeneraleT" %>


<div style="visibility: visible; overflow: auto; border-top-width: thin; border-left-width: thin; border-left-color: yellow; border-bottom-width: thin; border-bottom-color: yellow; border-top-color: yellow; border-right-width: thin; border-right-color: yellow;">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Edifici Alimentati" style="left: 0px; top: -30px" ForeColor="Black" Width="60px" TabIndex="-1"></asp:Label></td>
            <td>
                <div id="DIV1" style="border-right: #0000cc thin solid; border-top: #0000cc thin solid;
                    visibility: visible; overflow: auto; border-left: #0000cc thin solid; width: 620px;
                    border-bottom: #0000cc thin solid; height: 110px">
                    <asp:DataGrid ID="DataGridEdifici" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#000099" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="Black" PageSize="1" Style="table-layout: auto;
                        z-index: 101; left: 9px; border-collapse: separate" Width="100%">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Mode="NumericPages" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="0%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Width="50%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt"
                                    Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TOTALE_UI_AL" HeaderText="U.I. (Alimentate)">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_UI" HeaderText="U.I. (Totali)">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_MQ_AL" HeaderText="MQ (Alimentati)">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTALE_MQ" HeaderText="MQ (Totali)">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="UNITA" HeaderText="UNITA' IMMOBILIARI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CHK" HeaderText="CHK" Visible="False">
                                <HeaderStyle Width="0%" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" Wrap="False" />
                    </asp:DataGrid></div>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                <asp:Label ID="lblTotaleUI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="65px">Totale U.I.</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                <asp:TextBox ID="txtTotUI" runat="server" Enabled="False" Font-Names="Arial"
                    Font-Size="9pt" MaxLength="15" Style="left: 144px; top: 192px; text-align: right;" TabIndex="-1"
                    Width="65px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 18px">
                <asp:Label ID="lblMq" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="65px">Tot. mq</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                <asp:TextBox ID="txtTotMq" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="15" Style="left: 144px; top: 192px; text-align: right;" TabIndex="-1" Width="65px"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Ditta di Gestione</asp:Label></td>
            <td>
                <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="300"
                    Style="left: 80px; top: 88px;" TabIndex="8"
                    Width="410px"></asp:TextBox></td>
            <td style="width: 11px">
                &nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="lblNumTel" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
            <td>
                <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" TabIndex="9" Width="200px" ToolTip="Numero telefonico di Riferimento"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="height: 44px">
                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Ditta Fornitrice di Calore</asp:Label></td>
            <td style="height: 44px">
                <asp:TextBox ID="txtDittaFornitrice" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="300" Style="left: 80px; top: 88px;" TabIndex="10"
                    Width="410px"></asp:TextBox></td>
            <td style="width: 11px; height: 44px;">
            </td>
            <td style="height: 44px">
                <asp:Label ID="lblNumTel2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
            <td style="height: 44px">
                <asp:TextBox ID="txtNumTelefonico2" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" TabIndex="11" ToolTip="Numero telefonico di Riferimento" Width="200px"></asp:TextBox></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblDataRiposo" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="60px">Data Prima Accensione</asp:Label></td>
            <td>
                            <asp:TextBox ID="txtDataAccensione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Style="left: 504px; top: 152px" TabIndex="12" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataAccensione"
                                Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                Width="88px" Height="16px"></asp:RegularExpressionValidator></td>
            <td>
            </td>
            <td>
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                                Width="86px" Height="24px">Data Messa a Riposo Stagionale</asp:Label></td>
            <td>
                            <asp:TextBox ID="txtDataRiposo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Style="left: 504px; top: 152px" TabIndex="13" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><br />
                <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataRiposo"
                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    Width="90px"></asp:RegularExpressionValidator></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblOreEsercizio" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="70px">Num. Ore di Esercizio</asp:Label></td>
            <td>
                <asp:TextBox ID="txtOreEsercizio" runat="server" Font-Names="arial" Font-Size="9pt"
                    MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                    TabIndex="14" Width="50px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtOreEsercizio"
                    Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                    TabIndex="303" ValidationExpression="\d+" Width="80px"></asp:RegularExpressionValidator></td>
            <td style="width: 11px">
            </td>
            <td>
                <asp:Label ID="lblEstintori" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="70px">Estintori</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbEstintori" runat="server" AutoPostBack="True" BackColor="White"
                    Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="-1" ToolTip="Presenza Estintori" Width="56px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="S">SI</asp:ListItem>
                    <asp:ListItem Value="N">NO</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblNumEstintori" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="47px">Quantità Estintori</asp:Label></td>
            <td>
                <asp:TextBox ID="txtNumEstintori" runat="server" Font-Names="arial" Font-Size="9pt"
                    MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right"
                    TabIndex="-1" Width="48px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtNumEstintori"
                    Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                    TabIndex="303" ValidationExpression="\d+" Width="80px"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblCombustibile" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px;
                    top: 144px" Width="60px" TabIndex="-1">Combustibile</asp:Label>
                </td>
            <td>
                <asp:DropDownList ID="cmbCombustibile" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="8pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="15" Width="110px" AutoPostBack="True">
            </asp:DropDownList></td>
            <td>
                &nbsp;
            </td>
            <td id="TAB3">
                <asp:Label ID="lblSerbatoio" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 472px;
                    top: 192px" Width="50px">Serbatoio</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbTipoSerbatoio" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="8pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="16" Width="90px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>INTERRATO</asp:ListItem>
                    <asp:ListItem>FUORI TERRA</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblCapacita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px"
                    Width="70px" TabIndex="-1">Capacità (mc)</asp:Label></td>
            <td>
                <asp:TextBox ID="txtCapacita" runat="server"
                    Font-Names="arial" Font-Size="9pt" MaxLength="10" Style="z-index: 102; left: 592px; top: 192px; text-align: right;" Width="50px" TabIndex="17"></asp:TextBox>
                <asp:TextBox ID="txtContatoreDPR" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="15" TabIndex="15" Width="50px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCapacita"
                    ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt" Style="left: 464px; top: 144px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                    Width="80px" Display="Dynamic" TabIndex="-1" Height="16px">Valore Numerico</asp:RegularExpressionValidator></td>
            <td style="width: 11px">
                &nbsp;</td>
            <td>
                <asp:Label ID="LblPotenza" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px;
                    top: 8px" Width="70px">Potenza (Kw)</asp:Label></td>
            <td>
                <asp:TextBox ID="txtPotenza" runat="server"
                    Font-Names="arial" Font-Size="9pt" MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right;" TabIndex="18" Width="48px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPotenza"
                    ErrorMessage="RegularExpressionValidator" Style="left: 168px;
                    top: 8px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="48px" Font-Names="Arial" Font-Size="8pt" Display="Dynamic" TabIndex="-1" Height="16px">Valore Numerico</asp:RegularExpressionValidator></td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="LblConsumo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="47px" TabIndex="-1">Consumo Medio</asp:Label></td>
            <td>
                <asp:TextBox ID="txtConsumo" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                    Style="z-index: 102; left: 688px; top: 192px; text-align: right;" TabIndex="19" Width="48px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConsumo"
                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                        Font-Size="8pt" Style="left: 168px; top: 8px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="NOTE" Width="60px" TabIndex="-1"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="4000" Style="left: 8px;
                    top: 432px;" TextMode="MultiLine" Width="690px" TabIndex="20" Height="40px"></asp:TextBox></td>
        </tr>
    </table>
</div>
