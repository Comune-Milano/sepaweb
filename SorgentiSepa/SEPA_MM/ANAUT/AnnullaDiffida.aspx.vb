
Partial Class ANAUT_AnnullaDiffida
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "SELECT * from utenza_file_diffide where id=" & Request.QueryString("ID")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDiffida.Text = "Data Creazione:" & par.FormattaData(Mid(myReader("data_creazione"), 1, 8)) & "<br />Protocollo:" & myReader("protocollo")
                lblDataCreazione.Text = myReader("data_creazione")
                lblFile.Text = par.IfNull(myReader("nome_file"), "")
                lblAU.Text = par.IfNull(myReader("ID_AU"), "-1")
                lblTipo.Text = par.IfNull(myReader("TIPO"), "")
            End If
            myReader.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblDiffida.Text = ex.Message
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If txtNote.Text <> "" Then
            Try
                lblnote.Text = ""
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.EVENTI_CONTRATTI WHERE ID_CONTRATTO IN (SELECT id_contratto FROM UTENZA_FILE_DIFFIDE_DETT where id_file_diffide=" & Request.QueryString("ID") & ") AND DATA_ORA='" & lblDataCreazione.Text & "' AND COD_EVENTO='F226'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.POSTALER WHERE ID_LETTERA IN (SELECT ID FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO IN (SELECT id_contratto FROM UTENZA_FILE_DIFFIDE_DETT where id_file_diffide=" & Request.QueryString("ID") & ") AND DATA_GENERAZIONE='" & Mid(lblDataCreazione.Text, 1, 8) & "' AND TIPO=" & lblTipo.Text & " AND ID_AU=" & lblAU.Text & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO IN (SELECT id_contratto FROM UTENZA_FILE_DIFFIDE_DETT where id_file_diffide=" & Request.QueryString("ID") & ") AND DATA_GENERAZIONE='" & Mid(lblDataCreazione.Text, 1, 8) & "' AND TIPO=" & lblTipo.Text & " AND ID_AU=" & lblAU.Text
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "DELETE FROM UTENZA_FILE_DIFFIDE_DETT WHERE ID_FILE_DIFFIDE=" & Request.QueryString("ID")
                'par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE UTENZA_FILE_DIFFIDE SET DATA_ANNULLO='" & Format(Now, "yyyyMMdd") & "',NOTE='" & par.PulisciStrSql(txtNote.Text) & "' WHERE ID=" & Request.QueryString("ID")
                par.cmd.ExecuteNonQuery()

                'IO.File.Delete(Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\DIFFIDE\") & lblFile.Text)

                'par.myTrans.Rollback()
                par.myTrans.Commit()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione effettuata!');self.close();</script>")

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)

            End Try
        Else
            lblnote.Text = "Inserire una motivazione"
        End If
    End Sub
End Class
