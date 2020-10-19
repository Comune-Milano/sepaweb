<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tab_ElencoPagamenti.aspx.vb"
    Inherits="Tab_ElencoPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link rel="stylesheet" type="text/css" href="../../CicloPassivo.css" />
<script src="../../CicloPassivo.js" type="text/javascript"></script>
<script type="text/javascript">

    window.name = "modal";

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '111';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            //document.getElementById('txtModificato').value = '111';   
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
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <base target="_self" />
    <title>MODULO GESTIONE CONSUNTIVO MANUTENZIONI</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };

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
    <form id="form1" runat="server" target="modal">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div id="DIV1">
        &nbsp;&nbsp;
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                    &nbsp;
                    <asp:Label ID="lblELENCO_INTERVENTI" runat="server" Font-Bold="True" TabIndex="-1"
                        Text="ELENCO PAGAMENTI" Width="300px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="TABBLE_LISTA">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <telerik:RadButton ID="imgUscita" runat="server" Text="Esci" ToolTip="Esce dalla gestione dei consuntivi"
                        OnClientClick="document.getElementById('USCITA').value='1';" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 779px">
                    <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 99%">
                        <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                            IsExporting="False" PagerStyle-AlwaysVisible="true">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_PRENOTAZIONE" HeaderText="ID_PRENOTAZIONE"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="PROG/ANNO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE1" HeaderText="DESCRIZIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BENEFICIARIO" HeaderText="BENEFICIARIO / FORNITORE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="Importo PRENOTATO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_PRENOTAZIONE" HeaderText="Data PRENOTAZIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="Num. REPERTORIO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
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
                        <telerik:RadGrid ID="DataGrid2" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                            IsExporting="False" PagerStyle-AlwaysVisible="true">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_PRENOTAZIONE" HeaderText="ID_PRENOTAZIONE"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="PROG/ANNO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE1" HeaderText="DESCRIZIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BENEFICIARIO" HeaderText="BENEFICIARIO / FORNITORE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO" HeaderText="Importo CONSUNTIVATO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_PRENOTAZIONE" HeaderText="Data EMISSIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="Num. REPERTORIO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
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
                        <telerik:RadGrid ID="DataGrid3" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                            AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="100%" AllowSorting="true"
                            IsExporting="False" PagerStyle-AlwaysVisible="true">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_PAGAMENTO" HeaderText="ID_PAGAMENTO" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="PROG/ANNO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_PAGAMENTO" HeaderText="TIPO PAGAMENTO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE1" HeaderText="DESCRIZIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BENEFICIARIO" HeaderText="BENEFICIARIO / FORNITORE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVATO" HeaderText="Importo CONSUNTIVATO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_PAGATO" HeaderText="Importo PAGATO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_EMISSIONE" HeaderText="Data EMISSIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM_REPERTORIO" HeaderText="Num. REPERTORIO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
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
                    <asp:Label ID="lbl_Prenotato" runat="server" Font-Bold="True" CssClass="TitoloH1"
                        TabIndex="-1" Text="TOTALE SOLO PRENOTATO:" Style="text-align: left" Width="600px"></asp:Label>
                    <asp:Label ID="lbl_Tot_PRE" runat="server" Font-Bold="True" CssClass="TitoloH1" Style="text-align: right"
                        TabIndex="-1" Text="0" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 779px">
                    <asp:Label ID="lbl_Consuntivo" runat="server" CssClass="TitoloH1" TabIndex="-1" Text="TOTALE CONSUNTIVATO:"
                        Style="text-align: left" Width="600px"></asp:Label>
                    <asp:Label ID="lbl_Tot_CONS" runat="server" Font-Bold="True" CssClass="TitoloH1"
                        Style="text-align: right" TabIndex="-1" Text="0" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 21px">
                </td>
                <td style="width: 779px; height: 21px;">
                    <asp:Label ID="lbl_Pagato" runat="server" Font-Bold="True" CssClass="TitoloH1" TabIndex="-1"
                        Text="TOTALE SOLO PAGATO:" Width="600px" Style="text-align: left"></asp:Label>
                    <asp:Label ID="lbl_Tot_PAG" runat="server" Font-Bold="True" CssClass="TitoloH1" Style="text-align: right"
                        TabIndex="-1" Text="0" Width="100px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="USCITA" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="HFGriglia" runat="server" />
    </form>
    <script type="text/javascript">


    </script>
    <script type="text/javascript">
        window.focus();
        self.focus();
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
