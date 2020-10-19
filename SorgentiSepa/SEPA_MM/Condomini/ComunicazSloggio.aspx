<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ComunicazSloggio.aspx.vb"
    Inherits="Condomini_ComunicazSloggio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>ESTRATTO CONTO PER SLOGGIO</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
            color: #990000;
        }
        .tableStyle
        {
            font-family: Arial;
            font-size: 8pt;
            text-align: left;
        }
    </style>
    <script language="javascript" type="text/javascript">
        window.name = "modal";

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
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        };


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
        }
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }

        function ConfermaMav() {
            if (parseFloat(document.getElementById('txtDebitoInquilino').value.replace(/\./g, '').replace(',', '.')) > 0) {
                if (parseFloat(document.getElementById('txtDebitoInquilino').value.replace(/\./g, '').replace(',', '.')) > parseFloat(document.getElementById('txtCreditoInquilino').value.replace(/\./g, '').replace(',', '.'))) {

                    if (window.confirm('Salvando le informazioni verrà definito un debito residuo a carico dell\'intestatario?Procedere?')) {
                        document.getElementById("creaMav").value = 1;
                    }
                    else {
                        document.getElementById("creaMav").value = 0;
                    }

                }

            }

        }
    </script>
</head>
<body>
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF; visibility: visible;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server" target="modal">
    <table style="width: 100%;" class="tableStyle">
        <tr>
            <td class="style1">
                <strong>COMUNICAZIONE ESTRATTO CONTO CONDOMINIALE PER SLOGGIO</strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblContratto" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
                    Font-Size="10pt" Width="90%" ForeColor="Black" 
                    Style="text-align: center; font-size: 8pt;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblIntestatario" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
                    Font-Size="10pt" Width="90%" ForeColor="Black" 
                    Style="text-align: center; font-size: 8pt;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                            <asp:CheckBox ID="chkNonSollecitare" runat="server" 
                    Text="NON SOLLECITABILE" Font-Bold="True" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="border: thin solid #588CC8">
                    <tr>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            DATA INVIO COMUNICAZIONE AMMINISTRATORE
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataInvioAmm" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" MaxLength="10" Style="text-align: right"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            DATA RICEZIONE COMUNICAZIONE AMMINISTRATORE
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataRicAmm" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" MaxLength="10" Style="text-align: right"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            IMPORTO ESTRATTO CONTO A DEBITO DELL&#39;INQUILINO
                        </td>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            €.
                        </td>
                        <td>
                            <asp:TextBox ID="txtDebitoInquilino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" MaxLength="15" Style="text-align: right">0,00</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            IMPORTO ESTRATTO CONTO A CREDITO DELL&#39;INQUILINO
                        </td>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            €.
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreditoInquilino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" MaxLength="15" Style="text-align: right">0,00</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; font-family: Arial; font-size: 8pt;">
                            DATA INVIO COMUNICAZIONE INQUILINO
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataInvInq" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" MaxLength="10" Style="text-align: right"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-family: Arial; font-size: 8pt;">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:ImageButton ID="btnSalva" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Salva.png"
                                    ToolTip="Visualizza dettaglio" 
                                OnClientClick="document.getElementById('splash').style.visibility = 'visible';ConfermaMav();" 
                                style="height: 12px" />
                            </strong>
                        </td>
                        <td>
                            <asp:Image ID="imgEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" Style="cursor: pointer;"
                                onclick="self.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="creaMav" runat="server" Value="0" />
    <asp:HiddenField ID="id" runat="server" Value="0" />
    <asp:HiddenField ID="idBolletta" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('splash').style.visibility = 'hidden';
    </script>
</body>
</html>
