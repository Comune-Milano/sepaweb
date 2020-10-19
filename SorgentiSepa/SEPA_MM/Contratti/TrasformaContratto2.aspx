<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TrasformaContratto2.aspx.vb"
    Inherits="Contratti_TrasformaContratto2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trasforma Contratto</title>
    <style type="text/css">
        .font_caption {
            font-size: 13pt;
            font-weight: bold;
            color: #721C1F;
            text-align: center;
        }

        .colonna_domanda {
            width: 650px;
        }

        .colonna_cbx1 {
            width: 50px;
        }

        .colonna_cbx2 {
            width: 30px;
        }

        .stile_tabella {
            width: 100%;
            margin-left: 10px;
            font-family: Arial;
            font-size: 10pt;
        }

        .pulsante {
            margin-left: 50%;
            margin-top: 15%;
        }

        .bottone {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-family: Arial;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/jscript">
        window.name = "modal";

        function PrintDoc() {
            window.open('PrintLetter392.aspx?IDBOLL=<%=vIdBolletta %>', 'letDebt', 'height=598,width=920,scrollbars=no');
            document.getElementById('stampaModello').value = '1';

        };

        function ConfermaNewContratto() {
            var chiediConferma;
            var selezionato = false;
            var msg1 = "Attenzione, per questo Rapporto di Utenza si procederà al ricalcolo dei canoni pregressi. Continuare?"
            var msg2 = "Attenzione, per questo Rapporto di Utenza si procederà al ricalcolo dei canoni pregressi. Continuare?"

            if (document.getElementById('importoDebito').value != '0') {
                chiediConferma = window.confirm(msg1);
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            } else {


                chiediConferma = window.confirm(msg2);
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
        }

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        };



        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g,
            'numbers': /[^\d]/g
        };
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        };


        function AutoDecimal(obj, numdec) {
            if (numdec == null) numdec = 2;
            obj.value = obj.value.replace(/\./g, '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(numdec);
                if (a != 'NaN') {
                    if (numdec > 0) {
                        if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                            var decimali = a.substring(a.length, a.length - numdec);
                            var dascrivere = a.substring(a.length - (numdec + 1), 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                            };
                            risultato = dascrivere + risultato + ',' + decimali;
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace(/\./g, ',');
                        };
                    }
                    else {
                        if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                            var dascrivere = a.substring(a.length, 0);
                            var risultato = '';
                            while (dascrivere.replace('-', '').length >= 4) {
                                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
                                dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
                            };
                            risultato = dascrivere + risultato;
                            document.getElementById(obj.id).value = risultato;
                        }
                        else {
                            document.getElementById(obj.id).value = a.replace(/\./g, ',');
                        };

                    };
                }
                else {
                    document.getElementById(obj.id).value = '';
                };
            };
        };
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                };
                obj.value += ',';
                obj.value = obj.value.replace(/\./g, '');
            };
        };
        var oldValue;



    </script>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server" target="modal">
        <div style="width: 520px;">
            <table width="100%" class="stile_tabella">
                <tr>
                    <td class="font_caption" colspan="2">TRASFORMA CONTRATTO
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitoloCanone" runat="server" Text="Indennità annua di occupazione"
                            Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCanone" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblConfermaRifiuto" runat="server" Text="Conferma Rifiuto Accettazione Debito"
                            Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbConfermaRifiuto" runat="server" Width="100px" Visible="false"
                            AutoPostBack="True">
                            <asp:ListItem Value="1" Selected="True">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitoloSaldo" runat="server" Text="Importo credito/debito (al netto del DPC)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSaldo" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblDebitoAccTitolo" runat="server" Text="Debito Accettato" Style="visibility: hidden;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cmbDebitoAccettato" runat="server" Width="100px" AutoPostBack="True"
                            Style="visibility: hidden;">
                            <asp:ListItem Value="-1">- - -</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDataSl" runat="server" Text="Data Sloggio" Style="visibility: hidden;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDataSloggio" runat="server" MaxLength="10" Width="100px" Style="visibility: hidden;"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataSloggio"
                            Display="Static" ErrorMessage="??" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblImportAnnuo" runat="server" Text="Importo canone annuo" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtImportoAnnuo" runat="server" MaxLength="10" Width="100px" Style="visibility: hidden;"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </div>
        <div class="pulsante">
            <table>
                <tr>
                    <td>
                        <asp:Menu ID="btnStampaDoc" runat="server" CssClass="bottone" Orientation="Horizontal"
                            RenderingMode="Table" ToolTip="Estratto Conto" ForeColor="Black">
                            <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                            <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                                ForeColor="#0066FF" Width="250px" />
                            <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                                VerticalPadding="1px" />
                            <Items>
                                <asp:MenuItem Selectable="False" Value="" Text="Doc. allegata">
                                    <asp:MenuItem Text="Riconoscimento debito" Value="1"></asp:MenuItem>
                                    <asp:MenuItem Text="Risoluzione consensuale" Value="2"></asp:MenuItem>
                                </asp:MenuItem>
                            </Items>
                        </asp:Menu>
                    </td>
                    <td>
                        <asp:Button ID="btnNewContr" runat="server" CssClass="bottone" CausesValidation="False"
                            ToolTip="Procedi" Text="Procedi" Width="70px" Style="visibility: hidden;" OnClientClick="ConfermaNuovoRU();" />
                        <asp:Button ID="btnTrasformainST" runat="server" CssClass="bottone" CausesValidation="False"
                            ToolTip="Procedi" Text="Procedi" Width="70px" Visible="False" OnClientClick="ConfermaTrasfST();" />
                        <asp:Button ID="btnL43198" runat="server" CssClass="bottone" CausesValidation="False"
                            ToolTip="Procedi" Text="Stipula 431" Width="90px" Visible="False" OnClientClick="document.getElementById('btnL43198').style.visibility= 'hidden';" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="hiddenpanel" runat="server">
            <asp:HiddenField ID="idDich" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="importoDebito" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="accettazioneDeb" runat="server" Value="" ClientIDMode="Static" />
            <asp:HiddenField ID="confermaRifiuto" runat="server" Value="" ClientIDMode="Static" />
            <asp:HiddenField ID="tipo" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="dataFine" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="idContratto" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="dataDisdetta" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="codContr" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="conferma" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="stampaModello" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="idnuovoContratto" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="contratto431" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="indennita" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="idAreaEconomica" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="dataRicons" runat="server" Value="0" ClientIDMode="Static" />
        </asp:Panel>
    </form>
