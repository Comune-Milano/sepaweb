<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU.aspx.vb" Inherits="Contratti_CreaAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title>Crea A.U.</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold; color: #FF0000; text-align: center;">
                    ATTENZIONE...STAI INSERENDO UNA SCHEDA AU 2009! Se intendi inserire una scheda 
                    AU 2011 utilizza le funzioni agenda del modulo ANAGRAFE UTENZA.</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" 
                        Text="Scegliere il Dichiarante"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="ListaInt" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Procedi.png" onclientclick="Nascondi();" />
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="ImageButton2" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclientclick="self.close();" />
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
     <script  language="javascript" type="text/javascript">
         function Nascondi() {
             document.getElementById('ImageButton1').style.visibility = 'hidden';
             document.getElementById('ImageButton1').style.position = 'absolute';
             document.getElementById('ImageButton1').style.left = '-100px';
             document.getElementById('ImageButton1').style.display = 'none';
         }
         //document.getElementById('dvvvPre').style.visibility = 'hidden';
         //ImageButton1
    </script>
    
    </form>
     </body>
</html>
