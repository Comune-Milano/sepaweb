<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Oneri.aspx.vb" Inherits="Contratti_Report_Oneri" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {
          var bt = document.getElementById('btnCerca');
          bt.click();

          return false; 
      }  
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Ricerca Contratti</title>
	</head>
	<body bgcolor="#f2f5f1">
	<script type="text/javascript">
	    document.onkeydown = $onkeydown;
</script>
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Definizione range di date </strong></span><br />
                        <br />
                        <br />
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 50px; position: absolute; top: 131px" Width="6px">al</asp:Label>
                        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 95px; position: absolute; top: 130px"
                            Width="83px" TabIndex="2" ToolTip="GG/MM/AAAA"></asp:TextBox>
                        <br />
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 50px; position: absolute; top: 101px" Width="19px">Dal</asp:Label>
                        &nbsp;<br />
                        &nbsp;&nbsp;
                        <br />
                        &nbsp; &nbsp;&nbsp;<br />
                        <br />
                        &nbsp;<br />
                        &nbsp; &nbsp;<br />
                        &nbsp; &nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 95px; position: absolute; top: 98px" Width="83px" TabIndex="1" ToolTip="GG/MM/AAAA"></asp:TextBox>
                        &nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 334px" Visible="False" Width="525px"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 102; left: 660px; position: absolute; top: 441px" TabIndex="4" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 533px; position: absolute; top: 441px" TabIndex="3" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
                ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 183px; position: absolute;
                top: 98px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Width="1px"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
                ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 183px; position: absolute;
                top: 130px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                Width="1px"></asp:RegularExpressionValidator>
		</form>
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
 

	</body>
</html>
