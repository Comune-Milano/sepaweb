<%@ Page Language="VB" AutoEventWireup="false" CodeFile="prova.aspx.vb" Inherits="ANAUT_prova" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div id="DIVCONFERMA"                          
                            
        style="background-color: #c3c3bb; width: 100%; height: 100%; background-repeat: no-repeat; visibility: visible; z-index: 1000; text-align: left; position: absolute; top: 0px; left: 0px; display: block;">
        <br />
        <br />
        <table style="background-position: center; width: 100%; height: 100%; background-repeat: no-repeat; z-index: 2000; text-align: left; background-image: url('../ImmDiv/SfondoDim1.jpg');">
        <tr style="text-align: center;font-family: arial, Helvetica, sans-serif; font-size: 12pt">
        <td>
        <table style="width: 100%; height: 100%;">
        <tr style="text-align: center;">
        <td>
            &nbsp;<asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
        </td>
        </tr>
                <tr>
        <td>
            
            &nbsp; &nbsp;</td>
        </tr>
                <tr style="text-align: center">
        <td>
            
            <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SI.png"
                        Style="cursor:pointer" 
                        TabIndex="8" 
                onclientclick="document.getElementById('DIVCONFERMA').style.visibility = 'hidden';" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <img alt="Annulla" src="../NuoveImm/Img_NO.png" 
        style="cursor:pointer" onclick="SitIniziale();alert('Operazione Annullata!');document.getElementById('DIVCONFERMA').style.visibility = 'hidden';"/></td>
        </tr>
        </table>
        </td>
        </tr>
        
        </table>
    </div>
    </form>
</body>
</html>
