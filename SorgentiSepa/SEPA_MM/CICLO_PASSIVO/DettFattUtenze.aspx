<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettFattUtenze.aspx.vb"
    Inherits="CICLO_PASSIVO_DettFattUtenze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettaglio Fatture Utenze</title>
    <style type="text/css">
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
        .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
        }
        .style1
        {
            font-family: Arial;
            font-size: 12pt;
            color: #990000;
        }
    </style>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <strong style="font-size: 14pt">Dettaglio abbinamento</strong>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            Struttura competente*
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbStruttra" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Piano Finanziario*
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbEsercizio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Tipo tracciato*
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoTracciato" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Fornitore*
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbFornitore" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Voce BP*
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbPfVoci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Servizio
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbServizio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Voce servizio
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbPfVociImporto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="ESCI" ToolTip="Chiude la finestra"
                                OnClientClick="self.close();return false;" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSalva" runat="server" CssClass="bottone" Text="SALVA" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="idPiano" runat="server" Value="0" />
                <asp:HiddenField ID="idFornitore" runat="server" Value="0" />
                <asp:HiddenField ID="idVocePf" runat="server" Value="0" />
                <asp:HiddenField ID="idVocePfImporto" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
