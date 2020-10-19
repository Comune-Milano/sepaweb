
Partial Class Contratti_Pagamenti_AnnulloPagamento
    Inherits PageSetIdMode

    Dim par As New CM.Global
    Public Property ImpForRateiz() As Decimal
        Get
            If Not (ViewState("par_ImpForRateiz") Is Nothing) Then
                Return CDec(ViewState("par_ImpForRateiz"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_ImpForRateiz") = value
        End Set

    End Property

    Public Property idIncasso() As String
        Get
            If Not (ViewState("idIncasso") Is Nothing) Then
                Return CStr(ViewState("idIncasso"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("idIncasso") = value
        End Set

    End Property

    Public Property IdEvento() As String
        Get
            If Not (ViewState("par_IdEvento") Is Nothing) Then
                Return CStr(ViewState("par_IdEvento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdEvento") = value
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
            CaricDati()
        End If
    End Sub
    Private Function TrovaUltimoEvento() As String
        TrovaUltimoEvento = "0"
        Dim OpenNow As Boolean = False
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
            OpenNow = True
        End If

        par.cmd.CommandText = "select max(id) from siscom_mi.eventi_pagamenti_parziali where fl_annullata = 0 and cod_evento = 'F101' AND ID_CONTRATTO = " & Request.QueryString("IDCONT") _
                            & " and data_ora in (select max(data_ora) from siscom_mi.eventi_pagamenti_parziali where fl_annullata = 0 and cod_evento = 'F101' AND ID_CONTRATTO = " & Request.QueryString("IDCONT") & ") and id_incasso_extramav in (select id from siscom_mi.incassi_extramav where id_contratto = " & Request.QueryString("IDCONT") & " and fl_annullabile = 1)"
        'AND ID_VOCE_BOLLETTA IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA IN (SELECT ID FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_CONTRATTO = " & Request.QueryString("IDCONT") & "))"

        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            TrovaUltimoEvento = par.IfNull(lettore(0), "0")
        End If
        lettore.Close()

        If par.OracleConn.State = Data.ConnectionState.Open And OpenNow = True Then
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If

        Return TrovaUltimoEvento
    End Function

    Private Function TrovaIncassoEvento() As String
        TrovaIncassoEvento = "0"
        Dim OpenNow As Boolean = False
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
            OpenNow = True
        End If

        par.cmd.CommandText = "select id_incasso_extramav from siscom_Mi.eventi_pagamenti_parziali where id = " & IdEvento

        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            TrovaIncassoEvento = par.IfNull(lettore(0), "0")
        End If
        lettore.Close()

        If par.OracleConn.State = Data.ConnectionState.Open And OpenNow = True Then
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If

        Return TrovaIncassoEvento
    End Function

    Private Sub CaricDati()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim totale As Decimal = 0

            IdEvento = "0"
            idIncasso = "0"
            IdEvento = TrovaUltimoEvento()




            par.cmd.CommandText = "select id_main from siscom_mi.eventi_pagamenti_parziali where id = " & IdEvento
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                IdEvento = par.IfNull(lettore(0), IdEvento)
            End If
            lettore.Close()
            'par.cmd.CommandText = "select id_evento_principale from siscom_mi.eventi_pagamenti_parziali where id = " & Evento
            'lettore = par.cmd.ExecuteReader
            'If lettore.Read Then
            '    IdEvento = par.IfNull(lettore(0), "0")
            'End If
            idIncasso = TrovaIncassoEvento()


            par.cmd.CommandText = "SELECT TO_DATE (siscom_mi.eventi_pagamenti_parziali.data_ora,'yyyyMMddHH24MISS') AS DATA_ORA, " _
                                & "siscom_mi.TAB_EVENTI.descrizione, cod_evento, motivazione, " _
                                & "sepa.OPERATORI.operatore, " _
                                & "siscom_mi.eventi_pagamenti_parziali.id_operatore, " _
                                & "sepa.CAF_WEB.cod_caf, " _
                                & "TO_CHAR (incassi_extramav.importo,'9G999G999G990D99') AS importo, " _
                                & "(SELECT SUM(NVL(importo_riclassificato,0)) FROM siscom_Mi.eventi_pagamenti_parz_dett,siscom_mi.bol_bollette_voci " _
                                & "WHERE bol_bollette_voci.ID = id_voce_bolletta AND id_evento_principale = eventi_pagamenti_parziali.ID) AS riclassificato," _
                                & "INCASSI_EXTRAMAV.MOTIVO_PAGAMENTO, tipo_pag_parz.descrizione AS tipo " _
                                & "FROM sepa.CAF_WEB, " _
                                & "siscom_mi.eventi_pagamenti_parziali, " _
                                & "siscom_mi.TAB_EVENTI, " _
                                & "sepa.OPERATORI, " _
                                & "SISCOM_MI.INCASSI_EXTRAMAV, SISCOM_MI.TIPO_PAG_PARZ " _
                                & "WHERE siscom_mi.eventi_pagamenti_parziali.cod_evento = siscom_mi.TAB_EVENTI.cod(+) " _
                                & "AND siscom_mi.eventi_pagamenti_parziali.id_operatore = sepa.OPERATORI.ID(+) " _
                                & "AND sepa.CAF_WEB.ID = sepa.OPERATORI.id_caf " _
                                & "AND incassi_extramav.ID = eventi_pagamenti_parziali.id_incasso_extramav AND siscom_mi.incassi_extramav.id_tipo_pag = siscom_mi.tipo_pag_parz.ID(+) " _
                                & "AND eventi_pagamenti_parziali.ID = " & IdEvento & " " _
                                & "ORDER BY eventi_pagamenti_parziali.data_ora DESC, " _
                                & "eventi_pagamenti_parziali.cod_evento DESC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 And idIncasso <> "0" Then

                If par.IfNull(dt.Rows(0).Item("importo"), 0) > 0 Then
                    'If (dt.Rows(0).Item("riclassificato")) = 0 Then
                    'par.cmd.CommandText = "SELECT importo FROM siscom_mi.crediti WHERE id_evento_pagamento IN (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE id_main = " & IdEvento & " or id = " & IdEvento & ") and crediti.spalmabile = 0"

                    'par.cmd.CommandText = "SELECT importo FROM siscom_mi.crediti WHERE id_evento_pagamento IN (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE id_incasso_extramav = " & idIncasso & ") and crediti.spalmabile = 0"
                    par.cmd.CommandText = "SELECT importo_totale FROM siscom_mi.bol_bollette_gest WHERE id_evento_pagamento IN (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE id_incasso_extramav = " & idIncasso & ") "

                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        dt.Rows(0).Item("importo") = dt.Rows(0).Item("importo")

                    End If
                    lettore.Close()

                    For Each r As Data.DataRow In dt.Rows
                        totale = totale + par.IfNull(r.Item("importo"), 0)
                    Next

                    Me.TotAnnullo.Value = totale
                    Me.lblTotale.Text = "TOTALE DELL'IMPORTO CARICATO NELL'ULTIMA OPERAZIONE PARI A €." & Format(totale, "##,##0.00")
                    DgvUltimaOperazione.DataSource = dt
                    DgvUltimaOperazione.DataBind()


                    dt.Dispose()
                    'Else
                    'Response.Write("<script>alert('IMPOSSIBILE ANNULLARE L\'ULTIMA OPERAZIONE ESEGUITA!\nA seguito " _
                    '            & "dell\'ultima operazione,\nla bolletta interessata è stata riclassificata.');self.close();</script>")

                    'End If

                Else
                    Me.btnProcedi.Visible = False
                    Response.Write("<script>alert('IMPOSSIBILE ANNULLARE L\'ULTIMA OPERAZIONE ESEGUITA!\nA seguito " _
                                   & "dell\'ultima operazione,\nil debito dell\'inquilino in esame è stato riclassificato\n oppure l\'operazione eseguita non è annullabile.');self.close();</script>")

                End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Else
                Me.btnProcedi.Visible = False
                Response.Write("<script>alert('Non ci sono operazioni annullabili!');self.close();</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub


    Private Sub UndoRiclassificate(ByVal idBollettaRic As String)
        Try
            ' AND (IMPORTO_PAGATO IS NULL OR IMPORTO_PAGATO < IMPORTO_TOTALE) 
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT WHERE ID_VOCE_BOLLETTA IN " _
                                & "(SELECT BOL_BOLLETTE_VOCI.ID " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.T_VOCI_BOLLETTA " _
                                & "WHERE FL_ANNULLATA = '0' " _
                                & "AND ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_VOCE = T_VOCI_BOLLETTA.ID " _
                                & "AND IMPORTO > 0 " _
                                & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC = " & idBollettaRic & " " _
                                & " " _
                                & ") AND ID_EVENTO_PRINCIPALE in (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE id_incasso_extramav = " & idIncasso & ")"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim dt As New Data.DataTable
            da.Fill(dt)

            For Each r As Data.DataRow In dt.Rows

                If par.IfNull(r.Item("ID_VOCE_BOLLETTA"), 0) > 0 Then

                    '*********SCALO L'IMPORTO PAGATO DELLA VOCE DI BOLLETTA CHE ERA STATA PAGATA MANUALMENTE

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                        & "IMP_PAGATO = (NVL(IMP_PAGATO,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                        & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA")
                    par.cmd.ExecuteNonQuery()

                    '17/04/2012 undo anche importo_riclassificato_pagato in bol_bollette_voci per le riclassificate
                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                    & "importo_riclassificato_pagato = (NVL(importo_riclassificato_pagato,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                    & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA")
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                    & "imp_pagato_bak = (NVL(imp_pagato_bak,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                    & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and NVL(imp_pagato_bak,0) > 0"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                        & "IMP_PAGATO = null, imp_pagato_bak = null where importo < 0  and id_bolletta in (select id_bolletta from bol_bollette_voci where id = " & r.Item("ID_VOCE_BOLLETTA") & ")"


                End If
            Next
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- UndoRiclassificate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub
    Private Sub UndoBolRateizzate(ByVal idBolRateizzazione As String)
        Try



            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT WHERE ID_VOCE_BOLLETTA IN ( SELECT BOL_BOLLETTE_VOCI.ID FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI " _
                                & "WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID_RATEIZZAZIONE = (SELECT ID_RATEIZZAZIONE FROM SISCOM_MI.BOL_RATEIZZAZIONI_DETT WHERE ID_BOLLETTA = " & idBolRateizzazione & ") )  " _
                                & "AND ID_EVENTO_PRINCIPALE in (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE id_incasso_extramav = " & idIncasso & ")"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Dim ImpTemp As Decimal = 0
            For Each r As Data.DataRow In dt.Rows

                If par.IfNull(r.Item("ID_VOCE_BOLLETTA"), 0) > 0 Then



                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                        & "IMP_PAGATO = (NVL(IMP_PAGATO,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                        & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and nvl(imp_pagato,0) > 0"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                        & "importo_riclassificato_pagato = (NVL(importo_riclassificato_pagato,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                        & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and NVL(importo_riclassificato_pagato,0) > 0"
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                        & "imp_pagato_bak = (NVL(imp_pagato_bak,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                        & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and NVL(imp_pagato_bak,0) > 0"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                        & "IMP_PAGATO = null, imp_pagato_bak = null where importo < 0  and id_bolletta in (select id_bolletta from siscom_mi.bol_bollette_voci where id = " & r.Item("ID_VOCE_BOLLETTA") & ")"
                    par.cmd.ExecuteNonQuery()


                End If
            Next

        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- UndoBolRateizzate" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub


    Protected Sub btnProcedi_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '*************************APERTURA TRANSAZIONE****************************
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI WHERE (EVENTI_PAGAMENTI_PARZIALI.ID =" & IdEvento & " OR EVENTI_PAGAMENTI_PARZIALI.ID_MAIN = " & IdEvento & ") AND FL_ANNULLATA = 0"
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI WHERE ID_INCASSO_EXTRAMAV = " & idIncasso & " AND FL_ANNULLATA = 0"

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then

                'modifica marco 11/09/2012
                AnnullaIncassiAttribuiti()
                '-------------------------
                ReallineaPagateTotali()
                'par.cmd.CommandText = "SELECT EVENTI_PAGAMENTI_PARZIALI.ID,SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT .*,BOL_BOLLETTE.ID AS ID_BOLLETTA,BOL_BOLLETTE.ID_TIPO, BOL_BOLLETTE_VOCI.id_voce " _
                '    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI, SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
                '    & "WHERE EVENTI_PAGAMENTI_PARZIALI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_EVENTO_PRINCIPALE AND (EVENTI_PAGAMENTI_PARZIALI.ID =" & IdEvento & " OR EVENTI_PAGAMENTI_PARZIALI.ID_MAIN = " & IdEvento & ") AND " _
                '    & "BOL_BOLLETTE_VOCI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID " _
                '    & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL "


                par.cmd.CommandText = "SELECT EVENTI_PAGAMENTI_PARZIALI.ID,SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT .*,BOL_BOLLETTE.ID AS ID_BOLLETTA,BOL_BOLLETTE.ID_TIPO, BOL_BOLLETTE_VOCI.id_voce " _
                                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI, SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
                                    & "WHERE EVENTI_PAGAMENTI_PARZIALI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_EVENTO_PRINCIPALE AND ID_INCASSO_EXTRAMAV = " & idIncasso & " AND " _
                                    & "BOL_BOLLETTE_VOCI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID " _
                                    '& "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL "

                Dim IdBolletta As String = "0"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim dt As New Data.DataTable

                da.Fill(dt)

                'Dim reader = par.cmd.ExecuteReader


                For Each r As Data.DataRow In dt.Rows
                    If par.IfNull(r.Item("ID_VOCE_BOLLETTA"), 0) > 0 Then



                        '*********SCALO L'IMPORTO PAGATO DELLA VOCE DI BOLLETTA CHE ERA STATA PAGATA MANUALMENTE

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                            & "IMP_PAGATO = (NVL(IMP_PAGATO,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                            & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and NVL(IMP_PAGATO,0) > 0"
                        par.cmd.ExecuteNonQuery()

                        '**************update fl_pag_parziali = fl_pag_parziali - 1

                        '' ''If par.IfNull(r.Item("ID_VOCE"), 1) = 150 Or par.IfNull(r.Item("ID_VOCE"), 1) = 151 Then
                        '' ''    UndoRiclassificate(par.IfNull(r.Item("ID_BOLLETTA"), 0))
                        '' ''End If
                        ' '' ''******************23/08/2011 se sto per pagare una bolletta nata da una rateizzazione, allora aggiorno importo_pagato della bolletta...
                        '' ''If par.IfNull(r.Item("ID_TIPO"), 1) = 5 Then
                        '' ''    If IdBolletta <> par.IfNull(r.Item("ID_BOLLETTA"), 0) Then
                        '' ''        IdBolletta = par.IfNull(r.Item("ID_BOLLETTA"), 0)
                        '' ''        UndoBolRateizzate(IdBolletta)
                        '' ''    End If
                        '' ''End If

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                            & "importo_riclassificato_pagato = (NVL(importo_riclassificato_pagato,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                            & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and NVL(importo_riclassificato_pagato,0) > 0"
                        par.cmd.ExecuteNonQuery()


                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                            & "imp_pagato_bak = (NVL(imp_pagato_bak,0) - " & par.VirgoleInPunti(par.IfNull(r.Item("IMPORTO"), 0)) & ") " _
                                            & "WHERE ID = " & r.Item("ID_VOCE_BOLLETTA") & " and NVL(imp_pagato_bak,0) > 0"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI SET " _
                                            & "IMP_PAGATO = null, imp_pagato_bak = null , importo_riclassificato_pagato = null where importo < 0  and id_bolletta in (select id_bolletta from siscom_mi.bol_bollette_voci where id = " & r.Item("ID_VOCE_BOLLETTA") & ")"
                        par.cmd.ExecuteNonQuery()



                        '' ''par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_PAGAMENTO = nvl((SELECT BOL_BOLLETTE_DATE_PAG.DATA_PREC FROM SISCOM_MI.BOL_BOLLETTE_DATE_PAG WHERE " _
                        '' ''                     & "ID_EVENTO = " & IdEvento & " AND ID_BOLLETTA = " & par.IfNull(r.Item("ID_BOLLETTA"), 0) & "),null) WHERE ID = " & par.IfNull(r.Item("ID_BOLLETTA"), 0)
                        '' ''par.cmd.ExecuteNonQuery()


                    End If
                Next



                da.Dispose()
                dt.Clear()
                dt.Dispose()
                '*/*/*/*/*/*/*/* MODIFICA AGGIORNAMENTO DATA PAGAMENTO DELLE BOLLETTE 11/11/2011
                dt = New Data.DataTable
                par.cmd.CommandText = "SELECT DISTINCT EVENTI_PAGAMENTI_PARZIALI.ID,BOL_BOLLETTE.ID AS ID_BOLLETTA, IMPORTO_PAGATO " _
                                    & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI, SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
                                    & "WHERE EVENTI_PAGAMENTI_PARZIALI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_EVENTO_PRINCIPALE AND ID_INCASSO_EXTRAMAV = " & idIncasso & " AND " _
                                    & "BOL_BOLLETTE_VOCI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID " _
                                    & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)


                For Each r As Data.DataRow In dt.Rows
                    If par.IfNull(r.Item("IMPORTO_PAGATO"), 0) = 0 Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_PAGAMENTO = null,OPERATORE_PAG = '',DATA_INS_PAGAMENTO = '',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) - 1 WHERE ID = " & par.IfNull(r.Item("ID_BOLLETTA"), 0)
                    Else
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET OPERATORE_PAG = '',DATA_INS_PAGAMENTO = '', FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) - 1, DATA_PAGAMENTO = nvl((SELECT BOL_BOLLETTE_DATE_PAG.DATA_PREC FROM SISCOM_MI.BOL_BOLLETTE_DATE_PAG WHERE " _
                                             & "ID_EVENTO = " & IdEvento & " AND ID_BOLLETTA = " & par.IfNull(r.Item("ID_BOLLETTA"), 0) & "),null) WHERE ID = " & par.IfNull(r.Item("ID_BOLLETTA"), 0)
                    End If
                    par.cmd.ExecuteNonQuery()
                Next
                dt.Dispose()


                '*/*/*/*/*/*/*/*FINE DELLA MODIFICA AGGIORNAMENTO DATA PAGAMENTO DELLE BOLLETTE 11/11/2011 */*/*/*/*/*/*/*/*/*

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_CONTRATTO,ID_INCASSO_EXTRAMAV) " _
                        & "VALUES ( SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZIALI.NEXTVAL," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                        & "'F02','ANNULLO ULTIMA OPERAZIONE '," & Request.QueryString("IDCONT") & "," & idIncasso & ")"
                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "UPDATE SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI SET FL_ANNULLATA = 1 " _
                '                    & "WHERE ID = " & IdEvento & " or id_main = " & IdEvento
                par.cmd.CommandText = "UPDATE SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI SET FL_ANNULLATA = 1 " _
                                    & "WHERE ID_INCASSO_EXTRAMAV = " & idIncasso

                par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "delete FROM siscom_mi.crediti WHERE id_evento_pagamento IN (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE id_main = " & IdEvento & " or id = " & IdEvento & ") and crediti.spalmabile = 0"
                '19/09/2012 annullo il credito generato da un incasso
                par.cmd.CommandText = "delete FROM siscom_mi.bol_bollette_gest WHERE id_evento_pagamento IN (SELECT ID FROM siscom_mi.eventi_pagamenti_parziali WHERE ID_INCASSO_EXTRAMAV = " & idIncasso & ") " 'and crediti.spalmabile = 0"

                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "update siscom_mi.incassi_extramav set fl_annullata = 1 where id = " & idIncasso
                par.cmd.ExecuteNonQuery()

                '' ''15/10/2012 elimino la data di pagamento della voce di bolletta dalla tabella BOL_BOLLETTE_VOCI_PAGAMENTI
                ' ''par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_pagamenti where id_incasso_extramav = " & idIncasso
                ' ''par.cmd.ExecuteNonQuery()
                '' ''10/04/2013 elimino la data di pagamento della voce di bolletta dalla tabella BOL_BOLLETTE_VOCI_PAGAMENTI2
                ' ''par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_pagamenti2 where id_incasso_extramav = " & idIncasso
                ' ''par.cmd.ExecuteNonQuery()

                '03/12/2013 al posto delle delete in bol_bollette_voci_pagamentei e bol_bollette_voci_pagamenti2
                'inseriamo l'importo negativo della voce che viene annullata

                par.cmd.CommandText = "select * from siscom_mi.bol_bollette_voci_pagamenti where id_incasso_extramav = " & idIncasso
                Dim reader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While reader2.Read
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                    & "(" & reader2("id_voce_bolletta") & ",'" & par.IfNull(reader2("DATA_PAGAMENTO"), "") & "',-" & par.VirgoleInPunti(par.IfNull(reader2("IMPORTO_PAGATO"), 0)) & "," & reader2("ID_TIPO_PAGAMENTO") & "," & idIncasso & ")"
                    par.cmd.ExecuteNonQuery()

                End While
                reader2.Close()

                par.cmd.CommandText = "select * from siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI2 where id_incasso_extramav = " & idIncasso
                reader2 = par.cmd.ExecuteReader
                While reader2.Read
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI2 (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                    & "(" & reader2("id_voce_bolletta") & ",'" & par.IfNull(reader2("DATA_PAGAMENTO"), "") & "',-" & par.VirgoleInPunti(par.IfNull(reader2("IMPORTO_PAGATO"), 0)) & "," & reader2("ID_TIPO_PAGAMENTO") & "," & idIncasso & ")"
                    par.cmd.ExecuteNonQuery()

                End While
                reader2.Close()

                Response.Write("<script>alert('Operazione eseguita correttamente!Il pagamento è stato annullato!');window.returnValue = '1';self.close();</script>")
            Else
                'Response.Write("<script>alert('Operazione non riuscita!');window.returnValue = '1';self.close();</script>")

            End If

            '*********************COMMIT OPERAZIONI ESEGUITE E CHIUSURA CONNESSIONE**********************
            par.myTrans.Commit()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- btnProcedi_Click" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Private Sub ReallineaPagateTotali()
        Try
            'par.cmd.CommandText = "SELECT DISTINCT(BOL_BOLLETTE.ID) AS ID_BOLLETTA " _
            '        & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI, SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
            '        & "WHERE EVENTI_PAGAMENTI_PARZIALI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_EVENTO_PRINCIPALE AND (EVENTI_PAGAMENTI_PARZIALI.ID =" & IdEvento & " OR EVENTI_PAGAMENTI_PARZIALI.ID_MAIN = " & IdEvento & ") AND " _
            '        & "BOL_BOLLETTE_VOCI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID " _
            '        & "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL "
            par.cmd.CommandText = "SELECT DISTINCT(BOL_BOLLETTE.ID) AS ID_BOLLETTA " _
                                & "FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI, SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
                                & "WHERE EVENTI_PAGAMENTI_PARZIALI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_EVENTO_PRINCIPALE AND ID_INCASSO_EXTRAMAV = " & idIncasso & " AND " _
                                & "BOL_BOLLETTE_VOCI.ID = SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT.ID_VOCE_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID " _
                                '& "AND BOL_BOLLETTE.ID_BOLLETTA_RIC IS NULL AND BOL_BOLLETTE.ID_RATEIZZAZIONE IS NULL "


            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim letPagamenti As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                par.cmd.CommandText = "select importo_totale,importo_pagato from siscom_mi.bol_bollette where id = " & lettore("ID_BOLLETTA")
                letPagamenti = par.cmd.ExecuteReader
                If letPagamenti.Read Then
                    If par.IfNull(letPagamenti("IMPORTO_TOTALE"), 0) = par.IfNull(letPagamenti("IMPORTO_PAGATO"), 0) Then
                        par.cmd.CommandText = "update siscom_Mi.bol_bollette_voci set imp_pagato = nvl(imp_pagato_bak,null), importo_riclassificato_pagato = nvl(imp_pagato_bak,null) where id_bolletta = " & lettore("ID_BOLLETTA")
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set imp_pagato_bak = null where id_bolletta = " & lettore("ID_BOLLETTA")
                        par.cmd.ExecuteNonQuery()
                    End If
                End If
                letPagamenti.Close()
            End While
            lettore.Close()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- ReallineaPagateTotali" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    'MODIFICA MARCO 11/09/2012
    Private Sub AnnullaIncassiAttribuiti()
        'annulla tutti i record di incassi_attribuiti relativi all'incasso extramav
        'in questione. Riazzera il flag dell'assegno che era stato 
        'utilizzato per l'incasso
        Try
            Dim resNonAttribuiti As Decimal = 0
            par.cmd.CommandText = "select sum(nvl(importo,0)) as tot from siscom_mi.incassi_attribuiti where id_incasso_extramav = " & idIncasso
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                resNonAttribuiti = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "UPDATE (SELECT * FROM SISCOM_MI.INCASSI_ATTRIBUITI " _
                                & " WHERE ID_INCASSO_EXTRAMAV=" & idIncasso & ") AA SET AA.FL_ANNULLATO=1 "
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.INCASSI_NON_ATTRIBUIBILI " _
                                & " SET FL_ATTRIBUITO=0,DATA_ATTRIBUZIONE=NULL,importo_residuo = importo_residuo+" & par.VirgoleInPunti(resNonAttribuiti) _
                                & " WHERE ID IN (SELECT DISTINCT ID_INCASSO_NON_ATTR FROM SISCOM_MI.INCASSI_ATTRIBUITI WHERE ID_INCASSO_EXTRAMAV=" & idIncasso & ")"
            par.cmd.ExecuteNonQuery()
        Catch ex As Exception
            '*********************CHIUSURA TRANSAZIONE E CONNESSIONE**********************
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>- AnnullaIncassiAttribuiti" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    '----------------------------------
End Class
