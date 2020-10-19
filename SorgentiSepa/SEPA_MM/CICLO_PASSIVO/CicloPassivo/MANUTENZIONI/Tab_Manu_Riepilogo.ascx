<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Manu_Riepilogo.ascx.vb"
    Inherits="Tab_Manu_Riepilogo" %>
<div style="width: 100%; height: 350px">
    <table id="TABBLE_PRINCIPALE">
        <tr>
            <td>
                <table class="FontTelerik">
                    <tr>
                        <td style="vertical-align: middle; text-align: left">
                            <asp:Label ID="lblFornitore" runat="server">Fornitore</asp:Label>
                        </td>
                        <td style="vertical-align: middle; text-align: left;">
                            <asp:HyperLink ID="HLink_Fornitore" runat="server" Style="cursor: pointer" ForeColor="#1c2466"
                                Font-Bold="true">123456789 123456789 123456789  </asp:HyperLink>
                        </td>
                        <td style="width: 15px; vertical-align: top; text-align: left;">
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <asp:Label ID="lblAppalto" runat="server">Num. Repertorio</asp:Label>
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <asp:HyperLink ID="HLink_Appalto" runat="server" Font-Underline="True" ForeColor="#1c2466"
                                Font-Bold="true" Style="cursor: pointer">123456789 123456789  </asp:HyperLink>
                        </td>
                        <td style="width: 15px; vertical-align: middle; text-align: left;">
                            <asp:ImageButton ID="btnAggAppalto" runat="server" CausesValidation="False" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/40px-Crystal_Clear_action_edit_add.png"
                                OnClientClick="document.getElementById('Tab_Manu_Riepilogo_txtAppare1').value='1';document.getElementById('USCITA').value='1';document.getElementById('DIV_CORNICE').style.visibility='visible';document.getElementById('DIV_A').style.visibility='visible';"
                                TabIndex="9" ToolTip="Seleziona l'appalto" style="visibility:hidden;" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <asp:Label ID="lblDittaGestione" runat="server">Imp. Residuo</asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left">
                            <asp:TextBox ID="txtImportoTotale" runat="server" Font-Bold="True" MaxLength="30"
                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                Width="120px" ReadOnly="True" Font-Size="8pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblEuro" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label21" runat="server">(a netto dell'ordine)</asp:Label>
                        </td>
                    </tr>
                </table>
                &nbsp;&nbsp;
                <table style="font-weight: normal; text-decoration: none">
                    <tr>
                        <td style="vertical-align: top">
                            <fieldset style="border-width: 2px">
                                <legend class="TitoloH1">&nbsp;&nbsp;Importi Preventivo&nbsp;&nbsp;</legend>
                                <table style="font-weight: normal; text-decoration: none">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLordoOneri" runat="server">A lordo compresi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImporto" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro1" runat="server" Text="€" Font-Size="8pt"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblOneri" runat="server">Oneri di Sicurezza</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOneri" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro2" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server">A lordo esclusi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOneriImporto" runat="server" Font-Bold="True" MaxLength="30"
                                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                                Width="120px" ReadOnly="True" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro3" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="lblRibasso" runat="server">Ribasso d'asta</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRibassoAsta" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro4" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNettoOneri" runat="server">A netto esclusi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNetto" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro5" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" runat="server">Ritenuta di legge 0,5%</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRitenuta" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                TabIndex="-1" Width="120px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNetto" runat="server">A netto compresi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNettoOneri" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro6" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblIVA" runat="server">IVA</asp:Label>
                                            <asp:DropDownList ID="cmbIVA_P" runat="server" Font-Names="arial" Font-Size="8pt"
                                                SelectedValue='<%# DataBinder.Eval(Container, "DataItem.IVA") %>' TabIndex="7"
                                                Width="60px">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblIVA_P" runat="server">%</asp:Label>
                                        </td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtIVA" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server">A netto compresi oneri e IVA</asp:Label>
                                        </td>
                                        <td style="font-size: 8pt">
                                            <asp:TextBox ID="txtNettoOneriIVA" runat="server" Font-Bold="True" MaxLength="30"
                                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                                Width="120px" ReadOnly="True" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEuro8" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td class="style6">
                        </td>
                        <td style="vertical-align: top">
                            <fieldset style="border-width: 2px">
                                <legend class="TitoloH1">&nbsp;&nbsp;Importi Consuntivo&nbsp;&nbsp;</legend>
                                <table>
                                    <tr>
                                    </tr>
                                </table>
                                <table style="font-weight: normal; text-decoration: none">
                                    <tr>
                                        <td style="font-size: 8pt">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="False">A lordo compresi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtImportoC" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="€"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblOneriC" runat="server" Font-Bold="False">Oneri di Sicurezza</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOneriC" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server">A lordo esclusi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOneriImportoC" runat="server" Font-Bold="True" MaxLength="30"
                                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                                Width="120px" ReadOnly="True" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server">Ribasso d'asta</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRibassoAstaC" runat="server" Font-Bold="True" MaxLength="30"
                                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                                Width="120px" ReadOnly="True" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server">A netto esclusi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNettoC" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server">Ritenuta di legge 0,5%</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRitenutaC" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" ForeColor="Black"
                                                Style="text-align: right" TabIndex="-1" Text="€" Width="16px" Font-Size="8pt"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label10" runat="server">A netto compresi oneri</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNettoOneriC" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblIVAC" runat="server">IVA</asp:Label>&nbsp;<asp:DropDownList ID="cmbIVA_C"
                                                runat="server" SelectedValue='<%# DataBinder.Eval(Container, "DataItem.IVA") %>'
                                                TabIndex="8" Width="60px" Font-Size="8pt">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblIVA_C" runat="server">%</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIVAC" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                                                Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label28" runat="server">Tot. Rimborsi</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRimborsi" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                                Width="120px" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server">A netto compresi oneri e IVA</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNettoOneriIVAC" runat="server" Font-Bold="True" MaxLength="30"
                                                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                                                Width="120px" ReadOnly="True" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPenale" runat="server">Penale *</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPenale" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                                                left: 408px; top: 171px; text-align: right" TabIndex="3" Width="120px" Font-Size="8pt"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="€"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td class="style6">
                        </td>
                        <td class="style6">
                        </td>
                        <td class="style6">
                        </td>
                        <td class="style6">
                        </td>
                        <td class="style6">
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="txtImportoControllo" runat="server" />
                <asp:HiddenField ID="txtImportoControlloC" runat="server" />
                <asp:HiddenField ID="txtOneriP_MANO" runat="server" />
                <asp:HiddenField ID="txtOneriC_MANO" runat="server" />
                <asp:TextBox ID="txtIdConnessione" runat="server" Style="left: 800px; position: absolute;
                    visibility: hidden; top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                <asp:Label ID="lblLotto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="55px"
                    Visible="False">Lotto</asp:Label>&nbsp;
                <asp:TextBox ID="txtLotto" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="50"
                    TabIndex="-1" Width="180px" ReadOnly="True" Visible="False"></asp:TextBox>
                <asp:TextBox ID="txtIdComponente" runat="server" Style="left: 800px; position: absolute;
                    visibility: hidden; top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
                <asp:TextBox ID="txtAppare1" runat="server" Style="left: 800px; position: absolute;
                    visibility: hidden; top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
                <asp:ImageButton ID="btnINFO" runat="server" ImageUrl="~/NuoveImm/INFO.png" Style="z-index: 100;
                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Visualizza la scheda dell'appalto"
                    Visible="False" />
            </td>
        </tr>
    </table>
