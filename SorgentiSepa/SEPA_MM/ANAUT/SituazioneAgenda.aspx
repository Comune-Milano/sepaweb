<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneAgenda.aspx.vb"
    Inherits="ANAUT_SituazioneAgenda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Situazione Agenda</title>
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table width="97%">
            <tr>
                <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt;
                    font-weight: bold; text-align: center;">
                    SITUAZIONE APPUNTAMENTI &nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt;
                    font-weight: bold; text-align: center;">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt;
                    font-weight: bold; text-align: left;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <div id="divOverContentRisultatiOn" style="overflow: auto; width: 100%">
                        <telerik:RadGrid ID="dgvDocumenti" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                            ResolvedRenderMode="Classic" ShowGroupPanel="True" AutoGenerateColumns="False"
                            PageSize="300" Culture="it-IT" RegisterWithScriptManager="False" AllowPaging="True"
                            IsExporting="False" Width="95%" Height="95%" AllowFilteringByColumn="True">
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
                            <ExportSettings FileName="ExportAgenda_" IgnorePaging="True" OpenInNewWindow="True"
                                ExportOnlyData="True" HideStructureColumns="True">
                                <Pdf PageWidth="">
                                </Pdf>
                                <Excel Format="Biff" />
                                <Csv ColumnDelimiter="Semicolon" EncloseDataWithQuotes="False" />
                            </ExportSettings>
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="True"></Selecting>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                    ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                            </ClientSettings>
                            <MasterTableView CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                                    ShowRefreshButton="False" ShowExportToExcelButton="True" ShowExportToCsvButton="True" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA" HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_CONVOCAZIONE" HeaderText="CONVOC. NUM."
                                        HeaderStyle-Width="5%">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataType="System.String" DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO" HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataType="System.String" DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO CONTRATTO" HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_APPUNTAMENTO" HeaderText="DATA APPUNTAMENTO"
                                        HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ORE_APPUNTAMENTO" HeaderText="ORE APP."
                                        HeaderStyle-Width="5%">
                                        <HeaderStyle Width="5%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO APP." HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MOTIVO_SOSP" HeaderText="MOTIVO SOSP." HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO_SCHEDA_AU" HeaderText="STATO AU" HeaderStyle-Width="10%">
                                        <HeaderStyle Width="10%"></HeaderStyle>
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True" />
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
    </div>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var altezzaRad = $(window).height() - 205;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
            $("#divOverContentRisultatiOn").height(altezzaRad);
        });
        $(window).resize(function () {
            var altezzaRad = $(window).height() - 205;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
            $("#divOverContentRisultatiOn").height(altezzaRad);
        });
        
    </script>
</body>
</html>
