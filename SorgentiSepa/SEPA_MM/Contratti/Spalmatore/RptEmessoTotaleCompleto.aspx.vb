Imports System.IO
Imports Telerik.Web.UI
Partial Class Contratti_Spalmatore_RptEmessoTotaleCompleto
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Sub Contratti_Spalmatore_RptEmessoTotaleCompleto_Load(sender As Object, e As EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            LeggiUltimaData()
        End If
    End Sub

    Private Sub LeggiUltimaData()
        Try
            connData.apri()
            dataScad = Format(Now, "yyyyMMdd")

            par.cmd.CommandText = "select nvl(data_scadenza_boll,to_char(sysdate, 'yyyyMMdd')) from SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST where id = (SELECT max(id) FROM SISCOM_MI.ELABORAZIONE_SCRITTURE_GEST esg)"
            dataScad = par.IfEmpty(par.cmd.ExecuteScalar, Format(Now, "yyyyMMdd"))

            txtDataScad.SelectedDate = par.FormattaData(dataScad)

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Public Property dataScad() As String
        Get
            If Not (ViewState("par_dataScad") Is Nothing) Then
                Return CStr(ViewState("par_dataScad"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_dataScad") = value
        End Set

    End Property
    Private Sub CreaRptEmessoTotale()
        Try

            Dim condizioneDateScad As String = ""
            Dim condizioneDateScad2 As String = ""
            dataScad = par.AggiustaData(CDate(txtDataScad.SelectedDate).ToShortDateString)

            If dataScad <> "" Then
                condizioneDateScad = " and b.data_scadenza <= '" & dataScad & "' "
                condizioneDateScad2 = " and bo.data_scadenza <= '" & dataScad & "' "
            End If

            par.cmd.CommandText = "SELECT DATA_ESTRAZIONE, " _
                                & "DATA_SCADENZA_BOLLETTE, " _
                                & "RU, " _
                                & "ID_CONTRATTO, " _
                                & "COD_TIPOLOGIA_CONTR_LOC, " _
                                & "TOT_EMESSO_CONTABILE + NVL (importo_spese_mor_rat, 0) " _
                                & "TOT_EMESSO_CONTABILE, " _
                                & "TOT_MONTE_CANONE, " _
                                & "TOT_MONTE_CANONE_PAG, " _
                                & "TOT_PAGATO_CONTABILE + NVL (importo_spese_mor_rat_pag, 0) " _
                                & "TOT_PAGATO_CONTABILE, " _
                                & "TOT_PARTITA_GIRO, " _
                                & "TOT_PARTITA_GIRO_PAGATO, " _
                                & "TOT_RUOLO, " _
                                & "TOT_RUOLO_PAGATO, " _
                                & "TOT_INGIUNTO, " _
                                & "TOT_INGIUNTO_PAGATO, " _
                                & "TOT_SALDO_CONTABILE " _
                                & "+ (NVL (importo_spese_mor_rat, 0) - NVL (importo_spese_mor_rat_pag, 0)) " _
                                & "TOT_SALDO_CONTABILE, " _
                                & "TOT_SALDO_PER_SPALMATORE " _
                                & "+ (NVL (importo_spese_mor_rat, 0) - NVL (importo_spese_mor_rat_pag, 0)) " _
                                & "TOT_SALDO_PER_SPALMATORE " _
                                & "FROM (SELECT bo.id_contratto id_cnt, " _
                                    & "SUM (NVL (bov.importo, 0)) importo_spese_mor_rat, " _
                                    & "SUM (NVL (bov.imp_pagato, 0)) importo_spese_mor_rat_pag " _
                                    & "FROM siscom_mi.bol_bollette bo, siscom_mi.bol_bollette_voci bov " _
                                    & "WHERE     bo.id = bov.id_bolletta " _
                                    & "AND bo.id_tipo IN (4, 5) " _
                                    & "AND bo.fl_annullata = 0 " _
                                    & condizioneDateScad2 _
                                    & "AND bo.id_bolletta_storno IS NULL and bov.id_voce not in (select id from siscom_mi.t_voci_bolletta where fl_no_report =1) " _
                                    & "GROUP BY id_contratto) y, " _
                                & "(  SELECT DATA_eSTRAZIONE, " _
                                & "DATA_sCADENZA_BOLLETTE, " _
                                & "RU, " _
                                & "ID_CONTRATTO, " _
                                & "COD_TIPOLOGIA_CONTR_LOC, " _
                                & "SUM (TOT_EMESSO_CONTABILE) tot_emesso_contabile, " _
                                & "SUM (TOT_MONTE_CANONE) tot_monte_canone, " _
                                & "SUM (TOT_MONTE_cANONE_PAG) tot_monte_canone_pag, " _
                                & "SUM (TOT_PAGATO_CONTABILE) tot_pagato_contabile, " _
                                & "SUM (TOT_PARTITA_GIRO) tot_partita_giro, " _
                                & "SUM (TOT_PARTITA_GIRO_PAGATO) tot_partita_giro_pagato, " _
                                & "SUM (TOT_RUOLO) tot_ruolo, " _
                                & "SUM (TOT_RUOLO_PAGATO) tot_ruolo_pagato, " _
                                & "SUM (TOT_INGIUNTO) TOT_INGIUNTO, " _
                                & "SUM (TOT_INGIUNTO_PAGATO) TOT_INGIUNTO_PAGATO, " _
                                & "SUM (TOT_SALDO_CONTABILE) tot_saldo_contabile, " _
                                & "SUM (TOT_SALDO_PER_SPALMATORE) TOT_SALDO_PER_SPALMATORE " _
                                & "FROM ( " _
                                & "( (  SELECT TO_CHAR (SYSDATE, 'DD/MM/YYYY') data_estrazione, " _
                                & "'" & par.FormattaData(dataScad) & "' as data_scadenza_bollette, " _
                                & "u.cod_contratto ru, " _
                                & "u.id id_contratto, " _
                                & "u.cod_tipologia_contr_loc, " _
                                & "SUM (b.EMESSO_CONTABILE) tot_emesso_contabile, " _
                                & "SUM (x.tot_monte_canone) tot_monte_canone, " _
                                & "SUM (x.tot_monte_canone_pag) tot_monte_canone_pag, " _
                                & "SUM (b.INCASSATO_CONTABILE) tot_pagato_contabile, " _
                                & "SUM (NVL (b.quota_sind_b, 0)) tot_partita_giro, " _
                                & "SUM (NVL (b.quota_sind_pagata_b, 0)) " _
                                & "tot_partita_giro_pagato, " _
                                & "SUM (NVL (b.importo_ruolo, 0)) tot_ruolo, " _
                                & "SUM (NVL (b.imp_ruolo_pagato, 0)) tot_ruolo_pagato, " _
                                & "SUM (NVL (b.importo_ingiunzione, 0)) tot_ingiunto, " _
                                & "SUM (NVL (b.IMP_INGIUNZIONE_PAG, 0)) tot_ingiunto_pagato, " _
                                & "SUM (b.saldo_contabile) tot_saldo_contabile, " _
                                & "SUM (b.EMESSO_CONTABILE) " _
                                & "- SUM (b.INCASSATO_CONTABILE) " _
                                & "- SUM (NVL (b.importo_ruolo, 0)) " _
                                & "- SUM (NVL (b.importo_ingiunzione, 0)) " _
                                & "TOT_SALDO_PER_SPALMATORE " _
                                & "FROM siscom_mi.rapporti_utenza u, " _
                                & "siscom_mi.v_saldo_bollette b, " _
                                & "(  SELECT v.id_bolletta, " _
                                & "SUM (v.importo) tot_monte_canone, " _
                                & "SUM (v.imp_pagato) tot_monte_canone_pag " _
                                & "FROM siscom_mi.bol_bollette_voci v " _
                                & "WHERE v.id_voce IN " _
                                & "(SELECT t.id " _
                                & "FROM SISCOM_MI. " _
                                & "T_VOCI_BOLLETTA t " _
                                & "WHERE t.gruppo = 4 " _
                                & "AND t.tipo_voce IN (2, 10, 13)) " _
                                & "GROUP BY v.id_bolletta) x " _
                                & "WHERE u.id = b.id_contratto " _
                                & "AND b.id = x.id_bolletta(+) " _
                                & condizioneDateScad _
                                & "AND b.id_tipo NOT IN (4, 5, 9, 22, 25) " _
                                & "AND b.fl_annullata = 0 " _
                                & "AND b.id_bolletta_storno IS NULL " _
                                & "/*aND id_bolletta_Ric IS NULL " _
                                & "AND id_Rateizzazione IS NULL*/ " _
                                & "GROUP BY u.cod_contratto, u.id, u.cod_tipologia_contr_loc) " _
                                & "UNION " _
                                & "(  SELECT TO_CHAR (SYSDATE, 'DD/MM/YYYY') data_estrazione, " _
                                & "'" & par.FormattaData(dataScad) & "' data_scadenza_bollette, " _
                                & "u.cod_contratto ru, " _
                                & "u.id id_contratto, " _
                                & "u.cod_tipologia_contr_loc, " _
                                & "0 tot_emesso_contabile, " _
                                & "0 tot_monte_canone, " _
                                & "0 tot_monte_canone_pag, " _
                                & "0 tot_pagato_contabile, " _
                                & "0 tot_partita_giro, " _
                                & "0 tot_partita_giro_pagato, " _
                                & "0 tot_ruolo, " _
                                & "0 tot_ruolo_pagato, " _
                                & "0 tot_ingiunto, " _
                                & "0 tot_ingiunto_pagato, " _
                                & "0 tot_saldo_contabile, " _
                                & "0 TOT_SALDO_PER_SPALMATORE " _
                                & "FROM siscom_mi.rapporti_utenza u, " _
                                & "siscom_mi.v_saldo_bollette b, " _
                                & "(  SELECT v.id_bolletta, " _
                                        & "SUM (v.importo) tot_monte_canone, " _
                                        & "SUM (v.imp_pagato) tot_monte_canone_pag " _
                                  & "FROM siscom_mi.bol_bollette_voci v " _
                                  & "WHERE v.id_voce IN " _
                                           & "(SELECT t.id " _
                                              & "FROM SISCOM_MI. " _
                                                    & "T_VOCI_BOLLETTA t " _
                                             & "WHERE t.gruppo = 4 " _
                                                   & "AND t.tipo_voce IN (2, 10, 13)) " _
                               & "GROUP BY v.id_bolletta) x " _
                               & "WHERE     u.id = b.id_contratto " _
                                & "AND b.id = x.id_bolletta(+) " _
                              & condizioneDateScad _
                              & "AND b.id_tipo NOT IN (4, 5, 9, 22, 25) " _
                              & "AND b.fl_annullata = 0 " _
                              & "AND b.id_bolletta_storno IS NULL " _
                     & "GROUP BY u.cod_contratto, u.id, u.cod_tipologia_contr_loc)) " _
                     & ") " _
                    & "GROUP BY DATA_eSTRAZIONE, " _
                    & "DATA_sCADENZA_BOLLETTE, " _
                    & "RU, " _
                    & "ID_CONTRATTO, " _
                    & "COD_TIPOLOGIA_CONTR_LOC) z " _
                    & "WHERE z.id_contratto = y.id_cnt(+) "



            EstraiDati(par.cmd.CommandText, 17)
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Estrazione avviata correttamente!", 300, 150, "Info", "apriMaschera", Nothing)

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub EstraiDati(ByVal strQuery As String, ByVal idTipoReport As Integer)
        Try
            Dim sComando As String = strQuery
            connData.apri()


            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = par.cmd.ExecuteScalar

            If Len(strQuery) < 4000 Then

                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE, Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'" & strQuery.ToString.Replace("'", "''") & "', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,NULL)"
            Else
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE,Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,:TEXT_DATA)"


                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = strQuery
                End With

                par.cmd.Parameters.Add(paramData)
                paramData = Nothing


            End If
            par.cmd.ExecuteNonQuery()

            connData.chiudi()
            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnProcedi_Click(sender As Object, e As EventArgs) Handles btnProcedi.Click
        CreaRptEmessoTotale()
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub
End Class
