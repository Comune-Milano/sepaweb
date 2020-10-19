
Partial Class Contratti_Pagamenti_EventiIncassi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            lblTitolo.Text = "ELENCO INCASSI PAGAMENTI PARZIALI"

            CaricaIncassi()

            'CaricaEventiPagParziali()
            'CaricaEventiPagAnnullo()

        End If

    End Sub
    Private Sub CaricaIncassi()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT incassi_extramav.ID,id_operatore, " _
                    & "(SELECT TO_DATE(MIN(data_ora),'yyyyMMddHH24MISS') FROM siscom_mi.eventi_pagamenti_parziali WHERE id_incasso_extramav = incassi_extramav.ID) AS data_ora, " _
                    & "sepa.OPERATORI.operatore, sepa.CAF_WEB.cod_caf,tipo_pag_parz.descrizione || (case when nvl(tipo_pag_parz.id,0) = 5 then ' num. ' || numero_assegno else '' end) AS tipo, " _
                    & "motivo_pagamento,TO_CHAR(TO_DATE (DATA_pagamento, 'yyyyMMdd'),'dd/mm/yyyy') AS data_pagamento, TO_CHAR(TO_DATE (riferimento_da, 'yyyyMMdd'),'dd/mm/yyyy') AS riferimento_da, " _
                    & "TO_CHAR(TO_DATE (riferimento_a, 'yyyyMMdd'), 'dd/mm/yyyy' ) AS riferimento_a, " _
                    & "TRIM (TO_CHAR(importo,'9G999G999G990D99')) AS importo,FL_ANNULLATA " _
                    & "FROM siscom_mi.tipo_pag_parz,siscom_mi.incassi_extramav, " _
                    & "sepa.CAF_WEB,sepa.OPERATORI " _
                    & "WHERE siscom_mi.tipo_pag_parz.ID(+) = incassi_extramav.id_tipo_pag AND " _
                    & "incassi_extramav.ID_CONTRATTO  = " & Request.QueryString("IDCONT").ToString & " AND incassi_extramav.id_operatore = sepa.OPERATORI.ID " _
                    & "AND sepa.CAF_WEB.ID = sepa.OPERATORI.id_caf " _
                    & "ORDER BY ID desc"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()



            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:CaricaIncassi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                par.cmd.CommandText = "SELECT   eventi_pagamenti_parziali.ID," _
                                    & "TO_DATE (siscom_mi.eventi_pagamenti_parziali.data_ora, 'yyyyMMddHH24MISS') AS DATA_ORA, cod_evento," _
                                    & "siscom_mi.TAB_EVENTI.descrizione AS evento, motivazione AS DESC_EVENTO," _
                                    & "CASE WHEN eventi_pagamenti_parziali.importo IS NULL THEN ((SELECT TRIM" _
                                    & "(TO_CHAR(SUM (eventi_pagamenti_parz_dett.importo),  '9G999G999G990D99') )" _
                                    & "FROM siscom_mi.eventi_pagamenti_parz_dett," _
                                    & "siscom_mi.bol_bollette_voci," _
                                    & "siscom_mi.bol_bollette " _
                                    & "WHERE id_evento_principale = eventi_pagamenti_parziali.ID " _
                                    & "AND bol_bollette.ID = bol_bollette_voci.id_bolletta " _
                                    & "AND bol_bollette_voci.ID = eventi_pagamenti_parz_dett.id_voce_bolletta " _
                                    & "AND bol_bollette.id_bolletta_ric IS NULL " _
                                    & "AND bol_bollette.id_rateizzazione IS NULL)) " _
                                    & "ELSE TRIM (TO_CHAR (eventi_pagamenti_parziali.importo, '9G999G999G990D99')) " _
                                    & "END AS importo_EVENTO " _
                                    & "FROM siscom_mi.eventi_pagamenti_parziali,siscom_mi.TAB_EVENTI " _
                                    & "WHERE siscom_mi.eventi_pagamenti_parziali.cod_evento = siscom_mi.TAB_EVENTI.cod " _
                                    & "AND siscom_mi.eventi_pagamenti_parziali.id_incasso_extramav = " & e.Item.Cells(TrovaIndiceColonna(DataGrid1, "ID")).Text & " " _
                                    & "ORDER BY eventi_pagamenti_parziali.data_ora asc," _
                                    & "eventi_pagamenti_parziali.cod_evento asc"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim dt As New Data.DataTable
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    GeneraTabeEventi(dt, e)
                Else
                    e.Item.Cells(0).Text = ""
                End If

                If e.Item.Cells(TrovaIndiceColonna(DataGrid1, "FL_ANNULLATA")).Text = 1 Then
                    e.Item.BackColor = Drawing.Color.Red
                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:CaricaIncassi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub
    Private Sub GeneraTabeEventi(ByVal dtEventi As Data.DataTable, e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim NewDg As New DataGrid
        Dim hc As New HyperLinkColumn
        Dim dataOra As New BoundColumn
        Dim evento As New BoundColumn
        Dim motivazione As New BoundColumn
        Dim importo As New BoundColumn
        Dim id As New BoundColumn


        AddHandler NewDg.ItemDataBound, AddressOf newDgv_ItemDataBound

        NewDg.ID = "DgDettaglio"

        NewDg.AutoGenerateColumns = False

        hc.Text = "+"

        dataOra.DataField = "DATA_ORA"
        dataOra.HeaderText = "DATA ORA"

        evento.DataField = "EVENTO"
        evento.HeaderText = "EVENTO"

        motivazione.DataField = "DESC_EVENTO"
        motivazione.HeaderText = "DESCRIZIONE"

        importo.DataField = "IMPORTO_EVENTO"
        importo.HeaderText = "IMPORTO"

        id.DataField = "ID"
        id.HeaderText = "ID"


        NewDg.Columns.Add(hc)
        NewDg.Columns.Add(dataOra)
        NewDg.Columns.Add(evento)
        NewDg.Columns.Add(motivazione)
        NewDg.Columns.Add(importo)
        NewDg.Columns.Add(id)

        NewDg.Columns(5).Visible = False
        NewDg.Width = Unit.Percentage(100)
        NewDg.DataSource = dtEventi
        NewDg.DataBind()

        SetFiglioProps(NewDg)



        Dim sw As New System.IO.StringWriter
        Dim htw As New System.Web.UI.HtmlTextWriter(sw)
        NewDg.RenderControl(htw)

        Dim DivStart As String = "<DIV id='uniquename" + e.Item.ItemIndex.ToString() + "' style='DISPLAY: none;'>"
        Dim DivBody As String = sw.ToString()
        Dim DivEnd As String = "</DIV>"
        Dim FullDIV As String = DivStart + DivBody + DivEnd

        Dim LastCellPosition As Integer = e.Item.Cells.Count - 4
        Dim NewCellPosition As Integer = e.Item.Cells.Count

        e.Item.Cells(0).ID = "CellInfo" + e.Item.ItemIndex.ToString()

        If e.Item.ItemType = ListItemType.Item Then
            e.Item.Cells(LastCellPosition).Text() = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='f5f5f5'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
        Else
            e.Item.Cells(LastCellPosition).Text = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='d3d3d3'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
        End If
        e.Item.Cells(0).Attributes("onclick") = "HideShowPanel('uniquename" + e.Item.ItemIndex.ToString() + "'); ChangePlusMinusText('" + e.Item.Cells(0).ClientID + "'); "
        e.Item.Cells(0).Attributes("onmouseover") = "this.style.cursor='pointer'"
        e.Item.Cells(0).Attributes("onmouseout") = "this.style.cursor='pointer'"



    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If TypeOf c Is System.Web.UI.WebControls.BoundColumn Then
                    If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                        TrovaIndiceColonna = Indice
                        Exit For
                    End If
                End If

                Indice = Indice + 1

            Next

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:CaricaIncassi " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

        Return TrovaIndiceColonna

    End Function

    Protected Sub newDgv_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) 'Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then


            par.cmd.CommandText = "SELECT NUM_BOLLETTA, T_VOCI_BOLLETTA.DESCRIZIONE AS VOCE,trim(to_char(EVENTI_PAGAMENTI_PARZ_DETT.IMPORTO,'9G999G999G990D99'))as IMPORTO " _
                        & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA,SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
                        & "WHERE EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA = BOL_BOLLETTE_VOCI.ID(+) AND BOL_BOLLETTE_VOCI.ID_VOCE=T_VOCI_BOLLETTA.ID(+) " _
                        & "AND ID_EVENTO_PRINCIPALE = " & e.Item.Cells(5).Text & " AND BOL_BOLLETTE.ID(+) = BOL_BOLLETTE_VOCI.ID_BOLLETTA " _
                        & "AND (SELECT data_ora FROM siscom_mi.EVENTI_PAGAMENTI_PARZIALI WHERE ID = EVENTI_PAGAMENTI_PARZ_DETT.id_evento_principale)<NVL((SELECT NVL(data_emissione,'99991231') FROM siscom_mi.BOL_BOLLETTE b WHERE b.ID = NVL(BOL_BOLLETTE.id_bolletta_ric,0)),'99991231') " _
                        & "AND (SELECT data_ora FROM siscom_mi.EVENTI_PAGAMENTI_PARZIALI WHERE ID = EVENTI_PAGAMENTI_PARZ_DETT.id_evento_principale)<NVL((SELECT NVL(data_emissione,'99991231') FROM siscom_mi.BOL_RATEIZZAZIONI b WHERE b.ID = NVL(BOL_BOLLETTE.id_rateizzazione,0)),'99991231') " _
                        & "ORDER BY NUM_BOLLETTA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable
            da.Fill(dt)


            If dt.Rows.Count > 0 Then

                Dim NewDg As New DataGrid
                NewDg.AutoGenerateColumns = True


                NewDg.Width = Unit.Percentage(100)
                dt.Columns(0).ColumnName = "NUM. BOLLETTA"
                dt.Columns(2).ColumnName = "IMPORTO PAGATO"
                NewDg.DataSource = dt
                NewDg.DataBind()

                SetProps(NewDg)

                Dim NumBolletta As String = ""
                For Each di As DataGridItem In NewDg.Items
                    di.Cells(0).HorizontalAlign = HorizontalAlign.Left
                    di.Cells(2).HorizontalAlign = HorizontalAlign.Right
                    di.Cells(2).Text = di.Cells(2).Text
                Next

                Dim sw As New System.IO.StringWriter
                Dim htw As New System.Web.UI.HtmlTextWriter(sw)
                NewDg.RenderControl(htw)

                Dim DivStart As String = "<DIV id='nipote" + e.Item.Cells(5).Text + e.Item.ItemIndex.ToString() + "' style='DISPLAY: none;'>"
                Dim DivBody As String = sw.ToString()
                Dim DivEnd As String = "</DIV>"
                Dim FullDIV As String = DivStart + DivBody + DivEnd

                Dim LastCellPosition As Integer = e.Item.Cells.Count - 2
                Dim NewCellPosition As Integer = e.Item.Cells.Count 

                e.Item.Cells(0).ID = "CellInfo" + e.Item.Cells(5).Text + e.Item.ItemIndex.ToString()

                If e.Item.ItemType = ListItemType.Item Then
                    e.Item.Cells(LastCellPosition).Text() = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='f5f5f5'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
                Else
                    e.Item.Cells(LastCellPosition).Text = e.Item.Cells(LastCellPosition).Text + "</td><tr><td bgcolor='d3d3d3'></td><td colspan='" + NewCellPosition.ToString() + "'>" + FullDIV
                End If

                e.Item.Cells(0).Attributes("onclick") = "HideShowPanel('nipote" + e.Item.Cells(5).Text + e.Item.ItemIndex.ToString() + "'); ChangePlusMinusText('" + e.Item.Cells(0).ClientID + "'); "
                e.Item.Cells(0).Attributes("onmouseover") = "this.style.cursor='pointer'"
                e.Item.Cells(0).Attributes("onmouseout") = "this.style.cursor='pointer'"
            Else
                e.Item.Cells(0).Text = ""
            End If

        End If

    End Sub

    Public Sub SetProps(ByVal DG As System.Web.UI.WebControls.DataGrid)
        '************************************************************************** 
        DG.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.Font.Bold = False
        DG.Font.Name = "Arial"

        '******************************Professional 2********************************* 

        'Border Props 
        DG.GridLines = GridLines.Both
        DG.CellPadding = 0
        DG.CellSpacing = 0
        DG.BorderColor = System.Drawing.Color.FromName("#CCCCCC")
        DG.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(1)


        'Header Props 
        DG.HeaderStyle.BackColor = System.Drawing.Color.GreenYellow
        DG.HeaderStyle.ForeColor = System.Drawing.Color.Black
        DG.HeaderStyle.Font.Bold = True
        DG.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        DG.HeaderStyle.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.HeaderStyle.Font.Name = "Arial"
        DG.ItemStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow
    End Sub


    Public Sub SetFiglioProps(ByVal DG As System.Web.UI.WebControls.DataGrid)
        '************************************************************************** 
        DG.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.Font.Bold = False
        DG.Font.Name = "Arial"

        '******************************Professional 2********************************* 

        'Border Props 
        DG.GridLines = GridLines.Both
        DG.CellPadding = 0
        DG.CellSpacing = 0
        DG.BorderColor = System.Drawing.Color.FromName("#D3D3D3")
        DG.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2)


        'Header Props 
        DG.HeaderStyle.BackColor = System.Drawing.Color.SteelBlue '
        DG.HeaderStyle.ForeColor = System.Drawing.Color.White '
        DG.HeaderStyle.Font.Bold = True
        DG.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
        DG.HeaderStyle.Font.Size = System.Web.UI.WebControls.FontUnit.Parse("8")
        DG.HeaderStyle.Font.Name = "Arial"
        DG.ItemStyle.BackColor = System.Drawing.Color.LightBlue
        DG.Columns(4).ItemStyle.HorizontalAlign = HorizontalAlign.Right
    End Sub

End Class
