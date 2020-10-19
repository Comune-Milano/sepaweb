<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="Edifici_Condominio.aspx.vb" Inherits="SPESE_REVERSIBILI_Edifici_Condominio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;

        function selezionaCheckSingolo(id) {
            //alert(itemindex + " flag:" + flanalisicorpo.toString() + " perc:" + perccorpo.toString());
            var s = document.getElementById("HiddenIdEdificio").value;

            var idConfronto = "ID = " + id.toString();
            if (s.indexOf(idConfronto) !== -1) {
                s = s.replace("(ID = " + id.toString() + ") OR ", "");
            } else {
                s += "(ID = " + id + ") OR ";
            }
            //stringa dovrebbe esse 'ID = 1 OR ID = 2 OR etc' 
            document.getElementById("HiddenIdEdificio").value = s;
            //alert(s);
        };


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvEdifici">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvEdifici" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager> 
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla" />
    <asp:Panel runat="server" ID="PanelEdifici">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3">
                    <table border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="vertical-align: middle; text-align: center">
                                <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Per tutti gli edifici selezionati in questa tabella non verrà effettuata nessuna ripartizione spese.</i></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <telerik:RadGrid ID="dgvEdifici" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
            PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
            EnableLinqExpressions="False" Width="97%" AllowSorting="True" IsExporting="False"
            PageSize="100">
            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                DataKeyNames="ID" CommandItemDisplay="Top">
                <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                    ShowRefreshButton="true" />
                <Columns>
                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" AutoPostBackOnFilter="true"
                        FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IN_CONDOMINIO" HeaderText="IN CONDOMINIO"  Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn UniqueName="ClientSelectColumn" AllowFiltering="false">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECK1") %>' />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="headerChkbox_CheckedChanged" AutoPostBack="true" />
                        </HeaderTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
                <CommandItemTemplate>
                    <div style="display: inline-block; width: 100%;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <div style="float: right; padding: 4px;">
                                        <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                                            CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClientClick="caricamento(2);"
                                                OnClick="Esporta_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </CommandItemTemplate>
            </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                <Excel FileExtension="xls" Format="Xlsx" />
            </ExportSettings>
            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                <Selecting AllowRowSelect="True" />
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                    AllowResizeToFit="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="True" />
        </telerik:RadGrid>
    </asp:Panel>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="350" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenIdEdificio" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddenSelTutti" runat="server" Value="0" ClientIDMode="Static" />
    <div style="display: none">
        <asp:Button ID="btnSelTutti" runat="server" Text="Salva" ToolTip="Salva" ClientIDMode="Static" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" />
    <asp:Button ID="Button1" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
