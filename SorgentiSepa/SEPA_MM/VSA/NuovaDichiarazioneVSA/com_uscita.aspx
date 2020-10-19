<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_uscita.aspx.vb" Inherits="VSA_com_uscita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Eliminazione Componente</title>
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
<script type="text/javascript" src="../Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <div id="Div1" style="border: 2px solid lightsteelblue; z-index: 191; background-attachment: fixed;
        left: 2px; width: 462px; position: absolute; top: 2px; height: 350px; background-color: lightsteelblue">
        <asp:TextBox ID="txtCognome" runat="server" Columns="35" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="TIMES" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
            Style="z-index: 100; left: 103px; position: absolute; top: 39px" TabIndex="1"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 101; left: 21px; position: absolute;
            top: 40px" Width="50px">Cognome</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 21px; position: absolute;
            top: 66px" Width="31px">Nome</asp:Label>
        <p>
            <asp:TextBox ID="txtNome" runat="server" Columns="35" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
                Style="z-index: 103; left: 103px; position: absolute; top: 65px" TabIndex="2"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 104; left: 21px; position: absolute;
                top: 93px" Width="69px">Data Nascita</asp:Label>
            <asp:TextBox ID="txtDataNasc" runat="server" Columns="10" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="10" Style="z-index: 105; left: 103px; position: absolute; top: 92px"
                TabIndex="3"></asp:TextBox>
        </p>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <p>
            &nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                ForeColor="#0000C0" Style="z-index: 106; left: 3px; position: absolute; top: 2px"
                Text="Eliminazione Componente" Width="209px"></asp:Label>
            <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 107; left: 291px; position: absolute; top: 40px"
                Text="(valorizzare)" Visible="False" Width="138px"></asp:Label>
        </p>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <p>
            <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 108; left: 281px; position: absolute; top: 67px"
                Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
            <input id="btnChiudi" style="z-index: 129; left: 336px; position: absolute; top: 258px"
                type="button" value="Chiudi" language="javascript" onclick="return Button2_onclick()" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnSalva" runat="server" Style="z-index: 110; left: 179px; position: absolute;
                top: 258px; width: 136px;" TabIndex="7" Text="SALVA e Chiudi" />
            <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute;
                top: 121px" Width="71px">Cod. Fiscale</asp:Label>
            <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
                Font-Size="8pt" Height="18px" Style="z-index: 112; left: 21px; position: absolute;
                top: 149px" Width="71px">Motivo Uscita</asp:Label>
            <asp:TextBox ID="txtCF" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
                Style="z-index: 113; left: 102px; position: absolute; top: 120px" TabIndex="4"
                Width="172px"></asp:TextBox>
            <asp:DropDownList ID="cmbMotivoUscita" runat="server" Style="z-index: 114; left: 103px;
                position: absolute; top: 148px; text-transform: uppercase;" TabIndex="5" Width="280px"
                Font-Names="arial" Font-Size="8pt" ForeColor="Blue">
                <asp:ListItem Value="-1">- seleziona -</asp:ListItem>
                <asp:ListItem Value="1">DECESSO</asp:ListItem>
                <asp:ListItem Value="2">SEPARAZIONE, NULLITA&#39;, SCIOGLIMENTO MATRIMONIO</asp:ListItem>
                <asp:ListItem Value="3">TRASFERIMENTO</asp:ListItem>
                <asp:ListItem Value="4">ALTRO</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="lblDataUscita" runat="server" CssClass="CssLabel" Font-Bold="True"
                Font-Names="Times New Roman" Font-Size="8pt" Height="18px" Style="z-index: 119;
                left: 21px; position: absolute; top: 179px">Data Uscita</asp:Label>
            <asp:Label ID="lblEliminati" runat="server" CssClass="CssLabel" Font-Bold="True"
                Font-Names="Times New Roman" Font-Size="8pt" Height="18px" Style="z-index: 119;
                left: 21px; position: absolute; top: 220px"></asp:Label>
            <asp:TextBox ID="txtDataUscita" runat="server" Columns="22" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="16" Style="z-index: 113; left: 103px; position: absolute; top: 177px;
                width: 70px;" TabIndex="6"></asp:TextBox>
            <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 121; left: 180px; position: absolute; top: 93px"
                Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
            <asp:Label ID="lblValorizzaUsc" runat="server" Font-Bold="True" Font-Names="ARIAL"
                Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 193px; position: absolute;
                top: 178px" Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
            <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 122; left: 294px; position: absolute; top: 122px"
                Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
            <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 122; left: 390px; position: absolute; top: 150px;
                height: 14px; width: 85px;" Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
                Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
                MaxLength="6" Style="left: 257px; position: absolute; top: 265px" TabIndex="3"
                Width="17px" Height="12px"></asp:TextBox>
            <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 272px;
                position: absolute; top: 264px; right: 159px;" TabIndex="3" Width="11px" Height="12px"></asp:TextBox>
            <asp:TextBox ID="txtProgr" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
                Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 291px;
                position: absolute; top: 265px" TabIndex="3" Width="11px" Height="11px"></asp:TextBox>
        </p>
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        <asp:HiddenField ID="salvaCompElimina" runat="server" Value="0" />
    </div>
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
