<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU1.aspx.vb" Inherits="ANAUT_CreaAU1" %>

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
                    <b>E&#39; stata già inserita una dichiarazione di anagrafe utenza con redditi 2014 
                    per questo rapporto. Premere il pulsante &quot;Visualizza&quot; per visualizzare la scheda 
                    AU.</b></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                                
                                <asp:Image ID="imgVisualizza" runat="server" onclick="Visualizza();" style="cursor:pointer"
                                    ImageUrl="~/NuoveImm/Img_Visualizza.png" />
&nbsp;&nbsp;&nbsp;&nbsp;
                                
                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclientclick="self.close();" />
                            </td>
            </tr>
        </table>
        <asp:HiddenField ID="dichiarazione" runat="server" />

        <script type="text/javascript">
            function Visualizza() {
                window.open('max.aspx?ID=' + document.getElementById('dichiarazione').value +  '&CHIUDI=1&CH=1', 'Dettagli', 'height=540,top=200,left=350,width=670'); 
                self.close();
                
            }

        </script>
    </div>
    </form>

</body>
</html>
