Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_REG_PREGRESSE_RicevuteImposteAnteAler
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim errore As Boolean
        Dim sErrore As String = ""
        Dim NumeroProtocollo As String = ""
        Dim DataRegistrazione As String = ""

        Dim dtRisultati As New Data.DataTable
        Dim rowDT As System.Data.DataRow

        Try
            If HiddenField1.Value = "0" Then
                If FileUpload1.HasFile = True Then
                    dtRisultati.Columns.Add("NOME_FILE")
                    dtRisultati.Columns.Add("DESCRIZIONE")
                    dtRisultati.Columns.Add("ESITO")
                    errore = False

                    btnExport.Visible = True

                    If UCase(Mid(FileUpload1.FileName, Len(FileUpload1.FileName) - 2, 3)) <> "REL" Then
                        sErrore = sErrore & "Errore: Tipo file non valido! E' richiesto un file .rel"
                        errore = True
                    End If

                    If errore = False Then
                        Dim NomeFile As String = ""

                        Dim sr1 As StreamReader
                        NomeFile = Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName)
                        FileUpload1.SaveAs(Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload1.FileName))
                        sr1 = New StreamReader(NomeFile, System.Text.Encoding.Default)
                        Try
                            par.OracleConn.Open()
                            par.SettaCommand(par)
                            par.myTrans = par.OracleConn.BeginTransaction()
                            ‘‘par.cmd.Transaction = par.myTrans


                            Dim NomeFilePDF As String = ""



                            If FileUpload2.FileName <> "" Then
                                FileUpload2.SaveAs(Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\RICEVUTE\" & FileUpload2.FileName))
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
                            'Dim NomedelFile As String = ""
                            Dim DataInvioAE As String = ""
                            Dim AnnoRiferimento As String = ""
                            Dim ImportoRegistro As String = ""
                            Dim ImportoSostitutiva As String = ""
                            Dim ImportoSanzioni As String = ""
                            Dim ImportoInteressi As String = ""
                            Dim ImportoTotale As String = ""
                            Dim CodiceTributo As String = ""
                            Dim SCARTATI As String = ""
                            Dim NumScartati As Integer = 0
                            Dim CodiceRegistro As String = ""
                            Dim NumProtocolloAE As String = ""
                            Dim BUONO As Boolean = True
                            Dim MOTIVAZIONISCARTO As String = ""
                            Dim AnnoImposta As String = ""
                            Dim NomeFileAE As String = ""

                            Contenuto = sr1.ReadLine
                            NumProtocolloAE = Trim(Mid(Contenuto, 1, 17))
                            CodiceRegistro = Trim(Mid(Contenuto, 69, 3))
                            DataInvioAE = par.AggiustaData(Trim(Mid(Contenuto, InStr(UCase(Contenuto), "IN DATA") + 8, 10)))
                            NomeFileAE = Trim(Mid(Contenuto, 69, 18))

                            If InStr(Contenuto, "Il file e' stato scartato con la seguente motivazione:") > 0 Then
                                rowDT = dtRisultati.NewRow()
                                rowDT.Item("NOME_FILE") = NomeFileAE & ".xml"
                                rowDT.Item("DESCRIZIONE") = Trim(Mid(Contenuto, InStr(Contenuto, "Il file e' stato scartato con la seguente motivazione:") + 54, 100))
                                rowDT.Item("ESITO") = "FILE SCARTATO DA A.E."
                                dtRisultati.Rows.Add(rowDT)
                                SCARTATI = "File completamente scartato."

                                'elimino da imposte in modo che possano essere ripresi tutti i contratti in questo file
                                Comando = "DELETE FROM siscom_mi.RAPPORTI_UTENZA_IMPOSTE WHERE RECUPERO=1 AND UPPER(FILE_SCARICATO)='" & UCase(NomeFileAE) & ".XML'"
                                par.cmd.CommandText = Comando
                                par.cmd.ExecuteNonQuery()

                            End If

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
                                        NumRegistrazione = Trim(Mid(Contenuto, InStr(Contenuto, "n.") + 3, 6))
                                        par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where UPPER(cod_ufficio_reg)='" & UCase(CodiceRegistro) & "' AND SISCOM_MI.TONUMBER(NUM_REGISTRAZIONE)=" & UCase(NumRegistrazione)
                                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReader.HasRows = False Then
                                            BUONO = False
                                            NumScartati = NumScartati + 1

                                            rowDT = dtRisultati.NewRow()
                                            rowDT.Item("NOME_FILE") = NomeFileAE & ".xml"
                                            rowDT.Item("DESCRIZIONE") = "Il rapporto cod.ufficio registrazione " & CodiceRegistro & " registrato al n. " & NumRegistrazione & " Non è stato trovato in SepaWeb"
                                            rowDT.Item("ESITO") = "SCARTATO DA SEPAWEB"
                                            dtRisultati.Rows.Add(rowDT)

                                            'MOTIVAZIONISCARTO = MOTIVAZIONISCARTO & vbCrLf & "Il rapporto cod.ufficio registrazione " & CodiceRegistro & " registrato al n. " & NumRegistrazione & " Non è stato trovato in SepaWeb"
                                        Else
                                            If myReader.Read Then
                                                IdC = myReader("id")
                                            End If
                                        End If
                                        myReader.Close()

                                        If BUONO = True Then

                                            ImportoRegistro = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di registro") + 19, 18))
                                            ImportoSostitutiva = Trim(Mid(Contenuto, InStr(Contenuto, "Imposta di bollo") + 16, 18))
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
                                                AnnoImposta = par.AggiustaData(Trim(Mid(Contenuto, InStr(Contenuto, "risoluzione di un contratto di locazione in data") + 49, 10)))
                                                AnnoRiferimento = ""
                                            End If

                                            Comando = "INSERT INTO siscom_mi.RAPPORTI_UTENZA_RICEVUTE (ID_CONTRATTO,NOME_FILE_REL,PG_AE,COD_TRIBUTO,ANNO,NOME_FILE_PDF,DATA_REGISTRAZIONE,REGISTRO,SOSTITUTIVA,SANZIONI,INTERESSI,TOTALE,VALIDA_FINO_AL) VALUES (" & IdC & ",'" & FileUpload1.FileName & "','" & NumProtocolloAE & "','" & CodiceTributo & "','" & AnnoImposta & "','" & NomeFilePDF & "','" & DataInvioAE & "','" & ImportoRegistro & "','" & ImportoSostitutiva & "','" & ImportoSanzioni & "','" & ImportoInteressi & "','" & ImportoTotale & "','" & AnnoRiferimento & "')"
                                            par.cmd.CommandText = Comando
                                            par.cmd.ExecuteNonQuery()

                                            par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IdC & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F57','')"
                                            par.cmd.ExecuteNonQuery()

                                            rowDT = dtRisultati.NewRow()
                                            rowDT.Item("NOME_FILE") = NomeFileAE & ".xml"
                                            rowDT.Item("DESCRIZIONE") = "Pagamento effettuato per il rapporto cod.ufficio registrazione " & CodiceRegistro & " registrato al n. " & NumRegistrazione
                                            rowDT.Item("ESITO") = "PAGAMENTO EFFETTUATO"
                                            dtRisultati.Rows.Add(rowDT)

                                            i = i + 1
                                        End If
                                    Else
                                        rowDT = dtRisultati.NewRow()
                                        rowDT.Item("NOME_FILE") = NomeFileAE & ".xml"
                                        rowDT.Item("DESCRIZIONE") = Replace(Trim(Mid(Contenuto, InStr(Contenuto, "La richiesta si riferisce al contratto di locazione:"), 235)) & " " & Trim(Mid(Contenuto, InStr(Contenuto, "sono risultati i seguenti errori:"), 240)), "  ", "")
                                        rowDT.Item("ESITO") = "SCARTATO DA A.E."
                                        dtRisultati.Rows.Add(rowDT)

                                        NumRegistrazione = Trim(Mid(Contenuto, InStr(Contenuto, "n.") + 3, 6))
                                        par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza where UPPER(cod_ufficio_reg)='" & UCase(CodiceRegistro) & "' AND SISCOM_MI.TONUMBER(NUM_REGISTRAZIONE)=" & UCase(NumRegistrazione)
                                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                        If myReader.HasRows = False Then
                                            BUONO = False
                                            NumScartati = NumScartati + 1
                                            rowDT = dtRisultati.NewRow()
                                            rowDT.Item("NOME_FILE") = NomeFileAE & ".xml"
                                            rowDT.Item("DESCRIZIONE") = "Il rapporto cod.ufficio registrazione " & CodiceRegistro & " registrato al n. " & NumRegistrazione & " Non è stato trovato in SepaWeb"
                                            rowDT.Item("ESITO") = "SCARTATO DA SEPAWEB"
                                            dtRisultati.Rows.Add(rowDT)
                                        Else
                                            If myReader.Read Then
                                                IdC = myReader("id")
                                                'elimino da imposte in modo che possa poi essere ripreso in un altro file
                                                Comando = "DELETE FROM siscom_mi.RAPPORTI_UTENZA_IMPOSTE WHERE RECUPERO=1 AND UPPER(FILE_SCARICATO)='" & UCase(NomeFileAE) & ".XML'  and id_contratto=" & IdC
                                                par.cmd.CommandText = Comando
                                                par.cmd.ExecuteNonQuery()
                                            End If
                                        End If
                                        myReader.Close()

                                        

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

                            If NumScartati <> 0 Then
                                SCARTATI = "Sono stati scartati " & NumScartati & " contratti."
                            End If
                            lblDescrizione.Text = "OPERAZIONE EFFETTUATA! Sono stati importati i dati relativi a " & i & " rapporti. " & SCARTATI
                            dgRisultati.DataSource = dtRisultati
                            dgRisultati.DataBind()
                            Session.Add("MIADT", dtRisultati)

                        Catch ex As Exception
                            sr1.Close()
                            par.myTrans.Rollback()
                            par.OracleConn.Close()

                            rowDT = dtRisultati.NewRow()
                            If InStr(ex.Message, "Il processo non può accedere al file") > 0 Then
                                rowDT.Item("NOME_FILE") = FileUpload1.FileName
                                rowDT.Item("DESCRIZIONE") = "Questo file è stato già esaminato"
                                rowDT.Item("ESITO") = "OPERAZIONE ANNULLATA DA SEPAWEB"
                                dtRisultati.Rows.Add(rowDT)
                                btnExport.Visible = False
                            Else
                                rowDT.Item("NOME_FILE") = FileUpload1.FileName
                                rowDT.Item("DESCRIZIONE") = ex.Message
                                rowDT.Item("ESITO") = "OPERAZIONE ANNULLATA DA SEPAWEB"
                                dtRisultati.Rows.Add(rowDT)
                            End If
                            

                            dgRisultati.DataSource = dtRisultati
                            dgRisultati.DataBind()

                            HiddenField1.Value = "0"
                        End Try



                    Else
                        rowDT = dtRisultati.NewRow()
                        rowDT.Item("NOME_FILE") = FileUpload1.FileName
                        rowDT.Item("DESCRIZIONE") = sErrore
                        rowDT.Item("ESITO") = "SCARTATO DA SEPAWEB"
                        dtRisultati.Rows.Add(rowDT)
                        dgRisultati.DataSource = dtRisultati
                        dgRisultati.DataBind()
                    End If
                End If
                HiddenField1.Value = "1"
            End If

        Catch ex1 As HttpUnhandledException

            lblErrore.Visible = True
            lblErrore.Text = ex1.Message & vbCrLf & "OPERAZIONE ANNULLATA!!!!"
            btnExport.Visible = False
        End Try
    End Sub


    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim DT As New Data.DataTable

            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            If DT.Rows.Count > 0 Then

                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(DT, dgRisultati, "ExportConvocabili", , False, , False)

                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
