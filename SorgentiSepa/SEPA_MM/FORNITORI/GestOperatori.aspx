<%@ Page Title="" Language="VB" MasterPageFile="~/FORNITORI/HomePage.master" AutoEventWireup="false"
    CodeFile="GestOperatori.aspx.vb" Inherits="FORNITORI_Default" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Gestione Operatori Fornitori"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <table cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <telerik:RadButton ID="btnExport" runat="server" Text="Esporta in Excel" ToolTip="Esporta i risultati in Excel">
                </telerik:RadButton>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function Modifica(sender, args) {
                validNavigation = false;
            }
            function Uscita() {
                validNavigation = true;
                location.href = 'Home.aspx';
            }
            function Conferma(sender, args) {

                var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                    if (shouldSubmit) {
                        Uscita();
                    }
                });
                if (document.getElementById('Modificato').value == '1') {
                    apriConfirm("Sono state effettuate delle modifiche. Uscire senza salvare?", callBackFunction, 300, 150, "Attenzione", null);
                    args.set_cancel(true);
                }
                else {
                    Uscita();
                }
            }
        </script>
    </telerik:RadCodeBlock>
 <telerik:RadGrid ID="RadGridOperatori" runat="server" AllowPaging="True" PageSize="20"
        AllowSorting="True" AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
        AllowAutomaticInserts="True" AllowAutomaticUpdates="True" Width="97%" Culture="it-IT"
        GroupPanelPosition="Top" IsExporting="False" BorderWidth="0px" OnItemDataBound="OnItemDataBoundHandler">
        <MasterTableView AllowFilteringByColumn="False" AllowSorting="True" CommandItemDisplay="Top"
            DataKeyNames="ID" GridLines="None">
            <PagerStyle AlwaysVisible="True" />
            <CommandItemSettings ShowAddNewRecordButton="True" ShowExportToCsvButton="false"
                ShowExportToExcelButton="False" ShowExportToPdfButton="false" ShowExportToWordButton="false"
                ShowRefreshButton="False"  />
            <Columns>
                
                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                    <HeaderStyle Width="15%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                    <HeaderStyle Width="15%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                    <HeaderStyle Width="15%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE">
                    <HeaderStyle Width="15%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DATA_PW" HeaderText="DATA SCAD. PW" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle Width="10%" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RAGIONE_SOCIALE" HeaderText="FORNITORE">
                    <HeaderStyle Width="30%" />
                </telerik:GridBoundColumn>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                    Exportable="false" Visible="true" >
                    <HeaderStyle Width="50px"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn ConfirmText="Eliminare l'operatore selezionato?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Elimina" HeaderText="" HeaderStyle-Width="50px" CommandName="Delete"
                    UniqueName="DeleteColumn" ButtonType="ImageButton" Exportable="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings EditFormType="Template" InsertCaption="INSERIMENTO OPERATORE" PopUpSettings-CloseButtonToolTip="Chiudi"
                PopUpSettings-Height="300px" PopUpSettings-Modal="True" PopUpSettings-ShowCaptionInEditForm="True"
                PopUpSettings-Width="500px">
                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                </EditColumn>
                <FormTemplate>
                    <table id="Table2" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                        width="700px">
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
                            <td width="100%">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="100%">
                                <table id="Table3" border="0" class="module" width="100%" cellpadding="2" cellspacing="2">
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                            Operatore:
                                        </td>
                                        <td valign="top" width="60%">
                                            <telerik:RadTextBox ID="txtOperatore" runat="server" Rows="5" Text='<%# Bind("OPERATORE") %>'
                                                Width="400px" Height="30px" Font-Names="arial" Font-Size="8">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="20%">
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="txtOperatore"
                                                Display="Static" ErrorMessage="Valore obbligatorio!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                            Cognome:
                                        </td>
                                        <td valign="top" width="60%">
                                            <telerik:RadTextBox ID="txtCognome" runat="server" Rows="5" Text='<%# Bind("COGNOME") %>'
                                                Width="400px" Height="30px" Font-Names="arial" Font-Size="8">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="20%">
                                            <%--<asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="txtCognome"
                                                    Display="Static" ErrorMessage="Valore obbligatorio!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                            Nome:
                                        </td>
                                        <td valign="top" width="60%">
                                            <telerik:RadTextBox ID="txtNome" runat="server" Rows="5" Text='<%# Bind("NOME") %>'
                                                Width="400px" Height="30px" Font-Names="arial" Font-Size="8">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="20%">
                                            <%-- <asp:RequiredFieldValidator ID="Requiredfieldvalidator4" runat="server" ControlToValidate="txtNome"
                                                    Display="Static" ErrorMessage="Valore obbligatorio!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                            Cod.Fiscale:
                                        </td>
                                        <td valign="top" width="60%">
                                            <telerik:RadTextBox ID="txtCodFiscale" runat="server" Rows="5" Text='<%# Bind("COD_FISCALE") %>'
                                                Width="400px" Height="30px" Font-Names="arial" Font-Size="8">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="20%">
                                            <%--<asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" ControlToValidate="txtCodFiscale"
                                                    Display="Static" ErrorMessage="Valore obbligatorio!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" width="20%">
                                            Data Scadenza PW
                                        </td>
                                        <td valign="top" width="60%">
                                            <telerik:RadDatePicker ID="txtDataPW" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                                                MaxDate="01/01/9999" Width="120px" WrapperTableCaption="" Font-Names="arial"
                                                Font-Size="8">
                                                <DateInput ID="DateInput4" runat="server">
                                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" OnBlur="CalendarDatePickerHide" />
                                                </DateInput><Calendar ID="Calendar4" runat="server">
                                                    <SpecialDays>
                                                        <telerik:RadCalendarDay Repeatable="Today">
                                                            <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                                        </telerik:RadCalendarDay>
                                                    </SpecialDays>
                                                </Calendar>
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td valign="top" width="20%">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                        </td>
                                        <td valign="top" width="60%">
                                        </td>
                                        <td width="20%">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                            Fornitore:
                                        </td>
                                        <td valign="top" width="60%">

                                            <telerik:RadComboBox ID="cmbFornitore" runat="server" EnableLoadOnDemand="false" IsCaseSensitive="false"  OnSelectedIndexChanged="CaricaAppalti"
                                                Filter="Contains" AutoPostBack="true" Width="400px" Font-Names="arial" Font-Size="8" OnClientSelectedIndexChanged="ImpostaIdFornitore"
                                                Text='<%# Bind("RAGIONE_SOCIALE") %>' CausesValidation="false">
                                            </telerik:RadComboBox>
                                        </td>
                                        <td width="20%">
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="cmbFornitore"
                                                Display="Static" ErrorMessage="Valore obbligatorio!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" width="20%">
                                            Appalto:
                                        </td>
                                        <td valign="top" width="60%">
                                            <telerik:RadComboBox ID="cmbAppalto" runat="server"  IsCaseSensitive="false" EnableLoadOnDemand="false"
                                                Filter="Contains" AutoPostBack="False" Width="400px" Font-Names="arial" Font-Size="8" 
                                                EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="DisplayAllInInput"
                                                CheckBoxes="true" CausesValidation="false"
                                                >
                                            </telerik:RadComboBox>
                                        </td>
                                       <%-- <td width="20%">
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="cmbAppalto"
                                                Display="Static" ErrorMessage="Valore obbligatorio!!!" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>--%>
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
                                <asp:Button ID="btnUpdateOp" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                    Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />&#160;&#160;<asp:Button
                                        ID="btnCancelOp" runat="server" CausesValidation="False" CommandName="Cancel"
                                        Text="Chiudi" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &#160;&#160;
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
            <Pdf PageWidth="">
            </Pdf>
            <Excel FileExtension="xlsx" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" ClientEvents-OnBatchEditCellValueChanged="Modifica"
            ClientEvents-OnRowDeleted="Modifica">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
        <PagerStyle AlwaysVisible="True" />
    </telerik:RadGrid>    <asp:HiddenField ID="AltezzaRadGrid" Value="500" runat="server" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenField1" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivHeight" Value="100" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HfContenteDivWidth" Value="1" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="Modificato" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="vOperatore" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="vCognome" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="vNome" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="vCF" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="vFornitore" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="vDataPW" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="isExporting" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="HiddenIdFornitore" Value ="-1" />
    
    <script type="text/javascript">

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            Ridimensiona();
        });
        $(window).resize(function () {
            Ridimensiona();
        });
        function Ridimensiona() {
            var altezzaRad = $(window).height() - 200;
            var larghezzaRad = $(window).width() - 27;
            $("#MasterPage_CPContenuto_RadGridOperatori").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridOperatori").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        }

        function ImpostaIdFornitore(sender,args) {
            document.getElementById('HiddenIdFornitore').value = sender._value;
        };
    </script>
    <script src="../Standard/Scripts/jsfunzioniExit.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
</asp:Content>
