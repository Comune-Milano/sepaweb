Imports System.IO
Partial Class Contratti_Pagamenti_VisualIncasso
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            idContratto.Value = Request.QueryString("IDCONT")
            vIdAnagrafica.Value = Request.QueryString("IDANA")
            vIdConnessione.Value = Request.QueryString("IDCONN")
            Me.idIncasso.Value = Request.QueryString("IDINCASSO")
            Me.flAnnullata.Value = Request.QueryString("FLANNULLATO")
            CaricaTitolo()
            Me.rdbDettaglio.SelectedValue = 0
            DatiIncasso(0)
            If Request.QueryString("SL") = 1 Then
                Me.btnEdit.Visible = False
            End If
        End If

    End Sub
    Private Sub DatiIncasso(ByVal tipo As Integer, Optional isExport As Boolean = False)
        Try
            connData.apri(False)
            Dim condTipo As String = ""
            Dim condNoReport As String = ""
            If tipo = 0 Then
                condTipo = " and id_bolletta_ric is null and id_rateizzazione is null "
            ElseIf tipo = 1 Then
                condNoReport = " and fl_no_report = 0"

            End If
            par.cmd.CommandText = "SELECT num_bolletta," _
                                & "(case when id_bolletta_ric is not null then 'RICLASSIFICATA' ELSE (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_BOLLETTE WHERE ID = BOL_BOLLETTE.ID_TIPO)END) AS TIPO," _
                                & "Getdata(BOL_BOLLETTE.RIFERIMENTO_DA) AS RIF_DA,Getdata(BOL_BOLLETTE.RIFERIMENTO_A) AS RIF_A, " _
                                & "T_VOCI_BOLLETTA.DESCRIZIONE AS VOCE," _
                                & "to_char((SELECT SUM(importo_pagato) FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI b WHERE b.id_voce_bolletta = BOL_BOLLETTE_VOCI.ID AND b.id_incasso_extramav =  " & idIncasso.Value & " ) ,'9G999G999G990D99')AS PAG_INCASSO " _
                                & "FROM siscom_mi.BOL_BOLLETTE_VOCI,siscom_mi.BOL_BOLLETTE,siscom_mi.T_VOCI_BOLLETTA " _
                                & "WHERE T_VOCI_BOLLETTA.ID = BOL_BOLLETTE_VOCI.ID_VOCE AND BOL_BOLLETTE.ID = BOL_BOLLETTE_VOCI.id_bolletta AND " _
                                & "BOL_BOLLETTE_VOCI.ID IN (SELECT DISTINCT id_voce_bolletta FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE id_incasso_extramav = " & idIncasso.Value & " " & condNoReport & ") " _
                                & condTipo _
                                & "ORDER BY DATA_EMISSIONE DESC"
            'par.cmd.CommandText = "SELECT BOL_BOLLETTE.NUM_BOLLETTA,T_VOCI_BOLLETTA.DESCRIZIONE AS VOCE, " _
            '                    & "TRIM(TO_CHAR(BOL_BOLLETTE_VOCI.IMPORTO,'9G999G999G990D99')) as IMPORTO,TRIM(TO_CHAR(BOL_BOLLETTE_VOCI_PAGAMENTI.IMPORTO_PAGATO,'9G999G999G990D99')) AS PAG_INCASSO FROM " _
            '                    & "siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI, " _
            '                    & "siscom_mi.BOL_BOLLETTE_VOCI, " _
            '                    & "siscom_mi.BOL_BOLLETTE , " _
            '                    & "siscom_mi.T_VOCI_BOLLETTA " _
            '                    & "WHERE BOL_BOLLETTE_VOCI_PAGAMENTI.id_voce_bolletta =BOL_BOLLETTE_VOCI.ID " _
            '                    & "AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID " _
            '                    & "AND T_VOCI_BOLLETTA.ID = BOL_BOLLETTE_VOCI.ID_VOCE " _
            '                    & "AND id_incasso_extramav = " & idIncasso.Value & " and bol_bollette_voci_pagamenti.fl_no_report = 0 " _
            '                    & "ORDER BY num_bolletta ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim totEmesso As Decimal = 0
            Dim totIncasso As Decimal = 0
            For Each r As Data.DataRow In dt.Rows
                'totEmesso += CDec((par.IfNull(r.Item("IMPORTO"), 0)))
                totIncasso += CDec((par.IfNull(r.Item("PAG_INCASSO"), 0)))
            Next
            Dim row As Data.DataRow
            row = dt.NewRow()
            row.Item("NUM_BOLLETTA") = "TOTALE"
            'row.Item("IMPORTO") = Format(totEmesso, "##,##0.00")
            row.Item("PAG_INCASSO") = Format(totIncasso, "##,##0.00")
            dt.Rows.Add(row)

            If isExport = False Then
                dgvIncasso.DataSource = dt
                dgvIncasso.DataBind()
                Me.dgvIncasso.Items(dt.Rows.Count - 1).Cells(0).Font.Bold = True
                Me.dgvIncasso.Items(dt.Rows.Count - 1).Cells(0).Font.Italic = True
                Me.dgvIncasso.Items(dt.Rows.Count - 1).Cells(0).Font.Underline = True

            Else
                Dim xls As New ExcelSiSol

                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExpIncasso", "ExpIncasso", dt, True, , True)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "downloadFile('../../FileTemp/" & nomeFile & "');", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
                End If

            End If
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:DatiIncasso - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)


        End Try
    End Sub
    Private Sub CaricaTitolo()
        Try
            connData.apri(False)

            Dim Anagrafica As String = ""
            par.cmd.CommandText = "SELECT TIPO_PAG_PARZ.descrizione AS tipo_incasso,motivo_pagamento, " _
                                & "siscom_mi.Getintestatari(id_contratto) as inte,siscom_mi.Getdata(data_pagamento) AS data_pagamento, " _
                                & "siscom_mi.Getdataora(data_ora) AS operazione FROM siscom_mi.INCASSI_EXTRAMAV,siscom_mi.TIPO_PAG_PARZ " _
                                & "WHERE TIPO_PAG_PARZ.ID = id_tipo_pag AND INCASSI_EXTRAMAV.ID = " & idIncasso.Value

            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                Me.lblTitolo.Text = "DETTAGLI INCASSO DEL: " & reader("OPERAZIONE").ToString & " <br/>" _
                                  & "DATA PAGAMENTO:" & reader("data_pagamento").ToString & "<br/>" _
                                  & "INTESTATARIO CONTRATTO: " & reader("INTE").ToString
            End If
            reader.Close()
            connData.chiudi(False)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:CaricaTitolo - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEdit.Click
        Session.Add("MODINCA", 1)
        Response.Write("<script>self.close();</script>")


    End Sub

    Protected Sub btnEdit0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEdit0.Click
        DatiIncasso(rdbDettaglio.SelectedValue, True)
    End Sub

    Protected Sub rdbDettaglio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbDettaglio.SelectedIndexChanged
        DatiIncasso(rdbDettaglio.SelectedValue, False)
    End Sub
End Class
