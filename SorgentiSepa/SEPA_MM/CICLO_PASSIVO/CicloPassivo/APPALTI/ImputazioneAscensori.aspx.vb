
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_ImputazioneAscensori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim msgVisto As Boolean = False
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
            Session.Add("PrenAttAuto", 0)

            If Request.QueryString("IDCON") <> "" Then
                vIdConnModale = Request.QueryString("IDCON")
            End If
            If Not IsPostBack Then
                HiddenAttiva.Value = par.IfEmpty(Request.QueryString("ATTCONTR"), "0")
                HFGriglia.Value = dgvScadenze.ClientID
                If par.IfEmpty(Request.QueryString("IdAppalto"), 0) > 0 Then
                    HiddenIdContratto.Value = Request.QueryString("IdAppalto")
                    HiddenEsercizio.Value = Request.QueryString("ELENCOPREZZI")
                    LoadTable()
                Else

                End If
            End If
            If Request.QueryString("SL") = 1 Then
                btnSchedaImputazione.Visible = False

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub LoadTable()
        ' RIPRENDO LA CONNESSIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        'RIPRENDO LA TRANSAZIONE
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans

        Dim stringaImporti As String = ""
        Dim indice As Integer = 0
        par.cmd.CommandText = "SELECT SISCOM_MI.PF_VOCI_IMPORTO.ID,(PF_VOCI.CODICE ||' - '||SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS DESCRIZIONE   " _
                                    & "FROM SISCOM_MI.PF_VOCI_IMPORTO,siscom_mi.pf_voci, SISCOM_MI.APPALTI_LOTTI_SERVIZI" _
                                    & " WHERE  pf_voci.ID = pf_voci_importo.id_voce " _
                                    & "AND APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE > 0 AND PF_VOCI_IMPORTO.ID = ID_PF_VOCE_IMPORTO AND ID_APPALTO = " & Request.QueryString("IdAppalto")
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While lettore.Read
            Dim idPfVoceImporto As String = ""
            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                & " CONNECT BY PRIOR ID = ID_OLD " _
                                & " START WITH ID = " & par.IfNull(lettore("id"), 0) _
                                & " UNION " _
                                & " SELECT id FROM SISCOM_MI.PF_VOCI_IMPORTO  " _
                                & " CONNECT BY PRIOR ID_OLD=ID " _
                                & " START WITH ID = " & par.IfNull(lettore("id"), 0)
            Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore1.Read
                idPfVoceImporto &= par.IfNull(lettore1("id"), 0) & ","
            End While
            If Not String.IsNullOrEmpty(idPfVoceImporto) Then
                idPfVoceImporto = idPfVoceImporto.Substring(0, idPfVoceImporto.LastIndexOf(","))
            End If
            lettore1.Close()
            Dim descrizione As String = par.IfNull(lettore("descrizione").ToString, "")
            If Not String.IsNullOrEmpty(descrizione) Then
                Dim indiceInizio As Integer = 0
                If descrizione.Contains("-") Then
                    indiceInizio = descrizione.IndexOf("-") + 2
                End If
                Dim indiceFine As Integer = 0
                If descrizione.Contains("(") Then
                    indiceFine = descrizione.IndexOf("(") - indiceInizio - 1
                End If
                If indiceInizio <> 0 AndAlso indiceFine <> 0 Then
                    descrizione = descrizione.Substring(indiceInizio, indiceFine)
                Else
                    descrizione = descrizione.Substring(indiceInizio)
                End If
                If descrizione.Length > 30 Then
                    descrizione = Left(descrizione, 27) & "..."
                End If
            End If
            indice += 1
            stringaImporti &= ",(SELECT MAX(A.ID) FROM SISCOM_MI.PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") And A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " And A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3) AS ID" & indice _
                    & " ,(SELECT SUM(A.IMPORTO_PRENOTATO) FROM PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") And A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " And A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3) AS IMPORTO_PRENOTATO" & indice _
                    & " ,(SELECT SUM(NVL(A.IMPORTO_APPROVATO,0)) FROM PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") And A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " And A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3) AS IMPORTO_APPROVATO" & indice _
                    & " ,(SELECT SUM(NVL(A.IMPORTO_LIQUIDATO,0)) FROM PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") And A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " And A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3) AS IMPORTO_LIQUIDATO" & indice

        End While
        For i As Integer = 1 To 5 - indice
            stringaImporti &= ",0 AS ID" & indice + i _
                    & " ,0 AS IMPORTO_PRENOTATO" & indice + i _
                    & " ,0 AS IMPORTO_APPROVATO" & indice + i _
                    & " ,0 AS IMPORTO_LIQUIDATO" & indice + i
        Next

        lettore.Close()
        par.cmd.CommandText = "select rownum, a.* from (Select distinct ID_APPALTO,TIPO_PAGAMENTO,PRENOTAZIONI.PERC_IVA,TO_DATE(DATA_SCADENZA,'YYYYmmdd') AS SCADENZA,  prenotazioni.data_scadenza " _
                & stringaImporti _
                        & " FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & Request.QueryString("IdAppalto") & ")" _
                & " and PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO  " _
                & " and id_voce_pf_importo in (select b.id from siscom_mi.pf_voci_importo b connect by prior b.id=b.id_old start with b.id in (" _
                & "SELECT SISCOM_MI.PF_VOCI_IMPORTO.ID " _
                & "FROM SISCOM_MI.PF_VOCI_IMPORTO,siscom_mi.pf_voci, SISCOM_MI.APPALTI_LOTTI_SERVIZI" _
                & " WHERE  pf_voci.ID = pf_voci_importo.id_voce " _
                & "AND APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE > 0 AND PF_VOCI_IMPORTO.ID = ID_PF_VOCE_IMPORTO AND ID_APPALTO = " & Request.QueryString("IdAppalto") _
                & " )) " _
                & " AND ID_STATO <> -3 and tipo_pagamento = 6 AND IMPORTO_PRENOTATO>0 ORDER BY prenotazioni.data_scadenza ASC) a  "
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Add("DT_PRENOTAZIONI", dt)
        'par.cmd.CommandText = "SELECT PRENOTAZIONI.ID,ID_APPALTO,TIPO_PAGAMENTO,PRENOTAZIONI.PERC_IVA,PF_VOCI_IMPORTO.DESCRIZIONE,TO_CHAR(TO_DATE(DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"", " _
        '            & "TRIM(TO_CHAR(NVL(IMPORTO_PRENOTATO,0),'9G999G999G999G990D99')) AS IMPORTO_PRENOTATO," _
        '            & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'9G999G999G999G990D99')) AS IMPORTO_APPROVATO," _
        '            & "TRIM(TO_CHAR(NVL(IMPORTO_LIQUIDATO,0),'9G999G999G999G990D99')) AS IMPORTO_LIQUIDATO " _
        '            & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & Request.QueryString("IdAppalto") & ")" _
        '            & " and PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO /*and (ID_VOCE_PF_IMPORTO = " & Me.cmbvoceRiepilogo.SelectedValue & " or ID_VOCE_PF_IMPORTO = (select id from siscom_mi.pf_voci_importo pf2 where pf2.id_old = " & Me.cmbvoceRiepilogo.SelectedValue & "))*/ " _
        '            & " and id_voce_pf_importo in (select b.id from siscom_mi.pf_voci_importo b connect by prior b.id=b.id_old start with b.id=" & Me.cmbvoceRiepilogo.SelectedValue & ") " _
        '            & " AND ID_STATO <> -3 and tipo_pagamento = 6 AND IMPORTO_PRENOTATO>0 ORDER BY PRENOTAZIONI.DATA_SCADENZA ASC "

        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        'Dim dt As New Data.DataTable()

        'da.Fill(dt)

        'dgvScadenze.DataSource = dt
        'dgvScadenze.DataBind()
        'Me.cmbvoce.SelectedValue = Me.cmbvoceRiepilogo.SelectedValue
        'Me.cmbvoce.Enabled = False

        'Dim totale As Decimal = 0
        'For Each row As Data.DataRow In dt.Rows
        '    totale = totale + par.IfNull(row.Item("IMPORTO_PRENOTATO"), 0)
        'Next
        'Me.txtTotale.Text = Format(totale, "##,##0.00")

        'par.cmd.CommandText = "SELECT IMPORTO_CANONE,SCONTO_CANONE, ONERI_SICUREZZA_CANONE, IVA_CANONE " _
        '                    & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE  ID_APPALTO = " & Request.QueryString("IdAppalto") _
        '                    & " AND ID_PF_VOCE_IMPORTO = " & Me.cmbvoceRiepilogo.SelectedValue
        'Dim lettMaxImp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        'Dim appoggio As Decimal = 0
        'If lettMaxImp.Read Then
        '    appoggio = CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) - Math.Round(((CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) * par.IfNull(lettMaxImp("SCONTO_CANONE"), 0)) / 100), 4)
        '    appoggio = appoggio + par.IfNull(lettMaxImp("ONERI_SICUREZZA_CANONE"), 0)

        '    appoggio = appoggio + ((appoggio * par.IfNull(lettMaxImp("IVA_CANONE"), 0)) / 100)
        '    Me.ImpMassimo.Value = appoggio
        'End If

        'lettMaxImp.Close()

        'par.cmd.CommandText = "select sum(nvl(importo,0)) as variazione from siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni where id_appalto = " & Request.QueryString("IdAppalto") & " and appalti_variazioni.id = id_variazione and appalti_variazioni.id_tipologia = 5"
        'lettMaxImp = par.cmd.ExecuteReader
        'If lettMaxImp.Read Then
        '    Me.ImpMassimo.Value += Math.Round(CDec(par.IfNull(lettMaxImp("variazione"), 0)), 2)
        'End If
        'lettMaxImp.Close()

        'If Me.IdStato.Value = 1 Then
        '    For Each di As GridDataItem In dgvScadenze.Items
        '        DirectCast(di.FindControl("txtImporto1"), TextBox).ReadOnly = True
        '        DirectCast(di.FindControl("txtImporto2"), TextBox).ReadOnly = True
        '        DirectCast(di.FindControl("txtImporto3"), TextBox).ReadOnly = True
        '        DirectCast(di.FindControl("txtImporto4"), TextBox).ReadOnly = True
        '        DirectCast(di.FindControl("txtImporto5"), TextBox).ReadOnly = True
        '    Next
        'Else
        '    dgvScadenze.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
        '    dgvScadenze.Rebind()
        'End If
    End Sub


    Private Sub PrenotazCustodi(ByVal dtPrenot As Data.DataTable, ByVal idVoce As String, ByRef Esito As Boolean)
        Try
            Esito = True
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            '' ########## cancellazione di eventuali prenotazioni già fatte su questo appalto per la voce del 10%
            'par.cmd.CommandText = "delete from siscom_mi.prenotazioni where id_appalto = " & Request.QueryString("IdAppalto") & " and id_voce_pf = " & idVoce
            'par.cmd.ExecuteNonQuery()
            '' ########## cancellazione non consentita per problemi di lentezza di eventuali prenotazioni già fatte su questo appalto per la voce del 10% aggiorno a importo = 0
            par.cmd.CommandText = "update siscom_mi.prenotazioni set  id_stato = 0,importo_approvato = 0,importo_prenotato = 0 where id_appalto = " & Request.QueryString("IdAppalto") & " and id_voce_pf = " & idVoce & " and id_pagamento is null"
            par.cmd.ExecuteNonQuery()


            Dim impNovanta As Decimal = 0
            Dim impCento As Decimal = 0
            Dim impDieci As Decimal = 0
            Dim newId As String = ""
            Dim letNextVal As Oracle.DataAccess.Client.OracleDataReader
            'Dim letExist As Oracle.DataAccess.Client.OracleDataReader
            For Each r As Data.DataRow In dtPrenot.Rows
                If par.IfNull(r.Item("IMPORTO_PRENOTATO"), 0) <> 0 Then
                    impNovanta = r.Item("IMPORTO_PRENOTATO")
                    impCento = Math.Round(((impNovanta * 100) / 90), 2)
                    impDieci = Math.Round(((impCento * 10) / 100), 2)
                    newId = ""
                    If impDieci <> 0 Then
                        'par.cmd.CommandText = "select * from siscom_mi.prenotazioni " _
                        '                    & "where id_appalto = " & Request.QueryString("IdAppalto") & " " _
                        '                    & "and id_voce_pf = " & idVoce & " and id_fornitore = " & r.Item("id_fornitore")
                        'letExist = par.cmd.ExecuteReader
                        'If letExist.Read Then
                        '    '######### UPDATE DELLA PRENOTAZIONE IMPORTO AL 10%
                        '    par.cmd.CommandText = "update siscom_mi.prenotazioni set importo_prenotato = " & par.VirgoleInPunti(impDieci) & " where id = " & letExist("ID")
                        '    par.cmd.ExecuteNonQuery()
                        'Else

                        '######### INSERT DELLA PRENOTAZIONE IMPORTO AL 10%
                        par.cmd.CommandText = "select siscom_mi.SEQ_PRENOTAZIONI.NEXTVAL from dual"
                        letNextVal = par.cmd.ExecuteReader
                        If letNextVal.Read Then
                            newId = letNextVal(0)
                        End If
                        letNextVal.Close()
                        If newId <> "" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_STRUTTURA,PERC_IVA) VALUES" _
                            & "(" & newId & ", '" & r.Item("DATA_PRENOTAZIONE") & "', " & par.VirgoleInPunti(impDieci) & " ," & r.Item("ID_FORNITORE") & " ," & r.Item("ID_APPALTO") & "," _
                            & idVoce & "," & r.Item("TIPO_PAGAMENTO") & "," & r.Item("ID_STATO") & ", 'CONTRATTO DI APPALTO 10%'," & r.Item("DATA_SCADENZA") & "," _
                            & r.Item("ID_STRUTTURA") & "," & r.Item("PERC_IVA") & ")"
                            par.cmd.ExecuteNonQuery()
                        End If

                        'End If
                        'letExist.Close()


                    End If


                End If
            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            Esito = False
            Exit Sub
        End Try

    End Sub



    'Protected Sub dgvScadenze_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvScadenze.ItemCommand
    '    Try
    '        Select Case e.CommandName
    '            Case "SchedaImputazione"
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "VisSchedaImputazione();", True)
    '        End Select
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

    'Protected Sub dgvScadenze_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvScadenze.ItemDataBound

    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la prenotazione con scadenza: " & e.Item.Cells(3).Text & "';document.getElementById('idPrenotazione').value=" & e.Item.Cells(0).Text & ";")
    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la prenotazione con scadenza: " & e.Item.Cells(3).Text & "';document.getElementById('idPrenotazione').value=" & e.Item.Cells(0).Text & ";")
    '    End If

    'End Sub
    Protected Sub dgvScadenze_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvScadenze.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            '   e.Item.Attributes.Add("onclick", "document.getElementById('Tab_Servizio_txtIdComponente').value='" & dataItem("ID_PF_VOCE_IMPORTO").Text & "';document.getElementById('Tab_Servizio_txtIdComponente0').value='" & dataItem("ID").Text & "';document.getElementById('Tab_Servizio_txtIdComponente1').value='" & dataItem("ID_SERVIZIO").Text & "';")
            'e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato la prenotazione con scadenza: " & e.Item.Cells(par.IndRDGC(dgvScadenze, "SCADENZA")).Text & "';document.getElementById('idPrenotazione').value=" & e.Item.Cells(par.IndRDGC(dgvScadenze, "ID")).Text & ";")
            e.Item.Attributes.Add("onclick", "document.getElementById('idPrenotazione').value=" & e.Item.Cells(par.IndRDGC(dgvScadenze, "ID1")).Text & ";")
        End If
    End Sub


    Private Function ImportMassimo(ByVal id_pf_voce As String) As Decimal
        ImportMassimo = 0

        Try
            par.cmd.CommandText = "SELECT IMPORTO_CANONE,SCONTO_CANONE, ONERI_SICUREZZA_CANONE, IVA_CANONE, " _
                    & "(select sum(nvl(importo,0)) from siscom_mi.appalti_variazioni,siscom_mi.appalti_variazioni_importi where appalti_variazioni.id_appalto = " & Request.QueryString("IdAppalto") & " and appalti_variazioni_importi.id_variazione = appalti_variazioni.id and id_tipologia = 5 and ID_PF_VOCE_IMPORTO = " & id_pf_voce & " )as variazione " _
                    & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE  ID_APPALTO = " & Request.QueryString("IdAppalto") _
                    & " AND ID_PF_VOCE_IMPORTO = " & id_pf_voce
            Dim lettMaxImp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim appoggio As Decimal = 0
            If lettMaxImp.Read Then
                appoggio = CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) - Math.Round(((CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) * par.IfNull(lettMaxImp("SCONTO_CANONE"), 0)) / 100), 4)
                appoggio = appoggio + par.IfNull(lettMaxImp("ONERI_SICUREZZA_CANONE"), 0)
                appoggio = appoggio + ((appoggio * par.IfNull(lettMaxImp("IVA_CANONE"), 0)) / 100)
                appoggio += CDec(par.IfNull(lettMaxImp("variazione"), 0))
                ImportMassimo = Math.Round(appoggio, 2)
            End If

            lettMaxImp.Close()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        Return ImportMassimo

    End Function




    'Protected Sub dgvScadenze_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvScadenze.NeedDataSource
    '    Dim StringaSql As String

    '    Try

    '        If par.IfEmpty(Me.cmbvoceRiepilogo.SelectedValue, "-1") <> "-1" Then

    '            ' RIPRENDO LA CONNESSIONE
    '            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)

    '            'RIPRENDO LA TRANSAZIONE
    '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
    '            '‘par.cmd.Transaction = par.myTrans

    '            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID,ID_APPALTO,TIPO_PAGAMENTO,PRENOTAZIONI.PERC_IVA,PF_VOCI_IMPORTO.DESCRIZIONE,TO_CHAR(TO_DATE(DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"", " _
    '                        & "TRIM(TO_CHAR(NVL(IMPORTO_PRENOTATO,0),'9G999G999G999G990D99')) AS IMPORTO_PRENOTATO," _
    '                        & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'9G999G999G999G990D99')) AS IMPORTO_APPROVATO," _
    '                        & "TRIM(TO_CHAR(NVL(IMPORTO_LIQUIDATO,0),'9G999G999G999G990D99')) AS IMPORTO_LIQUIDATO " _
    '                        & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & Request.QueryString("IdAppalto") & ")" _
    '                        & " AND importo_prenotato > 0  " _
    '                        & " and PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO /*and (ID_VOCE_PF_IMPORTO = " & Me.cmbvoceRiepilogo.SelectedValue & " or ID_VOCE_PF_IMPORTO = (select id from siscom_mi.pf_voci_importo pf2 where pf2.id_old = " & Me.cmbvoceRiepilogo.SelectedValue & "))*/ " _
    '                        & " and id_voce_pf_importo in (select b.id from siscom_mi.pf_voci_importo b connect by prior b.id=b.id_old start with b.id=" & Me.cmbvoceRiepilogo.SelectedValue & ") " _
    '                        & " AND ID_STATO <> -3 and tipo_pagamento = 6 ORDER BY PRENOTAZIONI.DATA_SCADENZA ASC "
    '            '
    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            ' Dim dt As New Data.DataTable()

    '            ' da.Fill(dt)
    '            'dgvScadenze.DataSource = dt
    '            'dgvScadenze.DataBind()
    '            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
    '            TryCast(sender, RadGrid).DataSource = dt
    '            Me.cmbvoce.SelectedValue = Me.cmbvoceRiepilogo.SelectedValue
    '            Me.cmbvoce.Enabled = False

    '            Dim totale As Decimal = 0
    '            For Each row As Data.DataRow In dt.Rows
    '                totale = totale + par.IfNull(row.Item("IMPORTO_PRENOTATO"), 0)
    '            Next
    '            Me.txtTotale.Text = Format(totale, "##,##0.00")

    '            par.cmd.CommandText = "SELECT IMPORTO_CANONE,SCONTO_CANONE, ONERI_SICUREZZA_CANONE, IVA_CANONE " _
    '                                & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE  ID_APPALTO = " & Request.QueryString("IdAppalto") _
    '                                & " AND ID_PF_VOCE_IMPORTO = " & Me.cmbvoceRiepilogo.SelectedValue
    '            Dim lettMaxImp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '            Dim appoggio As Decimal = 0
    '            If lettMaxImp.Read Then
    '                appoggio = CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) - Math.Round(((CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) * par.IfNull(lettMaxImp("SCONTO_CANONE"), 0)) / 100), 4)
    '                appoggio = appoggio + par.IfNull(lettMaxImp("ONERI_SICUREZZA_CANONE"), 0)

    '                appoggio = appoggio + ((appoggio * par.IfNull(lettMaxImp("IVA_CANONE"), 0)) / 100)
    '                Me.ImpMassimo.Value = appoggio
    '            End If

    '            lettMaxImp.Close()

    '            par.cmd.CommandText = "select sum(nvl(importo,0)) as variazione from siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni where id_appalto = " & Request.QueryString("IdAppalto") & " and appalti_variazioni.id = id_variazione and appalti_variazioni.id_tipologia = 5"
    '            lettMaxImp = par.cmd.ExecuteReader
    '            If lettMaxImp.Read Then
    '                Me.ImpMassimo.Value += Math.Round(CDec(par.IfNull(lettMaxImp("variazione"), 0)), 2)
    '            End If
    '            lettMaxImp.Close()

    '            If Me.IdStato.Value = 1 Then
    '                For Each di As GridDataItem In dgvScadenze.Items
    '                    DirectCast(di.FindControl("txtImporto"), TextBox).ReadOnly = True
    '                Next
    '            Else
    '                Me.btnDelete.Visible = False
    '            End If
    '            AddJavascriptFunction()
    '        Else
    '            Me.btnSalva.Visible = False
    '            Me.btnDelete.Visible = False
    '            Me.btn_InserisciAppalti.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If IdStato.Value = 0 Then
            dgvScadenze.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            dgvScadenze.Rebind()
        End If


        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub



    Private Sub btnAggiorna_Click(sender As Object, e As EventArgs) Handles btnAggiorna.Click
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
            Dim idpiano As Integer = par.cmd.ExecuteScalar
            Dim dtTotale As Data.DataTable = Session.Item("DT_PRENOTAZIONI")
            par.cmd.CommandText = "SELECT IMPIANTI.ID, (SELECT    COMPLESSI_IMMOBILIARI.COD_COMPLESSO || ' - ' || COMPLESSI_IMMOBILIARI.DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID = IMPIANTI.ID_COMPLESSO)AS COMPLESSO, " _
                           & " (SELECT COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO) AS COD_EDIFICIO, (SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO) AS NOME_EDIFICIO, " _
                           & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_FORNITORI_EDIFICI WHERE     ID_EDIFICIO = IMPIANTI.ID_EDIFICIO AND ID_PF = 0 AND ID_TIPO_SPESA = 3)) AS FORNITORE, " _
                           & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_FORNITORI_EDIFICI WHERE     ID_EDIFICIO = IMPIANTI.ID_EDIFICIO AND ID_PF = 0 AND ID_TIPO_SPESA = 3) AS ID_FORNITORE, I_SOLLEVAMENTO_TMP.MATRICOLA AS MATRICOLA_IMPIANTO, " _
                           & " (SELECT INDIRIZZI.DESCRIZIONE || ' ' || INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI WHERE ID IN (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO)) AS INDIRIZZO_IMPIANTO, " _
                           & " (SELECT WM_CONCAT (DESCRIZIONE) FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.IMPIANTI_SCALE WHERE ID_IMPIANTO = IMPIANTI.ID)) AS SCALA_IMPIANTO,  " _
                           & " (SELECT LOCALITA FROM SISCOM_MI.INDIRIZZI WHERE ID IN (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO)) AS LOCALITA, TIPOLOGIA, NUM_FERMATE AS FERMATE, " _
                           & " (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU_VISITA_MENSILE, I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI,  " _
                           & " I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS TOTALE_IMPIANTO, " _
                           & " I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))  " _
                           & " * (  1 -  (SELECT MAX(SCONTO_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100)  " _
                           & " * (  1 +   (SELECT MAX(IVA_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100) AS TOT_ANNUO_SCONTATO, " _
                           & " ROUND (I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))  " _
                           & " * (  1 -   (SELECT MAX(SCONTO_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100)  " _
                           & " * (  1 +    (SELECT MAX(IVA_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100)  " _
                           & " , 2) AS TOT_ANNUO_SCONTATO_RETT  " _
                           & " FROM SISCOM_MI.IMPIANTI, SISCOM_MI.I_SOLLEVAMENTO_TMP, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.EDIFICI " _
                           & " WHERE     IMPIANTI.ID = I_SOLLEVAMENTO_TMP.ID " _
                           & " AND COD_TIPOLOGIA = 'SO' " _
                           & " AND EDIFICI.ID = IMPIANTI.ID_EDIFICIO  " _
                           & " AND IMPIANTI.ID_EDIFICIO = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                           & " AND SISCOM_MI.APPALTI_LOTTI_PATRIMONIO. ID_APPALTO =  " & HiddenIdContratto.Value _
                           & "AND I_SOLLEVAMENTO_TMP.ID_PRENOTAZIONE=" & idPrenotazione.Value

            Dim dtAscensori As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Dim totale As Decimal = 0
            If dtAscensori.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dtAscensori.Rows
                    totale += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
                Next
                totale = Math.Round(totale, 2)
                Dim idStato As Integer = 0
                Dim totaleApprovato As Decimal = 0
                Dim rigaTotale1 As Data.DataRow() = dtTotale.Select("ID1 = " & idPrenotazione.Value)
                par.cmd.CommandText = "SELECT ID_STATO, IMPORTO_APPROVATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & rigaTotale1(0).Item("ID1")
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    idStato = MyReader("ID_STATO")
                    totaleApprovato = MyReader("IMPORTO_APPROVATO")
                End If
                MyReader.Close()
                If idStato >= 1 Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONI_ASCENSORI WHERE ID_PRENOTAZIONE = " & rigaTotale1(0).Item("ID1")
                    par.cmd.ExecuteNonQuery()
                    For Each riga As Data.DataRow In dtAscensori.Rows
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.IMPUTAZIONI_ASCENSORI ( " _
                                    & "    ID_EDIFICIO, ID_PRENOTAZIONE, IMPORTO,  " _
                                    & "    ID_IMPIANTO)  " _
                                    & " VALUES ( " _
                                    & "(SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = " & riga.Item("COD_EDIFICIO") & ") /* ID_EDIFICIO */, " _
                                    & rigaTotale1(0).Item("ID1") & "  /* ID_PRENOTAZIONE */, " _
                                    & par.VirgoleInPunti(Math.Round(par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0) / totale * totaleApprovato, 4)) & "  /* IMPORTO */, " _
                                    & riga.Item("id") & "  /* ID_IMPIANTO */ ) "
                        par.cmd.ExecuteNonQuery()
                    Next
                End If
            End If



            Dim rigaTotale As Data.DataRow() = dtTotale.Select("ID1 = " & idPrenotazione.Value)

            par.cmd.CommandText = "update siscom_mi.prenotazioni " _
            & " set importo_prenotato = " & par.VirgoleInPunti(totale) _
            & " where id = " & rigaTotale(0).Item("ID1") & " and id_Stato<=0"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = " SELECT SUM(IMPORTO_PRENOTATO) " _
                            & " FROM SISCOM_MI.PRENOTAZIONI WHERE TIPO_PAGAMENTO=6 AND " _
                            & " ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI " _
                            & " WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & HiddenIdContratto.Value & "))"
            Dim importoPrenotato As Decimal = CDec(par.IfNull(par.cmd.ExecuteScalar, 0))

            par.cmd.CommandText = "SELECT SUM((IMPORTO_CANONE-ROUND(IMPORTO_CANONE*SCONTO_CANONE/100,2)+ONERI_SICUREZZA_CANONE)*(1+IVA_CANONE/100)) AS IMPORTO " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = " & HiddenIdContratto.Value
            Dim importoCanone As Decimal = CDec(par.IfNull(par.cmd.ExecuteScalar, 0))

            If importoPrenotato > importoCanone Then
                RadWindowManager1.RadAlert("La somma degli importi supera il massimo prenotabile per questo contratto!", 300, 150, "Attenzione", "", "null")
            End If

            LoadTable()

            dgvScadenze.Rebind()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub dgvScadenze_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvScadenze.NeedDataSource
        Try
            Dim DT As Data.DataTable = Session.Item("DT_PRENOTAZIONI")
            TryCast(sender, RadGrid).DataSource = DT
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class


