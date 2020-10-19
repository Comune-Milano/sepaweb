Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
'12/01/2015 PUCCIA Nuova connessione  tls ssl
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security


Partial Class Contratti_Morosita_CreaLettere
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim pp As New MavOnline.MAVOnlineBeanService
    Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
    Dim Esito As New MavOnline.rispostaMAVOnlineWS
    Dim binaryData() As Byte
    Dim outFile As System.IO.FileStream
    Dim outputFileName As String = ""
    Dim myExcelFile As New CM.ExcelFile
    Dim sNomeFile As String

    '12/01/2015 PUCCIA Nuova connessione  tls ssl
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim CodiceContratto As String = ""
        Dim ScadenzaPagamento As String = ""
        Dim presso_cor As String = ""
        Dim civico_cor As String = ""
        Dim luogo_cor As String = ""
        Dim cap_cor As String = ""
        Dim indirizzo_cor As String = ""
        Dim tipo_cor As String = ""
        Dim sigla_cor As String = ""
        Dim TipoIngiunzione As String = ""
        Dim Importo As String = "0"

        Dim IdAnagrafica As String = "-1"
        Dim Str As String = ""
        Dim Operatore As String = ""
        Dim VOCE As String = ""
        Dim ScadenzaBollettino As String = ""
        Dim Riassunto As String = ""
        Dim num_bollettino As String = ""
        Dim contenutoRiassunto As String = ""
        Dim idMorosita As Long = 0

        Dim MiaSHTML As String
        Dim MIOCOLORE As String
        Dim i As Integer
        Dim ElencoFile() As String

        Dim j As Integer
        Dim periodo As String
        Dim Condominio As String = ""
        Dim DIRIGENTE As String = ""
        Dim RESPONSABILE As String = ""
        Dim TRATTATADA As String = ""

        Dim APPLICABOLLO As Double = 0
        Dim SPESEmav As Double = 0
        Dim BOLLO As Double = 0
        Dim spese_notifica As Double = 0
        Dim Tot_Bolletta As Double = 0

        Dim IndiceMorosita As String = ""
        Dim indiceCondominio As String = ""


        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                IndiceMorosita = Request.QueryString("IDMOR")
                indiceCondominio = Request.QueryString("IDCOND")

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans




                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()

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

                par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=34"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    spese_notifica = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                End If
                myReaderA.Close()


                Dim xx As String = "Morosita_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss")
                sNomeFile = xx
                xx = xx & ".pdf"

                par.cmd.CommandText = "SELECT COND_MOROSITA_LETTERE.* FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE BOLLETTINO IS NULL and id_morosita = " & IndiceMorosita
                Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1234.HasRows = False Then
                    myReader1234.Close()


                    MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='450px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                    i = 0
                    MIOCOLORE = "#CCFFFF"
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("..\..\ALLEGATI\MOROSITA_CONDOMINI\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & IndiceMorosita & "-*.zip")
                        ReDim Preserve ElencoFile(i)
                        ElencoFile(i) = foundFile
                        i = i + 1
                    Next

                    Dim kk As Long
                    Dim jj As Long
                    Dim scambia

                    For kk = 0 To i - 2
                        For jj = kk + 1 To i - 1
                            If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                                scambia = ElencoFile(kk)
                                ElencoFile(kk) = ElencoFile(jj)
                                ElencoFile(jj) = scambia
                            End If
                        Next
                    Next


                    If i > 0 Then
                        For j = 0 To i - 1
                            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/MOROSITA_CONDOMINI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                            MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                            If MIOCOLORE = "#CCFFFF" Then
                                MIOCOLORE = "#FFFFCC"
                            Else
                                MIOCOLORE = "#CCFFFF"
                            End If
                            If j = 10 Then Exit For
                        Next j
                    End If
                    MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                    Response.Write(MiaSHTML)
                    Exit Sub
                End If
                myReader1234.Close()

                Riassunto = "<table style='width:100%;'>"
                Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COD.CONTRATTO</td><td>INDIRIZZO</td><td>COGN./RAG.SOCIALE</td><td>NOME</td><td>PERIODO DI RIF.</td><td>EMISSIONE</td><td>SCADENZA</td><td>N.BOLLETTINO</td><td>IMPORTO</td><td>SPESE</td></tr>"
                Riassunto = Riassunto & "<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>"

                par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=32"
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.Read Then
                    causalepagamento.Value = par.IfNull(myReader123("valore"), "")
                End If
                myReader123.Close()


                Condominio = ""
                DIRIGENTE = ""
                RESPONSABILE = ""
                TRATTATADA = ""

                par.cmd.CommandText = "select condomini.*,comuni_nazioni.nome as comune from siscom_mi.condomini,siscom_mi.cond_morosita, sepa.comuni_nazioni where condomini.cod_comune = comuni_nazioni.cod(+) and condomini.id=cond_morosita.id_condominio and cond_morosita.id=" & IndiceMorosita
                myReader123 = par.cmd.ExecuteReader()
                If myReader123.Read Then
                    Condominio = par.IfNull(myReader123("denominazione"), "") & " - " & par.IfNull(myReader123("comune"), "")

                    DIRIGENTE = par.IfNull(myReader123("TITOLO_DIRIGENTE"), "") & " " & par.IfNull(myReader123("COGNOME_DIRIGENTE"), "") & " " & par.IfNull(myReader123("NOME_DIRIGENTE"), "")
                    RESPONSABILE = par.IfNull(myReader123("TITOLO_RESPONSABILE"), "") & " " & par.IfNull(myReader123("COGNOME_RESPONSABILE"), "") & " " & par.IfNull(myReader123("NOME_RESPONSABILE"), "") & "  Tel. " & par.IfNull(myReader123("TELEFONO_RESPONSABILE"), "")
                    If par.IfNull(myReader123("COGNOME_TRATTATA"), "") <> "" And par.IfNull(myReader123("NOME_TRATTATA"), "") <> "" Then
                        TRATTATADA = par.IfNull(myReader123("TITOLO_TRATTATA"), "") & " " & par.IfNull(myReader123("COGNOME_TRATTATA"), "") & " " & par.IfNull(myReader123("NOME_TRATTATA"), "") & "  Tel. " & par.IfNull(myReader123("TELEFONO_TRATTATA"), "")

                    End If

                End If
                myReader123.Close()

                Dim idedificio As String = "0"
                Dim idcomplesso As String = "0"


                Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
                pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

                Dim Licenza As String = Session.Item("LicenzaPdfMerge")
                If Licenza <> "" Then
                    pdfMerge.LicenseKey = Licenza
                End If

                Dim sr2 As StreamReader = New StreamReader(Server.MapPath("Elenco.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                contenutoRiassunto = sr2.ReadToEnd()
                sr2.Close()

                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Ingiunzione.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenutoOriginale As String = sr1.ReadToEnd()
                sr1.Close()

                Dim K As Integer = 2
                'inizio a scrivere il file xls
                With myExcelFile

                    .CreateFile(Server.MapPath("..\..\FileTemp\") & sNomeFile & ".xls")
                    .PrintGridLines = False
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                    .SetDefaultRowHeight(14)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                    .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "TITOLO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOMINATIVO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INDIRIZZO", 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CAP-CITTA", 0)



                    Dim Contenuto As String = ""
                    par.cmd.CommandText = "SELECT COND_MOROSITA_LETTERE.*,anagrafica.cognome,anagrafica.nome,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.COND_MOROSITA_LETTERE WHERE ANAGRAFICA.ID=COND_MOROSITA_LETTERE.ID_ANAGRAFICA AND BOLLETTINO IS NULL and id_morosita = " & IndiceMorosita
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader.Read

                        If Len(par.IfNull(myReader("PARTITA_IVA"), 0)) = 11 Or par.ControllaCF(par.IfNull(myReader("COD_FISCALE"), 0)) = True Then
                            idMorosita = par.IfNull(myReader("ID_MOROSITA"), 0)
                            Contenuto = contenutoOriginale
                            Tot_Bolletta = 0

                            CodiceContratto = par.IfNull(myReader("COD_CONTRATTO"), "")
                            TipoIngiunzione = "RIMBORSO SPESE CONDOMINIALI"
                            VOCE = "626"
                            Importo = par.IfNull(myReader("Importo"), "0,00")
                            IdAnagrafica = par.IfNull(myReader("id_anagrafica"), "")

                            par.cmd.CommandText = "select complessi_immobiliari.id as idcomplesso,edifici.id as idedificio,siscom_mi.rapporti_utenza.*,unita_contrattuale.id_unita from SISCOM_MI.EDIFICI,siscom_mi.complessi_immobiliari,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.rapporti_utenza where complessi_immobiliari.id=edifici.id_complesso and unita_immobiliari.id=unita_contrattualE.id_unita and edifici.id=unita_immobiliari.id_edificio and unita_contrattuale.id_contratto=rapporti_utenza.id and unita_contrattuale.id_unita_principale is null and cod_contratto='" & CodiceContratto & "'"
                            myReader123 = par.cmd.ExecuteReader()
                            If myReader123.Read Then
                                idcontratto.Value = par.IfNull(myReader123("id"), "-1")
                                idunita.Value = par.IfNull(myReader123("id_unita"), "-1")
                                presso_cor = par.IfNull(myReader123("presso_cor"), "")
                                luogo_cor = par.IfNull(myReader123("luogo_cor"), "")
                                civico_cor = par.IfNull(myReader123("civico_cor"), "")
                                cap_cor = par.IfNull(myReader123("cap_cor"), "")
                                indirizzo_cor = par.IfNull(myReader123("VIA_cor"), "")
                                tipo_cor = par.IfNull(myReader123("tipo_cor"), "")
                                sigla_cor = par.IfNull(myReader123("sigla_cor"), "")
                                idedificio = par.IfNull(myReader123("idedificio"), "0")
                                idcomplesso = par.IfNull(myReader123("idcomplesso"), "0")
                            End If
                            myReader123.Close()

                            Dim Titolo As String = ""
                            Dim Nome As String = ""
                            Dim Cognome As String = ""
                            Dim CF As String = ""

                            Dim ID_BOLLETTA As Long = 0

                            par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & IdAnagrafica
                            myReader123 = par.cmd.ExecuteReader()
                            If myReader123.Read Then
                                If par.IfNull(myReader123("ragione_sociale"), "") <> "" Then
                                    Titolo = ""
                                    Cognome = par.IfNull(myReader123("ragione_sociale"), "")
                                    Nome = ""
                                    CF = par.IfNull(myReader123("partita_iva"), "")
                                Else
                                    If par.IfNull(myReader123("sesso"), "") = "M" Then
                                        Titolo = "Sign."
                                    Else
                                        Titolo = "Sign.ra"
                                    End If
                                    Cognome = par.IfNull(myReader123("cognome"), "")
                                    Nome = par.IfNull(myReader123("nome"), "")
                                    CF = par.IfNull(myReader123("cod_fiscale"), "")
                                End If
                            End If
                            myReader123.Close()

                            ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            periodo = par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))
                            Contenuto = Replace(Contenuto, "$titolo$", Titolo)
                            Contenuto = Replace(Contenuto, "$condominio$", Condominio)

                            'xxx
                            If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
                                Contenuto = Replace(Contenuto, "$nominativo$", Cognome & " " & Nome & "<br />presso " & presso_cor)
                            Else
                                Contenuto = Replace(Contenuto, "$nominativo$", presso_cor)
                            End If


                            Contenuto = Replace(Contenuto, "$indirizzo$", indirizzo_cor & ", " & civico_cor & "</br>" & cap_cor & " " & luogo_cor & " " & sigla_cor)
                            Contenuto = Replace(Contenuto, "$codicecontratto$", CodiceContratto)
                            Contenuto = Replace(Contenuto, "$tipo$", TipoIngiunzione)
                            Contenuto = Replace(Contenuto, "$importo$", Format(CDbl(Importo), "##,##0.00"))

                            Dim SpNotifica As Double = 0
                            If Importo + spese_notifica + SPESEmav >= APPLICABOLLO Then
                                Contenuto = Replace(Contenuto, "$importo1$", Format(CDbl(Importo + spese_notifica + SPESEmav + BOLLO), "##,##0.00"))
                                SpNotifica = CDbl(spese_notifica + SPESEmav + BOLLO)
                            Else
                                Contenuto = Replace(Contenuto, "$importo1$", Format(CDbl(Importo + spese_notifica + SPESEmav), "##,##0.00"))
                                SpNotifica = CDbl(spese_notifica + SPESEmav)

                            End If

                            '**********peppe modify 21/10/2010***********
                            Contenuto = Replace(Contenuto, "$importospnotifica$", Format(SpNotifica, "##,##0.00"))

                            Contenuto = Replace(Contenuto, "$responsabile$", RESPONSABILE)
                            Contenuto = Replace(Contenuto, "$dirigente$", DIRIGENTE)
                            Contenuto = Replace(Contenuto, "$trattatada$", TRATTATADA)


                            Contenuto = Replace(Contenuto, "$trattatada$", Operatore)
                            Contenuto = Replace(Contenuto, "$periodo$", par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")))
                            Contenuto = Replace(Contenuto, "$titolo$", par.FormattaData(par.IfNull(myReader("emissione"), "")))
                            Contenuto = Replace(Contenuto, "$emissione$", par.FormattaData(par.IfNull(myReader("emissione"), "")))
                            'Contenuto = Replace(Contenuto, "$scadenzabollettino$", par.FormattaData(ScadenzaBollettino))



                            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                            sr.WriteLine(Contenuto)
                            sr.Close()

                            Dim url As String = Server.MapPath("..\..\FileTemp\Ingiunzione_") & CodiceContratto
                            Dim pdfConverter1 As PdfConverter = New PdfConverter
                            Dim urlPdf As String
                            Licenza = Session.Item("LicenzaHtmlToPdf")
                            If Licenza <> "" Then
                                pdfConverter1.LicenseKey = Licenza
                            End If


                            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                            pdfConverter1.PdfDocumentOptions.ShowHeader = False
                            pdfConverter1.PdfDocumentOptions.ShowFooter = False
                            pdfConverter1.PdfDocumentOptions.LeftMargin = 40
                            pdfConverter1.PdfDocumentOptions.RightMargin = 40
                            pdfConverter1.PdfDocumentOptions.TopMargin = 30
                            pdfConverter1.PdfDocumentOptions.BottomMargin = 30
                            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                            pdfConverter1.PdfDocumentOptions.ShowHeader = False
                            pdfConverter1.PdfFooterOptions.FooterText = ("")
                            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                            pdfConverter1.PdfFooterOptions.PageNumberText = ""
                            pdfConverter1.PdfFooterOptions.ShowPageNumber = False
                            For Num As Integer = 1 To 3
                                urlPdf = url & "_Cp" & Num
                                pdfConverter1.SavePdfFromUrlToFile(url & ".htm", urlPdf & ".pdf")
                            Next

                            Dim Nome1 As String = ""
                            Dim Nome2 As String = ""

                            If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
                                Nome1 = Cognome & " " & Nome
                                Nome2 = presso_cor
                            Else
                                Nome1 = presso_cor
                            End If

                            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                        & "Values " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999 , '" & Format(Now, "yyyyMMdd") _
                                        & "', '" & ScadenzaBollettino & "', NULL,NULL,NULL,'RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "'," _
                                        & "" & idcontratto.Value _
                                        & " ," & par.RicavaEsercizioCorrente & ", " _
                                        & idunita.Value _
                                        & ", '0', ''," & IdAnagrafica _
                                        & ", '" & par.PulisciStrSql(Nome1) & "', " _
                                        & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) _
                                        & "', '" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") _
                                        & "', '" & par.PulisciStrSql(Nome2) & "', '" & par.IfNull(myReader("inizio_periodo"), "") _
                                        & "', '" & par.IfNull(myReader("inizio_periodo"), "") & "', " _
                                        & "'0', " & idcomplesso & ", '', NULL, '', " _
                                        & Year(Now) & ", '', " & idedificio & ", NULL, NULL,'MOR',7)"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderB.Read Then
                                ID_BOLLETTA = myReaderB(0)
                            Else
                                ID_BOLLETTA = -1
                            End If
                            myReaderB.Close()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & VOCE _
                            & "," & par.VirgoleInPunti(Importo) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + Importo

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",628" _
                            & "," & par.VirgoleInPunti(spese_notifica) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + spese_notifica


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                                    & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()

                            Tot_Bolletta = Tot_Bolletta + SPESEmav

                            If Tot_Bolletta >= APPLICABOLLO Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                            & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + BOLLO
                            End If



                            'If Session.Item("AmbienteDiTest") = "1" Then
                            '    causalepagamento.Value = "COMMITEST01"
                            '    'pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
                            '    pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

                            'End If
                            If Session.Item("AmbienteDiTest") = "1" Then
                                causalepagamento.Value = "COMMITEST01"
                                'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                                pp.Url = Session.Item("indirizzoMavOnLine")
                            Else
                                'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                                pp.Url = Session.Item("indirizzoMavOnLine")
                            End If

                            RichiestaEmissioneMAV.codiceEnte = "commi"
                            RichiestaEmissioneMAV.tipoPagamento = causalepagamento.Value
                            RichiestaEmissioneMAV.idOperazione = Format(ID_BOLLETTA, "0000000000")
                            RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(IdAnagrafica), "0000000000")

                            RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA)

                            RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                            RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                            RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                            RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                            RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                            'If Nome <> "" Then
                            RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                            'End If


                            If Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor) <= 23 Then
                                RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                            Else
                                RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                                RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                            End If

                            RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                            RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                            RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                            RichiestaEmissioneMAV.nazioneDebitore = "IT"

                            '/*/*/*/*/*tls v1
                            Dim v As String = ""
                            par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                            v = par.cmd.ExecuteScalar
                            System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                            '/*/*/*/*/*tls v1


                            '12/01/2015 PUCCIA Nuova connessione  tls ssl
                            System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler
                            Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)
                            If Esito.codiceRisultato = "0" Then

                                For Num As Integer = 1 To 3
                                    urlPdf = url & "_Cp" & Num
                                    pdfMerge.AppendPDFFile(urlPdf & ".pdf")

                                Next
                                IO.File.Delete(url & ".htm")

                                outputFileName = Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                                binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length - 1)
                                outFile.Close()

                                pdfMerge.AppendPDFFile(outputFileName)
                                num_bollettino = Esito.numeroMAV
                                par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & num_bollettino & "' where  id=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 9pt;'><td style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & CodiceContratto & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & indirizzo_cor & ", " & civico_cor & " " & cap_cor & " " & luogo_cor & " " & sigla_cor & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Cognome & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Nome & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("emissione"), "")) & " </td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(ScadenzaBollettino) & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & num_bollettino & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Importo), "##,##0.00") & "</td><td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Tot_Bolletta - Importo), "##,##0.00") & "</td></tr>"
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(Titolo))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(Cognome & " " & Nome))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(indirizzo_cor & ", " & civico_cor))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(cap_cor & " " & luogo_cor & " " & sigla_cor))
                                K = K + 1

                                par.cmd.CommandText = "UPDATE  siscom_mi.COND_MOROSITA_LETTERE SET BOLLETTINO = '" & num_bollettino & "' where id_anagrafica=" & IdAnagrafica & " and id_morosita=" & idMorosita
                                par.cmd.ExecuteNonQuery()
                                ' Response.Redirect("ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf")

                            Else
                                'lblErrore.Visible = True

                                par.cmd.CommandText = "delete from siscom_mi.bol_bollette where id=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='../../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")
                                outputFileName = Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
                                binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length)
                                outFile.Close()
                            End If




                        Else
                            If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("ragione_sociale"), "") & " non sono stati stampati perchè la partita iva non ha un formato corretto!</p>")

                            Else
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & " non sono stati stampati perchè il codice fiscale non ha un formato corretto!</p>")

                            End If
                        End If
                    Loop
                    myReader.Close()
                    .CloseFile()
                End With

                Riassunto = Riassunto & "</table>"
                contenutoRiassunto = Replace(contenutoRiassunto, "$riassunto$", Riassunto)
                contenutoRiassunto = Replace(contenutoRiassunto, "$periodo$", periodo)

                Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                sr3.WriteLine(contenutoRiassunto)
                sr3.Close()


                Dim url1 As String = Server.MapPath("..\..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita
                Dim pdfConverter As PdfConverter = New PdfConverter

                Licenza = Session.Item("LicenzaHtmlToPdf")
                If Licenza <> "" Then
                    pdfConverter.LicenseKey = Licenza
                End If


                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfDocumentOptions.ShowFooter = False
                pdfConverter.PdfDocumentOptions.LeftMargin = 30
                pdfConverter.PdfDocumentOptions.RightMargin = 30
                pdfConverter.PdfDocumentOptions.TopMargin = 30
                pdfConverter.PdfDocumentOptions.BottomMargin = 30
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfFooterOptions.FooterText = ("")
                pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                pdfConverter.PdfFooterOptions.DrawFooterLine = False
                pdfConverter.PdfFooterOptions.PageNumberText = ""
                pdfConverter.PdfFooterOptions.ShowPageNumber = False
                pdfConverter.SavePdfFromUrlToFile(url1 & ".htm", url1 & ".pdf")

                pdfMerge.AppendPDFFile(url1 & ".pdf")
                IO.File.Delete(url1 & ".htm")


                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\..\FileTemp\") & xx)

                If num_bollettino <> "" Then

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String


                    zipfic = Server.MapPath("..\..\ALLEGATI\MOROSITA_CONDOMINI\" & sNomeFile & ".zip")

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    'scrivo file xls
                    Dim strFile As String
                    strFile = Server.MapPath("..\..\FileTemp\" & sNomeFile & ".xls")
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

                    'scrivo file pdf
                    strFile = Server.MapPath("..\..\FileTemp\") & xx
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                    Dim sFile1 As String = Path.GetFileName(strFile)
                    theEntry = New ZipEntry(sFile1)
                    fi = New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer1)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                    File.Delete(strFile)

                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()


                    'Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")


                    Response.Write("</br>E' stato creato il file contenente le lettere e i bollettini.</br><a href='../../ALLEGATI/MOROSITA_CONDOMINI/" & sNomeFile & ".zip' target='_blank'>Clicca qui per visualizzare il file</a>")
                Else
                    Response.Write("</br>")
                End If


            Catch ex As Exception
                Response.Write(ex.Message)
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    Private Function CreaCausale(ByVal Tipo As String, ByVal idb As Long) As String
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            sCausale = ""

            par.cmd.CommandText = "select t_voci_bolletta.descrizione,bol_bollette_voci.importo from siscom_mi.bol_bollette,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where bol_bollette_voci.id_bolletta=bol_bollette.id and t_voci_bolletta.id=bol_bollette_voci.id_voce and bol_bollette.id=" & idb & " order by t_voci_bolletta.descrizione asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                sImporto = Format(par.IfNull(myReader("importo"), "0"), "##,##0.00")

                'If sImporto < 1 And sImporto > 0 Then
                '    sImporto = Format(CDbl(sImporto), "0.00")
                'End If

                If sImporto < 1 And sImporto > 0 Then
                    sImporto = "0" & sImporto
                End If

                If sImporto > -1 And sImporto < 0 Then
                    sImporto = "-0" & Replace(sImporto, "-", "")
                End If

                iDifferenza = 55 - Len(sImporto)
                sDescrizione = par.IfNull(myReader("descrizione"), "")
                sCausale = sCausale & Mid(sDescrizione.PadRight(iDifferenza), 1, iDifferenza) & sImporto
            Loop
            CreaCausale = sCausale
            myReader.Close()
            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'lblErrore.Visible = True
            'lblErrore.Text = ex.Message
            'Button1.Visible = False
        End Try

    End Function

End Class
