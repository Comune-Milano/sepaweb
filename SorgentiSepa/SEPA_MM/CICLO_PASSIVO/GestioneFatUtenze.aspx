<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneFatUtenze.aspx.vb"
    Inherits="CICLO_PASSIVO_GestioneFatUtenze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 12pt;
            color: #990000;
        }
        .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function ApriDettaglio() {

            window.showModalDialog('DettFattUtenze.aspx?IDPF=' + document.getElementById('idPiano').value + '&IDFORN=' + document.getElementById('idFornitore').value + '&IDVOCEPF=' + document.getElementById('idVocePf').value + '&IDVOCEPFIMP=' + document.getElementById('idVocePfImporto').value + ' ', 'window', 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');

        };

        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        };
        var Selezionato;
        function ConfElimina() {
            if (document.getElementById('idPiano').value != 0) {
                var sicuro = confirm('Sei sicuro di voler eliminare il dato selezionato?');
                if (sicuro == true) {
                    document.getElementById('confElimina').value = '1';
                }
                else {
                    document.getElementById('confElimina').value = '0';
                }
            }
            else {
                alert('Selezionare una riga per eliminarla');
            }
        };

       
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvTipoUtenze">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTipoUtenze" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PanelFattUtenza">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelFattUtenza" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <div id="ProgressExport" class="ExportProgress">
        <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
        <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Language="" ProgressIndicators="FilesCountBar, CurrentFileName, TimeElapsed"
            BackColor="#CCCCCC" HeaderText="Export Excel" Skin="Web20">
            <Localization UploadedFiles="Current item" CurrentFileName="Operazione:"></Localization>
        </telerik:RadProgressArea>
    </div>
    <div style="width: 100%">
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">
                    Gestione - Parametri - CDP tracciati
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            
                            <td>
                                <telerik:RadButton ID="btnElimina" runat="server" Text="Elimina" OnClientClicking="function(sender, args){ConfElimina();}"
                                    ToolTip="Elimina il record selezionato" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%">
                        <telerik:RadGrid ID="dgvTipoUtenze" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PageSize="50" AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%"
                            AllowSorting="True" IsExporting="False" AllowPaging="True" PagerStyle-AlwaysVisible="true">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                    ShowRefreshButton="true" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id_piano_finanziario" HeaderText="id_piano_finanziario"
                                        CurrentFilterFunction="Contains" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="id_tipo_utenza" HeaderText="id_tipo_utenza" CurrentFilterFunction="Contains"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="id_voce_pf" HeaderText="id_voce_pf" CurrentFilterFunction="Contains"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="id_voce_pf_importo" HeaderText="id_voce_pf_importo"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="id_fornitore" HeaderText="id_fornitore" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="id_struttura" HeaderText="id_struttura" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="ID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_UTENZE" HeaderText="TIPO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False"  HorizontalAlign="Center"/>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="pf" HeaderText="PIANO FINANZIARIO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" HorizontalAlign="Center"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VOCE_PIANO" HeaderText="VOCE BP" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE SERVIZIO BP" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ATTIVO" HeaderText="ATTIVO" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn HeaderStyle-Width="50px" CommandName="Delete" Text="Elimina"
                                        UniqueName="DeleteColumn" ButtonType="ImageButton">
                                        <ItemStyle Width="24px" Height="24px" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <a style="cursor: pointer" onclick="openWindow(null, null, 'RadWindowFattUtenza')">
                                        <img style="border: 0px" alt="" src="Immagini/addRecord.gif" />
                                        Aggiungi nuovo record</a>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                <Excel FileExtension="xls" Format="Xlsx" />
                            </ExportSettings>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                <Selecting AllowRowSelect="True" />
                                <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                    AllowResizeToFit="true" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <telerik:RadWindow ID="RadWindowFattUtenza" runat="server" CenterIfModal="true" Modal="true"
        Title="Aggiungi Fatture Utenza" Width="630px" Height="330px" VisibleStatusbar="false"
        Behaviors="Pin, Maximize, Move, Resize">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PanelFattUtenza" Style="height: 100%;" class="sfondo">
                <table style="width: 100%;">
                    <tr>
                        <td class="TitoloModulo">
                            Dettaglio abbinamento
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnSalvaFattureUtenze" runat="server" Text="Salva" ToolTip="Salva" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right">
                                        <telerik:RadButton ID="btnChiudiRadFattureUtenze" runat="server" Text="Esci" ToolTip="Esci"
                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowFattUtenza');}" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 25%">
                                        Struttura competente*
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbStruttura" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Piano Finanziario*
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbEsercizio" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Tipo tracciato*
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbTipoTracciato" Width="90%" AppendDataBoundItems="true"
                                            Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Fornitore*
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbFornitore" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Voce BP*
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbPfVoci" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Servizio
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbServizio" Width="90%" AppendDataBoundItems="true" Filter="Contains"
                                            runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                            LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Voce servizio
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbPfVociImporto" Width="90%" AppendDataBoundItems="true"
                                            Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                            <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                            <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                            <asp:HiddenField ID="HiddenField4" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="idPiano" runat="server" Value="0" />
    <asp:HiddenField ID="idTipoUtenza" runat="server" Value="0" />
    <asp:HiddenField ID="idFornitore" runat="server" Value="0" />
    <asp:HiddenField ID="idVocePf" runat="server" Value="0" />
    <asp:HiddenField ID="idVocePfImporto" runat="server" Value="0" />
    <asp:HiddenField ID="idStruttura" runat="server" Value="0" />
    <asp:HiddenField ID="confElimina" runat="server" Value="0" />
    <asp:HiddenField ID="idSel" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
