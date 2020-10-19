Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_SceltaImposteAnteAler
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim myExcelFile As New CM.ExcelFile

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        txtValuta.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        dataOdierna.Value = Format(Now, "yyyyMMdd")
        If Not IsPostBack Then
            Dim i As Integer = 0

            For i = 1 To Month(Now)
                cmbMese.Items.Add(New ListItem(par.ConvertiMese(i) & " " & 2014, CStr(2014 & Format(i, "00"))))
            Next
            
            cmbMese.Items.FindByValue(CStr(Year(Now) & Format(1, "00"))).Selected = True
        End If
    End Sub

    Protected Sub imgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        If txtValuta.Text = "" Then
            Response.Write("<script>alert('Inserire la data di addebito!');</script>")
            Exit Sub
        End If
        If cmbInteressi.SelectedItem.Value = "-1" Then
            Response.Write("<script>alert('Specificare se bisogna calcolare interessi e sanzioni!');</script>")
            Exit Sub
        End If
        Dim ConOpenNow As Boolean = False
        Try
            If confermaProcedi.Value = "1" Then

                Dim Str As String
                Dim bTrovato As Boolean = False
                Dim sStringaSql As String = ""
                Dim alertInfoMancante As Boolean = True
                Dim stringaXML As String = ""
                Dim stringaIntestaz As String = ""


                '***** 0 Intestazione XML *****
                Dim varIntestazione As String = ""
                Dim varCodiceFornitura As String = "RLI12"

                Dim varTipoFornitore As String = "10"

                Dim varCodFiscFornitore As String = ""
                Dim varSpazioUtente As String = ""
                Dim varSpazioServTelem As String = ""
                Dim varUfficioTerritoriale As String = ""
                Dim varCodiceABI As String = ""
                Dim varCodiceCAB As String = ""
                Dim varCodiceCIN As String = ""
                Dim varNumContoCorrente As String = ""
                Dim varCodFiscTitolareCC As String = "01349670156"
                Dim varImportoDaVersare As Decimal = 0
                Dim varImportoDaVersareTotale As Decimal = 0
                Dim strScript As String = ""
                Dim GiorniMese As Integer = 30

                Dim canone As Decimal = 0
                Dim impostaRegistro As String = ""
                Dim totSanzioni As Decimal = 0
                Dim totInteressi As Decimal = 0
                Dim giorniDiff As Integer = 0
                Dim ElencoIDContr As String = ""
                Dim CalcolaInteressi As Boolean = True
                Dim numerototalecontratti As Long = 0
                Dim ElencoIDContrDivisi As String = ""
                Dim AnnotazioniXml As String = ""

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    ConOpenNow = True
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    ‘‘par.cmd.Transaction = par.myTrans
                End If

                If cmbInteressi.SelectedItem.Value = "1" Then
                    CalcolaInteressi = True
                Else
                    CalcolaInteressi = False
                End If

                par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=10"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    varCodFiscFornitore = "01349670156"
                End If
                myReaderB.Close()

                'par.cmd.CommandText = "SELECT distinct cod_ufficio_reg  from siscom_mi.rapporti_utenza WHERE data_stipula>'20091001' and bozza=0 and COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'"
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader.HasRows = True Then
                '    alertInfoMancante = False
                'End If
                'myReader.Close()
                alertInfoMancante = False

                Dim zipfic As String
                Dim NomeFilezip As String = "ANTE_SUC_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                zipfic = Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFilezip)

                Dim i As Integer = 0
                Dim ElencoFile() As String
                Dim importoDaVersare As Decimal = 0

                Dim NomeFileElenco As String = "ANTE_IMPOSTE_" & Format(Now, "yyyyMMddHHmmss")
                Dim NomeFilexls As String = Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFileElenco & ".xls"
                Dim contaXSL As Integer = 2

                ReDim Preserve ElencoFile(i)

                ElencoFile(i) = NomeFilexls
                i = i + 1

                If alertInfoMancante = False Then

                    par.cmd.CommandText = "select * from siscom_mi.PARAMETRI_VERSAMENTO_XML"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        varCodiceABI = par.IfNull(myReaderA("CODICE_ABI"), "")
                        varCodiceCAB = par.IfNull(myReaderA("CODICE_CAB"), "")
                        varCodiceCIN = par.IfNull(myReaderA("CODICE_CIN"), "")
                        varNumContoCorrente = par.IfNull(myReaderA("NUM_CONTO_CORR"), "")
                        varCodFiscTitolareCC = par.IfNull(myReaderA("COD_FISC_TITOLARE_CC"), "")
                    End If
                    myReaderA.Close()

                    Dim IndiceGiorni As Integer = 1
                    par.cmd.CommandText = "SELECT DISTINCT cod_ufficio_reg FROM siscom_mi.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " _
                                        & "RAPPORTI_UTENZA.IMP_CANONE_INIZIALE>0 AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID And UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE Is NULL And UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA " _
                                        & "AND NVL(RAPPORTI_UTENZA.DATA_RICONSEGNA,'29991231')>='20140101' " _
                                        & "AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' " _
                                        & "AND NVL(SERIE_REGISTRAZIONE,'') IN ('3','3A','3B','3C','3P','3T','3V','1','1A','1B','1C','1D','1V','2','2A','2B','2C','2V') AND LENGTH(NVL(COD_UFFICIO_REG,''))=3 " _
                                        & "AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=2014) " _
                                        & "AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=2014) " _
                                        & "And data_stipula<'20091001' AND bozza=0  "
                    '& "and cod_contratto in ('0410202010500O01901','0410202010300Q00501','0410202010200P00301','0410202010400M01001','0410202020500G01701','0410202020500C01401','0410202020300B00701','0410202020400B01201','0410202020700L02101','0100002030100A03N02','0100046040100000E02','0510137010100AN0701','0510137010100AN0601','0410216020833B01401','0410224030500C01701','0410224030500B01701')"

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtCodUfficio As New Data.DataTable
                    da.Fill(dtCodUfficio)
                    da.Dispose()


                    If dtCodUfficio.Rows.Count > 0 Then
                        For Each rowCodUff As Data.DataRow In dtCodUfficio.Rows
                            Dim NomeFile As String = par.IfNull(rowCodUff.Item(0), "A01") & "_" & Format(Now, "yyyyMMddHHmmss")
                            Dim NomeFileXML As String = ""
                            varImportoDaVersare = 0

                            Dim contaContratti As Integer = 0

                            stringaXML = ""
                            varUfficioTerritoriale = par.IfNull(rowCodUff.Item("cod_ufficio_reg"), "A01")


                            par.cmd.CommandText = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                                & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                                & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA  from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI where  " _
                                & "RAPPORTI_UTENZA.IMP_CANONE_INIZIALE>0 AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND data_stipula<'20091001' AND nvl(cod_ufficio_reg,'AA')='" & varUfficioTerritoriale _
                                & "' and NVL(RAPPORTI_UTENZA.DATA_RICONSEGNA,'29991231')>='" & cmbMese.SelectedItem.Value & "01' " _
                                & " and SUBSTR(DATA_DECORRENZA,1,4)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                                & "' AND SUBSTR(DATA_DECORRENZA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                                & "' AND BOZZA=0 and COD_TIPOLOGIA_CONTR_LOC<>'NONE' " _
                                & "  AND NVL(SERIE_REGISTRAZIONE,'') IN ('3','3A','3B','3C','3P','3T','3V','1','1A','1B','1C','1D','1V','2','2A','2B','2C','2V') AND  NVL(num_registrazione,'aa')<>'aa' " _
                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & ")" _
                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & ")"
                            '& " and cod_contratto in ('0410202010500O01901','0410202010300Q00501','0410202010200P00301','0410202010400M01001','0410202020500G01701','0410202020500C01401','0410202020300B00701','0410202020400B01201','0410202020700L02101','0100002030100A03N02','0100046040100000E02','0510137010100AN0701','0510137010100AN0601','0410216020833B01401','0410224030500C01701','0410224030500B01701')"

                            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dtIdContratti As New Data.DataTable
                            da2.Fill(dtIdContratti)
                            da2.Dispose()

                            If dtIdContratti.Rows.Count > 0 Then
                                For Each rowIdContr As Data.DataRow In dtIdContratti.Rows

                                    If numerototalecontratti = 900 Then
                                        ElencoIDContr = ElencoIDContr & rowIdContr.Item("ID") & ","
                                        ElencoIDContrDivisi = ElencoIDContrDivisi & " rapporti_utenza.id in (" & Mid(ElencoIDContr, 1, Len(ElencoIDContr) - 1) & ") or " ' & rowIdContr.Item("ID") & ","
                                        numerototalecontratti = numerototalecontratti = 0
                                        ElencoIDContr = ""
                                    Else
                                        ElencoIDContr = ElencoIDContr & rowIdContr.Item("ID") & ","
                                        numerototalecontratti = numerototalecontratti + 1
                                    End If


                                    canone = 0
                                    impostaRegistro = ""
                                    totSanzioni = 0
                                    totInteressi = 0
                                    giorniDiff = 0
                                    contaContratti = contaContratti + 1
                                    AnnotazioniXml = ""
                                    stringaXML = stringaXML & par.ScriveXMLAdempSuccAnteAler(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtValuta.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "1", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff, CalcolaInteressi, AnnotazioniXml)
                                    varImportoDaVersare += importoDaVersare
                                    If varUfficioTerritoriale = "A01" Then
                                        AnnotazioniXml = AnnotazioniXml & "Cod.Uff.Registro impostato a A01"
                                    End If
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO,RECUPERO,note) VALUES " _
                                                    & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'112T','" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(txtValuta.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFile & ".xml','1','" & par.PulisciStrSql(AnnotazioniXml) & "')"
                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If

                            If stringaXML <> "" Then

                                NomeFileXML = Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFile & ".xml"
                                ReDim Preserve ElencoFile(i)

                                ElencoFile(i) = NomeFileXML
                                i = i + 1

                                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFile & ".xml"), False)


                                stringaIntestaz = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>" & vbCrLf _
                                   & "<!--created with SEPA@Web (www.sistemiesoluzionisrl.it)-->" & vbCrLf _
                                   & "<loc:Fornitura xmlns:sc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:common:v2" & Chr(34) & vbCrLf _
                                   & "xmlns:loc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:loc:v1" & Chr(34) & vbCrLf _
                                   & "xmlns:reg=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:reg:v1" & Chr(34) & " > " & vbCrLf _
                                   & "<loc:Intestazione>" & vbCrLf _
                                   & "<loc:CodiceFornitura>" & varCodiceFornitura & "</loc:CodiceFornitura>" & vbCrLf _
                                   & "<loc:TipoFornitore>" & varTipoFornitore & "</loc:TipoFornitore>" & vbCrLf _
                                   & "<loc:CodiceFiscaleFornitore>" & varCodFiscFornitore & "</loc:CodiceFiscaleFornitore>" & vbCrLf _
                                   & "</loc:Intestazione>" & vbCrLf _
                                   & "<loc:DatiGenerali>" & vbCrLf _
                                   & "<loc:Versamento>" & vbCrLf _
                                   & "<reg:CodiceABI>" & varCodiceABI & "</reg:CodiceABI>" & vbCrLf _
                                   & "<reg:CodiceCAB>" & varCodiceCAB & "</reg:CodiceCAB>" & vbCrLf _
                                   & "<reg:NumeroContoCorrente>" & varNumContoCorrente & "</reg:NumeroContoCorrente>" & vbCrLf _
                                   & "<reg:CIN>" & varCodiceCIN & "</reg:CIN>" & vbCrLf _
                                   & "<reg:CodiceFiscaleTitolareCC>" & varCodFiscTitolareCC & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
                                   & "</loc:Versamento>" & vbCrLf _
                                   & "<loc:ImportoDaVersare>" & Format(varImportoDaVersare, "0.00") & "</loc:ImportoDaVersare>" & vbCrLf _
                                   & "</loc:DatiGenerali>" & vbCrLf

                                varImportoDaVersareTotale = varImportoDaVersareTotale + varImportoDaVersare
                                stringaXML = stringaIntestaz & stringaXML & "</loc:Fornitura>"
                                sr.WriteLine(stringaXML)
                                sr.Close()
                            End If
                        Next

                        If numerototalecontratti <> 0 Then
                            ElencoIDContrDivisi = " and (" & ElencoIDContrDivisi & " rapporti_utenza.id in (" & Mid(ElencoIDContr, 1, Len(ElencoIDContr) - 1) & "))" ' & rowIdContr.Item("ID") & ","
                        End If

                        If varImportoDaVersareTotale > 0 Then
                            If i > 0 Then
                                CreaElencoXLSAnteAler(ElencoIDContrDivisi, contaXSL, NomeFilexls)
                                Dim kkK As Integer = 0

                                Dim objCrc32 As New Crc32()
                                Dim strmZipOutputStream As ZipOutputStream

                                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                                strmZipOutputStream.SetLevel(6)

                                Dim strFile As String


                                For kkK = 0 To i - 1
                                    'strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK))
                                    strFile = ElencoFile(kkK)
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
                                    If InStr(strFile, "xls") = 0 Then
                                        File.Delete(strFile)
                                    End If
                                Next
                                strmZipOutputStream.Finish()
                                strmZipOutputStream.Close()

                                'Response.Write("<script>alert('Operazione effettuata!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK se si desidera essere reindirizzati alla pagina contenente l\'elenco degli XML creati.');if (conf){location.replace('ElencoImposteANTE.aspx');}" _
                                & "else{document.getElementById('dvvvPre').style.visibility = 'hidden';}</script>"
                                Response.Write(strScript)
                            Else
                                Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                            End If
                        Else
                            Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                        End If
                    Else
                        Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                    End If
                Else
                    Response.Write("<script>alert('Attenzione, ci sono dei contratti in cui non è specificato l\'ufficio registro! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                End If

                If ConOpenNow = True Then
                    'par.myTrans.Rollback()
                    par.myTrans.Commit()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            End If
        Catch ex As Exception
            If ConOpenNow = True Then
                par.myTrans.Rollback()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " CercaFileDaRegistrare() -" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function CreaElencoXLSAnteAler(ByVal elencoContr As String, ByRef kk As Integer, ByVal NomeFilexls As String) As String
        'Try

        Dim nominativo As String = ""
        Dim codiceUtente As String = ""
        'If elencoContr <> "" Then
        '    elencoContr = "(" & Mid(elencoContr, 1, Len(elencoContr) - 1) & ")"
        'End If
        With myExcelFile
            If kk = 2 Then
                .CreateFile(NomeFilexls)
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)

                .SetColumnWidth(1, 1, 25)
                .SetColumnWidth(2, 2, 20)
                .SetColumnWidth(3, 3, 20)
                .SetColumnWidth(4, 4, 40)
                .SetColumnWidth(5, 5, 20)
                .SetColumnWidth(6, 6, 40)
                .SetColumnWidth(7, 7, 20)
                .SetColumnWidth(8, 8, 25)
                .SetColumnWidth(9, 9, 25)
                .SetColumnWidth(10, 10, 20)
                .SetColumnWidth(11, 11, 20)
                .SetColumnWidth(12, 12, 20)
                .SetColumnWidth(13, 13, 20)
                .SetColumnWidth(14, 14, 20)
                .SetColumnWidth(15, 15, 20)
                .SetColumnWidth(16, 16, 20)
                .SetColumnWidth(17, 17, 20)

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD.CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA DECORRENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA SCADENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD.UTENTE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "UFF.REGISTRO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ANNUALITA PAGATA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "ANNO REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "SERIE REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "NUM. REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "COD.TRIBUTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "GIORNI SANZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "IMPORTO CANONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "IMPORTO TRIBUTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "IMPORTO SANZIONI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "IMPORTO INTERESSI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "QUOTA INQUILINO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "QUOTA COMUNE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "NOTE", 12)
            End If
            par.cmd.CommandText = "select RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_IMPOSTE.*,RAPPORTI_UTENZA_IMPOSTE.NOTE AS NOTE_IMPOSTE,INDIRIZZI.DESCRIZIONE ||', '|| INDIRIZZi.civico AS ""INDIRIZZO"" from siscom_mi.RAPPORTI_UTENZA,siscom_mi.RAPPORTI_UTENZA_IMPOSTE,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.INDIRIZZI " _
                & "where RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+) AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND ANNO=" & Mid(cmbMese.SelectedItem.Value, 1, 4) & elencoContr

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtIdContratti As New Data.DataTable
            da2.Fill(dtIdContratti)
            da2.Dispose()
            If dtIdContratti.Rows.Count > 0 Then
                For Each rowIdContr As Data.DataRow In dtIdContratti.Rows

                    par.cmd.CommandText = " select id, (CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS ""INTESTATARIO"" from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where soggetti_contrattuali.id_contratto=" & rowIdContr.Item("id_contratto") & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read Then
                        nominativo = par.IfNull(myReaderX("intestatario"), "")
                        codiceUtente = Format(par.IfNull(myReaderX("id"), "0"), "0000000000")
                    End If
                    myReaderX.Close()

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 1, par.IfNull(rowIdContr.Item("COD_CONTRATTO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 2, par.FormattaData(par.IfNull(rowIdContr.Item("DATA_DECORRENZA"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 3, par.FormattaData(par.IfNull(rowIdContr.Item("DATA_SCADENZA"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 4, par.IfNull(rowIdContr.Item("INDIRIZZO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 5, codiceUtente, 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 6, par.PulisciStrSql(nominativo), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 7, par.IfNull(rowIdContr.Item("COD_UFFICIO_REG"), "A01"), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 8, Left(cmbMese.SelectedItem.Value, 4), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 9, Mid(par.IfNull(rowIdContr.Item("DATA_REG"), ""), 1, 4), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 10, par.IfNull(rowIdContr.Item("SERIE_REGISTRAZIONE"), "3t"), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 11, par.IfNull(rowIdContr.Item("NUM_REGISTRAZIONE"), "999999"), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 12, par.IfNull(rowIdContr.Item("COD_TRIBUTO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 13, par.IfNull(rowIdContr.Item("giorni_Sanzione"), 0), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 14, (par.IfNull(rowIdContr.Item("imp_canone_iniziale"), 0)), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 15, par.IfNull(rowIdContr.Item("importo_tributo"), 0), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 16, par.IfNull(rowIdContr.Item("importo_Sanzione"), 0), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 17, par.IfNull(rowIdContr.Item("importo_interessi"), 0), 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 18, Format(par.IfNull(rowIdContr.Item("importo_tributo"), 0) / 2, "0.00"), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 19, Format((par.IfNull(rowIdContr.Item("importo_tributo"), 0) / 2) + par.IfNull(rowIdContr.Item("importo_Sanzione"), 0) + par.IfNull(rowIdContr.Item("importo_interessi"), 0), "0.00"), 12)

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 20, par.IfNull(rowIdContr.Item("NOTE_IMPOSTE"), ""), 12)
                    kk = kk + 1
                Next
            End If

            .CloseFile()
        End With

        'Catch ex As Exception
        '    erroreCatch = "1"
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " CreaElencoXLS() -" & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        '    Return ex.Message
        'End Try

        Return ""

    End Function

    Private Function IsProroga(ByVal AnnoRiferimento As String, ByVal AnnoDecorrenza As String, ByVal durataAnni As Integer, ByVal durataRinnovo As Integer) As Boolean

        Dim proroga As Boolean = False

        If durataRinnovo > 0 Then
            If AnnoRiferimento < AnnoDecorrenza + durataAnni Then
                proroga = False
            ElseIf AnnoRiferimento = AnnoDecorrenza + durataAnni Then
                proroga = True
            End If

            If AnnoRiferimento > AnnoDecorrenza + durataAnni Then
                If (AnnoRiferimento - (AnnoDecorrenza + durataAnni)) Mod durataRinnovo = 0 Then
                    proroga = True
                End If
            End If
        End If

        Return proroga

    End Function

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Private Sub CreaElencoXLS(ElencoIDContrDivisi As String, contaXSL As Integer, NomeFilexls As String)
        Throw New NotImplementedException
    End Sub

End Class
