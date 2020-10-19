<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnitaLight.aspx.vb" Inherits="CENSIMENTO_AULIGHT_UnitaLight" %>

<%@ Register Src="Tab_Millesimali.ascx" TagName="Tab_Millesimali" TagPrefix="uc5" %>
<%@ Register Src="Tab_AdNormativo.ascx" TagName="Tab_AdNormativo" TagPrefix="uc4" %>
<%@ Register Src="Tab_AdVarConf.ascx" TagName="Tab_AdVarConf" TagPrefix="uc3" %>
<%@ Register Src="Tab_AdDimens.ascx" TagName="Tab_AdDimens" TagPrefix="uc2" %>
<%@ Register Src="Tab_Catastali.ascx" TagName="Tab_Catastali" TagPrefix="uc1" %>
<%@ Register Src="Tab_ValoriMilleismalil.ascx" TagName="Tab_ValoriMilleismalil" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
    <title>Unita Immobiliari</title>
    <script type="text/javascript">
        var Selezionato;
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
    <script type="text/javascript" src="../function.js">
    </script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript" src="../tabber.js"></script>
    <link rel="stylesheet" href="../example.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        function ConfermaEsci() {
            if (document.getElementById('Attesa')) {
                document.getElementById('Attesa').style.visibility = 'visible';
            }
        } 

    </script>
    <script language="javascript" type="text/javascript">
       
        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }
        function AutoDecimal(obj) {
            if (obj.value.indexOf(',') == obj.value.lastIndexOf(',')) {
                if (obj.value.replace(',', '.') > 0) {
                    var a = obj.value.replace(',', '.');
                    a = parseFloat(a).toFixed(4)
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }
            }
            else {
                alert('Controllare il dato inserito')
                document.getElementById(obj.id).value = ''
            }


        }

                
    </script>
    <style type="text/css">
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
        .<%=tabdefault6%>
        {
            width: 836px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <div id="Attesa" 
        
        
        
        style="position: absolute; width: 787px; height: 580px; top: 18px;
        left: 4px; background-color: #f0f0f0; visibility: visible; z-index: 500; display: block;">
        <img src="../../ImmDiv/DivUscitaInCorso2.jpg" alt="caricamento in corso..." />
    </div>
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

        function ApriFotoPlan() {

            
        }

    </script>
    <form id="form1" method="post" runat="server">
    &nbsp; &nbsp;&nbsp;&nbsp;
    <div style="margin-left: 40px">
    </div>
    &nbsp;&nbsp;
    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 186px; position: absolute; top: 179px" Width="49px">Tipologia*</asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 179px" Width="51px">Liv. Piano*</asp:Label>
    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 442px; position: absolute; top: 149px" Width="30px">Scala</asp:Label>
    <asp:DropDownList ID="DrLDisponib" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 72px;
        position: absolute; top: 206px; right: 552px;" TabIndex="9" Width="161px" 
        AutoPostBack="True" Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="cmbPertinenza" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 503px;
        position: absolute; top: 181px; width: 150px;" TabIndex="8" 
        Visible="False" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 231px" Width="57px">Stato Cons.</asp:Label>
    <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 259px" Width="57px">Sede Terr.*</asp:Label>
    &nbsp;
    <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 206px; height: 14px;"
        Width="58px">Disponibilità*</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 237px; position: absolute; top: 231px" Width="57px">Progr.Inter.</asp:Label>
    &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="DrLTipUnita" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 247px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 179px" TabIndex="6" Width="163px" Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="DrLStatoCons" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 72px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 231px" TabIndex="12" Width="161px" 
        Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlFiliale" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 72px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 257px" TabIndex="12" Width="550px" 
        Enabled="False">
    </asp:DropDownList>
    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    <div id="PertinenzeUI" style="z-index: 200; left: 423px; width: 137px; position: absolute;
        top: 68px; height: 193px; background-color: transparent; right: 972px;">
        <br />
        <div style="width: 130px; height: 178px; background-color: silver; text-align: left;">
            <div style="overflow: auto; width: 135px; height: 156px">
                &nbsp;
                <asp:Label ID="LblPertinenze" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 11px; top: 64px"
                    Width="126px"></asp:Label></div>
        </div>
    </div>
    <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 94px" Width="38px">Edificio*</asp:Label>
    <asp:DropDownList ID="DrLEdificio" runat="server" AutoPostBack="True" BackColor="White"
        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border: 1px solid black;
        z-index: 111; left: 73px; position: absolute; top: 94px; width: 690px;" 
        TabIndex="2" Enabled="False">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="DrLStatoCens" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="8pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 295px;
        position: absolute; top: 231px; right: 571px;" TabIndex="13" Width="320px" 
        Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 547px; position: absolute; top: 149px" Width="39px">Interno*</asp:Label>
    <asp:TextBox ID="TxtInterno" runat="server" Style="left: 587px; position: absolute;
        top: 149px; z-index: 10;" MaxLength="3" TabIndex="4" Width="60px" 
        Enabled="False"></asp:TextBox>
    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="DrLTipoLivPiano" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 73px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 179px" TabIndex="5" Width="105px" Enabled="False">
    </asp:DropDownList>
    <asp:ImageButton ID="ImButEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
        Style="left: 741px; position: absolute; top: 27px; z-index: 200; cursor: pointer;"
        ToolTip="Esci" OnClientClick="ConfermaEsci();" TabIndex="50" />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="DrlSc" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 475px;
        position: absolute; top: 149px;" TabIndex="3" Width="66px">
    </asp:DropDownList>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 69px">Complesso</asp:Label>
    <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
        Font-Names="arial" Font-Size="9pt" Height="20px" Style="border: 1px solid black;
        z-index: 111; left: 73px; position: absolute; top: 69px; width: 690px;" 
        TabIndex="1" Enabled="False">
    </asp:DropDownList>
    &nbsp;&nbsp; &nbsp;&nbsp;
    <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="left: 2px; position: absolute; top: 478px; z-index: 12;
        width: 433px;" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 49px" Width="51px"
        ForeColor="Black">Cod. U.I.</asp:Label>
    &nbsp; &nbsp;
    <asp:CheckBox ID="chkPertinenze" runat="server" AutoPostBack="True" Font-Names="Arial"
        Font-Size="8pt" Style="left: 427px; position: absolute; top: 181px; z-index: 10;"
        Text="Pertinenza" TabIndex="7" />
    <br />
    <asp:HyperLink ID="HyLinkPertinenze" onclick="document.getElementById('TextBox1').value!='1';myOpacity.toggle();"
        runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Blue"
        Style="z-index: 5; left: 424px; position: absolute; top: 49px; text-align: left;
        cursor: hand;" Width="63px">Pertinenze</asp:HyperLink>
    &nbsp;&nbsp;&nbsp;<br />
    &nbsp;
    &nbsp;&nbsp;<br />
    <asp:Label ID="txtCodUnitImm" runat="server" Font-Bold="True" Font-Names="Arial"
        Font-Size="9pt" Style="z-index: 100; left: 74px; position: absolute; top: 49px"
        Width="350px"></asp:Label>
        <asp:Label ID="lblSoglia" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 211px; position: absolute; top: 49px; width: 140px;"
        ForeColor="Black" BackColor="Transparent"></asp:Label>
    <asp:Label ID="Label43" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 543px; top: 119px; position: absolute; width: 28px;">Comune</asp:Label>
    <br />
    <asp:DropDownList ID="DrLTipoInd" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="10pt" Style="border: 1px solid black; z-index: 111; left: 73px; top: 120px;
        position: absolute; width: 80px;" TabIndex="21" Enabled="False">
    </asp:DropDownList>
    <asp:DropDownList ID="DrLComune" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Style="border: 1px solid black; z-index: 111; left: 586px; top: 119px;
        position: absolute;" TabIndex="24" AutoPostBack="True" Width="177px" 
        Enabled="False">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 121px" Width="38px">Indirizzo</asp:Label>
    <asp:TextBox ID="TxtLocalità" runat="server" CssClass="CssMaiuscolo" MaxLength="20"
        Style="z-index: 102; left: 200px; position: absolute; top: 148px" TabIndex="22"
        Width="234px" Enabled="False"></asp:TextBox>
    <asp:TextBox ID="TxtCap" runat="server" CssClass="CssMaiuscolo" MaxLength="20" Style="z-index: 102;
        left: 73px; position: absolute; top: 148px" TabIndex="22" Width="75px" 
        Enabled="False"></asp:TextBox>
    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 149px" Width="38px">Cap</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 159px; position: absolute; top: 148px" Width="39px">Località</asp:Label>
    <br />
    <asp:DropDownList ID="DrlDestUso" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 295px;
        position: absolute; top: 206px;" TabIndex="10" AutoPostBack="True" 
        Width="114px" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="LblCanone" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 429px; position: absolute; top: 206px; width: 35px;"
        Visible="False">Canone</asp:Label>
    <asp:Label ID="lblTipoRiscald" runat="server" Font-Bold="True" Font-Names="Arial"
        Font-Size="8pt" Style="z-index: 100; left: 658px; position: absolute; top: 231px; height: 15px; width: 132px;"
        TabIndex="-1"></asp:Label>
<asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 631px; position: absolute; top: 231px" Width="68px"
        Height="16px">Risc.</asp:Label>
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="TextBox1" runat="server" Value="0" />
    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 461px; top: 119px; position: absolute;" 
        Width="21px">N.</asp:Label>
    <asp:HiddenField ID="txtindietro" runat="server" Value="0" />
    <asp:HiddenField ID="USCITA" runat="server" Value="0" />
    <asp:HiddenField ID="maxLE" runat="server" Value="0" />
    <asp:HiddenField ID="maxSLE" runat="server" Value="0" />
    <asp:HiddenField ID="maxPed2SL" runat="server" Value="0" />
    <asp:HiddenField ID="visualizzaSM" runat="server" Value="0" />
    <asp:TextBox ID="TxtDescrInd" runat="server" MaxLength="50" Style="left: 157px; top: 119px;
        z-index: 102; position: absolute;" CssClass="CssMaiuscolo" TabIndex="22" 
        Width="278px" Enabled="False"></asp:TextBox>
    <asp:HiddenField ID="txtUpdate" runat="server" Value="0" />
    <asp:CheckBox ID="ChkCatastali" runat="server" AutoPostBack="True" Style="z-index: 5;
        left: 13px; width: 18px; position: absolute; top: 284px" Text=" " TabIndex="14" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtCanoneUI"
        ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 200; left: 530px;
        position: absolute; top: 206px" ToolTip="Inserire un valore con decimale a precisione doppia"
        ValidationExpression="^\d{1,7}((,|\.)\d{1,2})?$" Width="1px"></asp:RegularExpressionValidator>
    <asp:Label ID="Label42" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 238px; position: absolute; top: 206px; width: 49px;
        right: 320px;">Dest. Uso</asp:Label>
    <asp:TextBox ID="TxtCanoneUI" runat="server" Style="left: 474px; position: absolute;
        top: 206px; z-index: 10;" MaxLength="8" TabIndex="11" Visible="False" Width="58px"></asp:TextBox>
    <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 658px; top: 208px; position: absolute;" Width="60px">Ascensore</asp:Label>
    <asp:DropDownList ID="DrlAscensore" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 722px;
        top: 206px; position: absolute;" TabIndex="44" Width="42px" Enabled="False">
    </asp:DropDownList>
    <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 657px; top: 183px; position: absolute;" Width="65px">Per handicap</asp:Label>
    <asp:DropDownList ID="DrlHandicap" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 722px;
        top: 181px; position: absolute;" TabIndex="44" Width="42px" Enabled="False">
    </asp:DropDownList>
    <asp:TextBox ID="TxtCivicoKilo" runat="server" MaxLength="20" Style="left: 474px;
        top: 119px; z-index: 102; position: absolute;" CssClass="CssMaiuscolo" TabIndex="22"
        Width="60px"></asp:TextBox>
    <asp:HiddenField ID="HFIdIndirizzo" runat="server" />
    <asp:HiddenField ID="idFiliale" runat="server" Value="-1" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="tabber" style="text-align: left; position: absolute; top: 270px; width: 97%">
        <div class="tabbertab <%=tabdefault1%>" title="&nbsp;&nbsp;&nbsp; DATI CATASTALI">
            <uc1:Tab_Catastali ID="Tab_Catastali1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault2%>" title="DIMENSIONI">
            <br />
            <br />
            <uc2:Tab_AdDimens ID="Tab_AdDimens1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault3%>" title="VAR.CONFIG.">
            <br />
            <br />
            <uc3:Tab_AdVarConf ID="Tab_AdVarConf1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault4%>" title="AD.NORMATIVO">
            <br />
            <br />
            <uc4:Tab_AdNormativo ID="Tab_AdNormativo1" runat="server" />
        </div>
        <div class="<%=classetab %> <%=tabdefault5%>" title="MILLESIMALI">
            <br />
            <br />
            <uc6:Tab_ValoriMilleismalil ID="Tab_ValoriMilleismalil1" runat="server" />
            <asp:HiddenField ID="txttab" runat="server" Value="1" />
        </div>
        <div class="<%=classetabSpRev %> <%=tabdefault6%>" title="APPLICAZIONE SP. REVERSIBILI">
            <table style="width: 71%;">
                <tr>
                    <td>
                        <asp:CheckBox ID="ChkAscensori" runat="server" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="12pt" Text="ASCENSORI" Enabled="False" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ChkRiscaldamento" runat="server" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="12pt" Text="RISCALDAMENTO" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ChkSpGenerali" runat="server" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="12pt" Text="SPESE GENERALI" 
                            Enabled="False" />
                    </td>
                </tr>
            </table>
                        <asp:HiddenField ID="HFAscensori" runat="server" Value="0" />
            <asp:HiddenField ID="HFRiscaldamento" runat="server" Value="0" />
            <asp:HiddenField ID="HFSpGenerali" runat="server" Value="0" />

        </div>
    </div>
    <p>
        &nbsp;</p>
    <script language="javascript" type="text/javascript">
        myOpacity = new fx.Opacity('PertinenzeUI', { duration: 200 });
        if (document.getElementById('TextBox1').value != '2') {
            myOpacity.hide();
        }
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        if (document.getElementById('Attesa')) {
            document.getElementById('Attesa').style.visibility = 'hidden';
        }

    </script>
    <asp:HiddenField ID="statodisp" runat="server" />
    <asp:HiddenField ID="Connessione" runat="server" />
    </form>
</body>
</html>
