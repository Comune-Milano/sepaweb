<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaUI.aspx.vb" Inherits="CALL_CENTER_RicercaUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ricerca U.I.</title>
    </head>
<body bgcolor="white">
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="btnCerca">
    <div>
        &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 667px; position: absolute; top: 515px" ToolTip="Home" TabIndex="5" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 529px; position: absolute; top: 516px" 
            ToolTip="Avvia Ricerca" TabIndex="4" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMascheraContratti.jpg); WIDTH: 798px;
            position: absolute; top: 0px; z-index: 1;">
            <tr>
                <td style="width: 670px; z-index: 200;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        U.I.</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                        &nbsp;<br />
                    <br />
                    <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 20px; position: absolute; top: 96px">Indirizzo</asp:Label>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 21px; position: absolute; top: 158px">Scala</asp:Label>
                    <asp:DropDownList ID="cmbScala" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 66px; position: absolute; top: 154px; right: 666px;" 
                        TabIndex="6" Width="150px">
                    </asp:DropDownList>
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        
                        Style="border: 1px solid black; z-index: 111; left: 66px; position: absolute; top: 94px; right: 493px;" TabIndex="5"
            Width="400px">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 20px; position: absolute; top: 127px">Civico</asp:Label>
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 66px; position: absolute; top: 184px" 
                        TabIndex="7" Width="150px">
        </asp:DropDownList>
    
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 22px; position: absolute; top: 187px">Interno</asp:Label>
                    <br />
                    <br />
                    <br />
                        <br />
                    <br />
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 10px; position: absolute; top: 479px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    
                    <br />
                    <br />
                    <br />
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 66px; position: absolute; top: 124px; right: 662px;" 
                        TabIndex="6" Width="150px">
        </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="TextBox1" runat="server" />
                    <br />
                    <br />
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
