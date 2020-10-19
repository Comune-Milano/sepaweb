
Partial Class NuovoTipoAllegato
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            tipo.Value = Request.QueryString("T")
        End If
    End Sub

    Protected Sub BtnConfermacomplesso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConfermacomplesso.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            If Trim(txtDescrizione.Text) <> "" Then

                par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MODELLI_ALLEGATI WHERE upper(DESCRIZIONE)='" & UCase(par.PulisciStrSql(txtDescrizione.Text)) & "' and tipo=" & tipo.Value
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.HasRows = True Then
                    lblAvviso.Text = "Tipologia già presente in archivio!"
                    myReader5.Close()
                Else
                    myReader5.Close()
                    par.cmd.CommandText = "select MAX(ID) FROM SISCOM_MI.TAB_MODELLI_ALLEGATI"
                    Dim myReader6 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader6.Read Then
                        Dim indice As Long = 0
                        indice = par.IfNull(myReader6(0), 0) + 1

                        par.cmd.CommandText = "insert into SISCOM_MI.TAB_MODELLI_ALLEGATI (ID,COD,DESCRIZIONE,tipo) VALUES (" & indice & ",'" & Format(indice, "000") & "','" & par.PulisciStrSql(txtDescrizione.Text) & "'," & tipo.Value & ")"
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")
                    End If
                    myReader6.Close()

                End If
            Else
                Response.Write("<script>alert('Inserire la descrizione! Operazione non effettuata.');</script>")
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblAvviso.Visible = True
            lblAvviso.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
