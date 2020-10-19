<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaEdifici.aspx.vb" Inherits="CENSIMENTO_RicercaEdifici" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%--<script language="javascript">
var Uscita;
Uscita=0;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
      alert('Usare il tasto <Avvia Ricerca>');
      history.go(0);
      event.keyCode=0;
      }  
}

</script>--%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

	<head>
		<title>Ricerca Edifici</title>
	    <style type="text/css">
            .style1
            {
                width: 352px;
                height: 18px;
            }
            .style2
            {
                width: 27px;
                height: 18px;
            }
            .style3
            {
                width: 352px;
                height: 24px;
            }
            .style4
            {
                width: 27px;
                height: 24px;
            }
        </style>
	</head>
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">

		<form id="Form1" method="post" runat="server" defaultbutton="btnCerca" 
        defaultfocus="btnCerca">
            &nbsp; &nbsp;&nbsp;&nbsp;
            <table 
                
                style="position: absolute; top: 0px; background-color: transparent; z-index: 1; left: -1px;">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                            Edifici</strong></span><br />
                        <br />
                        <br />
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                            
                            Style="z-index: 100; left: 256px; position: absolute; top: 106px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
            <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
                            Style="z-index: 100; left: 44px; position: absolute; top: 91px; right: 586px; width: 46px;">Zona</asp:Label>
            <asp:DropDownList ID="cmbZona" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                            
                            
                            Style="border: 1px solid black; z-index: 10; left: 97px; position: absolute; top: 87px" TabIndex="2"
                Width="80px">
            </asp:DropDownList>
                        <br />
                        <br />
                        <br />
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                            
                            Style="z-index: 100; left: 256px; position: absolute; top: 171px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
                        <br />
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 100; left: 40px; position: absolute; top: 157px; right: 582px;">Ascensore</asp:Label>
                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 185px; position: absolute; top: 157px">Condominio</asp:Label>
                        <asp:DropDownList ID="cmbCondominio" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                            Style="border: 1px solid black; z-index: 111; left: 245px; position: absolute; top: 153px" TabIndex="4"
                Width="80px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbAscensore" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                            Style="border: 1px solid black; z-index: 111; left: 96px; position: absolute; top: 153px" TabIndex="4"
                Width="80px">
                        </asp:DropDownList>
                        <br />
                        &nbsp;
                        <img id="Img1" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
                            src="IMMCENSIMENTO/Search_24x24.png" style="left: 605px; cursor: pointer; position: absolute;
                            top: 187px" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="left: 10px; position: absolute; top: 282px" Text="Label"
                            Visible="False" Width="624px"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="TextBox1" runat="server" />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 10; left: 538px; position: absolute; top: 304px" TabIndex="8" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 10; left: 407px; position: absolute; top: 304px" TabIndex="7" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 192px">Edificio</asp:Label>
            <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" Style="border: 1px solid black; z-index: 10; left: 96px; position: absolute; top: 188px" 
                TabIndex="2" Width="491px" AutoPostBack="True">
            </asp:DropDownList>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 235px" 
                Visible="False">Indirizzo</asp:Label>
            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 262px" 
                Visible="False">Civico</asp:Label>
            <asp:DropDownList ID="cmbIndirizzo" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                Style="border: 1px solid black; z-index: 1; left: 96px; position: absolute; top: 231px" TabIndex="3"
                Width="491px" Visible="False">
            </asp:DropDownList>
            <asp:DropDownList ID="cmbCivico" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 96px; position: absolute; top: 258px" TabIndex="4"
                Width="80px" Visible="False">
            </asp:DropDownList>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 40px; position: absolute; top: 127px">Complesso</asp:Label>
            <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" Height="20px" Style="border: 1px solid black; z-index: 10; left: 96px; position: absolute; top: 123px"
                Width="491px">
            </asp:DropDownList>
            <div id="AiutoRicerca" style="z-index: 200; left: 287px; width: 306px; position: absolute; top: 169px; height: 227px; background-color: transparent;
                ">
                <br />
                <div style="width: 180px; height: 68px; background-color: silver">
                    <table style="width: 301px; height: 185px; background-color: silver">
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style1">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">Denominazione Edificio</asp:Label>
                            </td>
                            <td style="vertical-align: baseline; text-align: left" class="style2">
                                </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style3">
                                <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                    ToolTip="Approssimare la ricerca per questo indirizzo" Width="243px"></asp:TextBox>
                            </td>
                            <td style="vertical-align: baseline; text-align: left" class="style4">
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
                </div>
            </div>
                                    <script type="text/javascript">

                                        myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
                                        //myOpacity.hide();
                                        
       if (document.getElementById('TextBox1').value!='2') {
                                             myOpacity.hide();;
       }
        </script>
		    <p>
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 10; left: 407px; position: absolute; top: 304px" TabIndex="7" 
                    ToolTip="Avvia Ricerca" Visible="False" />
            </p>
		</form>
	</body>
</html>