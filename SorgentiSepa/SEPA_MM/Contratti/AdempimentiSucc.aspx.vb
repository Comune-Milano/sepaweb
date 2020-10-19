Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_AdempimentiSucc
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim myExcelFile As New CM.ExcelFile
    Dim ClasseIban As New LibCs.CheckBancari

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        txtValuta.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        dataOdierna.Value = Format(Now, "yyyyMMdd")
        If Not IsPostBack Then
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -32, Now))) & " " & Year(DateAdd("M", -32, Now)), CStr(Year(DateAdd("M", -32, Now)) & Format(Month(DateAdd("M", -32, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -31, Now))) & " " & Year(DateAdd("M", -31, Now)), CStr(Year(DateAdd("M", -31, Now)) & Format(Month(DateAdd("M", -31, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -30, Now))) & " " & Year(DateAdd("M", -30, Now)), CStr(Year(DateAdd("M", -30, Now)) & Format(Month(DateAdd("M", -30, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -29, Now))) & " " & Year(DateAdd("M", -29, Now)), CStr(Year(DateAdd("M", -29, Now)) & Format(Month(DateAdd("M", -29, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -28, Now))) & " " & Year(DateAdd("M", -28, Now)), CStr(Year(DateAdd("M", -28, Now)) & Format(Month(DateAdd("M", -28, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -27, Now))) & " " & Year(DateAdd("M", -27, Now)), CStr(Year(DateAdd("M", -27, Now)) & Format(Month(DateAdd("M", -27, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -26, Now))) & " " & Year(DateAdd("M", -26, Now)), CStr(Year(DateAdd("M", -26, Now)) & Format(Month(DateAdd("M", -26, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -25, Now))) & " " & Year(DateAdd("M", -25, Now)), CStr(Year(DateAdd("M", -25, Now)) & Format(Month(DateAdd("M", -25, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -24, Now))) & " " & Year(DateAdd("M", -24, Now)), CStr(Year(DateAdd("M", -24, Now)) & Format(Month(DateAdd("M", -24, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -23, Now))) & " " & Year(DateAdd("M", -23, Now)), CStr(Year(DateAdd("M", -23, Now)) & Format(Month(DateAdd("M", -23, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -22, Now))) & " " & Year(DateAdd("M", -22, Now)), CStr(Year(DateAdd("M", -22, Now)) & Format(Month(DateAdd("M", -22, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -21, Now))) & " " & Year(DateAdd("M", -21, Now)), CStr(Year(DateAdd("M", -21, Now)) & Format(Month(DateAdd("M", -21, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -20, Now))) & " " & Year(DateAdd("M", -20, Now)), CStr(Year(DateAdd("M", -20, Now)) & Format(Month(DateAdd("M", -20, Now)), "00"))))

            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -19, Now))) & " " & Year(DateAdd("M", -19, Now)), CStr(Year(DateAdd("M", -19, Now)) & Format(Month(DateAdd("M", -19, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -18, Now))) & " " & Year(DateAdd("M", -18, Now)), CStr(Year(DateAdd("M", -18, Now)) & Format(Month(DateAdd("M", -18, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -17, Now))) & " " & Year(DateAdd("M", -17, Now)), CStr(Year(DateAdd("M", -17, Now)) & Format(Month(DateAdd("M", -17, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -16, Now))) & " " & Year(DateAdd("M", -16, Now)), CStr(Year(DateAdd("M", -16, Now)) & Format(Month(DateAdd("M", -16, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -15, Now))) & " " & Year(DateAdd("M", -15, Now)), CStr(Year(DateAdd("M", -15, Now)) & Format(Month(DateAdd("M", -15, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -14, Now))) & " " & Year(DateAdd("M", -14, Now)), CStr(Year(DateAdd("M", -14, Now)) & Format(Month(DateAdd("M", -14, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -13, Now))) & " " & Year(DateAdd("M", -13, Now)), CStr(Year(DateAdd("M", -13, Now)) & Format(Month(DateAdd("M", -13, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -12, Now))) & " " & Year(DateAdd("M", -12, Now)), CStr(Year(DateAdd("M", -12, Now)) & Format(Month(DateAdd("M", -12, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -11, Now))) & " " & Year(DateAdd("M", -11, Now)), CStr(Year(DateAdd("M", -11, Now)) & Format(Month(DateAdd("M", -11, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -10, Now))) & " " & Year(DateAdd("M", -10, Now)), CStr(Year(DateAdd("M", -10, Now)) & Format(Month(DateAdd("M", -10, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -9, Now))) & " " & Year(DateAdd("M", -9, Now)), CStr(Year(DateAdd("M", -9, Now)) & Format(Month(DateAdd("M", -9, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -8, Now))) & " " & Year(DateAdd("M", -8, Now)), CStr(Year(DateAdd("M", -8, Now)) & Format(Month(DateAdd("M", -8, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -7, Now))) & " " & Year(DateAdd("M", -7, Now)), CStr(Year(DateAdd("M", -7, Now)) & Format(Month(DateAdd("M", -7, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -6, Now))) & " " & Year(DateAdd("M", -6, Now)), CStr(Year(DateAdd("M", -6, Now)) & Format(Month(DateAdd("M", -6, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -5, Now))) & " " & Year(DateAdd("M", -5, Now)), CStr(Year(DateAdd("M", -5, Now)) & Format(Month(DateAdd("M", -5, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -4, Now))) & " " & Year(DateAdd("M", -4, Now)), CStr(Year(DateAdd("M", -4, Now)) & Format(Month(DateAdd("M", -4, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -3, Now))) & " " & Year(DateAdd("M", -3, Now)), CStr(Year(DateAdd("M", -3, Now)) & Format(Month(DateAdd("M", -3, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -2, Now))) & " " & Year(DateAdd("M", -2, Now)), CStr(Year(DateAdd("M", -2, Now)) & Format(Month(DateAdd("M", -2, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", -1, Now))) & " " & Year(DateAdd("M", -1, Now)), CStr(Year(DateAdd("M", -1, Now)) & Format(Month(DateAdd("M", -1, Now)), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(Now)) & " " & Year(Now), CStr(Year(Now) & Format(Month(Now), "00"))))
            cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 1, Now))) & " " & Year(DateAdd("M", 1, Now)), CStr(Year(DateAdd("M", 1, Now)) & Format(Month(DateAdd("M", 1, Now)), "00"))))
            cmbMese.Items.FindByValue(CStr(Year(Now) & Format(Month(Now), "00"))).Selected = True
            provenienza.Value = Request.QueryString("C")
            If provenienza.Value = "1" Then
                lblTitolo.Text = "Adempimenti Successivi - Cessioni"
            End If
        End If

    End Sub

    Protected Sub imgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        'Dim erroreDaGlobal As String = ""
        Dim ConOpenNow As Boolean = False
        Dim varImportoDaVersareTotale As Double = 0
        Try
            If confermaProcedi.Value = "1" And provenienza.Value <> "1" Then

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
                'Assume i valori:
                '1 - Soggetti che inviano le proprie dichiarazione.
                '10 - C.A.F. dipendenti e pensionati; C.A.F. imprese;
                'Societa' ed enti di cui all'art.3, comma 2 del DPR 322/98 (se tale societa' appartiene a un gruppo puo' trasmettere la propria dichiarazione e quelle delle aziende del gruppo);
                'Altri intermediari di cui all'art.3. comma 3 lett a), b), c) ed e) del DPR 322/98; Societa' degli Ordini di cui all' art. 3 Decr. Dir. 18/2/99;
                'Soggetto che trasmette le dichiarazioni per le quali l'impegno a trasmettere e' stato assunto da un professionista deceduto.

                Dim varCodFiscFornitore As String = ""
                Dim varSpazioUtente As String = ""
                Dim varSpazioServTelem As String = ""
                Dim varUfficioTerritoriale As String = ""
                Dim varCodiceABI As String = ""
                Dim varCodiceCAB As String = ""
                Dim varCodiceCIN As String = ""
                Dim varNumeroIBAN As String = ""
                Dim varNumContoCorrente As String = ""
                Dim varCodFiscTitolareCC As String = "01349670156"
                Dim varImportoDaVersare As Decimal = 0 '=??? ImportoBollo, ImportoRegistrazione, ImportoSanzioniRegistrazione, ImportoInteressi
                Dim strScript As String = ""
                Dim GiorniMese As Integer = 30

                Dim canone As Decimal = 0
                Dim impostaRegistro As String = ""
                Dim totSanzioni As Decimal = 0
                Dim totInteressi As Decimal = 0
                Dim giorniDiff As Integer = 0
                Dim ElencoIDContr As String = ""

                'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
                'Str = Str & "<" & "/div>"

                'Response.Write(Str)
                'Response.Flush()

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    ConOpenNow = True
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.myTrans = par.OracleConn.BeginTransaction()
                    '‘par.cmd.Transaction = par.myTrans
                End If

                par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=42"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    varCodFiscFornitore = par.IfNull(myReaderB("valore"), "")
                    'varCodFiscFornitore = "01349670156"
                End If
                myReaderB.Close()


                Dim sCondizioneST As String = ""
                par.cmd.CommandText = "select id_tipo_st from siscom_mi.tab_filiali where id=" & Session.Item("ID_STRUTTURA")
                Dim tipoST As Integer = par.IfNull(par.cmd.ExecuteScalar, -1)

                If tipoST = 0 Then
                    sCondizioneST = "AND COMPLESSI_IMMOBILIARI.ID_FILIALE =" & Session.Item("ID_STRUTTURA")
                End If

                par.cmd.CommandText = "SELECT distinct cod_ufficio_reg  from siscom_mi.rapporti_utenza WHERE data_decorrenza_Ae>'20091001' and bozza=0 and COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then
                    alertInfoMancante = False
                End If
                myReader.Close()

                Dim zipfic As String
                Dim NomeFilezip As String = "SUC_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFilezip)



                Dim i As Integer = 0
                Dim ElencoFile() As String
                Dim importoDaVersare As Decimal = 0

                Dim NomeFileElenco As String = "IMPOSTE_" & Format(Now, "yyyyMMddHHmmss")
                Dim NomeFilexls As String = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFileElenco & ".xls"
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

                    varNumeroIBAN = ClasseIban.CalcolaIBAN("IT", varCodiceCIN & varCodiceABI & varCodiceCAB & varNumContoCorrente)

                    Dim IndiceGiorni As Integer = 1
                    par.cmd.CommandText = "SELECT distinct cod_ufficio_reg  from siscom_mi.rapporti_utenza WHERE data_decorrenza_Ae>'20091001' and bozza=0 and COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtCodUfficio As New Data.DataTable
                    da.Fill(dtCodUfficio)
                    da.Dispose()
                    Dim queryDatiGenerali As String = ""
                    Dim queryInsert As String = ""
                    Dim idForn As Long = 0
                    If dtCodUfficio.Rows.Count > 0 Then
                        For Each rowCodUff As Data.DataRow In dtCodUfficio.Rows
                            Dim NomeFile As String = ""
                            Dim NomeFileXML As String = ""


                            Dim contaContratti As Integer = 0
                            varImportoDaVersare = 0
                            'Dim srDettagli As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".txt"), False, System.Text.Encoding.Default)
                            'Dim srErrori As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\Errori_" & NomeFile & ".txt"), False, System.Text.Encoding.Default)


                            stringaXML = ""
                            varUfficioTerritoriale = par.IfNull(rowCodUff.Item("cod_ufficio_reg"), "")





                            'ANN. SUCCESSIVE
                            'par.cmd.CommandText = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                            '    & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                            '    & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA  from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI where  " _
                            '    & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND  data_stipula>'20091001' and VERSAMENTO_TR='A' AND cod_ufficio_reg='" & varUfficioTerritoriale _
                            '    & "' and SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
                            '    & " and SUBSTR(DATA_DECORRENZA,1,4)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                            '    & "' AND SUBSTR(DATA_DECORRENZA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                            '    & "' AND BOZZA=0 and COD_TIPOLOGIA_CONTR_LOC<>'NONE' " _
                            '    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='112T')" _
                            '    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='112T')" _
                            '    & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"

                            'par.cmd.CommandText = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                            '    & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                            '    & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA  from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI where  " _
                            '    & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND  data_stipula>'20091001' AND cod_ufficio_reg='" & varUfficioTerritoriale _
                            '    & "' and SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
                            '    & " and SUBSTR(DATA_DECORRENZA,1,4)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                            '    & "' AND SUBSTR(DATA_DECORRENZA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                            '    & "' AND BOZZA=0 and COD_TIPOLOGIA_CONTR_LOC<>'NONE' " _
                            '    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & ")" _
                            '    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & ")" _
                            '    & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"

                            par.cmd.CommandText = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                                & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                                & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA  from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                & " where  " _
                                & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND  data_decorrenza_Ae>'20091001' AND cod_ufficio_reg='" & varUfficioTerritoriale _
                                & "' and SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
                                & " and (case when DATA_DECORRENZA_AE > '20091001' or RAPPORTI_UTENZA.DATA_STIPULA>=RAPPORTI_UTENZA.DATA_DECORRENZA_AE THEN substr(DATA_DECORRENZA_AE,1,4) ELSE substr(DATA_STIPULA,1,4) END)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                                & "' AND (case when DATA_DECORRENZA_AE > '20091001' or RAPPORTI_UTENZA.DATA_STIPULA>=RAPPORTI_UTENZA.DATA_DECORRENZA_AE THEN substr(DATA_DECORRENZA_AE,5,2) ELSE substr(DATA_STIPULA,5,2) END)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                                & "' AND BOZZA=0 and COD_TIPOLOGIA_CONTR_LOC<>'NONE' " _
                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND ID_FASE_REGISTRAZIONE<>3 AND COD_TRIBUTO in ('112T','114T'))" _
                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO in ('112T','114T'))" _
                                & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL AND SERIE_REGISTRAZIONE='3T' " _
                                & sCondizioneST & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                                & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                                & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                                & " AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID "

                            '
                            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dtIdContratti As New Data.DataTable
                            da2.Fill(dtIdContratti)
                            da2.Dispose()

                            If dtIdContratti.Rows.Count > 0 Then
                                For Each rowIdContr As Data.DataRow In dtIdContratti.Rows
                                    ElencoIDContr = ElencoIDContr & rowIdContr.Item("ID") & ","
                                    If IsProroga(CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)), CInt(Mid(par.IfNull(rowIdContr.Item("DATA_DECORRENZA_AE"), "2010"), 1, 4)), par.IfNull(rowIdContr.Item("DURATA_ANNI"), 0), par.IfNull(rowIdContr.Item("DURATA_RINNOVO"), 0)) = False Then
                                        If par.IfNull(rowIdContr.Item("VERSAMENTO_TR"), "A") = "A" Then
                                            canone = 0
                                            impostaRegistro = ""
                                            totSanzioni = 0
                                            totInteressi = 0
                                            giorniDiff = 0
                                            contaContratti = contaContratti + 1
                                            stringaXML = stringaXML & par.ScriveXMLAdempSuccContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtValuta.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "1", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff, Mid(cmbMese.SelectedItem.Value, 1, 6), queryInsert, idForn)
                                            varImportoDaVersare += importoDaVersare
                                            varImportoDaVersareTotale = varImportoDaVersareTotale + varImportoDaVersare
                                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                                                            & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'112T','" & Format(Now, "yyyyMMddHHmmss") & "','" & par.AggiustaData(txtValuta.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFilezip & "')"
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                    Else
                                        canone = 0
                                        impostaRegistro = ""
                                        totSanzioni = 0
                                        totInteressi = 0
                                        giorniDiff = 0
                                        contaContratti = contaContratti + 1
                                        stringaXML = stringaXML & par.ScriveXMLAdempSuccContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtValuta.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "2", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff, Mid(cmbMese.SelectedItem.Value, 1, 6), queryInsert, idForn)
                                        varImportoDaVersare += importoDaVersare
                                        varImportoDaVersareTotale = varImportoDaVersareTotale + varImportoDaVersare
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                                                        & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'114T','" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(txtValuta.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFilezip & "')"
                                        par.cmd.ExecuteNonQuery()
                                    End If

                                    If contaContratti > 0 Then
                                        queryDatiGenerali = " UPDATE SISCOM_MI.DATI_GENERALI_RLI SET  " _
                                            & " CODICE_FORNITURA             = " & par.insDbValue(varCodiceFornitura, True) & "  " _
                                            & " ,TIPO_FORNITORE                = " & par.insDbValue(varTipoFornitore, True) & "  " _
                                            & " ,COD_FISCALE_FORNITORE         = " & par.insDbValue(varCodFiscFornitore, True) & "  " _
                                            & " ,UFFICIO_TERRITORIALE          = " & par.insDbValue(varUfficioTerritoriale, True) & "  " _
                                            & " ,CODICE_ABI                    = " & par.insDbValue(varCodiceABI, True) & "  " _
                                            & " ,CODICE_CAB                    = " & par.insDbValue(varCodiceCAB, True) & "  " _
                                            & " ,NUMERO_CONTO_CORRENTE         = " & par.insDbValue(varNumContoCorrente, True) & "  " _
                                            & " ,CIN                           = " & par.insDbValue(varCodiceCIN, True) & "  " _
                                            & " ,COD_FISCALE_TITOLARE_CC       = " & par.insDbValue(varCodFiscTitolareCC, True) & "  " _
                                            & " ,IMPORTO_DA_VERSARE            = " & par.insDbValue(varImportoDaVersareTotale, True) & "  " _
                                            & " ,NOME_FILE_XML            = " & par.insDbValue(NomeFilezip, True) & "  " _
                                            & " where id= " & idForn
                                        par.cmd.CommandText = queryDatiGenerali
                                        par.cmd.ExecuteNonQuery()

                                        par.cmd.CommandText = queryInsert & " where id= " & idForn
                                        par.cmd.ExecuteNonQuery()

                                    End If
                                Next
                            End If

                            ''PROROGHE
                            'par.cmd.CommandText = "select (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                            '   & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                            '   & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune, (select distinct cf_piva  from siscom_mi.rapporti_utenza_imposte where (cod_tributo='107T' or cod_tributo='115T') AND siscom_mi.rapporti_utenza_imposte.id_contratto=siscom_mi.rapporti_utenza.ID) AS CF_PIVA from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI where  " _
                            '   & "UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND  data_stipula>'20091001' AND cod_ufficio_reg='" & varUfficioTerritoriale _
                            '   & "' and SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND SUBSTR(DATA_DECORRENZA,7,2)='" & Format(IndiceGiorni, "00") _
                            '   & "' and SUBSTR(DATA_DECORRENZA,1,4)<'" & Mid(cmbMese.SelectedItem.Value, 1, 4) _
                            '   & "' AND SUBSTR(DATA_DECORRENZA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) _
                            '   & "' AND SUBSTR(DATA_SCADENZA_RINNOVO,1,6)>'" & cmbMese.SelectedItem.Value _
                            '   & "' AND BOZZA=0" _
                            '   & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='114T')" _
                            '   & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='114T')" _
                            '   & " AND SISCOM_MI.RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL and COD_TIPOLOGIA_CONTR_LOC<>'NONE'"
                            ''Dim contaContratti2 As Integer = 0
                            'Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            'Dim dtIdContratti2 As New Data.DataTable
                            'da3.Fill(dtIdContratti2)
                            'da3.Dispose()
                            'If dtIdContratti2.Rows.Count > 0 Then
                            '    For Each rowIdContr As Data.DataRow In dtIdContratti2.Rows
                            '        proroga = False
                            '        If par.IfNull(rowIdContr.Item("COD_TIPOLOGIA_CONTR_LOC"), "") = "ERP" And par.IfNull(rowIdContr.Item("DATA_DECORRENZA"), "19990101") >= 20040101 Then
                            '            If (CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)) - CInt(Mid(par.IfNull(rowIdContr.Item("DATA_DECORRENZA"), "2010"), 1, 4))) Mod CInt(rowIdContr.Item("DURATA_RINNOVO")) = 0 Then
                            '                proroga = True
                            '            End If
                            '        End If

                            '        If par.IfNull(rowIdContr.Item("COD_TIPOLOGIA_CONTR_LOC"), "") <> "ERP" And par.IfNull(rowIdContr.Item("DURATA_RINNOVO"), 0) <> 0 Then
                            '            If (CInt(Mid(cmbMese.SelectedItem.Value, 1, 4)) - CInt(Mid(par.IfNull(rowIdContr.Item("DATA_DECORRENZA"), "2010"), 1, 4))) Mod CInt(rowIdContr.Item("DURATA_RINNOVO")) = 0 Then
                            '                proroga = True
                            '            End If
                            '        End If
                            '        If proroga = True Then
                            '            canone = 0
                            '            impostaRegistro = ""
                            '            totSanzioni = 0
                            '            totInteressi = 0
                            '            giorniDiff = 0
                            '            contaContratti = contaContratti + 1
                            '            stringaXML = stringaXML & par.ScriveXMLAdempSuccContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtValuta.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "2", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff)
                            '            varImportoDaVersare += importoDaVersare
                            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                            '                            & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'114T','" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(txtValuta.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFilezip & ".zip')"
                            '            par.cmd.ExecuteNonQuery()
                            '        End If
                            '    Next
                            'End If

                            'RISOLUZIONI
                            par.cmd.CommandText = "SELECT (SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT,(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                                & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                & "SISCOM_MI.UNITA_IMMOBILIARI  WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID And UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE Is NULL And UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA And " _
                                & "data_decorrenza_ae>'20091001' AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  " _
                                & "AND SUBSTR(DATA_RICONSEGNA,1,4)='" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "' AND SUBSTR(DATA_RICONSEGNA,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) & "' and cod_ufficio_reg='" & varUfficioTerritoriale & "'" _
                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='113T' AND ID_FASE_REGISTRAZIONE<>3)" _
                                & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='113T')" _
                                & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL AND SERIE_REGISTRAZIONE='3T' " _
                                & sCondizioneST & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                                & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                                & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                                & " AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID "
                            'Dim contaContratti3 As Integer = 0
                            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dtIdContratti3 As New Data.DataTable

                            da4.Fill(dtIdContratti3)
                            da4.Dispose()
                            If dtIdContratti3.Rows.Count > 0 Then
                                For Each rowIdContr As Data.DataRow In dtIdContratti3.Rows
                                    ElencoIDContr = ElencoIDContr & rowIdContr.Item("ID") & ","
                                    canone = 0
                                    impostaRegistro = ""
                                    totSanzioni = 0
                                    totInteressi = 0
                                    giorniDiff = 0
                                    contaContratti = contaContratti + 1
                                    stringaXML = stringaXML & par.ScriveXMLAdempSuccContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtValuta.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "4", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff, Mid(cmbMese.SelectedItem.Value, 1, 6), queryInsert, idForn)
                                    varImportoDaVersare += importoDaVersare
                                    varImportoDaVersareTotale = varImportoDaVersareTotale + varImportoDaVersare
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                                                    & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'113T','" & Format(Now, "yyyyMMddHHmmss") & "','" & par.AggiustaData(txtValuta.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFilezip & "')"
                                    par.cmd.ExecuteNonQuery()

                                    queryDatiGenerali = " UPDATE SISCOM_MI.DATI_GENERALI_RLI SET  " _
                                        & " CODICE_FORNITURA             = " & par.insDbValue(varCodiceFornitura, True) & "  " _
                                        & " ,TIPO_FORNITORE                = " & par.insDbValue(varTipoFornitore, True) & "  " _
                                        & " ,COD_FISCALE_FORNITORE         = " & par.insDbValue(varCodFiscFornitore, True) & "  " _
                                        & " ,UFFICIO_TERRITORIALE          = " & par.insDbValue(varUfficioTerritoriale, True) & "  " _
                                        & " ,CODICE_ABI                    = " & par.insDbValue(varCodiceABI, False) & "  " _
                                        & " ,CODICE_CAB                    = " & par.insDbValue(varCodiceCAB, False) & "  " _
                                        & " ,NUMERO_CONTO_CORRENTE         = " & par.insDbValue(varNumContoCorrente, True) & "  " _
                                        & " ,CIN                           = " & par.insDbValue(varCodiceCIN, True) & "  " _
                                        & " ,COD_FISCALE_TITOLARE_CC       = " & par.insDbValue(varCodFiscTitolareCC, True) & "  " _
                                        & " ,IMPORTO_DA_VERSARE            = " & par.insDbValue(varImportoDaVersare, False) & "  " _
                                        & " ,NOME_FILE_XML            = " & par.insDbValue(NomeFilezip, True) & "  " _
                                        & " where id= " & idForn
                                    par.cmd.CommandText = queryDatiGenerali
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = queryInsert & " where id= " & idForn
                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If


                            ''CESSIONI

                            'Dim DataCessione As String = ""
                            'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA_CESSIONI.ID_INT,RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
                            '    & "(SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO<>2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ADEGUAMENTO," _
                            '    & "RAPPORTI_UTENZA.*,UNITA_IMMOBILIARI.COD_TIPOLOGIA, siscom_mi.unita_contrattuale.cod_comune " _
                            '    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA_CESSIONI  " _
                            '    & "WHERE RAPPORTI_UTENZA_CESSIONI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND data_stipula>'20091001' AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  AND SUBSTR(RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,1,4)='" & Mid(cmbMese.SelectedItem.Value, 1, 4) & "' AND SUBSTR(RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,5,2)='" & Mid(cmbMese.SelectedItem.Value, 5, 2) & "' and cod_ufficio_reg='" & varUfficioTerritoriale & "'" _
                            '    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='110T')" _
                            '    & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(cmbMese.SelectedItem.Value, 4) & " AND COD_TRIBUTO='110T')" _
                            '    & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL"
                            'Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            'Dim dtIdContratti4 As New Data.DataTable

                            'da5.Fill(dtIdContratti4)
                            'da5.Dispose()
                            'If dtIdContratti4.Rows.Count > 0 Then
                            '    For Each rowIdContr1 As Data.DataRow In dtIdContratti4.Rows
                            '        ElencoIDContr = ElencoIDContr & rowIdContr1.Item("ID") & ","
                            '        canone = 0
                            '        impostaRegistro = ""
                            '        totSanzioni = 0
                            '        totInteressi = 0
                            '        giorniDiff = 0
                            '        contaContratti = contaContratti + 1
                            '        stringaXML = stringaXML & par.ScriveXMLAdempSuccContr(par.IfNull(rowIdContr1.Item("ID"), 0), par.AggiustaData(par.IfEmpty(txtValuta.Text, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "3", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff, Mid(cmbMese.SelectedItem.Value, 1, 6))
                            '        varImportoDaVersare += importoDaVersare
                            '        varImportoDaVersareTotale = varImportoDaVersareTotale + varImportoDaVersare
                            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                            '                        & "(" & par.IfNull(rowIdContr1.Item("ID"), 0) & "," & Left(cmbMese.SelectedItem.Value, 4) & ",'110T','" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(txtValuta.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFilezip & "')"
                            '        par.cmd.ExecuteNonQuery()
                            '    Next
                            'End If


                            If stringaXML <> "" Then
                                NomeFile = par.IfNull(rowCodUff.Item(0), "") & "_" & Format(Now, "yyyyMMddHHmmss")
                                NomeFileXML = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFile & ".xml"
                                ReDim Preserve ElencoFile(i)

                                ElencoFile(i) = NomeFileXML
                                i = i + 1

                                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\" & NomeFile & ".xml"), False)


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
                                   & "<reg:IBAN>" & varNumeroIBAN & "</reg:IBAN>" & vbCrLf _
                                   & "<reg:CodiceFiscaleTitolareCC>" & varCodFiscTitolareCC & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
                                   & "</loc:Versamento>" & vbCrLf _
                                   & "<loc:ImportoDaVersare>" & Format(varImportoDaVersare, "0.00") & "</loc:ImportoDaVersare>" & vbCrLf _
                                   & "</loc:DatiGenerali>" & vbCrLf

                                stringaXML = stringaIntestaz & stringaXML & "</loc:Fornitura>"
                                sr.WriteLine(stringaXML)
                                sr.Close()

                                
                            End If
                        Next



                        If varImportoDaVersareTotale > 0 Then
                            If i > 0 Then
                                CreaElencoXLS(ElencoIDContr, contaXSL, NomeFilexls)
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
                                    'File.Delete(strFile)
                                Next
                                strmZipOutputStream.Finish()
                                strmZipOutputStream.Close()

                                strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK se si desidera essere reindirizzati alla pagina contenente l\'elenco degli XML creati.');if (conf){location.replace('ElencoImposte.aspx');}" _
                                & "else{}</script>"
                                Response.Write(strScript)
                            Else
                                Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');</script>")
                            End If
                        Else
                            Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');</script>")
                        End If
                    End If
                Else
                    Response.Write("<script>alert('Attenzione, ci sono dei contratti in cui non è specificato l\'ufficio registro! Impossibile procedere.');</script>")
                End If

                If ConOpenNow = True Then
                    par.myTrans.Commit()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            Else
                If provenienza.Value = "1" And confermaProcedi.Value = "1" Then
                    Response.Write("<script>var c = window.open('SelezioneContrattiCessioni.aspx?DATAINVIO=" & txtValuta.Text & "&MESE=" & cmbMese.SelectedValue & "');c.focus();</script>")
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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Function CreaElencoXLS(ByVal elencoContr As String, ByRef kk As Integer, ByVal NomeFilexls As String) As String
        'Try

        Dim nominativo As String = ""
        Dim codiceUtente As String = ""
        If elencoContr <> "" Then
            elencoContr = Mid(elencoContr, 1, Len(elencoContr) - 1)
            elencoContr = par.GetQueryIN1000(elencoContr, "RAPPORTI_UTENZA.ID")
            'elencoContr = "(" & Mid(elencoContr, 1, Len(elencoContr) - 1) & ")"
        End If
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

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD.CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA DECORRENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA SCADENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD.UTENTE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "UFF.REGISTRO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ANNO REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "SERIE REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NUM. REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "COD.TRIBUTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "IMPORTO CANONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "IMPORTO TRIBUTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "GIORNI SANZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "IMPORTO SANZIONI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "IMPORTO INTERESSI", 12)
            End If
            par.cmd.CommandText = "select RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_IMPOSTE.*,INDIRIZZI.DESCRIZIONE ||', '|| INDIRIZZi.civico AS ""INDIRIZZO"" from siscom_mi.RAPPORTI_UTENZA,siscom_mi.RAPPORTI_UTENZA_IMPOSTE,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.INDIRIZZI " _
                & "where RAPPORTI_UTENZA_IMPOSTE.ID_FASE_REGISTRAZIONE<>3 AND RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+) AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND ANNO=" & Mid(cmbMese.SelectedItem.Value, 1, 4) & " AND /*rapporti_utenza.id in*/ " & elencoContr

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
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 2, par.FormattaData(par.IfNull(rowIdContr.Item("DATA_DECORRENZA_AE"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 3, par.FormattaData(par.IfNull(rowIdContr.Item("DATA_SCADENZA"), "")), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 4, par.IfNull(rowIdContr.Item("INDIRIZZO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 5, codiceUtente, 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 6, par.PulisciStrSql(nominativo), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 7, par.IfNull(rowIdContr.Item("COD_UFFICIO_REG"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 8, Left(cmbMese.SelectedItem.Value, 4), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 9, par.IfNull(rowIdContr.Item("SERIE_REGISTRAZIONE"), "XX"), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 10, par.IfNull(rowIdContr.Item("NUM_REGISTRAZIONE"), "000000"), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 11, par.IfNull(rowIdContr.Item("COD_TRIBUTO"), ""), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 12, (par.IfNull(rowIdContr.Item("imp_canone_iniziale"), 0)), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 13, par.IfNull(rowIdContr.Item("importo_tributo"), 0), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 14, par.IfNull(rowIdContr.Item("giorni_Sanzione"), 0), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 15, par.IfNull(rowIdContr.Item("importo_Sanzione"), 0), 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 16, par.IfNull(rowIdContr.Item("importo_interessi"), 0), 12)
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
End Class
