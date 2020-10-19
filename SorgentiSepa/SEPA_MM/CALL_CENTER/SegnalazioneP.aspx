<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SegnalazioneP.aspx.vb" Inherits="CALL_CENTER_SegnalazioneP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Segnalazione</title>
    <style type="text/css">
        #form1
        {
            width: 920px;
        }
        .style1
        {
            font-family: Arial;
            font-size: 3pt;
            text-decoration: underline;
            color: #000000;
        }
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
        
        .style3
        {
            font-family: Arial;
            font-size: 12pt;
        }
    </style>
    <script type="text/javascript">
        function richiestaConferma() {
            if (<%= tipoS %> == '0') {
                if (document.getElementById('appuntamentoPresente').value == '1') {
                    var chiediConferma = window.confirm('La segnalazione di tipo amministrativo ha un appuntamento.\nSe si modifica questa tipologia l\'appuntamento verrà eliminato.\nProseguire?');
                    if (chiediConferma == true) {
                        document.getElementById('confermaGenerica').value = '1';
                    } else {
                        document.getElementById('confermaGenerica').value = '0';
                    };
                } else {
                    document.getElementById('confermaGenerica').value = '1';
                };
            } else {
                document.getElementById('confermaGenerica').value = '1';
            };

        };
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

        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13 || event.keyCode == 8) {
                e.preventDefault();
            }
        }
        function $onkeydown() {
            if (event.keyCode == 13 || event.keyCode == 8) {
                event.keyCode = 0;
            }
        }

        function ConfermaEsci() {
            var chiediConferma
            chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            if (chiediConferma == true) {
                self.close();
            }

        }
        function disabilitaMinore(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 226)
                return false;
            else
                return true;
        }

        function ConfAggiorna() {
            if (document.getElementById('idUiAggiorna').value != '') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sei sicuro di voler impostare l\'unita immobiliare su questa segnalazione?.");
                if (chiediConferma == true) {
                    document.getElementById('confAggiorna').value = '1';

                }
                else {
                    document.getElementById('confAggiorna').value = '0';

                }
            }
            else {

                alert('Seleziona una Unita Immobiliare per aggiornare la segnalazione!');
                return false;
            }

        }
        function ConfermaAnnulla() {

            if (document.getElementById('id').value != '-1') {
                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler annullare questa segnalazione?");
                if (chiediConferma == true) {
                    document.getElementById('sicuro').value = '1';
                    document.getElementById('splash').style.visibility = 'visible';
                    //  myOpacity.toggle();
                }
                else {
                    document.getElementById('sicuro').value = '0';
                }
            }
            else {
                alert('Non è possibile annulare la segnalazione perchè non ancora salvata!');
                document.getElementById('sicuro').value = '0';
            }
        }
        function ConfermaChiusura() {


            var chiediConferma
            chiediConferma = window.confirm("Sei sicuro di voler Chiudere questa segnalazione?");
            if (chiediConferma == true) {
                document.getElementById('sicuro').value = '1';
                document.getElementById('splash').style.visibility = 'visible';
                //  myOpacity.toggle();
            }
            else {
                document.getElementById('sicuro').value = '0';
            }
        }

        function Stampa() {
            window.open('StampaSegnalazione.aspx?ID=' + document.getElementById('id').value, 'Stampa', '');
        }

        function getDropDownListvalue() {
            var e = document.getElementById("cmbNoteChiusura");
            var strUser = e.options[e.selectedIndex].value;
            document.getElementById("txtDescNoteChiusura").value = strUser

        }
        function ApriAgenda() {
            var left = (screen.width / 2) - (1000 / 2);
            var top = (screen.height / 2) - (770 / 2);
            var targetWin = window.open('Agenda/Agenda.aspx?IDS=' + document.getElementById('id').value, 'AgendaCallCenter', 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=1000,height=770,top=' + top + ',left=' + left);
        };
    </script>
</head>
<body style="background-attachment: fixed; background-image: url('Immagini/XBackGround.gif');
    background-repeat: repeat-x;">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server" style="width: 100%">
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Segnalazione</span></strong>
    <img id="ImgEventi" alt="Eventi" border="0" onclick="window.open('Eventi.aspx?IDS=<%=vIdSegnalazione %>','Eventi', '');"
        src="../NuoveImm/Img_Eventi.png" style="cursor: pointer; left: 813px; position: absolute;
        top: 22px; right: 117px;" />
    <br />
    <table style="width: 100%;">
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td style="vertical-align: bottom; text-align: left">
                <table style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #C0C0C0;"
                    width="100%">
                    <tr>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="30px" Height="16px">DATA:</asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:TextBox ID="lblDataIns" runat="server" MaxLength="10" Width="70px" Font-Names="Arial"
                                Font-Size="8pt"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="lblDataIns"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" ToolTip="Inserire una data valida"
                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                SetFocusOnError="True" ValidationGroup="a"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="25px" Height="16px">ORA:</asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:TextBox ID="txtOra" runat="server" MaxLength="5" Width="40px" Font-Names="Arial"
                                Font-Size="8pt" ToolTip="Ora segnalazione in formato HH:MM"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOra"
                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="75px">N° RICHIESTA:</asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="lblNrich" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Width="80px"></asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">STATO:</asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="lblStato" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                Width="249px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblAppuntamento" Font-Names="Arial" Font-Size="7pt"
                                Text="<a href='javascript:void(0);ApriAgenda();'>Richiedi appuntamento</a>;"
                                Width="110px" />
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="lblsollecito" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" Width="150px">NON SOLLECITATA</asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <asp:Label ID="lblInoltro" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="50px" Visible="False">Inoltrata il:</asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Label Text="Tipo segnalazione" runat="server" Font-Names="Arial" Font-Size="8pt" />
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList runat="server" ID="cmbTipoSegnalazione" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="8pt" onchange="richiestaConferma();">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%">
                            <asp:Label ID="lblTipoIntervento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Text="Tipo Intervento" Width="100%"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:DropDownList ID="cmbTipoIntervento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="50%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40%">
                            <asp:Label ID="lblInCondominio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="100%" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: none">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Richiesta:"
                                    Width="70px"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTipoRichiesta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="250px" AutoPostBack="True" Enabled="False">
                                    <asp:ListItem Value="0">RICHIESTA INFORMAZIONI</asp:ListItem>
                                    <asp:ListItem Value="1">SEGNALAZIONE GUASTI</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="-1">TUTTE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong style="font-size: 8pt">Chiamante</strong>
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: 1px solid #33CC33">
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cognome"
                                Width="60px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCognChiama" runat="server" MaxLength="100" Width="350px" Font-Names="Arial"
                                Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nome"
                                Width="50px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNomeChiama" runat="server" MaxLength="100" Width="340px" Font-Names="Arial"
                                Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tel. 1"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <table frame="border" style="border-spacing: 0px" cellpadding="0" cellspacing="0"
                                border="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtTel1" runat="server" MaxLength="100" Width="180px" Font-Names="Arial"
                                            Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tel.2"
                                            Width="40px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTel2" runat="server" MaxLength="100" Width="180px" Font-Names="Arial"
                                            Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="e-Mail"
                                            Width="40px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMail" runat="server" MaxLength="100" Width="310px" Font-Names="Arial"
                                            Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 950px">
                    <tr>
                        <td class="style1" style="width: 150px">
                            <strong style="font-size: 8pt">Oggetto della Chiamata</strong>
                        </td>
                        <td>
                            <asp:Label ID="lblOggetto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="200px" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblImpianto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="200px" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcont" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Width="70px" Visible="False">CONTRATTO:</asp:Label>
                            <asp:Label ID="lblContratto" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" Width="200px" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: 1px solid #0066FF">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cognome Int."
                                Width="70px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCognomeInt" runat="server" MaxLength="100" Width="340px" Font-Names="Arial"
                                Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nome Int."
                                Width="60px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNomeInt" runat="server" MaxLength="100" Width="340px" Font-Names="Arial"
                                Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White" Font-Names="arial"
                                Font-Size="10pt" Style="border: 1px solid black;" TabIndex="1" Width="760px"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Interno"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <table frame="border" style="border-spacing: 0px" cellpadding="0" cellspacing="0"
                                border="0">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cmbInterno" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="80px" AutoPostBack="True" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Scala"
                                            Width="40px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbScala" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="80px" AutoPostBack="True" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Piano"
                                            Width="40px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbPiano" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="150px" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Struttura Comp."
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbStruttura" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="300px" ToolTip="Struttura di Competenza" Enabled="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Richiesta pervenuta per:"
                                Width="120px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoPervenuta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="250px">
                                <asp:ListItem>Telefonica</asp:ListItem>
                                <asp:ListItem>Fax</asp:ListItem>
                                <asp:ListItem Value="e-Mail">e-Mail</asp:ListItem>
                                <asp:ListItem>Postale</asp:ListItem>
                                <asp:ListItem>Verbale</asp:ListItem>
                                <asp:ListItem>Richiesta Interna</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblUrgenza" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Urgenza/pericolo"
                                Style="text-align: right" Width="100px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbUrgenza" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="50px" onchange="document.getElementById('txtDescrizione').focus();">
                                <asp:ListItem style="background-color: White; color: White;" Value="Bianco">&nbsp;</asp:ListItem>
                                <asp:ListItem style="background-color: Green; color: Green;" Value="Verde"></asp:ListItem>
                                <asp:ListItem style="background-color: Yellow; color: Yellow;" Value="Giallo"></asp:ListItem>
                                <asp:ListItem style="background-color: Red; color: Red;" Value="Rosso"></asp:ListItem>
                                <asp:ListItem style="background-color: Blue; color: Blue;" Value="Blu"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong style="font-size: 8pt">Descrizione Richiesta</strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDescrizione" runat="server" MaxLength="1000" Width="90%" Font-Names="Arial"
                    Font-Size="8pt" Height="40px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong style="font-size: 8pt">NUOVA NOTA (uso interno)</strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtNote" runat="server" MaxLength="1000" Width="90%" Font-Names="Arial"
                    Font-Size="8pt" Height="40px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong style="font-size: 8pt">NOTE PRECEDENTI (uso interno)</strong>
            </td>
        </tr>
        <tr>
            <td>
                <div id="NOTE" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4;
                    overflow: scroll; width: 90%;">
                    <%=TabellaNote %>
                </div>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong style="font-size: 8pt">ELENCO ALLEGATI</strong>
            </td>
        </tr>
        <tr>
            <td>
                <div id="Allegati" style="border: 1px solid #000000; height: 70px; background-color: #E4E4E4;
                    overflow: scroll; visibility: visible; width: 90%;">
                    <%=TabellaAllegati %>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 95%">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="Immagini/Img_Annulla_Segnalazione.png"
                                ToolTip="Annulla Segnalazione" OnClientClick="ConfermaAnnulla();" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="imgConferma" runat="server" ImageUrl="Immagini/Img_Conferma_Segnalazione.png"
                                ToolTip="Conferma Segnalazione" OnClientClick="ConfermaConferma();" Visible="False" />
                        </td>
                        <td style="text-align: right">
                            <asp:Image ID="btnSollecito" runat="server" ImageUrl="~/NuoveImm/Img_Sollecito.png"
                                Style="cursor: pointer; left: 314px;" ToolTip="Sollecito" onclick="if (document.getElementById('id').value != '-1') {myOpacity2.toggle();}"
                                TabIndex="10" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="imgChiudiSegnalazione" runat="server" ImageUrl="Immagini/Img_ChiudiSegnalazione.png"
                                ToolTip="Chiudi Segnalazione" OnClientClick="if (document.getElementById('id').value != '-1') {myOpacity3.toggle();} else {alert('Salvare prima di procedere!');}return false;" />
                        </td>
                        <td style="text-align: right">
                            <asp:Image ID="imgAllega" runat="server" Style="cursor: pointer;" ImageUrl="~/CALL_CENTER/Immagini/Img_Allega.png"
                                onclick="if (document.getElementById('id').value != '-1') {myOpacity.toggle();} else {alert('Salvare prima di allegare file!');}"
                                TabIndex="11" ToolTip="Allega" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                ToolTip="Salva" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                            <img id="stampa" alt="" src="../NuoveImm/Img_Stampa_Grande.png" onclick="Stampa();"
                                style="cursor: pointer" />
                        </td>
                        <td style="text-align: right; vertical-align: top;">
                            <asp:ImageButton ID="btnStampSopr" runat="server" ImageUrl="~/CALL_CENTER/Immagini/PrintSop.png"
                                ToolTip="Stampa il documento di Sopralluogo" />
                        </td>
                        <td>
                            <asp:Image ID="imgEsci" runat="server" Style="cursor: pointer" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                onclick="ConfermaEsci();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="Allega" style="width: 850px; height: 600px; position: absolute; top: 50px;
        left: 20px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #C0C0C0;
        visibility: hidden;">
        <table border="0" cellpadding="1" cellspacing="1" style="z-index: 200; left: 40px;
            width: 717px; position: absolute; top: 48px; height: 354px; background-color: #FFFFFF;">
            <tr>
                <td style="width: 404px; height: 19px; text-align: left">
                    <strong><span style="font-family: Arial">INSERIMENTO ALLEGATO</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                        TabIndex="4" Width="100%" />
                    <br />
                    <br />
                    <span class="style2"><strong>Nome</strong></span><br />
                    <asp:TextBox ID="txtDescrizioneA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        CssClass="CssMaiuscolo" Font-Names="ARIAL" Font-Size="9pt" MaxLength="25" TabIndex="4"
                        Width="231px" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Solo lettere (a-z) Max 15 caratteri senza spazi."
                        Font-Names="arial" Font-Size="8pt" ValidationExpression="^[|a-zA-Z]{1,15}$" ControlToValidate="txtDescrizioneA"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr style="text-align: right; font-size: 12pt; font-family: Times New Roman">
                <td style="text-align: left;">
                    <span class="style2"><strong>Descrizione</strong></span><br />
                    <asp:TextBox ID="txtDescrizioneAll" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        CssClass="CssMaiuscolo" Font-Names="ARIAL" Font-Size="9pt" MaxLength="500" TabIndex="4"
                        Width="90%" onkeydown="return disabilitaMinore(event)" Height="71px"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: right; font-size: 12pt; font-family: Times New Roman">
                <td style="width: 404px;">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 135%">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="img_InserisciBolletta" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                    ToolTip="Inserisci un allegato" Style="cursor: pointer;" TabIndex="409" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />&nbsp;
                                <asp:Image ID="img_ChiudiBolletta" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    onclick="myOpacity.toggle();" ToolTip="Esci senza inserire" Style="cursor: pointer"
                                    TabIndex="410" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="Sollecito" style="width: 872px; height: 555px; position: absolute; top: 50px;
        left: 50px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #C0C0C0;
        visibility: hidden;">
        <table border="0" cellpadding="1" cellspacing="1" style="z-index: 200; left: 211px;
            width: 448px; position: absolute; top: 187px; height: 150px; background-color: #FFFFFF;">
            <tr>
                <td style="width: 404px; height: 19px; text-align: left">
                    <strong><span style="font-family: Arial">RICHIESTA SOLLECITO</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 106;
                        left: 4px; position: absolute; top: 57px; width: 393px;">Sollecitare la seguente segnalazione?</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 106;
                        left: 4px; position: absolute; top: 93px; width: 393px;">Note:</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtNoteSoll" runat="server" Style="z-index: 106; left: 2px; position: absolute;
                        top: 114px; width: 393px;" TextMode="MultiLine" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                </td>
            </tr>
            <tr style="text-align: right; font-size: 12pt; font-family: Times New Roman;">
                <td style="width: 404px;">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 110%; top: 184px;
                        left: 10px;">
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnSalvaSoll" runat="server" ImageUrl="~/NuoveImm/Img_Applica.png"
                                    ToolTip="Invia sollecito" Style="cursor: pointer;" TabIndex="409" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />&nbsp;
                                <asp:Image ID="btnEsciSoll" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                    onclick="myOpacity2.toggle();" ToolTip="Esci senza sollecitare la richiesta"
                                    Style="cursor: pointer" TabIndex="410" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="NoteChiusura" style="width: 850px; height: 600px; position: absolute; top: 50px;
        left: 20px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #C0C0C0;
        visibility: hidden;">
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td width="60%">
                    &nbsp;
                </td>
                <td width="10%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="10%">
                    &nbsp;
                </td>
                <td>
                    <table style="width: 100%; background-color: #FFFFFF;">
                        <tr>
                            <td class="style3">
                                <strong>NOTE DI CHIUSURA SEGNALAZIONE</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="cmbNoteChiusura" runat="server" Width="80%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <strong>Note libere</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDescNoteChiusura" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    CssClass="CssMaiuscolo" Font-Names="ARIAL" Font-Size="9pt" MaxLength="500" TabIndex="4"
                                    Width="90%" onkeydown="return disabilitaMinore(event)" Height="71px" TextMode="MultiLine"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <strong>Data e Ora Chiusura Intervento</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                Width="30px" Height="16px">DATA:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDataCInt" runat="server" MaxLength="10" Width="70px" Font-Names="Arial"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataCInt"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" ToolTip="Inserire una data valida"
                                                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                SetFocusOnError="True" ValidationGroup="a"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                Width="25px" Height="16px">ORA:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOraCInt" runat="server" MaxLength="5" Width="40px" Font-Names="Arial"
                                                Font-Size="8pt" ToolTip="Ora segnalazione in formato HH:MM"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtOraCInt"
                                                ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="Inserire orario formato HH:MM"
                                                ValidationExpression="([01]?[0-9]|2[0-3])(.|:)[0-5][0-9]"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <table width="80%">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="img_InserisciBolletta0" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                                ToolTip="Inserisci note di chiusura" Style="cursor: pointer; text-align: right;"
                                                TabIndex="409" OnClientClick="ConfermaChiusura()" />
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Image ID="img_ChiudiBolletta0" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                                onclick="myOpacity3.toggle();" ToolTip="Esci senza inserire" Style="cursor: pointer"
                                                TabIndex="410" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="10%">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="btnControllaAppuntamento" runat="server" Style="display: none" />
    <asp:HiddenField ID="appuntamentoPresente" runat="server" Value="0" />
    <asp:HiddenField ID="confermaGenerica" runat="server" Value="0" />
    <asp:HiddenField ID="unita" runat="server" />
    <asp:HiddenField ID="idNote" runat="server" />
    <asp:HiddenField ID="id" runat="server" Value="-1" />
    <asp:HiddenField ID="sicuro" runat="server" />
    <script type="text/javascript">
        myOpacity = new fx.Opacity('Allega', { duration: 200 });
        myOpacity.hide();

        myOpacity2 = new fx.Opacity('Sollecito', { duration: 200 });
        myOpacity2.hide();

        myOpacity3 = new fx.Opacity('NoteChiusura', { duration: 200 });
        myOpacity3.hide();


        if (document.getElementById('id').value != '-1') {
            document.getElementById('stampa').style.visibility = 'visible'
        }
        else {
            document.getElementById('stampa').style.visibility = 'hidden'

        }
        document.getElementById('splash').style.visibility = 'hidden';

    </script>
    </form>
</body>
</html>
