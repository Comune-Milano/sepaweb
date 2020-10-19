<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Interessi.aspx.vb" Inherits="Contratti_Interessi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
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
    <form id="form1" runat="server" defaultbutton="imgProcedi">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label7" runat="server" Text="Calcolo Interessi su Deposito"></asp:Label><br />
                    </strong></span>
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Width="144px">Calcola interessi fino al:</asp:Label>&nbsp;
                    <asp:TextBox ID="txtData" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" TabIndex="1" Width="77px" MaxLength="10"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtData"
                        Display="Dynamic" ErrorMessage="gg/mm/aaaa" Font-Names="arial" Font-Size="8pt"
                        TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator><br />
                    &nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Width="492px">Gli interessi saranno inseriti nella prossima bollettazione utile</asp:Label><br />
                    <br />
                    <br />
                    <table width="90%">
                        <tr>
                            <td style="width: 8px">
                                &nbsp;</td>
                            <td style="width: 76px">
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 50px; position: static; top: 188px" Width="54px">Complesso</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; z-index: 111; left: 116px; border-left: black 1px solid;
                                    border-bottom: black 1px solid; position: static; top: 185px" TabIndex="2" 
                                    Width="283px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 8px">
                                &nbsp;</td>
                            <td style="width: 76px">
                                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 65px; position: static; top: 405px" Width="36px">Edificio</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
                                    Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                                    border-top: black 1px solid; z-index: 111; left: 131px; border-left: black 1px solid;
                                    border-bottom: black 1px solid; position: static; top: 402px" TabIndex="3" 
                                    Width="283px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 8px">
                                &nbsp;</td>
                            <td style="width: 76px">
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Style="z-index: 100; left: 65px; position: static; top: 431px" Width="29px">Unità</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cmbUnita" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 131px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    position: static; top: 427px" TabIndex="4" Width="283px">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Visible="False"
                        Width="700px" TabIndex="-1"></asp:Label><br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 712px; position: absolute; top: 501px" 
            ToolTip="Home" TabIndex="6" />
        <asp:ImageButton ID="imgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
            
            Style="z-index: 101; left: 617px; position: absolute; top: 501px; " 
            ToolTip="Procedi" TabIndex="5" />
    
    </div>
    </form>
        <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>