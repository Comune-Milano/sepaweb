Imports System.IO

Partial Class Condomini_RptUltmContabilitaImporti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        Try
            If Not IsPostBack Then
                CreaDT()
                RiempiDT()
                Esporta()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
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
            datatableUltContabImporti.Columns.Add("STATO")
            datatableUltContabImporti.Columns.Add("NUM_ADP")
            datatableUltContabImporti.Columns.Add("DATA_ADP")
            datatableUltContabImporti.Columns.Add("NUM_DATA_MANDATO")
            datatableUltContabImporti.Columns.Add("IMPORTO")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Private Sub RiempiDT()
        Try
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
            If Request.QueryString("X") = 0 Then
                par.cmd.CommandText = "SELECT A.ID AS IDCONDOMINIO, COND_GESTIONE.ID AS IDGESTIONE, A.DENOMINAZIONE AS CONDOMINIO, TO_CHAR(TO_DATE(COND_GESTIONE.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE, (CASE WHEN COND_GESTIONE.TIPO = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' END) AS TIPO, " _
                                    & "(CASE WHEN COND_GESTIONE.STATO_BILANCIO='P0' THEN 'BOZZA' WHEN STATO_BILANCIO='P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' END) AS STATO_BILANCIO, " _
                                    & "COND_GESTIONE.N_RATE, COND_GESTIONE.NOTE " _
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
                                    & "ORDER BY 3,4"
            Else
                par.cmd.CommandText = "SELECT A.ID AS IDCONDOMINIO, COND_GESTIONE.ID AS IDGESTIONE, A.DENOMINAZIONE AS CONDOMINIO, TO_CHAR(TO_DATE(COND_GESTIONE.DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INIZIO, " _
                                    & "TO_CHAR(TO_DATE(COND_GESTIONE.DATA_FINE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_FINE, (CASE WHEN COND_GESTIONE.TIPO = 'O' THEN 'ORDINARIA' ELSE 'STRAORDINARIA' END) AS TIPO, " _
                                    & "(CASE WHEN COND_GESTIONE.STATO_BILANCIO='P0' THEN 'BOZZA' WHEN STATO_BILANCIO='P1' THEN 'CONVALIDATO' ELSE 'CONSUNTIVATO' END) AS STATO_BILANCIO, " _
                                    & "COND_GESTIONE.N_RATE, COND_GESTIONE.NOTE " _
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
                par.cmd.CommandText = par.cmd.CommandText & " ORDER BY 3,4"
            End If
            Dim row As Data.DataRow
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader.Read
                par.cmd.CommandText = "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 1. Scad. '|| TO_CHAR(TO_DATE(RATA_1_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO, " _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND COND_GESTIONE.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 1 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_1_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 2. Scad. '|| TO_CHAR(TO_DATE(RATA_2_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO, " _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND COND_GESTIONE.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 2 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_2_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 3. Scad. '|| TO_CHAR(TO_DATE(RATA_3_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO," _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND COND_GESTIONE.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 3 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_3_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 4. Scad. '|| TO_CHAR(TO_DATE(RATA_4_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO," _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND COND_GESTIONE.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 4 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_4_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO," _
                                & "('RATA 5. Scad. '|| TO_CHAR(TO_DATE(RATA_5_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO," _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND COND_GESTIONE.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 5 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO," _
                                & "COND_GESTIONE.RATA_5_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE," _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO " _
            & "UNION " _
                                & "SELECT COND_GESTIONE.ID,PRENOTAZIONI.ID_PAGAMENTO AS ID_PAGAMENTO, " _
                                & "('RATA 6. Scad. '|| TO_CHAR(TO_DATE(RATA_6_SCAD,'yyyymmdd'),'dd/mm/yyyy') ||' Esercizio Finanz.'||TO_CHAR(TO_DATE(DATA_INIZIO,'yyyymmdd'),'dd/mm/yyyy') ||'-'||" _
                                & "TO_CHAR(TO_DATE(DATA_FINE,'yyyymmdd'),'dd/mm/yyyy')) AS DESCRIZIONE, TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS  DATA_EMISSIONE, " _
                                & "PAGAMENTI.PROGR ,TRIM(TO_CHAR(NVL(IMPORTO_CONSUNTIVATO,0),'9G999G999G999G990D99'))AS IMPORTO_CONSUNTIVATO, " _
                                & "(CASE WHEN ID_PAGAMENTO IS NULL THEN 'PRENOTATO' ELSE (SISCOM_MI.GETSTATOPAGAMENTO(ID_PAGAMENTO)) END) AS stato,'' as num_data_mandato " _
                                & "FROM siscom_mi.cond_gestione, siscom_mi.pagamenti,siscom_mi.prenotazioni,siscom_mi.cond_gestione_dett_scad " _
                                & "WHERE ID_CONDOMINIO = " & par.IfNull(myReader("IDCONDOMINIO"), 0) & " AND COND_GESTIONE.ID = " & par.IfNull(myReader("IDGESTIONE"), 0) & " AND COND_GESTIONE_DETT_SCAD.ID_GESTIONE = COND_GESTIONE.ID " _
                                & "AND COND_GESTIONE_DETT_SCAD.N_RATA = 6 " _
                                & "AND prenotazioni.ID = cond_gestione_dett_SCAD.ID_PRENOTAZIONE " _
                                & "AND prenotazioni.id_pagamento = pagamenti.ID " _
                                & "GROUP BY PAGAMENTI.ID_STATO,COND_GESTIONE.ID, PRENOTAZIONI.ID_PAGAMENTO, " _
                                & "COND_GESTIONE.RATA_6_SCAD,COND_GESTIONE.DATA_INIZIO,COND_GESTIONE.DATA_FINE, " _
                                & "PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.PROGR,PAGAMENTI.IMPORTO_CONSUNTIVATO"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim indice As Integer = 0
                Dim importo As Decimal = 0
                Dim numdatamandato As String = ""
                While myReader2.Read
                    par.cmd.CommandText = "select num_mandato, TO_CHAR(TO_DATE(data_mandato,'yyyymmdd'),'dd/mm/yyyy') as data_mandato from siscom_mi.pagamenti_liquidati where id_pagamento = " & par.IfNull(myReader2("ID_PAGAMENTO"), 0)
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        numdatamandato = par.IfNull(myReader3("NUM_MANDATO"), "") & " - " & par.IfNull(myReader3("DATA_MANDATO"), "")
                    End If
                    myReader3.Close()
                    row = datatableUltContabImporti.NewRow()
                    If indice <> 0 Then
                        row.Item("CONDOMINIO") = ""
                        row.Item("DATA_INIZIO") = ""
                        row.Item("DATA_FINE") = ""
                        row.Item("TIPO") = ""
                        row.Item("STATO_BILANCIO") = ""
                        row.Item("NR_RATE") = ""
                        row.Item("NOTE") = ""
                    Else
                        row.Item("CONDOMINIO") = par.IfNull(myReader("CONDOMINIO"), "")
                        row.Item("DATA_INIZIO") = par.IfNull(myReader("DATA_INIZIO"), "")
                        row.Item("DATA_FINE") = par.IfNull(myReader("DATA_FINE"), "")
                        row.Item("TIPO") = par.IfNull(myReader("TIPO"), "")
                        row.Item("STATO_BILANCIO") = par.IfNull(myReader("STATO_BILANCIO"), "")
                        row.Item("NR_RATE") = par.IfNull(myReader("N_RATE"), "")
                        row.Item("NOTE") = par.IfNull(myReader("NOTE"), "")
                    End If
                    row.Item("DESCRIZIONE") = par.IfNull(myReader2("DESCRIZIONE"), "")
                    row.Item("STATO") = par.IfNull(myReader2("STATO"), "")
                    row.Item("NUM_ADP") = par.IfNull(myReader2("PROGR"), "")
                    row.Item("DATA_ADP") = par.IfNull(myReader2("DATA_EMISSIONE"), "")
                    row.Item("NUM_DATA_MANDATO") = numdatamandato
                    row.Item("IMPORTO") = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
                    importo = importo + par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
                    datatableUltContabImporti.Rows.Add(row)
                    indice += 1
                End While
                If indice > 1 Then
                    row = datatableUltContabImporti.NewRow()
                    row.Item("DESCRIZIONE") = "TOTALE"
                    row.Item("IMPORTO") = IsNumFormat(importo, 0, "##,##0.00")
                    datatableUltContabImporti.Rows.Add(row)
                End If
                myReader2.Close()
            End While
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.dgvExport.Visible = True
            Me.dgvExport.DataSource = datatableUltContabImporti
            Me.dgvExport.DataBind()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
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
            Me.dgvExport.Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
End Class

