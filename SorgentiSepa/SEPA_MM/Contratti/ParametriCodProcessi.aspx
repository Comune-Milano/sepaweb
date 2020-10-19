<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriCodProcessi.aspx.vb"
    Inherits="Contratti_ParametriCodProcessi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Caricamento massivo voci</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .bottone2
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
        
        .bottone2:hover
        {
            background-color: #FFF5D3;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
    </style>
    <script type="text/javascript">
        function Modifica(sender, args) {
            validNavigation = false;
        }
    </script>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            <asp:ScriptReference Path="../Standard/Scripts/jsFunzioni.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <asp:Panel runat="server" ID="Panel1">
        <div>
            <table>
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp&nbsp Anagrafica
                            Processi </strong></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-left:10px;">
            <table>
                <tr>
                    <td>
                        <div style="width: 770px;">
                            <telerik:RadGrid ID="RadGridProcessi" runat="server" AllowPaging="True" PageSize="20"
                                AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                 AllowAutomaticUpdates="True" Width="100%" Culture="it-IT"
                                GroupPanelPosition="Top" IsExporting="False" BorderWidth="0px">
                                <MasterTableView CommandItemDisplay="Top" EditMode="Batch" runat="server">
                                    <BatchEditingSettings EditType="Cell" />
                                    <CommandItemSettings ShowAddNewRecordButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPOLOGIA PROCESSO"
                                            HeaderStyle-Width="100%" UniqueName="CambiaColumn" ItemStyle-CssClass="maximize">
                                            <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                <RequiredFieldValidator ForeColor="Red" ErrorMessage="   !" ToolTip="Il campo non può essere nullo"></RequiredFieldValidator>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100%"></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="COD_PROCESSO_KOFAX" HeaderText="CODICE PROCESSO"
                                            HeaderStyle-Width="100%" UniqueName="CambiaColumn2" ItemStyle-CssClass="maximize">
                                            <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                <RequiredFieldValidator ForeColor="Red" ErrorMessage="   !" ToolTip="Il campo non può essere nullo"></RequiredFieldValidator>
                                            </ColumnValidationSettings>
                                            <HeaderStyle Width="100%"></HeaderStyle>
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <PagerStyle AlwaysVisible="True" />
                            </telerik:RadGrid>
                        </div>
                    </td>
                </tr>
                <tr><td>&nbsp</td></tr>
                <tr><td>&nbsp</td></tr>
                <tr>
                    <td align="right">
                        <div>
                            <asp:Button ID="btnHome" runat="server" CssClass="bottone2" Text="Home" CausesValidation="false"
                                ToolTip="Home" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="PanelHidden">
        <asp:HiddenField ID="idDichiarazione" runat="server" Value="0" />
        <asp:HiddenField ID="codUnita" runat="server" Value="0" />
    </asp:Panel>
    </form>
</body>
</html>
