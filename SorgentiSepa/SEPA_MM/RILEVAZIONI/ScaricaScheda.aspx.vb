Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports OfficeOpenXml

Partial Class RILEVAZIONI_ScaricaScheda
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim idCafOp As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_CAR") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                If controlloTipoOperatore(idCafOp) <> 1 And idCafOp <> 6 And Session.Item("id_operatore") <> 1 Then
                    par.caricaComboBox("select * from SISCOM_MI.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1) order by descrizione asc", cmbUtenti, "ID", "DESCRIZIONE", True)
                    par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
                    par.caricaComboBox("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and RILIEVO_UI.id_lotto in (select id from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) order by edifici.denominazione", cmbEdificio, "ID", "DENOMINAZIONE", True, "-1", "- - -")
                    par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto in (select id from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI,siscom_mi.RILIEVO_UTENTI_OPERATORI where rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente and id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1))) ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")

                Else
                    par.caricaComboBox("select * from SISCOM_MI.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1) order by descrizione asc", cmbUtenti, "ID", "DESCRIZIONE", True)
                    par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente in (select id from siscom_mi.RILIEVO_TAB_UTENTI where id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)) order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)
                    par.caricaComboBox("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and RILIEVO_UI.id_lotto is not null order by edifici.denominazione", cmbEdificio, "ID", "DENOMINAZIONE", True, "-1", "- - -")
                    par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto IS not NULL ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")

                End If

                BindGridLotti()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - ScaricaSchede - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

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
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Scarica Schede - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
        Return livelloWeb
    End Function

    Private Sub BindGridLotti()
        Try
            connData.apri()

            Dim condizLotto As String = ""
            If cmbLotto.SelectedValue <> "-1" Then
                condizLotto = " AND RILIEVO_LOTTI.ID=" & cmbLotto.SelectedValue & " "
            End If
            Dim condizUtente As String = ""
            If cmbUtenti.SelectedValue <> "-1" Then
                condizUtente = " AND RILIEVO_TAB_UTENTI.ID=" & cmbUtenti.SelectedValue & " "
            End If

            Dim Str As String = ""
            Dim condSuOperatori As String = ""
            Dim fromOperatori As String = ""
            If controlloTipoOperatore(idCafOp) <> 1 And idCafOp <> 6 And Session.Item("id_operatore") <> 1 Then
                fromOperatori = ", siscom_mi.RILIEVO_UTENTI_OPERATORI"
                condSuOperatori = "  and rilievo_utenti_operatori.id_operatore = " & Session.Item("id_operatore") & " and rilievo_tab_utenti.id=rilievo_utenti_operatori.id_utente"
            End If

            Str = "select RILIEVO_TAB_UTENTI.DESCRIZIONE AS UTENTE,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£document.getElementById(''CPContenuto_LBLID'').value='||rilievo_lotti.id||';document.getElementById(''CPContenuto_btnDownload1'').click();£><img src=£../Standard/Immagini/download_16.png£ alt=£Download£ border=£0£/></a>','$','&'),'£','" & Chr(34) & "') as DOWNLOAD_SCHEDA,RILIEVO_LOTTI.* from SISCOM_MI.RILIEVO_TAB_UTENTI,SISCOM_MI.RILIEVO_LOTTI" & fromOperatori & " where RILIEVO_TAB_UTENTI.ID=RILIEVO_LOTTI.ID_UTENTE AND RILIEVO_TAB_UTENTI.id_rilievo in (select id from siscom_mi.rilievo where fl_attivo=1)" _
                & condizLotto & condizUtente & condSuOperatori & " order by descrizione asc"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridSchede.DataSource = dt
            DataGridSchede.DataBind()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Scarica Schede - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        CaricaDisponibili()
        'Try
        '    Dim NomeFileXls As String = ""
        '    If Not String.IsNullOrEmpty(Trim(LBLID.Value)) Then
        '        connData.apri(False)
        '        Dim Xls As String = ""
        '        par.cmd.CommandText = " select * " _
        '                            & "FROM siscom_mi.RILIEVO_SCHEDE " _
        '                            & "WHERE in_uso=1"
        '        Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        '        If MyReader.Read Then
        '            Xls = GetString(par.IfNull(MyReader("file_Excel"), ""))
        '        End If
        '        MyReader.Close()
        '        connData.chiudi(False)

        '        If Not String.IsNullOrEmpty(Trim(Xls)) Then

        '            NomeFileXls = "Scheda_" & Format(Now, "yyyyMMddhhmmss") & ".xls"
        '            Dim sw As StreamWriter = New StreamWriter(Server.MapPath("~/FileTemp/") & NomeFileXls, False, System.Text.Encoding.GetEncoding("iso-8859-1"))
        '            sw.WriteLine(Xls)
        '            sw.Close()
        '            NomeFileXls = ZipAllegatoDownload("~/FileTemp/" & NomeFileXls, NomeFileXls)
        '            If File.Exists(Server.MapPath("~\FileTemp\") & NomeFileXls) Then
        '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & NomeFileXls & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '                Exit Sub
        '            Else
        '                par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
        '            End If
        '        Else
        '            par.modalDialogMessage("Attenzione", "Errore durante il download!", Me.Page)
        '        End If
        '        LBLID.Value = ""
        '    Else
        '        par.modalDialogMessage("Attenzione", "Nessuna scheda selezionata!", Me.Page)
        '    End If


        '    'Dim ms As New MemoryStream()
        '    'Using fs As FileStream = File.OpenRead(Server.MapPath("~/FileTemp/" & NomeFileXls))
        '    '    Using excelPackage As New OfficeOpenXml.ExcelPackage(fs)
        '    '        Dim excelWorkBook As OfficeOpenXml.ExcelWorkbook = excelPackage.Workbook
        '    '        Dim excelWorksheet As OfficeOpenXml.ExcelWorksheet = excelWorkBook.Worksheets.
        '    '        excelWorksheet.Cells(1, 1).Value = "Test"
        '    '        excelWorksheet.Cells(3, 2).Value = "Test2"
        '    '        excelWorksheet.Cells(3, 3).Value = "Test3"

        '    '        ' This is the important part.
        '    '        excelPackage.SaveAs(ms)
        '    '    End Using
        '    'End Using

        '    'Using p As New ExcelPackage()
        '    '    Using stream As New FileStream(Server.MapPath("~/FileTemp/pippo2.xlsx"), FileMode.Open, FileAccess.ReadWrite)
        '    '        p.Load(stream)

        '    '        Dim ws As ExcelWorksheet = p.Workbook.Worksheets("SCHEDA MANUT ALL SFITTI")
        '    '        Dim ws2 As ExcelWorksheet
        '    '        ws.Cells("N3").Value = "Name"
        '    '        ws2 = p.Workbook.Worksheets.Add("test")
        '    '        ws2.Cells(1, 1).Value = "Name"
        '    '        'Dim rowIndex As Integer = 2
        '    '        'Dim text As String = ws.Cells(rowIndex, 1).Value.ToString()

        '    '        'Dim comment As String = ws.Comments(0).Text

        '    '        'Dim pictureName As String = ws.Drawings(0).Name

        '    '    End Using
        '    '    p.Save()

        '    'End Using




        '    Dim newFile As New FileInfo(Server.MapPath("~/FileTemp/pippo2.xlsx"))

        '    Dim pck As New ExcelPackage(newFile)
        '    'Add the Content sheet
        '    Dim ws = pck.Workbook.Worksheets("SCHEDA MANUT ALL SFITTI")

        '    ws.Cells("N3").Value = "TEST"
        '    ws.Cells("C1").Value = "Size"
        '    ws.Cells("D1").Value = "Created"
        '    ws.Cells("E1").Value = "Last modified"

        '    pck.Save()

        'Catch ex As Exception
        '    If par.OracleConn.State = Data.ConnectionState.Open Then
        '        connData.chiudi(False)
        '    End If
        '    Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - btnDownload_Click - " & ex.Message)
        '    Response.Redirect("../Errore.aspx", False)
        'End Try
    End Sub

    Private Sub CaricaDisponibili()
        Try
            Dim condEdific As String = ""
            If cmbEdificio.SelectedValue <> -1 Then
                condEdific = " And EDIFICI.ID = " & cmbEdificio.SelectedItem.Value
            End If
            Dim condIndirizzi As String = ""
            If cmbIndirizzo.SelectedValue <> "-1" Then
                condIndirizzi = " And (INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO)= '" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Value) & "'"
            End If

            Dim Str As String = "SELECT " _
                            & "UNITA_IMMOBILIARI.ID,UNITA_IMMOBILIARI.cod_unita_immobiliare,EDIFICI.denominazione AS edificio," _
                            & "INDIRIZZI.descrizione AS indirizzo,INDIRIZZI.civico," _
                            & "UNITA_IMMOBILIARI.interno,SCALE_EDIFICI.descrizione AS scala,TIPO_LIVELLO_PIANO.descrizione AS piano," _
                            & "INDIRIZZI.cap,INDIRIZZI.localita " _
                            & " FROM " _
                            & "siscom_mi.UNITA_IMMOBILIARI, siscom_mi.RILIEVO_UI, siscom_mi.EDIFICI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI, siscom_mi.TIPO_LIVELLO_PIANO " _
                            & " WHERE " _
                            & "UNITA_IMMOBILIARI.ID = RILIEVO_UI.id_unita " _
                            & " AND EDIFICI.ID (+)=UNITA_IMMOBILIARI.id_edificio " _
                            & " AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.id_indirizzo " _
                            & " AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.id_scala " _
                            & " AND TIPO_LIVELLO_PIANO.cod (+)=UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
                            & " AND RILIEVO_UI.ID_RILIEVO in (select id from siscom_mi.rilievo where fl_attivo=1)" _
                            & " And RILIEVO_UI.id_lotto = " & LBLID.Value _
                            & condEdific & condIndirizzi & " " _
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
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Unità disponibili - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function ZipAllegatoDownload(ByVal strFile As String, ByVal nomeFile As String) As String
        ZipAllegatoDownload = ""
        Try
            Dim zipFic As String = ""
            Dim estensioneAllegato As String = Mid(Server.MapPath(strFile), Server.MapPath(strFile).IndexOf(".") + 1)
            Dim AllegatoCompleto As String = nomeFile.Replace(estensioneAllegato, ".zip")
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim strmFile As FileStream = File.OpenRead(Server.MapPath("../FileTemp/" & nomeFile))
            Dim abyBuffer(strmFile.Length - 1) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(Server.MapPath("../FileTemp/" & nomeFile))
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(Server.MapPath("../FileTemp/" & nomeFile))
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            If File.Exists(Server.MapPath("../FileTemp/") & AllegatoCompleto) Then
                File.Delete(Server.MapPath("../FileTemp/") & AllegatoCompleto)
            End If
            zipFic = Server.MapPath("../FileTemp/") & AllegatoCompleto
            strmZipOutputStream = New ZipOutputStream(File.Create(zipFic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            If File.Exists(Server.MapPath(strFile)) Then
                File.Delete(Server.MapPath(strFile))
            End If
            ZipAllegatoDownload = AllegatoCompleto
        Catch ex As Exception
            ZipAllegatoDownload = ""
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ZipAllegatoDownload " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        CType(Me.Master.FindControl("noClose"), HiddenField).Value = 1
        CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
    End Sub

    Public Function GetString(bytes As Byte()) As String
        Dim chars As Char() = New Char(bytes.Length \ 2 - 1) {}
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length)
        Return New String(chars)
    End Function

    Protected Sub DataGridSchede_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSchede.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("style", "cursor:pointer;")
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;SelColo=OldColor;this.style.backgroundColor='#FFFFCC'};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;SelColo=''}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=SelColo;}SelColo=OldColor;Selezionato=this;this.style.backgroundColor='#FF9900';document.getElementById('CPContenuto_LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('CPContenuto_LBLNomeLotto').value='" & e.Item.Cells(1).Text & "';")
        End If
    End Sub

    Protected Sub DataGridSchede_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridSchede.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridSchede.CurrentPageIndex = e.NewPageIndex
            BindGridLotti()
        End If
    End Sub

    Protected Sub cmbLotto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbLotto.SelectedIndexChanged

        BindGridLotti()
    End Sub

    Protected Sub cmbUtenti_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbUtenti.SelectedIndexChanged
        par.caricaComboBox("select * from SISCOM_MI.RILIEVO_LOTTI where id_utente =" & cmbUtenti.SelectedValue & " order by descrizione asc", cmbLotto, "ID", "DESCRIZIONE", True)

        BindGridLotti()
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub btnDownload1_Click(sender As Object, e As System.EventArgs) Handles btnDownload1.Click
        CaricaDisponibili()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
    End Sub

    Protected Sub btnSelezionaTutti_Click(sender As Object, e As System.EventArgs) Handles btnSelezionaTutti.Click
        For Each riga As DataGridItem In DataGridUIDisponibili.Items
            If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False Then
                CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True
            End If
        Next
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
    End Sub

    Protected Sub btnDeselezionaTutti_Click(sender As Object, e As System.EventArgs) Handles btnDeselezionaTutti.Click
        For Each riga As DataGridItem In DataGridUIDisponibili.Items
            If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True Then
                CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False
            End If
        Next
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
    End Sub

    Private Function ControllaCheckbox() As Boolean
        ControllaCheckbox = False
        Try
            Dim chkExport As System.Web.UI.WebControls.CheckBox
            For Each oDataGridItem In Me.DataGridUIDisponibili.Items
                chkExport = oDataGridItem.FindControl("ChSelezionato")
                If chkExport.Checked Then
                    ControllaCheckbox = True
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub btnScaricaScheda_Click(sender As Object, e As System.EventArgs) Handles btnScaricaScheda.Click
        Try
            Dim idUnita As Long = 0
            Dim dt0 As New Data.DataTable
            Dim riga1 As System.Data.DataRow
            Dim dtFinale As New Data.DataTable
            If controlloTipoOperatore(idCafOp) <> 0 Or idCafOp <> 6 Then
                connData.apri()
                If ControllaCheckbox() = True Then
                    par.cmd.CommandText = "select 0 as id_unita,'' as sede_territoriale,'' as mm_spa,'' as codice_ui,'' as quartiere,'' as indirizzo," _
                        & "'' as scala,'' as piano, '' as n_ui,0 as sup_netta,0 as h_interna,'' as referente,'' as tel,'' as email," _
                        & "'' as data_sopralluogo,'' as data_caricamento,'' as data_rilevazione from dual"
                    Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt0)

                    dtFinale = dt0.Clone

                    For i As Integer = 0 To DataGridUIDisponibili.Items.Count - 1
                        If DirectCast(DataGridUIDisponibili.Items(i).Cells(1).FindControl("ChSelezionato"), CheckBox).Checked = True Then
                            idUnita = DataGridUIDisponibili.Items(i).Cells(1).Text
                            par.cmd.CommandText = "select unita_immobiliari.id as idUNI,COD_UNITA_IMMOBILIARE,interno,s_netta,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN,tab_quartieri.nome as quartiere, indirizzi.descrizione as descr,indirizzi.civico from siscom_mi.unita_immobiliari,siscom_mi.indirizzi,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.edifici,siscom_mi.complessi_immobiliari,siscom_mi.tab_quartieri where unita_immobiliari.id_indirizzo=indirizzi.id(+) " _
                                & " and edifici.id_complesso=complessi_immobiliari.id and unita_immobiliari.id_edificio=edifici.id and complessi_immobiliari.id_quartiere=tab_quartieri.id and UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) and unita_immobiliari.id=" & idUnita
                            Dim da1 As Oracle.DataAccess.Client.OracleDataAdapter
                            Dim dt1 As New Data.DataTable
                            da1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            da1.Fill(dt1)

                            riga1 = dtFinale.NewRow()
                            riga1.Item("id_unita") = idUnita

                            riga1.Item("mm_spa") = ""
                            riga1.Item("codice_ui") = par.IfNull(dt1.Rows(0).Item("cod_unita_immobiliare"), 0)
                            riga1.Item("quartiere") = par.IfNull(dt1.Rows(0).Item("quartiere"), "")
                            riga1.Item("indirizzo") = par.IfNull(dt1.Rows(0).Item("descr"), "") & " " & par.IfNull(dt1.Rows(0).Item("civico"), "")
                            riga1.Item("scala") = par.IfNull(dt1.Rows(0).Item("sc"), "")
                            riga1.Item("piano") = par.IfNull(dt1.Rows(0).Item("pian"), "")
                            riga1.Item("n_ui") = par.IfNull(dt1.Rows(0).Item("interno"), "")
                            riga1.Item("sup_netta") = par.IfNull(dt1.Rows(0).Item("s_netta"), 0)
                            'riga1.Item("h_interna") = 0

                            'riga1.Item("data_sopralluogo") = ""
                            'riga1.Item("data_caricamento") = ""
                            'riga1.Item("data_rilevazione") = ""

                            par.cmd.CommandText = "select tab_filiali.nome as filiale from siscom_mi.tab_filiali,siscom_mi.filiali_ui where " _
                                & " filiali_ui.id_filiale = tab_filiali.ID(+) and id_ui=" & par.IfNull(dt1.Rows(0).Item("idUNI"), 0)
                            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If myReader.Read Then
                                riga1.Item("sede_territoriale") = par.IfNull(myReader("filiale"), "")
                            End If
                            myReader.Close()

                            par.cmd.CommandText = "select rilievo_referenti.* from siscom_mi.rilievo_ui,siscom_mi.rilievo_lotti,siscom_mi.rilievo_referenti where rilievo_referenti.id=rilievo_lotti.id_referente and rilievo_ui.id_lotto=rilievo_lotti.id and rilievo_ui.id_unita=" & par.IfNull(dt1.Rows(0).Item("idUNI"), 0)
                            myReader = par.cmd.ExecuteReader
                            If myReader.Read Then
                                riga1.Item("referente") = par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                                riga1.Item("tel") = par.IfNull(myReader("TELEFONO"), "")
                                riga1.Item("email") = par.IfNull(myReader("email"), "")
                            End If
                            myReader.Close()

                            dtFinale.Rows.Add(riga1)
                        End If
                    Next


                    '**** Cerco modello Excel memorizzato del DB ****
                    Dim Xls As Byte()
                    Dim NomeFileXls As String = ""
                    Dim formatoFile As String = ""
                    Dim dataCaricamento As String = ""
                    par.cmd.CommandText = " select * " _
                                        & "FROM siscom_mi.RILIEVO_GESTIONE_SCHEDE " _
                                        & "WHERE in_uso=1"
                    Dim MyReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If MyReader2.Read Then
                        Xls = par.IfNull(MyReader2("file_Excel"), "")
                        formatoFile = "." & par.IfNull(MyReader2("estensione_file"), "")
                        dataCaricamento = par.IfNull(MyReader2("data_Caricamento"), "")
                    End If
                    MyReader2.Close()

                    connData.chiudi()
                    '**** FINE Cerco modello Excel memorizzato del DB ****

                    '**** Creo il file excel precompilato ****
                    Dim bw As BinaryWriter
                    Dim i2 As Integer = 0
                    Dim ElencoFile() As String
                    For Each rowDati As Data.DataRow In dtFinale.Rows
                        NomeFileXls = "Scheda_manutentiva_" & dataCaricamento
                        Dim fileName As String = Server.MapPath("..\FileTemp\") & "Scheda_manutentiva_" & Format(Now, "yyyyMMddHHmmss") & i2 & ".zip"
                        Dim fs As New FileStream(fileName, FileMode.CreateNew, FileAccess.ReadWrite)
                        bw = New BinaryWriter(fs)
                        bw.Write(Xls)
                        bw.Flush()
                        bw.Close()
                        par.EstraiZipFile(fileName, Server.MapPath("~\FileTemp\"), "")

                        fileName = Server.MapPath("~\FileTemp\") & NomeFileXls & formatoFile
                        Dim fileName2 As String = Server.MapPath("~\FileTemp\") & "SCHEDA_" & rowDati.Item("codice_ui") & "_" & Format(Now, "yyyyMMddHHmmss") & formatoFile

                        ReDim Preserve ElencoFile(i2)

                        ElencoFile(i2) = fileName2

                        Dim newFile As New FileInfo(fileName)
                        Dim pck As New ExcelPackage(newFile)
                        Dim ws = pck.Workbook.Worksheets("SCHEDA MANUT ALL SFITTI")

                        ws.Cells("N3").Value = rowDati.Item("sede_territoriale")
                        ws.Cells("X3").Value = rowDati.Item("mm_spa")
                        ws.Cells("AJ3").Value = rowDati.Item("codice_ui")
                        ws.Cells("B6").Value = rowDati.Item("quartiere")
                        ws.Cells("N6").Value = rowDati.Item("indirizzo")
                        ws.Cells("AJ6").Value = rowDati.Item("scala")
                        ws.Cells("AR6").Value = rowDati.Item("piano")
                        ws.Cells("AZ6").Value = rowDati.Item("n_ui")
                        ws.Cells("BH6").Value = rowDati.Item("sup_netta")
                        ws.Cells("BL6").Value = rowDati.Item("h_interna")
                        ws.Cells("B11").Value = rowDati.Item("referente")
                        ws.Cells("T11").Value = rowDati.Item("tel")
                        ws.Cells("AB11").Value = rowDati.Item("email")
                        ws.Cells("AJ11").Value = rowDati.Item("data_sopralluogo")
                        ws.Cells("AV11").Value = rowDati.Item("data_caricamento")
                        ws.Cells("B511").Value = rowDati.Item("data_rilevazione")

                        Dim newFile2 As New FileInfo(fileName2)
                        pck.SaveAs(newFile2)

                        i2 = i2 + 1
                    Next

                    Dim zipfic As String
                    Dim NomeFilezip As String = "SCHEDE_MANUTENTIVE" & Format(Now, "yyyyMMddHHmmss") & ".zip"

                    zipfic = Server.MapPath("..\FileTemp\" & NomeFilezip)

                    Dim kkK As Integer = 0

                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)

                    Dim strFile As String

                    For kkK = 0 To i2 - 1
                        strFile = ElencoFile(kkK)
                        Dim strmFile As FileStream = File.OpenRead(strFile)
                        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                        Dim sFile As String = Path.GetFileName(strFile)
                        Dim theEntry As ZipEntry = New ZipEntry(sFile)
                        Dim fi As New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                        'File.Delete(strFile)
                    Next
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()

                    If File.Exists(Server.MapPath("~\FileTemp\") & NomeFilezip) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & NomeFilezip & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                        Exit Sub
                    Else
                        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante il download. Riprovare!", Me.Page)
                    End If
                Else
                    par.modalDialogMessage("Attenzione", "Selezionare almeno un\'unità", Me.Page)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
                End If
            Else
                par.modalDialogMessage("Attenzione", "Utente non abilitato!", Me.Page)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - btnScaricaScheda_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        cmbIndirizzo.SelectedIndex = -1
        cmbIndirizzo.Items.FindByValue("-1").Selected = True
        CaricaDisponibili()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        cmbEdificio.SelectedIndex = -1
        cmbEdificio.Items.FindByValue("-1").Selected = True
        CaricaDisponibili()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
    End Sub

    Protected Sub DataGridUIDisponibili_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridUIDisponibili.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridUIDisponibili.CurrentPageIndex = e.NewPageIndex
            CaricaDisponibili()
        End If
    End Sub
End Class
