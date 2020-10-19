<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Simula39278.aspx.vb" Inherits="ANAUT_Simula39278" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Simulazione 392/78</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Simulazione AU RU 392/78</strong></span><br />
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
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
        Style="z-index: 100; left: 525px; position: absolute; top: 507px" TabIndex="11"
        ToolTip="Home" />
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 446px; position: absolute; top: 507px" TabIndex="11"
        ToolTip="Home" />
    <asp:Label ID="lblBando" runat="server" Font-Size="10pt" Font-Names="Arial" Font-Bold="True"
        
        Style="z-index: 102; left: 15px; position: absolute; top: 77px; width: 323px;">Seleziona Anagrafe Utenza di riferimento</asp:Label>
        <asp:DropDownList ID="cmbBando" TabIndex="1" runat="server" Height="35px" Style="border: 1px solid black;
                z-index: 111; left: 16px; position: absolute; top: 113px" 
        Width="250px" AutoPostBack="True"
                Font-Names="arial" Font-Size="12pt">
            </asp:DropDownList>
    </form>
</body>
</html>
