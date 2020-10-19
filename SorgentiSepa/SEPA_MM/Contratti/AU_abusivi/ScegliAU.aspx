<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScegliAU.aspx.vb" Inherits="Contratti_AU_abusivi_ScegliAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scelta Anagrafe Utenza</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="padding: 20px; width: 100%;">
            <tr>
                <td bgcolor="#932500">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                        Width="100%" ForeColor="White">Selezionare l&#39;anagrafe utenza ART. 15 che si desidera caricare:</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdbListaAU" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" BackColor="#FFFFCC" Width="100%">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table align="right">
                        <tr>
                            <td align="center">
                                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                    OnClientClick="self.close();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="CodContratto" runat="server" />
    </div>
    <script language="javascript" type="text/javascript">
       
    </script>
    </form>
</body>
</html>
