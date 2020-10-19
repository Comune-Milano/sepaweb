<%@ Page Title="" Language="VB" MasterPageFile="~/GESTIONE_CONTATTI/HomePage.master"
    AutoEventWireup="false" CodeFile="SegnalazioniAlert.aspx.vb" Inherits="GESTIONE_CONTATTI_SegnalazioniAlert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script src="../CICLO_PASSIVO/CicloPassivo.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ClickUscita(sender, args) {
            if (document.getElementById('HiddenNoMenu').value == 1)
                self.close();
            else
            location.href = 'Home.aspx';
        };
        function RowSelecting(sender, args) {
            document.getElementById('idSelected').value = args.getDataKeyValue("ID");
        };
        function ModificaDblClick() {
            document.getElementById('MasterPage_CPFooter_btnVisualizza').click();
        };
        function ApriSegnalazione() {
            if (document.getElementById('idSelected').value != '') {
                CenterPage2('Segnalazione.aspx?NM=1&IDS=' + document.getElementById('idSelected').value, 'Segnalazione' + document.getElementById('idSelected').value, 1200, 800);
            } else {
                alert('Selezionare una segnalazione!');
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label Text="Segnalazioni" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPFooter" runat="Server">
     <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton Text="Visualizza" runat="server" ToolTip="Visualizza segnalazione"
                    ID="btnVisualizza" OnClientClicking="function(sender,args){ ApriSegnalazione();}"
                    AutoPostBack="false" />
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita"
                    TabIndex="3">
                </telerik:RadButton>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
 <table style="width: 100%">
        <tr>
            <td>
                <telerik:RadGrid ID="dgvSegnalazioni" runat="server" AutoGenerateColumns="False"
                    AllowFilteringByColumn="true" EnableLinqExpressions="False" IsExporting="False"
                    Width="97%" AllowPaging="true" PageSize="100">
                    <MasterTableView CommandItemDisplay="none" AllowSorting="true" AllowMultiColumnSorting="true"
                        TableLayout="Fixed" NoMasterRecordsText="Nessun dato da visualizzare." ShowHeadersWhenNoRecords="true"
                        Width="200%" ClientDataKeyNames="ID, STATO,ID_STATO" DataKeyNames="ID,STATO,ID_STATO">
                        <CommandItemSettings ShowAddNewRecordButton="False" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" Exportable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" Visible="true" Exportable="true"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Visible="false" EmptyDataText=" ">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO_INT" HeaderText="PRIORITA'" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" SortExpression="ID_PERICOLO_SEGNALAZIONE"
                                CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO0" HeaderText="CATEGORIA" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" Visible="true" Exportable="true"
                                SortExpression="ID_STATO" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INSERIMENTO"
                                Visible="true" Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" Visible="true"
                                Exportable="true" DataFormatString="{0:@}" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="N_SOLLECITI" HeaderText="N° SOLLECITI" Visible="true"
                                Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" Visible="true"
                                Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                Visible="false" EmptyDataText=" ">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                Visible="true" Exportable="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_STATO" HeaderText="ID_STATO" Visible="false"
                                Exportable="false">
                            </telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn DataField="ALLEGATI_PRESENTI" HeaderText="ALLEGATI PRESENTI">
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Wrap="true" HorizontalAlign="Center" />
                        <SortExpressions>
                            <telerik:GridSortExpression FieldName="ID_PERICOLO_SEGNALAZIONE" SortOrder="Descending" />
                            <telerik:GridSortExpression FieldName="ID_STATO" SortOrder="Ascending" />
                            <telerik:GridSortExpression FieldName="N_SOLLECITI" SortOrder="Descending" />
                            <telerik:GridSortExpression FieldName="FIGLI" SortOrder="Descending" />
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
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="Modificato" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="IndiceFornitore" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenNoMenu" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="OPS" />
    <asp:HiddenField ID="idSelected" runat="server" Value="" ClientIDMode="Static" />
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
            $("#MasterPage_CPContenuto_dgvSegnalazioni").width(larghezzaRad);
            $("#MasterPage_CPContenuto_dgvSegnalazioni").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
    </script>
</asp:Content>


