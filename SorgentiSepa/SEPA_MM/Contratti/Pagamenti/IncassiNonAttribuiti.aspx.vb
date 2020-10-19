
Partial Class Contratti_Pagamenti_IncassiNonAttribuiti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub ImageButtonAvviaRicerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAvviaRicerca.Click
        Try
            'IMPOSTAZIONE DELLE CONDIZIONI
            Dim dataIncasso As String = par.FormatoDataDB(TextBoxDataIncasso.Text)
            Dim nominativo As String = Replace(Replace(TextBoxNominativo.Text, "*", "%"), "'", "''")
            Dim note As String = Replace(Replace(TextBoxNote.Text, "*", "%"), "'", "''")
            Dim causale As String = Replace(Replace(TextBoxCausale.Text, "*", "%"), "'", "''")
            Dim importomin As String = Replace(Replace(TextBoxImportoMin.Text, ".", ""), ",", ".")
            Dim importomax As String = Replace(Replace(TextBoxImportoMax.Text, ".", ""), ",", ".")
            Dim condizioniRicerca As String = ""
            If cmbTipoPagamento.SelectedValue <> "-1" Then
                condizioniRicerca &= " AND id_tipo_pag=" & cmbTipoPagamento.SelectedValue
            End If
            If dataIncasso <> "" Then
                condizioniRicerca &= " AND DATA_INCASSO='" & dataIncasso & "' "
            End If
            If nominativo <> "" Then
                condizioniRicerca &= " AND UPPER(NOMINATIVO) LIKE '%" & UCase(nominativo) & "%' "
            End If
            If note <> "" Then
                condizioniRicerca &= " AND UPPER(NOTE) LIKE '%" & UCase(note) & "%' "
            End If
            If causale <> "" Then
                condizioniRicerca &= " AND UPPER(CAUSALE) LIKE '%" & UCase(causale) & "%' "
            End If
            If importomin <> "" Then
                condizioniRicerca &= " AND IMPORTO>=" & importomin
            End If
            If importomax <> "" Then
                condizioniRicerca &= " AND IMPORTO<=" & importomax
            End If
            ApriConnessione()
            par.cmd.CommandText = "SELECT ID " _
                & ",TO_CHAR(TO_DATE(DATA_INCASSO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_INCASSO" _
                & ",UPPER(NOMINATIVO) AS NOMINATIVO" _
                & ",UPPER(NOTE) AS NOTE" _
                & ",UPPER(CAUSALE) AS CAUSALE" _
                & ",TRIM(TO_CHAR(IMPORTO_RESIDUO,'999G999G990D99')) AS IMPORTO,id_tipo_pag " _
                & " FROM SISCOM_MI.INCASSI_NON_ATTRIBUIBILI WHERE IMPORTO_RESIDUO <> 0 " & condizioniRicerca
            Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New Data.DataTable
            dataAdapter.Fill(dataTable)
            chiudiConnessione()
            If dataTable.Rows.Count > 0 Then
                DataGridIncassiNonAttribuiti.Visible = True
                ImageButtonProcedi.Visible = True
                DataGridIncassiNonAttribuiti.DataSource = dataTable
                DataGridIncassiNonAttribuiti.DataBind()
            Else
                DataGridIncassiNonAttribuiti.Visible = False
                ImageButtonProcedi.Visible = False
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "messRicerca", "alert('La ricerca non ha prodotto risultati!')", True)
            End If
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "errRicerca", "alert('Si è verificato un errore durante la ricerca degli incassi!')", True)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Session.Remove("IdIncassoNonAttribuito")
        Session.Remove("ImportoIncassoNonAttribuito")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            CaricaTipologie()
            aggiungiFunzioniJavascript()
        End If
    End Sub
    Protected Sub chiudiConnessione()
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub

    Private Sub aggiungiFunzioniJavascript()
        TextBoxImportoMin.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        TextBoxImportoMin.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
        TextBoxImportoMax.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        TextBoxImportoMax.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
        TextBoxDataIncasso.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub DataGridIncassiNonAttribuiti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIncassiNonAttribuiti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('ImportoIncassoNonAttribuito').value='" & e.Item.Cells(2).Text & "';document.getElementById('IdIncassoNonAttribuito').value='" & e.Item.Cells(0).Text & "';document.getElementById('idTipoPag').value='" & e.Item.Cells(6).Text & "';document.getElementById('TextBoxSelezionato').value='Hai selezionato l\'incasso di " & Replace(e.Item.Cells(3).Text, "'", "\'") & " di € " & e.Item.Cells(2).Text & "';")
        End If
    End Sub

    Protected Sub ImageButtonProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonProcedi.Click
        Session.Add("IdIncassoNonAttribuito", IdIncassoNonAttribuito.Value)
        Session.Add("ImportoIncassoNonAttribuito", ImportoIncassoNonAttribuito.Value)
        Session.Add("TipoIncassNonAtt", Me.idTipoPag.Value)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "confermaSelezione", "self.close();", True)
    End Sub

    Private Sub CaricaTipologie()
        Try
            ApriConnessione()
            par.cmd.CommandText = "SELECT -1 as ID,'---' AS DESCRIZIONE FROM DUAL UNION SELECT ID, DESCRIZIONE FROM SISCOM_MI.TIPO_PAG_PARZ ORDER BY DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            cmbTipoPagamento.DataSource = dt
            cmbTipoPagamento.DataTextField = "DESCRIZIONE"
            cmbTipoPagamento.DataValueField = "ID"
            cmbTipoPagamento.DataBind()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "errLoad", "alert('Si è verificato un errore durante il caricamento della pagina!')", True)
        End Try
    End Sub
End Class
