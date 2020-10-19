<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Imp_Riscaldamento.aspx.vb" Inherits="IMPIANTI_Imp_Riscaldamento" %>

<%@ Register Src="Tab_Termico_Generale.ascx"    TagName="TabGenerale"           TagPrefix="uc1" %>
<%@ Register Src="Tab_Termico_Edifici.ascx"     TagName="Tab_Termico_Edifici"   TagPrefix="uc2" %>
<%@ Register Src="Tab_Termico_Dettagli1.ascx"   TagName="TabDettagli1"          TagPrefix="uc3" %>
<%@ Register Src="Tab_Termico_Pompe.ascx"       TagName="Tab_Termico_Pompe"     TagPrefix="uc4" %>
<%@ Register Src="Tab_Termico_Cert.ascx"        TagName="Tab_Termico_Certificazioni" TagPrefix="uc5" %>
<%@ Register Src="Tab_Termico_Rendimento.ascx"  TagName="Tab_Termico_Rendimento" TagPrefix="uc6" %>
<%@ Register Src="Tab_Manutenzioni.ascx"        TagName="Tab_Manutenzioni"      TagPrefix="uc7" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            
                e.preventDefault();
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';
            }
    }

    
    function $onkeydown() {
 
        if (event.keyCode == 13) {
                event.keyCode = 0;
                document.getElementById('USCITA').value = '0';
                document.getElementById('txtModificato').value = '111';   
        }
    }


    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - 2);
                var dascrivere = a.substring(a.length - 3, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {

                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                }
                risultato = dascrivere + risultato + ',' + decimali
                //document.getElementById(obj.id).value = a.replace('.', ',')
                document.getElementById(obj.id).value = risultato
            }
            else {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >


<head runat="server">
<style type="text/css">
#TIPO1{
font:normal 12px Verdana;
}

#dropmenudiv{
position:absolute;
border:1px solid black;
border-bottom-width: 0;
font:normal 12px Verdana;
line-height:18px;
z-index:100;
}

#dropmenudiv a{
width: 100%;
display: block;
text-indent: 3px;
border-bottom: 1px solid black;
padding: 1px 0;
text-decoration: none;
font-weight: bold;
}

#dropmenudiv a:hover{ /*hover background color*/
background-color: yellow;
}

</style>

<script type="text/javascript">

function PROVA()
{
document.getElementById('USCITA').value='1';
}

//Contents for menu 1
//var menu1=new Array()
//menu1[0]='<a href="javascript:Cessione();">Denuncia Cessione Fabbricato</a>'
//menu1[1]='<a href="TestoContratti/VerbaleConsegna.htm" target="_blank">Verbale di Consegna</a>'
//menu1[2]='<a href="javascript:Ospitalita();">Dichiarazione di Ospitalità</a>'
//menu1[3]='<a href="javascript:Apri();">Visualizza stampe Contratti</a>'

//Contents for menu 2, and so on
//var menu2=new Array()
//menu2[0]='<a href="#">Stampa il contratto</a>'
//menu2[1]='<a href="#">Visualizza stampe</a>'


		
var menuwidth='300px' //default menu width
var menubgcolor='lightyellow'  //menu bgcolor
var disappeardelay=250  //menu disappear speed onMouseout (in miliseconds)
var hidemenu_onclick="yes" //hide menu when user clicks within menu?

/////No further editting needed

var ie4=document.all
var ns6=document.getElementById&&!document.all

if (ie4||ns6)
document.write('<div id="dropmenudiv" onclick="javascript:PROVA();" style="visibility:hidden;width:'+menuwidth+';background-color:'+menubgcolor+'" onMouseover="clearhidemenu()" onMouseout="dynamichide(event)"></div>')

function getposOffset(what, offsettype){
var totaloffset=(offsettype=="left")? what.offsetLeft : what.offsetTop;
var parentEl=what.offsetParent;
while (parentEl!=null){
totaloffset=(offsettype=="left")? totaloffset+parentEl.offsetLeft : totaloffset+parentEl.offsetTop;
parentEl=parentEl.offsetParent;
}
return totaloffset;
}

function showhide(obj, e, visible, hidden, menuwidth){
if (ie4||ns6)
dropmenuobj.style.left=dropmenuobj.style.top=-500
if (menuwidth!=""){
dropmenuobj.widthobj=dropmenuobj.style
dropmenuobj.widthobj.width=menuwidth
}
if (e.type=="click" && obj.visibility==hidden || e.type=="mouseover")
obj.visibility=visible
else if (e.type=="click")
obj.visibility=hidden
}

