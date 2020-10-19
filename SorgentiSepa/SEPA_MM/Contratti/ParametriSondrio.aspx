<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriSondrio.aspx.vb" Inherits="Contratti_ParametriSondrio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
      alert('Usare il tasto <Avvia Ricerca>');
      history.go(0);
      event.keyCode=0;
      }  
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Ricerca Contratti</title>
	    <style type="text/css">
            .style1
            {
                width: 137px;
            }
        </style>
	</head>
	<body bgcolor="#f2f5f1">

		<form id="Form1" method="post" runat="server" defaultbutton="btnSave" 
        defaultfocus="txtLocatore">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametri
                            MAV on Line Banca di Sondrio&nbsp;</strong></span><br />
                        <br />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                        <table width="100%">
                            <tr>
                                <td class="style1">
                                    &nbsp; <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 99px" Width="129px">Indirizzo WEB del Servizio</asp:Label></td>
                                <td>
                        <asp:TextBox ID="txtIndirizzo" runat="server" MaxLength="100" Style="left: 146px; position: static;
                            top: 96px" Width="408px" TabIndex="1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;<asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 131px" Width="120px">Codice SIA</asp:Label></td>
                                <td>
                        <asp:TextBox ID="TxtSIA" runat="server" MaxLength="11" Style="left: 146px; position: static;
                            top: 127px" Width="286px" TabIndex="2"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;<asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 161px" Width="114px">Codice Servizio</asp:Label></td>
                                <td>
                        <asp:TextBox ID="txtServizio" runat="server" MaxLength="100" Style="left: 146px;
                            position: static; top: 158px" Width="286px" TabIndex="3"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;<asp:Label ID="Label9" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 192px" Width="111px">Codice SottoServizio</asp:Label></td>
                                <td>
                        <asp:TextBox ID="TxtSottoServizio" runat="server" MaxLength="150" Style="left: 146px; position: static;
                            top: 189px" Width="285px" TabIndex="4"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;<asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 192px" Width="94px">Codice Lista</asp:Label></td>
                                <td>
                        <asp:TextBox ID="txtLista" runat="server" MaxLength="50" Style="left: 146px; position: static;
                            top: 189px" Width="285px" TabIndex="5"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;<asp:Label ID="Label11" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 192px" Width="94px">URL di Ritorno</asp:Label> </td>
                                <td>
                        <asp:TextBox ID="txturl" runat="server" MaxLength="90" Style="left: 146px; position: static;
                            top: 189px" Width="405px" TabIndex="6"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp; </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 443px" Visible="False" Width="525px" TabIndex="-1"></asp:Label>
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
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 102; left: 711px; position: absolute; top: 441px" TabIndex="9"
                            ToolTip="Home" />
                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                            
                            
                            Style="z-index: 101; left: 629px; position: absolute; top: 441px; right: 91px;" TabIndex="8"
                            ToolTip="Salva" />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
		</form>
	</body>
</html>
