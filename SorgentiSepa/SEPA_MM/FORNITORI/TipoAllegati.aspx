<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="TipoAllegati.aspx.vb" Inherits="FORNITORI_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Tipologie documenti allegati"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function Modifica(sender, args) {
            validNavigation = false;
        }
        function VerificaModifiche() {
            document.getElementById('Modificato').value == '0'
            var grid = $find('<%=RadGridTipi.ClientID%>');
            if (grid.get_batchEditingManager().hasChanges(grid.get_masterTableView())) {
                document.getElementById('Modificato').value = '1'
            }
        }
        function Uscita() {
            validNavigation = true;
            location.href = 'Home.aspx';
        }
        function Conferma(sender, args) {
            VerificaModifiche();
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    Uscita();
                }
            });
            if (document.getElementById('Modificato').value == '1') {
                apriConfirm("Sono state effettuate delle modifiche. Uscire senza salvare?", callBackFunction, 300, 150, "Attenzione", null);
                args.set_cancel(true);
            }
            else {
                Uscita();
            }
        }
    </script>
</telerik:RadCodeBlock>
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" 
                    ToolTip="Esporta i risultati in Excel">
                </telerik:RadButton>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                    AutoPostBack="False" CausesValidation="False" OnClientClicking="Conferma">
                </telerik:RadButton>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">

    <telerik:RadGrid ID="RadGridTipi" runat="server" AllowPaging="True"
        PageSize="20" AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True"
        AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
        Width="97%" Culture="it-IT" GroupPanelPosition="Top" IsExporting="False" BorderWidth="0px">
        <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
            <BatchEditingSettings EditType="Row" />
            <CommandItemSettings AddNewRecordText="Aggiungi" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPOLOGIA ALLEGATO"
                    HeaderStyle-Width="100%" UniqueName="CambiaColumn" itemstyle-cssclass="maximize">
                    <ColumnValidationSettings EnableRequiredFieldValidation="true">
                        <RequiredFieldValidator ForeColor="Red" ErrorMessage="   !" ToolTip="Il campo non può essere nullo"></RequiredFieldValidator>
                    </ColumnValidationSettings>
                    <HeaderStyle Width="100%"></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn ConfirmText="Eliminare l'elemento selezionato?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                    UniqueName="DeleteColumn" ButtonType="ImageButton">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    
                </telerik:GridButtonColumn>
            </Columns>
            <PagerStyle AlwaysVisible="True"></PagerStyle>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
            <Pdf PageWidth="">
            </Pdf>
            <Excel FileExtension="xlsx" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" ClientEvents-OnBatchEditCellValueChanged="Modifica" ClientEvents-OnRowDeleted="Modifica">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" />
        
    </telerik:RadGrid>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="Modificato"/>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 200;
            var larghezzaRad = $(window).width() - 27;
            $("#MasterPage_CPContenuto_RadGridTipi").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridTipi").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }
    </script>
    <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
