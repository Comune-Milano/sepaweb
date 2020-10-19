<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriBolletta.aspx.vb" Inherits="Contratti_ParametriBolletta" %>

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
                width: 78px;
            }
            .style2
            {
                height: 21px;
                width: 78px;
            }
        </style>
	</head>
	<body bgcolor="#f2f5f1">

		<form id="Form1" method="post" runat="server" defaultbutton="btnSave" 
        defaultfocus="txtRegAnnualita">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametri&nbsp;</strong></span><br />
                        &nbsp;&nbsp;<table width="100%">
                            <tr>
                                <td style="width: 3px">
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 12px; position: static; top: 82px" Width="202px">Registrazione prima annualita*</asp:Label></td>
                                <td class="style1">
                        <asp:TextBox ID="txtRegAnnualita" runat="server" MaxLength="10" Style="left: 269px;
                            position: static; top: 81px" Width="72px" TabIndex="1"></asp:TextBox></td>
                                <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtRegAnnualita"
                            ErrorMessage="EROORE!" Font-Bold="True" Font-Names="Arial"
                            Font-Size="9pt" Height="20px" Style="z-index: 10; left: 347px; position: static;
                            top: 87px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="60px"></asp:RegularExpressionValidator></td>
                                <td>
                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 12px; position: static; top: 82px" Width="202px">Numero di copie sottoscritte*</asp:Label></td>
                                <td>
                        <asp:TextBox ID="TxtNumCopie" runat="server" MaxLength="2" Style="left: 269px;
                            position: static; top: 116px" Width="72px" TabIndex="3"></asp:TextBox></td>
                                <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="TxtNumCopie"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 348px;
                            position: static; top: 122px" ValidationExpression="\d+" Width="60px" 
                                        Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 12px; position: static; top: 82px" Width="202px">Bollo su Contratto*</asp:Label></td>
                                <td class="style1">
                        <asp:TextBox ID="txtBolloContratto" runat="server" MaxLength="10" Style="left: 269px;
                            position: static; top: 81px" Width="72px" TabIndex="2"></asp:TextBox></td>
                                <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtBolloContratto"
                            ErrorMessage="EROORE!" Font-Bold="True" Font-Names="Arial"
                            Font-Size="9pt" Height="20px" Style="z-index: 10; left: 347px; position: static;
                            top: 87px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="60px"></asp:RegularExpressionValidator></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 117px" Width="234px">Mesi affitto anticipato*</asp:Label></td>
                                <td class="style1">
                        <asp:TextBox ID="TxtMesiAffAntici" runat="server" MaxLength="2" Style="left: 269px;
                            position: static; top: 116px" Width="72px" TabIndex="3"></asp:TextBox></td>
                                <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TxtMesiAffAntici"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 348px;
                            position: static; top: 122px" ValidationExpression="\d+" Width="60px" 
                                        Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                        <asp:Label ID="lblRecessione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 157px" Width="203px">Recesso contratto*</asp:Label></td>
                                <td class="style1">
                        <asp:TextBox ID="TxtRecesCont" runat="server" MaxLength="10" Style="left: 269px;
                            position: static; top: 157px" Width="72px" TabIndex="4"></asp:TextBox></td>
                                <td>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtRecesCont"
                ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 348px;
                position: static; top: 163px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                        <asp:Label ID="lblScadenza" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 198px" Width="241px">Scadenza dopo giorni dalla data di emissione*</asp:Label></td>
                                <td class="style1">
                        <asp:TextBox ID="txtScadDataEmiss" runat="server" MaxLength="3" Style="left: 269px;
                            position: static; top: 197px" Width="72px" TabIndex="5"></asp:TextBox></td>
                                <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtScadDataEmiss"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 347px;
                            position: static; top: 203px" ValidationExpression="\d+" Width="60px" 
                                        Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 249px" Width="226px">Scadenza del contratto ERP dopo giorni*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtScadErp" runat="server" MaxLength="3" Style="left: 269px; position: static;
                            top: 249px" Width="72px" TabIndex="6"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtScadErp"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 348px;
                            position: static; top: 255px" ValidationExpression="\d+" Width="60px" 
                                        Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 275px" Width="199px">Non stampare se inferiore  a*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtNonStamp" runat="server" MaxLength="10" Style="left: 269px; position: static;
                            top: 275px" Width="72px" TabIndex="7"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtNonStamp"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 348px;
                            position: static; top: 281px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 309px" Width="124px">Spese Postali MILANO*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtSpPost" runat="server" MaxLength="5" Style="left: 268px; position: static;
                            top: 313px" Width="72px" TabIndex="8"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSpPost"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 345px;
                            position: static; top: 319px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="A" TabIndex="9" Text="Applica ogni mese" />
                                </td>
                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton2" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="A" TabIndex="10" Text="Mesi Dispari" 
                                        ToolTip="Gennaio, Marzo, Maggio, Luglio, Settembre, Novembre" />
                                </td>
                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton3" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="A" TabIndex="11" Text="Mesi Pari" 
                                        ToolTip="Febbraio, Aprile, Giugno, Agosto, Ottobre, Dicembre" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 309px" Width="178px">Spese Postali ALTRI CAPOLUOGHI*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtSpPost0" runat="server" MaxLength="5" Style="left: 268px; position: static;
                            top: 313px" Width="72px" TabIndex="12"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtSpPost0"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 345px;
                            position: static; top: 319px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 309px" Width="124px">Spese Postali COMUNI*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtSpPost1" runat="server" MaxLength="5" Style="left: 268px; position: static;
                            top: 313px" Width="72px" TabIndex="13"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtSpPost1"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 345px;
                            position: static; top: 319px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 309px" Width="93px">Spese MAV*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtSpMAV" runat="server" MaxLength="5" Style="left: 268px; position: static;
                            top: 313px" Width="72px" TabIndex="14"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtSpMAV"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 345px;
                            position: static; top: 319px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton4" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="B" TabIndex="15" Text="Applica ogni mese" />
                                </td>
                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton5" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="B" TabIndex="16" Text="Mesi Dispari" 
                                        ToolTip="Gennaio, Marzo, Maggio, Luglio, Settembre, Novembre" />
                                </td>
                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton6" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="B" TabIndex="17" Text="Mesi Pari" 
                                        ToolTip="Febbraio, Aprile, Giugno, Agosto, Ottobre, Dicembre" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="lblBollo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 351px" Width="101px">Bollo su Bolletta*</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="TxtBollo" runat="server" MaxLength="5" Style="left: 267px; position: static;
                            top: 351px" Width="72px" TabIndex="18"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtBollo"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 346px;
                            position: static; top: 357px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                        <asp:Label ID="lblBollo3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 351px" Width="139px">Solo per importi Superiori a*</asp:Label></td>
                                <td style="height: 21px">
                        <asp:TextBox ID="TxtBollo0" runat="server" MaxLength="5" Style="left: 267px; position: static;
                            top: 351px" Width="72px" TabIndex="19"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="TxtBollo0"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 346px;
                            position: static; top: 357px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="lblBollo0" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 351px" Width="140px">% Spese Istruttoria ERP</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtPerERP" runat="server" MaxLength="5" Style="left: 267px; position: static;
                            top: 351px" Width="72px" TabIndex="20" Wrap="False"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtPerERP"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 346px;
                            position: static; top: 357px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="lblBollo1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 351px" Width="154px">% Spese Istruttoria NON ERP</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtPerNOERP" runat="server" MaxLength="5" Style="left: 267px; position: static;
                            top: 351px" Width="72px" TabIndex="21" Wrap="False"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtPerUSD"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 346px;
                            position: static; top: 357px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                        <asp:Label ID="lblBollo2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 351px" Width="162px">% Spese Istruttoria USI DIVERSI</asp:Label></td>
                                <td class="style2">
                        <asp:TextBox ID="txtPerUSD" runat="server" MaxLength="5" Style="left: 267px; position: static;
                            top: 351px" Width="72px" TabIndex="22" Wrap="False"></asp:TextBox></td>
                                <td style="height: 21px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="TxtBollo"
                            ErrorMessage="ERRORE!" Font-Bold="True" Height="20px" Style="z-index: 10; left: 346px;
                            position: static; top: 357px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="60px" Font-Names="Arial" Font-Size="9pt"></asp:RegularExpressionValidator></td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                                <td style="height: 21px">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp;
                        &nbsp;
                        &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 498px" Visible="False" Width="525px" TabIndex="-1"></asp:Label>
                        <br />
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 102; left: 711px; position: absolute; top: 496px" TabIndex="19"
                            ToolTip="Home" />
                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                            Style="z-index: 101; left: 628px; position: absolute; top: 496px" TabIndex="18"
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
