
Partial Class AMMSEPA_Controllo_Datafine
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM siscom_mi.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & UCase(txtCodice.Text) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                If myReader.Read Then
                    par.cmd.CommandText = "UPDATE siscom_mi.SOGGETTI_CONTRATTUALI SET data_fine='29991231' WHERE id_contratto=" & myReader("id") & " AND cod_tipologia_occupante='INTE'"
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione effettuata');</script>")
                End If
            Else
                Response.Write("Nessun contratto con questo codice!")
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                txtCodice.Visible = True
                Button1.Visible = True
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class
