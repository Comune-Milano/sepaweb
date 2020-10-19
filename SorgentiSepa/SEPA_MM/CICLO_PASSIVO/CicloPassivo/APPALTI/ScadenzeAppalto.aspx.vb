
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_ScadenzeAppalto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then
                Me.txtScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                Me.txtImporto.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                If Request.QueryString("STATO") = "ATTIVO" Then
                    Me.btnEliminaAppalti.Visible = False
                    Me.btnSave.Visible = False
                End If
                Session.Add("VoceSadVecchia", Request.QueryString("IDVOCE"))

                If Request.QueryString("IDAPPALTO") > 0 Then
                    CaricaDaDb()
                Else
                    CaricaDaDt()
                End If

                'If Session.Item("STAPPALTO") = "ATTIVO" Then
                '    imgAggiungiServ.Visible = False
                '    Me.btnEliminaAppalti.Visible = False
                'End If

            End If
        Catch ex As Exception
        End Try


    End Sub
    Private Sub CaricaDaDb()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT ID_APPALTO, TRIM(TO_CHAR(IMPORTO,'9G999G999G999G990D99')) as IMPORTO, TO_CHAR(TO_DATE(SISCOM_MI.APPALTI_SCADENZE.SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"" " _
                                & "FROM SISCOM_MI.APPALTI_SCADENZE WHERE ID_APPALTO = " & Request.QueryString("IDAPPALTO") & " AND ID_PF_VOCE_IMPORTO = " & Request.QueryString("IDVOCE")

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.dgvScadenze.DataSource = dt
            Me.dgvScadenze.DataBind()



        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub
    Private Sub CaricaDaDt()
        Try
            If Request.QueryString("IDVOCE") <> "" Then
                Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
                lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
                Dim dt As New Data.DataTable
                dt.Columns.Add("ID_APPALTO")
                dt.Columns.Add("SCADENZA")
                dt.Columns.Add("IMPORTO")
                dt.Columns.Add("ID_PF_VOCE_IMPORTO")
                Dim riga As Data.DataRow
                For Each r As Mario.ScadenzeManuali In lstscdenze
                    If r.ID_PF_VOCE_IMPORTO = Request.QueryString("IDVOCE") Then
                        riga = dt.NewRow()
                        riga.Item("ID_APPALTO") = r.ID_APPALTO
                        riga.Item("SCADENZA") = r.SCADENZA
                        riga.Item("IMPORTO") = r.IMPORTO
                        riga.Item("ID_PF_VOCE_IMPORTO") = r.ID_PF_VOCE_IMPORTO
                        dt.Rows.Add(riga)
                    End If
                Next
                dgvScadenze.DataSource = dt
                dgvScadenze.DataBind()

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click

        If Not String.IsNullOrEmpty(Me.txtScadenza.Text) AndAlso Not String.IsNullOrEmpty(Me.txtImporto.Text) Then
            'If par.FormattaData(par.IfEmpty(Me.txtScadenza.Text, 0)) < par.FormattaData(par.IfEmpty(Request.QueryString("RATAMIN"), 0)) Then
            '    Response.Write("<script>alert('Scadenza inferiore al periodo di durata dell\' appalto!Modificare la data');</script>")
            '    Exit Sub
            '    Me.VisibilitaDiv.Value = 1

            'End If
            'If par.FormattaData(par.IfEmpty(Me.txtScadenza.Text, 0)) > par.FormattaData(par.IfEmpty(Request.QueryString("RATAMAX"), 0)) Then
            '    Response.Write("<script>alert('Scadenza superiore al periodo di durata dell\' appalto!Modificare la data');</script>")
            '    Exit Sub
            '    Me.VisibilitaDiv.Value = 1

            'End If


            If Me.ScadSelected.Value > 0 Then
                Update()
            Else
                Salva()
            End If
            Me.VisibilitaDiv.Value = 0
        End If

    End Sub
    Private Function ControllaPrenotabile() As Boolean
        ControllaPrenotabile = False
        Dim prenotabile As Decimal = 0
        If Request.QueryString("IDAPPALTO") > 0 Then
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT SUM(IMPORTO) AS PRENOTATO FROM APPALTI_SCADENZE WHERE ID_APPALTO = " & Request.QueryString("IDAPPALTO") _
                                & " AND ID_PF_VOCE_IMPORTO = " & Request.QueryString("IDVOCE")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                prenotabile = CDec(par.IfEmpty(Request.QueryString("MAXVOCE"), 0)) - lettore("PRENOTATO")
            End If
            lettore.Close()


        Else
            Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
            lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
            For Each r As Mario.ScadenzeManuali In lstscdenze
                If r.ID_PF_VOCE_IMPORTO = Request.QueryString("IDVOCE") Then
                    prenotabile = prenotabile + r.IMPORTO
                End If
            Next
            prenotabile = CDec(par.IfEmpty(Request.QueryString("MAXVOCE"), 0)) - prenotabile

        End If

        If prenotabile <= 0 Then
            ControllaPrenotabile = False
        End If

    End Function
    Private Sub Salva()
        Try

            If Request.QueryString("IDAPPALTO") > 0 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_SCADENZE (ID_APPALTO,SCADENZA,IMPORTO,ID_PF_VOCE_IMPORTO) VALUES " _
                                    & "(" & Request.QueryString("IDAPPALTO") _
                                    & "," & par.AggiustaData(Me.txtScadenza.Text) _
                                    & " ," & par.VirgoleInPunti(par.IfEmpty(Me.txtImporto.Text.Replace(".", ""), 0)) & "," & Request.QueryString("IDVOCE") & ")"
                Try


                    par.cmd.ExecuteNonQuery()
                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    If EX1.Number = 1 Then
                        Response.Write("<script>alert('Scadenza già inserita!');</script>")
                        Exit Sub
                    End If

                End Try
                CaricaDaDb()
            Else

                Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
                lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
                For Each r As Mario.ScadenzeManuali In lstscdenze
                    If r.SCADENZA = Me.txtScadenza.Text And r.ID_PF_VOCE_IMPORTO = Request.QueryString("IDVOCE") Then
                        Response.Write("<script>alert('Scadenza già inserita!');</script>")
                        Me.txtScadenza.Text = ""
                        Me.txtImporto.Text = ""
                        Exit Sub
                    End If
                Next

                Dim gen As Mario.ScadenzeManuali
                gen = New Mario.ScadenzeManuali(0, Me.txtScadenza.Text, par.VirgoleInPunti(par.IfEmpty(Me.txtImporto.Text.Replace(".", ""), 0)), Request.QueryString("IDVOCE"))
                lstscdenze.Add(gen)
                gen = Nothing
                CaricaDaDt()
            End If

            Me.txtImporto.Text = ""
            Me.txtScadenza.Text = ""
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub Update()
        Try


            If Request.QueryString("IDAPPALTO") > 0 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_SCADENZE SET ID_APPALTO= " & Request.QueryString("IDAPPALTO") & ",SCADENZA=" & par.AggiustaData(Me.txtScadenza.Text) & ",IMPORTO= " & par.VirgoleInPunti(par.IfEmpty(Me.txtImporto.Text, 0)) & ") VALUES " _
                                    & "(" _
                                    & "," _
                                    & " ," & par.VirgoleInPunti(par.IfEmpty(Me.txtImporto.Text, 0)) & ")"
                Try


                    par.cmd.ExecuteNonQuery()
                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    If EX1.Number = 1 Then
                        Response.Write("<script>alert('Scadenza già inserita!');</script>")
                        Exit Sub
                    End If

                End Try
                CaricaDaDb()
            Else
                Dim r As Data.DataRow

                For Each r In CType(Session.Item("DTSCADENZE"), Data.DataTable).Rows
                    If r.Item("SCADENZA") = Me.txtScadenza.Text Then
                        Response.Write("<script>alert('Scadenza già inserita!');</script>")
                        Me.txtScadenza.Text = ""
                        Me.txtImporto.Text = ""
                    End If
                    Exit Sub
                Next

                r = CType(Session.Item("DTSCADENZE"), Data.DataTable).NewRow()
                r.Item("ID_APPALTO") = 0
                r.Item("SCADENZA") = Me.txtScadenza.Text
                r.Item("IMPORTO") = par.VirgoleInPunti(par.IfEmpty(Me.txtImporto.Text, 0))
                CType(Session.Item("DTSCADENZE"), Data.DataTable).Rows.Add(r)
                CaricaDaDt()
            End If

            Me.txtImporto.Text = ""
            Me.txtScadenza.Text = ""
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Protected Sub dgvScadenze_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvScadenze.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la scadenza: " & e.Item.Cells(1).Text & "';document.getElementById('ScadSelected').value=" & par.AggiustaData(e.Item.Cells(1).Text) & ";")
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la scadenza: " & e.Item.Cells(1).Text & "';document.getElementById('ScadSelected').value=" & par.AggiustaData(e.Item.Cells(1).Text) & ";")
        End If

    End Sub

    Protected Sub btnApriAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriAppalti.Click
        Try
            If Me.ScadSelected.Value > 0 Then
                If Request.QueryString("IDAPPALTO") > 0 Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_SCADENZE WHERE SCADENZA = '" & ScadSelected.Value _
                                        & "' AND ID_APPALTO = " & Request.QueryString("IDAPPALTO") & ""

                    Dim L As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If L.Read Then
                        Me.txtScadenza.Text = par.FormattaData(L("SCADENZA"))
                        Me.txtImporto.Text = L("IMPORTO")
                    End If
                    L.Close()
                Else
                    Dim dt As Data.DataTable = CType(Session.Item("DTSCADENZE"), Data.DataTable)
                    Dim found() As Data.DataRow
                    found = dt.Select("SCADENZA = '" & par.FormattaData(ScadSelected.Value) & "'")

                    Me.txtScadenza.Text = found(0).Item("SCADENZA")
                    Me.txtImporto.Text = found(0).Item("IMPORTO")
                End If
                Me.VisibilitaDiv.Value = 1


            Else
                Response.Write("<script>alert('Selezionare una riga dalla lista per modificare i dati!');</script>")

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnEliminaAppalti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaAppalti.Click
        Try
            If ConfElimina.Value > 0 Then
                If Request.QueryString("IDAPPALTO") > 0 Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Request.QueryString("IDCONNECTION")), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans


                    par.cmd.CommandText = "DELETE  FROM SISCOM_MI.APPALTI_SCADENZE WHERE ID_APPALTO = " & Request.QueryString("IDAPPALTO") _
                                        & " AND SCADENZA =  " & Me.ScadSelected.Value & " AND ID_PF_VOCE_IMPORTO = " & Request.QueryString("IDVOCE")
                    par.cmd.ExecuteNonQuery()
                    CaricaDaDb()

                Else
                    Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
                    lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))


                    Dim listacancellare As New System.Collections.Generic.List(Of Mario.ScadenzeManuali)

                    For Each r As Mario.ScadenzeManuali In lstscdenze
                        If r.ID_PF_VOCE_IMPORTO = Request.QueryString("IDVOCE") And r.SCADENZA = par.FormattaData(Me.ScadSelected.Value) Then
                            listacancellare.Add(r)
                        End If
                    Next
                    For i As Integer = 0 To listacancellare.Count - 1
                        lstscdenze.Remove(listacancellare(i))
                    Next

                    CaricaDaDt()
                End If
            End If
            Me.VisibilitaDiv.Value = 0
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
