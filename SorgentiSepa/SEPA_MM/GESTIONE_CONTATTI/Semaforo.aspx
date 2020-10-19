<%@ Page Title="" Language="VB" MasterPageFile="~/GESTIONE_CONTATTI/HomePage.master"
    AutoEventWireup="false" CodeFile="Semaforo.aspx.vb" Inherits="GESTIONE_CONTATTI_Semaforo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ConfermaEsci() {
            //            var chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            //            if (chiediConferma == true) {
            //                document.getElementById('confermaUscita').value = '1';
            //            } else {
            //                document.getElementById('confermaUscita').value = '0';
            //            }
            document.getElementById('confermaUscita').value = '1';
        };
        function showImageOnSelectedItemChanging(sender, eventArgs) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + eventArgs.get_item(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
        function showFirstItemImage(sender) {
            var input = sender.get_inputDomElement();
            input.style.background = "url(" + sender.get_items().getItem(sender._selectedIndex).get_imageUrl() + ") no-repeat";
        };
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Impostazione semafori d'ufficio" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGridElenco">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGridElenco" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid ID="DataGridElenco" runat="server" AutoGenerateColumns="False"
        AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
        Width="97%" AllowPaging="FALSE" Height="700">
        <MasterTableView CommandItemDisplay="Top" AllowSorting="true" AllowMultiColumnSorting="true" Width="100%"
            NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true" EnableHierarchyExpandAll="FALSE"
            TableLayout="Fixed" ClientDataKeyNames="ID,ID_CATEGORIZZAZIONE,SEMAFORO1,SEMAFORO2,SEMAFORO3" DataKeyNames="ID,ID_CATEGORIZZAZIONE,SEMAFORO1,SEMAFORO2,SEMAFORO3">
            <CommandItemSettings ShowAddNewRecordButton="False" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CATEGORIA1" HeaderText="CATEGORIA 1" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CATEGORIA2" HeaderText="CATEGORIA 2" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CATEGORIA3" HeaderText="CATEGORIA 3" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CATEGORIA4" HeaderText="CATEGORIA 4" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ID_CATEGORIZZAZIONE" HeaderText="ID_CATEGORIZZAZIONE" Visible="FALSE"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SEMAFORO1" HeaderText="SEMAFORO1" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SEMAFORO2" HeaderText="SEMAFORO2" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SEMAFORO3" HeaderText="SEMAFORO3" Visible="false"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="CATEGORIZZAZIONE"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" >
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cmbTipologiaManutenzione" Filter="Contains" runat="server" AutoPostBack="false" >
                            <Items>
                                <telerik:RadComboBoxItem Value="-1" Text="" />
                                <telerik:RadComboBoxItem Value="1" Text="CANONE" />
                                <telerik:RadComboBoxItem Value="2" Text="EXTRA-CANONE" />
                                <telerik:RadComboBoxItem Value="3" Text="A CARICO UTENTE" />
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SEMAFORO ORARIO D'UFFICIO" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" >
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cmbUrgenza0" Filter="Contains" runat="server" AutoPostBack="false" OnClientSelectedIndexChanged="onCommandRadCombobox"
                            OnClientLoad="showFirstItemImage" OnClientSelectedIndexChanging="showImageOnSelectedItemChanging">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SEMAFORO FUORI ORARIO D'UFFICIO in settimana dal lunedì alle 16:45 al venerdì mattina alle 8:30"
                     AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" >
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cmbUrgenza1" Filter="Contains" runat="server" AutoPostBack="false"
                            OnClientLoad="showFirstItemImage" OnClientSelectedIndexChanging="showImageOnSelectedItemChanging">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SEMAFORO FUORI ORARIO D'UFFICIO dal venerdì alle 16:30 al lunedì mattina alle 8:30"
                     AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" >
                    <ItemTemplate>
                        <telerik:RadComboBox ID="cmbUrgenza2" Filter="Contains" runat="server" AutoPostBack="false"
                            OnClientLoad="showFirstItemImage" OnClientSelectedIndexChanging="showImageOnSelectedItemChanging">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="TIPOLOGIA" SortOrder="Ascending" />
                <telerik:GridSortExpression FieldName="CATEGORIA1" SortOrder="Ascending" />
                <telerik:GridSortExpression FieldName="CATEGORIA2" SortOrder="Ascending" />
                <telerik:GridSortExpression FieldName="CATEGORIA3" SortOrder="Ascending" />
                <telerik:GridSortExpression FieldName="CATEGORIA4" SortOrder="Ascending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true" ClientEvents-OnCommand="onCommand"  >
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false" UseClientSelectColumnOnly="false" />
           
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField runat="server" ID="confermaUscita" Value="0" ClientIDMode="Static" />
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="btnSalva" runat="server" Text="Salva" CssClass="bottone" ToolTip="Salva" />
            </td>
            <td>
                <asp:Button ID="imgEsci" runat="server" Text="Esci" CssClass="bottone" OnClientClick="ConfermaEsci();"
                    ToolTip="Esci" />
            </td>
        </tr>
    </table>
        </asp:Content>
