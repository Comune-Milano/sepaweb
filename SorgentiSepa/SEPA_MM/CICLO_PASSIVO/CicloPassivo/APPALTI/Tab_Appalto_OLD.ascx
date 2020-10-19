<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Appalto_OLD.ascx.vb" Inherits="Tab_Appalto_OLD" %>

<table>
    <tr>
        <td style="width: 643px; height: 378px">
            <table style="width: 640px">
                <tr>
                    <td style="height: 64px">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="120px">Numero Repertorio</asp:Label></td>
                    <td style="height: 64px">
                        <asp:TextBox ID="txtnumero" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" Style="z-index: 107; left: 109px;
                            top: 67px" TabIndex="1" Width="150px"></asp:TextBox>&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtnumero"
                            ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                            top: 92px" ValidationExpression="^\d+$" Width="1px"></asp:RegularExpressionValidator></td>
                    <td style="width: 14px; height: 64px;">
                        &nbsp;&nbsp; &nbsp;</td>
                    <td style="height: 64px">
                        <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="80px">Descrizione</asp:Label></td>
                    <td style="height: 64px">
                    <asp:TextBox ID="txtdescrizione" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" 
                        Style="z-index: 107; left: 109px; top: 67px;" 
                        TabIndex="2" Width="220px" Height="60px" TextMode="MultiLine"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="120px">Anno inizio</asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 53px">
                                    <asp:TextBox ID="txtannoinizio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="4" Style="z-index: 107; left: 109px;
                                        top: 67px" TabIndex="3" Width="80px"></asp:TextBox></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtannoinizio"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                                        top: 92px" ValidationExpression="^\d{4}$"
                                        Width="1px"></asp:RegularExpressionValidator>&nbsp;</td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">Durata mesi</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtdurata" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="2" Style="z-index: 107; left: 109px;
                                        top: 67px" TabIndex="4" Width="80px"></asp:TextBox></td>
                                <td style="width: 11px">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtdurata"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                                        top: 92px" ValidationExpression="^\d+$" Width="1px"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 11px">
                                    &nbsp;&nbsp;
                                </td>
                                <td>
                                    </td>
                                <td>
                                    </td>
                                <td style="width: 19px">
                                </td>
                                <td>
                                </td>
                                <td style="width: 53px">
                                    &nbsp;</td>
                                <td style="width: 53px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                                    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="85px">Base asta</asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtasta" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                        Font-Size="10pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right;"
                                        TabIndex="5" Width="80px"></asp:TextBox></td>
                                <td>
                                    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="61" Text="€" Width="16px"></asp:Label></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtasta"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                                        top: 92px" ValidationExpression="^\d+$" Width="1px"></asp:RegularExpressionValidator>&nbsp;</td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">Costo grado giorno</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtcosto" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                        Font-Size="10pt" MaxLength="14" Style="z-index: 107; left: 109px; top: 67px; text-align: right;"
                                        TabIndex="6" Width="80px"></asp:TextBox>
                                    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="61" Text="€" Width="16px"></asp:Label></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtcosto"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                                        top: 92px" ValidationExpression="^\d+$" Width="1px"></asp:RegularExpressionValidator>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="120px">Percentuale oneri sicurezza</asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td style="height: 40px">
                                    <asp:TextBox ID="txtoneri" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                        Font-Size="10pt" MaxLength="3" Style="z-index: 107; left: 109px; top: 67px; text-align: right;"
                                        TabIndex="7" Width="80px">0</asp:TextBox></td>
                                <td style="height: 40px;">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="61" Text="%" Width="16px"></asp:Label></td>
                                <td style="height: 40px">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtoneri"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                                        top: 92px" ValidationExpression="^\d+$" Width="1px"></asp:RegularExpressionValidator>&nbsp;
                                </td>
                                <td style="height: 40px;">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">Percentuale sconto</asp:Label></td>
                                <td style="height: 40px">
                                    <asp:TextBox ID="txtsconto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="3" Style="z-index: 107; left: 109px;
                                        top: 67px; text-align: right;" TabIndex="8" Width="80px">0</asp:TextBox>
                                </td>
                                <td style="height: 40px">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="61" Text="%" Width="16px"></asp:Label>
                                </td>
                                <td style="width: 3px; height: 40px">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtsconto"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 135; left: 229px;
                                        top: 92px" ValidationExpression="^\d+$" Width="1px"></asp:RegularExpressionValidator>
                                </td>
                                <td style="height: 40px">
                                    </td>
                                <td style="height: 40px">
                                    </td>
                                <td style="height: 40px">
                                    &nbsp;&nbsp;
                                </td>
                                <td style="width: 59px; height: 40px;">
                                    </td>
                                <td style="height: 40px">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="85px">Anno rif. inizio</asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtannorifinizio" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                        Font-Size="10pt" MaxLength="4" Style="z-index: 107; left: 109px; top: 67px"
                                        TabIndex="9" Width="80px"></asp:TextBox></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtannorifinizio"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 133; left: 231px;
                                        top: 263px" ValidationExpression="^\d{4}$"
                                        Width="1px"></asp:RegularExpressionValidator></td>
                                <td>
                                    &nbsp; &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="99px">Anno rif. fine</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtannorifine" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="ARIAL"
                                        Font-Size="10pt" MaxLength="4" Style="z-index: 107; left: 109px; top: 67px"
                                        TabIndex="10" Width="80px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtannorifine"
                                        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 133; left: 471px;
                                        top: 264px" ValidationExpression="^\d{4}$"
                                        Width="1px"></asp:RegularExpressionValidator></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="120px">Lotto</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmblotto" runat="server" AutoPostBack="True" BackColor="White"
                            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 10; left: 142px; border-left: black 1px solid;
                            border-bottom: black 1px solid; top: 224px" TabIndex="11" Width="491px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 106; left: 19px; top: -374px" Width="120px">Tipo Servizio</asp:Label></td>
                    <td>
                        <asp:DropDownList ID="cmbservizio" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                            z-index: 10; left: 142px; border-left: black 1px solid; border-bottom: black 1px solid;
                            top: 224px" TabIndex="12" Width="491px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="height: 21px">
                    </td>
                    <td style="height: 21px">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            </td>
            </tr>
</table>
