<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Intervento.aspx.vb" Inherits="FORNITORI_Intervento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dettaglio Intervento</title>
    <style type="text/css">
        .ChangeCursor:hover {
            cursor: pointer;
        }

        .style1 {
            height: 21px;
        }
    </style>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <%-- <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/gestioneDimensioniPagina.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../Funzioni.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/notify.js" type="text/javascript"></script>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Conferma(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm("Sei sicuro di voler prendere in carico questo intervento?", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function ConfermaFine(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm("Sei sicuro di voler evadere questo intervento?", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function ConfermaConta(sender, args) {
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        this.click();
                    }
                });
                apriConfirm("Sei sicuro di aver inserito la proposta di consuntivazione?", callBackFunction, 300, 150, "Info", null);
                args.set_cancel(true);
            }

            function VisualizzaEventi(sender, args) {
                window.open('EventiInterventi.aspx?D=' + document.getElementById('indiceS').value, 'EventiIntervento', '');
            }


            function ModificaDati() {
                document.getElementById('HModificato').value = '1'
            }

            function Uscita() {
                validNavigation = true;
                self.close();
            }
            function ConfermaUscita(sender, args) {
                //VerificaModifiche();
                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        Uscita();
                    }
                });
                if (document.getElementById('HModificato').value == '1') {
                    apriConfirm("Sono state effettuate delle modifiche. Uscire senza salvare?", callBackFunction, 300, 150, "Attenzione", null);
                    args.set_cancel(true);
                }
                else {
                    Uscita();
                }
            }

        </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
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
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server"
            DecoratedControls="Buttons" />
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="divGenerale">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divGenerale" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <%--<asp:Panel ID="Panel1" runat="server" Height="700px">--%><%--<asp:Panel ID="Panel2" Visible="false" runat="server" Height="700px">--%>
        <asp:Panel runat="server" ID="divGenerale">
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="sStatoIntervento" />
            <asp:HiddenField runat="server" ClientIDMode="Static" ID="sRichiestaContab" />

            <asp:Panel runat="server" ID="divBody">
                <asp:Panel runat="server" ID="divTitolo">
                    <table id="tbTitolo">
                        <tr>
                            <td style="width: 5px;">&nbsp;
                            </td>
                            <td>Dettaglio Intervento
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="divMenu" Style="height: 32px;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 5px;">&nbsp;
                            </td>
                            <td>
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btnCarico" runat="server" Text="Prendi in Carico" ToolTip="Prendi in carico"
                                                OnClientClicking="function(sender, args){Conferma(sender, args);}">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnFineLavori" runat="server" Text="Fine Intervento" ToolTip="Fine intervento"
                                                OnClientClicking="function(sender, args){ConfermaFine(sender, args);}" Visible="False">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnContabilizza" runat="server" Text="Da Consuntivare" ToolTip="Da Consuntivare"
                                                OnClientClicking="function(sender, args){ConfermaConta(sender, args);}" Visible="False">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnEventi" runat="server" Text="Eventi" ToolTip="Visualizza eventi intervento"
                                                OnClientClicking="function(sender, args){VisualizzaEventi(sender, args);}" AutoPostBack="False"
                                                CausesValidation="False">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva i dati">
                                            </telerik:RadButton>
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Torna alla pagina principale"
                                                AutoPostBack="False" CausesValidation="False" OnClientClicking="function(sender, args){ConfermaUscita(sender, args);}">
                                            </telerik:RadButton>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HModificato" />
                </asp:Panel>
                <asp:Panel runat="server" ID="divOverContent">
                    <br />
                    <table width="100%">
                        <tr>
                            <td width="1%">&nbsp;
                            </td>
                            <td width="99%">
                                <table style="border: 1px solid #0066FF; width: 100%;" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Num./Data Segnalazione"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblsegnalazione" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Num./Data Ordine"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrdine" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" Text="STATO INTERVENTO"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStatoIntervento" runat="server" Font-Bold="True" Font-Size="10pt"
                                                ForeColor="Maroon"></asp:Label>
                                        </td>
                                        <td>
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
                            <td width="1%" valign="top"></td>
                            <td valign="top" width="99%">
                                <div class="tabContainer">
                                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Width="99%"
                                        MultiPageID="RadMultiPage1" SelectedIndex="2" ShowBaseLine="True"
                                        ScrollChildren="True">
                                        <Tabs>
                                            <telerik:RadTab PageViewID="RadPageViewSegnalazione" Text="Segnalazione" />
                                            <telerik:RadTab PageViewID="RadPageViewPreventivo" Text="Preventivo" Visible="false" />
                                            <telerik:RadTab PageViewID="RadPageViewOrdine" Text="Ordine" Selected="True" />
                                            <telerik:RadTab PageViewID="RadPageViewAllegati" Text="Allegati" />
                                            <telerik:RadTab PageViewID="RadPageViewSP" Text="Sopralluoghi" Visible="false" />
                                            <telerik:RadTab PageViewID="RadPageViewIrr" Text="Non Conformità" />
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" CssClass="RadMultiPage" Height="500px"
                                        SelectedIndex="2" Width="100%">
                                        <telerik:RadPageView ID="RadPageViewSegnalazione" runat="server">
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td width="1%"></td>
                                                    <td width="99%">
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label3" runat="server" Text="Cognome Intestatario"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblCognomeIntestatario" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label5" runat="server" Text="Nome Intestatario"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblNomeIntestatario" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1">
                                                                    <asp:Label ID="Label8" runat="server" Text="Codice Contratto"></asp:Label></td>
                                                                <td class="style1">
                                                                    <asp:Label ID="lblCodicecontratto" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td class="style1">
                                                                    <asp:Label ID="Label9" runat="server" Text="Codice Unità"></asp:Label></td>
                                                                <td class="style1">
                                                                    <asp:Label ID="lblCodiceUnita" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Text="Edificio"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblEdificio" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" Text="Indirizzo"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text="Scala"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblScala" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text="Piano"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblPiano" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text="Interno"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblInterno" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td>&nbsp;&nbsp; </td>
                                                                <td>&nbsp;&nbsp; </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label15" runat="server" Text="Richiesta"></asp:Label></td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lblrichiesta" runat="server" Font-Bold="True"></asp:Label></td>
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
                                                    <td width="1%">&nbsp;&nbsp; </td>
                                                    <td width="99%">
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text="ELENCO PREVENTIVI"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="ContenitorePreventivi" style="overflow: auto; visibility: visible; width: 100%; height: 400px;">
                                                                        <telerik:RadGrid ID="RadGridPreventivi" runat="server" AllowAutomaticDeletes="True"
                                                                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                            AllowSorting="True" AutoGenerateColumns="False" Culture="it-IT" GroupPanelPosition="Top"
                                                                            IsExporting="False" OnItemDataBound="OnItemDataBoundHandlerPR" OnNeedDataSource="RadGridPreventivi_NeedDataSource"
                                                                            PageSize="20" ShowStatusBar="True" Width="97%">
                                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                            <ExportSettings>
                                                                                <Pdf PageWidth=""></Pdf>
                                                                            </ExportSettings>
                                                                            <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="Top"
                                                                                DataKeyNames="ID" GridLines="None">
                                                                                <DetailTables>
                                                                                    <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                                                                        HierarchyDefaultExpanded="true" CommandItemDisplay="Top" DataKeyNames="IDS">
                                                                                        <CommandItemTemplate>
                                                                                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="InitInsert"><img alt="" src="Immagini/AddNewRecord.png" style="border:0px" />Aggiungi nuovo dettaglio Preventivo</asp:LinkButton><asp:LinkButton ID="LinkButton1" runat="server" CommandName="Aggiorna"><img alt="" src="Immagini/Refresh.png" 
                                                                    style="border:0px" />Aggiorna</asp:LinkButton>
                                                                                        </CommandItemTemplate>
                                                                                        <Columns>
                                                                                            <telerik:GridEditCommandColumn UniqueName="EditCommandColumn2" ButtonType="ImageButton"></telerik:GridEditCommandColumn>
                                                                                            <telerik:GridBoundColumn DataField="IDS" HeaderText="INDICE" Visible="false"></telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="DESCRIZIONEP" HeaderText="DESCRIZIONE" HeaderStyle-Width="10%">
                                                                                                <HeaderStyle Width="80%"></HeaderStyle>
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridBoundColumn DataField="IMPORTOP" HeaderText="IMPORTO" HeaderStyle-Width="5%">
                                                                                                <HeaderStyle Width="20%"></HeaderStyle>
                                                                                            </telerik:GridBoundColumn>
                                                                                            <telerik:GridButtonColumn ConfirmText="Eliminare il dettaglio preventivo selezionato?"
                                                                                                ConfirmDialogType="RadWindow" ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px"
                                                                                                CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton">
                                                                                                <HeaderStyle Width="50px"></HeaderStyle>
                                                                                            </telerik:GridButtonColumn>
                                                                                        </Columns>
                                                                                        <EditFormSettings EditFormType="Template" InsertCaption="INSERIMENTO PREVENTIVO"
                                                                                            PopUpSettings-CloseButtonToolTip="Chiudi" PopUpSettings-Height="700px" PopUpSettings-Modal="True"
                                                                                            PopUpSettings-ShowCaptionInEditForm="True" PopUpSettings-Width="800px">
                                                                                            <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1"></EditColumn>
                                                                                            <FormTemplate>
                                                                                                <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                                                                    width="800px">
                                                                                                    <tr>
                                                                                                        <td valign="top" width="100px"></td>
                                                                                                        <td valign="top" width="600px">
                                                                                                            <telerik:RadTextBox ID="txtIndicePR" runat="server" Text='<%# Bind("IDS") %>' TextMode="SingleLine"
                                                                                                                Width="120px" Visible="false">
                                                                                                            </telerik:RadTextBox></td>
                                                                                                        <td width="100px"></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top" width="100px">Descrizione </td>
                                                                                                        <td valign="top" width="600px">
                                                                                                            <telerik:RadTextBox ID="txtDescrizionePR" runat="server" Height="100px" Rows="5"
                                                                                                                Text='<%# Bind("DESCRIZIONEP") %>' TextMode="MultiLine" Width="590px">
                                                                                                            </telerik:RadTextBox></td>
                                                                                                        <td valign="top" width="100px">
                                                                                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3PR" runat="server" ControlToValidate="txtDescrizionePR"
                                                                                                                Display="Static" ErrorMessage="Obbligatorio!" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top" width="100px">Importo </td>
                                                                                                        <td valign="top" width="600px">
                                                                                                            <telerik:RadNumericTextBox ID="txtImportoPR" runat="server" Text='<%# Bind("IMPORTOP") %>'
                                                                                                                Width="120px">
                                                                                                            </telerik:RadNumericTextBox></td>
                                                                                                        <td width="100px">
                                                                                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1PR" runat="server" ControlToValidate="txtImportoPR"
                                                                                                                Display="Static" ErrorMessage="Obbligatorio!!" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td valign="top" width="100px">&#160;&#160; </td>
                                                                                                        <td valign="top" width="600px">&#160;&#160; </td>
                                                                                                        <td valign="top" width="100px">&#160;&#160; </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <tr>
                                                                                                            <td align="right" width="700px" colspan="3">
                                                                                                                <asp:Button ID="btnUpdateDPR" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                                                                    Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />&#160;&#160;<asp:Button
                                                                                                                        ID="btnCancelDPR" runat="server" CausesValidation="false" CommandName="Cancel"
                                                                                                                        Text="Chiudi" /></td>
                                                                                                        </tr>
                                                                                                </table>
                                                                                            </FormTemplate>
                                                                                            <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="800px" />
                                                                                        </EditFormSettings>
                                                                                        <PagerStyle AlwaysVisible="True" />
                                                                                    </telerik:GridTableView>
                                                                                </DetailTables>
                                                                                <CommandItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="InitInsert"><img alt="" src="Immagini/AddNewRecord.png" 
                                                                    style="border:0px" />Aggiungi nuovo Preventivo</asp:LinkButton>
                                                                                </CommandItemTemplate>
                                                                                <Columns>
                                                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"></telerik:GridEditCommandColumn>
                                                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false"></telerik:GridBoundColumn>
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
                                                                                    <telerik:GridButtonColumn ConfirmText="Eliminare l'elemento selezionato?" ConfirmDialogType="RadWindow"
                                                                                        ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                                                                                        UniqueName="DeleteColumn" ButtonType="ImageButton">
                                                                                        <HeaderStyle Width="50px"></HeaderStyle>
                                                                                    </telerik:GridButtonColumn>
                                                                                </Columns>
                                                                                <EditFormSettings EditFormType="Template" InsertCaption="INSERIMENTO PREVENTIVO"
                                                                                    PopUpSettings-CloseButtonToolTip="Chiudi" PopUpSettings-Height="700px" PopUpSettings-Modal="True"
                                                                                    PopUpSettings-ShowCaptionInEditForm="True" PopUpSettings-Width="800px">
                                                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1"></EditColumn>
                                                                                    <FormTemplate>
                                                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                                                            width="800px">
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">&#160;&#160; </td>
                                                                                                <td valign="top" width="600px">&#160;&#160; </td>
                                                                                                <td valign="top" width="100px">&#160;&#160; </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">Numero </td>
                                                                                                <td valign="top" width="600px">
                                                                                                    <telerik:RadTextBox ID="txNumPR" runat="server" Text='<%# Bind("NUMERO") %>' TextMode="SingleLine"
                                                                                                        Width="120px">
                                                                                                    </telerik:RadTextBox></td>
                                                                                                <td width="100px"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">Data Prev. </td>
                                                                                                <td valign="top" width="600px">
                                                                                                    <telerik:RadDatePicker ID="txtDataPR" runat="server" WrapperTableCaption=""
                                                                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                        ShowPopupOnFocus="true">
                                                                                                        <DateInput ID="DateInput6" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                                                                        </DateInput>
                                                                                                        <Calendar ID="Calendar6" runat="server">
                                                                                                            <SpecialDays>
                                                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                                                                </telerik:RadCalendarDay>
                                                                                                            </SpecialDays>
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </td>
                                                                                                <td valign="top" width="100px"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">Data Inizio Lav. </td>
                                                                                                <td valign="top" width="600px">


                                                                                                    <telerik:RadDatePicker ID="txtDataInizioPR" runat="server" WrapperTableCaption=""
                                                                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                        ShowPopupOnFocus="true">
                                                                                                        <DateInput ID="DateInput7" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                                                                        </DateInput>
                                                                                                        <Calendar ID="Calendar7" runat="server">
                                                                                                            <SpecialDays>
                                                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                                                                </telerik:RadCalendarDay>
                                                                                                            </SpecialDays>
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </td>
                                                                                                <td valign="top" width="100px"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">Data Fine Lav. </td>
                                                                                                <td valign="top" width="600px">
                                                                                                    <telerik:RadDatePicker ID="txtDataFinePR" runat="server" WrapperTableCaption=""
                                                                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                                                                        ShowPopupOnFocus="true">
                                                                                                        <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                                                                        </DateInput>
                                                                                                        <Calendar ID="Calendar3" runat="server">
                                                                                                            <SpecialDays>
                                                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                                                                </telerik:RadCalendarDay>
                                                                                                            </SpecialDays>
                                                                                                        </Calendar>
                                                                                                    </telerik:RadDatePicker>
                                                                                                </td>
                                                                                                <td valign="top" width="100px"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">Descrizione </td>
                                                                                                <td valign="top" width="600px">
                                                                                                    <telerik:RadTextBox ID="txtDescrizionePR" runat="server" Height="100px" Rows="5"
                                                                                                        Text='<%# Bind("DESCRIZIONE") %>' TextMode="MultiLine" Width="590px">
                                                                                                    </telerik:RadTextBox></td>
                                                                                                <td valign="top" width="100px"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100px">&#160;&#160; </td>
                                                                                                <td valign="top" width="600px">&#160;&#160; </td>
                                                                                                <td valign="top" width="100px">&#160;&#160; </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <tr>
                                                                                                    <td align="right" width="700px" colspan="3">
                                                                                                        <asp:Button ID="btnUpdatePR" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                                                            Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />&#160;&#160;<asp:Button
                                                                                                                ID="btnCancelPR" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                Text="Chiudi" /></td>
                                                                                                </tr>
                                                                                        </table>
                                                                                    </FormTemplate>
                                                                                    <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="800px" />
                                                                                </EditFormSettings>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageViewOrdine" runat="server" Selected="true">
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td width="1%">&nbsp;&nbsp; </td>
                                                    <td width="99%">
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label37" runat="server" Text="Riferimento Contratto"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblNumContratto" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label38" runat="server" Text="Descrizione Contratto"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblDescrizioneContratto" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label4" runat="server" Text="Data Inizio Intervento"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblInizioODL" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label7" runat="server" Text="Data Fine Intervento"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblFineODL" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label39" runat="server" Text="Ubicazione"></asp:Label></td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lblUbicazione" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" valign="top">
                                                                    <asp:Label ID="Label42" runat="server" Text="Sede T. Competenza"></asp:Label>
                                                                </td>
                                                                <td width="40%" valign="top">
                                                                    <asp:Label ID="lblSedeTerritoriale" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td width="10%" valign="top">
                                                                    <asp:Label ID="Label19" runat="server" Text="Building Manager"></asp:Label></td>
                                                                <td width="40%">
                                                                    <asp:Label ID="lblBM" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label33" runat="server" Text="Danneggiato"></asp:Label></td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lblDanneggiatoODL" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label31" runat="server" Text="Richiesta"></asp:Label></td>
                                                                <td colspan="3">
                                                                    <asp:Label ID="lblRichiestaODL" runat="server" Font-Bold="True"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;&nbsp; </td>
                                                                <td colspan="3">&nbsp;&nbsp; </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Label ID="Label34" runat="server" Text="OGGETTO INTERVENTI"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <telerik:RadGrid ID="dgvInterventi" runat="server" AllowPaging="True" AllowSorting="True"
                                                                        AutoGenerateColumns="False" Culture="it-IT" GroupPanelPosition="Top" Height="140px"
                                                                        IsExporting="False" RegisterWithScriptManager="False" PageSize="5">
                                                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                                        <ExportSettings ExportOnlyData="True" FileName="ExportInterventi" HideStructureColumns="True"
                                                                            IgnorePaging="True" OpenInNewWindow="True">
                                                                            <Pdf PageWidth=""></Pdf>
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
                                                                            <RowIndicatorColumn Visible="True"></RowIndicatorColumn>
                                                                            <ExpandCollapseColumn Created="True"></ExpandCollapseColumn>
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
                                                                            <PagerStyle AlwaysVisible="True" />
                                                                        </MasterTableView><ClientSettings AllowDragToGroup="True">
                                                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                                                            <Selecting AllowRowSelect="True" />
                                                                            <Resizing AllowColumnResize="true" AllowResizeToFit="true" EnableRealTimeResize="true"
                                                                                ResizeGridOnColumnResize="true" />
                                                                        </ClientSettings>
                                                                        <PagerStyle AlwaysVisible="True" />
                                                                    </telerik:RadGrid></td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp; &nbsp; </td>
                                                                <td>&nbsp; </td>
                                                                <td>&nbsp; </td>
                                                                <td>&nbsp; </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label40" runat="server" Text="Link al pdf"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblLinkPDF" runat="server" Font-Bold="True"></asp:Label></td>
                                                                <td>&nbsp; </td>
                                                                <td>&nbsp; </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label35" runat="server" Text="Data DPIL" ToolTip="Data programmata inizio lavori"></asp:Label></td>
                                                                <td>

                                                                    <telerik:RadDatePicker ID="txtDataPGI" runat="server" WrapperTableCaption=""
                                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                                        ShowPopupOnFocus="true">
                                                                        <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                                        </DateInput>
                                                                        <Calendar ID="Calendar3" runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                                <td>&nbsp;
                                                                    <asp:Label ID="Label36" runat="server" Text="Data DPFL" ToolTip="Data programmata fine lavori"></asp:Label></td>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtDataTDL" runat="server" WrapperTableCaption=""
                                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                                        ShowPopupOnFocus="true">
                                                                        <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                                        </DateInput>
                                                                        <Calendar ID="Calendar2" runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label41" runat="server" Text="Data termine Lav." ToolTip="Data termine lavorazione"></asp:Label></td>
                                                                <td>

                                                                    <telerik:RadDatePicker ID="txtDataFineLavori" runat="server" WrapperTableCaption=""
                                                                        MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                                                                        ShowPopupOnFocus="true">
                                                                        <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                            <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                                                            <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                                                                        </DateInput>
                                                                        <Calendar ID="Calendar1" runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today">
                                                                                    <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                    </telerik:RadDatePicker>

                                                                </td>
                                                                <td>&nbsp; </td>
                                                                <td>&nbsp; </td>
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
                                                    <td width="1%">&nbsp;&nbsp; </td>
                                                    <td width="99%">
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text="ELENCO ALLEGATI"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="ContenitoreAllegati" style="overflow: auto; visibility: visible; width: 100%; height: 400px;">
                                                                        <telerik:RadGrid ID="RadGridAllegati" runat="server" AllowAutomaticDeletes="True"
                                                                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True"
                                                                            AllowSorting="True" AutoGenerateColumns="False" Culture="it-IT" GroupPanelPosition="Top"
                                                                            IsExporting="False" OnItemDataBound="OnItemDataBoundHandler" PageSize="20" ShowStatusBar="True"
                                                                            Width="97%">
                                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                            <ExportSettings>
                                                                                <Pdf PageWidth=""></Pdf>
                                                                            </ExportSettings>
                                                                            <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="Top"
                                                                                DataKeyNames="ID" GridLines="None">
                                                                                <CommandItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="InitInsert"><img alt="" src="Immagini/AddNewRecord.png" 
                                                                    style="border:0px" />Aggiungi nuovo Allegato</asp:LinkButton>
                                                                                </CommandItemTemplate>
                                                                                <CommandItemSettings ShowAddNewRecordButton="True" ShowExportToCsvButton="false"
                                                                                    ShowExportToExcelButton="False" ShowExportToPdfButton="false" ShowExportToWordButton="false"
                                                                                    ShowRefreshButton="False" />
                                                                                <Columns>
                                                                                    <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"></telerik:GridEditCommandColumn>
                                                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false"></telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA">
                                                                                        <HeaderStyle Width="10%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPO DOC.">
                                                                                        <HeaderStyle Width="20%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                                                        <HeaderStyle Width="60%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="NOME_FILE" HeaderText="ALLEGATO">
                                                                                        <HeaderStyle Width="10%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridButtonColumn ConfirmText="Eliminare l'elemento selezionato?" ConfirmDialogType="RadWindow"
                                                                                        ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                                                                                        UniqueName="DeleteColumn" ButtonType="ImageButton">
                                                                                        <HeaderStyle Width="50px"></HeaderStyle>
                                                                                    </telerik:GridButtonColumn>
                                                                                </Columns>
                                                                                <EditFormSettings EditFormType="Template" InsertCaption="INSERIMENTO ALLEGATO" PopUpSettings-CloseButtonToolTip="Chiudi"
                                                                                    PopUpSettings-Height="300px" PopUpSettings-Modal="True" PopUpSettings-ShowCaptionInEditForm="True"
                                                                                    PopUpSettings-Width="500px">
                                                                                    <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1"></EditColumn>
                                                                                    <FormTemplate>
                                                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                                                            width="500px">
                                                                                            <tr class="EditFormHeader">
                                                                                                <td width="100%"><b></b></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td width="100%"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top" width="100%">
                                                                                                    <table id="Table3" border="0" class="module" width="100%">
                                                                                                        <tr valign="top">
                                                                                                            <td valign="top" width="10%">Tipologia: </td>
                                                                                                            <td valign="top" width="70%">
                                                                                                                <telerik:RadComboBox ID="cmbTipologia" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                                                                                    Filter="Contains" AutoPostBack="False" Width="250px" Text='<%# Bind("TIPOLOGIA") %>'>
                                                                                                                </telerik:RadComboBox>
                                                                                                            </td>
                                                                                                            <td width="20%">
                                                                                                                <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="cmbTipologia"
                                                                                                                    Display="Static" ErrorMessage="Tipologia obbligatoria!!!" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                                                                                        </tr>
                                                                                                        <tr valign="top">
                                                                                                            <td valign="top" width="10%">Descrizione: </td>
                                                                                                            <td valign="top" width="70%">
                                                                                                                <telerik:RadTextBox ID="txtDescrizioneAllegato" runat="server" Height="100px"
                                                                                                                    Rows="5" Text='<%# Bind("DESCRIZIONE") %>' TextMode="MultiLine" Width="400px">
                                                                                                                </telerik:RadTextBox></td>
                                                                                                            <td width="20%"></td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td></td>
                                                                                                            <td></td>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                        <tr valign="top">
                                                                                                            <td valign="top" width="10%">Allegato: </td>
                                                                                                            <td width="80%">
                                                                                                                <telerik:RadAsyncUpload ID="RadUploadAllegato" runat="server" AllowedFileExtensions="rtf,doc,docx,tiff,pdf,zip,xls,xlsx,jpg,png"
                                                                                                                    MaxFileInputsCount="1" />
                                                                                                            </td>
                                                                                                            <td width="10%"></td>
                                                                                                        </tr>
                                                                                                        <tr valign="top">
                                                                                                            <td>&#160;&#160; </td>
                                                                                                            <td>&#160;&#160; </td>
                                                                                                            <td></td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>&#160;&#160; </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>&#160;&#160; </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>&#160;&#160; </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="right" width="100%">
                                                                                                    <asp:Button ID="btnUpdate" runat="server" CommandName='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                                                                        Text='<%# IIf((TypeOf (Container) Is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />&#160;&#160;<asp:Button
                                                                                                            ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Chiudi" /></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </FormTemplate>
                                                                                    <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="500px" />
                                                                                </EditFormSettings>
                                                                            </MasterTableView><FilterMenu></FilterMenu>
                                                                            <HeaderContextMenu></HeaderContextMenu>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageViewSP" runat="server">
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td width="100%">
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" Text="ELENCO SOPRALLUOGHI"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="ContenitoreSP" style="overflow: auto; visibility: visible; width: 100%;">
                                                                        <telerik:RadGrid ID="RadGridSP" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" Culture="it-IT" IsExporting="False" PageSize="100"
                                                                            Width="97%" GroupPanelPosition="Top">
                                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                            <ExportSettings>
                                                                                <Pdf PageWidth=""></Pdf>
                                                                            </ExportSettings>
                                                                            <MasterTableView AllowFilteringByColumn="False" AllowSorting="True"
                                                                                GridLines="None">
                                                                                <Columns>
                                                                                    <telerik:GridBoundColumn DataField="TECNICO" HeaderText="TECNICO">
                                                                                        <HeaderStyle Width="10%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="RAPPORTO" HeaderText="RAPPORTO">
                                                                                        <HeaderStyle Width="70%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="PERICOLO" HeaderText="PERICOLO">
                                                                                        <HeaderStyle Width="10%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="DATA_SP" HeaderText="DATA" DataFormatString="{0:dd/MM/yyyy}">
                                                                                        <HeaderStyle Width="10%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                </Columns>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView ID="RadPageViewIrr" runat="server">
                                            <br />
                                            <table width="100%">
                                                <tr>
                                                    <td width="100%">
                                                        <table cellpadding="2" cellspacing="2" style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" Text="ELENCO NON CONFORMITA'"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div id="ContenitoreIRR" style="overflow: auto; visibility: visible; width: 100%;">
                                                                        <telerik:RadGrid ID="RadGridIrregolarita" runat="server" AllowPaging="True" AllowSorting="True"
                                                                            AutoGenerateColumns="False" Culture="it-IT" IsExporting="False" PageSize="100"
                                                                            Width="97%" GroupPanelPosition="Top">
                                                                            <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                                            <ExportSettings>
                                                                                <Pdf PageWidth=""></Pdf>
                                                                            </ExportSettings>
                                                                            <MasterTableView AllowFilteringByColumn="False" AllowSorting="True"
                                                                                GridLines="None">
                                                                                <Columns>
                                                                                    <telerik:GridBoundColumn DataField="DATA_ORA" HeaderText="DATA">
                                                                                        <HeaderStyle Width="10%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPO NON CONFORMITA'">
                                                                                        <HeaderStyle Width="30%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                                                        <HeaderStyle Width="60%" />
                                                                                    </telerik:GridBoundColumn>
                                                                                </Columns>
                                                                            </MasterTableView>
                                                                        </telerik:RadGrid>
                                                                    </div>
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
                </asp:Panel>
                <asp:Panel runat="server" ID="divFooter">
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
        <%--</asp:Panel>--%><%-- <asp:RequiredFieldValidator ID="Requiredfieldvalidator3PR" runat="server" ControlToValidate="txtDescrizionePR"
                                                                                                    Display="Static" ErrorMessage="Obbligatorio!" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        </telerik:RadAjaxLoadingPanel>
        <asp:HiddenField ID="HPageMasterHeight" runat="server" ClientIDMode="Static" Value="30" />
        <asp:HiddenField ID="AltezzaRadGrid" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="indiceM" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="indiceS" />
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="indiceSD" />


        <asp:HiddenField runat="server" ClientIDMode="Static" ID="sStatoPreventivo" />
        <script type="text/javascript">
            validNavigation = true;
            $(document).ready(function () {
                Ridimensiona();
            });
            $(window).resize(function () {
                Ridimensiona();
            });
            function Ridimensiona() {
                var altezzaRad = $(window).height() - 200;
                var larghezzaRad = $(window).width() + 500;
                //$("#RadAgenda").width(larghezzaRad);
                $("#RadMultiPage1").height(altezzaRad);
                $("#dgvInterventi").height(altezzaRad - 350);
                $("#ContenitoreAllegati").height(altezzaRad - 100);
                $("#ContenitorePreventivi").height(altezzaRad - 100);
                $("#ContenitoreSP").height(altezzaRad - 100);
                $("#ContenitoreIRR").height(altezzaRad - 100);
                document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
                document.getElementById('AltezzaRadGrid').value = altezzaRad;
            }
            window.focus();

            function StampaOrdine() {
                window.open('../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/StampaOrdine.aspx?COD=' + document.getElementById('indiceM').value, 'Ordine', '');
            }
        </script>
        <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
    </form>
</body>
</html>
