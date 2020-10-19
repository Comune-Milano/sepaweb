<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Canone_OA.aspx.vb" Inherits="Contratti_Canone_OA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
		    var Uscita;
		    Uscita = 0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Canone 431/98</title>
</head>
<body>
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
    <form id="form1" runat="server" defaultbutton="ImgProcedi" 
    defaultfocus="txtCanoneCorrente">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Canone
                        per abbinamento O.A.</strong></span><br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    <div style="position:absolute;width: 728px; top: 258px; left: 49px; overflow: auto; visibility: visible; height: 224px;">
                        <asp:Label ID="Label4" runat="server" Font-Names="Courier New" Font-Size="8pt" 
                            BackColor="#FFFFCC" BorderColor="Black" BorderWidth="1px" Width="719px"></asp:Label>
                    </div>
                    <br />
                    &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCanoneCorrente"
                        ErrorMessage="Non valido (0,00)" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                        Style="left: 138px; position: absolute; top: 118px" 
                        ValidationExpression="\b\d*,\d{2}\b"></asp:RegularExpressionValidator>
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txtData" ErrorMessage="Non Valido (gg/mm/aaaa)" Font-Bold="True"
                        Font-Names="arial" Font-Size="9pt" Height="1px" Style="z-index: 105; left: 138px;
                        position: absolute; top: 162px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="155px"></asp:RegularExpressionValidator>
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                        Style="left: 623px; position: absolute; top: 515px; right: 97px;" 
                        TabIndex="3" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 712px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="4" />
                    &nbsp;
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 128px; position: absolute; top: 98px">200% Valore Locativo + ISTAT</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 98px">Canone Annuo</asp:Label>
                    <asp:TextBox ID="txtCanoneCorrente" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="50" Style="z-index: 105; left: 46px; position: absolute; top: 114px"
                        TabIndex="1" Width="82px" ReadOnly="True"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        
                        Style="z-index: 104; left: 46px; position: absolute; top: 192px; width: 249px; height: 54px;"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 104; left: 46px; position: absolute; top: 144px">Data di Entrata</asp:Label>
                    <asp:TextBox ID="txtData" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="50"
                        Style="z-index: 105; left: 46px; position: absolute; top: 160px" TabIndex="2"
                        Width="82px"></asp:TextBox>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
