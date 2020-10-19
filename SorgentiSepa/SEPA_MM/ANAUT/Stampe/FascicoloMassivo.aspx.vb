Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class ANAUT_Stampe_FascicoloMassivo
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
                Cerca()
            End If
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey113", "$(function () {$(" & Chr(34) & "#txtDataInsDA" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });});", True)
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey114", "$(function () {$(" & Chr(34) & "#txtDataInsA" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });});", True)
            txtDataInsDA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataInsA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Load - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Function Cerca()
        'Dim ds As New Data.DataSet()
        'Dim dlist As CheckBoxList
        'Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim NewCon As Boolean = False
        'Try
        '    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '        NewCon = True
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)
        '    End If

        '    dlist = CheckDocumenti
        '    da = New Oracle.DataAccess.Client.OracleDataAdapter("select id,descrizione from UTENZA_DOC_NECESSARI where id_bando_au=(select max(id) from utenza_bandi where stato=1) order by descrizione asc", par.OracleConn)
        '    'da = New Oracle.DataAccess.Client.OracleDataAdapter("select id,descrizione from UTENZA_DOC_NECESSARI where id_bando_au=(select max(id) from utenza_bandi where stato=1) and id not in (select id_doc from UTENZA_DOC_MANCANTE where id_dichiarazione=" & idd.Value & ") order by descrizione asc", par.OracleConn)
        '    da.Fill(ds)

        '    dlist.Items.Clear()
        '    dlist.DataSource = ds
        '    dlist.DataTextField = "DESCRIZIONE"
        '    dlist.DataValueField = "ID"
        '    dlist.DataBind()

        '    da.Dispose()
        '    da = Nothing

        '    dlist.DataSource = Nothing
        '    dlist = Nothing

        '    ds.Clear()
        '    ds.Dispose()
        '    ds = Nothing

        '    'Dim I As Integer
        '    'par.cmd.CommandText = "select id,descrizione from UTENZA_DOC_NECESSARI where id_bando_au=(select id_bando from utenza_dichiarazioni where id=" & idd.Value & ") and id NOT in (select id_doc from UTENZA_DOC_MANCANTE where id_dichiarazione=" & idd.Value & ") AND ID IN (SELECT ID_DOC FROM UTENZA_DOC_PRESENTATA WHERE ID_DICHIARAZIONE=" & idd.Value & ") order by descrizione asc"
        '    'Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        '    'Do While myReaderS.Read
        '    '    For I = 0 To CheckDocumenti.Items.Count - 1
        '    '        If myReaderS("ID") = CheckDocumenti.Items(I).Value Then
        '    '            CheckDocumenti.Items(I).Selected = True
        '    '        End If
        '    '    Next
        '    'Loop
        '    'myReaderS.Close()

        '    If NewCon = True Then
        '        par.cmd.Dispose()
        '        par.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    End If
        'Catch ex As Exception
        '    If NewCon = True Then
        '        par.cmd.Dispose()
        '        par.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '    End If
        '    Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Cerca - " & ex.Message)
        '    Response.Redirect("../../Errore.aspx", False)
        'End Try
    End Function

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

    'Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
    '    Dim Selezionato As Boolean = False
    '    Dim TabellaElenco As String = ""
    '    Dim NomeFile As String = ""
    '    Dim DaStampare As Boolean = True
    '    Dim IndiceContratto As String = ""
    '    Dim CodiceProcesso As String = ""
    '    Dim IndiceProcesso As String = ""
    '    Dim Progressivo As Integer = 0
    '    Dim BarCodeDaStampare As String = ""
    '    Dim Data_Ora_Stampa As String = Format(Now, "yyyyMMddHHmmss")
    '    Dim FileCodice As String = ""
    '    Dim MessaggioAlert As String = ""
    '    Dim FileCreatiBarCode() As String
    '    Dim NomeFileMassivo = "FronteSpizioMassivo_" & Format(Now, "yyyyMMddHHmmss")
    '    Dim IndiceAU As Integer = 0
    '    Dim NomeProcesso As String = ""

    '    If txtDataPresenta.Text = "" Or txtDataInsDA.Text = "" Or txtDataInsA.Text = "" Then
    '        MessJQuery("Inserire Data Presentazione, Data Inizio e Data Fine intervallo!", 0, "Attenzione")
    '        Exit Sub
    '    End If

    '    Try
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '        par.myTrans = par.OracleConn.BeginTransaction()

    '        TabellaElenco = "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>RICEVUTA DICHIARAZIONE CALCOLO ISEE-ERP</td></tr>"

    '        For i = 0 To CheckDocumenti.Items.Count - 1
    '            If CheckDocumenti.Items(i).Selected = True Then
    '                Selezionato = True
    '                TabellaElenco = TabellaElenco & "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>" & CheckDocumenti.Items(i).Text & "</td></tr>"
    '            End If
    '        Next
    '        If Selezionato = False Then
    '            MessJQuery("Selezionare almeno un documento dalla lista!", 0, "Attenzione")
    '            DaStampare = False
    '        Else
    '            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\Modelli\FascicoloMassivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
    '            Dim contenuto As String = sr1.ReadToEnd()
    '            sr1.Close()

    '            Dim sr2 As StreamReader = New StreamReader(Server.MapPath("..\Modelli\FascicoloMassivoTesto.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
    '            Dim contenutoOriginaleTesto As String = sr2.ReadToEnd()
    '            sr2.Close()


    '            Dim BarcodeMetodo As String = "TELERIK"
    '            par.cmd.CommandText = "select valore from parameter where id=129"
    '            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReaderS.Read Then
    '                BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
    '            End If
    '            myReaderS.Close()


    '            par.cmd.CommandText = "select max(id) from utenza_bandi where stato=1"
    '            myReaderS = par.cmd.ExecuteReader()
    '            If myReaderS.Read Then
    '                IndiceAU = par.IfNull(myReaderS(0), 0)
    '            End If
    '            IndiceAU = 6
    '            myReaderS.Close()
    '            If IndiceAU = 0 Then
    '                MessJQuery("Nessuna AU aperta! Non è possibile procedere", 0, "Attenzione")
    '                DaStampare = False
    '            End If


    '            par.cmd.CommandText = "SELECT * FROM PROCESSI_BARCODE WHERE ID_BANDO_AU=" & IndiceAU
    '            myReaderS = par.cmd.ExecuteReader()
    '            If myReaderS.Read Then
    '                NomeProcesso = par.IfNull(myReaderS("descrizione"), "")
    '                CodiceProcesso = par.IfNull(myReaderS("valore"), "XXXXX")
    '                IndiceProcesso = par.IfNull(myReaderS("id"), "-1")
    '            Else
    '                MessJQuery("Errore nella ricerca del processo di stampa barcode!", 0, "Attenzione")
    '                DaStampare = False
    '            End If
    '            myReaderS.Close()

    '            If DaStampare = True Then
    '                Dim Contatore As Integer = 0
    '                Dim contenutoOriginale As String = ""
    '                Dim NomeFileTesto As String = "Contenuto_" & Format(Now, "yyyyMMddHHmmss") & ".txt"

    '                par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID AS IDD,RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA,UTENZA_DICHIARAZIONI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO = UTENZA_DICHIARAZIONI.RAPPORTO AND UTENZA_DICHIARAZIONI.ID_BANDO=" & IndiceAU & " and UTENZA_DICHIARAZIONI.ID_STATO=1 AND UTENZA_DICHIARAZIONI.DATA_PG>='" & par.AggiustaData(txtDataInsDA.Text) & "' AND UTENZA_DICHIARAZIONI.DATA_PG<='" & par.AggiustaData(txtDataInsA.Text) & "'"
    '                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                Do While myReader.Read
    '                    contenutoOriginale = contenutoOriginaleTesto

    '                    contenutoOriginale = Replace(contenutoOriginale, "$documentipresentati$", TabellaElenco)
    '                    contenutoOriginale = Replace(contenutoOriginale, "$nomeprocesso$", NomeProcesso)
    '                    If par.IfNull(myReader("ragione_sociale"), "") = "" Then
    '                        contenutoOriginale = Replace(contenutoOriginale, "$dichiarante$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
    '                    Else
    '                        contenutoOriginale = Replace(contenutoOriginale, "$dichiarante$", par.IfNull(myReader("ragione_sociale"), ""))
    '                    End If
    '                    IndiceContratto = par.IfNull(myReader("id"), "-1")
    '                    par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & IndiceContratto
    '                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                    If myReader1.Read Then
    '                        contenutoOriginale = Replace(contenutoOriginale, "$indirizzounita$", par.IfNull(myReader1("descrizione"), "") & ", " & par.IfNull(myReader1("civico"), "") & "   " & par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("nome"), "") & " " & par.IfNull(myReader1("sigla"), ""))
    '                    Else
    '                        MessaggioAlert = MessaggioAlert & "Per il contratto codice " & par.IfNull(myReader("cod_contratto"), "") & " non è presente l'indirizzo dell'unità abitativa e il frontespizio non è stato generato. Procedere manualmente." & vbCrLf
    '                    End If
    '                    myReader1.Close()
    '                    If MessaggioAlert = "" Then
    '                        par.cmd.CommandText = "SELECT MAX(PROGRESSIVO) FROM PROCESSI_BARCODE_STAMPE WHERE ID_PROCESSO=" & IndiceProcesso & " AND ID_CONTRATTO=" & IndiceContratto
    '                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                        If myReader2.Read Then
    '                            Progressivo = par.IfNull(myReader2(0), 0) + 1
    '                            BarCodeDaStampare = UCase(par.IfNull(myReader("cod_contratto"), "")) & CodiceProcesso & Format(Progressivo, "00")
    '                            par.cmd.CommandText = "INSERT INTO PROCESSI_BARCODE_STAMPE (ID,ID_PROCESSO,ID_CONTRATTO,PROGRESSIVO,DATA_ORA,CODICE) VALUES (SEQ_PROCESSI_BARCODE_STAMPE.NEXTVAL," & IndiceProcesso & "," & IndiceContratto & "," & Progressivo & ",'" & Data_Ora_Stampa & "','" & BarCodeDaStampare & "')"
    '                            par.cmd.ExecuteNonQuery()
    '                            contenutoOriginale = Replace(contenutoOriginale, "$codice$", "*" & BarCodeDaStampare & "*")
    '                            If BarcodeMetodo = "TELERIK" Then
    '                                FileCodice = par.CreaBarCode128(BarCodeDaStampare, "FileTemp", False)
    '                            Else
    '                                FileCodice = RicavaBarCode39(BarCodeDaStampare, "FileTemp")
    '                            End If
    '                            ReDim Preserve FileCreatiBarCode(Contatore)
    '                            FileCreatiBarCode(Contatore) = FileCodice

    '                            contenutoOriginale = Replace(contenutoOriginale, "$barcode$", FileCodice)
    '                            contenutoOriginale = Replace(contenutoOriginale, "$datastampa$", Mid(Data_Ora_Stampa, 7, 2) & "/" & Mid(Data_Ora_Stampa, 5, 2) & "/" & Mid(Data_Ora_Stampa, 1, 4) & " - " & Mid(Data_Ora_Stampa, 9, 2) & ":" & Mid(Data_Ora_Stampa, 11, 2))
    '                            contenutoOriginale = Replace(contenutoOriginale, "$codicecontratto$", UCase(par.IfNull(myReader("cod_contratto"), "")))

    '                            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\") & NomeFileTesto, True, System.Text.Encoding.Default)
    '                            sr.WriteLine(contenutoOriginale)
    '                            sr.Close()

    '                            par.cmd.CommandText = "DELETE FROM UTENZA_DOC_PRESENTATA WHERE ID_DICHIARAZIONE=" & myReader("IDD")
    '                            par.cmd.ExecuteNonQuery()
    '                            For i = 0 To CheckDocumenti.Items.Count - 1
    '                                If CheckDocumenti.Items(i).Selected = True Then
    '                                    par.cmd.CommandText = "INSERT INTO UTENZA_DOC_PRESENTATA (ID_DICHIARAZIONE,ID_DOC,DESCRIZIONE,DATA_PRESENTAZIONE) VALUES (" & myReader("IDD") & "," & CheckDocumenti.Items(i).Value & ",'" & par.PulisciStrSql(CheckDocumenti.Items(i).Text) & "','" & par.AggiustaData(txtDataPresenta.Text) & "')"
    '                                    par.cmd.ExecuteNonQuery()
    '                                End If
    '                            Next
    '                            Contatore = Contatore + 1
    '                        End If
    '                        myReader2.Close()
    '                    End If
    '                Loop
    '                myReader.Close()
    '                If Contatore = 0 Then
    '                    MessJQuery("Nessuna dichiarazione COMPLETA trovata nell'intervallo specificato!", 0, "Attenzione")
    '                    DaStampare = False
    '                Else
    '                    Dim pdfConverter As PdfConverter = New PdfConverter
    '                    Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
    '                    If Licenza <> "" Then
    '                        pdfConverter.LicenseKey = Licenza
    '                    End If
    '                    pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
    '                    pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
    '                    pdfConverter.PdfDocumentOptions.ShowHeader = False
    '                    pdfConverter.PdfDocumentOptions.ShowFooter = False
    '                    pdfConverter.PdfDocumentOptions.LeftMargin = 5
    '                    pdfConverter.PdfDocumentOptions.RightMargin = 5
    '                    pdfConverter.PdfDocumentOptions.TopMargin = 5
    '                    pdfConverter.PdfDocumentOptions.BottomMargin = 5
    '                    pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

    '                    pdfConverter.PdfDocumentOptions.ShowHeader = False
    '                    pdfConverter.PdfFooterOptions.FooterText = ("")
    '                    pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
    '                    pdfConverter.PdfFooterOptions.DrawFooterLine = False
    '                    pdfConverter.PdfFooterOptions.PageNumberText = ""
    '                    pdfConverter.PdfFooterOptions.ShowPageNumber = False

    '                    Dim sr3 As StreamReader = New StreamReader(Server.MapPath("..\..\FileTemp\") & NomeFileTesto, System.Text.Encoding.GetEncoding("iso-8859-1"))
    '                    Dim contenutoScritto As String = sr3.ReadToEnd()
    '                    sr3.Close()
    '                    contenuto = Replace(contenuto, "$testomodello$", contenutoScritto)


    '                    'scrivo il nuovo modulo compilato

    '                    Dim sr4 As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\") & NomeFileMassivo & ".htm", False, System.Text.Encoding.Default)
    '                    sr4.WriteLine(contenuto)
    '                    sr4.Close()

    '                    pdfConverter.SavePdfFromHtmlFileToFile(Server.MapPath("..\..\FileTemp\") & NomeFileMassivo & ".htm", Server.MapPath("..\..\FileTemp\") & NomeFileMassivo & ".pdf")
    '                    Dim i As Integer = 0
    '                    For i = 0 To Contatore - 1
    '                        System.IO.File.Delete(Server.MapPath("..\..\FileTemp\") & FileCreatiBarCode(i))
    '                    Next
    '                    System.IO.File.Delete(Server.MapPath("..\..\FileTemp\") & NomeFileTesto)
    '                    System.IO.File.Delete(Server.MapPath("..\..\FileTemp\") & NomeFileMassivo & ".htm")
    '                    System.IO.File.Move(Server.MapPath("..\..\FileTemp\") & NomeFileMassivo & ".pdf", Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFileMassivo & ".pdf")
    '                End If
    '            End If
    '        End If
    '        If DaStampare = True Then
    '            par.cmd.CommandText = "INSERT INTO UTENZA_DICHIARAZIONI_F_SPIZIO VALUES (" & IndiceAU & ",'" & NomeFileMassivo & ".pdf','" & par.AggiustaData(txtDataInsDA.Text) & "','" & par.AggiustaData(txtDataInsA.Text) & "','" & par.PulisciStrSql(MessaggioAlert) & "','" & NomeProcesso & "')"
    '            par.cmd.ExecuteNonQuery()
    '            par.myTrans.Commit()
    '        Else
    '            par.myTrans.Rollback()
    '        End If
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        If DaStampare = True Then
    '            'Response.Redirect("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & NomeFileMassivo & ".pdf", False)
    '            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.open('../../ALLEGATI/ANAGRAFE_UTENZA/" & NomeFileMassivo & ".pdf','FronteSpizio','');", True)
    '        End If
    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:Stampa Frontespizio-Procedi - " & ex.Message)
    '        Response.Redirect("../../Errore.aspx", True)
    '    End Try
    'End Sub

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
        Dim MessaggioAlert As String = ""
        Dim FileCreatiBarCode() As String
        Dim NomeFileFS() As String
        Dim NomeFileMassivo = "FronteSpizioMassivo_" & Format(Now, "yyyyMMddHHmmss")
        Dim IndiceAU As Integer = 0
        Dim NomeProcesso As String = ""
        Dim Contenuto As String = ""
        Dim i As Integer = 0
        Dim Contatore As Integer = 0

        If txtDataInsDA.Text = "" Or txtDataInsA.Text = "" Then
            MessJQuery("Inserire Data Presentazione, Data Inizio e Data Fine intervallo!", 0, "Attenzione")
            Exit Sub
        End If

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()

            If DaStampare = True Then
                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\Modelli\FascicoloM.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenutoOriginale As String = sr1.ReadToEnd()
                sr1.Close()

                Dim BarcodeMetodo As String = "TELERIK"
                par.cmd.CommandText = "select valore from parameter where id=129"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    BarcodeMetodo = par.IfNull(myReaderS(0), "TELERIK")
                End If
                myReaderS.Close()

                par.cmd.CommandText = "select max(id) from utenza_bandi where stato=1"
                myReaderS = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    IndiceAU = par.IfNull(myReaderS(0), 0)
                End If

                myReaderS.Close()
                If IndiceAU = 0 Then
                    MessJQuery("Nessuna AU aperta! Non è possibile procedere", 0, "Attenzione")
                    DaStampare = False
                End If

                par.cmd.CommandText = "SELECT * FROM PROCESSI_BARCODE WHERE ID_BANDO_AU=" & IndiceAU
                myReaderS = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    NomeProcesso = par.IfNull(myReaderS("descrizione"), "")
                    CodiceProcesso = par.IfNull(myReaderS("valore"), "XXXXX")
                    IndiceProcesso = par.IfNull(myReaderS("id"), "-1")
                Else
                    MessJQuery("Errore nella ricerca del processo di stampa barcode!", 0, "Attenzione")
                    DaStampare = False
                End If
                myReaderS.Close()
                If DaStampare = True Then
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


                    par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID AS IDD,UTENZA_DICHIARAZIONI.DATA_PG,UTENZA_DICHIARAZIONI.pg,RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA,UTENZA_DICHIARAZIONI WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO = UTENZA_DICHIARAZIONI.RAPPORTO AND UTENZA_DICHIARAZIONI.ID_BANDO=" & IndiceAU & " and UTENZA_DICHIARAZIONI.ID_STATO=1 AND UTENZA_DICHIARAZIONI.DATA_PG>='" & par.AggiustaData(txtDataInsDA.Text) & "' AND UTENZA_DICHIARAZIONI.DATA_PG<='" & par.AggiustaData(txtDataInsA.Text) & "' ORDER BY ANAGRAFICA.COGNOME,ANAGRAFICA.NOME"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        Contenuto = contenutoOriginale
                        NomeFile = "05_" & par.IfNull(myReader("cod_contratto"), "") & "_" & par.IfNull(myReader("idd"), "") & "-" & Format(Now, "yyyyMMddHHmmss")

                        Progressivo = 0
                        MessaggioAlert = ""
                        par.cmd.CommandText = "select max(progressivo) from PROCESSI_BARCODE_STAMPE where id_processo=" & IndiceProcesso & " and id_contratto=" & myReader("id")
                        Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderX.Read Then
                            Progressivo = par.IfNull(myReaderX(0), 0)
                        End If
                        myReaderX.Close()

                        If Progressivo <> 0 Then
                            par.cmd.CommandText = "select * from PROCESSI_BARCODE_STAMPE where id_processo=" & IndiceProcesso & " and id_contratto=" & myReader("id") & " and progressivo=" & Progressivo
                            myReaderX = par.cmd.ExecuteReader()
                            If myReaderX.Read Then
                                Data_Ora_Stampa = par.IfNull(myReaderX("DATA_ORA"), 0)
                                BarCodeDaStampare = par.IfNull(myReaderX("codice"), "")
                            End If
                            myReaderX.Close()

                            Contenuto = Replace(Contenuto, "$codice$", "*" & BarCodeDaStampare & "*")
                            If BarcodeMetodo = "TELERIK" Then
                                FileCodice = par.CreaBarCode128(BarCodeDaStampare, "ALLEGATI\ANAGRAFE_UTENZA", False)
                            Else
                                FileCodice = RicavaBarCode39(BarCodeDaStampare, "ALLEGATI\ANAGRAFE_UTENZA")
                            End If
                            ReDim Preserve FileCreatiBarCode(Contatore)
                            FileCreatiBarCode(Contatore) = FileCodice
                            Contenuto = Replace(Contenuto, "$barcode$", FileCodice)
                            Contenuto = Replace(Contenuto, "$datastampa$", Mid(Data_Ora_Stampa, 7, 2) & "/" & Mid(Data_Ora_Stampa, 5, 2) & "/" & Mid(Data_Ora_Stampa, 1, 4) & " - " & Mid(Data_Ora_Stampa, 9, 2) & ":" & Mid(Data_Ora_Stampa, 11, 2))
                            Contenuto = Replace(Contenuto, "$codicecontratto$", UCase(par.IfNull(myReader("cod_contratto"), "")))
                            Contenuto = Replace(Contenuto, "$pgprocesso$", UCase(par.IfNull(myReader("pg"), "")))

                            TabellaElenco = "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>RICEVUTA DICHIARAZIONE CALCOLO ISEE-ERP</td></tr>"
                            par.cmd.CommandText = "select DESCRIZIONE FROM UTENZA_DOC_PRESENTATA WHERE ID_DICHIARAZIONE=" & par.IfNull(myReader("IDD"), -1)
                            myReaderX = par.cmd.ExecuteReader()
                            Do While myReaderX.Read
                                Selezionato = True
                                TabellaElenco = TabellaElenco & "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'>X</td><td width='95%'>" & par.IfNull(myReaderX("DESCRIZIONE"), "") & "</td></tr>"
                            Loop
                            myReaderX.Close()

                            Contenuto = Replace(Contenuto, "$documentipresentati$", TabellaElenco)
                            Contenuto = Replace(Contenuto, "$nomeprocesso$", Mid(NomeProcesso, 1, 20))
                            If par.IfNull(myReader("ragione_sociale"), "") = "" Then
                                Contenuto = Replace(Contenuto, "$dichiarante$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                            Else
                                Contenuto = Replace(Contenuto, "$dichiarante$", par.IfNull(myReader("ragione_sociale"), ""))
                            End If
                            IndiceContratto = par.IfNull(myReader("id"), "-1")
                            par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & IndiceContratto
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                Contenuto = Replace(Contenuto, "$indirizzounita$", par.IfNull(myReader1("descrizione"), "") & ", " & par.IfNull(myReader1("civico"), "") & "   " & par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("nome"), "") & " " & par.IfNull(myReader1("sigla"), ""))
                            Else
                                MessaggioAlert = MessaggioAlert & "Per il contratto codice " & par.IfNull(myReader("cod_contratto"), "") & " non è presente l'indirizzo dell'unità abitativa e il frontespizio non è stato generato. Procedere manualmente." & vbCrLf
                            End If
                            myReader1.Close()
                        Else
                            Data_Ora_Stampa = par.FormattaData(par.IfNull(myReader("DATA_PG"), Format(Now, "yyyyMMdd")))
                            BarCodeDaStampare = "0000000000"

                            par.cmd.CommandText = "SELECT * FROM PROCESSI_BARCODE WHERE ID_BANDO_AU=(select id_bando from utenza_dichiarazioni where id=" & par.IfNull(myReader("IDD"), -1) & ")"
                            Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderX1.Read Then
                                CodiceProcesso = par.IfNull(myReaderX1("valore"), "XXXXX")
                            End If
                            myReaderX1.Close()
                            BarCodeDaStampare = UCase(par.IfNull(myReader("cod_contratto"), "")) & CodiceProcesso & "00"

                            Contenuto = Replace(Contenuto, "$codice$", "*" & BarCodeDaStampare & "*")
                            If BarcodeMetodo = "TELERIK" Then
                                FileCodice = par.CreaBarCode128(BarCodeDaStampare, "FileTemp", False)
                            Else
                                FileCodice = RicavaBarCode39(BarCodeDaStampare, "FileTemp")
                            End If
                            ReDim Preserve FileCreatiBarCode(Contatore)
                            FileCreatiBarCode(Contatore) = FileCodice
                            Contenuto = Replace(Contenuto, "$barcode$", FileCodice)
                            Contenuto = Replace(Contenuto, "$datastampa$", Data_Ora_Stampa)
                            Contenuto = Replace(Contenuto, "$codicecontratto$", UCase(par.IfNull(myReader("cod_contratto"), "")))


                            TabellaElenco = "<tr><td style='border: 1px solid #000000; text-align: center' width='5%'></td><td width='95%'></td></tr>"

                            Contenuto = Replace(Contenuto, "$documentipresentati$", TabellaElenco)
                            Contenuto = Replace(Contenuto, "$nomeprocesso$", Mid(NomeProcesso, 1, 20))
                            If par.IfNull(myReader("ragione_sociale"), "") = "" Then
                                Contenuto = Replace(Contenuto, "$dichiarante$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                            Else
                                Contenuto = Replace(Contenuto, "$dichiarante$", par.IfNull(myReader("ragione_sociale"), ""))
                            End If
                            IndiceContratto = par.IfNull(myReader("id"), "-1")
                            par.cmd.CommandText = "SELECT INDIRIZZI.*,comuni_nazioni.nome,comuni_nazioni.sigla FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,comuni_nazioni,siscom_mi.INDIRIZZI WHERE comuni_nazioni.cod=INDIRIZZI.cod_comune AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & IndiceContratto
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                Contenuto = Replace(Contenuto, "$indirizzounita$", par.IfNull(myReader1("descrizione"), "") & ", " & par.IfNull(myReader1("civico"), "") & "   " & par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("nome"), "") & " " & par.IfNull(myReader1("sigla"), ""))
                            Else
                                MessaggioAlert = MessaggioAlert & "Per il contratto codice " & par.IfNull(myReader("cod_contratto"), "") & " non è presente l'indirizzo dell'unità abitativa e il frontespizio non è stato generato. Procedere manualmente." & vbCrLf
                            End If
                            myReader1.Close()
                        End If
                        
                        If MessaggioAlert = "" Then
                            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\FileTemp\") & NomeFile & ".htm", True, System.Text.Encoding.Default)
                            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".htm", True, System.Text.Encoding.Default)
                            sr.WriteLine(Contenuto)
                            sr.Close()
                            pdfConverter.SavePdfFromHtmlFileToFile(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".htm", Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".pdf")
                            ReDim Preserve NomeFileFS(Contatore)
                            NomeFileFS(Contatore) = Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".pdf"
                            System.IO.File.Delete(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile & ".htm")
                            
                            Contatore = Contatore + 1
                            
                        End If
                    Loop
                    myReader.Close()

                    For i = 0 To Contatore - 1
                        System.IO.File.Delete(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & FileCreatiBarCode(i))
                    Next
                    
                    Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
                    pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                    pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                    Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

                    Dim Licenza1 As String = ""
                    Licenza1 = Session.Item("LicenzaPdfMerge")
                    If Licenza1 <> "" Then
                        pdfMerge.LicenseKey = Licenza1
                    End If

                    For i = 0 To Contatore - 1
                        pdfMerge.AppendPDFFile(NomeFileFS(i))
                        'System.IO.File.Move(NomeFileFS(i), Replace(NomeFileFS(i), "FileTemp", "ALLEGATI\ANAGRAFE_UTENZA"))
                    Next
                    pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & Replace(NomeFileMassivo & ".pdf", " ", "_"))
                    For i = 0 To Contatore - 1
                        System.IO.File.Delete(NomeFileFS(i))
                    Next
                End If
            End If

            If DaStampare = True Then
                If CONTATORE > 0 Then
                    par.cmd.CommandText = "INSERT INTO UTENZA_DICHIARAZIONI_F_SPIZIO VALUES (" & IndiceAU & ",'" & NomeFileMassivo & ".pdf','" & par.AggiustaData(txtDataInsDA.Text) & "','" & par.AggiustaData(txtDataInsA.Text) & "','" & par.PulisciStrSql(MessaggioAlert) & "','" & NomeProcesso & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                par.myTrans.Commit()
            Else
                par.myTrans.Rollback()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If DaStampare = True And CONTATORE > 0 Then
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.open('../../ALLEGATI/ANAGRAFE_UTENZA/" & NomeFileMassivo & ".pdf','FronteSpizio','');", True)
            Else
                MessJQuery("Nessuna Scheda da stampare", 0, "Attenzione")
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
End Class
