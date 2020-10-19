<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabConvocazione.ascx.vb"
    Inherits="Condomini_TabConvocazione" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
<table style="width: 90%;">
    <tr>
        <td style="vertical-align: top; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                width: 703px; top: 0px; height: 260px; text-align: left">
                <asp:DataGrid ID="DataGridConvocazioni" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="692px" EnableViewState="False">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_CONVOCAZIONE" HeaderText="DATA_CONVOCAZIONE" Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="DATA CONVOCAZIONE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_CONVOCAZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DELEGATO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DELEGATO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ALTRE PRESENZE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ALTRE_PRESENZE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA ARRIVO VERBALE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ARRIVO_VERBALE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NUM. VERBALE" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_VERBALE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PROTOCOLLO GESTORE" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROTOCOLLO_ALER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
            <asp:HiddenField ID="txtidConv" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
            <asp:HiddenField ID="vModifica" runat="server" Value="0" />
        </td>
        <td style="vertical-align: top; text-align: left">
            <div id="Convocazione" style="border: thin none #3366ff; left: 0px; width: 802px;
                position: absolute; top: 0px; height: 582px; background-color: #dedede; visibility: visible;
                vertical-align: top; text-align: left; z-index: 201; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
                margin-right: 10px;">
                <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                    Style="z-index: 100; left: 4px; position: absolute; top: 53px; height: 508px;
                    width: 786px;" />
                <table cellpadding="0" cellspacing="0" style="width: 91%; z-index: 500; left: 29px;
                    position: absolute; top: 79px;">
                    <tr>
                        <td style="width: 58px; height: 19px;">
                        </td>
                        <td style="width: 241px; height: 19px;">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Denominazione" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 211px; height: 19px;">
                        </td>
                        <td style="width: 284px; height: 19px;">
                        </td>
                        <td style="height: 19px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td colspan="2" style="width: 241px">
                            <asp:TextBox ID="txtDenomConv" runat="server" BackColor="White" MaxLength="40" ReadOnly="True"
                                TabIndex="11" Width="379px"></asp:TextBox>
                        </td>
                        <td style="width: 284px">
                        </td>
                        <td style="width: 191px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 19px;">
                        </td>
                        <td style="width: 241px; height: 19px;">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Data Arrivo" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 211px; height: 19px;">
                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Tipo Invio" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 284px; height: 19px;">
                        </td>
                        <td style="height: 19px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 241px">
                            <asp:TextBox ID="txtDataArrivo" runat="server" BackColor="White" MaxLength="10" TabIndex="12"
                                Width="100px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataArrivo"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
                                Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                Width="2px"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 211px">
                            <asp:DropDownList ID="cmbTipoInvio" runat="server" BackColor="White" Font-Names="Arial"
                                Font-Size="9pt" Style="top: 109px; left: 9px; right: 481px;" TabIndex="13" Width="186px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 284px">
                        </td>
                        <td style="width: 191px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Numero Protocollo Gestore" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 211px">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Data Arrivo Gestore" Width="101px"></asp:Label>
                        </td>
                        <td style="width: 284px">
                            &nbsp;
                        </td>
                        <td style="width: 191px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 22px;">
                        </td>
                        <td style="width: 241px; height: 22px;">
                            <asp:TextBox ID="txtProtAler" runat="server" BackColor="White" MaxLength="40" TabIndex="14"
                                Width="186px"></asp:TextBox>
                        </td>
                        <td style="width: 211px; height: 22px;">
                            <asp:TextBox ID="TxtDataArrivoAler" runat="server" BackColor="White" MaxLength="10"
                                TabIndex="15" Width="100px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtDataArrivoAler"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
                                Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                Width="2px"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 284px; height: 22px;">
                            &nbsp;
                        </td>
                        <td style="height: 22px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Tipo" Width="51px"></asp:Label>
                        </td>
                        <td style="width: 211px">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Data Convocazione" Width="122px"></asp:Label>
                        </td>
                        <td style="width: 284px">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="ORA" Width="47px"></asp:Label>
                        </td>
                        <td style="width: 191px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 241px">
                            <asp:DropDownList ID="CmbTipoConvoc" runat="server" BackColor="White" Font-Names="Arial"
                                Font-Size="9pt" Style="top: 109px; left: 9px; right: 481px;" TabIndex="17" Width="186px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 211px">
                            <asp:TextBox ID="txtDataConv" runat="server" BackColor="White" TabIndex="18" Width="100px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataConv"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
                                Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                Width="2px"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 284px">
                            <asp:TextBox ID="txthh" runat="server" BackColor="White" MaxLength="2" TabIndex="19"
                                Width="24px"></asp:TextBox>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                ControlToValidate="txthh" ErrorMessage="!" Font-Bold="True" Font-Names="Arial"
                                Font-Size="11pt" Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="([0-1]\d|2[0-3])" Width="2px"></asp:RegularExpressionValidator>
                            &nbsp;
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text=":" Width="10px"></asp:Label>
                            <asp:TextBox ID="txtMM" runat="server" BackColor="White" MaxLength="2" TabIndex="20"
                                Width="24px"></asp:TextBox>
                            &nbsp;
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                ControlToValidate="txtMM" ErrorMessage="!" Font-Bold="True" Font-Names="Arial"
                                Font-Size="11pt" Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="([0-1]\d|[1-5][0-9])" Width="2px"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 191px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Perc.Mill.Pres.Assemblea" Width="154px"></asp:Label>
                        </td>
                        <td style="width: 211px">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Percentuale Superfici/Mill." Width="161px" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 284px">
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Perc. Mill. Comproprietà" Width="154px" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 191px; vertical-align: top;">
                            <asp:Label ID="lbl1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Percent. Mill. Proprietà" Width="146px" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 24px">
                        </td>
                        <td style="height: 24px; width: 241px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtPercMillPresAss" runat="server" BackColor="White" Enabled="False"
                                MaxLength="10" ReadOnly="True" Style="text-align: right" TabIndex="23" Width="145px"></asp:TextBox>
                        </td>
                        <td style="height: 24px; width: 211px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtpercSup" runat="server" BackColor="White" Enabled="False" MaxLength="10"
                                ReadOnly="True" Style="text-align: right" TabIndex="22" Width="145px" Visible="False"></asp:TextBox>
                        </td>
                        <td style="height: 24px; width: 284px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtPercMillComp" runat="server" BackColor="White" Enabled="False"
                                MaxLength="10" ReadOnly="True" Style="text-align: right" TabIndex="23" Width="145px"
                                Visible="False"></asp:TextBox>
                        </td>
                        <td style="height: 24px; width: 191px; vertical-align: top;">
                            <asp:TextBox ID="txtPercMilProp" runat="server" BackColor="White" Enabled="False"
                                MaxLength="10" ReadOnly="True" Style="text-align: right" TabIndex="21" Width="145px"
                                Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px">
                        </td>
                        <td style="width: 241px">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Delegato" Width="51px"></asp:Label>
                        </td>
                        <td style="width: 211px">
                            &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Altre Presenze" Width="154px"></asp:Label>
                        </td>
                        <td style="width: 284px">
                            &nbsp;
                        </td>
                        <td style="width: 191px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 24px;">
                        </td>
                        <td style="width: 241px; height: 24px;">
                            <asp:TextBox ID="txtDelegato" runat="server" BackColor="White" MaxLength="80" TabIndex="24"
                                Width="176px"></asp:TextBox>
                            <asp:Image ID="ImgDeleg" runat="server" ImageUrl="~/Condomini/Immagini/down-icon.png"
                                onclick="myOpacityDeleg.toggle();" TabIndex="-1" ToolTip="Lista Delegati" />
                        </td>
                        <td style="width: 211px; height: 24px;">
                            <asp:TextBox ID="txtAltrePresenze" runat="server" BackColor="White" MaxLength="80"
                                TabIndex="25" Width="185px"></asp:TextBox>
                        </td>
                        <td style="width: 284px; height: 24px; text-align: center;">
                            <asp:ImageButton ID="btnOrdGiorno" runat="server" 
                                ImageUrl="~/Condomini/Immagini/ImgOrdGiorno.png" OnClientClick = "ApriOrdGiorno();return false;" 
                                ToolTip="Elabora ordine del giorno" />
                        </td>
                        <td style="height: 24px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 19px;">
                        </td>
                        <td style="width: 241px; height: 19px;">
                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="VERBALE" Width="51px"></asp:Label>
                        </td>
                        <td style="width: 211px; height: 19px;">
                            &nbsp;
                        </td>
                        <td style="width: 284px; height: 19px;">
                            &nbsp;
                        </td>
                        <td style="height: 19px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 19px;">
                        </td>
                        <td style="width: 241px; height: 19px;">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Data Arrivo" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 211px; height: 19px;">
                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Tipo Invio" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 284px; height: 19px;">
                            &nbsp;
                        </td>
                        <td style="height: 19px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 24px;">
                        </td>
                        <td style="width: 241px; height: 24px;">
                            <asp:TextBox ID="txtDataArrivoVerb" runat="server" BackColor="White" MaxLength="10"
                                TabIndex="25" Width="100px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataArrivoVerb"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
                                Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                Width="2px"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 211px; height: 24px;">
                            <asp:DropDownList ID="cmbTipoInvioVerb" runat="server" BackColor="White" Font-Names="Arial"
                                Font-Size="9pt" Style="top: 109px; left: 9px; right: 481px;" TabIndex="25" Width="186px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 284px; height: 24px;">
                            &nbsp;
                        </td>
                        <td style="height: 24px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 10px">
                        </td>
                        <td style="width: 241px; height: 10px">
                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Numero Protocollo Gestore" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 211px; height: 10px">
                            <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Data Arrivo Gestore" Width="101px"></asp:Label>
                        </td>
                        <td style="width: 284px; height: 10px">
                        </td>
                        <td style="height: 10px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 24px">
                        </td>
                        <td style="width: 241px; height: 24px">
                            <asp:TextBox ID="txtVerbNProtAler" runat="server" BackColor="White" MaxLength="40"
                                TabIndex="26" Width="183px"></asp:TextBox>
                        </td>
                        <td style="width: 211px; height: 24px">
                            <asp:TextBox ID="txtDataArrivoVerbAler" runat="server" BackColor="White" MaxLength="10"
                                TabIndex="27" Width="100px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataArrivoVerbAler"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
                                Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                Width="1px"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 284px; height: 24px">
                        </td>
                        <td style="height: 24px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 24px">
                        </td>
                        <td style="width: 241px; height: 24px">
                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Note" Width="162px"></asp:Label>
                        </td>
                        <td style="width: 211px; height: 24px">
                        </td>
                        <td style="width: 284px; height: 24px">
                        </td>
                        <td style="height: 24px; width: 191px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 58px; height: 40px;">
                        </td>
                        <td colspan="2" style="width: 241px; height: 40px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtNote" runat="server" BackColor="White" MaxLength="200" TabIndex="28"
                                TextMode="MultiLine" Width="360px"></asp:TextBox>
                        </td>
                        <td style="width: 284px; height: 40px; vertical-align: middle; text-align: right;">
                            &nbsp;<asp:ImageButton ID="btnSalvaCambioAmm" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="31" ToolTip="Salva" />
                        </td>
                        <td style="width: 191px; text-align: left; height: 40px; vertical-align: middle;">
                            <img id="imgAnnulla" alt="Chiudi la finestra" onclick="document.getElementById('TextBox2').value!='1';myOpacity2.toggle();myOpacityDeleg.hide();PulisciCampiConvocazione();document.getElementById('AggPercent').value!='1';"
                                src="../NuoveImm/Img_AnnullaVal.png" style="left: 185px; cursor: pointer; top: 23px" />&nbsp;
                        </td>
                        <td style="height: 40px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Image ID="imgAddConv" onclick="document.getElementById('TextBox2').value!='1';myOpacity2.toggle();"
                            runat="server" ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png"
                            ToolTip="Aggiungi una convocazione" Style="cursor: pointer" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" ToolTip="Modifica" CausesValidation="False"
                            OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" OnClientClick="DeleteConfirmConvoc();"
                            ToolTip="Elimina Elemento Selezionato" Height="18px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="572px">Nessuna Selezione</asp:TextBox>
        </td>
        <td style="vertical-align: top; text-align: left">
            &nbsp;
        </td>
    </tr>
