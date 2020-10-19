<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Eventi.aspx.vb" Inherits="FSA_Eventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Eventi</title>
    <link href="../../../CicloPassivo.css" rel="stylesheet" type="text/css" />
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <script type="text/javascript">
            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);
            function setDimensioni() {
                var griglie = document.getElementById('HFGriglia').value;
                var altezzaPagina = myHeight = window.innerHeight;
                if (document.getElementById('MyTab')) {
                    var tabs = document.getElementById('HFTAB').value;
                    var tab = tabs.split(",");
                    if (tab.length != 0) {
                        for (i = 0; i < tab.length; i++) {
                            document.getElementById(tab[i]).style.height = altezzaPagina - 420 + 'px';
                        };
                    };
                }
                if (griglie != '') {
                    var griglia = griglie.split(",");
                    if (document.getElementById('MyTab')) {
                        //Griglie nei tab (Nei tab va definito sempre il div MyTab)
                        for (i = 0; i < griglia.length; i++) {
                            document.getElementById(griglia[i]).style.height = altezzaPagina - 450 + 'px';
                        }
                    } else {
                        //Griglie fuori dai tab
                        for (i = 0; i < griglia.length; i++) {
                            document.getElementById(griglia[i]).style.height = altezzaPagina - 100 + 'px';
                        }
                    };
                }
            };
        </script>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="1">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="height: 7px;"></td>
            </tr>
            <tr>
                <td class="TitoloModulo">Eventi
                <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnEsci" runat="server" Text="Esci" OnClientClick="self.close();" Style="cursor: pointer" />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="PanelRadGrid">
                        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PagerStyle-AlwaysVisible="true" AllowFilteringByColumn="false" EnableLinqExpressions="False"
                            Width="100%" AllowSorting="True" Height="440px" IsExporting="False" AllowPaging="True"
                            PageSize="100">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top" Width="100%">
                                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                    ShowRefreshButton="true" />
                                <CommandItemTemplate>
                                    <div style="display: inline-block; width: 100%;">
                                        <div style="float: right; padding: 4px;">
                                            <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                                                CssClass="rgRefresh" Style="display: none" />
                                            <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                CommandName="ExportToExcel" CssClass="rgExpXLS" Style="display: none" />
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA ORA">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COD_EVENTO" HeaderText="COD_EVENTO" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_OPERATORE" HeaderText="ID_OPERATORE" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="COD_CAF" HeaderText="ENTE">
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                            <ExportSettings OpenInNewWindow="true" IgnorePaging="true" ExportOnlyData="true"
                                HideStructureColumns="true">
                                <Excel FileExtension="xlsx" Format="Xlsx" />
                            </ExportSettings>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
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
    </form>
</body>
</html>
