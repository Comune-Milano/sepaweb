Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_SimulaB
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0
    'Dim giorniScadERP As Integer = 1
    'Dim giorniScadAltri As Integer = 1


    Public Property sSimulazione() As String
        Get
            If Not (ViewState("par_sSimulazione") Is Nothing) Then
                Return CStr(ViewState("par_sSimulazione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSimulazione") = value
        End Set

    End Property


    Public Property Capoluoghi() As String
        Get
            If Not (ViewState("par_Capoluoghi") Is Nothing) Then
                Return CStr(ViewState("par_Capoluoghi"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Capoluoghi") = value
        End Set

    End Property


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""


        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;font-size: 8pt; color: #000080; text-align: center;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)
        Response.Flush()

        txtEmissione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtScadenzaAltri.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        If Not IsPostBack Then
            Try


                sSimulazione = Request.QueryString("S")




                par.OracleConn.Open()
                par.SettaCommand(par)

                cmbComplesso.Items.Clear()
                cmbEdificio.Items.Clear()
                cmbUnita.Items.Clear()


                'par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=7"
                'Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    giorniScadERP = par.IfNull(myReaderA("VALORE"), 1)
                'End If
                'myReaderA.Close()

                'par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=3"
                'myReaderA = par.cmd.ExecuteReader()
                'If myReaderA.Read Then
                '    giorniScadAltri = par.IfNull(myReaderA("VALORE"), 1)
                'End If
                'myReaderA.Close()


                cmbComplesso.Items.Add(New ListItem("TUTTI", -1))
                cmbEdificio.Items.Add(New ListItem("TUTTI", -1))
                cmbUnita.Items.Add(New ListItem("TUTTI", -1))

                par.cmd.CommandText = "SELECT id,denominazione FROM SISCOM_MI.complessi_immobiliari order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read

                    cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                cmbComplesso.SelectedIndex = -1
                cmbComplesso.Items.FindByValue("-1").Selected = True

                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -3, Now))) & " " & Year(DateAdd("M", -3, Now)), CStr(Year(DateAdd("M", -3, Now)) & Format(Month(DateAdd("M", -3, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -2, Now))) & " " & Year(DateAdd("M", -2, Now)), CStr(Year(DateAdd("M", -2, Now)) & Format(Month(DateAdd("M", -2, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -1, Now))) & " " & Year(DateAdd("M", -1, Now)), CStr(Year(DateAdd("M", -1, Now)) & Format(Month(DateAdd("M", -1, Now)), "00"))))

                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(Now)) & " " & Year(Now), CStr(Year(Now) & Format(Month(Now), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 1, Now))) & " " & Year(DateAdd("M", 1, Now)), CStr(Year(DateAdd("M", 1, Now)) & Format(Month(DateAdd("M", 1, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 2, Now))) & " " & Year(DateAdd("M", 2, Now)), CStr(Year(DateAdd("M", 2, Now)) & Format(Month(DateAdd("M", 2, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 3, Now))) & " " & Year(DateAdd("M", 3, Now)), CStr(Year(DateAdd("M", 3, Now)) & Format(Month(DateAdd("M", 3, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 4, Now))) & " " & Year(DateAdd("M", 4, Now)), CStr(Year(DateAdd("M", 4, Now)) & Format(Month(DateAdd("M", 4, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 5, Now))) & " " & Year(DateAdd("M", 5, Now)), CStr(Year(DateAdd("M", 5, Now)) & Format(Month(DateAdd("M", 5, Now)), "00"))))
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 6, Now))) & " " & Year(DateAdd("M", 6, Now)), CStr(Year(DateAdd("M", 6, Now)) & Format(Month(DateAdd("M", 6, Now)), "00"))))

                If sSimulazione = "1" Then
                    Label7.Text = "Simulazione"
                    Label1.Text = "Questa procedura ha lo scopo di simulare l'emissione delle bollette. Può essere effettuata più volte e non causa modifiche ai contratti/bollette. Alla fine del processo sarà generato un file di testo contenente i dettagli per ogni singola bolletta. Sarà sempre possibile visualizzare il file tramite la funzione -Elenco simulazioni-"
                    rbCSV.Visible = True
                    Label12.Visible = True
                    rbCSV0.Visible = True
                    Label13.Visible = True
                    ChkUltima.Visible = True
                    Label10.Visible = True
                Else
                    Label7.Text = "Emissione"
                    Label1.Text = "Questa procedura emetterà le bollette."
                    rbCSV.Visible = False
                    Label12.Visible = False
                    rbCSV0.Visible = False
                    Label13.Visible = False
                    ChkUltima.Visible = False
                    Label10.Visible = False
                End If
                '


                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
            

        End If
    End Sub


    Private Function CaricaCapoluoghi()
        Capoluoghi = "AGRIGENTO (SICILIA) " _
        & "ALESSANDRIA(PIEMONTE) " _
        & "ANCONA(MARCHE) " _
& "AOSTA (VALLE D'AOSTA) " _
        & "AREZZO(TOSCANA) " _
        & "ASCOLI PICENO(MARCHE)) " _
        & "ASTI(PIEMONTE) " _
        & "AVELLINO(CAMPANIA) " _
        & "BARI(PUGLIA)  " _
        & "TRANI() " _
        & "ANDRIA() " _
        & "BARLETTA() " _
        & "BELLUNO(VENETO) " _
        & "BENEVENTO(CAMPANIA) " _
        & "BERGAMO(LOMBARDIA) " _
        & "BIELLA(PIEMONTE) " _
        & "BOLOGNA(EMILIA - ROMAGNA) " _
& "BOLZANO (TRENTINO-ALTO ADIGE) " _
        & "BRESCIA(LOMBARDIA) " _
        & "BRINDISI(PUGLIA) " _
        & "CAGLIARI(SARDEGNA) " _
        & "CALTANISSETTA(SICILIA) " _
        & "CAMPOBASSO(MOLISE) " _
        & "CARBONIA-IGLESIAS(SARDEGNA)) " _
        & "CASERTA(CAMPANIA) " _
        & "CATANIA(SICILIA) " _
        & "CATANZARO(CALABRIA) " _
        & "CHIETI(ABRUZZO) " _
        & "COMO(LOMBARDIA) " _
        & "COSENZA(CALABRIA) " _
        & "CREMONA(LOMBARDIA) " _
        & "CROTONE(CALABRIA) " _
        & "CUNEO(PIEMONTE) " _
        & "ENNA(SICILIA) " _
        & "FERMO(MARCHE) " _
        & "FERRARA(EMILIA - ROMAGNA) " _
        & "FIRENZE(TOSCANA) " _
        & "FOGGIA(PUGLIA) " _
        & "FORLÌ-CESENA(EMILIA - ROMAGNA) " _
        & "FROSINONE(LAZIO) " _
        & "GENOVA(LIGURIA) " _
& "GORIZIA (FRIULI-VENEZIA GIULIA) " _
        & "GROSSETO(TOSCANA) " _
        & "IMPERIA(LIGURIA) " _
        & "ISERNIA(MOLISE) " _
        & "LA SPEZIA(LIGURIA)) " _
        & "L'AQUILA (ABRUZZO) " _
        & "LATINA(LAZIO) " _
        & "LECCE(PUGLIA) " _
        & "LECCO(LOMBARDIA) " _
        & "LIVORNO(TOSCANA) " _
        & "LODI(LOMBARDIA) " _
        & "LUCCA(TOSCANA) " _
        & "MACERATA(MARCHE) " _
        & "MANTOVA(LOMBARDIA) " _
        & "MASSA-CARRARA(TOSCANA)) " _
        & "MATERA(BASILICATA) " _
        & "MESSINA(SICILIA) " _
        & " " _
        & "MODENA(EMILIA - ROMAGNA) " _
        & "MONZA() " _
        & "NAPOLI(CAMPANIA) " _
        & "NOVARA(PIEMONTE) " _
        & "NUORO(SARDEGNA) " _
        & "OLBIA(-TEMPIO(SARDEGNA)) " _
        & "ORISTANO(SARDEGNA) " _
        & "PADOVA(VENETO)" _
        & "PALERMO(SICILIA) " _
        & "PARMA(EMILIA - ROMAGNA) " _
        & "PAVIA(LOMBARDIA) " _
        & "PERUGIA(UMBRIA) " _
& "PESARO E URBINO (MARCHE) " _
   & "PESCARA(ABRUZZO) " _
      & "  PIACENZA(EMILIA - ROMAGNA) " _
        & "PISA(TOSCANA) " _
        & "PISTOIA(TOSCANA) " _
& "PORDENONE (FRIULI-VENEZIA GIULIA) " _
        & "POTENZA(BASILICATA) " _
        & "PRATO(TOSCANA) " _
        & "RAGUSA(SICILIA) " _
        & "RAVENNA(EMILIA - ROMAGNA) " _
        & "REGGIO CALABRIA(CALABRIA)) " _
        & "REGGIO EMILIA(EMILIA - ROMAGNA)) " _
        & "RIETI(LAZIO)  " _
        & "RIMINI(EMILIA - ROMAGNA) " _
        & "ROMA(LAZIO) " _
        & "ROVIGO(VENETO) " _
        & "SALERNO(CAMPANIA) " _
        & "MEDIO CAMPIDANO(SARDEGNA))  " _
        & "SASSARI(SARDEGNA)  " _
        & "SAVONA(LIGURIA) " _
        & "SIENA(TOSCANA) " _
        & "SIRACUSA(SICILIA) " _
        & "SONDRIO(LOMBARDIA) " _
        & "TARANTO(PUGLIA) " _
        & "TERAMO(ABRUZZO) " _
        & "TERNI(UMBRIA) " _
        & "TORINO(PIEMONTE) " _
        & "OGLIASTRA(SARDEGNA) " _
        & "TRAPANI(SICILIA) " _
& "TRENTO (TRENTINO-ALTO ADIGE) " _
        & "TREVISO(VENETO) " _
