Imports System.IO

Partial Class StornoMassivo
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub btnStorna_Click(sender As Object, e As System.EventArgs) Handles btnStorna.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.par.cmd.Transaction = par.myTrans

            Dim percorso As String = Server.MapPath("FileTemp")
            Dim FileName As String = ""
            Dim errore As String = ""
            If FileUpload1.HasFile = True Then
                FileName = percorso & "\" & FileUpload1.FileName

                FileUpload1.SaveAs(FileName)
                If System.IO.File.Exists(FileName) = True Then
                    Dim sr = New StreamReader(FileName)
                    Dim idBoll As String = ""
                    Do
                        idBoll = sr.ReadLine
                        If idBoll <> "" Then
                            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID=" & idBoll & " AND ID_TIPO NOT IN (4,5) AND ID_BOLLETTA_RIC IS NULL AND ID_BOLLETTA_STORNO IS NULL"
                            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt1 As New Data.DataTable
                            da1.Fill(dt1)
                            da1.Dispose()
                            If dt1.Rows.Count > 0 Then
                                For Each rowDT In dt1.Rows
                                    StornaBolletta(rowDT.item("ID"), "Importo Erroneamente Addebitato", errore, False)
                                Next
                            Else
                                par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID=" & idBoll
                                Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                                Dim dt2 As New Data.DataTable
                                da2.Fill(dt2)
                                da2.Dispose()
                                If dt2.Rows.Count > 0 Then
                                    If dt2.Rows(0).Item("ID_TIPO") = "4" Then
                                        Response.Write("<script>alert('Attenzione! La bolletta " & dt2.Rows(0).Item("ID") & " è di tipo MOR. Storno non effettuato.')</script>")
                                    End If
                                    If dt2.Rows(0).Item("ID_TIPO") = "5" Then
                                        Response.Write("<script>alert('Attenzione! La bolletta " & dt2.Rows(0).Item("ID") & " è di tipo RAT. Storno non effettuato.')</script>")
                                    End If
                                    If par.IfNull(dt2.Rows(0).Item("ID_BOLLETTA_RIC"), "0") <> "0" Then
                                        Response.Write("<script>alert('Attenzione! La bolletta " & dt2.Rows(0).Item("ID") & " è RICLASSIFICATA. Storno non effettuato.')</script>")
                                    End If
                                    If par.IfNull(dt2.Rows(0).Item("ID_BOLLETTA_STORNO"), "0") <> "0" Then
                                        Response.Write("<script>alert('Attenzione! La bolletta " & dt2.Rows(0).Item("ID") & " è stata già stornata.')</script>")
                                    End If
                                Else
                                    Response.Write("<script>alert('Attenzione! Id bolletta " & idBoll & " non trovato.')</script>")
                                End If
                            End If
                        End If
                    Loop Until idBoll Is Nothing
                    sr.Close()

                    par.myTrans.Commit()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                Else
                    Response.Write("<script>alert('File non trovato!')</script>")
                End If
                Response.Write("<script>alert('Operazione effettuata!')</script>")
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
    End Sub
    Function StornaBolletta(ByVal idBolletta As String, ByVal motivoStorno As String, ByRef errore As String, Optional parziale As Boolean = False) As Boolean
        StornaBolletta = False

        errore = "StornaBolletta"

        Dim pagata As Boolean = False
        Dim dataPagamento As String = ""
        Dim dataValuta As String = ""
        par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID=" & idBolletta
        Dim daBolStorno As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtStorno As New Data.DataTable
        daBolStorno.Fill(dtStorno)
        daBolStorno.Dispose()
        If dtStorno.Rows.Count > 0 Then
            If par.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(dtStorno.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                pagata = True
                dataPagamento = par.IfNull(dtStorno.Rows(0).Item("DATA_PAGAMENTO"), "")
                dataValuta = par.IfNull(dtStorno.Rows(0).Item("DATA_VALUTA"), "")
            Else
                pagata = False
                parziale = False ' SE NON è PAGATA, è INUTILE CERCARE DI FARE LO STORNO PARZIALE
                dataPagamento = Format(Now, "yyyyMMdd")
                dataValuta = Format(Now, "yyyyMMdd")
            End If
        Else
            'id bolletta passato non valido
            StornaBolletta = False
            Exit Function
        End If
        'ricava id Anagrafica
        Dim idAnagr As Long = 0
        Dim intestatario As String = ""
        par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTEST  FROM siscom_mi.RAPPORTI_UTENZA,siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.ANAGRAFICA WHERE " _
                        & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=" & par.IfEmpty(dtStorno.Rows(0).Item("ID_CONTRATTO").ToString, "0") & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
        Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreDati.Read Then
            idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
            intestatario = par.IfNull(lettoreDati("INTEST"), "")
        End If
        lettoreDati.Close()

        Dim dataAttuale As String = ""
        Dim dataInizioCompet As String = ""
        Dim dataFineCompet As String = ""
        dataAttuale = Format(Now, "dd/MM/yyyy")
        If dataAttuale <> "" Then
            dataInizioCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & "01"
            dataFineCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & DateTime.DaysInMonth(Right(dataAttuale, 4), dataAttuale.Substring(3, 2))
        End If


        Dim NumBOlletta As String = dtStorno.Rows(0).Item("num_bolletta").ToString
        Dim noteBolletta As String = motivoStorno & " NUM. BOLLETTA STORNATA:" & NumBOlletta

        Dim pagataParz As Boolean = False
        Dim ID_BOLLETTA_STORNO As Integer = 0

        'STORNA BOLLETTA SELEZIONATA
        If pagata = True Then
            If parziale = False Then
                Dim importoTot As Decimal = 0
                importoTot = par.IfNull(dtStorno.Rows(0).Item("IMPORTO_TOTALE"), 0)
                'se pagata parzialmente viene creata l'eccedenza per l'importo pagato
                If par.IfNull(dtStorno.Rows(0).Item("IMPORTO_TOTALE"), 0) > par.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                    importoTot = par.IfNull(dtStorno.Rows(0).Item("IMPORTO_PAGATO"), 0)
                    pagataParz = True
                End If

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & par.IfEmpty(dtStorno.Rows(0).Item("ID_CONTRATTO").ToString, "0") & "," & par.RicavaEsercizioCorrente() & "," & dtStorno.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(importoTot * -1) & "," _
                            & "'" & Format(Now, "yyyyMMdd") & "','" & dtStorno.Rows(0).Item("DATA_PAGAMENTO") & "','" & dtStorno.Rows(0).Item("DATA_VALUTA") & "',4,'N',null,'ECCEDENZA PER PAGAMENTO BOLLETTA STORNATA " & dtStorno.Rows(0).Item("NUM_BOLLETTA") & "')"
                par.cmd.ExecuteNonQuery()

                Dim idBollGest As Long = 0
                par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idBollGest = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",712," & par.VirgoleInPunti(importoTot * -1) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & par.IfEmpty(dtStorno.Rows(0).Item("ID_CONTRATTO").ToString, "0") & "," & System.Web.HttpContext.Current.Session("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F204','IMPORTO PARI A EURO " & Format(importoTot, "##,##0.00") & "')"
                par.cmd.ExecuteNonQuery()
            Else
                pagataParz = True
            End If

        End If


        par.cmd.CommandText = "select siscom_mi.SEQ_BOL_BOLLETTE.NEXTVAL FROM DUAL"
        Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderST.Read Then
            ID_BOLLETTA_STORNO = myReaderST(0)
        End If
        myReaderST.Close()


        For Each row As Data.DataRow In dtStorno.Rows


            par.cmd.CommandText = "Insert into siscom_mi.BOL_BOLLETTE " _
                    & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                    & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                    & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                    & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                    & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                    & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                    & "Values " _
                    & "(" & ID_BOLLETTA_STORNO & ", 999, '" & Format(Now, "yyyyMMdd") _
                    & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & par.PulisciStrSql(noteBolletta) & "'," _
                    & "" & par.IfNull(row.Item("ID_CONTRATTO"), 0) _
                    & " ," & par.RicavaEsercizioCorrente() & ", " _
                    & par.IfNull(row.Item("ID_UNITA"), 0) _
                    & ", '0', '" & par.PulisciStrSql(par.IfNull(row.Item("PAGABILE_PRESSO"), "")) & "', " & par.IfNull(row.Item("COD_AFFITTUARIO"), 0) & "" _
                    & ", '" & par.PulisciStrSql(intestatario).ToUpper & "', " _
                    & "'" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) _
                    & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) _
                    & "', '" & par.PulisciStrSql(par.IfNull(row.Item("PRESSO"), "")) & "', '" & par.IfNull(row.Item("RIFERIMENTO_DA"), "") _
                    & "', '" & par.IfNull(row.Item("RIFERIMENTO_A"), "") & "', " _
                    & "'1', " & par.IfNull(row.Item("ID_COMPLESSO"), 0) & ", '', '', " _
                    & Year(Now) & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD',22)"
            par.cmd.ExecuteNonQuery()
        Next

        Dim ID_VOCE_STORNO As Long = 0
        'Dim SumImportoVOCI As Decimal = 0
        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM siscom_mi.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE_VOCI.ID_BOLLETTA = " & idBolletta


        Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtBVoci As New Data.DataTable
        daBVoci.Fill(dtBVoci)
        daBVoci.Dispose()
        Dim ImpVoceBolStorno As Decimal = 0
        For Each row As Data.DataRow In dtBVoci.Rows
            ImpVoceBolStorno = 0
            par.cmd.CommandText = "select siscom_mi.SEQ_BOL_BOLLETTE_VOCI.nextval FROM DUAL"
            Dim myReaderIDV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIDV.Read Then
                ID_VOCE_STORNO = myReaderIDV(0)
            End If
            myReaderIDV.Close()
            If parziale = False Then
                ImpVoceBolStorno = par.IfNull(row.Item("IMPORTO"), 0)

            ElseIf parziale = True Then
                ImpVoceBolStorno = par.IfNull(row.Item("IMPORTO"), 0) - par.IfNull(row.Item("imp_pagato"), 0)

            End If
            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                            & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO" _
                            & ") " _
                            & "VALUES ( " & ID_VOCE_STORNO & ", " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & par.VirgoleInPunti(ImpVoceBolStorno * -1) & ",'STORNO'," _
                            & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "" _
                            & ")"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(ImpVoceBolStorno * -1) & " WHERE ID=" & ID_VOCE_STORNO
            par.cmd.ExecuteNonQuery()

            'If parziale = False Then
            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) & " WHERE ID=" & row.Item("ID")
            par.cmd.ExecuteNonQuery()
            'End If

            'SumImportoVOCI = SumImportoVOCI + par.IfNull(row.Item("IMPORTO"), 0)
        Next



        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & idBolletta
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
        par.cmd.ExecuteNonQuery()

        Dim noteEvento As String = ""
        noteEvento = noteBolletta
        If pagata = True Then
            If pagataParz = True Then
                noteEvento &= "(parzialm. pagata)"
            Else
                noteEvento &= "(precedentam. pagata)"
            End If
        Else
            noteEvento &= "(non precedentem. pagata)"
        End If

        par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & dtStorno.Rows(0).Item("ID_CONTRATTO") & "," & System.Web.HttpContext.Current.Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F203','" & noteBolletta & "')"
        par.cmd.ExecuteNonQuery()

        StornaBolletta = True
        errore = ""
    End Function

End Class
