<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSituazContabile.aspx.vb" Inherits="RicercaSituazContabile" %>

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
    <title>RICERCA PER STAMPA SC</title>
    <style type="text/css">
        .style1
        {
            width: 125px;
        }
        .style2
        {
            width: 30px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;
        <table style="left: 0px;
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td >
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        &nbsp; 
                        <br />
                        &nbsp; Ricerca Situazione Contabile</span></strong><br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 320px">
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        <table style="width: 500px">
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td style="width: 120px">
                                    <asp:Label ID="LblES" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" Style="z-index: 100; left: 48px; top: 32px" Width="120px">Esercizio Finanziario</asp:Label></td>
                                <td style="width: 350px">
        <asp:DropDownList ID="cmbEsercizio" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 168px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 32px" TabIndex="1"
            Width="350px">
        </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 30px; height: 30px">
                                </td>
                                <td style="height: 30px; width: 120px;">
                        </td>
                                <td style="height: 30px; width: 350px;">
                        </td>
                            </tr>
                            <tr>
                                <td style="width: 30px">
                                </td>
                                <td style="width: 120px">
                                    <asp:Label ID="lblDataP1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 100; left: 48px; top: 32px" Width="120px">Data Esercizio dal</asp:Label><br />
                                    <br />
                                </td>
                                <td style="width: 350px">
                                    <table style="width: 350px">
                                        <tr>
                                            <td style="width: 100px">
                                    <asp:TextBox ID="txtDataDAL" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                        Style="left: 144px; top: 192px" TabIndex="3" ToolTip="gg/mm/aaaa" Width="70px" Enabled="False"></asp:TextBox></td>
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                    <asp:Label ID="lblDataP2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        Style="z-index: 104; left: 48px; top: 64px" Width="100px" Height="16px">Data Esercizio al</asp:Label></td>
                                            <td style="width: 100px; text-align: right">
                                    <asp:TextBox ID="txtDataAL" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="10"
                                        Style="left: 144px; top: 192px" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100px">
                                            </td>
                                            <td style="width: 50px">
                                            </td>
                                            <td>
                                    <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataDAL"
                                            Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                            Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Width="100px"></asp:RegularExpressionValidator></td>
                                            <td style="width: 100px; text-align: right">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>



                     
                    </div>
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
                    &nbsp;<br />
                    &nbsp;<br />
                    <br />
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="ChkStampa" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Text="Stampa con sottovoci" Width="200px" />&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/img_HomeModelli.png"
            Style="z-index: 106; left: 665px; position: absolute; top: 424px" ToolTip="Home" 
                        TabIndex="-1" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_VisualizzaReport.png"
            Style="z-index: 111; left: 517px; position: absolute; top: 424px; right: 153px;" 
                        ToolTip="Avvia Stampa" OnClick="btnCerca_Click" TabIndex="-1" />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField id="txtDATA" runat="server"></asp:HiddenField>
    </form>
    
</body>


</html>
