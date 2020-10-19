<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelettivaEdifici.aspx.vb" Inherits="MANUTENZIONI_SelettivaEdifici" %>

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
            Style="z-index: 100; left: 21px; position: absolute; top: 358px" TabIndex="-1" Visible="False">Gestore</asp:Label>
        &nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 21px; position: absolute; top: 390px" TabIndex="-1" Visible="False">Provenienza</asp:Label>
        &nbsp; &nbsp;
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 160px">EDIFICIO</asp:Label>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 10; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 160px" TabIndex="2"
            Width="491px">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 40px; position: absolute; top: 120px">Complesso</asp:Label>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 10; left: 96px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 120px" TabIndex="1"
            Width="491px">
        </asp:DropDownList>
        &nbsp; &nbsp;
        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px; z-index: 1;">
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
                    <asp:DropDownList ID="cmbFornitore" runat="server" Style="z-index: 10; left: 96px;
                        position: absolute; top: 229px" Width="410px" Font-Names="Arial" 
                        Font-Size="10pt">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <img id="Img1" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                        src="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" style="z-index: 10; left: 595px;
                        cursor: pointer; position: absolute; top: 160px" />
                    &nbsp;<br />
                    <asp:DropDownList ID="cmbTipoUtenze" runat="server" Style="z-index: 10; left: 96px;
                        position: absolute; top: 195px" Width="410px" Font-Names="Arial" 
                        Font-Size="10pt">
                        </asp:DropDownList>
                    <asp:Label ID="LBLTIPOL" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 40px; position: absolute; top: 197px" 
                        TabIndex="-1">Tipologia</asp:Label>
                    <br />
                    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="left: 13px; position: absolute; top: 421px" Text="Label"
                        Visible="False" Width="624px"></asp:Label>
                    <asp:Label ID="LBLFORN" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 41px; position: absolute; top: 232px; right: 583px;" 
                        TabIndex="-1">Fornitore</asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LBLCONT" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 43px; position: absolute; top: 265px" TabIndex="-1">Contatore</asp:Label>
                    <asp:TextBox ID="txtContatore" runat="server" Font-Names="Arial" Font-Size="10pt"
                        MaxLength="49" Style="z-index: 10; left: 96px; position: absolute; top: 265px"
                        Width="280px"></asp:TextBox>
                    <asp:TextBox ID="txtContratto" runat="server" Font-Names="Arial" Font-Size="10pt"
                        MaxLength="49" Style="z-index: 10; left: 96px; position: absolute; top: 299px"
                        Width="280px"></asp:TextBox>
                    <asp:Label ID="LBLCONTR" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 43px; position: absolute; top: 299px" TabIndex="-1">Contratto</asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtContatore"
                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Height="1px"
                        Style="z-index: 10; left: 386px; position: absolute; top: 276px" ToolTip="E' possibile inserire solo numeri"
                        ValidationExpression="\d+" Width="1px"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtContratto"
                        Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Size="12pt" Height="1px"
                        Style="z-index: 10; left: 386px; position: absolute; top: 308px" ToolTip="E' possibile inserire solo numeri"
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
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            style="z-index: 2; left: 551px; position: absolute; top: 340px" ToolTip="Home" TabIndex="5" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            style="z-index: 1; left: 423px; position: absolute; top: 340px" ToolTip="Avvia Ricerca" TabIndex="4" />
        <asp:DropDownList ID="cmbGestore" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; left: 93px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 358px"
            Width="283px" Visible="False">
        </asp:DropDownList>
        &nbsp;
        <asp:DropDownList ID="cmbProvenienza" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; left: 93px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 390px" TabIndex="1"
            Width="283px" Visible="False">
        </asp:DropDownList>
        &nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp; &nbsp;
                    <div id="AiutoRicerca" style="left: 287px; width: 306px; position: absolute; top: 165px;
                        height: 227px; background-color: transparent; z-index: 10;">
                        <br />
                        <div style="z-index: 100; width: 180px; height: 68px; background-color: silver">
                            <table style="width: 301px; height: 185px; background-color: silver">
                                <tr>
                                    <td class="style1" style="vertical-align: top; text-align: left">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">Denominazione Edificio</asp:Label>
                                    </td>
                                    <td class="style2" style="vertical-align: baseline; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3" style="vertical-align: top; text-align: left">
                                        <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                            ToolTip="Approssimare la ricerca per questo indirizzo" Width="243px"></asp:TextBox>
                                    </td>
                                    <td class="style4" style="vertical-align: baseline; text-align: left">
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
                        </div>
                    </div>
        <asp:TextBox ID="TextBox1" runat="server" ForeColor="White" Style="left: 17px; position: absolute;
            top: 479px" TabIndex="-1" Width="8px"></asp:TextBox>
    
    </div>
                                        <script type="text/javascript">

                                        myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
                                        //myOpacity.hide();
                                        
       if (document.getElementById('TextBox1').value!='2') {
                                             myOpacity.hide();;
       }
        </script>
    </form>
</body>
</html>
