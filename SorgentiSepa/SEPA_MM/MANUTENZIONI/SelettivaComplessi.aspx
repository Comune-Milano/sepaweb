<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelettivaComplessi.aspx.vb" Inherits="MANUTENZIONI_SelettivaEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
    <title>RICERCA</title>
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 21px; position: absolute; top: 445px" 
            TabIndex="-1" Visible="False">Gestore</asp:Label>
        <img id="Img1" alt="Aiuto Ricerca per Denominazione Complesso" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
            src="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" style="z-index: 50; left: 593px;
            cursor: pointer; position: absolute; top: 126px" />
        <div id="AiutoRicerca" style="z-index: 200; left: 296px; width: 323px; position: absolute;
            top: 130px; height: 216px; background-color: transparent">
            <br />
            <div style="width: 60px; height: 136px; background-color: silver">
                <table style="width: 301px; height: 185px; background-color: silver">
                    <tr>
                        <td class="style3" style="vertical-align: top; text-align: left">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="190px">Denominazione Complesso Immobiliare</asp:Label>
                        </td>
                        <td class="style4" style="vertical-align: baseline; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" style="vertical-align: top; text-align: left">
                            <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="230px"></asp:TextBox>
                        </td>
                        <td class="style6" style="vertical-align: baseline; text-align: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                            <asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                            <div style="left: 5px; overflow: auto; width: 263px; top: 87px; height: 101px">
                                <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Width="240px">
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
                                OnClientClick="myOpacity.toggle();" Style="z-index: 111; left: 268px; top: 190px"
                                ToolTip="Conferma" /></td>
                    </tr>
                </table>
                &nbsp;&nbsp;
            </div>
        </div>
        &nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 18px; position: absolute; top: 479px" 
            TabIndex="-1" Visible="False">Provenienza</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 38px; position: absolute; top: 128px" 
            TabIndex="-1">Complesso</asp:Label>
        &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
        <table style="left: -3px; background-image: url('../NuoveImm/SfondoMaschere.jpg'); width: 674px;
            position: absolute; top: -4px; z-index: 1;">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Selettiva </strong></span>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LBLTIPOL" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 44px; position: absolute; top: 166px" 
                        TabIndex="-1">Tipologia</asp:Label>
                    <br />
                    <br />
                    <asp:DropDownList ID="cmbTipoUtenze" runat="server" Style="z-index: 10; left: 99px;
                        position: absolute; top: 165px" Width="410px" Font-Names="Arial" 
                        Font-Size="10pt">
                        </asp:DropDownList>
                    <br />
                    <asp:Label ID="LBLFORN" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 44px; position: absolute; top: 199px; right: 583px;" 
                        TabIndex="-1">Fornitore</asp:Label>
                    <asp:DropDownList ID="cmbFornitore" runat="server" Style="z-index: 10; left: 99px;
                        position: absolute; top: 197px" Width="410px" Font-Names="Arial" 
                        Font-Size="10pt">
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="LBLCONT" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 43px; position: absolute; top: 230px" TabIndex="-1">Contatore</asp:Label>
                    <asp:TextBox ID="txtContatore" runat="server" Font-Names="Arial" Font-Size="10pt"
                        MaxLength="49" Style="z-index: 10; left: 99px; position: absolute; top: 230px"
                        Width="280px"></asp:TextBox>
                    <asp:TextBox ID="txtContratto" runat="server" Font-Names="Arial" Font-Size="10pt"
                        MaxLength="49" Style="z-index: 10; left: 99px; position: absolute; top: 263px"
                        Width="280px"></asp:TextBox>
                    <asp:Label ID="LBLCONTR" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 43px; position: absolute; top: 263px" TabIndex="-1">Contratto</asp:Label>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtContatore"
                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Height="1px"
                        Style="z-index: 10; left: 386px; position: absolute; top: 233px" ToolTip="E' possibile inserire solo numeri"
                        ValidationExpression="\d+" Width="1px"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtContratto"
                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Height="1px"
                        Style="z-index: 10; left: 386px; position: absolute; top: 265px" ToolTip="E' possibile inserire solo numeri"
                        ValidationExpression="\d+" Width="1px"></asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="left: 12px; position: absolute; top: 342px" Text="Label"
                        Visible="False" Width="624px"></asp:Label>
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
            Style="z-index: 106; left: 537px; position: absolute; top: 304px" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 405px; position: absolute; top: 304px" ToolTip="Avvia Ricerca" Enabled="False" Visible="False" /><asp:ImageButton ID="BtnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 111; left: 405px; position: absolute; top: 304px" ToolTip="Avvia Ricerca" />
        <asp:DropDownList ID="cmbGestore" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            Style="border: 1px solid black; z-index: 111; left: 68px; position: absolute; top: 446px; width: 48px;" 
            Visible="False">
        </asp:DropDownList>
        &nbsp;
        <asp:DropDownList ID="cmbProvenienza" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" 
            Style="border: 1px solid black; z-index: 111; left: 84px; position: absolute; top: 474px; width: 49px;" 
            TabIndex="1" Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 128px" TabIndex="2"
            Width="492px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" ForeColor="White" Style="left: 25px; position: absolute;
            top: 462px" TabIndex="-1">1</asp:TextBox>
    
    </div>
    </form>
    			                    <script type="text/javascript">

                                        myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
                                        //myOpacity.hide();
                                        
       if (document.getElementById('TextBox1').value!='2') {
                                             myOpacity.hide();;
       }
                    </script>
</body>
</html>