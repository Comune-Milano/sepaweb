Imports System.Collections.Generic
Imports System.IO

Partial Class CreaBollFIN
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public percentuale As Long = 0

    Protected Sub btnCreaFIN_Click(sender As Object, e As System.EventArgs) Handles btnCreaFIN.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            Dim percorso As String = Server.MapPath("FileTemp")
            Dim FileName As String = ""
            Dim errore As String = ""
            Dim NUMERORIGHE As Long = 0
            Dim Contatore As Long = 0
            Dim bollDaEmettere As Boolean = False
            If FileUpload1.HasFile = True Then
                FileName = percorso & "\" & FileUpload1.FileName

                FileUpload1.SaveAs(FileName)
                If System.IO.File.Exists(FileName) = True Then
                    Dim sr = New StreamReader(FileName)

                    NUMERORIGHE = File.ReadAllLines(FileName).Length
                    Dim idContratto As String = ""
                    Do
                        Contatore = Contatore + 1
                        percentuale = (Contatore * 100) / NUMERORIGHE

                        idContratto = sr.ReadLine

                        If idContratto <> "" Then
                            par.cmd.CommandText = "SELECT * from siscom_mi.rapporti_utenza where rapporti_utenza.ID=" & idContratto & "" _
                                & "  AND NOT EXISTS(SELECT id_contratto FROM " _
                                & " siscom_mi.bol_bollette WHERE bol_bollette.id_tipo IN (3) AND (FL_ANNULLATA = 0 OR (FL_ANNULLATA = 1 AND IMPORTO_PAGATO IS NOT NULL))" _
								& "	AND id_bolletta_storno is null AND id_Contratto = rapporti_utenza.id)"
                            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt1 As New Data.DataTable
                            da1.Fill(dt1)
                            da1.Dispose()
                            If dt1.Rows.Count > 0 Then
                                For Each rowDT In dt1.Rows
                                    bollDaEmettere = True
                                    CreaBollettaFIN(rowDT.item("ID"), errore)
                                Next
                            End If
                        End If
                        percentuale = (Contatore * 100) / NUMERORIGHE
                        Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                        Response.Flush()
                    Loop Until idContratto Is Nothing
                    sr.Close()

                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                Else
                    Response.Write("<script>alert('File non trovato!')</script>")
                End If
                If bollDaEmettere = False Then
                    Response.Write("<script>alert('Nessuna bolletta creata. Verificare gli ID contratto')</script>")
                Else
                    Response.Write("<script>alert('Operazione effettuata!')</script>")
                End If
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = " <div id='divLoading' style='position: absolute; margin: 0px; width: 100%; height: 100%;" _
                & " top: 0px; left: 0px; background-color: #ffffff; z-index: 1000;'>" _
                & " <div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px;" _
                & " margin-left: -117px; margin-top: -48px; background-image: url('NuoveImm/sfondo.png');"">" _
                & " <table style='width: 100%; height: 100%;'>" _
                & " <tr>" _
                & " <td valign='middle' align='center'>" _
                & " <img src='NuoveImm/load.gif' alt='Elaborazione in corso' /><p style='font-family: Arial;font-size: 8pt;'>" _
                & " Elaborazione in corso...</p>" _
                & " <div align='left' id='AA' style='background-color: #FFFFFF; border: none;width:100px;'>" _
                & " <img alt='' src='Contratti/barra.gif' id='barra' height='10' width='100' /></div>" _
                & " </td>" _
                & " </tr>" _
                & " <tr>" _
                & " <td style='text-align: center;border: none;'>" _
                & " <input id='txtpercent' value='' type='text' style='width: 35px; font-family: Arial, Helvetica, sans-serif;" _
                & "  font-size: 8pt;border: none; ' />" _
                & " </td>" _
                & " </tr>" _
                & " </table>" _
                & " </div>" _
                & " </div>" _
                & " <script  language='javascript' type='text/javascript'>var tempo; tempo=0; function Mostra()" _
                & " {document.getElementById('barra').style.width = tempo + 'px';document.getElementById('txtpercent').value = tempo + '%'};setInterval('Mostra();', 100);</script>"

        Response.Write(Loading)
        Response.Flush()

    End Sub


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

    Private Sub CaricaCapoluoghi()
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
    End Sub
    Function CreaBollettaFIN(ByVal lIdContratto As Long, ByRef errore As String) As Boolean
        CreaBollettaFIN = False

        errore = "BollettaFIN"

        '***

        Dim DESTINAZIONE As String = ""

        Dim Risoluzione As Boolean = False
        Dim importoRisoluzione As Double = 0
        Dim IMPORTOINTERESSI As Double = 0
        Dim sAggiunta As String = ""
        Dim DataCalcolo As String = ""
        Dim DataInizio As String = ""

        Dim tasso As Double = 0
        Dim baseCalcolo As Double = 0

        Dim Giorni As Integer = 0
        Dim GiorniAnno As Integer = 0
        Dim dataPartenza As String = ""

        Dim Totale As Double = 0
        Dim TotalePeriodo As Double = 0
        Dim indice As Long = 0
        Dim DataFine As String = ""
        Dim num_bolletta As String = ""
        Dim importobolletta As Double = 0
        Dim I As Integer = 0
        Dim virtuale As Boolean = False
        Dim importodanni As Double = 0
        Dim importotrasporto As Double = 0

        Dim Interessi As New SortedDictionary(Of Integer, Double)
        CaricaCapoluoghi()
        Dim RIASSUNTO_BOLLETTA As String = ""
        Dim INDICEANAGRAFICA As Long = 0
        par.cmd.CommandText = "SELECT id_unita,rapporti_utenza.* from siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale where rapporti_utenza.id=unita_contrattuale.id_contratto and id_unita_principale is null and rapporti_utenza.ID=" & lIdContratto
        Dim daContratto As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtContratto As New Data.DataTable
        daContratto.Fill(dtContratto)
        daContratto.Dispose()

        If Mid(dtContratto.Rows(0).Item("cod_contratto"), 1, 6) = "000000" Then
            virtuale = True
        End If

        par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=1"
        Dim myReaderAa As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderAa.Read Then
            importoRisoluzione = CDbl(par.PuntiInVirgole(myReaderAa("valore")))
        End If
        myReaderAa.Close()

        If virtuale = False Then
            par.cmd.CommandText = "select * from siscom_mi.unita_stato_manutentivo where id_unita=" & par.IfNull(dtContratto.Rows(0).Item("id_unita"), "null")
            myReaderAa = par.cmd.ExecuteReader()
            If myReaderAa.Read Then
                If par.IfNull(myReaderAa("FL_AUTORIZZATI_IMP"), "0") = "0" Then

                Else
                    importodanni = par.IfNull(myReaderAa("importo_danni"), 0)
                    importotrasporto = par.IfNull(myReaderAa("importo_trasporto"), 0)
                End If
            End If
            myReaderAa.Close()
            par.cmd.CommandText = "select * from siscom_mi.sl_sloggio where ID_UNITA_IMMOBILIARE = " & par.IfNull(dtContratto.Rows(0).Item("id_unita"), "null") & " and id_contratto = " & lIdContratto & ""
            myReaderAa = par.cmd.ExecuteReader()
            If myReaderAa.Read Then
                importodanni = importodanni + par.IfNull(myReaderAa("TOT_RAPPORTO_SLOGGIO"), 0)
            End If
            myReaderAa.Close()
        End If

        If par.IfNull(dtContratto.Rows(0).Item("provenienza_ass"), "null") = "7" Then
            importoRisoluzione = 0
        End If


        par.cmd.CommandText = "select * from siscom_mi.tab_interessi_legaLI order by anno desc"
        Interessi.Clear()
        Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Do While myReaderC.Read
            Interessi.Add(myReaderC("anno"), myReaderC("tasso"))
        Loop
        myReaderC.Close()

        DataCalcolo = par.IfNull(dtContratto.Rows(0).Item("data_riconsegna"), "29991231")

        baseCalcolo = par.IfNull(dtContratto.Rows(0).Item("imp_deposito_cauz"), 0)
        If baseCalcolo > 0 And par.IfNull(dtContratto.Rows(0).Item("interessi_cauzione"), 0) = 1 Then

            par.cmd.CommandText = "select * from siscom_mi.adeguamento_interessi where id_contratto=" & lIdContratto & " order by id desc"
            Dim myReaderZ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderZ.HasRows = False Then
                DataInizio = par.IfNull(dtContratto.Rows(0).Item("data_decorrenza"), "29991231")
            End If
            If myReaderZ.Read Then
                DataInizio = Format(DateAdd(DateInterval.Day, 1, CDate(par.FormattaData(myReaderZ("data")))), "yyyyMMdd")
            End If
            myReaderZ.Close()

            If DataInizio < "20091001" Then
                DataInizio = "20091001"
            End If

            Giorni = 0
            GiorniAnno = 0
            dataPartenza = DataInizio
            Totale = 0
            TotalePeriodo = 0

            par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi (id,id_contratto,data,fl_applicato) values (siscom_mi.seq_adeguamento_interessi.nextval," & lIdContratto & ",'" & DataCalcolo & "',1)"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "select siscom_mi.seq_adeguamento_interessi.currval from dual"
            myReaderZ = par.cmd.ExecuteReader()
            indice = 0
            If myReaderZ.Read Then
                indice = myReaderZ(0)
            End If

            For I = CInt(Mid(DataInizio, 1, 4)) To CInt(Mid(DataCalcolo, 1, 4))

                If I = CInt(Mid(DataCalcolo, 1, 4)) Then
                    DataFine = par.FormattaData(DataCalcolo)
                Else
                    DataFine = "31/12/" & I

                End If

                GiorniAnno = DateDiff(DateInterval.Day, CDate("01/01/" & I), CDate("31/12/" & I)) + 1

                Giorni = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataPartenza)), CDate(DataFine)) + 1

                If I < 1990 Then
                    tasso = 5
                Else
                    If Interessi.ContainsKey(I) = True Then
                        tasso = Interessi(I)
                    End If
                End If

                TotalePeriodo = Format((((baseCalcolo * tasso) / 100) / GiorniAnno) * Giorni, "0.00")
                Totale = Totale + TotalePeriodo



                par.cmd.CommandText = "insert into siscom_mi.adeguamento_interessi_voci (id_adeguamento,dal,al,giorni,tasso,importo) values (" & indice & ",'" & dataPartenza & "','" & Format(CDate(DataFine), "yyyyMMdd") & "'," & Giorni & "," & par.VirgoleInPunti(tasso) & "," & par.VirgoleInPunti(TotalePeriodo) & ")"
                par.cmd.ExecuteNonQuery()

                dataPartenza = I + 1 & "0101"

            Next
            par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set importo=" & par.VirgoleInPunti(Format(Totale, "0.00")) & " where id=" & indice
            par.cmd.ExecuteNonQuery()
        End If



        par.cmd.CommandText = "select sum(importo) as TotInteressi from siscom_mi.adeguamento_interessi where id_contratto=" & lIdContratto & " and fl_applicato=0 order by id desc"
        Dim myReaderZ2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderZ2.Read Then
            Totale = Totale + par.IfNull(myReaderZ2("TotInteressi"), 0)
        End If
        myReaderZ2.Close()

        par.cmd.CommandText = "UPDATE siscom_mi.adeguamento_interessi SET FL_APPLICATO=1 where id_contratto=" & lIdContratto & " and fl_applicato=0"
        par.cmd.ExecuteNonQuery()

        Dim DataScadenza As String = DateAdd("d", 10, Now)
        Dim TOTALE_BOLLETTA As Double = 0



        par.cmd.CommandText = "select anagrafica.id as ida,anagrafica.cognome,anagrafica.nome,ANAGRAFICA.RAGIONE_SOCIALE,RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & lIdContratto & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderS.Read Then


            Dim PRESSO As String = UCase(Trim(par.IfNull(myReaderS("PRESSO_COR"), "")))
            Dim Cognome As String = ""
            Dim Nome As String = ""

            If par.IfNull(myReaderS("ragione_sociale"), "") <> "" Then
                Cognome = par.IfNull(myReaderS("ragione_sociale"), "")
                Nome = ""
            Else
                Cognome = par.IfNull(myReaderS("cognome"), "")
                Nome = par.IfNull(myReaderS("nome"), "")
            End If

            If UCase(Trim(Cognome & " " & Nome)) = PRESSO Then
                PRESSO = ""
            End If
            INDICEANAGRAFICA = par.IfNull(myReaderS("IDA"), 0)

            par.cmd.CommandText = "update siscom_mi.adeguamento_interessi set id_anagrafica=" & INDICEANAGRAFICA & " where id=" & indice
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                                            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                                            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                            & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                                            & "Values " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                                                            & "', '" & Format(CDate(DataScadenza), "yyyyMMdd") & "', NULL,NULL,NULL,'BOLLETTA FINE CONTRATTO'," _
                                                            & "" & lIdContratto _
                                                            & " ," & par.RicavaEsercizioCorrente & ", " _
                                                            & par.IfNull(dtContratto.Rows(0).Item("id_unita"), "null") _
                                                            & ", '0', '', " & par.IfNull(myReaderS("IDA"), 0) _
                                                            & ", '" & par.PulisciStrSql(Trim(Cognome & " " & Nome)) & "', " _
                                                            & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                                                            & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                                                            & "', '" & par.PulisciStrSql(PRESSO) & "', '" & par.IfNull(dtContratto.Rows(0).Item("data_riconsegna"), "29991231") _
                                                            & "', '" & par.IfNull(dtContratto.Rows(0).Item("data_riconsegna"), "29991231") & "', " _
                                                            & "'0', " & myReaderS("ID_COMPLESSO") & ", '', NULL, '', " _
                                                            & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ", NULL, NULL,'FIN',3)"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                Dim ID_BOLLETTA As Long = myReaderA(0)

                If importoRisoluzione > 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                                        & ",35" _
                                                        & "," & par.VirgoleInPunti(importoRisoluzione) & ")"
                    RIASSUNTO_BOLLETTA = "Risoluzione: " & par.VirgoleInPunti(importoRisoluzione) & " Euro;\n"
                    par.cmd.ExecuteNonQuery()

                    TOTALE_BOLLETTA = importoRisoluzione
                End If

                If importodanni > 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                                        & ",100" _
                                                        & "," & par.VirgoleInPunti(importodanni) & ")"
                    RIASSUNTO_BOLLETTA = "Risoluzione: " & par.VirgoleInPunti(importodanni) & " Euro;\n"
                    par.cmd.ExecuteNonQuery()

                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + importodanni
                End If

                If importotrasporto > 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                                        & ",101" _
                                                        & "," & par.VirgoleInPunti(importotrasporto) & ")"
                    RIASSUNTO_BOLLETTA = "Risoluzione: " & par.VirgoleInPunti(importotrasporto) & " Euro;\n"
                    par.cmd.ExecuteNonQuery()

                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + importotrasporto
                End If


                If Totale > 0 And par.IfNull(dtContratto.Rows(0).Item("interessi_cauzione"), 0) = 1 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,note) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA _
                                                        & ",15" _
                                                        & "," & par.VirgoleInPunti(Format(Totale * -1, "0.00")) & ",'Dal " & par.FormattaData(DataInizio) & "')"
                    par.cmd.ExecuteNonQuery()
                    RIASSUNTO_BOLLETTA = RIASSUNTO_BOLLETTA & "Rimborso Interessi Annuo Cauzione: " & par.VirgoleInPunti(Format(Totale * -1, "0.00")) & " Euro;\n"
                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + (Totale * -1)

                    'MAX 29/11/2018 SCRITTURA IN GESTIONALE
                    Dim ID_BOLLETTA_GEST As Long = 0
                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA, RIFERIMENTO_A, IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE, ID_EVENTO_PAGAMENTO,ID_ADEGUAMENTO,ID_BOLLETTA) " _
                                        & " Values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & lIdContratto & ", " & par.RicavaEsercizioCorrente & ", " & par.IfNull(dtContratto.Rows(0).Item("id_unita"), "null") & "," _
                                        & INDICEANAGRAFICA & ", '" & DataInizio & "', '" & Format(Now, "yyyyMMdd") _
                                        & "', " & par.VirgoleInPunti(Format(Totale * -1, "0.00")) & ", '" & Format(Now, "yyyyMMdd") & "',NULL, NULL, 57, 'T', NULL, " _
                                        & "'', '" & par.PulisciStrSql("REST. INTERESSI DEP.CAUZIONALE ") & "', NULL," & indice & "," & ID_BOLLETTA & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        ID_BOLLETTA_GEST = myReaderX(0)
                        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) Values  (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & ID_BOLLETTA_GEST & ",15, " & par.VirgoleInPunti(Format(Totale * -1, "0.00")) & ",  NULL, NULL)"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "INSERT INTO OPERAZIONI_PART_GEST (DATA_ORA,ID_CONTRATTO,OPERAZIONE,NOTE,ID_BOLLETTA,ID_BOLLETTA_GEST) VALUES ('" & Format(Now, "yyyyMMddHHmmss") & "'," & lIdContratto & ",'RIMBORSO INTERESSI DEPOSITO CAUZIONALE','CREATA IN P.GEST. BOLLETTA NUM." & par.PulisciStrSql(ID_BOLLETTA_GEST) & " PER FINE CONTRATTO MASSIVO',NULL," & ID_BOLLETTA_GEST & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                    myReaderX.Close()

                End If

                Dim Giorni1 As Integer = 0
                Dim Giorni2 As Integer = 0
                Dim SPESEmav As Double = 0
                Dim BOLLO As Double = 0
                Dim APPLICABOLLO As Double = 0

                Dim PARTENZA As String = ""
                Dim FINE As String = ""


                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

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

                Dim SPESEPOSTALI As Double = 0
                Dim SPESEPOSTALI_CAPOLUOGHI As Double = 0
                Dim SPESEPOSTALI_ALTRI As Double = 0
                Dim SPESEPOSTALI_DA_APPLICARE As Double = 0

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

                If UCase(par.IfNull(dtContratto.Rows(0).Item("luogo_Cor"), "")) = "MILANO" Then
                    SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI
                Else
                    If InStr(Capoluoghi, UCase(par.IfNull(dtContratto.Rows(0).Item("luogo_Cor"), ""))) > 0 Then
                        SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI_CAPOLUOGHI
                    Else
                        SPESEPOSTALI_DA_APPLICARE = SPESEPOSTALI_ALTRI
                    End If
                End If

                'MAX 08/11/2018 Visto che su alcuni contratti vengono stornate numerose bollette ordinarie e poi riemesse 
                'ma di tipo MANUALE viene cambiata la query in modo da prendere tutte le bollette valide, non stornate e non di storno
                'par.cmd.CommandText = "select * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_BOLLETTA_STORNO IS NULL AND FL_ANNULLATA='0' AND N_RATA<>99 AND N_RATA<>999 AND N_RATA<>99999 AND ID_CONTRATTO=" & lIdContratto & " ORDER BY ID DESC"
                par.cmd.CommandText = "select max(riferimento_a) FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_TIPO<>22 AND id_tipo=1 and ID_BOLLETTA_STORNO IS NULL AND FL_ANNULLATA='0' AND ID_CONTRATTO=" & lIdContratto & " "
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    PARTENZA = myReaderA(0)
                End If
                myReaderA.Close()

                FINE = par.IfNull(dtContratto.Rows(0).Item("data_riconsegna"), "29991231")

                If PARTENZA <> "" And FINE <> "" Then
                    Giorni1 = DateDiff("d", CDate(par.FormattaData(PARTENZA)), CDate(par.FormattaData(FINE)))


                    If Giorni1 > 0 Then
                        'calcolo l'importo dell'affitto in base al numero di rate annue

                        Dim affitto As Double = 0

                        If par.IfNull(myReaderS("imp_CANONE_INIZIALE"), 0) > 0 Then
                            affitto = Format((par.IfNull(myReaderS("imp_CANONE_INIZIALE"), 1) / 365) * (Giorni1), "0.00")
                            TOTALE_BOLLETTA = TOTALE_BOLLETTA + (par.IfNull(myReaderS("imp_CANONE_INIZIALE"), 1) / 365) * (Giorni1)

                            If par.IfNull(myReaderS("PROVENIENZA_ASS"), "") <> "7" Then
                                If FINE = par.IfNull(myReaderS("data_scadenza_rinnovo"), "") Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",36" _
                                                                        & "," & par.VirgoleInPunti(affitto) & ")"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",1" _
                                                                        & "," & par.VirgoleInPunti(affitto) & ")"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",560" _
                                                    & "," & par.VirgoleInPunti(affitto) & ")"
                                par.cmd.ExecuteNonQuery()
                            End If


                        End If
                    End If
                End If

                If Giorni1 > 0 Then
                    Dim aggiornamento_istat As Double = 0
                    Dim AltriAdeguamenti As Double = 0

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo=2 and ID_CONTRATTO=" & lIdContratto
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        aggiornamento_istat = par.IfNull(myReaderA(0), 0)
                    End If
                    myReaderA.Close()

                    par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE id_motivo<>2 and ID_CONTRATTO=" & lIdContratto
                    myReaderA = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        AltriAdeguamenti = par.IfNull(myReaderA(0), 0)
                    End If
                    myReaderA.Close()

                    If aggiornamento_istat > 0 Then

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + Format((aggiornamento_istat / 365) * Giorni1, "0.00")
                        aggiornamento_istat = (aggiornamento_istat / 365) * Giorni1

                        If FINE = par.IfNull(myReaderS("data_scadenza_rinnovo"), "") Or par.IfNull(myReaderS("PROVENIENZA_ASS"), "") = "7" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",405" _
                                                                & "," & par.VirgoleInPunti(Format(aggiornamento_istat, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",404" _
                                                                & "," & par.VirgoleInPunti(Format(aggiornamento_istat, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If

                    If AltriAdeguamenti > 0 Then

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + Format((AltriAdeguamenti / 365) * Giorni1, "0.00")
                        AltriAdeguamenti = (AltriAdeguamenti / 365) * Giorni1


                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",652" _
                                                            & "," & par.VirgoleInPunti(Format(AltriAdeguamenti, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()

                    End If

                    par.cmd.CommandText = "select bol_schema.*,t_voci_bolletta.descrizione from siscom_mi.bol_schema,siscom_mi.t_voci_bolletta where anno=" & Year(Now) & " and da_rata=1 And per_rate=12 AND t_voci_bolletta.id=bol_schema.id_voce and  bol_schema.id_contratto=" & lIdContratto
                    myReaderA = par.cmd.ExecuteReader()
                    Do While myReaderA.Read

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl((myReaderA("importo") / 365) * Giorni1)

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & myReaderA("ID_VOCE") _
                                                            & "," & par.VirgoleInPunti(Format((myReaderA("importo") / 365) * Giorni1, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()


                    Loop
                    myReaderA.Close()


                    Dim TotMorosita As Double = 0
                    Dim TotMorositaSB As Double = 0
                    'par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,(NVL(IMPORTO_TOTALE,0)-NVL(QUOTA_SIND_B,0))-  NVL(IMPORTO_PAGATO,0)-NVL(QUOTA_SIND_PAGATA_B,0) FROM SISCOM_MI.BOL_BOLLETTE WHERE (BOL_BOLLETTE.FL_SOLLECITO=1 OR ID<0) AND BOL_BOLLETTE.data_scadenza<" & Format(Now, "yyyyMMdd") & " AND FL_ANNULLATA='0' AND ID_BOLLETTA_RIC IS NULL AND ID_RATEIZZAZIONE IS NULL AND (ID_TIPO=1 OR ID_TIPO=2 OR ID_TIPO=6 OR id_tipo=8) AND ID_CONTRATTO=" & lIdContratto

                    'myReaderA = par.cmd.ExecuteReader()
                    'Do While myReaderA.Read
                    '    TotMorositaSB = 0
                    '    TotMorositaSB = par.IfNull(myReaderA("IMPORTO_altri"), 0)
                    '    If TotMorositaSB > 0 Then
                    '        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET fl_sollecito='1',ID_BOLLETTA_RIC=" & ID_BOLLETTA & " WHERE ID=" & myReaderA("ID")
                    '        par.cmd.ExecuteNonQuery()
                    '        TotMorosita = TotMorosita + TotMorositaSB
                    '    End If
                    'Loop
                    'myReaderA.Close()

                    If TotMorosita > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                       & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",151" _
                                                       & "," & par.VirgoleInPunti(Format(TotMorosita, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + TotMorosita
                    End If
                End If

                'max 14/12/2016
                Dim IndiciGestionali As String = ""
                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=60"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.HasRows = True Then
                    If myReaderA.Read Then
                        IndiciGestionali = myReaderA("VALORE")
                    End If
                End If
                myReaderA.Close()
                If IndiciGestionali <> "" Then
                    par.cmd.CommandText = "select bol_bollette_VOCI_gest.*,BOL_BOLLETTE_GEST.NOTE FROM SISCOM_MI.bol_bollette_gest,SISCOM_MI.bol_bollette_VOCI_gest WHERE bol_bollette_VOCI_gest.ID_BOLLETTA_GEST=BOL_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N' AND ID_TIPO IN (" & IndiciGestionali & ") AND ID_CONTRATTO=" & lIdContratto & " ORDER BY ID_TIPO ASC"
                    myReaderA = par.cmd.ExecuteReader()
                    Do While myReaderA.Read
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & myReaderA("ID_VOCE") _
                                                            & "," & par.VirgoleInPunti(myReaderA("IMPORTO")) & ")"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' WHERE ID=" & myReaderA("ID_BOLLETTA_GEST")
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & lIdContratto & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F02', 'IMPORTO DI EURO " & myReaderA("IMPORTO") & " (" & par.PulisciStrSql(par.IfNull(myReaderA("NOTE"), "")) & ") INSERITE NELLA BOLLETTA DI FINE CONTRATTO')"
                        par.cmd.ExecuteNonQuery()

                        TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl(myReaderA("IMPORTO"))
                    Loop
                    myReaderA.Close()

                End If


                '--------------

                If SPESEmav > 0 And TOTALE_BOLLETTA > 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                    & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()

                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + SPESEmav
                End If

                If TOTALE_BOLLETTA > 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",403" _
                                                        & "," & par.VirgoleInPunti(Format(SPESEPOSTALI_DA_APPLICARE, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()
                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + SPESEPOSTALI_DA_APPLICARE
                End If

                If TOTALE_BOLLETTA > APPLICABOLLO Then
                    If BOLLO > 0 Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                            & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                End If

                If TOTALE_BOLLETTA <= SPESEmav And TOTALE_BOLLETTA > 0 Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_STAMPATO=0 WHERE ID=" & ID_BOLLETTA
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & ID_BOLLETTA
                    par.cmd.ExecuteNonQuery()
                    RIASSUNTO_BOLLETTA = ""
                End If

            End If

        End If
        myReaderS.Close()

        CreaBollettaFIN = True
        errore = ""
    End Function

End Class
