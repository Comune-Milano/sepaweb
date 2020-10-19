Imports Telerik.Web.UI

Partial Class SICUREZZA_GestioneGruppiOperatori
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                par.caricaComboBox("select * from SISCOM_MI.GRUPPI_SICUREZZA order by NOME_GRUPPO asc", cmbGruppo, "ID", "NOME_GRUPPO", False)
                CaricaGruppiOperatori()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - GestioneGruppiOperatori - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub CaricaGruppiOperatori()
        Try
            connData.apri()

            Dim Str As String = ""
            If cmbGruppo.SelectedItem.Value <> "" Then
                Str = "select GRUPPI_OPERATORI_SICUREZZA.ID,id_gruppo,operatori.operatore,operatori.cognome,operatori.nome from operatori,SISCOM_MI.GRUPPI_OPERATORI_SICUREZZA where operatori.id=GRUPPI_OPERATORI_SICUREZZA.id_operatore and GRUPPI_OPERATORI_SICUREZZA.id_gruppo=" & cmbGruppo.SelectedItem.Value & " order by operatore"
            End If
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            da.Dispose()
            Session.Item("ElencoOperatori") = dt

            connData.chiudi()
            RadGridOperatori.Rebind()
            If dt.Rows.Count > 0 Then

            End If

            par.caricaComboBox("select OPERATORI.ID,OPERATORI.OPERATORE from OPERATORI WHERE mod_GESTIONE_cONTATTI=1 AND ID NOT IN (SELECT ID_OPERATORE FROM SISCOM_MI.GRUPPI_OPERATORI_SICUREZZA WHERE ID_GRUPPO IN (" & cmbGruppo.SelectedValue & ")) ORDER BY OPERATORE ASC", cmbOperatore, "ID", "OPERATORE", False)
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub RadGridOperatori_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridOperatori.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridOperatori.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('LBLID').value='" & dataItem("ID").Text & "';" _
                                            & "document.getElementById('txtOperatoreSelected').value='Hai selezionato l\'operatore " & dataItem("operatore").Text & "';")
        End If
    End Sub

    Protected Sub RadGridOperatori_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridOperatori.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("ElencoOperatori"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("ElencoOperatori"), Data.DataTable)
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub cmbGruppo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbGruppo.SelectedIndexChanged
        CaricaGruppiOperatori()
        txtOperatoreSelected.Text = ""
    End Sub

    Protected Sub btnSalvaOp_Click(sender As Object, e As System.EventArgs) Handles btnSalvaOp.Click
        If IsNothing(cmbOperatore.SelectedItem) = False Then
            Try
                connData.apri()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.GRUPPI_OPERATORI_SICUREZZA (ID, ID_GRUPPO, ID_OPERATORE) VALUES (SISCOM_MI.SEQ_GRUPPI_OPERATORI_SICUREZZA.nextval," & cmbGruppo.SelectedValue & "," & cmbOperatore.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
                CaricaGruppiOperatori()
                TextBox1.Value = ""

            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza:Sicurezza - GestioneGruppiOperatori - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End Try
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun operatore selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Sub

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try

            If par.IfEmpty(Me.LBLID.Value, "") <> "" Then
                connData.apri()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.GRUPPI_OPERATORI_SICUREZZA WHERE ID = " & Me.LBLID.Value
                par.cmd.ExecuteNonQuery()

                connData.chiudi()

                Me.TextBox1.Value = "0"
                Me.LBLID.Value = ""
                txtOperatoreSelected.Text = ""
                CaricaGruppiOperatori()
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
            Else
                par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
            End If

        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            par.modalDialogMessage("Attenzione", "Elemento in uso. Non è possibile eliminare!", Me.Page)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - GestioneGruppiOperatori - btnEliminaElemento_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
