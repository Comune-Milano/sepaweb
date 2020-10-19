
Partial Class RATEIZZAZIONE_RateizzMorositaPregressa
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Private Sub RATEIZZAZIONE_RateizzMorositaPregressa_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Trim(Session.Item("ID_OPERATORE"))) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then

            txtDataSaldo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            If Not IsNothing(Request.QueryString("CODRU")) Then
                codContratto.Value = Request.QueryString("CODRU")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                idRateizz.Value = Request.QueryString("ID")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("DPR")) Then
                dataPres.Value = par.AggiustaData(Request.QueryString("DPR"))
            End If
            CaricaInfoContrattuali()
            If par.IfEmpty(idRateizz.Value, 0) > 0 Then
                CaricaDatiSalvati()
            End If

        End If
    End Sub

    Private Sub CaricaDatiSalvati()

        connData.apri(False)
        Me.connData.RiempiPar(par)

        Try
            If par.IfEmpty(idRateizz.Value, "0") <> "0" Then
                par.cmd.CommandText = "select id_stato from siscom_mi.bol_rateizzazioni where id = " & idRateizz.Value
                Dim idStato As String = par.cmd.ExecuteScalar
                If idStato = 0 Then
                    par.cmd.CommandText = "select bol_rateizzazioni.*,AREA_ECONOMICA.descrizione ||' '|| classe as fascia from siscom_mi.bol_rateizzazioni,siscom_mi.AREA_ECONOMICA where BOL_RATEIZZAZIONI.id_AREA_ECONOMICA=area_Economica.id and BOL_RATEIZZAZIONI.id = " & idRateizz.Value
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Me.Fascia.Value = par.IfNull(lettore("fascia"), "")

                        Me.txtDataSaldo.Text = par.FormattaData(par.IfNull(lettore("data_saldo_morosita"), "20161231"))
                        Me.sDataSald.Value = Me.txtDataSaldo.Text

                        Me.lblSaldo.Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:window.open('../Contabilita/EstrattoConto_New.aspx?RIFAL=" & par.AggiustaData(txtDataSaldo.Text) & "&IDANA=" & idAna.Value & "&IDCONT=" & idContratto.Value & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(CalcolaDebitoAlData(idContratto.Value, par.AggiustaData(txtDataSaldo.Text)), "##,##0.00") & " €</a>"


                    End If
                    lettore.Close()
                    If Fascia.Value <> "" Then
                        Me.lblFascia.Text = "AREA CANONE: " & Fascia.Value & ""
                        Me.cmbTipoContr.Enabled = False
                    Else
                        Me.lblFascia.Style.Value = " display:none "
                    End If
                    Me.cmbTipoContr.Enabled = False
                Else
                    If idStato = 1 Then

                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "domLoc", "alert('Il piano di rientro è stato APPROVATO!\nVerrà aperto il riepilogo!');", True)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "op", "window.open('RateizDati.aspx?TIPO=M&IDCONTRATTO=" & idContratto.Value & "&IDRAT=" & idRateizz.Value & "','RATEIZZO', 'menubar=1,resizable=1,height=598,width=920,scrollbars=no');", True)

                    Else
                        par.cmd.CommandText = "SELECT DESCRIZIONE FROM siscom_mi.TIPO_STATO_RATEIZZ WHERE ID = " & idStato
                        Dim STATO As String = par.cmd.ExecuteScalar

                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('PIANO DI RIENTRO " & STATO & "! Impossibile procedere!');self.close();", True)

                    End If
                End If
            End If

            connData.chiudi(False)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: RATEIZZAZIONE- CaricaDatiSalvati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function CalcolaDebitoAlData(ByVal IdContratto As Long, ByVal dataSaldo As String) As Double
        Dim DaChiudere As Boolean = False

        Dim TotSaldo As Double = 0
        Dim idGr As Integer = 0
        Dim idGrOrd As Integer = 0

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            DaChiudere = True
            par.cmd = par.OracleConn.CreateCommand()
        End If

        'par.cmd.CommandText = " select nvl(SUM (tot_emesso - tot_incassato),0) AS SALDO  from (  " _
        '            & " select rapporti_utenza.COD_CONTRATTO,  " _
        '            & " SUM (IMPORTO_TOTALE /*-NVL(IMPORTO_RIC_B, 0)*/ - NVL(QUOTA_SIND_B, 0)) AS tot_emesso,   " _
        '            & " NVL ( SUM ( IMPORTO_PAGATO - nvl((SELECT SUM (imp_pagato) FROM siscom_mi.bol_bollette_voci WHERE bol_bollette_voci.id_bolletta = bol_bollette.id   " _
        '            & " AND (/*id_voce IN (150, 151, 677, 676, 7, 126, 182) OR*/ id_voce IN (SELECT id FROM siscom_mi.t_voci_bolletta WHERE gruppo = 5))),0) /*- NVL (IMPORTO_RIC_PAGATO_B, 0)*/) - NVL ( (SELECT SUM (importo_pagato)   " _
        '            & " FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE data_pagamento  > '" & dataPres.Value & "'   " _
        '            & " AND id_gruppo_voce_bolletta <> 5 /*AND id_t_voce_bolletta NOT IN (150, 151, 677, 676, 7, 126, 182)*/  " _
        '            & " AND id_bolletta = BOL_BOLLETTE.ID AND bol_bollette.id_contratto = rapporti_utenza.id), 0), 0) AS tot_incassato " _
        '            & " FROM siscom_mi.rapporti_utenza,siscom_mi.bol_bollette WHERE " _
        '            & " (bol_bollette.DATA_SCADENZA<= '" & dataSaldo & "' or BOL_BOLLETTE.id_tipo=26 or BOL_BOLLETTE.id_tipo=27)  " _
        '            & " and BOL_BOLLETTE.id_tipo NOT IN (22,25)  " _
        '            & " and id_Bolletta_storno is null " _
        '            & " and nvl(importo_ruolo,0)=0 " _
        '            & " and nvl(importo_ingiunzione,0)=0  " _
        '            & " and rapporti_utenza.id=" & IdContratto & "  " _
        '            & " And BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID  " _
        '            & " And (FL_ANNULLATA = 0 Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL)) " _
        '            & " And bol_bollette.id_bolletta_ric Is null " _
        '            & " And bol_bollette.id_rateizzazione Is null " _
        '            & " AND nvl(bol_bollette.importo_totale,0) >nvl(bol_bollette.importo_pagato,0) " _
        '            & " GROUP BY rapporti_utenza.COD_CONTRATTO,BOL_BOLLETTE.ID,BOL_BOLLETTE.ID_contratto,rapporti_utenza.id) "

        'Segn. 2052/2018
        '

        par.cmd.CommandText = " select nvl(SUM (tot_emesso - tot_incassato),0) AS SALDO  from (  " _
                            & " select rapporti_utenza.COD_CONTRATTO,  " _
                            & " SUM (IMPORTO_TOTALE /*-NVL(IMPORTO_RIC_B, 0)*/ - NVL(QUOTA_SIND_B, 0)) AS tot_emesso,   " _
                            & " NVL ( SUM ( IMPORTO_PAGATO - nvl((SELECT SUM (imp_pagato) FROM siscom_mi.bol_bollette_voci WHERE bol_bollette_voci.id_bolletta = bol_bollette.id   " _
                            & " AND (/*id_voce IN (150, 151, 677, 676, 7, 126, 182) OR*/ id_voce IN (SELECT id FROM siscom_mi.t_voci_bolletta WHERE gruppo = 5))),0)), 0) AS tot_incassato " _
                            & " FROM siscom_mi.rapporti_utenza,siscom_mi.bol_bollette WHERE " _
                            & " (bol_bollette.DATA_SCADENZA<= '" & dataSaldo & "' or BOL_BOLLETTE.id_tipo=26 or BOL_BOLLETTE.id_tipo=27)  " _
                            & " and BOL_BOLLETTE.id_tipo NOT IN (22,25)  " _
                            & " and id_Bolletta_storno is null " _
                            & " and nvl(importo_ruolo,0)=0 " _
                            & " and nvl(importo_ingiunzione,0)=0  " _
                            & " and rapporti_utenza.id=" & IdContratto & "  " _
                            & " And BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID  " _
                            & " And (FL_ANNULLATA = 0 Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL)) " _
                            & " And bol_bollette.id_bolletta_ric Is null " _
                            & " And bol_bollette.id_rateizzazione Is null " _
                            & " AND nvl(bol_bollette.importo_totale,0) >nvl(bol_bollette.importo_pagato,0) " _
                            & " GROUP BY rapporti_utenza.COD_CONTRATTO,BOL_BOLLETTE.ID,BOL_BOLLETTE.ID_contratto,rapporti_utenza.id) "


        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader3.Read Then
            TotSaldo = par.IfNull(myReader3("SALDO"), 0)
        End If
        myReader3.Close()

        If DaChiudere = True Then
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If

        CalcolaDebitoAlData = TotSaldo

    End Function
    Private Sub CaricaInfoContrattuali()
        connData.apri(False)
        Me.connData.RiempiPar(par)

        Try
            par.cmd.CommandText = "select rapporti_utenza.id,cod_contratto,unita_immobiliari.cod_unita_immobiliare,cod_tipologia_contr_loc,(indirizzi.descrizione||', '||indirizzi.civico||'') AS INDIRIZZO from siscom_mi.rapporti_utenza,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.INDIRIZZI where " _
                & " rapporti_utenza.id=unita_contrattuale.id_contratto and UNITA_IMMOBILIARI. ID_INDIRIZZO = INDIRIZZI.ID(+) and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_unita=unita_immobiliari.id and cod_contratto = '" & codContratto.Value & "'"
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                idContratto.Value = par.IfNull(reader("id"), 0)
                Me.lblCodContratto.Text = par.IfNull(reader("cod_contratto"), "N.D.")
                lblCodUI.Text = par.IfNull(reader("cod_unita_immobiliare"), "N.D.")
                sCodice.Value = par.IfNull(reader("cod_contratto"), "")
                cmbTipoContr.Text = par.IfNull(reader("cod_tipologia_contr_loc"), "")
                lblIndirizzo.Text = par.IfNull(reader("indirizzo"), "")
            End If
            reader.Close()

            '********************BLOCCO EISTENZA ALTRO PIANO DI RIENTRO CREATO CON SEPA****************************
            If idRateizz.Value = "0" Or idRateizz.Value = "" Or idRateizz.Value = "-1" Then
                par.cmd.CommandText = "select id from siscom_mi.bol_rateizzazioni where fl_annullata= 0 /*and tipologia = 'M'*/ and id_contratto = " & idContratto.Value
                Dim esistRateizzo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If esistRateizzo <> 0 Then
                    connData.chiudi(False)
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ESISTE UN ALTRO PIANO DI RIENTRO SUL CONTRATTO!\nAPRIRE QUELLO ESISTENTE!');self.close();", True)
                    Exit Sub
                End If
            End If
            '********************FINE ALTRO PIANO DI RIENTRO CREATO CON SEPA****************************

            '********************ALTRI RATEIZZI SUL CONTRATTO****************************
            par.cmd.CommandText = "select getdata(data_emissione) as data,data_stipula AS DATAORD,tot_rateizzato AS DEBITO,num_rate," _
                                & "(SELECT DESCRIZIONE FROM siscom_mi.TIPO_STATO_RATEIZZ WHERE ID = BOL_RATEIZZAZIONI.ID_STATO) AS STATO  from siscom_mi.bol_rateizzazioni " _
                                & "where fl_annullata = 0 and id_contratto = " & idContratto.Value & " and nvl(num_rate,0)>0 and id <> " & idRateizz.Value _
                                & " ORDER BY DATAORD DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            dgvAltrRateizzi.DataSource = dt
            dgvAltrRateizzi.DataBind()
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('ATTENZIONE!Esiste già un piano di rientro!');", True)
            End If
            '********************FINE ALTRI RATEIZZI SUL CONTRATTO****************************

            par.cmd.CommandText = "select id_anagrafica from siscom_mi.soggetti_contrattuali where cod_tipologia_occupante = 'INTE' and id_contratto = " & idContratto.Value
            idAna.Value = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = "select (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END )AS INTECONTRATTO," _
                                & "(CASE WHEN anagrafica.partita_iva IS NOT NULL THEN partita_iva ELSE COD_FISCALE END) AS CFIVA " _
                                & " FROM siscom_mi.ANAGRAFICA WHERE ANAGRAFICA.ID = " & idAna.Value
            reader = par.cmd.ExecuteReader

            If reader.Read Then
                Me.lblIntest.Text = par.IfNull(reader("INTECONTRATTO"), "n.d")
                sIntestatario.Value = Me.lblIntest.Text
                Me.lblCfIva.Text = par.IfNull(reader("CFIVA"), "n.d")
            End If
            reader.Close()

            Me.lblSaldo.Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:window.open('../Contabilita/EstrattoConto_New.aspx?IDANA=" & idAna.Value & "&IDCONT=" & idContratto.Value & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(CalcolaDebitoAlData(idContratto.Value, "20161231"), "##,##0.00") & " €.</a>"
            Me.txtDataSaldo.Text = "31/12/2016"
            Me.lblDataSaldo.Text = "SALDO AL "

            Me.sDataSald.Value = Me.txtDataSaldo.Text


            connData.chiudi(False)

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: RATEIZZ_MOROSITA - CaricaInfoContrattuali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub btnConfProcedi_Click(sender As Object, e As ImageClickEventArgs) Handles btnConfProcedi.Click
        If CalcolaDebitoAlData(idContratto.Value, par.AggiustaData(txtDataSaldo.Text)) > 0 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "domLoc", "location.href='BolRateizzabili.aspx?RATM=1&DPR=" & dataPres.Value & "&DATAS=" & txtDataSaldo.Text & "&CODRU=" + codContratto.Value + "&IDCONTRATTO=" & idContratto.Value & "&IDRAT=" & idRateizz.Value & "'", True)

        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Impossibile creare un piano di rientro con im porto inferiore o pari a zero!');", True)

        End If
    End Sub

    Private Sub btnRefreshSaldo_Click(sender As Object, e As ImageClickEventArgs) Handles btnRefreshSaldo.Click
        If Not String.IsNullOrEmpty(Me.txtDataSaldo.Text) Then
            'par.CalcolaSaldoAlData(idContratto.Value, par.AggiustaData(Me.txtDataSaldo.Text))
            Me.lblSaldo.Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:window.open('../Contabilita/EstrattoConto_New.aspx?AL=" & par.AggiustaData(txtDataSaldo.Text) & "&IDANA=" & idAna.Value & "&IDCONT=" & idContratto.Value & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(CalcolaDebitoAlData(idContratto.Value, par.AggiustaData(txtDataSaldo.Text)), "##,##0.00") & " €</a>"

        Else
            Me.txtDataSaldo.Text = sDataSald.Value
            Me.lblSaldo.Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:window.open('../Contabilita/EstrattoConto_New.aspx?AL=" & par.AggiustaData(txtDataSaldo.Text) & "&IDANA=" & idAna.Value & "&IDCONT=" & idContratto.Value & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(CalcolaDebitoAlData(idContratto.Value, par.AggiustaData(txtDataSaldo.Text)), "##,##0.00") & " €</a>"

        End If
    End Sub
End Class
