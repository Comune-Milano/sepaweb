
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_RiepRitLegge
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        solaLettura.value = Request.QueryString("SL")
        If Not IsPostBack Then
            CaricaDati()
        End If
    End Sub
    Private Sub CaricaDati()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT PAGAMENTI.ID,trim(TO_CHAR(SUM(rit_legge_ivata),'9G999G999G999G999G990D99')) AS RIT_LEGGE, " _
                                & "TO_CHAR(TO_DATE(PAGAMENTI.data_stampa,'YYYYmmdd'),'DD/MM/YYYY') AS data_stampa,progr,PAGAMENTI.anno " _
                                & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID = PRENOTAZIONI.id_pagamento AND PRENOTAZIONI.ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & Request.QueryString("IDAPP") & ")))" _
                                & " And PRENOTAZIONI.ID_PAGAMENTO Is Not NULL AND PAGAMENTI.ID_STATO >0 " _
                                & "GROUP BY PAGAMENTI.progr,PAGAMENTI.anno,PAGAMENTI.data_stampa,PAGAMENTI.ID " _
                                & "ORDER BY anno,progr ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            par.OracleConn.Close()

            Dim somma As Decimal = 0

            For Each row As Data.DataRow In dt.Rows
                somma = somma + CDec(par.IfNull(row.Item("RIT_LEGGE"), 0).ToString.Replace(".", ""))
            Next

            dgvRitLegge.DataSource = dt
            dgvRitLegge.DataBind()
            Me.txtTotale.Text = Format(somma, "##,##0.00")

        Catch ex As Exception
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try
    End Sub

    Protected Sub dgvRitLegge_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvRitLegge.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('txtmia').value='Hai selezionato il PROGR:" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('ImageButtonDettaglio').click();")
        End If


        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il PROGR:" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il PROGR:" & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
        'End If

    End Sub

    Protected Sub ImageButtonDettaglio_Click(sender As Object, e As System.EventArgs) Handles ImageButtonDettaglio.Click
        CaricaDati()
        txtid.Value = ""
        txtmia.Text = "Nessuna Selezione"
    End Sub

    Protected Sub ImgButtonDettaglio_Click(sender As Object, e As System.EventArgs) Handles ImgButtonDettaglio.Click
        CaricaDati()
        txtid.Value = ""
        txtmia.Text = "Nessuna Selezione"
    End Sub
End Class
