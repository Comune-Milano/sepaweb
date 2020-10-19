
Partial Class CENSIMENTO_AssFiliali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Public Property dtEdifici() As Data.DataTable
        Get
            If Not (ViewState("dtEdifici") Is Nothing) Then
                Return ViewState("dtEdifici")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtEdifici") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String = ""
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCENSIMENTO/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            RiempiCampi()
        End If
    End Sub
    Private Sub RiempiCampi()
        Try
            par.caricaComboBox("SELECT DISTINCT TAB_FILIALI.ID, (NOME || ' - ' || TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE) AS FILIALE FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST order by filiale asc", Me.ddlFiliale, "ID", "FILIALE", True)
            par.cmd.CommandText = "SELECT DISTINCT ID, 'FALSE' AS SELEZIONE, (COD_EDIFICIO || ' - ' || DENOMINAZIONE) AS EDIFICIO,DENOMINAZIONE FROM SISCOM_MI.EDIFICI order by denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            dtEdifici = New Data.DataTable
            da.Fill(dtEdifici)
            da.Dispose()
            BindGrid()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - RiempiCampi - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvEdifici.DataSource = dtEdifici
            dgvEdifici.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - BindGrid - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Protected Sub btnSeleziona_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSeleziona.Click
        Try
            SelezionaTuttiSub()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - btnSeleziona_Click - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub SelezionaTuttiSub()
        Try
            For Each row As Data.DataRow In dtEdifici.Rows
                If SelezionaTutti.Value = 0 Then
                    row.Item("SELEZIONE") = "TRUE"
                Else
                    row.Item("SELEZIONE") = "FALSE"
                End If
            Next
            For Each riga As DataGridItem In dgvEdifici.Items
                If SelezionaTutti.Value = 0 Then
                    If CType(riga.FindControl("cboggetto"), CheckBox).Checked = False Then
                        CType(riga.FindControl("cboggetto"), CheckBox).Checked = True
                    End If
                Else
                    If CType(riga.FindControl("cboggetto"), CheckBox).Checked = True Then
                        CType(riga.FindControl("cboggetto"), CheckBox).Checked = False
                    End If
                End If
            Next
            If SelezionaTutti.Value = 0 Then
                SelezionaTutti.Value = 1
            Else
                SelezionaTutti.Value = 0
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - SelezionaTuttiSub - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Protected Sub dgvEdifici_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvEdifici.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
        End If
    End Sub
    Protected Sub dgvEdifici_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvEdifici.PageIndexChanged
        Try
            If e.NewPageIndex >= 0 Then
                dgvEdifici.CurrentPageIndex = e.NewPageIndex
                AggiustaCompSessione()
                BindGrid()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - dgvEdifici_PageIndexChanged - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Private Sub AggiustaCompSessione()
        Try
            Dim row As Data.DataRow
            For i As Integer = 0 To dgvEdifici.Items.Count - 1
                If DirectCast(dgvEdifici.Items(i).Cells(1).FindControl("cboggetto"), CheckBox).Checked = False Then
                    row = dtEdifici.Select("id = " & dgvEdifici.Items(i).Cells(0).Text)(0)
                    row.Item("SELEZIONE") = "FALSE"
                Else
                    row = dtEdifici.Select("id = " & dgvEdifici.Items(i).Cells(0).Text)(0)
                    row.Item("SELEZIONE") = "TRUE"
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - AggiustaCompSessione - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub
    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Session.Add("LAVORAZIONE", 1)
        If ConfProcedi.Value = 1 Then
            If ControllaDati() Then
                If SalvaFiliale() Then
                    Response.Write("<script>alert('Operazione completata correttamente!');</script>")
                    RiempiCampi()
                End If
            End If
            ConfProcedi.Value = 0
        End If
        Session.Remove("LAVORAZIONE")
    End Sub
    Private Function ControllaDati() As Boolean
        Try
            AggiustaCompSessione()
            ControllaDati = False
            If ddlFiliale.SelectedValue.ToString = "-1" Then
                Response.Write("<script>alert('Nessuna filiale selezionata!');</script>")
                Exit Function
            End If
            For Each row As Data.DataRow In dtEdifici.Rows
                If row.Item("SELEZIONE") = "TRUE" Then
                    ControllaDati = True
                    Exit Function
                End If
            Next
            Response.Write("<script>alert('Nessun edificio selezionato!');</script>")
        Catch ex As Exception
            ControllaDati = False
        End Try
    End Function
    Private Function SalvaFiliale() As Boolean
        Try
            SalvaFiliale = False
            Dim primo As Boolean = True
            Dim ListaEdifici As String = ""
            For Each row As Data.DataRow In dtEdifici.Rows
                If row.Item("SELEZIONE") = "TRUE" Then
                    If primo Then
                        ListaEdifici = par.IfNull(row.Item("ID"), "-1")
                        primo = False
                    Else
                        ListaEdifici &= ", " & par.IfNull(row.Item("ID"), "-1")
                    End If
                End If
            Next
            If Not String.IsNullOrEmpty(ListaEdifici) Then
                connData.apri(True)
                ‘‘par.cmd.Transaction = connData.Transazione
                Dim s As Generic.List(Of String)
                s = par.QueryINSplit(ListaEdifici, "SELECT FINE_VALIDITA FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.FILIALI_UI  WHERE UNITA_IMMOBILIARI.ID(+) = FILIALI_UI.ID_UI AND ID_EDIFICIO IN (#SOST#) AND FINE_VALIDITA = '30000101'", "#SOST#")
                For Each elemento As String In s
                    par.cmd.CommandText = "UPDATE (" & elemento & ") A SET A.FINE_VALIDITA = '" & Format(Now, "yyyyMMdd") & "'"
                    par.cmd.ExecuteNonQuery()
                Next
                s = par.QueryINSplit(ListaEdifici, "SELECT " & ddlFiliale.SelectedValue & ", ID, '" & Format(Now, "yyyyMMdd") & "', '30000101' FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN (#SOST#)", "#SOST#")
                For Each elemento As String In s
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FILIALI_UI " & elemento
                    par.cmd.ExecuteNonQuery()
                Next
                connData.chiudi(True)
            Else
                Response.Write("<script>alert('Nessun edificio selezionato!');</script>")
                Exit Function
            End If
            SalvaFiliale = True
        Catch ex As Exception
            SalvaFiliale = False
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Censimento_AssFiliali - SalvaFiliale - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Function
End Class
