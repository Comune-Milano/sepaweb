<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriDepCauz.aspx.vb" Inherits="Contratti_ParametriDepCauz" %>

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
                width: 257px;
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
                            Restituzione Deposito Cauzionale&nbsp;</strong></span><br />
                        <br />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<br />
                        <table width="100%">
                            <tr>
                                <td class="style1">
                                    &nbsp;<asp:Label ID="Label6" 
                                        runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 99px" Width="236px">Data Calcolo Restituzione Interessi (GG/MM)* : </asp:Label></td>
                                <td>
                        <asp:TextBox ID="txtData" runat="server" MaxLength="100" Style="left: 146px; position: static;
                            top: 96px" Width="64px" TabIndex="1"></asp:TextBox></td>

                                
                            </tr>
                            <tr>
                                <td class="style1" > &nbsp;
                                    </td>

                                <td >
                                    
                                </td>
                            </tr>
                            
                            <tr >
                                <td class="style1">&nbsp;<asp:Label ID="Label2" 
                                        runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 15px; position: static; top: 99px" Width="193px">Tipo di restituzione* : </asp:Label>
                                    </td>

                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton1" onclick="handleClick(this);" value="0" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="A" TabIndex="9" Text="Interessi restituiti nella prima bolletta utile" />
                                </td>

                            </tr>
                            <tr>
                                <td class="style1">
                                    &nbsp;</td>

                                <td style="height: 21px">
                                    <asp:RadioButton ID="RadioButton2" onclick="handleClick(this);" value="1" runat="server" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="A" TabIndex="9" Text="Nella bolletta N." />
                                        &nbsp;
                                    <asp:TextBox ID="txtRata" runat="server" MaxLength="100" Style="left: 146px; position: static;
                                      top: 96px" Width="64px" TabIndex="1"></asp:TextBox>
                                      &nbsp;

                                    </td>

                                <td>
                                
                                </td>
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

<script type="text/javascript">
    function handleClick(myRadio) {
        if (myRadio.value == '0') {
            document.getElementById("txtRata").style.visibility = 'hidden';
            //document.getElementById("txtAnno").style.visibility = 'hidden';
            //document.getElementById("Label1").style.visibility = 'hidden';
            document.getElementById("txtRata").value = ' ';
            //document.getElementById("txtAnno").value = ' ';
        }
        else {
            document.getElementById("txtRata").style.visibility = 'visible';
            //document.getElementById("txtAnno").style.visibility = 'visible';
            //document.getElementById("Label1").style.visibility = 'visible';
        }
    }
</script>
</html>
