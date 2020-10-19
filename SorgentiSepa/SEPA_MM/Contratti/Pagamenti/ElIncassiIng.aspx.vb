
Partial Class Contratti_Pagamenti_ElIncassiIng
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
            CaricaIncassi()
            If Request.QueryString("SL") = 1 Then
                Me.tblBtnElInca.Style.Value += " display:none; "
                SoloLett.Value = 1
            Else
                SoloLett.Value = 0
            End If
        Else
            If flReload.Value = 1 Then
                CaricaIncassi()
                flReload.Value = 0
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "opener.validNavigation=true;opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();", True)

                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "x", "validNavigation=true;", True)
            End If
        End If
    End Sub

    Private Sub CaricaIncassi()
        Try
            Dim errore As String = ""
            connData.apri(False)
            par.cmd.CommandText = "SELECT   INCASSI_INGIUNZIONE.ID, id_operatore, Getdataora (data_ora) AS data_ora, " _
                & " OPERATORI.operatore, " _
                & " TIPO_PAG_INGIUNZIONE.descrizione AS tipo_pagamento, " _
                & " motivo_pagamento , data_ora as ordDataOra,Getdata (data_pagamento) AS data_pagamento," _
                & " TRIM (TO_CHAR(importo,'9G999G999G990D99')) AS importo," _
                & " FL_ANNULLATA " _
                & " FROM SISCOM_MI.INCASSI_INGIUNZIONE, OPERATORI, SISCOM_MI.TIPO_PAG_INGIUNZIONE " _
                & " WHERE INCASSI_INGIUNZIONE.id_operatore = OPERATORI.ID " _
                & " AND TIPO_PAG_INGIUNZIONE.ID = INCASSI_INGIUNZIONE.id_tipo_pag " _
                & " AND INCASSI_INGIUNZIONE.id_contratto =" & idContratto.Value _
                & "ORDER BY ordDataOra DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            dgvIncassi.DataSource = dt
            dgvIncassi.DataBind()

            Me.lblTitolo.Text = "Elenco Incassi Registrati Boll. Ingiunte"
            connData.chiudi(False)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaIncassi - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub dgvIncassi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvIncassi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('txtmia').value='Hai selezionato il pagamento avvenuto il " & e.Item.Cells(1).Text.Replace("'", "\'").Replace("&nbsp;", "") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "';document.getElementById('flAnnullata').value='" & e.Item.Cells(par.IndDGC(dgvIncassi, "FL_ANNULLATA")).Text.Replace("&nbsp;", "") & "';")
            If Request.QueryString("SL") = 0 Then
                e.Item.Attributes.Add("onDblclick", "document.getElementById('btnEdit').click();")
            End If

            If e.Item.Cells(par.IndDGC(dgvIncassi, "FL_ANNULLATA")).Text = 1 Then
                e.Item.BackColor = Drawing.Color.Red
            End If

        End If
    End Sub

    Private Function EliminaPagamento() As Boolean
        EliminaPagamento = False

        par.cmd.CommandText = "update SISCOM_MI.INCASSI_INGIUNZIONE set fl_annullata = 1 where id = " & idSelected.Value
        par.cmd.ExecuteNonQuery()
        par.cmd.CommandText = "select data_pagamento from SISCOM_MI.INCASSI_INGIUNZIONE where id = " & idSelected.Value
        Dim dataPagIncasso As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "SELECT id_bolletta,sum(nvl(importo_pagato,0)) as importo_pagato,ID_TIPO_PAGAMENTO FROM SISCOM_MI.BOL_BOLLETTE_PAGAMENTI_ING WHERE ID_INCASSO_ING = " & idSelected.Value & " group by id_bolletta,ID_TIPO_PAGAMENTO"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        For Each r As Data.DataRow In dt.Rows
            par.cmd.CommandText = "update SISCOM_MI.bol_bollette set imp_ingiunzione_pag = (nvl(imp_ingiunzione_pag,0) + " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & ") where id = " & r.Item("id_bolletta")
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_PAGAMENTI_ING (ID_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_ING,DATA_OPERAZIONE) VALUES " _
                    & "(" & r.Item("id_bolletta") & ",'" & dataPagIncasso & "'," & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & "," & r.Item("ID_TIPO_PAGAMENTO") & "," & idSelected.Value & ",'" & Format(Now, "yyyyMMdd") & "')"
            par.cmd.ExecuteNonQuery()
        Next

        EliminaPagamento = True
    End Function

    Protected Sub btnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        If confAnnullo.Value = "1" Then
            connData.apri(True)
            Try
                If idSelected.Value <> "0" Then
                    If flAnnullata.Value = 0 Then
                        If EliminaPagamento() = True Then
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('IL PAGAMENTO SELEZIONATO E\' STATO ANNULLATO!');opener.validNavigation=true;opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();", True)
                        Else
                            connData.chiudi(False)
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Anomalia in fase di annullo!Operazione interrotta!');", True)
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('IL PAGAMENTO E\' GIA\' STATO ANNULLATO!\nImpossibile procedere!');", True)
                    End If
                    connData.chiudi(True)

                    idSelected.Value = 0
                    CaricaIncassi()
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Nessun pagamento selezionato.\nImpossibile procedere!');", True)
                End If

            Catch ex As Exception
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
                Session.Add("ERRORE", "Provenienza: Proprieta - btnDelete_Click - " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)
            End Try
        End If
    End Sub
End Class
