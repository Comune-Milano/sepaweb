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
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Ricerca
                    Rapporti</strong></span><br />
                <br />
                <br />
                <br />
                <asp:CheckBox ID="ChIntest" runat="server" Style="left: 619px; position: absolute;
                    top: 77px" Font-Names="ARIAL" Font-Size="9pt" TabIndex="30" Text="Solo Intestatari"
                    Checked="True" />
                <br />
                <img src="../ImmMaschere/alert2_ricercad.gif" style="z-index: 117; left: 453px; position: absolute;
                    top: 146px" alt="Image" />
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 101; left: 50px; position: absolute; top: 135px" TabIndex="-1">Rag. Sociale</asp:Label>
                <asp:TextBox ID="txtRagione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    Style="z-index: 107; left: 164px; position: absolute; top: 130px" TabIndex="5"></asp:TextBox>
                &nbsp;<br />
                <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Indirizzi.png"
                    style="position: absolute; top: 206px; left: 329px; cursor: pointer;" />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 266px" TabIndex="-1">Tipologia Rapporto</asp:Label>
                <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 113; left: 471px; position: absolute; top: 263px" TabIndex="-1">Durata</asp:Label>
                <asp:TextBox ID="txtDurata" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 510px; position: absolute; top: 262px"
                    TabIndex="11" Width="25px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                    ControlToValidate="txtDurata" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+" Style="z-index: 113;
                    left: 543px; position: absolute; top: 265px"></asp:RegularExpressionValidator>
                <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 554px; position: absolute; top: 264px; width: 6px;"
                    TabIndex="-1">+</asp:Label>
                <asp:TextBox ID="txtRinnovo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 572px; position: absolute; top: 262px"
                    TabIndex="12" Width="25px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                    ControlToValidate="txtRinnovo" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+" Style="z-index: 113;
                    left: 606px; position: absolute; top: 265px"></asp:RegularExpressionValidator>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cmbTipo" runat="server" Height="20px" Style="border: 1px solid black;
                            z-index: 118; left: 163px; position: absolute; top: 263px" TabIndex="10" Width="278px"
                            AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:Label ID="lblSpecifico" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 114; left: 50px; position: absolute; top: 290px"
                            TabIndex="-1" Visible="False">Tipo Contr.Specifico</asp:Label>
                        <asp:DropDownList ID="cmbProvenASS" runat="server" Height="20px" Style="border: 1px solid black;
                            z-index: 118; left: 163px; position: absolute; top: 288px" TabIndex="10" Width="278px"
                            Visible="False">
                        </asp:DropDownList>
                        <asp:Label ID="lblOrigineRU" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 114; left: 50px; position: absolute; top: 317px"
                            TabIndex="-1" Visible="False">Origine Contratto</asp:Label>
                        <asp:DropDownList ID="cmbOrigineContratto" runat="server" Height="20px" Style="border: 1px solid black;
                            z-index: 118; left: 163px; position: absolute; top: 315px" TabIndex="10" Width="278px"
                            Visible="False">
                            <%--<asp:ListItem Value="0">TUTTI</asp:ListItem>
                            <asp:ListItem Value="12">Canone Convenzionato</asp:ListItem>
                            <asp:ListItem Value="8">Art.22 C.10 RR 1/2004</asp:ListItem>
                            <asp:ListItem Value="10">Forze dell&#39;Ordine</asp:ListItem>
                            <asp:ListItem Value="2">ERP Moderato</asp:ListItem>
                            <asp:ListItem Value="1">ERP Sociale</asp:ListItem>
                            --%>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 238px" TabIndex="-1">Stato Rapporto</asp:Label>
                <asp:DropDownList ID="cmbStato" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 163px; position: absolute; top: 235px" TabIndex="9" Width="278px">
                    <asp:ListItem>BOZZA</asp:ListItem>
                    <asp:ListItem>IN CORSO</asp:ListItem>
                    <asp:ListItem>IN CORSO (S.T.)</asp:ListItem>
                    <asp:ListItem>CHIUSO</asp:ListItem>
                    <asp:ListItem Selected="True">TUTTI</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 114; left: 50px; position: absolute; top: 342px" TabIndex="-1">Tipologia Unità</asp:Label>
                <asp:DropDownList ID="cmbTipoImm" runat="server" Height="20px" Style="border: 1px solid black;
                    z-index: 118; left: 163px; position: absolute; top: 341px" TabIndex="13" Width="278px">
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 389px; position: absolute; top: 367px; right: 346px;
                    height: 16px; width: 65px;" TabIndex="-1">Inserito Dal</asp:Label>
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 367px; right: 643px;"
                    Width="107px" TabIndex="-1">Stipula Dal</asp:Label>
                <asp:TextBox ID="txtInseritoDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 489px; position: absolute; top: 364px"
                    TabIndex="15" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtStipulaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 365px"
                    TabIndex="13" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 650px; position: absolute; top: 366px" Width="22px"
                    TabIndex="-1">Al</asp:Label>
                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 260px; position: absolute; top: 367px" Width="22px"
                    TabIndex="-1">Al</asp:Label>
                <asp:TextBox ID="txtInseritoAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 666px; position: absolute; top: 362px"
                    TabIndex="16" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtStipulaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 364px"
                    TabIndex="14" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 390px" Width="105px"
                    TabIndex="-1">Decorrenza Dal</asp:Label>
                <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 388px; position: absolute; top: 401px" Width="105px"
                    TabIndex="-1">Scadenza Rinn. Dal</asp:Label>
                <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 388px; position: absolute; top: 435px; right: 307px;"
                    Width="105px" TabIndex="-1">Decreto Decadenza</asp:Label>
                <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 567px; position: absolute; top: 435px; right: 128px;"
                    Width="105px" TabIndex="-1">Esito Controllo Anag.</asp:Label>
                <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 574px; position: absolute; top: 401px" Width="105px"
                    TabIndex="-1">Scadenza Rinn. Al</asp:Label>
                <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 469px" Width="105px"
                    TabIndex="-10">Sloggio Dal</asp:Label>
                <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 442px; width: 112px;"
                    TabIndex="-10">Disdetta/R.Forzoso Dal</asp:Label>
                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 50px; position: absolute; top: 415px" Width="105px"
                    TabIndex="-10">Scadenza Dal</asp:Label>
                <asp:TextBox ID="txtSloggioDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 466px;"
                    TabIndex="25" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtDisdettaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 441px"
                    TabIndex="23" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtScadeDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 415px"
                    TabIndex="19" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 259px; position: absolute; top: 469px; width: 23px;">Al</asp:Label>
                <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 259px; position: absolute; top: 445px; width: 23px;">Al</asp:Label>
                <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 259px; position: absolute; top: 418px; width: 23px;">Al</asp:Label>
                <asp:TextBox ID="txtSloggioAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 466px"
                    TabIndex="26" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtDisdettaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 441px"
                    TabIndex="24" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtScadeAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 415px"
                    TabIndex="20" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:TextBox ID="txtRinnovoDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 489px; position: absolute; top: 399px"
                    TabIndex="21" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:DropDownList ID="cmbDecretoDec" runat="server" MaxLength="10" Style="border: 1px solid black;
                    z-index: 113; left: 489px; position: absolute; top: 433px" TabIndex="21" 
                    Width="75px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="0">Non Eseguito</asp:ListItem>
                    <asp:ListItem Value="1">Eseguito</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cmbControlloAnag" runat="server" MaxLength="10" Style="border: 1px solid black;
                    z-index: 113; left: 668px; position: absolute; top: 433px" TabIndex="21" 
                    Width="130px">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="0">VUOTO</asp:ListItem>
                    <asp:ListItem Value="1">TITOLARE MONONUCLEO DECEDUTO</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtRinnovoAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 666px; position: absolute; top: 399px"
                    TabIndex="22" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                    ControlToValidate="txtSloggioAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 363px; position: absolute; top: 468px"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDisdettaAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 363px; position: absolute; top: 442px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtScadeAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 363px; position: absolute; top: 419px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtSloggioDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 465px; height: 14px;" TabIndex="-1"
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDisdettaDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 443px; height: 14px;" TabIndex="-1"
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtRinnovoDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 566px; position: absolute; top: 401px; height: 14px;" TabIndex="-1"
                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                    ControlToValidate="txtRinnovoAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 743px; position: absolute; top: 401px;
                    height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                    ControlToValidate="txtScadeDal" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 242px; position: absolute; top: 418px;
                    height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:TextBox ID="txtDecorrenzaDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 164px; position: absolute; top: 389px;
                    right: 562px;" TabIndex="17" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Style="z-index: 112; left: 260px; position: absolute; top: 392px" Width="22px"
                    TabIndex="-1">Al</asp:Label>
                <asp:TextBox ID="txtDecorrenzaAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" Style="z-index: 113; left: 286px; position: absolute; top: 389px"
                    TabIndex="18" ToolTip="gg/mm/aaaa" Width="68px">
                </asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                    ControlToValidate="txtInseritoDal" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 566px; position: absolute; top: 366px"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtStipulaDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 367px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDecorrenzaAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 364px; position: absolute; top: 391px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                    ControlToValidate="txtInseritoAl" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                    Font-Names="arial" Font-Size="8pt" Style="left: 743px; position: absolute; top: 364px"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtStipulaAl"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 363px; position: absolute; top: 366px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDecorrenzaDal"
                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    Style="left: 242px; position: absolute; top: 392px" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:CheckBox ID="ChSloggio" runat="server" Style="position: absolute; top: 467px;
                    left: 385px;" Font-Names="ARIAL" Font-Size="9pt" TabIndex="27" Text="Senza date di sloggio" />
                <asp:CheckBox ID="ChVirtuali" runat="server" Style="left: 467px; position: absolute;
                    top: 234px" Font-Names="ARIAL" Font-Size="9pt" TabIndex="30" Text="Solo R.U. virtuali" />
                <asp:CheckBox ID="chkdoc" runat="server" Style="position: absolute; top: 467px; left: 551px;"
                    Font-Names="ARIAL" Font-Size="9pt" TabIndex="27" Text="Con documentazione ex gestore" />
                <br />
                <br />
                <asp:Label ID="Label22" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text="Attenzione: i codici rapporto che iniziano con le cifre 000000, 41, 42 e 43 identificano i rapporti 'virtuali' resisi recessari ai fini della bollettazione 'in fotocopia.'"></asp:Label>
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 660px; position: absolute; top: 524px" TabIndex="26"
        ToolTip="Home" />
    <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 527px; position: absolute; top: 524px" TabIndex="27"
        ToolTip="Avvia Ricerca" />
    <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 102; left: 50px; position: absolute; top: 80px; right: 1065px;"
        TabIndex="-1">Cognome</asp:Label>
    <asp:TextBox ID="txtCognome" TabIndex="1" runat="server" Style="z-index: 103; position: absolute;
        top: 77px; left: 164px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 104; left: 344px; position: absolute; top: 79px" TabIndex="-1">Nome</asp:Label>
    <asp:TextBox ID="txtNome" TabIndex="2" runat="server" Style="z-index: 105; left: 411px;
        position: absolute; top: 77px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 101; left: 50px; position: absolute; top: 108px; height: 14px;
        width: 70px;" TabIndex="-1">Codice Fiscale</asp:Label>
    <asp:TextBox ID="txtCF" TabIndex="3" runat="server" Style="z-index: 107; left: 164px;
        position: absolute; top: 103px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 106; left: 344px; position: absolute; top: 106px" TabIndex="-1">Partita Iva</asp:Label>
    <asp:TextBox ID="txtpiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 107;
        left: 411px; position: absolute; top: 103px" TabIndex="4"></asp:TextBox>
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 186px; height: 14px;"
        Width="100px" TabIndex="-1">Codice C. GIMI</asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 159px" Width="100px"
        TabIndex="-1">Codice Rapporto</asp:Label>
    <asp:TextBox ID="txtCodGIMI" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 182px" TabIndex="7" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:TextBox ID="txtCod" runat="server" Style="z-index: 113; left: 164px; position: absolute;
        top: 156px" TabIndex="6" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 112; left: 50px; position: absolute; top: 212px" Width="100px"
        TabIndex="-1">Codice Unità</asp:Label>
    <asp:TextBox ID="txtUnita" runat="server" BorderStyle="Solid" BorderWidth="1px" Style="z-index: 113;
        left: 164px; position: absolute; top: 207px" TabIndex="8"></asp:TextBox>
    <div id="Indirizzi" style="display: block; border: 1px solid #0000FF; position: absolute;
        width: 349px; width: 296px; background-color: #C0C0C0; top: 70px; left: 448px;
        height: 431px; overflow: auto; z-index: 200;">
        <table style="width: 90%;">
            <tr>
                <td style="text-align: right">
                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Conferma.png"
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
                    <img onclick="javascript:myOpacity.toggle();" alt="" src="../NuoveImm/Img_Conferma.png"
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
