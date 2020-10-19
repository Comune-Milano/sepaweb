<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonioI.aspx.vb"
    Inherits="ANAUT_com_patrimonioI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Patrimonio Immobiliare Componenti</title>
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
<script type="text/javascript"></script>
<body bgcolor="lightsteelblue">
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    <div>
        <input id="Button2" language="javascript" onclick="ConfermaEsci();" style="z-index: 122;
            left: 355px; position: absolute; top: 410px" type="button" value="Chiudi" />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 100; left: 9px; position: absolute; top: 14px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 101; left: 204px; position: absolute;
            top: 410px; height: 26px;" TabIndex="12" Text="SALVA e Chiudi" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="3px" MaxLength="6" Style="left: 251px; position: absolute; top: 416px"
            TabIndex="3" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 297px; position: absolute; top: 410px" TabIndex="3" Width="5px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 11px; position: absolute;
            top: 52px" Width="50px">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 94px; position: absolute; top: 50px" TabIndex="1" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 18px; position: absolute;
            top: 357px" Width="79px">Sup.Utile</asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
            Style="z-index: 106; left: 15px; position: absolute; top: 288px; width: 367px;
            height: 32px;">I campi sottostanti devono essere compilati se trattasi di Alloggio. In caso contrario inserire il valore 0 per entrambi i campi.</asp:Label>
        <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 18px; position: absolute;
            top: 330px" Width="79px">Num.Vani</asp:Label>
        <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 16px; position: absolute;
            top: 260px" Width="79px">Comune</asp:Label>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 12px; position: absolute;
            top: 81px" Width="79px">Tipologia F.</asp:Label>
        <asp:DropDownList ID="cmbComune" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 93px; position: absolute; top: 258px" TabIndex="9" Width="316px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbTipo" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 95px; position: absolute; top: 79px" TabIndex="2" Width="316px" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
            <asp:ListItem Value="1">TERRENI</asp:ListItem>
            <asp:ListItem Value="2">AREE EDIFICABILI</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="cmbTipoPropr" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 94px; position: absolute; top: 143px" TabIndex="2" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 108px" Width="69px">% Proprietà</asp:Label>
        <asp:Label ID="Label18" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 13px; position: absolute;
            top: 142px" Width="74px">Tipo Proprietà</asp:Label>
        <asp:TextBox ID="txtPerc" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 109;
            left: 95px; position: absolute; top: 107px" TabIndex="3" Width="34px">100</asp:TextBox>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 141px; position: absolute; top: 109px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 278px; position: absolute; top: 192px;
            width: 132px; height: 17px;" Text="valorizzare" Visible="False"></asp:Label>
        <asp:Label ID="Label17" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 162px; position: absolute;
            top: 173px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 344px; position: absolute;
            top: 174px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtSupUtile" runat="server" Columns="8" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="7" Style="z-index: 113; left: 131px; position: absolute; top: 356px"
            TabIndex="11"></asp:TextBox>
        <asp:Label ID="lblValMercato" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 18px; position: absolute;
            top: 387px; width: 98px;">Valore di mercato</asp:Label>
			
<asp:TextBox ID="txtValoreMercato" runat="server" Columns="8" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="7" Style="z-index: 113; left: 131px; position: absolute; top: 382px"
            TabIndex="11"></asp:TextBox>
        <asp:TextBox ID="txtNumVani" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="2" Style="z-index: 113;
            left: 131px; position: absolute; top: 330px" TabIndex="10"></asp:TextBox>
        <asp:TextBox ID="txtValore" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 113;
            left: 277px; position: absolute; top: 173px" TabIndex="7" ReadOnly="True" BackColor="#CCFFFF"></asp:TextBox>
        <asp:Label ID="Label16" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 15px; position: absolute;
            top: 174px" Width="71px">Rendita Cat.</asp:Label>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 214px; position: absolute;
            top: 174px; width: 62px;">Valore ICI</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 115; left: 161px; position: absolute;
            top: 204px" Width="24px">,00</asp:Label>
        <asp:ImageButton ID="btnCalcolaICI" runat="server" Style="position: absolute; top: 165px;
            left: 181px;" ImageUrl="~/NuoveImm/Img_Calcolatrice.png" ToolTip="Calcola Valore ICI/IMU"
            OnClientClick="document.getElementById('calcoloICI').value = 1;document.getElementById('Attendi').style.visibility='visible';"
            Width="30px" />
        <asp:TextBox ID="txtRendita" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 116;
            left: 94px; position: absolute; top: 173px" TabIndex="8"></asp:TextBox>
        <asp:TextBox ID="TxtMutuo" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 116;
            left: 93px; position: absolute; top: 202px" TabIndex="8"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Style="z-index: 117; left: 16px; position: absolute; top: 203px;
            height: 19px;" Width="71px">Mutuo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 189px; position: absolute; top: 204px"
            Text="(valorizzare)" Visible="False" Width="197px"></asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 15px; position: absolute;
            top: 231px" Width="50px">Cat.Catastale</asp:Label>
        <asp:DropDownList ID="cmbTipoImm" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 93px; position: absolute; top: 228px" TabIndex="9" Width="45px">
        </asp:DropDownList>
        <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 187px; position: absolute; top: 337px;
            height: 14px; width: 215px;" Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:Label ID="lblMQ" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 187px; position: absolute; top: 362px;
            height: 14px; width: 215px;" Text="(valorizzare)" Visible="False"></asp:Label>
    </div>
    <asp:CheckBox ID="ChPiena" runat="server" Style="position: absolute; top: 105px;
        left: 209px; width: 205px; display: none;" Font-Names="times new roman" Font-Size="8pt"
        Text="La restante percentuale è attribuibile ad altro/i componenti dello stesso nucleo"
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
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.\nUscire senza salvare causerà la perdita delle modifiche!\n Uscire ugualmente? Per non uscire premere ANNULLA.");
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
