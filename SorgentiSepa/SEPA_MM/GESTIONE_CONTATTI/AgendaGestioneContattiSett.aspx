<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false" CodeFile="AgendaGestioneContattiSett.aspx.vb" Inherits="GESTIONE_CONTATTI_AgendaGestioneContattiSett" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label Text="Agenda" runat="server" ID="lblTitolo" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="height: 35px; text-align: center;">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">AGENDA </span></strong>
            </td>
        </tr>
        <tr>
            <td style="height: 35px;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 36%">
                            <asp:Label ID="lblSlotLiberi" Text="Slot liberi" runat="server" Font-Bold="True" />
                            <br />
                            <asp:Label ID="lblFilialeCompetenza" runat="server" Width="100%" />
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="lblStruttura" Text="Sede territoriale" runat="server" Style="text-align: left" Font-Bold="True" Width="80%" />
                            <br />
                            <asp:DropDownList ID="cmbStruttura" runat="server" Width="95%" Style="text-align: left" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 8%; text-align: right;">
                            <asp:ImageButton ID="btnIndietro" runat="server" ToolTip="Vai al Mese Precedente" ImageUrl="Immagini/navigate-left-icon.png" />
                        </td>
                        <td style="width: 20%; text-align: center;">
                            <strong><span style="font-size: 14pt; color: #000000; font-family: Arial; font-weight: bold; font-style: italic; text-decoration: underline;">
                                <asp:Label ID="lblMese" runat="server" Text=""></asp:Label></span></strong>
                        </td>
                        <td style="width: 8%; text-align: left;">
                            <asp:ImageButton ID="btnAvanti" runat="server" ToolTip="Vai al Prossimo Mese" ImageUrl="Immagini/navigate-right-icon.png" />
                        </td>
                        <td style="width: 8%; text-align: right;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 13%; vertical-align: top;">
                            <div style="width: 100%; height: 545px;">
                                <table style="width: 100%; height: 97%;">
                                    <tr style="height: 33%">
                                        <td style="width: 97%; vertical-align: middle; text-align: center;">
                                            <asp:Calendar ID="CalendarioPrec" runat="server" CssClass="pager" ShowNextPrevMonth="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="150px" Width="200px" ToolTip="Calendario Mese Precedente">
                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#808080" />
                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                <SelectorStyle BackColor="#CCCCCC" />
                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                <WeekendDayStyle BackColor="#FFFFCC" />
                                            </asp:Calendar>
                                        </td>
                                    </tr>
                                    <tr style="height: 34%">
                                        <td style="width: 97%; vertical-align: middle; text-align: center;">
                                            <asp:Calendar ID="CalendarioMese" runat="server" CssClass="pager" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="150px" Width="200px" ToolTip="Calendario Mese in Corso">
                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#808080" />
                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                <SelectorStyle BackColor="#CCCCCC" />
                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <WeekendDayStyle BackColor="#FFFFCC" />
                                            </asp:Calendar>
                                        </td>
                                    </tr>
                                    <tr style="height: 33%">
                                        <td style="width: 97%; vertical-align: middle; text-align: center;">
                                            <asp:Calendar ID="CalendarioNext" runat="server" CssClass="pager" ShowNextPrevMonth="False" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="150px" Width="200px" ToolTip="Calendario Mese Successivo">
                                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                <NextPrevStyle VerticalAlign="Bottom" />
                                                <OtherMonthDayStyle ForeColor="#808080" />
                                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                <SelectorStyle BackColor="#CCCCCC" />
                                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                <WeekendDayStyle BackColor="#FFFFCC" />
                                            </asp:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width: 87%; vertical-align: top; text-align: left">
                            <div style="width: 100%; vertical-align: top; text-align: left">
                                <table style="width: 100%; height: 97%">
                                    <tr>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Luned&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Marted&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Mercoled&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Gioved&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Venerd&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Sabato
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold; text-align: center">
                                            Domenica
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata1" runat="server" style="width: 15%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData1" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio1" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata2" runat="server" style="width: 14%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData2" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio2" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata3" runat="server" style="width: 15%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData3" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio3" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata4" runat="server" style="width: 14%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData4" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio4" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata5" runat="server" style="width: 15%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData5" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio5" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata6" runat="server" style="width: 14%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData6" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio6" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari6" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata7" runat="server" style="width: 13%; border: 1px solid #000000; vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData7" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio7" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="LabelOrari7" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label7" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
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
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; text-align: left;">
                            <strong><span style="font-size: 8pt; color: #801f1c; font-family: Arial; font-weight: bold;">
                                <asp:Label ID="lblData" runat="server" Text=""></asp:Label></span></strong>
                        </td>
                        <td style="width: 80%; text-align: right;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%; text-align: left;">
                                        <asp:Label Text="" runat="server" ID="lblIndirizziFiliali" Width="100%" />
                                    </td>
                                    <td>
                                        Legenda:
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right;">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="border: medium solid #808080; background-color: #FFFFCC; width: 15px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 10px">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left">
                                                    Data Odierna
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="GiornoSelezionato" runat="server" Value="1" ClientIDMode="Static" />
    <asp:HiddenField ID="MeseSelezionato" runat="server" Value="1" ClientIDMode="Static" />
    <asp:HiddenField ID="AnnoSelezionato" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="idSegnalazione" runat="server" Value="-1" />
    <asp:HiddenField ID="provenienza" runat="server" Value="-1" />
    <asp:HiddenField ID="idStrutturaPredefinita" runat="server" Value="-1" />
    <asp:Button ID="btnAggiorna" runat="server" Style="display: none" />
    <script type="text/javascript">
        initialize();
        function initialize() {
            window.focus;
        };
        $(document).ready(function () {
            $('.pager a').click(function () { //caricamentoincorso();
            });
        });
        function EnterInvio(e) {
            sKeyPressed1 = e.which;
             {
                if (sKeyPressed1 == 37) { //INDIETRO
                    document.getElementById("btnIndietro").click();
                    return false;
                } else if (sKeyPressed1 == 39) { //AVANTI
                    document.getElementById("btnAvanti").click();
                    return false;
                };
            
        };
        function $keyPress() {
            
                if (event.keyCode == 37) { //INDIETRO
                    document.getElementById("btnIndietro").click();
                    return false;
                } else if (event.keyCode == 39) { //AVANTI
                    document.getElementById("btnAvanti").click();
                    return false;
                };
            
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $keyPress;
        }
        else {
            window.document.addEventListener("keydown", EnterInvio, true);
        };
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="btnGiorno" runat="server" CssClass="bottone" Text="Visualizzazione Giornaliera" ToolTip="Visualizzazione Giornaliera" />
            </td>
            <td>
                <asp:Button ID="btnOggi" runat="server" CssClass="bottone" Text="Settimana Corrente" ToolTip="Vai nella settimana in Corso" />
            </td>
            <td>
                <asp:Button ID="btnMese" runat="server" CssClass="bottone" Text="Visualizzazione Mensile" ToolTip="Visualizzazione Mensile" />
            </td>
            <td>
                <asp:Button ID="ButtonExportAgendaAppuntamenti" runat="server" CssClass="bottone" Text="Export appuntamenti" ToolTip="Esporta l'intera agenda appuntamenti" />
            </td>
            <td>
                <asp:Button ID="btnIndietroSegnalazione" runat="server" CssClass="bottone" Text="Torna alla segnalazione" ToolTip="Torna alla segnalazione" />
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        validNavigation = false;
    </script>
</asp:Content>
