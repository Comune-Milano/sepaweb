<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_VarAutomatica.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VarAutomatica" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:Label ID="lblAPPALTI" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="IMPORTO  A CANONE"
                Width="391px"></asp:Label>
        </td>
        <td style="text-align: left; vertical-align: top;">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                width: 660px; top: 0px; height: 70px; text-align: left">
                <asp:DataGrid ID="DataGridVCan" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="Gray" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="None" PageSize="1" Style="z-index: 105; left: 8px; top: 32px" 
                    Width="97%">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_VARIAZIONE" HeaderText="DATA VARIAZIONE" ReadOnly="True">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESC_VOCE" HeaderText="VOCE PF." ReadOnly="True">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PERCENTUALE" HeaderText="IMPORTO %">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ReadOnly="True">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
        </td>
        <td style="text-align: left; vertical-align: top;">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgAddAutoCanone" runat="server" ImageUrl="../../../NuoveImm/AddAutom.png"
                            ToolTip="Aggiungi variazione automatica (ripartita dal sistema)" 
                            Style="cursor: pointer;" onclick = "ApriVarAutoCan();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="imgAddManCan" runat="server" ImageUrl="../../../NuoveImm/AddManual.png"
                            ToolTip="Aggiungi una variazione Manualmente" Style="cursor: pointer;" 
                            onclick = "ApriVarManCanone();"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnDelAutoCan" runat="server" 
                            OnClientClick="ConfElimVar();" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                            TabIndex="15" ToolTip="Elimina voce selezionata" CausesValidation="False" 
                            Visible="False" />

                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:Label ID="lblAPPALTI0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="IMPROTO A CONSUMO"
                Width="575px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                width: 660px; top: 0px; height: 70px; text-align: left">
                <asp:DataGrid ID="DataGridVCons" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="97%">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_VARIAZIONE" HeaderText="DATA VARIAZIONE" ReadOnly="True">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESC_VOCE" HeaderText="VOCE PF." ReadOnly="True">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PERCENTUALE" HeaderText="IMPORTO %">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ReadOnly="True">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
        </td>
        <td style="text-align: left; vertical-align: top;">
            <table>
                <tr>
                    <td>
                        <asp:Image ID="imgAddAutoConsumo" runat="server" ImageUrl="../../../NuoveImm/AddAutom.png"
                            ToolTip="Aggiungi variazione automatica (ripartita dal sistema)" 
                            Style="cursor: pointer;" onclick = "ApriVarAutoCon();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image ID="imgAddManCons" runat="server" ImageUrl="../../../NuoveImm/AddManual.png"
                            ToolTip="Aggiungi una variazione Manualmente" Style="cursor: pointer;" 
                            onclick = "ApriVarManConsumo();" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnDelAutoCons" runat="server" 
                            OnClientClick="ConfElimVar();" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                            TabIndex="15" ToolTip="Elimina voce selezionata" CausesValidation="False" 
                            Visible="False" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="100%">Nessuna Selezione</asp:TextBox>
        </td>
    </tr>
</table>
<div id="VarAutoCan" style="border: thin none #3366ff; left: -14px; width: 802px;
    position: absolute; top: -8px; height: 560px; background-color: #dedede; visibility: hidden;
    vertical-align: top; text-align: left; z-index: 201; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');
    margin-right: 10px;">
    <table style="width: 85%; position: absolute; top: 79px; left: 45px; z-index: 600">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="VARIAZIONE CANONE" Width="390px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data" Width="74px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                    Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: left"
                    TabIndex="21" Width="80px" ToolTip="dd/MM/yyyy"></asp:TextBox>
                &nbsp;<asp:RegularExpressionValidator ID="controllodata0" runat="server" ControlToValidate="txtData"
                    ErrorMessage="!" Font-Bold="True" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    Font-Size="10pt" Font-Names="arial" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblImporto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Importo Canone" Width="82px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPercVarCanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px;
                    top: 67px; text-align: right" TabIndex="21" Width="80px"></asp:TextBox>
                &nbsp;<asp:Label ID="lblperc" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="text-align: left" TabIndex="61" Text="%"
                    Width="16px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Provvedimento" Width="74px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" BackColor="White" MaxLength="180" TabIndex="31"
                    TextMode="MultiLine" Width="539px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <div id="DivVarImpCan" style="border: medium solid #ccccff; vertical-align: top; overflow: auto; text-align: left;
                    height: 271px; "  >
                    <asp:DataGrid ID="DgvServAutoCan" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                        top: 32px" Width="98%">
                        <Columns>
                            <asp:BoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="IMPORTO ">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtImpCanone" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="80px" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="IMPORTO_CANONE" HeaderText="IMPORTO_CANONE" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid></div>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
            <td style="text-align: right">
                <asp:ImageButton ID="btn_AddVariazAutoCan" runat="server" ImageUrl="Immagini/Next.png"
                    OnClientClick="document.getElementById('USCITA').value='1'" Style="cursor: pointer"
                    TabIndex="55" ToolTip="Salva" />
                <asp:ImageButton ID="btn_ChiudiAppalti" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png"
                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Variazioni').style.visibility='hidden';"
                    Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 16px; position: absolute; top: 52px; height: 480px;
        width: 770px;" />
