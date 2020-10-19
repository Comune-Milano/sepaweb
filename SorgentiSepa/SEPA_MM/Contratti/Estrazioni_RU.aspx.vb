Imports System.IO

Partial Class Contratti_Estrazioni_RU
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            CreaDgvEdifici()
            CreaDgvIndirizzo()
            CaricaArea()
            CaricaClasse()
            CaricaProvenienza()
            CaricaSindacato()
            CaricaTipiDomanda()
            CaricaTipoRU()
            CaricaTipoUI()
            CaricaDispUI()
            CaricaUffReg()

            txtDataDecorrDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDecorrAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDataScadenzaDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataScadenzaAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDisdettaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDisdettaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtSloggioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtSloggioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtFineValDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFineValAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtInizioValDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioValAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDataAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPagamAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtNettaDa.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtNettaDa.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);")

            txtNettaA.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtNettaA.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);")

            cmbTipo.Attributes.Add("onchanged", "javascript:NascondiConduttore();")

            cmbDettagliCanone.Attributes.Add("onchanged", "javascript:FiltroDich();")
        Else
            Beep()
        End If
    End Sub

    Public Property dataTableIndirizzo() As Data.DataTable
        Get
            If Not (ViewState("dataTableIndirizzo") Is Nothing) Then
                Return ViewState("dataTableIndirizzo")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableIndirizzo") = value
        End Set
    End Property

    Public Property dataTableEdificio() As Data.DataTable
        Get
            If Not (ViewState("dataTableEdificio") Is Nothing) Then
                Return ViewState("dataTableEdificio")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableEdificio") = value
        End Set
    End Property

    Private Sub CreaDgvEdifici()
        Try
            dataTableEdificio = New Data.DataTable
            dataTableEdificio.Columns.Add("ID", Type.GetType("System.String"))
            dataTableEdificio.Columns.Add("CODICE", Type.GetType("System.String"))
            dataTableEdificio.Columns.Add("DENOMINAZIONE", Type.GetType("System.String"))

            DataGridEdifici.DataSource = dataTableEdificio
            DataGridEdifici.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CreaDgvEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CreaDgvIndirizzo()
        Try
            dataTableIndirizzo = New Data.DataTable
            dataTableIndirizzo.Columns.Add("PROGR", Type.GetType("System.String"))
            dataTableIndirizzo.Columns.Add("NOME_INDIRIZZO", Type.GetType("System.String"))
            dataTableIndirizzo.Columns.Add("CIVICO_INDIRIZZO", Type.GetType("System.String"))
            dataTableIndirizzo.Columns.Add("DESCRIZIONE", Type.GetType("System.String"))
            DataGridIndirizzi.DataSource = dataTableIndirizzo
            DataGridIndirizzi.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CreaDgvIndirizzo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaTipoRU()
        Try
            connData.apri()
            par.caricaComboBox("select cod,descrizione from siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE order by descrizione asc", cmbTipo, "COD", "DESCRIZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaTipoRU - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaTipoUI()
        Try
            connData.apri()
            par.caricaComboBox("select cod,descrizione from siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI order by descrizione asc", cmbTipoUnita, "COD", "DESCRIZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaTipoUI - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaArea()
        Try
            connData.apri()
            par.caricaComboBox("select id,descrizione from siscom_mi.AREA_ECONOMICA where id>0 order by id asc", cmbAreaEcon, "ID", "DESCRIZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaArea - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaClasse()
        Try
            connData.apri()
            par.caricaComboBox("select SOTTO_AREA from siscom_mi.canone_sopportabile_27 order by sotto_area asc", cmbClasse, "SOTTO_AREA", "SOTTO_AREA", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaClasse - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaProvenienza()
        Try
            connData.apri()
            par.caricaComboBox(" select " _
                        & " id,descrizione from T_TIPO_PROVENIENZA ORDER BY " _
                        & " (case when descrizione like 'A%' then 1 else 2 END)," _
                        & " (case when descrizione like 'G%' then 1 else 2 END)," _
                        & " (case when descrizione like 'C%' then 1 else 2 END)," _
                        & " (case when descrizione like 'A%' then SUBSTR (descrizione,1,2) else 'ZZ' END)," _
                        & " 1 DESC", cmbProvenienza, "id", "descrizione", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaProvenienza - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaTipiDomanda()
        Try
            connData.apri()
            par.caricaCheckBoxList("select id,lower(descrizione) as descrizione from T_MOTIVO_DOMANDA_VSA where fl_frontespizio=0 order by descrizione asc", chkListTipoDomanda, "ID", "DESCRIZIONE")
            StrConv(Title, VbStrConv.ProperCase)
            For Each Items As ListItem In chkListTipoDomanda.Items
                Items.Text = StrConv(Items.Text, VbStrConv.ProperCase)
            Next
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaTipiDomanda - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaUffReg()
        Try
            connData.apri()
            par.caricaComboBox("select cod,descrizione from siscom_mi.TAB_UFFICIO_REGISTRO order by cod asc", cmbUffReg, "COD", "DESCRIZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaUffReg - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaSindacato()
        Try
            connData.apri()
            par.caricaComboBox("select * from sindacati_vsa", cmbSindacato, "ID", "DESCRIZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaSindacato - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaDispUI()
        Try
            connData.apri()
            par.caricaComboBox("select * from SISCOM_MI.TIPO_DISPONIBILITA order by descrizione asc", cmbDispon, "COD", "DESCRIZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - CaricaDispUI - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAddEdificio_Click(sender As Object, e As System.EventArgs) Handles btnAddEdificio.Click
        Try
            If Not IsNothing(Session.Item("DTRICERCAEDIFICI")) Then
                Session.Remove("DTRICERCAEDIFICI")
            End If
            If Session.Item("idEdificio") <> Nothing Then
                par.cmd = connData.apri()
                par.cmd.CommandText = "SELECT EDIFICI.ID, cod_EDIFICIO AS CODICE , denominazione " _
                                    & "FROM SISCOM_MI.EDIFICI " _
                                    & "WHERE ID = " & Session.Item("idEdificio")
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                Dim rowOggetto As Data.DataRow
                For Each row As Data.DataRow In dt.Rows
                    rowOggetto = dataTableEdificio.NewRow
                    rowOggetto.Item("ID") = par.IfEmpty(row.Item("ID"), "")
                    rowOggetto.Item("CODICE") = par.IfEmpty(row.Item("CODICE"), "")
                    rowOggetto.Item("DENOMINAZIONE") = par.IfEmpty(row.Item("DENOMINAZIONE"), "")
                    dataTableEdificio.Rows.Add(rowOggetto)
                Next
                Session.Remove("idEdificio")
                connData.chiudi(False)
                DataGridEdifici.DataSource = dataTableEdificio
                DataGridEdifici.DataBind()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU -  btnAddEdificio_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridEdifici_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridEdifici.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('Hf_edificio').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub

    Protected Sub btnEliminaEd_Click(sender As Object, e As System.EventArgs) Handles btnEliminaEd.Click
        Try
            For i As Integer = 0 To dataTableEdificio.Rows.Count - 1
                If dataTableEdificio.Rows(i)("ID") = Hf_edificio.Value Then
                    dataTableEdificio.Rows(i).Delete()
                    i = dataTableEdificio.Rows.Count
                End If
            Next
            Hf_edificio.Value = 0
            DataGridEdifici.DataSource = dataTableEdificio
            DataGridEdifici.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - btnEliminaEdificio_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEliminaVia_Click(sender As Object, e As System.EventArgs) Handles btnEliminaVia.Click
        Try
            For i As Integer = 0 To dataTableIndirizzo.Rows.Count - 1
                If dataTableIndirizzo.Rows(i)("PROGR") = Hf_via.Value Then
                    dataTableIndirizzo.Rows(i).Delete()
                    i = dataTableIndirizzo.Rows.Count
                End If
            Next
            Hf_via.Value = ""
            DataGridIndirizzi.DataSource = dataTableIndirizzo
            DataGridIndirizzi.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU - btnEliminaVia_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnAddVia_Click(sender As Object, e As System.EventArgs) Handles btnAddVia.Click
        Try
            If Not IsNothing(Session.Item("DTRICERCAVIA")) Then
                Session.Remove("DTRICERCAVIA")
            End If
            If Session.Item("idIndirizzo") <> Nothing Then
                par.cmd = connData.apri()
                par.cmd.CommandText = "SELECT DISTINCT DESCRIZIONE ||' '|| CIVICO AS DESCRIZIONE,INDIRIZZI.DESCRIZIONE AS NOME_INDIRIZZO,INDIRIZZI.CIVICO AS CIVICO_INDIRIZZO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID " _
                                    & " AND INDIRIZZI.DESCRIZIONE ||' '|| CIVICO = '" & Session.Item("idIndirizzo") & "'"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                Dim num As Integer = 1
                Dim rowOggetto As Data.DataRow
                For Each row As Data.DataRow In dt.Rows
                    rowOggetto = dataTableIndirizzo.NewRow
                    rowOggetto.Item("PROGR") = num
                    rowOggetto.Item("DESCRIZIONE") = par.IfEmpty(row.Item("DESCRIZIONE"), "")
                    rowOggetto.Item("NOME_INDIRIZZO") = par.IfEmpty(row.Item("NOME_INDIRIZZO"), "")
                    rowOggetto.Item("CIVICO_INDIRIZZO") = par.IfEmpty(row.Item("CIVICO_INDIRIZZO"), "")
                    dataTableIndirizzo.Rows.Add(rowOggetto)
                    num = num + 1
                Next
                Session.Remove("idIndirizzo")
                connData.chiudi(False)
                DataGridIndirizzi.DataSource = dataTableIndirizzo
                DataGridIndirizzi.DataBind()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU -  btnAddVia_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE EXCEL ##########
            'If Not File.Exists(Server.MapPath("..\FileTemp\") & FileUpload1.FileName) Then1
            If FileUpload1.HasFile = True Then
                UploadOnServer = Server.MapPath("..\FileTemp\") & Format(Now, "HHmmss") & "_" & FileUpload1.FileName
                FileUpload1.SaveAs(UploadOnServer)
            End If
            'Else
            '    par.modalDialogMessage("Attenzione", "Esiste un file con stesso nome. Riprovare!", Me.Page)
            '    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "div", "MostraDiv();", True)
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Estrazioni_RU - UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return UploadOnServer
    End Function

    Private Function LeggiDaXlsDich() As Data.DataTable
        Dim xls As New ExcelSiSol
        Dim row As Data.DataRow
        Dim DTADAFILE2 As New Data.DataTable

        If Me.FileUploadDich.HasFile Then
            Dim fileOK As Boolean = False
            Dim fileExtension As String = System.IO.Path.GetExtension(FileUploadDich.FileName).ToLower()
            Dim allowedExtensions As String() = {".xlsx"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                    Exit For
                End If
            Next
            If fileOK = False Then
                par.modalDialogMessage("Attenzione", "Inserire un file con formato *.xlsx corretto!", Me.Page)
                Exit Function
            End If
        Else
            par.modalDialogMessage("Attenzione", "Inserire un file con formato *.xlsx corretto!", Me.Page)
            Exit Function
        End If
        Dim nFile As String = Server.MapPath("..\FileTemp\") & Format(Now, "HHmmss") & "_" & FileUploadDich.FileName
        FileUploadDich.SaveAs(nFile)
        Dim dtExcel As Data.DataTable

        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nFile, FileMode.Open)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtExcel = xls.WorksheetToDataTable(ws, True)
        End Using

        connData.apri()
        DTADAFILE2.Columns.Add("ID_DICH")
        If dtExcel.Rows.Count > 0 Then
            For i As Integer = 0 To dtExcel.Rows.Count - 1
                par.cmd.CommandText = "select ID from DICHIARAZIONI_VSA where pg='" & par.PulisciStrSql(dtExcel.Rows(i).Item(0)) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    row = DTADAFILE2.NewRow()
                    row.Item("ID_DICH") = myReader("ID")
                    DTADAFILE2.Rows.Add(row)
                End If
                myReader.Close()
            Next
        End If
        connData.chiudi()

        Return DTADAFILE2

    End Function


    Private Function LeggiDaXls() As Data.DataTable
        Dim xls As New ExcelSiSol
        Dim row As Data.DataRow
        Dim DTADAFILE As New Data.DataTable

        If Me.FileUpload1.HasFile Then
            Dim fileOK As Boolean = False
            Dim fileExtension As String = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()
            Dim allowedExtensions As String() = {".xlsx"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                    Exit For
                End If
            Next
            If fileOK = False Then
                par.modalDialogMessage("Attenzione", "Inserire un file con formato *.xlsx corretto!", Me.Page)
                Exit Function
            End If
        Else
            par.modalDialogMessage("Attenzione", "Inserire un file con formato *.xlsx corretto!", Me.Page)
            Exit Function
        End If
        Dim nFile As String = Server.MapPath("..\FileTemp\") & Format(Now, "HHmmss") & "_" & FileUpload1.FileName
        FileUpload1.SaveAs(nFile)
        Dim dtExcel As Data.DataTable

        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nFile, FileMode.Open)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtExcel = xls.WorksheetToDataTable(ws, True)
        End Using

        connData.apri()
        DTADAFILE.Columns.Add("ID_UI")
        If dtExcel.Rows.Count > 0 Then
            For i As Integer = 0 To dtExcel.Rows.Count - 1
                par.cmd.CommandText = "select ID from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & par.PulisciStrSql(dtExcel.Rows(i).Item(0)) & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    row = DTADAFILE.NewRow()
                    row.Item("ID_UI") = myReader("ID")
                    DTADAFILE.Rows.Add(row)
                End If
                myReader.Close()
            Next
        End If
        connData.chiudi()

        Return DTADAFILE

    End Function

    Public Property DTADAFILE() As Data.DataTable
        Get
            If Not (ViewState("DTADAFILE") Is Nothing) Then
                Return ViewState("DTADAFILE")
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("DTADAFILE") = value
        End Set
    End Property

    Protected Sub btnAvviaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnAvviaRicerca.Click
        Try
            Dim stringaCondizioni As String = ""
            Dim dtIdUI As New Data.DataTable
            Dim elencoIdUI As String = ""

            If FileUpload1.FileName <> "" Then
                dtIdUI = LeggiDaXls()

                If dtIdUI.Rows.Count > 0 Then
                    If dtIdUI.Rows.Count >= 1000 Then
                        par.modalDialogMessage("Attenzione", "Caricare al massimo 1000 unità!", Me.Page)
                        Exit Try
                    End If
                    For i As Integer = 0 To dtIdUI.Rows.Count - 1
                        elencoIdUI = elencoIdUI & dtIdUI.Rows(i).Item(0) & ","
                    Next
                Else
                    par.modalDialogMessage("Attenzione", "Impossibile importare i dati! Verificare che siano codici alfanumerici di 17 cifre.", Me.Page)
                    Exit Try
                End If
            End If
            stringaCondizioni = FiltriGenericiRicerca(elencoIdUI)

            Select Case rdbTipoEstrazione.SelectedValue
                Case "RU"
                    EstrazioneRU(stringaCondizioni)
                Case "UI"
                    EstrazioneUI(stringaCondizioni)
                Case "COND"
                    EstrazioneConduttore(stringaCondizioni)
                Case "CANONE"
                    EstrazioneCanone(stringaCondizioni)
                Case "REG"
                    EstrazioneRegistr(stringaCondizioni)
                Case "SBOL"
                    EstrazioneSchemaBoll(stringaCondizioni)
                Case "CONTAB"
                    EstrazioneContab(stringaCondizioni)
                Case "SALDO"
                    EstrazioneSaldo(stringaCondizioni)
                Case "COMUNIC"
                    EstrazioneComunicaz(stringaCondizioni)
                Case "INTERESSI"
                    EstrazioneInteressi(stringaCondizioni)
            End Select

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU -   btnAvviaRicerca_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function FiltriGenericiRicerca(ByVal elencoCodUIXls As String) As String
        Dim ElencoEdifici As String = ""
        Dim ElencoIndirizzi As String = ""
        Dim codContratto As String = ""
        Dim codUI As String = ""
        Dim statoRU As String = ""
        Dim tipoRU As String = ""
        Dim tipoUI As String = ""
        Dim decorrDal As String = ""
        Dim decorrAl As String = ""
        Dim scadenzaDal As String = ""
        Dim scadenzaAl As String = ""
        Dim disdettaDal As String = ""
        Dim disdettaAl As String = ""
        Dim sloggioDal As String = ""
        Dim sloggioAl As String = ""
        Dim sValore As String = ""
        Dim bTrovato As Boolean = False
        Dim sStringaSql As String = ""
        Dim sCompara As String = ""

        codContratto = txtCodRU.Text
        codUI = txtCodUI.Text
        statoRU = cmbStato.SelectedValue
        tipoRU = cmbTipo.SelectedValue
        tipoUI = cmbTipoUnita.SelectedValue
        decorrDal = txtDataDecorrDAL.Text
        decorrAl = txtDataDecorrAL.Text
        scadenzaDal = txtDataScadenzaDAL.Text
        scadenzaAl = txtDataScadenzaAL.Text
        disdettaDal = txtDisdettaDal.Text
        disdettaAl = txtDisdettaAl.Text
        sloggioDal = txtSloggioDal.Text
        sloggioAl = txtSloggioAl.Text

        If elencoCodUIXls = "" Then
            If dataTableEdificio.Rows.Count > 0 Then
                For Each row As Data.DataRow In dataTableEdificio.Rows
                    ElencoEdifici = ElencoEdifici & row.Item("ID") & ","
                Next
            End If
            If dataTableIndirizzo.Rows.Count > 0 Then
                For Each row As Data.DataRow In dataTableIndirizzo.Rows
                    If ElencoIndirizzi <> "" Then ElencoIndirizzi = ElencoIndirizzi & " OR "
                    ElencoIndirizzi = ElencoIndirizzi & " (INDIRIZZI.DESCRIZIONE='" & par.PulisciStrSql(row.Item("NOME_INDIRIZZO")) & "' AND INDIRIZZI.CIVICO='" & row.Item("CIVICO_INDIRIZZO") & "')"
                Next
            End If
            If ElencoIndirizzi <> "" Then ElencoIndirizzi = "(" & ElencoIndirizzi & ")"

            If codContratto <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = codContratto
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & sValore & "' "
            End If

            If codUI <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = codUI
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & sCompara & " '" & sValore & "'"
            End If

            If ElencoEdifici <> "" Then
                ElencoEdifici = "(" & Mid(ElencoEdifici, 1, Len(ElencoEdifici) - 1) & ")"
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = ElencoEdifici
                bTrovato = True
                sStringaSql = sStringaSql & " EDIFICI.ID IN " & sValore & " "
            End If

            If ElencoIndirizzi <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = ElencoIndirizzi
                bTrovato = True
                sStringaSql = sStringaSql & sValore
            End If
        Else
            elencoCodUIXls = "(" & Mid(elencoCodUIXls, 1, Len(elencoCodUIXls) - 1) & ")"
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = elencoCodUIXls
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID IN " & sValore & " "
        End If

        If decorrDal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = decorrDal
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DECORRENZA>='" & par.AggiustaData(sValore) & "' "
        End If

        If decorrAl <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = decorrAl
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_DECORRENZA<='" & par.AggiustaData(sValore) & "' "
        End If

        If scadenzaDal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = scadenzaDal
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA>='" & par.AggiustaData(sValore) & "' "
        End If

        If scadenzaAl <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = scadenzaAl
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_SCADENZA<='" & par.AggiustaData(sValore) & "' "
        End If

        If sloggioDal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sloggioDal
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA>='" & par.AggiustaData(sValore) & "' "
        End If

        If sloggioAl <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sloggioAl
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA<='" & par.AggiustaData(sValore) & "' "
        End If

        If statoRU <> "TUTTI" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = statoRU
            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='" & par.PulisciStrSql(sValore) & "' "
        End If

        If tipoRU <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = tipoRU
            bTrovato = True
            sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC='" & sValore & "' "
        End If

        If tipoUI <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = tipoUI
            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_TIPOLOGIA='" & sValore & "' "
        End If

        Return sStringaSql

    End Function

    Private Sub EstrazioneRU(ByVal sStringaSql As String)

        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        par.cmd.CommandText = "select ''''||rapporti_utenza.cod_contratto as cod_contratto," _
            & " (select tipologia_contratto_locazione.descrizione from siscom_mi.tipologia_contratto_locazione where cod=cod_tipologia_contr_loc) as tipo_contratto," _
            & " (select tipologia_contratto_locazione.rif_legislativo from siscom_mi.tipologia_contratto_locazione where cod=cod_tipologia_contr_loc) as rif_legislativo," _
            & " (CASE WHEN RAPPORTI_UTENZA.DEST_USO ='B' THEN 'POSTO AUTO COPERTO,SCOPERTO,BOX,MOTOBOX ETC.'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='N' THEN 'NEGOZIO, MAGAZZINO, LABORATORIO, UFFICIO, ETC.'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='R' THEN 'RESIDENZIALE'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='0' THEN 'STANDARD'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='C' THEN 'COOPERATIVE'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='P' THEN '431 P.O.R.'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='A' THEN '392/78 ASSOCIAZIONI'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='D' THEN '431/98 Art.15 R.R.1/2004'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='V' THEN '431/98 Art.15 C.2 R.R.1/2004'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='S' THEN '431/98 SPECIALI'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='Z' THEN 'FORZE DELL''ORDINE'" _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='X' THEN 'CONCESSIONE SPAZI P.' " _
            & " WHEN RAPPORTI_UTENZA.DEST_USO ='Y' THEN 'COMODATO D''USO GRATUITO'  " _
            & " END) AS destinazione_uso, (CASE WHEN DESCR_DEST_USO ='0' THEN '' else DESCR_DEST_USO END) as DESCR_DEST_USO," _
            & " durata_anni,durata_rinnovo," _
            & " (CASE WHEN RAPPORTI_UTENZA.FL_TUTORE_STR ='1' THEN 'SI' else 'NO' END) as tutore_straordinario," _
            & " (CASE WHEN RAPPORTI_UTENZA.FL_ASSEGN_TEMP ='1' THEN 'SI' else 'NO' END) as assegn_temp," _
            & " delibera,to_char(to_date(data_Delibera,'yyyymmdd'),'dd/mm/yyyy') as data_provvedimento," _
            & " to_char(to_date(data_stipula,'yyyymmdd'),'dd/mm/yyyy') as data_stipula," _
            & " to_char(to_date(data_decorrenza,'yyyymmdd'),'dd/mm/yyyy') as data_decorr," _
            & " to_char(to_date(data_consegna,'yyyymmdd'),'dd/mm/yyyy') as data_consegna,mesi_disdetta," _
            & " to_char(to_date(DATA_DISDETTA_LOCATARIO,'yyyymmdd'),'dd/mm/yyyy') as DATA_DISDETTA_LOCATARIO," _
            & " to_char(to_date(DATA_INVIO_RIC_DISDETTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_INVIO_RIC_DISDETTA," _
            & " to_char(to_date(DATA_NOTIFICA_DISDETTA,'yyyymmdd'),'dd/mm/yyyy') as DATA_NOTIFICA_DISDETTA," _
            & " to_char(to_date(DATA_RICONSEGNA,'yyyymmdd'),'dd/mm/yyyy') as DATA_RICONSEGNA," _
            & " to_char(to_date(DATA_SCADENZA,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA," _
            & " to_char(to_date(DATA_SCADENZA_RINNOVO,'yyyymmdd'),'dd/mm/yyyy') as DATA_SCADENZA_RINNOVO," _
            & " (select to_char(to_date(substr(DATA_APP_PRE_SLOGGIO,1,8),'yyyymmdd'),'dd/mm/yyyy') from siscom_mi.sl_sloggio where id_contratto=rapporti_utenza.id and sl_sloggio.id in (select max(id) from siscom_mi.sl_sloggio sl where sl.id_contratto=sl_sloggio.id_contratto)) as DATA_APP_PRE_SLOGGIO," _
            & " (select to_char(to_date(substr(DATA_APP_RAPPORTO_SLOGGIO,1,8),'yyyymmdd'),'dd/mm/yyyy') from siscom_mi.sl_sloggio where id_contratto=rapporti_utenza.id and sl_sloggio.id in (select max(id) from siscom_mi.sl_sloggio sl where sl.id_contratto=sl_sloggio.id_contratto)) as DATA_APP_RAPPORTO_SLOGGIO " _
            & " from siscom_mi.rapporti_utenza,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.edifici,siscom_mi.indirizzi " _
            & " where rapporti_utenza.id=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_immobiliari.id=unita_contrattuale.id_unita and unita_immobiliari.id_edificio=edifici.id " _
            & " and unita_immobiliari.id_indirizzo=indirizzi.id(+) " & sStringaSql & " order by data_decorrenza asc"
        '& " ,RAPPORTI_UTENZA.SCALA_COR AS SCALA_RECAPITO,RAPPORTI_UTENZA.PIANO_COR AS PIANO_RECAPITO,INTERNO_COR AS INTERNO_RECAPITO " _

        EstraiDati(par.cmd.CommandText, 1)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        'Dim daRU As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dtRU As New Data.DataTable
        'daRU = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        'daRU.Fill(dtRU)
        'daRU.Dispose()

        'If dtRU.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportRU", "ExportRU", dtRU)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If
    End Sub

    Private Sub EstrazioneUI(ByVal sStringaSql As String)
        Dim tipoDisp As String = cmbDispon.SelectedValue
        Dim nettaDal As Integer = par.IfEmpty(txtNettaDa.Text, 0)
        Dim nettaAl As Integer = par.IfEmpty(txtNettaA.Text, 1000000D)

        Dim svalore As String = ""
        Dim bTrovato As Boolean = False
        If sStringaSql <> "" Then bTrovato = True

        If tipoDisp <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = tipoDisp
            bTrovato = True
            sStringaSql = sStringaSql & " unita_immobiliari.COD_TIPO_DISPONIBILITA='" & svalore & "' "
        End If


        Dim condizioneDimensioni As String = ""
        If nettaDal <> 0 Or nettaAl <> 1000000 Then
            condizioneDimensioni = " AND (SELECT MAX(VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID=DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA') BETWEEN " & Math.Max(nettaDal, 0) & "  AND " & Math.Min(nettaAl, 1000000D)
        End If


        'If chkPertinenza.Checked = True Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

        '    bTrovato = True
        '    sStringaSql = sStringaSql & " unita_immobiliari.id_unita_principale is not null"
        'Else
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "

        '    bTrovato = True
        '    sStringaSql = sStringaSql & " unita_immobiliari.id_unita_principale is null"
        'End If

        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        'par.cmd.CommandText = " SELECT " _
        '    & " ''''||unita_immobiliari.COD_UNITA_immobiliare as cod_unita_immob,(SELECT MAX(VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID=DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA') AS MQNETTA," _
        '    & "(case when unita_immobiliari.id_unita_principale is not null then 'SI' else 'NO' end) as pertinenza,(select descrizione from siscom_mi.TIPO_DISPONIBILITA where COD_TIPO_DISPONIBILITA=cod) as disponibilita_ui," _
        '    & " (select tipo_livello_piano.descrizione from siscom_mi.tipo_livello_piano where tipo_livello_piano.cod=unita_immobiliari.cod_tipo_livello_piano) as piano," _
        '    & " (select scale_edifici.descrizione from siscom_mi.scale_edifici where scale_edifici.ID=unita_immobiliari.id_scala) as scala, " _
        '    & " (select TIPOLOGIA_UNITA_IMMOBILIARI.descrizione from siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI where cod=unita_immobiliari.cod_tipologia) as tipo_ui, " _
        '    & " (select foglio from siscom_mi.identificativi_catastali where identificativi_catastali.ID=unita_immobiliari.id_catastale) as foglio," _
        '    & " (select numero from siscom_mi.identificativi_catastali where identificativi_catastali.ID=unita_immobiliari.id_catastale) as mappale,  " _
        '    & " (select sub from siscom_mi.identificativi_catastali where identificativi_catastali.ID=unita_immobiliari.id_catastale) as sub,  " _
        '    & " indirizzi.DESCRIZIONE ||' '|| indirizzi.CIVICO AS indirizzo, unita_immobiliari.INTERNO " _
        '    & " FROM siscom_mi.rapporti_utenza," _
        '    & " siscom_mi.unita_immobiliari," _
        '    & " siscom_mi.unita_contrattuale," _
        '    & " siscom_mi.edifici," _
        '    & " siscom_mi.indirizzi" _
        '    & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
        '    & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
        '    & " AND unita_immobiliari.id_edificio = edifici.id" _
        '    & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+) " & sStringaSql _
        '    & condizioneDimensioni _
        '    & " order by indirizzo asc,unita_immobiliari.COD_UNITA_immobiliare asc"

        ''MAX 12/11/2018 MODIFICA SEGNALAZIONE 2538/2018
        par.cmd.CommandText = "SELECT distinct " _
                            & "UNITA_IMMOBILIARI.ID AS IDU,'''' || unita_immobiliari.COD_UNITA_immobiliare AS cod_unita_immob, " _
                            & "COMPLESSI_IMMOBILIARI.COD_COMPLESSO,COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS DENOMINAZIONE_COMPLESSO,EDIFICI.COD_EDIFICIO,EDIFICI.DENOMINAZIONE AS DENOMINAZIONE_EDIFICIO,COD_EDIFICIO_GIMI, " _
                            & "EDIFICI.NUM_PIANI_ENTRO AS NUM_PIANI_INTERRATI,EDIFICI.NUM_PIANI_FUORI AS NUM_PIANI,EDIFICI.NUM_SCALE, " _
                            & "to_char(to_date(EDIFICI.DATA_COSTRUZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_COSTRUZIONE,to_char(to_date(EDIFICI.DATA_RISTRUTTURAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RISTRUTTURAZIONE, " _
                            & "UTILIZZO_PRINCIPALE_EDIFICIO.DESCRIZIONE AS UTILIZZO_PRINCIPALE,  " _
                            & "TIPO_UBICAZIONE_LG_392_78.DESCRIZIONE AS UBICAZIONE_392_78,DECODE(NVL(UNITA_IMMOBILIARI.P_ASCENSORE,0),0,'NO',1,'SI') AS ASCENSORE, " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA') AS MQNETTA, " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV') AS MQCONVENZIONALE, " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_CONV_LR') AS MQCONVENZIONALE_UR1_2004, " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'VOL_RISCALDATO') AS VOLUME_RISCALDATO, " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'MCRISCALD') AS MC_RISCALDATI,   " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'ALT_INT') AS ALTEZZA_INTERNA_ML,       " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'BALCONI') AS SUP_BALCONI_TERRAZZE,      " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'GIARDINI') AS SUP_GIARDINI,     " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUT70') AS SUP_MINORE_170,         " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUSCO') AS SUP_SCOPERTA_ESCLUSIVO,     " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'VESCL') AS SUP_VERDE_ESCLUSIVO,     " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'VANI') AS VANI_RILEVATI,   " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUVEC') AS SUP_VERDE_COMUNE,        " _
                            & "IDENTIFICATIVI_CATASTALI.SUPERFICIE_CATASTALE, (SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_COMM') AS SUP_COMMERCIALE,      " _
                            & "(SELECT MAX (VALORE) FROM SISCOM_MI.DIMENSIONI WHERE  UNITA_IMMOBILIARI.ID = DIMENSIONI.ID_UNITA_IMMOBILIARE AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_LORDA') AS SUP_LORDA,                                                                                         " _
                            & "(CASE WHEN unita_immobiliari.id_unita_principale IS NOT NULL THEN 'SI' ELSE 'NO' END) AS pertinenza, " _
                            & "(SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI A WHERE A.ID=UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE) AS PERTINENZA_DI, " _
                            & "(SELECT descrizione FROM siscom_mi.TIPO_DISPONIBILITA WHERE COD_TIPO_DISPONIBILITA = cod) AS disponibilita_ui, " _
                            & "(SELECT tipo_livello_piano.descrizione FROM siscom_mi.tipo_livello_piano WHERE tipo_livello_piano.cod = unita_immobiliari.cod_tipo_livello_piano) AS piano, " _
                            & "(SELECT scale_edifici.descrizione FROM siscom_mi.scale_edifici WHERE scale_edifici.ID = unita_immobiliari.id_scala) AS scala, " _
                            & "(SELECT TIPOLOGIA_UNITA_IMMOBILIARI.descrizione FROM siscom_mi.TIPOLOGIA_UNITA_IMMOBILIARI WHERE cod = unita_immobiliari.cod_tipologia) AS tipo_ui, " _
                            & "IDENTIFICATIVI_CATASTALI.FOGLIO, IDENTIFICATIVI_CATASTALI.NUMERO AS mappale, " _
                            & "IDENTIFICATIVI_CATASTALI.SUB AS sub, IDENTIFICATIVI_CATASTALI.RENDITA AS RENDITA_UI, " _
                            & "indirizzi.DESCRIZIONE AS INDIRIZZO_UI,indirizzi.CIVICO AS CIVICO_UI,unita_immobiliari.INTERNO,INDIRIZZI.CAP AS CAP_UI,COMUNI_NAZIONI.ID AS ID_COMUNE,COMUNI_NAZIONI.NOME AS COMUNE, " _
                            & "DESTINAZIONI_USO_UI.DESCRIZIONE AS DESTINAZIONE_USO_UI,STATO_CONSERVATIVO_LG_392_78.DESCRIZIONE AS STATO_CONSERVAZIONE_392_78,DECODE(COMUNI_NAZIONI.ENTRO_70KM,0,'NO',1,'SI') AS UI_ENTRO_70KM, " _
                            & "IDENTIFICATIVI_CATASTALI.ZONA_CENSUARIA,TAB_ZONA_OSMI.DESCRIZIONE AS ZONA_OSMI, " _
                            & "(SELECT (NOME || ' - ' || TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE) FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST  " _
                            & "AND TAB_FILIALI.ID=(SELECT MAX(ID_FILIALE) FROM SISCOM_MI.FILIALI_UI WHERE ID_UI=UNITA_IMMOBILIARI.ID)) AS SEDE_TERRITORIALE, " _
                            & "(CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.CONDOMINI WHERE ID IN (SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO=EDIFICI.ID))>0 THEN 'SI' ELSE 'NO' END) AS GESTIONE_CONDOMINIO, " _
                            & "(CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.IMPIANTI WHERE COD_TIPOLOGIA='SO' AND ID_EDIFICIO=EDIFICI.ID)>0 THEN 'SI' ELSE 'NO' END) AS ASCENSORE_GESTIONE , " _
                            & "(CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.IMPIANTI WHERE COD_TIPOLOGIA='TE' AND ID_EDIFICIO=EDIFICI.ID)>0 THEN 'SI' ELSE 'NO' END) AS CT_GESTIONE, " _
                            & " siscom_mi.getvalorelocativo(unita_immobiliari.id) as valore_locativo " _
                            & "FROM siscom_mi.rapporti_utenza, " _
                            & "siscom_mi.unita_immobiliari, " _
                            & "siscom_mi.unita_contrattuale, " _
                            & "siscom_mi.edifici, " _
                            & "siscom_mi.indirizzi, " _
                            & "SISCOM_MI.IDENTIFICATIVI_CATASTALI, " _
                            & "SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPO_UBICAZIONE_LG_392_78,COMUNI_NAZIONI, " _
                            & "SISCOM_MI.UTILIZZO_PRINCIPALE_EDIFICIO, " _
                            & "SISCOM_MI.DESTINAZIONI_USO_UI, " _
                            & "SISCOM_MI.STATO_CONSERVATIVO_LG_392_78, " _
                            & "SISCOM_MI.TAB_ZONA_OSMI " _
                            & "WHERE  " _
                            & "TAB_ZONA_OSMI.ID (+)=EDIFICI.ID_OSMI AND " _
                            & "STATO_CONSERVATIVO_LG_392_78.COD(+)=UNITA_IMMOBILIARI.COD_STATO_CONS_LG_392_78 AND " _
                            & "DESTINAZIONI_USO_UI.ID (+)=UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO AND " _
                            & "IDENTIFICATIVI_CATASTALI.ID (+)=UNITA_IMMOBILIARI.ID_CATASTALE AND " _
                            & "COMUNI_NAZIONI.COD (+)=INDIRIZZI.COD_COMUNE AND UTILIZZO_PRINCIPALE_EDIFICIO.COD (+)=EDIFICI.COD_UTILIZZO_PRINCIPALE AND " _
                            & "TIPO_UBICAZIONE_LG_392_78.COD (+)=COMPLESSI_IMMOBILIARI.COD_TIPO_UBICAZIONE_LG_392_78 AND COMPLESSI_IMMOBILIARI.ID (+)=EDIFICI.ID_COMPLESSO AND " _
                            & "rapporti_utenza.id = unita_contrattuale.id_contratto " _
                            & "AND unita_immobiliari.id = unita_contrattuale.id_unita " _
                            & "AND unita_immobiliari.id_edificio = edifici.id " _
                            & "AND unita_immobiliari.id_indirizzo = indirizzi.id(+) " _
                            & sStringaSql _
                            & " " & condizioneDimensioni

        EstraiDati(par.cmd.CommandText, 2)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        'Dim daUI As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dtUI As New Data.DataTable
        'daUI = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        'daUI.Fill(dtUI)
        'daUI.Dispose()

        'If dtUI.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportUI", "ExportUI", dtUI)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If


    End Sub

    Private Sub EstrazioneConduttore(ByVal sStringaSql As String)
        Dim tipoDomanda As String = ""
        Dim presenzaOspiti As Integer = 0
        Dim svalore As String = ""
        Dim bTrovato As Boolean = False

        Dim dtCond As New Data.DataTable
        dtCond.Columns.Add("COD_CONTRATTO", Type.GetType("System.String"))
        dtCond.Columns.Add("COMPONENTE", Type.GetType("System.String"))
        dtCond.Columns.Add("COD_FISCALE_P_IVA", Type.GetType("System.String"))
        dtCond.Columns.Add("ORIGINE", Type.GetType("System.String"))
        dtCond.Columns.Add("TIPO_OCCUPANTE", Type.GetType("System.String"))
        dtCond.Columns.Add("DATA_INIZIO", Type.GetType("System.String"))
        dtCond.Columns.Add("DATA_FINE", Type.GetType("System.String"))


        For Each Items As ListItem In chkListTipoDomanda.Items
            If Items.Selected = True Then
                tipoDomanda &= Items.Value & ","
            End If
        Next

        If tipoDomanda <> "" Then
            tipoDomanda = Left(tipoDomanda, Len(tipoDomanda) - 1)
        End If
        If chkOspiti.Checked = True Then
            presenzaOspiti = 1
        Else
            presenzaOspiti = 0
        End If

        If tipoDomanda <> "" Then
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            'sValore = tipoDomanda
            'bTrovato = True
            'sStringaSql = sStringaSql & " DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA in (" & svalore & ") "
        End If

        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        Dim queryPrincipale As String = ""

        queryPrincipale = " select rapporti_utenza.cod_contratto as cod_contratto" _
            & " FROM siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
            & " siscom_mi.unita_immobiliari," _
            & " siscom_mi.unita_contrattuale," _
            & " siscom_mi.edifici," _
            & " siscom_mi.indirizzi" _
            & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
            & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
            & " AND unita_immobiliari.id_edificio = edifici.id" _
            & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
            & " and unita_contrattuale.id_unita_principale is null" _
            & " AND TIPOLOGIA_OCCUPANTE.COD=cod_tipologia_occupante" _
            & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_Contratto=rapporti_utenza.id " _
            & sStringaSql & " "


        par.cmd.CommandText = " SELECT DOMANDE_BANDO_VSA.CONTRATTO_NUM AS cod_contratto," _
            & " cognome || ' ' || nome AS COMPONENTE," _
            & " COD_FISCALE AS COD_FISCALE_P_IVA," _
            & " T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS ORIGINE," _
            & " '' AS TIPO_OCCUPANTE," _
            & " siscom_mi.getdata (DATA_INIZIO_VAL) AS DATA_INIZIO," _
            & " siscom_mi.getdata (DATA_FINE_VAL) AS DATA_FINE" _
            & " FROM DOMANDE_BANDO_VSA," _
            & " T_MOTIVO_DOMANDA_VSA," _
            & " DICHIARAZIONI_VSA," _
            & " COMP_NUCLEO_VSA" _
            & "  WHERE     DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE" _
            & " AND DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE" _
            & " AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID" _
            & " AND FL_AUTORIZZAZIONE = 1" _
            & " AND CONTRATTO_NUM IN" _
            & " (" & queryPrincipale & ")" _
            & " UNION" _
            & " SELECT UTENZA_DICHIARAZIONI.rapporto AS COD_CONTRATTO," _
            & " cognome || ' ' || nome AS COMPONENTE," _
            & " cod_fiscale AS COD_FISCALE_P_IVA," _
            & " UTENZA_BANDI.DESCRIZIONE AS ORIGINE," _
            & " '' AS TIPO_OCCUPANTE," _
            & " siscom_mi.getdata (DATA_INIZIO_VAL) AS DATA_INIZIO," _
            & " siscom_mi.getdata (DATA_FINE_VAL) AS DATA_FINE" _
            & "   FROM UTENZA_DICHIARAZIONI, UTENZA_BANDI, UTENZA_COMP_NUCLEO" _
            & "  WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE = UTENZA_DICHIARAZIONI.ID" _
            & " AND NVL (FL_GENERAZ_AUTO, 0) = 0" _
            & " AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL" _
            & " OR UTENZA_DICHIARAZIONI.NOTE_WEB <> 'GENERATA_AUTOMATICAMENTE')" _
            & " AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO" _
            & " AND rapporto IN" _
            & " (" & queryPrincipale & ")" _
            & " UNION" _
            & " SELECT domande_bando.CONTRATTO_NUM AS cod_Contratto," _
            & " cognome || ' ' || nome AS COMPONENTE," _
            & " cod_fiscale AS COD_FISCALE_P_IVA," _
            & " '' AS ORIGINE," _
            & " '' AS TIPO_OCCUPANTE," _
            & " '' AS DATA_INIZIO," _
            & " '' AS DATA_FINE" _
            & "   FROM BANDI," _
            & " DOMANDE_BANDO," _
            & " DICHIARAZIONI," _
            & " COMP_NUCLEO" _
            & " WHERE     COMP_NUCLEO.ID_DICHIARAZIONE = DICHIARAZIONI.ID" _
            & " AND DICHIARAZIONI.ID = DOMANDE_BANDO.ID_DICHIARAZIONE" _
            & " AND BANDI.ID = DOMANDE_BANDO.ID_BANDO" _
            & " AND contratto_num IN" _
            & " (" & queryPrincipale & ")" _
            & " UNION" _
            & " SELECT (SELECT cod_Contratto" _
            & "  FROM siscom_mi.rapporti_utenza" _
            & " WHERE rapporti_utenza.id = soggetti_contrattuali_inizio.id_contratto)" _
            & " AS COD_CONTRATTO," _
           & " CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS COMPONENTE," _
            & " CASE WHEN anagrafica.partita_iva is not null then partita_iva else anagrafica.COD_FISCALE end AS COD_FISCALE_P_IVA," _
            & " 'Situazione Iniziale' AS ORIGINE," _
            & " '' AS TIPO_OCCUPANTE," _
            & " '' AS DATA_INIZIO," _
            & " '' AS DATA_FINE" _
            & " FROM SISCOM_MI.SOGGETTI_CONTRATTUALI_INIZIO, siscom_mi.anagrafica" _
            & " WHERE SOGGETTI_CONTRATTUALI_INIZIO.id_anagrafica = anagrafica.id" _
            & " AND (SELECT cod_Contratto" _
            & " FROM siscom_mi.rapporti_utenza" _
            & " WHERE rapporti_utenza.id =" _
            & " soggetti_contrattuali_inizio.id_contratto) IN" _
            & " (" & queryPrincipale & ")" _
            & " UNION" _
            & " SELECT (SELECT cod_Contratto" _
            & "  FROM siscom_mi.rapporti_utenza" _
            & " WHERE rapporti_utenza.id = ospiti.id_contratto)" _
            & " AS cod_Contratto," _
            & " nominativo AS COMPONENTE," _
            & " cod_fiscale AS COD_FISCALE_P_IVA," _
            & " 'Ospite' AS ORIGINE," _
            & " '' AS TIPO_OCCUPANTE," _
            & " '' AS DATA_INIZIO," _
            & " '' AS DATA_FINE" _
            & " FROM siscom_mi.ospiti" _
            & " WHERE (SELECT cod_Contratto" _
            & " FROM siscom_mi.rapporti_utenza" _
            & " WHERE rapporti_utenza.id = ospiti.id_contratto) IN" _
            & " (" & queryPrincipale & ")" _
        & " UNION" _
        & " select RAPPORTI_UTENZA.cod_contratto," _
            & " CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS COMPONENTE," _
            & " CASE WHEN anagrafica.partita_iva is not null then partita_iva else anagrafica.COD_FISCALE end AS COD_FISCALE_P_IVA,'Contratto' as origine," _
            & " TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCCUPANTE," _
            & " to_char(to_date(data_inizio,'yyyymmdd'),'dd/mm/yyyy') AS data_inizio," _
            & " to_char(to_date(data_fine,'yyyymmdd'),'dd/mm/yyyy') as data_fine" _
            & " FROM siscom_mi.rapporti_utenza,siscom_mi.anagrafica,siscom_mi.soggetti_contrattuali,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
            & " siscom_mi.unita_immobiliari," _
            & " siscom_mi.unita_contrattuale," _
            & " siscom_mi.edifici," _
            & " siscom_mi.indirizzi" _
            & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
            & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
            & " AND unita_immobiliari.id_edificio = edifici.id" _
            & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
            & " and unita_contrattuale.id_unita_principale is null" _
            & " AND TIPOLOGIA_OCCUPANTE.COD=cod_tipologia_occupante" _
            & " and anagrafica.id=soggetti_contrattuali.id_anagrafica and soggetti_contrattuali.id_Contratto=rapporti_utenza.id " _
            & sStringaSql & " order by 1 asc"

        EstraiDati(par.cmd.CommandText, 3)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")


        'Dim daCond As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dt As New Data.DataTable
        'daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        'daCond.Fill(dt)
        'daCond.Dispose()
        'Dim codContratto As String = ""
        'Dim idContratto As String = ""
        'Dim RIGA1 As System.Data.DataRow
        'Dim RIGA2 As System.Data.DataRow
        'Dim RIGA3 As System.Data.DataRow
        'Dim RIGA4 As System.Data.DataRow
        'Dim RIGA5 As System.Data.DataRow
        'Dim RIGA6 As System.Data.DataRow
        'Dim RIGA7 As System.Data.DataRow

        'If dt.Rows.Count > 0 Then

        '    For Each rigaCodC As Data.DataRow In dt.Rows
        '        RIGA1 = dtCond.NewRow()
        '        RIGA1.Item("COMPONENTE") = rigaCodC.Item("componente")
        '        RIGA1.Item("COD_FISCALE_P_IVA") = rigaCodC.Item("COD_FISCALE_P_IVA")
        '        RIGA1.Item("ORIGINE") = "Contratto"
        '        RIGA1.Item("TIPO_OCCUPANTE") = rigaCodC.Item("TIPO_OCCUPANTE")
        '        RIGA1.Item("DATA_INIZIO") = rigaCodC.Item("DATA_INIZIO")
        '        RIGA1.Item("DATA_FINE") = rigaCodC.Item("DATA_FINE")
        '        dtCond.Rows.Add(RIGA1)

        '        codContratto = rigaCodC.Item("cod_contratto")
        '        RIGA1.Item("DATA_FINE") = codContratto
        '        idContratto = rigaCodC.Item("idContr")
        '        If tipoDomanda <> "" Then
        '            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,DICHIARAZIONI_VSA.ID AS IDDICH,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOM,DOMANDE_BANDO_VSA.CONTRATTO_NUM," _
        '                & " DOMANDE_BANDO_VSA.PG AS PGDOM,DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA,DOMANDE_BANDO_VSA.ID AS IDDOM,DICHIARAZIONI_VSA.DATA_INIZIO_VAL,DICHIARAZIONI_VSA.DATA_FINE_VAL" _
        '                & " FROM COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID=COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & codContratto & "' AND FL_AUTORIZZAZIONE = 1 AND ID_MOTIVO_DOMANDA IN (" & tipoDomanda & ") ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
        '            daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dt2 As New Data.DataTable
        '            daCond.Fill(dt2)
        '            daCond.Dispose()
        '            If dt2.Rows.Count > 0 Then
        '                For Each rigaVSA As Data.DataRow In dt2.Rows
        '                    RIGA2 = dtCond.NewRow()
        '                    RIGA2.Item("COMPONENTE") = rigaVSA.Item("cognome") & " " & rigaVSA.Item("nome")
        '                    RIGA2.Item("COD_FISCALE_P_IVA") = rigaVSA.Item("COD_FISCALE")
        '                    RIGA2.Item("ORIGINE") = par.IfNull(rigaVSA.Item("MOT_DOM"), "")
        '                    RIGA2.Item("TIPO_OCCUPANTE") = ""
        '                    RIGA2.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(rigaVSA.Item("DATA_INIZIO_VAL"), ""))
        '                    RIGA2.Item("DATA_FINE") = par.FormattaData(par.IfNull(rigaVSA.Item("DATA_FINE_VAL"), ""))
        '                    RIGA2.Item("cod_contratto") = par.IfNull(rigaVSA.Item("CONTRATTO_NUM"), "")
        '                    dtCond.Rows.Add(RIGA2)
        '                Next
        '            End If
        '        Else
        '            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,DICHIARAZIONI_VSA.ID AS IDDICH,DOMANDE_BANDO_VSA.CONTRATTO_NUM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOM," _
        '                & " DOMANDE_BANDO_VSA.PG AS PGDOM,DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA,DOMANDE_BANDO_VSA.ID AS IDDOM,DICHIARAZIONI_VSA.DATA_INIZIO_VAL,DICHIARAZIONI_VSA.DATA_FINE_VAL" _
        '                & " FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE DICHIARAZIONI_VSA.ID=DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND DICHIARAZIONI_VSA.ID=COMP_NUCLEO_VSA.ID_DICHIARAZIONE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND CONTRATTO_NUM='" & codContratto & "' AND FL_AUTORIZZAZIONE = 1 ORDER BY DOMANDE_BANDO_VSA.DATA_PG DESC"
        '            daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dt3 As New Data.DataTable
        '            daCond.Fill(dt3)
        '            daCond.Dispose()
        '            If dt3.Rows.Count > 0 Then
        '                For Each rigaVSA2 As Data.DataRow In dt3.Rows
        '                    RIGA3 = dtCond.NewRow()
        '                    RIGA3.Item("COMPONENTE") = rigaVSA2.Item("cognome") & " " & rigaVSA2.Item("nome")
        '                    RIGA3.Item("COD_FISCALE_P_IVA") = rigaVSA2.Item("COD_FISCALE")
        '                    RIGA3.Item("ORIGINE") = par.IfNull(rigaVSA2.Item("MOT_DOM"), "")
        '                    RIGA3.Item("TIPO_OCCUPANTE") = ""
        '                    RIGA3.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(rigaVSA2.Item("DATA_INIZIO_VAL"), ""))
        '                    RIGA3.Item("DATA_FINE") = par.FormattaData(par.IfNull(rigaVSA2.Item("DATA_FINE_VAL"), ""))
        '                    RIGA3.Item("cod_contratto") = par.IfNull(rigaVSA2.Item("CONTRATTO_NUM"), "")
        '                    dtCond.Rows.Add(RIGA3)
        '                Next
        '            End If

        '            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.*,UTENZA_COMP_NUCLEO.*,UTENZA_BANDI.DESCRIZIONE AS NOME_BANDO,UTENZA_BANDI.ANNO_ISEE," _
        '                & " UTENZA_DICHIARAZIONI.DATA_INIZIO_VAL,UTENZA_DICHIARAZIONI.DATA_FINE_VAL FROM UTENZA_DICHIARAZIONI,UTENZA_BANDI,UTENZA_COMP_NUCLEO " _
        '                & " WHERE UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID AND NVL(FL_GENERAZ_AUTO,0)=0 AND (UTENZA_DICHIARAZIONI.NOTE_WEB IS NULL OR UTENZA_DICHIARAZIONI.NOTE_WEB<>'GENERATA_AUTOMATICAMENTE') AND UTENZA_BANDI.ID = UTENZA_DICHIARAZIONI.ID_BANDO " _
        '                & " AND RAPPORTO='" & codContratto & "' ORDER BY ID_BANDO DESC"
        '            daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dt4 As New Data.DataTable
        '            daCond.Fill(dt4)
        '            daCond.Dispose()
        '            If dt4.Rows.Count > 0 Then
        '                For Each rigaAU As Data.DataRow In dt4.Rows
        '                    RIGA4 = dtCond.NewRow()
        '                    RIGA4.Item("COMPONENTE") = rigaAU.Item("cognome") & " " & rigaAU.Item("nome")
        '                    RIGA4.Item("COD_FISCALE_P_IVA") = rigaAU.Item("COD_FISCALE")
        '                    RIGA4.Item("ORIGINE") = par.IfNull(rigaAU.Item("NOME_BANDO"), "")
        '                    RIGA4.Item("TIPO_OCCUPANTE") = ""
        '                    RIGA4.Item("DATA_INIZIO") = par.FormattaData(par.IfNull(rigaAU.Item("DATA_INIZIO_VAL"), ""))
        '                    RIGA4.Item("DATA_FINE") = par.FormattaData(par.IfNull(rigaAU.Item("DATA_FINE_VAL"), ""))
        '                    RIGA4.Item("cod_contratto") = par.IfNull(rigaAU.Item("rapporto"), "")
        '                    dtCond.Rows.Add(RIGA4)
        '                Next
        '            End If

        '            par.cmd.CommandText = "SELECT DOMANDE_BANDO.*,bandi.descrizione,COMP_NUCLEO.* " _
        '                & " FROM BANDI,DOMANDE_BANDO,DICHIARAZIONI,COMP_NUCLEO WHERE COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND DICHIARAZIONI.ID=DOMANDE_BANDO.ID_DICHIARAZIONE AND BANDI.ID=DOMANDE_BANDO.ID_BANDO AND CONTRATTO_NUM='" & codContratto & "'"
        '            daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dt5 As New Data.DataTable
        '            daCond.Fill(dt5)
        '            daCond.Dispose()
        '            If dt5.Rows.Count > 0 Then
        '                For Each rigaBando As Data.DataRow In dt5.Rows
        '                    RIGA5 = dtCond.NewRow()
        '                    RIGA5.Item("COMPONENTE") = rigaBando.Item("cognome") & " " & rigaBando.Item("nome")
        '                    RIGA5.Item("COD_FISCALE_P_IVA") = rigaBando.Item("COD_FISCALE")
        '                    RIGA5.Item("ORIGINE") = par.IfNull(rigaBando.Item("DESCRIZIONE"), "")
        '                    RIGA5.Item("TIPO_OCCUPANTE") = ""
        '                    RIGA5.Item("DATA_INIZIO") = ""
        '                    RIGA5.Item("DATA_FINE") = ""
        '                    RIGA5.Item("cod_contratto") = par.IfNull(rigaBando.Item("CONTRATTO_NUM"), "")
        '                    dtCond.Rows.Add(RIGA5)
        '                Next
        '            End If

        '            par.cmd.CommandText = "SELECT anagrafica.*,(SELECT cod_Contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=soggetti_contrattuali_inizio.id_contratto) as cod_ru " _
        '                & " FROM SISCOM_MI.SOGGETTI_CONTRATTUALI_INIZIO,siscom_mi.anagrafica WHERE SOGGETTI_CONTRATTUALI_INIZIO.id_anagrafica=anagrafica.id and SOGGETTI_CONTRATTUALI_INIZIO.ID_CONTRATTO = " & idContratto
        '            daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dt6 As New Data.DataTable
        '            daCond.Fill(dt6)
        '            daCond.Dispose()
        '            If dt6.Rows.Count > 0 Then
        '                For Each rigaOrigine As Data.DataRow In dt6.Rows
        '                    RIGA6 = dtCond.NewRow()
        '                    RIGA6.Item("COMPONENTE") = rigaOrigine.Item("cognome") & " " & rigaOrigine.Item("nome")
        '                    RIGA6.Item("COD_FISCALE_P_IVA") = rigaOrigine.Item("COD_FISCALE")
        '                    RIGA6.Item("ORIGINE") = "Situazione Iniziale"
        '                    RIGA6.Item("TIPO_OCCUPANTE") = ""
        '                    RIGA6.Item("DATA_INIZIO") = ""
        '                    RIGA6.Item("DATA_FINE") = ""
        '                    RIGA6.Item("cod_contratto") = par.IfNull(rigaOrigine.Item("cod_ru"), "")
        '                    dtCond.Rows.Add(RIGA6)
        '                Next
        '            End If
        '        End If
        '        If presenzaOspiti = 1 Then
        '            par.cmd.CommandText = "SELECT ospiti.*,(SELECT cod_Contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=ospiti.id_contratto) as cod_ru " _
        '                & " from ospiti where id_contratto=" & idContratto
        '            daCond = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dt7 As New Data.DataTable
        '            daCond.Fill(dt7)
        '            daCond.Dispose()
        '            If dt7.Rows.Count > 0 Then
        '                For Each rigaOspiti As Data.DataRow In dt7.Rows
        '                    RIGA7 = dtCond.NewRow()
        '                    RIGA7.Item("COMPONENTE") = rigaOspiti.Item("cognome") & " " & rigaOspiti.Item("nome")
        '                    RIGA7.Item("COD_FISCALE_P_IVA") = rigaOspiti.Item("COD_FISCALE")
        '                    RIGA7.Item("ORIGINE") = "Ospite"
        '                    RIGA7.Item("TIPO_OCCUPANTE") = ""
        '                    RIGA7.Item("DATA_INIZIO") = ""
        '                    RIGA7.Item("DATA_FINE") = ""
        '                    RIGA7.Item("cod_contratto") = par.IfNull(rigaOspiti.Item("cod_ru"), "")
        '                    dtCond.Rows.Add(RIGA7)
        '                Next
        '            End If
        '        End If
        '    Next

        'If dtCond.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportConduttore", "ExportConduttore", dtCond)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If
        'Else
        'par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If

    End Sub

    Private Sub EstrazioneCanone(ByVal sStringaSql As String)
        Dim dicDt As New Generic.Dictionary(Of String, Data.DataTable)

        Dim idAreaEcon As Integer = cmbAreaEcon.SelectedValue
        Dim classeEcon As String = cmbClasse.SelectedItem.Text
        Dim inizioValDal As String = txtInizioValDal.Text
        Dim inizioValAl As String = txtInizioValAl.Text
        Dim fineValDal As String = txtFineValDal.Text
        Dim fineValAl As String = txtFineValAl.Text
        Dim tipoProven As Integer = cmbProvenienza.SelectedValue

        Dim svalore As String = ""
        Dim bTrovato As Boolean


        Dim stringaCondizioni As String = ""
        Dim dtPgDich As New Data.DataTable
        Dim elencoDich As String = ""

        If FileUploadDich.FileName <> "" Then
            dtPgDich = LeggiDaXlsDich()

            If dtPgDich.Rows.Count > 0 Then
                If dtPgDich.Rows.Count >= 1000 Then
                    par.modalDialogMessage("Attenzione", "Caricare al massimo 1000 elementi!", Me.Page)
                    Exit Sub
                End If
                For i As Integer = 0 To dtPgDich.Rows.Count - 1
                    elencoDich = elencoDich & dtPgDich.Rows(i).Item(0) & ","
                Next
            Else
                par.modalDialogMessage("Attenzione", "Impossibile importare i dati! Verificare gli elementi presenti nel file.", Me.Page)
                Exit Sub
            End If
        End If

        If sStringaSql <> "" Then bTrovato = True

        If idAreaEcon <> -1 Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = idAreaEcon
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.ID_area_economica in (" & svalore & ") "
        End If

        If classeEcon <> "- - -" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = classeEcon
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.sotto_area in ('" & svalore & "') "
        End If

        If tipoProven <> -1 Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = tipoProven
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.TIPO_PROVENIENZA in (" & svalore & ") "
        End If

        If inizioValDal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = inizioValDal
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.INIZIO_VALIDITA_CAN>='" & par.AggiustaData(svalore) & "' "
        End If

        If inizioValAl <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = inizioValAl
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.INIZIO_VALIDITA_CAN<='" & par.AggiustaData(svalore) & "' "
        End If

        If fineValDal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = fineValDal
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.FINE_VALIDITA_CAN>='" & par.AggiustaData(svalore) & "' "
        End If

        If fineValAl <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = fineValAl
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.FINE_VALIDITA_CAN<='" & par.AggiustaData(svalore) & "' "
        End If


        If elencoDich <> "" Then
            sStringaSql = ""

            elencoDich = "(" & Mid(elencoDich, 1, Len(elencoDich) - 1) & ")"
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = elencoDich
            bTrovato = True
            sStringaSql = sStringaSql & " CANONI_EC.ID_DICHIARAZIONE IN " & svalore & " and TIPO_PROVENIENZA=1"
        End If

        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql


        If cmbDettagliCanone.SelectedValue = "0" Then

            par.cmd.CommandText = "select ''''||rapporti_utenza.cod_contratto as cod_contratto,round(imp_canone_iniziale,2) as imp_canone_iniziale,(nvl((SELECT (SUM(round(nvl(IMPORTO,0),2))) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID),0)+ round(nvl(imp_canone_iniziale,0),2)) as canone_corrente,round(imp_deposito_cauz,2) as imp_deposito_cauz,LIBRETTO_DEPOSITO,IMPORTO_ANTICIPO," _
                & "(CASE WHEN NRO_RATE ='12' THEN 'Mensile' WHEN NRO_RATE ='4' THEN 'Trimestrale' WHEN NRO_RATE ='2' then 'Semestrale' WHEN NRO_RATE ='1' then 'Annuale' end) as frequenza_canone," _
                & "to_char(to_date(DATA_REST_DEP,'yyyymmdd'),'dd/mm/yyyy') as DATA_REST_DEP,(CASE WHEN ID_DEST_RATE ='1' THEN 'Inquilino' else 'Studio Amministratore' end) as destinazione_rate," _
                & "(CASE WHEN INTERESSI_CAUZIONE ='0' THEN 'NO' else 'SI' END) as INTERESSI_CAUZIONE,(CASE WHEN FL_CONGUAGLIO ='0' THEN 'NO' else 'SI' END) as CONGUAGLIO_bollette," _
                & "(CASE WHEN INVIO_BOLLETTA ='0' THEN 'NO' else 'SI' END) AS INVIO_BOLLETTA,(CASE WHEN interessi_rit_pag ='0' THEN 'NO' else 'SI' END) AS interessi_ritardo_pag,(CASE WHEN FL_INTERESSI_C ='0' THEN 'NO' else 'SI' END) AS interessi_cauz_dopo_chiusura" _
                & " FROM siscom_mi.rapporti_utenza,SISCOM_MI.CANONI_EC," _
                & " siscom_mi.unita_immobiliari," _
                & " siscom_mi.unita_contrattuale," _
                & " siscom_mi.edifici," _
                & " siscom_mi.indirizzi" _
                & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
                & " AND CANONI_EC.ID_CONTRATTO(+)=RAPPORTI_UTENZA.ID " _
                & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
                & " AND unita_immobiliari.id_edificio = edifici.id" _
                & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
                & " and unita_contrattuale.id_unita_principale is null " & sStringaSql & " order by imp_canone_iniziale asc"

            EstraiDati(par.cmd.CommandText, 4)
            par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        End If

        If cmbDettagliCanone.SelectedValue = "1" Then
            par.cmd.CommandText = "SELECT ''''||CANONI_EC.COD_CONTRATTO AS RU," _
                & " TO_CHAR (TO_DATE (SUBSTR (canoni_ec.data_calcolo, 1, 8), 'yyyymmdd'),  'dd/mm/yyyy') AS data_calcolo, SUBSTR (canoni_ec.data_calcolo, 9, 2)||':'||SUBSTR (canoni_ec.data_calcolo, 11, 2) as ORA_CALCOLO," _
                   & " DECODE (ID_AREA_ECONOMICA," _
                   & "   1, 'PROTEZIONE'," _
                   & "   2, 'ACCESSO', " _
                   & "   3, 'PERMANENZA'," _
                   & "   4, 'DECADENZA')" _
                & "  AS AREA_ECONOMICA," _
                & " (SELECT descrizione from T_TIPO_PROVENIENZA where T_TIPO_PROVENIENZA.id=CANONI_EC.TIPO_PROVENIENZA) as PROVENIENZA," _
                & " SOTTO_AREA AS classe," _
                & "  CANONI_EC.ISEE," _
                & "  CANONI_EC.ISE," _
                & "  CANONI_EC.ISR," _
                & "  CANONI_EC.ISP," _
                & "  CANONI_EC.PSE," _
                & "  CANONI_EC.VSE," _
                & "  REDDITI_DIP," _
                & "  REDDITI_ATRI," _
                & "  LIMITE_PENSIONI," _
                & "  ISEE_27," _
                & "  PERC_VAL_LOC," _
                & "  INC_MAX," _
                & "  INCIDENZA_ISE," _
                & "  CANONE AS CANONE_ANNUO_DA_APPLICARE," _
                & "  CANONI_eC.NOTE," _
                & "  DEM," _
                & "  SUPCONVENZIONALE," _
                & "  COSTOBASE," _
                & "  ZONA," _
                & "  PIANO," _
                & "  CONSERVAZIONE," _
                & "  VETUSTA," _
                & "  COEFF_NUCLEO_FAM," _
                & "  ANNOTAZIONI," _
                & "  PATRIMONIO_SUP," _
                & "  NON_RISPONDENTE," _
                & "  LIMITE_ISEE," _
                & "  CANONE_ATTUALE," _
                & "  ADEGUAMENTO," _
                & "  ISTAT," _
                & "  NUM_COMP," _
                & "  NUM_COMP_66," _
                & "  NUM_COMP_100," _
                & "  NUM_COMP_100_CON," _
                & "  REDD_PREV_DIP," _
                & "  DETRAZIONI," _
                & "  DETRAZIONI_FRAGILITA," _
                & "  REDD_MOBILIARI," _
                & "  REDD_IMMOBILIARI," _
                & "  REDD_COMPLESSIVO," _
                & "  ANNO_COSTRUZIONE," _
                & "  CANONI_EC.LOCALITA," _
                & "  NUMERO_PIANO," _
                & "  PRESENTE_ASCENSORE," _
                & "  SUP_NETTA," _
                & "  ALTRE_SUP," _
                & "  MINORI_15," _
                & "  MAGGIORI_65," _
                & "  SUP_ACCESSORI," _
                & "  VALORE_LOCATIVO," _
                & "  CANONE_MINIMO_AREA," _
                & "  DECADENZA_ALL_ADEGUATO," _
                & "  DECADENZA_VAL_ICI," _
                & "  CANONE_CLASSE," _
                & "  CANONE_SOPPORTABILE," _
                & "  PERC_ISTAT_APPLICATA," _
                & "  CANONE_CLASSE_ISTAT," _
                & "  CANONI_EC.SCONTO_COSTO_BASE," _
                & "  CANONE_1243_12," _
                & "  DELTA_1243_12," _
                & "  ESCLUSIONE_1243_12," _
                & "  TIPO_CANONE_APP," _
                & "  getdata (INIZIO_VALIDITA_CAN) AS inizio_Validita_Canone," _
                & "  getdata (FINE_VALIDITA_CAN) AS fine_Validita_Canone," _
                & "  COMPETENZA," _
                & " (SELECT PG from DOMANDE_BANDO_VSA where DOMANDE_BANDO_VSA.id_DICHIARAZIONE=CANONI_EC.ID_DICHIARAZIONE) as PG_DOM_LOCA" _
                & " FROM SISCOM_MI.CANONI_EC," _
                & " siscom_mi.rapporti_utenza," _
                & " siscom_mi.unita_immobiliari," _
                & " siscom_mi.unita_contrattuale," _
                & " siscom_mi.edifici," _
                & " siscom_mi.indirizzi" _
                & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
                & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
                & " AND unita_immobiliari.id_edificio = edifici.id" _
                & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
                & " and unita_contrattuale.id_unita_principale is null " _
                & " and canoni_Ec.id_contratto=rapporti_utenza.id " _
            & sStringaSql & " order by rapporti_utenza.cod_contratto asc,FINE_VALIDITA_CAN asc"

            EstraiDati(par.cmd.CommandText, 4)
            par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        End If

    End Sub

    Private Sub EstrazioneRegistr(ByVal sStringaSql As String)
        Dim codUffReg As String = cmbUffReg.SelectedValue
        Dim svalore As String = ""
        Dim bTrovato As Boolean = False
        If sStringaSql <> "" Then bTrovato = True

        If codUffReg <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = codUffReg
            bTrovato = True
            sStringaSql = sStringaSql & " rapporti_utenza.cod_ufficio_reg='" & svalore & "' "
        End If

        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        par.cmd.CommandText = "select ''''||cod_contratto as cod_contratto,cod_ufficio_reg,serie_registrazione,num_registrazione," _
            & "to_char(to_date(data_reg,'yyyymmdd'),'dd/mm/yyyy') as data_registrazione,nro_repertorio," _
            & "to_char(to_date(data_repertorio,'yyyymmdd'),'dd/mm/yyyy') as data_repertorio,nro_assegnazione_pg," _
            & "to_char(to_date(data_assegnazione_pg,'yyyymmdd'),'dd/mm/yyyy') as data_assegnazione_pg," _
            & "(case when versamento_tr ='a' then 'Annuale' else 'Unica' end) as modalita_registrazione," _
            & "(select descrizione from siscom_mi.tipologia_posizione where tipologia_posizione.id=rapporti_utenza.id_tipo_posizione) as tipologia_posizione," _
            & "(select descrizione from siscom_mi.tipologia_pagamento where tipologia_pagamento.id=rapporti_utenza.id_tipo_pagamento) as tipologia_pagamento,note_registrazione," _
            & "(select perc_tr_canone from siscom_mi.tipologia_contratto_locazione where tipologia_contratto_locazione.cod=rapporti_utenza.cod_tipologia_contr_loc) as perc_tr_canone," _
            & "(select perc_conduttore from siscom_mi.tipologia_contratto_locazione where tipologia_contratto_locazione.cod=rapporti_utenza.cod_tipologia_contr_loc) as perc_conduttore," _
            & "(select perc_conduttore from siscom_mi.tipologia_contratto_locazione where tipologia_contratto_locazione.cod=rapporti_utenza.cod_tipologia_contr_loc) as perc_locatore," _
            & "0 as tassa_registrazione_tot,0 as tassa_registr_cond,0 as tassa_registr_loc" _
            & " FROM siscom_mi.rapporti_utenza," _
            & " siscom_mi.unita_immobiliari," _
            & " siscom_mi.unita_contrattuale," _
            & " siscom_mi.edifici," _
            & " siscom_mi.indirizzi" _
            & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
            & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
            & " AND unita_immobiliari.id_edificio = edifici.id" _
            & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
            & " and unita_contrattuale.id_unita_principale is null " _
        & sStringaSql & " order by data_registrazione asc"

        EstraiDati(par.cmd.CommandText, 5)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")


        'Dim daReg As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dtReg As New Data.DataTable
        'daReg = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        'daReg.Fill(dtReg)
        'daReg.Dispose()

        'If dtReg.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportRegistraz", "ExportRegistraz", dtReg)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If

    End Sub

    Private Sub EstrazioneSchemaBoll(ByVal sStringaSql As String)

        Dim sindacato As Integer = cmbSindacato.SelectedValue
        Dim tipoSchema As Integer = rdbListAnnoSchema.SelectedValue
        Dim annoRiferimento As String = txtAnnoRiferim.Text

        Dim svalore As String = ""
        Dim bTrovato As Boolean = False
        If sStringaSql <> "" Then bTrovato = True

        If sindacato <> -1 Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = sindacato
            bTrovato = True
            sStringaSql = sStringaSql & " rapporti_utenza.sindacato=" & svalore & " "
        End If

        If tipoSchema = 1 Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = sindacato
            bTrovato = True
            sStringaSql = sStringaSql & " bol_schema.anno=" & Year(Now) & " "
        Else
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            svalore = sindacato
            bTrovato = True
            sStringaSql = sStringaSql & " bol_schema.anno=" & CInt(txtAnnoRiferim.Text) & " "
        End If

        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        par.cmd.CommandText = "select " _
        & "''''||cod_contratto as cod_contratto,SISCOM_MI.Getintestatari(RAPPORTI_UTENZA.ID) AS INTESTATARIO,TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_DECORRENZA," _
        & "TO_CHAR(TO_DATE(RAPPORTI_UTENZA.DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_SLOGGIO, " _
& "RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPO_CONTRATTO," _
& "ROUND(RAPPORTI_UTENZA.IMP_CANONE_INIZIALE,2) AS CANONE_ANNUALE,(SELECT (SUM(round(IMPORTO,2))) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS ISTAT," _
& "(select to_char(to_date(prossima_bolletta,'yyyymm'),'mm/yyyy') from siscom_mi.rapporti_utenza_prossima_bol where rapporti_utenza_prossima_bol.id_contratto=rapporti_utenza.id) as prossima_boll," _
    & "(select descrizione from sindacati_vsa where sindacati_vsa.id=rapporti_utenza.sindacato) as sindacato," _
    & "(select descrizione from siscom_mi.t_voci_bolletta where t_voci_bolletta.id=bol_schema.id_voce) as voce,round(importo_singola_rata,2) as imp,da_rata,per_rate,anno " _
    & " FROM siscom_mi.rapporti_utenza," _
    & " siscom_mi.unita_immobiliari," _
    & " siscom_mi.unita_contrattuale," _
    & " siscom_mi.edifici," _
    & " siscom_mi.indirizzi,siscom_mi.bol_schema" _
    & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
    & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
    & " AND unita_immobiliari.id_edificio = edifici.id" _
    & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
    & " and unita_contrattuale.id_unita_principale is null" _
    & " AND rapporti_utenza.id=bol_schema.id_contratto " _
    & sStringaSql & " order by cod_contratto asc" _
    '& " UNION " _
        '& "SELECT cod_contratto,(SELECT TO_CHAR (TO_DATE (prossima_bolletta, 'yyyymm'), 'mm/yyyy')FROM siscom_mi.rapporti_utenza_prossima_bol WHERE rapporti_utenza_prossima_bol.id_contratto = rapporti_utenza.id) AS prossima_boll," _
        '& "(SELECT descrizione FROM sindacati_vsa WHERE sindacati_vsa.id = rapporti_utenza.sindacato) AS sindacato,'Canone/Indennità' AS voce,imp_canone_iniziale as imp,1 as  da_rata,12 as per_rate, anno" _
        '& " FROM siscom_mi.rapporti_utenza," _
        '& " siscom_mi.unita_immobiliari," _
        '& " siscom_mi.unita_contrattuale," _
        '& " siscom_mi.edifici," _
        '& " siscom_mi.indirizzi,siscom_mi.bol_schema" _
        '& " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
        '& " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
        '& " AND unita_immobiliari.id_edificio = edifici.id" _
        '& " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
        '& " and unita_contrattuale.id_unita_principale is null" _
        '& " AND rapporti_utenza.id=bol_schema.id_contratto " & sStringaSql
        'Dim daSchemaBoll As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dtSchemaBoll As New Data.DataTable
        'daSchemaBoll = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        EstraiDati(par.cmd.CommandText, 6)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        'daSchemaBoll.Fill(dtSchemaBoll)
        'daSchemaBoll.Dispose()

        'If dtSchemaBoll.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSchemaBoll", "ExportSchemaBoll", dtSchemaBoll)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If

    End Sub

    Private Sub EstraiDati(ByVal strQuery As String, ByVal idTipoReport As Integer)
        Try
            Dim sComando As String = strQuery
            connData.apri()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = par.cmd.ExecuteScalar

            If Len(strQuery) < 4000 Then

                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE, Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'" & strQuery.ToString.Replace("'", "''") & "', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,NULL)"
            Else
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE,Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,:TEXT_DATA)"


                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = strQuery
                End With

                par.cmd.Parameters.Add(paramData)
                paramData = Nothing


            End If
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
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
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU -  EstraiDati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    'Public Sub WriteLOBData()
    '    Dim connection As New OracleConnection(connectionstring)
    '    connection.Open()

    '    Dim strSQL As String = "INSERT INTO TestCLOB (ID,CLOBTEXTFIELD) VALUES (1,:TEXT_DATA) "
    '    'Dim strsql As String = "UPDATE TestCLOB SET CLOBTEXTFIELD=:TEXTDATA where testid=1"
    '    Dim paramData As New OracleParameter
    '    paramData.Direction = ParameterDirection.Input
    '    paramData.OracleDbType = OracleDbType.Clob
    '    paramData.ParameterName = "TEXT_DATA"
    '    paramData.Value = txtInput.Text

    '    Dim cmd As New OracleCommand
    '    cmd.Connection = connection
    '    cmd.Parameters.Add(paramData)
    '    cmd.CommandText = strSQL
    '    cmd.ExecuteNonQuery()

    '    paramData = Nothing
    '    cmd = Nothing
    '    connection.Close()
    'End Sub

    Private Sub EstrazioneInteressi(ByVal sStringaSql As String)
        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        'par.cmd.CommandText = "select ''''||rapporti_utenza.cod_contratto as cod_contratto,presso_cor as presso,tipo_cor||' '|| via_cor ||', '||civico_cor as indirizzo, cap_cor as cap,sigla_cor as sigla, (select descrizione from siscom_mi.tab_commissariati where tab_commissariati.id=id_commissariato) as commiss " _
        '    & " ,RAPPORTI_UTENZA.SCALA_COR AS SCALA_RECAPITO,RAPPORTI_UTENZA.PIANO_COR AS PIANO_RECAPITO,INTERNO_COR AS INTERNO_RECAPITO " _
        '    & " FROM siscom_mi.rapporti_utenza," _
        '    & " siscom_mi.unita_immobiliari," _
        '    & " siscom_mi.unita_contrattuale," _
        '    & " siscom_mi.edifici," _
        '    & " siscom_mi.indirizzi" _
        '    & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
        '    & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
        '    & " AND unita_immobiliari.id_edificio = edifici.id" _
        '    & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
        '    & " and unita_contrattuale.id_unita_principale is null " & sStringaSql & " order by cod_contratto asc,presso asc,indirizzo asc"

        par.cmd.CommandText = "select distinct rapporti_utenza.cod_contratto,  " _
                            & "rapporti_utenza.cod_tipologia_contr_loc, " _
                            & "siscom_mi.getdata(rapporti_utenza.data_decorrenza) data_decorrenza, " _
                            & "tab_provenienza_dep.descrizione provenienza_dep, " _
                            & "rapporti_utenza.imp_deposito_cauz, " _
                            & "adeguamento_interessi.importo int_totali " _
                            & "from siscom_mi.rapporti_utenza, " _
                            & "siscom_mi.adeguamento_interessi, " _
                            & " siscom_mi.storico_dep_cauzionale, " _
                            & "siscom_mi.rapporti_utenza_dep_prov, " _
                            & "siscom_mi.tab_provenienza_dep, " _
                            & "siscom_mi.unita_immobiliari, " _
                            & "siscom_mi.unita_contrattuale, " _
                            & "siscom_mi.edifici, " _
                            & "siscom_mi.indirizzi " _
                            & "where adeguamento_interessi.id_contratto = rapporti_utenza.id " _
                            & " and adeguamento_interessi.id_anagrafica = storico_dep_cauzionale.id_anagrafica " _
                            & " and rapporti_utenza.id = storico_dep_cauzionale.id_contratto " _
                            & " And storico_dep_cauzionale.id = rapporti_utenza_dep_prov.id_storico_dep " _
                            & " and rapporti_utenza_dep_prov.provenienza = tab_provenienza_dep.id " _
                            & " And nvl(adeguamento_interessi.fl_applicato, 2) = 0  " _
                            & " and rapporti_utenza.id = unita_contrattuale.id_contratto  " _
                            & " And unita_immobiliari.id = unita_contrattuale.id_unita " _
                            & " AND unita_immobiliari.id_edificio = edifici.id " _
                            & " And unita_immobiliari.id_indirizzo = indirizzi.id(+) " _
                            & " and unita_contrattuale.id_unita_principale is null " & sStringaSql & " order by cod_contratto asc"

        EstraiDati(par.cmd.CommandText, 16)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        'Dim daCom As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dtCom As New Data.DataTable
        'daCom = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        'daCom.Fill(dtCom)
        'daCom.Dispose()

        'If dtCom.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportComunicaz", "ExportComunicaz", dtCom)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If

    End Sub


    Private Sub EstrazioneComunicaz(ByVal sStringaSql As String)
        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        par.cmd.CommandText = "select ''''||rapporti_utenza.cod_contratto as cod_contratto,presso_cor as presso,tipo_cor||' '|| via_cor ||', '||civico_cor as indirizzo, cap_cor as cap,sigla_cor as sigla, (select descrizione from siscom_mi.tab_commissariati where tab_commissariati.id=id_commissariato) as commiss " _
            & " ,RAPPORTI_UTENZA.SCALA_COR AS SCALA_RECAPITO,RAPPORTI_UTENZA.PIANO_COR AS PIANO_RECAPITO,INTERNO_COR AS INTERNO_RECAPITO " _
            & " FROM siscom_mi.rapporti_utenza," _
            & " siscom_mi.unita_immobiliari," _
            & " siscom_mi.unita_contrattuale," _
            & " siscom_mi.edifici," _
            & " siscom_mi.indirizzi" _
            & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
            & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
            & " AND unita_immobiliari.id_edificio = edifici.id" _
            & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
            & " and unita_contrattuale.id_unita_principale is null " & sStringaSql & " order by cod_contratto asc,presso asc,indirizzo asc"

        EstraiDati(par.cmd.CommandText, 9)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        'Dim daCom As Oracle.DataAccess.Client.OracleDataAdapter
        'Dim dtCom As New Data.DataTable
        'daCom = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

        'daCom.Fill(dtCom)
        'daCom.Dispose()

        'If dtCom.Rows.Count > 0 Then
        '    Dim xls As New ExcelSiSol
        '    Dim nomeFile As String = ""
        '    nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportComunicaz", "ExportComunicaz", dtCom)

        '    If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '        File.Move(Server.MapPath("..\FileTemp\") & nomeFile, Server.MapPath("..\ALLEGATI\CONTRATTI\") & nomeFile)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/CONTRATTI/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        '    Else
        '        par.modalDialogMessage("Attenzione", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page)
        '    End If
        'Else
        '    par.modalDialogMessage("Attenzione", "Nessun risultato corrispondente ai criteri di ricerca inseriti. Riprovare!", Me.Page)
        'End If

    End Sub

    Private Sub EstrazioneContab(ByVal sStringaSql As String)
        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql
        Dim dicDt As New Generic.Dictionary(Of String, Data.DataTable)

        If cmbEstrattoConto.SelectedValue = "2" Then
            par.cmd.CommandText = " select ''''||rapporti_utenza.cod_contratto as cod_contratto,bol_bollette_gest.id as NUM_BOLLETTA,(select TIPO_BOLLETTE_GEST.ACRONIMO from siscom_mi.TIPO_BOLLETTE_GEST where BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID) as acronimo," _
                & " to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy') as periodo_dal,to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy') as periodo_al," _
                & " to_char(to_date(data_emissione,'yyyymmdd'),'dd/mm/yyyy') as data_emissione,round(importo_totale,2) as importo_totale," _
                & " (CASE WHEN bol_bollette_gest.tipo_applicazione ='N' THEN 'Nessuna' when bol_bollette_gest.tipo_applicazione ='P' THEN 'Parziale' when bol_bollette_gest.tipo_applicazione ='T' THEN 'Totale' END) as elaborazione," _
                & " to_char(to_date(data_applicazione,'yyyymmdd'),'dd/mm/yyyy') as data_applicazione," _
                & " BOL_BOLLETTE_GEST.note" _
                & " FROM siscom_mi.rapporti_utenza," _
                & " siscom_mi.unita_immobiliari," _
                & " siscom_mi.unita_contrattuale," _
                & " siscom_mi.edifici," _
                & " siscom_mi.indirizzi,siscom_mi.bol_bollette_gest" _
                & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
                & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
                & " AND unita_immobiliari.id_edificio = edifici.id" _
                & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
                & " and unita_contrattuale.id_unita_principale is null" _
                & " and bol_bollette_gest.id_tipo in (select id from siscom_mi.tipo_bollette_gest where fl_visualizzabile=1)" _
                & " AND rapporti_utenza.id=bol_bollette_gest.id_contratto " _
                & sStringaSql & " order by bol_bollette_gest.data_emissione desc," _
                & "bol_bollette_gest.riferimento_da desc, bol_bollette_gest.riferimento_a desc,bol_bollette_gest.id desc"

            EstraiDati(par.cmd.CommandText, 8)
            par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        End If


        If cmbEstrattoConto.SelectedValue = "1" Then
            par.cmd.CommandText = " select ''''||rapporti_utenza.cod_contratto as cod_contratto,BOL_BOLLETTE.NUM_BOLLETTA,(CASE WHEN bol_bollette.n_rata =99 THEN 'MA' when  bol_bollette.n_rata =999 THEN 'AU' when bol_bollette.N_RATA =99999 THEN 'CO' ELSE to_char(n_Rata) END)  || ' ' || (CASE WHEN nvl(FL_ANNULLATA,0) <>0 THEN 'ANNUL.' when  nvl(id_bolletta_ric,0) <>0 or nvl(ID_RATEIZZAZIONE,0) <>0  THEN 'RICLA.' when nvl(ID_bolletta_Storno,0) <>0 THEN 'STORN' ELSE 'VALIDA'  END) ||' ' || (select TIPO_BOLLETTE.ACRONIMO from siscom_mi.TIPO_BOLLETTE where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID)  as n_tipo," _
                & " to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy') as periodo_dal,to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy') as periodo_al," _
                & " to_char(to_date(data_emissione,'yyyymmdd'),'dd/mm/yyyy') as data_emissione," _
                & " to_char(to_date(bol_bollette.data_scadenza,'yyyymmdd'),'dd/mm/yyyy') as data_scadenza," _
                & " round(nvl(importo_totale,0)-nvl(importo_ric_b,0)-nvl(quota_sind_b,0),2) as importo_contabile," _
                & " round(importo_totale,2) as importo_emesso," _
                & " round(importo_totale,2) as importo_totale," _
                & " round(NVL(importo_pagato,0),2) AS IMPorto_PAGATO," _
                & " /*((nvl(importo_totale,0)-nvl(importo_ric_b,0)-nvl(quota_sind_b,0)) -NVL(importo_pagato,0)) as residuo_contabile,*/ " _
                & " round((nvl(importo_totale,0)-nvl(importo_ric_b,0)-nvl(quota_sind_b,0)) -(NVL(importo_pagato,0)-nvl(IMPORTO_RIC_PAGATO_B,0)-nvl(QUOTA_SIND_PAGATA_B,0)),2) as residuo_contabile," _
                & " to_char(to_date(data_pagamento,'yyyymmdd'),'dd/mm/yyyy') as data_pagamento," _
                & " round(importo_ruolo,2) as importo_ruolo," _
				& " round(nvl(importo_ingiunzione,0),2) as importo_ingiunzione," _
                & " (CASE WHEN nvl(ID_RATEIZZAZIONE,0) =0 THEN 'NO' else 'SI' END) as rateizzaTA," _
                & " (CASE WHEN nvl(id_morosita,0) =0 and nvl(id_bolletta_ric,0) =0 THEN 'NO' else 'SI' END) as morosita," _
                & " (CASE WHEN nvl(ID_bolletta_Storno,0) =0 THEN 'NO' else 'SI' END) as stornata," _
                & " bol_bollette.note" _
                & " FROM siscom_mi.rapporti_utenza," _
                & " siscom_mi.unita_immobiliari," _
                & " siscom_mi.unita_contrattuale," _
                & " siscom_mi.edifici," _
                & " siscom_mi.indirizzi,siscom_mi.bol_bollette" _
                & " WHERE rapporti_utenza.id = unita_contrattuale.id_contratto" _
                & " AND unita_immobiliari.id = unita_contrattuale.id_unita" _
                & " AND unita_immobiliari.id_edificio = edifici.id" _
                & " AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
                & " and unita_contrattuale.id_unita_principale is null" _
                & " AND rapporti_utenza.id=bol_bollette.id_contratto " _
                & sStringaSql & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.riferimento_da desc,BOL_BOLLETTE.riferimento_a desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc "

            EstraiDati(par.cmd.CommandText, 7)
            par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        End If


    End Sub

    Private Sub EstrazioneSaldo(ByVal sStringaSql As String)
        If sStringaSql <> "" Then sStringaSql = "AND " & sStringaSql

        If (txtDataAl.Text = "" Or txtDataAl.Text = Format(Now, "dd/MM/yyyy")) And (txtDataPagamAl.Text = "" Or txtDataPagamAl.Text = Format(Now, "dd/MM/yyyy")) Then
            par.cmd.CommandText = " SELECT ''''||rapporti_utenza.cod_contratto as cod_contratto," _
                    & "SUBSTR (riferimento_da, 1, 4) AS anno," _
                    & "SUBSTR (riferimento_da, 5, 2) AS mese," _
                    & "SUM (round(importo_totale,2)) AS tot_emesso," _
                    & "SUM (round(nvl(importo_pagato,0),2)) AS tot_incassato," _
                    & "round(SUM (importo_totale - NVL (importo_ric_b, 0) - NVL (quota_sind_b, 0))" _
                    & "- (SUM (" _
                    & "        NVL (importo_pagato, 0)" _
                    & "      - NVL (quota_sind_pagata_b, 0)" _
                    & "      - NVL (importo_ric_pagato_b, 0))),2)" _
                    & "   AS saldo_contab_al_" & Format(Now, "ddMMyyyy") _
                    & " FROM siscom_mi.rapporti_utenza," _
                    & "   siscom_mi.unita_immobiliari," _
                    & "   siscom_mi.unita_contrattuale," _
                    & "    siscom_mi.edifici," _
                    & "   siscom_mi.indirizzi," _
                    & "   siscom_mi.bol_bollette" _
                    & " WHERE     rapporti_utenza.id = unita_contrattuale.id_contratto" _
                    & "      AND unita_immobiliari.id = unita_contrattuale.id_unita" _
                    & "     AND unita_immobiliari.id_edificio = edifici.id" _
                    & "     AND unita_immobiliari.id_indirizzo = indirizzi.id(+)" _
                    & "     AND unita_contrattuale.id_unita_principale IS NULL" _
                    & "    AND BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID" _
                    & "      AND (FL_ANNULLATA = 0" _
                    & "         OR (FL_ANNULLATA = 1 AND IMPORTO_PAGATO IS NOT NULL))" _
                    & "    AND NVL (IMPORTO_RUOLO, 0) = 0" _
                    & "    AND ID_BOLLETTA_STORNO IS NULL" _
                    & "     AND ID_BOLLETTA_RIC IS NULL" _
                    & "   AND ID_RATEIZZAZIONE IS NULL" _
                    & "   AND ID_TIPO <> 22 " _
                    & sStringaSql _
                    & " GROUP BY rapporti_utenza.COD_CONTRATTO," _
                    & "  SUBSTR (riferimento_da, 1, 4)," _
                    & "   SUBSTR (riferimento_da, 5, 2)"
            EstraiDati(par.cmd.CommandText, 10)
            par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

        Else

            QuerySaldiBBVP(sStringaSql)

        End If

    End Sub

    Private Sub QuerySaldiBBVP(ByVal sStringaSql As String)
        Dim ruolo As String = ""
        Dim ruoloPag As String = ""

        If cmbRuolo.SelectedValue = 0 Then
            ruolo = " - NVL(IMPORTO_RUOLO, 0)"
            ruoloPag = "- NVL (IMP_RUOLO_PAGATO, 0) "
        Else
            ruolo = ""
        End If
        If txtDataPagamAl.Text = "" And txtDataAl.Text <> "" Then


            par.cmd.CommandText = " select ''''||cod_contratto as cod_contratto,  sum(tot_emesso) as tot_emesso,sum(tot_incassato) as tot_incassato,SUM (tot_emesso - tot_incassato) AS saldo_contab_al_" & Replace(txtDataAl.Text, "/", "") & " from (  " _
                        & " Select rapporti_utenza.cod_contratto, " _
                        & " SUM (round(IMPORTO_TOTALE -NVL(IMPORTO_RIC_B, 0) - NVL(QUOTA_SIND_B, 0) " & ruolo & ",2)) AS tot_emesso, " _
                        & "SUM (round(nvl(importo_pagato,0)- NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0),2)) AS tot_incassato " _
                        & " FROM siscom_mi.rapporti_utenza,   siscom_mi.unita_immobiliari,   siscom_mi.unita_contrattuale,    siscom_mi.edifici,   siscom_mi.indirizzi,   siscom_mi.bol_bollette WHERE     rapporti_utenza.id = unita_contrattuale.id_contratto  " _
                        & " and data_emissione<= '" & par.AggiustaData(txtDataAl.Text) & "'  " _
                        & " And unita_immobiliari.id = unita_contrattuale.id_unita     And unita_immobiliari.id_edificio = edifici.id     And unita_immobiliari.id_indirizzo = indirizzi.id(+)     And unita_contrattuale.id_unita_principale Is NULL     " _
                        & " And BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID      And (FL_ANNULLATA = 0         Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL)) " _
                        & sStringaSql _
                        & " GROUP BY rapporti_utenza.COD_CONTRATTO,BOL_BOLLETTE.ID,BOL_BOLLETTE.ID_contratto,rapporti_utenza.id) " _
                        & " group by cod_contratto "
        Else

            par.cmd.CommandText = " select ''''||cod_contratto as cod_contratto,  sum(tot_emesso) as tot_emesso,sum(tot_incassato) as tot_incassato,SUM (tot_emesso - tot_incassato) AS saldo_contab_al_" & Replace(txtDataAl.Text, "/", "") & " from (  " _
                       & " Select rapporti_utenza.cod_contratto, " _
                       & " SUM (round(IMPORTO_TOTALE -NVL(IMPORTO_RIC_B, 0) - NVL(QUOTA_SIND_B, 0) " & ruolo & ",2)) AS tot_emesso, " _
                       & " round(NVL ( SUM ( IMPORTO_PAGATO - nvl((SELECT SUM (imp_pagato) FROM siscom_mi.bol_bollette_voci WHERE bol_bollette_voci.id_bolletta = bol_bollette.id AND " _
                       & " (id_voce IN (150, 151, 677, 676) OR id_voce IN (SELECT id FROM siscom_mi.t_voci_bolletta WHERE gruppo = 5))),0) - NVL (IMPORTO_RIC_PAGATO_B, 0) " & ruoloPag & ") - NVL ( (SELECT SUM (importo_pagato) " _
                       & " FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE data_pagamento  > '" & par.AggiustaData(par.IfEmpty(txtDataPagamAl.Text, Format(Now, "dd/MM/yyyy"))) & "' AND id_gruppo_voce_bolletta <> 5 AND id_t_voce_bolletta NOT IN (150, 151, 677, 676) " _
                       & " AND id_bolletta = BOL_BOLLETTE.ID AND bol_bollette.id_contratto = rapporti_utenza.id), 0), 0),2) AS tot_incassato " _
                       & " FROM siscom_mi.rapporti_utenza,   siscom_mi.unita_immobiliari,   siscom_mi.unita_contrattuale,    siscom_mi.edifici,   siscom_mi.indirizzi,   siscom_mi.bol_bollette WHERE     rapporti_utenza.id = unita_contrattuale.id_contratto  " _
                       & " and data_emissione<= '" & par.AggiustaData(par.IfEmpty(txtDataAl.Text, Format(Now, "dd/MM/yyyy"))) & "'  " _
                       & " And unita_immobiliari.id = unita_contrattuale.id_unita     And unita_immobiliari.id_edificio = edifici.id     And unita_immobiliari.id_indirizzo = indirizzi.id(+)     And unita_contrattuale.id_unita_principale Is NULL     " _
                       & " And BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID      And (FL_ANNULLATA = 0         Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL)) " _
                       & sStringaSql _
                       & " GROUP BY rapporti_utenza.COD_CONTRATTO,BOL_BOLLETTE.ID,BOL_BOLLETTE.ID_contratto,rapporti_utenza.id) " _
                       & " group by cod_contratto "

        End If

            EstraiDati(par.cmd.CommandText, 10)
        par.modalDialogMessage("Attenzione", "Estrazione avviata! Attendere il completamento dell\'operazione.", Me.Page, "", "VisualizzaEstrazioni_RU.aspx")

    End Sub


    Protected Sub DataGridIndirizzi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIndirizzi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('Hf_via').value='" & e.Item.Cells(par.IndDGC(DataGridIndirizzi, "PROGR")).Text & "';")
        End If
    End Sub

    Protected Sub cmbAreaEcon_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAreaEcon.SelectedIndexChanged
        Try

            connData.apri()
            par.caricaComboBox("select SOTTO_AREA from siscom_mi.canone_sopportabile_27 WHERE aree=" & cmbAreaEcon.SelectedValue & " order by sotto_area asc", cmbClasse, "SOTTO_AREA", "SOTTO_AREA", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU -  cmbAreaEcon_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
