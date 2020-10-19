
Partial Class GestioneAutonoma_RicercaEdificio
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            If Not IsPostBack Then

                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                '***CARICAMENTO LISTA COMPLESSI
                DrLComplesso.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader2.Read
                    ' DrLComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader2("cod_complesso"), " ") & "- -" & par.IfNull(myReader2("denominazione"), " "), par.IfNull(myReader2("id"), -1)))
                    DrLComplesso.Items.Add(New ListItem(par.IfNull(myReader2("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader2("cod_complesso"), " "), par.IfNull(myReader2("id"), -1)))
                End While
                myReader2.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaEdifici()
                'CaricaIndirizzi()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub
    Private Sub CaricaEdifici()
        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbEdificio.Items.Clear()

            cmbEdificio.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID order by denominazione asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

                'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DrLComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLComplesso.SelectedIndexChanged
        If Me.DrLComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            Me.CaricaEdificiComp()
            'Me.filtraindirizzi()
        Else
            Me.cmbEdificio.Items.Clear()
            CaricaEdifici()
            'Me.CaricaIndirizzi()
        End If
        'Me.TextBox1.Value = 1
        'Me.ListEdifci.Items.Clear()
        'Me.TxtDescInd.Text = ""
    End Sub
    Private Sub CaricaEdificiComp()
        Try

            If Me.DrLComplesso.SelectedValue <> "-1" Then


                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                Dim gest As Integer = 0
                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))


                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.DrLComplesso.SelectedValue.ToString & " order by denominazione asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
                    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While


                myReader1.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("RisultatiEdificio.aspx?E=" & Me.cmbEdificio.SelectedValue.ToString & "&C=" & Me.DrLComplesso.SelectedValue.ToString)

    End Sub
End Class
