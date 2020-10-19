<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalcolaAventiDiritto.aspx.vb"
    Inherits="Contratti_ContCalore_CalcolaAventiDiritto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script language="javascript" type="text/javascript">
        function ConfRewritePreventivo() {
            if (window.confirm('Identificata una precedente elaborazione di preventivo non approvata.\nProcedere con la cancellazione della precedente e la scrittura di un nuovo preventivo?')) {
                document.getElementById("btnContinuaP").click();

            }
            else {
                alert('Nessuna operazione eseguita');
            }
        };

        function ConfRewriteConsuntivo() {

            if (window.confirm('Identificata precedente elaborazione di consuntivo non approvata.\nProcedere con la cancellazione della precedente e la scrittura di un nuovo consuntivo?')) {
                document.getElementById("btnContinuaP").click();

            }
            else {
                alert('Nessuna operazione eseguita');

            }

        };

    </script>
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 782px;
        }
        .style1
        {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
    <table style="width: 100%; position: absolute; top: 25; left: 10;" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="font-family: Arial; font-size: 8pt">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
                    ForeColor="#801F1C" Text="Label" BorderStyle="None" BorderWidth="0px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style1">
                            <strong>INDICARE L&#39;ANNO PER IL CALCOLO DEL CONTRIBUTO CALORE</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAnnoCalcolo" runat="server" MaxLength="4" Width="40px" Style="text-align: center"
                                Font-Names="Arial" Font-Size="10pt" Font-Bold="True"></asp:TextBox>
                            <asp:DropDownList ID="cmbAnniConsuntivabili" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Visible="False" Font-Bold="True" Style="text-align: center">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAvviaCalcolo" runat="server" Text="GENERA ASSEGNATARI AVENTI DIRITTO"
                                OnClientClick="ConfCreazione();" BackColor="#507CD1" Font-Bold="True" Font-Names="Arial"
                                Font-Size="10pt" ForeColor="White" />
                        </td>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 250px; text-align: right; vertical-align: bottom">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                OnClientClick="parent.main.location.replace('../pagina_home.aspx');return false;" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                <asp:HiddenField ID="idContributo" runat="server" Value="0" />
                <asp:HiddenField ID="ValoreMq" runat="server" Value="0" />
                <asp:HiddenField ID="AreaLimite" runat="server" Value="0" />
                <asp:HiddenField ID="cfArrotond" runat="server" Value="0" />
                <asp:HiddenField ID="ConfCrea" runat="server" Value="0" />
                <asp:HiddenField ID="TipoDimensione" runat="server" Value="0" />
                <asp:HiddenField ID="TipoCalcolo" runat="server" Value="0" />
                <asp:Button ID="btnContinuaP" runat="server" Text="btnContinuaP" Style="visibility: hidden" />
            </td>
        </tr>
    </table>
    </form>
</body>
<script language="javascript" type="text/javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d]/g,
        'onlynumbers': /[^\d\-\,\.]/g

    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');

    }
    function ConfCreazione() {
        if (document.getElementById('TipoCalcolo').value == 0) {
            if (document.getElementById('txtAnnoCalcolo').value.length > 0 && document.getElementById('txtAnnoCalcolo').value.length == 4) {
                if (window.confirm('Procedere con l\'inserimento del preventivo di calcolo del contributo calore?')) {
                    document.getElementById("ConfCrea").value = 1;
                }
                else {
                    document.getElementById("ConfCrea").value = 0;

                }

            }
            else {

                document.getElementById("ConfCrea").value = 0;
                alert('Definire correttamente l\'anno per il calcolo del contributo calore!!!');
                return false;
            }
        }
        else {

            if (window.confirm('Procedere con l\'inserimento del consuntivo di calcolo del contributo calore?')) {
                document.getElementById("ConfCrea").value = 1;
            }
            else {
                document.getElementById("ConfCrea").value = 0;

            }

        }
    };
    document.getElementById('dvvvPre').style.visibility = 'hidden';



</script>
</html>
