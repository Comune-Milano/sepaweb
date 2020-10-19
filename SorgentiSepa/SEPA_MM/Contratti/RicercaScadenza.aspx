<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaScadenza.aspx.vb" Inherits="Contratti_RicercaScadenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="txtDataDal">
    <div>
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricerca Contratti in Scadenza</span></strong><br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 469px">
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 652px; position: absolute; top: 402px" ToolTip="Home" 
                            TabIndex="9" />
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            
                            Style="z-index: 102; left: 41px; position: absolute; top: 286px; height: 14px; width: 154px; right: 589px;">Rinnovo Ammissibile</asp:Label>
                        <asp:DropDownList ID="cmbNotificato" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="8pt" Height="20px" 
                            Style="border: 1px solid black; z-index: 111; left: 232px; position: absolute; top: 310px; width: 95px;" 
                            TabIndex="6">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            
                            Style="z-index: 102; left: 41px; position: absolute; top: 311px; width: 101px;">Notifica Disdetta</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Style="z-index: 102; left: 13px; position: absolute; top: 8px; width: 452px;">Intervallo di tempo basato sulla data Scadenza Rinnovo del Contratto</asp:Label>
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Style="z-index: 102; left: 13px; position: absolute; top: 63px; width: 452px;">Intervallo di tempo basato sulla data Scadenza del Contratto</asp:Label>
                        <asp:DropDownList ID="CmbRinnovoAmmissibile" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="8pt" Height="20px" 
                            Style="border: 1px solid black; z-index: 111; left: 232px; position: absolute; top: 284px" 
                            TabIndex="5" Width="95px">
                            <asp:ListItem>NON DEFINITO</asp:ListItem>
                            <asp:ListItem Value="0">NO</asp:ListItem>
                            <asp:ListItem Value="1">SI</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 195px;
                            position: absolute; top: 36px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                            runat="server" ControlToValidate="TextBox1"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 195px;
                            position: absolute; top: 87px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 331px;
                            position: absolute; top: 35px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                            runat="server" ControlToValidate="TextBox2"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 331px;
                            position: absolute; top: 86px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <br />
            
                        <asp:CheckBoxList ID="CheckTipologie" runat="server" Font-Names="arial" 
                            Font-Size="8pt" Font-Strikeout="False" RepeatColumns="4" 
                            style="position:absolute; top: 116px; left: 88px;">
                        </asp:CheckBoxList>
            
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
                    &nbsp;<br />
                    &nbsp;<br />
                    <asp:HiddenField ID="DivVisible" runat="server" />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 494px; height: 13px; width: 442px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 519px; position: absolute; top: 468px; right: 151px;" 
                        ToolTip="Avvia Ricerca" TabIndex="6" />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 212px; position: absolute; top: 101px" Width="3px">al</asp:Label>
            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 212px; position: absolute; top: 153px" 
            Width="3px">al</asp:Label>
        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 240px; position: absolute; top: 98px; width: 94px;"
            TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 240px; position: absolute; top: 150px; width: 94px;"
            TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 102; left: 50px; position: absolute; top: 101px; right: 890px;" 
            Width="19px">Dal</asp:Label>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 102; left: 50px; position: absolute; top: 186px; right: 1468px;" 
            Width="19px">Tipologia</asp:Label>
            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 102; left: 50px; position: absolute; top: 155px; right: 1468px;" 
            Width="19px">Dal</asp:Label>
        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 104px; position: absolute; top: 99px; width: 93px;"
            TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 104px; position: absolute; top: 151px; width: 93px;"
            TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
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
</html>
