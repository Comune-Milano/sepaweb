
Partial Class Contabilita_DateCompensi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            If Request.QueryString("CHIAMA") = "PROPERTY" Then
                Me.lblTitle.Text = "Calcolo Compenso Giornaliero per il Property"

            ElseIf Request.QueryString("CHIAMA") = "FACILITY" Then
                Me.lblTitle.Text = "Calcolo Compenso Giornaliero per il Property ed il Facility"

            End If


            RiempiCampi()
        End If
    End Sub
    Private Sub RiempiCampi()
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT DISTINCT ANNO FROM SISCOM_MI.COMPENSI_ALER order by anno asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            cmbAnnoInizio.Items.Add(New ListItem(" ", -1))
            cmbAnnoFine.Items.Add(New ListItem(" ", -1))

            While myReader.Read
                cmbAnnoInizio.Items.Add(New ListItem(par.IfNull(myReader("ANNO"), " "), par.IfNull(myReader("ANNO"), "")))
                cmbAnnoFine.Items.Add(New ListItem(par.IfNull(myReader("ANNO"), " "), par.IfNull(myReader("ANNO"), "")))
            End While

            myReader.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Me.cmbMeseInizio.Items.Add(New ListItem(" ", -1))
            Me.cmbMeseFine.Items.Add(New ListItem(" ", -1))

            '*********************GENNAIO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("GENNAIO", "01"))
            Me.cmbMeseFine.Items.Add(New ListItem("GENNAIO", "01"))
            '*********************FEBBRAIO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("FEBBRAIO", "02"))
            Me.cmbMeseFine.Items.Add(New ListItem("FEBBRAIO ", "02"))
            '*********************MARZO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("MARZO", "03"))
            Me.cmbMeseFine.Items.Add(New ListItem("MARZO", "03"))
            '*********************APRILE**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("APRILE", "04"))
            Me.cmbMeseFine.Items.Add(New ListItem("APRILE", "04"))
            '*********************MAGGIO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("MAGGIO", "05"))
            Me.cmbMeseFine.Items.Add(New ListItem("MAGGIO", "05"))
            '*********************GIUGNO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("GIUGNO", "06"))
            Me.cmbMeseFine.Items.Add(New ListItem("GIUGNO", "06"))
            '*********************LUGLIO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("LUGLIO", "07"))
            Me.cmbMeseFine.Items.Add(New ListItem("LUGLIO", "07"))
            '*********************AGOSTO**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("AGOSTO", "08"))
            Me.cmbMeseFine.Items.Add(New ListItem("AGOSTO", "08"))
            '*********************SETTEMBRE**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("SETTEMBRE", "09"))
            Me.cmbMeseFine.Items.Add(New ListItem("SETTEMBRE", "09"))
            '*********************OTTOBRE**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("OTTOBRE", 10))
            Me.cmbMeseFine.Items.Add(New ListItem("OTTOBRE", 10))
            '*********************NOVEMBRE**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("NOVEMBRE", 11))
            Me.cmbMeseFine.Items.Add(New ListItem("NOVEMBRE", 11))
            '*********************DICEMBRE**********************
            Me.cmbMeseInizio.Items.Add(New ListItem("DICEMBRE", 12))
            Me.cmbMeseFine.Items.Add(New ListItem("DICEMBRE", 12))

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnCalcola_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCalcola.Click

        '*********************controllo mese**********************
        If Me.cmbMeseInizio.SelectedValue <> -1 And Me.cmbAnnoInizio.SelectedValue = -1 Then
            Response.Write("<script>alert('Definire l\'anno!')</script>")
            Exit Sub
        End If
        If Me.cmbMeseFine.SelectedValue <> -1 And Me.cmbAnnoFine.SelectedValue = -1 Then
            Response.Write("<script>alert('Definire l\'anno!')</script>")
            Exit Sub
        End If

        '*********************controllo anno**********************
        If Me.cmbAnnoInizio.SelectedValue <> -1 And Me.cmbMeseInizio.SelectedValue = -1 Then
            Response.Write("<script>alert('Definire il mese!')</script>")
            Exit Sub
        End If
        If Me.cmbAnnoFine.SelectedValue <> -1 And Me.cmbMeseFine.SelectedValue = -1 Then
            Response.Write("<script>alert('Definire il mese!')</script>")
            Exit Sub
        End If

        If (Me.cmbAnnoInizio.SelectedValue <> -1 And Me.cmbMeseInizio.SelectedValue <> -1) And (Me.cmbAnnoFine.SelectedValue <> -1 And Me.cmbMeseFine.SelectedValue <> -1) Then
            If Me.cmbAnnoInizio.SelectedValue & Me.cmbMeseInizio.SelectedValue > Me.cmbAnnoFine.SelectedValue & Me.cmbMeseFine.SelectedValue Then
                Response.Write("<script>alert('Intervallo non valido!')</script>")
                Exit Sub
            End If
        End If

        Response.Write("<script>window.open('CompMensiliFPAler.aspx?M_INIZIO=" & Me.cmbMeseInizio.SelectedValue & "&A_INIZIO=" & Me.cmbAnnoInizio.SelectedValue & "&M_FINE=" & Me.cmbMeseFine.SelectedValue & "&A_FINE=" & Me.cmbAnnoFine.SelectedValue & "&CHIAMA=FACILITY');</script>")


        'If Not String.IsNullOrEmpty(Me.txtDataInizio.Text) Then
        'If Not String.IsNullOrEmpty(Me.txtDataFine.Text) Then

        '    If par.AggiustaData(Me.txtDataFine.Text) < par.AggiustaData(Me.txtDataInizio.Text) Then
        '        Response.Write("<script>alert('Intervallo non valido!')</script>")
        '        Exit Sub
        '    End If


        'End If

        'If Request.QueryString("CHIAMA") = "PROPERTY" Then
        '    Response.Write("<script>alert('Funzione al momento non disponibile!')</script>")

        '    Response.Write("<script>window.open('CompensiFacProp.aspx?D_INIZIO=" & par.AggiustaData(par.IfEmpty(Me.txtDataInizio.Text, "")) & "&D_FINE=" & par.AggiustaData(par.IfEmpty(Me.txtDataFine.Text, "")) & "&CHIAMA=PROPERTY');</script>")
        'ElseIf Request.QueryString("CHIAMA") = "FACILITY" Then
        '    Response.Write("<script>window.open('CompensiFacProp.aspx?D_INIZIO=" & par.AggiustaData(par.IfEmpty(Me.txtDataInizio.Text, "")) & "&D_FINE=" & par.AggiustaData(par.IfEmpty(Me.txtDataFine.Text, "")) & "&CHIAMA=FACILITY');</script>")
        'End If

        ''Else
        ''    'Response.Write("<script>alert('Definire almeno la data iniziale!')</script>")

        ''End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub


End Class
