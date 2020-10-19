<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProspettoRateiz2.aspx.vb"
    Inherits="RATEIZZAZIONE_ProspettoRateiz2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calcolo Rateizzazione Bolletta</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Arial;
            font-size: 12pt;
            font-weight: bold;
        }
        
        .style2
        {
            color: #0000FF;
            font-family: Arial;
            font-size: 10pt;
            font-weight: bold;
        }
        
        .style3
        {
            font-family: Arial;
            font-size: 8pt;
        }
        
        .style4
        {
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
            text-align: right;
        }
        
        .style5
        {
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
            width: 400px;
        }
    </style>
    <script type="text/javascript">

        function Nascondi() {
            document.getElementById('btnSa').style.visibility = 'hidden';
            document.getElementById('btnSa').style.position = 'absolute';
            document.getElementById('btnSa').style.left = '-100px';
            document.getElementById('btnSa').style.display = 'none';
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

        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        };
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }
        function AutoDecimal2(obj) {

            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
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
                else
                    document.getElementById(obj.id).value = ''
            }
        };

        function Scelta() {
            if (document.getElementById("rdbType_1").checked == true) {
                document.getElementById("lblN").style.visibility = 'hidden';
                document.getElementById("txtNRate").style.visibility = 'hidden';
                document.getElementById("txtNRate").value = '';

                document.getElementById("lblImp").style.visibility = 'visible';
                document.getElementById("lblEur").style.visibility = 'visible';
                document.getElementById("txtImpRate").style.visibility = 'visible';


            }
            else {
                document.getElementById("lblN").style.visibility = 'visible';
                document.getElementById("txtNRate").style.visibility = 'visible';
                document.getElementById("lblImp").style.visibility = 'hidden';
                document.getElementById("lblEur").style.visibility = 'hidden';
                document.getElementById("txtImpRate").style.visibility = 'hidden';
                document.getElementById("txtImpRate").value = '';

            }
        }

        function Sottrai() {

            if (parseFloat(document.getElementById("txtVersAnticipo").value.replace('.', '').replace(',', '.')) <= parseFloat(document.getElementById("txtImporto").value.replace('.', '').replace(',', '.'))) {
                var risultato
                risultato = document.getElementById("txtImporto").value.replace('.', '').replace(',', '.') - document.getElementById("txtVersAnticipo").value.replace('.', '').replace(',', '.');
                risultato = risultato.toFixed(2);

                if (risultato.substring(risultato.length - 3, 0).length >= 4) {
                    var decimali = risultato.substring(risultato.length, risultato.length - 2);
                    var dascrivere = risultato.substring(risultato.length - 3, 0);
                    var risultNew = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultNew = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultNew;
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                    }
                    risultNew = dascrivere + risultNew + ',' + decimali;
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    //document.getElementById(obj.id).value = risultNew
                }
                else {
                    risultNew = risultato.replace('.', ',');
                }

                document.getElementById("txtCapitale").value = risultNew;
                document.getElementById("CapitaleRateiz").value = risultNew;

            }

            else {

                alert('Inserisci un importo valido!');
                document.getElementById("txtVersAnticipo").value = '0,00';
                Sottrai();
            }

        }

        function ControlDate(obj) {
            if (obj.value != '') {
                var currentTime = new Date();
                var myDate = obj.value.substring(6, 11) + obj.value.substring(3, 5) + obj.value.substring(0, 2)
                if (myDate < currentTime.getFullYear() + '0' + (currentTime.getMonth() + 1) + '0' + currentTime.getDate()) {
                    alert('La data deve essere uguale o superiore alla data odierna!');
                    document.getElementById(obj.id).value = '';
                }
            }
        }

        function PrintDoc() {
            window.open('PrintLetter.aspx?IDDICH=' + document.getElementById('idDich').value + '&IDBOLL=<%=vIdBolletta %>', 'letDebt', 'height=598,width=920,scrollbars=no');
             
        }
        function ConfirmSave() {

            var chiediConferma
            chiediConferma = window.confirm("Attenzione...\nSi è sicuri di voler salvare la rateizzazione della bolletta?");
            if (chiediConferma == true) {
                document.getElementById('txtModificato').value = '0';
                document.getElementById('ConfermaSalva').value = '1';

            }
            else {

                document.getElementById('ConfermaSalva').value = '0';

            }
        }

    </script>
