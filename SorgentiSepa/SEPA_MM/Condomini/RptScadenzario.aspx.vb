Imports System.IO

Partial Class Condomini_RptScadenzario
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String

    Dim nGiornoRif As String
    Dim GiorniAp As String
    Public percentuale As Long = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                CreaDT()
                CreaReport()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try

    End Sub

    Public Property datatableUltContabImporti() As Data.DataTable
        Get
            If Not (ViewState("datatableUltContabImporti") Is Nothing) Then
                Return ViewState("datatableUltContabImporti")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatableUltContabImporti") = value
        End Set
    End Property

    Private Sub CreaDT()
        Try
            '######### SVUOTA E CREA COLONNE DATATABLE #########
            datatableUltContabImporti = New Data.DataTable
            datatableUltContabImporti.Clear()
            datatableUltContabImporti.Columns.Clear()
            datatableUltContabImporti.Rows.Clear()
            datatableUltContabImporti.Columns.Add("CONDOMINIO")
            datatableUltContabImporti.Columns.Add("DATA_INIZIO")
            datatableUltContabImporti.Columns.Add("DATA_FINE")
            datatableUltContabImporti.Columns.Add("TIPO")
            datatableUltContabImporti.Columns.Add("STATO_BILANCIO")
            datatableUltContabImporti.Columns.Add("NR_RATE")
            datatableUltContabImporti.Columns.Add("NOTE")
            datatableUltContabImporti.Columns.Add("DESCRIZIONE")
            datatableUltContabImporti.Columns.Add("DATA_SCADENZA")
            datatableUltContabImporti.Columns.Add("STATO")
            datatableUltContabImporti.Columns.Add("NUM_ADP")
            datatableUltContabImporti.Columns.Add("DATA_ADP")
            datatableUltContabImporti.Columns.Add("IMPORTO")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub


    Private Sub CreaReport()
        par.OracleConn.Open()
        par.cmd = par.OracleConn.CreateCommand

        par.cmd.CommandText = "SELECT A.ID AS IDCONDOMINIO, COND_GESTIONE.ID AS IDGESTIONE, A.DENOMINAZIONE AS CONDOMINIO, TO_CHAR(TO_DATE(COND_GESTIONE.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO, " _
                    & "TO_CHAR(TO_DATE(COND_GESTIONE.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE, (CASE WHEN COND_GESTIONE.TIPO = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' END) AS TIPO, " _
                    & "(CASE WHEN COND_GESTIONE.STATO_BILANCIO='P0' THEN 'BOZZA' WHEN STATO_BILANCIO='P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' END) AS STATO_BILANCIO, " _
                    & "COND_GESTIONE.N_RATE, COND_GESTIONE.NOTE " _
                    & "FROM siscom_mi.cond_gestione, SISCOM_MI.CONDOMINI A " _
                    & "WHERE COND_GESTIONE.ID_CONDOMINIO = A.ID and COND_GESTIONE.DATA_INIZIO>='20140101' " _
                    & "ORDER BY 3,2 DESC"


        Dim row As Data.DataRow
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While myReader.Read
            par.cmd.CommandText = "SELECT 'RATA '||n_RATA AS NUMERO_RATA," _
                    & "         GETDATA (RATA_SCAD) AS DATA_SCADENZA," _
                    & "         TO_CHAR (TO_DATE (data_emissione, 'yyyymmdd'), 'dd/mm/yyyy')" _
                    & "            AS  data_adp," _
                    & "         (CASE" _
                    & "             WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO'" _
                    & "             ELSE (SISCOM_MI.GETSTATOPAGAMENTO (ID_PAGAMENTO))" _
                    & "          END)" _
                    & "            AS stato," _
                    & "         PAGAMENTI.PROGR as PROGR," _
                    & "         TRIM (" _
                    & "            TO_CHAR (" _
                    & "               NVL (IMPORTO_CONSUNTIVATO," _
                    & "                    SUM (NVL (cond_gestione_dett_scad.IMPORTO, 0)))," _
                    & "               '9G999G999G999G990D99'))" _
                    & "            AS IMPORTO" _
                    & "    		FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD," _
                    & "         siscom_mi.prenotazioni," _
                    & "         siscom_mi.pagamenti" _
                    & "   		WHERE     ID_GESTIONE = " & par.IfNull(myReader("IDGESTIONE"), 0) _
                    & "         AND prenotazioni.ID(+) = cond_gestione_dett_SCAD.ID_PRENOTAZIONE" _
                    & "         AND prenotazioni.id_pagamento = pagamenti.ID(+)" _
                    & "			GROUP BY N_RATA, " _
                    & "         RATA_SCAD," _
                    & "         PAGAMENTI.DATA_EMISSIONE," _
                    & "         PRENOTAZIONI.ID_PAGAMENTO," _
                    & "         PAGAMENTI.PROGR," _
                    & "         PAGAMENTI.IMPORTO_CONSUNTIVATO" _
                    & "         ORDER BY NUMERO_RATA "

            Dim importo As Decimal = 0
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim conta As Integer = 0
            For Each riga As Data.DataRow In dt.Rows
                row = datatableUltContabImporti.NewRow()
                If conta = 0 Then
                    row.Item("CONDOMINIO") = par.IfNull(myReader("CONDOMINIO"), "")
                    row.Item("DATA_INIZIO") = par.IfNull(myReader("DATA_INIZIO"), "")
                    row.Item("DATA_FINE") = par.IfNull(myReader("DATA_FINE"), "")
                    row.Item("TIPO") = par.IfNull(myReader("TIPO"), "")
                    row.Item("STATO_BILANCIO") = par.IfNull(myReader("STATO_BILANCIO"), "")
                    row.Item("NR_RATE") = par.IfNull(myReader("N_RATE"), "")
                    row.Item("NOTE") = par.IfNull(myReader("NOTE"), "")
                End If
                row.Item("DESCRIZIONE") = par.IfNull(riga.Item("NUMERO_RATA"), "")
                row.Item("STATO") = par.IfNull(riga.Item("STATO"), "")
                row.Item("DATA_SCADENZA") = par.IfNull(riga.Item("DATA_SCADENZA"), "")
                row.Item("NUM_ADP") = par.IfNull(riga.Item("PROGR"), "")
                row.Item("DATA_ADP") = par.IfNull(riga.Item("DATA_ADP"), "")
                row.Item("IMPORTO") = par.IfNull(riga.Item("IMPORTO"), 0)
                importo = importo + CDec(par.IfNull(riga.Item("IMPORTO"), 0))
                datatableUltContabImporti.Rows.Add(row)
                conta += 1
            Next
            row = datatableUltContabImporti.NewRow()
            row.Item("DESCRIZIONE") = "TOTALE"
            row.Item("IMPORTO") = IsNumFormat(importo, 0, "##,##0.00")
            datatableUltContabImporti.Rows.Add(row)

        End While
        myReader.Close()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Esporta()


    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDec(v), Precision)
        End If
    End Function
    Private Sub Esporta()
        Try
            If datatableUltContabImporti.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomefile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExpUltimaContabilita", "SCADENZARIO", datatableUltContabImporti)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                If Request.QueryString("X") = 1 Then
                    Response.Write("<script>alert('I filtri inseriti non hanno prodotto risultati. Riprovare!');</script>")
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
End Class
