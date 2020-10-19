<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaStManutentivo.aspx.vb" Inherits="MANUTENZIONI_RicercaStManutentivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgcolor="#ffffff">
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Ricerca Stato Manutentivo</strong></span><br />
                    <br />
                    <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 44px; position: absolute; top: 99px"
            Width="19px">Dal</asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                        runat="server" ControlToValidate="TxtConsChDal"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 164px;
                            position: absolute; top: 98px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                        runat="server" ControlToValidate="TxtConsChAl"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 297px;
                            position: absolute; top: 98px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
        <asp:TextBox ID="TxtConsChAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 197px; width: 94px; position: absolute;
            top: 97px" TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 181px; position: absolute; top: 100px" Width="15px">al</asp:Label>
        <asp:TextBox ID="TxtConsChDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 67px; width: 93px; position: absolute;
            top: 97px" TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            Style="z-index: 102; left: 42px; position: absolute; top: 70px" 
                        Width="165px">Data Consegna Chiavi</asp:Label>
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
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 22px; position: absolute; top: 427px"
                        Text="Label" Visible="False" Width="535px"></asp:Label>
                    <br />
                    <br />
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
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 567px; position: absolute; top: 310px" ToolTip="Home" TabIndex="6" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 434px; position: absolute; top: 310px" ToolTip="Avvia Ricerca" TabIndex="5" />
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtRipChDal"
            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 163px;
            position: absolute; top: 160px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="9px"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtRipChAl"
            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 298px;
            position: absolute; top: 160px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="9px"></asp:RegularExpressionValidator>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 180px; position: absolute; top: 162px" Width="18px">al</asp:Label>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Style="z-index: 102; left: 42px; position: absolute; top: 136px" Width="165px">Data Ripresa Chiavi</asp:Label>
        <asp:TextBox ID="TxtRipChAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 197px; width: 94px; position: absolute;
            top: 159px" TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; right: 890px; left: 42px; position: absolute; top: 162px"
            Width="19px">Dal</asp:Label>
        <asp:TextBox ID="TxtRipChDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 67px; width: 93px; position: absolute;
            top: 159px" TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
    
    </div>
    </form>
            <script type ="text/javascript" >

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