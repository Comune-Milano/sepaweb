<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptDateConvocazioni.aspx.vb" Inherits="Condomini_RptDateConvocazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body bgColor="#f2f5f1" style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
        <script type="text/javascript"  >
    
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
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Ricerca Convocazioni Condominiali per data Convocazione" Width="759px"></asp:Label>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="Immagini/Img_Home.png" Style="z-index: 106;
                left: 666px; position: absolute; top: 304px" TabIndex="5" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="Immagini/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 534px; position: absolute; top: 304px" TabIndex="4"
            ToolTip="Avvia Ricerca" />
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 14px; position: absolute; top: 278px" Text="Label"
            Visible="False" Width="624px"></asp:Label>
        <asp:Label ID="lblCognome0" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 175px; position: absolute; top: 119px" TabIndex="-1">al</asp:Label>
        <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 104; left: 50px; position: absolute; top: 118px" TabIndex="-1">Dal</asp:Label>
        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 73px; position: absolute;
            top: 115px" TabIndex="1" Width="90px"></asp:TextBox>
        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 195px; position: absolute;
            top: 115px" TabIndex="2" Width="90px"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
            Style="z-index: 2; left: 169px; position: absolute; top: 117px" ToolTip="Inserire una data valida"
            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="1px"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Height="1px"
            Style="z-index: 2; left: 290px; position: absolute; top: 117px" ToolTip="Inserire una data valida"
            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="1px"></asp:RegularExpressionValidator>
    </div>
    </form>
</body>
</html>
