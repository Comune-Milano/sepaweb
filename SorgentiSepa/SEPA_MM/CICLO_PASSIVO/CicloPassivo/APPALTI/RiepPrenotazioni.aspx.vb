Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_RiepPrenotazioni
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

                If par.IfEmpty(Request.QueryString("IdAppalto"), 0) > 0 Then
                    CaricaDati()
                    LoadTable()
                Else

                End If
                Me.txtScadenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                'Me.txtScadenza.Attributes.Add("onBlur", "javascript:NuovaIva();")


                'Me.txtImporto.Attributes.Add("onBlur", "javascript:SostPuntVirg(event,this);")
                Me.txtImporto.Attributes.Add("onkeyup", "javascript:valid(this,'onlynumbers');SostPuntVirg(event,this);")
            End If
            If Request.QueryString("SL") = 1 Then
                dgvScadenze.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
                dgvScadenze.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                dgvScadenze.Rebind()
                Me.btnSalva.Visible = False
                Me.btn_InserisciAppalti.Visible = False
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CaricaDati()
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.APPALTI WHERE ID = " & Request.QueryString("IdAppalto")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Me.IdStato.Value = par.IfNull(lettore("ID_STATO"), 0)
            End If
            lettore.Close()


            par.cmd.CommandText = "SELECT SISCOM_MI.PF_VOCI_IMPORTO.ID, (PF_VOCI.CODICE ||' - '||SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS DESCRIZIONE   " _
                                    & "FROM SISCOM_MI.PF_VOCI_IMPORTO,siscom_mi.pf_voci, SISCOM_MI.APPALTI_LOTTI_SERVIZI" _
                                    & " WHERE  pf_voci.ID = pf_voci_importo.id_voce " _
                                    & "AND APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE > 0 AND PF_VOCI_IMPORTO.ID = ID_PF_VOCE_IMPORTO AND ID_APPALTO = " & Request.QueryString("IdAppalto")
            lettore = par.cmd.ExecuteReader
            Me.cmbvoceRiepilogo.Items.Clear()
            Me.cmbvoce.Items.Clear()
            While lettore.Read
                cmbvoce.Items.Add(New RadComboBoxItem(par.IfNull(lettore("DESCRIZIONE"), " "), par.IfNull(lettore("ID"), -1)))
                cmbvoceRiepilogo.Items.Add(New RadComboBoxItem(par.IfNull(lettore("DESCRIZIONE"), " "), par.IfNull(lettore("ID"), -1)))
            End While
            lettore.Close()
            'par.caricaComboTelerik(par.cmd.CommandText, cmbvoceRiepilogo, "ID", "DESCRIZIONE", False)
            'If cmbvoceRiepilogo.Items.Count = 2 Then
            '    Me.cmbvoceRiepilogo.Items(1).Selected = True
            '    cmbvoceRiepilogo.Items(0).Remove()
            'End If

            'par.caricaComboTelerik(par.cmd.CommandText, cmbvoce, "ID", "DESCRIZIONE", True)
            'If cmbvoce.Items.Count = 2 Then
            '    Me.cmbvoce.Items(1).Selected = True
            '    cmbvoce.Items(0).Remove()
            'End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub
    Private Sub LoadTable()
        If par.IfEmpty(Me.cmbvoceRiepilogo.SelectedValue, "-1") <> "-1" Then

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID,ID_APPALTO,TIPO_PAGAMENTO,PRENOTAZIONI.PERC_IVA,PF_VOCI_IMPORTO.DESCRIZIONE,TO_CHAR(TO_DATE(DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"", " _
                        & "TRIM(TO_CHAR(NVL(IMPORTO_PRENOTATO,0),'9G999G999G999G990D99')) AS IMPORTO_PRENOTATO," _
                        & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'9G999G999G999G990D99')) AS IMPORTO_APPROVATO," _
                        & "TRIM(TO_CHAR(NVL(IMPORTO_LIQUIDATO,0),'9G999G999G999G990D99')) AS IMPORTO_LIQUIDATO " _
                        & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & Request.QueryString("IdAppalto") & ")" _
                        & " and PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO /*and (ID_VOCE_PF_IMPORTO = " & Me.cmbvoceRiepilogo.SelectedValue & " or ID_VOCE_PF_IMPORTO = (select id from siscom_mi.pf_voci_importo pf2 where pf2.id_old = " & Me.cmbvoceRiepilogo.SelectedValue & "))*/ " _
                        & " and id_voce_pf_importo in (select b.id from siscom_mi.pf_voci_importo b connect by prior b.id=b.id_old start with b.id=" & Me.cmbvoceRiepilogo.SelectedValue & ") " _
                        & " AND ID_STATO <> -3 and tipo_pagamento = 6 AND IMPORTO_PRENOTATO>0 ORDER BY PRENOTAZIONI.DATA_SCADENZA ASC "
            '
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()

            da.Fill(dt)
            dgvScadenze.DataSource = dt

            dgvScadenze.DataBind()
            Me.cmbvoce.SelectedValue = Me.cmbvoceRiepilogo.SelectedValue
            Me.cmbvoce.Enabled = False

            Dim totale As Decimal = 0
            For Each row As Data.DataRow In dt.Rows
                totale = totale + par.IfNull(row.Item("IMPORTO_PRENOTATO"), 0)
            Next
            Me.txtTotale.Text = Format(totale, "##,##0.00")

            par.cmd.CommandText = "SELECT IMPORTO_CANONE,SCONTO_CANONE, ONERI_SICUREZZA_CANONE, IVA_CANONE " _
                                & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE  ID_APPALTO = " & Request.QueryString("IdAppalto") _
                                & " AND ID_PF_VOCE_IMPORTO = " & Me.cmbvoceRiepilogo.SelectedValue
            Dim lettMaxImp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim appoggio As Decimal = 0
            If lettMaxImp.Read Then
                appoggio = CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) - Math.Round(((CDec(par.IfNull(lettMaxImp("IMPORTO_CANONE"), 0)) * par.IfNull(lettMaxImp("SCONTO_CANONE"), 0)) / 100), 4)
                appoggio = appoggio + par.IfNull(lettMaxImp("ONERI_SICUREZZA_CANONE"), 0)

                appoggio = appoggio + ((appoggio * par.IfNull(lettMaxImp("IVA_CANONE"), 0)) / 100)
                Me.ImpMassimo.Value = appoggio
            End If

            lettMaxImp.Close()

            par.cmd.CommandText = "select sum(nvl(importo,0)) as variazione from siscom_mi.appalti_variazioni_importi,siscom_mi.appalti_variazioni where id_appalto = " & Request.QueryString("IdAppalto") & " and appalti_variazioni.id = id_variazione and appalti_variazioni.id_tipologia = 5"
            lettMaxImp = par.cmd.ExecuteReader
            If lettMaxImp.Read Then
                Me.ImpMassimo.Value += Math.Round(CDec(par.IfNull(lettMaxImp("variazione"), 0)), 2)
            End If
            lettMaxImp.Close()

            If Me.IdStato.Value = 1 Then
                For Each di As GridDataItem In dgvScadenze.Items
                    DirectCast(di.FindControl("txtImporto"), TextBox).ReadOnly = True
                Next
            Else
                dgvScadenze.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                dgvScadenze.Rebind()
            End If
            AddJavascriptFunction()
        Else
            Me.btnSalva.Visible = False

            dgvScadenze.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            dgvScadenze.Rebind()
            Me.btn_InserisciAppalti.Visible = False
        End If

    End Sub
    Private Sub AddJavascriptFunction()
        Try

            Dim i As Integer = 0
            Dim di As GridDataItem

            For i = 0 To Me.dgvScadenze.Items.Count - 1
                di = Me.dgvScadenze.Items(i)
                DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);SostPuntVirg();")
            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            msgVisto = False
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            '###### controllo dell'importo massimo prenotabile per la voce della combo selezionata
            Dim Totale As Decimal = 0
            For Each di As GridDataItem In Me.dgvScadenze.Items
                If DirectCast(di.FindControl("txtImporto"), TextBox).Text <> 0 Then
                    Totale = Totale + DirectCast(di.FindControl("txtImporto"), TextBox).Text.Replace(".", "")
                End If
            Next
            Me.txtTotale.Text = Format(Totale, "##,##0.00")
            'If Math.Round(Totale, 2) > Math.Round(ImportMassimo(Me.cmbvoce.SelectedValue), 2) Then
            '    Response.Write("<script>alert('La somma degli importi supera il massimo prenotabile per la voce selezionata!Impossibile Salvare!');</script>")
            '    'Session.Item("PrenAttAuto") = 0
            '    'Exit Sub
            'End If

            For Each di As GridDataItem In Me.dgvScadenze.Items
                If DirectCast(di.FindControl("txtImporto"), TextBox).Text <> 0 Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET " _
                        & " IMPORTO_PRENOTATO = " & par.VirgoleInPunti(DirectCast(di.FindControl("txtImporto"), TextBox).Text.Replace(".", "")) & " " _
                        & " WHERE ID = " & di.Cells(par.IndRDGC(dgvScadenze, "ID")).Text & " AND ID_PAGAMENTO IS NULL"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            '###### FINE controllo dell'importo massimo prenotabile per la voce della combo selezionata


            If ControllaImporti() = True Then

                '############## PRENOTAZIONE DEL 10% SULLA VOCE DI SERVIZIO CUSTODI
                Dim idEsercizio = 0
                Dim idVoceDieci As String = ""

                ''########## SELEZIONE DELL'ESERCIZIO FINANZIARIO LEGATO AL LOTTO ASSOCIATO ALL'APPALTO
                par.cmd.CommandText = "SELECT ID AS id_esercizio FROM siscom_mi.pf_main " _
                                    & "WHERE id_esercizio_finanziario = " _
                                    & "(SELECT id_esercizio_finanziario FROM siscom_mi.lotti,siscom_mi.appalti " _
                                    & "WHERE lotti.ID = appalti.id_lotto AND appalti.ID = " & Request.QueryString("IdAppalto") & ")"
                Dim myLettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myLettore.Read Then
                    idEsercizio = par.IfNull(myLettore("ID_ESERCIZIO"), 0)
                End If
                myLettore.Close()
                ''########## SELEZIONE DELLE PRENOTAZIONI PER LA VOCE PORTIERATO 90%
                'PEPPE MODIFY 11/04/2012
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO IS NULL AND importo_prenotato <> 0 AND ID_VOCE_PF = " _
                                    & "(SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CODICE = '2.02.01' AND ID_PIANO_FINANZIARIO = " & idEsercizio & " AND ID_VOCE_PF_IMPORTO = " _
                                    & "(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_VOCE = " _
                                    & "(SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CODICE = '2.02.01' AND ID_PIANO_FINANZIARIO = " & idEsercizio & ") AND ID_SERVIZIO = 2 and id_lotto = (select id_lotto from siscom_mi.appalti where id = " & Request.QueryString("IdAppalto") & "))) AND ID_APPALTO = " & Request.QueryString("IdAppalto")
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                Dim Ev As Boolean = True
                da.Fill(dt)
                '####### SE PRESENTI ALLORA TROVO LA VOCE PER LE PRENOTAZIONI DEL 10% SUL PAGAMENTO DEI CUSTODI
                If dt.Rows.Count > 0 Then
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PF_VOCI WHERE CODICE = '2.03.01' AND ID_PIANO_FINANZIARIO = " & idEsercizio
                    myLettore = par.cmd.ExecuteReader
                    If myLettore.Read Then
                        idVoceDieci = par.IfNull(myLettore(0), "")
                    End If
                    myLettore.Close()
                    '######### SE LA VOCE DEL 10% è presente allora posso effettuare anche le prenotazioni del 10% altrimenti non le faccio
                    If idVoceDieci <> "" Then
                        PrenotazCustodi(dt, idVoceDieci, Ev)
                    End If
                    If Ev = False Then
                        RadWindowManager1.RadAlert("Si sono verificati degli errori!Impossibile procedere!", 300, 150, "Attenzione", "", "null")
                        'Response.Write("<script>alert('Si sono verificati degli errori!Impossibile procedere!');</script>")
                        Session.Item("PrenAttAuto") = 0
                        Exit Sub
                    End If

                End If


                RadWindowManager1.RadAlert("Aggiornamento delle prenotazioni eseguito correttamente!", 300, 150, "Attenzione", "", "null")
                'Response.Write("<script>alert('Aggiornamento delle prenotazioni eseguito correttamente!');</script>")
                Session.Item("PrenAttAuto") = 1
                Modificato.Value = 0
            Else

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
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
            Me.btnSalva.Visible = False
        End Try

    End Sub

    Private Function ControllaImporti() As Boolean
        ControllaImporti = True

        Try

            Dim totModificato As Decimal = 0

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            For Each Items As RadComboBoxItem In cmbvoceRiepilogo.Items
                'PEPPE MODIFY 11/04/2012
                par.cmd.CommandText = "SELECT PRENOTAZIONI.ID,ID_APPALTO,PF_VOCI_IMPORTO.DESCRIZIONE,TO_CHAR(TO_DATE(DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"", " _
                & "(CASE WHEN PRENOTAZIONI.ID_STATO <= 0 THEN TRIM(TO_CHAR(NVL(IMPORTO_PRENOTATO,0),'9G999G999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'9G999G999G999G990D99'))END )AS ""IMPORTO_PRENOTATO""" _
                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo =" & Request.QueryString("IdAppalto") & ")" _
                & " and tipo_pagamento = 6 AND importo_prenotato > 0 AND PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO and id_stato >=0 and ID_VOCE_PF_IMPORTO in  ( SELECT b.id FROM siscom_mi.pf_voci_importo b CONNECT BY PRIOR b.id = b. id_old  START WITH b.  id = " & Items.Value & ") "

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                totModificato = 0
                For Each i As Data.DataRow In dt.Rows
                    If par.IfNull(i.Item("IMPORTO_PRENOTATO"), 0) <> 0 Then
                        totModificato = totModificato + par.IfNull(i.Item("IMPORTO_PRENOTATO"), 0)
                    Else
                        RadWindowManager1.RadAlert("Avvalorare tutti gli importi!", 300, 150, "Attenzione", "", "null")
                        'Response.Write("<script>alert('Avvalorare tutti gli importi!');</script>")
                        ControllaImporti = False
                        Session.Item("PrenAttAuto") = 0
                        Exit Function
                    End If
                Next
                If Math.Round(totModificato, 2) > Math.Round(ImportMassimo(Items.Value), 2) Then
                    If msgVisto = False Then
                        msgVisto = True
                        RadWindowManager1.RadAlert("La somma degli importi supera il massimo prenotabile per questo contratto!", 300, 150, "Attenzione", "", "null")
                        'Response.Write("<script>alert('La somma degli importi supera il massimo prenotabile per questo contratto!');</script>")
                        Me.txtTotale.Text = Format(totModificato, "##,##0.00")
                        'ControllaImporti = False
                        'Session.Item("PrenAttAuto") = 0
                        'Exit Function
                        ControllaImporti = True
                    End If

                ElseIf totModificato < Me.ImpMassimo.Value Then
                    'Response.Write("<script>alert('ATTENZIONE!La somma degli importi è minore del Totale a Canone dell\'appalto!');</script>")
                    'Me.txtTotale.Text = Format(totModificato, "##,##0.00")
                    ControllaImporti = True
                End If
            Next

            'Me.txtTotale.Text = Format(totModificato, "##,##0.00")
            'LoadTable()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            ControllaImporti = False
        End Try
        Return ControllaImporti

    End Function


    Private Function ControlTotale() As Boolean
        ControlTotale = True
        Dim TotDatagrid As Decimal = 0
        Try
            'For Each di As GRIDDATAITEM In dgvScadenze.Items
            '    TotDatagrid = CDec(DirectCast(di.FindControl("txtImporto"), TextBox).Text.Replace(".", ""))
            'Next
            If (CDec(Me.txtTotale.Text.Replace(".", "")) + CDec(par.IfEmpty(Me.txtImporto.Text.Replace(".", ""), 0))) > CDec(ImpMassimo.Value.Replace(".", "")) Then
                'ControlTotale = False
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        Return ControlTotale
    End Function

    Protected Sub btn_InserisciAppalti_Click(sender As Object, e As System.EventArgs) Handles btn_InserisciAppalti.Click
        Try
            If ControlTotale() = True Then
                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                Dim DataMin As String = ""
                Dim DataMax As String = ""

                par.cmd.CommandText = "SELECT DATA_INIZIO,DATA_FINE FROM SISCOM_MI.APPALTI WHERE ID = " & Request.QueryString("IdAppalto")
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    DataMin = par.IfNull(lettore("DATA_INIZIO"), "00000000")
                    DataMax = par.IfNull(lettore("DATA_FINE"), "00000000")
                End If
                lettore.Close()
                If par.AggiustaData(Me.txtScadenza.Text) < DataMin Or par.AggiustaData(Me.txtScadenza.Text) > DataMax Then
                    'Response.Write("<script>alert('La data deve essere compresa nel periodo di durata dell\'Appalto!');</script>")
                    RadWindowManager1.RadAlert("La data deve essere compresa nel periodo di durata dell\'Appalto!", 300, 150, "Attenzione", "", "null")
                    Me.txtScadenza.Text = ""
                    Me.txtImporto.Text = ""
                    Exit Sub
                End If
                par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO AS PF_VOCI_IMPORTO_ID,PF_VOCI_IMPORTO.ID_VOCE AS PF_VOCI_IMPORTO_IDVOCE," _
                                    & "SISCOM_MI.APPALTI.ID_LOTTO,APPALTI.ID_FORNITORE, APPALTI.FL_RIT_LEGGE, " _
                                    & "APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN, APPALTI_LOTTI_SERVIZI.IVA_CANONE " _
                                    & " FROM SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO  WHERE ID_APPALTO = " & Request.QueryString("IdAppalto") & " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO = " & Me.cmbvoce.SelectedValue & " AND PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO AND APPALTI.ID = APPALTI_LOTTI_SERVIZI.ID_APPALTO "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)
                Dim RitIvate As String = "Null"
                If dt.Rows(0).Item("FL_RIT_LEGGE") = 1 Then
                    RitIvate = (CDec(Me.txtImporto.Text.Replace(".", "")) * 0.5) / 100
                    'RitIvate = RitIvate + ((RitIvate * dt.Rows(0).Item("IVA_CANONE")) / 100)
                End If

                'aumento l'iva al 22
                Dim iva As String = par.IfEmpty(dt.Rows(0).Item("IVA_CANONE").ToString, "0")
                If par.AggiustaData(Me.txtScadenza.Text) >= "20131001" Then
                    If iva = "21" Or iva = "20" Then
                        iva = "22"
                    End If
                End If
                par.cmd.CommandText = "select substr(inizio,1,4) from SISCOM_MI.T_ESERCIZIO_FINANZIARIO where id = (select id_esercizio_finanziario from SISCOM_MI.pf_main where pf_main.id_stato<5 and id = (SELECT id_piano_finanziario FROM SISCOM_MI.PF_VOCI WHERE ID = (SELECT id_voce FROM SISCOM_MI.PF_VOCI_IMPORTO  WHERE ID = " & Me.cmbvoce.SelectedValue & ")))"
                Dim EsVoce As String = par.IfNull(par.cmd.ExecuteScalar, "0")
                Dim stato As String = 0
                If EsVoce <> par.AggiustaData(Me.txtScadenza.Text).Substring(0, 4) Then
                    stato = "-1"
                End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(par.IfEmpty(Me.txtImporto.Text.Replace(".", ""), 0)) & " ," & par.IfNull(dt.Rows(0).Item("ID_FORNITORE"), "Null") & " ," & Request.QueryString("IdAppalto") & "," _
                    & dt.Rows(0).Item("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO'," & par.AggiustaData(Me.txtScadenza.Text) & "," _
                    & dt.Rows(0).Item("PF_VOCI_IMPORTO_ID") & "," & Session.Item("ID_STRUTTURA") & "," & par.VirgoleInPunti(RitIvate) & "," & iva & ")"
                Try
                    par.cmd.ExecuteNonQuery()
                Catch ex As Oracle.DataAccess.Client.OracleException
                    If ex.Number = 1403 Then
                        RadWindowManager1.RadAlert("Non è possibile emettere la prenotazione!\nL\'esercizio finanziario di riferimento è chiuso!", 300, 150, "Attenzione", "", "null")
                        'Response.Write("<script>alert('Non è possibile emettere la prenotazione!\nL\'esercizio finanziario di riferimento è chiuso!');</script>")
                        Exit Sub
                    Else
                        RadWindowManager1.RadAlert("Errore nell\'inserimento della prenotazione!", 300, 150, "Attenzione", "", "null")
                        'Response.Write("<script>alert('Errore nell\'inserimento della prenotazione!');</script>")
                        Exit Sub
                    End If
                End Try
                RadWindowManager1.RadAlert("Inserimento eseguito correttamente!", 300, 150, "Attenzione", "", "null")
                'Response.Write("<script>alert('Inserimento eseguito correttamente!');</script>")
                LoadTable()
                Me.txtScadenza.Text = ""
                Me.txtImporto.Text = ""
                Modificato.Value = 1
            Else
                RadWindowManager1.RadAlert("La somma degli importi supera il massimo prenotabile per questo contratto!", 300, 150, "Attenzione", "", "null")
                'Response.Write("<script>alert('La somma degli importi supera il massimo prenotabile per questo contratto!');</script>")
                Me.txtScadenza.Text = ""
                Me.txtImporto.Text = ""
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub btn_ChiudiAppalti_Click(sender As Object, e As System.EventArgs) Handles btn_ChiudiAppalti.Click
        Me.txtImporto.Text = ""
        Me.txtScadenza.Text = ""
    End Sub

    Protected Sub dgvScadenze_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvScadenze.ItemCommand
        Try
            If e.CommandName = "Delete" Then
                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                If idPrenotazione.Value = "0" Then
                    Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                    idPrenotazione.Value = par.IfNull(dataItem("ID").Text, "0")
                End If

                par.cmd.CommandText = "SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & idPrenotazione.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("ID_PAGAMENTO"), 0) > 0 Then
                        RadWindowManager1.RadAlert("Impossibile eliminare la prenotazione, è stato già emesso il pagamento!", 300, 150, "Attenzione", "", "null")
                        idPrenotazione.Value = "0"
                        'Response.Write("<script>alert('Impossibile eliminare la prenotazione, è stato già emesso il pagamento!');</script>")
                        Exit Sub

                    End If
                End If
                lettore.Close()

                par.cmd.CommandText = "update siscom_mi.prenotazioni set  id_stato = -3 WHERE  ID = " & idPrenotazione.Value
                'par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & idPrenotazione.Value
                par.cmd.ExecuteNonQuery()

                'Response.Write("<script>alert('Eliminazione eseguita correttamente!');</script>")
                idPrenotazione.Value = 0
                Modificato.Value = 1
                LoadTable()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

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
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato la prenotazione con scadenza: " & e.Item.Cells(par.IndRDGC(dgvScadenze, "SCADENZA")).Text & "';document.getElementById('idPrenotazione').value=" & e.Item.Cells(par.IndRDGC(dgvScadenze, "ID")).Text & ";")

        End If
    End Sub

    Protected Sub cmbvoceRiepilogo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbvoceRiepilogo.SelectedIndexChanged
        LoadTable()
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

        If Me.IdStato.Value = 1 Then
            For Each di As GridDataItem In dgvScadenze.Items
                DirectCast(di.FindControl("txtImporto"), TextBox).ReadOnly = True
            Next
        Else
            dgvScadenze.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            dgvScadenze.Rebind()
        End If
        AddJavascriptFunction()
    End Sub

    Protected Sub dgvScadenze_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvScadenze.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID,ID_APPALTO,TIPO_PAGAMENTO,PRENOTAZIONI.PERC_IVA,PF_VOCI_IMPORTO.DESCRIZIONE,TO_CHAR(TO_DATE(DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"", " _
                       & "NVL(IMPORTO_PRENOTATO,0) AS IMPORTO_PRENOTATO," _
                       & "NVL(IMPORTO_APPROVATO,0) AS IMPORTO_APPROVATO," _
                       & "NVL(IMPORTO_LIQUIDATO,0) AS IMPORTO_LIQUIDATO " _
                       & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & Request.QueryString("IdAppalto") & ")" _
                       & " AND importo_prenotato > 0  " _
                       & " and PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO /*and (ID_VOCE_PF_IMPORTO = " & par.IfEmpty(Me.cmbvoceRiepilogo.SelectedValue, "-1") & " or ID_VOCE_PF_IMPORTO = (select id from siscom_mi.pf_voci_importo pf2 where pf2.id_old = " & par.IfEmpty(Me.cmbvoceRiepilogo.SelectedValue, "-1") & "))*/ " _
                       & " and id_voce_pf_importo in (select b.id from siscom_mi.pf_voci_importo b connect by prior b.id=b.id_old start with b.id=" & par.IfEmpty(Me.cmbvoceRiepilogo.SelectedValue, "-1") & ") " _
                       & " AND ID_STATO <> -3 and tipo_pagamento = 6 ORDER BY PRENOTAZIONI.DATA_SCADENZA ASC "

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
