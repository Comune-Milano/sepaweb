
Partial Class Contratti_ParametriPrioritaVoci
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        Dim Str As String = ""
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            CaricaVociBollette()
        End If
    End Sub
    Private Sub CaricaVociBollette()
        Try
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE, PRIORITA " _
                                & "FROM SISCOM_MI.T_VOCI_BOLLETTA " _
                                & "ORDER BY PRIORITA ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                HFMinPriorita.Value = "1"
                HFMaxPriorita.Value = dt.Rows.Count - 1
            Else
                HFMinPriorita.Value = "0"
                HFMaxPriorita.Value = "0"
            End If
            Session.Add("dtPrioritaVoci", dt)
            BindGrid()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - CaricaVociBollette " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dgvVoci.DataSource = CType(Session.Item("dtPrioritaVoci"), Data.DataTable)
            dgvVoci.DataBind()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - BindGrid " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub dgvVoci_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvVoci.PageIndexChanged
        Try
            If e.NewPageIndex >= 0 Then
                dgvVoci.CurrentPageIndex = e.NewPageIndex
                AggiustaCompSessione()
                BindGrid()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - dgvVoci_PageIndexChanged " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub dgvVoci_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvVoci.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            If e.Item.Cells(1).Text.ToString.ToUpper.Contains("IVA") Then
                CType(e.Item.FindControl("txtPriorita"), TextBox).ReadOnly = True
                CType(e.Item.FindControl("txtPriorita"), TextBox).Attributes.Add("onfocus", "javascript:alert('Non è possibile modificare questa priorità');document.getElementById('btnAggiorna').focus();")
            Else
                CType(e.Item.FindControl("txtPriorita"), TextBox).Attributes.Add("onblur", "javascript:valid(this,'notnumbers');controllaPriorita('" & CType(e.Item.FindControl("txtPriorita"), TextBox).ClientID & "');")
                CType(e.Item.FindControl("txtPriorita"), TextBox).Attributes.Add("onfocus", "javascript:selectall('" & CType(e.Item.FindControl("txtPriorita"), TextBox).ClientID & "');")
            End If
        End If
    End Sub
    Private Sub AggiustaCompSessione()
        Try
            Dim dt As Data.DataTable = CType(Session.Item("dtPrioritaVoci"), Data.DataTable)
            Dim row As Data.DataRow
            For i As Integer = 0 To dgvVoci.Items.Count - 1
                row = dt.Select("id = " & dgvVoci.Items(i).Cells(0).Text)(0)
                If Not String.IsNullOrEmpty(CType(dgvVoci.Items(i).FindControl("txtPriorita"), TextBox).Text) Then
                    row.Item("PRIORITA") = CType(dgvVoci.Items(i).FindControl("txtPriorita"), TextBox).Text
                End If
            Next
            Session.Item("dtPrioritaVoci") = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - AggiustaCompSessione " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Write("<script>location.href='pagina_home.aspx';</script>")
    End Sub
    Protected Sub btnAggiorna_Click(sender As Object, e As System.EventArgs) Handles btnAggiorna.Click
        Try
            AggiustaCompSessione()
            SalvaPriorita()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - btnAggiorna_Click " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub SalvaPriorita()
        Try
            connData.apri(True)
            Dim dt As Data.DataTable = CType(Session.Item("dtPrioritaVoci"), Data.DataTable)
            For Each row As Data.DataRow In dt.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.T_VOCI_BOLLETTA SET PRIORITA = " & par.insDbValue(row.Item("PRIORITA").ToString, False) & " " _
                                    & "WHERE ID = " & par.insDbValue(row.Item("ID"), False)
                par.cmd.ExecuteNonQuery()
            Next
            connData.chiudi(True)
            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
            CaricaVociBollette()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - SalvaPriorita " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
