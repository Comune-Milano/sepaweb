<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiLotti.aspx.vb"
    Inherits="MANUTENZIONI_RisultatiLotti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RISULTATI RICERCA</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        var Selezionato;
    </script>
</head>
<body class="sfondo">
    <form id="form2" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                    Gestione - Gestione lotti - Ricerca
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnVisualizza" runat="server" Text="Visualizza" ToolTip="Visualizza" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca"
                                    Style="top: 0px; left: 0px" />
                            </td>
                            <td>
                                <telerik:RadButton ID="RadButton3" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%; overflow: auto">
                        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
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
                                                CssClass="rgRefresh" />
                                            <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_ESERCIZIO_FINANZIARIO" HeaderText="ESERCIZIO FINANZIARIO"
                                        FilterControlWidth="85%" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                        DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="SEDE TERRITORIALE" DataField="ID_FILIALE" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_COMPLESSO" HeaderText="COMPLESSO" Visible="False">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" FilterControlWidth="85%"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="APPALTO" Visible="False">
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
            <tr>
                <td>
                    <asp:Label ID="LBLID" runat="server" Height="21px" Visible="False" Width="78px">Label</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Width="100%" Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100"
                        CssClass="txtMia" ReadOnly="True">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtid" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        MaxLength="100" Style="visibility: hidden"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtdesc" runat="server" BackColor="#F2F5F1" BorderColor="White"
                        BorderStyle="None" MaxLength="100" Style="visibility: hidden" Height="8px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="isExporting" runat="server" Value="0" />
    <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
