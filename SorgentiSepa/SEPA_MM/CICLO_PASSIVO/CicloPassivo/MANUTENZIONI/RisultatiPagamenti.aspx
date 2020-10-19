<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiPagamenti.aspx.vb"
    Inherits="MANUTENAZIONI_RisultatiPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<head id="Head1" runat="server">
    <title>RISULTATI RICERCA</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script type="text/javascript">
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

    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <asp:Panel ID="panelContenuto" runat="server" Style="width: 100%">
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">Ordini - Manutenzioni e servizi - SAL - Stampa pagamenti
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnStampaPagamento" runat="server" Text="Stampa Pagamento"
                                        ToolTip="Stampa Pagamento" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnNuovaRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%">
                            <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                PageSize="100" IsExporting="False">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                    CommandItemDisplay="Top">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="true" />
                                    <CommandItemTemplate>
                                        <div style="display: inline-block; width: 100%;">
                                            <div style="float: right; padding: 4px;">
                                                <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                    CssClass="rgRefresh" OnClientClick="nascondi=0;" />
                                                <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                    CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                            </div>
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="PROG/ANNO" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="SAL_ANNO" HeaderText="SAL/ANNO" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterControlWidth="85%">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_PRENOTAZIONE" HeaderText="PRENOTAZIONE"
                                            DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="15%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE" DataFormatString="{0:dd/MM/yyyy}"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="15%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="BENEFICIARIO" HeaderText="BENEFICIARIO" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterControlWidth="85%">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="25%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="IMPORTO CONSUNTIVATO"
                                            AutoPostBackOnFilter="true" DataFormatString="{0:C2}" FilterControlWidth="85%">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="12%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO PRENOTATO"
                                            Visible="False">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO"
                                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}"
                                            FilterControlWidth="85%">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="15%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains" DataFormatString="{0:@}" FilterControlWidth="85%">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="30%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO PAGAMENTO" Visible="False">
                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                    <Excel FileExtension="xls" Format="Xlsx" />
                                </ExportSettings>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                                    ClientEvents-OnCommand="onCommand">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                    <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                            CssClass="txtMia" Style="visibility: hidden" Font-Names="Arial" Font-Size="12pt"
                            MaxLength="100" Width="768px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                            Style="visibility: hidden" MaxLength="100" Width="152px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <telerik:RadWindow ID="RadWindowVociServizi" runat="server" CenterIfModal="true"
            Modal="true" Width="650px" Height="400px" VisibleStatusbar="false" Title="Aggiungi Voci Servizi"
            Behaviors="Pin, Maximize, Move, Resize">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <table border="0" cellpadding="4" cellspacing="4" width="90%">

                        <tr>
                            <td colspan="2" class="TitoloModulo">
                                <asp:Label ID="ADP" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadButton ID="ImgConferma" runat="server" Text="Stampa" ToolTip="Stampa" />
                            </td>
                            <td>
                                <telerik:RadButton ID="ImgAnnulla" runat="server" Text="Annulla Stampa" ToolTip="Annulla Stampa" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblEsercizioFinanziario" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    ForeColor="Blue" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%">
                                <asp:Label Text="Data di emissione*" runat="server" Font-Names="Arial" Font-Size="9pt" />
                            </td>
                            <td style="width: 80%">
                                <asp:TextBox ID="DataEmissione" runat="server" Width="70px" MaxLength="10" Font-Names="Arial"
                                    Font-Size="9pt"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DataEmissione"
                                    ErrorMessage="!" Font-Names="arial" Font-Size="12pt" ForeColor="#CC0000"
                                    ToolTip="Modificare la data" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" Text="Modalità di pagamento" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtModalitaPagamento" runat="server" Width="300px" Font-Names="Arial"
                                    Font-Size="9pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" Text="Condizione di pagamento" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtCondizionePagamento" runat="server" Width="300px" Font-Names="Arial"
                                    Font-Size="9pt" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" Text="Data di scadenza" runat="server" Font-Names="Arial"
                                    Font-Size="9pt" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDataScadenza" runat="server" Width="70px" MaxLength="10" Font-Names="Arial"
                                    Font-Size="9pt"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataScadenza"
                                    ErrorMessage="!" Font-Names="arial" Font-Size="12pt" ForeColor="#CC0000"
                                    ToolTip="Modificare la data" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" Text="Descrizione" runat="server" Font-Names="Arial" Font-Size="9pt" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescrizioneBreve" runat="server" Width="500px" MaxLength="1000"
                                    Font-Names="Arial" Font-Size="9pt"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField runat="server" ID="idCondizione" Value="NULL" />


        <asp:HiddenField runat="server" ID="idModalita" Value="NULL" />
        <asp:HiddenField ID="HiddenFieldRielabPagam" runat="server" Value="" />
        <asp:HiddenField ID="TipoAllegato" runat="server" Value="" />
        <script type="text/javascript" language="javascript">
            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);
        </script>
    </form>
</body>
</html>
