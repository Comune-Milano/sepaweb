<%@ Page Language="vb" AutoEventWireup="false" Inherits="CM.AssociaDichiarazione" CodeFile="AssociaDichiarazione.aspx.vb" EnableSessionState="ReadOnly" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>AssociaDichiarazione</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
	</head>
	<script type="text/javascript">

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
	<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server" defaultbutton="Button1" 
        defaultfocus="txtPG">
            &nbsp;
            <img src="ImmMaschere/alertComp.gif" style="z-index: 106; left: 25px; position: absolute;
                top: 81px" />
            <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 100; left: 525px; position: absolute; top: 231px" 
                TabIndex="3" ToolTip="Avvia Ricerca" />
            &nbsp;
            <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Associa
                            Dichiarazione alla nuova Domanda</strong></span><br />
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
            <asp:label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 101; left: 182px; position: absolute; top: 134px">N. Dichiarazione</asp:label>
            <asp:textbox id="txtPG" runat="server" 
                style="z-index: 102; left: 294px; position: absolute; top: 132px" 
                BorderStyle="Solid" BorderWidth="1px" TabIndex="1"></asp:textbox><asp:label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False" style="z-index: 103; left: 182px; position: absolute; top: 163px">Codice Fiscale</asp:label>
            <asp:textbox id="txtCF" runat="server" 
                style="z-index: 104; left: 294px; position: absolute; top: 161px" 
                BorderStyle="Solid" BorderWidth="1px" TabIndex="2"></asp:textbox>
			&nbsp;
		</form>
	</body>
</html>
