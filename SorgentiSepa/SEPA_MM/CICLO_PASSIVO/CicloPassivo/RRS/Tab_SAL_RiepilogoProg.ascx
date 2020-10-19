<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SAL_RiepilogoProg.ascx.vb"
    Inherits="Tab_SAL_RiepilogoProg" %>
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
<table id="TABBLE_PRINCIPALE" class="FontTelerik">
    <tr>
        <td>
            &nbsp;&nbsp;
            <br />
            <table style="font-weight: normal; text-decoration: none">
               
                <tr>
                    <td>
                        <asp:Label ID="lblLordoOneri" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">A lordo compresi oneri</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtImporto" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                            left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro1" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                            Width="16px"></asp:Label>
                    </td>
                    <td style="width: 90px">
                    </td>
                    <td>
                        <asp:Label ID="LabelRitenuta" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">Ritenuta di legge 0,5%</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRitenuta" runat="server" Font-Bold="True"  MaxLength="30"
                            ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                            TabIndex="-1" Width="120px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="LabelRitenutaEuro" runat="server" Style="text-align: right" TabIndex="-1"
                            Text="€" Width="16px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOneri" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">Oneri di Sicurezza</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOneri" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                            left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                            ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro2" runat="server" Font-Bold="False"  Style="text-align: right"
                            TabIndex="-1" Text="€" Width="16px"></asp:Label>
                    </td>
                    <td style="width: 90px;">
                    </td>
                    <td>
                        <asp:Label ID="lblNetto" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">A netto compresi oneri</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNettoOneri" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                            left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                            ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro6" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                            Width="16px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">A lordo esclusi oneri</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOneriImporto" runat="server" Font-Bold="True" MaxLength="30"
                            Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                            Width="120px" ReadOnly="True" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro3" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                            Width="16px"></asp:Label>
                    </td>
                    <td style="width: 90px">
                    </td>
                    <td>
                        <asp:Label ID="lblIVA" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">IVA</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIVA" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                            left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                            ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                            Width="16px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="lblRibasso" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">Ribasso d'asta</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRibassoAsta" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                            left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                            ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro4" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                            Width="16px"></asp:Label>
                    </td>
                    <td class="style3" style="width: 90px;">
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label28" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">Tot. Rimborsi</asp:Label>
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtRimborsi" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="True"
                            Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                            Width="120px" ></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label18" runat="server" ForeColor="Black" Style="text-align: right"
                            TabIndex="-1" Text="€" Width="16px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNettoOneri" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">A netto esclusi oneri</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNetto" runat="server" Font-Bold="True" MaxLength="30" Style="z-index: 10;
                            left: 408px; top: 171px; text-align: right" TabIndex="-1" Width="120px" ReadOnly="True"
                            ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro5" runat="server" Font-Bold="False"  ForeColor="Black"
                            Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label>
                    </td>
                    <td style="width: 90px;">
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Style="z-index: 100; left: 8px; top: 88px"
                            TabIndex="-1" Width="160px">A netto compresi oneri e IVA</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNettoOneriIVA" runat="server" Font-Bold="True" MaxLength="30"
                            Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                            Width="120px" ReadOnly="True" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblEuro8" runat="server" Style="text-align: right" TabIndex="-1" Text="€"
                            Width="16px"></asp:Label>
                    </td>
                </tr>
<tr>
                    <td style="height: 30px">
                    </td>
                    <td style="height: 30px">
                    </td>
                    <td style="height: 30px">
                    </td>
                    <td style="width: 90px; height: 30px;">
                    </td>
                    <td style="height: 30px">
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" ForeColor="Black"
                            Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="160px" Font-Size="9pt">Importo imponibile trattenuto</asp:Label>
                    </td>
                    <td style="height: 30px">
                        <asp:TextBox ID="txtImportoTrattenuto" runat="server" Font-Bold="True" MaxLength="30"
                            Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                            Width="120px" Font-Size="9pt"></asp:TextBox>
                    </td>
                    <td style="height: 30px">
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" ForeColor="Black"
                            Style="text-align: right" TabIndex="-1" Text="€" Width="16px" Font-Size="9pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td style="height: 30px">
                    </td>
                    <td style="height: 30px">
                    </td>
                    <td style="width: 90px; height: 30px;">
                    </td>
                    <td style="height: 30px">
                        <asp:Label ID="Label29" runat="server" Font-Bold="False"  ForeColor="Black"
                            Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="160px" >Importo IVA trattenuto</asp:Label>
                    </td>
                    <td style="height: 30px">
                        <asp:TextBox ID="txtImportoIvaTrattenuto" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="true" Text="0"
                            Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                            Width="120px" ></asp:TextBox>
                    </td>
                    <td style="height: 30px">
                        <asp:Label ID="Label32" runat="server" Font-Bold="False"  ForeColor="Black"
                            Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                        &nbsp;</td>
                    <td style="height: 30px">
                        &nbsp;</td>
                    <td style="height: 30px">
                        &nbsp;</td>
                    <td style="width: 90px; height: 30px;">
                    	&nbsp;</td>
                    <td style="height: 30px">
                        <asp:Label ID="Label31" runat="server" Font-Bold="False"  ForeColor="Black"
                            Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="160px" >Importo totale trattenuto</asp:Label>
                    </td>
                    <td style="height: 30px">
                        <asp:TextBox ID="txtImportoTotaleTrattenuto" runat="server" Font-Bold="True" MaxLength="30" ReadOnly="true" Text="0"
                            Style="z-index: 10; left: 408px; top: 171px; text-align: right" TabIndex="-1"
                            Width="120px" ></asp:TextBox>
                    </td>
                    <td style="height: 30px">
                        <asp:Label ID="Label34" runat="server" Font-Bold="False"  ForeColor="Black"
                            Style="text-align: right" TabIndex="-1" Text="€" Width="16px" ></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtIdConnessione" runat="server" Style="left: 800px; position: absolute;
                visibility: hidden; top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="txtIdComponente" runat="server" Style="left: 800px; position: absolute;
                visibility: hidden; top: 320px; visibility: hidden" TabIndex="-1" Width="0px"
                Height="0px"></asp:TextBox>
            <asp:TextBox ID="txtAppare1" runat="server" Style="left: 800px; position: absolute;
                visibility: hidden; top: 320px; visibility: hidden" TabIndex="-1" Width="0px"
                Height="0px"></asp:TextBox>
        </td>
    </tr>
</table>
