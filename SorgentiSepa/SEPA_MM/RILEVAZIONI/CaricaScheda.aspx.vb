Imports System.IO
Imports OfficeOpenXml

Partial Class RILEVAZIONI_CaricaScheda
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim idCafOp As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_CAR") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
                Exit Sub
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                If controlloTipoOperatore(idCafOp) <> 1 And idCafOp <> 6 And Session.Item("id_operatore") <> 1 Then
                    superUtente.Value = "1"
                    par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
                    par.caricaComboBox("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and RILIEVO_UI.id_lotto in (select id from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) order by edifici.denominazione", cmbEdificio, "ID", "DENOMINAZIONE", True, "-1", "- - -")
                    par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto in (select id from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")
                Else
                    par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
                    par.caricaComboBox("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and RILIEVO_UI.id_lotto IS NOT NULL order by edifici.denominazione", cmbEdificio, "ID", "DENOMINAZIONE", True, "-1", "- - -")
                    par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto IS NOT NULL ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")
                End If
                CaricaDati()
                CaricaUnita()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - ScaricaSchede - " & ex.Message)
            txtRisultati.Text = "Provenienza: Gestione Rilevazioni - ScaricaSchede - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)

        End Try
    End Sub

    Private Sub CaricaDati()
        Try
            connData.apri()

            Dim Str As String = ""
            If superUtente.Value = "1" Then
                Str = "select rilievo_tab_utenti.id_rilievo, RILIEVO_TAB_UTENTI.DESCRIZIONE AS UTENTE,RILIEVO_LOTTI.* from SISCOM_MI.RILIEVO_TAB_UTENTI,SISCOM_MI.RILIEVO_LOTTI,siscom_mi.rilievo_utenti_operatori where RILIEVO_TAB_UTENTI.ID=RILIEVO_LOTTI.ID_UTENTE AND  rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente"
            Else
                Str = "select rilievo_tab_utenti.id_rilievo, RILIEVO_TAB_UTENTI.DESCRIZIONE AS UTENTE,RILIEVO_LOTTI.* from SISCOM_MI.RILIEVO_TAB_UTENTI,SISCOM_MI.RILIEVO_LOTTI where RILIEVO_TAB_UTENTI.ID=RILIEVO_LOTTI.ID_UTENTE AND id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)"
            End If
            par.cmd.CommandText = Str
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                IDLotto.Value = par.IfNull(myReader("DESCRIZIONE"), "")
                IDUtente.Value = par.IfNull(myReader("UTENTE"), -1)
                IDRilievo.Value = par.IfNull(myReader("ID_RILIEVO"), -1)
            End If
            myReader.Close()
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaScheda - Carica Dati - " & ex.Message)
            txtRisultati.Text = "Provenienza: CaricaScheda - Carica Dati - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Sub CaricaUnita(Optional ByVal condizioneLotto As String = "", Optional ByVal condizioneEdificio As String = "", Optional ByVal condizioneIndirizzo As String = "")
        Try
            Dim condSuOperatori As String = ""
            Dim fromOperatori As String = ""
            If controlloTipoOperatore(idCafOp) <> 1 And idCafOp <> 6 And Session.Item("id_operatore") <> 1 Then
                fromOperatori = " siscom_mi.RILIEVO_UTENTI_OPERATORI,SISCOM_MI.RILIEVO_LOTTI,SISCOM_MI.rilievo_tab_utenti, "
                condSuOperatori = "  and rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and RILIEVO_UI.id_lotto=RILIEVO_LOTTI.id AND RILIEVO_LOTTI.id_utente=rilievo_tab_utenti.id AND rilievo_tab_utenti.id = rilievo_utenti_operatori.id_utente"
            End If
            Dim Str As String = "SELECT (select rilievo_lotti.descrizione from siscom_mi.rilievo_lotti where rilievo_lotti.id=rilievo_ui.id_lotto) as lotto," _
                            & "UNITA_IMMOBILIARI.ID,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE,EDIFICI.denominazione AS edificio," _
                            & "replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£document.getElementById(''CPContenuto_IDui'').value='||UNITA_IMMOBILIARI.ID||';document.getElementById(''CPContenuto_btnUpload1'').click();£><img src=£../Standard/Immagini/upload_16.png£ alt=£Upload£ border=£0£/></a>','$','&'),'£','" & Chr(34) & "') as UPLOAD_SCHEDA,INDIRIZZI.descrizione AS indirizzo,INDIRIZZI.civico,'' as UPLOAD_SCHEDA," _
                            & "UNITA_IMMOBILIARI.interno,SCALE_EDIFICI.descrizione AS scala,TIPO_LIVELLO_PIANO.descrizione AS piano," _
                            & "INDIRIZZI.cap,INDIRIZZI.localita " _
                            & " FROM " _
                            & "siscom_mi.UNITA_IMMOBILIARI," & fromOperatori & " siscom_mi.RILIEVO_UI, siscom_mi.EDIFICI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI, siscom_mi.TIPO_LIVELLO_PIANO " _
                            & " WHERE " _
                            & "UNITA_IMMOBILIARI.ID = RILIEVO_UI.id_unita " _
                            & " AND EDIFICI.ID (+)=UNITA_IMMOBILIARI.id_edificio " _
                            & " AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.id_indirizzo " _
                            & " AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.id_scala " _
                            & " AND TIPO_LIVELLO_PIANO.cod (+)=UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
                            & " And RILIEVO_UI.id_lotto Is not NULL AND RILIEVO_UI.ID_RILIEVO=" & IDRilievo.Value _
                            & condizioneLotto & condizioneEdificio & condizioneIndirizzo & condSuOperatori _
                            & " ORDER BY edificio ASC,indirizzo ASC,civico,interno"
            connData.apri()

            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridUIDisponibili.DataSource = dt
            DataGridUIDisponibili.DataBind()
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaScheda - Carica Unità - " & ex.Message)
            txtRisultati.Text = "Provenienza: CaricaScheda - Carica Unità - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Function CaricaPerLotto() As String
        Dim condizioneLotto As String = ""
        If cmbLotto.SelectedValue <> "-1" Then
            condizioneLotto = " AND RILIEVO_UI.id_lotto = " & cmbLotto.SelectedValue
            par.caricaComboBox("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and RILIEVO_UI.id_lotto IS NOT NULL order by edifici.denominazione", cmbEdificio, "ID", "DENOMINAZIONE", True, "-1", "- - -")
            par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto IS NOT NULL ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")
        End If

        Return condizioneLotto
    End Function

    Private Function CaricaPerEdificio() As String
        Dim condizioneEdificio As String = ""
        If cmbEdificio.SelectedValue <> "-1" Then
            condizioneEdificio = " AND EDIFICI.ID = " & cmbEdificio.SelectedValue
            par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto IS NOT NULL ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")
            If superUtente.Value = "1" Then
                par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
            Else
                par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
            End If
        End If
        Return condizioneEdificio
    End Function

    Private Function CaricaPerIndirizzo() As String
        Dim condizioneIndirizzo As String = ""
        If cmbIndirizzo.SelectedValue <> "-1" Then
            condizioneIndirizzo = " AND INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO = '" & par.PulisciStrSql(cmbIndirizzo.SelectedValue) & "'"
            par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto IS NOT NULL ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")
            If superUtente.Value = "1" Then
                par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
            Else
                par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
            End If
        End If
        Return condizioneIndirizzo
    End Function

    Private Function controlloTipoOperatore(ByRef idCaf As Integer) As Integer
        Dim livelloWeb As Integer = 0
        Try
            connData.apri()

            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                livelloWeb = par.IfNull(MyReader("livello_web"), 0)
                idCaf = par.IfNull(MyReader("id_caf"), 0)
            End If
            MyReader.Close()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: CaricaScheda - controlloTipoOperatore - " & ex.Message)
            txtRisultati.Text = "Provenienza: CaricaScheda - controlloTipoOperatore - " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)

        End Try
        Return livelloWeb
    End Function

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub cmbLotto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbLotto.SelectedIndexChanged
        Dim condizioneLotto As String = ""
        condizioneLotto = CaricaPerLotto()
        CaricaUnita(condizioneLotto, "", "")
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Dim condizioneEdificio As String = ""
        condizioneEdificio = CaricaPerEdificio()
        CaricaUnita("", condizioneEdificio, "")
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Dim condizioneIndirizzo As String = ""
        condizioneIndirizzo = CaricaPerIndirizzo()
        CaricaUnita("", "", condizioneIndirizzo)
    End Sub

    Protected Sub btnUpload1_Click(sender As Object, e As System.EventArgs) Handles btnUpload1.Click
        If controlloTipoOperatore(idCafOp) <> 0 Or idCafOp <> 6 Then
            txtRisultati.Visible = False
            lblFileDaCaric.Text = "Selezionare il file excel da caricare tramite il pulsante ""Sfoglia"""
            uploadMassivo.Value = "0"
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
        Else
            par.modalDialogMessage("Attenzione", "Utente non abilitato!", Me.Page)
        End If
    End Sub

    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnAllega.Click
        Try
            connData.apri(True)

            Dim FileName As String = UploadOnServer()
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If uploadMassivo.Value = "0" Then
                If Not String.IsNullOrEmpty(FileName) Then
                    If objFile.FileExists(FileName) And FileName.Contains(".xlsx") Then
                        LeggiFile(FileName)
                        par.modalDialogMessage("Info", "Operazione completata!", Me.Page)
                    Else
                        par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare un file .xlsx", Me.Page)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
                    End If
                End If
            Else
                If Not String.IsNullOrEmpty(FileName) Then
                    If objFile.FileExists(FileName) And FileName.Contains(".zip") Then
                        If LeggiFileMulti(FileName) = True Then
                            par.modalDialogMessage("Info", "Operazione completata!", Me.Page)
                        Else
                            par.modalDialogMessage("Attenzione", "Operazione interrotta!", Me.Page)
                        End If
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
                    Else
                        par.modalDialogMessage("Attenzione", "Tipo file non valido. Selezionare una cartella zip", Me.Page)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
                    End If
                End If
            End If

            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: CaricaScheda - btnAllega_Click - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
            txtRisultati.Text = "Errore dati scheda UI:" & codUI
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try
    End Sub

    Private Function LeggiFileMulti(ByVal nomeCartella As String) As Boolean
        Dim ElencoFileMassivo() As String = Nothing
        Dim fileExcel As String = ""
        Dim i As Integer = 0
        Dim i2 As Integer = 0
        Dim scartati As String = ""
        par.EstraiZipFile(nomeCartella, Server.MapPath("~\FileTemp\"), "")

        i = 0
        NumScartati = 0
        NumInserite = 0
        motivoScarto = ""
        If My.Computer.FileSystem.DirectoryExists(Server.MapPath("../FileTemp/" & Left(FileUpload1.FileName, Len(FileUpload1.FileName) - 4))) = True Then
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../FileTemp/" & Left(FileUpload1.FileName, Len(FileUpload1.FileName) - 4)), FileIO.SearchOption.SearchTopLevelOnly, "*.xls")
                ReDim Preserve ElencoFileMassivo(i)
                ElencoFileMassivo(i) = foundFile
                LeggiFile(foundFile)
                i = i + 1
                uploadMassivo.Value = "1"
            Next
            If NumScartati <> 0 Then
                scartati = vbCrLf & "Sono stati SCARTATI i dati relativi a " & NumScartati & " unità poichè non presenti in alcuna delle consistenze caricate in SEPA." & vbCrLf & vbCrLf & "Elenco cod. UI scartate:" & motivoScarto
            End If
            txtRisultati.Text = txtRisultati.Text & "Sono stati IMPORTATI i dati relativi a " & NumInserite & " unità su " & i & " totali." & scartati
            LeggiFileMulti = True
        Else
            txtRisultati.Text = "ERRORE! Contenuto cartella non valido."
            LeggiFileMulti = False
        End If


        File.Delete(nomeCartella)
    End Function
    Public Property NumScartati() As Integer
        Get
            If Not (ViewState("par_NumScartati") Is Nothing) Then
                Return CInt(ViewState("par_NumScartati"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NumScartati") = value
        End Set

    End Property

    Public Property NumInserite() As Integer
        Get
            If Not (ViewState("par_NumInserite") Is Nothing) Then
                Return CInt(ViewState("par_NumInserite"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NumInserite") = value
        End Set

    End Property

    Public Property motivoScarto() As String
        Get
            If Not (ViewState("par_motivoScarto") Is Nothing) Then
                Return CStr(ViewState("par_motivoScarto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_motivoScarto") = value
        End Set

    End Property

    Public Property codUI() As String
        Get
            If Not (ViewState("par_codUI") Is Nothing) Then
                Return CStr(ViewState("par_codUI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codUI") = value
        End Set

    End Property

    Private Sub LeggiFile(ByVal nomeFile As String)
        Dim newFile As New FileInfo(nomeFile)
        Dim pck As New ExcelPackage(newFile)
        'Dim codUI As String = ""
        Dim idRilievoAll As String = ""
        Dim ws = pck.Workbook.Worksheets("SCHEDA MANUT ALL SFITTI")
        Dim row As Data.DataRow
        Dim uiValida As Boolean = False
        Dim dtDaFile As New Data.DataTable
        dtDaFile = CreaDTAllSfitto()



        row = dtDaFile.NewRow()
        codUI = par.IfEmpty(ws.Cells("AJ3").Value, 0)
        par.cmd.CommandText = "select unita_immobiliari.id from siscom_mi.unita_immobiliari,SISCOM_MI.RILIEVO_UI where unita_immobiliari.id=RILIEVO_UI.id_unita and id_lotto is not null and RILIEVO_UI.ID_RILIEVO=(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1) and cod_unita_immobiliare='" & codUI & "'"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            uiValida = True
            row.Item("ID_UNITA_IMMOBILIARE") = myReader("id")
            NumInserite = NumInserite + 1
        Else
            row.Item("ID_UNITA_IMMOBILIARE") = 0
            NumScartati = NumScartati + 1
            motivoScarto = motivoScarto & vbCrLf & codUI
        End If
        myReader.Close()


        If (par.IfEmpty(IDui.Value, 0) = row.Item("ID_UNITA_IMMOBILIARE") Or uploadMassivo.Value = "1") And uiValida = True Then

            par.cmd.CommandText = "select siscom_mi.SEQ_RILIEVO_ALLOGGIO_SFITTO.nextval from dual"
            idRilievoAll = par.cmd.ExecuteScalar
            row.Item("ID") = idRilievoAll
            row.Item("DATA_RILIEVO") = par.IfEmpty(ws.Cells("B248").Value, "")
            row.Item("DATA_SOPRALLUOGO") = par.IfEmpty(ws.Cells("AJ11").Value, "")
            row.Item("DATA_CARICAMENTO") = par.IfEmpty(ws.Cells("AV11").Value, "")
            If par.IfEmpty(ws.Cells("AV11").Value, "") = "" Then
                row.Item("DATA_CARICAMENTO") = Format(Now, "dd/MM/yyyy")
            End If
            row.Item("DATA_ULTIMO_RILIEVO") = par.IfEmpty(ws.Cells("BF11").Value, "")
            If par.IfEmpty(ws.Cells("H211").Value, "") <> "" Then
                row.Item("ASCENSORE") = 1
            Else
                row.Item("ASCENSORE") = 0
            End If
            If par.IfEmpty(ws.Cells("T214").Value, "") <> "" Then
                row.Item("SCIVOLI") = 1
            Else
                row.Item("SCIVOLI") = 0
            End If
            If par.IfEmpty(ws.Cells("T217").Value, "") <> "" Then
                row.Item("MONTASCALE") = 1
            Else
                row.Item("MONTASCALE") = 0
            End If
            If par.IfEmpty(ws.Cells("T222").Value, "") <> "" Then
                row.Item("FORO_AREAZIONE_100") = 1
            Else
                row.Item("FORO_AREAZIONE_100") = 0
            End If
            row.Item("NOME_LOCALE_FORO_AREAZ_100") = par.IfEmpty(ws.Cells("T224").Value, "")
            If par.IfEmpty(ws.Cells("T227").Value, "") <> "" Then
                row.Item("FORO_AREAZIONE_200") = 1
            Else
                row.Item("FORO_AREAZIONE_200") = 0
            End If
            row.Item("NOME_LOCALE_FORO_AREAZ_200") = par.IfEmpty(ws.Cells("T229").Value, "")
            If ws.Cells("T231").Value <> "" Then
                row.Item("COD_STATO_CONSERV") = "BUONO"
            ElseIf ws.Cells("T233").Value <> "" Then
                row.Item("COD_STATO_CONSERV") = "MEDIOCRE"
            ElseIf ws.Cells("T235").Value <> "" Then
                row.Item("COD_STATO_CONSERV") = "SCADENTE"
            Else
                row.Item("COD_STATO_CONSERV") = ""
            End If
            If ws.Cells("AL231").Value <> "" Then
                row.Item("LIVELLO") = "BASSO"
            ElseIf ws.Cells("AL233").Value <> "" Then
                row.Item("LIVELLO") = "MEDIO"
            ElseIf ws.Cells("AL235").Value <> "" Then
                row.Item("LIVELLO") = "ALTO"
            Else
                row.Item("LIVELLO") = ""
            End If
            If ws.Cells("BF231").Value <> "" Then
                row.Item("AGIBILE") = 1
            ElseIf ws.Cells("BF233").Value <> "" Then
                row.Item("AGIBILE") = 0
            Else
                row.Item("AGIBILE") = 0
            End If
            If par.IfEmpty(ws.Cells("T219").Value, "") <> "" Then
                row.Item("ADEGUATO_DISABILI") = 1
            Else
                row.Item("ADEGUATO_DISABILI") = 0
            End If
            If par.IfEmpty(ws.Cells("V211").Value, "") <> "" Then
                row.Item("ADEGUATO_ASCENSORE") = 1
            Else
                row.Item("ADEGUATO_ASCENSORE") = 0
            End If
            If par.IfEmpty(ws.Cells("J238").Value, "") <> "" Then
                row.Item("NON_VISITAB_LASTRATURA") = 1
            Else
                row.Item("NON_VISITAB_LASTRATURA") = 0
            End If
            If par.IfEmpty(ws.Cells("R238").Value, "") <> "" Then
                row.Item("NON_VISITAB_OCCUPATO") = 1
            Else
                row.Item("NON_VISITAB_OCCUPATO") = 0
            End If
            If par.IfEmpty(ws.Cells("X238").Value, "") <> "" Then
                row.Item("NON_VISITAB_MURATO") = 1
            Else
                row.Item("NON_VISITAB_MURATO") = 0
            End If
            If par.IfEmpty(ws.Cells("AB238").Value, "") <> "" Then
                row.Item("NON_VISITAB_NO_CHIAVI") = 1
            Else
                row.Item("NON_VISITAB_NO_CHIAVI") = 0
            End If
            If par.IfEmpty(ws.Cells("AF219").Value, "") <> "" Then
                row.Item("ADATTABILE_DISABILI") = 1
            Else
                row.Item("ADATTABILE_DISABILI") = 0
            End If
            If par.IfEmpty(ws.Cells("N58").Value, "") <> "" Then
                row.Item("PERTINENZA_PORTA") = 1
            Else
                row.Item("PERTINENZA_PORTA") = 0
            End If
            If par.IfEmpty(ws.Cells("N59").Value, "") <> "" Then
                row.Item("PERTINENZA_INFILTRAZIONI") = 1
            Else
                row.Item("PERTINENZA_INFILTRAZIONI") = 0
            End If
            'If par.IfEmpty(ws.Cells("T179").Value, "") <> "" Then
            row.Item("LOCALE_PAVIMENTI") = par.IfEmpty(ws.Cells("T179").Value, "")
            'Else
            '    row.Item("LOCALE_PAVIMENTI") = 0
            'End If
            row.Item("NOTE") = par.IfEmpty(ws.Cells("H203").Value, "")
            row.Item("VARIE") = par.IfEmpty(ws.Cells("T198").Value, "")
            row.Item("NOME_TECNICO") = par.IfEmpty(ws.Cells("H248").Value, "")
            row.Item("NOME_RESPONSABILE") = par.IfEmpty(ws.Cells("AT248").Value, "")

            dtDaFile.Rows.Add(row)

            'par.cmd.CommandText = "DELETE FROM SISCOM_MI.RILIEVO_ALLOGGIO_SFITTO WHERE ID_UNITA_IMMOBILIARE=" & row.Item("ID_UNITA_IMMOBILIARE")
            'par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = " INSERT INTO SISCOM_MI.RILIEVO_ALLOGGIO_SFITTO (" _
                & " ID, ID_UNITA_IMMOBILIARE, DATA_RILIEVO,DATA_CARICAMENTO,DATA_ULTIMO_RILIEVO, " _
                & " ASCENSORE, SCIVOLI, " _
                & " MONTASCALE, FORO_AREAZIONE_100, " _
                & " NOME_LOCALE_FORO_AREAZ_100, FORO_AREAZIONE_200, NOME_LOCALE_FORO_AREAZ_200, " _
                & " COD_STATO_CONSERV, LIVELLO, AGIBILE, " _
                & " ADEGUATO_DISABILI, ADEGUATO_ASCENSORE, NON_VISITAB_LASTRATURA, " _
                & " NON_VISITAB_OCCUPATO,NOTE,ADATTABILE_DISABILI,PERTINENZA_PORTA,PERTINENZA_INFILTRAZIONI," _
                & "LOCALE_PAVIMENTI,VARIE,NOME_TECNICO,NOME_RESPONSABILE,ID_OPERATORE_INSERIMENTO,DATA_SOPRALLUOGO,NON_VISITAB_MURATO,NON_VISITAB_NO_CHIAVI) " _
                & " VALUES ( " & idRilievoAll & "," _
                & " " & row.Item("ID_UNITA_IMMOBILIARE") & "," _
                & " " & par.insDbValue(row.Item("DATA_RILIEVO"), True, True) & "," _
                & " " & par.insDbValue(row.Item("DATA_CARICAMENTO"), True, True) & "," _
                & " " & par.insDbValue(row.Item("DATA_ULTIMO_RILIEVO"), True, True) & "," _
                & " " & par.insDbValue(row.Item("ASCENSORE"), False) & "," _
                & " " & par.insDbValue(row.Item("SCIVOLI"), False) & "," _
                & " " & par.insDbValue(row.Item("MONTASCALE"), False) & "," _
                & " " & par.insDbValue(row.Item("FORO_AREAZIONE_100"), False) & "," _
                & " " & par.insDbValue(row.Item("NOME_LOCALE_FORO_AREAZ_100"), True) & "," _
                & " " & par.insDbValue(row.Item("FORO_AREAZIONE_200"), False) & "," _
                & " " & par.insDbValue(row.Item("NOME_LOCALE_FORO_AREAZ_200"), True) & "," _
                & " " & par.insDbValue(row.Item("COD_STATO_CONSERV"), True) & "," _
                & " " & par.insDbValue(row.Item("LIVELLO"), True) & "," _
                & " " & par.insDbValue(row.Item("AGIBILE"), False) & "," _
                & " " & par.insDbValue(row.Item("ADEGUATO_DISABILI"), False) & "," _
                & " " & par.insDbValue(row.Item("ADEGUATO_ASCENSORE"), False) & "," _
                & " " & par.insDbValue(row.Item("NON_VISITAB_LASTRATURA"), False) & "," _
                & " " & par.insDbValue(row.Item("NON_VISITAB_OCCUPATO"), False) & "," _
                & " " & par.insDbValue(row.Item("NOTE"), True) & "," _
                & " " & par.insDbValue(row.Item("ADATTABILE_DISABILI"), False) & "," _
                & " " & par.insDbValue(row.Item("PERTINENZA_PORTA"), False) & "," _
                & " " & par.insDbValue(row.Item("PERTINENZA_INFILTRAZIONI"), False) & "," _
                & " " & par.insDbValue(row.Item("LOCALE_PAVIMENTI"), True) & "," _
                & " " & par.insDbValue(row.Item("VARIE"), True) & "," _
                & " " & par.insDbValue(row.Item("NOME_TECNICO"), True) & "," _
                & " " & par.insDbValue(row.Item("NOME_RESPONSABILE"), True) & "," _
                & " " & Session.Item("ID_OPERATORE") & "," _
                & " " & par.insDbValue(row.Item("DATA_SOPRALLUOGO"), True, True) & "," _
                & " " & par.insDbValue(row.Item("NON_VISITAB_MURATO"), False) & "," _
                & " " & par.insDbValue(row.Item("NON_VISITAB_NO_CHIAVI"), False) & ")"
            par.cmd.ExecuteNonQuery()

            Dim dtDaFile2 As New Data.DataTable
            dtDaFile2 = CreaDTSchedaManut()

            par.cmd.CommandText = "SELECT id from siscom_mi.RILIEVO_DESC_STATI_MANUT"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da.Fill(dt0)

            Dim row2 As Data.DataRow

            If dt0.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dt0.Rows
                    row2 = dtDaFile2.NewRow()
                    row2.Item("ID_ALLOGGIO_SFITTO") = idRilievoAll
                    par.cmd.CommandText = "select siscom_mi.SEQ_RILIEVO_SCHEDA_MANUTENTIVA.nextval from dual"
                    row2.Item("ID") = par.cmd.ExecuteScalar
                    row2.Item("ID_DESC_ST_MANUT") = r1.Item("ID")
                    dtDaFile2.Rows.Add(row2)
                Next
            End If

            If dtDaFile2.Rows.Count > 0 Then
                For Each rIns As Data.DataRow In dtDaFile2.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RILIEVO_SCHEDA_MANUTENTIVA (" _
                        & "ID, ID_ALLOGGIO_SFITTO, ID_DESC_ST_MANUT)" _
                        & "VALUES ( " & rIns.Item("ID") & "," _
                        & rIns.Item("ID_ALLOGGIO_SFITTO") & "," _
                        & rIns.Item("ID_DESC_ST_MANUT") & ")"
                    par.cmd.ExecuteNonQuery()
                Next
            End If

            'Inserisci dettagli scheda
            Dim dtA As New Data.DataTable
            dtA = InsertDettagliSchedaManut(idRilievoAll, newFile)
            If dtA.Rows.Count > 0 Then
                For Each r1 As Data.DataRow In dtA.Rows
                    par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_SCHEDA_MANUTENTIVA SET " _
                        & " ID_DESC_ST_MANUT = " & r1.Item("ID_DESC_ST_MANUT") & "," _
                        & " CHK_1= " & r1.Item("CHK_1") & "," _
                        & " CHK_2= " & r1.Item("CHK_2") & "," _
                        & " CHK_3= " & r1.Item("CHK_3") & "," _
                        & " CHK_4= " & r1.Item("CHK_4") & "," _
                        & " CHK_5= " & r1.Item("CHK_5") & "," _
                        & " NUMERO= '" & par.IfNull(r1.Item("NUMERO"), "") & "'," _
                        & " VANO= '" & par.IfNull(r1.Item("VANO"), "") & "'" _
                        & " WHERE  ID  = " & r1.Item("ID") & " and ID_ALLOGGIO_SFITTO= " & r1.Item("ID_ALLOGGIO_SFITTO")
                    par.cmd.ExecuteNonQuery()
                Next
            End If
        Else
            If uploadMassivo.Value = "0" Then

                par.modalDialogMessage("Attenzione", "La scheda che si intende caricare si riferisce ad un alloggio diverso da quello selezioanto!", Me.Page)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
            End If
        End If

    End Sub

    Private Function InsertDettagliSchedaManut(ByVal idAllSfitto As Long, ByVal newFile As FileInfo) As Data.DataTable

        Dim chk1 As Integer = 0
        Dim chk2 As Integer = 0
        Dim chk3 As Integer = 0
        Dim chk4 As Integer = 0
        Dim chk5 As Integer = 0

        Dim pck As New ExcelPackage(newFile)
        Dim ws = pck.Workbook.Worksheets("SCHEDA MANUT ALL SFITTI")

        par.cmd.CommandText = "Select * from SISCOM_MI.RILIEVO_SCHEDA_MANUTENTIVA where ID_ALLOGGIO_SFITTO=" & idAllSfitto & " order by ID_DESC_ST_MANUT asc"
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt0 As New Data.DataTable
        da.Fill(dt0)
        If dt0.Rows.Count > 0 Then
            For Each r1 As Data.DataRow In dt0.Rows
                If r1.Item("ID_DESC_ST_MANUT") = 1 Then
                    If par.IfEmpty(ws.Cells("T20").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z20").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF20").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN20").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 2 Then
                    If par.IfEmpty(ws.Cells("T22").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z22").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF22").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN22").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 3 Then
                    If par.IfEmpty(ws.Cells("T24").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z24").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF24").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN24").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 4 Then
                    If par.IfEmpty(ws.Cells("T26").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z26").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF26").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN26").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 5 Then
                    If par.IfEmpty(ws.Cells("T27").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z27").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF27").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN27").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 6 Then
                    If par.IfEmpty(ws.Cells("T29").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If

                    If par.IfEmpty(ws.Cells("AF29").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN29").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 7 Then
                    If par.IfEmpty(ws.Cells("T31").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z31").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF31").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AL31").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AP31").Value, "") <> "" Then
                        r1.Item("CHK_5") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R31").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R31").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 8 Then
                    If par.IfEmpty(ws.Cells("T33").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z33").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF33").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If

                End If
                If r1.Item("ID_DESC_ST_MANUT") = 9 Then
                    If par.IfEmpty(ws.Cells("T36").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z36").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF36").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R36").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R36").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 10 Then
                    If par.IfEmpty(ws.Cells("T38").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z38").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AH38").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN38").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R38").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R38").Value
                    End If
                End If
                'Posizione Contatore
                If r1.Item("ID_DESC_ST_MANUT") = 79 Then
                    If par.IfEmpty(ws.Cells("T39").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z39").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AK39").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AP39").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("Z40").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("Z40").Value
                    End If
                    If par.IfEmpty(CStr(ws.Cells("AN40").Value), "") <> "" Then
                        r1.Item("vano") = ws.Cells("AN40").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 11 Then
                    If par.IfEmpty(ws.Cells("T42").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z42").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF42").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R42").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R42").Value
                    End If
                End If
                'Posizione Contatore
                If r1.Item("ID_DESC_ST_MANUT") = 80 Then
                    If par.IfEmpty(ws.Cells("T48").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z48").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AK48").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AP48").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("Z49").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("Z49").Value
                    End If
                    If par.IfEmpty(CStr(ws.Cells("AN49").Value), "") <> "" Then
                        r1.Item("vano") = ws.Cells("AN49").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 12 Then
                    If par.IfEmpty(ws.Cells("T44").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z44").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF44").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN44").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 13 Then
                    If par.IfEmpty(ws.Cells("T45").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 14 Then
                    If par.IfEmpty(ws.Cells("T47").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 15 Then
                    If par.IfEmpty(ws.Cells("T51").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z51").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 16 Then
                    If par.IfEmpty(ws.Cells("T54").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z54").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AH54").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN54").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 17 Then
                    If par.IfEmpty(ws.Cells("T57").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z57").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 81 Then
                    If par.IfEmpty(ws.Cells("T58").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z58").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF58").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN58").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 18 Then
                    If par.IfEmpty(ws.Cells("T61").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z61").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AH61").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN61").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 19 Then
                    If par.IfEmpty(ws.Cells("T63").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z63").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 20 Then
                    If par.IfEmpty(ws.Cells("T66").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z66").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 21 Then
                    If par.IfEmpty(ws.Cells("T69").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z69").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF69").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN69").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("L69").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("L69").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 22 Then
                    If par.IfEmpty(ws.Cells("T72").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z72").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF72").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN72").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 23 Then
                    If par.IfEmpty(ws.Cells("T74").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z74").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 24 Then
                    If par.IfEmpty(ws.Cells("T77").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z77").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R77").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R77").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 25 Then
                    If par.IfEmpty(ws.Cells("T79").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z79").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R79").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R79").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 26 Then
                    If par.IfEmpty(ws.Cells("T82").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z82").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R82").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R82").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 27 Then
                    If par.IfEmpty(ws.Cells("T87").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z87").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 28 Then
                    If par.IfEmpty(ws.Cells("T90").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z90").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF90").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN90").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R90").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R90").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 29 Then
                    If par.IfEmpty(ws.Cells("T92").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z92").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF92").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN92").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R92").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R92").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 30 Then
                    If par.IfEmpty(ws.Cells("T94").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z94").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF94").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN94").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R94").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R94").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 31 Then
                    If par.IfEmpty(ws.Cells("T96").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z96").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF96").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN96").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R96").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R96").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 32 Then
                    If par.IfEmpty(ws.Cells("T98").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z98").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF98").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN98").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R98").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R98").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 33 Then
                    If par.IfEmpty(ws.Cells("T100").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z100").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF100").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN100").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R100").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R100").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 34 Then
                    If par.IfEmpty(ws.Cells("T102").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z102").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF102").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN102").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R102").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R102").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 35 Then
                    If par.IfEmpty(ws.Cells("T104").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z104").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF104").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN104").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R104").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R104").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 36 Then
                    If par.IfEmpty(ws.Cells("T106").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z106").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF106").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN106").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R106").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R106").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 37 Then
                    If par.IfEmpty(ws.Cells("T109").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z109").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF109").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN109").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R109").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R109").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 38 Then
                    If par.IfEmpty(ws.Cells("T111").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z111").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF111").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN111").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R111").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R111").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 39 Then
                    If par.IfEmpty(ws.Cells("T113").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z113").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF113").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN113").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R113").Value), 0) <> 0 Then
                        r1.Item("numero") = ws.Cells("R113").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 40 Then
                    If par.IfEmpty(ws.Cells("T115").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z115").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF115").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN115").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R115").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R115").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 41 Then
                    If par.IfEmpty(ws.Cells("T117").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z117").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF117").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN117").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R117").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R117").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 42 Then
                    If par.IfEmpty(ws.Cells("T119").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z119").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF119").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN119").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R119").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R119").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 43 Then
                    If par.IfEmpty(ws.Cells("T121").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z121").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF121").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN121").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R121").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R121").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 44 Then
                    If par.IfEmpty(ws.Cells("T123").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z123").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF123").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN123").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R123").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R123").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 45 Then
                    If par.IfEmpty(ws.Cells("T125").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z125").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF125").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN125").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R125").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R125").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 46 Then
                    If par.IfEmpty(ws.Cells("T127").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z127").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF127").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN127").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R127").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R127").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 47 Then
                    If par.IfEmpty(ws.Cells("T128").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z128").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF128").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN128").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R128").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R128").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 48 Then
                    If par.IfEmpty(ws.Cells("T131").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z131").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF131").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN131").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R131").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R131").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 49 Then
                    If par.IfEmpty(ws.Cells("T133").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z133").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF133").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN133").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R133").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R133").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 50 Then
                    If par.IfEmpty(ws.Cells("T135").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z135").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF135").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN135").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R135").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R135").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 51 Then
                    If par.IfEmpty(ws.Cells("T137").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z137").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF137").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN137").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R137").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R137").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 52 Then
                    If par.IfEmpty(ws.Cells("T139").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z139").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF139").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN139").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R139").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R139").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 53 Then
                    If par.IfEmpty(ws.Cells("T141").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z141").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF141").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN141").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R141").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R141").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 54 Then
                    If par.IfEmpty(ws.Cells("T144").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z144").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF144").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN144").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R144").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R144").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 55 Then
                    If par.IfEmpty(ws.Cells("T146").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z146").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF146").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN146").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R146").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R146").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 56 Then
                    If par.IfEmpty(ws.Cells("T148").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z148").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF148").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN148").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R148").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R148").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 57 Then
                    If par.IfEmpty(ws.Cells("T150").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z150").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF150").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN150").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R150").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R150").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 58 Then
                    If par.IfEmpty(ws.Cells("T152").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z152").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF152").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN152").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R152").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R152").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 59 Then
                    If par.IfEmpty(ws.Cells("T154").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z154").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF154").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN154").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R154").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R154").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 60 Then
                    If par.IfEmpty(ws.Cells("T156").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z156").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF156").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN156").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R156").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R156").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 61 Then
                    If par.IfEmpty(ws.Cells("T158").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z158").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF158").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN158").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R158").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R158").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 62 Then
                    If par.IfEmpty(ws.Cells("T160").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z160").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF160").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN156").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R160").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R160").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 63 Then
                    If par.IfEmpty(ws.Cells("T162").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z162").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF162").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN162").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R162").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R162").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 64 Then
                    If par.IfEmpty(ws.Cells("T164").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z164").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF164").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN164").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R164").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R164").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 65 Then
                    If par.IfEmpty(ws.Cells("T167").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z167").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF167").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN167").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R167").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R167").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 66 Then
                    If par.IfEmpty(ws.Cells("T169").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z169").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF169").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN169").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R169").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R169").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 67 Then
                    If par.IfEmpty(ws.Cells("T171").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z171").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF171").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN171").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R171").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R171").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 68 Then
                    If par.IfEmpty(ws.Cells("T173").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z173").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF173").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN173").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R173").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R173").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 69 Then
                    If par.IfEmpty(ws.Cells("T175").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z175").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF175").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN175").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R175").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R175").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 70 Then
                    If par.IfEmpty(ws.Cells("T177").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z177").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF177").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN177").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R177").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R177").Value
                    End If
                End If
                'If r1.Item("ID_DESC_ST_MANUT") = 71 Then
                '    If par.IfEmpty(ws.Cells("T175").Value, "") <> "" Then
                '        r1.Item("CHK_1") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("Z175").Value, "") <> "" Then
                '        r1.Item("CHK_2") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("AF175").Value, "") <> "" Then
                '        r1.Item("CHK_3") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("AN175").Value, "") <> "" Then
                '        r1.Item("CHK_4") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("R175").Value, 0) <> 0 Then
                '        r1.Item("numero") = ws.Cells("R175").Value
                '    End If
                'End If
                If r1.Item("ID_DESC_ST_MANUT") = 72 Then
                    If par.IfEmpty(ws.Cells("T182").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z182").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF182").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN182").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R182").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R182").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 73 Then
                    If par.IfEmpty(ws.Cells("T184").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z184").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 74 Then
                    If par.IfEmpty(ws.Cells("T186").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z186").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF186").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN186").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R186").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R186").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 75 Then
                    If par.IfEmpty(ws.Cells("T188").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z188").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF188").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN188").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R188").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R188").Value
                    End If
                End If
                'If r1.Item("ID_DESC_ST_MANUT") = 76 Then
                '    If par.IfEmpty(ws.Cells("T186").Value, "") <> "" Then
                '        r1.Item("CHK_1") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("Z186").Value, "") <> "" Then
                '        r1.Item("CHK_2") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("AF186").Value, "") <> "" Then
                '        r1.Item("CHK_3") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("AN186").Value, "") <> "" Then
                '        r1.Item("CHK_4") = 1
                '    End If
                '    If par.IfEmpty(ws.Cells("R186").Value, 0) <> 0 Then
                '        r1.Item("numero") = ws.Cells("R186").Value
                '    End If
                'End If
                If r1.Item("ID_DESC_ST_MANUT") = 77 Then
                    If par.IfEmpty(ws.Cells("T192").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z192").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF192").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN192").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R192").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R192").Value
                    End If
                End If
                If r1.Item("ID_DESC_ST_MANUT") = 78 Then
                    If par.IfEmpty(ws.Cells("T194").Value, "") <> "" Then
                        r1.Item("CHK_1") = 1
                    End If
                    If par.IfEmpty(ws.Cells("Z194").Value, "") <> "" Then
                        r1.Item("CHK_2") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AF194").Value, "") <> "" Then
                        r1.Item("CHK_3") = 1
                    End If
                    If par.IfEmpty(ws.Cells("AN194").Value, "") <> "" Then
                        r1.Item("CHK_4") = 1
                    End If
                    If par.IfEmpty(CStr(ws.Cells("R194").Value), "") <> "" Then
                        r1.Item("numero") = ws.Cells("R194").Value
                    End If
                End If
            Next
        End If

        Return dt0
    End Function

    Private Function CreaDTAllSfitto() As Data.DataTable
        Dim dt0 As New Data.DataTable
        Dim dtFinale As New Data.DataTable
        'max 12/06/2015 aggiunto '' as data_caricamento,'' as 'DATA_ULTIMO_RILIEVO
        par.cmd.CommandText = "select 0 as ID,0 as ID_UNITA_IMMOBILIARE,'' as data_caricamento,'' as DATA_ULTIMO_RILIEVO,'' as DATA_RILIEVO,0 as ASCENSORE,0 as SCIVOLI,0 as MONTASCALE,0 as FORO_AREAZIONE_100,'' as NOME_LOCALE_FORO_AREAZ_100,0 as FORO_AREAZIONE_200," _
            & "'' as NOME_LOCALE_FORO_AREAZ_200,'' as COD_STATO_CONSERV,'' as LIVELLO,0 as AGIBILE,0 as ADEGUATO_DISABILI,0 as ADEGUATO_ASCENSORE,0 as NON_VISITAB_LASTRATURA, 0 as NON_VISITAB_OCCUPATO,'' AS NOTE,0 AS ADATTABILE_DISABILI,0 AS PERTINENZA_PORTA," _
            & "0 AS PERTINENZA_INFILTRAZIONI,'' as LOCALE_PAVIMENTI,'' AS VARIE,'' AS NOME_TECNICO,'' AS NOME_RESPONSABILE,'' AS DATA_SOPRALLUOGO,'' AS NON_VISITAB_MURATO,'' AS NON_VISITAB_NO_CHIAVI from dual"
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt0)

        dtFinale = dt0.Clone

        Return dtFinale
    End Function

    Private Function CreaDTSchedaManut() As Data.DataTable
        Dim dt0 As New Data.DataTable
        Dim dtFinale As New Data.DataTable
        par.cmd.CommandText = "SELECT 0 as ID, 0 as ID_ALLOGGIO_SFITTO, 0 as ID_DESC_ST_MANUT, 0 as QUANTITA, 0 as CHK_1, 0 as CHK_2, 0 as CHK_3, 0 as CHK_4, '' as NUMERO,'' as VANO FROM dual"
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt0)

        dtFinale = dt0.Clone

        Return dtFinale
    End Function

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE EXCEL ##########
            'If Not File.Exists(Server.MapPath("..\FileTemp\") & FileUpload1.FileName) Then
            If FileUpload1.HasFile = True Then
                UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload1.FileName
                FileUpload1.SaveAs(UploadOnServer)
            Else
                par.modalDialogMessage("Attenzione", "Nessun file allegato!", Me.Page)
            End If
            'Else
            '    par.modalDialogMessage("Attenzione", "Esiste un file con stesso nome. Riprovare!", Me.Page)
            '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:UploadOnServer " & ex.Message)
            txtRisultati.Text = "Provenienza:UploadOnServer " & ex.Message
            par.modalDialogMessage("Attenzione", "Attenzione...si è verificato un errore!", Me.Page)
        End Try

        Return UploadOnServer
    End Function

    Protected Sub DataGridUIDisponibili_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridUIDisponibili.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_IDui').value='" & e.Item.Cells(0).Text & "';")
        End If

    End Sub

    Protected Sub btnUploadMassivo_Click(sender As Object, e As System.EventArgs) Handles btnUploadMassivo.Click
        If controlloTipoOperatore(idCafOp) <> 0 Or idCafOp <> 6 Then
            txtRisultati.Text = ""
            txtRisultati.Visible = True
            uploadMassivo.Value = "1"
            lblFileDaCaric.Text = "Selezionare la cartella zip da caricare tramite il pulsante ""Sfoglia"""
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
        Else
            par.modalDialogMessage("Attenzione", "Utente non abilitato!", Me.Page)
        End If
    End Sub
End Class
