Imports Telerik.Web.UI
Imports Telerik.Pdf


Partial Class RILEVAZIONI_ProvaTel
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_RILIEVO_GEST") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
 
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                IDLotto.Value = Request.QueryString("ID")
                CaricaDati()
                BindGridUnita()
                CaricaEdifici()
                CaricaIndirizzi()
                CaricaDisponibili()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Caricamento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub BindGridUnita()
        Try
            connData.apri()

            Dim Str As String = ""
            Str = "SELECT " _
                & "UNITA_IMMOBILIARI.ID,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE,EDIFICI.denominazione AS edificio," _
                & "INDIRIZZI.descrizione AS indirizzo,INDIRIZZI.civico," _
                & "UNITA_IMMOBILIARI.interno,SCALE_EDIFICI.descrizione AS scala,TIPO_LIVELLO_PIANO.descrizione AS piano," _
                & "INDIRIZZI.cap,INDIRIZZI.localita " _
                & "FROM " _
                & "siscom_mi.UNITA_IMMOBILIARI, siscom_mi.RILIEVO_UI, siscom_mi.EDIFICI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI, siscom_mi.TIPO_LIVELLO_PIANO" _
                & " WHERE " _
                & "UNITA_IMMOBILIARI.ID = RILIEVO_UI.id_unita " _
                & "AND EDIFICI.ID (+)=UNITA_IMMOBILIARI.id_edificio " _
                & "AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.id_indirizzo " _
                & "AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.id_scala " _
                & "AND TIPO_LIVELLO_PIANO.cod (+)=UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
                & "And RILIEVO_UI.id_lotto =  " & IDLotto.Value _
                & " ORDER BY edificio ASC,indirizzo ASC,civico,interno"

            'Str = "select * from siscom_mi.rapporti_utenza where substr(id,1,1)=1"
            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            RadGrid1.DataSource = dt
            RadGrid1.DataBind()
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Griglia - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub CaricaDisponibiliIndirizzo()
        Try
            Dim Str As String = "SELECT " _
                            & "UNITA_IMMOBILIARI.ID,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE,EDIFICI.denominazione AS edificio," _
                            & "INDIRIZZI.descrizione AS indirizzo,INDIRIZZI.civico," _
                            & "UNITA_IMMOBILIARI.interno,SCALE_EDIFICI.descrizione AS scala,TIPO_LIVELLO_PIANO.descrizione AS piano," _
                            & "INDIRIZZI.cap,INDIRIZZI.localita " _
                            & " FROM " _
                            & "siscom_mi.UNITA_IMMOBILIARI, siscom_mi.RILIEVO_UI, siscom_mi.EDIFICI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI, siscom_mi.TIPO_LIVELLO_PIANO " _
                            & " WHERE " _
                            & "UNITA_IMMOBILIARI.ID = RILIEVO_UI.id_unita " _
                            & " AND EDIFICI.ID (+)=UNITA_IMMOBILIARI.id_edificio " _
                            & " AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.id_indirizzo " _
                            & " AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.id_scala " _
                            & " AND TIPO_LIVELLO_PIANO.cod (+)=UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
                            & " And RILIEVO_UI.id_lotto Is NULL AND RILIEVO_UI.ID_RILIEVO=" & IDRilievo.Value _
                            & " And (INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO)= '" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Value) _
                            & "' ORDER BY edificio ASC,indirizzo ASC,civico,interno"
            connData.apri()

            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridUIDisponibili.DataSource = dt
            DataGridUIDisponibili.DataBind()
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Unità disponibili - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaDisponibili()
        Try
            Dim Str As String = "SELECT " _
                            & "UNITA_IMMOBILIARI.ID,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LET=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE,EDIFICI.denominazione AS edificio," _
                            & "INDIRIZZI.descrizione AS indirizzo,INDIRIZZI.civico," _
                            & "UNITA_IMMOBILIARI.interno,SCALE_EDIFICI.descrizione AS scala,TIPO_LIVELLO_PIANO.descrizione AS piano," _
                            & "INDIRIZZI.cap,INDIRIZZI.localita " _
                            & " FROM " _
                            & "siscom_mi.UNITA_IMMOBILIARI, siscom_mi.RILIEVO_UI, siscom_mi.EDIFICI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI, siscom_mi.TIPO_LIVELLO_PIANO " _
                            & " WHERE " _
                            & "UNITA_IMMOBILIARI.ID = RILIEVO_UI.id_unita " _
                            & " AND EDIFICI.ID (+)=UNITA_IMMOBILIARI.id_edificio " _
                            & " AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.id_indirizzo " _
                            & " AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.id_scala " _
                            & " AND TIPO_LIVELLO_PIANO.cod (+)=UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
                            & " And RILIEVO_UI.id_lotto Is NULL AND RILIEVO_UI.ID_RILIEVO=" & IDRilievo.Value _
                            & " And EDIFICI.ID = " & cmbEdificio.SelectedItem.Value _
                            & " ORDER BY edificio ASC,indirizzo ASC,civico,interno"
            connData.apri()

            par.cmd.CommandText = Str
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DataGridUIDisponibili.DataSource = dt
            DataGridUIDisponibili.DataBind()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Unità disponibili - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaIndirizzi()
        Try
            connData.apri()
            par.caricaComboBox("SELECT DISTINCT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO AS INDIRIZZO FROM SISCOM_MI.INDIRIZZI,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.RILIEVO_UI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=RILIEVO_UI.id_unita AND RILIEVO_UI.id_lotto IS NULL ORDER BY INDIRIZZO ASC", cmbIndirizzo, "INDIRIZZO", "INDIRIZZO", True, "-1", "- - -")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Indirizzi - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaEdifici()
        Try
            connData.apri()
            par.caricaComboBox("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and RILIEVO_UI.id_lotto is null order by edifici.denominazione", cmbEdificio, "ID", "DENOMINAZIONE", True, "-1", "- - -")
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Edifici - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaDati()
        Try
            connData.apri()

            Dim Str As String = ""
            Str = "select rilievo_tab_utenti.id_rilievo, RILIEVO_TAB_UTENTI.DESCRIZIONE AS UTENTE,RILIEVO_LOTTI.* from SISCOM_MI.RILIEVO_TAB_UTENTI,SISCOM_MI.RILIEVO_LOTTI where RILIEVO_TAB_UTENTI.ID=RILIEVO_LOTTI.ID_UTENTE AND RILIEVO_LOTTI.ID=" & IDLotto.Value
            par.cmd.CommandText = Str
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblLotto.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                lblUtente.Text = par.IfNull(myReader("UTENTE"), "")
                IDRilievo.Value = par.IfNull(myReader("ID_RILIEVO"), "")
            End If
            myReader.Close()
            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Dati - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub NavigationMenu_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles NavigationMenu.MenuItemClick
        Try
            If optMenu.Value = 0 Then
                Select Case NavigationMenu.SelectedValue

                    Case "Esci"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
                        'noClose.Value = 0
                End Select
            Else
                par.modalDialogMessage("Attenzione", "Chiudere la pagina in cui si sta lavorando prima di Procedere!", Me.Page)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - NavigationMenu_MenuItemClick - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        noClose.Value = "1"
        optMenu.Value = "0"
    End Sub

    Protected Sub btnEliminaElemento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaElemento.Click
        If LBLID.Value <> "" Then
            Try
                connData.apri()
                par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_UI SET ID_LOTTO=NULL WHERE ID_UNITA = " & Me.LBLID.Value & " AND ID_LOTTO=" & IDLotto.Value
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
                Me.LBLID.Value = ""
                BindGridUnita()

            Catch EX1 As Data.OracleClient.OracleException
                connData.chiudi()
                Me.lblErrore.Visible = True
                If EX1.Code = 2292 Then
                    lblErrore.Text = "Elemento in uso. Non è possibile eliminare!"
                Else
                    lblErrore.Text = EX1.Message
                End If
            Catch ex As Exception
                connData.chiudi()
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        Else
            par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        End If
    End Sub

    Protected Sub btnSalvaDen_Click(sender As Object, e As System.EventArgs) Handles btnSalvaDen.Click
        If DataGridUIDisponibili.Items.Count > 0 Then
            Try
                connData.apri()
                Dim j As Boolean = False
                For i As Integer = 0 To DataGridUIDisponibili.Items.Count - 1
                    If DirectCast(DataGridUIDisponibili.Items(i).Cells(1).FindControl("ChSelezionato"), CheckBox).Checked = True Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.RILIEVO_UI SET ID_LOTTO=" & IDLotto.Value & " WHERE ID_UNITA=" & DataGridUIDisponibili.Items(i).Cells(1).Text & " AND ID_RILIEVO=" & IDRilievo.Value
                        par.cmd.ExecuteNonQuery()
                        j = True
                    End If
                Next
                connData.chiudi()
                If j = True Then
                    par.modalDialogMessage("Info", "Operazione effettuata!", Me.Page)
                    Me.TextBox1.Value = "0"
                    Me.LBLID.Value = ""
                    BindGridUnita()
                    cmbEdificio.SelectedIndex = -1
                    cmbEdificio.Items.FindByValue("-1").Selected = True
                    CaricaDisponibili()
                Else
                    par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
                End If

            Catch ex As Exception
                connData.chiudi()
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        Else
            par.modalDialogMessage("Attenzione", "Nessun elemento selezionato!", Me.Page)
        End If
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        cmbIndirizzo.SelectedIndex = -1
        cmbIndirizzo.Items.FindByValue("-1").Selected = True
        CaricaDisponibili()
    End Sub

    Protected Sub btnSelezionaTutti_Click(sender As Object, e As System.EventArgs) Handles btnSelezionaTutti.Click
        For Each riga As DataGridItem In DataGridUIDisponibili.Items
            If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False Then
                CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True
            End If
        Next
    End Sub

    Protected Sub btnDeselezionaTutti_Click(sender As Object, e As System.EventArgs) Handles btnDeselezionaTutti.Click
        For Each riga As DataGridItem In DataGridUIDisponibili.Items
            If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True Then
                CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False
            End If
        Next
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        cmbEdificio.SelectedIndex = -1
        cmbEdificio.Items.FindByValue("-1").Selected = True
        CaricaDisponibiliIndirizzo()
    End Sub


    Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        'Try
        '    connData.apri()

        '    Dim Str As String = ""
        '    Str = "SELECT " _
        '        & "UNITA_IMMOBILIARI.ID,replace(replace('<a href=£javascript:AfterSubmit()£ onclick=£window.open(''../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1$ID='||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||''',''Dettagli'',''height=580,top=0,left=0,width=780'');£>'||UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_UNITA_IMMOBILIARE,EDIFICI.denominazione AS edificio," _
        '        & "INDIRIZZI.descrizione AS indirizzo,INDIRIZZI.civico," _
        '        & "UNITA_IMMOBILIARI.interno,SCALE_EDIFICI.descrizione AS scala,TIPO_LIVELLO_PIANO.descrizione AS piano," _
        '        & "INDIRIZZI.cap,INDIRIZZI.localita " _
        '        & "FROM " _
        '        & "siscom_mi.UNITA_IMMOBILIARI, siscom_mi.RILIEVO_UI, siscom_mi.EDIFICI, siscom_mi.INDIRIZZI, siscom_mi.SCALE_EDIFICI, siscom_mi.TIPO_LIVELLO_PIANO" _
        '        & " WHERE " _
        '        & "UNITA_IMMOBILIARI.ID = RILIEVO_UI.id_unita " _
        '        & "AND EDIFICI.ID (+)=UNITA_IMMOBILIARI.id_edificio " _
        '        & "AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.id_indirizzo " _
        '        & "AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.id_scala " _
        '        & "AND TIPO_LIVELLO_PIANO.cod (+)=UNITA_IMMOBILIARI.cod_tipo_livello_piano " _
        '        & "And RILIEVO_UI.id_lotto =  " & IDLotto.Value _
        '        & " ORDER BY edificio ASC,indirizzo ASC,civico,interno"

        '    par.cmd.CommandText = Str
        '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    Dim dt As New Data.DataTable
        '    da.Fill(dt)
        '    'RadGrid1.DataSource = dt
        '    'RadGrid1.DataBind()
        '    connData.chiudi()
        '    TryCast(sender, RadGrid).DataSource = dt

        'Catch ex As Exception
        '    connData.chiudi()
        '    Session.Add("ERRORE", "Provenienza: Dettaglio Lotto-Unità - Carica Griglia - " & ex.Message)
        '    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        'End Try


    End Sub

    Protected Sub RadGrid1_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            RadGrid1.CurrentPageIndex = e.NewPageIndex
            BindGridUnita()
        End If
    End Sub
End Class
