
Partial Class Condomini_RisRicCondomini
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            cerca()
        End If
    End Sub
    Private Sub cerca()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim condizione As String = ""
            Dim condFrom As String = ""
            Dim inWhere As String = ""
            Dim e As Boolean = False
            If par.IfEmpty(Request.QueryString("COMP"), "-1") <> "-1" Or par.IfEmpty(Request.QueryString("EDIF"), "-1") <> "-1" Or par.IfEmpty(Request.QueryString("IND"), "-1") <> "---" Then


                If par.IfEmpty(Request.QueryString("EDIF"), "-1") <> "-1" Then
                    e = True
                    inWhere = inWhere & " edifici.id = " & Request.QueryString("EDIF")
                ElseIf par.IfEmpty(Request.QueryString("COMP"), "-1") <> "-1" Then
                    e = True
                    inWhere = inWhere & " id_complesso = " & Request.QueryString("COMP")
                End If

                If par.IfEmpty(Request.QueryString("IND"), "-1") <> "---" Then
                    condFrom = ", siscom_mi.indirizzi "
                    If e = True Then
                        inWhere = inWhere & " and "
                    End If
                    inWhere = inWhere & " edifici.id_indirizzo_principale = indirizzi.id and indirizzi.descrizione = '" & par.PulisciStrSql(par.IfEmpty(Request.QueryString("IND"), "-1")) & "'"

                    If par.IfEmpty(Request.QueryString("CIV"), "-1") <> "---" Then
                        inWhere = inWhere & " and siscom_mi.indirizzi.civico = '" & par.PulisciStrSql(Request.QueryString("CIV")) & "'"
                    End If


                End If

                condizione = " and cond_edifici.id_edificio in (select edifici.id from siscom_mi.edifici " & condFrom & " where " & inWhere & ") "

            End If
            If par.IfEmpty(Request.QueryString("AMMINIST"), "-1") <> "-1" Then
                condizione = condizione & " and cond_amministrazione.id_amministratore = " & Request.QueryString("AMMINIST")
            End If


            par.cmd.CommandText = "SELECT distinct condomini.ID,comuni_nazioni.nome AS citta, " _
                                & "TO_CHAR (condomini.ID, '00000') AS cod_condominio, " _
                                & "cond_amministratori.ID AS id_amm, " _
                                & "(cond_amministratori.cognome || ' ' || cond_amministratori.nome) AS amminist, " _
                                & " condomini.denominazione AS condominio " _
                                & "FROM siscom_mi.condomini, siscom_mi.cond_edifici, " _
                                & "siscom_mi.cond_amministratori, " _
                                & "siscom_mi.cond_amministrazione, " _
                                & "sepa.COMUNI_NAZIONI " _
                                & "WHERE COMUNI_NAZIONI.cod(+) = condomini.cod_comune " _
                                & "AND cond_amministratori.ID(+) = cond_amministrazione.id_amministratore " _
                                & "and cond_edifici.id_condominio = condomini.id " & condizione _
                                & "AND cond_amministrazione.id_condominio(+) = condomini.ID " _
                                & "AND cond_amministrazione.data_fine IS NULL " _
                                & "ORDER BY condomini.denominazione asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            dgvCondomini4.DataSource = dt
            dgvCondomini4.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub dgvCondomini_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvCondomini4.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Condominio " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExcel.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(dgvCondomini4, "ExpCondomini", , True, , False)
        If System.IO.File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("..\/FileTemp\/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub
End Class
