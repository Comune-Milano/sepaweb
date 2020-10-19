
Partial Class Condomini_RiepGestione
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim PrenDaCancellare As String = ""
    Dim txt As TextBox
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
            Try
                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If

                If Request.QueryString("IDGEST") <> "" Then
                    vIdGestione = Request.QueryString("IDGEST")
                End If

                If Request.QueryString("IDCONDOMINIO") <> "" Then
                    idCondominio.Value = Request.QueryString("IDCONDOMINIO")
                End If

                If Request.QueryString("IDVISUAL") <> "" Then
                    idVisual.Value = Request.QueryString("IDVISUAL")
                End If
                Me.Session.Add("MODIFYMODAL", 0)

                txtAnnoInizio.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")
                TxtAnnoFine.Attributes.Add("onkeyup", "javascript:valid(this,'notnumbers');")

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                If Request.QueryString("IDCONDOMINIO") <> "" Then
                    par.cmd.CommandText = "SELECT CONDOMINI.TIPO_GESTIONE, GESTIONE_INIZIO, GESTIONE_FINE, DENOMINAZIONE,COMUNI_NAZIONI.nome AS comune FROM SISCOM_MI.CONDOMINI, sepa.COMUNI_NAZIONI WHERE condomini.cod_comune = COMUNI_NAZIONI.cod(+) and condomini.ID = " & Request.QueryString("IDCONDOMINIO")
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        Me.lblTitolo.Text = "Preventivo Condominio: " & myReader1("DENOMINAZIONE") & " - " & myReader1("comune")
                        Me.txtInizioGest.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_INIZIO").ToString), "/2000", "")
                        Me.TxtFineGest.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_FINE").ToString), "/2000", "")
                        If par.IfNull(myReader1(0), "D") = "I" Then
                            Me.btnElInquilini.Visible = True
                        Else
                            Me.btnElInquilini.Visible = False

                        End If
                    End If
                    myReader1.Close()


                    Me.cmbTipoGest.Items.Add(New ListItem("ORDINARIO", "O"))
                    Me.cmbTipoGest.Items.Add(New ListItem("STRAORDINARIO", "S"))
                    If Not IsNothing(Request.QueryString("TIPO")) Then
                        Me.cmbTipoGest.SelectedValue = Request.QueryString("TIPO")
                    End If
                    If Me.cmbTipoGest.SelectedValue = "S" Then
                        Me.txtInizioGest.ReadOnly = False
                        Me.TxtFineGest.ReadOnly = False
                        Me.TxtAnnoFine.ReadOnly = False
                    End If

                    Me.cmbNumRate.Items.Add(New ListItem("1", "1"))
                    Me.cmbNumRate.Items.Add(New ListItem("2", "2"))
                    Me.cmbNumRate.Items.Add(New ListItem("3", "3"))
                    Me.cmbNumRate.Items.Add(New ListItem("4", "4"))
                    Me.cmbNumRate.Items.Add(New ListItem("5", "5"))
                    Me.cmbNumRate.Items.Add(New ListItem("6", "6"))
                    Me.cmbNumRate.Items.Add(New ListItem("7", "7"))
                    Me.cmbNumRate.Items.Add(New ListItem("8", "8"))
                    Me.cmbNumRate.Items.Add(New ListItem("9", "9"))
                    Me.cmbNumRate.Items.Add(New ListItem("10", "10"))
                    Me.cmbNumRate.Items.Add(New ListItem("11", "11"))
                    Me.cmbNumRate.Items.Add(New ListItem("12", "12"))
                    Me.cmbNumRate.Items.Add(New ListItem("13", "13"))
                    Me.cmbNumRate.Items.Add(New ListItem("14", "14"))
                    Me.cmbNumRate.Items.Add(New ListItem("15", "15"))
                    Me.cmbNumRate.Items.Add(New ListItem("16", "16"))
                    Me.cmbNumRate.Items.Add(New ListItem("17", "17"))
                    Me.cmbNumRate.Items.Add(New ListItem("18", "18"))
                    Me.cmbNumRate.Items.Add(New ListItem("19", "19"))
                    Me.cmbNumRate.Items.Add(New ListItem("20", "20"))
                    Me.cmbNumRate.Items.Add(New ListItem("21", "21"))
                    Me.cmbNumRate.Items.Add(New ListItem("22", "22"))
                    Me.cmbNumRate.Items.Add(New ListItem("23", "23"))
                    Me.cmbNumRate.Items.Add(New ListItem("24", "24"))
                    Me.cmbNumRate.SelectedValue = 1

                    'CREO SUL DATABASE UN PALETTO PER IL ROLBACK FINO A QUI
                    par.cmd.CommandText = "SAVEPOINT PREVENTIVO"
                    par.cmd.ExecuteNonQuery()

                    If vIdGestione = "" Then
                        'SalvaGestione(True)
                        ApriRicerca()
                        Cerca()
                        ApriRicercaBis()
                        SommaColonne()
                        AddJavascriptFunction()
                        CreaIdNuovaGestione()
                    Else
                        NewEs.Value = 0
                        ApriRicerca()
                        Cerca()
                        ApriRicercaBis()
                        SommaColonne()
                        AddJavascriptFunction()
                    End If
                    EnableDisableDgv()
                    GestStatoBilancio(vStato)
                    nRate.Value = Me.cmbNumRate.SelectedValue.ToString

                End If
                'If Request.QueryString("MODIFICATO") = 1 Then
                '    Session.Item("MODIFYMODAL") = 1
                'End If
                If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) Then
                    SettaFrmReadOnly()
                End If
                'If Session.Item("MODIFYMODAL") = 1 Then
                '    txtconfuscita.Value = 1
                'End If




            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try

        End If
        'Me.DataGridVociSpesa.Width = 855
        'Me.DataGridVociSpMor.Width = 855

    End Sub


    Private Sub ApriRicerca()
        Try
            If Not String.IsNullOrEmpty(vIdGestione) Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
                par.cmd.CommandText = "SELECT TIPO,DATA_INIZIO, DATA_FINE, N_RATE," _
                    & " RATA_1_SCAD,RATA_2_SCAD,RATA_3_SCAD,RATA_4_SCAD,RATA_5_SCAD,RATA_6_SCAD," _
                    & " RATA_7_SCAD,RATA_8_SCAD,RATA_9_SCAD,RATA_10_SCAD,RATA_11_SCAD,RATA_12_SCAD," _
                    & " RATA_13_SCAD,RATA_14_SCAD,RATA_15_SCAD,RATA_16_SCAD,RATA_17_SCAD,RATA_18_SCAD," _
                    & " RATA_19_SCAD,RATA_20_SCAD,RATA_21_SCAD,RATA_22_SCAD,RATA_23_SCAD,RATA_24_SCAD," _
                    & " NOTE,STATO_BILANCIO FROM SISCOM_MI.COND_GESTIONE WHERE ID = " & vIdGestione & " AND ID_CONDOMINIO =" & Request.QueryString("IDCONDOMINIO")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then

                    Me.txtInizioGest.Text = (par.FormattaData(myReader1("DATA_INIZIO").ToString)).Substring(0, 5)
                    Me.TxtFineGest.Text = (par.FormattaData(myReader1("DATA_FINE").ToString)).Substring(0, 5)
                    Me.txtAnnoInizio.Text = myReader1("DATA_INIZIO").ToString.Substring(0, 4)
                    Me.TxtAnnoFine.Text = myReader1("DATA_FINE").ToString.Substring(0, 4)
                    Me.cmbNumRate.SelectedValue = myReader1("N_RATE").ToString
                    Me.cmbTipoGest.SelectedValue = myReader1("TIPO").ToString
                    Me.HiddenField1.Value = par.FormattaData(myReader1("RATA_1_SCAD").ToString)
                    Me.HiddenField2.Value = par.FormattaData(myReader1("RATA_2_SCAD").ToString)
                    Me.HiddenField3.Value = par.FormattaData(myReader1("RATA_3_SCAD").ToString)
                    Me.HiddenField4.Value = par.FormattaData(myReader1("RATA_4_SCAD").ToString)
                    Me.HiddenField5.Value = par.FormattaData(myReader1("RATA_5_SCAD").ToString)
                    Me.HiddenField6.Value = par.FormattaData(myReader1("RATA_6_SCAD").ToString)
                    Me.HiddenField7.Value = par.FormattaData(myReader1("RATA_7_SCAD").ToString)
                    Me.HiddenField8.Value = par.FormattaData(myReader1("RATA_8_SCAD").ToString)
                    Me.HiddenField9.Value = par.FormattaData(myReader1("RATA_9_SCAD").ToString)
                    Me.HiddenField10.Value = par.FormattaData(myReader1("RATA_10_SCAD").ToString)
                    Me.HiddenField11.Value = par.FormattaData(myReader1("RATA_11_SCAD").ToString)
                    Me.HiddenField12.Value = par.FormattaData(myReader1("RATA_12_SCAD").ToString)
                    Me.HiddenField13.Value = par.FormattaData(myReader1("RATA_13_SCAD").ToString)
                    Me.HiddenField14.Value = par.FormattaData(myReader1("RATA_14_SCAD").ToString)
                    Me.HiddenField15.Value = par.FormattaData(myReader1("RATA_15_SCAD").ToString)
                    Me.HiddenField16.Value = par.FormattaData(myReader1("RATA_16_SCAD").ToString)
                    Me.HiddenField17.Value = par.FormattaData(myReader1("RATA_17_SCAD").ToString)
                    Me.HiddenField18.Value = par.FormattaData(myReader1("RATA_18_SCAD").ToString)
                    Me.HiddenField19.Value = par.FormattaData(myReader1("RATA_19_SCAD").ToString)
                    Me.HiddenField20.Value = par.FormattaData(myReader1("RATA_20_SCAD").ToString)
                    Me.HiddenField21.Value = par.FormattaData(myReader1("RATA_21_SCAD").ToString)
                    Me.HiddenField22.Value = par.FormattaData(myReader1("RATA_22_SCAD").ToString)
                    Me.HiddenField23.Value = par.FormattaData(myReader1("RATA_23_SCAD").ToString)
                    Me.HiddenField24.Value = par.FormattaData(myReader1("RATA_24_SCAD").ToString)

                    Me.txtNote.Text = par.IfNull(myReader1("NOTE").ToString, "")
                    Me.vStato = par.IfNull(myReader1("STATO_BILANCIO").ToString.ToUpper, "P0")

                End If
                myReader1.Close()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub ApriRicercaBis()
        Try
            If Not String.IsNullOrEmpty(vIdGestione) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text = Me.HiddenField1.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text = Me.HiddenField2.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text = Me.HiddenField3.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text = Me.HiddenField4.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text = Me.HiddenField5.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text = Me.HiddenField6.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = Me.HiddenField7.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = Me.HiddenField8.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = Me.HiddenField9.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = Me.HiddenField10.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = Me.HiddenField11.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = Me.HiddenField12.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = Me.HiddenField13.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = Me.HiddenField14.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = Me.HiddenField15.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = Me.HiddenField16.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = Me.HiddenField17.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = Me.HiddenField18.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = Me.HiddenField19.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = Me.HiddenField20.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = Me.HiddenField21.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = Me.HiddenField22.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = Me.HiddenField23.Value
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = Me.HiddenField24.Value
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub GestStatoBilancio(ByVal ST As String)

        Select Case ST
            Case "P0"
                Me.lblStatoPreventivo.Text = "STATO: BOZZA"
            Case "P1"
                Me.lblStatoPreventivo.Text = "STATO: CONVALIDATO"
                SettaFrmReadOnlyConvalida()

            Case "C"
                Me.lblStatoPreventivo.Text = "STATO: CONSUNTIVATO"
                'SettaFrmReadOnlyConvalida()

        End Select

    End Sub
    Private Sub Cerca()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader



            If vIdGestione = "" Then
                par.cmd.CommandText = "SELECT MAX(ID) AS ID_PIANO_F FROM SISCOM_MI.PF_MAIN WHERE ID_STATO = 5"
            Else
                par.cmd.CommandText = "select distinct(id_piano_finanziario) as id_piano_f from siscom_mi.cond_voci_spesa_pf where id_voce_cond in (select id_voce from siscom_mi.cond_gestione_dett where id_gestione =" & vIdGestione & ")"

                'par.cmd.CommandText = "SELECT pf_main.ID as id_piano_f FROM siscom_mi.pf_main ,siscom_mi.T_ESERCIZIO_FINANZIARIO " _
                '                    & "WHERE id_esercizio_finanziario = T_ESERCIZIO_FINANZIARIO.ID " _
                '                    & "AND SUBSTR(T_ESERCIZIO_FINANZIARIO.inizio,1,4) = (SELECT CASE WHEN SUBSTR(DATA_INIZIO,1,4)>2010 THEN SUBSTR(DATA_INIZIO,1,4) ELSE '2011' END FROM siscom_mi.cond_gestione WHERE ID = " & vIdGestione & ")"
            End If

            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                idPianoF.Value = par.IfNull(myReader1("ID_PIANO_F"), 0)

            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT id_stato from siscom_mi.pf_main where id = " & idPianoF.Value
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                If par.IfNull(myReader1("id_stato"), 0) <> 5 Then
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "SELECT MAX(ID) AS ID_PIANO_F FROM SISCOM_MI.PF_MAIN WHERE ID_STATO = 5"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        idPianoF.Value = par.IfNull(lettore("ID_PIANO_F"), 0)
                    End If
                    lettore.Close()
                End If
            End If
            myReader1.Close()

            If idPianoF.Value = 0 Then
                Response.Write("<script>alert('Impossibile procedere!Nessun piano finanziario con stato approvato è presente!');</script>")
                Response.Write("<script>window.close();</script>")
                Session("MODIFYMODAL") = 1
                Exit Sub
            End If

            Dim straordinaria As String = ""
            If cmbTipoGest.SelectedValue.ToString = "S" Then
                straordinaria = " FL_STRAORDINARIA=1 AND "
            Else
                straordinaria = " FL_STRAORDINARIA=0 AND "
            End If

            par.cmd.CommandText = "SELECT COND_VOCI_SPESA.FL_TOTALE,1 AS TIPO_RIGA,COND_VOCI_SPESA.ID AS IDVOCE, " _
                                & "COND_VOCI_SPESA.DESCRIZIONE,'' AS ID_GESTIONE,'' AS CONGUAGLIO_GP, '' AS PREVENTIVO," _
                                & " '' AS RATA_1,'' AS RATA_2,'' AS RATA_3,'' AS RATA_4, '' AS RATA_5,'' AS RATA_6, " _
                                & " '' AS RATA_7,'' AS RATA_8,'' AS RATA_9,'' AS RATA_10, '' AS RATA_11,'' AS RATA_12, " _
                                & " '' AS RATA_13,'' AS RATA_14,'' AS RATA_15,'' AS RATA_16, '' AS RATA_17,'' AS RATA_18, " _
                                & " '' AS RATA_19,'' AS RATA_20,'' AS RATA_21,'' AS RATA_22, '' AS RATA_23,'' AS RATA_24, " _
                                & "COND_VOCI_SPESA_PF.ID_VOCE_PF, COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO " _
                                & "FROM SISCOM_MI.COND_VOCI_SPESA,SISCOM_MI.COND_VOCI_SPESA_PF WHERE " & straordinaria & " FL_TOTALE = 1 AND COND_VOCI_SPESA.ID = ID_VOCE_COND AND ID_PIANO_FINANZIARIO = " & idPianoF.Value & " ORDER BY idvoce ASC"




            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()


            da.Fill(dt)

            'voci morosità o con fl_totale = 0
            par.cmd.CommandText = "SELECT COND_VOCI_SPESA.FL_TOTALE,2 AS TIPO_RIGA,COND_VOCI_SPESA.ID AS IDVOCE, " _
                                & "COND_VOCI_SPESA.DESCRIZIONE,'' AS ID_GESTIONE,'' AS CONGUAGLIO_GP, '' AS PREVENTIVO," _
                                & " '' AS RATA_1,'' AS RATA_2,'' AS RATA_3,'' AS RATA_4, '' AS RATA_5,'' AS RATA_6, " _
                                & " '' AS RATA_7,'' AS RATA_8,'' AS RATA_9,'' AS RATA_10, '' AS RATA_11,'' AS RATA_12, " _
                                & " '' AS RATA_13,'' AS RATA_14,'' AS RATA_15,'' AS RATA_16, '' AS RATA_17,'' AS RATA_18, " _
                                & " '' AS RATA_19,'' AS RATA_20,'' AS RATA_21,'' AS RATA_22, '' AS RATA_23,'' AS RATA_24, " _
                                & "COND_VOCI_SPESA_PF.ID_VOCE_PF, COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO " _
                                & "FROM SISCOM_MI.COND_VOCI_SPESA,SISCOM_MI.COND_VOCI_SPESA_PF WHERE " & straordinaria & " FL_TOTALE = 0 AND COND_VOCI_SPESA.ID = ID_VOCE_COND AND ID_PIANO_FINANZIARIO = " & idPianoF.Value & " ORDER BY idvoce ASC"

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable()

            da.Fill(dt2)

            If vIdGestione <> "" Then
                Me.cmbTipoGest.Enabled = False
                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "SELECT CONGUAGLIO_GP, PREVENTIVO" _
                    & " FROM SISCOM_MI.COND_GESTIONE_DETT" _
                    & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        row.Item("CONGUAGLIO_GP") = IsNumFormat(myReader1("CONGUAGLIO_GP"), "", "##,##0.00")
                        row.Item("PREVENTIVO") = IsNumFormat(myReader1("PREVENTIVO"), "", "##,##0.00")
                    End If
                    myReader1.Close()
                    'IMPORTO SINGOLE RATE
                    '**************RATA 1
                    If Not String.IsNullOrEmpty(Me.HiddenField1.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField1.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_1") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 2
                    If Not String.IsNullOrEmpty(Me.HiddenField2.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField2.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_2") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 3
                    If Not String.IsNullOrEmpty(Me.HiddenField3.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField3.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_3") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 4
                    If Not String.IsNullOrEmpty(Me.HiddenField4.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField4.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_4") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 5
                    If Not String.IsNullOrEmpty(Me.HiddenField5.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField5.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_5") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 6
                    If Not String.IsNullOrEmpty(Me.HiddenField6.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField6.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_6") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 7
                    If Not String.IsNullOrEmpty(Me.HiddenField7.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField7.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_7") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 8
                    If Not String.IsNullOrEmpty(Me.HiddenField8.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField8.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_8") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 9
                    If Not String.IsNullOrEmpty(Me.HiddenField9.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField9.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_9") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 10
                    If Not String.IsNullOrEmpty(Me.HiddenField10.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField10.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_10") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 11
                    If Not String.IsNullOrEmpty(Me.HiddenField11.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField11.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_11") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 12
                    If Not String.IsNullOrEmpty(Me.HiddenField12.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField12.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_12") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 13
                    If Not String.IsNullOrEmpty(Me.HiddenField13.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField13.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_13") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 14
                    If Not String.IsNullOrEmpty(Me.HiddenField14.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField14.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_14") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 15
                    If Not String.IsNullOrEmpty(Me.HiddenField15.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField15.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_15") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 16
                    If Not String.IsNullOrEmpty(Me.HiddenField16.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField16.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_16") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 17
                    If Not String.IsNullOrEmpty(Me.HiddenField17.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField17.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_17") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 18
                    If Not String.IsNullOrEmpty(Me.HiddenField18.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField18.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_18") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 19
                    If Not String.IsNullOrEmpty(Me.HiddenField19.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField19.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_19") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 20
                    If Not String.IsNullOrEmpty(Me.HiddenField20.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField20.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_20") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 21
                    If Not String.IsNullOrEmpty(Me.HiddenField21.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField21.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_21") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 22
                    If Not String.IsNullOrEmpty(Me.HiddenField22.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField22.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_22") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 23
                    If Not String.IsNullOrEmpty(Me.HiddenField23.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField23.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_23") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 24
                    If Not String.IsNullOrEmpty(Me.HiddenField24.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField24.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_24") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If


                Next




                For Each row As Data.DataRow In dt2.Rows
                    par.cmd.CommandText = "SELECT CONGUAGLIO_GP, PREVENTIVO" _
                    & " FROM SISCOM_MI.COND_GESTIONE_DETT" _
                    & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        row.Item("CONGUAGLIO_GP") = IsNumFormat(myReader1("CONGUAGLIO_GP"), "", "##,##0.00")
                        row.Item("PREVENTIVO") = IsNumFormat(myReader1("PREVENTIVO"), "", "##,##0.00")
                    End If
                    myReader1.Close()
                    'IMPORTO SINGOLE RATE
                    '**************RATA 1
                    If Not String.IsNullOrEmpty(Me.HiddenField1.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField1.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_1") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 2
                    If Not String.IsNullOrEmpty(Me.HiddenField2.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField2.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_2") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 3
                    If Not String.IsNullOrEmpty(Me.HiddenField3.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField3.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_3") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 4
                    If Not String.IsNullOrEmpty(Me.HiddenField4.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField4.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_4") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 5
                    If Not String.IsNullOrEmpty(Me.HiddenField5.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField5.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_5") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 6
                    If Not String.IsNullOrEmpty(Me.HiddenField6.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField6.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_6") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 7
                    If Not String.IsNullOrEmpty(Me.HiddenField7.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField7.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_7") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 8
                    If Not String.IsNullOrEmpty(Me.HiddenField8.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField8.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_8") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 9
                    If Not String.IsNullOrEmpty(Me.HiddenField9.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField9.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_9") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 10
                    If Not String.IsNullOrEmpty(Me.HiddenField10.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField10.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_10") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 11
                    If Not String.IsNullOrEmpty(Me.HiddenField11.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField11.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_11") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 12
                    If Not String.IsNullOrEmpty(Me.HiddenField12.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField12.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_12") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 13
                    If Not String.IsNullOrEmpty(Me.HiddenField13.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField13.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_13") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 14
                    If Not String.IsNullOrEmpty(Me.HiddenField14.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField14.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_14") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 15
                    If Not String.IsNullOrEmpty(Me.HiddenField15.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField15.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_15") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 16
                    If Not String.IsNullOrEmpty(Me.HiddenField16.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField16.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_16") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 17
                    If Not String.IsNullOrEmpty(Me.HiddenField17.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField17.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_17") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 18
                    If Not String.IsNullOrEmpty(Me.HiddenField18.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField18.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_18") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 19
                    If Not String.IsNullOrEmpty(Me.HiddenField19.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField19.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_19") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 20
                    If Not String.IsNullOrEmpty(Me.HiddenField20.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField20.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_20") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 21
                    If Not String.IsNullOrEmpty(Me.HiddenField21.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField21.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_21") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 22
                    If Not String.IsNullOrEmpty(Me.HiddenField22.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField22.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_22") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 23
                    If Not String.IsNullOrEmpty(Me.HiddenField23.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField23.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("rata_6") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 24
                    If Not String.IsNullOrEmpty(Me.HiddenField24.Value) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & vIdGestione & " AND RATA_SCAD = " & par.AggiustaData(Me.HiddenField24.Value)
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_24") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                        End If
                        myReader1.Close()
                    End If


                Next

                'DataGridVociSpMor.DataSource = dt2
                'DataGridVociSpMor.DataBind()


            ElseIf Not String.IsNullOrEmpty(Request.QueryString("ANNO")) Then
                '08/05/2012 Modifica per più contabilità parallele aventi diversa tipologia!Viene commentato questo controllo  verrà effettuato al primo salvataggio e basandosi anche sulla tipologia
                '*controllo esistenza di un preventivo di spesa già presente
                '
                'If Me.txtInizioGest.Text = "01/01" AndAlso Me.TxtFineGest.Text = "31/12" Then
                '    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.COND_GESTIONE WHERE SUBSTR(COND_GESTIONE.DATA_INIZIO,0,4)=" & Request.QueryString("ANNO") & " AND SUBSTR(COND_GESTIONE.DATA_FINE,0,4)=" & Request.QueryString("ANNO") & "  AND ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                'Else
                '    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.COND_GESTIONE WHERE SUBSTR(COND_GESTIONE.DATA_INIZIO,0,4)=" & Request.QueryString("ANNO") & " AND SUBSTR(COND_GESTIONE.DATA_FINE,0,4)=" & Request.QueryString("ANNO") + 1 & "  AND ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                'End If
                'myReader1 = par.cmd.ExecuteReader
                'If myReader1.Read Then
                '    Response.Write("<script>alert('Esiste già un preventivo avente stessa tipologia per l\'anno scelto!');</script>")
                '    Response.Write("<script>window.close();</script>")
                '    Exit Sub
                'End If
                'myReader1.Close()

                Dim precGestione As String = "0"
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.COND_GESTIONE WHERE id_Condominio = " & idCondominio.Value & " ORDER BY ID DESC" '& " AND ID_GESTIONE <> " & vIdGestione
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    precGestione = par.IfNull(myReader1("ID"), 0)
                End If
                myReader1.Close()
                'Se il controllo va a buon fine riempio i dati del conguaglio
                For Each row As Data.DataRow In dt.Rows
                    par.cmd.CommandText = "SELECT CONGUAGLIO FROM SISCOM_MI.COND_GESTIONE_DETT, SISCOM_MI.COND_GESTIONE WHERE COND_GESTIONE.ID= COND_GESTIONE_DETT.ID_GESTIONE AND ID_GESTIONE = " & precGestione & " AND ID_VOCE = " & row.Item("IDVOCE") & " AND ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        row.Item("CONGUAGLIO_GP") = Format(CDec(par.IfNull(myReader1("CONGUAGLIO"), 0)), "0.00")
                    Else
                        row.Item("CONGUAGLIO_GP") = Format(CDec("0,00"), "0.00")
                    End If
                    myReader1.Close()
                Next
                Me.txtAnnoInizio.Text = Request.QueryString("ANNO")
                If Me.txtInizioGest.Text = "01/01" AndAlso Me.TxtFineGest.Text = "31/12" Then
                    Me.TxtAnnoFine.Text = Request.QueryString("ANNO")
                Else
                    Me.TxtAnnoFine.Text = Request.QueryString("ANNO") + 1

                End If
            End If

            Dim dtFinale As Data.DataTable = dt.Clone()
            Dim riga As Data.DataRow = dtFinale.NewRow
            riga.Item("DESCRIZIONE") = "TOTALE"
            dtFinale.Rows.Add(riga)
            dt.Merge(dtFinale)
            dtFinale.Merge(dt)
            dtFinale.Merge(dt2)
            dtFinale.Rows(0).Item("DESCRIZIONE") = ""

            DataGridVociSpesaRiepilogo.DataSource = dtFinale
            DataGridVociSpesaRiepilogo.DataBind()

            For Each elemento As DataGridItem In DataGridVociSpesaRiepilogo.Items
                If elemento.Cells(par.IndDGC(DataGridVociSpesaRiepilogo, "DESCRIZIONE")).Text.ToString = "TOTALE" Then
                    elemento.Cells(par.IndDGC(DataGridVociSpesaRiepilogo, "DESCRIZIONE")).ForeColor = Drawing.Color.MediumBlue
                    elemento.Cells(par.IndDGC(DataGridVociSpesaRiepilogo, "DESCRIZIONE")).Font.Bold = True
                End If
            Next

            DataGridVociSpesa.DataSource = dtFinale
            DataGridVociSpesa.DataBind()
            'DataGridVociSpMor.DataSource = dt2
            'DataGridVociSpMor.DataBind()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
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
    Public Property vIdGestione() As String
        Get
            If Not (ViewState("par_vIdGestione") Is Nothing) Then
                Return CStr(ViewState("par_vIdGestione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdGestione") = value
        End Set

    End Property
    Public Property vStato() As String
        Get
            If Not (ViewState("par_vStato") Is Nothing) Then
                Return CStr(ViewState("par_vStato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vStato") = value
        End Set

    End Property
    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        Try

            'CType(Me.Page.FindControl("txt"), TextBox).Text = "no"

            If vIdGestione = "" Or NewEs.Value = 1 Then
                SalvaGestione()
                Me.txtSalvato.Value = 1
            Else
                If AggSitPat.Value = 1 Then
                    If vStato = "P1" Then
                        If Convalida(False) = True Then
                            ApriRicerca()
                            Cerca()
                            ApriRicercaBis()
                        End If
                    Else
                        UpdateGestione(True)
                        ApriRicerca()
                        Cerca()
                        ApriRicercaBis()

                    End If


                End If
            End If

            EnableDisableDgv()
            Me.nRate.Value = Me.cmbNumRate.SelectedValue.ToString

            AggSitPat.Value = 0
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub SalvaGestione(Optional ByVal Nuovo As Boolean = False)
        Try


            Dim StatoBil As String = ""

            If Not IsDate(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) Or Not IsDate(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) Then
                Response.Write("<script>alert('Le date del periodo di gestione sono errate!');</script>")
                Exit Sub
            End If
            If par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) >= par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) Then
                Response.Write("<script>alert('Le date del periodo di gestione sono errate!');</script>")
                Exit Sub
            End If

            ClearDisbledItDgv()
            Dim i As Integer = 0
            Dim di As DataGridItem
            'Dim t As TextBox
            Dim idVoce As String = ""
            Dim ConguaglioGP As String = ""
            Dim Preventivo As String = ""
            Dim Rata1 As String = ""
            Dim Rata2 As String = ""
            Dim Rata3 As String = ""
            Dim Rata4 As String = ""
            Dim Rata5 As String = ""
            Dim Rata6 As String = ""
            Dim Rata7 As String = ""
            Dim Rata8 As String = ""
            Dim Rata9 As String = ""
            Dim Rata10 As String = ""
            Dim Rata11 As String = ""
            Dim Rata12 As String = ""
            Dim Rata13 As String = ""
            Dim Rata14 As String = ""
            Dim Rata15 As String = ""
            Dim Rata16 As String = ""
            Dim Rata17 As String = ""
            Dim Rata18 As String = ""
            Dim Rata19 As String = ""
            Dim Rata20 As String = ""
            Dim Rata21 As String = ""
            Dim Rata22 As String = ""
            Dim Rata23 As String = ""
            Dim Rata24 As String = ""

            Dim SalvaSitPatr As Boolean = False
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            '****************CONTROLLO DELLE RATE CHE SIANO SUCCESSIVE********************
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 2 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 3 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 4 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 5 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 6 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 7 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 8 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 9 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 10 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 11 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 12 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 13 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 14 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 15 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 16 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 17 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 18 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 19 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 20 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 21 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 22 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 23 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 24 è inferiore o uguale a quella precedente!');</script>")
                    Exit Sub
                End If
            End If

            If Nuovo = False Then
                '*****************CONTROLLO DATE NON VUOTE PER N-RATE DEFINITO DALLA COMBO
                If ControlloDateRate() = False Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLe date di scadenza delle rate sono obbligatorie!');</script>")
                    Exit Sub
                End If

                '**************fine controllo date************************************************
                '*************CONTROLLO CHE LA SOMMA DELLE RATE SIA INFERIORE O UGUALE AL CONGUAGLIO + IL PREVENTIVO
                If ControlloRate() = False Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nVerificare gli importi delle rate!');</script>")
                    Exit Sub
                End If
                '*************FINE CONTROLLO CHE LA SOMMA DELLE RATE SIA INFERIORE O UGUALE AL CONGUAGLIO + IL PREVENTIVO
            End If



            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            '08/05/2012 Controllo coesistenza preventivi di spesa avente lo stesso periodo ma tipologia diversa
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_GESTIONE WHERE DATA_INIZIO= " & par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) & " AND DATA_FINE=" & par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) & " AND TIPO = '" & Me.cmbTipoGest.SelectedValue & "' AND ID_CONDOMINIO=" & Request.QueryString("IDCONDOMINIO")
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read And NewEs.Value = 0 Then
                Response.Write("<script>alert('Preventivo di spesa già inserito per questo periodo ed avente stessa tipologia!');</script>")
                Exit Sub
            End If
            myReader1.Close()
            If Not String.IsNullOrEmpty(Me.txtInizioGest.Text) AndAlso Not String.IsNullOrEmpty(Me.txtAnnoInizio.Text) AndAlso Not String.IsNullOrEmpty(Me.TxtFineGest.Text) AndAlso Not String.IsNullOrEmpty(Me.TxtAnnoFine.Text) Then
                If vIdGestione = "" Then
                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COND_GESTIONE.NEXTVAL FROM DUAL"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        vIdGestione = myReader1(0)
                    End If
                    myReader1.Close()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE (ID, ID_CONDOMINIO,DATA_INIZIO,DATA_FINE, TIPO, N_RATE," _
                        & " RATA_1_SCAD,RATA_2_SCAD,RATA_3_SCAD,RATA_4_SCAD,RATA_5_SCAD,RATA_6_SCAD," _
                        & " RATA_7_SCAD,RATA_8_SCAD,RATA_9_SCAD,RATA_10_SCAD,RATA_11_SCAD,RATA_12_SCAD," _
                        & " RATA_13_SCAD,RATA_14_SCAD,RATA_15_SCAD,RATA_16_SCAD,RATA_17_SCAD,RATA_18_SCAD," _
                        & " RATA_19_SCAD,RATA_20_SCAD,RATA_21_SCAD,RATA_22_SCAD,RATA_23_SCAD,RATA_24_SCAD," _
                        & " STATO_BILANCIO,NOTE)" _
                    & " VALUES (" & vIdGestione & ", " & Request.QueryString("IDCONDOMINIO") & "," _
                    & " " & par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) & ", " & par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) & "," _
                    & " '" & Me.cmbTipoGest.SelectedValue & "', " & Me.cmbNumRate.SelectedValue & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") & "," _
                    & "'P0','" & par.PulisciStrSql(Me.txtNote.Text) & "')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE SET DATA_INIZIO=" & par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) & " ,DATA_FINE=" & par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) & " ,TIPO='" & Me.cmbTipoGest.SelectedValue & "' ,N_RATE=" & Me.cmbNumRate.SelectedValue & "," _
                    & " RATA_1_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") & "," _
                    & " RATA_2_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") & "," _
                    & " RATA_3_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") & "," _
                    & " RATA_4_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") & "," _
                    & " RATA_5_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") & "," _
                    & " RATA_6_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") & "," _
                    & " RATA_7_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") & "," _
                    & " RATA_8_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") & "," _
                    & " RATA_9_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") & "," _
                    & " RATA_10_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") & "," _
                    & " RATA_11_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") & "," _
                    & " RATA_12_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") & "," _
                    & " RATA_13_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") & "," _
                    & " RATA_14_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") & "," _
                    & " RATA_15_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") & "," _
                    & " RATA_16_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") & "," _
                    & " RATA_17_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") & "," _
                    & " RATA_18_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") & "," _
                    & " RATA_19_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") & "," _
                    & " RATA_20_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") & "," _
                    & " RATA_21_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") & "," _
                    & " RATA_22_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") & "," _
                    & " RATA_23_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") & "," _
                    & " RATA_24_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") & "," _
                    & " NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & " 'WHERE ID = " & vIdGestione
                    par.cmd.ExecuteNonQuery()


                End If
                '****************INSERIMENTO DEI DATI PRESENTI NELLA DATAGRID******************
                For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                    di = Me.DataGridVociSpesa.Items(i)
                    If di.Cells(par.IndDGC(DataGridVociSpesa, "TIPO_RIGA")).Text = "1" Then
                        idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                        ConguaglioGP = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Text
                        Preventivo = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Text
                        If Preventivo <> "" Then
                            SalvaSitPatr = True
                        End If
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT (ID_GESTIONE, ID_VOCE,CONGUAGLIO_GP,PREVENTIVO)" _
                                            & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.VirgoleInPunti(ConguaglioGP.Replace(".", "")), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Preventivo.Replace(".", "")), "Null") & ")"
                        par.cmd.ExecuteNonQuery()

                        Rata1 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text, 0)
                        Rata2 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text, 0)
                        Rata3 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text, 0)
                        Rata4 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text, 0)
                        Rata5 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text, 0)
                        Rata6 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text, 0)
                        Rata7 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text, 0)
                        Rata8 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text, 0)
                        Rata9 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text, 0)
                        Rata10 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text, 0)
                        Rata11 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text, 0)
                        Rata12 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text, 0)
                        Rata13 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text, 0)
                        Rata14 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text, 0)
                        Rata15 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text, 0)
                        Rata16 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text, 0)
                        Rata17 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text, 0)
                        Rata18 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text, 0)
                        Rata19 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text, 0)
                        Rata20 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text, 0)
                        Rata21 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text, 0)
                        Rata22 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text, 0)
                        Rata23 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text, 0)
                        Rata24 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text, 0)

                        If Rata1 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") & ",1," & par.IfEmpty(par.VirgoleInPunti(Rata1.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata2 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") & ",2," & par.IfEmpty(par.VirgoleInPunti(Rata2.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata3 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") & ",3," & par.IfEmpty(par.VirgoleInPunti(Rata3.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata4 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") & ",4," & par.IfEmpty(par.VirgoleInPunti(Rata4.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata5 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") & ",5," & par.IfEmpty(par.VirgoleInPunti(Rata5.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata6 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") & ",6," & par.IfEmpty(par.VirgoleInPunti(Rata6.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata7 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") & ",7," & par.IfEmpty(par.VirgoleInPunti(Rata7.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata8 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") & ",8," & par.IfEmpty(par.VirgoleInPunti(Rata8.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata9 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") & ",9," & par.IfEmpty(par.VirgoleInPunti(Rata9.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata10 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") & ",10," & par.IfEmpty(par.VirgoleInPunti(Rata10.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata11 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") & ",11," & par.IfEmpty(par.VirgoleInPunti(Rata11.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata12 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") & ",12," & par.IfEmpty(par.VirgoleInPunti(Rata12.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata13 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") & ",13," & par.IfEmpty(par.VirgoleInPunti(Rata13.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata14 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") & ",14," & par.IfEmpty(par.VirgoleInPunti(Rata14.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata15 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") & ",15," & par.IfEmpty(par.VirgoleInPunti(Rata15.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata16 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") & ",16," & par.IfEmpty(par.VirgoleInPunti(Rata16.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata17 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") & ",17," & par.IfEmpty(par.VirgoleInPunti(Rata17.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata18 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") & ",18," & par.IfEmpty(par.VirgoleInPunti(Rata18.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata19 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") & ",19," & par.IfEmpty(par.VirgoleInPunti(Rata19.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata20 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") & ",20," & par.IfEmpty(par.VirgoleInPunti(Rata20.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata21 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") & ",21," & par.IfEmpty(par.VirgoleInPunti(Rata21.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata22 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") & ",22," & par.IfEmpty(par.VirgoleInPunti(Rata22.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata23 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") & ",23," & par.IfEmpty(par.VirgoleInPunti(Rata23.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata24 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") & ",24," & par.IfEmpty(par.VirgoleInPunti(Rata24.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                Next
                '******************************FINE INSERIMENTO DATI DATAGRID**********************


                '************************INSERIMENTO VALORI DELLA MOROSITA'************************
                For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                    di = Me.DataGridVociSpesa.Items(i)
                    If di.Cells(par.IndDGC(DataGridVociSpesa, "TIPO_RIGA")).Text = "2" Then
                        idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                        ConguaglioGP = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Text
                        Preventivo = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Text
                        If Preventivo <> "" Then
                            SalvaSitPatr = True
                        End If
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT (ID_GESTIONE, ID_VOCE,CONGUAGLIO_GP,PREVENTIVO)" _
                                            & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.VirgoleInPunti(ConguaglioGP.Replace(".", "")), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Preventivo.Replace(".", "")), "Null") & ")"
                        par.cmd.ExecuteNonQuery()

                        Rata1 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text, 0)
                        Rata2 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text, 0)
                        Rata3 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text, 0)
                        Rata4 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text, 0)
                        Rata5 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text, 0)
                        Rata6 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text, 0)
                        Rata7 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text, 0)
                        Rata8 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text, 0)
                        Rata9 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text, 0)
                        Rata10 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text, 0)
                        Rata11 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text, 0)
                        Rata12 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text, 0)
                        Rata13 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text, 0)
                        Rata14 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text, 0)
                        Rata15 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text, 0)
                        Rata16 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text, 0)
                        Rata17 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text, 0)
                        Rata18 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text, 0)
                        Rata19 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text, 0)
                        Rata20 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text, 0)
                        Rata21 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text, 0)
                        Rata22 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text, 0)
                        Rata23 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text, 0)
                        Rata24 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text, 0)


                        If Rata1 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") & ",1," & par.IfEmpty(par.VirgoleInPunti(Rata1.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata2 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") & ",2," & par.IfEmpty(par.VirgoleInPunti(Rata2.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata3 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") & ",3," & par.IfEmpty(par.VirgoleInPunti(Rata3.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata4 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") & ",4," & par.IfEmpty(par.VirgoleInPunti(Rata4.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata5 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") & ",5," & par.IfEmpty(par.VirgoleInPunti(Rata5.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        If Rata6 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") & ",6," & par.IfEmpty(par.VirgoleInPunti(Rata6.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata7 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") & ",7," & par.IfEmpty(par.VirgoleInPunti(Rata7.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata8 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") & ",8," & par.IfEmpty(par.VirgoleInPunti(Rata8.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata9 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") & ",9," & par.IfEmpty(par.VirgoleInPunti(Rata9.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata10 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") & ",10," & par.IfEmpty(par.VirgoleInPunti(Rata10.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata11 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") & ",11," & par.IfEmpty(par.VirgoleInPunti(Rata11.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata12 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") & ",12," & par.IfEmpty(par.VirgoleInPunti(Rata12.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata13 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") & ",13," & par.IfEmpty(par.VirgoleInPunti(Rata13.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata14 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") & ",14," & par.IfEmpty(par.VirgoleInPunti(Rata14.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata15 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") & ",15," & par.IfEmpty(par.VirgoleInPunti(Rata15.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata16 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") & ",16," & par.IfEmpty(par.VirgoleInPunti(Rata16.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata17 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") & ",17," & par.IfEmpty(par.VirgoleInPunti(Rata17.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata18 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") & ",18," & par.IfEmpty(par.VirgoleInPunti(Rata18.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata19 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") & ",19," & par.IfEmpty(par.VirgoleInPunti(Rata19.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata20 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") & ",20," & par.IfEmpty(par.VirgoleInPunti(Rata20.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata21 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") & ",21," & par.IfEmpty(par.VirgoleInPunti(Rata21.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata22 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") & ",22," & par.IfEmpty(par.VirgoleInPunti(Rata22.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata23 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") & ",23," & par.IfEmpty(par.VirgoleInPunti(Rata23.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                        If Rata24 <> 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE, ID_VOCE,RATA_SCAD,N_RATA,IMPORTO)" _
                                                & " VALUES(" & vIdGestione & "," & idVoce & "," & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") & ",24," & par.IfEmpty(par.VirgoleInPunti(Rata24.Replace(".", "")), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                Next
                '******************************FINE INSERIMENTO DATI MOROSITA**********************

                '******************INSERIMENTO IN COND_UI_GESTIONE DI TUTTO QUELLO CHE è PRESENTE IN COND_UI PER IL CONDOMINIO
                If SalvaSitPatr = True Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                    Dim dt As New Data.DataTable
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        For Each row As System.Data.DataRow In dt.Rows
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI_PREVENTIVI(ID_GESTIONE, ID_UI, POSIZIONE_BILANCIO," _
                                & " MIL_PRO, MIL_ASC, MIL_COMPRO, MIL_GEST,MIL_RISC,NOTE,ADDEBITO_SINGOLO,ID_INTESTARIO)" _
                                & " VALUES (" & vIdGestione & ", " & row.Item("ID_UI") & ", '" & par.IfNull(row.Item("POSIZIONE_BILANCIO"), "") & "'," _
                                & " " & par.VirgoleInPunti(row.Item("MIL_PRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_ASC")) & ", " & par.VirgoleInPunti(row.Item("MIL_COMPRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_GEST")) & ", " & par.VirgoleInPunti(row.Item("MIL_RISC")) & ", '" & par.PulisciStrSql(par.IfNull(row.Item("NOTE"), "")) & "'," _
                                & " " & par.VirgoleInPunti(row.Item("ADDEBITO_SINGOLO")) & ", " & par.IfNull(row.Item("ID_INTESTARIO"), "Null") & ")"
                            par.cmd.ExecuteNonQuery()

                        Next
                    End If
                End If
                '******************FINE INSERIMENTO COND_UI_GESTIONE

                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','PREVENTIVO CONTABILITA CONDOMINIO')"
                par.cmd.ExecuteNonQuery()

                Me.txtModificato.Value = 0
                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                Me.cmbTipoGest.Enabled = False
                NewEs.Value = 0
            Else
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!');</script>")
                Exit Sub
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try

    End Sub
    Private Sub UpdateDateScadDett()
        Try
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 1 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If

            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 2 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 3 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 4 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 5 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 6 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 7 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 8 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 9 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 10 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 11 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 12 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 13 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 14 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 15 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 16 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 17 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 18 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 19 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 20 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 21 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 22 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 23 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = " & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") _
                    & " WHERE N_RATA = 24 AND ID_GESTIONE = " & vIdGestione
                par.cmd.ExecuteNonQuery()

            End If

            If Me.cmbNumRate.SelectedValue.ToString < nRate.Value Then
                '**************PULIZIA DELLE RATE VECCHIE
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET RATA_SCAD = '', IMPORTO = 0 WHERE ID_GESTIONE =" & vIdGestione _
                                    & " AND N_RATA >" & Me.cmbNumRate.SelectedValue.ToString
                par.cmd.ExecuteNonQuery()
            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub PrenotDaEliminare(ByVal idVoce As String, ByVal numrata As Integer)

        Dim lettPrenDaCancellare As Oracle.DataAccess.Client.OracleDataReader
        par.cmd.CommandText = "select id_prenotazione from siscom_mi.cond_gestione_dett_scad where ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = " & numrata
        lettPrenDaCancellare = par.cmd.ExecuteReader
        If lettPrenDaCancellare.Read Then
            If par.IfNull(lettPrenDaCancellare("id_prenotazione"), 0) <> 0 Then

                If Not String.IsNullOrEmpty(PrenDaCancellare) Then
                    PrenDaCancellare = PrenDaCancellare & ","
                End If
                PrenDaCancellare = PrenDaCancellare & par.IfNull(lettPrenDaCancellare("id_prenotazione"), "")
            End If

        End If
        lettPrenDaCancellare.Close()
    End Sub
    Private Function UpdateGestione(ByVal msgEsito As Boolean) As Boolean
        UpdateGestione = True

        Dim AggiornamContabilita As Boolean = False

        Try
            PrenDaCancellare = ""
            Dim StatoBil As String = ""

            If Not IsDate(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) Or Not IsDate(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) Then
                Response.Write("<script>alert('Date periodo di gestione errate!');</script>")
                UpdateGestione = False
                Exit Function
            End If

            If par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) >= par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) Then
                Response.Write("<script>alert('Le date del periodo di gestione sono errate!');</script>")
                Exit Function
            End If

            ClearDisbledItDgv()
            Dim i As Integer = 0
            Dim di As DataGridItem
            'Dim t As TextBox
            Dim idVoce As String = ""
            Dim ConguaglioGP As String = ""
            Dim Preventivo As String = ""
            Dim Rata1 As String = ""
            Dim Rata2 As String = ""
            Dim Rata3 As String = ""
            Dim Rata4 As String = ""
            Dim Rata5 As String = ""
            Dim Rata6 As String = ""
            Dim Rata7 As String = ""
            Dim Rata8 As String = ""
            Dim Rata9 As String = ""
            Dim Rata10 As String = ""
            Dim Rata11 As String = ""
            Dim Rata12 As String = ""
            Dim Rata13 As String = ""
            Dim Rata14 As String = ""
            Dim Rata15 As String = ""
            Dim Rata16 As String = ""
            Dim Rata17 As String = ""
            Dim Rata18 As String = ""
            Dim Rata19 As String = ""
            Dim Rata20 As String = ""
            Dim Rata21 As String = ""
            Dim Rata22 As String = ""
            Dim Rata23 As String = ""
            Dim Rata24 As String = ""

            Dim SalvaSitPatr As Boolean = False

            '****************CONTROLLO DELLE RATE CHE SIANO SUCCESSIVE********************
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 2 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 3 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 4 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 5 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 6 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 7 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 8 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 9 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 10 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 11 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 12 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 13 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 14 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 15 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 16 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 17 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 18 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 19 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 20 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 21 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 22 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 23 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            If Not String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text) Then
                If par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) >= par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text) Then
                    Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLa data di scadenza della rata 24 è inferiore o uguale a quella precedente!');</script>")
                    UpdateGestione = False

                    Exit Function

                End If
            End If
            '*****************CONTROLLO DATE NON VUOTE PER N-RATE DEFINITO DALLA COMBO
            If ControlloDateRate() = False Then
                Response.Write("<script>alert('Impossibile completare l\'operazione\r\nLe date di scadenza delle rate sono obbligatorie!');</script>")
                UpdateGestione = False

                Exit Function

            End If


            '**************fine controllo date************************************************
            '*************CONTROLLO CHE LA SOMMA DELLE RATE SIA INFERIORE O UGUALE AL CONGUAGLIO + IL PREVENTIVO
            If ControlloRate() = False Then
                Response.Write("<script>alert('Impossibile completare l\'operazione\r\nVerificare gli importi delle rate!');</script>")
                UpdateGestione = False
                Exit Function

            End If
            '*************FINE CONTROLLO CHE LA SOMMA DELLE RATE SIA INFERIORE O UGUALE AL CONGUAGLIO + IL PREVENTIVO
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            If Not String.IsNullOrEmpty(Me.txtInizioGest.Text) AndAlso Not String.IsNullOrEmpty(Me.txtAnnoInizio.Text) AndAlso Not String.IsNullOrEmpty(Me.TxtFineGest.Text) AndAlso Not String.IsNullOrEmpty(Me.TxtAnnoFine.Text) Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE SET DATA_INIZIO=" & par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) & " ,DATA_FINE=" & par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) & " ,TIPO='" & Me.cmbTipoGest.SelectedValue & "' ,N_RATE=" & Me.cmbNumRate.SelectedValue & "," _
                & " RATA_1_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") & "," _
                & " RATA_2_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") & "," _
                & " RATA_3_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") & "," _
                & " RATA_4_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") & "," _
                & " RATA_5_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") & "," _
                & " RATA_6_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") & "," _
                & " RATA_7_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") & "," _
                & " RATA_8_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") & "," _
                & " RATA_9_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") & "," _
                & " RATA_10_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") & "," _
                & " RATA_11_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") & "," _
                & " RATA_12_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") & "," _
                & " RATA_13_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") & "," _
                & " RATA_14_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") & "," _
                & " RATA_15_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") & "," _
                & " RATA_16_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") & "," _
                & " RATA_17_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") & "," _
                & " RATA_18_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") & "," _
                & " RATA_19_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") & "," _
                & " RATA_20_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") & "," _
                & " RATA_21_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") & "," _
                & " RATA_22_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") & "," _
                & " RATA_23_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") & "," _
                & " RATA_24_SCAD=" & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") & "," _
                & " NOTE = '" & par.PulisciStrSql(Me.txtNote.Text) & " 'WHERE ID = " & vIdGestione
                par.cmd.ExecuteNonQuery()
                UpdateDateScadDett()
            Else
                '****OBBLIGATORIETA' DEL PERIODO DI GESTIONE ANCHE IN AGGIORNAMENTO DEI DATI (UPDATE)
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!');</script>")
                Exit Function
                UpdateGestione = False

            End If
            '****************UPDATE DEI DATI PRESENTI NELLA DATAGRID******************
            Dim nrata As Integer = 0

            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                If di.Cells(par.IndDGC(DataGridVociSpesa, "TIPO_RIGA")).Text = "1" Then
                    idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                    ConguaglioGP = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Text
                    Preventivo = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Text
                    If Preventivo <> "" Then
                        SalvaSitPatr = True
                    End If
                    Rata1 = CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text
                    Rata2 = CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text
                    Rata3 = CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text
                    Rata4 = CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text
                    Rata5 = CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text
                    Rata6 = CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text
                    Rata7 = CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text
                    Rata8 = CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text
                    Rata9 = CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text
                    Rata10 = CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text
                    Rata11 = CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text
                    Rata12 = CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text
                    Rata13 = CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text
                    Rata14 = CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text
                    Rata15 = CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text
                    Rata16 = CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text
                    Rata17 = CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text
                    Rata18 = CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text
                    Rata19 = CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text
                    Rata20 = CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text
                    Rata21 = CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text
                    Rata22 = CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text
                    Rata23 = CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text
                    Rata24 = CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT SET CONGUAGLIO_GP = " & par.IfEmpty(par.VirgoleInPunti(ConguaglioGP.Replace(".", "")), "0") _
                                        & ",PREVENTIVO=" & par.IfEmpty(par.VirgoleInPunti(Preventivo.Replace(".", "")), "0") & " " _
                                        & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & ""
                    par.cmd.ExecuteNonQuery()
                    nrata = 1
                    '                If par.IfEmpty(Rata1, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then

                    If par.IfEmpty(Rata1, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 1, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null"))
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata1.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 1"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata1, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 1)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 1"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If

                    End If
                    If par.IfEmpty(Rata2, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 2, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata2.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 2"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata2, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 2)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 2"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata3, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 3, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata3.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 3"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata3, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 3)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 3"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata4, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 4, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata4.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 4"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata4, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 4)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 4"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata5, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 5, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata5.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 5"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata5, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 5)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 5"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata6, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 6, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata6.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 6"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata6, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 6)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 6"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata7, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 7, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata7.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 7"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata7, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 7)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 7"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata8, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 8, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata8.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 8"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata8, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 8)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 8"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata9, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 9, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata9.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 9"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata9, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 9)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 9"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata10, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 10, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata10.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 10"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata10, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 10)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 10"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata11, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 11, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata11.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 11"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata11, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 11)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 11"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata12, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 12, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata12.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 12"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata12, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 12)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 12"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata13, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 13, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata13.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 13"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata13, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 13)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 13"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata14, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 14, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata14.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 14"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata14, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 14)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 14"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata15, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 15, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata15.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 15"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata15, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 15)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 15"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata16, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 16, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata16.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 16"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata16, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 16)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 16"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata17, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 17, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata17.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 17"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata17, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 17)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 17"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata18, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 18, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata18.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 18"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata18, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 18)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 18"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata19, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 19, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata19.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 19"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata19, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 19)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 19"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata20, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 20, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata20.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 20"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata20, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 20)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 20"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata21, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 21, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata21.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 21"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata21, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 21)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 21"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata22, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 22, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata22.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 22"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata22, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 22)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 22"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata23, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 23, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata23.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 23"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata23, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 23)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 23"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata24, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 24, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata24.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 24"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata24, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 24)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 24"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                End If
            Next
            '******************************FINE INSERIMENTO DATI DATAGRID **********************


            '******************************UPDATE INFO MOROSITA*********************************

            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                If di.Cells(par.IndDGC(DataGridVociSpesa, "TIPO_RIGA")).Text = "2" Then
                    idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                    ConguaglioGP = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Text
                    Preventivo = CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Text
                    If Preventivo <> "" Then
                        SalvaSitPatr = True
                    End If
                    Rata1 = CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text
                    Rata2 = CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text
                    Rata3 = CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text
                    Rata4 = CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text
                    Rata5 = CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text
                    Rata6 = CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text
                    Rata7 = CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text
                    Rata8 = CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text
                    Rata9 = CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text
                    Rata10 = CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text
                    Rata11 = CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text
                    Rata12 = CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text
                    Rata13 = CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text
                    Rata14 = CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text
                    Rata15 = CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text
                    Rata16 = CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text
                    Rata17 = CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text
                    Rata18 = CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text
                    Rata19 = CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text
                    Rata20 = CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text
                    Rata21 = CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text
                    Rata22 = CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text
                    Rata23 = CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text
                    Rata24 = CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT SET CONGUAGLIO_GP = " & par.IfEmpty(par.VirgoleInPunti(ConguaglioGP.Replace(".", "")), "0") _
                                        & ",PREVENTIVO=" & par.IfEmpty(par.VirgoleInPunti(Preventivo.Replace(".", "")), "0") & " " _
                                        & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & ""
                    par.cmd.ExecuteNonQuery()
                    nrata = 1
                    '                If par.IfEmpty(Rata1, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then

                    If par.IfEmpty(Rata1, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 1, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null"))
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata1.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 1"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata1, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 1)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 1"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If

                    End If
                    If par.IfEmpty(Rata2, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 2, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata2.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 2"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata2, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 2)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 2"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata3, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 3, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata3.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 3"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata3, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 3)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 3"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata4, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 4, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata4.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 4"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata4, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 4)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 4"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata5, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 5, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata5.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 5"
                        par.cmd.ExecuteNonQuery()
                        nrata = nrata + 1
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata5, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 5)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 5"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata6, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 6, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata6.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 6"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata6, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 6)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 6"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata7, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 7, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata7.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 7"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata7, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 7)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 7"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata8, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 8, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata8.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 8"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata8, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 8)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 8"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata9, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 9, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata9.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 9"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata9, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 9)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 9"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata10, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 10, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata10.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 10"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata10, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 10)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 10"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata11, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 11, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata11.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 11"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata11, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 11)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 11"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata12, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 12, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata12.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 12"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata12, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 12)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 12"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata13, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 13, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata13.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 13"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata13, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 13)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 13"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata14, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 14, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata14.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 14"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata14, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 14)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 14"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata15, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 15, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata15.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 15"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata15, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 15)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 15"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata16, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 16, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata16.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 16"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata16, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 16)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 16"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata17, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 17, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata17.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 17"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata17, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 17)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 17"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata18, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 18, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata18.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 18"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata18, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 18)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 18"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata19, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 19, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata19.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 19"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata19, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 19)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 19"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata20, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 20, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata20.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 20"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata20, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 20)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 20"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata21, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 21, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata21.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 21"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata21, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 21)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 21"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata22, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 22, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata22.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 22"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata22, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 22)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 22"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata23, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 23, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata23.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 23"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata23, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 23)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 23"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                    If par.IfEmpty(Rata24, 0) <> 0 And nrata <= Me.cmbNumRate.SelectedValue.ToString Then
                        EsisteDetScad(idVoce, 24, par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null"))

                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET IMPORTO = " & par.IfEmpty(par.VirgoleInPunti(Rata24.Replace(".", "")), "0") _
                                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 24"
                        par.cmd.ExecuteNonQuery()
                        AggiornamContabilita = True
                    Else
                        If par.IfEmpty(Rata24, 0) = 0 And nrata > Me.cmbNumRate.SelectedValue.ToString Then
                            PrenotDaEliminare(idVoce, 24)
                            par.cmd.CommandText = "delete from SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
                                                & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA = 24"
                            par.cmd.ExecuteNonQuery()
                            nrata = nrata + 1

                        End If
                    End If
                End If

            Next

            If Not String.IsNullOrEmpty(PrenDaCancellare) Then
                par.cmd.CommandText = "delete from siscom_mi.prenotazioni " _
                    & "WHERE ID IN (" & PrenDaCancellare & ")"
                par.cmd.ExecuteNonQuery()

                AggiornamContabilita = True
            End If
            '***************13/09/2011 modifica per Cambio numero rate elimino le prenotazioni della rata eliminata
            If Me.cmbNumRate.SelectedValue < nRate.Value Then
                'par.cmd.CommandText = "delete from siscom_mi.prenotazioni where id in " _
                '                    & "(select id_prenotazione from siscom_mi.cond_gestione_dett_scad where " _
                '                    & "id_gestione = " & vIdGestione & " and N_rata > = " & nRate.Value & " and id_prenotazione is not null )"

                par.cmd.CommandText = "delete from siscom_mi.prenotazioni where id in ( " _
                                    & "SELECT id_prenotazione " _
                                    & "FROM siscom_mi.cond_gestione_dett_scad " _
                                    & "WHERE id_gestione = " & vIdGestione _
                                    & " AND n_rata > = " & nRate.Value _
                                    & " AND id_prenotazione IS NOT NULL)"

                'par.cmd.CommandText = "UPDATE siscom_mi.prenotazioni " _
                '                    & "SET id_stato = 0," _
                '                    & "importo_approvato = 0, " _
                '                    & "importo_prenotato = 0, " _
                '                    & "data_scadenza = '' " _
                '                    & "WHERE ID IN ( " _
                '                    & "SELECT id_prenotazione " _
                '                    & "FROM siscom_mi.cond_gestione_dett_scad " _
                '                    & "WHERE id_gestione = " & vIdGestione _
                '                    & " AND n_rata > = " & nRate.Value _
                '                    & " AND id_prenotazione IS NOT NULL)"
                par.cmd.ExecuteNonQuery()

                AggiornamContabilita = True

            End If

            '******************INSERIMENTO IN COND_UI_GESTIONE DI TUTTO QUELLO CHE è PRESENTE IN COND_UI PER IL CONDOMINIO
            If AggSitPat.Value = 1 Then
                If SalvaSitPatr = True Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_UI_PREVENTIVI WHERE ID_GESTIONE = " & vIdGestione
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO")
                    Dim dt As New Data.DataTable
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        For Each row As System.Data.DataRow In dt.Rows
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_UI_PREVENTIVI(ID_GESTIONE, ID_UI, POSIZIONE_BILANCIO," _
                            & " MIL_PRO, MIL_ASC, MIL_COMPRO, MIL_GEST,MIL_RISC,NOTE,ADDEBITO_SINGOLO,ID_INTESTARIO)" _
                            & " VALUES (" & vIdGestione & ", " & row.Item("ID_UI") & ", '" & par.IfNull(row.Item("POSIZIONE_BILANCIO"), "") & "'," _
                            & " " & par.VirgoleInPunti(row.Item("MIL_PRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_ASC")) & ", " & par.VirgoleInPunti(row.Item("MIL_COMPRO")) & ", " & par.VirgoleInPunti(row.Item("MIL_GEST")) & ", " & par.VirgoleInPunti(row.Item("MIL_RISC")) & ", '" & par.PulisciStrSql(par.IfNull(row.Item("NOTE"), "")) & "', " _
                            & " " & par.VirgoleInPunti(row.Item("ADDEBITO_SINGOLO")) & ", " & par.IfNull(row.Item("ID_INTESTARIO"), "Null") & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                    End If
                End If
                AggSitPat.Value = 0
            End If
            '******************FINE INSERIMENTO COND_UI_GESTIONE


            Session("MODIFYMODAL") = 1
            Me.txtModificato.Value = 0
            Me.txtSalvato.Value = 1
            If msgEsito = True Then
                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

            End If

            If AggiornamContabilita = True Then
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','AGGIORNAMENTO PREVENTIVO CONTABILITA CONDOMINIO')"
                par.cmd.ExecuteNonQuery()
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            UpdateGestione = False
        End Try
    End Function
    Protected Sub cmbNumRate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNumRate.SelectedIndexChanged

        EnableDisableDgv()

    End Sub
    Private Function ControlloRate() As Boolean
        Dim i As Integer = 0
        Dim di As DataGridItem
        'Dim t As TextBox
        Dim idVoce As String = ""
        Dim ConguaglioGP As String = ""
        Dim Preventivo As String = ""
        Dim Rata1 As String = ""
        Dim Rata2 As String = ""
        Dim Rata3 As String = ""
        Dim Rata4 As String = ""
        Dim Rata5 As String = ""
        Dim Rata6 As String = ""
        Dim Rata7 As String = ""
        Dim Rata8 As String = ""
        Dim Rata9 As String = ""
        Dim Rata10 As String = ""
        Dim Rata11 As String = ""
        Dim Rata12 As String = ""
        Dim Rata13 As String = ""
        Dim Rata14 As String = ""
        Dim Rata15 As String = ""
        Dim Rata16 As String = ""
        Dim Rata17 As String = ""
        Dim Rata18 As String = ""
        Dim Rata19 As String = ""
        Dim Rata20 As String = ""
        Dim Rata21 As String = ""
        Dim Rata22 As String = ""
        Dim Rata23 As String = ""
        Dim Rata24 As String = ""

        '****************INSERIMENTO DEI DATI PRESENTI NELLA DATAGRID******************
        For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
            di = Me.DataGridVociSpesa.Items(i)
            If di.Cells(par.IndDGC(DataGridVociSpesa, "TIPO_RIGA")).Text = "1" Then
                idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                ConguaglioGP = par.IfEmpty(CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Text, 0)
                Preventivo = par.IfEmpty(CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Text, 0)
                Rata1 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text, 0)
                Rata2 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text, 0)
                Rata3 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text, 0)
                Rata4 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text, 0)
                Rata5 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text, 0)
                Rata6 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text, 0)
                Rata7 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text, 0)
                Rata8 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text, 0)
                Rata9 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text, 0)
                Rata10 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text, 0)
                Rata11 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text, 0)
                Rata12 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text, 0)
                Rata13 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text, 0)
                Rata14 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text, 0)
                Rata15 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text, 0)
                Rata16 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text, 0)
                Rata17 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text, 0)
                Rata18 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text, 0)
                Rata19 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text, 0)
                Rata20 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text, 0)
                Rata21 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text, 0)
                Rata22 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text, 0)
                Rata23 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text, 0)
                Rata24 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text, 0)
                If (CDec(ConguaglioGP) + CDec(Preventivo)) >= 0 Then
                    If Math.Round(CDec(ConguaglioGP) + CDec(Preventivo), 2) < (CDec(Rata1) + CDec(Rata2) + CDec(Rata3) + CDec(Rata4) + CDec(Rata5) + CDec(Rata6) + CDec(Rata7) + CDec(Rata8) + CDec(Rata9) + CDec(Rata10) + CDec(Rata11) + CDec(Rata12) + CDec(Rata13) + CDec(Rata14) + CDec(Rata15) + CDec(Rata16) + CDec(Rata17) + CDec(Rata18) + CDec(Rata19) + CDec(Rata20) + CDec(Rata21) + CDec(Rata22) + CDec(Rata23) + CDec(Rata24)) Then
                        ControlloRate = False
                        Exit Function
                    Else
                        ControlloRate = True
                    End If
                    If (CDec(ConguaglioGP) + CDec(Preventivo)) > 0 And (CDec(Rata1) + CDec(Rata2) + CDec(Rata3) + CDec(Rata4) + CDec(Rata5) + CDec(Rata6) + CDec(Rata7) + CDec(Rata8) + CDec(Rata9) + CDec(Rata10) + CDec(Rata11) + CDec(Rata12) + CDec(Rata13) + CDec(Rata14) + CDec(Rata15) + CDec(Rata16) + CDec(Rata17) + CDec(Rata18) + CDec(Rata19) + CDec(Rata20) + CDec(Rata21) + CDec(Rata22) + CDec(Rata23) + CDec(Rata24)) <= 0 Then
                        ControlloRate = False
                        Exit Function
                    End If

                End If
            End If
        Next

        For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
            di = Me.DataGridVociSpesa.Items(i)
            If di.Cells(par.IndDGC(DataGridVociSpesa, "TIPO_RIGA")).Text = "2" Then
                idVoce = Me.DataGridVociSpesa.Items(i).Cells(0).Text
                ConguaglioGP = par.IfEmpty(CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Text, 0)
                Preventivo = par.IfEmpty(CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Text, 0)
                Rata1 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text, 0)
                Rata2 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text, 0)
                Rata3 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text, 0)
                Rata4 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text, 0)
                Rata5 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text, 0)
                Rata6 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text, 0)
                Rata7 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text, 0)
                Rata8 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text, 0)
                Rata9 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text, 0)
                Rata10 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text, 0)
                Rata11 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text, 0)
                Rata12 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text, 0)
                Rata13 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text, 0)
                Rata14 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text, 0)
                Rata15 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text, 0)
                Rata16 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text, 0)
                Rata17 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text, 0)
                Rata18 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text, 0)
                Rata19 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text, 0)
                Rata20 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text, 0)
                Rata21 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text, 0)
                Rata22 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text, 0)
                Rata23 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text, 0)
                Rata24 = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text, 0)
                If (CDec(ConguaglioGP) + CDec(Preventivo)) >= 0 Then
                    If Math.Round(CDec(ConguaglioGP) + CDec(Preventivo), 2) < (CDec(Rata1) + CDec(Rata2) + CDec(Rata3) + CDec(Rata4) + CDec(Rata5) + CDec(Rata6) + CDec(Rata7) + CDec(Rata8) + CDec(Rata9) + CDec(Rata10) + CDec(Rata11) + CDec(Rata12) + CDec(Rata13) + CDec(Rata14) + CDec(Rata15) + CDec(Rata16) + CDec(Rata17) + CDec(Rata18) + CDec(Rata19) + CDec(Rata20) + CDec(Rata21) + CDec(Rata22) + CDec(Rata23) + CDec(Rata24)) Then
                        ControlloRate = False
                        Exit Function
                    Else
                        ControlloRate = True
                    End If
                    If (CDec(ConguaglioGP) + CDec(Preventivo)) > 0 And (CDec(Rata1) + CDec(Rata2) + CDec(Rata3) + CDec(Rata4) + CDec(Rata5) + CDec(Rata6) + CDec(Rata7) + CDec(Rata8) + CDec(Rata9) + CDec(Rata10) + CDec(Rata11) + CDec(Rata12) + CDec(Rata13) + CDec(Rata14) + CDec(Rata15) + CDec(Rata16) + CDec(Rata17) + CDec(Rata18) + CDec(Rata19) + CDec(Rata20) + CDec(Rata21) + CDec(Rata22) + CDec(Rata23) + CDec(Rata24)) <= 0 Then
                        ControlloRate = False
                        Exit Function
                    End If

                End If
            End If
        Next


        '******************************FINE INSERIMENTO DATI DATAGRID**********************
    End Function

    Private Function ControlloDateRate() As Boolean
        ControlloDateRate = True
        Select Case Me.cmbNumRate.SelectedValue.ToString
            Case 1
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If

            Case 2
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 3
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 4
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 5
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 6
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 7
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 8
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 9
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 10
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 11
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 12
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 13
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 14
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 15
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 16
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 17
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 18
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 19
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 20
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 21
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 22
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 23
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
            Case 24
                If String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text) Or String.IsNullOrEmpty(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text) Then
                    ControlloDateRate = False
                    Response.Write("<script>alert('Non puoi procedere con le rate errate!');</script>")
                    Exit Function
                End If
        End Select
    End Function

    Private Sub EnableDisableDgv()
        Try
            If Me.cmbNumRate.SelectedValue < nRate.Value Then
                If NRata_Adeguata(Me.cmbNumRate.SelectedValue.ToString) = False Then
                    Response.Write("<script>alert('Impossibile modificare le rate!Sono stati emessi i pagamenti!');</script>")
                    Me.cmbNumRate.SelectedValue = nRate.Value
                    Exit Sub
                End If
            End If

            nRate.Value = Me.cmbNumRate.SelectedValue

            Dim i As Integer = 0
            Select Case Me.cmbNumRate.SelectedValue.ToString
                Case 1
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 2
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 3
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 4
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 5
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 6
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 7
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 8
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 9
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 10
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 11
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 12
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 13
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 14
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 15
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 16
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 17
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 18
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 19
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 20
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 21
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 22
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = True
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 23
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = True
                    Next

                Case 24
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = False
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = False
                    Next

                Case Else
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = False
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = False
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata1"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox).ReadOnly = False
                        CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox).ReadOnly = False
                    Next

            End Select

            Dim numeroRate As Integer = cmbNumRate.SelectedValue
            For iii As Integer = numeroRate To 23
                DataGridVociSpesa.Columns(par.IndDGC(DataGridVociSpesa, "RATA " & iii + 1 & "°")).Visible = False
            Next
            For jjj As Integer = numeroRate To 1 Step -1
                DataGridVociSpesa.Columns(par.IndDGC(DataGridVociSpesa, "RATA " & jjj & "°")).Visible = True
            Next

            AddJavascriptFunction()
            If vStato = "P1" Then
                SettaFrmReadOnlyConvalida()
            End If


            For Each elemento As DataGridItem In DataGridVociSpesa.Items
                For ind As Integer = 1 To cmbNumRate.SelectedValue
                    If CType(elemento.FindControl("txtRata" & ind), TextBox).ReadOnly = True Then
                        CType(elemento.FindControl("txtRata" & ind), TextBox).Enabled = False

                    End If
                Next
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub ClearDisbledItDgv()
        Try


            Dim i As Integer = 0
            Dim t As TextBox
            Select Case Me.cmbNumRate.SelectedValue.ToString
                Case 1
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata2"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If

                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If

                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next

                Case 2
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata3"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If

                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next

                Case 3
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata4"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next

                Case 4
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata5"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 5
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata6"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 6
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata7"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 7
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata8"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 8
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata9"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 9
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata10"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 10
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata11"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 11
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata12"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 12
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata13"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 13
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata14"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 14
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata15"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 15
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata16"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 16
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata17"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 17
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata18"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 18
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata19"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 19
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata20"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 20
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata21"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 21
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata22"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 22
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text = ""
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata23"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
                Case 23
                    CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text = ""
                    For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                        t = CType(Me.DataGridVociSpesa.Items(i).Cells(0).FindControl("txtRata24"), TextBox)
                        If t.ReadOnly = True Then
                            t.Text = ""
                        End If
                    Next
            End Select
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnConsuntivi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConsuntivi.Click
        If vIdGestione <> "" Then

            'If Request.QueryString("IDVISUAL") <> "0" Then
            '    'Me.txtModificato.Value = 1
            'End If

            'Dim txtscript As String = "<script>var dialogResults = window.showModalDialog('ConsGestione.aspx?IDCONDOMINIO= " _
            '                          & Request.QueryString("IDCONDOMINIO") & "&IDCON=" & vIdConnModale _
            '                          & "&IDGEST=" & vIdGestione & "&IDVISUAL=" & Request.QueryString("IDVISUAL") & "'," _
            '                          & "window, 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');" _
            '                          & "if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false))" _
            '                          & "{" _
            '                          & "document.getElementById('txtModificato').value = '1';" _
            '                          & "}</script>"""
            'Response.Write(txtscript)
            SommaColonne()
            'Else
            '    Response.Write("<script>alert('Salvare il preventivo prima di procedere!');</script>")
        End If

    End Sub

    Protected Sub btnSommatoria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSommatoria.Click
        SommaColonne()

    End Sub

    Private Sub SommaColonne()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem
            'Dim totRata1 As Decimal = 0
            'Dim totRata2 As Decimal = 0
            'Dim totRata3 As Decimal = 0
            'Dim totRata4 As Decimal = 0
            'Dim totRata5 As Decimal = 0
            'Dim totRata6 As Decimal = 0
            'Dim totRata7 As Decimal = 0
            'Dim totRata8 As Decimal = 0
            'Dim totRata9 As Decimal = 0
            'Dim totRata10 As Decimal = 0
            'Dim totRata11 As Decimal = 0
            'Dim totRata12 As Decimal = 0
            'Dim totRata13 As Decimal = 0
            'Dim totRata14 As Decimal = 0
            'Dim totRata15 As Decimal = 0
            'Dim totRata16 As Decimal = 0
            'Dim totRata17 As Decimal = 0
            'Dim totRata18 As Decimal = 0
            'Dim totRata19 As Decimal = 0
            'Dim totRata20 As Decimal = 0
            'Dim totRata21 As Decimal = 0
            'Dim totRata22 As Decimal = 0
            'Dim totRata23 As Decimal = 0
            'Dim totRata24 As Decimal = 0
            totaleRata1.Value = 0
            totaleRata2.Value = 0
            totaleRata3.Value = 0
            totaleRata4.Value = 0
            totaleRata5.Value = 0
            totaleRata6.Value = 0
            totaleRata7.Value = 0
            totaleRata8.Value = 0
            totaleRata9.Value = 0
            totaleRata10.Value = 0
            totaleRata11.Value = 0
            totaleRata12.Value = 0
            totaleRata13.Value = 0
            totaleRata14.Value = 0
            totaleRata15.Value = 0
            totaleRata16.Value = 0
            totaleRata17.Value = 0
            totaleRata18.Value = 0
            totaleRata19.Value = 0
            totaleRata20.Value = 0
            totaleRata21.Value = 0
            totaleRata22.Value = 0
            totaleRata23.Value = 0
            totaleRata24.Value = 0
            totCongPrec.Value = 0
            totPreventivo.Value = 0

            For i = 0 To Me.DataGridVociSpesaRiepilogo.Items.Count - 1
                di = Me.DataGridVociSpesaRiepilogo.Items(i)
                If di.Cells(par.IndDGC(DataGridVociSpesaRiepilogo, "FL_TOTALE")).Text = "1" Then
                    totCongPrec.Value = CDec(totCongPrec.Value) + CDec(par.IfEmpty(CType(di.Cells(1).FindControl("txtCongPrec"), TextBox).Text, 0))
                    totPreventivo.Value = CDec(totPreventivo.Value) + CDec(par.IfEmpty(CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text, 0))
                End If
            Next
            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                If di.Cells(par.IndDGC(DataGridVociSpesa, "FL_TOTALE")).Text = "1" Then

                    totaleRata1.Value = CDec(totaleRata1.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata1"), TextBox).Text, 0))
                    totaleRata2.Value = CDec(totaleRata2.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata2"), TextBox).Text, 0))
                    totaleRata3.Value = CDec(totaleRata3.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata3"), TextBox).Text, 0))
                    totaleRata4.Value = CDec(totaleRata4.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata4"), TextBox).Text, 0))
                    totaleRata5.Value = CDec(totaleRata5.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata5"), TextBox).Text, 0))
                    totaleRata6.Value = CDec(totaleRata6.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata6"), TextBox).Text, 0))
                    totaleRata7.Value = CDec(totaleRata7.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata7"), TextBox).Text, 0))
                    totaleRata8.Value = CDec(totaleRata8.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata8"), TextBox).Text, 0))
                    totaleRata9.Value = CDec(totaleRata9.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata9"), TextBox).Text, 0))
                    totaleRata10.Value = CDec(totaleRata10.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata10"), TextBox).Text, 0))
                    totaleRata11.Value = CDec(totaleRata11.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata11"), TextBox).Text, 0))
                    totaleRata12.Value = CDec(totaleRata12.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata12"), TextBox).Text, 0))
                    totaleRata13.Value = CDec(totaleRata13.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata13"), TextBox).Text, 0))
                    totaleRata14.Value = CDec(totaleRata14.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata14"), TextBox).Text, 0))
                    totaleRata15.Value = CDec(totaleRata15.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata15"), TextBox).Text, 0))
                    totaleRata16.Value = CDec(totaleRata16.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata16"), TextBox).Text, 0))
                    totaleRata17.Value = CDec(totaleRata17.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata17"), TextBox).Text, 0))
                    totaleRata18.Value = CDec(totaleRata18.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata18"), TextBox).Text, 0))
                    totaleRata19.Value = CDec(totaleRata19.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata19"), TextBox).Text, 0))
                    totaleRata20.Value = CDec(totaleRata20.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata20"), TextBox).Text, 0))
                    totaleRata21.Value = CDec(totaleRata21.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata21"), TextBox).Text, 0))
                    totaleRata22.Value = CDec(totaleRata22.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata22"), TextBox).Text, 0))
                    totaleRata23.Value = CDec(totaleRata23.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata23"), TextBox).Text, 0))
                    totaleRata24.Value = CDec(totaleRata24.Value) + CDec(par.IfEmpty(CType(di.Cells(3).FindControl("txtRata24"), TextBox).Text, 0))
                End If
            Next

            'Me.totCongPrec.Text = Format(CDec(Me.totCongPrec.Text), "##,##0.00")
            'Me.totPreventivo.Text = Format(CDec(Me.totPreventivo.Text), "##,##0.00")
            'Me.totRata1.Text = Format(CDec(Me.totRata1.Text), "##,##0.00")
            'Me.totRata2.Text = Format(CDec(Me.totRata2.Text), "##,##0.00")
            'Me.totRata3.Text = Format(CDec(Me.totRata3.Text), "##,##0.00")
            'Me.totRata4.Text = Format(CDec(Me.totRata4.Text), "##,##0.00")
            'Me.totRata5.Text = Format(CDec(Me.totRata5.Text), "##,##0.00")
            'Me.totRata6.Text = Format(CDec(Me.totRata6.Text), "##,##0.00")
            'Me.totRata7.Text = Format(CDec(Me.totRata7.Text), "##,##0.00")
            'Me.totRata8.Text = Format(CDec(Me.totRata8.Text), "##,##0.00")
            'Me.totRata9.Text = Format(CDec(Me.totRata9.Text), "##,##0.00")
            'Me.totRata10.Text = Format(CDec(Me.totRata10.Text), "##,##0.00")
            'Me.totRata11.Text = Format(CDec(Me.totRata11.Text), "##,##0.00")
            'Me.totRata12.Text = Format(CDec(Me.totRata12.Text), "##,##0.00")
            'Me.totRata13.Text = Format(CDec(Me.totRata13.Text), "##,##0.00")
            'Me.totRata14.Text = Format(CDec(Me.totRata14.Text), "##,##0.00")
            'Me.totRata15.Text = Format(CDec(Me.totRata15.Text), "##,##0.00")
            'Me.totRata16.Text = Format(CDec(Me.totRata16.Text), "##,##0.00")
            'Me.totRata17.Text = Format(CDec(Me.totRata17.Text), "##,##0.00")
            'Me.totRata18.Text = Format(CDec(Me.totRata18.Text), "##,##0.00")
            'Me.totRata19.Text = Format(CDec(Me.totRata19.Text), "##,##0.00")
            'Me.totRata20.Text = Format(CDec(Me.totRata20.Text), "##,##0.00")
            'Me.totRata21.Text = Format(CDec(Me.totRata21.Text), "##,##0.00")
            'Me.totRata22.Text = Format(CDec(Me.totRata22.Text), "##,##0.00")
            'Me.totRata23.Text = Format(CDec(Me.totRata23.Text), "##,##0.00")
            'Me.totRata24.Text = Format(CDec(Me.totRata24.Text), "##,##0.00")


            Dim indice As Integer = 0
            Dim indiceTotale As Integer = 0
            For Each elemento As DataGridItem In DataGridVociSpesaRiepilogo.Items
                If elemento.Cells(par.IndDGC(DataGridVociSpesaRiepilogo, "DESCRIZIONE")).Text.ToString = "TOTALE" Then
                    indiceTotale = indice
                End If
                indice += 1
            Next


            CType(DataGridVociSpesaRiepilogo.Items(indiceTotale).FindControl("txtCongPrec"), TextBox).Text = Format(CDec(totCongPrec.Value), "##,##0.00")
            CType(DataGridVociSpesaRiepilogo.Items(indiceTotale).FindControl("txtPreventivo"), TextBox).Text = Format(CDec(totPreventivo.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata1"), TextBox).Text = Format(CDec(totaleRata1.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata2"), TextBox).Text = Format(CDec(totaleRata2.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata3"), TextBox).Text = Format(CDec(totaleRata3.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata4"), TextBox).Text = Format(CDec(totaleRata4.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata5"), TextBox).Text = Format(CDec(totaleRata5.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata6"), TextBox).Text = Format(CDec(totaleRata6.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata7"), TextBox).Text = Format(CDec(totaleRata7.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata8"), TextBox).Text = Format(CDec(totaleRata8.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata9"), TextBox).Text = Format(CDec(totaleRata9.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata10"), TextBox).Text = Format(CDec(totaleRata10.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata11"), TextBox).Text = Format(CDec(totaleRata11.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata12"), TextBox).Text = Format(CDec(totaleRata12.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata13"), TextBox).Text = Format(CDec(totaleRata13.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata14"), TextBox).Text = Format(CDec(totaleRata14.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata15"), TextBox).Text = Format(CDec(totaleRata15.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata16"), TextBox).Text = Format(CDec(totaleRata16.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata17"), TextBox).Text = Format(CDec(totaleRata17.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata18"), TextBox).Text = Format(CDec(totaleRata18.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata19"), TextBox).Text = Format(CDec(totaleRata19.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata20"), TextBox).Text = Format(CDec(totaleRata20.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata21"), TextBox).Text = Format(CDec(totaleRata21.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata22"), TextBox).Text = Format(CDec(totaleRata22.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata23"), TextBox).Text = Format(CDec(totaleRata23.Value), "##,##0.00")
            CType(DataGridVociSpesa.Items(indiceTotale).FindControl("txtRata24"), TextBox).Text = Format(CDec(totaleRata24.Value), "##,##0.00")

            Dim numRate As Integer = cmbNumRate.SelectedValue
            For ii As Integer = numRate + 1 To 24
                CType(DataGridVociSpesa.Items(DataGridVociSpesa.Items.Count - 2).FindControl("txtRata" & ii), TextBox).Text = ""
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
                If i = 0 Then
                    'date
                    CType(di.Cells(3).FindControl("txtRata1"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata2"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata3"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata4"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata5"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata6"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata7"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata8"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata9"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata10"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata11"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata12"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata13"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata14"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata15"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata16"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata17"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata18"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata19"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata20"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata21"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata22"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata23"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata24"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    CType(di.Cells(3).FindControl("txtRata1"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata2"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata3"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata4"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata5"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata6"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata7"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata8"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata9"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata10"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata11"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata12"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata13"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata14"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata15"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata16"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata17"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata18"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata19"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata20"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata21"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata22"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata23"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                    CType(di.Cells(3).FindControl("txtRata24"), TextBox).Attributes.Add("onblur", "javascript:CDataPrec(event,this);")
                Else
                    'importi
                    CType(di.Cells(3).FindControl("txtRata1"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                    CType(di.Cells(3).FindControl("txtRata2"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata3"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata4"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata5"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata6"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata7"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata8"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata9"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata10"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata11"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata12"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata13"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata14"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata15"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata16"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata17"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata18"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata19"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata20"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata21"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata22"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata23"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("txtRata24"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                    CType(di.Cells(3).FindControl("Validator1"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator2"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator3"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator4"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator5"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator6"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator7"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator8"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator9"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator10"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator11"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator12"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator13"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator14"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator15"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator16"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator17"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator18"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator19"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator20"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator21"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator22"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator23"), RegularExpressionValidator).Enabled = False
                    CType(di.Cells(3).FindControl("Validator24"), RegularExpressionValidator).Enabled = False

                End If
            Next


            For i = 0 To Me.DataGridVociSpesaRiepilogo.Items.Count - 1
                di = Me.DataGridVociSpesaRiepilogo.Items(i)
                If i > 0 Then
                    'importi
                    CType(di.Cells(3).FindControl("txtCongPrec"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                    CType(di.Cells(3).FindControl("txtPreventivo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this); ")
                End If
            Next

            CType(DataGridVociSpesaRiepilogo.Items(0).FindControl("txtCongPrec"), TextBox).Enabled = False
            CType(DataGridVociSpesaRiepilogo.Items(0).FindControl("txtPreventivo"), TextBox).Enabled = False

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub SettaFrmReadOnly()
        Try
            Dim i As Integer = 0
            Dim di As DataGridItem

            For i = 0 To Me.DataGridVociSpesaRiepilogo.Items.Count - 1
                di = Me.DataGridVociSpesaRiepilogo.Items(i)
                CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtCongPrec"), TextBox).Enabled = False
                CType(DataGridVociSpesaRiepilogo.Items(i).FindControl("txtPreventivo"), TextBox).Enabled = False
            Next
            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)
                CType(DataGridVociSpesa.Items(i).FindControl("txtCongPrec"), TextBox).Enabled = False
                CType(DataGridVociSpesa.Items(i).FindControl("txtPreventivo"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata1"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata2"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata3"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata4"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata5"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata6"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata7"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata8"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata9"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata10"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata11"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata12"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata13"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata14"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata15"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata16"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata17"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata18"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata19"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata20"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata21"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata22"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata23"), TextBox).Enabled = False
                CType(di.Cells(3).FindControl("txtRata24"), TextBox).Enabled = False
            Next
            Me.txtNote.Enabled = False
            Me.txtInizioGest.ReadOnly = True
            Me.TxtFineGest.ReadOnly = True
            Me.txtAnnoInizio.ReadOnly = True
            Me.TxtAnnoFine.ReadOnly = True
            Me.cmbNumRate.Enabled = False
            Me.cmbTipoGest.Enabled = False
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
            Me.btnSalvaCambioAmm.Visible = False
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub SettaFrmReadOnlyConvalida()

        Try
            '******************PEPPE MODIFY ON 09/09/2011****************
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT DISTINCT n_rata,(SELECT id_pagamento FROM siscom_mi.prenotazioni WHERE ID = id_prenotazione) AS id_pagamento FROM siscom_mi.COND_GESTIONE_DETT_SCAD " _
                                & "WHERE id_gestione = " & vIdGestione & " AND id_prenotazione IS NOT NULL"

            Dim lstRate As New System.Collections.Generic.SortedList(Of Integer, Integer)

            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)

            For Each r As Data.DataRow In dt.Rows
                If par.IfNull(r.Item("ID_PAGAMENTO"), 0) <> 0 Then
                    If lstRate.ContainsKey(r.Item("N_RATA")) Then
                    Else
                        lstRate.Add(r.Item("N_RATA"), r.Item("N_RATA"))

                    End If
                End If
            Next

            Dim i As Integer = 0
            Dim di As DataGridItem

            For i = 0 To Me.DataGridVociSpesa.Items.Count - 1
                di = Me.DataGridVociSpesa.Items(i)

                If lstRate.ContainsKey(1) Then
                    CType(di.Cells(3).FindControl("txtRata1"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(2) Then
                    CType(di.Cells(3).FindControl("txtRata2"), TextBox).ReadOnly = True
                End If
                If lstRate.ContainsKey(3) Then
                    CType(di.Cells(3).FindControl("txtRata3"), TextBox).ReadOnly = True
                End If
                If lstRate.ContainsKey(4) Then
                    CType(di.Cells(3).FindControl("txtRata4"), TextBox).ReadOnly = True
                End If
                If lstRate.ContainsKey(5) Then
                    CType(di.Cells(3).FindControl("txtRata5"), TextBox).ReadOnly = True
                End If
                If lstRate.ContainsKey(6) Then
                    CType(di.Cells(3).FindControl("txtRata6"), TextBox).ReadOnly = True
                End If
                If lstRate.ContainsKey(7) Then
                    CType(di.Cells(3).FindControl("txtRata7"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(8) Then
                    CType(di.Cells(3).FindControl("txtRata8"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(9) Then
                    CType(di.Cells(3).FindControl("txtRata9"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(10) Then
                    CType(di.Cells(3).FindControl("txtRata10"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(11) Then
                    CType(di.Cells(3).FindControl("txtRata11"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(12) Then
                    CType(di.Cells(3).FindControl("txtRata12"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(13) Then
                    CType(di.Cells(3).FindControl("txtRata13"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(14) Then
                    CType(di.Cells(3).FindControl("txtRata14"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(15) Then
                    CType(di.Cells(3).FindControl("txtRata15"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(16) Then
                    CType(di.Cells(3).FindControl("txtRata16"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(17) Then
                    CType(di.Cells(3).FindControl("txtRata17"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(18) Then
                    CType(di.Cells(3).FindControl("txtRata18"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(19) Then
                    CType(di.Cells(3).FindControl("txtRata19"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(20) Then
                    CType(di.Cells(3).FindControl("txtRata20"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(21) Then
                    CType(di.Cells(3).FindControl("txtRata21"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(22) Then
                    CType(di.Cells(3).FindControl("txtRata22"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(23) Then
                    CType(di.Cells(3).FindControl("txtRata23"), TextBox).ReadOnly = True
                End If

                If lstRate.ContainsKey(24) Then
                    CType(di.Cells(3).FindControl("txtRata24"), TextBox).ReadOnly = True
                End If
            Next

            Me.txtNote.Enabled = True
            Me.txtInizioGest.ReadOnly = True
            Me.TxtFineGest.ReadOnly = True
            Me.txtAnnoInizio.ReadOnly = True
            Me.TxtAnnoFine.ReadOnly = True
            Me.cmbNumRate.Enabled = True
            Me.cmbTipoGest.Enabled = False

            If lstRate.ContainsKey(1) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(2) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(3) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(4) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(5) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(6) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(7) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(8) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(9) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(10) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(11) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(12) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(13) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(14) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(15) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(16) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(17) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(18) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(19) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(20) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(21) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(22) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(23) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).ReadOnly = True
            End If
            If lstRate.ContainsKey(24) Then
                CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).ReadOnly = True
            End If

            'Me.btnConvalida.Visible = False
            'Me.txtMorCongPrec.ReadOnly = True
            'Me.txtMorPrevent.ReadOnly = True

            'Me.btnConvalida.Visible = False
            'Me.txtMorCongPrec.ReadOnly = True
            'Me.txtMorPrevent.ReadOnly = True

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnSituazPatr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSituazPatr.Click
        'If vIdGestione <> "" Then
        '    Response.Write("<script>window.open('RptSituazPat.aspx?IDGESTIONE= " & vIdGestione & "&CHIAMA=PREV&IDCONDOMINIO=" & Request.QueryString("IDCONDOMINIO") & "&IDCON=" & vIdConnModale & "','RptInquilini','');</script>")
        'Else
        '    Response.Write("<script>alert('Salvare il preventivo prima di procedere!');</script>")
        'End If
    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDec(v), Precision)
        End If
    End Function

    Protected Sub btnLiberiAbusivi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnLiberiAbusivi.Click
        'If Request.QueryString("IDVISUAL") <> "0" Then
        '    Me.txtModificato.Value = 1
        'End If
        'If vIdGestione <> "" Then
        '    Response.Write("<script>window.showModalDialog('LiberiAbusivi.aspx?IDCONDOMINIO= " & Request.QueryString("IDCONDOMINIO") & "&IDCON=" & vIdConnModale & "&IDGEST=" & vIdGestione & "&IDVISUAL=" & Request.QueryString("IDVISUAL") & "&CHIAMA=P',window, 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');</script>")
        'Else
        '    Response.Write("<script>alert('Salvare il preventivo prima di procedere!');</script>")

        'End If
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
                    '‘par.cmd.Transaction = par.myTrans
                    If Me.txtSalvato.Value = 0 Then
                        par.cmd.CommandText = "ROLLBACK TO SAVEPOINT PREVENTIVO"
                        par.cmd.ExecuteNonQuery()
                        Session.Item("MODIFYMODAL") = 0
                    End If



                    Response.Write("<script>window.close();</script>")
                Else


                End If

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Function ControllaTotali() As Boolean
        ControllaTotali = True

        SommaColonne()
        If CDec(Me.totaleRata1.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata2.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata3.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata4.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata5.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata6.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata7.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata8.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata9.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata10.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata11.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata12.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata13.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata14.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata15.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata16.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata17.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata18.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata19.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata20.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata21.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata22.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata23.Value < 0) Then
            ControllaTotali = False
        End If
        If CDec(Me.totaleRata24.Value < 0) Then
            ControllaTotali = False
        End If
        Return ControllaTotali
    End Function

    Function Convalida(ByVal visMsg As Boolean) As Boolean
        Dim esito As Boolean = False

        Try


            If ControlloDateRate() = True Then 'And ControllaImporti() = True Then
                If ControllaTotali() = False Then
                    Response.Write("<script>alert('Il totale di una scadenza è negativo!\nOperazione annullata!');</script>")
                    Return esito
                    Exit Function
                End If
                '


                Dim dict As New System.Collections.Generic.Dictionary(Of Integer, String)

                dict.Add(1, CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text)
                dict.Add(2, CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text)
                dict.Add(3, CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text)
                dict.Add(4, CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text)
                dict.Add(5, CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text)
                dict.Add(6, CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text)
                dict.Add(7, CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text)
                dict.Add(8, CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text)
                dict.Add(9, CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text)
                dict.Add(10, CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text)
                dict.Add(11, CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text)
                dict.Add(12, CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text)
                dict.Add(13, CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text)
                dict.Add(14, CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text)
                dict.Add(15, CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text)
                dict.Add(16, CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text)
                dict.Add(17, CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text)
                dict.Add(18, CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text)
                dict.Add(19, CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text)
                dict.Add(20, CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text)
                dict.Add(21, CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text)
                dict.Add(22, CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text)
                dict.Add(23, CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text)
                dict.Add(24, CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text)

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
                If vIdGestione <> "" Then
                    If UpdateGestione(False) = False Then
                        Return esito

                        Exit Function
                    End If
                Else
                    Response.Write("<script>alert('Salvare la gestione prima di Convalidare!');</script>")
                    Return esito
                    Exit Function

                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                Dim idVoce As String = ""
                Dim Id_Fornitore As String = ""
                Dim stato As String = ""
                Dim idStruttura As String = -1
                Dim FineEFApprovato As String = ""

                par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idStruttura = par.IfNull(lettore("ID_UFFICIO"), "-1")
                End If
                lettore.Close()

                If idStruttura = "-1" Then
                    Response.Write("<script>alert('Operazione è stata  annullata!');</script>")
                    Response.Write("<script>alert('Impossibile trovare la struttura di appartenenza dell\'operatore che sta effettuando l\' operazione');</script>")
                    Return esito
                    Exit Function
                End If

                '*************recupero id_fornitore
                par.cmd.CommandText = "SELECT FORNITORI.ID AS ID_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & Request.QueryString("IDCONDOMINIO") & ")"
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    Id_Fornitore = par.IfNull(myReader1("ID_FORNITORE"), "Null")
                Else
                    Response.Write("<script>alert('ATTENZIONE!NESSUN FORNITORE TROVATO PER IL CONDOMINIO!');</script>")
                    Response.Write("<script>alert('L\'operazione è stata  annullata!');</script>")
                    myReader1.Close()
                    Exit Function
                End If
                myReader1.Close()

                '########### ricavo la data fine dell'ultimo esercizio approvato per il quale sono conosciute le voci_pf ############
                par.cmd.CommandText = "SELECT fine FROM siscom_mi.T_ESERCIZIO_FINANZIARIO where id = (select id_esercizio_finanziario from siscom_mi.pf_main where id = " & idPianoF.Value & ")"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    FineEFApprovato = par.IfNull(lettore("FINE"), "")
                End If
                lettore.Close()

                If FineEFApprovato = "" Then
                    Response.Write("<script>alert('ATTENZIONE!NESSUN ESERCIZIO TROVATO PER PRENOTARE LE VOCI DEL PREVENTIVO!');</script>")
                    Response.Write("<script>alert('L\'operazione è stata  annullata!');</script>")
                End If



                Dim i As Integer = 1
                Dim idPrenotazione As String = "0"

                '*********************peppe modify 11/09/2011 SELEZIONO LE RATE GIà PAGATE PER LE QUALI NON è POSSIBILE MODIFICARE I DATI
                par.cmd.CommandText = "SELECT DISTINCT n_rata,(SELECT id_pagamento FROM siscom_mi.prenotazioni WHERE ID = id_prenotazione) AS id_pagamento " _
                                    & "FROM siscom_mi.COND_GESTIONE_DETT_SCAD " _
                                    & "WHERE id_gestione = " & vIdGestione & " AND id_prenotazione IS NOT NULL"

                Dim lstRate As New System.Collections.Generic.SortedList(Of Integer, Integer)

                Dim dt As New Data.DataTable
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                For Each r As Data.DataRow In dt.Rows
                    If par.IfNull(r.Item("ID_PAGAMENTO"), 0) <> 0 Then

                        lstRate.Add(r.Item("N_RATA"), r.Item("N_RATA"))

                    End If
                Next

                Dim voceProp As Boolean = False

                'PER IL NUMERO DI RATE DA PRENOTARE, FACCIO PARTIRE UN CICLO
                For i = 1 To Me.cmbNumRate.SelectedValue
                    'se non fa parte delle rate non modificabili procedo all'aggiornamento della prenotazione altrimenti passa alla rata successiva
                    If Not lstRate.ContainsKey(i) Then

                        stato = "0"
                        idVoce = "Null"
                        'End If
                        '****************PEPPE UPDATE PER OGNI VOCE DI SERVIZIO CARICATA NEL PERIODO DI GESTIONE
                        Dim nVoci As Integer = 0
                        Dim di As DataGridItem
                        Dim importo As Decimal = 0
                        'portato nVoci a 1 per evitare che prenda le date
                        For nVoci = 1 To Me.DataGridVociSpesa.Items.Count - 1
                            di = Me.DataGridVociSpesa.Items(nVoci)
                            importo = par.IfEmpty(CType(di.Cells(3).FindControl("txtRata" & i), TextBox).Text.Replace(".", ""), 0)
                            'PEPPE MODIFY 13/10/2011 COMMENTO E PERMETTO PRENOTAZIONE IMPORTO NEGATIVO
                            'If importo <> 0 Then
                            idPrenotazione = 0
                            If par.IfEmpty(di.Cells(par.IndDGC(DataGridVociSpesa, "ID_VOCE_PF")).Text.Replace("&nbsp;", ""), 0) > 0 Then
                                idVoce = di.Cells(par.IndDGC(DataGridVociSpesa, "ID_VOCE_PF")).Text
                                idPrenotazione = "0"

                                If par.AggiustaData(dict(i).ToString) <= FineEFApprovato Then
                                    idVoce = di.Cells(par.IndDGC(DataGridVociSpesa, "ID_VOCE_PF")).Text
                                    stato = "0"
                                Else
                                    idVoce = "Null"
                                    stato = "-1"
                                End If
                                If idVoce <> "Null" Then
                                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_VOCI_SPESA_PF WHERE ID_VOCE_COND = " & di.Cells(par.IndDGC(DataGridVociSpesa, "FL_TOTALE")).Text.Replace("&nbsp;", "0") & " AND ID_PIANO_FINANZIARIO = " & idPianoF.Value
                                    lettore = par.cmd.ExecuteReader
                                    If lettore.Read Then

                                        If (par.IfNull(lettore("ID_VOCE_PF"), 0)) > 0 Then
                                            voceProp = True
                                        Else
                                            voceProp = False
                                        End If

                                        If Left(par.AggiustaData(dict(i).ToString), 4) < Left(FineEFApprovato, 4) Then
                                            Response.Write("<script>alert('Impossibile completare l\'operazione!\nVerificare che le date di scadenza siano\nall\'interno dell\'ultimo E.F. approvato!');</script>")

                                            par.cmd.CommandText = "ROLLBACK TO SAVEPOINT PREVENTIVO"
                                            par.cmd.ExecuteNonQuery()
                                            Exit Function

                                        End If

                                        If CtrlResiduo(idVoce, importo, voceProp) = False Then
                                            par.cmd.CommandText = "ROLLBACK TO SAVEPOINT PREVENTIVO"
                                            par.cmd.ExecuteNonQuery()
                                            Exit Function
                                        End If

                                    End If
                                End If



                                par.cmd.CommandText = "select id_prenotazione from siscom_mi.cond_gestione_dett_scad where n_rata = " & i & " and id_gestione = " & vIdGestione & " and id_voce = " & par.IfEmpty(di.Cells(0).Text.Replace("&nbsp;", ""), 0)
                                myReader1 = par.cmd.ExecuteReader
                                If myReader1.Read Then
                                    idPrenotazione = par.IfNull(myReader1(0), 0)
                                End If
                                myReader1.Close()

                                If idPrenotazione = 0 And importo <> 0 Then
                                    '*******************************Selezione nuovo id_Prenotazione ID_VOCE_PF PIENA***********************************************
                                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL"
                                    myReader1 = par.cmd.ExecuteReader
                                    If myReader1.Read Then
                                        idPrenotazione = par.IfNull(myReader1(0), "-1")
                                    End If
                                    myReader1.Close()
                                    '****************INSERISCO LA PRENOTAZIONE DELLA RATA iesima sulla voce
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,DATA_SCADENZA,ID_STRUTTURA) VALUES" _
                                                        & " (" & idPrenotazione & ", " & Id_Fornitore & "," & idVoce & ", " & stato & ", 1, 'PAGAMENTO RATA " & i & " CONDOMINI', " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(importo) _
                                                        & " ," & par.AggiustaData(dict(i).ToString) & "," & idStruttura & ")"
                                    par.cmd.ExecuteNonQuery()
                                Else
                                    'par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET DATA_PRENOTAZIONE = " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ",IMPORTO_PRENOTATO = " & par.VirgoleInPunti(importo) _
                                    '                    & " ,DATA_SCADENZA = " & par.AggiustaData(dict(i).ToString) & " WHERE ID = " & idPrenotazione

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(importo) _
                                                        & " ,DATA_SCADENZA = " & par.AggiustaData(dict(i).ToString) & " WHERE ID = " & idPrenotazione

                                    par.cmd.ExecuteNonQuery()

                                End If

                                If idPrenotazione > 0 Then

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET ID_PRENOTAZIONE = " & idPrenotazione _
                                                        & " WHERE N_RATA = " & i & " AND ID_GESTIONE = " & vIdGestione & " AND ID_VOCE = " & par.IfEmpty(di.Cells(0).Text.Replace("&nbsp;", ""), 0)
                                    par.cmd.ExecuteNonQuery()

                                ElseIf importo <> 0 Then
                                    Response.Write("<script>alert('Attenzione!Impossibile completare la convalida!');</script>")
                                    par.cmd.CommandText = "ROLLBACK TO SAVEPOINT PREVENTIVO"
                                    par.cmd.ExecuteNonQuery()
                                    Exit Function
                                End If

                            ElseIf par.IfEmpty(di.Cells(par.IndDGC(DataGridVociSpesa, "ID_VOCE_PF_IMPORTO")).Text.Replace("&nbsp;", ""), 0) Then
                                idVoce = di.Cells(par.IndDGC(DataGridVociSpesa, "ID_VOCE_PF_IMPORTO")).Text
                                Dim idVocePf As String = ""
                                '*******************************PRENOTAZIONE ID_VOCE_PF_IMPORTO PIENA***********************************************



                                par.cmd.CommandText = "SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID = " & idVoce
                                myReader1 = par.cmd.ExecuteReader
                                If myReader1.Read Then
                                    idVocePf = par.IfNull(myReader1(0), "Null")
                                End If
                                myReader1.Close()


                                par.cmd.CommandText = "select id_prenotazione from siscom_mi.cond_gestione_dett_scad where n_rata = " & i & " and id_gestione = " & vIdGestione _
                                                    & " and id_voce = " & par.IfEmpty(di.Cells(0).Text.Replace("&nbsp;", ""), 0)
                                myReader1 = par.cmd.ExecuteReader
                                If myReader1.Read Then
                                    idPrenotazione = par.IfNull(myReader1(0), 0)
                                End If
                                myReader1.Close()
                                stato = "0"
                                If par.AggiustaData(dict(i).ToString) > FineEFApprovato Then
                                    idVoce = "Null"
                                    idVocePf = "Null"
                                    stato = "-1"
                                End If
                                If idPrenotazione = 0 And importo <> 0 Then
                                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL"
                                    myReader1 = par.cmd.ExecuteReader
                                    If myReader1.Read Then
                                        idPrenotazione = par.IfNull(myReader1(0), "-1")
                                    End If
                                    myReader1.Close()
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID,ID_FORNITORE,ID_VOCE_PF,ID_VOCE_PF_IMPORTO,ID_STATO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,DATA_SCADENZA,ID_STRUTTURA) VALUES" _
                                                        & " (" & idPrenotazione & ", " & Id_Fornitore & "," & idVocePf & "," & idVoce & ", " & stato & ", 1, 'PAGAMENTO RATA " & i & " CONDOMINI', " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(importo) _
                                                        & " ," & par.AggiustaData(dict(i).ToString) & "," & idStruttura & ")"
                                    par.cmd.ExecuteNonQuery()

                                Else
                                    'par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET DATA_PRENOTAZIONE = " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ",IMPORTO_PRENOTATO = " & par.VirgoleInPunti(importo) _
                                    '                    & ",DATA_SCADENZA = " & par.AggiustaData(dict(i).ToString) & "  WHERE ID = " & idPrenotazione
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(importo) _
                                                        & ",DATA_SCADENZA = " & par.AggiustaData(dict(i).ToString) & "  WHERE ID = " & idPrenotazione

                                    par.cmd.ExecuteNonQuery()

                                End If


                                If idPrenotazione > 0 Then
                                    '****************INSERISCO LA PRENOTAZIONE DELLA RATA iesima sulla voce
                                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE_DETT_SCAD SET ID_PRENOTAZIONE = " & idPrenotazione _
                                                        & " WHERE N_RATA = " & i & " AND ID_GESTIONE = " & vIdGestione & " AND ID_VOCE = " & par.IfEmpty(di.Cells(0).Text.Replace("&nbsp;", ""), 0)
                                    par.cmd.ExecuteNonQuery()

                                ElseIf importo <> 0 Then
                                    Response.Write("<script>alert('Attenzione!Impossibile completare la convalida!');</script>")
                                    par.cmd.CommandText = "ROLLBACK TO SAVEPOINT PREVENTIVO"
                                    par.cmd.ExecuteNonQuery()
                                    Exit Function
                                End If
                            End If
                            'End If

                        Next
                    End If
                Next

                '*****INSERIMENTO DELLE PRENOTAZIONI DI PAGAMENTO
                '*****Inserisco una prenotazione per ogni rata del pagamento

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GESTIONE SET STATO_BILANCIO = 'P1' WHERE ID = " & vIdGestione
                par.cmd.ExecuteNonQuery()
                GestStatoBilancio("P1")
                esito = True

                If visMsg = True Then
                    Response.Write("<script>alert('Il bilancio è stato Convalidato, e sono stati prenotati i pagamenti!');</script>")

                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CONVALIDA PREVENTIVO CONTABILITA')"
                    par.cmd.ExecuteNonQuery()

                Else
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                End If


            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.cmd.CommandText = "ROLLBACK TO SAVEPOINT PREVENTIVO"
            par.cmd.ExecuteNonQuery()

        End Try


        Return esito

    End Function

    Protected Sub btnConvalida_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConvalida.Click
        Convalida(True)
    End Sub
    Private Function CtrlStatoEs(ByVal idVocePF As String) As Boolean
        CtrlStatoEs = True

        par.cmd.CommandText = "SELECT "

        Return CtrlStatoEs
    End Function
    Private Function ControllaImporti() As Boolean
        ControllaImporti = True
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim id_voce As String = ""

            'par.cmd.CommandText = "select id_stato from siscom_mi.pf_main where id_esercizio_finanziario = (select id from T_ESERCIZIO_FINANZIARIO where substr(inizio,0,4) = "& par.AggiustaData ( &")"

            par.cmd.CommandText = "SELECT cond_voci_spesa_pf.id_voce_pf, cond_voci_spesa_pf.id_voce_pf_importo, SUM(importo)AS importo FROM siscom_mi.cond_gestione_dett_scad ,siscom_mi.cond_voci_spesa, siscom_mi.cond_voci_spesa_pf " _
                                & "WHERE cond_voci_spesa.ID = cond_gestione_dett_scad.id_voce AND cond_voci_spesa.fl_totale = 1 AND id_gestione = " & vIdGestione & " AND importo > 0 AND cond_voci_spesa_pf.id_voce_cond = cond_voci_spesa.ID " _
                                & "and cond_voci_spesa_pf.id_piano_finanziario = " & idPianoF.Value _
                                & " GROUP BY cond_voci_spesa_pf.id_voce_pf, cond_voci_spesa_pf.id_voce_pf_importo ORDER BY cond_voci_spesa_pf.id_voce_pf ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtVoci As New Data.DataTable()
            Dim voceProperty As Boolean = False
            da.Fill(dtVoci)


            For Each r As Data.DataRow In dtVoci.Rows

                If par.IfNull(r.Item("id_voce_pf"), 0) > 0 Then
                    id_voce = r.Item("id_voce_pf")
                    voceProperty = True
                Else
                    id_voce = r.Item("id_voce_pf_importo")
                    voceProperty = False
                End If
                If CtrlResiduo(id_voce, r.Item("IMPORTO"), voceProperty) = False Then
                    ControllaImporti = False
                    Exit Function
                End If
            Next


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            ControllaImporti = False
        End Try
        Return ControllaImporti
    End Function
    Private Function CtrlResiduo(ByVal IdVoce As String, ByVal Importo As Decimal, ByVal VoceProperty As Boolean) As Boolean
        CtrlResiduo = True
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim idStruttura As String = -1
            par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idStruttura = par.IfNull(lettore("ID_UFFICIO"), "-1")
            End If
            lettore.Close()

            'par.cmd.CommandText = "SELECT codice,pf_voci_struttura.* FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_VOCI_STRUTTURA " _
            '    & "WHERE pf_voci_struttura.id_voce = " & IdVoce & " AND pf_voci.ID = pf_voci_struttura.id_voce AND id_struttura = " & idStruttura
            'lettore = par.cmd.ExecuteReader
            'If lettore.Read Then

            'If par.IfNull(lettore("CODICE").ToString, "0").ToString.Substring(0, 1) = "1" Then
            '    VoceProperty = True
            'End If

            Dim Prenotazioni As String = ""
            Dim Rimanente As Double = 0
            Dim Stanziamento As Double = 0
            Dim ElVociPf As String = ""
            Dim ElVociPfImporto As String = ""
            Dim Condition As String = ""
            Dim TotPrenotato As Decimal = 0
            Dim myreader As Oracle.DataAccess.Client.OracleDataReader



            If VoceProperty = True Then

                '*********CASO IN CUI LA VOCE è DI PROPERTY E FACCIO CAPO ALLE PRENOTAZIONI E AGLI IMPORTI STANZIATI A CARICO DELLA VOCE MADRE
                par.cmd.CommandText = "SELECT CODICE FROM SISCOM_MI.PF_VOCI WHERE ID = " & IdVoce
                lettore = par.cmd.ExecuteReader
                Dim figlio As Boolean
                If lettore.Read Then
                    Dim l As Integer = par.IfNull(lettore("CODICE"), ".").ToString.Length
                    If l - par.IfNull(lettore("CODICE"), ".").ToString.Replace(".", "").Length >= 3 Then
                        figlio = True
                    Else
                        figlio = False
                    End If
                End If
                lettore.Close()

                If figlio = True Then
                    par.cmd.CommandText = "SELECT SUM(CASE WHEN nvl(IMPORTO_APPROVATO,0) > 0 THEN IMPORTO_APPROVATO ELSE importo_prenotato END)AS prenotato FROM siscom_mi.prenotazioni,siscom_mi.pf_voci " _
                                        & "WHERE id_stato >= 0 and  id_voce_pf = siscom_mi.pf_voci.ID AND id_voce_madre = ( " _
                                        & "SELECT pf_voci_struttura.id_voce FROM siscom_mi.pf_voci,siscom_mi.pf_voci_struttura " _
                                        & "WHERE pf_voci_struttura.id_voce = pf_voci.id_voce_madre AND pf_voci.ID = " & IdVoce & " " _
                                        & "AND id_struttura = " & idStruttura & ")"
                Else
                    par.cmd.CommandText = "SELECT SUM(CASE WHEN nvl(IMPORTO_APPROVATO,0) > 0 THEN IMPORTO_APPROVATO ELSE importo_prenotato END)AS prenotato FROM siscom_mi.prenotazioni " _
                    & "WHERE id_stato >= 0 and id_voce_pf = " & IdVoce & "AND id_struttura = " & idStruttura

                End If
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    TotPrenotato = par.IfNull(myreader("PRENOTATO"), 0)
                End If
                myreader.Close()
                If figlio = True Then
                    par.cmd.CommandText = "SELECT * FROM siscom_mi.pf_voci,siscom_mi.pf_voci_struttura " _
                                        & "WHERE pf_voci_struttura.id_voce = pf_voci.id_voce_madre And pf_voci.ID = " & IdVoce & "AND id_struttura = " & idStruttura & ""
                Else
                    par.cmd.CommandText = "SELECT * FROM siscom_mi.pf_voci_struttura " _
                    & "WHERE pf_voci_struttura.id_voce = " & IdVoce & " AND id_struttura = " & idStruttura & ""

                End If
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    Stanziamento = CDbl(par.IfNull(myreader("VALORE_LORDO"), 0) + par.IfNull(myreader("ASSESTAMENTO_VALORE_LORDO"), 0) + par.IfNull(myreader("VARIAZIONI"), 0))
                End If
                myreader.Close()

            Else
                par.cmd.CommandText = "SELECT SUM(CASE WHEN NVL(IMPORTO_APPROVATO,0) > 0 THEN IMPORTO_APPROVATO ELSE importo_prenotato END)AS prenotato " _
                                    & "FROM SISCOM_MI.PRENOTAZIONI WHERE id_stato >= 0 and ID_VOCE_PF = (SELECT id_voce FROM siscom_mi.pf_voci_importo WHERE ID =" & IdVoce & ") AND id_struttura = " & idStruttura & ""
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    TotPrenotato = par.IfNull(myreader("PRENOTATO"), 0)
                End If
                myreader.Close()

                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE = (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID = " & IdVoce & " ) AND ID_STRUTTURA =" & idStruttura & "" ' (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE DESCRIZIONE LIKE '%CONDOMINIO%')"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                For Each row As Data.DataRow In dt.Rows
                    Stanziamento = Stanziamento + CDbl(par.IfNull(row.Item("VALORE_LORDO"), 0) + par.IfNull(row.Item("ASSESTAMENTO_VALORE_LORDO"), 0) + par.IfNull(myreader("VARIAZIONI"), 0))
                Next

            End If

            If Stanziamento > 0 Then

                Rimanente = Stanziamento - TotPrenotato
                If Rimanente > 0 Then
                    If Math.Round(Rimanente, 2) < Math.Round(Importo, 2) Then
                        Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
                        CtrlResiduo = False
                        Exit Function
                    End If
                Else
                    Response.Write("<script>alert('Sono stati spesi più soldi di quelli previsti nel Piano Finanziario!!Impossibile continuare!!');</script>")
                    CtrlResiduo = False
                    Exit Function
                End If
            Else
                Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento oppure il piano finanziario non è stato ancora approvato!');</script>")
                CtrlResiduo = False
                Exit Function
            End If

            'End If

            lettore.Close()


            'If idCond.Value <> 0 And idGestione.Value <> 0 And Importo.Value > 0 And nRata.Value > 0 Then
            '    Dim Prenotazioni As String = ""
            '    Dim Rimanente As Double = 0
            '    Dim Stanziamento As Double = 0
            '    Dim ElVociPf As String = ""
            '    Dim ElVociPfImporto As String = ""
            '    Dim Condition As String = ""
            '    '*******************APERURA CONNESSIONE*********************
            '    If par.OracleConn.State = Data.ConnectionState.Closed Then
            '        par.OracleConn.Open()
            '        par.SettaCommand(par)
            '    End If

            '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            '    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN " _
            '                        & "(SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD " _
            '                        & "WHERE ID_GESTIONE = " & idGestione.Value & "AND N_RATA = " & nRata.Value & ")"
            '    myReader1 = par.cmd.ExecuteReader
            '    While myReader1.Read
            '        If String.IsNullOrEmpty(Prenotazioni) Then
            '            Prenotazioni = par.IfNull(myReader1("ID"), 0)
            '        Else
            '            Prenotazioni = Prenotazioni & "," & myReader1("ID")
            '        End If
            '    End While
            '    myReader1.Close()
            '    'par.cmd.CommandText = "SELECT PF_VOCI_STRUTTURA.*,SISCOM_MI.PF_VOCI.* FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_PIANO_FINANZIARIO = (SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID_STATO = 5 AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND SUBSTR(INIZIO,0,4) = '" & par.AggiustaData(txtScadenza.Value).ToString.Substring(0, 4) & "') AND CODICE = '1.6' AND PF_VOCI.ID = PF_VOCI_STRUTTURA.ID_VOCE"
            '    'par.cmd.CommandText = "SELECT PF_VOCI_STRUTTURA.*,SISCOM_MI.PF_VOCI.* FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PF_VOCI_STRUTTURA " _
            '    '                    & "WHERE ID_PIANO_FINANZIARIO = (SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
            '    '                    & "WHERE ID_STATO = 5 AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND SUBSTR(INIZIO,0,4) = '" _
            '    '                    & Format(Now, "yyyy") & "') AND PF_VOCI.codice = '1.02.09' AND PF_VOCI.ID = PF_VOCI_STRUTTURA.ID_VOCE"

            '    par.cmd.CommandText = "SELECT ID_VOCE_PF, ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & Prenotazioni & ")"

            '    myReader1 = par.cmd.ExecuteReader()
            '    While myReader1.Read
            '        If par.IfNull(myReader1("ID_VOCE_PF_IMPORTO"), 0) > 0 Then
            '            par.cmd.CommandText = " SELECT * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE = (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID = " & myReader1("id_voce_pf_importo") & " ) AND ID_STRUTTURA = (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE DESCRIZIONE LIKE '%CONDOMINIO%')"
            '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '            Dim dt As New Data.DataTable
            '            da.Fill(dt)
            '            For Each row As Data.DataRow In dt.Rows
            '                Stanziamento = Stanziamento + CDbl(par.IfNull(row.Item("VALORE_LORDO"), 0) + par.IfNull(row.Item("ASSESTAMENTO_VALORE_LORDO"), 0))
            '            Next
            '            If String.IsNullOrEmpty(ElVociPfImporto) Then
            '                ElVociPfImporto = myReader1("ID_VOCE_PF_IMPORTO")
            '            Else
            '                ElVociPfImporto = ElVociPfImporto & "," & myReader1("ID_VOCE_PF_IMPORTO")
            '            End If
            '        ElseIf par.IfNull(myReader1("ID_VOCE_PF"), 0) > 0 Then
            '            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE =" & myReader1("id_voce_pf") & " AND ID_STRUTTURA = (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE DESCRIZIONE LIKE '%CONDOMINIO%')"
            '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '            Dim dt As New Data.DataTable
            '            da.Fill(dt)
            '            For Each row As Data.DataRow In dt.Rows
            '                Stanziamento = Stanziamento + CDbl(par.IfNull(row.Item("VALORE_LORDO"), 0) + par.IfNull(row.Item("ASSESTAMENTO_VALORE_LORDO"), 0))
            '            Next
            '            If String.IsNullOrEmpty(ElVociPf) Then
            '                ElVociPf = myReader1("ID_VOCE_PF")
            '            Else
            '                ElVociPf = ElVociPf & "," & myReader1("ID_VOCE_PF")
            '            End If
            '        End If


            '    End While
            '    myReader1.Close()

            '    '***controllo che il valore preventivato di spesa esista e sia maggiore di 0
            '    If Stanziamento > 0 Then
            '        If Not String.IsNullOrEmpty(ElVociPf) Then
            '            Condition = "ID_VOCE_PF IN (" & ElVociPf & ")"
            '        End If
            '        If Not String.IsNullOrEmpty(ElVociPfImporto) Then
            '            If Not String.IsNullOrEmpty(Condition) Then
            '                Condition = Condition & " OR "
            '            End If
            '            Condition = Condition & "ID_VOCE_PF_IMPORTO IN (" & ElVociPfImporto & ")"
            '        End If
            '        par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE (" & Condition & ")" _
            '                            & " AND TIPO_PAGAMENTO = 1 AND ID_PAGAMENTO IS NULL AND ID_STATO = 0"
            '        Dim PagatiPrenotati As Decimal = 0
            '        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '        If lettore.Read Then
            '            PagatiPrenotati = par.IfNull(lettore("TOT_PRENOTATO"), 0)
            '        End If
            '        lettore.Close()
            '        par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_APPROVATO),0) as TOT_PAGATO FROM SISCOM_MI.PRENOTAZIONI WHERE (" & Condition & ")" _
            '                            & " AND TIPO_PAGAMENTO = 1 AND ID_STATO > 0"
            '        lettore = par.cmd.ExecuteReader
            '        If lettore.Read Then
            '            PagatiPrenotati = PagatiPrenotati + par.IfNull(lettore("TOT_PAGATO"), 0)
            '            '*******Differenza fra preventivato e importi fino a ora pagati
            '            Rimanente = Stanziamento - PagatiPrenotati
            '            '******Se il rimanente è positivo ed è superiore alla cifra da pagare procedo con il pagamento
            '            If Rimanente >= 0 Then

            '                If Rimanente >= CDbl(Importo.Value) Then

            '                    '******Scrittura del nuovo pagamento nell'apposita tabella!*******
            '                    Dim Pagamento As String = CreaPagamento(Prenotazioni)
            '                    If Pagamento <> "" Then
            '                        Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
            '                        lettore.Close()
            '                        'myReader1.Close()
            '                        '*********************CHIUSURA CONNESSIONE**********************
            '                        par.OracleConn.Close()
            '                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '                        PdfPagamento(Pagamento)
            '                        CaricaTabella()
            '                        idGestione.Value = 0
            '                        Importo.Value = 0
            '                        Response.Flush()
            '                        Exit Function
            '                    Else
            '                        Response.Write("<script>alert('Operazione annullata!Non è stato possibile emettere il pagamento!');</script>")
            '                        lettore.Close()
            '                        'myReader1.Close()
            '                        idGestione.Value = 0
            '                        Importo.Value = 0

            '                        '*********************CHIUSURA CONNESSIONE**********************
            '                        par.OracleConn.Close()
            '                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            '                    End If

            '                Else
            '                    Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
            '                    idGestione.Value = 0
            '                    Importo.Value = 0

            '                    Exit Function
            '                    lettore.Close()
            '                    'myReader1.Close()
            '                End If
            '            Else
            '                Response.Write("<script>alert('Sono stati spesi più soldi di quelli previsti nel Piano Finanziario!!Impossibile continuare!!');</script>")
            '                idGestione.Value = 0
            '                Importo.Value = 0

            '                Exit Function
            '                lettore.Close()
            '                'myReader1.Close()
            '            End If

            '        End If
            '        lettore.Close()
            '    Else
            '        Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento oppure il piano finanziario non è stato ancora approvato!');</script>")
            '        idGestione.Value = 0
            '        Importo.Value = 0

            '        Exit Function
            '        'myReader1.Close()
            '    End If
            '    'myReader1.Close()
            'Else
            '    Response.Write("<script>alert('Selezionare una riga per emettere il pagamento!');</script>")

            'End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            CtrlResiduo = False
        End Try



        Return CtrlResiduo
    End Function

    Private Function NRata_Adeguata(ByVal rata As Integer) As Boolean
        NRata_Adeguata = True

        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans
        For i As Integer = 24 To rata + 1 Step -1
            par.cmd.CommandText = "select importo,id_prenotazione, " _
                    & "(select prenotazioni.id_pagamento from siscom_mi.prenotazioni where id = id_prenotazione) as pagamento " _
                    & "from siscom_mi.cond_gestione_dett_scad where id_prenotazione is not null and n_rata = " & i _
                    & " and id_gestione = " & vIdGestione

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If lettore.Read Then
                If par.IfNull(lettore("id_prenotazione"), 0) > 0 And par.IfNull(lettore("pagamento"), 0) > 0 Then
                    NRata_Adeguata = False
                    rata = nRate.Value
                    lettore.Close()
                    Exit For
                End If
            End If
            lettore.Close()

        Next
        For i As Integer = 24 To rata + 1 Step -1
            CType(DataGridVociSpesa.Items(0).FindControl("txtRata" & i), TextBox).Text = ""
            'CType(Me.Page.FindControl("txtDataRata" & i & ""), TextBox).Text = ""
        Next

        Return NRata_Adeguata
    End Function
    Function EsisteDetScad(ByVal idVoce As Integer, ByVal nRata As Integer, ByVal rScad As String) As Boolean
        Dim esito As Boolean = False
        par.cmd.CommandText = "SELECT * from SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                            & " WHERE ID_GESTIONE= " & vIdGestione & " AND ID_VOCE=" & idVoce & " AND N_RATA =" & nRata
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            esito = True
        Else
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE_DETT_SCAD (ID_GESTIONE,ID_VOCE,N_RATA,RATA_SCAD) VALUES " _
                                & "(" & vIdGestione & ", " & idVoce & ", " & nRata & " , " & rScad & ")"
            par.cmd.ExecuteNonQuery()
            esito = True
        End If

        Return esito
    End Function
    Private Sub CreaIdNuovaGestione()
        ''08/05/2012 Controllo coesistenza preventivi di spesa avente lo stesso periodo ma tipologia diversa
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        If Me.cmbTipoGest.SelectedValue = "O" Then
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_GESTIONE WHERE SUBSTR(DATA_INIZIO,0,4)= " & Me.txtAnnoInizio.Text & " AND SUBSTR(DATA_FINE,0,4)=" & Me.TxtAnnoFine.Text & " AND TIPO = '" & Me.cmbTipoGest.SelectedValue & "' AND ID_CONDOMINIO=" & Request.QueryString("IDCONDOMINIO")
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                Response.Write("<script>alert('Preventivo di spesa già inserito per questo periodo ed avente stessa tipologia!');self.close();</script>")
                Exit Sub
            End If
            myReader1.Close()
        End If
        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COND_GESTIONE.NEXTVAL FROM DUAL"
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            vIdGestione = myReader1(0)
        End If
        myReader1.Close()
        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GESTIONE (ID, ID_CONDOMINIO,DATA_INIZIO,DATA_FINE, TIPO, N_RATE," _
                    & " RATA_1_SCAD,RATA_2_SCAD,RATA_3_SCAD,RATA_4_SCAD,RATA_5_SCAD,RATA_6_SCAD," _
                    & " RATA_7_SCAD,RATA_8_SCAD,RATA_9_SCAD,RATA_10_SCAD,RATA_11_SCAD,RATA_12_SCAD," _
                    & " RATA_13_SCAD,RATA_14_SCAD,RATA_15_SCAD,RATA_16_SCAD,RATA_17_SCAD,RATA_18_SCAD," _
                    & " RATA_19_SCAD,RATA_20_SCAD,RATA_21_SCAD,RATA_22_SCAD,RATA_23_SCAD,RATA_24_SCAD," _
                    & " STATO_BILANCIO,NOTE)" _
                    & " VALUES (" & vIdGestione & ", " & Request.QueryString("IDCONDOMINIO") & "," _
                    & " " & par.AggiustaData(Me.txtInizioGest.Text & "/" & Me.txtAnnoInizio.Text) & ", " & par.AggiustaData(Me.TxtFineGest.Text & "/" & Me.TxtAnnoFine.Text) & "," _
                    & " '" & Me.cmbTipoGest.SelectedValue & "', " & Me.cmbNumRate.SelectedValue & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata1"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata2"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata3"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata4"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata5"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata6"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata7"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata8"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata9"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata10"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata11"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata12"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata13"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata14"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata15"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata16"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata17"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata18"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata19"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata20"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata21"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata22"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata23"), TextBox).Text), "Null") & "," _
                    & par.IfEmpty(par.AggiustaData(CType(DataGridVociSpesa.Items(0).FindControl("txtRata24"), TextBox).Text), "Null") & "," _
                    & "'P0','" & par.PulisciStrSql(Me.txtNote.Text) & "')"
        par.cmd.ExecuteNonQuery()
        NewEs.Value = 1
    End Sub

    Protected Sub btnElInquilini_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElInquilini.Click
        'par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COND_GESTIONE.NEXTVAL FROM DUAL"
        SommaIndiretti()
    End Sub
    Private Sub SommaIndiretti()
        Try
            Dim msgImpMinoreDiSomma As Boolean = False
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans



            par.cmd.CommandText = "SELECT SUM(NVL(importo_preventivo,0)) AS Somma, id_voce AS id_voce_bolletta, ID AS id_voce_condominio " _
                                & "FROM siscom_mi.COND_GEST_INDIRETTE, siscom_mi.COND_VOCI_SPESA  " _
                                & "WHERE COND_VOCI_SPESA.id_voce_bolletta = id_voce And id_gestione = " & vIdGestione & " And id_condominio = " & idCondominio.Value & " " _
                                & "GROUP BY id_voce,ID"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim i As Integer = 0
            For Each r As Data.DataRow In dt.Rows
                For Each di As DataGridItem In DataGridVociSpesa.Items
                    If di.Cells(0).Text = r.Item("ID_VOCE_CONDOMINIO") Then
                        If par.IfEmpty(CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text, 0) = 0 Then
                            CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text = IsNumFormat(r.Item("SOMMA"), "", "##,##0.00")

                        ElseIf par.IfEmpty(CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text, 0) >= r.Item("SOMMA") Then
                            CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text = IsNumFormat(r.Item("SOMMA"), "", "##,##0.00") + (par.IfEmpty(CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text, 0) - r.Item("SOMMA"))

                        Else
                            CType(di.Cells(2).FindControl("txtPreventivo"), TextBox).Text = IsNumFormat(r.Item("SOMMA"), "", "##,##0.00")
                            msgImpMinoreDiSomma = True
                        End If
                    End If
                Next
            Next
            If msgImpMinoreDiSomma = True Then
                Response.Write("<script>alert('ATTENZIONE!!!L\'importo a preventivo non può essere inferiore di quello definito per inquilino!');</script>")
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub


End Class
