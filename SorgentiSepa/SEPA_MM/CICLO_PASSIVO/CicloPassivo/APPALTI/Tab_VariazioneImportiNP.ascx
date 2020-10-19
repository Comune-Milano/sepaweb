<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_VariazioneImportiNP.ascx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_VariazioneImportiNP" %>
<style type="text/css">
    .style1
    {
        height: 19px;
    }
</style>
<script type="text/javascript">
    var Selezionato;
</script>
<telerik:RadWindow ID="RadWindowVarImporti" runat="server" CenterIfModal="true" Modal="true"
    Title="Gestione variazioni" Width="680px" Height="330px" VisibleStatusbar="false"
    Behaviors="Pin, Maximize, Move, Resize">
    <ContentTemplate>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <asp:Panel runat="server" ID="PanelVarImporti" Style="height: 100%;" class="sfondo">
            <table style="width: 85%;">
                <tr>
                    <td colspan="2" class="TitoloModulo">
                        <asp:Label ID="lblTitle" runat="server" Text="VARIAZIONE" Width="390px"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <td>
                       <table>
                           <tr>
                               <td>
                                     <asp:Button ID="btn_AddVariazAutoCan" runat="server" text="Aggiorna"
                            OnClientClick="document.getElementById('USCITA').value='1'" Style="cursor: pointer"
                            TabIndex="55" ToolTip="Salva" />
                               </td>
                               <td>
                                     <asp:Button ID="btn_ChiudiAppalti" runat="server" Text="Esci"
                            OnClientClick="document.getElementById('USCITA').value='1';closeWindow(null, null, 'Tab_VariazioneImportiNP1_RadWindowVarImporti');" Style="cursor: pointer"
                            TabIndex="57" ToolTip="Esci senza inserire" />
                               </td>
                           </tr>
                       </table>
                   </td>
                  
                </tr>
                
                <tr>
                    <td class="style2">
                        <asp:Label ID="Label5" runat="server" Text="Provvedimento" Width="74px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" BackColor="White" MaxLength="180" TabIndex="31"
                            TextMode="MultiLine" Width="539px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="Label2" runat="server" Text="Tipo variazione" Width="100px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlTipoVariazione" Width="300" AppendDataBoundItems="true" Visible="false"
                            Enabled="false" Filter="Contains" runat="server" AutoPostBack="false" HighlightTemplatedItems="true"
                            LoadingMessage="Caricamento...">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Label ID="Label3" runat="server" Text="Data" Width="74px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtData" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                            DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110">
                            <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa" LabelWidth="28px"
                                Width="70px">
                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                            </DateInput>
                            <Calendar ID="Calendar3" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                
                <tr>
                    <td class="style2">
                        <asp:Label ID="lblImporto" runat="server" Text="Importo" Width="82px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtImportoVar" runat="server" MaxLength="15" Style="text-align:right" Width="80px"></telerik:RadNumericTextBox>
                        
                    </td>
                </tr>
                <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Oneri" Width="82px"></asp:Label>
            </td>
            <td>
                 <telerik:RadNumericTextBox ID="txtImportoOneri" runat="server" MaxLength="15" Style="text-align:right" Width="80px"></telerik:RadNumericTextBox>
                
            </td>
        </tr>
                <%--<tr>
            <td colspan="3">
                <div id="DivVarImporti" style="border: medium solid #ccccff; vertical-align: top;
                    overflow: auto; text-align: left; height: 271px;">
                    <asp:DataGrid ID="DgvServAutoCan" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                        Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                        top: 32px" Width="98%">
                        <Columns>
                            <asp:BoundColumn DataField="ID_SERVIZIO" HeaderText="ID_SERVIZIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SERVIZIO" HeaderText="SERVIZIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="VOCE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="IMPORTO ">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtImpCanone" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Style="text-align: right;" Width="80px" MaxLength="10"></asp:TextBox>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="IMPORTO_CANONE" HeaderText="IMPORTO_CANONE" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:DataGrid></div>
            </td>
        </tr>--%>
                
            </table>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>
<table cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td class="TitoloH1" style="text-align: left">
            <asp:Label ID="lblAPPALTI" runat="server" Text="Elenco delle variazioni importi"
                Width="391px"></asp:Label>
        </td>
        <td style="text-align: left; vertical-align: top;" class="style1">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;">
            <div style="overflow: auto; width: 100%; height: 100%;">
                <telerik:RadGrid ID="DataGridVCan" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
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
                            </telerik:GridBoundColumn>
                           <telerik:GridBoundColumn DataField="DATA_ORA_OP" HeaderText="DATA VARIAZIONE" ReadOnly="True">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Width="30%" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_CONSUMO" HeaderText="IMPORTO CONSUMO" ReadOnly="True">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ONERI_SICUREZZA_CONSUMO" HeaderText="ONERI DI SICUREZZA CONSUMO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                       
                        </Columns>
                        <CommandItemTemplate>
                            <a id="addServizio" style="cursor: pointer" onclick="openWindow(null, null, 'Tab_VariazioneImportiNP1_RadWindowVarImporti')">
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
        </td>
    </tr>
    <tr>
        <td style="text-align: left; vertical-align: top;" class="TitoloH1">
            <asp:Label ID="txtmia" runat="server" Text="Nessuna selezione" Width="391px"></asp:Label><asp:ImageButton
                ID="imgModVarImporti" runat="server" OnClientClick="ApriVarImporti();" ImageUrl="../../../NuoveImm/Img_Modifica.png"
                TabIndex="15" ToolTip="Modifica la variazione selezionata" Style="visibility: hidden" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="idAppalti" runat="server" Value="" />
<asp:HiddenField ID="hfCanSelected" runat="server" Value="0" />
<asp:HiddenField ID="hfConSelected" runat="server" Value="0" />
<asp:HiddenField ID="PercUsCanone" runat="server" Value="0" />
<asp:HiddenField ID="SpalmCanone" runat="server" Value="0" />
<asp:HiddenField ID="hfRestaVisible" runat="server" Value="0" ClientIDMode="Static" />
<asp:HiddenField ID="PercUsCons" runat="server" Value="0" />
<asp:HiddenField ID="SpalmCons" runat="server" Value="0" />
<asp:HiddenField ID="hfRestaVisibleCon" runat="server" Value="0" />
<asp:HiddenField ID="idSelected" runat="server" Value="" />
<asp:HiddenField ID="hfElimina" runat="server" Value="0" />
<asp:HiddenField ID="HFidGruppoVariazione" runat="server" Value="0" />
<script language="javascript" type="text/javascript">



    //    if (document.getElementById('hfRestaVisible').value == 1) {
    //        document.getElementById('VarImporti').style.visibility = 'visible';
    //    };
</script>
