<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FattureCaricamentoNew.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCaricamentoNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fatture Utenze</title>
    <script language="javascript" type="text/javascript">
        var Selezionato;

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
        function ApriAnomalie() {

            window.open('FattureCaricAnomlie.aspx;');
        }
    </script>
    <script type="text/javascript" src="../../CicloPassivo.js"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvTipoUtenze">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvTipoUtenze" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table width="98%" class="FontTelerik">
        <tr>
            <td style="width: 100%" class="TitoloModulo">
                <asp:Label ID="lblTitolo" runat="server" Text="Fatture Utenze"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <telerik:RadButton ID="RadButtonCarica" runat="server" Text="Carica Fatture">
                            </telerik:RadButton>
                        </td>
                        <td>
                            <telerik:RadButton ID="RadButtonEsci" runat="server" Text="Esci">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label Text="Esercizio Finanziario" runat="server" Font-Size="9pt" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbEsercizio" AppendDataBoundItems="true" Filter="Contains"
                                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                LoadingMessage="Caricamento..." Width="300px">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" Text="Data Caricamento" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataCaricamento" runat="server" Width="80px" Wrap="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" Style="background: white" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel runat="server" ID="PanelRadGrid">
                                <telerik:RadGrid ID="dgvTipoUtenze" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                    PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                                    AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                    PageSize="100" IsExporting="False">
                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                        CommandItemDisplay="Top">
                                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                            ShowRefreshButton="true" />
                                        <CommandItemTemplate>
                                            <div style="display: inline-block; width: 100%;">
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                        CssClass="rgRefresh" OnClientClick="nascondi=0;" />
                                                    <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                        CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                                </div>
                                            </div>
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="id_piano_finanziario" HeaderText="id_piano_finanziario"
                                                Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="id_tipo_utenza" HeaderText="id_tipo_utenza" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="id_voce_pf" HeaderText="id_voce_pf" Visible="False">
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
                                            <telerik:GridBoundColumn DataField="pf" HeaderText="PIANO FINANZIARIO" FilterControlWidth="85%"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPO_UTENZE" HeaderText="TIPO UTENZA" FilterControlWidth="85%"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="VOCE_PIANO" HeaderText="VOCE BP" FilterControlWidth="85%"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="SERVIZIO BP" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE" FilterControlWidth="85%"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA" FilterControlWidth="85%"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                        <Excel FileExtension="xls" Format="Xlsx" />
                                    </ExportSettings>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                                        ClientEvents-OnCommand="onCommand">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True" />
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                            AllowResizeToFit="true" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        <div>
            <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
                EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="100" Position="BottomRight"
                OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
            </telerik:RadNotification>
        </div>
    <asp:HiddenField ID="idParam" runat="server" Value="0" />
    <asp:HiddenField ID="idTipo" runat="server" Value="0" />
    <asp:HiddenField ID="idPiano" runat="server" Value="0" />
    <asp:HiddenField ID="idTipoUtenza" runat="server" Value="0" />
    <asp:HiddenField ID="idFornitore" runat="server" Value="0" />
    <asp:HiddenField ID="idVocePf" runat="server" Value="0" />
    <asp:HiddenField ID="idVocePfImporto" runat="server" Value="0" />
    <asp:HiddenField ID="idStruttura" runat="server" Value="0" />
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
    </form>
</body>
</html>
