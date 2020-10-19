<%@ Page Title="" Language="VB" MasterPageFile="BasePage.master" AutoEventWireup="false"
    CodeFile="Siraper.aspx.vb" Inherits="SIRAPER_Siraper" %>

<%@ Register Src="Tab_Fabbricato.ascx" TagName="Tab_Fabbricato" TagPrefix="uc1" %>
<%@ Register Src="Tab_Alloggio.ascx" TagName="Tab_Alloggio" TagPrefix="uc2" %>
<%@ Register Src="Tab_Inquilino.ascx" TagName="Tab_Inquilino" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/gestioneDimensioni.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr style="height: 35px; vertical-align: middle">
                        <td>
                            <asp:Button ID="btnIndietro" runat="server" CssClass="bottone" Text="Indietro" ToolTip="Esci"
                                OnClientClick="caricamentoincorso();ConfermaEsci();" CausesValidation="False" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnElabora" runat="server" CssClass="bottone" Text="Elabora" ToolTip="Elabora i dati Siraper"
                                OnClientClick="caricamentoincorso();ConfElaborazione();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnLastSiraper" runat="server" CssClass="bottone" Text="Carica da ultimo Siraper"
                                ToolTip="Carica i dati dall'ultimo Siraper Elaborato" OnClientClick="caricamentoincorso();ConfermaUltimoSiraper();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSalva" runat="server" CssClass="bottone" Text="Salva" ToolTip="Salva i dati"
                                OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnEsporta" runat="server" CssClass="bottone" Text="Esporta Siraper Excel"
                                ToolTip="Crea File Excel Siraper" OnClientClick="caricamentoincorso();return ControlModExport();"
                                Width="125px" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnControllo" runat="server" CssClass="bottone" Text="Controllo Siraper"
                                ToolTip="Avvia il Controllo del Siraper" OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnScaricaControllo" runat="server" CssClass="bottone" Text="Scarica File Controllo"
                                ToolTip="Scalica file controllo" OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnCreaFile" runat="server" CssClass="bottone" Text="Crea File" ToolTip="Crea File Xml Siraper"
                                OnClientClick="caricamentoincorso();" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnEventi" runat="server" CssClass="bottone" Text="Eventi" ToolTip="Elenco Eventi"
                                OnClientClick="Eventi();return false;" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" ToolTip="Esci"
                                OnClientClick="caricamentoincorso();ConfermaEsci();" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td colspan="12">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Sigla Ente Proprietario*
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSiglaEnte" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                        MaxLength="8" Width="125px"></asp:TextBox>
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td>
                                    Tipo Ente Proprietario*
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoEnte" runat="server" Font-Names="Arial" Font-Size="8">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 15px">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td>
                                                Data Riferimento*
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDataRiferimento" runat="server" CssClass="CssMaiuscolo" Font-Names="Arial"
                                                                Font-Size="8pt" MaxLength="10" Width="70px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataRiferimento"
                                                                ErrorMessage="!" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                                Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000" ToolTip="Modificare la data di Fusione"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                Anno Riferimento
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAnnoRif" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                                    MaxLength="4" Width="50px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Codice Fiscale Ente*
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodFiscale" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                        MaxLength="16" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Partita Iva Ente*
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartitaIva" runat="server" Font-Names="Arial" Font-Size="8" CssClass="CssMaiuscolo"
                                        MaxLength="11" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    Ragione Sociale Ente*
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRagioneSociale" runat="server" Font-Names="Arial" Font-Size="8"
                                        CssClass="CssMaiuscolo" Width="300px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="12">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="tabs">
                            <ul>
                                <li><a href="#tabs-0">Fabbricato </a></li>
                                <li><a href="#tabs-1">Alloggio</a></li>
                                <li><a href="#tabs-2">Inquilino</a></li>
                                <li><a href="#tabs-3">Programmazione</a></li>
                            </ul>
                            <div id="tabs-0" style="height: 430px;">
                                <uc1:Tab_Fabbricato ID="Tab_Fabbricato1" runat="server" />
                            </div>
                            <div id="tabs-1" style="height: 430px">
                                <uc2:Tab_Alloggio ID="Tab_Alloggio1" runat="server" />
                            </div>
                            <div id="tabs-2" style="height: 430px">
                                <uc3:Tab_Inquilino ID="Tab_Inquilino1" runat="server" />
                            </div>
                            <div id="tabs-3" style="height: 430px">
                                <div id="divProgrammazione" style="overflow: auto; height: 425px;">
                                    <table>
                                        <tr>
                                            <td colspan="11">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                CODICE ISTAT COMUNE*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCodIstatProg" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="6" Width="75px"></asp:TextBox>
                                            </td>
                                            <td colspan="8">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALLOGGI NUOVI ERP SOCIALE*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallnuovierpsoc" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ALLOGGI NUOVI ERP MODERATO*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallnuovierpmod" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ALLOGGI NUOVI NON ERP
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallnuovinonerp" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALLOGGI DA ACQUISTARE ERP SOCIALE*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallacqerpsoc" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ALLOGGI DA ACQUISTARE ERP MODERATO*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallacqerpmod" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ALLOGGI DA ACQUISTARE NON ERP:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallacqnonerp" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ALLOGGI DA RECUPERARE/RISTRUTTURARE ERP SOCIALE*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallrsterpsoc" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ALLOGGI DA RECUPERARE/RISTRUTTURARE ERP SOCIALE*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallrsterpmod" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                ALLOGGI DA RECUPERARE/RISTRUTTURARE NON ERP PROGRAMMATI*:
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtallrsterpnonerp" runat="server" Font-Names="Arial" Font-Size="8"
                                                    CssClass="CssMaiuscolo" MaxLength="4" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="height: 150px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                N.B. Durante la creazione del File Xml i campi del Tab Programmazione se Nulli saranno
                                                settati con i parametri di Default.
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
            <asp:HiddenField ID="idSiraper" runat="server" Value="-1" />
            <asp:HiddenField ID="idSiraperVersione" runat="server" Value="1" />
            <asp:HiddenField ID="idConnessione" runat="server" Value="" />
            <asp:HiddenField ID="Elaborazione" runat="server" Value="0" />
            <asp:HiddenField ID="ConfermaElaborazione" runat="server" Value="0" />
            <asp:HiddenField ID="tabSelect" runat="server" Value="" />
            <asp:HiddenField ID="SLE" runat="server" Value="0" />
            <asp:HiddenField ID="FileCreato" runat="server" Value="0" />
            <asp:HiddenField ID="Controllo" runat="server" Value="1" />
            <asp:HiddenField ID="noClose" runat="server" Value="1" ClientIDMode="Static" />
            <asp:HiddenField ID="closeWait" runat="server" Value="1" ClientIDMode="Static" />
            <asp:HiddenField ID="HiddenConf" runat="server" Value="0" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        initialize();
        function initialize() {
            var pos = 0;
            var indice = document.getElementById("MasterPage_MainContent_tabSelect").value;
            if (indice != '') {
                pos = indice;
            };
            $('#tabs').tabs({
                select: function (event, ui) {
                    var tabNumber = 0;
                    if (ui.tab.hash.indexOf('tabs') != -1) {
                        tabNumber = ui.index;
                        document.getElementById("MasterPage_MainContent_tabSelect").value = tabNumber;
                    };
                }
            });
            $('#tabs').tabs({ selected: pos });
            $("#MasterPage_MainContent_txtDataRiferimento").datepicker({ dateFormat: 'dd/mm/yy', showAnim: 'slide' });
            if (document.getElementById('MasterPage_MainContent_idSiraper').value != -1 && document.getElementById('MasterPage_MainContent_Elaborazione').value == 1) {
                document.getElementById("tabs").style.visibility = 'visible';
            }
            else {
                document.getElementById("tabs").style.visibility = 'hidden';
            };
            var larghezzaPaginaIntera = $(window).width();
            var larghezzatabs = larghezzaPaginaIntera - 20;
            var larghezzaDiv = larghezzaPaginaIntera - 72;
            $("#tabs").width(larghezzatabs);
            $("#divFabbricato").width(larghezzaDiv);
            $("#divAlloggio").width(larghezzaDiv);
            $("#divInquilino").width(larghezzaDiv);
            $("#divProgrammazione").width(larghezzaDiv);
            window.focus();
            $(document).ready(function () {
                $('.pager a').click(function () { caricamentoincorso(); });
            });
        };
        function ConfermaUltimoSiraper() {
            var conferma = window.confirm('Tutti i dati inseriti sino a questo momento andranno persi!\nSei sicuro di voler continuare?');
            if (conferma) {
                document.getElementById('HiddenConf').value = 1;
            };
        };
    </script>
</asp:Content>
