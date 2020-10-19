
Partial Class MANUTENZIONI_DirettaEdifici
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Private Sub CaricaIndirizzi()

        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            cmbIndirizzo.Items.Add(" ")

            par.cmd.CommandText = "SELECT distinct ID, descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM siscom_mi.COMPLESSI_IMMOBILIARI) order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            cmbIndirizzo.Text = " "

            cmbCivico.Items.Clear()
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
            CaricaIndirizzi()
        End If

    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        CARICACIVICO()
        TextBox1.Text = ""
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
        Try
            Dim bTrovato As Boolean = False
            Dim sValore As String
            Dim sCompara As String

            sStringaSql = ""

            If TxtcodCompl.Text <> "" Then
                sValore = TxtcodCompl.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " COMPLESSI_IMMOBILIARI.COD_COMPLESSO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If



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



            'If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql
            '********MODIFICO LA QUERY SE A CHIAMARE LA RICERCA è LA PARTE DEGLI INTERVENTI DI MANUTENZIONE

            'If Session.Item("CHIAMA") = "INTMAN" Then
            '    If bTrovato = True Then

            '        sStringaSql = "select complessi_immobiliari.id, complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari,SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE " & sStringaSql & " and COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_COMPLESSO ORDER BY complessi_immobiliari.COD_complesso ASC"
            '    Else
            '        sStringaSql = "select complessi_immobiliari.id, complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari,SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_COMPLESSO ORDER BY complessi_immobiliari.COD_complesso ASC"

            '    End If
            'Else
            If bTrovato = True Then

                sStringaSql = "select complessi_immobiliari.id,COMPLESSI_IMMOBILIARI.COD_COMPLESSO_GIMI, complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari WHERE " & sStringaSql & " and COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID ORDER BY complessi_immobiliari.COD_complesso ASC"
            Else
                sStringaSql = "select complessi_immobiliari.id,COMPLESSI_IMMOBILIARI.COD_COMPLESSO_GIMI, complessi_immobiliari.cod_complesso,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID ORDER BY complessi_immobiliari.COD_complesso ASC"

            End If

            'End If
            '********FINE MODIFICA

            Session.Add("PED", sStringaSql)
            Response.Redirect("RisultatiSelettivaComplessi.aspx?T=0")
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

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

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
            Me.ListEdifci.Items.Clear()
            par.cmd.CommandText = "SELECT distinct ID,descrizione FROM siscom_mi.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM siscom_mi.COMPLESSI_IMMOBILIARI) and descrizione like '%" & Me.TxtDescInd.Text & "%'order by descrizione asc"
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
End Class
