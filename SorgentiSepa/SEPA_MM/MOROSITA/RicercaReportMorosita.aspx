<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaReportMorosita.aspx.vb" Inherits="MOROSITA_RicercaReportMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<script language="javascript" type="text/javascript">

function CompletaData(e,obj) {
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

<head id="Head1" runat="server">
    <title>RICERCA REPORT CONTABILI MOROSITA</title>
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style3
        {
            width: 3px;
        }
        .style5
        {
            height: 180px;
        }
        .style6
        {
            height: 50px;
        }
    </style>
</head>

<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
       <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <form id="form1" runat="server" defaultbutton="btnStampa">
    <div>
        <table style="left: 0px; top: 0px" id="Table1">
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        Ricerca Report 
                    Morosità</span></strong>
                <table id="Table2">
                            <tr>
                                <td class="style6">
                                    </td>
                                <td class="style6">
                                    </td>
                                <td class="style6">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblProtocollo0" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
                                                Width="120px" Visible="False">Tipo di gestione:
                                    </asp:Label>
                                 </td>
                                <td>
                                    <asp:RadioButtonList ID="RBList1" runat="server" Font-Bold="True" 
                                        Font-Names="Arial" Font-Size="8pt" 
                                        Style="left: 248px; top: 232px; border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid; border-left: lightsteelblue 1px solid; border-bottom: lightsteelblue 1px solid; vertical-align: middle; text-align: left;" 
                                        TabIndex="7" Visible="False">
                                        <asp:ListItem Value="MA" Selected="True">Gestore (dopo il 30.9.2009)</asp:ListItem>
                                        <asp:ListItem Value="MG">Global Service (fino al 30.9.2009)</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                                <td class="style1">
                                    </td>
                                <td class="style1">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="125px">Periodo di riferimento dal:</asp:Label>
                                </td>
                                <td class="style1">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataRIF_DAL" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="3" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRIF_DAL"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                            <td class="style3">
                                                &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                    Style="z-index: 104; left: 48px; top: 64px" Width="15px"> al:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtDataRIF_AL" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="4" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataRIF_AL"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="150px"></asp:RegularExpressionValidator></td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="style1">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="125px">Data di emissione dal:</asp:Label>
                                </td>
                                <td class="style1">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataEMISSIONE_DAL" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="5" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataEMISSIONE_DAL"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" 
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                            <td class="style3">
                                                &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                    Style="z-index: 104; left: 48px; top: 64px" Width="15px"> al:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtDataEMISSIONE_AL" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="6" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataEMISSIONE_AL"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="150px"></asp:RegularExpressionValidator></td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="style1">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left; " class="style5">
                    </td>
                                <td class="style5">
                                    </td>
                                <td class="style5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left; height: 26px;">
                                    &nbsp;</td>
                                <td style="height: 26px">
                                    &nbsp;</td>
                                <td style="height: 26px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; text-align: left; height: 26px;">
                    </td>
                                <td style="height: 26px">
                                    <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
            Style="z-index: 106; " ToolTip="Stampa" />
                                    </td>
                                <td style="height: 26px">
                                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; " ToolTip="Home" TabIndex="1" /></td>
                            </tr>
                        </table>
               </td>
            </tr>
        </table>
    </div>
    </form>
    
    
</body>
</html>
