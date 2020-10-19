
Partial Class CENSIMENTO_Tab_ComEdifici
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String = ""

    Public Property vId() As Long
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = CType(Me.Page, Object).vId
            CaricaElencoEdifici()
        End If
        '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
        If vId = 0 Then
            vId = CType(Me.Page, Object).vId
        End If
        '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************

    End Sub
    Public Sub CaricaElencoEdifici()
        Try
   
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            sStringaSql = "SELECT ROWNUM,COD_EDIFICIO,SISCOM_MI.EDIFICI.ID, EDIFICI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, COMUNI_NAZIONI.NOME FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI WHERE SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE = SISCOM_MI.INDIRIZZI.ID (+) AND SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+) and EDIFICI.ID_COMPLESSO = " & vId


            par.cmd.CommandText = sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "EDIFICI_COMPLESSI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try

    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Not IsNothing(txtid.Value) And txtid.Value <> 0 Then

            If vId <> -1 Then
                If CType(Me.Page.FindControl("txtModificato"), HiddenField).Value <> "111" Then

                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("TRANSAZIONE")
                    HttpContext.Current.Session.Remove("CONNESSIONE")
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Item("LAVORAZIONE") = 0

                    Response.Redirect("InserimentoEdifici.aspx?C=InserimentoComplessi&ID=" & txtid.Value & "&COMPLESSO=" & vId)
                    Me.txtid.Value = 0
                    Me.txtmia.Text = ""
                Else
                    CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "1"
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                End If

            End If

        Else
            Response.Write("<script>alert('Nessun Edificio selezionato!')</script>")
        End If

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ComEdifici1_txtmia').value='Hai selezionato l\'edificio con cod. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ComEdifici1_txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ComEdifici1_txtmia').value='Hai selezionato l\'edificio con cod. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ComEdifici1_txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub btnNewEdi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNewEdi.Click
        If CType(Me.Page.FindControl("txtModificato"), HiddenField).Value <> "111" Then
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("TRANSAZIONE")
            HttpContext.Current.Session.Remove("CONNESSIONE")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = 0
            Response.Redirect("InserimentoEdifici.aspx?C=InserimentoComplessi&IDC=" & vId & "&COMPLESSO=" & vId)
            Me.txtid.Value = 0
            Me.txtmia.Text = ""

        Else
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub
End Class