function iecompattest(){
return (document.compatMode && document.compatMode!="BackCompat")? document.documentElement : document.body
}

function clearbrowseredge(obj, whichedge){
var edgeoffset=0
if (whichedge=="rightedge"){
var windowedge=ie4 && !window.opera? iecompattest().scrollLeft+iecompattest().clientWidth-15 : window.pageXOffset+window.innerWidth-15
dropmenuobj.contentmeasure=dropmenuobj.offsetWidth
if (windowedge-dropmenuobj.x < dropmenuobj.contentmeasure)
edgeoffset=dropmenuobj.contentmeasure-obj.offsetWidth
}
else{
var topedge=ie4 && !window.opera? iecompattest().scrollTop : window.pageYOffset
var windowedge=ie4 && !window.opera? iecompattest().scrollTop+iecompattest().clientHeight-15 : window.pageYOffset+window.innerHeight-18
dropmenuobj.contentmeasure=dropmenuobj.offsetHeight
if (windowedge-dropmenuobj.y < dropmenuobj.contentmeasure){ //move up?
edgeoffset=dropmenuobj.contentmeasure+obj.offsetHeight
if ((dropmenuobj.y-topedge)<dropmenuobj.contentmeasure) //up no good either?
edgeoffset=dropmenuobj.y+obj.offsetHeight-topedge
}
}
return edgeoffset
}

function populatemenu(what){
if (ie4||ns6)
dropmenuobj.innerHTML=what.join("")
}

function dropdownmenu(obj, e, menucontents, menuwidth){
if (window.event) event.cancelBubble=true
else if (e.stopPropagation) e.stopPropagation()
clearhidemenu()
dropmenuobj=document.getElementById? document.getElementById("dropmenudiv") : dropmenudiv
populatemenu(menucontents)

if (ie4||ns6){
showhide(dropmenuobj.style, e, "visible", "hidden", menuwidth)
dropmenuobj.x=getposOffset(obj, "left")
dropmenuobj.y=getposOffset(obj, "top")
dropmenuobj.style.left=dropmenuobj.x-clearbrowseredge(obj, "rightedge")+"px"
dropmenuobj.style.top=dropmenuobj.y-clearbrowseredge(obj, "bottomedge")+obj.offsetHeight+"px"
}

return clickreturnvalue()
}

function clickreturnvalue(){
if (ie4||ns6) return false
else return true
}

function contains_ns6(a, b) {
while (b.parentNode)
if ((b = b.parentNode) == a)
return true;
return false;
}

function dynamichide(e){
if (ie4&&!dropmenuobj.contains(e.toElement))
delayhidemenu()
else if (ns6&&e.currentTarget!= e.relatedTarget&& !contains_ns6(e.currentTarget, e.relatedTarget))
delayhidemenu()
}

function hidemenu(e){
if (typeof dropmenuobj!="undefined"){
if (ie4||ns6)
dropmenuobj.style.visibility="hidden"
}
}

function delayhidemenu(){
if (ie4||ns6)
delayhide=setTimeout("hidemenu()",disappeardelay)
}

function clearhidemenu(){
if (typeof delayhide!="undefined")
clearTimeout(delayhide)
}

if (hidemenu_onclick=="yes")
document.onclick=hidemenu

</script>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<title>IMPIANTO DI RISCALDAMENTO</title>

<script language="javascript" type="text/javascript">
    var Uscita;
Uscita=0;
</script>

<script type="text/javascript">
document.write('<style type="text/css">.tabber{display:none;}<\/style>');
//var tabberOptions = {'onClick':function(){alert("clicky!");}};
var tabberOptions = {


  /* Optional: code to run when the user clicks a tab. If this
     function returns boolean false then the tab will not be changed
     (the click is canceled). If you do not return a value or return
     something that is not boolean false, */

  'onClick': function(argsObj) {

    var t = argsObj.tabber; /* Tabber object */
    var id = t.id; /* ID of the main tabber DIV */
    var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
    var e = argsObj.event; /* Event object */

    document.getElementById('txttab').value=i+1;
  },
  'addLinkId': true
};

</script>


<script type="text/javascript" src="tabber.js"></script>
<link rel="stylesheet" href="example.css" type="text/css" media="screen"/>
    
<script language="javascript" type="text/javascript">

//window.onbeforeunload = confirmExit; 

function EliminaImpianto() {
    var sicuro = confirm('Sei sicuro di voler eliminare questo impianto? Tutti i dati andranno persi.');
    if (sicuro == true) {
    document.getElementById('txtElimina').value='1';
    }
    else
    {
    document.getElementById('txtElimina').value='0'; 
    }
}

