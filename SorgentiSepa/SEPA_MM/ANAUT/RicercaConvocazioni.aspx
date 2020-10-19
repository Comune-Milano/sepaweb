<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaConvocazioni.aspx.vb" Inherits="ANAUT_RicercaConvocazioni" %>

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
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Convocazioni</strong></span><br />
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
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton 
                ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 539px; position: absolute; top: 452px" 
                TabIndex="5" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 406px; position: absolute; top: 452px; height: 20px;" 
                TabIndex="4" ToolTip="Avvia Ricerca" />
									<asp:label id="Label10" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 50px; position: absolute; top: 216px; ">Sportello</asp:label>
                <asp:label id="Label1" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 50px; position: absolute; top: 178px">Sede Territoriale</asp:label>
									<asp:label id="Label6" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 50px; position: absolute; top: 104px">Anagrafe Utenza</asp:label>
                
									
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <asp:DropDownList id="cmbBando" tabIndex="1" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                
                
                
                            style="border: 1px solid black; z-index: 111; left: 164px; position: absolute; top: 101px" 
                            AutoPostBack="True"></asp:DropDownList>
                    <asp:label id="Label2" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 50px; position: absolute; top: 141px">Lista Convocazione</asp:label>
                <asp:DropDownList id="cmbLista" tabIndex="2" 
                runat="server" Height="20px"  
                
                
                style="border: 1px solid black; z-index: 111; left: 164px; position: absolute; top: 138px" 
                Width="300px" AutoPostBack="True"></asp:DropDownList>
                        <asp:DropDownList id="cmbFiliale" tabIndex="3" 
                runat="server" Height="20px"  
                
                
                style="border: 1px solid black; z-index: 111; left: 164px; position: absolute; top: 175px" 
                Width="300px" AutoPostBack="True"></asp:DropDownList>
                <asp:DropDownList id="cmbOperatore" tabIndex="4" 
                runat="server" Height="20px" Width="300px" 
                
                
                
                
                
                            
                            
                            
                            
                            
                            
                            style="border: 1px solid black; z-index: 111; left: 164px; position: absolute; top: 212px"></asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
									
			
		</form>

       
	</body>
</html>
