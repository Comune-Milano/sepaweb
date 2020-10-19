<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriMandatario.aspx.vb" Inherits="Contratti_ParametriBolletta" %>

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
		<title>R</title>
	</head>
	<body bgcolor="#f2f5f1">
<script type="text/javascript">
    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }

</script>
		<form id="Form1" method="post" runat="server" defaultbutton="btnSave" 
        defaultfocus="txtUfficio">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Parametri
                            Mandatario&nbsp;</strong></span><br />
                        <br />
                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp;&nbsp;<br />
                        &nbsp;
                        <br />
                        <table width="100%">
                            <tr>
                                <td style="width: 3px">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 12px; position: static; top: 82px" Width="202px">Denominazione Ufficio</asp:Label></td>
                                <td style="width: 322px">
                                    <asp:TextBox ID="txtUfficio" runat="server" MaxLength="100" Style="left: 269px; position: static;
                                        top: 81px" Width="313px" TabIndex="1"></asp:TextBox></td>
                                <td style="width: 58px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 117px" Width="234px">Riferimento Convenzione</asp:Label></td>
                                <td style="width: 322px">
                                    <asp:TextBox ID="txtContratto" runat="server" MaxLength="300" Style="left: 269px;
                                        position: static; top: 116px" TabIndex="2" Width="313px"></asp:TextBox></td>
                                <td style="width: 58px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                                    <asp:Label ID="lblRecessione" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 15px; position: static; top: 157px"
                                        Width="203px">Cognome del Responsabile</asp:Label></td>
                                <td style="width: 322px">
                                    <asp:TextBox ID="txtCognome" runat="server" MaxLength="100" Style="left: 269px; position: static;
                                        top: 157px" TabIndex="3" Width="313px"></asp:TextBox></td>
                                <td style="width: 58px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                                    <asp:Label ID="lblScadenza" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 198px" Width="241px">Nome del Responsabile</asp:Label></td>
                                <td style="width: 322px">
                                    <asp:TextBox ID="txtNome" runat="server" MaxLength="100" Style="left: 269px; position: static;
                                        top: 197px" TabIndex="4" Width="313px"></asp:TextBox></td>
                                <td style="width: 58px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 249px" Width="226px">Data di nascita del Responsabile</asp:Label></td>
                                <td style="width: 322px; height: 21px">
                                    <asp:TextBox ID="txtDataNascita" runat="server" MaxLength="10" Style="left: 269px;
                                        position: static; top: 249px" TabIndex="5" Width="72px"></asp:TextBox>&nbsp;<asp:RegularExpressionValidator 
                                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataNascita"
                    Display="Dynamic" ErrorMessage="Errata!" Font-Bold="True" 
                    Font-Names="arial" Font-Size="8pt"
                    TabIndex="300" 
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator></td>
                                <td style="width: 58px; height: 21px">
                                </td>
                                <td style="height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 275px" Width="199px">Comune di Nascita</asp:Label></td>
                                <td style="width: 322px; height: 21px">
                                    <asp:TextBox ID="txtComuneNascita" runat="server" MaxLength="100" Style="left: 269px;
                                        position: static; top: 275px" TabIndex="6" Width="313px"></asp:TextBox></td>
                                <td style="width: 58px; height: 21px">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 275px" Width="58px">Provincia</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtProvinciaNascita" runat="server" MaxLength="2" Style="left: 269px;
                                        position: static; top: 249px" TabIndex="7" Width="30px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 309px" Width="177px">Comune di Residenza</asp:Label></td>
                                <td style="width: 322px; height: 21px">
                                    <asp:TextBox ID="txtComuneResidenza" runat="server" MaxLength="100" Style="left: 268px;
                                        position: static; top: 313px" TabIndex="8" Width="313px"></asp:TextBox></td>
                                <td style="width: 58px; height: 21px">
                                </td>
                                <td style="height: 21px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3px; height: 21px">
                                    <asp:Label ID="lblBollo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 351px" Width="33px">Indirizzo</asp:Label></td>
                                <td style="width: 322px; height: 21px">
                                    <asp:TextBox ID="txtIndirizzo" runat="server" MaxLength="100" Style="left: 267px;
                                        position: static; top: 351px" TabIndex="9" Width="313px"></asp:TextBox></td>
                                <td style="width: 58px; height: 21px">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 15px; position: static; top: 275px" Width="58px">Telefono</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtTelefono" runat="server" MaxLength="50" Style="left: 269px; position: static;
                                        top: 249px" TabIndex="10" Width="133px"></asp:TextBox></td>
                            </tr>
                        </table>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp;
                        &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 443px" Visible="False" Width="525px" TabIndex="-1"></asp:Label>
                        <br />
                        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 102; left: 710px; position: absolute; top: 441px" TabIndex="12"
                            ToolTip="Home" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="tipo" runat="server" />
                        <br />
                        <br />
                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                            
                            Style="z-index: 101; left: 628px; position: absolute; top: 441px; right: 92px;" TabIndex="11"
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
