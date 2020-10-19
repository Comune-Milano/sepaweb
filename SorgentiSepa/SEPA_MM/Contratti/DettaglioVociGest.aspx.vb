
Partial Class Contratti_DettaglioVociGest
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            idBoll = Request.QueryString("IDBOLL")
            CaricaDettagli()
        End If
    End Sub

    Private Sub CaricaDettagli()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim dtPerDatagr As New Data.DataTable

            Dim RIGA As System.Data.DataRow

            dtPerDatagr.Columns.Add("ID")
            dtPerDatagr.Columns.Add("DESCR_VOCE")
            dtPerDatagr.Columns.Add("IMPORTO")
            
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.id as ID_VOCE_GEST,IMPORTO,BOL_BOLLETTE_GEST.*,TIPO_BOLLETTE_GEST.descrizione AS TIPOBOLL,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST,SISCOM_MI.T_VOCI_BOLLETTA" _
                & " WHERE BOL_BOLLETTE_GEST.ID=BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI_GEST.ID_VOCE AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND BOL_BOLLETTE_GEST.ID=" & idBoll
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtDocGest As New Data.DataTable
            da.Fill(dtDocGest)
            da.Dispose()

            If par.IfNull(dtDocGest.Rows(0).Item("IMPORTO"), 0) < 0 Then
                lblDettGest.Text = "<ul><li>" & par.IfNull(dtDocGest.Rows(0).Item("TIPOBOLL"), "") & " - Credito pari a <u>" & Format(CDec(par.IfNull(dtDocGest.Rows(0).Item("IMPORTO_TOTALE"), 0) * (-1)), "##,##0.00") & "</u> € </li></ul>"
            Else
                lblDettGest.Text = "<ul><li>" & par.IfNull(dtDocGest.Rows(0).Item("TIPOBOLL"), "") & " - Debito pari a <u>" & Format(CDec(par.IfNull(dtDocGest.Rows(0).Item("IMPORTO_TOTALE"), 0)), "##,##0.00") & "</u> € </li></ul>"
            End If

            For i As Integer = 0 To dtDocGest.Rows.Count - 1
                RIGA = dtPerDatagr.NewRow()

                RIGA.Item("ID") = idBoll
                RIGA.Item("DESCR_VOCE") = par.IfNull(dtDocGest.Rows(i).Item("DESCRIZIONE"), "")
                RIGA.Item("IMPORTO") = Format(CDec(par.IfNull(dtDocGest.Rows(i).Item("IMPORTO"), 0)), "##,##0.00")

                dtPerDatagr.Rows.Add(RIGA)
            Next

            DataGrDettaglio.DataSource = dtPerDatagr
            DataGrDettaglio.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property idBoll() As Long
        Get
            If Not (ViewState("par_idBoll") Is Nothing) Then
                Return CLng(ViewState("par_idBoll"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBoll") = value
        End Set
    End Property
End Class
