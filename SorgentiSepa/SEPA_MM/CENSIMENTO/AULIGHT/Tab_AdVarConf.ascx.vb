
Partial Class CENSIMENTO_Tab_AdVarConf
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
                Case "Unita Immobiliari"
                    Passato = "UI"
                    sStringaSql = "Select ROWNUM , ID, TIPOLOGIA_VARIAZIONE_CONFIG.DESCRIZIONE AS TIPO, VARIAZIONI_CONFIGURAZIONE.DESCRIZIONE FROM SISCOM_MI.VARIAZIONI_CONFIGURAZIONE, SISCOM_MI.TIPOLOGIA_VARIAZIONE_CONFIG WHERE SISCOM_MI.VARIAZIONI_CONFIGURAZIONE.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_VARIAZIONE_CONFIG.COD AND ID_UNITA = " & vId & "ORDER BY ROWNUM ASC"
                    QUERY = sStringaSql
                    BindGrid()
                Case "Inserimento EDIFICI"
                    Passato = "ED"
                    sStringaSql = "Select ROWNUM, ID, TIPOLOGIA_VARIAZIONE_CONFIG.DESCRIZIONE AS TIPO, VARIAZIONI_CONFIGURAZIONE.DESCRIZIONE FROM SISCOM_MI.VARIAZIONI_CONFIGURAZIONE, SISCOM_MI.TIPOLOGIA_VARIAZIONE_CONFIG WHERE SISCOM_MI.VARIAZIONI_CONFIGURAZIONE.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_VARIAZIONE_CONFIG.COD AND ID_EDIFICIO = " & vId & "ORDER BY ROWNUM ASC"
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



    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AdVarConf1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_AdVarConf1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AdVarConf1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_AdVarConf1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    'End Sub
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
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
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
