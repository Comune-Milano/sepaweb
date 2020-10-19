Imports System.IO
Partial Class Contratti_Pagamenti_VisualIncassoRuoli
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

            DatiIncasso(0)
            If Request.QueryString("SL") = 1 Then
                Me.btnEdit.Visible = False
            End If
        End If

    End Sub
    Private Sub DatiIncasso(Optional isExport As Boolean = False)
        Try
            connData.apri(False)

            par.cmd.CommandText = "SELECT num_bolletta," _
                                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_BOLLETTE WHERE ID = BOL_BOLLETTE.ID_TIPO) AS TIPO," _
                                & "Getdata(BOL_BOLLETTE.RIFERIMENTO_DA) AS RIF_DA,Getdata(BOL_BOLLETTE.RIFERIMENTO_A) AS RIF_A, " _
                                & "to_char((SELECT SUM(importo_pagato) FROM siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO b WHERE b.id_bolletta = BOL_BOLLETTE.ID AND b.ID_INCASSO_RUOLO =  " & idIncasso.Value & " ) ,'9G999G999G990D99')AS PAG_INCASSO " _
                                & "FROM siscom_mi.BOL_BOLLETTE " _
                                & "WHERE " _
                                & "BOL_BOLLETTE.ID IN (SELECT DISTINCT id_bolletta FROM siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO WHERE ID_INCASSO_RUOLO = " & idIncasso.Value & " ) " _
                                & "ORDER BY DATA_EMISSIONE DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim totEmesso As Decimal = 0
            Dim totIncasso As Decimal = 0
            For Each r As Data.DataRow In dt.Rows
                totIncasso += CDec((par.IfNull(r.Item("PAG_INCASSO"), 0)))
            Next
            Dim row As Data.DataRow
            row = dt.NewRow()
            row.Item("NUM_BOLLETTA") = "TOTALE"
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

                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExpIncassoR", "ExpIncassoR", dt, True, , True)
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
            par.cmd.CommandText = "SELECT TIPO_PAG_RUOLO.descrizione AS tipo_incasso,motivo_pagamento, " _
                                & "siscom_mi.Getintestatari(id_contratto) as inte,siscom_mi.Getdata(data_pagamento) AS data_pagamento, " _
                                & "siscom_mi.Getdataora(data_ora) AS operazione FROM siscom_mi.INCASSI_RUOLI,siscom_mi.TIPO_PAG_RUOLO " _
                                & "WHERE TIPO_PAG_RUOLO.ID = id_tipo_pag AND INCASSI_RUOLI.ID = " & idIncasso.Value

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
        DatiIncasso(True)
    End Sub

End Class
