<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRicPostAler.aspx.vb" Inherits="ANAUT_RicercaRicPostAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        //if (event.keyCode==13) 
        //{  
        //alert('Usare il tasto <Avvia Ricerca>');
        //history.go(0);
        //event.keyCode=0;
        //}  
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Ricerca</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body bgColor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
	<script type="text/javascript">
	    //document.onkeydown=$onkeydown;
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
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Diffide/Ricevute non Rispondenti</strong></span><br />
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
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton 
                ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 539px; position: absolute; top: 364px" 
                TabIndex="6" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 406px; position: absolute; top: 364px" 
                TabIndex="5" ToolTip="Avvia Ricerca" />
									<asp:label id="Label2" runat="server" 
                Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 104; left: 50px; position: absolute; top: 129px; height: 12px;">Sede T./Sportello</asp:label>
									<asp:label id="Label4" runat="server" 
                Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 268px; position: absolute; top: 156px">Al</asp:label>
                <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 287px; position: absolute; top: 152px"
                            TabIndex="4" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 361px; position: absolute; top: 154px" TabIndex="-1"                             
                            
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtStipulaDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 173px; position: absolute; top: 153px"
                            TabIndex="3" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 248px; position: absolute; top: 155px" TabIndex="-1" 
                            
                            
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:label id="Label1" runat="server" Font-Size="X-Small" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 157px">Diffide invate dal</asp:label>
									<asp:label id="Label10" runat="server" 
                Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 50px; position: absolute; top: 100px">Bando</asp:label>
									<asp:DropDownList id="cmbBando" tabIndex="1" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                
                style="border: 1px solid black; z-index: 111; left: 173px; position: absolute; top: 97px" 
                AutoPostBack="True" CausesValidation="True"></asp:DropDownList>
                <asp:DropDownList id="cmbFiliale" tabIndex="2" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                style="border: 1px solid black; z-index: 111; left: 173px; position: absolute; top: 124px"></asp:DropDownList>
		</form>
	</body>
</html>


