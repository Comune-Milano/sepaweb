<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_nucleo.aspx.vb" Inherits="VSA_com_nucleo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <title>Componenti Nucleo</title>
    <script language="javascript" type="text/javascript">
<!--


        function Button2_onclick() {
            window.close();
        }


        // Funzione javascript per l'inserimento in automatico degli slash nella data
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

// -->
    </script>
    <style type="text/css">
        .CssMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssComuniNazioni
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 166px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssPresenta
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 450px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssFamiAbit
        {
            font-size: 8pt;
            width: 600px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssProv
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 48px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssIndirizzo
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 66px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssLabel
        {
            font-size: 8pt;
            color: black;
            line-height: normal;
            font-style: normal;
            font-family: times;
            height: 20px;
            font-variant: normal;
        }
        .CssLblValori
        {
            font-size: 8pt;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssEtichetta
        {
            alignment: center;
        }
        .CssNuovoMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
    </style>
</head>
<script type="text/javascript" src="Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <form id="form1" runat="server">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <div id="Div1" style="border: 2px solid lightsteelblue; z-index: 191; background-attachment: fixed;
        left: 2px; width: 451px; position: absolute; top: 2px; height: 550px; background-color: lightsteelblue">
        <asp:TextBox ID="txtCognome" runat="server" Columns="35" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
            Style="z-index: 100; left: 103px; position: absolute; top: 39px" TabIndex="1"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 101; left: 21px; position: absolute;
            top: 40px" Width="50px">Cognome</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 21px; position: absolute;
            top: 67px" Width="31px">Nome</asp:Label>
        <p>
            <asp:TextBox ID="txtNome" runat="server" Columns="35" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
                Style="z-index: 103; left: 103px; position: absolute; top: 65px" TabIndex="2"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 104; left: 21px; position: absolute;
                top: 93px" Width="69px">Data Nascita</asp:Label>
            <asp:TextBox ID="txtData" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
                Style="z-index: 105; left: 103px; position: absolute; top: 92px" TabIndex="3"></asp:TextBox>
        </p>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <p>
            &nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                ForeColor="#0000C0" Style="z-index: 106; left: 3px; position: absolute; top: 2px"
                Text="Componente Nucleo" Width="209px"></asp:Label>
            <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 107; left: 291px; position: absolute; top: 40px"
                Text="(valorizzare)" Visible="False" Width="138px"></asp:Label>
        </p>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <p>
            <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 108; left: 281px; position: absolute; top: 67px"
                Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
            <input id="Button2" style="z-index: 129; left: 363px; position: absolute; top: 509px"
                type="button" value="Chiudi" language="javascript" onclick="return Button2_onclick()" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="Button1" runat="server" Style="z-index: 110; left: 212px; position: absolute;
                top: 509px" TabIndex="22" Text="SALVA e Chiudi" />
            <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute;
                top: 121px" Width="71px">Cod. Fiscale</asp:Label>
            <asp:Label ID="lblIndirizzo" runat="server" CssClass="CssLabel" Font-Bold="True"
                Font-Names="Times New Roman" Font-Size="8pt" Height="18px" Style="z-index: 111;
                left: 23px; position: absolute; top: 310px" Width="71px" Visible="False">Indirizzo</asp:Label>
            <asp:Label ID="lblCivico" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 22px; position: absolute;
                top: 335px; bottom: 212px;" Width="71px" Visible="False">Civico</asp:Label>
            <asp:Label ID="lblComune" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 22px; position: absolute;
                top: 360px" Width="71px" Visible="False">Comune</asp:Label>
            <asp:Label ID="lblCap" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 22px; position: absolute;
                top: 386px" Width="71px" Visible="False">CAP</asp:Label>
            <asp:Label ID="lblDocIden" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 20px; position: absolute;
                top: 409px; width: 83px;" Visible="False">Doc. Identità N.</asp:Label>
            <asp:Label ID="lblRilasciata" runat="server" CssClass="CssLabel" Font-Bold="True"
                Font-Names="Times New Roman" Font-Size="8pt" Height="18px" Style="z-index: 111;
                left: 21px; position: absolute; top: 434px; width: 83px; bottom: 113px;" Visible="False">Rilasciata da</asp:Label>
            <asp:Label ID="lblPermSogg" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 20px; position: absolute;
                top: 459px; width: 148px;" Visible="False">Perm. di Sogg.</asp:Label>
            <asp:Label ID="lblDataIdent" runat="server" CssClass="CssLabel" Font-Bold="True"
                Font-Names="Times New Roman" Font-Size="8pt" Height="18px" Style="z-index: 111;
                left: 208px; position: absolute; top: 407px; width: 22px;" Visible="False">Data</asp:Label>
            <asp:Label ID="lblDataSogg" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 209px; position: absolute;
                top: 459px; width: 23px;" Visible="False">Data</asp:Label>
            <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 112; left: 21px; position: absolute;
                top: 148px" Width="71px">Gr. Parentela</asp:Label>
            <img id="imgVerifica" style="position: absolute; top: 121px; left: 228px; cursor: pointer"
                alt="Verifica Componente" onclick="document.getElementById('verifica').value=1;ApriCorrelazioni();"
                src="../NuoveImm/binocolo.png" width="20px" />
            <asp:TextBox ID="txtCF" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
                Style="z-index: 113; left: 103px; position: absolute; top: 120px" TabIndex="4"></asp:TextBox>
            <asp:TextBox ID="txtVia" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="30"
                Style="z-index: 113; left: 203px; position: absolute; top: 307px; width: 214px;"
                TabIndex="13" ToolTip="Inserire l'indirizzo di residenza" Visible="False"></asp:TextBox>
            <asp:DropDownList ID="cmbTipoVia" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
                left: 101px; position: absolute; top: 306px" TabIndex="12" Width="90px" 
                Visible="False">
            </asp:DropDownList>
            <asp:TextBox ID="txtCivico" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
                Style="z-index: 113; left: 100px; position: absolute; top: 333px; width: 88px;
                bottom: 216px;" TabIndex="14" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtComune" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
                Style="z-index: 113; left: 100px; position: absolute; top: 358px; width: 88px;"
                TabIndex="15" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtCap" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
                Style="z-index: 113; left: 100px; position: absolute; top: 382px; width: 88px;
                right: 748px;" TabIndex="16" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtDocIdent" runat="server" Columns="22" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="16" Style="z-index: 113; left: 100px; position: absolute; top: 406px;
                width: 88px;" TabIndex="17" ToolTip="Inserire numero carta d'identità" 
                Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtRilasciata" runat="server" Columns="22" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="16" Style="z-index: 113; left: 101px; position: absolute; top: 432px;
                width: 88px;" TabIndex="19" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtPermSogg" runat="server" Columns="22" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="16" Style="z-index: 113; left: 101px; position: absolute; top: 458px;
                width: 88px;" TabIndex="20" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtDataDocI" runat="server" Columns="10" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="10" Style="z-index: 105; left: 239px; position: absolute; top: 406px;
                width: 70px;" TabIndex="18" ToolTip="Inserire la data di rilascio del doc. d'identità"
                Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtDataPermSogg" runat="server" Columns="10" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="10" Style="z-index: 105; left: 239px; position: absolute; top: 458px;
                width: 70px;" TabIndex="21" ToolTip="Inserire la data del permesso di soggiorno"
                Visible="False"></asp:TextBox>
            <asp:DropDownList ID="cmbParenti" runat="server" CssClass="CssMaiuscolo" Style="z-index: 114;
                left: 103px; position: absolute; top: 146px" TabIndex="5" Width="316px">
            </asp:DropDownList>
            <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 115; left: 21px; position: absolute;
                top: 175px" Width="69px">% Invalidità</asp:Label>
            <asp:TextBox ID="txtInv" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" 
                MaxLength="6" Style="z-index: 116;
                left: 103px; position: absolute; top: 176px" TabIndex="6"></asp:TextBox>
            <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 117; left: 24px; position: absolute;
                top: 203px" Width="69px">ASL</asp:Label>
            <asp:TextBox ID="txtASL" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" 
                MaxLength="5" Style="z-index: 118;
                left: 103px; position: absolute; top: 201px" TabIndex="7"></asp:TextBox>
            <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 119; left: 22px; position: absolute;
                top: 228px" Width="71px">Ind. Accomp.</asp:Label>
            <asp:DropDownList ID="cmbAcc" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
                left: 103px; position: absolute; top: 225px" TabIndex="8" Width="49px">
            </asp:DropDownList>
            <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 119; left: 22px; position: absolute;
                top: 256px" Width="71px">Nuovo Comp.</asp:Label>
            <asp:DropDownList ID="cmbNuovoComp" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
                left: 103px; position: absolute; top: 254px" TabIndex="9" Width="49px" AutoPostBack="True"
                ControlID="cmbNuovoComp">
                <asp:ListItem Value="-1">--</asp:ListItem>
                <asp:ListItem Value="0">NO</asp:ListItem>
                <asp:ListItem Value="1">SI</asp:ListItem>
            </asp:DropDownList>
            <%--                <asp:ListItem Value="1">CAPOFAMIGLIA</asp:ListItem>
                <asp:ListItem Value="2">CONIUGE</asp:ListItem>
                <asp:ListItem Value="3">FIGLIO/A</asp:ListItem>
                <asp:ListItem Value="4">GENITORE</asp:ListItem>
                <asp:ListItem Value="5">FRATELLO/SORELLA</asp:ListItem>
                <asp:ListItem Value="6">NIPOTE</asp:ListItem>
                <asp:ListItem Value="7">NIPOTE COLLATERALE</asp:ListItem>
                <asp:ListItem Value="8">NIPOTE AFFINE</asp:ListItem>
                <asp:ListItem Value="9">ZIO/A</asp:ListItem>
                <asp:ListItem Value="10">CUGINO/A</asp:ListItem>
                <asp:ListItem Value="11">NUORA/GENERO</asp:ListItem>
                <asp:ListItem Value="12">SUOCERO/A</asp:ListItem>
                <asp:ListItem Value="13">COGNATO/A</asp:ListItem>
                <asp:ListItem Value="14">BISCUGINO/A</asp:ListItem>
                <asp:ListItem Value="15">ALTRO AFFINE</asp:ListItem>
                <asp:ListItem Value="16">CONVIVENTE</asp:ListItem>
                <asp:ListItem Value="17">NUBENDO/A</asp:ListItem>
                <asp:ListItem Value="18">ALTRO PARENTE</asp:ListItem>
                <asp:ListItem Value="19">NONNO/A</asp:ListItem>
                <asp:ListItem Value="20">BISNONNO/A</asp:ListItem>
                <asp:ListItem Value="21">FIGLIASTRO/A</asp:ListItem>
                <asp:ListItem Value="22">PATRIGNO/MATRIGNA</asp:ListItem>
                <asp:ListItem Value="23">FRATELLASTRO/SORELLASTRA</asp:ListItem>
                <asp:ListItem Value="24">ZIO/A AFFINE</asp:ListItem>
                <asp:ListItem Value="25">PRONIPOTE</asp:ListItem>
            --%>
            <asp:Label ID="lblDataIngr" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 119; left: 22px; position: absolute;
                top: 284px" Visible="False">Data ingresso</asp:Label>
            <asp:TextBox ID="txtDataIngr" runat="server" Columns="22" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="16" Style="z-index: 113; left: 102px; position: absolute; top: 282px;
                width: 70px;" TabIndex="11" Visible="False"></asp:TextBox>
            <asp:CheckBox ID="chkReferente" runat="server" Style="z-index: 113; left: 275px;
                position: absolute; top: 254px;" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" TabIndex="10" Visible="False" Text="Referente" />
            <asp:Label ID="lblNuovoComp" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 125; left: 164px; position: absolute; top: 257px"
                Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="lblerroreData" runat="server" Font-Bold="True" Font-Names="ARIAL"
                Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 194px; position: absolute;
                top: 283px" Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="LBLdataDoc" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 121; left: 330px; position: absolute; top: 405px"
                Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="LBLdataPerm" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 121; left: 329px; position: absolute; top: 459px"
                Text="(valorizzare)" Visible="False"></asp:Label>
            <%--                <asp:ListItem Value="1">CAPOFAMIGLIA</asp:ListItem>
                <asp:ListItem Value="2">CONIUGE</asp:ListItem>
                <asp:ListItem Value="3">FIGLIO/A</asp:ListItem>
                <asp:ListItem Value="4">GENITORE</asp:ListItem>
                <asp:ListItem Value="5">FRATELLO/SORELLA</asp:ListItem>
                <asp:ListItem Value="6">NIPOTE</asp:ListItem>
                <asp:ListItem Value="7">NIPOTE COLLATERALE</asp:ListItem>
                <asp:ListItem Value="8">NIPOTE AFFINE</asp:ListItem>
                <asp:ListItem Value="9">ZIO/A</asp:ListItem>
                <asp:ListItem Value="10">CUGINO/A</asp:ListItem>
                <asp:ListItem Value="11">NUORA/GENERO</asp:ListItem>
                <asp:ListItem Value="12">SUOCERO/A</asp:ListItem>
                <asp:ListItem Value="13">COGNATO/A</asp:ListItem>
                <asp:ListItem Value="14">BISCUGINO/A</asp:ListItem>
                <asp:ListItem Value="15">ALTRO AFFINE</asp:ListItem>
                <asp:ListItem Value="16">CONVIVENTE</asp:ListItem>
                <asp:ListItem Value="17">NUBENDO/A</asp:ListItem>
                <asp:ListItem Value="18">ALTRO PARENTE</asp:ListItem>
                <asp:ListItem Value="19">NONNO/A</asp:ListItem>
                <asp:ListItem Value="20">BISNONNO/A</asp:ListItem>
                <asp:ListItem Value="21">FIGLIASTRO/A</asp:ListItem>
                <asp:ListItem Value="22">PATRIGNO/MATRIGNA</asp:ListItem>
                <asp:ListItem Value="23">FRATELLASTRO/SORELLASTRA</asp:ListItem>
                <asp:ListItem Value="24">ZIO/A AFFINE</asp:ListItem>
                <asp:ListItem Value="25">PRONIPOTE</asp:ListItem>
            --%>
            </asp:UpdatePanel>
            <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 121; left: 180px; position: absolute; top: 93px"
                Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
            <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 122; left: 264px; position: absolute; top: 122px;
                height: 14px; width: 177px;" Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 123; left: 144px; position: absolute; top: 176px"
                Text="(valorizzare)" Visible="False" Width="243px"></asp:Label>
            <asp:Label ID="L6" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 124; left: 143px; position: absolute; top: 201px"
                Text="(valorizzare)" Visible="False" Width="250px"></asp:Label>
            <asp:Label ID="L7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 125; left: 164px; position: absolute; top: 227px"
                Text="(valorizzare)" Visible="False" Width="248px"></asp:Label>
            <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="6" Style="left: 252px; position: absolute; top: 521px" TabIndex="3"
                Width="17px" Height="12px"></asp:TextBox>
            <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 283px;
                position: absolute; top: 521px" TabIndex="3" Width="11px" Height="12px"></asp:TextBox>
            <asp:TextBox ID="txtProgr" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 308px;
                position: absolute; top: 521px" TabIndex="3" Width="11px" Height="11px"></asp:TextBox>
        </p>
    </div>
    <asp:HiddenField ID="verifica" runat="server" Value="0" />
    <asp:HiddenField ID="iddich" runat="server" />
    </form>
</body>
<script type="text/javascript">
    document.getElementById('txtRiga').style.visibility = 'hidden';
    document.getElementById('txtProgr').style.visibility = 'hidden';
    document.getElementById('txtOperazione').style.visibility = 'hidden';

    function ApriCorrelazioni() {
        window.open('ControllaComponenti.aspx?CF=' + document.getElementById('txtCF').value + '&IDDICH=' + document.getElementById('iddich').value, 'ControllaComponente', 'top=320,left=420,width=480,height=310');

    }
</script>
</html>
