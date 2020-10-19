<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sopralluogo.aspx.vb" Inherits="CALL_CENTER_Sopralluogo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sopralluogo</title>
        <script type="text/javascript" language="javascript">
            window.name = "modal";


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
<body style="background-attachment: fixed; background-image: url('Immagini/XBackGround.gif');
    background-repeat: repeat-x;">
    <form id="form1" runat="server" target="modal">
   <table style="width:50%; position: absolute; top: 6px; left: 9px;">
        <tr>
            <td style="font-family: Arial; font-size: 5pt">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                Dettagli del sopralluogo</span></strong>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblNomTec" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="10pt" Text="Nome Tecnico Soprall."></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTecnico" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblNomTec1" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="10pt" Text="Pericolo?"></asp:Label>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbPericolo" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">SI</asp:ListItem>
                                <asp:ListItem Value="0">NO</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                <asp:Label ID="lblNomTec0" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="10pt" Text="Rapporto di Sopralluogo"></asp:Label>
                        </td>
                        <td style="text-align: right">
                <asp:Label ID="lblNomTec2" runat="server" Font-Bold="True" Font-Names="Arial" 
                    Font-Size="10pt" Text="Data Sopralluogo"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataS" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                Width="70px"></asp:TextBox>
                        &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtRapporto" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    Height="100px" MaxLength="2000" TextMode="MultiLine" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 63%">
                    <tr>
                        <td>
                            <asp:ImageButton ID="btnSalva" runat="server" 
                                ImageUrl="~/NuoveImm/Img_Salva.png" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnSalva0" runat="server" 
                                ImageUrl="~/NuoveImm/Img_Stampa.png" />
                        </td>
                        <td>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" onclick="self.close();" style="cursor :pointer " />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


    <asp:HiddenField ID="idSegnalazione" runat="server" />


    </form>
</body>
</html>
