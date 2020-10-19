<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicPagamenti.aspx.vb" Inherits="Condomini_RicPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function $onkeydown() {

        if (event.keyCode == 13) {
            var bt = document.getElementById('btnCerca');
            bt.click();
            return false;
        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Pagamenti</title>
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
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d]/g,
            'onlynumbers': /[^\d\-\,\.]/g

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

    </script>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <form id="form1" runat="server" defaultfocus="txtDataDal">
    <div style="width: 789px">
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Ricerca Pagamenti Condominiali" Width="759px"></asp:Label>
        <table style="width: 100%;">
            <tr>
                <td style="font-size: 6pt">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>ANNO A.D.P.</strong>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <table>
                        <tr>
                            <td class="style1">
                                Da
                            </td>
                            <td>
                                <asp:TextBox ID="txtAnnoDa" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="4" Width="83px" TabIndex="1" ToolTip="yyyy" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td class="style1">
                                A
                            </td>
                            <td>
                                <asp:TextBox ID="txtAnnoA" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="4"
                                    Width="83px" TabIndex="2" ToolTip="yyyy" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>NUMERO A.D.P.</strong>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <table width="25%">
                        <tr>
                            <td class="style1">
                                Da
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumAdpDA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="10" Width="83px" TabIndex="3" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td class="style1">
                                A
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumAdpA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="10" Width="83px" TabIndex="4" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>IMPORTO A.D.P.</strong>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <table width="25%">
                        <tr>
                            <td class="style1">
                                Da
                            </td>
                            <td>
                                <asp:TextBox ID="txtImportoDA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="20" Width="83px" TabIndex="5" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td class="style1">
                                A
                            </td>
                            <td>
                                <asp:TextBox ID="txtImportoA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="20" Width="83px" TabIndex="6" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>NUMERO MANDATO GESTORE</strong>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <table width="25%">
                        <tr>
                            <td class="style1">
                                Da
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumMandatoDA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="20" Width="83px" TabIndex="7" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td class="style1">
                                A
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumMandatoA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="20" Width="83px" TabIndex="8" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>DATA MANDATO GESTORE</strong>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC;">
                    <table width="25%">
                        <tr>
                            <td class="style1">
                                Da
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataMandatoDA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="10" Width="83px" TabIndex="9" ToolTip="dd/MM/yyyy" 
                                    Style="text-align: center"></asp:TextBox>
                            </td>
                            <td class="style1">
                                A
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataMandatoA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="10" Width="83px" TabIndex="10" ToolTip="dd/MM/yyyy" 
                                    Style="text-align: center"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <strong>CONDOMINIO</strong>
                </td>
            </tr>
            <tr>
                <td style="border: 1px solid #CCCCCC; text-align: left;">
                    <div style="height: 210px; width: 100%; overflow: auto;">
                        <asp:CheckBoxList ID="chkCondomini" runat="server" Width="92%" CellPadding="1" CellSpacing="1"
                            DataTextField="DENOMINAZIONE" DataValueField="ID" Font-Names="Arial" 
                            Font-Size="8pt" TabIndex="11">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="Immagini/Img_Home.png"
                        Style="z-index: 106; left: 666px; top: 402px" TabIndex="12" 
                        ToolTip="Home" /> &nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
                        Style="z-index: 111; left: 534px; top: 405px" TabIndex="13" 
                        ToolTip="Avvia Ricerca" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
