<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProceduraDuplicazionePF.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_Plan_ProceduraDuplicazionePF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Duplicazione piano finanziario</title>
    <script type="text/javascript">
        function chiediConferma() {
            var chiedi = window.confirm('L\'operazione che si sta per effettuare\nè irreversibile. Proseguire?');
            if (chiedi == true) {
                document.getElementById('HiddenFieldConferma').value = '1';
                document.getElementById('divLoading').style.display = 'block';
            }
        };
    </script>
</head>
<body style="background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg');
    width: 770px; height: 590px; ">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2" style="height:10px;"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label2" runat="server" Text="Duplicazione Piano Finanziario" Font-Bold="True"
                        Font-Names="Arial" ForeColor="Maroon" Font-Size="14pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="height: 15px; width: 100%" colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Struttura Piano Finanziario con importi azzerati" Font-Names="Arial" Font-Size="9pt"></asp:Label>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Names="Arial" Font-Size="9pt"
                        AutoPostBack="True" Visible="false">
                        <asp:ListItem Value="1" Selected="True">Struttura Piano Finanziario con importi azzerati</asp:ListItem>
                        <asp:ListItem Value="2">Struttura e valori con variazione percentuale delle voci reversibili (voci non reversibili azzerate)</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="height: 15px;" colspan="2">
                </td>
            </tr>
            <tr>
                <td style="height: 360px; vertical-align: top;" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="Label1" runat="server" Text="Variazione percentuale" Font-Names="Arial"
                                    Font-Size="10pt" Visible="False"></asp:Label>
                            </td>
                            <td style="width: 75%">
                                <asp:TextBox ID="TextBoxPercentuale" runat="server" MaxLength="3" Width="40px" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ImageButton ID="ImageButtonAvvia" runat="server" ImageUrl="../../../NuoveImm/Img_Avvia.png"
                        OnClientClick="chiediConferma();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenFieldConferma" runat="server" Value="0" />
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('divLoading').style.display = 'none';
    </script>
</body>
</html>
