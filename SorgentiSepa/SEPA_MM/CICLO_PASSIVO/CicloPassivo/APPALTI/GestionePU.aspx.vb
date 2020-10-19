
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_GestionePU
    Inherits PageSetIdMode
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            Try
                HFGriglia.Value = dgvPrezziUnitari.ClientID
                CaricaEsercizio()
                CaricaGrigliaPU(cmbEsercizio.SelectedValue)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub CaricaEsercizio()
        Try
            Dim i As Integer = 0
            Dim ID_ANNO_EF_CORRENTE As Long = -1
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            'par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE SISCOM_MI.PF_MAIN.ID_STATO=5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as stato FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,siscom_mi.PF_STATI " _
                                & "WHERE id_stato > = 5 and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND id_stato = PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", False)
            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If
            End If


            If connAperta = True Then
                connData.chiudi(False)
            End If


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaGrigliaPU(ByVal esercizioFinanziario As String)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SELECT ID, (CASE " _
                                & "           WHEN PREZZO = '9' AND TIPOLOGIA = 'B' THEN 'PU9B' " _
                                & "           WHEN PREZZO = '9' AND TIPOLOGIA = 'A' THEN 'PU9A' " _
                                & "           WHEN PREZZO = '2' THEN 'PU2' " _
                                & "           WHEN PREZZO = '3' THEN 'PU3' " _
                                & "           WHEN PREZZO = '7' THEN 'PU7' " _
                                & "           WHEN PREZZO = '1' THEN 'PU1' " _
                                & "           WHEN PREZZO = '8' THEN 'PU8' " _
                                & "           WHEN PREZZO = '10' THEN 'PU10' " _
                                & "           WHEN PREZZO = '12' AND TIPOLOGIA = '1' THEN 'PU12A' " _
                                & "           WHEN PREZZO = '12' AND TIPOLOGIA = '2' THEN 'PU12B' " _
                                & "           WHEN PREZZO = '12' AND TIPOLOGIA = '3' THEN 'PU12C' " _
                                & "           WHEN PREZZO = '13' THEN 'PU13' " _
                                & "        END) " _
                                & "          AS PREZZO_UNITARIO, UM, " _
                                & " (CASE " _
                                & "           WHEN PREZZO = '9' AND TIPOLOGIA = 'B' THEN 'PU00000009B' " _
                                & "           WHEN PREZZO = '9' AND TIPOLOGIA = 'A' THEN 'PU00000009A' " _
                                & "           WHEN PREZZO = '2' THEN 'PU00000002' " _
                                & "           WHEN PREZZO = '3' THEN 'PU00000003' " _
                                & "           WHEN PREZZO = '7' THEN 'PU00000007' " _
                                & "           WHEN PREZZO = '1' THEN 'PU00000001' " _
                                & "           WHEN PREZZO = '8' THEN 'PU00000008' " _
                                & "           WHEN PREZZO = '10'THEN 'PU00000010' " _
                                & "           WHEN PREZZO = '12' AND TIPOLOGIA = '1' THEN 'PU00000012A' " _
                                & "           WHEN PREZZO = '12' AND TIPOLOGIA = '2' THEN 'PU00000012B' " _
                                & "           WHEN PREZZO = '12' AND TIPOLOGIA = '3' THEN 'PU00000012C' " _
                                & "           WHEN PREZZO = '13' THEN 'PU00000013' " _
                                & "       END) AS ORDINE, " _
                                & "       DESCRIZIONE, " _
                                & "        IMPORTO " _
                                & "  FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                & " WHERE  (  PREZZO = '7' " _
                                & "       OR (PREZZO = '9' AND TIPOLOGIA = 'A') " _
                                & "       OR PREZZO = '2' " _
                                & "       OR PREZZO = '3' " _
                                & "       OR (PREZZO = '9' AND TIPOLOGIA = 'B') " _
                                & "       OR PREZZO = '1' " _
                                & "       OR PREZZO = '8' " _
                                & "       OR PREZZO = '10' " _
                                & "       OR (PREZZO = '12' AND TIPOLOGIA = '1') " _
                                & "       OR (PREZZO = '12' AND TIPOLOGIA = '2') " _
                                & "       OR (PREZZO = '12' AND TIPOLOGIA = '3') " _
                                & "       OR PREZZO = '13' ) " _
                                & "       AND ID_ESERCIZIO_FINANZIARIO = " & esercizioFinanziario
            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Item("DT_PU") = DT
            dgvPrezziUnitari.Rebind()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaGrigliaPU - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub dgvPrezziUnitari_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvPrezziUnitari.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_PU")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvPrezziUnitari_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub EsportaPrezziUnitari_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_PU")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        dtNuova.Columns.RemoveAt(1)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "PREZZI_UNITARI", "PREZZI_UNITARI", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvPrezziUnitari.Rebind()
    End Sub

    Protected Sub RefreshPrezziUnitari_Click(sender As Object, e As System.EventArgs)
        AggiornaValoriPU()
        dgvPrezziUnitari.Rebind()
    End Sub

    Private Sub AggiornaValoriPU()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_PU"), Data.DataTable)
            Dim row As Data.DataRow
            For Each item As GridDataItem In dgvPrezziUnitari.Items
                Dim prezzoUnitario As String = CType(item.FindControl("txtPrezzoUnitario"), RadNumericTextBox).Text
                row = dt.Select("id = " & item("ID").Text)(0)
                row.Item("IMPORTO") = par.PuntiInVirgole(par.IfEmpty(prezzoUnitario, 0))
            Next
            Session.Item("DT_PU") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaValoriComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnSalva_Click(sender As Object, e As EventArgs) Handles btnSalva.Click
        Try
            Dim dt As Data.DataTable = Session.Item("DT_PU")
            AggiornaValoriPU()
            connData.apri()
            For Each riga As Data.DataRow In dt.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " SET    IMPORTO       = " & par.insDbValue(par.IfNull(riga.Item("IMPORTO"), 0), False) & " " _
                                    & " WHERE  ID            = " & riga.Item("ID")
                par.cmd.ExecuteNonQuery()
            Next
            RadNotificationNote.Text = "Operazione completata!"
            RadNotificationNote.Show()
            connData.chiudi(True)
            CaricaGrigliaPU(cmbEsercizio.SelectedValue)
            dgvPrezziUnitari.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub cmbEsercizio_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbEsercizio.SelectedIndexChanged
        CaricaGrigliaPU(cmbEsercizio.SelectedValue)
    End Sub


    Private Sub btnAnnulla_Click(sender As Object, e As EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub
End Class
