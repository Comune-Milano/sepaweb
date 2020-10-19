<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaLotto.aspx.vb" Inherits="CicloPassivo_CicloPassivo_LOTTI_SceltaLotto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <base target="_self" />
    <title>Scelta tipologia di Appalto</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <table style="width: 100%" class="FontTelerik">
            <tr>
                <td class="TitoloModulo">
                    Contratti - Inserimento
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="ImgProcedi" runat="server" Text="Procedi" ToolTip="Procedi"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="Label1" runat="server">Tipologia contratto</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="rdbType" AppendDataBoundItems="true" Filter="Contains" runat="server"
                                                AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                LoadingMessage="Caricamento..." Width="300px">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="PATRIMONIALE" />
                                                    <telerik:RadComboBoxItem Value="1" Text="NON PATRIMONIALE" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">
                                            <asp:Label ID="lblServizio" runat="server">Esercizio finanziario</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadComboBox ID="cmbEsercizio" AppendDataBoundItems="true" Filter="Contains"
                                                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                                LoadingMessage="Caricamento..." Width="300px">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblStruttura" runat="server">Struttura</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="CmbFiliali" Width="300px" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoLotto" runat="server">Tipologia Lotto</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="cmbTipoLotto" Width="300px" AppendDataBoundItems="true"
                                    Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                    HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="E" Text="EDIFICI" Selected="true" />
                                        <telerik:RadComboBoxItem Value="I" Text="IMPIANTI" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblLotto" runat="server">Lotto da associare al nuovo contratto</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="cmblotto" Width="300px" AppendDataBoundItems="true" Filter="Contains"
                                    runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                                    LoadingMessage="Caricamento...">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblErrore" runat="server" Visible="False" Font-Bold="True" Font-Names="arial"
                                    Font-Size="8pt" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <asp:HiddenField ID="idfornitore" runat="server" Value="0" />
                        <asp:HiddenField ID="x" runat="server" Value="0" />
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        function ConfermaEsci() {
            var chiediConferma
            chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            if (chiediConferma == true) {
                self.close();
            }

        }
    </script>
</body>
</html>
