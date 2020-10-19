<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicBolletta.aspx.vb" Inherits="Contabilita_RicBolletta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<script type="text/javascript" >
<!--
var Uscita1;
Uscita1=1;
// -->
</script>


<head id="Head1" runat="server">
    <title>RICERCA</title>
    </head>
<body>
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        L&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricerca Bolletta</span></strong><br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 370px">
									<asp:label id="lblCognome" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="False" 
                style="z-index: 104; left: 50px; position: absolute; top: 96px" 
                TabIndex="-1">NUMERO BOLLETTA</asp:label>
                <asp:label id="Label1" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="False" 
                style="z-index: 104; left: 50px; position: absolute; top: 136px" 
                TabIndex="-1">NUMERO MAV</asp:label>
        <asp:TextBox ID="txtNumBolletta" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 158px; position: absolute; top: 93px; width: 305px;" Font-Names="arial" Font-Size="10pt" 
                                        TabIndex="1"></asp:TextBox>
                                        <asp:TextBox ID="txtNumMav" runat="server" BorderStyle="Solid" 
                                        BorderWidth="1px" Style="z-index: 102;
            left: 158px; position: absolute; top: 130px; width: 305px;" Font-Names="arial" Font-Size="10pt" 
                                        TabIndex="2"></asp:TextBox>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 693px; position: absolute; top: 326px" ToolTip="Home" TabIndex="15" />
                    </div>
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
                    &nbsp;<br />
                    &nbsp;<br />
                    <asp:HiddenField ID="DivVisible" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 366px; height: 13px; width: 442px;"
                        Text="Label" Visible="False"></asp:Label>
                    
                    <br />
                    <br />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 570px; position: absolute; top: 390px; right: 210px;" 
                        ToolTip="Avvia Ricerca" TabIndex="14" />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
    

</body>
</html>
