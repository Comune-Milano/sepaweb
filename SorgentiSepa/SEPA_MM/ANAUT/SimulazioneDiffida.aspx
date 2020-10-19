<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SimulazioneDiffida.aspx.vb" Inherits="ANAUT_SimulazioneDiffida" %>

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
	<body bgcolor="#f2f5f1">
	
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Simulazione Emissione Diffide</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            style="position:absolute; top: 104px; left: 15px;" Font-Names="arial" 
                            Font-Size="10pt" Target="_blank">[HyperLink1]</asp:HyperLink>
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
                Style="z-index: 100; left: 539px; position: absolute; top: 379px" 
                TabIndex="9" ToolTip="Home" />
                <asp:label id="Label3" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 106; left: 16px; position: absolute; top: 71px">E&#39; stato generato il seguente file</asp:label>
		</form>
	</body>
</html>
