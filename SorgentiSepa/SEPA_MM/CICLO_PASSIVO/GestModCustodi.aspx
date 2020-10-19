<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestModCustodi.aspx.vb"
    Inherits="CICLO_PASSIVO_GestModCustodi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestione CUSTODI</title>
    <script src="../Standard/Scripts/jsMessage.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
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
    <form id="form1" runat="server" target="modal">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator RenderMode="Classic" Skin="Web20" ID="FormDecorator1" runat="server"
        DecoratedControls="Buttons" ControlsToSkip="Zone" />
    <table style="width: 730px;" class="FontTelerik">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblTitolo" runat="server" Text="" CssClass="testoGrassettoMaiuscoloBlu"
                    Font-Size="20px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            Matricola*
                            <asp:TextBox ID="txtMatricola" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="250px" MaxLength="9" Style="text-align: left; color: #CC3300; background-color: #FFFFCC;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkDipMM" runat="server" Text="Dipendente MM" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAllErpAssegnato" runat="server" Text="Alloggio ERP Assegnato" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cognome*
                        </td>
                        <td>
                            Nome*
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCognome" runat="server" Font-Names="Arial" Font-Size="8pt" Width="330px"
                                MaxLength="50" Style="text-align: left" TabIndex="1"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtNome" runat="server" Font-Names="Arial" Font-Size="8pt" Width="325px"
                                MaxLength="50" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            E-mail
                        </td>
                        <td>
                            Tel. Aziendale
                        </td>
                        <td>
                            Cell. Aziendale
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtMail" runat="server" Font-Names="Arial" Font-Size="8pt" Width="330px"
                                MaxLength="100" Style="text-align: left"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTel" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"
                                MaxLength="16" Style="text-align: left"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCel" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"
                                MaxLength="16" Style="text-align: left"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            Note
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Width="700px"
                                MaxLength="4000" Style="text-align: left" Height="60px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: right">
                            <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" />
                            <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="CancelEdit();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="ID" runat="server" Value="0" />
    <asp:HiddenField ID="idEdifOld" runat="server" Value="0" />
    </form>
</body>
</html>
