<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDichFuoriMI.aspx.vb"
    Inherits="ASS_RicercaDichFuoriMI" %>

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
    <title>Ricerca Dichiarazioni Fuori Milano</title>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;
    vertical-align: top;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr style="height: 35px; vertical-align: bottom">
            <td colspan="2">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;&nbsp;Ricerca
                    Dichiarazioni per Abbin. Fuori Milano</strong></span>
            </td>
        </tr>
        <tr style="height: 20px">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 20px;">
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Cognome
                        </td>
                        <td>
                            <asp:TextBox ID="txtCognome" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"
                                MaxLength="50" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td rowspan="9">
                            <img src="../ImmMaschere/alert2_ricercad.gif" alt="messaggio approssimazione" />
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Nome
                        </td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"
                                MaxLength="50" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Cod. Fiscale
                        </td>
                        <td>
                            <asp:TextBox ID="txtCF" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"
                                MaxLength="16" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Numero
                        </td>
                        <td>
                            <asp:TextBox ID="txtPG" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px"
                                MaxLength="50" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Stato
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbStato" TabIndex="5" runat="server" Height="20px" Width="210px"
                                Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111;
                                border-left: black 1px solid; border-bottom: black 1px solid;">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt">
                            Comune di Assegn.
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbComuneAss" TabIndex="5" runat="server" Height="20px" Width="210px"
                                Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111;
                                border-left: black 1px solid; border-bottom: black 1px solid;">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 254px">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 60%">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png"
                                OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                OnClientClick="caricamentoincorso();location.href='pagina_home.aspx';return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="abbin" runat="server" Value="0" />
    </form>
    <script type="text/javascript" language="javascript">
        function caricamentoincorso() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'visible';
                    };
                }
                else {
                    alert('ATTENZIONE! Ci sono delle incongruenze nei dati della pagina!');
                };
            }
            else {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'visible';
                };
            };
        };
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
</body>
</html>
