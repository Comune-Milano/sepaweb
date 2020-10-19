<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaNonRispondenti.aspx.vb" Inherits="ANAUT_RicercaNonRispondenti" %>

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
			</head>
	<body bgcolor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
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
                            Non Rispondenti AU</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:CheckBox ID="CheckBox1" runat="server" 
                            style="position:absolute; top: 182px; left: 440px;" Checked="True" 
                            TabIndex="7" />
                            <asp:CheckBox ID="CheckBox2" runat="server" 
                            style="position:absolute; top: 211px; left: 440px;" Checked="True" 
                            TabIndex="8" />
                            <asp:CheckBox ID="CheckBox3" runat="server" 
                            style="position:absolute; top: 235px; left: 440px;" Checked="True" 
                            TabIndex="9" AutoPostBack="True" CausesValidation="True" />
                            <asp:CheckBox ID="CheckBox4" runat="server" 
                            style="position:absolute; top: 259px; left: 440px;" Checked="True" 
                            TabIndex="10" Enabled="False" />
                            <asp:CheckBox ID="CheckBox5" runat="server" 
                            style="position:absolute; top: 283px; left: 440px;" Checked="True" 
                            TabIndex="11" Enabled="False" />
                            <asp:CheckBox ID="CheckBox6" runat="server" 
                            style="position:absolute; top: 307px; left: 440px;" Checked="True" 
                            TabIndex="12" Enabled="False" />
                            <asp:CheckBox ID="CheckBox7" runat="server" 
                            style="position:absolute; top: 331px; left: 440px;" Checked="True" 
                            TabIndex="13" Enabled="False" />
                            <asp:CheckBox ID="CheckBox8" runat="server" 
                            style="position:absolute; top: 355px; left: 440px;" Checked="True" 
                            TabIndex="14" Enabled="False" />
                            <asp:CheckBox ID="CheckBox9" runat="server" 
                            style="position:absolute; top: 379px; left: 440px;" Checked="True" 
                            TabIndex="15" Enabled="False" />
                            <asp:CheckBox ID="CheckBox10" runat="server" 
                            style="position:absolute; top: 403px; left: 440px;" Checked="True" 
                            TabIndex="16" Enabled="False" />
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
                Style="z-index: 100; left: 539px; position: absolute; top: 499px" 
                TabIndex="11" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 406px; position: absolute; top: 500px; right: 833px;" 
                TabIndex="10" ToolTip="Avvia Ricerca" />
									<asp:label id="Label2" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                style="z-index: 104; left: 50px; position: absolute; top: 91px; height: 12px;">Sportello</asp:label>
									<asp:label id="Label4" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 268px; position: absolute; top: 115px">Al</asp:label>
                <asp:label id="Label5" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 268px; position: absolute; top: 142px">Al</asp:label>
                <asp:label id="Label6" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 19px; position: absolute; top: 457px; width: 608px; height: 31px;" 
                ForeColor="#CC0000">ATTENZIONE...si consiglia di effettuare la ricerca e la creazione delle diffide su un numero non elevato di soggetti per evitare lunghe attese durante la fase di generazione delle lettere.</asp:label>
                <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 287px; position: absolute; top: 112px"
                            TabIndex="4" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                            <asp:TextBox ID="AAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 287px; position: absolute; top: 140px"
                            TabIndex="6" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 365px; position: absolute; top: 114px" TabIndex="-1"                             
                            
                
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="AAl"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 365px; position: absolute; top: 141px" TabIndex="-1"                             
                            
                
                
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtStipulaDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 172px; position: absolute; top: 113px"
                            TabIndex="3" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                            <asp:TextBox ID="ADal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 113; left: 172px; position: absolute; top: 140px"
                            TabIndex="5" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 248px; position: absolute; top: 114px" TabIndex="-1" 
                            
                            
                
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                runat="server" ControlToValidate="ADal"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                            Style="left: 248px; position: absolute; top: 143px" TabIndex="-1" 
                            
                            
                
                
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:label id="Label1" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 116px; bottom: 506px;">Contratti stipulati  dal</asp:label>
                <asp:label id="Label3" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 50px; position: absolute; top: 142px">Appuntamenti dal</asp:label>
                <asp:label id="Label18" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                style="z-index: 106; left: 49px; position: absolute; top: 169px; height: 15px; width: 397px;" 
                Font-Italic="True">CRITERI DI ESCLUSIONE:</asp:label>
                <asp:label id="Label7" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 192px; height: 19px; width: 397px;">Contratti chiusi dopo l&#39;invio della notifica dell&#39;appuntamento</asp:label>
                <asp:label id="Label8" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 215px; height: 16px; width: 397px;">Contratti già Diffidati</asp:label>
                <asp:label id="Label9" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 239px; height: 14px; width: 397px;">Tutte le sospese in agenda</asp:label>
                <asp:label id="Label11" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 264px; height: 14px; width: 397px;">Sospese per altro motivo</asp:label>
                <asp:label id="Label12" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 288px; height: 14px; width: 397px;">Sospesa per Fissato Nuovo Appuntamento</asp:label>
                <asp:label id="Label13" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 312px; height: 14px; width: 397px;">Sospesa per In Altro Procedimento</asp:label>
                <asp:label id="Label14" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 336px; height: 14px; width: 397px;">Sospesa Invio per Posta</asp:label>
                <asp:label id="Label15" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 359px; height: 14px; width: 397px;">Sospesa per Invio Tramite Sindacati</asp:label>
                <asp:label id="Label16" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 384px; height: 14px; width: 397px;">Sospesa per sloggio</asp:label>
                <asp:label id="Label17" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                style="z-index: 106; left: 50px; position: absolute; top: 407px; height: 14px; width: 397px;">Sospesa per Visita Domiciliare</asp:label>
									<asp:label id="Label10" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 50px; position: absolute; top: 65px">Bando</asp:label>
									<asp:DropDownList id="cmbBando" tabIndex="1" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                
                
                
                style="border: 1px solid black; z-index: 111; left: 173px; position: absolute; top: 62px" 
                Enabled="False"></asp:DropDownList>
                <asp:DropDownList id="cmbFiliale" tabIndex="2" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                
                
                
                style="border: 1px solid black; z-index: 111; left: 173px; position: absolute; top: 86px"></asp:DropDownList>
		</form>
	</body>
</html>
