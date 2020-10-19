
Partial Class Condomini_ContabIndiretta
    Inherits PageSetIdMode
    Dim par As New CM.Global

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Response.Write("<div id='splash' style='border: thin dashed #000066;position:absolute; z-index: 500; text-align: center;font-size: 10px; width: 95%; height: 95%; visibility: visible; vertical-align: top;" _
                    & "line-height: normal; top: 20px; left: 20px; background-color: #FFFFFF;'><br /><br /><br /><br /><br /><br /><br /><br /><br /><img src='Immagini/load.gif' alt='caricamento in corso' />" _
                    & "<br /><br />caricamento in corso...<br /><br /><br /><br /><br /><br /><br />&nbsp;</div>")
        If Not IsPostBack Then
            'Response.Flush()
            idCondominio.Value = Request.QueryString("IdCond")
            idGestione.Value = Request.QueryString("IDGESTIONE")
            If Request.QueryString("IDCON") <> "" Then
                vIdConnModale = Request.QueryString("IDCON")
            End If

            'CaricaDatiCondominio()
            CaricaInquilini()

            If Me.dgvInquilini.Items.Count >= 1 Then
                AddJsFunction()
            End If
        End If

    End Sub
    'Private Sub CaricaDatiCondominio()
    '    Try

    '        '*******************RICHIAMO LA CONNESSIONE*********************
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)
    '        '*******************RICHIAMO LA TRANSAZIONE*********************
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans

    '        par.cmd.CommandText = "select condomini.id,denominazione,tipo_gestione,tipologia, " _
    '                            & "(cond_amministratori.titolo||' '||cond_amministratori.cognome||' '||cond_amministratori.nome) as amministratore " _
    '                            & "from siscom_mi.condomini,siscom_mi.cond_amministrazione,siscom_mi.cond_amministratori " _
    '                            & "where condomini.id = " & idCondominio.Value _
    '                            & " and cond_amministrazione.id_condominio = condomini.id " _
    '                            & " and cond_amministrazione.data_fine is null " _
    '                            & " and cond_amministrazione.id_amministratore = cond_amministratori.id"
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dt As New Data.DataTable
    '        da.Fill(dt)
    '        If dt.Rows.Count > 0 Then
    '            Me.txtCodCond.Text = Format(CDbl(idCondominio.Value), "00000")
    '            Me.txtCondominio.Text = dt.Rows(0).Item("DENOMINAZIONE")
    '            If dt.Rows(0).Item("TIPO_GESTIONE") = "D" Then
    '                Me.txtGestione.Text = "DIRETTA"
    '            ElseIf dt.Rows(0).Item("TIPO_GESTIONE") = "I" Then
    '                Me.txtGestione.Text = "INDIRETTA"
    '            End If
    '            If dt.Rows(0).Item("TIPOLOGIA") = "C" Then
    '                Me.txtTipologia.Text = "CONDOMINIO"
    '            ElseIf dt.Rows(0).Item("TIPOLOGIA") = "S" Then
    '                Me.txtTipologia.Text = "SUPER CONDOMINIO"
    '            ElseIf dt.Rows(0).Item("TIPOLOGIA") = "T" Then
    '                Me.txtTipologia.Text = "CENTRALE TERMICA"
    '            End If
    '            Me.txtAmministratore.Text = dt.Rows(0).Item("amministratore")


    '        End If
    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Redirect("..\Errore.aspx")

    '    End Try
    'End Sub
    Private Sub CaricaInquilini()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT id_ui,posizione_bilancio,mil_pro,mil_asc,mil_Compro,mil_gest,mil_Risc,mill_pres_ass,NOTE,id_intestario, " _
                                & "(CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO " _
                                & "FROM siscom_mi.COND_UI,siscom_mi.ANAGRAFICA " _
                                & "WHERE id_intestario = ANAGRAFICA.ID(+) and posizione_bilancio is not null AND id_condominio = " & idCondominio.Value & " order by posizione_bilancio asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.dgvInquilini.DataSource = dt
            Me.dgvInquilini.DataBind()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")

        End Try
    End Sub
    Private Function CaricaVociInquilino(ByVal di As DataGridItem) As Boolean
        CaricaVociInquilino = False
        Dim idUi = di.Cells(0).Text
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "select '" & idUi & "' as id_ui,id as id_voce,descrizione as voce, " _
                & "trim(TO_CHAR(NVL((select TRIM(TO_CHAR(NVL(importo_preventivo,0),'9G999G999G990D99')) from siscom_mi.cond_gest_indirette where id_ui =" & idUi & " and id_gestione = " & idGestione.Value & " and id_condominio = " & idCondominio.Value & " AND id_voce = T_VOCI_BOLLETTA.ID),0),'9G999G999G990D99'))as importo_preventivo, " _
                & "trim(TO_CHAR(NVL((select TRIM(TO_CHAR(NVL(importo_consuntivo,0),'9G999G999G990D99')) from siscom_mi.cond_gest_indirette where id_ui =" & idUi & " and id_gestione = " & idGestione.Value & " and id_condominio = " & idCondominio.Value & " AND id_voce = T_VOCI_BOLLETTA.ID),0),'9G999G999G990D99'))as importo_consuntivo " _
                & "from siscom_mi.t_voci_bolletta where id in (300,302,303) order by descrizione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            CType(di.Cells(TrovaIndiceColonna(dgvInquilini, "GESTIONE")).FindControl("dgvVociInquilino"), DataGrid).DataSource = dt
            CType(di.Cells(TrovaIndiceColonna(dgvInquilini, "GESTIONE")).FindControl("dgvVociInquilino"), DataGrid).DataBind()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")

        End Try
        CaricaVociInquilino = True

    End Function
    Private Sub AddJsFunction()
        Try
            For Each i As DataGridItem In dgvInquilini.Items
                For Each di As DataGridItem In CType(i.Cells(TrovaIndiceColonna(dgvInquilini, "GESTIONE")).FindControl("dgvVociInquilino"), DataGrid).Items

                    CType(di.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                    CType(di.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

                    CType(di.Cells(2).FindControl("txtImpPreventivo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);  ")
                    CType(di.Cells(3).FindControl("txtImpPreventivo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

                    'CType(di.Cells(3).FindControl("txtInizio"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    'CType(di.Cells(3).FindControl("txtFine"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                    'par.caricaComboBox("select id,descrizione from siscom_mi.cond_voci_addebito", CType(di.Cells(3).FindControl("cmbVoceBolletta"), DropDownList), "id", "descrizione", True)

                Next

            Next


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns

                If c.GetType.Name = "BoundColumn" Then

                    If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                        TrovaIndiceColonna = Indice
                        Exit For
                    End If
                ElseIf c.GetType.Name = "TemplateColumn" Then
                    If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.TemplateColumn).HeaderText, StringComparison.InvariantCultureIgnoreCase) Then
                        TrovaIndiceColonna = Indice
                        Exit For
                    End If


                End If
                Indice = Indice + 1


            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

        Return TrovaIndiceColonna

    End Function


    Protected Sub dgvInquilini_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvInquilini.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            CaricaVociInquilino(e.Item)
        End If

    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        SaveData()

    End Sub
    Private Sub SaveData()
        Try

            Dim inserimentoImporti As Boolean = False
            Dim aggiornamentoImporti As Boolean = False

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            Dim Esiste As Boolean = False
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            For Each dgInq As DataGridItem In dgvInquilini.Items
                For Each diVoci As DataGridItem In CType(dgInq.Cells(TrovaIndiceColonna(dgvInquilini, "GESTIONE")).FindControl("dgvVociInquilino"), DataGrid).Items
                    If par.IfEmpty(CType(diVoci.Cells(2).FindControl("txtImpPreventivo"), TextBox).Text.Replace(".", ""), 0) > 0 Or par.IfEmpty(CType(diVoci.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Text.Replace(".", ""), 0) > 0 Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_GEST_INDIRETTE where ID_UI = " & dgInq.Cells(0).Text & " AND ID_CONDOMINIO = " & idCondominio.Value & " AND ID_GESTIONE = " & idGestione.Value & " AND ID_VOCE = " & diVoci.Cells(1).Text
                        lettore = par.cmd.ExecuteReader

                        If lettore.HasRows Then
                            aggiornamentoImporti = True
                            par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GEST_INDIRETTE  SET IMPORTO_PREVENTIVO= " & par.IfEmpty(par.VirgoleInPunti(CType(diVoci.Cells(2).FindControl("txtImpPreventivo"), TextBox).Text.Replace(".", "")), "NULL") & ", " _
                                                & "IMPORTO_CONSUNTIVO = " & par.IfEmpty(par.VirgoleInPunti(CType(diVoci.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Text.Replace(".", "")), "NULL") _
                                                & " WHERE ID_UI = " & dgInq.Cells(0).Text & " AND ID_CONDOMINIO = " & idCondominio.Value & " AND ID_GESTIONE = " & idGestione.Value & " AND ID_VOCE = " & diVoci.Cells(1).Text
                            par.cmd.ExecuteNonQuery()
                        Else
                            inserimentoImporti = True
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_GEST_INDIRETTE  (ID_GESTIONE,ID_CONDOMINIO,ID_UI,ID_VOCE,IMPORTO_PREVENTIVO,IMPORTO_CONSUNTIVO) " _
                                                & " VALUES (" & idGestione.Value & "," & idCondominio.Value & "," & dgInq.Cells(0).Text & ", " & diVoci.Cells(1).Text & "," _
                                                & par.IfEmpty(par.VirgoleInPunti(CType(diVoci.Cells(2).FindControl("txtImpPreventivo"), TextBox).Text.Replace(".", "")), "NULL") & ", " _
                                                & par.IfEmpty(par.VirgoleInPunti(CType(diVoci.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Text.Replace(".", "")), "NULL") & ")"
                            par.cmd.ExecuteNonQuery()

                        End If
                        lettore.Close()
                    End If
                Next
            Next

            If inserimentoImporti = True Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idCondominio.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F192','INSERIMENTO IMPORTI INQUILINI PREVENTIVO CONTABILITA')"
                par.cmd.ExecuteNonQuery()
            End If
            If aggiornamentoImporti = True Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & idCondominio.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F192','MODIFICA IMPORTI INQUILINI PREVENTIVO CONTABILITA')"
                par.cmd.ExecuteNonQuery()
            End If
            

            Me.txtModificato.Value = 0
            Response.Write("<script>alert('Operazione eseguita correttamente!')</script>")
        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")

        End Try
    End Sub
    Private Sub UpdateData()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            For Each dgInq As DataGridItem In dgvInquilini.Items
                For Each diVoci As DataGridItem In CType(dgInq.Cells(TrovaIndiceColonna(dgvInquilini, "GESTIONE")).FindControl("dgvVociInquilino"), DataGrid).Items
                    If Not String.IsNullOrEmpty(CType(diVoci.Cells(2).FindControl("txtImpPreventivo"), TextBox).Text) Or Not String.IsNullOrEmpty(CType(diVoci.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Text) Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_GEST_INDIRETTE  SET IMPORTO_PREVENTIVO= " & par.IfEmpty(par.VirgoleInPunti(CType(diVoci.Cells(2).FindControl("txtImpPreventivo"), TextBox).Text.Replace(".", "")), "NULL") & ", " _
                                            & "IMPORTO_CONSUNTIVO = " & par.IfEmpty(par.VirgoleInPunti(CType(diVoci.Cells(2).FindControl("txtImpConsuntivo"), TextBox).Text.Replace(".", "")), "NULL") _
                                            & " WHERE ID_UI = " & dgInq.Cells(0).Text & " AND ID_CONDOMINIO = " & idCondominio.Value & " AND ID_GESTIONE = " & idGestione.Value & " AND ID_VOCE = " & diVoci.Cells(1).Text
                        par.cmd.ExecuteNonQuery()
                    End If
                Next
            Next

            Response.Write("<script>alert('Aggiornamento completato!')</script>")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Sub



End Class
