
Partial Class CALL_CENTER_RicercaEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String = ""
    ''Dim par As New CM.Global
    Dim edificio As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where SISCOM_MI.EDIFICI.id <> 1 order by denominazione asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_edificio"), " "), par.IfNull(myReader1("id"), -1)))
                End While

                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                cmbEdificio.Text = "-1"
                TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            Else
                Exit Sub
            End If


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca2.Click
        Response.Redirect("ElencoSegnalazioni.aspx?E=" & par.PulisciStrSql(cmbEdificio.SelectedItem.Value))
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        HelpRicerca()
    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                Me.cmbEdificio.SelectedValue = Me.ListEdifci.SelectedValue.ToString
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.TextBox1.Value = 1
                Me.LblNoResult.Visible = False
            Else
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.LblNoResult.Visible = False
                Me.TextBox1.Value = 1
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Me.TextBox1.Value = 1
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()
    End Sub


    Private Sub HelpRicerca()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
                Me.ListEdifci.Items.Clear()

                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' order by denominazione asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
            End If
            If ListEdifci.Items.Count = 0 Then
                Me.LblNoResult.Visible = True
            Else
                Me.LblNoResult.Visible = False
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.TextBox1.Value = 2

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

End Class
