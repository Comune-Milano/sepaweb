<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU1.aspx.vb" Inherits="Contratti_CreaAU1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title>Anagrafe</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        &nbsp;&nbsp;
        <table style="width:100%;">
            <tr>
                <td>
                    <b>E&#39; stata già inserita una dichiarazione di anagrafe utenza con redditi 2008 
                    per questo rapporto. Visualizzare l&#39;elenco e modificare se necessario.</b></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclientclick="self.close();" />
                            </td>
            </tr>
        </table>
    </div>
    </form>

</body>
</html>
