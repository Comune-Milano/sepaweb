<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiSegnChiusura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_RisultatiSegnChiusura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Segnalazioni</title>
</head>
<body>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGridSegnalaz">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGridSegnalaz" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                    <td class="TitoloModulo">Elenco Segnalazioni
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnChiudi" runat="server" Text="Chiudi" ToolTip="Chiudi le segnalazioni selezionate" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="RadNuovaRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca"
                                        Style="top: 0px; left: 0px" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="DataGridSegnalaz" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PagerStyle-Visible="true" PagerStyle-AlwaysVisible="false" AllowPaging="false"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top" Width="200%">
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
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False" Exportable="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" Exportable="false">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" Width="2%" />
                                        <HeaderTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                <telerik:RadButton ID="btnSeleziona" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" OnClientClicking="function(sender,args){nascondi=0;}"
                                                    AutoPostBack="true" OnClientCheckedChanged="selezionaTutti" OnClick="btnSeleziona_Click" />
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="width: 100%; text-align: center;">
                                                <telerik:RadButton ID="cboggetto" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                    AutoPostBack="false" Checked='<%# DataBinder.Eval(Container, "DataItem.SELEZIONE") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="NUM" HeaderText="NUMERO" HeaderStyle-Width="3%" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                        FilterControlWidth="95%" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" HeaderStyle-Width="5%" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                        FilterControlWidth="95%" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_INT" HeaderText="TIPO INT." ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="3%"
                                        HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" FilterControlWidth="95%" Exportable="true"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-Width="5%">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" FilterControlWidth="95%"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" FilterControlWidth="95%"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-Width="5%">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" FilterControlWidth="95%"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FILIALE" HeaderText="STRUTTURA ASSOCIATA" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" HeaderStyle-Width="6%">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOTE_C" HeaderText="NOTE DI CHIUSURA" ItemStyle-HorizontalAlign="Left" Exportable="true"
                                        HeaderStyle-HorizontalAlign="Center" AllowFiltering="false">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_PERICOLO_sEGNALAZIONE" HeaderText="ID_PERICOLO_sEGNALAZIONE" Exportable="true"
                                        Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_sTATO" HeaderText="ID_sTATO" Visible="false" Exportable="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" Visible="False" Exportable="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MANAGER" HeaderText="B. MANAGER" AutoPostBackOnFilter="true" Exportable="true"
                                        FilterControlWidth="95%" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                        <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                            HorizontalAlign="Center" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True"></PagerStyle>
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
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" Text="Descrizione nota chiusura" runat="server" Font-Bold="True"
                            Font-Names="Arial" Font-Size="9pt" />
                        <asp:TextBox ID="TextBox1" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Visible="False" Style="position: absolute; top: 485px; left: 10px;"
                            Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                            ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="hiddenSelTutti" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField ID="tipo" runat="server" />
        <asp:HiddenField ID="TipoSegnalazione" runat="server" />
        <asp:HiddenField ID="tipo1" runat="server" />
        <asp:HiddenField ID="dal" runat="server" />
        <asp:HiddenField ID="oreda" runat="server" />
        <asp:HiddenField ID="minda" runat="server" />
        <asp:HiddenField ID="al" runat="server" />
        <asp:HiddenField ID="orea" runat="server" />
        <asp:HiddenField ID="mina" runat="server" />
        <asp:HiddenField ID="filiale" runat="server" />
        <asp:HiddenField ID="edificio" runat="server" />
        <asp:HiddenField ID="segnalante" runat="server" />
        <asp:HiddenField ID="LBLID" runat="server" />
        <asp:HiddenField ID="identificativo" runat="server" />
        <asp:HiddenField ID="origine" runat="server" />
        <asp:HiddenField ID="stato" runat="server" />
        <asp:HiddenField ID="urgenza" runat="server" Value="-1" />
        <asp:HiddenField ID="numero" runat="server" Value="-1" />
        <asp:HiddenField ID="IDSTATO" runat="server" Value="-1" />
        <asp:HiddenField ID="idBm" runat="server" Value="-1" />
        <asp:HiddenField ID="idComplesso" runat="server" Value="-1" />
        <asp:HiddenField ID="cat0" runat="server" Value="-1" />
        <asp:HiddenField ID="cat1" runat="server" Value="-1" />
        <asp:HiddenField ID="cat2" runat="server" Value="-1" />
        <asp:HiddenField ID="cat3" runat="server" Value="-1" />
        <asp:HiddenField ID="cat4" runat="server" Value="-1" />
        <asp:HiddenField ID="fornitore" runat="server" Value="-1" />
        <asp:HiddenField ID="complesso" runat="server" Value="-1" />
        <asp:HiddenField runat="server" ID="prov" Value="" />
        <asp:HiddenField runat="server" ID="Selected" Value="0" />
        <script type="text/javascript">
            var selezionato;
            function ConfermaEsci() {


                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    document.location.href = 'pagina_home.aspx';
                }

            }

            function Apri() {


                if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                    document.getElementById('btnChiudi').click();
                    //                today = new Date();
                    //                var Titolo = 'Segnalazione' + today.getMinutes() + today.getSeconds();

                    //                //                    popupWindow = window.open('Segnalazione.aspx?T=' + document.getElementById('tipo').value + '&IDE=' + document.getElementById('identificativo').value + '&ID=' + document.getElementById('LBLID').value, Titolo, 'height=700,width=900');
                    //                //                    popupWindow.focus();

                    //                if (document.getElementById('prov').value == 'S') {
                    //                    window.open('Segnalazioni.aspx?IDS=' + document.getElementById('LBLID').value + '', 'Segnalazione', 'height=700px,width=900px,resizable=yes');
                    //                } else {
                    //                    window.open('Segnalazioni.aspx?IDS=' + document.getElementById('LBLID').value + '', 'Segnalazione', 'height=700px,width=900px,resizable=yes');
                    //                };

                }
                else {
                    alert('Nessuna Segnalazione Selezionata!');
                }

            }

            function NuovaSegnalazione() {

                today = new Date();
                var Titolo = 'Segnalazione' + today.getMinutes() + today.getSeconds();

                popupWindow = window.open('Segnalazione.aspx?T=' + document.getElementById('tipo').value + '&IDE=' + document.getElementById('identificativo').value + '&ID=-1', Titolo, 'height=700,width=900');
                popupWindow.focus();
            }

            // document.getElementById('dvvvPre').style.visibility = 'hidden';

            function selezionaTutti(sender, args) {
                if (sender._checked)
                    document.getElementById('hiddenSelTutti').value = "1";
                else
                    document.getElementById('hiddenSelTutti').value = "0";
            };

            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);
        </script>
    </form>
</body>
</html>
