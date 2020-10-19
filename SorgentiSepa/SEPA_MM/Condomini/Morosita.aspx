<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Morosita.aspx.vb" Inherits="Condomini_Morosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <script type="text/javascript" language="javascript">
        window.name = "modal";

        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }

        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                event.keyCode = 0;
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        };


        function CompletaData(e, obj) {
            var sKeyPressed;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
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
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            document.getElementById('txtModificato').value = '1';
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
        };

        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
            }
        }
        function $onkeydown() {
            if (event.keyCode == 13) {
                //alert('ATTENZIONE!E\'stato premuto erroneamente il tasto invio! Utilizzare il mouse o il tasto TAB per spostarsi nei campi di testo!');
                //history.go(0);
                document.getElementById('txtModificato').value = '111'
                event.keyCode = 9;
            }
        }

        function NuovoDettMorInquilini() {
            if ((document.getElementById('txtDataRifDa').value == '') || (document.getElementById('txtDataRifA').value == '')) {
                alert('Inserire date riferimento!')
            }
            else if ((document.getElementById('txtDataDoc').value == '') || (document.getElementById('RegularExpressionValidator1').style.visibility != 'hidden')) {
                alert('Inserire data documentazione valida!')
            }
            else window.showModalDialog('DettMorInquilini.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnModale %>&IDMOROSITA=<%=vIdMorosita %>&IDVISUAL=<%=vIdVisual %>&TXTDATARIFDA=' + document.getElementById('txtDataRifDa').value + '&TXTDATARIFA=' + document.getElementById('txtDataRifA').value + '&DATADOC=' + document.getElementById('txtDataDoc').value + '&SL=' + document.getElementById('SoloLettura').value, 'window', 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        }

        function cercaDettMorosita() {
            if (document.getElementById('cmbDataDocum').value == -1) {
                alert('Inserire la data!')
            }
            else {
                window.showModalDialog('DettMorInquilini.aspx?IDCONDOMINIO=<%=vIdCondominio %>&IDCON=<%=vIdConnModale %>&IDMOROSITA=<%=vIdMorosita %>&IDVISUAL=<%=vIdVisual %>&TXTDATARIFDA=' + document.getElementById('txtDataRifDa').value + '&TXTDATARIFA=' + document.getElementById('txtDataRifA').value + '&DATADOC=' + document.getElementById('cmbDataDocum').value + '&SL=' + document.getElementById('SoloLettura').value, 'window', 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            }
        }

    </script>
    <title>Morosità Condominiale</title>
    <style type="text/css">
        .style1
        {
            width: 49%;
        }
    </style>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/SfondoContratto.png');
    background-repeat: no-repeat; width: 885px;">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" target="modal">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:Label ID="lblTitolo" runat="server" Style="position: absolute; top: 22px; left: 7px;
            width: 100%" Text="Morosità Condominio: NameCond"></asp:Label>
    </span></strong>
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <td colspan="5" style="font-family: Arial; font-size: 5pt">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="vertical-align: top; text-align: left">
                <table cellpadding="0" cellspacing="0" style="width: 97%">
                    <tr>
                        <td style="vertical-align: top; text-align: left; height: 15px;" class="style5">
                            <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Richiesta"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left; height: 15px;" class="style1">
                            <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Amministratore*"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td class="style4" style="height: 15px">
                            <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Riferimento Da*"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td class="style3" style="height: 15px">
                            <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Riferimento A*"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left;" class="style5">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Richiesta Rimborso"
                                Font-Bold="True" Width="140px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" class="style5">
                            <asp:TextBox ID="txtDataRichiesta" runat="server" Width="90px" BackColor="White"
                                TabIndex="1" MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtDataRichiesta"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; text-align: left;" class="style1">
                            <asp:DropDownList ID="cmbAmministratori" runat="server" Style="top: 109px; left: 9px;
                                right: 481px;" Font-Names="Arial" Font-Size="9pt" TabIndex="2" Width="232px"
                                BackColor="White" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td class="style4" style="vertical-align: top; text-align: left">
                            <asp:TextBox ID="txtDataRifDa" runat="server" Width="90px" BackColor="White" TabIndex="3"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataRifDa"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td class="style4" style="vertical-align: top; text-align: left">
                            <asp:TextBox ID="txtDataRifA" runat="server" Width="90px" BackColor="White" TabIndex="4"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDataRifA"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataRichRimborso" runat="server" Width="90px" BackColor="White"
                                TabIndex="5" MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRichRimborso"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" class="style5">
                            <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Arrivo"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left;" class="style1">
                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Text="Tipo Invio" Width="59px"></asp:Label>
                        </td>
                        <td class="style4">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Protocollo Gestore"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Arrivo Gestore"
                                Font-Bold="True" Width="120px"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" class="style5">
                            <asp:TextBox ID="txtDataArrivo" runat="server" Width="90px" BackColor="White" TabIndex="6"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataArrivo"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; text-align: left;" class="style1">
                            <asp:DropDownList ID="cmbTipoInvio" runat="server" Style="top: 109px; left: 9px;
                                right: 481px;" Font-Names="Arial" Font-Size="9pt" TabIndex="7" Width="141px"
                                BackColor="White">
                            </asp:DropDownList>
                        </td>
                        <td style="vertical-align: top; text-align: left;" class="style4">
                            <asp:TextBox ID="txtProtocollo" runat="server" Width="90px" BackColor="White" TabIndex="8"
                                MaxLength="20" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; text-align: left" class="style3">
                            <asp:TextBox ID="txtDataArrivoAler" runat="server" Width="90px" BackColor="White"
                                TabIndex="9" MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataArrivoAler"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                    left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" class="style5" colspan="5">
                            <table style="width: 100%; border: 2px solid #ccccff">
                                <tr>
                                    <td style="vertical-align: top; text-align: left; height: 15px;" class="style5">
                                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Mandato"
                                            Font-Bold="True" Width="120px"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: left; height: 15px;" class="style5">
                                        <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Numero Mandato"
                                            Font-Bold="True" Width="120px"></asp:Label>
                                    </td>
                                    <td style="vertical-align: top; text-align: left; height: 15px;" class="style5">
                                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Importo Mandato"
                                            Font-Bold="True" Width="120px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left;" class="style5">
                                        <asp:TextBox ID="txtDataMandato" runat="server" Width="90px" BackColor="White" TabIndex="10"
                                            MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataMandato"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                                left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="vertical-align: top; text-align: left;" class="style4">
                                        <asp:TextBox ID="txtNumeroMandato" runat="server" Width="90px" BackColor="White"
                                            TabIndex="11" MaxLength="20" Font-Names="Arial" Font-Size="9pt" Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td style="vertical-align: top; text-align: left;" class="style4">
                                        <asp:TextBox ID="txtImportoMandato" runat="server" Width="137px" BackColor="White"
                                            TabIndex="12" MaxLength="20" Font-Names="Arial" Font-Size="9pt" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" class="style5">
                            <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Note"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left;" class="style1">
                            &nbsp;
                        </td>
                        <td style="vertical-align: top; text-align: left" class="style4">
                            &nbsp;
                        </td>
                        <td style="vertical-align: top; text-align: left" class="style3">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" colspan="2">
                            <asp:TextBox ID="txtNote" runat="server" Width="122%" BackColor="White" TabIndex="13"
                                MaxLength="500" Font-Names="Arial" Font-Size="8pt" Style="text-align: left" Height="30px"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; text-align: center;" colspan="3">
                            <asp:CheckBox ID="ChkCompleto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Text="Completata e Stampabile" TabIndex="14" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data Documentazione"
                    Font-Bold="True" Width="140px"></asp:Label>
                <asp:DropDownList ID="cmbDataDocum" runat="server" Style="top: 109px; left: 9px;
                    right: 481px;" Font-Names="Arial" Font-Size="9pt" TabIndex="15" Width="106px"
                    OnSelectedIndexChanged="cmbDataDocum_SelectedIndexChanged" BackColor="White"
                    Height="21px">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:ImageButton ID="cercaDettaglio" runat="server" ImageUrl="Immagini/Search_16x16.png"
                    Style="z-index: 102; left: 115px; cursor: pointer; top: 26px; right: 600px;"
                    CausesValidation="False" OnClientClick="cercaDettMorosita()" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <div style="overflow: auto; width: 100%; height: 178px;" id="DivMorositaInquilini">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="DataGridMorosita" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 105;
                            left: 193px; top: 54px" Width="97%" TabIndex="18" BorderColor="#000033" BorderWidth="1px"
                            CellPadding="1" CellSpacing="1">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" HorizontalAlign="Center" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_INTESTARIO" HeaderText="ID_INTESTATARIO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_MOROSITA" HeaderText="ID_MOROSITA" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOMINATIVO" HeaderText="NOMINATIVO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD_CONTRATTO" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="POS. BIL."></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="INQUILINO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOMINATIVO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="40%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"
                                        Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCALA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTERNO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PIANO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PIANO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="VARIAZIONE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtVariazione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.VARIAZIONE") %>'
                                            Width="60px"></asp:TextBox>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
            <td style="text-align: left; vertical-align: top">
                <img alt="Aggiungi" src="Immagini/40px-Crystal_Clear_action_edit_add.png" onclick="myOpacityMorosita.toggle();"
                    id="AddInquilini" style="cursor: pointer" /><br />
                <br />
                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                    Style="z-index: 102; left: 392px; top: 387px" OnClientClick="DeleteConfirm()"
                    ToolTip="Elimina Elemento Selezionato" TabIndex="-1" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">
                &nbsp;
            </td>
            <td style="text-align: left; vertical-align: top">
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                    ReadOnly="True" Style="left: 13px; top: 197px" Width="100%">Nessuna Selezione</asp:TextBox>
            </td>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">
                &nbsp;
            </td>
            <td style="text-align: left; vertical-align: top">
                <table style="width: 97%;">
                    <tr>
                        <td style="width: 80%">
                            <asp:ImageButton ID="btnConguaglio" runat="server" ImageUrl="~/Condomini/Immagini/Img_Totale.png"
                                Style="z-index: 102; left: 392px; top: 387px; height: 16px;" ToolTip="Aggiungi"
                                CausesValidation="False" TabIndex="19" />
                        </td>
                        <td style="text-align: center" width="20%">
                            <asp:TextBox ID="txtTotale" runat="server" Width="60px" BackColor="White" TabIndex="-1"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: right" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
            <td style="text-align: right; vertical-align: top">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left" width="80%">
                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Style="z-index: 104; left: 9px; top: 222px" Visible="False" Width="80%"></asp:Label>
                        </td>
                        <td width="10%">
                            <asp:ImageButton ID="btnSalvaCambioAmm" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="20" ToolTip="Salva" Style="height: 16px" />
                        </td>
                        <td width="10%">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                TabIndex="21" ToolTip="Esci dalla finestra" Style="height: 16px" OnClientClick="ConfermaEsci();" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="VisibDiv" runat="server" Value="0" />
    <div id="DataDocum" style="border: thin solid #6699ff; position: absolute; z-index: 100;
        top: 296px; left: 558px; width: 290px; height: 52px; visibility: hidden; background-color: #C0C0C0;">
        <table width="95%">
            <tr>
                <td class="style1">
                    <asp:Label ID="lblDataDoc" runat="server" Width="180px" Font-Bold="True" Font-Names="Arial"
                        Font-Size="9pt" Text="Definire data documentazione"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDataDoc" runat="server" Width="70px" BackColor="White" TabIndex="16"
                        MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDoc"
                        ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                        left: 683px; top: 67px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="btnConfermaDataDoc" runat="server" ImageUrl="~/Condomini/Immagini/Conferma.png"
                        Style="z-index: 102; left: 392px; top: 387px" ToolTip="Conferma Data" CausesValidation="False"
                        OnClientClick="myOpacityMorosita.toggle()" TabIndex="17" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="ReadOnlyxMoro" runat="server" Value="0" />
    <asp:HiddenField ID="txtidIntestatario" runat="server" Value="0" />
    <asp:HiddenField ID="txtIdUi" runat="server" Value="0" />
    <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="txtinquilino" runat="server" />
    <asp:HiddenField ID="txtimporto" runat="server" />
    <asp:HiddenField ID="txtSalvato" runat="server" Value="0" />
    <asp:HiddenField ID="chkChecked" runat="server" Value="0" />
    <asp:HiddenField ID="noPatrimonio" runat="server" Value="0" />
    <asp:HiddenField ID="SoloLettura" runat="server" Value="0" />
    <script type="text/javascript">
        myOpacityMorosita = new fx.Opacity('DataDocum', { duration: 200 });
        myOpacityMorosita.hide();

        function DeleteConfirm() {
            if (document.getElementById('txtidIntestatario').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('txtConfElimina').value = '0';
                }
                else {
                    document.getElementById('txtConfElimina').value = '1';
                }
            }
        }
        if (document.getElementById('SoloLettura').value == 1) {

            document.getElementById('AddInquilini').style.visibility = 'hidden'
        } 
    </script>
    </form>
</body>
</html>
