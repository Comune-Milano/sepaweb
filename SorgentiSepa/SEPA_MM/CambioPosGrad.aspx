<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioPosGrad.aspx.vb" Inherits="CambioIntestazione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            alert('Usare il tasto <Avvia Ricerca>');
            history.go(0);
            event.keyCode = 0;
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambio Posizione graduatoria</title>
    <style type="text/css">
        .stile_tabella {
            width: 100%;
            margin-top: 5%;
            margin-left: 15px;
        }

        .pulsante {
            margin-left: 75%;
            margin-top: 25%;
        }
    </style>
</head>
<body style="background-image: url(NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;">

    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="4">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        Cambio Posizione Graduatoria</strong></span>
                    </td>
                </tr>
            </table>
            <table class="stile_tabella">
                <tr>
                    <td width="750px" style="font-family: arial, Helvetica, sans-serif; font-size: 9pt; vertical-align: top;">Inserire il protocollo della domanda e cliccare sull'icona di ricerca.
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Num. protocollo domanda" Font-Size="9pt" Font-Names="Arial"
                                        Width="195px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPG" runat="server" Width="120px"></asp:TextBox>
                                    <asp:ImageButton ID="btnCercaPG" runat="server" ImageUrl="Condomini/Immagini/Search_16x16.png"
                                        ToolTip="Ricerca Domanda" CausesValidation="false" />
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblInfoDomanda" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="9pt" Width="630px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>

                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPosizGrad" runat="server" Text="Nuova posizione graduatoria" Font-Size="9pt" Font-Names="Arial"
                                        Width="195px" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPosizGrad" runat="server" Width="120px" BackColor="#D8F9FC" Visible="false" Font-Bold="True"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPosizGrad"
                                        Display="Static" ErrorMessage="Valore mancante!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp</td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblErr" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt"
                            ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="pulsante">
            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                ToolTip="Procedi" OnClientClick="ConfermaCambioPos();" Visible="False" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                ToolTip="Home" OnClientClick="self.close();" CausesValidation="false" />
        </div>
        <asp:HiddenField ID="confermaCambioPos" runat="server" />
        <asp:HiddenField ID="idDomanda" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    if (document.getElementById('divLoading') != null) {
        document.getElementById('divLoading').style.visibility = 'hidden';
    };

    function ConfermaCambioPos() {
        var sicuro = window.confirm('Attenzione...Sei sicuro di voler procedere?');
        if (sicuro == true) {
            document.getElementById('confermaCambioPos').value = '1';
        }
        else {
            document.getElementById('confermaCambioPos').value = '0';
        }
    };

</script>
</html>
