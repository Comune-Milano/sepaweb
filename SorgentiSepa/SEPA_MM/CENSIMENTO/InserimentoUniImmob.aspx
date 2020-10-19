<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimentoUniImmob.aspx.vb"
    Inherits="CENSIMENTO_InserimentoUniImmob" %>

<%@ Register Src="Tab_Millesimali.ascx" TagName="Tab_Millesimali" TagPrefix="uc5" %>
<%@ Register Src="Tab_AdNormativo.ascx" TagName="Tab_AdNormativo" TagPrefix="uc4" %>
<%@ Register Src="Tab_AdVarConf.ascx" TagName="Tab_AdVarConf" TagPrefix="uc3" %>
<%@ Register Src="Tab_AdDimens.ascx" TagName="Tab_AdDimens" TagPrefix="uc2" %>
<%@ Register Src="Tab_Catastali.ascx" TagName="Tab_Catastali" TagPrefix="uc1" %>
<%@ Register Src="Tab_ValoriMilleismalil.ascx" TagName="Tab_ValoriMilleismalil" TagPrefix="uc6" %>
<%@ Register Src="~/CENSIMENTO/Tab_Ril_Manutentive.ascx" TagName="Tab_Ril_Manutentive"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head runat="server">
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
    <script type="text/javascript" src="function.js">
    </script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" href="example.css" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                }
                else {
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'visible';
                    }
                }
            }
            else {
                if (document.getElementById('Attesa')) {
                    document.getElementById('Attesa').style.visibility = 'visible';
                }
            }
        } 

    </script>
    <script language="javascript" type="text/javascript">
        function ConfermaSalva() {
            if (document.getElementById('ChkCatastali').checked == false) {
                var chiediConferma
                if (document.getElementById('Tab_Catastali1_TxtSub').value == '') {
                    chiediConferma = window.confirm("Attenzione...Si desidera salvare l'unità senza dati catastali.CONTINUARE?");

                }
                else {
                    chiediConferma = window.confirm("Attenzione...Verranno cancellati i dati catastali dell'unità.CONTINUARE?");
                }

                if (chiediConferma == false) {
                    document.getElementById('txtUpdate').value = '1';
                }
                else {
                    document.getElementById('txtUpdate').value = '0';
                }

            }
            else {
                document.getElementById('txtUpdate').value = '0';
            }

        }

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
    <style>
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
        .<%=tabdefault6%>
        {
            width: 836px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <div id="Attesa" 
        style="position: absolute; width: 787px; height: 580px; top: 18px;
        left: 4px; background-color: #f0f0f0; visibility: visible; z-index: 500; display: block;">
        <img src="../ImmDiv/DivUscitaInCorso2.jpg" alt="caricamento in corso..." />
    </div>
    <script type="text/javascript">
        function CompletaData(e, obj) {
            var sKeyPressed;
            var n;
            sKeyPressed = (window.event) ? event.keyCode : e.which;
            if ((sKeyPressed < 48) || (sKeyPressed > 57)) {
                if ((sKeyPressed != 8) && (sKeyPressed != 0)) {
                    if (window.event) {
                        if (navigator.appName == 'Microsoft Internet Explorer') {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        e.preventDefault();
                    };
                };
            }
            else {
                if (obj.value.length == 0) {
                    if ((sKeyPressed < 48) || (sKeyPressed > 51)) {
                        if (window.event) {
                            if (navigator.appName == 'Microsoft Internet Explorer') {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        }
                        else {
                            e.preventDefault();
                        };
                    };
                }
                else if (obj.value.length == 1) {
                    if (obj.value == 3) {
                        if (sKeyPressed < 48 || sKeyPressed > 49) {
                            if (window.event) {
                                if (navigator.appName == 'Microsoft Internet Explorer') {
                                    event.keyCode = 0;
                                }
                                else {
                                    e.preventDefault();
                                };
                            }
                            else {
                                e.preventDefault();
                            };
                        };
                    };
                }
                else if (obj.value.length == 2) {
                    if ((sKeyPressed < 48) || (sKeyPressed > 49)) {
                        if (window.event) {
                            if (navigator.appName == 'Microsoft Internet Explorer') {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        }
                        else {
                            e.preventDefault();
                        };
                    }
                    else {
                        obj.value += "/";
                    };
                }
                else if (obj.value.length == 4) {
                    n = obj.value.substr(3, 1);
                    if (n == 1) {
                        if ((sKeyPressed < 48) || (sKeyPressed > 50)) {
                            if (window.event) {
                                if (navigator.appName == 'Microsoft Internet Explorer') {
                                    event.keyCode = 0;
                                }
                                else {
                                    e.preventDefault();
                                };
                            }
                            else {
                                e.preventDefault();
                            };
                        };
                    };
                }
                else if (obj.value.length == 5) {
                    obj.value += "/";
                }
                else if (obj.value.length > 9) {
                    var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                    if (selText.length == 0) {
                        if (window.event) {
                            if (navigator.appName == 'Microsoft Internet Explorer') {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            };
                        }
                        else {
                            e.preventDefault();
                        };
                    };
                };
            };
        };

        function ApriFotoPlan() {

            window.open('FotoImmobile.aspx?T=U&ID=<%=vId %>&I=<%=vIdIndirizzo %>', '');
        }

    </script>
    <form id="form1" method="post" runat="server">
    &nbsp; &nbsp;&nbsp;&nbsp;
    <div style="margin-left: 40px">
        <asp:ImageButton ID="ImgButSave" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="z-index: 102; left: 60px; position: absolute; top: 27px; cursor: pointer;
            width: 50px; height: 12px;" ToolTip="Salva" OnClientClick="ConfermaSalva();"
            TabIndex="43" />
    </div>
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
        Style="z-index: 100; left: 9px; position: absolute; top: 27px; cursor: pointer;
        height: 12px;" ToolTip="Indietro" OnClientClick="ConfermaEsci();" TabIndex="41" />
    &nbsp;&nbsp;
    <asp:ImageButton ID="ImgButStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
        Style="z-index: 103; left: 105px; position: absolute; top: 27px; cursor: pointer;
        right: 808px;" ToolTip="Stampa" OnClientClick="ConfermaEsci();" TabIndex="44"
        Visible="False" />
    <asp:ImageButton ID="btnFoto" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
        Style="z-index: 103; left: 154px; position: absolute; top: 27px; cursor: pointer;"
        ToolTip="Foto e Planimetrie" TabIndex="45" Visible="False" CausesValidation="False"
        OnClientClick="ApriFotoPlan();return false;" />
    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<a href="javascript:" onclick="opener.form1.submit();history.go(document.getElementById('txtindietro').value); return false"></a>
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
        position: absolute; top: 206px; right: 552px;" TabIndex="9" Width="161px" AutoPostBack="True">
    </asp:DropDownList>
    <asp:DropDownList ID="cmbPertinenza" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 503px;
        position: absolute; top: 181px; width: 150px;" TabIndex="8" Visible="False">
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
        position: absolute; top: 179px" TabIndex="6" Width="163px">
    </asp:DropDownList>
    <asp:DropDownList ID="DrLStatoCons" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 72px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 231px" TabIndex="12" Width="161px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlFiliale" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 72px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 257px" TabIndex="12" Width="450px">
    </asp:DropDownList>
    <asp:Label ID="lblOSMI" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="8pt" 
            Style="z-index: 100; left: 531px; position: absolute; top: 259px; width: 246px; height: 14px;" 
            ForeColor="#0000CC" Visible="False">Z.OSMI</asp:Label>
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
        z-index: 111; left: 73px; position: absolute; top: 94px; width: 690px;" TabIndex="2">
    </asp:DropDownList>
    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="DrLStatoCens" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="8pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 295px;
        position: absolute; top: 231px; right: 571px;" TabIndex="13" Width="470px">
    </asp:DropDownList>
    <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 547px; position: absolute; top: 149px" Width="39px">Interno*</asp:Label>
    <asp:TextBox ID="TxtInterno" runat="server" Style="left: 587px; position: absolute;
        top: 149px; z-index: 10;" MaxLength="3" TabIndex="4" Width="60px"></asp:TextBox>
    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:DropDownList ID="DrLTipoLivPiano" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
        z-index: 111; left: 73px; border-left: black 1px solid; border-bottom: black 1px solid;
        position: absolute; top: 179px" TabIndex="5" Width="105px">
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
        z-index: 111; left: 73px; position: absolute; top: 69px; width: 690px;" TabIndex="1">
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
    <img id="ImgEventi" alt="Eventi" border="0" onclick="window.open('Eventi.aspx?ID=<%=vId %> &CHIAMA=UI','Eventi', '');"
        src="../NuoveImm/Img_Eventi.png" style="left: 684px; cursor: pointer; position: absolute;
        z-index: 200; top: 27px" />
    <img id="imgVisualizzaStatoManutentivo" alt="Visualizza stato manutentivo" border="0"
        onclick="VisualizzaStatoM();" src="../NuoveImm/Img_Verifica_Stato_Manutentivo.png"
        style="left: 534px; cursor: pointer; position: absolute; z-index: 200; top: 27px" />
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
        position: absolute; width: 80px;" TabIndex="21">
    </asp:DropDownList>
    <asp:DropDownList ID="DrLComune" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Style="border: 1px solid black; z-index: 111; left: 586px; top: 119px;
        position: absolute;" TabIndex="24" AutoPostBack="True" Width="177px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 121px" Width="38px">Indirizzo</asp:Label>
    <asp:TextBox ID="TxtLocalità" runat="server" CssClass="CssMaiuscolo" MaxLength="20"
        Style="z-index: 102; left: 200px; position: absolute; top: 148px" TabIndex="22"
        Width="234px"></asp:TextBox>
    <asp:TextBox ID="TxtCap" runat="server" CssClass="CssMaiuscolo" MaxLength="20" Style="z-index: 102;
        left: 73px; position: absolute; top: 148px" TabIndex="22" Width="75px"></asp:TextBox>
    <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 11px; position: absolute; top: 149px" Width="38px">Cap</asp:Label>
    <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 159px; position: absolute; top: 148px" Width="39px">Località</asp:Label>
    <br />
    <asp:DropDownList ID="DrlDestUso" runat="server" BackColor="White" Font-Names="arial"
        Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111; left: 295px;
        position: absolute; top: 206px;" TabIndex="10" AutoPostBack="True" Width="114px">
    </asp:DropDownList>
    <asp:Label ID="LblCanone" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 429px; position: absolute; top: 206px; width: 35px;"
        Visible="False">Canone</asp:Label>
    <asp:Label ID="lblTipoRiscald" runat="server" Font-Bold="True" Font-Names="Arial"
        Font-Size="8pt" Style="z-index: 100; left: 475px; position: absolute; top: 208px;
        height: 15px; width: 132px;" TabIndex="-1"></asp:Label>
    <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 431px; position: absolute; top: 208px" Width="68px"
        Height="16px">Risc.</asp:Label>
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="TextBox1" runat="server" Value="0" />
    <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
        Style="z-index: 100; left: 461px; top: 119px; position: absolute;" Width="21px">N.</asp:Label>
    <asp:HiddenField ID="txtindietro" runat="server" Value="0" />
    <asp:HiddenField ID="USCITA" runat="server" Value="0" />
    <asp:HiddenField ID="maxLE" runat="server" Value="0" />
    <asp:HiddenField ID="maxSLE" runat="server" Value="0" />
    <asp:HiddenField ID="maxPed2SL" runat="server" Value="0" />
    <asp:HiddenField ID="visualizzaSM" runat="server" Value="0" />
    <asp:TextBox ID="TxtDescrInd" runat="server" MaxLength="50" Style="left: 157px; top: 119px;
        z-index: 102; position: absolute;" CssClass="CssMaiuscolo" TabIndex="22" Width="278px"></asp:TextBox>
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
        <div class="tabbertab <%=tabdefault1%>" title="&nbsp;&nbsp;&nbsp;&nbsp;CATASTALI">
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
        <div class="<%=classetabSpRev %> <%=tabdefault6%>" title="SP.REVERSIBILI">
            <table style="width: 71%;">
                <tr>
                    <td>
                        <asp:CheckBox ID="ChkAscensori" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="12pt" Text="ASCENSORI" />
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
                        <asp:CheckBox ID="ChkRiscaldamento" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="12pt" Text="RISCALDAMENTO" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ChkSpGenerali" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="12pt" Text="SPESE GENERALI" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="HFAscensori" runat="server" Value="0" />
            <asp:HiddenField ID="HFRiscaldamento" runat="server" Value="0" />
            <asp:HiddenField ID="HFSpGenerali" runat="server" Value="0" />
        </div>
        <div class="<%=classetab %> <%=tabdefault7%>" title="RIL.MANUTENT.">
            <br />
            <br />
            <uc7:Tab_Ril_Manutentive ID="Tab_Ril_Manutentive" runat="server" />
        </div>
        <div class="tabbertab <%=tabdefault8%>" title="REGIONE" style="height: 250px">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Dest. Uso RL</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDestUsoRL" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111;"
                            Width="175px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Alloggio Escluso</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAlloggioEscluso" runat="server" BackColor="White" Font-Names="arial"
                            Font-Size="9pt" Height="20px" Style="border: 1px solid black; z-index: 111;"
                            Width="60px" AutoPostBack="True">
                            <asp:ListItem Selected="True" Text="No" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Sì" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Data Esclusione</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDataEsclusione" runat="server" EnableViewState="False" Font-Names="Arial"
                                        Font-Size="8pt" MaxLength="10" TabIndex="-1" Width="70px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;&nbsp;
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataEsclusione"
                                        ErrorMessage="!" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        Font-Names="arial" Font-Size="8pt" ForeColor="#CC0000" ToolTip="Modificare la data di Esclusione"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt">Nr. Provvedimento</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtNrProvvedimentoEsclusione" runat="server" Font-Names="Arial"
                            Font-Size="8pt" MaxLength="50" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="11">
                        <table style="width: 645px; height: 177px">
                            <tr>
                                <td style="vertical-align: top; width: 589px; height: 81px; text-align: left">
                                    <div style="border: medium solid #ccccff; left: 0px; vertical-align: top; overflow: auto;
                                        width: 703px; top: 0px; height: 170px; text-align: left">
                                        <asp:DataGrid ID="dataGridNote" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                                            Font-Size="8pt" Width="97%" PageSize="13" Style="z-index: 105; left: 0px; top: 48px"
                                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" GridLines="None">
                                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White"
                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                ForeColor="#0000C0"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ID_RIFERIMENTO" HeaderText="ID_RIF" ReadOnly="True" Visible="False">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATAORA" HeaderStyle-HorizontalAlign="Center" HeaderText="DATA INSERIM.">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Width="15%" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATA_EVENTO" HeaderStyle-HorizontalAlign="Center" HeaderText="DATA EVENTO">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Width="15%" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NOTE" HeaderStyle-HorizontalAlign="Center" HeaderText="NOTE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Width="55%" Wrap="true" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="OPERATORE" HeaderStyle-HorizontalAlign="Center" HeaderText="OPERATORE">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Width="15%" />
                                                </asp:BoundColumn>
                                            </Columns>
                                            <ItemStyle Height="20px" />
                                            <PagerStyle Mode="NumericPages"></PagerStyle>
                                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:DataGrid></div>
                                </td>
                                <td valign="top">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgAggNota" runat="server" ImageUrl="~/ANAUT/img/ImgAdd.png"
                                                    ToolTip="Aggiungi Nota" Style="width: 16px; cursor: pointer;" OnClientClick="AggiungiNote();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="ImgModifyNota" runat="server" ImageUrl="~/ANAUT/img/Pencil-icon.png"
                                                    ToolTip="Modifica Nota" Style="width: 16px; cursor: pointer;" OnClientClick="ModificaNote();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">

            myOpacity = new fx.Opacity('PertinenzeUI', { duration: 200 });
            //myOpacity.hide();
            if (document.getElementById('TextBox1').value!='2') {
                 myOpacity.hide();
             }

             VisualizzaStatoManutentivo();


            function VisualizzaStatoManutentivo() {
             if (document.getElementById('visualizzaSM').value=='1') {
             
             }
             else {
                        document.getElementById('imgVisualizzaStatoManutentivo').style.visibility = 'hidden';
                        document.getElementById('imgVisualizzaStatoManutentivo').style.position = 'absolute';
                        document.getElementById('imgVisualizzaStatoManutentivo').style.left = '-100px';
                        document.getElementById('imgVisualizzaStatoManutentivo').style.display = 'none';

             }
            }

             function VisualizzaStatoM() {
              if (<%=vId %> != '-1') {
                if (document.getElementById('txtModificato').value != '111') {
                    if (document.getElementById('maxLE').value != '1' && document.getElementById('maxPed2SL').value != '1') {
                        if (document.getElementById('maxSLE').value == '1') {
                            window.open('VerificaSManutentivo.aspx?F='+ document.getElementById('Connessione').value +'&A=0&L=2&ID=<%=vId %>', '');
                        }
                        else {
                            if (document.getElementById('txtModificato').value == '1') {
                                alert('Sono state effettuate delle modifiche. Salvare prima di richiamare il modulo!');
                            }
                            else {
                            window.open('VerificaSManutentivo.aspx?F='+ document.getElementById('Connessione').value +'&A=1&ID=<%=vId %>', '');
                            }
                        }
                    }
                    else {
                    window.open('VerificaSManutentivo.aspx?F='+ document.getElementById('Connessione').value +'&A=0&ID=<%=vId %>', '');
                    }
                }
                else {
                document.getElementById('txtModificato').value='1';
                document.getElementById('USCITA').value='0';
                }
             }
            }
       
         function MyDialogArguments() {
            this.Sender = null;
            this.StringValue = "";
        }

       function AggiungiNote() {
    
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = '';
        dialogArgs.Sender = window;
        var dialogResults = window.showModalDialog('../InserimentoNote.aspx?PROV=2' + '&IDRIF=' + <%=vID %> , window, 'status:no;dialogWidth:450px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
        
//        if (dialogResults != undefined) {
//            if (dialogResults == '1') {
//                document.getElementById('imgSalva').click();
//            }
//            if (dialogResults == '2') {
//                document.getElementById('txtModificato').value='1';
//                }
//           }
        }

        function ModificaNote() {
    
        if (document.getElementById('idNota').value == '-1') {
            alert('Selezionare una riga dalla lista!');
        }
        else {
        dialogArgs = new MyDialogArguments();
        dialogArgs.StringValue = '';
        dialogArgs.Sender = window;
        var dialogResults = window.showModalDialog('../InserimentoNote.aspx?MOD=1&IDNOTA=' + document.getElementById('idNota').value + '&PROV=2' + '&IDRIF=' + <%=vId %> , window, 'status:no;dialogWidth:450px;dialogHeight:250px;dialogHide:true;help:no;scroll:no');
        
//        if (dialogResults != undefined) {
//            if (dialogResults == '1') {
//                document.getElementById('imgSalva').click();
//            }
//            if (dialogResults == '2') {
//                document.getElementById('txtModificato').value='1';
//            }
//        }
       }
    }
       




    </script>
    <p>
        <asp:Label ID="Lbllocativo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="cursor: pointer; z-index: 100; left: 491px; position: absolute; top: 49px;"
            ForeColor="Blue" Width="91px">Valore locativo</asp:Label>
        <asp:Label ID="LblDatiContratt" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="cursor: pointer; z-index: 100; left: 583px; position: absolute;
            top: 49px;" Height="14px" Width="77px">Dati contrattuali</asp:Label>
    </p>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        if (document.getElementById('Attesa')) {
            document.getElementById('Attesa').style.visibility = 'hidden';
        }

    </script>
    <asp:HiddenField ID="statodisp" runat="server" />
    <asp:HiddenField ID="Connessione" runat="server" />
    <asp:HiddenField ID="idNota" runat="server" Value="-1" />
        <asp:HiddenField ID="operatoreComune" runat="server" Value="0" />
        <asp:HiddenField ID="modUI" runat="server" Value="0" />
    </form>
</body>
</html>
