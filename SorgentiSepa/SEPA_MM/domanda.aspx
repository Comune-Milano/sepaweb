<%@ Reference Control="~/Dom_Richiedente.ascx" %>
<%@ Reference Control="~/Dom_Dichiara.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Dom_Dichiara" Src="Dom_Dichiara.ascx" %>

<%@ Page Language="VB" AutoEventWireup="false" Inherits="CM.domanda" CodeFile="domanda.aspx.vb" EnableSessionState="True" %>

<%@ Register Src="Dom_Requisiti.ascx" TagName="Dom_Requisiti" TagPrefix="uc6" %>
<%@ Register Src="Dom_Abitative_1.ascx" TagName="Dom_Abitative_1" TagPrefix="uc3" %>
<%@ Register Src="Dom_Abitative_2.ascx" TagName="Dom_Abitative_2" TagPrefix="uc4" %>
<%@ Register Src="Note.ascx" TagName="Note" TagPrefix="uc5" %>
<%@ Register Src="Dom_Familiari.ascx" TagName="Dom_Familiari" TagPrefix="uc2" %>
<%@ Register TagPrefix="uc1" TagName="Dom_Richiedente" Src="Dom_Richiedente.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;



    function $onkeydown() {

        if (event.keyCode == 8) {
            //alert('Questo tasto non può essere usato!');
            event.keyCode = 0;

        }
    }

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Domanda</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">
        .CssMaiuscolo {
            FONT-SIZE: 8pt;
            TEXT-TRANSFORM: uppercase;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 16px;
            FONT-VARIANT: normal
        }

        .CssComuniNazioni {
            FONT-SIZE: 8pt;
            TEXT-TRANSFORM: uppercase;
            WIDTH: 166px;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 20px;
            FONT-VARIANT: normal
        }

        .CssPresenta {
            FONT-SIZE: 8pt;
            TEXT-TRANSFORM: uppercase;
            WIDTH: 450px;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 20px;
            FONT-VARIANT: normal
        }

        .CssFamiAbit {
            FONT-SIZE: 8pt;
            WIDTH: 600px;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 20px;
            FONT-VARIANT: normal
        }

        .CssProv {
            FONT-SIZE: 8pt;
            TEXT-TRANSFORM: uppercase;
            WIDTH: 48px;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 20px;
            FONT-VARIANT: normal
        }

        .CssIndirizzo {
            FONT-SIZE: 8pt;
            TEXT-TRANSFORM: uppercase;
            WIDTH: 66px;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 20px;
            FONT-VARIANT: normal
        }

        .CssLabel {
            FONT-SIZE: 8pt;
            COLOR: black;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: times;
            FONT-VARIANT: normal
        }

        .CssLblValori {
            FONT-SIZE: 8pt;
            COLOR: blue;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: arial;
            HEIGHT: 16px;
            FONT-VARIANT: normal
        }

        .CssEtichetta {
            ALIGNMENT: center
        }
        .auto-style2 {
            FONT-SIZE: 8pt;
            COLOR: red;
            LINE-HEIGHT: normal;
            FONT-STYLE: normal;
            FONT-FAMILY: times;
            FONT-VARIANT: normal;
            z-index: 107;
            left: 546px;
            position: absolute;
            top: 92px;
            width: 103px;
            background-color:lemonchiffon;
        }
        .auto-style3 {
            z-index: 999;
            left: 11px;
            position: absolute;
            top: 487px;
        }
        .auto-style4 {
            z-index: 999;
            left: 11px;
            position: absolute;
            top: 463px;
        }
        .auto-style5 {
            z-index: 125;
            left: 467px;
            position: absolute;
            top: 29px;
            height: 12px;
        }
        .auto-style6 {
            z-index: 127;
            left: 368px;
            cursor: pointer;
            position: absolute;
            top: 29px;
        }
        .auto-style7 {
            z-index: 127;
            left: 294px;
            cursor: pointer;
            position: absolute;
            top: 29px;
        }
        .auto-style8 {
            z-index: 127;
            left: 232px;
            cursor: pointer;
            position: absolute;
            top: 29px;
        }
        .auto-style9 {
            cursor: pointer;
            z-index: 136;
            left: 174px;
            position: absolute;
            top: 29px;
        }
        .auto-style10 {
            z-index: 127;
            left: 65px;
            position: absolute;
            top: 29px;
            height: 12px;
        }
        .auto-style11 {
            z-index: 127;
            left: 118px;
            position: absolute;
            top: 29px;
            height: 12px;
        }
    </style>

    <script language="javascript" type="text/javascript">

        window.onbeforeunload = confirmExit;
        window.onunload = Exit;

        function Exit() {

            if (document.getElementById("H1")) {
                if (document.getElementById("H1").value == '1') {
                    if (document.getElementById('imgUscita') != null) {
                        document.getElementById('imgUscita').click();
                    }
                }
            }
        }

        function confirmExit() {
            if (document.getElementById("H1")) {
                if (document.getElementById("H1").value == '1') {
                    if (navigator.appName == 'Microsoft Internet Explorer') {

                        event.returnValue = "Attenzione...Uscendo le modifiche non salvate andranno perse. Si consiglia di salvare prima di uscire!";
                    }
                }
            }
        }

    </script>



