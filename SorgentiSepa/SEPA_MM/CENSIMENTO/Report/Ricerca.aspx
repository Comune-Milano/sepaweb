<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ricerca.aspx.vb" Inherits="CENSIMENTO_Report_Ricerca" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Ricerca U.i.</title>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 121px">Tipologia</asp:Label>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg'); width: 800px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Unità Libere </strong>
                    <asp:Label ID="lblTipo" runat="server" Font-Bold="True"></asp:Label>
                    </span><br />
                    <br />
                    <br />
                    <br />
            <asp:DropDownList ID="cmbFiliale" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" 
                        
                        Style="border: 1px solid black; z-index: 10; left: 107px; position: absolute; top: 148px; " TabIndex="2"
                Width="500px">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbTipo" runat="server" 
                Font-Names="arial" Font-Size="9pt" 
                        
                        Style="border: 1px solid black; z-index: 10; left: 107px; position: absolute; top: 118px;" TabIndex="1"
                Width="500px">
            </asp:DropDownList>
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
                    <asp:HiddenField ID="tipologia" runat="server" />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 538px; position: absolute; top: 309px" 
            TabIndex="4" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 404px; position: absolute; top: 309px" 
            TabIndex="3" ToolTip="Avvia Ricerca" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 52px; position: absolute; top: 152px">Sede Terr.</asp:Label>
    
    </div>
    </form>
</body>
</html>
