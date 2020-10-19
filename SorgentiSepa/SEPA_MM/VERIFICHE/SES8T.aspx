<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES8T.aspx.vb" Inherits="VERIFICHE_SES8T" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Select SISCOM_MI</title>
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <style type="text/css">
        div.RadGrid_WebBlue .rgCommandTable td
        {
            text-align: left;
            color: transparent;
            padding-left: 0;
        }
        div.RadGrid_WebBlue .rgCommandTable td:first-child
        {
            width: 0;
            padding: 0;
        }
        .RadGrid_WebBlue input.rgExpXLS
        {
            margin-left: -27px;
        }
        .style1
        {
            width: 135px;
        }
        .style2
        {
            width: 255px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 120) {
                document.getElementById('Button2').click();
                //                e.preventDefault();
            }
        }

        function $onkeydown() {
            if (event.keyCode == 120) {
                //                event.keyCode = '9';
                document.getElementById('Button2').click();

            }
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="TextBox1">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        Skin="Web20"></telerik:RadFormDecorator>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="CONFERMA" />
        &nbsp;<br />
    </div>
    <asp:TextBox ID="TextBox2" runat="server" Height="230px" TextMode="MultiLine" Visible="False"
        Width="100%"></asp:TextBox>
    <br />
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <asp:Button ID="Button2" runat="server" Text="ESEGUI" Visible="False" />
            </td>
            <td class="style2">
                <asp:Button ID="btnExport" runat="server" Text="ELIMINA PAGINAZIONE" Visible="False" />
            </td>
            <td>
                <asp:Button ID="btnExport1" runat="server" Text="Esporta con paginazione" Visible="False" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td class="style2">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <td style="width: 100%">
                <telerik:RadGrid ID="dgvDocumenti" runat="server" AllowPaging="True" Culture="it-IT"
                    GroupPanelPosition="Top" IsExporting="False" PageSize="300" 
                    Visible="False" Height="50%"
                    Skin="WebBlue" AllowFilteringByColumn="True" AllowSorting="True" 
                    ShowGroupPanel="True">
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                    <ExportSettings ExportOnlyData="True" FileName="GridExport" HideStructureColumns="True"
                        IgnorePaging="True" OpenInNewWindow="True">
                        <Pdf PageWidth="">
                        </Pdf>
                        <Excel Format="Biff" />
                        <Csv ColumnDelimiter="Semicolon" />
                    </ExportSettings>
                    <MasterTableView CommandItemDisplay="Top" CommandItemStyle-Width="500px">
                        <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="True"
                            ShowExportToExcelButton="True" ShowRefreshButton="False" />
                        <PagerStyle AlwaysVisible="True" Mode="NextPrevNumericAndAdvanced" 
                            Position="TopAndBottom"></PagerStyle>
                        <CommandItemStyle Width="500px"></CommandItemStyle>
                    </MasterTableView>
                    <ClientSettings AllowColumnsReorder="True" AllowDragToGroup="True" 
                        ReorderColumnsOnClient="True">
                        <Scrolling UseStaticHeaders="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" Mode="NextPrevNumericAndAdvanced" 
                        Position="TopAndBottom" />
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    </form>
</body>
</html>

