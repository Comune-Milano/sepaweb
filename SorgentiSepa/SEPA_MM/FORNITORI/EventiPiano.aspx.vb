
Imports Telerik.Web.UI

Partial Class FORNITORI_EventiPiano
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

    Private Sub RadGridEventi_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGridEventi.NeedDataSource
        Try
            Dim Query As String = EsportaQueryPiano()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            RadGridEventi.DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - RadGridEventi_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Function EsportaQueryPiano() As String
        Dim idCronoprogramma As Integer = Request.QueryString("idcrono")
        Dim idEdificio As Integer = Request.QueryString("idedificio")
        Dim indice As Integer = Request.QueryString("indice")
        'Dim stringa As String = " SELECT (SELECT OPERATORE FROM SEPA.OPERATORI WHERE ID = ID_OPERATORE) AS OPERATORE, " _
        '    & " TO_DATE(DATA,'YYYYMMDD') AS DATA, " _
        '    & " TO_DATE(DATA_ORA_OPERAZIONE, 'YYYYMMDDHH24MISS') AS DATA_ORA " _
        '    & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE id_programma_attivita = " & idCronoprogramma _
        '    & " and indice =  " & indice _
        '    & " and id_edificio = (select id from siscom_mi.edifici where cod_edificio = " & idEdificio & ")"

        Dim stringa As String = "SELECT " _
                   & " SEPA.OPERATORI.OPERATORE, " _
                   & " TO_DATE(DATA_ORA, 'YYYYMMDDHH24MISS') AS DATA_EVENTO," _
                   & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, " _
                   & " (CASE WHEN (VALORE_NEW IS NOT NULL) THEN (EVENTI_PROGRAMMA_ATTIVITA.MOTIVAZIONE||'<br />'||'Valore attuale: '||VALORE_NEW||' - Valore precedente: '||VALORE_OLD) ELSE (EVENTI_PROGRAMMA_ATTIVITA.MOTIVAZIONE) END)  AS MOTIVAZIONE " _
                   & " FROM SISCOM_MI.EVENTI_PROGRAMMA_ATTIVITA, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI " _
                   & " WHERE EVENTI_PROGRAMMA_ATTIVITA.COD_EVENTO = TAB_EVENTI.COD " _
                   & " AND EVENTI_PROGRAMMA_ATTIVITA.ID_OPERATORE = OPERATORI.ID " _
                   & " AND ID_PROGRAMMA_ATTIVITA =  " & idCronoprogramma
        Return stringa




    End Function

    Private Sub FORNITORI_EventiPiano_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub
End Class
