<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Abb_Automatico_p2.aspx.vb"
    Inherits="ANAUT_Abb_Automatico_p2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Abbinamento Automatico</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr style="font-size: 12px; font-family: arial, Helvetica, sans-serif; color: #990000;
                background-color: #800000">
                <td height="20px" style="text-align: center; color: #FFFFFF; font-family: ARIAL, Helvetica, sans-serif;
                    font-size: 14pt;">
                    ABBINAMENTO AUTOMATICO - SCELTA INQUILINI
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:ImageButton ID="btnSelezionaTutti" runat="server" ImageUrl="../NuoveImm/Img_SelezionaTuttiGrande.png"
                                    Style="z-index: 102;" ToolTip="Seleziona Tutti" TabIndex="2" OnClientClick="Seleziona_deseleziona();return false;" /><asp:ImageButton
                                        ID="btnDeselezionaTutti" runat="server" ImageUrl="../NuoveImm/Img_DeSelezionaTutti.png"
                                        Style="z-index: 102;" ToolTip="Deseleziona tutti" TabIndex="1" OnClientClick="Seleziona_deseleziona();return false;" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: right">
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="../NuoveImm/Img_Procedi_Abbinamento.png"
                                    Style="" TabIndex="1" ToolTip="Effettua la proposta ai candidati selezionati per gli alloggi selezionati"
                                    OnClientClick="ConfermaAbbinam();" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_EsciCorto.png"
                                    Style="" ToolTip="Esci" CausesValidation="False" OnClientClick="ConfermaEsci();"
                                    TabIndex="2" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTabella" runat="server" Font-Names="arial" Font-Size="8pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function ConfermaEsci() {


            //             var chiediConferma
            //             chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            //             if (chiediConferma == true) {
            self.close();
            //             }

        }
        function Seleziona_deseleziona() {
            var i = 0;
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        modulo[i].checked = false;
                    } else {
                        modulo[i].checked = true;
                    }
                }
            }
        }

       function ConfermaAbbinam() {

            var i = 0;
            var spunta = false;
            var chiediConferma;
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        spunta = true;
                    }
                }
            }
            if (spunta == false) {
                alert('Nessuna proposta di abbinamento selezionata!');
            }
            else {
                chiediConferma = window.confirm("Sei sicuro di voler convalidare gli abbinamenti selezionati?");
                if (chiediConferma == true) {
                    document.getElementById('confermaAbb').value = "1";
                } else {
                    document.getElementById('confermaAbb').value = "0";
                }
            }
        }
    </script>
    <asp:HiddenField ID="idAlloggio" runat="server" Value="0" />
    <asp:HiddenField ID="confermaAbb" runat="server" Value="0" />
    <asp:HiddenField ID="nomeFileOK" runat="server" Value="0" />
    <asp:HiddenField ID="nomeFileScart" runat="server" Value="0" />
    </form>
</body>
</html>
