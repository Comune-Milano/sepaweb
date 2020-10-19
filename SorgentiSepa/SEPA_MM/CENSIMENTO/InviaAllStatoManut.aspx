<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InviaAllStatoManut.aspx.vb" Inherits="CENSIMENTO_InviaAllStatoManut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Allegati</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table align="center" style="width: 80%;">
        <tr>
            <td align="center">
                <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="12pt">Allega un documento</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Data Allegato"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                        <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; "
                            TabIndex="2" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtData"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                        
                            
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Descrizione Allegato"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="arial" 
                    Font-Size="8pt" Height="99px" MaxLength="500" TextMode="MultiLine" 
                    Width="499px" TabIndex="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Allegato"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
            
            
                        Style=" width: 671px;" 
                        TabIndex="4" />
                    </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;<br/><br/>  </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right">
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                         
                        TabIndex="5" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img id="imgEsci" alt="Esci" src="..\NuoveImm/Img_EsciCorto.png" 
                        onclick="self.close();" style="cursor:pointer" /></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="cod" runat="server" />
    <asp:HiddenField ID="identificativo" runat="server" />
    <asp:HiddenField ID="sicuro" runat="server" />
    
    <script type="text/javascript">
        function Sicuro() {


            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sicuro di voler eliminare la tipologia selezionata?");
            if (chiediConferma == true) {
                document.getElementById('sicuro').value = '1';
            }
            else {
                document.getElementById('sicuro').value = '0';
            }

        } 
    
    </script>
    
    <script type="text/javascript">
        //document.onkeydown = $onkeydown;


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
    </form>
</body>
            <script  language="javascript" type="text/javascript">
                document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>

</html>
