<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Servizio.ascx.vb"
    Inherits="Tab_Servizio" %>
<link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
<telerik:RadWindow ID="RadWindowServizi" runat="server" CenterIfModal="true" Modal="true"
    Title="Gestione servizi" Width="680px" Height="330px" VisibleStatusbar="false"
    Behaviors="Pin, Maximize, Move, Resize">
    <ContentTemplate>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <asp:Panel runat="server" ID="PanelServiziVoci" Style="height: 100%;" class="sfondo">
            <table class="sfondo">
                <tr>
                    <td class="TitoloH1">Gestione voci di servizio
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btn_InserisciAppalti" runat="server" Text="Aggiorna" ToolTip="Salva"
                                        OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';}"
                                        CausesValidation="False">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <telerik:RadButton ID="btn_ChiudiAppalti" runat="server" Text="Esci" ToolTip="Esci senza inserire"
                                        CausesValidation="False" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';closeWindow(sender, args, 'Tab_Servizio_RadWindowServizi');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: left;">
                        <telerik:RadComboBox ID="cmbservizio" runat="server" AppendDataBoundItems="true" Height="150"
                            AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                            ResolvedRenderMode="Classic" Width="70%">
                        </telerik:RadComboBox>
                        <br />
                        <asp:Label ID="Label1111" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">Scegli una voce di servizio</asp:Label><br />
                        <telerik:RadComboBox ID="cmbvoce" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnClientSelectedIndexChanging="function(sender,args){nascondi=0;}"
                            Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Height="150"
                            ResolvedRenderMode="Classic" Width="70%">
                        </telerik:RadComboBox>
                        &nbsp;<table>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td style="text-align: center; vertical-align: top">
                                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Importo Base Asta</asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="vertical-align: top; text-align: center">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Sconto</asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblOneri" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Oneri</asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblOneri0" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Oneri %</asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="vertical-align: top; text-align: center">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">IVA</asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="vertical-align: top; text-align: center">
                                    <asp:Label ID="Label102" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Freq.Pagamento*</asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Canone</asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtimportocorpo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="21" Width="80px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€" Width="12px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtscontocorpo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="22" Width="60px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label10000000" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="text-align: left" TabIndex="61" Text="%"
                                        Width="15px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOnerCanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="22" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpercanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="-1" Width="50px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label100" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="-1" Text="%"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtivacorpo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="2" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="23" Width="30px" EnableTheming="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label55555" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="61" Text="%" Width="16px"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbFreqPagamento" runat="server" Font-Names="arial" Font-Size="8pt"
                                        SelectedValue='<%# DataBinder.Eval(Container, "DataItem.FREQ_PAGAMENTO") %>'
                                        TabIndex="11" Width="100px">
                                        <asp:ListItem Value="0">Non Definito</asp:ListItem>
                                        <asp:ListItem Value="1">Mensile</asp:ListItem>
                                        <asp:ListItem Value="2">Bimestrale</asp:ListItem>
                                        <asp:ListItem Value="3">Trimestrale</asp:ListItem>
                                        <asp:ListItem Value="4">Quadrimestrale</asp:ListItem>
                                        <asp:ListItem Value="6">Semestrale</asp:ListItem>
                                        <asp:ListItem Value="12">Annuale</asp:ListItem>
                                        <asp:ListItem Value="13">Manuale</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnDate" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/APPALTI/Immagini/event-search-icon.png"
                                        OnClientClick="ApriModalScadenze();return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6666" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="80px">Consumo</asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtimportoconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="24" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label7777" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€" Width="12px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtscontoconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="25" Width="60px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label8888" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="%" Width="15px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOneriConsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="25" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label22221" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtperconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="-1" Width="50px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label101" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="-1" Text="%"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtivaconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="2" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="26" Width="30px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="61" Text="%" Width="16px"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>
