<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Agenda.aspx.vb" Inherits="SEGNALAZIONI_Agenda_Agenda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Agenda Appuntamenti</title>
    <script src="js/jsfunzioni.js" type="text/javascript"></script>
    <script src="js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="Style/Site.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="height: 35px; text-align: center;">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">AGENDA SEGNALAZIONI
                </span></strong>
            </td>
        </tr>
        <tr>
            <td style="height: 35px;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 8%">
                            <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td style="width:100%">
                                        <asp:Button ID="btnOggi" runat="server" CssClass="bottone" Text="Mese Corrente" ToolTip="Vai nel Mese in Corso"
                                            OnClientClick="caricamentoincorso();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="ButtonExportAgendaAppuntamenti" runat="server" CssClass="bottone"
                                            Text="Export appuntamenti" ToolTip="Esporta l'intera agenda appuntamenti"
                                            OnClientClick="caricamentoincorso();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5%">
                            <asp:Label Text="Slot liberi" runat="server" />
                        </td>
                        <td style="width: 28%">
                            <asp:Label ID="lblFilialeCompetenza" runat="server" />
                        </td>
                        <td style="width: 5%">
                            <asp:Label ID="lblStruttura" Text="Sede territoriale" runat="server" Style="text-align: right"
                                Width="80%" />
                        </td>
                        <td style="width: 10%">
                            <asp:DropDownList ID="cmbStruttura" runat="server" Width="95%" Style="text-align: left"
                                AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" OnChange="caricamentoincorso();">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 8%; text-align: right;">
                            <asp:ImageButton ID="btnIndietro" runat="server" ToolTip="Vai al Mese Precedente"
                                OnClientClick="caricamentoincorso();" ImageUrl="Immagini/navigate-left-icon.png" />
                        </td>
                        <td style="width: 20%; text-align: center;">
                            <strong><span style="font-size: 14pt; color: #000000; font-family: Arial; font-weight: bold;
                                font-style: italic; text-decoration: underline;">
                                <asp:Label ID="lblMese" runat="server" Text=""></asp:Label></span></strong>
                        </td>
                        <td style="width: 8%; text-align: left;">
                            <asp:ImageButton ID="btnAvanti" runat="server" ToolTip="Vai al Prossimo Mese" OnClientClick="caricamentoincorso();"
                                ImageUrl="Immagini/navigate-right-icon.png" />
                        </td>
                        <td style="width: 8%; text-align: right;">
                            <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" ToolTip="Esci dall'Agenda"
                                OnClientClick="caricamentoincorso();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 20%; vertical-align: top;">
                            <div style="width: 100%; height: 545px;">
                                <table style="width: 100%; height: 97%;">
                                    <tr style="height: 33%">
                                        <td style="width: 97%; vertical-align: middle; text-align: center;">
                                            <center>
                                                <asp:Calendar ID="CalendarioPrec" runat="server" CssClass="pager" ShowNextPrevMonth="False"
                                                    BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest"
                                                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="150px" Width="200px"
                                                    ToolTip="Calendario Mese Precedente">
                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                    <OtherMonthDayStyle ForeColor="#808080" />
                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                                </asp:Calendar>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr style="height: 34%">
                                        <td style="width: 97%; vertical-align: middle; text-align: center;">
                                            <center>
                                                <asp:Calendar ID="CalendarioMese" runat="server" CssClass="pager" BackColor="White"
                                                    BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana"
                                                    Font-Size="8pt" ForeColor="Black" Height="150px" Width="200px" ToolTip="Calendario Mese in Corso">
                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                    <OtherMonthDayStyle ForeColor="#808080" />
                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                                </asp:Calendar>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr style="height: 33%">
                                        <td style="width: 97%; vertical-align: middle; text-align: center;">
                                            <center>
                                                <asp:Calendar ID="CalendarioNext" runat="server" CssClass="pager" ShowNextPrevMonth="False"
                                                    BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest"
                                                    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="150px" Width="200px"
                                                    ToolTip="Calendario Mese Successivo">
                                                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                                    <NextPrevStyle VerticalAlign="Bottom" />
                                                    <OtherMonthDayStyle ForeColor="#808080" />
                                                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                                    <SelectorStyle BackColor="#CCCCCC" />
                                                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                                    <WeekendDayStyle BackColor="#FFFFCC" />
                                                </asp:Calendar>
                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td style="width: 80%; vertical-align: top; top: +3px; position: relative;">
                            <div style="width: 100%; height: 537px; vertical-align: top;">
                                <table style="width: 100%; height: 97%">
                                    <tr>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Luned&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Marted&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Mercoled&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Gioved&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Venerd&#236;
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Sabato
                                        </td>
                                        <td style="border: 1px solid #000000; font-family: Arial; font-size: 8pt; font-weight: bold;
                                            text-align: center">
                                            Domenica
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata1" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata2" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata3" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata4" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label4" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata5" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata6" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata7" runat="server" style="width: 13%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
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
                                                        <asp:Label ID="Label7" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata8" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData8" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio8" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label8" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata9" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData9" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio9" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label9" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata10" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData10" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio10" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label10" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata11" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData11" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio11" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata12" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData12" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio12" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label12" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata13" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData13" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio13" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label13" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata14" runat="server" style="width: 13%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData14" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio14" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label14" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata15" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData15" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio15" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label15" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata16" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData16" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio16" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label16" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata17" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData17" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio17" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label17" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata18" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData18" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio18" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label18" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata19" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData19" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio19" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label19" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata20" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData20" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio20" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label20" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata21" runat="server" style="width: 13%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData21" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio21" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label21" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata22" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData22" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio22" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label22" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata23" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData23" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio23" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label23" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata24" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData24" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio24" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label24" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata25" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData25" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio25" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label25" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata26" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData26" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio26" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label26" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata27" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData27" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio27" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label27" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata28" runat="server" style="width: 13%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData28" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio28" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label28" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata29" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData29" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio29" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label29" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata30" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData30" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio30" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label30" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata31" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData31" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio31" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label31" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata32" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData32" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio32" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label32" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata33" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData33" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio33" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label33" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata34" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData34" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio34" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label34" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata35" runat="server" style="width: 13%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData35" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio35" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label35" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 16%">
                                        <td id="tdbloccodata36" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData36" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio36" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label36" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata37" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData37" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio37" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label37" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata38" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData38" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio38" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label38" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata39" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData39" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio39" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label39" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata40" runat="server" style="width: 15%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData40" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio40" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label40" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata41" runat="server" style="width: 14%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData41" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio41" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label41" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdbloccodata42" runat="server" style="width: 13%; border: 1px solid #000000;
                                            vertical-align: top;" onclick="InserisciAppuntamento(this);">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="99%">
                                                            <tr>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="BloccoData42" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td style="width: 50%" class="style1">
                                                                    <asp:Label ID="Dettaglio42" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="Label42" runat="server"></asp:Label>
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
                        <td style="width: 20%; text-align: center; position: relative; top: -5px;">
                            <strong><span style="font-size: 8pt; color: #801f1c; font-family: Arial; font-weight: bold;">
                                <asp:Label ID="lblData" runat="server" Text=""></asp:Label></span></strong>
                        </td>
                        <td style="width: 80%; position: relative; top: -7px; text-align: right;">
                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 50%;text-align: left;">
                                        <asp:Label Text="" runat="server" ID="lblIndirizziFiliali" Width="100%" />
                                    </td>
                                    <td>
                                        Legenda:
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="border: medium solid #808080; background-color: #FFFFCC; width: 15px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: left">
                                        Data Odierna
                                    </td>
                                    <td style="width: 10px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="GiornoSelezionato" runat="server" Value="1" />
    <asp:HiddenField ID="MeseSelezionato" runat="server" Value="1" />
    <asp:HiddenField ID="AnnoSelezionato" runat="server" Value="0" />
    <asp:Button ID="btnAggiorna" runat="server" Style="display: none" OnClientClick="caricamentoincorso();" />
    </form>
    <script type="text/javascript">
        initialize();
        function initialize() {
            document.getElementById('caricamento').style.visibility = 'hidden';
            window.focus;
        };
        $(document).ready(function () {
            $('.pager a').click(function () { caricamentoincorso(); });
        });
        function EnterInvio(e) {
            sKeyPressed1 = e.which;
            if (document.getElementById('caricamento').style.visibility == 'hidden') {
                if (sKeyPressed1 == 37) { //INDIETRO
                    document.getElementById("btnIndietro").click();
                    return false;
                } else if (sKeyPressed1 == 39) { //AVANTI
                    document.getElementById("btnAvanti").click();
                    return false;
                };
            };
        };
        function $keyPress() {
            if (document.getElementById('caricamento').style.visibility == 'hidden') {
                if (event.keyCode == 37) { //INDIETRO
                    document.getElementById("btnIndietro").click();
                    return false;
                } else if (event.keyCode == 39) { //AVANTI
                    document.getElementById("btnAvanti").click();
                    return false;
                };
            };
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $keyPress;
        }
        else {
            window.document.addEventListener("keydown", EnterInvio, true);
        };
    </script>
</body>
</html>
