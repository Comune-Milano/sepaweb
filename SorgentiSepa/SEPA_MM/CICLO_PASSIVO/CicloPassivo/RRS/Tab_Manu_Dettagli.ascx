<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Manu_Dettagli.ascx.vb"
    Inherits="Tab_Manu_Dettagli" %>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
    Localization-Cancel="Annulla">
</telerik:RadWindowManager>
<table style="width: 100%" class="FontTelerik">
    <tr>
        <td>
            <asp:Label ID="lblDescrizione" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="80px">Descrizione *</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDescrizione" runat="server" Height="30px" MaxLength="300" Style="left: 80px; top: 88px"
                TabIndex="8" TextMode="MultiLine" Width="670px" ReadOnly="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDanneggiante" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="80px">Danneggiante</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDanneggiante" runat="server" MaxLength="500" Style="z-index: 10; left: 408px; top: 171px"
                TabIndex="9" Width="670px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDanneggiato" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="80px">Danneggiato</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDanneggiato" runat="server" MaxLength="500" Style="z-index: 10; left: 408px; top: 171px"
                TabIndex="10" Width="670px"></asp:TextBox>
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr>
        <td class="TitoloH1" style="text-align: left">&nbsp;<asp:Label ID="lblELENCO_INTERVENTI" runat="server" TabIndex="-1" Text="Elenco interventi"
            Width="248px"></asp:Label>&nbsp;
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>
            <div style="overflow: auto; width: 100%; height: 100%;">
                <telerik:RadGrid ID="DataGrid1" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                    AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                    IsExporting="False" PagerStyle-AlwaysVisible="true">
                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                        CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                            ShowRefreshButton="true" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DETTAGLIO" HeaderText="DETTAGLIO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="TIPO">
                                <HeaderStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownListTipologiaOggetto" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="DropDownListTipologiaOggetto_SelectedIndexChanged" Font-Size="8pt"
                                        Font-Names="Arial" Width="98%">
                                        <asp:ListItem Text="- - -" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="COMPLESSO" Value="COMPLESSO"></asp:ListItem>
                                        <asp:ListItem Text="EDIFICIO" Value="EDIFICIO"></asp:ListItem>
                                        <asp:ListItem Text="UNITA IMMOBILIARE" Value="UNITA IMMOBILIARE"></asp:ListItem>
                                        <asp:ListItem Text="UNITA COMUNE" Value="UNITA COMUNE"></asp:ListItem>
                                        <asp:ListItem Text="IMPIANTO - CENTRALE TERMICA" Value="CENTRALE TERMICA"></asp:ListItem>
                                        <asp:ListItem Text="IMPIANTO - SOLLEVAMENTO" Value="SOLLEVAMENTO"></asp:ListItem>
                                        <asp:ListItem Text="IMPIANTO - TELERISCALDAMENTO" Value="TELERISCALDAMENTO"></asp:ListItem>
                                        <asp:ListItem Text="IMPIANTO - CENTRALE IDRICA" Value="CENTRALE IDRICA"></asp:ListItem>
                                        <asp:ListItem Text="SCALA" Value="SCALA"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="DETTAGLIO OGGETTO">
                                <HeaderStyle Width="45%" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropDownListTipologiaOggettoDettaglio" runat="server" 
                                        Font-Size="8pt" Width="98%">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_PRESUNTO" HeaderText="IMPORTO PRESUNTO"
                                Visible="false">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="IMPORTO PRESUNTO">
                                <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TextBoxImportoPresunto" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_PRESUNTO") %>'
                                        Style="font-family: Arial; font-size: 8pt; text-align: right" Width="98%"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_CONSUNTIVO" HeaderText="IMPORTO CONSUNTIVO"
                                Visible="false">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="IMPORTO CONSUNTIVO">
                                <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TextBoxImportoConsuntivo" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_CONSUNTIVO") %>'
                                        Style="font-family: Arial; font-size: 8pt; text-align: right" Width="98%"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="IMPORTO_RIMBORSO" HeaderText="IMPONIBILE RIMBORSO" Visible="false">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="IMPONIBILE RIMBORSO">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="TextBoxImportoRimborso" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_RIMBORSO") %>' Style="font-family: Arial; font-size: 8pt; text-align: right" Width="98%"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>


                            <telerik:GridBoundColumn DataField="PERC_IVA_RIMBORSO" HeaderText="IVA RIMBORSO" Visible="false">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </telerik:GridBoundColumn>
                            <%--<asp:TemplateColumn HeaderText="IVA RIMBORSO">
							<ItemTemplate>
								<asp:TextBox runat="server" ID="TextBoxIvaRimborso" Text='<%# DataBinder.Eval(Container, "DataItem.PERC_IVA_RIMBORSO") %>'
									Style="font-family: Arial; font-size: 8pt; text-align: right" Width="100px"></asp:TextBox>
							</ItemTemplate>
						</asp:TemplateColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="IVA RIMBORSO">
                                <ItemTemplate>
                                    <asp:DropDownList ID="TextBoxIvaRimborso" runat="server" AutoPostBack="false"
                                        Font-Size="8pt"
                                         Width="50px">
                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="FL_BLOCCATO" HeaderText="FL_BLOCCATO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_COMPLESSO" HeaderText="ID_COMPLESSO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_UNITA_IMMOBILIARE" HeaderText="ID_UNITA_IMMOBILIARE"
                                Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_UNITA_COMUNE" HeaderText="ID_UNITA_COMUNE"
                                Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_IMPIANTO" HeaderText="ID_IMPIANTO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_SCALA" HeaderText="ID_SCALA" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="DeleteColumn">
                                <HeaderStyle Width="3%" />
                                <ItemTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: center">
                                                <telerik:RadImageButton ID="btnElimina2" runat="server" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ConfermaAnnulloIntervento();}"
                                                    ToolTip="Elimina l'intervento selezionato" Width="16" Height="16" Image-Url="../../Immagini/Delete.gif"
                                                    OnClick="btnElimina2_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <a id="addServizio" style="cursor: pointer" onclick="document.getElementById('HiddenAggiungi').value='1';document.getElementById('Tab_Manu_Dettagli_ImageButtonAggiorna').click();">
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
                <telerik:RadButton ID="ImageButtonAggiorna" runat="server" Text="Aggiungi" OnClientClicking="function(sender, args){document.getElementById('HiddenAggiungi').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_1').style.visibility='hidden';}"
                    ToolTip="Aggiunge un nuovo intervento" Width="100px" Style="visibility: hidden" />
                <telerik:RadButton ID="btnElimina1" runat="server" Text="Elimina" OnClientClicking="function(sender, args){document.getElementById('USCITA').value='1';ConfermaAnnulloIntervento();}"
                    ToolTip="Elimina l'intervento selezionato" Width="100px" Style="visibility: hidden" />
            </div>
            <div>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle">
                            <img src="../../../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Gli importi presunti e consuntivati sono da considerarsi al lordo degli oneri e al netto dell'iva</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
        <td>&nbsp; &nbsp;
        </td>
        <td>
            <table>
                <tr>
                    <td style="height: 16px;"></td>
                </tr>
                <%--<tr>
                    <td style="height: 16px;">
                        <asp:ImageButton ID="btnAgg1" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg" OnClientClick="document.getElementById('Tab_Manu_Dettagli_txtAppare1').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_1').style.visibility='visible';" TabIndex="9"
                            ToolTip="Aggiunge una nuovo intervento" />
                    </td>
                </tr>--%>
                <tr>
                    <td></td>
                </tr>
                <%--<tr>
                    <td style="height: 14px">
                        <asp:ImageButton ID="btnApri1" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Modifica.png" OnClientClick="controlla_div();document.getElementById('USCITA').value='1';" TabIndex="11" ToolTip="Modifica l'intervento selezionato" Width="60px" />
                    </td>
                </tr>--%>
            </table>
            <table>
                <tr>
                    <td style="height: 16px"></td>
                </tr>
                <%--<tr>
                    <td style="height: 14px">
                        <asp:ImageButton ID="btnConsuntivo" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Img_Consuntivo.png" TabIndex="12" ToolTip="Gestione Consuntivi" Width="60px" />
                    </td>
                </tr>--%>
                <tr>
                    <td style="height: 14px">
                        <%--<asp:ImageButton ID="btnVisualConsuntivo" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Img_VisualizzaConsuntivo.png" TabIndex="12" ToolTip="Visualizza il dettaglio del Consuntivo" Width="60px" />--%>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>
