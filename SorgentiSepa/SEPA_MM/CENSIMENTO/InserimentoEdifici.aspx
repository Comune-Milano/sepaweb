<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimentoEdifici.aspx.vb"
    Inherits="CENSIMENTO_InserimentoEdifici" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Src="Tab_UtMillesimali.ascx" TagName="Tab_UtMillesimali" TagPrefix="uc5" %>
<%@ Register Src="Tab_AdNormativo.ascx" TagName="Tab_AdNormativo" TagPrefix="uc3" %>
<%@ Register Src="Tab_AdVarConf.ascx" TagName="Tab_AdVarConf" TagPrefix="uc2" %>
<%@ Register Src="Tab_AdDimens.ascx" TagName="Tab_AdDimens" TagPrefix="uc1" %>
<%@ Register Src="Tab_Millesimali.ascx" TagName="Tab_Millesimali" TagPrefix="uc4" %>
<%@ Register Src="Tab_Servizi.ascx" TagName="Tab_Servizi" TagPrefix="uc6" %>
<%@ Register Src="Tab_ImpComuni.ascx" TagName="Tab_ImpComuni" TagPrefix="uc7" %>
<%@ Register Src="Tab_NonIspezion.ascx" TagName="Tab_NonIspezion" TagPrefix="uc8" %>
<%@ Register Src="Tab_CPI.ascx" TagName="Tab_CPI" TagPrefix="uc9" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head runat="server">
    <style type="text/css">
        div.RadGrid .rgMasterTable {
            line-height: large;
        }

        .rgAltRow, .rgRow {
            cursor: pointer !important;
        }

        .sfondo {
            /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#e8e8e8+0,fefefe+100 */
            background: rgb(232,232,232); /* Old browsers */
            background: -moz-linear-gradient(top, rgba(232,232,232,1) 0%, rgba(254,254,254,1) 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top, rgba(232,232,232,1) 0%,rgba(254,254,254,1) 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom, rgba(232,232,232,1) 0%,rgba(254,254,254,1) 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#e8e8e8', endColorstr='#fefefe',GradientType=0 ); /* IE6-9 */
            background-size: 100% 1000px;
        }

        .TitoloModulo {
            font-size: 15pt;
            color: #1c2466;
            font-family: Segoe UI;
            vertical-align: middle;
            font-weight: bold;
            text-align: center;
        }
    </style>
    <title>Inserimento EDIFICI</title>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');

        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true



        };
    </script>
    <script type="text/javascript" src="function.js"></script>
    <style>
        .CssMaiuscolo {
            text-transform: uppercase;
        }
        .auto-style1 {
            z-index: 103;
            left: 739px;
            top: 274px;
            position: absolute;
            width: 18px;
        }
    </style>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen">
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="36000">
        </telerik:RadScriptManager>
    <asp:HiddenField ID="CENS_MANUT_SL" runat="server" Value="0" />
    <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
        Style="z-index: 110; left: 722px; position: absolute; top: 27px; cursor: pointer;"
        ToolTip="Esci" OnClientClick="ConfermaEsci();" TabIndex="44" />
    <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 59px" Width="60px">Complesso*</asp:Label>
    <asp:DropDownList ID="DdLComplesso" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 101px; position: absolute; top: 59px; width: 664px;"
            TabIndex="1" AutoPostBack="True">
    </asp:DropDownList>
    &nbsp; &nbsp;
    <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
        Style="z-index: 104; left: 107px; position: absolute; top: 27px; cursor: pointer; height: 12px; right: 589px;"
        ToolTip="Stampa" OnClientClick="ConfermaEsci();"
        Visible="False" TabIndex="37" />
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 89px" Width="64px">Denominazione*</asp:Label>
        <asp:TextBox ID="TxtDenEdificio" runat="server" MaxLength="120" Style="left: 101px; position: absolute; top: 89px; z-index: 5; width: 450px; right: 574px;"
            CssClass="CssMaiuscolo"
        TabIndex="2"></asp:TextBox>
    <asp:DropDownList ID="CmbPTerra" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 326px; position: absolute; top: 268px"
            TabIndex="17" Width="48px">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 326px; position: absolute; top: 254px" Width="48px">P. Terra</asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 257px; position: absolute; top: 254px" Width="48px">Seminterrato</asp:Label>
    <asp:DropDownList ID="CmbSeminterrato" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 258px; position: absolute; top: 268px; right: 368px;"
            TabIndex="15" Width="60px">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="CmbSottotetto" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 508px; position: absolute; top: 268px"
            TabIndex="18" Width="60px">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 508px; position: absolute; top: 254px" Width="48px">Sottotetto</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 378px; position: absolute; top: 254px" Width="30px">Attico</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 441px; position: absolute; top: 253px" Width="49px"
        Height="17px">S. Attico</asp:Label>
    <asp:DropDownList ID="CmbAttico" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 378px; position: absolute; top: 268px"
            TabIndex="19" Width="56px">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="CmbSuperAttico" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 441px; position: absolute; top: 268px"
            TabIndex="16" Width="60px">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 10px; position: absolute; top: 253px" Width="72px">P. Entro Terra*</asp:Label>
    &nbsp; &nbsp;
    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 99px; position: absolute; top: 253px" Width="70px">P. Fuori Terra*</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 435px; position: absolute; top: 144px" Width="50px"
        ToolTip="Data Ristrutturazione">Data Ristr.</asp:Label>
    <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 582px; position: absolute; top: 203px" Width="88px">N° Passi CArrabili</asp:Label>
    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 301px; position: absolute; top: 144px" Width="55px"
        ToolTip="Data costruzione">Data Costr*</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 170px" Width="47px">Tipologia*</asp:Label>
    <asp:DropDownList ID="DrLTipoEdificio" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 100px; position: absolute; top: 171px;"
            TabIndex="4" Width="135px">
    </asp:DropDownList>
    <asp:DropDownList ID="CmbEntroTerra" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 10px; position: absolute; top: 268px; right: 980px;"
            TabIndex="11" Width="71px">
    </asp:DropDownList>
    <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 556px; position: absolute; top: 147px" Width="37px">Utilizzo*</asp:Label>
    <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 526px; position: absolute; top: 173px" Width="70px">Tipo Costrutt.*</asp:Label>
    <asp:DropDownList ID="DrLTipCostrut" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Style="border: 1px solid black; z-index: 111; left: 595px; position: absolute; top: 172px; width: 167px; height: 15px;"
            TabIndex="6">
    </asp:DropDownList>
    <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 218px; position: absolute; top: 228px" Width="72px">Liv. Possesso*</asp:Label>
    <asp:DropDownList ID="DrLLivelloPoss" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 296px; position: absolute; top: 226px; width: 280px;"
            TabIndex="9">
    </asp:DropDownList>
    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 226px" Width="68px"
        Height="16px">Imp. Riscald.*</asp:Label>
    <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Height="16px" Style="z-index: 100; left: 100px; position: absolute; top: 290px"
        Width="68px">Piano Vendita</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;&nbsp;
    <asp:TextBox ID="TxtDataCostr" runat="server" Style="left: 357px; position: absolute; top: 144px; z-index: 2;"
        Width="70px" ToolTip="dd/Mm/YYYY" MaxLength="10" TabIndex="8"></asp:TextBox>
        <asp:TextBox ID="TxtDataRistrut" runat="server" Style="left: 485px; position: absolute; top: 144px; z-index: 2;"
            Width="70px" ToolTip="dd/Mm/YYYY" MaxLength="10" TabIndex="8"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp;
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 10px; position: absolute; top: 291px" Width="56px">Condominio</asp:Label>
    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="CmbCondominio" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 11px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 306px"
        TabIndex="14" Width="69px" AutoPostBack="True">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="TxtNumScale" runat="server" MaxLength="4" Style="left: 634px; position: absolute; top: 270px; z-index: 2;"
        Width="15px" Enabled="False" TabIndex="13">0</asp:TextBox>
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 634px; position: absolute; top: 253px" Width="41px">N.  Scale</asp:Label>
    &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 909px; position: absolute; top: 73px" Width="16px"
        Visible="False">GIMI</asp:Label>
        <asp:TextBox ID="TxtGimi" runat="server" MaxLength="20" Style="left: 901px; position: absolute; top: 87px"
            Width="144px" Visible="False"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<a href="#"
        onclick="window.open('GestScale.aspx','','top=0,left=0,width=300,height=180,toolbar=no,scrollbars=no'); return false;"></a>
    <asp:DropDownList ID="DrLImpRiscald" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 99px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 226px"
            TabIndex="10" Width="110px">
    </asp:DropDownList>
    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
        Style="z-index: 103; left: 62px; position: absolute; top: 27px; cursor: pointer;"
        ToolTip="Salva" TabIndex="36" />
    <br />
    <asp:ImageButton ID="btnFoto" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
            Style="z-index: 103; left: 164px; position: absolute; top: 27px; cursor: pointer; height: 12px;"
            ToolTip="Foto e Planimetrie" TabIndex="45" Visible="False" OnClientClick="ApriFotoPlan();return false;" />
    <br />
    <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 256px; position: absolute; top: 45px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
    <img id="ImgEventi" alt="Eventi" border="0" onclick="window.open('Eventi.aspx?ID=<%=vId %> &CHIAMA=ED','Eventi', '');"
            src="../NuoveImm/Img_Eventi.png" style="left: 669px; cursor: pointer; position: absolute; top: 27px; right: 406px;" />
    <br />
    <br />
    <br />
    <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Height="16px" Style="z-index: 100; left: 11px; position: absolute; top: 144px"
        Width="50px">Località</asp:Label>
    &nbsp;
    <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 238px; top: 170px; position: absolute;" Width="58px">B. Manager</asp:Label>
    <asp:DropDownList ID="cmbBuildingManager" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Style="border: 1px solid black; z-index: 111; left: 293px; top: 172px; position: absolute;"
            TabIndex="24" Width="232px">
    </asp:DropDownList>
    <asp:DropDownList ID="DrLUtilizzo" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 595px; position: absolute; top: 143px;"
            TabIndex="5" Width="167px">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Height="16px" Style="z-index: 100; left: 11px; position: absolute; top: 198px"
        Width="68px">Tipologia Edile</asp:Label>
    <asp:DropDownList ID="cmbTipoEdil1" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 100px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 198px"
            TabIndex="10" Width="135px">
    </asp:DropDownList>
    &nbsp;
    <asp:DropDownList ID="CmbTipoEdil3" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 418px; position: absolute; top: 199px"
        TabIndex="10" Width="157px">
    </asp:DropDownList>
    &nbsp;
    <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 581px; position: absolute; top: 228px" Width="110px">% Sconto Costo Base</asp:Label>
    <br />
    <asp:DropDownList ID="cmbTipoEdil2" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 247px; position: absolute; top: 198px"
            TabIndex="10" Width="157px">
    </asp:DropDownList>
        <asp:TextBox ID="txtScCostoBase" runat="server" Style="left: 691px; position: absolute; top: 224px; z-index: 2; width: 38px; text-align: right;"
            ToolTip="%" MaxLength="2"
        TabIndex="8"></asp:TextBox>
    <br />
    <br />
        <asp:TextBox ID="txtNumMezzanini" runat="server" MaxLength="2" Style="left: 189px; text-align: right; position: absolute; top: 267px; z-index: 5; width: 40px;"
        TabIndex="20"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtNumMezzanini"
            ErrorMessage="!" Font-Bold="True" Style="left: 240px; position: absolute; top: 268px; z-index: 2; height: 20px; width: 9px;"
            ValidationExpression="\d{1,50}" ToolTip="Sono ammessi solo numeri"></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblGestDirRisc" runat="server" Font-Bold="False" Font-Names="Arial"
        Font-Size="8pt" Style="z-index: 100; left: 190px; position: absolute; top: 291px"
        Width="69px" ToolTip="Gestione Diretta del Riscaldamento">G. Dir.Risc.</asp:Label>
    <asp:DropDownList ID="cmbGestDirRisc" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 189px; position: absolute; top: 306px"
            TabIndex="14" Width="60px" Enabled="False">
        <asp:ListItem Value="0">NO</asp:ListItem>
        <asp:ListItem Value="1">SI</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:DropDownList ID="cmbScale" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 659px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 270px"
            TabIndex="10" Width="56px">
    </asp:DropDownList>
    <br />
    <asp:DropDownList ID="DrLTipoInd" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 101px; border-left: black 1px solid; border-bottom: black 1px solid; top: 118px; position: absolute;"
            TabIndex="21" Width="80px">
    </asp:DropDownList>
        <asp:TextBox ID="TxtDescrInd" runat="server" MaxLength="20" Style="left: 183px; top: 118px; z-index: 102; position: absolute;"
            Width="282px" CssClass="CssMaiuscolo" TabIndex="22"></asp:TextBox>
    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 472px; top: 118px; position: absolute;" Width="21px">Civ.</asp:Label>
        <asp:TextBox ID="TxtCivicoKilo" runat="server" MaxLength="13" Style="left: 491px; top: 118px; z-index: 102; position: absolute;"
            Width="59px" TabIndex="23"></asp:TextBox>
    <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 556px; top: 118px; position: absolute;" Width="40px">Comune</asp:Label>
    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="16px" Style="z-index: 100; left: 258px; position: absolute; top: 290px; width: 124px; right: 1150px;">St. Fisico Mancato Rilievo</asp:Label>
    <asp:DropDownList ID="cmbStatoFisico" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 258px; position: absolute; top: 306px; width: 178px;"
            TabIndex="10">
    </asp:DropDownList>
    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Height="16px" Style="z-index: 100; left: 441px; position: absolute; top: 290px; width: 49px; right: 967px;">Microzona</asp:Label>
    <asp:Label ID="lblZona" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 508px; position: absolute; top: 290px; width: 37px; height: 14px;">Municipio</asp:Label>
    <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 654px; position: absolute; top: 290px; width: 37px; right: 891px; height: 11px;">Rif.Legislativo</asp:Label>
        <asp:ImageButton ID="BtnElimina" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png" TabIndex="2"
            ToolTip="Elimina elemento selezionato dalla lista" CssClass="auto-style1" />
    <asp:Label ID="lblUnitaNonProprieta" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 573px; position: absolute; top: 290px; width: 79px; height: 14px;">N° UI NO Prop.</asp:Label>
            <asp:Label ID="lblOSMI" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="8pt" 
            Style="z-index: 100; left: 541px; position: absolute; top: 334px; width: 246px; height: 14px;" 
            ForeColor="#0000CC" Visible="False">Z.OSMI</asp:Label>
    <br />
    <asp:HyperLink ID="HyLinkPertinenze" runat="server" Font-Bold="True" Font-Names="Arial"
        Font-Size="20pt" ForeColor="LimeGreen" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
        Style="z-index: 5; left: 717px; cursor: hand; top: 267px; text-align: left; position: absolute;"
        Width="20px" Height="25px">+</asp:HyperLink>
        <asp:DropDownList ID="cmbPeriodo" runat="server" Style="position: absolute; top: 332px; left: 441px; width: 90px;"
            Enabled="False">
    </asp:DropDownList>
        <asp:TextBox ID="txtpCarrabili" runat="server" Style="left: 691px; position: absolute; top: 199px; z-index: 2; width: 38px;"
            MaxLength="2" TabIndex="8"></asp:TextBox>
    <br />
        <asp:DropDownList ID="DrlSchede" runat="server" Enabled="False" Style="z-index: 143; left: 99px; position: absolute; top: 333px; width: 335px;"
            TabIndex="5">
    </asp:DropDownList>
    <img border="0" alt="Apri Scheda Rilievo" id="imgSchede" src="../NuoveImm/Img_SchedaRilievo.png"
        style="cursor: pointer; left: 10px; position: absolute; top: 334px;" onclick="scDaAprire();" />
    &nbsp;<br />
    <asp:DropDownList ID="DrLComune" runat="server" BackColor="White" Font-Names="arial"
            Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 595px; border-left: black 1px solid; border-bottom: black 1px solid; top: 118px; position: absolute;"
            TabIndex="24" Width="167px" AutoPostBack="True">
    </asp:DropDownList>
    <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; top: 116px; position: absolute;" Width="50px"
        Height="16px">Indirizzo *</asp:Label>
        <asp:TextBox ID="TxtLocalità" runat="server" MaxLength="50" Style="left: 100px; top: 144px; z-index: 102; position: absolute;"
            Width="120px" CssClass="CssMaiuscolo" TabIndex="25"></asp:TextBox>
    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 221px; top: 144px; position: absolute;" Width="24px">CAP*</asp:Label>
        <asp:TextBox ID="TxtCAP" runat="server" MaxLength="5" Style="left: 248px; top: 144px; z-index: 102; position: absolute;"
            Width="44px" TabIndex="26"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtCAP"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 295px; top: 144px; z-index: 2; position: absolute;"
            ValidationExpression="\d{5}" Width="2px" ToolTip="E' ammesso un  CAP di 5 cifre"
        Display="Dynamic"></asp:RegularExpressionValidator>
    <div class="tabber" style="text-align: left;">
        <asp:DropDownList ID="CmbPianoVendita" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 100px; position: absolute; top: 306px"
                TabIndex="12" Width="82px">
            <asp:ListItem Value="0">NO</asp:ListItem>
            <asp:ListItem Value="1">SI</asp:ListItem>
        </asp:DropDownList>
        <asp:DropDownList ID="cmbMicrozona" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 200; left: 441px; position: absolute; top: 306px; width: 60px;"
                TabIndex="10">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbZona" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 200; left: 508px; position: absolute; top: 306px; width: 58px;"
                TabIndex="10">
        </asp:DropDownList>
            <asp:TextBox ID="txtUnitaNP" runat="server" Font-Names="Arial" Font-Size="8pt" Style="border: 1px solid black; z-index: 200; left: 573px; text-align: right; position: absolute; top: 306px; width: 58px;"
                TabIndex="10" MaxLength="5" Width="40px"></asp:TextBox>
        <asp:DropDownList ID="cmbRifLeg" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 200; left: 654px; position: absolute; top: 306px; width: 135px;"
                TabIndex="10">
        </asp:DropDownList>
            <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
                DecorationZoneID="decorationZone1"></telerik:RadFormDecorator>
        <div class="<%=classetab %> <%=tabdefault1%>" title="DIMENS.">
            <uc1:Tab_AdDimens ID="Tab_AdDimens1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault2%>" title="VAR.CONF.">
            <uc2:Tab_AdVarConf ID="Tab_AdVarConf1" runat="server"></uc2:Tab_AdVarConf>
        </div>
        <div class="<%=classetab %> <%=tabdefault3%>" title="AD.NORMATIVO.">
            <uc3:Tab_AdNormativo ID="Tab_AdNormativo1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault4%>" title="MILLESIMALI">
            <uc4:Tab_Millesimali ID="Tab_Millesimali1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault5%>" title="UTENZE.MILL.">
            <uc5:Tab_UtMillesimali ID="Tab_UtMillesimali1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault6%>" title="SERVIZI">
            <uc6:Tab_Servizi ID="Tab_Servizi1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault7%>" title="IMP.COMUNI">
            <uc7:Tab_ImpComuni ID="Tab_ImpComuni1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault8%>" title="NOTE">
            <table style="width: 645px;">
                <tr>
                        <td style="text-align: left; vertical-align: top; font-family: Arial, Helvetica, sans-serif; font-size: 9pt; font-style: normal; width: 50%;">Sintesi Edificio
                    </td>
                        <td style="text-align: left; vertical-align: top; font-family: Arial, Helvetica, sans-serif; font-size: 9pt; font-style: normal; width: 50%;">Note Manutentive
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left; vertical-align: top; width: 50%;">
                        <asp:TextBox ID="txtNote" runat="server" Height="113px" TextMode="MultiLine" Width="97%"
                            MaxLength="1500"></asp:TextBox>
                    </td>
                    <td style="text-align: left; vertical-align: top; width: 50%;">
                        <asp:TextBox ID="txtNoteMan" runat="server" Height="113px" TextMode="MultiLine" Width="100%"
                            MaxLength="1500"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="<%=classetab %> <%=tabdefault9%>" title="NON ISPEZ.">
            <uc8:Tab_NonIspezion ID="Tab_NonIspezion1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault10%>" title="C.P.I">
            <uc9:Tab_CPI ID="Tab_CPI1" runat="server" />
        </div>
            <div title="RIL.MANUTENT." style="display:none;">

                <telerik:RadWindow ID="RadWindowProgrIntervento" runat="server" CenterIfModal="true"
                    Modal="true" Title="Gestione programma intervento" Width="600" Height="350px"
                    VisibleStatusbar="false" Behaviors="Pin, Maximize, Move, Resize">
                    <ContentTemplate>
                        <div id="decorationZone1">
                            <asp:Panel runat="server" ID="PanelProgrIntervento" Style="height: 100%;" class="sfondo">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="TitoloModulo">Programma intervento - Appalto
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnSalvaProgrInt" runat="server" Text="Salva" Style="cursor: pointer;"
                                                            TabIndex="55" ToolTip="Salva" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnChiudiProgrInt" runat="server" Text="Esci" OnClientClick="closeWindow(null, null, 'RadWindowProgrIntervento');"
                                                            Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <asp:Label ID="lblProgrInt" Text="Programma intervento" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbProgrInt" Width="95%" AppendDataBoundItems="true" Filter="Contains"
                                                            Font-Size="8pt" runat="server" AutoPostBack="TRUE" ResolvedRenderMode="Classic"
                                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label34" Text="Appalto" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                                                    <td>
                                                        <telerik:RadComboBox ID="cmbAppalto" Width="95%" AppendDataBoundItems="true" Filter="Contains"
                                                            Font-Size="8pt" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                                            HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>

                <telerik:RadWindow ID="RadWindowFunzioniSpeciali" runat="server" CenterIfModal="true"
                    Modal="true" Title="Gestione progetti speciali" Width="720px" Height="550px"
                    VisibleStatusbar="false" Behaviors="Pin, Maximize, Move, Resize">
                    <ContentTemplate>
                        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Buttons"
                            DecorationZoneID="decorationZone"></telerik:RadFormDecorator>
                        <div id="decorationZone">
                            <asp:Panel runat="server" ID="PanelProgSpeciali" Style="height: 100%;" class="sfondo">
                                <table>
                                    <tr>
                                        <td class="TitoloModulo">Progetti Speciali
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btn_SalvaProgSpeciali" runat="server" Text="Salva" Style="cursor: pointer;"
                                                            TabIndex="55" ToolTip="Salva" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_ChiudiProgSpeciali" runat="server" Text="Esci" OnClientClick="closeWindow(null, null, 'RadWindowFunzioniSpeciali');"
                                                            Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgvElencoFunSpeciali" runat="server" GroupPanelPosition="Top"
                                                ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                                AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                                Height="450" IsExporting="False" PagerStyle-AlwaysVisible="true">
                                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                                    CommandItemDisplay="None" Font-Size="8pt">
                                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                                        ShowRefreshButton="true" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn HeaderText="SELEZIONA" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                <asp:CheckBox ID="ChkSeleziona" runat="server" TabIndex="-1" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECKED") %>' />
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="100px" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" HorizontalAlign="Center" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="NOME_PROGETTO" HeaderText="NOME PROGETTO" AutoPostBackOnFilter="true"
                                                            FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DATA_INIZIO_PROGETTO" HeaderText="DATA INIZIO PROGETTO"
                                                            AutoPostBackOnFilter="true" FilterControlWidth="90%" CurrentFilterFunction="Contains"
                                                            DataFormatString="{0:@}">
                                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DATA_FINE_PROGETTO" HeaderText="DATA INIZIO PROGETTO"
                                                            AutoPostBackOnFilter="true" FilterControlWidth="90%" CurrentFilterFunction="Contains"
                                                            DataFormatString="{0:@}">
                                                            <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                </MasterTableView>
                                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                                    <Excel FileExtension="xls" Format="Xlsx" />
                                                </ExportSettings>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                                                    <Selecting AllowRowSelect="True" />
                                                    <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                                        AllowResizeToFit="true" />
                                                </ClientSettings>
                                                <PagerStyle AlwaysVisible="True"></PagerStyle>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
                <div style="width: 100%; height: 170px; overflow: auto;">
                    <table style="width:100%">
                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <legend>&nbsp;&nbsp;<asp:Label ID="Label39" Text="Programma intervento - Appalto" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="8pt"></asp:Label></legend>
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="dgvProgrInt" runat="server" GroupPanelPosition="Top"
                                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="700px" AllowSorting="True"
                                                    Height="100" IsExporting="False" PagerStyle-AlwaysVisible="true">
                                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true" Width="100%"
                                                        CommandItemDisplay="None" Font-Size="8pt">
                                                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                                            ShowRefreshButton="true" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="PROGRAMMA_INTERVENTO" HeaderText="PROGRAMMA INTERVENTO" AutoPostBackOnFilter="true"
                                                                FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="350"
                                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" Wrap="False" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="APPALTO" HeaderText="APPALTO" AutoPostBackOnFilter="true"
                                                                FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="350"
                                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" Wrap="False" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                    </MasterTableView>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                                        <Excel FileExtension="xls" Format="Xlsx" />
                                                    </ExportSettings>
                                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                                                        <Selecting AllowRowSelect="True" />
                                                        <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                                            AllowResizeToFit="false" />
                                                    </ClientSettings>
                                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                </telerik:RadGrid>
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:ImageButton ID="btnAddProgrInt" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png"
                                                    Style="z-index: 103; left: 495px; top: 38px" TabIndex="1" ToolTip="Aggiungi elemento alla lista" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>

                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <fieldset>
                                    <legend>&nbsp;&nbsp;<asp:Label ID="Label40" Text="Progetto Speciale" Font-Bold="true" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label></legend>
                                    <table style="width:100%">
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="dgvProgettiSpeciali" runat="server" GroupPanelPosition="Top"
                                                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                                    AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="700px" AllowSorting="True"
                                                    Height="100" IsExporting="False" PagerStyle-AlwaysVisible="true">
                                                    <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                                        CommandItemDisplay="None" Font-Size="8pt">
                                                        <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                                            ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                                            ShowRefreshButton="true" />
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="NOME_PROGETTO" HeaderText="NOME PROGETTO" AutoPostBackOnFilter="true"
                                                                FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="360"
                                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" Wrap="False" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="DATA_INIZIO_PROGETTO" HeaderText="DATA INIZIO PROGETTO"
                                                                AutoPostBackOnFilter="true" FilterControlWidth="90%" CurrentFilterFunction="Contains"
                                                                DataFormatString="{0:@}">
                                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="170"
                                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" Wrap="False" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="DATA_FINE_PROGETTO" HeaderText="DATA FINE PROGETTO"
                                                                AutoPostBackOnFilter="true" FilterControlWidth="90%" CurrentFilterFunction="Contains"
                                                                DataFormatString="{0:@}">
                                                                <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="170"
                                                                    Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" Wrap="False" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                    </MasterTableView>
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                                    <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                                        <Excel FileExtension="xls" Format="Xlsx" />
                                                    </ExportSettings>
                                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                                                        <Selecting AllowRowSelect="True" />
                                                        <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                                            AllowResizeToFit="true" />
                                                    </ClientSettings>
                                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                </telerik:RadGrid>
                                            </td>
                                            <td style="vertical-align: top">
                                                <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png"
                                                    OnClientClick="openWindow(null, null, 'RadWindowFunzioniSpeciali');return false;"
                                                    Style="z-index: 103; left: 495px; top: 38px" TabIndex="1" ToolTip="Aggiungi elemento alla lista" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>

                            </td>
                        </tr>
                    </table>
                </div>

            </div>
            <div class="<%=classetab %> <%=tabdefault12 %>" title="EDIFICI">
                <div style="width: 100%; height: 170px; overflow: auto;">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblMqEsterni" Text="Mq Esterni" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtMqEsterni" runat="server" NumberFormat-DecimalDigits="2"
                                    Width="80px" MinValue="0" Style="text-align: right">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label41" Text="Mq Piloty" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtMqPiloty" runat="server" NumberFormat-DecimalDigits="2"
                                    Width="80px" MinValue="0" Style="text-align: right">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label42" Text="Numero bidoni di carta" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNumBidoniCarta" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="50px" MinValue="0" Style="text-align: right">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label43" Text="Numero bidoni di vetro" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNumBidoniVetro" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="80px" MinValue="0" Style="text-align: right">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label44" Text="Numero bidoni di umido" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="txtNumBidoniUmido" runat="server" NumberFormat-DecimalDigits="0"
                                    Width="80px" MinValue="0" Style="text-align: right">
                                </telerik:RadNumericTextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label45" Text="Spazio Esterno" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkSpazioEsterno" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
            <div class="<%=classetab %> <%=tabdefault13 %>" title="SERVIZI SCALE">
                <div style="width: 100%; height: 170px; overflow: auto;">
                    <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true"
                        Modal="true" Title="Gestione scale" Width="720px" Height="550px"
                        VisibleStatusbar="false" Behaviors="Pin, Maximize, Move, Resize">
                        <ContentTemplate>
                            <div>
                                <asp:Panel runat="server" ID="Panel1" Style="height: 100%;" class="sfondo">
                                    <table>
                                    </table>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </telerik:RadWindow>

                    <telerik:RadGrid ID="dgvScaleEdifici" runat="server" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT"
                        PagerStyle-AlwaysVisible="true" AllowPaging="false" AllowFilteringByColumn="false"
                        EnableLinqExpressions="False" Width="700px" AllowSorting="True" IsExporting="False" Height="150px">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            DataKeyNames="ID" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA" AutoPostBackOnFilter="true"
                                    FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Width="350"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="PULIZIA_SCALE" HeaderText="PULIZIA SCALE">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPuliziaScale" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.PULIZIA_SCALE") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="headerChkScale" runat="server" AutoPostBack="true" OnCheckedChanged="headerChkScale_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="ROTAZIONE_SACCHI" HeaderText="ROT. SACCHI">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRotSacchi" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.ROTAZIONE_SACCHI") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="headerChkRotSacchi" runat="server" AutoPostBack="true" OnCheckedChanged="headerChkRotSacchi_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn DataField="RESA_SACCHI" HeaderText="RESA SACCHI">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkResaSacchi" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.RESA_SACCHI") %>' />
                                    </ItemTemplate>
                                    <FilterTemplate>
                                        <table style="text-align: center">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="headerChkResaSacchi" runat="server" AutoPostBack="true" OnCheckedChanged="headerChkResaSacchi_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </FilterTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" />
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                    </telerik:RadGrid>

                </div>
                <asp:HiddenField ID="hiddenSelTuttiScale" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenSelTuttiRotSacchi" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenSelTuttiResaSacchi" runat="server" Value="0" ClientIDMode="Static" />
            </div>
        <asp:HiddenField ID="txttab" runat="server" Value="1" />
            <asp:HiddenField ID="HiddenIdProgrInterv" runat="server" Value="" />
    </div>
    <asp:HiddenField ID="txtVisibility" runat="server" Value="0" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:ImageButton ID="btnUniImmob" runat="server" ImageUrl="~/NuoveImm/Img_UnitaImmobiliari.png"
        Style="left: 463px; position: absolute; top: 582px; cursor: pointer; height: 12px; z-index: 5;"
        ToolTip="Elenco delle Unità Immobbliari legate a questo edificio"
        OnClientClick="ConfermaEsci();" Visible="False" TabIndex="33" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtDataCostr"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 429px; position: absolute; top: 145px; z-index: 2;"
            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
        Width="1px" ToolTip="Inserire una data valida"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TxtDataRistrut"
            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 557px; position: absolute; top: 145px; z-index: 2;"
            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
        Width="1px" ToolTip="Inserire una data valida"></asp:RegularExpressionValidator>
    <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 558px; position: absolute; top: 89px; width: 36px;"
        ForeColor="Black">Codice</asp:Label>
        <asp:TextBox ID="txtCodEdificio" runat="server" MaxLength="20" Style="left: 595px; position: absolute; top: 89px; z-index: 2; bottom: 588px;"
            TabIndex="3" ReadOnly="True"
        Width="168px"></asp:TextBox>
    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="left: 265px; position: absolute; top: 27px; z-index: 3;"
        Text="Label" Visible="False" Width="436px"></asp:Label>
    &nbsp;
    <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
        Style="left: 9px; position: absolute; top: 27px; z-index: 102; cursor: pointer; width: 50px; height: 12px;"
        ToolTip="Indietro" OnClientClick="ConfermaEsci();"
        TabIndex="35" />
        <asp:TextBox ID="txtNumAscensori" runat="server" MaxLength="2" Style="left: 573px; position: absolute; top: 267px; z-index: 5;"
            Width="29px" TabIndex="20"></asp:TextBox>
    &nbsp;
    <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 573px; position: absolute; top: 253px" Width="55px"
        Height="16px">N. Ascens.</asp:Label>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtNumAscensori"
            ErrorMessage="!" Font-Bold="True" Style="left: 608px; position: absolute; top: 271px; z-index: 2; height: 20px; width: 9px;"
            ValidationExpression="\d{1,50}" ToolTip="Sono ammessi solo numeri"></asp:RegularExpressionValidator>
    <p>
        <asp:ImageButton ID="btnUniCom" runat="server" ImageUrl="~/NuoveImm/Img_UnitàComuni.png"
            Style="left: 563px; position: absolute; top: 581px; cursor: pointer; z-index: 5;"
            ToolTip="Elenco delle Unità Comuni legate a questo edificio" OnClientClick="ConfermaEsci();"
            Visible="False" TabIndex="33" />
        <asp:DropDownList ID="CmbFuoriTerra" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid; z-index: 111; left: 99px; border-left: black 1px solid; border-bottom: black 1px solid; position: absolute; top: 268px"
                TabIndex="11" Width="83px">
        </asp:DropDownList>
        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
        <asp:HiddenField ID="dbLock" runat="server" Value="0" />
        &nbsp;
        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 191px; position: absolute; top: 254px" Width="49px">Mezzanini</asp:Label>
        <asp:HiddenField ID="USCITA" runat="server" Value="0" />
        <asp:HiddenField ID="TextBox1" runat="server" Value="0" />
    </p>
        <div style="z-index: 200; left: 643px; width: 107px; position: absolute; top: 271px; height: 90px; background-color: gainsboro"
            id="Scala">
        <table>
            <tr>
                <td style="width: 75px; vertical-align: top; height: 23px; text-align: left;">
                    <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Style="z-index: 100; left: 8px; top: 200px" Width="64px">Nuova Scala</asp:Label>
                </td>
                    <td style="width: 1px; height: 23px; vertical-align: top; text-align: left;"></td>
            </tr>
            <tr>
                <td style="width: 75px; height: 35px">
                    <asp:TextBox ID="TxtScala" runat="server" MaxLength="4" Style="left: 72px; top: 200px"
                        TabIndex="1" Width="60px"></asp:TextBox>
                </td>
                <td style="width: 1px; height: 35px">
                    <asp:ImageButton ID="BtnOK" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/BtnOK.png"
                        Style="z-index: 103; left: 495px; top: 38px; height: 18px" TabIndex="1" ToolTip="Aggiungi elemento alla lista" />
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        myOpacity = new fx.Opacity('Scala', { duration: 200 });
        //myOpacity.hide();
        if (document.getElementById('TextBox1').value != '2') {
            myOpacity.hide(); ;
        }
    </script>
    </form>
    <script type="text/javascript">

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

    </script>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';


        if (document.getElementById("txtVisibility").value != 0) {
            if (document.getElementById('Tab_Millesimali1_imgAddConv')) {
                document.getElementById('Tab_Millesimali1_imgAddConv').style.visibylity = 'hidden'
            }
            document.getElementById('Tab_Servizi1_imgAddConv').style.visibility = 'hidden'
            document.getElementById('Tab_ImpComuni1_imgAddConv').style.visibility = 'hidden'
            document.getElementById('Tab_NonIspezion1_imgAddConv').style.visibility = 'hidden'

            }

    </script>

    <script language="javascript" type="text/javascript">
            var Uscita;
            Uscita = 0;

            var r = {
                'special': /[\W]/g,
                'quotes': /['\''&'\"']/g,
                'notnumbers': /[^\d]/g,
                'onlynumbers': /[^\d\-\,\.]/g
            }
            function valid(o, w) {
                o.value = o.value.replace(r[w], '');
            }
            function ApriFotoPlan() {

                window.open('FotoImmobile.aspx?T=E&ID=<%=vId %>&I=<%=vIdIndirizzo %>', '');
            }

    </script>

    <script language="javascript" type="text/javascript">

            function scDaAprire() {
                var ins = <%=vId %> 
            if (ins == 0) {
                alert('SALAVARE IL NUOVO EDIFICIO PRIMA DI APRIRE UNA SCHEDA!');
                return;
            }

            if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
                var objSchede = document.getElementById("DrlSchede");
                var objPeriodo = document.getElementById("cmbPeriodo");
                var I = objSchede.options[objSchede.selectedIndex].value
                var D = objPeriodo.options[objPeriodo.selectedIndex].innerText
                if (D != '') {
                    D = D.substring(6) + D.substring(3, 5) + D.substring(0, 2)
                }
                //            

            }
            else {
                var objSchede = document.getElementById("DrlSchede");
                var objPeriodo = document.getElementById("cmbPeriodo");
                var I = objSchede.options[objSchede.selectedIndex].value
                var D = objPeriodo.options[objPeriodo.selectedIndex].text
                if (D != '') {
                    D = D.substring(6) + D.substring(3, 5) + D.substring(0, 2)
                }

            }
            switch (I) {
                case '0':
                    window.open("../Manutenzioni/ScA.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '1':
                    window.open("../Manutenzioni/ScB.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '2':
                    window.open("../Manutenzioni/ScC.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                        break;
                    case '3':
                        window.open("../Manutenzioni/ScD.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                        break;
                    case '4':
                        window.open("../Manutenzioni/ScE.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                           break;
                       case '5':
                           window.open("../Manutenzioni/ScF.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                           break;
                       case '6':
                           window.open("../Manutenzioni/ScG.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                        break;
                    case '7':
                        window.open("../Manutenzioni/ScH.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                        break;
                    case '8':
                        window.open("../Manutenzioni/ScI.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '9':
                    window.open("../Manutenzioni/ScL.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '10':
                    window.open("../Manutenzioni/ScM.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '11':
                    window.open("../Manutenzioni/ScN.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '12':
                    window.open("../Manutenzioni/ScO.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '13':
                    window.open("../Manutenzioni/ScP.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '14':
                    window.open("../Manutenzioni/ScQ.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '15':
                    window.open("../Manutenzioni/ScR.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '16':
                    window.open("../Manutenzioni/ScS.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;
                case '17':
                    window.open("../Manutenzioni/ScT.aspx?ID=<%=vId %>&TIPO=EDIF&DATA=" + D + '&SL=' + document.getElementById("CENS_MANUT_SL").value, '', 'scrollbars=yes, resizable=yes, width=' + screen.width + ', height=' + screen.width + '');
                    break;

                default:
                    alert('Selezionare una scheda da aprire!')
            }

            }

    </script>
    <script language="javascript" type="text/javascript">
        //        window.onbeforeunload = confirmExit; 
        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').innerText = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }

        }

    </script>
</body>
</html>
