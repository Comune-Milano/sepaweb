<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabModelliBC.ascx.vb" Inherits="GestioneAutonoma_TabModelliBC" %>
<style type="text/css">
    .style1
    {
        height: 21px;
        width: 135px;
    }
    .style2
    {
        height: 26px;
        width: 135px;
    }
    .style3
    {
        height: 21px;
        width: 404px;
    }
    .style4
    {
        height: 26px;
        width: 404px;
    }
    .style5
    {
        height: 21px;
        width: 107px;
    }
    .style6
    {
        height: 26px;
        width: 107px;
    }
</style>
<asp:HiddenField ID="TextBox2" runat="server" />

<table style="width: 98%; height: 95px" id="TABLE1" >
    <tr>
        <td style="vertical-align: top; width: 132px; height: 81px; text-align: left">
            <div style="border-right: #ccccff solid; border-top: #ccccff solid; left: 0px; vertical-align: top;
                overflow: auto; border-left: #ccccff solid; width: 740px; border-bottom: #ccccff solid;
                top: 0px; height: 209px; text-align: left">
                <asp:DataGrid ID="DataGridModBeC" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="690px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_PROV" HeaderText="PROVVEDIMENTO" ReadOnly="True"
                            Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="PROVVEDIMENTO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_PROV") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DATA PROVV.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PROV") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DECORRENZA">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_DEC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="AUTORIZZATO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FL_AUTORIZZATA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                </asp:DataGrid></div>
            <table style="width: 80%">
                <tr>
                    <td style="width: 1148px">
                        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                            ReadOnly="True" Style="left: 13px; top: 197px" Width="572px">Nessuna Selezione</asp:TextBox></td>
                    <td style="width: 400px">
                        </td>
                </tr>
            </table>
            <asp:HiddenField ID="txtidModB" runat="server" Value="0" />
        </td>
        <td style="vertical-align: top; width: 144px; height: 81px; text-align: left">
            <asp:Image ID="imgAddConv" 
                onclick="document.getElementById('TabModelliBC1_TextBox2').value!='1';myOpacity2.toggle();" 
                runat="server" 
                ImageUrl="~/Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png" ToolTip="Aggiungi una convocazione"  /><br />
            <br />
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                Style="z-index: 102; left: 392px; top: 387px" ToolTip="Modifica" /><br />
            <br />
            &nbsp;</td>
    </tr>
