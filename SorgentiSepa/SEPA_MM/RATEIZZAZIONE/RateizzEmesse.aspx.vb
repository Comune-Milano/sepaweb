
Partial Class RATEIZZAZIONE_RateizzEmesse
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            Cerca()
            idContratto.Value = Request.QueryString("idCont")
        End If
        If Session.Item("MOD_ANNULLA_RATEIZZA") <> "1" Then
            btnEliminaR.Visible = False
            Me.lblButton.Visible = False
        End If
    End Sub
    Private Sub Cerca()
        Try
            Dim condition As String = ""
            If Not String.IsNullOrEmpty(Request.QueryString("idCont")) Then
                condition = " AND bol_rateizzazioni.id_contratto = " & Request.QueryString("idCont")
            End If
            par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID AS ID_CONTRATTO,BOL_RATEIZZAZIONI.ID AS id_rateizzazione, cod_contratto,TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') as data_emissione,DECODE(tipo_rateizzazione,0,'NUMERO RATE',1,'IMPORTO RATA') AS TIPO_RAT, " _
                                & "(SELECT (CASE WHEN RAGIONE_SOCIALE IS NULL THEN COGNOME ||' '|| NOME ELSE RAGIONE_SOCIALE END) FROM siscom_mi.ANAGRAFICA,siscom_mi.SOGGETTI_CONTRATTUALI " _
                                & "WHERE ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.id_anagrafica AND SOGGETTI_CONTRATTUALI.id_contratto = RAPPORTI_UTENZA.ID AND COD_TIPOLOGIA_OCCUPANTE = 'INTE') AS INTESTATARIO, " _
                                & "DECODE(bol_rateizzazioni.imp_anticipo,0,'NO',trim(TO_CHAR(imp_anticipo,'9G999G999G999G999G990D99'))) AS anticipo," _
                                & "(SELECT COUNT(ID) FROM siscom_mi.bol_rateizzazioni_dett WHERE id_rateizzazione =bol_rateizzazioni.ID AND fl_annullata = 0 AND num_rata >0 ) AS NUM_RATE," _
                                & "(SELECT COUNT(ID) FROM siscom_mi.bol_rateizzazioni_dett WHERE id_rateizzazione =bol_rateizzazioni.ID AND fl_annullata = 0 AND  id_bolletta IS NOT NULL) AS NUM_MAV_EMESSI," _
                                & "NVL((SELECT COUNT(bol_bollette.ID) FROM siscom_mi.bol_bollette,siscom_mi.bol_rateizzazioni_dett WHERE bol_rateizzazioni_dett.id_bolletta " _
                                & "IS NOT NULL AND bol_rateizzazioni_dett.fl_annullata = 0 AND NVL(importo_pagato,0)<>0 AND bol_bollette.ID = id_bolletta " _
                                & "AND bol_rateizzazioni_dett.ID_rateizzazione = bol_rateizzazioni.ID " _
                                & "GROUP BY bol_rateizzazioni_dett.id_rateizzazione),0)AS mav_pagati,trim(TO_CHAR((IMP_ANTICIPO +IMP_RESIDUO),'9G999G999G999G999G990D99')) AS TOT_RAT, BOL_RATEIZZAZIONI.FL_ANNULLATA " _
                                & "FROM siscom_mi.BOL_RATEIZZAZIONI,siscom_mi.RAPPORTI_UTENZA " _
                                & "WHERE RAPPORTI_UTENZA.ID = BOL_RATEIZZAZIONI.id_contratto " & condition & " and (SELECT COUNT(ID) FROM siscom_mi.bol_rateizzazioni_dett WHERE id_rateizzazione =bol_rateizzazioni.ID AND num_rata >0 )>0 ORDER BY data_emissione ASC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            DataGrid.DataSource = dt
            DataGrid.DataBind()

            For Each di As DataGridItem In DataGrid.Items
                If di.Cells(TrovaIndiceColonna(DataGrid, "FL_ANNULLATA")).Text = 1 Then
                    di.Font.Strikeout = True
                End If
            Next

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub DataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('idRat').value = " & e.Item.Cells(0).Text)

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('idRat').value = " & e.Item.Cells(0).Text)

        End If

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
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return TrovaIndiceColonna

    End Function
    Protected Sub btnEliminaR_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaR.Click
        If ConfermaSalva.Value = 1 Then
            DelRateizzazione()
        End If
        'Response.Write(<script>alert('Funzione non ancora disponibile!')</script>)
    End Sub
    Private Sub DelRateizzazione()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni where id = " & idRat.Value & " and fl_annullata = 0"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count = 1 Then

                'If CreditoGestRateizzPagataParz() = True Then
                If StornaRateizzateNonPagate() = True Then
                    If GestisciRiclassificateInRateizzzazione() = True Then
                        'ANNULLO LA RATEIZZAZIONE ED IL DETTAGLIO DELLA RATEIZZAZIONE

                        par.cmd.CommandText = "UPDATE siscom_mi.BOL_RATEIZZAZIONI SET FL_ANNULLATA = 1 WHERE ID = " & idRat.Value
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "update siscom_mi.bol_rateizzazioni_dett set fl_annullata = 1 where id_rateizzazione = " & idRat.Value
                        par.cmd.ExecuteNonQuery()

                        Response.Write(<script>alert('Operazione eseguita correttamente!')</script>)
                    Else
                        Response.Write(<script>alert('Errori in fase di ripristino delle bollette riclassificate!\OPERAZIONE ANNULLATA!!\Nessun dato è stato modificato')</script>)
                        par.myTrans.Rollback()
                        Exit Sub


                    End If
                Else
                    Response.Write(<script>alert('Errori in fase di annullo delle bollette di rateizzazione!\OPERAZIONE ANNULLATA!!\Nessun dato è stato modificato')</script>)
                    par.myTrans.Rollback()
                    Exit Sub


                End If



                'Else
                '    Response.Write(<script>alert('Errori in fase di gestione del credito derivante dalle bollette di rateizzazione!\OPERAZIONE ANNULLATA!!\Nessun dato è stato modificato')</script>)
                '    par.myTrans.Rollback()
                '    Exit Sub


                'End If



            Else
                Response.Write(<script>alert('La rateizzazione selezionata è già stata annullata!\nImpossibile procedere!')</script>)
                par.myTrans.Rollback()
                Exit Sub


            End If



            par.myTrans.Commit()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Cerca()

        Catch ex As Exception
            par.myTrans.Rollback()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
    Private Function CreditoGestRateizzPagataParz() As Boolean
        CreditoGestRateizzPagataParz = True
        Try
            Dim TotCreditoGestionale As Decimal = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            'cerco bollette di tipo RATEIZZAZIONE pagate parzialmente
            par.cmd.CommandText = "select id,importo_totale,importo_pagato,id_contratto,id_unita,COD_AFFITTUARIO,DATA_PAGAMENTO from siscom_mi.bol_bollette where id in " _
                                & "(select id_bolletta from siscom_mi.bol_rateizzazioni_dett where id_bolletta is not null and id_rateizzazione = " & idRat.Value & ") " _
                                & " and nvl(importo_pagato,0)>0 " 'and (nvl(importo_totale,0)-nvl(importo_pagato,0))>0"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                For Each r As Data.DataRow In dt.Rows
                    TotCreditoGestionale = 0
                    par.cmd.CommandText = "select nvl(imp_pagato,0) as impcredito from siscom_mi.bol_bollette_voci where id_bolletta = " & r.Item("id").ToString & " and id_voce = 678"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        TotCreditoGestionale = par.IfNull(lettore("impcredito"), 0)
                    End If
                    lettore.Close()
                    Dim dataPagamento As String = par.FormattaData(par.IfEmpty(r.Item("DATA_PAGAMENTO").ToString, Format(Now, "yyyyMMdd")))
                    Dim dataInizioCompet As String = ""
                    Dim dataFineCompet As String = ""

                    dataInizioCompet = Right(dataPagamento, 4) & dataPagamento.Substring(3, 2) & "01"
                    dataFineCompet = Right(dataPagamento, 4) & dataPagamento.Substring(3, 2) & DateTime.DaysInMonth(Right(dataPagamento, 4), dataPagamento.Substring(3, 2))


                    ' inserisco il credito maturato a favore dell'inquilino per gli interessi pagati sulla bolletta di rateizzazione che verrà stornata
                    par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                                        & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE ) " _
                                        & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & r.Item("id_contratto").ToString & "," & par.RicavaEsercizioCorrente() & "," _
                                        & r.Item("id_unita").ToString & "," & r.Item("COD_AFFITTUARIO").ToString & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(Math.Round(TotCreditoGestionale, 2) * (-1)) & "," _
                                        & "'" & Format(Now, "yyyyMMdd") & "','" & par.AggiustaData(par.IfEmpty(dataPagamento, Format(Now, "dd/MM/yyyy"))) & "','" & par.AggiustaData(DateAdd(DateInterval.Day, 2, CDate(par.IfEmpty(dataPagamento, Format(Now, "dd/MM/yyyy"))))) & "',4,'N',NULL,'CREDITO MATURATO PER PAGAMENTO ANNULLO RATEIZZAZIONE ')"
                    par.cmd.ExecuteNonQuery()

                    'CreaStorno(r.Item("id_contratto").ToString, r.Item("id").ToString, "ANNULLO RATEIZZAZIONE")
                Next


            End If

        Catch ex As Exception
            CreditoGestRateizzPagataParz = False
        End Try
    End Function
    Private Function StornaRateizzateNonPagate() As Boolean
        StornaRateizzateNonPagate = True
        Try
            'cerco bollette di tipo RATEIZZAZIONE pagate parzialmente
            par.cmd.CommandText = "select id,importo_totale,importo_pagato,id_contratto,id_unita,COD_AFFITTUARIO,DATA_PAGAMENTO from siscom_mi.bol_bollette where id in " _
                                & "(select id_bolletta from siscom_mi.bol_rateizzazioni_dett where id_bolletta is not null and id_rateizzazione = " & idRat.Value & ") " _
                                & " and nvl(importo_pagato,0)=0 and (nvl(importo_totale,0)-nvl(importo_pagato,0))= nvl(importo_totale,0)"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            For Each r As Data.DataRow In dt.Rows
                CreaStorno(r.Item("id_contratto"), r.Item("id"), "ANNULLO RATEIZZAZIONE")
            Next
        Catch ex As Exception
            StornaRateizzateNonPagate = False
        End Try
    End Function
    Private Function GestisciRiclassificateInRateizzzazione() As Boolean
        GestisciRiclassificateInRateizzzazione = True
        Try
            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            ''Leggo le voci delle bollette riclassificate nella rateizzazione
            'par.cmd.CommandText = "select * from siscom_mi.bol_bollette_voci where id_bolletta in (select id from siscom_mi.bol_bollette where id_rateizzazione = " & idRat.Value & ")"
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)

            'For Each r As Data.DataRow In dt.Rows
            '    'controllo che se hanno ricevuto un pagamento devo spostarlo da VOCI_PAGAMENTI2 a VOCI_PAGAMENTI
            '    par.cmd.CommandText = "select * from siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI2 where id_voce_bolletta = " & r.Item("id")

            '    lettore = par.cmd.ExecuteReader

            '    If lettore.HasRows = True Then
            '        par.cmd.CommandText = "INSERT INTO siscom_mi.bol_bollette_voci_pagamenti select * from siscom_mi.bol_bollette_voci_pagamenti2 where id_voce_bolletta =" & r.Item("id")
            '        par.cmd.ExecuteNonQuery()

            '        par.cmd.CommandText = "delete from siscom_mi.bol_bollette_voci_pagamenti2 where id_voce_bolletta = " & r.Item("id")
            '        par.cmd.ExecuteNonQuery()
            '    End If
            '    lettore.Close()

            'Next
            par.cmd.CommandText = "update siscom_mi.bol_bollette_voci set importo_riclassificato = importo_riclassificato_pagato  where id_bolletta in (select id from siscom_mi.bol_bollette where id_rateizzazione = " & idRat.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "update siscom_mi.bol_bollette set id_rateizzazione = null where id_rateizzazione = " & idRat.Value
            par.cmd.ExecuteNonQuery()
        Catch ex As Exception
            GestisciRiclassificateInRateizzzazione = False
        End Try

    End Function

    'DA MARIA TERESA 12/12/2013
    Private Sub CreaStorno(ByVal idContratto As String, ByVal idBollettaDaStornare As String, ByVal motivoStorno As String)
        Try


            Dim pagata As Boolean = False
            Dim dataPagamento As String = ""
            Dim dataValuta As String = ""
            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto & " AND ID=" & idBollettaDaStornare
            Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da0.Fill(dt0)
            da0.Dispose()
            If dt0.Rows.Count > 0 Then
                'AND (NVL(IMPORTO_PAGATO,0)>0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0))
                If par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(dt0.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                    pagata = True
                    dataPagamento = par.IfNull(dt0.Rows(0).Item("DATA_PAGAMENTO"), "")
                    dataValuta = par.IfNull(dt0.Rows(0).Item("DATA_VALUTA"), "")
                Else
                    pagata = False
                    dataPagamento = Format(Now, "yyyyMMdd")
                    dataValuta = Format(Now, "yyyyMMdd")
                End If
            End If

            'RICAVO ID ANAGRAFICA
            Dim idAnagr As Long = 0
            Dim intestatario As String = ""
            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA,(ANAGRAFICA.COGNOME ||' '|| ANAGRAFICA.NOME) AS INTEST  FROM siscom_mi.RAPPORTI_UTENZA,siscom_mi.SOGGETTI_CONTRATTUALI,siscom_mi.ANAGRAFICA WHERE " _
                & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND RAPPORTI_UTENZA.ID=" & idContratto & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
            Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreDati.Read Then
                idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
                intestatario = par.IfNull(lettoreDati("INTEST"), "")
            End If
            lettoreDati.Close()

            Dim dataAttuale As String = ""
            Dim dataInizioCompet As String = ""
            Dim dataFineCompet As String = ""
            dataAttuale = Format(Now, "dd/MM/yyyy")
            If dataAttuale <> "" Then
                dataInizioCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & "01"
                dataFineCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & DateTime.DaysInMonth(Right(dataAttuale, 4), dataAttuale.Substring(3, 2))
            End If

            'STORNA BOLLETTA SELEZIONATA
            Dim note As String = ""
            Dim pagataParz As Boolean = False
            If pagata = True Then
                Dim importoTot As Decimal = 0

                'importoTot = dt0.Rows(0).Item("IMPORTO_TOTALE") - par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0)

                importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0)

                If par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0) > par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                    importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0)
                    pagataParz = True
                End If

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & idContratto & "," & par.RicavaEsercizioCorrente() & "," & dt0.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(importoTot * -1) & "," _
                            & "'" & Format(Now, "yyyyMMdd") & "','" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_VALUTA") & "',4,'N',NULL,'ECCEDENZA PER PAGAMENTO BOLLETTA STORNATA " & dt0.Rows(0).Item("NUM_BOLLETTA") & "')"
                par.cmd.ExecuteNonQuery()

                Dim idBollGest As Long = 0
                par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idBollGest = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",712," & par.VirgoleInPunti(importoTot * -1) & ")"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F204','IMPORTO PARI A EURO " & Format(importoTot, "##,##0.00") & "')"
                par.cmd.ExecuteNonQuery()

            End If

            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto & "AND ID=" & idBollettaDaStornare
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            da1.Dispose()
            For Each row As Data.DataRow In dt1.Rows

                note = "STORNO PER " & motivoStorno.ToUpper & " NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA")


                par.cmd.CommandText = "Insert into siscom_mi.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(siscom_mi.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                        & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & par.PulisciStrSql(note) & "'," _
                        & "" & par.IfNull(row.Item("ID_CONTRATTO"), 0) _
                        & " ," & par.RicavaEsercizioCorrente() & ", " _
                        & par.IfNull(row.Item("ID_UNITA"), 0) _
                        & ", '0', '" & par.PulisciStrSql(par.IfNull(row.Item("PAGABILE_PRESSO"), "")) & "', " & par.IfNull(row.Item("COD_AFFITTUARIO"), 0) & "" _
                        & ", '" & par.PulisciStrSql(par.IfNull(row.Item("INTESTATARIO"), "")) & "', " _
                        & "'" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) _
                        & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) _
                        & "', '" & par.PulisciStrSql(par.IfNull(row.Item("PRESSO"), "")) & "', '" & par.IfNull(row.Item("RIFERIMENTO_DA"), "") _
                        & "', '" & par.IfNull(row.Item("RIFERIMENTO_A"), "") & "', " _
                        & "'1', " & par.IfNull(row.Item("ID_COMPLESSO"), 0) & ", '', '', " _
                        & Year(Now) & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD',22)"
                par.cmd.ExecuteNonQuery()
            Next

            Dim ID_BOLLETTA_STORNO As Long = 0
            par.cmd.CommandText = "select siscom_mi.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
            Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderST.Read Then
                ID_BOLLETTA_STORNO = myReaderST(0)
            End If
            myReaderST.Close()

            Dim ID_VOCE_STORNO As Long = 0
            Dim SumImportoVOCI As Decimal = 0
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM siscom_mi.BOL_BOLLETTE,siscom_mi.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID= " & idBollettaDaStornare
            Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBVoci As New Data.DataTable
            daBVoci.Fill(dtBVoci)
            daBVoci.Dispose()
            For Each row As Data.DataRow In dtBVoci.Rows
                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                    & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO" _
                    & " ) VALUES ( siscom_mi.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL, " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & ",'STORNO'," _
                    & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "" _
                    & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select siscom_mi.SEQ_BOL_BOLLETTE_VOCI.CURRVAL FROM DUAL"
                Dim myReaderIDV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDV.Read Then
                    ID_VOCE_STORNO = myReaderIDV(0)
                End If
                myReaderIDV.Close()

                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & " WHERE ID=" & ID_VOCE_STORNO
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) & " WHERE ID=" & row.Item("ID")
                par.cmd.ExecuteNonQuery()

                SumImportoVOCI = SumImportoVOCI + par.IfNull(row.Item("IMPORTO"), 0)
            Next

            'par.cmd.CommandText = "UPDATE bol_bollette set ID_BOLLETTA_STORNATA=" & V3.Value & ",IMPORTO_PAGATO=" & par.VirgoleInPunti(SumImportoVOCI * -1) & " WHERE ID=" & ID_BOLLETTA_STORNO
            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & idBollettaDaStornare
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
            par.cmd.ExecuteNonQuery()

            Dim strPagata As String = ""
            If pagata = True Then
                strPagata = "(precedentem. pagata) "
            Else
                strPagata = "(non precedentem. pagata) "
            End If
            If pagataParz = True Then
                strPagata = "(parzialm. pagata) "
            End If
            par.cmd.CommandText = "INSERT INTO siscom_mi.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F203','NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA") & " " & strPagata & "STORNATA PER " & motivoStorno & "')"
            par.cmd.ExecuteNonQuery()





        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
