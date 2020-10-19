<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaStatoDispUI.aspx.vb" Inherits="ASS_RicercaStatoDispUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stato Disponibilita</title>
</head>
<body bgcolor="#f2f5f1">
	<script type="text/javascript">
//document.onkeydown=$onkeydown;
</script>
    <form id="form1" runat="server" defaultbutton="btnCerca" 
    defaultfocus="txtCognome">
    <div>
        &nbsp;</div>
        <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 538px; position: absolute; top: 298px" 
        TabIndex="6" ToolTip="Home" />
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
            Style="z-index: 101; left: 404px; position: absolute; top: 298px; height: 20px;" 
        TabIndex="5" ToolTip="Avvia Ricerca" />
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                        Stato Disponibilità Unità Immobiliari</strong></span><br />
                    <br />
                    <br />
                    <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 102; left: 42px; position: absolute; top: 79px" Width="87px">Data Disdetta</asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtSlDal"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 166px;
                        position: absolute; top: 166px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="9px"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtSlAl"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 296px;
                        position: absolute; top: 166px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="9px"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtDisdettaDal"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 164px;
                        position: absolute; top: 104px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="9px"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtDisdettaAl"
                        ErrorMessage="!" Font-Bold="True" Height="19px" Style="z-index: 150; left: 299px;
                        position: absolute; top: 104px" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                        Width="9px"></asp:RegularExpressionValidator>
                    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 102; left: 181px; position: absolute; top: 106px" Width="15px">al</asp:Label>
                    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 102; left: 182px; position: absolute; top: 170px" Width="18px">al</asp:Label>
                    <asp:TextBox ID="TxtDisdettaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="10" Style="z-index: 103; left: 198px; width: 94px; position: absolute;
                        top: 102px" TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                        Style="z-index: 102; left: 43px; position: absolute; top: 144px" Width="87px">Data Sloggio</asp:Label>
                    <asp:TextBox ID="TxtSlAl" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="10"
                        Style="z-index: 103; left: 198px; width: 94px; position: absolute; top: 167px"
                        TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 102; right: 890px; left: 43px; position: absolute; top: 170px"
                        Width="19px">Dal</asp:Label>
                    <asp:TextBox ID="TxtSlDal" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="10"
                        Style="z-index: 103; left: 65px; width: 93px; position: absolute; top: 167px"
                        TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 102; left: 43px; position: absolute; top: 106px" Width="19px">Dal</asp:Label>
                    <asp:TextBox ID="TxtDisdettaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                        MaxLength="10" Style="z-index: 103; left: 65px; width: 93px; position: absolute;
                        top: 102px" TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 11px; width: 442px; position: absolute;
                        top: 323px; height: 13px" Text="Label" Visible="False"></asp:Label>
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
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
