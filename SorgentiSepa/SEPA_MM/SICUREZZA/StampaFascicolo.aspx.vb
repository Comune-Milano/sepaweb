Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports Telerik.Web.UI

Partial Class SICUREZZA_StampaFascicolo
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                idFascicolo.Value = Request.QueryString("IDF")
                CreaFascicolo()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Function RicavaBarCode39(ByVal Codice As String, ByVal DoveSalvare As String, Optional ByVal BarHeight As Integer = 40, Optional ByVal ImageWidth As Integer = 480, Optional ByVal ImageHeight As Integer = 40) As String
        Try
            Dim NomeFile As String = "CodeBar_" & Codice & "_" & Format(Now, "yyyyMMddHHmmss") & ".jpg"
            Dim codeBarImage As New System.Drawing.Bitmap(ImageWidth, ImageHeight)
            Dim barcode As New iTextSharp.text.pdf.Barcode39
            barcode.Code = Codice
            barcode.StartStopText = False
            barcode.Extended = False
            barcode.BarHeight = 28.0F
            barcode.Size = 12.0F
            barcode.N = 3.20000005F
            barcode.Baseline = 12.0F
            barcode.X = 1.09000003F
            codeBarImage = barcode.CreateDrawingImage(Color.Black, Color.White)
            codeBarImage.Save(System.Web.HttpContext.Current.Server.MapPath("~\" & DoveSalvare & "\") & NomeFile, System.Drawing.Imaging.ImageFormat.Jpeg)
            RicavaBarCode39 = NomeFile
        Catch ex As Exception
            RicavaBarCode39 = ""
        End Try
    End Function

    Private Sub CreaFascicolo()
        Dim Selezionato As Boolean = False
        Dim TabellaElenco As String = ""
        Dim TabellaElenco2 As String = ""
        Dim NomeFile As String = ""
        Dim DaStampare As Boolean = True
        Dim IdUnita As String = ""
        Dim CodiceProcesso As String = ""
        Dim IndiceProcesso As String = ""
        Dim Progressivo As Integer = 0
        Dim BarCodeDaStampare As String = ""
        Dim Data_Ora_Stampa As String = Format(Now, "yyyyMMddHHmmss")
        Dim FileCodice As String = ""
        Dim idFascBC As Long = 0
        Dim codUI As String = ""
        Try
            connData.apri(True)

            TabellaElenco = ""
            TabellaElenco2 = ""

            par.cmd.CommandText = "select tipo_intervento.descrizione as tipoInt,interventi_sicurezza.id as idInt from siscom_mi.interventi_sicurezza,siscom_mi.tipo_intervento where tipo_intervento.id=interventi_sicurezza.id_tipo_intervento and interventi_sicurezza.id_fascicolo=" & idFascicolo.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "SELECT NOME_SOGG_COINVOLTO," _
                         & " COGNOME_SOGG_COINVOLTO " _
                         & " FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA WHERE ANAGRAFICA_SOGG_COINVOLTI.id=ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV and id_intervento=" & par.IfNull(row.Item("idInt"), 0) & " order by COGNOME_SOGG_COINVOLTO asc"
                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt2 As New Data.DataTable
                    da2.Fill(dt2)
                    da2.Dispose()
                    If dt2.Rows.Count > 0 Then
                        TabellaElenco2 = "<tr>"
                        For Each row2 As Data.DataRow In dt2.Rows
                            TabellaElenco2 = TabellaElenco2 & "<td>" & row2.Item("COGNOME_SOGG_COINVOLTO") & " " & row2.Item("NOME_SOGG_COINVOLTO") & "</td>"
                        Next
                    End If


                    Selezionato = True
                    TabellaElenco = TabellaElenco & "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>" & row.Item("tipoInt") & "</td></tr>"
                Next
            End If

            If TabellaElenco2 <> "" Then
                TabellaElenco2 = TabellaElenco2 & "<tr>"
            End If

            par.cmd.CommandText = "select cod_unita_immobiliare,fascicolo_sicurezza.id_unita from siscom_mi.unita_immobiliari,siscom_mi.fascicolo_sicurezza where unita_immobiliari.id=siscom_mi.fascicolo_sicurezza.id_unita "
            Dim myReaderUI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderUI.Read Then
                codUI = par.IfNull(myReaderUI("Cod_unita_immobiliare"), "")
                IdUnita = par.IfNull(myReaderUI("id_unita"), "")
            End If
            myReaderUI.Close()

            If Selezionato = False Then
                'MessJQuery("Selezionare almeno un documento dalla lista!", 0, "Attenzione")
                'DaStampare = False
            Else
                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("Fascicolo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()
                contenuto = Replace(contenuto, "$soggCoinvolti$", TabellaElenco2)
                contenuto = Replace(contenuto, "$documentipresentati$", TabellaElenco)
                Dim BarcodeMetodo As String = "TELERIK"
                par.cmd.CommandText = "select valore from parameter where id=129"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
                End If
                myReaderS.Close()

                par.cmd.CommandText = "SELECT distinct (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0,(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1 " _
                    & " FROM siscom_mi.FASCICOLO_SICUREZZA,siscom_mi.interventi_sicurezza,siscom_mi.segnalazioni WHERE segnalazioni.id=interventi_sicurezza.id_segnalazione and FASCICOLO_SICUREZZA.id=interventi_sicurezza.id_fascicolo and FASCICOLO_SICUREZZA.ID=" & idFascicolo.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$nomeprocesso$", par.IfNull(myReader("TIPO0"), "") & "-" & par.IfNull(myReader("TIPO1"), ""))
                    IndiceProcesso = idFascicolo.Value
                Else
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore nella ricerca del processo di stampa barcode!", 300, 150, "Attenzione", Nothing, Nothing)
                    DaStampare = False
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_UNITA=" & IdUnita
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$indirizzounita$", par.IfNull(myReader("descrizione"), "") & ", " & par.IfNull(myReader("civico"), "") & "   " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("nome"), "") & " " & par.IfNull(myReader("sigla"), ""))
                Else
                    DaStampare = False
                End If
                myReader.Close()

                If DaStampare = True Then

                    par.cmd.CommandText = "SELECT MAX(PROGRESSIVO) FROM siscom_mi.FASCICOLO_BARCODE_SICUREZZA WHERE ID_FASCICOLO=" & IndiceProcesso & " "
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Progressivo = par.IfNull(myReader(0), 0) + 1

                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_FASC_BARCODE_SICUREZZA.NEXTVAL FROM DUAL"
                        Dim lettore22 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore22.Read Then
                            idFascBC = par.IfNull(lettore22(0), "-1")
                        End If
                        lettore22.Close()
                        BarCodeDaStampare = UCase(codUI) & IdUnita & Format(Progressivo, "00")
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.FASCICOLO_BARCODE_SICUREZZA ( ID, ID_FASCICOLO, DATA_ORA, CODICE_A_BARRE,PROGRESSIVO)" _
                        & "VALUES (" & idFascBC & ", " & idFascicolo.Value & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & BarCodeDaStampare & "'," & Progressivo & " )"
                        par.cmd.ExecuteNonQuery()
                        contenuto = Replace(contenuto, "$codice$", "*" & BarCodeDaStampare & "*")
                        If BarcodeMetodo = "TELERIK" Then
                            FileCodice = par.CreaBarCode128(BarCodeDaStampare, "ALLEGATI\SICUREZZA", False)
                        Else
                            FileCodice = RicavaBarCode39(BarCodeDaStampare, "ALLEGATI\SICUREZZA")
                        End If

                        contenuto = Replace(contenuto, "$barcode$", FileCodice)
                        contenuto = Replace(contenuto, "$datastampa$", Mid(Data_Ora_Stampa, 7, 2) & "/" & Mid(Data_Ora_Stampa, 5, 2) & "/" & Mid(Data_Ora_Stampa, 1, 4) & " - " & Mid(Data_Ora_Stampa, 9, 2) & ":" & Mid(Data_Ora_Stampa, 11, 2))

                        NomeFile = "SEC_" & codUI & "_" & idFascBC & "-" & Format(Now, "yyyyMMddHHmmss")

                        Dim pdfConverter As PdfConverter = New PdfConverter
                        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
                        If Licenza <> "" Then
                            pdfConverter.LicenseKey = Licenza
                        End If
                        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                        pdfConverter.PdfDocumentOptions.ShowHeader = False
                        pdfConverter.PdfDocumentOptions.ShowFooter = False
                        pdfConverter.PdfDocumentOptions.LeftMargin = 5
                        pdfConverter.PdfDocumentOptions.RightMargin = 5
                        pdfConverter.PdfDocumentOptions.TopMargin = 5
                        pdfConverter.PdfDocumentOptions.BottomMargin = 5
                        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                        pdfConverter.PdfDocumentOptions.ShowHeader = False
                        pdfConverter.PdfFooterOptions.FooterText = ("")
                        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                        pdfConverter.PdfFooterOptions.DrawFooterLine = False
                        pdfConverter.PdfFooterOptions.PageNumberText = ""
                        pdfConverter.PdfFooterOptions.ShowPageNumber = False


                        'scrivo il nuovo modulo compilato
                        Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\SICUREZZA\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
                        sr.WriteLine(contenuto)
                        sr.Close()
                        pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\ALLEGATI\SICUREZZA\") & NomeFile & ".htm", Server.MapPath("..\ALLEGATI\SICUREZZA\") & NomeFile & ".pdf")

                        If FileCodice <> "" Then
                            System.IO.File.Delete(Server.MapPath("..\ALLEGATI\SICUREZZA\") & FileCodice)
                        End If
                        System.IO.File.Delete(Server.MapPath("..\ALLEGATI\SICUREZZA\") & NomeFile & ".htm")

                    Else
                        CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore nella ricerca del processo di stampa barcode!", 300, 150, "Attenzione", Nothing, Nothing)
                        DaStampare = False
                    End If
                End If

            End If

            If DaStampare = True Then
                connData.chiudi(True)
            Else
                connData.chiudi(False)
            End If

            If DaStampare = True Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), Me.Page.ClientID, "location.replace('../ALLEGATI/SICUREZZA/" & NomeFile & ".pdf','','')", True)
            End If

         
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Procedi - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
