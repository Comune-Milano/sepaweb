Imports System.IO

Partial Class Contratti_RicevutaEntratel
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            provenienza.Value = Request.QueryString("C")
            If provenienza.Value = "1" Then
                titoloPag.Text = "Cessioni"
            Else
                titoloPag.Text = "Prime Registrazioni"
            End If
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If provenienza.Value = "1" Then
            RicevutaCessioni()
        Else
            RicevutaPrimaRegistrazione()
        End If
    End Sub

    Private Sub RicevutaCessioni()
        Dim errore As Boolean
        Dim sErrore As String = ""
        Dim NumeroProtocollo As String = ""
        Dim DataRegistrazione As String = ""
        Dim NomeFilePDF As String = ""
        Dim NOMEFILEREG As String = ""
        Dim BUONO As Boolean = True

        Try
            If HiddenField1.Value = "0" Then
                If FileUpload1.HasFile = True Then
                    TextBox1.Visible = False
                    errore = False

                    If UCase(Mid(FileUpload1.FileName, Len(FileUpload1.FileName) - 2, 3)) <> "REL" Then
                        sErrore = sErrore & "Errore: Tipo file non valido! E' richiesto un file .rel"
                        errore = True
                    End If

                    If errore = False Then
                        Try
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                            par.myTrans = par.OracleConn.BeginTransaction()

                            If System.IO.Directory.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\")) = False Then
                                System.IO.Directory.CreateDirectory(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\"))
                            End If
                            Dim NomeFile As String = ""
                            FileUpload1.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName))

                            If FileUpload2.FileName <> "" Then
                                FileUpload2.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload2.FileName))
                                NomeFilePDF = FileUpload2.FileName
                            End If

                            Dim Contenuto As String = ""
                            Dim Leggi As Boolean = True
                            Dim Comando As String = ""
                            Dim IdC As String = ""
                            Dim NumRegistrazione As String = ""
                            Dim i As Integer = 0
                            Dim Tipo As String = ""
                            Dim sTipo As String = ""
                            Dim DataValidita As String = ""
                            Dim ImportoRegistro As String = "0,00"
                            Dim ImportoSostitutiva As String = "0,00"
                            Dim ImportoSanzioni As String = "0,00"
                            Dim ImportoInteressi As String = "0,00"
                            Dim ImportoTotale As String = "0,00"
                            Dim ImportoGestionale As Decimal = 0
                            Dim CodiceTributo As String = ""
                            Dim nregistraz As String = ""
                            Dim PERC_CONDUTTORE As Double = 0
                            Dim ricevutaCaricata As Boolean = False

                            NomeFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName)
                            Dim sr1 As StreamReader = New StreamReader(NomeFile, System.Text.Encoding.Default)
                            Contenuto = sr1.ReadToEnd()
                            sr1.Close()

                            IdC = Trim(Mid(Contenuto, InStr(Contenuto, "Codice identificativo del contratto registrato:") + 48, 10))
                            NOMEFILEREG = Replace(Trim(Mid(Contenuto, InStr(Contenuto, "il file             ") + 20, 50)), "_dcm.ccf", "")
                            Dim dataDecorrAE As String = ""
                            Dim dataDecorr As String = ""
                            If Len(IdC) <> 19 Then
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE id=" & IdC
                            Else
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE cod_contratto='" & IdC & "'"
                            End If

                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.HasRows = False Then
                                BUONO = False
                                TextBox1.Visible = True
                                TextBox1.Text = "OPERAZIONE NON EFFETTUATA!" & vbCrLf & "Il rapporto con identificativo " & IdC & " non è stato trovato."
                            Else
                                If myReader.Read Then
                                    IdC = myReader("id")
                                End If
                                NumRegistrazione = myReader("num_registrazione")
                                dataDecorrAE = myReader("data_decorrenza_ae")
                                dataDecorr = myReader("data_decorrenza")
                            End If
                            myReader.Close()
                            Dim TipoTributo As String = ""
                            Dim RicScartata As Boolean = False

                            If BUONO = True Then
                                If InStr(UCase(Contenuto), "SCARTATA") > 0 Then
                                    RicScartata = True
                                End If
                                If InStr(UCase(Contenuto), "SCARTATO") > 0 Then
                                    RicScartata = True
                                End If
                                If RicScartata = True Then
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=3,NOTE='" & par.PulisciStrSql(Replace(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta e' stata scartata"), 240)), "     ", "")) & "' WHERE ID_FASE_REGISTRAZIONE=1 AND COD_TRIBUTO IN ('110T') AND ID_CONTRATTO=" & IdC
                                    par.cmd.ExecuteNonQuery()

                                    Comando = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE_SC (ID_CONTRATTO,NOME_FILE_REL,NOME_FILE_PDF,NOTE,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NomeFilePDF & "','" & Mid(par.PulisciStrSql(Replace(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta e' stata scartata"), 240)), "     ", "")), 1, 4000) & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                    par.cmd.CommandText = Comando
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F02','XML CESSIONE SCARTATO DA A.E.')"
                                    par.cmd.ExecuteNonQuery()
                                    TextBox1.Visible = True
                                    TextBox1.Text = "OPERAZIONE EFFETTUATA!" & vbCrLf & Trim(Mid(Contenuto, InStr(Contenuto, "Codice identificativo del contratto registrato:"), 70)) & " " & Replace(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta e' stata scartata"), 240)), "     ", "")
                                Else
                                    DataRegistrazione = par.AggiustaData(Mid(Contenuto, InStr(Contenuto, "il sistema informativo dell'Agenzia delle Entrate") - 11, 10))
                                    NumeroProtocollo = Mid(Contenuto, 1, 17)

                                    TipoTributo = "110T"

                                    ImportoRegistro = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di registro") + 19, 18))
                                    ImportoSostitutiva = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di bollo") + 16, 18))

                                    'ImportoRegistro = "0,00"
                                    'ImportoSostitutiva = "0,00"

                                    If InStr(UCase(Contenuto), "SANZIONI") > 0 Then
                                        ImportoSanzioni = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI") + 8, 18))
                                    Else
                                        ImportoSanzioni = "0,00"
                                    End If
                                    If InStr(UCase(Contenuto), "INTERESSI") > 0 Then
                                        ImportoInteressi = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + 9, 18))
                                    Else
                                        ImportoInteressi = "0,00"
                                    End If

                                    '////////////////////////////////////////////////////
                                    '// NOTE COSIMO: Nel fare i report delle ricevute  (1249/2017)
                                    '// Importo sanzioni e interessi sono sbagliati
                                    '// In realtà ci sarebbero 5 importi da caricare in RAPPORTI_UTENZA_RICEVUTE: 
                                    '// Imposta di registro:  Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IMPOSTA DI REGISTRO") + len("IMPOSTA DI REGISTRO"), 18))
                                    '// Imposta di bollo:   Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IMPOSTA DI BOLLO") + len("IMPOSTA DI BOLLO"), 17))        
                                    '// Sanzioni registro:    Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI REGISTRO") + len("SANZIONI REGISTRO"), 20))
                                    '// Sanzioni bollo: Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI BOLLO") + len("SANZIONI BOLLO"), 19))
                                    '// Interessi:  Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + len("INTERESSI"), 28))

                                    Dim imposta_di_regsitro As String = "0,00"
                                    Dim sanzioni_bollo As String = "0,00"
                                    Dim interessi As String = "0,00"

                                    If InStr(UCase(Contenuto), "IMPOSTA DI REGISTRO") > 0 Then
                                        imposta_di_regsitro = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IMPOSTA DI REGISTRO") + Len("IMPOSTA DI REGISTRO"), 18))
                                    End If
                                    If InStr(UCase(Contenuto), "SANZIONI BOLLO") > 0 Then
                                        sanzioni_bollo = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI BOLLO") + Len("SANZIONI BOLLO"), 19))
                                    End If
                                    If InStr(UCase(Contenuto), "INTERESSI") > 0 Then
                                        interessi = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + Len("INTERESSI"), 28))
                                    End If
                                    If InStr(UCase(Contenuto), "SANZIONI REGISTRO") > 0 Then
                                        ImportoSanzioni = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI REGISTRO") + Len("SANZIONI REGISTRO"), 20))
                                    End If
                                    ImportoSanzioni = Format(CDec(ImportoSanzioni) + CDec(sanzioni_bollo), "##,##0.00")
                                    '''' INSERIRE QUESTI VALORI IN TABELLA RAPPORTI_UTENZA_RICEVUTE
                                    '////////////////////////////////////////////////

                                    'DataValidita = par.AggiustaData(Trim(Mid(Contenuto, InStr(Contenuto, "Durata dal") + 25, 10)))
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                       & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                       & "'F57','')"
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE where id_contratto=" & IdC & " and anno=" & Mid(DataRegistrazione, 1, 4) & " and PG_AE='" & NumeroProtocollo & "' AND COD_TRIBUTO='" & TipoTributo & "' AND VALIDA_FINO_AL='" & DataValidita & "'"
                                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then
                                        ricevutaCaricata = True
                                    End If
                                    myReaderA.Close()

                                    '/////////////////////////
                                    '// 1249/2017
                                    'Comando = "INSERT INTO RAPPORTI_UTENZA_RICEVUTE (ID_CONTRATTO,NOME_FILE_REL,PG_AE,COD_TRIBUTO,ANNO,NOME_FILE_PDF,DATA_REGISTRAZIONE,REGISTRO,SOSTITUTIVA,SANZIONI,INTERESSI,TOTALE,VALIDA_FINO_AL,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NumeroProtocollo & "','" & TipoTributo & "','" & Mid(DataRegistrazione, 1, 4) & "','" & NomeFilePDF & "','" & DataRegistrazione & "','" & ImportoRegistro & "','','" & ImportoSanzioni & "','" & ImportoInteressi & "','','" & DataValidita & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                    Comando = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE (ID_CONTRATTO,NOME_FILE_REL,PG_AE,COD_TRIBUTO,ANNO,NOME_FILE_PDF,DATA_REGISTRAZIONE,REGISTRO,SOSTITUTIVA,SANZIONI,INTERESSI,TOTALE,VALIDA_FINO_AL,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NumeroProtocollo & "','" & TipoTributo & "','" & Mid(DataRegistrazione, 1, 4) & "','" & NomeFilePDF & "','" & DataRegistrazione & "','" & imposta_di_regsitro & "','','" & sanzioni_bollo & "','" & interessi & "','','" & DataValidita & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                    '/////////////////////////

                                    par.cmd.CommandText = Comando
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=2 WHERE ID_FASE_REGISTRAZIONE=1 AND COD_TRIBUTO ='110T' AND ID_CONTRATTO=" & IdC
                                    par.cmd.ExecuteNonQuery()

                                    ImportoGestionale = CDec(ImportoRegistro.ToString.PadLeft(Len(ImportoRegistro) + 1, "0")) 'SOLO TRIBUTO

                                    par.cmd.CommandText = "select tipologia_contratto_locazione.* from siscom_mi.tipologia_contratto_locazione where cod=(SELECT COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IdC & ")"
                                    Dim myReaderFF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderFF.Read Then
                                        PERC_CONDUTTORE = par.IfNull(myReaderFF("perc_conduttore"), 0)
                                        ImportoGestionale = (ImportoGestionale * PERC_CONDUTTORE) / 100
                                    End If
                                    myReaderFF.Close()

                                    If dataDecorrAE > dataDecorr Then
                                        ImportoSostitutiva = 0
                                    End If

                                    'If IsNumeric(ImportoSanzioni) Then
                                    '    ImportoGestionale = CDec(ImportoRegistro.ToString.PadLeft(Len(ImportoRegistro) + 1, "0")) + CDec(ImportoSostitutiva.ToString.PadLeft(Len(ImportoSostitutiva) + 1, "0")) + CDec(ImportoSanzioni.ToString.PadLeft(Len(ImportoSanzioni) + 1, "0")) + CDec(ImportoInteressi.ToString.PadLeft(Len(ImportoInteressi) + 1, "0"))
                                    'Else
                                    '    ImportoGestionale = CDec(ImportoRegistro.ToString.PadLeft(Len(ImportoRegistro) + 1, "0")) + CDec(ImportoSostitutiva.ToString.PadLeft(Len(ImportoSostitutiva) + 1, "0")) + CDec(ImportoInteressi.ToString.PadLeft(Len(ImportoInteressi) + 1, "0"))
                                    'End If

                                    If ImportoGestionale > 0 And ricevutaCaricata = False Then
                                        ScriviGestionale(IdC, DataRegistrazione, TipoTributo, ImportoGestionale, CDec(ImportoSostitutiva.ToString.PadLeft(Len(ImportoSostitutiva) + 1, "0")))

                                        TextBox1.Visible = True
                                        TextBox1.Text = TextBox1.Text & vbCrLf & "OPERAZIONE EFFETTUATA!" & vbCrLf & "Sono stati importati i dati relativi a 1 rapporto."
                                    Else
                                        If ricevutaCaricata = True Then
                                            par.myTrans.Rollback()
                                            par.OracleConn.Close()
                                            TextBox1.Visible = True
                                            TextBox1.Text = TextBox1.Text & vbCrLf & "OPERAZIONE NON EFFETTUATA!" & vbCrLf & "Ricevuta già caricata per il rapporto con identificativo " & IdC
                                            Exit Try
                                        End If
                                    End If


                                End If
                            Else
                                File.Delete(Server.MapPath("..\ ALLEGATI \ CONTRATTI \ ELABORAZIONI \ RICEVUTE \ " & FileUpload1.FileName))
                                If FileUpload2.FileName <> "" Then
                                    File.Delete(Server.MapPath("..\ ALLEGATI \ CONTRATTI \ ELABORAZIONI \ RICEVUTE \ " & FileUpload2.FileName))
                                End If
                            End If

                            par.myTrans.Commit()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        Catch ex As Exception

                            par.myTrans.Rollback()
                            par.OracleConn.Close()
                            TextBox1.Visible = True
                            TextBox1.Text = TextBox1.Text & vbCrLf & ex.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!"

                        End Try
                    Else
                        TextBox1.Visible = True
                        TextBox1.Text = sErrore
                    End If
                End If
                HiddenField1.Value = "1"
            End If

        Catch ex1 As HttpUnhandledException
            lblErrore.Visible = True
            lblErrore.Text = ex1.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!"
        End Try
    End Sub

    Private Sub ScriviGestionale(ByVal IdContratto As Long, ByVal dataRegistrazione As String, ByVal codTributo As String, ByVal importoReg As Decimal, ByVal importoBollo As Decimal)

        Dim tipoImposta As String = ""
        Dim idBollGest As Long = 0
        Dim idUI As Long = 0
        Dim idAnagrafica As Long = 0


        par.cmd.CommandText = "SELECT id_unita from siscom_mi.unita_contrattuale where id_unita_principale is null and id_contratto=" & IdContratto
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            idUI = myReader("id_unita")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT id_anagrafica from siscom_mi.soggetti_contrattuali where cod_tipologia_occupante='INTE' and id_contratto=" & IdContratto
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            idAnagrafica = myReader("id_anagrafica")
        End If
        myReader.Close()

        'Imposta di registro cessioni 110T
        'Imposta di registro per contratti di locazione fabbricati annualità successive 112T
        'Imposta di registro per contratti di locazione fabbricati intero periodo 107T
        'Imposta di registro per contratti di locazione fabbricati prima annualità 115T
        'Imposta di registro per proroghe (contratti di locazione e affitti) 114T
        'Imposta di registro per risoluzioni (contratti di locazione e affitti) 113T
        Select Case codTributo
            Case "110T"
                tipoImposta = "cessione"

            Case "115T"
                tipoImposta = "stipula contratto (prima annualità)"

            Case "107T"
                tipoImposta = "stipula contratto (intero periodo)"

        End Select

        If importoReg > 0 Then
            par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                idBollGest = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE ) " _
                            & "VALUES (" & idBollGest & "," & IdContratto & "," & par.RicavaEsercizioCorrente() & "," & idUI & "," & idAnagrafica & "," _
                            & "'" & dataRegistrazione & "','" & dataRegistrazione & "'," & par.VirgoleInPunti(importoReg) & "," _
                            & "'" & dataRegistrazione & "','" & dataRegistrazione & "'," _
                            & "'" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.IfEmpty(par.FormattaData(dataRegistrazione), Format(Now, "dd/MM/yyyy"))))) & "'" _
                            & ",117,'N',NULL,'Imposta di registro per " & tipoImposta & "')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                                & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & "," & 93 & "," & par.VirgoleInPunti(importoReg) & ")"
            par.cmd.ExecuteNonQuery()
        End If
        If importoBollo > 0 Then
            par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                idBollGest = myReader(0)
            End If
            myReader.Close()

            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                           & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE ) " _
                           & "VALUES (" & idBollGest & "," & IdContratto & "," & par.RicavaEsercizioCorrente() & "," & idUI & "," & idAnagrafica & "," _
                           & "'" & dataRegistrazione & "','" & dataRegistrazione & "'," & par.VirgoleInPunti(importoBollo) & "," _
                           & "'" & dataRegistrazione & "','" & dataRegistrazione & "'," _
                           & "'" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.IfEmpty(par.FormattaData(dataRegistrazione), Format(Now, "dd/MM/yyyy"))))) & "'" _
                           & ",118,'N',NULL,'Imposta di bollo per " & tipoImposta & "')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                                & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",8," & par.VirgoleInPunti(importoBollo) & ")"
            par.cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub RicevutaPrimaRegistrazione()

        Dim errore As Boolean
        Dim sErrore As String = ""
        Dim NumeroProtocollo As String = ""
        Dim DataRegistrazione As String = ""
        Dim NomeFilePDF As String = ""
        Dim NOMEFILEREG As String = ""
        Dim BUONO As Boolean = True

        Try
            If HiddenField1.Value = "0" Then
                If FileUpload1.HasFile = True Then
                    TextBox1.Visible = False
                    errore = False

                    If UCase(Mid(FileUpload1.FileName, Len(FileUpload1.FileName) - 2, 3)) <> "REL" Then
                        sErrore = sErrore & "Errore: Tipo file non valido! E' richiesto un file .rel"
                        errore = True
                    End If


                    If errore = False Then

                        Try
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                            par.myTrans = par.OracleConn.BeginTransaction()
                            '‘par.cmd.Transaction = par.myTrans


                            If System.IO.Directory.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\")) = False Then
                                System.IO.Directory.CreateDirectory(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\"))
                            End If
                            Dim NomeFile As String = ""
                            FileUpload1.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName))

                            If FileUpload2.FileName <> "" Then
                                FileUpload2.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload2.FileName))
                                NomeFilePDF = FileUpload2.FileName
                            End If

                            Dim Contenuto As String = ""
                            Dim Leggi As Boolean = True
                            Dim Comando As String = ""
                            Dim IdC As String = ""
                            Dim NumRegistrazione As String = ""
                            Dim i As Integer = 0
                            Dim Tipo As String = ""
                            Dim sTipo As String = ""
                            Dim DataValidita As String = ""
                            Dim ImportoRegistro As String = "0,00"
                            Dim ImportoSostitutiva As String = "0,00"
                            Dim ImportoSanzioni As String = "0,00"
                            Dim ImportoInteressi As String = "0,00"
                            Dim ImportoTotale As String = "0,00"
                            Dim ImportoGestionale As Decimal = 0
                            Dim CodiceTributo As String = ""
                            Dim PERC_CONDUTTORE As Double = 0
                            Dim ricevutaCaricata As Boolean = False
                            Dim dataDecorrAE As String = ""
                            Dim dataDecorr As String = ""

                            NomeFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName)
                            Dim sr1 As StreamReader = New StreamReader(NomeFile, System.Text.Encoding.Default)
                            Contenuto = sr1.ReadToEnd()
                            sr1.Close()

                            IdC = Trim(Mid(Contenuto, InStr(Contenuto, "Identificativo assegnato dal richiedente :") + 42, 20))
                            NOMEFILEREG = Replace(Trim(Mid(Contenuto, InStr(Contenuto, "il file             ") + 20, 50)), "_dcm.ccf", "")

                            If Len(IdC) <> 19 Then
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE id=" & IdC
                            Else
                                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE cod_contratto='" & IdC & "'"
                            End If

                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader.HasRows = False Then
                                BUONO = False
                                TextBox1.Visible = True
                                TextBox1.Text = "OPERAZIONE NON EFFETTUATA!" & vbCrLf & "Il rapporto con identificativo " & IdC & " non è stato trovato."
                            Else
                                If myReader.Read Then
                                    IdC = myReader("id")
                                    dataDecorrAE = myReader("data_decorrenza_ae")
                                    dataDecorr = myReader("data_decorrenza")
                                End If
                            End If
                            myReader.Close()
                            Dim TipoTributo As String = ""
                            Dim RicScartata As Boolean = False

                            If BUONO = True Then
                                If InStr(UCase(Contenuto), "SCARTATA") > 0 Then
                                    RicScartata = True
                                End If
                                If InStr(UCase(Contenuto), "SCARTATO") > 0 Then
                                    RicScartata = True
                                End If
                                If RicScartata = True Then

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=3,NOTE='" & par.PulisciStrSql(Replace(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta di registrazione"), 240)), "     ", "")) & "' WHERE ID_FASE_REGISTRAZIONE=1 AND COD_TRIBUTO IN ('107T','115T','1500') AND ID_CONTRATTO=" & IdC
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET versamento_tr=null, REG_TELEMATICA=null WHERE ID=" & IdC
                                    par.cmd.ExecuteNonQuery()

                                    Comando = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE_SC (ID_CONTRATTO,NOME_FILE_REL,NOME_FILE_PDF,NOTE,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NomeFilePDF & "','" & Mid(par.PulisciStrSql(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta di registrazione"), 240))), 1, 4000) & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                    par.cmd.CommandText = Comando
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F02','XML PRIMA REGISTRAZIONE SCARTATO DA A.E.')"
                                    par.cmd.ExecuteNonQuery()
                                    TextBox1.Visible = True
                                    TextBox1.Text = "OPERAZIONE EFFETTUATA!" & vbCrLf & Trim(Mid(Contenuto, InStr(Contenuto, "Identificativo assegnato dal richiedente"), 320)) & " " & Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta di registrazione"), 240))
                                Else
                                    NumeroProtocollo = Mid(Contenuto, 1, 17)
                                    DataRegistrazione = par.AggiustaData(Mid(Contenuto, InStr(Contenuto, "Il contratto e' stato registrato il") + 36, 10))

                                    NumRegistrazione = Trim(Mid(Contenuto, InStr(Contenuto, "al n.") + 6, 6))
                                    Tipo = Trim(Mid(Contenuto, InStr(Contenuto, "Tipo di pagamento:") + 18, 40))
                                    If UCase(Tipo) = "INTERA DURATA" Then
                                        sTipo = "versamento_tr='U',"
                                        TipoTributo = "107T"
                                    Else
                                        sTipo = "versamento_tr='A',"
                                        TipoTributo = "115T"
                                    End If

                                    '////////////////////////////////////////////////////
                                    '// NOTE COSIMO: Nel fare i report delle ricevute  (1249/2017)
                                    '// Importo sanzioni e interessi sono sbagliati
                                    '// In realtà ci sarebbero 5 importi da caricare in RAPPORTI_UTENZA_RICEVUTE: 
                                    '// Imposta di registro:  Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IMPOSTA DI REGISTRO") + len("IMPOSTA DI REGISTRO"), 18))
                                    '// Imposta di bollo:   Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IMPOSTA DI BOLLO") + len("IMPOSTA DI BOLLO"), 17))        
                                    '// Sanzioni registro:    Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI REGISTRO") + len("SANZIONI REGISTRO"), 20))
                                    '// Sanzioni bollo: Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI BOLLO") + len("SANZIONI BOLLO"), 19))
                                    '// Interessi:  Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + len("INTERESSI"), 28))
                                    ' Imposta di registro            185,00 Imposta di bollo            48,00         Sanzioni registro               20,00 Sanzioni bollo               4,80         Interessi                        0,03

                                    ImportoRegistro = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di registro") + 19, 18))
                                    ImportoSostitutiva = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di bollo") + 16, 18))

                                    If InStr(UCase(Contenuto), "Sanzioni registro") > 0 Then
                                        ImportoSanzioni = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "Sanzioni registro") + 17, 18))
                                    Else
                                        ImportoSanzioni = "0,00"
                                    End If

                                    If InStr(UCase(Contenuto), "INTERESSI") > 0 Then
                                        ImportoInteressi = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + 9, 18))
                                    Else
                                        ImportoInteressi = "0,00"
                                    End If


                                    Dim imposta_di_regsitro As String = "0,00"
                                    Dim sanzioni_bollo As String = "0,00"
                                    Dim interessi As String = "0,00"

                                    If InStr(UCase(Contenuto), "IMPOSTA DI REGISTRO") > 0 Then
                                        imposta_di_regsitro = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IMPOSTA DI REGISTRO") + Len("IMPOSTA DI REGISTRO"), 18))
                                    End If
                                    If InStr(UCase(Contenuto), "SANZIONI BOLLO") > 0 Then
                                        sanzioni_bollo = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI BOLLO") + Len("SANZIONI BOLLO"), 19))
                                    End If
                                    If InStr(UCase(Contenuto), "INTERESSI") > 0 Then
                                        interessi = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + Len("INTERESSI"), 28))
                                    End If

                                    ImportoSanzioni = Format(CDec(ImportoSanzioni) + CDec(sanzioni_bollo), "##,##0.00")
                                    '''' INSERIRE QUESTI VALORI IN TABELLA RAPPORTI_UTENZA_RICEVUTE
                                    '////////////////////////////////////////////////


                                    DataValidita = par.AggiustaData(Trim(Mid(Contenuto, InStr(Contenuto, "Durata dal") + 25, 10)))

                                    Comando = "update SISCOM_MI.rapporti_utenza set num_registrazione='" & NumRegistrazione & "',serie_registrazione='3T',data_reg='" & DataRegistrazione & "',data_assegnazione_pg='" & DataRegistrazione & "',nro_assegnazione_pg='" & NumeroProtocollo & "' where id=" & IdC
                                    par.cmd.CommandText = Comando
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = " INSERT INTO SISCOM_MI.RAPP_UTENZA_INFO_REGISTRAZONE (" _
                                        & "    ID, ID_CONTRATTO, " _
                                        & "    NUM_REGISTRAZIONE, SERIE_REGISTRAZIONE, DATA_REG, DATA_ASSEGNAZIONE_PG,NRO_ASSEGNAZIONE_PG) " _
                                        & " VALUES ( siscom_mi.SEQ_rapp_utenza_info_reg.nextval," _
                                        & IdC & ",'" _
                                        & NumRegistrazione & "'," _
                                        & "'3T','" _
                                        & DataRegistrazione & "','" _
                                        & DataRegistrazione & "','" _
                                        & NumeroProtocollo & "')"
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                       & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                       & "'F57','')"
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE where id_contratto=" & IdC & " and anno=" & Mid(DataRegistrazione, 1, 4) & " and PG_AE='" & NumeroProtocollo & "' AND COD_TRIBUTO='" & TipoTributo & "' AND VALIDA_FINO_AL='" & DataValidita & "'"
                                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then
                                        ricevutaCaricata = True
                                    End If
                                    myReaderA.Close()

                                    '/////////////////////////
                                    '// 1249/2017
                                    'Comando = "INSERT INTO RAPPORTI_UTENZA_RICEVUTE (ID_CONTRATTO,NOME_FILE_REL,PG_AE,COD_TRIBUTO,ANNO,NOME_FILE_PDF,DATA_REGISTRAZIONE,REGISTRO,SOSTITUTIVA,SANZIONI,INTERESSI,TOTALE,VALIDA_FINO_AL,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NumeroProtocollo & "','" & TipoTributo & "','" & Mid(DataRegistrazione, 1, 4) & "','" & NomeFilePDF & "','" & DataRegistrazione & "','" & ImportoRegistro & "','','" & ImportoSanzioni & "','" & ImportoInteressi & "','','" & DataValidita & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                    Comando = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE (ID_CONTRATTO,NOME_FILE_REL,PG_AE,COD_TRIBUTO,ANNO,NOME_FILE_PDF,DATA_REGISTRAZIONE,REGISTRO,SOSTITUTIVA,SANZIONI,INTERESSI,TOTALE,VALIDA_FINO_AL,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NumeroProtocollo & "','" & TipoTributo & "','" & Mid(DataRegistrazione, 1, 4) & "','" & NomeFilePDF & "','" & DataRegistrazione & "','" & imposta_di_regsitro & "',' " & ImportoSostitutiva & " ','" & ImportoSanzioni & "','" & interessi & "','','" & DataValidita & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                    '/////////////////////////
                                    par.cmd.CommandText = Comando
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=2 WHERE ID_FASE_REGISTRAZIONE=1 AND COD_TRIBUTO ='" & TipoTributo & "' AND ID_CONTRATTO=" & IdC
                                    par.cmd.ExecuteNonQuery()

                                    ImportoGestionale = CDec(ImportoRegistro.ToString.PadLeft(Len(ImportoRegistro) + 1, "0")) 'SOLO TRIBUTO

                                    par.cmd.CommandText = "select tipologia_contratto_locazione.* from siscom_mi.tipologia_contratto_locazione where cod=(SELECT COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IdC & ")"
                                    Dim myReaderFF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                    If myReaderFF.Read Then
                                        PERC_CONDUTTORE = par.IfNull(myReaderFF("perc_conduttore"), 0)
                                        ImportoGestionale = (ImportoGestionale * PERC_CONDUTTORE) / 100
                                    End If
                                    myReaderFF.Close()

                                    If dataDecorrAE > dataDecorr Then
                                        ImportoSostitutiva = 0
                                    End If

                                    'If IsNumeric(ImportoSanzioni) Then
                                    '    ImportoGestionale = CDec(ImportoRegistro.ToString.PadLeft(Len(ImportoRegistro) + 1, "0")) + CDec(ImportoSostitutiva.ToString.PadLeft(Len(ImportoSostitutiva) + 1, "0")) + CDec(ImportoSanzioni.ToString.PadLeft(Len(ImportoSanzioni) + 1, "0")) + CDec(ImportoInteressi.ToString.PadLeft(Len(ImportoInteressi) + 1, "0"))
                                    'Else
                                    '    ImportoGestionale = CDec(ImportoRegistro.ToString.PadLeft(Len(ImportoRegistro) + 1, "0")) + CDec(ImportoSostitutiva.ToString.PadLeft(Len(ImportoSostitutiva) + 1, "0")) + CDec(ImportoInteressi.ToString.PadLeft(Len(ImportoInteressi) + 1, "0"))
                                    'End If

                                    If ImportoGestionale > 0 And ricevutaCaricata = False Then
                                        ScriviGestionale(IdC, DataRegistrazione, TipoTributo, ImportoGestionale, CDec(ImportoSostitutiva.ToString.PadLeft(Len(ImportoSostitutiva) + 1, "0")))

                                        TextBox1.Visible = True
                                        TextBox1.Text = TextBox1.Text & vbCrLf & "OPERAZIONE EFFETTUATA!" & vbCrLf & "Sono stati importati i dati relativi a 1 rapporto."
                                    Else
                                        If ricevutaCaricata = True Then
                                            par.myTrans.Rollback()
                                            par.OracleConn.Close()
                                            TextBox1.Visible = True
                                            TextBox1.Text = TextBox1.Text & vbCrLf & "OPERAZIONE NON EFFETTUATA!" & vbCrLf & "Ricevuta già caricata per il rapporto con identificativo " & IdC & "."
                                            Exit Try
                                        End If
                                    End If


                                End If
                            Else
                                File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName))
                                If FileUpload2.FileName <> "" Then
                                    File.Delete(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload2.FileName))
                                End If
                            End If
                            par.myTrans.Commit()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        Catch ex As Exception

                            par.myTrans.Rollback()
                            par.OracleConn.Close()
                            TextBox1.Visible = True
                            TextBox1.Text = TextBox1.Text & vbCrLf & ex.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!"

                        End Try



                    Else
                        TextBox1.Visible = True
                        TextBox1.Text = sErrore
                    End If
                End If
                HiddenField1.Value = "1"
            End If

        Catch ex1 As HttpUnhandledException

            lblErrore.Visible = True
            lblErrore.Text = ex1.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!"
        End Try
    End Sub
End Class
