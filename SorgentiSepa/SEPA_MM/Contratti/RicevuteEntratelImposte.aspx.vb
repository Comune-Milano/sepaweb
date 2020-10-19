Imports System.IO

Partial Class Contratti_RicevuteEntratelImposte
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

        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim errore As Boolean
        Dim sErrore As String = ""
        Dim NumeroProtocollo As String = ""
        Dim DataRegistrazione As String = ""
        Dim Contenuto As String = ""
        Try
            If HiddenField1.Value = "0" Then
                If FileUpload1.HasFile = True Then
                    TextBox1.Visible = False
                    errore = False
                    TextBox1.Text = ""
                    If UCase(Mid(FileUpload1.FileName, Len(FileUpload1.FileName) - 2, 3)) <> "REL" Then
                        sErrore = sErrore & "Errore: Tipo file non valido! E' richiesto un file .rel"
                        errore = True
                    End If

                    If errore = False Then
                        Dim sr1 As StreamReader
                        Try
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                            par.myTrans = par.OracleConn.BeginTransaction()
                            '‘par.cmd.Transaction = par.myTrans

                            Dim NomeFile As String = ""
                            Dim NomeFilePDF As String = ""

                            FileUpload1.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName))

                            If FileUpload2.FileName <> "" Then
                                FileUpload2.SaveAs(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload2.FileName))
                                NomeFilePDF = FileUpload2.FileName
                            End If

                            NomeFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName)


                            sr1 = New StreamReader(NomeFile, System.Text.Encoding.Default)

                            Dim Leggi As Boolean = True
                            Dim Comando As String = ""
                            Dim IdC As String = ""
                            Dim annoRegistrazione As String = ""
                            Dim NumRegistrazione As String = ""
                            Dim i As Integer = 0
                            Dim Tipo As String = ""
                            Dim sTipo As String = ""
                            'Dim NomedelFile As String = ""
                            Dim DataInvioAE As String = ""
                            Dim AnnoRiferimento As String = ""
                            Dim ImportoRegistro As String = "0,00"
                            Dim ImportoSostitutiva As String = "0,00"
                            Dim ImportoSanzioni As String = "0,00"
                            Dim ImportoInteressi As String = "0,00"
                            Dim ImportoTotale As String = "0,00"
                            Dim ImportoGestionale As Decimal = 0
                            Dim CodiceTributo As String = ""
                            Dim SCARTATI As String = ""
                            Dim NumScartati As Integer = 0
                            Dim CodiceRegistro As String = ""
                            Dim NumProtocolloAE As String = ""
                            Dim BUONO As Boolean = True
                            Dim MOTIVAZIONISCARTO As String = ""
                            Dim AnnoImposta As String = ""
                            Dim PERC_CONDUTTORE As Double = 0
                            Dim ricevutaCaricata As Boolean = False

                            Contenuto = sr1.ReadLine
                            NumProtocolloAE = Trim(Mid(Contenuto, 1, 17))
                            CodiceRegistro = Trim(Mid(Contenuto, 69, 3))
                            DataInvioAE = par.AggiustaData(Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IN DATA") + 8, 10)))

                            Do Until Contenuto = ""
                                Contenuto = sr1.ReadLine
                                If Len(Contenuto) > 0 Then
                                    If InStr(UCase(Contenuto), "RICEVUTA DI SCARTO") = 0 Then
                                        ImportoRegistro = ""
                                        ImportoSostitutiva = ""
                                        ImportoSanzioni = ""
                                        ImportoInteressi = ""
                                        ImportoTotale = ""
                                        CodiceTributo = ""
                                        AnnoImposta = ""

                                        BUONO = True
                                        'Tipo = UCase(Mid(NomedelFile, 1, 1))

                                        annoRegistrazione = Replace(Trim(Mid(Contenuto, InStr(Contenuto, "-anno") + 5, 6)), ",", "")


                                        NumRegistrazione = Trim(Mid(Contenuto, InStr(Contenuto, "n.") + 3, 6))

                                        par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where substr(nvl(data_reg,'29991231'),1,4)='" & annoRegistrazione & "' and UPPER(cod_ufficio_reg)='" & UCase(CodiceRegistro) & "' AND SISCOM_MI.TONUMBER(NUM_REGISTRAZIONE)=" & UCase(NumRegistrazione)
                                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReader.HasRows = False Then
                                            BUONO = False
                                            NumScartati = NumScartati + 1
                                                MOTIVAZIONISCARTO = MOTIVAZIONISCARTO & vbCrLf & "Il rapporto utenza con anno registrazione " & annoRegistrazione & " registrato al n. " & NumRegistrazione & " Non è stato trovato in SepaWeb"
                                        Else
                                            If myReader.Read Then
                                                IdC = myReader("id")
                                            End If
                                        End If
                                        myReader.Close()

                                        If BUONO = True Then

                                            ImportoRegistro = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di registro") + 19, 18))
                                            ImportoSostitutiva = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di bollo") + 16, 18))
                                            If InStr(UCase(Contenuto), "SANZIONI REGISTRO") > 0 Then
                                                ImportoSanzioni = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "SANZIONI REGISTRO") + 17, 20))
                                            Else
                                                ImportoSanzioni = "0,00"
                                            End If
                                            If InStr(UCase(Contenuto), "INTERESSI") > 0 Then
                                                ImportoInteressi = Trim(Mid(Contenuto, InStr(UCase(Contenuto), "INTERESSI") + 9, 28))
                                            Else
                                                ImportoInteressi = "0,00"
                                            End If
                                            ImportoTotale = ""
                                            CodiceTributo = ""
                                            If InStr(UCase(Contenuto), "PROROGA DI UN CONTRATTO") > 0 Then
                                                CodiceTributo = "114T"
                                                AnnoRiferimento = par.AggiustaData(Trim(Mid(Contenuto, InStr(Contenuto, "fino alla data") + 15, 10)))
                                                AnnoImposta = Mid(DataInvioAE, 1, 4)
                                            End If

                                            If InStr(LCase(Contenuto), "pagamento annualita' successive per l'anno") > 0 Then
                                                CodiceTributo = "112T"
                                                AnnoRiferimento = Trim(Mid(Contenuto, InStr(Contenuto, "successive per l'anno") + 22, 4))
                                                AnnoImposta = AnnoRiferimento
                                                AnnoRiferimento = CStr(CInt(AnnoRiferimento) + 1)
                                            End If

                                            If InStr(LCase(Contenuto), "risoluzione di un contratto") > 0 Then
                                                CodiceTributo = "113T"
                                                AnnoImposta = Mid(par.AggiustaData(Trim(Mid(Contenuto, InStr(Contenuto, "risoluzione di un contratto di locazione in data") + 49, 10))), 1, 4)
                                                AnnoRiferimento = ""
                                            End If

                                            par.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE where id_contratto=" & IdC & " and anno=" & Mid(DataInvioAE, 1, 4) & " and PG_AE='" & NumProtocolloAE & "' AND COD_TRIBUTO='" & CodiceTributo & "' AND VALIDA_FINO_AL='" & AnnoRiferimento & "'"
                                            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                            If myReaderA.Read Then
                                                ricevutaCaricata = True
                                            End If
                                            myReaderA.Close()

                                            Comando = "INSERT INTO siscom_mi.RAPPORTI_UTENZA_RICEVUTE (ID_CONTRATTO,NOME_FILE_REL,PG_AE,COD_TRIBUTO,ANNO,NOME_FILE_PDF,DATA_REGISTRAZIONE,REGISTRO,SOSTITUTIVA,SANZIONI,INTERESSI,TOTALE,VALIDA_FINO_AL,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NumProtocolloAE & "','" & CodiceTributo & "','" & AnnoImposta & "','" & NomeFilePDF & "','" & DataInvioAE & "','" & ImportoRegistro & "','" & ImportoSostitutiva & "','" & ImportoSanzioni & "','" & ImportoInteressi & "','" & ImportoTotale & "','" & AnnoRiferimento & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                            par.cmd.CommandText = Comando
                                            par.cmd.ExecuteNonQuery()

                                            par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F57','')"
                                            par.cmd.ExecuteNonQuery()

                                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=2 WHERE anno='" & AnnoImposta & "' and ID_FASE_REGISTRAZIONE IN (0,1) AND COD_TRIBUTO ='" & CodiceTributo & "' AND ID_CONTRATTO=" & IdC
                                            par.cmd.ExecuteNonQuery()

                                            i = i + 1

                                            ImportoGestionale = CDec(ImportoRegistro)
                                            If CodiceTributo <> "113T" Then
                                                par.cmd.CommandText = "select tipologia_contratto_locazione.* from siscom_mi.tipologia_contratto_locazione where cod=(SELECT COD_TIPOLOGIA_CONTR_LOC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IdC & ")"
                                                Dim myReaderFF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                                If myReaderFF.Read Then
                                                    PERC_CONDUTTORE = par.IfNull(myReaderFF("perc_conduttore"), 0)
                                                    ImportoGestionale = (ImportoGestionale * PERC_CONDUTTORE) / 100
                                                End If
                                                myReaderFF.Close()
                                            End If
                                            'If IsNumeric(ImportoSanzioni) Then
                                            '    ImportoGestionale = CDec(ImportoRegistro) + CDec(ImportoSostitutiva) + CDec(ImportoSanzioni) + CDec(ImportoInteressi)
                                            'Else
                                            '    ImportoGestionale = CDec(ImportoRegistro) + CDec(ImportoSostitutiva) + CDec(ImportoInteressi)
                                            'End If

                                            If ImportoGestionale > 0 And ricevutaCaricata = False Then
                                                ScriviGestionale(IdC, DataInvioAE, CodiceTributo, ImportoGestionale)
                                            End If
                                        End If
                                    Else
                                            annoRegistrazione = Replace(Trim(Mid(Contenuto, InStr(Contenuto, "-anno") + 5, 6)), ",", "")
                                        NumRegistrazione = Trim(Mid(Contenuto, InStr(Contenuto, "n.") + 3, 6))

                                        If InStr(UCase(Contenuto), "PROROGA DI UN CONTRATTO") > 0 Then
                                            AnnoImposta = Mid(DataInvioAE, 1, 4)
                                            CodiceTributo = "114T"
                                        End If

                                        If InStr(LCase(Contenuto), "pagamento annualita' successive per l'anno") > 0 Then
                                            AnnoImposta = Trim(Mid(Contenuto, InStr(Contenuto, "successive per l'anno") + 22, 4))
                                            CodiceTributo = "112T"
                                        End If

                                        If InStr(LCase(Contenuto), "risoluzione di un contratto") > 0 Then
                                            CodiceTributo = "113T"
                                            AnnoImposta = Mid(par.AggiustaData(Trim(Mid(Contenuto, InStr(Contenuto, "risoluzione di un contratto di locazione in data") + 49, 10))), 1, 4)
                                        End If

                                        par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where substr(nvl(data_reg,'29991231'),1,4)='" & annoRegistrazione & "' and UPPER(cod_ufficio_reg)='" & UCase(CodiceRegistro) & "' AND SISCOM_MI.TONUMBER(NUM_REGISTRAZIONE)=" & UCase(NumRegistrazione)
                                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReader.HasRows = True Then
                                            If myReader.Read Then
                                                IdC = myReader("id")

                                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F02','ANNULLO XML ADEMPIMENTI SUCCESSIVO PER SCARTO A.E.')"
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE SET ID_FASE_REGISTRAZIONE=3,NOTE='" & par.PulisciStrSql(Trim(Mid(Contenuto, InStr(Contenuto, "sono risultati i seguenti errori:"), 240))) & "' WHERE anno='" & AnnoImposta & "' and ID_FASE_REGISTRAZIONE IN (0,1) AND COD_TRIBUTO ='" & CodiceTributo & "' AND ID_CONTRATTO=" & IdC
                                                par.cmd.ExecuteNonQuery()

                                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE_SC (ID_CONTRATTO,NOME_FILE_REL,NOME_FILE_PDF,NOTE,DATA_INSERIMENTO) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NomeFilePDF & "','" & Mid(par.PulisciStrSql(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta si riferisce al contratto di locazione:"), 235)) & " " & Trim(Mid(Contenuto, InStr(Contenuto, "sono risultati i seguenti errori:"), 240))), 1, 4000) & "','" & Format(Now, "yyyyMMddHHmmss") & "')"
                                                par.cmd.ExecuteNonQuery()
                                            End If
                                        End If
                                        myReader.Close()

                                        MOTIVAZIONISCARTO = MOTIVAZIONISCARTO & vbCrLf & Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta si riferisce al contratto di locazione:"), 235)) & " " & Trim(Mid(Contenuto, InStr(Contenuto, "sono risultati i seguenti errori:"), 240))
                                        NumScartati = NumScartati + 1
                                    End If
                                Else
                                    Contenuto = ""
                                End If
                            Loop


                            sr1.Close()
                            'File.Delete(NomeFile)
                            par.myTrans.Commit()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            TextBox1.Visible = True
                            If NumScartati <> 0 Then
                                SCARTATI = vbCrLf & "Sono stati scartati " & NumScartati & " contratti." & vbCrLf & MOTIVAZIONISCARTO
                            End If
                            TextBox1.Text = TextBox1.Text & vbCrLf & "OPERAZIONE EFFETTUATA!" & vbCrLf & "Sono stati importati i dati relativi a " & i & " rapporti." & SCARTATI


                        Catch ex As Exception
                            sr1.Close()
                            par.myTrans.Rollback()
                            par.OracleConn.Close()
                            TextBox1.Visible = True
                            TextBox1.Text = TextBox1.Text & vbCrLf & ex.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!" & vbCrLf & "Errore alla riga " & Contenuto
                            HiddenField1.Value = "0"
                        End Try



                    Else
                        TextBox1.Visible = True
                        TextBox1.Text = sErrore
                    End If
                End If
                ' HiddenField1.Value = "1"
            End If

        Catch ex1 As HttpUnhandledException

            lblErrore.Visible = True
            lblErrore.Text = ex1.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!"
        End Try
    End Sub

    Private Sub ScriviGestionale(ByVal IdContratto As Long, ByVal dataRegistrazione As String, ByVal codTributo As String, ByVal importo As Decimal)

        Dim tipoImposta As String = ""
        Dim idBollGest As Long = 0
        Dim idUI As Long = 0
        Dim idAnagrafica As Long = 0

        par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL FROM DUAL"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            idBollGest = myReader(0)
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT id_unita from siscom_mi.unita_contrattuale where id_unita_principale is null and id_contratto=" & IdContratto
        myReader = par.cmd.ExecuteReader()
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
            Case "112T"
                tipoImposta = "annualità successiva"
            Case "113T"
                tipoImposta = "risoluzione"
            Case "114T"
                tipoImposta = "proroga"
        End Select

        par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE ) " _
                            & "VALUES (" & idBollGest & "," & IdContratto & "," & par.RicavaEsercizioCorrente() & "," & idUI & "," & idAnagrafica & "," _
                            & "'" & dataRegistrazione & "','" & dataRegistrazione & "'," & par.VirgoleInPunti(importo) & "," _
                            & "'" & dataRegistrazione & "','" & dataRegistrazione & "'," _
                            & "'" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.IfEmpty(par.FormattaData(dataRegistrazione), Format(Now, "dd/MM/yyyy"))))) & "'" _
                            & ",117,'N'," & Session.Item("ID_OPERATORE") & ",'Imposta di registro per " & tipoImposta & "')"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",93," & par.VirgoleInPunti(importo) & ")"
        par.cmd.ExecuteNonQuery()

    End Sub
End Class
