<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Composizione.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_Composizione" %>
<table width="100%">
    <tr>
        <td style="width: 90%;">
            <div style="visibility: visible; overflow: auto; width: 100%; height: 100%">
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <telerik:RadGrid ID="DataGridComposizione" runat="server" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                        IsExporting="False" PagerStyle-AlwaysVisible="true">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="SELEZIONA" AllowFiltering="true" ShowFilterIcon="true">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                    <ItemTemplate>
                                        <telerik:RadButton ID="CheckBox1" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="false" Checked='<%# DataBinder.Eval(Container,"DataItem.CHECKED") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <div style="width: 100%; text-align: center;">
                                            <telerik:RadButton ID="chkSelTutti" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                                AutoPostBack="true" OnClientCheckedChanged="selezionaTutti" OnClick="ButtonSelAll_Click" />
                                        </div>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO" AllowFiltering="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_FILIALE" HeaderText="ID_FILIALE" Visible="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True"></PagerStyle>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <ClientEvents OnCommand="onCommand"></ClientEvents>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="false" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                    </telerik:RadGrid>
                </span></strong>
            </div>
        </td>
        <td style="text-align: left; vertical-align: top; width: 10%;">
            <table border="0" cellpadding="0" cellspacing="0" class="FontTelerik">
                <tr>
                    <td>
                        <asp:CheckBox Text="Lotto A" runat="server" ID="CheckBoxA" AutoPostBack="True" Font-Bold="True"
                            class="TitoloH1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="Lotto B" runat="server" ID="CheckBoxB" AutoPostBack="True" Font-Bold="True"
                            class="TitoloH1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="Lotto C" runat="server" ID="CheckBoxC" AutoPostBack="True" Font-Bold="True"
                            class="TitoloH1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="Lotto D" runat="server" ID="CheckBoxD" AutoPostBack="True" Font-Bold="True"
                            class="TitoloH1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox Text="Lotto Unico" runat="server" ID="CheckBoxU" AutoPostBack="True"
                            Font-Bold="True" class="TitoloH1" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top">
            <telerik:RadButton ID="btnVariazConf" runat="server" Text="Variaz.Cons.Contrattuale" AutoPostBack="false"
                OnClientClicking="function(sender, args){ApriModalVarCof();return false;}" ToolTip="Variazione Composizione" />
            
        </td>
    </tr>
</table>
<div style="display:none;visibility:hidden">
    <asp:Button Text="" runat="server" ID="ricaricaComposizione" Style="display: none" />
</div>
<asp:HiddenField ID="Selezionati" runat="server" Value="0" />
<asp:HiddenField ID="hiddenSelTutti" runat="server" Value="0" ClientIDMode="Static" />
<asp:HiddenField ID="isExporting" runat="server" Value="0" />
