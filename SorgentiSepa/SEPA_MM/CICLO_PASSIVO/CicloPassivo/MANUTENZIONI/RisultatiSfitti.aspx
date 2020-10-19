<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiSfitti.aspx.vb" Inherits="MANUTENZIONI_RisultatiSfitti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<head id="Head1" runat="server">
    <title>RISULTATI RICERCA SFITTI</title>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
            <table >
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Risultati Ricerca n.<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;<br />
                    </span></strong><br />
                    <table>
                        <tr>
                            <td style="width: 20px;">
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 48px; top: 32px" Width="130px">Esercizio Finanziario</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cmbEsercizio" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                    border-bottom: black 1px solid; top: 32px" TabIndex="2" Width="600px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 20px; height: 7px">
                                &nbsp; &nbsp;&nbsp;
                            </td>
                            <td style="height: 7px">
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 48px; top: 32px" Width="130px">UBICAZIONE</asp:Label></td>
                            <td style="height: 7px"><asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                    border-bottom: black 1px solid; top: 32px" TabIndex="2" Width="600px">
                            </asp:DropDownList></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 20px;">
                            </td>
                            <td style="vertical-align: top; text-align: left">
                            </td>
                        </tr>
                    </table><table>
                        <tr>
                            <td style="width: 20px;">
                            </td>
                            <td>
                                <asp:Label ID="lblServizio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 48px; top: 32px" Width="130px">Servizio</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cmbServizio" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                    border-bottom: black 1px solid; top: 96px" TabIndex="5" Width="600px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 20px; height: 7px">
                                &nbsp; &nbsp;&nbsp;
                            </td>
                            <td style="height: 7px">
                                <asp:Label ID="lblTipoServizioDett" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" Width="130px">Voce DGR</asp:Label></td>
                            <td style="height: 7px">
                                <asp:DropDownList ID="cmbServizioVoce" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
                                    border-bottom: black 1px solid; top: 96px" TabIndex="5" Width="600px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 20px; height: 7px">
                            </td>
                            <td style="height: 7px">
                                <asp:Label ID="LblAppalto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 48px; top: 64px" Width="130px">Num. Repertorio</asp:Label></td>
                            <td style="height: 7px">
                                <asp:DropDownList ID="cmbAppalto" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    top: 64px" TabIndex="7" Width="600px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 20px; height: 7px">
                            </td>
                            <td style="height: 7px">
                            </td>
                            <td style="height: 7px">
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 20px">
                                &nbsp; &nbsp;&nbsp;
                            </td>
                            <td style="width: 303px">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            </td>
                            <td>
                                <br />
                                <br />
                                <br />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
            Style="z-index: 106; left: 224px; top: 480px" ToolTip="Visualizza" />
        &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 648px; top: 480px" ToolTip="Home" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         <asp:HiddenField id="txtid"          runat="server"></asp:HiddenField>
         <asp:HiddenField id="txtSTATO_PF" runat="server"></asp:HiddenField>
    </div>
    </form>
</body>
</html>

