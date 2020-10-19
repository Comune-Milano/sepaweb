
Partial Class CENSIMENTO_EdificiUI
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            vEdificio = Request.QueryString("E")
            'LBLID.Text = "-1"
            LblEdificio.Text = vEdificio
            If vEdificio <> "-1" Then

                cerca()

            End If
            If Session("PED2_SOLOLETTURA") = "1" Then
                Me.btnNuovo.Visible = False
            End If
            If Session.Item("SLE") = 1 Then
                Me.btnNuovo.Visible = False
            End If
        End If

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
        Try

  

            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(QUERY, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "UNITA_COMUNI, INDIRIZZI,COMUNI_NAZIONI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            LnlNumeroRisultati.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()

        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub
    Private Sub cerca()

        Dim bTrovato As Boolean
        'Dim sValore As String
        Dim condizione As String = ""

        bTrovato = False
        sStringaSql = ""
        sStringaSql = "SELECT DISTINCT ROWNUM,  SISCOM_MI.UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ,UNITA_IMMOBILIARI.INTERNO, (SCALE_EDIFICI.DESCRIZIONE)AS SCALA  FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.SCALE_EDIFICI where UNITA_IMMOBILIARI.ID_SCALA= SCALE_EDIFICI.ID(+) AND TIPOLOGIA_UNITA_IMMOBILIARI.COD= UNITA_IMMOBILIARI.COD_TIPOLOGIA and SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID(+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) and UNITA_IMMOBILIARI.ID_EDIFICIO = " & vEdificio
        sStringaSql = sStringaSql & " ORDER BY ROWNUM ASC"
        QUERY = sStringaSql
        BindGrid()
    End Sub
    Public Property vEdificio() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
        'Response.Redirect("InserimentoEdifici.aspx?ID=" & vEdificio)
        Response.Redirect("InserimentoEdifici.aspx?C=EdificiUI&ID=" & vEdificio & "&COMPLESSO=" & Request.QueryString("COMPLESSO"))

    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovo.Click
        Response.Redirect("InserimentoUniImmob.aspx?C=EdificiUI&IDED=" & vEdificio & "&EDIFICIO=" & Request.QueryString("E"))
        'Response.Write("<script>top.location.href=" & Chr(34) & "InserimentoUniImmob.aspx?IDED=" & vEdificio & Chr(34) & ";</script>")
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna Unita selezionata!')</script>")
        Else
            If Session.Item("SLE") = "1" Then
                Response.Redirect("InserimentoUniImmob.aspx?LE=1&ID=" & txtid.Text & "&C=EdificiUI&EDIFICIO=" & Request.QueryString("E") & "&COMPLESSO=" & Request.QueryString("COMPLESSO"))
            Else
                Response.Redirect("InserimentoUniImmob.aspx?ID=" & txtid.Text & "&C=EdificiUI&EDIFICIO=" & Request.QueryString("E"))
            End If

        End If

    End Sub


    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub
End Class
