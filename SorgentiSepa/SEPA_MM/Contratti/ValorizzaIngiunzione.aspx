<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ValorizzaIngiunzione.aspx.vb" Inherits="Contratti_ValorizzaIngiunzione" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Bolletta ingiunta</title>
    <style type="text/css">
        .dialA {
            width: 100%;
            height: 100%;
            top: 0px;
            right: 0px;
            position: fixed;
            background-color: #C0C0C0;
            z-index: 0;
            filter: alpha(opacity='90');
            opacity: 0.90;
        }

        .dialB {
            width: 100%;
            height: 100%;
            top: 0px;
            right: 0px;
            position: fixed;
            background-color: transparent;
            z-index: 1;
        }

        .dialC {
            width: 400px;
            height: 150px;
            top: 85%;
            left: 60%;
            position: fixed;
            background-color: #ffffff;
            z-index: 2;
            margin-left: -300px;
            margin-top: -300px;
        }

        .stileLabel {
            text-align: right;
        }
    </style>
</head>
<body style="background-color: #F2F2F2">
    <form id="form1" runat="server">
        <div id="Ingiunzione" style="margin: 0px; position: absolute; top: 0px; left: 0px; width: 530px; height: 205px; background-color: #DBDBDB; visibility: visible;">
            <div id="Ingiunzione2" style="margin-left: 10px; margin-top: 25px; background-image: url('../ImmDiv/SfondoDim2.jpg'); width: 500px;"
                align="center">
                <table style="text-align: left; width: 100%" align="center">
                    <tr>
                        <td style="height: 19px; padding-left: 15px; padding-right: 15px; padding-top: 10px;">
                            <strong><span style="font-family: Arial">
                                <asp:Label ID="Label7" runat="server" Text="BOLLETTA INGIUNTA" Width="440px"></asp:Label></span></strong>
                            <br />
                            <hr style="color: #006699" />
                            <table style="width: 100%; font-family: Arial; font-size: 10pt;" cellpadding="3">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="font-size: 10pt; font-family: Arial;">Specificare il tipologia di ingiunzione:</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmbTipoIngiunzione" runat="server" Width="250px" Font-Names="Arial"
                                            Font-Size="10pt">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td>


                                        <asp:Label ID="lblSpecifico" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="10pt">Sblocco Ingiunzione</asp:Label>


                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="cmbSbloccoIng" runat="server" Width="50px" Font-Names="Arial"
                                            Font-Size="10pt">
                                            <asp:ListItem Value="1">SI</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr style="text-align: right; font-size: 12pt; font-family: Times New Roman">
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ImgSalvaBoll2" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                            OnClientClick="controllaSalvaIngiunz();" ToolTip="Salva" Style="cursor: pointer"
                                            TabIndex="6" />
                                        <img id="ImgAnnullaBoll2" alt="Esci senza modificare" src="../NuoveImm/Img_AnnullaVal.png"
                                            onclick="CancelEdit();" style="cursor: pointer;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="idConnessione" runat="server" />
        <asp:HiddenField ID="idBolletta" runat="server" />
        <asp:HiddenField ID="idcontratto" runat="server" />
        <asp:HiddenField ID="importo" runat="server" />
        <asp:HiddenField ID="confermaIngiunzione" runat="server" />
        <asp:HiddenField ID="idBollSelezionata" runat="server" />
        <asp:HiddenField ID="numBolletta" runat="server" />
        <script type="text/javascript">

            function controllaSalvaIngiunz() {

                var motivoStorno;
                var chiediConferma;
                var errore = 0;

                motivoStorno = document.getElementById('cmbTipoIngiunzione').value;

                if (motivoStorno == '-1') {
                    alert('Inserire la tipologia di ingiunzione!');
                    errore = 1;

                    return false;
                };
                if (errore == 0) {
                    chiediConferma = window.confirm("Attenzione... Sicuro di voler procedere?");
                    if (chiediConferma == false) {
                        document.getElementById('confermaIngiunzione').value = '0';
                    } else {
                        document.getElementById('confermaIngiunzione').value = '1';

                    }
                };
                return false;
            };




            function CloseAndRefresh(args) {
                GetRadWindow().BrowserWindow.refreshPage(args);
                GetRadWindow().close();
            };
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            };
            function CancelEdit() {
                GetRadWindow().close();
            };
        </script>
    </form>
</body>
</html>
