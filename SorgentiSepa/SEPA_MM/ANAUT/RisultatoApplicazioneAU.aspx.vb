
Partial Class AMMSEPA_RisultatoApplicazioneAU
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            LBLID.Value = "-1"
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Session.Item("PGAPPLICAZIONEAU"), par.OracleConn)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()



        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count


        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Session.Remove("PGAPPLICAZIONEAU")
        Response.Redirect("RicercaApplicazioneAU.aspx")
    End Sub

    Protected Sub btnSelezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = True
        Next
    End Sub

    Protected Sub btnDeselezionaTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselezionaTutti.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox


        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            chkExport.Checked = False
        Next
    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim oDataGridItem As DataGridItem
        Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim dt As New System.Data.DataTable
        Dim ROW As System.Data.DataRow
        Dim I As Long = 0

        dt.Columns.Add("COD_CONTRATTO")
        dt.Columns.Add("PG_AU")
        dt.Columns.Add("COGNOME")
        dt.Columns.Add("NOME")
        dt.Columns.Add("TIPOLOGIA")
        dt.Columns.Add("DECORRENZA")
        dt.Columns.Add("SCADENZA")
        dt.Columns.Add("INDIRIZZO_UNITA")
        dt.Columns.Add("CIVICO_UNITA")
        dt.Columns.Add("COMUNE_UNITA")
        dt.Columns.Add("CAP_UNITA")
        dt.Columns.Add("FILIALE")
        dt.Columns.Add("PREVALENTE")
        dt.Columns.Add("PRESENZA_15")
        dt.Columns.Add("PRESENZA_65")
        dt.Columns.Add("N_INV_100_CON")
        dt.Columns.Add("N_INV_100_SENZA")
        dt.Columns.Add("N_INV_66_99")
        dt.Columns.Add("IDC")
        dt.Columns.Add("IDAU")

        For Each oDataGridItem In Me.DataGrid1.Items
            chkExport = oDataGridItem.FindControl("ChSelezionato")
            If chkExport.Checked Then
                ROW = dt.NewRow()
                ROW.Item("COD_CONTRATTO") = oDataGridItem.Cells(1).Text
                ROW.Item("PG_AU") = oDataGridItem.Cells(20).Text
                ROW.Item("COGNOME") = oDataGridItem.Cells(7).Text
                ROW.Item("NOME") = oDataGridItem.Cells(8).Text
                ROW.Item("TIPOLOGIA") = oDataGridItem.Cells(9).Text
                ROW.Item("DECORRENZA") = oDataGridItem.Cells(10).Text
                ROW.Item("SCADENZA") = oDataGridItem.Cells(11).Text
                ROW.Item("INDIRIZZO_UNITA") = oDataGridItem.Cells(2).Text
                ROW.Item("CIVICO_UNITA") = oDataGridItem.Cells(3).Text
                ROW.Item("COMUNE_UNITA") = oDataGridItem.Cells(6).Text
                ROW.Item("CAP_UNITA") = oDataGridItem.Cells(4).Text
                ROW.Item("FILIALE") = oDataGridItem.Cells(5).Text
                ROW.Item("PREVALENTE") = oDataGridItem.Cells(12).Text
                ROW.Item("PRESENZA_15") = oDataGridItem.Cells(13).Text
                ROW.Item("PRESENZA_65") = oDataGridItem.Cells(14).Text
                ROW.Item("N_INV_100_CON") = oDataGridItem.Cells(15).Text
                ROW.Item("N_INV_100_SENZA") = oDataGridItem.Cells(16).Text
                ROW.Item("N_INV_66_99") = oDataGridItem.Cells(17).Text
                ROW.Item("IDC") = oDataGridItem.Cells(18).Text
                ROW.Item("IDAU") = oDataGridItem.Cells(19).Text
                dt.Rows.Add(ROW)
                I = I + 1
            End If
        Next

        If I > 0 Then
            HttpContext.Current.Session.Add("ElencoDT", dt)
            Response.Write("<script>self.close();</script>")
        Else
            Response.Write("<script>alert('Nessuna riga selezionata');</script>")
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub
End Class