<table id="TABBLE_LISTA" style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblAPPALTI" runat="server" CssClass="TitoloH1" Text="Elenco servizi"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <div style="overflow: auto; width: 100%; height: 100%;">
                <telerik:RadGrid ID="DataGrid3" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                    IsExporting="False" PagerStyle-AlwaysVisible="true">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        CommandItemDisplay="Top" Width="200%">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="False" DataField="ID_LOTTO" HeaderText="ID_LOTTO">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESC_PF" HeaderText="VOCE P.F.">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="150px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO">
                                <HeaderStyle Width="150px" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PF_VOCE_IMPORTO" HeaderText="ID_PF_VOCE_IMPORTO"
                                Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE VOCE SERVIZIO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="150px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_CANONE" HeaderText="IMPORTO CANONE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SCONTO_CANONE" HeaderText="SCONTO CANONE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ONERI_SICUREZZA_CANONE" HeaderText="ONERI CANONE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IVA_CANONE" HeaderText="IVA CANONE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_CONSUMO" HeaderText="IMPORTO CONSUMO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SCONTO_CONSUMO" HeaderText="SCONTO CONSUMO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ONERI_SICUREZZA_CONSUMO" HeaderText="ONERI CONSUMO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IVA_CONSUMO" HeaderText="IVA CONSUMO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn UniqueName="modificaServizio" HeaderText="" ButtonType="ImageButton"
                                ShowInEditForm="true" CommandName="myEdit" ConfirmDialogType="RadWindow" ButtonCssClass="rgEdit">
                                <HeaderStyle Width="3%" />
                                <ItemStyle Width="24px" Height="24px" />
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn HeaderStyle-Width="3%" CommandName="Delete" ImageUrl="../Immagini/Delete.gif"
                                Text="Elimina" UniqueName="DeleteColumn" ButtonType="ImageButton" ButtonCssClass="rgDel">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="24px" Height="24px" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <a id="addServizio" style="cursor: pointer" onclick="openWindow(null, null, 'Tab_Servizio_RadWindowServizi')">
                                <img style="border: 0px" alt="" src="../../Immagini/addRecord.gif" />
                                Aggiungi nuovo record</a>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                        <Excel FileExtension="xls" Format="Xlsx" />
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
            <telerik:RadButton ID="btnApriServizioAppalto" runat="server" Style="visibility: hidden">
            </telerik:RadButton>
            <asp:TextBox ID="txtSelAppalti" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="670px"></asp:TextBox>
        </td>
    </tr>
</table>
<asp:HiddenField ID="txtIdComponente0" runat="server" />
<asp:HiddenField ID="txtIdComponente" runat="server" />
<asp:HiddenField ID="txtAppareP" runat="server" />
<asp:HiddenField ID="txtannullo" runat="server" />
<asp:HiddenField ID="txtIdComponente1" runat="server" />
<asp:HiddenField ID="txtAppareF" runat="server" />
<asp:HiddenField ID="txtTipoF" runat="server" />
<%--<div id="DIV_Appalti" style="border: thin none #3366ff; width: 802px; position: absolute;
    height: 540px; left: -5px; top: -20px; visibility: visible; vertical-align: top;
    text-align: left; z-index: 201; margin-right: 10px;">
    &nbsp;
    <asp:DropDownList ID="cmbservizio1" runat="server" AutoPostBack="True" BackColor="White"
        Font-Names="arial" Font-Size="8pt" Height="20px" Style="border-right: black 1px solid;
        border-top: black 1px solid; z-index: 10; left: 142px; border-left: black 1px solid;
        border-bottom: black 1px solid; top: 224px" TabIndex="19" Width="652px">
    </asp:DropDownList>
    <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="../../../ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 24px; background-image: url('../../../ImmDiv/DivMGrande.png');
        position: absolute; top: 51px; height: 274px; width: 732px;" />
    <table style="border-color: #6699ff; border-width: thin; z-index: 105; left: 49px;
        width: 689px; margin-right: 0px; position: absolute; top: 67px; height: 220px;">
    </table>
    &nbsp; &nbsp;<strong><span style="color: #660000; font-family: Arial"></span></strong><br />
    &nbsp; &nbsp; &nbsp;&nbsp; <strong><span style="color: #660000; font-family: Arial">
        Gestione Servizi</span></strong><br />
</div>--%>
<script type="text/javascript">

    function controlla_div() {
        if (document.getElementById('Tab_Servizio_txtIdComponente0').value != "") {
            document.getElementById('Tab_Servizio_txtAppareP').value = '1';
            //  document.getElementById('DIV_Appalti').style.visibility = 'visible';
        }
        else {
            alert('Nessuna riga selezionata!');
        }
    }


    //    function controlla_div2() {
    //        if (document.getElementById('Tab_Servizio_txtIdFornitore').value != "") {
    //            document.getElementById('Tab_Servizio_txtAppareF').value = '1';
    //            document.getElementById('DIV_Fornitori').style.visibility = 'visible';
    //        }
    //        else {
    //            alert('Nessun fornitore selezionato!');
    //        }
    //    }

    //    if (document.getElementById('Tab_Servizio_txtAppareP').value != '1') {
    //        document.getElementById('DIV_Appalti').style.visibility = 'hidden';
    //    }



</script>
<asp:HiddenField ID="controllaservizio" runat="server" />
<asp:HiddenField ID="txtIDS" runat="server" />
<asp:HiddenField ID="perconsumo" runat="server" />
<asp:HiddenField ID="percanone" runat="server" />
<asp:HiddenField ID="idvoce" runat="server" />
<asp:HiddenField ID="PagatoCanone" runat="server" Value="0" />
<asp:HiddenField ID="PagatoConsumo" runat="server" Value="0" />
