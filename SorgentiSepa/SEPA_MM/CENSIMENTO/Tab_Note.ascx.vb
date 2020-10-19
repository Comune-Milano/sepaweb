
Partial Class CENSIMENTO_Tab_Note
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

       
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If DirectCast(Me.Page.FindControl("id_stato"), HiddenField).Value = 2 Then
            sola_lettura.Value = 1
        End If

        If Not IsPostBack Then
            ControllaStato()
        End If
    End Sub

    Private Sub caricaNote()
        Try
            Dim dt As New Data.DataTable()
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT SL_SLOGGIO.NOTE FROM SISCOM_MI.SL_SLOGGIO WHERE ID = " & DirectCast(Me.Page.FindControl("id_sloggio"), HiddenField).Value & ""

            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                txtNote.Text = par.IfNull(myReader3("NOTE"), "")
            End If
            myReader3.Close()
            If sola_lettura.Value = 1 Then
                txtNote.Enabled = False
            End If
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub ControllaStato()
        Try
            '*****************APERTURA CONNESSIONE***************

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'par.cmd.CommandText = "SELECT SL_SLOGGIO.STATO_VERBALE AS ST_VERB FROM SISCOM_MI.SL_SLOGGIO WHERE SL_SLOGGIO.ID = " & id_sloggio.Value

            'Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader3.Read Then
            '    stato_verb.Value = par.IfNull(myReader3("ST_VERB"), "")
            'End If
            'myReader3.Close()

            If DirectCast(Me.Page.FindControl("id_stato"), HiddenField).Value >= 0 And DirectCast(Me.Page.FindControl("stato_verb"), HiddenField).Value = 0 Then
            Else
                caricaNote()
            End If

            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub




End Class
