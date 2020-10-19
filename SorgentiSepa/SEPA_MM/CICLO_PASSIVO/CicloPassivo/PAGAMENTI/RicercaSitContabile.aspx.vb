'*** RICERCA SITUAZIONE CONTABILE per STRUTTURA

Imports Telerik.Web.UI

Partial Class RicercaSitContabile
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '#########################
        'CONTROLLO BP_GENERALE/STRUTTURA/OPERATORE
        'If Session.Item("BP_GENERALE") <> 1 Then
        'If Session.Item("ID_STRUTTURA") = "-1" Then
        '    Response.Write("<script>alert('Attenzione! Operatore non associato ad una struttura!');parent.main.location.href=""../../pagina_home.aspx""</script>")
        '    Exit Sub
        'End If
        'End If
        '#########################

        If Not IsPostBack Then
            cmbStruttura.Items.Clear()
            cmbEsercizio.Items.Clear()
            CaricaStrutture()
            caricaEserciziFinanziari()

        End If

    End Sub

    Private Sub CaricaStrutture()

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader

        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If Session.Item("BP_GENERALE") = 1 Then
                'CARICO TUTTE LE STRUTTURE
                par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
                'myReader = par.cmd.ExecuteReader
                'While myReader.Read
                '    cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
                'End While
                'myReader.Close()
                par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)
                'SELEZIONO LA STRUTTURA DI APPARTENENZA DELL'OPERATORE
                cmbStruttura.SelectedValue = Session.Item("ID_STRUTTURA")

            Else
                'CARICO LA STRUTTURA DI APPARTENENZA
                par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & Session.Item("ID_STRUTTURA") & "'"
                'myReader = par.cmd.ExecuteReader
                'If myReader.Read Then
                '    cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
                'End If
                'myReader.Close()
                par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)
                cmbStruttura.Enabled = False
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim script As String = "window.open('StampaPFdisp.aspx?STR=" & cmbStruttura.SelectedValue.ToString _
                                                          & "&VOCI=" & ChkStampa.Checked _
                                                          & "&EF_R=" & cmbEsercizio.SelectedValue.ToString _
                                                          & "&CHIAMANTE=STAMPA_GENERALE','SitContabile','');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)
    End Sub



    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If



            'SELEZIONO I PIANI FINANZIARI APPROVATI
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI " _
            '        & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
            '        & "AND PF_STATI.ID=PF_MAIN.ID_STATO " _
            '        & "AND ID_STATO>='5'  ORDER BY PF_MAIN.ID ASC"
            par.cmd.CommandText = "SELECT pf_main.ID, ID_STATO, INIZIO, FINE, DESCRIZIONE, " _
                  & "GETDATA(INIZIO) || ' - ' || GETDATA(FINE) || ' (' || DESCRIZIONE || ')' AS STATO " _
                  & "FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI " _
                  & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                  & "AND PF_STATI.ID=PF_MAIN.ID_STATO " _
                  & "AND ID_STATO>='5'  ORDER BY PF_MAIN.ID desc"
            Dim DATAOGGI As String = Format(Now, "yyyyMMdd")
            'par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", True)
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
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento degli esercizi finanziari.');location.replace('../../pagina_home_ncp.aspx');</script>")
                End If

            End While

            If cmbEsercizio.Items.Count = 2 Then
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

End Class