</div>
<div id="VarAutoCons" style="border: thin none #3366ff; left: -14px; width: 802px;
    position: absolute; top: -8px; height: 560px; background-color: #dedede; visibility: hidden;
    vertical-align: top; text-align: left; z-index: 201; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');
    margin-right: 10px;">
    <table style="width: 85%; position: absolute; top: 79px; left: 45px; z-index: 600">
        <tr>
            <td colspan="2">
                <asp:Label ID="lblTitle0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="VARIAZIONE CONSUMO" Width="390px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data" Width="74px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDataConsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px;
                    top: 67px; text-align: left" TabIndex="21" Width="80px" ToolTip="dd/MM/yyyy"></asp:TextBox>
                <asp:RegularExpressionValidator ID="controllodata1" runat="server" ControlToValidate="txtDataConsumo"
                    ErrorMessage="!" Font-Bold="True" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                    Font-Size="8pt" Font-Names="arial" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblImportoCons" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" Text="Importo Consumo" Width="89px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPercVarCons" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px;
                    top: 67px; text-align: right" TabIndex="21" Width="80px"></asp:TextBox>
                &nbsp;<asp:Label ID="lblpercCons" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="text-align: left" TabIndex="61" Text="%"
                    Width="16px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Provvedimento" Width="74px"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNoteConsumo" runat="server" BackColor="White" MaxLength="180"
                    TabIndex="31" TextMode="MultiLine" Width="539px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="border: medium solid #ccccff; vertical-align: top; overflow: auto; text-align: left;
                    height: 271px;" id="DivVarImpCon">
                    <asp:DataGrid ID="DgvServAutoCons" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                        top: 32px" Width="97%">
                        <Columns>
                            <asp:BoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="IMPORTO">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPercVarCons" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="80px" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="IMPORTO_CANONE" HeaderText="IMPORTO_CANONE" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid></div>
            </td>
        </tr>
        <tr>
            <td class="style2">
            </td>
            <td style="text-align: right">
                <asp:ImageButton ID="btn_AddVariazAutoCon" runat="server" ImageUrl="Immagini/Next.png"
                    OnClientClick="document.getElementById('USCITA').value='1'" Style="cursor: pointer"
                    ToolTip="Salva" />
                <asp:ImageButton ID="btn_ChiudiConsumo" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png"
                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Variazioni').style.visibility='hidden';"
                    Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />
            </td>
        </tr>
    </table>
    <asp:Image ID="Image2" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 15px; position: absolute; top: 52px; height: 478px;
        width: 770px;" />
</div>
<asp:HiddenField ID="hfCanSelected" runat="server" Value="0" />
<asp:HiddenField ID="hfConSelected" runat="server" Value="0" />
<asp:HiddenField ID="PercUsCanone" runat="server" Value="0" />
<asp:HiddenField ID="SpalmCanone" runat="server" Value="0" />
<asp:HiddenField ID="hfRestaVisible" runat="server" Value="0" />

<asp:HiddenField ID="PercUsCons" runat="server" Value="0" />
<asp:HiddenField ID="SpalmCons" runat="server" Value="0" />
<asp:HiddenField ID="hfRestaVisibleCon" runat="server" Value="0" />
<asp:HiddenField ID="idSelected" runat="server" Value="0" />
<asp:HiddenField ID="hfElimina" runat="server" Value="0" />