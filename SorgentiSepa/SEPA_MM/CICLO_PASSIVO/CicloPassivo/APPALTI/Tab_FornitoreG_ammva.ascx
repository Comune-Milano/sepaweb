<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_FornitoreG_ammva.ascx.vb" Inherits="Tab_FornitoreG_ammva" %>

<table>
    <tr>
        <td style="width: 640px;">
            <table id="TABLE1">
                <tr>
                    <td style="height: 21px">
                    </td>
                    <td style="height: 21px">
                    </td>
                    <td style="height: 21px">
                    </td>
                    <td style="height: 21px">
                    </td>
                    <td style="height: 21px">
                    </td>
                    <td style="height: 21px">
                    </td>
                    <td style="width: 14px; height: 21px;">
                    </td>
                    <td style="width: 121px; height: 21px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">Tipo indirizzo</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="DrLTipoIndA" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 111; left: 30px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 19px" TabIndex="13" Width="92px">
                        </asp:DropDownList></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px">Indirizzo</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtIndirizzoResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 107; left: 109px;
                            top: 67px" TabIndex="14" Width="200px"></asp:TextBox></td>
                    <td>
                        &nbsp;&nbsp;
                    </td>
                    <td style="width: 14px;">
                        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 288px; top: 53px">Civico</asp:Label></td>
                    <td style="width: 121px">
                        <asp:TextBox ID="txtCivicoResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="6" Style="z-index: 107; right: 305px;
                            left: 287px; width: 47px; top: 67px" TabIndex="15"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -306px; top: -342px">CAP</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCAPA" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                            Font-Size="10pt" MaxLength="5" Style="z-index: 107; right: 245px; left: 347px;
                            width: 47px; bottom: 225px; top: 68px" TabIndex="16"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; right: 183px; left: 417px; top: 55px;
                            height: 12px">Comune</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtComuneResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="25" Style="z-index: 107; left: 416px; top: 69px" TabIndex="17" Width="200px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td style="width: 14px;">
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 37px; top: -286px">Pr.</asp:Label></td>
                    <td style="width: 121px">
                        <asp:TextBox ID="txtProvinciaResidenzaA" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="2" Style="z-index: 107; left: 594px;
                            width: 23px; top: 69px" TabIndex="18"></asp:TextBox></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">Telefono</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtTelA" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="19" Width="162px"></asp:TextBox></td>
                    <td>
                        &nbsp; &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 37px; top: -286px">Fax</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtfaxA" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="20" Width="162px"></asp:TextBox></td>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" ForeColor="Black" 
                        Style="z-index: 106; left: -254px; top: -281px" Width="100px">E-Mail</asp:Label></td>
                    <td colspan = "5">
                        <asp:TextBox ID="txtemail" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="11" Width="250px"></asp:TextBox>
                                </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <br />
                        <asp:Button ID="btncopia" runat="server" Text="Copia dati da Sede Legale" /><br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>
