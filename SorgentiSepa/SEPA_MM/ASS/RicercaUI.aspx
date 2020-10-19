<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaUI.aspx.vb" Inherits="ASS_RicercaUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
		<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ricerca U.i.</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" defaultbutton="btnCerca" defaultfocus="txtUI">
    <div>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 121px">Codice U.i.</asp:Label>
        <asp:TextBox ID="txtUI" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 113px; position: absolute; top: 118px" Font-Names="arial" Font-Size="10pt" Width="241px"></asp:TextBox>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        U.I.</strong></span><br />
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
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 587px; position: absolute; top: 302px" 
            ToolTip="Home" TabIndex="9" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 456px; position: absolute; top: 302px; height: 20px;" 
            ToolTip="Avvia Ricerca" TabIndex="8" />
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 160px">Foglio</asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 177px; position: absolute; top: 160px">Particella</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 289px; position: absolute; top: 160px">Sub</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 201px">Indirizzo</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 373px; position: absolute; top: 201px">Civico</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 505px; position: absolute; top: 201px">Interno</asp:Label>
        <asp:TextBox ID="txtFoglio" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 113px; position: absolute;
            top: 158px; right: 948px;" Width="40px" TabIndex="1"></asp:TextBox>
        <asp:TextBox ID="txtParticella" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 226px; position: absolute;
            top: 158px" Width="40px" TabIndex="2"></asp:TextBox>
        <asp:TextBox ID="txtSub" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 315px; position: absolute; top: 158px"
            Width="40px" TabIndex="3"></asp:TextBox>
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 113px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 198px" TabIndex="4"
            Width="246px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 413px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 198px" TabIndex="5"
            Width="80px">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 239px">Tipo</asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 114px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 236px" TabIndex="7"
            Width="244px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
            z-index: 111; left: 548px; border-left: black 1px solid; border-bottom: black 1px solid;
            position: absolute; top: 198px" TabIndex="6" Width="80px">
        </asp:DropDownList>
    
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
            Style="left: 14px; position: absolute; top: 267px; width: 644px; right: 224px;" Text="Label"
                Visible="False"></asp:Label>
    
    </div>
    </form>
    
    
</body>
</html>

