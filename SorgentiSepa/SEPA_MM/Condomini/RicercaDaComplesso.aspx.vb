
Partial Class CENSIMENTO_Report_RicercaDaComplesso
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim tipiUI As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        If Request.QueryString("U") = "2" Then
            lbltitolo.Text = "Totalizzazioni dati patrimoniali per stato giuridico delle UI"
            lbltitolo2.Text = "Indicare il Complesso Immobiliare di cui si intende visualizzare le tot. dei dati patrimoniali e la tipologia dell'UI"
            lbltipoUI.Visible = True
            cmbTipoUI.Visible = True
        End If
        If Request.QueryString("U") = "3" Then
            lbltitolo.Text = "Totalizzazioni dati patrimoniali per gruppi di UI"
            lbltitolo2.Text = "Indicare il Complesso Immobiliare di cui si intende visualizzare le tot. dei dati patrimoniali"
        End If

        
        Try
            If Not IsPostBack Then

                If Request.QueryString("U") = "1" Then
                    lbltitolo.Text = "Totalizzazioni dati patrimoniali per tipo di UI"
                    lbltitolo2.Text = "Indicare il Complesso Immobiliare di cui si intende visualizzare le tot. dei dati patrimoniali"
                    lblTipi.Visible = True
                    chTipoUI.Visible = True
                    chSelezione.Visible = True
                    checkTipoUI()
                End If

                If Request.QueryString("U") = "2" Then
                    tipoUI()
                End If

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                cmbComplesso.Items.Add(New ListItem("TUTTO IL PATRIMONIO", "-1"))
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and SISCOM_MI.complessi_immobiliari.id <> 1 order by denominazione asc "

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read
                    cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), "--") & " - - " & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))
                End While

                myReader1.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                Exit Sub
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub


    Protected Sub btnAvanti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAvanti.Click '"&UI=" & chTipoUI.SelectedItem.Value &

        If Request.QueryString("U") = "1" Then
            Dim primo As Boolean = False
            For Each chtipo As ListItem In chTipoUI.Items
                If chtipo.Selected = True Then
                    If primo = False Then
                        tipiUI = chtipo.Value & ","
                        primo = True
                    Else
                        tipiUI = tipiUI & chtipo.Value & ","
                    End If
                End If
            Next
        End If

        If Request.QueryString("U") = "1" And tipiUI = "" Then
            Response.Write("<script>alert('Selezionare almeno una tipologia di UI');</script>")
            Exit Sub
        ElseIf Request.QueryString("U") = "1" And tipiUI <> "" Then
            tipiUI = Mid(tipiUI, 1, Len(tipiUI) - 1)
        End If
        If Request.QueryString("U") = "1" Then
            Response.Write("<script>window.open('TotPatrimTipoUI3.aspx?C=" & par.PulisciStrSql(cmbComplesso.SelectedItem.Value) & "&TOT=" & cmbTotali.SelectedItem.Value & "&UI=" & tipiUI & "')</script>")
        End If
        If Request.QueryString("U") = "2" Then
            Response.Write("<script>window.open('TotPatrimStatoUI.aspx?C=" & par.PulisciStrSql(cmbComplesso.SelectedItem.Value) & "&T=" & par.PulisciStrSql(cmbTipoUI.SelectedItem.Value) & "&D=" & par.PulisciStrSql(cmbTipoUI.SelectedItem.Text) & "&TOT=" & cmbTotali.SelectedItem.Value & "')</script>")
        End If
        If Request.QueryString("U") = "3" Then
            Response.Write("<script>window.open('TotPatrimGruppiUI.aspx?C=" & par.PulisciStrSql(cmbComplesso.SelectedItem.Value) & "&TOT=" & cmbTotali.SelectedItem.Value & "')</script>")
        End If
    End Sub

    Private Sub tipoUI()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            cmbTipoUI.Items.Add(New ListItem("TUTTE", "-1"))
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD ASC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbTipoUI.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), "").ToString.ToUpper, par.IfNull(myReader1("COD"), "")))
            End While
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub checkTipoUI()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI.COD ASC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                chTipoUI.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), "").ToString.ToUpper, par.IfNull(myReader1("COD"), "")))
            End While
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub chSelezione_CheckedChanged(sender As Object, e As System.EventArgs) Handles chSelezione.CheckedChanged
        If chSelezione.Checked = True Then
            For Each li As ListItem In chTipoUI.Items
                li.Selected = True
            Next
        Else
            For Each li As ListItem In chTipoUI.Items
                li.Selected = False
            Next
        End If
    End Sub
End Class
