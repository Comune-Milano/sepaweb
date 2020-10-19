Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_PrintLetter392
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            idDichiarazione.Value = Request.QueryString("IDD")
            If Request.QueryString("RD") = "1" Then
                RiconosciDebito()
            Else
                RisoluzioneConsensuale()
            End If
        End If
    End Sub

    Private Sub RisoluzioneConsensuale()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\RisoluzioneConsensuale.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim CodContratto As String = ""
            Dim idContr As Long = Request.QueryString("IDC")

            par.cmd.CommandText = "select * from utenza_comp_nucleo where progr=0 and id_dichiarazione=" & idDichiarazione.Value
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                contenuto = Replace(contenuto, "$INTESTATARIO$", par.IfNull(lettore0("COGNOME"), 0) & " " & par.IfNull(lettore0("NOME"), 0))
            End If
            lettore0.Close()

            par.cmd.CommandText = "SELECT anagrafica.ID AS IDAN,(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                & " UNITA_IMMOBILIARI.cod_tipo_livello_piano,RAPPORTI_UTENZA.*,COMUNE_RESIDENZA,CIVICO_RESIDENZA,(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno, indirizzi.localita " _
                & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SISCOM_MI.UNITA_IMMOBILIARI.ID= UNITA_CONTRATTUALE.ID_UNITA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO =UNITA_CONTRATTUALE.ID_CONTRATTO AND " _
                & "RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  AND scale_edifici.ID(+) = unita_immobiliari.id_scala AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO =" & idContr & ""
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If lettore.Read Then

                'contenuto = Replace(contenuto, "$INTESTATARIO$", par.IfNull(lettore("INTECONTRATTO"), 0))
                contenuto = Replace(contenuto, "$INTERNO$", par.IfNull(lettore("interno"), 0))
                contenuto = Replace(contenuto, "$CODFISCALE$", par.IfNull(lettore("CFIVA"), 0))
                contenuto = Replace(contenuto, "$PIANO$", par.IfNull(lettore("cod_tipo_livello_piano"), 0))
                contenuto = Replace(contenuto, "$INDIRIZZO$", par.IfNull(lettore("descrizione"), 0))
                contenuto = Replace(contenuto, "$CIVICO$", par.IfNull(lettore("civico"), 0))
                contenuto = Replace(contenuto, "$LOCALITA$", par.IfNull(lettore("LOCALITA"), 0))
                contenuto = Replace(contenuto, "$LUOGORESIDENZA$", par.IfNull(lettore("COMUNE_RESIDENZA"), ""))
                contenuto = Replace(contenuto, "$INDIRIZZORES$", par.IfNull(lettore("TIPO_COR"), "") & " " & par.IfNull(lettore("VIA_COR"), "") & ", " & par.IfNull(lettore("CIVICO_RESIDENZA"), ""))

                par.cmd.CommandText = "SELECT NOME FROM COMUNI_NAZIONI WHERE COD = (SELECT SUBSTR((COD_FISCALE),12,4) AS COD FROM SISCOM_MI.anagrafica WHERE ID=" & lettore("IDAN") & ")"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader2.Read Then
                    contenuto = Replace(contenuto, "$LUOGONASC$", par.IfNull(myReader2("NOME"), ""))
                End If
                myReader2.Close()

                contenuto = Replace(contenuto, "$DATASTIPULA$", par.FormattaData(par.IfNull(lettore("DATA_STIPULA"), 0)))
                contenuto = Replace(contenuto, "$DURATA$", par.IfNull(lettore("DURATA_ANNI"), ""))
                contenuto = Replace(contenuto, "$CANONE$", "€" & Format(CDec(par.IfNull(lettore("IMP_CANONE_INIZIALE"), 0)), "##,##0.00"))
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=8"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$LOCATORE$", par.IfNull(myReader("valore"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=10"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                contenuto = Replace(contenuto, "$CFLOCATORE$", par.IfNull(myReader("valore"), ""))
            End If
            myReader.Close()

            Dim sedelocatore As String = ""
            
            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=11"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                'Località
                sedelocatore = par.IfNull(myReader("valore"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=19"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                'Provincia
                sedelocatore = sedelocatore & " (" & par.IfNull(myReader("valore"), "") & ") "
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=20"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                'Indirizzo
                sedelocatore = sedelocatore & par.IfNull(myReader("valore"), "") & " "
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=21"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                'Civico
                sedelocatore = sedelocatore & par.IfNull(myReader("valore"), "") & " "
            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$SEDELOCATORE$", sedelocatore)

            contenuto = caricaRespFiliale(idContr, contenuto)

            
            contenuto = Replace(contenuto, "$DATAOGGI$", Format(Now, "dd/MM/yyyy"))


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "RisoluzioneConsensuale" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            Dim NomeZipfile As String = Format(Now, "yyyyMMddHHmmss") & "_008_" & CodContratto
            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\" & NomeZipfile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & nomefile)
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
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            Response.Redirect("..\FileTemp\" & nomefile, False)
        Catch ex As Exception
            Session.Add("ERRORE", "RisoluzioneConsensuale:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Function caricaRespFiliale(ByVal idContra As String, ByVal conten As String, Optional Tipo As Integer = 0) As String

        Dim Responsabile As String = ""
        Dim Acronimo As String = ""
        Dim dataPresent As String = ""
        Dim DataNascitaResp As String = ""
        Dim LuogoNascitaResp As String = ""

        Dim Percorso As String = "../" & Session.Item("Firme_Responsabili")

        Select Case Tipo
            Case 1
                Percorso = "../" & Session.Item("Firme_Responsabili")
        End Select

        par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr, indirizzi.civico,indirizzi.cap, indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.FILIALI_UI WHERE unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto =" & idContra & " AND UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID AND indirizzi.ID = tab_filiali.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND INIZIO_VALIDITA <='" & Format(Now, "yyyyMMdd") & "' AND FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "'"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If myReader.Read Then
            conten = Replace(conten, "$TESTORAPPRESENTATO$", par.IfNull(myReader("PROCURA"), ""))
            conten = Replace(conten, "$testorappresentato1$", par.IfNull(myReader("PROCURA1"), ""))

            Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
            conten = Replace(conten, "$responsabile$", par.IfNull(myReader("responsabile"), ""))
            conten = Replace(conten, "$luogonascitaresp$", PrimaGrande(par.IfNull(myReader("luogo_nascita_resp"), "")))
            conten = Replace(conten, "$datanascitaresp$", Format(CDate(par.IfNull(myReader("data_nascita_resp"), "")), "dd MMMM yyyy"))
            If par.IfNull(myReader("firma"), "") <> "" Then
                conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & par.IfNull(myReader("firma"), "") & "' />")
            Else
                conten = Replace(conten, "$firmaresponsabile$", "")
            End If
        Else
            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContra & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader2.Read Then
                conten = Replace(conten, "$testorappresentato$", par.IfNull(myReader("PROCURA"), ""))
                conten = Replace(conten, "$testorappresentato1$", par.IfNull(myReader("PROCURA1"), ""))
                Responsabile = par.IfNull(myReader2("RESPONSABILE"), "")
                conten = Replace(conten, "$responsabile$", par.IfNull(myReader2("responsabile"), ""))
                conten = Replace(conten, "$luogonascitaresp$", PrimaGrande(par.IfNull(myReader("luogo_nascita_resp"), "")))
                conten = Replace(conten, "$datanascitaresp$", Format(CDate(par.IfNull(myReader("data_nascita_resp"), "")), "dd MMMM yyyy"))
                If par.IfNull(myReader2("firma"), "") <> "" Then
                    conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & par.IfNull(myReader("firma"), "") & "' />")
                Else
                    conten = Replace(conten, "$firmaresponsabile$", "")
                End If
            Else
                Responsabile = ""
                conten = Replace(conten, "$responsabile$", Responsabile)
                conten = Replace(conten, "$firmaresponsabile$", "")
                conten = Replace(conten, "$luogonascitaresp$", "")
                conten = Replace(conten, "$datanascitaresp$", "")
            End If
            myReader2.Close()
        End If
        myReader.Close()

        conten = Replace(conten, "$responsabile$", "")
        conten = Replace(conten, "$firmaresponsabile$", "")
        conten = Replace(conten, "$luogonascitaresp$", "")
        conten = Replace(conten, "$datanascitaresp$", "")

        Return conten

    End Function

    Private Function PrimaGrande(ByVal testo As String) As String
        PrimaGrande = StrConv(testo, vbProperCase)
    End Function

    Private Sub RiconosciDebito()
        Try
            If Not String.IsNullOrEmpty(Request.QueryString("IDBOLL")) Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\RiconoscDebito392.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                par.cmd.CommandText = "select * from utenza_comp_nucleo where progr=0 and id_dichiarazione=" & idDichiarazione.Value
                Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore0.Read Then
                    contenuto = Replace(contenuto, "$INTESTATARIO$", par.IfNull(lettore0("COGNOME"), 0) & " " & par.IfNull(lettore0("NOME"), 0))
                End If
                lettore0.Close()

                Dim idRU As Long = 0
                Dim CodContratto As String = ""
                par.cmd.CommandText = "SELECT (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                    & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA, INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,scale_edifici.descrizione AS scala,unita_immobiliari.interno, indirizzi.localita " _
                    & "FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,siscom_mi.scale_edifici " _
                    & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = BOL_BOLLETTE.ID_CONTRATTO " _
                    & "AND UNITA_IMMOBILIARI.ID = BOL_BOLLETTE.ID_UNITA AND " _
                    & "SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID  AND scale_edifici.ID(+) = unita_immobiliari.id_scala AND BOL_BOLLETTE.ID IN (" & Session.Item("IDBOLLETTE") & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                If lettore.Read Then

                    'contenuto = Replace(contenuto, "$INTESTATARIO$", par.IfNull(lettore("INTECONTRATTO"), 0))

                    contenuto = Replace(contenuto, "$NUMALLOGGIO$", par.IfNull(lettore("interno"), 0))


                    contenuto = Replace(contenuto, "$INDIRIZZO$", par.IfNull(lettore("descrizione"), 0))

                    contenuto = Replace(contenuto, "$CIVICO$", par.IfNull(lettore("civico"), 0))

                    contenuto = Replace(contenuto, "$LOCALITA$", par.IfNull(lettore("LOCALITA"), 0))

                End If
                lettore.Close()


                par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.BOL_BOLLETTE WHERE RAPPORTI_UTENZA.ID = BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID IN (" & Session.Item("IDBOLLETTE") & ")"
                lettore = par.cmd.ExecuteReader

                If lettore.Read Then
                    contenuto = Replace(contenuto, "$CODCONTRATTO$", par.IfNull(lettore("COD_CONTRATTO"), 0))
                    CodContratto = par.IfNull(lettore("COD_CONTRATTO"), 0)
                    idRU = par.IfNull(lettore("ID_CONTRATTO"), 0)
                End If
                lettore.Close()

                Dim ElencoComp1 As String = ""
                Dim ElencoComp2 As String = ""
                Dim dataDisdetta As String = Format(Now, "yyyyMMdd")

                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & idDichiarazione.Value
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    dataDisdetta = par.IfNull(lettore("data_disdetta_392"), Format(Now, "yyyyMMdd"))
                End If
                lettore.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE PROGR<>0 AND ID_DICHIARAZIONE=" & idDichiarazione.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtComp As New Data.DataTable
                da.Fill(dtComp)
                Dim conta As Integer = 0
                For Each rComp As Data.DataRow In dtComp.Rows

                    If par.RicavaEta(par.IfNull(rComp.Item("DATA_NASCITA"), dataDisdetta)) >= 18 Then
                        ElencoComp1 = ElencoComp1 & par.IfNull(rComp.Item("COGNOME"), "") & " " & par.IfNull(rComp.Item("NOME"), "") & ""
                        ElencoComp2 = ElencoComp2 & par.IfNull(rComp.Item("COGNOME"), "") & " " & par.IfNull(rComp.Item("NOME"), "") & ""

                        If dtComp.Rows.Count > 1 And conta <> dtComp.Rows.Count Then
                            ElencoComp1 = ElencoComp1 & " - "
                            ElencoComp2 = ElencoComp2 & ";<br/>"
                        End If
                        conta = conta + 1
                    End If
                    If conta = dtComp.Rows.Count And dtComp.Rows.Count > 1 Then
                        ElencoComp1 = Mid(ElencoComp1, 1, Len(ElencoComp1) - 3) & ", "
                    End If
                    If conta = dtComp.Rows.Count And dtComp.Rows.Count > 1 Then
                        ElencoComp2 = Mid(ElencoComp2, 1, Len(ElencoComp2) - 6)
                    End If
                Next

                If ElencoComp1 <> "" Then
                    contenuto = Replace(contenuto, "$COMPONENTI$", "ed il/i sottoscritto/i " & ElencoComp1 & " componente/i maggiorenne/i del nucleo famigliare dell'intestatario,")
                Else
                    contenuto = Replace(contenuto, "$COMPONENTI$", "")
                End If

                contenuto = Replace(contenuto, "$ELENCOCOMPONENTI$", ElencoComp2)

                If ElencoComp2 <> "" Then
                    contenuto = Replace(contenuto, "$TITOLOCOMPONENTI$", "Il/I componente/i del nucleo")
                Else
                    contenuto = Replace(contenuto, "$TITOLOCOMPONENTI$", "")
                End If

                'par.cmd.CommandText = "SELECT SUM ((NVL (IMPORTO_TOTALE, 0) - NVL(bol_bollette.QUOTA_SIND_B,0)) -( NVL (IMPORTO_PAGATO, 0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0))) AS DEBITO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID IN (" & Session.Item("IDBOLLETTE") & ")"
                'lettore = par.cmd.ExecuteReader
                'If lettore.Read Then
                '    contenuto = Replace(contenuto, "$TOTBOLLETTA$", Format(par.IfNull(lettore("DEBITO"), 0), "##,##0.00"))
                'End If
                'lettore.Close()
                Dim saldo As Decimal = 0
                Dim credito As Decimal = 0
                Dim debito As Decimal = 0

                debito = par.CalcolaSaldoAttuale(idRU)

                par.cmd.CommandText = "select sum(importo_totale) AS SALDOGEST from siscom_mi.bol_bollette_gest where tipo_Applicazione='N' and id_tipo<>55 and id_Contratto=" & idRU
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                credito = par.IfNull(par.cmd.ExecuteScalar, 0)

                saldo = debito + credito
                contenuto = Replace(contenuto, "$TOTBOLLETTA$", Format(saldo, "##,##0.00"))

                contenuto = Replace(contenuto, "$DATAOGGI$", Format(Now, "dd/MM/yyyy"))


                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


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
                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
                pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
                pdfConverter1.PdfFooterOptions.ShowPageNumber = True

                Dim nomefile As String = "RiconosciDebito" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\NuoveImm\"))



                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                Dim NomeZipfile As String = Format(Now, "yyyyMMddHHmmss") & "_008_" & CodContratto
                zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\" & NomeZipfile & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                '
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & nomefile)
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
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                'File.Delete(strFile)


                ' Response.Write("<script>window.open('../FileTemp/" & nomefile & "','RiconosciDebito','');self.close();</script>")
                Response.Redirect("..\FileTemp\" & nomefile, False)

            Else
                Response.Write("ERRORE MANCA BOLLETTA")

            End If
        Catch ex As Exception

            Session.Add("ERRORE", "RiconosciDebito:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
End Class
