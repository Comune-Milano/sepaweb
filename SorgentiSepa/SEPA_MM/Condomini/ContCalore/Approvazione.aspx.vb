Imports System.IO

Partial Class Contratti_ContCalore_Approvazione
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim tipo As Integer

    'Public Property dt() As Data.DataTable
    '    Get
    '        If Not (ViewState("datatableNucleoRic") Is Nothing) Then
    '            Return ViewState("datatableNucleoRic")
    '        Else
    '            Return New Data.DataTable
    '        End If
    '    End Get
    '    Set(ByVal value As Data.DataTable)
    '        ViewState("datatableNucleoRic") = value
    '    End Set
    'End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        If Not IsPostBack Then
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If Request.QueryString("TIPO") = "NUOVO" Then
                Me.lblTitolo.Text = "Approvazione Preventivo Contributo Calore"
                Me.btnAvviaCalcolo.Text = "APPROVA"
                tipo = 1
                lblAnni.Text = "Elenco degli aventi diritto per l'anno: "
                par.caricaComboBox("select id,anno  from siscom_mi.cont_calore_anno where id_stato = 1", cmbAnniApprovabili, "ID", "ANNO", False)
            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                Me.lblTitolo.Text = "Approvazione Consuntivo Contributo Calore"
                Me.btnAvviaCalcolo.Text = "APPROVA CONSUNTIVO"
                tipo = 3
                lblAnni.Text = "Elenco consuntivo per l'anno: "
                par.caricaComboBox("select id,anno  from siscom_mi.cont_calore_anno where id_stato = 3", cmbAnniApprovabili, "ID", "ANNO", False)

            End If
            'par.cmd.CommandText = "select max( id_cont_calore )from siscom_mi.cont_calore_anno where id_stato = 1 "
            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettore.Read Then
            '    idContCalore.Value = par.IfNull(lettore(0), 0)
            'End If
            'lettore.Close()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If

            If Me.cmbAnniApprovabili.Items.Count > 0 Then
                Me.idContCalore.Value = Me.cmbAnniApprovabili.SelectedValue
            End If
            If idContCalore.Value > 0 Then
                CaricaAventiDiritto(idContCalore.Value, tipo)
            Else
                Response.Write("<script>alert('Nessun contributo calore da approvare!');parent.main.location.replace('../pagina_home.aspx');</script>")
            End If

        End If
    End Sub
    Private Sub CaricaAventiDiritto(ByVal idContCalore As Integer, ByVal tipoCalcolo As Integer)
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ID_CONT_CALORE,nvl(ID_CONDOMINIO,-1) as id_condominio,CONT_CALORE_ELABORAZIONE.ID_CONTRATTO,CONT_CALORE_ELABORAZIONE.ID_ANAGRAFICA,CONT_CALORE_ELABORAZIONE.ID_UNITA, " _
                                & "RAPPORTI_UTENZA.COD_CONTRATTO, CONT_CALORE_ANNO.ANNO," _
                                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END ) AS NOMINATIVO, " _
                                & "TIPO_CALCOLO_CONT_CALORE.DESCRIZIONE AS TIPO_CALCOLO, " _
                                & " CONT_CALORE_ELABORAZIONE.IMPORTO_RICONOSCIUTO,0 as SELECTED " _
                                & "FROM SISCOM_MI.CONT_CALORE_ELABORAZIONE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.TIPO_CALCOLO_CONT_CALORE, " _
                                & "siscom_mi.cont_calore_anno,SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                                & "WHERE ID_CONT_CALORE = " & idContCalore & " " _
                                & "AND TIPO_CALCOLO = " & tipoCalcolo & " " _
                                & "and cont_calore_anno.id = cont_calore_elaborazione.id_cont_calore " _
                                & "AND RAPPORTI_UTENZA.ID = CONT_CALORE_ELABORAZIONE.ID_CONTRATTO " _
                                & "AND TIPO_CALCOLO_CONT_CALORE.ID = CONT_CALORE_ELABORAZIONE.TIPO_CALCOLO " _
                                & "AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA " _
                                & "AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                & "AND CONT_CALORE_ELABORAZIONE.STATO = -1 " _
                                & "ORDER BY NOMINATIVO ASC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            If dt.Rows.Count > 0 Then

                Me.dgvApprovazione.DataSource = dt
                Me.dgvApprovazione.DataBind()

                Session.Add("dtses", dt)
            Else
                Response.Write("<script>alert('Nessun contributo calore da approvare!');</script>")
                Response.Write("<script>parent.main.location.replace('../pagina_home.aspx');</script>")

            End If
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>Approva" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub

    Protected Sub btnAvviaCalcolo_Click(sender As Object, e As System.EventArgs) Handles btnAvviaCalcolo.Click
        AggiustaSelSession()
        Approva()

    End Sub
    Private Sub Approva()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim TipoCalcolo As Integer = 0
            If Request.QueryString("TIPO") = "NUOVO" Then
                TipoCalcolo = 2
            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                TipoCalcolo = 4

            End If

            '**********apertura transazione
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            Dim dt As Data.DataTable = Session.Item("dtses")
            Dim Stato As Integer = 0
            For Each row As Data.DataRow In dt.Rows

                If row.Item("SELECTED") = 1 Then
                    If row.Item("ID_CONDOMINIO") <> "-1" Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.CONT_CALORE_ELABORAZIONE SET ID_CONDOMINIO = " & RitornaNullSeMenoUno(row.Item("ID_CONDOMINIO")) & ", TIPO_CALCOLO = " & TipoCalcolo & ", STATO = " & row.Item("SELECTED") & " WHERE " _
                                            & "ID_CONT_CALORE = " & row.Item("id_cont_calore") & " AND ID_ANAGRAFICA = " & row.Item("ID_ANAGRAFICA") & " " _
                                            & "AND ID_CONTRATTO = " & row.Item("ID_CONTRATTO") & " and tipo_calcolo = " & TipoCalcolo - 1
                        par.cmd.ExecuteNonQuery()
                    Else
                        Response.Write("<script>alert('Definire il condominio per gli aventi diritto al contributo calore!');</script>")
                        par.myTrans.Rollback()
                        Exit Sub
                    End If
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.CONT_CALORE_ELABORAZIONE SET ID_CONDOMINIO = " & RitornaNullSeMenoUno(row.Item("ID_CONDOMINIO")) & ", TIPO_CALCOLO = " & TipoCalcolo & ", STATO = " & row.Item("SELECTED") & " WHERE " _
                    & "ID_CONT_CALORE = " & row.Item("id_cont_calore") & " AND ID_ANAGRAFICA = " & row.Item("ID_ANAGRAFICA") & " " _
                    & "AND ID_CONTRATTO = " & row.Item("ID_CONTRATTO") & " and tipo_calcolo = " & TipoCalcolo - 1
                    par.cmd.ExecuteNonQuery()

                End If

            Next
            If Request.QueryString("TIPO") = "NUOVO" Then
                par.cmd.CommandText = "update siscom_mi.cont_calore_anno set id_stato = 2 where id = " & idContCalore.Value
            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                par.cmd.CommandText = "update siscom_mi.cont_calore_anno set id_stato = 4 where id = " & idContCalore.Value

            End If

            par.cmd.ExecuteNonQuery()
            par.myTrans.Commit()

            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If
            Response.Write("<script>alert('Operazione completata, ed eseguita correttamete!');</script>")
            Response.Write("<script>parent.main.location.replace('../pagina_home.aspx');</script>")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>Approva" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            par.myTrans.Rollback()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)

            End If

        End Try
    End Sub

    Protected Sub dgvApprovazione_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvApprovazione.ItemCommand

    End Sub

    Protected Sub dgvApprovazione_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvApprovazione.ItemCreated


    End Sub

    Protected Sub dgvApprovazione_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvApprovazione.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If Request.QueryString("TIPO") = "NUOVO" Then
                par.caricaComboBox("select id, denominazione from siscom_mi.condomini where id in (select id_condominio from siscom_mi.cond_ui where id_ui = " & e.Item.Cells(3).Text & ")", DirectCast(e.Item.FindControl("cmbCondominio"), DropDownList), "id", "denominazione", True, "-1")
                DirectCast(e.Item.Cells(0).FindControl("cmbCondominio"), DropDownList).SelectedValue = e.Item.Cells(11).Text

            ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then

                par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.CONT_CALORE_ELABORAZIONE WHERE ID_CONT_CALORE = " & idContCalore.Value & " AND TIPO_CALCOLO = 2 AND STATO = 1 AND ID_CONTRATTO =" & e.Item.Cells(1).Text
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore(0), 0) > 0 Then
                        par.caricaComboBox("select id, denominazione from siscom_mi.condomini where id = " & par.IfNull(lettore(0), 0), DirectCast(e.Item.FindControl("cmbCondominio"), DropDownList), "id", "denominazione", True, "-1")

                    End If
                Else
                    par.caricaComboBox("select id, denominazione from siscom_mi.condomini where id in (select id_condominio from siscom_mi.cond_ui where id_ui = " & e.Item.Cells(3).Text & ")", DirectCast(e.Item.FindControl("cmbCondominio"), DropDownList), "id", "denominazione", True, "-1")

                End If
                lettore.Close()
                DirectCast(e.Item.Cells(0).FindControl("cmbCondominio"), DropDownList).SelectedValue = e.Item.Cells(11).Text

            End If

        End If

    End Sub


    Protected Sub dgvApprovazione_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvApprovazione.PageIndexChanged
        If e.NewPageIndex >= 0 Then

            dgvApprovazione.CurrentPageIndex = e.NewPageIndex
            AggiustaSelSession()
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            End If
            dgvApprovazione.DataSource = Session.Item("dtses")
            dgvApprovazione.DataBind()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)


            End If
        End If

    End Sub
    Private Sub AggiustaSelSession()
        Try
            Dim r As Data.DataRow
            Dim dt As Data.DataTable = Session.Item("dtses")
            For Each di As DataGridItem In dgvApprovazione.Items
                If DirectCast(di.Cells(0).FindControl("chkSel"), CheckBox).Checked = False Then
                    r = dt.Select("id_contratto = " & di.Cells(1).Text)(0)
                    r.Item("SELECTED") = "0"

                Else
                    r = dt.Select("id_contratto = " & di.Cells(1).Text)(0)
                    r.Item("SELECTED") = "1"
                    Selezionati.Value = 1

                End If
                If DirectCast(di.Cells(0).FindControl("cmbCondominio"), DropDownList).SelectedValue <> "-1" Then
                    r = dt.Select("id_contratto = " & di.Cells(1).Text)(0)
                    r.Item("id_condominio") = DirectCast(di.Cells(0).FindControl("cmbCondominio"), DropDownList).SelectedValue
                End If
            Next
            Session.Item("dtses") = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>AggiustaSelSession" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try
    End Sub



    Protected Sub btnSelectAll_Click(sender As Object, e As System.EventArgs)
        Try
            Dim dt As Data.DataTable = Session.Item("dtses")

            If Selezionati.Value = 0 Then

                For Each r As Data.DataRow In dt.Rows
                    r.Item("SELECTED") = "1"
                Next
                Selezionati.Value = 1

            Else
                For Each r As Data.DataRow In dt.Rows
                    r.Item("SELECTED") = "0"
                Next
                Selezionati.Value = 0

            End If

            Session.Item("dtses") = dt
            dgvApprovazione.DataSource = dt
            dgvApprovazione.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnSelectAll_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")

        End Try

    End Sub

    Protected Sub cmbAnniApprovabili_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbAnniApprovabili.SelectedIndexChanged
        If Request.QueryString("TIPO") = "NUOVO" Then
            tipo = 1
        Else
            tipo = 2
        End If

        Me.idContCalore.Value = Me.cmbAnniApprovabili.SelectedValue
        If idContCalore.Value > 0 Then
            CaricaAventiDiritto(idContCalore.Value, tipo)
        Else
            Response.Write("<script>alert('Nessun contributo calore da approvare!');parent.main.location.replace('../pagina_home.aspx');</script>")
        End If

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            If Conferma.Value = 1 Then
                If dgvApprovazione.Items.Count > 0 Then
                    PreparaDataTable(Session.Item("dtses"))
                Else
                    Response.Write("<script>alert('Nessun risultato da esportare!');</script>")
                End If
                Conferma.Value = 0
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnSelectAll_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
    Private Sub PreparaDataTable(ByVal dt As Data.DataTable)
        Try
            Dim dtExport As New Data.DataTable
            PreparaDT(dtExport)
            Dim row2 As Data.DataRow
            For Each row As Data.DataRow In dt.Rows
                row2 = dtExport.NewRow()
                row2.Item("CODICE") = row.Item("COD_CONTRATTO")
                row2.Item("ANNO") = row.Item("ANNO")
                row2.Item("NOMINATIVO") = row.Item("NOMINATIVO")
                row2.Item("IMPORTO") = row.Item("IMPORTO_RICONOSCIUTO")
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                If Request.QueryString("TIPO") = "NUOVO" Then
                    par.cmd.CommandText = "select denominazione from siscom_mi.condomini where id in (select id_condominio from siscom_mi.cond_ui where id_ui = " & row.Item("ID_UNITA") & ")"
                ElseIf Request.QueryString("TIPO") = "CONGUAGLIO" Then
                    par.cmd.CommandText = "SELECT ID_CONDOMINIO FROM SISCOM_MI.CONT_CALORE_ELABORAZIONE WHERE ID_CONT_CALORE = " & idContCalore.Value & " AND TIPO_CALCOLO = 2 AND STATO = 1 AND ID_CONTRATTO =" & row.Item("ID_CONTRATTO")
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        If par.IfNull(lettore(0), 0) > 0 Then
                            par.cmd.CommandText = "select denominazione from siscom_mi.condomini where id = " & par.IfNull(lettore(0), 0)
                        End If
                    Else
                        par.cmd.CommandText = "select denominazione from siscom_mi.condomini where id in (select id_condominio from siscom_mi.cond_ui where id_ui = " & row.Item("ID_UNITA") & ")"
                    End If
                    lettore.Close()
                End If
                Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettore2.Read
                    row2.Item("CONDOMINIO") += par.IfNull(lettore2("DENOMINAZIONE"), "") & ", "
                End While
                lettore2.Close()
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    ''*********************CHIUSURA CONNESSIONE**********************
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
                End If
                If Len(row2.Item("CONDOMINIO").ToString) > 0 Then
                    row2.Item("CONDOMINIO") = Left(row2.Item("CONDOMINIO").ToString, Len(row2.Item("CONDOMINIO").ToString) - 2)
                End If
                dtExport.Rows.Add(row2)
            Next
            Esporta(dtExport)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>Esporta" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
    Private Sub PreparaDT(ByVal dt As Data.DataTable)
        Try
            '######### SVUOTA E CREA COLONNE DATATABLE #########
            dt.Clear()
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("CODICE")
            dt.Columns.Add("ANNO")
            dt.Columns.Add("NOMINATIVO")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("CONDOMINIO")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnSelectAll_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Try
            Dim nomefile As String = EsportaExcelDaDT(dt, "ExportApprovazioneContCalore", True)
            If File.Exists(Server.MapPath("..\..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/..\/FileTemp/" & nomefile, True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>btnSelectAll_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
    Private Function RitornaNullSeMenoUno(ByVal val As Integer) As String
        RitornaNullSeMenoUno = "Null"
        If val <> -1 Then
            RitornaNullSeMenoUno = val
        End If
    End Function
    Function EsportaExcelDaDT(ByVal dt As Data.DataTable, ByVal nomeFile As String, Optional ByVal EliminazioneLink As Boolean = True) As String
        Try
            Dim NumeroColonneDT As Integer = dt.Columns.Count
            Dim FileExcel As New CM.ExcelFile
            Dim indiceRighe As Long = 0
            Dim IndiceColonne As Integer = 1
            Dim IndiceIntestazione As Integer = 1
            Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                For j = 0 To NumeroColonneDT - 1 Step 1
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, IndiceIntestazione, dt.Columns.Item(j).ColumnName, 0)
                    IndiceIntestazione = IndiceIntestazione + 1
                Next
                indiceRighe = indiceRighe + 1
                For Each riga As Data.DataRow In dt.Rows
                    indiceRighe = indiceRighe + 1
                    Dim Cella As Integer = 0
                    For IndiceColonne = 0 To NumeroColonneDT - 1
                        Select Case EliminazioneLink
                            Case False
                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 4)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 0)
                                End If
                            Case True
                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                    If IndiceColonne <> 1 Then
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                    Else
                                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                    End If
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                End If
                            Case Else
                                    If IsNumeric(riga.Item(IndiceColonne)) Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                    Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                    End If
                        End Select
                        Cella = Cella + 1
                    Next
                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            Dim FileNameXls As String = FileName & ".xls"
            Return FileNameXls
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
