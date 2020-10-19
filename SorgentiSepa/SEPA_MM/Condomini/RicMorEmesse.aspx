<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicMorEmesse.aspx.vb" Inherits="Condomini_RicMorEmesse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RicercaMorosita</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
            font-weight: bold;
        }
    </style>
     <script type="text/javascript">
           function CompletaData(e, obj) {
               // Check if the key is a number
               var sKeyPressed;

               sKeyPressed = (window.event) ? event.keyCode : e.which;

               if (sKeyPressed < 48 || sKeyPressed > 57) {
                   if (sKeyPressed != 8 && sKeyPressed != 0) {
                       // don't insert last non-numeric character
                       if (window.event) {
                           event.keyCode = 0;
                       }
                       else {
                           e.preventDefault();
                       }
                   }
               }
               else {
                   if (obj.value.length == 2) {
                       obj.value += "/";
                   }
                   else if (obj.value.length == 5) {
                       obj.value += "/";
                   }
                   else if (obj.value.length > 9) {
                       var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                       if (selText.length == 0) {
                           // make sure the field doesn't exceed the maximum length
                           if (window.event) {
                               event.keyCode = 0;
                           }
                           else {
                               e.preventDefault();
                           }
                       }
                   }
               }
           }
</script>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
    <form id="form1" runat="server">


    <table style="width:78%;">
        <tr>
            <td>
                <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="16pt" ForeColor="#660000" TabIndex="9" 
                    Text="Ricerca Morosità Emesse" Width="758px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                Elenco Condomini su cui sono state emesse Morosità</td>
        </tr>
        <tr>
            <td>
    <asp:DropDownList ID="cmbCondominio" runat="server" style="width: 90%;" 
        Font-Names="Arial" Font-Size="9pt" TabIndex="4">
    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style1">
                            Periodo Riferimento Dal</td>
                        <td>
                            <asp:TextBox ID="txtRifDa" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="75px"></asp:TextBox>
                        </td>
                        <td class="style1">
                            al </td>
                        <td>
                            <asp:TextBox ID="txtRifAl" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="75px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" >
                <table>
                    <tr>
                        <td>
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
            
             TabIndex="1"
            ToolTip="Avvia Ricerca" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <asp:Image ID="Home" runat="server" onclick="parent.main.location.replace('pagina_home.aspx');" style="cursor :pointer"  ImageUrl="Immagini/Img_Home.png" />
                        </td>
                        <td>
                                 &nbsp;
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


    </form>
</body>
</html>
