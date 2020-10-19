<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginFondoSolidarieta.aspx.vb"
    Inherits="Fondo_solidarieta_LoginFondoSolidarieta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Accesso Fondo Solidarieta</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function chiudi() {
            self.close();
        };
        function closeWindow(sender, args, nomeRad) {
            var radwindow = $find(nomeRad);
            radwindow.close();
        };
           
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <telerik:RadWindow ID="RadWindowInfoRU" runat="server" CenterIfModal="true" Modal="True"
            VisibleStatusbar="False" AutoSize="false" Behavior="Pin, Move, Resize, Close"
            Skin="Web20" Height="370" Width="370" OnClientClose="chiudi">
            <ContentTemplate>
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="font-family: Arial; font-size: 11pt; font-weight: bold; text-align: center;">
                            ACCESSO
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="Table3" border="0" width="100%">
                                <tr>
                                    <td style="width: 20%">
                                        Municipo
                                    </td>
                                    <td style="width: 80%">
                                        <telerik:RadComboBox ID="cmbZona" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                            Width="100%">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        Password:
                                    </td>
                                    <td style="width: 80%">
                                        <asp:TextBox ID="txtPassword" runat="server" ClientIDMode="Static" CssClass="CssMaiuscolo"
                                            Text="" Width="100%" TextMode="Password">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="#FF3300"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 185px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 70%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 30%" align="right">
                                        <telerik:RadButton ID="RadButtonLogin" runat="server" Text="Login" AutoPostBack="True">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadWindow>
    </div>
    </form>
</body>
</html>
