<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScegliXML.aspx.vb" Inherits="ANAUT_ScegliXML" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Scelta XML</title>
</head>
<script type="text/javascript">
function Attendi() {
    //var win=null;
    //LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
    //TopPosition=(screen.height) ? (screen.height-150)/2 :0;
    //LeftPosition=LeftPosition;
    //TopPosition=TopPosition;
    //parent.funzioni.aa=window.open('../InvioXML.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }

function AzzeraCampi() {
document.getElementById('txtxml').value='';
document.getElementById('txtXsd').value='';
}
</script>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Invio
                        File XML</strong></span><br />
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
        <asp:Button ID="Button1" runat="server" Font-Names="arial"
            Font-Size="9pt" Style="z-index: 100; left: 370px;
            position: absolute; top: 157px" Text="Invia" Width="56px" OnClick="Button1_Click" OnClientClick="AzzeraCampi()" />
        &nbsp;
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
            
            Style="z-index: 101; left: 14px; position: absolute; top: 158px; right: 958px;" 
            Width="342px" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"
            Style="z-index: 102; left: 15px; position: absolute; top: 137px" Text="Invio File"></asp:Label>

	  <p>
          <asp:TextBox ID="txtxml" runat="server" Style="z-index: 103; left: 5px; position: absolute;
              top: 509px; height: 24px;"></asp:TextBox>
          <asp:TextBox ID="txtXsd" runat="server" Style="z-index: 104; left: 148px; position: absolute;
              top: 508px"></asp:TextBox>
          &nbsp;&nbsp;
          <asp:Label ID="Label2" runat="server" Style="z-index: 105; left: 16px; position: absolute;
              top: 94px" Text='Selezionare il file da inviare * tramite il pulsante "Sfoglia", quindi premere il pulsante "Invia"' Font-Bold="True" Font-Names="arial" Font-Size="9pt"></asp:Label>
          <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
              Style="z-index: 106; left: 15px; position: absolute; top: 185px" Text="* Il file deve avere dimensioni inferiori a 10 Mb."></asp:Label>
          &nbsp;&nbsp;
          <asp:TextBox ID="TextBox1" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
              Font-Size="8pt" ForeColor="Black" Height="103px" Rows="10" Style="z-index: 107;
              left: 12px;
              position: absolute; top: 211px; background-color: #f2f5f1;"
              TextMode="MultiLine" Visible="False" Width="513px"></asp:TextBox>
          &nbsp;&nbsp;
          <asp:Button ID="btnHome" runat="server" Font-Names="arial" Font-Size="9pt" Style="z-index: 110;
              left: 478px; position: absolute; top: 157px" Text="Home" OnClientClick="AzzeraCampi()" />
      </p>
    </div>
    </form>
</body>
</html>
