<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiManutenzioniSTR.aspx.vb"
    Inherits="MANUTENZIONI_RisultatiManutenzioniSTR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Risultati ricerca manutenzioni</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                          <telerik:AjaxSetting AjaxControlID="imgExport">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">Manutenzioni e servizi - STR - Export STR
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="imgExport" runat="server" Text="Export in excel" OnClientClicking ="function (sender,args){nascondi=0;}"
                                    ToolTip="Export Excel" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnRicerca" runat="server" Text="Nuova ricerca" ToolTip="Nuova Ricerca"
                                    Style="top: 0px; left: -1px" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <telerik:RadGrid ID="DataGrid1" runat="server" AllowSorting="True" Width="98%"
                        AutoGenerateColumns="False" Culture="it-IT">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID_MANUTENZIONE" HeaderText="ID_MANUTENZIONE" UniqueName="idmanutenzione"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <HeaderTemplate>
                                        <telerik:RadButton ID="chkSelTutti" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            AutoPostBack="true" OnClientCheckedChanged="function (sender,args){nascondi=0;}" OnClick="CheckBoxTutti_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ODL_ANNO" HeaderText="ODL/ANNO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_INIZIO_ORDINE" HeaderText="DATA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="UBICAZIONE" HeaderText="UBICAZIONE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_PRESUNTO" HeaderText="IMPORTO PRESUNTO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_PRE" HeaderText="IMPORTO PREVENTIVO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_CON" HeaderText="IMPORTO CONSUNTIVO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_MANUTENZIONE" HeaderText="ID_MANUTENZIONE"
                                    Display="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True"></PagerStyle>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="4"></Scrolling>
                        </ClientSettings>
                    </telerik:RadGrid>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Names="Arial" Font-Size="12pt" MaxLength="100" Width="768px" ReadOnly="True"
                        CssClass="txtMia" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
                        Style="visibility: hidden" MaxLength="100" Width="152px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="isExporting" runat="server" Value="0" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <script type="text/javascript" language="javascript">
            window.onresize = setDimensioni;
            Sys.Application.add_load(setDimensioni);
        </script>
    </form>
</body>
</html>
