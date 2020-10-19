
Partial Class MOROSITA_RicercaDebitoriMultiSelezioneMor
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Request.ServerVariables("HTTP_REFERER") = "MesseInMora.aspx" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "location.href='RicercaDebitoriMultiSelezioneMor.aspx';", True)
        End If
        If Not IsPostBack Then
            impostaJavascript()
            caricaFiliali()
            caricaQuartieri()
            caricaTipologiaRapporti()
            caricaTipologiaUI()
            caricaStatoContratto()
            caricaProtocolli()
        End If
    End Sub
    Protected Sub ApriConnessione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
            par.cmd.Dispose()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Private Sub caricaFiliali()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI " _
                & "WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI) ORDER BY NOME"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListFiliali.DataSource = dataTable
            CheckBoxListFiliali.DataTextField = "NOME"
            CheckBoxListFiliali.DataValueField = "ID"
            CheckBoxListFiliali.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaQuartieri()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,NOME FROM SISCOM_MI.TAB_QUARTIERI " _
                & "WHERE ID IN (SELECT DISTINCT ID_QUARTIERE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI) ORDER BY NOME"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListQuartieri.DataSource = dataTable
            CheckBoxListQuartieri.DataTextField = "NOME"
            CheckBoxListQuartieri.DataValueField = "ID"
            CheckBoxListQuartieri.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaComplessi()
        Try
            Dim filialiSelezionate As String = ""
            For Each Items As ListItem In CheckBoxListFiliali.Items
                If Items.Selected = True Then
                    filialiSelezionate &= Items.Value & ","
                End If
            Next
            Dim condizioneFiliali As String = ""
            If filialiSelezionate <> "" Then
                filialiSelezionate = Left(filialiSelezionate, Len(filialiSelezionate) - 1)
                condizioneFiliali = " AND ID_FILIALE IN (" & filialiSelezionate & ") "
            End If
            Dim quartieriSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListQuartieri.Items
                If Items.Selected = True Then
                    quartieriSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneQuartieri As String = ""
            If quartieriSelezionati <> "" Then
                quartieriSelezionati = Left(quartieriSelezionati, Len(quartieriSelezionati) - 1)
                condizioneQuartieri = " AND ID_QUARTIERE IN (" & quartieriSelezionati & ") "
            End If
            CheckBoxListEdifici.Items.Clear()
            CheckBoxListIndirizzi.Items.Clear()
            If condizioneFiliali <> "" Or condizioneQuartieri <> "" Then
                ApriConnessione()
                par.cmd.CommandText = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    & "WHERE ID<>1 " & condizioneQuartieri & condizioneFiliali & " ORDER BY DENOMINAZIONE"
                Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable As New Data.DataTable
                dataAdapter.Fill(dataTable)
                CheckBoxListComplessi.DataSource = dataTable
                CheckBoxListComplessi.DataTextField = "DENOMINAZIONE"
                CheckBoxListComplessi.DataValueField = "ID"
                CheckBoxListComplessi.DataBind()
                chiudiConnessione()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaEdifici()
        Try
            CheckBoxListIndirizzi.Items.Clear()
            Dim complessiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListComplessi.Items
                If Items.Selected = True Then
                    complessiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneComplessi As String = ""
            If complessiSelezionati <> "" Then
                complessiSelezionati = Left(complessiSelezionati, Len(complessiSelezionati) - 1)
                condizioneComplessi = " AND ID_COMPLESSO IN (" & complessiSelezionati & ") "
            End If
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,DENOMINAZIONE FROM SISCOM_MI.EDIFICI " _
                & "WHERE ID<>1 " & condizioneComplessi & " ORDER BY DENOMINAZIONE"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListEdifici.DataSource = dataTable
            CheckBoxListEdifici.DataTextField = "DENOMINAZIONE"
            CheckBoxListEdifici.DataValueField = "ID"
            CheckBoxListEdifici.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaIndirizzi()
        Try
            Dim edificiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListEdifici.Items
                If Items.Selected = True Then
                    edificiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneEdifici As String = ""
            If edificiSelezionati <> "" Then
                edificiSelezionati = Left(edificiSelezionati, Len(edificiSelezionati) - 1)
                condizioneEdifici = " AND ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE " _
                    & "FROM SISCOM_MI.EDIFICI WHERE ID IN (" & edificiSelezionati & ")) "
            End If
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,DESCRIZIONE ||','||CIVICO AS DESCRIZIONE " _
                & "FROM SISCOM_MI.INDIRIZZI WHERE ID<>1 " & condizioneEdifici & " ORDER BY DESCRIZIONE"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListIndirizzi.DataSource = dataTable
            CheckBoxListIndirizzi.DataTextField = "DESCRIZIONE"
            CheckBoxListIndirizzi.DataValueField = "ID"
            CheckBoxListIndirizzi.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaTipologiaRapporti()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT COD,TRIM(DESCRIZIONE) AS DESCRIZIONE " _
                & "FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                & "ORDER BY DESCRIZIONE ASC"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListTipologiaRapporti.DataSource = dataTable
            CheckBoxListTipologiaRapporti.DataTextField = "DESCRIZIONE"
            CheckBoxListTipologiaRapporti.DataValueField = "COD"
            CheckBoxListTipologiaRapporti.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaContrattoSpecifico()
        Try
            CheckBoxListContrattoSpecifico.Items.Clear()
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Value = "ERP" Then
                    If Items.Selected = True Then
                        'CheckBoxListContrattoSpecifico.Items.Add(New ListItem("TUTTI", "0"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("Canone Convenzionato", "12"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("Art.22 C.10 RR 1/2004", "8"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("Forze dell'Ordine", "10"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("ERP Moderato", "2"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("ERP Sociale", "1"))
                    End If
                End If
                If Items.Value = "L43198" Then
                    If Items.Selected = True Then
                        'CheckBoxListContrattoSpecifico.Items.Add(New ListItem("TUTTI", "-1"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("StANDard", "0"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("Cooperative", "C"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("431 P.O.R.", "P"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("431/98 Art.15 R.R.1/2004", "D"))
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("431/98 Speciali", "S"))
                    End If
                End If
            Next
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaTipologiaUI()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT COD,DESCRIZIONE " _
                & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI " _
                & "ORDER BY DESCRIZIONE ASC"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListTipologiaUI.DataSource = dataTable
            CheckBoxListTipologiaUI.DataTextField = "DESCRIZIONE"
            CheckBoxListTipologiaUI.DataValueField = "COD"
            CheckBoxListTipologiaUI.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaStatoContratto()
        Try
            CheckBoxListStatoContratto.Items.Clear()
            CheckBoxListStatoContratto.Items.Add(New ListItem("IN CORSO", "1"))
            CheckBoxListStatoContratto.Items.Add(New ListItem("CHIUSO", "0"))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaAreaCanone()
        Try
            CheckBoxListAreaCanone.Items.Clear()
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Value = "ERP" Then
                    If Items.Selected = True Then
                        CheckBoxListAreaCanone.Items.Add(New ListItem("PROTEZIONE", "1"))
                        CheckBoxListAreaCanone.Items.Add(New ListItem("ACCESSO", "2"))
                        CheckBoxListAreaCanone.Items.Add(New ListItem("PERMANENZA", "3"))
                        CheckBoxListAreaCanone.Items.Add(New ListItem("DECADENZA", "4"))
                    End If
                End If
            Next
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Protected Sub CheckBoxFiliali_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxFiliali.CheckedChanged
        Try
            If CheckBoxFiliali.Checked = True Then
                For Each Items As ListItem In CheckBoxListFiliali.Items
                    Items.Selected = True
                Next
                caricaComplessi()
            Else
                For Each Items As ListItem In CheckBoxListFiliali.Items
                    Items.Selected = False
                Next
                CheckBoxListComplessi.Items.Clear()
                CheckBoxListEdifici.Items.Clear()
                CheckBoxListIndirizzi.Items.Clear()
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxQuartieri_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxQuartieri.CheckedChanged
        Try
            If CheckBoxQuartieri.Checked = True Then
                For Each Items As ListItem In CheckBoxListQuartieri.Items
                    Items.Selected = True
                Next
                caricaComplessi()
            Else
                For Each Items As ListItem In CheckBoxListQuartieri.Items
                    Items.Selected = False
                Next
                CheckBoxListComplessi.Items.Clear()
                CheckBoxListEdifici.Items.Clear()
                CheckBoxListIndirizzi.Items.Clear()
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxComplessi_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxComplessi.CheckedChanged
        Try
            If CheckBoxListComplessi.Items.Count > 0 Then
                If CheckBoxComplessi.Checked = True Then
                    For Each Items As ListItem In CheckBoxListComplessi.Items
                        Items.Selected = True
                    Next
                    caricaEdifici()
                Else

                    Dim controllo As Boolean = False
                    For Each Items As ListItem In CheckBoxListFiliali.Items
                        If Items.Selected = True Then
                            controllo = True
                            Exit For
                        End If
                    Next
                    If controllo = False Then
                        For Each Items As ListItem In CheckBoxListQuartieri.Items
                            If Items.Selected = True Then
                                controllo = True
                                Exit For
                            End If
                        Next
                    End If
                    CheckBoxListComplessi.Items.Clear()
                    If controllo = True Then
                        caricaComplessi()
                    End If
                    CheckBoxListEdifici.Items.Clear()
                    CheckBoxListIndirizzi.Items.Clear()
                End If
            Else
                CheckBoxComplessi.Checked = False
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxEdifici_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxEdifici.CheckedChanged
        Try

            If CheckBoxListEdifici.Items.Count > 0 Then
                If CheckBoxEdifici.Checked = True Then
                    For Each Items As ListItem In CheckBoxListEdifici.Items
                        Items.Selected = True
                    Next
                    caricaIndirizzi()
                Else
                    Dim controllo As Boolean = False
                    For Each Items As ListItem In CheckBoxListComplessi.Items
                        If Items.Selected = True Then
                            controllo = True
                            Exit For
                        End If
                    Next
                    CheckBoxListEdifici.Items.Clear()
                    If controllo = True Then
                        caricaEdifici()
                    End If
                    CheckBoxListIndirizzi.Items.Clear()
                End If
            Else
                CheckBoxEdifici.Checked = False
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxIndirizzi_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxIndirizzi.CheckedChanged
        Try
            If CheckBoxListIndirizzi.Items.Count > 0 Then
                If CheckBoxIndirizzi.Checked = True Then
                    For Each Items As ListItem In CheckBoxListIndirizzi.Items
                        Items.Selected = True
                    Next
                Else
                    Dim controllo As Boolean = False
                    For Each Items As ListItem In CheckBoxListEdifici.Items
                        If Items.Selected = True Then
                            controllo = True
                            Exit For
                        End If
                    Next
                    CheckBoxListIndirizzi.Items.Clear()
                    If controllo = True Then
                        caricaIndirizzi()
                    End If
                End If
            Else
                CheckBoxIndirizzi.Checked = False
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxTipologiaRapporto_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipologiaRapporto.CheckedChanged
        Try
            If CheckBoxTipologiaRapporto.Checked = True Then
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    Items.Selected = True
                Next
                caricaContrattoSpecifico()
                caricaAreaCanone()
            Else
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    Items.Selected = False
                Next
                CheckBoxListContrattoSpecifico.Items.Clear()
                CheckBoxListAreaCanone.Items.Clear()
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxContrattoSpecifico_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxContrattoSpecifico.CheckedChanged
        Try
            If CheckBoxContrattoSpecifico.Checked = True Then
                For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                    Items.Selected = False
                Next
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxTipologiaUI_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipologiaUI.CheckedChanged
        Try
            If CheckBoxTipologiaUI.Checked = True Then
                For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                    Items.Selected = False
                Next
            End If
            'caricaInquilini()
        Catch ex As Exception

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxStatoContratto_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxStatoContratto.CheckedChanged
        Try
            If CheckBoxStatoContratto.Checked = True Then
                For Each Items As ListItem In CheckBoxListStatoContratto.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListStatoContratto.Items
                    Items.Selected = False
                Next
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxAreaCanone_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxAreaCanone.CheckedChanged
        Try
            If CheckBoxAreaCanone.Checked = True Then
                For Each Items As ListItem In CheckBoxListAreaCanone.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListAreaCanone.Items
                    Items.Selected = False
                Next
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxListFiliali_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListFiliali.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListFiliali.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxFiliali.Checked = False
            End If
            caricaComplessi()
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxListQuartieri_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListQuartieri.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListQuartieri.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxQuartieri.Checked = False
            End If
            caricaComplessi()
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try


    End Sub
    Protected Sub CheckBoxListComplessi_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListComplessi.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListComplessi.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxComplessi.Checked = False
            Else
                CheckBoxComplessi.Checked = True
            End If
            Dim controlloAlmenoUnSelezionato As Boolean = False
            For Each Items As ListItem In CheckBoxListComplessi.Items
                If Items.Selected = True Then
                    controlloAlmenoUnSelezionato = True
                    Exit For
                End If
            Next
            If controlloAlmenoUnSelezionato = True Then
                caricaEdifici()
                scrollPositionPanel()
                'caricaInquilini()
            Else
                Dim controllo As Boolean = False
                For Each Items As ListItem In CheckBoxListFiliali.Items
                    If Items.Selected = True Then
                        controllo = True
                        Exit For
                    End If
                Next
                If controllo = False Then
                    For Each Items As ListItem In CheckBoxListQuartieri.Items
                        If Items.Selected = True Then
                            controllo = True
                            Exit For
                        End If
                    Next
                End If
                CheckBoxComplessi.Checked = False
                CheckBoxListComplessi.Items.Clear()
                If controllo = True Then
                    caricaComplessi()
                End If
                CheckBoxListEdifici.Items.Clear()
                scrollPositionPanel()
                'caricaInquilini()

            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListEdifici_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListEdifici.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListEdifici.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxEdifici.Checked = False
            Else
                CheckBoxEdifici.Checked = True
            End If
            Dim controlloAlmenoUnSelezionato As Boolean = False
            For Each Items As ListItem In CheckBoxListEdifici.Items
                If Items.Selected = True Then
                    controlloAlmenoUnSelezionato = True
                    Exit For
                End If
            Next
            If controlloAlmenoUnSelezionato = True Then
                caricaIndirizzi()
                scrollPositionPanel()
                'caricaInquilini()
            Else
                Dim controllo As Boolean = False
                For Each Items As ListItem In CheckBoxListComplessi.Items
                    If Items.Selected = True Then
                        controllo = True
                        Exit For
                    End If
                Next
                CheckBoxEdifici.Checked = False
                CheckBoxListEdifici.Items.Clear()
                CheckBoxListIndirizzi.Items.Clear()
                If controllo = True Then
                    caricaEdifici()
                End If
                scrollPositionPanel()
                'caricaInquilini()
            End If
            'Dim controlloSeTuttiSelezionati As Boolean = True
            'For Each Items As ListItem In CheckBoxListEdifici.Items
            '    If Items.Selected = False Then
            '        controlloSeTuttiSelezionati = False
            '        Exit For
            '    End If
            'Next
            'If controlloSeTuttiSelezionati = False Then
            '    CheckBoxEdifici.Checked = False
            'End If
            'caricaIndirizzi()
            'scrollPositionPanel()
            ''caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListIndirizzi_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListIndirizzi.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListIndirizzi.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxIndirizzi.Checked = False
            Else
                CheckBoxIndirizzi.Checked = True
            End If
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListTipologiaRapporti_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipologiaRapporti.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxTipologiaRapporto.Checked = False
            Else
                CheckBoxTipologiaRapporto.Checked = True
            End If
            caricaAreaCanone()
            caricaContrattoSpecifico()
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListContrattoSpecifico_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListContrattoSpecifico.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxContrattoSpecifico.Checked = False
            Else
                CheckBoxContrattoSpecifico.Checked = True
            End If
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListTipologiaUI_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipologiaUI.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxTipologiaUI.Checked = False
            Else
                CheckBoxTipologiaUI.Checked = True
            End If
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListStatoContratto_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListStatoContratto.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListStatoContratto.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxStatoContratto.Checked = False
            Else
                CheckBoxStatoContratto.Checked = True
            End If
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListAreaCanone_SELECTedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListAreaCanone.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In CheckBoxListAreaCanone.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                CheckBoxAreaCanone.Checked = False
            Else
                CheckBoxAreaCanone.Checked = True
            End If
            scrollPositionPanel()
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Private Sub scrollPositionPanel()
        Try
            Dim script As String = "document.getElementById('PanelFiliali').scrollTop = document.getElementById('yPosFiliali').value;"
            ScriptManager.RegisterClientScriptBlock(PanelFiliali, GetType(Panel), Page.ClientID, script, True)
            script = "document.getElementById('PanelQuartieri').scrollTop = document.getElementById('yPosQuartieri').value;"
            ScriptManager.RegisterClientScriptBlock(PanelQuartieri, GetType(Panel), Page.ClientID, script, True)
            script = "document.getElementById('PanelComplessi').scrollTop = document.getElementById('yPosComplessi').value;"
            ScriptManager.RegisterClientScriptBlock(PanelComplessi, GetType(Panel), Page.ClientID, script, True)
            script = "document.getElementById('PanelEdifici').scrollTop = document.getElementById('yPosEdifici').value;"
            ScriptManager.RegisterClientScriptBlock(PanelEdifici, GetType(Panel), Page.ClientID, script, True)
            script = "document.getElementById('PanelIndirizzi').scrollTop = document.getElementById('yPosIndirizzi').value;"
            ScriptManager.RegisterClientScriptBlock(PanelIndirizzi, GetType(Panel), Page.ClientID, script, True)
            script = "document.getElementById('PanelContrattoSpecifico').scrollTop = document.getElementById('yPosContrattoSpecifico').value;"
            ScriptManager.RegisterClientScriptBlock(PanelContrattoSpecifico, GetType(Panel), Page.ClientID, script, True)
            script = "document.getElementById('PanelTipologiaUI').scrollTop = document.getElementById('yPosTipologiaUI').value;"
            ScriptManager.RegisterClientScriptBlock(PanelTipologiaUI, GetType(Panel), Page.ClientID, script, True)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub caricaInquilini()
        Try
            ApriConnessione()
            Dim dataTable As New Data.DataTable
            '---------------SELEZIONE FILIALI------------------------
            Dim FilialiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListFiliali.Items
                If Items.Selected = True Then
                    FilialiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneFiliali As String = ""
            Dim condizioneFilialiContratti As String = ""
            If FilialiSelezionati <> "" Then
                FilialiSelezionati = Left(FilialiSelezionati, Len(FilialiSelezionati) - 1)
                condizioneFilialiContratti = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (" & FilialiSelezionati & ") "
                condizioneFiliali = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.EDIFICI " _
                    & " WHERE   ID_COMPLESSO IN (SELECT ID " _
                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE IN (" & FilialiSelezionati & ") " _
                    & " ))))) "
            End If
            '---------------SELEZIONE QUARTIERI------------------------
            Dim QuartieriSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListQuartieri.Items
                If Items.Selected = True Then
                    QuartieriSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneQuartieri As String = ""
            Dim condizioneQuartieriContratti As String = ""
            If QuartieriSelezionati <> "" Then
                QuartieriSelezionati = Left(QuartieriSelezionati, Len(QuartieriSelezionati) - 1)
                condizioneQuartieriContratti = " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & QuartieriSelezionati & ") "
                condizioneQuartieri = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.EDIFICI " _
                    & " WHERE   ID_COMPLESSO IN (SELECT ID " _
                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_QUARTIERE IN (" & QuartieriSelezionati & ") " _
                    & " ))))) "
            End If
            '---------------SELEZIONE COMPLESSI------------------------
            Dim complessiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListComplessi.Items
                If Items.Selected = True Then
                    complessiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneComplessi As String = ""
            Dim condizioneComplessiContratti As String = ""
            If complessiSelezionati <> "" Then
                complessiSelezionati = Left(complessiSelezionati, Len(complessiSelezionati) - 1)
                condizioneComplessiContratti = " AND COMPLESSI_IMMOBILIARI.ID IN (" & complessiSelezionati & ") "
                condizioneComplessi = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.EDIFICI " _
                    & " WHERE   ID_COMPLESSO IN (" & complessiSelezionati & ") " _
                    & " )))) "
            End If
            '---------------SELEZIONE EDIFICI------------------------
            Dim edificiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListEdifici.Items
                If Items.Selected = True Then
                    edificiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneEdifici As String = ""
            Dim condizioneEdificiContratti As String = ""
            If edificiSelezionati <> "" Then
                edificiSelezionati = Left(edificiSelezionati, Len(edificiSelezionati) - 1)
                condizioneEdificiContratti = " AND EDIFICI.ID IN (" & edificiSelezionati & ") "
                condizioneEdifici = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" & edificiSelezionati _
                    & " )))) "
            End If
            '---------------SELEZIONE INDIRIZZI------------------------
            Dim IndirizziSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListIndirizzi.Items
                If Items.Selected = True Then
                    IndirizziSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneIndirizzi As String = ""
            Dim condizioneIndirizziContratti As String = ""
            If IndirizziSelezionati <> "" Then
                IndirizziSelezionati = Left(IndirizziSelezionati, Len(IndirizziSelezionati) - 1)
                condizioneIndirizziContratti = " AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (" & IndirizziSelezionati & ") "
                condizioneIndirizzi = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN ( SELECT ID " _
                    & " FROM SISCOM_MI.EDIFICI WHERE ID_INDIRIZZO_PRINCIPALE IN (" & IndirizziSelezionati _
                    & " ))))) "
            End If
            '---------------SELEZIONE TIPOLOGIA RAPPORTO------------------------
            Dim TipologiaRapportoSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Selected = True Then
                    TipologiaRapportoSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneTipologiaRapporto As String = ""
            Dim condizioneTipologiaRapportoContratti As String = ""
            If TipologiaRapportoSelezionati <> "" Then
                TipologiaRapportoSelezionati = Left(TipologiaRapportoSelezionati, Len(TipologiaRapportoSelezionati) - 1)
                condizioneTipologiaRapportoContratti = " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC IN (" & TipologiaRapportoSelezionati & ") "
                condizioneTipologiaRapporto = " AND SISCOM_MI.MOROSITA.ID IN (" _
                        & " SELECT  ID_MOROSITA " _
                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                        & " WHERE   ID_CONTRATTO IN (" _
                        & " SELECT  ID " _
                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                        & " WHERE   COD_TIPOLOGIA_CONTR_LOC IN (" _
                        & TipologiaRapportoSelezionati _
                        & "))) "
            End If
            '---------------SELEZIONE CONTRATTO SPECIFICO------------------------
            Dim ContrattoSpecificoSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    ContrattoSpecificoSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneContrattoSpecifico As String = ""
            Dim ERP As Boolean = False
            Dim L43198 As Boolean = False
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Value = "ERP" Then
                    If Items.Selected = True Then
                        ERP = True
                    End If
                ElseIf Items.Value = "L43198" Then
                    If Items.Selected = True Then
                        L43198 = True
                    End If
                End If
            Next
            Dim PRIMO As Boolean = True
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    If ERP = True Then
                        Select Case Items.Value
                            Case "12"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=12)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=12)) "
                                End If
                            Case "8"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                         & " SELECT  ID_MOROSITA " _
                                         & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                         & " WHERE   ID_CONTRATTO IN (" _
                                         & " SELECT  ID " _
                                         & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                         & " WHERE   PROVENIENZA_ASS=8)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=8)) "
                                End If
                            Case "10"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=10)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=10)) "
                                End If
                            Case "2"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico = " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                       & " SELECT  ID_MOROSITA " _
                                       & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                       & " WHERE   ID_CONTRATTO IN (" _
                                       & " SELECT  ID_CONTRATTO " _
                                       & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                                       & " WHERE   ID_UNITA IN (" _
                                       & " SELECT  ID " _
                                       & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                                       & " WHERE   ID_DESTINAZIONE_USO=2))) "
                                Else
                                    condizioneContrattoSpecifico = " or SISCOM_MI.MOROSITA.ID IN (" _
                                       & " SELECT  ID_MOROSITA " _
                                       & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                       & " WHERE   ID_CONTRATTO IN (" _
                                       & " SELECT  ID_CONTRATTO " _
                                       & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                                       & " WHERE   ID_UNITA IN (" _
                                       & " SELECT  ID " _
                                       & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                                       & " WHERE   ID_DESTINAZIONE_USO=2))) "
                                End If
                            Case "1"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=1)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                         & " SELECT  ID_MOROSITA " _
                                         & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                         & " WHERE   ID_CONTRATTO IN (" _
                                         & " SELECT  ID " _
                                         & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                         & " WHERE   PROVENIENZA_ASS=1)) "
                                End If
                        End Select
                    End If
                    If L43198 = True Then
                        Select Case Items.Value
                            Case "0"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='0')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='0')) "
                                End If
                            Case "C"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='C')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='C')) "
                                End If
                            Case "P"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='P')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='P')) "
                                End If
                            Case "D"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='D')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='D')) "
                                End If
                            Case "S"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='S')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='S')) "
                                End If
                        End Select
                    End If
                End If
            Next
            If condizioneContrattoSpecifico <> "" Then
                condizioneContrattoSpecifico &= ") "
            End If


            '---------------SELEZIONE CONTRATTO SPECIFICO------------------------
            Dim contrattoSpecificoCSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    contrattoSpecificoCSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizionecontrattoSpecificoC As String = ""
            ERP = False
            L43198 = False
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Value = "ERP" Then
                    If Items.Selected = True Then
                        ERP = True
                    End If
                ElseIf Items.Value = "L43198" Then
                    If Items.Selected = True Then
                        L43198 = True
                    End If
                End If
            Next
            PRIMO = True
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    If ERP = True Then
                        Select Case Items.Value
                            Case "12"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=12 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=12 "
                                End If
                            Case "8"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=8 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=8 "
                                End If
                            Case "10"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=10 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=10 "
                                End If
                            Case "2"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO=2 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO=2 "
                                End If
                            Case "1"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=1 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=1 "
                                End If

                        End Select
                    End If
                    If L43198 = True Then
                        Select Case Items.Value
                            Case "0"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='0' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='0' "
                                End If
                            Case "C"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='C' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='C' "
                                End If
                            Case "P"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='P' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='P' "
                                End If
                            Case "D"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='D' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='D' "
                                End If
                            Case "S"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='S' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='S' "
                                End If
                        End Select
                    End If

                End If
            Next
            If condizionecontrattoSpecificoC <> "" Then
                condizionecontrattoSpecificoC &= ") "
            End If
            '---------------SELEZIONE TIPOLOGIAUI------------------------
            Dim TipologiaUISelezionati As String = ""
            For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                If Items.Selected = True Then
                    TipologiaUISelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneTipologiaUI As String = ""
            Dim condizioneTipologiaUIcontratti As String = ""
            If TipologiaUISelezionati <> "" Then
                TipologiaUISelezionati = Left(TipologiaUISelezionati, Len(TipologiaUISelezionati) - 1)
                condizioneTipologiaUIcontratti = " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD IN (" & TipologiaUISelezionati & ") "
                condizioneTipologiaUI = " AND SISCOM_MI.MOROSITA.ID in (" _
                    & " select  ID_MOROSITA " _
                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                    & " where   ID_CONTRATTO in (" _
                    & " select  ID_CONTRATTO " _
                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " where   TIPOLOGIA in (" & TipologiaUISelezionati & ")" _
                    & " )) "
            End If
            '---------------SELEZIONE STATO------------------------
            Dim statoINCORSO As Boolean = False
            Dim statoCHIUSO As Boolean = False
            Dim indice As Integer = 0
            For Each Items As ListItem In CheckBoxListStatoContratto.Items
                If Items.Selected = True Then
                    If indice = 0 Then
                        statoINCORSO = True
                    Else
                        statoCHIUSO = True
                    End If
                End If
                indice += 1
            Next
            Dim condizioneStatoContratto As String = ""
            Dim condizioneStatoContrattoC As String = ""
            If statoINCORSO = True And statoCHIUSO = True Then
            ElseIf statoINCORSO = True And statoCHIUSO = False Then
                condizioneStatoContrattoC = " AND RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL "
                condizioneStatoContratto &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_RICONSEGNA IS NULL)) "
            ElseIf statoINCORSO = False And statoCHIUSO = True Then
                condizioneStatoContrattoC = " AND RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL "
                condizioneStatoContratto &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_RICONSEGNA IS NOT NULL)) "
            ElseIf statoINCORSO = False And statoCHIUSO = False Then
            End If
            '---------------SELEZIONE AREA CANONE------------------------
            Dim AreaCanoneSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListAreaCanone.Items
                If Items.Selected = True Then
                    AreaCanoneSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneAreaCanone As String = ""
            Dim condizioneAreaCanoneContratti As String = ""
            If AreaCanoneSelezionati <> "" Then
                AreaCanoneSelezionati = Left(AreaCanoneSelezionati, Len(AreaCanoneSelezionati) - 1)
                condizioneAreaCanoneContratti = " AND RAPPORTI_UTENZA.ID IN (SELECT DISTINCT ID_CONTRATTO FROM SISCOM_MI.CANONI_EC WHERE ID_AREA_ECONOMICA IN (" & AreaCanoneSelezionati & ")) "
                condizioneAreaCanone &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (SELECT DISTINCT " _
                                        & " ID_CONTRATTO FROM SISCOM_MI.CANONI_EC WHERE " _
                                        & " ID_AREA_ECONOMICA IN (" & AreaCanoneSelezionati & "))) "
            End If
            '---------------CONDIZIONE STIPULA DEL CONTRATTO-------------------------------------------
            Dim condizioneStipula As String = ""
            Dim condizioneStipulaContratti As String = ""
            If par.AggiustaData(stipulaDal.Text) <> "" Then
                condizioneStipulaContratti &= " AND RAPPORTI_UTENZA.DATA_STIPULA>='" & par.AggiustaData(stipulaDal.Text) & "' "
                condizioneStipula &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_STIPULA>='" & par.AggiustaData(stipulaDal.Text) & "')) "
            End If
            If par.AggiustaData(stipulaAl.Text) <> "" Then
                condizioneStipulaContratti &= " AND RAPPORTI_UTENZA.DATA_STIPULA<='" & par.AggiustaData(stipulaAl.Text) & "' "
                condizioneStipula &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_STIPULA<='" & par.AggiustaData(stipulaAl.Text) & "')) "
            End If
            '---------------CONDIZIONE PROTOCOLLO -------------------------------------------
            Dim ProtocolloSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListProtocollo.Items
                If Items.Selected = True Then
                    ProtocolloSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneProtocollo As String = ""
            If ProtocolloSelezionati <> "" Then
                ProtocolloSelezionati = Left(ProtocolloSelezionati, Len(ProtocolloSelezionati) - 1)
                condizioneProtocollo = " AND SISCOM_MI.MOROSITA.PROTOCOLLO_ALER in (" & ProtocolloSelezionati & ") "
            End If
            '---------------CONDIZIONE DATA PROTOCOLLO -------------------------------------------
            Dim condizioneDataProtocollo As String = ""
            If par.AggiustaData(protocolloDal.Text) <> "" Then
                condizioneDataProtocollo &= " AND SISCOM_MI.MOROSITA.DATA_PROTOCOLLO>='" & par.AggiustaData(protocolloDal.Text) & "' "
            End If
            If par.AggiustaData(protocolloAl.Text) <> "" Then
                condizioneDataProtocollo &= " AND SISCOM_MI.MOROSITA.DATA_PROTOCOLLO<='" & par.AggiustaData(protocolloAl.Text) & "' "
            End If
            '--------------- condizione tipolettera ----------------------------------------------------------
            Dim moraSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListMora.Items
                If Items.Selected = True Then
                    moraSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizionemora As String = ""
            If moraSelezionati <> "" Then
                moraSelezionati = Left(moraSelezionati, Len(moraSelezionati) - 1)
                condizionemora = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & "select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE where TIPO_LETTERA in (" & moraSelezionati & "))"
            End If

            If condizioneAreaCanone <> "" Or _
                condizioneComplessi <> "" Or _
                    condizioneContrattoSpecifico <> "" Or _
                    condizioneEdifici <> "" Or _
                    condizioneFiliali <> "" Or _
                    condizioneIndirizzi <> "" Or
                    condizioneQuartieri <> "" Or _
                    condizioneStatoContratto <> "" Or _
                    condizioneTipologiaRapporto <> "" Or _
                    condizioneTipologiaUI <> "" Or _
                    condizioneProtocollo <> "" Or _
                    condizioneDataProtocollo <> "" Or _
                    condizioneStipula <> "" Then

                'par.cmd.CommandText = "SELECT MOROSITA.PROGR,MOROSITA.ID AS ID_MOROSITA,MOROSITA.PROTOCOLLO_ALER," _
                '  & " TO_CHAR(TO_DATE(SUBSTR(MOROSITA.DATA_PROTOCOLLO,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_PROTOCOLLO," _
                '  & " MOROSITA.TIPO_INVIO," _
                '  & " TO_CHAR(TO_DATE(SUBSTR(MOROSITA.RIF_DA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_DAL," _
                '  & " TO_CHAR(TO_DATE(SUBSTR(MOROSITA.RIF_A,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_AL," _
                '  & " NOTE,MOROSITA.DATA_PROTOCOLLO AS DATA_PROTOCOLLO_ORD " _
                '  & " FROM  " _
                '  & " SISCOM_MI.MOROSITA WHERE 1=1 " _
                '  & condizioneComplessi _
                '  & condizioneQuartieri _
                '  & condizioneEdifici _
                '  & condizioneFiliali _
                '  & condizioneAreaCanone _
                '  & condizioneContrattoSpecifico _
                '  & condizioneTipologiaUI _
                '  & condizioneStatoContratto _
                '  & condizioneStipula _
                '  & condizioneIndirizzi _
                '  & condizioneTipologiaRapporto _
                '  & condizioneProtocollo _
                '  & condizioneDataProtocollo

                'par.cmd.CommandText = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                '    & " NVL(SISCOM_MI.SALDI.SALDO,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=650,width=900'');£>'||trim(RAPPORTI_UTENZA.COD_CONTRATTO)||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''top=0,left=0'');£>'||" _
                '    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                '    & " then  trim(RAGIONE_SOCIALE) " _
                '    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                '    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                '    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                '    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                '    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                '    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                '    & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                '    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                '    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                '    & " then trim(RAGIONE_SOCIALE) " _
                '    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                '    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                '    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                '    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                '    & " SISCOM_MI.ANAGRAFICA, " _
                '    & " SISCOM_MI.INDIRIZZI, " _
                '    & " SISCOM_MI.EDIFICI, " _
                '    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                '    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                '    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                '    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                '    & " SISCOM_MI.SALDI " _
                '    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                '    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                '    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                '    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                '    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                '    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                '    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                '    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                '    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                '    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                '    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                '    & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '    & condizioneEdificiContratti _
                '    & condizioneQuartieriContratti _
                '    & condizioneComplessiContratti _
                '    & condizioneFilialiContratti _
                '    & condizioneIndirizziContratti _
                '    & condizioneTipologiaRapportoContratti _
                '    & condizionecontrattoSpecificoC _
                '    & condizioneTipologiaUIcontratti _
                '    & condizioneStatoContrattoC _
                '    & condizioneAreaCanoneContratti _
                '    & condizioneStipulaContratti _
                '    & "ORDER BY " & DropDownListOrdinamento.SelectedValue


                'par.cmd.CommandText = "SELECT id_contratto " _
                '    & " from siscom_mi.morosita_lettere where id_morosita in (" _
                '    & " select id from siscom_mi.morosita " _
                '    & " WHERE 1=1 " _
                '  & condizioneComplessi _
                '  & condizioneQuartieri _
                '  & condizioneEdifici _
                '  & condizioneFiliali _
                '  & condizioneAreaCanone _
                '  & condizioneContrattoSpecifico _
                '  & condizioneTipologiaUI _
                '  & condizioneStatoContratto _
                '  & condizioneStipula _
                '  & condizioneIndirizzi _
                '  & condizioneTipologiaRapporto _
                '  & condizioneProtocollo _
                '  & condizioneDataProtocollo _
                '  & " and id_contratto in (" _
                '  & " SELECT rapporti_utenza.id " _
                '  & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                '  & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                '  & " SISCOM_MI.RAPPORTI_UTENZA, " _
                '  & " SISCOM_MI.ANAGRAFICA, " _
                '  & " SISCOM_MI.INDIRIZZI, " _
                '  & " SISCOM_MI.EDIFICI, " _
                '  & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                '  & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                '  & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                '  & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                '  & " SISCOM_MI.SALDI " _
                '  & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                '  & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                '  & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                '  & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                '  & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                '  & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '  & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '  & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                '  & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                '  & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                '  & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                '  & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                '  & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                '  & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '  & condizioneEdificiContratti _
                '  & condizioneQuartieriContratti _
                '  & condizioneComplessiContratti _
                '  & condizioneFilialiContratti _
                '  & condizioneIndirizziContratti _
                '  & condizioneTipologiaRapportoContratti _
                '  & condizionecontrattoSpecificoC _
                '  & condizioneTipologiaUIcontratti _
                '  & condizioneStatoContrattoC _
                '  & condizioneAreaCanoneContratti _
                '  & condizioneStipulaContratti _
                '  & "))"




                par.cmd.CommandText = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                        & " NVL(SISCOM_MI.SALDI.SALDO,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||trim(RAPPORTI_UTENZA.COD_CONTRATTO)||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''top=0,left=0'');£>'||" _
                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & " then  trim(RAGIONE_SOCIALE) " _
                        & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                        & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                        & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                        & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                        & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                        & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                        & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                        & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                        & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & " then trim(RAGIONE_SOCIALE) " _
                        & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.RAPPORTI_UTENZA, " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.EDIFICI, " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.SALDI " _
                        & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                        & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                        & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                        & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                        & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                        & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                        & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                        & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " and rapporti_utenza.id in (" _
                        & "SELECT id_contratto " _
                        & " from siscom_mi.morosita_lettere where id_morosita in (" _
                        & " select id from siscom_mi.morosita " _
                        & " WHERE 1=1 " _
                        & condizioneComplessi _
                        & condizioneQuartieri _
                        & condizioneEdifici _
                        & condizioneFiliali _
                        & condizioneAreaCanone _
                        & condizioneContrattoSpecifico _
                        & condizioneTipologiaUI _
                        & condizioneStatoContratto _
                        & condizioneStipula _
                        & condizioneIndirizzi _
                        & condizioneTipologiaRapporto _
                        & condizioneProtocollo _
                        & condizioneDataProtocollo _
                        & condizionemora _
                        & " and id_contratto in (" _
                        & " SELECT rapporti_utenza.id " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.RAPPORTI_UTENZA, " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.EDIFICI, " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.SALDI " _
                        & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                        & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                      & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                      & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                      & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                      & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                      & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                      & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                      & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                      & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                      & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                      & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                      & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                      & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                      & condizioneEdificiContratti _
                      & condizioneQuartieriContratti _
                      & condizioneComplessiContratti _
                      & condizioneFilialiContratti _
                      & condizioneIndirizziContratti _
                      & condizioneTipologiaRapportoContratti _
                      & condizionecontrattoSpecificoC _
                      & condizioneTipologiaUIcontratti _
                      & condizioneStatoContrattoC _
                      & condizioneAreaCanoneContratti _
                      & condizioneStipulaContratti _
                      & "))" _
                      & ") " _
                      & "ORDER BY " & DropDownListOrdinamento.SelectedValue
                Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dataAdapter.Fill(dataTable)
                DataGridInquilini.DataSource = dataTable
                DataGridInquilini.DataBind()
                If dataTable.Rows.Count = 1 Then
                    Nintestatari.Text = "ESTRATTO " & dataTable.Rows.Count & " INTESTATARIO"
                Else
                    Nintestatari.Text = "ESTRATTI " & dataTable.Rows.Count & " INTESTATARI"
                End If
                PanelInquilini.Visible = True
                ButtonProcediDettaglioInquilini.Visible = True
                ButtonNuovaRicerca.Visible = False

            Else
                'DataGridInquilini.DataSource = dataTable
                'DataGridInquilini.DataBind()
                Nintestatari.Text = "Nessun inquilino estratto!"
                PanelInquilini.Visible = False

                ButtonProcediDettaglioInquilini.Visible = False
                ButtonNuovaRicerca.Visible = True
            End If
            'If IsNothing(Session.Item("listamorositaMM")) Then
            '    Dim lista As New System.Collections.Generic.List(Of String)
            '    For Each items As Data.DataRow In dataTable.Rows
            '        lista.Add(CStr(items.Item(1)))
            '    Next
            '    Session.Add("listamorositaMM", lista)
            'End If
            Dim lista As New System.Collections.Generic.List(Of String)
            For Each items As Data.DataRow In dataTable.Rows
                lista.Add(CStr(items.Item(1)))
            Next
            Session.Add("listamorositaMM", lista)
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Protected Sub DataGridInquilini_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridInquilini.PageIndexChanged
        Try
            Dim lista As System.Collections.Generic.List(Of String) = Session.Item("listamorositaMM")
            If Not IsNothing(lista) Then
                For Each Items As DataGridItem In DataGridInquilini.Items
                    If CType(Items.FindControl("Checkbox1"), CheckBox).Checked = False Then
                        lista.Remove(Items.Cells(0).Text)
                    Else
                        If Not lista.Contains(Items.Cells(0).Text) Then
                            lista.Add(Items.Cells(0).Text)
                        End If
                    End If
                Next
            End If
            Session.Item("listamorositaMM") = lista
            DataGridInquilini.CurrentPageIndex = e.NewPageIndex
            caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListMora_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListMora.SelectedIndexChanged
        'caricaInquilini()
    End Sub
    Protected Sub DropDownListOrdinamento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListOrdinamento.SelectedIndexChanged
        'caricaInquilini()
    End Sub
    Private Sub impostaJavascript()
        dataEstrazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        stipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        stipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        protocolloAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        protocolloDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

    End Sub
    Private Sub stipulaDal_TextChanged(sender As Object, e As System.EventArgs) Handles stipulaDal.TextChanged
        'caricaInquilini()
    End Sub
    Private Sub stipulaAl_TextChanged(sender As Object, e As System.EventArgs) Handles stipulaAl.TextChanged
        'caricaInquilini()
    End Sub
    Private Sub PreCaricaRiepilogo()
        Try
            'CONTROLLO TUTTO QUELLO CHE E' STATO SELEZIONATO
            Dim parametriDiRicerca As String = ""
            Dim controlloFiliali As String = ""
            If CheckBoxFiliali.Checked = True Then
                controlloFiliali = "Filiali: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListFiliali.Items
                    If Items.Selected = True Then
                        controlloFiliali &= Items.Text & ","
                    End If
                Next
                If controlloFiliali <> "" Then
                    controlloFiliali = "Filiali: " & Left(controlloFiliali, Len(controlloFiliali) - 1) & "<br />"
                End If
            End If
            Dim controlloQuartieri As String = ""
            If CheckBoxQuartieri.Checked = True Then
                controlloQuartieri = "Quartieri: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListQuartieri.Items
                    If Items.Selected = True Then
                        controlloQuartieri &= Items.Text & ","
                    End If
                Next
                If controlloQuartieri <> "" Then
                    controlloQuartieri = "Quartieri: " & Left(controlloQuartieri, Len(controlloQuartieri) - 1) & "<br />"
                End If
            End If
            Dim controlloComplessi As String = ""
            If CheckBoxComplessi.Checked = True Then
                controlloComplessi = "Complessi: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListComplessi.Items
                    If Items.Selected = True Then
                        controlloComplessi &= Items.Text & ","
                    End If
                Next
                If controlloComplessi <> "" Then
                    controlloComplessi = "Complessi: " & Left(controlloComplessi, Len(controlloComplessi) - 1) & "<br />"
                End If
            End If
            Dim controlloEdifici As String = ""
            If CheckBoxEdifici.Checked = True Then
                controlloEdifici = "Edifici: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListEdifici.Items
                    If Items.Selected = True Then
                        controlloEdifici &= Items.Text & ","
                    End If
                Next
                If controlloEdifici <> "" Then
                    controlloEdifici = "Edifici: " & Left(controlloEdifici, Len(controlloEdifici) - 1) & "<br />"
                End If
            End If
            Dim controlloIndirizzi As String = ""
            If CheckBoxIndirizzi.Checked = True Then
                controlloIndirizzi = "Indirizzi: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListIndirizzi.Items
                    If Items.Selected = True Then
                        controlloIndirizzi &= Items.Text & ","
                    End If
                Next
                If controlloIndirizzi <> "" Then
                    controlloIndirizzi = "Indirizzi: " & Left(controlloIndirizzi, Len(controlloIndirizzi) - 1) & "<br />"
                End If
            End If
            Dim controlloTipologiaRapporto As String = ""
            If CheckBoxTipologiaRapporto.Checked = True Then
                controlloTipologiaRapporto = "Tipologie Rapporto: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    If Items.Selected = True Then
                        controlloTipologiaRapporto &= Items.Text & ","
                    End If
                Next
                If controlloTipologiaRapporto <> "" Then
                    controlloTipologiaRapporto = "Tipologie Rapporto: " & Left(controlloTipologiaRapporto, Len(controlloTipologiaRapporto) - 1) & "<br />"
                End If
            End If
            Dim controlloContrattoSpecifico As String = ""
            If CheckBoxContrattoSpecifico.Checked = True Then
                controlloContrattoSpecifico = "Tipologie Contratto Specifico: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                    If Items.Selected = True Then
                        controlloContrattoSpecifico &= Items.Text & ","
                    End If
                Next
                If controlloContrattoSpecifico <> "" Then
                    controlloContrattoSpecifico = "Tipologie Contratto Specifico: " & Left(controlloContrattoSpecifico, Len(controlloContrattoSpecifico) - 1) & "<br />"
                End If
            End If
            Dim controlloTipologiaUI As String = ""
            If CheckBoxTipologiaUI.Checked = True Then
                controlloTipologiaUI = "Tipologie UI: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                    If Items.Selected = True Then
                        controlloTipologiaUI &= Items.Text & ","
                    End If
                Next
                If controlloTipologiaUI <> "" Then
                    controlloTipologiaUI = "Tipologie UI: " & Left(controlloTipologiaUI, Len(controlloTipologiaUI) - 1) & "<br />"
                End If
            End If
            Dim controlloareacanone As String = ""
            If CheckBoxAreaCanone.Checked = True Then
                controlloareacanone = "Area Canone: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListAreaCanone.Items
                    If Items.Selected = True Then
                        controlloareacanone &= Items.Text & ","
                    End If
                Next
                If controlloareacanone <> "" Then
                    controlloareacanone = "Area Canone: " & Left(controlloareacanone, Len(controlloareacanone) - 1) & "<br />"
                End If
            End If
            Dim controllostatoContratto As String = ""
            If CheckBoxStatoContratto.Checked = True Then
                controllostatoContratto = "Stato Contratto: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListStatoContratto.Items
                    If Items.Selected = True Then
                        controllostatoContratto &= Items.Text & ","
                    End If
                Next
                If controllostatoContratto <> "" Then
                    controllostatoContratto = "Stato Contratto: " & Left(controllostatoContratto, Len(controllostatoContratto) - 1) & "<br />"
                End If
            End If

            Dim controlloProtocollo As String = ""
            If CheckBoxProtocollo.Checked = True Then
                controlloProtocollo = "Protocollo gestore: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListProtocollo.Items
                    If Items.Selected = True Then
                        controlloProtocollo &= Items.Text & ","
                    End If
                Next
                If controlloProtocollo <> "" Then
                    controlloProtocollo = "Protocollo: " & Left(controlloProtocollo, Len(controlloProtocollo) - 1) & "<br />"
                End If
            End If

            Dim controllomora As String = ""
            For Each Items As ListItem In CheckBoxListMora.Items
                If Items.Selected = True Then
                    controllomora &= Items.Text & ","
                End If
            Next
            If controllomora <> "" Then
                controllomora = "Condizioni mora: " & Left(controllomora, Len(controllomora) - 1) & "<br />"
            End If


            Dim controlloStipula As String = ""
            If stipulaAl.Text <> "" And stipulaDal.Text <> "" Then
                controlloStipula = "Stipula contratto dal " & stipulaDal.Text & " al " & stipulaAl.Text & "<br />"
            ElseIf stipulaAl.Text <> "" And stipulaDal.Text = "" Then
                controlloStipula = "Stipula contratto fino al " & stipulaAl.Text & "<br />"
            ElseIf stipulaAl.Text = "" And stipulaDal.Text <> "" Then
                controlloStipula = "Stipula contratto dal " & stipulaDal.Text & "<br />"
            ElseIf stipulaAl.Text = "" And stipulaDal.Text = "" Then
                controlloStipula = ""
            End If

            Dim controlloprotocollodata As String = ""
            If protocolloAl.Text <> "" And protocolloDal.Text <> "" Then
                controlloprotocollodata = "protocollo contratto dal " & protocolloDal.Text & " al " & protocolloAl.Text & "<br />"
            ElseIf protocolloAl.Text <> "" And protocolloDal.Text = "" Then
                controlloprotocollodata = "protocollo contratto fino al " & protocolloAl.Text & "<br />"
            ElseIf protocolloAl.Text = "" And protocolloDal.Text <> "" Then
                controlloprotocollodata = "protocollo contratto dal " & protocolloDal.Text & "<br />"
            ElseIf protocolloAl.Text = "" And protocolloDal.Text = "" Then
                controlloprotocollodata = ""
            End If


            parametriDiRicerca &= controlloFiliali _
                & controlloQuartieri _
                & controlloComplessi _
                & controlloEdifici _
                & controlloIndirizzi _
                & controlloTipologiaRapporto _
                & controlloareacanone _
                & controlloContrattoSpecifico _
                & controlloTipologiaUI _
                & controllostatoContratto _
                & controlloprotocollodata _
                & controlloProtocollo _
                & controllomora _
                & controlloStipula


            Session.Add("PARAMETRIDIRICERCA", parametriDiRicerca)
            Dim lista As System.Collections.Generic.List(Of String) = Session.Item("listamorositaMM")
            If Not IsNothing(lista) Then
                For Each Items As DataGridItem In DataGridInquilini.Items
                    If CType(Items.FindControl("Checkbox1"), CheckBox).Checked = False Then
                        lista.Remove(Items.Cells(0).Text)
                    Else
                        If Not lista.Contains(Items.Cells(0).Text) Then
                            lista.Add(Items.Cells(0).Text)
                        End If
                    End If
                Next
            End If
            Session.Item("listamorositaMM") = lista
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub ButtonRiepilogoGenerale_Click(sender As Object, e As System.EventArgs) Handles ButtonRiepilogoGenerale.Click
        Try
            'CONTROLLO TUTTO QUELLO CHE E' STATO SELEZIONATO
            Dim parametriDiRicerca As String = ""
            Dim controlloFiliali As String = ""
            If CheckBoxFiliali.Checked = True Then
                controlloFiliali = "Filiali: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListFiliali.Items
                    If Items.Selected = True Then
                        controlloFiliali &= Items.Text & ","
                    End If
                Next
                If controlloFiliali <> "" Then
                    controlloFiliali = "Filiali: " & Left(controlloFiliali, Len(controlloFiliali) - 1) & "<br />"
                End If
            End If
            Dim controlloQuartieri As String = ""
            If CheckBoxQuartieri.Checked = True Then
                controlloQuartieri = "Quartieri: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListQuartieri.Items
                    If Items.Selected = True Then
                        controlloQuartieri &= Items.Text & ","
                    End If
                Next
                If controlloQuartieri <> "" Then
                    controlloQuartieri = "Quartieri: " & Left(controlloQuartieri, Len(controlloQuartieri) - 1) & "<br />"
                End If
            End If
            Dim controlloComplessi As String = ""
            If CheckBoxComplessi.Checked = True Then
                controlloComplessi = "Complessi: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListComplessi.Items
                    If Items.Selected = True Then
                        controlloComplessi &= Items.Text & ","
                    End If
                Next
                If controlloComplessi <> "" Then
                    controlloComplessi = "Complessi: " & Left(controlloComplessi, Len(controlloComplessi) - 1) & "<br />"
                End If
            End If
            Dim controlloEdifici As String = ""
            If CheckBoxEdifici.Checked = True Then
                controlloEdifici = "Edifici: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListEdifici.Items
                    If Items.Selected = True Then
                        controlloEdifici &= Items.Text & ","
                    End If
                Next
                If controlloEdifici <> "" Then
                    controlloEdifici = "Edifici: " & Left(controlloEdifici, Len(controlloEdifici) - 1) & "<br />"
                End If
            End If
            Dim controlloIndirizzi As String = ""
            If CheckBoxIndirizzi.Checked = True Then
                controlloIndirizzi = "Indirizzi: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListIndirizzi.Items
                    If Items.Selected = True Then
                        controlloIndirizzi &= Items.Text & ","
                    End If
                Next
                If controlloIndirizzi <> "" Then
                    controlloIndirizzi = "Indirizzi: " & Left(controlloIndirizzi, Len(controlloIndirizzi) - 1) & "<br />"
                End If
            End If
            Dim controlloTipologiaRapporto As String = ""
            If CheckBoxTipologiaRapporto.Checked = True Then
                controlloTipologiaRapporto = "Tipologie Rapporto: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    If Items.Selected = True Then
                        controlloTipologiaRapporto &= Items.Text & ","
                    End If
                Next
                If controlloTipologiaRapporto <> "" Then
                    controlloTipologiaRapporto = "Tipologie Rapporto: " & Left(controlloTipologiaRapporto, Len(controlloTipologiaRapporto) - 1) & "<br />"
                End If
            End If
            Dim controlloContrattoSpecifico As String = ""
            If CheckBoxContrattoSpecifico.Checked = True Then
                controlloContrattoSpecifico = "Tipologie Contratto Specifico: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                    If Items.Selected = True Then
                        controlloContrattoSpecifico &= Items.Text & ","
                    End If
                Next
                If controlloContrattoSpecifico <> "" Then
                    controlloContrattoSpecifico = "Tipologie Contratto Specifico: " & Left(controlloContrattoSpecifico, Len(controlloContrattoSpecifico) - 1) & "<br />"
                End If
            End If
            Dim controlloTipologiaUI As String = ""
            If CheckBoxTipologiaUI.Checked = True Then
                controlloTipologiaUI = "Tipologie UI: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                    If Items.Selected = True Then
                        controlloTipologiaUI &= Items.Text & ","
                    End If
                Next
                If controlloTipologiaUI <> "" Then
                    controlloTipologiaUI = "Tipologie UI: " & Left(controlloTipologiaUI, Len(controlloTipologiaUI) - 1) & "<br />"
                End If
            End If
            Dim controlloareacanone As String = ""
            If CheckBoxAreaCanone.Checked = True Then
                controlloareacanone = "Area Canone: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListAreaCanone.Items
                    If Items.Selected = True Then
                        controlloareacanone &= Items.Text & ","
                    End If
                Next
                If controlloareacanone <> "" Then
                    controlloareacanone = "Area Canone: " & Left(controlloareacanone, Len(controlloareacanone) - 1) & "<br />"
                End If
            End If
            Dim controllostatoContratto As String = ""
            If CheckBoxStatoContratto.Checked = True Then
                controllostatoContratto = "Stato Contratto: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListStatoContratto.Items
                    If Items.Selected = True Then
                        controllostatoContratto &= Items.Text & ","
                    End If
                Next
                If controllostatoContratto <> "" Then
                    controllostatoContratto = "Stato Contratto: " & Left(controllostatoContratto, Len(controllostatoContratto) - 1) & "<br />"
                End If
            End If

            Dim controlloProtocollo As String = ""
            If CheckBoxProtocollo.Checked = True Then
                controlloProtocollo = "Protocollo gestore: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListProtocollo.Items
                    If Items.Selected = True Then
                        controlloProtocollo &= Items.Text & ","
                    End If
                Next
                If controlloProtocollo <> "" Then
                    controlloProtocollo = "Protocollo: " & Left(controlloProtocollo, Len(controlloProtocollo) - 1) & "<br />"
                End If
            End If

            Dim controllomora As String = ""
            For Each Items As ListItem In CheckBoxListMora.Items
                If Items.Selected = True Then
                    controllomora &= Items.Text & ","
                End If
            Next
            If controllomora <> "" Then
                controllomora = "Condizioni mora: " & Left(controllomora, Len(controllomora) - 1) & "<br />"
            End If


            Dim controlloStipula As String = ""
            If stipulaAl.Text <> "" And stipulaDal.Text <> "" Then
                controlloStipula = "Stipula contratto dal " & stipulaDal.Text & " al " & stipulaAl.Text & "<br />"
            ElseIf stipulaAl.Text <> "" And stipulaDal.Text = "" Then
                controlloStipula = "Stipula contratto fino al " & stipulaAl.Text & "<br />"
            ElseIf stipulaAl.Text = "" And stipulaDal.Text <> "" Then
                controlloStipula = "Stipula contratto dal " & stipulaDal.Text & "<br />"
            ElseIf stipulaAl.Text = "" And stipulaDal.Text = "" Then
                controlloStipula = ""
            End If

            Dim controlloprotocollodata As String = ""
            If protocolloAl.Text <> "" And protocolloDal.Text <> "" Then
                controlloprotocollodata = "protocollo contratto dal " & protocolloDal.Text & " al " & protocolloAl.Text & "<br />"
            ElseIf protocolloAl.Text <> "" And protocolloDal.Text = "" Then
                controlloprotocollodata = "protocollo contratto fino al " & protocolloAl.Text & "<br />"
            ElseIf protocolloAl.Text = "" And protocolloDal.Text <> "" Then
                controlloprotocollodata = "protocollo contratto dal " & protocolloDal.Text & "<br />"
            ElseIf protocolloAl.Text = "" And protocolloDal.Text = "" Then
                controlloprotocollodata = ""
            End If


            parametriDiRicerca &= controlloFiliali _
                & controlloQuartieri _
                & controlloComplessi _
                & controlloEdifici _
                & controlloIndirizzi _
                & controlloTipologiaRapporto _
                & controlloareacanone _
                & controlloContrattoSpecifico _
                & controlloTipologiaUI _
                & controllostatoContratto _
                & controlloprotocollodata _
                & controlloProtocollo _
                & controllomora _
                & controlloStipula


            Session.Add("PARAMETRIDIRICERCA", parametriDiRicerca)
            caricaInquilini2()
            'PreCaricaRiepilogo()
            Dim lista As System.Collections.Generic.List(Of String) = Session.Item("LISTAMOROSITAMM")
            If Not IsNothing(lista) Then
                If lista.Count <= 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Nessun risultato trovato! E\' necessario reimpostare i parametri di ricerca!');", True)
                Else
                    Response.Redirect("MesseInMora.aspx?d=" & par.AggiustaData(dataEstrazione.Text) & "&t=1")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Nessun risultato trovato! E\' necessario reimpostare i parametri di ricerca!');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Protected Sub ButtonDettaglioInquilini_Click(sender As Object, e As System.EventArgs) Handles ButtonDettaglioInquilini.Click
        MultiView1.ActiveViewIndex = 1
        caricaInquilini()
    End Sub
    Protected Sub ButtonRiepilogoBollette_Click(sender As Object, e As System.EventArgs) Handles ButtonRiepilogoBollette.Click
        Try
            'CONTROLLO TUTTO QUELLO CHE E' STATO SELEZIONATO
            Dim parametriDiRicerca As String = ""
            Dim controlloFiliali As String = ""
            If CheckBoxFiliali.Checked = True Then
                controlloFiliali = "Filiali: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListFiliali.Items
                    If Items.Selected = True Then
                        controlloFiliali &= Items.Text & ","
                    End If
                Next
                If controlloFiliali <> "" Then
                    controlloFiliali = "Filiali: " & Left(controlloFiliali, Len(controlloFiliali) - 1) & "<br />"
                End If
            End If
            Dim controlloQuartieri As String = ""
            If CheckBoxQuartieri.Checked = True Then
                controlloQuartieri = "Quartieri: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListQuartieri.Items
                    If Items.Selected = True Then
                        controlloQuartieri &= Items.Text & ","
                    End If
                Next
                If controlloQuartieri <> "" Then
                    controlloQuartieri = "Quartieri: " & Left(controlloQuartieri, Len(controlloQuartieri) - 1) & "<br />"
                End If
            End If
            Dim controlloComplessi As String = ""
            If CheckBoxComplessi.Checked = True Then
                controlloComplessi = "Complessi: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListComplessi.Items
                    If Items.Selected = True Then
                        controlloComplessi &= Items.Text & ","
                    End If
                Next
                If controlloComplessi <> "" Then
                    controlloComplessi = "Complessi: " & Left(controlloComplessi, Len(controlloComplessi) - 1) & "<br />"
                End If
            End If
            Dim controlloEdifici As String = ""
            If CheckBoxEdifici.Checked = True Then
                controlloEdifici = "Edifici: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListEdifici.Items
                    If Items.Selected = True Then
                        controlloEdifici &= Items.Text & ","
                    End If
                Next
                If controlloEdifici <> "" Then
                    controlloEdifici = "Edifici: " & Left(controlloEdifici, Len(controlloEdifici) - 1) & "<br />"
                End If
            End If
            Dim controlloIndirizzi As String = ""
            If CheckBoxIndirizzi.Checked = True Then
                controlloIndirizzi = "Indirizzi: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListIndirizzi.Items
                    If Items.Selected = True Then
                        controlloIndirizzi &= Items.Text & ","
                    End If
                Next
                If controlloIndirizzi <> "" Then
                    controlloIndirizzi = "Indirizzi: " & Left(controlloIndirizzi, Len(controlloIndirizzi) - 1) & "<br />"
                End If
            End If
            Dim controlloTipologiaRapporto As String = ""
            If CheckBoxTipologiaRapporto.Checked = True Then
                controlloTipologiaRapporto = "Tipologie Rapporto: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    If Items.Selected = True Then
                        controlloTipologiaRapporto &= Items.Text & ","
                    End If
                Next
                If controlloTipologiaRapporto <> "" Then
                    controlloTipologiaRapporto = "Tipologie Rapporto: " & Left(controlloTipologiaRapporto, Len(controlloTipologiaRapporto) - 1) & "<br />"
                End If
            End If
            Dim controlloContrattoSpecifico As String = ""
            If CheckBoxContrattoSpecifico.Checked = True Then
                controlloContrattoSpecifico = "Tipologie Contratto Specifico: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                    If Items.Selected = True Then
                        controlloContrattoSpecifico &= Items.Text & ","
                    End If
                Next
                If controlloContrattoSpecifico <> "" Then
                    controlloContrattoSpecifico = "Tipologie Contratto Specifico: " & Left(controlloContrattoSpecifico, Len(controlloContrattoSpecifico) - 1) & "<br />"
                End If
            End If
            Dim controlloTipologiaUI As String = ""
            If CheckBoxTipologiaUI.Checked = True Then
                controlloTipologiaUI = "Tipologie UI: TUTTE<br />"
            Else
                For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                    If Items.Selected = True Then
                        controlloTipologiaUI &= Items.Text & ","
                    End If
                Next
                If controlloTipologiaUI <> "" Then
                    controlloTipologiaUI = "Tipologie UI: " & Left(controlloTipologiaUI, Len(controlloTipologiaUI) - 1) & "<br />"
                End If
            End If
            Dim controlloareacanone As String = ""
            If CheckBoxAreaCanone.Checked = True Then
                controlloareacanone = "Area Canone: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListAreaCanone.Items
                    If Items.Selected = True Then
                        controlloareacanone &= Items.Text & ","
                    End If
                Next
                If controlloareacanone <> "" Then
                    controlloareacanone = "Area Canone: " & Left(controlloareacanone, Len(controlloareacanone) - 1) & "<br />"
                End If
            End If
            Dim controllostatoContratto As String = ""
            If CheckBoxStatoContratto.Checked = True Then
                controllostatoContratto = "Stato Contratto: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListStatoContratto.Items
                    If Items.Selected = True Then
                        controllostatoContratto &= Items.Text & ","
                    End If
                Next
                If controllostatoContratto <> "" Then
                    controllostatoContratto = "Stato Contratto: " & Left(controllostatoContratto, Len(controllostatoContratto) - 1) & "<br />"
                End If
            End If

            Dim controlloProtocollo As String = ""
            If CheckBoxProtocollo.Checked = True Then
                controlloProtocollo = "Protocollo gestore: TUTTI<br />"
            Else
                For Each Items As ListItem In CheckBoxListProtocollo.Items
                    If Items.Selected = True Then
                        controlloProtocollo &= Items.Text & ","
                    End If
                Next
                If controlloProtocollo <> "" Then
                    controlloProtocollo = "Protocollo: " & Left(controlloProtocollo, Len(controlloProtocollo) - 1) & "<br />"
                End If
            End If

            Dim controllomora As String = ""
            For Each Items As ListItem In CheckBoxListMora.Items
                If Items.Selected = True Then
                    controllomora &= Items.Text & ","
                End If
            Next
            If controllomora <> "" Then
                controllomora = "Condizioni mora: " & Left(controllomora, Len(controllomora) - 1) & "<br />"
            End If


            Dim controlloStipula As String = ""
            If stipulaAl.Text <> "" And stipulaDal.Text <> "" Then
                controlloStipula = "Stipula contratto dal " & stipulaDal.Text & " al " & stipulaAl.Text & "<br />"
            ElseIf stipulaAl.Text <> "" And stipulaDal.Text = "" Then
                controlloStipula = "Stipula contratto fino al " & stipulaAl.Text & "<br />"
            ElseIf stipulaAl.Text = "" And stipulaDal.Text <> "" Then
                controlloStipula = "Stipula contratto dal " & stipulaDal.Text & "<br />"
            ElseIf stipulaAl.Text = "" And stipulaDal.Text = "" Then
                controlloStipula = ""
            End If

            Dim controlloprotocollodata As String = ""
            If protocolloAl.Text <> "" And protocolloDal.Text <> "" Then
                controlloprotocollodata = "protocollo contratto dal " & protocolloDal.Text & " al " & protocolloAl.Text & "<br />"
            ElseIf protocolloAl.Text <> "" And protocolloDal.Text = "" Then
                controlloprotocollodata = "protocollo contratto fino al " & protocolloAl.Text & "<br />"
            ElseIf protocolloAl.Text = "" And protocolloDal.Text <> "" Then
                controlloprotocollodata = "protocollo contratto dal " & protocolloDal.Text & "<br />"
            ElseIf protocolloAl.Text = "" And protocolloDal.Text = "" Then
                controlloprotocollodata = ""
            End If


            parametriDiRicerca &= controlloFiliali _
                & controlloQuartieri _
                & controlloComplessi _
                & controlloEdifici _
                & controlloIndirizzi _
                & controlloTipologiaRapporto _
                & controlloareacanone _
                & controlloContrattoSpecifico _
                & controlloTipologiaUI _
                & controllostatoContratto _
                & controlloprotocollodata _
                & controlloProtocollo _
                & controllomora _
                & controlloStipula


            Session.Add("PARAMETRIDIRICERCA", parametriDiRicerca)
            caricaInquilini2()
            'PreCaricaRiepilogo()
            Dim lista As System.Collections.Generic.List(Of String) = Session.Item("LISTAMOROSITAMM")
            If Not IsNothing(lista) Then
                If lista.Count <= 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Nessun risultato trovato! E\' necessario reimpostare i parametri di ricerca!');", True)
                Else
                    Response.Redirect("MesseInMora.aspx?d=" & par.AggiustaData(dataEstrazione.Text) & "&t=3")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Nessun risultato trovato! E\' necessario reimpostare i parametri di ricerca!');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Private Sub caricaProtocolli()
        Try
            CheckBoxListProtocollo.Items.Clear()
            ApriConnessione()
            par.cmd.CommandText = "SELECT DISTINCT PROTOCOLLO_ALER FROM SISCOM_MI.MOROSITA ORDER BY PROTOCOLLO_ALER"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListProtocollo.DataSource = dataTable
            CheckBoxListProtocollo.DataTextField = "PROTOCOLLO_ALER"
            CheckBoxListProtocollo.DataValueField = "PROTOCOLLO_ALER"
            CheckBoxListProtocollo.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Protected Sub CheckBoxProtocollo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxProtocollo.CheckedChanged
        Try
            If CheckBoxProtocollo.Checked = True Then
                For Each Items As ListItem In CheckBoxListProtocollo.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListProtocollo.Items
                    Items.Selected = False
                Next
            End If
            'caricaInquilini()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub
    Protected Sub CheckBoxListProtocollo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListProtocollo.SelectedIndexChanged
        'caricaInquilini()
    End Sub
    Protected Sub protocolloDal_TextChanged(sender As Object, e As System.EventArgs) Handles protocolloDal.TextChanged
        'caricaInquilini()
    End Sub
    Protected Sub protocolloAl_TextChanged(sender As Object, e As System.EventArgs) Handles protocolloAl.TextChanged
        'caricaInquilini()
    End Sub
    Private Sub caricaInquilini2()
        Try
            ApriConnessione()
            Dim dataTable As New Data.DataTable
            '---------------SELEZIONE FILIALI------------------------
            Dim FilialiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListFiliali.Items
                If Items.Selected = True Then
                    FilialiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneFiliali As String = ""
            Dim condizioneFilialiContratti As String = ""
            If FilialiSelezionati <> "" Then
                FilialiSelezionati = Left(FilialiSelezionati, Len(FilialiSelezionati) - 1)
                condizioneFilialiContratti = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (" & FilialiSelezionati & ") "
                condizioneFiliali = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.EDIFICI " _
                    & " WHERE   ID_COMPLESSO IN (SELECT ID " _
                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE IN (" & FilialiSelezionati & ") " _
                    & " ))))) "
            End If
            '---------------SELEZIONE QUARTIERI------------------------
            Dim QuartieriSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListQuartieri.Items
                If Items.Selected = True Then
                    QuartieriSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneQuartieri As String = ""
            Dim condizioneQuartieriContratti As String = ""
            If QuartieriSelezionati <> "" Then
                QuartieriSelezionati = Left(QuartieriSelezionati, Len(QuartieriSelezionati) - 1)
                condizioneQuartieriContratti = " AND COMPLESSI_IMMOBILIARI.ID_QUARTIERE IN (" & QuartieriSelezionati & ") "
                condizioneQuartieri = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.EDIFICI " _
                    & " WHERE   ID_COMPLESSO IN (SELECT ID " _
                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_QUARTIERE IN (" & QuartieriSelezionati & ") " _
                    & " ))))) "
            End If
            '---------------SELEZIONE COMPLESSI------------------------
            Dim complessiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListComplessi.Items
                If Items.Selected = True Then
                    complessiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneComplessi As String = ""
            Dim condizioneComplessiContratti As String = ""
            If complessiSelezionati <> "" Then
                complessiSelezionati = Left(complessiSelezionati, Len(complessiSelezionati) - 1)
                condizioneComplessiContratti = " AND COMPLESSI_IMMOBILIARI.ID IN (" & complessiSelezionati & ") "
                condizioneComplessi = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.EDIFICI " _
                    & " WHERE   ID_COMPLESSO IN (" & complessiSelezionati & ") " _
                    & " )))) "
            End If
            '---------------SELEZIONE EDIFICI------------------------
            Dim edificiSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListEdifici.Items
                If Items.Selected = True Then
                    edificiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneEdifici As String = ""
            Dim condizioneEdificiContratti As String = ""
            If edificiSelezionati <> "" Then
                edificiSelezionati = Left(edificiSelezionati, Len(edificiSelezionati) - 1)
                condizioneEdificiContratti = " AND EDIFICI.ID IN (" & edificiSelezionati & ") "
                condizioneEdifici = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN (" & edificiSelezionati _
                    & " )))) "
            End If
            '---------------SELEZIONE INDIRIZZI------------------------
            Dim IndirizziSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListIndirizzi.Items
                If Items.Selected = True Then
                    IndirizziSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneIndirizzi As String = ""
            Dim condizioneIndirizziContratti As String = ""
            If IndirizziSelezionati <> "" Then
                IndirizziSelezionati = Left(IndirizziSelezionati, Len(IndirizziSelezionati) - 1)
                condizioneIndirizziContratti = " AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (" & IndirizziSelezionati & ") "
                condizioneIndirizzi = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & " SELECT  ID_MOROSITA " _
                    & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                    & " WHERE   ID_CONTRATTO IN (" _
                    & " SELECT  ID_CONTRATTO " _
                    & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " WHERE   ID_UNITA IN (" _
                    & " SELECT  ID " _
                    & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE   ID_EDIFICIO IN ( SELECT ID " _
                    & " FROM SISCOM_MI.EDIFICI WHERE ID_INDIRIZZO_PRINCIPALE IN (" & IndirizziSelezionati _
                    & " ))))) "
            End If
            '---------------SELEZIONE TIPOLOGIA RAPPORTO------------------------
            Dim TipologiaRapportoSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Selected = True Then
                    TipologiaRapportoSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneTipologiaRapporto As String = ""
            Dim condizioneTipologiaRapportoContratti As String = ""
            If TipologiaRapportoSelezionati <> "" Then
                TipologiaRapportoSelezionati = Left(TipologiaRapportoSelezionati, Len(TipologiaRapportoSelezionati) - 1)
                condizioneTipologiaRapportoContratti = " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC IN (" & TipologiaRapportoSelezionati & ") "
                condizioneTipologiaRapporto = " AND SISCOM_MI.MOROSITA.ID IN (" _
                        & " SELECT  ID_MOROSITA " _
                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                        & " WHERE   ID_CONTRATTO IN (" _
                        & " SELECT  ID " _
                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                        & " WHERE   COD_TIPOLOGIA_CONTR_LOC IN (" _
                        & TipologiaRapportoSelezionati _
                        & "))) "
            End If
            '---------------SELEZIONE CONTRATTO SPECIFICO------------------------
            Dim ContrattoSpecificoSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    ContrattoSpecificoSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneContrattoSpecifico As String = ""
            Dim ERP As Boolean = False
            Dim L43198 As Boolean = False
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Value = "ERP" Then
                    If Items.Selected = True Then
                        ERP = True
                    End If
                ElseIf Items.Value = "L43198" Then
                    If Items.Selected = True Then
                        L43198 = True
                    End If
                End If
            Next
            Dim PRIMO As Boolean = True
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    If ERP = True Then
                        Select Case Items.Value
                            Case "12"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=12)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=12)) "
                                End If
                            Case "8"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                         & " SELECT  ID_MOROSITA " _
                                         & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                         & " WHERE   ID_CONTRATTO IN (" _
                                         & " SELECT  ID " _
                                         & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                         & " WHERE   PROVENIENZA_ASS=8)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=8)) "
                                End If
                            Case "10"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=10)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=10)) "
                                End If
                            Case "2"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico = " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                       & " SELECT  ID_MOROSITA " _
                                       & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                       & " WHERE   ID_CONTRATTO IN (" _
                                       & " SELECT  ID_CONTRATTO " _
                                       & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                                       & " WHERE   ID_UNITA IN (" _
                                       & " SELECT  ID " _
                                       & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                                       & " WHERE   ID_DESTINAZIONE_USO=2))) "
                                Else
                                    condizioneContrattoSpecifico = " or SISCOM_MI.MOROSITA.ID IN (" _
                                       & " SELECT  ID_MOROSITA " _
                                       & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                       & " WHERE   ID_CONTRATTO IN (" _
                                       & " SELECT  ID_CONTRATTO " _
                                       & " FROM    SISCOM_MI.UNITA_CONTRATTUALE " _
                                       & " WHERE   ID_UNITA IN (" _
                                       & " SELECT  ID " _
                                       & " FROM    SISCOM_MI.UNITA_IMMOBILIARI " _
                                       & " WHERE   ID_DESTINAZIONE_USO=2))) "
                                End If
                            Case "1"
                                If PRIMO = True Then
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   PROVENIENZA_ASS=1)) "
                                    PRIMO = False
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                         & " SELECT  ID_MOROSITA " _
                                         & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                         & " WHERE   ID_CONTRATTO IN (" _
                                         & " SELECT  ID " _
                                         & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                         & " WHERE   PROVENIENZA_ASS=1)) "
                                End If
                        End Select
                    End If
                    If L43198 = True Then
                        Select Case Items.Value
                            Case "0"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='0')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='0')) "
                                End If
                            Case "C"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='C')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='C')) "
                                End If
                            Case "P"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='P')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='P')) "
                                End If
                            Case "D"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='D')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='D')) "
                                End If
                            Case "S"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizioneContrattoSpecifico &= " AND (SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='S')) "
                                Else
                                    condizioneContrattoSpecifico &= " or SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DEST_USO='S')) "
                                End If
                        End Select
                    End If
                End If
            Next
            If condizioneContrattoSpecifico <> "" Then
                condizioneContrattoSpecifico &= ") "
            End If


            '---------------SELEZIONE CONTRATTO SPECIFICO------------------------
            Dim contrattoSpecificoCSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    contrattoSpecificoCSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizionecontrattoSpecificoC As String = ""
            ERP = False
            L43198 = False
            For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                If Items.Value = "ERP" Then
                    If Items.Selected = True Then
                        ERP = True
                    End If
                ElseIf Items.Value = "L43198" Then
                    If Items.Selected = True Then
                        L43198 = True
                    End If
                End If
            Next
            PRIMO = True
            For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
                If Items.Selected = True Then
                    If ERP = True Then
                        Select Case Items.Value
                            Case "12"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=12 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=12 "
                                End If
                            Case "8"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=8 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=8 "
                                End If
                            Case "10"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=10 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=10 "
                                End If
                            Case "2"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO=2 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO=2 "
                                End If
                            Case "1"
                                If PRIMO = True Then
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.PROVENIENZA_ASS=1 "
                                    PRIMO = False
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=1 "
                                End If

                        End Select
                    End If
                    If L43198 = True Then
                        Select Case Items.Value
                            Case "0"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='0' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='0' "
                                End If
                            Case "C"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='C' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='C' "
                                End If
                            Case "P"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='P' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='P' "
                                End If
                            Case "D"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='D' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='D' "
                                End If
                            Case "S"
                                If PRIMO = True Then
                                    PRIMO = False
                                    condizionecontrattoSpecificoC &= " AND (RAPPORTI_UTENZA.DEST_USO='S' "
                                Else
                                    condizionecontrattoSpecificoC &= " OR RAPPORTI_UTENZA.DEST_USO='S' "
                                End If
                        End Select
                    End If

                End If
            Next
            If condizionecontrattoSpecificoC <> "" Then
                condizionecontrattoSpecificoC &= ") "
            End If
            '---------------SELEZIONE TIPOLOGIAUI------------------------
            Dim TipologiaUISelezionati As String = ""
            For Each Items As ListItem In CheckBoxListTipologiaUI.Items
                If Items.Selected = True Then
                    TipologiaUISelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneTipologiaUI As String = ""
            Dim condizioneTipologiaUIcontratti As String = ""
            If TipologiaUISelezionati <> "" Then
                TipologiaUISelezionati = Left(TipologiaUISelezionati, Len(TipologiaUISelezionati) - 1)
                condizioneTipologiaUIcontratti = " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD IN (" & TipologiaUISelezionati & ") "
                condizioneTipologiaUI = " AND SISCOM_MI.MOROSITA.ID in (" _
                    & " select  ID_MOROSITA " _
                    & " from    SISCOM_MI.MOROSITA_LETTERE " _
                    & " where   ID_CONTRATTO in (" _
                    & " select  ID_CONTRATTO " _
                    & " from    SISCOM_MI.UNITA_CONTRATTUALE " _
                    & " where   TIPOLOGIA in (" & TipologiaUISelezionati & ")" _
                    & " )) "
            End If
            '---------------SELEZIONE STATO------------------------
            Dim statoINCORSO As Boolean = False
            Dim statoCHIUSO As Boolean = False
            Dim indice As Integer = 0
            For Each Items As ListItem In CheckBoxListStatoContratto.Items
                If Items.Selected = True Then
                    If indice = 0 Then
                        statoINCORSO = True
                    Else
                        statoCHIUSO = True
                    End If
                End If
                indice += 1
            Next
            Dim condizioneStatoContratto As String = ""
            Dim condizioneStatoContrattoC As String = ""
            If statoINCORSO = True And statoCHIUSO = True Then
            ElseIf statoINCORSO = True And statoCHIUSO = False Then
                condizioneStatoContrattoC = " AND RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL "
                condizioneStatoContratto &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_RICONSEGNA IS NULL)) "
            ElseIf statoINCORSO = False And statoCHIUSO = True Then
                condizioneStatoContrattoC = " AND RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL "
                condizioneStatoContratto &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_RICONSEGNA IS NOT NULL)) "
            ElseIf statoINCORSO = False And statoCHIUSO = False Then
            End If
            '---------------SELEZIONE AREA CANONE------------------------
            Dim AreaCanoneSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListAreaCanone.Items
                If Items.Selected = True Then
                    AreaCanoneSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneAreaCanone As String = ""
            Dim condizioneAreaCanoneContratti As String = ""
            If AreaCanoneSelezionati <> "" Then
                AreaCanoneSelezionati = Left(AreaCanoneSelezionati, Len(AreaCanoneSelezionati) - 1)
                condizioneAreaCanoneContratti = " AND RAPPORTI_UTENZA.ID IN (SELECT DISTINCT ID_CONTRATTO FROM SISCOM_MI.CANONI_EC WHERE ID_AREA_ECONOMICA IN (" & AreaCanoneSelezionati & ")) "
                condizioneAreaCanone &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (SELECT DISTINCT " _
                                        & " ID_CONTRATTO FROM SISCOM_MI.CANONI_EC WHERE " _
                                        & " ID_AREA_ECONOMICA IN (" & AreaCanoneSelezionati & "))) "
            End If
            '---------------CONDIZIONE STIPULA DEL CONTRATTO-------------------------------------------
            Dim condizioneStipula As String = ""
            Dim condizioneStipulaContratti As String = ""
            If par.AggiustaData(stipulaDal.Text) <> "" Then
                condizioneStipulaContratti &= " AND RAPPORTI_UTENZA.DATA_STIPULA>='" & par.AggiustaData(stipulaDal.Text) & "' "
                condizioneStipula &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_STIPULA>='" & par.AggiustaData(stipulaDal.Text) & "')) "
            End If
            If par.AggiustaData(stipulaAl.Text) <> "" Then
                condizioneStipulaContratti &= " AND RAPPORTI_UTENZA.DATA_STIPULA<='" & par.AggiustaData(stipulaAl.Text) & "' "
                condizioneStipula &= " AND SISCOM_MI.MOROSITA.ID IN (" _
                                        & " SELECT  ID_MOROSITA " _
                                        & " FROM    SISCOM_MI.MOROSITA_LETTERE " _
                                        & " WHERE   ID_CONTRATTO IN (" _
                                        & " SELECT  ID " _
                                        & " FROM    SISCOM_MI.RAPPORTI_UTENZA " _
                                        & " WHERE   DATA_STIPULA<='" & par.AggiustaData(stipulaAl.Text) & "')) "
            End If
            '---------------CONDIZIONE PROTOCOLLO -------------------------------------------
            Dim ProtocolloSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListProtocollo.Items
                If Items.Selected = True Then
                    ProtocolloSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizioneProtocollo As String = ""
            If ProtocolloSelezionati <> "" Then
                ProtocolloSelezionati = Left(ProtocolloSelezionati, Len(ProtocolloSelezionati) - 1)
                condizioneProtocollo = " AND SISCOM_MI.MOROSITA.PROTOCOLLO_ALER in (" & ProtocolloSelezionati & ") "
            End If
            '---------------CONDIZIONE DATA PROTOCOLLO -------------------------------------------
            Dim condizioneDataProtocollo As String = ""
            If par.AggiustaData(protocolloDal.Text) <> "" Then
                condizioneDataProtocollo &= " AND SISCOM_MI.MOROSITA.DATA_PROTOCOLLO>='" & par.AggiustaData(protocolloDal.Text) & "' "
            End If
            If par.AggiustaData(protocolloAl.Text) <> "" Then
                condizioneDataProtocollo &= " AND SISCOM_MI.MOROSITA.DATA_PROTOCOLLO<='" & par.AggiustaData(protocolloAl.Text) & "' "
            End If
            '--------------- condizione tipolettera ----------------------------------------------------------
            Dim moraSelezionati As String = ""
            For Each Items As ListItem In CheckBoxListMora.Items
                If Items.Selected = True Then
                    moraSelezionati &= "'" & Items.Value & "',"
                End If
            Next
            Dim condizionemora As String = ""
            If moraSelezionati <> "" Then
                moraSelezionati = Left(moraSelezionati, Len(moraSelezionati) - 1)
                condizionemora = " AND SISCOM_MI.MOROSITA.ID IN (" _
                    & "select ID_MOROSITA from SISCOM_MI.MOROSITA_LETTERE where TIPO_LETTERA in (" & moraSelezionati & "))"
            End If

            If condizioneAreaCanone <> "" Or _
                condizioneComplessi <> "" Or _
                    condizioneContrattoSpecifico <> "" Or _
                    condizioneEdifici <> "" Or _
                    condizioneFiliali <> "" Or _
                    condizioneIndirizzi <> "" Or
                    condizioneQuartieri <> "" Or _
                    condizioneStatoContratto <> "" Or _
                    condizioneTipologiaRapporto <> "" Or _
                    condizioneTipologiaUI <> "" Or _
                    condizioneProtocollo <> "" Or _
                    condizioneDataProtocollo <> "" Or _
                    condizioneStipula <> "" Then

                'par.cmd.CommandText = "SELECT MOROSITA.PROGR,MOROSITA.ID AS ID_MOROSITA,MOROSITA.PROTOCOLLO_ALER," _
                '  & " TO_CHAR(TO_DATE(SUBSTR(MOROSITA.DATA_PROTOCOLLO,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_PROTOCOLLO," _
                '  & " MOROSITA.TIPO_INVIO," _
                '  & " TO_CHAR(TO_DATE(SUBSTR(MOROSITA.RIF_DA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_DAL," _
                '  & " TO_CHAR(TO_DATE(SUBSTR(MOROSITA.RIF_A,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_AL," _
                '  & " NOTE,MOROSITA.DATA_PROTOCOLLO AS DATA_PROTOCOLLO_ORD " _
                '  & " FROM  " _
                '  & " SISCOM_MI.MOROSITA WHERE 1=1 " _
                '  & condizioneComplessi _
                '  & condizioneQuartieri _
                '  & condizioneEdifici _
                '  & condizioneFiliali _
                '  & condizioneAreaCanone _
                '  & condizioneContrattoSpecifico _
                '  & condizioneTipologiaUI _
                '  & condizioneStatoContratto _
                '  & condizioneStipula _
                '  & condizioneIndirizzi _
                '  & condizioneTipologiaRapporto _
                '  & condizioneProtocollo _
                '  & condizioneDataProtocollo

                'par.cmd.CommandText = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                '    & " NVL(SISCOM_MI.SALDI.SALDO,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=650,width=900'');£>'||trim(RAPPORTI_UTENZA.COD_CONTRATTO)||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''top=0,left=0'');£>'||" _
                '    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                '    & " then  trim(RAGIONE_SOCIALE) " _
                '    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                '    & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                '    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                '    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                '    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                '    & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                '    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                '    & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                '    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                '    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                '    & " then trim(RAGIONE_SOCIALE) " _
                '    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                '    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                '    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                '    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                '    & " SISCOM_MI.ANAGRAFICA, " _
                '    & " SISCOM_MI.INDIRIZZI, " _
                '    & " SISCOM_MI.EDIFICI, " _
                '    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                '    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                '    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                '    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                '    & " SISCOM_MI.SALDI " _
                '    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                '    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                '    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                '    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                '    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                '    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                '    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                '    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                '    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                '    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                '    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                '    & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '    & condizioneEdificiContratti _
                '    & condizioneQuartieriContratti _
                '    & condizioneComplessiContratti _
                '    & condizioneFilialiContratti _
                '    & condizioneIndirizziContratti _
                '    & condizioneTipologiaRapportoContratti _
                '    & condizionecontrattoSpecificoC _
                '    & condizioneTipologiaUIcontratti _
                '    & condizioneStatoContrattoC _
                '    & condizioneAreaCanoneContratti _
                '    & condizioneStipulaContratti _
                '    & "ORDER BY " & DropDownListOrdinamento.SelectedValue


                'par.cmd.CommandText = "SELECT id_contratto " _
                '    & " from siscom_mi.morosita_lettere where id_morosita in (" _
                '    & " select id from siscom_mi.morosita " _
                '    & " WHERE 1=1 " _
                '  & condizioneComplessi _
                '  & condizioneQuartieri _
                '  & condizioneEdifici _
                '  & condizioneFiliali _
                '  & condizioneAreaCanone _
                '  & condizioneContrattoSpecifico _
                '  & condizioneTipologiaUI _
                '  & condizioneStatoContratto _
                '  & condizioneStipula _
                '  & condizioneIndirizzi _
                '  & condizioneTipologiaRapporto _
                '  & condizioneProtocollo _
                '  & condizioneDataProtocollo _
                '  & " and id_contratto in (" _
                '  & " SELECT rapporti_utenza.id " _
                '  & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                '  & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                '  & " SISCOM_MI.RAPPORTI_UTENZA, " _
                '  & " SISCOM_MI.ANAGRAFICA, " _
                '  & " SISCOM_MI.INDIRIZZI, " _
                '  & " SISCOM_MI.EDIFICI, " _
                '  & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                '  & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                '  & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                '  & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                '  & " SISCOM_MI.SALDI " _
                '  & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                '  & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                '  & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                '  & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                '  & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                '  & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '  & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '  & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                '  & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                '  & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                '  & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                '  & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                '  & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                '  & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                '  & condizioneEdificiContratti _
                '  & condizioneQuartieriContratti _
                '  & condizioneComplessiContratti _
                '  & condizioneFilialiContratti _
                '  & condizioneIndirizziContratti _
                '  & condizioneTipologiaRapportoContratti _
                '  & condizionecontrattoSpecificoC _
                '  & condizioneTipologiaUIcontratti _
                '  & condizioneStatoContrattoC _
                '  & condizioneAreaCanoneContratti _
                '  & condizioneStipulaContratti _
                '  & "))"




                par.cmd.CommandText = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                        & " NVL(SISCOM_MI.SALDI.SALDO,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||RAPPORTI_UTENZA.ID||''',''Contratto'',''height=780,width=1160'');£>'||trim(RAPPORTI_UTENZA.COD_CONTRATTO)||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO ," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CONTABILITA/DatiUtenza.aspx?C=RisUtenza&IDANA='||ANAGRAFICA.ID||'&IDCONT='||RAPPORTI_UTENZA.ID||''',''DatiUtente'' ,''top=0,left=0'');£>'||" _
                        & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & " then  trim(RAGIONE_SOCIALE) " _
                        & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                        & " ||'</a>','$','&'),'£','" & Chr(34) & "') as  INTESTATARIO ," _
                        & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI.SALDO,0) ,'9G999G999G999G999G990D99'))   as DEBITO2," _
                        & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                        & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                        & " replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE ," _
                        & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                        & " trim(INDIRIZZI.DESCRIZIONE) AS ""INDIRIZZO"",trim(INDIRIZZI.CIVICO) as ""CIVICO"" ," _
                        & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as COMUNE_UNITA," _
                        & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                        & " then trim(RAGIONE_SOCIALE) " _
                        & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.RAPPORTI_UTENZA, " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.EDIFICI, " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.SALDI " _
                        & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                        & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                        & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                        & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                        & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                        & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                        & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                        & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                        & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                        & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                        & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                        & " and rapporti_utenza.id in (" _
                        & "SELECT id_contratto " _
                        & " from siscom_mi.morosita_lettere where id_morosita in (" _
                        & " select id from siscom_mi.morosita " _
                        & " WHERE 1=1 " _
                        & condizioneComplessi _
                        & condizioneQuartieri _
                        & condizioneEdifici _
                        & condizioneFiliali _
                        & condizioneAreaCanone _
                        & condizioneContrattoSpecifico _
                        & condizioneTipologiaUI _
                        & condizioneStatoContratto _
                        & condizioneStipula _
                        & condizioneIndirizzi _
                        & condizioneTipologiaRapporto _
                        & condizioneProtocollo _
                        & condizioneDataProtocollo _
                        & condizionemora _
                        & " and id_contratto in (" _
                        & " SELECT rapporti_utenza.id " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                        & " SISCOM_MI.RAPPORTI_UTENZA, " _
                        & " SISCOM_MI.ANAGRAFICA, " _
                        & " SISCOM_MI.INDIRIZZI, " _
                        & " SISCOM_MI.EDIFICI, " _
                        & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                        & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                        & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                        & " SISCOM_MI.SALDI " _
                        & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                        & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                      & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                      & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                      & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                      & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                      & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                      & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                      & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                      & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                      & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                      & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                      & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                      & " AND SALDI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                      & condizioneEdificiContratti _
                      & condizioneQuartieriContratti _
                      & condizioneComplessiContratti _
                      & condizioneFilialiContratti _
                      & condizioneIndirizziContratti _
                      & condizioneTipologiaRapportoContratti _
                      & condizionecontrattoSpecificoC _
                      & condizioneTipologiaUIcontratti _
                      & condizioneStatoContrattoC _
                      & condizioneAreaCanoneContratti _
                      & condizioneStipulaContratti _
                      & "))" _
                      & ") " _
                      & "ORDER BY " & DropDownListOrdinamento.SelectedValue
                Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dataAdapter.Fill(dataTable)
                'DataGridInquilini.DataSource = dataTable
                'DataGridInquilini.DataBind()
                'If dataTable.Rows.Count = 1 Then
                'Nintestatari.Text = "ESTRATTO " & dataTable.Rows.Count & " INTESTATARIO"
                'Else
                'Nintestatari.Text = "ESTRATTI " & dataTable.Rows.Count & " INTESTATARI"
                'End If
                'PanelInquilini.Visible = True
                'Else
                'DataGridInquilini.DataSource = dataTable
                'DataGridInquilini.DataBind()
                'Nintestatari.Text = ""
                'PanelInquilini.Visible = False
            End If
            'If IsNothing(Session.Item("listamorositaMM")) Then
            '    Dim lista As New System.Collections.Generic.List(Of String)
            '    For Each items As Data.DataRow In dataTable.Rows
            '        lista.Add(CStr(items.Item(1)))
            '    Next
            '    Session.Add("listamorositaMM", lista)
            'End If
            Dim lista As New System.Collections.Generic.List(Of String)
            For Each items As Data.DataRow In dataTable.Rows
                lista.Add(CStr(items.Item(1)))
            Next
            Session.Add("listamorositaMM", lista)
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Protected Sub ButtonProcediDettaglioInquilini_Click(sender As Object, e As System.EventArgs) Handles ButtonProcediDettaglioInquilini.Click
        Try
            PreCaricaRiepilogo()
            Dim lista As System.Collections.Generic.List(Of String) = Session.Item("LISTAMOROSITAMM")
            If Not IsNothing(lista) Then
                If lista.Count <= 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('E\' necessario selezionare almeno un inquilino!');", True)
                Else
                    Response.Redirect("MesseInMora.aspx?d=" & par.AggiustaData(dataEstrazione.Text) & "&t=2")
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('E\' necessario selezionare almeno un inquilino!');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Protected Sub ButtonNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles ButtonNuovaRicerca.Click
        MultiView1.ActiveViewIndex = 0
    End Sub
End Class