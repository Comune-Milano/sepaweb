<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimentoAvviso.aspx.vb"
    Inherits="Contratti_InserimentoAvviso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Dettagli avviso liquidazione</title>
    <style type="text/css">
        .bottone
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-family: Arial;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            width: 50px;
            cursor: pointer;
        }
        .auto-style1
        {
            width: 150px;
        }
    </style>
</head>
<body>
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    <script type="text/javascript" language="javascript">
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }

        function DelPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;

        }

        function $onkeydown() {
            if (event.keyCode == 46) {
                event.keyCode = 0;
            }
        }

        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');

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

    </script>
    <div style="width: 650px; height: 450px; background-image: url('ImmDiv/SfondoDim4.png');
        z-index: 300;" align="center">
        <table style="z-index: 400;">
            <tr>
                <td style="width: 600px; height: 400px;">
                    <fieldset style="border: 1px solid #999999; padding-top: 20px;">
                        <legend style="font-family: Arial; font-size: 12pt; font-weight: bold; color: Black;">
                            Dettagli Avviso Liquidazione</legend>
                        <table style="width: 100%">
                            <tr>
                                <td style="vertical-align: top;" class="auto-style1">
                                    &nbsp;
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px; text-align: left;">Imposta</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:DropDownList ID="cmbImposta" runat="server" Font-Names="arial" Font-Size="10pt"
                                        Width="400px" TabIndex="1">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px; text-align: left;">Importo</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="10"
                                        TabIndex="2" Font-Bold="True" Width="90px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Sanzione</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtSanzione" runat="server" Font-Names="Arial" Font-Size="10pt"
                                        MaxLength="10" TabIndex="3" Font-Bold="True" Width="90px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Interessi</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtInteressi" runat="server" Font-Names="Arial" Font-Size="10pt"
                                        MaxLength="10" TabIndex="4" Font-Bold="True" Width="90px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Spese Notifica</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtSpese" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="10"
                                        TabIndex="5" Font-Bold="True" Width="90px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Data Protocollo</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtDataPG" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="10"
                                        TabIndex="6" Font-Bold="True" Width="90px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataPG"
                                        Display="Dynamic" ErrorMessage="Errore" Font-Bold="False" Font-Names="arial"
                                        Font-Size="10pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="10px"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Data Pagamento</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtDataPag" runat="server" Font-Names="Arial" Font-Size="10pt" MaxLength="10"
                                        TabIndex="7" Font-Bold="True" Width="90px"></asp:TextBox><asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataPag"
                                            Display="Dynamic" ErrorMessage="Errore" Font-Bold="False" Font-Names="arial"
                                            Font-Size="10pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Width="10px"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Note</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="10pt" Width="399px"
                                        MaxLength="4000" TabIndex="8" Height="76px" Font-Bold="True" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Ricevuta</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:FileUpload ID="FileUploadRic" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Style="z-index: 101; " Width="400px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Quietanza</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:FileUpload ID="FileUploadQui" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Style="z-index: 101; " Width="400px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left;" class="auto-style1">
                                    &nbsp;</td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:Label ID="lblRicevute" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Maroon"
                                        Visible="False">Attenzione, inserire nuovamente Ricevuta e Quietanza prima di salvare!</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    &nbsp
                                </td>
                                <td>
                                    &nbsp
                                    <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="Maroon"
                                        Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left" class="auto-style1">
                                    &nbsp;
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="btn_inserisci" runat="server" TabIndex="9" Text="Inserisci" CssClass="bottone"
                                        Width="80px" />
                                    <asp:Button ID="btn_chiudi" runat="server" OnClientClick="ConfermaEsci();" Text="Esci"
                                        CssClass="bottone" TabIndex="10" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        <asp:HiddenField ID="salvaNote" runat="server" Value="0" />
        <asp:HiddenField ID="controllaSalva" runat="server" Value="0" />
    </div>
    <script type="text/javascript">
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

        function Chiudi() {

            document.getElementById('txtModificato').value = '0';
            window.close();
        }

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }


        function ConfermaEsci() {
            if ((document.getElementById('txtModificato').value == '1') || (document.getElementById('txtModificato').value == '111')) {

                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.\nUscire senza salvare causerà la perdita delle modifiche!\nUscire ugualmente? Per non uscire premere ANNULLA.");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                }
                else {
                    if (document.getElementById('caric')) {
                        document.getElementById('caric').style.visibility = 'visible';

                    }
                    Chiudi();
                }
            }
            else {
                if (document.getElementById('caric')) {
                    document.getElementById('caric').style.visibility = 'visible';

                }
                Chiudi();
            }
        }
        function ConfermaProcedi() {

            var note = document.getElementById('txtNote').value;

            if (note == '') {
                alert('Dato mancante!');
                document.getElementById('controllaSalva').value = '0';
            } else {
                document.getElementById('controllaSalva').value = '1';
            }
        }
    </script>
    </form>
</body>
</html>
