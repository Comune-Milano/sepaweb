<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU_new.aspx.vb" Inherits="ANAUT_CreaAU_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Crea scheda AU</title>
    <link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-attachment: fixed; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; width: 792px;">
    <form id="form1" runat="server">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <br />
        &nbsp
        <asp:Label ID="lblTitolo" runat="server" Text="Creazione scheda AU"></asp:Label>
    </span></strong>
    <br />
    <table style="width: 775px;">
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: left; border-bottom-style: solid; border-bottom-width: 1px;
                border-bottom-color: #000000;" colspan="1" class="style1">
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="#3366CC"
                    Font-Bold="True">Informazioni import dati</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: left; border-bottom-style: solid; border-bottom-width: 1px;
                border-bottom-color: #000000;" colspan="1">
                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="#3366CC"
                    Font-Bold="True">Il dichiarante selezionato è l&#39;intestatario del contratto</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <asp:RadioButtonList ID="ListaInt" runat="server" Font-Names="ARIAL" 
                    Font-Size="10pt" Enabled="False">
                </asp:RadioButtonList>
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
        <tr>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
            <td style="text-align: right; vertical-align: top">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left">
                            <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" />
                            <asp:Label ID="lblMsgVSA" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Italic="True"
                                Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
            <td style="text-align: right; vertical-align: top">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left" width="80%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" width="80%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" width="80%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            <asp:Button ID="btnProcedi" runat="server" ToolTip="Procedi" CssClass="bottone" Text="Procedi"
                                Width="75px" 
                                onclientclick="document.getElementById('btnProcedi').style.visibility = 'hidden';" />
                        </td>
                        <td>
                            <asp:Button ID="imgUscita" runat="server" ToolTip="Esci" CssClass="bottone" Text="Esci"
                                Width="42px" OnClientClick="self.close()" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right; vertical-align: top">
                &nbsp;
            </td>
        </tr>
    </table>
    <div align='center' id='dvvvPre' style='position: absolute; background-color: #ffffff;
        text-align: center; width: 98%; height: 98%; top: 1px; left: 1px; z-index: 10;
        border: 1px dashed #660000; font-size: 10px;'>
        <br />
        <img src='../NuoveImm/load.gif' alt='caricamento in corso' /><br />
        elaborazione in corso...attendere
    </div>
    <asp:HiddenField ID="idcont" runat="server" />
    <asp:HiddenField ID="t" runat="server" />
    <asp:HiddenField ID="fase" runat="server" />
    <asp:HiddenField ID="procedi" runat="server" />
    <asp:HiddenField ID="iddich" runat="server" Value="0" />
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="IDA" runat="server" />
    <asp:HiddenField ID="IDCONVOCAZIONE" runat="server" />
    <asp:HiddenField ID="scheda" runat="server" />
    <asp:HiddenField ID="AUS" runat="server" />
    <asp:HiddenField ID="S392" runat="server" />
    <script language="javascript" type="text/javascript">
        function Nascondi() {
            document.getElementById('ImageButton1').style.visibility = 'hidden';
            document.getElementById('ImageButton1').style.position = 'absolute';
            document.getElementById('ImageButton1').style.left = '-100px';
            document.getElementById('ImageButton1').style.display = 'none';
            document.getElementById('dvvvPre').style.visibility = 'visible';
        }
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        //ImageButton1
    </script>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
