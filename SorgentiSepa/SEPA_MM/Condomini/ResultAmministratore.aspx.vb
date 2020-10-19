﻿
Partial Class Condomini_ResultAmministratore
    Inherits PageSetIdMode
    Dim vIdAmministratore As String
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            vIdAmministratore = Request.QueryString("AMM")
            Cerca()
            If Request.QueryString("P") <> "" Then
                cambiataPagina(Request.QueryString("P"))
                'Request.QueryString("P") = ""
            End If

        End If
    End Sub
    Private Sub Cerca()
        Try
            QUERY = "SELECT  TO_CHAR(CONDOMINI.ID,'00000') AS COD_CONDOMINIO,COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME ||' '|| COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE  COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "
            'QUERY = "SELECT  COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME || COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID,CONDOMINI.DENOMINAZIONE AS CONDOMINIO, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO , (COMPLESSI_IMMOBILIARI.DENOMINAZIONE || EDIFICI.DENOMINAZIONE) AS COMP_EDIF FROM SISCOM_MI.CONDOMINI, SISCOM_MI.INDIRIZZI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE (CONDOMINI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID(+) AND CONDOMINI.ID_EDIFICIO = EDIFICI.ID(+)) AND (INDIRIZZI.ID= EDIFICI.ID_INDIRIZZO_PRINCIPALE OR INDIRIZZI.ID = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO) AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "
            If vIdAmministratore <> "-1" Then
                QUERY = QUERY & " AND COND_AMMINISTRATORI.ID=" & vIdAmministratore
            End If
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Public Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "CONDOMINI, INDIRIZZI,COMPLESSI_EDIFICI")
        DataGridAmminist.DataSource = ds
        DataGridAmminist.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGridAmminist.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    End Sub
    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>parent.main.location.replace('RicercaAmminist.aspx');</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub

    Protected Sub DataGridAmminist_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAmminist.ItemDataBound
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

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Not String.IsNullOrEmpty(txtid.Value) Then
            Response.Write("<script>parent.main.location.replace('Condominio.aspx?IdCond=" & txtid.Value & "&AMM=" & Request.QueryString("AMM") & "&CALL=ResultAmministratore&P=" & Me.DataGridAmminist.CurrentPageIndex & "');</script>")
        Else
            Response.Write("<script>alert('Selezionare un condominio da visualizzare!');</script>")
        End If

    End Sub

    Protected Sub DataGridAmminist_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridAmminist.PageIndexChanged
        cambiataPagina(e.NewPageIndex)
    End Sub
    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            DataGridAmminist.CurrentPageIndex = numero
            BindGrid()
        End If
    End Sub
End Class