</table>
<div id="ModelloB" 
    
    
    style="border: thin solid #3366ff; z-index: 200; left: 195px; vertical-align: top; width: 781px; position: absolute; top: 717px;
    height: 551px; background-color: lightgrey; text-align: left; visibility: visible;">
    <table style="width: 100%; height: 98%" width="100%" cellpadding ="0" cellspacing ="0">
        <tr>
            <td style="width: 751px; height: 36px">
                <strong><span style="vertical-align: top; font-family: Arial; text-align: center">
                    Esame e Inserimento servizi Gestione Autonoma</span></strong></td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left; width: 751px; height: 118px;">
                <table style="width: 85%" cellpadding ="0" cellspacing ="0">
                    <tr>
                        <td style="width: 176px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">ALLOGGI</span></td>
                        <td style="width: 232px; height: 21px">
                            <asp:Label ID="lblAlloggi" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="width: 157px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">mq.</span></td>
                        <td style="width: 265px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">
                                <asp:Label ID="lblMqAlloggi" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                    Width="59px"></asp:Label></span></td>
                        <td style="width: 710px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">AFFITTUARI</span></td>
                        <td style="width: 393px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">
                                <asp:Label ID="lblAffittuari" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                    Width="59px"></asp:Label></span></td>
                        <td style="width: 337px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">COMPETENZE</span></td>
                        <td style="width: 274px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">€.</span><asp:Label ID="lblCompetenze" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 176px; height: 26px;">
                            <span style="font-size: 9pt; font-family: Arial">NEGOZI</span></td>
                        <td style="width: 232px; height: 26px;">
                            <asp:Label ID="lblNegozi" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="width: 157px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">mq.</span></td>
                        <td style="width: 265px; height: 26px">
                            <asp:Label ID="lblMqNegozi" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="width: 710px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">SENZA TITOLO</span></td>
                        <td style="vertical-align: middle; width: 393px; height: 26px; text-align: left">
                            <asp:Label ID="lblSenzTitle" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="vertical-align: top; width: 337px; height: 26px; text-align: left">
                            <span style="font-size: 9pt; font-family: Arial; vertical-align: middle; text-align: left;">AFFITTO</span></td>
                        <td style="vertical-align: top; width: 274px; height: 26px; text-align: left">
                            <span style="font-size: 9pt; vertical-align: middle; text-align: right;"><span style="font-family: Arial">€.</span><asp:Label ID="lblAffitto" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></span></td>
                    </tr>
                    <tr>
                        <td style="width: 176px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">DIVERSI</span></td>
                        <td style="width: 232px; height: 26px">
                            <asp:Label ID="lblDiversi" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="width: 157px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">mq.</span></td>
                        <td style="width: 265px; height: 26px">
                            <asp:Label ID="lblMqDiversi" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="width: 710px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">OCC. ABUSIVE</span></td>
                        <td style="vertical-align: middle; width: 393px; height: 26px; text-align: left">
                            <asp:Label ID="lblOccAbus" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="vertical-align: top; width: 337px; height: 26px; text-align: left">
                            <span style="font-size: 9pt; font-family: Arial; vertical-align: middle; text-align: left;">SPESE</span></td>
                        <td style="vertical-align: top; width: 274px; height: 26px; text-align: left">
                            <span style="font-size: 9pt; font-family: Arial; vertical-align: middle; text-align: left;">€.<asp:Label ID="lblSpese" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></span></td>
                    </tr>
                    <tr>
                        <td style="width: 176px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">PROPRIETARI</span></td>
                        <td style="width: 232px; height: 26px">
                            <asp:Label ID="lblPropriet" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="59px"></asp:Label></td>
                        <td style="width: 157px; height: 26px">
                        </td>
                        <td style="width: 265px; height: 26px">
                        </td>
                        <td style="width: 710px; height: 26px">
                            <span style="font-size: 9pt; font-family: Arial">PERC. ABBUSIVE</span></td>
                        <td style="vertical-align: middle; width: 393px; height: 26px; text-align: left">
                            <asp:Label ID="lblPercAbusive" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Text="Label" Width="59px"></asp:Label></td>
                        <td style="vertical-align: top; width: 337px; height: 26px; text-align: left">
                            <span style="font-size: 9pt; font-family: Arial"></span></td>
                        <td style="vertical-align: top; width: 274px; height: 26px; text-align: left">
                            <span style="font-size: 9pt; font-family: Arial"></span>
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left; width: 751px;">
                <table style="width: 86%">
                    <tr>
                        <td style="width: 10%; height: 18px">
                            <strong><span style="font-size: 9pt; font-family: Arial">MOROSITA' AL</span></strong></td>
                        <td style="width: 10%; height: 18px">
                            <asp:Label ID="lblRifFinanz" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Label"
                                Width="81px" Font-Bold="True"></asp:Label></td>
                        <td style="width: 12%; height: 18px">
                            <span style="font-size: 9pt">€.</span><asp:Label ID="lblMorosita" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Label" Width="81px"></asp:Label></td>
                        <td style="width: 20%; height: 18px">
                            <span style="font-size: 9pt; font-family: Arial">
                                <asp:Label ID="lblPercMor" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                    Text="Label" Width="81px"></asp:Label><strong><span style="font-size: 10pt"></span></strong></span></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left; width: 751px; z-index: 400;">
                <table style="width: 90%">
                    <tr>
                        <td style="width: 91px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial">Provvedimento</span></td>
                        <td class="style3">
                            <asp:TextBox ID="txtNumProvv" runat="server" Font-Names="Arial" Font-Size="10pt"
                                MaxLength="12" TabIndex="10" Width="108px"></asp:TextBox>
                            </td>
                        <td class="style5">
                            <span style="font-size: 9pt; font-family: Arial">data</span></td>
                        <td class="style1">
                            <asp:TextBox ID="txtDataProvv" runat="server" Font-Names="Arial" Font-Size="10pt"
                                MaxLength="10" TabIndex="11" Width="100px"></asp:TextBox>&nbsp; <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataProvv"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                Style="z-index: 400; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                        <td style="width: 499px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial; vertical-align: bottom; text-align: left;">INQUILINI</span></td>
                        <td style="width: 499px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial; vertical-align: text-bottom; text-align: left;">FIRMATARI</span></td>
                        <td style="width: 499px; height: 21px">
                            <span style="font-size: 9pt; font-family: Arial; vertical-align: bottom; text-align: left;">PERCENTUALE</span></td>
                    </tr>
                    <tr>
                        <td style="width: 91px; height: 26px;">
                            <span style="font-size: 9pt; font-family: Arial">Decorrenza</span></td>
                        <td class="style4">
                            <asp:TextBox ID="txtDecorrenza" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="10"
                                TabIndex="12" Width="108px"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDecorrenza"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                Style="z-index: 400; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                        <td class="style6">
                            <span style="font-size: 9pt; font-family: Arial">stato</span></td>
                        <td class="style2">
                            <asp:DropDownList ID="cmbStatoAutor" runat="server" BackColor="White" CausesValidation="True"
                                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                                border-top: black 1px solid; z-index: 1; left: 76px; border-left: black 1px solid;
                                border-bottom: black 1px solid; top: 95px" TabIndex="13" Width="115px">
                            </asp:DropDownList></td>
                        <td style="width: 499px; height: 26px">
                                            <asp:TextBox ID="txtInquilini" runat="server" Font-Names="Arial" 
                                            Font-Size="10pt" MaxLength="10"
                                                TabIndex="14" Width="70px" ReadOnly="True"></asp:TextBox></td>
                        <td style="width: 499px; height: 26px">
                                        <asp:TextBox ID="txtFirmatari" runat="server" Font-Names="Arial" 
                                            Font-Size="10pt" MaxLength="10"
                                            TabIndex="15" Width="70px"></asp:TextBox></td>
                        <td style="vertical-align: middle; width: 499px; height: 26px; text-align: left">
                                        <asp:TextBox ID="txtPercentualeFirm" runat="server" Font-Names="Arial" 
                                            Font-Size="10pt" MaxLength="10"
                                            TabIndex="16" Width="70px" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 751px; height: 263px; text-align: right;">
                <table style="width: 100%">
                    <tr>
                        <td style="vertical-align: top; width: 92%; height: 22px; text-align: left">
                        </td>
                        <td style="vertical-align: top; text-align: center" >
                            <span style="font-size: 9pt; font-family: Arial"><strong>DATA SCAD.</strong></span></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: text-top; width: 92%; text-align: left; height: 211px;">
                <asp:CheckBoxList ID="chkListServizi" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="100%" CellPadding="3" CellSpacing="1" TabIndex="17" BackColor="White"  >
                </asp:CheckBoxList></td>
                        <td style="vertical-align: top; text-align: left; height: 211px;">
                            <table cellpadding ="2" cellspacing ="0">
                                <tr>
                                    <td style="width: 124px">
                                        <asp:TextBox ID="txtScadSrv1" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                            TabIndex="18" Width="66px"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                runat="server" ControlToValidate="txtScadSrv1" ErrorMessage="!" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="11pt" Style="z-index: 2; left: 683px;
                                                top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px; height: 24px;">
                                        <asp:TextBox ID="txtScadSrv2" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                            TabIndex="19" Width="66px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtScadSrv2"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                            Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:TextBox ID="txtScadSrv3" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                            TabIndex="20" Width="66px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtScadSrv3"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                            Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:TextBox ID="txtScadSrv4" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                            TabIndex="21" Width="66px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtScadSrv4"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                            Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px; height: 26px">
                                        <asp:TextBox ID="txtScadSrv5" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                            TabIndex="22" Width="66px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtScadSrv5"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                            Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:TextBox ID="txtScadSrv6" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                            TabIndex="23" Width="66px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtScadSrv6"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                            Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 124px">
                                        <asp:TextBox ID="txtScadSrv7" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="10"
                                            TabIndex="24" Width="66px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtScadSrv7"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt"
                                            Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                            <asp:ImageButton ID="btnSalva" runat="server" 
                            ImageUrl="~/NuoveImm/Img_SalvaVal.png" TabIndex="25" ToolTip="Salva" />
                <asp:ImageButton ID="imgBtnAnnulla" runat="server" 
                            ImageUrl="../NuoveImm/Img_AnnullaVal.png" TabIndex="25" ToolTip="Chiudi la finestra" />
            </td>
        </tr>
    </table>
                    <script type="text/javascript">

                    myOpacity2 = new fx.Opacity('ModelloB', { duration: 200 });
                    //myOpacity.hide();

                    if (document.getElementById('TabModelliBC1_TextBox2').value != '2') {
                        myOpacity2.hide(); 
                    }
                    </script>

</div>
<asp:HiddenField ID="Percentuale" runat="server" />
