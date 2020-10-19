
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_ElencoPrezzi
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim lstprezzi As New System.Collections.Generic.List(Of Mario.ElencoPrezzi)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lstprezzi = CType(HttpContext.Current.Session.Item("LSTPREZZI"), System.Collections.Generic.List(Of Mario.ElencoPrezzi))

        If Not IsPostBack Then
            If Not IsNothing(lstprezzi) Then
                lstprezzi.Clear()
            End If
            CaricaElPrezzi()
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSoloLettura()
            End If

        End If

    End Sub
    Private Sub CaricaElPrezzi()

        Try
            If CType(Me.Page, Object).vIdAppalti = 0 Then
                DataGridElPrezzi.DataSource = Nothing

                DataGridElPrezzi.DataSource = lstprezzi
                DataGridElPrezzi.DataBind()

            Else
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT *  FROM SISCOM_MI.APPALTI_EL_PREZZI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.DataGridElPrezzi.DataSource = dt
                Me.DataGridElPrezzi.DataBind()

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_ElencoPrezzi"
        End Try

    End Sub

    Protected Sub btn_Inserisci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci.Click
        Try

            If Me.idSelezionato.Value = -1 Then
                Salva()
                Me.divVisibility.Value = 0
                CaricaElPrezzi()
            Else
                Update()
                Me.divVisibility.Value = 0
                CaricaElPrezzi()
            End If
            Me.txtDescrizione.Text = ""

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_ElencoPrezzi"
        End Try


    End Sub
    Private Sub Salva()
        Try
            If CType(Me.Page, Object).vIdAppalti = 0 Then
                Dim gen As Mario.ElencoPrezzi

                gen = New Mario.ElencoPrezzi(lstprezzi.Count, "0", par.PulisciStrSql(Me.txtDescrizione.Text.Replace(Chr(13), "").Replace(Chr(10), "<br/>")))

                lstprezzi.Add(gen)

                gen = Nothing
            Else


                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_EL_PREZZI (ID,ID_APPALTO,DESCRIZIONE) " _
                                    & "VALUES (SISCOM_MI.SEQ_APPALTI_EL_PREZZI.NEXTVAL, " & CType(Me.Page, Object).vIdAppalti & ", '" _
                                    & par.PulisciStrSql(Me.txtDescrizione.Text.Replace(Chr(13), "").Replace(Chr(10), "<br/>")) & "')"

                par.cmd.ExecuteNonQuery()
            End If
            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_ElencoPrezzi"
        End Try
    End Sub
    Private Sub Update()
        Try
            If CType(Me.Page, Object).vIdAppalti = 0 Then

                Me.lstprezzi.Item(idSelezionato.Value).DESCRIZIONE = par.PulisciStrSql(Me.txtDescrizione.Text.Replace(Chr(13), "").Replace(Chr(10), "<br/>"))

            Else

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_EL_PREZZI SET DESCRIZIONE = '" & par.PulisciStrSql(Me.txtDescrizione.Text.Replace(Chr(13), "").Replace(Chr(10), "<br/>")) & "' WHERE ID =" & idSelezionato.Value
                par.cmd.ExecuteNonQuery()
                idSelezionato.Value = -1
                Response.Write("<script>alert('Modifica eseguita correttamente!');</script>")
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_ElencoPrezzi"
        End Try

    End Sub

    'Protected Sub DataGridElPrezzi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridElPrezzi.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ElencoPrezzi1_txtmia').value='Hai selezionato:" & e.Item.Cells(2).Text.Replace("'", "\'").Replace("<br/>", "") & "';document.getElementById('Tab_ElencoPrezzi1_idSelezionato').value='" & e.Item.Cells(0).Text & "'")
    '        'document.getElementById('Tab_ElencoPrezzi1_txtmia').value='Hai selezionato: " & e.Item.Cells(2).Text.Replace("'", "\'") & "';document.getElementById('Tab_ElencoPrezzi1_idSelezionato).value='" & e.Item.Cells(0).Text & "'"
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow';this.style.cursor='pointer'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_ElencoPrezzi1_txtmia').value='Hai selezionato:" & e.Item.Cells(2).Text.Replace("'", "\'").Replace("<br/>", "") & "';document.getElementById('Tab_ElencoPrezzi1_idSelezionato').value='" & e.Item.Cells(0).Text & "'")
    '    End If

    'End Sub

    Protected Sub btnApriAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriAppalti.Click
        Try
            If CType(Me.Page, Object).vIdAppalti > 0 Then

                If idSelezionato.Value > 0 Then
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_EL_PREZZI WHERE ID = " & idSelezionato.Value
                    Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lettore.Read Then
                        Me.txtDescrizione.Text = Lettore("DESCRIZIONE").ToString.Replace("<br/>", Chr(10))
                    End If
                    Lettore.Close()
                Else
                    Me.divVisibility.Value = 0

                    Response.Write("<script>alert('Selezionare un elemento dalla lista');</script>")
                    idSelezionato.Value = -1
                End If
            Else
                If idSelezionato.Value > -1 Then
                    Me.txtDescrizione.Text = Me.lstprezzi.Item(idSelezionato.Value).DESCRIZIONE.ToString.Replace("<br/>", Chr(10))
                Else
                    Me.divVisibility.Value = 0

                    Response.Write("<script>alert('Selezionare un elemento dalla lista');</script>")
                    idSelezionato.Value = -1

                End If


            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_ElencoPrezzi"
        End Try
        'Response.Write("<script>alert('Funzione non ancora disponibile');</script>")

    End Sub

    Protected Sub btnEliminaAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaAppalti.Click
        Try
            If confElimina.Value = 1 Then
                If CType(Me.Page, Object).vIdAppalti > 0 Then

                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_EL_PREZZI WHERE ID = " & idSelezionato.Value
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Operazione eseguita correttamente');</script>")
                Else
                    lstprezzi.RemoveAt(idSelezionato.Value)

                    Dim indice As Integer = 0
                    For Each griglia As Mario.ElencoPrezzi In lstprezzi
                        griglia.ID = indice
                        indice += 1
                    Next

                End If

            End If
            CaricaElPrezzi()
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_ElencoPrezzi"
        End Try

    End Sub
    Private Sub FrmSoloLettura()
        Me.btnEliminaAppalti.Visible = False
        btnApriAppalti.Visible = False
    End Sub



End Class
