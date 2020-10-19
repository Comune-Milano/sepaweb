<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SirAlloggio.aspx.vb" Inherits="SIRAPER_SirAlloggio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jsFunzioni.js"></script>
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <title>Alloggio</title>
</head>
<body style="background-image: url('Immagini/Sfondo.png'); background-repeat: repeat-x;">
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
    <form id="form1" runat="server" target="modal">
    <table style="width: 100%;">
        <tr style="height: 35px;">
            <td>
                <asp:Label ID="lblTitolo" runat="server" Text="DETTAGLIO ALLOGGIO" 
                    Font-Names="Arial" Font-Size="10pt"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 99%;">
                    <tr>
                        <td>
                            <div style="width: 97%; height: 625px; overflow: auto;">
                                <table>
                                    <tr>
                                        <td>
                                            CODICE MIR
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodiceMir" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" BackColor="#FFFF99" ForeColor="#FF3300" Style="text-align: right;"
                                                MaxLength="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CODICE ALLOGGIO DELL'ENTE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="CODICE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CONTRATTO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="CONTRATTO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ENTE GESTORE DELL'ALLOGGIO CODICE FISCALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="COD_FISCALE_ENTE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ENTE GESTORE DELL'ALLOGGIO PARTITA IVA
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="P_IVA_ENTE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            NOME ENTE GESTORE DELL'ALLOGGIO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="SIGLA_ENTE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIPO ENTE GESTORE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="TIPO_ENTE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIPO AMMINISTRAZIONE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="TIPO_AMMINISTRAZIONE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ALLOGGIO DISMESSO/ CARTOLARIZZATO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ALL_DISM_CART" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PROVENTI DA DISMISSIONE O DA CARTOLARIZZAZIONE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProventi" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ALLOGGIO A RISCATTO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkRiscatto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SEZIONE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="SEZIONE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            FOGLIO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="FOGLIO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MAPPALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MAPPALE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUBALTERNO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="SUBALTERNO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            INDIRIZZO DELL'UNITA'
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="INDIRIZZO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            LOCALITA'
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="LOCALITA" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COORDINATA X GAUSS-BOAGA
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCordGaussX" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COORDINATA Y GAUSS-BOAGA
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCordGaussY" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ALLOGGIO OCCUPATO/NON OCCUPATO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ALL_OCCUPATO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIPO DI GODIMENTO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="TIPO_GODIMENTO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ALLOGGIO ESCLUSO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkEscluso" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CATEGORIA CATASTALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlgestionedificio" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.CATEGORIA_CATASTALE") %>'
                                                Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="A01" Text="ABITAZIONI DI TIPO SIGNORILE"></asp:ListItem>
                                                <asp:ListItem Value="A02" Text="ABITAZIONI DI TIPO CIVILE"></asp:ListItem>
                                                <asp:ListItem Value="A03" Text="ABITAZIONI DI TIPO ECONOMICO"></asp:ListItem>
                                                <asp:ListItem Value="A04" Text="ABITAZIONI DI TIPO POPOLARE"></asp:ListItem>
                                                <asp:ListItem Value="A05" Text="ABITAZIONI DI TIPO ULTRAPOPOLARE"></asp:ListItem>
                                                <asp:ListItem Value="A06" Text="ABITAZIONI DI TIPO RURALE"></asp:ListItem>
                                                <asp:ListItem Value="A07" Text="ABITAZIONI IN VILLINI"></asp:ListItem>
                                                <asp:ListItem Value="A08" Text="ABITAZIONI IN VILLE"></asp:ListItem>
                                                <asp:ListItem Value="A09" Text="CASTELLI, PALAZZI DI EMINENTI PREGI ARTISTICI O STORICI"></asp:ListItem>
                                                <asp:ListItem Value="A10" Text="UFFICI E STUDI PRIVATI"></asp:ListItem>
                                                <asp:ListItem Value="A11" Text="ABITAZIONI ED ALLOGGI TIPICI DEI LUOGHI"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            RENDITA CATASTALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRendita" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COEFFICIENTE CLASSE DEMOGRAFICA (ante legem)
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlcoeffante" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="19" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 400.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 250.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="21" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 100.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="22" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 50.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="23" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 10.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="24" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE FINO A 10.000 ABITANTI"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COEFFICIENTE CLASSE DEMOGRAFICA (L.R. n°27/2009)
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlcoefflr" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="19" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 400.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 250.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="21" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 100.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="25" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 50.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="26" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 30.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="23" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE SUPERIORE A 10.000 ABITANTI"></asp:ListItem>
                                                <asp:ListItem Value="24" Text="PER GLI IMMOBILI SITI IN COMUNI CON POPOLAZIONE FINO A 10.000 ABITANTI"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            NUMERO DI STANZE DELL'ALLOGGIO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumStanze" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="60px" Style="text-align: right;" MaxLength="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ANNO DI ULTIMAZIONE O DI RECUPERO O DI RISTRUTTURAZIONE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAnnoRistrutturazione" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="40px" Style="text-align: right;" MaxLength="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIPO D'INTERVENTO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddltipointervento" runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.TIPO_INTERVENTO") %>'
                                                Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="NC" Text="NUOVA COSTRUZIONE"></asp:ListItem>
                                                <asp:ListItem Value="RE" Text="RECUPERO/RISTRUTTURAZIONE STRAORDINARIA"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PIANO IN CUI E' SITUATO L'ALLOGGIO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PIANO_ALLOGGIO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COEFFICIENTE PIANO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="COEFF_PIANO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE UTILE ALLOGGIO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsuputileall" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE EFFETTIVA CANTINE E/O SOFFITTE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupcantine" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE EFFETTIVA BALCONI
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupbalconi" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE EFFETTIVA AREA PRIVATA
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupareaprivata" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE VERDE CONDOMINIALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupverdecond" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE BOX
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupbox" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            NUMERO BOX/POSTI AUTO DI PERTINENZA DELL'ALLOGGIO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="NUM_BOX" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE POSTO AUTO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupostauto" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE ALTRE PERTINENZE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="supertinenze" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE CONVENZIONALE (ante legem)
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupconante" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SUPERFICIE CONVENZIONALE (L.R. n°27/2009)*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtsupconvlr" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            RISCALDAMENTO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="RISCALDAMENTO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ASCENSORE AL SERVIZIO ALLOGGIO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ASCENSORE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE ACCESSI
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsaccessi" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE FACCIATE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsfacc" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE PAVIMENTI
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconspav" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE PARETI E SOFFITTI
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconspareti" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE INFISSI
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsinfissi" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE IMPIANTO ELETTRICO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsimpele" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE IMPIANTO IDRICO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsimpidri" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI CONSERVAZIONE IMPIANTO DI RISCALDAMENTO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsimprisc" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO GENERALE DI CONSERVAZIONE ALLOGGIO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatoconsall" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="NORMALE"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MEDIOCRE"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="SCADENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIPO DI CUCINA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddltipocucina" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SUP. INFERIORE AD 8 MQ"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="SUP. SUPERIORE AD 8 MQ"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="CUCINA ASSENTE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PRESENZA DI BARRIERE ARCHITETTONICHE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlbarrarch" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="0" Text="NON RILEVATO"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COSTO BASE AL MQ
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcostobasemq" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PERCENTUALE ISTAT DI AGGIORNAMENTO - ANNUALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpercistataggann" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CANONE BASE (EQUO CANONE) ANNUALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcanonebase" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CANONE INDICIZZATO ANNUALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcanoneindicizz" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PERCENTUALE DI APPLICAZIONE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpercapllcaz" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            FASCIA DI APPARTENENZA
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtfasciapp" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            AREA DI APPARTENENZA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="AREA_APPARTENENZA" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CANONE APPLICATO ANNUALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="CANONE_APP_ANN" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PERCENTUALE ISTAT DI AGGIORNAMENTO LEGGE 27/2009*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpercistatagglg27" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TIPO DI CANONE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="TIPO_CANONE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CANONE ANNUALE ANTE LEGEM
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcanannante" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CANONE ANNUALE A REGIME*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="CANONE_ANN_REG" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VALORE CONVENZIONALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="VALORE_CONVENZIONALE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COSTO CONVENZIONALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="COSTO_CONVENZIONALE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CARATTERISTICHE DELL'UNITA ABITATIVA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="CARATT_UNITA_AB" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            REDDITO PREVALENTEMENTE DIPENDENTE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="REDD_PREV_DIP" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ABBATTIMENTO CANONE PER CONFRONTO LIBERO MERCATO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlabbatimentocan" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SOVRAPPREZZO PER DECADENZA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlsovraprezzodecadenza" runat="server" Font-Names="Arial"
                                                Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PERCENTUALE AGGIUNTIVA PER AREA DECADENZA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtperaggareadec" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SOVRAPPREZZO PER SOTTOUTILIZZO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlsovraprsottouti" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            REDDITO PREVALENTEMENTE DIPENDENTE MINORE O UGUALE ALLA PENSIONE MINIMA + PENSIONE
                                            SOCIALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="REDD_DIP" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            NUMERO BOX/POSTI AUTO A CONTRATTO SEPARATO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtnumpostiautoconsep" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CANONE BOX/POSTI AUTO A CONTRATTO SEPARATO ANNUALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcanboxconseo" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            CONTABILITA' UNICA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlcontunica" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            GETTITO PREVISTO CONTABILITA' UNICA*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtgettitocontabunic" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            N° PERSONE INVALIDE AL 100% CON INDENNITA' DI ACCOMPAGNAMENTO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="NUM_PERSONE_INV100_CON" runat="server" Text="" Font-Names="Arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            N° PERSONE INVALIDE AL 100% SENZA INDENNITA' DI ACCOMPAGNAMENTO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="NUM_PERSONE_INV100_SENZA" runat="server" Text="" Font-Names="Arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            N° PERSONE INVALIDE AL 67-99%*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="NUM_PERSONE_INV_67_99" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SPESE EFFETTIVAMENTE SOSTENUTE PER PERSONE INVALIDE AL 100% CON INDENNITA' DI ACCOMPAGNAMENTO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="SPESE_PERSONE_INV100_CON" runat="server" Text="" Font-Names="Arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            STATO DI AGGIORNAMENTO DELL'ANAGRAFE DEL NUCLEO FAMILIARE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="STATO_AGG_NUCLEO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DATA CALCOLO ISEE-ERP*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="DATA_CALCOLO_ISEE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISR*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ISR" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISP*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ISP" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PSE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpse" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="60px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISE-ERP*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ISE_ERP" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISEE-ERP*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ISEE_ERP" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISE-ERP ASSEGNATO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ISE_ERP_ASS" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISEE-ERP ASSEGNATO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ISEE_ERP_ASS" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            REDDITO DIPENDENTE O ASSIMILATO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="REDD_DIP_ASS" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ALTRI TIPI DI REDDITO IMPONIBILI
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ALTRI_REDD" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VALORE LOCATIVO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="VALORE_LOCATIVO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VALORE MERCATO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtvalmercato" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            COEFFICIENTE VETUSTA'
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="COEFF_VETUSTA" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            NUMERO COMPONENTI*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="NUM_COMPONENTI" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ANNO DI VETUSTA'*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="ANNO_VETUSTA" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PERCENTUALE VALORE LOCATIVO
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="PERC_VAL_LOCATIVO" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            TABELLA CLASSI ISEE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="TAB_CLASSI_ISEE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            INVALIDITA' SOCIALE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlinvaliditasoc" runat="server" Font-Names="Arial" Font-Size="8">
                                                <asp:ListItem Value="-1" Text="- - -"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="SI"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ISEE PER PRONUNCIA DECADENZA
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtiseprondec" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DATA DISPONIBILITA'
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="DATA_DISPONIBILITA" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            DATA CONTRATTO*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="DATA_ASSEGNAZIONE" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            VALORE PATRIMONIALE
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtvalpatrimoniale" runat="server" Font-Names="Arial" Font-Size="8"
                                                CssClass="CssMaiuscolo" Width="120px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MOROSITA' DELL' ATTUALE FAMIGLIA OCCUPANTE*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MOROSITA_ATTUALE_FAM" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MOROSITA' DELLE PRECEDENTI FAMIGLIE OCCUPANTI*
                                        </td>
                                        <td style="width: 10px;">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="MOROSITA_PREC_FAM" runat="server" Text="" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 40%">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="Immagini/confirm.png" OnClientClick="caricamentoincorso();" />
                        </td>
                        <td style="width: 10%">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <img alt="Uscita" src="Immagini/logout.png" onclick="caricamentoincorso();self.close();" style="cursor: pointer" />
                        </td>
                        <td style="width: 40%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            SALVA
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            ESCI
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idConnessione" runat="server" Value="" />
    <asp:HiddenField ID="sescon" runat="server" Value="" />
    <asp:HiddenField ID="IdAlloggio" runat="server" Value="0" />
    <asp:HiddenField ID="idSiraper" runat="server" Value="-1" />
    <asp:HiddenField ID="idSiraperVersione" runat="server" Value="1" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="SLE" runat="server" Value="0" ClientIDMode="Static" />
    <script language="javascript" type="text/javascript">
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
    </form>
</body>
</html>
