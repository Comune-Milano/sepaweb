
Partial Class Condomini_ConsGestione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            Try
                'If Session.Item("OPERATORE") = "" Then
                '    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                'End If
                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If

                Me.cmbTipoGest.Items.Add(New ListItem("ORDINARIO", "O"))
                Me.cmbTipoGest.Items.Add(New ListItem("STRAORDINARIO", "S"))

                'Me.cmbNumRate.Items.Add(New ListItem("1", "1"))
                'Me.cmbNumRate.Items.Add(New ListItem("2", "2"))
                'Me.cmbNumRate.Items.Add(New ListItem("3", "3"))
                'Me.cmbNumRate.Items.Add(New ListItem("4", "4"))
                'Me.cmbNumRate.Items.Add(New ListItem("5", "5"))
                'Me.cmbNumRate.Items.Add(New ListItem("6", "6"))
                'Me.cmbNumRate.SelectedValue = 4

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                If Request.QueryString("IDGEST") <> "" Then

                    par.cmd.CommandText = "SELECT TIPO, DATA_INIZIO, DATA_FINE,N_RATE, DENOMINAZIONE,COMUNI_NAZIONI.nome AS comune  FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_GESTIONE, sepa.COMUNI_NAZIONI WHERE condomini.cod_comune = COMUNI_NAZIONI.cod(+) and ID_CONDOMINIO = CONDOMINI.ID AND COND_GESTIONE.ID = " & Request.QueryString("IDGEST")
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        Me.lblTitolo.Text = "Consuntivo Condominio: " & myReader1("DENOMINAZIONE") & " - " & myReader1("comune")
                        Me.txtInizioGest.Text = (par.FormattaData(myReader1("DATA_INIZIO").ToString)).Substring(0, 5)
                        Me.TxtFineGest.Text = (par.FormattaData(myReader1("DATA_FINE").ToString)).Substring(0, 5)
                        Me.txtAnnoInizio.Text = myReader1("DATA_INIZIO").ToString.Substring(0, 4)
                        Me.TxtAnnoFine.Text = myReader1("DATA_FINE").ToString.Substring(0, 4)
                        'Me.cmbNumRate.SelectedValue = myReader1("N_RATE").ToString
                        Me.cmbTipoGest.SelectedValue = myReader1("TIPO").ToString

                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI_CONSUNTIVI WHERE ID_GESTIONE = " & Request.QueryString("IDGEST")
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        Domanda.Value = 1
                    End If
                    myReader1.Close()




                    'CREO SUL DATABASE UN PALETTO PER IL ROLBACK FINO A QUI
                    par.cmd.CommandText = "SAVEPOINT CONSUNTIVO"
                    par.cmd.ExecuteNonQuery()

                    Cerca()
                    AddJavascriptFunction()

                    'If Domanda.Value = 0 Then
                    '    Me.btnSituazPatr.Visible = False
                    'End If
                End If
                'If Request.QueryString("MODIFICATO") = 1 Then
                '    Session.Item("MODIFYMODAL") = 1
                'End If

                If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) Then
                    SettaFrmReadOnly()
                End If
            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try

        End If
        Me.DataGridVociSpesa.Width = 845
        Me.DataGridVociSpMor.Width = 845


    End Sub
    Public Property vIdConnModale() As String
        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property
    Private Sub Cerca()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT MAX(ID) AS ID_PIANO_F FROM SISCOM_MI.PF_MAIN WHERE ID_STATO = 5"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                idPianoF.value = par.IfNull(myReader1("ID_PIANO_F"), 0)
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT COND_VOCI_SPESA.FL_TOTALE, COND_VOCI_SPESA.ID AS IDVOCE,COND_VOCI_SPESA.DESCRIZIONE,'' AS CONSUNTIVO,'' AS PREVENTIVO,''AS CONGUAGLIO " _
                                & "FROM SISCOM_MI.COND_VOCI_SPESA,siscom_mi.cond_voci_spesa_pf " _
                                & "WHERE cond_voci_spesa.ID = cond_voci_spesa_pf.id_voce_cond " _
                                & "AND cond_voci_spesa_pf.id_piano_finanziario = " & idPianoF.Value & " AND COND_VOCI_SPESA.FL_TOTALE = 1 ORDER BY idvoce ASC"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()


            da.Fill(dt)
            For Each row As Data.DataRow In dt.Rows
                par.cmd.CommandText = "SELECT PREVENTIVO, CONSUNTIVO,CONGUAGLIO FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    row.Item("CONSUNTIVO") = IsNumFormat(myReader1("CONSUNTIVO"), 0, "##,##0.00")
                    row.Item("PREVENTIVO") = IsNumFormat(myReader1("PREVENTIVO"), 0, "##,##0.00")
                    row.Item("CONGUAGLIO") = IsNumFormat(myReader1("CONGUAGLIO"), 0, "##,##0.00")
                Else
                    row.Item("CONSUNTIVO") = IsNumFormat("0,00", 0, "##,##0.00")
                    row.Item("PREVENTIVO") = IsNumFormat("0,00", 0, "##,##0.00")
                    row.Item("CONGUAGLIO") = IsNumFormat("0,00", 0, "##,##0.00")
                End If
                myReader1.Close()
            Next

            DataGridVociSpesa.DataSource = dt
            DataGridVociSpesa.DataBind()


            par.cmd.CommandText = "SELECT COND_VOCI_SPESA.FL_TOTALE, COND_VOCI_SPESA.ID AS IDVOCE,COND_VOCI_SPESA.DESCRIZIONE,'' AS CONSUNTIVO,'' AS PREVENTIVO,''AS CONGUAGLIO " _
                                & "FROM SISCOM_MI.COND_VOCI_SPESA,siscom_mi.cond_voci_spesa_pf " _
                                & "WHERE cond_voci_spesa.ID = cond_voci_spesa_pf.id_voce_cond " _
                                & "AND cond_voci_spesa_pf.id_piano_finanziario = " & idPianoF.Value & " AND COND_VOCI_SPESA.FL_TOTALE = 0 ORDER BY idvoce ASC"



            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable()


            da2.Fill(dt2)
            For Each row As Data.DataRow In dt2.Rows
                par.cmd.CommandText = "SELECT PREVENTIVO, CONSUNTIVO,CONGUAGLIO FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    row.Item("CONSUNTIVO") = IsNumFormat(myReader1("CONSUNTIVO"), 0, "##,##0.00")
                    row.Item("PREVENTIVO") = IsNumFormat(myReader1("PREVENTIVO"), 0, "##,##0.00")
                    row.Item("CONGUAGLIO") = IsNumFormat(myReader1("CONGUAGLIO"), 0, "##,##0.00")
                Else
                    row.Item("CONSUNTIVO") = IsNumFormat("0,00", 0, "##,##0.00")
                    row.Item("PREVENTIVO") = IsNumFormat("0,00", 0, "##,##0.00")
                    row.Item("CONGUAGLIO") = IsNumFormat("0,00", 0, "##,##0.00")
                End If
                myReader1.Close()
            Next

            DataGridVociSpMor.DataSource = dt2
            DataGridVociSpMor.DataBind()


            'Dim IdVoceMorosita As String = ""
            'par.cmd.CommandText = "select id from siscom_mi.cond_voci_spesa , siscom_mi.cond_voci_spesa_pf where fl_totale = 0 and cond_voci_spesa.id = cond_voci_spesa_pf.id_voce_cond and id_piano_finanziario = " & idPianoF.Value
            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then
            '    IdVoceMorosita = par.IfNull(myReader1("ID"), 0)
            'End If
            'myReader1.Close()


            'par.cmd.CommandText = "SELECT PREVENTIVO, CONSUNTIVO,CONGUAGLIO FROM SISCOM_MI.COND_GESTIONE_DETT WHERE ID_VOCE = " & IdVoceMorosita & " AND ID_GESTIONE = " & Request.QueryString("IDGEST")
            'myReader1 = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            '    Me.txtMorConsuntivo.Text = IsNumFormat(myReader1("CONSUNTIVO"), 0, "##,##0.00")
            '    Me.txtMorPreventivo.Text = IsNumFormat(myReader1("PREVENTIVO"), 0, "##,##0.00")
            '    Me.txtMorConguaglio.Text = IsNumFormat(myReader1("CONGUAGLIO"), 0, "##,##0.00")
            'End If
            'myReader1.Close()
            SommaColonne()
            SommaRighe()
            If Me.txtAnnoInizio.Text <> Me.TxtAnnoFine.Text Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_GESTIONE WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & " AND SUBSTR(DATA_INIZIO,0,4) = " & TxtAnnoFine.Text
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_GESTIONE WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & " AND SUBSTR(DATA_INIZIO,0,4) = " & TxtAnnoFine.Text + 1
            End If

            myReader1 = par.cmd.ExecuteReader
            If myReader1.HasRows Then
                SettaFrmReadOnly()
                Response.Write("<script>alert('Impostazione finestra in sola lettura per esistenza preventivo successivo a questo consuntivo!');</script>")

            End If
            myReader1.Close()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        Try

            If Request.QueryString("IDGEST") <> "" Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                Dim SalvaSitPatr As Boolean = False

                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim idVoce As String = ""
                Dim Preventivo As String = ""
                Dim Consuntivo As String = ""
                Dim Conguaglio As String = ""
                For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                    di = Me.DataGridVociSpesa.Items(i)
                    idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                    Consuntivo = CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Text
                    Conguaglio = CType(di.Cells(3).FindControl("txtConguaglio"), TextBox).Text
                    Preventivo = CType(di.Cells(3).FindControl("txtPreventivo"), TextBox).Text
                    If Preventivo <> "" Then
                        SalvaSitPatr = True
                    End If
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT SET CONSUNTIVO=" & par.VirgoleInPunti(par.IfEmpty(Consuntivo.Replace(".", ""), "Null")) & ",CONGUAGLIO=" & par.VirgoleInPunti(par.IfEmpty(Conguaglio.Replace(".", ""), "Null")) & " WHERE ID_GESTIONE=" & Request.QueryString("IDGEST") & " AND ID_VOCE =" & idVoce
                    par.cmd.ExecuteNonQuery()
                Next


                For i = 0 To Me.DataGridVociSpMor.Items.Count - 1
                    di = Me.DataGridVociSpMor.Items(i)
                    idVoce = Me.DataGridVociSpMor.Items(i).Cells(0).Text
                    Consuntivo = CType(di.Cells(3).FindControl("txtConsuntivoMor"), TextBox).Text
                    Conguaglio = CType(di.Cells(3).FindControl("txtConguaglioMor"), TextBox).Text
                    Preventivo = CType(di.Cells(3).FindControl("txtPreventivoMor"), TextBox).Text
                    If Preventivo <> "" Then
                        SalvaSitPatr = True
                    End If
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT SET CONSUNTIVO=" & par.VirgoleInPunti(par.IfEmpty(Consuntivo.Replace(".", ""), "Null")) & ",CONGUAGLIO=" & par.VirgoleInPunti(par.IfEmpty(Conguaglio.Replace(".", ""), "Null")) & " WHERE ID_GESTIONE=" & Request.QueryString("IDGEST") & " AND ID_VOCE =" & idVoce
                    par.cmd.ExecuteNonQuery()
                Next



                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CONSUNTIVO CONTABILITA CONDOMINIO')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE SET STATO_BILANCIO = 'C' WHERE ID=" & Request.QueryString("IDGEST")
                par.cmd.ExecuteNonQuery()


                '******************INSERIMENTO IN COND_UI_GESTIONE DI TUTTO QUELLO CHE è PRESENTE IN COND_UI PER IL CONDOMINIO
                If Domanda.Value = 1 Then
                    If AggSitPat.Value = 1 Then
                        If SalvaSitPatr = True Then
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_CONSUNTIVI WHERE ID_GESTIONE = " & Request.QueryString("IDGEST")
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                            Dim dt As New Data.DataTable
                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            da.Fill(dt)
                            If dt.Rows.Count > 0 Then
                                For Each row As System.Data.DataRow In dt.Rows
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI_CONSUNTIVI(ID_GESTIONE, ID_UI, POSIZIONE_BILANCIO," _
                                    & " MIL_PRO, MIL_ASC, MIL_COMPRO, MIL_GEST,MIL_RISC,NOTE,ADDEBITO_SINGOLO,ID_INTESTARIO)" _
                                    & " VALUES (" & Request.QueryString("IDGEST") & ", " & row.Item("ID_UI") & ", '" & par.IfNull(row.Item("POSIZIONE_BILANCIO"), "") & "'," _
                                    & " " & par.VirgoleInPunti(row.Item("MIL_PRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_ASC")) & ", " & par.VirgoleInPunti(row.Item("MIL_COMPRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_GEST")) & ", " & par.VirgoleInPunti(row.Item("MIL_RISC")) & ", '" & par.PulisciStrSql(par.IfNull(row.Item("NOTE"), "")) & "', " _
                                    & " " & par.VirgoleInPunti(row.Item("ADDEBITO_SINGOLO")) & ", " & par.IfNull(row.Item("ID_INTESTARIO"), "Null") & ")"
                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If
                        End If
                        AggSitPat.Value = 0
                    End If
                Else
                    If SalvaSitPatr = True Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_CONSUNTIVI WHERE ID_GESTIONE = " & Request.QueryString("IDGEST")
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                        Dim dt As New Data.DataTable
                        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            For Each row As System.Data.DataRow In dt.Rows
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI_CONSUNTIVI(ID_GESTIONE, ID_UI, POSIZIONE_BILANCIO," _
                                & " MIL_PRO, MIL_ASC, MIL_COMPRO, MIL_GEST,MIL_RISC,NOTE,ADDEBITO_SINGOLO,ID_INTESTARIO)" _
                                & " VALUES (" & Request.QueryString("IDGEST") & ", " & row.Item("ID_UI") & ", '" & par.IfNull(row.Item("POSIZIONE_BILANCIO"), "") & "'," _
                                & " " & par.VirgoleInPunti(row.Item("MIL_PRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_ASC")) & ", " & par.VirgoleInPunti(row.Item("MIL_COMPRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_GEST")) & ", " & par.VirgoleInPunti(row.Item("MIL_RISC")) & ", '" & par.PulisciStrSql(par.IfNull(row.Item("NOTE"), "")) & "', " _
                                & " " & par.VirgoleInPunti(row.Item("ADDEBITO_SINGOLO")) & ", " & par.IfNull(row.Item("ID_INTESTARIO"), "Null") & ")"
                                par.cmd.ExecuteNonQuery()
                            Next
                        End If
                    End If


                End If
                '******************FINE INSERIMENTO COND_UI_GESTIONE
                Session.Item("MODIFYMODAL") = 1
                Me.txtModificato.Value = 0
                Me.txtSalvato.Value = 1
                Response.Write("<script>alert('Operazone eseguita correttamente!');</script>")
                'Response.Write("<script>window.close();</script>")
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try

    End Sub

    Protected Sub btnConguaglio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConguaglio.Click
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            Dim Conguaglio As String = ""

            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                If Me.DataGridVociSpesa.Items(i).Cells(4).Text <> "&nbsp;" Then
                    Conguaglio = par.IfEmpty(CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Text, 0) - par.IfEmpty(Me.DataGridVociSpesa.Items(i).Cells(4).Text, 0)
                    CType(di.Cells(3).FindControl("txtConguaglio"), TextBox).Text = Conguaglio
                Else
                    CType(di.Cells(3).FindControl("txtConguaglio"), TextBox).Text = CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Text
                End If

            Next
            SommaColonne()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub SommaColonne()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem

            Me.totConsuntivo.Text = 0
            Me.totPreventivo.Text = 0
            Me.totConguaglio.Text = 0

            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                If di.Cells(8).Text = 1 Then

                    Me.totConguaglio.Text = Format(CDbl(totConguaglio.Text) + CDbl(par.IfEmpty(CType(di.Cells(1).FindControl("txtConguaglio"), TextBox).Text, 0)), "##,##0.00")
                    Me.totPreventivo.Text = Format(CDbl(Me.totPreventivo.Text) + CDbl(Replace(di.Cells(4).Text, "&nbsp;", 0)), "##,##0.00")
                    Me.totConsuntivo.Text = Format(CDbl(Me.totConsuntivo.Text) + CDbl(par.IfEmpty(CType(di.Cells(1).FindControl("txtConsuntivo"), TextBox).Text, 0)), "##,##0.00")
                End If
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub SommaRighe()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                CType(di.Cells(1).FindControl("txtConguaglio"), TextBox).Text = Format(CDbl(par.IfEmpty(CType(di.Cells(1).FindControl("txtConsuntivo"), TextBox).Text, 0)) - CDbl(Replace(di.Cells(4).Text, "&nbsp;", 0)), "##,##0.00")
            Next

            For i = 0 To Me.DataGridVociSpMor.Items.Count - 1
                di = Me.DataGridVociSpMor.Items(i)
                CType(di.Cells(1).FindControl("txtConguaglioMor"), TextBox).Text = Format(CDbl(par.IfEmpty(CType(di.Cells(1).FindControl("txtConsuntivoMor"), TextBox).Text, 0)) - CDbl(Replace(di.Cells(4).Text, "&nbsp;", 0)), "##,##0.00")
            Next


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub AddJavascriptFunction()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');Differenza(this);javascript:AutoDecimal2(this);")
                'CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:delPointer(this);")

                'CType(di.Cells(3).FindControl("txtPreventivo"), TextBox).Attributes.Add("onkeyup", "javascript:")
                CType(di.Cells(3).FindControl("txtConguaglio"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');")

            Next

            For i = 0 To Me.DataGridVociSpMor.Items.Count - 1
                di = Me.DataGridVociSpMor.Items(i)
                CType(di.Cells(3).FindControl("txtConsuntivoMor"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');Differenza(this);javascript:AutoDecimal2(this);")
                'CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:delPointer(this);")

                'CType(di.Cells(3).FindControl("txtPreventivo"), TextBox).Attributes.Add("onkeyup", "javascript:")
                CType(di.Cells(3).FindControl("txtConguaglioMor"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');")

            Next


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub SettaFrmReadOnly()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem

            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                CType(di.Cells(3).FindControl("txtConsuntivo"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtPreventivo"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtConguaglio"), TextBox).Enabled = False

            Next

            Me.txtInizioGest.ReadOnly = True
            Me.TxtFineGest.ReadOnly = True
            Me.txtAnnoInizio.ReadOnly = True
            Me.TxtAnnoFine.ReadOnly = True
            'Me.cmbNumRate.Enabled = False
            Me.cmbTipoGest.Enabled = False
            Me.btnSalvaCambioAmm.Visible = False
            For i = 0 To Me.DataGridVociSpMor.Items.Count - 1
                di = Me.DataGridVociSpMor.Items(i)
                CType(di.Cells(3).FindControl("txtConsuntivoMor"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtPreventivoMor"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtConguaglioMor"), TextBox).Enabled = False

            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSituazPatr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSituazPatr.Click

        Response.Write("<script>window.open('RptSituazPat.aspx?IDGESTIONE= " & Request.QueryString("IDGEST") & "&CHIAMA=CONSU&IDCONDOMINIO=" & Request.QueryString("IDCONDOMINIO") & "&IDCON=" & vIdConnModale & "','RptInquilini','');</script>")
        'Response.Write("<script>window.open('RptSituazPat.aspx?IDGESTIONE= " & vIdGestione & "&CHIAMA=PREV&IDCONDOMINIO=" & Request.QueryString("IDCONDOMINIO") & "&IDCON=" & vIdConnModale & "','RptInquilini','');</script>")

    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = Format(CDbl(S), Precision)
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Protected Sub btnLiberiAbusivi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLiberiAbusivi.Click
        Response.Write("<script>window.showModalDialog('LiberiAbusivi.aspx?IDCONDOMINIO= " & Request.QueryString("IDCONDOMINIO") & "&IDCON=" & vIdConnModale & "&IDGEST=" & Request.QueryString("IDGEST") & "&IDVISUAL=" & Request.QueryString("IDVISUAL") & "&CHIAMA=C',window, 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');</script>")
        Session("MODIFYMODAL") = 1

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try

            If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) Then
                Response.Write("<script>window.close();</script>")
            Else

                If txtesci.Value = 1 Then
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                    'Session.Item("MODIFYMODAL") = 1
                    If Me.txtSalvato.Value = 0 Then
                        par.cmd.CommandText = "ROLLBACK TO SAVEPOINT CONSUNTIVO"
                        par.cmd.ExecuteNonQuery()
                        Response.Write("<script>window.close();</script>")
                    Else
                        Response.Write("<script>window.close();window.returnValue = '1';</script>")
                        Session.Item("MODIFYMODAL") = 1
                    End If

                Else

                End If

                End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


End Class
