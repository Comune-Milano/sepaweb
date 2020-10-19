<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LottiScambio.aspx.vb" Inherits="LOTTI_GestioneLotto" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script type="text/javascript">



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


    function AggiornaImporti(campo) {
        var Prezzo1,Prezzo2,Risultato1


        if (campo==1) {
        
         //CONSUMO1
            Prezzo1 = document.getElementById('txtConsumo1').value.replace(/\./g, '');
            Prezzo1 = Prezzo1.replace(/\,/g, '.'); //(',', '.');

            Prezzo2 = document.getElementById('txtConsumo1EXP').value.replace(/\./g, '');
            Prezzo2 = Prezzo2.replace(/\,/g, '.'); //(',', '.');

            if (parseFloat(Prezzo2) > parseFloat(Prezzo1)) {     
                alert('Attenzione...Importo a Consumo da trasferire deve essere inferiore all\'importo totale iniziale!');
                document.getElementById('txtConsumo1EXP').value = "";
                return;
            }
               
            Risultato1 = parseFloat(Prezzo1) - parseFloat(Prezzo2);
            Risultato1 = Risultato1 + '';
            Prezzo2 = Prezzo2 + '';
       
        
            document.getElementById('txtConsumo2IMP').value = Prezzo2.replace('.', ',');
            AutoDecimal2(document.getElementById('txtConsumo2IMP'));  
            
            document.getElementById('txtConsumo1EXP_TMP').value = Prezzo2.replace('.', ',');
            AutoDecimal2(document.getElementById('txtConsumo1EXP_TMP'));  

            document.getElementById('txtConsumo1_FINALE').value = Risultato1.replace('.', ',');
            AutoDecimal2(document.getElementById('txtConsumo1_FINALE'));

            document.getElementById('txtConsumo1_TMP').value = Risultato1.replace('.', ',');
            
             //CONSUMO2
            Prezzo1 = document.getElementById('txtConsumo2').value.replace(/\./g, '');
            Prezzo1 = Prezzo1.replace(/\,/g, '.'); //(',', '.');

            Prezzo2 = document.getElementById('txtConsumo2IMP').value.replace(/\./g, '');
            Prezzo2 = Prezzo2.replace(/\,/g, '.'); //(',', '.');
            
            
            Risultato1 = parseFloat(Prezzo1) + parseFloat(Prezzo2);
            Risultato1 = Risultato1 + '';
                            
            document.getElementById('txtConsumo2_FINALE').value = Risultato1.replace('.', ',');
            AutoDecimal2(document.getElementById('txtConsumo2_FINALE'));

            document.getElementById('txtConsumo2_TMP').value = Risultato1.replace('.', ',');
        //*********************************************************************
        }
        else {
        //CANONE 1
            Prezzo1 = document.getElementById('txtCanone1').value.replace(/\./g, '');
            Prezzo1 = Prezzo1.replace(/\,/g, '.'); //(',', '.');

            Prezzo2 = document.getElementById('txtCanone1EXP').value.replace(/\./g, '');
            Prezzo2 = Prezzo2.replace(/\,/g, '.'); //(',', '.');

            if (parseFloat(Prezzo2) > parseFloat(Prezzo1)) {     
                alert('Attenzione...Importo a Canone da trasferire deve essere inferiore all\'importo totale iniziale!');
                document.getElementById('txtCanone1EXP').value = "";
                return;
            }
            
            Risultato1 = parseFloat(Prezzo1) - parseFloat(Prezzo2);
            Risultato1 = Risultato1 + '';
            Prezzo2 = Prezzo2 + '';
        
            document.getElementById('txtCanone2IMP').value = Prezzo2.replace('.', ',');
            AutoDecimal2(document.getElementById('txtCanone2IMP'));  
            
            document.getElementById('txtCanone1EXP_TMP').value = Prezzo2.replace('.', ',');
            AutoDecimal2(document.getElementById('txtCanone1EXP_TMP'));              
            
            document.getElementById('txtCanone1_FINALE').value = Risultato1.replace('.', ',');
            AutoDecimal2(document.getElementById('txtCanone1_FINALE'));

            document.getElementById('txtCanone1_TMP').value = Risultato1.replace('.', ',');
            
            //CANONE 2
            Prezzo1 = document.getElementById('txtCanone2').value.replace(/\./g, '');
            Prezzo1 = Prezzo1.replace(/\,/g, '.'); //(',', '.');

            Prezzo2 = document.getElementById('txtCanone2IMP').value.replace(/\./g, '');
            Prezzo2 = Prezzo2.replace(/\,/g, '.'); //(',', '.');
            
            Risultato1 = parseFloat(Prezzo1) + parseFloat(Prezzo2);
            Risultato1 = Risultato1 + '';
            
            document.getElementById('txtCanone2_FINALE').value = Risultato1.replace('.', ',');
            AutoDecimal2(document.getElementById('txtCanone2_FINALE'));

            document.getElementById('txtCanone2_TMP').value = Risultato1.replace('.', ',');               
        //*********************************************************************
        }
     }




