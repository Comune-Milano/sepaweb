 function vai() {

   pos=document.prov.comuni.selectedIndex;

   if (pos>-1) {

     com=document.prov.comuni.options[pos].value;

     window.opener.document.FormCodFis.Comune.value=com;

     self.close();

   }

 }

 function esci() {

   self.close();

 }