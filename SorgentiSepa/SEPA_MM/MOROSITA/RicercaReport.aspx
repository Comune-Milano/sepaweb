<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaReport.aspx.vb" Inherits="MOROSITA_RicercaReport" %>

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
    <title>RICERCA REPORT MOROSITA</title>
</head>

<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
       <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <div>
        <table style="left: 0px; top: 0px">
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        Ricerca Report
                        Morosità</span></strong> &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <table>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    </td>
                                <td style="height: 21px">
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
                                    </td>
                                <td style="height: 21px"></td>
                            </tr>
                            <tr>
                                <td style="height: 21px">
                                </td>
                                <td style="height: 21px">
        <asp:Label ID="lblProtocollo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px"
            Width="120px">Protocollo gestore:</asp:Label></td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtProtocollo" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                        top: 171px; text-transform: uppercase;" TabIndex="3" Width="544px" ToolTip="Inserendo il carattere *  si effettua una ricerca parziale del protocollo"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 65px">
                                    &nbsp; &nbsp;&nbsp;
                                </td>
                                <td style="height: 65px">
                                    <asp:Label ID="lblDataProtocollo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 48px; top: 96px" Width="120px">Data Protocollo Gestore:</asp:Label></td>
                                <td style="height: 65px">
                                    <table>
                                        <tr>
                                            <td>
                                    <asp:TextBox ID="txtDataDAL" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                        Style="left: 144px; top: 192px" TabIndex="4" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                            <td style="width: 1px;">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDAL"
                                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="152px"></asp:RegularExpressionValidator></td>
                                            <td style="width: 3px;">
                                                &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td>
                                    <asp:Label ID="lblDataP2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 48px; top: 64px" Width="30px"> al:</asp:Label></td>
                                            <td>
                                    <asp:TextBox ID="txtDataAL" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                        Style="left: 144px; top: 192px" TabIndex="5" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                            <td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataAL"
                                        Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                        Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Width="150px"></asp:RegularExpressionValidator></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    </td>
                                <td>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="125px">Competenza Bollette dal:</asp:Label></td>
                                <td>
                                    &nbsp;<table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataRIF_DAL" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="19" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRIF_DAL"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                            <td style="width: 3px;">
                                                &nbsp; &nbsp;&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                    Style="z-index: 104; left: 48px; top: 64px" Width="15px"> al:</asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtDataRIF_AL" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                    MaxLength="10" Style="left: 144px; top: 192px" TabIndex="20" ToolTip="gg/mm/aaaa"
                                                    Width="70px"></asp:TextBox></td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataRIF_AL"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="150px"></asp:RegularExpressionValidator></td>
                                        </tr>
                                    </table>
        </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 121px">
                                </td>
                                <td style="vertical-align: top; text-align: left; height: 121px;">
                                    <br />
                                    </td>
                                <td style="height: 121px">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 9px">
                                </td>
                                <td style="vertical-align: top; text-align: left; height: 9px;">
                    </td>
                                <td style="height: 9px">
                    </td>
                            </tr>
                            <tr>
                                <td style="height: 26px">
                                </td>
                                <td style="vertical-align: top; text-align: left; height: 26px;">
                    </td>
                                <td style="height: 26px">
                                    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                                        OnClick="btnCerca_Click" Style="z-index: 111; left: 512px; top: 448px" ToolTip="Avvia Ricerca" />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa_Grande.png"
            Style="z-index: 106; left: 656px; top: 448px" ToolTip="Home" TabIndex="2" />
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 656px; top: 448px" ToolTip="Home" TabIndex="2" /></td>
                            </tr>
                        </table>
                    <asp:HiddenField ID="txtProtocollo_TMP" runat="server" />    <asp:HiddenField ID="txtDataDAL_TMP" runat="server" />
                    <asp:HiddenField ID="txtDataAL_TMP" runat="server" />
                    <asp:HiddenField ID="txtNomeFile" runat="server" />
                    <asp:HiddenField ID="txtFlag" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    
    
</body>
</html>
