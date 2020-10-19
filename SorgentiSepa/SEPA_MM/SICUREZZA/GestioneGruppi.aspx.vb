Imports Telerik.Web.UI

Partial Class SICUREZZA_GestioneGruppi
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
                CaricaGruppi()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Gestione Gruppi - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub CaricaGruppi()
        Try
            connData.apri()
            par.cmd.CommandText = " SELECT id, nome_gruppo from siscom_mi.gruppi_sicurezza order by nome_gruppo asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Item("ElencoGruppi") = dt

            connData.chiudi()
            RadGridGruppi.Rebind()
            If dt.Rows.Count > 0 Then

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - GestioneGruppi - CaricaGruppi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridGruppi_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridGruppi.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridGruppi.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('LBLID').value='" & dataItem("ID").Text & "';" _
                                            & "document.getElementById('txtGruppoSelected').value='Hai selezionato il gruppo " & dataItem("NOME_GRUPPO").Text & "';" _
            & "document.getElementById('Denominazione').value='" & dataItem("NOME_GRUPPO").Text & "';")

        End If
    End Sub

    Protected Sub RadGridGruppi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridGruppi.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("ElencoGruppi"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("ElencoGruppi"), Data.DataTable)
    End Sub

    Protected Sub btnSalvaDen_Click(sender As Object, e As System.EventArgs) Handles btnSalvaDen.Click
        Try
            Dim CodComune As String = ""

            If Me.TextBox1.Value = 2 Then
                Update()
            ElseIf Me.TextBox1.Value = 1 Then
                connData.apri()

                If par.IfEmpty(Me.txtDenominazione.Text, "Null") <> "Null" Then
                    par.cmd.CommandText = "select * FROM SISCOM_MI.GRUPPI_SICUREZZA where NOME_GRUPPO='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        
                        CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Elemento già inserito!", 300, 150, "Attenzione", Nothing, Nothing)
                        txtDenominazione.Text = ""
                        myReader.Close()
                        connData.chiudi()
                        Exit Sub
                    End If
                    myReader.Close()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.GRUPPI_SICUREZZA (ID, NOME_GRUPPO) VALUES (SISCOM_MI.SEQ_GRUPPI_SICUREZZA.NEXTVAL, '" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()

                    Me.TextBox1.Value = "0"

                    Me.txtDenominazione.Text = ""
                    LBLID.Value = ""
                    connData.chiudi()
                    'par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
                    CaricaGruppi()
                Else
                    connData.chiudi()
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Campi Obbligatori!", 300, 150, "Attenzione", Nothing, Nothing)
                End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Sicurezza - GestioneGruppi - CaricaGruppi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub Update()

        If par.IfEmpty(Me.txtDenominazione.Text, "Null") <> "Null" Then
            connData.apri()

            par.cmd.CommandText = "select * FROM SISCOM_MI.GRUPPI_SICUREZZA WHERE id<>" & LBLID.Value & " and NOME_GRUPPO='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                par.modalDialogMessage("Info", "Valore già inserito!", Me.Page)
                'txtDenominazione.Text = ""
                '*****CHIUSURA DEL MYREADER
                myReader.Close()
                connData.chiudi()
                Exit Sub
            End If
            myReader.Close()
            par.cmd.CommandText = "UPDATE SISCOM_MI.GRUPPI_SICUREZZA SET NOME_GRUPPO='" & par.PulisciStrSql(txtDenominazione.Text.ToUpper) & "' WHERE ID=" & LBLID.Value
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
            CaricaGruppi()

            Me.TextBox1.Value = "0"
            txtDenominazione.Text = ""
            txtGruppoSelected.Text = ""
            Me.LBLID.Value = ""
        End If

    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        If LBLID.Value <> "" Then
            connData.apri()
            par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.gruppi_sicurezza WHERE ID = " & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtDenominazione.Text = par.IfNull(myReader("NOME_GRUPPO"), "")
            End If
            myReader.Close()
            connData.chiudi()
            Me.TextBox1.Value = "2"
        Else
            Me.TextBox1.Value = "0"
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun elemento selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    'Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
    '    If LBLID.Value <> "" Then
    '        par.modalDialogConfirm("Info", "Eliminare l\'elemento selezionato?", "Ok", "document.getElementById('btnEliminaElemento').click();", "Annulla", "", Page)
    '    Else
    '        par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
    '    End If
    'End Sub



    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        Try
            connData.apri()

            par.cmd.CommandText = "select * from siscom_mi.interventi_sicurezza where id_gruppo=" & LBLID.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Elemento in uso. Non è possibile eliminare!", 300, 150, "Attenzione", Nothing, Nothing)
            Else
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.GRUPPI_SICUREZZA WHERE ID = " & Me.LBLID.Value
                par.cmd.ExecuteNonQuery()
            End If
            myReader.Close()

            connData.chiudi()
            Me.TextBox1.Value = "0"
            txtDenominazione.Text = ""
            txtGruppoSelected.Text = ""
            Me.LBLID.Value = ""
            par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
            CaricaGruppi()

        Catch EX1 As Data.OracleClient.OracleException
            connData.chiudi()
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Elemento in uso. Non è possibile eliminare!", 300, 150, "Attenzione", Nothing, Nothing)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - GestioneGruppi - btnEliminaElemento_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

End Class
