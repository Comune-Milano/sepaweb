<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EventiInterventi.aspx.vb"
    Inherits="FORNITORI_EventiInterventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Eventi Intervento</title>
    <%-- <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var uploadedFilesCount = 0;
            var isEditMode;
            function validateRadUpload(source, e) {

                if (isEditMode == null || isEditMode == undefined) {
                    e.IsValid = false;

                    if (uploadedFilesCount > 0) {
                        e.IsValid = true;
                    }
                }
                isEditMode = null;
            }

            function OnClientFileUploaded(sender, eventArgs) {
                uploadedFilesCount++;
            }
 
        </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="divGenerale">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divGenerale" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <asp:Panel runat="server" ID="divGenerale">
        <table width="100%">
            <tr>
                <td width="1%">
                </td>
                <td width="98%">
                    <telerik:RadGrid ID="dgvEventi" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                         AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowPaging="True" IsExporting="False" Height="200px" Skin="Web20" AllowFilteringByColumn="True"
                        PageSize="100">
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ExportSettings FileName="ExportInterventi" IgnorePaging="True" OpenInNewWindow="True"
                            ExportOnlyData="True" HideStructureColumns="True">
                            <Pdf PageWidth="">
                            </Pdf>
                            <Excel Format="Xlsx" />
                            <Csv EncloseDataWithQuotes="False" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            <Selecting AllowRowSelect="True"></Selecting>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" HierarchyLoadMode="Client">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                                ShowRefreshButton="False" ShowExportToCsvButton="True" ShowExportToExcelButton="True" />
                            <RowIndicatorColumn Visible="True">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn Created="True">
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="DATA_EVENTO" HeaderText="DATA" HeaderStyle-Width="10%">
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EVENTO" HeaderText="EVENTO" HeaderStyle-Width="10%">
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE" HeaderStyle-Width="70%">
                                    <HeaderStyle Width="70%"></HeaderStyle>
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" HeaderStyle-Width="10%">
                                    <HeaderStyle Width="10%"></HeaderStyle>
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True" />
                        </MasterTableView><ClientSettings AllowDragToGroup="True">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" EnableRealTimeResize="true"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="True" />
                    </telerik:RadGrid>
                </td>
                <td width="1%">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <script type="text/javascript">
        validNavigation = false;
        Ridimensiona();
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 100;
            var larghezzaRad = $(window).width() - 27;
            //$("#MasterPage_CPContenuto_dgvSegnalazioni").width(larghezzaRad);
            $("#dgvEventi").height(altezzaRad);

        }
        
    </script>
    </form>
</body>
</html>
