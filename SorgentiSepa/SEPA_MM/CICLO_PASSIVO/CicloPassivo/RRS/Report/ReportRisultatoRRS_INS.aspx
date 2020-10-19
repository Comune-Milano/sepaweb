<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportRisultatoRRS_INS.aspx.vb"
    Inherits="ReportRisultatoRRS_INS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
    <script language="javascript" type="text/javascript">
        // <!CDATA[



        // ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td>
                    <img src="../../../../IMG/logo.gif" alt="logo" style="z-index: 100; left: 0px; position: static;
                        top: 0px" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 21px">
                    <span style="font-size: 10pt"><strong>Settore Manutenzioni</strong></span>
                </td>
                <td style="height: 21px">
                </td>
                <td style="height: 21px">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <table width="100%">
        <tr>
            <td style="text-align: center" width="100%">
                <strong><span style="font-size: 14pt">
                    <asp:Label ID="lblTitolo" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px; font-size: 11pt; font-family: Arial;" Text="Label" Width="100%" BorderStyle="Solid"
                        BorderWidth="1px"></asp:Label></span></strong>
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%">
        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
            IsExporting="False" AllowPaging="True" PagerStyle-AlwaysVisible="true">
            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                <Columns>
                    <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_PF_VOCE" HeaderText="ID_PF_VOCE" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ID_COMPLESSO" HeaderText="ID_COMPLESSO" Visible="False">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO">
                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="7%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DESCRIZIONE_APPALTI" HeaderText="DESCRIZIONE">
                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="COD_VOCE" HeaderText="COD. VOCE P.F.">
                        <HeaderStyle Width="10%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VOCE" HeaderText="VOCE P.F.">
                        <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" Width="20%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="COMPLESSO" HeaderText="COMPLESSO">
                        <HeaderStyle Width="17%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DESC_EDIFICIO" HeaderText="DESCRIZIONE EDIFICIO">
                        <HeaderStyle Width="20%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO EDIFICIO">
                        <HeaderStyle Width="25%" Font-Bold="false" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
            <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                <Excel FileExtension="xls" Format="Xlsx" />
            </ExportSettings>
            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                <Selecting AllowRowSelect="True" />
                <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                    AllowResizeToFit="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </asp:Panel>
    <table width="100%">
        <tr>
            <td style="text-align: center; height: 26px;" width="100%">
                <strong><span style="font-size: 14pt">
                    <asp:Label ID="lblTotale" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="font-size: 11pt;
                        z-index: 100; left: 0px; width: 100%; font-family: Arial; position: static; top: 0px;
                        text-align: left" Text="Label" Width="736px"></asp:Label></span></strong>
            </td>
        </tr>
    </table>
    <br />
    </form>
</body>
</html>
