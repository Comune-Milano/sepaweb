<%@ Page Language="VB" AutoEventWireup="false" CodeFile="assegnaStrutturaSUA.aspx.vb"
    Inherits="AMMSEPA_OperatoreSUA_assegnaStrutturaSUA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca per Struttura</title>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;

        function $onkeydown() {

            if (event.keyCode == 13) {
                alert('Usare il tasto <Avvia Ricerca>');
                history.go(0);
                event.keyCode = 0;
            }
        } 

    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Ricerca per Struttura" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="../Immagini/SfondoHome.jpg" height="75px" width="101%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 40%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 40%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="False">Struttura</asp:Label>
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="cmbfiliale" TabIndex="5" runat="server" Width="500px" Style="border-right: black 1px solid;
                                                border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                Font-Names="Arial" Font-Size="10pt">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 15%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../../NuoveImm/Img_AvviaRicerca.png"
                                                TabIndex="8" ToolTip="Avvia Ricerca" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                                TabIndex="9" ToolTip="Home" />
                                        </td>
                                        <td>
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
