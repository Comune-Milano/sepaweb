
Partial Class Contratti_ScegliIndirizzi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            CreaDgvVia()
        End If
    End Sub

    Private Sub CreaDgvVia()
        Try
            dataTableVia = New Data.DataTable
            dataTableVia.Columns.Add("ID", Type.GetType("System.String"))
            dataTableVia.Columns.Add("INDIRIZZO", Type.GetType("System.String"))
            dataTableVia.Columns.Add("NOME_INDIRIZZO", Type.GetType("System.String"))
            dataTableVia.Columns.Add("CIVICO_INDIRIZZO", Type.GetType("System.String"))
            DataGridIndirizzi.DataSource = dataTableVia
            DataGridIndirizzi.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ScegliIndirizzi - CreaDgvVia - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Property dataTableVia() As Data.DataTable
        Get
            If Not (ViewState("dataTableVia") Is Nothing) Then
                Return ViewState("dataTableVia")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableVia") = value
        End Set
    End Property

    Private Sub RicercaIndirizzo()
        Dim Trovato As Boolean = False
        Dim condizione As String = ""
        Dim sValore As String = ""
        Dim StringaSql As String = ""
        Try
            Dim sCompara As String = ""
            Me.connData.RiempiPar(par)
            par.cmd.CommandText = "SELECT DISTINCT DESCRIZIONE ||' '|| CIVICO AS DESCRIZIONE,INDIRIZZI.DESCRIZIONE AS NOME_INDIRIZZO,INDIRIZZI.CIVICO AS CIVICO_INDIRIZZO,'' AS PROGR FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID"
            If Not String.IsNullOrEmpty(txtDenominazione.Text) Then
                sValore = Trim(Me.txtDenominazione.Text.ToUpper)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                condizione = condizione & " and INDIRIZZI.DESCRIZIONE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            par.cmd.CommandText = par.cmd.CommandText & condizione & " ORDER BY DESCRIZIONE ASC"
            StringaSql = par.cmd.CommandText
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Add("DTRICERCAVIA", dt)
            BindGrid()

            If dt.Rows.Count = 0 Then
                par.modalDialogMessage("Attenzione", "Nessun risultato trovato!", Me.Page)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " RicercaEdificio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnSeleziona_Click(sender As Object, e As System.EventArgs) Handles btnSeleziona.Click
        Try
            If idSelected.Value <> "" Then
                Session.Add("idIndirizzo", idSelected.Value)
                idSelected.Value = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "close", "self.close();", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Nessun elemento selezionato!');", True)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " btnSeleziona_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            DataGridIndirizzi.DataSource = Session.Item("DTRICERCAVIA")
            DataGridIndirizzi.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " BindGrid " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridIndirizzi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridIndirizzi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('txtmia').value='Hai selezionato l\'indirizzo " & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnSeleziona').click();")
        End If
    End Sub

    Protected Sub btnCercaVia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaVia.Click
        If txtDenominazione.Text = "" Then
            par.modalDialogMessage("Attenzione", "Compilare il filtro di ricerca!", Me.Page)
        Else
            RicercaIndirizzo()
        End If
    End Sub

    Protected Sub DataGridIndirizzi_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIndirizzi.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridIndirizzi.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
