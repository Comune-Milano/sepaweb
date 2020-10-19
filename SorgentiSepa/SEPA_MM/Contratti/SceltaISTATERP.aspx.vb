
Partial Class Contratti_SceltaISTATERP
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)


                par.cmd.CommandText = "select COD,DESCRIZIONE from SISCOM_MI.tipologia_contratto_locazione where cod in (select COD_TIPOLOGIA_CONTR_LOC from SISCOM_MI.rapporti_utenza where id in (select  id_contratto from SISCOM_MI.CANONI_EC where tipo_provenienza=(select max(id_tipo_provenienza) from utenza_bandi where stato=1))) order by descrizione Asc"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read
                    cmbTipo.Items.Add(New ListItem(par.IfNull(myReader("descrizione"), ""), par.IfNull(myReader("COD"), "")))
                Loop
                myReader.Close()
                cmbTipo.Items.Add(New ListItem("TUTTI", "0"))

                cmbAnno.Items.Add(New ListItem(Year(Now), Year(Now)))
                cmbAnno.Items.Add(New ListItem(Year(Now) + 1, Year(Now) + 1))
                cmbAnno.SelectedIndex = -1
                cmbAnno.Items.FindByValue(Year(Now) + 1).Selected = True
                cmbMese.Items.Add(New ListItem("Tutti i Mesi", "0"))
                cmbMese.Enabled = False
                For i = 1 To 12
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ADEGUAMENTO_ISTAT WHERE ANNO_MESE='" & cmbAnno.SelectedItem.Value & Format(i, "00") & "'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = False Then
                        cmbMese.Items.Add(New ListItem(par.ConvertiMese(i), CStr(i)))
                    End If
                    myReader.Close()
                Next

                par.cmd.Dispose()
                par.OracleConn.Close()
            Catch ex As Exception
                par.OracleConn.Close()
                Label3.Visible = True
                Label3.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub imgProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgProcedi.Click
        Response.Redirect("SceltaIstatERP1.aspx?A=" & cmbAnno.SelectedItem.Value & "&P=" & cmbMese.SelectedItem.Value & "&C=" & cmbTipo.SelectedItem.Value)
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
