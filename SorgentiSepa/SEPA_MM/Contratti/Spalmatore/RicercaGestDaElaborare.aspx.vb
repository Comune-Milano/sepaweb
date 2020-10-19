Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class Contratti_Spalmatore_RicercaGestDaElaborare
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = dgvElaborazioni.ClientID.ToString.Replace("ctl00", "MasterPage")
            caricaTipologiaRapporti()
            caricaTipologiaUI()
            caricaStatoContratto()
            caricaTipoDocGest()
            caricaEccezioni()

        End If

    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

        MultiView1.ActiveViewIndex = CInt(HFMultiView1.Value)
        MultiView2.ActiveViewIndex = CInt(HFMultiView2.Value)
        If HFMultiView1.Value.ToString = "0" Then
            HFbtnClickGo.Value = btnAvviaRicerca.ClientID
            btnAvviaRicerca.Focus()
        ElseIf HFMultiView1.Value.ToString = "1" Then
            HFbtnClickGo.Value = btnProcedi.ClientID
            dgvElaborazioni.Focus()
            lblAsterisco.Text = ""
        Else
            HFbtnClickGo.Value = ""
            Me.Page.Focus()
        End If
    End Sub


    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportGestionali", "ExportGestionali", dt)
        If IO.File.Exists(Server.MapPath("..\..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!", True)
        End If
    End Sub
    Private Sub CaricaFiltriCkbList()
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

        Dim listaEccezioni As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListEccez.Items
            If Items.Selected = True Then
                If Not listaEccezioni.Contains(Items.Value) Then
                    listaEccezioni.Add(Items.Value)
                End If
            Else
                If listaEccezioni.Contains(Items.Value) Then
                    listaEccezioni.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaEccezioni", listaEccezioni)

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
    End Sub

    Private Function stringaFiltriChkList() As String
        CaricaFiltriCkbList()

        Dim condizioniWhere As String = ""

        Dim listaTipoRapporto As System.Collections.Generic.List(Of String) = Session.Item("listaTipoRapporto")
        Session.Remove("listaTipoRapporto")
        Dim listaTipoRapportoSi As Boolean = False
        Dim condizionelistaTipoRapporto As String = ""
        If Not IsNothing(listaTipoRapporto) Then
            For Each Items As String In listaTipoRapporto

                If Items <> "ERP" And Items <> "L43198" Then
                    condizionelistaTipoRapporto &= "'" & Items & "',"
                End If

            Next
        End If
        If condizionelistaTipoRapporto <> "" Then
            condizionelistaTipoRapporto = Left(condizionelistaTipoRapporto, Len(condizionelistaTipoRapporto) - 1)

            condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC IN (" & condizionelistaTipoRapporto.ToUpper & ") "

            listaTipoRapportoSi = True
        End If


        '---------------SELEZIONE CONTRATTO SPECIFICO------------------------
        Dim ContrattoSpecificoSelezionati As String = ""
        For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
            If Items.Selected = True Then
                ContrattoSpecificoSelezionati &= "'" & Items.Value & "',"
            End If
        Next
        Dim condizioneContrattoSpecifico As String = ""
        Dim condizioneContrattoSpecificoL431 As String = ""
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
        Dim condizioneTipoRU1 As Boolean = False
        Dim condizioneTipoRU2 As Boolean = False
        For Each Items As ListItem In CheckBoxListContrattoSpecifico.Items
            If Items.Selected = True Then
                If ERP = True Then
                    condizioneTipoRU1 = False
                    Select Case Items.Value
                        Case "12"
                            If condizioneContrattoSpecifico = "" Then
                                condizioneContrattoSpecifico = " RAPPORTI_UTENZA.PROVENIENZA_ASS=12 "
                            Else
                                condizioneContrattoSpecifico &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=12 "
                            End If
                        Case "8"
                            If condizioneContrattoSpecifico = "" Then
                                condizioneContrattoSpecifico = " RAPPORTI_UTENZA.PROVENIENZA_ASS=8 "
                            Else
                                condizioneContrattoSpecifico &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=8 "
                            End If
                        Case "10"
                            If condizioneContrattoSpecifico = "" Then
                                condizioneContrattoSpecifico = " RAPPORTI_UTENZA.PROVENIENZA_ASS=10 "
                            Else
                                condizioneContrattoSpecifico &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=10 "
                            End If
                        Case "2"
                            If condizioneContrattoSpecifico = "" Then
                                condizioneContrattoSpecifico = " UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO=2 "
                            Else
                                condizioneContrattoSpecifico &= " OR UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO=2 "
                            End If
                        Case "1"
                            If condizioneContrattoSpecifico = "" Then
                                condizioneContrattoSpecifico = " RAPPORTI_UTENZA.PROVENIENZA_ASS=1 "
                            Else
                                condizioneContrattoSpecifico &= " OR RAPPORTI_UTENZA.PROVENIENZA_ASS=1 "
                            End If
                        Case Else
                            condizioneTipoRU1 = True
                    End Select
                    If condizioneContrattoSpecifico <> "" Then
                        If condizionelistaTipoRapporto = "" Then
                            condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC ='ERP' AND (" & condizioneContrattoSpecifico & ")"
                        Else
                            condizionelistaTipoRapporto = condizionelistaTipoRapporto & " OR (COD_TIPOLOGIA_CONTR_LOC ='ERP' AND (" & condizioneContrattoSpecifico & "))"
                        End If
                    Else
                        If condizionelistaTipoRapporto Then
                            condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC ='ERP' "
                        Else
                            condizionelistaTipoRapporto = condizionelistaTipoRapporto & " OR COD_TIPOLOGIA_CONTR_LOC ='ERP' "
                        End If
                    End If
                End If
                If L43198 = True Then
                    condizioneTipoRU2 = True
                    Select Case Items.Value
                        Case "0"
                            If condizioneContrattoSpecificoL431 = "" Then
                                condizioneContrattoSpecificoL431 = " RAPPORTI_UTENZA.DEST_USO='0' "
                            Else
                                condizioneContrattoSpecificoL431 &= " OR RAPPORTI_UTENZA.DEST_USO='0' "
                            End If
                        Case "C"
                            If condizioneContrattoSpecificoL431 = "" Then
                                condizioneContrattoSpecificoL431 = " RAPPORTI_UTENZA.DEST_USO='C' "
                            Else
                                condizioneContrattoSpecificoL431 &= " OR RAPPORTI_UTENZA.DEST_USO='C' "
                            End If
                        Case "P"
                            If condizioneContrattoSpecificoL431 = "" Then
                                condizioneContrattoSpecificoL431 = " RAPPORTI_UTENZA.DEST_USO='P' "
                            Else
                                condizioneContrattoSpecificoL431 &= " OR RAPPORTI_UTENZA.DEST_USO='P' "
                            End If
                        Case "D"
                            If condizioneContrattoSpecificoL431 = "" Then
                                condizioneContrattoSpecificoL431 = " RAPPORTI_UTENZA.DEST_USO='D' "
                            Else
                                condizioneContrattoSpecificoL431 &= " OR RAPPORTI_UTENZA.DEST_USO='D' "
                            End If
                        Case "S"
                            If condizioneContrattoSpecificoL431 = "" Then
                                condizioneContrattoSpecificoL431 = " RAPPORTI_UTENZA.DEST_USO='S' "
                            Else
                                condizioneContrattoSpecificoL431 &= " OR RAPPORTI_UTENZA.DEST_USO='S' "
                            End If
                        Case Else
                            condizioneTipoRU2 = False
                    End Select
                    If condizioneContrattoSpecificoL431 <> "" Then
                        If condizionelistaTipoRapporto = "" Then
                            condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC ='L43198' AND (" & condizioneContrattoSpecificoL431 & ")"
                        Else
                            condizionelistaTipoRapporto = condizionelistaTipoRapporto & " OR (COD_TIPOLOGIA_CONTR_LOC ='L43198' AND (" & condizioneContrattoSpecificoL431 & "))"
                        End If
                    Else
                        If condizionelistaTipoRapporto = "" Then
                            condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC ='L43198' "
                        Else
                            condizionelistaTipoRapporto = condizionelistaTipoRapporto & " OR COD_TIPOLOGIA_CONTR_LOC ='L43198' "
                        End If

                    End If
                End If

            End If
        Next
        If L43198 = True And condizioneContrattoSpecificoL431 = "" Then
            If condizionelistaTipoRapporto = "" Then
                condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC ='L43198' "
            Else
                condizionelistaTipoRapporto = condizionelistaTipoRapporto & " OR COD_TIPOLOGIA_CONTR_LOC ='L43198' "
            End If
        End If
        If ERP = True And condizioneContrattoSpecifico = "" Then
            If condizionelistaTipoRapporto = "" Then
                condizionelistaTipoRapporto = " COD_TIPOLOGIA_CONTR_LOC ='ERP' "
            Else
                condizionelistaTipoRapporto = condizionelistaTipoRapporto & " OR COD_TIPOLOGIA_CONTR_LOC ='ERP' "
            End If
        End If

        If condizionelistaTipoRapporto <> "" Then
            condizionelistaTipoRapporto = " AND (" & condizionelistaTipoRapporto & ")"
        End If

        '######## CONDIZIONE TIPOLOGIA ############
        Dim listaTipoUI As System.Collections.Generic.List(Of String) = Session.Item("listaTipoUI")
        Session.Remove("listaTipoUI")
        Dim listaTipoUISi As Boolean = False
        Dim condizionelistaTipoUI As String = ""
        If Not IsNothing(listaTipoUI) Then
            For Each Items As String In listaTipoUI
                condizionelistaTipoUI &= "'" & Items & "',"
            Next
        End If
        If condizionelistaTipoUI <> "" Then
            condizionelistaTipoUI = Left(condizionelistaTipoUI, Len(condizionelistaTipoUI) - 1)
            condizionelistaTipoUI = " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA IN (" & condizionelistaTipoUI & ") "
            listaTipoUISi = True
        End If
        '########################################################


        '######## CONDIZIONE STATO CONTRATTO ############
        Dim listaStatoContr As System.Collections.Generic.List(Of String) = Session.Item("listaStatoContr")
        Session.Remove("listaStatoContr")
        Dim listaStatoContrSi As Boolean = False
        Dim condizionelistaStatoContr As String = ""
        If Not IsNothing(listaStatoContr) Then
            For Each Items As String In listaStatoContr
                condizionelistaStatoContr &= "'" & Items & "',"
            Next
        End If
        If condizionelistaStatoContr <> "" Then
            condizionelistaStatoContr = Left(condizionelistaStatoContr, Len(condizionelistaStatoContr) - 1)
            condizionelistaStatoContr = " AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) IN (" & condizionelistaStatoContr.ToUpper & ") "
            listaStatoContrSi = True
        End If
        '########################################################

        '############## CONDIZIONE TIPO DOCUMENTO ###############
        Dim listaTipoDoc As System.Collections.Generic.List(Of String) = Session.Item("listaTipoDoc")
        Session.Remove("listaTipoDoc")
        Dim listaTipoDocSi As Boolean = False
        Dim condizionelistaTipoDoc As String = ""
        If Not IsNothing(listaTipoDoc) Then
            For Each Items As String In listaTipoDoc
                condizionelistaTipoDoc &= "'" & Items & "',"
            Next
        End If
        If condizionelistaTipoDoc <> "" Then
            condizionelistaTipoDoc = Left(condizionelistaTipoDoc, Len(condizionelistaTipoDoc) - 1)
            condizionelistaTipoDoc = " AND BOL_BOLLETTE_GEST.ID_TIPO IN (" & condizionelistaTipoDoc & ") "
            listaTipoDocSi = True
        End If
        '########################################################

        condizioniWhere = condizionelistaTipoRapporto & condizionelistaTipoUI & condizionelistaStatoContr & condizionelistaTipoDoc

        Return condizioniWhere

    End Function

    Protected Sub CheckBoxTipologiaRapporto_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipologiaRapporto.CheckedChanged
        Try
            If CheckBoxTipologiaRapporto.Checked = True Then
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    Items.Selected = True
                Next
                caricaContrattoSpecifico()
            Else
                For Each Items As ListItem In CheckBoxListTipologiaRapporti.Items
                    Items.Selected = False
                Next
                CheckBoxListContrattoSpecifico.Items.Clear()
            End If
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

    Private Sub caricaTipologiaRapporti()
        Try
            connData.apri()
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
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
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
                        CheckBoxListContrattoSpecifico.Items.Add(New ListItem("Standard", "0"))
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
            connData.apri()
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
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Private Sub caricaTipoDocGest()
        Try
            connData.apri()
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

            par.cmd.CommandText = "SELECT ID,DESCRIZIONE " _
                & "FROM SISCOM_MI.TIPO_BOLLETTE_GEST WHERE FL_CHECKATO=1 " _
                & "ORDER BY DESCRIZIONE ASC"
            Dim dataAdapter2 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable2 As New Data.DataTable
            dataAdapter2.Fill(dataTable2)

            If dataTable2.Rows.Count > 0 Then
                For Each Items As ListItem In CheckBoxListTipoDoc.Items
                    For Each row As Data.DataRow In dataTable2.Rows
                        If row.Item("ID") = Items.Value Then
                            Items.Selected = True
                        End If
                    Next
                Next
            End If

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Private Sub caricaStatoContratto()
        Try
            CheckBoxListStatoContratto.Items.Clear()
            CheckBoxListStatoContratto.Items.Add(New ListItem("IN CORSO", "1"))
            CheckBoxListStatoContratto.Items.Add(New ListItem("IN CORSO (S.T.)", "2"))
            CheckBoxListStatoContratto.Items.Add(New ListItem("CHIUSO", "0"))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Private Sub caricaEccezioni()
        Try
            CheckBoxListEccez.Items.Clear()
            CheckBoxListEccez.Items.Add(New ListItem("Escludi KPi1", "KP"))
            CheckBoxListEccez.Items.Add(New ListItem("Escludi Art.15 c.2 bis", "ART15"))
            CheckBoxListEccez.Items.Add(New ListItem("Escludi UI vendute", "VEND"))
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub



    Protected Sub chkSelTutti_CheckedChanged(sender As Object, e As System.EventArgs)
        dgvElaborazioni.AllowPaging = False
        dgvElaborazioni.Rebind()
        If HiddenCheck.Value = "0" Then
            HiddenCheck.Value = "1"
            For i As Integer = 0 To dgvElaborazioni.Items.Count - 1
                DirectCast(dgvElaborazioni.Items(i).FindControl("CheckBox1"), RadButton).Checked = True
            Next
        Else
            HiddenCheck.Value = "0"
            For i As Integer = 0 To dgvElaborazioni.Items.Count - 1
                DirectCast(dgvElaborazioni.Items(i).FindControl("CheckBox1"), RadButton).Checked = False
            Next
        End If
        CheckBox()
        Dim int As Integer = dgvElaborazioni.Items.Count
        dgvElaborazioni.AllowPaging = True
        dgvElaborazioni.Rebind()
    End Sub

    Protected Sub CheckBox()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_GEST"), Data.DataTable)

            Dim row As Data.DataRow

            For i As Integer = 0 To dgvElaborazioni.Items.Count - 1

                If DirectCast(dgvElaborazioni.Items(i).FindControl("CheckBox1"), RadButton).Checked = False Then
                    row = dt.Select("id_boll = " & dgvElaborazioni.Items(i).Cells(2).Text)(0)
                    row.Item("CHECKALL") = "FALSE"
                Else
                    row = dt.Select("id_boll = " & dgvElaborazioni.Items(i).Cells(2).Text)(0)
                    row.Item("CHECKALL") = "TRUE"
                End If
            Next

            Session.Item("DT_GEST") = dt
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs)
        CheckBox()
    End Sub
    Private Function EsportaQuery() As String

        Dim condizioniWhere As String = stringaFiltriChkList()

        If cmbPianoRateizzo.SelectedValue <> -1 Then
            If cmbPianoRateizzo.SelectedValue = 1 Then
                condizioniWhere &= " and exists (Select id_contratto from siscom_mi.bol_rateizzazioni where nvl(fl_annullata,0)=0 and bol_rateizzazioni.id_contratto=rapporti_utenza.id) "
            Else
                condizioniWhere &= " and not exists (Select id_contratto from siscom_mi.bol_rateizzazioni where nvl(fl_annullata,0)=0 and bol_rateizzazioni.id_contratto=rapporti_utenza.id) "
            End If
        End If

        If cmbTipoImporto.SelectedValue <> -1 Then
            If cmbTipoImporto.SelectedValue = 1 Then
                condizioniWhere &= " and bol_bollette_gest.importo_Totale<0 "
            Else
                condizioniWhere &= " and bol_bollette_gest.importo_Totale>0 "
            End If
        End If

        If cmbSemaforo.SelectedValue <> -1 Then
            If cmbSemaforo.SelectedValue = 1 Then
                condizioniWhere &= " and bol_bollette_gest.fl_sbloccata=1 "
            Else
                condizioniWhere &= " and bol_bollette_gest.fl_sbloccata=0 "
            End If
        End If

        Dim listaEccez As System.Collections.Generic.List(Of String) = Session.Item("listaEccezioni")
        Session.Remove("listaEccezioni")
        Dim condizionelistaKPi1 As String = ""
        Dim condizionelistaArt15 As String = ""
        Dim condizionelistaVEND As String = ""
        If Not IsNothing(listaEccez) Then
            For Each Items As String In listaEccez
                Select Case Items
                    Case "KP"
                        condizionelistaKPi1 = " and not exists (select id_contratto from siscom_mi.SPALM_KPI1 where id_contratto=rapporti_utenza.id) "
                    Case "ART15"
                        condizionelistaArt15 = " and not exists (select id_contratto from siscom_mi.SPALM_ART_15 where id_contratto=rapporti_utenza.id) "
                    Case "VEND"
                        condizionelistaVEND = " and unita_immobiliari.cod_tipo_disponibilita <>'VEND' "
                End Select
            Next
        End If

        condizioniWhere &= condizionelistaKPi1 & condizionelistaArt15 & condizionelistaVEND

        par.cmd.CommandText = "SELECT bol_bollette_GEST.id as id_boll,'TRUE' AS CHECKALL,to_char(BOL_BOLLETTE_GEST.IMPORTO_TOTALE,'9G999G990D99') as IMP_EMESSO,'' as DETTAGLI,BOL_BOLLETTE_GEST.IMPORTO_TOTALE as importoTOT,rapporti_utenza.ID as id,cod_contratto, tipologia_contratto_locazione.descrizione AS tipo_cont, " _
                        & "tipologia_unita_immobiliari.descrizione AS tipo_ui,data_emissione,riferimento_da, " _
                        & "TO_CHAR (TO_DATE (replace(data_emissione,'NULL',''), 'yyyymmdd'),'dd/mm/yyyy') AS data_emiss," _
                        & "TO_CHAR (TO_DATE (replace(riferimento_da,'NULL',''), 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim1," _
                        & "TO_CHAR (TO_DATE (replace(riferimento_a,'NULL',''), 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim2, " _
                        & "(CASE " _
                        & "WHEN rapporti_utenza.provenienza_ass = 1 " _
                        & "AND unita_immobiliari.id_destinazione_uso <> 2 " _
                        & "THEN 'ERP Sociale' " _
                        & "WHEN unita_immobiliari.id_destinazione_uso = 2 " _
                        & "THEN 'ERP Moderato' " _
                        & "WHEN rapporti_utenza.provenienza_ass = 12 " _
                        & "THEN 'CANONE CONVENZ.' " _
                        & "WHEN rapporti_utenza.provenienza_ass = 10 " _
                        & "THEN 'FORZE DELL''ORDINE' " _
                        & "WHEN rapporti_utenza.dest_uso = 'C' " _
                        & "THEN 'Cooperative' " _
                        & "WHEN rapporti_utenza.dest_uso = 'P' " _
                        & "THEN '431 P.O.R.' " _
                        & "WHEN rapporti_utenza.dest_uso = 'D' " _
                        & "THEN '431/98 ART.15 R.R.1/2004' " _
                        & "WHEN rapporti_utenza.dest_uso = 'S' " _
                        & "THEN '431/98 Speciali' " _
                        & "WHEN rapporti_utenza.dest_uso = '0' " _
                        & "THEN 'Standard' " _
                        & "END " _
                        & ") AS TIPO_spec,indirizzi.descrizione ||', '|| indirizzi.civico as indirizzo, bol_bollette_GEST.note," _
                        & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (anagrafica.cognome || ' ' || anagrafica.nome) ) END) AS INTESTATARIO," _
                        & "tipo_bollette_gest.DESCRIZIONE AS TIPO_DOC,soggetti_contrattuali.id_anagrafica," _
                        & "(CASE WHEN BOL_BOLLETTE_GEST.IMPORTO_TOTALE <0 THEN '1' ELSE '0' END) AS CREDITO,BOL_BOLLETTE_GEST.TIPO_APPLICAZIONE,unita_immobiliari.cod_unita_immobiliare " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,siscom_mi.tipologia_contratto_locazione,siscom_mi.tipologia_unita_immobiliari,siscom_mi.tipo_bollette_gest,SISCOM_MI.unita_immobiliari,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica,siscom_mi.indirizzi " _
                        & "WHERE BOL_BOLLETTE_GEST.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND FL_VISUALIZZABILE=1 AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO and UNITA_CONTRATTUALE.id_unita=UNITA_IMMOBILIARI.ID and UNITA_CONTRATTUALE.id_unita_principale is null " _
                        & "AND tipologia_contratto_locazione.cod = rapporti_utenza.cod_tipologia_contr_loc and bol_bollette_gest.id_tipo=tipo_bollette_gest.id and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_contratto=rapporti_utenza.id " _
                        & "AND unita_immobiliari.id_indirizzo = indirizzi.ID (+) and bol_Bollette_gest.tipo_applicazione='N' and COD_TIPOLOGIA_OCCUPANTE='INTE' AND unita_immobiliari.cod_tipologia = tipologia_unita_immobiliari.cod " _
                        & condizioniWhere & " order by bol_bollette_gest.data_emissione asc,bol_bollette_gest.id asc"

        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_GEST") = dt


        queryXLS = "  SELECT DISTINCT " _
                        & "         TO_CHAR (SYSDATE, 'DD/MM/YYYY') data_estrazione," _
                        & "         rapporti_utenza.cod_contratto ru," _
                        & "         BOL_BOLLETTE_GEST.id_contratto," _
                        & "         siscom_mi.getdata (rapporti_utenza.data_decorrenza) data_decorrenza_contratto," _
                        & "         siscom_mi.getdata (rapporti_utenza.data_riconsegna) data_chiusura_contratto," _
                        & "         rapporti_utenza.cod_tipologia_contr_loc tipo_ru," _
                        & "         (CASE" _
                        & "             WHEN rapporti_utenza.PROVENIENZA_ASS = 1 AND unita_immobiliari.ID_DESTINAZIONE_USO <> 2" _
                        & "             THEN" _
                        & "                'ERP Sociale'" _
                        & "             WHEN unita_immobiliari.ID_DESTINAZIONE_USO = 2" _
                        & "             THEN" _
                        & "                'ERP Moderato'" _
                        & "             WHEN rapporti_utenza.PROVENIENZA_ASS = 12" _
                        & "             THEN" _
                        & "                'CANONE CONVENZ.'" _
                        & "             WHEN rapporti_utenza.PROVENIENZA_ASS = 10" _
                        & "             THEN" _
                        & "                'FORZE DELL''ORDINE'" _
                        & "             WHEN rapporti_utenza.DEST_USO = 'C'" _
                        & "             THEN" _
                        & "                'Cooperative'" _
                        & "             WHEN rapporti_utenza.DEST_USO = 'P'" _
                        & "             THEN" _
                        & "                '431 P.O.R.'" _
                        & "             WHEN rapporti_utenza.DEST_USO = 'D'" _
                        & "             THEN" _
                        & "                '431/98 ART.15 R.R.1/2004'" _
                        & "             WHEN rapporti_utenza.DEST_USO = 'V'" _
                        & "             THEN" _
                        & "                '431/98 ART.15 C.2 R.R.1/2004'" _
                        & "             WHEN rapporti_utenza.DEST_USO = 'S'" _
                        & "             THEN" _
                        & "                '431/98 Speciali'" _
                        & "             WHEN rapporti_utenza.DEST_USO = '0'" _
                        & "             THEN" _
                        & "                'Standard'" _
                        & "          END)" _
                        & "            AS tipo_ru_specifico," _
                        & "         BOL_BOLLETTE_GEST.id id_gestionale," _
                        & "         BOL_BOLLETTE_GEST.id_tipo," _
                        & "         tipo_bollette_gest.descrizione tipo_gestionale," _
                        & "         tipo_bollette_gest.acronimo," _
                        & "         siscom_mi.getdata (BOL_BOLLETTE_GEST.riferimento_da) riferimento_da," _
                        & "         siscom_mi.getdata (BOL_BOLLETTE_GEST.riferimento_a) riferimento_a," _
                        & "        to_date (BOL_BOLLETTE_GEST.data_emissione,'yyyyMMdd') data_emissione," _
                        & "         BOL_BOLLETTE_GEST.importo_totale," _
                        & "         (CASE" _
                        & "             WHEN BOL_BOLLETTE_GEST.importo_totale < 0 THEN 'CREDITO'" _
                        & "             WHEN BOL_BOLLETTE_GEST.importo_totale > 0 THEN 'DEBITO'" _
                        & "             ELSE 'ZERO'" _
                        & "          END)" _
                        & "            tipo_importo," _
                        & "         DECODE (BOL_BOLLETTE_GEST.tipo_applicazione," _
                        & "                 'T', 'SI'," _
                        & "                 'N', 'NO'," _
                        & "                 'P', 'PARZIALE'," _
                        & "                 'Non definito')" _
                        & "            Lavorato," _
                        & "         siscom_mi.getdata (BOL_BOLLETTE_GEST.data_applicazione) data_lavorazione," _
                        & "         DECODE (BOL_BOLLETTE_GEST.fl_sbloccata, 0, 'ROSSO', 'VERDE') semaforo," _
                        & "       (case when  (SELECT COUNT (id_contratto)" _
                        & "            FROM siscom_mi.SPALM_KPI1" _
                        & "           WHERE id_contratto = RAPPORTI_UTENZA.id)>0 then 'SI' else 'NO' end)" _
                        & "            AS KP1," _
                        & "         (CASE WHEN cod_tipo_disponibilita = 'VEND' THEN 'SI' ELSE 'NO' END)" _
                        & "            AS UI_VENDUTA," _
                        & "        (case when (SELECT COUNT (id_contratto)" _
                        & "            FROM siscom_mi.SPALM_ART_15" _
                        & "           WHERE id_contratto = RAPPORTI_UTENZA.id)>0 then 'SI' else 'NO' end)" _
                        & "            AS ART_15_C2_BIS," _
                        & "        (case when ( SELECT COUNT (id_contratto)" _
                        & "            FROM siscom_mi.bol_rateizzazioni" _
                        & "           WHERE id_contratto = RAPPORTI_UTENZA.id AND fl_annullata = 0)>0 then 'SI' else 'NO' end)" _
                        & "            AS RU_CON_RAT," _
                        & "         BOL_BOLLETTE_GEST.note" _
                        & "    FROM SISCOM_MI.BOL_BOLLETTE_GEST ," _
                        & "         SISCOM_MI.RAPPORTI_UTENZA ," _
                        & "         SISCOM_MI.UNITA_CONTRATTUALE ," _
                        & "         siscom_mi.tipologia_contratto_locazione," _
                        & "         siscom_mi.tipologia_unita_immobiliari," _
                        & "         siscom_mi.tipo_bollette_gest ," _
                        & "         SISCOM_MI.unita_immobiliari ," _
                        & "         siscom_mi.soggetti_contrattuali," _
                        & "         siscom_mi.anagrafica," _
                        & "         siscom_mi.indirizzi " _
                        & "WHERE BOL_BOLLETTE_GEST.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND FL_VISUALIZZABILE=1 AND RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO and UNITA_CONTRATTUALE.id_unita=UNITA_IMMOBILIARI.ID and UNITA_CONTRATTUALE.id_unita_principale is null " _
                        & "AND tipologia_contratto_locazione.cod = rapporti_utenza.cod_tipologia_contr_loc and bol_bollette_gest.id_tipo=tipo_bollette_gest.id and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_contratto=rapporti_utenza.id " _
                        & "AND unita_immobiliari.id_indirizzo = indirizzi.ID (+) and bol_Bollette_gest.tipo_applicazione='N' and COD_TIPOLOGIA_OCCUPANTE='INTE' AND unita_immobiliari.cod_tipologia = tipologia_unita_immobiliari.cod " _
                        & condizioniWhere & " order by data_emissione asc,bol_bollette_gest.id asc"


    End Function

    Public Property queryXLS() As String
        Get
            If Not (ViewState("par_queryXLS") Is Nothing) Then
                Return CStr(ViewState("par_queryXLS"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_queryXLS") = value
        End Set

    End Property
    Private Sub btnAvviaRicerca_Click(sender As Object, e As EventArgs) Handles btnAvviaRicerca.Click
        Try
            EsportaQuery()
            dgvElaborazioni.Rebind()
            HFMultiView1.Value = 1
            HFMultiView2.Value = 1
            lblTitolo.Text = "Risultati Ricerca"
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub
    Private Sub dgvElaborazioni_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvElaborazioni.NeedDataSource
        Try
            connData.apri(False)
            'EsportaQuery()
            Dim DT As Data.DataTable = Session.Item("DT_GEST")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub
    Private Sub btnIndietro_Click(sender As Object, e As EventArgs) Handles btnIndietro.Click
        Try
            Session.Remove("DT_GEST")
            HFMultiView1.Value = 0
            HFMultiView2.Value = 0
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub

    Protected Sub CheckBoxEccezioni_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEccezioni.CheckedChanged
        Try
            If CheckBoxEccezioni.Checked = True Then
                For Each Items As ListItem In CheckBoxListEccez.Items
                    Items.Selected = True
                Next
            Else
                For Each Items As ListItem In CheckBoxListEccez.Items
                    Items.Selected = False
                Next
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
    Protected Sub CheckBoxTipoDoc_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTipoDoc.CheckedChanged
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

    Private Sub CheckBoxListTipologiaRapporti_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckBoxListTipologiaRapporti.SelectedIndexChanged
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

            caricaContrattoSpecifico()

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Private Sub btnNuovaRicerca_Click(sender As Object, e As EventArgs) Handles btnNuovaRicerca.Click
        Session.Remove("DT_GEST")
        Response.Redirect("RicercaGestDaElaborare.aspx", False)
    End Sub

    Private Sub btnProcedi_Click(sender As Object, e As EventArgs) Handles btnProcedi.Click
        Try
            Dim DT As Data.DataTable = Session.Item("DT_GEST")
            Dim continua As Boolean = False
            connData.apri(True)
            If DT.Rows.Count > 0 Then
                par.cmd.CommandText = "delete from SISCOM_MI.ELENCO_GESTIONALI_SPALMATORE"
                par.cmd.ExecuteNonQuery()
                For Each riga As Data.DataRow In DT.Rows
                    If riga("CHECKALL") = True Then
                        continua = True
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_GESTIONALI_SPALMATORE (" _
                        & " ID_BOLLETTA_GEST, ID_CONTRATTO) " _
                        & "VALUES ( " & riga.Item("id_boll") & "," _
                        & riga.Item("id") & ")"
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            End If
            If continua Then
                Session.Remove("DT_GEST")
                Dim idElaborazione As String = ""
                Dim totaliElaborati As Integer = 0
                par.cmd.CommandText = "select count(id) as id_elab from SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST where esito=0"
                totaliElaborati = par.IfNull(par.cmd.ExecuteScalar, 0)
                If totaliElaborati = 0 Then
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ELABORAZ_SCRITTURE_GEST.NEXTVAL FROM DUAL"
                    idElaborazione = par.IfNull(par.cmd.ExecuteScalar, "")
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST (ID, NOME_OPERATORE, DATA_ORA_INIZIO, DATA_ORA_FINE, ESITO, ERRORE, " _
                                        & "PARZIALE, TOTALE, PARAMETRI, TIPO, DATA_SCADENZA_BOLL) VALUES " _
                                        & "(" & idElaborazione & ", '" & Session.Item("OPERATORE").ToString & "', '" & Format(Now, "yyyyMMddHHmmss") & "', '', 0, '', " _
                                        & "0, 100, " & idElaborazione & ", " & cmbTipoImporto.SelectedValue & "," & par.insDbValue(txtDataScad.SelectedDate.Value.ToString("yyyyMMdd"), True) & ")"
                    par.cmd.ExecuteNonQuery()
                Else
                    connData.chiudi(False)
                    dgvElaborazioni.Rebind()
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("E\' stata già avviata una procedura. Attendere il termine.", 300, 150, "Attenzione", Nothing, Nothing)
                    Exit Sub
                End If

                connData.chiudi(True)
                Try
                    Dim p As New System.Diagnostics.Process
                    Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
                    Dim dicParaConnection As New Generic.Dictionary(Of String, String)
                    Dim sParametri As String = ""
                    For i As Integer = 0 To elParameter.Length - 1
                        Dim s As String() = elParameter(i).Split("=")
                        If s.Length > 1 Then
                            dicParaConnection.Add(s(0), s(1))
                        End If
                    Next
                    sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idElaborazione & "#" & Session.Item("id_operatore")
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/SPALMATORE.exe")
                    p.StartInfo.Arguments = sParametri
                    p.Start()
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Procedura avviata correttamente!", 300, 150, "Info", "apriMaschera", Nothing)

                    'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Procedura avviata correttamente!');", True)
                    ' Response.Redirect("Procedure.aspx", False)
                Catch ex As Exception
                    connData.apri(False)
                    par.cmd.CommandText = ""
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(False)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST SET ESITO = 2, DATA_ORA_FINE = '" & Format(Now, "yyyyMMddHHmmss") & "', ERRORE = 'Procedura non Avviata' WHERE ID = " & idElaborazione
                    par.cmd.ExecuteNonQuery()
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore durante l\'avvio della procedura!", 300, 150, "Attenzione", Nothing, Nothing)

                End Try
            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Impossibile procedere. Selezionare almeno un elemento!", 300, 150, "Attenzione", Nothing, Nothing)

            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub dgvElaborazioni_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles dgvElaborazioni.PageIndexChanged
        CheckBox()
        dgvElaborazioni.CurrentPageIndex = e.NewPageIndex
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        Try
            connData.apri()
            par.cmd.CommandText = queryXLS
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtRecords As New Data.DataTable()
            da.Fill(dtRecords)
            da.Dispose()
            connData.chiudi()
            Esporta(dtRecords)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
End Class