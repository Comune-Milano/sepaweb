
Partial Class NEW_CENSIMENTO_Tab_NonIspezion
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaCmb()
            Cerca()
            If Session("SLE") = "1" Or CType(Me.Page.FindControl("txtVisibility"), HiddenField).Value = 1 Then
                Me.BtnElimina.Visible = False
                Me.BtnSalva.Visible = False
            End If
        End If


    End Sub
    Private Sub CaricaCmb()
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


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "Select * from SISCOM_MI.locali"
            myReader1 = par.cmd.ExecuteReader()
            cmbLocali.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbLocali.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub
    Private Sub Cerca()
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


            Dim StringaSql As String = ""

            If Me.Page.Title = "Inserimento EDIFICI" Then
                StringaSql = "SELECT ID_LOCALE , descrizione FROM SISCOM_MI.LOCALI, SISCOM_MI.LOCALI_NO_ACCESSIBILI WHERE LOCALI_NO_ACCESSIBILI.id_EDIFICIO = " & CType(Me.Page, Object).vId & " AND LOCALI_NO_ACCESSIBILI.ID_LOCALE = LOCALI.ID"
            ElseIf Me.Page.Title = "Inserimento Complessi" Then
                StringaSql = "SELECT ID_LOCALE , descrizione FROM SISCOM_MI.LOCALI, SISCOM_MI.LOCALI_NO_ACCESSIBILI WHERE LOCALI_NO_ACCESSIBILI.ID_COMPLESSO = " & CType(Me.Page, Object).vId & " AND LOCALI_NO_ACCESSIBILI.ID_LOCALE = LOCALI.ID"
            End If

            par.cmd.CommandText = StringaSql
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")

            DgvLocNoAccess.DataSource = ds
            DgvLocNoAccess.DataBind()

            If Session("SLE") = "1" Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSalva.Click
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        If Me.Page.Title = "Inserimento EDIFICI" Then


            If Me.cmbLocali.SelectedValue <> -1 Then
                Dim i As Integer
                Dim dt As New Data.DataTable
                Dim stringa As String = "select id_locale from SISCOM_MI.locali_no_accessibili where id_edificio =" & CType(Me.Page, Object).vId

                par.cmd.CommandText = stringa
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If cmbLocali.SelectedValue = dt.Rows(i).Item("ID_LOCALE") Then
                        Response.Write("<script>alert('Locale già iserito!')</script>")
                        Exit Sub
                        Me.cmbLocali.SelectedValue = -1

                    End If
                Next
                par.cmd.CommandText = "insert into SISCOM_MI.locali_no_accessibili (ID_EDIFICIO,id_locale) values (" & CType(Me.Page, Object).vId & ", " & Me.cmbLocali.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Me.cmbLocali.SelectedValue = -1
                Cerca()
            End If
        ElseIf Me.Page.Title = "Inserimento Complessi" Then

            If Me.cmbLocali.SelectedValue <> -1 Then
                Dim i As Integer
                Dim dt As New Data.DataTable
                Dim stringa As String = "select id_locale from SISCOM_MI.locali_no_accessibili where id_complesso =" & CType(Me.Page, Object).vId

                par.cmd.CommandText = stringa
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)


                If Not IsNothing(da) Then
                    da.Fill(dt)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If cmbLocali.SelectedValue = dt.Rows(i).Item("ID_LOCALE") Then
                        Response.Write("<script>alert('Locale già iserito!')</script>")
                        Exit Sub

                        Me.cmbLocali.SelectedValue = -1

                    End If
                Next
                par.cmd.CommandText = "insert into SISCOM_MI.locali_no_accessibili (ID_COMPLESSO,id_locale) values (" & CType(Me.Page, Object).vId & ", " & Me.cmbLocali.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                Me.cmbLocali.SelectedValue = -1
                'par.OracleConn.Close()
                Cerca()
            End If
        End If

    End Sub

    Protected Sub BtnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnElimina.Click
        Try

            If HFtxtId.Value <> "" Then
                If Me.Page.Title = "Inserimento EDIFICI" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.locali_no_accessibili where id_EDIFICIO = " & CType(Me.Page, Object).vId & " and id_locale = '" & HFtxtId.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    HFtxtId.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"


                ElseIf Me.Page.Title = "Inserimento Complessi" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "delete from SISCOM_MI.locali_no_accessibili where id_complesso = " & CType(Me.Page, Object).vId & " and id_locale = '" & HFtxtId.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    'par.OracleConn.Close()
                    HFtxtId.Value = ""
                    Me.txtmia.Text = "Nessuna Selezione"

                End If
                Cerca()
            Else
                Response.Write("<script>alert('Selezionare il rigo da cancellare!');</script>")

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try
    End Sub

    Protected Sub DgvLocNoAccess_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DgvLocNoAccess.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_NonIspezion1_txtmia').value='Hai selezionato il locale: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_NonIspezion1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_NonIspezion1_txtmia').value='Hai selezionato il locale: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_NonIspezion1_HFtxtId').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub
End Class
