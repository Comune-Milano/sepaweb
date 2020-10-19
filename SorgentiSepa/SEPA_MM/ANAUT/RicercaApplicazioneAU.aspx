<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaApplicazioneAU.aspx.vb" Inherits="ANAUT_RicercaApplicazioneAU" %>

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
    <base target="_self"/>
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
                            Dichiarazioni Applicazione A.U.</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:CheckBox ID="CheckBox1" runat="server" 
                            style="position:absolute; top: 173px; left: 421px;" TabIndex="6"/>
                            <asp:CheckBox ID="CheckBox2" runat="server" 
                            style="position:absolute; top: 201px; left: 421px;" TabIndex="7"/>
                            <asp:CheckBox ID="CheckBox3" runat="server" 
                            style="position:absolute; top: 228px; left: 421px;" TabIndex="8"/>
                            <asp:CheckBox ID="CheckBox4" runat="server" 
                            style="position:absolute; top: 255px; left: 421px;" TabIndex="9"/>
                            <asp:CheckBox ID="CheckBox5" runat="server" 
                            style="position:absolute; top: 281px; left: 421px;" TabIndex="10"/>
                            <asp:CheckBox ID="CheckBox6" runat="server" 
                            style="position:absolute; top: 306px; left: 421px;" TabIndex="11"/>
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
                Style="z-index: 100; left: 539px; position: absolute; top: 468px" 
                TabIndex="13" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 401px; position: absolute; top: 468px" 
                TabIndex="12" ToolTip="Avvia Ricerca" />
									<asp:label id="Label2" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 104; left: 50px; position: absolute; top: 79px; height: 12px;">Sede Territoriale</asp:label>
									<asp:label id="Label4" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 268px; position: absolute; top: 111px">Al</asp:label>
                <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 287px; position: absolute; top: 108px"
                            TabIndex="3" ToolTip="gg/mm/aaaa" Width="70px">30/09/2009</asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 361px; position: absolute; top: 112px" TabIndex="-1"                             
                            
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtStipulaDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 173px; position: absolute; top: 109px"
                            TabIndex="2" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 248px; position: absolute; top: 113px" TabIndex="-1" 
                            
                            
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:label id="Label1" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 111px">Contratti Stipulati dal</asp:label>
                <asp:label id="Label3" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 144px">N. Componenti da:</asp:label>
                <asp:label id="Label6" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 177px">Presenza di &lt;15 anni rispetto l&#39;anno del reddito considerato</asp:label>
                <asp:label id="Label7" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 205px">Presenza di &gt;65 anni rispetto l&#39;anno del reddito considerato</asp:label>
                <asp:label id="Label8" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 232px">Presenza di invalidi al 100% con indennità di accompagnamento</asp:label>
                <asp:label id="Label9" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 259px">Presenza di invalidi al 100% senza indennità di accompagnamento</asp:label>
                <asp:label id="Label10" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 285px">Presenza di invalidi tra 66% e 99%</asp:label>
                <asp:label id="Label11" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 310px">Redditi prevalentemente dipendenti</asp:label>
                <asp:label id="Label5" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 268px; position: absolute; top: 144px">A:</asp:label>
                <asp:DropDownList id="cmbFiliale" tabIndex="1" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                
                
                style="border: 1px solid black; z-index: 111; left: 173px; position: absolute; top: 76px"></asp:DropDownList>
                <asp:DropDownList id="cmbNumCDa" tabIndex="4" 
                runat="server" Height="20px" Width="70px" 
                
                
                style="border: 1px solid black; z-index: 111; left: 173px; position: absolute; top: 141px"></asp:DropDownList>
                <asp:DropDownList id="cmbNumCA" tabIndex="5" 
                runat="server" Height="20px" Width="70px" 
                
                
                
                style="border: 1px solid black; z-index: 111; left: 287px; position: absolute; top: 141px"></asp:DropDownList>
		</form>
	</body>
</html>
