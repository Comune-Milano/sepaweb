<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_FornitoreG_legale.ascx.vb" Inherits="Tab_FornitoreG_legale" %>

<table>
    <tr>
        <td style="height: 378px">
        <table id="TABLE1">
            <tr>
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
                <td style="width: 121px">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
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
                <td style="width: 121px">
                </td>
            </tr>
        <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black" Style="z-index: 106; left: -254px; top: -281px" Width="100px">Tipo indirizzo*</asp:Label></td>
            <td>
                    <asp:DropDownList ID="DrLTipoInd" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        Style="border: 1px solid black; z-index: 111; left: 30px; top: 19px; " 
                        TabIndex="5" Height="20px" Width="92px" >
                </asp:DropDownList></td>
            <td>
                &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            </td>
            <td>
                    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" 
                        Style="z-index: 106; left: 19px; top: -374px" ForeColor="Black">Indirizzo*</asp:Label></td>
            <td>
                    <asp:TextBox ID="txtIndirizzoResidenza" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" 
                        Style="z-index: 107; left: 109px; top: 67px;" 
                        TabIndex="6" Width="200px"></asp:TextBox></td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            </td>
            <td>
                    <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" 
                        Style="z-index: 106; left: 288px; top: 53px" ForeColor="Black">Civico*</asp:Label></td>
            <td style="width: 121px">
                    <asp:TextBox ID="txtCivicoResidenza" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="ARIAL" Font-Size="10pt" MaxLength="6" 
                        Style="z-index: 107; left: 287px; top: 67px; width: 47px; right: 305px;" 
                        TabIndex="7"></asp:TextBox></td>
        </tr>
            <tr>
                <td>
                    <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" 
                        Style="z-index: 106; left: -306px; top: -342px" ForeColor="Black">CAP*</asp:Label></td>
                <td>
                    <asp:TextBox ID="txtCAP" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="5" 
                        Style="z-index: 107; left: 347px; top: 68px; width: 47px; bottom: 225px; right: 245px;" 
                        TabIndex="8"></asp:TextBox></td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" 
                        Style="z-index: 106; left: 417px; top: 55px; right: 183px; height: 12px;" ForeColor="Black">Comune*</asp:Label></td>
                <td>
                    <asp:TextBox ID="txtComuneResidenza" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="ARIAL" Font-Size="10pt" MaxLength="25" 
                        Style="z-index: 107; left: 416px; top: 69px;" 
                        TabIndex="9" Width="200px"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" 
                        Style="z-index: 106; left: 37px; top: -286px" ForeColor="Black">Pr.*</asp:Label></td>
                <td style="width: 121px">
	
                    <asp:TextBox ID="txtProvinciaResidenza" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="ARIAL" Font-Size="10pt" MaxLength="2" 
                        Style="z-index: 107; left: 594px; top: 69px; width: 23px;" 
                        TabIndex="10"></asp:TextBox></td>
            </tr>
        </table>
            <table>
                <tr>
                    <td style="height: 23px">
                    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" ForeColor="Black" 
                        Style="z-index: 106; left: -254px; top: -281px" Width="100px">Telefono</asp:Label></td>
                    <td style="height: 23px">
                    <asp:TextBox ID="txtTel" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                        MaxLength="50" 
                        Style="z-index: 107; left: 11px; top: 146px;" 
                        TabIndex="11" Width="162px"></asp:TextBox></td>
                    <td style="height: 23px">
                        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
                    </td>
                    <td style="height: 23px">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 37px; top: -286px">Fax</asp:Label></td>
                    <td style="height: 23px">
                        <asp:TextBox ID="txtfax" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="12" Width="162px"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 23px">
                    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" ForeColor="Black" 
                        Style="z-index: 106; left: -254px; top: -281px" Width="100px">E-Mail</asp:Label></td>
                    <td colspan = "5">
                        <asp:TextBox ID="txtemail" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                            Style="z-index: 107; left: 11px; top: 146px" TabIndex="11" Width="250px"></asp:TextBox>
                                </td>
                    <td style="height: 23px">
                        &nbsp;</td>
                    <td style="height: 23px">
                        &nbsp;</td>
                    <td style="height: 23px">
                        &nbsp;</td>
                </tr>
            </table>
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
            <br />
            <br />
            <br />
        </td>
    </tr>
</table>
