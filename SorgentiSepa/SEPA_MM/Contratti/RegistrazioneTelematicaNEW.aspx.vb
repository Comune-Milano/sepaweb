Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_RegistrazioneTelematicaNEW
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            dataOggi.Value = Format(Now, "dd/MM/yyyy")
        End If
        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataInvio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Write("<script>window.open('SelezionaContrRegistraz.aspx?CODCONTR=" & txtCodContratto.Text & "&DATAINVIO=" & txtDataInvio.Text & "&DATA_ST_DAL=" & txtStipulaDal.Text & "&DATA_ST_AL=" & txtStipulaAl.Text & "');</script>")
    End Sub
    'Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
    '    'Dim erroreDaGlobal As String = ""
    '    Try
    '        If confermaProcedi.Value = "1" Then


    '            Dim Str As String
    '            Dim bTrovato As Boolean = False
    '            Dim sStringaSql As String = ""
    '            Dim alertInfoMancante As Boolean = False
    '            Dim stringaXML As String = ""
    '            Dim stringaIntestaz As String = ""
    '            Dim ConOpenNow As Boolean = False

    '            '***** 0 Intestazione XML *****
    '            Dim varIntestazione As String = ""
    '            Dim varCodiceFornitura As String = "RLI12"

    '            Dim varTipoFornitore As String = "10"
    '            'Assume i valori:
    '            '1 - Soggetti che inviano le proprie dichiarazione.
    '            '10 - C.A.F. dipendenti e pensionati; C.A.F. imprese;
    '            'Societa' ed enti di cui all'art.3, comma 2 del DPR 322/98 (se tale societa' appartiene a un gruppo puo' trasmettere la propria dichiarazione e quelle delle aziende del gruppo);
    '            'Altri intermediari di cui all'art.3. comma 3 lett a), b), c) ed e) del DPR 322/98; Societa' degli Ordini di cui all' art. 3 Decr. Dir. 18/2/99;
    '            'Soggetto che trasmette le dichiarazioni per le quali l'impegno a trasmettere e' stato assunto da un professionista deceduto.

    '            Dim varCodFiscFornitore As String = ""
    '            Dim varSpazioUtente As String = ""
    '            Dim varSpazioServTelem As String = ""
    '            Dim varUfficioTerritoriale As String = ""
    '            Dim varCodiceABI As String = ""
    '            Dim varCodiceCAB As String = ""
    '            Dim varCodiceCIN As String = ""
    '            Dim varNumContoCorrente As String = ""
    '            Dim varCodFiscTitolareCC As String = "01349670156"
    '            Dim varImportoDaVersare As Decimal = 0 '=??? ImportoBollo, ImportoRegistrazione, ImportoSanzioniRegistrazione, ImportoInteressi
    '            Dim strScript As String = ""

    '            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
    '            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
    '            Str = Str & "<" & "/div>"

    '            Response.Write(Str)
    '            Response.Flush()

    '            If txtStipulaDal.Text <> "" Then
    '                bTrovato = True
    '                sStringaSql = sStringaSql & " (case when RAPPORTI_UTENZA.DATA_STIPULA>=RAPPORTI_UTENZA.DATA_DECORRENZA and data_Decorrenza_Ae=data_Decorrenza THEN DATA_DECORRENZA_AE WHEN data_Decorrenza_Ae>data_Decorrenza THEN DATA_DECORRENZA_AE ELSE DATA_STIPULA END)>='" & par.PulisciStrSql(par.AggiustaData(txtStipulaDal.Text)) & "' "
    '            End If

    '            If txtStipulaAl.Text <> "" Then
    '                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
    '                bTrovato = True
    '                sStringaSql = sStringaSql & " (case when RAPPORTI_UTENZA.DATA_STIPULA>=RAPPORTI_UTENZA.DATA_DECORRENZA and data_Decorrenza_Ae=data_Decorrenza THEN DATA_DECORRENZA_AE WHEN data_Decorrenza_Ae>data_Decorrenza THEN DATA_DECORRENZA_AE ELSE DATA_STIPULA END)<='" & par.PulisciStrSql(par.AggiustaData(txtStipulaAl.Text)) & "' "
    '            End If

    '            If sStringaSql <> "" Then
    '                sStringaSql = " AND " & sStringaSql
    '            End If

    '            If par.OracleConn.State = Data.ConnectionState.Closed Then
    '                ConOpenNow = True
    '                par.OracleConn.Open()
    '                par.SettaCommand(par)
    '                par.myTrans = par.OracleConn.BeginTransaction()
    '                ‘‘par.cmd.Transaction = par.myTrans
    '            End If

    '            par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=42"
    '            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReaderB.Read Then
    '                varCodFiscFornitore = par.IfNull(myReaderB("valore"), "")
    '                'varCodFiscFornitore = "01349670156"
    '            End If
    '            myReaderB.Close()

    '            Dim sCondizioneST As String = ""
    '            par.cmd.CommandText = "select id_tipo_st from siscom_mi.tab_filiali where id=" & Session.Item("ID_STRUTTURA")
    '            Dim tipoST As Integer = par.IfNull(par.cmd.ExecuteScalar, -1)

    '            If tipoST = 0 Then
    '                sCondizioneST = "AND COMPLESSI_IMMOBILIARI.ID_FILIALE =" & Session.Item("ID_STRUTTURA")
    '            End If

    '            par.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA WHERE SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
    '                & " AND (COD_UFFICIO_REG Is NULL Or COD_UFFICIO_REG='-1') AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='' OR DATA_DECORRENZA_AE<>DATA_DECORRENZA) " _
    '                & " AND FL_STAMPATO=1 And COD_TIPOLOGIA_CONTR_LOC<>'NONE' " & sStringaSql
    '            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader.HasRows = True Then
    '                alertInfoMancante = True
    '            End If
    '            myReader.Close()


    '            Dim zipfic As String = ""
    '            Dim NomeFile As String = ""
    '            Dim ElencoFile() As String
    '            Dim i As Integer = 0
    '            Dim importoDaVersare As Decimal = 0
    '            If alertInfoMancante = False Then

    '                par.cmd.CommandText = "select * from siscom_mi.PARAMETRI_VERSAMENTO_XML"
    '                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                If myReaderA.Read Then
    '                    varCodiceABI = par.IfNull(myReaderA("CODICE_ABI"), "")
    '                    varCodiceCAB = par.IfNull(myReaderA("CODICE_CAB"), "")
    '                    varCodiceCIN = par.IfNull(myReaderA("CODICE_CIN"), "")
    '                    varNumContoCorrente = par.IfNull(myReaderA("NUM_CONTO_CORR"), "")
    '                    varCodFiscTitolareCC = par.IfNull(myReaderA("COD_FISC_TITOLARE_CC"), "")
    '                End If
    '                myReaderA.Close()

    '                par.cmd.CommandText = "select distinct COD_UFFICIO_REG from SISCOM_MI.RAPPORTI_UTENZA WHERE SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
    '                    & " AND (REG_TELEMATICA Is NULL Or REG_TELEMATICA='' OR DATA_DECORRENZA_AE<>DATA_DECORRENZA) AND BOZZA='0' AND FL_STAMPATO=1 AND DEST_USO<>'Y' AND " _
    '                    & " COD_TIPOLOGIA_CONTR_LOC<>'NONE' " & sStringaSql
    '                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                Dim dtCodUfficio As New Data.DataTable
    '                da.Fill(dtCodUfficio)
    '                da.Dispose()

    '                If dtCodUfficio.Rows.Count > 0 Then
    '                    For Each rowCodUff As Data.DataRow In dtCodUfficio.Rows

    '                        Dim NomeFilezip As String = "REG_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

    '                        zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip)

    '                        stringaXML = ""
    '                        varUfficioTerritoriale = par.IfNull(rowCodUff.Item("cod_ufficio_reg"), "")
    '                        Dim contaContratti As Integer = 0
    '                        Dim tiporegistrazione As String = ""
    '                        Dim CodiceTributo As String = ""
    '                        Dim tpg As String = ""
    '                        Dim varStringTxt As String = ""
    '                        par.cmd.CommandText = "select RAPPORTI_UTENZA.*  from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI " _
    '                            & " WHERE COD_UFFICIO_REG='" & par.IfNull(rowCodUff.Item(0), "") & "' AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='' OR DATA_DECORRENZA_AE<>DATA_DECORRENZA) " _
    '                            & " AND BOZZA='0' AND FL_STAMPATO=1 " & sStringaSql & " AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' AND DEST_USO<>'Y' " _
    '                            & sCondizioneST & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
    '                            & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
    '                            & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
    '                            & " AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID " _
    '                            & " ORDER BY RAPPORTI_UTENZA.DATA_STIPULA ASC"
    '                        Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                        Dim dtIdContratti As New Data.DataTable
    '                        da2.Fill(dtIdContratti)
    '                        da2.Dispose()
    '                        Dim queryDatiGenerali As String = ""
    '                        Dim queryInsert As String = ""
    '                        Dim idForn As Long = 0
    '                        If dtIdContratti.Rows.Count > 0 Then
    '                            For Each rowIdContr As Data.DataRow In dtIdContratti.Rows
    '                                NomeFile = par.IfNull(rowCodUff.Item(0), "") & par.IfNull(rowIdContr.Item("ID"), 0) & "_" & Format(Now, "yyyyMMddHHmmss")
    '                                ReDim Preserve ElencoFile(i)

    '                                ElencoFile(i) = NomeFile
    '                                i = i + 1
    '                                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".xml"), False)

    '                                Dim srDettagli As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".txt"), False, System.Text.Encoding.Default)

    '                                srDettagli.WriteLine("ID CONTRATTO" & vbTab & "CODICE CONTRATTO" & vbTab & "CODICE UTENTE" & vbTab & "TIPO RAPPORTO" & vbTab & "IMPORTO REGISTRAZIONE" & vbTab & "SANZIONI" & vbTab & "INTERESSI" & vbTab & "DATA INVIO AG.ENTRATE" & vbTab & "DATA RIFERIMENTO")

    '                                contaContratti = 0
    '                                contaContratti = contaContratti + 1
    '                                varImportoDaVersare = 0
    '                                stringaXML = ""
    '                                varStringTxt = ""
    '                                If par.SoluzioneUnica(par.IfNull(rowIdContr.Item("imp_canone_iniziale"), 0), par.IfNull(rowIdContr.Item("durata_anni"), 4)) <= 67 Then
    '                                    tiporegistrazione = "T"
    '                                    CodiceTributo = "107T"
    '                                Else
    '                                    tiporegistrazione = "P"
    '                                    CodiceTributo = "115T"
    '                                End If
    '                                stringaXML = stringaXML & par.ScriveXMLStipulaContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtDataInvio.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, varStringTxt, NomeFilezip & "-" & NomeFile, CodiceTributo, queryInsert, idForn)
    '                                'Else
    '                                ' par.cmd.Dispose()
    '                                ' par.OracleConn.Close()
    '                                'Session.Add("ERRORE", "Provenienza: ScriveXMLContratto " & erroreDaGlobal)
    '                                'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '                                ' Exit Sub
    '                                'End If
    '                                varImportoDaVersare += importoDaVersare

    '                                If tiporegistrazione = "P" Then
    '                                    tpg = "A"
    '                                Else
    '                                    tpg = "U"
    '                                End If
    '                                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET versamento_tr='" & tpg & "', REG_TELEMATICA='" & NomeFile & "' WHERE ID=" & par.IfNull(rowIdContr.Item("ID"), 0)
    '                                par.cmd.ExecuteNonQuery()

    '                                srDettagli.WriteLine(varStringTxt)

    '                                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO,RECUPERO,note) VALUES " _
    '                                '              & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & ",NULL,'" & CodiceTributo & "','" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(txtDataInvio.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFile & ".xml','1','" & par.PulisciStrSql(AnnotazioniXml) & "')"
    '                                'par.cmd.ExecuteNonQuery()

    '                                stringaIntestaz = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>" & vbCrLf _
    '                                      & "<!--created with SEPA@Web (www.sistemiesoluzionisrl.it)-->" & vbCrLf _
    '                                      & "<loc:Fornitura xmlns:sc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:common:v2" & Chr(34) & vbCrLf _
    '                                      & "xmlns:loc=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:loc:v1" & Chr(34) & vbCrLf _
    '                                      & "xmlns:reg=" & Chr(34) & "urn:www.agenziaentrate.gov.it:specificheTecniche:sco:reg:v1" & Chr(34) & " > " & vbCrLf _
    '                                      & "<loc:Intestazione>" & vbCrLf _
    '                                      & "<loc:CodiceFornitura>" & varCodiceFornitura & "</loc:CodiceFornitura>" & vbCrLf _
    '                                      & "<loc:TipoFornitore>" & varTipoFornitore & "</loc:TipoFornitore>" & vbCrLf _
    '                                      & "<loc:CodiceFiscaleFornitore>" & varCodFiscFornitore & "</loc:CodiceFiscaleFornitore>" & vbCrLf _
    '                                      & "</loc:Intestazione>" & vbCrLf _
    '                                      & "<loc:DatiGenerali>" & vbCrLf _
    '                                      & "<loc:UfficioCompetente>" & vbCrLf _
    '                                      & "<loc:UfficioTerritoriale>" & varUfficioTerritoriale & "</loc:UfficioTerritoriale>" & vbCrLf _
    '                                      & "</loc:UfficioCompetente>" & vbCrLf _
    '                                      & "<loc:Versamento>" & vbCrLf _
    '                                      & "<reg:CodiceABI>" & varCodiceABI & "</reg:CodiceABI>" & vbCrLf _
    '                                      & "<reg:CodiceCAB>" & varCodiceCAB & "</reg:CodiceCAB>" & vbCrLf _
    '                                      & "<reg:NumeroContoCorrente>" & varNumContoCorrente & "</reg:NumeroContoCorrente>" & vbCrLf _
    '                                      & "<reg:CIN>" & varCodiceCIN & "</reg:CIN>" & vbCrLf _
    '                                      & "<reg:CodiceFiscaleTitolareCC>" & varCodFiscTitolareCC & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
    '                                      & "</loc:Versamento>" & vbCrLf _
    '                                      & "<loc:ImportoDaVersare>" & CStr(varImportoDaVersare) & "</loc:ImportoDaVersare>" & vbCrLf _
    '                                      & "</loc:DatiGenerali>" & vbCrLf

    '                                stringaXML = stringaIntestaz & stringaXML & "</loc:Fornitura>"
    '                                sr.WriteLine(stringaXML)
    '                                sr.Close()
    '                                srDettagli.Close()

    '                                queryDatiGenerali = " UPDATE SISCOM_MI.DATI_GENERALI_RLI SET  " _
    '                                    & " CODICE_FORNITURA             = " & par.insDbValue(varCodiceFornitura, True) & "  " _
    '                                    & " ,TIPO_FORNITORE                = " & par.insDbValue(varTipoFornitore, True) & "  " _
    '                                    & " ,COD_FISCALE_FORNITORE         = " & par.insDbValue(varCodFiscFornitore, True) & "  " _
    '                                    & " ,UFFICIO_TERRITORIALE          = " & par.insDbValue(varUfficioTerritoriale, True) & "  " _
    '                                    & " ,CODICE_ABI                    = " & par.insDbValue(varCodiceABI, True) & "  " _
    '                                    & " ,CODICE_CAB                    = " & par.insDbValue(varCodiceCAB, True) & "  " _
    '                                    & " ,NUMERO_CONTO_CORRENTE         = " & par.insDbValue(varNumContoCorrente, True) & "  " _
    '                                    & " ,CIN                           = " & par.insDbValue(varCodiceCIN, True) & "  " _
    '                                    & " ,COD_FISCALE_TITOLARE_CC       = " & par.insDbValue(varCodFiscTitolareCC, True) & "  " _
    '                                    & " ,IMPORTO_DA_VERSARE            = " & par.insDbValue(varImportoDaVersare, False) & "  " _
    '                                    & " where id= " & idForn
    '                                par.cmd.CommandText = queryDatiGenerali
    '                                par.cmd.ExecuteNonQuery()

    '                                par.cmd.CommandText = queryInsert & " where id= " & idForn
    '                                par.cmd.ExecuteNonQuery()
    '                            Next
    '                        Else
    '                            Response.Write("<script>alert('Nessun risultato!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
    '                        End If
    '                    Next

    '                    If i > 0 Then

    '                        Dim kkK As Integer = 0

    '                        Dim objCrc32 As New Crc32()
    '                        Dim strmZipOutputStream As ZipOutputStream

    '                        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '                        strmZipOutputStream.SetLevel(6)

    '                        Dim strFile As String

    '                        For kkK = 0 To i - 1
    '                            strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".xml")
    '                            Dim strmFile As FileStream = File.OpenRead(strFile)
    '                            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '                            '
    '                            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '                            Dim sFile As String = Path.GetFileName(strFile)
    '                            Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '                            Dim fi As New FileInfo(strFile)
    '                            theEntry.DateTime = fi.LastWriteTime
    '                            theEntry.Size = strmFile.Length
    '                            strmFile.Close()
    '                            objCrc32.Reset()
    '                            objCrc32.Update(abyBuffer)
    '                            theEntry.Crc = objCrc32.Value
    '                            strmZipOutputStream.PutNextEntry(theEntry)
    '                            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '                            File.Delete(strFile)

    '                            strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".txt")
    '                            strmFile = File.OpenRead(strFile)
    '                            Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '                            '
    '                            strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)

    '                            Dim sFile1 As String = Path.GetFileName(strFile)
    '                            theEntry = New ZipEntry(sFile1)
    '                            fi = New FileInfo(strFile)
    '                            theEntry.DateTime = fi.LastWriteTime
    '                            theEntry.Size = strmFile.Length
    '                            strmFile.Close()
    '                            objCrc32.Reset()
    '                            objCrc32.Update(abyBuffer1)
    '                            theEntry.Crc = objCrc32.Value
    '                            strmZipOutputStream.PutNextEntry(theEntry)
    '                            strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
    '                            File.Delete(strFile)
    '                        Next

    '                        strmZipOutputStream.Finish()
    '                        strmZipOutputStream.Close()

    '                        'Response.Write("<script>alert('Operazione effettuata!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")

    '                        strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK se si desidera essere reindirizzati alla pagina contenente l\'elenco degli XML creati.');if (conf){location.replace('ElencoRegistrazioni.aspx');}" _
    '                        & "else{document.getElementById('dvvvPre').style.visibility = 'hidden';}</script>"
    '                        Response.Write(strScript)
    '                    Else
    '                        Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
    '                    End If
    '                Else
    '                    Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
    '                End If
    '            Else
    '                Response.Write("<script>alert('Attenzione, ci sono dei contratti in cui non è specificato l\'ufficio registro! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
    '            End If

    '            If ConOpenNow = True Then
    '                par.myTrans.Commit()
    '                par.cmd.Dispose()
    '                par.OracleConn.Close()
    '                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            End If

    '        End If
    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " CercaFileDaRegistrare() -" & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try

    'End Sub
End Class
