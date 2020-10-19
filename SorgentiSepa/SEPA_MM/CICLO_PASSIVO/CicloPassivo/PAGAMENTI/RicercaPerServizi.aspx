<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPerServizi.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RicercaPerServizi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title></title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table width="100%" class="FontTelerik">
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td class="TitoloModulo">
                Report - Situazione contabile - Servizi
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnStampa" runat="server" Text="Stampa" ToolTip="Stampa" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 10%">
                            <asp:Label ID="Label4" runat="server" Width="110px" Text="Esercizio Finanziario"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="40%">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="width: 20%">
                            <asp:Label ID="lblStruttura" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" Width="110px" Text="Struttura"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="cmbStruttura" runat="server" Font-Names="arial"
                                Font-Size="8pt" Height="20px" TabIndex="2" Width="400px">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 250px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <%--<td>
                            <asp:CheckBox ID="ChkStampa" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Text="Stampa con sottovoci" Width="200px" TabIndex="3" /><br />
                        </td>--%>
                        <td style="width: 70%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                        <td style="width: 15%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AnnoSelezionato" runat="server" />
    </form>
</body>
</html>
