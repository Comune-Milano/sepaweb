function ApriStatoDomanda(indice)
{
var win=null;
win=window.open('StatoDomanda.aspx?ID='+indice,'Stato','height=370,top=0,left=0,width=480,scrollbars=no');
}

function ApriHelp()
{
var win=null;
win=window.open('Help_pw.htm','Accesso','height=380,top=0,left=0,width=490,scrollbars=no');
}

function ApriContatti()
{
var win=null;
window.open('../Contatti.htm',null,'height=480,top=0,left=0,width=490,scrollbars=yes');
}

function ApriModulistica()
{
var win=null;

window.open('../Public/Modulistica.htm',null,'height=430,top=0,left=0,width=490,scrollbars=yes');
}

function PosizioneUtente()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
//window.open('Public/VerificaPos.aspx',null,'height=430,top='+TopPosition+',left='+LeftPosition+',width=400');
window.open('Public/VerificaPos.aspx','Posizione','');
}

function Grad_Por()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('Public/Grad_Por.aspx',null,'height=250,top='+TopPosition+',left='+LeftPosition+',width=650');
}

function ApriAccessoBando()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginBandoErp.aspx','a','height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function ApriAccessoBandoCambi()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginBandoCambi.aspx','a','height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function ApriAccessoAnagrafe()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginAnagrafe.aspx','a','height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function ApriAccessoConsultazione()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginConsultazione.aspx','a','height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function ApriAccessoPED()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginPED.aspx','a','height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function ApriAccessoAbbina()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginAbbinamento.aspx',null,'height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function ApriAccessoAbbinaDec()
{
var win=null;
LeftPosition=(screen.width) ? (screen.width-790)/2 :0 ;
TopPosition=(screen.height) ? (screen.height-550)/2 :0;
LeftPosition=LeftPosition-20;
TopPosition=TopPosition-20;
window.open('LoginAbbinamentoDec.aspx',null,'height=598,top='+TopPosition+',left='+LeftPosition+',width=790');
}

function AzzeraCF(C1,C2)
{
 //C1.value='';
 C2.value='0';
 if (document.getElementById('Dic_Dichiarazione1_txtCF').value!='')
 {
 document.getElementById('Dic_Dichiarazione1_messaggio').value='>ATTENZIONE:Dati Anagrafici modificati! Verificare e modificare le eventuali occorrenze nei vari pannelli.<';
 document.getElementById('txtbinserito').value='0';
 document.getElementById('Dic_Dichiarazione1_txtCF').value='';
 }
}

function mask(str,textbox,loc,delim)
	{
	var locs = loc.split(',');
	
	for (var i = 0; i <= locs.length; i++){
		for (var k = 0; k <= str.length; k++){
		if (k == locs[i]){
		if (str.substring(k, k+1) != delim){
			str = str.substring(0,k) + delim + str.substring(k,str.length);
		}
		}
		}
	}
	
	primacoppia=str.substr(0,2)
    secondacoppia=str.substr(3,2)
    quadrupla=str.substr(6,4)
    //CONVERTO I VALORI STRINGA IN NUMERI
    numero=parseInt(primacoppia,10)
    numero1=parseInt(secondacoppia,10)
    numero2=parseInt(quadrupla,10)
    //estraggo le posizioni relative agli slash
    primoslash=str.substr(2,1)
    secondoslash=str.substr(5,1)
    //CALCOLO LA LUNGHEZZA DELLE VARIABILE CHE CONTENGONO I NUMERI
    primalunghezza=primacoppia.length
    secondalunghezza=secondacoppia.length
    terzalunghezza=quadrupla.length
    if ((primalunghezza==2)&&(primoslash=="/")&&(numero>=1)&&(numero<=31)&&(secondalunghezza==2)&&(secondoslash=="/")&&(numero1>=1)&&(numero1<=12)&&(terzalunghezza==4)&&(numero2>=1800)&&(numero2<=3000))
    {
    textbox.value = str;
    }
    else
    {
    if (textbox.value != '') {
    textbox.value = '';
     alert("Digitare gg/mm/aaaa oppure ggmmaaaa");
     }
    }
	}


//function Aggiorna(questo,questo1,questo2) {
//alert(questo.value);
//questo.visibility='visible'; 
//questo1.visibility='hidden';
//questo2.visibility='hidden';
//}

