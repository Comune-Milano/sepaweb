<%@ Page Title="" Language="VB" MasterPageFile="~/ARPA_LOMBARDIA/OpenPage.master"
    AutoEventWireup="false" CodeFile="AnomalieF.aspx.vb" Inherits="ARPA_LOMBARDIA_AnomalieF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function esci() {
            self.close();
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esporta in Excel">
    </telerik:RadButton>
    &nbsp;
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="esci"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="RadGridAnomalie" runat="server" AllowSorting="false" AutoGenerateColumns="False"
        AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="true" PageSize="100">
        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" ClientDataKeyNames="ID" DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COD_EDIFICIO" HeaderText="CODICE EDIFICIO">
                    <HeaderStyle HorizontalAlign="Center" Width="180px" Wrap="false" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MOTIVAZIONE_ERRORE" HeaderText="MOTIVAZIONE ERRORE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATO_ERRORE" HeaderText="DATO ERRATO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="70" ClientIDMode="Static" />
    <asp:HiddenField ID="HFTipoGestione" runat="server" Value="" ClientIDMode="Static" />
</asp:Content>
