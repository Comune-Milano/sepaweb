
Partial Class GestioneModelli_Segnaposto
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            'vTipo = Request.QueryString("ID")
            'Select Case vTipo
            '    Case "431"
            lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>CONTRATTO</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Locatore</td><td>$locatore$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Indirizzo Locatore</td><td>$indirizzolocatore$</td></tr>")

            '
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Codice F./PIVA Locatore</td><td>$cflocatore$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sede del Locatore</td><td>$sedelocatore$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Direttore di Settore</td><td>$direttore$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data completi Locatore: Sesso, Denominazione, CF/PIVA, Comune, Prov., Indirizzo, Civico</td><td>$datilocatore$</td></tr>")



            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Delibera</td><td>$delibera$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data della Delibera</td><td>$datadelibera$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data Decorrenza</td><td>$decorrenza$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data Scadenza</td><td>$scadenza$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Durata in anni</td><td>$durata$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Mesi entro cui disdettare</td><td>$mesidisdetta$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Giorni entro cui disdettare</td><td>$giornidisdetta$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Anni tacitamente rinnovabili</td><td>$annirinnovo$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Scadenza del tacito rinnovo</td><td>$scadenzarinnovo$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Numero Rate</td><td>$numerorate$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Anticipo</td><td>$anticipo$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data Stipula</td><td>$datastipula$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Canone Iniziale</td><td>$canoneiniziale$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>% ISTAT</td><td>$percistat$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Destinazione Uso</td><td>$destinazioneuso$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Cognome e nome del conduttore</td><td>$nominativoconduttore$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Dati completi Conduttore:Sesso, Cognome, Nome, Comune di Nascita, Prov., Data di nascita, Indirizzo Residenza, Civico, Comune, Prov., Cod. Fiscale</td><td>$daticonduttore$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Piano</td><td>$piano$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Numero Locali</td><td>$numerolocali$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Numero Servizi</td><td>$numeroservizi$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Scala</td><td>$scala$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Interno</td><td>$interno$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sezione</td><td>$sezione$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Foglio</td><td>$foglio$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Particella</td><td>$particella$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sub</td><td>$subalterno$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Categoria Catastale</td><td>$categoria$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Classe Catastale</td><td>$classe$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Rendita Catastale</td><td>$rendita$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Zona Urbana e sub fascia</td><td>$zonafascia$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Superficie Utile</td><td>$suputile$</td></tr>")

            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Elenco Pertinenze</td><td>$pertinenze$</td></tr>")
            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Dati Completi Alloggio: Indirizzo, Civico, Scala, Piano, Interno, Cap, Comune, Prov.</td><td>$datialloggio$</td></tr>")

            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Indirizzo Alloggio</td><td>$indirizzoalloggio$</td></tr>")



            lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Dattaglio superfici</td><td>$dettaglisup$</td></tr>")



            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Protocollo</td><td>$protocollo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Locatore</td><td>$locatore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Codice Fiscale del Locatore</td><td>$cflocatore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sede del Locatore</td><td>$sedelocatore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Direttore di settore</td><td>$direttore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data Delibera</td><td> $datadelibera$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Delibera</td><td> $delibera$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Dati Intestatari</td><td>$DatiIntestatari$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Locatari</td><td>$Locatari$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Indirizzo alloggio</td><td>$indirizzoalloggio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Scala</td><td>$scala$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Piano</td><td>$piano$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Interno</td><td>$interno$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Superficie Utile</td><td>$suputile$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Foglio</td><td>$foglio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Particella </td><td> $particella$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sub</td><td>$subalterno$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Categoria Catastale</td><td>$catcatastale$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Classe Catastale</td><td>$classe$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Rendita</td><td>$rendita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Decorrenza</td><td>$decorrenza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Scadenza</td><td>$scadenza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Durata</td><td>$durata$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Giorni entro cui disdettare</td><td> $giornidisdetta$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Canone Iniziale</td><td> $canoneiniziale$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Anticipo</td><td>$anticipo$</td></tr>")

            lblStampa.Text = lblStampa.Text & ("</table>")
            '    Case "ERP"
            'lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>CONTRATTO ERP</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"

            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Protocollo</td><td> $protocollo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Locatore</td><td> $locatore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>CF Locatore</td><td> $cflocatore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sede Locatore</td><td> $sedelocatore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Direttore</td><td> $direttore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Data Delibera</td><td> $datadelibera$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Delibera</td><td> $delibera$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Dati Intestatari</td><td>$DatiIntestatari$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Locatari</td><td>$Locatari$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Indirizzo alloggio</td><td>$indirizzoalloggio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Scala</td><td> $scala$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Piano</td><td> $piano$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Interno</td><td> $interno$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Superficie Utile</td><td> $suputile$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Foglio</td><td> $foglio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Particella </td><td> $particella$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Sub</td><td> $subalterno$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Categoria Catastale</td><td> $catcatastale$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Classe Catastale</td><td> $classe$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Rendita</td><td> $rendita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Decorrenza</td><td> $decorrenza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Scadenza</td><td> $scadenza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Durata</td><td> $durata$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Giorni entro cui disdettare</td><td> $giornidisdetta$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Canone Iniziale</td><td> $canoneiniziale$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 10pt;text-align: left;'><td>Anticipo</td><td>$anticipo$</td></tr>")

            'lblStampa.Text = lblStampa.Text & ("</table>")
            '    Case "BOL"
            'lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>LETTERA BOLLETTA</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"

            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Mese</td><td> $mese$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Codice Contratto</td><td> $codcontratto$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Nominativo</td><td> $nominativo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo</td><td> $indirizzo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cap e citta</td><td> $capcitta$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Oggetto</td><td> $oggetto$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Testo Lettera</td><td> $testolettera$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Note</td><td> $note$</td></tr>")

            'lblStampa.Text = lblStampa.Text & ("</table>")

            '    Case "CES"
            'lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>DENUNCIA CESSIONE</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"

            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Commissariato</td><td> $commissariato$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Ufficio</td><td> $ufficio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cognome Direttore</td><td> $cognomeDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Nome Direttore</td><td> $nomeDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Data Nascita Direttore</td><td> $nascitaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune Nascita Direttore</td><td> $comunenascitaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Provincia Nascita Direttore</td><td> $provincianascitaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune Residenza Direttore</td><td> $comuneresidenzaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo Direttore</td><td> $indirizzoDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Telefono Direttore</td><td> $telefonoDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Contratto</td><td> $contratto$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cognome</td><td> $cognome$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Nome</td><td> $nome$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Data di Nascita</td><td> $datanascita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune di Nascita</td><td> $comunenascita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Provincia Di Nascita</td><td> $provincianascita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Residenza</td><td> $residenza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cittadinanza</td><td> $cittadinanza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo</td><td> $indirizzo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Telefono</td><td> $telefono$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Documento</td><td> $documento$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Num. Documento</td><td> $numerodocumento$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Data Documento</td><td> $datadocumento$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Autorità</td><td> $autorita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune</td><td> $comune$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Provincia</td><td> $provincia$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo</td><td> $indirizzo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Civico</td><td> $civico$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cap</td><td> $cap$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Piano</td><td> $piano$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Scala</td><td> $scala$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Interno</td><td> $interno$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Vani</td><td> $vani$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Accessori</td><td> $accessori$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Ingressi</td><td> $ingressi$</td></tr>")

            'lblStampa.Text = lblStampa.Text & ("</table>")

            '    Case "FAT"
            'lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>LETTERA FATTURA</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"

            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Mese</td><td> $mese$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cod. Contratto</td><td> $codcontratto$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Nominativo</td><td> $nominativo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo</td><td> $indirizzo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cap e Città</td><td> $capcitta$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Importo</td><td> $importo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Causale</td><td> $causale$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Oggetto</td><td> $oggetto$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Testo Lettera</td><td> $testolettera$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Dettaglio</td><td> $dettaglio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Note</td><td> $note$</td></tr>")

            'lblStampa.Text = lblStampa.Text & ("</table>")


            '    Case "OSP"
            'lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>OSPITALITA</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Elenco Ospiti</td><td> $ospiti$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Commissariato</td><td> $commissariato$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Ufficio</td><td> $ufficio$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cognome Direttore</td><td> $cognomeDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Nome Direttore</td><td> $nomeDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Data Nascita Direttore</td><td> $nascitaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune Nascita Direttore</td><td> $comunenascitaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Provincia Nascita Direttore</td><td> $provincianascitaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune Residenza Direttore</td><td> $comuneresidenzaDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo Direttore</td><td> $indirizzoDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Telefono Direttore</td><td> $telefonoDirettore$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Contratto</td><td> $contratto$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cognome</td><td> $cognome$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Nome</td><td> $nome$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Data di Nascita</td><td> $datanascita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune di Nascita</td><td> $comunenascita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Provincia Di Nascita</td><td> $provincianascita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Residenza</td><td> $residenza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cittadinanza</td><td> $cittadinanza$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo</td><td> $indirizzo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Telefono</td><td> $telefono$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Documento</td><td> $documento$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Num. Documento</td><td> $numerodocumento$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Data Documento</td><td> $datadocumento$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Autorità</td><td> $autorita$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Comune</td><td> $comune$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Provincia</td><td> $provincia$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Indirizzo</td><td> $indirizzo$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Civico</td><td> $civico$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Cap</td><td> $cap$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Piano</td><td> $piano$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Scala</td><td> $scala$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Interno</td><td> $interno$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Vani</td><td> $vani$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Accessori</td><td> $accessori$</td></tr>")
            'lblStampa.Text = lblStampa.Text & ("<tr style='font-size: 8pt;text-align: left;'><td>Ingressi</td><td> $ingressi$</td></tr>")

            'lblStampa.Text = lblStampa.Text & ("</table>")
            '    Case "CNS"
            'lblStampa.Text = "<table border='1'width='50%'><tr style='font-size: 12pt ;text-align: center;font-weight: bold; bgcolor='#CCCCCC'><h3><th colspan= '8'>VERBALE DI CONSEGNA</th><h3></tr><tr style='font-size: 10pt ;text-align: left;font-weight: bold; bgcolor='#CCCCCC'><td>DESCRIZIONE</td><td>SEGNAPOSTO</td></tr>"
            'lblStampa.Text = lblStampa.Text & ("</table>")


            'End Select
        End If

    End Sub
    Public Property vTipo() As String
        Get
            If Not (ViewState("par_vTipologia") Is Nothing) Then
                Return CStr(ViewState("par_vTipologia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vTipologia") = value
        End Set

    End Property

End Class