</head>
<body>
    <div id="splash" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500;">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../NuoveImm/sfondo2.png');">
            <table style="width: 100%; height: 100%; font-family: Arial; font-size: 8pt;">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="../immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1" style="background-color: #800000; text-align: center;">
                Calcolo Piano di Rateizzazione Straordinaria
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF"
                                        class="style2">
                                        <span class="style2">Riepilogo</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text="Intestatario:" Font-Names="Arial" Font-Size="8pt"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIntestatario" runat="server" Text="INTESTATARIO" Font-Names="Arial"
                                                        Font-Size="8pt" Font-Bold="False" ForeColor="Black" Font-Underline="False" Style="font-weight: 700;
                                                        font-size: 8pt;"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIn" runat="server" Text="C.F / P. IVA:" Font-Names="Arial" Font-Size="8pt"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCFIVA" runat="server" Text="CF" Font-Names="Arial" Font-Size="8pt"
                                                        Font-Bold="False" ForeColor="Black" Font-Underline="False" Style="font-weight: 700;
                                                        font-size: 8pt;"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="Stato Pratica:" Font-Names="Arial" Font-Size="8pt"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cmbSTRateizz" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Cod. Contratto:" Font-Names="Arial" Font-Size="8pt"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCodiceRu" runat="server" Text="COD_RU" Font-Names="Arial" Font-Size="10pt"
                                                        Font-Bold="False" ForeColor="Black" Style="font-weight: 700; font-size: 8pt;"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Fascia Reddituale:" Font-Names="Arial"
                                                        Font-Size="8pt" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFascia" runat="server" Font-Names="Arial" Font-Size="8pt" Font-Bold="False"
                                                        ForeColor="Black" Font-Italic="False" Font-Underline="False" Style="font-weight: 700;
                                                        font-size: 8pt;"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="Tasso Interesse:" Font-Names="Arial"
                                                        Font-Size="8pt" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTassoInt" runat="server" Font-Names="Arial" Font-Size="8pt" Font-Bold="False"
                                                        ForeColor="Black" Font-Italic="False" Font-Underline="False" Style="font-weight: 700;
                                                        font-size: 8pt;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="border-bottom-style: ridge; border-bottom-width: 2px; border-bottom-color: #FFFFFF"
                            class="style2">
                            Elenco delle bollette selezionate per la rateizzazione
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="TBL_RIEPILOGO" runat="server" Width="100%"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="100%">
                <table width="100%">
                    <tr>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            DEBITO
                        </td>
                        <td class="style4" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            €.
                        </td>
                        <td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #C0C0C0">
                            <asp:TextBox ID="totDebito" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                ReadOnly="True" Style="text-align: right"></asp:TextBox>
                        </td>
                        <td class="style5" width="200px">
                            di cui rateizzabile
                        </td>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            €.
                            <asp:TextBox ID="txtImpRateizzabile" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="80px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            REDDITO LORDO MENSILE
                        </td>
                        <td class="style4" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            €.
                        </td>
                        <td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #C0C0C0">
                            <asp:TextBox ID="txtReddMensile" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            CAPITALE DA RATEIZZARE
                        </td>
                        <td class="style4" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            €.
                        </td>
                        <td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #C0C0C0">
                            <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                ReadOnly="True" Style="text-align: right"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td class="style3">
                            <strong>NOTE</strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            <table style="vertical-align: top;">
                                <tr>
                                    <td style="vertical-align: top; width: 150px; font-family: Arial; font-size: 8pt;
                                        font-weight: bold;">
                                        ANTICIPO
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPerc" runat="server" Text="%" Font-Names="Arial" Font-Size="8pt"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbPercentuale" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="50px" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="style4">
                            €.
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersAnticipo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" Style="text-align: right">0,00</asp:TextBox>
                        </td>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            DATA SCAD. BOLL. ANTICIPO
                        </td>
                        <td>
                            <asp:TextBox ID="txtscadBollettino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="65px" MaxLength="10" Style="text-align: left"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtscadBollettino"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style=""
                                ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                SetFocusOnError="True" ValidationGroup="Date"></asp:RegularExpressionValidator>
                        </td>
                        <td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #C0C0C0;
                            vertical-align: top;" rowspan="4">
                            <asp:TextBox ID="txtNote" runat="server" Height="70px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            TOT. RESIDUO
                        </td>
                        <td class="style4" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            €.
                        </td>
                        <td>
                            <asp:TextBox ID="txtCapitale" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                Style="text-align: right; margin-bottom: 0px;" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" style="border-bottom-style: dotted; border-bottom-width: 1px;
                            border-bottom-color: #C0C0C0">
                            DATA EMISSIONE
                        </td>
                        <td style="border-bottom-style: dotted; border-bottom-width: 1px; border-bottom-color: #C0C0C0">
                            &nbsp;
                        </td>
                        <td style="border-bottom: 1px dotted #C0C0C0;">
                            <asp:TextBox ID="txtData" runat="server" Font-Names="Arial" Font-Size="8pt" Width="65px"
                                MaxLength="10" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtData"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style=""
                                ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                SetFocusOnError="True" ValidationGroup="Date"></asp:RegularExpressionValidator>
                        </td>
                        <td style="border-bottom: 1px dotted #C0C0C0; text-align: left;">
                            &nbsp;
                        </td>
                        <td style="border-bottom: 1px dotted #C0C0C0; text-align: right;">
                            &nbsp;
                        </td>
                        <td style="border-bottom: 1px dotted #C0C0C0; text-align: right;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="80%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="45%">
                                <tr>
                                    <td style="border-right-style: solid; border-right-width: 1px;" width="35%">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblN" runat="server" Text="N° Rate" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtNRate" runat="server" Font-Names="Arial" Font-Size="8pt" Width="40px"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNRate"
                                                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style=""
                                                        ToolTip="E' possibile inserire solo un numero intero" ValidationExpression="(\d)+"
                                                        SetFocusOnError="True" ValidationGroup="Date"></asp:RegularExpressionValidator>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtImpRate" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"
                                                        Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ImageButton ID="BtnSimula" runat="server" ImageUrl="~/NuoveImm/SimulazRate.png"
                                            Style="text-align: right" OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                            Height="20px" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="46%" style="text-align: right">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table width="100%">
                    <tr>
                        <td style="text-align: left">
                            <%--<asp:ImageButton ID="btnPrintAccDebito" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
                                Style="text-align: right" ToolTip="Stampa Modulo di Accertamento del Debito"
                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';PrintDoc();"
                                ValidationGroup="Date" />--%>

                                <asp:Menu ID="btnPrint" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                                    Orientation="Horizontal" RenderingMode="Table" ToolTip="Stampa Moduli">
                                    <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                                    <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                        ForeColor="#0066FF" Width="180px" />
                                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                        VerticalPadding="1px" />
                                    <Items>
                                        <asp:MenuItem ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" Selectable="False" Value="">
                                            <asp:MenuItem Text="Stampa riconoscimento" Value="1"></asp:MenuItem>
                                            <asp:MenuItem Text="Stampa proposta" Value="2"></asp:MenuItem>
                                        </asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right" width="80%">
                            <asp:ImageButton ID="btnSa" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                OnClientClick="ConfirmSave();Nascondi();document.getElementById('splash').style.visibility = 'visible';"
                                ValidationGroup="Date" />
                        </td>
                        <td style="text-align: right">
                            <img id="imgEsci" alt="" src="../NuoveImm/Img_Esci_AMM.png" onclick="self.close();"
                                style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="98%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <asp:DataGrid ID="DataGridRate" runat="server" BackColor="White" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" PageSize="24"
                        Style="z-index: 105; left: 193px; top: 54px" Width="97%" EnableViewState="False"
                        BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="2" CellSpacing="5"
                        AutoGenerateColumns="False" Visible="False">
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <ItemStyle ForeColor="Black" BackColor="#DEDFDE" />
                        <Columns>
                            <asp:BoundColumn DataField="NUMRATA" HeaderText="N° RATA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EMISSIONE" HeaderText="DATA EMISSIONE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SCADENZA" HeaderText="DATA SCADENZA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTORATA" HeaderText="RATA €.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="QUOTACAPITALI" HeaderText="QUOTA CAPITALE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="QUOTAINTERESSI" HeaderText="QUOTA INTERESSI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTORESIDUO" HeaderText="RESIDUO €.">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#E7E7FF" />
                        <SelectedItemStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>
                </span></strong>
            </td>
        </tr>
    </table>
    <script type="text/javascript">


        document.getElementById('splash').style.visibility = 'hidden';


        function Simulazione() {

            if (document.getElementById("txtData").value == '') {
                alert('Inserisci la data emissione!');
                return;
            }

            if (document.getElementById("txtNRate").value > 0 && document.getElementById("txtImpRate").value == '') {


                document.getElementById("NumRate").value = document.getElementById("txtNRate").value


                window.open('RateizDati.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&NRATE=' + document.getElementById("NumRate").value + '&EMISSIONE=' + document.getElementById("txtData").value + '&IDCONTRATTO=' + document.getElementById("idContratto").value + '&RS=1', 'RateizzDati', 'height=598,width=920,scrollbars=no');
            }

            if (document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') > 0) {
                document.getElementById("NumRate").value = document.getElementById("txtNRate").value
                MaxImporto();
                window.open('RateizDati.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&NRATE=' + document.getElementById("NumRate").value + '&IMPRATA=' + document.getElementById("txtImpRate").value + '&EMISSIONE=' + document.getElementById("txtData").value + '&IDCONTRATTO=' + document.getElementById("idContratto").value + '&RS=1', 'RateizzDati2', 'height=598,width=920,scrollbars=no');

            }


            if ((document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') == '' && document.getElementById("txtNRate").value == '') || (document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') == '0' && document.getElementById("txtNRate").value == '') || (document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.') == '' && document.getElementById("txtNRate").value == '0')) {
                alert('Inserisci il numero delle rate o l\'importo della singola rata!');
            }





        }
        function MaxRata() {
            if (document.getElementById("txtNRate").value > 72) {
                if (window.confirm('Conferma di voler produrre un piano di rateizzazione superiore a 72 rate?') == true) {
                    //confermato il piano > 72 mensilità
                    document.getElementById("NumRate").value = document.getElementById("txtNRate").value
                } else {
                    document.getElementById("txtNRate").value = '72';
                }

            }
        }
        //        function MaxRata() {
        //            if (document.getElementById("txtNRate").value > 72) {
        //                document.getElementById("NumRate").value = '72';
        //                alert('Il numero massimo di rate consentite per un piano di rateizzazione è 72!');
        //                document.getElementById("txtNRate").value = '72';
        //            }
        //            else {
        //                document.getElementById("NumRate").value = document.getElementById("txtNRate").value
        //            }
        //        }

        function MaxImporto() {
            if (parseFloat(document.getElementById("txtImpRate").value.replace('.', '').replace(',', '.')) > parseFloat(document.getElementById("txtCapitale").value.replace('.', '').replace(',', '.'))) {
                alert('L\'importo della rata non può superare quello da rateizzare!');
                document.getElementById("txtImpRate").value = '';
            }

        }
        function EsitoPosRAT() {
            document.getElementById("NumRate").value = document.getElementById("txtNRate").value
            window.open('../VSA/StampeRateizzStraord.aspx?CAPITALE=' + document.getElementById("txtCapitale").value + '&NRATE=' + document.getElementById("NumRate").value + '&EMISSIONE=' + document.getElementById("txtData").value + '&IDCONTRATTO=' + document.getElementById("idContratto").value + '&RS=1&IDDICHIARAZ=' + document.getElementById("idDich").value + '&CODUNITA=' + document.getElementById('codUI').value + '&NUMCONT=' + document.getElementById('lblCodiceRu').innerText + '&TIPO=EsPositivo', '', '');
        }
    </script>
    <asp:HiddenField ID="CapitaleRateiz" runat="server" />
    <asp:HiddenField ID="ConfermaSalva" runat="server" Value="0" />
    <asp:HiddenField ID="idContratto" runat="server" Value="0" />
    <asp:HiddenField ID="fascia" runat="server" />
    <asp:HiddenField ID="MaxNumRate" runat="server" Value="" />
    <asp:HiddenField ID="NumRate" runat="server" />
    <asp:HiddenField ID="idRateizz" runat="server" Value="0" />
    <asp:HiddenField ID="impAnticipo" runat="server" Value="0" />
    <asp:HiddenField ID="reddMensile" runat="server" Value="0" />
    <asp:HiddenField ID="saldo" runat="server" Value="0" />
    <asp:HiddenField ID="dataSaldo" runat="server" />
    <asp:HiddenField ID="classe" runat="server" />
    <asp:HiddenField ID="isee" runat="server" Value="0" />
    <asp:HiddenField ID="idDich" runat="server" Value="0" />
    <asp:HiddenField ID="codTipoRU" runat="server" />
    <asp:HiddenField ID="codUI" runat="server" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript" language="javascript">
    document.getElementById("txtCapitale").value = document.getElementById("CapitaleRateiz").value
    //document.getElementById("txtNRate").value = document.getElementById("NumRate").value



</script>
</html>
