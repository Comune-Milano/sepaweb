<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrendiAppuntamento.aspx.vb" Inherits="ANAUT_PrendiAppuntamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    //document.onkeydown = $onkeydown;


    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed != 13) {
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
    }


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt" 
                        
                        
                        Text="Indicare la data di presa in carico da parte di AUCM e premere il pulsante procedi."></asp:Label>
                    <br />
                    <br />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="19" Style=" width: 93px;"
                            TabIndex="9"></asp:TextBox>
                    <br />
&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" 
                        ForeColor="Red" Visible="False"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Procedi.png" 
                        onclientclick="Nascondi();" TabIndex="3" style="height: 20px" />&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="ImageButton2" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclientclick="self.close();" TabIndex="4" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    <script  language="javascript" type="text/javascript">
        function Nascondi() {
            document.getElementById('ImageButton1').style.visibility = 'hidden';
            document.getElementById('ImageButton1').style.position = 'absolute';
            document.getElementById('ImageButton1').style.left = '-100px';
            document.getElementById('ImageButton1').style.display = 'none';
        }

    </script>
    </form>
</body>
</html>
