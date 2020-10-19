<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VariazConfig.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_VariazConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <table width="98%">
            <tr>
                <td class="TitoloH1" colspan="2">Variazione configurazione
                </td>
            </tr>
            <tr align="right">
                <td style="text-align: right; vertical-align: top" colspan="2">
                    <table>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnSeleziona0" runat="server" Text="Salva"
                                    TabIndex="11" ToolTip="Seleziona/Deseleziona tutto" />
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnExit" runat="server" Text="Esci"
                                    OnClientClick="CancelEdit();return false;" TabIndex="11" ToolTip="Seleziona/Deseleziona tutto" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top; width: 80%">

                    <telerik:RadGrid ID="DataGridComposizione" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="false"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                        IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="false" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="false" />

                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="SELEZIONA">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSeleziona" runat="server" TabIndex="-1" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECKED") %>' />
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="EDIFICIO"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="SELEZIONATO" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CHECKED") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CHECKED") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ID_FILIALE" HeaderText="ID_FILIALE" Visible="false">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="false" />
                        </ClientSettings>
                    </telerik:RadGrid>

                </td>
                <td style="text-align: left; vertical-align: top; width: 20%">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:CheckBox Text="Lotto A" runat="server" ID="CheckBoxA" AutoPostBack="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox Text="Lotto B" runat="server" ID="CheckBoxB" AutoPostBack="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox Text="Lotto C" runat="server" ID="CheckBoxC" AutoPostBack="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox Text="Lotto D" runat="server" ID="CheckBoxD" AutoPostBack="True" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="9pt" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox Text="Lotto Unico" runat="server" ID="CheckBoxU" AutoPostBack="True"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="9pt" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top" colspan="2">
                    <%--<asp:ImageButton ID="btnSeleziona" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/APPALTI/Immagini/img_Sel_Tutti.png"
                    TabIndex="11" ToolTip="Seleziona/Deseleziona tutto" />--%>
                </td>
            </tr>

        </table>
        <asp:HiddenField ID="HFAltezzaSottratta" runat="server" Value="200" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:HiddenField ID="Selezionati" runat="server" Value="0" />
        <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Text="Label" Visible="False"></asp:Label>
    </form>
    <script language="javascript" type="text/javascript">
        function CloseAndRefresh(args) {
            GetRadWindow().BrowserWindow.refreshPageComp(args);
            GetRadWindow().close();
        };
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };
    </script>
</body>
</html>
