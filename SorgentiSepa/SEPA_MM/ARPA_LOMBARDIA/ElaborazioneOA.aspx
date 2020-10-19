<%@ Page Title="" Language="VB" MasterPageFile="~/ARPA_LOMBARDIA/HomePage.master" AutoEventWireup="false" CodeFile="ElaborazioneOA.aspx.vb" Inherits="ARPA_LOMBARDIA_ElaborazioneOA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnFileXml" runat="server" Text="Crea/Scarica XML" ToolTip="Crea o Scarica il File XML dell'Elaborazione">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnVisualizzaAnomalie" runat="server" Text="Visualizza Anomalie"
        ToolTip="Visualizza Anomalie">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esporta in Excel">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="TornaHome"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridElaborazione">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridElaborazione" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="RadGridElaborazione" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="true"
        EnableLinqExpressions="False" IsExporting="False" Width="97%" AllowPaging="true" PagerStyle-AlwaysVisible="true" 
        PageSize="100"
        ShowFooter="true">
        <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true"
            NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
            TableLayout="Auto">
            <CommandItemSettings ShowAddNewRecordButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="CF_ENTE_PROPRIETARIO" HeaderText="CF ENTE PROPRIETARIO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AZIONE" HeaderText="AZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_OCCUPAZIONE" HeaderText="COD. CONTRATTO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FLG_CF_CONOSCIUTO" HeaderText="CF CONOSCIUTO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CF" HeaderText="CF" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_NASCITA" HeaderText="DATA DI NASCITA"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="FLG_SESSO" HeaderText="SESSO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_CATASTALE_NAZ_NASCITA" HeaderText="COD. CATASTALE NAZIONE NASCITA" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SIGLA_PROVINCIA_NASCITA" HeaderText="SIGLA PROVINCIA NASCITA" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_CATASTALE_COM_NASCITA" HeaderText="COD. CATASTALE COMUME NASCITA" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_CITTADINANZA" HeaderText="COD. CITTADINANZA" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_ALLOGGIO" HeaderText="COD. ALLOGGIO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SIGLA_PROVINCIA_ALLOGGIO" HeaderText="SIGLA PROVINCIA ALLOGGIO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CD_CATASTALE_ALLOGGIO" HeaderText="COD. CATASTALE ALLOGGIO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PREFISSO_INDIRIZZO" HeaderText="PREFISSO INDIRIZZO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DENOMINAZIONE_INDIRIZZO" HeaderText="DENOMINAZIONE INDIRIZZO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NUM_CIVICO" HeaderText="NUM. CIVICO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_INIZIO_OCCUPAZIONE" HeaderText="DATA INIZIO OCCUPAZIONE"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ID_TIPO_OCCUPAZIONE" HeaderText="TIPO OCCUPAZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_TIPO_ATTO_RILEVAZIONE" HeaderText="TIPO ATTO RILEVAZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IDENTIFICATIVO_ATTO_RIL" HeaderText="IDENTIFICATIVO ATTO RILEVAZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_ATTO_RILEVAZIONE" HeaderText="DATA ATTO RILEVAZIONE"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ID_TIPO_ATTO_LEGITTIMANTE" HeaderText="TIPO ATTO LEGITTIMANTE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IDENTIFICATIVO_ATTO_LEGIT" HeaderText="IDENTIFICATIVO ATTO LEGITTIMANTE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="IDENTIFICATIVO_ATTO_RILASCIO" HeaderText="IDENTIFICATIVO ATTO RILASCIO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PROTOCOLLO_ATTO_RILASCIO" HeaderText="PROTOCOLLO ATTO RILASCIO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_ATTO_RILASCIO" HeaderText="DATA ATTO RILASCIO"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="ID_TIPO_PRESENZA_DEBITO" HeaderText="TIPO PRESENZA DEBITO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPO_DEBITO" HeaderText="TIPO DEBITO" ItemStyle-HorizontalAlign="Left"
                    Exportable="false" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}" Visible="false">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FLGTIPODEBITODANNEGGIAMENTO" HeaderText="TIPO DEBITO DANNEGGIAMENTO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FLGTIPODEBITOMANCATO" HeaderText="TIPO DEBITO MANCATO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FLGTIPODEBITOCOSTI" HeaderText="TIPO DEBITO COSTI" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FLGTIPODEBITOALTRO" HeaderText="TIPO DEBITO ALTRO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="FLG_ESTINZIONE_DEBITO" HeaderText="ESTINZIONE DEBITO" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_ESTINZIONE_DEBITO" HeaderText="DATA ESTINZIONE DEBITO"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="FLG_CESSAZIONE_OCCU" HeaderText="CESSAZIONE OCCUPAZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_CESSAZIONE_OCCUPAZIONE" HeaderText="DATA CESSAZIONE OCCUPAZIONE"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="IDENT_PROVV_CESSAZ" HeaderText="IDENTIFICATIVO PROVVEDIMENTO CESSAZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PROT_PROVV_CESSAZ" HeaderText="PROTOCOLLO PROVVEDIMENTO CESSAZIONE" ItemStyle-HorizontalAlign="Left"
                    Exportable="true" HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DT_PROVV_CESSAZ" HeaderText="DATA PROVVEDIMENTO CESSAZIONE"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
            </Columns>
            <HeaderStyle Wrap="false" />
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <telerik:RadWindow ID="RadWindowElaborazioneAnomalie" runat="server" CenterIfModal="true"
        Modal="true" Width="600px" Height="400px" VisibleStatusbar="false" Title="Elaborazione - Anomalie"
        Behaviors="Move, Close" RestrictionZoneID="RestrictionZoneID" ReloadOnShow="True"
        ShowContentDuringLoad="False">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PanelRadWindowElaborazioneAnomalie" Style="height: 100%;">
                <br />
                <telerik:RadTextBox ID="txtAnomalieElaborazioni" runat="server" Width="97%" Height="320px"
                    ReadOnly="true" TextMode="MultiLine">
                </telerik:RadTextBox>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left; width: 50%">
                <asp:Label ID="lblElabValidazione" runat="server" Text="Elaborazione creata senza la procedura di validazione!"
                    Font-Bold="True" Font-Italic="True" Font-Size="12pt" Font-Underline="True" Visible="False"
                    ForeColor="Red" />
            </td>
            <td style="text-align: right; width: 50%">
                <asp:Label ID="lblValidazioneFile" runat="server" Text="Il file risulta correttamente validato"
                    Font-Bold="True" Font-Italic="True" Font-Size="12pt" Font-Underline="True"
                    ForeColor="Red" />
            </td>
        </tr>
    </table>
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
    <asp:HiddenField ID="HFIdElaborazione" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="180" ClientIDMode="Static" />
</asp:Content>