& "TRIESTE (FRIULI-VENEZIA GIULIA) " _
& "UDINE (FRIULI-VENEZIA GIULIA) " _
   & "     OSSOLA() " _
      & "  VARESE(LOMBARDIA) " _
        & "CUSIO() " _
        & "VENEZIA(VENETO) " _
        & "VERBANIA() " _
        & "VERBANO() " _
        & "VERCELLI(PIEMONTE) " _
        & "VERONA(VENETO) " _
        & "VIBO VALENTIA(CALABRIA)) " _
        & "VICENZA(VENETO) " _
        & "VITERBO(LAZIO)"
    End Function
   
    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Dim myReaderBOL As Oracle.DataAccess.Client.OracleDataReader
        Dim xlsNumBolletta As String = ""
        Dim xlsDataEmissione As String = ""
        Dim xlsDataScadenza As String = ""
        Dim xlsPeriodoRif As String = ""
        Dim xlsCodContratto As String = ""
        Dim xlsTipoContratto As String = ""
        Dim xlsNominativo As String = ""
        Dim xlsPresso As String = ""
        Dim xlsIndirizzoCorr As String = ""
        Dim xlsCapCorr As String = ""
        Dim xlsLuogoCor As String = ""
        Dim xlsIndirizzoUN As String = ""
        Dim xlsCapUN As String = ""
        Dim xlsLocalitaUN As String = ""
        Dim xlsPianoUN As String = ""
        Dim xlsInternoUN As String = ""
        Dim xlsDataDecorrenza As String = ""
        Dim xlsDataDisdetta As String = ""
        Dim xlsDataSloggio As String = ""
        Dim xlsInteressiC As String = ""
        Dim xlsInteressiCong As String = ""
        Dim xlsInvioBoll As String = ""
        Dim xlsInteressiRIT As String = ""
        Dim xlsDataCalcolo As String = ""
        Dim xlsAreaCanone As String = ""
        Dim xlsClasse As String = ""
        Dim xlsValiditaCan As String = ""
        Dim xlsProvenienza As String = ""

        Dim xlsUltimoImporto As String = ""
        Dim xlsUltimoRiferimento As String = ""

        Dim xlsVoci() As String
        Dim xlsImporti() As Double
        Dim xlsIndice As Long = 0
        Dim xlsI As Long = 0
        Dim TotXls As Double = 0

        Try
            If par.IfEmpty(txtEmissione.Text, "") = "" Then
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility='hidden';alert('Inserire una data valida!');</script>")
                Exit Try
            End If

            If par.IfEmpty(txtScadenza.Text, "") = "" Then
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility='hidden';alert('Inserire una data di scadenza ERP valida!');</script>")
                Exit Try
            End If

            If par.IfEmpty(txtScadenzaAltri.Text, "") = "" Then
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility='hidden';alert('Inserire una data di scadenza ALTRI valida!');</script>")
                Exit Try
            End If

            If par.AggiustaData(txtScadenza.Text) < Format(Now, "yyyyMMdd") Then
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility='hidden';alert('Le date di scadenza devono essere successive alla data odierna (preferibilmente oltre i 10 giorni)!');</script>")
                Exit Try
            End If

            If par.AggiustaData(txtScadenzaAltri.Text) < Format(Now, "yyyyMMdd") Then
                Response.Write("<script>document.getElementById('dvvvPre').style.visibility='hidden';alert('Le date di scadenza devono essere successive alla data odierna (preferibilmente oltre i 10 giorni)!');</script>")
                Exit Try
            End If

            par.OracleConn.Open()
            par.SettaCommand(par)

            If sSimulazione <> "1" Then
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
            End If


            par.cmd.CommandText = "select * FROM SISCOM_MI.RAPPORTI_UTENZA FOR UPDATE NOWAIT"
            myReaderBOL = par.cmd.ExecuteReader()


            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.* FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL WHERE RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA=" & cmbMese.SelectedItem.Value & " and INVIO_BOLLETTA='1' AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND RAPPORTI_UTENZA.DATA_DECORRENZA<=" & cmbMese.SelectedItem.Value & "31  AND ( (PRESSO_COR IS NULL OR PRESSO_COR='' OR PRESSO_COR=' ') OR (LUOGO_COR IS NULL OR LUOGO_COR='' OR LUOGO_COR=' ') OR (VIA_COR IS NULL OR VIA_COR='' OR VIA_COR=' ') OR (CIVICO_COR IS NULL OR CIVICO_COR='' OR CIVICO_COR=' ') OR (SIGLA_COR IS NULL OR SIGLA_COR='' OR SIGLA_COR=' ') OR (CAP_COR IS NULL OR CAP_COR='' OR CAP_COR=' '))"
            Dim myReaderIND As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIND.HasRows = False Then
                myReaderIND.Close()


                par.cmd.CommandText = "SELECT ANAGRAFICA.cognome, ANAGRAFICA.nome , RAPPORTI_UTENZA.cod_contratto FROM SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA WHERE SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA=" & cmbMese.SelectedItem.Value & " AND INVIO_BOLLETTA='1' AND SISCOM_MI.Getstatocontratto(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND SOGGETTI_CONTRATTUALI.id_contratto=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.cod_tipologia_occupante='INTE' AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND (ANAGRAFICA.cognome IS NULL OR ANAGRAFICA.nome IS NULL) AND ANAGRAFICA.ragione_sociale IS NULL"
                myReaderIND = par.cmd.ExecuteReader()
                If myReaderIND.HasRows = True Then
                    myReaderBOL.Close()
                    Dim ELENCO As String = ""
                    Do While myReaderIND.Read
                        ELENCO = ELENCO & myReaderIND("COD_CONTRATTO") & vbCrLf
                    Loop
                    myReaderIND.Close()
                    Response.Write("<script>alert('Non è possibile emettere le bollette. I rapporti elencati in maschera hanno un un intestatario con nome o cognome errato o mancante! Risolvere il problema prima di proseguire.');</script>")
                    lblerrore.Visible = True
                    lblerrore.Text = "Rapporti con nominativo intestatario errato o mancante: " & vbCrLf & ELENCO
                    If sSimulazione <> "1" Then
                        par.myTrans.Commit()
                    End If
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Exit Sub
                End If
                myReaderIND.Close()

                Dim ID_BOLLETTA As Long = 0
                Dim TOTALE As Double = 0
                Dim A As String = ""
                Dim Contatore As Long = 0
                Dim sAggiunta As String = ""
                Dim SPESEmav As Double = 0
                Dim SPESEPOSTALI As Double = 0
                Dim SPESEPOSTALI_DA_APPLICARE As Double = 0
                Dim SPESEPOSTALI_CAPOLUOGHI As Double = 0
                Dim SPESEPOSTALI_ALTRI As Double = 0
                Dim BOLLO As Double = 0
                Dim APPLICABOLLO As Double = 0
                Dim CiSonoInteressi As Boolean = False
                Dim ScadenzaPagamento As String = ""
                Dim iNumRata As Integer
                Dim sDataInizio As String = ""
                Dim sDataFine As String = ""
                Dim Giorni1 As Integer = 0
                Dim Giorni2 As Integer = 0
                Dim sProssimo_Periodo As String = ""

                Dim TOTALE_ERP As Long = 0
                Dim TOTALE_ALTRI As Long = 0
                Dim TOTALE_NONE As Long = 0
                Dim TOTALE_USD As Long = 0
                Dim TOTALE_431 As Long = 0
                Dim TOTALE_392 As Long = 0

                Dim importoerp As Double = 0
                Dim importoAltri As Double = 0
                Dim importoUSD As Double = 0
                Dim importo431 As Double = 0
                Dim importo392 As Double = 0
                Dim importoNONE As Double = 0

                Dim STATO_CONTRATTO As String = ""
                Dim aggiornamento_istat As Double = 0
                Dim AltriAdeguamenti As Double = 0
                Dim NUMERORIGHE As Long = 0
                Dim bolletteemesse As Long = 0
                Dim ARROTONDAMENTO As Double = 0
                Dim IntestazioneBolletta As String = ""

                par.cmd.CommandText = "select * FROM SISCOM_MI.adeguamento_interessi WHERE fl_applicato=0"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    CiSonoInteressi = True
                End If
                myReaderA.Close()

                'MAX 18/10/2017 RESTITUZ. INTERESSI DEP. CAUZ
                Dim REST_INT As Integer = 0
                Dim RATA_REST_INT As Integer = 0
                Dim Restituisci As Boolean = False
                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=71"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    REST_INT = par.IfNull(myReaderA("VALORE"), 0)
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=72"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    RATA_REST_INT = par.IfNull(myReaderA("VALORE"), 0)
                End If
                myReaderA.Close()
                '-------------------


                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=29"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPESEPOSTALI_CAPOLUOGHI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=30"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPESEPOSTALI_ALTRI = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=27"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    Select Case par.IfNull(myReaderA("VALORE"), "0")
                        Case "0"
                            'da mettere sempre
                        Case ("1")
                            'solo mesi dispari
                            If CDbl(Mid(cmbMese.SelectedItem.Value, 5, 2)) Mod 2 = 0 Then
                                SPESEPOSTALI = 0
                                SPESEPOSTALI_CAPOLUOGHI = 0
                                SPESEPOSTALI_ALTRI = 0
                            End If
                        Case "2"
                            'solo mesi pari
                            If CDbl(Mid(cmbMese.SelectedItem.Value, 5, 2)) Mod 2 <> 0 Then
                                SPESEPOSTALI = 0
                                SPESEPOSTALI_CAPOLUOGHI = 0
                                SPESEPOSTALI_ALTRI = 0
                            End If
                    End Select

                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=28"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    Select Case par.IfNull(myReaderA("VALORE"), "0")
                        Case "0"
                            'da mettere sempre
                        Case ("1")
                            'solo mesi dispari
                            If CDbl(Mid(cmbMese.SelectedItem.Value, 5, 2)) Mod 2 = 0 Then
                                SPESEmav = 0
                            End If
                        Case "2"
                            'solo mesi pari
                            If CDbl(Mid(cmbMese.SelectedItem.Value, 5, 2)) Mod 2 <> 0 Then
                                SPESEmav = 0
                            End If
                    End Select

                End If
                myReaderA.Close()

                Dim IndiceExport As Long = 0


                Dim smNomeFile As String = ""
                If sSimulazione <> "1" Then
                    smNomeFile = "EMISSIONE_" & par.ConvertiMese(Mid(cmbMese.SelectedItem.Value, 5, 2)) & "_" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "-" & Format(Now, "yyyyMMddHHmmss")
                Else
                    smNomeFile = "SIMULAZIONE_" & par.ConvertiMese(Mid(cmbMese.SelectedItem.Value, 5, 2)) & "_" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "-" & Format(Now, "yyyyMMddHHmmss")
                    par.cmd.CommandText = "delete from siscom_mi.EXPORT_SIMULAZIONE"
                    par.cmd.ExecuteNonQuery()
                End If

                Dim srCSV As StreamWriter
                If rbCSV.Checked = True Then
                    srCSV = New StreamWriter(Server.MapPath("..\FileTemp\" & smNomeFile & ".csv"), False, System.Text.Encoding.ASCII)
                    srCSV.WriteLine("COD. CONTRATTO;TIPO CONTRATTO;NOMINATIVO;INDIRIZZO;CAP;LUOGO;PIANO;INTERNO;RECAPITO(PRESSO);RECAPITO(INDIRIZZO);RECAPITO(CAP);RECAPITO(LUOGO);DATA DECORRENZA;DATA DISDETTA;DATA SLOGGIO;CANONE DATA_CALCOLO;CANONE AREA;CANONE CLASSE;CANONE VALIDITA;CANONE ORIGINE;INTERESSI CAUZIONE;CONGUAGLIO BOLLETTE;INVIO BOLLETTE;INTERESSI RITARDO PAGAMENTO;NUM. BOLLETTA;DATA_EMISSIONE;DATA_SCADENZA;PERIODO RIFERIMENTO;TOTALE BOLLETTA;VOCE BOLLETTA;IMPORTO VOCE;TOTALE BOLLETTA ORDINARIA PRECEDENTE;RIFERIMENTO BOLLETTA ORDINARIA PRECEDENTE;")
                End If
                If sSimulazione = "1" Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EXPORT_SIMULAZIONE (ID,TESTO,IDENTIFICATIVO) VALUES ('" & IndiceExport & "','" & par.PulisciStrSql("COD. CONTRATTO;TIPO CONTRATTO;NOMINATIVO;INDIRIZZO;CAP;LUOGO;PIANO;INTERNO;RECAPITO(PRESSO);RECAPITO(INDIRIZZO);RECAPITO(CAP);RECAPITO(LUOGO);DATA DECORRENZA;DATA DISDETTA;DATA SLOGGIO;CANONE DATA_CALCOLO;CANONE AREA;CANONE CLASSE;CANONE VALIDITA;CANONE ORIGINE;INTERESSI CAUZIONE;CONGUAGLIO BOLLETTE;INVIO BOLLETTE;INTERESSI RITARDO PAGAMENTO;NUM. BOLLETTA;DATA_EMISSIONE;DATA_SCADENZA;PERIODO RIFERIMENTO;TOTALE BOLLETTA;VOCE BOLLETTA;IMPORTO VOCE;TOTALE BOLLETTA ORDINARIA PRECEDENTE;RIFERIMENTO BOLLETTA ORDINARIA PRECEDENTE;") & "','" & smNomeFile & "')"
                    par.cmd.ExecuteNonQuery()
                    IndiceExport = IndiceExport + 1
                End If

                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\" & smNomeFile & ".txt"), False, System.Text.Encoding.ASCII)

                CaricaCapoluoghi()

                If cmbComplesso.SelectedItem.Value <> "-1" Then
                    sAggiunta = "EDIFICI.ID_COMPLESSO=" & cmbComplesso.SelectedItem.Value
                End If

                If cmbEdificio.SelectedItem.Value <> "-1" Then
                    If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "
                    sAggiunta = "UNITA_CONTRATTUALE.ID_EDIFICIO=" & cmbEdificio.SelectedItem.Value
                End If

                If cmbUnita.SelectedItem.Value <> "-1" Then
                    If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "
                    sAggiunta = "UNITA_CONTRATTUALE.COD_UNITA_IMMOBILIARE='" & cmbUnita.SelectedItem.Text & "' "
                End If
                If sAggiunta <> "" Then sAggiunta = sAggiunta & " AND "

                Dim ESERCIZIOFCORRENTE As Long = par.RicavaEsercizioCorrente
                Dim DataEmissione As String = par.AggiustaData(txtEmissione.Text)
                Dim DataScadenza As String = par.AggiustaData(txtScadenzaAltri.Text) ' par.AggiustaData(DateAdd("d", giorniScadAltri, CDate(par.FormattaData(DataEmissione))))
                Dim DataScadenzaERP As String = par.AggiustaData(txtScadenza.Text) ' par.AggiustaData(DateAdd("d", giorniScadERP, CDate(par.FormattaData(DataEmissione))))



                Dim RigaXLS As Long = 2
                par.cmd.CommandText = "select  ROWNUM,anagrafica.cognome,anagrafica.nome,ANAGRAFICA.RAGIONE_SOCIALE,(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ISTAT"",(select SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ""ADEGUAMENTO""," _
                                    & "SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA,RAPPORTI_UTENZA.*,siscom_mi.getstatocontratto(RAPPORTI_UTENZA.id) as ""STATO_CONTRATTO"",EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO," _
                                    & "UNITA_CONTRATTUALE.ID_UNITA,UNITA_IMMOBILIARI.INTERNO,INDIRIZZI.DESCRIZIONE AS IND_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS LOC_UNITA,'' AS PIANO_UNITA FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL,SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI," _
                                    & "SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.INDIRIZZI WHERE " & sAggiunta & " UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND BOZZA='0' AND siscom_mi.getstatocontratto(RAPPORTI_UTENZA.id)<>'CHIUSO' AND RAPPORTI_UTENZA.DATA_DECORRENZA<=" & cmbMese.SelectedItem.Value & "31  " _
                                    & "AND SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL.PROSSIMA_BOLLETTA='" & cmbMese.SelectedItem.Value _
                                    & "' and SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND " _
                                    & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO and rapporti_utenza.invio_bolletta='1' ORDER BY ROWNUM DESC"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read
                    If NUMERORIGHE = 0 Then
                        NUMERORIGHE = par.IfNull(myReader("ROWNUM"), 0)
                    End If
                    xlsNumBolletta = ""
                    xlsDataEmissione = ""
                    xlsDataScadenza = ""
                    xlsPeriodoRif = ""
                    xlsCodContratto = ""
                    xlsTipoContratto = ""
                    xlsNominativo = ""
                    xlsPresso = ""
                    xlsIndirizzoCorr = ""
                    xlsCapCorr = ""
                    xlsLuogoCor = ""
                    xlsIndirizzoUN = ""
                    xlsCapUN = ""
                    xlsLocalitaUN = ""
                    xlsPianoUN = ""
                    xlsInternoUN = ""
                    xlsDataDecorrenza = ""
                    xlsDataDisdetta = ""
                    xlsDataSloggio = ""
                    xlsInteressiC = ""
                    xlsInteressiCong = ""
                    xlsInvioBoll = ""
                    xlsInteressiRIT = ""
                    xlsDataCalcolo = ""
                    xlsAreaCanone = ""
                    xlsClasse = ""
                    xlsValiditaCan = ""
                    xlsProvenienza = ""

                    xlsUltimoImporto = ""
                    xlsUltimoRiferimento = ""

                    ReDim xlsVoci(0)
                    ReDim xlsImporti(0)

                    xlsVoci(0) = ""
                    xlsImporti(0) = 0

                    STATO_CONTRATTO = myReader("STATO_CONTRATTO")

                    'calcolo del prossimo periodo di bollettazione
                    sProssimo_Periodo = par.CalcolaPeriodo(Mid(cmbMese.SelectedItem.Value, 5, 2), par.IfNull(myReader("nro_rate"), "0"))
                    If sProssimo_Periodo = "01" Then
                        sProssimo_Periodo = CStr(Year(Now) + 1) & "01" 'sProssimo_Periodo
                    Else
                        sProssimo_Periodo = Mid(cmbMese.SelectedItem.Value, 1, 4) & sProssimo_Periodo
                    End If

                    'calcolo la rata corrente
                    iNumRata = par.VerificaSePeriodoOk(Mid(cmbMese.SelectedItem.Value, 1, 6), par.IfNull(myReader("nro_rate"), "0"), sDataInizio, sDataFine)
                    'calcolo del giorni che intercorrono tra la data di inizio e di fine
                    Giorni1 = DateDiff("d", CDate(par.FormattaData(sDataInizio)), CDate(par.FormattaData(sDataFine)))

                    'verifico se la data di fine non è quella fissata, 
                    'ma in caso di recesso o riconsegna o scadenza deve essere anticipata
                    If par.IfNull(myReader("DATA_RICONSEGNA"), "") <> "" Then
                        If par.IfNull(myReader("DATA_RICONSEGNA"), "0") < sDataFine Then
                            sDataFine = par.IfNull(myReader("DATA_RICONSEGNA"), "0")
                        End If
                    Else
                    End If
                    'calcolo il periodo di giorni effettivo
                    Giorni2 = DateDiff("d", CDate(par.FormattaData(sDataInizio)), CDate(par.FormattaData(sDataFine)))

                    If Giorni2 > 0 Then
                        ''calcolo la data di scadenza della bolletta
                        Contatore = Contatore + 1
                        percentuale = (Contatore * 100) / NUMERORIGHE
                        Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                        Response.Flush()


                        If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "ERP" Then
                            ScadenzaPagamento = DataScadenzaERP
                        Else
                            ScadenzaPagamento = DataScadenza
                        End If

                        Dim PRESSO As String = UCase(Trim(par.IfNull(myReader("PRESSO_COR"), "")))
                        Dim Cognome As String = ""
                        Dim Nome As String = ""

                        If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                            Cognome = par.IfNull(myReader("ragione_sociale"), "")
                            Nome = ""
                        Else
                            Cognome = par.IfNull(myReader("cognome"), "")
                            Nome = par.IfNull(myReader("nome"), "")
                        End If

                        If UCase(Trim(Cognome & " " & Nome)) = PRESSO Then
                            PRESSO = ""
                        End If
                        xlsUltimoImporto = ""
                        xlsUltimoRiferimento = ""

                        If ChkUltima.Checked = True Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE  substr(rif_file,1,3)='BO_' and ID_BOLLETTA_STORNO IS NULL AND ID_TIPO=1 AND ID_CONTRATTO=" & myReader("ID") & " ORDER BY ID DESC"
                            Dim myReaderUltimo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderUltimo.Read Then
                                xlsUltimoImporto = Format(par.IfNull(myReaderUltimo("IMPORTO_TOTALE"), 0), "##,##0.00")
                                xlsUltimoRiferimento = "DAL " & par.FormattaData(par.IfNull(myReaderUltimo("RIFERIMENTO_DA"), "")) & " AL " & par.FormattaData(par.IfNull(myReaderUltimo("RIFERIMENTO_A"), ""))
                            End If
                            myReaderUltimo.Close()
                        Else
                            xlsUltimoImporto = ""
                            xlsUltimoRiferimento = ""
                        End If

                        If sSimulazione <> "1" Then
                            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                                            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                                            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                            & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE_TXT,ID_TIPO) " _
                                                            & "Values " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, " & iNumRata & " , '" & DataEmissione _
                                                            & "', '" & ScadenzaPagamento & "', NULL,NULL,NULL,'RATA n." & iNumRata & "'," _
                                                            & "" & myReader("ID") _
                                                            & " ," & ESERCIZIOFCORRENTE & ", " _
                                                            & myReader("ID_UNITA") _
                                                            & ", '0', ''," & par.IfNull(myReader("ID_ANAGRAFICA"), 0) _
                                                            & ", '" & par.PulisciStrSql(Trim(Cognome & " " & Nome)) & "', " _
                                                            & "'" & par.PulisciStrSql(par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReader("CIVICO_COR"), ""))) _
                                                            & "', '" & par.PulisciStrSql(par.IfNull(myReader("CAP_COR"), "") & " " & par.IfNull(myReader("LUOGO_COR"), "") & "(" & par.IfNull(myReader("SIGLA_COR"), "") & ")") _
                                                            & "', '" & par.PulisciStrSql(PRESSO) & "', '" & sDataInizio _
                                                            & "', '" & sDataFine & "', " _
                                                            & "'0', " & myReader("ID_COMPLESSO") & ", '', NULL, '', " _
                                                            & Mid(cmbMese.SelectedItem.Value, 1, 4) & ", '', " & myReader("ID_EDIFICIO") & ", NULL, NULL,'" & smNomeFile & "',1)"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If PRESSO <> "" Then
                            PRESSO = vbCrLf & "C/O " & PRESSO
                        End If

                        IntestazioneBolletta = Trim(Cognome & " " & Nome) _
                                            & PRESSO _
                                            & vbCrLf _
                                            & par.PulisciStrSql(par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReader("CIVICO_COR"), ""))) _
                                            & vbCrLf _
                                            & par.PulisciStrSql(par.IfNull(myReader("CAP_COR"), "") & " " & par.IfNull(myReader("LUOGO_COR"), "") & "(" & par.IfNull(myReader("SIGLA_COR"), "") & ")") _
                                            & vbCrLf & vbCrLf _
                                            & "Codice Contratto: " & par.IfNull(myReader("cod_contratto"), "") _
                                            & vbCrLf _
                                            & "DATA EMISSIONE: " & par.FormattaData(DataEmissione) & vbTab & "DATA SCADENZA:" & par.FormattaData(ScadenzaPagamento) _
                                            & vbCrLf _
                                            & "RATA N.: " & iNumRata & vbTab & "DAL:" & par.FormattaData(sDataInizio) & " AL " & par.FormattaData(sDataFine) _
                                            & vbCrLf & vbCrLf

                        xlsNumBolletta = iNumRata
                        xlsDataEmissione = par.FormattaData(DataEmissione)
                        xlsDataScadenza = par.FormattaData(ScadenzaPagamento)
                        xlsPeriodoRif = par.FormattaData(sDataInizio) & " AL " & par.FormattaData(sDataFine)

                        xlsCodContratto = par.IfNull(myReader("cod_contratto"), "")
                        Select Case Mid(par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "   "), 1, 3)
                            Case "ERP"
                                xlsTipoContratto = "ERP"
                            Case "USD"
                                xlsTipoContratto = "USD"
                            Case "L43"
                                xlsTipoContratto = "L431/98"
                            Case "EQC"
                                xlsTipoContratto = "EQC"
                            Case "NON"
                                xlsTipoContratto = "ABUSIVO"
                            Case Else
                                xlsTipoContratto = "ALTRO TIPO"
                        End Select
                        xlsNominativo = Trim(Cognome & " " & Nome)
                        xlsPresso = Trim(Replace(PRESSO, vbCrLf, " "))
                        xlsIndirizzoCorr = Trim(par.PulisciStrSql(par.IfNull(myReader("TIPO_COR"), "") & " " & par.IfNull(myReader("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReader("CIVICO_COR"), ""))))
                        xlsCapCorr = Trim(par.IfNull(myReader("CAP_COR"), ""))
                        xlsLuogoCor = Trim(par.IfNull(myReader("LUOGO_COR"), "") & "(" & par.IfNull(myReader("SIGLA_COR"), "") & ")")
                        xlsIndirizzoUN = Trim(par.IfNull(myReader("IND_UNITA"), "") & " " & par.IfNull(myReader("CIVICO_UNITA"), ""))
                        xlsCapUN = Trim(par.IfNull(myReader("CAP_UNITA"), ""))
                        xlsLocalitaUN = Trim(par.IfNull(myReader("LOC_UNITA"), ""))
                        xlsPianoUN = Trim(par.IfNull(myReader("PIANO_UNITA"), ""))
                        xlsInternoUN = Trim(par.IfNull(myReader("INTERNO"), ""))
                        xlsDataDecorrenza = par.FormattaData(par.IfNull(myReader("DATA_DECORRENZA"), ""))
                        xlsDataDisdetta = par.FormattaData(par.IfNull(myReader("DATA_DISDETTA_LOCATARIO"), ""))
                        xlsDataSloggio = par.FormattaData(par.IfNull(myReader("DATA_RICONSEGNA"), ""))
                        If par.IfNull(myReader("INTERESSI_CAUZIONE"), "0") = "0" Then
                            xlsInteressiC = "NO"
                        Else
                            xlsInteressiC = "SI"
                        End If
                        If par.IfNull(myReader("FL_CONGUAGLIO"), "0") = "0" Then
                            xlsInteressiCong = "NO"
                        Else
                            xlsInteressiCong = "SI"
                        End If
                        If par.IfNull(myReader("INVIO_BOLLETTA"), "0") = "0" Then
                            xlsInvioBoll = "NO"
                        Else
                            xlsInvioBoll = "SI"
                        End If
                        If par.IfNull(myReader("INTERESSI_RIT_PAG"), "0") = "0" Then
                            xlsInteressiRIT = "NO"
                        Else
                            xlsInteressiRIT = "SI"
                        End If

                        xlsDataCalcolo = ""
                        xlsAreaCanone = ""
                        xlsClasse = ""
                        xlsValiditaCan = ""
                        xlsProvenienza = ""

                        par.cmd.CommandText = "select T_TIPO_PROVENIENZA.DESCRIZIONE AS PROVENIENZA,DECODE(ID_AREA_ECONOMICA,1,'PROTEZIONE',2,'ACCESSO',3,'PERMANENZA',4,'DECADENZA') AS AREA_CANONE,CANONI_EC.* FROM T_TIPO_PROVENIENZA,SISCOM_MI.CANONI_EC WHERE T_TIPO_PROVENIENZA.ID(+)=CANONI_EC.TIPO_PROVENIENZA AND  ID_CONTRATTO=" & myReader("ID") & " AND TO_NUMBER (REPLACE(CANONE,'.',''))=" & par.VirgoleInPunti(par.IfNull(myReader("IMP_CANONE_INIZIALE"), "0")) & " ORDER BY DATA_CALCOLO DESC"
                        Dim myReaderCan As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderCan.HasRows = True Then
                            If myReaderCan.Read Then
                                xlsDataCalcolo = par.FormattaData(Mid(par.IfNull(myReaderCan("DATA_CALCOLO"), ""), 1, 8))
                                xlsAreaCanone = par.IfNull(myReaderCan("AREA_CANONE"), "")
                                xlsClasse = par.IfNull(myReaderCan("SOTTO_AREA"), "")
                                xlsValiditaCan = "DAL " & par.FormattaData(par.IfNull(myReaderCan("INIZIO_VALIDITA_CAN"), "")) & " AL " & par.FormattaData(par.IfNull(myReaderCan("FINE_VALIDITA_CAN"), ""))
                                xlsProvenienza = par.IfNull(myReaderCan("PROVENIENZA"), "")
                            End If
                        Else
                            xlsDataCalcolo = ""
                            xlsAreaCanone = ""
                            xlsClasse = ""
                            xlsValiditaCan = ""
                            xlsProvenienza = ""
                        End If
                        myReaderCan.Close()

                        If sSimulazione <> "1" Then
                            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderB.Read Then
                                ID_BOLLETTA = myReaderB(0)
                            Else
                                ID_BOLLETTA = -1
                            End If
                            myReaderB.Close()
                        End If


                        'calcolo l'importo dell'affitto in base al numero di rate annue
                        If par.IfNull(myReader("imp_CANONE_INIZIALE"), 0) > 0 Then

                            If par.IfNull(myReader("PROVENIENZA_ASS"), "") <> "7" Then
                                If STATO_CONTRATTO = "IN CORSO (S.T.)" Then
                                    IntestazioneBolletta = IntestazioneBolletta & "INDENNITA' DI OCCUPAZIONE".ToString.PadRight(40) & " " & Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                    ReDim Preserve xlsVoci(xlsIndice)
                                    ReDim Preserve xlsImporti(xlsIndice)
                                    xlsVoci(xlsIndice) = "INDENNITA' DI OCCUPAZIONE"
                                    xlsImporti(xlsIndice) = Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "##,##0.00")
                                Else
                                    IntestazioneBolletta = IntestazioneBolletta & "CANONE DI LOCAZIONE".ToString.PadRight(40) & " " & Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                    ReDim Preserve xlsVoci(xlsIndice)
                                    ReDim Preserve xlsImporti(xlsIndice)
                                    xlsVoci(xlsIndice) = "CANONE DI LOCAZIONE"
                                    xlsImporti(xlsIndice) = Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "##,##0.00")
                                End If
                            Else
                                IntestazioneBolletta = IntestazioneBolletta & "INDENNITA' DI OCCUPAZIONE ABUSIVA".ToString.PadRight(40) & " " & Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                ReDim Preserve xlsVoci(xlsIndice)
                                ReDim Preserve xlsImporti(xlsIndice)
                                xlsVoci(xlsIndice) = "INDENNITA' DI OCCUPAZIONE ABUSIVA"
                                xlsImporti(xlsIndice) = Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "##,##0.00")
                            End If
                            xlsIndice = xlsIndice + 1
                        End If

                        If par.IfNull(myReader("imp_CANONE_INIZIALE"), 0) > 0 Then
                            TOTALE = Format((par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1), "0.00")

                            If sSimulazione <> "1" Then
                                If par.IfNull(myReader("PROVENIENZA_ASS"), "") <> "7" Then
                                    If STATO_CONTRATTO = "IN CORSO (S.T.)" Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",36" _
                                                            & "," & par.VirgoleInPunti(TOTALE) & ")"
                                        par.cmd.ExecuteNonQuery()

                                        ARROTONDAMENTO = 0
                                        ARROTONDAMENTO = (par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1) - TOTALE
                                        If ARROTONDAMENTO <> 0 Then

                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                                & myReader("ID") & ",36," _
                                                                & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                            par.cmd.ExecuteNonQuery()
                                        End If


                                    Else
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",1" _
                                                            & "," & par.VirgoleInPunti(TOTALE) & ")"
                                        par.cmd.ExecuteNonQuery()

                                        ARROTONDAMENTO = 0
                                        ARROTONDAMENTO = (par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1) - TOTALE
                                        If ARROTONDAMENTO <> 0 Then
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                                  & myReader("ID") & ",1," _
                                                                  & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                    End If
                                Else
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",36" _
                                        & "," & par.VirgoleInPunti(TOTALE) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    ARROTONDAMENTO = 0
                                    ARROTONDAMENTO = (par.IfNull(myReader("imp_CANONE_INIZIALE"), 1) / par.IfNull(myReader("NRO_RATE"), 12)) * (Giorni2 / Giorni1) - TOTALE
                                    If ARROTONDAMENTO <> 0 Then

                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                            & myReader("ID") & ",36," _
                                                            & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If

                            End If
                        End If

                        aggiornamento_istat = 0
                        'calcolo eventuali aggiornamenti istat

                        aggiornamento_istat = Format(par.IfNull(myReader("ISTAT"), 0) / par.IfNull(myReader("NRO_RATE"), 12), "0.00")
                        If aggiornamento_istat <> 0 Then
                            If STATO_CONTRATTO = "IN CORSO (S.T.)" Or par.IfNull(myReader("PROVENIENZA_ASS"), "") = "7" Then
                                IntestazioneBolletta = IntestazioneBolletta & "MONTANTE ISTAT INDENNITA'".ToString.PadRight(40) & " " & Format((aggiornamento_istat), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                ReDim Preserve xlsVoci(xlsIndice)
                                ReDim Preserve xlsImporti(xlsIndice)
                                xlsVoci(xlsIndice) = "MONTANTE ISTAT INDENNITA'"
                                xlsImporti(xlsIndice) = Format((aggiornamento_istat), "##,##0.00")
                            Else
                                IntestazioneBolletta = IntestazioneBolletta & "AGGIORNAMENTO ISTAT".ToString.PadRight(40) & " " & Format((aggiornamento_istat), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                ReDim Preserve xlsVoci(xlsIndice)
                                ReDim Preserve xlsImporti(xlsIndice)
                                xlsVoci(xlsIndice) = "AGGIORNAMENTO ISTAT"
                                xlsImporti(xlsIndice) = Format((aggiornamento_istat), "##,##0.00")
                            End If
                            TOTALE = TOTALE + Format(aggiornamento_istat, "0.00")
                            xlsIndice = xlsIndice + 1
                            If sSimulazione <> "1" Then
                                If STATO_CONTRATTO = "IN CORSO (S.T.)" Or par.IfNull(myReader("PROVENIENZA_ASS"), "") = "7" Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",405" _
                                                        & "," & par.VirgoleInPunti(Format(aggiornamento_istat, "0.00")) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    ARROTONDAMENTO = 0
                                    ARROTONDAMENTO = (par.IfNull(myReader("ISTAT"), 0) / par.IfNull(myReader("NRO_RATE"), 12)) - aggiornamento_istat
                                    If ARROTONDAMENTO <> 0 Then

                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                            & myReader("ID") & ",405," _
                                                            & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                        par.cmd.ExecuteNonQuery()

                                    End If
                                Else
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",404" _
                                                        & "," & par.VirgoleInPunti(Format(aggiornamento_istat, "0.00")) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    ARROTONDAMENTO = 0
                                    ARROTONDAMENTO = (par.IfNull(myReader("ISTAT"), 0) / par.IfNull(myReader("NRO_RATE"), 12)) - aggiornamento_istat
                                    If ARROTONDAMENTO <> 0 Then

                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                            & myReader("ID") & ",404," _
                                                            & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                        End If



                        AltriAdeguamenti = 0
                        'calcolo eventuali Adeguamenti al canone

                        AltriAdeguamenti = Format(par.IfNull(myReader("ADEGUAMENTO"), 0) / par.IfNull(myReader("NRO_RATE"), 12), "0.00")
                        If AltriAdeguamenti <> 0 Then

                            IntestazioneBolletta = IntestazioneBolletta & "ADEGUAMENTO CANONE".ToString.PadRight(40) & " " & Format((AltriAdeguamenti), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                            TOTALE = TOTALE + Format(AltriAdeguamenti, "0.00")
                            ReDim Preserve xlsVoci(xlsIndice)
                            ReDim Preserve xlsImporti(xlsIndice)
                            xlsVoci(xlsIndice) = "ADEGUAMENTO CANONE"
                            xlsImporti(xlsIndice) = Format((AltriAdeguamenti), "##,##0.00")
                            xlsIndice = xlsIndice + 1



                            If sSimulazione <> "1" Then
                                If STATO_CONTRATTO = "IN CORSO (S.T.)" Or par.IfNull(myReader("PROVENIENZA_ASS"), "") = "7" Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",654" _
                                                        & "," & par.VirgoleInPunti(Format(AltriAdeguamenti, "0.00")) & ")"
                                Else
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",652" _
                                                        & "," & par.VirgoleInPunti(Format(AltriAdeguamenti, "0.00")) & ")"
                                End If

                                par.cmd.ExecuteNonQuery()

                                ARROTONDAMENTO = 0
                                ARROTONDAMENTO = (par.IfNull(myReader("ADEGUAMENTO"), 0) / par.IfNull(myReader("NRO_RATE"), 12)) - AltriAdeguamenti
                                If ARROTONDAMENTO <> 0 Then
                                    If STATO_CONTRATTO = "IN CORSO (S.T.)" Or par.IfNull(myReader("PROVENIENZA_ASS"), "") = "7" Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                            & myReader("ID") & ",654," _
                                                            & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                            & myReader("ID") & ",652," _
                                                            & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If

                                End If

                            End If
                        End If


                        If par.IfNull(myReader("PROVENIENZA_ASS"), "") <> "7" Then
                            'verifico se deve pagare la PROROGA del contratto

                            'Dim PROROGA As Boolean = False
                            'Dim DRIN As Integer

                            'If myReader("DATA_STIPULA") >= "20100101" And Mid(myReader("COD_CONTRATTO"), 1, 6) <> "000000" And Mid(myReader("data_decorrenza"), 5, 2) = Mid(cmbMese.SelectedItem.Value, 5, 2) And Mid(par.IfNull(myReader("data_decorrenza"), "00000000"), 1, 4) <> Mid(cmbMese.SelectedItem.Value, 1, 4) Then

                            '    If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "ERP" And par.IfNull(myReader("DATA_DECORRENZA"), "19990101") >= 20040101 Then
                            '        If par.IfNull(myReader("DURATA_RINNOVO"), 0) <> 0 Then
                            '    If (CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)) - CInt(Mid(par.IfNull(myReader("DATA_DECORRENZA"), "2010"), 1, 4))) Mod CInt(myReader("DURATA_RINNOVO")) = 0 Then
                            '        PROROGA = True
                            '    End If
                            'End If



                            '    End If

                            '    If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") <> "ERP" Then
                            '        If par.IfNull(myReader("DURATA_RINNOVO"), 0) = 0 Then
                            '            DRIN = CInt(myReader("DURATA_ANNI"))
                            'Else
                            '            DRIN = CInt(myReader("DURATA_RINNOVO"))
                            '            If DRIN = 0 Then DRIN = CInt(myReader("DATA_ANNI"))
                            '        End If
                            '        If (CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)) - CInt(Mid(par.IfNull(myReader("DATA_DECORRENZA"), "2010"), 1, 4))) Mod DRIN = 0 Then
                            '            PROROGA = True
                            '        End If
                            '    End If

                            '    Dim Agevolato As String = "N"

                            '        par.cmd.CommandText = "select tipologia_contratto_locazione.* from siscom_mi.tipologia_contratto_locazione where cod='" & par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") & "'"
                            '        Dim myReaderFF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            '        If myReaderFF.Read Then
                            '        Dim importo112 As Double = 0
                            '        Dim importo112_1 As Double = 0

                            '        If par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "") = "L43198" And (par.IfNull(myReader("DEST_USO"), "") = "0" Or par.IfNull(myReader("DEST_USO"), "") = "P") Then
                            '            Agevolato = "S"
                            '        End If

                            '        If PROROGA = True Then
                            '            If par.IfNull(myReader("versamento_tr"), "A") <> "U" Then
                            '                If Agevolato = "N" Then
                            '                    importo112 = Format(((par.IfNull(myReaderFF("perc_conduttore"), 0) * Format(((par.IfNull(myReaderFF("perc_tr_canone"), 0) * (par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)) / 100), "0")) / 100), "0.00")
                            '                ElseIf Agevolato = "S" Then
                            '                    importo112 = Format(((par.IfNull(myReaderFF("perc_conduttore"), 0) * Format(((par.IfNull(myReaderFF("perc_tr_canone"), 0) * ((par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti) - ((30 / 100) * (par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)))) / 100), "0")) / 100), "0.00")
                            '                End If
                            '                If importo112 < 33.5 Then
                            '                    importo112 = 33.5
                            '                End If
                            '            Else
                            '                If Agevolato = "N" Then
                            '                    importo112 = par.SoluzioneUnica(CDbl(par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti), CInt(myReader("durata_anni")))
                            '                ElseIf Agevolato = "S" Then
                            '                    importo112 = par.SoluzioneUnica(CDbl(par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti) - ((30 / 100) * CDbl(par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)), CInt(myReader("durata_anni")))
                            '                End If

                            '                importo112_1 = ((par.IfNull(myReaderFF("perc_conduttore"), 0) * Format(((par.IfNull(myReaderFF("perc_tr_canone"), 0) * (par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)) / 100), "0")) / 100)

                            '                If (importo112 / 2) <= (importo112_1 * CInt(myReader("durata_anni"))) * 2 Then
                            '                    importo112 = importo112 / 2
                            '                Else
                            '                    importo112 = importo112_1
                            '                    If sSimulazione <> "1" Then
                            '                        par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET VERSAMENTO_TR='A' WHERE ID=" & myReader("ID")
                            '                        par.cmd.ExecuteNonQuery()
                            '                    End If
                            '                End If
                            '                If importo112 < 33.5 Then
                            '                    importo112 = 33.5
                            '                End If
                            '            End If

                            '        Else
                            '            If par.IfNull(myReader("versamento_tr"), "A") <> "U" Then
                            '                If Agevolato = "N" Then
                            '                    importo112 = Format(((par.IfNull(myReaderFF("perc_conduttore"), 0) * Format(((par.IfNull(myReaderFF("perc_tr_canone"), 0) * (par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)) / 100), "0")) / 100), "0.00")
                            '                ElseIf Agevolato = "S" Then
                            '                    importo112 = Format(((par.IfNull(myReaderFF("perc_conduttore"), 0) * Format(((par.IfNull(myReaderFF("perc_tr_canone"), 0) * ((par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti) - ((30 / 100) * (par.IfNull(myReader("imp_CANONE_iniziale"), 0) + aggiornamento_istat + AltriAdeguamenti)))) / 100), "0")) / 100), "0.00")
                            '                End If
                            '            Else
                            '                importo112 = 0
                            '            End If
                            '        End If

                            '            If importo112 > 0 Then

                            '            importo112 = Format(importo112, 0)

                            '                IntestazioneBolletta = IntestazioneBolletta & "REGISTRAZIONE CONTRATTO".ToString.PadRight(40) & " " & Format(importo112, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                            '            TOTALE = TOTALE + Format(importo112, "0")

                            '            ReDim Preserve xlsVoci(xlsIndice)
                            '            ReDim Preserve xlsImporti(xlsIndice)
                            '            xlsVoci(xlsIndice) = "REGISTRAZIONE CONTRATTO"
                            '            xlsImporti(xlsIndice) = Format(importo112, "##,##0.00")
                            '            xlsIndice = xlsIndice + 1

                            '                If sSimulazione <> "1" Then
                            '                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                            '                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",93" _
                            '                                        & "," & par.VirgoleInPunti(Format(importo112, "0.00")) & ")"
                            '                    par.cmd.ExecuteNonQuery()
                            '                End If
                            '            End If
                            '        End If
                            '        myReaderFF.Close()

                            '    End If

                            'CercaVociGestPerBolletta(myReader("ID"), ID_BOLLETTA, sSimulazione)

                            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.*,NOTE,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST, SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST,SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI_GEST.ID_VOCE AND TIPO_APPLICAZIONE='N' AND" _
                                                & " BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST=SISCOM_MI.BOL_BOLLETTE_GEST.ID AND BOL_BOLLETTE_GEST.ID_TIPO=SISCOM_MI.TIPO_BOLLETTE_GEST.ID AND FL_IN_BOLLETTA=1 AND ID_CONTRATTO=" & myReader("ID")
                            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
                            Dim dtVoci As New Data.DataTable
                            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                            daVoci.Fill(dtVoci)
                            daVoci.Dispose()
                            For Each row As Data.DataRow In dtVoci.Rows
                                If sSimulazione <> 1 Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                                        & "," & row.Item("ID_VOCE") & "" _
                                                        & "," & par.VirgoleInPunti(Format(row.Item("IMPORTO"), "0.00")) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    'AGGIORNO CON TIPO APPLICAZIONE = T (: spostamento totale)
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & row.Item("ID_BOLLETTA_GEST")
                                    par.cmd.ExecuteNonQuery()

                                End If

                                IntestazioneBolletta = IntestazioneBolletta & row.Item("DESCRIZIONE").ToString.PadRight(40) & " " & Format(row.Item("IMPORTO"), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                TOTALE = TOTALE + Format(row.Item("IMPORTO"), "0.00")

                                ReDim Preserve xlsVoci(xlsIndice)
                                ReDim Preserve xlsImporti(xlsIndice)
                                xlsVoci(xlsIndice) = row.Item("DESCRIZIONE")
                                xlsImporti(xlsIndice) = Format(row.Item("IMPORTO"), "##,##0.00")
                                xlsIndice = xlsIndice + 1



                            Next



                        End If

                        If par.IfNull(myReader("PROVENIENZA_ASS"), "") <> "7" Then
                            Restituisci = False
                            If CiSonoInteressi = True Then
                                If REST_INT = 1 Then
                                    Restituisci = True
                                Else
                                    If iNumRata = RATA_REST_INT Then
                                        Restituisci = True
                                    Else
                                        Restituisci = False
                                    End If
                                End If

                                'Verifico se inserire interessi su cauzione
                                If Restituisci = True Then
                                    'par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & myReader("id") & " and fl_applicato=0 AND order by id desc"
                                    'MODIFICA PER BOLLETTAZIONE 6 BIM 2019
                                    par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & myReader("id") & " and fl_applicato=0 AND ID_CONTRATTO IN (SELECT ID FROM SISCOM_MI.INTERESSI6BIM) order by id desc"
                                    Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderF.Read Then
                                        Dim InteressiCauzione As Double = par.IfNull(myReaderF("importo"), 0) * -1
                                        If InteressiCauzione <> 0 Then
                                            IntestazioneBolletta = IntestazioneBolletta & "RIMB.INTERESSE ANNUO CAUZIONE".ToString.PadRight(40) & " " & Format(InteressiCauzione, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                            TOTALE = TOTALE + Format(InteressiCauzione, "0.00")
                                            ReDim Preserve xlsVoci(xlsIndice)
                                            ReDim Preserve xlsImporti(xlsIndice)
                                            xlsVoci(xlsIndice) = "RIMB.INTERESSE ANNUO CAUZIONE"
                                            xlsImporti(xlsIndice) = Format(InteressiCauzione, "##,##0.00")
                                            xlsIndice = xlsIndice + 1
                                            If sSimulazione <> "1" Then
                                                Dim idBollGest As Long = 0
                                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",15" _
                                                                    & "," & par.VirgoleInPunti(Format(InteressiCauzione, "0.00")) & ")"
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set fl_applicato=1 where id_contratto=" & myReader("id") & " and fl_applicato=0"
                                                par.cmd.ExecuteNonQuery()
                                                'scrivere bol_bollette_gest di tipo 57 con voce=15 con applicazione=T

                                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                                                                    & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE,ID_ADEGUAMENTO,ID_BOLLETTA) " _
                                                                    & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & myReader("id") & "," & par.RicavaEsercizioCorrente & "," & myReader("ID_UNITA") & "," _
                                                                    & par.IfNull(myReader("ID_ANAGRAFICA"), 0) & ",'" & sDataInizio & "','" & sDataFine _
                                                                    & "'," & par.VirgoleInPunti(Format(InteressiCauzione, "0.00")) & "," _
                                                                    & "'" & Format(Now, "yyyyMMdd") & "','','',57,'T'," & Session.Item("ID_OPERATORE") & ",'RIMB.INTERESSE ANNUO CAUZIONE".ToString.PadRight(40) _
                                                                    & "'," & myReaderF("id") & "," & ID_BOLLETTA & ")"
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                                                Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                                If myReaderZ.Read() Then
                                                    idBollGest = myReaderZ(0)
                                                End If
                                                myReaderZ.Close()
                                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                                                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",15," & par.VirgoleInPunti(Format(InteressiCauzione, "0.00")) & ")"
                                                par.cmd.ExecuteNonQuery()
                                            Else
                                                If sSimulazione <> "1" Then
                                                    'importo 0, non viene scritto nulla
                                                    par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set fl_applicato=1 where id_contratto=" & myReader("id") & " and fl_applicato=0"
                                                    par.cmd.ExecuteNonQuery()
                                                End If
                                            End If

                                        End If
                                        
                                        
                                    End If
                                    myReaderF.Close()
                                End If
                            End If
                        End If
                        'spese postali
                        If UCase(par.IfNull(myReader("LUOGO_COR"), "")) = "MILANO" Then
                            SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI
                        Else
                            If InStr(Capoluoghi, UCase(par.IfNull(myReader("LUOGO_COR"), ""))) > 0 Then
                                SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI_CAPOLUOGHI
                            Else
                                SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI_ALTRI
                            End If
                        End If

                        If SPESEPOSTALI_DA_APPLICARE > 0 Then
                            IntestazioneBolletta = IntestazioneBolletta & "SPESE POSTALI".ToString.PadRight(40) & " " & Format(SPESEPOSTALI_DA_APPLICARE, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                            TOTALE = TOTALE + SPESEPOSTALI_DA_APPLICARE
                            ReDim Preserve xlsVoci(xlsIndice)
                            ReDim Preserve xlsImporti(xlsIndice)
                            xlsVoci(xlsIndice) = "SPESE POSTALI"
                            xlsImporti(xlsIndice) = Format(SPESEPOSTALI_DA_APPLICARE, "##,##0.00")
                            xlsIndice = xlsIndice + 1
                            If sSimulazione <> "1" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",403" _
                                                    & "," & par.VirgoleInPunti(Format(SPESEPOSTALI_DA_APPLICARE, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If

                        'spese mav
                        If SPESEmav > 0 Then
                            IntestazioneBolletta = IntestazioneBolletta & "SPESE MAV".ToString.PadRight(40) & " " & Format(SPESEmav, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                            TOTALE = TOTALE + SPESEmav
                            ReDim Preserve xlsVoci(xlsIndice)
                            ReDim Preserve xlsImporti(xlsIndice)
                            xlsVoci(xlsIndice) = "SPESE MAV"
                            xlsImporti(xlsIndice) = Format(SPESEmav, "##,##0.00")
                            xlsIndice = xlsIndice + 1
                            If sSimulazione <> "1" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                                    & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If

                        'prendo tutte le voci nello schema dell'anno di riferimento del mese scelto
                        par.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from siscom_mi.bol_schema,siscom_mi.t_voci_bolletta where " & iNumRata & " >= da_rata And (" & iNumRata & "- da_rata) <= (per_rate - 1) AND t_voci_bolletta.id=bol_schema.id_voce and  bol_schema.id_contratto=" & myReader("id") & " and anno=" & Mid(cmbMese.SelectedItem.Value, 1, 4)
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader1.Read
                            ARROTONDAMENTO = 0

                            TOTALE = TOTALE + CDbl(myReader1("importo_singola_rata") * (Giorni2 / Giorni1))
                            IntestazioneBolletta = IntestazioneBolletta & myReader1("descrizione").ToString.PadRight(40) & " " & Format(myReader1("importo_singola_rata") * (Giorni2 / Giorni1), "##,##0.00").ToString.PadLeft(15) & vbCrLf
                            ReDim Preserve xlsVoci(xlsIndice)
                            ReDim Preserve xlsImporti(xlsIndice)
                            xlsVoci(xlsIndice) = UCase(myReader1("descrizione"))
                            xlsImporti(xlsIndice) = Format(myReader1("importo_singola_rata") * (Giorni2 / Giorni1), "##,##0.00")
                            xlsIndice = xlsIndice + 1

                            If sSimulazione <> "1" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & myReader1("ID_VOCE") _
                                                    & "," & par.VirgoleInPunti(Format(myReader1("importo_singola_rata") * (Giorni2 / Giorni1), "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()

                                ARROTONDAMENTO = myReader1("importo") / myReader1("PER_RATE") - myReader1("importo_singola_rata") * (Giorni2 / Giorni1)

                                If ARROTONDAMENTO <> 0 Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_SCHEMA_ARR (ID_CONTRATTO,ID_VOCE,IMPORTO,ID_BOLLETTA) VALUES (" _
                                                        & myReader("ID") & "," & myReader1("ID_VOCE") & "," _
                                                        & par.VirgoleInPunti(ARROTONDAMENTO) & "," & ID_BOLLETTA & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If

                            End If
                        Loop
                        myReader1.Close()

                        If TOTALE > SPESEmav + SPESEPOSTALI_DA_APPLICARE Then
                            bolletteemesse = bolletteemesse + 1
                            If BOLLO > 0 Then
                                If TOTALE > APPLICABOLLO And par.IfNull(myReader("FL_BOLLO_ASSOLTO"), "0") = "0" Then
                                    IntestazioneBolletta = IntestazioneBolletta & "BOLLO".ToString.PadRight(40) & " " & Format(BOLLO, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                    TOTALE = TOTALE + BOLLO
                                    ReDim Preserve xlsVoci(xlsIndice)
                                    ReDim Preserve xlsImporti(xlsIndice)
                                    xlsVoci(xlsIndice) = "BOLLO"
                                    xlsImporti(xlsIndice) = Format(BOLLO, "##,##0.00")
                                    xlsIndice = xlsIndice + 1
                                    If sSimulazione <> "1" Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                            & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                End If
                            End If
                            IntestazioneBolletta = IntestazioneBolletta & vbCrLf
                            IntestazioneBolletta = IntestazioneBolletta & "TOTALE".ToString.PadRight(40) & " " & Format(TOTALE, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                            IntestazioneBolletta = IntestazioneBolletta & vbCrLf & "--------------------------------------------------------" & vbCrLf
                            If rbCSV.Checked = True Then
                                For xlsI = 0 To xlsIndice - 1
                                    srCSV.WriteLine(xlsCodContratto & ";" & xlsTipoContratto & ";" & xlsNominativo & ";" & xlsIndirizzoUN & ";" & xlsCapUN & ";" & xlsLocalitaUN & ";" & xlsPianoUN & ";" & xlsInternoUN & ";" & xlsPresso & ";" & xlsIndirizzoCorr & ";" & xlsCapCorr & ";" & xlsLuogoCor & ";" & xlsDataDecorrenza & ";" & xlsDataDisdetta & ";" & xlsDataSloggio & ";" & xlsDataCalcolo & ";" & xlsAreaCanone & ";" & xlsClasse & ";" & xlsValiditaCan & ";" & xlsProvenienza & ";" & xlsInteressiC & ";" & xlsInteressiCong & ";" & xlsInvioBoll & ";" & xlsInteressiRIT & ";" & xlsNumBolletta & ";" & xlsDataEmissione & ";" & xlsDataScadenza & ";" & xlsPeriodoRif & ";" & CDbl(Format(TOTALE, "0.00")) & ";" & xlsVoci(xlsI) & ";" & xlsImporti(xlsI) & ";" & xlsUltimoImporto & ";" & xlsUltimoRiferimento & ";")
                                    RigaXLS = RigaXLS + 1
                                Next
                            End If
                            If sSimulazione = "1" Then
                                For xlsI = 0 To xlsIndice - 1
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EXPORT_SIMULAZIONE (ID,TESTO,IDENTIFICATIVO) VALUES ('" & IndiceExport & "','" & par.PulisciStrSql(xlsCodContratto & ";" & xlsTipoContratto & ";" & xlsNominativo & ";" & xlsIndirizzoUN & ";" & xlsCapUN & ";" & xlsLocalitaUN & ";" & xlsPianoUN & ";" & xlsInternoUN & ";" & xlsPresso & ";" & xlsIndirizzoCorr & ";" & xlsCapCorr & ";" & xlsLuogoCor & ";" & xlsDataDecorrenza & ";" & xlsDataDisdetta & ";" & xlsDataSloggio & ";" & xlsDataCalcolo & ";" & xlsAreaCanone & ";" & xlsClasse & ";" & xlsValiditaCan & ";" & xlsProvenienza & ";" & xlsInteressiC & ";" & xlsInteressiCong & ";" & xlsInvioBoll & ";" & xlsInteressiRIT & ";" & xlsNumBolletta & ";" & xlsDataEmissione & ";" & xlsDataScadenza & ";" & xlsPeriodoRif & ";" & CDbl(Format(TOTALE, "0.00")) & ";" & xlsVoci(xlsI) & ";" & xlsImporti(xlsI) & ";" & xlsUltimoImporto & ";" & xlsUltimoRiferimento & ";") & "','" & smNomeFile & "')"
                                    par.cmd.ExecuteNonQuery()
                                    IndiceExport = IndiceExport + 1
                                Next
                            End If
                        Else
                            If TOTALE < 0 Then
                                'in caso di bolletta negativa emetto comunque, eliminando spese mav e postali
                                If sSimulazione <> "1" Then
                                    'in caso di bolletta negativa emetto comunque, eliminando spese mav e postali
                                    par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci where id_voce in (407,403) and id_bolletta=" & ID_BOLLETTA
                                    par.cmd.ExecuteNonQuery()
                                End If
                                IntestazioneBolletta = IntestazioneBolletta & vbCrLf
                                IntestazioneBolletta = IntestazioneBolletta & "TOTALE".ToString.PadRight(40) & " " & Format(TOTALE - SPESEPOSTALI_DA_APPLICARE - SPESEmav, "##,##0.00").ToString.PadLeft(15) & vbCrLf
                                IntestazioneBolletta = IntestazioneBolletta & vbCrLf & "--------------------------------------------------------" & vbCrLf
                                TOTALE = TOTALE + -SPESEPOSTALI_DA_APPLICARE - SPESEmav
                                If rbCSV.Checked = True Then
                                    For xlsI = 0 To xlsIndice - 1
                                        If xlsVoci(xlsI) <> "SPESE POSTALI" And xlsVoci(xlsI) <> "SPESE MAV" Then
                                            srCSV.WriteLine(xlsCodContratto & ";" & xlsTipoContratto & ";" & xlsNominativo & ";" & xlsIndirizzoUN & ";" & xlsCapUN & ";" & xlsLocalitaUN & ";" & xlsPianoUN & ";" & xlsInternoUN & ";" & xlsPresso & ";" & xlsIndirizzoCorr & ";" & xlsCapCorr & ";" & xlsLuogoCor & ";" & xlsDataDecorrenza & ";" & xlsDataDisdetta & ";" & xlsDataSloggio & ";" & xlsDataCalcolo & ";" & xlsAreaCanone & ";" & xlsClasse & ";" & xlsValiditaCan & ";" & xlsProvenienza & ";" & xlsInteressiC & ";" & xlsInteressiCong & ";" & xlsInvioBoll & ";" & xlsInteressiRIT & ";" & xlsNumBolletta & ";" & xlsDataEmissione & ";" & xlsDataScadenza & ";" & xlsPeriodoRif & ";" & CDbl(Format(TOTALE, "0.00")) & ";" & xlsVoci(xlsI) & ";" & xlsImporti(xlsI) & ";" & xlsUltimoImporto & ";" & xlsUltimoRiferimento & ";")
                                            RigaXLS = RigaXLS + 1
                                        End If
                                    Next
                                End If
                                If sSimulazione = "1" Then
                                    For xlsI = 0 To xlsIndice - 1
                                        If xlsVoci(xlsI) <> "SPESE POSTALI" And xlsVoci(xlsI) <> "SPESE MAV" Then
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EXPORT_SIMULAZIONE (ID,TESTO,IDENTIFICATIVO) VALUES ('" & IndiceExport & "','" & par.PulisciStrSql(xlsCodContratto & ";" & xlsTipoContratto & ";" & xlsNominativo & ";" & xlsIndirizzoUN & ";" & xlsCapUN & ";" & xlsLocalitaUN & ";" & xlsPianoUN & ";" & xlsInternoUN & ";" & xlsPresso & ";" & xlsIndirizzoCorr & ";" & xlsCapCorr & ";" & xlsLuogoCor & ";" & xlsDataDecorrenza & ";" & xlsDataDisdetta & ";" & xlsDataSloggio & ";" & xlsDataCalcolo & ";" & xlsAreaCanone & ";" & xlsClasse & ";" & xlsValiditaCan & ";" & xlsProvenienza & ";" & xlsInteressiC & ";" & xlsInteressiCong & ";" & xlsInvioBoll & ";" & xlsInteressiRIT & ";" & xlsNumBolletta & ";" & xlsDataEmissione & ";" & xlsDataScadenza & ";" & xlsPeriodoRif & ";" & CDbl(Format(TOTALE, "0.00")) & ";" & xlsVoci(xlsI) & ";" & xlsImporti(xlsI) & ";" & xlsUltimoImporto & ";" & xlsUltimoRiferimento) & "','" & smNomeFile & "')"
                                            par.cmd.ExecuteNonQuery()
                                            IndiceExport = IndiceExport + 1
                                        End If
                                    Next
                                End If
                            Else
                                IntestazioneBolletta = ""
                                If sSimulazione <> "1" Then
                                    par.cmd.CommandText = "delete from siscom_mi.bol_bollette_gest where id_bolletta=" & ID_BOLLETTA
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "delete from siscom_mi.bol_bollette where id=" & ID_BOLLETTA
                                    par.cmd.ExecuteNonQuery()
                                End If
                                TOTALE = 0
                            End If

                            End If
                        sr.WriteLine(IntestazioneBolletta)

                        xlsIndice = 0



                        IntestazioneBolletta = ""

                        Select Case Mid(par.IfNull(myReader("COD_TIPOLOGIA_CONTR_LOC"), "   "), 1, 3)
                            Case "ERP"
                                importoerp = importoerp + TOTALE
                                If TOTALE > 0 Then
                                    TOTALE_ERP = TOTALE_ERP + 1
                                End If
                            Case "USD"
                                importoUSD = importoUSD + TOTALE
                                If TOTALE > 0 Then
                                    TOTALE_USD = TOTALE_USD + 1
                                End If
                            Case "L43"
                                importo431 = importo431 + TOTALE
                                If TOTALE > 0 Then
                                    TOTALE_431 = TOTALE_431 + 1
                                End If
                            Case "EQC"
                                importo392 = importo392 + TOTALE
                                If TOTALE > 0 Then
                                    TOTALE_392 = TOTALE_392 + 1
                                End If
                            Case "NON"
                                importoNONE = importoNONE + TOTALE
                                If TOTALE > 0 Then
                                    TOTALE_NONE = TOTALE_NONE + 1
                                End If
                            Case Else
                                importoAltri = importoAltri + TOTALE
                                If TOTALE > 0 Then
                                    TOTALE_ALTRI = TOTALE_ALTRI + 1
                                End If
                        End Select

                        TOTALE = 0

                        If sSimulazione <> "1" Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET INIZIO_PERIODO='" & sProssimo_Periodo & "' WHERE ID=" & myReader("ID")
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_PROSSIMA_BOL SET PROSSIMA_BOLLETTA='" & sProssimo_Periodo & "' WHERE ID_CONTRATTO=" & myReader("ID")
                            par.cmd.ExecuteNonQuery()
                            'RAPPORTI_UTENZA_PROSSIMA_BOL

                        End If
                    Else

                    End If
                Loop
                myReader.Close()

                If rbCSV.Checked = True Then
                    srCSV.Close()
                End If

                If sSimulazione <> "1" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE='" & cmbMese.SelectedItem.Value & "' WHERE ID=5"
                    par.cmd.ExecuteNonQuery()
                End If

                sr.WriteLine(" ")
                sr.WriteLine("Totale Bollette Emesse: " & bolletteemesse)
                sr.WriteLine("Totale Importo: " & Format(importoerp + importoAltri + importo392 + importo431 + importoNONE + importoUSD, "##,##0.00") & " Euro")
                sr.WriteLine(" ")
                sr.WriteLine("Di cui ERP: " & TOTALE_ERP)
                sr.WriteLine("Di cui USI DIVERSI: " & TOTALE_USD)
                sr.WriteLine("Di cui 431/98: " & TOTALE_431)
                sr.WriteLine("Di cui 392/78: " & TOTALE_392)
                sr.WriteLine("Di cui ABUSIVI: " & TOTALE_NONE)
                sr.WriteLine("Di cui ALTRI: " & TOTALE_ALTRI)
                sr.WriteLine(" ")
                sr.WriteLine("Totale ERP:" & Format(importoerp, "##,##0.00") & " Euro")
                sr.WriteLine("Totale USI DIVERSI:" & Format(importoUSD, "##,##0.00") & " Euro")
                sr.WriteLine("Totale 431/98:" & Format(importo431, "##,##0.00") & " Euro")
                sr.WriteLine("Totale 392/78:" & Format(importo392, "##,##0.00") & " Euro")
                sr.WriteLine("Totale ABUSIVI:" & Format(importoNONE, "##,##0.00") & " Euro")
                sr.WriteLine("Totale ALTRI:" & Format(importoAltri, "##,##0.00") & " Euro")
                sr.Close()

                'cambio la data di scadenza delle eventuali bollette di fine contratto che andranno nella bollettazione massiva
                'par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE set data_scadenza='" & ScadenzaPagamento & "' WHERE SISCOM_MI.BOL_BOLLETTE.FL_STAMPATO='0' AND SISCOM_MI.BOL_BOLLETTE.FL_ANNULLATA='0' AND SISCOM_MI.BOL_BOLLETTE.RIF_FILE = 'FIN' AND BOL_BOLLETTE.IMPORTO_TOTALE>0 AND NVL(ID_BOLLETTA_STORNO,0)=0 AND NVL(ID_MOROSITA,0)=0 AND NVL(ID_BOLLETTA_RIC,0)=0"
                'TOLGO MOD  e metto ID_TIPO=3
                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE set data_scadenza='" & ScadenzaPagamento & "' WHERE SISCOM_MI.BOL_BOLLETTE.FL_STAMPATO='0' AND SISCOM_MI.BOL_BOLLETTE.FL_ANNULLATA='0' AND SISCOM_MI.BOL_BOLLETTE.ID_TIPO = 3 AND BOL_BOLLETTE.IMPORTO_TOTALE>0 AND NVL(ID_BOLLETTA_STORNO,0)=0 AND NVL(ID_MOROSITA,0)=0 AND NVL(ID_BOLLETTA_RIC,0)=0"
                par.cmd.ExecuteNonQuery()
                '-------------------------------------------

                If Contatore > 0 Then

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String

                    zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\" & smNomeFile & ".zip")

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    '
                    Dim strFile As String
                    strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\" & smNomeFile & ".txt")
                    Dim strmFile As FileStream = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    '
                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                    Dim sFile As String = Path.GetFileName(strFile)
                    Dim theEntry As ZipEntry = New ZipEntry(sFile)
                    Dim fi As New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                    File.Delete(strFile)

                    If rbCSV.Checked = True Then
                        strFile = Server.MapPath("..\FileTemp\" & smNomeFile & ".csv")
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                        sFile = Path.GetFileName(strFile)
                        Dim theEntry1 As ZipEntry = New ZipEntry(sFile)
                        Dim fi1 As New FileInfo(strFile)
                        theEntry1.DateTime = fi1.LastWriteTime
                        theEntry1.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer1)
                        theEntry1.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry1)
                        strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                        File.Delete(strFile)
                    End If
                    

                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                    myReaderBOL.Close()
                    If sSimulazione <> "1" Then
                        par.myTrans.Commit()
                    End If
                    par.cmd.Dispose()
                    par.OracleConn.Close()

                    If sSimulazione <> "1" Then
                        Response.Write("<script>location.href='ElencoEmissioni.aspx';</script>")
                    Else
                        Response.Write("<script>location.href='ElencoSimulazioni.aspx?T=SIMULAZIONE';</script>")
                    End If
                Else
                    Response.Write("<script>alert('Nessuna Bolletta emessa!');</script>")
                End If
            Else
                myReaderBOL.Close()
                Dim ELENCO As String = ""
                Do While myReaderIND.Read
                    ELENCO = ELENCO & myReaderIND("COD_CONTRATTO") & vbCrLf
                Loop
                myReaderIND.Close()
                Response.Write("<script>alert('Non è possibile emettere le bollette. I rapporti elencati in maschera hanno un indirizzo di spedizione errato o mancante! Risolvere il problema prima di proseguire.');</script>")
                lblerrore.Visible = True
                lblerrore.Text = "Rapporti con indirizzo di spedizione errato o mancante: " & vbCrLf & ELENCO
                If sSimulazione <> "1" Then
                    par.myTrans.Commit()
                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException

            If EX1.Number = 54 Then
                If sSimulazione <> "1" Then
                    par.myTrans.Rollback()
                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                Response.Write("<script>alert('Attenzione, non è possibile procedere perchè uno o più contratti potrebbero essere aperti da utenti!\nContattare l\'amministratore del sistema.');</script>")
            Else
                myReaderBOL.Close()
                If sSimulazione <> "1" Then
                    par.myTrans.Rollback()
                End If
                par.cmd.Dispose()
                par.OracleConn.Close()
                If sSimulazione <> "1" Then
                    Session.Add("ERRORE", "Provenienza:Emissione Bollette - " & EX1.Message)
                Else
                    Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & EX1.Message)
                End If
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            myReaderBOL.Close()
            If sSimulazione <> "1" Then
                par.myTrans.Rollback()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            If sSimulazione <> "1" Then
                Session.Add("ERRORE", "Provenienza:Emissione Bollette - " & ex.Message)
            Else
                Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
            End If
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        Try


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbEdificio.Items.Clear()
            cmbUnita.Items.Clear()

            cmbEdificio.Items.Add(New ListItem("TUTTI", -1))
            cmbUnita.Items.Add(New ListItem("TUTTI", -1))

            par.cmd.CommandText = "SELECT distinct(id),denominazione FROM SISCOM_MI.edifici where id_complesso=" & cmbComplesso.SelectedValue & " order by denominazione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbUnita.Items.Clear()
            cmbUnita.Items.Add(New ListItem("TUTTI", -1))

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.unita_immobiliari where id_unita_principale is null and id_edificio=" & cmbEdificio.SelectedValue & " order by cod_unita_immobiliare asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbUnita.Items.Add(New ListItem(par.IfNull(myReader1("COD_UNITA_IMMOBILIARE"), " "), par.IfNull(myReader1("ID"), "-1")))
            End While
            myReader1.Close()
            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Simulazione Bollette - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub cmbMese_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMese.SelectedIndexChanged

    End Sub
End Class
