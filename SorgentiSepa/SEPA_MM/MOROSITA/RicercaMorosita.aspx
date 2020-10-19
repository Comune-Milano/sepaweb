<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaMorosita.aspx.vb" Inherits="MOROSITA_RicercaMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<script language="javascript" type="text/javascript">

function CompletaData(e,obj) {
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

<head id="Head1" runat="server">
    <title>RICERCA MOROSITA</title>
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</head>

<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <table style="left: 0px; top: 0px">
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        Ricerca Morosità</span></strong> &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <table>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    <asp:Label ID="lblStrutturaAler" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" Width="130px">Struttura:</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="cmbStruttura" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 32px" TabIndex="2" Width="580px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="LblComplesso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; top: 32px" Width="130px">Complesso:</asp:Label></td>
                                <td style="height: 21px">
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 32px" TabIndex="3"
            Width="580px">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; top: 64px" Width="130px">Edificio:</asp:Label></td>
                                <td style="height: 21px">
        <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 64px" TabIndex="4"
            Width="580px" AutoPostBack="True">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="130px">Indirizzo:</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="cmbIndirizzo" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 64px" TabIndex="5"
            Width="400px" AutoPostBack="True">
                                </asp:DropDownList>&nbsp;&nbsp;
                                    <asp:Label ID="lblCivico" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="48px">Civico:</asp:Label>
                                    <asp:DropDownList ID="cmbCivico" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 64px" TabIndex="6"
            Width="115px">
                                </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
            Width="140px">Cognome o ragione sociale:</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtCognome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="7" Width="250px" 
                                        
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del cognome o ragione sociale"></asp:TextBox>
                                    <asp:Label ID="lblNome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" 
                                        Style="z-index: 100; left: 48px; top: 96px; text-align: center;" Width="60px">Nome:</asp:Label>
                                    <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="8" Width="258px" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del cognome "></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblCod_Contratto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" Width="130px">Codice Rapporto:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCodice" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px" TabIndex="9" Width="250px" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del codice rapporto"></asp:TextBox>
                                    <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px; text-align: center;"
                                        Width="90px">Tipologia U.I.:</asp:Label>
        <asp:DropDownList ID="cmbTipologiaUI" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 96px" TabIndex="10"
            Width="228px">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <br />
                                    <asp:Label ID="lblDataDebito" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="130px">Consistenza debito dal:</asp:Label></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                    <asp:TextBox ID="txtDataDAL" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                        Style="left: 144px; top: 192px" TabIndex="11" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                            <td style="width: 1px;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDAL"
                                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="152px"></asp:RegularExpressionValidator></td>
                                            <td style="width: 3px;">
                                                &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td>
                                    <asp:Label ID="lblDataP2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 48px; top: 64px" Width="30px"> al:</asp:Label></td>
                                            <td>
                                    <asp:TextBox ID="txtDataAL" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                        Style="left: 144px; top: 192px" TabIndex="12" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                            <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataAL"
                                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="150px"></asp:RegularExpressionValidator></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="lblProtocollo" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                        Width="120px">Protocollo:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtProtocollo" runat="server" MaxLength="50" Style="z-index: 10;
                                        left: 408px; text-transform: uppercase; top: 171px" TabIndex="13" ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del protocollo"
                                        Width="544px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <br />
                                    <asp:Label ID="lblDataProtocollo" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                        Width="120px">Data Protocollo:</asp:Label><br />
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataDAL_P" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="14" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td style="width: 1px;">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataDAL_P"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="152px"></asp:RegularExpressionValidator></td>
                                            <td style="width: 3px;">
                                                &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                    Style="z-index: 104; left: 48px; top: 64px" Width="30px"> al:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtDataAL_P" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                                    Style="left: 144px; top: 192px" TabIndex="15" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataAL_P"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="150px"></asp:RegularExpressionValidator></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="lblTipoLettera" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                        Width="120px">Messa in mora se:</asp:Label></td>
                                <td>
                                        <asp:CheckBoxList ID="CheckBoxMora" runat="server" BorderColor="Black" Font-Names="Arial"
                                            Font-Size="7pt" ForeColor="Black" RepeatLayout="Flow" Style="border-right: blue 1px double;
                                            border-top: blue 1px double; border-left: blue 1px double; border-bottom: blue 1px double"
                                            TabIndex="16" Width="580px">
                                            <asp:ListItem Selected="True" Value="AB">DISPONIBILE  IL SALDO AL 30.9.2009</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="CD">MANCA IL SALDO AL 30.9.2009</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="EF">DISPONIBILE  IL SALDO AL 30.9.2009 E SENZA DEBITO SUCCESSIVO</asp:ListItem>
                                        </asp:CheckBoxList></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                    <asp:Label ID="lblOrdinamento" runat="server" Font-Bold="False" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 88px;
                        top: 256px" Width="72px">Ordina per:</asp:Label></td>
                                <td>
                    <asp:RadioButtonList ID="RBList1" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" 
                                        Style="left: 248px; top: 232px; border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid; border-left: lightsteelblue 1px solid; border-bottom: lightsteelblue 1px solid; vertical-align: middle; text-align: left;" 
                                        TabIndex="17" Height="1px" Width="400px" RepeatDirection="Horizontal">
                        <asp:ListItem Value="DATA_PROTOCOLLO_ORD DESC" Selected="True">DATA PROTOCOLLO</asp:ListItem>
                        <asp:ListItem Value="PROTOCOLLO_ALER ASC">NUM. PROTOCOLLO</asp:ListItem>
                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                                <td style="vertical-align: top; text-align: left" class="style1">
                                </td>
                                <td class="style1">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="vertical-align: top; text-align: left">
                    </td>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 512px; top: 448px" 
                        ToolTip="Avvia Ricerca"  />
                                    &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 656px; top: 448px" ToolTip="Home" TabIndex="1" /></td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
        
</body>
</html>
