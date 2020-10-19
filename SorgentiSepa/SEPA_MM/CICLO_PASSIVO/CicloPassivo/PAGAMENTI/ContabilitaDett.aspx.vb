
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ContabilitaDett
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        '#########################
        'CONTROLLO BP_GENERALE
        If Session.Item("BP_GENERALE") <> 1 Then
            RadWindowManager1.RadAlert("Attenzione! Operatore non abilitato per questa funzione!", 300, 150, "Attenzione", "", "null")
            Response.Write("<script>parent.main.location.href=""../../pagina_home_ncp.aspx""</script>")
        End If
        '#########################

        If Not IsPostBack Then
            cmbEsercizio.Items.Clear()
            caricaEserciziFinanziari()
            ImpostaDataInizio()
        End If

    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        '########### CONTROLLO DATE #############
        Dim ControlloCampi As Boolean
        Try
            Dim datadal As String = ""
            If Not IsNothing(txtdataDAL.SelectedDate) Then
                datadal = txtdataDAL.SelectedDate
            End If
            Dim dataal As String = ""
            If Not IsNothing(txtDataAL.SelectedDate) Then
                dataal = txtDataAL.SelectedDate
            End If
            ControlloCampi = True
            If IsNothing(txtDataAL.SelectedDate) Or IsNothing(txtdataDAL.SelectedDate) Then
                RadWindowManager1.RadAlert("Entrambe le date sono obbligatorie!", 300, 150, "Attenzione", "", "null")

                ControlloCampi = False
            Else
                If par.AggiustaData(txtDataAL.SelectedDate) < par.AggiustaData(txtdataDAL.SelectedDate) Then
                    RadWindowManager1.RadAlert("La data fine deve essere maggiore o uguale alla data di inizio!", 300, 150, "Attenzione", "", "null")

                    ControlloCampi = False
                End If
                If par.AggiustaData(txtDataAL.SelectedDate) > par.AggiustaData(txtDATAfinePredefinita.Value) Then
                    RadWindowManager1.RadAlert("La data fine deve essere minore o uguale alla data di fine esercizio!", 300, 150, "Attenzione", "", "null")

                    ControlloCampi = False
                End If
                If par.AggiustaData(txtdataDAL.SelectedDate) < par.AggiustaData(txtDATAinizioPredefinita.Value) Then
                    RadWindowManager1.RadAlert("La data inizio deve essere maggiore o uguale alla data di inizio esercizio!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                End If
            End If

            If ControlloCampi = True Then
                Dim script As String = "window.open('RisultatiContabilitaDett.aspx?&VOCI=" & ChkStampa.Checked & "&AL=" & par.IfEmpty(par.AggiustaData(dataal), "") & "&DAL=" & par.IfEmpty(par.AggiustaData(datadal), "") & "&EF_R=" & cmbEsercizio.SelectedValue.ToString & "','Contabilita','');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
        '#########################################
    End Sub

    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'SELEZIONO I PIANI FINANZIARI APPROVATI
            par.cmd.CommandText = "SELECT pf_main.ID, ID_STATO, INIZIO, FINE, DESCRIZIONE, " _
                    & "GETDATA(INIZIO) || ' - ' || GETDATA(FINE) || ' (' || DESCRIZIONE || ')' AS STATO " _
                    & "FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI " _
                    & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                    & "AND PF_STATI.ID=PF_MAIN.ID_STATO " _
                    & "AND ID_STATO>='5'  ORDER BY PF_MAIN.ID DESC"
            'par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", True)
            Dim DATAOGGI As String = Format(Now, "yyyyMMdd")
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                Dim ANNOINIZIO As String = par.IfNull(myReader1("INIZIO"), "")
                Dim ANNOFINE As String = par.IfNull(myReader1("FINE"), "")
                Dim VERIFICADATA = False
                If ANNOINIZIO <= DATAOGGI And ANNOFINE >= DATAOGGI Then
                    VERIFICADATA = True
                End If
                If Len(ANNOINIZIO) = 8 And Len(ANNOFINE) = 8 Then
                    ANNOINIZIO = ConvertiData(ANNOINIZIO)
                    ANNOFINE = ConvertiData(ANNOFINE)
                    cmbEsercizio.Items.Add(New RadComboBoxItem(ANNOINIZIO & " - " & ANNOFINE & "  (" & par.IfNull(myReader1("DESCRIZIONE"), "") & ")", par.IfNull(myReader1("ID"), 0)))
                    If par.IfNull(myReader1("ID_STATO"), 0) >= 5 And VERIFICADATA = True Then
                        cmbEsercizio.SelectedValue = par.IfNull(myReader1("ID"), 0)
                    End If
                Else
                    'ERRORE USCIRE DALLA PAGINA
                    RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli esercizi finanziari.", 300, 150, "Attenzione", "", "null")
                    Response.Write("<script>location.replace('../../pagina_home_ncp.aspx');</script>")
                End If

            End While

            If cmbEsercizio.Items.Count = 1 Then
                cmbEsercizio.Enabled = False
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        ImpostaDataInizio()
    End Sub

    Protected Sub ImpostaDataInizio()
        Try
            '######## IMPOSTO LA DATA DI INIZIO ##########
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.* " _
                & "FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PF_MAIN " _
                & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID " _
                & "AND PF_MAIN.ID='" & cmbEsercizio.SelectedValue & "'"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                txtdataDAL.SelectedDate = par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
                txtDataAL.SelectedDate = par.FormattaData(par.IfNull(myReader1("FINE"), ""))
                txtDATAfinePredefinita.Value = par.FormattaData(par.IfNull(myReader1("FINE"), ""))
                txtDATAinizioPredefinita.Value = par.FormattaData(par.IfNull(myReader1("INIZIO"), ""))
            End If
            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '#############################################
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try


    End Sub



End Class