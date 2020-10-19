<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Dichiara.ascx.vb" Inherits="VSA_NuovaDomandaVSA_Tab_Dichiara" %>
<link rel="stylesheet" type="text/css" href="Styles/impromptu.css" />
<link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
<table width="97%" style="border: 1px solid #0066FF; vertical-align: top;">
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 13%">
                        <asp:Label ID="Label2" Style="width: 190px;" Font-Size="9pt" Font-Names="Arial" runat="server">Tipologia Richiesta</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:DropDownList ID="cmbTipoRichiesta" Style="width: 417px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="1" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 13%">
                        <asp:Label ID="Label16" Style="width: 230px;" Font-Size="9pt" Font-Names="Arial" runat="server">Motivo di presentazione della Richiesta</asp:Label>
                        <asp:Label ID="lblCodContrattoScambio" Style="width: 230px;" Font-Size="9pt" Font-Names="Arial" runat="server" Visible="False">Codice Contratto oggetto dello scambio</asp:Label>
                    </td>
                    <td style="width: 40%">
                        <asp:DropDownList ID="cmbPresentaD" Style="width: 417px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="2">
                        </asp:DropDownList>
                        <asp:HyperLink ID="CEM" runat="server" Style="top: 33px; left: 202px;" Font-Bold="True" Font-Names="arial" Font-Size="9pt" Visible="False">Clicca qui per visualizzare/inserire le motivazioni della domanda</asp:HyperLink>
                        <asp:TextBox ID="txtCodContrattoScambio" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="19" Style="width: 158px;" TabIndex="3" Visible="False"></asp:TextBox>
                        <img id="imgDatiContratto" style="top: 34px; left: 364px; cursor: pointer" alt="Visualizza Dati Contratto" onclick="ApriContratto();" src="../../Condomini/Immagini/Search_16x16.png" />
                        <asp:Button ID="imgCercaContratto" runat="server" CssClass="bottone" Text="Seleziona" OnClientClick="apriRicerca();" ToolTip="Cerca codice contratto" Width="80px" style="visibility: hidden;" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 35%">
                        <asp:Label ID="lblDataDomanda" Style="width: 190px;" Font-Size="9pt" Font-Names="Arial" runat="server">Data presentazione Richiesta</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtDataPrRichiesta" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="3" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="lblDataEvento" Style="width: 100px;" Font-Size="9pt" Font-Names="Arial" runat="server">Data Evento</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="txtDataEvento" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="3" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="lblInizioVal" Style="width: 100px;" Font-Size="9pt" Font-Names="Arial" runat="server">Inizio Validità</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="txtInizioVal" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="3" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="lblFineVal" Style="width: 100px;" Font-Size="9pt" Font-Names="Arial" runat="server">Fine Validità</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="txtFineVal" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="3" Width="72px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblModPres" runat="server" Font-Names="Arial" Font-Size="9pt" Style="width: 417px;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDurataProc" runat="server" Font-Names="Arial" Font-Size="9pt" Style="width: 417px;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 40%">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="9pt" Style="width: 850px;">Il nucleo familiare richiedente ha effettuato gli adempimenti connessi all&#39;anagrafe utenza?</asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="cmbFattaAU" Style="width: 58px;" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="4">
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <asp:Label ID="lblCointest" runat="server" Font-Names="Arial" Font-Size="9pt" Style="width: 517px;" ForeColor="Red" Visible="False">E' presente una morosità!! Procedere alla cointestazione di uscente
        e subentrante?
                        </asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="cmbSiNo" Style="width: 58px;" ForeColor="Red" Font-Size="9pt" Font-Names="Arial" CssClass="CssPresenta" Font-Bold="False" runat="server" TabIndex="4" Visible="False">
                            <asp:ListItem Value="1">SI</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Width="290px">ESTREMI DOCUMENTO DI RICONOSCIMENTO</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td style="width: 13%">
                        <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="9pt" Width="135px">Tipo Documento</asp:Label>
                    </td>
                    <td style="width: 87%;" colspan="7">
                        <asp:DropDownList ID="cmbTipoDocumento" runat="server" CssClass="CssProv" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" TabIndex="110" Width="126px">
                            <asp:ListItem Selected="True" Value="0">CARTA IDENTITA</asp:ListItem>
                            <asp:ListItem Value="1">PASSAPORTO</asp:ListItem>
                            <asp:ListItem Value="2">PATENTE DI GUIDA</asp:ListItem>
                            <asp:ListItem Value="3">TESSERE MINISTERIALI</asp:ListItem>
                            <asp:ListItem Value="-1">--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 13%">
                        <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="9pt" Width="135px">Doc. Identita N.</asp:Label>
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtCINum" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="25" TabIndex="5" Width="120px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="9pt" Width="36px">Data</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtCIData" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="6" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 11%">
                        <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="9pt" Width="80px">Rilasciata Da</asp:Label>
                    </td>
                    <td style="width: 44%" colspan="3">
                        <asp:TextBox ID="txtCIRilascio" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="50" Style="width: 181px;" TabIndex="7"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 13%">
                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="9pt" Width="135px">Permesso Soggiorno N.</asp:Label>
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtPSNum" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="25" TabIndex="8" Width="120px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="9pt" Width="36px">Data</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtPSData" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="9" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 12%">
                        <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="9pt" Width="57px">Scadenza</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtPSScade" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="10" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="9pt" Width="81px">Rinnovo</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 33%">
                        <asp:TextBox ID="txtPSRinnovo" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="11" Width="72px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 13%">
                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="9pt" Width="135px">Carta Soggiorno N.</asp:Label>
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtCSNum" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="25" TabIndex="12" Width="120px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 13%">
                        <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="9pt" Width="36px">Data</asp:Label>
                        &nbsp;
                    </td>
                    <td style="width: 13%">
                        <asp:TextBox ID="txtCSData" runat="server" Columns="7" Font-Names="Arial" Font-Size="9pt" MaxLength="10" TabIndex="13" Width="72px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 10%">
                        &nbsp;
                    </td>
                    <td style="width: 43%" colspan="3">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 700px">
            <asp:Image ID="imgAlert" runat="server" ImageUrl="../../IMG/Alert.gif" Style="z-index: 125; left: 10px; top: 294px;" />
            &nbsp;
            <asp:Label ID="lblDocumRicon" runat="server" Font-Names="arial" Font-Size="9pt" Font-Bold="True" Style="width: 580px;">Verificare la correttezza degli estremi del documento di riconoscimento!</asp:Label>
        </td>
    </tr>
</table>
<%--<asp:CheckBox ID="chkAler" runat="server" Style="z-index: 124; left: 393px; position: absolute;
        top: 32px; width: 199px;" Font-Size="9pt" ForeColor="#0000C0" Text="Trattasi di contratto ALER"
        Visible="False" />--%>
<asp:HiddenField ID="cu" runat="server" />
<asp:HiddenField ID="DOMANDA" runat="server" />
<asp:HiddenField ID="DICHIARAZIONE" runat="server" />
<script language="javascript" type="text/javascript">

    // Funzione javascript per l'inserimento in automatico degli slash nella data
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


    function apriRicerca() {

        var win = null;
        LeftPosition = (screen.width) ? (screen.width - 620) / 2 : 0;
        TopPosition = (screen.height) ? (screen.height - 500) / 2 : 0;
        LeftPosition = LeftPosition - 20;
        TopPosition = TopPosition - 20;
        window.open('../../RicercaUI.aspx', 'RicercaUI', 'height=450,top=0,left=0,width=670,scrollbars=no');
    }

</script>