function ConfermaAnnullo() {
    var sicuro = confirm('Sei sicuro di voler cancellare questo componente?');
    if (sicuro == true) {
    document.getElementById('TabDettagli1_txtannullo').value='1';
    }
    else
    {
    document.getElementById('TabDettagli1_txtannullo').value='0'; 
    }
}

function ConfermaAnnulloPompe() {
    var sicuro = confirm('Sei sicuro di voler cancellare questa pompa?');
    if (sicuro == true) {
    document.getElementById('Tab_Termico_Pompe_txtannullo').value='1';
    }
    else
    {
    document.getElementById('Tab_Termico_Pompe_txtannullo').value='0'; 
    }
}


function ConfermaAnnulloRendimento() {
    var sicuro = confirm('Sei sicuro di voler cancellare questo controllo?');
    if (sicuro == true) {
    document.getElementById('Tab_Termico_Rendimento_txtannullo').value='1';
    }
    else
    {
    document.getElementById('Tab_Termico_Rendimento_txtannullo').value='0'; 
    }
}


function ConfermaAnnulloEdificio() {
    var sicuro = confirm('Sei sicuro di voler eliminare questo edificio dalla lista?');
    if (sicuro == true) {
    document.getElementById('Tab_Termico_Edifici_txtannullo').value='1';
    }
    else
    {
    document.getElementById('Tab_Termico_Edifici_txtannullo').value='0'; 
    }
}

function ConfermaEsci()
{
 if (document.getElementById('txtModificato').value=='1') 
 {
    var chiediConferma
    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
    if (chiediConferma == false) {
        document.getElementById('txtModificato').value='111';
        //document.getElementById('USCITA').value='0';
    }
 } 
} 

function confirmExit(){
 if (document.getElementById("USCITA").value=='0') {
 if (navigator.appName == 'Microsoft Internet Explorer') 
    {
    event.returnValue = "Attenzione...Uscire dall\'impianto premendo il pulsante ESCI. In caso contrario non sara più possibile accedere all\'impianto per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dall\'impianto premendo il pulsante ESCI. In caso contrario non sara più possibile accedere all\'impianto per un determinato periodo di tempo!";
    }
    }
}


