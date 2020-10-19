'**********************************************************************22/08/2011**********************************************************************************************
'****************CODICE AGGIORNATO PER NON FAR RIENTRARE NEL DEBITO/CREDITO DELL'INQUILINO LE BOLLETTE DERIVANTI DALLE RATEIZZAZIONI*******************************************
'************************************************per visualizzare le modifiche cerca BOL_BOLLETTE.ID_TIPO <> 5*****************************************************************************
Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Math

Partial Class Contabilita_EstrattoConto
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            Select Case Request.QueryString("ES")
                Case "TRUE"
                    sEsclStornate = " BOL_BOLLETTE.ID_BOLLETTA_STORNO IS NULL AND BOL_BOLLETTE.ID_TIPO<>22 AND "
                Case Else
                    sEsclStornate = ""
            End Select

            If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then
                CaricaSaldoContabile()
                CaricaImportiPeriodo()
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

    Public Property sEsclStornate() As String
        Get
            If Not (ViewState("par_sEsclStornate") Is Nothing) Then
                Return CStr(ViewState("par_sEsclStornate"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sEsclStornate") = value
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

    Private Function ImpostaFlSgravio(ByVal idBollRuolo As Long, ByVal importoRuolo As Decimal) As String
        Dim tiposgravio As String = "N"
        Dim impPagatoRuolo As Decimal = 0
        Dim flSgravio As Integer = 0

        par.cmd.CommandText = "select * from siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO,siscom_mi.incassi_ruoli where " _
                & " BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO=incassi_ruoli.id and id_bolletta=" & idBollRuolo
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt1R As New Data.DataTable
        da1.Fill(dt1R)
        da1.Dispose()
        If dt1R.Rows.Count > 0 Then
            For Each rowRuolo As Data.DataRow In dt1R.Rows
                impPagatoRuolo = impPagatoRuolo + par.IfNull(rowRuolo.Item("importo_pagato"), 0)
                If par.IfNull(rowRuolo.Item("fl_sgravio"), 0) = 1 Then
                    flSgravio = 1
                End If
            Next
        End If

        If flSgravio = 1 Then
            If importoRuolo = impPagatoRuolo Then
                tiposgravio = "ST"
            Else
                tiposgravio = "SP"
            End If
        Else
            tiposgravio = "N"
        End If

        Return tiposgravio
    End Function

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
            Dim CondEmissione As String = ""

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondCompetenza = "AND BOL_BOLLETTE.RIFERIMENTO_DA >= " & Request.QueryString("RIFDAL") & ""
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondCompetenza = CondCompetenza & "AND BOL_BOLLETTE.RIFERIMENTO_A <= " & Request.QueryString("RIFAL") & ""
            End If
            'Setto date periodo
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))


            If Not String.IsNullOrEmpty(DataDal) Then
                CondEmissione = "AND BOL_BOLLETTE.data_emissione >= '" & DataDal & "'"
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                CondEmissione = CondEmissione & "AND BOL_BOLLETTE.data_emissione <= " & DataAl & ""
            End If

            Me.lblInformazione.Text = "* = Bollette pagate successivamente al " & par.FormattaData(DataAl)
            Me.lblInformazione.Text = lblInformazione.Text & "<br/>Le bollette elencate hanno data emissione compresa nelle date dell'estratto conto."
            Dim sValore As String
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader

            '***********DOVUTO RELATIVO ALLA PRIMA DATA*****************
            If (par.IfEmpty(DataDal, "Null") <> "Null") Then
                'sValore = DataDal
                'sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE >= " & sValore

                sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS IMPORTO, " _
                            & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO " _
                            & "FROM SISCOM_MI.BOL_BOLLETTE " _
                            & "WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                            & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & CondCompetenza & " " & CondEmissione
                par.cmd.CommandText = sStringaSQL
                myReader = par.cmd.ExecuteReader()
                'TOTALE DEL DEBITO A CARICO DI UN UTENTE
                If myReader.Read Then
                    DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
                    PAGATO = par.IfNull(myReader("PAGATO"), 0)
                End If
                myReader.Close()
                sStringaSQL = ""

                'Me.LblSaldoInizio.Text = "<span class='ttip' onmouseover=if(t1)t1.Show(event,sinizio) onmouseout=if(t1)t1.Hide(event)>Saldo contabile di quanto dovuto all'inizio del periodo pari a: &euro;." & Format(DOVUTO - PAGATO, "##,##0.00") & "</span>"
                Me.txtsldinizio.Value = "Saldo contabile di quanto dovuto all'inizio del periodo pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00")
            Else
                'LblSaldoInizio.Text = ""
                Me.txtsldinizio.Value = ""
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

            Dim strRuolo As String = ""
            Dim sStringaSQL1 As String = ""
            sStringaSQL1 = "SELECT sum(NVL(IMPORTO_RUOLO,0)) as ruolo " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza & CondEmissione
            par.cmd.CommandText = sStringaSQL1
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                strRuolo = " di cui a ruolo €." & Format(par.IfNull(myReader("RUOLO"), 0), "##,##0.00")
            End If
            myReader.Close()

            Dim strIngiunz As String = ""
            Dim sStringaSQL2 As String = ""
            sStringaSQL2 = "SELECT sum(NVL(IMPORTO_INGIUNZIONE,0)) as INGIUNZIONE " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza & CondEmissione
            par.cmd.CommandText = sStringaSQL2
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                strIngiunz = " di cui ingiunto €." & Format(par.IfNull(myReader("INGIUNZIONE"), 0), "##,##0.00")
            End If
            myReader.Close()



            sStringaSQL = ""
            par.cmd.CommandText = ""
            DOVUTO = 0
            PAGATO = 0

            If (par.IfEmpty(DataAl, "Null") <> "Null") Then
                sValore = DataAl
                sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
            End If
            sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS IMPORTO, " _
                        & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS PAGATO " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " " & CondCompetenza & CondEmissione
            par.cmd.CommandText = sStringaSQL
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
                PAGATO = par.IfNull(myReader("PAGATO"), 0)
            End If
            myReader.Close()
            sStringaSQL = ""


            If Not String.IsNullOrEmpty(DataDal) And Not String.IsNullOrEmpty(DataAl) Then
                Me.LblSaldoFine.Text = "<span class='ttip' onmouseover=if(t1)t1.Show(event,sfine) onmouseout=if(t1)t1.Hide(event)>Saldo contabile di quanto dovuto dal " & par.FormattaData(DataDal) & " al " & par.FormattaData(DataAl) & " pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00") & strRuolo & strIngiunz & "</span>"
                Me.txtsldfine.Value = "Saldo contabile di quanto dovuto dal " & par.FormattaData(DataDal) & " al " & par.FormattaData(DataAl) & " pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00") & strRuolo & strIngiunz
            End If
            If Not String.IsNullOrEmpty(DataDal) And String.IsNullOrEmpty(DataAl) Then
                Me.LblSaldoFine.Text = "<span class='ttip' onmouseover=if(t1)t1.Show(event,sfine) onmouseout=if(t1)t1.Hide(event)>Saldo contabile di quanto dovuto dal " & par.FormattaData(DataDal) & " pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00") & strRuolo & strIngiunz & "</span>"
                Me.txtsldfine.Value = "Saldo contabile di quanto dovuto dal " & par.FormattaData(DataDal) & " pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00") & strRuolo & strIngiunz
            End If
            If Not String.IsNullOrEmpty(DataAl) And String.IsNullOrEmpty(DataDal) Then
                Me.LblSaldoFine.Text = "<span class='ttip' onmouseover=if(t1)t1.Show(event,sfine) onmouseout=if(t1)t1.Hide(event)>Saldo contabile di quanto dovuto al " & par.FormattaData(DataAl) & " pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00") & strRuolo & strIngiunz & "</span>"
                Me.txtsldfine.Value = "Saldo contabile di quanto dovuto al " & par.FormattaData(DataAl) & " pari a: €." & Format(DOVUTO - PAGATO, "##,##0.00") & strRuolo & strIngiunz
            End If


            Me.lblSaldoCont.Text = "al " & Format(Now, "dd/MM/yyyy") & " €." & Format(par.CalcolaSaldoAttuale(Request.QueryString("IDCONT")), "##,##0.00")
            sStringaSQL = "SELECT sum(NVL(IMPORTO_RUOLO,0)) as ruolo " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT")
            par.cmd.CommandText = sStringaSQL
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.lblImpRuolo.Text = "€." & Format(par.IfNull(myReader("RUOLO"), 0), "##,##0.00")
            End If
            myReader.Close()

            sStringaSQL = "SELECT sum(NVL(IMPORTO_INGIUNZIONE,0)) as INGIUNZIONE " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE " _
                        & "WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) " _
                        & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT")
            par.cmd.CommandText = sStringaSQL
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.lblImpIng.Text = "€." & Format(par.IfNull(myReader("INGIUNZIONE"), 0), "##,##0.00")
            End If
            myReader.Close()

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

            Me.lblSottoTitolo.Text = "Saldo contabile per data EMISSIONE"
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
    Private Sub CaricaImportiPeriodo()

        Dim testoTabella As String = ""
        Dim Nbolletta As String = ""
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader
        Dim COLORE As String = "#E6E6E6"
        Dim Contatore As Integer = 0
        Dim DataDal As String
        Dim DataAl As String = ""
        Dim GiorniRitardo As String = ""
        '*********VARIABILI APPOGGIO IMPORTI
        Dim Importo As Decimal = 0
        Dim Riclassificate As Decimal = 0
        Dim ImpContabile As Decimal = 0
        Dim ImpIncassato As Decimal = 0
        Dim ImpSaldo As Decimal = 0
        '*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/

        '*********VARIABILI PER I TOTALI
        Dim TotImporto As Decimal = 0
        Dim TotRiclassificate As Decimal = 0
        Dim TotImpContabile As Decimal = 0
        Dim TotImpIncassato As Decimal = 0
        Dim TotImpSaldoContabile As Decimal = 0
        '*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/


        Dim CondBollAnnullate As String = "(BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL ))"

        '******APERTURA CONNESSIONE*****
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Try
            '**************Setto date periodo
            DataDal = Request.QueryString("DAL")
            DataAl = par.IfEmpty(Request.QueryString("AL"), Format(Date.Now, "yyyyMMdd"))

            '**************COLONNE DELLA TABELLA
            testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                            & "<tr>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span class='ttip' onmouseover=if(t1)t1.Show(event,numBol) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>NUM.BOL.</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span class='ttip' onmouseover=if(t1)t1.Show(event,numRata) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>NUM.RATA</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span class='ttip' onmouseover=if(t1)t1.Show(event,tipoR) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:center'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,riferimento) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,emissione) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,scadenza) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,bollettato) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>BOLLETTATO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,riclass) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>IMP.RICLASSIFICATO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,impCont) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>IMPORTO CONTABILE</strong></span></td>" _
                            & "<td style='height: 19px;text-align:center'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,dataPag) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,impIncassato) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>IMPORTO INCASSATO</strong></span></td>" _
                            & "<td style='height: 19px;text-align:right'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,saldoContab) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>SALDO CONTABILE</strong></span></td>" _
                            & "<td style='height: 19px;text-align:center'>" _
                            & "<span onmouseover=if(t1)t1.Show(event,gRitardo) onmouseout=if(t1)t1.Hide(event) style='font-size: 8pt; font-family: Arial'><strong>GIORNI RITARDO</strong></span></td>" _
                            & "</tr>"

            '**************SELEZIONO LE INFORMAZIONI RICHIESTE PER LA VISUALIZZAZIONE DEI DATI


            par.cmd.CommandText = "SELECT BOL_BOLLETTE.*, " _
                                & "(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, " _
                                & "(CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE  " & sEsclStornate & " (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND " _
                                & "TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND BOL_BOLLETTE.DATA_EMISSIONE >= '" & DataDal & "' AND BOL_BOLLETTE.DATA_EMISSIONE <= '" & DataAl & "' " _
                                & " ORDER BY BOL_BOLLETTE.data_emissione DESC"


            'par.cmd.CommandText = "SELECT NUM_BOLLETTA, BOL_BOLLETTE.ID,N_RATA,DATA_EMISSIONE, DATA_SCADENZA, DATA_PAGAMENTO, IMPORTO_PAGATO,ANNO,FL_ANNULLATA, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO, ID_BOLLETTA_RIC " _
            '                    & ",(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento,BOL_BOLLETTE.IMPORTO_TOTALE " _
            '                    & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.TIPO_BOLLETTE WHERE " & CondBollAnnullate & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO " _
            '                    & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND BOL_BOLLETTE.DATA_EMISSIONE >= " & DataDal & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & DataAl & " AND BOL_BOLLETTE.ID_TIPO <> 5  ORDER BY BOL_BOLLETTE.ID DESC  "
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read

                '********************SEZIONE AZZERA VARIABILI************************
                Nbolletta = ""

                Importo = 0
                Riclassificate = 0
                ImpContabile = 0
                ImpIncassato = 0
                ImpSaldo = 0
                GiorniRitardo = ""

                '*/*/*/*/*/*/*/*/*/*FINE SEZIONE AZZERA VARIABILI*/*/*/*/*/*/*/*/*/*

                If par.IfNull(myReader("N_RATA"), "") = "99" Then
                    Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                    Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                    Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                Else
                    Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                End If

                If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
                    COLORE = "#FF1800"
                End If

                Contatore = Contatore + 1


                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>"



                '**********SCRITTURA DATI INFORMATIVI DELLA BOLLETTA
                If DataAl < par.IfNull(myReader("DATA_PAGAMENTO"), 0) Then
                    testoTabella = testoTabella & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a>   *</span></td>"
                Else
                    testoTabella = testoTabella & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a>    </span></td>"
                End If
                testoTabella = testoTabella & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
                                            & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>" _
                                            & "<td style='height: 19px;text-align:center;'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                                            & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                                            & "<td style='height: 19px'>" _
                                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>"

                '*/*/*/*/*/*/*/*/*/*/*/*/FINE SCRITTURA DATI INFORMATIVI DELLA BOLLETTA


                '**************************SCRITTURA COLONNE IMPORTO + DATA PAGAMENTO


                '********************IMPORTO TOTALE DELLA BOLLETTA*********************************
                Importo = par.IfNull(myReader("IMPORTO_TOTALE"), 0)
                TotImporto = TotImporto + Importo
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Importo, "##,##0.00") & "</span></td>"

                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA MOROSITA (dovrebbe essere rinominata RICLASSIFICATE)***************************
                'par.cmd.CommandText = "SELECT SUM(IMPORTO) AS TOT_RIC FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & "  AND ID_VOCE IN (150,151) "
                'lettore2 = par.cmd.ExecuteReader
                'If lettore2.Read Then
                Riclassificate = par.IfNull(myReader("IMPORTO_RIC_B"), 0)
                'End If
                'lettore2.Close()
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(Riclassificate, "##,##0.00") & "</span></td>"

                TotRiclassificate = TotRiclassificate + Riclassificate
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA IMPORTO CONTABILE********************************************

                'par.cmd.CommandText = "SELECT SUM(IMPORTO) AS TOT_CONTABILE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA <> 3  AND ID_VOCE not in ( 150,151) "
                'lettore2 = par.cmd.ExecuteReader
                'If lettore2.Read Then
                '    ImpContabile = par.IfNull(lettore2("TOT_CONTABILE"), 0)

                'End If
                'lettore2.Close()
                ImpContabile = par.IfNull(myReader("IMPORTO_TOTALE"), 0) - par.IfNull(myReader("IMPORTO_RIC_B"), 0) - par.IfNull(myReader("QUOTA_SIND_B"), 0) '
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(ImpContabile, "##,##0.00") & "</span></td>"

                TotImpContabile = TotImpContabile + ImpContabile
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '**********************DATA PAGAMENTO *******************************************
                testoTabella = testoTabella _
                & "<td style='height: 19px;text-align:center'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>"
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


                '***********************COLONNA IMPORTO INCASSATO********************************************
                'par.cmd.CommandText = "SELECT SUM(IMP_PAGATO) AS TOT_INCASSATO FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA <> 3  AND ID_VOCE not in ( 150,151) "
                'lettore2 = par.cmd.ExecuteReader
                'If lettore2.Read Then
                '    ImpIncassato = par.IfNull(lettore2("TOT_INCASSATO"), 0)
                'End If
                'lettore2.Close()
                ImpIncassato = (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0))
                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(ImpIncassato, "##,##0.00") & "</span></td>"

                TotImpIncassato = TotImpIncassato + ImpIncassato
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA SALDO CONTABILE********************************************

                ImpSaldo = ImpContabile - ImpIncassato
                TotImpSaldoContabile = TotImpSaldoContabile + ImpSaldo
                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

                '***********************COLONNA GIORNI DI RITARDO********************************************
                If myReader("FL_ANNULLATA") = 0 Then
                    If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then
                        'DIFFERENZA FRA DATA SCADENZA E DATA PAGAMENTE
                        GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), CDate(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))))
                    Else
                        'DIFFERENZA FRA DATA SCADENZA E OGGI
                        GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), Date.Now)
                    End If

                    If GiorniRitardo < 0 Then
                        GiorniRitardo = ""
                    End If
                Else
                    GiorniRitardo = "ANNULLATA"
                End If

                '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*



                testoTabella = testoTabella _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfEmpty(ImpSaldo, 0), "##,##0.00") & "</span></td>" _
                            & "<td style='height: 19px; text-align:right'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>" _
                            & "</tr>"



                If COLORE = "#FFFFFF" Then
                    COLORE = "#E6E6E6"
                Else
                    COLORE = "#FFFFFF"
                End If

            Loop
            myReader.Close()


            '*************************************RIGA DEI TOTLAI**************************************************************************************************************
            testoTabella = testoTabella _
                                    & "<tr>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong> €." & Format(par.IfEmpty(TotImporto, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong> €." & Format(par.IfEmpty(TotRiclassificate, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(par.IfEmpty(TotImpContabile, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(par.IfEmpty(TotImpIncassato, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px; text-align:right'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(par.IfEmpty(TotImpSaldoContabile, 0), "##,##0.00") & "</strong></span></td>" _
                                    & "<td style='height: 19px'>" _
                                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                                    & "</tr>"

            'Me.TBL_ESTRATTO_CONTO.Text = testoTabella & "</table>"

            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*

            '************************************BOLLETTE ANNULLATE*****************************************
            testoTabella = ""
            Contatore = 0


            par.cmd.CommandText = "SELECT ID,DATA_EMISSIONE, DATA_SCADENZA, DATA_PAGAMENTO, IMPORTO_PAGATO,DATA_ANNULLO " _
                                & ",(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE  " & sEsclStornate & " ( FL_ANNULLATA='1' OR FL_ANNULLATA='2') AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO " _
                                & "AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND BOL_BOLLETTE.DATA_EMISSIONE >= '" & DataDal & "' AND BOL_BOLLETTE.ID_TIPO <> 5  AND BOL_BOLLETTE.DATA_EMISSIONE <= '" & DataAl & "'  "
            myReader = par.cmd.ExecuteReader()

            testoTabella = "<table cellpadding='0' cellspacing='0' width='60%'>" & vbCrLf _
                        & "<tr>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.BOLLETTA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                        & "<td style='height: 19px'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:right'>" _
                        & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA ANNULLO</strong></span></td>" _
                        & "<td style='height: 19px;text-align:center'>" _
                        & "</tr>"

            TotImporto = 0

            Do While myReader.Read

                Contatore = Contatore + 1

                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                                        & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</a></span></td>" _
                & "<td style='height: 19px;text-align:center;'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>"

                par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "")
                lettore2 = par.cmd.ExecuteReader

                If lettore2.Read Then
                    testoTabella = testoTabella _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(lettore2("IMPORTO"), 0), "##,##0.00") & "</span></td>"
                    TotImporto = TotImporto + par.IfNull(lettore2("IMPORTO"), "")
                End If
                lettore2.Close()
                testoTabella = testoTabella _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_ANNULLO"), "")) & "</span></td>"



                testoTabella = testoTabella & "</tr>"


                If COLORE = "#FFFFFF" Then
                    COLORE = "#E6E6E6"
                Else
                    COLORE = "#FFFFFF"
                End If
            Loop
            myReader.Close()

            If TotImporto > 0 Then

                testoTabella = testoTabella _
                & "<tr bgcolor = '" & COLORE & "'>" _
                                        & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
                & "<td style='height: 19px'>" _
                & "<span style='font-size: 8pt; font-family: Arial'></span></td>"


                testoTabella = testoTabella _
                & "<td style='height: 19px; text-align:right'>" _
                & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(TotImporto, 0), "##,##0.00") & "</span></td>"



                testoTabella = testoTabella _
                    & "<td style='height: 19px; text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>"



                testoTabella = testoTabella & "</tr>"

                Me.TBL_ANNULLATE.Text = testoTabella & "</table>"
            Else
                Me.TBL_ANNULLATE.Text = ""
                TBL_ANNULLATE0.Text = ""
            End If

            '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/**/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*


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

    Private Sub RiempiDatagridBoll()
        Try
            Dim num_bolletta As String = ""
            Dim I As Integer = 0
            Dim importobolletta As Decimal = 0
            Dim importopagato As Decimal = 0
            Dim residuo As Decimal = 0
            Dim morosita As Integer = 0
            Dim riclass As Integer = 0
            Dim indiceMorosita As Integer = 0
            Dim indiceBolletta As Integer = 0
            Dim storno As Integer = 0

            Dim condEmissione As String = ""
            Dim CondRiferimento As String = ""
            Dim CondIncasso As String = ""

            If Not String.IsNullOrEmpty(DataDal) Then
                condEmissione = " AND BOL_BOLLETTE.DATA_EMISSIONE >= '" & DataDal & "' "
            End If
            If Not String.IsNullOrEmpty(DataAl) Then
                condEmissione = condEmissione & " AND BOL_BOLLETTE.DATA_EMISSIONE <= '" & DataAl & "'"
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("RIFDAL")) Then
                CondRiferimento = "AND BOL_BOLLETTE.RIFERIMENTO_DA >= '" & Request.QueryString("RIFDAL") & "' "
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("RIFAL")) Then
                CondRiferimento = CondRiferimento & "AND BOL_BOLLETTE.RIFERIMENTO_A <= '" & Request.QueryString("RIFAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("INCDAL")) Then
                CondIncasso = "AND BOL_BOLLETTE.DATA_PAGAMENTO >= '" & Request.QueryString("INCDAL") & "'"
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("INCAL")) Then
                CondIncasso = CondIncasso & "AND BOL_BOLLETTE.DATA_PAGAMENTO <= '" & Request.QueryString("INCAL") & "'"
            End If

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select TIPO_BOLL_INGIUNZIONE.DESCRIZIONE AS INGIUNZIONE,TIPO_BOLLETTE.ACRONIMO,bol_bollette.*,('<a href=""javascript:window.open(''../Contabilita/DettaglioBolletta.aspx?IDCONT=" & Request.QueryString("IDCONT") & "&IDBOLL='||BOL_BOLLETTE.ID||'&IDANA=" & Request.QueryString("IDANA") & "'',''Dettagli1'','''');void(0);"">'||BOL_BOLLETTE.NUM_BOLLETTA||'</a>') as NUM_BOLL" _
                & " from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette,siscom_mi.TIPO_BOLL_INGIUNZIONE where " & sEsclStornate & " BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND TIPO_BOLL_INGIUNZIONE.ID(+)=ID_TIPO_INGIUNZIONE and bol_bollette.id_contratto=" & Request.QueryString("IDCONT") _
                & " and (bol_bollette.fl_annullata = 0 OR (bol_bollette.fl_annullata <> 0 AND data_pagamento IS NOT NULL)) " & " " & condEmissione & " " & CondRiferimento & " " & CondIncasso & " ORDER BY BOL_BOLLETTE.data_emissione DESC,RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC,DATA_SCADENZA DESC"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable
            dt1 = New Data.DataTable
            Dim rowDT As System.Data.DataRow

            dt1.Columns.Add("id")
            dt1.Columns.Add("num")
            dt1.Columns.Add("num_boll")
            dt1.Columns.Add("num_tipo")
            dt1.Columns.Add("riferimento_da")
            dt1.Columns.Add("riferimento_a")
            dt1.Columns.Add("data_emissione")
            dt1.Columns.Add("data_scadenza")
            dt1.Columns.Add("importo_totale")
            dt1.Columns.Add("importobolletta")
            dt1.Columns.Add("imp_pagato")
            dt1.Columns.Add("imp_residuo")
            dt1.Columns.Add("data_pagamento")
            dt1.Columns.Add("fl_mora")
            dt1.Columns.Add("fl_rateizz")
            dt1.Columns.Add("importo_ruolo")
            dt1.Columns.Add("imp_ruolo_pagato")
            dt1.Columns.Add("sgravio")
            dt1.Columns.Add("ingiunzione")
            dt1.Columns.Add("importo_ingiunzione")
            dt1.Columns.Add("imp_ingiunzione_pag")
            dt1.Columns.Add("note")
            dt1.Columns.Add("id_tipo")


            da1.Fill(dtQuery)
            da1.Dispose()

            Dim TOTimportobolletta As Decimal = 0
            Dim TOTimportopagato As Decimal = 0
            Dim TOTimportoresiduo As Decimal = 0
            Dim TOTImpRuolo As Decimal = 0
            Dim TOTImpRuoloPag As Decimal = 0
            Dim TOTimportoEmesso As Decimal = 0
            Dim numero As Integer = 1
            Dim TOTImpIngPag As Decimal = 0
            Dim TOTImpIng As Decimal = 0
            For Each row As Data.DataRow In dtQuery.Rows
                indiceMorosita = 0
                indiceBolletta = 0
                rowDT = dt1.NewRow()

                Select Case par.IfNull(row.Item("n_rata"), "")
                    Case "99" 'bolletta manuale
                        num_bolletta = "MA"
                    Case "999" 'bolletta automatica
                        num_bolletta = "AU"
                    Case "99999" 'bolletta di conguaglio
                        num_bolletta = "CO"
                    Case Else
                        num_bolletta = Format(par.IfNull(row.Item("n_rata"), "??"), "00")
                End Select

                importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_B"), 0) '


                TOTimportobolletta = TOTimportobolletta + importobolletta


                importopagato = (par.IfNull(row.Item("IMPORTO_PAGATO"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), 0))
                TOTimportopagato = TOTimportopagato + importopagato

                Dim STATO As String = ""
                If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> "0" Then
                    STATO = "ANNUL."
                Else
                    STATO = "VALIDA"
                End If
                If par.IfNull(row.Item("id_bolletta_ric"), "0") <> "0" Or par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
                    STATO = "RICLA."
                    riclass = 1
                End If

                If par.IfNull(row.Item("id_bolletta_storno"), "0") <> "0" Then
                    STATO = "STORN."
                End If

                residuo = importobolletta - importopagato
                TOTimportoresiduo = TOTimportoresiduo + residuo
                'Select Case par.IfNull(row.Item("ID_TIPO"), "0")
                '    Case "3"
                '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                '    Case "4"
                '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                'End Select
                rowDT.Item("NUM") = numero & ")"
                rowDT.Item("NUM_BOLL") = par.IfNull(row.Item("NUM_BOLL"), "")
                rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "")
                rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
                rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
                rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))
                rowDT.Item("data_scadenza") = par.FormattaData(par.IfNull(row.Item("data_scadenza"), ""))
                rowDT.Item("importo_totale") = Format(par.IfNull(row.Item("importo_totale"), 0), "##,##0.00")
                rowDT.Item("importobolletta") = Format(importobolletta, "##,##0.00")
                rowDT.Item("imp_pagato") = Format(importopagato, "##,##0.00")
                rowDT.Item("imp_residuo") = Format(residuo, "##,##0.00")
                rowDT.Item("data_pagamento") = par.FormattaData(par.IfNull(row.Item("data_pagamento"), ""))
                rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
                rowDT.Item("importo_ruolo") = Format(par.IfNull(row.Item("importo_ruolo"), 0), "##,##0.00")
                rowDT.Item("imp_ruolo_pagato") = Format(par.IfNull(row.Item("imp_ruolo_pagato"), 0), "##,##0.00")
                rowDT.Item("importo_ingiunzione") = Format(par.IfNull(row.Item("importo_ingiunzione"), 0), "##,##0.00")
                rowDT.Item("imp_ingiunzione_pag") = Format(par.IfNull(row.Item("imp_ingiunzione_pag"), 0), "##,##0.00")

                If par.IfNull(row.Item("imp_ruolo_pagato"), 0) > 0 Then
                    rowDT.Item("sgravio") = ImpostaFlSgravio(par.IfNull(row.Item("ID"), 0), CDec(par.IfNull(row.Item("IMPORTO_RUOLO"), 0)))
                Else
                    rowDT.Item("sgravio") = "N"
                End If
                rowDT.Item("INGIUNZIONE") = par.IfNull(row.Item("INGIUNZIONE"), "")
                TOTImpRuolo = TOTImpRuolo + rowDT.Item("importo_ruolo")
                TOTimportoEmesso = TOTimportoEmesso + rowDT.Item("importo_totale")
                TOTImpRuoloPag = TOTImpRuoloPag + rowDT.Item("imp_ruolo_pagato")
                TOTImpIng = TOTImpIng + rowDT.Item("importo_ingiunzione")
                TOTImpIngPag = TOTImpIngPag + rowDT.Item("imp_ingiunzione_pag")
                'If par.IfNull(row.Item("ID_TIPO"), "0") = 4 Then
                '    rowDT.Item("fl_mora") = "<a href='javascript:apriMorosita();void(0);'>SI</a>"
                'Else
                '    rowDT.Item("fl_mora") = "NO"
                'End If
                If par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
                    rowDT.Item("fl_rateizz") = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratti/DettRateizz.aspx?IDB=" & par.IfNull(row.Item("id"), 0) & "', '', 'height=550,top=0,left=0,width=800');" & Chr(34) & ">SI</a>"
                Else
                    rowDT.Item("fl_rateizz") = "NO"
                End If

                indiceMorosita = par.IfNull(row.Item("id_morosita"), 0)

                If indiceMorosita <> 0 Then
                    rowDT.Item("fl_mora") = "<a href='javascript:apriMorosita();void(0);'>SI</a>"
                Else
                    rowDT.Item("fl_mora") = "NO"
                End If

                rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)

                rowDT.Item("id") = par.IfNull(row.Item("id"), 0)

                If rowDT.Item("id_tipo") = "3" Or rowDT.Item("id_tipo") = "4" Then
                    morosita = 1
                End If

                Select Case par.IfNull(row.Item("id_tipo"), 0)
                    Case "3"
                        indiceBolletta = par.IfNull(row.Item("id"), 0)
                    Case "4"
                        indiceMorosita = par.IfNull(row.Item("id_morosita"), "")
                        indiceBolletta = 0
                    Case "5"
                        indiceBolletta = par.IfNull(row.Item("id"), 0)
                    Case "22"
                        storno = 1
                End Select

                If par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
                    indiceBolletta = par.IfNull(row.Item("id"), 0)
                End If

                If par.IfNull(row.Item("id_rateizzazione"), 0) <> 0 Then
                    indiceBolletta = par.IfNull(row.Item("id"), 0)
                End If

                'If par.IfNull(row.Item("ID_TIPO"), 0) = 22 Then
                '    DataGridContab.Columns(2).Visible = True
                '    rowDT.Item("dettagli") = "<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                'End If

                'If indiceBolletta = 0 Then
                '    rowDT.Item("dettagli") = ""
                'Else
                '    DataGridContab.Visible = True
                '    rowDT.Item("dettagli") = "<a href=javascript:apriMorosita();void(0); onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                'End If

                'rowDT.Item("anteprima") = "<a href=javascript:ApriAnteprima();void(0); onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

                dt1.Rows.Add(rowDT)
                numero = numero + 1
            Next

            rowDT = dt1.NewRow()
            rowDT.Item("id") = -1
            rowDT.Item("num_tipo") = ""
            rowDT.Item("riferimento_da") = ""
            rowDT.Item("riferimento_a") = "TOTALE"
            rowDT.Item("data_emissione") = ""
            rowDT.Item("data_scadenza") = ""
            rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
            rowDT.Item("imp_pagato") = Format(TOTimportopagato, "##,##0.00")
            rowDT.Item("imp_residuo") = Format(TOTimportoresiduo, "##,##0.00")
            rowDT.Item("importo_totale") = Format(TOTimportoEmesso, "##,##0.00")
            rowDT.Item("importo_ruolo") = Format(TOTImpRuolo, "##,##0.00")
            rowDT.Item("imp_ruolo_pagato") = Format(TOTImpRuoloPag, "##,##0.00")
            rowDT.Item("importo_ingiunzione") = Format(TOTImpIng, "##,##0.00")
            rowDT.Item("imp_ingiunzione_pag") = Format(TOTImpIngPag, "##,##0.00")
            rowDT.Item("data_pagamento") = ""
            rowDT.Item("note") = ""
            rowDT.Item("fl_mora") = ""
            rowDT.Item("fl_rateizz") = ""
            rowDT.Item("id_tipo") = ""
            dt1.Rows.Add(rowDT)

            DataGridContab.DataSource = dt1
            DataGrid1Pdf.DataSource = dt1
            'Session.Add("dtExcel", dt1)
            DataGridContab.DataBind()
            DataGrid1Pdf.DataBind()

            'If morosita = 1 Then
            '    For Each di As DataGridItem In DataGridContab.Items
            '        If di.Cells(par.IndDGC(DataGridContab, "ID_TIPO")).Text.Contains("3") Or di.Cells(par.IndDGC(DataGridContab, "ID_TIPO")).Text.Contains("4") Then
            '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
            '        End If
            '    Next
            'End If
            If riclass = 1 Then
                For Each di As DataGridItem In DataGridContab.Items
                    If di.Cells(par.IndDGC(DataGridContab, "num_tipo")).Text.Contains("RICLA.") Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("blue")
                    End If
                Next
            End If
            If storno = 1 Then
                For Each di As DataGridItem In DataGridContab.Items
                    If di.Cells(par.IndDGC(DataGridContab, "ID_TIPO")).Text = "22" Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    End If
                    If di.Cells(par.IndDGC(DataGridContab, "num_tipo")).Text.Contains("STORN.") Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    End If
                    If di.Cells(par.IndDGC(DataGridContab, "num_tipo")).Text.Contains("ANNUL.") Then
                        di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    End If
                Next
            End If

            For Each di As DataGridItem In DataGridContab.Items
                If di.Cells(par.IndDGC(DataGridContab, "RIFERIMENTO_A")).Text.Contains("TOTALE") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).Font.Underline = True
                        End If
                    Next
                    'di.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
                    'di.ForeColor = Drawing.ColorTranslator.FromHtml("white")
                    'di.Attributes.Add("onmouseover", "this.style.backgroundColor='#006699'")
                    'di.Attributes.Add("onmouseout", "this.style.backgroundColor='#006699'")
                End If
            Next

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

    ' '' ''Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    ' '' ''    If Session.Item("OPERATORE") = "" Then
    ' '' ''        Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
    ' '' ''    End If
    ' '' ''    Try
    ' '' ''        If Not IsPostBack Then

    ' '' ''            If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then
    ' '' ''                '******APERTURA CONNESSIONE*****
    ' '' ''                If par.OracleConn.State = Data.ConnectionState.Closed Then
    ' '' ''                    par.OracleConn.Open()
    ' '' ''                    par.SettaCommand(par)
    ' '' ''                End If

    ' '' ''                Dim DataDal As String
    ' '' ''                Dim DataAl As String
    ' '' ''                Dim sStringaSQL As String = ""
    ' '' ''                Dim testoTabella As String = ""
    ' '' ''                Dim TotImporto As Double = 0
    ' '' ''                Dim TotMorosita As Double = 0
    ' '' ''                Dim TotImportoIncassato As Double = 0
    ' '' ''                Dim TotSaldoContabile As Double = 0
    ' '' ''                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
    ' '' ''                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader
    ' '' ''                Dim ImpDovuto As Double = 0
    ' '' ''                Dim COLORE As String = "#E6E6E6"
    ' '' ''                Dim ImportoContabile As Double = 0
    ' '' ''                Dim ToTContabile As Double = 0
    ' '' ''                Dim importobolletta As Double = 0
    ' '' ''                Dim SaldoContabile As Decimal = 0
    ' '' ''                Dim Contatore As Integer = 0
    ' '' ''                Dim Riclassificate As Decimal = 0
    ' '' ''                Dim DOVUTO As Double = 0
    ' '' ''                DataDal = Request.QueryString("DAL")
    ' '' ''                Dim ImpIncassato As Decimal = 0
    ' '' ''                Dim QSindacali As Decimal = 0


    ' '' ''                DataAl = par.IfEmpty(Request.QueryString("AL"), par.AggiustaData(Format(Date.Now, "dd/MM/yyyy")))
    ' '' ''                Me.lblInformazione.Text = "* = Bollette pagate successivamente al " & par.FormattaData(DataAl)
    ' '' ''                Dim sValore As String
    ' '' ''                Dim CondBollAnnullate As String = "(BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL ))"

    ' '' ''                '***********DOVUTO RELATIVO ALLA PRIMA DATA*****************
    ' '' ''                If (par.IfEmpty(DataDal, "Null") <> "Null") Then
    ' '' ''                    sValore = DataDal
    ' '' ''                    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
    ' '' ''                End If

    ' '' ''                '' ''MODIFY 21/04/2011 PER RECUPERO INFORMAZIONI SINGOLO CONTRATTO
    ' '' ''                '' ''sStringaSQL = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " " & sStringaSQL & " )"
    ' '' ''                '' ''sStringaSQL = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " " & sStringaSQL & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " )"
    ' '' ''                sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0)-NVL(IMPORTO_RIC,0)) AS IMPORTO " _
    ' '' ''                                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
    ' '' ''                                    & "WHERE FL_ANNULLATA = 0 " _
    ' '' ''                                    & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID " _
    ' '' ''                                    & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL

    ' '' ''                par.cmd.CommandText = sStringaSQL
    ' '' ''                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    ' '' ''                'TOTALE DEL DEBITO A CARICO DI UN UTENTE
    ' '' ''                If myReader.Read Then
    ' '' ''                    DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
    ' '' ''                End If
    ' '' ''                myReader.Close()
    ' '' ''                sStringaSQL = ""

    ' '' ''                '***********PAGATO RELATIVO ALLA PRIMA DATA*****************
    ' '' ''                If (par.IfEmpty(DataDal, "Null") <> "Null") Then
    ' '' ''                    sValore = DataDal
    ' '' ''                    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
    ' '' ''                End If
    ' '' ''                'MODIFY 21/04/2011 PER RECUPERO INFORMAZIONI SINGOLO CONTRATTO
    ' '' ''                'sStringaSQL = "SELECT SUM(IMPORTO_PAGATO) AS PAGATO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE " & CondBollAnnullate & " AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " " & sStringaSQL & " )"
    ' '' ''                sStringaSQL = "SELECT SUM(IMPORTO_PAGATO) AS PAGATO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE " & CondBollAnnullate & " AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " " & sStringaSQL & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " )"

    ' '' ''                par.cmd.CommandText = sStringaSQL
    ' '' ''                myReader = par.cmd.ExecuteReader()
    ' '' ''                'IMPORTO PAGATO DALL'UTENTE
    ' '' ''                If myReader.Read Then
    ' '' ''                    Me.LblSaldoInizio.Text = "Saldo contabile di quanto dovuto al " & par.FormattaData(DataDal) & " (dall'inizio del rapporto) pari a: €." & Format(DOVUTO - par.IfNull(myReader("PAGATO"), 0), "##,##0.00")
    ' '' ''                End If
    ' '' ''                myReader.Close()

    ' '' ''                '*********PULISCO STINGA SQL PER SECONDO ESTRATTO*******
    ' '' ''                sStringaSQL = ""
    ' '' ''                par.cmd.CommandText = ""
    ' '' ''                DOVUTO = 0


    ' '' ''                '***********DOVUTO RELATIVO ALLA SECONDA DATA*****************

    ' '' ''                If (par.IfEmpty(DataAl, "Null") <> "Null") Then
    ' '' ''                    sValore = DataAl
    ' '' ''                    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & sValore
    ' '' ''                End If
    ' '' ''                'MODIFY 21/04/2011 PER RECUPERO INFORMAZIONI SINGOLO CONTRATTO
    ' '' ''                'sStringaSQL = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " " & sStringaSQL & ")"
    ' '' ''                'sStringaSQL = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE FL_ANNULLATA = 0 AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & ")"
    ' '' ''                sStringaSQL = "SELECT SUM(NVL(IMPORTO_TOTALE,0)-NVL(IMPORTO_RIC,0)) AS IMPORTO " _
    ' '' ''                            & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_FLUSSI " _
    ' '' ''                            & "WHERE FL_ANNULLATA = 0 " _
    ' '' ''                            & "AND BOL_FLUSSI.ID_BOLLETTA(+) = BOL_BOLLETTE.ID " _
    ' '' ''                            & "AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL

    ' '' ''                par.cmd.CommandText = sStringaSQL
    ' '' ''                myReader = par.cmd.ExecuteReader()
    ' '' ''                If myReader.Read Then
    ' '' ''                    DOVUTO = par.IfNull(myReader("IMPORTO"), 0)
    ' '' ''                End If
    ' '' ''                myReader.Close()
    ' '' ''                sStringaSQL = ""

    ' '' ''                '***********PAGATO RELATIVO ALLA SECONDA DATA*****************

    ' '' ''                If (par.IfEmpty(DataAl, "Null") <> "Null") Then
    ' '' ''                    sValore = DataAl
    ' '' ''                    sStringaSQL = sStringaSQL & " AND BOL_BOLLETTE.DATA_PAGAMENTO <= " & sValore
    ' '' ''                End If
    ' '' ''                'sStringaSQL = "SELECT SUM(IMPORTO_PAGATO) AS PAGATO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE " & CondBollAnnullate & " AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " " & sStringaSQL & " )"
    ' '' ''                sStringaSQL = "SELECT SUM(IMPORTO_PAGATO) AS PAGATO FROM SISCOM_MI.BOL_BOLLETTE WHERE ID IN ( SELECT ID FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE " & CondBollAnnullate & " AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA=" & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " " & sStringaSQL & " )"
    ' '' ''                par.cmd.CommandText = sStringaSQL
    ' '' ''                myReader = par.cmd.ExecuteReader()

    ' '' ''                If myReader.Read Then
    ' '' ''                    Me.LblSaldoFine.Text = "Saldo contabile di quanto dovuto al " & par.FormattaData(DataAl) & " (dall'inizio del rapporto) pari a: €." & Format(DOVUTO - par.IfNull(myReader("PAGATO"), 0), "##,##0.00")
    ' '' ''                End If
    ' '' ''                myReader.Close()

    ' '' ''                '*********PULISCO STINGA SQL*******
    ' '' ''                sStringaSQL = ""
    ' '' ''                par.cmd.CommandText = ""
    ' '' ''                DOVUTO = 0

    ' '' ''                ''ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
    ' '' ''                Dim UltimoPagam As String = 0
    ' '' ''                'par.cmd.CommandText = "SELECT to_char(to_date(MAX(DATA_PAGAMENTO),'yyyymmdd'),'dd/mm/yyyy') AS ULTIMA_BOLL FROM SISCOM_MI.BOL_BOLLETTE WHERE n_rata<>999 and  N_RATA<>99"
    ' '' ''                'Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    ' '' ''                'If myReaderTEMP.Read Then
    ' '' ''                UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) 'par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
    ' '' ''                'End If
    ' '' ''                'myReaderTEMP.Close()
    ' '' ''                'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI

    ' '' ''                par.cmd.CommandText = ""
    ' '' ''                par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & Request.QueryString("IDANA")
    ' '' ''                myReader = par.cmd.ExecuteReader()
    ' '' ''                If myReader.Read Then
    ' '' ''                    Me.LblTitolo.Text = " - " & par.IfNull(myReader("INTESTATARIO"), "") & " - Saldo Contabile dal " & par.FormattaData(DataDal) & " al " & par.FormattaData(DataAl) & " DATA STAMPA: " & UltimoPagam
    ' '' ''                End If
    ' '' ''                myReader.Close()

    ' '' ''                '*elaboro bollette comprese intervallo definito
    ' '' ''                par.cmd.CommandText = "SELECT NUM_BOLLETTA, BOL_BOLLETTE.ID,N_RATA,DATA_EMISSIONE, DATA_SCADENZA, DATA_PAGAMENTO, IMPORTO_PAGATO,ANNO,FL_ANNULLATA, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO, ID_BOLLETTA_RIC " _
    ' '' ''                                    & ",(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento " _
    ' '' ''                                    & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO, SISCOM_MI.TIPO_BOLLETTE WHERE " & CondBollAnnullate & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE " _
    ' '' ''                                    & "AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND BOL_BOLLETTE.DATA_EMISSIONE >= " & DataDal & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & DataAl & " ORDER BY BOL_BOLLETTE.ID DESC  "
    ' '' ''                myReader = par.cmd.ExecuteReader()

    ' '' ''                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
    ' '' ''                & "<tr>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.BOL.</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.RATA</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>TIPO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:center'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>MOROSITA</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO CONTABILE</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:center'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO INCASSATO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>SALDO CONTABILE</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:center'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>GIORNI RITARDO</strong></span></td>" _
    ' '' ''                & "</tr>"
    ' '' ''                Dim Nbolletta As String = ""

    ' '' ''                Do While myReader.Read

    ' '' ''                    Nbolletta = ""
    ' '' ''                    If par.IfNull(myReader("N_RATA"), "") = "99" Then
    ' '' ''                        Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
    ' '' ''                    ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
    ' '' ''                        Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
    ' '' ''                    ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
    ' '' ''                        Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
    ' '' ''                    Else
    ' '' ''                        Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
    ' '' ''                    End If

    ' '' ''                    If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    ' '' ''                        COLORE = "#FF1800"
    ' '' ''                    End If

    ' '' ''                    Contatore = Contatore + 1

    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                    & "<tr bgcolor = '" & COLORE & "'>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>"

    ' '' ''                    If DataAl < par.IfNull(myReader("DATA_PAGAMENTO"), 0) Then
    ' '' ''                        testoTabella = testoTabella & "<td style='height: 19px'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a>   *</span></td>"
    ' '' ''                    Else
    ' '' ''                        testoTabella = testoTabella & "<td style='height: 19px'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "") & "</a>    </span></td>"
    ' '' ''                    End If

    ' '' ''                    testoTabella = testoTabella & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & Nbolletta & "</span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>" _
    ' '' ''                    & "<td style='height: 19px;text-align:center;'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>"


    ' '' ''                    If par.IfNull(par.IfNull(myReader("ID_TIPO"), 1), 1) <> 4 And par.IfNull(par.IfNull(myReader("ID_TIPO"), 1), 1) <> 3 Then

    ' '' ''                        par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "")
    ' '' ''                        myReader2 = par.cmd.ExecuteReader

    ' '' ''                        If myReader2.Read Then
    ' '' ''                            '**************************COLONNA IMPORTO*******************************************
    ' '' ''                            testoTabella = testoTabella _
    ' '' ''                            & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader2("IMPORTO"), 0), "##,##0.00") & "</span></td>"
    ' '' ''                            TotImporto = TotImporto + par.IfNull(myReader2("IMPORTO"), "0")

    ' '' ''                            If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    ' '' ''                                'CASO IN CUI LA  BOLLETTA è ANNULLATA, IMPORTO PAGATO DOVREBBE ESSERE 0...
    ' '' ''                                ImpDovuto = par.IfNull(myReader("IMPORTO_PAGATO"), 0)
    ' '' ''                            Else
    ' '' ''                                'CASO IN CUI LA BOLLETTA è NORMALE IO DEVO VERSARE importo di bolletta - pagato
    ' '' ''                                ImpDovuto = par.IfNull(myReader2("IMPORTO"), 0) - par.IfNull(myReader("IMPORTO_PAGATO"), 0)
    ' '' ''                            End If
    ' '' ''                            importobolletta = ImpDovuto
    ' '' ''                        Else
    ' '' ''                            testoTabella = testoTabella & "<td style='height: 19px'><span style='font-size: 8pt; font-family: Arial'>€." & Format(0, "##,##0.00") & "</span>" _
    ' '' ''                                                        & "</td>"
    ' '' ''                        End If

    ' '' ''                        '***********************COLONNA MOROSITA (dovrebbe essere rinominata RICLASSIFICATE)
    ' '' ''                        testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(0, "##,##0.00") & "</span></td>"

    ' '' ''                        '**********************COLONNA IMPORTO CONTABILE *********************= IMPORTO - QUOTE SINDACALI
    ' '' ''                        par.cmd.CommandText = "SELECT SUM(IMPORTO) AS QUOTE_SINDACALI FROM SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA = 3 "
    ' '' ''                        myReader3 = par.cmd.ExecuteReader
    ' '' ''                        If myReader3.Read Then
    ' '' ''                            ImportoContabile = par.IfNull(myReader2("IMPORTO"), 0) - par.IfNull(myReader3("QUOTE_SINDACALI"), 0)
    ' '' ''                            QSindacali = par.IfNull(myReader3("QUOTE_SINDACALI"), 0)
    ' '' ''                        Else
    ' '' ''                            ImportoContabile = par.IfNull(myReader2("IMPORTO"), 0)
    ' '' ''                        End If
    ' '' ''                        ToTContabile = ToTContabile + ImportoContabile
    ' '' ''                        testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(ImportoContabile, 0), "##,##0.00") & "</span></td>"

    ' '' ''                    Else

    ' '' ''                        '************************COSTRUZIONE DELLA RIGA PER LE BOLLETTE DI MOROSITA O FINE CONTRATTO*********************

    ' '' ''                        '********************COLONNA IMPORTO SEMPRE 0***********************************
    ' '' ''                        testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(0, "##,##0.00") & "</span></td>"

    ' '' ''                        '***************COLONNA MOROSITA, dovrà essere rinominata RICLASSIFICATE*********************************

    ' '' ''                        par.cmd.CommandText = "SELECT IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & "  AND (ID_VOCE = 150 OR ID_VOCE = 151) "
    ' '' ''                        myReader2 = par.cmd.ExecuteReader

    ' '' ''                        If myReader2.Read Then


    ' '' ''                            testoTabella = testoTabella _
    ' '' ''                            & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                            & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader2("IMPORTO"), 0), "##,##0.00") & "</span></td>" 'RICLASSIFICATE (morosita)
    ' '' ''                            Riclassificate = par.IfNull(myReader2("IMPORTO"), "0")
    ' '' ''                            TotMorosita = TotMorosita + Riclassificate

    ' '' ''                            If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    ' '' ''                                'CASO IN CUI LA  BOLLETTA è ANNULLATA, IMPORTO PAGATO DOVREBBE ESSERE 0...
    ' '' ''                                ImpDovuto = par.IfNull(myReader("IMPORTO_PAGATO"), 0)
    ' '' ''                            Else
    ' '' ''                                'CASO IN CUI LA BOLLETTA è NORMALE IO DEVO VERSARE importo di bolletta - pagato
    ' '' ''                                ImpDovuto = par.IfNull(myReader2("IMPORTO"), 0) - par.IfNull(myReader("IMPORTO_PAGATO"), 0)
    ' '' ''                            End If
    ' '' ''                            importobolletta = ImpDovuto


    ' '' ''                        Else
    ' '' ''                            testoTabella = testoTabella & "<td style='height: 19px'><span style='font-size: 8pt; font-family: Arial'>€." & Format(0, "##,##0.00") & "</span>" _
    ' '' ''                                                        & "</td>"
    ' '' ''                        End If
    ' '' ''                        '******************COLONNA IMPORTO CONTABILE********************
    ' '' ''                        '**E' DATA DALL'IMPORTO DELLA BOLLETTA, MENO LE VOCI 150/151 CHE RAPPRESENTANO L'IMPORTO DELLE BOLLETTE RICLASSIFICATE SENZA QUOTE SINDACALI

    ' '' ''                        ''par.cmd.CommandText = "SELECT SUM(IMPORTO) AS TOT_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " "
    ' '' ''                        '**********modifico la query per alleggerire (piuttosto che la somma delle voci leggo direttamente l'importo totale della bolletta
    ' '' ''                        par.cmd.CommandText = "SELECT SUM(IMPORTO) AS QUOTE_SINDACALI FROM SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "") & " AND T_VOCI_BOLLETTA.ID= BOL_BOLLETTE_VOCI.ID_VOCE AND T_VOCI_BOLLETTA.COMPETENZA = 3 "
    ' '' ''                        myReader3 = par.cmd.ExecuteReader
    ' '' ''                        If myReader3.Read Then
    ' '' ''                            QSindacali = par.IfNull(myReader3("QUOTE_SINDACALI"), 0)
    ' '' ''                        End If

    ' '' ''                        par.cmd.CommandText = "SELECT IMPORTO_TOTALE FROM SISCOM_MI.BOL_BOLLETTE WHERE ID =" & par.IfNull(myReader("ID"), "")
    ' '' ''                        myReader3 = par.cmd.ExecuteReader
    ' '' ''                        If myReader3.Read Then
    ' '' ''                            ImportoContabile = par.IfNull(myReader3("IMPORTO_TOTALE"), 0) - par.IfNull(myReader2("IMPORTO"), 0) 'DEBITO SU RICLASSIFICATE
    ' '' ''                        Else
    ' '' ''                            ImportoContabile = 0
    ' '' ''                        End If
    ' '' ''                        ToTContabile = ToTContabile + ImportoContabile
    ' '' ''                        testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(ImportoContabile, 0), "##,##0.00") & "</span></td>"

    ' '' ''                    End If


    ' '' ''                    myReader2.Close()
    ' '' ''                    myReader3.Close()

    ' '' ''                    'SOMMATORIE PER I TOTALIù
    ' '' ''                    If (par.IfNull(myReader("ID_TIPO"), 1) <> 4 And par.IfNull(myReader("ID_TIPO"), 1) <> 3) And par.IfNull(myReader("IMPORTO_PAGATO"), 0) > 0 Then
    ' '' ''                        'If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    ' '' ''                        '    TotImportoIncassato = TotImportoIncassato - par.IfNull(myReader("IMPORTO_PAGATO"), 0)
    ' '' ''                        'Else
    ' '' ''                        '    TotImportoIncassato = TotImportoIncassato + par.IfNull(myReader("IMPORTO_PAGATO"), 0)
    ' '' ''                        'End If

    ' '' ''                        ImpIncassato = par.IfNull(myReader("IMPORTO_PAGATO"), 0) - QSindacali

    ' '' ''                    ElseIf (par.IfNull(myReader("ID_TIPO"), 1) = 4 Or par.IfNull(myReader("ID_TIPO"), 1) = 3) And par.IfNull(myReader("IMPORTO_PAGATO"), 0) > 0 Then
    ' '' ''                        ImpIncassato = par.IfNull(myReader("IMPORTO_PAGATO"), 0) - Riclassificate - QSindacali

    ' '' ''                        'If par.IfNull(myReader("FL_ANNULLATA"), 0) <> 0 Then
    ' '' ''                        '    TotImportoIncassato = TotImportoIncassato - (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - (ImpDovuto))
    ' '' ''                        'Else
    ' '' ''                        '    TotImportoIncassato = TotImportoIncassato + (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - (ImpDovuto))
    ' '' ''                        'End If

    ' '' ''                    End If
    ' '' ''                    TotImportoIncassato = TotImportoIncassato + ImpIncassato
    ' '' ''                    SaldoContabile = ImportoContabile - ImpIncassato
    ' '' ''                    TotSaldoContabile = TotSaldoContabile + SaldoContabile


    ' '' ''                    'If par.IfNull(par.IfNull(myReader("ID_TIPO"), 1), 1) <> 4 And par.IfNull(myReader("ID_TIPO"), 1) <> 3 Then
    ' '' ''                    '    SaldoContabile = ImportoContabile - ImpIncassato
    ' '' ''                    '    TotSaldoContabile = TotSaldoContabile + SaldoContabile
    ' '' ''                    'Else
    ' '' ''                    '    'If ImpDovuto > 0 Then
    ' '' ''                    '    '    TotSaldoContabile = TotSaldoContabile + ImportoContabile
    ' '' ''                    '    'End If
    ' '' ''                    '    SaldoContabile = ImportoContabile - (par.IfNull(myReader("IMPORTO_PAGATO"), 0) - Riclassificate)
    ' '' ''                    '    TotSaldoContabile = TotSaldoContabile + SaldoContabile

    ' '' ''                    'End If



    ' '' ''                    Dim GiorniRitardo As String = 0

    ' '' ''                    If importobolletta > 0 Then

    ' '' ''                        If par.IfNull(myReader("DATA_PAGAMENTO"), "") <> "" Then
    ' '' ''                            'DIFFERENZA FRA DATA SCADENZA E DATA PAGAMENTE
    ' '' ''                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), CDate(par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), ""))))
    ' '' ''                        Else
    ' '' ''                            'DIFFERENZA FRA DATA SCADENZA E OGGI
    ' '' ''                            GiorniRitardo = DateDiff(DateInterval.Day, CDate(par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))), Date.Now)
    ' '' ''                        End If

    ' '' ''                    Else
    ' '' ''                        GiorniRitardo = -1
    ' '' ''                    End If
    ' '' ''                    'Se giorni di ritardo è negativo allroa non lo faccio apparire
    ' '' ''                    If GiorniRitardo < 0 Then
    ' '' ''                        GiorniRitardo = ""
    ' '' ''                    End If


    ' '' ''                    If myReader("FL_ANNULLATA") <> 0 Then
    ' '' ''                        GiorniRitardo = "ANNULLATA - PAGATA"
    ' '' ''                        'testoTabella = testoTabella _
    ' '' ''                        '& "<td style='height: 19px;text-align:center'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>€.-" & Format(par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfEmpty(ImportoContabile, 0) - par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px'>" _
    ' '' ''                        '& "</td>" _
    ' '' ''                        '& "<td style='height: 19px'>" _
    ' '' ''                        '& "</td>" _
    ' '' ''                        '& "</tr>"
    ' '' ''                    Else
    ' '' ''                        'testoTabella = testoTabella _
    ' '' ''                        '& "<td style='height: 19px;text-align:center'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfEmpty(ImportoContabile, 0) - par.IfNull(myReader("IMPORTO_PAGATO"), 0), "##,##0.00") & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        '& "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>" _
    ' '' ''                        '& "<td style='height: 19px'>" _
    ' '' ''                        '& "</td>" _
    ' '' ''                        '& "<td style='height: 19px'>" _
    ' '' ''                        '& "</td>" _
    ' '' ''                        '& "</tr>"

    ' '' ''                    End If
    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                                & "<td style='height: 19px;text-align:center'>" _
    ' '' ''                                & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "</span></td>" _
    ' '' ''                                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                                & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(ImpIncassato, "##,##0.00") & "</span></td>" _
    ' '' ''                                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                                & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfEmpty(SaldoContabile, 0), "##,##0.00") & "</span></td>" _
    ' '' ''                                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                                & "<span style='font-size: 8pt; font-family: Arial'>" & GiorniRitardo & "</span></td>" _
    ' '' ''                                & "<td style='height: 19px'>" _
    ' '' ''                                & "</td>" _
    ' '' ''                                & "<td style='height: 19px'>" _
    ' '' ''                                & "</td>" _
    ' '' ''                                & "</tr>"



    ' '' ''                    If COLORE = "#FFFFFF" Then
    ' '' ''                        COLORE = "#E6E6E6"
    ' '' ''                    Else
    ' '' ''                        COLORE = "#FFFFFF"
    ' '' ''                    End If
    ' '' ''                    Riclassificate = 0
    ' '' ''                    SaldoContabile = 0
    ' '' ''                    ImpIncassato = 0
    ' '' ''                Loop

    ' '' ''                testoTabella = testoTabella _
    ' '' ''                & "<tr>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong> €." & Format(par.IfEmpty(TotImporto, 0), "##,##0.00") & "</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong> €." & Format(par.IfEmpty(TotMorosita, 0), "##,##0.00") & "</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(par.IfEmpty(ToTContabile, 0), "##,##0.00") & "</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(par.IfEmpty(TotImportoIncassato, 0), "##,##0.00") & "</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(par.IfEmpty(TotSaldoContabile, 0), "##,##0.00") & "</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
    ' '' ''                & "</tr>"

    ' '' ''                Me.TBL_ESTRATTO_CONTO.Text = testoTabella & "</table>"



    ' '' ''                myReader.Close()


    ' '' ''                'BOLLETTE ANNULLATE
    ' '' ''                testoTabella = ""
    ' '' ''                Contatore = 0


    ' '' ''                par.cmd.CommandText = "SELECT ID,DATA_EMISSIONE, DATA_SCADENZA, DATA_PAGAMENTO, IMPORTO_PAGATO,DATA_ANNULLO " _
    ' '' ''                                    & ",(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento " _
    ' '' ''                                    & "FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE ( FL_ANNULLATA='1' OR FL_ANNULLATA='2') AND BOL_BOLLETTE.ID_CONTRATTO = INTESTATARI_RAPPORTO.ID_CONTRATTO AND BOL_BOLLETTE.DATA_EMISSIONE<=INTESTATARI_RAPPORTO.DATA_FINE AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA = " & Request.QueryString("IDANA") & " AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") & " AND BOL_BOLLETTE.DATA_EMISSIONE >= " & DataDal & " AND BOL_BOLLETTE.DATA_EMISSIONE <= " & DataAl & "  "
    ' '' ''                myReader = par.cmd.ExecuteReader()

    ' '' ''                testoTabella = "<table cellpadding='0' cellspacing='0' width='60%'>" & vbCrLf _
    ' '' ''                & "<tr>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>NUM.BOLLETTA</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:center'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>IMPORTO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:right'>" _
    ' '' ''                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA ANNULLO</strong></span></td>" _
    ' '' ''                & "<td style='height: 19px;text-align:center'>" _
    ' '' ''                & "</tr>"

    ' '' ''                TotImporto = 0

    ' '' ''                Do While myReader.Read

    ' '' ''                    Contatore = Contatore + 1

    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                    & "<tr bgcolor = '" & COLORE & "'>" _
    ' '' ''                                            & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & Contatore & ")</span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioBolletta.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "&IDANA=" & Request.QueryString("IDANA") & "&IDCONT=" & Request.QueryString("IDCONT") & "','Dettagli" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & Format(par.IfNull(myReader("ID"), ""), "0000000000") & "</a></span></td>" _
    ' '' ''                    & "<td style='height: 19px;text-align:center;'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>"

    ' '' ''                    par.cmd.CommandText = "SELECT SUM(IMPORTO) AS IMPORTO FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), "")
    ' '' ''                    myReader2 = par.cmd.ExecuteReader

    ' '' ''                    If myReader2.Read Then
    ' '' ''                        testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(myReader2("IMPORTO"), 0), "##,##0.00") & "</span></td>"
    ' '' ''                        TotImporto = TotImporto + par.IfNull(myReader2("IMPORTO"), "")
    ' '' ''                    End If
    ' '' ''                    myReader2.Close()
    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_ANNULLO"), "")) & "</span></td>"



    ' '' ''                    testoTabella = testoTabella & "</tr>"


    ' '' ''                    If COLORE = "#FFFFFF" Then
    ' '' ''                        COLORE = "#E6E6E6"
    ' '' ''                    Else
    ' '' ''                        COLORE = "#FFFFFF"
    ' '' ''                    End If
    ' '' ''                Loop

    ' '' ''                If TotImporto > 0 Then

    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                    & "<tr bgcolor = '" & COLORE & "'>" _
    ' '' ''                                            & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>" _
    ' '' ''                    & "<td style='height: 19px'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'></span></td>"


    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                    & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                    & "<span style='font-size: 8pt; font-family: Arial'>€." & Format(par.IfNull(TotImporto, 0), "##,##0.00") & "</span></td>"



    ' '' ''                    testoTabella = testoTabella _
    ' '' ''                        & "<td style='height: 19px; text-align:right'>" _
    ' '' ''                        & "<span style='font-size: 8pt; font-family: Arial'></span></td>"



    ' '' ''                    testoTabella = testoTabella & "</tr>"

    ' '' ''                    Me.TBL_ANNULLATE.Text = testoTabella & "</table>"
    ' '' ''                Else
    ' '' ''                    Me.TBL_ANNULLATE.Text = ""
    ' '' ''                    TBL_ANNULLATE0.Text = ""
    ' '' ''                End If







    ' '' ''                myReader.Close()
    ' '' ''                'myReaderTEMP.Close()



    ' '' ''                '*********************CHIUSURA CONNESSIONE**********************
    ' '' ''                par.cmd.Dispose()
    ' '' ''                par.OracleConn.Close()
    ' '' ''                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    ' '' ''            Else
    ' '' ''                Response.Write("<script>alert('ERRORE RECUPERO INFORMAZIONI')</script>")

    ' '' ''            End If

    ' '' ''        End If

    ' '' ''    Catch ex As Exception
    ' '' ''        Me.lblErrore.Visible = True
    ' '' ''        lblErrore.Text = ex.Message
    ' '' ''        par.OracleConn.Close()
    ' '' ''        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    ' '' ''    End Try
    ' '' ''End Sub
    Function GenHtmlTable()
        Dim sRet As String
        sRet = "<table border=0 width='100%'><tr>"
        sRet = sRet & Me.LblTitolo.Text
        sRet = sRet & "</tr>"
        'sRet = sRet & "<tr>" & LblSaldoInizio.Text & "</tr>"
        sRet = sRet & "<tr>" & LblSaldoFine.Text & "</tr></table>"
        'sRet = sRet & TBL_ESTRATTO_CONTO.Text

        GenHtmlTable = sRet

    End Function

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
            pdfConverter1.PageWidth = 1400
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("Estratto Conto " & LblTitolo.Text & " " & Format(Now, "hh:mm") & " - OPERATORE: " & Session.Item("OPERATORE"))
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_EstrattoContab_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFile("<br /><br /><b>Estratto Conto " & LblTitolo.Text & "</b><br /><br />" & lblSottoTitolo.Text & "<br /><br />" & LblSaldoFine.Text & "<br /><br />Saldo contabile " & lblSaldoCont.Text & " di cui a ruolo " & lblImpRuolo.Text & " di cui ingiunto " & lblImpIng.Text & " " & Html, url & nomefile)

            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        PrintPdf()
        ''Dim Html As String = ""
        ' ''Dim stringWriter As New System.IO.StringWriter
        ' ''Dim sourcecode As New HtmlTextWriter(stringWriter)

        ''Try

        ''    'Html = Me.TBL_ESTRATTO_CONTO.Text

        ''    Dim url As String = Server.MapPath("..\FileTemp\")
        ''    Dim pdfConverter1 As PdfConverter = New PdfConverter

        ''    Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        ''    If Licenza <> "" Then
        ''        pdfConverter1.LicenseKey = Licenza
        ''    End If

        ''    pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        ''    pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
        ''    pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        ''    pdfConverter1.PdfDocumentOptions.ShowHeader = False
        ''    pdfConverter1.PdfDocumentOptions.ShowFooter = True
        ''    pdfConverter1.PdfDocumentOptions.LeftMargin = 20
        ''    pdfConverter1.PdfDocumentOptions.RightMargin = 5
        ''    pdfConverter1.PdfDocumentOptions.TopMargin = 10
        ''    pdfConverter1.PdfDocumentOptions.BottomMargin = 10
        ''    pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

        ''    pdfConverter1.PdfDocumentOptions.ShowHeader = False
        ''    pdfConverter1.PdfFooterOptions.FooterText = ("Estratto Conto " & LblTitolo.Text)
        ''    pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
        ''    pdfConverter1.PdfFooterOptions.DrawFooterLine = False
        ''    pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
        ''    pdfConverter1.PdfFooterOptions.ShowPageNumber = True

        ''    Dim nomefile As String = "Exp_Pagamenti_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        ''    pdfConverter1.SavePdfFromHtmlStringToFile("<br /><br />Estratto Conto " & LblTitolo.Text & "<br /><br />" & Me.LblSaldoInizio.Text & "<br /><br />" & LblSaldoFine.Text & "<br /><br />" & Html, url & nomefile)
        ''    Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        ''Catch ex As Exception
        ''    Me.lblErrore.Visible = True
        ''    lblErrore.Text = ex.Message
        ''End Try
    End Sub

    Protected Sub DataGridContab_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridContab.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
            'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#E7E7FF';}")

            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('idBolletta').value=" & e.Item.Cells(0).Text & ";")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
            'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#F7F7F7';}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('idBolletta').value=" & e.Item.Cells(0).Text & ";")

        End If
    End Sub

    Protected Sub ImgBtnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnExport.Click
        'par.EsportaExcelAutomaticoDaDataGrid(
        Dim nomeFile As String = EsportaExcel(dt1, DataGridContab, "ExportContabile", , , , False)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub DataGridContab_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridContab.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridContab.CurrentPageIndex = e.NewPageIndex
            RiempiDatagridBoll()
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
            Dim LarghezzaDataGrid As Integer = Max(DataGrid.Width.Value, 200)
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
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 5, 1, txtsldfine.Value, 0)
                indiceRighe += 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 6, 1, lblSaldoTot.Text & " " & lblSaldoCont.Text & " " & lblRuolo.Text & " " & lblImpRuolo.Text & " " & lblIng.Text & " " & lblImpIng.Text, 0)
                indiceRighe += 1


                Dim indiceVisibile As Integer = 1
                IndiceColonne = indiceRighe + 2
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
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, Max(LarghezzaMinimaColonna, WitdhColum))
                        'GESTIONE DELLE INTESTAZIONI
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, indiceVisibile, DataGrid.Columns.Item(j).HeaderText, 0)
                        indiceVisibile = indiceVisibile + 1
                    End If
                Next
                indiceRighe = indiceRighe + 2
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
End Class
