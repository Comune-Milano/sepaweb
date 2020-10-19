Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contabilita_EstrattoConto_Gest
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then
                CaricaSaldoContabile()
                'CaricaImportiPeriodo()
                RiempiDatagridBoll()
            Else
                Response.Write("<script>alert('ERRORE RECUPERO INFORMAZIONI')</script>")

            End If


        End If
    End Sub

    Public Property dt1() As Data.DataTable
        Get
            If Not (ViewState("dt1") Is Nothing) Then
                Return ViewState("dt1")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dt1") = value
        End Set
    End Property

    Public Property DataDal() As String
        Get
            If Not (ViewState("par_DataDal") Is Nothing) Then
                Return CStr(ViewState("par_DataDal"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DataDal") = value
        End Set

    End Property

    Public Property DataAl() As String
        Get
            If Not (ViewState("par_DataAl") Is Nothing) Then
                Return CStr(ViewState("par_DataAl"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DataAl") = value
        End Set

    End Property

    Private Sub CaricaSaldoContabile()

        Dim DataDal As String
        Dim DataAl As String = ""
        Dim sStringaSQL As String = ""
        Dim DOVUTO As Decimal = 0
        Dim PAGATO As Decimal = 0

        '******APERTURA CONNESSIONE*****
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Try

            Dim CondCompetenza As String = ""
            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondCompetenza = "AND BOL_BOLLETTE.RIFERIMENTO_DA >= " & Request.QueryString("RIFDAL") & ""
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondCompetenza = CondCompetenza & "AND BOL_BOLLETTE.RIFERIMENTO_A <= " & Request.QueryString("RIFAL") & ""
            End If
            'Setto date periodo
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            'Me.lblInformazione.Text = "* = Bollette pagate successivamente al " & par.FormattaData(DataAl)
            Dim sValore As String
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader

            '***********DOVUTO RELATIVO ALLA PRIMA DATA*****************
            If (par.IfEmpty(DataDal, "Null") <> "Null") Then
                sValore = DataDal
                sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore

                ' '' '' ''sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0)-NVL(IMPORTO_RIC,0)-NVL(BOLLETTATO_QUOTA_SIND,0)) AS IMPORTO " _
                ' '' '' ''                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
                ' '' '' ''                    & "WHERE FL_ANNULLATA = 0 " _
                ' '' '' ''                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID AND BOL_BOLLETTE.ID_TIPO <> 5 " _
                ' '' '' ''                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL
                sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS IMPORTO, " _
                            & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE " _
                            & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                            & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza
                par.cmd.CommandText = sStringaSQL
                myReader = par.cmd.ExecuteReader()
                'TOTALE DEL DEBITO A CARICO DI UN UTENTE
                If myReader.Read Then
                    DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
                    PAGATO = par.IfNull(myReader("PAGATO"), 0)
                End If
                myReader.Close()
                sStringaSQL = ""

                '' '' ''***********PAGATO RELATIVO ALLA PRIMA DATA*****************
                ' '' '' ''If (par.IfEmpty(DataDal, "Null") <> "Null") Then
                ' '' '' ''    sValore = DataDal
                ' '' '' ''    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
                ' '' '' ''End If
                ' '' '' '' '' ''sStringaSQL = "SELECT SUM(NVL(IMPORTO_PAGATO,0)-NVL(IMPORTO_RIC,0)-NVL(BOLLETTATO_QUOTA_SIND,0)) AS PAGATO " _
                ' '' '' '' '' ''                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
                ' '' '' '' '' ''                    & "WHERE FL_ANNULLATA = 0 " _
                ' '' '' '' '' ''                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID AND BOL_BOLLETTE.ID_TIPO <> 5 " _
                ' '' '' '' '' ''                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL

                ' '' ''sStringaSQL = "SELECT SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO" _
                ' '' ''            & "FROM SISCOM_MI.BOL_BOLLETTE " _
                ' '' ''            & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL ))" _
                ' '' ''            & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL

                '' ''par.cmd.CommandText = sStringaSQL
                '' ''myReader = par.cmd.ExecuteReader()
                '' ''If myReader.Read Then
                '' ''    PAGATO = par.IfNull(myReader("PAGATO"), 0)
                '' ''End If
                '' ''myReader.Close()


            Else
                
            End If


            '*********PULISCO STINGA SQL PER SECONDO ESTRATTO*******
            sStringaSQL = ""
            par.cmd.CommandText = ""
            DOVUTO = 0
            PAGATO = 0

            '***********DOVUTO RELATIVO ALLA SECONDA DATA*****************

            If (par.IfEmpty(DataAl, "Null") <> "Null") Then
                sValore = DataAl
                sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
            End If
            sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS IMPORTO, " _
                        & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza
            par.cmd.CommandText = sStringaSQL
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
                PAGATO = par.IfNull(myReader("PAGATO"), 0)
            End If
            myReader.Close()
            sStringaSQL = ""

            ''***********PAGATO RELATIVO ALLA SECONDA DATA*****************

            'If (par.IfEmpty(DataAl, "Null") <> "Null") Then
            '    sValore = DataAl
            '    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_PAGAMENTO <= " & sValore
            'End If
            'sStringaSQL = "SELECT SUM(NVL(IMPORTO_PAGATO,0)-NVL(IMPORTO_RIC,0)-NVL(BOLLETTATO_QUOTA_SIND,0)) AS PAGATO " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
            '                    & "WHERE FL_ANNULLATA = 0 " _
            '                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID AND BOL_BOLLETTE.ID_TIPO <> 5 " _
            '                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL
            'par.cmd.CommandText = sStringaSQL
            'myReader = par.cmd.ExecuteReader()

            'If myReader.Read Then

            'End If
            'myReader.Close()

            '*********PULISCO STINGA SQL*******
            sStringaSQL = ""
            par.cmd.CommandText = ""
            DOVUTO = 0

            ''ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
            Dim UltimoPagam As String = 0
            UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) 'par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
            par.cmd.CommandText = ""
            par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & Request.QueryString("IDANA")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.LblTitolo.Text = " - " & par.IfNull(myReader("INTESTATARIO"), "") & " DATA STAMPA: " & UltimoPagam

            End If
            myReader.Close()

            Me.lblSottoTitolo.Text = "Partita Gestionale per data EMISSIONE"
            If Not String.IsNullOrEmpty(DataDal) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " dal " & par.FormattaData(DataDal)
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " al " & par.FormattaData(DataAl)
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Or Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " per COMPETENZA "
                If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " dal " & par.FormattaData(Request.QueryString("RIFDAL"))
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " al " & par.FormattaData(Request.QueryString("RIFAL"))
                End If
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("INCDAL")) Or Not String.IsNullOrEmpty(Request.QueryString("INCAL")) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " per data PAGAMENTO "
                If Not String.IsNullOrEmpty(Request.QueryString("INCDAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " dal " & par.FormattaData(Request.QueryString("INCDAL"))
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("INCAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " al " & par.FormattaData(Request.QueryString("INCAL"))
                End If
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("VALDAL")) Or Not String.IsNullOrEmpty(Request.QueryString("VALAL")) Then
                lblSottoTitolo.Text = lblSottoTitolo.Text & " per data VALUTA "
                If Not String.IsNullOrEmpty(Request.QueryString("VALDAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " dal " & par.FormattaData(Request.QueryString("VALDAL"))
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("VALAL")) Then
                    lblSottoTitolo.Text = lblSottoTitolo.Text & " al " & par.FormattaData(Request.QueryString("VALAL"))
                End If
            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        End Try


    End Sub

    Private Sub RiempiDatagridBoll()
        Try
            Dim num_bolletta As String = ""
            Dim I As Integer = 0
            Dim importobolletta As Decimal = 0
            Dim importobolletta2 As Decimal = 0
            Dim importopagato As Decimal = 0
            Dim residuo As Decimal = 0
            Dim morosita As Integer = 0
            Dim riclass As Integer = 0
            Dim indiceMorosita As Integer = 0
            Dim indiceBolletta As Integer = 0

            Dim condEmissione As String = ""
            Dim CondRiferimento As String = ""
            Dim CondIncasso As String = ""
            Dim CondIncasso2 As String = ""
            Dim CondTipoDoc As String = ""


            If Not String.IsNullOrEmpty(DataDal) Then
                condEmissione = " AND BOL_BOLLETTE_GEST.DATA_EMISSIONE >= '" & DataDal & "' "
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                condEmissione = condEmissione & " AND BOL_BOLLETTE_GEST.DATA_EMISSIONE <= '" & DataAl & "'"
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondRiferimento = "AND BOL_BOLLETTE_GEST.RIFERIMENTO_DA >= '" & Request.QueryString("RIFDAL") & "' "
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondRiferimento = CondRiferimento & "AND BOL_BOLLETTE_GEST.RIFERIMENTO_A <= '" & Request.QueryString("RIFAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("INCDAL")) Then
                CondIncasso = "AND BOL_BOLLETTE_GEST.DATA_PAGAMENTO >= '" & Request.QueryString("INCDAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("INCAL")) Then
                CondIncasso = CondIncasso & "AND BOL_BOLLETTE_GEST.DATA_PAGAMENTO <= '" & Request.QueryString("INCAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("VALDAL")) Then
                CondIncasso2 = "AND BOL_BOLLETTE_GEST.DATA_VALUTA >= '" & Request.QueryString("VALDAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("VALAL")) Then
                CondIncasso2 = CondIncasso2 & "AND BOL_BOLLETTE_GEST.DATA_VALUTA <= '" & Request.QueryString("VALAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("TIPO")) Then
                CondTipoDoc = "AND BOL_BOLLETTE_GEST.ID_TIPO IN (" & Request.QueryString("TIPO") & ")"
            End If

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'par.cmd.CommandText = "select TIPO_BOLLETTE_GEST.ACRONIMO,bol_bollette_GEST.*,('<a href=""javascript:window.open(''../Contabilita/DettaglioBolletta.aspx?IDCONT=" & Request.QueryString("IDCONT") & "&IDBOLL='||BOL_BOLLETTE_GEST.ID||'&IDANA=" & Request.QueryString("IDANA") & "'',''Dettagli1'','''');void(0);"">'||BOL_BOLLETTE_GEST.ID||'</a>') as NUM_BOLL from SISCOM_MI.TIPO_BOLLETTE_GEST,siscom_mi.bol_bollette_GEST" _
            '    & " where BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID (+) and bol_bollette_GEST.id_contratto=" & Request.QueryString("IDCONT") _
            '     & condEmissione & " " & CondRiferimento & " " & CondIncasso & " " & CondIncasso2 & " " & CondTipoDoc & " AND TIPO_APPLICAZIONE<>'T' order by bol_bollette_GEST.data_emissione,bol_bollette_GEST.id desc"
            par.cmd.CommandText = "select TIPO_BOLLETTE_GEST.ACRONIMO,bol_bollette_GEST.* from SISCOM_MI.TIPO_BOLLETTE_GEST,siscom_mi.bol_bollette_GEST" _
                & " where BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID (+) AND FL_VISUALIZZABILE=1 and bol_bollette_GEST.id_contratto=" & Request.QueryString("IDCONT") _
                & condEmissione & " " & CondRiferimento & " " & CondIncasso & " " & CondIncasso2 & " " & CondTipoDoc & " order by bol_bollette_GEST.data_emissione,bol_bollette_GEST.id desc"

            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery2 As New Data.DataTable
            'Dim dt1 As New Data.DataTable
            dt1 = New Data.DataTable
            Dim rowDT As System.Data.DataRow

            dt1.Columns.Add("id")
            dt1.Columns.Add("num")
            dt1.Columns.Add("num_boll")
            dt1.Columns.Add("num_tipo")
            dt1.Columns.Add("riferimento_da")
            dt1.Columns.Add("riferimento_a")
            dt1.Columns.Add("data_emissione")
            dt1.Columns.Add("importobolletta")
            dt1.Columns.Add("note")
            dt1.Columns.Add("data_pagamento")
            dt1.Columns.Add("id_tipo")
            dt1.Columns.Add("tipo_applicazione")

            da1.Fill(dtQuery2)
            da1.Dispose()

            Dim TOTimportobolletta As Decimal = 0
            Dim importoVoceEmessa As Decimal = 0
            Dim numero As Integer = 1

            For Each row As Data.DataRow In dtQuery2.Rows
                indiceMorosita = 0
                indiceBolletta = 0
                rowDT = dt1.NewRow()

                importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
                If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "N") = "N" Then
                    importobolletta2 = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
                End If

                'CONTROLLO IN BOL_BOLLETTE_VOCI SE è stata emessa quella bolletta (in bol_bollette)
                par.cmd.CommandText = "SELECT ID AS ID_VOCE_GEST FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & par.IfNull(row.Item("ID"), 0)
                Dim daVociGest As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtVociGest As New Data.DataTable
                Dim rowDTVociGest As System.Data.DataRow
                daVociGest.Fill(dtVociGest)
                daVociGest.Dispose()

                If dtVociGest.Rows.Count > 0 Then
                    For Each rowDTVociGest In dtVociGest.Rows
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE_BOLLETTA_GEST=" & par.IfNull(rowDTVociGest.Item("ID_VOCE_GEST"), 0)
                        Dim daVociNew As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtVociNew As New Data.DataTable
                        Dim rowDTVociNew As System.Data.DataRow
                        daVociNew.Fill(dtVociNew)
                        daVociNew.Dispose()

                        If dtVociNew.Rows.Count > 0 Then
                            For Each rowDTVociNew In dtVociNew.Rows
                                importoVoceEmessa += par.IfNull(rowDTVociNew.Item("IMPORTO"), 0)
                            Next
                        End If
                    Next
                End If

                Dim STATO As String = ""

                residuo = importobolletta - importoVoceEmessa
                TOTimportobolletta = TOTimportobolletta + (importobolletta2 - importoVoceEmessa)

                rowDT.Item("num") = numero & ")"
                'rowDT.Item("NUM_BOLL") = par.IfNull(row.Item("NUM_BOLL"), "")
                rowDT.Item("num_tipo") = par.IfNull(row.Item("ACRONIMO"), "")
                rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
                rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
                rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))
                rowDT.Item("importobolletta") = Format(residuo, "##,##0.00")
                rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
                'rowDT.Item("anteprima") = "<a href='javascript:window.open('''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);'><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

                rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)
                rowDT.Item("id") = par.IfNull(row.Item("id"), 0)

                If rowDT.Item("id_tipo") = "3" Or rowDT.Item("id_tipo") = "4" Then
                    morosita = 1
                End If

                'Select Case par.IfNull(row.Item("id_tipo"), 0)
                '    Case "3"
                '        indiceBolletta = par.IfNull(row.Item("id"), 0)
                '    Case "4"
                '        indiceMorosita = par.IfNull(row.Item("id_morosita"), "")
                '        indiceBolletta = 0
                '    Case "5"
                '        indiceBolletta = par.IfNull(row.Item("id"), 0)
                'End Select

                importobolletta = 0
                importobolletta2 = 0
                rowDT.Item("TIPO_APPLICAZIONE") = row.Item("TIPO_APPLICAZIONE")

                Select Case row.Item("TIPO_APPLICAZIONE")
                    Case "P"
                        If residuo < 0 Then
                            rowDT.Item("NUM_BOLL") = "<a href=""javascript:window.open('../Contratti/DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("ID"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & par.IfNull(row.Item("ID"), 0) & "</a>"
                        Else
                            rowDT.Item("NUM_BOLL") = "<a href=""javascript:window.open('../Contratti/DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("ID"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & par.IfNull(row.Item("ID"), 0) & "</a>"
                        End If
                    Case "T"
                        If residuo < 0 Then
                            rowDT.Item("NUM_BOLL") = "<a href=""javascript:window.open('../Contratti/DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("ID"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & par.IfNull(row.Item("ID"), 0) & "</a>"
                        Else
                            rowDT.Item("NUM_BOLL") = "<a href=""javascript:window.open('../Contratti/DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("ID"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=no,menubar=no,toolbar=no,scrollbars=no');void(0);"">" & par.IfNull(row.Item("ID"), 0) & "</a>"
                        End If
                    Case "N"
                        rowDT.Item("NUM_BOLL") = par.IfNull(row.Item("ID"), 0)
                End Select

                dt1.Rows.Add(rowDT)
                numero = numero + 1
            Next

            rowDT = dt1.NewRow()
            rowDT.Item("id") = -1
            rowDT.Item("num_tipo") = ""
            rowDT.Item("riferimento_da") = ""
            rowDT.Item("riferimento_a") = "TOTALE"
            rowDT.Item("data_emissione") = ""
            rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")

            rowDT.Item("note") = ""
            rowDT.Item("id_tipo") = ""

            dt1.Rows.Add(rowDT)

            DataGridGest.DataSource = dt1
            DataGridGest.DataBind()

            DataGrid1Pdf.DataSource = dt1
            DataGrid1Pdf.DataBind()

            'If morosita = 1 Then
            '    For Each di As DataGridItem In DataGridGest.Items
            '        If di.Cells(11).Text.Contains("3") Or di.Cells(11).Text.Contains("4") Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            '        End If
            '    Next
            'End If
            'If riclass = 1 Then
            '    For Each di As DataGridItem In DataGridGest.Items
            '        If di.Cells(1).Text.Contains("RICLA.") Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            '        End If
            '    Next
            'End If

            For Each di As DataGridItem In DataGridGest.Items
                If di.Cells(5).Text.Contains("TOTALE") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).Font.Underline = True
                        End If
                    Next
                End If
            Next
            For Each di As DataGridItem In DataGridGest.Items
                If di.Cells(10).Text.Contains("T") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(j).Font.Strikeout = True
                    Next
                End If
            Next


            For Each di As DataGridItem In DataGrid1Pdf.Items
                If di.Cells(5).Text.Contains("TOTALE") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).Font.Underline = True
                        End If
                    Next
                End If
            Next
            For Each di As DataGridItem In DataGrid1Pdf.Items
                If di.Cells(10).Text.Contains("T") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(j).Font.Strikeout = True
                    Next
                End If
            Next


            If DataGridGest.Items.Count > 1 Then
                Me.lblInformazione.Text = lblInformazione.Text & "<br/>Le scritture elencate hanno data emissione compresa nelle date dell'estratto conto."
            Else
                Me.lblInformazione.Text = ""
                Me.lblSottoTitolo.Text &= " - NESSUN RISULTATO TROVATO"
            End If

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub

    Private Sub PrintPdf()
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            Me.DataGrid1Pdf.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString
            Html = par.EliminaLink(Html)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 0
            pdfConverter1.PdfDocumentOptions.RightMargin = 0
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            'pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.InternalLinksEnabled = False
            pdfConverter1.PdfFooterOptions.FooterText = ("Estratto Conto " & LblTitolo.Text & " " & Format(Now, "hh:mm") & " - OPERATORE: " & Session.Item("OPERATORE"))
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_EstrattoGest_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFile("<br /><br />Estratto Conto " & LblTitolo.Text & "<br /><br />" & lblSottoTitolo.Text & "<br /><br />" & Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub ImgBtnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnExport.Click
        Dim nomeFile As String = EsportaExcel(dt1, DataGridGest, "ExportGest", , , , False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub

    Private Function EsportaExcel(ByVal dt As Data.DataTable, ByVal datagrid As DataGrid, ByVal nomeFile As String, Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True, Optional ByVal WitdhColum As Decimal = 30, Optional ByVal creazip As Boolean = True, Optional ByVal titolo As String = "") As String
        Try
            'CONTO IL NUMERO DELLE COLONNE DEL DATATABLE
            Dim NumeroColonneDatagrid As Integer = DataGrid.Columns.Count
            'CONTO IL NUMERO DELLE COLONNE VISIBILI DEL DATAGRID
            Dim NumeroColonneVisibiliDatagrid As Integer = 0
            For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
                If DataGrid.Columns.Item(indiceColonna).Visible = True Then
                    NumeroColonneVisibiliDatagrid += 1
                End If
            Next
            'INIZIALIZZAZIONE RIGHE, COLONNE E FILENAME
            Dim FileExcel As New CM.ExcelFile
            Dim indiceRighe As Long = 0
            Dim IndiceColonne As Integer = 1
            Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
            Dim LarghezzaMinimaColonna As Integer = 30
            Dim allineamentoCella As String = "Center"
            Dim LarghezzaDataGrid As Integer = System.Math.Max(datagrid.Width.Value, 200)
            Dim TipoLarghezzaDataGrid As UnitType = DataGrid.Width.Type
            Dim LarghezzaColonnaHeader As Decimal = 0
            Dim LarghezzaColonnaItem As Decimal = 0
            'SETTO A ZERO LA VARIABILE DELLE RIGHE
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE

                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, LblTitolo.Text, 0)
                indiceRighe += 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 2, 1, "", 0)
                indiceRighe += 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 1, lblSottoTitolo.Text, 0)
                indiceRighe += 1


                Dim indiceVisibile As Integer = 1
                IndiceColonne = indiceRighe + 1
                For j = 0 To NumeroColonneDatagrid - 1 Step 1
                    'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                    If DataGrid.Columns.Item(j).Visible = True Then
                        If DataGrid.Columns.Item(j).HeaderStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = DataGrid.Columns.Item(j).HeaderStyle.Width.Value * 1000 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = DataGrid.Columns.Item(j).HeaderStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If
                        If DataGrid.Columns.Item(j).ItemStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = DataGrid.Columns.Item(j).ItemStyle.Width.Value * 1000 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = DataGrid.Columns.Item(j).ItemStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * System.Math.Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, System.Math.Max(LarghezzaMinimaColonna, WitdhColum))
                        'GESTIONE DELLE INTESTAZIONI
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, indiceVisibile, DataGrid.Columns.Item(j).HeaderText, 0)
                        indiceVisibile = indiceVisibile + 1
                    End If
                Next
                indiceRighe = indiceRighe + 1
                For Each riga As Data.DataRow In dt.Rows
                    indiceRighe = indiceRighe + 1
                    Dim Cella As Integer = 0
                    For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                        'RIEPILOGO ALLINEAMENTI
                        'CENTER 2,LEFT 1,RIGHT 3
                        'CONSIDERO DI FORMATO NUMERICO TUTTE LE CELLE CON ALLINEAMENTO A DESTRA
                        If DataGrid.Columns.Item(IndiceColonne).Visible = True Then
                            allineamentoCella = DataGrid.Columns.Item(IndiceColonne).ItemStyle.HorizontalAlign
                            Select Case EliminazioneLink
                                Case False
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.IfNull(riga.Item(IndiceColonne), ""), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.IfNull(riga.Item(IndiceColonne), ""), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.IfNull(riga.Item(IndiceColonne), ""), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.IfNull(riga.Item(IndiceColonne), ""), 0)
                                    End Select
                                Case True
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 0)
                                    End Select
                                Case Else
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(par.IfNull(riga.Item(IndiceColonne), "")), 0)
                                    End Select
                            End Select
                            Cella = Cella + 1
                        End If
                    Next
                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            If creazip = True Then
                'COSTRUZIONE ZIPFILE
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim strFile As String
                strFile = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls")
                Dim strmFile As FileStream = File.OpenRead(strFile)
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
                Dim zipfic As String
                zipfic = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                File.Delete(strFile)
                Dim FileNameZip As String = FileName & ".zip"
                Return FileNameZip
            Else
                Dim FileNameExcel As String = FileName & ".xls"
                Return FileNameExcel
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        PrintPdf()
    End Sub

    Protected Sub DataGridGest_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridGest.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridGest.CurrentPageIndex = e.NewPageIndex
            RiempiDatagridBoll()
        End If
    End Sub
End Class
