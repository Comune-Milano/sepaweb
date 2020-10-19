<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_patrimonioI.aspx.vb"
    Inherits="ANAUT_com_patrimonioI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
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
<script type="text/javascript" src="Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <form id="form1" runat="server">
    <div>
        <input id="Button2" language="javascript" onclick="Chiudi()" style="z-index: 122;
            left: 352px; position: absolute; top: 411px" type="button" value="Chiudi" />
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 100; left: 9px; position: absolute; top: 14px"
            Text="Componente Nucleo" Width="209px"></asp:Label>
        <asp:Label ID="lblErrore" runat="server" Visible="False" Style="z-index: 122; left: 19px;
            position: absolute; top: 440px" Font-Bold="True" ForeColor="Red"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 101; left: 204px; position: absolute;
            top: 411px; height: 26px;" TabIndex="11" Text="SALVA e Chiudi" />
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="3px" MaxLength="6" Style="left: 242px; position: absolute; top: 422px"
            TabIndex="3" Width="1px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 268px; position: absolute; top: 419px" TabIndex="3" Width="5px"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 104; left: 18px; position: absolute;
            top: 52px" Width="50px">Componente</asp:Label>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 104px; position: absolute; top: 50px" TabIndex="1" Width="316px">
        </asp:DropDownList>
        <asp:Label ID="Label13" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 18px; position: absolute;
            top: 391px" Width="79px">Sup.Utile</asp:Label>
        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
            Style="z-index: 106; left: 18px; position: absolute; top: 320px; width: 367px;
            height: 32px;">I campi sottostanti devono essere compilati se trattasi di Alloggio. In caso contrario inserire il valore 0 per entrambi i campi.</asp:Label>
        <asp:Label ID="Label12" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 19px; position: absolute;
            top: 361px" Width="79px">Num.Vani</asp:Label>
        <asp:Label ID="Label11" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 19px; position: absolute;
            top: 288px" Width="79px">Comune</asp:Label>
        <asp:Label ID="Label6" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 106; left: 19px; position: absolute;
            top: 80px" Width="79px">Tipologia F.</asp:Label>
        <asp:DropDownList ID="cmbComune" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 103px; position: absolute; top: 285px" TabIndex="7" Width="316px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbTipo" runat="server" CssClass="CssMaiuscolo" Style="z-index: 107;
            left: 104px; position: absolute; top: 79px" TabIndex="2" Width="316px">
            <asp:ListItem Selected="True" Value="0">FABBRICATI</asp:ListItem>
            <asp:ListItem Value="1">TERRENI</asp:ListItem>
            <asp:ListItem Value="2">AREE EDIFICABILI</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 108; left: 18px; position: absolute;
            top: 111px" Width="69px">% Proprietà</asp:Label>
        <asp:TextBox ID="txtPerc" runat="server" Columns="4" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="5" Style="z-index: 109;
            left: 103px; position: absolute; top: 110px" TabIndex="3" Width="34px">100</asp:TextBox>
        <asp:Label ID="L1" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 110; left: 148px; position: absolute; top: 111px"
            Text="(valorizzare)" Visible="False" Width="245px"></asp:Label>
        <asp:Label ID="L2" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 111; left: 239px; position: absolute; top: 186px;
            height: 13px; width: 186px;" Text="(valorizzare)" Visible="False"></asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 181px; position: absolute;
            top: 221px" Width="24px">,00</asp:Label>
        <asp:Label ID="Label15" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 112; left: 181px; position: absolute;
            top: 187px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="txtSupUtile" runat="server" Columns="8" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            MaxLength="7" Style="z-index: 113; left: 105px; position: absolute; top: 389px"
            TabIndex="9"></asp:TextBox>
        <asp:TextBox ID="txtNumVani" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="2" Style="z-index: 113;
            left: 105px; position: absolute; top: 361px" TabIndex="8"></asp:TextBox>
        <asp:TextBox ID="txtRendita" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 113;
            left: 105px; position: absolute; top: 183px" TabIndex="4"></asp:TextBox>
        <asp:ImageButton ID="btnCalcolaICI" runat="server" Style="position: absolute; top: 177px;
            left: 200px;" ImageUrl="~/NuoveImm/Img_Calcolatrice.png" ToolTip="Calcola Valore ICI/IMU"
            OnClientClick="document.getElementById('calcoloICI').value = 1;document.getElementById('Attendi').style.visibility='visible';"
            Width="30px" />
        <asp:TextBox ID="txtValore" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="True"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 113;
            left: 104px; position: absolute; top: 217px" TabIndex="4" BackColor="#CCFFFF"
            ReadOnly="True"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 114; left: 16px; position: absolute;
            top: 218px" Width="80px">Valore ICI/IMU</asp:Label>
        <asp:Label ID="lblRendita" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Style="z-index: 110; left: 19px; position: absolute; top: 183px;
            width: 90px; height: 18px;">Rendita catast.</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 115; left: 181px; position: absolute;
            top: 253px" Width="24px">,00</asp:Label>
        <asp:TextBox ID="TxtMutuo" runat="server" Columns="8" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" MaxLength="8" Style="z-index: 116;
            left: 104px; position: absolute; top: 251px" TabIndex="5"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 117; left: 19px; position: absolute;
            top: 252px" Width="71px">Mutuo</asp:Label>
        <asp:Label ID="L3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 118; left: 215px; position: absolute; top: 253px"
            Text="(valorizzare)" Visible="False" Width="197px"></asp:Label>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 119; left: 18px; position: absolute;
            top: 145px" Width="50px">Cat.Catastale</asp:Label>
        <asp:DropDownList ID="cmbTipoImm" runat="server" CssClass="CssMaiuscolo" Style="z-index: 120;
            left: 105px; position: absolute; top: 143px" TabIndex="6" Width="45px">
        </asp:DropDownList>
        <asp:Label ID="L5" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt"
            ForeColor="Red" Style="z-index: 123; left: 178px; position: absolute; top: 362px;
            height: 14px; width: 255px;" Text="(valorizzare)" Visible="False"></asp:Label>
    </div>
    <asp:CheckBox ID="ChPiena" runat="server" Style="position: absolute; top: 105px;
        left: 216px; width: 205px;" Font-Names="times new roman" Font-Size="8pt" Text="La restante percentuale è attribuibile ad altro/i componenti dello stesso nucleo" />
    <asp:HiddenField ID="calcoloICI" runat="server" Value="0" />
    </form>
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
                    Calcolo in corso...
                </td>
            </tr>
        </table>
    </div>
</body>
<script type="text/javascript">
    document.getElementById('txtRiga').style.visibility = 'hidden';
    document.getElementById('txtOperazione').style.visibility = 'hidden';

    function Chiudi() {
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
    document.getElementById('Attendi').style.visibility = 'hidden';


</script>
</html>
