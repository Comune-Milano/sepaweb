<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneMotiviProcessDecis.aspx.vb"
    Inherits="Contratti_GestioneMotiviProcessDecis" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestione motivazioni processo decisionale</title>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../StandardTelerik/Style/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .bottone2 {
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

            .bottone2:hover {
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
        function RowSelectingMotivo(sender, args) {
            document.getElementById('idSelectedMotivo').value = args.getDataKeyValue("IDMOTIVO");
            document.getElementById('idSelectedTipoIst').value = args.getDataKeyValue("IDTIPOISTANZA");
        };
        function ModificaDblClickMotivo() {

        };

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
                <asp:ScriptReference Path="../Standard/Scripts/jsMessage.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <asp:Panel runat="server" ID="Panel1">
            <div>
                <table>
                    <tr>
                        <td>
                            <br />
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp&nbsp
                            Motivi Processo Decisionale </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding-left: 10px;">
                <table>
                    <tr>
                        <td>
                            <div style="width: 670px; overflow: auto;">
                                <telerik:RadGrid ID="RadGridMotivi" runat="server" PageSize="50" AllowSorting="True"
                                    AutoGenerateColumns="False" ShowStatusBar="True" AllowAutomaticDeletes="True"
                                    AllowAutomaticUpdates="True" Width="100%" Culture="it-IT" GroupPanelPosition="Top"
                                    IsExporting="False" BorderWidth="0px" RegisterWithScriptManager="true">
                                    <MasterTableView runat="server" CommandItemDisplay="Top" EditMode="Batch" ClientDataKeyNames="IDMOTIVO,IDTIPOISTANZA,ACCOGLIMENTO"
                                        DataKeyNames="IDMOTIVO,IDTIPOISTANZA,ACCOGLIMENTO">
                                        <BatchEditingSettings EditType="Cell" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                        <GroupByExpressions>
                                            <telerik:GridGroupByExpression>
                                                <SelectFields>
                                                    <telerik:GridGroupByField FieldAlias="TIPO" FieldName="DESCRIZIONE" HeaderValueSeparator=" PROCESSO: "></telerik:GridGroupByField>
                                                </SelectFields>
                                                <GroupByFields>
                                                    <telerik:GridGroupByField FieldName="DESCRIZIONE"></telerik:GridGroupByField>
                                                </GroupByFields>
                                            </telerik:GridGroupByExpression>
                                        </GroupByExpressions>
                                        <Columns>

                                            <telerik:GridBoundColumn DataField="ID" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ID2" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IDMOTIVO" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="IDTIPOISTANZA" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="TIPO PROCESSO" HeaderStyle-Width="100%"
                                                ItemStyle-CssClass="maximize" ReadOnly="True">
                                                <HeaderStyle Width="100%" HorizontalAlign="Center"></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MOTIVO" HeaderText="MOTIVO" HeaderStyle-Width="100%"
                                                UniqueName="CambiaColumn2" ItemStyle-CssClass="maximize" HeaderStyle-HorizontalAlign="Center">
                                                <ColumnValidationSettings EnableRequiredFieldValidation="true">
                                                    <RequiredFieldValidator ForeColor="Red" ErrorMessage="   !" ToolTip="Il campo non può essere nullo"></RequiredFieldValidator>
                                                </ColumnValidationSettings>
                                                <HeaderStyle Width="100%"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn HeaderText="DECISIONE" DataField="ACCOGLIMENTO" AllowFiltering="FALSE" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "ACCOGLIMENTO")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <telerik:RadComboBox ID="cmbDecisioni" Style="z-index: 10000000" EnableLoadOnDemand="True" Width="100%" AppendDataBoundItems="true"
                                                        DataTextField="DECISIONE" DataValueField="ID" OnItemsRequested="cmbDecisioni_ItemsRequested"
                                                        runat="server" AutoPostBack="false" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                        LoadingMessage="Caricamento...">
                                                    </telerik:RadComboBox>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="100%"></HeaderStyle>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="FRASE_STAMPA" HeaderText="FRASE STAMPA" HeaderStyle-Width="100%"
                                                ItemStyle-CssClass="maximize">
                                                <HeaderStyle Width="100%" HorizontalAlign="Center"></HeaderStyle>
                                            </telerik:GridBoundColumn>

                                        </Columns>

                                    </MasterTableView>
                                    <ClientSettings ReorderColumnsOnClient="True" AllowDragToGroup="True" AllowColumnsReorder="True">
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <Resizing AllowRowResize="True" AllowColumnResize="True" EnableRealTimeResize="True"
                                            ResizeGridOnColumnResize="False"></Resizing>
                                        <ClientEvents OnRowSelecting="RowSelectingMotivo" OnRowClick="RowSelectingMotivo"
                                            OnRowDblClick="ModificaDblClickMotivo" />
                                    </ClientSettings>
                                    <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            <table>
                                 <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonSegnaposto" runat="server" Text="Elenco Segnaposto"
                                            AutoPostBack="false" Width="104px" OnClientClicking="function(sender, args){CenterPage('../Gestione_locatari/ElencoSegnaposto.aspx', 'ElencoSegnaposto', 1000, 800);}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonAggMotivo" runat="server" Text="Agg. Motivo"
                                            AutoPostBack="false" Width="104px" OnClientClicking="function(sender, args){openWindow(sender, args, 'RadWindowMotiviDecisioni');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <telerik:RadButton ID="RadButtonElimina" runat="server" Text="Elimina" Width="104px"
                                            OnClientClicking="function(sender, args){deleteElementTelerik(sender, args, 'idSelectedMotivo');}">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp</td>
                    </tr>
                    <tr>
                        <td>&nbsp</td>
                    </tr>
                    <tr>
                        <td>&nbsp</td>
                    </tr>
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
        <telerik:RadWindow ID="RadWindowMotiviDecisioni" runat="server" CenterIfModal="true" Modal="true"
            Title="Aggiungi" Width="630px" Height="250px" VisibleStatusbar="false"
            Behaviors="Pin, Maximize, Move, Resize">
            <ContentTemplate>
                <asp:Panel runat="server" ID="PanelMotiviDecisioni" Style="height: 100%;" class="sfondo">
                    <table style="width: 100%;">
                        <tr>
                            <td class="TitoloModulo">Nuovo motivo
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btnSalvaMotivoDec" runat="server" Text="Salva" ToolTip="Salva" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <telerik:RadButton ID="btnChiudiMotivoDec" runat="server" Text="Esci" ToolTip="Esci"
                                                OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowMotiviDecisioni');}" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 25%">Motivo*:
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtMotivo" runat="server" Width="80%"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">Tipo istanza*:
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbTipoIstanza" Width="80%" AppendDataBoundItems="true" Filter="Contains"
                                                runat="server" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">Fase decisionale*:
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbDecisioni" Width="80%" AppendDataBoundItems="true"
                                                Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">Frase stampa:
                                        </td>
                                        <td>
                                            <telerik:RadTextBox ID="txtFraseSt" runat="server" Width="80%" TextMode="MultiLine"></telerik:RadTextBox>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>

                    </table>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
        <asp:Panel runat="server" ID="PanelHidden">
            <asp:HiddenField ID="idSelectedMotivo" runat="server" Value="" ClientIDMode="Static" />
            <asp:HiddenField ID="idSelectedTipoIst" runat="server" Value="" ClientIDMode="Static" />
            <asp:HiddenField ID="HFPathLock" runat="server" Value="" ClientIDMode="Static" />
        </asp:Panel>
    </form>
    <script type="text/javascript">


        function SaveBatchEditPostback() {

            var grid = $find("<%=RadGridMotivi.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var batchManager = grid.get_batchEditingManager();
            var hasChanges = batchManager.hasChanges(masterTable);
            if (hasChanges) {
                batchManager.saveChanges(masterTable);
            }
            else {
                document.getElementById('btnSalva1').click();
            };
        };

    </script>
</body>
</html>
