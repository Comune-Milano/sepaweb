Imports System.IO
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_Composizione
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "CHIUSO" Then
                SoloLettura()
            End If
            ''If CType(Me.Page, Object).vIdAppalti > 0 Then
            ''    SelAssociati()
            ''End If
        End If
    End Sub
 

  
    'Public Sub SelAssociati()
    '    Try

    '        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans
    '        If DirectCast(Me.Page.FindControl("TipoLotto"), HiddenField).Value = "E" Then
    '            par.cmd.CommandText = "SELECT ID_EDIFICIO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
    '        Else
    '            par.cmd.CommandText = "SELECT ID_IMPIANTO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
    '        End If

    '        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        If Lettore.HasRows = True Then

    '            While Lettore.Read
    '                Dim i As Integer = 0
    '                Dim di As DataGridItem

    '                For i = 0 To Me.DataGridComposizione.Items.Count - 1
    '                    di = Me.DataGridComposizione.Items(i)
    '                    If di.Cells(0).Text = par.IfNull(Lettore("ID"), 0) Then
    '                        DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True
    '                        Selezionati.Value = 1
    '                    Else
    '                        DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = False

    '                    End If
    '                Next

    '            End While
    '        Else
    '            Dim i As Integer = 0
    '            Dim di As DataGridItem

    '            For i = 0 To Me.DataGridComposizione.Items.Count - 1
    '                di = Me.DataGridComposizione.Items(i)
    '                DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = False
    '            Next
    '            Selezionati.Value = 0

    '        End If



    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
    '        CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabComposizione"
    '    End Try


    'End Sub
    Public Sub SoloLettura()
        DataGridComposizione.Rebind()
        DataGridComposizione.Enabled = False
        'If DataGridComposizione.Items.Count > 0 Then
        '
        '    Dim i As Integer = 0
        '    Dim di As GridDataItem
        '
        '
        '    For i = 0 To Me.DataGridComposizione.Items.Count - 1
        '        di = Me.DataGridComposizione.Items(i)
        '        DirectCast(di.Cells(1).FindControl("CheckBox1"), RadButton).Enabled = False
        '        Selezionati.Value = 1
        '    Next
        'End If
        CheckBoxA.Enabled = False
        CheckBoxB.Enabled = False
        CheckBoxC.Enabled = False
        CheckBoxD.Enabled = False
        CheckBoxU.Enabled = False
    End Sub
    'Protected Sub DataGridComposizione_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridComposizione.ItemDataBound

    'End Sub

    'Protected Sub DataGridComposizione_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridComposizione.PageIndexChanged
    '    If e.NewPageIndex >= 0 Then

    '        DataGridComposizione.CurrentPageIndex = e.NewPageIndex
    '        AggiustaCompSessione()
    '        DataGridComposizione.DataSource = Session.Item("dtComp")
    '        DataGridComposizione.DataBind()
    '        If DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "ATTIVO" Or DirectCast(Me.Page.FindControl("lblStato"), Label).Text = "CHIUSO" Then
    '            SoloLettura()
    '        End If

    '    End If
    'End Sub
    Public Sub AggiustaCompSessione()
        Dim dt As Data.DataTable = Session.Item("dtComp")
        Dim r As Data.DataRow
        For i As Integer = 0 To DataGridComposizione.Items.Count - 1
            If DirectCast(DataGridComposizione.Items(i).Cells(1).FindControl("CheckBox1"), RadButton).Checked = False Then
                'dt.Rows(i).Item("CHECKED") = 0
                r = dt.Select("id = " & DataGridComposizione.Items(i).Cells(par.IndRDGC(DataGridComposizione, "ID")).Text)(0)
                r.Item("CHECKED") = 0
            Else
                'dt.Rows(i).Item("CHECKED") = 1
                r = dt.Select("id = " & DataGridComposizione.Items(i).Cells(par.IndRDGC(DataGridComposizione, "ID")).Text)(0)
                r.Item("CHECKED") = 1
            End If

        Next
        'Label3.Text = "0"
        Session.Item("dtComp") = dt
    End Sub

   

    Protected Sub ricaricaComposizione_Click(sender As Object, e As System.EventArgs) Handles ricaricaComposizione.Click
        'CaricaLista()
        DataGridComposizione.Rebind()
        CType(Me.Page, Object).Tabber7 = "tabbertabdefault"
        If Not IsNothing(Session.Item("MODIFICATO")) Then
            If Session.Item("MODIFICATO") = 1 Then
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
            End If
        End If
        Session.Remove("MODIFICATO")
    End Sub

    Protected Sub ButtonSelAll_Click(sender As Object, e As System.EventArgs)

        Try
            If hiddenSelTutti.Value = "1" Then
                For Each riga As GridItem In DataGridComposizione.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = True
                Next
            Else
                For Each riga As GridItem In DataGridComposizione.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = False
                Next
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ButtonSelAll_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridComposizione_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGridComposizione.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
                DataGridComposizione.MasterTableView.GetColumn("ID").Visible = False
                Dim dt As Data.DataTable = CType(Session.Item("dtComp"), Data.DataTable)
                Dim dtDaEsportare As New Data.DataTable
                dtDaEsportare.Columns.Add("DENOMINAZIONE")
                Dim riga As Data.DataRow
                For Each elemento As Data.DataRow In dt.Rows
                    If elemento.Item("CHECKED") = "1" Then
                        riga = dtDaEsportare.NewRow
                        riga.Item("DENOMINAZIONE") = elemento.Item("DENOMINAZIONE")
                        dtDaEsportare.Rows.Add(riga)
                    End If
                Next
                Dim nomeFile As String = par.EsportaExcelDaDT(dtDaEsportare, "ExportComposizione")
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    Response.Redirect("../../../FileTemp/" & nomeFile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

   
    Protected Sub DataGridComposizione_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridComposizione.NeedDataSource
        Try
            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            'SEGN. 2797/2018
            'Puccia
            'Aggiunto controllo della presenza data fine >= NOW in quanto in VariazConfig.vb Setta DATA_FINE = NOW

            If DirectCast(Me.Page.FindControl("TipoLotto"), HiddenField).Value = "E" Then
                par.cmd.CommandText = "SELECT EDIFICI.ID, DENOMINAZIONE,(CASE WHEN EDIFICI.ID IN (SELECT ID_EDIFICIO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " and nvl(d_fine,99991231)>to_char(sysdate,'YYYYMMDD') ) THEN 1 ELSE 0 END) AS CHECKED,(SELECT complessi_immobiliari.ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE complessi_immobiliari.id=EDIFICI.ID_COMPLESSO) AS ID_FILIALE FROM SISCOM_MI.EDIFICI, SISCOM_MI.LOTTI_PATRIMONIO WHERE EDIFICI.ID = LOTTI_PATRIMONIO.ID_EDIFICIO AND LOTTI_PATRIMONIO.ID_LOTTO =" & DirectCast(Me.Page.FindControl("txtidlotto"), HiddenField).Value _
                    & " ORDER BY DENOMINAZIONE ASC"
            Else
                par.cmd.CommandText = "SELECT IMPIANTI.ID, (TIPOLOGIA_IMPIANTI.DESCRIZIONE|| ' - - ' || IMPIANTI.DESCRIZIONE ) AS DENOMINAZIONE " _
                                    & ",(CASE WHEN IMPIANTI.ID IN (SELECT ID_IMPIANTO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & " and nvl(d_fine,99991231)>to_char(sysdate,'YYYYMMDD') ) THEN 1 ELSE 0 END) AS CHECKED,'' as ID_FILIALE " _
                                    & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE IMPIANTI.ID = LOTTI_PATRIMONIO.ID_IMPIANTO " _
                                    & "AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA AND LOTTI_PATRIMONIO.ID_LOTTO =" & DirectCast(Me.Page.FindControl("txtidlotto"), HiddenField).Value _
                                    & " ORDER BY 2"
                Me.DataGridComposizione.Columns(2).HeaderText = "IMPIANTO"
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            Session.Add("dtComp", dt)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - DataGridComposizione_NeedDataSource - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub

    Protected Sub btnVariazConf_Click(sender As Object, e As System.EventArgs) Handles btnVariazConf.Click
        'CaricaLista()
        DataGridComposizione.Rebind()
        CType(Me.Page, Object).Tabber7 = "tabbertabdefault"
        'Response.Write("<script>alert('La Composizione Patrimoniale dell\'appalto è stata correttamente modificata!');</script>")
        If Not IsNothing(Session.Item("MODIFICATO")) Then
            If Session.Item("MODIFICATO") = 1 Then
                DirectCast(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
            End If
        End If
        Session.Remove("MODIFICATO")
    End Sub


    Protected Sub CheckBoxA_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxA.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtComp")
                If CheckBoxA.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 101 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 101 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtComp") = dt
                DataGridComposizione.DataSource = Session.Item("dtComp")
                DataGridComposizione.Rebind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxb_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxB.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtComp")
                If CheckBoxb.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 102 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 102 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtComp") = dt
                DataGridComposizione.DataSource = Session.Item("dtComp")
                DataGridComposizione.Rebind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxc_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxC.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtComp")
                If CheckBoxc.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 103 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 103 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtComp") = dt
                DataGridComposizione.DataSource = Session.Item("dtComp")
                DataGridComposizione.Rebind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxd_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxD.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtComp")
                If CheckBoxd.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 104 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 104 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtComp") = dt
                DataGridComposizione.DataSource = Session.Item("dtComp")
                DataGridComposizione.Rebind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxu_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxU.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtComp")
                If CheckBoxu.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        row.Item("CHECKED") = 1
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        row.Item("CHECKED") = 0
                    Next
                End If
                Session.Item("dtComp") = dt
                DataGridComposizione.DataSource = Session.Item("dtComp")
                DataGridComposizione.Rebind()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
