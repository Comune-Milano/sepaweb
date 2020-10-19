Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_ImputazionePulizie
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

                Try
                    btnSchedaImputazione.Visible = False
                Catch ex As Exception
                End Try

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
                stringaImporti &= ",(SELECT A.ID FROM SISCOM_MI.PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") AND A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " AND A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3) AS ID" & indice _
                    & " ,nvl((SELECT SUM(round(A.IMPORTO_PRENOTATO,2)) FROM PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") AND A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " AND A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3),0) AS IMPORTO_PRENOTATO" & indice _
                    & " ,nvl((SELECT SUM(round(A.IMPORTO_APPROVATO,2)) FROM PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") AND A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " AND A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3),0) AS IMPORTO_APPROVATO" & indice _
                    & " ,nvl((SELECT SUM(round(A.IMPORTO_LIQUIDATO,2)) FROM PRENOTAZIONI A " _
                    & " WHERE A.ID_VOCE_PF_IMPORTO IN (" & idPfVoceImporto & ") AND A.DATA_sCADENZA=PRENOTAZIONI.DATA_SCADENZA " _
                    & " AND A.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND A.ID_STATO <> -3),0) AS IMPORTO_LIQUIDATO" & indice

            End While

            'For i As Integer = 1 To 5 - indice
            '    stringaImporti &= ",0 AS ID" & indice + i _
            '        & " ,0 AS IMPORTO_PRENOTATO" & indice + i _
            '        & " ,0 AS IMPORTO_APPROVATO" & indice + i _
            '        & " ,0 AS IMPORTO_LIQUIDATO" & indice + i
            'Next

            lettore.Close()
            par.cmd.CommandText = "select rownum,a.* from (Select distinct /*PRENOTAZIONI.ID,*/ID_APPALTO,TIPO_PAGAMENTO,/*PRENOTAZIONI.PERC_IVA,PF_VOCI_IMPORTO.DESCRIZIONE,*/TO_DATE(DATA_SCADENZA,'YYYYmmdd') AS SCADENZA,prenotazioni.data_scadenza " _
                & stringaImporti _
                        & " FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO WHERE ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo = " & Request.QueryString("IdAppalto") & ")" _
                & " and PF_VOCI_IMPORTO.ID = PRENOTAZIONI.ID_VOCE_PF_IMPORTO  " _
                & " and id_voce_pf_importo in (select b.id from siscom_mi.pf_voci_importo b connect by prior b.id=b.id_old start with b.id in (" _
                & "SELECT SISCOM_MI.PF_VOCI_IMPORTO.ID " _
                & "FROM SISCOM_MI.PF_VOCI_IMPORTO,siscom_mi.pf_voci, SISCOM_MI.APPALTI_LOTTI_SERVIZI" _
                & " WHERE  pf_voci.ID = pf_voci_importo.id_voce " _
                & "AND APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE > 0 AND PF_VOCI_IMPORTO.ID = ID_PF_VOCE_IMPORTO AND ID_APPALTO = " & Request.QueryString("IdAppalto") _
                & " )) " _
                & " AND ID_STATO <> -3 and tipo_pagamento = 6 AND IMPORTO_PRENOTATO>0 ORDER BY prenotazioni.data_scadenza ASC) a"
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

        Else

            dgvScadenze.Rebind()
            Me.btn_InserisciAppalti.Visible = False
        End If

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
            Select Case e.CommandName
                Case "Delete"
                    ' RIPRENDO LA CONNESSIONE
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    'RIPRENDO LA TRANSAZIONE
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    If idPrenotazione.Value = "0" Then
                        Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                        idPrenotazione.Value = par.IfNull(dataItem("ID1").Text, "0")
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
                    'Case "SchedaImputazione"
                    '    If idPrenotazione.Value = "0" Then
                    '        Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                    '        idPrenotazione.Value = par.IfNull(dataItem("ID1").Text, "0")
                    '    End If
                    '    idPrenotazione.Value = 0
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "VisSchedaImputazione();", True)
            End Select
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
            e.Item.Attributes.Add("onclick", "document.getElementById('idPrenotazione').value=" & e.Item.Cells(par.IndRDGC(dgvScadenze, "ID1")).Text & ";")

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


        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub



    Private Sub btnAggiorna_Click(sender As Object, e As EventArgs) Handles btnAggiorna.Click
        '05 SERVIZI PARTI COMUNI
        ' RIPRENDO LA CONNESSIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        'RIPRENDO LA TRANSAZIONE
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
        Dim idpiano As Integer = par.cmd.ExecuteScalar
        Dim dtTotale As Data.DataTable = Session.Item("DT_PRENOTAZIONI")
        par.cmd.CommandText = "SELECT ID, (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, " _
                    & " (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO,  " _
                    & " (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE, " _
                    & " COD_EDIFICIO, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, " _
                    & " REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni, " _
                    & " REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, " _
                    & " NUMERO_BIDONI_CARTA," _
                    & " NUMERO_BIDONI_VETRO," _
                    & " NUMERO_BIDONI_UMIDO," _
                    & " (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.edifici_tmp.ID_COMPLESSO)) AS SEDE_TERRITORIALE," _
                    & " FL_SPAZI_ESTERNI * " _
                    & " NVL (MQ_ESTERNI, 0) * " _
                    & " (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) " _
                    & " +  FL_SPAZI_ESTERNI * " _
                    & " NVL (MQ_PILOTY, 0) * " _
                    & " (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS IMPORTO_EDIFICIO,  " _
                    & " NVL ( (SELECT SUM ( SCALE_edifici_tmp.PULIZIA_SCALE * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     PREZZO = '1' AND TIPOLOGIA = (SCALE_edifici_tmp.N_PIANI) AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                    & " FROM SISCOM_MI.SCALE_edifici_tmp WHERE SCALE_edifici_tmp.ID_eDIFICIO = edifici_tmp.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), 0) AS IMPORTO_SCALA," _
                    & " (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO," _
                    & " (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, " _
                    & " FL_SPAZI_ESTERNI * NVL (MQ_ESTERNI, 0) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   FL_SPAZI_ESTERNI * NVL (MQ_PILOTY, 0) * (SELECT IMPORTO " _
                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) + NVL ( (SELECT SUM ( SCALE_edifici_tmp.PULIZIA_SCALE * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     PREZZO = '1' " _
                    & " AND TIPOLOGIA = (SCALE_edifici_tmp.N_PIANI) AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_edifici_tmp WHERE SCALE_edifici_tmp.ID_eDIFICIO = edifici_tmp.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), 0) AS TOTALE_EDIFICIO, " _
                    & " (    FL_SPAZI_ESTERNI * NVL (MQ_ESTERNI, 0) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   FL_SPAZI_ESTERNI * NVL (MQ_PILOTY, 0) * (SELECT IMPORTO " _
                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) + NVL ( (SELECT SUM ( SCALE_edifici_tmp.PULIZIA_SCALE * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     PREZZO = '1' " _
                    & " AND TIPOLOGIA = (SCALE_edifici_tmp.N_PIANI) AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_edifici_tmp WHERE SCALE_edifici_tmp.ID_eDIFICIO = edifici_tmp.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), 0)) * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100)  * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100) AS TOT_ANNUO_SCONTATO," _
                    & " '' AS TOTALE_LORDO FROM SISCOM_MI.edifici_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    & " WHERE     edifici_tmp.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                    & " AND ID <> 1 AND  ID_APPALTO = " & HiddenIdContratto.Value & " AND edifici_tmp.FL_IN_CONDOMINIO = 0 " _
                    & " and edifici_tmp.id_prenotazione = " & idPrenotazione.Value _
                    & " ORDER BY edifici_tmp.DENOMINAZIONE ASC "

        Dim dt05 As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Dim totale05 As Decimal = 0
        Dim rigaTotale As Data.DataRow() = dtTotale.Select("ID1 = " & idPrenotazione.Value)
        If dt05.Rows.Count > 0 Then

            For Each riga As Data.DataRow In dt05.Rows
                totale05 += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
            Next
            totale05 = Math.Round(totale05, 2)

            Dim idStato As Integer = 0
            Dim totaleApprovato As Decimal = 0
            par.cmd.CommandText = "SELECT ID_STATO, IMPORTO_APPROVATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & par.IfNull(rigaTotale(0).Item("ID1"), "-1")
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                idStato = MyReader("ID_STATO")
                totaleApprovato = MyReader("IMPORTO_APPROVATO")
            End If
            MyReader.Close()
            If idStato >= 1 Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO WHERE ID_PRENOTAZIONE = " & par.IfNull(rigaTotale(0).Item("ID1"), "-1")
                par.cmd.ExecuteNonQuery()
                For Each riga As Data.DataRow In dt05.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO ( " _
                                & "    ID_EDIFICIO, ID_PRENOTAZIONE, IMPORTO)  " _
                                & " VALUES ( " _
                                & "(SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = " & riga.Item("COD_EDIFICIO") & ") /* ID_EDIFICIO */, " _
                                & rigaTotale(0).Item("ID1") & "  /* ID_PRENOTAZIONE */, " _
                                & par.VirgoleInPunti(Math.Round(par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0) / totale05 * totaleApprovato, 4)) & "  /* IMPORTO */ ) "
                    par.cmd.ExecuteNonQuery()
                Next
            End If
        End If



        '05 
        par.cmd.CommandText = "update siscom_mi.prenotazioni " _
            & " set importo_prenotato = " & par.VirgoleInPunti(totale05) _
            & " where id = " & par.IfNull(rigaTotale(0).Item("ID1"), "-1") & " and id_Stato<=0"
        par.cmd.ExecuteNonQuery()





        par.cmd.CommandText = "SELECT ID, (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI " _
            & " WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO," _
            & " (SELECT ID_FORNITORE FROM SISCOM_MI.appalti WHERE appalti.id=appalti_lotti_patrimonio.id_appalto) AS ID_FORNITORE, " _
            & " COD_EDIFICIO, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni, REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, " _
                                    & " NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI  " _
                                    & " WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE, NVL ( (SELECT   SUM (  SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.RESA_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), 0) AS IMPORTO_SCALA, 0 AS IMPORTO_EDIFICIO, NVL ( (SELECT   SUM (  SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI " _
                                    & " * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.RESA_SACCHI * SCALE_EDIFICI_TMP.N_UNITA " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), 0) AS TOTALE_EDIFICIO, " _
                                    & " (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, NVL ( (SELECT   SUM (  SCALE_EDIFICI_TMP.ROTAZIONE_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '7' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.RESA_SACCHI * SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '8' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " FROM SISCOM_MI.SCALE_EDIFICI_TMP  WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), 0) * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0)/ 100) * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto),0) / 100) AS TOT_ANNUO_SCONTATO, '' AS TOTALE_LORDO FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                    & " WHERE     EDIFICI_TMP.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO AND ID <> 1 AND ID_APPALTO = " & HiddenIdContratto.Value & " AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & idPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "


        Dim dt06 As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Dim totale06 As Decimal = 0
        If dt06.Rows.Count > 0 Then

            For Each riga As Data.DataRow In dt06.Rows
                totale06 += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
            Next
            totale06 = Math.Round(totale06, 2)

            Dim idStato As Integer = 0
            Dim totaleApprovato As Decimal = 0
            par.cmd.CommandText = "SELECT ID_STATO, IMPORTO_APPROVATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & par.IfNull(rigaTotale(0).Item("ID2"), "-1")
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                idStato = MyReader("ID_STATO")
                totaleApprovato = MyReader("IMPORTO_APPROVATO")
            End If
            MyReader.Close()
            If idStato >= 1 Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO WHERE ID_PRENOTAZIONE = " & par.IfNull(rigaTotale(0).Item("ID2"), "-1")
                par.cmd.ExecuteNonQuery()
                For Each riga As Data.DataRow In dt06.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO ( " _
                                & "    ID_EDIFICIO, ID_PRENOTAZIONE, IMPORTO)  " _
                                & " VALUES ( " _
                                & "(SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = " & riga.Item("COD_EDIFICIO") & ") /* ID_EDIFICIO */, " _
                                & rigaTotale(0).Item("ID2") & "  /* ID_PRENOTAZIONE */, " _
                                & par.VirgoleInPunti(Math.Round(par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0) / totale06 * totaleApprovato, 4)) & "  /* IMPORTO */ ) "
                    par.cmd.ExecuteNonQuery()
                Next
            End If
        End If

        par.cmd.CommandText = "update siscom_mi.prenotazioni " _
            & " set importo_prenotato = " & par.VirgoleInPunti(totale06) _
            & " where id = " & par.IfNull(rigaTotale(0).Item("ID2"), "-1") & " and id_Stato<=0"
        par.cmd.ExecuteNonQuery()



        par.cmd.CommandText = "SELECT ID, (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI " _
            & " WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO, " _
            & " (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE, " _
            & " COD_EDIFICIO, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni,  " _
                                    & " REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT ID_FILIALE " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE, (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   2 * (NVL (NUMERO_BIDONI_UMIDO, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '9' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS IMPORTO_EDIFICIO, 0 AS IMPORTO_SCALA, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) " _
                                    & " AS IVA, (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   2 " _
                                    & " * (NVL (NUMERO_BIDONI_UMIDO, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS TOTALE_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, (    (NVL (NUMERO_BIDONI_VETRO, 0) + NVL (NUMERO_BIDONI_CARTA, 0)) * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '9' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) +   2 * (NVL (NUMERO_BIDONI_UMIDO, 0)) * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '9' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), " _
                                    & " 0) / 100) * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100) " _
                                    & " AS TOT_ANNUO_SCONTATO, '' AS TOTALE_LORDO FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  " _
                                    & " WHERE     EDIFICI_TMP.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO aND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & HiddenIdContratto.Value & " AND ID <> 1 AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & idPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "

        Dim dt07 As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Dim totale07 As Decimal = 0
        If dt07.Rows.Count > 0 Then

            For Each riga As Data.DataRow In dt07.Rows
                totale07 += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
            Next
            totale07 = Math.Round(totale07, 2)

            Dim idStato As Integer = 0
            Dim totaleApprovato As Decimal = 0
            par.cmd.CommandText = "SELECT ID_STATO, IMPORTO_APPROVATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & par.IfNull(rigaTotale(0).Item("ID3"), "-1")
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                idStato = MyReader("ID_STATO")
                totaleApprovato = MyReader("IMPORTO_APPROVATO")
            End If
            MyReader.Close()
            If idStato >= 1 Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO WHERE ID_PRENOTAZIONE = " & par.IfNull(rigaTotale(0).Item("ID3"), "-1")
                par.cmd.ExecuteNonQuery()
                For Each riga As Data.DataRow In dt07.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO ( " _
                                & "    ID_EDIFICIO, ID_PRENOTAZIONE, IMPORTO)  " _
                                & " VALUES ( " _
                                & "(SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = " & riga.Item("COD_EDIFICIO") & ") /* ID_EDIFICIO */, " _
                                & rigaTotale(0).Item("ID3") & "  /* ID_PRENOTAZIONE */, " _
                                & par.VirgoleInPunti(Math.Round(par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0) / totale07 * totaleApprovato, 4)) & "  /* IMPORTO */ ) "
                    par.cmd.ExecuteNonQuery()
                Next
            End If
        End If

        par.cmd.CommandText = "update siscom_mi.prenotazioni " _
            & " set importo_prenotato = " & par.VirgoleInPunti(totale07) _
            & " where id = " & par.IfNull(rigaTotale(0).Item("ID3"), "-1") & " and id_Stato<=0"
        par.cmd.ExecuteNonQuery()


        par.cmd.CommandText = "SELECT ID, " _
                                    & " (SELECT NUM_rEPERTORIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS REPERTORIO, (SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI " _
            & " WHERE APPALTI.ID = APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_GRUPPO, " _
            & " (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) AS ID_FORNITORE, " _
            & " COD_EDIFICIO, COD_EDIFICIO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE, REPLACE (MQ_ESTERNI, ',', '.') AS mq_esterni,  " _
                                    & " REPLACE (MQ_PILOTY, ',', '.') AS MQ_PILOTY, NUMERO_BIDONI_CARTA, NUMERO_BIDONI_VETRO, NUMERO_BIDONI_UMIDO, (SELECT DISTINCT NOME FROM SISCOM_MI.TAB_FILIALI " _
                                    & " WHERE ID IN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = SISCOM_MI.EDIFICI_TMP.ID_COMPLESSO)) AS SEDE_TERRITORIALE, NVL ( (SELECT   SUM (  1 " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '12' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ") )) + SUM (  1 " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & "), " _
                                    & " 0) AS IMPORTO_SCALA, 0 AS IMPORTO_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) " _
                                    & " AS SCONTO, (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, " _
                                    & " (SELECT   SUM (  1 * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '12' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  1 * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & ") AS TOTALE_EDIFICIO, (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS SCONTO,  " _
                                    & " (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto) AS IVA, (SELECT   SUM (  1 " _
                                    & " * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '10' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO " _
                                    & " WHERE PREZZO = '12' AND TIPOLOGIA = '1' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '2' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) " _
                                    & " + SUM (  SCALE_EDIFICI_TMP.N_UNITA * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '12' AND TIPOLOGIA = '3' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) + SUM (  1 * (SELECT IMPORTO " _
                                    & " FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE PREZZO = '13' AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))) FROM SISCOM_MI.SCALE_EDIFICI_TMP WHERE SCALE_EDIFICI_TMP.ID_eDIFICIO = EDIFICI_TMP.ID and SCALE_edifici_tmp.id_prenotazione = " & idPrenotazione.Value & ") * (  1 -   NVL ( (select max(sconto_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto), 0) / 100) " _
                                    & " * (  1 +   NVL ( (select max(iva_canone) from siscom_mi.appalti_lotti_Servizi where appalti_lotti_servizi.id_appalto=appalti_lotti_patrimonio.id_appalto),0) / 100) AS TOT_ANNUO_SCONTATO, " _
                                    & " '' AS TOTALE_LORDO FROM SISCOM_MI.EDIFICI_tmp, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE     EDIFICI_TMP.ID = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO AND APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =  " & HiddenIdContratto.Value _
                                    & " AND ID <> 1 AND EDIFICI_TMP.FL_IN_CONDOMINIO = 0 " _
                                    & " and edifici_tmp.id_prenotazione = " & idPrenotazione.Value _
                                    & " ORDER BY EDIFICI_TMP.DENOMINAZIONE ASC "

        Dim dt15 As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Dim totale15 As Decimal = 0
        If dt15.Rows.Count > 0 Then

            For Each riga As Data.DataRow In dt15.Rows
                totale15 += par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0)
            Next
            totale15 = Math.Round(totale15, 2)

            Dim idStato As Integer = 0
            Dim totaleApprovato As Decimal = 0
            par.cmd.CommandText = "SELECT ID_STATO, IMPORTO_APPROVATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & par.IfNull(rigaTotale(0).Item("ID4"), "-1")
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                idStato = MyReader("ID_STATO")
                totaleApprovato = MyReader("IMPORTO_APPROVATO")
            End If
            MyReader.Close()
            If idStato >= 1 Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO WHERE ID_PRENOTAZIONE = " & par.IfNull(rigaTotale(0).Item("ID4"), "-1")
                par.cmd.ExecuteNonQuery()
                For Each riga As Data.DataRow In dt15.Rows
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.IMPUTAZIONI_PULIZIE_COMPLESSO ( " _
                                & "    ID_EDIFICIO, ID_PRENOTAZIONE, IMPORTO )  " _
                                & " VALUES ( " _
                                & "(SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = " & riga.Item("COD_EDIFICIO") & ") /* ID_EDIFICIO */, " _
                                & rigaTotale(0).Item("ID4") & "  /* ID_PRENOTAZIONE */, " _
                                & par.VirgoleInPunti(Math.Round(par.IfNull(riga.Item("TOT_ANNUO_SCONTATO"), 0) / totale15 * totaleApprovato, 4)) & "  /* IMPORTO */ ) "
                    par.cmd.ExecuteNonQuery()
                Next
            End If
        End If

        par.cmd.CommandText = "update siscom_mi.prenotazioni " _
            & " set importo_prenotato = " & par.VirgoleInPunti(totale15) _
            & " where id = " & par.IfNull(rigaTotale(0).Item("ID4"), "-1") & " and id_Stato<=0"
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

    Private Sub btnSchedaImputazione_Click(sender As Object, e As EventArgs) Handles btnSchedaImputazione.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "VisSchedaImputazione();", True)
    End Sub
End Class
