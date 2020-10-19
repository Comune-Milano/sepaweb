<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioIntestazione1.aspx.vb" Inherits="CambioIntestazione1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;


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
<head id="Head1" runat="server">
    <title>Cambio Intestazione</title>
</head>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnSalva" 
    defaultfocus="cmbNucleo">
    <div>
        &nbsp;
        <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Cambio
                        Intestazione</strong></span><br />
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
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 536px; position: absolute; top: 313px" 
            TabIndex="4" ToolTip="Home" />
        &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 56px; position: absolute; top: 96px">Domanda attualmente intestata a:</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 56px; position: absolute; top: 66px">Protocollo:</asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 222px; position: absolute; top: 96px" Width="429px"></asp:Label>
        <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 109px; position: absolute; top: 66px" Width="429px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 56px; position: absolute; top: 130px">Nuovo Intestatario:</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 196px; position: absolute; top: 130px" ForeColor="#C00000" Visible="False" Width="21px">***</asp:Label>
        <asp:DropDownList ID="cmbNucleo" runat="server" BackColor="White" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 222px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 127px" TabIndex="1"
            Width="427px">
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 108; left: 56px; position: absolute; top: 165px">L'attuale intestatario fa ancora parte del nucleo?</asp:Label>
        <asp:DropDownList ID="cmbElimina" runat="server" BackColor="White" Height="20px"
            Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111;
            left: 298px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute;
            top: 162px" TabIndex="2" Width="68px">
            <asp:ListItem Selected="True" Value="0">NO</asp:ListItem>
            <asp:ListItem Value="1">SI</asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="btnSalva" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_SalvaContinua.png"
            OnClientClick="Conferma();" Style="z-index: 105; left: 397px; position: absolute;
            top: 313px" TabIndex="3" ToolTip="Salva e Continua" />
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 108; left: 83px; position: absolute; top: 243px"
            Text="Attenzione...è ora necessario elaborare la domanda per assegnare un nuovo ISBARC/R"
            Visible="False"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 108; left: 271px; position: absolute; top: 214px"
            Text="OPERAZIONE EFFETTUATA!" Visible="False"></asp:Label>
        <asp:Image ID="Image2" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 114;
            left: 57px; position: absolute; top: 242px" Visible="False" />
    
    </div>
    </form>
</body>
</html>