</table>
<div style="z-index: 800; left: 52px; width: 178px; position: absolute; top: 340px;
    height: 50px" id="divDeleg">
    <asp:ListBox ID="listDelegati" runat="server" Width="100%" Font-Names="Arial" Font-Size="9pt"
        TabIndex="-1" onclick="selezDaListDeleg();myOpacityDeleg.toggle();"></asp:ListBox>
</div>
<script type="text/javascript">

    myOpacity2 = new fx.Opacity('Convocazione', { duration: 200 });
    //myOpacity.hide();
    myOpacityDeleg = new fx.Opacity('divDeleg', { duration: 200 });
    myOpacityDeleg.hide();

    if (document.getElementById('TextBox2').value != '2') {
        myOpacity2.hide(); ;
    }
    //                   if (document.getElementById('TextBox5').value != '2') {
    //                        myOpacity.hide(); ;
    //                    }
    if (document.getElementById('ImgVisibility').value != '1') {
        document.getElementById('TabConvocazione1_imgAddConv').style.visibility = 'hidden';
    }


    function DeleteConfirmConvoc() {
        if (document.getElementById('TabConvocazione1_txtidConv').value != 0) {
            var Conferma
            Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
            if (Conferma == false) {
                document.getElementById('TabConvocazione1_txtConfElimina').value = '0';

            }
            else {
                document.getElementById('TabConvocazione1_txtConfElimina').value = '1';

            }
        }
    }
</script>
