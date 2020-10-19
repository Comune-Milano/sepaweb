<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaOfferta.aspx.vb" Inherits="ASS_RicercaOfferta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
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
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ricerca Offerta</title>
</head>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="txtOfferta">
    <div>
        &nbsp;</div>
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 538px; position: absolute; top: 252px" 
        TabIndex="3" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            
        Style="z-index: 101; left: 404px; position: absolute; top: 252px; right: 901px;" 
        TabIndex="2" ToolTip="Avvia Ricerca" />
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Offerta</strong></span><br />
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
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" style="z-index: 102; left: 111px; position: absolute; top: 110px">N° Offerta</asp:Label>
                                <asp:TextBox ID="txtOfferta" runat="server" TabIndex="1" style="z-index: 103; left: 166px; position: absolute; top: 106px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="10pt" ForeColor="#C00000"
            Height="23px" Style="left: 165px; position: absolute; top: 128px" Visible="False"
            Width="211px"></asp:Label>
    </form>
</body>
</html>
