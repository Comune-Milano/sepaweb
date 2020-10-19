<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaCodContr.aspx.vb" Inherits="PED_RicercaOccupante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ricerca U.i. per Occipante</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="btnCerca">
    <div>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 121px">Cognome</asp:Label>
        <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 102;
            left: 117px; position: absolute; top: 118px" Font-Names="arial" Font-Size="10pt" Width="173px" TabIndex="1"></asp:TextBox>
        &nbsp;&nbsp;
        <table style="left: 0px;
            position: absolute; top: 0px; width: 794px;">
            <tr>
                <td>
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
                        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="left: 10px; position: absolute; top: 224px" Text="Label"
                            Visible="False" Width="624px"></asp:Label>
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
            Style="z-index: 106; left: 538px; position: absolute; top: 309px" TabIndex="7" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 404px; position: absolute; top: 309px" TabIndex="6" ToolTip="Avvia Ricerca" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 303px; position: absolute; top: 121px">Nome</asp:Label>
        <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 345px; position: absolute; top: 118px"
            TabIndex="2" Width="173px"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 147px">Cod. Fiscale</asp:Label>
        <asp:TextBox ID="txtCF" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 117px; position: absolute; top: 144px"
            TabIndex="3" Width="173px"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 173px">Rag. Sociale</asp:Label>
        <asp:TextBox ID="txtRS" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 117px; position: absolute; top: 170px"
            TabIndex="4" Width="173px"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 303px; position: absolute; top: 173px">P.Iva</asp:Label>
        <asp:TextBox ID="txtIva" runat="server" BorderStyle="Solid" BorderWidth="1px" Font-Names="arial"
            Font-Size="10pt" Style="z-index: 102; left: 345px; position: absolute; top: 170px"
            TabIndex="5" Width="173px"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>

