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
        </style>

	</head>
	
	<body bgcolor="white" onload="document.getElementById('btnCerca2').focus()">

		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca2" 
        defaultfocus="btnCerca2">
        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"         
            Style="z-index: 100; left: 15px; position: absolute; top: 64px; height: 16px; width: 504px;">Indicare il complesso immobiliare a cui fa riferimento la segnalazione</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 278px; position: absolute; top: 109px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
                   <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 3px">
                <tr>
                    <td style="width: 670px; height: 442px;">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                            Complesso Immobiliare</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                                                <img id="Img1" 
                            alt="Aiuto Ricerca per Denominazione Complesso" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                            src="Immagini/Search_24x24.png" style="left: 671px; cursor: pointer; position: absolute;
                            top: 125px" />
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
                        <asp:HiddenField ID="TextBox1" runat="server" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" 
                            Style="left: 12px; position: absolute; top: 475px; width: 488px;" Text="Label"
                            Visible="False"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 10; left: 667px; position: absolute; top: 515px" 
            TabIndex="5" ToolTip="Home" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:DropDownList ID="cmbComplesso" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 15px; position: absolute; top: 128px; width: 650px;" 
                TabIndex="1">
            </asp:DropDownList>
            <asp:ImageButton ID="btnCerca2" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 9; left: 558px; position: absolute; top: 516px" 
            TabIndex="4" ToolTip="Visualizza" />
            <div id="AiutoRicerca" style="z-index: 200; left: 363px; width: 323px; position: absolute; top: 132px; height: 365px; background-color: transparent;
                ">
                <br />
                <div style="width: 60px; height: 136px; background-color: silver">
                    <table style="width: 301px; height: 326px; background-color: silver">
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
                            <td style="vertical-align: top; text-align: left" class="style6">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/text_view.png"
                                    Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" /></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                <asp:Label
                                        ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                                <div style="left: 5px; overflow: auto; width: 263px; top: 87px; height: 243px">
                                    <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Width="240px">
                                    </asp:RadioButtonList></div>
                            </td>
                            <td style="vertical-align: bottom; width: 27px; height: 104px; text-align: left" 
                                valign="bottom">
                                <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Next_24x24.png"
                                    Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" 
                                    onclientclick="myOpacity.toggle();" /></td>
                        </tr>
                    </table>
                &nbsp;&nbsp;
                </div>
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