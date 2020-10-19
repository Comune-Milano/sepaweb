<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaPreferenze.aspx.vb" Inherits="ASS_SceltaTipoInvito" %>

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
    <title>Preferenze Utente</title>
</head>
<script type="text/javascript">
function Attendi() {
    //var win=null;
    //LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
    //TopPosition=(screen.height) ? (screen.height-150)/2 :0;
    //LeftPosition=LeftPosition;
    //TopPosition=TopPosition;
    //parent.funzioni.aa=window.open('../loading.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }
</script>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" defaultfocus="txtPG">
    <div>
        &nbsp;</div>
        <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 108; left: 346px; position: absolute;
            top: 79px" />
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 537px; position: absolute; top: 247px" 
        TabIndex="4" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 101; left: 400px; position: absolute; top: 247px" 
        TabIndex="3" ToolTip="Avvia Ricerca" />
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Preferenze
                        Utente</strong></span><br />
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
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                                    Style="z-index: 102; left: 106px; position: absolute; top: 93px" Width="50px">Protocollo</asp:Label>
                                <asp:TextBox ID="txtPG" runat="server" Style="z-index: 103; left: 165px; position: absolute;
                                    top: 91px" TabIndex="1" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
                                    Style="z-index: 104; left: 106px; position: absolute; top: 127px" Width="50px">Tipologia</asp:Label>
                                <asp:DropDownList ID="cmbStato" runat="server"
                                    
        Style="z-index: 105; left: 165px; position: absolute; top: 123px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;" 
        TabIndex="2" Width="155px">
                                </asp:DropDownList>
    </form>
</body>
</html>
