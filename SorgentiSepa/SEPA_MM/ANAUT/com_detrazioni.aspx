<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_detrazioni.aspx.vb"
    Inherits="ANAUT_com_detrazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
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
    <title>Detrazioni</title>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js2/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="js2/jquery-ui-1.9.0.custom.js"></script>
    <script type="text/javascript" src="js2/jquery-ui-1.9.0.custom.min.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <script type="text/javascript" src="../Standard/Scripts/jsFunzioni.js"></script>
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
            left: 371px; position: absolute; top: 353px; width: 76px;" type="button" 
            value="Chiudi" class="bottone" />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="Maroon" Style="z-index: 100; left: 13px; position: absolute; top: 16px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 110; left: 233px; position: absolute;
            top: 353px; width: 130px;" TabIndex="4" Text="SALVA e Chiudi" 
            CssClass="bottone" />
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 12px; position: absolute;
            top: 57px" Width="50px">Componente</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 13px; position: absolute;
            top: 87px; width: 95px;">Tipo Detrazione</asp:Label>
        <asp:DropDownList ID="cmbDetrazione" runat="server" CssClass="CssMaiuscolo" Style="z-index: 104;
            left: 116px; position: absolute; top: 85px" TabIndex="2" Width="311px">
            <%--<asp:ListItem Value="0">IRPEF</asp:ListItem>
            <asp:ListItem Value="1">Spese Sanitarie</asp:ListItem>
            <asp:ListItem Value="2">Ricovero in strut. sociosanitarie</asp:ListItem>--%>
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 115px; position: absolute; top: 55px" TabIndex="1" Width="311px">
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 183px; position: absolute;
            top: 117px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtImporto" runat="server" Columns="8" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 107;
            left: 116px; position: absolute; top: 115px" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 14px; position: absolute;
            top: 116px" Width="71px">Importo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 109; left: 212px; position: absolute; top: 117px"
            Text="(valorizzare)" Visible="False" Width="214px"></asp:Label>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="12px" MaxLength="6" Style="left: 313px; position: absolute; top: 163px; right: 1063px;"
            TabIndex="3" Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 232px; position: absolute; top: 163px" TabIndex="3" Width="5px"></asp:TextBox>
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    </div>
    <asp:HiddenField ID="salvaDetrazioni" runat="server" Value="0" />
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
