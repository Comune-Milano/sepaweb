
Partial Class Contratti_SceltaIstat
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)



                par.cmd.CommandText = "select * from siscom_mi.variazioni_istat order by id desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then


                    Label1.Text = "<span style='font-size: 10pt; font-family: Arial'>Ultimo Aggiornamento ISTAT inserito:<br /></span><br /><table width='100%'><tr><td style='width: 104px'><span style='font-size: 10pt; font-family: Arial'>Anno</span></td><td><span style='font-size: 10pt; font-family: Arial'><strong>" & Mid(myReader("data_validita"), 1, 4) _
                    & "</strong></span></td><td style='width: 3px'></td></tr><tr><td style='width: 104px'><span style='font-size: 10pt; font-family: Arial'>Mese</span></td><td><span style='font-size: 10pt; font-family: Arial'><strong>" & par.ConvertiMese(Mid(myReader("data_validita"), 5, 2)) _
                    & "</strong></span></td><td style='width: 3px'></td></tr><tr><td style='width: 104px'><span style='font-size: 10pt; font-family: Arial'>Variazione 100%</span></td><td><span style='font-size: 10pt; font-family: Arial'><strong>" & myReader("var_100_annuale") _
                    & "</strong></span></td><td style='width: 3px'></td></tr><tr><td style='width: 104px'><span style='font-size: 10pt; font-family: Arial'>Variazione 75%</span></td><td><span style='font-size: 10pt; font-family: Arial'><strong>" & myReader("var_75_annuale") _
                    & "</strong></span></td><td style='width: 3px'></td></tr></table>"
                End If
                myReader.Close()

                cmbMese.Items.Add(New ListItem(par.ConvertiMese(Month(DateAdd("M", 1, Now))) & " " & Year(DateAdd("M", 1, Now)), CStr(Year(DateAdd("M", 1, Now)) & Format(Month(DateAdd("M", 1, Now)), "00"))))


                par.OracleConn.Close()
            Catch ex As Exception
                par.OracleConn.Close()
                Label3.Visible = True
                Label3.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub imgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Response.Redirect("SceltaIstat1.aspx?P=" & cmbMese.SelectedItem.Value)
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
