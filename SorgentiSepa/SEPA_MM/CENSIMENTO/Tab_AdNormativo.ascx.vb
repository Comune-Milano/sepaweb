
Partial Class CENSIMENTO_Tab_AdNormativo
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String = ""
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
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            If Not IsPostBack Then
                vId = CType(Me.Page, Object).vId
                If Session("PED2_SOLOLETTURA") = "1" Then
                    FrmSolaLettura()
                End If
                If Session("SLE") = "1" Then
                    FrmSolaLettura()
                End If

            End If
            '**********SERVE PER RECUPERARE ID SUBITO DOPO NUOVO INSERIMENTO IMMOBILE*****************
            If vId = 0 Then
                vId = CType(Me.Page, Object).vId
            End If
            '**********************FINE MODIFICA PER ID NUOVO INSERMENTO******************************

            cerca()


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message

        End Try
    End Sub
    Private Sub FrmSolaLettura()
        Try
            Me.BtnElimina.Visible = False
            Me.BtnADD.Visible = False
            'Dim CTRL As Control = Nothing
            'For Each CTRL In Me.Form1.Controls
            '    If TypeOf CTRL Is TextBox Then
            '        DirectCast(CTRL, TextBox).Enabled = False
            '    ElseIf TypeOf CTRL Is DropDownList Then
            '        DirectCast(CTRL, DropDownList).Enabled = False
            '    End If
            'Next
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
                    sStringaSql = "Select ROWNUM, ID, TIPOLOGIA_ADEGUAMENTO.DESCRIZIONE AS TIPO, INTERV_ADEG_NORM.DESCRIZIONE FROM SISCOM_MI.INTERV_ADEG_NORM, SISCOM_MI.TIPOLOGIA_ADEGUAMENTO WHERE SISCOM_MI.INTERV_ADEG_NORM.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_ADEGUAMENTO.COD AND ID_UNITA_IMMOBILIARE = " & vId & " ORDER BY ROWNUM ASC "
                    QUERY = sStringaSql
                    BindGrid()

                Case "Inserimento EDIFICI"
                    Passato = "ED"
                    sStringaSql = "Select ROWNUM, ID, TIPOLOGIA_ADEGUAMENTO.DESCRIZIONE AS TIPO, INTERV_ADEG_NORM.DESCRIZIONE FROM SISCOM_MI.INTERV_ADEG_NORM, SISCOM_MI.TIPOLOGIA_ADEGUAMENTO WHERE SISCOM_MI.INTERV_ADEG_NORM.COD_TIPOLOGIA = SISCOM_MI.TIPOLOGIA_ADEGUAMENTO.COD AND ID_EDIFICIO = " & vId & " ORDER BY ROWNUM ASC "
                    QUERY = sStringaSql
                    BindGrid()
            End Select
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
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
        Try

            If Session("SLE") = "1" And Me.Page.Title = "Unita Immobiliari" Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            Else

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If
            par.cmd.CommandText = QUERY
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "INTERV_ADEG_NORM")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            ds.Clear()
            da.Dispose()

            If Session("SLE") = "1" And Me.Page.Title = "Unita Immobiliari" Then
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
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AdNormativo1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_AdNormativo1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_AdNormativo1_txtmia').value='Hai selezionato la riga n. " & e.Item.Cells(1).Text & "';document.getElementById('Tab_AdNormativo1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub

    Protected Sub BtnADD_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnADD.Click
        Response.Write("<script>window.open('IntervAdeguaNorm.aspx?ID=" & vId & ",&Pas=" & Passato & "','VARIAZIONI', 'resizable=yes, width=650, height=250');</script>")

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

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        If HFtxtId.Value = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Delete()
            cerca()

        End If

    End Sub
    Private Sub Delete()
        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.INTERV_ADEG_NORM WHERE ID = " & HFtxtId.Value
            par.cmd.ExecuteNonQuery()
            Select Case Passato
                Case "UI"
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI(ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F56','INTERVENTI ADEGUAMENTO NORMATIVO')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

                Case "ED"
                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_EDIFICI(ID_EDIFICIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F56','INTERVENTI ADEGUAMENTO NORMATIVO')"
                    par.cmd.ExecuteNonQuery()
                    '*******************************FINE****************************************
                    '***************************************************************************

            End Select
            Session.Item("MODIFICASOTTOFORM") = 1
            CType(Me.Page, Object).VerificaModificheSottoform()

            'Response.Write("<SCRIPT>alert('Elemento selezionato eiliminato correttamente!');</SCRIPT>")
            'Response.Write("<script language='javascript'> { self.close() }</script>")
            Me.HFtxtId.Value = ""
            Me.txtmia.Text = ""

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try

    End Sub
End Class
