<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDebitori.aspx.vb"
    Inherits="MOROSITA_RicercaDebitori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
    var Uscita1;
    Uscita1 = 1;
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
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) {
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
            else {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
    }
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
<head id="Head1" runat="server">
    <title>RICERCA INQUILINI MOROSI</title>
    <style type="text/css">
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div id="Loading" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image22" runat="server" ImageUrl="Immagini/load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica2" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="Immagini/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="left: 0px; top: 0px">
                <tr>
                    <td style="left: 0px; position: absolute; top: 0px; height: 560px;">
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        &nbsp;Ricerca Debitori <em><span style="font-size: 10pt">(Saranno estratti solo 
                        intestatari sollecitati) </span></em></span></strong>
                        <table>
                            <tr>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblStrutturaAler" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" 
                                        Width="125px">Struttura</asp:Label>
                                    <br />
                                    <br />
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                                                    overflow: auto; border-left: navy 1px solid; width: 280px; border-bottom: navy 1px solid;
                                                    height: 60px">
                                                    <asp:CheckBoxList ID="CheckStrutture" runat="server" AutoPostBack="True" 
                                                        BorderColor="Black" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                                        Height="64px" RepeatLayout="Flow" Style="background-color: #ffffff" 
                                                        TabIndex="2" Width="256px">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td style="width: 15px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_AreaCanone" runat="server" Font-Bold="False" 
                                                    Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                                    Width="70px">Area Canone:</asp:Label>
                                            </td>
                                            <td>
                                                <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                                                    overflow: auto; border-left: navy 1px solid; width: 200px; border-bottom: navy 1px solid;
                                                    height: 60px">
                                                    <asp:CheckBoxList ID="CheckAreaCanone" runat="server" BorderColor="Black" 
                                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="64px" 
                                                        RepeatLayout="Flow" Style="background-color: #ffffff" TabIndex="3" 
                                                        Width="176px">
                                                        <asp:ListItem Selected="True" Value="1">PROTEZIONE</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2">ACCESSO</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="3">PERMANENZA</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="4">DECADENZA</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnDeselTutti" runat="server" 
                                                    ImageUrl="../MOROSITA/Immagini/Img_DeselezionaTutti_Piccolo.png" 
                                                    Style="z-index: 102; left: 160px; top: 392px" TabIndex="-1" 
                                                    ToolTip="Deseleziona tutte le Strutture" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:ImageButton ID="btnSelTutti" runat="server" 
                                                    ImageUrl="../MOROSITA/Immagini/Img_SelezionaTutti_Piccolo.png" 
                                                    Style="z-index: 102; left: 16px; top: 392px" TabIndex="-1" 
                                                    ToolTip="Seleziona tutte le Strutture" />
                                            </td>
                                            <td style="width: 15px">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnDeselTuttiAREA" runat="server" 
                                                    ImageUrl="../MOROSITA/Immagini/Img_DeselezionaTutti_Piccolo.png" 
                                                    Style="z-index: 102; left: 160px; top: 392px" TabIndex="-1" 
                                                    ToolTip="Deseleziona tutte le Aree Canone" />
                                                &nbsp;&nbsp;
                                                <asp:ImageButton ID="btnSelTuttiAREA" runat="server" 
                                                    ImageUrl="../MOROSITA/Immagini/Img_SelezionaTutti_Piccolo.png" 
                                                    Style="z-index: 102; left: 16px; top: 392px" TabIndex="-1" 
                                                    ToolTip="Seleziona tutte le Aree Canone" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Label ID="LblComplesso" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" 
                                        Width="125px">Complesso</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" 
                                        BackColor="White" Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 32px" TabIndex="4" Width="270px">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="45px">Edificio</asp:Label>
                                    <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" 
                                        BackColor="White" Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 64px" TabIndex="5" Width="270px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                        Width="125px">Indirizzo</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" 
                                        BackColor="White" Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 64px" TabIndex="6" Width="400px">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Label ID="lblCivico" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="35px">Civico</asp:Label>
                                    &nbsp;&nbsp;<asp:DropDownList ID="cmbCivico" runat="server" BackColor="White" 
                                        Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 64px" TabIndex="7" Width="142px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoRapporto" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                        Width="125px">Tipologia Rapporto</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipologiaRapporto" runat="server" AutoPostBack="True" 
                                        BackColor="White" Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 64px" TabIndex="8" Width="400px">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Label ID="lblStatoContratto" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                        Width="80px">Stato Contratto</asp:Label>
                                    &nbsp;<asp:DropDownList ID="cmbStato" runat="server" BackColor="White" 
                                        Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                            z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                            top: 64px" TabIndex="9" Width="105px">
                                        <asp:ListItem Selected="True"></asp:ListItem>
                                        <asp:ListItem>CHIUSO</asp:ListItem>
                                        <asp:ListItem>IN CORSO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoContr" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                        Width="125px">Tipo Contr.Specifico</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoContr" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Width="290px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px;">
                                </td>
                                <td>
                                    <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" 
                                        Width="125px">Cognome</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCognome" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="10" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del cognome " 
                                        Width="290px"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="lblNome" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" 
                                        Width="45px">Nome</asp:Label>
                                    <asp:TextBox ID="txtNome" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="11" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del nome" 
                                        Width="280px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <asp:Label ID="lblCod_Contratto" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                        Style="z-index: 100; left: 48px; top: 96px" Width="125px">Codice Rapporto</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodice" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px" TabIndex="12" 
                                        ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del codice rapporto" 
                                        Width="290px"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                        Style="z-index: 100; left: 48px; top: 96px" Width="70px">Tipologia U.I.</asp:Label>
                                    <asp:DropDownList ID="cmbTipologiaUI" runat="server" BackColor="White" 
                                        Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 96px" TabIndex="13" Width="260px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 10px; text-align: left">
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="lblDataDebito" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                        Width="125px">MESSA IN MORA SE:</asp:Label>
                                    <br />
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="CheckBoxMora" runat="server" BorderColor="Black" 
                                        Font-Names="Arial" Font-Size="7pt" ForeColor="Black" RepeatLayout="Flow" 
                                        Style="border-right: blue 1px double;
                                        border-top: blue 1px double; border-left: blue 1px double; border-bottom: blue 1px double" 
                                        TabIndex="14" Width="640px">
                                        <asp:ListItem Selected="True" Value="0">DISPONIBILE  IL SALDO AL 30.9.2009 PER ALLOGGI ERP, IMMOBILI DIVERSI E ALLOGGI LOCATI EX L. n.392/78 </asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">DISPONIBILE  IL SALDO AL 30.9.2009 PER  OCCUPANTI ABUSIVI </asp:ListItem>
                                        <asp:ListItem Selected="True" Value="2">MANCA IL SALDO AL 30.9.2009 PER     ALLOGGI ERP, IMMOBILI DIVERSI E ALLOGGI LOCATI EX L. n.392/78 </asp:ListItem>
                                        <asp:ListItem Selected="True" Value="3">MANCA IL SALDO AL 30.9.2009 PER       OCCUPANTI  ABUSIVI </asp:ListItem>
                                        <asp:ListItem Selected="True" Value="4">DISPONIBILE  IL SALDO AL 30.9.2009 E SENZA DEBITO SUCC. PER ALLOGGI ERP, IMMOBILI DIVERSI E ALLOGGI LOCATI EX L. n.392/78 </asp:ListItem>
                                        <asp:ListItem Selected="True" Value="5">DISPONIBILE  IL SALDO AL 30.9.2009 E SENZA DEBITO SUCC. PER  OCCUPANTI ABUSIVI </asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 10px; text-align: left">
                                    &nbsp;</td>
                                <td colspan="2" style="vertical-align: top; text-align: left;">
                                    <fieldset style="border: thin ridge #C0C0C0; width: 100%;">
                                        <legend class="style2">
                                            <asp:CheckBox ID="chkFiltraMor" runat="server" Font-Bold="True" 
                                                Font-Names="Arial" Font-Size="8pt" Text="Filtra bollette messe in mora" 
                                                Checked="True" />
                                        </legend>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="125px">Consistenza importo da:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtImporto1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        MaxLength="10" Style="z-index: 10; left: 408px;
                                        top: 171px" TabIndex="15" Width="120px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" ForeColor="Black" Style="text-align: right" TabIndex="-1" 
                                                        Text="€"></asp:Label>
                                                    &nbsp;</td>
                                                <td>
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" Style="z-index: 104; left: 48px; top: 64px" Width="15px">a:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtImporto2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        MaxLength="10" Style="z-index: 10; left: 408px;
                                            top: 171px" TabIndex="16" Width="120px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" ForeColor="Black" Style="text-align: right" TabIndex="-1" 
                                                        Text="€"></asp:Label>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBolScadute" runat="server" Font-Bold="False" 
                                                        Font-Names="Arial" Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" 
                                                        Width="125px">Num. bollette scadute da:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumBolletteDA" runat="server" Font-Names="arial" 
                                                        Font-Size="8pt" MaxLength="5" Style="z-index: 102; left: 592px; top: 192px" 
                                                        TabIndex="17" Width="90px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                                        ControlToValidate="txtNumBolletteA" Display="Dynamic" 
                                                        ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt" 
                                                        Style="left: 464px; top: 144px" TabIndex="-1" 
                                                        ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?">Valore Numerico</asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" Style="z-index: 104; left: 48px; top: 64px" Width="15px"> a:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNumBolletteA" runat="server" Font-Names="arial" 
                                                        Font-Overline="False" Font-Size="8pt" MaxLength="5" 
                                                        Style="z-index: 102; left: 592px; top: 192px" TabIndex="18" Width="90px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                        ControlToValidate="txtNumBolletteA" Display="Dynamic" 
                                                        ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt" 
                                                        Style="left: 464px; top: 144px" TabIndex="-1" 
                                                        ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?">Valore Numerico</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="125px">Competenza Bollette dal:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDataRIF_DAL" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" MaxLength="10" Style="left: 144px; top: 192px" TabIndex="19" 
                                                        ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                                        ControlToValidate="txtDataRIF_DAL" Display="Dynamic" 
                                                        ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False" 
                                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" 
                                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                        Font-Size="8pt" Style="z-index: 104; left: 48px; top: 64px" Width="15px"> al:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDataRIF_AL" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" MaxLength="10" Style="left: 144px; top: 192px" TabIndex="20" 
                                                        ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                                        ControlToValidate="txtDataRIF_AL" Display="Dynamic" 
                                                        ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False" 
                                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" 
                                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                                        Width="150px"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset></td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 10px; text-align: left">
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 64px" Width="125px">Stipula Contratto dal:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataS_DAL" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" MaxLength="10" Style="left: 144px; top: 192px" TabIndex="21" 
                                        ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="txtDataS_DAL" Display="Dynamic" 
                                        ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False" 
                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" 
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                        Width="150px"></asp:RegularExpressionValidator>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Style="z-index: 104; left: 48px; top: 64px" Width="15px"> al:</asp:Label>
                                    <asp:TextBox ID="txtDataS_AL" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        MaxLength="10" Style="left: 144px; top: 192px" TabIndex="22" 
                                        ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                        ControlToValidate="txtDataS_AL" Display="Dynamic" 
                                        ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False" 
                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" 
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                        Width="150px"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 10px; text-align: left">
                                </td>
                                <td style="vertical-align: top; text-align: left">
                                    <asp:Label ID="lblOrdinamento" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                        Style="z-index: 100; left: 88px; top: 256px" Width="72px">Ordina per:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="RBList1" runat="server" BackColor="White" 
                                        Font-Bold="True" Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 96px" TabIndex="23" Width="304px">
                                        <asp:ListItem Selected="True" Value="DEBITO DESC">DEBITO DECRESCENTE</asp:ListItem>
                                        <asp:ListItem Value="DEBITO ASC">DEBITO CRESCENTE</asp:ListItem>
                                        <asp:ListItem Value="INTESTATARIO2">INTESTATARIO</asp:ListItem>
                                        <asp:ListItem Value="COMUNE_UNITA,INDIRIZZO,CIVICO">COMUNE, INDIRIZZO, CIVICO</asp:ListItem>
                                        <asp:ListItem Value="RAPPORTI_UTENZA.COD_CONTRATTO">CODICE CONTRATTO</asp:ListItem>
                                        <asp:ListItem Value="UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE">CODICE UNITA</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                    <asp:ImageButton ID="btnCerca" runat="server" 
                                        ImageUrl="../NuoveImm/Img_AvviaRicerca.png" 
                                        Style="z-index: 111; left: 512px; top: 488px" ToolTip="Avvia Ricerca" 
                                        TabIndex="24" />
                                    <asp:ImageButton ID="btnAnnulla" runat="server" 
                                        ImageUrl="../NuoveImm/Img_Home.png" 
                                        Style="z-index: 106; left: 656px; top: 488px" TabIndex="25" 
                                        ToolTip="Home" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="txtID_STRUTTURE_SEL" runat="server" />
            <asp:HiddenField ID="txtID_STRUTTURE" runat="server" />
            <asp:HiddenField ID="txtID_AREE_SEL" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        document.getElementById('Loading').style.visibility = 'hidden';
    </script>
</body>
</html>
