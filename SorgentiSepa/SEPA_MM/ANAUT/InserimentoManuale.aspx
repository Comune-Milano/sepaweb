<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimentoManuale.aspx.vb" Inherits="ANAUT_InserimentoManuale" %>

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
		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="txtCognome">
            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Inserimento AU
                            Inquilino senza Appuntamento</strong></span><br />
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
                        <div style="position: absolute; overflow: auto; width: 633px; height: 202px; top: 227px; left: 16px;">
                            <asp:RadioButtonList ID="rdElenco" runat="server" Font-Names="Arial" 
                                Font-Size="8pt">
                            </asp:RadioButtonList>
                        </div>
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
                            <asp:TextBox ID="txtContratto" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="19" Style="z-index: 113; left: 118px; position: absolute; top: 81px; width: 320px;"
                            TabIndex="1"></asp:TextBox>
            &nbsp;<asp:ImageButton 
                ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 539px; position: absolute; top: 472px" 
                TabIndex="11" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 491px; position: absolute; top: 141px; height: 20px;" 
                TabIndex="10" ToolTip="Avvia Ricerca" />
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                Style="z-index: 101; left: 433px; position: absolute; top: 473px; height: 20px;" 
                TabIndex="10" ToolTip="Procedi" Visible="False" />
                <asp:label id="lblDati" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 16px; position: absolute; top: 185px; " 
                Visible="False" ForeColor="#CC0000"></asp:label>

                <asp:label id="Label1" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 16px; position: absolute; top: 84px">Cod.Contratto</asp:label>
 <asp:label id="Label2" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 110; left: 16px; position: absolute; top: 114px; height: 14px; width: 54px;">Cognome</asp:label>
            <asp:TextBox ID="txtCognome" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="100" Style="z-index: 113; left: 118px; position: absolute; top: 111px; width: 320px;"
                            TabIndex="2"></asp:TextBox>
									<asp:label id="Label3" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 16px; position: absolute; top: 144px">Nome</asp:label>
            <asp:TextBox ID="txtNome" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="100" Style="z-index: 113; left: 118px; position: absolute; top: 141px; width: 320px;"
                            TabIndex="3"></asp:TextBox>
									
			
		</form>

       
	</body>
</html>
