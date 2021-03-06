﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonio.aspx.vb"
    Inherits="ANAUT_com_patrimonio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title>Patrimonio Mobiliare Componenti</title>
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
    <div>
        <input id="Button2" onclick="javascript:ConfermaEsci();" style="z-index: 129;
            left: 371px; position: absolute; top: 353px; width: 76px;" type="button" value="Chiudi" 
            class="bottone" />
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
            top: 60px" Width="50px">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 101;
            left: 88px; position: absolute; top: 59px" TabIndex="1" Width="380px">
        </asp:DropDownList>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="Maroon" Style="z-index: 102; left: 16px; position: absolute; top: 18px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        &nbsp;
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 13px; position: absolute;
            top: 91px" Width="69px">Codice</asp:Label>
        <asp:TextBox ID="txtABI" runat="server" Columns="5" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="27"
            Style="z-index: 105; left: 88px; position: absolute; top: 88px" TabIndex="2"
            Width="239px"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 108; left: 339px; position: absolute; top: 93px"
            Text="(valorizzare)" Visible="False" Width="113px"></asp:Label>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 109; left: 13px; position: absolute;
            top: 125px" Width="71px">Intermediario</asp:Label>
        <asp:TextBox ID="txtInter" runat="server" Columns="50" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="16"
            Style="z-index: 110; left: 88px; position: absolute; top: 123px" TabIndex="3"
            Width="240px"></asp:TextBox>
        <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 338px; position: absolute; top: 124px"
            Text="(valorizzare)" Visible="False"></asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 181px; position: absolute;
            top: 188px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server" Columns="8" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 115;
            left: 106px; position: absolute; top: 183px" TabIndex="5"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 116; left: 13px; position: absolute;
            top: 184px; width: 88px;">Importo Totale</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 13px; position: absolute;
            top: 156px" Width="83px">Tipo Patrim.</asp:Label>
        <asp:DropDownList ID="cmbTipoPatrim" runat="server" CssClass="CssMaiuscolo" Style="z-index: 104;
            left: 88px; position: absolute; top: 152px" TabIndex="4" Width="380px">
        </asp:DropDownList>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 117; left: 219px; position: absolute; top: 185px; width: 202px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 110; left: 233px; position: absolute;
            top: 353px; width: 130px;" TabIndex="6" Text="SALVA e Chiudi" 
            CssClass="bottone" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="6" Style="left: 149px; position: absolute; top: 317px" TabIndex="3"
            Width="5px" Height="1px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" 
            MaxLength="6" Style="left: 105px;
            position: absolute; top: 304px" TabIndex="3" Width="5px" Height="13px"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 217px; width: 95px;">Num. Intestatari</asp:Label>
            <asp:TextBox ID="txtPerc" runat="server" Columns="4" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="1" Style="z-index: 109;
            left: 106px; position: absolute; top: 213px; width: 31px;" TabIndex="3">1</asp:TextBox>
            <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 162px; position: absolute; top: 215px; width: 119px;"
            Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    </div>
    <asp:HiddenField ID="salvaPatrMob" runat="server" Value="0" />
    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';

        var opener = window.dialogArguments;
        window.opener.document.getElementById('caric').style.visibility = 'hidden';


        function Chiudi() {
            //            CloseModal2(document.getElementById('txtModificato').value);
            document.getElementById('txtModificato').value = '0';
            window.close();
        }

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