</div>
<div id="DIV_A" style="display: block; left: 0px; background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg);
    width: 800px; position: absolute; top: 0px; height: 550px; background-color: whitesmoke">
    &nbsp;
    <table id="TABLE1" style="border-right: blue 2px; border-top: blue 2px; z-index: 102;
        left: 32px; border-left: blue 2px; border-bottom: blue 2px; position: absolute;
        top: 96px; background-color: #ffffff">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Lista Appalti</span></strong>
            </td>
        </tr>
        <tr>
            <td style="height: 150px">
                <table style="width: 456px">
                    <tr>
                        <td>
                            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                                overflow: auto; border-left: #0000cc thin solid; width: 685px; border-bottom: #0000cc thin solid;
                                height: 250px" id="DIV_CORNICE">
                                <asp:DataGrid ID="DataGridAppalti" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                                    TabIndex="18" Width="1008px">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID_APPALTO" HeaderText="ID" Visible="False">
                                            <HeaderStyle Width="0%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="APPALTO" HeaderText="APPALTO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NUM_REPERTORIO" HeaderText="NUM. REPERTORIO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_FORNITORE" HeaderText="ID_FORNITORE" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FORNITORE" HeaderText="FORNITORE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD. FISCALE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PARTITA_IVA" HeaderText="PARTITA IVA"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
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
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#0000C0" Wrap="False" />
                                </asp:DataGrid></div>
                            <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White"
                                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="680px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:TextBox ID="txtID1" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 49px">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px; vertical-align: middle; text-align: right;">
                            <asp:ImageButton ID="btn_Inserisci1" runat="server" ImageUrl="~/NuoveImm/Img_Seleziona.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Manu_Riepilogo_txtAppare1').value='0';"
                                Style="cursor: pointer" TabIndex="16" ToolTip="Seleziona l'appalto" />
                            <asp:ImageButton ID="btn_Chiudi1" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_A').style.visibility='hidden'; document.getElementById('DIV_CORNICE').style.visibility='hidden';   document.getElementById('Tab_Manu_Riepilogo_txtAppare1').value='0';"
                                Style="cursor: pointer" TabIndex="17" ToolTip="Esci senza selezione" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="448px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 8px; position: absolute; top: 56px" Width="776px" />
</div>
<script type="text/javascript">


    if (document.getElementById('Tab_Manu_Riepilogo_txtAppare1').value != '1') {
        document.getElementById('DIV_A').style.visibility = 'hidden';
        document.getElementById('DIV_CORNICE').style.visibility = 'hidden';
    }

</script>
