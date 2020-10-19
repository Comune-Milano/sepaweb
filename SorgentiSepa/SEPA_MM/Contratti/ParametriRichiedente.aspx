<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriRichiedente.aspx.vb"
    Inherits="Contratti_ParametriRichiedente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parametri Richidente</title>
    <style type="text/css">
        .CssMaiuscolo
        {
            text-transform: uppercase;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" method="post" runat="server">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametri
                    Richiedente Registrazione Contratto</strong></span><br />
                <table width="800px">
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 15px" colspan="2">
                            <fieldset style="width: 96%">
                                <legend style="font-family: Arial; font-size: 9pt; font-weight: bold; color: Black;">
                                    Richiedente</legend>
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left: 10px; width: 150px;">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Tipologia richiedente</asp:Label>
                                        </td>
                                        <td style="width: 80px;">
                                            <asp:DropDownList ID="cmbTipoRich" runat="server" Width="304px">
                                                <asp:ListItem Value="1">PARTE DEL CONTRATTO</asp:ListItem>
                                                <asp:ListItem Value="2">MEDIATORE DEL CONTRATTO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;">
                                            <div id="SceltaTipo" style="display: block; font-family: Arial; font-size: 8pt;">
                                                <asp:Label ID="lblTipoRich" runat="server" Text=""></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Label ID="lblDenominaz" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt">Denominazione</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" TabIndex="1" Width="300px" CssClass="CssMaiuscolo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px">
                                            <asp:Label ID="lblCodFisc" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Codice Fiscale</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodFiscale" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" TabIndex="2" Width="300px" 
                                                CssClass="CssMaiuscolo"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 15px; width: 100%;" colspan="2">
                            <fieldset id="rapprLeg" style="width: 96%">
                                <legend style="font-family: Arial; font-size: 9pt; font-weight: bold; color: Black;">
                                    Rappresentante legale</legend>
                                <table width="100%">
                                    <tr>
                                        <td style="padding-left: 10px; width: 150px;">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Cognome</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCognRapp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" TabIndex="3" Width="300px" CssClass="CssMaiuscolo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Nome</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNomeRapp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" TabIndex="4" Width="300px" 
                                                CssClass="CssMaiuscolo"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 10px;">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Codice Fiscale</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCFrapp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="10pt" TabIndex="5" Width="300px" 
                                                CssClass="CssMaiuscolo"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                        <td align="right" style="padding-right: 15px;">
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                                TabIndex="4" ToolTip="Salva" OnClientClick="controllaCampiVuoti();" />
                            <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" TabIndex="5"
                                ToolTip="Chiudi" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="errore" runat="server" Value="0" />
    <asp:HiddenField ID="HF_rdbTipoRich" runat="server" Value="" />
    <asp:HiddenField ID="nomeLocatore" runat="server" Value="" />
    <asp:HiddenField ID="codFiscLocatore" runat="server" Value="" />
    <asp:HiddenField ID="nomeMediatore" runat="server" Value="" />
    <asp:HiddenField ID="codFiscMediatore" runat="server" Value="" />
    <asp:HiddenField ID="nomeRapprLegale" runat="server" Value="" />
    <asp:HiddenField ID="cognomeRapprLegale" runat="server" Value="" />
    <asp:HiddenField ID="codFiscaleRapprLegale" runat="server" Value="" />
    </form>
    <script type="text/javascript" language="javascript">

        function controllaCampiVuoti() {
            if (document.getElementById('rdbTipoRichiedente2').checked == false) {
                if (document.getElementById('txtCognome').value == '' || document.getElementById('txtCodFiscale').value == '') {
                    alert('Campi obbligatori!');
                    document.getElementById('errore').value = '1';
                }
                else {
                    document.getElementById('errore').value = '0';
                }
                if (document.getElementById('cmbTipoRich').value == '1' && document.getElementById('rdbTipoRichiedente2').checked == false && document.getElementById('rdbTipoRichiedente1').checked == false) {
                    alert('Campi obbligatori!');
                    document.getElementById('errore').value = '1';
                }
                else {
                    document.getElementById('errore').value = '0';
                }
            }
        };

        function disabilitaTextBox() {
            if (document.getElementById('cmbTipoRich').value == '1' && document.getElementById('rdbTipoRichiedente2').checked == true) {
                document.getElementById('txtCognome').style.display = 'none';
                document.getElementById('lblDenominaz').style.display = 'none';

                document.getElementById('txtCodFiscale').style.display = 'none';
                document.getElementById('lblCodFisc').style.display = 'none';

                document.getElementById('rapprLeg').style.display = 'none';

            } else {
                document.getElementById('txtCognome').style.display = 'block';
                document.getElementById('lblDenominaz').style.display = 'block';

                document.getElementById('txtCodFiscale').style.display = 'block';
                document.getElementById('lblCodFisc').style.display = 'block';

                document.getElementById('rapprLeg').style.display = 'block';

            }
            if (document.getElementById('cmbTipoRich').value == '1' && document.getElementById('rdbTipoRichiedente2').checked == false && document.getElementById('rdbTipoRichiedente1').checked == false) {
                document.getElementById('SceltaTipo').style.display = 'block';
                document.getElementById('txtCognome').value = '';
                document.getElementById('txtCodFiscale').value = '';
                document.getElementById('txtCognRapp').value = '';
                document.getElementById('txtNomeRapp').value = '';
                document.getElementById('txtCFrapp').value = '';

            } 
            if (document.getElementById('cmbTipoRich').value == '2') {
                document.getElementById('SceltaTipo').style.display = 'none';
            }

            if (document.getElementById('cmbTipoRich').value == '2' && (document.getElementById('rdbTipoRichiedente2').checked == true || document.getElementById('rdbTipoRichiedente1').checked == true)) {
                document.getElementById('txtCognome').value = '';
                document.getElementById('txtCodFiscale').value = '';
                document.getElementById('txtCognRapp').value = '';
                document.getElementById('txtNomeRapp').value = '';
                document.getElementById('txtCFrapp').value = '';
            } 

            if (document.getElementById('rdbTipoRichiedente2').checked == true) {
                document.getElementById('HF_rdbTipoRich').value = 'CONDUTTORE';

            }
            if (document.getElementById('cmbTipoRich').value == '2') {
                document.getElementById('HF_rdbTipoRich').value = 'MEDIATORE';
            }

            if (document.getElementById('cmbTipoRich').value == '1' && document.getElementById('rdbTipoRichiedente1').checked == true) {
                document.getElementById('SceltaTipo').style.display = 'block';
                document.getElementById('HF_rdbTipoRich').value = 'LOCATORE';
                if (document.getElementById('nomeLocatore').value != '') {
                    document.getElementById('txtCognome').value = document.getElementById('nomeLocatore').value;
                }
                if (document.getElementById('codFiscLocatore').value != '') {
                    document.getElementById('txtCodFiscale').value = document.getElementById('codFiscLocatore').value;
                }
                if (document.getElementById('cognomeRapprLegale').value != '') {
                    document.getElementById('txtCognRapp').value = document.getElementById('cognomeRapprLegale').value;
                }
                if (document.getElementById('nomeRapprLegale').value != '') {
                    document.getElementById('txtNomeRapp').value = document.getElementById('nomeRapprLegale').value;
                }
                if (document.getElementById('codFiscaleRapprLegale').value != '') {
                    document.getElementById('txtCFrapp').value = document.getElementById('codFiscaleRapprLegale').value;
                }
            }
            if (document.getElementById('cmbTipoRich').value == '2' && document.getElementById('rdbTipoRichiedente2').checked == false && document.getElementById('rdbTipoRichiedente1').checked == false) {
                document.getElementById('HF_rdbTipoRich').value = 'MEDIATORE';
                if (document.getElementById('nomeMediatore').value != '') {
                    document.getElementById('txtCognome').value = document.getElementById('nomeMediatore').value;
                    
                }
                if (document.getElementById('codFiscMediatore').value != '') {
                    document.getElementById('txtCodFiscale').value = document.getElementById('codFiscMediatore').value;
                }
                if (document.getElementById('cognomeRapprLegale').value != '') {
                    document.getElementById('txtCognRapp').value = document.getElementById('cognomeRapprLegale').value;
                }
                if (document.getElementById('nomeRapprLegale').value != '') {
                    document.getElementById('txtNomeRapp').value = document.getElementById('nomeRapprLegale').value;
                }
                if (document.getElementById('codFiscaleRapprLegale').value != '') {
                    document.getElementById('txtCFrapp').value = document.getElementById('codFiscaleRapprLegale').value;
                }
            }
        };
        disabilitaTextBox();
    </script>
</body>
</html>
