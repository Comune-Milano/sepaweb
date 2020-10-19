<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PaginaScelta.aspx.vb" Inherits="Contratti_Proroga_AssDefinitiva_PaginaScelta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <div>
        <table style="padding: 20px; width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                        Width="100%">Selezionare l'operazione che si intende effettuare:</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdbScelta" runat="server" Font-Names="ARIAL" 
                        Font-Size="11pt">
                        <asp:ListItem Value="proroga">Proroga</asp:ListItem>
                        <asp:ListItem Value="assegn_definitiva">Assegn. definitiva</asp:ListItem>
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
                                <img id="exit" alt="Esci" src="../../NuoveImm/Img_EsciCorto.png" title="Esci" style="cursor: pointer"
                                    onclick="self.close();" />
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
