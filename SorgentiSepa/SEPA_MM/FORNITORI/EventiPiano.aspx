<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/OpenPage.master" AutoEventWireup="false" CodeFile="EventiPiano.aspx.vb" Inherits="FORNITORI_EventiPiano" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        //if (window.attachEvent) {
        //    window.attachEvent("onload", initDialog);
        //}
        //else if (window.addEventListener) {
        //    window.addEventListener("load", initDialog, false);
        //}

        //function getRadWindow() {
        //    if (window.radWindow) {
        //        return window.radWindow;
        //    }
        //    if (window.frameElement && window.frameElement.radWindow) {
        //        return window.frameElement.radWindow;
        //    }
        //    return null;
        //}

        //function initDialog() {
        //    getRadWindow().maximize();
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" Runat="Server">
     <asp:Label ID="lblTitolo" runat="server" Text="Eventi"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" Runat="Server">
       <telerik:RadGrid ID="RadGridEventi" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="false"
        EnableLinqExpressions="False" IsExporting="False" Width="97%" AllowPaging="true"
        PageSize="100">
        <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true"
            TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
            Width="100%">
            <CommandItemSettings ShowAddNewRecordButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" Visible="true" Exportable="true"
                    DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="DATA_EVENTO" HeaderText="DATA EVENTO"
                    FilterControlWidth="125px" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" CurrentFilterFunction="EqualTo" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" Visible="true" Exportable="true">
                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="150px" />
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="TIPO_EVENTO" HeaderText="TIPO EVENTO" Visible="true" Exportable="true"
                    DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE" Visible="true" Exportable="true"
                    DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="DATA_EVENTO" SortOrder="Descending" />
            </SortExpressions>
            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>

    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" Runat="Server">
 <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 50;
            var larghezzaRad = $(window).width() - 47;
            $("#OpenPage_ContentPlaceHolder2_RadGridEventi").width(larghezzaRad);
            $("#OpenPage_ContentPlaceHolder2_RadGridEventi").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
    </script>
</asp:Content>


