<%@ Page Language="VB" AutoEventWireup="false" CodeFile="prova.aspx.vb" Inherits="prova" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>

</head>
<script language="javascript" >

    function Simula() {
        document.getElementById('TextBox1').value="12345678901"
        document.getElementById('Button1').focus();
    }

    function Conta() {
        var ss;
        ss = document.getElementById('TextBox1').value;
        if (ss.length == 10) {
            Completo();
        }
    }

    function Completo() {

        document.getElementById('Button1').click();

    }

</script>
<body>
    <form id="form1" runat="server">
    
    


        &nbsp;<asp:TextBox ID="TextBox1" runat="server" 
            onChange="Conta();" style="height: 22px"></asp:TextBox>
        
        <asp:Button ID="Button1" runat="server" Text="Button" />
        
    &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Clicca per Simulare Pistola" 
            onclick="Simula()" BorderStyle="Outset" 
            style="font-family: 'Times New Roman', Times, serif; font-size: x-small"></asp:Label>
        
    <table style="width:100%;">
        <tr>
            <td class="style1" align="left">
                bnnv</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<asp:UpdatePanel ID="Pannello" runat="server">
<ContentTemplate>
  <asp:Label ID="lblTitolo" runat="server" />
  

  <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick"/>
  

    <asp:HiddenField ID="vis" runat="server" Value="0" />
  

</ContentTemplate>
</asp:UpdatePanel> 
        
    <p>
        &nbsp;</p>
   
   <script type="text/javascript">
       function Chiudi() {
           if (document.getElementById('Messaggio')) {
               document.getElementById('Messaggio').style.visibility = 'hidden';
               document.getElementById("vis").value = '0';
           }
       }
   </script>
        
    </form>
    <div style="border: 1px solid #CC0000">
    </div>
    </body>
    

</html>
