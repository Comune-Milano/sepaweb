<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreaAU.aspx.vb" Inherits="ANAUT_CreaAU" %>

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
                <td>
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label></td>
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
    <div align='center' id='dvvvPre' 
        style='position:absolute; background-color:#ffffff; text-align:center; width:98%; height:98%; top:1px; left:1px; z-index:10; border:1px dashed #660000;font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' /><br>elaborazione in corso...attendere</div>
    <asp:HiddenField ID="idc" runat="server" />
    <asp:HiddenField ID="t" runat="server" />
    <asp:HiddenField ID="fase" runat="server" />
    <asp:HiddenField ID="procedi" runat="server" />
    <asp:HiddenField ID="iddich" runat="server" Value ="0" />
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="IDA" runat="server" />
    <asp:HiddenField ID="IDCONVOCAZIONE" runat="server" />
    <asp:HiddenField ID="scheda" runat="server" />
    <asp:HiddenField ID="AUS" runat="server" />
     <script  language="javascript" type="text/javascript">
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
     </body>
</html>
