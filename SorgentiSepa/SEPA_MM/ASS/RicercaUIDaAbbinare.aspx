<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaUIDaAbbinare.aspx.vb"
    Inherits="ASS_RicercaUIDaAbbinare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Ricerca UI da Abbinare</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
            width: 798px; position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Unità libere da abbinare</strong> </span>
                    <br />
                    <br />
                    <table width="750px" cellpadding="3" cellspacing="2">
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 200px;">
                                <asp:Label ID="Label25" runat="server" Font-Names="arial" Font-Size="10pt" Text="Quartiere"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbQuartiere" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    TabIndex="1" Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 200px;">
                                <asp:Label ID="Label26" runat="server" Font-Names="arial" Font-Size="10pt" Text="Indirizzo"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbIndirizzo" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    TabIndex="2" Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label15" runat="server" Font-Names="arial" Font-Size="10pt" Text="Tipologia"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbTipo" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    TabIndex="3" Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="10pt" Text="Pertinenze"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbPertinenze" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    TabIndex="4" Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 200px;">
                                <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="10pt" Text="Piano"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="cmbPiano" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    TabIndex="5" Width="350px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="10pt" Text="Zona Decentramento"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbDecentramento" runat="server" Font-Names="ARIAL" Font-Size="10pt"
                                    TabIndex="6" Width="60px">
                                    <asp:ListItem Value="- - -" Selected="True">- - -</asp:ListItem>
                                    <asp:ListItem Value="01">01</asp:ListItem>
                                    <asp:ListItem Value="02">02</asp:ListItem>
                                    <asp:ListItem Value="03">03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>99</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Text="Proprietà Gestore"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbProprieta" runat="server" Width="60px">
                                    <asp:ListItem Value="-1" Selected="True">TUTTI</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="10pt" Text="Con Ascensore"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chAscensore" runat="server" TabIndex="7" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="10pt" Text="Idoneo Handicap"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chHandicap" runat="server" TabIndex="8" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="10pt" Text="Con Posto auto"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chPostoAuto" runat="server" TabIndex="9" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
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
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnEsci" runat="server" Style="z-index: 102; left: 685px; position: absolute;
            top: 469px" Text="Uscita" TabIndex="11" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
            OnClientClick="self.close();" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 525px; position: absolute; top: 469px; height: 20px;"
            ToolTip="Avvia Ricerca" TabIndex="10" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="provenienza" runat="server" Value="0" />
    </div>
    </form>
</body>
</html>
