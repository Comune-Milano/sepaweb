
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_VariazConfig
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            HFGriglia.Value = DataGridComposizione.ClientID
            lIdConnessione = Request.QueryString("IDCON")

            CaricaLista()
            If Request.QueryString("IdAppalto") > 0 Then
                SelAssociati()
            End If
        End If

    End Sub
    Private Sub CaricaLista()
        Try

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            If Request.QueryString("TIPO") = "E" Then
                'par.cmd.CommandText = "SELECT EDIFICI.ID, DENOMINAZIONE FROM SISCOM_MI.EDIFICI, SISCOM_MI.LOTTI_PATRIMONIO WHERE EDIFICI.ID = LOTTI_PATRIMONIO.ID_EDIFICIO AND LOTTI_PATRIMONIO.ID_LOTTO =" & Request.QueryString("IDLOTTO") & " order by denominazione asc"
                par.cmd.CommandText = "SELECT EDIFICI.ID, DENOMINAZIONE,(CASE WHEN EDIFICI.ID IN (SELECT ID_EDIFICIO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & Request.QueryString("IdAppalto") & " ) THEN 1 ELSE 0 END) AS CHECKED,(SELECT complessi_immobiliari.ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE complessi_immobiliari.id=EDIFICI.ID_COMPLESSO) AS ID_FILIALE FROM SISCOM_MI.EDIFICI, SISCOM_MI.LOTTI_PATRIMONIO WHERE EDIFICI.ID = LOTTI_PATRIMONIO.ID_EDIFICIO AND LOTTI_PATRIMONIO.ID_LOTTO =" & Request.QueryString("IDLOTTO") _
                    & " ORDER BY DENOMINAZIONE ASC"

            Else
                par.cmd.CommandText = "SELECT IMPIANTI.ID, (TIPOLOGIA_IMPIANTI.DESCRIZIONE|| ' - - ' || IMPIANTI.DESCRIZIONE ) AS DENOMINAZIONE " _
                                    & "FROM SISCOM_MI.IMPIANTI, SISCOM_MI.LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE IMPIANTI.ID = LOTTI_PATRIMONIO.ID_IMPIANTO " _
                                    & "AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA AND LOTTI_PATRIMONIO.ID_LOTTO =" & Request.QueryString("IDLOTTO")
                Me.DataGridComposizione.Columns(2).HeaderText = "IMPIANTO"
            End If


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds As New Data.DataTable
            da.Fill(ds)


            DataGridComposizione.DataSource = ds
            DataGridComposizione.DataBind()
            Session.Add("dtCompVar", ds)

            ds.Dispose()

        Catch ex As Exception
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try




    End Sub
    Private Sub SelAssociati()
        Try

            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            If Request.QueryString("TIPO") = "E" Then
                par.cmd.CommandText = "SELECT ID_EDIFICIO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & Request.QueryString("IdAppalto")
            Else
                par.cmd.CommandText = "SELECT ID_IMPIANTO AS ID FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & Request.QueryString("IdAppalto")
            End If

            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While Lettore.Read
                Dim i As Integer = 0
                Dim di As GridDataItem

                For i = 0 To Me.DataGridComposizione.Items.Count - 1
                    di = Me.DataGridComposizione.Items(i)
                    If di.Cells(2).Text = par.IfNull(Lettore("ID"), 0) Then
                        DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True
                        Selezionati.Value = 1
                    End If
                Next

            End While


        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabComposizione"
        End Try


    End Sub
    Public Sub SoloLettura()
        If DataGridComposizione.Items.Count > 0 Then

            Dim i As Integer = 0
            Dim di As GridDataItem

            'Me.btnSeleziona.Visible = False
            For i = 0 To Me.DataGridComposizione.Items.Count - 1
                di = Me.DataGridComposizione.Items(i)
                DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Enabled = False
                Selezionati.Value = 1
            Next
        End If
        CheckBoxA.Enabled = False
        CheckBoxB.Enabled = False
        CheckBoxC.Enabled = False
        CheckBoxD.Enabled = False
        CheckBoxU.Enabled = False

    End Sub

    Protected Sub Selezionati_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Selezionati.ValueChanged
        If DataGridComposizione.Items.Count > 0 Then

            Dim i As Integer = 0
            Dim di As GridDataItem
            If Selezionati.Value = 0 Then
                For i = 0 To Me.DataGridComposizione.Items.Count - 1
                    di = Me.DataGridComposizione.Items(i)
                    DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True
                    Selezionati.Value = 1
                Next
            Else
                For i = 0 To Me.DataGridComposizione.Items.Count - 1
                    di = Me.DataGridComposizione.Items(i)
                    DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = False
                    Selezionati.Value = 0
                Next
            End If

        End If

    End Sub

    'Protected Sub btnSeleziona_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSeleziona.Click
    '    If DataGridComposizione.Items.Count > 0 Then

    '        Dim i As Integer = 0
    '        Dim di As DataGridItem
    '        If Selezionati.Value = 0 Then
    '            For i = 0 To Me.DataGridComposizione.Items.Count - 1
    '                di = Me.DataGridComposizione.Items(i)
    '                DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True
    '                Selezionati.Value = 1
    '            Next
    '        Else
    '            For i = 0 To Me.DataGridComposizione.Items.Count - 1
    '                di = Me.DataGridComposizione.Items(i)
    '                DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = False
    '                Selezionati.Value = 0
    '            Next
    '        End If

    '    End If

    'End Sub

    Protected Sub btnSeleziona0_Click(sender As Object, e As System.EventArgs) Handles btnSeleziona0.Click
        Salva()
    End Sub
    Private Sub Salva()
        Try


            If Request.QueryString("IdAppalto") > 0 Then

                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                'CANCELLO LA LISTA DEGLI EDIFICI/IMPIANTI LEGATI ALL'APPALTO
                'par.cmd.CommandText = "delete FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & Request.QueryString("IdAppalto")
                'par.cmd.ExecuteNonQuery()
                Dim continua As Boolean = False
                Dim i As Integer = 0
                Dim di As GridDataItem
                For i = 0 To DataGridComposizione.Items.Count - 1
                    di = DataGridComposizione.Items(i)
                    If DirectCast(di.Cells(3).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
                        continua = True
                    End If
                Next
                Dim ris As Integer = 0
                If continua = True Then
                    par.cmd.CommandText = "SELECT ID FROM APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM APPALTI WHERE ID = " & Request.QueryString("IdAppalto") & ")"
                    Dim dtAppalti As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                    For i = 0 To DataGridComposizione.Items.Count - 1
                        di = DataGridComposizione.Items(i)
                        If DirectCast(di.Cells(3).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
                            If Request.QueryString("TIPO") = "E" Then
                                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE " _
                                    & " ID_APPALTO= " & Request.QueryString("IdAppalto") & " AND ID_EDIFICIO=" & di.Cells(2).Text
                                ris = par.cmd.ExecuteScalar
                                If ris = 0 Then
                                    For Each riga As Data.DataRow In dtAppalti.Rows
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_EDIFICIO,D_INIZIO) VALUES " _
                                                    & "(" & riga.Item("ID") & "," & di.Cells(2).Text & "," & Format(Now, "yyyyMMdd") & ")"
                                par.cmd.ExecuteNonQuery()
                                    Next
                                End If
                            Else
                                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE " _
                                    & " ID_APPALTO= " & Request.QueryString("IdAppalto") & " AND ID_IMPIANTO=" & di.Cells(2).Text
                                ris = par.cmd.ExecuteScalar
                                If ris = 0 Then
                                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_IMPIANTO,D_INIZIO) VALUES " _
                                    '                    & "(" & Request.QueryString("IdAppalto") & "," & di.Cells(2).Text & "," & Format(Now, "yyyyMMdd") & ")"
                                    'par.cmd.ExecuteNonQuery()
                                    For Each riga As Data.DataRow In dtAppalti.Rows
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_EDIFICIO,D_INIZIO) VALUES " _
                                                    & "(" & riga.Item("ID") & "," & di.Cells(2).Text & "," & Format(Now, "yyyyMMdd") & ")"
                                par.cmd.ExecuteNonQuery()
                                    Next

                            End If

                        End If
                        Else
                            'par.cmd.CommandText = "update SISCOM_MI.APPALTI_LOTTI_PATRIMONIO set d_fine='" & Format(Now, "yyyyMMdd") & "' WHERE ID_APPALTO = " & Request.QueryString("IdAppalto") _
                            '    & " and id_Edificio=" & di.Cells(2).Text
                            'par.cmd.ExecuteNonQuery()
                            For Each riga As Data.DataRow In dtAppalti.Rows
                                par.cmd.CommandText = "update SISCOM_MI.APPALTI_LOTTI_PATRIMONIO set d_fine='" & Format(Now, "yyyyMMdd") & "' " _
                                                    & " WHERE ID_APPALTO = " & riga.Item("ID") _
                                                    & " And id_Edificio=" & di.Cells(2).Text
                                par.cmd.ExecuteNonQuery()
                            Next
                        End If
                    Next
                    For Each riga As Data.DataRow In dtAppalti.Rows

                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES (" & riga.Item("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F02','VARIAZIONE CONSISTENZA')"
                    par.cmd.ExecuteNonQuery()
                    Next

                    'Response.Write("<script>window.close();</script>")
                    Session.Add("MODIFICATO", 1)
                    RadWindowManager1.RadAlert("Modifica completata! ", 300, 150, "Attenzione", "CloseAndRefresh", Nothing)
                Else
                    RadWindowManager1.RadAlert("Selezionare almeno un elemento! ", 300, 150, "Attenzione", "", Nothing)
                End If

            Else

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try


    End Sub

    Protected Sub CheckBoxA_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxA.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtCompVar")
                If CheckBoxA.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 101 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 101 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtCompVar") = dt
                DataGridComposizione.DataSource = Session.Item("dtCompVar")
                DataGridComposizione.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxB_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxB.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtCompVar")
                If CheckBoxB.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 102 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 102 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtCompVar") = dt
                DataGridComposizione.DataSource = Session.Item("dtCompVar")
                DataGridComposizione.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxC_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxC.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtCompVar")
                If CheckBoxC.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 103 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 103 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtCompVar") = dt
                DataGridComposizione.DataSource = Session.Item("dtCompVar")
                DataGridComposizione.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxD_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxD.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtCompVar")
                If CheckBoxD.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 104 Then
                            row.Item("CHECKED") = 1
                        End If
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("ID_FILIALE") = 104 Then
                            row.Item("CHECKED") = 0
                        End If
                    Next
                End If
                Session.Item("dtCompVar") = dt
                DataGridComposizione.DataSource = Session.Item("dtCompVar")
                DataGridComposizione.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckBoxU_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxU.CheckedChanged
        Try
            If DataGridComposizione.Items.Count > 0 Then
                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtCompVar")
                If CheckBoxU.Checked = True Then
                    For Each row As Data.DataRow In dt.Rows
                        row.Item("CHECKED") = 1
                    Next
                Else
                    For Each row As Data.DataRow In dt.Rows
                        row.Item("CHECKED") = 0
                    Next
                End If
                Session.Item("dtCompVar") = dt
                DataGridComposizione.DataSource = Session.Item("dtCompVar")
                DataGridComposizione.DataBind()
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_APPALTI_VariazConfig_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub



    Private Sub DataGridComposizione_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles DataGridComposizione.PageIndexChanged
        CaricaLista()
        DataGridComposizione.CurrentPageIndex = e.NewPageIndex
    End Sub
End Class
