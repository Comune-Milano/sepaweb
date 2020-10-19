
Partial Class Contratti_Report_PagamentiPervenuti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Select Case Request.QueryString("TIPO")
                    Case "1"
                        lblTipo.Text = "Pagamenti pervenuti" 'per: Data Emissione"
                    Case "2"
                        lblTipo.Text = "Pagamenti pervenuti" 'per: Data Pagamento"
                    Case "3"
                        Label15.Text = "DATA DI SCADENZA"
                        lblTipo.Text = "Pagamenti non pervenuti" 'per: Data Emissione"
                    Case "4"
                        Label15.Text = "DATA DI SCADENZA"
                        lblTipo.Text = "Pagamenti non pervenuti" ' per: Data Scadenza"

                End Select


                par.cmd.CommandText = "SELECT complessi_immobiliari.id,(denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico)as denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.id in(Select id_complesso from siscom_mi.bol_bollette)  order by denominazione asc "
                CmbComplesso.Items.Add(New ListItem(" ", -1))

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader1.Read
                    CmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While

                myReader1.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaEdifici()

                'CaricaUnita()
                txtDataDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                txtDataDal0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataAl0.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                txtDataDal1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataAl1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If

    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Contabilita/pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            'If par.IfEmpty(par.AggiustaData(Me.txtDataDal.Text), "Null") = "Null" Or par.IfEmpty(par.AggiustaData(Me.txtDataAl.Text), "Null") = "Null" Then
            '    Response.Write("<script>alert('E\' necessario specificare entrabe le date!')</script>")
            '    Exit Sub
            'End If
            'If Me.txtDataAl.Text <> "" Then
            '    If par.AggiustaData(Me.txtDataDal.Text) > par.AggiustaData(Me.txtDataAl.Text) Then
            '        Response.Write("<script>alert('Intervallo non valido!')</script>")
            '        Exit Sub
            '    End If
            'End If


            Response.Write("<script>window.open('StampaPagamentiPerv.aspx?DAL1=" & par.AggiustaData(Me.txtDataDal1.Text) & "&AL1=" & par.AggiustaData(Me.txtDataAl1.Text) & "&DAL0=" & par.AggiustaData(Me.txtDataDal0.Text) & "&AL0=" & par.AggiustaData(Me.txtDataAl0.Text) & "&DAL=" & par.AggiustaData(Me.txtDataDal.Text) & "&AL=" & par.AggiustaData(Me.txtDataAl.Text) & "&COMPLESSO=" & par.PulisciStrSql(Me.CmbComplesso.SelectedValue.ToString) & "&EDIFICIO=" & par.PulisciStrSql(Me.cmbEdificio.SelectedValue.ToString) & "&UNITA=" & par.PulisciStrSql(Me.cmbCodUnita.SelectedValue.ToString) & "&TIPO=" & Request.QueryString("TIPO") & "');</script>")
            'Me.cmbEdificio.Items.Clear()
            'Me.CaricaEdifici()
            'Me.CmbComplesso.SelectedValue = "-1"
            'Me.cmbCodUnita.Items.Clear()
            'Me.txtDataAl.Text = ""
            'Me.txtDataDal.Text = ""
        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CaricaEdifici()
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then

            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbEdificio.Items.Clear()

            cmbEdificio.Items.Add(New ListItem(" ", -1))

            If gest <> 0 Then
                par.cmd.CommandText = "SELECT distinct id,denominazione FROM siscom_mi.edifici where substr(id,1,1)= " & gest & " order by denominazione asc"
            Else
                'par.cmd.CommandText = "SELECT distinct EDIFICI.id,(EDIFICI.denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico) as denominazione FROM siscom_mi.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.indirizzi WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and edifici.id_indirizzo_principale = indirizzi.id and edifici.id in (select id_edificio from siscom_mi.bol_bollette) order by denominazione asc"
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,(EDIFICI.denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico) as denominazione FROM siscom_mi.edifici, SISCOM_MI.indirizzi WHERE edifici.id_indirizzo_principale = indirizzi.id order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    'Private Sub CaricaUnita()
    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Exit Sub
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        Me.cmbCodUnita.Items.Clear()
    '        cmbCodUnita.Items.Add(New ListItem(" ", -1))

    '        par.cmd.CommandText = "SELECT unita_immobiliari.cod_unita_immobiliare, unita_immobiliari.id from siscom_mi.unita_immobiliari, siscom_mi.bol_bollette where bol_bollette.id_unita=unita_immobiliari.id  order by cod_unita_immobiliare asc"

    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        While myReader1.Read
    '            cmbCodUnita.Items.Add(New ListItem(par.IfNull(myReader1("cod_unita_immobiliare"), " "), par.IfNull(myReader1("id"), -1)))
    '        End While


    '        myReader1.Close()

    '        par.OracleConn.Close()
    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message
    '    End Try

    'End Sub

    Protected Sub CmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbComplesso.SelectedIndexChanged
        FilterEdifComplessi()

    End Sub
    Private Sub FilterEdifComplessi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If Me.CmbComplesso.SelectedValue <> "-1" Then
                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,(EDIFICI.denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico) as denominazione FROM siscom_mi.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.indirizzi WHERE EDIFICI.ID_COMPLESSO = " & Me.CmbComplesso.SelectedValue.ToString & " and edifici.id_indirizzo_principale = indirizzi.id order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
            Else
                Me.CaricaEdifici()
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub FilterUniEdifici()
        Try
            If Me.cmbEdificio.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Me.cmbCodUnita.Items.Clear()
                cmbCodUnita.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT DISTINCT unita_immobiliari.cod_unita_immobiliare, unita_immobiliari.id,UNITA_IMMOBILIARI.INTERNO from siscom_mi.unita_immobiliari where unita_immobiliari.id_edificio = " & Me.cmbEdificio.SelectedValue.ToString & "  and ID_UNITA_PRINCIPALE IS NULL order by cod_unita_immobiliare asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCodUnita.Items.Add(New ListItem(par.IfNull(myReader1("cod_unita_immobiliare"), " ") & " INT. " & par.IfNull(myReader1("INTERNO"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                Me.cmbCodUnita.Items.Clear()
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try


    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        FilterUniEdifici()
    End Sub
End Class
