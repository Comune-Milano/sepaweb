<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaDichiarazioni.aspx.vb"
    Inherits="ANAUT_RicercaDichiarazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        //if (event.keyCode==13) 
        //{  
        //alert('Usare il tasto <Avvia Ricerca>');
        //history.go(0);
        //event.keyCode=0;
        //}  
    }

</script>
<script type="text/javascript">
    //document.onkeydown = $onkeydown;


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
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ricerca</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body bgcolor="#f2f5f1" onload="document.getElementById('btnCerca').focus()">
    <script type="text/javascript">
        //document.onkeydown=$onkeydown;
    </script>
    <form id="Form1" method="post" runat="server" defaultbutton="btnCerca" defaultfocus="txtCognome">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                    Dichiarazioni</strong></span><br />
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
                <asp:CheckBox ID="ch45" runat="server" Style="position: absolute; top: 357px; left: 160px;"
                    TabIndex="12" />
                <asp:CheckBox ID="chAutomatiche" runat="server" Style="position: absolute; top: 357px;
                    left: 435px;" Checked="True" TabIndex="13" />
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
    <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 117; left: 349px; position: absolute;
        top: 82px" />
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 539px; position: absolute; top: 500px" TabIndex="16"
        ToolTip="Home" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 406px; position: absolute; top: 500px" TabIndex="15"
        ToolTip="Avvia Ricerca" />
    <asp:Label ID="Label1" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 102; left: 50px; position: absolute; top: 101px">Cognome</asp:Label>
    <asp:TextBox ID="txtCognome" TabIndex="1" runat="server" Style="z-index: 103; left: 164px;
        position: absolute; top: 97px" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 104; left: 50px; position: absolute; top: 126px">Nome</asp:Label>
    <asp:TextBox ID="txtNome" TabIndex="2" runat="server" Style="z-index: 105; left: 164px;
        position: absolute; top: 124px" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 106; left: 50px; position: absolute; top: 153px">Codice Fiscale</asp:Label>
    <asp:TextBox ID="txtCF" TabIndex="3" runat="server" Style="z-index: 107; left: 164px;
        position: absolute; top: 151px" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
    <asp:Label ID="Label5" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 108; left: 50px; position: absolute; top: 180px">Numero</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 108; left: 337px; position: absolute; top: 180px">Dal</asp:Label>
    <asp:Label ID="Label13" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        
        Style="z-index: 108; left: 456px; position: absolute; top: 180px; height: 14px;">Al</asp:Label>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        Style="left: 441px; position: absolute; top: 179px" TabIndex="-1" 
        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
    <asp:TextBox ID="txtPG" TabIndex="4" runat="server" Style="z-index: 109; left: 164px;
        position: absolute; top: 178px" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
    <asp:TextBox ID="txtStipulaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
        MaxLength="10" Style="z-index: 113; left: 358px; position: absolute; top: 178px"
        TabIndex="5" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
    <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
        MaxLength="10" Style="z-index: 113; left: 472px; position: absolute; top: 178px"
        TabIndex="6" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        Style="left: 555px; position: absolute; top: 180px" TabIndex="-1" 
        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
    <asp:Label ID="Label11" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 329px">Tipo</asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 363px">Solo 4-5 Lotto</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 336px; position: absolute; top: 361px; height: 34px;
        width: 105px;">Escludi Gen. Automaticamente</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 398px; width: 105px;">AU Art.15</asp:Label>
        <asp:Label ID="Label16" runat="server" Font-Size="X-Small" 
        Font-Names="Arial" Font-Bold="True"
        
        
        Style="z-index: 110; left: 50px; position: absolute; top: 432px; width: 105px;">Operatore</asp:Label>
    <asp:Label ID="Label15" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        
        Style="z-index: 110; left: 336px; position: absolute; top: 398px; width: 105px;">Componenti In Carrozzina</asp:Label>
    <asp:Label ID="Label10" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 298px">Bando</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Size="X-Small" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 50px; position: absolute; top: 210px">Stato</asp:Label>
    <asp:DropDownList ID="cmbBando" TabIndex="10" runat="server" Height="20px" Width="160px"
        Style="border: 1px solid black; z-index: 111; left: 164px; position: absolute;
        top: 293px">
    </asp:DropDownList>
    <asp:DropDownList ID="cmbTipo" TabIndex="11" runat="server" Height="20px" Width="160px"
        Style="border: 1px solid black; z-index: 111; left: 164px; position: absolute;
        top: 325px">
        <asp:ListItem Value="0">TUTTE</asp:ListItem>
        <asp:ListItem Value="1">DA VERIFICARE</asp:ListItem>
        <asp:ListItem Value="2">IN SOSPESO</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="CMBaRT15" TabIndex="14" runat="server" Height="20px" Width="160px"
        Style="border: 1px solid black; z-index: 111; left: 164px; position: absolute;
        top: 395px">
        <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="ddl_carrozzina" TabIndex="14" runat="server" Height="20px"
        Width="154px" Style="border: 1px solid black; z-index: 111; left: 440px; position: absolute;
        top: 395px">
        <asp:ListItem Value="0" Selected="True">---</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
        <asp:ListItem Value="2">NO</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cmbStato" TabIndex="7" runat="server" Height="20px" Width="160px"
        Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111;
        left: 164px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute;
        top: 206px">
    </asp:DropDownList>
    <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
        Style="z-index: 112; left: 50px; position: absolute; top: 268px" Width="100px">Codice Unità</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small"
        Style="z-index: 112; left: 50px; position: absolute; top: 238px" Width="100px">Codice Rapporto</asp:Label>
    <asp:TextBox ID="txtUnita" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 263px" TabIndex="9" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
    <asp:TextBox ID="txtCod" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 236px" TabIndex="8" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
        <asp:TextBox ID="txtOp" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 431px" TabIndex="14" BorderStyle="Solid" BorderWidth="1px" 
        Width="155px"></asp:TextBox>
    </form>
</body>
</html>
