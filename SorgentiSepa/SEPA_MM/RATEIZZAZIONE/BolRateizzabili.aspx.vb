
Partial Class RATEIZZAZIONE_BolRateizzabili
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then
            ratMor.Value = Request.QueryString("RATM")
            dataPres.Value = Request.QueryString("DPR")
            codRU.Value = Request.QueryString("CODRU")
            If Not String.IsNullOrEmpty(Request.QueryString("IDRAT")) Then
                idRateizz.Value = Request.QueryString("IDRAT")
            End If
            If ratMor.Value = "1" Then
                rdbBoll.Visible = False
                ricavaValoriRateizz()
                btnIndietro.Visible = True
            End If
            CaricaTabella("T")

        End If
    End Sub

    Private Sub ricavaValoriRateizz()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim fascia As Integer = 0
            Dim reddito As Decimal = 0
            Dim saldo As Decimal = 0
            Dim maxNumRate As Integer = 0

            par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni where id=" & idRateizz.Value
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If MyReader.Read Then
                fascia = par.IfNull(MyReader("ID_AREA_ECONOMICA"), 1)
                reddito = Math.Round(par.IfNull(MyReader("REDDITO_COMPLESSIVO"), 0) * 2 / 3, 2)
                saldo = par.IfNull(MyReader("SALDO"), 0)
            End If
            MyReader.Close()

            par.cmd.CommandText = "select max_num_rate from siscom_mi.PARAM_RATEIZZ_MOROSITA where ID_AREA_ECONOMICA = " & fascia & " and importo_da<=" & par.VirgoleInPunti(saldo) & " and importo_a>=" & par.VirgoleInPunti(saldo) & " "
            maxNumRate = par.IfEmpty(par.cmd.ExecuteScalar, 0)

            If maxNumRate > 0 And reddito > 0 Then
                importoMaxRateizzabile.Value = (Math.Round(reddito / 12 / 8)) * maxNumRate
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "ricavaValoriRateizz - " & ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Function ControllaBollRateizzabili(ByVal importoMaxRateizz As Decimal, ByVal dtElencoBollette As Data.DataTable) As Data.DataTable
        Try
            Dim totBolletta As Decimal = 0

            Dim dtIdOK As New Data.DataTable
            Dim rowIdOK As System.Data.DataRow

            dtIdOK.Columns.Add("id")

            Dim dtIdNO As New Data.DataTable
            Dim rowIdNO As System.Data.DataRow

            dtIdNO.Columns.Add("id")


            Dim dataView As New Data.DataView(dtElencoBollette)
            dataView.Sort = "DATA_EMISSIONE ASC"
            dtElencoBollette = dataView.ToTable


            If dtElencoBollette.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtElencoBollette.Rows
                    totBolletta = totBolletta + (row.Item("importo_totale") - row.Item("importo_pagato"))

                    If importoMaxRateizz >= totBolletta Then
                        rowIdOK = dtIdOK.NewRow()
                        rowIdOK.Item("id") = row.Item("id")
                        dtIdOK.Rows.Add(rowIdOK)
                    Else
                        rowIdNO = dtIdNO.NewRow()
                        rowIdNO.Item("id") = row.Item("id")
                        dtIdNO.Rows.Add(rowIdNO)
                    End If
                Next
            End If

            Return dtIdOK

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "ControllaBollRateizzabili - " & ex.Message

        End Try
    End Function


    Private Sub CaricaTabella(ByVal sollecito As String)

        Try

            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ANAGRAFICA.cod_fiscale,ANAGRAFICA.partita_iva FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.RAPPORTI_UTENZA " _
                                & "WHERE RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.id_contratto AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica " _
                                & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND RAPPORTI_UTENZA.ID =" & Request.QueryString("IDCONTRATTO")
            Dim lettorea As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettorea.Read Then
                If CStr(par.IfNull(lettorea("cod_fiscale"), "")) = "" And CStr(par.IfNull(lettorea("partita_iva"), "")) = "" Then
                    Response.Write("<script>alert('Codice Fiscale e Partita Iva dell\'intestatario vuoti!Impossibile procedere con la rateizzazione!');self.close();</script>")
                    lettorea.Close()

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
            End If
            lettorea.Close()


            Dim condizioneSollecito As String = ""
            Dim strQuery As String = ""
            If sollecito = "S" Then
                'VISUALIZZAZIONE BOLLETTE VECCHIE + SOLLECITI
                'condizioneSollecito = " AND (FL_SOLLECITO=1 OR BOL_BOLLETTE.ID<0) "
                condizioneSollecito = " AND DATA_SCADENZA <= TO_CHAR(SYSDATE,'YYYYMMDD')"
            Else
                'VISUALIZZAZIONE TUTTE BOLLETTE
                condizioneSollecito = ""

            End If

            If ratMor.Value = "1" Then
                strQuery = "SELECT bol_bollette.ID, bol_bollette.note, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.riferimento_da,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_da, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.riferimento_a,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_a, " _
                                & "NVL(bol_bollette.importo_totale,0)/*-NVL(IMPORTO_RIC_B, 0) */- NVL(bol_bollette.QUOTA_SIND_B,0) AS importo_totale,  " _
                  & " NVL (  ( IMPORTO_PAGATO - nvl((SELECT SUM (imp_pagato) FROM siscom_mi.bol_bollette_voci WHERE bol_bollette_voci.id_bolletta = bol_bollette.id   " _
                              & " AND (/*id_voce IN (150, 151, 677, 676, 7, 126, 182) OR*/ id_voce IN (SELECT id FROM siscom_mi.t_voci_bolletta WHERE gruppo = 5))),0)), 0) AS importo_pagato, " _
                    & "TO_CHAR(TO_DATE(bol_bollette.data_scadenza,'yyyymmdd'),'dd/mm/yyyy')AS data_scadenza,  " _
                    & "TIPO_BOLLETTE.ACRONIMO,bol_bollette.Data_emissione " _
                                & " FROM siscom_mi.rapporti_utenza,siscom_mi.bol_bollette,SISCOM_MI.TIPO_BOLLETTE WHERE BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) " _
                                & " and (bol_bollette.DATA_SCADENZA<= '" & par.AggiustaData(Request.QueryString("DATAS")) & "' or BOL_BOLLETTE.id_tipo=26 or BOL_BOLLETTE.id_tipo=27)  " _
                                & " and BOL_BOLLETTE.id_tipo NOT IN (22,25)  " _
                                & " and id_Bolletta_storno is null " _
                                & " and nvl(importo_ruolo,0)=0 " _
                                & " and nvl(importo_ingiunzione,0)=0  " _
                                & " and rapporti_utenza.id=" & Request.QueryString("IDCONTRATTO") & "  " _
                                & " And BOL_BOLLETTE.ID_CONTRATTO = RAPPORTI_UTENZA.ID  " _
                                & " And (FL_ANNULLATA = 0 Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL)) " _
                                & " And bol_bollette.id_bolletta_ric Is null " _
                                & " And bol_bollette.id_rateizzazione Is null " _
                                & "AND (NVL(bol_bollette.importo_totale,0) - NVL(bol_bollette.QUOTA_SIND_B,0)) > " _
                                                  & " NVL (  ( IMPORTO_PAGATO - nvl((SELECT SUM (imp_pagato) FROM siscom_mi.bol_bollette_voci WHERE bol_bollette_voci.id_bolletta = bol_bollette.id   " _
                              & " AND (/*id_voce IN (150, 151, 677, 676, 7, 126, 182) OR*/ id_voce IN (SELECT id FROM siscom_mi.t_voci_bolletta WHERE gruppo = 5))),0)), 0)  " _
                                 & "ORDER BY bol_bollette.data_emissione DESC,BOL_BOLLETTE.ANNO " _
                                & "DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.ID DESC"

            Else
                strQuery = "SELECT bol_bollette.ID, bol_bollette.note, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.riferimento_da,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_da, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.riferimento_a,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_a, " _
                                & "trim(TO_CHAR((NVL(bol_bollette.importo_totale,0) - NVL(bol_bollette.QUOTA_SIND_B,0)),'9G999G999G999G990D99')) AS importo_totale,  " _
                                & "trim(TO_CHAR((NVL(bol_bollette.importo_pagato,0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0)),'9G999G999G999G990D99')) AS importo_pagato , " _
                                & "TO_CHAR(TO_DATE(bol_bollette.data_scadenza,'yyyymmdd'),'dd/mm/yyyy')AS data_scadenza,  " _
                                & "TIPO_BOLLETTE.ACRONIMO " _
                                & "FROM SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette " _
                                & "WHERE BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) " _
                                & "AND bol_bollette.id_contratto=" & Request.QueryString("IDCONTRATTO") & "  " _
                                & "AND bol_bollette.fl_annullata = 0 " _
                                & "AND bol_bollette.id_bolletta_ric IS NULL " _
                                & "AND BOL_BOLLETTE.ID_TIPO <> 5 and id_tipo not in (9,10) " _
                                & "AND IMPORTO_RUOLO = 0 and nvl(importo_ingiunzione,0)=0 " _
                                & condizioneSollecito _
                                & "AND nvl(bol_bollette.importo_totale,0) >nvl(bol_bollette.importo_pagato,0) " _
                                & "AND bol_bollette.id_rateizzazione is null " _
                                & "ORDER BY bol_bollette.data_emissione DESC,BOL_BOLLETTE.ANNO " _
                                & "DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.ID DESC"
            End If
            par.cmd.CommandText = strQuery
            ' & "AND bol_bollette.id_rateizzazione is null " _
            'and id_tipo not in (9,10) significa esclutedere deposito cauzionale e attivazione contratto
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            DgvBolRateizzabili.DataSource = dt
            DgvBolRateizzabili.DataBind()


            AddFunction()


            Dim dtBollDaIncludere As New Data.DataTable
            If ratMor.Value = "1" Then
                If importoMaxRateizzabile.Value > 0 Then
                    dtBollDaIncludere = ControllaBollRateizzabili(CDec(importoMaxRateizzabile.Value), dt)
                    CheckSelezionaTutto(dtBollDaIncludere)
                Else
                    Response.Write("<script>alert('Impossibile rateizzare!');</script>")
                    btnProcedi.Visible = False
                End If
            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()




        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CaricaTabella - " & ex.Message
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try

            Dim IdBollette As String = ""
            Dim primo As Boolean = True

            Dim IdBollette2 As String = ""
            Dim primo2 As Boolean = True
            For Each di As DataGridItem In DgvBolRateizzabili.Items
                If DirectCast(di.Cells(8).FindControl("ChkSelected"), CheckBox).Checked = True Then
                    If primo = True Then
                        IdBollette = di.Cells(0).Text
                        primo = False
                    Else
                        IdBollette = IdBollette & "," & di.Cells(0).Text
                    End If
                End If

                If DirectCast(di.Cells(8).FindControl("ChkSelected"), CheckBox).Checked = False Then
                    If primo2 = True Then
                        IdBollette2 = di.Cells(0).Text
                        primo2 = False
                    Else
                        IdBollette2 = IdBollette2 & "," & di.Cells(0).Text
                    End If
                End If
            Next

            If Not String.IsNullOrEmpty(IdBollette2) Then
                Session.Add("IDBOLLESCLUSE", IdBollette2)
            End If

            If Not String.IsNullOrEmpty(IdBollette) Then
                Session.Add("IDBOLLETTE", IdBollette)

                If ratMor.Value = "1" Then
                    Response.Redirect("ProspettoRateiz2.aspx?IDRAT=" & Request.QueryString("IDRAT") & "")

                Else
                    Response.Redirect("ProspettoRateiz.aspx")

                End If
            Else

                Response.Write("<script>alert('Selezionare almeno una bolletta da rateizzare!');</script>")
            End If
        Catch ex As Exception

            Session.Add("ERRORE", "Ricalcolo Canone:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub AddFunction()
        For Each di As DataGridItem In DgvBolRateizzabili.Items
            DirectCast(di.Cells(8).FindControl("ChkSelected"), CheckBox).Attributes.Add("onclick", "javascript:Somma(" & par.VirgoleInPunti(di.Cells(5).Text.Replace("&nbsp;", "0").Replace(".", "")) & "," & par.VirgoleInPunti(di.Cells(7).Text.Replace("&nbsp;", "0").Replace(".", "")) & ",this);") '"SommaUI(" & di.Cells(1) & ");")
        Next
    End Sub

    Private Function Somma(ByVal S As Boolean) As Integer
        Somma = 1
    End Function

    Protected Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        CheckSelezionaTutto()

    End Sub

    Private Sub CheckSelezionaTutto(Optional ByVal dtBolldaincl As Data.DataTable = Nothing)
        Try

            If Selezionati.Value = 0 Then
                SumSelected.Value = 0

                For Each di As DataGridItem In DgvBolRateizzabili.Items
                    If Not IsNothing(dtBolldaincl) Then
                        If dtBolldaincl.Rows.Count > 0 Then
                            For Each rr As Data.DataRow In dtBolldaincl.Rows
                                If rr.Item("id") = di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "id")).Text Then
                                    DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = True
                                    Me.SumSelected.Value = SumSelected.Value + (Math.Round(CDec(di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "IMPORTO_TOTALE")).Text) - CDec(di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "IMPORTO_PAGATO")).Text), 2))
                                End If
                            Next
                            'If DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = False Then
                            'DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Enabled = False
                            'End If
                        Else
                            DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = True
                            Me.SumSelected.Value = SumSelected.Value + (Math.Round(CDec(di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "IMPORTO_TOTALE")).Text) - CDec(di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "IMPORTO_PAGATO")).Text), 2))
                        End If
                    Else
                        'If DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Enabled = True Then
                        DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = True
                        Me.SumSelected.Value = SumSelected.Value + (Math.Round(CDec(di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "IMPORTO_TOTALE")).Text) - CDec(di.Cells(TrovaIndiceColonna(DgvBolRateizzabili, "IMPORTO_PAGATO")).Text), 2))
                        'End If
                    End If
                Next
                If ratMor.Value = "1" Then
                    If CDec(SumSelected.Value) > CDec(importoMaxRateizzabile.Value) Then
                        Response.Write("<script>alert('Importo totale selezionato maggiore della somma rateizzabile!');</script>")
                        For Each di As DataGridItem In DgvBolRateizzabili.Items
                            DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = False
                            SumSelected.Value = 0
                        Next
                    Else
                        Selezionati.Value = 1
                    End If
                End If
            Else
                For Each di As DataGridItem In DgvBolRateizzabili.Items
                    DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = False
                    SumSelected.Value = 0
                Next
                Selezionati.Value = 0

            End If
            Me.txtSomma.Text = "IMPORTO TOTALE DELLA RATEIZZAZIONE PARI A €. " & Format(CDec(SumSelected.Value), "##,##0.00") & " AL NETTO DELLE QUOTE SINDACALI"

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "btnSelectAll_Click - " & ex.Message

        End Try
    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                    TrovaIndiceColonna = Indice
                    Exit For
                End If
                Indice = Indice + 1
            Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "TrovaIndiceColonna - " & ex.Message
        End Try

        Return TrovaIndiceColonna

    End Function

    Protected Sub rdbGestione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbBoll.SelectedIndexChanged
        CaricaTabella(rdbBoll.SelectedValue)
        If ratMor.Value <> "1" Then
            SumSelected.Value = 0
            Me.txtSomma.Text = "IMPORTO TOTALE DELLA RATEIZZAZIONE PARI A €. 0,00"
        End If
    End Sub

    Private Sub btnIndietro_Click(sender As Object, e As EventArgs) Handles btnIndietro.Click
        Response.Redirect("RateizzMorositaPregressa.aspx?ID=" & Request.QueryString("IDRAT") & "&DPR=" & par.FormattaData(dataPres.Value) & "&CODRU=" & codRU.Value & "&DATAS=" & Request.QueryString("DATAS"), False)
    End Sub
End Class
