
Partial Class Condomini_RisultatiIndirizzo
    Inherits PageSetIdMode
    Dim vComplesso As String
    Dim vEdificio As String
    Dim vIndirizzo As String
    Dim vCivico As String
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then
            '********BISOGNA SEMPRE METTERLO NEL POSTBACK!
            '********SE FUORI EVENTUALI METODI CHE USANO LA RESP.WRITE SI IMBALLANO PERCHè LUI LE PULISCE TUTTE!
            Response.Flush()
            vComplesso = Request.QueryString("C")
            vEdificio = Request.QueryString("E")
            'vIndirizzo = Request.QueryString("I")
            'vCivico = Request.QueryString("Civ")
            Cerca()
            If Request.QueryString("P") <> "" Then
                cambiataPagina(Request.QueryString("P"))
                'Request.QueryString("P") = ""
            End If

        End If
    End Sub
    Private Sub Cerca()
        Try

            Dim bTrovato As Boolean
            '            Dim sValore As String
            Dim condizione As String = ""
            Dim QueryComplessi As String = ""
            Dim QueryEdifici as String = ""


            bTrovato = False


            QUERY = "SELECT  TO_CHAR(CONDOMINI.ID,'00000') AS COD_CONDOMINIO,COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME ||' '|| COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE  COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL "

            If Not String.IsNullOrEmpty(vEdificio) And vEdificio <> "-1" Then
                QueryEdifici = " AND CONDOMINI.ID IN (SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE COND_EDIFICI.ID_EDIFICIO = " & vEdificio & ") "
                QUERY = QUERY & " " & QueryEdifici
            ElseIf Not String.IsNullOrEmpty(vComplesso) And vComplesso <> "-1" Then
                QueryComplessi = "AND CONDOMINI.ID IN (SELECT ID_CONDOMINIO FROM SISCOM_MI.COND_EDIFICI WHERE COND_EDIFICI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO =" & vComplesso & " )) "
                QUERY = QUERY & " " & QueryComplessi

                'bTrovato = True
                'condizione = " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = " & vComplesso & ""
            Else

            End If

            'If Not String.IsNullOrEmpty(QueryEdifici) Then
            '    QUERY = QueryEdifici

            'ElseIf Not String.IsNullOrEmpty(QueryComplessi) Then
            '    QUERY = QueryComplessi
            'Else
            '    QUERY = "SELECT  COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME || COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO , COMPLESSI_IMMOBILIARI.DENOMINAZIONE  AS COMP_EDIF FROM SISCOM_MI.CONDOMINI, SISCOM_MI.INDIRIZZI, SISCOM_MI.COMPLESSI_IMMOBILIARI , SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE CONDOMINI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID (+) AND  INDIRIZZI.ID(+) = COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL AND CONDOMINI.ID_EDIFICIO IS NULL UNION SELECT  COND_AMMINISTRATORI.ID AS ID_AMM , (COND_AMMINISTRATORI.COGNOME || COND_AMMINISTRATORI.NOME )AS AMMINIST, CONDOMINI.ID, CONDOMINI.DENOMINAZIONE AS CONDOMINIO, INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO , EDIFICI.DENOMINAZIONE  AS COMP_EDIF FROM SISCOM_MI.CONDOMINI, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI , SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE CONDOMINI.ID_EDIFICIO = EDIFICI.ID (+) AND  INDIRIZZI.ID(+) = EDIFICI.ID_INDIRIZZO_PRINCIPALE AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL AND CONDOMINI.ID_COMPLESSO IS NULL "
            'End If


            'If vIndirizzo <> "-1" AndAlso par.IfEmpty(vIndirizzo, "Null") <> "Null" Then
            '    sValore = vIndirizzo
            '    condizione = condizione & "AND INDIRIZZI.DESCRIZIONE = '" & sValore & "'"
            '    If Not String.IsNullOrEmpty(vCivico) Then
            '        sValore = vCivico
            '        condizione = condizione & " AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
            '    End If
            'End If

            QUERY = QUERY & " ORDER BY CONDOMINIO ASC"

            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "CONDOMINI, INDIRIZZI,COMPLESSI_EDIFICI")
        DataGridCondom.DataSource = ds
        DataGridCondom.DataBind()
        LnlNumeroRisultati.Text = "  - " & DataGridCondom.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
        '*********************CHIUSURA CONNESSIONE**********************

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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

    Protected Sub DataGridCondom_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCondom.ItemDataBound
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

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>parent.main.location.replace('RicercaIndirizzo.aspx');</script>")

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click

        If Not String.IsNullOrEmpty(txtid.Value) Then
            Response.Write("<script>parent.main.location.replace('Condominio.aspx?IdCond=" & txtid.Value & "&CALL=RisultatiIndirizzo&C=" & Request.QueryString("C") & "&E=" & Request.QueryString("E") & "&P=" & Me.DataGridCondom.CurrentPageIndex & "');</script>")
        Else
            Response.Write("<script>alert('Selezionare un condominio da visualizzare!');</script>")
        End If

    End Sub

    Protected Sub DataGridCondom_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridCondom.PageIndexChanged
        cambiataPagina(e.NewPageIndex)
    End Sub
    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            DataGridCondom.CurrentPageIndex = numero
            BindGrid()
        End If
    End Sub
End Class
