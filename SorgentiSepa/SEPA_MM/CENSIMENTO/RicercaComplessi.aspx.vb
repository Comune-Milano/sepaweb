
Partial Class CENSIMENTO_Ricerca
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Dim complesso As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                Me.cmbIndirizzo.Items.Clear()
                Me.cmbCivico.Items.Clear()
                'Dim gest As Integer = 0

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
                    Exit Sub
                End If

                CaricaZone()
                CaricaComplessi()

                'CaricaTuttiIndirizzi()

            Else
                Exit Sub
            End If
            TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub CaricaZone()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader 
        par.cmd.CommandText = "SELECT * FROM ZONA_ALER order by zona asc"
        myReader1 = par.cmd.ExecuteReader
        Me.cmbZona.Items.Add(New ListItem(" ", -1))

        While myReader1.Read
            cmbZona.Items.Add(New ListItem(par.IfNull(myReader1("zona"), " "), par.IfNull(myReader1("cod"), -1)))
        End While
        myReader1.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
    Private Sub CaricaComplessi(Optional filtroZona As String = "")
        Me.cmbComplesso.Items.Clear()

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        'cmbComplesso.Items.Add(New ListItem(" ", -1))
        If Session("PED2_ESTERNA") = "1" Then
            'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.id<>1 " & filtroZona & " order by denominazione asc "
        Else
            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.id<>1  " & filtroZona & " order by denominazione asc "

        End If

        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader1.Read
            'cmbComplesso.Items.Add(New ListItem("cod." & par.IfNull(myReader1("cod_complesso"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

        End While

        myReader1.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
    'Private Function CaricaTuttiIndirizzi()

    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Exit Function
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi where id in (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI)order by descrizione asc"
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '    Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
    '    While myReader1.Read
    '        cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " ")))
    '    End While
    '    myReader1.Close()
    '    cmbIndirizzo.Text = "-1"

    'End Function

    'Private Function CaricaIndirizzi()
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
    '    Dim idIndrizzoComplesso As String
    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Exit Function
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    If cmbComplesso.Text <> "" Then
    '        'Prima era così
    '        'par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by descrizione asc"
    '        'Adesso è così

    '        par.cmd.CommandText = "SELECT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & Me.cmbComplesso.SelectedValue.ToString
    '        myReader1 = par.cmd.ExecuteReader
    '        While myReader1.Read
    '            idIndrizzoComplesso = myReader1(0)
    '        End While
    '        myReader1.Close()
    '        If idIndrizzoComplesso > 0 Then


    '            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE SISCOM_MI.INDIRIZZI.ID = " & idIndrizzoComplesso & " order by descrizione asc"
    '            myReader1 = par.cmd.ExecuteReader
    '            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
    '            While myReader1.Read
    '                cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
    '            End While

    '            myReader1.Close()
    '            par.OracleConn.Close()
    '        End If
    '        cmbCivico.Items.Clear()
    '        cmbIndirizzo.Text = "-1"

    '    End If

    'End Function

    'Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
    '    If cmbComplesso.Text <> "" Then
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Exit Sub
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        cmbCivico.Items.Clear()

    '        par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        Me.cmbCivico.Items.Add(New ListItem(" ", -1))
    '        While myReader1.Read
    '            cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("id"), "-1")))
    '        End While
    '        myReader1.Close()
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Me.cmbCivico.Text = "-1"

    '    End If

    'End Sub
    'Private Function CaricaCivico()
    '    If cmbIndirizzo.Text <> "" Then
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Exit Function
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        Me.cmbCivico.Items.Add(New ListItem(" ", -1))
    '        While myReader1.Read
    '            cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("id"), "-1")))
    '        End While
    '        myReader1.Close()
    '        par.OracleConn.Close()
    '    End If
    'End Function

    'Protected Sub cmbCivico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCivico.SelectedIndexChanged
    '    Exit Sub
    'End Sub

    'Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
    '    If Me.cmbComplesso.Text <> "-1" Then
    '        Me.cmbIndirizzo.Items.Clear()
    '        Me.cmbCivico.Items.Clear()
    '        Me.CaricaIndirizzi()
    '    Else
    '        Me.cmbIndirizzo.Items.Clear()
    '        Me.cmbCivico.Items.Clear()
    '        Me.CaricaTuttiIndirizzi()
    '    End If
    'End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            Dim bTrovato As Boolean = False
            If Me.cmbComplesso.Text <> "-1" Then
                complesso = Me.cmbComplesso.SelectedItem.Text
            End If
            'If Me.cmbIndirizzo.Text <> "-1" Then
            '    indirizzo = Me.cmbIndirizzo.SelectedItem.Text   
            '    If Me.cmbCivico.Text <> "-1" Then
            '        civico = Me.cmbCivico.SelectedItem.Text
            '    End If
            'End If
            Session.Add("CENSIMENTO", sStringaSql)
            Response.Redirect("RisultatiCOMPLESSI.aspx?C=" & par.PulisciStrSql(complesso) & "&I=" & par.VaroleDaPassare(par.PulisciStrSql(indirizzo)) & "&CIV=" & par.VaroleDaPassare(par.PulisciStrSql(civico)))

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca2.Click
        Session.Add("LE", 1)
        Response.Redirect("InserimentoComplessi.aspx?C=RicercaComplessi&ID=" & Me.cmbComplesso.SelectedValue.ToString)

    End Sub


    Protected Sub ImageButton1_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
                Me.ListEdifci.Items.Clear()
                If Session("PED2_ESTERNA") = "1" Then
                    'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 order by denominazione asc"

                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'order by denominazione asc"

                End If
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

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                Me.cmbComplesso.SelectedValue = Me.ListEdifci.SelectedValue.ToString
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

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        Me.TextBox1.Value = 1
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()
    End Sub

    Protected Sub cmbZona_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbZona.SelectedIndexChanged
        If Me.cmbZona.SelectedValue <> "-1" Then
            CaricaComplessi(" and exists(select * from siscom_mi.edifici where id_zona = " & Me.cmbZona.SelectedValue & " and edifici.id_complesso = complessi_immobiliari.id)")
        Else
            CaricaComplessi()
        End If
    End Sub
End Class



