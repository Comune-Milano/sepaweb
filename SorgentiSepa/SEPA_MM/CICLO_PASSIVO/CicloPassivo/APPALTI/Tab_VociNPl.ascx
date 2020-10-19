<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_VociNPl.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VociNPl" %>
<style type="text/css">
    .style1 {
        height: 14px;
        width: 88px;
    }

    .style2 {
        width: 88px;
    }
</style>
<link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
<telerik:RadWindow ID="RadWindowServizi" runat="server" CenterIfModal="true" Modal="true"
    Title="Gestione servizi voci" Width="680px" Height="330px" VisibleStatusbar="false"
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
                                    <asp:Button ID="btn_InserisciAppalti" runat="server" text="Aggiorna"
                                        OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_VociNPl1_divVisible').value = '1';"
                                        Style="cursor: pointer" TabIndex="55" ToolTip="Salva" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_ChiudiAppalti" runat="server" text="Esci"
                                        OnClientClick="closeWindow(null, null, 'Tab_VociNPl1_RadWindowServizi');return false;"
                                        Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: left;">
                        <br />
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">Scegli una voce del B.P.</asp:Label><br />
                        <telerik:RadComboBox ID="cmbvoce" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                            Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Height="150"
                            ResolvedRenderMode="Classic" Width="70%">
                        </telerik:RadComboBox>

                        <table width="90%">
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
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="75px">Consumo</asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtimportoconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="24" Width="100px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtscontoconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="25" Width="60px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="%" Width="16px"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOnerconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="25" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtperconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="16" Width="50px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label100" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
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
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Visible="False">Canone</asp:Label>
                                </td>
                                <td style="text-align: center">
                                    <asp:TextBox ID="txtimportocorpo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="21" Width="100px" Visible="False"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€" Width="16px"
                                        Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtscontocorpo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="22" Width="60px" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="%" Width="16px"
                                        Visible="False"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOneriCanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="10" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="25" Width="80px" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="61" Text="€" Visible="False"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpercanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="16" Width="50px" ReadOnly="True" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label101" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: left" TabIndex="-1" Text="%" Visible="False"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:TextBox ID="txtivacorpo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="2" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                        TabIndex="23" Width="30px" ReadOnly="True" Visible="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="%" Visible="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <br />
                        <asp:HiddenField ID="controllaservizio" runat="server" />
                        <asp:HiddenField ID="txtIDS" runat="server" />
                        <asp:HiddenField ID="perconsumo" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>

        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>

<table id="TABBLE_LISTA" style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblAPPALTI" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Text="ELENCO VOCI SERVIZI" Width="223px"></asp:Label>
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
                        CommandItemDisplay="Top" Width="100%">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="False" DataField="ID_LOTTO" HeaderText="ID_LOTTO"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESC_PF" HeaderText="VOCE P.F." Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO" Visible="False">
                                <HeaderStyle Width="20%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PF_VOCE_IMPORTO" HeaderText="ID_PF_VOCE_IMPORTO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE VOCE SERVIZIO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="20%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_CANONE" HeaderText="IMPORTO CANONE" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IVA_CANONE" HeaderText="IVA CANONE" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SCONTO_CANONE" HeaderText="SCONTO CANONE" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_CONSUMO" HeaderText="IMPORTO CONSUMO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SCONTO_CONSUMO" HeaderText="SCONTO CONSUMO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ONERI_SICUREZZA_CONSUMO" HeaderText="ONERI">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="8%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IVA_CONSUMO" HeaderText="IVA CONSUMO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="True" HorizontalAlign="Center" Width="8%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                           <telerik:GridButtonColumn UniqueName="modificaServizio" HeaderText="" ButtonType="ImageButton"
                                ShowInEditForm="true" CommandName="myEdit" ConfirmDialogType="RadWindow" ButtonCssClass="rgEdit">
                                <HeaderStyle Width="3%" />
                                <ItemStyle Width="24px" Height="24px" />
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn CommandName="Delete" ImageUrl="../Immagini/Delete.gif"
                                Text="Elimina" UniqueName="DeleteColumn" ButtonType="ImageButton" ButtonCssClass="rgDel">
                                  <HeaderStyle Width="3%" />
                                <ItemStyle Width="24px" Height="24px" />
                                <HeaderStyle HorizontalAlign="Center"  />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <a id="addServizio" style="cursor: pointer" onclick="openWindow(null, null, 'Tab_VociNPl1_RadWindowServizi')">
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
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
            <asp:TextBox ID="txtSelAppalti" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="670px"></asp:TextBox><br />
        </td>
                  
            <div style="visibility:hidden;">
 <asp:Button ID="btnApriAppalti" runat="server" CausesValidation="False" Height="12px"
                           
                            ImageUrl="../../../NuoveImm/Img_Modifica.png" TabIndex="16" ToolTip="Modifica voce selezionata" />
            </div>
            <asp:HiddenField ID="txtAppareP" runat="server" />
            <asp:HiddenField ID="txtIdComponente" runat="server" />
            <asp:HiddenField ID="txtIdComponente0" runat="server" />
            <asp:HiddenField ID="txtIdComponente1" runat="server" />
            <asp:HiddenField ID="txtannullo" runat="server" />
     
    </tr>
</table>
<script type="text/javascript">

</script>
<div id="Voci" style="border: thin none #3366ff; left: 0px; width: 802px; position: absolute; top: -5px; height: 251px; visibility: visible; vertical-align: top; text-align: left; z-index: 201; margin-right: 10px;">
    <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 12px; position: absolute; top: 59px; height: 186px; width: 771px;" />

    <asp:HiddenField ID="divVisible" runat="server" Value="0" />
</div>
<script type="text/javascript">

    //if (document.getElementById('Tab_VociNPl1_divVisible').value == 0) {
        document.getElementById('Voci').style.visibility = 'hidden';
    //}
    //else {
    //    document.getElementById('Voci').style.visibility = 'visible';
    //}
</script>
