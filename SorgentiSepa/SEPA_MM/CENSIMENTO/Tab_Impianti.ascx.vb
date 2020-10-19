
Partial Class CENSIMENTO_Tab_Impianti
    Inherits UserControlSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global

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
            CaricaImpianti()
        End If
        '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
        If vId = 0 Then
            vId = CType(Me.Page, Object).vId
        End If
        '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************

    End Sub
    Public Sub CaricaImpianti()
        Try
            If Session("SLE") = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If
            sStringaSql = "SELECT ID, cod_impianto,tipologia_impianti.descrizione AS TIPOLOGIA ,IMPIANTI.descrizione FROM siscom_mi.impianti,SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA AND id_complesso = " & vId

            par.cmd.CommandText = sStringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "IMPIANTI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ComEdifici1_txtmia').value='Hai selezionato l\'edificio con cod. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ComEdifici1_txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ComEdifici1_txtmia').value='Hai selezionato l\'edificio con cod. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_ComEdifici1_txtid').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub
End Class
