<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_ImportiNP.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_ImportiNP" %>
<style type="text/css">
    .style2 {
        width: 100%;
    }
</style>
<table style="width: 752px" cellpadding="0" cellspacing="0">
    <tbody>
        <tr>
            <td style="font-weight: bold; font-size: 9pt; color: #0000ff; font-family: Arial; border-right-style: solid; border-right-width: 1pt; border-right-color: #C0C0C0; border-bottom-style: solid; border-bottom-width: 1pt; border-bottom-color: #C0C0C0; border-top-style: solid; border-top-width: 1pt; border-top-color: #C0C0C0;">Consumo
                <asp:Label ID="Label81" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black">Base asta</asp:Label>
            </td>
            <td style="border-bottom-style: solid; border-bottom-width: 1pt; border-bottom-color: #C0C0C0; border-top-style: solid; border-top-width: 1pt; border-top-color: #C0C0C0;">
                <table>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtastaconsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="text-align: right" TabIndex="14"
                                Width="100px" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label93" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
                        </td>
                        <td width="100%">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top; border-right-style: solid; border-right-width: 1pt; border-right-color: #C0C0C0; border-bottom-style: solid; border-bottom-width: 1pt; border-bottom-color: #C0C0C0;">
                <table style="width: 100%; height: 83px">
                    <tr>
                        <td style="text-align: right">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label94" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="109px">TOT Contrattuale</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: bottom">
                            <table style="width: 100%; height: 50px;">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label100" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="80px">Residuo</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; text-align: left; border-bottom-style: solid; border-bottom-width: 1pt; border-bottom-color: #C0C0C0;">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtImpContConsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="17" Width="100px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label95" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label96" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="80px" Height="16px">Oneri sicurezza</asp:Label>
                                    </td>
                                    <td style="width: 1px;">
                                        <asp:TextBox ID="txtonericonsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="15" Width="100px" AutoPostBack="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label97" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label82" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="34px">T.O.T</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImpTotPlusOneriCon" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="12" Width="100px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtresiduoConsumo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="20" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="18" Width="100px" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label101" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Fondo Penali</asp:Label>
            </td>
            <td>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtfondopenali" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                    TabIndex="19" Width="80px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label103" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblFond" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black">Rit. legge</asp:Label>
                            </td>
                            <td style="width: 1px;">
                                <asp:TextBox ID="txtfondoritenute" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                    TabIndex="20" Width="80px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbleurFond" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnPrintPagParz" runat="server" ImageUrl="../../../Condomini/Immagini/print-icon.png"
                                    ToolTip="Visualizza il Pagamento della Ritenuta di Legge Stampato" />
                            </td>
                            <td>
                                <asp:Label ID="Label1111" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text="Totale anticipo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfondoAnticipo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                    TabIndex="20" Width="70px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label5551" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text=" €"></asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnStampaCDP" runat="server" ImageUrl="../../../Condomini/Immagini/print-icon.png"
                                    Visible="false" ToolTip="Stampa dell'anticipo contrattuale" />
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text="Residuo anticipo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfondoTrattenuto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                    TabIndex="20" Width="70px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text=" €"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; vertical-align: top">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="60px" Visible="False">Base asta</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: bottom">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label76" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="78px" Visible="False">Imp contrattuale</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top; text-align: left;" class="style2">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table width="97%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtastacanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="8" Width="80px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="80px" Height="16px" Visible="False">Oneri sicurezza</asp:Label>
                                    </td>
                                    <td style="width: 1px;">
                                        <asp:TextBox ID="txtonericanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="9" Width="80px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€" Visible="False"></asp:Label>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="34px"
                                            Visible="False">pari a</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtpercanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="15" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="10" Width="70px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label39" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="%" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                                            ControlToValidate="txtpercanone" ErrorMessage="!" Font-Bold="True" Height="16px"
                                            Style="z-index: 135; left: 229px; top: 92px" ValidationExpression="^(100(?:\.0{1,2})?|0*?\.\d{1,2}|\d{1,2}(?:\.\d{1,2})?)$"
                                            Width="16px" Display="Dynamic" Font-Names="Arial" Font-Size="8pt" Enabled="False"
                                            ToolTip="oneri superiore alla base asta" Visible="False"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label42" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Visible="False">Frequenza</asp:Label>
                                    </td>
                                    <td class="style3">
                                        <asp:DropDownList ID="cmbFreqPagamento" runat="server" Font-Names="arial" Font-Size="8pt"
                                            SelectedValue='<%# DataBinder.Eval(Container, "DataItem.FREQ_PAGAMENTO") %>'
                                            TabIndex="11" Width="100px" Visible="False">
                                            <asp:ListItem Value="0">Non Definito</asp:ListItem>
                                            <asp:ListItem Value="1">Mensile</asp:ListItem>
                                            <asp:ListItem Value="2">Bimestrale</asp:ListItem>
                                            <asp:ListItem Value="3">Trimestrale</asp:ListItem>
                                            <asp:ListItem Value="4">Quadrimestrale</asp:ListItem>
                                            <asp:ListItem Value="6">Semestrale</asp:ListItem>
                                            <asp:ListItem Value="12">Annuale</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td class="style2">
                                        <asp:TextBox ID="txtImpContCanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                                            TabIndex="12" Width="80px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label53" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Width="80px" Visible="False">Residuo</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResiduoCanone" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="20" Style="text-align: right" TabIndex="13"
                                            Width="80px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                    <td class="style1">
                                        <asp:Label ID="Label56" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: left" Text="€" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:HiddenField ID="canone" runat="server" />
                <asp:HiddenField ID="durata" runat="server" />
                <asp:HiddenField ID="consumo" runat="server" />
                <asp:HiddenField ID="durataMesi" runat="server" />
            </td>
        </tr>
    </tbody>
</table>
