<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_DatiAmminist.ascx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_DatiAmminist" %>
<style type="text/css">
    .style1
    {
        height: 18px;
    }
</style>
<table>
    <tr>
        <td>
            <table class="FontTelerik">
                <tr>
                    <asp:Label ID="Label9" runat="server" CssClass="TitoloH1" Text="Direttore lavori"></asp:Label>   
                </tr>
                <tr>
                    <td style="font-weight: bold; font-size: 9pt; color: #0000ff; font-family: Arial">
                        <table cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Cognome</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Nome</asp:Label>
                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                        <asp:TextBox ID="txtCognDirect" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>
                        <asp:TextBox ID="txtNomDirect" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                <td class="style1">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Telefono</asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black" Width="100px">e-Mail</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTelDirect" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailDirect" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:top">
              <table style="border: thin solid #ccccff;" cellpadding = "0" >
                  <tr>
                       <td style="font-weight: bold; font-size: 9pt; color: #0000ff; font-family: Arial; vertical-align:top">
                &nbsp;Proponente</td>
    </tr>
    <tr>
        <td>
                                    <asp:Label ID="Label90" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Cognome</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Nome</asp:Label>
                                </td>

                            </tr>
                <tr>
                    <td >
                                    <asp:TextBox ID="txtCognomeProponente" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px; top: 72px"
                                        TabIndex="1" Width="150"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomeProponente" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px; top: 72px"
                                        TabIndex="1" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                  </table>

                    </td>
                </tr>
                <tr>
        <td>
            <table style="border: thin solid #ccccff;" cellpadding = "0" >
                <tr>
                    <td style="font-weight: bold; font-size: 9pt; color: #0000ff; font-family: Arial">
                &nbsp;RUP (Responsabile Unico del Procedimento )</td>

                </tr>
                <tr>
                                <td style="font-weight: bold; font-size: 9pt; color: #0000ff; font-family: Arial">
                        <table cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Cognome</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Nome</asp:Label>
                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                        <asp:TextBox ID="txtCognomeRup" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                            </td>
                                <td>
                        <asp:TextBox ID="txtNomeRup" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">Telefono</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 5px; top: 25px" ForeColor="Black">e-Mail</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtTelefonoRup" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailRup" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="8pt" MaxLength="30" Style="z-index: 105; left: 10px;
                            top: 72px" TabIndex="1" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
