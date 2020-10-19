<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneAE.aspx.vb" Inherits="Contratti_SituazioneAE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Situazione Registrazione A.E.</title>
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnAvviaRicerca">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="dgvDocumenti">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel2"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>


    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Transparency="70">
        </telerik:RadAjaxLoadingPanel>



        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>

    <asp:panel runat="server" id="Panel1">
            <telerik:RadScriptBlock ID="RadScriptBlock" runat="server">
                <script type="text/javascript">
                    $(document).ready(function () {
                        var altezzaRad = $(window).height() - 175;
                        document.getElementById('AltezzaRadGrid').value = altezzaRad;
                        $("#divOverContentRisultatiOn").height(altezzaRad);
                    });
                    $(window).resize(function () {
                        var altezzaRad = $(window).height() - 175;
                        document.getElementById('AltezzaRadGrid').value = altezzaRad;
                        $("#divOverContentRisultatiOn").height(altezzaRad);
                    });
                </script>
            </telerik:RadScriptBlock>

            <table width="97%">
                <tr>
                    <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">SITUAZIONE REGISTRAZIONE A.E. &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">&nbsp; &nbsp;
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Anno:" Font-Names="arial" Font-Size="8" Width="80px"></asp:Label>
                        <telerik:RadComboBox ID="cmbAnno" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="False" Width="150px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Mese:" Font-Names="arial" Font-Size="8" Width="80px"></asp:Label>
                        <telerik:RadComboBox ID="cmbMese" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="False" Width="150px">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: left;">&nbsp;
                    </td>
                </tr>

                <tr>
                    <td>
                        <telerik:RadButton ID="btnAvviaRicerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia la ricerca in base ai filtri impostati"
                            AutoPostBack="True" CausesValidation="True" ClientIDMode="Static">
                        </telerik:RadButton>
                    </td>
                </tr>

                <tr>
                    <td style="width: 100%">
                        <div id="divOverContentRisultatiOn" style="overflow: auto; width: 100%">
                            <telerik:RadGrid ID="dgvDocumenti" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                ResolvedRenderMode="Classic" ShowGroupPanel="False" AutoGenerateColumns="False"
                                PageSize="300" Culture="it-IT" RegisterWithScriptManager="False" AllowPaging="True"
                                IsExporting="False" Width="99%" Height="95%" AllowFilteringByColumn="True">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
                                <ExportSettings FileName="ExportSituazioneAE" IgnorePaging="True" OpenInNewWindow="True"
                                    ExportOnlyData="True" HideStructureColumns="True">
                                    <Pdf PageWidth="">
                                    </Pdf>
                                    <Excel Format="Biff" />
                                    <Csv ColumnDelimiter="Semicolon" EncloseDataWithQuotes="False" />
                                </ExportSettings>

                                <MasterTableView CommandItemDisplay="Top">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                    <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                                        ShowRefreshButton="True" ShowExportToExcelButton="True" ShowExportToCsvButton="True" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="STATO" HeaderText="STATO" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                            <HeaderStyle Width="30%" />
                                            <ItemStyle Width="30%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="SEDE_TERR" HeaderText="SEDE TERRITORIALE" FilterControlWidth ="80%">
                                            <HeaderStyle Width="30%" />
                                            <ItemStyle Width="30%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="INTESTATARIO" HeaderText="INTESTATARIO" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="TIPO_MOVIMENTO" HeaderText="TIPO MOVIMENTO" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_GENERAZIONE" HeaderText="DATA GENERAZIONE" PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                            DataFormatString="{0:dd/MM/yyyy HH:mm}" CurrentFilterFunction="EqualTo" ShowFilterIcon="True"
                                            AutoPostBackOnFilter="true" Visible="true" Exportable="true" FilterControlWidth ="80%">
                                            <HeaderStyle Width="30%" />
                                            <ItemStyle Width="30%" />
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="COD_TRIBUTO" HeaderText="COD. TRIBUTO" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="IMPORTO_CANONE" HeaderText="IMPORTO CANONE" DecimalDigits="2"
                                            DataType="System.Decimal" DataFormatString="{0:###,##0.00}" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="IMP_BOLLO" HeaderText="IMPOSTA BOLLO" DecimalDigits="2"
                                            DataType="System.Decimal" DataFormatString="{0:###,##0.00}" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridNumericColumn DataField="IMPORTO_REGISTRO" HeaderText="IMPOSTA REGISTRO" DecimalDigits="2"
                                            DataType="System.Decimal" DataFormatString="{0:###,##0.00}" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataField="GIORNI_SANZIONE" HeaderText="GIORNI SANZIONE" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridNumericColumn DataField="IMPORTO_INTERESSI" HeaderText="IMPORTO INTERESSI" DecimalDigits="2"
                                            DataType="System.Decimal" DataFormatString="{0:###,##0.00}" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridNumericColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="REGISTRO_RIBALTATO_BOLLETTA" HeaderText="REGISTRO RIBALTATO BOLLETTA" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataType="System.String" DataField="BOLLO_RIBALTATO_BOLLETTA" HeaderText="BOLLO RIBALTATO BOLLETTA" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MODALITA" HeaderText="MODALITA" FilterControlWidth ="80%">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FILE_SCARICATO" HeaderText="FILE SCARICATO" FilterControlWidth ="80%">
                                            <HeaderStyle Width="45%" />
                                            <ItemStyle Width="45%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOTE" HeaderText="NOTE" FilterControlWidth ="80%" ItemStyle-Wrap="true">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings AllowDragToGroup="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="True" />
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
            </table>
    </asp:panel>
        <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
    <script type="text/javascript" language="javascript">

    </script>
</body>
</html>
