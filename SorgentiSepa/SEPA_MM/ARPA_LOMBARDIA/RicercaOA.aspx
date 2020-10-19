<%@ Page Title="" Language="VB" MasterPageFile="~/ARPA_LOMBARDIA/HomePage.master" AutoEventWireup="false" CodeFile="RicercaOA.aspx.vb" Inherits="ARPA_LOMBARDIA_RicercaOA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function RowSelecting(sender, args) {
            document.getElementById('HFIdElaborazione').value = args.getDataKeyValue("ID");
        };
        function VisualizzaElaborazione(sender, args) {
            var idElaborazione = document.getElementById('HFIdElaborazione').value;
            if (idElaborazione != '') {
                ApriElaborazioneOA();
            } else {
                var notification = $find("<%= RadNotificationNote.ClientID %>");
                var message = 'Nessuna elaborazione selezionata!';
                notification.set_title('Attenzione');
                notification.set_text(message);
                notification.show();
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Ricerca Elaborazioni"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Visualizza Elaborazione"
        OnClientClicking="VisualizzaElaborazione" AutoPostBack="false">
    </telerik:RadButton>
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="TornaHome"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridElaborazioni">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridElaborazioni" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadGrid ID="RadGridElaborazioni" runat="server" AllowSorting="false" AutoGenerateColumns="False"
        AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="true" PageSize="20">
        <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" ClientDataKeyNames="ID" DataKeyNames="ID"
            AllowMultiColumnSorting="true" CommandItemSettings-ShowAddNewRecordButton="false">
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="#">
                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA E ORA" SortExpression="DATA_ORA_ORDER">
                    <HeaderStyle HorizontalAlign="Center" Width="125px" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CF_ENTE_PROPRIETARIO" HeaderText="CODICE FISCALE ENTE PROPRIETARIO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="ANNO" SortOrder="Descending" />
                <telerik:GridSortExpression FieldName="ID" SortOrder="Descending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
            <Selecting AllowRowSelect="True" />
            <ClientEvents OnRowSelecting="RowSelecting" OnRowDblClick="VisualizzaElaborazione" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="130" ClientIDMode="Static" />
    <asp:HiddenField ID="HFIdElaborazione" runat="server" Value="" ClientIDMode="Static" />
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
</asp:Content>