</body>
<script language="javascript" type="text/javascript">



        if (document.getElementById('stampaModello').value == '1') {
            if (document.getElementById('lblDebitoAccTitolo')) {
                document.getElementById('lblDebitoAccTitolo').style.visibility = 'visible';

            }
            if (document.getElementById('cmbDebitoAccettato')) {
                document.getElementById('cmbDebitoAccettato').style.visibility = 'visible';

            }
            if (document.getElementById('btnStampaAccett')) {
                document.getElementById('btnStampaAccett').style.visibility= 'hidden';

            }

        }
        function ApriContratto() {

            today = new Date();
            var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();

            popupWindow = window.open('Contratto.aspx?ID=' + document.getElementById('idnuovoContratto').value + '&COD=' + document.getElementById('codContr').value, Titolo, 'height=780,width=1160');
            popupWindow.focus();


        }

        function ConfermaNuovoRU() {

            if (document.getElementById('txtDataSloggio').value == '' && document.getElementById('accettazioneDeb').value != '0') {
                alert('Data sloggio mancante!');
            }
            else {

                if (document.getElementById('accettazioneDeb').value == '1') {
                    var chiediConferma;
                    var msg1 = "Attenzione, si procederà alla stipula di un nuovo contratto di locazione ERP. Continuare?";
                    var msg2 = "Attenzione, si procederà alla stipula di un nuovo contratto di locazione 431. Continuare?";
                    if (document.getElementById('accettazioneDeb').value == '1') {
                        if (document.getElementById('indennita').value != '0') {
                            chiediConferma = window.confirm(msg2);
                            if (chiediConferma == true) {
                                document.getElementById('conferma').value = '1';
                                document.getElementById('btnNewContr').style.visibility= 'hidden';
                            }
                            else {
                                document.getElementById('conferma').value = '0';
                            }
                        }
                        else {
                            if (document.getElementById('idAreaEconomica').value == '4') {
                                chiediConferma = window.confirm(msg2);

                            } else {
                                chiediConferma = window.confirm(msg1);

                            }
                            if (chiediConferma == true) {
                                document.getElementById('conferma').value = '1';
                                document.getElementById('btnNewContr').style.visibility= 'hidden';
                            }
                            else {
                                document.getElementById('conferma').value = '0';
                            }
                        }
                    }
                }
            }
        }

        function ConfermaTrasfST() {
            document.getElementById('conferma').value = '0';
            if (document.getElementById('confermaRifiuto').value == '1') {

                var chiediConferma;
                var msg1 = "Attenzione, si procederà alla trasformazione del contratto in SENZA TITOLO con applicazione dell'indennità. Continuare?";
                chiediConferma = window.confirm(msg1);
                if (chiediConferma == true) {
                    if (document.getElementById('btnTrasformainST')) {
                        document.getElementById('btnTrasformainST').style.visibility= 'hidden';
                    }
                    document.getElementById('conferma').value = '1';
                    if (document.getElementById('btnNewContr')) {
                        document.getElementById('btnNewContr').style.visibility= 'hidden';
                    }
                }
                else {
                    document.getElementById('conferma').value = '0';
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
</html>
