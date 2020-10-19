<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaVerifiche.aspx.vb" Inherits="RicercaVerifiche" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>


<head id="Head1" runat="server">
    <title>RICERCA</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricerca Verifiche</span></strong><br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 320px">
                    <asp:Label ID="lblOrdinamento" runat="server" Font-Bold="False" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 280px; position: absolute;
                        top: 144px" Width="72px">Ordina per:</asp:Label>
                    <asp:RadioButtonList ID="RBList1" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" Style="left: 360px; position: absolute; top: 144px">
                        <asp:ListItem Selected="True">COMPLESSO</asp:ListItem>
                        <asp:ListItem>EDIFICIO</asp:ListItem>
                        <asp:ListItem>TIPO IMPIANTO</asp:ListItem>
                        <asp:ListItem>DATA SCADENZA</asp:ListItem>
                    </asp:RadioButtonList>
        <asp:Label ID="lblTipoImpianto" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 48px; position: absolute; top: 96px"
            Width="72px">Tipo Impianto</asp:Label>
        <asp:DropDownList ID="cmbTipoImpianto" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 96px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
        <asp:Label ID="LblComplesso" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; position: absolute; top: 32px">Complesso</asp:Label>
        <asp:Label ID="LblEdificio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 48px; position: absolute; top: 64px">Edificio</asp:Label>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 32px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 128px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 64px" TabIndex="5"
            Width="536px">
        </asp:DropDownList>
                        <asp:Label ID="lblVerifica" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 48px; position: absolute; top: 144px"
                            Width="72px">Verifiche:</asp:Label>
                        <asp:RadioButtonList ID="RBverifiche" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" Style="left: 128px; position: absolute; top: 144px">
                            <asp:ListItem Selected="True">SCADUTE</asp:ListItem>
                            <asp:ListItem>IN SCADENZA</asp:ListItem>
                            <asp:ListItem>NON SCADUTE</asp:ListItem>
                        </asp:RadioButtonList>
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
                    <br />
                    &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 665px; position: absolute; top: 424px" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 517px; position: absolute; top: 424px" 
                        ToolTip="Avvia Ricerca" OnClick="btnCerca_Click" />
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
