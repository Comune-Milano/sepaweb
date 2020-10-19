<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DirettaCopmlessi.aspx.vb" Inherits="MANUTENZIONI_DirettaEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RICERCA DIRETTA </title>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 50; left: 56px; position: absolute; top: 120px">Codice Complesso</asp:Label>
        <asp:TextBox ID="TxtcodCompl" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 50;
            left: 152px; position: absolute; top: 119px" Font-Names="arial" Font-Size="10pt" Width="241px"></asp:TextBox>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px; z-index: 20;">
            <tr>
                <td style="width: 670px; height: 423px;">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Complessi</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;
                    <img id="Img2" alt="Aggiungi Variazione ISTAT" onclick="document.getElementById('AiutoRicerca').style.visibility='visible';"
                        src="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" style="left: 401px; cursor: pointer; position: absolute;
                        top: 148px" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="left: 10px; position: absolute; top: 282px; z-index: 1;" Text="Label"
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
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 50; left: 509px; position: absolute; top: 239px" ToolTip="Home" TabIndex="4" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 50; left: 376px; position: absolute; top: 240px" ToolTip="Avvia Ricerca" TabIndex="3" />
        &nbsp; &nbsp;
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 50; left: 56px; position: absolute; top: 152px">Indirizzo</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 50; left: 56px; position: absolute; top: 184px">Civico</asp:Label>
        &nbsp; &nbsp;&nbsp;
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 50; left: 152px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 152px" TabIndex="1"
            Width="246px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 50; left: 152px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 184px" TabIndex="2"
            Width="80px">
        </asp:DropDownList>
        &nbsp;&nbsp;
        <div id="AiutoRicerca" 
            style="z-index: 200; left: 194px; vertical-align: middle; width: 332px;
            position: absolute; top: 115px; height: 308px; background-color: transparent; text-align: center">
            <br />
            <strong><span style="font-size: 10pt; font-family: Arial; background-color: transparent;"></span></strong><br />
            <br />
            <div style="width: 180px; height: 68px; background-color: silver;">
                <table style="width: 301px; height: 185px; background-color: silver;">
                    <tr>
                        <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="106px">Descrizione Indirizzo</asp:Label><br />
                            <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="243px"></asp:TextBox><asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                            <div style="left: 5px; overflow: auto; width: 263px; top: 87px; height: 101px">
                                <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Width="240px">
                                </asp:RadioButtonList></div>
                        </td>
                        <td style="vertical-align: top; height: 104px; text-align: left; width: 27px;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /><br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/Next_24x24.png"
                                Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:TextBox ID="TextBox1" runat="server" Style="left: 230px; position: absolute; top: 119px" Width="1px"></asp:TextBox>
    
    </div>
        <script type="text/javascript">
        if (document.getElementById('TextBox1').value!='2') {
        document.getElementById('AiutoRicerca').style.visibility='hidden';
        }
        </script>
    </form>
</body>
</html>

