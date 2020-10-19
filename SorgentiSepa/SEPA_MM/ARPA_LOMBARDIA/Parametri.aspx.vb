Imports Telerik.Web.UI

Partial Class ARPA_LOMBARDIA_Parametri
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CaricaParametri()
        End If
    End Sub
    Private Sub CaricaParametri()
        Try
            par.cmd.CommandText = "SELECT ID, VALORE FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_PARAMETRI"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            For Each row As Data.DataRow In dt.Rows
                Select Case row.Item("ID").ToString
                    Case "1"
                        txtCodiceFiscaleEnteProprietario.Text = row.Item("VALORE").ToString
                End Select
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Parametri - CaricaParametri - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            Dim Errori As String = ""
            If String.IsNullOrEmpty(Trim(txtCodiceFiscaleEnteProprietario.Text)) Then
                If String.IsNullOrEmpty(Trim(Errori)) Then
                    Errori = "- Definire il Codice Fiscale Ente Proprietario;"
                Else
                    Errori &= "<br>" & "- Definire il Codice Fiscale Ente Proprietario;"
                End If
            Else
                If Len(Trim(txtCodiceFiscaleEnteProprietario.Text)) < 11 Then
                    If String.IsNullOrEmpty(Trim(Errori)) Then
                        Errori = "- Definire il Codice Fiscale Ente Proprietario corretto;"
                    Else
                        Errori &= "<br>" & "- Definire il Codice Fiscale Ente Proprietario corretto;"
                    End If
                End If
            End If
            If String.IsNullOrEmpty(Trim(Errori)) Then
                connData = New CM.datiConnessione(par, False, False)
                connData.apri(True)
                par.cmd.CommandText = "UPDATE " & CType(Me.Master, Object).StringaSiscom & "ARPA_PARAMETRI SET VALORE = " & par.insDbValue(txtCodiceFiscaleEnteProprietario.Text, True) & " WHERE ID = 1"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
                RadNotificationNote.Text = par.Messaggio_Operazione_Eff
                RadNotificationNote.Show()
            Else
                CType(Me.Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert(Errori, 450, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Parametri - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
