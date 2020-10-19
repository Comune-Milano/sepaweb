<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_spese.aspx.vb" Inherits="ANAUT_com_spese" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>Spese componenti Nucleo</title>
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
            font-family: arial;
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
            height: 20px;
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
        <asp:TextBox ID="txtDescrizione" runat="server" Columns="30" CssClass="CssMaiuscolo"
            Font-Bold="False" ForeColor="Blue" MaxLength="17"
            
            Style="z-index: 100; left: 86px; position: absolute; top: 92px; width: 367px;" 
            TabIndex="1"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 101; left: 12px; position: absolute;
            top: 97px" Width="50px">Descrizione</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 13px; position: absolute;
            top: 129px; width: 53px;">Importo</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 143px; position: absolute;
            top: 126px" Width="31px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server" Columns="35" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue"
            MaxLength="6" Style="z-index: 104; left: 86px; position: absolute; top: 124px"
            TabIndex="2" Width="48px"></asp:TextBox>
        <asp:Label ID="txtComponente" runat="server" CssClass="CssLabel" 
            Font-Bold="True" Font-Size="8pt" Height="18px" Style="z-index: 105;
            left: 11px; position: absolute; top: 58px; width: 461px;"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="Maroon" Style="z-index: 106; left: 11px; position: absolute; top: 17px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 107; left: 164px; position: absolute; top: 125px"
            Text="(valorizzare)" Visible="False" Width="222px"></asp:Label>
        &nbsp;
        <asp:Button ID="Button1" runat="server" Style="z-index: 110; left: 233px; position: absolute;
            top: 353px; width: 130px;" TabIndex="11" Text="SALVA e Chiudi" 
            CssClass="bottone"/>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="6" Style="left: 231px; position: absolute; top: 152px" TabIndex="3"
            Width="5px" Height="12px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 289px;
            position: absolute; top: 155px" TabIndex="3" Width="5px" Height="10px"></asp:TextBox>
        <input id="Button2" language="javascript" onclick="ConfermaEsci();" style="z-index: 111;
            left: 367px; position: absolute; top: 351px" type="button" value="Chiudi" 
            class="bottone" />
    <asp:HiddenField ID="salvaSpese" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';
        var opener = window.dialogArguments;
        window.opener.document.getElementById('caric').style.visibility = 'hidden';
        
        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }

        function Chiudi() {
            window.close();
        }
        
        function ConfermaEsci() {

            if (document.getElementById('txtModificato').value == '1') {

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
