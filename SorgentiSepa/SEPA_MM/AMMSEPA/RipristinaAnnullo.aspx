<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RipristinaAnnullo.aspx.vb"
    Inherits="AMMSEPA_RipristinaAnnullo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ripristino Bolletta</title>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Ripristino Bolletta" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="101%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 60%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 40%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Password" Font-Names="arial" Font-Size="10pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpw" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Conferma" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Numero Bolletta (compresi 0 iniziali)"
                                                Font-Names="arial" Font-Size="10pt" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpw0" runat="server" Visible="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="RIPRISTINA" Visible="False" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="Label4" runat="server" Text="Numero Bolletta" Font-Names="arial" Font-Size="10pt"
                                                Font-Bold="True" Visible="False" Width="800px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 60%">
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Red" Height="16px" Visible="False" Width="525px"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <span style="font-size: 24pt; color: #722615; font-family: Arial">
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/img_HomeModelli.png"
                                                    TabIndex="7" ToolTip="Home" /></span>
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
