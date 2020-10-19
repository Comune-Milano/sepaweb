
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_DettRiepRitLegge
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        solaLettura.Value = Request.QueryString("SL")
        If Not IsPostBack Then
            CaricaDati()
        End If

    End Sub
    Private Sub CaricaDati()
        Try
            apriConnessione()
            par.cmd.CommandText = "SELECT PERC_IVA AS IVA,PF_VOCI.DESCRIZIONE AS DESC_VOCE_PF,TO_CHAR(TO_DATE(PRENOTAZIONI.data_prenotazione,'YYYYmmdd'),'DD/MM/YYYY') AS data_prenotazione, " _
                & "TO_CHAR(TO_DATE(PRENOTAZIONI.data_consuntivazione,'YYYYmmdd'),'DD/MM/YYYY') AS data_consuntivazione, " _
                & "trim(TO_CHAR((rit_legge_ivata),'9G999G999G999G999G990D99')) AS RITENUTA_LEGGE,(odl||'/'||manutenzioni.anno) as odl,'' as importo_lordo_modificato,'' as importo_lordo_modificato_diff,PRENOTAZIONI.ID AS ID, " _
                & "(SELECT ID FROM SISCOM_MI.PF_VOCI A WHERE A.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_STATO=5) AND A.CODICE=(SELECT CODICE FROM SISCOM_MI.PF_VOCI B WHERE B.ID=PF_VOCI.ID )) AS ID_VOCE_PF_NEW " _
                & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI, SISCOM_MI.MANUTENZIONI " _
                & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND MANUTENZIONI.id_prenotazione_pagamento(+) = PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_STATO<>-3 " _
                & "/*AND MANUTENZIONI.STATO<>5*/ " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO = " & Request.QueryString("IDPAG") _
                & "AND PRENOTAZIONI.ID NOT IN (SELECT ID_PRENOTAZIONE_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE) " _
                & "ORDER BY MANUTENZIONI.ANNO,MANUTENZIONI.ODL,DATA_PRENOTAZIONE,DATA_CONSUNTIVAZIONE ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            par.cmd.CommandText = "SELECT DISTINCT progr,PAGAMENTI.anno FROM siscom_mi.pagamenti where id = " & Request.QueryString("IDPAG")
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                Me.lblTitolo.Text = "Dettaglio Ritenuta di Legge del Pagamento ANNO:" & par.IfNull(reader("anno"), "") & " ADP:" & par.IfNull(reader("progr"), "")
            Else
                Me.lblTitolo.Text = "Dettaglio Ritenuta di Legge"
            End If



           

            dgvRitLegge.DataSource = dt
            dgvRitLegge.DataBind()

            GestioneIva()

            Dim somma As Decimal = 0
            For Each row As DataGridItem In dgvRitLegge.Items

                'somma = somma + CDec(par.IfNull(row.Item("RITENUTA_LEGGE"), 0).ToString.Replace(".", ""))

                If IsNumeric(row.Cells(TrovaIndiceColonna(dgvRitLegge, "importo_lordo_modificato")).Text) Then
                    somma += CDec(row.Cells(TrovaIndiceColonna(dgvRitLegge, "importo_lordo_modificato")).Text)
                Else
                    somma += CDec(row.Cells(TrovaIndiceColonna(dgvRitLegge, "ritenuta_legge")).Text)
                End If

                'If IsNumeric(row.Cells(TrovaIndiceColonna(dgvRitLegge, "ritenuta_legge")).Text) Then
                '    somma += CDec(row.Cells(TrovaIndiceColonna(dgvRitLegge, "ritenuta_legge")).Text)
                'End If

            Next
            Me.txtTotale.Text = Format(somma, "##,##0.00")

            GestioneLotto()
            chiudiConnessione()
            HiddenFieldModifica.Value = "0"

            If solaLettura.Value = "1" Then
                ImgSalva.Visible = False
            Else
                ImgSalva.Visible = True
            End If

        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try
    End Sub

    Protected Sub DropDownListAliquota_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim numeroRiga As Integer = CType(sender, DropDownList).Attributes("numeroRiga")
        Dim nuovaIvaSelezionata As Integer = CType(sender, DropDownList).SelectedValue
        If nuovaIvaSelezionata <> -1 Then
            Dim vecchiaIvaSelezionata As Integer = CInt(dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IVA")).Text)
            Dim vecchioImportoLordo As Decimal = CDec(dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "RITENUTA_LEGGE")).Text)
            dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO")).Text = Format(CDec(Math.Round(vecchioImportoLordo * (100 + nuovaIvaSelezionata) / (100 + vecchiaIvaSelezionata), 2)), "#,##0.00")
            dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO_DIFF")).Text = Format(CDec(Math.Round(vecchioImportoLordo * (100 + nuovaIvaSelezionata) / (100 + vecchiaIvaSelezionata), 2) - vecchioImportoLordo), "#,##0.00")
            If IsNumeric(txtTotale.Text) Then
                txtTotale.Text = Format(CDec(txtTotale.Text) + CDec(dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO_DIFF")).Text), "#,##0.00")
            End If
        Else
            txtTotale.Text = Format(CDec(txtTotale.Text) + CDec(dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "RITENUTA_LEGGE")).Text) - CDec(dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO")).Text), "#,##0.00")
            dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO")).Text = ""
            dgvRitLegge.Items(numeroRiga).Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO_DIFF")).Text = ""
        End If
        HiddenFieldModifica.Value = "1"
    End Sub

    Private Sub GestioneIva()

        Dim numeroRiga As Integer = 0
        For Each riga As DataGridItem In dgvRitLegge.Items
            Dim ivaOld As Integer = CInt(riga.Cells(4).Text)
            par.caricaComboBox("SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 AND VALORE>" & ivaOld, CType(riga.FindControl("DropDownListAliquota"), DropDownList), "VALORE", "VALORE", True, "-1", " ")

            par.cmd.CommandText = "SELECT ID_PRENOTAZIONE_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE WHERE ID_PRENOTAZIONE=" & CInt(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "ID")).Text)
            Dim lettoreRit As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim idNew As Integer = 0
            If lettoreRit.Read Then
                idNew = par.IfNull(lettoreRit(0), 0)
            End If
            lettoreRit.Close()

            If idNew <> 0 Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PRENOTAZIONI WHERE ID=" & CInt(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "ID")).Text)
                Dim LettRitLegge As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim Importo As Decimal = 0
                If LettRitLegge.Read Then
                    Importo = par.IfNull(LettRitLegge("RIT_LEGGE_IVATA"), 0)
                End If
                LettRitLegge.Close()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PRENOTAZIONI WHERE ID=" & idNew
                LettRitLegge = par.cmd.ExecuteReader
                Dim valoreIva As Integer = 0
                Dim valoreImporto As Decimal = 0
                If LettRitLegge.Read Then
                    valoreIva = par.IfNull(LettRitLegge("PERC_IVA"), 0)
                    valoreImporto = par.IfNull(LettRitLegge("RIT_LEGGE_IVATA"), 0)
                End If
                LettRitLegge.Close()

                CType(riga.FindControl("DropDownListAliquota"), DropDownList).SelectedValue = valoreIva
                riga.Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO")).Text = Format(valoreImporto + Importo, "#,##0.00")
                riga.Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO_diff")).Text = Format(valoreImporto, "#,##0.00")
            End If



            CType(riga.FindControl("DropDownListAliquota"), DropDownList).Attributes.Add("numeroRiga", numeroRiga)
            If CType(riga.FindControl("DropDownListAliquota"), DropDownList).Items.Count > 1 Then
                CType(riga.FindControl("DropDownListAliquota"), DropDownList).Enabled = True
            Else
                CType(riga.FindControl("DropDownListAliquota"), DropDownList).Enabled = False
            End If
            numeroRiga += 1

            If solaLettura.Value = "1" Then
                CType(riga.FindControl("DropDownListAliquota"), DropDownList).Enabled = False
            End If
        Next

    End Sub

    Protected Sub ImgSalva_Click(sender As Object, e As System.EventArgs) Handles ImgSalva.Click

        Try
            apriConnessione()
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans

            For Each riga As DataGridItem In dgvRitLegge.Items

                If CType(riga.FindControl("DropDownListAliquota"), DropDownList).Enabled = True Then

                    If CType(riga.FindControl("DropDownListAliquota"), DropDownList).SelectedValue <> -1 Then

                        Dim IvaNuova As Integer = CType(riga.FindControl("DropDownListAliquota"), DropDownList).SelectedValue
                        Dim importoModificato As Decimal = CDec(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "IMPORTO_LORDO_MODIFICATO_DIFF")).Text)

                        'controllo che la ritenuta sia stata modificata
                        Dim id As Integer = CInt(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "ID")).Text)
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE WHERE ID_PRENOTAZIONE=" & id
                        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim idPrenotazioneRit As Integer = 0
                        If Lettore.Read Then
                            idPrenotazioneRit = par.IfNull(Lettore("ID_PRENOTAZIONE_RIT_LEGGE"), 0)
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI " _
                                & " SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(importoModificato) & ", " _
                                & " IMPORTO_APPROVATO = " & par.VirgoleInPunti(importoModificato) & "," _
                                & " RIT_LEGGE_IVATA = " & par.VirgoleInPunti(importoModificato) & "," _
                                & " PERC_IVA = " & IvaNuova _
                                & " WHERE ID=" & idPrenotazioneRit
                            par.cmd.ExecuteNonQuery()
                        Else
                            'bisogna inserire una nuova prenotazione

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PRENOTAZIONI WHERE ID=" & id
                            Dim LettorePrenotazioni As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If LettorePrenotazioni.Read Then
                                Dim ID_FORNITORE As String = par.IfNull(LettorePrenotazioni("ID_FORNITORE"), "NULL")
                                Dim ID_APPALTO As String = par.IfNull(LettorePrenotazioni("ID_APPALTO"), "NULL")

                                Dim ID_VOCE_PF As String = CInt(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "ID_VOCE_PF_NEW")).Text)
                                'Dim ID_VOCE_PF_IMPORTO As String = CType(riga.FindControl("DropDownListLotto"), DropDownList).SelectedValue
                                Dim ID_VOCE_PF_IMPORTO As String = par.IfNull(LettorePrenotazioni("ID_VOCE_PF_IMPORTO"), "NULL")

                                Dim ID_STATO As String = par.IfNull(LettorePrenotazioni("ID_STATO"), "NULL")
                                Dim ID_PAGAMENTO As String = par.IfNull(LettorePrenotazioni("ID_PAGAMENTO"), "NULL")
                                Dim TIPO_PAGAMENTO As String = par.IfNull(LettorePrenotazioni("TIPO_PAGAMENTO"), "NULL")
                                Dim DESCRIZIONE As String = par.IfNull(LettorePrenotazioni("DESCRIZIONE"), "")


                                Dim DATA_PRENOTAZIONE As String = Format(Now, "yyyyMMdd")

                                Dim IMPORTO_PRENOTATO As Decimal = importoModificato 'par.IfNull(LettorePrenotazioni("IMPORTO_PRENOTATO"), "NULL")
                                Dim IMPORTO_APPROVATO As Decimal = importoModificato 'par.IfNull(LettorePrenotazioni("IMPORTO_APPROVATO"), "NULL")


                                Dim PROGR_FORNITORE As String = par.IfNull(LettorePrenotazioni("PROGR_FORNITORE"), "NULL")
                                Dim ANNO As String = par.IfNull(LettorePrenotazioni("ANNO"), "NULL")

                                Dim DATA_SCADENZA As String = par.IfNull(LettorePrenotazioni("DATA_SCADENZA"), "")
                                Dim DATA_STAMPA As String = par.IfNull(LettorePrenotazioni("DATA_STAMPA"), "")

                                Dim ID_STRUTTURA As String = par.IfNull(LettorePrenotazioni("ID_STRUTTURA"), "NULL")

                                Dim RIT_LEGGE_IVATA As String = importoModificato 'par.IfNull(LettorePrenotazioni("RIT_LEGGE_IVATA"), "NULL")

                                Dim PERC_IVA As Integer = IvaNuova 'par.IfNull(LettorePrenotazioni("PERC_IVA"), "NULL")

                                Dim ID_PAGAMENTO_RIT_LEGGE As String = par.IfNull(LettorePrenotazioni("ID_PAGAMENTO_RIT_LEGGE"), "NULL")
                                Dim DATA_PRENOTAZIONE_TMP As String = par.IfNull(LettorePrenotazioni("DATA_PRENOTAZIONE_TMP"), "")

                                Dim DATA_CONSUNTIVAZIONE As String = Format(Now, "yyyyMMdd")
                                Dim DATA_CERTIFICAZIONE As String = Format(Now, "yyyyMMdd")


                                Dim DATA_CERT_RIT_LEGGE As String = par.IfNull(LettorePrenotazioni("DATA_CERT_RIT_LEGGE"), "")

                                Dim IMPORTO_LIQUIDATO As String = "NULL"

                                Dim DATA_ANNULLO As String = par.IfNull(LettorePrenotazioni("DATA_ANNULLO"), "")

                                Dim IMPORTO_RIT_LIQUIDATO As String = par.IfNull(LettorePrenotazioni("IMPORTO_RIT_LIQUIDATO"), "NULL")

                                par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL "
                                Dim lettoreSeq As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                Dim idNew As Integer = 0
                                If lettoreSeq.Read Then
                                    idNew = par.IfNull(lettoreSeq(0), 0)
                                End If
                                lettoreSeq.Close()

                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.PRENOTAZIONI ( " _
                                    & " ID, ID_FORNITORE, ID_APPALTO,  " _
                                    & " ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO,  " _
                                    & " ID_PAGAMENTO, TIPO_PAGAMENTO, DESCRIZIONE,  " _
                                    & " DATA_PRENOTAZIONE, IMPORTO_PRENOTATO, IMPORTO_APPROVATO,  " _
                                    & " PROGR_FORNITORE, ANNO, DATA_SCADENZA,  " _
                                    & " DATA_STAMPA, ID_STRUTTURA, RIT_LEGGE_IVATA,  " _
                                    & " PERC_IVA, ID_PAGAMENTO_RIT_LEGGE, DATA_PRENOTAZIONE_TMP,  " _
                                    & " DATA_CONSUNTIVAZIONE, DATA_CERTIFICAZIONE, DATA_CERT_RIT_LEGGE,  " _
                                    & " IMPORTO_LIQUIDATO, DATA_ANNULLO, IMPORTO_RIT_LIQUIDATO)  " _
                                    & " VALUES ( " & idNew & ", " _
                                    & ID_FORNITORE & ", " _
                                    & ID_APPALTO & ", " _
                                    & ID_VOCE_PF & ", " _
                                    & ID_VOCE_PF_IMPORTO & ", " _
                                    & ID_STATO & ", " _
                                    & ID_PAGAMENTO & ", " _
                                    & TIPO_PAGAMENTO & ", " _
                                    & "'" & Replace(DESCRIZIONE, "'", "''") & "', " _
                                    & "'" & DATA_PRENOTAZIONE & "', " _
                                    & par.VirgoleInPunti(IMPORTO_PRENOTATO) & ", " _
                                    & par.VirgoleInPunti(IMPORTO_APPROVATO) & ", " _
                                    & PROGR_FORNITORE & ", " _
                                    & ANNO & ", " _
                                    & "'" & DATA_SCADENZA & "', " _
                                    & "'" & DATA_STAMPA & "', " _
                                    & ID_STRUTTURA & ", " _
                                    & par.VirgoleInPunti(RIT_LEGGE_IVATA) & ", " _
                                    & PERC_IVA & ", " _
                                    & ID_PAGAMENTO_RIT_LEGGE & ", " _
                                    & "'" & DATA_PRENOTAZIONE_TMP & "'," _
                                    & "'" & DATA_CONSUNTIVAZIONE & "'," _
                                    & "'" & DATA_CERTIFICAZIONE & "'," _
                                    & "'" & DATA_CERT_RIT_LEGGE & "'," _
                                    & par.VirgoleInPunti(IMPORTO_LIQUIDATO) & ", " _
                                    & "'" & DATA_ANNULLO & "', " _
                                    & par.VirgoleInPunti(IMPORTO_RIT_LIQUIDATO) & ")"

                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE(ID_PRENOTAZIONE,ID_PRENOTAZIONE_RIT_LEGGE) " _
                                    & " VALUES(" _
                                    & id & "," & idNew & ")"

                                par.cmd.ExecuteNonQuery()

                            End If
                            LettorePrenotazioni.Close()

                        End If
                        Lettore.Close()
                    Else
                        'ELIMINO TUTTE LE PRENOTAZIONi rit legge create precedentemente
                        Dim id As Integer = CInt(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "ID")).Text)
                        par.cmd.CommandText = "SELECT ID_PRENOTAZIONE_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE WHERE ID_PRENOTAZIONE=" & id
                        Dim lettRitenuta As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        Dim idRit As Integer = 0
                        If lettRitenuta.Read Then
                            idRit = par.IfNull(lettRitenuta(0), 0)
                            If idRit <> 0 Then
                                par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI WHERE ID=" & idRit
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI_PRE_RIT_LEGGE WHERE ID_PRENOTAZIONE=" & id & " AND ID_PRENOTAZIONE_RIT_LEGGE= " & idRit
                                par.cmd.ExecuteNonQuery()
                            End If
                        End If
                        lettRitenuta.Close()
                    End If

                End If
            Next

            par.myTrans.Commit()
            chiudiConnessione()

            'Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
            RadWindowManager1.RadAlert("Operazione eseguita correttamente!", 300, 150, "Attenzione", "CloseAndRefresh", Nothing)

        Catch ex As Exception
            par.myTrans.Rollback()
            chiudiConnessione()
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try
        CaricaDati()
    End Sub

    Function TrovaIndiceColonna(ByVal dgv As DataGrid, ByVal nameCol As String) As Integer
        TrovaIndiceColonna = -1
        Dim Indice As Integer = 0
        Try
            For Each c As DataGridColumn In dgv.Columns
                If TypeOf c Is System.Web.UI.WebControls.BoundColumn Then
                    If String.Equals(nameCol, DirectCast(c, System.Web.UI.WebControls.BoundColumn).DataField, StringComparison.InvariantCultureIgnoreCase) Then
                        TrovaIndiceColonna = Indice
                        Exit For
                    End If
                End If
                Indice = Indice + 1
            Next
        Catch ex As Exception
            Return -1
        End Try
        Return TrovaIndiceColonna
    End Function


    Protected Sub apriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand
        End If
    End Sub

    Protected Sub chiudiConnessione()
        If Not IsNothing(par.myTrans) Then
            par.myTrans.Dispose()
        End If
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.EventArgs) Handles ImageButtonEsci.Click
        If HiddenFieldEsci.Value = "1" Then
            Response.Write("<script>self.close();</script>")
        End If
    End Sub

    Private Sub GestioneLotto()
        Dim numeroRiga As Integer = 0
        For Each riga As DataGridItem In dgvRitLegge.Items

            Dim idPrenotazione = CInt(riga.Cells(TrovaIndiceColonna(dgvRitLegge, "ID")).Text)

            par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_STATO=5"
            Dim LettoreStato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim idPfnew As Integer = 0
            If LettoreStato.Read Then
                idPfnew = par.IfNull(LettoreStato(0), 0)
            End If
            LettoreStato.Close()

            par.cmd.CommandText = "SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.PF_VOCI,SISCOM_MI.PRENOTAZIONI " _
                & " WHERE PF_VOCI.ID_PIANO_FINANZIARIO=PF_MAIN.ID " _
                & " AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF AND PRENOTAZIONI.ID=" & idPrenotazione
            Dim LettoreStatoOld As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim idPfold As Integer = 0
            If LettoreStatoOld.Read Then
                idPfold = par.IfNull(LettoreStatoOld(0), 0)
            End If
            LettoreStatoOld.Close()

            Dim idLotto As Integer = 0
            Dim descrizioneLotto As String = ""
            Dim idStruttura As Integer = 0
            par.cmd.CommandText = "SELECT LOTTI.ID,LOTTI.DESCRIZIONE,PRENOTAZIONI.ID_STRUTTURA FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO,siscom_mi.lotti " _
               & " WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID " _
               & " AND LOTTI.ID = PF_VOCI_IMPORTO.ID_LOTTO " _
               & " AND PRENOTAZIONI.ID=" & idPrenotazione

            Dim lettoreLotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreLotto.Read Then
                idLotto = par.IfNull(lettoreLotto("ID"), 0)
                descrizioneLotto = par.IfNull(lettoreLotto("DESCRIZIONE"), "")
                idStruttura = par.IfNull(lettoreLotto("ID_STRUTTURA"), 0)
            End If
            lettoreLotto.Close()

            If idPfold <> idPfnew Then
                par.cmd.CommandText = "SELECT LOTTI.ID,LOTTI.DESCRIZIONE FROM SISCOM_MI.LOTTI WHERE ID_OLD=" & idLotto
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.HasRows Then
                    If Lett.Read Then
                        Dim DescrizioneNewLotto As String = par.IfNull(Lett("DESCRIZIONE"), "")
                        Dim idLottoNew As Integer = par.IfNull(Lett("ID"), 0)
                        CType(riga.FindControl("DropDownListLotto"), DropDownList).Attributes.Add("numeroRiga", numeroRiga)
                        CType(riga.FindControl("DropDownListLotto"), DropDownList).Items.Clear()
                        CType(riga.FindControl("DropDownListLotto"), DropDownList).Items.Add(New ListItem(DescrizioneNewLotto, idLottoNew))
                        CType(riga.FindControl("DropDownListLotto"), DropDownList).Enabled = False

                    End If
                Else
                    par.cmd.CommandText = "SELECT LOTTI.ID,LOTTI.DESCRIZIONE FROM SISCOM_MI.LOTTI " _
                        & " WHERE ID_ESERCIZIO_FINANZIARIO=(SELECT ID_eSERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPfnew & ") AND TIPO<>'X' " _
                        & " AND ID_FILIALE=" & idStruttura _
                        & " ORDER BY DESCRIZIONE DESC "
                    Dim Da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    Da.Fill(dt)
                    Da.Dispose()
                    CType(riga.FindControl("DropDownListLotto"), DropDownList).Attributes.Add("numeroRiga", numeroRiga)
                    CType(riga.FindControl("DropDownListLotto"), DropDownList).Items.Clear()
                    CType(riga.FindControl("DropDownListLotto"), DropDownList).DataSource = dt
                    CType(riga.FindControl("DropDownListLotto"), DropDownList).DataTextField = "DESCRIZIONE"
                    CType(riga.FindControl("DropDownListLotto"), DropDownList).DataValueField = "ID"
                    CType(riga.FindControl("DropDownListLotto"), DropDownList).DataBind()
                    If CType(riga.FindControl("DropDownListLotto"), DropDownList).Items.Count > 1 Then
                        CType(riga.FindControl("DropDownListLotto"), DropDownList).Enabled = True
                    Else
                        CType(riga.FindControl("DropDownListLotto"), DropDownList).Enabled = False
                    End If
                End If
                Lett.Close()
            Else
                CType(riga.FindControl("DropDownListLotto"), DropDownList).Attributes.Add("numeroRiga", numeroRiga)
                CType(riga.FindControl("DropDownListLotto"), DropDownList).Items.Clear()
                CType(riga.FindControl("DropDownListLotto"), DropDownList).Items.Add(New ListItem(descrizioneLotto, idLotto))
                CType(riga.FindControl("DropDownListLotto"), DropDownList).Enabled = False
            End If
            numeroRiga += 1

            If solaLettura.Value = "1" Then
                CType(riga.FindControl("DropDownListLotto"), DropDownList).Enabled = False
            End If

        Next
    End Sub

End Class