function AggTabDom(tab,tabv,tabinv1,tabinv2,tabinv3,tabinv4,tabinv5,tabinv6,tabinv7) {

if (tab=='1') {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    
    
    document.getElementById('i1').src='../p_menu/RICH_1.gif';
    document.getElementById('i2').src='../p_menu/DICH_0.gif';
    document.getElementById('i3').src='../p_menu/FAM_0.gif';
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif';
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif';
    document.getElementById('i6').src='../p_menu/REC_0.gif';
    document.getElementById('i7').src='../p_menu/NOTE_0.gif';
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif';    
    
    
    }
if (tab=='2') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='visible';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    
    
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_1.gif'
    document.getElementById('i3').src='../p_menu/FAM_0.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif'
    document.getElementById('i6').src='../p_menu/REC_0.gif'
    document.getElementById('i7').src='../p_menu/NOTE_0.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif'
    
    }  
if (tab=='3') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='visible';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
   
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_0.gif'
    document.getElementById('i3').src='../p_menu/FAM_1.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif'
    document.getElementById('i6').src='../p_menu/REC_0.gif'
    document.getElementById('i7').src='../p_menu/NOTE_0.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif'
    } 
if (tab=='4') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='visible';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_0.gif'
    document.getElementById('i3').src='../p_menu/FAM_0.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_1.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif'
    document.getElementById('i6').src='../p_menu/REC_0.gif'
    document.getElementById('i7').src='../p_menu/NOTE_0.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif'
    } 
if (tab=='5') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='visible';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_0.gif'
    document.getElementById('i3').src='../p_menu/FAM_0.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_1.gif'
    document.getElementById('i6').src='../p_menu/REC_0.gif'
    document.getElementById('i7').src='../p_menu/NOTE_0.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif'
    } 
if (tab=='6') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='visible';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_0.gif'
    document.getElementById('i3').src='../p_menu/FAM_0.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif'
    document.getElementById('i6').src='../p_menu/REC_1.gif'
    document.getElementById('i7').src='../p_menu/NOTE_0.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif'
    } 
if (tab=='7') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='visible';
    tabinv7.visibility='hidden';
    
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_0.gif'
    document.getElementById('i3').src='../p_menu/FAM_0.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif'
    document.getElementById('i6').src='../p_menu/REC_0.gif'
    document.getElementById('i7').src='../p_menu/NOTE_1.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_0.gif'
    
    }    
if (tab=='8') {  
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='visible';
    document.getElementById('i1').src='../p_menu/RICH_0.gif'
    document.getElementById('i2').src='../p_menu/DICH_0.gif'
    document.getElementById('i3').src='../p_menu/FAM_0.gif'
    document.getElementById('i4').src='../p_menu/ABIT1_0.gif'
    document.getElementById('i5').src='../p_menu/ABIT2_0.gif'
    document.getElementById('i6').src='../p_menu/REC_0.gif'
    document.getElementById('i7').src='../p_menu/NOTE_0.gif'
    document.getElementById('i1_1').src='../p_menu/ALL_1.gif'
    
    }    
    document.getElementById('txtTab').value=tab;
}


