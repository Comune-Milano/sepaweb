<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaComplessi.aspx.vb" Inherits="CENSIMENTO_Ricerca" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
		<title>Ricerca Complessi</title>
	    <style type="text/css">
            .style3
            {
                width: 352px;
                height: 10px;
            }
            .style4
            {
                width: 27px;
                height: 10px;
            }
            .style5
            {
                width: 352px;
                height: 18px;
            }
            .style6
            {
                width: 27px;
                height: 18px;
            }
            #Form1
            {
                height: 215px;
                width: 766px;
            }
        </style>

	</head>
	
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">

		<form id="Form1" method="post" runat="server">
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                
            Style="z-index: 100; left: 256px; position: absolute; top: 120px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
           
            &nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 10; left: 533px; position: absolute; top: 304px" TabIndex="5" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 10; left: 270px; position: absolute; top: 304px" TabIndex="-1" Visible="False" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 142px">Complesso</asp:Label>
            <asp:DropDownList ID="cmbComplesso" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 96px; position: absolute; top: 138px; width: 484px;" 
                TabIndex="1">
            </asp:DropDownList>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 196px" 
            Visible="False">Indirizzo</asp:Label>
            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 220px" 
            Visible="False">Civico</asp:Label>
            <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
            Style="border: 1px solid black; z-index: 10; left: 96px; position: absolute; top: 194px" TabIndex="2"
                Width="408px" Visible="False">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
            Style="border: 1px solid black; z-index: 10; left: 96px; position: absolute; top: 220px" TabIndex="3"
                Width="80px" Visible="False">
            </asp:DropDownList>
            <asp:ImageButton ID="btnCerca2" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 10; left: 404px; position: absolute; top: 304px" TabIndex="4" ToolTip="Visualizza" />
            <div id="AiutoRicerca" style="z-index: 200; left: 267px; width: 323px; position: absolute; top: 132px; height: 216px; background-color: transparent;
                ">
                <br />
                <div style="width: 60px; height: 136px; background-color: silver">
                    <table style="width: 301px; height: 185px; background-color: silver">
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style3">
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="190px">Denominazione Complesso Immobiliare</asp:Label>
                            </td>
                            <td style="vertical-align: baseline; text-align: left" class="style4">
                                </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style5">
                                <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                    ToolTip="Approssimare la ricerca per questo indirizzo" Width="230px"></asp:TextBox>
                            </td>
                            <td style="vertical-align: baseline; text-align: left" class="style6">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                    Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                <asp:Label
                                        ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
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
                                    Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" 
                                    onclientclick="myOpacity.toggle();" /></td>
                        </tr>
                    </table>
                &nbsp;&nbsp;
                </div>
            </div>
            
            <table style="position: absolute;  z-index: 1; top: 18px; left: 8px;">
                <tr>
                    <td>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Ricerca
                            Complessi Immobiliari</strong></span><br />

                                                <img id="Img1" 
                            alt="Aiuto Ricerca per Denominazione Complesso" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                            src="IMMCENSIMENTO/Search_24x24.png" style="left: 573px; cursor: pointer; position: absolute;
                            top: 114px" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
            <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 32px; position: absolute; top: 87px; right: 214px;" 
                            Width="40px">Zona</asp:Label>
                        <br />
            <asp:DropDownList ID="cmbZona" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                            
                            Style="border: 1px solid black; z-index: 10; left: 88px; position: absolute; top: 83px" TabIndex="2"
                Width="80px">
            </asp:DropDownList>
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="TextBox1" runat="server" />
                        <br />
                        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="left: 10px; position: absolute; top: 260px" Text="Label"
                            Visible="False" Width="624px"></asp:Label>
                        <br />
                    </td>
                </tr>
            </table>
            
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