
Partial Class AMMSEPA_OperatoreSUA_RUAbusivi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property sStringaSQL() As String
        Get
            If Not (ViewState("par_sStringaSQL") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sStringaSQL") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        Response.Flush()
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Try
            par.OracleConn.Open()
            sStringaSQL = "SELECT SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI.ID_CONTRATTO AS ID, SISCOM_MI.RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE, " _
                        & "(CASE WHEN siscom_mi.rapporti_utenza_au_abusivi.fl_lavorato = 0 THEN 'NO' ELSE 'SI' END) AS ELABORATO, " _
                        & "(CASE WHEN siscom_mi.anagrafica.cognome IS NULL AND siscom_mi.anagrafica.NOME IS NULL THEN siscom_mi.anagrafica.RAGIONE_SOCIALE ELSE siscom_mi.anagrafica.cognome || ' ' ||siscom_mi.anagrafica.NOME END) AS INTESTATARIO " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA " _
                        & "WHERE SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI.ID_CONTRATTO = SISCOM_MI.RAPPORTI_UTENZA.ID " _
                        & "AND SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_CONTRATTO = SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI.ID_CONTRATTO " _
                        & "AND SISCOM_MI.SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                        & "AND SISCOM_MI.SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = SISCOM_MI.ANAGRAFICA.ID ORDER BY SISCOM_MI.RAPPORTI_UTENZA.COD_CONTRATTO"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            lblrecord.Text = "Trovati: " & dt.Rows.Count & " record"
            dgvruabusivi.DataSource = dt
            dgvruabusivi.DataBind()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            txtmia.Text = "Nessuna Selezione"
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvruabusivi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvruabusivi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il contratto nr. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il contratto nr. " & e.Item.Cells(1).Text & "';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
        End If
    End Sub
    Protected Sub btnelimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnelimina.Click
        Try
            If ConfElimina.Value = 1 Then
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE FL_LAVORATO = 0 AND ID_CONTRATTO = " & LBLID.Value
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myReader.Read Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO = " & LBLID.Value
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione Completata!!');</script>")
                Else
                    Response.Write("<script>alert('Non e\' possibile cancellare il dato selezionato!!');</script>")
                End If
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            LBLID.Value = 0
            ConfElimina.Value = 0
            BindGrid()
        Catch ex1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                Response.Write("<script>alert('Non è possibile cancellare il dato in questo momento. Riprovare più tardi!');</script>")
            End If
            If Data.ConnectionState.Open = True Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub dgvruabusivi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvruabusivi.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            dgvruabusivi.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('../pagina_home.aspx');</script>")
    End Sub
End Class
