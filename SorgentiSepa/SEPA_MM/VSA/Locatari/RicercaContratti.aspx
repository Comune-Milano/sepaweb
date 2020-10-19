<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaContratti.aspx.vb"
    Inherits="Contratti_RicercaContratti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            alert('Usare il tasto <Avvia Ricerca>');
            history.go(0);
            event.keyCode = 0;
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<head>
    <title>Ricerca Contratti</title>
</head>
<body bgcolor="#f2f5f1">
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
    <form id="Form1" method="post" runat="server" defaultbutton="btnCerca" defaultfocus="txtCognome">
    <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
        width: 674px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Gestione
                    Locatari - Intestatario domanda</strong></span><br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 106; left: 50px; position: absolute; top: 127px" TabIndex="-1">Rag. Sociale</asp:Label>
                <asp:TextBox ID="txtRagione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Style="z-index: 107; left: 164px; position: absolute; top: 124px" TabIndex="3"></asp:TextBox>
                <br />
                <img onclick="javascript:myOpacity.toggle();" alt="" src="../../NuoveImm/Img_Indirizzi.png"
                    style="position: absolute; top: 244px; left: 330px; cursor: pointer;" />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 298px" TabIndex="-1">Tipologia Rapporto</asp:Label>
                <asp:DropDownList ID="cmbTipo" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 165px; position: absolute; top: 295px" TabIndex="10" Width="278px">
                </asp:DropDownList>
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 273px" TabIndex="-1">Stato Rapporto</asp:Label>
                <asp:DropDownList ID="cmbStato" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 165px; position: absolute; top: 268px" TabIndex="9" Width="278px">
                    
                    <asp:ListItem>IN CORSO</asp:ListItem>
                    <asp:ListItem>IN CORSO (S.T.)</asp:ListItem>
                    
                    <asp:ListItem Selected="True">TUTTI</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 327px" TabIndex="-1">Tipologia Unità</asp:Label>
                <asp:DropDownList ID="cmbTipoImm" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 165px; position: absolute; top: 323px" TabIndex="11" Width="278px">
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 387px; position: absolute; top: 353px; right: 348px;
                    height: 16px; width: 65px;" TabIndex="-1">Inserito Dal</asp:Label>
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 353px; right: 643px;"
                    Width="107px" TabIndex="-1">Stipula Dal</asp:Label>
                <asp:TextBox ID="txtInseritoDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 446px; position: absolute; top: 351px"
                    TabIndex="14" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:TextBox ID="txtStipulaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 351px"
                    TabIndex="12" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 539px; position: absolute; top: 353px" Width="22px"
                    TabIndex="-1">Al</asp:Label>
                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 260px; position: absolute; top: 353px" Width="22px"
                    TabIndex="-1">Al</asp:Label>
                <asp:TextBox ID="txtInseritoAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 556px; position: absolute; top: 350px"
                    TabIndex="15" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 350px"
                    TabIndex="13" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 376px" Width="105px"
                    TabIndex="-1">Decorrenza Dal</asp:Label>
                <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 452px" Width="105px"
                    TabIndex="-10">Sloggio Dal</asp:Label>
                <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 427px; width: 112px;"
                    TabIndex="-10">Disdetta/R.Forzoso Dal</asp:Label>
                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 401px" Width="105px"
                    TabIndex="-10">Scadenza Dal</asp:Label>
                <asp:TextBox ID="txtSloggioDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 449px;
                    right: 568px;" TabIndex="22" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:TextBox ID="txtDisdettaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 424px"
                    TabIndex="20" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:TextBox ID="txtScadeDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 399px"
                    TabIndex="18" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 259px; position: absolute; top: 451px; width: 23px;">Al</asp:Label>
                <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 259px; position: absolute; top: 427px; width: 23px;">Al</asp:Label>
                <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 259px; position: absolute; top: 401px; width: 23px;">Al</asp:Label>
                <asp:TextBox ID="txtSloggioAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 448px"
                    TabIndex="23" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:TextBox ID="txtDisdettaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 423px"
                    TabIndex="21" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:TextBox ID="txtScadeAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 399px"
                    TabIndex="19" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ControlToValidate="txtSloggioAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 363px; position: absolute; top: 453px"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDisdettaAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 363px; position: absolute; top: 427px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtScadeAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 363px; position: absolute; top: 401px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtSloggioDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 453px; height: 14px;" TabIndex="-1"
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDisdettaDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 428px; height: 14px;" TabIndex="-1"
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtScadeDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 401px; height: 14px;" TabIndex="-1"
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtDecorrenzaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 375px;
                    right: 568px;" TabIndex="16" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 260px; position: absolute; top: 377px" Width="22px"
                    TabIndex="-1">Al</asp:Label>
                <asp:TextBox ID="txtDecorrenzaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 374px"
                    TabIndex="17" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                    ControlToValidate="txtInseritoDal" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 517px; position: absolute; top: 353px"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 353px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDecorrenzaAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 364px; position: absolute; top: 376px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                    ControlToValidate="txtInseritoAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 630px; position: absolute; top: 353px"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 363px; position: absolute; top: 353px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDecorrenzaDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 376px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:CheckBox ID="ChSloggio" runat="server" Style="position: absolute; top: 448px;
                    left: 396px;" Font-Names="ARIAL" Font-Size="9pt" TabIndex="24" Text="Senza date di sloggio" />
                <br />
                <br />
                <br />
                <asp:HiddenField ID="ModRich" runat="server" />
                <br />
            </td>
        </tr>
    </table>
    <img src="../../ImmMaschere/alert2_ricercad.gif" style="z-index: 117; left: 327px;
        position: absolute; top: 125px" alt="Image" />
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 550px; position: absolute; top: 503px" TabIndex="26"
        ToolTip="Home" />
    <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="~/NuoveImm/Img_IndietroGrande.png"
        Style="z-index: 101; left: 307px; position: absolute; top: 503px" TabIndex="25"
        ToolTip="Indietro" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 400px; position: absolute; top: 503px" TabIndex="25"
        ToolTip="Avvia Ricerca" />
    <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 102; left: 50px; position: absolute; top: 80px; right: 1061px;"
        TabIndex="-1">Cognome</asp:Label>
    <asp:TextBox ID="txtCognome" TabIndex="1" runat="server" Style="z-index: 103; position: absolute;
        top: 77px; left: 164px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 104; left: 50px; position: absolute; top: 104px" TabIndex="-1">Nome</asp:Label>
    <asp:TextBox ID="txtNome" TabIndex="2" runat="server" Style="z-index: 105; left: 164px;
        position: absolute; top: 100px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 106; left: 50px; position: absolute; top: 152px; height: 14px;
        width: 70px;" TabIndex="-1">Codice Fiscale</asp:Label>
    <asp:TextBox ID="txtCF" TabIndex="4" runat="server" Style="z-index: 107; left: 164px;
        position: absolute; top: 148px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 106; left: 50px; position: absolute; top: 175px" TabIndex="-1">Partita Iva</asp:Label>
    <asp:TextBox ID="txtpiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 107;
        left: 164px; position: absolute; top: 172px" TabIndex="5"></asp:TextBox>
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 224px; height: 14px;"
        Width="100px" TabIndex="-1">Codice C. GIMI</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 199px" Width="100px"
        TabIndex="-1">Codice Rapporto</asp:Label>
    <asp:TextBox ID="txtCodGIMI" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 221px" TabIndex="7" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:TextBox ID="txtCod" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 196px" TabIndex="6" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 247px" Width="100px"
        TabIndex="-1">Codice Unità</asp:Label>
    <asp:TextBox ID="txtUnita" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 245px" TabIndex="8"></asp:TextBox>
    <div id="Indirizzi" style="display: block; border: 1px solid #0000FF; position: absolute;
        width: 255px; background-color: #C0C0C0; top: 66px; left: 405px; height: 350px;
        overflow: auto; z-index: 200;">
        <table style="width: 90%;">
            <tr>
                <td style="text-align: right">
                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../../NuoveImm/Img_Conferma.png"
                        style="cursor: pointer" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="chIndirizzi" runat="server" Font-Names="arial" Font-Size="8pt">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../../NuoveImm/Img_Conferma.png"
                        style="cursor: pointer" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    myOpacity = new fx.Opacity('Indirizzi', { duration: 200 });
    myOpacity.hide();
    //document.getElementById('Indirizzi').style.visibility='hidden';
</script>
</html>
