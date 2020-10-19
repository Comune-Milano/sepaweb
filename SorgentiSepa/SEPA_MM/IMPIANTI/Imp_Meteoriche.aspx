<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Imp_Meteoriche.aspx.vb" Inherits="IMPIANTI_Imp_Meteoriche" %>

<%@ Register Src="Tab_Meteoriche_Generale.ascx" TagName="Tab_Meteoriche_Generale" TagPrefix="uc1" %>
<%@ Register Src="Tab_Meteoriche_Dettagli.ascx" TagName="Tab_Meteoriche_Dettagli" TagPrefix="uc2" %>
<%@ Register Src="Tab_Manutenzioni.ascx"        TagName="Tab_Manutenzioni"        TagPrefix="uc3" %>


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

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >


<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<title>IMPIANTO - STAZIONE SOLLEVAMENTO ACQUE METEORICHE</title>

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
    var sicuro = confirm('Sei sicuro di voler cancellare questa pompa?');
    if (sicuro == true) {
    document.getElementById('Tab_Meteoriche_Dettagli_txtannullo').value='1';
    }
    else
    {
    document.getElementById('Tab_Meteoriche_Dettagli_txtannullo').value='0'; 
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
    event.returnValue = "Attenzione...Uscire dall\'impianto premendo il pulsante ESCI. In caso contrario non sara pi� possibile accedere all'impianto per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dall\'impianto premendo il pulsante ESCI. In caso contrario non sara pi� possibile accedere all'impianto per un determinato periodo di tempo!";
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
                                <img id="imgStampa" alt="Stampa Scheda Impianto" border="0" onclick="window.open('Report/ReportMeteoriche.aspx?ID_IMPIANTO=<%=vIdImpianto %> ','Report', '');"
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
            ForeColor="Black" Style="z-index: 100; left: -46px; top: -302px" Width="60px" TabIndex="-1">Edificio</asp:Label></td>
                    <td>
        <asp:DropDownList ID="DrLEdificio" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 88px; border-left: black 1px solid;
            border-bottom: black 1px solid; top: 24px" TabIndex="2"
            Width="380px" AutoPostBack="True">
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
                                                MaxLength="500" Style="left: 136px; top: 112px;" TabIndex="3" TextMode="MultiLine"
                                                Width="344px" Height="30px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" TabIndex="-1" Text="Codice Impianto" Width="136px"></asp:Label><asp:TextBox ID="txtCodImpianto" runat="server" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" TabIndex="-1" Width="165px"></asp:TextBox></td>
                            <td rowspan="2">
                                <asp:Label ID="lblElementiServiti" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" TabIndex="-1" Text="Elementi Serviti"></asp:Label><br />
                                <div style="border-right: navy 1px solid; border-top: navy 1px solid; visibility: visible;
                                    overflow: auto; border-left: navy 1px solid; width: 80px; border-bottom: navy 1px solid;
                                    height: 90px">
                                    <asp:CheckBoxList ID="CheckBoxScale" runat="server" BorderColor="Black"
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="96px" TabIndex="4"
                                        Width="56px">
                                    </asp:CheckBoxList></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LblDitta" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="72px">Ditta Installatrice</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDitta" runat="server" Font-Names="Arial" Font-Size="9pt" MaxLength="100"
                                                Style="left: 80px; top: 88px;" TabIndex="5" TextMode="MultiLine"
                                                Width="344px" Height="30px"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                            <td>
                                            <asp:Label ID="LblAnnoRealizzazione" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 392px; top: 152px"
                                                TabIndex="-1" Width="96px">Data Installazione</asp:Label><asp:TextBox ID="txtAnnoRealizzazione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Style="left: 504px; top: 152px" TabIndex="6" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAnnoRealizzazione"
                                                    Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)" Font-Bold="False"
                                                    Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="152px" style="text-align: right"></asp:RegularExpressionValidator></td>
                        </tr>
                    </table>
                       <div class="tabber" id="tab1">
                            <div class="tabbertab <%=Tabber1%>"  style="BACKGROUND-COLOR: white;width:775px"> 
                                <h2>Dettagli</h2>
                                <uc1:Tab_Meteoriche_Generale ID="Tab_Meteoriche_Generale" runat="server"  Visible=" true" />         
                            </div>
                            <div class="tabbertab <%=Tabber2%>"  style="BACKGROUND-COLOR: white;width:775px"> 
                                <h2>Pompe</h2>
                                <uc2:Tab_Meteoriche_Dettagli ID="Tab_Meteoriche_Dettagli" runat="server"  Visible=" true" />         
                            </div>
                            <div  class="<%=TabberHide%> <%=Tabber3%>" style="BACKGROUND-COLOR: white;width:775px"> 
                                <h2>Manutenzioni</h2>
                                <uc3:Tab_Manutenzioni ID="Tab_Manutenzioni" runat="server"   Visible="true"  />       
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
        <asp:TextBox ID="SOLO_LETTURA" runat="server" Style="z-index: -1; left: 0px; position: absolute; top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>        
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