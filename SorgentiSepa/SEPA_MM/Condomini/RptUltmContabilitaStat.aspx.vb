Imports System.IO

Partial Class Condomini_RptUltmContabilitaStat
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

    Public Property datatableUltContabStat() As Data.DataTable
        Get
            If Not (ViewState("datatableUltContabStat") Is Nothing) Then
                Return ViewState("datatableUltContabStat")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatableUltContabStat") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        Try
            If Not IsPostBack Then
                CreaDT()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub CreaDT()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            If Request.QueryString("X") = 0 Then
                par.cmd.CommandText = "SELECT A.DENOMINAZIONE AS CONDOMINIO, TO_CHAR(TO_DATE(COND_GESTIONE.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE, (CASE WHEN COND_GESTIONE.TIPO = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' END) AS TIPO, " _
                                    & "(CASE WHEN COND_GESTIONE.STATO_BILANCIO='P0' THEN 'BOZZA' WHEN STATO_BILANCIO='P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' END) AS STATO_BILANCIO, " _
                                    & "COND_GESTIONE.N_RATE, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_1_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_1, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_2_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_2, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.RATA_3_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_3, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_4_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_4, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.RATA_5_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_5, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_6_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_6, " _
                                    & "COND_GESTIONE.NOTE " _
                                    & "FROM siscom_mi.cond_gestione, SISCOM_MI.CONDOMINI A " _
                                    & "WHERE COND_GESTIONE.ID IN (SELECT DISTINCT cond_gestione.ID " _
                                    & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti, siscom_mi.prenotazioni, siscom_mi.cond_gestione_dett_scad " _
                                    & "WHERE cond_gestione_dett_scad.id_gestione = cond_gestione.ID AND prenotazioni.ID = cond_gestione_dett_scad.id_prenotazione " _
                                    & "AND prenotazioni.id_pagamento = pagamenti.ID AND siscom_mi.getstatopagamento (id_pagamento) <> 'EMESSO') " _
                                    & "AND COND_GESTIONE.ID_CONDOMINIO = A.ID AND COND_GESTIONE.DATA_INIZIO = (SELECT MAX(COND_GESTIONE.DATA_INIZIO) " _
                                    & "FROM siscom_mi.cond_gestione, SISCOM_MI.CONDOMINI " _
                                    & "WHERE COND_GESTIONE.ID IN (SELECT DISTINCT cond_gestione.ID " _
                                    & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti, siscom_mi.prenotazioni, siscom_mi.cond_gestione_dett_scad " _
                                    & "WHERE cond_gestione_dett_scad.id_gestione = cond_gestione.ID AND prenotazioni.ID = cond_gestione_dett_scad.id_prenotazione " _
                                    & "AND prenotazioni.id_pagamento = pagamenti.ID AND siscom_mi.getstatopagamento (id_pagamento) <> 'EMESSO' " _
                                    & "AND COND_GESTIONE.ID_CONDOMINIO = CONDOMINI.ID) AND CONDOMINI.DENOMINAZIONE = A.DENOMINAZIONE) " _
                                    & "ORDER BY 1,2"
            Else
                par.cmd.CommandText = "SELECT A.DENOMINAZIONE AS CONDOMINIO, TO_CHAR(TO_DATE(COND_GESTIONE.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE, (CASE WHEN COND_GESTIONE.TIPO = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' END) AS TIPO, " _
                                    & "(CASE WHEN COND_GESTIONE.STATO_BILANCIO='P0' THEN 'BOZZA' WHEN STATO_BILANCIO='P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' END) AS STATO_BILANCIO, " _
                                    & "COND_GESTIONE.N_RATE, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_1_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_1, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_2_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_2, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.RATA_3_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_3, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_4_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_4, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.RATA_5_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_5, TO_CHAR(TO_DATE(COND_GESTIONE.RATA_6_SCAD,'yyyymmdd'),'dd/mm/yyyy') AS SCADENZA_RATA_6, " _
                                    & "COND_GESTIONE.NOTE " _
                                    & "FROM siscom_mi.cond_gestione, SISCOM_MI.CONDOMINI A " _
                                    & "WHERE COND_GESTIONE.ID IN (SELECT DISTINCT cond_gestione.ID " _
                                    & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti, siscom_mi.prenotazioni, siscom_mi.cond_gestione_dett_scad " _
                                    & "WHERE cond_gestione_dett_scad.id_gestione = cond_gestione.ID AND prenotazioni.ID = cond_gestione_dett_scad.id_prenotazione " _
                                    & "AND prenotazioni.id_pagamento = pagamenti.ID AND siscom_mi.getstatopagamento (id_pagamento) <> 'EMESSO') " _
                                    & "AND COND_GESTIONE.ID_CONDOMINIO = A.ID AND COND_GESTIONE.DATA_INIZIO = (SELECT MAX(COND_GESTIONE.DATA_INIZIO) " _
                                    & "FROM siscom_mi.cond_gestione, SISCOM_MI.CONDOMINI " _
                                    & "WHERE COND_GESTIONE.ID IN (SELECT DISTINCT cond_gestione.ID " _
                                    & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti, siscom_mi.prenotazioni, siscom_mi.cond_gestione_dett_scad " _
                                    & "WHERE cond_gestione_dett_scad.id_gestione = cond_gestione.ID AND prenotazioni.ID = cond_gestione_dett_scad.id_prenotazione " _
                                    & "AND prenotazioni.id_pagamento = pagamenti.ID AND siscom_mi.getstatopagamento (id_pagamento) <> 'EMESSO' " _
                                    & "AND COND_GESTIONE.ID_CONDOMINIO = CONDOMINI.ID) AND CONDOMINI.DENOMINAZIONE = A.DENOMINAZIONE) "
                If Request.QueryString("Dal") <> "" Then
                    par.cmd.CommandText = par.cmd.CommandText & " AND substr(COND_GESTIONE.DATA_INIZIO,0,4) >= " & Request.QueryString("Dal")
                End If
                If Request.QueryString("Al") <> "" Then
                    par.cmd.CommandText = par.cmd.CommandText & " AND substr(COND_GESTIONE.DATA_INIZIO,0,4) <= " & Request.QueryString("Al")
                End If
                par.cmd.CommandText = par.cmd.CommandText & " ORDER BY 1,2"
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            datatableUltContabStat = New Data.DataTable
            da.Fill(datatableUltContabStat)
            da.Dispose()

            Me.dgvExport.Visible = True
            Me.dgvExport.DataSource = datatableUltContabStat
            Me.dgvExport.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Esporta()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub Esporta()
        Try
            If datatableUltContabStat.Rows.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvExport, "ExpUltimaContabilita", , , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    'If Request.QueryString("X") = 0 Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                    'Else
                    '    Response.Write("<script>location.href='..\/FileTemp\/" & nomefile & "';</script>")
                    'End If
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
