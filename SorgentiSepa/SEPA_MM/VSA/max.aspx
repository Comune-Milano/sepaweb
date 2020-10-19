<%@ Register Src="../Dic_DichiarazioneVSA.ascx" TagName="Dic_Dichiarazione" TagPrefix="uc1" %>
<%@ Register Src="../Dic_Patrimonio_VSA.ascx" TagName="Dic_Patrimonio" TagPrefix="uc3" %>
<%@ Register Src="../Dic_Reddito.ascx" TagName="Dic_Reddito" TagPrefix="uc4" %>
<%@ Register Src="../Dic_Sottoscrittore.ascx" TagName="Dic_Sottoscrittore" TagPrefix="uc5" %>
<%@ Register Src="../Dic_Integrazione.ascx" TagName="Dic_Integrazione" TagPrefix="uc6" %>
<%@ Register Src="../Dic_Note_VSA.ascx" TagName="Dic_Note" TagPrefix="uc7" %>
<%@ Register Src="../Dic_Nucleo_VSA.ascx" TagName="Dic_Nucleo" TagPrefix="uc2" %>
<%@ Register Src="../Dic_Reddito_Conv.ascx" TagName="Dic_Reddito_Conv" TagPrefix="uc8" %>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="max.aspx.vb" Inherits="VSA_max" %>

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
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
    <title>SEPA@Web - Dichiarazione</title>
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
            height: 20px;
            font-variant: normal;
        }
        .CssLblValori
        {
            font-size: 8pt;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
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
        #form1
        {
            width: 671px;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function window_onbeforeunload() {
            //alert('ciao');
            if (document.getElementById('H1').value == 1) {
                event.returnValue = "Attenzione...Uscire dalla Dichiarazione utilizzando il pulsante ESCI!! In caso contrario la dichiarazione VERRA' BLOCCATA E NON SARA' PIU' POSSIBILE MODIFICARE!";
            }
        }
    </script>
</head>
<script language="javascript" type="text/javascript" for="window" event="onbeforeunload">

aa.close();

if (document.getElementById('H1').value==1)  {

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



    function ConfermaEsci() {

        if (document.getElementById('txtModificato').value == '1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?\nUSCIRE SENZA SALVARE CAUSERA\' LA PERDITA DELLE MODIFICHE!\nPER NON USCIRE PREMERE IL PULSANTE ANNULLA.");
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

    function ElStampe() { 
    
    window.open('ElencoStampe.aspx?IDDIC='+ <%=lIdDichiarazione %>,'elstmp','');
    
    }

    function cerca() {
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

    function CalcoloCanone() {
        var tipo;
        tipo = document.getElementById('tipoRichiesta').value;

        //document.getElementById('Hcanone').value = '1';
        window.open("Canone.aspx?ID=" + <%= lIdDomanda %> + '&T=' + tipo + '&COD=' + document.getElementById('id_contr').value + '&IDUNITA=' + document.getElementById('codUI').value, "", "top=0,left=0,width=550,height=600,resizable=yes,menubar=yes,toolbar=no,scrollbars=yes");

    }


        // ********** DOCUMENTI PER AMPLIAMENTO **********
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=DocMancanteAMPL', '','');
                }
            
            return true;
        }

        
        //**** Autocertificazione nucleo di famiglia ****
        function AUcertStFamiglia() {
            if (document.getElementById('txtModificato').value != 1){
            window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=StFamigliaAMPL', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=MoreUxorioAMPL', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AssistenzaAMPL', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
            

		}


        //**** Avvio Procedimento ****
        function AvvProcedim() {
        if (document.getElementById('Dic_Note1_documAlleg').value == 1) { 
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AvvioProcAMPL', '', '');
                }
                  
            return true;
        }

        // **** Permanenza requisiti ERP (titolare) ****
        function PermReqERP() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=PermanenzaAMPL1', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=PermanenzaAMPL2', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=SoprallAMPL', '');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=ComSopralAMPL', '', '');
                }
                
            return true;
        }
        // *************** FINE DOCUMENTI PER AMPLIAMENTO

        
        // *************** DOCUMENTI PER REVISIONE CANONE
        //##################### STAMPA RICEZIONE RICHIESTA #####################
        function RicezRichiesta() {
        document.getElementById('H1').value='0';
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                                 document.getElementById('codUI').value +
                                 '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=RichRC','');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=DocMancRC', '', '');
                }
            
            return true;
        }
        //#####################FINE STAMPA DOCUMENTO MANCANTE #####################
        //*************************************************************************************************************************************************
        //#####################STAMPA AVVIO PROCEDIMENTO #####################
        function AvvioProc() {
        if (document.getElementById('Dic_Note1_documAlleg').value == 1) { 
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AvvProcRC', '', '');
                }
                  
            return true;
        }
        //#####################FINE STAMPA PROCEDIMENTO #####################
        //***********************************************************************************************************************************************
        //#####################STAMPA AUTOCERTIFICAZIONE #####################

        function AutoCert() {
            if (document.getElementById('txtModificato').value != 1){
            window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AutoCertRC', '');
            }
            else
            {
                alert('Salvare le modifiche prima di procedere!');
            }
         }
        //#####################FINE STAMPA AUTOCERTIFICAZIONE #####################


        // *************** DOCUMENTI PER VOLTURA *******************
        //***** Modulo richiesta Voltura ******
        function RichVoltura() {
        
          if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=RicezRicVOL', '');
          }
          else
          {
                    alert('Salvare le modifiche prima di procedere!');
          }

        }


     //***** Modulo richiesta Voltura *****
    function AvvProcVoltura() {
                 if (document.getElementById('Dic_Note1_documAlleg').value == 1) { 

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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AvvProcedVOL', '', '');
                }
                  
            return true;
        }

     
     //***** Sopralluogo *****
     function ModSoprall() {
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=ModuloSoprallVOL', '');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=SoprUtenteVOL', '', '');
                }
                
            return true;
        }




      // ********* DOCUMENTI PER SUBENTRO ***********
      //#####################STAMPA AVVIO PROCEDIMENTO #####################
     function AvvioProcSUB() {
            if (document.getElementById('Dic_Note1_documAlleg').value == 1){
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AvvioProcSUB', '', '');
                }
                  
            return true;
        }
        //#####################FINE STAMPA PROCEDIMENTO #####################



     //**** Domanda di subentro ****
     function DomSubentro() {
     
                 if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=DomandaSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=PermReqRSUB', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=SoprallSUB', '');
                }
                else
                {
                    alert('Salvare le modifiche prima di procedere!');
                }
     }


     //**** Comunicazione Doc. Mancante ****
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=DocMancanteSUB', '', '');
                }
                
            return true;
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=ComSopralSUB', '', '');
                }
                
            return true;
        }


        // ************ DOMANDA DI OSPITALITA' **************

        //***** Modulo richiesta Ospitalità ******
        function RichOSP() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=RichOSP', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=RichOSPbada', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=RichOSPscol', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=StFamigliaOSP', '');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=DocMancanteOSP', '','');
                }
            
            return true;
        }

        
        //**** Avvio Procedimento ****
        function AvvioProcOSP() {
        if (document.getElementById('Dic_Note1_documAlleg').value == 1) { 

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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=AvvioProcOSP', '', '');
                }
                  
            return true;
        }


        //***** Sopralluogo *****
        function SopralluogoOSP() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=SopralluogoOSP', '');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&TIPO=ComSoprallOSP', '', '');
                }
                
            return true;
        }


        // ************* DOMANDA DI CAMBIO CONSENSUALE ***********

        //**** Ricezione Richiesta ****
       function RichCAMB() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&NUMCONT2='+ document.getElementById('codcontr2').value +'&TIPO=RichCAMB', '');
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
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&NUMCONT2='+ document.getElementById('codcontr2').value +'&TIPO=DichPermanenza', '');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&NUMCONT2='+ document.getElementById('codcontr2').value +'&TIPO=DocMancCAMB', '','');
                }
            
            return true;
        }


        //**** Avvio Procedimento ****
        function AvvioProcCAMB() {
        if (document.getElementById('Dic_Note1_documAlleg').value == 1) { 

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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&NUMCONT2='+ document.getElementById('codcontr2').value +'&TIPO=AvvProcedCAMB', '', '');
                }
                  
            return true;
        }

        //***** Sopralluogo *****
        function SopralluogoCAMB() {
            if (document.getElementById('txtModificato').value != 1){
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' +
                       document.getElementById('codUI').value +
                       '&NUMCONT=' + document.getElementById('id_contr').value +'&NUMCONT2='+ document.getElementById('codcontr2').value +'&TIPO=SoprallCAMB', '');
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
                    window.open('StampeDoc.aspx?IDDICHIARAZ=' + <%=lIdDichiarazione %>+'&CODUNITA=' + document.getElementById('codUI').value +'&PROT='+ f.txtprot +'&NUMCONT=' + document.getElementById('id_contr').value +'&NUMCONT2='+ document.getElementById('codcontr2').value +'&TIPO=ComSoprallCAMB', '', '');
                }
                
            return true;
        }


