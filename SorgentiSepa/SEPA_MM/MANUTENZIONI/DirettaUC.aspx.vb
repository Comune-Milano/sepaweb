
Partial Class MANUTENZIONI_DirettaUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Private Sub CaricaIndirizzi()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If Me.RadioButtonList1.SelectedValue = 0 Then
                cmbIndirizzo.Items.Add(" ")

                par.cmd.CommandText = "SELECT distinct ID, descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI) order by descrizione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                cmbIndirizzo.Text = " "

                cmbCivico.Items.Clear()
            Else
                cmbIndirizzo.Items.Add(" ")

                par.cmd.CommandText = "SELECT distinct ID,descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by descrizione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()

                cmbIndirizzo.Text = " "

                cmbCivico.Items.Clear()

            End If
            If cmbIndirizzo.Text <> " " Then


                par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
                End While
                myReader2.Close()
            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

        If Not IsPostBack Then
            Me.RadioButtonList1.SelectedValue = 0
            CaricaIndirizzi()
        End If

    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged

        CARICACIVICO()

    End Sub
    Private Sub CARICACIVICO()
        Try
            If cmbIndirizzo.Text <> "" Then

                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                cmbCivico.Items.Clear()
                par.cmd.CommandText = "SELECT id,civico FROM siscom_mi.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "' order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("id"), "-1")))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean = False
        Dim sValore As String
        Dim sCompara As String

        sStringaSql = ""

        If TxtUC.Text <> "" Then
            sValore = TxtUC.Text
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            sStringaSql = sStringaSql & " UNITA_COMUNI.COD_UNITA_COMUNE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If Me.RadioButtonList1.SelectedValue = 0 Then
            If cmbCivico.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbCivico.Text
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO" & sCompara & "" & par.PulisciStrSql(sValore) & " "
            End If
        Else
            If cmbCivico.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbCivico.Text
                If InStr(sValore, "*") Then
                    sCompara = " = "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " EDIFICI.ID_INDIRIZZO_PRINCIPALE" & sCompara & "" & par.PulisciStrSql(sValore) & " "
            End If

        End If



        'If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

        If bTrovato = True Then
            If Me.RadioButtonList1.SelectedValue = 0 Then
                sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl, TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA  from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where " & sStringaSql & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and ID_COMPLESSO = COMPLESSI_IMMOBILIARI.id order by COD_UNITA_COMUNE ASC "
            Else
                sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA  from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.edifici where " & sStringaSql & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and ID_EDIFICIO = edifici.id order by COD_UNITA_COMUNE ASC "
            End If
        Else

            If Me.RadioButtonList1.SelectedValue = 0 Then
                If Me.sStringaSql <> "" Then
                    sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA  from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where " & sStringaSql & " AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and ID_COMPLESSO = COMPLESSI_IMMOBILIARI.id order by COD_UNITA_COMUNE ASC "
                Else
                    sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA  from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where " & sStringaSql & "  ID_COMPLESSO = COMPLESSI_IMMOBILIARI.id AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD order by COD_UNITA_COMUNE ASC "
                End If
            Else
                If Me.sStringaSql <> "" Then

                    sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA   from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.edifici where " & sStringaSql & "AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD and ID_EDIFICIO = edifici.id order by COD_UNITA_COMUNE ASC "
                Else
                    sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl,TIPO_UNITA_COMUNE.DESCRIZIONE  AS TIPO_UNITA   from SISCOM_MI.TIPO_UNITA_COMUNE, SISCOM_MI.unita_comuni, SISCOM_MI.edifici where " & sStringaSql & "  ID_EDIFICIO = edifici.id AND UNITA_COMUNI.COD_TIPOLOGIA=TIPO_UNITA_COMUNE.COD order by COD_UNITA_COMUNE ASC "

                End If

            End If

        End If

        '******************
        'If bTrovato = True Then
        '    If Me.RadioButtonList1.SelectedValue = 0 Then
        '        sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl from SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where " & sStringaSql & " and ID_COMPLESSO = COMPLESSI_IMMOBILIARI.id order by COD_UNITA_COMUNE ASC "
        '    Else
        '        sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl from SISCOM_MI.unita_comuni, SISCOM_MI.edifici where " & sStringaSql & " and ID_EDIFICIO = edifici.id order by COD_UNITA_COMUNE ASC "
        '    End If
        'Else

        '    If Me.RadioButtonList1.SelectedValue = 0 Then
        '        If Me.sStringaSql <> "" Then
        '            sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl from SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where " & sStringaSql & " and ID_COMPLESSO = COMPLESSI_IMMOBILIARI.id order by COD_UNITA_COMUNE ASC "
        '        Else
        '            sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, COMPLESSI_IMMOBILIARI.DENOMINAZIONE as EdiCompl from SISCOM_MI.unita_comuni, SISCOM_MI.COMPLESSI_IMMOBILIARI where " & sStringaSql & " ID_COMPLESSO = COMPLESSI_IMMOBILIARI.id order by COD_UNITA_COMUNE ASC "
        '        End If
        '    Else
        '        If Me.sStringaSql <> "" Then

        '            sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl from SISCOM_MI.unita_comuni, SISCOM_MI.edifici where " & sStringaSql & " and ID_EDIFICIO = edifici.id order by COD_UNITA_COMUNE ASC "
        '        Else
        '            sStringaSql = "SELECT UNITA_COMUNI.id,UNITA_COMUNI.COD_UNITA_COMUNE, UNITA_COMUNI.LOCALIZZAZIONE, EDIFICI.DENOMINAZIONE as EdiCompl from SISCOM_MI.unita_comuni, SISCOM_MI.edifici where " & sStringaSql & "  ID_EDIFICIO = edifici.id order by COD_UNITA_COMUNE ASC "

        '        End If

        '    End If

        'End If
        '******************

        Session.Add("PED", sStringaSql)
        Response.Redirect("RisultatiUC.aspx?T=0")
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
            Me.ListEdifci.Items.Clear()
            If Me.RadioButtonList1.SelectedValue = 0 Then
                par.cmd.CommandText = "SELECT distinct ID,descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI) and descrizione like '%" & Me.TxtDescInd.Text & "%'order by descrizione asc"
            Else
                par.cmd.CommandText = "SELECT distinct ID,descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) and descrizione like '%" & Me.TxtDescInd.Text & "%'order by descrizione asc"
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
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
        Me.TextBox1.Text = 2

    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        If Me.ListEdifci.SelectedValue.ToString <> "" Then
            Me.cmbIndirizzo.SelectedValue = Me.ListEdifci.SelectedValue.ToString
            CARICACIVICO()
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.TextBox1.Text = 1
            Me.LblNoResult.Visible = False
        Else
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.LblNoResult.Visible = False
            Me.TextBox1.Text = 1
        End If

    End Sub

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonList1.SelectedIndexChanged
        CaricaIndirizzi()
    End Sub
End Class
