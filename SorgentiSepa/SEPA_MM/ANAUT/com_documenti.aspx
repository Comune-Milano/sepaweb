<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_documenti.aspx.vb" Inherits="ANAUT_com_documenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <title>Documentazione</title>
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
        <input id="Button2" onclick="javascript:ConfermaEsci();" style="z-index: 129; left: 371px;
            position: absolute; top: 353px; width: 76px;" type="button" value="Chiudi" 
            class="bottone" />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="Maroon" Style="z-index: 100; left: 13px; position: absolute; top: 15px"
            Text="Documentazione Mancante" Width="209px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 110; left: 233px; position: absolute;
            top: 353px; width: 130px;" TabIndex="4" Text="SALVA e Chiudi" 
            CssClass="bottone" />
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="arial"
            Font-Size="8pt" Style="z-index: 102; left: 12px; position: absolute; top: 92px;
            width: 473px; height: 31px;">ATTENZIONE...Selezionare un componente dalla lista se si desidera specificare che il tipo di documento scelto è riferito al singolo individuo.</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 12px; position: absolute;
            top: 141px" Width="50px">Componente</asp:Label>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 12px; position: absolute;
            top: 182px; width: 85px;">Note</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 11px; position: absolute;
            top: 61px; width: 96px;">Tipo Documento</asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server" CssClass="CssMaiuscolo" Style="z-index: 104;
            left: 103px; position: absolute; top: 58px" TabIndex="1" Width="385px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 102px; position: absolute; top: 139px" TabIndex="2" Width="385px">
        </asp:DropDownList>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="12px" MaxLength="6" Style="left: 443px; position: absolute; top: 222px"
            TabIndex="3" Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 348px; position: absolute; top: 222px" TabIndex="3" Width="5px"></asp:TextBox>
    </div>
    <asp:TextBox ID="txtDescr" runat="server" TextMode="MultiLine" Style="position: absolute;
        top: 183px; left: 102px; width: 381px; height: 44px;" TabIndex="3"></asp:TextBox>
    <asp:HiddenField ID="salvaDocumenti" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';

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
