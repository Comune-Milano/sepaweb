﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettagliContr.aspx.vb" Inherits="Contratti_DettagliContr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contratto Collegato</title>
</head>
<body style="background-attachment: fixed; background-color: #E2E2E2; background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <div style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td>
                    <div style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #005B88;">
                        <asp:Label ID="LabelTit" Text="DETTAGLI CONTRATTO" runat="server" Font-Names="arial"
                            ForeColor="#993333" Font-Size="10pt" Font-Bold="True"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Width="100%"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="IdContratto" runat="server" Value="0" />
        <asp:HiddenField ID="IdDomAbus" runat="server" Value="0" />
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';


        //            if (document.getElementById('indiceMorosita').value != '') {
        //                //chiamare qui la pagina morosità

        //                //Pagina Morosità "completa":
        //                window.open('../MOROSITA/MorositaInquilino.aspx?X=1&ID_MOR=' + document.getElementById('indiceMorosita').value + '&CON=' + document.getElementById('indiceContratto').value, 'Anteprima', 'height=550,top=0,left=0,width=800');


        //                //Sostituita da quella con le uniche informazioni sulla morosità:
        //                //window.open('DettaglioStatoMorosita.aspx?ID_MOR=' + document.getElementById('indiceMorosita').value + '&CON=' + document.getElementById('indiceContratto').value, 'Morosita', 'height=400,top=0,left=0,width=700');

        //                //subito dopo si chiude questa finestra
        //                self.close();
        //            }

        //            if (document.getElementById('indiceMorosita').value == '' && document.getElementById('indiceFine').value == '') {
        //                alert('Nessun dato da visualizzare!');
        //                self.close();
        //            }

        //            function apriDettRateizz() {
        //                var win
        //                window.open('DettRateizz.aspx?IDB=' + document.getElementById('indiceFine').value, '', 'height=550,top=0,left=0,width=800');
        //                self.close;
        //            }


        function ApriContratto2(id, codice) {

            today = new Date();
            var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();

            popupWindow = window.open('Contratto.aspx?LT=1&ID=' + id + '&LETT=READ' + '&COD=' + codice, Titolo, 'height=780,width=1160');
            popupWindow.focus();


        }

    </script>
    <img id="Fine" alt="Chiudi finestra" src="../NuoveImm/Img_Esci.png" onclick="self.close();"
        style="position: absolute; top: 179px; left: 342px; cursor: pointer;" />
    </form>
</body>
</html>
