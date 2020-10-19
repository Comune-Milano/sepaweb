<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SpostamGestionaleTot.aspx.vb"
    Inherits="Contratti_SpostamGestionaleTot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Operazione in corso...</title>
    <style type="text/css">
        .style2
        {
            text-align: center;
            font-size: 10pt;
            font-family: Verdana;
            color: #000000;
        }
        .style3
        {
            font-family: Cambria;
            font-size: 8pt;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function ConfermaRate(numRate) {
            var Conferma
            Conferma = window.confirm('Attenzione! E\' stato calcolato un numero di rate pari a ' + numRate + ', superiore al limite massimo parametrizzato. Si desidera comunque procedere con la ripartizione?');
            if (Conferma == true) {
                document.getElementById("Esegui").click();
            }
            else {

                document.getElementById("riga").value = document.getElementById("riga").value + 1;
                document.getElementById("conferma0").value = '0';
                self.close();
            }

        }
        function ClickBottone() { document.getElementById("btnConfermaRate").click(); }

        function aggiornaRU() {


            if ((opener.opener.opener != 'undefined') && (opener.opener.opener != null)) {
                opener.opener.document.getElementById('imgSalva').click();
                opener.close();
            }
            else {
                opener.document.getElementById('imgSalva').click();
            }
            self.close();
        };
       
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <table style="border: 2px dashed #3366FF; width: 10px; background-color: #FFFFCC;">
        <tr>
            <td class="style2">
                <span style="font-family: Arial">
                    <img alt="caricamento" src="../NuoveImm/Load_nuovo.gif" style="z-index: 100; left: 0px;
                        position: static; top: 0px" /></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" class="style3">
                CARICAMENTO IN CORSO....
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
                <asp:Button ID="btnConfermaRate" runat="server" Text="Button" Style="display: none;" />
                <asp:Button ID="Esegui" runat="server" Text="Button" Style="display: none;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="conferma0" runat="server" Value="1" />
    <asp:HiddenField ID="riga" runat="server" Value="0" />
    <asp:HiddenField ID="BolloPagParz" runat="server" Value="0" />
    </form>
</body>
</html>
