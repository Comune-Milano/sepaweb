
Partial Class FORNITORI_PianoDettaglioCrono
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            connData.apri()

            connData.chiudi()
        End If
    End Sub


    Protected Sub RadGridPiani_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPiani.NeedDataSource
        Try
            Dim Query As String = EsportaQueryPiano()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            RadGridPiani.DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - RadGridPiani_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Function EsportaQueryPiano() As String
        Dim idCronoprogramma As Integer = Request.QueryString("idcrono")
        Dim idEdificio As Integer = Request.QueryString("idedificio")
        Dim indice As Integer = Request.QueryString("indice")
        Dim stringa As String = " SELECT (SELECT OPERATORE FROM SEPA.OPERATORI WHERE ID = ID_OPERATORE) AS OPERATORE, " _
            & " TO_DATE(DATA,'YYYYMMDD') AS DATA, " _
            & " TO_DATE(DATA_ORA_OPERAZIONE, 'YYYYMMDDHH24MISS') AS DATA_ORA " _
            & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE id_programma_attivita = " & idCronoprogramma _
            & " and indice =  " & indice _
            & " and id_edificio = (select id from siscom_mi.edifici where cod_edificio = " & idEdificio & ")"
        Return stringa
    End Function

    Private Sub FORNITORI_PianoDettaglioCrono_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub
End Class
