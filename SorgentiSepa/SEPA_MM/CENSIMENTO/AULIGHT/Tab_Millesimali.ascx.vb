
Partial Class CENSIMENTO_Tab_UtMillesimali
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
    Public Property vIdTipologia() As String
        Get
            If Not (ViewState("par_IdTipologia") Is Nothing) Then
                Return CStr(ViewState("par_IdTipologia"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdTipologia") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                vId = CType(Me.Page, Object).vId
                cerca()
            End If
           

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub

    Private Sub cerca()
        Try

            Select Case Me.Page.Title

                Case "Inserimento Complessi"

                    sStringaSql = "select ROWNUM, ID, TABELLE_MILLESIMALI.DESCRIZIONE, TIPOLOGIA_MILLESIMALE.DESCRIZIONE as tipo, DEscrizione_tabella from SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE where SISCOM_MI.TABELLE_MILLESIMALI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_MILLESIMALE.COD and ID_COMPLESSO = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

                Case "Inserimento EDIFICI"
                    sStringaSql = "select ROWNUM, ID, TABELLE_MILLESIMALI.DESCRIZIONE, TIPOLOGIA_MILLESIMALE.DESCRIZIONE as tipo, DEscrizione_tabella from SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE where SISCOM_MI.TABELLE_MILLESIMALI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_MILLESIMALE.COD and ID_EDIFICIO = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub
    Private Property QUERY() As String
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
            par.SettaCommand(par)

            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "INTERV_ADEG_NORM")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Millesimali1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Millesimali1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Millesimali1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Millesimali1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    'End Sub

    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property
    

End Class
