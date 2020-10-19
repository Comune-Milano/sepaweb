
Partial Class ANAUT_RicercaNonRispondenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub CheckBox3_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Settacheck()
    End Sub

    Private Function SettaCheck()
        If CheckBox3.Checked = True Then
            CheckBox4.Checked = True
            CheckBox5.Checked = True
            CheckBox6.Checked = True
            CheckBox7.Checked = True
            CheckBox8.Checked = True
            CheckBox9.Checked = True
            CheckBox10.Checked = True
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
            CheckBox6.Enabled = False
            CheckBox7.Enabled = False
            CheckBox8.Enabled = False
            CheckBox9.Enabled = False
            CheckBox10.Enabled = False
        Else
            CheckBox4.Checked = True
            CheckBox5.Checked = True
            CheckBox6.Checked = True
            CheckBox7.Checked = True
            CheckBox8.Checked = True
            CheckBox9.Checked = True
            CheckBox10.Checked = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
            CheckBox6.Enabled = True
            CheckBox7.Enabled = True
            CheckBox8.Enabled = True
            CheckBox9.Enabled = True
            CheckBox10.Enabled = True
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi where id>1 and stato=1 ORDER BY id desc", "DESCRIZIONE", "ID")
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from UTENZA_SPORTELLI where ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & cmbBando.SelectedItem.Value & ") ORDER BY DESCRIZIONE asc", "DESCRIZIONE", "ID")
                'cmbFiliale.Items.Add(New ListItem("TUTTI", "TUTTI"))

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End Try
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        ADal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        AAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If cmbFiliale.SelectedItem.Value <> "-1" Then
            Response.Write("<script>location.replace('RisultatoNonRispondenti.aspx?BA=" & cmbBando.SelectedItem.Value & "&NSP=" & par.Cripta(cmbFiliale.SelectedItem.Text) & "&FI=" & cmbFiliale.SelectedItem.Value & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "&ADAL=" & par.AggiustaData(ADal.Text) & "&AAL=" & par.AggiustaData(AAl.Text) & "&ES=" & CheckBox1.Checked & "&GD=" & CheckBox2.Checked & "&M1=" & CheckBox3.Checked & "&M2=" & CheckBox4.Checked & "&M3=" & CheckBox5.Checked & "&M4=" & CheckBox6.Checked & "&M5=" & CheckBox7.Checked & "&M6=" & CheckBox8.Checked & "&M7=" & CheckBox9.Checked & "&M8=" & CheckBox10.Checked & "');</script>")
        Else
            Response.Write("<script>alert('Scelta non valida!');</script>")
        End If
    End Sub
End Class
