Imports Telerik.Web.UI

Partial Class Fondo_solidarieta_FondoSolidarieta
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then

            If IsNothing(Request.QueryString("IDZ")) Then
                Response.Redirect("~/AccessoNegato.htm", True)
                Exit Sub
            Else
                idZona.Value = Request.QueryString("IDZ")
                lblUtente2.Text = "Municipio 0" & CaricaNomeZona(idZona.Value)
            End If
        End If
    End Sub

    Protected Sub RadGrid_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('idSel').value='" & dataItem("ID_CONTRATTO").Text & "';")
                e.Item.Attributes.Add("onDblclick", "document.getElementById('idSel').value='" & dataItem("ID_CONTRATTO").Text & "';")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadGrid_ItemDataBound - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaWindow(ByVal idCont As Long, sender As Object)
        par.cmd.CommandText = "SELECT rapporti_utenza.ID AS ID_CONTRATTO,TRIM(COGNOME) AS COGNOME,TRIM(NOME) AS NOME,TRIM(COD_FISCALE) AS COD_FISCALE,rapporti_utenza.COD_CONTRATTO, " _
            & " COD_TIPOLOGIA_CONTR_LOC as tipo_ru,INDIRIZZI.DESCRIZIONE as indirizzo,INDIRIZZI.civico,SISCOM_MI.GETDATA(SUBSTR(DATA_PRESENTAZIONE,1,8) AS DATA_PRES,SUBSTR(DATA_PRESENTAZIONE,9,2) ||':'|| SUBSTR(DATA_PRESENTAZIONE,11,2) AS ORA_PR, " _
            & "(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004' " _
            & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='V' THEN '431/98 ART.15 C.2 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
            & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) as tipo_specifico, siscom_mi.getstatocontratto(rapporti_utenza.id) as stato, SISCOM_MI.GETDATA(DATA_dECORRENZA) as decorrenza,SISCOM_MI.GETDATA(DATA_DISDETTA_LOCATARIO) as disdetta, " _
            & "(select  DECODE (NVL (CANONI_EC.ID_AREA_ECONOMICA, 0)," _
            & "   1, 'PROTEZIONE'," _
            & "   2, 'ACCESSO'," _
            & "   3, 'PERMANENZA'," _
            & "   4, 'DECADENZA')" _
            & "   from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=8 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
            & "   ) as ""AREA_AU_2013""," _
            & "     (select SOTTO_aREA" _
            & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=8 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
            & "    ) as ""CLASSE_au_2013""," _
            & " (select  DECODE (NVL (CANONI_EC.ID_AREA_ECONOMICA, 0)," _
            & "    1, 'PROTEZIONE'," _
            & "    2, 'ACCESSO'," _
            & "    3, 'PERMANENZA'," _
            & "    4, 'DECADENZA')" _
            & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=10 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
            & "    ) as AREA_AU_2015," _
            & "      (select SOTTO_aREA" _
            & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=10 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
            & "    ) as ""CLASSE_AU_2015""," _
            & "       (select ISEE" _
            & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=10 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
            & "    ) as ""ISEE_ERP_AU_2015""" _
            & " from siscom_mi.rapporti_utenza,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.unita_immobiliari,SISCOM_MI.INDIRIZZI where anagrafica.id=soggetti_Contrattuali.id_anagrafica AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_CONTRATTUALE.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID (+) =UNITA_CONTRATTUALE.ID_UNITA " _
            & " and cod_tipologia_occupante='INTE' and soggetti_contrattuali.id_contratto=rapporti_utenza.id and RAPPORTI_UTENZA.id=" & idCont
        TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)


    End Sub

    Protected Sub RadGrid_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT rapporti_utenza.ID AS ID_CONTRATTO,TRIM(COGNOME) AS COGNOME,TRIM(NOME) AS NOME,TRIM(COD_FISCALE) AS COD_FISCALE,SISCOM_MI.GETDATA(SUBSTR(DATA_PRESENTAZIONE,1,8)) AS DATA_PR,(CASE WHEN DATA_PRESENTAZIONE IS NOT NULL THEN SUBSTR(DATA_PRESENTAZIONE,9,2) ||':'|| SUBSTR(DATA_PRESENTAZIONE,11,2) ELSE '' END) AS ORA_PR,siscom_mi.getstatocontratto(rapporti_utenza.id) as stato,rapporti_utenza.COD_CONTRATTO,INDIRIZZI.DESCRIZIONE as indirizzo, INDIRIZZI.civico " _
                & " from siscom_mi.rapporti_utenza,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica,siscom_mi.unita_contrattuale,siscom_mi.unita_immobiliari,siscom_mi.edifici,siscom_mi.indirizzi,SISCOM_mi.domande_fondo_solidarieta where " _
                & " anagrafica.id=soggetti_Contrattuali.id_anagrafica  and unita_contrattuale.id_contratto=rapporti_utenza.id and siscom_mi.unita_contrattuale.id_unita=siscom_mi.unita_immobiliari.id and SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA.id_contratto(+)=rapporti_utenza.id " _
                & " and unita_contrattuale.id_unita_principale is null and edifici.id=unita_immobiliari.id_Edificio and cod_tipologia_contr_loc='ERP' and bozza=0 and soggetti_contrattuali.id_contratto=rapporti_utenza.id " _
                & " and cod_tipologia_occupante='INTE' AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO AND (edifici.id_zona=" & Request.QueryString("IDZ") & " or edifici.id_zona=19) ORDER BY COGNOME,NOME"
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadGrid_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function CaricaNomeZona(ByVal idZ As Integer) As String
        'Dim nomeZona As String = ""
        Try
            connData.apri()

            par.cmd.CommandText = "SELECT * from zona_aler where COD=" & idZ
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                nomeZona.Value = par.IfNull(lettore0("ZONA"), "")
            End If
            lettore0.Close()

            connData.chiudi()
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaNomeZona - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

        Return nomeZona.Value
    End Function

    Protected Sub btnCaricaDati_Click(sender As Object, e As System.EventArgs) Handles btnCaricaDati.Click
        Try

            connData.apri()
            txtDataPr.Clear()
            par.cmd.CommandText = "SELECT rapporti_utenza.ID AS ID_CONTRATTO,TRIM(COGNOME) AS COGNOME,TRIM(NOME) AS NOME,TRIM(COD_FISCALE) AS COD_FISCALE,rapporti_utenza.COD_CONTRATTO, " _
               & " COD_TIPOLOGIA_CONTR_LOC as tipo_ru, INDIRIZZI.DESCRIZIONE ||', '|| INDIRIZZI.CIVICO ||' '||INDIRIZZI.LOCALITA as indirizzo," _
               & "(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004' " _
               & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='V' THEN '431/98 ART.15 C.2 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
               & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) as tipo_specifico, siscom_mi.getstatocontratto(rapporti_utenza.id) as stato, SISCOM_MI.GETDATA(DATA_dECORRENZA) as decorrenza,SISCOM_MI.GETDATA(DATA_DISDETTA_LOCATARIO) as disdetta, " _
               & "(select  DECODE (NVL (CANONI_EC.ID_AREA_ECONOMICA, 0)," _
               & "   1, 'PROTEZIONE'," _
               & "   2, 'ACCESSO'," _
               & "   3, 'PERMANENZA'," _
               & "   4, 'DECADENZA')" _
               & "   from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=8 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
               & "   ) as ""AREA_AU_2013""," _
               & "     (select SOTTO_aREA" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=8 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
               & "    ) as ""CLASSE_au_2013""," _
               & " (select  DECODE (NVL (CANONI_EC.ID_AREA_ECONOMICA, 0)," _
               & "    1, 'PROTEZIONE'," _
               & "    2, 'ACCESSO'," _
               & "    3, 'PERMANENZA'," _
               & "    4, 'DECADENZA')" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=10 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
               & "    ) as AREA_AU_2015," _
               & "      (select SOTTO_aREA" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=10 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
               & "    ) as ""CLASSE_AU_2015""," _
               & "       (select ISEE" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=10 AND CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza)" _
               & "    ) as ""ISEE_ERP_AU_2015""," _
                & " (select  DECODE (NVL (CANONI_EC.ID_AREA_ECONOMICA, 0)," _
               & "    1, 'PROTEZIONE'," _
               & "    2, 'ACCESSO'," _
               & "    3, 'PERMANENZA'," _
               & "    4, 'DECADENZA')" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=1 and CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza) and substr(inizio_validita_can,1,4)>='2016' " _
               & "    ) as AREA_RECA," _
               & "      (select SOTTO_aREA" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=1 and CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza) and substr(inizio_validita_can,1,4)>='2016'" _
               & "    ) as ""CLASSE_RECA""," _
               & "       (select ISEE" _
               & "    from siscom_mi.canoni_Ec where canoni_Ec.id_contratto=rapporti_utenza.id and tipo_provenienza=1 and CANONI_EC.DATA_CALCOLO =(SELECT MAX (DATA_CALCOLO) FROM siscom_mi.CANONI_EC EC WHERE EC.ID_CONTRATTO = CANONI_EC.ID_CONTRATTO and ec.tipo_provenienza= CANONI_EC.tipo_provenienza) and substr(inizio_validita_can,1,4)>='2016'" _
               & "    ) as ""ISEE_ERP_RECA""" _
               & " from siscom_mi.rapporti_utenza,siscom_mi.soggetti_contrattuali,siscom_mi.anagrafica,siscom_mi.UNITA_CONTRATTUALE,siscom_mi.unita_immobiliari,siscom_mi.indirizzi where anagrafica.id=soggetti_Contrattuali.id_anagrafica AND UNITA_CONTRATTUALE.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID (+) =UNITA_CONTRATTUALE.ID_UNITA " _
               & " and cod_tipologia_occupante='INTE' AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO and soggetti_contrattuali.id_contratto=rapporti_utenza.id and RAPPORTI_UTENZA.id=" & idSel.Value
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                txtAreaAU2013.Text = par.IfNull(lettore0("AREA_AU_2013"), "")
                txtAreaAU2015.Text = par.IfNull(lettore0("AREA_AU_2015"), "")
                txtAreaReca.Text = par.IfNull(lettore0("AREA_RECA"), "")
                txtClasseAU2013.Text = par.IfNull(lettore0("CLASSE_AU_2013"), "")
                txtClasseAU2015.Text = par.IfNull(lettore0("CLASSE_AU_2015"), "")
                txtClasseReca.Text = par.IfNull(lettore0("CLASSE_RECA"), "")
                txtDataDecorr.Text = par.IfNull(lettore0("DECORRENZA"), "")
                txtDataDisdetta.Text = par.IfNull(lettore0("DISDETTA"), "")
                txtIndirizzo.Text = par.IfNull(lettore0("INDIRIZZO"), "")

                If par.IfNull(lettore0("ISEE_ERP_AU_2015"), -1) = -1 Then
                    txtISEE.Text = ""
                Else
                    txtISEE.Text = Format(CDec(par.IfNull(lettore0("ISEE_ERP_AU_2015"), 0)), "#,##0.00")
                End If
                If par.IfNull(lettore0("ISEE_ERP_RECA"), -1) = -1 Then
                    txtISEEreca.Text = ""
                Else
                    txtISEEreca.Text = Format(CDec(par.IfNull(lettore0("ISEE_ERP_RECA"), 0)), "#,##0.00")
                End If
                txtSaldo2015.Text = Format(CalcolaSaldo2015(par.IfNull(lettore0("ID_CONTRATTO"), 0)), "#,##0.00")
                txtSaldoAttuale.Text = Format(CalcolaSaldoScaduto(par.IfNull(lettore0("ID_CONTRATTO"), 0)), "#,##0.00")
                txtSaldoCond.Text = Format(SaldoCondominio(par.IfNull(lettore0("ID_CONTRATTO"), 0)), "#,##0.00")
                txtTotSaldo.Text = Format(CDec(par.IfEmpty(txtSaldo2015.Text, 0)) + CDec(par.IfEmpty(txtSaldoCond.Text, 0)), "#,##0.00")
                txtStatoRU.Text = par.IfNull(lettore0("STATO"), "")
                txtTipoRU.Text = par.IfNull(lettore0("TIPO_RU"), "")
                txtTipoSpecifico.Text = par.IfNull(lettore0("tipo_specifico"), "")
                idContr.Value = par.IfNull(lettore0("ID_CONTRATTO"), 0)
                If ControllaRat(idContr.Value) = True Then
                    txtRateizzazione.Text = "Sì"
                Else
                    txtRateizzazione.Text = "No"
                End If

                If txtStatoRU.Text = "CHIUSO" Then
                    btnStampa.Visible = False
                    txtDataPr.Enabled = False
                Else
                    btnStampa.Visible = True
                    txtDataPr.Enabled = True
                End If


                par.cmd.CommandText = "select * from SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA where id_contratto=" & par.IfNull(lettore0("ID_CONTRATTO"), 0)
                Dim myReader00 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader00.Read Then
                    If par.IfNull(myReader00("DATA_PRESENTAZIONE"), "") <> "" Then
                        txtDataPr.SelectedDate = par.FormattaData(Mid(par.IfNull(myReader00("DATA_PRESENTAZIONE"), ""), 1, 8))
                        btnCanc.Visible = True
                    End If
                Else
                    btnCanc.Visible = False
                End If
                myReader00.Close()

            End If
            lettore0.Close()

            connData.chiudi()
            Dim script As String = "function f(){$find(""" + RadWindowInfoRU.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnCaricaDati_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Function ControllaRat(ByVal IdContratto As Long) As Boolean
        Dim rat As Boolean = False
        par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni where id_contratto=" & IdContratto
            Dim myReader00 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader00.Read Then
                rat = True
            End If
            myReader00.Close()
        Return rat
    End Function

    Public Function SaldoCondominio(ByVal IdContratto As Long) As Decimal
        Dim imp As Decimal = 0
        par.cmd.CommandText = "select * from siscom_mi.saldo_condominio_contratti where id_contratto=" & IdContratto
        Dim myReader00 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader00.Read Then
            imp = par.IfNull(myReader00("importo"), 0)
        End If
        myReader00.Close()
        Return imp
    End Function

    Public Function CalcolaSaldo2015(ByVal IdContratto As Long) As Decimal

        Dim TotSaldo As Double = 0

        par.cmd.CommandText = "SELECT " _
            & "SUM(NVL(IMPORTO_TOTALE,0)- NVL(IMPORTO_RIC_B,0)) EMESSO_BOLLETTA," _
            & "SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS EMESSO_CONTABILE, " _
            & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS INCASSATO, " _
            & "(SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0))-(SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)))) AS saldo " _
            & "FROM SISCOM_MI.BOL_BOLLETTE " _
            & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND BOL_BOLLETTE.DATA_EMISSIONE <='20160907' AND BOL_BOLLETTE.DATA_SCADENZA<='20160907' " _
            & "AND ID_CONTRATTO = " & IdContratto
        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader3.Read Then
            TotSaldo = par.IfNull(myReader3("SALDO"), 0)
        End If
        myReader3.Close()

        par.cmd.CommandText = "select sum(importo_pagato) as pagatoDopo from siscom_mi.bol_bollette_voci_pagamenti where data_pagamento>'20160907' and id_gruppo_voce_bolletta<>5 and id_incasso_extramav in (select id from siscom_mi.incassi_extramav where id_contratto=" & IdContratto & ")"
        myReader3 = par.cmd.ExecuteReader()
        If myReader3.Read Then
            TotSaldo = TotSaldo + par.IfNull(myReader3("pagatoDopo"), 0)
        End If
        myReader3.Close()


        Return TotSaldo

    End Function


    Public Function CalcolaSaldoScaduto(ByVal IdContratto As Long) As Decimal

        Dim TotSaldo As Double = 0

        par.cmd.CommandText = "SELECT " _
            & "SUM(NVL(IMPORTO_TOTALE,0)- NVL(IMPORTO_RIC_B,0)) EMESSO_BOLLETTA," _
            & "SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0)) AS EMESSO_CONTABILE, " _
            & "SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)) AS INCASSATO, " _
            & "(SUM(IMPORTO_TOTALE -  NVL(IMPORTO_RIC_B,0) - NVL(QUOTA_SIND_B,0))-(SUM(NVL(IMPORTO_PAGATO,0) - NVL(QUOTA_SIND_PAGATA_B,0)- NVL(IMPORTO_RIC_PAGATO_B,0)))) AS saldo " _
            & "FROM SISCOM_MI.BOL_BOLLETTE " _
            & "WHERE (BOL_BOLLETTE.FL_ANNULLATA=0 OR (BOL_BOLLETTE.FL_ANNULLATA<>0 AND DATA_PAGAMENTO IS NOT NULL )) AND BOL_BOLLETTE.DATA_SCADENZA<='" & Format(Now, "yyyyMMdd") & "' " _
            & "AND ID_CONTRATTO = " & IdContratto
        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader3.Read Then
            TotSaldo = par.IfNull(myReader3("SALDO"), 0)
        End If
        myReader3.Close()

        Return TotSaldo

    End Function


    Protected Sub btnStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Try
            If IsNothing(txtDataPr.SelectedDate) Then
                RadWindowManager1.RadAlert("Inserire la data di presentazione!", 330, 180, "Attenzione!", "function(sender, args){openWindow(sender, args, 'RadWindowInfoRU');}", Nothing)
            Else
                connData.apri()
                par.cmd.CommandText = "select * from SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA where id_contratto=" & idContr.Value
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA set data_presentazione='" & par.AggiustaData(txtDataPr.SelectedDate) & Format(Now, "HHmmss") & "' WHERE ID=" & dt.Rows(0).Item("ID")
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA (ID, DATA_PRESENTAZIONE, ID_CONTRATTO) VALUES (SISCOM_MI.SEQ_DOMANDE_FONDO_SOLIDARIETA.nextval,'" & par.AggiustaData(txtDataPr.SelectedDate) & Format(Now, "HHmmss") & "'," & idContr.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If

                connData.chiudi()
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "print", "PrintDoc();", True)
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
        End Try
    End Sub

    Protected Sub btnCanc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCanc.Click
        Try

            connData.apri()
            par.cmd.CommandText = "select * from SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA where id_contratto=" & idContr.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.DOMANDE_FONDO_SOLIDARIETA set data_presentazione='' WHERE ID=" & dt.Rows(0).Item("ID")
                par.cmd.ExecuteNonQuery()
            End If

            connData.chiudi()
            txtDataPr.Clear()
            Dim script As String = "function f(){$find(""" + RadWindowInfoRU.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
        End Try
    End Sub

    Protected Sub RadButtonEsciAggregazioneImpianto_Click(sender As Object, e As System.EventArgs) Handles RadButtonEsciAggregazioneImpianto.Click

    End Sub
End Class
