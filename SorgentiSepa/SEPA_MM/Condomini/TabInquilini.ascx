<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabInquilini.ascx.vb"
    Inherits="Condomini_TabInquilini" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
<table style="width: 90%;">
    <tr>
        <td style="vertical-align: top; text-align: left">
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                width: 703px; top: 0px; height: 250px; text-align: left">
                <asp:DataGrid ID="DataGridInquilini" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="Black" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="Vertical" PageSize="1" Style="z-index: 105; left: 8px; top: 32px"
                    Width="1350px" BorderWidth="1px" CellSpacing="1">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD_UI" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="STATOVISUAL" HeaderText="STATO" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_EDIFICIO" HeaderText="ID_EDIFICIO" Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="COD U.I.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPOLOGIA">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="STATO OCC.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OCCUPAZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="INTESTATARIO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="COF.FISC/P.IVA">
                            <ItemTemplate>
                                <asp:Label ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CF_IVA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NOMINATIVO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOMINATIVO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="RAPPORTO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="POS. BILANCIO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_BILANCIO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="N. COMP.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_COMP_NUCLEO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="N. OSPITI">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_OSPITI") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MIL. PROP.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_PRO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MIL. ASC.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_ASC") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MIL. COMPRO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_COMPRO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MIL. GESTIONE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_GEST") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MIL. RISCALD.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_RISC") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL.PRES.ASSEMB">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MILL_PRES_ASS") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="NOTE">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid></div>
        </td>
        <td style="vertical-align: top; text-align: left">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" ToolTip="Modifica" CausesValidation="False"
                            OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="imgPrint" alt="Stampa" onclick="PrintInquilini();" src="Immagini/print-icon.png"
                            style="left: 115px; cursor: pointer; top: 26px; right: 600px;" title="Stampa" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" src="Immagini/Search_16x16.png"
                            style="left: 115px; cursor: pointer; top: 26px; right: 600px;" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="cod_ui" runat="server" />
            <asp:HiddenField ID="STATO" runat="server" />
            <asp:HiddenField ID="TIPOLOGIA" runat="server" />
            <asp:HiddenField ID="idContratto" runat="server" />
            <asp:HiddenField ID="Id_Edificio" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left">
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="572px">Nessuna Selezione</asp:TextBox>
        </td>
        <td style="vertical-align: top; text-align: left">
            &nbsp;
        </td>
    </tr>
