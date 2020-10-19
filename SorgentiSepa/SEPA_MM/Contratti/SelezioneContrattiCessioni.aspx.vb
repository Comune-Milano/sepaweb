Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_SelezioneContrattiCessioni
    Inherits System.Web.UI.Page
    Dim Str As String = ""
    Dim par As New CM.Global
    Dim myExcelFile As New CM.ExcelFile
    Dim ClasseIban As New LibCs.CheckBancari

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
        End If

        Str = "<div id=""dvvvPre"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            periodo = Request.QueryString("MESE")
            dataInvio.Value = Request.QueryString("DATAINVIO")
            CaricaContratti(periodo)
        End If
    End Sub

    Private Sub CaricaContratti(ByVal periodo As String)
        Try
            Dim bTrovato As Boolean = False
            Dim sStringaSql As String = ""
            Dim alertInfoMancante As Boolean = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            If periodo <> "" Then
                bTrovato = True
                sStringaSql = sStringaSql & " (SUBSTR(DATA_CESSIONE,1,4)<='" & Mid(periodo, 1, 4) & "' AND SUBSTR(DATA_CESSIONE,5,2)='" & Mid(periodo, 5, 2) & "')"
            End If

            If sStringaSql <> "" Then
                sStringaSql = " AND " & sStringaSql
            End If

            Dim dtContr As New Data.DataTable
            dtContr.Columns.Add("ID_CONTRATTO")
            dtContr.Columns.Add("TIPO_RAPPORTO")
            dtContr.Columns.Add("STATO")
            dtContr.Columns.Add("COD_CONTRATTO")
            dtContr.Columns.Add("DATA_CESSIONE")
            dtContr.Columns.Add("EX_INTESTATARIO")
            dtContr.Columns.Add("NUOVO_INTEST")

            Dim canone As Decimal = 0
            Dim impostaRegistro As String = ""
            Dim totSanzioni As Decimal = 0
            Dim totInteressi As Decimal = 0
            Dim giorniDiff As Integer = 0
            Dim ElencoIDContr As String = ""
            Dim dtElencoIDContr As New Data.DataTable
            Dim r1 As Data.DataRow
            dtElencoIDContr.Columns.Add("id_contratto")

            Dim codUfficioReg As String = ""
            Dim condizioneUffReg As String = ""
            Dim RIGA As System.Data.DataRow

            Dim sCondizioneST As String = ""
            par.cmd.CommandText = "select id_tipo_st from siscom_mi.tab_filiali where id=" & Session.Item("ID_STRUTTURA")
            Dim tipoST As Integer = par.IfNull(par.cmd.ExecuteScalar, -1)

            If tipoST = 0 Then
                sCondizioneST = "AND COMPLESSI_IMMOBILIARI.ID_FILIALE =" & Session.Item("ID_STRUTTURA")
            End If

            par.cmd.CommandText = "select distinct TAB_UFFICIO_REGISTRO.cod AS cod_ufficio_reg,TAB_UFFICIO_REGISTRO.descrizione from siscom_mi.TAB_UFFICIO_REGISTRO,siscom_mi.RAPPORTI_UTENZA,siscom_mi.RAPPORTI_UTENZA_CESSIONI WHERE RAPPORTI_UTENZA_CESSIONI.ID_CONTRATTO=RAPPORTI_UTENZA.ID and TAB_UFFICIO_REGISTRO.COD=RAPPORTI_UTENZA.COD_UFFICIO_REG AND siscom_mi.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND BOZZA='0' AND FL_STAMPATO=1 AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'" & sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtCodUfficio As New Data.DataTable
            da.Fill(dtCodUfficio)
            da.Dispose()
            If dtCodUfficio.Rows.Count > 0 Then
                For Each rowCodUff As Data.DataRow In dtCodUfficio.Rows
                    Dim contaContratti As Integer = 0
                    codUfficioReg = par.IfNull(rowCodUff.Item(0), "")
                    If codUfficioReg = "" Then
                        condizioneUffReg = " COD_UFFICIO_REG IS NULL "
                    Else
                        condizioneUffReg = " COD_UFFICIO_REG='" & codUfficioReg & "' "
                    End If

                    par.cmd.CommandText = "SELECT TO_CHAR(TO_DATE(DATA_CESSIONE,'YYYYmmdd'),'DD/MM/YYYY') as data_cessione, RAPPORTI_UTENZA_CESSIONI.ID_EX_INT as EX_INTESTATARIO,RAPPORTI_UTENZA_CESSIONI.ID_INT as NUOVO_INTEST, RAPPORTI_UTENZA.id,RAPPORTI_UTENZA.COD_CONTRATTO,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPO_RAPPORTO,SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) AS STATO " _
                                  & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.RAPPORTI_UTENZA_CESSIONI,siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI  " _
                                  & "WHERE RAPPORTI_UTENZA_CESSIONI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID and TIPOLOGIA_CONTRATTO_LOCAZIONE.COD=COD_TIPOLOGIA_CONTR_LOC AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND bozza=0 AND COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'  AND SUBSTR(RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,1,4)='" & Mid(periodo, 1, 4) & "' AND SUBSTR(RAPPORTI_UTENZA_CESSIONI.DATA_CESSIONE,5,2)='" & Mid(periodo, 5, 2) & "' and cod_ufficio_reg='" & rowCodUff.Item("cod_ufficio_reg") & "'" _
                                  & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE ANNO=" & Left(periodo, 4) & " AND COD_TRIBUTO='110T' AND ID_FASE_REGISTRAZIONE<>3)" _
                                  & "  AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_RICEVUTE WHERE ANNO=" & Left(periodo, 4) & " AND COD_TRIBUTO='110T')" _
                                  & " AND RAPPORTI_UTENZA.NUM_REGISTRAZIONE IS NOT NULL " _
                                  & sCondizioneST & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                                  & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                                  & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                                  & " AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID "
                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtIdContratti As New Data.DataTable
                    da2.Fill(dtIdContratti)
                    da2.Dispose()

                    If dtIdContratti.Rows.Count > 0 Then
                        For Each rowIdContr As Data.DataRow In dtIdContratti.Rows
                            r1 = dtElencoIDContr.NewRow()
                            r1.Item("id_contratto") = rowIdContr.Item("ID")
                            dtElencoIDContr.Rows.Add(r1)
                            RIGA = dtContr.NewRow()
                            RIGA.Item("ID_CONTRATTO") = rowIdContr.Item("ID")
                            RIGA.Item("TIPO_RAPPORTO") = rowIdContr.Item("TIPO_RAPPORTO")
                            RIGA.Item("STATO") = rowIdContr.Item("STATO")
                            RIGA.Item("COD_CONTRATTO") = rowIdContr.Item("COD_CONTRATTO")
                            RIGA.Item("DATA_CESSIONE") = par.FormattaData(par.IfNull(rowIdContr.Item("DATA_CESSIONE"), ""))

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE ID =" & rowIdContr.Item("EX_INTESTATARIO")
                            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                If par.IfNull(myReaderA("RAGIONE_SOCIALE"), "") <> "" Then
                                    RIGA.Item("EX_INTESTATARIO") = par.IfNull(myReaderA("RAGIONE_SOCIALE"), "")
                                Else
                                    RIGA.Item("EX_INTESTATARIO") = par.IfNull(myReaderA("COGNOME"), "") & " " & par.IfNull(myReaderA("NOME"), "")
                                End If
                            End If
                            myReaderA.Close()

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE ID =" & rowIdContr.Item("NUOVO_INTEST")
                            myReaderA = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                If par.IfNull(myReaderA("RAGIONE_SOCIALE"), "") <> "" Then
                                    RIGA.Item("NUOVO_INTEST") = par.IfNull(myReaderA("RAGIONE_SOCIALE"), "")
                                Else
                                    RIGA.Item("NUOVO_INTEST") = par.IfNull(myReaderA("COGNOME"), "") & " " & par.IfNull(myReaderA("NOME"), "")
                                End If
                            End If
                            myReaderA.Close()

                            dtContr.Rows.Add(RIGA)
                            contaContratti = contaContratti + 1
                        Next
                    End If
                Next
                lblContaContr.Text = " - Trovati " & dtContr.Rows.Count & " contratti"
                datagridContr.DataSource = dtContr
                datagridContr.DataBind()
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub chkAll_click(sender As Object, e As System.EventArgs)
        Try
            Dim impSelezionato As Integer = 0
            If controlloSelezione.Value = 0 Then
                For Each di As DataGridItem In datagridContr.Items
                    If DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = True
                        impSelezionato = impSelezionato + 1
                    End If
                Next
                controlloSelezione.Value = 1
            Else
                For Each di As DataGridItem In datagridContr.Items
                    If DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = False
                        impSelezionato = 0
                    End If
                Next
                controlloSelezione.Value = 0
            End If
            lblTotSelez2.Text = impSelezionato
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property periodo() As String
        Get
            If Not (ViewState("par_periodo") Is Nothing) Then
                Return CStr(ViewState("par_periodo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_periodo") = value
        End Set
    End Property


    Protected Sub chkSelected_click(sender As Object, e As System.EventArgs)
        Try
            Dim impSelezionato As Integer = 0
            For Each di As DataGridItem In datagridContr.Items
                If DirectCast(di.Cells(0).FindControl("ChSelezionato"), CheckBox).Checked = True Then
                    impSelezionato = impSelezionato + 1
                    controlloSelezione.Value = 1
                End If
            Next
            lblTotSelez2.Text = impSelezionato
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function ControllaCheckbox() As Boolean
        ControllaCheckbox = False
        Try
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            For Each oDataGridItem In Me.datagridContr.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    ControllaCheckbox = True
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Private Sub ImposteContratti(ByVal idcontratto As String)
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
                ' Dim ElencoIDContr As String = ""
                Dim dtElencoIDContr As New Data.DataTable
                Dim r1 As Data.DataRow
                dtElencoIDContr.Columns.Add("id_contratto")

                Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
                Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='Elaborazione in corso' ><br>Elaborazione in corso..."
                Str = Str & "<" & "/div>"

                Response.Write(Str)
                Response.Flush()

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    ConOpenNow = True
                    par.OracleConn.Open()
                    par.cmd = par.OracleConn.CreateCommand()
                    par.myTrans = par.OracleConn.BeginTransaction()
                    'par.cmd.Transaction = par.myTrans
                End If

                par.cmd.CommandText = "SELECT * from siscom_mi.parametri_bolletta where id=42"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderB.Read Then
                    varCodFiscFornitore = par.IfNull(myReaderB("valore"), "")
                End If
                myReaderB.Close()

                par.cmd.CommandText = "SELECT distinct cod_ufficio_reg  from siscom_mi.rapporti_utenza WHERE data_stipula>'20091001' and bozza=0 and COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE'"
                'par.cmd.CommandText = "select RAPPORTI_UTENZA.*,tipi_contratto_locazione.descrizione as TIPO_RAPPORTO from tipi_contratto_locazione,RAPPORTI_UTENZA WHERE tipi_contratto_locazione.id=rapporti_utenza.id_tipo_contratto and GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (COD_UFFICIO_REG IS NULL OR COD_UFFICIO_REG='-1') AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='') AND FL_STAMPATO=1 AND ID_TIPO_CONTRATTO IN (SELECT ID FROM TIPI_CONTRATTO_LOCAZIONE WHERE FL_ABUSIVO=0 AND FL_REGISTRAZIONE_AE=1) " & sStringaSql
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
                    par.cmd.CommandText = "select distinct TAB_UFFICIO_REGISTRO.cod AS cod_ufficio_reg,TAB_UFFICIO_REGISTRO.descrizione from siscom_mi.TAB_UFFICIO_REGISTRO,siscom_mi.RAPPORTI_UTENZA WHERE TAB_UFFICIO_REGISTRO.COD=RAPPORTI_UTENZA.COD_UFFICIO_REG AND BOZZA='0' AND FL_STAMPATO=1 AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' AND RAPPORTI_UTENZA.ID IN " & idcontratto
                    'par.cmd.CommandText = "SELECT distinct cod_ufficio_reg  from siscom_mi.rapporti_utenza WHERE data_stipula>'20091001' and bozza=0 and COD_UFFICIO_REG<>'-1' AND COD_UFFICIO_REG IS NOT NULL AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' AND RAPPORTI_UTENZA.ID IN " & idcontratto
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

                            stringaXML = ""
                            varUfficioTerritoriale = par.IfNull(rowCodUff.Item("cod_ufficio_reg"), "")
                            NomeFile = par.IfNull(rowCodUff.Item(1), "") & "_" & Format(Now, "yyyyMMddHHmmss")
                            NomeFileXML = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\IMPOSTE\") & NomeFile & ".xml"

                            par.cmd.CommandText = "select * from siscom_mi.rapporti_utenza,siscom_mi.rapporti_utenza_cessioni where rapporti_utenza.id in " & idcontratto & " and cod_ufficio_reg='" & rowCodUff.Item("cod_ufficio_Reg") & "' AND data_riconsegna is null and rapporti_utenza.id=rapporti_utenza_cessioni.id_contratto "
                            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dtIdContratti As New Data.DataTable
                            da2.Fill(dtIdContratti)
                            da2.Dispose()

                            If dtIdContratti.Rows.Count > 0 Then
                                For Each rowIdContr As Data.DataRow In dtIdContratti.Rows
                                    r1 = dtElencoIDContr.NewRow()
                                    r1.Item("id_contratto") = rowIdContr.Item("ID")
                                    dtElencoIDContr.Rows.Add(r1)
                                    canone = 0
                                    impostaRegistro = par.IfNull(rowIdContr.Item("IMPORTO"), 0).ToString
                                    totSanzioni = 0
                                    totInteressi = 0
                                    giorniDiff = 0
                                    contaContratti = contaContratti + 1
                                    stringaXML = stringaXML & par.ScriveXMLAdempSuccContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(dataInvio.Value, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, "6", varUfficioTerritoriale, canone, impostaRegistro, totSanzioni, totInteressi, giorniDiff, Mid(periodo, 1, 6), queryInsert, idForn)
                                    varImportoDaVersare += importoDaVersare
                                    'varImportoDaVersareTotale = varImportoDaVersareTotale + varImportoDaVersare
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO) VALUES " _
                                                    & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & "," & Left(periodo, 4) & ",'110T','" & Format(Now, "yyyyMMddHHmmss") & "','" & par.AggiustaData(dataInvio.Value) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFile & ".xml')"
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
                                        & " where id= " & idForn
                                    par.cmd.CommandText = queryDatiGenerali
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = queryInsert & " where id= " & idForn
                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If


                            If stringaXML <> "" Then

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
                                   & "<loc:DatiGenerali>" & vbCrLf

                                If varImportoDaVersare <> 0 Then
                                    stringaIntestaz &= "<loc:Versamento>" & vbCrLf _
                                   & "<reg:IBAN>" & varNumeroIBAN & "</reg:IBAN>" & vbCrLf _
                                   & "<reg:CodiceFiscaleTitolareCC>" & varCodFiscTitolareCC & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
                                   & "</loc:Versamento>" & vbCrLf
                                End If

                                stringaIntestaz &= "<loc:ImportoDaVersare>" & Format(varImportoDaVersare, "0.00") & "</loc:ImportoDaVersare>" & vbCrLf _
                                   & "</loc:DatiGenerali>" & vbCrLf

                                stringaXML = stringaIntestaz & stringaXML & "</loc:Fornitura>"
                                sr.WriteLine(stringaXML)
                                sr.Close()
                            End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.DATI_GENERALI_RLI SET nome_file_xml='" & NomeFile & ".xml' where id= " & idForn
                            par.cmd.ExecuteNonQuery()
                        Next

                        'If varImportoDaVersare > 0 Then
                        If i > 0 Then
                            CreaElencoXLS(dtElencoIDContr, contaXSL, NomeFilexls)
                            Dim kkK As Integer = 0

                            Dim objCrc32 As New Crc32()
                            Dim strmZipOutputStream As ZipOutputStream

                            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                            strmZipOutputStream.SetLevel(6)

                            Dim strFile As String

                            For kkK = 0 To i - 1
                                'strFile = Server.MapPath("..\ALLEGATI\" & Session.Item("ComuneCollegato") & "\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK))
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

                            'Response.Write("<script>alert('Operazione effettuata!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                            strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK se si desidera essere reindirizzati alla pagina contenente l\'elenco degli XML creati.');if (conf){location.replace('ElencoImposte.aspx');}" _
                            & "else{document.getElementById('dvvvPre').style.visibility = 'hidden';}</script>"
                            Response.Write(strScript)
                        Else
                            Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                            Exit Sub
                        End If
                        'Else
                        '    Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                        '    Exit Sub
                        'End If
                    End If
                Else
                    Response.Write("<script>alert('Attenzione, ci sono dei contratti in cui non è specificato l\'ufficio registro! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                    Exit Sub
                End If

                If ConOpenNow = True Then
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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function CreaElencoXLS(ByVal elencoIDcontr As Data.DataTable, ByRef kk As Integer, ByVal NomeFilexls As String) As String
        'Try

        Dim nominativo As String = ""
        Dim codiceUtente As String = ""
        Dim elencoContr As String = ""

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

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "NUM. RECORD", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD.CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA DECORRENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "DATA SCADENZA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "INDIRIZZO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "COD.UTENTE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "UFF.REGISTRO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "ANNO REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "SERIE REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "NUM. REGISTRAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "COD.TRIBUTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "IMPORTO CANONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "IMPORTO TRIBUTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "GIORNI SANZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "IMPORTO SANZIONI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "IMPORTO INTERESSI", 12)
            End If
            Dim contaContr As Integer = 0
            If elencoIDcontr.Rows.Count > 0 Then
                For Each rowIdContr As Data.DataRow In elencoIDcontr.Rows
                    contaContr = contaContr + 1
                    par.cmd.CommandText = "select RAPPORTI_UTENZA.*,RAPPORTI_UTENZA_IMPOSTE.*,INDIRIZZI.DESCRIZIONE ||', '|| INDIRIZZi.civico AS ""INDIRIZZO"" from siscom_mi.RAPPORTI_UTENZA,siscom_mi.RAPPORTI_UTENZA_IMPOSTE,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.INDIRIZZI " _
                        & "where RAPPORTI_UTENZA.ID=RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+) and cod_tributo='110T' and id_fase_Registrazione=1 AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND DATA_CREAZIONE='" & Format(Now, "yyyyMMdd") & "' AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND ANNO=" & Mid(periodo, 1, 4) & " AND rapporti_utenza.id = " & rowIdContr.Item("ID_CONTRATTO")
                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtIdContratti As New Data.DataTable
                    da2.Fill(dtIdContratti)
                    da2.Dispose()
                    For Each rowContr As Data.DataRow In dtIdContratti.Rows
                        par.cmd.CommandText = " select id, (CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END) AS ""INTESTATARIO"" from siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali where soggetti_contrattuali.id_contratto=" & rowContr.Item("id_contratto") & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.cod_tipologia_occupante='INTE'"
                        Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderX.Read Then
                            nominativo = par.IfNull(myReaderX("intestatario"), "")
                            codiceUtente = Format(par.IfNull(myReaderX("id"), "0"), "0000000000")
                        End If
                        myReaderX.Close()

                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 1, Format(contaContr, "000000000"), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 2, par.IfNull(rowContr.Item("COD_CONTRATTO"), ""), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 3, par.FormattaData(par.IfNull(rowContr.Item("DATA_DECORRENZA"), "")), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 4, par.FormattaData(par.IfNull(rowContr.Item("DATA_SCADENZA"), "")), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 5, par.IfNull(rowContr.Item("INDIRIZZO"), ""), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 6, codiceUtente, 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 7, par.PulisciStrSql(nominativo), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 8, par.IfNull(rowContr.Item("COD_UFFICIO_REG"), ""), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 9, Mid(par.IfNull(rowContr.Item("DATA_REG"), ""), 1, 4), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 10, par.IfNull(rowContr.Item("SERIE_REGISTRAZIONE"), "XX"), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 11, par.IfNull(rowContr.Item("NUM_REGISTRAZIONE"), "000000"), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 12, par.IfNull(rowContr.Item("COD_TRIBUTO"), ""), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 13, (par.IfNull(rowContr.Item("imp_canone_iniziale"), 0)), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 14, par.IfNull(rowContr.Item("importo_tributo"), 0), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 15, par.IfNull(rowContr.Item("giorni_Sanzione"), 0), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 16, par.IfNull(rowContr.Item("importo_Sanzione"), 0), 12)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, kk, 17, par.IfNull(rowContr.Item("importo_interessi"), 0), 12)
                        kk = kk + 1
                    Next
                Next
            End If

            .CloseFile()
        End With

        Return ""

    End Function

    Protected Sub ImgSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Dim chkSelezione As System.Web.UI.WebControls.CheckBox
        Dim dtContr As New Data.DataTable
        Dim ElencoContr As String = ""

        If ControllaCheckbox() = False Then
            Response.Write("<script>alert('Selezionare almeno un contratto!')</script>")
            Exit Sub
        End If

        For Each oDataGridItem In Me.datagridContr.Items
            chkSelezione = oDataGridItem.FindControl("ChSelezionato")

            If chkSelezione.Checked Then
                ElencoContr = ElencoContr & oDataGridItem.Cells(0).Text & ","
            End If
        Next
        If ElencoContr <> "" Then
            ElencoContr = "(" & Mid(ElencoContr, 1, Len(ElencoContr) - 1) & ")"

            ImposteContratti(ElencoContr)
        End If
    End Sub
End Class
