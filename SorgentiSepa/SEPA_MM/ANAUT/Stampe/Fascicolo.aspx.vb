Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip


Partial Class ANAUT_Stampe_Fascicolo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Try
            If Not IsPostBack Then
                idd.Value = Request.QueryString("ID")
                cod.Value = Request.QueryString("COD")
                Cerca()
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey112", "$(function () {$(" & Chr(34) & "#txtDataPresenta" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });});", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Load - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Function Cerca()
        Dim ds As New Data.DataSet()
        Dim dlist As CheckBoxList
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        Dim NewCon As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                NewCon = True
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            dlist = CheckDocumenti
            'da = New Oracle.DataAccess.Client.OracleDataAdapter("select id,descrizione from UTENZA_DOC_NECESSARI where id_bando_au=(select id_bando from utenza_dichiarazioni where id=" & idd.Value & ") and id not in (select id_doc from UTENZA_DOC_MANCANTE where id_dichiarazione=" & idd.Value & ") AND ID NOT IN (SELECT ID_DOC FROM UTENZA_DOC_PRESENTATA WHERE ID_DICHIARAZIONE=" & idd.Value & ") order by descrizione asc", par.OracleConn)
            da = New Oracle.DataAccess.Client.OracleDataAdapter("select id,descrizione from UTENZA_DOC_NECESSARI where id_bando_au=(select id_bando from utenza_dichiarazioni where id=" & idd.Value & ") and id not in (select id_doc from UTENZA_DOC_MANCANTE where id_dichiarazione=" & idd.Value & ") order by descrizione asc", par.OracleConn)
            da.Fill(ds)

            dlist.Items.Clear()
            dlist.DataSource = ds
            dlist.DataTextField = "DESCRIZIONE"
            dlist.DataValueField = "ID"
            dlist.DataBind()

            da.Dispose()
            da = Nothing

            dlist.DataSource = Nothing
            dlist = Nothing

            ds.Clear()
            ds.Dispose()
            ds = Nothing

            Dim I As Integer
            par.cmd.CommandText = "select id,descrizione from UTENZA_DOC_NECESSARI where id_bando_au=(select id_bando from utenza_dichiarazioni where id=" & idd.Value & ") and id NOT in (select id_doc from UTENZA_DOC_MANCANTE where id_dichiarazione=" & idd.Value & ") AND ID IN (SELECT ID_DOC FROM UTENZA_DOC_PRESENTATA WHERE ID_DICHIARAZIONE=" & idd.Value & ") order by descrizione asc"
            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderS.Read
                For I = 0 To CheckDocumenti.Items.Count - 1
                    If myReaderS("ID") = CheckDocumenti.Items(I).Value Then
                        CheckDocumenti.Items(I).Selected = True
                    End If
                Next
            Loop
            myReaderS.Close()
            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & idd.Value
            myReaderS = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                pg.Value = par.IfNull(myReaderS("pg"), "")
            End If
            myReaderS.Close()




            If NewCon = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If NewCon = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Cerca - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Function

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim Selezionato As Boolean = False
        Dim TabellaElenco As String = ""
        Dim NomeFile As String = ""
        Dim DaStampare As Boolean = True
        Dim IndiceContratto As String = ""
        Dim CodiceProcesso As String = ""
        Dim IndiceProcesso As String = ""
        Dim Progressivo As Integer = 0
        Dim BarCodeDaStampare As String = ""
        Dim Data_Ora_Stampa As String = Format(Now, "yyyyMMddHHmmss")
        Dim FileCodice As String = ""

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            TabellaElenco = "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>RICEVUTA DICHIARAZIONE CALCOLO ISEE-ERP</td></tr>"

            For i = 0 To CheckDocumenti.Items.Count - 1
                If CheckDocumenti.Items(i).Selected = True Then
                    Selezionato = True
                    TabellaElenco = TabellaElenco & "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>" & CheckDocumenti.Items(i).Text & "</td></tr>"
                End If
            Next
            If Selezionato = False Then
                MessJQuery("Selezionare almeno un documento dalla lista!", 0, "Attenzione")
                DaStampare = False
            Else
                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\Modelli\Fascicolo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()

                contenuto = Replace(contenuto, "$documentipresentati$", TabellaElenco)
                Dim BarcodeMetodo As String = "TELERIK"
                par.cmd.CommandText = "select valore from parameter where id=129"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
                End If
                myReaderS.Close()

                par.cmd.CommandText = "select RAPPORTI_UTENZA.ID,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO='" & cod.Value & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If par.IfNull(myReader("ragione_sociale"), "") = "" Then
                        contenuto = Replace(contenuto, "$dichiarante$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                    Else
                        contenuto = Replace(contenuto, "$dichiarante$", par.IfNull(myReader("ragione_sociale"), ""))
                    End If
                    IndiceContratto = par.IfNull(myReader("id"), "-1")
                Else
                    MessJQuery("Errore nella ricerca dell'intestatario del contratto!", 0, "Attenzione")
                    DaStampare = False
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & IndiceContratto
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$indirizzounita$", par.IfNull(myReader("descrizione"), "") & ", " & par.IfNull(myReader("civico"), "") & "   " & par.IfNull(myReader("cap"), "") & " " & par.IfNull(myReader("nome"), "") & " " & par.IfNull(myReader("sigla"), ""))
                Else
                    DaStampare = False
                End If
                myReader.Close()

                par.cmd.CommandText = "SELECT * FROM PROCESSI_BARCODE WHERE ID_BANDO_AU=(select id_bando from utenza_dichiarazioni where id=" & idd.Value & ")"
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    contenuto = Replace(contenuto, "$nomeprocesso$", Mid(par.IfNull(myReader("descrizione"), ""), 1, 20))
                    CodiceProcesso = par.IfNull(myReader("valore"), "XXXXX")
                    IndiceProcesso = par.IfNull(myReader("id"), "-1")
                Else
                    MessJQuery("Errore nella ricerca del processo di stampa barcode!", 0, "Attenzione")
                    DaStampare = False
                End If
                myReader.Close()

                contenuto = Replace(contenuto, "$pgprocesso$", pg.Value)

                If DaStampare = True Then
                    par.cmd.CommandText = "SELECT MAX(PROGRESSIVO) FROM PROCESSI_BARCODE_STAMPE WHERE ID_PROCESSO=" & IndiceProcesso & " AND ID_CONTRATTO=" & IndiceContratto
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Progressivo = par.IfNull(myReader(0), 0) + 1
                        BarCodeDaStampare = UCase(cod.Value) & CodiceProcesso & Format(Progressivo, "00")
                        par.cmd.CommandText = "INSERT INTO PROCESSI_BARCODE_STAMPE (ID,ID_PROCESSO,ID_CONTRATTO,PROGRESSIVO,DATA_ORA,CODICE) VALUES (SEQ_PROCESSI_BARCODE_STAMPE.NEXTVAL," & IndiceProcesso & "," & IndiceContratto & "," & Progressivo & ",'" & Data_Ora_Stampa & "','" & BarCodeDaStampare & "')"
                        par.cmd.ExecuteNonQuery()
                        contenuto = Replace(contenuto, "$codice$", "*" & BarCodeDaStampare & "*")
                        If BarcodeMetodo = "TELERIK" Then
                            FileCodice = par.CreaBarCode128(BarCodeDaStampare, "ALLEGATI\ANAGRAFE_UTENZA", False)
                        Else
                            FileCodice = RicavaBarCode39(BarCodeDaStampare, "ALLEGATI\ANAGRAFE_UTENZA")
                        End If

                        contenuto = Replace(contenuto, "$barcode$", FileCodice)
                        contenuto = Replace(contenuto, "$datastampa$", Mid(Data_Ora_Stampa, 7, 2) & "/" & Mid(Data_Ora_Stampa, 5, 2) & "/" & Mid(Data_Ora_Stampa, 1, 4) & " - " & Mid(Data_Ora_Stampa, 9, 2) & ":" & Mid(Data_Ora_Stampa, 11, 2))
                        contenuto = Replace(contenuto, "$codicecontratto$", UCase(cod.Value))

                        NomeFile = "05_" & cod.Value & "_" & idd.Value & "-" & Format(Now, "yyyyMMddHHmmss")

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
                        Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
                        sr.WriteLine(contenuto)
                        sr.Close()
                        pdfConverter.SavePdfFromHtmlFileToFile(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".htm", Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".pdf")

                        If FileCodice <> "" Then
                            System.IO.File.Delete(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & FileCodice)
                        End If
                        System.IO.File.Delete(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".htm")

                        par.cmd.CommandText = "DELETE FROM UTENZA_DOC_PRESENTATA WHERE ID_DICHIARAZIONE=" & idd.Value
                        par.cmd.ExecuteNonQuery()

                        For i = 0 To CheckDocumenti.Items.Count - 1
                            If CheckDocumenti.Items(i).Selected = True Then
                                If Len(txtDataPresenta.Text) = 10 Then
                                    par.cmd.CommandText = "INSERT INTO UTENZA_DOC_PRESENTATA (ID_DICHIARAZIONE,ID_DOC,DESCRIZIONE,DATA_PRESENTAZIONE) VALUES (" & idd.Value & "," & CheckDocumenti.Items(i).Value & ",'" & par.PulisciStrSql(CheckDocumenti.Items(i).Text) & "','" & par.AggiustaData(txtDataPresenta.Text) & "')"
                                Else
                                    par.cmd.CommandText = "INSERT INTO UTENZA_DOC_PRESENTATA (ID_DICHIARAZIONE,ID_DOC,DESCRIZIONE,DATA_PRESENTAZIONE) VALUES (" & idd.Value & "," & CheckDocumenti.Items(i).Value & ",'" & par.PulisciStrSql(CheckDocumenti.Items(i).Text) & "','" & txtDataPresenta.Text & "')"
                                End If
                                par.cmd.ExecuteNonQuery()
                            End If
                        Next


                    Else
                        MessJQuery("Errore nella ricerca del progressivo barcode!", 0, "Attenzione")
                        DaStampare = False
                    End If
                End If
            End If

            If DaStampare = True Then
                par.myTrans.Commit()
            Else
                par.myTrans.Rollback()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If DaStampare = True Then
                Dim tempo As Integer = 0
                If System.IO.File.Exists(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".pdf") = False Then
                    Do While System.IO.File.Exists(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".pdf") = False
                        System.Threading.Thread.Sleep(1000)
                        tempo = tempo + 1
                        If tempo = 11 Then
                            Exit Do
                        End If
                    Loop
                End If
                ' Response.Redirect("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & NomeFile & ".pdf", False)
                ZippaFiles(NomeFile)
                Response.Redirect("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & NomeFile & ".zip", False)
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Procedi - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Private Sub ZippaFiles(ByVal nomefile As String)
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & nomefile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & nomefile & ".pdf")
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

        File.Delete(strFile)

    End Sub

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-MessJQuery - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnSelezionaTutto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutto.Click
        Dim I As Integer
        For I = 0 To CheckDocumenti.Items.Count - 1
            CheckDocumenti.Items(I).Selected = True
        Next
    End Sub

    Protected Sub btnDeSelezionaTutto_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDeSelezionaTutto.Click
        Dim I As Integer
        For I = 0 To CheckDocumenti.Items.Count - 1
            CheckDocumenti.Items(I).Selected = False
        Next
    End Sub

    ''' <summary>Procedura per la creazione di un Barcode</summary>
    ''' <param name="Codice">Definizione del Testo di Codifica del BarCode</param>
    ''' <param name="DoveSalvare">Definizione del Percorso del Barcode</param>
    ''' <param name="BarHeight">Definizione dell'Altezza del Barcode</param>
    ''' <param name="ImageWidth">Definizione dell'Altezza dell'Immagine del Barcode</param>
    ''' <param name="ImageHeight">Definizione della Larghezza dell'Immagine del Barcode</param>
    Public Function RicavaBarCode128(ByVal Codice As String, ByVal DoveSalvare As String, Optional ByVal BarHeight As Integer = 50, Optional ByVal ImageWidth As Integer = 1200, Optional ByVal ImageHeight As Integer = 50) As String
        Try
            Dim NomeFile As String = "CodeBar_" & Codice & "_" & Format(Now, "yyyyMMddHHmmss") & ".jpg"
            Dim codeBarImage As New System.Drawing.Bitmap(ImageWidth, ImageHeight)
            Dim barcode As New iTextSharp.text.pdf.Barcode128
            barcode.BarHeight = BarHeight
            barcode.ChecksumText = True
            barcode.GenerateChecksum = True
            barcode.Code = Codice
            codeBarImage = barcode.CreateDrawingImage(Color.Black, Color.White)
            codeBarImage.Save(System.Web.HttpContext.Current.Server.MapPath("~\" & DoveSalvare & "\") & NomeFile, System.Drawing.Imaging.ImageFormat.Jpeg)
            RicavaBarCode128 = NomeFile
        Catch ex As Exception
            RicavaBarCode128 = ""
        End Try
    End Function

    ''' <summary>Procedura per la creazione di un Barcode</summary>
    ''' <param name="Codice">Definizione del Testo di Codifica del BarCode</param>
    ''' <param name="DoveSalvare">Definizione del Percorso del Barcode</param>
    ''' <param name="BarHeight">Definizione dell'Altezza del Barcode</param>
    ''' <param name="ImageWidth">Definizione dell'Altezza dell'Immagine del Barcode</param>
    ''' <param name="ImageHeight">Definizione della Larghezza dell'Immagine del Barcode</param>
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
End Class
