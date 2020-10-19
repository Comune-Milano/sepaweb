<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_ospiti.aspx.vb" Inherits="VSA_com_nucleo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <title>Componenti Nucleo</title>
    <script language="javascript" type="text/javascript">
<!--

        function Chiudi() {

            document.getElementById('txtModificato').value = '0';
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
<script type="text/javascript" src="../Funzioni.js"></script>
<body style="background-image: url('../../NuoveImm/SfondoMascheraRubrica.jpg'); background-repeat: no-repeat; width: 400px;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="iddom" runat="server" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <div id="Div1" style="z-index: 191; background-attachment: fixed; left: 12px; width: 439px; position: absolute; top: 18px; height: 444px;">
        <asp:TextBox ID="txtCognome" runat="server" Columns="35" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="25" Style="z-index: 100; left: 103px; position: absolute; top: 39px" TabIndex="1"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 101; left: 21px; position: absolute; top: 40px" Width="50px">Cognome</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 102; left: 21px; position: absolute; top: 66px" Width="31px">Nome</asp:Label>
        <p>
            <asp:TextBox ID="txtNome" runat="server" Columns="35" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="25" Style="z-index: 103; left: 103px; position: absolute; top: 65px" TabIndex="2"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 104; left: 21px; position: absolute; top: 93px" Width="69px">Data Nascita</asp:Label>
            <asp:TextBox ID="txtData" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 105; left: 103px; position: absolute; top: 92px" TabIndex="3"></asp:TextBox>
            <asp:CheckBox ID="chkReferente" runat="server" Style="z-index: 113; left: 100px; position: absolute; top: 1px;" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" TabIndex="20" Text="Referente" />
        </p>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <p>
            &nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt" ForeColor="Maroon" Style="z-index: 106; left: 3px; position: absolute; top: 2px" Text="Ospiti" Width="209px"></asp:Label>
            <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 107; left: 330px; position: absolute; top: 40px" Text="(valorizzare)" Visible="False" Width="138px"></asp:Label>
        </p>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <p>
            <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 108; left: 330px; position: absolute; top: 67px" Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
            <input id="btnChiudi" class="bottone" style="z-index: 129; left: 335px; position: absolute; width: 76px; top: 400px" type="button" value="Chiudi" language="javascript" onclick="ConfermaEsci()" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="btnSalva" runat="server" CssClass="bottone" Style="z-index: 110; left: 188px; width: 130px; position: absolute; top: 400px" TabIndex="9" Text="SALVA e Chiudi" />
            <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute; top: 121px" Width="71px">Cod. Fiscale</asp:Label>
            <asp:Label ID="lblIndirizzo" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 23px; position: absolute; top: 149px" Width="71px">Indirizzo</asp:Label>
            <asp:Label ID="lblCivico" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 23px; position: absolute; top: 176px; bottom: 376px;" Width="71px">Civico</asp:Label>
            <asp:Label ID="lblComune" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 23px; position: absolute; top: 202px" Width="71px">Comune</asp:Label>
            <asp:Label ID="lblCap" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 22px; position: absolute; top: 226px" Width="71px">CAP</asp:Label>
            <asp:Label ID="lblDocIden" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute; top: 248px; width: 83px;">Doc. Identità N.</asp:Label>
            <asp:Label ID="lblRilasciata" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 22px; position: absolute; top: 273px; width: 83px; bottom: 185px;">Rilasciata da</asp:Label>
            <asp:Label ID="lblPermSogg" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 21px; position: absolute; top: 298px; width: 148px;">Perm. di Sogg.</asp:Label>
            <asp:Label ID="lblDataIdent" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 207px; position: absolute; top: 246px; width: 22px;">Data</asp:Label>
            <asp:Label ID="lblDataSogg" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 111; left: 207px; position: absolute; top: 296px; width: 25px;">Data</asp:Label>
            <asp:TextBox ID="txtVia" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 204px; position: absolute; top: 147px; width: 214px;" TabIndex="6" ToolTip="Inserire l'indirizzo di residenza"></asp:TextBox>
            <asp:DropDownList ID="cmbTipoVia" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120; left: 102px; position: absolute; top: 146px" TabIndex="5" Width="90px">
            </asp:DropDownList>
            <asp:TextBox ID="txtCivico" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 101px; position: absolute; top: 175px; width: 88px; bottom: 379px;" TabIndex="7"></asp:TextBox>
            <asp:TextBox ID="txtComune" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 100px; position: absolute; top: 199px; width: 88px;" TabIndex="8"></asp:TextBox>
            <asp:TextBox ID="txtCap" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 100px; position: absolute; top: 223px; width: 88px; right: 353px;" TabIndex="9"></asp:TextBox>
            <asp:TextBox ID="txtDocIdent" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 100px; position: absolute; top: 246px; width: 88px;" TabIndex="10" ToolTip="Inserire numero carta d'identità"></asp:TextBox>
            <asp:TextBox ID="txtRilasciata" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 100px; position: absolute; top: 270px; width: 88px;" TabIndex="12"></asp:TextBox>
            <asp:TextBox ID="txtPermSogg" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 100px; position: absolute; top: 296px; width: 88px;" TabIndex="13"></asp:TextBox>
            <asp:TextBox ID="txtDataDocI" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 105; left: 238px; position: absolute; top: 245px; width: 70px;" TabIndex="11" ToolTip="Inserire la data di rilascio del doc. d'identità"></asp:TextBox>
            <asp:TextBox ID="txtDataPermSogg" runat="server" Columns="10" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 105; left: 238px; position: absolute; top: 295px; width: 70px;" TabIndex="14" ToolTip="Inserire la data del permesso di soggiorno"></asp:TextBox>
            <asp:TextBox ID="txtCF" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="16" Style="z-index: 113; left: 102px; position: absolute; top: 120px" TabIndex="4" Width="172px"></asp:TextBox>
            <asp:Label ID="lblDataIngr" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 119; left: 20px; position: absolute; top: 323px">Data inizio</asp:Label>
            <asp:TextBox ID="txtDataIngr" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 113; left: 100px; position: absolute; top: 322px; width: 70px;" TabIndex="15"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Height="18px" Style="z-index: 119; left: 21px; position: absolute; top: 349px">Data fine</asp:Label>
            <asp:TextBox ID="txtDataFine" runat="server" Columns="22" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="10" Style="z-index: 113; left: 100px; position: absolute; top: 347px; width: 70px;" TabIndex="16"></asp:TextBox>
            <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 205px; position: absolute; top: 93px" Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
            <asp:Label ID="lblInizio" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 197px; position: absolute; top: 324px" Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
            <asp:Label ID="lblFine" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 196px; position: absolute; top: 350px" Text="(valorizzare)" Visible="False" Width="229px"></asp:Label>
            <asp:Label ID="LBLdataDoc" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 329px; position: absolute; top: 246px" Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="LBLdataPerm" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 121; left: 329px; position: absolute; top: 295px" Text="(valorizzare)" Visible="False"></asp:Label>
            <asp:Label ID="L4" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="Red" Style="z-index: 122; left: 294px; position: absolute; top: 122px" Text="(valorizzare)" Visible="False" Width="136px"></asp:Label>
            <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 257px; position: absolute; top: 420px" TabIndex="3" Width="17px" Height="12px"></asp:TextBox>
            <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 274px; position: absolute; top: 416px; right: 157px;" TabIndex="3" Width="11px" Height="12px"></asp:TextBox>
            <asp:TextBox ID="txtProgr" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue" MaxLength="6" Style="left: 200px; position: absolute; top: 419px" TabIndex="3" Width="11px" Height="11px"></asp:TextBox>
        </p>
    </div>
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="verifica" runat="server" Value="0" />
    <asp:HiddenField ID="salvaComponente" runat="server" Value="0" />
    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtProgr').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        };

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
