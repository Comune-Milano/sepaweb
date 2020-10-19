
Partial Class Contratti_Report_RicercaDocGestionali
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaFiliali()
            caricaQuartieri()
            caricaTipologiaRapporti()
            caricaTipologiaUI()
            caricaStatoContratto()
            caricaTipoDocGest()
            riferimentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            riferimentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            emissioneAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            emissioneDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            stipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            stipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            importoDA.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
            importoDA.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            importoA.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
            importoA.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
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
                & "WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.FILIALI_UI) ORDER BY NOME"
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
                condizioneFiliali = " AND COMPLESSI_IMMOBILIARI.ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.FILIALI_UI WHERE EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND FILIALI_UI.ID_UI=UNITA_IMMOBILIARI.ID AND ID_FILIALE IN (" & filialiSelezionate & ")) "
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

    Private Sub caricaTipoDocGest()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,DESCRIZIONE " _
                & "FROM SISCOM_MI.TIPO_BOLLETTE_GEST " _
                & "ORDER BY DESCRIZIONE ASC"
            Dim dataAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            CheckBoxListTipoDoc.DataSource = dataTable
            CheckBoxListTipoDoc.DataTextField = "DESCRIZIONE"
            CheckBoxListTipoDoc.DataValueField = "ID"
            CheckBoxListTipoDoc.DataBind()
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
                        CheckBoxListAreaCanone.Items.Add(New ListItem("NON DEFINITA", "0"))
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

    Protected Sub CheckBoxListFiliali_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListFiliali.SelectedIndexChanged
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

    Protected Sub CheckBoxListQuartieri_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListQuartieri.SelectedIndexChanged
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

    Protected Sub CheckBoxListComplessi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListComplessi.SelectedIndexChanged
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

    Protected Sub CheckBoxListEdifici_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListEdifici.SelectedIndexChanged
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

    Protected Sub CheckBoxListIndirizzi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListIndirizzi.SelectedIndexChanged
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

    Protected Sub CheckBoxListTipologiaRapporti_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipologiaRapporti.SelectedIndexChanged
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

    Protected Sub CheckBoxListContrattoSpecifico_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListContrattoSpecifico.SelectedIndexChanged
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

    Protected Sub CheckBoxListTipologiaUI_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipologiaUI.SelectedIndexChanged
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

    Protected Sub CheckBoxListStatoContratto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListStatoContratto.SelectedIndexChanged
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

    Protected Sub CheckBoxListAreaCanone_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListAreaCanone.SelectedIndexChanged
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
            Dim script As String = ""
            script = "if(document.getElementById('PanelFiliali')!=null){document.getElementById('PanelFiliali').scrollTop = document.getElementById('yPosFiliali').value;}"

            script &= "if(document.getElementById('PanelQuartieri')!=null){document.getElementById('PanelQuartieri').scrollTop = document.getElementById('yPosQuartieri').value;}"

            script &= "if(document.getElementById('PanelComplessi')!=null){document.getElementById('PanelComplessi').scrollTop = document.getElementById('yPosComplessi').value;}"

            script &= "if(document.getElementById('PanelEdifici')!=null){document.getElementById('PanelEdifici').scrollTop = document.getElementById('yPosEdifici').value;}"

            script &= "if(document.getElementById('PanelIndirizzi')!=null){document.getElementById('PanelIndirizzi').scrollTop = document.getElementById('yPosIndirizzi').value;}"

            script &= "if(document.getElementById('PanelContrattoSpecifico')!=null){document.getElementById('PanelContrattoSpecifico').scrollTop = document.getElementById('yPosContrattoSpecifico').value;}"

            script &= "if(document.getElementById('PanelTipologiaUI')!=null){document.getElementById('PanelTipologiaUI').scrollTop = document.getElementById('yPosTipologiaUI').value;}"

            ScriptManager.RegisterStartupScript(Page, GetType(Panel), Page.ClientID, script, True)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CheckBoxTipoDoc_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipoDoc.CheckedChanged
        Try
            If CheckBoxTipoDoc.Checked = True Then
                For Each Items As ListItem In CheckBoxListTipoDoc.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListTipoDoc.Items
                    Items.Selected = False
                Next
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try

    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Dim parametriDaPassare As String = ""
        parametriDaPassare &= "?DataStipulaDa=" & par.FormatoDataDB(stipulaDal.Text)
        parametriDaPassare &= "&DataStipulaA=" & par.FormatoDataDB(stipulaAl.Text)
        parametriDaPassare &= "&DataEmissioneDal=" & par.FormatoDataDB(emissioneDal.Text)
        parametriDaPassare &= "&DataEmissioneAl=" & par.FormatoDataDB(emissioneAl.Text)
        parametriDaPassare &= "&CodContratto=" & txtCodContr.Text
        parametriDaPassare &= "&RiferimentoDal=" & par.FormatoDataDB(riferimentoDal.Text)
        parametriDaPassare &= "&RiferimentoAl=" & par.FormatoDataDB(riferimentoAl.Text)
        parametriDaPassare &= "&ImportoDa=" & importoDA.Text
        parametriDaPassare &= "&ImportoA=" & importoA.Text
        parametriDaPassare &= "&Ordinamento=" & DropDownListOrdinamento.SelectedValue
        parametriDaPassare &= "&Credito=" & DropDownListCredDeb.SelectedValue

        If CheckBoxElab.Checked = True Then
            parametriDaPassare &= "&Elaborati=0"
        End If

        'FILTRO FILIALI
        Dim listaFiliali As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListFiliali.Items
            If Items.Selected = True Then
                If Not listaFiliali.Contains(Items.Value) Then
                    listaFiliali.Add(Items.Value)
                End If
            Else
                If listaFiliali.Contains(Items.Value) Then
                    listaFiliali.Remove(Items.Value)
                End If
            End If
        Next

        Session.Add("listaFiliali", listaFiliali)

        'FILTRO QUARTIERI
        Dim listaQuartieri As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListQuartieri.Items
            If Items.Selected = True Then
                If Not listaQuartieri.Contains(Items.Value) Then
                    listaQuartieri.Add(Items.Value)
                End If
            Else
                If listaQuartieri.Contains(Items.Value) Then
                    listaQuartieri.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaQuartieri", listaQuartieri)

        'FILTRO COMPLESSI
        Dim listaComplessi As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListComplessi.Items
            If Items.Selected = True Then
                If Not listaComplessi.Contains(Items.Value) Then
                    listaComplessi.Add(Items.Value)
                End If
            Else
                If listaComplessi.Contains(Items.Value) Then
                    listaComplessi.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaComplessi", listaComplessi)

        'FILTRO EDIFICI
        Dim listaEdifici As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListEdifici.Items
            If Items.Selected = True Then
                If Not listaEdifici.Contains(Items.Value) Then
                    listaEdifici.Add(Items.Value)
                End If
            Else
                If listaEdifici.Contains(Items.Value) Then
                    listaEdifici.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaEdifici", listaEdifici)

        'FILTRO INDIRIZZI
        Dim listaIndirizzi As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListIndirizzi.Items
            If Items.Selected = True Then
                If Not listaIndirizzi.Contains(Items.Value) Then
                    listaIndirizzi.Add(Items.Value)
                End If
            Else
                If listaIndirizzi.Contains(Items.Value) Then
                    listaIndirizzi.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaIndirizzi", listaIndirizzi)

        'FILTRO TIPO RAPPORTO
        Dim listaTipoRapporto As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
            If Items.Selected = True Then
                If Not listaTipoRapporto.Contains(Items.Value) Then
                    listaTipoRapporto.Add(Items.Value)
                End If
            Else
                If listaTipoRapporto.Contains(Items.Value) Then
                    listaTipoRapporto.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipoRapporto", listaTipoRapporto)

        'FILTRO TIPO CONTRATTO SPEC.
        Dim listaContrattoSpec As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
            If Items.Selected = True Then
                If Not listaContrattoSpec.Contains(Items.Value) Then
                    listaContrattoSpec.Add(Items.Value)
                End If
            Else
                If listaContrattoSpec.Contains(Items.Value) Then
                    listaContrattoSpec.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaContrattoSpec", listaContrattoSpec)

        'FILTRO TIPOLOGIA UI
        Dim listaTipoUI As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologiaUI.Items
            If Items.Selected = True Then
                If Not listaTipoUI.Contains(Items.Value) Then
                    listaTipoUI.Add(Items.Value)
                End If
            Else
                If listaTipoUI.Contains(Items.Value) Then
                    listaTipoUI.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipoUI", listaTipoUI)

        'FILTRO STATO CONTRATTO
        Dim listaStatoContr As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListStatoContratto.Items
            If Items.Selected = True Then
                If Not listaStatoContr.Contains(Items.Value) Then
                    listaStatoContr.Add(Items.Text)
                End If
            Else
                If listaStatoContr.Contains(Items.Value) Then
                    listaStatoContr.Remove(Items.Text)
                End If
            End If
        Next
        Session.Add("listaStatoContr", listaStatoContr)

        'FILTRO AREA CANONE
        Dim listaAreaCanone As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListAreaCanone.Items
            If Items.Selected = True Then
                If Not listaAreaCanone.Contains(Items.Value) Then
                    listaAreaCanone.Add(Items.Value)
                End If
            Else
                If listaAreaCanone.Contains(Items.Value) Then
                    listaAreaCanone.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaAreaCanone", listaAreaCanone)

        'FILTRO TIPO DOC. GESTIONALE
        Dim listaTipoDoc As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipoDoc.Items
            If Items.Selected = True Then
                If Not listaTipoDoc.Contains(Items.Value) Then
                    listaTipoDoc.Add(Items.Value)
                End If
            Else
                If listaTipoDoc.Contains(Items.Value) Then
                    listaTipoDoc.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipoDoc", listaTipoDoc)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg0", "window.open('RisultatoDocGest.aspx" & parametriDaPassare & "','_blank','');", True)
    End Sub
End Class
