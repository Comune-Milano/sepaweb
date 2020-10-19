
Partial Class CENSIMENTO_RisultatiUI2
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String = ""
    Dim vEdificio As String
    Dim vIndirizzo As String
    Dim vCivico As String
    Dim vTipo As String
    Dim vComplesso As String
    Dim vInterno As String
    Dim vAsc As String
    Dim vDisp As String
    Dim vScala As String
    Dim vidind As String

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


            vCivico = par.IfEmpty(Request.QueryString("C"), "")
            vInterno = par.IfEmpty(Request.QueryString("INT"), "")
            vScala = par.IfEmpty(Request.QueryString("SCAL"), "")
            vIndirizzo = par.IfEmpty(Request.QueryString("IND"), "")



            CercaSelettiva()


        End If

    End Sub
   
    Private Sub CercaSelettiva()
        Dim s As String = ""
        Dim s1 As String = ""

        Try
            QUERY = "SELECT DISTINCT ROWNUM,  EDIFICI.DENOMINAZIONE, SISCOM_MI.UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ,UNITA_IMMOBILIARI.INTERNO,  (SCALE_EDIFICI.DESCRIZIONE)AS SCALA ,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.identificativi_catastali where UNITA_IMMOBILIARI.ID_SCALA= SCALE_EDIFICI.ID(+) AND TIPOLOGIA_UNITA_IMMOBILIARI.COD= UNITA_IMMOBILIARI.COD_TIPOLOGIA and SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID(+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND " _
                  & "unita_immobiliari.ID_INDIRIZZO IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI " _
                  & "  "

            If vIndirizzo <> "" Then
                s1 = "where SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & par.PulisciStrSql(vIndirizzo) & "' "
            End If

            If vCivico <> "" Then
                If s1 <> "" Then
                    s1 = s1 & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & par.PulisciStrSql(vCivico) & "' "
                Else
                    s1 = " where SISCOM_MI.INDIRIZZI.CIVICO = '" & par.PulisciStrSql(vCivico) & "' "
                End If
            End If

            QUERY = QUERY & s1 & ") "

            If vScala <> "" And vScala <> "-1" Then
                s = s & " and SCALE_EDIFICI.ID = " & vScala
            End If

            If vInterno <> "" And vInterno <> "-1" Then
                s = s & " AND UNITA_IMMOBILIARI.INTERNO ='" & vInterno & "' "
            End If

            QUERY = QUERY & s & " ORDER BY ROWNUM ASC "

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
        da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
        Datagrid1.DataSource = ds
        Datagrid1.DataBind()
        LnlNumeroRisultati.Text = "  - " & Datagrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        If Session.Item("PED") <> Nothing Then
            Response.Write("<script>document.location.href=""DirettaUI.aspx""</script>")
            Session.Remove("PED")

        Else
            Response.Write("<script>document.location.href=""RicercaUI.aspx""</script>")
        End If

    End Sub

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    LBLID.Text = e.Item.Cells(0).Text
    '    Label2.Text = "Hai selezionato la riga n°: " & e.Item.Cells(1).Text

    'End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità COD. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow';}")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro';}")
            e.Item.Attributes.Add("onclick", "if (selezionato) {selezionato.style.backgroundColor='';}selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'unità COD. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Datagrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            Datagrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna Unita selezionata!')</script>")
        Else
            Response.Redirect("ElencoSegnalazioni.aspx?U=" & txtid.Text)
        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub
End Class
