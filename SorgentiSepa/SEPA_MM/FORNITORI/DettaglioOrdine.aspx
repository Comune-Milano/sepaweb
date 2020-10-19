<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioOrdine.aspx.vb"
    Inherits="FORNITORI_DettaglioOrdine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dettaglio Ordini</title>
    <style type="text/css">
        .ChangeCursor:hover
        {
            cursor: pointer;
        }
        
        .style1
        {
            text-align: left;
        }
        
        .style3
        {
            width: 12%;
        }
    </style>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
   <%-- <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />--%>
    <%-- <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>--%>
    <%--<script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>--%>
    <%--<script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>--%>
    <%--<script src="../Funzioni.js" type="text/javascript"></script>--%>
    <%-- <script src="../Standard/Scripts/notify.js" type="text/javascript"></script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function pageLoad(sender, eventArgs) {
                if (!eventArgs.get_isPartialLoad()) {
                    $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("InitialPageLoad");
                }
            }

            function ConfermaProposta() {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm("Sei sicuro di voler richiedere al fornitore la richiesta di consuntivazione?", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function openWin() {
                var radwindow = $find('RadWindowConferma');
                radwindow.show();
            }

           
        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript" id="telerikClientEvents1">
        //<![CDATA[

        function OnClientAppointmentClick(sender, args) {
            var apt = args.get_appointment();
            document.getElementById('indiceM').value = apt._id;
            document.getElementById('btnVisDettagli').click();
        }

        function StampaOrdine() {
            window.open('../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/StampaOrdine.aspx?COD=' + document.getElementById('indiceM').value, 'Ordine', '');
        }

        function StampaODL() {
            window.open('../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?ODL=' + document.getElementById('vNUM_ODL').value + '&ANNO=' + document.getElementById('vANNO_ODL').value + '&ORD=UBICAZIONE&PROVENIENZA=RICERCA_DIRETTA&EF_R=' + document.getElementById('vEF_F').value + '&REP=&X=CH', 'Ordine', 'height=700,top=0,left=0,width=1300');
        }

        function ConfermaProposta(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });
            apriConfirm("Sei sicuro di voler richiedere al fornitore la richiesta di consuntivazione?", callBackFunction, 300, 150, "Info", null);
            args.set_cancel(true);
        }



        //]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            <asp:ScriptReference Path="../Standard/Scripts/gestioneDimensioniPagina.js" />
            <asp:ScriptReference Path="../Standard/Scripts/notify.js" />
            <asp:ScriptReference Path="../Standard/Scripts/jsFunzioni.js" />
            <asp:ScriptReference Path="../Funzioni.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAgenda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadAgenda" LoadingPanelID="RadAjaxLoadingPanel1">
                    </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindow ID="RadWindowConferma" runat="server" CenterIfModal="true" Modal="True"
        VisibleStatusbar="False" Behavior="Pin, Move, Resize" Title="Info" Skin="Web20"
        Height="250px" Width="300px">
        <ContentTemplate>
            <asp:Panel runat="server" ID="contenuto">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            Le date DPIL e/o DPFL sono state modificate dal fornitore. Indicare se contestare
                            con non conformità o accettare la modifica.
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonAccetta" runat="server" Checked="True" Font-Names="arial"
                                Font-Size="9pt" GroupName="A" Text="Accettare la modifica" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadioButtonRifiuta" runat="server" Font-Names="arial" Font-Size="9pt"
                                GroupName="A" Text="Contesta con non conformità" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="RadButtonConfermaModifica" runat="server" Text="Conferma">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadButton ID="RadButtonEsciConferma" runat="server" Text="Esci" AutoPostBack="false"
                                            OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowConferma', '');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:Panel ID="Panel1" runat="server">
        <asp:Panel ID="Panel2" Visible="false" runat="server">
            <div id="divGenerale" align="center">
                <div id="divBody">
                    <table width="90%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                                <div id="divTitolo">
                                    <table id="tbTitolo">
                                        <tr>
                                            <td style="width: 5px;">
                                                &nbsp;
                                            </td>
                                            <td>
                                                Dettaglio Ordini
                                                <asp:Label ID="lblNumeroOrdini" runat="server"></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divMenu" style="height: 32px;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 5px;">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <table cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                                                                AutoPostBack="False" CausesValidation="False" OnClientClicking="ClickUscita">
                                                            </telerik:RadButton>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadScheduler runat="server" ID="RadAgenda" SelectedView="TimelineView"
                                    DayEndTime="23:59:00" DayStartTime="00:01:00" DataKeyField="ID_MANUTENZIONE"
                                    DataSubjectField="NUM_ODL" DataStartField="DATA_INIZIO_INTERVENTO" DataEndField="DATA_INIZIO_INTERVENTO"
                                    Localization-HeaderMultiDay="Work Week" AllowDelete="False" Culture="it-IT" DisplayDeleteConfirmation="False"
                                    AppointmentStyleMode="Simple" Skin="Web20" ShowViewTabs="False" ShowHoursColumn="False"
                                    AllowEdit="False" AllowInsert="False" ReadOnly="True" Width="1500px" Height="170px"
                                    OnClientAppointmentClick="OnClientAppointmentClick" Visible="False">
                                    <ExportSettings FileName="OrdiniExport" OpenInNewWindow="True">
                                        <Pdf PageTopMargin="1in" PageBottomMargin="1in" PageLeftMargin="1in" PageRightMargin="1in"
                                            Author="Sistemi e Soluzioni srl" Title="Elenco ordini"></Pdf>
                                    </ExportSettings>
                                    <AdvancedForm Modal="true"></AdvancedForm>
                                    <Localization HeaderMultiDay="Work Week"></Localization>
                                    <MultiDayView UserSelectable="false"></MultiDayView>
                                    <DayView UserSelectable="false"></DayView>
                                    <TimelineView ShowInsertArea="True" ReadOnly="True" SlotDuration="1.00:00:00" />
                                    <WeekView UserSelectable="false"></WeekView>
                                    <MonthView UserSelectable="false"></MonthView>
                                    <TimeSlotContextMenuSettings EnableEmbeddedBaseStylesheet="False" EnableEmbeddedScripts="False"
                                        EnableEmbeddedSkins="False"></TimeSlotContextMenuSettings>
                                    <AgendaView UserSelectable="False" />
                                    <AppointmentContextMenuSettings EnableEmbeddedBaseStylesheet="False" EnableEmbeddedScripts="False"
                                        EnableEmbeddedSkins="False"></AppointmentContextMenuSettings>
                                </telerik:RadScheduler>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 98%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 5%; text-align: right;">
                                            <img alt="Ordine Regolare" src="Immagini/OrdineRG.png" height="20" width="20" />
                                        </td>
                                        <td style="width: 20%; text-align: left;">
                                            &nbsp;
                                            <asp:Label ID="lblRegolari" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                                                Text="ORDINE REGOLARE"></asp:Label>
                                        </td>
                                        <td style="width: 5%; text-align: right;">
                                            <img alt="Ordine Fuori Tempo" src="Immagini/OrdineFT.png" height="20" width="20" />
                                        </td>
                                        <td style="width: 20%; text-align: left;">
                                            &nbsp;
                                            <asp:Label ID="lblFuoriTempo" runat="server" Font-Bold="True" Font-Names="ARIAL"
                                                Font-Size="8pt" Text="ORDINE FUORI TEMPO"></asp:Label>
                                        </td>
                                        <td style="width: 50%; text-align: right;">
                                            <telerik:RadButton ID="btnPrContab" runat="server" Text="RICHIESTA CONSUNTIVO ODL"
                                                ToolTip="Richiesta di inserimento consuntivo ODL" OnClientClicking="function(sender, args){ConfermaProposta(sender, args);}"
                                                Visible="False">
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" Visible="true" runat="server" Height="380px">
                                    <table width="99%">
                                        <tr>
                                            <td>
                                                <table style="border: 1px solid #0066FF; width: 100%;" cellpadding="2" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Num./Data Segnalazione"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsegnalazione" runat="server" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Num./Data Ordine"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOrdine" runat="server" Font-Bold="True"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" runat="server" Text="STATO INTERVENTO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatoIntervento" runat="server" Font-Bold="True" Font-Size="10pt"
                                                                ForeColor="Maroon"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnVisDettagli" runat="server" Style="visibility: hidden" Text="Button" />
                                                            <asp:Label ID="lblStatoPR" runat="server" Visible="False" Text="STATO PREVENTIVO"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRDORichiesto" runat="server" Font-Bold="True" Font-Size="10pt"
                                                                ForeColor="Maroon" Visible="False"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table width="100%">
                                        <tr>
                                            <td valign="top" width="100%">
                                                <div class="tabContainer">
                                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="99%" MultiPageID="RadMultiPage1"
                                                        Align="Left" SelectedIndex="0" ShowBaseLine="true" ScrollChildren="true">
                                                        <Tabs>
                                                            <telerik:RadTab PageViewID="RadPageViewOrdine" Selected="True" Text="Ordine" />
                                                            <telerik:RadTab PageViewID="RadPageViewSegnalazione" Text="Segnalazione" />
                                                            <telerik:RadTab PageViewID="RadPageViewPreventivo" Text="Preventivo" Visible="false"  />
                                                            <telerik:RadTab PageViewID="RadPageViewAllegati" Text="Allegati" />
                                                            <telerik:RadTab PageViewID="RadPageViewIrregolarita" Text="Non Conformità" />
                                                            <telerik:RadTab PageViewID="RadPageViewEventi" Text="Eventi" />
                                                        </Tabs>
                                                    </telerik:RadTabStrip>
                                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" CssClass="RadMultiPage" Height="320px"
                                                        SelectedIndex="0" Width="99%">
                                                        <telerik:RadPageView ID="RadPageViewOrdine" runat="server" Selected="true">
                                                            <br />
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="1%">
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                    <td width="99%">
                                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                                            <tr>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label17" runat="server" Text="Riferimento Contratto"></asp:Label>
                                                                                </td>
                                                                                <td width="40%" style="text-align: left">
                                                                                    <asp:Label ID="lblNumContratto" runat="server" Font-Bold="True"></asp:Label>
                                                                                    <asp:Label ID="lblFornitore" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label19" runat="server" Text="Descrizione Contratto"></asp:Label>
                                                                                </td>
                                                                                <td width="40%" style="text-align: left" colspan="3">
                                                                                    <asp:Label ID="lblDescrizioneContratto" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label21" runat="server" Text="Data Inizio/Fine Inter."></asp:Label>
                                                                                </td>
                                                                                <td width="40%" style="text-align: left">
                                                                                    <asp:Label ID="lblInizioODL" runat="server" Font-Bold="True"></asp:Label>
                                                                                    <asp:Label ID="Label56" runat="server" Font-Bold="True">/</asp:Label>
                                                                                    <asp:Label ID="lblFineODL" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label35" runat="server" Text="Data DPIL/DPFL" ToolTip="Data programmata inizio lavori/Data programmata termine lavori"></asp:Label>
                                                                                </td>
                                                                                <td width="15%" style="text-align: left">
                                                                                    <asp:Label ID="lblDataPGI" runat="server" Font-Bold="True"></asp:Label>
                                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True">/</asp:Label>
                                                                                    <asp:Label ID="lblDataTDL" runat="server" Font-Bold="True"></asp:Label>
                                                                                    &nbsp;
                                                                                    <asp:Image ID="imgAccettaDate" runat="server" ImageUrl="Immagini/Alert.gif" onclick="openWin(); return false;"
                                                                                        Style="cursor: pointer" />
                                                                                </td>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label57" runat="server" Text="Data termine Lav."></asp:Label>
                                                                                </td>
                                                                                <td width="15%" style="text-align: left">
                                                                                    <asp:Label ID="lblDataFINEINT" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label25" runat="server" Text="Ubicazione"></asp:Label>
                                                                                </td>
                                                                                <td width="40%" style="text-align: left">
                                                                                    <asp:Label ID="lblUbicazione" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label36" runat="server" Text="Link al pdf"></asp:Label>
                                                                                </td>
                                                                                <td width="15%" style="text-align: left">
                                                                                    <asp:Label ID="lblLinkPDF" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label58" runat="server" Text="Link al ODL"></asp:Label>
                                                                                </td>
                                                                                <td width="15%" style="text-align: left">
                                                                                    <asp:Label ID="lblLinkODL" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left" class="style3">
                                                                                    <asp:Label ID="Label27" runat="server" Text="Danneggiato"></asp:Label>
                                                                                </td>
                                                                                <td colspan="5" style="text-align: left">
                                                                                    <asp:Label ID="lblDanneggiatoODL" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left" class="style3">
                                                                                    <asp:Label ID="Label29" runat="server" Text="Richiesta"></asp:Label>
                                                                                </td>
                                                                                <td colspan="5" style="text-align: left">
                                                                                    <asp:Label ID="lblRichiestaODL" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    <asp:Label ID="Label32" runat="server" Text="OGGETTO INTERVENTI"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    <telerik:RadGrid ID="dgvInterventi" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                        AutoGenerateColumns="False" Culture="it-IT" GroupPanelPosition="Top" Height="120px"
                                                                                        IsExporting="False" RegisterWithScriptManager="False" PageSize="5" Width="97%">
                                                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                                        <ExportSettings ExportOnlyData="True" FileName="ExportInterventi" HideStructureColumns="True"
                                                                                            IgnorePaging="True" OpenInNewWindow="True">
                                                                                            <Pdf PageWidth="">
                                                                                            </Pdf>
                                                                                            <Excel Format="Xlsx" />
                                                                                            <Csv EncloseDataWithQuotes="False" />
                                                                                        </ExportSettings>
                                                                                        <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" ReorderColumnsOnClient="True">
                                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                                                            <Resizing AllowColumnResize="false" AllowResizeToFit="true" AllowRowResize="false"
                                                                                                ClipCellContentOnResize="true" EnableRealTimeResize="false" ResizeGridOnColumnResize="true" />
                                                                                        </ClientSettings>
                                                                                        <MasterTableView HierarchyLoadMode="Client">
                                                                                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                                                                                                ShowRefreshButton="False" />
                                                                                            <RowIndicatorColumn Visible="True">
                                                                                            </RowIndicatorColumn>
                                                                                            <ExpandCollapseColumn Created="True">
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderStyle-Width="10%" HeaderText="TIPOLOGIA">
                                                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="DETTAGLIO" HeaderStyle-Width="60%" HeaderText="DETTAGLIO">
                                                                                                    <HeaderStyle Width="60%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="INDIRIZZO_BENE" HeaderStyle-Width="30%" HeaderText="INDIRIZZO">
                                                                                                    <HeaderStyle Width="30%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                            </Columns>
                                                                                        </MasterTableView><ClientSettings AllowDragToGroup="True">
                                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                                            <Selecting AllowRowSelect="True" />
                                                                                            <Resizing AllowColumnResize="true" AllowResizeToFit="true" EnableRealTimeResize="true"
                                                                                                ResizeGridOnColumnResize="true" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="vEF_F" />
                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="vNUM_ODL" />
                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="vANNO_ODL" />
                                                        </telerik:RadPageView>
                                                        <telerik:RadPageView ID="RadPageViewSegnalazione" runat="server">
                                                            <br />
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="1%">
                                                                    </td>
                                                                    <td width="99%">
                                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                                            <tr>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label1" runat="server" Text="Cognome Intestatario"></asp:Label>
                                                                                </td>
                                                                                <td width="40%" style="text-align: left">
                                                                                    <asp:Label ID="lblCognomeIntestatario" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td width="10%" style="text-align: left">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Nome Intestatario"></asp:Label>
                                                                                </td>
                                                                                <td width="40%" style="text-align: left">
                                                                                    <asp:Label ID="lblNomeIntestatario" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style1">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Codice Contratto"></asp:Label>
                                                                                </td>
                                                                                <td class="style1">
                                                                                    <asp:Label ID="lblCodicecontratto" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td class="style1">
                                                                                    <asp:Label ID="Label2" runat="server" Text="Codice Unità"></asp:Label>
                                                                                </td>
                                                                                <td class="style1">
                                                                                    <asp:Label ID="lblCodiceUnita" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="Label10" runat="server" Text="Edificio"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="lblEdificio" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="Label16" runat="server" Text="Indirizzo"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Scala"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="lblScala" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="Label55" runat="server" Text="Piano"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="lblPiano" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="Label14" runat="server" Text="Interno"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="lblInterno" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;&nbsp;
                                                                                </td>
                                                                                <td>
                                                                                    &nbsp;&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: left">
                                                                                    <asp:Label ID="Label15" runat="server" Text="Richiesta"></asp:Label>
                                                                                </td>
                                                                                <td colspan="3" style="text-align: left">
                                                                                    <asp:Label ID="lblrichiesta" runat="server" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </telerik:RadPageView>
                                                        <telerik:RadPageView ID="RadPageViewPreventivo" runat="server">
                                                            <br />
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label11" runat="server" Text="ELENCO PREVENTIVI"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <telerik:RadGrid ID="RadGridPreventivi" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                        AutoGenerateColumns="False" Culture="it-IT" IsExporting="False" OnNeedDataSource="RadGridPreventivi_NeedDataSource"
                                                                                        PageSize="100" Width="97%" GroupPanelPosition="Top" Height="260px">
                                                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                                        <ExportSettings>
                                                                                            <Pdf PageWidth="">
                                                                                            </Pdf>
                                                                                        </ExportSettings>
                                                                                        <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" DataKeyNames="ID"
                                                                                            GridLines="None">
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="NUMERO" HeaderText="NUM.">
                                                                                                    <HeaderStyle Width="10%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="DATA_PR" HeaderText="DATA">
                                                                                                    <HeaderStyle Width="10%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO">
                                                                                                    <HeaderStyle Width="10%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO">
                                                                                                    <HeaderStyle Width="10%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="DATA_FINE" HeaderText="DATA FINE">
                                                                                                    <HeaderStyle Width="10%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                                                                    <HeaderStyle Width="40%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                                                                                                    <HeaderStyle Width="10%" />
                                                                                                </telerik:GridBoundColumn>
                                                                                            </Columns>
                                                                                            <EditFormSettings>
                                                                                                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                                                                                                </EditColumn>
                                                                                            </EditFormSettings>
                                                                                        </MasterTableView>
                                                                                    </telerik:RadGrid>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </telerik:RadPageView>
                                                        <telerik:RadPageView ID="RadPageViewAllegati" runat="server">
                                                            <br />
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label6" runat="server" Text="ELENCO ALLEGATI"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    
                                                                                        <telerik:RadGrid ID="RadGridAllegati" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                                                                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                                                                        AllowPaging="True" IsExporting="False" Height="260px" AllowFilteringByColumn="True"
                                                                                        PageSize="100" Width="97%">
                                                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                                        <ExportSettings FileName="ExportInterventi" IgnorePaging="True" OpenInNewWindow="True"
                                                                                            ExportOnlyData="True" HideStructureColumns="True">
                                                                                            <Pdf PageWidth="">
                                                                                            </Pdf>
                                                                                            <Excel Format="ExcelML" />
                                                                                            <Csv EncloseDataWithQuotes="False" />
                                                                                        </ExportSettings>
                                                                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                                                            <Selecting AllowRowSelect="True"></Selecting>
                                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                                                            <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                                                                                ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                                                                                        </ClientSettings>
                                                                                            <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" DataKeyNames="ID"
                                                                                                GridLines="None">
                                                                                                <Columns>
                                                                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA">
                                                                                                        <HeaderStyle Width="10%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPO DOC.">
                                                                                                        <HeaderStyle Width="20%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                                                                        <HeaderStyle Width="50%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="NOME_FILE" HeaderText="ALLEGATO">
                                                                                                        <HeaderStyle Width="10%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                     <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                                                                                        <HeaderStyle Width="10%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                </Columns>
                                                                                            </MasterTableView><FilterMenu >
                                                                                            </FilterMenu>
                                                                                            <HeaderContextMenu >
                                                                                            </HeaderContextMenu>
                                                                                        </telerik:RadGrid>
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </telerik:RadPageView>
                                                        <telerik:RadPageView ID="RadPageViewIrregolarita" runat="server">
                                                            <br />
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label13" runat="server" Text="ELENCO NON CONFORMITA'"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <div id="Div1" style="overflow: auto; visibility: visible; width: 100%; height: 400px;">
                                                                                        <telerik:RadGrid ID="RadGridIrregolarita" runat="server" AllowAutomaticDeletes="True"
                                                                                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                                            AllowSorting="True" AutoGenerateColumns="False" Culture="it-IT" GroupPanelPosition="Top"
                                                                                            IsExporting="False" OnItemDataBound="OnItemDataBoundHandlerIRR" PageSize="100"
                                                                                            ShowStatusBar="True" Width="97%">
                                                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                                            <ExportSettings>
                                                                                                <Pdf PageWidth="">
                                                                                                </Pdf>
                                                                                            </ExportSettings>
                                                                                            <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="Top"
                                                                                                DataKeyNames="ID" GridLines="None">
                                                                                                <CommandItemTemplate>
                                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="InitInsert"><img alt="" src="Immagini/AddNewRecord.png" 
                                                                    style="border:0px" />Aggiungi nuova non conformità</asp:LinkButton>
                                                                                                </CommandItemTemplate>
                                                                                                <CommandItemSettings ShowAddNewRecordButton="True" ShowExportToCsvButton="false"
                                                                                                    ShowExportToExcelButton="False" ShowExportToPdfButton="false" ShowExportToWordButton="false"
                                                                                                    ShowRefreshButton="False" />
                                                                                                <Columns>
                                                                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                                                                                    </telerik:GridEditCommandColumn>
                                                                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA">
                                                                                                        <HeaderStyle Width="10%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPO">
                                                                                                        <HeaderStyle Width="20%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                                                                        <HeaderStyle Width="50%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridBoundColumn DataField="VISIBILE" HeaderText="VISIBILITA' FORNITORE">
                                                                                                        <HeaderStyle Width="10%" />
                                                                                                    </telerik:GridBoundColumn>
                                                                                                    <telerik:GridButtonColumn ConfirmText="Eliminare l'elemento selezionato?" ConfirmDialogType="RadWindow"
                                                                                                        ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                                                                                                        UniqueName="DeleteColumn" ButtonType="ImageButton">
                                                                                                        <HeaderStyle Width="50px"></HeaderStyle>
                                                                                                    </telerik:GridButtonColumn>
                                                                                                </Columns>
                                                                                                <EditFormSettings EditFormType="Template" InsertCaption="INSERIMENTO NON CONFORMITA'"
                                                                                                    PopUpSettings-CloseButtonToolTip="Chiudi" PopUpSettings-Height="150px" PopUpSettings-Modal="True"
                                                                                                    PopUpSettings-ShowCaptionInEditForm="True" PopUpSettings-Width="500px">
                                                                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                                                                                                    </EditColumn>
                                                                                                    <FormTemplate>
                                                                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                                                                            width="500px">
                                                                                                            <tr class="EditFormHeader">
                                                                                                                <td width="100%">
                                                                                                                    <b></b>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td width="100%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td valign="top" width="100%">
                                                                                                                    <table id="Table3" border="0" class="module" width="100%">
                                                                                                                        <tr valign="top">
                                                                                                                            <td valign="top" width="30%">
                                                                                                                                Tipologia:
                                                                                                                            </td>
                                                                                                                            <td valign="top" width="50%">
                                                                                                                                <telerik:RadComboBox ID="cmbTipologia" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                                                                                                    Filter="Contains" AutoPostBack="False" Width="300px" Text='<%# Bind("TIPOLOGIA") %>'>
                                                                                                                                </telerik:RadComboBox>
                                                                                                                            </td>
                                                                                                                            <td width="20%">
                                                                                                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="cmbTipologia"
                                                                                                                                    Display="Static" ErrorMessage="Tipologia obbligatoria!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr valign="top">
                                                                                                                            <td valign="top" width="30%">
                                                                                                                                Descrizione:
                                                                                                                            </td>
                                                                                                                            <td valign="top" width="50%">
                                                                                                                                <telerik:RadTextBox ID="txtDescrizioneIrregolarita" runat="server" Height="60px"
                                                                                                                                     Rows="5" Text='<%# Bind("DESCRIZIONE") %>' TextMode="MultiLine"
                                                                                                                                    Width="300px">
                                                                                                                                </telerik:RadTextBox>
                                                                                                                            </td>
                                                                                                                            <td width="20%">
                                                                                                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtDescrizioneIrregolarita"
                                                                                                                                    Display="Static" ErrorMessage="Descrizione obbligatoria!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr valign="top">
                                                                                                                            <td valign="top" width="30%">
                                                                                                                                Visibilità Fornitore:
                                                                                                                            </td>
                                                                                                                            <td valign="top" width="50%">
                                                                                                                                <telerik:RadComboBox ID="cmbVisibilita" runat="server" EnableLoadOnDemand="true"
                                                                                                                                    IsCaseSensitive="false" Filter="Contains" AutoPostBack="False" Width="300px"
                                                                                                                                    Text='<%# Bind("VISIBILE") %>'>
                                                                                                                                    <Items>
                                                                                                                                        <telerik:RadComboBoxItem Text="SI" Value="1" />
                                                                                                                                        <telerik:RadComboBoxItem Text="NO" Value="0" />
                                                                                                                                    </Items>
                                                                                                                                </telerik:RadComboBox>
                                                                                                                            </td>
                                                                                                                            <td width="20%">
                                                                                                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="cmbVisibilita"
                                                                                                                                    Display="Static" ErrorMessage="Visibilità obbligatoria!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr valign="top">
                                                                                                                            <td>
                                                                                                                                &#160;&#160;
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                                &#160;&#160;
                                                                                                                            </td>
                                                                                                                            <td>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    &#160;&#160;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td align="right" width="100%">
                                                                                                                    <asp:Button ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                                                                        Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />&#160;&#160;<asp:Button
                                                                                                                            ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Chiudi" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </FormTemplate>
                                                                                                    <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="500px" />
                                                                                                </EditFormSettings>
                                                                                            </MasterTableView><FilterMenu >
                                                                                            </FilterMenu>
                                                                                            <HeaderContextMenu >
                                                                                            </HeaderContextMenu>
                                                                                        </telerik:RadGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </telerik:RadPageView>
                                                        <telerik:RadPageView ID="RadPageViewEventi" runat="server">
                                                            <br />
                                                            <table width="100%">
                                                                <tr>
                                                                    <td width="100%">
                                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label18" runat="server" Text="ELENCO EVENTI"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <telerik:RadGrid ID="dgvEventi" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                                                                                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                                                                        AllowPaging="True" IsExporting="False" Height="260px" AllowFilteringByColumn="True"
                                                                                        PageSize="100" Width="97%" OnPageIndexChanged="RidimensionaEventi" OnPageSizeChanged ="RidimensionaEventi">
                                                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                                        <ExportSettings FileName="ExportInterventi" IgnorePaging="True" OpenInNewWindow="True"
                                                                                            ExportOnlyData="True" HideStructureColumns="True">
                                                                                            <Pdf PageWidth="">
                                                                                            </Pdf>
                                                                                            <Excel Format="ExcelML" />
                                                                                            <Csv EncloseDataWithQuotes="False" />
                                                                                        </ExportSettings>
                                                                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                                                            <Selecting AllowRowSelect="True"></Selecting>
                                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                                                                            <Resizing AllowColumnResize="false" AllowRowResize="false" ResizeGridOnColumnResize="true"
                                                                                                ClipCellContentOnResize="true" EnableRealTimeResize="false" AllowResizeToFit="true" />
                                                                                                
                                                                                        </ClientSettings>
                                                                                        <MasterTableView CommandItemDisplay="None" HierarchyLoadMode="Client">
                                                                                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToPdfButton="false"
                                                                                                ShowRefreshButton="False" />
                                                                                            <RowIndicatorColumn Visible="True">
                                                                                            </RowIndicatorColumn>
                                                                                            <ExpandCollapseColumn Created="True">
                                                                                            </ExpandCollapseColumn>
                                                                                            <Columns>
                                                                                                <telerik:GridBoundColumn DataField="DATA_EVENTO" HeaderText="DATA" HeaderStyle-Width="10%">
                                                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="EVENTO" HeaderText="EVENTO" HeaderStyle-Width="20%">
                                                                                                    <HeaderStyle Width="20%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE" HeaderStyle-Width="60%">
                                                                                                    <HeaderStyle Width="60%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE" HeaderStyle-Width="10%">
                                                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                                                </telerik:GridBoundColumn>
                                                                                            </Columns>
                                                                                            <PagerStyle AlwaysVisible="True" />
                                                                                        </MasterTableView><ClientSettings AllowDragToGroup="True">
                                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                                            <Selecting AllowRowSelect="True" />
                                                                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" EnableRealTimeResize="true"
                                                                                                AllowResizeToFit="true" />
                                                                                        </ClientSettings>
                                                                                    </telerik:RadGrid>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </telerik:RadPageView>
                                                    </telerik:RadMultiPage>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="indiceM" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </asp:Panel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <asp:HiddenField ID="HPageMasterHeight" runat="server" ClientIDMode="Static" Value="30" />
    <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <script type="text/javascript">
        validNavigation = false;
        window.onresize = Ridimensiona;
        Sys.Application.add_load(Ridimensiona);


        function Ridimensiona() {
            var altezzaRad = 170 //$(window).height() /4;
            var larghezzaRad = $(window).width() - 27;
            $("#RadAgenda").width(larghezzaRad - 10);
            $("#RadMultiPage1").height($(window).height() - 400);
            $("#RadGridPreventivi").height($(window).height() - 450);
            $("#dgvInterventi").height($(window).height() - 580);
            $("#RadGridAllegati").height($(window).height() - 450);
            $("#Div1").height($(window).height() - 450);
            $("#RadGridIrregolarita").height($(window).height() - 460);
            $("#dgvEventi").height($(window).height() - 450);
            
            
        }

        window.focus();

        function CaricamentoInCorso() {
            document.getElementById('divLoadingHome').style.visibility = 'visible';
        }

        function CaricamentoInCorsoClose() {
            document.getElementById('divLoadingHome').style.visibility = 'hidden';
        }

        function ClickUscita(sender, args) {
            self.close();
        }

        function AccettaModifiche(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });
            apriConfirm("Le date DPIL e/o DPFL sono state modificate dal fornitore. Se desideri contestare queste date e inserire una non conformità premi il tasto OK.", callBackFunction, 300, 150, "Info", null);
            args.set_cancel(true);
        }

    </script>
    </form>
</body>
</html>
