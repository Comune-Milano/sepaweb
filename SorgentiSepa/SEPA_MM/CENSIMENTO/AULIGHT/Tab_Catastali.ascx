<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Catastali.ascx.vb" Inherits="CENSIMENTO_Tab_Catastali" %>
<table style="width: 645px; height: 210px">
    <tr>
        <td style="width: 36px; height: 26px;">
            <asp:Label ID="Label40" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 25px" Width="32px">Sezione</asp:Label></td>
        <td style="width: 133px; height: 26px;">
            <asp:TextBox ID="TxtSezione" runat="server" Enabled="False" ReadOnly="True" Style="z-index: 10;
                left: 85px; top: 25px" TabIndex="20" Width="48px"></asp:TextBox></td>
        <td style="width: 26px; height: 26px;">
            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 163px; top: 25px" Width="32px">Foglio*</asp:Label></td>
        <td style="width: 168px; height: 26px;">
            <asp:TextBox ID="TxtFoglio" runat="server" Enabled="False" MaxLength="3" Style="z-index: 10;
                left: 195px; top: 25px" TabIndex="21" Width="48px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TxtFoglio"
                Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                left: 250px; top: 26px" ToolTip="E' possibile inserire solo numeri" ValidationExpression="\d+"
                Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 27px; height: 26px;">
            <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 265px; top: 25px" Width="48px">Particella*</asp:Label></td>
        <td style="width: 93px; height: 26px;">
            <asp:TextBox
                    ID="TxtNumero" runat="server" Enabled="False" MaxLength="5" Style="z-index: 10;
                    left: 325px; top: 25px" TabIndex="22" Width="48px"></asp:TextBox></td>
        <td style="width: 15px; height: 26px;">
            <asp:Label ID="Label13"
                        runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Height="16px"
                        Style="z-index: 100; left: 399px; top: 25px" Width="24px">Sub*</asp:Label></td>
        <td style="width: 87px; height: 26px;">
            <asp:TextBox
                            ID="TxtSub" runat="server" Enabled="False" MaxLength="3" Style="z-index: 10;
                            left: 424px; top: 25px" TabIndex="25" ToolTip="* NON ACCATASTATO - ? NON RILEVABILE"
                            Width="48px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 54px" Width="70px">Tipologia Cat.*</asp:Label></td>
        <td style="width: 51px" colspan ="2"><asp:DropDownList ID="DrLTipoCat" 
                runat="server" BackColor="White" Enabled="False"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 85px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 54px" TabIndex="26" Width="158px">
        </asp:DropDownList></td>
        <td style="width: 26px" colspan ="2">
            <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 312px; top: 54px" Width="117px">Perc. Poss.* (Millesimi)</asp:Label></td>
        <td style="width: 93px">
        <asp:TextBox ID="TxtPercPoss" runat="server" Enabled="False" MaxLength="4" Style="z-index: 10;
                left: 424px; top: 54px" TabIndex="27" Width="48px"></asp:TextBox><asp:Label ID="Label42"
                    runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Style="z-index: 100;
                    left: 478px; top: 60px" Width="9px">%</asp:Label><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator10" runat="server" ControlToValidate="TxtPercPoss"
                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                        left: 494px; top: 58px" ToolTip="E' possibile inserire solo numeri" ValidationExpression="\d+"
                        Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 15px">
        <asp:Label ID="Label17" runat="server"
                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 537px;
                    top: 111px; position: static;" Width="35px">Classe</asp:Label></td>
        <td style="width: 87px">
            <asp:DropDownList ID="DrLClasse" runat="server"
                        BackColor="White" Enabled="False" Font-Names="arial" 
                Font-Size="9pt" Height="20px"
                        Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111;
                        left: 572px; border-left: black 1px solid; border-bottom: black 1px solid; top: 111px"
                        TabIndex="28" Width="83px">
                    </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 36px; height: 26px;">
            <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 82px; position: static;" Width="40px">Categoria</asp:Label></td>
        <td style="width: 51px; height: 26px;" colspan ="5">
            <asp:DropDownList ID="DrLCategoria" runat="server" BackColor="White" Enabled="False"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 85px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 82px; position: static;" 
                TabIndex="29" Width="369px">
        </asp:DropDownList></td>
        <td style="width: 15px; height: 26px;">
            <asp:Label ID="Label34" runat="server" Font-Bold="False"
                                Font-Names="Arial" Font-Size="8pt" Height="8px" Style="z-index: 100; left: 494px;
                                top: 25px" Width="68px">Cod. Comune*</asp:Label></td>
        <td style="width: 87px; height: 26px;">
            <asp:TextBox ID="TxtCodComun" runat="server"
                                    Enabled="False" MaxLength="5" 
                Style="z-index: 10; left: 564px; top: 25px" TabIndex="30"
                                    Width="60px"></asp:TextBox></td>
        <td style="height: 26px">
        </td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 110px; position: static;" Width="41px">Sup. Mq</asp:Label></td>
        <td style="width: 133px">
            <asp:TextBox ID="TxtMq" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 86px; top: 110px; position: static;" TabIndex="31" Width="65px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                ControlToValidate="TxtMq" Display="Dynamic" ErrorMessage="!" Font-Bold="True"
                Height="1px" Style="z-index: 10; left: 158px; top: 111px; position: static;" ToolTip="E' possibile inserire solo numeri"
                ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 26px">
            <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 180px; top: 111px; position: static;" Width="72px">Sup. Catastale</asp:Label></td>
        <td style="width: 168px">
        <asp:TextBox
                    ID="TxtSupCat" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                    left: 253px; top: 111px; position: static;" TabIndex="32" Width="65px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtSupCat"
                Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                left: 324px; width: 1px; top: 111px; position: static;" ToolTip="E' possibile inserire solo numeri"
                ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" ValidationGroup="A"></asp:RegularExpressionValidator></td>
        <td style="width: 27px">
            <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 341px; top: 111px; position: static;" Width="24px">Vani</asp:Label></td>
        <td style="width: 93px">
        <asp:TextBox
                    ID="TxtVani" runat="server" Enabled="False" MaxLength="5" Style="z-index: 10;
                    left: 364px; top: 111px; position: static;" TabIndex="33" Width="53px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TxtVani"
                Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                left: 405px; top: 111px; position: static;" ToolTip="E' possibile inserire solo numeri" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$"
                Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 15px">
            <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 420px; top: 111px; position: static;" Width="50px">Cubatura</asp:Label></td>
        <td style="width: 87px">
        <asp:TextBox
                    ID="TxtCubatura" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                    left: 471px; top: 111px; position: static;" TabIndex="34" Width="60px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtCubatura"
                Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                left: 526px; top: 111px; position: static;" ToolTip="E' possibile inserire solo numeri" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$"
                Width="1px"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 140px; position: static;" Width="65px">Stato Catast.</asp:Label></td>
        <td style="width: 120px" colspan ="3">
            <asp:DropDownList
                    ID="DrLStatoCatast" runat="server" BackColor="White" Enabled="False" Font-Names="arial"
                    Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 86px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 140px; position: static;" TabIndex="35" Width="239px">
        </asp:DropDownList></td>
        <td style="width: 26px">
            <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 326px; top: 141px; position: static;" Width="68px">Val. Imponibile</asp:Label></td>
        <td style="width: 93px">
            <asp:TextBox ID="TxtValImponibile" runat="server" Enabled="False" MaxLength="10"
                Style="z-index: 10; left: 408px; top: 141px; position: static;" 
                TabIndex="36" Width="53px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TxtValImponibile"
                Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                left: 486px; top: 141px; position: static;" ToolTip="E' possibile inserire solo numeri" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$"
                Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 15px">
            <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 509px; top: 141px; position: static;" Width="64px">Val. Bilancio</asp:Label></td>
        <td style="width: 87px">
            <asp:TextBox ID="TxtValBil" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 572px; top: 140px; position: static;" TabIndex="37" Width="60px"></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator14" runat="server" ControlToValidate="TxtValBil"
                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                    left: 657px; top: 142px; text-align: left; position: static;" ToolTip="E' possibile inserire solo numeri"
                    ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" Width="1px"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 170px" Width="60px">Imm. Storico</asp:Label></td>
        <td style="width: 133px">
            <asp:DropDownList ID="DrLImmStorico" runat="server" BackColor="White" Enabled="False"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 86px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 170px" TabIndex="38" Width="62px">
            </asp:DropDownList></td>
        <td style="width: 26px">
            <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 175px; top: 171px" Width="74px">Rendita Storica</asp:Label></td>
        <td style="width: 168px">
            <asp:TextBox ID="TxtRendStorica" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 248px; top: 171px" TabIndex="39" Width="65px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtRendStorica"
                Display="Dynamic" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                left: 326px; top: 171px" ToolTip="E' possibile inserire solo numeri" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$"
                Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 27px">
            <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 363px; top: 171px" Width="44px">Rendita</asp:Label></td>
        <td style="width: 93px">
            <asp:TextBox ID="TxtRendita" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 408px; top: 171px" TabIndex="40" Width="53px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                ControlToValidate="TxtRendita" Display="Dynamic" ErrorMessage="!" Font-Bold="True"
                Height="1px" Style="z-index: 10; left: 486px; top: 171px" ToolTip="E' possibile inserire solo numeri"
                ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" Width="1px"></asp:RegularExpressionValidator></td>
        <td style="width: 15px">
            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 516px; top: 171px" Width="63px">Esente ICI</asp:Label></td>
        <td style="width: 87px">
            <asp:DropDownList ID="DrlEsenzICI" runat="server" BackColor="White" Enabled="False"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 572px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 171px" TabIndex="41" Width="60px">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 198px; position: static;" Width="64px">Reddito Agr.</asp:Label></td>
        <td style="width: 133px">
            <asp:TextBox ID="TxtRedAgrari" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 85px; top: 198px; position: static;" TabIndex="42" Width="78px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtRedAgrari"
                ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10; left: 168px;
                top: 198px; position: static;" ToolTip="E' possibile inserire solo numeri" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$"
                Width="1px" Display="Dynamic"></asp:RegularExpressionValidator></td>
        <td style="width: 26px">
            <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 178px; top: 198px; position: static;" Width="66px">Reddito Dom.</asp:Label></td>
        <td style="width: 168px">
            <asp:TextBox ID="TxtRedDomini" runat="server" Enabled="False" MaxLength="10"
                Style="z-index: 10; left: 248px; top: 198px; position: static;" 
                TabIndex="43" Width="65px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TxtRedDomini"
                ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10; left: 326px;
                top: 198px; position: static;" ToolTip="E' possibile inserire solo numeri" ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$"
                Width="1px" Display="Dynamic"></asp:RegularExpressionValidator></td>
        <td style="width: 27px">
            <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 362px; top: 198px; position: static;" Width="40px">Inagibile</asp:Label></td>
        <td style="width: 93px">
            <asp:DropDownList ID="DrLInagibile" runat="server" BackColor="White" Enabled="False"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 409px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 198px; position: static;" 
                TabIndex="44" Width="62px">
            </asp:DropDownList></td>
        <td style="width: 15px">
        </td>
        <td style="width: 87px">
        </td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 226px; position: static;" Width="60px">Zona Cens.</asp:Label></td>
        <td style="width: 133px">
            <asp:TextBox ID="TxtZonaCens" runat="server" Enabled="False" MaxLength="20" Style="z-index: 10;
                left: 85px; top: 226px; position: static;" TabIndex="45" Width="78px"></asp:TextBox></td>
        <td style="width: 26px">
            <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 179px; top: 226px; position: static;" Width="65px">Micro. Cens.</asp:Label></td>
        <td style="width: 168px">
            <asp:TextBox ID="TxtMicrozCens" runat="server" CssClass="CssMaiuscolo" Enabled="False"
                MaxLength="20" 
                Style="z-index: 10; left: 248px; top: 225px; position: static;" TabIndex="46" 
                Width="65px"></asp:TextBox></td>
        <td style="width: 27px">
            <asp:Label ID="Label39" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 10; left: 362px; top: 219px; position: static;" Width="30px">Note</asp:Label></td>
        <td style="width: 54px; vertical-align: top; text-align: left;" colspan ="3" rowspan ="2">
        <asp:TextBox style="z-index: 50; left: 361px; position: static; top: 233px;" 
                CssClass="CssMaiuscolo" Enabled="False" ID="TxtNote" MaxLength="200" 
                runat="server" TabIndex="49" TextMode="MultiLine" Width="231px" Height="47px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 253px; position: static;" Width="72px">Data Acquisiz.</asp:Label></td>
        <td style="width: 133px">
            <asp:TextBox ID="TxtDataAcquisiz" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 85px; top: 253px; position: static;" TabIndex="47" 
                ToolTip="dd/mm/YYYY" Width="78px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                ControlToValidate="TxtDataAcquisiz" ErrorMessage="!" Font-Bold="True" Height="1px"
                Style="z-index: 10; left: 170px; top: 253px; position: static;" ToolTip="Inserire una data valida"
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Width="1px" Display="Dynamic"></asp:RegularExpressionValidator></td>
        <td style="width: 26px">
            <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 180px; top: 252px; position: static;" Width="70px">Data Fine Val.</asp:Label></td>
        <td style="width: 168px">
        <asp:TextBox ID="TxtDataFineVal" runat="server" Enabled="False" MaxLength="10" Style="z-index: 10;
                left: 248px; top: 252px; position: static;" TabIndex="48" 
                ToolTip="dd/mm/YYYY" Width="65px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                ControlToValidate="TxtDataFineVal" ErrorMessage="!" Font-Bold="True" Height="1px"
                Style="z-index: 10; left: 327px; top: 252px; position: static;" ToolTip="Inserire una data valida"
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Width="1px" Display="Dynamic"></asp:RegularExpressionValidator></td>
        <td style="width: 27px">
            </td>
        <td style="width: 360px">
        </td>
        <td style="width: 175px">
        </td>
        <td style="width: 13px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 36px">
            <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 280px; position: static;" Width="64px">Num. Partita</asp:Label></td>
        <td style="width: 133px">
            <asp:TextBox ID="TxtNumPartita" runat="server" Enabled="False" MaxLength="20" Style="z-index: 10;
                left: 85px; top: 280px; position: static;" TabIndex="50" Width="79px"></asp:TextBox></td>
        <td style="width: 26px">
            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 182px; top: 281px;" Width="65px">Ditta</asp:Label></td>
        <td style="width: 168px" colspan ="5">
            <asp:TextBox ID="TxtDitta" runat="server" CssClass="CssMaiuscolo" Enabled="False"
                MaxLength="20" Style="z-index: 10; left: 247px; width: 350px; top: 282px;" 
                TabIndex="51"></asp:TextBox></td>
        <td>
        </td>
    </tr>
</table>
