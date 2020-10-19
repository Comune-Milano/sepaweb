Partial Class CENSIMENTO_Tab_AdDimens
    Inherits UserControlSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Private Property vId() As Long
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

                Case "Inserimento Unità Comuni"
                    Passato = "UC"
                    sStringaSql = "Select rownum, ID, TIPOLOGIA_DIMENSIONI.DESCRIZIONE AS TIPO, to_char(DIMENSIONI.VALORE,'999G999G990D99') as VALORE FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.TIPOLOGIA_DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_DIMENSIONI.COD AND ID_UNITA_COMUNE = " & vId & " ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

                Case "Unita Immobiliari"
                    Passato = "UI"
                    sStringaSql = "Select rownum ,ID, TIPOLOGIA_DIMENSIONI.DESCRIZIONE AS TIPO, to_char(DIMENSIONI.VALORE,'999G999G990D99') as VALORE FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.TIPOLOGIA_DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_DIMENSIONI.COD AND ID_UNITA_IMMOBILIARE = " & vId & " ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

                Case "Inserimento EDIFICI"
                    Passato = "ED"
                    sStringaSql = "Select rownum, ID, TIPOLOGIA_DIMENSIONI.DESCRIZIONE AS TIPO, to_char(DIMENSIONI.VALORE,'999G999G990D99') as VALORE FROM SISCOM_MI.DIMENSIONI, SISCOM_MI.TIPOLOGIA_DIMENSIONI WHERE SISCOM_MI.DIMENSIONI.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_DIMENSIONI.COD AND ID_EDIFICIO = " & vId & " ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()

            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            If Me.Page.Title = "Unita Immobiliari" Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


            Dim ds As New Data.DataSet()
            da.Fill(ds, "DIMENSIONI")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            da.Dispose()
            ds.Dispose()
            If Me.Page.Title = "Unita Immobiliari" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

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

   

    'Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
    '    HFtxtId.Value = e.Item.Cells(0).Text
    'End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AdDimens1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_AdDimens1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")
    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AdDimens1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_AdDimens1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    'End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property
End Class
