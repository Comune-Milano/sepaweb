<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DispComune.aspx.vb" Inherits="ASS_DispAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Disponibilità Comune</title>
    <style type="text/css">
        .style1
        {
            width: 183px;
        }
        .style2
        {
            width: 119px;
        }
        .style3
        {
            width: 63px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="imgSalva" defaultfocus="cmbZona">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; text-align: left;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Disponibilità
                        U.I. Comune
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 108; left: 540px; position: absolute; top: 432px" 
                        ToolTip="Home" TabIndex="28" />
                    <asp:ImageButton ID="imgSalva" 
                        runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                            Style="z-index: 108; left: 456px; position: absolute; top: 432px; height: 20px;" 
                        ToolTip="Salva" TabIndex="27" />
                        &nbsp;</strong></span><br />
                    <br />
                    <br />
                    <table width="100%">
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                &nbsp;</td>
                            <td style="text-align: left">
                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                                    Font-Size="9pt" 
                                    Text="Clicca qui per visualizzare i dettagli dell&amp;#39;unità"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 23px; position: static; top: 397px" Text="Codice" Width="37px"></asp:Label></td>
                            <td style="text-align: left" valign="middle">
                                <asp:TextBox ID="txtCodice" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="20"
                                    TabIndex="1" Width="159px"></asp:TextBox>
                                &nbsp;
                                <asp:Label ID="Label17" runat="server" Font-Names="arial" 
                                    Font-Size="8pt" Style="z-index: 104;
                                    left: 23px; position: static; top: 397px" Text="Destinazione" 
                                    Width="71px"></asp:Label><asp:DropDownList ID="cmbTipologia" 
                                    runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="2" Width="125px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 94px" Text="Zona Decentramento *"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbZona" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="3" Width="80px">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Image ID="imgManutentivo" runat="server" ImageUrl="~/IMG/Alert.gif" />
                                <asp:Label ID="lblManutentivo" runat="server" Font-Names="arial" 
                                    Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 116px" Text="Stato Manutentivo Assente" 
                                    Font-Bold="True"></asp:Label>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 114px; text-align: left;">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 116px" Text="Disponibile Dal *"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtDisponibile" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="10" TabIndex="4" Width="73px"></asp:TextBox>&nbsp;
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDisponibile"
                                    Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial" Font-Size="8pt"
                                    TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chRiservata" runat="server" Font-Names="arial" 
                                    Font-Size="8pt" TabIndex="5" Text="Riservata Esigenze Sp. Note" 
                                    ToolTip="Unità riservata per esigenze speciali." />
                                <asp:TextBox ID="txtNoteRiservata" runat="server" Font-Names="ARIAL" 
                                    Font-Size="8pt" MaxLength="50"
                                    TabIndex="6" Width="220px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 114px">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="Image1" runat="server" AlternateText="Info" 
                                    ImageUrl="~/NuoveImm/INFO.png" Visible="False"  />
&nbsp;<asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 160px" Text="Tipo *"></asp:Label></td>
                            <td style="text-align: left;" class="style1">
                                <asp:DropDownList ID="cmbTipo" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" 
                                    Style="border: 1px solid black; z-index: 111; left: 413px; position: static; top: 198px; margin-left: 0px;" 
                                    TabIndex="7" Width="175px">
                                </asp:DropDownList>
                                </td>
                            <td style="width: 4px">
                                <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 164px; position: static; top: 160px" Text="N.Locali *" Width="45px"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtLocali" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="8" Width="40px"></asp:TextBox>
                                &nbsp;</td>
                            <td style="text-align: left;" class="style3">
                                <asp:Label ID="Label20" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 164px; position: static; top: 160px" Text="N.Servizi *" Width="54px"></asp:Label></td>
                            <td style="text-align: left" class="style2">
                                <asp:TextBox ID="txtServizi" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="9" Width="40px" Wrap="False"></asp:TextBox>
                                </td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 249px; position: static; top: 160px" Text="Sup.Conv"></asp:Label></td>
                            <td style="text-align: left;" class="style1">
                                <asp:TextBox ID="txtSuperficie" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="8" TabIndex="10" Width="44px" 
                                    
                                    ToolTip="Superficie Convenzionale in mq">0,00</asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSuperficie"
                                    ErrorMessage="!! 2 cifre dec." Font-Bold="False" Font-Names="ARIAL"
                                    Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b"></asp:RegularExpressionValidator>
                                </td>
                            <td style="width: 4px">
                                <asp:Label ID="Label21" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 249px; position: static; top: 160px" Text="Sup.Netta"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtSupNetta" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="8" TabIndex="11" Width="44px" 
                                    
                                    ToolTip="Superficie Netta in mq" Wrap="False">0,00</asp:TextBox>
                                </td>
                            <td style="text-align: left;" class="style3">
                                <asp:Label ID="Label22" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 249px; position: static; top: 160px" Text="Sup.Lorda"></asp:Label></td>
                            <td style="text-align: left" class="style2">
                                <asp:TextBox ID="txtSupLorda" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="8" TabIndex="12" Width="44px" 
                                    
                                    ToolTip="Superficie Lorda in mq" Wrap="False">0,00</asp:TextBox>
                                </td>
                            <td style="text-align: left" class="style2">
                                <asp:Label ID="Label23" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 249px; position: static; top: 160px" Text="Sup.C.le" 
                                    ToolTip="Superficie Commerciale in mq"></asp:Label></td>
                            <td style="text-align: left" class="style2">
                                <asp:TextBox ID="txtSupCle" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="8" TabIndex="13" Width="44px" 
                                    
                                    ToolTip="Superficie Lorda in mq" Wrap="False">0,00</asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 182px" Text="Indirizzo"></asp:Label></td>
                            <td style="text-align: left;" class="style1">
                                <asp:DropDownList ID="cmbTipoVia" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="14" Width="77px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtIndirizzo" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                                    MaxLength="20" TabIndex="15" Width="100px"></asp:TextBox></td>
                            <td style="width: 4px">
                                <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 96px; position: static; top: 182px" Text="Civico"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtCivico" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="16" Width="40px"></asp:TextBox></td>
                            <td style="text-align: left;" class="style3">
                                <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 182px; position: static; top: 182px" Text="Comune"></asp:Label></td>
                            <td style="text-align: left" class="style2">
                                <asp:TextBox ID="TXTCOMUNE" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="35"
                                    TabIndex="17" Width="99px"></asp:TextBox></td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Interno"></asp:Label></td>
                            <td style="text-align: left;" class="style1">
                                <asp:TextBox ID="txtInterno" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="18" Width="40px"></asp:TextBox></td>
                            <td style="width: 4px">
                                <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 96px; position: static; top: 204px" Text="Piano"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:DropDownList ID="cmbPiano" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 413px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 198px" TabIndex="19" Width="100px">
                                </asp:DropDownList></td>
                            <td style="text-align: left;" class="style3">
                                <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 177px; position: static; top: 204px" Text="Scala"></asp:Label></td>
                            <td style="text-align: left" class="style2">
                                <asp:TextBox ID="txtscala" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="20" Width="40px"></asp:TextBox></td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Foglio"></asp:Label></td>
                            <td style="text-align: left;" class="style1">
                                <asp:TextBox ID="txtfoglio" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="21" Width="40px"></asp:TextBox></td>
                            <td style="width: 4px">
                                <asp:Label ID="Label15" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Mappale"></asp:Label></td>
                            <td style="width: 131px; text-align: left;">
                                <asp:TextBox ID="txtmappale" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="22" Width="40px"></asp:TextBox></td>
                            <td style="text-align: left;" class="style3">
                                <asp:Label ID="Label16" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 104;
                                    left: 14px; position: static; top: 204px" Text="Sub"></asp:Label></td>
                            <td style="text-align: left" class="style2">
                                <asp:TextBox ID="txtsub" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="5"
                                    TabIndex="23" Width="40px"></asp:TextBox></td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                            <td style="text-align: left" class="style2">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <table width="95%">
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chAscensore" runat="server" Font-Names="arial" Font-Size="8pt"
                                    Style="position: static" TabIndex="24" Text="Con Ascensore" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chBar" runat="server" Font-Names="arial" Font-Size="8pt" Style="position: static"
                                    TabIndex="25" Text="Alloggio con Barriere Architettoniche" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:CheckBox ID="chHandicap" runat="server" Font-Names="arial" Font-Size="8pt" Style="position: static"
                                    TabIndex="26" Text="Destinato a portatori di handicap motorio" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="#C00000"
                                    Visible="False" Width="443px"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;<br />
                    <asp:TextBox ID="txtid" runat="server" Visible="False" TabIndex="-1"></asp:TextBox>
                    <asp:HiddenField ID="hiddenTipo" runat="server" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    <script type ="text/javascript" >
        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }
    </script>
</body>
</html>
