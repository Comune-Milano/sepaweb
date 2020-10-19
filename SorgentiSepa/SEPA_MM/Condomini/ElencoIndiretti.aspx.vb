Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_ElencoIndiretti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            Cerca()

        End If
    End Sub
    Private Sub Cerca()
        Try
            par.cmd.CommandText = "SELECT  COMUNI_NAZIONI.NOME AS CITTA, TO_CHAR(CONDOMINI.ID,'00000') AS COD_CONDOMINIO,COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME ||' '|| COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO,(CASE WHEN TIPO_GESTIONE = 'D' THEN 'DIRETTA' WHEN TIPO_GESTIONE = 'I' THEN 'INDIRETTA' ELSE ''END )AS GESTIONE, " _
                                  & "MIL_PRO_TOT_COND,MIL_COMPRO_TOT_COND,MIL_SUP_TOT_COND,MIL_GEST_TOT_COND " _
                                  & "FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE,SEPA.COMUNI_NAZIONI " _
                                  & "WHERE TIPO_GESTIONE = 'I' AND COMUNI_NAZIONI.COD(+) = CONDOMINI.COD_COMUNE AND COND_AMMINISTRATORI.ID(+) = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO(+) = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridCondom.DataSource = dt
            DataGridCondom.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGridCondom.Items.Count & " "
            da.Dispose()
            'ds.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")

        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Esporta()
    End Sub
    Private Sub Esporta()
        Try
            If DataGridCondom.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridCondom, "ExportCondIndiretti", 90 / 100, , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Protected Sub DataGridCondom_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCondom.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("ondblclick", "Apri();")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Condominio " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("ondblclick", "Apri();")

        End If
    End Sub
End Class
