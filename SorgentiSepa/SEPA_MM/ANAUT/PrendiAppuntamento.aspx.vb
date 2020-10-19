
Partial Class ANAUT_PrendiAppuntamento
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim strScript As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0
        txtDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        If IsPostBack = False Then
            txtDal.Text = Format(Now, "dd/MM/yyyy")
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Label2.Visible = False
        Label2.Text = ""
        If txtDal.Text <> "" And Len(txtDal.Text) = 10 Then
            Try

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & Request.QueryString("IC") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'CONVOCAZIONE PRESA IN CARICO DA AUCM IL " & txtDal.Text & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU SET CARICO_AUSI=1,DATA_AUSI='" & par.AggiustaData(txtDal.Text) & "' WHERE ID=" & Request.QueryString("IC")
                par.cmd.ExecuteNonQuery()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                strScript = "<script language='javascript'>alert('Operazione effettuata con successo.');self.close();" _
           & "</script>"
                Response.Write(strScript)

            Catch ex As Exception
                Label2.Visible = True
                Label2.Text = ex.Message
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        Else
            Label2.Visible = True
            Label2.Text = "Data non valida!"
        End If
    End Sub
End Class
