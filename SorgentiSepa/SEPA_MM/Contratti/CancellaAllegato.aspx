<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CancellaAllegato.aspx.vb" Inherits="Contratti_CancellaAllegato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" Text="Vuoi cancellare il file"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="imgConferma" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Procedi.png" style="height: 20px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="imgConferma0" runat="server" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclientclick="self.close();" />
&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="NomeF" runat="server" />
    <asp:HiddenField ID="NomeD" runat="server" />
    <asp:HiddenField ID="NomeX" runat="server" />
    </form>
</body>
</html>
