
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Str As String
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        If Not IsPostBack Then

            Response.Flush()
            Cerca()

        End If

    End Sub

    Private Sub Cerca()

        Try
            Query = Session.Item("RICUTENTE")
            BindGrid()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Public Property Query() As String
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
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)

        Dim dt As New Data.DataTable()
        da.Fill(dt)
        'For Each riga As Data.DataRow In dt.Rows
        If dt.Rows.Count > 0 Then
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
        Else
            TrovaIntestaNucleo()
        End If
        'Next

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Private Sub TrovaIntestaNucleo()
        sStringaSql = Query
        sStringaSql = sStringaSql.Replace("AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'", "")
        par.cmd.CommandText = sStringaSql

        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim contratti As String = ""
        Dim Primo As Boolean = True
        Dim inte As String = ""
        While Lettore.Read
            inte = par.IfNull(Lettore("INTESTATARIO"), "")
            If Primo = True Then
                contratti = Lettore("ID_CONTRATTO")
                Primo = False
            Else
                contratti = contratti & "," & Lettore("ID_CONTRATTO")
            End If
        End While
        If Not String.IsNullOrEmpty(contratti) Then

            sStringaSql = "SELECT DISTINCT ANAGRAFICA.ID AS ID_ANAGRAFICA,SOGGETTI_CONTRATTUALI.ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO, CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"", " _
                        & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""CFIVA"",TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASCITA , ANAGRAFICA.SESSO, ANAGRAFICA.TELEFONO," _
                        & "RTRIM(LTRIM(ANAGRAFICA.INDIRIZZO_RESIDENZA||','|| ANAGRAFICA.CIVICO_RESIDENZA)) AS RESIDENZA,ANAGRAFICA.COMUNE_RESIDENZA, ANAGRAFICA.PROVINCIA_RESIDENZA " _
                        & "FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE, " _
                        & "SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.ANAGRAFICA, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI, " _
                        & "SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.IDENTIFICATIVI_CATASTALI  WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
                        & "RAPPORTI_UTENZA.ID= SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID AND  RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = TIPOLOGIA_CONTRATTO_LOCAZIONE.COD " _
                        & "AND UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO= TIPO_LIVELLO_PIANO.COD AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) " _
                        & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND data_fine>= to_char(to_date(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') AND COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO IN (" & contratti & ")  ORDER BY INTESTATARIO ASC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count
            Response.Write("<script>alert('Il soggetto ricercato non risulta esser intestatario di un Rapporto!\nVerranno caricati i contratti su cui " & inte & " è componente del nucleo!')</script>")
        Else
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(Query, par.OracleConn)
            Dim dt As New Data.DataTable()
            da.Fill(dt)
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count

        End If


    End Sub
    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Session.Remove("RICUTENTE")
        Response.Write("<script>document.location.href=""RicUtente.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("RICUTENTE")
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        'Response.Write("<script>onClick='window.print()'</script>")

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'utente: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('IdAnagrafica').value='" & e.Item.Cells(0).Text & "';document.getElementById('CFIVA').value='" & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(3).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'utente: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('IdAnagrafica').value='" & e.Item.Cells(0).Text & "';document.getElementById('CFIVA').value='" & e.Item.Cells(1).Text & "';document.getElementById('IdContratto').value='" & e.Item.Cells(3).Text & "'")

        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If IdAnagrafica.Value = "" Then
            Response.Write("<script>alert('Nessun Utente selezionato!')</script>")
        Else
            Response.Write("<script>window.open('DatiUtenza.aspx?C=RisUtenza&IDANA=" & Me.IdAnagrafica.Value & "&IDCONT=" & IdContratto.Value & "','DatiUtente', '');</script>")
            'Response.Write("<script>window.open('Dimensioni.aspx?ID=" & vId & ",&Pas=" & Passato & "','DatiDimensioni',  width=450, height=160');</script>")

        End If
    End Sub


End Class
