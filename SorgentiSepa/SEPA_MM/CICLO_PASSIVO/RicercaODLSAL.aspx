<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaODLSAL.aspx.vb" Inherits="CICLO_PASSIVO_RicercaODLSAL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Ricerca Elenco ODL su SAL</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <div>
            <table style="width: 100%">
                <tr>
                    <td class="TitoloModulo">Report - Situazione contabile - ODL/SAL</td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAvviaRicerca" runat="server"
                                        Text="Avvia ricerca"
                                        Style="cursor: pointer;" ToolTip="Avvia ricerca" />

                                </td>
                                <td>
                                    <asp:Button ID="btnEsci" runat="server" CausesValidation="False" Text="Esci"
                                        Style="cursor: pointer;" ToolTip="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>Numero repertorio</td>
                                <td>
                                    <telerik:RadTextBox ID="txtNumRepertorio" runat="server" Width="100px"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Fornitore
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbfornitore" runat="server" AppendDataBoundItems="true" Height="550"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        Width="350px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:Label ID="Label2" runat="server">Esercizio Finanziario</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbesercizio" runat="server" AppendDataBoundItems="true" 
                                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        Width="350px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Stato SAL
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbStatoSAL" runat="server" AppendDataBoundItems="true" 
                                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        Width="350px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                            <telerik:RadComboBoxItem Value="2" Text="FIRMATO" />
                                            <telerik:RadComboBoxItem Value="1" Text="FIRMATO CON RISERVA" />
                                            <telerik:RadComboBoxItem Value="0" Text="NON FIRMATO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Stato liquidazione
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbLiquidazione" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        Width="350px">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Text="" Selected="true" />
                                            <telerik:RadComboBoxItem Value="COMPLETO" Text="COMPLETO" />
                                            <telerik:RadComboBoxItem Value="DA LIQUIDARE" Text="DA LIQUIDARE" />
                                            <telerik:RadComboBoxItem Value="PARZIALE" Text="PARZIALE" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:Label ID="Label1" runat="server">Servizio</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbServizio" runat="server" AppendDataBoundItems="true" Height="550"
                                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        Width="350px">
                                    </telerik:RadComboBox>
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
