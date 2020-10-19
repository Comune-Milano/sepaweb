<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tab_ElencoMandati.aspx.vb" Inherits="Tab_ElencoMandati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script type="text/javascript">


    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }




</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />

    <title>MODULO GESTIONE MANDATI PAGAMENTI</title>

    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>


    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />

    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit; 



        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }


    </script>


</head>

<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div id="DIV1">
            &nbsp;&nbsp;
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo" colspan="2">ELENCO MANDATI DI PAGAMENTO</td>

                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="imgUscita" runat="server" text="Esci"
                            Style="z-index: 125; left: 600px; position: static; top: 29px; cursor:pointer" TabIndex="12"
                            ToolTip="Esce dalla gestione dei mandati di pagamento" OnClientClick="document.getElementById('USCITA').value='1';" /></td>
                </tr>
            </table>
            <table id="TABBLE_LISTA" style="width: 100%">
                <tr>
                    <td style="height: 139px"></td>
                    <td style="height: 139px;">

                        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true"
                            AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            PageSize="100" IsExporting="False">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToExcelButton="FALSE" ShowExportToWordButton="false"
                                    ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                    ShowRefreshButton="FALSE" />

                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_MANDATO_ANNO" HeaderText="NUM. MANDATO/ANNO">
                                        <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False"  Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_MANDATO" HeaderText="DATA">
                                        <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False"  Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                                        <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False"  Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Selezione" Visible="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                                ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                                Text="Annulla"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                                Text="Modifica">Seleziona</asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                <PagerStyle AlwaysVisible="True"></PagerStyle>
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

                        <asp:Label ID="lbl_Consuntivi" runat="server" CssClass="TitoloH1" Style ="text-align:left"
                            Text="TOTALE DA LIQUIDARE" Width="376px"></asp:Label>
                        <asp:Label ID="lbl_Tot_Da_liquidare" runat="server" Style="text-align: right" CssClass="TitoloH1" Text="0" Width="100px"></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lbl_Rimborsi" runat="server" CssClass="TitoloH1" Style ="text-align:left" Text="TOTALE LIQUIDATO:" Width="376px"></asp:Label>
                        <asp:Label ID="lbl_Tot_Liquidato" runat="server" CssClass="TitoloH1" Style="text-align: right" TabIndex="-1" Text="0"
                            Width="100px"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="TitoloH1" Style ="text-align:left" TabIndex="-1" Text="TOTALE:" Width="376px"></asp:Label>
                        <asp:Label ID="lbl_Totale" runat="server"  CssClass="TitoloH1"
                             Style="text-align: right" TabIndex="-1" Text="0" Width="100px"></asp:Label></td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="USCITA" runat="server"></asp:HiddenField>
    </form>
    <script type="text/javascript">
</script>

    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>

</body>

</html>
