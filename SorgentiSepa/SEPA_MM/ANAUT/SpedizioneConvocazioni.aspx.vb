
Partial Class ANAUT_SpedizioneConvocazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaLista()
        End If
        txtData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Function CaricaLista()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter("SELECT DESCRIZIONE FROM SISCOM_MI.CONVOCAZIONI_AU_STAMPE WHERE DATA_STAMPA IS NULL ORDER BY DESCRIZIONE ASC", par.OracleConn)
            Dim ds As New Data.DataSet()

            da.Fill(ds, "CONVOCAZIONI_AU_STAMPE")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
     
    End Function

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If Len(txtData.Text) = 10 Then
            Try
                Dim oDataGridItem As DataGridItem
                Dim chkExport As System.Web.UI.WebControls.CheckBox
                Dim i As Integer = 0

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans



                For Each oDataGridItem In Me.DataGrid1.Items
                    chkExport = oDataGridItem.FindControl("ChSelezionato")
                    If chkExport.Checked Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU_STAMPE SET DATA_STAMPA='" & par.AggiustaData(txtData.Text) & "' WHERE DESCRIZIONE='" & par.PulisciStrSql(oDataGridItem.Cells(1).Text) & "'"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONVOCAZIONI_AU WHERE NOME_FILE='" & par.PulisciStrSql(oDataGridItem.Cells(1).Text) & "'"
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader2.Read
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONVOCAZIONI_AU_EVENTI (ID_CONVOCAZIONE,DATA_ORA,ID_OPERATORE,DESCRIZIONE) VALUES (" & myReader2("ID") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," & Session.Item("ID_OPERATORE") & ",'SPEDITA CONVOCAZIONE AU IN DATA " & txtData.Text & "')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & myReader2("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F225', ' IN DATA " & txtData.Text & "')"
                            par.cmd.ExecuteNonQuery()
                        Loop
                        myReader2.Close()

                        i = i + 1
                    End If
                Next
                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If i > 0 Then
                    Response.Write("<script>alert('Operazione Effettuata!');document.location.href = 'pagina_home.aspx';</script>")
                Else
                    Response.Write("<script>alert('Selezionare almeno un file dalla lista!');</script>")
                End If

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End Try
        Else
            Response.Write("<script>alert('Inserire una data di spedizione valida!');</script>")
        End If
    End Sub
End Class
