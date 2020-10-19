<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateDettBoll.aspx.vb" Inherits="Contabilita_DateDettBoll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Date Dettaglio Bollette</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12pt;
            width: 54px;
        }
        .style5
        {
            width: 135px;
        }
        .style6
        {
            width: 105px;
        }
        .style7
        {
            width: 104px;
        }
        .style8
        {
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnVisualizza">
    <div>
    
        <table style="left: 0px; 
            width: 100%; ">
            <tr>
                <td style="left: 0px;">
                    <div style="height: 32px">
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        Date Dettaglio Bollette<br />
                        <br />
                        <span style="color: #801f1c; font-family: Arial; text-align: center;" 
                            class="style8">
                        Inserire il range di date per l&#39;elaborazione del Dettaglio Bollette</span></span><table style="width:100%;">
                            <tr>
                                <td class="style1">
                        &nbsp;
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt">Dal</asp:Label>
                                &nbsp;</td>
                                <td class="style6">
                                    <asp:TextBox ID="TxtDal" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                        Width="80px" TabIndex="1" ToolTip="Formato data dd/mm/yyyy"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            runat="server" ControlToValidate="TxtDal" ErrorMessage="!" Font-Bold="True" Height="16px"
                                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            Width="15px"></asp:RegularExpressionValidator></td>
                                <td class="style1">
                        <strong>
                        &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" 
                                        Font-Size="12pt">al</asp:Label>
                                &nbsp;</strong></td>
                                <td class="style7">
                                    <strong>
                                    <asp:TextBox ID="TxtAl" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                        Width="80px" TabIndex="2" ToolTip="Formato data dd/mm/yyyy"></asp:TextBox>&nbsp; <strong><strong>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtAl"
                                                ErrorMessage="!" Font-Bold="True" Height="16px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                Width="16px"></asp:RegularExpressionValidator></strong></strong></strong></td>
                                <td class="style5">
                                    <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            
        ToolTip="Visualizza Pagamenti Pervenuti" style="vertical-align: bottom" TabIndex="3" />
                                </td>
                                <td>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_Grande.png"
            ToolTip="Chiude la finestra" style="vertical-align: bottom" TabIndex="4" />
    
                                </td>
                            </tr>
                        </table>
                        </strong></div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="LblSuggeriemnto" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"
                        ForeColor="#0066CC" Style="z-index: 10;"
                        
                        
                        
                        Text="Se entrambe le date non sono avvalorate, il sistema ricercherà tutte le bollette emesse"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 13px; position: absolute; top: 598px; height: 13px; width: 719px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
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