function CompletaData(e,obj) {
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

    function SommaUI(ui,mq,ID,obj)
    {
        
        var TotEDIFICI = document.getElementById('txtSommaEdifici').value.replace(/\./g, '');
        TotEDIFICI = TotEDIFICI.replace(/\,/g, '.');
        
        var totUI = document.getElementById('Tab_Termico_Edifici_txtTotUI').value.replace(/\./g, '');
        totUI = totUI.replace(/\,/g, '.');

        var totMQ = document.getElementById('Tab_Termico_Edifici_txtTotMq').value.replace(/\./g, '');
        totMQ = totMQ.replace(/\,/g, '.');


        var totUI_Extra = document.getElementById('Tab_Termico_Edifici_txtTotUI_Extra').value.replace(/\./g, '');
        totUI_Extra = totUI_Extra.replace(/\,/g, '.');

        var totMQ_Extra = document.getElementById('Tab_Termico_Edifici_txtTotMq_Extra').value.replace(/\./g, '');
        totMQ_Extra = totMQ_Extra.replace(/\,/g, '.');
        
    
        if (obj.checked == true){
            document.getElementById('txtContaEdifici').value = document.getElementById('txtContaEdifici').value + '#' + ID + '#'
            totUI = parseFloat(totUI) + parseFloat(ui);
            totMQ = parseFloat(totMQ) + parseFloat(mq);
            TotEDIFICI=parseFloat(TotEDIFICI)+1;
        }
        else
        {
            document.getElementById('txtContaEdifici').value = document.getElementById('txtContaEdifici').value.replace('#' + ID + '#', '#')
            totUI = parseFloat(totUI) - parseFloat(ui);
            totMQ = parseFloat(totMQ) - parseFloat(mq);
            TotEDIFICI=parseFloat(TotEDIFICI)-1;
        }

        totUI_Extra = parseFloat(totUI_Extra) + parseFloat(totUI);
        totMQ_Extra = parseFloat(totMQ_Extra) + parseFloat(totMQ);

        totUI = totUI.toFixed(0);
        totMQ = totMQ.toFixed(2);
 
        totUI = totUI + '';
        totMQ = totMQ + '';

        TotEDIFICI=TotEDIFICI.toFixed(0);
        TotEDIFICI=TotEDIFICI + '';
 
        document.getElementById('Tab_Termico_Edifici_txtTotUI').value = totUI.replace('.', ',');
        document.getElementById('Tab_Termico_Edifici_txtTotMq').value = totMQ.replace('.', ',');
        AutoDecimal2(document.getElementById('Tab_Termico_Edifici_txtTotUI'));
        AutoDecimal2(document.getElementById('Tab_Termico_Edifici_txtTotMq'));


        totUI_Extra = totUI_Extra.toFixed(0);
        totMQ_Extra = totMQ_Extra.toFixed(2);

        totUI_Extra = totUI_Extra + '';
        totMQ_Extra = totMQ_Extra + '';

        document.getElementById('TabGenerale_txtTotUI').value = totUI_Extra.replace('.', ',');
        document.getElementById('TabGenerale_txtTotMq').value = totMQ_Extra.replace('.', ',');
        AutoDecimal2(document.getElementById('TabGenerale_txtTotUI'));
        AutoDecimal2(document.getElementById('TabGenerale_txtTotMq'));

        document.getElementById('txtSommaEdifici').value=TotEDIFICI.replace('.', ',');
        document.getElementById('TabGenerale_txtTotEdifici').value=TotEDIFICI.replace('.', ',');

        document.getElementById('txtModificato').value='1';
   }


</script>

</head>


<body bgcolor="#f2f5f1" text="#ede0c0">
<script type="text/javascript">
	    if (navigator.appName == 'Microsoft Internet Explorer') 
	    {
	        document.onkeydown = $onkeydown;
	    }
	    else 
	    {
	        window.document.addEventListener("keydown", TastoInvio , true);
	    }
</script>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px; height: 1px;" id="TD_Principale">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                    <table style="width: 760px">
                        <tr>
                            <td style="width: 70px">
                                &nbsp;<asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                    left: 16px; position: static; top: 29px" TabIndex="-1" ToolTip="Indietro" />&nbsp;
                            </td>
                            <td style="width: 130px">
                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'" Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Salva" /></td>
                            <td style="width: 250px">
                                <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';  EliminaImpianto();"
                                    Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                    ToolTip="Elimina l'impianto visualizzato" /></td>
                            <td style="width: 60px">
                                <img id="imgStampa" alt="Stampa Scheda Impianto" border="0" onclick="window.open('Report/ReportTermicoCentralizzato.aspx?ID_IMPIANTO=<%=vIdImpianto %> ','Report', '');"
                                    src="../NuoveImm/Img_Stampa.png" style="left: 616px; cursor: pointer; top: 32px" /></td>
                            <td style="width: 30px">
                                &nbsp; &nbsp;&nbsp;
                            </td>
                            <td style="width: 60px">
                                <img id="ImgEventi" alt="Eventi Scheda Impianto" border="0" onclick="window.open('Report/Eventi.aspx?ID_IMPIANTO=<%=vIdImpianto %> ','Report', '');"
                                    src="../NuoveImm/Img_Eventi.png" style="left: 616px; cursor: pointer; top: 32px" /></td>
                            <td style="width: 100px">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td style="width: 60px">
                                <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125;
                                    left: 600px; position: static; top: 29px" TabIndex="-1" ToolTip="Esci" /></td>
                        </tr>
                    </table>
            <table style="width: 760px">
                <tr>
                    <td>
        <asp:Label ID="lbl_COMPLESSO" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="72px" TabIndex="-1">Complesso *</asp:Label></td>
                    <td>
        <asp:DropDownList  ID="cmbComplesso" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 56px" Width="210px" TabIndex="1" AutoPostBack="True">
        </asp:DropDownList></td>
                    <td>
                        &nbsp;&nbsp; &nbsp; &nbsp;</td>
                    <td>
        <asp:Label ID="lbl_EDIFICIO" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: -46px; top: -302px" Width="60px" TabIndex="-1">Edificio *</asp:Label></td>
                    <td>
        <asp:DropDownList ID="DrLEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 24px" TabIndex="2"
            Width="380px">
        </asp:DropDownList></td>
                </tr>
            </table>
                    <table style="width: 760px">
                        <tr>
                            <td>
                                <asp:Label ID="Lbl_DENOMINAZIONE" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: -46px; top: -222px"
                                    TabIndex="-1" Width="72px">Denominazione Impianto *</asp:Label></td>
                            <td>
                                            <asp:TextBox ID="txtDenominazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="500" Style="left: 136px; top: 112px; height: 30px" TabIndex="3" TextMode="MultiLine"
                                                Width="490px"></asp:TextBox></td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" TabIndex="-1" Text="Codice Impianto"></asp:Label><br />
                                <asp:TextBox ID="txtCodImpianto" runat="server" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" TabIndex="-1" Width="168px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblDitta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="72px">Ditta Installatrice</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDitta" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="100"
                                                Style="left: 80px; top: 88px;" TabIndex="4" TextMode="MultiLine"
                                                Width="344px" Height="30px"></asp:TextBox>&nbsp;
                                            <asp:Label ID="LblAnnoRealizzazione" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                                                TabIndex="-1" Width="56px">Data di consegna</asp:Label>
                                <asp:TextBox ID="txtAnnoRealizzazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Style="left: 504px; top: 152px" TabIndex="5" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAnnoRealizzazione"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="496px" style="text-align: right"></asp:RegularExpressionValidator></td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="Lbl_TIPOLOGIA_USO" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 152px"
                                    TabIndex="-1" Width="75px">Tipologia d'uso</asp:Label><asp:DropDownList ID="cmbTipoUso"
                                        runat="server" BackColor="White" Font-Names="arial" Font-Size="8pt" Style="border-right: black 1px solid;
                                        border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid;
                                        border-bottom: black 1px solid; top: 144px" TabIndex="6" Width="168px">
                                    </asp:DropDownList></td>
                        </tr>
                    </table>
                     <div class="tabber" id="tab1">
                        <div class="tabbertab <%=Tabber1%>"  style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Dettagli</h2>
                            <uc1:TabGenerale ID="TabGenerale" runat="server"  Visible=" true" />         
                        </div>
                        <div class="tabbertab <%=Tabber2%>"  style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Edifici</h2>
                            <uc2:Tab_Termico_Edifici ID="Tab_Termico_Edifici" runat="server"  Visible=" true" />         
                        </div>                        
                        <div <%=Visibile %> class="tabbertab <%=Tabber3%>" style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Generatori/Bruciatori</h2>
                            <uc3:TabDettagli1 ID="TabDettagli1" runat="server"  Visible="true"  />  &nbsp;
                        </div>     
                        <div <%=Visibile %> class="tabbertab <%=Tabber4%>" style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Pompe</h2>
                            <uc4:Tab_Termico_Pompe ID="Tab_Termico_Pompe" runat="server"  Visible="true"  />  &nbsp;
                        </div>     
                        <div <%=Visibile %> class="tabbertab <%=Tabber5%>" style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Certificazioni</h2>
                            <uc5:Tab_Termico_Certificazioni ID="Tab_Termico_Certificazioni" runat="server"  Visible="true"  />  &nbsp;
                        </div>     
                        <div <%=Visibile %> class="tabbertab <%=Tabber6%>" style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Rendimento di Combustione</h2>
                            <uc6:Tab_Termico_Rendimento ID="Tab_Termico_Rendimento" runat="server"  Visible="true"  />  &nbsp;
                        </div>     
                        <div class="<%=TabberHide%> <%=Tabber7%>"  style="BACKGROUND-COLOR: white;width:775px"> 
                            <h2>Manutenzioni</h2>
                            <uc7:Tab_Manutenzioni ID="Tab_Manutenzioni" runat="server"  Visible=" true" />         
                        </div>                        
                    </div>
            </td>
            </tr>
        </table>
        <br />
<br />
        <asp:TextBox ID="USCITA"        runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtModificato" runat="server" BackColor="White" BorderStyle="None" ForeColor="White" Style="left: 0px; position: absolute; top: 200px; z-index: -1;">0</asp:TextBox>
        <asp:TextBox ID="txtElimina" runat="server" BackColor="White" BorderStyle="None" ForeColor="White" Style="z-index: -1; left: 0px; position: absolute; top: 200px">0</asp:TextBox>
        
        <asp:TextBox ID="txtIdImpianto" runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txttab"        runat="server" ForeColor="White" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1">1</asp:TextBox>
        <asp:TextBox ID="txtindietro"   runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute; top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA"  runat="server" Style="z-index: -1; left: 0px; position: absolute; top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>        

        <asp:TextBox ID="txtTIPO_IMPIANTO"      runat="server" Style="left: 0px; position: absolute; top: 50px; z-index: -1; color: white;" TabIndex="-1" Visible="False" Width="5px"></asp:TextBox>
        <asp:HiddenField ID="txtSommaEdifici" runat="server" />
        <asp:HiddenField ID="txtContaEdifici" runat="server" />
        
        &nbsp;<br />
        <br />
        &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
      
    </div>
        <br />
        &nbsp; &nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp;<br />
        <br />
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;<br />
            <br />
        &nbsp;
    </form>

<script type="text/javascript">
window.focus();
self.focus();

</script>

</body>

</html>
