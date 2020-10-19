Imports System.IO

Partial Class Contabilita_Report_ReportSituazioneMorosita
    Inherits PageSetIdMode

    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 796px; height: 540px;" _
              & "top: 0px; left: 0px;background-color: #eeeeee;background-image: url('../../NuoveImm/SfondoMascheraContratti2.jpg');z-index:1000;"">" _
              & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
              & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
              & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
              & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
              & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
              & "</td></tr></table></div></div>"
        Response.Write(Loading)

        Dim emesso As Boolean = False
        Panel5.Visible = False
        If Not IsNothing(Request.QueryString("TIPO")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("TIPO").ToString) Then
                If Request.QueryString("TIPO").ToString = "EM" Then
                    LabelRicerca.Text = "Ricerca Situazione Emesso"
                    Label11.Text = "Ricerca Situazione Emesso"
                    emesso = True
                    Panel5.Visible = True
                End If
            End If
        End If

        If Not IsPostBack Then
            Response.Flush()
            caricaTipologieContrattuali()
            caricaEdifici()
            caricaComplessi()
            caricaIndirizzi()
            CaricaClasse()
            If emesso Then
                caricaTipologie()
            End If

            caricaListaMacroCategorie()
            caricaListaTipologieUI()
            caricaListaVoci()
            caricaCapitoli()
            caricaEsercizioContabile()

        End If
        ImpostaJavaScriptFunction()
    End Sub

    Private Sub ImpostaJavaScriptFunction()
        TextBoxRiferimentoAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TextBoxRiferimentoDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Private Sub caricaTipologie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_BOLLETTE WHERE ID<21 order by descrizione asc"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListTipologiaBollettazione.Items.Clear()
                CheckBoxListTipologiaBollettazione.DataSource = dataTable
                CheckBoxListTipologiaBollettazione.DataValueField = "ID"
                CheckBoxListTipologiaBollettazione.DataTextField = "DESCRIZIONE"
                CheckBoxListTipologiaBollettazione.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaIndirizzi()
        Try
            ApriConnessione()

            Dim I As Integer = 0
            chIndirizzi.Items.Clear()
            par.cmd.CommandText = "SELECT DISTINCT DESCRIZIONE FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID ORDER BY DESCRIZIONE ASC"
            'INDIRIZZI.DESCRIZIONE='* BOLLETTAZIONE *' OR 
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReaderA.Read
                If par.IfEmpty(par.IfNull(myReaderA("DESCRIZIONE"), ""), "") <> "" Then
                    chIndirizzi.Items.Add(par.IfNull(myReaderA("DESCRIZIONE"), "")) ' & ", " & par.IfNull(myReaderA("CIVICO"), ""))
                    'chIndirizzi.Items(I).Value = par.IfNull(myReaderA("ID"), -1)
                    I = I + 1
                End If
            Loop
            myReaderA.Close()

            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaEdifici()
        Try
            ApriConnessione()

            par.caricaComboBox("SELECT * FROM SISCOM_MI.EDIFICI ORDER BY DENOMINAZIONE ASC", DropDownListEdificio, "ID", "DENOMINAZIONE", False)
            DropDownListEdificio.Items.Add("TUTTI")
            DropDownListEdificio.Items.FindByText("TUTTI").Selected = True

            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaComplessi()
        Try
            ApriConnessione()

            par.caricaComboBox("SELECT * FROM SISCOM_MI.COMPLESSI_IMMOBILIARI ORDER BY DENOMINAZIONE ASC", DropDownListComplesso, "ID", "DENOMINAZIONE", False)
            DropDownListComplesso.Items.Add("TUTTI")
            DropDownListComplesso.Items.FindByText("TUTTI").Selected = True

            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub CaricaClasse()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT DISTINCT SOTTO_AREA FROM SISCOM_MI.CANONI_EC WHERE SOTTO_AREA IS NOT NULL ORDER BY SOTTO_AREA ASC"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListClasseAppartenenza.Items.Clear()
                CheckBoxListClasseAppartenenza.DataSource = dataTable
                CheckBoxListClasseAppartenenza.DataTextField = "SOTTO_AREA"
                CheckBoxListClasseAppartenenza.DataValueField = "SOTTO_AREA"
                CheckBoxListClasseAppartenenza.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaTipologieContrattuali()
        Try
            ApriConnessione()

            'par.RiempiDList(Me, par.OracleConn, "DropDownListComplesso", "SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", "DESCRIZIONE", "COD")
            par.cmd.CommandText = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListTipologiaContrattuale.Items.Clear()
                CheckBoxListTipologiaContrattuale.DataSource = dataTable
                CheckBoxListTipologiaContrattuale.DataTextField = "DESCRIZIONE"
                CheckBoxListTipologiaContrattuale.DataValueField = "COD"
                CheckBoxListTipologiaContrattuale.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub
    Protected Sub chiudiConnessione()
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub CheckBoxTipologiaContrattuale_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipologiaContrattuale.CheckedChanged
        If CheckBoxTipologiaContrattuale.Checked = True Then
            For Each Items As ListItem In CheckBoxListTipologiaContrattuale.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListTipologiaContrattuale.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxClasseAppartenenza_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxClasseAppartenenza.CheckedChanged
        If CheckBoxClasseAppartenenza.Checked = True Then
            For Each Items As ListItem In CheckBoxListClasseAppartenenza.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListClasseAppartenenza.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxTipo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipo.CheckedChanged
        If CheckBoxTipo.Checked = True Then
            For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
                Items.Selected = False
            Next
        End If        
    End Sub

    Protected Sub CheckBoxListTipologiaBollettazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListTipologiaBollettazione.SelectedIndexChanged        
        Dim Script As String = "document.getElementById('PanelTipo').scrollTop = document.getElementById('yPosTipo').value;"
        ScriptManager.RegisterStartupScript(PanelTipo, GetType(Panel), Page.ClientID, Script, True)
    End Sub
    '****************************************************************************************************************************
    Private Sub caricaEsercizioContabile()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT to_char(to_date(VALIDITA_DA,'yyyyMMdd'),'dd/MM/yyyy')||' - '||to_char(to_date(VALIDITA_A,'yyyyMMdd'),'dd/MM/yyyy') AS VALIDITA," _
                & " ID FROM SISCOM_MI.BOL_BOLLETTE_ES_CONTABILE ORDER BY VALIDITA_A"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListEserciziContabili.Items.Clear()
                CheckBoxListEserciziContabili.DataSource = dataTable
                CheckBoxListEserciziContabili.DataValueField = "ID"
                CheckBoxListEserciziContabili.DataTextField = "VALIDITA"
                CheckBoxListEserciziContabili.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaListaVoci()
        Try
            ApriConnessione()
            Dim macroCategorieSelezionate As String = ""
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                If Items.Selected = True Then
                    macroCategorieSelezionate &= Items.Value & ","
                End If
            Next
            If macroCategorieSelezionate <> "" Then
                macroCategorieSelezionate = Left(macroCategorieSelezionate, Len(macroCategorieSelezionate) - 1)
            End If
            Dim CategorieSelezionate As String = ""
            'For Each Items As ListItem In CheckBoxListCategorie.Items
            '    If Items.Selected = True Then
            '        CategorieSelezionate &= Items.Value & ","
            '    End If
            'Next
            If CategorieSelezionate <> "" Then
                CategorieSelezionate = Left(CategorieSelezionate, Len(CategorieSelezionate) - 1)
            End If

            Dim competenzeSelezionate As String = ""
            'For Each Items As ListItem In CheckBoxListCompetenza.Items
            '    If Items.Selected = True Then
            '        competenzeSelezionate &= Items.Value & ","
            '    End If
            'Next
            If competenzeSelezionate <> "" Then
                competenzeSelezionate = Left(competenzeSelezionate, Len(competenzeSelezionate) - 1)
            End If

            If competenzeSelezionate = "" Then
                If CategorieSelezionate = "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate = "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND GRUPPO IN(" & macroCategorieSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") AND GRUPPO IN(" & macroCategorieSelezionate & ") ORDER BY DESCRIZIONE"
                End If
            Else
                If CategorieSelezionate = "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 ORDER BY DESCRIZIONE"
                    'par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate = "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND GRUPPO IN(" & macroCategorieSelezionate & ") AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate = "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                ElseIf CategorieSelezionate <> "" And macroCategorieSelezionate <> "" Then
                    par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA WHERE ID<10000 AND TIPO_VOCE IN(" & CategorieSelezionate & ") AND GRUPPO IN(" & macroCategorieSelezionate & ") AND COMPETENZA IN (" & competenzeSelezionate & ") ORDER BY DESCRIZIONE"
                End If
            End If
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            CheckBoxListVoci.Items.Clear()
            CheckBoxListVoci.DataSource = dataTable
            CheckBoxListVoci.DataTextField = "DESCRIZIONE"
            CheckBoxListVoci.DataValueField = "ID"
            CheckBoxListVoci.DataBind()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Private Sub caricaListaTipologieUI()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListTipologieUI.Items.Clear()
                CheckBoxListTipologieUI.DataSource = dataTable
                CheckBoxListTipologieUI.DataTextField = "DESCRIZIONE"
                CheckBoxListTipologieUI.DataValueField = "COD"
                CheckBoxListTipologieUI.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Protected Sub CheckBoxListMacrocategorie_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListMacrocategorie.SelectedIndexChanged
        caricaListaVoci()
        Dim Script As String = "document.getElementById('PanelMacrocategorie').scrollTop = document.getElementById('yPosMacrocategorie').value;"
        ScriptManager.RegisterStartupScript(PanelMacrocategorie, GetType(Panel), Page.ClientID, Script, True)
    End Sub

    Protected Sub ImageButtonAvanti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvanti.Click        
        MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "red", "location.replace('../pagina_home.aspx')", True)
    End Sub

    Protected Sub ImageButtonHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonHome.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "red", "location.replace('../pagina_home.aspx')", True)
    End Sub

    Protected Sub ImageButtonIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonIndietro.Click
        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub CheckBoxMacrocategorie_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxMacrocategorie.CheckedChanged
        If CheckBoxMacrocategorie.Checked = True Then
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListMacrocategorie.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxVoci_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxVoci.CheckedChanged
        If CheckBoxVoci.Checked = True Then
            For Each Items As ListItem In CheckBoxListVoci.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListVoci.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Protected Sub CheckBoxEserciziContabili_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxEserciziContabili.CheckedChanged
        If CheckBoxEserciziContabili.Checked = True Then
            For Each Items As ListItem In CheckBoxListEserciziContabili.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListEserciziContabili.Items
                Items.Selected = False
            Next
        End If
    End Sub
    Private Sub caricaListaMacroCategorie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.T_VOCI_BOLLETTA_GRUPPI ORDER BY 2"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListMacrocategorie.Items.Clear()
                CheckBoxListMacrocategorie.DataSource = dataTable
                CheckBoxListMacrocategorie.DataTextField = "DESCRIZIONE"
                CheckBoxListMacrocategorie.DataValueField = "ID"
                CheckBoxListMacrocategorie.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Protected Sub CheckBoxTipologieUI_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxTipologieUI.CheckedChanged
        If CheckBoxTipologieUI.Checked = True Then
            For Each Items As ListItem In CheckBoxListTipologieUI.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListTipologieUI.Items
                Items.Selected = False
            Next
        End If
    End Sub

    Private Function LeggiDaXls() As String
        Dim xls As New ExcelSiSol
        Dim returnString As String = ""

        Dim DTADAFILE As New Data.DataTable

        If FileUpload2.HasFile Then
            Dim fileOK As Boolean = False
            Dim fileExtension As String = System.IO.Path.GetExtension(FileUpload2.FileName).ToLower()
            Dim allowedExtensions As String() = {".xlsx"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension = allowedExtensions(i) Then
                    fileOK = True
                    Exit For
                End If
            Next
            If fileOK = False Then
                'par.modalDialogMessage("Attenzione", "Inserire un file con formato *.xlsx corretto!", Me.Page)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Inserire un file con formato *.xlsx corretto! Lista NON CARICATA.');", True)
                returnString = ""
                Return returnString
                Exit Function
            End If
        Else
            'par.modalDialogMessage("Attenzione", "Inserire un file con formato *.xlsx corretto!", Me.Page)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Inserire un file con formato *.xlsx corretto! Lista NON CARICATA.');", True)
            Return returnString
            returnString = ""
            Exit Function
        End If
        Dim nFile As String = Server.MapPath("..\..\FileTemp\") & Format(Now, "HHmmss") & "_" & FileUpload2.FileName
        FileUpload2.SaveAs(nFile)

        Dim dtExcel As Data.DataTable

        Using pck As New OfficeOpenXml.ExcelPackage()
            Using stream = File.Open(nFile, FileMode.Open)
                pck.Load(stream)
            End Using
            Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
            dtExcel = xls.WorksheetToDataTable(ws, True)
        End Using

        'ApriConnessione()
        If dtExcel.Rows.Count > 0 Then
            For i As Integer = 0 To dtExcel.Rows.Count - 1
                returnString &= par.PulisciStrSql(dtExcel.Rows(i).Item(0)) & "|"                
            Next
        End If
        Return returnString

    End Function

    Private Sub caricaCapitoli()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_VOCI_BOLLETTA_CAP ORDER BY NLSSORT(descrizione,'NLS_SORT=BINARY')"
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                CheckBoxListCapitoli.Items.Clear()
                CheckBoxListCapitoli.DataSource = dataTable
                CheckBoxListCapitoli.DataValueField = "id"
                CheckBoxListCapitoli.DataTextField = "descrizione"
                CheckBoxListCapitoli.DataBind()
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub

    Protected Sub CheckBoxCapitoli_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxCapitoli.CheckedChanged
        If CheckBoxCapitoli.Checked = True Then
            For Each Items As ListItem In CheckBoxListCapitoli.Items
                Items.Selected = True
            Next
        Else
            For Each Items As ListItem In CheckBoxListCapitoli.Items
                Items.Selected = False
            Next
        End If
    End Sub


    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function


    Protected Sub ImageButtonAvviaDettaglio_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvviaDettaglio.Click
        Dim parametriDaPassare As String = ""       
        parametriDaPassare &= "?Condominio=" & DropDownListCondomini.SelectedValue
        parametriDaPassare &= "&RiferimentoDal=" & par.FormatoDataDB(TextBoxRiferimentoDal.Text)
        parametriDaPassare &= "&RiferimentoAl=" & par.FormatoDataDB(TextBoxRiferimentoAl.Text)
        parametriDaPassare &= "&Edificio=" & DropDownListEdificio.SelectedValue
        parametriDaPassare &= "&Complesso=" & DropDownListComplesso.SelectedValue
        parametriDaPassare &= "&ingiunto=" & Valore01(chIngiunto.Checked)
        parametriDaPassare &= "&ruolo=" & Valore01(chRuolo.Checked)
        parametriDaPassare &= "&Dettaglio=1"

        Dim emesso As String = ""
        If Not IsNothing(Request.QueryString("TIPO")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("TIPO").ToString) Then
                If Request.QueryString("TIPO").ToString = "EM" Then
                    parametriDaPassare &= "&TIPO=EM"
                End If
            End If
        End If

        Dim listavoci As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListVoci.Items
            If Items.Selected = True Then
                If Not listavoci.Contains(Items.Value) Then
                    listavoci.Add(Items.Value)
                End If
            Else
                If listavoci.Contains(Items.Value) Then
                    listavoci.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaVoci", listavoci)

        Dim listaMacrocategorie As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListMacrocategorie.Items
            If Items.Selected = True Then
                If Not listaMacrocategorie.Contains(Items.Value) Then
                    listaMacrocategorie.Add(Items.Value)
                End If
            Else
                If listaMacrocategorie.Contains(Items.Value) Then
                    listaMacrocategorie.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaMacrocategorie", listaMacrocategorie)

        Dim listaTipologieUI As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologieUI.Items
            If Items.Selected = True Then
                If Not listaTipologieUI.Contains(Items.Value) Then
                    listaTipologieUI.Add(Items.Value)
                End If
            Else
                If listaTipologieUI.Contains(Items.Value) Then
                    listaTipologieUI.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipologieUI", listaTipologieUI)

        Dim listaCapitoli As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListCapitoli.Items
            If Items.Selected = True Then
                If Not listaCapitoli.Contains(Items.Value) Then
                    listaCapitoli.Add(Items.Value)
                End If
            Else
                If listaCapitoli.Contains(Items.Value) Then
                    listaCapitoli.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaCapitoli", listaCapitoli)

        Dim listaEserciziContabili As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListEserciziContabili.Items
            If Items.Selected = True Then
                If Not listaEserciziContabili.Contains(Items.Value) Then
                    listaEserciziContabili.Add(Items.Value)
                End If
            Else
                If listaEserciziContabili.Contains(Items.Value) Then
                    listaEserciziContabili.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaEserciziContabili", listaEserciziContabili)

        Dim listaTipologiaBollettazione As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologiaBollettazione.Items
            If Items.Selected = True Then
                If Not listaTipologiaBollettazione.Contains(Items.Value) Then
                    listaTipologiaBollettazione.Add(Items.Value)
                End If
            Else
                If listaTipologiaBollettazione.Contains(Items.Value) Then
                    listaTipologiaBollettazione.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipologiaBollettazione", listaTipologiaBollettazione)


        Dim parametriRicercaDataPagamento As String = ""
        Dim parametriRicercaRiferimento As String = ""
        If TextBoxRiferimentoAl.Text <> "" And TextBoxRiferimentoDal.Text = "" Then
            parametriRicercaRiferimento = "Data riferimento al " & TextBoxRiferimentoAl.Text
        ElseIf TextBoxRiferimentoAl.Text <> "" And TextBoxRiferimentoDal.Text <> "" Then
            parametriRicercaRiferimento = "Data riferimento dal " & TextBoxRiferimentoDal.Text & " al " & TextBoxRiferimentoAl.Text
        ElseIf TextBoxRiferimentoAl.Text = "" And TextBoxRiferimentoDal.Text = "" Then
            parametriRicercaRiferimento = ""
        ElseIf TextBoxRiferimentoAl.Text = "" And TextBoxRiferimentoDal.Text <> "" Then
            parametriRicercaRiferimento = "Data riferimento dal " & TextBoxRiferimentoDal.Text
        End If

        Dim parametriRicercaContabilita As String = ""
        Dim parametriRicercaAttribuiti As String = ""
        Dim parametriRicercaAggiornamento As String = ""
        Dim parametriRicercaCondominio As String = ""
        If DropDownListCondomini.SelectedValue = -1 Then
            parametriRicercaCondominio = "CONDOMINI: nessuno"
        ElseIf DropDownListCondomini.SelectedValue = 2 Then
            parametriRicercaCondominio = "CONDOMINI: tutti"
        ElseIf DropDownListCondomini.SelectedValue = 1 Then
            parametriRicercaCondominio = "CONDOMINI: gestione diretta"
        ElseIf DropDownListCondomini.SelectedValue = 0 Then
            parametriRicercaCondominio = "CONDOMINI: gestione indiretta"
        End If

        Dim parametriRicercaC As String = ""
        Dim parametriEserciziContabili As String = ""
        For Each Items As ListItem In CheckBoxListEserciziContabili.Items
            If Items.Selected = True Then
                parametriEserciziContabili &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriEserciziContabili <> "" Then
            parametriEserciziContabili = "ESERCIZI CONTABILI: " & Left(parametriEserciziContabili, Len(parametriEserciziContabili) - 1)
        End If

        Dim parametriRicercaMC As String = ""
        For Each Items As ListItem In CheckBoxListMacrocategorie.Items
            If Items.Selected = True Then
                parametriRicercaMC &= UCase(Left(Items.Text, 1)) & LCase(Mid(Items.Text, 2)) & ","
            End If
        Next
        If parametriRicercaMC <> "" Then
            parametriRicercaMC = "MACROCATEGORIE: " & Left(parametriRicercaMC, Len(parametriRicercaMC) - 1)
        End If

        Dim parametriRicercaTipologiaBollettazione As String = ""
        Dim parametriDate As String = ""
        parametriDate = parametriRicercaDataPagamento
        If parametriDate <> "" And parametriRicercaContabilita <> "" Then
            parametriDate &= ", " & parametriRicercaContabilita
        Else
            parametriDate &= parametriRicercaContabilita
        End If
        If parametriDate <> "" Then
            parametriDate &= ", " & parametriRicercaRiferimento
        Else
            parametriDate &= parametriRicercaRiferimento
        End If
        If parametriDate <> "" And parametriRicercaAggiornamento <> "" Then
            parametriDate &= ", " & parametriRicercaAggiornamento
        Else
            parametriDate &= parametriRicercaAggiornamento
        End If

        If parametriDate <> "" And parametriRicercaAttribuiti <> "" Then
            parametriDate &= ", " & parametriRicercaAttribuiti
        Else
            parametriDate &= parametriRicercaAttribuiti
        End If

        Dim parametriRicercaContocorrente As String = ""
        Dim parametriRicercaTipoIncasso As String = ""
        Dim parametriRicercaNumeroAssegno As String = ""
        '************ LISTA RU ****************

        Dim parametriRicercaRU As String = ""
        If Not IsNothing(Session.Item("listaRU")) Then
            parametriRicercaRU = Session.Item("listaRU").ToString
        End If
        '**************************************
        Dim listaTipologieContrattuali As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListTipologiaContrattuale.Items
            If Items.Selected = True Then
                If Not listaTipologieContrattuali.Contains(Items.Value) Then
                    listaTipologieContrattuali.Add(Items.Value)
                End If
            Else
                If listaTipologieContrattuali.Contains(Items.Value) Then
                    listaTipologieContrattuali.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaTipologieContrattuali", listaEserciziContabili)

        Dim listaAreaAppartenenza As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In CheckBoxListClasseAppartenenza.Items
            If Items.Selected = True Then
                If Not listaAreaAppartenenza.Contains(Items.Value) Then
                    listaAreaAppartenenza.Add(Items.Value)
                End If
            Else
                If listaAreaAppartenenza.Contains(Items.Value) Then
                    listaAreaAppartenenza.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaClasseAppartenenza", listaAreaAppartenenza)

        Dim listaIndirizzi As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In chIndirizzi.Items
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

        Session.Add("filtriRicerca", parametriDate & ", " & parametriRicercaTipologiaBollettazione & vbCrLf _
                    & parametriRicercaRU & ", " _
                    & parametriRicercaCondominio & ", " & parametriRicercaContocorrente & ", " & parametriRicercaTipoIncasso & ", " & parametriRicercaNumeroAssegno & ", " & parametriEserciziContabili & vbCrLf _
                    & parametriRicercaC & vbCrLf & parametriRicercaMC)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "window.open('RisultatiReportSituazioneMorosita.aspx" & parametriDaPassare & "','_blank','');", True)
    End Sub

    Protected Sub uploadListaRU_Click(sender As Object, e As System.EventArgs) Handles uploadListaRU.Click
        Try
            Session.Remove("listaRU")

            Dim listaCodiciRU As String = LeggiDaXls()
            If listaCodiciRU <> "" Then
                Session.Add("listaRU", listaCodiciRU)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Lista caricata correttamente!');", True)
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');", True)
        End Try
    End Sub
End Class