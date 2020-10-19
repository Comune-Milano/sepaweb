<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="Piano.aspx.vb" Inherits="FORNITORI_Piano" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../StandardTelerik/Scripts/modalTelerik.js" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ClickUscita(sender, args) {
            if (document.getElementById('HiddenProvenienza').value == 'SEGNALAZIONI') {
                self.close();
            }
            else {
                location.href = 'Home.aspx';
            }
        };

        function AllegaFile() {
            CenterPage('../GestioneAllegati/GestioneAllegati.aspx?T=2&O=' + document.getElementById('TipoAllegato').value + '&I=' + document.getElementById('idCronoprogramma').value, 'Allegati', 1000, 800);
        };
        function ApriDettaglioPiano(indice, cronoprogramma, codiceEdificio) {
            openModalInRadClose('RadWindow1', 'PianoDettaglioCrono.aspx?idedificio=' + codiceEdificio + '&indice=' + indice + '&idcrono=' + cronoprogramma, 1000, 700);
        };
        function ApriEventiPiano(cronoprogramma) {
            openModalInRadClose('RadWindow1', 'EventiPiano.aspx?idcrono=' + cronoprogramma, 1000, 700);
        };
        function ApriEdifici(cronoprogramma) {
            openModalInRadClose('RadWindow1', 'EdificiApprovati.aspx?idcrono=' + cronoprogramma, 1000, 700, 1);
        };

        function ApriEdificiNonValorizzati(cronoprogramma) {
            openModalInRadClose('RadWindow1', 'EdificiNonValorizzati.aspx?idcrono=' + cronoprogramma, 1000, 700, 1);
        };
        function confermaEl(btnToClik) {
            //   document.getElementById('MasterPage_tipoSubmit').value = 1;
            apriConfirm('Vuoi eliminare il cronoprogramma?',
                function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);

            //var chiediConferma = window.confirm('Questa operazione eliminerà tutti i consuntivi.\nVuoi continuare?');
            //if (chiediConferma) {
            //    document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazione').value = 1;
            //} else {
            //    document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazione').value = 0;
            //}
        };

        function confermaApp(btnToClik) {
            //   document.getElementById('MasterPage_tipoSubmit').value = 1;
            apriConfirm('Sei sicuro di voler approvare il cronoprogramma anche se incompleto?',
                function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);

            //var chiediConferma = window.confirm('Questa operazione eliminerà tutti i consuntivi.\nVuoi continuare?');
            //if (chiediConferma) {
            //    document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazione').value = 1;
            //} else {
            //    document.getElementById('MasterPage_ContentPlaceHolder2_ConfermaEliminazione').value = 0;
            //}
        };


    </script>
    <style type="text/css">
        .Pulsante
        {
            cursor: pointer;
        }
        
        .nascondi
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Cronoprogramma attività a canone"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGridCronoprogramma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGridCronoprogramma" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <asp:Button ID="btnSalva" runat="server" Text="Salva" Style="cursor: pointer" />
    <asp:Button Text="Download cronoprogramma" runat="server" ToolTip="Download Cronoprogramma attività a canone"
        ID="btnDownload" Style="cursor: pointer" />
    <asp:Button ID="btnModifica" runat="server" Text="Upload file excel" Style="cursor: pointer" />
    <asp:Button ID="btnUploadRendicontazione" runat="server" Text="Upload file rendicontazione"
        Style="cursor: pointer" />
    <asp:Button ID="btnElimina" runat="server" Text="Elimina" Style="cursor: pointer"
        OnClientClick="confermaEl(this);return false;" />
    <asp:Button ID="btnApp" runat="server" Text="Approva" ToolTip="Approva il cronoprogramma"
        Style="cursor: pointer" />
    <asp:Button ID="btnApp1" runat="server" Text="Approva" ToolTip="Approva il cronoprogramma"
        Style="cursor: pointer" CssClass="nascondi" ClientIDMode="Static" OnClientClick="confermaApp(this);return false;" />
    <asp:Button ID="btnMostraEdifici" runat="server" Text="Mostra edifici approvati"
        ToolTip="Visualizza la lista degli edifici approvati in altri cronoprogrammi"
        Style="cursor: pointer" OnClientClick="ApriEdifici(document.getElementById('idCronoprogramma').value);return false;" />
    <asp:Button ID="btnEdificiNonValorizzati" runat="server" Text="Mostra edifici non valorizzati"
        ToolTip="Visualizza la lista degli edifici non ancora valorizzati" Style="cursor: pointer"
        OnClientClick="ApriEdificiNonValorizzati(document.getElementById('idCronoprogramma').value);return false;" />
    <asp:Button Text="Allegati" runat="server" ID="btnAllegati" OnClientClick="AllegaFile();return false;"
        Style="cursor: pointer" />
    <asp:Button Text="Eventi" runat="server" ID="btnEventi" OnClientClick="ApriEventiPiano(document.getElementById('idCronoprogramma').value);return false;"
        Style="cursor: pointer" />
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
        AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita"
        TabIndex="3">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table>
        <tr>
            <td style="width: 15%">
                Fornitore
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtFornitore" Width="300px" Enabled="false" />
            </td>
            <td>
                Appalto
            </td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtAppalto" Width="300px" Enabled="false" TextMode="MultiLine"
                    Rows="3" />
            </td>
        </tr>
        <tr>
            <td>
                Tipologia
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtTipologia" Width="150px" Enabled="false" />
            </td>
            <td>
                Attività
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtAttivita" Width="150px" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                Data inizio
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDataInizio" runat="server" WrapperTableCaption="" Enabled="false"
                    MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                    ShowPopupOnFocus="true">
                    <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                        <ClientEvents OnKeyPress="CompletaDataTelerik" />
                        <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                    </DateInput>
                    <Calendar ID="Calendar2" runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today">
                                <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                </telerik:RadDatePicker>
            </td>
            <td>
                Data fine
            </td>
            <td>
                <telerik:RadDatePicker ID="txtDataFine" runat="server" WrapperTableCaption="" MinDate="01/01/1000"
                    MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}" ShowPopupOnFocus="true">
                    <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                        <ClientEvents OnKeyPress="CompletaDataTelerik" />
                        <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                    </DateInput>
                    <Calendar ID="Calendar1" runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today">
                                <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                </telerik:RadDatePicker>
            </td>
        </tr>
        <tr>
            <td>
                Data inserimento
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDataInserimento" Width="70px" Enabled="false" />
            </td>
            <td>
                Data ultima modifica
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDataUltimaModifica" Width="70px" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                Data ultima approvazione
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDataUltimaApprovazione" Width="70px" Enabled="false" />
            </td>
            <td>
                Stato
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtStato" Width="150px" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                Carica File Excel
            </td>
            <td>
                <telerik:RadAsyncUpload ID="RadUploadAllegato" runat="server" AllowedFileExtensions="rtf,doc,docx,tiff,pdf,zip,xls,xlsx,jpg,png"
                    MaxFileInputsCount="1" />
            </td>
            <td>
                Carica File rendicontazione
            </td>
            <td>
                <telerik:RadAsyncUpload ID="radUploadFileRendicontazione" runat="server" AllowedFileExtensions="rtf,doc,docx,tiff,pdf,zip,xls,xlsx,jpg,png"
                    MaxFileInputsCount="1000" MultipleFileSelection="Automatic" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <telerik:RadGrid ID="DataGridCronoprogramma" runat="server" GroupPanelPosition="Top"
                    AllowPaging="true" PagerStyle-AlwaysVisible="true" PageSize="50" AutoGenerateColumns="TRUE"
                    Culture="it-IT" AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%"
                    AllowSorting="True" Height="400" IsExporting="False" ShowFooter="true">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        AllowMultiColumnSorting="true" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="false" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <PagerStyle AlwaysVisible="True" />
                        <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                    </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
                        HideStructureColumns="true">
                        <Excel FileExtension="xlsx" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="2" />
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True"
        Title="Dettaglio cronoprogramma" VisibleStatusbar="False" Behavior="Pin, Move, Resize, Maximize"
        Width="900px" Height="1000" ClientIDMode="Static" ShowContentDuringLoad="false">
    </telerik:RadWindow>
    <asp:HiddenField runat="server" ID="TipoAllegato" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idFornitore" Value="0" />
    <asp:HiddenField runat="server" ID="idDirettoreLavori" Value="0" />
    <asp:HiddenField runat="server" ID="idGruppo" Value="0" />
    <asp:HiddenField runat="server" ID="idCronoprogramma" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="numDate" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idStato" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="idAttivitaCronoprogramma" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenProvenienza" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 410;
            var larghezzaRad = $(window).width() - 77;
            $("#MasterPage_CPContenuto_DataGridCronoprogramma").width(larghezzaRad);
            $("#MasterPage_CPContenuto_DataGridCronoprogramma").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
    </script>
</asp:Content>
