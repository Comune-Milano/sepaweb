<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SimulaB.aspx.vb" Inherits="Contratti_SimulaB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Simulazione</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	    <style type="text/css">
            .style1
            {
                height: 24px;
            }
            .style2
            {
                width: 76px;
                height: 24px;
            }
        </style>
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server" defaultbutton="imgProcedi" 
        defaultfocus="cmbMese">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label><br />
                        </strong></span>
                        <br />
                        &nbsp;&nbsp;
                        <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Text="Questa procedura ha lo scopo di simulare l'emissione delle bollette. Può essere effettuata più volte e non causa modifiche ai contratti/bollette. Alla fine del processo sarà generato un file di testo contenente i dettagli per ogni singola bolletta. Sarà sempre possibile visualizzare il file tramite la funzione -Elenco simulazioni-"
                            Width="700px"></asp:Label><br />
                        &nbsp;<table width="90%">
                            <tr>
                                <td>
                                </td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 50px; position: static; top: 188px" Width="103px">Periodo Da Bollettare</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbMese" runat="server" BackColor="White"
                                        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 116px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; position: static; top: 185px" TabIndex="1" 
                                        Width="283px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 20px">
                                    &nbsp;</td>
                                <td style="width: 76px; height: 20px">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 50px; position: static; top: 188px" Width="103px">Data Emissione</asp:Label></td>
                                <td style="height: 20px">
                                    <asp:TextBox ID="txtEmissione" runat="server" Font-Names="arial" Font-Size="9pt"
                                        MaxLength="10" Width="76px" TabIndex="2"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmissione"
                                        Display="Dynamic" ErrorMessage="Errore (gg/mm/aaaa)" Font-Bold="True" Font-Names="arial"
                                        Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 50px; position: static; top: 188px" 
                                        Width="103px">Data Scadenza ERP</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtScadenza" runat="server" Font-Names="arial" Font-Size="9pt"
                                        MaxLength="10" Width="76px" TabIndex="3"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtScadenza"
                                        Display="Dynamic" ErrorMessage="Errore (gg/mm/aaaa)" Font-Bold="True" Font-Names="arial"
                                        Font-Size="8pt" TabIndex="300" 
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 50px; position: static; top: 188px" 
                                        Width="111px">Data Scadenza ALTRI</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtScadenzaAltri" runat="server" Font-Names="arial" Font-Size="9pt"
                                        MaxLength="10" Width="76px" TabIndex="4"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtScadenzaAltri"
                                        Display="Dynamic" ErrorMessage="Errore (gg/mm/aaaa)" Font-Bold="True" Font-Names="arial"
                                        Font-Size="8pt" TabIndex="300" 
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 50px; position: static; top: 188px" Width="54px">Complesso</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 116px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; position: static; top: 185px" TabIndex="8" 
                                        Width="283px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 65px; position: static; top: 405px" Width="36px">Edificio</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
                                        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 131px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; position: static; top: 402px" TabIndex="5" 
                                        Width="283px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 65px; position: static; top: 431px" Width="29px">Unità</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbUnita" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 131px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        position: static; top: 427px" TabIndex="6" Width="283px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Style="z-index: 100; left: 65px; position: static; top: 431px" 
                                        Width="60px">Esporta in:</asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    </td>
                                <td class="style2">
                                    <asp:RadioButton ID="rbCSV" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        GroupName="A" Text="CSV" />
                                </td>
                                <td class="style1">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Italic="True" 
                                        Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False" 
                                        Style="z-index: 100; left: 65px; position: static; top: 431px" Width="477px">File importabile in excel.</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:RadioButton ID="rbCSV0" runat="server" Checked="True" Font-Names="arial" 
                                        Font-Size="8pt" GroupName="A" Text="Non Estrarre" Width="95px" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="width: 76px">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Style="z-index: 100; left: 65px; position: static; top: 431px" 
                                        Width="60px">Opzioni</asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="ChkUltima" runat="server" Font-Names="arial" Font-Size="8pt" 
                                        Text="Inserisci importo ultima bolletta ordinaria (rallenta l'elaborazione)" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        
                       
                        <asp:TextBox ID="lblerrore" runat="server" Font-Names="arial" Font-Size="8pt" 
                            Height="68px" TextMode="MultiLine" Visible="False" Width="712px"></asp:TextBox>
                        
                       
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        &nbsp;
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
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 712px; position: absolute; top: 530px" 
                ToolTip="Home" TabIndex="9" />
            <asp:ImageButton ID="imgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                Style="z-index: 101; left: 606px; position: absolute; top: 530px; right: 711px; height: 20px;" 
                ToolTip="Procedi" TabIndex="8" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        </form>
    <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
    
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

	</body>
</html>
