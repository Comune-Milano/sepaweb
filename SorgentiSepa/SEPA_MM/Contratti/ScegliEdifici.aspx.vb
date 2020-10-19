
Partial Class Contratti_ScegliEdifici
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
            CreaDgvEdifici()
        End If
    End Sub

    Private Sub CreaDgvEdifici()
        Try
            dataTableEdificio = New Data.DataTable
            dataTableEdificio.Columns.Add("ID", Type.GetType("System.String"))
            dataTableEdificio.Columns.Add("CODICE", Type.GetType("System.String"))
            dataTableEdificio.Columns.Add("DENOMINAZIONE", Type.GetType("System.String"))

            DataGridEdifici.DataSource = dataTableEdificio
            DataGridEdifici.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ScegliEdifici - CreaDgvEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub RicercaEdificio()
        Dim Trovato As Boolean = False
        Dim condizione As String = ""
        Dim sValore As String = ""
        Dim StringaSql As String = ""
        Try
            Dim sCompara As String = ""
            Me.connData.RiempiPar(par)
            par.cmd.CommandText = "SELECT edifici.ID, cod_edificio, denominazione " _
                                & "FROM siscom_mi.edifici " _
                                & "WHERE id<>0 "
            If Not String.IsNullOrEmpty(txtCodEdificio.Text) Then
                sValore = Trim(Me.txtCodEdificio.Text.ToUpper)
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                condizione = condizione & " and cod_edificio " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If Not String.IsNullOrEmpty(txtDenominazione.Text) Then
                sValore = Me.txtDenominazione.Text.ToUpper
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                condizione = condizione & " and denominazione " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            par.cmd.CommandText = par.cmd.CommandText & condizione & " ORDER BY COD_edificio ASC"
            StringaSql = par.cmd.CommandText
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Session.Add("DTRICERCAEDIFICI", dt)
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

    Private Sub BindGrid()
        Try
            DataGridEdifici.DataSource = Session.Item("DTRICERCAEDIFICI")
            DataGridEdifici.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " BindGrid " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGridEdifici_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridEdifici.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('txtmia').value='Hai selezionato l\'edificio " & e.Item.Cells(2).Text.Replace("'", "\'").Replace("&nbsp;", "") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnSeleziona').click();")
        End If
    End Sub

    Protected Sub btnCercaUnit_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCercaUnit.Click
        If txtCodEdificio.Text = "" And txtDenominazione.Text = "" Then
            par.modalDialogMessage("Attenzione", "Compilare almeno un filtro!", Me.Page)
        Else
            RicercaEdificio()
        End If
    End Sub

    Public Property dataTableEdificio() As Data.DataTable
        Get
            If Not (ViewState("dataTableEdificio") Is Nothing) Then
                Return ViewState("dataTableEdificio")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableEdificio") = value
        End Set
    End Property

    Protected Sub btnSeleziona_Click(sender As Object, e As System.EventArgs) Handles btnSeleziona.Click
        Try
            If idSelected.Value > 0 Then
                Session.Add("idEdificio", idSelected.Value)
                idSelected.Value = 0
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

    Protected Sub DataGridEdifici_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridEdifici.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridEdifici.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
