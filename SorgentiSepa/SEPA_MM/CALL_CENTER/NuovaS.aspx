<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovaS.aspx.vb" Inherits="CALL_CENTER_NuovaS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
    <title>Ricerca Oggetto Segnalazione</title>
    <style type="text/css">
        #form1
        {
            width: 833px;
        }
        .style1
        {
            font-family: Arial;
            font-size: 10pt;
            text-decoration: underline;
            color: #000000;
        }
        .style3
        {
            width: 352px;
            height: 10px;
        }
        .style4
        {
            width: 27px;
            height: 10px;
        }
        .style5
        {
            width: 352px;
            height: 18px;
        }
        .style6
        {
            width: 27px;
            height: 18px;
        }
        .style8
        {
            font-family: Arial;
            font-size: 8pt;
            text-decoration: underline;
        }
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
    </style>
    <script type="text/javascript" language="javascript">

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


        var Selezionato;
        function ApriDettaglio() {
            if ((document.getElementById('txtCognomeInt').value != '') || (document.getElementById('TextBoxContratto').value != '')) {
                window.showModalDialog('FindInquilino.aspx?T=1&COGNOME=' + document.getElementById('txtCognomeInt').value + '&NOME=' + document.getElementById('txtNomeInt').value + '&COD=' + document.getElementById('TextBoxContratto').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
            } else {
                alert('Definire almeno il cognome o il codice contratto dell\'intestatario!');
                return false;
            };
        };

        function ApriDettaglioChiamante() {
            if (document.getElementById('txtCognChiama').value != '') {
                window.showModalDialog('FindInquilino.aspx?T=2&COGNOME=' + document.getElementById('txtCognChiama').value + '&NOME=' + document.getElementById('txtNomeChiama').value + '', 'window', 'status:no;dialogWidth:800px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
            } else {
                alert('Definire almeno il cognome del chiamante!');
                return false;
            };
        };

        function ConfermaEsci() {
            var chiediConferma
            chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            if (chiediConferma == true) {
                self.close();
            }

        }
        function Apri() {

            if (document.getElementById("idSelected").value != 0) {
                window.open('SegnalazioneP.aspx?ID=' + document.getElementById("idSelected").value + '', 'Segnalazione', 'height=700px,width=900px,resizable=yes');

            }
            else {
                alert('Selezionare un elemento dalla lista dei risultati!');
            }
        }
        function ControlSearch() {
            document.getElementById('txtmia').value = 'Nessuna Selezione';
            document.getElementById('idSelected').value = 0;
            var cmb = document.getElementById('cmbEdificio');
            var selVal = cmb.options[cmb.selectedIndex].text;
            if (selVal == '--') {

                alert('Selezionare almeno l\'edificio per eseguire la ricerca!');

            }

        }

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
    <form id="form1" runat="server" style="width: 100%">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Individuazione
        dell'oggetto della richiesta e del soggetto chiamante</span></strong>
    <table width="100%">
        <tr>
            <td style="font-family: Arial; font-size: 7pt">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Richiesta"
                                Width="100px" style="display:none"></asp:Label>
                        
                            <asp:DropDownList ID="cmbTipoRichiesta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="150px" AutoPostBack="True" Enabled="False" style="display:none">
                                <asp:ListItem Value="-1">--</asp:ListItem>
                                <asp:ListItem Value="1">SEGNALAZIONE GUASTI</asp:ListItem>
                            </asp:DropDownList>
                        
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Segnalazione"
                                Width="100px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoSegnalazione" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="150px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Intervento"
                                Width="100px"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbTipoIntervento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="150px" Enabled="False">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmbTipoRichiesta" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong style="font-size: 8pt">Chiamante</strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="border: 1px solid #33CC33">
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cognome"
                                        Width="60px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCognChiama" runat="server" CssClass="CssMaiuscolo" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="100" Width="300px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nome"
                                        Width="70px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomeChiama" runat="server" CssClass="CssMaiuscolo" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="100" Width="300px"></asp:TextBox>
                                    <asp:ImageButton ID="btnFindImmobileChiamante" runat="server" ImageUrl="~/CALL_CENTER/Immagini/houseIco.png"
                                        ToolTip="Cerca/Aggiorna informazioni chiamante in base all'intestatario" OnClientClick="ApriDettaglioChiamante();" />
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
                                                <asp:TextBox ID="txtTel1" runat="server" CssClass="CssMaiuscolo" MaxLength="100"
                                                    Width="180px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tel.2"
                                                    Width="40px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTel2" runat="server" MaxLength="100" CssClass="CssMaiuscolo"
                                                    Width="180px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="e-Mail"
                                                    Width="40px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMail" runat="server" MaxLength="100" CssClass="CssMaiuscolo"
                                                    Width="300px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnFindImmobile" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnFindImmobileChiamante" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmbInterno" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="BtnConferma" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <%--<tr>
            <td style="width:100%;text-align: center">
                <asp:ImageButton ID="imgCopia" runat="server" Height="24px" 
                    ImageUrl="~/CALL_CENTER/Immagini/down-icon.png" 
                    ToolTip="Copia valori chiamante" Width="24px" style="display:none;" />
            </td>
        </tr>
        --%><tr>
            <td class="style1">
                <strong style="font-size: 8pt">Oggetto della Chiamata</strong>
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
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCognomeInt" runat="server" CssClass="CssMaiuscolo" MaxLength="100"
                                        Width="300px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmbInterno" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbEdificio" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobile" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobileChiamante" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbPiano" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnConferma" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nome Int."
                                Width="60px"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtNomeInt" runat="server" CssClass="CssMaiuscolo" MaxLength="100"
                                        Width="340px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                    <asp:ImageButton ID="btnFindImmobile" runat="server" ImageUrl="~/CALL_CENTER/Immagini/houseIco.png"
                                        ToolTip="Cerca/Aggiorna informazioni immobiliari in base all'intestatario" OnClientClick="ApriDettaglio();" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmbInterno" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbEdificio" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbPiano" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobile" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobileChiamante" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnConferma" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                    <td>
                            <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cod.Contratto"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBoxContratto" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Width="125px"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmbInterno" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbEdificio" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobile" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobileChiamante" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbPiano" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnConferma" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="8pt" Style="border: 1px solid black;" TabIndex="1" 
                                        Width="700px" AutoPostBack="True">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;
                                    <img id="Img1" alt="Aiuto Ricerca per Denominazione per Edificio" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                                        src="Immagini/Search_24x24.png" style="cursor: pointer;" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="BtnConferma" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobile" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobileChiamante" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Interno"
                                Width="70px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table frame="border" style="border-spacing: 0px" cellpadding="0" cellspacing="0"
                                        border="0">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="cmbInterno" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="80px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Scala"
                                                    Width="40px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbScala" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="80px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cmbInterno" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Piano"
                                                    Width="40px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cmbPiano" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="150px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="cmbInterno" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Struttura"
                                                    Width="45px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="cmbStruttura" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Width="300px" ToolTip="Struttura di Competenza">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cmbEdificio" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="BtnConferma" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobile" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnFindImmobileChiamante" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbScala" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="cmbTipoIntervento" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
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
                            <asp:ImageButton ID="btnRefreshDb" runat="server" ImageUrl="~/CALL_CENTER/Immagini/dbRefresh.png"
                                ToolTip="Carica/Aggiorna i risultati" OnClientClick="ControlSearch();" />
                        </td>
                        <td>
                            <span class="style8"><strong>Elenco richieste pendenti per lo stesso Oggetto della Chiamata</strong></span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 250px; vertical-align: top;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="overflow: auto;height:245px;">
                            <asp:DataGrid ID="DataGridSegnalaz" runat="server" AutoGenerateColumns="False" BackColor="White"
                                CellPadding="1" CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                GridLines="None" PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="100%">
                                <PagerStyle Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle ForeColor="Black" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NUM" HeaderText="N°"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO" HeaderText="Tipo"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_INT" HeaderText="Tipo Int."></asp:BoundColumn>
                                    <asp:BoundColumn DataField="STATO" HeaderText="Stato"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="RICHIEDENTE" HeaderText="Richiedente"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="Data Ins."></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#0000C0" />
                            </asp:DataGrid>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnRefreshDb" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" ReadOnly="True"
                    Style="z-index: 500;" Width="685px">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="width:25%">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/CALL_CENTER/Immagini/Img_NuovaSegnalazione.png" />
                        </td>
                        <td style="text-align: right;width:25%">
                            <asp:Image ID="imgApri" runat="server" Style="cursor: pointer" ImageUrl="~/CALL_CENTER/Immagini/Img_Visualizza.png"
                                onclick="Apri();" />
                        </td>
                        <td style="text-align: right;width:25%">
                            <asp:ImageButton ID="imgSvuota" runat="server" 
                                ImageUrl="~/CALL_CENTER/Immagini/Img_Svuota.png"  />
                        </td>
                        <td style="text-align: right;width:25%">
                            <asp:Image ID="imgEsci" runat="server" Style="cursor: pointer" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                onclick="ConfermaEsci();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="AiutoRicerca" style="z-index: 200; left: 454px; width: 402px; position: absolute;
        top: 121px; height: 373px; background-color: transparent; visibility: hidden;">
        <br />
        <div style="width: 60px; height: 136px; background-color: silver">
            <table style="width: 395px; height: 335px; background-color: silver">
                <tr>
                    <td class="style3" style="vertical-align: top; text-align: left">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="190px">Denominazione Via</asp:Label>
                    </td>
                    <td class="style4" style="vertical-align: baseline; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td class="style5" style="vertical-align: top; text-align: left">
                        <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                            ToolTip="Approssimare la ricerca per questo indirizzo" Width="343px"></asp:TextBox>
                    </td>
                    <td class="style6" style="vertical-align: top; text-align: left">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                            Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="left: 5px; overflow: auto; width: 365px; top: 87px; height: 243px">
                                    <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Width="340px">
                                    </asp:RadioButtonList>
                                    <asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Red" Style="z-index: 500; left: 14px; top: 73px" Visible="False" Width="120px">Nessun Risultato</asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="vertical-align: bottom; width: 27px; height: 104px; text-align: left"
                        valign="bottom">
                        <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                            OnClientClick="myOpacity.toggle();" Style="z-index: 111; left: 268px; top: 190px"
                            ToolTip="Conferma" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="idSelected" runat="server" Value="0" />
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="unita" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="Immagini/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
    <script type="text/javascript">

        myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
        //myOpacity.hide();

        if (document.getElementById('TextBox1').value != '2') {
            myOpacity.hide(); ;
        }
    </script>
</body>
</html>
