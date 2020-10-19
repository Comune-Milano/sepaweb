
Partial Class MOROSITA_ReportMorosita2
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            caricaDati()
            'Session.Remove("LISTAMOROSITA")
        End If
    End Sub
    Private Sub caricaDati()
        parametriDiRicerca.Text = Session.Item("PARAMETRIDIRICERCA")
        Dim LISTA As System.Collections.Generic.IList(Of String) = Session.Item("LISTAMOROSITA")
        Dim listaContratti As String = ""
        If Not IsNothing(LISTA) Then
            For Each Items As String In LISTA
                listaContratti &= Items & ","
            Next
            If listaContratti <> "" Then
                listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                ApriConnessione()
                par.cmd.CommandText = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                        & " NVL(SISCOM_MI.SALDI.SALDO,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                        & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & " then  trim(RAGIONE_SOCIALE) " _
                        & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                        & " AS INTESTATARIO ," _
                        & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                        & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                        & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                        & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                        & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                        & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                        & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                        & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & " then trim(RAGIONE_SOCIALE) " _
                        & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.RAPPORTI_UTENZA, " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.EDIFICI, " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.SALDI " _
                        & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                        & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                        & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                        & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                        & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                        & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                        & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                        & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")"
                Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable As New Data.DataTable
                dataAdapter.Fill(dataTable)
                DataGridInquilini3Mesi.DataSource = dataTable
                DataGridInquilini3Mesi.DataBind()
                Dim nRUmorosi As Integer = 0
                Dim nRUmorosiMM As Integer = 0
                Dim nRUmorosiCS As Integer = 0
                Dim nRUmorosiPL As Integer = 0
                Dim nRUmorosiAltri As Integer = 0
                Dim LettoreMorosi As Oracle.DataAccess.Client.OracleDataReader
                For Each Items As Data.DataRow In dataTable.Rows
                    nRUmorosi += 1
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=" & Items.Item(1)
                    LettoreMorosi = par.cmd.ExecuteReader
                    If LettoreMorosi.Read Then
                        If par.IfNull(LettoreMorosi(0), 0) <> 0 Then
                            nRUmorosiMM += 1
                        End If
                    End If
                    LettoreMorosi.Close()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE COD_STATO='M20'" _
                        & "AND ID_CONTRATTO = " & Items.Item(1)
                    LettoreMorosi = par.cmd.ExecuteReader
                    If LettoreMorosi.Read Then
                        If par.IfNull(LettoreMorosi(0), 0) <> 0 Then
                            nRUmorosiPL += 1
                        End If
                    End If
                    LettoreMorosi.Close()
                Next
                chiudiConnessione()
                NAssegnatari3Mesi.Text = nRUmorosi
                NAssegnatariMM3Mesi.Text = nRUmorosiMM
                NAssegnatariCS3Mesi.Text = nRUmorosiCS
                NAssegnatariPL3Mesi.Text = nRUmorosiPL
                NAssegnatariAltri3Mesi.Text = nRUmorosiAltri
            End If
        End If
    End Sub
    Protected Sub ApriConnessione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
            par.cmd.Dispose()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
End Class