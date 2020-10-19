<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportOfferteAbbinamenti.aspx.vb" Inherits="Ass_ReportOfferteAbbinamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Offerte/Abbinamenti</title>
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


        <asp:Panel runat="server" ID="Panel1">

            <telerik:RadScriptBlock ID="RadScriptBlock" runat="server">
                <script type="text/javascript">
                    $(document).ready(function () {
                        var altezzaRad = $(window).height() - 230;
                        document.getElementById('AltezzaRadGrid').value = altezzaRad;
                        $("#divOverContentRisultatiOn").height(altezzaRad);
                    });
                    $(window).resize(function () {
                        var altezzaRad = $(window).height() - 230;
                        document.getElementById('AltezzaRadGrid').value = altezzaRad;
                        $("#divOverContentRisultatiOn").height(altezzaRad);
                    });
                </script>
            </telerik:RadScriptBlock>

            <table width="99%" style="table-layout: fixed;">
                <tr>
                    <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">Report Offerte/Abbinamenti &nbsp;</td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Data Presentazione Dal:" Font-Names="arial" Font-Size="8" Width="120px"></asp:Label>
                        <telerik:RadDatePicker ID="txtRicDA" runat="server" WrapperTableCaption="" MinDate="01/01/1900" MaxDate="01/01/9999"
                            Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                            <DateInput ID="DateInput7" runat="server">
                            </DateInput>
                            <Calendar ID="Calendar1" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>

                        <asp:Label ID="Label1" runat="server" Text="Al:" Font-Names="arial" Font-Size="8"></asp:Label>
                        <telerik:RadDatePicker ID="txtRicA" runat="server" WrapperTableCaption="01/01/1900" MaxDate="01/01/9999"
                            Skin="Web20" DataFormatString="{0:dd/MM/yyyy}" Width="120px">
                            <DateInput ID="DateInput1" runat="server">
                            </DateInput>
                            <Calendar ID="Calendar2" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>


                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Nome Bando:" Font-Names="arial" Font-Size="8" Width="120px"></asp:Label>
                        <telerik:RadComboBox ID="cmbBando" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="False" Width="400px">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Tipo Bando:" Font-Names="arial" Font-Size="8" Width="120px"></asp:Label>
                        <telerik:RadComboBox ID="cmbTipoBando" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="False" Width="400px">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Stato Domanda:" Font-Names="arial" Font-Size="8" Width="120px"></asp:Label>
                        <telerik:RadComboBox ID="cmbStatoDomanda" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="False" Width="400px">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Esito Offerta:" Font-Names="arial" Font-Size="8" Width="120px"></asp:Label>
                        <telerik:RadComboBox ID="cmbEsito" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="False" Width="400px">
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td align="center" style="font-family: ARIAL, Helvetica, sans-serif; font-size: 12pt; font-weight: bold; text-align: center;">&nbsp; &nbsp;
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
                        <asp:Panel runat="server" ID="divOverContentRisultatiOn" Style="overflow: auto; width: 100%">
                            <telerik:RadGrid ID="dgvDocumenti" runat="server" AllowSorting="True"
                                ResolvedRenderMode="Classic" ShowGroupPanel="false" AutoGenerateColumns="False"
                                PageSize="300" Culture="it-IT" RegisterWithScriptManager="False" AllowPaging="True"
                                IsExporting="False" Width="99%" Height="99%" AllowFilteringByColumn="True">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
                                <ExportSettings FileName="Export_" IgnorePaging="True" OpenInNewWindow="True" ExportOnlyData="True">
                                    <Excel Format="Biff" />
                                    <Csv ColumnDelimiter="Semicolon" EncloseDataWithQuotes="False" />
                                </ExportSettings>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                        ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" AllowSorting="True">
                                    <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                                        ShowRefreshButton="true" ShowExportToExcelButton="True" ShowExportToCsvButton="True" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="NUM_OFFERTA" HeaderText="NUM. OFFERTA" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PG_DOMANDA" HeaderText="NUM. DOMANDA" AutoPostBackOnFilter="true">
                                            <HeaderStyle Width="0px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PG_DOMANDA1" HeaderText="NUM. DOMANDA" AutoPostBackOnFilter="true" Exportable="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOME_BANDO" HeaderText="NOME BANDO" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO_BANDO" HeaderText="TIPO BANDO" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CF_RICHIEDENTE" HeaderText="CF RICHIEDENTE" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UNITA_IMMOBILIARE" HeaderText="UNITA IMMOBILIARE" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NUM_CIVICO" HeaderText="NUM CIVICO" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridDateTimeColumn DataField="DATA_PROPOSTA" HeaderText="DATA EVENTO"
                                            PickerType="DatePicker" EnableTimeIndependentFiltering="true"
                                            DataFormatString="{0:dd/MM/yyyy}"
                                            Visible="true" Exportable="true">
                                        </telerik:GridDateTimeColumn>
                                        <telerik:GridBoundColumn DataField="ESITO" HeaderText="ESITO" AutoPostBackOnFilter="true">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MOTIVAZIONE_ALL" HeaderText="MOTIVAZIONE_ALL" AutoPostBackOnFilter="true" Visible="False">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <SortExpressions>
                                        <telerik:GridSortExpression FieldName="CF_RICHIEDENTE" SortOrder="Ascending" />
                                    </SortExpressions>
                                    <PagerStyle AlwaysVisible="True" />
                                </MasterTableView>
                                <ClientSettings AllowDragToGroup="True">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="True" />
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>


        <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
</body>
</html>
