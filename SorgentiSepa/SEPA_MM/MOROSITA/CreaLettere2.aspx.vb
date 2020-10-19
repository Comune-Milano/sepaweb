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

    Private Function RicavaFile(ByVal sFile As String) As String
        Dim N As Integer

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Public Property DisabilitaExpect100Continue() As String
        Get
            If Not (ViewState("par_DisabilitaExpect100Continue") Is Nothing) Then
                Return CStr(ViewState("par_DisabilitaExpect100Continue"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DisabilitaExpect100Continue") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
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

        Dim ValImporto1 As Decimal = 0
        Dim ValImporto2 As Decimal = 0


        Dim sSCALA As String = ""
        Dim sINTERNO As String = ""

        Dim sNomeFiliale As String = ""
        Dim sNumTel_Filiale As String = ""
        Dim sIndirizzo_Filiale As String = ""

        Dim IdAnagrafica As String = "-1"
        Dim Str As String = ""
        Dim Operatore As String = ""
        Dim VOCE As String = ""
        Dim ScadenzaBollettino As String = ""
        Dim Riassunto As String = ""
        Dim num_bollettino As String = ""
        Dim contenutoRiassunto As String = ""
        Dim idMorositaLettere As Long = 0

        Dim MiaSHTML As String
        Dim MIOCOLORE As String
        Dim i As Integer
        Dim ElencoFile() As String

        Dim j As Integer
        Dim periodo As String = ""
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

        Dim TipoStampa As String = ""
        Dim NoteBollette As String = ""

        Dim sPosteAler As String = ""               'TUTTI i CAMPI
        Dim sPosteAlerNominativo As String = ""     '1)  Nominativo Postale (50)
        Dim sPosteAlerInd As String = ""            '3)  Indirizzo          (50)
        Dim sPosteAlerScala As String = ""          '6)  Scala              (2)
        Dim sPosteAlerInterno As String = ""        '7)  Interno            (3)
        Dim sPosteAlerCAP As String = ""            '8)  CAP                (5)
        Dim sPosteAlerLocalita As String = ""       '9)  Località           (50)
        Dim sPosteAlerProv As String = ""           '10) Provincia          (2)
        Dim sPosteAlerCodUtente As String = ""      '11) Codice Utente      (12)
        Dim sPosteAlerAcronimo As String = ""       '12) Acronimo           (4)
        Dim sPosteAlerIA As String = ""             '13) IA                 (16)
        Dim sPosteDefault As String = ""            ' per i campi 2-4-5 (Presso, casella postale, indirizzo casella postale)


        Dim url As String

        Dim kk As Long
        Dim jj As Long
        Dim scambia As String

        Dim PeriodoXLS_INIZIO As Long = 0
        Dim PeriodoXLS_FINE As Long = 0

        Dim FlagConnessione As Boolean

        Dim TrovatoAB As Boolean = False    '"Ingiunzione_Aa.htm" e/o "Ingiunzione_Bb.htm"
        Dim TrovatoCD As Boolean = False    '"Ingiunzione_C.htm"  e/o "Ingiunzione_D.htm"
        Dim TrovatoEF As Boolean = False    '"Ingiunzione_E.htm"  e/o "Ingiunzione_F.htm"

        Dim sNomeFileMorLettera As String = ""
        Dim ValSommaImportoCanone As Decimal = 0
        Dim ValSommaImportoOneri As Decimal = 0



        Dim idStruttura As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                IndiceMorosita = Request.QueryString("IDMOR")

                'par.OracleConn.Open()
                'par.SettaCommand(par)
                'par.myTrans = par.OracleConn.BeginTransaction()
                '‘‘par.cmd.Transaction = par.myTrans

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If



                'PARAMETRI BOLLETTA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.PARAMETRI_BOLLETTA "
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReaderA.Read
                    Select Case par.IfNull(myReaderA("ID"), 0)
                        Case "25"
                            APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                        Case "26"
                            SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                        Case "0"
                            BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                        Case "36" '"ex 34"
                            spese_notifica = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
                        Case "32"
                            causalepagamento.Value = par.IfNull(myReaderA("VALORE"), "")
                    End Select
                End While
                myReaderA.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='EXPECT100CONTINUE'"
                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    DisabilitaExpect100Continue = par.IfNull(myReaderA("valore"), "0")
                End If
                myReaderA.Close()
                '*********************************************
                par.cmd.CommandText = "Select id_ufficio from operatori where id = " & Session.Item("ID_OPERATORE")

                myReaderA = par.cmd.ExecuteReader
                If myReaderA.Read Then
                    idStruttura = par.IfNull(myReaderA("id_ufficio"), "0")
                End If
                myReaderA.Close()

                'SE LA MOROSITA' è STATA già ESEGUITA e non CI SONO MOROSITA' INCOMPLETE, 
                '   allora visualizzo i file zip 

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select MOROSITA_LETTERE.* " _
                                   & " from SISCOM_MI.MOROSITA_LETTERE " _
                                   & " where BOLLETTINO IS NULL and ID_MOROSITA = " & IndiceMorosita

                myReaderA = par.cmd.ExecuteReader()
                If myReaderA.HasRows = False Then
                    myReaderA.Close()


                    MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='450px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                    i = 0
                    MIOCOLORE = "#CCFFFF"

                    'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("Varie\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & IndiceMorosita & "-*.zip")
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & IndiceMorosita & "-*.zip")
                        ReDim Preserve ElencoFile(i)
                        ElencoFile(i) = foundFile
                        i = i + 1
                    Next


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
                            'MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='Varie/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/MOROSITA_CONTRATTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
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

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                    Exit Sub
                End If
                myReaderA.Close()
                '**********************************************************

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select distinct(TIPO_LETTERA) " _
                                  & " from SISCOM_MI.MOROSITA_LETTERE " _
                                  & " where MOROSITA_LETTERE.ID_MOROSITA=" & IndiceMorosita _
                                  & " and  MOROSITA_LETTERE.BOLLETTINO IS NULL " _

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    If myReader1(0) = "AB" Then
                        TrovatoAB = True
                    End If

                    If myReader1(0) = "CD" Then
                        TrovatoCD = True
                    End If

                    If myReader1(0) = "EF" Then
                        TrovatoEF = True
                    End If
                Loop
                myReader1.Close()


                'par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "select count(*) as CONTA " _
                '                  & " from SISCOM_MI.MOROSITA_LETTERE " _
                '                  & " where MOROSITA_LETTERE.ID_MOROSITA=" & IndiceMorosita _
                '                  & " and  MOROSITA_LETTERE.BOLLETTINO IS NULL " _
                '                  & " and  MOROSITA_LETTERE.ID_CONTRATTO in " _
                '                            & " (select ID_CONTRATTO from SISCOM_MI.SALDI where NVL(SALDI.SALDO_1,0)=0  and NVL(SALDI.SALDO_2,0)>0 )"

                ''& " (select ID_CONTRATTO from SISCOM_MI.SALDI where N_BOL_SCADUTE_1=0 and N_BOL_SCADUTE_2>0)"
                '' " (select ID_CONTRATTO from SISCOM_MI.SALDI where FL_MOR_1=0 and FL_MOR_2=1)"

                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    If myReader1(0) > 0 Then
                '        TrovatoCD = True
                '    End If
                'End If
                'myReader1.Close()


                'par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "select count(*) as CONTA " _
                '                  & " from SISCOM_MI.MOROSITA_LETTERE " _
                '                  & " where MOROSITA_LETTERE.ID_MOROSITA=" & IndiceMorosita _
                '                  & " and  MOROSITA_LETTERE.BOLLETTINO IS NULL " _
                '                  & " and  MOROSITA_LETTERE.ID_CONTRATTO in " _
                '                            & " (select ID_CONTRATTO from SISCOM_MI.SALDI where NVL(SALDI.SALDO_1,0)>0  and NVL(SALDI.SALDO_2,0)>0 )"

                ''& " (select ID_CONTRATTO from SISCOM_MI.SALDI where N_BOL_SCADUTE_1>0 and N_BOL_SCADUTE_2>0)"

                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    If myReader1(0) > 0 Then
                '        TrovatoAB = True
                '    End If
                'End If
                'myReader1.Close()


                'par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "select count(*) as CONTA " _
                '                  & " from SISCOM_MI.MOROSITA_LETTERE " _
                '                  & " where MOROSITA_LETTERE.ID_MOROSITA=" & IndiceMorosita _
                '                  & " and  MOROSITA_LETTERE.BOLLETTINO IS NULL " _
                '                  & " and  MOROSITA_LETTERE.ID_CONTRATTO in " _
                '                            & " (select ID_CONTRATTO from SISCOM_MI.SALDI where NVL(SALDI.SALDO_1,0)>0  and NVL(SALDI.SALDO_2,0)=0 )"

                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    If myReader1(0) > 0 Then
                '        TrovatoEF = True
                '    End If
                'End If
                'myReader1.Close()



                'INIZIO Aa Bb **********************************************************************************************************
                '             **********************************************************************************************************
                If TrovatoAB = True Then
                    ' PRIMA VOLTA CHE VIENE ESEGUITA LA MOROSITA'

                    sPosteAler = ""

                    'CREO LA TRANSAZIONE
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans


                    'Riassunto = "<table style='width:100%;'>"
                    'Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COD.CONTRATTO</td>" _
                    '                                                                                         & "<td>INDIRIZZO</td>" _
                    '                                                                                         & "<td>COGN./RAG.SOCIALE</td>" _
                    '                                                                                         & "<td>NOME</td>" _
                    '                                                                                         & "<td>PERIODO DI RIF.</td>" _
                    '                                                                                         & "<td>EMISSIONE</td>" _
                    '                                                                                         & "<td>SCADENZA</td>" _
                    '                                                                                         & "<td>N.BOLLETTINO</td>" _
                    '                                                                                         & "<td>IMPORTO</td>" _
                    '                                                                                         & "<td>SPESE</td></tr>"
                    'Riassunto = Riassunto & "<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>"


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

                    Dim pdfMergeF As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)    'x FILE ZIP singolo

                    Dim fileNamePosteAler As String = "PosteAler_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss") & "-Aa_Bb"

                    Dim xx As String = "Morosita_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss") & "-Aa_Bb"
                    sNomeFile = xx
                    xx = xx & ".pdf"



                    'Dim sr2 As StreamReader = New StreamReader(Server.MapPath("Elenco.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    'contenutoRiassunto = sr2.ReadToEnd()
                    'sr2.Close()


                    Dim K As Integer = 2
                    'inizio a scrivere il file xls
                    With myExcelFile

                        '.CreateFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & sNomeFile & ".xls")
                        .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

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

                        .SetColumnWidth(1, 4, 30)

                        Dim Contenuto As String = ""
                        'Dim url As String
                        Dim pdfConverter1 As PdfConverter

                        Dim FlagStampa As Boolean = False


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "select MOROSITA.TIPO_INVIO,MOROSITA.DATA_PROTOCOLLO,MOROSITA.PROTOCOLLO_ALER," _
                                                  & " MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE,MOROSITA_LETTERE.COD_CONTRATTO,MOROSITA_LETTERE.Importo, MOROSITA_LETTERE.ID_ANAGRAFICA, MOROSITA_LETTERE.EMISSIONE, MOROSITA_LETTERE.INIZIO_PERIODO, MOROSITA_LETTERE.FINE_PERIODO, MOROSITA_LETTERE.NUM_LETTERE," _
                                                  & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA " _
                                           & " from  SISCOM_MI.MOROSITA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.MOROSITA_LETTERE " _
                                           & " where MOROSITA.ID=" & IndiceMorosita _
                                           & "   and MOROSITA_LETTERE.ID_ANAGRAFICA=ANAGRAFICA.ID " _
                                           & "   and MOROSITA.ID                   =MOROSITA_LETTERE.ID_MOROSITA " _
                                           & "   and MOROSITA_LETTERE.BOLLETTINO is NULL " _
                                           & "   and MOROSITA_LETTERE.TIPO_LETTERA='AB' " _
                                           & " order by MOROSITA_LETTERE.ID_ANAGRAFICA,MOROSITA_LETTERE.ID_CONTRATTO,MOROSITA_LETTERE.NUM_LETTERE "

                        '& "   and MOROSITA_LETTERE.ID_CONTRATTO in " _
                        '& " (select ID_CONTRATTO from SISCOM_MI.SALDI where NVL(SALDI.SALDO_1,0)>0  and NVL(SALDI.SALDO_2,0)>0 ) " _

                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader.Read
                            'LOOP di tutte le lettere di MOROSITA

                            'If Len(par.IfNull(myReader("PARTITA_IVA"), 0)) = 11 Or Len(par.IfNull(myReader("COD_FISCALE"), 0)) = 16 Then
                            idMorositaLettere = par.IfNull(myReader("ID_MOROSITA_LETTERE"), 0)

                            Tot_Bolletta = 0

                            CodiceContratto = par.IfNull(myReader("COD_CONTRATTO"), "")
                            TipoIngiunzione = "RECUPERO MOROSITA'"
                            VOCE = "150" '"626"
                            Importo = par.IfNull(myReader("Importo"), "0,00")
                            IdAnagrafica = par.IfNull(myReader("id_anagrafica"), "")


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "select COMPLESSI_IMMOBILIARI.ID as idcomplesso,EDIFICI.id as idedificio," _
                                                      & " RAPPORTI_UTENZA.*,UNITA_CONTRATTUALE.ID_UNITA,UNITA_CONTRATTUALE.SCALA,UNITA_CONTRATTUALE.INTERNO," _
                                                      & " TAB_FILIALI.NOME as ""NOME_FILIALE"",TAB_FILIALI.ACRONIMO,TAB_FILIALI.N_TELEFONO_VERDE AS N_TELEFONO," _
                                                      & " (INDIRIZZI.DESCRIZIONE||' N°'||INDIRIZZI.CIVICO)  AS ""INDIRIZZO_FILIALE"" " _
                                               & " from SISCOM_MI.EDIFICI," _
                                                    & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                                    & " SISCOM_MI.UNITA_IMMOBILIARI," _
                                                    & " SISCOM_MI.UNITA_CONTRATTUALE," _
                                                    & " SISCOM_MI.RAPPORTI_UTENZA," _
                                                    & " SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI, " _
                                                    & " SISCOM_MI.FILIALI_UI " _
                                             & " where COMPLESSI_IMMOBILIARI.ID     =EDIFICI.ID_COMPLESSO " _
                                             & "   and EDIFICI.ID                   =UNITA_IMMOBILIARI.ID_EDIFICIO " _
                                             & "   and UNITA_IMMOBILIARI.ID         =UNITA_CONTRATTUALE.ID_UNITA " _
                                             & "   and RAPPORTI_UTENZA.ID           =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                             & "   and FILIALI_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
                                             & "   and FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID (+) AND FILIALI_UI.INIZIO_VALIDITA <= '" & Format(Now, "yyyyMMdd") & "' AND FILIALI_UI.FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "' " _
                                             & "   and TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID (+) " _
                                             & "   and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE is null " _
                                             & "   and COD_CONTRATTO='" & CodiceContratto & "'"


                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                'Da RAPPORTI_UTENZA
                                Me.idcontratto.Value = par.IfNull(myReaderA("ID"), "-1")

                                presso_cor = par.IfNull(myReaderA("presso_cor"), "")
                                luogo_cor = par.IfNull(myReaderA("luogo_cor"), "")
                                civico_cor = par.IfNull(myReaderA("civico_cor"), "")
                                cap_cor = par.IfNull(myReaderA("cap_cor"), "")
                                indirizzo_cor = par.IfNull(myReaderA("VIA_cor"), "")
                                tipo_cor = par.IfNull(myReaderA("tipo_cor"), "")
                                sigla_cor = par.IfNull(myReaderA("sigla_cor"), "")

                                'Da UNITA_CONTRATTUALE
                                Me.idunita.Value = par.IfNull(myReaderA("ID_UNITA"), "-1")
                                sSCALA = par.IfNull(myReaderA("SCALA"), "")
                                sINTERNO = par.IfNull(myReaderA("INTERNO"), "")

                                idedificio = par.IfNull(myReaderA("idedificio"), "0")
                                idcomplesso = par.IfNull(myReaderA("idcomplesso"), "0")


                                If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
                                    TipoStampa = "Ingiunzione_Aa.htm"
                                Else
                                    TipoStampa = "Ingiunzione_Bb.htm"
                                End If
                                NoteBollette = "MOROSITA' MG" 'MG (M.AV. Global Service) per quello relativo alla morosità fino al 30/9/2009

                                'If par.IfNull(myReaderA("SALDO_1"), 0) > 0 And par.IfNull(myReaderA("SALDO_2"), 0) > 0 Then
                                '    If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
                                '        TipoStampa = "Ingiunzione_Aa.htm"
                                '    Else
                                '        TipoStampa = "Ingiunzione_Bb.htm"
                                '    End If
                                '    NoteBollette = "MOROSITA' MG" 'MG (M.AV. Global Service) per quello relativo alla morosità fino al 30/9/2009

                                'ElseIf par.IfNull(myReaderA("SALDO_1"), 0) > 0 And par.IfNull(myReaderA("SALDO_2"), 0) = 0 Then
                                '    If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
                                '        TipoStampa = "Ingiunzione_E.htm"
                                '    Else
                                '        TipoStampa = "Ingiunzione_F.htm"
                                '    End If
                                '    NoteBollette = "MOROSITA' MG" 'MG (M.AV. Global Service) per quello relativo alla morosità fino al 30/9/2009 e basta
                                'Else
                                '    If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
                                '        TipoStampa = "Ingiunzione_C.htm"
                                '    Else
                                '        TipoStampa = "Ingiunzione_D.htm"
                                '    End If
                                '    NoteBollette = "MOROSITA' MA" 'MA” (M.AV.) per quello relativo alla morosità dall’1/10/2009
                                'End If

                                Dim sr1 As StreamReader = New StreamReader(Server.MapPath(TipoStampa), System.Text.Encoding.GetEncoding("iso-8859-1"))
                                Dim contenutoOriginale As String = sr1.ReadToEnd()
                                sr1.Close()
                                Contenuto = contenutoOriginale

                                'INFORMAZIONI SULLA FILIALE
                                sNomeFiliale = par.IfNull(myReaderA("NOME_FILIALE"), "")
                                'sPosteAlerAcronimo = "CORE" 'par.IfNull(myReaderA("ACRONIMO"), "")
                                sNumTel_Filiale = par.IfNull(myReaderA("N_TELEFONO"), "")
                                sIndirizzo_Filiale = par.IfNull(myReaderA("INDIRIZZO_FILIALE"), "")
                                '****************************

                            End If
                            myReaderA.Close()

                            par.cmd.CommandText = "select acronimo from siscom_mi.tab_filiali where id =" & idStruttura
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                sPosteAlerAcronimo = par.IfNull(myReaderA("acronimo"), "----")
                            Else
                                sPosteAlerAcronimo = "----"
                            End If
                            myReaderA.Close()

                            'TIPO INVIO
                            Select Case par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")
                                Case "Racc. A.R"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
                                Case "Racc. mano"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A MANO")
                                Case "FAX"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "FAX.")
                                Case Else
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
                            End Select
                            '************************************

                            Dim Titolo As String = ""
                            Dim Nome As String = ""
                            Dim Cognome As String = ""
                            Dim CF As String = ""

                            Dim ID_BOLLETTA As Long = 0

                            'ANAGRAFICA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA where ID=" & IdAnagrafica
                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then

                                sPosteAlerCodUtente = Format(CDbl(par.IfNull(myReaderA("ID"), 0)), "000000000000")
                                If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                                    Titolo = "Spettabile"
                                    'Titolo = "Spettabile Ditta"
                                    Cognome = par.IfNull(myReaderA("ragione_sociale"), "")
                                    Nome = ""
                                    CF = par.IfNull(myReaderA("partita_iva"), "")
                                Else
                                    Titolo = "Spettabile"
                                    'If par.IfNull(myReaderA("sesso"), "") = "M" Then
                                    '    Titolo = "Egregio Signore"
                                    'Else
                                    '    Titolo = "Gentile Signora"
                                    'End If
                                    Cognome = par.IfNull(myReaderA("cognome"), "")
                                    Nome = par.IfNull(myReaderA("nome"), "")
                                    CF = par.IfNull(myReaderA("cod_fiscale"), "")
                                End If
                            End If
                            myReaderA.Close()
                            '*********************************************
                            par.cmd.CommandText = "select valore from sepa.parameter where id = 120"
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                sPosteAlerAcronimo = par.IfNull(myReaderA(0), "BA0301/NUMI")
                            Else
                                sPosteAlerAcronimo = "BA0301/NUMI"
                            End If
                            myReaderA.Close()
                            Contenuto = Replace(Contenuto, "$data$", par.FormattaData(par.IfNull(myReader("DATA_PROTOCOLLO"), "")))
                            Contenuto = Replace(Contenuto, "$protocollo$", "" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))
                            'Contenuto = Replace(Contenuto, "$protocollo$", "GL0000/" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))

                            Contenuto = Replace(Contenuto, "$codcontratto$", CodiceContratto)
                            Contenuto = Replace(Contenuto, "$codUI$", Strings.Left(CodiceContratto, Strings.Len(CodiceContratto) - 2))
                            If UCase(Trim(Trim(Cognome) & " " & Trim(Nome))) <> UCase(Trim(presso_cor)) Then
                                'If UCase(Trim(Cognome) & " " & Trim(Nome)) <> UCase(Trim(presso_cor)) Then
                                Contenuto = Replace(Contenuto, "$nominativo$", Cognome & " " & Nome & "<br />c/o " & presso_cor)
                            Else
                                Contenuto = Replace(Contenuto, "$nominativo$", presso_cor)
                            End If
                            sPosteAlerNominativo = Cognome & " " & Nome 'POSTE

                            If sINTERNO <> "" Then
                                If sSCALA <> "" Then
                                    Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO & " SCALA " & sSCALA)
                                Else
                                    Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO)
                                End If
                            ElseIf sSCALA <> "" Then
                                Contenuto = Replace(Contenuto, "$indirizzo2$", "SCALA " & sSCALA)
                            End If

                            sPosteAlerInterno = sINTERNO    'POSTE 

                            For i = 1 To Strings.Len(sSCALA)
                                If Char.IsDigit(Strings.Mid(sSCALA, i, 1)) = False Then
                                    sPosteAlerScala = Strings.Mid(sSCALA, i, Strings.Len(sSCALA))  'POSTE 
                                    Exit For
                                End If
                            Next i

                            Contenuto = Replace(Contenuto, "$indirizzo0$", tipo_cor & " " & indirizzo_cor & ", " & civico_cor)
                            Contenuto = Replace(Contenuto, "$indirizzo1$", cap_cor & " " & luogo_cor & " " & sigla_cor)
                            'Contenuto = Replace(Contenuto, "$indirizzo$", indirizzo_cor & ", " & civico_cor & "</br>" & cap_cor & " " & luogo_cor & " " & sigla_cor)

                            Contenuto = Replace(Contenuto, "$titolo$", Titolo & " " & Cognome & " " & Nome)

                            sPosteAlerInd = tipo_cor & " " & indirizzo_cor & " " & civico_cor 'POSTE 
                            sPosteAlerCAP = cap_cor                          'POSTE 
                            sPosteAlerLocalita = luogo_cor                   'POSTE 
                            sPosteAlerProv = sigla_cor                       'POSTE 


                            'INFORMAZIONI FILIALE
                            Contenuto = Replace(Contenuto, "$NomeFiliale$", sNomeFiliale)
                            Contenuto = Replace(Contenuto, "$IndirizzoFiliale$", sIndirizzo_Filiale)


                            par.cmd.CommandText = "select siscom_mi.getstatocontratto(" & idcontratto.Value & ") from dual"
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                If par.IfNull(myReaderA(0), "") = "CHIUSO" Then
                                    Contenuto = Replace(Contenuto, "$chiuso$", ".")
                                    sNumTel_Filiale = "Nel caso Lei desideri ricevere informazioni anche in merito alla possibilità di rateizzare " _
                                                    & "il debito maturato, al fine di evitarLe inutili attese, potrà contattare telefonicamente " _
                                                    & "il Settore Recupero Morosità presso la sede di viale Romagna n.26 al numero 02/73922319 per fissare un appuntamento."

                                Else
                                    Contenuto = Replace(Contenuto, "$chiuso$", " fino, se necessario, all’esecuzione dello sfratto per morosità.")
                                    sNumTel_Filiale = "Nel caso Lei desideri ricevere informazioni anche in merito alla possibilità di rateizzare " _
                                                    & "il debito maturato, al fine di evitarLe inutili attese, potrà contattare telefonicamente " _
                                                    & "la filiale al seguente numero " & sNumTel_Filiale & " per fissare un appuntamento. "
                                End If
                            Else
                                Contenuto = Replace(Contenuto, "$chiuso$", " fino, se necessario, all’esecuzione dello sfratto per morosità.")

                            End If
                            myReaderA.Close()
                            Contenuto = Replace(Contenuto, "$TelFiliale$", sNumTel_Filiale)


                            'Da MOROSITA_LETTERE
                            'NOTA: prima del 02/Sett/2001 era di 40 gg ora è di 60 + 20 + controllo se capita di sabato o domenica
                            '08/06/2012 su richiesta dell'avvocato RUMORE si riporta la data scadenza a 40gg dalla data di emissione
                            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            ' 12/09/2014 ora scadenzabollettino è definita nella maschera della creazione della morosita
                            ScadenzaBollettino = Request.QueryString("SCADBOL")


                            Dim d1 As Date
                            d1 = New Date(Mid(ScadenzaBollettino, 1, 4), Mid(ScadenzaBollettino, 5, 2), Mid(ScadenzaBollettino, 7, 2))
                            If d1.DayOfWeek = DayOfWeek.Saturday Then
                                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 42, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            ElseIf d1.DayOfWeek = DayOfWeek.Sunday Then
                                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 41, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            End If
                            '************

                            periodo = par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))

                            If PeriodoXLS_INIZIO > par.IfNull(myReader("inizio_periodo"), 0) Or PeriodoXLS_INIZIO = 0 Then
                                PeriodoXLS_INIZIO = par.IfNull(myReader("inizio_periodo"), 0)
                            End If

                            If PeriodoXLS_FINE < par.IfNull(myReader("fine_periodo"), 0) Or PeriodoXLS_FINE = 0 Then
                                PeriodoXLS_FINE = par.IfNull(myReader("fine_periodo"), 0)
                            End If

                            Contenuto = Replace(Contenuto, "$spazi$", "")
                            Contenuto = Replace(Contenuto, "$spazi1$", "")


                            'Contenuto = Replace(Contenuto, "$tipo$", TipoIngiunzione)
                            If TipoStampa = "Ingiunzione_Aa.htm" Or TipoStampa = "Ingiunzione_Bb.htm" Then

                                ValImporto1 = par.IfNull(myReader("Importo"), 0)

                                If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                                    par.cmd.Parameters.Clear()
                                    par.cmd.CommandText = "select IMPORTO" _
                                                       & " from SISCOM_MI.MOROSITA_LETTERE " _
                                                       & " where ID_MOROSITA=" & IndiceMorosita _
                                                       & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                       & "   and ID_ANAGRAFICA=" & IdAnagrafica _
                                                       & "   and BOLLETTINO is NULL " _
                                                       & "   and NUM_LETTERE=2"

                                    myReaderA = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then
                                        ValImporto2 = par.IfNull(myReaderA("Importo"), 0)
                                    End If
                                    myReaderA.Close()

                                    Contenuto = Replace(Contenuto, "$importo1$", Format(ValImporto1, "##,##0.00"))
                                    Contenuto = Replace(Contenuto, "$importo2$", Format(ValImporto2, "##,##0.00"))

                                    ValImporto1 = ValImporto1 + ValImporto2
                                    Contenuto = Replace(Contenuto, "$importoTOT$", Format(ValImporto1, "##,##0.00"))

                                End If

                            Else
                                Contenuto = Replace(Contenuto, "$importoTOT$", Format(CDbl(Importo), "##,##0.00"))
                            End If


                            If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then

                                'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

                                sr.WriteLine(Contenuto)
                                sr.Close()

                                'Dim url As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto
                                url = Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto
                                pdfConverter1 = New PdfConverter

                                Licenza = Session.Item("LicenzaHtmlToPdf")
                                If Licenza <> "" Then
                                    pdfConverter1.LicenseKey = Licenza
                                End If

                                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                                pdfConverter1.PdfDocumentOptions.LeftMargin = 40
                                pdfConverter1.PdfDocumentOptions.RightMargin = 40
                                pdfConverter1.PdfDocumentOptions.TopMargin = 17
                                pdfConverter1.PdfDocumentOptions.BottomMargin = 30
                                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                                pdfConverter1.PdfFooterOptions.FooterText = ("")
                                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
                                pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")

                                'If sPosteAler <> "" Then
                                '    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
                                'Else
                                '    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
                                'End If

                                ''****************EVENTI CONTRATTI UNA PER CONTRATTO***************
                                Dim sStr1 As String = "Inviato il MAV MG, il MAV MA e la lettera di messa in mora con " & par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")

                                par.cmd.Parameters.Clear()

                                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_CONTRATTI " _
                                                          & " (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                    & "values (:id_contratto,:id_operatore,:data,:cod_evento,:motivo)"

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "F176"))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                '************************************************

                                'INIZIO CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 1° PARTE #############################################
                                sNomeFileMorLettera = "MorositaLettera_" & idMorositaLettere & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

                                Dim LicenzaF As String = Session.Item("LicenzaPdfMerge")
                                If LicenzaF <> "" Then
                                    pdfMergeF.LicenseKey = LicenzaF
                                End If
                                'FINE CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 1° PARTE ###############################################


                                ' '' Ricavo ID di POSTALER per PostAler.txt
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                                myReaderA = par.cmd.ExecuteReader()
                                If myReaderA.Read Then
                                    sPosteAlerIA = myReaderA(0)
                                End If
                                myReaderA.Close()


                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                                                  & " values (" & sPosteAlerIA & "," & idMorositaLettere & ",1)"
                                par.cmd.ExecuteNonQuery()
                                '******************************************************************

                                If sPosteAler <> "" Then
                                    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                                Else
                                    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                                End If


                            Else
                                NoteBollette = "MOROSITA' MA" 'MA” (M.AV.) per quello relativo alla morosità dall’1/10/2009


                                ' CREO UN SOLO RECORD DI POSTALER (anche se ci sono 2 MAV), ed al secondo MAV aggiorno ID_LETTERA_2
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.POSTALER " _
                                                   & " set   ID_LETTERA_2=" & idMorositaLettere _
                                                   & " where ID=" & sPosteAlerIA
                                par.cmd.ExecuteNonQuery()


                            End If


                            ' '' Ricavo ID di POSTALER per PostAler.txt
                            'par.cmd.Parameters.Clear()
                            'par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                            'myReaderA = par.cmd.ExecuteReader()
                            'If myReaderA.Read Then
                            '    sPosteAlerIA = myReaderA(0)
                            'End If
                            'myReaderA.Close()


                            'par.cmd.Parameters.Clear()
                            'par.cmd.CommandText = "insert into SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                            '                  & " values (" & sPosteAlerIA & "," & idMorositaLettere & ",1)"
                            'par.cmd.ExecuteNonQuery()
                            ''******************************************************************

                            'If sPosteAler <> "" Then
                            '    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                            'Else
                            '    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                            'End If



                            'Dim SpNotifica As Double = 0
                            'If Importo + spese_notifica + SPESEmav >= APPLICABOLLO Then
                            '    Contenuto = Replace(Contenuto, "$importo1$", Format(CDbl(Importo + spese_notifica + SPESEmav + BOLLO), "##,##0.00"))
                            '    SpNotifica = CDbl(spese_notifica + SPESEmav + BOLLO)
                            'Else
                            '    Contenuto = Replace(Contenuto, "$importo1$", Format(CDbl(Importo + spese_notifica + SPESEmav), "##,##0.00"))
                            '    SpNotifica = CDbl(spese_notifica + SPESEmav)
                            'End If

                            '**********peppe modify 21/10/2010***********
                            'Contenuto = Replace(Contenuto, "$importospnotifica$", Format(SpNotifica, "##,##0.00"))

                            'Contenuto = Replace(Contenuto, "$responsabile$", RESPONSABILE)
                            'Contenuto = Replace(Contenuto, "$dirigente$", DIRIGENTE)
                            'Contenuto = Replace(Contenuto, "$trattatada$", TRATTATADA)


                            'Contenuto = Replace(Contenuto, "$trattatada$", Operatore)
                            'Contenuto = Replace(Contenuto, "$periodo$", par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")))
                            'Contenuto = Replace(Contenuto, "$titolo$", par.FormattaData(par.IfNull(myReader("emissione"), "")))
                            'Contenuto = Replace(Contenuto, "$emissione$", par.FormattaData(par.IfNull(myReader("emissione"), "")))
                            'Contenuto = Replace(Contenuto, "$scadenzabollettino$", par.FormattaData(ScadenzaBollettino))



                            Dim Nome1 As String = ""
                            Dim Nome2 As String = ""

                            If UCase(Trim(Trim(Cognome) & " " & Trim(Nome))) <> UCase(Trim(presso_cor)) Then
                                Nome1 = Cognome & " " & Nome
                                Nome2 = presso_cor
                            Else
                                Nome1 = presso_cor
                            End If

                            ' INSERT BOL_BOLLETTE
                            'par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                            '                            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                            '                            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                            '                            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                            '                            & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                            '                            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                            '                            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA) " _
                            '                   & " values " _
                            '                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999 , '" & Format(Now, "yyyyMMdd") & "','" _
                            '                        & ScadenzaBollettino & "', NULL,NULL,NULL,'RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "'," _
                            '                        & idcontratto.Value & " ," & par.RicavaEsercizioCorrente & "," & idunita.Value & "," _
                            '                        & "'0', ''," & IdAnagrafica & "," _
                            '                        & "'" & par.PulisciStrSql(Nome1) & "'," _
                            '                        & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) & "'," _
                            '                        & "'" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") & "'," _
                            '                        & "'" & par.PulisciStrSql(Nome2) & "', '" & par.IfNull(myReader("inizio_periodo"), "") & "'," _
                            '                        & "'" & par.IfNull(myReader("fine_periodo"), "") & "'," _
                            '                        & "'0', " & idcomplesso & ", '', NULL, '', " _
                            '                        & Year(Now) & ", '', " & idedificio & ", NULL, NULL,'MOR'," & IndiceMorosita & ")"
                            'par.cmd.ExecuteNonQuery()


                            ' '' Ricavo ID_BOLLETTA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM dual "
                            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderB.Read Then
                                ID_BOLLETTA = myReaderB(0)
                            End If
                            myReaderB.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                                                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, " _
                                                        & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA,ID_TIPO) " _
                                                & "values (:id,:n_rata,:data_emissione,:data_scadenza," _
                                                        & ":note,:id_contratto,:id_esercizio_f," _
                                                        & ":id_unita,:fl_annullata,:pagabile_presso,:cod_affittuario,:intestatario," _
                                                        & ":indirizzo,:cap_citta,:presso,:riferimento_da,:riferimento_a," _
                                                        & ":fl_stampato,:id_complesso,:data_ins_pagamento,:importo_pagato,:note_pagamento," _
                                                        & ":anno,:operatore_pag,:id_edificio,:data_annullo_pag,:operatore_annullo_pag,:rif_file,:id_morosita,4)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", ID_BOLLETTA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_rata", 999))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", ScadenzaBollettino))

                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_1_sollecito", "NULL"))
                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_2_sollecito", "NULL"))
                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_pagamento", "NULL"))

                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", NoteBollette))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_esercizio_f", RitornaNullSeIntegerMenoUno(par.RicavaEsercizioCorrente)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idunita.Value))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_annullata", "0"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pagabile_presso", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_affittuario", RitornaNullSeIntegerMenoUno(IdAnagrafica)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("intestatario", Strings.Left(Nome1, 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", Strings.Left(tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor), 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap_citta", Strings.Left(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")", 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("presso", Strings.Left(Nome2, 100)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_da", par.IfNull(myReader("inizio_periodo"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_a", par.IfNull(myReader("fine_periodo"), "")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_stampato", "0"))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(idcomplesso)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ins_pagamento", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_pagato", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note_pagamento", ""))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Year(Now)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_pag", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(idedificio)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_annullo_pag", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_annullo_pag", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_file", "MOR"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", IndiceMorosita))


                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()




                            'par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                            'Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReaderB.Read Then
                            '    ID_BOLLETTA = myReaderB(0)
                            'Else
                            '    ID_BOLLETTA = -1
                            'End If
                            'myReaderB.Close()

                            ' INSERT tutte le sotto voci di BOL_BOLLETTE_VOCI
                            'RECUPERO MOROSITA' (150)
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                & " values " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                        & ID_BOLLETTA & "," _
                                                        & VOCE & "," _
                                                        & par.VirgoleInPunti(Importo) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + Importo

                            If NoteBollette = "MOROSITA' MA" Then
                                'SPESE DI NOTIFICA (628)
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                   & " values " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                        & ID_BOLLETTA _
                                                        & ",628," _
                                                        & par.VirgoleInPunti(spese_notifica) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + spese_notifica
                            End If

                            'SPESE MAV
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                               & " values " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                    & ID_BOLLETTA _
                                                    & ",407," _
                                                    & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + SPESEmav

                            'BOLLO
                            If Tot_Bolletta >= APPLICABOLLO Then
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                   & " values " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                            & ID_BOLLETTA _
                                                            & ",95," _
                                                            & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + BOLLO
                            End If
                            '******************************************************

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

                            RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA, CodiceContratto)

                            RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                            RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                            RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                            'RichiestaEmissioneMAV.codiceFiscaleDebitore = CF
                            RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                            '30/04/2012 Elimino controllo del vuoto perchè necessario azzerare NOMEDEBITORE del metodo RichiestaEmissioneMAV
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

                            Try
                                '12/01/2015 PUCCIA Nuova connessione  tls ssl
                                If DisabilitaExpect100Continue = "1" Then
                                    System.Net.ServicePointManager.Expect100Continue = False
                                End If
                                par.cmd.Parameters.Clear()
                                '/*/*/*/*/*tls v1
                                Dim v As String = ""
                                par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                                v = par.cmd.ExecuteScalar
                                System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                                '/*/*/*/*/*tls v1
                                System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler

                                Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                            Catch ex As Exception

                                par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "' where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                par.myTrans.Commit()

                                If RiControllaMAVCreati() > 0 Then
                                    Response.Write("<SCRIPT>alert('Attenzione...Alcuni MAV non sono stati creati! \n Rientrare nella singola scheda dell\'inquilino è rigenerare il MAV tramite il tasto PROCEDI!');</SCRIPT>")
                                End If

                                par.cmd.Dispose()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                                Session.Item("LAVORAZIONE") = "0"
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>" & ex.Message & " !</p>")
                                Exit Sub
                            End Try

                            If Esito.codiceRisultato = "0" Then
                                'If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                                '    pdfMerge.AppendPDFFile(url & ".pdf")
                                '    IO.File.Delete(url & ".htm")
                                'End If

                                'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".pdf"

                                binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length - 1)
                                outFile.Close()
                                'SPOSTO DOPO CREAZIONE LETTERA E POI MAV
                                'pdfMerge.AppendPDFFile(outputFileName)
                                'pdfMergeF.AppendPDFFile(outputFileName)

                                ' se la banca emette correttamente i MAV allora:
                                ' SETTO a BOL_BOLLETTE che è stato stampato e il numero di bollettino
                                num_bollettino = Esito.numeroMAV
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                   & " set   FL_STAMPATO='1'," _
                                                   & "       rif_bollettino='" & num_bollettino & "'" _
                                                   & " where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                'Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 9pt;'><td style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & CodiceContratto & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & tipo_cor & " " & indirizzo_cor & ", " & civico_cor & " " & cap_cor & " " & luogo_cor & " " & sigla_cor & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Cognome & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Nome & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("emissione"), "")) & " </td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(ScadenzaBollettino) & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & num_bollettino & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Importo), "##,##0.00") & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Tot_Bolletta - Importo), "##,##0.00") & "</td></tr>"

                                If par.IfNull(myReader("NUM_LETTERE"), 1) = 2 Or TipoStampa = "Ingiunzione_C.htm" Or TipoStampa = "Ingiunzione_D.htm" Then

                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(Titolo))
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(Cognome & " " & Nome))
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(tipo_cor & " " & indirizzo_cor & ", " & civico_cor))
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(cap_cor & " " & luogo_cor & " " & sigla_cor))

                                    K = K + 1
                                End If




                                'SOLO x TEST , per la produxione toglie il commento sopra e rimettrerlo sotto e togliere MOROSITA_EVENTI
                                'INIZIO TEST
                                'par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
                                '                  & "     set BOLLETTINO='" & num_bollettino & "'," _
                                '                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                '                  & "         COD_STATO='M04'" _
                                '                  & " where ID=" & idMorositaLettere

                                'par.cmd.ExecuteNonQuery()

                                'par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                '                       & "  (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                '                       & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL," & idMorositaLettere & "," _
                                '                                & Session.Item("ID_OPERATORE") & "," & Format(Now, "yyyyMMddHHmmss") & "," _
                                '                                & "'M04','Ricevuta PostAler: RITIRATA DAL DESTINATARIO')"

                                'par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                '************ FINE TEST


                                'UPDATE BOL_BOLLETTE VECCHIE raggruppate per la BOLLETTA MOROSITA' NUOVA
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_BOLLETTA_RIC=" & ID_BOLLETTA _
                                                    & " where ID_MOROSITA_LETTERA=" & idMorositaLettere _
                                                    & "   and ID_MOROSITA=" & IndiceMorosita _
                                                    & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()
                                '************************************

                                'prima del 14/02/2012 stava in crealettere
                                'UPDATE BOL_BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                                   & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                                                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                        & "  where ID_MOROSITA_LETTERA=" & idMorositaLettere _
                                                                        & "    and ID_MOROSITA=" & IndiceMorosita _
                                                                        & "    and FL_ANNULLATA=0 " _
                                                                        & "    and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) & ") " _
                                                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()


                                'IMPORTO_CANONE    
                                ValSommaImportoCanone = 0
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                                  & "  from SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA " _
                                                  & "  where BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID " _
                                                  & "    and T_VOCI_BOLLETTA.GRUPPO=4 " _
                                                  & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                         & " where ID_MOROSITA=" & IndiceMorosita _
                                                                         & "   and FL_ANNULLATA=0 " _
                                                                         & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                                         & "   and ID>0 " _
                                                                         & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                                myReaderB = par.cmd.ExecuteReader()
                                If myReaderB.Read Then
                                    ValSommaImportoCanone = par.IfNull(myReaderB(0), 0)
                                End If
                                myReaderB.Close()
                                '***********************************

                                'IMPORTO_ONERI    
                                ValSommaImportoOneri = 0
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                                  & "  from SISCOM_MI.BOL_BOLLETTE_VOCI" _
                                                  & "  where ID_VOCE NOT IN (select ID from SISCOM_MI.T_VOCI_BOLLETTA where GRUPPO=4) " _
                                                  & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                         & " where ID_MOROSITA=" & IndiceMorosita _
                                                                         & "   and FL_ANNULLATA=0 " _
                                                                         & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                                         & "   and ID>0 " _
                                                                         & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                                myReaderB = par.cmd.ExecuteReader()
                                If myReaderB.Read Then
                                    ValSommaImportoOneri = par.IfNull(myReaderB(0), 0)
                                End If
                                myReaderB.Close()
                                '***********************************

                                'UPDATE MOROSITA_LETTERE
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
                                                  & "     set BOLLETTINO='" & num_bollettino & "'," _
                                                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                                  & "         COD_STATO='M00'," _
                                                  & "         IMPORTO_BOLLETTA=" & par.VirgoleInPunti(Tot_Bolletta) & "," _
                                                  & "         IMPORTO_CANONE= " & par.VirgoleInPunti(ValSommaImportoCanone) & "," _
                                                  & "         IMPORTO_ONERI= " & par.VirgoleInPunti(ValSommaImportoOneri) _
                                                  & " where ID=" & idMorositaLettere

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()

                                ' Response.Redirect("ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf")

                            Else
                                'lblErrore.Visible = True

                                ' se la banca NON emette correttamente i MAV allora:
                                ' ELIMINO  BOL_BOLLETTE 

                                par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'  where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                                If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml")) = True Then
                                    FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                                End If

                                'Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

                                'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
                                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & FileDaCreare & ".xml"

                                binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length)
                                outFile.Close()
                            End If


                            'Else
                            '    If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                            '        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("ragione_sociale"), "") & " non sono stati stampati perchè la partita iva non ha un formato corretto!</p>")
                            '    Else
                            '        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & " non sono stati stampati perchè il codice fiscale non ha un formato corretto!</p>")
                            '    End If
                            'End If

                            'AGGIUNGO LA LETTERA
                            If par.IfNull(myReader("NUM_LETTERE"), 1) = 2 Or TipoStampa = "Ingiunzione_C.htm" Or TipoStampa = "Ingiunzione_D.htm" Then
                                pdfMerge.AppendPDFFile(url & ".pdf")
                                pdfMergeF.AppendPDFFile(url & ".pdf")
                                pdfMerge.AppendPDFFile(outputFileName)
                                pdfMergeF.AppendPDFFile(outputFileName)

                                IO.File.Delete(url & ".htm")


                                'Per la ogni MOROSITA_LETTERA creo un File ZIP con il/i MAV e la Lettera
                                'INIZIO CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 2° PARTE #############################################
                                pdfMergeF.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & sNomeFileMorLettera)
                                Dim zipficF As String
                                Dim objCrc32F As New Crc32()
                                Dim strmZipOutputStreamF As ZipOutputStream
                                Dim strFileF As String
                                Dim strmFileF As FileStream

                                zipficF = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & Strings.Left(sNomeFileMorLettera, Strings.Len(sNomeFileMorLettera) - 4) & ".zip")

                                strmZipOutputStreamF = New ZipOutputStream(File.Create(zipficF))
                                strmZipOutputStreamF.SetLevel(6)

                                strFileF = Server.MapPath("..\FileTemp\") & sNomeFileMorLettera
                                strmFileF = File.OpenRead(strFileF)
                                Dim abyBufferF(Convert.ToInt32(strmFileF.Length - 1)) As Byte
                                strmFileF.Read(abyBufferF, 0, abyBufferF.Length)

                                Dim sFile1F As String = Path.GetFileName(strFileF)
                                Dim theEntryF As ZipEntry = New ZipEntry(sFile1F)
                                Dim fiF As New FileInfo(strFileF)

                                theEntryF.DateTime = fiF.LastWriteTime
                                theEntryF.Size = strmFileF.Length
                                strmFileF.Close()
                                objCrc32F.Reset()
                                objCrc32F.Update(abyBufferF)
                                theEntryF.Crc = objCrc32F.Value
                                strmZipOutputStreamF.PutNextEntry(theEntryF)
                                strmZipOutputStreamF.Write(abyBufferF, 0, abyBufferF.Length)
                                File.Delete(strFileF)

                                strmZipOutputStreamF.Finish()
                                strmZipOutputStreamF.Close()
                                '***** FINE CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 2° PARTE ########################################

                            End If


                        Loop
                        myReader.Close()
                        .CloseFile()
                    End With

                    'Riassunto = Riassunto & "</table>"
                    contenutoRiassunto = Replace(contenutoRiassunto, "$riassunto$", Riassunto)

                    contenutoRiassunto = Replace(contenutoRiassunto, "$periodo$", par.FormattaData(PeriodoXLS_INIZIO) & " - " & par.FormattaData(PeriodoXLS_FINE))

                    PeriodoXLS_INIZIO = 0
                    PeriodoXLS_FINE = 0

                    'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & "-Aa_Bb" & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

                    'sr3.WriteLine(contenutoRiassunto)
                    'sr3.Close()


                    ''Dim url1 As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita
                    'Dim url1 As String = Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & "-Aa_Bb"

                    'Dim pdfConverter As PdfConverter = New PdfConverter

                    'Licenza = Session.Item("LicenzaHtmlToPdf")
                    'If Licenza <> "" Then
                    '    pdfConverter.LicenseKey = Licenza
                    'End If


                    'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                    'pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
                    'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfDocumentOptions.ShowFooter = False
                    'pdfConverter.PdfDocumentOptions.LeftMargin = 30
                    'pdfConverter.PdfDocumentOptions.RightMargin = 30
                    'pdfConverter.PdfDocumentOptions.TopMargin = 17
                    'pdfConverter.PdfDocumentOptions.BottomMargin = 30
                    'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfFooterOptions.FooterText = ("")
                    'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                    'pdfConverter.PdfFooterOptions.DrawFooterLine = False
                    'pdfConverter.PdfFooterOptions.PageNumberText = ""
                    'pdfConverter.PdfFooterOptions.ShowPageNumber = False
                    'pdfConverter.SavePdfFromUrlToFile(url1 & ".htm", url1 & ".pdf")

                    ' ''AGGIUNGO LA LETTERA
                    ''pdfMerge.AppendPDFFile(url & ".pdf")
                    ''IO.File.Delete(url & ".htm")

                    ''AGGIUNGO L'elenco
                    'pdfMerge.AppendPDFFile(url1 & ".pdf")
                    'IO.File.Delete(url1 & ".htm")


                    'COMMIT
                    par.myTrans.Commit()
                    par.cmd.Dispose()


                    'pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx)
                    pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & xx)

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String
                    Dim strFile As String
                    Dim strmFile As FileStream

                    'zipfic = Server.MapPath("Varie\" & sNomeFile & ".zip")
                    zipfic = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".zip")

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)

                    'scrivo file XLS
                    'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".xls")
                    strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
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

                    'scrivo file PDF
                    'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx
                    strFile = Server.MapPath("..\FileTemp\") & xx
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

                    'Scrivo FILE TXT POSTE *******************************
                    Using sw As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt"))
                        sw.Write(sPosteAler)
                        sw.Close()
                    End Using

                    strFile = Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer2(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer2, 0, abyBuffer2.Length)

                    Dim sFile2 As String = Path.GetFileName(strFile)
                    theEntry = New ZipEntry(sFile2)
                    fi = New FileInfo(strFile)

                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer2)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer2, 0, abyBuffer2.Length)
                    File.Delete(strFile)
                    '******************************************

                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                End If
                'FINE Aa Bb **********************************************************************************************************
                '           **********************************************************************************************************




                'INIZIO C D **********************************************************************************************************
                '           **********************************************************************************************************
                If TrovatoCD = True Then

                    sPosteAler = ""
                    ' PRIMA VOLTA CHE VIENE ESEGUITA LA MOROSITA'

                    'CREO LA TRANSAZIONE
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans


                    'Riassunto = "<table style='width:100%;'>"
                    'Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COD.CONTRATTO</td>" _
                    '                                                                                         & "<td>INDIRIZZO</td>" _
                    '                                                                                         & "<td>COGN./RAG.SOCIALE</td>" _
                    '                                                                                         & "<td>NOME</td>" _
                    '                                                                                         & "<td>PERIODO DI RIF.</td>" _
                    '                                                                                         & "<td>EMISSIONE</td>" _
                    '                                                                                         & "<td>SCADENZA</td>" _
                    '                                                                                         & "<td>N.BOLLETTINO</td>" _
                    '                                                                                         & "<td>IMPORTO</td>" _
                    '                                                                                         & "<td>SPESE</td></tr>"
                    'Riassunto = Riassunto & "<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>"


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

                    Dim pdfMergeF As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)    'x FILE ZIP singolo

                    Dim fileNamePosteAler As String = "PosteAler_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss") & "-C_D"

                    Dim xx As String = "Morosita_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss") & "-C_D"
                    sNomeFile = xx
                    xx = xx & ".pdf"


                    'Dim sr2 As StreamReader = New StreamReader(Server.MapPath("Elenco.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    'contenutoRiassunto = sr2.ReadToEnd()
                    'sr2.Close()


                    Dim K As Integer = 2
                    'inizio a scrivere il file xls
                    With myExcelFile

                        '.CreateFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & sNomeFile & ".xls")
                        .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

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

                        .SetColumnWidth(1, 4, 30)

                        Dim Contenuto As String = ""
                        'Dim url As String
                        Dim pdfConverter1 As PdfConverter

                        Dim FlagStampa As Boolean = False


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "select MOROSITA.TIPO_INVIO,MOROSITA.DATA_PROTOCOLLO,MOROSITA.PROTOCOLLO_ALER," _
                                                  & " MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE,MOROSITA_LETTERE.COD_CONTRATTO,MOROSITA_LETTERE.Importo, MOROSITA_LETTERE.ID_ANAGRAFICA, MOROSITA_LETTERE.EMISSIONE, MOROSITA_LETTERE.INIZIO_PERIODO, MOROSITA_LETTERE.FINE_PERIODO, MOROSITA_LETTERE.NUM_LETTERE," _
                                                  & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA " _
                                           & " from  SISCOM_MI.MOROSITA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.MOROSITA_LETTERE " _
                                           & " where MOROSITA.ID=" & IndiceMorosita _
                                           & "   and MOROSITA_LETTERE.ID_ANAGRAFICA=ANAGRAFICA.ID " _
                                           & "   and MOROSITA.ID                   =MOROSITA_LETTERE.ID_MOROSITA " _
                                           & "   and MOROSITA_LETTERE.BOLLETTINO is NULL " _
                                           & "   and MOROSITA_LETTERE.TIPO_LETTERA='CD' " _
                                           & " order by MOROSITA_LETTERE.ID_ANAGRAFICA,MOROSITA_LETTERE.ID_CONTRATTO,MOROSITA_LETTERE.NUM_LETTERE "

                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader.Read
                            'LOOP di tutte le lettere di MOROSITA


                            'If Len(par.IfNull(myReader("PARTITA_IVA"), 0)) = 11 Or Len(par.IfNull(myReader("COD_FISCALE"), 0)) = 16 Then
                            idMorositaLettere = par.IfNull(myReader("ID_MOROSITA_LETTERE"), 0)

                            Tot_Bolletta = 0

                            CodiceContratto = par.IfNull(myReader("COD_CONTRATTO"), "")
                            TipoIngiunzione = "RECUPERO MOROSITA'"
                            VOCE = "150" '"626"
                            Importo = par.IfNull(myReader("Importo"), "0,00")
                            IdAnagrafica = par.IfNull(myReader("id_anagrafica"), "")

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "select COMPLESSI_IMMOBILIARI.ID as idcomplesso,EDIFICI.id as idedificio," _
                                                      & " RAPPORTI_UTENZA.*,UNITA_CONTRATTUALE.ID_UNITA,UNITA_CONTRATTUALE.SCALA,UNITA_CONTRATTUALE.INTERNO," _
                                                      & " TAB_FILIALI.NOME as ""NOME_FILIALE"",TAB_FILIALI.ACRONIMO,TAB_FILIALI.N_TELEFONO_VERDE as N_TELEFONO," _
                                                      & " (INDIRIZZI.DESCRIZIONE||' N°'||INDIRIZZI.CIVICO)  AS ""INDIRIZZO_FILIALE""" _
                                               & " from SISCOM_MI.EDIFICI," _
                                                    & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                                    & " SISCOM_MI.UNITA_IMMOBILIARI," _
                                                    & " SISCOM_MI.UNITA_CONTRATTUALE," _
                                                    & " SISCOM_MI.RAPPORTI_UTENZA," _
                                                    & " SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI, " _
                                                    & " SISCOM_MI.FILIALI_UI " _
                                             & " where COMPLESSI_IMMOBILIARI.ID     =EDIFICI.ID_COMPLESSO " _
                                             & "   and EDIFICI.ID                   =UNITA_IMMOBILIARI.ID_EDIFICIO " _
                                             & "   and UNITA_IMMOBILIARI.ID         =UNITA_CONTRATTUALE.ID_UNITA " _
                                             & "   and RAPPORTI_UTENZA.ID           =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                             & "   and FILIALI_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
                                             & "   and FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID (+) AND FILIALI_UI.INIZIO_VALIDITA <= '" & Format(Now, "yyyyMMdd") & "' AND FILIALI_UI.FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "' " _
                                             & "   and TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID (+) " _
                                             & "   and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE is null " _
                                             & "   and COD_CONTRATTO='" & CodiceContratto & "'"


                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                'Da RAPPORTI_UTENZA
                                Me.idcontratto.Value = par.IfNull(myReaderA("ID"), "-1")

                                presso_cor = par.IfNull(myReaderA("presso_cor"), "")
                                luogo_cor = par.IfNull(myReaderA("luogo_cor"), "")
                                civico_cor = par.IfNull(myReaderA("civico_cor"), "")
                                cap_cor = par.IfNull(myReaderA("cap_cor"), "")
                                indirizzo_cor = par.IfNull(myReaderA("VIA_cor"), "")
                                tipo_cor = par.IfNull(myReaderA("tipo_cor"), "")
                                sigla_cor = par.IfNull(myReaderA("sigla_cor"), "")

                                'Da UNITA_CONTRATTUALE
                                Me.idunita.Value = par.IfNull(myReaderA("ID_UNITA"), "-1")
                                sSCALA = par.IfNull(myReaderA("SCALA"), "")
                                sINTERNO = par.IfNull(myReaderA("INTERNO"), "")

                                idedificio = par.IfNull(myReaderA("idedificio"), "0")
                                idcomplesso = par.IfNull(myReaderA("idcomplesso"), "0")


                                If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
                                    TipoStampa = "Ingiunzione_C.htm"
                                Else
                                    TipoStampa = "Ingiunzione_D.htm"
                                End If
                                NoteBollette = "MOROSITA' MA" 'MA” (M.AV.) per quello relativo alla morosità dall’1/10/2009


                                Dim sr1 As StreamReader = New StreamReader(Server.MapPath(TipoStampa), System.Text.Encoding.GetEncoding("iso-8859-1"))
                                Dim contenutoOriginale As String = sr1.ReadToEnd()
                                sr1.Close()
                                Contenuto = contenutoOriginale

                                'INFORMAZIONI SULLA FILIALE
                                sNomeFiliale = par.IfNull(myReaderA("NOME_FILIALE"), "")
                                'sPosteAlerAcronimo = "CORE" 'par.IfNull(myReaderA("ACRONIMO"), "")
                                sNumTel_Filiale = par.IfNull(myReaderA("N_TELEFONO"), "")
                                sIndirizzo_Filiale = par.IfNull(myReaderA("INDIRIZZO_FILIALE"), "")
                                '****************************

                            End If
                            myReaderA.Close()


                            par.cmd.CommandText = "select acronimo from siscom_mi.tab_filiali where id =" & idStruttura
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                sPosteAlerAcronimo = par.IfNull(myReaderA("acronimo"), "----")
                            Else
                                sPosteAlerAcronimo = "----"
                            End If
                            myReaderA.Close()

                            'TIPO INVIO
                            Select Case par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")
                                Case "Racc. A.R"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
                                Case "Racc. mano"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A MANO")
                                Case "FAX"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "FAX.")
                                Case Else
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
                            End Select
                            '************************************

                            Dim Titolo As String = ""
                            Dim Nome As String = ""
                            Dim Cognome As String = ""
                            Dim CF As String = ""

                            Dim ID_BOLLETTA As Long = 0

                            'ANAGRAFICA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA where ID=" & IdAnagrafica
                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then

                                sPosteAlerCodUtente = Format(CDbl(par.IfNull(myReaderA("ID"), 0)), "000000000000")
                                If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                                    Titolo = "Spettabile"
                                    'Titolo = "Spettabile Ditta"
                                    Cognome = par.IfNull(myReaderA("ragione_sociale"), "")
                                    Nome = ""
                                    CF = par.IfNull(myReaderA("partita_iva"), "")

                                Else
                                    Titolo = "Spettabile"
                                    'If par.IfNull(myReaderA("sesso"), "") = "M" Then
                                    '    Titolo = "Egregio Signore"
                                    'Else
                                    '    Titolo = "Gentile Signora"
                                    'End If
                                    Cognome = par.IfNull(myReaderA("cognome"), "")
                                    Nome = par.IfNull(myReaderA("nome"), "")
                                    CF = par.IfNull(myReaderA("cod_fiscale"), "")
                                End If
                            End If
                            myReaderA.Close()
                            '*********************************************
                            par.cmd.CommandText = "select valore from sepa.parameter where id = 120"
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                sPosteAlerAcronimo = par.IfNull(myReaderA(0), "BA0301/NUMI")
                            Else
                                sPosteAlerAcronimo = "BA0301/NUMI"
                            End If
                            myReaderA.Close()
                            Contenuto = Replace(Contenuto, "$data$", par.FormattaData(par.IfNull(myReader("DATA_PROTOCOLLO"), "")))
                            Contenuto = Replace(Contenuto, "$protocollo$", "" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))
                            'Contenuto = Replace(Contenuto, "$protocollo$", "GL0000/" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))

                            Contenuto = Replace(Contenuto, "$codcontratto$", CodiceContratto)
                            Contenuto = Replace(Contenuto, "$codUI$", Strings.Left(CodiceContratto, Strings.Len(CodiceContratto) - 2))

                            If UCase(Trim(Trim(Cognome) & " " & Trim(Nome))) <> UCase(Trim(presso_cor)) Then
                                Contenuto = Replace(Contenuto, "$nominativo$", Cognome & " " & Nome & "<br />c/o " & presso_cor)
                            Else
                                Contenuto = Replace(Contenuto, "$nominativo$", presso_cor)
                            End If
                            sPosteAlerNominativo = Cognome & " " & Nome 'POSTE 

                            If sINTERNO <> "" Then
                                If sSCALA <> "" Then
                                    Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO & " SCALA " & sSCALA)
                                Else
                                    Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO)
                                End If
                            ElseIf sSCALA <> "" Then
                                Contenuto = Replace(Contenuto, "$indirizzo2$", "SCALA " & sSCALA)
                            End If

                            sPosteAlerInterno = sINTERNO    'POSTE 

                            For i = 1 To Strings.Len(sSCALA)
                                If Char.IsDigit(Strings.Mid(sSCALA, i, 1)) = False Then
                                    sPosteAlerScala = Strings.Mid(sSCALA, i, Strings.Len(sSCALA))  'POSTE 
                                    Exit For
                                End If
                            Next i

                            Contenuto = Replace(Contenuto, "$indirizzo0$", tipo_cor & " " & indirizzo_cor & ", " & civico_cor)
                            Contenuto = Replace(Contenuto, "$indirizzo1$", cap_cor & " " & luogo_cor & " " & sigla_cor)
                            'Contenuto = Replace(Contenuto, "$indirizzo$", indirizzo_cor & ", " & civico_cor & "</br>" & cap_cor & " " & luogo_cor & " " & sigla_cor)

                            Contenuto = Replace(Contenuto, "$titolo$", Titolo & " " & Cognome & " " & Nome)

                            sPosteAlerInd = tipo_cor & " " & indirizzo_cor & " " & civico_cor 'POSTE 
                            sPosteAlerCAP = cap_cor                          'POSTE 
                            sPosteAlerLocalita = luogo_cor                   'POSTE 
                            sPosteAlerProv = sigla_cor                       'POSTE 


                            'INFORMAZIONI FILIALE
                            Contenuto = Replace(Contenuto, "$NomeFiliale$", sNomeFiliale)
                            Contenuto = Replace(Contenuto, "$IndirizzoFiliale$", sIndirizzo_Filiale)

                            par.cmd.CommandText = "select siscom_mi.getstatocontratto(" & idcontratto.Value & ") from dual"
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                If par.IfNull(myReaderA(0), "") = "CHIUSO" Then
                                    Contenuto = Replace(Contenuto, "$chiuso$", ".")
                                    sNumTel_Filiale = "Nel caso Lei desideri ricevere informazioni anche in merito alla possibilità di rateizzare " _
                                                    & "il debito maturato, al fine di evitarLe inutili attese, potrà contattare telefonicamente " _
                                                    & "il Settore Recupero Morosità presso la sede di viale Romagna n.26 al numero 02/73922319 per fissare un appuntamento."

                                Else
                                    Contenuto = Replace(Contenuto, "$chiuso$", " fino, se necessario, all’esecuzione dello sfratto per morosità.")
                                    sNumTel_Filiale = "Nel caso Lei desideri ricevere informazioni anche in merito alla possibilità di rateizzare " _
                                                    & "il debito maturato, al fine di evitarLe inutili attese, potrà contattare telefonicamente " _
                                                    & "la filiale al seguente numero " & sNumTel_Filiale & " per fissare un appuntamento. "
                                End If
                            Else
                                Contenuto = Replace(Contenuto, "$chiuso$", " fino, se necessario, all’esecuzione dello sfratto per morosità.")

                            End If
                            myReaderA.Close()
                            Contenuto = Replace(Contenuto, "$TelFiliale$", sNumTel_Filiale)

                            'Da MOROSITA_LETTERE
                            'NOTA: prima del 02/Sett/2001 era di 40 gg ora è di 60 + 20 + controllo se capita di sabato o domenica
                            '08/06/2012 su richiesta dell'avvocato RUMORE si riporta la data scadenza a 40gg dalla data di emissione
                            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            ' 12/09/2014 ora scadenzabollettino è definita nella maschera della creazione della morosita
                            ScadenzaBollettino = Request.QueryString("SCADBOL")

                            Dim d1 As Date
                            d1 = New Date(Mid(ScadenzaBollettino, 1, 4), Mid(ScadenzaBollettino, 5, 2), Mid(ScadenzaBollettino, 7, 2))
                            If d1.DayOfWeek = DayOfWeek.Saturday Then
                                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 42, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            ElseIf d1.DayOfWeek = DayOfWeek.Sunday Then
                                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 41, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            End If
                            '************

                            periodo = par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))

                            If PeriodoXLS_INIZIO > par.IfNull(myReader("inizio_periodo"), 0) Or PeriodoXLS_INIZIO = 0 Then
                                PeriodoXLS_INIZIO = par.IfNull(myReader("inizio_periodo"), 0)
                            End If

                            If PeriodoXLS_FINE < par.IfNull(myReader("fine_periodo"), 0) Or PeriodoXLS_FINE = 0 Then
                                PeriodoXLS_FINE = par.IfNull(myReader("fine_periodo"), 0)
                            End If

                            Contenuto = Replace(Contenuto, "$spazi$", "")
                            Contenuto = Replace(Contenuto, "$spazi1$", "")


                            'Contenuto = Replace(Contenuto, "$tipo$", TipoIngiunzione)
                            If TipoStampa = "Ingiunzione_Aa.htm" Or TipoStampa = "Ingiunzione_Bb.htm" Then

                                ValImporto1 = par.IfNull(myReader("Importo"), 0)

                                If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                                    par.cmd.Parameters.Clear()
                                    par.cmd.CommandText = "select IMPORTO" _
                                                       & " from SISCOM_MI.MOROSITA_LETTERE " _
                                                       & " where ID_MOROSITA=" & IndiceMorosita _
                                                       & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                       & "   and ID_ANAGRAFICA=" & IdAnagrafica _
                                                       & "   and BOLLETTINO is NULL " _
                                                       & "   and NUM_LETTERE=2"

                                    myReaderA = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then
                                        ValImporto2 = par.IfNull(myReaderA("Importo"), 0)
                                    End If
                                    myReaderA.Close()

                                    Contenuto = Replace(Contenuto, "$importo1$", Format(ValImporto1, "##,##0.00"))
                                    Contenuto = Replace(Contenuto, "$importo2$", Format(ValImporto2, "##,##0.00"))

                                    ValImporto1 = ValImporto1 + ValImporto2
                                    Contenuto = Replace(Contenuto, "$importoTOT$", Format(ValImporto1, "##,##0.00"))

                                End If

                            Else
                                Contenuto = Replace(Contenuto, "$importoTOT$", Format(CDbl(Importo), "##,##0.00"))
                            End If


                            If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then

                                'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

                                sr.WriteLine(Contenuto)
                                sr.Close()

                                'Dim url As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto
                                url = Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto
                                pdfConverter1 = New PdfConverter

                                Licenza = Session.Item("LicenzaHtmlToPdf")
                                If Licenza <> "" Then
                                    pdfConverter1.LicenseKey = Licenza
                                End If

                                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                                pdfConverter1.PdfDocumentOptions.LeftMargin = 40
                                pdfConverter1.PdfDocumentOptions.RightMargin = 40
                                pdfConverter1.PdfDocumentOptions.TopMargin = 17
                                pdfConverter1.PdfDocumentOptions.BottomMargin = 30
                                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                                pdfConverter1.PdfFooterOptions.FooterText = ("")
                                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
                                pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")

                                'If sPosteAler <> "" Then
                                '    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
                                'Else
                                '    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
                                'End If

                                ''****************EVENTI CONTRATTI UNA PER CONTRATTO***************
                                Dim sStr1 As String = "Inviato il MAV MA e la lettera di messa in mora con " & par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")

                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_CONTRATTI " _
                                                          & " (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                    & "values (:id_contratto,:id_operatore,:data,:cod_evento,:motivo)"

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "F176"))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                '************************************************

                                'INIZIO CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 1° PARTE #############################################
                                sNomeFileMorLettera = "MorositaLettera_" & idMorositaLettere & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

                                Dim LicenzaF As String = Session.Item("LicenzaPdfMerge")
                                If LicenzaF <> "" Then
                                    pdfMergeF.LicenseKey = LicenzaF
                                End If
                                'FINE CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 1° PARTE ###############################################


                            Else
                                NoteBollette = "MOROSITA' MA" 'MA” (M.AV.) per quello relativo alla morosità dall’1/10/2009
                            End If


                            ' '' Ricavo ID di POSTALER per PostAler.txt (2)

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                sPosteAlerIA = myReaderA(0)
                            End If
                            myReaderA.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                                              & " values (" & sPosteAlerIA & "," & idMorositaLettere & ",1)"
                            par.cmd.ExecuteNonQuery()
                            '******************************************************************

                            If sPosteAler <> "" Then
                                sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                            Else
                                sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                            End If



                            'Dim SpNotifica As Double = 0
                            'If Importo + spese_notifica + SPESEmav >= APPLICABOLLO Then
                            '    Contenuto = Replace(Contenuto, "$importo1$", Format(CDbl(Importo + spese_notifica + SPESEmav + BOLLO), "##,##0.00"))
                            '    SpNotifica = CDbl(spese_notifica + SPESEmav + BOLLO)
                            'Else
                            '    Contenuto = Replace(Contenuto, "$importo1$", Format(CDbl(Importo + spese_notifica + SPESEmav), "##,##0.00"))
                            '    SpNotifica = CDbl(spese_notifica + SPESEmav)
                            'End If

                            '**********peppe modify 21/10/2010***********
                            'Contenuto = Replace(Contenuto, "$importospnotifica$", Format(SpNotifica, "##,##0.00"))

                            'Contenuto = Replace(Contenuto, "$responsabile$", RESPONSABILE)
                            'Contenuto = Replace(Contenuto, "$dirigente$", DIRIGENTE)
                            'Contenuto = Replace(Contenuto, "$trattatada$", TRATTATADA)


                            'Contenuto = Replace(Contenuto, "$trattatada$", Operatore)
                            'Contenuto = Replace(Contenuto, "$periodo$", par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")))
                            'Contenuto = Replace(Contenuto, "$titolo$", par.FormattaData(par.IfNull(myReader("emissione"), "")))
                            'Contenuto = Replace(Contenuto, "$emissione$", par.FormattaData(par.IfNull(myReader("emissione"), "")))
                            'Contenuto = Replace(Contenuto, "$scadenzabollettino$", par.FormattaData(ScadenzaBollettino))



                            Dim Nome1 As String = ""
                            Dim Nome2 As String = ""

                            If UCase(Trim(Trim(Cognome) & " " & Trim(Nome))) <> UCase(Trim(presso_cor)) Then
                                Nome1 = Cognome & " " & Nome
                                Nome2 = presso_cor
                            Else
                                Nome1 = presso_cor
                            End If

                            ' INSERT BOL_BOLLETTE
                            'par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                            '                            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                            '                            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                            '                            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                            '                            & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                            '                            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                            '                            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA) " _
                            '                   & " values " _
                            '                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999 , '" & Format(Now, "yyyyMMdd") & "','" _
                            '                        & ScadenzaBollettino & "', NULL,NULL,NULL,'RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "'," _
                            '                        & idcontratto.Value & " ," & par.RicavaEsercizioCorrente & "," & idunita.Value & "," _
                            '                        & "'0', ''," & IdAnagrafica & "," _
                            '                        & "'" & par.PulisciStrSql(Nome1) & "'," _
                            '                        & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) & "'," _
                            '                        & "'" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") & "'," _
                            '                        & "'" & par.PulisciStrSql(Nome2) & "', '" & par.IfNull(myReader("inizio_periodo"), "") & "'," _
                            '                        & "'" & par.IfNull(myReader("fine_periodo"), "") & "'," _
                            '                        & "'0', " & idcomplesso & ", '', NULL, '', " _
                            '                        & Year(Now) & ", '', " & idedificio & ", NULL, NULL,'MOR'," & IndiceMorosita & ")"
                            'par.cmd.ExecuteNonQuery()


                            ' '' Ricavo ID_BOLLETTA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM dual "
                            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderB.Read Then
                                ID_BOLLETTA = myReaderB(0)
                            End If
                            myReaderB.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                                                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, " _
                                                        & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA,ID_TIPO) " _
                                                & "values (:id,:n_rata,:data_emissione,:data_scadenza," _
                                                        & ":note,:id_contratto,:id_esercizio_f," _
                                                        & ":id_unita,:fl_annullata,:pagabile_presso,:cod_affittuario,:intestatario," _
                                                        & ":indirizzo,:cap_citta,:presso,:riferimento_da,:riferimento_a," _
                                                        & ":fl_stampato,:id_complesso,:data_ins_pagamento,:importo_pagato,:note_pagamento," _
                                                        & ":anno,:operatore_pag,:id_edificio,:data_annullo_pag,:operatore_annullo_pag,:rif_file,:id_morosita,4)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", ID_BOLLETTA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_rata", 999))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", ScadenzaBollettino))

                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_1_sollecito", "NULL"))
                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_2_sollecito", "NULL"))
                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_pagamento", "NULL"))

                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", NoteBollette))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_esercizio_f", RitornaNullSeIntegerMenoUno(par.RicavaEsercizioCorrente)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idunita.Value))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_annullata", "0"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pagabile_presso", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_affittuario", RitornaNullSeIntegerMenoUno(IdAnagrafica)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("intestatario", Strings.Left(Nome1, 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", Strings.Left(tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor), 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap_citta", Strings.Left(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")", 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("presso", Strings.Left(Nome2, 100)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_da", par.IfNull(myReader("inizio_periodo"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_a", par.IfNull(myReader("fine_periodo"), "")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_stampato", "0"))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(idcomplesso)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ins_pagamento", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_pagato", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note_pagamento", ""))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Year(Now)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_pag", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(idedificio)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_annullo_pag", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_annullo_pag", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_file", "MOR"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", IndiceMorosita))


                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()




                            'par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                            'Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            'If myReaderB.Read Then
                            '    ID_BOLLETTA = myReaderB(0)
                            'Else
                            '    ID_BOLLETTA = -1
                            'End If
                            'myReaderB.Close()

                            ' INSERT tutte le sotto voci di BOL_BOLLETTE_VOCI
                            'RECUPERO MOROSITA' (150)
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                & " values " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                        & ID_BOLLETTA & "," _
                                                        & VOCE & "," _
                                                        & par.VirgoleInPunti(Importo) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + Importo

                            If NoteBollette = "MOROSITA' MA" Then
                                'SPESE DI NOTIFICA (628)
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                   & " values " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                        & ID_BOLLETTA _
                                                        & ",628," _
                                                        & par.VirgoleInPunti(spese_notifica) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + spese_notifica
                            End If

                            'SPESE MAV
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                               & " values " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                    & ID_BOLLETTA _
                                                    & ",407," _
                                                    & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + SPESEmav

                            'BOLLO
                            If Tot_Bolletta >= APPLICABOLLO Then
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                   & " values " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                            & ID_BOLLETTA _
                                                            & ",95," _
                                                            & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + BOLLO
                            End If
                            '******************************************************

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

                            RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA, CodiceContratto)

                            RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                            RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                            RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                            'RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                            RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)

                            '30/04/2012 Elimino controllo del vuoto perchè necessario azzerare NOMEDEBITORE del metodo RichiestaEmissioneMAV
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

                            Try
                                '12/01/2015 PUCCIA Nuova connessione  tls ssl
                                If DisabilitaExpect100Continue = "1" Then
                                    System.Net.ServicePointManager.Expect100Continue = False
                                End If

                                par.cmd.Parameters.Clear()
                                '/*/*/*/*/*tls v1
                                Dim v As String = ""
                                par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                                v = par.cmd.ExecuteScalar
                                System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                                '/*/*/*/*/*tls v1


                                System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler
                                

                                Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                            Catch ex As Exception

                                par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'  where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                par.myTrans.Commit()

                                If RiControllaMAVCreati() > 0 Then
                                    Response.Write("<SCRIPT>alert('Attenzione...Alcuni MAV non sono stati creati! \n Rientrare nella singola scheda dell\'inquilino è rigenerare il MAV tramite il tasto PROCEDI!');</SCRIPT>")
                                End If

                                par.cmd.Dispose()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                                Session.Item("LAVORAZIONE") = "0"
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>" & ex.Message & " !</p>")
                                Exit Sub
                            End Try

                            If Esito.codiceRisultato = "0" Then
                                'If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                                '    pdfMerge.AppendPDFFile(url & ".pdf")
                                '    IO.File.Delete(url & ".htm")
                                'End If

                                'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".pdf"

                                binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length - 1)
                                outFile.Close()
                                ''sposto sotto per inversione
                                'pdfMerge.AppendPDFFile(outputFileName)
                                'pdfMergeF.AppendPDFFile(outputFileName)


                                ' se la banca emette correttamente i MAV allora:
                                ' SETTO a BOL_BOLLETTE che è stato stampato e il numero di bollettino
                                num_bollettino = Esito.numeroMAV
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                   & " set   FL_STAMPATO='1'," _
                                                   & "       rif_bollettino='" & num_bollettino & "'" _
                                                   & " where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                'Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 9pt;'><td style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & CodiceContratto & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & tipo_cor & " " & indirizzo_cor & ", " & civico_cor & " " & cap_cor & " " & luogo_cor & " " & sigla_cor & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Cognome & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Nome & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("emissione"), "")) & " </td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(ScadenzaBollettino) & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & num_bollettino & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Importo), "##,##0.00") & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Tot_Bolletta - Importo), "##,##0.00") & "</td></tr>"
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(Titolo))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(Cognome & " " & Nome))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(tipo_cor & " " & indirizzo_cor & ", " & civico_cor))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(cap_cor & " " & luogo_cor & " " & sigla_cor))
                                K = K + 1


                                'SOLO x TEST , per la produxione toglie il commento sopra e rimettrerlo sotto e togliere MOROSITA_EVENTI
                                'INIZIO TEST
                                'par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
                                '                  & "     set BOLLETTINO='" & num_bollettino & "'," _
                                '                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                '                  & "         COD_STATO='M04'" _
                                '                  & " where ID=" & idMorositaLettere

                                'par.cmd.ExecuteNonQuery()

                                'par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                '                       & "  (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                '                       & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL," & idMorositaLettere & "," _
                                '                                & Session.Item("ID_OPERATORE") & "," & Format(Now, "yyyyMMddHHmmss") & "," _
                                '                                & "'M04','Ricevuta PostAler: RITIRATA DAL DESTINATARIO')"

                                'par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                '************ FINE TEST


                                'UPDATE BOL_BOLLETTE VECCHIE raggruppate per la BOLLETTA MOROSITA' NUOVA
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_BOLLETTA_RIC=" & ID_BOLLETTA _
                                                    & " where ID_MOROSITA_LETTERA=" & idMorositaLettere _
                                                    & "   and ID_MOROSITA=" & IndiceMorosita _
                                                    & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()
                                '************************************

                                'prima del 14/02/2012 stava in crealettere
                                'UPDATE BOL_BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                                   & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                                                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                        & "  where ID_MOROSITA_LETTERA=" & idMorositaLettere _
                                                                        & "    and ID_MOROSITA=" & IndiceMorosita _
                                                                        & "    and FL_ANNULLATA=0 " _
                                                                        & "    and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) & ") " _
                                                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()


                                'IMPORTO_CANONE    
                                ValSommaImportoCanone = 0
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                                  & "  from SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA " _
                                                  & "  where BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID " _
                                                  & "    and T_VOCI_BOLLETTA.GRUPPO=4 " _
                                                  & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                         & " where ID_MOROSITA=" & IndiceMorosita _
                                                                         & "   and FL_ANNULLATA=0 " _
                                                                         & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                                         & "   and ID>0 " _
                                                                         & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                                myReaderB = par.cmd.ExecuteReader()
                                If myReaderB.Read Then
                                    ValSommaImportoCanone = par.IfNull(myReaderB(0), 0)
                                End If
                                myReaderB.Close()
                                '***********************************

                                'IMPORTO_ONERI    
                                ValSommaImportoOneri = 0
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                                  & "  from SISCOM_MI.BOL_BOLLETTE_VOCI" _
                                                  & "  where ID_VOCE NOT IN (select ID from SISCOM_MI.T_VOCI_BOLLETTA where GRUPPO=4) " _
                                                  & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                         & " where ID_MOROSITA=" & IndiceMorosita _
                                                                         & "   and FL_ANNULLATA=0 " _
                                                                         & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                                         & "   and ID>0 " _
                                                                         & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                                myReaderB = par.cmd.ExecuteReader()
                                If myReaderB.Read Then
                                    ValSommaImportoOneri = par.IfNull(myReaderB(0), 0)
                                End If
                                myReaderB.Close()
                                '***********************************

                                'UPDATE MOROSITA_LETTERE
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
                                                  & "     set BOLLETTINO='" & num_bollettino & "'," _
                                                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                                  & "         COD_STATO='M00'," _
                                                  & "         IMPORTO_BOLLETTA=" & par.VirgoleInPunti(Tot_Bolletta) & "," _
                                                  & "         IMPORTO_CANONE= " & par.VirgoleInPunti(ValSommaImportoCanone) & "," _
                                                  & "         IMPORTO_ONERI= " & par.VirgoleInPunti(ValSommaImportoOneri) _
                                                  & " where ID=" & idMorositaLettere

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()


                                ' Response.Redirect("ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf")

                            Else
                                'lblErrore.Visible = True

                                ' se la banca NON emette correttamente i MAV allora:
                                ' ELIMINO  BOL_BOLLETTE 

                                par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'  where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                'Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

                                'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
                                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".xml"

                                binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length)
                                outFile.Close()
                            End If


                            'Else
                            '    If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                            '        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("ragione_sociale"), "") & " non sono stati stampati perchè la partita iva non ha un formato corretto!</p>")
                            '    Else
                            '        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & " non sono stati stampati perchè il codice fiscale non ha un formato corretto!</p>")
                            '    End If
                            'End If

                            'AGGIUNGO LA LETTERA
                            If par.IfNull(myReader("NUM_LETTERE"), 1) = 2 Or TipoStampa = "Ingiunzione_C.htm" Or TipoStampa = "Ingiunzione_D.htm" Then
                                pdfMerge.AppendPDFFile(url & ".pdf")
                                pdfMergeF.AppendPDFFile(url & ".pdf")
                                'sposto qui sotto per inversione ordine lettera mav
                                pdfMerge.AppendPDFFile(outputFileName)
                                pdfMergeF.AppendPDFFile(outputFileName)

                                IO.File.Delete(url & ".htm")

                                'Per la ogni MOROSITA_LETTERA creo un File ZIP con il/i MAV e la Lettera
                                'INIZIO CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 2° PARTE #############################################
                                pdfMergeF.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & sNomeFileMorLettera)
                                Dim zipficF As String
                                Dim objCrc32F As New Crc32()
                                Dim strmZipOutputStreamF As ZipOutputStream
                                Dim strFileF As String
                                Dim strmFileF As FileStream

                                zipficF = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & Strings.Left(sNomeFileMorLettera, Strings.Len(sNomeFileMorLettera) - 4) & ".zip")

                                strmZipOutputStreamF = New ZipOutputStream(File.Create(zipficF))
                                strmZipOutputStreamF.SetLevel(6)

                                strFileF = Server.MapPath("..\FileTemp\") & sNomeFileMorLettera
                                strmFileF = File.OpenRead(strFileF)
                                Dim abyBufferF(Convert.ToInt32(strmFileF.Length - 1)) As Byte
                                strmFileF.Read(abyBufferF, 0, abyBufferF.Length)

                                Dim sFile1F As String = Path.GetFileName(strFileF)
                                Dim theEntryF As ZipEntry = New ZipEntry(sFile1F)
                                Dim fiF As New FileInfo(strFileF)

                                theEntryF.DateTime = fiF.LastWriteTime
                                theEntryF.Size = strmFileF.Length
                                strmFileF.Close()
                                objCrc32F.Reset()
                                objCrc32F.Update(abyBufferF)
                                theEntryF.Crc = objCrc32F.Value
                                strmZipOutputStreamF.PutNextEntry(theEntryF)
                                strmZipOutputStreamF.Write(abyBufferF, 0, abyBufferF.Length)
                                File.Delete(strFileF)

                                strmZipOutputStreamF.Finish()
                                strmZipOutputStreamF.Close()
                                '***** FINE CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 2° PARTE ########################################

                            End If


                        Loop
                        myReader.Close()
                        .CloseFile()
                    End With

                    'Riassunto = Riassunto & "</table>"
                    contenutoRiassunto = Replace(contenutoRiassunto, "$riassunto$", Riassunto)
                    contenutoRiassunto = Replace(contenutoRiassunto, "$periodo$", par.FormattaData(PeriodoXLS_INIZIO) & " - " & par.FormattaData(PeriodoXLS_FINE))

                    PeriodoXLS_INIZIO = 0
                    PeriodoXLS_FINE = 0

                    'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & "-C_D" & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

                    ''sr3.WriteLine(contenutoRiassunto)
                    'sr3.Close()


                    'Dim url1 As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita
                    'Dim url1 As String = Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & "-C_D"

                    'Dim pdfConverter As PdfConverter = New PdfConverter

                    'Licenza = Session.Item("LicenzaHtmlToPdf")
                    'If Licenza <> "" Then
                    '    pdfConverter.LicenseKey = Licenza
                    'End If


                    'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                    'pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
                    'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfDocumentOptions.ShowFooter = False
                    'pdfConverter.PdfDocumentOptions.LeftMargin = 30
                    'pdfConverter.PdfDocumentOptions.RightMargin = 30
                    'pdfConverter.PdfDocumentOptions.TopMargin = 17
                    'pdfConverter.PdfDocumentOptions.BottomMargin = 30
                    'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfFooterOptions.FooterText = ("")
                    'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                    'pdfConverter.PdfFooterOptions.DrawFooterLine = False
                    'pdfConverter.PdfFooterOptions.PageNumberText = ""
                    'pdfConverter.PdfFooterOptions.ShowPageNumber = False
                    'pdfConverter.SavePdfFromUrlToFile(url1 & ".htm", url1 & ".pdf")

                    ''AGGIUNGO LA LETTERA
                    'pdfMerge.AppendPDFFile(url & ".pdf")
                    'IO.File.Delete(url & ".htm")

                    'AGGIUNGO L'elenco
                    'pdfMerge.AppendPDFFile(url1 & ".pdf")
                    'IO.File.Delete(url1 & ".htm")


                    'COMMIT
                    par.myTrans.Commit()
                    par.cmd.Dispose()


                    'pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx)
                    pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & xx)

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String
                    Dim strFile As String
                    Dim strmFile As FileStream

                    'zipfic = Server.MapPath("Varie\" & sNomeFile & ".zip")
                    zipfic = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".zip")

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)

                    'scrivo file XLS
                    'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".xls")
                    strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
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

                    'scrivo file PDF
                    'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx
                    strFile = Server.MapPath("..\FileTemp\") & xx
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

                    'Scrivo FILE TXT POSTE  *******************************
                    Using sw As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt"))
                        sw.Write(sPosteAler)
                        sw.Close()
                    End Using

                    strFile = Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer2(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer2, 0, abyBuffer2.Length)

                    Dim sFile2 As String = Path.GetFileName(strFile)
                    theEntry = New ZipEntry(sFile2)
                    fi = New FileInfo(strFile)

                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer2)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer2, 0, abyBuffer2.Length)
                    File.Delete(strFile)
                    '******************************************

                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                End If
                'FINE C D **********************************************************************************************************
                '         **********************************************************************************************************


                'INIZIO E F **********************************************************************************************************
                '           **********************************************************************************************************
                If TrovatoEF = True Then

                    sPosteAler = ""
                    ' PRIMA VOLTA CHE VIENE ESEGUITA LA MOROSITA'

                    'CREO LA TRANSAZIONE
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans


                    'Riassunto = "<table style='width:100%;'>"
                    'Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 10pt; font-weight: bold'><td>COD.CONTRATTO</td>" _
                    '                                                                                         & "<td>INDIRIZZO</td>" _
                    '                                                                                         & "<td>COGN./RAG.SOCIALE</td>" _
                    '                                                                                         & "<td>NOME</td>" _
                    '                                                                                         & "<td>PERIODO DI RIF.</td>" _
                    '                                                                                         & "<td>EMISSIONE</td>" _
                    '                                                                                         & "<td>SCADENZA</td>" _
                    '                                                                                         & "<td>N.BOLLETTINO</td>" _
                    '                                                                                         & "<td>IMPORTO</td>" _
                    '                                                                                         & "<td>SPESE</td></tr>"
                    'Riassunto = Riassunto & "<tr><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td><td>&nbsp;&nbsp;</td></tr>"


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

                    Dim pdfMergeF As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)    'x FILE ZIP singolo

                    Dim fileNamePosteAler As String = "PosteAler_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss") & "-E_F"

                    Dim xx As String = "Morosita_" & IndiceMorosita & "-" & Format(Now, "yyyyMMddHHmmss") & "-E_F"
                    sNomeFile = xx
                    xx = xx & ".pdf"


                    'Dim sr2 As StreamReader = New StreamReader(Server.MapPath("Elenco.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                    'contenutoRiassunto = sr2.ReadToEnd()
                    'sr2.Close()


                    Dim K As Integer = 2
                    'inizio a scrivere il file xls
                    With myExcelFile

                        '.CreateFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & sNomeFile & ".xls")
                        .CreateFile(Server.MapPath("..\FileTemp\") & sNomeFile & ".xls")

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

                        .SetColumnWidth(1, 4, 30)

                        Dim Contenuto As String = ""
                        'Dim url As String
                        Dim pdfConverter1 As PdfConverter

                        Dim FlagStampa As Boolean = False


                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "select MOROSITA.TIPO_INVIO,MOROSITA.DATA_PROTOCOLLO,MOROSITA.PROTOCOLLO_ALER," _
                                                  & " MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE,MOROSITA_LETTERE.COD_CONTRATTO,MOROSITA_LETTERE.Importo, MOROSITA_LETTERE.ID_ANAGRAFICA, MOROSITA_LETTERE.EMISSIONE, MOROSITA_LETTERE.INIZIO_PERIODO, MOROSITA_LETTERE.FINE_PERIODO, MOROSITA_LETTERE.NUM_LETTERE," _
                                                  & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA " _
                                           & " from  SISCOM_MI.MOROSITA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.MOROSITA_LETTERE " _
                                           & " where MOROSITA.ID=" & IndiceMorosita _
                                           & "   and MOROSITA_LETTERE.ID_ANAGRAFICA=ANAGRAFICA.ID " _
                                           & "   and MOROSITA.ID                   =MOROSITA_LETTERE.ID_MOROSITA " _
                                           & "   and MOROSITA_LETTERE.BOLLETTINO is NULL " _
                                           & "   and MOROSITA_LETTERE.TIPO_LETTERA='EF' " _
                                           & " order by MOROSITA_LETTERE.ID_ANAGRAFICA,MOROSITA_LETTERE.ID_CONTRATTO,MOROSITA_LETTERE.NUM_LETTERE "

                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader.Read
                            'LOOP di tutte le lettere di MOROSITA


                            'If Len(par.IfNull(myReader("PARTITA_IVA"), 0)) = 11 Or Len(par.IfNull(myReader("COD_FISCALE"), 0)) = 16 Then
                            idMorositaLettere = par.IfNull(myReader("ID_MOROSITA_LETTERE"), 0)

                            Tot_Bolletta = 0

                            CodiceContratto = par.IfNull(myReader("COD_CONTRATTO"), "")
                            TipoIngiunzione = "RECUPERO MOROSITA'"
                            VOCE = "150" '"626"
                            Importo = par.IfNull(myReader("Importo"), "0,00")
                            IdAnagrafica = par.IfNull(myReader("id_anagrafica"), "")

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "select COMPLESSI_IMMOBILIARI.ID as idcomplesso,EDIFICI.id as idedificio," _
                                                      & " RAPPORTI_UTENZA.*,UNITA_CONTRATTUALE.ID_UNITA,UNITA_CONTRATTUALE.SCALA,UNITA_CONTRATTUALE.INTERNO," _
                                                      & " TAB_FILIALI.NOME as ""NOME_FILIALE"",TAB_FILIALI.ACRONIMO,TAB_FILIALI.N_TELEFONO_VERDE AS N_TELEFONO," _
                                                      & " (INDIRIZZI.DESCRIZIONE||' N°'||INDIRIZZI.CIVICO)  AS ""INDIRIZZO_FILIALE"" " _
                                               & " from SISCOM_MI.EDIFICI," _
                                                    & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                                    & " SISCOM_MI.UNITA_IMMOBILIARI," _
                                                    & " SISCOM_MI.UNITA_CONTRATTUALE," _
                                                    & " SISCOM_MI.RAPPORTI_UTENZA," _
                                                    & " SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI, " _
                                                    & " SISCOM_MI.FILIALI_UI " _
                                             & " where COMPLESSI_IMMOBILIARI.ID     =EDIFICI.ID_COMPLESSO " _
                                             & "   and EDIFICI.ID                   =UNITA_IMMOBILIARI.ID_EDIFICIO " _
                                             & "   and UNITA_IMMOBILIARI.ID         =UNITA_CONTRATTUALE.ID_UNITA " _
                                             & "   and RAPPORTI_UTENZA.ID           =UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                             & "   and FILIALI_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
                                             & "   and FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID (+) AND FILIALI_UI.INIZIO_VALIDITA <= '" & Format(Now, "yyyyMMdd") & "' AND FILIALI_UI.FINE_VALIDITA >= '" & Format(Now, "yyyyMMdd") & "' " _
                                             & "   and TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID (+) " _
                                             & "   and UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE is null " _
                                             & "   and COD_CONTRATTO='" & CodiceContratto & "'"


                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                'Da RAPPORTI_UTENZA
                                Me.idcontratto.Value = par.IfNull(myReaderA("ID"), "-1")

                                presso_cor = par.IfNull(myReaderA("presso_cor"), "")
                                luogo_cor = par.IfNull(myReaderA("luogo_cor"), "")
                                civico_cor = par.IfNull(myReaderA("civico_cor"), "")
                                cap_cor = par.IfNull(myReaderA("cap_cor"), "")
                                indirizzo_cor = par.IfNull(myReaderA("VIA_cor"), "")
                                tipo_cor = par.IfNull(myReaderA("tipo_cor"), "")
                                sigla_cor = par.IfNull(myReaderA("sigla_cor"), "")

                                'Da UNITA_CONTRATTUALE
                                Me.idunita.Value = par.IfNull(myReaderA("ID_UNITA"), "-1")
                                sSCALA = par.IfNull(myReaderA("SCALA"), "")
                                sINTERNO = par.IfNull(myReaderA("INTERNO"), "")

                                idedificio = par.IfNull(myReaderA("idedificio"), "0")
                                idcomplesso = par.IfNull(myReaderA("idcomplesso"), "0")


                                If par.IfNull(myReaderA("COD_TIPOLOGIA_RAPP_CONTR"), "") <> "ILLEG" Then
                                    TipoStampa = "Ingiunzione_E.htm"
                                Else
                                    TipoStampa = "Ingiunzione_F.htm"
                                End If
                                NoteBollette = "MOROSITA' MG" 'MG (M.AV. Global Service) per quello relativo alla morosità fino al 30/9/2009 e basta


                                Dim sr1 As StreamReader = New StreamReader(Server.MapPath(TipoStampa), System.Text.Encoding.GetEncoding("iso-8859-1"))
                                Dim contenutoOriginale As String = sr1.ReadToEnd()
                                sr1.Close()
                                Contenuto = contenutoOriginale

                                'INFORMAZIONI SULLA FILIALE
                                sNomeFiliale = par.IfNull(myReaderA("NOME_FILIALE"), "")
                                'sPosteAlerAcronimo = "CORE" 'par.IfNull(myReaderA("ACRONIMO"), "")
                                sNumTel_Filiale = par.IfNull(myReaderA("N_TELEFONO"), "")
                                sIndirizzo_Filiale = par.IfNull(myReaderA("INDIRIZZO_FILIALE"), "")
                                '****************************

                            End If
                            myReaderA.Close()

                            par.cmd.CommandText = "select acronimo from siscom_mi.tab_filiali where id =" & idStruttura
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                sPosteAlerAcronimo = par.IfNull(myReaderA("acronimo"), "----")
                            Else
                                sPosteAlerAcronimo = "----"
                            End If
                            myReaderA.Close()

                            'TIPO INVIO
                            Select Case par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")
                                Case "Racc. A.R"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
                                Case "Racc. mano"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A MANO")
                                Case "FAX"
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "FAX.")
                                Case Else
                                    Contenuto = Replace(Contenuto, "$TIPO_INVIO$", "RACCOMANDATA A.R.")
                            End Select
                            '************************************

                            Dim Titolo As String = ""
                            Dim Nome As String = ""
                            Dim Cognome As String = ""
                            Dim CF As String = ""

                            Dim ID_BOLLETTA As Long = 0

                            'ANAGRAFICA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "select * from SISCOM_MI.ANAGRAFICA where ID=" & IdAnagrafica
                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then

                                sPosteAlerCodUtente = Format(CDbl(par.IfNull(myReaderA("ID"), 0)), "000000000000")
                                If par.IfNull(myReaderA("ragione_sociale"), "") <> "" Then
                                    Titolo = "Spettabile"
                                    'Titolo = "Spettabile Ditta"
                                    Cognome = par.IfNull(myReaderA("ragione_sociale"), "")
                                    Nome = ""
                                    CF = par.IfNull(myReaderA("partita_iva"), "")

                                Else
                                    Titolo = "Spettabile"
                                    'If par.IfNull(myReaderA("sesso"), "") = "M" Then
                                    '    Titolo = "Egregio Signore"
                                    'Else
                                    '    Titolo = "Gentile Signora"
                                    'End If
                                    Cognome = par.IfNull(myReaderA("cognome"), "")
                                    Nome = par.IfNull(myReaderA("nome"), "")
                                    CF = par.IfNull(myReaderA("cod_fiscale"), "")
                                End If
                            End If
                            myReaderA.Close()
                            '*********************************************
                            par.cmd.CommandText = "select valore from sepa.parameter where id = 120"
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                sPosteAlerAcronimo = par.IfNull(myReaderA(0), "BA0301/NUMI")
                            Else
                                sPosteAlerAcronimo = "BA0301/NUMI"
                            End If
                            myReaderA.Close()
                            Contenuto = Replace(Contenuto, "$data$", par.FormattaData(par.IfNull(myReader("DATA_PROTOCOLLO"), "")))
                            Contenuto = Replace(Contenuto, "$protocollo$", "" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))
                            'Contenuto = Replace(Contenuto, "$protocollo$", "GL0000/" & sPosteAlerAcronimo & "/" & par.IfNull(myReader("PROTOCOLLO_ALER"), ""))

                            Contenuto = Replace(Contenuto, "$codcontratto$", CodiceContratto)
                            Contenuto = Replace(Contenuto, "$codUI$", Strings.Left(CodiceContratto, Strings.Len(CodiceContratto) - 2))

                            If UCase(Trim(Trim(Cognome) & " " & Trim(Nome))) <> UCase(Trim(presso_cor)) Then
                                Contenuto = Replace(Contenuto, "$nominativo$", Cognome & " " & Nome & "<br />c/o " & presso_cor)
                            Else
                                Contenuto = Replace(Contenuto, "$nominativo$", presso_cor)
                            End If
                            sPosteAlerNominativo = Cognome & " " & Nome 'POSTE 

                            If sINTERNO <> "" Then
                                If sSCALA <> "" Then
                                    Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO & " SCALA " & sSCALA)
                                Else
                                    Contenuto = Replace(Contenuto, "$indirizzo2$", "INTERNO " & sINTERNO)
                                End If
                            ElseIf sSCALA <> "" Then
                                Contenuto = Replace(Contenuto, "$indirizzo2$", "SCALA " & sSCALA)
                            End If

                            sPosteAlerInterno = sINTERNO    'POSTE 

                            For i = 1 To Strings.Len(sSCALA)
                                If Char.IsDigit(Strings.Mid(sSCALA, i, 1)) = False Then
                                    sPosteAlerScala = Strings.Mid(sSCALA, i, Strings.Len(sSCALA))  'POSTE 
                                    Exit For
                                End If
                            Next i

                            Contenuto = Replace(Contenuto, "$indirizzo0$", tipo_cor & " " & indirizzo_cor & ", " & civico_cor)
                            Contenuto = Replace(Contenuto, "$indirizzo1$", cap_cor & " " & luogo_cor & " " & sigla_cor)
                            'Contenuto = Replace(Contenuto, "$indirizzo$", indirizzo_cor & ", " & civico_cor & "</br>" & cap_cor & " " & luogo_cor & " " & sigla_cor)

                            Contenuto = Replace(Contenuto, "$titolo$", Titolo & " " & Cognome & " " & Nome)

                            sPosteAlerInd = tipo_cor & " " & indirizzo_cor & " " & civico_cor 'POSTE 
                            sPosteAlerCAP = cap_cor                          'POSTE 
                            sPosteAlerLocalita = luogo_cor                   'POSTE 
                            sPosteAlerProv = sigla_cor                       'POSTE 


                            'INFORMAZIONI FILIALE
                            Contenuto = Replace(Contenuto, "$NomeFiliale$", sNomeFiliale)
                            Contenuto = Replace(Contenuto, "$IndirizzoFiliale$", sIndirizzo_Filiale)

                            par.cmd.CommandText = "select siscom_mi.getstatocontratto(" & idcontratto.Value & ") from dual"
                            myReaderA = par.cmd.ExecuteReader
                            If myReaderA.Read Then
                                If par.IfNull(myReaderA(0), "") = "CHIUSO" Then
                                    Contenuto = Replace(Contenuto, "$chiuso$", ".")
                                    sNumTel_Filiale = "Nel caso Lei desideri ricevere informazioni anche in merito alla possibilità di rateizzare " _
                                                    & "il debito maturato, al fine di evitarLe inutili attese, potrà contattare telefonicamente " _
                                                    & "il Settore Recupero Morosità presso la sede di viale Romagna n.26 al numero 02/73922319 per fissare un appuntamento."

                                Else
                                    Contenuto = Replace(Contenuto, "$chiuso$", " fino, se necessario, all’esecuzione dello sfratto per morosità.")
                                    sNumTel_Filiale = "Nel caso Lei desideri ricevere informazioni anche in merito alla possibilità di rateizzare " _
                                                    & "il debito maturato, al fine di evitarLe inutili attese, potrà contattare telefonicamente " _
                                                    & "la filiale al seguente numero <bold>" & sNumTel_Filiale & "</bold> per fissare un appuntamento. "
                                End If
                            Else
                                Contenuto = Replace(Contenuto, "$chiuso$", " fino, se necessario, all’esecuzione dello sfratto per morosità.")

                            End If
                            myReaderA.Close()
                            Contenuto = Replace(Contenuto, "$TelFiliale$", sNumTel_Filiale)

                            'Da MOROSITA_LETTERE
                            'NOTA: prima del 02/Sett/2001 era di 40 gg ora è di 60 + 20 + controllo se capita di sabato o domenica
                            '08/06/2012 su richiesta dell'avvocato RUMORE si riporta la data scadenza a 40gg dalla data di emissione
                            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            'ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            ' 12/09/2014 ora scadenzabollettino è definita nella maschera della creazione della morosita
                            ScadenzaBollettino = Request.QueryString("SCADBOL")

                            Dim d1 As Date
                            d1 = New Date(Mid(ScadenzaBollettino, 1, 4), Mid(ScadenzaBollettino, 5, 2), Mid(ScadenzaBollettino, 7, 2))
                            If d1.DayOfWeek = DayOfWeek.Saturday Then
                                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 42, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            ElseIf d1.DayOfWeek = DayOfWeek.Sunday Then
                                ScadenzaBollettino = par.AggiustaData(DateAdd("d", 41, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))
                            End If
                            '************

                            periodo = par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))

                            If PeriodoXLS_INIZIO > par.IfNull(myReader("inizio_periodo"), 0) Or PeriodoXLS_INIZIO = 0 Then
                                PeriodoXLS_INIZIO = par.IfNull(myReader("inizio_periodo"), 0)
                            End If

                            If PeriodoXLS_FINE < par.IfNull(myReader("fine_periodo"), 0) Or PeriodoXLS_FINE = 0 Then
                                PeriodoXLS_FINE = par.IfNull(myReader("fine_periodo"), 0)
                            End If

                            Contenuto = Replace(Contenuto, "$spazi$", "")
                            Contenuto = Replace(Contenuto, "$spazi1$", "")


                            'Contenuto = Replace(Contenuto, "$tipo$", TipoIngiunzione)
                            If TipoStampa = "Ingiunzione_Aa.htm" Or TipoStampa = "Ingiunzione_Bb.htm" Then

                                ValImporto1 = par.IfNull(myReader("Importo"), 0)

                                If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                                    par.cmd.Parameters.Clear()
                                    par.cmd.CommandText = "select IMPORTO" _
                                                       & " from SISCOM_MI.MOROSITA_LETTERE " _
                                                       & " where ID_MOROSITA=" & IndiceMorosita _
                                                       & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                       & "   and ID_ANAGRAFICA=" & IdAnagrafica _
                                                       & "   and BOLLETTINO is NULL " _
                                                       & "   and NUM_LETTERE=2"

                                    myReaderA = par.cmd.ExecuteReader()
                                    If myReaderA.Read Then
                                        ValImporto2 = par.IfNull(myReaderA("Importo"), 0)
                                    End If
                                    myReaderA.Close()

                                    Contenuto = Replace(Contenuto, "$importo1$", Format(ValImporto1, "##,##0.00"))
                                    Contenuto = Replace(Contenuto, "$importo2$", Format(ValImporto2, "##,##0.00"))

                                    ValImporto1 = ValImporto1 + ValImporto2
                                    Contenuto = Replace(Contenuto, "$importoTOT$", Format(ValImporto1, "##,##0.00"))

                                End If

                            Else
                                Contenuto = Replace(Contenuto, "$importoTOT$", Format(CDbl(Importo), "##,##0.00"))
                            End If


                            If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then

                                'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

                                sr.WriteLine(Contenuto)
                                sr.Close()

                                'Dim url As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Ingiunzione_") & CodiceContratto
                                url = Server.MapPath("..\FileTemp\Ingiunzione_") & CodiceContratto
                                pdfConverter1 = New PdfConverter

                                Licenza = Session.Item("LicenzaHtmlToPdf")
                                If Licenza <> "" Then
                                    pdfConverter1.LicenseKey = Licenza
                                End If

                                pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
                                pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                                pdfConverter1.PdfDocumentOptions.ShowFooter = False
                                pdfConverter1.PdfDocumentOptions.LeftMargin = 40
                                pdfConverter1.PdfDocumentOptions.RightMargin = 40
                                pdfConverter1.PdfDocumentOptions.TopMargin = 17
                                pdfConverter1.PdfDocumentOptions.BottomMargin = 30
                                pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                                pdfConverter1.PdfDocumentOptions.ShowHeader = False
                                pdfConverter1.PdfFooterOptions.FooterText = ("")
                                pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                                pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
                                pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")

                                'If sPosteAler <> "" Then
                                '    sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
                                'Else
                                '    sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteDefault.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4))
                                'End If

                                ''****************EVENTI CONTRATTI UNA PER CONTRATTO***************
                                Dim sStr1 As String = "Inviato il MAV MG e la lettera di messa in mora con " & par.IfNull(myReader("TIPO_INVIO"), "Racc. A.R.")

                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_CONTRATTI " _
                                                          & " (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                    & "values (:id_contratto,:id_operatore,:data,:cod_evento,:motivo)"

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))

                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMddHHmmss")))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_evento", "F176"))
                                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("motivo", sStr1))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                par.cmd.Parameters.Clear()
                                '************************************************

                                'INIZIO CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 1° PARTE #############################################
                                sNomeFileMorLettera = "MorositaLettera_" & idMorositaLettere & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

                                Dim LicenzaF As String = Session.Item("LicenzaPdfMerge")
                                If LicenzaF <> "" Then
                                    pdfMergeF.LicenseKey = LicenzaF
                                End If
                                'FINE CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 1° PARTE ###############################################


                            Else
                                NoteBollette = "MOROSITA' MG" 'MG” (M.AV. Global) per quello relativo alla morosità fino al 31/09/2009
                            End If


                            ' '' Ricavo ID di POSTALER per PostAler.txt (2)

                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                sPosteAlerIA = myReaderA(0)
                            End If
                            myReaderA.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                                              & " values (" & sPosteAlerIA & "," & idMorositaLettere & ",1)"
                            par.cmd.ExecuteNonQuery()
                            '******************************************************************

                            If sPosteAler <> "" Then
                                sPosteAler = sPosteAler & vbCrLf & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                            Else
                                sPosteAler = sPosteAler & String.Format("{0,-50};{1,-50};{2,-50};{3,-50};{4,-50};{5,-2};{6,-3};{7,-5};{8,-50};{9,-2};{10,-12};{11,-4};{12,-16};", sPosteAlerNominativo.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerInd.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteDefault.PadRight(50).Substring(0, 50), sPosteAlerScala.PadRight(2).Substring(0, 2), sPosteAlerInterno.PadRight(3).Substring(0, 3), sPosteAlerCAP.PadRight(5).Substring(0, 5), sPosteAlerLocalita.PadRight(50).Substring(0, 50), sPosteAlerProv.PadRight(2).Substring(0, 2), sPosteAlerCodUtente.PadRight(12).Substring(0, 12), sPosteAlerAcronimo.PadRight(4).Substring(0, 4), sPosteAlerIA.PadRight(16).Substring(0, 16))
                            End If



                            Dim Nome1 As String = ""
                            Dim Nome2 As String = ""

                            If UCase(Trim(Trim(Cognome) & " " & Trim(Nome))) <> UCase(Trim(presso_cor)) Then
                                Nome1 = Cognome & " " & Nome
                                Nome2 = presso_cor
                            Else
                                Nome1 = presso_cor
                            End If


                            ' '' Ricavo ID_BOLLETTA
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL FROM dual "
                            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderB.Read Then
                                ID_BOLLETTA = myReaderB(0)
                            End If
                            myReaderB.Close()


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE " _
                                                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, " _
                                                        & "NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_MOROSITA,ID_TIPO) " _
                                                & "values (:id,:n_rata,:data_emissione,:data_scadenza," _
                                                        & ":note,:id_contratto,:id_esercizio_f," _
                                                        & ":id_unita,:fl_annullata,:pagabile_presso,:cod_affittuario,:intestatario," _
                                                        & ":indirizzo,:cap_citta,:presso,:riferimento_da,:riferimento_a," _
                                                        & ":fl_stampato,:id_complesso,:data_ins_pagamento,:importo_pagato,:note_pagamento," _
                                                        & ":anno,:operatore_pag,:id_edificio,:data_annullo_pag,:operatore_annullo_pag,:rif_file,:id_morosita,4)"

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", ID_BOLLETTA))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("n_rata", 999))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", Format(Now, "yyyyMMdd")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", ScadenzaBollettino))

                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_1_sollecito", "NULL"))
                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_2_sollecito", "NULL"))
                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_pagamento", "NULL"))

                            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "RECUPERO CREDITI MOROSITA DAL " & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " AL " & par.FormattaData(par.IfNull(myReader("fine_periodo"), ""))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", NoteBollette))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_contratto", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_esercizio_f", RitornaNullSeIntegerMenoUno(par.RicavaEsercizioCorrente)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idunita.Value))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_annullata", "0"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pagabile_presso", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_affittuario", RitornaNullSeIntegerMenoUno(IdAnagrafica)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("intestatario", Strings.Left(Nome1, 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("indirizzo", Strings.Left(tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor), 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cap_citta", Strings.Left(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")", 100)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("presso", Strings.Left(Nome2, 100)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_da", par.IfNull(myReader("inizio_periodo"), "")))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("riferimento_a", par.IfNull(myReader("fine_periodo"), "")))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_stampato", "0"))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(idcomplesso)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ins_pagamento", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_pagato", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note_pagamento", ""))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", Year(Now)))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_pag", ""))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(idedificio)))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_annullo_pag", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_annullo_pag", DBNull.Value))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rif_file", "MOR"))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_morosita", IndiceMorosita))


                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                            par.cmd.Parameters.Clear()


                            ' INSERT tutte le sotto voci di BOL_BOLLETTE_VOCI
                            'RECUPERO MOROSITA' (150)
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                & " values " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                        & ID_BOLLETTA & "," _
                                                        & VOCE & "," _
                                                        & par.VirgoleInPunti(Importo) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + Importo

                            If NoteBollette = "MOROSITA' MA" Then
                                'SPESE DI NOTIFICA (628)
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                   & " values " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                        & ID_BOLLETTA _
                                                        & ",628," _
                                                        & par.VirgoleInPunti(spese_notifica) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + spese_notifica
                            End If

                            'SPESE MAV
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                               & " values " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                    & ID_BOLLETTA _
                                                    & ",407," _
                                                    & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                            par.cmd.ExecuteNonQuery()
                            Tot_Bolletta = Tot_Bolletta + SPESEmav

                            'BOLLO
                            If Tot_Bolletta >= APPLICABOLLO Then
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "insert into SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) " _
                                                   & " values " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," _
                                                            & ID_BOLLETTA _
                                                            & ",95," _
                                                            & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                par.cmd.ExecuteNonQuery()
                                Tot_Bolletta = Tot_Bolletta + BOLLO
                            End If
                            '******************************************************

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

                            RichiestaEmissioneMAV.causalePagamento = CreaCausale(TipoIngiunzione, ID_BOLLETTA, CodiceContratto)

                            RichiestaEmissioneMAV.scadenzaPagamento = Mid(ScadenzaBollettino, 1, 4) & "-" & Mid(ScadenzaBollettino, 5, 2) & "-" & Mid(ScadenzaBollettino, 7, 2)
                            RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                            RichiestaEmissioneMAV.userName = Format(CDbl(IdAnagrafica), "0000000000")
                            'RichiestaEmissioneMAV.codiceFiscaleDebitore = CF

                            RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                            '30/04/2012 Elimino controllo del vuoto perchè necessario azzerare NOMEDEBITORE del metodo RichiestaEmissioneMAV
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

                            Try
                                '12/01/2015 PUCCIA Nuova connessione  tls ssl
                                If DisabilitaExpect100Continue = "1" Then
                                    System.Net.ServicePointManager.Expect100Continue = False
                                End If
                                par.cmd.Parameters.Clear()
                                '/*/*/*/*/*tls v1
                                Dim v As String = ""
                                par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                                v = par.cmd.ExecuteScalar
                                System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                                '/*/*/*/*/*tls v1


                                System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler

                                Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                            Catch ex As Exception

                                par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'  where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                par.myTrans.Commit()

                                If RiControllaMAVCreati() > 0 Then
                                    Response.Write("<SCRIPT>alert('Attenzione...Alcuni MAV non sono stati creati! \n Rientrare nella singola scheda dell\'inquilino è rigenerare il MAV tramite il tasto PROCEDI!');</SCRIPT>")
                                End If

                                par.cmd.Dispose()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                                Session.Item("LAVORAZIONE") = "0"
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>" & ex.Message & " !</p>")
                                Exit Sub
                            End Try

                            If Esito.codiceRisultato = "0" Then
                                'If par.IfNull(myReader("NUM_LETTERE"), 1) = 1 Then
                                '    pdfMerge.AppendPDFFile(url & ".pdf")
                                '    IO.File.Delete(url & ".htm")
                                'End If

                                'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf"
                                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & Format(ID_BOLLETTA, "0000000000") & ".pdf"

                                binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length - 1)
                                outFile.Close()
                                ''sposto sotto per inversione ordine
                                'pdfMerge.AppendPDFFile(outputFileName)
                                'pdfMergeF.AppendPDFFile(outputFileName)


                                ' se la banca emette correttamente i MAV allora:
                                ' SETTO a BOL_BOLLETTE che è stato stampato e il numero di bollettino
                                num_bollettino = Esito.numeroMAV
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                   & " set   FL_STAMPATO='1'," _
                                                   & "       rif_bollettino='" & num_bollettino & "'" _
                                                   & " where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                'Riassunto = Riassunto & "<tr style='font-family: ARIAL; font-size: 9pt;'><td style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & CodiceContratto & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & tipo_cor & " " & indirizzo_cor & ", " & civico_cor & " " & cap_cor & " " & luogo_cor & " " & sigla_cor & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Cognome & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & Nome & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("inizio_periodo"), "")) & " - " & par.FormattaData(par.IfNull(myReader("fine_periodo"), "")) & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(par.IfNull(myReader("emissione"), "")) & " </td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & par.FormattaData(ScadenzaBollettino) & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000'>" & num_bollettino & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Importo), "##,##0.00") & "</td>" _
                                '                                                                      & "<td  style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;text-align: Right'>" & Format(CDbl(Tot_Bolletta - Importo), "##,##0.00") & "</td></tr>"
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(Titolo))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(Cognome & " " & Nome))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(tipo_cor & " " & indirizzo_cor & ", " & civico_cor))
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(cap_cor & " " & luogo_cor & " " & sigla_cor))
                                K = K + 1

                                'SOLO x TEST , per la produxione toglie il commento sopra e rimettrerlo sotto e togliere MOROSITA_EVENTI
                                'INIZIO TEST
                                'par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
                                '                  & "     set BOLLETTINO='" & num_bollettino & "'," _
                                '                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                '                  & "         COD_STATO='M04'" _
                                '                  & " where ID=" & idMorositaLettere

                                'par.cmd.ExecuteNonQuery()

                                'par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "insert into SISCOM_MI.MOROSITA_EVENTI " _
                                '                       & "  (ID,ID_MOROSITA_LETTERE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                '                       & "values (SISCOM_MI.SEQ_MOROSITA_EVENTI.NEXTVAL," & idMorositaLettere & "," _
                                '                                & Session.Item("ID_OPERATORE") & "," & Format(Now, "yyyyMMddHHmmss") & "," _
                                '                                & "'M04','Ricevuta PostAler: RITIRATA DAL DESTINATARIO')"

                                'par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                                '************ FINE TEST


                                'UPDATE BOL_BOLLETTE VECCHIE raggruppate per la BOLLETTA MOROSITA' NUOVA
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE " _
                                                    & " set   ID_BOLLETTA_RIC=" & ID_BOLLETTA _
                                                    & " where ID_MOROSITA_LETTERA=" & idMorositaLettere _
                                                    & "   and ID_MOROSITA=" & IndiceMorosita _
                                                    & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value))

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()
                                '************************************

                                'prima del 14/02/2012 stava in crealettere
                                'UPDATE BOL_BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                                   & " set  IMPORTO_RICLASSIFICATO = NVL(IMPORTO,0) - NVL(IMP_PAGATO,0) " _
                                                   & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                        & "  where ID_MOROSITA_LETTERA=" & idMorositaLettere _
                                                                        & "    and ID_MOROSITA=" & IndiceMorosita _
                                                                        & "    and FL_ANNULLATA=0 " _
                                                                        & "    and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) & ") " _
                                                  & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()


                                'IMPORTO_CANONE    
                                ValSommaImportoCanone = 0
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                                  & "  from SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA " _
                                                  & "  where BOL_BOLLETTE_VOCI.id_voce = T_VOCI_BOLLETTA.ID " _
                                                  & "    and T_VOCI_BOLLETTA.GRUPPO=4 " _
                                                  & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                         & " where ID_MOROSITA=" & IndiceMorosita _
                                                                         & "   and FL_ANNULLATA=0 " _
                                                                         & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                                         & "   and ID>0 " _
                                                                         & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                                myReaderB = par.cmd.ExecuteReader()
                                If myReaderB.Read Then
                                    ValSommaImportoCanone = par.IfNull(myReaderB(0), 0)
                                End If
                                myReaderB.Close()
                                '***********************************

                                'IMPORTO_ONERI    
                                ValSommaImportoOneri = 0
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "select SUM(nvl(importo_riclassificato, 0)) " _
                                                  & "  from SISCOM_MI.BOL_BOLLETTE_VOCI" _
                                                  & "  where ID_VOCE NOT IN (select ID from SISCOM_MI.T_VOCI_BOLLETTA where GRUPPO=4) " _
                                                  & "    and id_bolletta IN (select ID from SISCOM_MI.BOL_BOLLETTE " _
                                                                         & " where ID_MOROSITA=" & IndiceMorosita _
                                                                         & "   and FL_ANNULLATA=0 " _
                                                                         & "   and ID_CONTRATTO=" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.idcontratto.Value)) _
                                                                         & "   and ID>0 " _
                                                                         & "   and ID_BOLLETTA_RIC=" & ID_BOLLETTA & ")"

                                myReaderB = par.cmd.ExecuteReader()
                                If myReaderB.Read Then
                                    ValSommaImportoOneri = par.IfNull(myReaderB(0), 0)
                                End If
                                myReaderB.Close()
                                '***********************************

                                'UPDATE MOROSITA_LETTERE
                                par.cmd.Parameters.Clear()
                                par.cmd.CommandText = "UPDATE  siscom_mi.MOROSITA_LETTERE " _
                                                  & "     set BOLLETTINO='" & num_bollettino & "'," _
                                                  & "         DATA_SCADENZA='" & ScadenzaBollettino & "'," _
                                                  & "         COD_STATO='M00'," _
                                                  & "         IMPORTO_BOLLETTA=" & par.VirgoleInPunti(Tot_Bolletta) & "," _
                                                  & "         IMPORTO_CANONE= " & par.VirgoleInPunti(ValSommaImportoCanone) & "," _
                                                  & "         IMPORTO_ONERI= " & par.VirgoleInPunti(ValSommaImportoOneri) _
                                                  & " where ID=" & idMorositaLettere

                                par.cmd.ExecuteNonQuery()
                                par.cmd.Parameters.Clear()



                                ' Response.Redirect("ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".pdf")

                            Else
                                'lblErrore.Visible = True

                                ' se la banca NON emette correttamente i MAV allora:
                                ' ELIMINO  BOL_BOLLETTE 

                                par.cmd.Parameters.Clear()
                                'par.cmd.CommandText = "delete from SISCOM_MI.BOL_BOLLETTE where ID=" & ID_BOLLETTA
                                par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE set FL_ANNULLATA=1, DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "'  where ID=" & ID_BOLLETTA
                                par.cmd.ExecuteNonQuery()

                                Dim FileDaCreare As String = Format(ID_BOLLETTA, "0000000000")
                                If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml")) = True Then
                                    FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                                End If

                                'Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='ELABORAZIONI\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")
                                Response.Write("<p style='color: #FF0000; font-weight: bold'>Ci sono stati degli errori durante la fase di creazione.</br><a href='..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\" & FileDaCreare & ".xml' target='_blank'>Clicca qui per visualizzare gli errori</a></br>Il MAV on line non è stato creato!!</p>")

                                'outputFileName = Server.MapPath("ELABORAZIONI") & "\MAV\" & Format(ID_BOLLETTA, "0000000000") & ".xml"
                                outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\MAV\") & FileDaCreare & ".xml"

                                binaryData = System.Convert.FromBase64String(Esito.descrizioneTecnicaRisultato)
                                outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                                outFile.Write(binaryData, 0, binaryData.Length)
                                outFile.Close()
                            End If


                            'Else
                            '    If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                            '        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("ragione_sociale"), "") & " non sono stati stampati perchè la partita iva non ha un formato corretto!</p>")
                            '    Else
                            '        Response.Write("<p style='color: #FF0000; font-weight: bold'>La Raccomandata e il bollettino di " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "") & " non sono stati stampati perchè il codice fiscale non ha un formato corretto!</p>")
                            '    End If
                            'End If

                            'AGGIUNGO LA LETTERA
                            If par.IfNull(myReader("NUM_LETTERE"), 1) = 2 Or TipoStampa = "Ingiunzione_E.htm" Or TipoStampa = "Ingiunzione_F.htm" Then
                                pdfMerge.AppendPDFFile(url & ".pdf")
                                pdfMergeF.AppendPDFFile(url & ".pdf")
                                ''sposto sotto per inversione ordine
                                pdfMerge.AppendPDFFile(outputFileName)
                                pdfMergeF.AppendPDFFile(outputFileName)

                                IO.File.Delete(url & ".htm")

                                'Per la ogni MOROSITA_LETTERA creo un File ZIP con il/i MAV e la Lettera
                                'INIZIO CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 2° PARTE #############################################
                                pdfMergeF.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & sNomeFileMorLettera)
                                Dim zipficF As String
                                Dim objCrc32F As New Crc32()
                                Dim strmZipOutputStreamF As ZipOutputStream
                                Dim strFileF As String
                                Dim strmFileF As FileStream

                                zipficF = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & Strings.Left(sNomeFileMorLettera, Strings.Len(sNomeFileMorLettera) - 4) & ".zip")

                                strmZipOutputStreamF = New ZipOutputStream(File.Create(zipficF))
                                strmZipOutputStreamF.SetLevel(6)

                                strFileF = Server.MapPath("..\FileTemp\") & sNomeFileMorLettera
                                strmFileF = File.OpenRead(strFileF)
                                Dim abyBufferF(Convert.ToInt32(strmFileF.Length - 1)) As Byte
                                strmFileF.Read(abyBufferF, 0, abyBufferF.Length)

                                Dim sFile1F As String = Path.GetFileName(strFileF)
                                Dim theEntryF As ZipEntry = New ZipEntry(sFile1F)
                                Dim fiF As New FileInfo(strFileF)

                                theEntryF.DateTime = fiF.LastWriteTime
                                theEntryF.Size = strmFileF.Length
                                strmFileF.Close()
                                objCrc32F.Reset()
                                objCrc32F.Update(abyBufferF)
                                theEntryF.Crc = objCrc32F.Value
                                strmZipOutputStreamF.PutNextEntry(theEntryF)
                                strmZipOutputStreamF.Write(abyBufferF, 0, abyBufferF.Length)
                                File.Delete(strFileF)

                                strmZipOutputStreamF.Finish()
                                strmZipOutputStreamF.Close()
                                '***** FINE CREAZIONE ZIP singolo per MOROSITA_LETTERE.ID 2° PARTE ########################################

                            End If


                        Loop
                        myReader.Close()
                        .CloseFile()
                    End With

                    'Riassunto = Riassunto & "</table>"
                    contenutoRiassunto = Replace(contenutoRiassunto, "$riassunto$", Riassunto)
                    contenutoRiassunto = Replace(contenutoRiassunto, "$periodo$", par.FormattaData(PeriodoXLS_INIZIO) & " - " & par.FormattaData(PeriodoXLS_FINE))

                    PeriodoXLS_INIZIO = 0
                    PeriodoXLS_FINE = 0

                    'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    'Dim sr3 As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & "-E_F" & ".htm", False, System.Text.Encoding.GetEncoding("iso-8859-1"))

                    ''sr3.WriteLine(contenutoRiassunto)
                    'sr3.Close()


                    ''Dim url1 As String = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\Elenco_Lettere_Mor_") & IndiceMorosita
                    'Dim url1 As String = Server.MapPath("..\FileTemp\Elenco_Lettere_Mor_") & IndiceMorosita & "-E_F"

                    'Dim pdfConverter As PdfConverter = New PdfConverter

                    'Licenza = Session.Item("LicenzaHtmlToPdf")
                    'If Licenza <> "" Then
                    '    pdfConverter.LicenseKey = Licenza
                    'End If


                    'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                    'pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
                    'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfDocumentOptions.ShowFooter = False
                    'pdfConverter.PdfDocumentOptions.LeftMargin = 30
                    'pdfConverter.PdfDocumentOptions.RightMargin = 30
                    'pdfConverter.PdfDocumentOptions.TopMargin = 17
                    'pdfConverter.PdfDocumentOptions.BottomMargin = 30
                    'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                    'pdfConverter.PdfDocumentOptions.ShowHeader = False
                    'pdfConverter.PdfFooterOptions.FooterText = ("")
                    'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                    'pdfConverter.PdfFooterOptions.DrawFooterLine = False
                    'pdfConverter.PdfFooterOptions.PageNumberText = ""
                    'pdfConverter.PdfFooterOptions.ShowPageNumber = False
                    'pdfConverter.SavePdfFromUrlToFile(url1 & ".htm", url1 & ".pdf")

                    ' ''AGGIUNGO LA LETTERA
                    ''pdfMerge.AppendPDFFile(url & ".pdf")
                    ''IO.File.Delete(url & ".htm")

                    ''AGGIUNGO L'elenco
                    'pdfMerge.AppendPDFFile(url1 & ".pdf")
                    'IO.File.Delete(url1 & ".htm")


                    'COMMIT
                    par.myTrans.Commit()
                    par.cmd.Dispose()


                    'pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx)
                    pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\FileTemp\") & xx)

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String
                    Dim strFile As String
                    Dim strmFile As FileStream

                    'zipfic = Server.MapPath("Varie\" & sNomeFile & ".zip")
                    zipfic = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".zip")

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)

                    'scrivo file XLS
                    'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".xls")
                    strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
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

                    'scrivo file PDF
                    'strFile = Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\") & xx
                    strFile = Server.MapPath("..\FileTemp\") & xx
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

                    'Scrivo FILE TXT POSTE *******************************
                    Using sw As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt"))
                        sw.Write(sPosteAler)
                        sw.Close()
                    End Using

                    strFile = Server.MapPath("..\FileTemp\" & fileNamePosteAler & ".txt")
                    strmFile = File.OpenRead(strFile)
                    Dim abyBuffer2(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer2, 0, abyBuffer2.Length)

                    Dim sFile2 As String = Path.GetFileName(strFile)
                    theEntry = New ZipEntry(sFile2)
                    fi = New FileInfo(strFile)

                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer2)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer2, 0, abyBuffer2.Length)
                    File.Delete(strFile)
                    '******************************************

                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                End If
                'FINE E F **********************************************************************************************************
                '         **********************************************************************************************************





                If RiControllaMAVCreati() > 0 Then
                    Response.Write("<SCRIPT>alert('Attenzione...Alcuni MAV non sono stati creati! \n Rientrare nella singola scheda dell\'inquilino è rigenerare il MAV tramite il tasto PROCEDI!');</SCRIPT>")
                End If


                'Response.Redirect("..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".zip")

                'Response.Write("</br>E' stato creato il file contenente le lettere e i bollettini.</br><a href='Varie\" & sNomeFile & ".zip' target='_blank'>Clicca qui per visualizzare il file</a>")
                '***** Response.Write("</br>E' stato creato il file contenente le lettere e i bollettini.</br><a href='..\ALLEGATI\MOROSITA_CONTRATTI\" & sNomeFile & ".zip' target='_blank'>Clicca qui per visualizzare il file</a>")


                MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='450px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                i = 0
                MIOCOLORE = "#CCFFFF"

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & IndiceMorosita & "-*.zip")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next

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
                        MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/MOROSITA_CONTRATTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
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



                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                Exit Sub



            Catch ex As Exception
                Response.Write(ex.Message)

                If FlagConnessione = True Then
                    If TrovatoCD = True Or TrovatoAB = True = True Or TrovatoEF = True Then
                        par.myTrans.Rollback()
                    End If

                    If RiControllaMAVCreati() > 0 Then
                        Response.Write("<SCRIPT>alert('Attenzione...Alcuni MAV non sono stati creati! \n Rientrare nella singola scheda dell\'inquilino è rigenerare il MAV tramite il tasto PROCEDI!');</SCRIPT>")
                    End If

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Session.Item("LAVORAZIONE") = "0"
                End If

            End Try
        End If
    End Sub

    Private Function CreaCausale(ByVal Tipo As String, ByVal idb As Long, Optional ByVal codContratto As String = "") As String
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            sCausale = ""
            If Not String.IsNullOrEmpty(codContratto) Then
                sCausale = ("COD.RAPPORTO: " & codContratto).ToString.PadRight(55)
            End If

            CreaCausale = ""

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select T_VOCI_BOLLETTA.descrizione,BOL_BOLLETTE_VOCI.importo " _
                               & " from siscom_mi.BOL_BOLLETTE,siscom_mi.T_VOCI_BOLLETTA,siscom_mi.BOL_BOLLETTE_VOCI " _
                               & " where BOL_BOLLETTE_VOCI.id_bolletta=BOL_BOLLETTE.id " _
                               & "   and T_VOCI_BOLLETTA.id=BOL_BOLLETTE_VOCI.id_voce " _
                               & "   and BOL_BOLLETTE.id=" & idb _
                               & " order by t_voci_bolletta.descrizione asc"

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
            CreaCausale = ""
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'lblErrore.Visible = True
            'lblErrore.Text = ex.Message
            'Button1.Visible = False
        End Try

    End Function

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function


    Function RiControllaMAVCreati() As Integer
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim FlagConnessione As Boolean = False
        Dim ContaMAVMoristaErrati As Integer = 0
        Dim IndiceMorosita As String = ""

        RiControllaMAVCreati = 0

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            ''CREO LA TRANSAZIONE
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans


            IndiceMorosita = Request.QueryString("IDMOR")

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select count(*) from SISCOM_MI.MOROSITA_LETTERE where COD_STATO='M94' and  ID_MOROSITA=" & IndiceMorosita
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                If myReader1(0) > 0 Then
                    RiControllaMAVCreati = myReader1(0)
                End If
            End If
            myReader1.Close()

            'While myReader1.Read
            '    If par.IfNull(myReader1("BOLLETTINO"), "") = "" Then

            '        '3) MODIFICO LO STATO DELLE MOROSITA LETTERE (MAV ERRATO)
            '        par.cmd.Parameters.Clear()
            '        par.cmd.CommandText = "update  SISCOM_MI.MOROSITA_LETTERE " _
            '                           & " set COD_STATO='M94' " _
            '                           & " where ID=" & par.IfNull(myReader1("ID"), -1)

            '        par.cmd.ExecuteNonQuery()

            '        '2) RIPRISTINO TUTTE le BOLLETTE_VOCI (mod. 01/12/2011) tolto da BOL_BOLLETTE.IMPORTO_RICLASSIFICATO
            '        ' non dovrebbero esserci però per sicurezza
            '        par.cmd.Parameters.Clear()
            '        par.cmd.CommandText = "update SISCOM_MI.BOL_BOLLETTE_VOCI " _
            '                           & " set  IMPORTO_RICLASSIFICATO=Null " _
            '                           & " where ID_BOLLETTA in (select ID from SISCOM_MI.BOL_BOLLETTE " _
            '                                                & " where ID_MOROSITA=" & IndiceMorosita _
            '                                                & "   and ID_MOROSITA_LETTERA=" & par.IfNull(myReader1("ID"), -1) _
            '                                                & "   and FL_ANNULLATA=0 " _
            '                                                & "   and ID_RATEIZZAZIONE IS NULL ) " _
            '                          & " and ID_VOCE NOT IN (SELECT ID FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE FL_NO_SALDO = 1)"

            '        par.cmd.ExecuteNonQuery()
            '        '**************************************

            '        '2) RIPRISTINO TUTTE le BOLLETTE interessate della MOROSITA (vIdMorosita)
            '        par.cmd.Parameters.Clear()
            '        par.cmd.CommandText = "update  SISCOM_MI.BOL_BOLLETTE " _
            '                           & " set ID_BOLLETTA_RIC=Null," _
            '                           & "     ID_MOROSITA=Null" _
            '                           & " where ID_MOROSITA=" & IndiceMorosita _
            '                           & "   and ID_MOROSITA_LETTERA=" & par.IfNull(myReader1("ID"), -1) _
            '                           & "   and FL_ANNULLATA=0 " _
            '                           & "   and ID_RATEIZZAZIONE IS NULL"

            '        par.cmd.ExecuteNonQuery()
            '        ContaMAVMoristaErrati = ContaMAVMoristaErrati + 1

            '    End If

            'End While
            'myReader1.Close()

            ' COMMIT
            ' par.myTrans.Commit()
            par.cmd.CommandText = ""
            FlagConnessione = False


            'RiControllaMAVCreati = ContaMAVMoristaErrati

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"

        End Try

    End Function

End Class
