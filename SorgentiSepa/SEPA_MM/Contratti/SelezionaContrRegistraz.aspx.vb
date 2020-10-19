Imports System.IO
Imports System.Xml
Imports System.Xml.Schema
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic


Partial Class Contratti_SelezionaContrRegistraz
    Inherits System.Web.UI.Page
    Dim Str As String = ""
    Dim par As New CM.Global
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
            Dim dataStipulaDal As String = Request.QueryString("DATA_ST_DAL")
            Dim dataStipulaAl As String = Request.QueryString("DATA_ST_AL")
            Dim codContr As String = Request.QueryString("CODCONTR")
            dataInvio.Value = Request.QueryString("DATAINVIO")
            CaricaContratti(dataStipulaDal, dataStipulaAl, codContr)
        End If
    End Sub

    Private Sub CaricaContratti(ByVal dataStipulaDal As String, ByVal dataStipulaAl As String, ByVal codContratto As String)
        Try
            Dim bTrovato As Boolean = False
            Dim sStringaSql As String = ""
            Dim alertInfoMancante As Boolean = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand()
            End If

            Dim sCondizioneST As String = ""
            par.cmd.CommandText = "select id_tipo_st from siscom_mi.tab_filiali where id=" & Session.Item("ID_STRUTTURA")
            Dim tipoST As Integer = par.IfNull(par.cmd.ExecuteScalar, -1)

            If tipoST = 0 Then
                sCondizioneST = "AND COMPLESSI_IMMOBILIARI.ID_FILIALE =" & Session.Item("ID_STRUTTURA")
            End If


            If dataStipulaDal <> "" Then
                bTrovato = True
                sStringaSql = sStringaSql & " (case when RAPPORTI_UTENZA.DATA_STIPULA>=RAPPORTI_UTENZA.DATA_DECORRENZA and data_Decorrenza_Ae>=data_Decorrenza THEN DATA_DECORRENZA_AE ELSE DATA_STIPULA END)>='" & par.PulisciStrSql(par.AggiustaData(dataStipulaDal)) & "' "
            End If

            If dataStipulaAl <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " (case when RAPPORTI_UTENZA.DATA_STIPULA>=RAPPORTI_UTENZA.DATA_DECORRENZA and data_Decorrenza_Ae>=data_Decorrenza THEN DATA_DECORRENZA_AE ELSE DATA_STIPULA END)<='" & par.PulisciStrSql(par.AggiustaData(dataStipulaAl)) & "' "
            End If

            If codContratto <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO='" & par.PulisciStrSql(codContratto.ToUpper) & "' "
            End If

            If sStringaSql <> "" Then
                sStringaSql = " AND " & sStringaSql
            End If

            Dim dtContr As New Data.DataTable
            dtContr.Columns.Add("ID_CONTRATTO")
            dtContr.Columns.Add("TIPO_RAPPORTO")
            dtContr.Columns.Add("STATO")
            dtContr.Columns.Add("COD_CONTRATTO")
            dtContr.Columns.Add("INTESTATARIO")
            dtContr.Columns.Add("DATA_AE")
            dtContr.Columns.Add("VERSAMENTO_TR")

            'par.cmd.CommandText = "select RAPPORTI_UTENZA.*,tipi_contratto_locazione.descrizione as TIPO_RAPPORTO from siscom_mi.tipologia_contratto_locazione,RAPPORTI_UTENZA WHERE siscom_mi.tipologia_contratto_locazione.id=rapporti_utenza.id_tipo_contratto and GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (COD_UFFICIO_REG IS NULL OR COD_UFFICIO_REG='-1') AND (REG_TELEMATICA IS NULL OR REG_TELEMATICA='') AND FL_STAMPATO=1 AND ID_TIPO_CONTRATTO IN (SELECT ID FROM siscom_mi.tipologia_contratto_locazione WHERE FL_ABUSIVO=0 AND FL_REGISTRAZIONE_AE=1) " & sStringaSql
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.HasRows = True Then
            '    alertInfoMancante = True
            'End If
            'myReader.Close()

            'If alertInfoMancante = False Then
            Dim codUfficioReg As String = ""
            Dim condizioneUffReg As String = ""
            Dim RIGA As System.Data.DataRow
            par.cmd.CommandText = "select distinct COD_UFFICIO_REG from SISCOM_MI.RAPPORTI_UTENZA WHERE SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' " _
                        & " AND (NUM_REGISTRAZIONE IS NULL OR DATA_DECORRENZA_AE<>DATA_DECORRENZA) AND BOZZA='0' AND FL_STAMPATO=1 AND DEST_USO<>'Y' AND " _
                        & " COD_TIPOLOGIA_CONTR_LOC<>'NONE' " & sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtCodUfficio As New Data.DataTable
            da.Fill(dtCodUfficio)
            da.Dispose()
            If dtCodUfficio.Rows.Count > 0 Then
                For Each rowCodUff As Data.DataRow In dtCodUfficio.Rows
                    codUfficioReg = par.IfNull(rowCodUff.Item(0), "")
                    If codUfficioReg = "" Then
                        condizioneUffReg = " COD_UFFICIO_REG IS NULL "
                    Else
                        condizioneUffReg = " COD_UFFICIO_REG='" & codUfficioReg & "' "
                    End If
                    par.cmd.CommandText = "select RAPPORTI_UTENZA.ID AS ID_CONTRATTO,nvl(VERSAMENTO_TR,'A') as VERSAMENTO_TR ,RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(DATA_DECORRENZA_AE,'YYYYmmdd'),'DD/MM/YYYY') as data_AE,tipologia_contratto_locazione.descrizione as TIPO_RAPPORTO, siscom_mi.getintestatari(rapporti_utenza.id) AS INTESTATARIO,SISCOM_MI.getstatocontratto(rapporti_utenza.id) AS STATO " _
                                & " from SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI,siscom_mi.tipologia_contratto_locazione " _
                                & " WHERE " & condizioneUffReg & " And SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)<>'CHIUSO' AND (NUM_REGISTRAZIONE IS NULL OR DATA_DECORRENZA_AE<>DATA_DECORRENZA) " _
                                & " AND BOZZA='0' AND FL_STAMPATO=1 " & sStringaSql & " AND COD_TIPOLOGIA_CONTR_LOC<>'NONE' AND DEST_USO<>'Y' and tipologia_contratto_locazione.COD=rapporti_utenza.COD_TIPOLOGIA_CONTR_LOC " _
                                & sCondizioneST & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID " _
                                & " AND UNITA_CONTRATTUALE.ID_UNITA=UNITA_IMMOBILIARI.ID " _
                                & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                                & " AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID " _
                                & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE WHERE COD_TRIBUTO IN ('107T','115T') AND ID_FASE_REGISTRAZIONE<>3)" _
                                & " ORDER BY RAPPORTI_UTENZA.DATA_STIPULA ASC"

                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt2 As New Data.DataTable
                    da2.Fill(dt2)
                    da2.Dispose()
                    If dt2.Rows.Count > 0 Then
                        For i As Integer = 0 To dt2.Rows.Count - 1
                            RIGA = dtContr.NewRow()
                            RIGA.Item("ID_CONTRATTO") = dt2.Rows(i).Item("ID_CONTRATTO")
                            RIGA.Item("TIPO_RAPPORTO") = dt2.Rows(i).Item("TIPO_RAPPORTO")
                            RIGA.Item("STATO") = dt2.Rows(i).Item("STATO")
                            RIGA.Item("COD_CONTRATTO") = dt2.Rows(i).Item("COD_CONTRATTO")
                            RIGA.Item("INTESTATARIO") = dt2.Rows(i).Item("INTESTATARIO")
                            RIGA.Item("DATA_AE") = dt2.Rows(i).Item("DATA_AE")
                            If dt2.Rows(i).Item("VERSAMENTO_TR") = "A" Then
                                RIGA.Item("VERSAMENTO_TR") = "ANNUALE"
                            Else
                                RIGA.Item("VERSAMENTO_TR") = "UNICA"
                            End If
                            dtContr.Rows.Add(RIGA)
                        Next
                    End If
                Next
                lblContaContr.Text = " - Trovati " & dtContr.Rows.Count & " contratti"
                datagridContr.DataSource = dtContr
                datagridContr.DataBind()
            End If
            'Else
            'Response.Write("<script>alert('Attenzione, ci sono dei contratti in cui non è specificato l\'ufficio registro! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
            'End If

            Dim chkSelezione As System.Web.UI.WebControls.CheckBox
            Dim tipoErr As String = ""
            For Each oDataGridItem In Me.datagridContr.Items
                chkSelezione = oDataGridItem.FindControl("ChSelezionato")

                If VerificaRequisitiContratti(oDataGridItem.Cells(0).Text(), tipoErr) = True Then
                    chkSelezione.Enabled = False
                    DirectCast(oDataGridItem.Cells(0).FindControl("lblMsg"), Label).ForeColor = Drawing.Color.Red
                    Select Case tipoErr
                        Case "fileXML"
                            chkSelezione.Visible = False
                            DirectCast(oDataGridItem.Cells(0).FindControl("lblMsg"), Label).ToolTip = "Stampa contratto mancante"
                            DirectCast(oDataGridItem.Cells(0).FindControl("lblMsg"), Label).Text = "Stampa contratto mancante"
                        Case "uffReg"
                            DirectCast(oDataGridItem.Cells(0).FindControl("lblMsg"), Label).ToolTip = "Cod. ufficio registro mancante"
                        Case "dataDecorrAE"
                            DirectCast(oDataGridItem.Cells(0).FindControl("lblMsg"), Label).ToolTip = "Data decorrenza AE mancante"
                    End Select
                End If
            Next

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

    Private Function VerificaRequisitiContratti(ByVal idContratto As Long, ByRef tipoErr As String) As Boolean
        Dim datoMancante As Boolean = False

        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza_registrazione where id_contratto= " & idContratto
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read = False Then
            datoMancante = True
            tipoErr = "fileXML"
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where id= " & idContratto & " AND COD_UFFICIO_REG IS NOT NULL"
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader2.Read = False Then
            datoMancante = True
            tipoErr = "uffReg"
        End If
        myReader2.Close()

        par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where id= " & idContratto & " AND DATA_DECORRENZA_AE IS NOT NULL"
        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader3.Read = False Then
            datoMancante = True
            tipoErr = "dataDecorrAE"
        End If
        myReader3.Close()

        Return datoMancante

    End Function

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

    Private Sub RegistraContratti(ByVal idcontratto As String)
        Try
            Dim ConOpenNow As Boolean = False
            Dim bTrovato As Boolean = False
            Dim sStringaSql As String = ""
            Dim alertInfoMancante As Boolean = False
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
            Dim varNumeroIBAN As String = ""
            Dim varCodFiscTitolareCC As String = "01349670156"
            Dim varImportoDaVersare As Decimal = 0 '=??? ImportoBollo, ImportoRegistrazione, ImportoSanzioniRegistrazione, ImportoInteressi
            Dim strScript As String = ""

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
                'varCodFiscFornitore = "01349670156"
            End If
            myReaderB.Close()


            Dim zipfic As String = ""
            Dim NomeFile As String = ""
            Dim ElencoFile() As String
            Dim i As Integer = 0
            Dim importoDaVersare As Decimal = 0

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


            par.cmd.CommandText = "select distinct COD_UFFICIO_REG from SISCOM_MI.RAPPORTI_UTENZA WHERE RAPPORTI_UTENZA.ID IN " & idcontratto
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtCodUfficio As New Data.DataTable
            da.Fill(dtCodUfficio)
            da.Dispose()

            If dtCodUfficio.Rows.Count > 0 Then
                For Each rowCodUff As Data.DataRow In dtCodUfficio.Rows

                    Dim NomeFilezip As String = "REG_" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                    zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFilezip)

                    stringaXML = ""
                    varUfficioTerritoriale = par.IfNull(rowCodUff.Item("cod_ufficio_reg"), "")
                    Dim contaContratti As Integer = 0
                    Dim tiporegistrazione As String = ""
                    Dim CodiceTributo As String = ""
                    Dim tpg As String = ""
                    Dim varStringTxt As String = ""

                    par.cmd.CommandText = "select RAPPORTI_UTENZA.*  from siscom_mi.RAPPORTI_UTENZA WHERE  COD_UFFICIO_REG='" & par.IfNull(rowCodUff.Item(0), "") & "' AND RAPPORTI_UTENZA.ID IN " & idcontratto & " ORDER BY RAPPORTI_UTENZA.DATA_STIPULA ASC"

                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtIdContratti As New Data.DataTable
                    da2.Fill(dtIdContratti)
                    da2.Dispose()
                    Dim queryDatiGenerali As String = ""
                    Dim queryInsert As String = ""
                    Dim idForn As Long = 0
                    If dtIdContratti.Rows.Count > 0 Then
                        For Each rowIdContr As Data.DataRow In dtIdContratti.Rows
                            NomeFile = par.IfNull(rowCodUff.Item(0), "") & par.IfNull(rowIdContr.Item("ID"), 0) & "_" & Format(Now, "yyyyMMddHHmmss")
                            ReDim Preserve ElencoFile(i)

                            ElencoFile(i) = NomeFile
                            i = i + 1
                            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".xml"), False)

                            Dim srDettagli As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & NomeFile & ".txt"), False, System.Text.Encoding.Default)

                            srDettagli.WriteLine("ID CONTRATTO" & vbTab & "CODICE CONTRATTO" & vbTab & "CODICE UTENTE" & vbTab & "TIPO RAPPORTO" & vbTab & "IMPORTO REGISTRAZIONE" & vbTab & "SANZIONI" & vbTab & "INTERESSI" & vbTab & "DATA INVIO AG.ENTRATE" & vbTab & "DATA RIFERIMENTO")

                            contaContratti = 0
                            contaContratti = contaContratti + 1
                            varImportoDaVersare = 0
                            stringaXML = ""
                            varStringTxt = ""
                            'If par.SoluzioneUnica(par.IfNull(rowIdContr.Item("imp_canone_iniziale"), 0), par.IfNull(rowIdContr.Item("durata_anni"), 4), par.IfNull(rowIdContr.Item("perc_tr_canone"), 2)) <= 67 Then
                            '    tiporegistrazione = "T"
                            '    CodiceTributo = "107T"
                            ' Else
                            '    tiporegistrazione = "P"
                            '    CodiceTributo = "115T"
                            'End If
                            stringaXML = stringaXML & par.ScriveXMLStipulaContr(par.IfNull(rowIdContr.Item("ID"), 0), par.AggiustaData(par.IfEmpty(dataInvio.Value, Format(Now, "yyyyMMdd"))), contaContratti, importoDaVersare, varStringTxt, NomeFilezip & "-" & NomeFile, CodiceTributo, queryInsert, idForn)
                            'Else
                            ' par.cmd.Dispose()
                            ' par.OracleConn.Close()
                            'Session.Add("ERRORE", "Provenienza: ScriveXMLContratto " & erroreDaGlobal)
                            'Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                            ' Exit Sub
                            'End If
                            varImportoDaVersare += importoDaVersare

                            'If tiporegistrazione = "P" Then
                            '    tpg = "A"
                            'Else
                            '    tpg = "U"
                            'End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET REG_TELEMATICA='" & NomeFile & "' WHERE ID=" & par.IfNull(rowIdContr.Item("ID"), 0)
                            par.cmd.ExecuteNonQuery()

                            srDettagli.WriteLine(varStringTxt)

                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE (ID_CONTRATTO,ANNO,COD_TRIBUTO,DATA_CREAZIONE,DATA_AE,IMPORTO_CANONE,IMPORTO_TRIBUTO,GIORNI_SANZIONE,IMPORTO_SANZIONE,IMPORTO_INTERESSI, FILE_SCARICATO,RECUPERO,note) VALUES " _
                            '              & "(" & par.IfNull(rowIdContr.Item("ID"), 0) & ",NULL,'" & CodiceTributo & "','" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(txtDataInvio.Text) & "'," & par.VirgoleInPunti(canone) & "," & par.VirgoleInPunti(CDec(impostaRegistro)) & "," & giorniDiff & "," & par.VirgoleInPunti(totSanzioni) & "," & par.VirgoleInPunti(totInteressi) & ",'" & NomeFile & ".xml','1','" & par.PulisciStrSql(AnnotazioniXml) & "')"
                            'par.cmd.ExecuteNonQuery()

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
                                          & "<loc:UfficioCompetente>" & vbCrLf _
                                          & "<loc:UfficioTerritoriale>" & varUfficioTerritoriale & "</loc:UfficioTerritoriale>" & vbCrLf _
                                          & "</loc:UfficioCompetente>" & vbCrLf _
                                          & "<loc:Versamento>" & vbCrLf _
                                          & "<reg:IBAN>" & varNumeroIBAN & "</reg:IBAN>" & vbCrLf _
                                          & "<reg:CodiceFiscaleTitolareCC>" & varCodFiscTitolareCC & "</reg:CodiceFiscaleTitolareCC>" & vbCrLf _
                                          & "</loc:Versamento>" & vbCrLf _
                                          & "<loc:ImportoDaVersare>" & CStr(varImportoDaVersare) & "</loc:ImportoDaVersare>" & vbCrLf _
                                          & "</loc:DatiGenerali>" & vbCrLf

                            stringaXML = stringaIntestaz & stringaXML & "</loc:Fornitura>"
                            sr.WriteLine(stringaXML)
                            sr.Close()
                            srDettagli.Close()

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
                                        & " ,IMPORTO_DA_VERSARE            = " & par.insDbValue(varImportoDaVersare, False) & "  " _
                                        & " where id= " & idForn
                            par.cmd.CommandText = queryDatiGenerali
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = queryInsert & " where id= " & idForn
                            par.cmd.ExecuteNonQuery()
                        Next
                    Else
                        Response.Write("<script>alert('Nessun risultato!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                    End If
                Next

                If i > 0 Then

                    Dim kkK As Integer = 0

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)

                    Dim strFile As String

                    For kkK = 0 To i - 1
                        strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".xml")
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

                        strFile = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI\REGISTRAZIONI\" & ElencoFile(kkK) & ".txt")
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        '
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
                    Next

                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                    'Response.Write("<script>alert('Operazione effettuata!');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")

                    strScript = "<script language='javascript'>var conf = window.confirm('Operazione effettuata con successo. Cliccare su OK se si desidera essere reindirizzati alla pagina contenente l\'elenco degli XML creati.');if (conf){location.replace('ElencoRegistrazioni.aspx');}" _
                            & "else{document.getElementById('dvvvPre').style.visibility = 'hidden';}</script>"
                    Response.Write(strScript)
                Else
                    Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
                End If
            Else
                Response.Write("<script>alert('Attenzione, non sono presenti contratti da registrare! Impossibile procedere.');document.getElementById('dvvvPre').style.visibility = 'hidden';</script>")
            End If

            If ConOpenNow = True Then
                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " CercaFileDaRegistrare() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ImgSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        If confermaProcedi.Value = "1" Then
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

                RegistraContratti(ElencoContr)
            End If
        End If
    End Sub
End Class
