<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimentoNote.aspx.vb"
    Inherits="InserimentoNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Dettagli Note</title>
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
    </style>
</head>
<body>
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    <div style="width: 420px; height: 250px; z-index: 300; background-color: #FFFFFF;" 
        align="center">
        <table style="z-index: 400;">
            <tr>
                <td style="width: 420px; height: 220px;">
                    <fieldset style="border: 1px solid #999999; padding-top: 10px;">
                        <legend style="font-family: Arial; font-size: 12pt; font-weight: bold; color: Black;">
                            Dettagli Note</legend>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <span style="font-size: 10pt; font-family: Arial; width: 800px;">Data Evento</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtDataEvento" runat="server" Font-Names="Arial" Font-Size="10pt"
                                        MaxLength="10" TabIndex="1" Font-Bold="True" Width="90px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataEvento"
                                        ErrorMessage="Errore" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="#CC0000" ToolTip="Errore data"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">
                                    <span style="font-size: 10pt; font-family: Arial">Descrizione</span>
                                </td>
                                <td style="vertical-align: top; text-align: left;">
                                    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="10pt" Width="320px"
                                        MaxLength="4000" TabIndex="2" Height="60px" Font-Bold="True" 
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    &nbsp;
                                </td>
                                <td style="text-align: right">
                                    <asp:Button ID="btn_inserisci" runat="server" TabIndex="2" Text="Inserisci" CssClass="bottone"
                                        OnClientClick="ConfermaProcedi();" Width="80px" />
                                    <asp:Button ID="btn_chiudi" runat="server" OnClientClick="ConfermaEsci();" Text="Esci" CssClass="bottone" />
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