</script>
<body onload="return AggTabDic(document.getElementById('txtTab').value,document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
    bgcolor="#f2f5f1">
    <script type="text/javascript">
        document.onkeydown = $onkeydown;

    </script>
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
    &nbsp;<br />
    <br />
    <br />
    <img id="i1" language="javascript" onclick="return AggTabDic('1',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D1_0.gif" style="z-index: 125; left: 10px; cursor: pointer; position: absolute;
        top: 86px" />
    <img id="i2" language="javascript" onclick="return AggTabDic('2',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D2_0.gif" style="z-index: 124; left: 105px; cursor: pointer; position: absolute;
        top: 86px" />
    <img id="i3" language="javascript" onclick="return AggTabDic('3',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D3_0.gif" style="z-index: 123; left: 159px; cursor: pointer; position: absolute;
        top: 86px" />
    <img id="i4" language="javascript" onclick="return AggTabDic('4',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D4_0.gif" style="z-index: 122; left: 238px; cursor: pointer; position: absolute;
        top: 86px" />
    <img id="i5" language="javascript" onclick="return AggTabDic('5',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D5_0.gif" style="z-index: 121; left: 297px; cursor: pointer; position: absolute;
        top: 86px" />
    <img id="i6" language="javascript" onclick="return AggTabDic('6',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D6_0.gif" style="z-index: 120; left: 407px; cursor: pointer; position: absolute;
        top: 86px" />
    <img id="i8" language="javascript" onclick="return AggTabDic('8',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/ReddConv_0.gif" style="z-index: 126; left: 487px; cursor: pointer;
        position: absolute; top: 86px" />
    <img id="i7" language="javascript" onclick="return AggTabDic('7',document.getElementById('dic').style,document.getElementById('nuc').style,document.getElementById('pat').style,document.getElementById('red').style,document.getElementById('sot').style,document.getElementById('int').style,document.getElementById('not').style,document.getElementById('redC').style);"
        src="../p_menu/D_Man.gif" style="z-index: 128; left: 569px; cursor: pointer;
        position: absolute; top: 86px" />
    <form id="form1" runat="server">
    &nbsp;
    <asp:ImageButton ID="imgVaiDomanda" runat="server" ImageUrl="~/NuoveImm/Img_PassaDomRed.png"
        OnClientClick="document.getElementById('H1').value=0;" Style="z-index: 125; left: 388px;
        position: absolute; top: 21px; right: 672px;" ToolTip="Vai alla Domanda" Visible="False" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <table onmousedown="Uscita=1;document.getElementById('H1').value='0';" onmouseout="document.getElementById('H1').value='1';"
        style="width: 68px; position: absolute; top: 21px; left: 238px; z-index: 800;">
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
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere1.jpg);
        width: 674px; position: absolute; top: 0px">
        <tr>
            <td style="width: 670px; text-align: right">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong></strong>
                </span>
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Dichiarazione&nbsp;</strong></span><br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <%--                <atlas:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
                </atlas:ScriptManager>
                --%>
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
                <asp:CheckBox ID="ChFO" runat="server" Style="position: absolute; top: 426px; left: 168px;"
                    Font-Bold="True" Font-Names="arial" Font-Size="8pt" Text="TRATTASI DI DICHIARAZIONE PER FORZE DELL'ORDINE" />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
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
    &nbsp;<br />
    <%--<atlas:UpdatePanel ID="up" runat="server" Mode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div>
                &nbsp;<uc1:Dic_Dichiarazione ID="Dic_Dichiarazione1" runat="server" Visible="true" />
            </div>
        </ContentTemplate>
    </atlas:UpdatePanel>--%>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="giaAccesso" runat="server" Value="0" />
    <input id="Hidden1" type="hidden" />
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        ForeColor="#0000C0" Style="z-index: 100; left: 496px; position: absolute; top: 428px"
        Text="Aggiornare i redditi al 2006!!" Visible="False" Width="157px"></asp:Label>
    <asp:Label ID="lblAvviso" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
        ForeColor="#0000C0" Style="z-index: 100; left: 43px; position: absolute; top: 450px;
        width: 602px;" Text="Attenzione, in caso di domanda di RIDUZIONE CANONE inserire i redditi dell'anno corrente, anche se presunti!"></asp:Label>
    <asp:Image ID="Image3" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 126;
        left: 477px; position: absolute; top: 427px" Visible="False" />
    <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="8pt"
        ForeColor="White" Height="18px" Style="z-index: 320px; left: 302px; position: absolute;
        top: 503px" Width="301px" BackColor="#CC0000">SPECIFICARE L'ANNO DI RIFERIMENTO REDDITUALE</asp:Label>
    <asp:DropDownList ID="cmbAnnoReddituale" runat="server" Font-Names="arial" Font-Size="8pt"
        ForeColor="Black" Style="z-index: 330; left: 557px; position: absolute; top: 502px"
        Width="55px" Height="20px" AutoPostBack="True" 
        ToolTip="Anno di riferimento reddituale">
        <%--        <asp:ListItem>2007</asp:ListItem>
        <asp:ListItem>2008</asp:ListItem>
        <asp:ListItem>2009</asp:ListItem>
        <asp:ListItem>2010</asp:ListItem>
        --%>
    </asp:DropDownList>
    <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 126;
        left: 18px; position: absolute; top: 448px" Visible="true" />
    <asp:Label ID="LBLENTE" runat="server" BackColor="#C0FFC0" BorderStyle="Solid" BorderWidth="1px"
        Font-Bold="True" Font-Names="ARIAL" Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 102;
        left: 11px; position: absolute; top: 427px" Text="VISUALIZZA INDICI" ToolTip="Ente che ha inserito la domanda"
        Visible="False" Width="157px"></asp:Label>
    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp;
    <br />
    <%--        <asp:ListItem>2007</asp:ListItem>
        <asp:ListItem>2008</asp:ListItem>
        <asp:ListItem>2009</asp:ListItem>
        <asp:ListItem>2010</asp:ListItem>
    --%>
    <uc1:Dic_Dichiarazione ID="Dic_Dichiarazione1" runat="server" Visible="true" />
    <uc2:Dic_Nucleo ID="Dic_Nucleo1" runat="server" Visible="true" />
    <uc3:Dic_Patrimonio ID="Dic_Patrimonio1" runat="server" Visible="true" />
    <uc4:Dic_Reddito ID="Dic_Reddito1" runat="server" Visible="true" />
    <uc5:Dic_Sottoscrittore ID="Dic_Sottoscrittore1" runat="server" Visible="true" />
    <uc6:Dic_Integrazione ID="Dic_Integrazione1" runat="server" Visible="true" />
    <uc7:Dic_Note ID="Dic_Note1" runat="server" Visible="true" />
    <uc8:Dic_Reddito_Conv ID="Dic_Reddito_Conv1" runat="server" Visible="true" />
    <asp:HiddenField ID="txtbinserito" runat="server" Value="0" />
    <asp:HiddenField ID="H2" runat="server" Value="0" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="senzaEsci" runat="server" Value="0" />
    <asp:DropDownList ID="cmbComp" runat="server" Style="z-index: 106; left: 147px; position: absolute;
        top: 397px" Width="236px" Visible="False">
    </asp:DropDownList>
    <asp:HiddenField ID="txtTab" runat="server" />
    <asp:HiddenField ID="id_contr" runat="server" />
    <asp:HiddenField ID="codUI" runat="server" />
    <asp:HiddenField ID="tipoRichiesta" runat="server" />
    <asp:HiddenField ID="tipoCausale" runat="server" />
    <asp:HiddenField ID="codcontr2" runat="server" />
    &nbsp;&nbsp; &nbsp;&nbsp;
    <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
        OnClientClick="document.getElementById('H1').value=0;ConfermaEsci();" Style="z-index: 125;
        left: 512px; position: absolute; top: 29px" ToolTip="Esci" />
    <asp:ImageButton ID="btnVisualizzaDich" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
        Style="z-index: 100; left: 71px; position: absolute; top: 569px;" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;" />
    <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
        Style="z-index: 100; left: 7px; position: absolute; top: 29px;" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;"
        ValidationGroup="Salva" />
    <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
        Style="z-index: 100; left: 48px; position: absolute; top: 29px; right: 1082px;"
        Enabled="False" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;"
        ToolTip="Stampa" />
    <asp:ImageButton ID="btnApplica" runat="server" ImageUrl="~/NuoveImm/Img_Elabora_Piccolo.png"
        Style="z-index: 100; left: 94px; position: absolute; top: 29px; right: 1036px;"
        Enabled="False" OnClientClick="document.getElementById('H2').value=document.getElementById('H1').value;document.getElementById('H1').value=0;"
        ToolTip="Elabora dichiarazione" Visible="False" />
    <asp:Image ID="imgAnagrafe" runat="server" ImageUrl="~/NuoveImm/Img_Anagrafe.png"
        Style="z-index: 127; left: 336px; cursor: pointer; position: absolute; top: 29px"
        ToolTip="Anagrafe della popolazione" />
    <asp:ImageButton ID="IMGCanone" runat="server" ImageUrl="../NuoveImm/Img_CanoneRegime.png"
        OnClientClick="CalcoloCanone();return false;" Style="z-index: 136; left: 144px;
        position: absolute; top: 29px;" ToolTip="Calcolo del canone a regime secondo L.R. 27/07 e L.R. 36/2008" />
    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 100;
        left: 16px; position: absolute; top: 467px" Width="284px"></asp:Label>
    <asp:Label ID="lblElaborare" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Red"
        Style="z-index: 101; left: 9px; position: absolute; top: 9px; width: 274px;"
        Font-Bold="True"></asp:Label>
    
    <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 108; left: 191px; position: absolute; top: 62px" 
        Width="32px">Data</asp:Label>
    <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 109; left: 6px; position: absolute; top: 62px" Width="34px">Dic. N.</asp:Label>
    <asp:Label ID="lblPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080" BorderStyle="Solid"
        BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 110; left: 42px; position: absolute;
        top: 62px" Width="66px">0000000000</asp:Label>
        <asp:Label ID="lblSlash" runat="server" Text="/" 
        Style="z-index: 111; left: 112px; position: absolute; top: 63px" 
        CssClass="CssLblValori" Visible="False"></asp:Label>
        <asp:Label ID="lblPGcoll" Style="z-index: 111; left: 117px; position: absolute; top: 62px"
        runat="server" Width="66px" BackColor="Cornsilk" BorderWidth="1px" BorderStyle="Solid"
        BorderColor="#FFC080" CssClass="CssLblValori" Visible="False">0000000000</asp:Label>
    <asp:Label ID="lblDomAssociata" runat="server" BackColor="PaleTurquoise" BorderColor="#FFFFC0"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 111;
        left: 363px; position: absolute; top: 61px" Width="66px"></asp:Label>
    &nbsp;&nbsp;
    <%--<asp:Label ID="lblSPG" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 112;
        left: 45px; position: absolute; top: 62px" Width="26px">06-1</asp:Label>--%>
    <%--<asp:Label ID="Label7" runat="server" BackColor="Cornsilk" BorderColor="#FFC080"
        BorderStyle="Solid" BorderWidth="1px" CssClass="CssLblValori" Style="z-index: 113;
        left: 142px; position: absolute; top: 62px" Width="31px">F205</asp:Label>--%>
    <asp:DropDownList ID="cmbStato" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
        Font-Size="8pt" ForeColor="#0000C0" Style="z-index: 114; left: 481px; position: absolute;
        top: 59px" Width="166px">
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 115; left: 439px; position: absolute; top: 61px" Width="44px">STATO</asp:Label>
    <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Bold="True" Height="18px"
        Style="z-index: 116; left: 294px; position: absolute; top: 62px" 
        Width="105px">Domanda PG</asp:Label>
    <asp:TextBox ID="txtDataPG" runat="server" Columns="10" CssClass="CssNuovoMaiuscolo"
        MaxLength="10" Style="z-index: 117; left: 220px; position: absolute; top: 61px"
        Width="63px"></asp:TextBox>
    <script type = "text/jscript" language = "javascript" >

     if (document.getElementById('giaAccesso').value == 0) {

         var op = window.opener
         if (typeof op != 'undefined') {
             window.opener = window.opener.opener
             if (op.name.substring(0, 13) == 'Nuova_domanda') {
                 op.self.close();
             }
             document.getElementById('giaAccesso').value = 1
         }
      }
        
    </script>

    </form>
    <script type="text/javascript">

        document.getElementById('txtbinserito').style.visibilty = 'hidden';
        //document.getElementById('attendi').style.visibilty='hidden';
        //aa.close();
        //-->
        
    </script>
</body>
<script type="text/xml-script">
        <page xmlns:script="http://schemas.microsoft.com/xml-script/2005">
            <references>
            </references>
            <components>
            </components>
        </page>
</script>
<script type="text/javascript">
    if (document.getElementById('Attesa')) {
        document.getElementById('Attesa').style.visibility = 'hidden';
    }
    aa.close();

</script>
</html>
