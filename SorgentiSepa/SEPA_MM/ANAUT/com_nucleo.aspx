<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_nucleo.aspx.vb" Inherits="ANAUT_com_nucleo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Componenti Nucleo</title>
    <script language="javascript" type="text/javascript">
<!--
        function Chiudi() {
            document.getElementById('txtModificato').value = '0';
            window.close();
        }

// -->
    </script>
    <link href="Styles/StileAU.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CssMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
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
            font-family: Arial;
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
            height: 20px;
            font-variant: normal;
        }
    </style>
</head>
<script type="text/javascript" src="Funzioni.js"></script>
<body style="background-image: url('../NuoveImm/SfondoMascheraRubrica.jpg'); background-repeat: no-repeat;
    width: 400px;">
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="txtCognome" runat="server" Columns="35" CssClass="CssMaiuscolo"
        Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
        Style="z-index: 100; left: 103px; position: absolute; top: 51px; width: 240px;"
        TabIndex="1"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial"
        Font-Size="8pt" Height="18px" Style="z-index: 101; left: 21px; position: absolute;
        top: 55px" Width="50px">Cognome</asp:Label>
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 102; left: 21px; position: absolute; top: 82px" Width="31px">Nome</asp:Label>
    <p>
        <asp:TextBox ID="txtNome" runat="server" Columns="35" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="50"
            Style="z-index: 103; left: 103px; position: absolute; top: 77px; width: 240px;"
            TabIndex="2"></asp:TextBox>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
            Style="z-index: 104; left: 21px; position: absolute; top: 108px" Width="69px">Data Nascita</asp:Label>
        <asp:TextBox ID="txtData" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="10"
            Style="z-index: 105; left: 103px; position: absolute; top: 103px" TabIndex="3"></asp:TextBox>
    </p>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <p>
        &nbsp;&nbsp;
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="Maroon" Style="z-index: 106; left: 21px; position: absolute; top: 15px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 107; left: 371px; position: absolute; top: 54px; height: 15px; width: 78px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
    </p>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    <p>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 108; left: 371px; position: absolute; top: 79px"
            Text="(valorizzare)" Visible="False" Width="100px"></asp:Label>
        <input id="Button2" style="z-index: 129; left: 371px; position: absolute; top: 422px;
            width: 76px;" type="button" value="Chiudi" onclick="ConfermaEsci();" 
            class="bottone" />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Button ID="Button1" runat="server" Style="z-index: 110; left: 233px; position: absolute;
            top: 422px; width: 130px;" TabIndex="15" Text="SALVA e Chiudi" 
            CssClass="bottone" />
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
            Style="z-index: 111; left: 21px; position: absolute; top: 136px" Width="71px">Cod. Fiscale</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 112; left: 21px; position: absolute; top: 164px"
            Width="71px">Gr. Parentela</asp:Label>
        <asp:TextBox ID="txtCF" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
            
            Style="z-index: 113; left: 103px; position: absolute; top: 131px; width: 133px;" 
            TabIndex="4"></asp:TextBox>
        <asp:DropDownList ID="cmbParenti" runat="server" CssClass="CssMaiuscolo" Style="z-index: 114;
            left: 103px; position: absolute; top: 159px" TabIndex="5" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 115; left: 21px; position: absolute; top: 191px"
            Width="69px">% Invalidità</asp:Label>
        <asp:TextBox ID="txtInv" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="z-index: 116;
            left: 103px; position: absolute; top: 186px" TabIndex="6"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 117; left: 21px; position: absolute; top: 272px"
            Width="69px">ASL</asp:Label>
        <asp:TextBox ID="txtASL" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 118;
            left: 103px; position: absolute; top: 265px" TabIndex="9"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 119; left: 21px; position: absolute; top: 296px;
            right: 1374px;" Width="71px">Ind. Accomp.</asp:Label>
        <asp:DropDownList ID="cmbAcc" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 103px; position: absolute; top: 291px" TabIndex="10" Width="49px">
        </asp:DropDownList>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 119; left: 22px; position: absolute; top: 217px;
            right: 1373px;" Width="71px">Tipo Inval.</asp:Label>
        <asp:DropDownList ID="cmbTipoInval" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 103px; position: absolute; top: 212px" TabIndex="7" Width="180px">
            <asp:ListItem Value="-1">---</asp:ListItem>
            <asp:ListItem Value="P" Text="Provvisoria"></asp:ListItem>
            <asp:ListItem Value="D" Text="Definitiva"></asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 119; left: 21px; position: absolute; top: 244px;
            right: 1374px;" Width="71px">Natura Inval.</asp:Label>
        <asp:DropDownList ID="cmbNaturaInval" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 103px; position: absolute; top: 239px" TabIndex="8" Width="180px">
            <asp:ListItem Value="-1">---</asp:ListItem>
            <asp:ListItem Value="MC" Text="Motoria con carrozzella"></asp:ListItem>
            <asp:ListItem Value="MS" Text="Motoria senza carrozzella"></asp:ListItem>
            <asp:ListItem Value="S" Text="Schizofrenia"></asp:ListItem>
            <asp:ListItem Value="P" Text="Psichica"></asp:ListItem>
            <asp:ListItem Value="G" Text="Generica"></asp:ListItem>
            <asp:ListItem Value="SM">Sordomutismo</asp:ListItem>
            <asp:ListItem Value="CC">Cecita</asp:ListItem>
            <asp:ListItem Value="8">Handicap grave ex art3 .L.104/1992</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 121; left: 180px; position: absolute; top: 105px"
            Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
        <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 122; left: 274px; position: absolute; top: 133px; height: 16px; width: 139px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 160px; position: absolute; top: 188px"
            Text="(valorizzare)" Visible="False" Width="243px"></asp:Label>
        <asp:Label ID="LTipoInval" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 290px; position: absolute; top: 214px"
            Text="(valorizzare)" Visible="False" Width="243px"></asp:Label>
        <asp:Label ID="LNaturaInval" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 290px; position: absolute; top: 241px; width: 91px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:CheckBox ID="chkL104" runat="server" 
            style="position:absolute; top: 236px; left: 364px; width: 96px;" 
            Font-Names="arial" Font-Size="8pt" Text="Legge104"/>
        <asp:Label ID="L6" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 124; left: 160px; position: absolute; top: 267px"
            Text="(valorizzare)" Visible="False" Width="250px"></asp:Label>
        <asp:Label ID="L7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 125; left: 160px; position: absolute; top: 293px"
            Text="(valorizzare)" Visible="False" Width="248px"></asp:Label>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="6" Style="left: 252px; position: absolute; top: 311px" TabIndex="3"
            Width="17px" Height="12px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 288px;
            position: absolute; top: 312px" TabIndex="3" Width="11px" Height="12px"></asp:TextBox>
        <asp:TextBox ID="txtProgr" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 313px;
            position: absolute; top: 309px" TabIndex="3" Width="11px" Height="11px"></asp:TextBox>
        <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 117; left: 21px; position: absolute; top: 323px;
            width: 84px;">Telefono 1</asp:Label>
        <asp:TextBox ID="txtTelefono1" runat="server" Columns="22" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="20" Style="z-index: 113; left: 103px; position: absolute; top: 320px; width: 110px;"
            TabIndex="11"></asp:TextBox>
        <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 117; left: 231px; position: absolute; top: 323px;
            width: 84px;">Telefono 2</asp:Label>
            <asp:TextBox ID="txtTelefono2" runat="server" Columns="22" 
            CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
            Style="z-index: 113; left: 297px; position: absolute; top: 320px; width: 110px;" 
            TabIndex="12"></asp:TextBox>
            <asp:Label ID="Label14" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 117; left: 21px; position: absolute; top: 351px;
            width: 84px;">Email 1</asp:Label>
            <asp:TextBox ID="txtmail1" runat="server" Columns="22" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="100" Style="z-index: 113; left: 103px; position: absolute; top: 348px; width: 303px;"
            TabIndex="13"></asp:TextBox>
            <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Size="8pt"
            Height="18px" Style="z-index: 117; left: 21px; position: absolute; top: 378px;
            width: 84px;">Email 2</asp:Label>
            <asp:TextBox ID="txtmail2" runat="server" Columns="22" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="100" Style="z-index: 113; left: 103px; position: absolute; top: 375px; width: 303px;"
            TabIndex="14"></asp:TextBox>
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    </p>
    <asp:HiddenField ID="salvaComponente" runat="server" Value="0" />
    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtProgr').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';

        var opener = window.dialogArguments;
        window.opener.document.getElementById('caric').style.visibility = 'hidden';

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }
        function CloseModal2(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }
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

        function ConfermaEsci() {

            if ((document.getElementById('txtModificato').value == '1') || (document.getElementById('txtModificato').value == '111')) {

                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.\nUscire senza salvare causerà la perdita delle modifiche!\nUscire ugualmente? Per non uscire premere ANNULLA.");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                }
                else {
                    if (document.getElementById('caric')) {
                        document.getElementById('caric').style.visibility = 'visible';

                    }
                    Chiudi();
                }
            }
            else {
                if (document.getElementById('caric')) {
                    document.getElementById('caric').style.visibility = 'visible';

                }
                Chiudi();
            }
        }
    </script>
    
    </form>
</body>
</html>
