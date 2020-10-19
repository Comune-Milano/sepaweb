Imports System.IO

Partial Class Contratti_ContCalore_ElAnomalie
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property dataTableAnomalie() As Data.DataTable
        Get
            If Not (ViewState("dataTableAnomalie") Is Nothing) Then
                Return ViewState("dataTableAnomalie")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableAnomalie") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            If Request.QueryString("TIPO") = "NUOVO" Then
                par.caricaComboBox("SELECT CONT_CALORE_ANNO.ID, (anno ||' - '||TIPO_CALCOLO_CONT_CALORE.DESCRIZIONE) as anno FROM siscom_mi.CONT_CALORE_ANNO,SISCOM_MI.TIPO_CALCOLO_CONT_CALORE WHERE TIPO_CALCOLO_CONT_CALORE.id = ID_STATO and id_stato = 1", cmbContCalore, "ID", "anno", True)
            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                par.caricaComboBox("SELECT CONT_CALORE_ANNO.ID, (anno ||' - '||TIPO_CALCOLO_CONT_CALORE.DESCRIZIONE) as anno FROM siscom_mi.CONT_CALORE_ANNO,SISCOM_MI.TIPO_CALCOLO_CONT_CALORE WHERE TIPO_CALCOLO_CONT_CALORE.id = ID_STATO and id_stato = 3", cmbContCalore, "ID", "anno", True)

            End If

            CaricaAnomalie()

        End If
    End Sub

    Private Sub CaricaAnomalie()

        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select cont_calore_anomalie.id_cont_calore,cont_calore_anomalie.id_contratto,cont_calore_anomalie.id_unita," _
                & "rapporti_utenza.cod_contratto, unita_immobiliari.cod_unita_immobiliare,cont_calore_anomalie.motivazione " _
                & "from siscom_mi.cont_calore_anomalie, siscom_mi.rapporti_utenza, siscom_mi.unita_immobiliari " _
                & "where id_cont_calore= " & idConCal.Value & " and cont_calore_anomalie.id_contratto = rapporti_utenza.id(+) and " _
                & "cont_calore_anomalie.id_unita = unita_immobiliari.id(+)"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dataTableAnomalie = New Data.DataTable
            'Dim dt As New Data.DataTable
            da.Fill(dataTableAnomalie)
            Me.dgvAnomalie.DataSource = dataTableAnomalie
            Me.dgvAnomalie.DataBind()

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try

    End Sub

    Public Sub cmbContCalore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbContCalore.SelectedIndexChanged
        If cmbContCalore.SelectedValue <> "-1" Then
            idConCal.Value = cmbContCalore.SelectedValue
        Else
            idConCal.Value = 0
        End If

        CaricaAnomalie()

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If dgvAnomalie.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(dataTableAnomalie, Me.dgvAnomalie, "ExportAnomalieContCalore", , True, , False)
                If File.Exists(Server.MapPath("..\..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/..\/FileTemp/" & nomefile, True)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun risultato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnSelectAll_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
End Class