</script>

<html xmlns="http://www.w3.org/1999/xhtml" >


<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<title>MODULO GESTIONE LOTTI</title>

<script language="javascript" type="text/javascript">
    var Uscita;
Uscita=0;
</script>
<script type="text/javascript" src="../../../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../../../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../../../Contratti/moo.fx.pack.js"></script>

<script type="text/javascript" src="tabber.js"></script>
<link rel="stylesheet" href="example.css" type="text/css" media="screen"/>

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
    
<script language="javascript" type="text/javascript">

//window.onbeforeunload = confirmExit; 

function AnnullaVariazioni() {
    var sicuro = confirm('Sei sicuro di voler ripristinare le variazioni effettuate?');
    if (sicuro == true) {
    document.getElementById('txtAnnulla').value='1';
    }
    else
    {
    document.getElementById('txtAnnulla').value='0'; 
    }
}


function ConfermaEsci()
{
 if (document.getElementById('txtModificato').value=='1') 
 {
    var chiediConferma
     //if (document.getElementById('txtStatoPagamento').value<=3){
        chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
        if (chiediConferma == false) {
            document.getElementById('txtModificato').value='111';
            //document.getElementById('USCITA').value='0';
        }
    // }
 } 
} 



function confirmExit(){
 if (document.getElementById("USCITA").value=='0') {
 if (navigator.appName == 'Microsoft Internet Explorer') 
    {
    event.returnValue = "Attenzione...Uscire dalla scheda Lotti premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dalla scheda manutenzione premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
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

<body >
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
        <table>
                <td style="width: 800px; height: 1px;" id="TD_Principale">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                    <table style="width: 760px">
                        <tr>
                            <td style="width: 76px">
                                &nbsp;<asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                    left: 16px; position: static; top: 29px" TabIndex="8" ToolTip="Indietro" /></td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                    
                                    OnClientClick="document.getElementById('USCITA').value='1' ; " Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="5" ToolTip="Salva" 
                                    CausesValidation="False" /></td>
                            <td style="width: 76px;">
                                </td>
                            <td style="width: 76px"><asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Immagini/img_AnnullaVoce.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';  AnnullaVariazioni();" Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="6" ToolTip="Salva" /></td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px"></td>
                            <td style="width: 76px">
                                <img id="ImgEventi" alt="Eventi Scheda Lotto di origine" border="0" onclick="window.open('Report/Eventi.aspx?ID_LOTTO=<%=vId %>','Report', '');"
                                    src="Immagini/Img_EventiOrigine.png" style="left: 616px; cursor: pointer; top: 32px" /></td>
                            <td style="width: 76px"><img id="ImgEventi2" alt="Eventi Scheda Lotto di destinazione" border="0" onclick="window.open('Report/Eventi.aspx?ID_LOTTO=<%=vId2 %>','Report', '');"
                                    src="Immagini/Img_EventiDestinazione.png" style="left: 616px; cursor: pointer; top: 32px" /></td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125;
                                    left: 600px; position: static; top: 29px" TabIndex="7" ToolTip="Esci" /></td>
                        </tr>
                    </table>
                    &nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                        Width="760px">DETTAGLI LOTTO.............................................................................................................................................................................................................................</asp:Label>
            <table style="width: 760px">
                <tr>
                    <td style="height: 21px">
                        <asp:Label ID="lblES" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                            TabIndex="-1" Width="100px">Esercizio Finanziario</asp:Label></td>
                    <td style="height: 21px">
                        <asp:TextBox ID="txtEsercizio" runat="server" Enabled="False" MaxLength="300" Style="z-index: 10;
                            left: 408px; top: 171px" TabIndex="-1" Width="250px" Font-Size="8pt"></asp:TextBox><asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px; text-align: center;" TabIndex="-1" Width="100px">Struttura:    </asp:Label><asp:TextBox ID="txtFiliale" runat="server" Enabled="False" MaxLength="300" Style="z-index: 10;
                            left: 408px; top: 171px" TabIndex="-1" Width="290px" Font-Size="8pt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="height: 21px">
                                <asp:Label ID="lblServizi" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Servizio</asp:Label></td>
                    <td style="height: 21px">
                        <asp:TextBox ID="txtServizi" runat="server" Enabled="False" MaxLength="300" Style="z-index: 10;
                            left: 408px; top: 171px" TabIndex="-1" Width="650px" Font-Size="8pt"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Dettaglio Servizio</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtServiziDettaglio" runat="server" Enabled="False" MaxLength="300" Style="z-index: 10;
                            left: 408px; top: 171px" TabIndex="-1" Width="650px" Font-Size="8pt"></asp:TextBox></td>
                </tr>
            </table>
                    &nbsp;<asp:Label style="z-index: 100; left: 8px; top: 88px;" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0" ID="Label1" runat="server" TabIndex="-1" Width="760px">...............................................................................................................................................................................................................................</asp:Label><br /><table>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="330px">Lotto Origine *</asp:Label></td>
                            <td style="width: 100px">
                            </td>
                            <td>
                                <asp:Label ID="lblDescrizione" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="330px">Lotto Destinazione *</asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="cmbLotto1" runat="server" BackColor="White"
                            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 10; left: 17px; border-left: black 1px solid;
                            border-bottom: black 1px solid; top: 127px" TabIndex="-1" Width="330px">
                                </asp:DropDownList></td>
                            <td style="width: 100px">
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbLotto2" runat="server" AutoPostBack="True" BackColor="White"
                            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 10; left: 17px; border-left: black 1px solid;
                            border-bottom: black 1px solid; top: 127px" Width="330px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblComplessi" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="330px">Lista Complessi *</asp:Label></td>
                            <td style="width: 100px">
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="330px">Lista Complessi *</asp:Label></td>
                        </tr>
                        <tr>
                            <td><div style="border-left-color: #ccccff; border-bottom-color: #ccccff; width: 330px;
                                    border-top-style: solid; border-top-color: #ccccff; border-right-style: solid;
                                    border-left-style: solid; height: 200px; border-right-color: #ccccff; border-bottom-style: solid; overflow: auto;">
                                <asp:CheckBoxList ID="lstcomplessi1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Width="325px" TabIndex="1">
                                </asp:CheckBoxList></div>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="88px">A Consumo (&#8364;)</asp:Label>
                                &nbsp; &nbsp;
                                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="88px">A Canone (&#8364;)</asp:Label></td>
                            <td style="width: 100px">
                                &nbsp;&nbsp;
                                <asp:ImageButton ID="btn_Destra_1_2" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/LOTTI/Immagini/Front.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'" Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="3" ToolTip="Trasferisce i complessi selezionati dal lotto d'origine a quello di destinazione." /><br />
                                &nbsp;&nbsp;
                                </td>
                            <td>
                                <div style="border-left-color: #ccccff; border-bottom-color: #ccccff; width: 330px;
                                    border-top-style: solid; border-top-color: #ccccff; border-right-style: solid;
                                    border-left-style: solid; height: 200px; border-right-color: #ccccff; border-bottom-style: solid;">
                                    <asp:ListBox ID="lstcomplessi2" runat="server" Width="325px" style="overflow: auto" Height="200px"></asp:ListBox></div>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="88px">A Consumo (&#8364;)</asp:Label>
                                &nbsp; &nbsp;&nbsp;
                                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="88px">A Canone (&#8364;)</asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblVal3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Importo Totale Iniziale:</asp:Label>
                                <asp:TextBox ID="txtConsumo1" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtCanone1" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox></td>
                            <td style="width: 100px">
                                </td>
                            <td>
                                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Importo Totale Iniziale:</asp:Label>
                                <asp:TextBox ID="txtConsumo2" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtCanone2" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Importo Da Trasferire:</asp:Label>
                                <asp:TextBox ID="txtConsumo1EXP" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtCanone1EXP" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox></td>
                            <td style="width: 100px">
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Importo Trasferito:</asp:Label>
                                <asp:TextBox ID="txtConsumo2IMP" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtCanone2IMP" runat="server" Font-Bold="True" Font-Size="8pt" MaxLength="30"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Importo Totale Finale:</asp:Label>
                                <asp:TextBox ID="txtConsumo1_FINALE" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtCanone1_FINALE" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox></td>
                            <td style="width: 100px">
                            </td>
                            <td>
                                <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="110px">Importo Totale Finale:</asp:Label>
                                <asp:TextBox ID="txtConsumo2_FINALE" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtCanone2_FINALE" runat="server" Font-Bold="True" Font-Size="8pt"
                                    MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                    TabIndex="-1" Width="100px"></asp:TextBox></td>
                        </tr>
                    </table>
                                <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Red" Height="16px" Style="z-index: 104; left: 7px; top: 537px" Visible="False"
                                    Width="680px"></asp:Label></table>
        <br />
<br />

        <asp:TextBox ID="USCITA"         runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtModificato"  runat="server" BackColor="White" BorderStyle="None" ForeColor="White" Style="left: 0px; position: absolute; top: 200px; z-index: -1;">0</asp:TextBox>
        <asp:TextBox ID="txtAnnulla"     runat="server" BackColor="White" BorderStyle="None" ForeColor="White" Style="z-index: -1; left: 0px; position: absolute; top: 200px">0</asp:TextBox>
        <asp:TextBox ID="txtindietro"    runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute; top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        
        
        <asp:HiddenField id="txtSTATO"         runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtID_Servizio"   runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtID_ServizioDettaglio"   runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtTipoScambio"   runat="server"></asp:HiddenField>
        
        <asp:HiddenField id="txtEvento1"       runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtEvento2"       runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtID_Complessi1" runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtID_Complessi2" runat="server"></asp:HiddenField>
        
        <asp:HiddenField id="txtCanone1_TMP"   runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtConsumo1_TMP"   runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtCanone2_TMP"   runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtConsumo2_TMP"   runat="server"></asp:HiddenField>
        
        <asp:HiddenField id="txtCanone1EXP_TMP"   runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtConsumo1EXP_TMP"   runat="server"></asp:HiddenField>

        
        
        
    </div>
        
    </form>

<script type="text/javascript">
window.focus();
self.focus();

          
    if (document.getElementById('txtSTATO').value == '0') {
        //SOLO LETTURA
        document.getElementById('btnSalva').style.visibility = 'hidden';
        document.getElementById('btnAnnulla').style.visibility = 'hidden';
    }          

    if (document.getElementById('txtSTATO').value == '1') {
        //DOPO SALVA o ANNULLA
        document.getElementById('btnSalva').style.visibility = 'hidden';
        document.getElementById('btnAnnulla').style.visibility = 'hidden';
    }          

    if (document.getElementById('txtSTATO').value == '2') {
        //DOPO AVER FATTO DELLE VARIAZIONI
        document.getElementById('btnSalva').style.visibility = 'visible';
        document.getElementById('btnAnnulla').style.visibility = 'visible';
    }          


</script>

</body>

</html>
