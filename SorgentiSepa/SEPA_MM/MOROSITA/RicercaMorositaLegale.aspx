<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaMorositaLegale.aspx.vb" Inherits="MOROSITA_RicercaMorositaLegale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<script language="javascript" type="text/javascript">

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }
    
    
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
//        o.value = o.value.replace('.', ',');
       // document.getElementById('txtModificato').value = '1';
    }
    

	function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) 
        {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) 
            {
                var decimali = a.substring(a.length, a.length - 2);
                var dascrivere = a.substring(a.length - 3, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {

                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato 
                dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
            }
                risultato = dascrivere + risultato + ',' + decimali
                //document.getElementById(obj.id).value = a.replace('.', ',')
                document.getElementById(obj.id).value = risultato
            }
            else 
            {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        }


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
</head>

<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <table style="left: 0px; top: 0px">
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        Ricerca Affidamento
                        della Pratica al Legale</span></strong> &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;<br />
                    <br />
                    <span style="color: #0000ff"><strong><span style="font-size: 11pt; font-family: Arial">
                        <em>Ricerca Inquilino</em></span></strong></span> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <table>
                            <tr>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblStrutturaAler" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" Width="125px">Struttura:</asp:Label><br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                                                    overflow: auto; border-left: navy 1px solid; width: 280px; border-bottom: navy 1px solid;
                                                    height: 80px">
                                                    <asp:CheckBoxList ID="CheckStrutture" runat="server" AutoPostBack="True" BorderColor="Black"
                                                        Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Height="64px" RepeatLayout="Flow"
                                                        Style="background-color: #ffffff" TabIndex="2" Width="256px">
                                                    </asp:CheckBoxList></div>
                                            </td>
                                            <td style="width: 15px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_AreaCanone" runat="server" Font-Bold="False" Font-Names="Arial"
                                                    Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="70px">Area Canone:</asp:Label><br />
                                                <br />
                                                <br />
                                                <br />
                                            </td>
                                            <td>
                                                <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                                                    overflow: auto; border-left: navy 1px solid; width: 200px; border-bottom: navy 1px solid;
                                                    height: 80px">
                                                    <asp:CheckBoxList ID="CheckAreaCanone" runat="server" BorderColor="Black" Font-Names="Arial"
                                                        Font-Size="9pt" ForeColor="Black" Height="64px" RepeatLayout="Flow" Style="background-color: #ffffff"
                                                        TabIndex="3" Width="176px">
                                                        <asp:ListItem Selected="True" Value="1">PROTEZIONE</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2">ACCESSO</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="3">PERMANENZA</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="4">DECADENZA</asp:ListItem>
                                                    </asp:CheckBoxList></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnDeselTutti" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_DeselezionaTutti_Piccolo.png"
                                                    Style="z-index: 102; left: 160px; top: 392px" TabIndex="-1" ToolTip="Deseleziona tutte le Strutture" />
                                                &nbsp;&nbsp;
                                                <asp:ImageButton ID="btnSelTutti" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_SelezionaTutti_Piccolo.png"
                                                    Style="z-index: 102; left: 16px; top: 392px" TabIndex="-1" ToolTip="Seleziona tutte le Strutture" /></td>
                                            <td style="width: 15px">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnDeselTuttiAREA" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_DeselezionaTutti_Piccolo.png"
                                                    Style="z-index: 102; left: 160px; top: 392px" TabIndex="-1" ToolTip="Deseleziona tutte le Aree Canone" />
                                                &nbsp;&nbsp;
                                                <asp:ImageButton ID="btnSelTuttiAREA" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_SelezionaTutti_Piccolo.png"
                                                    Style="z-index: 102; left: 16px; top: 392px" TabIndex="-1" ToolTip="Seleziona tutte le Aree Canone" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20px;">
                                </td>
                                <td>
        <asp:Label ID="LblComplesso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; top: 32px" Width="130px">Complesso:</asp:Label></td>
                                <td>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 32px" TabIndex="4"
            Width="550px">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px;">
                                </td>
                                <td>
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; top: 64px" Width="130px">Edificio:</asp:Label></td>
                                <td>
        <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 64px" TabIndex="5"
            Width="550px" AutoPostBack="True">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="130px">Indirizzo:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 64px" TabIndex="6" Width="400px">
                                    </asp:DropDownList><asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="35px">Civico:</asp:Label><asp:DropDownList
                                            ID="cmbCivico" runat="server" BackColor="White" Font-Names="arial" Font-Size="10pt"
                                            Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                            z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                            top: 64px" TabIndex="7" Width="142px">
                                        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px;">
                                </td>
                                <td>
        <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
            Width="130px">Cognome:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCognome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        text-transform: uppercase; top: 171px" TabIndex="8" ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del cognome "
                                        Width="270px"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="lblNome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" Width="45px">Nome:</asp:Label><asp:TextBox
                                            ID="txtNome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px; text-transform: uppercase;
                                            top: 171px" TabIndex="9" ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del nome"
                                            Width="263px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblCod_Contratto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" Width="130px">Codice Rapporto:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCodice" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px" TabIndex="10" ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del codice rapporto"
                                        Width="270px"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                        Width="70px">Tipologia U.I.</asp:Label><asp:DropDownList ID="cmbTipologiaUI" runat="server"
                                            BackColor="White" Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                            border-bottom: black 1px solid; top: 96px" TabIndex="11" Width="245px">
                                        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                    &nbsp; &nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="130px">Consistenza importo da:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtImporto1" runat="server" MaxLength="10" Style="z-index: 10; left: 408px;
                                        top: 171px" TabIndex="12" Width="120px"></asp:TextBox><asp:Label ID="Label3" runat="server"
                                            Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                            TabIndex="-1" Text="€" Width="16px"></asp:Label>&nbsp;
                                    <asp:Label ID="Label4" runat="server"
                                                Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Style="z-index: 104; left: 48px;
                                                top: 64px" Width="15px">a:</asp:Label><asp:TextBox ID="txtImporto2" runat="server"
                                                    MaxLength="10" Style="z-index: 10; left: 408px; top: 171px" TabIndex="13" Width="120px"></asp:TextBox><asp:Label
                                                        ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label></td>
                            </tr><tr>
                                <td style="width: 20px; height: 25px;">
                                </td>
                                <td style="vertical-align: top; text-align: left; height: 25px;">
                                </td>
                                <td style="height: 25px">
                                </td>
                            </tr>
                        </table>
                    <strong><em><span style="font-size: 11pt; color: #0000ff; font-family: Arial">Ricerca
                        Legale</span></em></strong>&nbsp;<br />
                    <table>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:Label ID="lblTribunale" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                        TabIndex="-1" Width="130px">Tribunale di Competenza:</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbTribunali" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 56px" TabIndex="13" Width="550px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="lblBolScadute" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="130px">Num. pratiche trattate da:</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtNumPraticheDA" runat="server" Font-Names="arial" Font-Size="9pt"
                                        MaxLength="5" Style="z-index: 102; left: 592px; top: 192px" TabIndex="14" Width="90px"></asp:TextBox><asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtNumPraticheDA"
                                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                            Font-Size="8pt" Style="left: 464px; top: 144px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                            Width="96px">Valore Numerico</asp:RegularExpressionValidator>
                                    &nbsp;
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 48px; top: 64px" Width="15px"> a:</asp:Label><asp:TextBox
                                            ID="txtNumPraticheA" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="5"
                                            Style="z-index: 102; left: 592px; top: 192px" TabIndex="15" Width="90px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumPraticheA"
                                                Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                                Font-Size="8pt" Style="left: 464px; top: 144px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                                Width="128px">Valore Numerico</asp:RegularExpressionValidator></td>
                            </tr>
                    </table>
                    <table>
                            <tr>
                                <td style="width: 20px; height: 30px;">
                                </td>
                                <td style="width: 130px; height: 30px;">
                                </td>
                                <td style="height: 30px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td style="width: 130px;">
                    </td>
                                <td>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 512px; top: 448px" 
                        ToolTip="Avvia Ricerca" OnClick="btnCerca_Click" />
                                    &nbsp; &nbsp; &nbsp;
                                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 656px; top: 448px" ToolTip="Home" TabIndex="1" /></td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
    </div>
        <asp:HiddenField ID="txtID_STRUTTURE"       runat="server" />   
        <asp:HiddenField ID="txtID_STRUTTURE_SEL"   runat="server" />      
        <asp:HiddenField ID="txtID_AREE_SEL"        runat="server" />   
    </form>
    
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
        
</body>
</html>
