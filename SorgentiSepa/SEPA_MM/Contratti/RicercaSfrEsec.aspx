<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSfrEsec.aspx.vb" Inherits="Contratti_RicercaSfrEsec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
                    width: 800px; position: absolute; top: 0px; height: 483px">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Ricerca Contratti per date Sfratto</span></strong><br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 64px;
                        height: 320px">
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 106; left: 652px; position: absolute; top: 295px" ToolTip="Home" 
                            TabIndex="10" />
                        &nbsp; &nbsp;
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            Style="z-index: 102; left: 42px; position: absolute; top: 8px" Width="165px">Data Convalida Sfratto</asp:Label>
                        &nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtConvSfDal"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 195px;
                            position: absolute; top: 36px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtConvSfAl"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 331px;
                            position: absolute; top: 35px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 205px; position: absolute; top: 200px" Width="18px">al</asp:Label>
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            Style="z-index: 102; left: 43px; position: absolute; top: 174px" Width="141px">Data Conferma F. P.</asp:Label>
                        <asp:TextBox ID="TxtDataConfFPAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 232px; width: 94px; position: absolute;
                            top: 197px" TabIndex="8" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; right: 890px; left: 43px; position: absolute; top: 200px"
                            Width="19px">Dal</asp:Label>
                        <asp:TextBox ID="TxtDataConfFPDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 96px; width: 93px; position: absolute;
                            top: 196px" TabIndex="7" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtSfEsecDal"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 195px;
                            position: absolute; top: 90px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtSfEsecAl"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 331px;
                            position: absolute; top: 89px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TxtDataConfFPDal"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 195px;
                            position: absolute; top: 198px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TxtDataConfFPAl"
                            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 331px;
                            position: absolute; top: 199px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="9px"></asp:RegularExpressionValidator>
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
                        ForeColor="Red" Style="z-index: 10; left: 11px; position: absolute; top: 366px; height: 13px; width: 442px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 111; left: 525px; position: absolute; top: 360px; right: 151px;" 
                        ToolTip="Avvia Ricerca" TabIndex="9" />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 212px; position: absolute; top: 101px" Width="15px">al</asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 212px; position: absolute; top: 155px" Width="18px">al</asp:Label>
        <asp:TextBox ID="TxtConvSfAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 240px; width: 94px; position: absolute;
            top: 98px" TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Style="z-index: 102; left: 50px; position: absolute; top: 129px" Width="165px">Data Sfratto Esecutivo</asp:Label>
        <asp:TextBox ID="TxtSfEsecAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 240px; width: 94px; position: absolute;
            top: 152px" TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 212px; position: absolute; top: 211px" Width="18px">al</asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
            Style="z-index: 102; left: 50px; position: absolute; top: 185px" Width="77px">Data Rinvio</asp:Label>
        <asp:TextBox ID="TxtDataRinvAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 240px; width: 94px; position: absolute;
            top: 208px" TabIndex="6" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; right: 890px; left: 50px; position: absolute; top: 211px"
            Width="19px">Dal</asp:Label>
        <asp:TextBox ID="TxtDataRinvDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 104px; width: 93px; position: absolute;
            top: 209px" TabIndex="5" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; right: 890px; left: 50px; position: absolute; top: 155px"
            Width="19px">Dal</asp:Label>
        <asp:TextBox ID="TxtSfEsecDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 104px; width: 93px; position: absolute;
            top: 153px" TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 102; left: 50px; position: absolute; top: 101px"
            Width="19px">Dal</asp:Label>
        <asp:TextBox ID="TxtConvSfDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
            MaxLength="10" Style="z-index: 103; left: 104px; width: 93px; position: absolute;
            top: 99px" TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TxtDataRinvDal"
            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 202px;
            position: absolute; top: 211px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="9px"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TxtDataRinvAl"
            ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 338px;
            position: absolute; top: 210px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
            Width="9px"></asp:RegularExpressionValidator>
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
