<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="CaricaPiani.aspx.vb" Inherits="FORNITORI_CaricaPiani" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../CICLO_PASSIVO/CicloPassivo.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ClickUscita() {
            location.href = 'Home.aspx';
        };
        function RowSelecting(sender, args) {
            document.getElementById('idSelected').value = args.getDataKeyValue("ID");
        };
        function ModificaDblClick() {
            document.getElementById('CPMenu_btnVisualizza').click();
        };

        function CreaPiano() {
            location.href = 'CreaPiano.aspx';
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Cronoprogramma attività a canone"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Button Text="Crea" runat="server" ID="btnCreaProgramma" OnClientClick="CreaPiano();return false;" />
                <asp:Button Text="Visualizza" runat="server" ID="btnVisualizza" OnClientClick="ApriPiano();return false;" />
                <asp:Button Text="Esci" runat="server" ID="btnEsci" OnClientClick="ClickUscita();return false;" />
            </td>
        </tr>
        <tr>

            <td>
                <telerik:RadButton ID="RadButtonDirettoreLavori" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Direttore Lavori" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
               
            </td>
            <td>
                 <telerik:RadButton ID="RadButtonBuildingManager" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Building Manager" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="RadButtonFieldQualityManager" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Coordinatore qualità" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
            <td>
                <telerik:RadButton ID="RadButtonTecnicoAmministrativo" runat="server" ToggleType="Radio"
                    ButtonType="StandardButton" GroupName="StandardButton" Skin="Default">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadioChecked" />
                        <telerik:RadButtonToggleState Text="Tecnico Amministrativo" PrimaryIconCssClass="rbToggleRadio" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>

        </tr>
    </table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">

    <table style="width: 100%">
        <tr>
            <td style="height: 50px">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="RadGridPiani" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="true"
                    EnableLinqExpressions="False" IsExporting="False" Width="97%" AllowPaging="true"
                    PageSize="100">
                    <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true"
                        TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                        Width="100%" ClientDataKeyNames="ID,ID_STATO,STATO" DataKeyNames="ID,ID_STATO,STATO">
                        <CommandItemSettings ShowAddNewRecordButton="False" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="NUMERO PROGRAMMA" Visible="true" Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="false"
                                Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="APPALTO" HeaderText="APPALTO" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"
                                Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_ULTIMA_MODIFICA" HeaderText="DATA ULTIMA MODIFICA"
                                Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_ULTIMA_APPROVAZIONE" HeaderText="DATA ULTIMA APPROVAZIONE"
                                Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" Exportable="true"
                                DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPOLOGIA_CRONOPROGRAMMA" HeaderText="TIPOLOGIA" Visible="true" Exportable="true"
                                DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ATTIVITA_CRONOPROGRAMMA" HeaderText="ATTIVITA'" Visible="true" Exportable="true"
                                DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO"
                                Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_FINE" HeaderText="DATA FINE"
                                Visible="true" Exportable="true" DataFormatString="{0:dd/MM/yyyy}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <ItemStyle Wrap="true" />
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="ID" SortOrder="Descending" />
                        </SortExpressions>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
                        <ClientEvents OnRowSelecting="RowSelecting" OnRowClick="RowSelecting" OnRowDblClick="ModificaDblClick" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="idFornitore" Value="0" />
    <asp:HiddenField runat="server" ID="idDirettoreLavori" Value="0" />
    <asp:HiddenField ID="idSelected" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="AltezzaRadGrid" Value="0" />
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 300;
            var larghezzaRad = $(window).width() - 47;
            $("#MasterPage_CPContenuto_RadGridPiani").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridPiani").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }


        function ApriPiano() {
            if (document.getElementById('idSelected').value != '') {

                var RadButtonBuildingManager = $find("<%= RadButtonBuildingManager.ClientID %>");
                          var RadButtonDirettoreLavori = $find("<%= RadButtonDirettoreLavori.ClientID %>");
                var RadButtonFieldQualityManager = $find("<%= RadButtonFieldQualityManager.ClientID %>");
                var RadButtonTecnicoAmministrativo = $find("<%= RadButtonTecnicoAmministrativo.ClientID %>");
                var continua = false;
                var parametri = '';
                if (!RadButtonBuildingManager && !RadButtonDirettoreLavori && !RadButtonFieldQualityManager && !RadButtonTecnicoAmministrativo) {
                    parametri = '&TIPOLOGIA=FO';
                    continua = true;
                }
                else {
                    if (RadButtonBuildingManager && RadButtonBuildingManager._checked) {
                        parametri = '&TIPOLOGIA=BM';
                        continua = true;
                    } else if (RadButtonDirettoreLavori && RadButtonDirettoreLavori._checked) {

                        parametri = '&TIPOLOGIA=DL';
                        continua = true;
                    } else if (RadButtonFieldQualityManager && RadButtonFieldQualityManager._checked) {

                        parametri = '&TIPOLOGIA=FQM';
                        continua = true;
                    } else if (RadButtonTecnicoAmministrativo && RadButtonTecnicoAmministrativo._checked) {

                        parametri = '&TIPOLOGIA=TA';
                        continua = true;
                    }
                }
                if (continua) {
                    location.href = 'Piano.aspx?TIPO=1&ID=' + document.getElementById('idSelected').value + parametri;

                } else {
                    alert('Selezionare una tipologia!');
                }

            } else {
                alert('Selezionare un programma!');
            };
        };
    </script>
</asp:Content>

