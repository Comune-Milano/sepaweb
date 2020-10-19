<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonioI.aspx.vb"
    Inherits="ANAUT_com_patrimonioI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title>Patrimonio Immobiliare Componenti</title>
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
            left: 371px; position: absolute; top: 403px; width: 76px;" type="button" 
            value="Chiudi" class="bottone" />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="Maroon" Style="z-index: 100; left: 13px; position: absolute; top: 18px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 110; left: 233px; position: absolute;
            top: 403px; width: 130px;" TabIndex="16" Text="SALVA e Chiudi" 
            CssClass="bottone" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="3px" MaxLength="6" Style="left: 251px; position: absolute; top: 402px"
            TabIndex="3" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 297px; position: absolute; top: 398px" TabIndex="3" Width="5px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 11px; position: absolute;
            top: 52px" Width="50px">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 94px; position: absolute; top: 50px" TabIndex="1" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 18px; position: absolute;
            top: 381px" Width="79px">Sup.Utile</asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
            Style="z-index: 106; left: 15px; position: absolute; top: 314px; width: 367px;
            height: 32px;">I campi sottostanti devono essere compilati se trattasi di Alloggio. In caso contrario inserire il valore 0 per entrambi i campi.</asp:Label>
        <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 18px; position: absolute;
            top: 356px" Width="79px">Num.Vani</asp:Label>
        <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 16px; position: absolute;
            top: 291px" Width="79px">Comune</asp:Label>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 12px; position: absolute;
            top: 81px" Width="79px">Tipologia F.</asp:Label>
        <asp:DropDownList ID="cmbComune" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 95px; position: absolute; top: 287px" TabIndex="13" Width="316px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbTipo" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 95px; position: absolute; top: 79px" TabIndex="2" Width="316px" 
            AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
            <asp:ListItem Value="1">TERRENI</asp:ListItem>
            <asp:ListItem Value="2">AREE EDIFICABILI</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="cmbTipoPropr" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 94px; position: absolute; top: 143px" TabIndex="5" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 108px" Width="69px">% Proprietà</asp:Label>
        <asp:Label ID="Label18" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 142px; width: 78px;">Tipo Proprietà</asp:Label>
        <asp:TextBox ID="txtPerc" runat="server" Columns="4" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 109;
            left: 95px; position: absolute; top: 107px" TabIndex="3" Width="34px">100</asp:TextBox>
            <asp:CheckBox ID="chVenduto" runat="server" 
            style="position:absolute; top: 103px; left: 280px;" CssClass="CssMaiuscolo" 
            TabIndex="4" Text="Immobile Venduto" />
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 141px; position: absolute; top: 109px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 278px; position: absolute; top: 219px;
            width: 132px; height: 17px;" Text="valorizzare" Visible="False"></asp:Label>
        <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 162px; position: absolute;
            top: 205px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 344px; position: absolute;
            top: 201px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtSupUtile" runat="server" Columns="8" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue"
            MaxLength="7" Style="z-index: 113; left: 94px; position: absolute; top: 380px"
            TabIndex="15"></asp:TextBox>
        <asp:TextBox ID="txtNumVani" runat="server" Columns="8" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="2" Style="z-index: 113;
            left: 94px; position: absolute; top: 354px" TabIndex="14"></asp:TextBox>
        <asp:TextBox ID="txtIndirizzo" runat="server" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="25"
            Style="z-index: 113; left: 94px; position: absolute; top: 173px; width: 249px;"
            TabIndex="6"></asp:TextBox>
        <asp:TextBox ID="txtCivico" runat="server" Columns="8" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 113;
            left: 366px; position: absolute; top: 172px; width: 35px;" TabIndex="7"></asp:TextBox>
        <asp:TextBox ID="txtValore" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" 
            MaxLength="8" Style="z-index: 113;
            left: 277px; position: absolute; top: 201px" TabIndex="10" ReadOnly="True" 
            BackColor="#CCFFFF"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 14px; position: absolute;
            top: 174px" Width="71px">Indirizzo</asp:Label>
        <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 352px; position: absolute;
            top: 175px; width: 20px;">N.</asp:Label>
        <asp:Label ID="Label16" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 15px; position: absolute;
            top: 205px" Width="71px">Rendita Cat.</asp:Label>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 214px; position: absolute;
            top: 203px; width: 62px;">Valore ICI</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 115; left: 162px; position: absolute;
            top: 233px" Width="24px">,00</asp:Label>
        <asp:ImageButton ID="btnCalcolaICI" runat="server" Style="position: absolute; top: 198px;
            left: 181px;" ImageUrl="~/NuoveImm/Img_Calcolatrice.png" ToolTip="Calcola Valore ICI/IMU"
            OnClientClick="document.getElementById('calcoloICI').value = 1;document.getElementById('Attendi').style.visibility='visible';"
            Width="30px" TabIndex="9" />
        <asp:TextBox ID="txtRendita" runat="server" Columns="8" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 116;
            left: 94px; position: absolute; top: 203px" TabIndex="8"></asp:TextBox>
        <asp:TextBox ID="TxtMutuo" runat="server" Columns="8" CssClass="CssMaiuscolo" 
            Font-Bold="False" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 116;
            left: 94px; position: absolute; top: 230px; width: 48px;" TabIndex="11"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Style="z-index: 117; left: 15px; position: absolute; top: 232px;
            height: 19px; width: 86px;" ToolTip="Mutuo residuo">Mutuo resid.</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 190px; position: absolute; top: 232px"
            Text="(valorizzare)" Visible="False" Width="197px"></asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 15px; position: absolute;
            top: 260px" Width="50px">Cat.Catastale</asp:Label>
        <asp:DropDownList ID="cmbTipoImm" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 94px; position: absolute; top: 258px" TabIndex="12" Width="45px">
        </asp:DropDownList>
        <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 175px; position: absolute; top: 355px;
            height: 14px; width: 215px;" Text="(valorizzare)" Visible="False"></asp:Label>
    </div>
    <asp:CheckBox ID="ChPiena" runat="server" Style="position: absolute; top: 105px;
        left: 209px; width: 205px;display:none;" Font-Names="times new roman" Font-Size="8pt" Text="La restante percentuale è attribuibile ad altro/i componenti dello stesso nucleo"
        TabIndex="4" />
    <asp:HiddenField ID="calcoloICI" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <div id="Attendi" style="position: absolute; width: 100%; height: 100%; visibility: hidden;
        background-color: #C0C0C0; top: 0px; left: 0px; z-index: 1000;">
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr style="text-align: center">
                <td>
                    Caricamento in corso...
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="salvaPatrImmob" runat="server" Value="0" />
    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';

        var opener = window.dialogArguments;
        window.opener.document.getElementById('caric').style.visibility = 'hidden';

        document.getElementById('Attendi').style.visibility = 'hidden';

        function Chiudi() {
            //            CloseModal2(document.getElementById('txtModificato').value);
            document.getElementById('txtModificato').value = '0';
            window.close();
        }
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');

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
