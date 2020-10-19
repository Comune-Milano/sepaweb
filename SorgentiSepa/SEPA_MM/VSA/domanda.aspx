<%@ Page Language="VB" AutoEventWireup="false" CodeFile="domanda.aspx.vb" Inherits="VSA_domanda" %>

<%@ Register Src="../Dom_DocAllegati.ascx" TagName="Dom_DocAllegati" TagPrefix="uc9" %>
<%@ Register Src="../Dom_Dichiara_VSA.ascx" TagName="Dom_Dichiara_Cambi" TagPrefix="uc8" %>
<%@ Register Src="../Dom_Alloggio_VSA.ascx" TagName="Dom_Alloggio_ERP" TagPrefix="uc7" %>
<%@ Register Src="../Dom_Requisiti_VSA.ascx" TagName="Dom_Requisiti" TagPrefix="uc6" %>
<%@ Register Src="../Dom_Note_VSA.ascx" TagName="Note" TagPrefix="uc5" %>
<%@ Register TagPrefix="uc1" TagName="Dom_Richiedente" Src="../Dom_RichiedenteVSA.ascx" %>
<%@ Register Src="../Dom_Decisioni.ascx" TagName="Dom_Decisioni" TagPrefix="uc10" %>
<%@ Register Src="../Dom_Ospiti.ascx" TagName="Dom_Ospiti" TagPrefix="uc11" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;



    function $onkeydown() {

        if (event.keyCode == 8) {
            //alert('Questo tasto non può essere usato!');
            event.keyCode = 0;
        }
    }


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../CONTRATTI/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../CONTRATTI/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../CONTRATTI/jquery.corner.js"></script>
<script type="text/javascript" src="../CONTRATTI/common.js"></script>
<head runat="server">
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
    <title>Domanda</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">
        .CssMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssComuniNazioni
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 166px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssPresenta
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 450px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssFamiAbit
        {
            font-size: 8pt;
            width: 600px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssProv
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 48px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssIndirizzo
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 66px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssLabel
        {
            font-size: 8pt;
            color: black;
            line-height: normal;
            font-style: normal;
            font-family: times;
            font-variant: normal;
        }
        .CssLblValori
        {
            font-size: 8pt;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 13px;
            font-variant: normal;
        }
        .CssEtichetta
        {
            text-align: center;
        }
        .CssNuovoMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        #Form1
        {
            width: 660px;
            height: 966px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function window_onbeforeunload() {
            aa.close();
            if (document.getElementById('H1').value == 1) {
                event.returnValue = "Attenzione...Uscire dalla Domanda utilizzando il pulsante ESCI!! In caso contrario la domanda VERRA' BLOCCATA E NON SARA' PIU' POSSIBILE MODIFICARE!";
            }
        }
        function visibleMotivazioni() {
            if (document.getElementById ('divCondNonAccolta')){
                if (document.getElementById ('Dom_Decisioni1_rdbListDecisione_1').checked == true){
                    document.getElementById ('divCondNonAccolta').style.visibility = 'visible'
                    }               
                else{
                    document.getElementById ('divCondNonAccolta').style.visibility = 'hidden'
                }
                
            
            }
             if (document.getElementById ('divCondNonAccolta2')){
                if (document.getElementById ('Dom_Decisioni1_rdbListRiesame_1').checked == true){
                    document.getElementById ('divCondNonAccolta2').style.visibility = 'visible'
                    }               
                else{
                    document.getElementById ('divCondNonAccolta2').style.visibility = 'hidden'
                }
                
            
            }
             if (document.getElementById ('divNoteDecis0')){
                if (document.getElementById ('Dom_Decisioni1_rdbListDecis0_1').checked == true){
                    document.getElementById ('divNoteDecis0').style.visibility = 'visible'
                    }               
                else{
                    document.getElementById ('divNoteDecis0').style.visibility = 'hidden'
                }
                
            
            }
            if (document.getElementById ('divNoteRies0')){
                if (document.getElementById ('Dom_Decisioni1_rdbListRies0_1').checked == true){
                    document.getElementById ('divNoteRies0').style.visibility = 'visible'
                    }               
                else{
                    document.getElementById ('divNoteRies0').style.visibility = 'hidden'
                }
                
            
            }
      }

        // DATA OSSERVAZIONI
        function visibleDataOsserv() {
            if (document.getElementById ('divOsservazioni')){
                if (document.getElementById ('Dom_Decisioni1_chkOsserv').checked == true){
                    document.getElementById ('divOsservazioni').style.visibility = 'visible';
                    }               
                else{
                    document.getElementById ('divOsservazioni').style.visibility = 'hidden';
                }
            }
        }

        function divSospesione()
        {
            //if (document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value != "3")
            //{
                if (document.getElementById ('divSospesa')){
                    if (document.getElementById ('Dom_Note1_documMancante').value == "1" && document.getElementById ('Dom_Decisioni1_esNegativo1').value == "0"){
                    document.getElementById ('divSospesa').style.visibility = 'visible'
                    document.getElementById('imgAvvisoSospesa').style.visibility = 'visible'
                    document.getElementById('lblSospesa').style.visibility = 'visible'
                    //document.getElementById('lblStatoDOM').style.visibility = 'hidden'
                    if (document.getElementById ('lblScadenza')) {
                        document.getElementById('imgAlertScadenza').style.visibility = 'hidden'
                        document.getElementById('lblScadenza').style.visibility = 'hidden'
                    }
                    if (document.getElementById ('Dom_Note1_chkSosp')){
                    if (document.getElementById ('Dom_Note1_chkSosp').checked == true){
                        document.getElementById ('divSospesa').style.visibility = 'hidden'
                        document.getElementById('imgAvvisoSospesa').style.visibility = 'hidden'
                        document.getElementById('lblSospesa').style.visibility = 'hidden'
                        if (document.getElementById ('lblStatoDOM')) {
                        document.getElementById('lblStatoDOM').style.visibility = 'visible'
                        }
                    }
                    }
                    }               
                    else{
                    document.getElementById ('divSospesa').style.visibility = 'hidden'
                    document.getElementById('imgAvvisoSospesa').style.visibility = 'hidden'
                    document.getElementById('lblSospesa').style.visibility = 'hidden'
                    if (document.getElementById ('lblStatoDOM')) {
                        document.getElementById('lblStatoDOM').style.visibility = 'visible'
                    }

                   }
                
            
                }
            //}
            //else
            //{
               // document.getElementById ('divSospesa').style.visibility = 'hidden'
            //}
        }

//        function copiaMotivazioni(checkMotivi) {

//            if(checkMotivi.checked) {
//                var testo = document.getElementById('Dom_Decisioni1_txtNoteDecisione').value;
//                testo += checkMotivi.value + "\n";
//                document.getElementById('Dom_Decisioni1_txtNoteDecisione').value = testo;
//            }
//            else
//            {
//                var chkDaCanc = checkMotivi.value;
//                var txtBoxPiena = document.getElementById('Dom_Decisioni1_txtNoteDecisione').value;
//                var txtBoxCanc = txtBoxPiena.replace(chkDaCanc,'');
//                document.getElementById('Dom_Decisioni1_txtNoteDecisione').value = txtBoxCanc;
//            }
//        
//        }

        function visibleOspiti() {
        if (document.getElementById ('osp')){
            if (document.getElementById('tipoRichiesta').value == '7'){
                    document.getElementById('Img1').style.visibility = 'visible'
                    document.getElementById('osp').style.visibility = 'visible'
                }

            else{
                    document.getElementById('Img1').style.visibility = 'hidden'
                    document.getElementById('osp').style.visibility = 'hidden'

                    document.getElementById('i2').style.left="184px"
                    document.getElementById('i6').style.left="249px"
                    document.getElementById('i7').style.left="317px"
                    
                    document.getElementById('i8').style.left="402px"
                    document.getElementById('i9').style.left="484px"
                }
                
            }
        }

        function cerca() {
            document.getElementById('txtModificato').value='0';
            if (document.all) {
                finestra = showModelessDialog('Find.htm', window, 'dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus
                finestra.document.close()
            }
            else if (document.getElementById) {
                self.find()
            }
        else window.alert('Il tuo browser non supporta questo metodo')
        }
                

   //########################################## Suddivisione per tipologia di domanda ##########################################
       
        //###################################inizio sezione TIPOLOGIA: Riduzione Canone ##########################################    
       //##################### STAMPA RICEZIONE RICHIESTA #####################
      function RicezRichiesta() {
       document.getElementById('H1').value='0';
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RichRC','');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

       }
        //#####################FINE STAMPA RICEZIONE RICHIESTA #####################
        
        //*************************************************************************************************************************************************
        //##################### STAMPA DOCUMENTO MANCANTE #####################
        function StampaDoc() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallStampaDoc,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallStampaDoc(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=DocMancRC', '', '');
                }
            
            return true;
        }
        //#####################FINE STAMPA DOCUMENTO MANCANTE #####################
        //*************************************************************************************************************************************************
        //#####################STAMPA AVVIO PROCEDIMENTO #####################
        function AvvioProc() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) { 
                if (document.getElementById('txtModificato').value != 1){
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                submit: mycallAvvioProc,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else
            {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProc(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AvvProcRC', '', '');
                }
                  
            return true;
        }
        //#####################FINE STAMPA PROCEDIMENTO #####################
        //***********************************************************************************************************************************************
        //#####################STAMPA AUTOCERTIFICAZIONE #####################

        function AutoCert() {
            if (document.getElementById('txtModificato').value != 1){
            window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AutoCertRC', '');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
        //#####################FINE STAMPA AUTOCERTIFICAZIONE #####################


        //#####################STAMPA EsitoPositivo #####################
        function EsitoPos() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPos,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPos(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPositRC', '', '');
                }
                
            return true;
        }
        //#####################FINE STAMPA EsitoPositivo #####################
        //*************************************************************************************************************************************************

        
        //#####################STAMPA EsitoPositivo DEFINITIVO #####################
        function EsitoPosDEF() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPosDEF,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosDEF(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosDEF', '', '');
                }
                
            return true;
        }
        //#####################FINE STAMPA EsitoPositivo DEFINITIVO #####################
        //*************************************************************************************************************************************************



         //#####################STAMPA EsitoPositivo Provvisorio #####################
        function EsitoPosProvv() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPosProvv,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosProvv(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosRCProvv', '', '');
                }
                
            return true;
        }
        //#####################FINE STAMPA EsitoPositivo Provvisorio #####################
        //*************************************************************************************************************************************************



        //*************************************************************************************************************************************************
        //#####################STAMPA EsitoNegativo #####################
        function EsitoNeg() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNeg,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNeg(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsNegatRC', '', '');
                }
                
            return true;
        }
        //#####################FINE STAMPA EsitoNegativo #####################
        //*************************************************************************************************************************************************
        
        
        //#####################STAMPA EsitoNegativoRiesame SENZA Oss.#####################
        function EsNegRiesame() {
          if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsNegRiesame,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesame(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsNegRiesNOoss', '', '');
                }
            return true;
        }


         // **** Esito Negativo CON Osservazioni ****
        function EsNegRiesConOss() {
          if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsNegRiesConOss,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesConOss(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsNegRiesameRC', '', '');
                }
            return true;
        }



        // **** Rapporto Sintetico RECA ****
       function RapportoRECA() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RapportoRECA', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}
        //#####################FINE STAMPA Rapporto Sintetico RECA  #####################

        //################################### fine sezione TIPOLOGIA: Riduzione Canone ##########################################



        //################################### 16/11/'11 Inizio sezione TIPOLOGIA: Ampliamento ##########################################    

        //##################### STAMPA RICEZIONE RICHIESTA #####################
        function RicRichiesta() {
        document.getElementById('H1').value='0';
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                                 document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                                 '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RicRichiesta','');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

       }
        //#####################FINE STAMPA RICEZIONE RICHIESTA #####################


        //**** Documentazione Mancante ****
       function DocMancante() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallDocMancante,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocMancante(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=DocMancanteAMPL', '','');
                }
            
            return true;
        }

        
        //**** Autocertificazione nucleo di famiglia ****
        function AUcertStFamiglia() {
            if (document.getElementById('txtModificato').value != 1){
            window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=StFamigliaAMPL', '');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
		}


        //**** Convivenza More Uxorio ****
        function MoreUxorio() {
            if (document.getElementById('txtModificato').value != 1){
            window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=MoreUxorioAMPL', '');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
		}


        //**** Convivenza Assistenza ****
        function Assistenza() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AssistenzaAMPL', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}


        //**** Avvio Procedimento ****
        function AvvProcedim() {
        if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1){
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallAvvProcedim,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
         else
            {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvProcedim(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AvvioProcAMPL', '', '');
                }
                  
            return true;
        }

        
        //**** Esito Negativo ****
        function EsNegativo() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsNegativo,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegativo(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsNegativoAMPL', '', '');
                }
                
            return true;
        }


        // **** Esito Postivo ****
        function EsitoPosit() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPosit,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosit(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPositivoAMPL', '', '');
                }
                
            return true;
        }

        // --------------------------------- DOCUMENTI AGGIUNTIVI 27/02/2012 ---------------------------------
        
        // **** Permanenza requisiti ERP (titolare) ****
        function PermReqERP() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=PermanenzaAMPL1', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}

        // **** Permanenza requisiti ERP (nuovo componente) ****
        function PermReqERP2() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=PermanenzaAMPL2', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}


        //**** Sopralluogo ****
        function SopralluogoAMPL() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=SoprallAMPL', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
        }

        function SopralluogoRID() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=SoprallRID', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
        }


        //**** Comunicazione per sopralluogo ****
        function ComSoprallAMPL() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallComSoprallAMPL,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallAMPL(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=ComSopralAMPL', '', '');
                }
                
            return true;
        }

        // **** Presa D'atto Per Rientro ****
        function PresaAttoRientro() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallPresaAttoRientro,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallPresaAttoRientro(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=PresaAttoRientro', '', '');
                }
                
            return true;
        }


        // **** Esito Positivo Riesame ****
        function EsPosRiesAMPL() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPosRiesAMPL,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesAMPL(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosRiesAMPL', '', '');
                }
                
            return true;
        }


        // **** Esito Positivo Rientro ****
        function EsPosRiesRientro() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPosRiesRientro,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesRientro(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosRiesRientro', '', '');
                }
                
            return true;
        }

        // **** Esito Negativo Con Osservazioni ****
        function EsNegRiesameAMPL() {
          if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsNegRiesameAMPL,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesameAMPL(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsNegRiesameAMPL', '', '');
                }
            return true;
        }

        // **** Esito Negativo Senza Osservazioni ****
        function EsNegRiesameNOoss() {
          if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsNegRiesameNOoss,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsNegRiesameNOoss(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsNegRiesameNOoss', '', '');
                }
            return true;
        }


        // **** Provvedimento Definitivo Comune ****
        function ProvvDefComune() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=ProvvDefComune', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}


        // **** Rapporto Sintetico ANF ****
       function RapportoANF() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RapportoANF', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}



       
      //################################### fine sezione TIPOLOGIA: Ampliamento ##########################################



     //################################### 01/12/'11 Inizio sezione TIPOLOGIA: Subentro ##########################################    
     //#####################STAMPA AVVIO PROCEDIMENTO #####################
     function AvvioProcSUB() {
            if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) { 
                if (document.getElementById('txtModificato').value != 1){
                var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
                jQuery.prompt(txt, {
                submit: mycallAvvioProcSUB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            }
            else
            {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProcSUB(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AvvioProcSUB', '', '');
                }
                  
            return true;
        }
        //#####################FINE STAMPA PROCEDIMENTO #####################

     
     //**** Domanda di subentro ****
     function DomSubentro() {
     if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1){
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=DomandaSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
      }
      else
         {
             alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
         }
     }

     //**** Domanda di subentro FFOO ****
     function DomSubentroFFOO() {
       if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=DomandaSUBFFOO', '');
       }
       else
       {
           alert('Salvare le modifiche prima di procedere!');
       }
     }

     //**** Dichiarazione certificazione rinuciante ****
     function CertRinunciante() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=PermReqRSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
     }


     //**** Sopralluogo ****
     function Sopralluogo() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=SoprallSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
     }


     //**** Comunicazione per sopralluogo ****
     function ComSoprall() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallComSoprall,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprall(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=ComSopralSUB', '', '');
                }
                
            return true;
        }



        //******** Doc. Mancante ***********
        function DocMancanteSUB() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallDocMancanteSUB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocMancanteSUB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=DocMancanteSUB', '', '');
                }
                
            return true;
        }

                
        //**** Esito Positivo ****
        function EsPositSub() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositSub,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositSub(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosSUB', '', '');
                }
                
            return true;
        }


        //**** Esito Positivo FFOO ****
        function EsPositFFOO() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositFFOO,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositFFOO(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CAUS=' + document.getElementById('Dom_Dichiara_Cambi1_cmbPresentaD').value + '&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosFFOO', '', '');
                }
                
            return true;
        }

        //**** Esito Positivo FFOO (decesso 2) ****
        function EsPositFFOO2() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositFFOO2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositFFOO2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CAUS=' + document.getElementById('Dom_Dichiara_Cambi1_cmbPresentaD').value + '&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosFFOO2', '', '');
                }
                
            return true;
        }


        //**** Esito Positivo Comun. Commissiariato ****
        function EsPosCommiss() {
        
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosComSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }

        }

        //**** Esito Positivo Condomini ****
         function EsPosCondom() {
        
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosCondom', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }

        }

        

        //**** Esito Positivo Direzione Crediti ****
        function EsPosDirezCrediti() {
        
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosDRCSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }

        }


        //**** Comunicazione Comiss Governo ****
       function ComCommissGoverno() {
        
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=ComGovSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }

        }


        //**** Esito Negativo ****
        function EsiNegat() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegat,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegat(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitNegSUB', '', '');
                }
                
            return true;
        }


        //**** Esito Negativo FFOO ****
        function EsiNegatFFOO1() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegatFFOO1,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsiNegatFFOO1(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitNegFFOO1', '', '');
                }
                
            return true;
        }
        //**** Esito Negativo FFOO ****
        function EsiNegatFFOO2() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegatFFOO2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsiNegatFFOO2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitNegFFOO2', '', '');
                }
                
            return true;
        }
        //**** Esito Negativo FFOO ****
        function EsiNegatFFOO3() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegatFFOO3,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsiNegatFFOO3(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitNegFFOO3', '', '');
                }
                
            return true;
        }


        //**** Lett. di decadenza FFOO ****
        function LetteraDecadenza() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=LettDecadComune', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}

        
        //**** Esito Positivo Riesame ****
        function EsiPosRies() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiPosRies,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiPosRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitPosRiSUB', '', '');
                }
                
            return true;
        }


        //**** Esito Negativo Riesame Con OSS****
        function EsiNegaRies() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegaRies,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegaRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitNegRiesSUB', '', '');
                }
                
            return true;
        }



        //**** Esito Negativo Riesame Senza Oss ****
        function EsiNegaRiesNoOSS() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegaRiesNoOSS,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegaRiesNoOSS(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitNegRiesSUBNoOS', '', '');
                }
                
            return true;
        }




        function RapportoVAIN() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RapportoVAIN', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}

        //**** Lett. di trasformazione FFOO ****
        function LetteraTrasf() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=LettTrasfor', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}

        function RapportoVAINFFOO() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RapportoVAINFFOO', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}
     //################################### fine sezione TIPOLOGIA: Subentro ##########################################






     //################################### 05/12/'11 Inizio sezione TIPOLOGIA: Voltura ##########################################    
     
     //***** Modulo richiesta Voltura ******
     function RichVoltura() {
        
          if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RicezRicVOL', '');
          }
          else
          {
                    alert('Salvare le modifiche prima di procedere!');
          }

    }


     //***** Modulo richiesta Voltura *****
    function AvvProcVoltura() {
        if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) { 
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallAvvProcVoltura,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
         else
            {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvProcVoltura(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AvvProcedVOL', '', '');
                }
                  
            return true;
        }

     
     //***** Sopralluogo *****
     function ModSoprall() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=ModuloSoprallVOL', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
     }


     //***** Com. Sopralluogo *****
     function SoprallUtente() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallSoprallUtente,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallSoprallUtente(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=SoprUtenteVOL', '', '');
                }
                
            return true;
        }


        //***** Com. Esito Positivo *****
        function EsPositVolt() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositVolt,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }
        function mycallEsPositVolt(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosiVOL', '', '');
                }
                
            return true;
        }


        //***** Com. Esito Negativo *****
        function EsitoNega() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNega,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNega(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoNegaVOL', '', '');
                }
                
            return true;
        }


        //***** Com. Esito Pos. Riesame *****
        function EsitoPosRies() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPosRies,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoPosRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoPosRiesVOL', '', '');
                }
                
            return true;
        }



        //***** Com. Esito Neg. Riesame *****
        function EsitoNegatRies() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatRies,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegatRies(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoNegatRiesVOL', '', '');
                }
                
            return true;
        }


        
        //***** Com. Esito Neg. Riesame No Osservazioni *****
        function EsitoNegatRiesNO() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatRiesNO,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegatRiesNO(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoNegatRiesNOVOL', '', '');
                }
                
            return true;
        }
        

        // STAMPA Rapporto Sintetico CAIN
        function RapportoCAIN() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RapportoCAIN', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}
        // FINE STAMPA Rapporto Sintetico CAIN  
     //################################### fine sezione TIPOLOGIA: Voltura ##########################################    



     //################################### 13/12/'11 Inizio sezione TIPOLOGIA: Ospitalità ##########################################    
     
     
     //***** Modulo richiesta Ospitalità ******
     function RichOSP() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RichOSP', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
     }

     //***** Modulo richiesta Ospitalità Badanti ******
     function RichOSPbada() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RichOSPbada', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
     }

     //***** Modulo richiesta Ospitalità Autorizz.scolastiche ******
     function RichOSPscol() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RichOSPscol', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
     }


    //***** Autocertificazione stato di famiglia Ospitalità ******
    function StFamigliaOSP(){
            if (document.getElementById('txtModificato').value != 1){
            window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=StFamigliaOSP', '');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
    }

    //**** Documentazione Mancante ****
    function DocMancanteOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallDocMancanteOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocMancanteOSP(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=DocMancanteOSP', '','');
                }
            
            return true;
        }

        
        //**** Avvio Procedimento ****
        function AvvioProcOSP() {
        if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) { 
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallAvvioProcOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
         else
            {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
            }
        }

        function mycallAvvioProcOSP(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=AvvioProcOSP', '', '');
                }
                  
            return true;
        }


        //***** Sopralluogo *****
     function SopralluogoOSP() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=SopralluogoOSP', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
     }


     //***** Com. Sopralluogo *****
     function ComSoprallOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallComSoprallOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=ComSoprallOSP', '', '');
                }
                
            return true;
        }



        //***** Com. Esito Negativo *****
        function EsiNegatOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegatOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegatOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsiNegatOSP', '', '');
                }
                
            return true;
        }



        //***** Com. Esito Positivo *****
        function EsPositOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPositOSP', '', '');
                }
                
            return true;
        }

         //***** Com. Esito Positivo Badanti *****
        function EsPositOSPbada() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositOSPbada,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositOSPbada(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPositOSPbada', '', '');
                }
                
            return true;
        }


         //***** Com. Esito Positivo Autorizz.scolastiche*****
        function EsPositOSPscol() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositOSPscol,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositOSPscol(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPositOSPscol', '', '');
                }
                
            return true;
        }

        //***** Com. Esito Pos. Riesame *****
        function EsPosRiesOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPosRiesOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosRiesOSP', '', '');
                }
                
            return true;
        }

        //***** Com. Esito Pos. Riesame Badanti *****
        function EsPosRiesOSPbada() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPosRiesOSPbada,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesOSPbada(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosRiesOSPbada', '', '');
                }
                
            return true;
        }


        //***** Com. Esito Pos. Riesame Autorizz. Scolastica *****
        function EsPosRiesOSPscol() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPosRiesOSPscol,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPosRiesOSPscol(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsPosRiesOSPscol', '', '');
                }
                
            return true;
        }

        //***** Com. Esito Neg. Riesame *****
        function EsitoNegRiesOsservOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegRiesOsservOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegRiesOsservOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoNegRiesOsservOSP', '', '');
                }
                
            return true;
        }


        
        //***** Com. Esito Neg. Riesame No Osservazioni *****
        function EsitoNegRiesNoOsservOSP() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegRiesNoOsservOSP,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsitoNegRiesNoOsservOSP(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=EsitoNegRiesNoOsservOSP', '', '');
                }
                
            return true;
        }

        // STAMPA Rapporto Sintetico OSP
        function RapportoOSP() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&TIPO=RapportoOSP', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}
        // FINE STAMPA Rapporto Sintetico OSP  


       //################################### 30/01/'12 Inizio sezione TIPOLOGIA: Cambio Consensuale ##########################################    

       //**** Ricezione Richiesta ****
       function RichCAMB() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=RichCAMB', '');
            }
            else
            {
                 alert('Salvare le modifiche prima di procedere!');
            }
       }


       //**** Ricezione Richiesta2 ****
       function RichCAMB2() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=RichCAMB2', '');
            }
            else
            {
                 alert('Salvare le modifiche prima di procedere!');
            }
       }


       //**** Dichiarazione Perm. Requisiti ****
       function DichPermanenzaReq() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=DichPermanenza', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
       }

       //**** Dichiarazione Perm. Requisiti 2 ****
       function DichPermanenzaReq2() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=DichPermanenza2', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
       }



        //**** Documentazione Mancante ****
        function DocMancanteCAMB() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallDocMancanteCAMB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocMancanteCAMB(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=DocMancCAMB', '','');
                }
            
            return true;
        }

        //**** Documentazione Mancante 2****
        function DocMancanteCAMB2() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallDocMancanteCAMB2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallDocMancanteCAMB2(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=DocMancCAMB2', '','');
                }
            
            return true;
        }


        
        //**** Avvio Procedimento ****
        function AvvioProcCAMB() {
        if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) { 
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallAvvioProcCAMB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
         else
           {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
           }
        }

        function mycallAvvioProcCAMB(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=AvvProcedCAMB', '', '');
                }
                  
            return true;
        }



        //**** Avvio Procedimento 2****
        function AvvioProcCAMB2() {
        if (document.getElementById('Dom_DocAllegati_documAlleg').value == 1) { 
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallAvvioProcCAMB2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
         else
           {
                alert('Attenzione...per stampare tale modello selezionare nello specifico Tab i documenti allegati alla domanda!');
           }
        }

        function mycallAvvioProcCAMB2(e, v, m, f) {
            if (v != undefined)
            
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=AvvProcedCAMB2', '', '');
                }
                  
            return true;
        }



        //***** Sopralluogo *****
        function SopralluogoCAMB() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value+'&TIPO=SoprallCAMB', '');
            }
            else
            {
                    alert('Salvare le modifiche prima di procedere!');
            }
        }


        //***** Com. Sopralluogo *****
        function ComSoprallCAMB() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallComSoprallCAMB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallCAMB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value+'&TIPO=ComSoprallCAMB', '', '');
                }
                
            return true;
        }


        //***** Com. Sopralluogo 2 *****
        function ComSoprallCAMB2() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallComSoprallCAMB2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallComSoprallCAMB2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value+'&TIPO=ComSoprallCAMB2', '', '');
                }
                
            return true;
        }



        //***** Com. Esito Negativo *****
        function EsiNegatCAMB() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegatCAMB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegatCAMB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value+'&TIPO=EsNegaCAMB', '', '');
                }
                
            return true;
        }


        //***** Com. Esito Negativo 2 *****
        function EsiNegatCAMB2() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsiNegatCAMB2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsiNegatCAMB2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=EsNegaCAMB2', '', '');
                }
                
            return true;
        }


        //***** Com. Esito Positivo *****
        function EsPositCAMB() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositCAMB,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositCAMB(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=EsPositCAMB', '', '');
                }
                
            return true;
        }

        //***** Com. Esito Positivo 2*****
        function EsPositCAMB2() {
            if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsPositCAMB2,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

        function mycallEsPositCAMB2(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value +'&TIPO=EsPositCAMB2', '', '');
                }
                
            return true;
        }

        // STAMPA Rapporto Sintetico OSP
        function RapportoCACO() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value +
                       '&NUMCONT=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value +'&NUMCONT2='+ document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value+'&TIPO=RapportoCACO', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}
        // FINE STAMPA Rapporto Sintetico OSP  


        function ModOffertaAlloggio() {
        window.open('ReportAbbinamento.aspx?ABB=' + document.getElementById('numOfferta').value + '&IDALL=' + document.getElementById('idAlloggio').value,'');
        }
        //Documenti per Cambi In Emergenza


        //window.open('ModelliCambio22/ElencoStampe.aspx?IDDOM=' + <%=lIdDomanda %>, '');

        function ComposizioneNucleo() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallComposizioneNucleo,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallComposizioneNucleo(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
					window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot +'&TIPODOC=CompNucleo', '');
            }
                
            return true;
        }

        function DocumentazioneMancante() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallDocumentazioneMancante,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
	
        }

	    function mycallDocumentazioneMancante(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
					window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=DocMancante', '');
            }
			
            return true;
	    }

        function EsitoNegativoArt22() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegativoArt22,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }

        }

	    function mycallEsitoNegativoArt22(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				    window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegativo', '');
            }
            return true;
	    }

        function EsitoNegatAllAdeguato() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatAllAdeguato,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallEsitoNegatAllAdeguato(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegatAllAdeg', '');
            }
                
            return true;
        }

        function EsitoNegativoISEE() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegativoISEE,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallEsitoNegativoISEE(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				    window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegatISEE', '');
            }
                
            return true;
        }

        function EsitoNegatMorosita() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatMorosita,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoNegatMorosita(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegatMoros', '');
            }
            return true;
        }

        function EsitoNegatMorosita() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatMorosita,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }

        function mycallEsitoNegatMorosita(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegatMoros', '');

            }
            return true;
        }

        function EsitoNegatRequisiti() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatRequisiti,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoNegatRequisiti(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				    window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegatRequis', '');
                }
                
            return true;
        }

        function EsitoPositivoArt22() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPositivoArt22,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoPositivoArt22(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				    window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsPositivo', '');
            }
            return true;
        }

        function EsitoPositMorosita() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPositMorosita,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoPositMorosita(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				    window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsPositMoros', '');
            }
            return true;
        }

        function EsitoNegatRicorso() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoNegatRicorso,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoNegatRicorso(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsNegatRicorso', '');
            }
            return true;
        }


        function EsitoPositRicorso() {
		    if (document.getElementById('txtModificato').value != 1){
            var txt = 'Inserire il numero di protocollo:<br/><input id="txtprot" type="text" name = "txtprot" value = "" />';
            jQuery.prompt(txt, {
                submit: mycallEsitoPositRicorso,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            }
            else
            {
               alert('Salvare le modifiche prima di procedere!');
            }
        }
        function mycallEsitoPositRicorso(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
				window.open('ModelliCambio22/StampeOutput.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+ '&PROT='+ f.txtprot + '&TIPODOC=EsPositRicorso', '');
            }
                
            return true;
        }

        function ElStampeART22() {

            window.open('ModelliCambio22/ElencoStampe.aspx?IDDOM=' + <%=lIdDomanda %>, '');
        }

    </script>
</head>
<script language="javascript" type="text/javascript" for="window" event="onbeforeunload">


if (document.getElementById('H1').value==1) {
    return window_onbeforeunload()
}

</script>
<script type="text/javascript" src="Funzioni.js"></script>
<script type="text/javascript">
    var win = null;
    LeftPosition = (screen.width) ? (screen.width - 250) / 2 : 0;
    TopPosition = (screen.height) ? (screen.height - 150) / 2 : 0;
    LeftPosition = LeftPosition;
    TopPosition = TopPosition;

    aa = window.open('loading.htm', '', 'height=90,top=' + TopPosition + ',left=' + LeftPosition + ',width=250');
</script>
<script type="text/javascript">

    function ConfermaStampa() {
        var tipo;
        tipo = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;
        if (tipo == 3) {
            if (document.getElementById('Dom_Alloggio_ERP1_cmbTipoU').value == 0) {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, procedere con la stampa?");
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
            else {
                document.getElementById('conferma').value = '0';
                alert('Attenzione...è possibile utilizzare solo alloggi di tipo E.R.P.');
            }

        }
    }

    function ConfContabilizza() {
    chiediConferma = window.confirm('Sei sicuro di voler contabilizzare l\'importo calcolato?');
            if (chiediConferma == true) {
                document.getElementById('ConfRidCan').value = '1';
            }
            else {
                document.getElementById('ConfRidCan').value = '0';
            }
    }

    function InserisciCanone() {
           
            var txt = 'Importo di locazione annuo:<br/><input id="txtcanone" type="text" name = "txtcanone" value = "" onkeypress="SostPuntVirg(event, this);" onchange="valid(this,\'notnumbers\');AutoDecimal2(this);" /><br />N.ro provvedimento:<br /><input type="text" id="txtNumProvv" name="txtNumProvv" /><br />Data provvedimento:<br /><input type="text" id="txtDataProvv" name="txtDataProvv" onkeypress="CompletaData(event,this);"/>';
            jQuery.prompt(txt, {
                submit: mycallInserCanone,
                buttons: { Procedi: '1', Annulla: '2' },
                show: 'slideDown',
                focus: 0
                });
            
        }

        function mycallInserCanone(e, v, m, f) {
        
            if (v != undefined)
                if (v != '2') {
                    document.getElementById('importoCanone').value= f.txtcanone;
                    document.getElementById('numProvvedim').value= f.txtNumProvv;
                    document.getElementById('dataProvvedim').value= f.txtDataProvv;
                    if (document.getElementById('importoCanone').value !=0 && document.getElementById('numProvvedim').value != 0 && document.getElementById('dataProvvedim').value != 0)
                    {
                        ConfRiduzCanone();
                        document.getElementById('Dom_Decisioni1_btnRiduzCanone').click();
                    }
                    else{
                        alert('Compilare i campi!');
                    }
                    
                }
                
            return true;
        }


    function ConfRiduzCanone() {
        var chiediConferma
        var tipoDomanda
        var msg1 = "Attenzione, procedere con l\'adeguamento del canone?"
        //var msg1 = "Attenzione, procedendo con l\'operazione, se la domanda risultasse idonea, le variazioni al canone SARANNO APPLICATE DALLA PROSSIMA BOLLETTAZIONE UTILE.\nGli eventuali importi a credito dell\'inquilino, maturati dalla data di presentazione della domanda ad oggi, saranno gestiti previa verifica del Comune di Milano o ALER.\n\nSei sicuro di voler proseguire?"
        //var msg2 = "Attenzione, procedendo con l\'operazione, se la domanda risultasse idonea, le variazioni al canone SARANNO APPLICATE DALLA PROSSIMA BOLLETTAZIONE UTILE. Gli eventuali importi a credito dell\'inquilino, maturati dalla data di presentazione della domanda ad oggi, saranno gestiti previa verifica del Comune di Milano o ALER.\n\nSei sicuro di voler proseguire?"
        var msg2 = "Attenzione, procedere con l\'ampliamento del nucleo familiare?"
        var msg3 = "Attenzione, procedere con la variazione di intestazione?"
        var msg4 = "Attenzione, procedere con il cambio di intestazione? Il processo provvederà alla formalizzazione di un nuovo contratto di locazione in stato di BOZZA.\n\nSei sicuro di voler proseguire?"
        var msg5 = "Attenzione, procedendo con l\'operazione verrà concessa l\'autorizzazione all\'ospitalità temporanea per la persona inserita.\n\nSei sicuro di voler proseguire?"
        var msg6 = "Attenzione, procedendo con l\'operazione la domanda verrà messa in graduatoria per la stipula dei nuovi contratti.\n\nSei sicuro di voler proseguire?"
        var msg7 = "Attenzione, procedendo con l\'operazione la domanda verrà messa in assegnazione per una nuova offerta di alloggio.\n\nSei sicuro di voler proseguire?"
        var msg8 = "Attenzione, procedere con la creazione posizione abusiva? Il processo provvederà alla formalizzazione di un nuovo contratto di locazione in stato di BOZZA.\n\nSei sicuro di voler proseguire?"
        var msg9 = "Attenzione, procedendo con l\'operazione il processo provvederà alla formalizzazione di un nuovo contratto di locazione in stato di BOZZA.\n\nSei sicuro di voler proseguire?"
        tipoDomanda = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

        if (tipoDomanda == '3'){
            chiediConferma = window.confirm(msg1);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }

        if (tipoDomanda == '2'){
            chiediConferma = window.confirm(msg2);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }


        if (tipoDomanda == '1'||tipoDomanda == '6'){
            chiediConferma = window.confirm(msg3);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }

        
        if (tipoDomanda == '0'){
            chiediConferma = window.confirm(msg4);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }

        if (tipoDomanda == '7'){
            chiediConferma = window.confirm(msg5);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }

        if (tipoDomanda == '5'){
            chiediConferma = window.confirm(msg6);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }



           if (tipoDomanda == '8'){
            chiediConferma = window.confirm(msg8);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }


             if (tipoDomanda == '9'){
             chiediConferma = window.confirm(msg9);
              
            if (chiediConferma == true) {
                
                document.getElementById('ConfRidCan').value = '1';
                
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }




        if (tipoDomanda == '4'){
            chiediConferma = window.confirm(msg7);
            if (chiediConferma == true) {
        
                document.getElementById('ConfRidCan').value = '1';
        
            }
            else {
                document.getElementById('ConfRidCan').value = '0';

            }
        }


    }

    function RimborsoDeposito() {
           
           var txt = 'Selezionare le operazioni che si intendono eseguire sul NUOVO contratto: <br /><input type="radio" id="docDeposito" name="rdbScegli" value="docDeposito"/>Restituzione vecchio deposito e calcolo nuovo importo<br /><input type="radio" id = "docDeposito2" name="rdbScegli" value="docDeposito2"/>Restituzione deposito nel nuovo RU <br /><br /><input type="checkbox" id="chkRegistr" name="chk1" value="chkRegistr"/>Import dati registrazione fiscale';
           jQuery.prompt(txt, {
               submit: mycallRimborsoDeposito,
               buttons: { Procedi: '1', Annulla: '2' },
               show: 'slideDown',
               focus: 0
               });
          
        };

        function mycallRimborsoDeposito(e, v, m, f) {
            if (v != undefined)
                if (v != '2') {
                    if (f.rdbScegli != undefined) {
                         errore = '0';

                         if (f.rdbScegli == "docDeposito") {
                            document.getElementById('restituisciCauz').value = '0';
                         }
                         if (f.rdbScegli == "docDeposito2") {
                            document.getElementById('restituisciCauz').value = '1';
                         }
                         if (f.chk1 == "chkRegistr"){
                            document.getElementById('fl_import_registr').value = '1';
                         }else
                         {
                            document.getElementById('fl_import_registr').value = '0';
                         }
                        
                        chiediConferma = window.confirm('Attenzione, il processo provvederà alla formalizzazione di un nuovo contratto di locazione ERP in stato di BOZZA.\n\nSei sicuro di voler proseguire?');
                        if (chiediConferma == true) {
                            document.getElementById('ConfRidCan').value = '1';
                            document.getElementById('Dom_Decisioni1_btnRiduzCanone').click();
                        }
                        else {
                            document.getElementById('ConfRidCan').value = '0';
                        }
                        
                     }
                     else {
                         alert('Selezionare la funzione che si intende eseguire!');
                         errore = '1';
                         return false;
                     }
                }
        };
    function Indici() {

        window.open("indici.aspx?" + document.getElementById('txtIndici').value, "", "top=0,left=0,width=490,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no");

    }

    function TempiProcessi() {

        window.open("Locatari/TempisticaProcessi.aspx", "", "top=0,left=0,width=600,height=400,resizable=no,menubar=no,toolbar=no,scrollbars=no");

    }

    function CalcoloCanone() {
        var tipo;
        tipo = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

        document.getElementById('Hcanone').value = '1';
        if (tipo != '5') {
            if (document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value != '') {
                   window.open("Canone.aspx?ID=" + document.getElementById('HiddenID').value + '&T=' + tipo + '&COD=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&IDUNITA=' + document.getElementById('Dom_Alloggio_ERP1_txtCodiceUnita').value, "", "top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes");
            }
            else 
            {
                alert('Inserire il codice del contratto e assicurarsi di aver elaborato la domanda!');
            }
        }
        else {
            if (document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value != '') {
                window.open("Canone.aspx?ID=" + document.getElementById('HiddenID').value + '&T=' + tipo + '&COD=' + document.getElementById('Dom_Alloggio_ERP1_txtNumContratto').value + '&IDUNITA=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value, "", "top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes");
            }
            else {
                alert('Inserire il codice unità oggetto dello scambio e assicurarsi di aver elaborato la domanda!');
            }
        }



    }

    function ConfermaEsci() {

            if (document.getElementById('txtModificato').value == '1') {
            
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?\nUSCIRE SENZA SALVARE CAUSERA\' LA PERDITA DELLE MODIFICHE!\nPER NON USCIRE PREMERE IL PULSANTE ANNULLA.");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';

                }
                else {
                
                    AggContratto();
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'visible';
                    }

                }

            }
            else{
                    
                    AggContratto();
                    if (document.getElementById('Attesa')) {
                        document.getElementById('Attesa').style.visibility = 'visible';
                    }
                
                }

        }

function AggContratto() {

if (typeof opener != 'undefined') 
                    {
                        if (typeof opener.opener != 'undefined') 
	                    {
                            if (typeof opener.opener != 'unknown')
                            {
		                        if (opener.opener.name.substring(0,9) == 'Contratto')
		                        {
                                    if (opener.opener.document.getElementById('imgSalva'))
			                        {
                                        opener.opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                        opener.opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                        opener.opener.document.getElementById('MostrMsgSalva').value='0';
                                        opener.opener.document.getElementById('imgSalva').click();
                                    }
                                }
                            }
	                    }
	                    if (typeof opener.opener != 'undefined') 
                        {
                            if (typeof opener.opener != 'unknown')
                            {
                                if (opener.name.substring(0,9) == 'Contratto')
		                        {
                                    if (opener.document.getElementById('imgSalva'))
			                        {
                                        opener.document.getElementById('nuovocanone').value = document.getElementById('nuovocanone').value;
                                        opener.document.getElementById('pressoCOR').value = document.getElementById('pressoCOR').value;
                                        if (opener.opener.document.getElementById('MostrMsgSalva')) {
                                        opener.opener.document.getElementById('MostrMsgSalva').value='0';
                                        }
                                        opener.document.getElementById('imgSalva').click();
                                    }
                                }
                            }
                        }
                    }
		}                   

//function rinnovaContratto() {
//                    if (typeof opener != 'undefined') 
//                    {
//                        if (opener.opener.name.substring(0,9) == 'Contratto')
//                        {
//                           var dataR;
//                           dataR = document.getElementById('data_riconsegna').value;
//                           opener.opener.window.document.getElementById('datariconsegna').value = dataR;
//                            opener.opener.document.getElementById('imgSalva').click();
//                            
//                        }
//                        if (opener.opener.opener.name.substring(0,9) == 'Contratto')
//                        {
//                           
//                                    opener.opener.window.ApriRinnovoUSD();
//                            
//                        }

//                    }
//                    else 
//                    {
//                        if (opener.name.substring(0,9) == 'Contratto')
//                        {
//                            
//                            opener.opener.window.ApriCambioBOX();
//                            

//                        }
//                    
//                    }
//                                        
//}



    function ApriContratto() {
        if (document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value != '') {
            window.open('DatiContratto.aspx?COD=' + document.getElementById('Dom_Dichiara_Cambi1_txtCodContrattoScambio').value, 'DatiContratto', '');
        }
    }
    function ElStampe() { 
    
    window.open('ElencoStampe.aspx?IDDIC='+ <%=lIdDichiarazione %>,'elstmp','');
    
    }

    function VisualizzaEventi() {

    window.open('EventiVSA.aspx?IDDOM='+ <%=lIdDomanda %>,'eleventi','');

    }


</script>
<body onload="return AggTabDom(document.getElementById('txtTab').value,document.getElementById('ric').style,document.getElementById('dic').style,document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style,
document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);"
    bgcolor="#f2f5f1">
    <div id="Attesa" style="position: absolute; width: 674px; top: 0px; left: 0px; background-color: #ffffff;
        visibility: hidden; z-index: 900; display: block; height: 530px;">
        <table align="center" style="position: absolute; top: 250px; left: 264px; font-family: Verdana;
            font-size: 10pt;">
            <tr>
                <td>
                    <img src='../NuoveImm/load.gif' alt='caricamento in corso' style="position: absolute;
                        top: -29px; left: 44px;" />
                    <br />
                    chiusura in corso...
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <form id="Form1" method="post" runat="server">
    <table onmousedown="Uscita=1;document.getElementById('H1').value='0';" onmouseout="document.getElementById('H1').value='1';"
        style="width: 73px; position: absolute; top: 23px; left: 353px; z-index: 800;">
        <tr>
            <td>
                <asp:Menu ID="MenuStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                    Orientation="Horizontal" RenderingMode="Table">
                    <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                    <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                        ForeColor="#0066FF" Width="260px" />
                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                        VerticalPadding="1px" />
                    <Items>
                        <asp:MenuItem ImageUrl="../NuoveImm/Img_Documentazione.png" Selectable="False" Value="">
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png"
        Style="z-index: 127; left: 184px; cursor: pointer; position: absolute; top: 29px"
        ToolTip="Anagrafe della popolazione" />
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
        width: 674px; position: absolute; top: 0px">
        <tr>
            <td style="width: 670px; text-align: right">
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
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    <br />
    <%--<asp:Image ID="imgAlert" runat="server" ImageUrl="../IMG/Alert.gif" Style="z-index: 125;
        left: 23px; position: absolute; top: 480px; height: 17px;" Visible="false" />
    <asp:Label ID="lblAvviso" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        ForeColor="#0000C0" Style="z-index: 125; left: 45px; position: absolute; top: 484px;
        width: 602px;" Text="ATTENZIONE, questa è una domanda per OSPITALITA'. Aggiungere gli ospiti nell'apposita sezione!"
        Visible="false"></asp:Label>--%>
    <asp:ImageButton ID="btnVisualizzaDich" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
        Style="z-index: 100; left: 41px; position: absolute; top: 829px;" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" />
    <asp:ImageButton ID="btnNote" Style="z-index: 100; left: 460px; position: absolute;
        top: 391px" runat="server" ImageUrl="..\p_menu\NOTE_0.gif" Height="21px" Width="42px"
        CausesValidation="False" TabIndex="7" Visible="False"></asp:ImageButton>
    &nbsp;
    <asp:ImageButton ID="btnAbitative2" Style="z-index: 101; left: 312px; position: absolute;
        top: 390px" runat="server" ImageUrl="..\p_menu\ABIT2_0.gif" Height="21px" Width="76px"
        CausesValidation="False" TabIndex="5" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnAbitative1" Style="z-index: 102; left: 234px; position: absolute;
        top: 391px" runat="server" ImageUrl="../p_menu\ABIT1_0.gif" Height="21px" Width="76px"
        CausesValidation="False" TabIndex="4" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnFamiliari" Style="z-index: 103; left: 168px; position: absolute;
        top: 399px" runat="server" ImageUrl="../p_menu\FAM_0.gif" Height="21px" Width="64px"
        CausesValidation="False" TabIndex="3" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnDichiara" Style="z-index: 104; left: 103px; position: absolute;
        top: 392px" runat="server" ImageUrl="../p_menu\DICH_0.gif" Height="21px" Width="63px"
        CausesValidation="False" TabIndex="2" Visible="False"></asp:ImageButton>
    <asp:Label ID="lblISBAR" Style="z-index: 105; left: 475px; position: absolute; top: 64px;
        width: 77px;" runat="server" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="#FFC080" CssClass="CssLblValori" Font-Bold="True">0</asp:Label>
    <asp:Label ID="lblPGDic" Style="z-index: 106; left: 324px; position: absolute; top: 64px;
        width: 122px;" runat="server" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="#FFC080" CssClass="CssLblValori">F205-000000000-00</asp:Label>
    <asp:Label ID="Label4" Style="z-index: 107; left: 449px; position: absolute; top: 64px;
        width: 29px;" runat="server" Height="18px" CssClass="CssLabel" Font-Bold="True">ISEE</asp:Label>
    <asp:Label ID="lblPosizione" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
        ForeColor="White" Height="18px" Style="z-index: 107; left: 77px; position: absolute;
        top: 447px; background-color: red; text-align: center" Visible="False" Width="493px">POSIZIONE IN GRADUATORIA</asp:Label>
    <asp:Label ID="Label3" Style="z-index: 108; left: 281px; position: absolute; top: 64px"
        runat="server" Height="18px" Width="43px" CssClass="CssLabel" Font-Bold="True">N. Dich.</asp:Label>
    <asp:Label ID="Label2" Style="z-index: 109; left: 181px; position: absolute; top: 63px"
        runat="server" Height="18px" Width="33px" CssClass="CssLabel" Font-Bold="True">Data</asp:Label>
    <asp:Label ID="Label1" Style="z-index: 110; left: 4px; position: absolute; top: 64px"
        runat="server" Height="18px" Width="31px" CssClass="CssLabel" Font-Bold="True">PG N.</asp:Label>
    <asp:Label ID="lblPG" Style="z-index: 111; left: 36px; position: absolute; top: 64px"
        runat="server" Width="66px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="#FFC080" CssClass="CssLblValori">0000000000</asp:Label>
    <asp:Label ID="lblSlash" runat="server" Text="/" Style="z-index: 111; left: 106px;
        position: absolute; top: 65px" CssClass="CssLblValori" Visible="False"></asp:Label>
    <asp:Label ID="lblPGcoll" Style="z-index: 111; left: 111px; position: absolute; top: 64px"
        runat="server" Width="66px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="#FFC080" CssClass="CssLblValori" Visible="False">0000000000</asp:Label>
    <asp:TextBox ID="txtDataPG" Style="z-index: 112; left: 207px; position: absolute;
        top: 62px" runat="server" CssClass="CssMaiuscolo" Columns="10" MaxLength="10"
        Width="65px" TabIndex="1"></asp:TextBox>
    <asp:ImageButton ID="btnRichiedente" Style="z-index: 113; left: 16px; position: absolute;
        top: 392px" runat="server" ImageUrl="../p_menu\RICH_0.gif" Height="21px" Width="85px"
        CausesValidation="False" TabIndex="1" Visible="False"></asp:ImageButton>
    <uc1:Dom_Richiedente ID="Dom_Richiedente1" runat="server" Visible="true"></uc1:Dom_Richiedente>
    <%--    <asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 114;
        left: 38px; position: absolute; top: 64px" Width="26px">06-1</asp:Label>
    --%><%--<asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 115;
        left: 134px; position: absolute; top: 64px" Width="31px">F205</asp:Label>
    --%>&nbsp;
    <uc7:Dom_Alloggio_ERP ID="Dom_Alloggio_ERP1" runat="server" Visible="true" />
    <uc5:Note ID="Dom_Note1" runat="server" Visible="true" />
    <uc10:Dom_Decisioni ID="Dom_Decisioni1" runat="server" />
    <uc11:Dom_Ospiti ID="Dom_Ospiti1" runat="server" />
    &nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="btnRequisiti" runat="server" CausesValidation="False" Height="21px"
        ImageUrl="../p_menu\REC_0.gif" Style="z-index: 116; left: 391px; position: absolute;
        top: 391px" Width="66px" TabIndex="6" Visible="False" />
    <uc6:Dom_Requisiti ID="Dom_Requisiti_Cambi1" runat="server" Visible="true" />
    <asp:ImageButton ID="imgAttendi" runat="server" ImageUrl="../IMG/A1.gif" Style="z-index: 117;
        left: 295px; position: absolute; top: 247px" Visible="False" />
    <uc8:Dom_Dichiara_Cambi ID="Dom_Dichiara_Cambi1" runat="server" />
    <uc9:Dom_DocAllegati ID="Dom_DocAllegati" runat="server" />
    <asp:Label ID="lblIdDomanda" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblIdDichiarazione" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="ProgrComponente" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblBando" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:Label ID="lblIdBando" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        Font-Names="Arial" Font-Size="8pt" Height="31px" Width="368px" Visible="False"
        Style="z-index: 119; left: 257px; position: absolute; top: 204px">
        <HeaderStyle BackColor="white" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
        <Columns>
            <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA"></asp:BoundColumn>
            <asp:BoundColumn DataField="PERC_INVAL" HeaderText="INVALIDITA"></asp:BoundColumn>
            <asp:BoundColumn DataField="INDENNITA_ACC" HeaderText="ACC"></asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
    <img id="i1" src="../p_menu/RICH_0.gif" style="z-index: 129; left: 10px; cursor: pointer;
        position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('1',document.getElementById('ric').style,
                document.getElementById('dic').style,
                document.getElementById('req').style,document.getElementById('not').style,
                document.getElementById('all').style,document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <img id="i2" src="../p_menu/DICH_0.gif" style="z-index: 130; cursor: pointer; left: 246px;
        position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('2',document.getElementById('ric').style,document.getElementById('dic').style, document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style,document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    &nbsp;&nbsp;&nbsp;
    <img id="i6" src="../p_menu/REC_0.gif" style="z-index: 131; cursor: pointer; left: 310px;
        position: absolute; top: 87px; right: 854px;" language="javascript" onclick="return AggTabDom('6',document.getElementById('ric').style,document.getElementById('dic').style, document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style,document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <img id="i7" src="../p_menu/D_Man.gif" style="z-index: 134; cursor: pointer; left: 377px;
        position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('7',document.getElementById('ric').style,document.getElementById('dic').style, document.getElementById('req').style,document.getElementById('not').style,document.getElementById('all').style,document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <img id="i1_1" src="../p_menu/ALL_0.gif" style="z-index: 132; cursor: pointer; left: 96px;
        position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('8',document.getElementById('ric').style,
                document.getElementById('dic').style,document.getElementById('req').style,document.getElementById('not').style,
                document.getElementById('all').style,document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <img id="i8" src="../p_menu/D9_0.gif" style="z-index: 133; cursor: pointer; left: 461px;
        position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('9',document.getElementById('ric').style,document.getElementById('dic').style,
                document.getElementById('req').style,
                document.getElementById('not').style,document.getElementById('all').style,document.getElementById('docallegati').style,document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <img id="i9" src="../p_menu/D_DEC.gif" style="z-index: 134; cursor: pointer; left: 542px;
        position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('10', document.getElementById('ric').style, document.getElementById('dic').style, 
                document.getElementById('req').style,
                document.getElementById('not').style, document.getElementById('all').style, document.getElementById('docallegati').style, document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <img id="Img1" src="../p_menu/D10_osp.gif" style="z-index: 135; cursor: pointer;
        left: 182px; position: absolute; top: 87px" language="javascript" onclick="return AggTabDom('11', document.getElementById('ric').style, document.getElementById('dic').style,
                document.getElementById('req').style,
                document.getElementById('not').style, document.getElementById('all').style, document.getElementById('docallegati').style, document.getElementById('decisioni').style,document.getElementById('osp').style);" />
    <asp:TextBox ID="txtIndici" runat="server" Style="z-index: 120; left: 16px; position: absolute;
        top: 399px"></asp:TextBox>
    &nbsp;
    <asp:Image ID="imgAvvisoSospesa" runat="server" ImageUrl="../IMG/Alert.gif" Style="z-index: 195;
        left: 15px; position: absolute; top: 470px; height: 17px; visibility: hidden;" />
    <asp:Image ID="imgAlertScadenza" runat="server" ImageUrl="../IMG/Alert.gif" Style="z-index: 195;
        left: 15px; position: absolute; top: 470px; height: 17px;" Visible="False" />
    <asp:Label ID="lblProcessi" runat="server" BackColor="white" BorderStyle="Solid"
        BorderWidth="2px" Font-Bold="True" Font-Names="ARIAL" Font-Size="7pt" ForeColor="#990000"
        Style="z-index: 302; left: 555px; position: absolute; top: 64px; width: 107px;"
        Visible="False"></asp:Label>
    <asp:Label ID="lblSospesa" runat="server" Text="Procedimento SOSPESO!" Font-Names="arial"
        Font-Size="9pt" ForeColor="#0000C0" Style="z-index: 205; left: 34px; position: absolute;
        top: 474px; height: 17px; visibility: hidden;" Font-Bold="True"></asp:Label>
    <asp:Label ID="lblScadenza" runat="server" Text="" Font-Names="arial" Font-Size="9pt"
        ForeColor="#0000C0" Style="z-index: 195; left: 34px; position: absolute; top: 470px;
        height: 17px; width: 611px;" Font-Bold="True" Visible="False"></asp:Label>
    <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 196;
        left: 11px; position: absolute; top: 428px" Text="VISUALIZZA INDICI" Visible="False"
        Width="157px"></asp:Label>
    <asp:Label ID="Label12" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 123;
        left: 173px; cursor: pointer; position: absolute; top: 428px" Text="VISUALIZZA EVENTI"
        Visible="False" Width="109px"></asp:Label>
    <asp:Label ID="Label10" runat="server" Style="z-index: 124; left: 11px; position: absolute;
        top: 468px" Width="639px" Font-Names="arial" Font-Size="8pt" Font-Bold="True"
        Visible="False"></asp:Label>
    <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
        OnClientClick="aa.close();document.getElementById('H1').value=0;ConfermaEsci();"
        Style="z-index: 125; left: 471px; position: absolute; top: 29px" ToolTip="Esci" />
    <asp:Label ID="lblStatoDOM" runat="server" Style="z-index: 124; left: 502px; position: absolute;
        top: 11px; width: 165px; height: 13px;" Font-Names="arial" Font-Size="9pt" Font-Bold="True"
        ForeColor="Red"></asp:Label>
    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
        OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;"
        Style="z-index: 126; left: 8px; position: absolute; top: 29px;" ToolTip="Salva" />
    <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
        Enabled="False" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;ConfermaStampa();"
        Style="z-index: 127; left: 60px; position: absolute; top: 29px; right: 1042px;"
        ToolTip="Elabora e Stampa" EnableViewState="False" />
    <asp:ImageButton ID="imgEventi" runat="server" ImageUrl="../NuoveImm/Img_Eventi.png"
        OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;VisualizzaEventi();return false;"
        Style="z-index: 127; left: 123px; position: absolute; top: 29px; right: 889px;"
        ToolTip="Elenco Eventi" Visible="True" />
    <%--<img src="../NuoveImm/Img_CanoneRegime.png" id="IMGCanone" onclick="CalcoloCanone()"
        style="cursor: pointer; z-index: 136; left: 244px; position: absolute; top: 29px;"
        alt="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008" />--%>
    <asp:ImageButton ID="IMGCanone" runat="server" ImageUrl="../NuoveImm/Img_CanoneRegime.png"
        OnClientClick="CalcoloCanone();return false;" Style="z-index: 136; left: 244px;
        position: absolute; top: 29px;" ToolTip="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008" />
    <asp:Image ID="IMGPREFERENZE" runat="server" ImageUrl="~/NuoveImm/ImgPreferenze.png"
        Style="z-index: 127; left: 259px; cursor: pointer; position: absolute; top: 29px"
        ToolTip="Preferenze Utente" Visible="False" />
    &nbsp; &nbsp;
    <%--    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../NuoveImm/Img_CanoneRegime.png"
        OnClientClick="TempiProcessi();return false;" Style="z-index: 136; left: 559px;
        position: absolute; top: 54px;" ToolTip="Gestione tempistica processi" />
    --%>
    <asp:Label ID="Label5" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 123;
        left: 287px; position: absolute; top: 428px; text-align: center;" Text="DOMANDA IN VERIFICA REQUISITI"
        Visible="False" Width="363px"></asp:Label>
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    <asp:HiddenField ID="H2" runat="server" Value="0" />
    <asp:HiddenField ID="txtTab" runat="server" />
    <asp:HiddenField ID="HiddenID" runat="server" Value="0" />
    <asp:HiddenField ID="conferma" runat="server" Value="0" />
    <asp:HiddenField ID="Ccodicetrovato" runat="server" Value="" />
    <asp:HiddenField ID="Ucodicetrovato" runat="server" Value="" />
    <asp:HiddenField ID="contrattodialer" runat="server" Value="" />
    <asp:HiddenField ID="inlettura" runat="server" Value="0" />
    <asp:HiddenField ID="chiudidirettamente" runat="server" Value="0" />
    <asp:HiddenField ID="ProcediProtoc" runat="server" Value="0" />
    <asp:HiddenField ID="ConfRidCan" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="tipoRichiesta" runat="server" />
    <%--<asp:HiddenField ID="esPositivo" runat="server" Value="0" />
    <asp:HiddenField ID="esNegativo1" runat="server" Value="0" />
    <asp:HiddenField ID="esNegaRiesame" runat="server" Value="0" />--%>
    <%--<asp:HiddenField ID="documMancante" runat="server" Value="0" />--%>
    <asp:HiddenField ID="RinnovoDataChiusura" runat="server" />
    <asp:HiddenField ID="data_riconsegna" runat="server" />
    <asp:HiddenField ID="Hcanone" runat="server" Value="0" />
    <asp:HiddenField ID="ISEEnullo" runat="server" Value="0" />
    <asp:HiddenField ID="nuovocanone" runat="server" Value="0" />
    <asp:HiddenField ID="nuovoCodContr" runat="server" Value="0" />
    <asp:HiddenField ID="pressoCOR" runat="server" />
    <asp:HiddenField ID="importoCanone" runat="server" Value="0" />
    <asp:HiddenField ID="numProvvedim" runat="server" Value="0" />
    <asp:HiddenField ID="dataProvvedim" runat="server" Value="0" />
    <asp:HiddenField ID="restituisciCauz" runat="server" />
    <asp:HiddenField ID="fl_import_registr" runat="server" />
    </form>
</body>
<script language="javascript" type="text/javascript">
    if (document.getElementById('Attesa')) {
        document.getElementById('Attesa').style.visibility = 'hidden';
    }
    document.getElementById('txtIndici').style.visibility = 'hidden';
    aa.close();

    var tipo;
    tipo = document.getElementById('Dom_Dichiara_Cambi1_cmbTipoRichiesta').value;

    if (tipo != '5') {
        document.getElementById('imgDatiContratto').style.visibility = 'hidden';
    }

    if (tipo == '9' && document.getElementById('Dom_Decisioni1_btnRiduzCanone').disabled == false) {
        document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'none';
        document.getElementById('buttonAutorAbus').style.display = 'block';
    }
    if (tipo == '9' && document.getElementById('Dom_Decisioni1_autorizzFinale').value == '1') {
        document.getElementById('buttonAutorAbus').disabled = true;
    }


    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\,]/g
    }

    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }


    function AutoDecimal2(obj) {

        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a != 'NaN') {
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
            else
                document.getElementById(obj.id).value = ''
        }
    }

    function SostPuntVirg(e, obj) {
        var keyPressed;
        keypressed = (window.event) ? event.keyCode : e.which;
        if (keypressed == 46) {
            if (navigator.appName == 'Microsoft Internet Explorer') {
                event.keyCode = 0;
            }
            else {
                e.preventDefault();
            }
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        }

    };

    if (document.getElementById('tipoRichiesta').value == '10') {

        if (document.getElementById('Dom_Decisioni1_txtDataScelta').value != '') {
            document.getElementById('btnDeposito').style.display = 'block';
            document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'none';
        }
        else {
            document.getElementById('Dom_Decisioni1_btnRiduzCanone').style.display = 'block';
            document.getElementById('btnDeposito').style.display = 'none';
        };
    }
    if (document.getElementById('Dom_Decisioni1_txtdataAUTORIZ').value != '') {
        document.getElementById('btnDeposito').disabled = 'disabled';
    };

    visibleMotivazioni();
    visibleOspiti();
    divSospesione();
    visibleDataOsserv();
    //controlloAllegati();
</script>
</html>