function AggTabDic(tab,tabv,tabinv1,tabinv2,tabinv3,tabinv4,tabinv5,tabinv6,tabinv7) {
if (tab=='1') {
    
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    if (tab == '2') {
       
    if (Valorizza()==0) {
        
    if (document.getElementById('txtbinserito').value=='0') {
        mias=MiaFormat(document.getElementById('Dic_Dichiarazione1_txtCognome').value,25) + ' ' + MiaFormat(document.getElementById('Dic_Dichiarazione1_txtNome').value,25) + ' ' + MiaFormat(document.getElementById('Dic_Dichiarazione1_txtDataNascita').value,10) + ' ' + MiaFormat(document.getElementById('Dic_Dichiarazione1_txtCF').value,16) + ' ' + MiaFormat('CAPOFAMIGLIA', 25) + ' ' + MiaFormat('0', 6) + ' ' + MiaFormat('-----', 5) + ' ' + MiaFormat('NO', 2);
        var miaOption = new Option(mias, '0'); 
        document.getElementById('Dic_Nucleo1_ListBox1').options[0]=miaOption; 
        document.getElementById('txtbinserito').value='1';
    }

    tabv.visibility='hidden'; 
    tabinv1.visibility='visible';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_1.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    }
    }  
if (tab=='3') {  
    if (Valorizza()==0) {
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='visible';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_1.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    } 
    }
if (tab=='4') {  
    if (Valorizza()==0) {
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='visible';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_1.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    }
    }
if (tab=='5') {  
    if (Valorizza()==0) {
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='visible';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_1.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    } 
    }
if (tab=='6') { 
    if (Valorizza()==0) { 
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='visible';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_1.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    }  
    }
if (tab=='7') {  
    if (Valorizza()==0) { 
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='visible';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_1.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    }  
    }  
    
if (tab=='8') {  
    if (Valorizza()==0) { 
    tabv.visibility='hidden'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='visible';
    document.getElementById('i1').src='../p_menu/D1_0.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_1.gif'
    document.getElementById('txtTab').value=tab;
    }
    else
    {
    tabv.visibility='visible'; 
    tabinv1.visibility='hidden';
    tabinv2.visibility='hidden';
    tabinv3.visibility='hidden';
    tabinv4.visibility='hidden';
    tabinv5.visibility='hidden';
    tabinv6.visibility='hidden';
    tabinv7.visibility='hidden';
    document.getElementById('i1').src='../p_menu/D1_1.gif'
    document.getElementById('i2').src='../p_menu/D2_0.gif'
    document.getElementById('i3').src='../p_menu/D3_0.gif'
    document.getElementById('i4').src='../p_menu/D4_0.gif'
    document.getElementById('i5').src='../p_menu/D5_0.gif'
    document.getElementById('i6').src='../p_menu/D6_0.gif'
    document.getElementById('i7').src='../p_menu/D7_0.gif'
    document.getElementById('i8').src='../p_menu/ReddConv_0.gif'
    }  
    }      
}

function Valorizza(){
valore=0;
if (document.getElementById('Dic_Dichiarazione1_txtCognome').value==''){
 valore=1;
}
if (document.getElementById('Dic_Dichiarazione1_txtNome').value==''){
 valore=1;
}
if (document.getElementById('Dic_Dichiarazione1_txtCF').value==''){
 valore=1;
}
if (document.getElementById('Dic_Dichiarazione1_txtIndRes').value==''){
 valore=1;
}
if (document.getElementById('Dic_Dichiarazione1_txtCivicoRes').value==''){
 valore=1;
}
if (valore==1){
    alert('Valorizzare tutti i campi!');
    return 1;
    }
    else
    {
    return 0;
}    
}

function MiaFormat(testo,lunghezza){
    //par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtCognome"), TextBox).Text, 25) & " " & par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtNome"), TextBox).Text, 25) & " " & par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtDataNascita"), TextBox).Text, 10) & " " & par.MiaFormat(CType(Dic_Dichiarazione1.FindControl("txtCF"), TextBox).Text, 16)
       
ss=testo;
if (ss.length!=lunghezza){
for (i=ss.length+1;i<lunghezza+1;i++){
   ss += String.fromCharCode(160);
}
}
return ss; 
}

function InserisciRiga(){
    
    if (document.getElementById('Dic_Nucleo1_txtprova').value!='') {
        mias=document.getElementById('Dic_Nucleo1_txtprova').value;
        var miaOption = new Option(mias, document.getElementById('Dic_Nucleo1_txtProgr').value); 
        obj=document.getElementById('Dic_Nucleo1_ListBox1');
        obj.options[obj.length]=miaOption; 
        document.getElementById('Dic_Nucleo1_txtprova').value='';
        obj1=document.getElementById('cmbComp');
        obj1.options[obj1.length]=miaOption; 
    }
    if (document.getElementById('Dic_Nucleo1_txtprova1').value!='') {
        mias=document.getElementById('Dic_Nucleo1_txtprova1').value;
        var miaOption = new Option(mias, document.getElementById('Dic_Nucleo1_txtProgr').value); 
        obj=document.getElementById('Dic_Nucleo1_ListBox2');
        obj.options[obj.length]=miaOption; 
        document.getElementById('Dic_Nucleo1_txtprova1').value='';
    }
    }
    
function ModificaRiga(riga){
    
    if (document.getElementById('Dic_Nucleo1_txtprova').value!='') {
        mias=document.getElementById('Dic_Nucleo1_txtprova').value;
        obj=document.getElementById('Dic_Nucleo1_ListBox1');
        obj.options[riga].text=mias; 
        valore=obj.options[riga].value;
        document.getElementById('Dic_Nucleo1_txtprova').value='';
        obj1=document.getElementById('Dic_Nucleo1_ListBox2');
        if (obj1.length!=0){
            for (i=0;i<=obj1.length-1;i++)
            {
              if (obj1.options[i].value==valore)
                {
                obj1.options[i]=null;
               }
            }
        }
    }
    if (document.getElementById('Dic_Nucleo1_txtprova1').value!='') {
        mias=document.getElementById('Dic_Nucleo1_txtprova1').value;
        var miaOption = new Option(mias, document.getElementById('Dic_Nucleo1_txtProgr').value); 
        obj=document.getElementById('Dic_Nucleo1_ListBox2');
        obj.options[obj.length]=miaOption; 
        document.getElementById('Dic_Nucleo1_txtprova1').value='';
    }
    }

