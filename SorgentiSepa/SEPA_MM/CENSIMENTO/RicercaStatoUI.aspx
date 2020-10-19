<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaStatoUI.aspx.vb" Inherits="PED_RicercaStatoUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Stato U.I.</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 231px">Tipologia</asp:Label>
        &nbsp;<span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Stato 
        Occupazione U.I.</strong></span>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 120px">Complesso</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 160px">Edificio</asp:Label>
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; </strong></span><br />
                    <br />
                    <br />
                    <br />
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                        Style="z-index: 100; left: 256px; position: absolute; top: 144px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                        Style="z-index: 100; left: 256px; position: absolute; top: 103px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        
                        Style="border: 1px solid black; z-index: 111; left: 96px; position: absolute; top: 195px; right: 333px;" TabIndex="3"
            Width="239px">
        </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="left: 10px; position: absolute; top: 282px" Text="Label"
                        Visible="False" Width="624px" TabIndex="50"></asp:Label>
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
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 528px; position: absolute; top: 322px" 
            ToolTip="Home" TabIndex="9" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 396px; position: absolute; top: 322px" 
            ToolTip="Avvia Ricerca" TabIndex="8" />
        &nbsp;
        <asp:DropDownList ID="cmbTipologia" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            Style="border: 1px solid black; z-index: 111; left: 96px; position: absolute; top: 228px" TabIndex="6"
            Width="239px">
        </asp:DropDownList>
        &nbsp;
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 120px" TabIndex="1"
            Width="550px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 160px" TabIndex="2"
            Width="550px" AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;&nbsp;
    
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 591px; position: absolute; top: 195px" 
                        TabIndex="6" Width="55px">
        </asp:DropDownList>
    
                    <asp:DropDownList ID="cmbScala" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 494px; position: absolute; top: 195px; right: 958px;" 
                        TabIndex="5" Width="66px">
                    </asp:DropDownList>
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 370px; position: absolute; top: 195px; right: 1058px;" 
                        TabIndex="4" Width="90px">
        </asp:DropDownList>
                    <asp:Label ID="Label6" runat="server" Font-Bold="False" 
            Font-Names="Arial" Font-Size="8pt"
                        
            Style="z-index: 100; left: 463px; position: absolute; top: 197px">Scala</asp:Label>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 340px; position: absolute; top: 197px">Civico</asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 41px; position: absolute; top: 197px">Indirizzo</asp:Label>
    
    </div>
    <p>
    
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 570px; position: absolute; top: 198px">Int.</asp:Label>
                    </p>
    </form>
</body>
</html>