<div align='center' id="ATTESA" style="position: absolute; background-color: #ffffff; text-align: center; width: 200px; height: 100px; top: 200px; left: 300px; z-index: 10; border: 1px dashed #660000; font: verdana; font-size: 10px; color: blue;">
    <br />
    <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
    caricamento in corso...
</div>
<div id="DIV_1" style="width: 800px; position: absolute; height: 550px; background-color: whitesmoke; left: 0px; top: 0px; display: none; background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg);">
    &nbsp;
    <table style="border-right: blue 2px; border-top: blue 2px; left: 32px; border-left: blue 2px; border-bottom: blue 2px; position: absolute; top: 96px; background-color: #ffffff; z-index: 102;"
        id="TABLE1">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Gestione Interventi<br />
                </span></strong>
            </td>
        </tr>
        <tr>
            <td style="height: 150px">
                <table id="Table5">
                    <tr>
                        <td>
                            <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" 
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Tipologia Oggetto </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList onchange="AttesaDIV()" ID="cmbTipologia" runat="server" AutoPostBack="True"
                                BackColor="White"  Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid; top: 56px"
                                TabIndex="13" Width="600px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDettaglio" runat="server" Font-Bold="False" 
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Dettaglio Tipologia </asp:Label>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
                <asp:DropDownList ID="cmbDettaglio" runat="server" BackColor="White" 
                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid; top: 56px"
                    TabIndex="14" Width="720px">
                </asp:DropDownList>
                <br />
                <table>
                    <tr>
                        <td></td>
                        <td>&nbsp;
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblImporto" runat="server" Font-Bold="False"  Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="110px">Importo Presunto *</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtImporto" runat="server" MaxLength="10" Style="z-index: 10; left: 408px; top: 171px"
                                TabIndex="15" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <table style="width: 456px">
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <asp:TextBox ID="txtID1" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_Inserisci1" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Manu_Dettagli_txtAppare1').value='0';"
                                Style="cursor: pointer" TabIndex="16" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_Chiudi1" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_1').style.visibility='hidden';document.getElementById('Tab_Manu_Dettagli_txtAppare1').value='0';"
                                Style="cursor: pointer" TabIndex="17" ToolTip="Esci senza inserire" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="328px" ImageUrl="../../../ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 8px; position: absolute; top: 80px" Width="776px" />
