<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestModPod.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_GestModPod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Pod</title>
    <script src="../../../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <script src="../../../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../../../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        window.name = 'modal';
        function CloseAndRebind(args) {
            GetRadWindow().BrowserWindow.refreshGrid(args);
            GetRadWindow().close();
        };
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well)
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator RenderMode="Classic" Skin="Web20" ID="FormDecorator1" runat="server"
        DecoratedControls="Buttons" ControlsToSkip="Zone" />
    <table class="fontTelerik">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo" runat="server" CssClass="testoGrassettoMaiuscoloBlu"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:CheckBox ID="chkAttivo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                    Text="ATTIVO" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Contratto</td>
                        <td>
                            Pod</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtContratto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="270px" MaxLength="500" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPod" runat="server" Font-Names="Arial" Font-Size="8pt" Width="270px"
                                MaxLength="100" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fornitura</td>
                        <td>
                            Fornitore</td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="cmbTipoFornitura" runat="server" EnableLoadOnDemand="true"
                                IsCaseSensitive="false" Filter="Contains" AutoPostBack="True" 
                                Width="270px">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbFornitore" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                Filter="Contains" AutoPostBack="True" Width="270px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            DESCRIZIONE
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="1000"
                                Width="570px" Height="50px" Style="text-align: left" TextMode="MultiLine"></asp:TextBox>
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
            <td align="right">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" />
                            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="CancelEdit();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="ID" runat="server" Value="0" />
    </form>
</body>
</html>
