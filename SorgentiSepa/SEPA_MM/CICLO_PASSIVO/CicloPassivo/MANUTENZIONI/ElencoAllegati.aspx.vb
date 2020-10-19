Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip

Partial Class Contratti_ElencoAllegati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'If Not IsPostBack Then

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim MiaSHTML As String
            Dim MiaSHTML_Stampa As String = ""
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile() as string

            Dim j As Integer
            Dim Conduttore As String = ""




            par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI where SISCOM_MI.MANUTENZIONI.ID = " & Request.QueryString("cod")

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Label1.Text = "Manutenzione ODL: " & par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
            End If
            myReader1.Close()

            'Label1.Text = Label1.Text & "<br/>Intestatario/i:" & Conduttore

            'par.cmd.CommandText = "select unita_immobiliari.cod_tipologia,complessi_immobiliari.id as idq,COMPLESSI_IMMOBILIARI.ID_QUARTIERE,edifici.id as idf,edifici.fl_piano_vendita,EDIFICI.GEST_RISC_DIR,edifici.condominio,tipo_livello_piano.descrizione as miopiano,unita_immobiliari.scala,indirizzi.cap,comuni_nazioni.nome as comune,unita_immobiliari.id_destinazione_uso,SISCOM_MI.UNITA_IMMOBILIARI.interno,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,indirizzi.descrizione,indirizzi.civico,indirizzi.cap from siscom_mi.tipo_livello_piano,comuni_nazioni,siscom_mi.indirizzi,SISCOM_MI.UNITA_IMMOBILIARI,siscom_mi.edifici,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO AND edifici.id=unita_immobiliari.id_edificio and unita_immobiliari.cod_tipo_livello_piano=tipo_livello_piano.cod (+) and indirizzi.cod_comune=comuni_nazioni.cod (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and unita_immobiliari.cod_unita_immobiliare='" & Mid(Request.QueryString("cod"), 1, 17) & "'"
            'Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader3.Read Then
            '    Label1.Text = Label1.Text & "<br/>Indirizzo: " & par.IfNull(myReader3("descrizione"), "") & ", " & par.IfNull(myReader3("civico"), "") _
            '    & "</br>" & par.IfNull(myReader3("cap"), "") & " " & par.IfNull(myReader3("comune"), "") & "</br>Interno: " & par.IfNull(myReader3("interno"), "") & " Scala: " & par.IfNull(myReader3("scala"), "") & " Piano: " & par.IfNull(myReader3("miopiano"), "")
            'End If
            'myReader3.Close()

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='900px'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='150px'><font size='2' face='Arial'>Data Documento</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='200px'><font size='2' face='Arial'>Tipo Allegato</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='350px'><font size='2' face='Arial'>Descrizione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Download</font></td>" & vbCrLf                'solo lettura
            If Request.QueryString("LT") <> "1" Then
                MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Elimina</font></td>" & vbCrLf
            End If

            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            'MiaSHTML_Stampa = "CONTRATTO CODICE " & Request.QueryString("cod") & " - Data Stampa:" & Format(Now, "dd/MM/yyyy") & "<br/>" & Label1.Text & "<br/><br/>Elenco Allegati<br/><br/>"
            MiaSHTML_Stampa = "Data Stampa:" & Format(Now, "dd/MM/yyyy") & "<br/>" & Label1.Text & "<br/><br/>Elenco Allegati<br/><br/>"
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<tr>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='10%'><font size='2' face='Arial'>Data Documento</font></td>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='20%'><font size='2' face='Arial'>Tipo Allegato</font></td>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='70%'><font size='2' face='Arial'>Descrizione</font></td>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "</tr>" & vbCrLf


            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../../../ALLEGATI/MANUTENZIONI/"), FileIO.SearchOption.SearchTopLevelOnly, "*_" & Request.QueryString("cod") & ".zip")
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = foundFile
                i = i + 1
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), 1, 8)) < CLng(Mid(RicavaFile(ElencoFile(jj)), 1, 8)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            '0300030080200X29903


            If i > 0 Then
                For j = 0 To i - 1
                    'j = i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(UCase(Mid(RicavaFile(ElencoFile(j)), 1, 8))) & "</font></td>"


                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<tr>" & vbCrLf
                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='10%'><font size='1' face='Arial'>" & par.FormattaData(UCase(Mid(RicavaFile(ElencoFile(j)), 1, 8))) & "</font></td>"

                    par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MODELLI_ALLEGATI WHERE COD='" & UCase(Mid(RicavaFile(ElencoFile(j)), 10, 3)) & "'"
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = True Then
                        If myReader5.Read Then
                            MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='90%'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                        End If
                    Else
                        MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>NON DEFINITO</font></td>" & vbCrLf
                        MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='90%'><font size='1' face='Arial'>NON DEFINITO</font></td>" & vbCrLf
                    End If
                    myReader5.Close()

                    Dim erroreTrasferimento As Boolean = False
                    Dim DESCRIZIONE_ALLEGATO As String = ""

                    Try
                        Dim inStream As New ZipInputStream(File.OpenRead(ElencoFile(j)))
                        Dim outStream As FileStream
                        'Dim entry As ZipEntry
                        Dim buff(2047) As Byte
                        Dim bytes As Integer
                        Dim entry As ICSharpCode.SharpZipLib.Zip.ZipEntry

                        Do While True
                            Try
                                entry = inStream.GetNextEntry()
                                If entry Is Nothing Then

                                    Exit Do
                                End If
                                Try
                                    If InStr(UCase(entry.Name), "_DESCRIZIONE.TXT") > 0 Then
                                        outStream = File.Create(Server.MapPath("../../../FileTemp/") & entry.Name, 2048)
                                        Do While True
                                            bytes = inStream.Read(buff, 0, 2048)
                                            If bytes = 0 Then

                                                Exit Do
                                            End If
                                            outStream.Write(buff, 0, bytes)
                                        Loop
                                        outStream.Close()

                                        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("../../../FileTemp/") & entry.Name, System.Text.Encoding.GetEncoding("iso-8859-1"))
                                        Dim contenuto As String = sr1.ReadToEnd()
                                        sr1.Close()

                                        DESCRIZIONE_ALLEGATO = Mid(contenuto, 32, Len(contenuto))
                                    End If
                                Catch
                                End Try

                            Catch ex As ZipException
                                DESCRIZIONE_ALLEGATO = ""
                                Response.Write(ex.Message)
                            End Try
                        Loop
                        inStream.Close()
                    Catch ex As Exception
                        DESCRIZIONE_ALLEGATO = "Errore...file corrotto in fase di trasferimento!"
                        erroreTrasferimento = True
                    End Try


                    MiaSHTML = MiaSHTML & "<td width='350px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & DESCRIZIONE_ALLEGATO & "</font></td>" & vbCrLf
                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='70%'><font size='1' face='Arial'>" & DESCRIZIONE_ALLEGATO & "</font></td>" & vbCrLf

                    If erroreTrasferimento = False Then
                        MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a  alt='Download' href='../../../ALLEGATI/MANUTENZIONI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'><img src='../../../ImmMaschere/MenuTopDownload.gif' border='0'></a></font></td>" & vbCrLf
                        If Request.QueryString("LT") <> "1" Then
                            MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><a href='CancellaAllegato.aspx?NOME=" & RicavaFile(ElencoFile(j)) & "&EXT=ZIP' target='_blank'><img border='0' src='../../../NuoveImm/Elimina.png'></a></td>" & vbCrLf
                        End If
                    Else
                        MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'></font></td>" & vbCrLf
                        If Request.QueryString("LT") <> "1" Then
                            MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'></td>" & vbCrLf
                        End If
                    End If


                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If
                    'If j = 10 Then Exit For
                Next j
            End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "</table>" & vbCrLf
            Label3.Text = MiaSHTML
            Label4.Text = MiaSHTML_Stampa


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label3.Text = ex.Message
        End Try

    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim NomeFile As String = "Allegati_" & Format(Now, "yyyyMMddHHmmss")


        Dim url As String = NomeFile
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

        Dim pdfConverter As PdfConverter = New PdfConverter

        If Licenza <> "" Then
            pdfConverter.LicenseKey = Licenza
        End If

        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter.PdfDocumentOptions.ShowHeader = False
        pdfConverter.PdfDocumentOptions.ShowFooter = False
        pdfConverter.PdfDocumentOptions.LeftMargin = 20
        pdfConverter.PdfDocumentOptions.RightMargin = 20
        pdfConverter.PdfDocumentOptions.TopMargin = 5
        pdfConverter.PdfDocumentOptions.BottomMargin = 5
        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter.PdfDocumentOptions.ShowHeader = False
        pdfConverter.PdfFooterOptions.FooterText = ("")
        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
        pdfConverter.PdfFooterOptions.DrawFooterLine = False
        pdfConverter.PdfFooterOptions.PageNumberText = ""
        pdfConverter.PdfFooterOptions.ShowPageNumber = False


        pdfConverter.SavePdfFromHtmlStringToFile(Label4.Text, Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
        Response.Write("<script>window.open('../../../FileTemp/" & NomeFile & ".pdf','Stampa','');</script>")

    End Sub
End Class
