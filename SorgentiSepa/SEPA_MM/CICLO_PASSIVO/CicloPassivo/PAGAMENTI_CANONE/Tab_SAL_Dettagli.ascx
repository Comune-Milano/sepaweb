<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SAL_Dettagli.ascx.vb"
    Inherits="Tab_SAL_Dettagli" %>
<table style="width: 100%">

    <tr>
        <td class="TitoloH1" style="text-align: left">&nbsp;<asp:Label ID="lblELENCO_INTERVENTI" runat="server" TabIndex="-1" Text="Elenco pagamenti a canone"
            Width="248px"></asp:Label>&nbsp;
        </td>
        <td style="height: 21px"></td>
        <td style="height: 21px">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 95%">
            <div style="overflow: auto; width: 100%;">
                <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="false"
                    IsExporting="False" PagerStyle-AlwaysVisible="false" AllowPaging="false">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <HeaderStyle Width="0%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PENALE" HeaderText="ID_PENALE" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PROG_ANNO" HeaderText="PROG/ANNO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" Width="7%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_PRENOTAZIONE" HeaderText="DATA PREN.">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VOCE_SERVIZIO" HeaderText="VOCE BP">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PREN_LORDO" HeaderText="IMP. PRENOTATO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CONS_LORDO" HeaderText="IMP. APPROVATO">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PENALE" HeaderText="PENALE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <%--  <asp:TemplateColumn HeaderText="Selezione" Visible="False">
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
                        </asp:TemplateColumn>--%>
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
            </div>
            <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox>
        </td>
        <td>&nbsp; &nbsp;
        </td>
        <td>
            <table>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Button ID="btnApri1" runat="server" CausesValidation="False"
                            Text="Modifica" OnClientClick="document.getElementById('Tab_SAL_Dettagli_txtAppare1').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_1').style.visibility='visible';"
                            TabIndex="11" ToolTip="Modifica gli importi" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 14px"></td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>

<telerik:RadWindow ID="RadWindowServizi" runat="server" CenterIfModal="true" Modal="true"
    Title="Modifica pagamento a canone" Width="800px" Height="500px" VisibleStatusbar="false"
    Behaviors="Pin, Maximize, Move, Resize">
    <ContentTemplate>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <asp:Panel runat="server" ID="PanelServiziVoci" Style="height: 100%;" class="sfondo">
            <table class="sfondo">
                <tr>
                    <td>
                        <strong><span style="color: #0000ff; font-family: Arial">Dettaglio Pagamento</span></strong>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="Table5">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black"
                                        Width="110px">Voce BP</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoce" runat="server" Enabled="False" Font-Size="8pt" MaxLength="300"
                                        ReadOnly="True"
                                        Font-Bold="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDettaglio" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black"
                                        Width="110px"> Voce DGR</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVoceDettaglio" runat="server" Enabled="False" Font-Size="8pt"
                                        MaxLength="300" ReadOnly="True"
                                        TabIndex="15" Width="550px" Font-Bold="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVal1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="220px">Budget o consistenza inizale</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:Label ID="lblVal3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Width="120px">Totale Prenotazione</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto2" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblVal2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="220px">Budget assestato o consistenza assestante</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto1" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:Label ID="lblVal4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="120px">Totale Consuntivazione</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto3" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="width: 20px"></td>
                                <td>
                                    <asp:Label ID="lblVal5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="120px">Totale Pagato</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto4" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblval6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="220px">Disponibilità residua</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtImporto5" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 20px"></td>
                                <td>&nbsp;<asp:HyperLink ID="HLink_ElencoPag" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" Font-Underline="True" ForeColor="Blue" Style="cursor: hand" ToolTip="Visualizza tutti le prenotazioni e pagamenti della voce BP"
                                    Visible="False" Width="120px">Dettaglio Pagamenti</asp:HyperLink>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIVA" runat="server" Enabled="False" Font-Bold="True" MaxLength="30"
                                        ReadOnly="True" Style="text-align: right"
                                        TabIndex="-1" Visible="False" Width="60px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="height: 5px"></td>
                                <td style="height: 5px"></td>
                                <td style="height: 5px"></td>
                                <td style="width: 364px; height: 5px"></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="#0000C0" TabIndex="-1"
                                        Width="150px">IMPORTO PRENOTATO</asp:Label>
                                </td>
                                <td style="height: 21px"></td>
                                <td style="height: 21px"></td>
                                <td style="width: 364px; height: 21px;"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="150px">A netto compresi oneri e IVA</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNettoOneriIVA" runat="server" Enabled="False" Font-Bold="True"
                                        Font-Size="8pt" MaxLength="30" ReadOnly="True" Style="text-align: right" TabIndex="-1" Width="120px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblEuro8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 364px"></td>
                            </tr>
                            <tr>
                                <td style="height: 5px"></td>
                                <td style="height: 5px"></td>
                                <td style="height: 5px"></td>
                                <td style="width: 364px; height: 5px"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="#0000C0" TabIndex="-1"
                                        Width="150px">IMPORTO IN APPROVAZIONE</asp:Label>
                                </td>
                                <td></td>
                                <td></td>
                                <td style="width: 364px"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="150px">A netto compresi oneri e IVA</asp:Label>
                                </td>
                                <td>

                                    <telerik:RadNumericTextBox ID="txtNettoOneriIVA2" runat="server" MaxLength="15" Style="text-align: right" Width="80px"></telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 364px"></td>
                            </tr>
                            <tr>
                                <td style="height: 5px"></td>
                                <td style="height: 5px"></td>
                                <td style="height: 5px"></td>
                                <td style="width: 364px; height: 5px"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPenale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" TabIndex="-1" Width="80px">Penale *</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPenale" runat="server" MaxLength="15" Style="text-align: right" Width="80px"></telerik:RadNumericTextBox>

                                </td>
                                <td>
                                    <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 364px"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btn_Inserisci1" runat="server" Text="Salva"
                                        OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_SAL_Dettagli_txtAppare1').value='0';"
                                        Style="cursor: pointer" TabIndex="4" ToolTip="Salva le modifiche apportate" />
                                    <asp:Button ID="btn_Chiudi1" runat="server" Text="Esci"
                                        OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_1').style.visibility='hidden';closeWindow(null, null, 'Tab_SAL_Dettagli_RadWindowServizi');document.getElementById('Tab_SAL_Dettagli_txtAppare1').value='0';"
                                        Style="cursor: pointer" TabIndex="5" ToolTip="Esci senza inserire" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</telerik:RadWindow>

<div id="DIV_1">
    &nbsp;
    <table style="border-right: blue 2px; border-top: blue 2px; left: 32px; border-left: blue 2px; border-bottom: blue 2px; position: absolute; top: 80px; background-color: #ffffff; z-index: 102;"
        id="TABLE1">
    </table>

</div>
<asp:TextBox ID="txtAppare1" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdConnessione" runat="server" Style="left: 800px; position: absolute; top: 320px; visibility: hidden;"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:HiddenField ID="txt_FL_BLOCCATO" runat="server" />
<asp:HiddenField ID="txtData" runat="server" />
<asp:HiddenField ID="txtIdPenale" runat="server" />
<asp:HiddenField ID="txtResiduoControllo" runat="server"></asp:HiddenField>
<script type="text/javascript">


    if (document.getElementById('Tab_SAL_Dettagli_txtAppare1').value != '1') {
        document.getElementById('DIV_1').style.visibility = 'hidden';
    }

</script>
