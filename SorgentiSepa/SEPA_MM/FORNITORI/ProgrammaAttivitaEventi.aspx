<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false" CodeFile="ProgrammaAttivitaEventi.aspx.vb" Inherits="FORNITORI_ProgrammaAttivitaEventi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Modifica(sender, args) {
                validNavigation = false;
            }

            function Uscita() {
                validNavigation = true;
                location.href = 'Home.aspx';
            }

        </script>
    </telerik:RadCodeBlock>
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esporta i risultati in Excel">
                </telerik:RadButton>
            </td>
            <td>&nbsp;
            </td>
            <td>
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="Uscita">
                </telerik:RadButton>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadGrid ID="RadGridRPTLOG" runat="server" AutoGenerateColumns="False" AllowFilteringByColumn="true"
        EnableLinqExpressions="False" IsExporting="False" Width="97%" AllowPaging="true"
        PageSize="100">
        <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true"
            TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
            Width="100%" ClientDataKeyNames="ID_PROGRAMMA_ATTIVITA" DataKeyNames="ID_PROGRAMMA_ATTIVITA">
            <CommandItemSettings ShowAddNewRecordButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID_PROGRAMMA_ATTIVITA" HeaderText="PROGRAMMA ATTIVITA'">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_EVENTO" HeaderText="DATA EVENTO" DataFormatString="{0:dd/MM/yyyy}"
                    AutoPostBackOnFilter="true" ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE">
                </telerik:GridBoundColumn>
            </Columns>
            <HeaderStyle Wrap="true" HorizontalAlign="Center" />
            <SortExpressions>
                <telerik:GridSortExpression FieldName="ID_PROGRAMMA_ATTIVITA" SortOrder="Descending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />

    <asp:HiddenField runat="server" ClientIDMode="Static" ID="Modificato" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <script type="text/javascript">
            $(document).ready(function () {
                Ridimensiona();
            });
            $(window).resize(function () {
                Ridimensiona();
            });
            function Ridimensiona() {
                var altezzaRad = $(window).height() - 250;
                var larghezzaRad = $(window).width() - 47;
                $("#MasterPage_CPContenuto_RadGridRPTLOG").width(larghezzaRad);
                $("#MasterPage_CPContenuto_RadGridRPTLOG").height(altezzaRad);
                document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
                document.getElementById('AltezzaRadGrid').value = altezzaRad;
            }
    </script>
    <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>

