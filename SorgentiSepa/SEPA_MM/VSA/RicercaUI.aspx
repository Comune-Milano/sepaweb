<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaUI.aspx.vb" Inherits="VSA_RicercaUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
    <title>Ricerca U.I.</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="btnCerca">
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 160px">Edifcio</asp:Label>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 160px" TabIndex="2"
            Width="488px">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 120px">Complesso</asp:Label>
        <asp:DropDownList ID="DrLComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 120px" TabIndex="1"
            Width="488px">
        </asp:DropDownList>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 10px; position: absolute; top: 282px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
    <div>
        &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 538px; position: absolute; top: 304px" ToolTip="Home" TabIndex="5" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            
            Style="z-index: 111; left: 406px; position: absolute; top: 304px; height: 20px; width: 130px;" 
            ToolTip="Avvia Ricerca" TabIndex="4" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMaschere.jpg); WIDTH: 674px;
            position: absolute; top: 0px; z-index: 1;">
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
                        <img id="Img1" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                            src="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" style="left: 588px; cursor: pointer; position: absolute;
                            top: 154px" 
                    <br />
                    <br />
                    <br />
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 41px; position: absolute; top: 197px">Indirizzo</asp:Label>
        <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 95px; position: absolute; top: 195px; right: 333px;" TabIndex="5"
            Width="246px">
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 344px; position: absolute; top: 197px">Civico</asp:Label>
        <asp:DropDownList ID="cmbInterno" runat="server" BackColor="White" Font-Names="ARIAL"
            Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 504px; position: absolute; top: 195px" 
                        TabIndex="7" Width="80px">
        </asp:DropDownList>
    
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 465px; position: absolute; top: 198px">Interno</asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
        <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
                        Style="border: 1px solid black; z-index: 111; left: 373px; position: absolute; top: 195px; width: 84px;" 
                        TabIndex="6">
        </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="TextBox1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <div id="AiutoRicerca" style="z-index: 200; left: 280px; width: 302px; position: absolute; top: 158px; height: 225px; background-color: transparent;
            ">
            <br />
            <div style="width: 180px; height: 68px; background-color: silver; vertical-align: top; text-align: left;">
                <table style="width: 301px; background-color: silver" cellpadding="1" cellspacing="1">
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 20px; text-align: left">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="112px">Denominazione Edificio</asp:Label></td>
                        <td style="vertical-align: baseline; width: 27px; height: 20px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 20px; text-align: left">
                            <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="233px"></asp:TextBox></td>
                        <td style="vertical-align: baseline; width: 27px; height: 20px; text-align: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 104px; text-align: left">
                            <asp:Label
                                    ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                            <div style="left: 5px; overflow: auto; width: 256px; top: 87px; height: 109px">
                                <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Width="233px">
                                </asp:RadioButtonList></div>
                        </td>
                        <td style="vertical-align: baseline; width: 27px; height: 104px; text-align: left">
                            <br />
                            <br />
                            <br />
                            <br />
                            
                            <br />
                            <br />
                            <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                                Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" 
                                onclientclick="myOpacity.toggle();" /></td>
                    </tr>
                </table>
            </div>
        </div>
    
    </div>
            <script type="text/javascript">
                myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
                //myOpacity.hide();
                if (document.getElementById('TextBox1').value != '2') {
                    myOpacity.hide(); ;
                }
            </script>
    </form>
</body>
</html>
