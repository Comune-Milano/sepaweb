<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaLottiScambio.aspx.vb"
    Inherits="LOTTI_RicercaLottiScambio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script language="javascript" type="text/javascript">


   
</script>
<head id="Head1" runat="server">
    <title>RICERCA</title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        &nbsp;
        <table style="left: 0px; width: 800px; position: absolute; top: 0px">
            <tr>
                <td >
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp;
                        <br />
                        &nbsp; Ricerca Lotti</span></strong><br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 320px">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp;&nbsp;
                        <table>
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td>
                                    <asp:Label ID="lblEsercizio" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" Width="120px">Esercizio Finanziario *</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbEsercizio" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 168px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 32px" TabIndex="1" Width="600px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px; height: 21px">
                                </td>
                                <td style="height: 21px">
                                    <asp:Label ID="lblFiliale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 32px" Width="120px">Struttura *</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <asp:DropDownList ID="cmbFiliale" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 168px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 32px" TabIndex="2" Width="600px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td>
                                    <asp:Label ID="lblServizio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 128px" Width="120px">Tipo Servizio *</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoServizio" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 172px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 128px" TabIndex="3" Width="600px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 128px" Width="120px">Dettaglio Servizio *</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbDettaglioServizio" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 172px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 128px" TabIndex="3" Width="600px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td>
                                    <asp:HiddenField ID="txtID_LOTTI" runat="server" Visible="False" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
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
                    &nbsp;<br />
                    &nbsp;<br />
                    <br />
                    &nbsp;
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 106; left: 665px; position: absolute; top: 424px" ToolTip="Home"
                        TabIndex="4" />
                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        Style="z-index: 111; left: 509px; position: absolute; top: 423px" ToolTip="Avvia Ricerca"
                        OnClick="btnCerca_Click" />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
