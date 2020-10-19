' TAB APPALTO

Partial Class Tab_Appalto_OLD
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If
        End If
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.lblErrore.Visible = True
            'lblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub cmblotto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmblotto.SelectedIndexChanged
        Try
            If cmblotto.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                cmbservizio.Items.Clear()
                par.cmd.CommandText = "SELECT SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.LOTTI_SERVIZI " _
                & "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO AND SISCOM_MI.LOTTI_SERVIZI.ID=" & cmblotto.SelectedValue.ToString & " ORDER BY SISCOM_MI.TAB_SERVIZI.ID ASC, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                'cmbservizio.Items.Add(New ListItem(" ", -1))
                If myReader2.Read() Then
                    cmbservizio.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                End If
                myReader2.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
