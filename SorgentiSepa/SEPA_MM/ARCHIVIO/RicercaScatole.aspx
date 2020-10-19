<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaScatole.aspx.vb" Inherits="ARCHIVIO_RicercaScatole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            alert('Usare il tasto <Avvia Ricerca>');
            history.go(0);
            event.keyCode = 0;
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="..\Contratti\prototype.lite.js"></script>
<script type="text/javascript" src="..\Contratti\moo.fx.js"></script>
<script type="text/javascript" src="..\Contratti\moo.fx.pack.js"></script>
<head>
    <title>Ricerca Scatole</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="Form1" method="post" runat="server" defaultbutton="btnCerca" defaultfocus="txtCognome">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                    Scatole</strong></span><br />
                <br />
                <br />
                <br />
                <br />
                &nbsp;<br />
                <br />
                &nbsp;<br />
                &nbsp;<br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 188px" 
                    TabIndex="-1">Scatola*</asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 218px" 
                    TabIndex="-1" Visible="False">Intervallo Scatole Da</asp:Label>
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 244px; position: absolute; top: 218px" 
                    TabIndex="-1" Visible="False">a</asp:Label>
                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 485px; position: absolute; top: 188px; width: 208px; height: 41px;" 
                    TabIndex="-1">* è possibile inserire più numeri di scatole, separati da &quot;,&quot; (virgola)</asp:Label>
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
                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 129px" Width="100px"
        TabIndex="-1">Codice Eustorgio</asp:Label>
        <asp:TextBox ID="txtEustorgio" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 126px;" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 158px" Width="100px"
        TabIndex="-1">Faldone</asp:Label>
        <asp:TextBox ID="txtFaldone" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 155px;" TabIndex="4"></asp:TextBox>
        <asp:TextBox ID="txtScatola" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 185px; width: 305px;" TabIndex="5"></asp:TextBox>
        <asp:TextBox ID="txtScatolaDa" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 215px; width: 65px;" TabIndex="6" 
                    Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtScatolaA" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 262px; position: absolute; top: 215px; width: 65px;" TabIndex="7" 
                    Visible="False"></asp:TextBox>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        
        
        Style="z-index: 100; left: 660px; position: absolute; top: 504px; height: 20px;" TabIndex="9"
        ToolTip="Home" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        
        
        Style="z-index: 101; left: 527px; position: absolute; top: 504px; height: 20px;" TabIndex="8"
        ToolTip="Avvia Ricerca" />
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 49px; position: absolute; top: 101px" Width="100px"
        TabIndex="-1">Codice Utente</asp:Label>
    <asp:TextBox ID="txtCod" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 98px" TabIndex="1" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
        <asp:CheckBox ID="ChIntest" runat="server" Style="left: 332px; position: absolute;
                    top: 98px" Font-Names="ARIAL" Font-Size="9pt" TabIndex="2" 
                    Text="Solo Intestatari" Checked="True" />
    </form>
</body>

</html>


