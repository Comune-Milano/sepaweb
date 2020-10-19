Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_PagamentiPerStruttura
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        '#########################
        'CONTROLLO BP_GENERALE/STRUTTURA/OPERATORE
        If Session.Item("BP_GENERALE") <> 1 Then
            If Session.Item("ID_STRUTTURA") = "-1" Then
                Response.Write("<script>alert('Attenzione! Operatore non abilitato per questa funzione!');parent.main.location.href=""../../pagina_home_ncp.aspx""</script>")
                Exit Sub
            End If
        End If
        '#########################

        If Not IsPostBack Then
            ddlEsercizio.Items.Clear()
            ddlStrutture.Items.Clear()
            caricaEserciziFinanziari()
            CaricaStrutture()
        End If
    End Sub
  Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim condPU As String = ""
        If ChkPagamentiUtenze.Checked = True Then
            condPU = "PU=1&"
        Else
            condPU = "PU=0&"
        End If
        Dim script As String = "window.open('RisultatiPagamentiPerStruttura.aspx?" & condPU & "IDS=" & ddlStrutture.SelectedValue & "&EF=" & ddlEsercizio.SelectedValue & "','Report');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", script, True)

    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub

   
    Protected Sub caricaEserciziFinanziari()
        Try
            'APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'SELEZIONO I PIANI FINANZIARI APPROVATI
            par.cmd.CommandText = "Select pf_main.ID, ID_STATO, INIZIO, FINE, DESCRIZIONE, " _
                     & "GETDATA(INIZIO) || ' - ' || GETDATA(FINE) || ' (' || DESCRIZIONE || ')' AS STATO " _
                     & "FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI " _
                     & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO=T_ESERCIZIO_FINANZIARIO.ID " _
                     & "AND PF_STATI.ID=PF_MAIN.ID_STATO " _
                     & "AND ID_STATO>='5'  ORDER BY PF_MAIN.ID desc"
            ' par.caricaComboTelerik(par.cmd.CommandText, ddlEsercizio, "ID", "STATO", True)
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
                    ddlEsercizio.Items.Add(New RadComboBoxItem(ANNOINIZIO & " - " & ANNOFINE & "  (" & par.IfNull(myReader1("DESCRIZIONE"), "") & ")", par.IfNull(myReader1("ID"), 0)))
                    If par.IfNull(myReader1("ID_STATO"), 0) >= 5 And VERIFICADATA = True Then
                        ddlEsercizio.SelectedValue = par.IfNull(myReader1("ID"), 0)
                    End If
                Else
                    'ERRORE USCIRE DALLA PAGINA
                    Response.Write("<script>alert('Si è verificato un errore durante il caricamento degli esercizi finanziari.');location.replace('../../pagina_home_ncp.aspx');</script>")
                End If

            End While

            If ddlEsercizio.Items.Count = 1 Then
                ddlEsercizio.Enabled = False
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
                ddlStrutture.Items.Clear()
                'ddlStrutture.Items.Add(New ListItem("Tutte", "-1"))
                par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
                'myReader = par.cmd.ExecuteReader
                'While myReader.Read
                '    ddlStrutture.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
                'End While
                'myReader.Close()
                par.caricaComboTelerik(par.cmd.CommandText, ddlStrutture, "ID", "NOME", False)
                Dim item As RadComboBoxItem = New RadComboBoxItem
                item.Text = "Tutte"
                item.Value = "-1"
                ddlStrutture.Items.Add(item)
                'SELEZIONO LA STRUTTURA DI APPARTENENZA DELL'OPERATORE
                ddlStrutture.SelectedValue = Session.Item("ID_STRUTTURA")

            Else
                'CARICO LA STRUTTURA DI APPARTENENZA
                ddlStrutture.Items.Clear()
                par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & Session.Item("ID_STRUTTURA") & "'"
                'myReader = par.cmd.ExecuteReader
                'If myReader.Read Then
                '    ddlStrutture.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
                'End If
                'myReader.Close()
                par.caricaComboTelerik(par.cmd.CommandText, ddlStrutture, "ID", "NOME", False)
                ddlStrutture.Enabled = False
            End If

            'ddlStrutture.Items.Add(New ListItem("Tutte", "-1"))
            ''CARICO TUTTE LE STRUTTURE
            'par.cmd.CommandText = "SELECT DISTINCT ID, NOME FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC"
            'myReader = par.cmd.ExecuteReader
            'While myReader.Read
            '    ddlStrutture.Items.Add(New ListItem(par.IfNull(myReader("NOME"), ""), par.IfNull(myReader("ID"), "")))
            'End While
            'myReader.Close()

            ''SELEZIONO LA STRUTTURA DI APPARTENENZA DELL'OPERATORE
            ''ddlStrutture.SelectedValue = Session.Item("ID_STRUTTURA")

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

    Private Function ConvertiData(ByVal dataIn As String) As String
        Dim dataOut As String = ""
        If Len(dataIn) = 8 Then
            Return Right(dataIn, 2) & "/" & Mid(dataIn, 5, 2) & "/" & Left(dataIn, 4)
        Else
            Return ""
        End If
    End Function

  
End Class
