<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiSituazioneContabileGeneraleSintesi.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabileGeneraleSintesi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Situazione Contabile Generale Sintesi</title>
    <style type="text/css">
        .RadButton_Default.rbSkinnedButton {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            color: #333
        }

        .RadButton_Default.rbSkinnedButton {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            color: #333
        }

        .RadButton_Default {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 12px
        }

        .rbSkinnedButton {
            padding-left: 4px
        }

        .RadButton {
            cursor: pointer
        }

        .rbSkinnedButton {
            vertical-align: middle
        }

        .rbSkinnedButton {
            vertical-align: top
        }

        .rbSkinnedButton {
            display: inline-block;
            position: relative;
            background-color: transparent;
            background-repeat: no-repeat;
            border: 0 none;
            height: 22px;
            text-align: center;
            text-decoration: none;
            white-space: nowrap;
            background-position: left -525px;
            padding-left: 4px;
            vertical-align: top;
            box-sizing: border-box
        }

        .RadButton {
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadButton {
            box-sizing: content-box;
            -moz-box-sizing: content-box
        }

        .RadButton_Default {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 12px
        }

        .rbSkinnedButton {
            padding-left: 4px
        }

        .RadButton {
            cursor: pointer
        }

        .rbSkinnedButton {
            vertical-align: middle
        }

        .rbSkinnedButton {
            vertical-align: top
        }

        .rbSkinnedButton {
            display: inline-block;
            position: relative;
            background-color: transparent;
            background-repeat: no-repeat;
            border: 0 none;
            height: 22px;
            text-align: center;
            text-decoration: none;
            white-space: nowrap;
            background-position: left -525px;
            padding-left: 4px;
            vertical-align: top;
            box-sizing: border-box
        }

        .RadButton {
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .RadButton {
            box-sizing: content-box;
            -moz-box-sizing: content-box
        }

        .RadButton_Default .rbDecorated {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            color: #333
        }

        .RadButton_Default .rbDecorated {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 12px
        }

        .RadButton .rbDecorated {
            padding-left: 8px;
            padding-right: 12px;
            margin: 0;
            border: 0
        }

        .RadButton_Default .rbDecorated {
            background-image: url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            color: #333
        }

        .RadButton_Default .rbDecorated {
            font-family: "Segoe UI",Arial,Helvetica,sans-serif;
            font-size: 12px
        }

        .RadButton .rbDecorated {
            padding-left: 8px;
            padding-right: 12px;
            margin: 0;
            border: 0
        }

        .rbDecorated {
            padding-left: 8px;
            padding-right: 12px
        }

        .rbDecorated {
            display: block;
            *display: inline;
            *zoom: 1;
            height: 22px;
            padding-left: 6px;
            *padding-left: 8px;
            padding-right: 10px;
            border: 0;
            text-align: center;
            background-position: right -88px;
            overflow: visible;
            background-color: transparent;
            outline: 0;
            cursor: pointer;
            -webkit-border-radius: 0;
            -webkit-appearance: none;
            *line-height: 22px
        }

        .rbDecorated {
            line-height: 20px
        }

        .rbDecorated {
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }

        .rbDecorated {
            padding-left: 8px;
            padding-right: 12px
        }

        .rbDecorated {
            display: block;
            *display: inline;
            *zoom: 1;
            height: 22px;
            padding-left: 6px;
            *padding-left: 8px;
            padding-right: 10px;
            border: 0;
            text-align: center;
            background-position: right -88px;
            overflow: visible;
            background-color: transparent;
            outline: 0;
            cursor: pointer;
            -webkit-border-radius: 0;
            -webkit-appearance: none;
            *line-height: 22px
        }

        .rbDecorated {
            line-height: 20px
        }

        .rbDecorated {
            font-size: 12px;
            font-family: "Segoe UI",Arial,Helvetica,sans-serif
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <script type="text/javascript">
            window.onresize = ResizeGrid;
            Sys.Application.add_load(ResizeGrid);
            function ResizeGrid() {
                var scrollArea = document.getElementById("<%= DataGridEs.ClientID %>" + "_GridData");
                scrollArea.style.height = window.innerHeight - 170 + 'px';
            };
            function requestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToCsv") >= 0) {
                    args.set_enableAjax(false);
                }
                if (args.get_eventTarget().indexOf("ExportToExcel") >= 0) {
                    args.set_enableAjax(false);
                }
            };
        </script>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="requestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="PanelRadGrid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGridEs" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div>
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td style="width: 100%">
                        <telerik:RadButton ID="btnEsci" runat="server" Style="top: 0px; left: 0px" Text="Esci"
                            ToolTip="Esci" OnClientClicking="function(sender, args){self.close();}" />
                        <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../../../NuoveImm/Img_Esci.png" />--%>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                            Text="Situazione Contabile Generale Sintesi - Esercizio Contabile"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="PanelRadGrid">
                            <telerik:RadGrid ID="DataGridEs" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                                Culture="it-IT" RegisterWithScriptManager="False" AllowFilteringByColumn="false"
                                EnableLinqExpressions="False" Width="100%" AllowSorting="false" IsExporting="False"
                                AllowPaging="false">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                    CommandItemDisplay="Top" HierarchyLoadMode="ServerBind">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="true" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="true" />
                                    <DetailTables>
                                        <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                            HierarchyDefaultExpanded="true">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left"
                                                    Visible="false">
                                                </telerik:GridBoundColumn>
                                                <%--<telerik:GridBoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="5%" />
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridBoundColumn>--%>
                                                <telerik:GridBoundColumn DataField="CODICE" HeaderText="CODICE" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="10%" />
                                                    <HeaderStyle Width="10%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BUDGET" HeaderText="BUDGET INIZIALE" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ASSESTATO" HeaderText="BUDGET ASSESTATO + VARIAZIONI"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RESIDUO" HeaderText="DISPONIBILITA' RESIDUA"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RESIDUO_TOTALE" HeaderText="DISPONIBILITA' RESIDUA - PRENOTATO"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PRENOTATO" HeaderText="TOTALE PRENOTATO" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CONSUNTIVATO" HeaderText="TOTALE CONSUNTIVATO"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RITCONSUNTIVATO" HeaderText="TOTALE RIT LEGGE CONSUNTIVATA"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IMPONIBILE_CERTIFICATO" HeaderText="TOTALE IMPONIBILE CERTIFICATO"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IVA_CERTIFICATA" HeaderText="TOTALE IVA CERTIFICATA"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CERTIFICATO" HeaderText="TOTALE CERTIFICATO"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IMPONIBILE_CERTIFICATO_RIT" HeaderText="TOTALE RIT LEGGE IMPONIBILE CERTIFICATA"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IVA_CERTIFICATA_RIT" HeaderText="TOTALE RIT LEGGE IVA CERTIFICATA"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="6%" />
                                                    <HeaderStyle Width="6%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CERTIFICATO_RIT" HeaderText="TOTALE RIT LEGGE CERTIFICATA "
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FATTURATO" HeaderText="TOTALE FATTURATO" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FATTURATO_RIT" HeaderText="TOTALE RIT LEGGE FATTURATO" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PAGATO" HeaderText="TOTALE PAGATO SENZA IVA"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IVA" HeaderText="IVA" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IVA_SPLIT" HeaderText="IVA SPLIT" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TOTPAGATO" HeaderText="TOTALE PAGATO" ItemStyle-HorizontalAlign="Right"
                                                    DataFormatString="{0:C2}">
                                                    <ItemStyle Width="5%" />
                                                    <HeaderStyle Width="5%" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CAPITOLO" HeaderText="CAPITOLO" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CODICE" HeaderText="CODICE" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BUDGET" HeaderText="BUDGET INIZIALE" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ASSESTATO" HeaderText="BUDGET ASSESTATO + VARIAZIONI"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RESIDUO" HeaderText="DISPONIBILITA' RESIDUA"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RESIDUO_TOTALE" HeaderText="DISPONIBILITA' RESIDUA - PRENOTATO"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PRENOTATO" HeaderText="TOTALE PRENOTATO" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CONSUNTIVATO" HeaderText="TOTALE CONSUNTIVATO"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RITCONSUNTIVATO" HeaderText="TOTALE RIT LEGGE CONSUNTIVATA"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPONIBILE_CERTIFICATO" HeaderText="TOTALE IMPONIBILE CERTIFICATO"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IVA_CERTIFICATA" HeaderText="TOTALE IVA CERTIFICATA"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CERTIFICATO" HeaderText="TOTALE CERTIFICATO"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IMPONIBILE_CERTIFICATO_RIT" HeaderText="TOTALE RIT LEGGE IMPONIBILE CERTIFICATA"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IVA_CERTIFICATA_RIT" HeaderText="TOTALE RIT LEGGE IVA CERTIFICATA"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="6%" />
                                            <HeaderStyle Width="6%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CERTIFICATO_RIT" HeaderText="TOTALE RIT LEGGE CERTIFICATA "
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FATTURATO" HeaderText="TOTALE FATTURATO" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FATTURATO_RIT" HeaderText="TOTALE RIT LEGGE FATTURATO" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PAGATO" HeaderText="TOTALE PAGATO SENZA IVA"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IVA" HeaderText="IVA" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="IVA_SPLIT" HeaderText="IVA SPLIT" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TOTPAGATO" HeaderText="TOTALE PAGATO" ItemStyle-HorizontalAlign="Right"
                                            DataFormatString="{0:C2}">
                                            <ItemStyle Width="5%" />
                                            <HeaderStyle Width="5%" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ExportSettings>
                                    <Csv EncloseDataWithQuotes="false" ColumnDelimiter="Semicolon" />
                                </ExportSettings>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
</html>
