
Partial Class Contabilita_Flussi_RicercaPrelievi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select distinct periodo from siscom_mi.INCASSI_MESI order by periodo desc"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReaderA.Read
                    If Mid(par.IfNull(myReaderA("periodo"), ""), 5, 2) <> 12 Then
                        cmbPeriodo.Items.Add(New ListItem(par.ConvertiMese(Mid(par.IfNull(myReaderA("periodo"), ""), 5, 2) + 1) & " " & Mid(par.IfNull(myReaderA("periodo"), ""), 1, 4), par.IfNull(myReaderA("periodo"), -1)))
                    Else
                        cmbPeriodo.Items.Add(New ListItem(par.ConvertiMese("01") & " " & Mid(par.IfNull(myReaderA("periodo"), ""), 1, 4) + 1, par.IfNull(myReaderA("periodo"), -1)))
                    End If
                Loop
                myReaderA.Close()





                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        If cmbPeriodo.SelectedIndex <> -1 Then
            Response.Write("<script>window.open('Prelievi.aspx?T=" & par.Cripta(cmbPeriodo.SelectedItem.Text) & "&P=" & cmbPeriodo.SelectedItem.Value & "','Prelievi','');</script>")
        End If
    End Sub
End Class