</table>
<div id="divInquilini" style="border: thin none #3366ff; z-index: 201; left: -3px;
    vertical-align: top; width: 802px; position: absolute; top: 4px; visibility: hidden;
    height: 582px; background-color: #dedede; text-align: left; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    margin-right: 10px;">
    <br />
    <table style="z-index: 200; left: 26px; width: 769px; position: absolute; top: 67px;
        height: 257px">
        <tr>
            <td style="vertical-align: top; width: 765px; text-align: left">
                <table style="border: thin solid lightblue; width: 701px; z-index: 120;">
                    <tr>
                        <td style="vertical-align: top; text-align: left;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="110px">UNITA IMMOBILIARE :</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodUI" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                            Font-Size="10pt" MaxLength="50" Style="z-index: 600; left: 10px; top: 72px; text-align: left"
                                            TabIndex="-1" Width="181px" Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="77px">TIPOLOGIA</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtTipologia" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="False" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                            Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="203px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="110px">INDIRIZZO U.I.</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIndirizzoUI" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="False" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                            Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="208px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="25px">Civ.</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCivicoUI" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="False" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 600;
                                            left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="78px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="27px">Cap</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCapUI" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True" Style="z-index: 600;
                                            left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="85px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLocalitaUI" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Bold="False" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                            Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="-1" Width="85px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 16px; text-align: left">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="110px">Interno</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInterno" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 600; left: 10px;
                                            top: 72px; text-align: left" TabIndex="-1" Width="65px" Font-Bold="False" EnableTheming="True"
                                            Font-Strikeout="False" Font-Underline="False" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="27px">Scala</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtScala" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                            Font-Size="10pt" MaxLength="10" Style="z-index: 600; left: 10px; top: 72px; text-align: left"
                                            TabIndex="-1" Width="63px" Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="33px">Piano</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPiano" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                            Font-Size="10pt" MaxLength="10" Style="z-index: 600; left: 10px; top: 72px; text-align: left"
                                            TabIndex="-1" Width="78px" Font-Italic="False" Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Style="z-index: 104; left: 10px; top: 104px" Width="52px">Stato Occ.</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStOccupazione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 600; left: 10px;
                                            top: 72px; text-align: left" TabIndex="-1" Width="95px" Font-Bold="False" EnableTheming="True"
                                            Font-Strikeout="False" Font-Underline="False" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="width: 94%">
                    <tr>
                        <td style="width: 12%; height: 24px">
                        </td>
                        <td style="width: 55px; height: 24px">
                        </td>
                        <td style="width: 11%; height: 24px">
                        </td>
                        <td style="width: 6%; height: 24px">
                        </td>
                        <td style="width: 10%; height: 24px">
                        </td>
                        <td style="width: 5%; height: 24px">
                        </td>
                        <td style="width: 331%; height: 24px">
                        </td>
                    </tr>
                </table>
                <table style="width: 669px; z-index: 120;">
                    <tr>
                        <td style="width: 140px; height: 16px">
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="83px">INTESTATARIO</asp:Label>
                        </td>
                        <td style="width: 252px; height: 16px" colspan="3">
                            <asp:DropDownList ID="cmbIntestatari" runat="server" BackColor="White" Font-Names="Arial"
                                Font-Size="9pt" Style="right: 481px; left: 9px; top: 109px" TabIndex="11" Width="294px"
                                Font-Bold="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 131px; height: 16px" colspan="3">
                            &nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Style="z-index: 104; left: 10px; top: 104px" Width="106px">STATO RAPPORTO</asp:Label>
                        </td>
                        <td colspan="3" style="width: 131px; height: 16px">
                            <asp:TextBox ID="txtStatoRapp" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                Style="z-index: 605; left: 10px; top: 72px; text-align: left" TabIndex="13" Width="156px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 94%">
                    <tr>
                        <td style="width: 1%; height: 24px; vertical-align: top; text-align: left;">
                            &nbsp;<asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" Style="z-index: 104; left: 10px; top: 104px" Width="27px">Recapito</asp:Label>
                        </td>
                        <td style="width: 11%; height: 24px; vertical-align: top; text-align: left;">
                            <asp:DropDownList ID="cmbTipoCor" runat="server" Style="" Font-Names="Arial" Font-Size="9pt"
                                TabIndex="14" Width="90px" Font-Bold="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 11%; height: 24px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtVia" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 600; left: 10px;
                                top: 72px; text-align: left" TabIndex="15" Width="198px"></asp:TextBox>
                        </td>
                        <td style="width: 7px">
                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="25px">Civ.</asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left; width: 3px;">
                            <asp:TextBox ID="txtCivico" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 600;
                                left: 10px; top: 72px; text-align: left" TabIndex="15" Width="78px"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; text-align: left; width: 7px;">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="27px">Cap</asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: left">
                            <asp:TextBox ID="txtCap" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 600; left: 10px;
                                top: 72px; text-align: left" TabIndex="15" Width="78px"></asp:TextBox>&nbsp;
                        </td>
                        <td style="width: 331%; height: 24px; vertical-align: top; text-align: left;">
                            &nbsp;<asp:TextBox ID="txtLocalità" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="15" Width="85px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 1%; height: 24px; text-align: left">
                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="52px">Presso</asp:Label>
                        </td>
                        <td style="vertical-align: top; width: 11%; height: 24px; text-align: left" colspan="5">
                            <asp:TextBox ID="txtPresso" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True"
                                Style="z-index: 600; left: 10px; top: 72px; text-align: left" TabIndex="15" Width="294px"></asp:TextBox>
                        </td>
                        <td style="vertical-align: top; width: 11%; height: 24px; text-align: left">
                        </td>
                        <td style="width: 7px">
                        </td>
                        <td style="vertical-align: top; width: 3px; text-align: left">
                        </td>
                        <td style="vertical-align: top; width: 7px; text-align: left">
                        </td>
                        <td style="vertical-align: top; text-align: left">
                        </td>
                        <td style="vertical-align: top; width: 331%; height: 24px; text-align: left">
                        </td>
                    </tr>
                </table>
                <table style="width: 98%; z-index: 120;">
                    <tr>
                        <td style="width: 116px; height: 21px;">
                        </td>
                        <td style="width: 207px; height: 21px;">
                        </td>
                        <td style="width: 167px; height: 21px;">
                        </td>
                        <td style="width: 918px; height: 21px;">
                        </td>
                        <td style="width: 918px; height: 21px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 21px;">
                            <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="149px">Pos. su Bilancio Condominiale</asp:Label>
                        </td>
                        <td style="width: 207px; height: 21px;">
                            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px">Millesimi Proprietà</asp:Label>
                        </td>
                        <td style="width: 167px; height: 21px;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="104px">Millesimi Ascensore</asp:Label>
                        </td>
                        <td style="width: 918px; height: 21px;">
                            <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="104px">Millesimi Presenza</asp:Label>
                        </td>
                        <td style="width: 918px; height: 21px;">
                            <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="104px">Num.Persone</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 20px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtPosBil" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="10" Style="z-index: 600; left: 10px;
                                top: 72px; text-align: right" TabIndex="19" Width="100px"></asp:TextBox>
                        </td>
                        <td style="width: 207px; height: 20px">
                            <asp:TextBox ID="txtMil_Pro" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="20" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMil_Pro"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"
                                Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 167px; height: 20px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtAsc" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px; top: 72px; text-align: right"
                                TabIndex="21" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAsc"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 20px; text-align: left">
                            <asp:TextBox ID="txtMillPres" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="21" Width="75px" Height="20px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtMillPres"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 20px; text-align: left">
                            <asp:TextBox ID="txtnumPers" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="21" Width="75px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 20px">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="124px">Millesimi Comproprietà</asp:Label>
                        </td>
                        <td style="width: 207px; height: 20px">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="86px">Millesimi Gestione</asp:Label>
                        </td>
                        <td style="width: 167px; height: 20px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="116px">Millesimi Riscaldamento</asp:Label>
                        </td>
                        <td style="width: 918px; height: 20px">
                            <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="116px">Addebito Singolo</asp:Label>
                        </td>
                        <td style="width: 918px; height: 20px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 22px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtMil_Compro" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="22" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMil_Compro"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 207px; height: 22px">
                            <asp:TextBox ID="txtMil_Gest" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="23" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMil_Gest"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 167px; height: 22px; vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txt_Mil_Risc" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="24" Width="75px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_Mil_Risc"
                                ErrorMessage="N,0000" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|\.)\d{1,4})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 22px; text-align: left">
                            <asp:TextBox ID="txtAddebito" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="10pt" MaxLength="30" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: right" TabIndex="25" Width="75px"></asp:TextBox><asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAddebito"
                                    ErrorMessage="N,00" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Style="left: 144px;
                                    top: 80px" ToolTip="Inserire un valore con decimale a precisione doppia" ValidationExpression="^\d{1,7}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 22px; text-align: left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 22px">
                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="30px">Note</asp:Label>
                        </td>
                        <td style="width: 207px; height: 22px">
                        </td>
                        <td style="width: 167px; height: 22px">
                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Style="z-index: 104; left: 10px; top: 104px" Width="101px">Altri Condomini</asp:Label>
                        </td>
                        <td style="width: 918px; height: 22px">
                        </td>
                        <td style="width: 918px; height: 22px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 117px; height: 21px" colspan="2">
                            <asp:TextBox ID="txtNote" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                Font-Size="10pt" Height="81px" MaxLength="100" Style="z-index: 605; left: 10px;
                                top: 72px; text-align: left" TabIndex="26" TextMode="MultiLine" Width="271px"></asp:TextBox>
                        </td>
                        <td style="width: 837px; height: 21px; vertical-align: top; text-align: left;" colspan="3">
                            <asp:ListBox ID="ListCondomini" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Height="81px" Style="overflow: auto; border-collapse: separate" Width="96%" TabIndex="-1">
                            </asp:ListBox>
                        </td>
                        <td style="width: 837px; height: 21px; vertical-align: top; text-align: left;">
                            &nbsp;
                        </td>
                        <td style="vertical-align: top; width: 918px; height: 21px; text-align: left">
                        </td>
                        <td style="width: 380px; height: 21px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 116px; height: 22px">
                        </td>
                        <td style="width: 207px; height: 22px">
                        </td>
                        <td style="width: 167px; height: 22px">
                            <asp:ImageButton ID="btnSalvaInquilini" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="25" ToolTip="Salva i dati" OnClientClick="document.getElementById('splash').style.visibility = 'visible';" />
                        </td>
                        <td style="width: 918px; height: 22px">
                            <img id="imgAnnulla" alt="Chiudi la finestra" onclick="document.getElementById('TextBox4').value!='1';myOpacity4.toggle();PulisciCampi();"
                                src="../NuoveImm/Img_AnnullaVal.png" style="left: 185px; cursor: pointer; top: 23px" />
                        </td>
                        <td style="width: 918px; height: 22px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;
    <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 100; left: 6px; position: absolute; top: 46px; height: 531px;
        width: 771px;" />
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:Label ID="lblTitolo" runat="server" Style="position: absolute; top: 22px; left: 7px;"
            Text="Condominio: NameCond"></asp:Label>
    </span></strong>
</div>
<script type="text/javascript">
    myOpacity4 = new fx.Opacity('divInquilini', { duration: 200 });
    //myOpacity.hide();
    if (document.getElementById('TextBox4').value != '2') {
        myOpacity4.hide(); ;
    }
                    

</script>
