// File JScript

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
    if ((primalunghezza==2)&&(primoslash=="/")&&(numero>=1)&&(numero<=31)&&(secondalunghezza==2)&&(secondoslash=="/")&&(numero1>=1)&&(numero1<=12)&&(terzalunghezza==4)&&(numero2>=1000)&&(numero2<=3000))
    {
    textbox.value = str;
    }
    else
    {
    if (textbox.value != 'dd/Mm/YYYY') {
    textbox.value = '';
//     alert("Digitare gg/mm/aaaa oppure ggmmaaaa");
     }
    }
	}
	
	//Funzione cancella testo da textbox
	function OnChangeText(textbox)
        {
    if (textbox.value != '') {
    textbox.value = '';
                              }
        }

 function selectText(textbox){

      textbox.select();
      textbox.focus();
    }
    
    function ApriANAUT(ID)
{
var win=null;

win=window.open('../ANAUT/max.aspx?CHIUDI=1&LE=1&ID='+ID,'Dichiarazione','height=480,top=0,left=0,width=680,scrollbars=no');

}
