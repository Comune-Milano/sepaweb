<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SAL_Riepilogo.ascx.vb"
    Inherits="Tab_SAL_Riepilogo" %>
<table style="width: 760px;" class="FontTelerik">
 <tr>
        <td class="TitoloH1" style="text-align: left" colspan="2">
            <asp:Label ID="Label2" runat="server" Font-Size="8pt" Style="z-index: 100; left: 8px;
                top: 88px" TabIndex="-1" Width="150px">IMPORTO PRENOTATO</asp:Label>
        </td>
        
        <td>
        </td>
        <td style="width: 35px;">
       
        <td class="TitoloH1" style="text-align: left" colspan="2">
            <asp:Label ID="lblImportoApprovazione" runat="server" Font-Size="8pt" Style="z-index: 100;
                left: 8px; top: 88px" TabIndex="-1" Width="100%">IMPORTO IN APPROVAZIONE</asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblLordoOneri" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A lordo compresi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImporto" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                Font-Size="7pt"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblEuro1" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A lordo compresi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImporto2" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label4" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td class="TitoloH1" style="text-align: left">
            <asp:HyperLink ID="HLink_ElencoMandati" runat="server" Font-Bold="True" Font-Underline="True"
                 Style="cursor: pointer" ToolTip="Visualizza tutti i mandati di pagamento"
                Width="125px">Mandati di Pagamento</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblOneri" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">Oneri di Sicurezza</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOneri" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                Font-Size="7pt"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblEuro2" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px;">
        </td>
        <td>
            <asp:Label ID="lblOneriC" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">Oneri di Sicurezza</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOneri2" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label6" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A lordo esclusi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOneriImporto" runat="server" Font-Bold="True" MaxLength="30"
                Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                Width="120px" ReadOnly="True" Font-Size="7pt"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblEuro3" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="Label17" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A lordo esclusi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtOneriImporto2" runat="server" Font-Bold="True" Font-Size="7pt"
                MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label7" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="Label28" runat="server" ForeColor="Black" Style="z-index: 100; left: 8px;
                top: 88px" TabIndex="-1" Width="125px">STATO LIQUIDAZIONE</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1" >
            <asp:Label ID="lblRibasso" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">Ribasso d'asta</asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtRibassoAsta" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                Font-Size="7pt"></asp:TextBox>
        </td>
        <td >
            <asp:Label ID="lblEuro4" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td class="style3" style="width: 35px;">
        </td>
        <td class="style3" >
            <asp:Label ID="Label18" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">Ribasso d'asta</asp:Label>
        </td>
        <td class="style3" >
            <asp:TextBox ID="txtRibassoAsta2" runat="server" Font-Bold="True" Font-Size="7pt"
                MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td class="style3" >
            <asp:Label ID="Label9" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td class="style3" style="width: 35px">
        </td>
        <td class="style3" >
            <asp:DropDownList ID="cmb_Liquidazione" runat="server" BackColor="White" Enabled="False"
                Font-Bold="True" Font-Size="10pt" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 56px" TabIndex="5" Width="125px">
                <asp:ListItem Selected="True" Value="0">DA LIQUIDARE</asp:ListItem>
                <asp:ListItem Value="1">PARZIALE</asp:ListItem>
                <asp:ListItem Value="3">COMPLETO</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNettoOneri" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A netto esclusi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNetto" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                Font-Size="7pt"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblEuro5" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px;">
        </td>
        <td>
            <asp:Label ID="Label19" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A netto esclusi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNetto2" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label10" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td >
            <asp:Label ID="Label25" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">Ritenuta di legge 0,5%</asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtRitenuta" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td >
            <asp:Label ID="Label26" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px;">
        </td>
        <td >
            <asp:Label ID="Label20" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">Ritenuta di legge 0,5%</asp:Label>
        </td>
        <td >
            <asp:TextBox ID="txtRitenuta2" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td >
            <asp:Label ID="Label11" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td >
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblNetto" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A netto compresi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNettoOneri" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblEuro6" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="Label21" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A netto compresi oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNettoOneri2" runat="server" Font-Bold="True" Font-Size="7pt"
                MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label12" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="lblPenale" runat="server" Font-Bold="False" 
                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Penale *</asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblIVA" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">IVA</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtIVA" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label13" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="Label22" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">IVA</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtIVA2" runat="server" Font-Bold="True" Font-Size="7pt" MaxLength="30"
                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label14" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:TextBox ID="txtPenale" runat="server" Font-Bold="False" Font-Size="7pt" MaxLength="30"
                Style="z-index: 10; left: 408px; top: 171px" TabIndex="3" Width="100px" ReadOnly="True"></asp:TextBox>
            <asp:Label ID="Label24" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label5" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A netto compresi oneri e IVA</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNettoOneriIVA" runat="server" Font-Bold="True" Font-Size="7pt"
                MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblEuro8" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:Label ID="Label23" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                TabIndex="-1" Width="150px">A netto compresi oneri e IVA</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNettoOneriIVA2" runat="server" Font-Bold="True" Font-Size="7pt"
                MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                TabIndex="-1" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label15" runat="server" ForeColor="Black" Style="text-align: right"
                TabIndex="-1" Text="€" Width="16px"></asp:Label>
        </td>
        <td style="width: 35px">
        </td>
        <td>
            <asp:HiddenField ID="txtImponibile" runat="server"></asp:HiddenField>
        </td>
    </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="150px">Importo imponibile trattenuto</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxImportoTrattenuto" runat="server" Font-Bold="True" Font-Size="7pt"
                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                    TabIndex="-1" Width="120px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label27" runat="server" Font-Bold="False" ForeColor="Black"
                    Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
            </td>
            <td style="width: 35px">
            </td>
            <td>
                <asp:Label ID="Label29" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="150px">Importo imponibile trattenuto</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxImportoTrattenuto2" runat="server" Font-Bold="True" Font-Size="7pt"
                    MaxLength="30" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                    TabIndex="-1" Width="120px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label30" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
            </td>
            <td style="width: 35px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label33" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="150px">Importo IVA trattenuto</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxImportoIvaTrattenuto" runat="server" ReadOnly="true" Text="0"
                    Font-Bold="True" Font-Size="7pt" MaxLength="30" Style="z-index: 10; left: 408px;
                    top: 171px; text-align: right" TabIndex="-1" Width="120px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label35" runat="server" Font-Bold="False" ForeColor="Black"
                    Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
            </td>
            <td class="auto-style2">
            </td>
            <td>
                <asp:Label ID="Label32" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="150px">Importo IVA trattenuto</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxImportoIvaTrattenuto2" runat="server" ReadOnly="true" Text="0"
                    Font-Bold="True" Font-Size="7pt" MaxLength="30" Style="z-index: 10; left: 408px;
                    top: 171px; text-align: right" TabIndex="-1" Width="120px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label38" runat="server" Font-Bold="False" ForeColor="Black"
                    Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
            </td>
            <td class="auto-style2">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label34" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="150px">Importo totale trattenuto</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxImportoTotaleTrattenuto" runat="server" ReadOnly="true" Text="0"
                    Font-Bold="True" Font-Size="7pt" MaxLength="30" Style="z-index: 10; left: 408px;
                    top: 171px; text-align: right" TabIndex="-1" Width="120px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label36" runat="server" Font-Bold="False" ForeColor="Black"
                    Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
            </td>
            <td class="auto-style2">
            </td>
            <td>
                <asp:Label ID="Label31" runat="server" Font-Bold="False" 
                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="150px">Importo totale trattenuto</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBoxImportoTotaleTrattenuto2" runat="server" ReadOnly="true"
                    Text="0" Font-Bold="True" Font-Size="7pt" MaxLength="30" Style="z-index: 10;
                    left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label37" runat="server" Font-Bold="False" ForeColor="Black"
                    Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
            </td>
            <td class="auto-style2">
            </td>
            <td>
            </td>
        </tr>
</table>

<style type="text/css">
    .style1
    {
        width: 153px;
    }
    .style3
    {
        width: 80px;
    }
</style>
