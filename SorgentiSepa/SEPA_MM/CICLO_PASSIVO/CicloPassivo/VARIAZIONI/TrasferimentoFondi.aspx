<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TrasferimentoFondi.aspx.vb"
    Inherits="TrasferimentoFondi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Variazione Fondi</title>
    <script type="text/javascript">
        function ApriEventi() {
            window.open('EventiFondi.aspx', 'Eventi', '');
        }

        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

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
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }

        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                ForeColor="Maroon" Width="100%">Trasferimento fondi tra strutture</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:Image ID="Image1" runat="server" ToolTip="Visualizza eventi" Style="cursor: pointer;"
                                ImageUrl="../../../NuoveImm/Img_Eventi.png" onclick="ApriEventi();" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label3" runat="server" Text="Esercizio Finanziario*" Font-Names="Arial"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlanno" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="100%" AutoPostBack="True" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label2" runat="server" Text="Voce*" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlPrelievo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="100%" AutoPostBack="True" Font-Overline="False" TabIndex="2">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label1" runat="server" Text="Struttura di prelievo*" Font-Names="Arial"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlStrutture" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Width="100%" AutoPostBack="True" TabIndex="3">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label4" runat="server" Text="Struttura di destinazione*" Font-Names="Arial"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlStruttureDestinazione" runat="server" Font-Names="Arial"
                                Font-Size="9pt" Width="100%" AutoPostBack="True" TabIndex="4">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblResiduo" runat="server" Text="Importo residuo" Font-Names="Arial"
                                Font-Size="9pt"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtImportoResiduo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                ReadOnly="True" Style="text-align: right" TabIndex="-1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="lblImportoDestinazione" runat="server" Text="Importo da destinare*"
                                Font-Names="Arial" Font-Size="9pt"></asp:Label>
                        </td>
                        <td valign="middle">
                            <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="9pt" TabIndex="5"></asp:TextBox>
                            <asp:Label ID="lblErroreImporto" runat="server" Font-Names="Arial" Font-Size="9pt"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td valign="middle">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErrore" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Red"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 200px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 85%; height: 30px; text-align: right;">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../../NuoveImm/Img_Procedi.png"
                                ToolTip="Procedi" TabIndex="6" />
                        </td>
                        <td style="width: 15%">
                            <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../../../NuoveImm/Img_HomeModelli.png"
                                ToolTip="Torna alla Home" TabIndex="7" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="ErroreGen" runat="server" Value="0" />
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
<script language="javascript" type="text/javascript">
    if (document.getElementById('dvvvPre') != null) {
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    }
    if (document.getElementById('divLoading5') != null) {
        document.getElementById('divLoading5').style.visibility = 'hidden';
    }
</script>
</html>
