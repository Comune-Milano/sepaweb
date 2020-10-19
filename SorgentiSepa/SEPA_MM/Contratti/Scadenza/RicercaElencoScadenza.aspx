﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaElencoScadenza.aspx.vb"
    Inherits="Contratti_Scadenza_RicercaElencoScadenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    width: 780px;">
    <form id="form1" runat="server" >
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">
         <tr>
                <td style="height:5px;"></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <asp:Label Text="Ricerca Elenco Contratti in Scadenza" runat="server" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                </td>
            </tr>
            <tr>
                <td style="height:25px;"></td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:12%">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Data Riferimento Scadenza</asp:Label>
                            </td>
                            <td style="width:12%">
                                <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    MaxLength="10" ToolTip="GG/MM/YYYY"></asp:TextBox>
                            </td>
                            <td style="width:33%">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                                    ControlToValidate="txtDataDal" Display="Dynamic" ErrorMessage="Errata!" Font-Bold="False"
                                    Font-Names="arial" Font-Size="8pt" TabIndex="300" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                    ToolTip="Formato gg/mm/aaaa"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height:25px;"></td>
            </tr>
            <tr>
                <td style="width:12%">

                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                        <tr>
                            <td style="width:17%;">
                              <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Comuni"></asp:Label>
                            </td>
                            <td style="width:12%; text-align:center;">
                             
                    <asp:CheckBox ID="chSelezione" runat="server" AutoPostBack="True" Font-Names="Arial"
                        Font-Size="8pt" Text="seleziona/deseleziona tutti" />
                            </td>
                            <td style="width:33%">
                              
                            </td>
                        </tr>
                    </table>


            
                </td>
            </tr>


         
            <tr>
                <td>
                    <asp:CheckBoxList ID="chComuni" Font-Names="Arial" Font-Size="8pt" runat="server" CellPadding="2" CellSpacing="2" 
                        RepeatColumns="4" RepeatDirection="Horizontal" BorderColor="#CCCCCC"
                        BorderStyle="Solid" BorderWidth="1px" Width="100%">
                    </asp:CheckBoxList>
                </td>
            </tr>
             <tr>
                <td style="height:60px;"></td>
            </tr>
            <tr>
                <td>


                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width:70%;">
                           
                            </td>
                            <td style="width:15%;">
                             
                           <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                        
                        ToolTip="Avvia Ricerca" TabIndex="6" />

                            </td>
                            <td style="width:15%">
                               <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        ToolTip="Home"
                        TabIndex="9" />
                            </td>
                        </tr>
                    </table>
                   
                
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="DivVisible" runat="server" />
    </div>
    </form>
</body>
</html>