</div>
<asp:TextBox ID="txtAppare1" runat="server" Style="left: 800px; position: absolute; visibility: hidden; top: 320px"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente" runat="server" Style="left: 800px; position: absolute; visibility: hidden; top: 320px"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo" runat="server" Style="left: 800px; position: absolute; visibility: hidden; top: 320px"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdConnessione" runat="server" Style="left: 800px; position: absolute; visibility: hidden; top: 320px"
    TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:HiddenField ID="txt_FL_BLOCCATO" runat="server" />
<asp:HiddenField ID="txtResiduoConsumo" runat="server" />
<asp:HiddenField ID="txtImportoODL" runat="server" />
<asp:HiddenField ID="txtIdManuPadre" runat="server" />
<asp:HiddenField ID="HiddenAggiungi" runat="server" ClientIDMode="Static" Value="0" />
<script language="javascript" type="text/javascript">

    function controlla_div() {
        if (document.getElementById('Tab_Manu_Dettagli_txtIdComponente').value != "") {
            document.getElementById('Tab_Manu_Dettagli_txtAppare1').value = '1';
            document.getElementById('DIV_1').style.visibility = 'visible';
        }
        else {
            alert('Nessuna riga selezionata!')
        }
    }

    if (document.getElementById('Tab_Manu_Dettagli_txtAppare1').value != '1') {
        document.getElementById('DIV_1').style.visibility = 'hidden';
    }

    document.getElementById('ATTESA').style.visibility = 'hidden';

</script>
