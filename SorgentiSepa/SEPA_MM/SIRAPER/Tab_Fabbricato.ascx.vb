Imports System.IO

Partial Class SIRAPER_Tab_Fabbricato
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
                CaricaDatagridFabbricato()
            End If
        End If
    End Sub
    Public Sub CaricaDatagridFabbricato()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            par.cmd.CommandText = "SELECT DISTINCT ID, ROWNUM, " _
                                & "'<a href=""javascript:window.open(''../CENSIMENTO/InserimentoEdifici.aspx?X=1&SLE=1&ID=' || ID || ''',''_blank'',''resizable=yes,height=620,width=800,top=0,left=0,scrollbars=no'');void(0);"">' || COD_FABBRICATO || '</a>' AS COD_FABBRICATO, " _
                                & "(CASE WHEN PROPRIETA = 1 THEN 'Intera Proprietà' ELSE 'Mista Proprietà' END) AS TIPO_PROPRIETA, NVL(SIR_FABBRICATO.GESTIONE, -1) AS GESTIONE, COD_ISTAT_COMUNE, NVL(SIR_FABBRICATO.UBICAZIONE, -1) AS UBICAZIONE, NVL(COEFF_UBICAZIONE, -1) AS COEFF_UBICAZIONE, " _
                                & "ANNO_COSTRUZIONE, ANNO_RISTRUTTURAZIONE, (NVL(T_TIPO_VIA.DESCRIZIONE, 'VIA') || ' ' || NOME_VIA || ', ' || NUMERO_CIVICO) AS INDIRIZZO, (LOCALITA || ' - ' || CAP) AS LOCALITA, NUM_ALL_RISCATTO, COD_FABBRICATO AS CODICE_FABBRICATO " _
                                & "FROM SISCOM_MI.SIR_FABBRICATO, T_TIPO_VIA " _
                                & "WHERE T_TIPO_VIA.COD_SIRAPER(+) = SIR_FABBRICATO.PREFISSO_INDIRIZZO AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value & " " _
                                & "ORDER BY ROWNUM"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            BindGridFabbricato(dt)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - CaricaDatagridFabbricato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Sub SolaLetturaDatagrid()
        Try
            For Each Items As DataGridItem In dgvFabbricati.Items
                CType(Items.FindControl("ddlgestionedificio"), DropDownList).Enabled = False
                CType(Items.FindControl("ddlubicazionedificio"), DropDownList).Enabled = False
                CType(Items.FindControl("ddlcoeffubicazione"), DropDownList).Enabled = False
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - SolaLetturaDatagrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Sub BindGridFabbricato(ByVal dt As Data.DataTable)
        Try
            dgvFabbricati.DataSource = dt
            dgvFabbricati.DataBind()
            If CType(Me.Page.FindControl("MasterPage$MainContent$SLE"), HiddenField).Value = 1 Then
                For Each Items As DataGridItem In dgvFabbricati.Items
                    CType(Items.FindControl("ddlgestionedificio"), DropDownList).Enabled = False
                    CType(Items.FindControl("ddlubicazionedificio"), DropDownList).Enabled = False
                    CType(Items.FindControl("ddlcoeffubicazione"), DropDownList).Enabled = False
                Next
            Else
                For Each Items As DataGridItem In dgvFabbricati.Items
                    CType(Items.FindControl("ddlgestionedificio"), DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
                    CType(Items.FindControl("ddlubicazionedificio"), DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
                    CType(Items.FindControl("ddlcoeffubicazione"), DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('frmModify').value='1';")
                Next
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - BindGridFabbricato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvFabbricati_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvFabbricati.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
        End If
    End Sub
    Protected Sub dgvFabbricati_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvFabbricati.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            AggiustaCompSessioneFabbricato()
            dgvFabbricati.CurrentPageIndex = e.NewPageIndex
            CaricaDatagridFabbricato()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
        End If
    End Sub
    Public Sub AggiustaCompSessioneFabbricato()
        Try
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
                'par.cmd.Transaction = connData.Transazione
            End If
            For i As Integer = 0 To dgvFabbricati.Items.Count - 1
                par.cmd.CommandText = "UPDATE SISCOM_MI.SIR_FABBRICATO SET GESTIONE = " & RitornaNullSeMenoUno(par.IfEmpty(CType(dgvFabbricati.Items(i).FindControl("ddlgestionedificio"), DropDownList).SelectedValue, "-1")) & ", " _
                                    & "UBICAZIONE = " & RitornaNullSeMenoUno(par.IfEmpty(CType(dgvFabbricati.Items(i).FindControl("ddlubicazionedificio"), DropDownList).SelectedValue, "-1")) & ", " _
                                    & "COEFF_UBICAZIONE = " & RitornaNullSeMenoUno(par.IfEmpty(CType(dgvFabbricati.Items(i).FindControl("ddlcoeffubicazione"), DropDownList).SelectedValue, "-1")) & " " _
                                    & "WHERE ID = " & dgvFabbricati.Items(i).Cells(0).Text.ToString & " AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value
                par.cmd.ExecuteNonQuery()
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - AggiustaCompSessioneFabbricato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnExportXlsFabbricato_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportXlsFabbricato.Click
        Try
            EsportaExcel()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - btnExportXlsFabbricato_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Public Function EsportaQuery() As String
        Try
            EsportaQuery = "SELECT DISTINCT ROWNUM AS RIGA, COD_FABBRICATO AS CODICE_FABBRICATO, (CASE WHEN PROPRIETA = 1 THEN 'Intera Proprietà' ELSE 'Mista Proprietà' END) AS TIPO_PROPRIETA, " _
                         & "TIPO_GESTIONE_EDIFICIO. DESCRIZIONE AS GESTIONE,  COD_ISTAT_COMUNE, TIPO_UBICAZIONE_EDIFICIO.DESCRIZIONE AS UBICAZIONE, TIPO_COEFF_UBICAZIONE_EDIFICIO.DESCRIZIONE AS COEFFICENTE_UBICAZIONE, " _
                         & "ANNO_COSTRUZIONE, ANNO_RISTRUTTURAZIONE, (NVL(T_TIPO_VIA.DESCRIZIONE, 'VIA') || ' ' || NOME_VIA || ', ' || NUMERO_CIVICO) AS INDIRIZZO, (LOCALITA || ' - ' || CAP) AS LOCALITA, NUM_ALL_RISCATTO AS NUMERO_ALLOGGI_A_RISCATTO " _
                         & "FROM SISCOM_MI.SIR_FABBRICATO, T_TIPO_VIA, SISCOM_MI.TIPO_GESTIONE_EDIFICIO, SISCOM_MI.TIPO_UBICAZIONE_EDIFICIO, SISCOM_MI.TIPO_COEFF_UBICAZIONE_EDIFICIO " _
                         & "WHERE T_TIPO_VIA.COD_SIRAPER(+) = SIR_FABBRICATO.PREFISSO_INDIRIZZO AND TIPO_GESTIONE_EDIFICIO.COD(+) = SIR_FABBRICATO.GESTIONE AND TIPO_UBICAZIONE_EDIFICIO.COD(+) = SIR_FABBRICATO.UBICAZIONE " _
                         & "AND TIPO_COEFF_UBICAZIONE_EDIFICIO.COD(+) = SIR_FABBRICATO.COEFF_UBICAZIONE AND ID_SIRAPER = " & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value & " " _
                         & "ORDER BY ROWNUM"
        Catch ex As Exception
            EsportaQuery = ""
        End Try
    End Function
    Private Sub EsportaExcel()
        Try
            If dgvFabbricati.Items.Count > 0 Then
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
                Dim NomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportFabbricatiSiraper" & CType(Me.Page.FindControl("MasterPage$MainContent$idSiraper"), HiddenField).Value, "Fabbricati", dt)
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
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - EsportaExcel - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnCercaFabbricato_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaFabbricato.Click
        Try
            CercaFabbricato()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - btnCercaFabbricato_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CercaFabbricato()
        Try
            If Not String.IsNullOrEmpty(txtCodiceFabbricato.Text) Then
                AggiustaCompSessioneFabbricato()
                Dim dt As Data.DataTable = Session.Item("dtFabbricato")
                Dim row As Data.DataRow
                Try
                    row = dt.Select("CODICE_FABBRICATO = '" & par.PulisciStrSql(txtCodiceFabbricato.Text.ToUpper) & "'")(0)
                    If Not IsNothing(row) Then
                        CType(Me.Page, Object).TrovaRigaDataGrid(1, dgvFabbricati, par.IfNull(row.Item("ROWNUM"), 0))
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "div", "if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Il Codice del Fabbricato inserito non è presente! Controllare il Codice!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                    End If
                Catch ex As Exception
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Il Codice del Fabbricato inserito non è presente! Controllare il Codice!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                End Try
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Inserire il Codice del Fabbricato da Ricercare!');if (document.getElementById('caricamento') != null) { if (document.getElementById('caricamento').style.visibility == 'visible') { document.getElementById('caricamento').style.visibility = 'hidden'; }; };", True)
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, Me.Page.ClientID, "RicercaOggetto(1,1);", True)
            End If
            Me.txtCodiceFabbricato.Text = ""
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Siraper_Tab_Fabbricato - btnCercaFabbricato_Click - " & ex.Message)
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
