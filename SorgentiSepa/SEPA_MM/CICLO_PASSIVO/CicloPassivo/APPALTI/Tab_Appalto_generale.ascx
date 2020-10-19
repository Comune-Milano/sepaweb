<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Appalto_generale.ascx.vb"
    Inherits="Tab_Appalto_generale" %>
<style type="text/css">
    .style4
    {
        width: 4px;
    }
    .style5
    {
        width: 102px;
    }
</style>
<table>
    <tr>
        <td>
            <asp:Label ID="lblCanone" runat="server" CssClass="TitoloH1" Text="Canone"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black">Base asta</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtastacanone" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="8" Width="80px" ReadOnly="True" ToolTip="Sommatoria importi a canone per i servizi caricati"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td class="style5">
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="Label76" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="86px">TOT Contrattuale</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpContCanone" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="12" Width="80px" ReadOnly="True" ToolTip="Base d'Asta al netto dello sconto"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label53" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="41px" Height="16px" Style="text-align: right">Oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtonericanone" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="9" Width="80px" AutoPostBack="True" ReadOnly="True" ToolTip="Somma degli Oneri di Sicurezza a Canone per i servizi caricati"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td style="font-family: Arial; font-size: 8pt">
            <asp:Label ID="Label87" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="25px" Height="16px" Style="text-align: right">Var.</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtVarCan" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="14"
                Style="text-align: right" TabIndex="14" Width="75px" ReadOnly="True" ToolTip="Somma degli importi a consumo per i servizi caricati"></asp:TextBox>
        </td>
        <td style="font-family: Arial; font-size: 8pt">
            €
        </td>
        <td class="style5">
            <asp:Label ID="Label82" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px; text-align: right;">TOT</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpTotPlusOneriCan" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="12" Width="80px" ReadOnly="True" ToolTip="T.O.T Contrattuale + Oneri sicurezza + % iva"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label85" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td style="width: 80px">
            <asp:Label ID="lblResiduoCan" runat="server" Font-Names="Arial" Font-Size="8pt" Width="30px"
                Style="text-align: right">Residuo</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtResiduoCanone" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" TabIndex="13" Width="80px" ReadOnly="True" Style="text-align: right"></asp:TextBox>
        </td>
        <td class="style4">
            <asp:Label ID="Label56" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="10">
            &nbsp;
        </td>
        <td class="style5">
            <asp:Label ID="Label48" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="65px" Height="16px">Reversibile</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpCanoneRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label54" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label51" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="45px" Style="text-align: right">Reversib.</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtResiduoRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label57" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="10">
            &nbsp;
        </td>
        <td class="style5">
            <asp:Label ID="Label49" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="90px">NON Reversibile</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpCanoneNotRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label55" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label52" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="45px">Non Rev.</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtResiduoNotRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label58" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table>
    <tr>
        <td>
            <asp:Label ID="LabelConsumo" runat="server" CssClass="TitoloH1" Text="Consumo"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label81" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black">Base asta</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtastaconsumo" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="text-align: right" TabIndex="14" Width="80px" ReadOnly="True"
                ToolTip="Somma degli importi a consumo per i servizi caricati"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label79" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="Label78" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="86px">TOT Contrattuale</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpContConsumo" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="17" Width="80px" ReadOnly="True" ToolTip="Base d'Asta al netto dello sconto"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label65" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label60" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="41px" Height="16px" Style="text-align: right">Oneri</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtonericonsumo" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="15" Width="80px" AutoPostBack="True" ReadOnly="True" ToolTip="Somma degli Oneri di Sicurezza a Consumo per i servizi caricati"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label61" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td style="font-family: Arial; font-size: 8pt">
            <asp:Label ID="Label86" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="25px" Style="text-align: right">Var.</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtVarCons" runat="server" Font-Names="ARIAL" Font-Size="8pt" MaxLength="14"
                Style="text-align: right" TabIndex="14" Width="75px" ReadOnly="True" ToolTip="Somma degli importi a consumo per i servizi caricati"></asp:TextBox>
        </td>
        <td style="font-family: Arial; font-size: 8pt">
            €
        </td>
        <td style="width: 80px">
            <asp:Label ID="Label83" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black">TOT</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpTotPlusOneriCons" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="12" Width="80px" ReadOnly="True" ToolTip="T.O.T Contrattuale + Oneri sicurezza + % iva"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label84" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td style="width: 80px">
            <asp:Label ID="lblResiduoCons" runat="server" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="30px" Style="text-align: right">Residuo</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtresiduoConsumo" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="18" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td class="style4">
            <asp:Label ID="Label69" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="10">
            &nbsp;
        </td>
        <td>
            <asp:Label ID="Label66" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="71px" Height="16px">Reversibile</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpConsRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label73" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label70" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="45px" Style="text-align: right">Reversib.</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtResiduoConsRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label71" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="10">
            &nbsp;
        </td>
        <td>
            <asp:Label ID="Label72" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="104px">NON Reversibile</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtImpConsNotRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label80" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="Label74" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Width="45px" Style="text-align: right">Non Rev.</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtResiduoConsNotRevers" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="20" Style="text-align: right" TabIndex="13" Width="80px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label75" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table>
    <tr>
        <td>
            <asp:Label ID="Label43" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Fondo Penali</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtfondopenali" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="19" Width="100px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label44" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblFond" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black">Fondo Ritenute di legge</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtfondoritenute" runat="server" Font-Names="ARIAL" Font-Size="8pt"
                MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right"
                TabIndex="20" Width="100px" ReadOnly="True"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lbleurFond" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="text-align: left" Text="€"></asp:Label>
        </td>
        <td>
       
            <asp:ImageButton ID="btnInfoRitLegge" runat="server" ImageUrl="../../../Images/Telerik/Information-icon.png" height="16" width="16"
                OnClientClick="RiepRitLegge();return false;" />
            <div style="display:none; visibility:hidden;">
                <asp:Button Text="" runat="server" ID="btnInfoRitLegge2" Style="display: none" />
       </div>
            
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
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px;
                                    top: 67px; text-align: right" TabIndex="20" Width="70px" ReadOnly="True"></asp:TextBox>
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
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text="Residuo anticipo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfondoTrattenuto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="8pt" MaxLength="14" Style="z-index: 107; left: 109px;
                                    top: 67px; text-align: right" TabIndex="20" Width="70px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: left" Text=" €"></asp:Label>
                            </td>
    </tr>
</table>
<table style="width: 752px" cellpadding="0" cellspacing="0">
    <tbody>
        <asp:HiddenField ID="canone" runat="server" />
        <asp:HiddenField ID="durata" runat="server" />
        <asp:HiddenField ID="consumo" runat="server" />
        <asp:HiddenField ID="durataMesi" runat="server" />
    </tbody>
</table>
