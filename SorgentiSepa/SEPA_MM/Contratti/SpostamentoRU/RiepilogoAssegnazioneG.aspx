<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RiepilogoAssegnazioneG.aspx.vb"
    Inherits="Contratti_SpostamentoRU_RiepilogoAssegnazioneG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Informazioni di riepilogo</title>
    <script type="text/javascript" language="javascript">

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
        function confronta_data(data1, data2) {	// controllo validità formato data    

            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
            //controllo se la seconda data è successiva alla prima
            if (data2str - data1str < 0) {
                alert("La data di decorrenza deve essere successiva alla data di decorrenza attuale!");
                document.getElementById('txtDataNuova').value = '';
            } else {
                //alert("ok");
            }
        }
        function confronta_data2(data1, data2) {	// controllo validità formato data    

            data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
            data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
            //controllo se la seconda data è successiva alla prima
            if (data2str - data1str < 0) {
                alert("La data di consegna deve essere successiva o uguale alla data di decorrenza!");
                document.getElementById('txtDataConsegna').value = '';
            } else {
                //alert("ok");
            }
        }
    </script>
    <style type="text/css">
        .stile_tabella
        {
            width: 770px;
            margin-top: 5%;
            margin-left: 15px;
        }
        .stile_tabella2
        {
            width: 770px;
            margin-left: 15px;
        }
        .pulsante
        {
            margin-left: 65%;
            margin-top: 35%;
        }
        .stileEtichette
        {
            font-family: Arial;
            font-size: 9pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="riepilogo" style="width: 800px; left: 0px; background-repeat: no-repeat;
        background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg'); z-index: 500;
        position: absolute; top: 0px; height: 550px;">
        <br />
        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp Informazioni
            Intestatario Contratto </strong>
            <asp:Label ID="lblCodContr" runat="server" Font-Size="14pt" Font-Bold="True"></asp:Label></span><br />
        <table class="stile_tabella2">
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 10pt; font-weight: bold;">
                    DATI PERSONA GIURIDICA
                </td>
            </tr>
        </table>
        <table class="stile_tabella2" cellpadding="4" cellspacing="1" style="border: 1px solid #339966;">
            <tr>
                <td class="stileEtichette">
                    Ragione Sociale
                </td>
                <td>
                    <asp:Label ID="lblRagSociale" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    P.IVA
                </td>
                <td>
                    <asp:Label ID="lblIVA" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Comune
                </td>
                <td>
                    <asp:Label ID="lblComGiur" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Prov.
                </td>
                <td>
                    <asp:Label ID="lblProvGiur" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Indirizzo
                </td>
                <td>
                    <asp:Label ID="lblViaGiur" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Civico
                </td>
                <td>
                    <asp:Label ID="lblCivGiur" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Cap
                </td>
                <td>
                    <asp:Label ID="lblCapGiur" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Telefono
                </td>
                <td>
                    <asp:Label ID="lblTelGiur" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <table class="stile_tabella2">
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 10pt; font-weight: bold;">
                    RAPPRESENTANTE
                </td>
            </tr>
        </table>
        <table class="stile_tabella2" cellpadding="4" cellspacing="1" style="border: 1px solid #0066FF;">
            <tr>
                <td class="stileEtichette">
                    Rappresentante Legale
                </td>
                <td>
                    <asp:Label ID="lblLegale" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Procuratore
                </td>
                <td>
                    <asp:Label ID="lblProcuratore" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Cognome
                </td>
                <td>
                    <asp:Label ID="lblCognome" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Nome
                </td>
                <td>
                    <asp:Label ID="lblNome" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Cod.Fiscale
                </td>
                <td>
                    <asp:Label ID="lblCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Sesso
                </td>
                <td>
                    <asp:Label ID="lblSesso" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Data Nascita
                </td>
                <td>
                    <asp:Label ID="lblDataNasc" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Cittadinanza
                </td>
                <td>
                    <asp:Label ID="lblCittadinanza" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Comune di nascita
                </td>
                <td>
                    <asp:Label ID="lblComNasc" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Tipo Documento
                </td>
                <td>
                    <asp:Label ID="lblTipoDoc" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Num. Documento
                </td>
                <td>
                    <asp:Label ID="lblNumDoc" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Data Rilascio
                </td>
                <td>
                    <asp:Label ID="lblDataRil" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Estremi doc. di soggiorno
                </td>
                <td>
                    <asp:Label ID="lblDocSogg" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="stile_tabella2">
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 10pt; font-weight: bold;">
                    RESIDENZA
                </td>
            </tr>
        </table>
        <table class="stile_tabella2" cellpadding="4" cellspacing="1" style="border: 1px solid #801f1c;
            border-collapse: collapse;">
            <tr>
                <td class="stileEtichette">
                    Comune
                </td>
                <td>
                    <asp:Label ID="lblComune" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Prov.
                </td>
                <td>
                    <asp:Label ID="lblProv" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    CAP
                </td>
                <td>
                    <asp:Label ID="lblCAP" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td width="200px">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td class="stileEtichette">
                    Indirizzo
                </td>
                <td>
                    <asp:Label ID="lblVia" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Civico
                </td>
                <td>
                    <asp:Label ID="lblCivico" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td class="stileEtichette">
                    Telefono
                </td>
                <td>
                    <asp:Label ID="lblTel" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                </td>
                <td>
                    &nbsp
                </td>
            </tr>
        </table>
        <table class="stile_tabella2">
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
                <td>
                    <asp:Label ID="lblDataAtt" runat="server" Text="Data decorrenza attuale" Font-Names="Arial"
                        Font-Size="10pt" Font-Bold="False" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataAttuale" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 10pt; font-weight: bold;">
                    MOTIVAZIONE
                    <asp:Label ID="lblScelta" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataNuova" runat="server" Text="Data decorrenza nuova" Font-Names="Arial"
                        Font-Size="10pt" Font-Bold="False" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataNuova" runat="server" Visible="False"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataNuova"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                        TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ForeColor="#CC3300"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="cmbMotivazioni" runat="server" Width="350px">
                    </asp:DropDownList>
                </td>
                 <td>
                    <asp:Label ID="lblDataCons" runat="server" Text="Data consegna nuova" Font-Names="Arial"
                        Font-Size="10pt" Font-Bold="False" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataConsegna" runat="server" Visible="False"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataConsegna"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
                        TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        ForeColor="#CC3300"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="padding-left: 580px;">
                    <br />
                    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        TabIndex="30" ToolTip="Procedi" OnClientClick="Conferma()" />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        TabIndex="31" ToolTip="Esci" OnClientClick="self.close();" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="ConfAnnull" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenScelta" runat="server" Value="0" />
    <asp:HiddenField ID="dataScadenza1" runat="server" Value="0" />
    <asp:HiddenField ID="dataScadenza2" runat="server" Value="0" />
    </form>
    <script type="text/javascript">

        function Conferma() {
            var avviso = false;
            if (document.getElementById('HiddenScelta').value == '3' && (document.getElementById('txtDataNuova').value == '' || document.getElementById('cmbMotivazioni').value == '-1')) {
                alert('Campi obbligatori!');
                avviso = true;
            }
            if (document.getElementById('HiddenScelta').value != '3' && document.getElementById('cmbMotivazioni').value == '-1') {
                alert('Selezionare la motivazione prima di procedere!');
                avviso = true;
            }
            if (avviso == false) {
                if (document.getElementById('HiddenScelta').value == '3') {
                    chiediConferma = window.confirm('Attenzione...procedendo con la variazione della data di decorrenza verranno calcolati eventuali crediti maturati durante il periodo di inutilizzo. Continuare?');
                }
                else {
                    chiediConferma = window.confirm('Attenzione...procedere con la CHIUSURA del contratto?');
                }
                if (chiediConferma == true) {
                    document.getElementById('ConfAnnull').value = '1';
                }
                else {
                    document.getElementById('ConfAnnull').value = '0';
                }
            }
        }
    </script>
</body>
</html>
