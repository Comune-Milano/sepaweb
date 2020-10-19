
Partial Class Contratti_ParameReCaGest
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CaricaRadioButton()
        End If
    End Sub

    Private Sub CaricaRadioButton()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=41"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("VALORE"), 0) = 1 Then
                    rdbListRecaGest.SelectedValue = 1
                Else
                    rdbListRecaGest.SelectedValue = 0
                End If
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
            Dim StrUpdate As String = ""
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If rdbListRecaGest.SelectedValue = 1 Then

                StrUpdate = " UPDATE SISCOM_MI.PARAMETRI_BOLLETTA " _
                & "SET " _
                & "VALORE = '1' WHERE ID=41"
                par.cmd.CommandText = StrUpdate
            Else
                StrUpdate = " UPDATE SISCOM_MI.PARAMETRI_BOLLETTA " _
                & "SET " _
                & "VALORE = '0' WHERE ID=41"
                par.cmd.CommandText = StrUpdate
            End If

            par.cmd.ExecuteNonQuery()

            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaRadioButton()
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
