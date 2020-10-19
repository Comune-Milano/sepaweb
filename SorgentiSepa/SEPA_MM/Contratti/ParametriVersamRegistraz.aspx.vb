
Partial Class Contratti_ParametriVersamRegistraz
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CaricaDati()
        End If
    End Sub

    Private Sub CaricaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * from SISCOM_MI.PARAMETRI_VERSAMENTO_XML ORDER BY ID DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                txtABI.Text = par.IfNull(dt.Rows(0).Item("CODICE_ABI"), "")
                txtCAB.Text = par.IfNull(dt.Rows(0).Item("CODICE_CAB"), "")
                txtCIN.Text = par.IfNull(dt.Rows(0).Item("CODICE_CIN"), "")
                txtNumCC.Text = par.IfNull(dt.Rows(0).Item("NUM_CONTO_CORR"), "")
                txtCFTitolare.Text = par.IfNull(dt.Rows(0).Item("COD_FISC_TITOLARE_CC"), "")
                idVersamento.Value = par.IfNull(dt.Rows(0).Item("ID"), "")
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " CaricaDati() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            'If errore.Value = "0" Then
            Dim StrUpdate As String = ""
            Dim condizione As String = ""
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If idVersamento.Value <> "0" Then
                condizione = " WHERE ID=" & idVersamento.Value & ""

                StrUpdate = " UPDATE SISCOM_MI.PARAMETRI_VERSAMENTO_XML " _
                & "SET " _
                & "CODICE_ABI= '" & par.IfEmpty(txtABI.Text, "") & "'," _
                & "CODICE_CAB= '" & par.IfEmpty(txtCAB.Text, "") & "'," _
                & "CODICE_CIN= '" & par.IfEmpty(txtCIN.Text.ToString, "") & "'," _
                & "NUM_CONTO_CORR= '" & par.IfEmpty(txtNumCC.Text, "") & "'," _
                & "COD_FISC_TITOLARE_CC = '" & par.PulisciStrSql(par.IfEmpty(txtCFTitolare.Text, "")).ToUpper & "'" & condizione
                par.cmd.CommandText = StrUpdate
            Else
                StrUpdate = "INSERT INTO SISCOM_MI.PARAMETRI_VERSAMENTO_XML (ID, CODICE_ABI, CODICE_CAB, CODICE_CIN, NUM_CONTO_CORR, COD_FISC_TITOLARE_CC) VALUES (1,'" & txtABI.Text & "' ,'" & txtCAB.Text & "' ,'" & txtCIN.Text.ToUpper & "' ,'" & txtNumCC.Text & "' ,'" & txtCFTitolare.Text.ToUpper & "' )"
                par.cmd.CommandText = StrUpdate
            End If

            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaDati()
            'End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " SalvaDati() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
