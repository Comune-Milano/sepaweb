<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU.aspx.vb" Inherits="ANAUT_CreaAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Crea A.U.</title>
    <style type="text/css">
        .CssEtichetta
        {
            padding: 30px,30px,30px,30px;
        }
    </style>
</head>
<script language="javascript" type="text/javascript">
    function ConfInserimentoAU() {
        var chiediConferma;
        var nomeBando = "";
        if (document.getElementById('IDBANDO').value = '1') {

            nomeBando = "2007"
        }
        if (document.getElementById('IDBANDO').value = '2') {

            nomeBando = "2009"
        }
        if (document.getElementById('IDBANDO').value = '3') {

            nomeBando = "2011"
        }

        //var msg1 = "Attenzione, sei sicuro di voler procedere con l\'inserimento dell\'Anagrafe Utenza \""+ nomeBando +"\"?";
        var msg1 = "Attenzione, sei sicuro di voler procedere con l\'inserimento dell\'Anagrafe Utenza selezionata?";

        chiediConferma = window.confirm(msg1);
        if (chiediConferma == true) {
            document.getElementById('ConfAU').value = '1';
        }
        else {
            document.getElementById('ConfAU').value = '0';
            alert('Operazione annullata!');
        }
    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td bgcolor="#932500">
                    <span style="font-size: 12pt; color: #0066FF; font-family: Arial">
                        <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="12pt" ForeColor="White"></asp:Label>
                    </span>
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                        Font-Names="arial" Font-Size="12pt"
                                    Text=" - Selezionare il dichiarante" ForeColor="White"
                                    BackColor="#932500"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="ListaInt" runat="server" Font-Names="ARIAL" 
                                    Font-Size="10pt" BackColor="#FFFFCC" Width="100%">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                        Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                        Width="100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Visible="False" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table align="right" width="350px">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                                    OnClientClick="ConfInserimentoAU();" style="height: 20px" />
                                <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="~/NuoveImm/Img_IndietroGrande.png"
                                    Visible="False" />
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                    OnClientClick="self.close();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="idc" runat="server" />
    <asp:HiddenField ID="t" runat="server" />
    <asp:HiddenField ID="fase" runat="server" />
    <asp:HiddenField ID="procedi" runat="server" />
    <asp:HiddenField ID="iddich" runat="server" />
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="IDA" runat="server" />
    <asp:HiddenField ID="IDCONVOCAZIONE" runat="server" />
    <asp:HiddenField ID="scheda" runat="server" />
    <asp:HiddenField ID="ConfAU" runat="server" />
    <asp:HiddenField ID="IDBANDO" runat="server" />
    </form>
</body>
</html>
