
Partial Class AreaPrivata
    Inherits PageSetIdMode
    Public psNews As String
    Dim par As New CM.Global


    Public Modulo1 As String = ""
    Public Modulo2 As String = ""
    Public Modulo3 As String = ""
    Public Modulo4 As String = ""
    Public Modulo5 As String = ""
    Public Modulo6 As String = ""
    Public Modulo7 As String = ""
    Public Modulo8 As String = ""
    Public Modulo9 As String = ""
    Public Modulo10 As String = ""
    Public Modulo11 As String = ""
    Public Modulo12 As String = ""
    Public Modulo13 As String = ""

    'condomini
    Public SModulo14 As String = ""
    Public Modulo14 As String = ""

    'ciclo passivo
    Public SModulo15 As String = ""
    Public Modulo15 As String = ""

    'Agenda e Segnalazioni
    Public SModulo16 As String = ""
    Public Modulo16 As String = ""

    Public SModulo17 As String = ""
    Public Modulo17 As String = ""

    Public SModulo1 As String = ""
    Public SModulo2 As String = ""
    Public SModulo3 As String = ""
    Public SModulo4 As String = ""
    Public SModulo5 As String = ""
    Public SModulo6 As String = ""
    Public SModulo7 As String = ""
    Public SModulo8 As String = ""
    Public SModulo9 As String = ""
    Public SModulo10 As String = ""
    Public SModulo11 As String = ""
    Public SModulo12 As String = ""
    Public SModulo13 As String = ""

    'Gestione Morosità
    Public SModulo18 As String = ""
    Public Modulo18 As String = ""

    'Gestione Satisfaction
    Public SModulo19 As String = ""
    Public Modulo19 As String = ""

    Public SModulo20 As String = ""
    Public Modulo20 As String = ""
    Public Modulo21 As String = ""

    Public SModulo21 As String = ""

    Public SModulo22 As String = ""
    Public Modulo22 As String = ""
    'SIRAPER
    Public SModulo23 As String = ""
    Public Modulo23 As String = ""

    'SICUREZZA
    Public SModulo24 As String = ""
    Public Modulo24 As String = ""

    'FORNITORI
    Public SModulo25 As String = ""
    Public Modulo25 As String = ""
    'ARPA LOMBARDIA
    Public Modulo26 As String = ""
    Public SModulo26 As String = ""
    'NUOVO CICLO PASSIVO
    Public SModulo27 As String = ""
    Public Modulo27 As String = ""

    'SPESE REVERSIBILI
    Public Modulo28 As String = ""
    Public SModulo28 As String = ""

    Public AltezzaColonna1 As Integer = 0
    Public AltezzaColonna2 As Integer = 0
    Public AltezzaColonna3 As Integer = 0






    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("AccessoNegato.htm", True)
                Exit Sub
            End If
            Try
                Dim i As Integer = 0
                Dim L As Integer = 1

                Modulo1 = "<h1>Bando ERP</h1>" _
                        & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                        & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_05.jpg" & Chr(34) & " alt=" & Chr(34) & "Bando ERP" & Chr(34) & " /></td><td valign='top'>" _
                        & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo  per l'inserimento e la gestione delle domande di bando ai sensi del  R.R.1/2004 ai fini " _
                        & "dell'assegnazione in locazione degli alloggi di Edilizia Residenziale Pubblica (E.R.P.)</span></p>" _
                        & "</td></tr>" _
                        & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoBando();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                Modulo2 = "<h1>Bando Cambi</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_06.jpg" & Chr(34) & " alt=" & Chr(34) & "Bando Cambi" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label9" & Chr(34) & ">Modulo per l'inserimento e la gestione delle domande di bando per la mobilità abitativa ai sensi del R.R. 1/2004</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoBandoCambi();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo3 = "<h1>F.S.A.</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_06.jpg" & Chr(34) & " alt=" & Chr(34) & "F.S.A." & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "A11" & Chr(34) & ">Modulo per la gestione delle domande di bando F.S.A.</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoFSA();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo4 = "<h1>Anagrafe Utenza</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_08.jpg" & Chr(34) & " alt=" & Chr(34) & "Anagrafe Utenza" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Modulo per l&#39;acquisizione dei dati dell&#39;utenza E.R.P. degli alloggi del Comune di Milano.</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoAnagrafe();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                Modulo5 = "<h1>Abbinamento e Disponib.</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_09.jpg" & Chr(34) & " alt=" & Chr(34) & "Abbinamento e Disponibilità" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Modulo per la gestione delle disponibilità generali e del ciclo di assegnazione degli alloggi di Erp a canone sociale e moderato</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoAbbina();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo6 = "<h1>Cambi Alloggio</h1>" _
                                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_10.jpg" & Chr(34) & " alt=" & Chr(34) & "Gestione Locatari" & Chr(34) & " /></td><td valign='top'>" _
                                & "<p><span>Modulo per la gestione dei cambi in emergenza ex art.22 c.10 RR 1/2004 e Cambi Consensuali</span></p>" _
                                & "</td></tr>" _
                                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoVSA();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo7 = "<h1>Consultazione</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_15.jpg" & Chr(34) & " alt=" & Chr(34) & "Consultazione" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Modulo, per enti convenzionati, per l&#39;accesso ai dati delle domande di bando E.R.P. ai sensi della normativa sulla privacy</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoConsultazione();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo8 = "<h1>Anagrafe del Patrimonio</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_16.jpg" & Chr(34) & " alt=" & Chr(34) & "Anagrafe del Patrimonio" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Modulo per l&#39;interrogazione e la gestione dei dati del patrimonio.</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoCensimento();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                Modulo9 = "<h1>Servizi e Manutenzioni</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_11.jpg" & Chr(34) & " alt=" & Chr(34) & "Servizi e Manutenzioni" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Modulo per la gestione delle manutenzioni, censimento dello stato manutentivo e conservaz. degli immobili, gestione dei servizi e ripartizione degli oneri reversibili.</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoManutenzioni();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo10 = "<h1>Abbinamento Veloce</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_Amministra.jpg" & Chr(34) & " alt=" & Chr(34) & "Abbinamento Veloce" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Abbinamento veloce Usi Diversi, 431, 392, Forze dell'Ordine, Occupanti Abusivi, Cambi ex-Gestore</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoUI();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo11 = "<h1>Rapporti Utenza</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_Contratti.jpg" & Chr(34) & " alt=" & Chr(34) & "Rapporti Utenza" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Modulo per la gestione dei Rapporti con l&#39;Utenza</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoContratti();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo12 = "<h1>Impianti</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_Bollette.jpg" & Chr(34) & " alt=" & Chr(34) & "Impianti" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span>Gestione Impianti.</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoImpianti();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "



                Modulo13 = "<h1>Contabilità</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_cont.jpg" & Chr(34) & " alt=" & Chr(34) & "Contabilità" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione e l'interrogazione della partita contabile dell'inquilinato. " _
                & "</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoContabilita();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Modulo14 = "<h1>Condomini</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_condomini.jpg" & Chr(34) & " alt=" & Chr(34) & "Condomini" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione dei condomini. " _
                & "</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoCondomini();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                'ciclo passivo
                Modulo15 = "<h1>Piani finanziari</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/piani_finanziari.jpg" & Chr(34) & " alt=" & Chr(34) & "Ciclo Passivo" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione del Piano Finanziario</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoCicloPassivo();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                'Agenda e Segnalazioni
                Modulo16 = "<h1>Agenda e Segnalazioni</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_call_center.jpg" & Chr(34) & " alt=" & Chr(34) & "Agenda e Segnalazioni" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione dei contatti</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoGestioneContatti();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                'Autogestioni
                Modulo17 = "<h1>Gestione Autonoma</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_gest_autonoma.jpg" & Chr(34) & " alt=" & Chr(34) & "Gestione Autonoma" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione autonoma degli immobili</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoGestAutonoma();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                'Gestione Morosita
                Modulo18 = "<h1>Gestione Morosità</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_Morosita.jpg" & Chr(34) & " alt=" & Chr(34) & "Gestione Morosità" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione delle morosità</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoMorosita();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                'Gestione Customer Satisfaction
                Modulo19 = "<h1>Customer Satisfaction</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_customer_satisfaction.jpg" & Chr(34) & " alt=" & Chr(34) & "Customer Satisfaction" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Indagine per la valutazione del livello di gradimento di alcuni servizi gestiti per il patrimonio immobiliare del Comune di Milano</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoSatisfaction();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                Modulo20 = "<h1>Stampe Massive</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_St_Massive.jpg" & Chr(34) & " alt=" & Chr(34) & "Stampe Massve" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione e l'acquisizione delle elaborazioni di stampe massive</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoMassive();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "



                'modulo archivio
                Modulo21 = "<h1>Archivio</h1>" _
                & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_St_Massive.jpg" & Chr(34) & " alt=" & Chr(34) & "Archivio" & Chr(34) & " /></td><td valign='top'>" _
                & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per l'indicizzazione dei documenti di archivio</span></p>" _
                & "</td></tr>" _
                & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoArchivio();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "



                Modulo22 = "<h1>Rilievo</h1>" _
               & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
               & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_Rilievo.jpg" & Chr(34) & " alt=" & Chr(34) & "Rilievo" & Chr(34) & " /></td><td valign='top'>" _
               & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la rilevazione dello stato di fatto delle unità immobiliari</span></p>" _
               & "</td></tr>" _
               & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoRilievo();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                'SIRAPER
                Modulo23 = "<h1>Siraper</h1>" _
                         & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_s.jpg" & Chr(34) & " alt=" & Chr(34) & "SIRAPER" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Trasmissione dati SIRAPER</span></p>" _
                         & "</td></tr>" _
                         & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoSiraper();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                'SICUREZZA
                Modulo24 = "<h1>Sicurezza</h1>" _
                         & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/img_modulo_sicurezza.jpg" & Chr(34) & " alt=" & Chr(34) & "SICUREZZA" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Gestione processi di tutela del patrimonio</span></p>" _
                         & "</td></tr>" _
                         & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoSicurezza();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                'FORNITORI
                Modulo25 = "<h1>Fornitori</h1>" _
                         & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_s.jpg" & Chr(34) & " alt=" & Chr(34) & "FORNITORI" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Comunicazione, consultazione e cooperazione tra Ente e Ditte.</span></p>" _
                         & "</td></tr>" _
                         & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoFornitori();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "
                'ARPA
                Modulo26 = "<h1>A.R.P.A. Lombardia</h1>" _
                         & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_s.jpg" & Chr(34) & " alt=" & Chr(34) & "SIRAPER" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Anagrafe Regionale del Patrimonio Abitativo L.R. 16/2016</span></p>" _
                         & "</td></tr>" _
                         & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriArpaLombardia();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "


                'Nuovo ciclo passivo
                Modulo27 = "<h1>Ciclo Passivo</h1>" _
                         & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_ciclo_passivo.jpg" & Chr(34) & " alt=" & Chr(34) & "Ciclo Passivo" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione del Ciclo Passivo " _
                         & "</span></p>" _
                         & "</td></tr>" _
                         & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoNuovoCicloPassivo();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "



                'SPESE REVERSIBILI
                Modulo28 = "<h1>Spese Reversibili</h1>" _
                         & "<div class=" & Chr(34) & "service_box" & Chr(34) & ">" _
                         & "<table width='100%'><tr><td valign='top'><img height=" & Chr(34) & "110" & Chr(34) & " src=" & Chr(34) & "images/sepa_image_spese_reversibili.jpg" & Chr(34) & " alt=" & Chr(34) & "Spese Reversibili" & Chr(34) & " /></td><td valign='top'>" _
                         & "<p><span id=" & Chr(34) & "Label1" & Chr(34) & ">Modulo per la gestione delle spese reversibili. " _
                         & "</span></p>" _
                         & "</td></tr>" _
                         & "<tr><td valign='top'></td><td valign='top' align='right'><span><a href=" & Chr(34) & "javascript:void(0)" & Chr(34) & " onclick=" & Chr(34) & "ApriAccessoSpeseReversibili();" & Chr(34) & ">Accedi al servizio</a></span></td></tr></table></div> "

                Label7.Text = "<a href=""javascript:window.open('VerInfo.aspx','Versione','resizable=no,height=550px,width=800px,top=100,left=100,scrollbars=no');void(0);"">" & "Ver. " & Mid(System.Configuration.ConfigurationManager.AppSettings("Versione"), 10) & "</a>"


                Response.Expires = 0
                psNews = "<script type='text/javascript'>var fcontent=new Array();"

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.cmd = par.OracleConn.CreateCommand()
                End If

                Dim tentativi As Integer = 0
                Dim Inattivita As Integer = 0
                Dim GiorniScadenza As Integer = 0
                Dim MinLunghezza As Integer = 0


                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=64"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    tentativi = par.IfNull(myReader("VALORE"), 0)
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=65"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblInattivita.Text = par.IfNull(myReader("VALORE"), 0)
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=61"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblGiorni.Text = par.IfNull(myReader("VALORE"), 0)
                    lblGiorni1.Text = par.IfNull(myReader("VALORE"), 0)
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=62"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lblLunghezza.Text = par.IfNull(myReader("VALORE"), 0)
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=63"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If myReader("valore") = "1" Then
                        lblAlfa.Text = "e deve contenere obbligatoriamente sia numeri che lettere"
                    Else
                        lblAlfa.Text = ""
                    End If
                End If
                myReader.Close()

                '08/06/2015
                par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=124"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Session.Add("Firme_Responsabili", par.IfNull(myReader("VALORE"), ""))
                    'di default ALLEGATI/FIRME/
                End If
                myReader.Close()
                '----

                lblOperatore.Text = Session.Item("Nome_Operatore")
                i = 0
                par.cmd.CommandText = "select WEB_NEWS_ENTI.* from WEB_REL_NEWS_ENTI,WEB_NEWS_ENTI where WEB_REL_NEWS_ENTI.ID_ENTE=" & Session.Item("ID_CAF") & " and WEB_NEWS_ENTI.ID=WEB_REL_NEWS_ENTI.ID_NEWS  AND WEB_NEWS_ENTI.DATA_V<='" & Format(Now, "yyyyMMdd") & "' and WEB_NEWS_ENTI.DATA_F>='" & Format(Now, "yyyyMMdd") & "' order by data_v desc"
                myReader = par.cmd.ExecuteReader()
                Do While myReader.Read()
                    'psNews = psNews & "fcontent[" & i & "]=" & Chr(34) & "<a href='News.aspx?T=1&ID=" & par.IfNull(myReader("id"), "-1") & "' onclick=" & Chr(34) & "document.getElementById('Scarica').value='0';" & Chr(34) & "><span style='font-size: 14pt; font-family: Arial; color: #982127'>" & Replace(par.IfNull(myReader("messaggio_breve"), ""), vbCrLf, "</br>") & "</span><span style='font-size: 10pt; font-family: Arial; color: #982127'> (" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & ")</span><br><span style='font-size: 10pt; font-family: Arial'>" & Replace(Replace(Mid(par.IfNull(myReader("messaggio_lungo"), ""), 1, 100), "<br/>", " "), vbCrLf, " ") & "...</span></a>" & Chr(34) & ";"
                    psNews = psNews & "fcontent[" & i & "]=" & Chr(34) & "<a href='javascript:void(0)' onclick='ApriNews(" & par.IfNull(myReader("id"), "-1") & ");'><span style='font-size: 14pt; font-family: Arial; color: #982127'>" & Replace(par.IfNull(myReader("messaggio_breve"), ""), vbCrLf, "</br>") & "</span><span style='font-size: 10pt; font-family: Arial; color: #982127'> (" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & ")</span><br><span style='font-size: 10pt; font-family: Arial'>" & Replace(Replace(Mid(par.IfNull(myReader("messaggio_lungo"), ""), 1, 100), "<br/>", " "), vbCrLf, " ") & "...</span></a>" & Chr(34) & ";"
                    i = i + 1
                Loop
                If i = 0 Then
                    psNews = psNews & "fcontent[0]='';"
                    HyperLink10.Visible = False
                End If
                myReader.Close()


                psNews = psNews & "</script>"

                If Session.Item("LIVELLO") = "1" Then
                    hyAmministrazione.Visible = True
                    'HyperLink12.Visible = False
                    HyperLink1.Visible = False
                    HyperLink2.Visible = False
                    par.cmd.CommandText = "select * from OPERATORI where REVOCA='0' AND fl_da_confermare='1' AND (FL_ELIMINATO IS NULL OR FL_ELIMINATO='0')"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        'Response.Write("<script>alert('Attenzione, ci sono degli operatori in attesa di CONFERMA REGISTRAZIONE. Usare il modulo ricerca operatori, utilizzando il campo DA REGISTRARE impostato su SI');</script>")
                        Label11.Text = "CI SONO OPERATORI DA REGISTRARE!!"
                    End If
                    myReader.Close()
                Else

                    'If Session.Item("GEST_OPERATORI") = "1" Then
                    '    hyAmministrazione.Visible = True
                    '    HyperLink1.Text = ""
                    'Else
                    '    hyAmministrazione.Visible = False
                    'End If

                    'ACCESSO A GESTIONE PER SUPER UTENTI CON MENU LIMITATO
                    If (Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1) Or Session.Item("GEST_OPERATORI") = "1" Then
                        hyAmministrazione.Visible = True
                        HyperLink1.Text = ""
                    Else
                        hyAmministrazione.Visible = False
                    End If
                    '################################################################
                End If

                If Session.Item("LIVELLO") <> "1" Then
                    par.cmd.CommandText = "select * from OPERATORI where id=" & Session.Item("ID_OPERATORE")
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then

                        If par.IfNull(myReader("FL_RESPONSABILE_ENTE"), "") = "1" Then
                            HyperLink1.Visible = True
                        Else
                            HyperLink1.Visible = False
                        End If

                        If Session.Item("ID_CAF") <> "6" Then
                            HyperLink2.Visible = True
                        Else
                            HyperLink2.Visible = False
                        End If

                        If par.IfNull(myReader("mod_erp"), "") = "1" Then
                            SModulo1 = Modulo1
                            L = L + 1

                        Else
                        End If

                        If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                            HyperLink2.Visible = False
                            HyperLink2.Text = ""
                        End If

                        If par.IfNull(myReader("mod_cambi"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo2
                                Case 2
                                    SModulo2 = Modulo2
                                Case 3
                                    SModulo3 = Modulo2
                                Case 4
                                    SModulo4 = Modulo2
                                Case 5
                                    SModulo5 = Modulo2
                                Case 6
                                    SModulo6 = Modulo2
                                Case 7
                                    SModulo7 = Modulo2
                                Case 8
                                    SModulo8 = Modulo2
                                Case 9
                                    SModulo9 = Modulo2
                                Case 10
                                    SModulo10 = Modulo2
                                Case 11
                                    SModulo11 = Modulo2
                                Case 12
                                    SModulo12 = Modulo2
                                Case 13
                                    SModulo13 = Modulo2
                                Case 14
                                    SModulo14 = Modulo2
                                Case 15
                                    SModulo15 = Modulo2
                                Case 16
                                    SModulo16 = Modulo2
                                Case 17
                                    SModulo17 = Modulo2
                                Case 18
                                    SModulo18 = Modulo2
                                Case 19
                                    SModulo19 = Modulo2
                                Case 20
                                    SModulo20 = Modulo2
                                Case 21
                                    SModulo21 = Modulo2
                                Case 22
                                    SModulo22 = Modulo2
                                Case 23
                                    SModulo23 = Modulo2
                                Case 24
                                    SModulo24 = Modulo2
                                Case 25
                                    SModulo25 = Modulo2
                                Case 26
                                    SModulo26 = Modulo2
                                Case 27
                                    SModulo27 = Modulo2
                                Case 28
                                    SModulo28 = Modulo2
                            End Select
                            L = L + 1

                        Else
                            'Modulo2 = ""
                            'HyBandoCambi.Visible = False
                        End If

                        If par.IfNull(myReader("mod_fsa"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo3
                                Case 2
                                    SModulo2 = Modulo3
                                Case 3
                                    SModulo3 = Modulo3
                                Case 4
                                    SModulo4 = Modulo3
                                Case 5
                                    SModulo5 = Modulo3
                                Case 6
                                    SModulo6 = Modulo3
                                Case 7
                                    SModulo7 = Modulo3
                                Case 8
                                    SModulo8 = Modulo3
                                Case 9
                                    SModulo9 = Modulo3
                                Case 10
                                    SModulo10 = Modulo3
                                Case 11
                                    SModulo11 = Modulo3
                                Case 12
                                    SModulo12 = Modulo3
                                Case 13
                                    SModulo13 = Modulo3
                                Case 14
                                    SModulo14 = Modulo3
                                Case 15
                                    SModulo15 = Modulo3
                                Case 16
                                    SModulo16 = Modulo3
                                Case 17
                                    SModulo17 = Modulo3
                                Case 18
                                    SModulo18 = Modulo3
                                Case 19
                                    SModulo19 = Modulo3
                                Case 20
                                    SModulo20 = Modulo3
                                Case 21
                                    SModulo21 = Modulo3
                                Case 22
                                    SModulo22 = Modulo3
                                Case 23
                                    SModulo23 = Modulo3
                                Case 24
                                    SModulo24 = Modulo3
                                Case 25
                                    SModulo25 = Modulo3
                                Case 26
                                    SModulo26 = Modulo3
                                Case 27
                                    SModulo27 = Modulo3
                                Case 28
                                    SModulo28 = Modulo3
                            End Select
                            L = L + 1
                        Else
                            'HyBandoFSA.Visible = False
                            'Modulo3 = ""
                        End If

                        If par.IfNull(myReader("mod_au"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo4
                                Case 2
                                    SModulo2 = Modulo4
                                Case 3
                                    SModulo3 = Modulo4
                                Case 4
                                    SModulo4 = Modulo4
                                Case 5
                                    SModulo5 = Modulo4
                                Case 6
                                    SModulo6 = Modulo4
                                Case 7
                                    SModulo7 = Modulo4
                                Case 8
                                    SModulo8 = Modulo4
                                Case 9
                                    SModulo9 = Modulo4
                                Case 10
                                    SModulo10 = Modulo4
                                Case 11
                                    SModulo11 = Modulo4
                                Case 12
                                    SModulo12 = Modulo4
                                Case 13
                                    SModulo13 = Modulo4
                                Case 14
                                    SModulo14 = Modulo4
                                Case 15
                                    SModulo15 = Modulo4
                                Case 16
                                    SModulo16 = Modulo4
                                Case 17
                                    SModulo17 = Modulo4
                                Case 18
                                    SModulo18 = Modulo4
                                Case 19
                                    SModulo19 = Modulo4
                                Case 20
                                    SModulo20 = Modulo4
                                Case 21
                                    SModulo21 = Modulo4
                                Case 22
                                    SModulo22 = Modulo4
                                Case 23
                                    SModulo23 = Modulo4
                                Case 24
                                    SModulo24 = Modulo4
                                Case 25
                                    SModulo25 = Modulo4
                                Case 26
                                    SModulo26 = Modulo4
                                Case 27
                                    SModulo27 = Modulo4
                                Case 28
                                    SModulo28 = Modulo4
                            End Select
                            L = L + 1
                        Else
                            'HyBandoAU.Visible = False
                            'Modulo4 = ""
                        End If

                        If par.IfNull(myReader("FL_ABB_ERP"), "") = "0" Then
                            'HyAbbinamento.Visible = False
                            'Modulo5 = ""
                        End If

                        If par.IfNull(myReader("mod_abb"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo5
                                Case 2
                                    SModulo2 = Modulo5
                                Case 3
                                    SModulo3 = Modulo5
                                Case 4
                                    SModulo4 = Modulo5
                                Case 5
                                    SModulo5 = Modulo5
                                Case 6
                                    SModulo6 = Modulo5
                                Case 7
                                    SModulo7 = Modulo5
                                Case 8
                                    SModulo8 = Modulo5
                                Case 9
                                    SModulo9 = Modulo5
                                Case 10
                                    SModulo10 = Modulo5
                                Case 11
                                    SModulo11 = Modulo5
                                Case 12
                                    SModulo12 = Modulo5
                                Case 13
                                    SModulo13 = Modulo5
                                Case 14
                                    SModulo14 = Modulo5
                                Case 15
                                    SModulo15 = Modulo5
                                Case 16
                                    SModulo16 = Modulo5
                                Case 17
                                    SModulo17 = Modulo5
                                Case 18
                                    SModulo18 = Modulo5
                                Case 19
                                    SModulo19 = Modulo5
                                Case 20
                                    SModulo20 = Modulo5
                                Case 21
                                    SModulo21 = Modulo5
                                Case 22
                                    SModulo22 = Modulo5
                                Case 23
                                    SModulo23 = Modulo5
                                Case 24
                                    SModulo24 = Modulo5
                                Case 25
                                    SModulo25 = Modulo5
                                Case 26
                                    SModulo26 = Modulo5
                                Case 27
                                    SModulo27 = Modulo5
                                Case 28
                                    SModulo28 = Modulo5
                            End Select
                            L = L + 1


                        Else
                            'HyAbbinamento.Visible = False
                            'Modulo5 = ""
                            'Modulo10 = ""
                            'lblUI.Visible = True
                        End If



                        If par.IfNull(myReader("mod_abb_dec"), "") = "1" Or par.IfNull(myReader("MOD_EMRI"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo6
                                Case 2
                                    SModulo2 = Modulo6
                                Case 3
                                    SModulo3 = Modulo6
                                Case 4
                                    SModulo4 = Modulo6
                                Case 5
                                    SModulo5 = Modulo6
                                Case 6
                                    SModulo6 = Modulo6
                                Case 7
                                    SModulo7 = Modulo6
                                Case 8
                                    SModulo8 = Modulo6
                                Case 9
                                    SModulo9 = Modulo6
                                Case 10
                                    SModulo10 = Modulo6
                                Case 11
                                    SModulo11 = Modulo6
                                Case 12
                                    SModulo12 = Modulo6
                                Case 13
                                    SModulo13 = Modulo6
                                Case 14
                                    SModulo14 = Modulo6
                                Case 15
                                    SModulo15 = Modulo6
                                Case 16
                                    SModulo16 = Modulo6
                                Case 17
                                    SModulo17 = Modulo6
                                Case 18
                                    SModulo18 = Modulo6
                                Case 19
                                    SModulo19 = Modulo6
                                Case 20
                                    SModulo20 = Modulo6
                                Case 21
                                    SModulo21 = Modulo6
                                Case 22
                                    SModulo22 = Modulo6
                                Case 23
                                    SModulo23 = Modulo6
                                Case 24
                                    SModulo24 = Modulo6
                                Case 25
                                    SModulo25 = Modulo6
                                Case 26
                                    SModulo26 = Modulo6
                                Case 27
                                    SModulo27 = Modulo6
                                Case 28
                                    SModulo28 = Modulo6
                            End Select
                            L = L + 1
                        Else
                            'HyAbbinamentoDec.Visible = False
                            'Modulo6 = ""
                        End If

                        If par.IfNull(myReader("mod_cons"), "") = "1" Then
                            'HyConsultazione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo7
                                Case 2
                                    SModulo2 = Modulo7
                                Case 3
                                    SModulo3 = Modulo7
                                Case 4
                                    SModulo4 = Modulo7
                                Case 5
                                    SModulo5 = Modulo7
                                Case 6
                                    SModulo6 = Modulo7
                                Case 7
                                    SModulo7 = Modulo7
                                Case 8
                                    SModulo8 = Modulo7
                                Case 9
                                    SModulo9 = Modulo7
                                Case 10
                                    SModulo10 = Modulo7
                                Case 11
                                    SModulo11 = Modulo7
                                Case 12
                                    SModulo12 = Modulo7
                                Case 13
                                    SModulo13 = Modulo7
                                Case 14
                                    SModulo14 = Modulo7
                                Case 15
                                    SModulo15 = Modulo7
                                Case 16
                                    SModulo16 = Modulo7
                                Case 17
                                    SModulo17 = Modulo7
                                Case 18
                                    SModulo18 = Modulo7
                                Case 19
                                    SModulo19 = Modulo7
                                Case 20
                                    SModulo20 = Modulo7
                                Case 21
                                    SModulo21 = Modulo7
                                Case 22
                                    SModulo22 = Modulo7
                                Case 23
                                    SModulo23 = Modulo7
                                Case 24
                                    SModulo24 = Modulo7
                                Case 25
                                    SModulo25 = Modulo7
                                Case 26
                                    SModulo26 = Modulo7
                                Case 27
                                    SModulo27 = Modulo7
                                Case 28
                                    SModulo28 = Modulo7
                            End Select
                            L = L + 1
                        Else
                            'HyConsultazione.Visible = False
                            'Modulo7 = ""
                        End If

                        'If myReader("mod_ped") = "1" Then
                        '    HyPed.Visible = True
                        'Else
                        '    HyPed.Visible = False
                        'End If




                        If par.IfNull(myReader("mod_ped2"), "") = "1" Then
                            'lblPED2.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo8
                                Case 2
                                    SModulo2 = Modulo8
                                Case 3
                                    SModulo3 = Modulo8
                                Case 4
                                    SModulo4 = Modulo8
                                Case 5
                                    SModulo5 = Modulo8
                                Case 6
                                    SModulo6 = Modulo8
                                Case 7
                                    SModulo7 = Modulo8
                                Case 8
                                    SModulo8 = Modulo8
                                Case 9
                                    SModulo9 = Modulo8
                                Case 10
                                    SModulo10 = Modulo8
                                Case 11
                                    SModulo11 = Modulo8
                                Case 12
                                    SModulo12 = Modulo8
                                Case 13
                                    SModulo13 = Modulo8
                                Case 14
                                    SModulo14 = Modulo8
                                Case 15
                                    SModulo15 = Modulo8
                                Case 16
                                    SModulo16 = Modulo8
                                Case 17
                                    SModulo17 = Modulo8
                                Case 18
                                    SModulo18 = Modulo8
                                Case 19
                                    SModulo19 = Modulo8
                                Case 20
                                    SModulo20 = Modulo8
                                Case 21
                                    SModulo21 = Modulo8
                                Case 22
                                    SModulo22 = Modulo8
                                Case 23
                                    SModulo23 = Modulo8
                                Case 24
                                    SModulo24 = Modulo8
                                Case 25
                                    SModulo25 = Modulo8
                                Case 26
                                    SModulo26 = Modulo8
                                Case 27
                                    SModulo27 = Modulo8
                                Case 28
                                    SModulo28 = Modulo8

                            End Select
                            L = L + 1
                        Else
                            'lblPED2.Visible = False
                            'Modulo8 = ""
                        End If

                        If par.IfNull(myReader("mod_manutenzioni"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo9
                                Case 2
                                    SModulo2 = Modulo9
                                Case 3
                                    SModulo3 = Modulo9
                                Case 4
                                    SModulo4 = Modulo9
                                Case 5
                                    SModulo5 = Modulo9
                                Case 6
                                    SModulo6 = Modulo9
                                Case 7
                                    SModulo7 = Modulo9
                                Case 8
                                    SModulo8 = Modulo9
                                Case 9
                                    SModulo9 = Modulo9
                                Case 10
                                    SModulo10 = Modulo9
                                Case 11
                                    SModulo11 = Modulo9
                                Case 12
                                    SModulo12 = Modulo9
                                Case 13
                                    SModulo13 = Modulo9
                                Case 14
                                    SModulo14 = Modulo9
                                Case 15
                                    SModulo15 = Modulo9
                                Case 16
                                    SModulo16 = Modulo9
                                Case 17
                                    SModulo17 = Modulo9
                                Case 18
                                    SModulo18 = Modulo9
                                Case 19
                                    SModulo19 = Modulo9
                                Case 20
                                    SModulo20 = Modulo9
                                Case 21
                                    SModulo21 = Modulo9
                                Case 22
                                    SModulo22 = Modulo9
                                Case 23
                                    SModulo23 = Modulo9
                                Case 24
                                    SModulo24 = Modulo9
                                Case 25
                                    SModulo25 = Modulo9
                                Case 26
                                    SModulo26 = Modulo9
                                Case 27
                                    SModulo27 = Modulo9
                                Case 28
                                    SModulo28 = Modulo9

                            End Select
                            L = L + 1
                        Else
                            'HyManutenzione.Visible = False
                            'Modulo9 = ""
                        End If

                        If par.IfNull(myReader("mod_abb"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo10
                                Case 2
                                    SModulo2 = Modulo10
                                Case 3
                                    SModulo3 = Modulo10
                                Case 4
                                    SModulo4 = Modulo10
                                Case 5
                                    SModulo5 = Modulo10
                                Case 6
                                    SModulo6 = Modulo10
                                Case 7
                                    SModulo7 = Modulo10
                                Case 8
                                    SModulo8 = Modulo10
                                Case 9
                                    SModulo9 = Modulo10
                                Case 10
                                    SModulo10 = Modulo10
                                Case 11
                                    SModulo11 = Modulo10
                                Case 12
                                    SModulo12 = Modulo10
                                Case 13
                                    SModulo13 = Modulo10
                                Case 14
                                    SModulo14 = Modulo10
                                Case 15
                                    SModulo15 = Modulo10
                                Case 16
                                    SModulo16 = Modulo10
                                Case 17
                                    SModulo17 = Modulo10
                                Case 18
                                    SModulo18 = Modulo10
                                Case 19
                                    SModulo19 = Modulo10
                                Case 20
                                    SModulo20 = Modulo10
                                Case 21
                                    SModulo21 = Modulo10
                                Case 22
                                    SModulo22 = Modulo10
                                Case 23
                                    SModulo23 = Modulo10
                                Case 24
                                    SModulo24 = Modulo10
                                Case 25
                                    SModulo25 = Modulo10
                                Case 26
                                    SModulo26 = Modulo10
                                Case 27
                                    SModulo27 = Modulo10
                                Case 28
                                    SModulo28 = Modulo10

                            End Select
                            L = L + 1
                        Else
                            'HyAbbinamento.Visible = False
                            'Modulo5 = ""
                            'Modulo10 = ""
                            'lblUI.Visible = True
                        End If

                        'If myReader("mod_bollette") = "1" Then
                        '    'lblImpianti.Visible = True
                        'Else
                        '    'lblImpianti.Visible = False
                        '    Modulo12 = ""
                        'End If

                        If par.IfNull(myReader("mod_contratti"), "") = "1" Then
                            'lblContratti.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo11
                                Case 2
                                    SModulo2 = Modulo11
                                Case 3
                                    SModulo3 = Modulo11
                                Case 4
                                    SModulo4 = Modulo11
                                Case 5
                                    SModulo5 = Modulo11
                                Case 6
                                    SModulo6 = Modulo11
                                Case 7
                                    SModulo7 = Modulo11
                                Case 8
                                    SModulo8 = Modulo11
                                Case 9
                                    SModulo9 = Modulo11
                                Case 10
                                    SModulo10 = Modulo11
                                Case 11
                                    SModulo11 = Modulo11
                                Case 12
                                    SModulo12 = Modulo11
                                Case 13
                                    SModulo13 = Modulo11
                                Case 14
                                    SModulo14 = Modulo11
                                Case 15
                                    SModulo15 = Modulo11
                                Case 16
                                    SModulo16 = Modulo11
                                Case 17
                                    SModulo17 = Modulo11
                                Case 18
                                    SModulo18 = Modulo11
                                Case 19
                                    SModulo19 = Modulo11
                                Case 20
                                    SModulo20 = Modulo11
                                Case 21
                                    SModulo21 = Modulo11
                                Case 22
                                    SModulo22 = Modulo11
                                Case 23
                                    SModulo23 = Modulo11
                                Case 24
                                    SModulo24 = Modulo11
                                Case 25
                                    SModulo25 = Modulo11
                                Case 26
                                    SModulo26 = Modulo11
                                Case 27
                                    SModulo27 = Modulo11
                                Case 28
                                    SModulo28 = Modulo11

                            End Select
                            L = L + 1
                        Else
                            'lblContratti.Visible = False
                            'Modulo11 = ""

                        End If



                        If par.IfNull(myReader("mod_demanio"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo12
                                Case 2
                                    SModulo2 = Modulo12
                                Case 3
                                    SModulo3 = Modulo12
                                Case 4
                                    SModulo4 = Modulo12
                                Case 5
                                    SModulo5 = Modulo12
                                Case 6
                                    SModulo6 = Modulo12
                                Case 7
                                    SModulo7 = Modulo12
                                Case 8
                                    SModulo8 = Modulo12
                                Case 9
                                    SModulo9 = Modulo12
                                Case 10
                                    SModulo10 = Modulo12
                                Case 11
                                    SModulo11 = Modulo12
                                Case 12
                                    SModulo12 = Modulo12
                                Case 13
                                    SModulo13 = Modulo12
                                Case 14
                                    SModulo14 = Modulo12
                                Case 15
                                    SModulo15 = Modulo12
                                Case 16
                                    SModulo16 = Modulo12
                                Case 17
                                    SModulo17 = Modulo12
                                Case 18
                                    SModulo18 = Modulo12
                                Case 19
                                    SModulo19 = Modulo12
                                Case 20
                                    SModulo20 = Modulo12
                                Case 21
                                    SModulo21 = Modulo12
                                Case 22
                                    SModulo22 = Modulo12
                                Case 23
                                    SModulo23 = Modulo12
                                Case 24
                                    SModulo24 = Modulo12
                                Case 25
                                    SModulo25 = Modulo12
                                Case 26
                                    SModulo26 = Modulo12
                                Case 27
                                    SModulo27 = Modulo12
                                Case 28
                                    SModulo28 = Modulo12

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If


                        If par.IfNull(myReader("mod_contabile"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo13
                                Case 2
                                    SModulo2 = Modulo13
                                Case 3
                                    SModulo3 = Modulo13
                                Case 4
                                    SModulo4 = Modulo13
                                Case 5
                                    SModulo5 = Modulo13
                                Case 6
                                    SModulo6 = Modulo13
                                Case 7
                                    SModulo7 = Modulo13
                                Case 8
                                    SModulo8 = Modulo13
                                Case 9
                                    SModulo9 = Modulo13
                                Case 10
                                    SModulo10 = Modulo13
                                Case 11
                                    SModulo11 = Modulo13
                                Case 12
                                    SModulo12 = Modulo13
                                Case 13
                                    SModulo13 = Modulo13
                                Case 14
                                    SModulo14 = Modulo13
                                Case 15
                                    SModulo15 = Modulo13
                                Case 16
                                    SModulo16 = Modulo13
                                Case 17
                                    SModulo17 = Modulo13
                                Case 18
                                    SModulo18 = Modulo13
                                Case 19
                                    SModulo19 = Modulo13
                                Case 20
                                    SModulo20 = Modulo13
                                Case 21
                                    SModulo21 = Modulo13
                                Case 22
                                    SModulo22 = Modulo13
                                Case 23
                                    SModulo23 = Modulo13
                                Case 24
                                    SModulo24 = Modulo13
                                Case 25
                                    SModulo25 = Modulo13
                                Case 26
                                    SModulo26 = Modulo13
                                Case 27
                                    SModulo27 = Modulo13
                                Case 28
                                    SModulo28 = Modulo13

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If

                        If par.IfNull(myReader("MOD_CONDOMINIO"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo14
                                Case 2
                                    SModulo2 = Modulo14
                                Case 3
                                    SModulo3 = Modulo14
                                Case 4
                                    SModulo4 = Modulo14
                                Case 5
                                    SModulo5 = Modulo14
                                Case 6
                                    SModulo6 = Modulo14
                                Case 7
                                    SModulo7 = Modulo14
                                Case 8
                                    SModulo8 = Modulo14
                                Case 9
                                    SModulo9 = Modulo14
                                Case 10
                                    SModulo10 = Modulo14
                                Case 11
                                    SModulo11 = Modulo14
                                Case 12
                                    SModulo12 = Modulo14
                                Case 13
                                    SModulo13 = Modulo14
                                Case 14
                                    SModulo14 = Modulo14
                                Case 15
                                    SModulo15 = Modulo14
                                Case 16
                                    SModulo16 = Modulo14
                                Case 17
                                    SModulo17 = Modulo14
                                Case 18
                                    SModulo18 = Modulo14
                                Case 19
                                    SModulo19 = Modulo14
                                Case 20
                                    SModulo20 = Modulo14
                                Case 21
                                    SModulo21 = Modulo14
                                Case 22
                                    SModulo22 = Modulo14
                                Case 23
                                    SModulo23 = Modulo14
                                Case 24
                                    SModulo24 = Modulo14
                                Case 25
                                    SModulo25 = Modulo14
                                Case 26
                                    SModulo26 = Modulo14
                                Case 27
                                    SModulo27 = Modulo14
                                Case 28
                                    SModulo28 = Modulo14

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If


                        If par.IfNull(myReader("MOD_CICLO_P"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo15
                                Case 2
                                    SModulo2 = Modulo15
                                Case 3
                                    SModulo3 = Modulo15
                                Case 4
                                    SModulo4 = Modulo15
                                Case 5
                                    SModulo5 = Modulo15
                                Case 6
                                    SModulo6 = Modulo15
                                Case 7
                                    SModulo7 = Modulo15
                                Case 8
                                    SModulo8 = Modulo15
                                Case 9
                                    SModulo9 = Modulo15
                                Case 10
                                    SModulo10 = Modulo15
                                Case 11
                                    SModulo11 = Modulo15
                                Case 12
                                    SModulo12 = Modulo15
                                Case 13
                                    SModulo13 = Modulo15
                                Case 14
                                    SModulo14 = Modulo15
                                Case 15
                                    SModulo15 = Modulo15
                                Case 16
                                    SModulo16 = Modulo15
                                Case 17
                                    SModulo17 = Modulo15
                                Case 18
                                    SModulo18 = Modulo15
                                Case 19
                                    SModulo19 = Modulo15
                                Case 20
                                    SModulo20 = Modulo15
                                Case 21
                                    SModulo21 = Modulo15
                                Case 22
                                    SModulo22 = Modulo15
                                Case 23
                                    SModulo23 = Modulo15
                                Case 24
                                    SModulo24 = Modulo15
                                Case 25
                                    SModulo25 = Modulo15
                                Case 26
                                    SModulo26 = Modulo15
                                Case 27
                                    SModulo27 = Modulo15
                                Case 28
                                    SModulo28 = Modulo15

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If

                        If par.IfNull(myReader("MOD_GESTIONE_CONTATTI"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo16
                                Case 2
                                    SModulo2 = Modulo16
                                Case 3
                                    SModulo3 = Modulo16
                                Case 4
                                    SModulo4 = Modulo16
                                Case 5
                                    SModulo5 = Modulo16
                                Case 6
                                    SModulo6 = Modulo16
                                Case 7
                                    SModulo7 = Modulo16
                                Case 8
                                    SModulo8 = Modulo16
                                Case 9
                                    SModulo9 = Modulo16
                                Case 10
                                    SModulo10 = Modulo16
                                Case 11
                                    SModulo11 = Modulo16
                                Case 12
                                    SModulo12 = Modulo16
                                Case 13
                                    SModulo13 = Modulo16
                                Case 14
                                    SModulo14 = Modulo16
                                Case 15
                                    SModulo15 = Modulo16
                                Case 16
                                    SModulo16 = Modulo16
                                Case 17
                                    SModulo17 = Modulo16
                                Case 18
                                    SModulo18 = Modulo16
                                Case 19
                                    SModulo19 = Modulo16
                                Case 20
                                    SModulo20 = Modulo16
                                Case 21
                                    SModulo21 = Modulo16
                                Case 22
                                    SModulo22 = Modulo16
                                Case 23
                                    SModulo23 = Modulo16
                                Case 24
                                    SModulo24 = Modulo16
                                Case 25
                                    SModulo25 = Modulo16
                                Case 26
                                    SModulo26 = Modulo16
                                Case 27
                                    SModulo27 = Modulo16
                                Case 28
                                    SModulo28 = Modulo16

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If

                        If par.IfNull(myReader("MOD_AUTOGESTIONI"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo17
                                Case 2
                                    SModulo2 = Modulo17
                                Case 3
                                    SModulo3 = Modulo17
                                Case 4
                                    SModulo4 = Modulo17
                                Case 5
                                    SModulo5 = Modulo17
                                Case 6
                                    SModulo6 = Modulo17
                                Case 7
                                    SModulo7 = Modulo17
                                Case 8
                                    SModulo8 = Modulo17
                                Case 9
                                    SModulo9 = Modulo17
                                Case 10
                                    SModulo10 = Modulo17
                                Case 11
                                    SModulo11 = Modulo17
                                Case 12
                                    SModulo12 = Modulo17
                                Case 13
                                    SModulo13 = Modulo17
                                Case 14
                                    SModulo14 = Modulo17
                                Case 15
                                    SModulo15 = Modulo17
                                Case 16
                                    SModulo16 = Modulo17
                                Case 17
                                    SModulo17 = Modulo17
                                Case 18
                                    SModulo18 = Modulo17
                                Case 19
                                    SModulo19 = Modulo17
                                Case 20
                                    SModulo20 = Modulo17
                                Case 21
                                    SModulo21 = Modulo17
                                Case 22
                                    SModulo22 = Modulo17
                                Case 23
                                    SModulo23 = Modulo17
                                Case 24
                                    SModulo24 = Modulo17
                                Case 25
                                    SModulo25 = Modulo17
                                Case 26
                                    SModulo26 = Modulo17
                                Case 27
                                    SModulo27 = Modulo17
                                Case 28
                                    SModulo28 = Modulo17

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If

                        If par.IfNull(myReader("MOD_MOROSITA"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo18
                                Case 2
                                    SModulo2 = Modulo18
                                Case 3
                                    SModulo3 = Modulo18
                                Case 4
                                    SModulo4 = Modulo18
                                Case 5
                                    SModulo5 = Modulo18
                                Case 6
                                    SModulo6 = Modulo18
                                Case 7
                                    SModulo7 = Modulo18
                                Case 8
                                    SModulo8 = Modulo18
                                Case 9
                                    SModulo9 = Modulo18
                                Case 10
                                    SModulo10 = Modulo18
                                Case 11
                                    SModulo11 = Modulo18
                                Case 12
                                    SModulo12 = Modulo18
                                Case 13
                                    SModulo13 = Modulo18
                                Case 14
                                    SModulo14 = Modulo18
                                Case 15
                                    SModulo15 = Modulo18
                                Case 16
                                    SModulo16 = Modulo18
                                Case 17
                                    SModulo17 = Modulo18
                                Case 18
                                    SModulo18 = Modulo18
                                Case 19
                                    SModulo19 = Modulo18
                                Case 20
                                    SModulo20 = Modulo18
                                Case 21
                                    SModulo21 = Modulo18
                                Case 22
                                    SModulo22 = Modulo18
                                Case 23
                                    SModulo23 = Modulo18
                                Case 24
                                    SModulo24 = Modulo18
                                Case 25
                                    SModulo25 = Modulo18
                                Case 26
                                    SModulo26 = Modulo18
                                Case 27
                                    SModulo27 = Modulo18
                                Case 28
                                    SModulo28 = Modulo18

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If
                        If par.IfNull(myReader("MOD_SATISFACTION"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo19
                                Case 2
                                    SModulo2 = Modulo19
                                Case 3
                                    SModulo3 = Modulo19
                                Case 4
                                    SModulo4 = Modulo19
                                Case 5
                                    SModulo5 = Modulo19
                                Case 6
                                    SModulo6 = Modulo19
                                Case 7
                                    SModulo7 = Modulo19
                                Case 8
                                    SModulo8 = Modulo19
                                Case 9
                                    SModulo9 = Modulo19
                                Case 10
                                    SModulo10 = Modulo19
                                Case 11
                                    SModulo11 = Modulo19
                                Case 12
                                    SModulo12 = Modulo19
                                Case 13
                                    SModulo13 = Modulo19
                                Case 14
                                    SModulo14 = Modulo19
                                Case 15
                                    SModulo15 = Modulo19
                                Case 16
                                    SModulo16 = Modulo19
                                Case 17
                                    SModulo17 = Modulo19
                                Case 18
                                    SModulo18 = Modulo19
                                Case 19
                                    SModulo19 = Modulo19
                                Case 20
                                    SModulo20 = Modulo19
                                Case 21
                                    SModulo21 = Modulo19
                                Case 22
                                    SModulo22 = Modulo19
                                Case 23
                                    SModulo23 = Modulo19
                                Case 24
                                    SModulo24 = Modulo19
                                Case 25
                                    SModulo25 = Modulo19
                                Case 26
                                    SModulo26 = Modulo19
                                Case 27
                                    SModulo27 = Modulo19
                                Case 28
                                    SModulo28 = Modulo19

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If

                        If par.IfNull(myReader("MOD_STAMPE_MASSIVE"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo20
                                Case 2
                                    SModulo2 = Modulo20
                                Case 3
                                    SModulo3 = Modulo20
                                Case 4
                                    SModulo4 = Modulo20
                                Case 5
                                    SModulo5 = Modulo20
                                Case 6
                                    SModulo6 = Modulo20
                                Case 7
                                    SModulo7 = Modulo20
                                Case 8
                                    SModulo8 = Modulo20
                                Case 9
                                    SModulo9 = Modulo20
                                Case 10
                                    SModulo10 = Modulo20
                                Case 11
                                    SModulo11 = Modulo20
                                Case 12
                                    SModulo12 = Modulo20
                                Case 13
                                    SModulo13 = Modulo20
                                Case 14
                                    SModulo14 = Modulo20
                                Case 15
                                    SModulo15 = Modulo20
                                Case 16
                                    SModulo16 = Modulo20
                                Case 17
                                    SModulo17 = Modulo20
                                Case 18
                                    SModulo18 = Modulo20
                                Case 19
                                    SModulo19 = Modulo20
                                Case 20
                                    SModulo20 = Modulo20
                                Case 21
                                    SModulo21 = Modulo20
                                Case 22
                                    SModulo22 = Modulo20
                                Case 23
                                    SModulo23 = Modulo20
                                Case 24
                                    SModulo24 = Modulo20
                                Case 25
                                    SModulo25 = Modulo20
                                Case 26
                                    SModulo26 = Modulo20
                                Case 27
                                    SModulo27 = Modulo20
                                Case 28
                                    SModulo28 = Modulo20

                            End Select
                            L = L + 1
                        Else
                            'lblImpianti.Visible = False
                            'Modulo12 = ""
                        End If

                        If par.IfNull(myReader("mod_archivio"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo21
                                Case 2
                                    SModulo2 = Modulo21
                                Case 3
                                    SModulo3 = Modulo21
                                Case 4
                                    SModulo4 = Modulo21
                                Case 5
                                    SModulo5 = Modulo21
                                Case 6
                                    SModulo6 = Modulo21
                                Case 7
                                    SModulo7 = Modulo21
                                Case 8
                                    SModulo8 = Modulo21
                                Case 9
                                    SModulo9 = Modulo21
                                Case 10
                                    SModulo10 = Modulo21
                                Case 11
                                    SModulo11 = Modulo21
                                Case 12
                                    SModulo12 = Modulo21
                                Case 13
                                    SModulo13 = Modulo21
                                Case 14
                                    SModulo14 = Modulo21
                                Case 15
                                    SModulo15 = Modulo21
                                Case 16
                                    SModulo16 = Modulo21
                                Case 17
                                    SModulo17 = Modulo21
                                Case 18
                                    SModulo18 = Modulo21
                                Case 19
                                    SModulo19 = Modulo21
                                Case 20
                                    SModulo20 = Modulo21
                                Case 21
                                    SModulo21 = Modulo21
                                Case 22
                                    SModulo22 = Modulo21
                                Case 23
                                    SModulo23 = Modulo21
                                Case 24
                                    SModulo24 = Modulo21
                                Case 25
                                    SModulo25 = Modulo21
                                Case 26
                                    SModulo26 = Modulo21
                                Case 27
                                    SModulo27 = Modulo21
                                Case 28
                                    SModulo28 = Modulo21

                            End Select
                            L = L + 1
                        Else
                            'HyManutenzione.Visible = False
                            'Modulo9 = ""
                        End If

                        If par.IfNull(myReader("mod_rilievo"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo22
                                Case 2
                                    SModulo2 = Modulo22
                                Case 3
                                    SModulo3 = Modulo22
                                Case 4
                                    SModulo4 = Modulo22
                                Case 5
                                    SModulo5 = Modulo22
                                Case 6
                                    SModulo6 = Modulo22
                                Case 7
                                    SModulo7 = Modulo22
                                Case 8
                                    SModulo8 = Modulo22
                                Case 9
                                    SModulo9 = Modulo22
                                Case 10
                                    SModulo10 = Modulo22
                                Case 11
                                    SModulo11 = Modulo22
                                Case 12
                                    SModulo12 = Modulo22
                                Case 13
                                    SModulo13 = Modulo22
                                Case 14
                                    SModulo14 = Modulo22
                                Case 15
                                    SModulo15 = Modulo22
                                Case 16
                                    SModulo16 = Modulo22
                                Case 17
                                    SModulo17 = Modulo22
                                Case 18
                                    SModulo18 = Modulo22
                                Case 19
                                    SModulo19 = Modulo22
                                Case 20
                                    SModulo20 = Modulo22
                                Case 21
                                    SModulo21 = Modulo22
                                Case 22
                                    SModulo22 = Modulo22
                                Case 23
                                    SModulo23 = Modulo22
                                Case 24
                                    SModulo24 = Modulo22
                                Case 25
                                    SModulo25 = Modulo22
                                Case 26
                                    SModulo26 = Modulo22
                                Case 27
                                    SModulo27 = Modulo22
                                Case 28
                                    SModulo28 = Modulo22

                            End Select
                            L = L + 1
                        Else
                            'HyManutenzione.Visible = False
                            'Modulo9 = ""
                        End If

                        If par.IfNull(myReader("MOD_SIRAPER"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo23
                                Case 2
                                    SModulo2 = Modulo23
                                Case 3
                                    SModulo3 = Modulo23
                                Case 4
                                    SModulo4 = Modulo23
                                Case 5
                                    SModulo5 = Modulo23
                                Case 6
                                    SModulo6 = Modulo23
                                Case 7
                                    SModulo7 = Modulo23
                                Case 8
                                    SModulo8 = Modulo23
                                Case 9
                                    SModulo9 = Modulo23
                                Case 10
                                    SModulo10 = Modulo23
                                Case 11
                                    SModulo11 = Modulo23
                                Case 12
                                    SModulo12 = Modulo23
                                Case 13
                                    SModulo13 = Modulo23
                                Case 14
                                    SModulo14 = Modulo23
                                Case 15
                                    SModulo15 = Modulo23
                                Case 16
                                    SModulo16 = Modulo23
                                Case 17
                                    SModulo17 = Modulo23
                                Case 18
                                    SModulo18 = Modulo23
                                Case 19
                                    SModulo19 = Modulo23
                                Case 20
                                    SModulo20 = Modulo23
                                Case 21
                                    SModulo21 = Modulo23
                                Case 22
                                    SModulo22 = Modulo23
                                Case 23
                                    SModulo23 = Modulo23
                                Case 24
                                    SModulo24 = Modulo23
                                Case 25
                                    SModulo25 = Modulo23
                                Case 26
                                    SModulo26 = Modulo23
                                Case 27
                                    SModulo27 = Modulo23
                                Case 28
                                    SModulo28 = Modulo23
                            End Select
                            L = L + 1
                        End If

                        If par.IfNull(myReader("MOD_SICUREZZA"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo24
                                Case 2
                                    SModulo2 = Modulo24
                                Case 3
                                    SModulo3 = Modulo24
                                Case 4
                                    SModulo4 = Modulo24
                                Case 5
                                    SModulo5 = Modulo24
                                Case 6
                                    SModulo6 = Modulo24
                                Case 7
                                    SModulo7 = Modulo24
                                Case 8
                                    SModulo8 = Modulo24
                                Case 9
                                    SModulo9 = Modulo24
                                Case 10
                                    SModulo10 = Modulo24
                                Case 11
                                    SModulo11 = Modulo24
                                Case 12
                                    SModulo12 = Modulo24
                                Case 13
                                    SModulo13 = Modulo24
                                Case 14
                                    SModulo14 = Modulo24
                                Case 15
                                    SModulo15 = Modulo24
                                Case 16
                                    SModulo16 = Modulo24
                                Case 17
                                    SModulo17 = Modulo24
                                Case 18
                                    SModulo18 = Modulo24
                                Case 19
                                    SModulo19 = Modulo24
                                Case 20
                                    SModulo20 = Modulo24
                                Case 21
                                    SModulo21 = Modulo24
                                Case 22
                                    SModulo22 = Modulo24
                                Case 23
                                    SModulo23 = Modulo24
                                Case 24
                                    SModulo24 = Modulo24
                                Case 25
                                    SModulo25 = Modulo24
                                Case 26
                                    SModulo26 = Modulo24
                                Case 27
                                    SModulo27 = Modulo24
                                Case 28
                                    SModulo28 = Modulo24
                            End Select
                            L = L + 1
                        End If

                        If par.IfNull(myReader("MOD_FORNITORI"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo25
                                Case 2
                                    SModulo2 = Modulo25
                                Case 3
                                    SModulo3 = Modulo25
                                Case 4
                                    SModulo4 = Modulo25
                                Case 5
                                    SModulo5 = Modulo25
                                Case 6
                                    SModulo6 = Modulo25
                                Case 7
                                    SModulo7 = Modulo25
                                Case 8
                                    SModulo8 = Modulo25
                                Case 9
                                    SModulo9 = Modulo25
                                Case 10
                                    SModulo10 = Modulo25
                                Case 11
                                    SModulo11 = Modulo25
                                Case 12
                                    SModulo12 = Modulo25
                                Case 13
                                    SModulo13 = Modulo25
                                Case 14
                                    SModulo14 = Modulo25
                                Case 15
                                    SModulo15 = Modulo25
                                Case 16
                                    SModulo16 = Modulo25
                                Case 17
                                    SModulo17 = Modulo25
                                Case 18
                                    SModulo18 = Modulo25
                                Case 19
                                    SModulo19 = Modulo25
                                Case 20
                                    SModulo20 = Modulo25
                                Case 21
                                    SModulo21 = Modulo25
                                Case 22
                                    SModulo22 = Modulo25
                                Case 23
                                    SModulo23 = Modulo25
                                Case 24
                                    SModulo24 = Modulo25
                                Case 25
                                    SModulo25 = Modulo25
                                Case 26
                                    SModulo26 = Modulo25
                                Case 27
                                    SModulo27 = Modulo25
                                Case 28
                                    SModulo28 = Modulo25
                            End Select
                            L = L + 1
                        End If

                        If par.IfNull(myReader("MOD_ARPA"), "") = "1" Then
                            'HyManutenzione.Visible = True
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo26
                                Case 2
                                    SModulo2 = Modulo26
                                Case 3
                                    SModulo3 = Modulo26
                                Case 4
                                    SModulo4 = Modulo26
                                Case 5
                                    SModulo5 = Modulo26
                                Case 6
                                    SModulo6 = Modulo26
                                Case 7
                                    SModulo7 = Modulo26
                                Case 8
                                    SModulo8 = Modulo26
                                Case 9
                                    SModulo9 = Modulo26
                                Case 10
                                    SModulo10 = Modulo26
                                Case 11
                                    SModulo11 = Modulo26
                                Case 12
                                    SModulo12 = Modulo26
                                Case 13
                                    SModulo13 = Modulo26
                                Case 14
                                    SModulo14 = Modulo26
                                Case 15
                                    SModulo15 = Modulo26
                                Case 16
                                    SModulo16 = Modulo26
                                Case 17
                                    SModulo17 = Modulo26
                                Case 18
                                    SModulo18 = Modulo26
                                Case 19
                                    SModulo19 = Modulo26
                                Case 20
                                    SModulo20 = Modulo26
                                Case 21
                                    SModulo21 = Modulo26
                                Case 22
                                    SModulo22 = Modulo26
                                Case 23
                                    SModulo23 = Modulo26
                                Case 24
                                    SModulo24 = Modulo26
                                Case 25
                                    SModulo25 = Modulo26
                                Case 26
                                    SModulo26 = Modulo26
                                Case 27
                                    SModulo27 = Modulo26
                                Case 28
                                    SModulo28 = Modulo26
                            End Select
                            L = L + 1
                        End If
                        If par.IfNull(myReader("MOD_CICLO_P"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo27
                                Case 2
                                    SModulo2 = Modulo27
                                Case 3
                                    SModulo3 = Modulo27
                                Case 4
                                    SModulo4 = Modulo27
                                Case 5
                                    SModulo5 = Modulo27
                                Case 6
                                    SModulo6 = Modulo27
                                Case 7
                                    SModulo7 = Modulo27
                                Case 8
                                    SModulo8 = Modulo27
                                Case 9
                                    SModulo9 = Modulo27
                                Case 10
                                    SModulo10 = Modulo27
                                Case 11
                                    SModulo11 = Modulo27
                                Case 12
                                    SModulo12 = Modulo27
                                Case 13
                                    SModulo13 = Modulo27
                                Case 14
                                    SModulo14 = Modulo27
                                Case 15
                                    SModulo15 = Modulo27
                                Case 16
                                    SModulo16 = Modulo27
                                Case 17
                                    SModulo17 = Modulo27
                                Case 18
                                    SModulo18 = Modulo27
                                Case 19
                                    SModulo19 = Modulo27
                                Case 20
                                    SModulo20 = Modulo27
                                Case 21
                                    SModulo21 = Modulo27
                                Case 22
                                    SModulo22 = Modulo27
                                Case 23
                                    SModulo23 = Modulo27
                                Case 24
                                    SModulo24 = Modulo27
                                Case 25
                                    SModulo25 = Modulo27
                                Case 26
                                    SModulo26 = Modulo27
                                Case 27
                                    SModulo27 = Modulo27
                                Case 28
                                    SModulo28 = Modulo27
                            End Select
                            L = L + 1
                        End If

                        If par.IfNull(myReader("FL_SPESE_REVERSIBILI"), "") = "1" Then
                            Select Case L
                                Case 1
                                    SModulo1 = Modulo28
                                Case 2
                                    SModulo2 = Modulo28
                                Case 3
                                    SModulo3 = Modulo28
                                Case 4
                                    SModulo4 = Modulo28
                                Case 5
                                    SModulo5 = Modulo28
                                Case 6
                                    SModulo6 = Modulo28
                                Case 7
                                    SModulo7 = Modulo28
                                Case 8
                                    SModulo8 = Modulo28
                                Case 9
                                    SModulo9 = Modulo28
                                Case 10
                                    SModulo10 = Modulo28
                                Case 11
                                    SModulo11 = Modulo28
                                Case 12
                                    SModulo12 = Modulo28
                                Case 13
                                    SModulo13 = Modulo28
                                Case 14
                                    SModulo14 = Modulo28
                                Case 15
                                    SModulo15 = Modulo28
                                Case 16
                                    SModulo16 = Modulo28
                                Case 17
                                    SModulo17 = Modulo28
                                Case 18
                                    SModulo18 = Modulo28
                                Case 19
                                    SModulo19 = Modulo28
                                Case 20
                                    SModulo20 = Modulo28
                                Case 21
                                    SModulo21 = Modulo28
                                Case 22
                                    SModulo22 = Modulo28
                                Case 23
                                    SModulo23 = Modulo28
                                Case 24
                                    SModulo24 = Modulo28
                                Case 25
                                    SModulo25 = Modulo28
                                Case 26
                                    SModulo26 = Modulo28
                                Case 27
                                    SModulo27 = Modulo28
                                Case 28
                                    SModulo28 = Modulo28
                            End Select
                            L = L + 1
                        End If
                    End If
                    myReader.Close()
                Else
                    'HyBandoERP.Visible = True
                    'HyBandoCambi.Visible = True
                    'HyBandoCambi.Visible = True
                    'HyBandoFSA.Visible = True

                    'HyBandoAU.Visible = True
                    'HyAbbinamento.Visible = True
                    'HyAbbinamentoDec.Visible = True
                    'HyConsultazione.Visible = True
                    'HyManutenzione.Visible = True
                    'lblContabilita.Visible = True
                    SModulo1 = Modulo1
                    SModulo2 = Modulo2
                    SModulo3 = Modulo3
                    SModulo4 = Modulo4
                    SModulo5 = Modulo5
                    SModulo6 = Modulo6
                    SModulo7 = Modulo7
                    SModulo8 = Modulo8
                    SModulo9 = Modulo9
                    SModulo10 = Modulo10
                    SModulo11 = Modulo11
                    SModulo12 = Modulo12
                    SModulo13 = Modulo13
                    SModulo14 = Modulo14
                    SModulo15 = Modulo15
                    SModulo16 = Modulo16
                    SModulo17 = Modulo17
                    SModulo18 = Modulo18
                    SModulo19 = Modulo19
                    SModulo20 = Modulo20
                    SModulo21 = Modulo21
                    SModulo22 = Modulo22
                    SModulo23 = Modulo23
                    SModulo24 = Modulo24
                    SModulo25 = Modulo25
                    SModulo26 = Modulo26
                    SModulo27 = Modulo27
                    SModulo28 = Modulo28

                    L = 29

                End If


                Select Case L - 1
                    Case 0
                        AltezzaColonna1 = 0
                        AltezzaColonna2 = 0
                        AltezzaColonna3 = 0
                    Case 1
                        AltezzaColonna1 = 180
                        AltezzaColonna2 = 0
                        AltezzaColonna3 = 0
                    Case 2
                        AltezzaColonna1 = 180
                        AltezzaColonna2 = 180
                        AltezzaColonna3 = 0
                    Case 3
                        AltezzaColonna1 = 180
                        AltezzaColonna2 = 180
                        AltezzaColonna3 = 180
                    Case 4
                        AltezzaColonna1 = 380
                        AltezzaColonna2 = 190
                        AltezzaColonna3 = 190
                    Case 5
                        AltezzaColonna1 = 380
                        AltezzaColonna2 = 380
                        AltezzaColonna3 = 190
                    Case 6
                        AltezzaColonna1 = 380
                        AltezzaColonna2 = 380
                        AltezzaColonna3 = 380
                    Case 7
                        AltezzaColonna1 = 570
                        AltezzaColonna2 = 380
                        AltezzaColonna3 = 380
                    Case 8
                        AltezzaColonna1 = 570
                        AltezzaColonna2 = 570
                        AltezzaColonna3 = 380
                    Case 9
                        AltezzaColonna1 = 570
                        AltezzaColonna2 = 570
                        AltezzaColonna3 = 570
                    Case 10
                        AltezzaColonna1 = 760
                        AltezzaColonna2 = 570
                        AltezzaColonna3 = 570
                    Case 11
                        AltezzaColonna1 = 760
                        AltezzaColonna2 = 760
                        AltezzaColonna3 = 570
                    Case 12
                        AltezzaColonna1 = 800
                        AltezzaColonna2 = 800
                        AltezzaColonna3 = 800
                    Case 13
                        AltezzaColonna1 = 1000
                        AltezzaColonna2 = 800
                        AltezzaColonna3 = 800
                    Case 14
                        AltezzaColonna1 = 1000
                        AltezzaColonna2 = 1000
                        AltezzaColonna3 = 800
                    Case 15
                        AltezzaColonna1 = 1000
                        AltezzaColonna2 = 1000
                        AltezzaColonna3 = 1000
                    Case 16
                        AltezzaColonna1 = 1220
                        AltezzaColonna2 = 1000
                        AltezzaColonna3 = 1000
                    Case 17
                        AltezzaColonna1 = 1220
                        AltezzaColonna2 = 1220
                        AltezzaColonna3 = 1000
                    Case 18
                        AltezzaColonna1 = 1220
                        AltezzaColonna2 = 1220
                        AltezzaColonna3 = 1220
                    Case 19
                        AltezzaColonna1 = 1410
                        AltezzaColonna2 = 1220
                        AltezzaColonna3 = 1220
                    Case 20
                        AltezzaColonna1 = 1410
                        AltezzaColonna2 = 1410
                        AltezzaColonna3 = 1220
                    Case 21
                        AltezzaColonna1 = 1410
                        AltezzaColonna2 = 1410
                        AltezzaColonna3 = 1410
                    Case 22
                        AltezzaColonna1 = 1650
                        AltezzaColonna2 = 1410
                        AltezzaColonna3 = 1410
                    Case 23
                        AltezzaColonna1 = 1650
                        AltezzaColonna2 = 1650
                        AltezzaColonna3 = 1410
                    Case 24
                        AltezzaColonna1 = 1650
                        AltezzaColonna2 = 1650
                        AltezzaColonna3 = 1650
                    Case 25
                        AltezzaColonna1 = 1840
                        AltezzaColonna2 = 1650
                        AltezzaColonna3 = 1650
                    Case 26
                        AltezzaColonna1 = 1840
                        AltezzaColonna2 = 1840
                        AltezzaColonna3 = 1650
                    Case 27
                        AltezzaColonna1 = 1840
                        AltezzaColonna2 = 1840
                        AltezzaColonna3 = 1840
                    Case 28
                        AltezzaColonna1 = 2040
                        AltezzaColonna2 = 1840
                        AltezzaColonna3 = 1840

                End Select
                C1.Value = AltezzaColonna1
                C2.Value = AltezzaColonna2
                C3.Value = AltezzaColonna3
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If

    End Sub


    Protected Sub LinkLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkLogOut.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then

            Else
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If
            If IsNothing(Session.Item("ID_OPERATORE")) = False Then
                par.cmd.CommandText = "UPDATE OPERATORI_WEB_LOG SET DATA_ORA_OUT='" & Format(Now, "yyyyMMddHHmm") & "' WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & " AND DATA_ORA_IN='" & Session.Item("DATA_IN") & "'"
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Session.RemoveAll()
            Session.Abandon()
            Session.Clear()

            HttpContext.Current.Session.Abandon()
            Response.Redirect("Portale.aspx", False)

        Catch ex As Exception
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.RemoveAll()
            Session.Abandon()
            Session.Clear()
            HttpContext.Current.Session.Abandon()
            Response.Redirect("Portale.aspx", True)
        End Try




    End Sub


    Public Property Sezione1() As String
        Get
            If Not (ViewState("par_Sezione1") Is Nothing) Then
                Return CStr(ViewState("par_Sezione1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sezione1") = value
        End Set

    End Property

End Class
