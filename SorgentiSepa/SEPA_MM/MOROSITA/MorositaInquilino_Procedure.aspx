<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MorositaInquilino_Procedure.aspx.vb" Inherits="MorositaInquilino_Procedure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script type="text/javascript">
    var Uscita;
    Uscita=0;

    window.name = "modal";

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) 
        {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    
    function $onkeydown() {
 
        if (event.keyCode == 13) 
        {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';   
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }
    
    
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
//        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';
    }
	
	
    function DelPointer(obj) {
	    obj.value = obj.value.replace('.', '');
	    document.getElementById(obj.id).value = obj.value;
	}

    function $onkeydown() {
        if (event.keyCode == 46) {
            event.keyCode = 0;
        }
    }
	
	function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) 
        {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) 
            {
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
            else 
            {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        }

var Selezionato;

       function ApriDettBollettaMG() {

           if (document.getElementById('txtIdMG').value != '')  {
              window.showModalDialog('../Contratti/Pagamenti/DettManuale.aspx?IDBOL=' + document.getElementById('txtIdMG').value + '&NUMBOL=' + document.getElementById('numBollettaMG').value + '&IDCON_MOR=' + document.getElementById('txtIdConnessione').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
              //alert('Funzione momentaneamente disattivata!In fase di modifica!')
              document.getElementById('txtIdMG').value = '';
           }
           else {
               alert('Selezionare una BOLLETTA, per procedere con il pagamento MANUALE!');
           }
       }


       function ApriDettBollettaMA() {

           if (document.getElementById('txtIdMA').value != '')  {
               window.showModalDialog('../Contratti/Pagamenti/DettManuale.aspx?IDBOL=' + document.getElementById('txtIdMA').value + '&NUMBOL=' + document.getElementById('numBollettaMA').value + '&IDCON_MOR=' + document.getElementById('txtIdConnessione').value, 'window', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
               //alert('Funzione momentaneamente disattivata!In fase di modifica!')
               document.getElementById('txtIdMA').value = '';

           }
           else {
               alert('Selezionare una BOLLETTA, per procedere con il pagamento MANUALE!');
           }
       }

    function AnnullaPagamento() {
        if (document.getElementById('txtModificato').value=='1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono stati effettuati dei pagamenti. Uscire ugualmente senza salvare?");
            if (chiediConferma == true) {
                document.getElementById('txtModificato').value='111';
                //document.getElementById('USCITA').value='0';
            }
        } 
    } 


</script>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<base target="_self"/>
<title>MODULO GESTIONE MOROSITA ESECUZIONE PROCEDURE</title>

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




function confirmExit(){
 if (document.getElementById("USCITA").value=='0') {
 if (navigator.appName == 'Microsoft Internet Explorer') 
    {
    event.returnValue = "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    }
}


function Procedi() {
    var r1
    var r1_Val
    var i
    var bTrovato

    bTrovato = '0';
    
    r1 = document.forms[0].elements['RBList1'];

//        for (i = 0; i < r1.children.length; i++) {

    for (i = 0; i < r1.length; i++) {

        bTrovato = '1';
        r1_Val = r1[i].value

        switch (r1_Val) {

           case "14":
                /*DILAZIONE*/
                if (r1[i].checked == true) {
                   document.getElementById('txtAppareDilazione').value = '1';
                   document.getElementById('DIV_Dilazione').style.visibility = 'visible';
                }
                break;

           case "18":
                /*SOSPENSIONE*/
                if (r1[i].checked == true) {
                   document.getElementById('txtAppareSospensione').value = '1';
                   document.getElementById('DIV_Sospenzione').style.visibility = 'visible';
                   document.getElementById('T_DataGridSospensione').style.visibility = 'visible';
                }
                break;

            case "19":
                if (r1[i].checked == true) {
                    var sicuro = confirm('Attenzione...Confermi di voler annullare la sospensione?');
                    if (sicuro == true) {
                        document.getElementById('txtAnnullo').value='1';
                    }
                    else
                    {
                        document.getElementById('txtAnnullo').value='0'; 
                    }                
                }
                break; 

           case "1":
               alert('NON IMPLEMENTATO!')
           break; 

        case "21":
                /*PAGAMENTO*/
                if (r1[i].checked == true) {
                   document.getElementById('txtApparePagamento').value = '1';
                   document.getElementById('DIV_Pagamento').style.visibility = 'visible';
                   
                   document.getElementById('DivPag_Grid1').style.visibility = 'visible';
                   document.getElementById('DivPag_Grid2').style.visibility = 'visible';
                }
                break;

        case "101":
                /*ESITO MANUALE*/
                if (r1[i].checked == true) {
                    document.getElementById('txtAppareEsitoManuale').value = '1';
                    document.getElementById('DIV_EsitoManuale').style.visibility = 'visible';
                }
                break;


        case "102":
            if (r1[i].checked == true) {
                var sicuro = confirm('Attenzione...Confermi di voler annullare e rigenerare il MAV?');
                if (sicuro == true) {
                    document.getElementById('txtRigeneraMAV').value = '1';
                }
                else {
                    document.getElementById('txtRigeneraMAV').value = '0';
                }
            }
            break;


        case "103":
            if (r1[i].checked == true) {
                var sicuro = confirm('Attenzione...Confermi di voler annullare e rigenerare la lettera, MAV e postAler?');
                if (sicuro == true) {
                    document.getElementById('txtRigeneraLettera').value = '1';
                }
                else {
                    document.getElementById('txtRigeneraLettera').value = '0';
                }
            }
            break; 

           default:
                /*alert('NON IMPLEMENTATO!')*/
       }         
    }
        
    if (bTrovato == '0') {
        r1_Val = document.forms[0].elements['RBList1'].value
        switch (r1_Val) {
            case "14":
                /*DILAZIONE*/
                document.getElementById('txtAppareDilazione').value = '1';
                document.getElementById('DIV_Dilazione').style.visibility = 'visible';
                break;

            case "18":
                /*SOSPENSIONE*/
                document.getElementById('txtAppareSospensione').value = '1';
                document.getElementById('DIV_Sospenzione').style.visibility = 'visible';
                document.getElementById('T_DataGridSospensione').style.visibility = 'visible';
                break;

            case "19":
                var sicuro = confirm('Attenzione...Confermi di voler annullare la sospensione?');
                if (sicuro == true) {
                    document.getElementById('txtAnnullo').value = '1';
                }
                else {
                    document.getElementById('txtAnnullo').value = '0';
                }
                break;

            case "1":
                alert('NON IMPLEMENTATO!')
                break;

            case "21":
                /*PAGAMENTO*/
                document.getElementById('txtApparePagamento').value = '1';
                document.getElementById('DIV_Pagamento').style.visibility = 'visible';

                document.getElementById('DivPag_Grid1').style.visibility = 'visible';
                document.getElementById('DivPag_Grid2').style.visibility = 'visible';
                break;

            case "101":
                /*ESITO MANUALE*/
                document.getElementById('txtAppareEsitoManuale').value = '1';
                document.getElementById('DIV_EsitoManuale').style.visibility = 'visible';
                break;

            case "102":
                var sicuro = confirm('Attenzione...Confermi di voler annullare e rigenerare il MAV?');
                if (sicuro == true) {
                    document.getElementById('txtRigeneraMAV').value = '1';
                }
                else {
                    document.getElementById('txtRigeneraMAV').value = '0';
                }
                break;

            case "103":
                var sicuro = confirm('Attenzione...Confermi di voler annullare e rigenerare la lettera, MAV e postAler?');
                if (sicuro == true) {
                    document.getElementById('txtRigeneraLettera').value = '1';
                }
                else {
                    document.getElementById('txtRigeneraLettera').value = '0';
                }
                break;

            case "94":
                var sicuro = confirm('Attenzione...Confermi di voler creare il MAV?');
                if (sicuro == true) {
                    document.getElementById('txtCreaMAV').value = '1';
                }
                else {
                    document.getElementById('txtCreaMAV').value = '0';
                }
                break;

            case "104":
                var sicuro = confirm('Attenzione...Confermi di voler annullare la lettera e il MAV?');
                if (sicuro == true) {
                    document.getElementById('txtAnnullaMAV').value = '1';
                }
                else {
                    document.getElementById('txtAnnullaMAV').value = '0';
                }
                break;

            default:
                /*alert('NON IMPLEMENTATO!')*/
        }
    } 
}
   
</script>


</head>

<body bgcolor="#f2f5f1" text="#ede0c0">
    <form id="form1" runat="server" target ="modal">
    <div id="DIV1" style="display: block; left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px; background-color: whitesmoke; height: 544px;">
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="14pt"
            ForeColor="#801F1C" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
            Width="760px">Morosità: Procedura Preliminare</asp:Label>
        &nbsp; <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
        </strong>
        <br />
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                            Width="760px">SITUAZIONE ATTUALE........................................................................................................................................................</asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDescrizioneM" runat="server" Font-Names="Arial" Font-Size="9pt"
                            Height="80px" MaxLength="2000" Style="left: 80px; top: 88px" TabIndex="-1" TextMode="MultiLine"
                            Width="760px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                            ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                            Width="760px">Scegliere una delle opzioni seguenti.................................................................................................................................</asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                            overflow: auto; border-left: #0000cc thin solid; width: 750px; border-bottom: #0000cc thin solid;
                            height: 200px">
                            <asp:RadioButtonList ID="RBList1" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Height="80%" Style="border-right: lightsteelblue 1px solid;
                                border-top: lightsteelblue 1px solid; left: 248px; vertical-align: middle; border-left: lightsteelblue 1px solid;
                                border-bottom: lightsteelblue 1px solid; top: 232px; text-align: left"
                                Width="95%">
                            </asp:RadioButtonList>
                            <asp:Label ID="lbl_Opzioni" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                ForeColor="Red" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Attenzione: </asp:Label><asp:Label
                                    ID="lbl_Opzioni_Contenuto" runat="server" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="10pt" ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="600px">In attesa della ricevuta PostAler</asp:Label></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png" Style="left: 574px; top: 516px; height: 20px" TabIndex="1" OnClientClick="Procedi();" ToolTip="Procede con l'esecuzione dell'opzione scelta" />
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:ImageButton ID="ImgEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png" Style="left: 574px; top: 516px; height: 20px" TabIndex="2" OnClientClick="document.getElementById('USCITA').value='1';" /></td>
                </tr>
            </table>
        </div>
        
    <div id="DIV_Dilazione" 
        style="left: 0px;
            width: 800px; position: absolute; top: 0px; height: 544px; background-color: whitesmoke;">
            &nbsp;
            <table id="TABLE1" style="border-right: blue 2px; border-top: blue 2px; z-index: 102;
                left: 24px; border-left: blue 2px; border-bottom: blue 2px; position: absolute;
                top: 32px; background-color: #ffffff">
                <tr>
                    <td>
                        <strong><span style="color: #0000ff; font-family: Arial">
                            <asp:Label ID="lblTitolo1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="300px">Gestione dilazione</asp:Label></span></strong></td>
                </tr>
                <tr>
                    <td>
                        <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_Lettera1_1" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Blue" 
                                                    Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                                    Width="180px">Dettaglio M.AV. Global Service:</asp:Label>
                                                <asp:CheckBox ID="chk1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                    ForeColor="Black" TabIndex="7" Text="(Abilita)" Width="96px" 
                                                    AutoPostBack="True" /><br />
                                                <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                                    border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Importo1" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Imp. TOTALE:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtImporto1" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                                                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                                TabIndex="-1" Width="80px"></asp:TextBox></td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lbl_euro1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="&#8364;"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_Tipo1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">N./Tipo:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTipo1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="50" ReadOnly="True" TabIndex="-1" Width="80px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Dal1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Periodo Dal:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataDal1" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_Al_1" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                                                TabIndex="-1" Width="80px">Periodo Al:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataAL_1" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="8pt" MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px"
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa" Width="70px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lbl_Lettera1_2" runat="server" Font-Bold="True" 
                                                    Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                    Width="224px">Scadenze M.AV. Global Service:</asp:Label>
                                                <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                                    border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Emissione_1" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Emissione</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmissione_1" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="?" Visible="False"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_Giorni_Dilazione_1" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">Giorni Dilazione</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtGiorni_1" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="3" ToolTip="gg/mm/aaaa"
                                                                Width="70px" AutoPostBack="True"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Scadenza_1" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Scadenza</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtScadenza_1" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_Scadenza_Dilazione_1" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">Scadenza dilazione</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtScadenzaDilazione_1" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="4" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            -------------</td>
                                                        <td>
                                                            ------------</td>
                                                        <td style="text-align: left">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Giorni_Scadenza_1" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Scadenza tra</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtGironiScadenza_1" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="50" ReadOnly="True" TabIndex="-1" Width="60px" 
                                                                ForeColor="Black"></asp:TextBox>
                                                            <asp:Label ID="lbl_gg1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="gg"></asp:Label></td>
                                                        <td style="text-align: left">
                                                        </td>
                                                        <td>
                                                            </td>
                                                        <td>
                                                            </td>
                                                    </tr>
                                                </table>
                                                <asp:TextBox ID="txt_ID1" runat="server" Height="16px" Style="left: 640px; top: 200px"
                                                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
                                            <td>
                                                <asp:Label ID="lbl_Lettera2_1" runat="server" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="Blue" 
                                                    Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                                    Width="180px">Dettaglio M.AV. Gestore:</asp:Label><asp:CheckBox ID="chk2" runat="server"
                                                        Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" 
                                                        TabIndex="8" Text="(Abilita)" Width="96px" AutoPostBack="True" />
                                                <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                                    border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Importo2" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Imp. TOTALE:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtImporto2" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                                                ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                                                TabIndex="-1" Width="80px"></asp:TextBox></td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lbl_euro2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="&#8364;"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_Tipo2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">N./Tipo:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTipo2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="50" ReadOnly="True" TabIndex="-1" Width="80px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Dal2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Periodo Dal:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataDal2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                        <td style="text-align: left">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_Al_2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">Periodo Al:</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtDataAL_2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lbl_Lettera2_2" runat="server" Font-Bold="True" 
                                                    Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                    Width="224px">Scadenze M.AV. Gestore:</asp:Label>
                                                <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                                    border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Emissione_2" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Emissione</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmissione_2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="?" Visible="False"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbl_Giorni_Dilazione_2" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">Giorni Dilazione</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtGiorni_2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" Style="left: 144px; top: 192px" 
                                                                TabIndex="5" ToolTip="gg/mm/aaaa"
                                                                Width="70px" AutoPostBack="True"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Scadenza_2" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Scadenza</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtScadenza_2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="-1" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                        <td style="text-align: left">
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbl_Scadenza_Dilazione_2" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="80px">Scadenza dilazione</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtScadenzaDilazione_2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                                                TabIndex="6" ToolTip="gg/mm/aaaa"
                                                                Width="70px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            -------------</td>
                                                        <td>
                                                            ------------</td>
                                                        <td style="text-align: left">
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_Giorni_Scadenza_2" runat="server" Font-Bold="False" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" 
                                                                Width="65px">Scadenza tra</asp:Label></td>
                                                        <td>
                                                            <asp:TextBox ID="txtGironiScadenza_2" runat="server" Font-Bold="True" 
                                                                Font-Names="Arial" Font-Size="8pt"
                                                                MaxLength="50" ReadOnly="True" TabIndex="-1" Width="60px"></asp:TextBox>
                                                            <asp:Label ID="lbl_gg2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="gg"></asp:Label></td>
                                                        <td style="text-align: left">
                                                        </td>
                                                        <td>
                                                            </td>
                                                        <td>
                                                            </td>
                                                    </tr>
                                                </table>
                                                <asp:TextBox ID="txt_ID2" runat="server" Height="16px" Style="left: 640px; top: 200px"
                                                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 22px">
                                            </td>
                                            <td style="height: 22px">
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="1" cellspacing="1" style="width: 90%">
                                        <tr>
                                            <td align="right" style="vertical-align: top; text-align: center; height: 26px;">
                                                &nbsp;<asp:ImageButton 
                                                    ID="btn_InserisciDilazione" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';"
                                                    Style="cursor: pointer" ToolTip="Salva le modifiche apportate" 
                                                    TabIndex="9" />
                                                &nbsp; &nbsp;<asp:ImageButton ID="btn_ChiudiDilazione" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Dilazione').style.visibility='hidden';document.getElementById('txtAppareDilazione').value='0';"
                                                    Style="cursor: pointer" TabIndex="10" 
                                                    ToolTip="Esci senza inserire o modificare" /></td>
                                        </tr>
                                    </table>
                                </td>
                </tr>
            </table>
            <asp:Image ID="Image1" runat="server" BackColor="White" Height="440px" ImageUrl="../ImmDiv/DivMGrande.png"
                Style="z-index: 101; left: 0px; position: absolute; top: 0px;" Width="800px" />
        <asp:HiddenField ID="txtTotale2" runat="server" />
                                    <asp:HiddenField ID="txtImportoODL" runat="server" />
        </div>
       
    <div id="DIV_Sospenzione" 
        
        
        style="left: 0px;
            width: 800px; position: absolute; top: 0px; height: 544px; background-color: whitesmoke;">
        &nbsp;
        <table id="Table2" style="border-right: blue 2px; border-top: blue 2px; z-index: 102;
                left: 30px; border-left: blue 2px; border-bottom: blue 2px; position: absolute;
                top: 30px; background-color: #ffffff">
            <tr>
                <td>
                    <strong><span style="color: #0000ff; font-family: Arial">
                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                            ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="424px">Gestione sospensione</asp:Label></span></strong></td>
            </tr>
            <tr>
                <td>
                                <table>
                                    <tr>
                                        <td style="height: 20px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
                                                Font-Size="10pt" ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px"
                                                TabIndex="-1" Width="376px">Motivo della sospensione</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="T_DataGridSospensione" style="border-right: #0000cc thin solid; border-top: #0000cc thin solid;
                                                visibility: visible; overflow: auto; border-left: #0000cc thin solid; width: 650px;
                                                border-bottom: #0000cc thin solid; height: 152px">
                                                <asp:RadioButtonList ID="RButtonSospensione" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Height="120px" Style="border-right: lightsteelblue 1px solid;
                                border-top: lightsteelblue 1px solid; left: 248px; vertical-align: middle; border-left: lightsteelblue 1px solid;
                                border-bottom: lightsteelblue 1px solid; top: 232px; text-align: left"
                                Width="600px" TabIndex="11">
                                                    <asp:ListItem Value="15">Presenza d&#8217;istanza revisione canone.</asp:ListItem>
                                                    <asp:ListItem Value="16">Richiesta in corso di Contributo di Solidariet&#224; (Fondo Sociale).</asp:ListItem>
                                                    <asp:ListItem Value="17">Richiesta verifica congruit&#224; debito.</asp:ListItem>
                                                    <asp:ListItem Value="18">Altro.</asp:ListItem>
                                                </asp:RadioButtonList></div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="120px" Font-Italic="True">Note:</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNoteSospensione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="80px" MaxLength="500" Style="left: 80px; top: 88px" TabIndex="12" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox></td>
                                    </tr>
                                </table><asp:CheckBox ID="chkSospensione" runat="server" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="9pt"
                                                    ForeColor="Black" TabIndex="13" 
                                    Text="Sospensione del M.AV. Global Service" Width="640px" /></td>
            </tr>
            <tr>
                <td style="height: 14px">
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr>
                            <td align="right" style="vertical-align: top; text-align: center; height: 26px;">
                                &nbsp;&nbsp;<asp:ImageButton 
                                    ID="btn_InserisciSospensione" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';"
                                                    Style="cursor: pointer" 
                                    ToolTip="Salva le modifiche apportate" TabIndex="14" />
                                &nbsp; &nbsp;<asp:ImageButton ID="btn_ChiudiSospensione" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Sospenzione').style.visibility='hidden';document.getElementById('txtAppareSospensione').value='0';"
                                                    Style="cursor: pointer" TabIndex="15" 
                                                    ToolTip="Esci senza inserire o modificare" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Image ID="Image2" runat="server" BackColor="White" Height="550px" ImageUrl="../ImmDiv/DivMGrande.png"
                Style="z-index: 101; left: 0px; position: absolute; top: 0px;" Width="800px" />
    </div>
       
    <div id="DIV_Pagamento" 
        

        style="left: 0px;
            width: 800px; position: absolute; top: 0px; height: 544px; background-color: whitesmoke;">
    &nbsp;
    <table id="Table3" style="border-right: blue 2px; border-top: blue 2px; z-index: 102;
                left: 24px; border-left: blue 2px; border-bottom: blue 2px; position: absolute;
                top: 24px; background-color: #ffffff">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">
                <table>
                    <tr>
                        <td>
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                        ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="300px">Gestione Pagamento</asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="180px">Dettaglio M.AV. Global Service:</asp:Label>
                            <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                                    border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="65px">Imp. TOTALE:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtImportoP1" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                            ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                            TabIndex="-1" Width="80px"></asp:TextBox></td>
                                    <td style="text-align: left">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="&#8364;"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">N./Tipo:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtTipo_MG" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="50" ReadOnly="True" TabIndex="-1" Width="80px"></asp:TextBox></td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="65px">Periodo Dal:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDataDal_MG" runat="server" Font-Bold="True" 
                                            Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                            TabIndex="-1" ToolTip="gg/mm/aaaa"
                                            Width="70px"></asp:TextBox></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Periodo Al:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDataAL_MG" runat="server" Font-Bold="True" 
                                            Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                            TabIndex="-1" ToolTip="gg/mm/aaaa"
                                            Width="70px"></asp:TextBox></td>
                                </tr>
                            </table>
                            <table id="Table4">
                                <tr>
                                    <td>
                                        &nbsp;<asp:Label ID="lblELENCO_BOLLETTE" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO BOLLETTE" Width="248px"></asp:Label>&nbsp;</td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                                            overflow: auto; border-left: #0000cc thin solid; width: 650px; border-bottom: #0000cc thin solid;
                                            height: 125px" id="DivPag_Grid1">
                                            <asp:DataGrid ID="DataGridBolletteMG" runat="server" 
                                                AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="Blue" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                PageSize="24" 
                                                Style="z-index: 105; left: 193px; top: 54px; border-collapse:separate" 
                                                Width="100%" TabIndex="16">
                                                <PagerStyle Mode="NumericPages" />
                                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <ItemStyle ForeColor="Black" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM."></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="N_RATA" HeaderText="N.RATA" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RIFERIMENTO" HeaderText="RIFERIMENTO">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO &#8364;.">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="PAGATO &#8364;.">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO &#8364;.">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    ForeColor="#0000C0" />
                                            </asp:DataGrid></div>
                                        <asp:TextBox ID="txtSelMG" runat="server" BackColor="#F2F5F1" BorderColor="White"
                                            BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                                            ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="650px"></asp:TextBox></td>
                                    <td style="width: 5px">
                                        </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="height: 16px">
                                                    <asp:ImageButton ID="btnApri1" runat="server" CausesValidation="False" 
                                                        ImageUrl="~/MOROSITA/Immagini/Img_Pagamento.png" TabIndex="17" 
                                                        ToolTip="Procedi con il pagamento ad Imputazione Manuale" 
                                                        OnClientClick="ApriDettBollettaMG();" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 14px">
                                                    </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="180px">Dettaglio M.AV. Gestore:</asp:Label><br />
                            <table style="border-right: gainsboro 1px solid; border-top: gainsboro 1px solid;
                                                    border-left: gainsboro 1px solid; border-bottom: gainsboro 1px solid">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="65px">Imp. TOTALE:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtImportoP2" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="10"
                                            ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                            TabIndex="-1" Width="80px"></asp:TextBox></td>
                                    <td style="text-align: left">
                                        <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="&#8364;"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">N./Tipo:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtTipo_MA" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="50" ReadOnly="True" TabIndex="-1" Width="80px"></asp:TextBox></td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="65px">Periodo Dal:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDataDal_MA" runat="server" Font-Bold="True" 
                                            Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                            TabIndex="-1" ToolTip="gg/mm/aaaa"
                                            Width="70px"></asp:TextBox></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Periodo Al:</asp:Label></td>
                                    <td>
                                        <asp:TextBox ID="txtDataAL_MA" runat="server" Font-Bold="True" 
                                            Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="10" ReadOnly="True" Style="left: 144px; top: 192px" 
                                            TabIndex="-1" ToolTip="gg/mm/aaaa"
                                            Width="70px"></asp:TextBox></td>
                                </tr>
                            </table>
                            <table id="Table5">
                                <tr>
                                    <td>
                                        &nbsp;<asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO BOLLETTE" Width="248px"></asp:Label>&nbsp;</td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                                            overflow: auto; border-left: #0000cc thin solid; width: 650px; border-bottom: #0000cc thin solid;
                                            height: 130px" id="DivPag_Grid2"><asp:DataGrid ID="DataGridBolletteMA" 
                                                runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="Blue" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                PageSize="24" 
                                                Style="z-index: 105; left: 193px; top: 54px; border-collapse:separate" 
                                                Width="100%" TabIndex="18">
                                                <PagerStyle Mode="NumericPages" />
                                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <ItemStyle ForeColor="Black" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM."></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="N_RATA" HeaderText="N.RATA" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RIFERIMENTO" HeaderText="RIFERIMENTO">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO &#8364;.">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="PAGATO &#8364;.">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO &#8364;.">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                    ForeColor="#0000C0" />
                                            </asp:DataGrid></div>
                                        <asp:TextBox ID="txtSelMA" runat="server" BackColor="#F2F5F1" BorderColor="White"
                                            BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                                            ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="650px"></asp:TextBox></td>
                                    <td style="width: 5px">
                                        </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="height: 16px">
                                                    <asp:ImageButton ID="btnApri2" runat="server" CausesValidation="False" 
                                                        ImageUrl="~/MOROSITA/Immagini/Img_Pagamento.png" TabIndex="19" 
                                                        ToolTip="Procedi con il pagamento ad Imputazione Manuale" 
                                                        OnClientClick="ApriDettBollettaMA();" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 14px">
                                                    </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp;<asp:ImageButton ID="btn_InserisciPagamento" runat="server" ImageUrl="~/MOROSITA/Immagini/Salva_EmettiMAV.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1'; document.getElementById('splash').style.visibility = 'visible'"
                                                    Style="cursor: pointer" 
                                            ToolTip="Salva le modifiche apportate ed emette il nuovo MAV" TabIndex="20" />
                            &nbsp; &nbsp;&nbsp;
                                        <asp:ImageButton ID="btn_ChiudiPagamento" runat="server" ImageUrl="~/MOROSITA/Immagini/Esci_AnnullaPagamento.png"
                                                    Style="cursor: pointer" TabIndex="21" 
                                                    ToolTip="Esci annullando eventuali pagamenti effettuati" 
                                            OnClientClick="AnnullaPagamento();" />&nbsp;&nbsp;
                                        &nbsp;&nbsp;</td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                            <asp:ImageButton ID="btn_EsciPagamento" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Pagamento').style.visibility='hidden';document.getElementById('txtApparePagamento').value='0';"
                                                    Style="cursor: pointer" TabIndex="22" 
                                                    ToolTip="Esci e ritorna alla maschera precedente" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </span></strong></td>
        </tr>
    </table>
    <asp:Image ID="Image3" runat="server" BackColor="White" Height="555px" ImageUrl="../ImmDiv/DivMGrande.png"
                Style="z-index: 101; left: 0px; position: absolute; top: 0px;" 
            Width="800px" />

</div>       
     

    <div id="DIV_EsitoManuale" 
        
        
        style="left: 0px;
            width: 800px; position: absolute; top: 0px; height: 544px; background-color: whitesmoke;">
        &nbsp;
        <table id="Table6" style="border-right: blue 2px; border-top: blue 2px; z-index: 102;
                left: 30px; border-left: blue 2px; border-bottom: blue 2px; position: absolute;
                top: 30px; background-color: #ffffff">
            <tr>
                <td>
                    <strong><span style="color: #0000ff; font-family: Arial">
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                            ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" 
                        Width="450px">Gestione Aggiornamento Manaule dell'Esito PostAler</asp:Label></span></strong></td>
            </tr>
            <tr>
                <td>
                                <table>
                                    <tr>
                                        <td style="height: 20px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
                                                Font-Size="10pt" ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px"
                                                TabIndex="-1" Width="376px">Scelta Esito</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:DropDownList ID="cmbElencoEsiti" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    top: 56px" TabIndex="23" Width="650px" Font-Bold="True">
                                </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="50px">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                                                ForeColor="Blue" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="120px" Font-Italic="True">Note:</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtNoteEsito" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                Height="120px" MaxLength="400" Style="left: 80px; top: 88px" TabIndex="24" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox></td>
                                    </tr>
                                </table></td>
            </tr>
            <tr>
                <td style="height: 14px">
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr>
                            <td align="right" style="vertical-align: top; text-align: center; height: 26px;">
                                &nbsp;&nbsp;<asp:ImageButton ID="btn_InserisciEsitoManuale" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';"
                                                    Style="cursor: pointer" 
                                    ToolTip="Salva le modifiche apportate" TabIndex="25" />
                                &nbsp; &nbsp;<asp:ImageButton ID="btn_ChiudiEsitoManuale" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_EsitoManuale').style.visibility='hidden';document.getElementById('txtAppareEsitoManuale').value='0';"
                                                    Style="cursor: pointer" TabIndex="26" 
                                                    ToolTip="Esci senza modificare" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Image ID="Image4" runat="server" BackColor="White" Height="550px" ImageUrl="../ImmDiv/DivMGrande.png"
                Style="z-index: 101; left: 1px; position: absolute; top: -6px;" 
            Width="800px" />
    </div>

    <div id="splash" style="border-right: #000066 thin dashed; border-top: #000066 thin dashed;
            font-size: 10px; z-index: 500; left: 15px; visibility: hidden; vertical-align: top;
            border-left: #000066 thin dashed; width: 760px; line-height: normal; border-bottom: #000066 thin dashed;
            position: absolute; top: 15px; height: 536px; background-color: #ffffff; text-align: center">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <img alt="Elaborazione in corso" src="Immagini/load.gif" /><br />
            <br />
           <strong style="color: black">
            Elaborazione in corso... </strong><br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;
        </div>
       
    <asp:HiddenField ID="USCITA"                runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato"         runat="server" Value="0" />
    <asp:HiddenField ID="txtAnnullo"            runat="server" Value="0" />

    <asp:HiddenField ID="txtFlag"               runat="server" Value="0" />

    <asp:HiddenField ID="txtAppareDilazione"    runat="server" Value="0" />
    <asp:HiddenField ID="txtAppareSospensione"  runat="server" Value="0" />
    <asp:HiddenField ID="txtApparePagamento"    runat="server" Value="0" />
     <asp:HiddenField ID="txtAppareEsitoManuale"    runat="server" Value="0" />

    <asp:HiddenField ID="txtIdMG"               runat="server" />
    <asp:HiddenField ID="txtIdMA"               runat="server" />

    <asp:HiddenField ID="totPagabile"           runat="server" Value="0"/>
    <asp:HiddenField ID="totPagato"             runat="server" Value="0"/>
    <asp:HiddenField ID="totResiduo"            runat="server" Value="0"/>

    <asp:HiddenField ID="numBollettaMG" runat="server" Value="0"/>
    <asp:HiddenField ID="numBollettaMA" runat="server" Value="0"/>

    <asp:HiddenField ID="txtID_SelectMG"               runat="server" Value="" />
    <asp:HiddenField ID="txtID_SelectMA"               runat="server" Value="" />

    <asp:HiddenField ID="txtRigeneraMAV"            runat="server" Value="0" />
    <asp:HiddenField ID="txtRigeneraLettera"        runat="server" Value="0" />
    <asp:HiddenField ID="txtEsimoMAnuale"           runat="server" Value="0" />

    <asp:HiddenField ID="txtCreaMAV"                runat="server" Value="0" />
    <asp:HiddenField ID="txtAnnullaMAV"             runat="server" Value="0" />     
     

    <asp:HiddenField id="txtIdConnessione"    runat="server"></asp:HiddenField>

    

 <script type="text/javascript">

document.getElementById('splash').style.visibility = 'hidden';
if (document.getElementById('txtAppareDilazione').value == 0 ) {
    document.getElementById('DIV_Dilazione').style.visibility = 'hidden';
}

if (document.getElementById('txtAppareSospensione').value == 0) {
    document.getElementById('DIV_Sospenzione').style.visibility = 'hidden';
    document.getElementById('T_DataGridSospensione').style.visibility = 'hidden';
}

if (document.getElementById('txtApparePagamento').value == 0) {
    document.getElementById('DIV_Pagamento').style.visibility = 'hidden';
   
    document.getElementById('DivPag_Grid1').style.visibility = 'hidden';
    document.getElementById('DivPag_Grid2').style.visibility = 'hidden';
    document.getElementById('splash').style.visibility = 'hidden';
}

if (document.getElementById('txtAppareEsitoManuale').value == 0) {
    document.getElementById('DIV_EsitoManuale').style.visibility = 'hidden';
}

</script>

 <script type="text/javascript">
window.focus();
self.focus();
</script>

      

 </form>
 
   
 </body>
        
</html>
