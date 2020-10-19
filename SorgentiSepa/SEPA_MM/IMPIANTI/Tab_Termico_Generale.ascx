<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Generale.ascx.vb" Inherits="TabGenerale" %>

<div style="visibility: visible; overflow: auto; border-top-width: thin; border-left-width: thin; border-left-color: yellow; border-bottom-width: thin; border-bottom-color: yellow; border-top-color: yellow; border-right-width: thin; border-right-color: yellow;">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblDittaGestione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Ditta di Gestione</asp:Label></td>
            <td>
                <asp:TextBox ID="txtDittaGestione" runat="server" Font-Names="Arial" 
                    Font-Size="9pt" MaxLength="300"
                    Style="left: 80px; top: 88px;" TabIndex="8"
                    Width="410px" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
            <td style="width: 11px">
                &nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Num. Telefonico</asp:Label></td>
            <td>
                <asp:TextBox ID="txtNumTelefonico" runat="server" Font-Names="Arial" Font-Size="9pt"
                    MaxLength="50" TabIndex="9" Width="200px" ToolTip="Numero telefonico di Riferimento"></asp:TextBox></td>
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
                                Style="left: 504px; top: 152px" TabIndex="10" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataAccensione"
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
                                Style="left: 504px; top: 152px" TabIndex="11" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><br />
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
                    TabIndex="12" Width="50px"></asp:TextBox><br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtOreEsercizio"
                    Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                    TabIndex="-1" ValidationExpression="\d+" Width="80px"></asp:RegularExpressionValidator></td>
            <td style="width: 11px">
            </td>
            <td>
                <asp:Label ID="lblEstintori" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="60px">Estintori</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbEstintori" runat="server" AutoPostBack="True" BackColor="White"
                    Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="14" ToolTip="Presenza Estintori" Width="60px">
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
                    TabIndex="12" Width="50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtNumEstintori"
                    Display="Dynamic" ErrorMessage="Valore Numerico Intero" Font-Names="arial" Font-Size="8pt"
                    TabIndex="-1" ValidationExpression="\d+" Width="75px"></asp:RegularExpressionValidator></td>
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
                    border-bottom: black 1px solid; top: 192px" TabIndex="13" Width="110px" AutoPostBack="True">
            </asp:DropDownList></td>
            <td>
                &nbsp;</td>
            <td id="TAB3">
                <asp:Label ID="lblSerbatoio" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 472px;
                    top: 192px" Width="86px">Serbatoio</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbTipoSerbatoio" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="8pt" Style="border-right: black 1px solid;
                    border-top: black 1px solid; z-index: 111; left: 552px; border-left: black 1px solid;
                    border-bottom: black 1px solid; top: 192px" TabIndex="14" Width="90px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>INTERRATO</asp:ListItem>
                    <asp:ListItem>FUORI TERRA</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblCapacita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 528px; top: 192px"
                    Width="70px" TabIndex="-1">Capacità (mc)</asp:Label></td>
            <td>
                <asp:TextBox ID="txtCapacita" runat="server"
                    Font-Names="arial" Font-Size="9pt" MaxLength="10" Style="z-index: 102; left: 592px; top: 192px; text-align: right;" Width="50px" TabIndex="15"></asp:TextBox><asp:TextBox
                        ID="txtContatoreDPR" runat="server" Font-Names="Arial" Font-Size="9pt"
                        MaxLength="15" TabIndex="15" Width="50px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCapacita"
                    ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt" Style="left: 464px; top: 144px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                    Width="80px" Display="Dynamic" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
            <td style="width: 11px">
                </td>
            <td>
                <asp:Label ID="LblPotenza" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px;
                    top: 8px" Width="60px">Potenza (Kw)</asp:Label></td>
            <td>
                <asp:TextBox ID="txtPotenza" runat="server"
                    Font-Names="arial" Font-Size="9pt" MaxLength="10" Style="z-index: 102; left: 688px; top: 192px; text-align: right;" TabIndex="16" Width="60px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPotenza"
                    ErrorMessage="RegularExpressionValidator" Style="left: 168px;
                    top: 8px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="50px" Font-Names="Arial" Font-Size="8pt" Display="Dynamic" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="LblConsumo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 8px" Width="47px" TabIndex="-1">Consumo Medio</asp:Label></td>
            <td>
                <asp:TextBox ID="txtConsumo" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                    Style="z-index: 102; left: 688px; top: 192px; text-align: right;" TabIndex="17" Width="50px"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConsumo"
                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                        Font-Size="8pt" Style="left: 168px; top: 8px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" TabIndex="-1">Valore Numerico</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbldefangatori" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="60px">Defangatori</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbDefangatori" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="18" Width="56px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="S">SI</asp:ListItem>
                    <asp:ListItem Value="N">NO</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblTrattamento" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="86px">Trattamento Acque</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbTrattamento" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="19" ToolTip="Sistema Trattamento Acque" Width="56px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="S">SI</asp:ListItem>
                    <asp:ListItem Value="N">NO</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblCanaliFumo" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                    TabIndex="-1" Width="70px">Canali di Fumo</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbCanaliFumo" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="20" Width="50px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="S">SI</asp:ListItem>
                    <asp:ListItem Value="N">NO</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 11px">
            </td>
            <td>
                <asp:Label ID="lblVaso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px" TabIndex="-1"
                    Width="60px">Vaso Espansione</asp:Label></td>
            <td>
                <asp:DropDownList ID="cmbVasoEspansione" runat="server" BackColor="White"
                    Font-Names="arial" Font-Size="8pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                    z-index: 111; left: 80px; border-left: black 1px solid; border-bottom: black 1px solid;
                    top: 184px" TabIndex="21" Width="60px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="A">APERTO</asp:ListItem>
                    <asp:ListItem Value="C">CHIUSO</asp:ListItem>
                </asp:DropDownList></td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Edifici Alimentati" style="left: 0px; top: -30px" ForeColor="Black" Width="60px" TabIndex="-1"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtTotEdifici" runat="server" Enabled="False" Font-Names="Arial"
                    Font-Size="9pt" MaxLength="15" Style="left: 144px; top: 192px; text-align: right"
                    TabIndex="-1" Width="80px"></asp:TextBox></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblMq" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="86px">Totale mq riscaldato</asp:Label></td>
            <td>
                <asp:TextBox ID="txtTotMq" runat="server" Enabled="False" Font-Names="arial" Font-Size="8pt"
                    MaxLength="15" Style="left: 144px; top: 192px; text-align: right;" TabIndex="-1" Width="80px"></asp:TextBox></td>
            <td>
            </td>
            <td>
                <asp:Label ID="lblTotaleUI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="70px">Totale U.I.</asp:Label></td>
            <td>
                <asp:TextBox ID="txtTotUI" runat="server" Enabled="False" Font-Names="Arial" Font-Size="8pt"
                    MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                    Width="50px"></asp:TextBox></td>
            <td style="width: 11px;">
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="NOTE" Width="60px" TabIndex="-1"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="4000" Style="left: 8px;
                    top: 432px;" TextMode="MultiLine" Width="690px" TabIndex="22" Height="80px"></asp:TextBox></td>
        </tr>
    </table>
        
    <script type="text/javascript">


//    function Prova(obj) {
//        alert(document.getElementById('Tab_Morosita_Bollette_txtSel1').value);
//        alert(document.getElementById('Tab_Morosita_Bollette_txtSel1').value);
//        return;
//        
//         lstEdifici As System.Collections.Generic.List(Of Epifani.Edifici)
//        lstEdifici = CType(HttpContext.Current.Session.Item("LSTEDIFICI"), System.Collections.Generic.List(Of Epifani.Edifici))

//        
//        }    
    
</script>
    
</div>
