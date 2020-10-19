<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResiduoConsumo.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_ResiduoConsumo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Residuo a consumo Appalto</title>
    <link rel="shortcut icon" href="../../../favicon.ico" type="image/x-icon" />
    <link rel="icon" href="../../../favicon.ico" type="image/x-icon" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";


        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';

        }

        function AutoDecimal2(obj) {
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {

                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }

            }
        }

        function CompletaData(e, obj) {
            // Check if the key is a number
            var sKeyPressed;

            sKeyPressed = (window.event) ? event.keyCode : e.which;

            if (sKeyPressed < 48 || sKeyPressed > 57) {
                if (sKeyPressed != 8 && sKeyPressed != 0) {
                    // don't insert last non-numeric character
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
            else {
                if (obj.value.length == 2) {
                    obj.value += "/";
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        // make sure the field doesn't exceed the maximum length
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
            }
        }

    </script>
    <style type="text/css">
        #form1 {
            width: 929px;
        }

        .style1 {
            font-family: Arial;
            font-size: 10pt;
        }

        .style2 {
            font-family: Arial;
            font-size: 10pt;
            font-weight: bold;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="dgvresiduo">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="dgvresiduo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <table style="width: 925px;">
            <tr>
                <td style="font-weight: 700; text-align: center; font-family: Arial; font-size: 3pt">&nbsp;</td>
            </tr>
            <tr>
                <td class="TitoloH1">Riepilogo residuo importo a consumo
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
           <tr>
                <td>
                      <asp:Button ID="btnIndietro" runat="server" Text="Esci" OnClientClick=" GetRadWindow().close();" Style="cursor: pointer;" />
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: auto; height: 332px; width: 920px;">
                        <telerik:RadGrid ID="dgvresiduo" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
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
                                            <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="nascondi=0;"
                                                CssClass="rgRefresh" />
                                            <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                                                CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="nascondi=0;" />
                                        </div>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                            Width="60px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROGR" HeaderText="PROGR"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESC_STATO" HeaderText="STATO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                            Width="200px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_TOT" HeaderText="IMPORTO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                            Width="150px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                                            Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RIT_LEGGE" HeaderText="RIT.LEGGE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                            Width="100px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right"
                                            Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FILIALE" HeaderText="SEDE T.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                            Width="200px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                        <HeaderStyle Width="500px" />
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
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td class="style2">Totale Appalto a Consumo</td>
                            <td class="style2">
                                <asp:TextBox ID="txtTotAppalto" runat="server" Font-Names="Arial"
                                    Font-Size="8pt" Style="text-align: right" Width="80px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style2">Totale Manutenzioni</td>
                            <td class="style2">
                                <asp:TextBox ID="txtTotManutenzioni" runat="server" Font-Names="Arial"
                                    Font-Size="8pt" Style="text-align: right" Width="80px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="style2">Residuo pari a
                            </td>
                            <td class="style1">
                                <asp:TextBox ID="txtResiduo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Style="text-align: right" Width="80px" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        function closeWin() {
            var window = $find('<%=dgvresiduo.ClientID %>');
            window.close();
        };
    </script>
</body>

</html>
