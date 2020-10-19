<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrazioneTelematica.aspx.vb" Inherits="Contratti_RegistrazioneTelematica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
      alert('Usare il tasto <Avvia Ricerca>');
      history.go(0);
      event.keyCode=0;
      }  
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Registrazione Telematica</title>
	</head>
	<body bgcolor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
	<script type="text/javascript">
	    document.onkeydown = $onkeydown;


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
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtStipulaDal">
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Registrazione 
                        Telematica</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                            Font-Size="8pt" ForeColor="#CC3300" Visible="False" Width="770px"></asp:Label>
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
                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 112; left: 189px; position: absolute; top: 121px; width: 347px;">Attenzione...Questa data sarà usata per calcolare eventuali sanzioni!</asp:Label>
                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 112; left: 22px; position: absolute; top: 127px" 
                            Width="107px">Data di invio</asp:Label>
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 112; left: 22px; position: absolute; top: 91px" 
                            Width="107px">Stipula Dal</asp:Label>
                        <asp:TextBox ID="txtDataInvio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 95px; position: absolute; top: 124px; right: 637px;"
                            TabIndex="3" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <asp:TextBox ID="txtStipulaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 95px; position: absolute; top: 88px; right: 637px;"
                            TabIndex="1" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 112; left: 189px; position: absolute; top: 92px" 
                            Width="22px">Al</asp:Label>
                        <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 208px; position: absolute; top: 88px"
                            TabIndex="2" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                        <br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataInvio"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                            Style="left: 172px; position: absolute; top: 127px" TabIndex="300" 
                            
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                            Style="left: 172px; position: absolute; top: 91px" TabIndex="300" 
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                            Style="left: 286px; position: absolute; top: 91px" TabIndex="300" 
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                        <br />
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
            &nbsp;<asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 657px; position: absolute; top: 511px" 
                TabIndex="5" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 517px; position: absolute; top: 511px; right: 862px; font-weight: 700;" 
                TabIndex="4" ToolTip="Avvia Ricerca" />
									
            &nbsp; &nbsp; &nbsp;&nbsp;
                                    
		</form>
	    </body>

</html>