Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CALL_CENTER_Sopralluogo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtDataS.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            idSegnalazione.Value = Request.QueryString("IdSegn")
            CaricaDati()
            If Request.QueryString("LE") = 1 Then
                Me.txtRapporto.ReadOnly = True
                Me.txtTecnico.ReadOnly = True
                Me.btnSalva.Visible = False
                rdbPericolo.Enabled = False
                Me.txtDataS.ReadOnly = True
            End If
        End If
    End Sub

    Private Sub CaricaDati()
        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select * from siscom_mi.segnalazioni_sopralluogo where id_segnalazione = " & idSegnalazione.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Me.txtTecnico.Text = par.IfNull(lettore("TECNICO"), "")
                Me.txtRapporto.Text = par.IfNull(lettore("RAPPORTO"), "")
                Me.rdbPericolo.SelectedValue = par.IfNull(lettore("FL_PERICOLO"), 0)
            Else
                Me.rdbPericolo.SelectedValue = 0
            End If
            lettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try


            If Not String.IsNullOrEmpty(Me.txtTecnico.Text) Or Not String.IsNullOrEmpty(Me.txtRapporto.Text) Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select id from siscom_mi.segnalazioni_sopralluogo where id_segnalazione = " & idSegnalazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO SET " _
                                        & "TECNICO = '" & par.PulisciStrSql(Me.txtTecnico.Text.ToUpper) & "', " _
                                        & "RAPPORTO = '" & par.PulisciStrSql(Me.txtRapporto.Text.ToUpper) & "', " _
                                        & "FL_PERICOLO = " & Me.rdbPericolo.SelectedValue _
                                        & ",DATA_SOPRALLUOGO = '" & par.AggiustaData(Me.txtDataS.Text) & "' WHERE ID = " & par.IfNull(lettore("ID"), -1)
                    par.cmd.ExecuteNonQuery()

                    WriteEvent("F02", "AGGIORNAMENTO DATI SOPRALLUOGO")

                Else
                    par.cmd.CommandText = "insert into siscom_mi.SEGNALAZIONI_SOPRALLUOGO(id,id_segnalazione,tecnico,rapporto,fl_pericolo,data_sopralluogo) values " _
                                        & "(siscom_mi.seq_segnalazioni_sopralluogo.nextval," & idSegnalazione.Value & ",'" & par.PulisciStrSql(Me.txtTecnico.Text.ToUpper) _
                                        & "','" & par.PulisciStrSql(Me.txtRapporto.Text.ToUpper) & "'," & Me.rdbPericolo.SelectedValue & ",'" & par.AggiustaData(Me.txtDataS.Text) & "')"

                    par.cmd.ExecuteNonQuery()

                    WriteEvent("F55", "DATI SOPRALLUOGO")

                End If
                lettore.Close()



                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")

            Else
                Response.Write("<script>alert('Definire almeno il nome del tecnico o il rapporto del sopralluogo!')</script>")

            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnSalva0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva0.Click
        'Response.Write("<script>alert('Funzione non disponibile!')</script>")
        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\SopralluogoCallC.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim richiesta As String = ""
            Dim note As String = ""
            Dim condominio As String = ""
            Dim gestAuto As String = ""
            Dim sfratto As String = ""
            Dim morosità As String = ""
            Dim sloggio As String = ""
            Dim idContratto As String = ""
            par.cmd.CommandText = "select * from siscom_mi.segnalazioni where id = " & idSegnalazione.Value
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then

                contenuto = Replace(contenuto, "$nrichiesta$", par.IfNull(lettore("ID"), ""))
                contenuto = Replace(contenuto, "$datarichiesta$", par.FormattaData(Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "                 "), 1, 8)))
                contenuto = Replace(contenuto, "$descrizione$", par.IfNull(lettore("descrizione_ric"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(lettore("COGNOME_RS"), "") & " " & par.IfNull(lettore("NOME"), ""))


                contenuto = Replace(contenuto, "$numerotelefono1$", par.IfNull(lettore("TELEFONO1"), ""))
                contenuto = Replace(contenuto, "$numerotelefono2$", par.IfNull(lettore("TELEFONO2"), ""))


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO WHERE ID_SEGNALAZIONE = " & idSegnalazione.Value
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader.Read Then

                    contenuto = Replace(contenuto, "$rapporto$", par.IfNull(myreader("rapporto"), ""))
                    contenuto = Replace(contenuto, "$tecnico$", par.IfNull(myreader("tecnico"), ""))

                    If myreader("fl_pericolo") = 1 Then
                        contenuto = Replace(contenuto, "$pericolo$", "SI")
                    ElseIf myreader("fl_pericolo") = 0 Then
                        contenuto = Replace(contenuto, "$pericolo$", "NO")
                    Else
                        contenuto = Replace(contenuto, "$pericolo$", "")

                    End If
                Else
                    contenuto = Replace(contenuto, "$pericolo$", "")
                    contenuto = Replace(contenuto, "$rapporto$", "&nbsp; ")
                    contenuto = Replace(contenuto, "$tecnico$", "")

                End If
                myreader.Close()

                If par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "1" Then
                    richiesta = "SEGNALAZIONE GUASTI"
                    par.cmd.CommandText = "select descrizione from siscom_mi.tipologie_guasti where id = " & par.IfNull(lettore("id_tipologie"), "0")
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        richiesta = richiesta & " - " & par.IfNull(myreader("descrizione"), "")
                    End If
                    myreader.Close()
                ElseIf par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "0" Then
                    richiesta = "RICHIESTA INFORMAZIONI"

                End If
                contenuto = Replace(contenuto, "$tiporichiesta$", richiesta)

                par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = " & idSegnalazione.Value
                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    note = note & par.IfNull(myreader("note"), "") & "<br/>"
                End While
                myreader.Close()
                contenuto = Replace(contenuto, "$note$", note)

                Dim indirizzo As String = ""
                par.cmd.CommandText = "SELECT COD_EDIFICIO,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = " & par.IfNull(lettore("id_edificio"), "0")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = par.IfNull(myreader("denominazione"), "")
                    contenuto = Replace(contenuto, "$edificio$", "EDIFICIO COD." & par.IfNull(myreader("cod_edificio"), ""))
                End If
                myreader.Close()

                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.cod_unita_immobiliare, UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS piano,SCALE_EDIFICI.descrizione AS SCALA, " _
                                    & "siscom_mi.Getintestatari(id_contratto) AS intestatario " _
                                    & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO, siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita(+)" _
                                    & "AND UNITA_IMMOBILIARI.ID = " & par.IfNull(lettore("id_unita"), 0)
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = indirizzo & " " & "SCALA: " & par.IfNull(myreader("SCALA"), "--") & " PIANO: " & par.IfNull(myreader("PIANO"), "--") & " INTERNO:" & par.IfNull(myreader("interno"), "--")
                    contenuto = Replace(contenuto, "$unita$", "U.I. cod." & par.IfNull(myreader("COD_UNITA_IMMOBILIARE"), ""))
                Else
                    contenuto = Replace(contenuto, "$unita$", "")

                End If
                myreader.Close()
                contenuto = Replace(contenuto, "$indirizzo$", indirizzo)

                par.cmd.CommandText = "Select nome from siscom_mi.tab_filiali Where id = " & par.IfNull(lettore("id_struttura"), "")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    contenuto = Replace(contenuto, "$struttura$", "STRUTTURA: " & par.IfNull(myreader("nome"), ""))
                End If
                myreader.Close()


                par.cmd.CommandText = "SELECT ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO, rapporti_utenza.data_riconsegna, siscom_mi.Getintestatari (id_contratto) AS intestatario, " _
                                    & "SISCOM_MI.Getstatocontratto(ID_CONTRATTO) AS STATO,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPO_CONTRATTO " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & "AND NVL(DATA_RICONSEGNA,'50000000')=(" _
                                    & "SELECT MAX(NVL(DATA_RICONSEGNA,'50000000')) " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & ")"

                myreader = par.cmd.ExecuteReader
                Dim datiCont As String = ""
                If myreader.Read Then
                    idContratto = par.IfNull(myreader("ID_CONTRATTO"), "")
                    datiCont = "CONTRATTO: " & par.IfNull(myreader("tipo_contratto"), "") & " " & par.IfNull(myreader("cod_contratto"), "") & " Stato Contratto: " & par.IfNull(myreader("stato"), "") & " Saldo: " & Format(par.CalcolaSaldoAttuale(par.IfNull(myreader("ID_CONTRATTO"), "0")), "##,##0.00") & " Euro "
                    contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myreader("intestatario"), ""))
                    If par.IfNull(myreader("data_riconsegna"), "") <> "" Then
                        sloggio = "SLOGGIO: " & par.FormattaData(par.IfNull(myreader("data_riconsegna"), ""))
                    End If
                Else
                    contenuto = Replace(contenuto, "$intestatario$", "")

                End If
                contenuto = Replace(contenuto, "$contratto$", datiCont)





                par.cmd.CommandText = "SELECT condomini.denominazione, (cond_amministratori.cognome || ' ' ||cond_amministratori.nome) AS amministratore " _
                                & "FROM siscom_mi.condomini,siscom_mi.cond_amministratori,siscom_mi.cond_amministrazione " _
                                & "WHERE condomini.ID =cond_amministrazione.id_condominio AND cond_amministratori.ID = id_amministratore AND cond_amministrazione.data_fine IS NULL " _
                                & "AND condomini.ID IN (SELECT id_condominio FROM siscom_mi.cond_edifici WHERE id_edificio = " & par.IfNull(lettore("id_edificio"), 0) & ")"

                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    condominio = condominio & "CONDOMINIO: " & par.IfNull(myreader("denominazione"), "") & " AMMINISTRATORE: " & par.IfNull(myreader("amministratore"), "")
                End While
                myreader.Close()

                contenuto = Replace(contenuto, "$condomino$", condominio)
                contenuto = Replace(contenuto, "$gestauto$", gestAuto)
                contenuto = Replace(contenuto, "$morosità$", morosità)
                contenuto = Replace(contenuto, "$sfratto$", sfratto)
                contenuto = Replace(contenuto, "$sloggio$", sloggio)

                If idContratto <> "" Then
                    par.cmd.CommandText = "SELECT ID_MOROSITA ,(CASE WHEN COD_STATO = 'M20' THEN 'SI' ELSE 'NO' END)AS PRATICA_LEGALE FROM SISCOM_MI.MOROSITA_LETTERE where  cod_stato not in ('M94','M98','M100') and id_contratto =" & idContratto
                    myreader = par.cmd.ExecuteReader

                    If myreader.Read Then

                        morosità = "MESSA IN MORA "
                        If par.IfNull(myreader("pratica_legale"), "NO") = "SI" Then
                            morosità = morosità & "- AVVIATA PRATICA LEGALE"
                        End If
                    End If
                    myreader.Close()
                End If


            End If
            lettore.Close()


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = idSegnalazione.Value & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))

            Response.Write("<script>window.open('../FileTemp/" & nomefile & ".pdf','','');</script>")

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                connOpNow = True
            End If


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES ( " & idSegnalazione.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
            & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"

            par.cmd.ExecuteNonQuery()

            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")


        End Try
    End Sub

End Class
