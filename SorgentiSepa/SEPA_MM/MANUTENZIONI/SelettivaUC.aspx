<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelettivaUC.aspx.vb" Inherits="MANUTENZIONI_SelettivaUI" %>

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
            Style="z-index: 100; left: 45px; position: absolute; top: 399px" TabIndex="-1" Visible="False">Gestore</asp:Label>
        &nbsp;
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 45px; position: absolute; top: 431px" TabIndex="-1" Visible="False">Provenienza</asp:Label>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 56px; position: absolute; top: 101px" TabIndex="-1">Complesso</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 56px; position: absolute; top: 133px" TabIndex="-1">Edificio</asp:Label>
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px; z-index: 1;">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Selettiva</strong></span><br />
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
        <img id="Img2" alt="Aiuto Ricerca per Denominazione Complesso" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
            src="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" style="z-index: 5; left: 596px;
            cursor: pointer; position: absolute; top: 96px" />
        <img id="Img1" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('TextBox2').value!='1';myOpacity2.toggle();"
            src="../CENSIMENTO/IMMCENSIMENTO/Search_24x24.png" style="z-index: 5; left: 595px;
            cursor: pointer; position: absolute; top: 136px" />
        <div id="AiutoRicerca" style="z-index: 200; left: 214px; vertical-align: middle;
            width: 428px; position: absolute; top: 103px; height: 227px; background-color: transparent;
            text-align: center">
            <br />
            <div style="width: 180px; height: 68px; background-color: silver">
                <table style="width: 301px; height: 185px; background-color: silver">
                    <tr>
                        <td style="vertical-align: top; width: 352px; height: 15px; text-align: left">
                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="190px">Denominazione Complesso Immobiliare</asp:Label></td>
                        <td style="vertical-align: baseline; width: 27px; height: 15px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 352px; height: 20px; text-align: left">
                            <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="243px"></asp:TextBox></td>
                        <td style="vertical-align: baseline; width: 27px; height: 20px; text-align: left">
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
                                Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="AiutoRicercaEd" style="z-index: 200; left: 294px; width: 308px; position: absolute;
            top: 140px; height: 231px; background-color: transparent">
            <br />
            <div style="vertical-align: top; width: 180px; height: 68px; background-color: silver;
                text-align: left">
                <table style="width: 301px; height: 185px; background-color: silver">
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 18px; text-align: left">
                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="112px">Denominazione Edificio</asp:Label></td>
                        <td style="vertical-align: baseline; width: 27px; height: 18px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 20px; text-align: left">
                            <asp:TextBox ID="TextBoxDescIndEd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                ToolTip="Approssimare la ricerca per questo indirizzo" Width="251px"></asp:TextBox></td>
                        <td style="vertical-align: baseline; width: 27px; height: 20px; text-align: left">
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 166px; height: 104px; text-align: left">
                            <asp:Label ID="LblNoresult2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                            <div style="left: 5px; overflow: auto; width: 256px; top: 87px; height: 109px">
                                <asp:RadioButtonList ID="ListEdifici2" runat="server" Font-Names="Arial" Font-Size="8pt"
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
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                                Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" /></td>
                    </tr>
                </table>
            </div>
        </div>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 2; left: 535px; position: absolute; top: 327px" TabIndex="5" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 2; left: 402px; position: absolute; top: 327px" TabIndex="4" />
        <asp:DropDownList ID="cmbGestore" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 117px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 399px"
            Width="283px" Visible="False">
        </asp:DropDownList>
        &nbsp;
        <asp:DropDownList ID="cmbProvenienza" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 117px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 431px" TabIndex="1"
            Width="283px" Visible="False">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 2; left: 115px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 101px" TabIndex="2"
            Width="481px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 2; left: 115px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 133px" TabIndex="3"
            Width="481px">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 56px; position: absolute; top: 165px">Tipologia</asp:Label>
        <asp:DropDownList ID="cmbTipologia" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
            z-index: 2; left: 115px; border-left: black 1px solid; border-bottom: black 1px solid;
            position: absolute; top: 165px" TabIndex="3" Width="412px">
        </asp:DropDownList>
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 100; left: 10px; position: absolute; top: 258px"
            Text="Label" Visible="False" Width="624px"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" ForeColor="White" Style="left: 17px; position: absolute;
            top: 331px" TabIndex="-1" Width="5px"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" ForeColor="White" Style="left: 70px; position: absolute;
            top: 486px" TabIndex="-1" Width="13px"></asp:TextBox>
    
    </div>
    </form>
            <script type="text/javascript">
                myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
                //myOpacity.hide();
                if (document.getElementById('TextBox1').value != '2') {
                    myOpacity.hide(); ;
                }
            </script>
            <script type="text/javascript">
                myOpacity2 = new fx.Opacity('AiutoRicercaEd', { duration: 200 });
                //myOpacity.hide();
                if (document.getElementById('TextBox2').value != '2') {
                    myOpacity2.hide(); ;
                }
            </script>
</body>
</html>
