Imports System.IO

Partial Class SIRAPER_Tab_Inquilino
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione
    Dim xls As New ExcelSiSol

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        par = CType(Me.Page, Object).par
        Me.connData = CType(Me.Page, Object).connData
        If Not IsNothing(Me.connData) Then
            Me.connData.RiempiPar(par)
            'par.cmd.Transaction = connData.Transazione
        End If
        If Not IsPostBack Then
            If CType(Me.Page.FindControl("MasterPage$MainContent$Elaborazione"), HiddenField).Value = 1 Then
                CaricaDataGridInquilino()
            End If
        End If
    End Sub
    Public Sub CaricaDataGridInquilino()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT DISTINCT SIR_INQUILINO.ID, NVL(CANONE_SOCIALE, 0) AS CANONE_SOCIALE, ROWNUM, " _
                                & "'<a href=""javascript:window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID='|| SIR_INQUILINO.ID_ALLOGGIO ||''',''_blank'',''resizable=no,height=620,top=0,left=0,width=800,scrollbars=no'');void(0);"">'||'Visualizza Alloggio'||'</a>' AS ALLOGGIO, " _
                                & "(CASE WHEN SIR_INQUILINO.ID_CONTRATTO IS NOT NULL THEN '<a href=""javascript:window.open(''../Contratti/Contratto.aspx?LT=1&ID='|| SIR_INQUILINO.ID_CONTRATTO ||''',''_blank'',''resizable=no,height=750,top=0,left=0,width=900,scrollbars=no'');void(0);"">'|| 'Dettagli Contratto' ||'</a>' ELSE '' END) AS CONTRATTO, " _
                                & "(CASE WHEN SIR_INQUILINO.ID_DICHIARAZIONE IS NOT NULL THEN '<a href=""javascript:window.open(''../ANAUT/max.aspx?LE=1&US=1&ID='|| SIR_INQUILINO.ID_DICHIARAZIONE ||''',''_blank'',''resizable=no,height=450,top=0,left=0,width=670,scrollbars=no,menubar=no,toolbar=no'');void(0);"">'|| 'Dettagli Dichirazione' ||'</a>' ELSE '' END) AS DICHIRAZIONE, " _
                                & "COD_INQUILINO, COD_FISCALE, COGNOME, NOME, SESSO, TIPO_GRADO_PARENTELA_SIRAPER.DESCRIZIONE AS GR_PARENTELA, T_TIPO_NUCLEO.DESCRIZIONE AS TIPO_NUCLEO, 'PRIMA FAMIGLIA' AS NUCLEO, NVL(FISC_A_CARICO, -1) AS FISC_A_CARICO, " _
                                & "TO_CHAR(TO_DATE(DATA_NASCITA,'YYYYmmdd'),'DD-MM-YYYY') AS DATA_NASCITA, LUOGO_NASCITA, T_TIPO_CITTADINANZA.DESCRIZIONE AS CITTADINANZA, T_STATO_OCCUPAZIONE.DESCRIZIONE AS COND_PROFESSIONALE, T_TIPO_PROFESSIONE.DESCRIZIONE AS PROFESSIONE, NVL(TRIM(TO_CHAR(REDD_COMPLESSIVO, '9G999G999G999G999G990D99')), 0) AS REDD_COMPLESSIVO, " _
                                & "NVL(TRIM(TO_CHAR(REDD_DIPENDENTE, '9G999G999G999G999G990D99')), 0) AS REDD_DIPENDENTE, NVL(TRIM(TO_CHAR(REDD_AUTONOMO, '9G999G999G999G999G990D99')), 0) AS REDD_AUTONOMO, NVL(TRIM(TO_CHAR(REDD_PENSIONE, '9G999G999G999G999G990D99')), 0) AS REDD_PENSIONE, TRIM(TO_CHAR(REDD_TERRENI, '9G999G999G999G999G990D99')) AS REDD_TERRENI, " _
                                & "NVL(TRIM(TO_CHAR(REDD_FABBRICATI, '9G999G999G999G999G990D99')), 0) AS REDD_FABBRICATI, NVL(TRIM(TO_CHAR(REDD_ALTRI, '9G999G999G999G999G990D99')), 0) AS REDD_ALTRI, TRIM(TO_CHAR(EMOLUMENTI, '9G999G999G999G999G990D99')) AS EMOLUMENTI, NVL(TRIM(TO_CHAR(REDD_AGRARI, '9G999G999G999G999G990D99')), 0) AS REDD_AGRARI, " _
                                & "NVL(TRIM(TO_CHAR(DETRAZ_IRPEF, '9G999G999G999G999G990D99')), 0) AS DETRAZ_IRPEF, ANNO_REDDITO, NVL(TRIM(TO_CHAR(DETRAZ_SP_SANITARIE, '9G999G999G999G999G990D99')), 0) AS DETRAZ_SP_SANITARIE, NVL(DETRAZ_ANZ_DISABILI, 0) AS DETRAZ_ANZ_DISABILI, TRIM(TO_CHAR(SUSSIDI, '9G999G999G999G999G990D99')) AS SUSSIDI " _
                                & "FROM SISCOM_MI.SIR_INQUILINO, SISCOM_MI.TIPO_GRADO_PARENTELA_SIRAPER, SISCOM_MI.T_TIPO_CITTADINANZA, SISCOM_MI.T_TIPO_PROFESSIONE, SISCOM_MI.T_STATO_OCCUPAZIONE, SISCOM_MI.T_TIPO_NUCLEO " _
                                & "WHERE TIPO_GRADO_PARENTELA_SIRAPER.COD(+) = GR_PARENTELA AND T_TIPO_NUCLEO.ID(+) = TIPO_NUCLEO AND T_TIPO_CITTADINANZA.ID(+) = CITTADINANZA AND T_TIPO_PROFESSIONE.ID(+) = PROFESSIONE AND T_STATO_OCCUPAZIONE.ID(+) = COND_PROFESSIONALE " _
                                & "AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value & " " _
                                & "ORDER BY ROWNUM"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            BindGridInquilino(dt)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - CaricaDataGridInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Sub SolaLetturaDatagrid()
        Try
            For Each Items As DataGridItem In dgvInquilino.Items
                FrmSolaLettura(Items)
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - SolaLetturaDatagrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Sub BindGridInquilino(ByVal dt As Data.DataTable)
        Try
            dgvInquilino.DataSource = dt
            dgvInquilino.DataBind()
            If CType(Me.Page.FindControl("MasterPage$MainContent$SLE"), HiddenField).Value = 1 Then
                For Each Items As DataGridItem In dgvInquilino.Items
                    FrmSolaLettura(Items)
                    CType(Items.FindControl("btnMobiliare"), ImageButton).OnClientClick = "alert('Operazione non consentita perchè il file è stato elaborato, o la maschera è in sola lettura!');"
                    CType(Items.FindControl("btnImmobiliare"), ImageButton).OnClientClick = "alert('Operazione non consentita perchè il file è stato elaborato, o la maschera è in sola lettura!');"
                Next
            Else
                For Each Items As DataGridItem In dgvInquilino.Items
                    SettaControlModifiche(Items)
                    CType(Items.FindControl("btnMobiliare"), ImageButton).OnClientClick = "caricamentoincorso();ApriPatrMobInquilino(" & Items.Cells(0).Text & ");"
                    CType(Items.FindControl("btnImmobiliare"), ImageButton).OnClientClick = "caricamentoincorso();ApriPatrImmobInquilino(" & Items.Cells(0).Text & ");"
                    CType(Items.FindControl("txtredditoterreni"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                    CType(Items.FindControl("txtredditoterreni"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                    CType(Items.FindControl("txtaltriemolumenti"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                    CType(Items.FindControl("txtaltriemolumenti"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                    CType(Items.FindControl("txtsussidi"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
                    CType(Items.FindControl("txtsussidi"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                Next
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - BindGridInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('frmModify').value='1';")
            End If
        Next
    End Sub
    Public Sub FrmSolaLettura(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                FrmSolaLettura(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Enabled = False
            End If
        Next
    End Sub
    Protected Sub dgvInquilino_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvInquilino.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
        End If
    End Sub
    Protected Sub dgvInquilino_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvInquilino.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            AggiustaCompSessioneInquilino()
            dgvInquilino.CurrentPageIndex = e.NewPageIndex
            CaricaDataGridInquilino()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
        End If
    End Sub
    Public Sub AggiustaCompSessioneInquilino()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            For i As Integer = 0 To dgvInquilino.Items.Count - 1
                Dim RedditoComplessivo As Decimal = Math.Round(CDec(par.IfEmpty(dgvInquilino.Items(i).Cells(par.IndDGC(dgvInquilino, "REDD_DIPENDENTE")).Text, 0)) _
                                                               + CDec(par.IfEmpty(dgvInquilino.Items(i).Cells(par.IndDGC(dgvInquilino, "REDD_AUTONOMO")).Text, 0)) _
                                                               + CDec(par.IfEmpty(dgvInquilino.Items(i).Cells(par.IndDGC(dgvInquilino, "REDD_PENSIONE")).Text, 0)) _
                                                               + CDec(par.IfEmpty(CType(dgvInquilino.Items(i).FindControl("txtredditoterreni"), TextBox).Text.ToUpper, 0)) _
                                                               + CDec(par.IfEmpty(dgvInquilino.Items(i).Cells(par.IndDGC(dgvInquilino, "REDD_FABBRICATI")).Text, 0)) _
                                                               + CDec(par.IfEmpty(dgvInquilino.Items(i).Cells(par.IndDGC(dgvInquilino, "REDD_ALTRI")).Text, 0)) _
                                                               + CDec(par.IfEmpty(CType(dgvInquilino.Items(i).FindControl("txtaltriemolumenti"), TextBox).Text.ToUpper, 0)) _
                                                               + CDec(par.IfEmpty(dgvInquilino.Items(i).Cells(par.IndDGC(dgvInquilino, "REDD_AGRARI")).Text, 0)), 2)
                par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_INQUILINO SET FISC_A_CARICO = " & RitornaNullSeMenoUno(par.IfEmpty(CType(dgvInquilino.Items(i).FindControl("ddlfisccaric"), DropDownList).SelectedValue.ToString, "-1")) & ", " _
                                    & "REDD_TERRENI = " & par.VirgoleInPunti(par.IfEmpty(CType(dgvInquilino.Items(i).FindControl("txtredditoterreni"), TextBox).Text.ToUpper, "null").ToString.Replace(".", "")) & ", " _
                                    & "EMOLUMENTI = " & par.VirgoleInPunti(par.IfEmpty(CType(dgvInquilino.Items(i).FindControl("txtaltriemolumenti"), TextBox).Text.ToUpper, "null").ToString.Replace(".", "")) & ", " _
                                    & "SUSSIDI = " & par.VirgoleInPunti(par.IfEmpty(CType(dgvInquilino.Items(i).FindControl("txtsussidi"), TextBox).Text.ToUpper, "null").ToString.Replace(".", "")) & ", " _
                                    & "REDD_COMPLESSIVO = " & par.VirgoleInPunti(par.IfEmpty(RedditoComplessivo, "null").ToString.Replace(".", "")) & " " _
                                    & "WHERE ID = " & dgvInquilino.Items(i).Cells(0).Text.ToString & " AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value
                par.cmd.ExecuteNonQuery()
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - AggiustaCompSessioneInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnExportXlsInquilino_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXlsInquilino.Click
        Try
            EsportaExcel()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - btnExportXlsInquilino_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Function EsportaQuery() As String
        Try
            EsportaQuery = "SELECT DISTINCT ROWNUM AS RIGA, (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID = SIR_INQUILINO.ID_ALLOGGIO) AS ALLOGGIO, (SELECT COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID = SIR_INQUILINO.ID_CONTRATTO) AS CONTRATTO, " _
                         & "COD_INQUILINO AS CODICE_INQUILINO, COD_FISCALE AS CODICE_FISCALE, COGNOME, NOME, SESSO, TIPO_GRADO_PARENTELA_SIRAPER.DESCRIZIONE AS GRADO_PARENTELA, T_TIPO_NUCLEO.DESCRIZIONE AS TIPO_NUCLEO, 'PRIMA FAMIGLIA' AS NUCLEO, " _
                         & "(CASE WHEN FISC_A_CARICO = 1 THEN 'SI' WHEN FISC_A_CARICO = 2 THEN 'NO' ELSE '' END) AS FISCALMENTE_A_CARICO, TO_CHAR (TO_DATE (DATA_NASCITA, 'YYYYmmdd'), 'DD-MM-YYYY') AS DATA_NASCITA, LUOGO_NASCITA, " _
                         & "T_TIPO_CITTADINANZA.DESCRIZIONE AS CITTADINANZA, T_STATO_OCCUPAZIONE.DESCRIZIONE AS COND_PROFESSIONALE, T_TIPO_PROFESSIONE.DESCRIZIONE AS PROFESSIONE, " _
                         & "NVL (TRIM (TO_CHAR (REDD_COMPLESSIVO, '9G999G999G999G999G990D99')), 0) AS REDD_COMPLESSIVO, NVL (TRIM (TO_CHAR (REDD_DIPENDENTE, '9G999G999G999G999G990D99')), 0) AS REDDITO_DIPENDENTE, " _
                         & "NVL (TRIM (TO_CHAR (REDD_AUTONOMO, '9G999G999G999G999G990D99')), 0) AS REDD_AUTONOMO, NVL (TRIM (TO_CHAR (REDD_PENSIONE, '9G999G999G999G999G990D99')), 0) AS REDDITO_PENSIONE, " _
                         & "TRIM (TO_CHAR (REDD_TERRENI, '9G999G999G999G999G990D99')) AS REDDITO_TERRENI, NVL (TRIM (TO_CHAR (REDD_FABBRICATI, '9G999G999G999G999G990D99')), 0) AS REDD_FABBRICATI, " _
                         & "NVL (TRIM (TO_CHAR (REDD_ALTRI, '9G999G999G999G999G990D99')), 0) AS REDDITO_ALTRI, TRIM (TO_CHAR (EMOLUMENTI, '9G999G999G999G999G990D99')) AS EMOLUMENTI, " _
                         & "NVL (TRIM (TO_CHAR (REDD_AGRARI, '9G999G999G999G999G990D99')), 0) AS REDDITO_AGRARI, NVL (TRIM (TO_CHAR (DETRAZ_IRPEF, '9G999G999G999G999G990D99')), 0) AS DETRAZIONI_IRPEF, " _
                         & "ANNO_REDDITO, NVL (TRIM (TO_CHAR (DETRAZ_SP_SANITARIE, '9G999G999G999G999G990D99')), 0) AS DETRAZIONI_SPESE_SANITARIE, NVL (DETRAZ_ANZ_DISABILI, 0) AS DETRAZIONI_ANZ_DISABILI, " _
                         & "TRIM (TO_CHAR (SUSSIDI, '9G999G999G999G999G990D99')) AS SUSSIDI " _
                         & "FROM SISCOM_MI.SIR_INQUILINO, SISCOM_MI.TIPO_GRADO_PARENTELA_SIRAPER, SISCOM_MI.T_TIPO_CITTADINANZA, SISCOM_MI.T_TIPO_PROFESSIONE, SISCOM_MI.T_STATO_OCCUPAZIONE, SISCOM_MI.T_TIPO_NUCLEO " _
                         & "WHERE TIPO_GRADO_PARENTELA_SIRAPER.COD(+) = GR_PARENTELA AND T_TIPO_NUCLEO.ID(+) = TIPO_NUCLEO AND T_TIPO_CITTADINANZA.ID(+) = CITTADINANZA AND T_TIPO_PROFESSIONE.ID(+) = PROFESSIONE " _
                         & "AND T_STATO_OCCUPAZIONE.ID(+) = COND_PROFESSIONALE " _
                         & "AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value & " " _
                         & "ORDER BY ROWNUM"
        Catch ex As Exception
            EsportaQuery = ""
        End Try
    End Function
    Private Sub EsportaExcel()
        Try
            If dgvInquilino.Items.Count > 0 Then
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                    'par.cmd.Transaction = connData.Transazione
                End If
                par.cmd.CommandText = EsportaQuery()
                If String.IsNullOrEmpty(par.cmd.CommandText) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                    Exit Sub
                End If
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                Dim NomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportInquiliniSiraper" & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value, "Inquilini", dt)
                If File.Exists(Server.MapPath("~\FileTemp\") & NomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "downloadFile('../FileTemp/" & NomeFile & "');", True)
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun risultato da esportare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - EsportaExcel - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnMobiliare_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not IsNothing(Session.Item("FRMODIFY")) Then
                If Session.Item("FRMODIFY") = 1 Then
                    CType(Me.Page.FindControl("MasterPage$MainContent$frmModify"), HiddenField).Value = 1
                End If
                Session.Remove("FRMODIFY")
            End If
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - btnMobiliare_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnImmobiliare_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not IsNothing(Session.Item("FRMODIFY")) Then
                If Session.Item("FRMODIFY") = 1 Then
                    CType(Me.Page.FindControl("MasterPage$MainContent$frmModify"), HiddenField).Value = 1
                End If
                Session.Remove("FRMODIFY")
            End If
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - btnImmobiliare_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnCercaInquilino_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaInquilino.Click
        Try
            CercaInquilino()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - btnCercaInquilino_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CercaInquilino()
        Try
            If Not String.IsNullOrEmpty(txtCodFiscaleInquilino.Text) Then
                AggiustaCompSessioneInquilino()
                Dim dt As Data.DataTable = Session.Item("dtInquilino")
                Dim row As Data.DataRow
                Try
                    row = dt.Select("COD_FISCALE = '" & par.PulisciStrSql(txtCodFiscaleInquilino.Text.ToUpper) & "'")(0)
                    If Not IsNothing(row) Then
                        CType(Me.Page, Object).TrovaRigaDataGrid(3, dgvInquilino, par.IfNull(row.Item("ROWNUM"), 0))
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Il Codice Fiscale dell\'Inquilino inserito non è presente! Controllare il Codice Fiscale!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                    End If
                Catch ex As Exception
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Il Codice Fiscale dell\'Inquilino inserito non è presente! Controllare il Codice Fiscale!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                End Try
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire il Codice Fiscale dell\'Inquilino da Ricercare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, Me.Page.ClientID, "RicercaOggetto(3,1);", True)
            End If
            Me.txtCodFiscaleInquilino.Text = ""
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Inquilino - CercaInquilino - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function RitornaNullSeMenoUno(ByVal Valore As String) As String
        Try
            RitornaNullSeMenoUno = "null"
            If Valore = "-1" Then
                RitornaNullSeMenoUno = "null"
            ElseIf Valore <> "-1" Then
                RitornaNullSeMenoUno = "'" & Valore & "'"
            End If
        Catch ex As Exception
            RitornaNullSeMenoUno = "null"
        End Try
    End Function
End Class
