<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaAbbinamento.aspx.vb" Inherits="ASS_RicercaAbbinamento" %>

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
    <title>Ricerca Domanda</title>
</head>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="txtCognome">
    <div>
        &nbsp;</div>
        <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 112; left: 356px; position: absolute;
            top: 102px" />
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 538px; position: absolute; top: 298px" 
        TabIndex="7" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 101; left: 404px; position: absolute; top: 298px; height: 20px;" 
        TabIndex="6" ToolTip="Avvia Ricerca" />
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Domande Idonee per Abbinamento</strong></span><br />
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
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" style="z-index: 102; left: 89px; position: absolute; top: 93px">Cognome</asp:Label>
                                <asp:TextBox ID="txtCognome" runat="server" TabIndex="1" style="z-index: 103; left: 170px; position: absolute; top: 90px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" style="z-index: 104; left: 90px; position: absolute; top: 121px">Nome</asp:Label>
                                <asp:TextBox ID="txtNome" runat="server" TabIndex="2" style="z-index: 105; left: 170px; position: absolute; top: 118px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" style="z-index: 106; left: 90px; position: absolute; top: 148px">Codice Fiscale</asp:Label>
                                <asp:TextBox ID="txtCF" runat="server" TabIndex="3" style="z-index: 107; left: 170px; position: absolute; top: 145px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" style="z-index: 108; left: 90px; position: absolute; top: 176px">Protocollo</asp:Label>
        <asp:Label ID="Tipo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
            Style="z-index: 108; left: 90px; position: absolute; top: 205px">Tipo</asp:Label>
                                <asp:TextBox ID="txtPG" runat="server" TabIndex="4" style="z-index: 109; left: 170px; position: absolute; top: 173px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
        <asp:DropDownList ID="cmbTipo" runat="server" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 105; left: 171px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 201px" TabIndex="5"
            Width="155px">
            <asp:ListItem Selected="True" Value="1">BANDO ERP</asp:ListItem>
            <asp:ListItem Value="2">BANDO CAMBI</asp:ListItem>
            <asp:ListItem Value="3">CAMBIO EMERGENZA</asp:ListItem>
        </asp:DropDownList>
    </form>
</body>
</html>
