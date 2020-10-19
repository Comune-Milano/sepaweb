<%@ Page Title="Prospetto consuntivi" Language="VB" MasterPageFile="HomePage.master"
    AutoEventWireup="false" CodeFile="ProspettoConsuntivi.aspx.vb" Inherits="SPESE_REVERSIBILI_ProspettoConsuntivi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .RadGrid .rgDataDiv, .RadGrid .rgRow td, .RadGrid .rgAltRow td
        {
            position: relative;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function RowSelecting(sender, args) {
            document.getElementById('txtID').value = args.getDataKeyValue("ID");
        };
        function apriConfirm(testo, funzione, larghezza, altezza, titolo, img) {
            var confirmTelerik = radconfirm(testo, funzione, larghezza, altezza, null, titolo, img);
            confirmTelerik = confirmTelerik.set_behaviors();
        };
        function Aggiungi(btnToClik, idradwindow, tipo, page, w, h, maximizeWin) {
            if ('undefined' === typeof maximizeWin) {
                maximizeWin = 0;
            };
            if (tipo == 0) {
                openModalInRad(idradwindow, page + '?BTN=' + btnToClik.id + '&IDSPESA=-1', w, h);
            } else {
                openModalInRadClose(idradwindow, page + '?BTN=' + btnToClik.id + '&IDSPESA=-1', w, h, maximizeWin);
            };
        };
        function Modifica(btnToClik, idradwindow, tipo, page, hfidsel, ntf, w, h, maximizeWin) {
            if ('undefined' === typeof maximizeWin) {
                maximizeWin = 0;
            };
            if (document.getElementById(hfidsel)) {
                if (document.getElementById(hfidsel).value != '0') {
                    if (tipo == 0) {
                        openModalInRad(idradwindow, page + '?BTN=' + btnToClik.id + '&IDSPESA=' + document.getElementById(hfidsel).value, w, h);
                    } else {
                        openModalInRadClose(idradwindow, page + '?BTN=' + btnToClik.id + '&IDSPESA=' + document.getElementById(hfidsel).value, w, h, maximizeWin);
                    };
                } else {
                    var notification = $find(ntf);
                    notification.set_title('Attenzione');
                    notification.set_text('Nessun elemento selezionato!');
                    notification.show();
                };
            };
        };
        function Elimina(btnToClik, hfidsel, ntf) {
            if (document.getElementById(hfidsel)) {
                if ((document.getElementById(hfidsel).value != '') && (document.getElementById(hfidsel).value != '0')) {
                    apriConfirm('Confermi di voler eliminare la voce di spesa selezionata?', function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, 'Attenzione', null);
                } else {
                    var notification = $find(ntf);
                    notification.set_title('Attenzione');
                    notification.set_text('Nessun elemento selezionato!');
                    notification.show();
                };
            };
        };

        function clickElimina(btnToClik) {
            var attr;
            attr = $('#' + btnToClik).attr('onclick');
            $('#' + btnToClik).attr('onclick', '__doPostBack("' + document.getElementById(btnToClik).name + '", "")');
            document.getElementById(btnToClik).click();
            $('#' + btnToClik).attr('onclick', attr);
        };
        //funzioni nuove

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) {
                oWindow = window.radWindow;
            } else {
                if (window.frameElement) {
                    if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;
                    };
                };
            };
            return oWindow;
        };
        function refreshPage(btnToClik) {

            if (document.getElementById(btnToClik)) {
                var attr;
                attr = $('#' + btnToClik).attr('onclick');
                $('#' + btnToClik).attr('onclick', '');
                document.getElementById(btnToClik).click();
                $('#' + btnToClik).attr('onclick', attr);

            };
        };
        function CloseAndRefresh(pulsante) {
            GetRadWindow().close();
            GetRadWindow().BrowserWindow.refreshPage(pulsante);
        };


        //fine

        function confermaEl() {
            //   document.getElementById('MasterPage_tipoSubmit').value = 1;
            var chiediConferma = window.confirm('Questa operazione eliminerà tutti i consuntivi.\nVuoi continuare?');
            if (chiediConferma) {
                document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazione').value = 1;
            } else {
                document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazione').value = 0;
            }
        }
        function confermaElPro() {
            //   document.getElementById('MasterPage_tipoSubmit').value = 1;
            var chiediConferma = window.confirm('Questa operazione eliminerà tutte le voci inserite nel prospetto.\nVuoi continuare?');
            if (chiediConferma) {
                document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazioneProspetto').value = 1;
            } else {
                document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazioneProspetto').value = 0;
            }
        }
        function conferma() {
            //    document.getElementById('MasterPage_tipoSubmit').value = 1;
            var chiediConferma = window.confirm('Questa operazione eliminerà le spese importate precedentemente;\nle spese aggiunte manualmente non verranno eliminate.\nVuoi continuare?');
            if (chiediConferma) {
                document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaImportazione').value = 1;
            } else {
                document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaImportazione').value = 0;
            }
        }

        function confermaSimulazione() {
            var dataDa = document.getElementById('RadDatePickerDa').value;
            var dataA = document.getElementById('RadDatePickerA').value;
            if (dataDa == "" || dataA == "") {
                alert('Valorizzare il periodo di riferimento!');
            }
            else {
                dataA = new Date(dataA);
                dataDa = new Date(dataDa);
                if (dataA < dataDa) {
                    alert("'Periodo di Riferimento A' deve essere maggiore di 'Periodo di riferimento da'");
                }
                else {
                    document.getElementById('MasterPage_tipoSubmit').value = 1;
                    var chiediConferma = window.confirm('Questa operazione eliminerà le simulazioni create precedentemente.\nVuoi continuare?');
                    if (chiediConferma) {
                        document.getElementById('MasterPage_ContentPlaceHolder2_HiddenFieldConfermaSimulazione').value = 1;
                    } else {
                        document.getElementById('MasterPage_ContentPlaceHolder2_HiddenFieldConfermaSimulazione').value = 0;
                    }
                };
            };
        }

        function apriAggiungiSpesaCons() {
            var oWnd = $find('RadWindow3');
            oWnd.setUrl('AggiungiSpesaConsuntivi.aspx');
            oWnd.show();
        }
        function modificaSpesa(id) {
            document.getElementById('txtID').value = id;
            document.getElementById('MasterPage_ContentPlaceHolder2_ButtonModificaSpesa').click();
        }
        function eliminaSpesa(id) {
            var chiediConferma = window.confirm('Questa operazione eliminerà definitivamente questa spesa. Vuoi continuare?');
            if (chiediConferma) {
                document.getElementById('MasterPage_ContentPlaceHolder2_HiddenFieldIdSpesa').value = id;
                document.getElementById('MasterPage_ContentPlaceHolder2_ButtonElimina').click();

            }
        }
        window.onresize = setDimensioni;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelRadGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelRadGrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadWindow ID="RadWindow3" runat="server" CenterIfModal="true" Modal="True"
        ClientIDMode="Static" VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize"
        Skin="Web20" Height="800" Width="600">
    </telerik:RadWindow>
    <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle;text-align:center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Tutte le spese elencate nel prospetto sottostante saranno sottoposte al calcolo del consuntivo cliccando sul bottone "Calcolo consuntivi"</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    <fieldset style="border-width: 2px;">
        <legend>&nbsp;&nbsp;Parametri di elaborazione&nbsp;&nbsp;</legend>
        <table style="width: 400px;">
            <tr>
                <td>
                    Periodo di riferimento da
                </td>
                <td>
                    <telerik:RadDatePicker ID="RadDatePickerDa" runat="server" WrapperTableCaption=""
                        ClientIDMode="Static" MaxDate="01/01/9999" ToolTip="Data di riferimento iniziale"
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                        <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                        </DateInput>
                        <Calendar ID="Calendar1" runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today">
                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDatePicker>
                </td>
                <td>
                    a
                </td>
                <td>
                    <telerik:RadDatePicker ID="RadDatePickerA" runat="server" WrapperTableCaption=""
                        ClientIDMode="Static" MaxDate="01/01/9999" ToolTip="Data di riferimento finale"
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                        <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                        </DateInput>
                        <Calendar ID="Calendar2" runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today">
                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
    </fieldset>
    <table width="100%">
        <tr>
            <td>
                <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%">
                    <telerik:RadGrid ID="DataGridProspetto" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="97%" AllowSorting="True" IsExporting="False"
                        ShowFooter="true"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top" Width="100%" ClientDataKeyNames="ID" DataKeyNames="ID" AllowMultiColumnSorting="true">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="VOCE_SPESA" HeaderText="VOCE SPESA" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    FilterControlWidth="80%"
                                    DataFormatString="{0:@}">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO_SPESA" HeaderText="TIPOLOGIA" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    FilterControlWidth="80%"
                                    DataFormatString="{0:@}">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CRITERIO_RIPARTIZIONE" HeaderText="CRITERIO DI RIPARTIZIONE"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains"
                                    FilterControlWidth="80%"
                                    DataFormatString="{0:@}">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FL_MANUALE" HeaderText="INSERITO MANUALMENTE"
                                    HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:@}" 
                                    FilterControlWidth="80%"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPOLOGIA_DIVISIONE" HeaderText="TIPOLOGIA DIVISIONE"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" 
                                    FilterControlWidth="80%"
                                    DataFormatString="{0:@}">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPO" HeaderText="TIPO" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Center" Visible="false" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" 
                                    FilterControlWidth="80%"
                                    DataFormatString="{0:@}">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OGGETTO" HeaderText="OGGETTO" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    FilterControlWidth="80%"
                                    DataFormatString="{0:@}">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LOTTO" HeaderText="LOTTO" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPIANTO" HeaderText="IMPIANTO" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AGGREGAZIONE" HeaderText="AGGREGAZIONE" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO" HeaderStyle-HorizontalAlign="Center"
                                    AutoPostBackOnFilter="true" 
                                    FilterControlWidth="80%"
                                    Aggregate="Sum"
                                    DataFormatString="{0:C2}" CurrentFilterFunction="EqualTo">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true"></FooterStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <div style="float: right; padding: 4px;">
                                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                        CssClass="rgRefresh" OnClientClick="caricamento(2);" />
                                    <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                        CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="caricamento(2);" />
                                </div>
                            </CommandItemTemplate>
                            <SortExpressions>
                            <telerik:GridSortExpression FieldName="CRITERIO_RIPARTIZIONE" SortOrder="Ascending" />
                            <telerik:GridSortExpression FieldName="TIPO" SortOrder="Ascending" />
                            </SortExpressions>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true">
                            </Scrolling>
                            <Selecting AllowRowSelect="true" />
                            <ClientEvents OnRowSelecting="RowSelecting" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnRefreshGrid" runat="server" Text=""  ClientIDMode="Static"
        CssClass="nascondi" />
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="350" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="btnInserisci" runat="server" Text="Inserisci" ToolTip="Inserisci"
        OnClientClick="Aggiungi(this, 'RadWindow3', 1, 'AggiungiSpesaConsuntivi.aspx', 700, 700);return false;" />
    <asp:Button ID="btnModifica" runat="server" Text="Modifica" ToolTip="Modifica" OnClientClick="Modifica(this, 'RadWindow3', 1, 'AggiungiSpesaConsuntivi.aspx', 'txtID', 'MasterPage_ContentPlaceHolder2_RadNotificationNote', 700, 700);return false;" />
    <asp:Button ID="btnElimina" runat="server" Text="Elimina" ToolTip="Elimina" OnClientClick="Elimina(this, 'txtID', 'MasterPage_ContentPlaceHolder2_RadNotificationNote');return false;" />
    <asp:Button ID="ButtonAnomalia" runat="server" Text="Calcolo consuntivi"
        ToolTip="Calcolo dei consuntivi" OnClientClick="confermaSimulazione();" />
    <asp:Button ID="ButtonEliminaVoci" runat="server" Text="Elimina voci prospetto" OnClientClick="confermaElPro();" />
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
    <asp:Button ID="ButtonModificaSpesa" runat="server" Text="Modifica spesa" 
        OnClientClick="apriModificaSpesaCons();" CssClass="nascondi" />
    <asp:HiddenField ID="txtID" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="ConfermaImportazione" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenFieldConfermaSimulazione" runat="server" Value="0" />
    <asp:Button ID="ButtonElimina" runat="server" Text="" CssClass="nascondi" />
    <asp:HiddenField ID="HiddenFieldIdSpesa" runat="server" Value="0" />
    <asp:HiddenField ID="ConfermaEliminazione" runat="server" Value="0" />
    <asp:HiddenField ID="ConfermaEliminazioneProspetto" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenFieldSelectAll" runat="server" Value="1" />
    <asp:HiddenField ID="HiddenFieldNoLoading" ClientIDMode="Static" runat="server" Value="1" />
    <asp:HiddenField ID="HFElencoGriglie" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightElencoGriglie" runat="server" Value="500" ClientIDMode="Static" />
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
</asp:Content>