</head>
<%--<script language="javascript" type="text/javascript" for="window" event="onbeforeunload">
<!--

if (document.getElementById('H1').value==1) {
    return window_onbeforeunload()
}

// -->
</script>--%>
<script type="text/javascript" src="Funzioni.js"></script>



<script type="text/javascript">
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 250) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 150) / 2 : 0;
    LeftPosition = LeftPosition;
    TopPosition = TopPosition;

    aa = window.open('loading.htm', '', 'height=150,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
</script>

<script type="text/javascript">
    function Indici() {

        window.open("indici.aspx?" + document.getElementById('txtIndici').value, "", "top=0,left=0,width=490,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no");

    }

</script>
<body onload="return AggTabDom(document.getElementById('txtTab').value,document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" bgcolor="#f2f5f1">
    <script type="text/javascript">

        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);

        }


    </script>
    <form id="Form1" method="post" runat="server">
        <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png" CssClass="auto-style8" />
        <asp:Image ID="IMGPREFERENZE" runat="server" ImageUrl="~/NuoveImm/ImgPreferenze.png" Visible="False" CssClass="auto-style7" />
        <asp:Image ID="imgElencostampe" runat="server" ImageUrl="~/NuoveImm/Img_Documentazione.png"
            ToolTip="Elenco Documentazione prodotta" CssClass="auto-style6" />
        &nbsp; &nbsp;
            
            <br />
        <br />
        <br />
        <br />
        <asp:ImageButton ID="btnNote" Style="z-index: 100; left: 460px; position: absolute; top: 391px" runat="server" ImageUrl="p_menu\NOTE_0.gif" Height="21px" Width="42px" CausesValidation="False" TabIndex="7" Visible="False"></asp:ImageButton>
        &nbsp;
                <asp:ImageButton ID="btnAbitative2" Style="z-index: 101; left: 312px; position: absolute; top: 390px" runat="server" ImageUrl="p_menu\ABIT2_0.gif" Height="21px" Width="76px" CausesValidation="False" TabIndex="5" Visible="False"></asp:ImageButton>
        <asp:ImageButton ID="btnAbitative1" Style="z-index: 102; left: 234px; position: absolute; top: 391px" runat="server" ImageUrl="p_menu\ABIT1_0.gif" Height="21px" Width="76px" CausesValidation="False" TabIndex="4" Visible="False"></asp:ImageButton>
        <asp:ImageButton ID="btnFamiliari" Style="z-index: 103; left: 168px; position: absolute; top: 399px" runat="server" ImageUrl="p_menu\FAM_0.gif" Height="21px" Width="64px" CausesValidation="False" TabIndex="3" Visible="False"></asp:ImageButton>
        <asp:ImageButton ID="btnDichiara" Style="z-index: 104; left: 103px; position: absolute; top: 392px" runat="server" ImageUrl="p_menu\DICH_0.gif" Height="21px" Width="63px" CausesValidation="False" TabIndex="2" Visible="False"></asp:ImageButton>
        <asp:Label ID="lblISBAR" Style="z-index: 105; left: 572px; position: absolute; top: 64px" runat="server" Width="70px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori" Font-Bold="True">0</asp:Label>
        <asp:Label ID="lblPGDic" Style="z-index: 106; left: 355px; position: absolute; top: 64px" runat="server" Width="126px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori">pg</asp:Label>
        <asp:Label ID="lblStatoDomanda" runat="server" Height="18px" CssClass="auto-style2" Font-Bold="True" Visible ="false"></asp:Label>
        <asp:Label ID="Label4" Style="z-index: 107; left: 508px; position: absolute; top: 64px" runat="server" Height="18px" Width="60px" CssClass="CssLabel" Font-Bold="True">ISBARC/R</asp:Label>
        <asp:Label ID="Label3" Style="z-index: 108; left: 307px; position: absolute; top: 64px" runat="server" Height="18px" Width="43px" CssClass="CssLabel" Font-Bold="True">N. Dich.</asp:Label>
        <asp:Label ID="Label2" Style="z-index: 109; left: 180px; position: absolute; top: 63px" runat="server" Height="18px" Width="33px" CssClass="CssLabel" Font-Bold="True">Data</asp:Label>
        <asp:Label ID="Label1" Style="z-index: 110; left: 4px; position: absolute; top: 64px" runat="server" Height="18px" Width="31px" CssClass="CssLabel" Font-Bold="True">PG N.</asp:Label>
        <asp:Label ID="lblPG" Style="z-index: 111; left: 66px; position: absolute; top: 64px" runat="server" Width="66px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FFC080" CssClass="CssLblValori">0000000000</asp:Label>
        <asp:TextBox ID="txtDataPG" Style="z-index: 112; left: 210px; position: absolute; top: 62px" runat="server" CssClass="CssMaiuscolo" Columns="10" MaxLength="10" Width="68px" TabIndex="1"></asp:TextBox>
        <asp:ImageButton ID="btnRichiedente" Style="z-index: 113; left: 16px; position: absolute; top: 392px" runat="server" ImageUrl="p_menu\RICH_0.gif" Height="21px" Width="85px" CausesValidation="False" TabIndex="1" Visible="False"></asp:ImageButton>
        <uc1:Dom_Richiedente ID="Dom_Richiedente1" runat="server" Visible="true"></uc1:Dom_Richiedente>
        <uc1:Dom_Dichiara ID="Dom_Dichiara1" runat="server" Visible="true"></uc1:Dom_Dichiara>
        <asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
            BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 114; left: 38px; position: absolute; top: 64px"
            Width="26px">06-1</asp:Label>
        <asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
            BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 115; left: 134px; position: absolute; top: 64px"
            Width="31px">F205</asp:Label>
        &nbsp;
            <uc2:Dom_Familiari ID="Dom_Familiari1" runat="server" Visible="true" />
        <uc3:Dom_Abitative_1 ID="Dom_Abitative_1_1" runat="server" Visible="true" />
        <uc4:Dom_Abitative_2 ID="Dom_Abitative_2_1" runat="server" Visible="true" />
        <uc5:Note ID="Note1" runat="server" Visible="true" />
        &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnRequisiti" runat="server" CausesValidation="False" Height="21px"
                ImageUrl="p_menu\REC_0.gif" Style="z-index: 116; left: 391px; position: absolute; top: 391px"
                Width="66px" TabIndex="6" Visible="False" />
        <uc6:Dom_Requisiti ID="Dom_Requisiti1" runat="server" Visible="true" />
        <asp:ImageButton ID="imgAttendi" runat="server" ImageUrl="IMG/A1.gif" Style="z-index: 117; left: 295px; position: absolute; top: 247px" Visible="False" />
        <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere1.jpg); width: 674px; position: absolute; top: 0px">
            <tr>
                <td style="width: 670px; text-align: right">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Domanda ERP&nbsp;</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="txtTab" runat="server" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lblIdDomanda" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblIdDichiarazione" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="ProgrComponente" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblBando" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblIdBando" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Names="Arial" Font-Size="8pt" Height="31px" Width="368px" Visible="False" Style="z-index: 119; left: 257px; position: absolute; top: 204px">
            <PagerStyle Mode="NumericPages" />
            <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
            <Columns>
                <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA"></asp:BoundColumn>
                <asp:BoundColumn DataField="PERC_INVAL" HeaderText="INVALIDITA"></asp:BoundColumn>
                <asp:BoundColumn DataField="INDENNITA_ACC" HeaderText="ACC"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <img id="i1" src="p_menu/RICH_0.gif" style="z-index: 135; left: 10px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('1',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        <img id="i2" src="p_menu/DICH_0.gif" style="z-index: 129; left: 97px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('2',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        <img id="i3" src="p_menu/FAM_0.gif" style="z-index: 130; left: 162px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('3',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        <img id="i4" src="p_menu/ABIT1_0.gif" style="z-index: 131; left: 228px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('4',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        <img id="i5" src="p_menu/ABIT2_0.gif" style="z-index: 132; left: 306px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('5',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        <img id="i6" src="p_menu/REC_0.gif" style="z-index: 133; left: 385px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('6',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        <img id="i7" src="p_menu/NOTE_0.gif" style="z-index: 134; left: 454px; position: absolute; top: 87px"
            language="javascript" onclick="return AggTabDom('7',document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('fam').style,document.getElementById('abuno').style,document.getElementById('abdue').style,document.getElementById('req').style,document.getElementById('not').style);" />
        &nbsp;&nbsp;
            <asp:TextBox ID="txtIndici" runat="server" Style="z-index: 120; left: 16px; position: absolute; top: 399px"></asp:TextBox>
        &nbsp;
                        <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
                            Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                            Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
                            Visible="TRUE" Width="139px" CssClass="auto-style4"></asp:Label>

        <asp:Label ID="lblPunteggio" runat="server"  
                            Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0"
                            ToolTip="Punteggio"
                            Width="139px" CssClass="auto-style3"></asp:Label>
        <asp:Label ID="Label10" runat="server" Style="z-index: 124; left: 155px; position: absolute; top: 428px"
            Width="497px" Font-Names="arial" Font-Size="8pt"></asp:Label>
        <asp:ImageButton ID="imgUscita" runat="server"
            ImageUrl="~/NuoveImm/Img_Esci.png"
            OnClientClick="aa.close();document.getElementById('H1').value=0;" CssClass="auto-style5" />
        <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" Style="z-index: 126; left: 7px; position: absolute; top: 29px" />
        &nbsp;
            <asp:ImageButton ID="imgStampa" runat="server"
                ImageUrl="~/NuoveImm/Img_Stampa.png" Enabled="False"
                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" CssClass="auto-style10" />
        <asp:ImageButton ID="imgEventi" runat="server"
                ImageUrl="~/NuoveImm/Img_Eventi.png" Enabled="true"
                OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" CssClass="auto-style11" />
        <img src="NuoveImm/Img_Indici.png" id="IMG1" language="javascript" onclick="Indici()" class="auto-style9" />

        <asp:HiddenField ID="H1" runat="server" Value="0" />
        <asp:HiddenField ID="H2" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript">
    document.getElementById('txtIndici').style.visibility = 'hidden';
    aa.close();

    function ElencoStampe(id, codice) {
        window.open('ElencoStampeBandi.aspx?T=1&COD=' + codice + '&ID=' + id, 'Elenco', '');
    }
</script>
</html>
