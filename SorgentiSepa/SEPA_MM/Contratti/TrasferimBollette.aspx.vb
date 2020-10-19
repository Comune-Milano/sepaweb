
Partial Class Contratti_TrasferimBollette
    Inherits System.Web.UI.Page
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

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        Response.Flush()
        If Not IsPostBack Then

            idContratto.Value = Request.QueryString("IDCONT")
            codContrAbus.Value = Request.QueryString("CODRUA")
            RiempiTabella("N")
            tipoSelezione.Value = "N"

        End If

    End Sub

    Private Sub RiempiTabella(ByVal pagata As String)

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where cod_contratto='" & codContrAbus.Value & "'"
            Dim lettoreOA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreOA.Read Then
                idRUA.Value = par.IfNull(lettoreOA("ID"), 0)
            End If
            lettoreOA.Close()

            lblIntest.Text = "Selezionare le bollette da trasferire nella posizione contrattuale num. <a href='javascript:void(0)' onclick=" & Chr(34) & "today = new Date();window.open('Contratto.aspx?LT=1&ID=" & idRUA.Value & "&COD=" & codContrAbus.Value & "','Contratto'+ today.getMinutes() + today.getSeconds(),'height=780,top=0,left=0,width=1160,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">" & codContrAbus.Value & " </a>"

            Dim condizionePagate As String = ""
            If pagata = "P" Then
                condizionePagate = " AND nvl(bol_bollette.importo_pagato,0) > 0 "
            ElseIf pagata = "N" Then
                condizionePagate = " AND nvl(bol_bollette.importo_totale,0) >nvl(bol_bollette.importo_pagato,0)"
            End If

            par.cmd.CommandText = "SELECT bol_bollette.ID, bol_bollette.note, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.riferimento_da,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_da, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.riferimento_a,'yyyymmdd'),'dd/mm/yyyy') AS riferimento_a, " _
                                & "TO_CHAR(TO_DATE(bol_bollette.data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS data_emissione, " _
                                & "trim(TO_CHAR((NVL(bol_bollette.importo_totale,0) - NVL(bol_bollette.QUOTA_SIND_B,0)),'9G999G999G999G990D99')) AS importo_totale,  " _
                                & "trim(TO_CHAR((NVL(bol_bollette.importo_pagato,0)- NVL(bol_bollette.QUOTA_SIND_PAGATA_B,0)),'9G999G999G999G990D99')) AS importo_pagato , " _
                                & "TO_CHAR(TO_DATE(bol_bollette.data_scadenza,'yyyymmdd'),'dd/mm/yyyy')AS data_scadenza,  " _
                                & "TIPO_BOLLETTE.ACRONIMO " _
                                & "FROM SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette " _
                                & "WHERE BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) " _
                                & "AND bol_bollette.id_contratto=" & idContratto.Value & "  " _
                                & "AND bol_bollette.fl_annullata = 0 AND NVL(ID_BOLLETTA_STORNO,0)=0 " _
                                & "AND bol_bollette.id_bolletta_ric IS NULL and id_tipo not in (4,5) " _
                                & "AND BOL_BOLLETTE.ID_TIPO <> 5 AND BOL_BOLLETTE.ID_TIPO<>22 " _
                                & condizionePagate _
                                & "AND bol_bollette.id_rateizzazione is null " _
                                & "ORDER BY BOL_BOLLETTE.data_emissione DESC,RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC,DATA_SCADENZA DESC"
            
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da.Fill(dt0)
            DgvBolTrasferim.DataSource = dt0
            DgvBolTrasferim.DataBind()

            
            lblNumBoll.Text = "TOT. BOLLETTE: " & dt0.Rows.Count

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub chkAll_click(sender As Object, e As System.EventArgs)
        Try
            If Selezionati.Value = 0 Then

                For Each di As DataGridItem In DgvBolTrasferim.Items
                    DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = True
                Next
                Selezionati.Value = 1
            Else
                For Each di As DataGridItem In DgvBolTrasferim.Items
                    DirectCast(di.Cells(5).FindControl("ChkSelected"), CheckBox).Checked = False
                Next
                Selezionati.Value = 0
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property dtdati() As Data.DataTable
        Get
            If Not (ViewState("dtdati") Is Nothing) Then
                Return ViewState("dtdati")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtdati") = value
        End Set
    End Property

    Protected Sub rdbBoll_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbBoll.SelectedIndexChanged
        RiempiTabella(rdbBoll.SelectedValue)
        tipoSelezione.Value = rdbBoll.SelectedValue
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim chkSelezione As System.Web.UI.WebControls.CheckBox
            Dim RIGA As Data.DataRow

            dtdati = New Data.DataTable
            dtdati.Columns.Add("id_bolletta")

            For Each oDataGridItem In DgvBolTrasferim.Items
                chkSelezione = oDataGridItem.FindControl("ChkSelected")

                If chkSelezione.Checked Then
                    RIGA = dtdati.NewRow()
                    RIGA.Item("id_bolletta") = oDataGridItem.Cells(0).Text

                    dtdati.Rows.Add(RIGA)
                End If
            Next

            If confermaStorno.Value = "1" Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_CONTRATTI WHERE COD_EVENTO='F04' AND ID_CONTRATTO=" & idRUA.Value
                Dim myReaderQ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderQ.HasRows = False Then
                    Response.Write("<script>alert('Prima di trasferire le bollette è necessario attivare il nuovo contratto!')</script>")
                Else
                    For Each rowDati As Data.DataRow In dtdati.Rows
                        CreaStornoEnuovaBoll(rowDati.Item("ID_BOLLETTA"))
                    Next
                    Response.Write("<script>var opener = window.dialogArguments;window.opener.document.getElementById('imgSalva').click();self.close();</script>")
                End If
                myReaderQ.Close()

                par.myTrans.Commit()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub CreaStornoEnuovaBoll(ByVal idbolletta As Long)
        Try
            Dim pagata As Boolean = False
            Dim dataPagamento As String = ""
            Dim dataValuta As String = ""
            Dim idAnagrafica As Long = 0
            Dim dataEmiss As String = ""
            Dim dataCompetDal As String = ""
            Dim dataCompetAl As String = ""
            Dim dataDecorr As String = ""
            Dim dataScadenza As String = ""
            Dim dtV As New Data.DataTable

            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto.Value & " AND ID=" & idbolletta
            Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da0.Fill(dt0)
            da0.Dispose()
            If dt0.Rows.Count > 0 Then
                If par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(dt0.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                    pagata = True
                    dataPagamento = par.IfNull(dt0.Rows(0).Item("DATA_PAGAMENTO"), "")
                    dataValuta = par.IfNull(dt0.Rows(0).Item("DATA_VALUTA"), "")
                    dataEmiss = par.IfNull(dt0.Rows(0).Item("DATA_EMISSIONE"), "")
                    dataCompetDal = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_DA"), "")
                    dataCompetAl = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_A"), "")
                Else
                    pagata = False
                    dataPagamento = Format(Now, "yyyyMMdd")
                    dataValuta = Format(Now, "yyyyMMdd")
                    dataEmiss = par.IfNull(dt0.Rows(0).Item("DATA_EMISSIONE"), "")
                    dataCompetDal = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_DA"), "")
                    dataCompetAl = par.IfNull(dt0.Rows(0).Item("RIFERIMENTO_A"), "")
                End If
            End If

            par.cmd.CommandText = "SELECT * FROM siscom_mi.rapporti_utenza where id='" & idRUA.Value & "'"
            Dim lettoreOA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreOA.Read Then
                dataDecorr = par.IfNull(lettoreOA("DATA_DECORRENZA"), "")
                dataScadenza = par.IfNull(lettoreOA("DATA_SCADENZA"), "")
            End If
            lettoreOA.Close()

            Dim idAnagr As Long = 0
            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE " _
                & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & idContratto.Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
            Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreDati.Read Then
                idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
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

                importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0)

                If par.IfNull(dt0.Rows(0).Item("IMPORTO_TOTALE"), 0) > par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                    importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0)
                    pagataParz = True
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & idRUA.Value & "," & par.RicavaEsercizioCorrente & "," & dt0.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(importoTot * -1) & "," _
                            & "'" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_VALUTA") & "',4,'N',NULL,'PAGAMENTO RECUPERATO DA RU PRECEDENTE')"
                par.cmd.ExecuteNonQuery()

                Dim idBollGest As Long = 0
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idBollGest = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",713," & par.VirgoleInPunti(importoTot * -1) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F204','IMPORTO PARI A EURO " & Format(importoTot, "##,##0.00") & " TRASFERITO NEL RU NUM. " & codContrAbus.Value & "')"
                par.cmd.ExecuteNonQuery()
            End If

            If pagataParz = True Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idbolletta
                Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                daV.Fill(dtV)
                daV.Dispose()
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & idbolletta
                Dim daV As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                daV.Fill(dtV)
                daV.Dispose()
            End If


            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto.Value & "AND ID=" & idbolletta
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            da1.Dispose()
            For Each row As Data.DataRow In dt1.Rows

                note = "STORNO PER CREAZ. NUOVO RU - NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA")

                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                        & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & par.PulisciStrSql(note) & "'," _
                        & "" & par.IfNull(row.Item("ID_CONTRATTO"), 0) _
                        & " ," & par.RicavaEsercizioCorrente & ", " _
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
            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
            Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderST.Read Then
                ID_BOLLETTA_STORNO = myReaderST(0)
            End If
            myReaderST.Close()

            Dim ID_VOCE_STORNO As Long = 0
            Dim SumImportoVOCI As Decimal = 0
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID= " & idbolletta
            Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBVoci As New Data.DataTable
            daBVoci.Fill(dtBVoci)
            daBVoci.Dispose()
            For Each row As Data.DataRow In dtBVoci.Rows
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                    & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO, COMPETENZA_INIZIO," _
                    & "COMPETENZA_FINE, FL_ACCERTATO ) VALUES ( SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL, " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & ",'STORNO'," _
                    & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "," _
                    & "'" & par.IfNull(row.Item("COMPETENZA_INIZIO"), "") & "', '" & par.IfNull(row.Item("COMPETENZA_FINE"), "") & "'," & par.IfNull(row.Item("FL_ACCERTATO"), 0) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.CURRVAL FROM DUAL"
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

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & idbolletta
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
            par.cmd.ExecuteNonQuery()

            Dim strPagata As String = ""
            If pagata = True Then
                strPagata = "(precedentam. pagata) "
            Else
                strPagata = "(non precedentem. pagata) "
            End If
            If pagataParz = True Then
                pagata = False
                strPagata = "(parzialm. pagata) "
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F203','NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA") & " " & strPagata & " STORNATA PER CREAZ.NUOVO RU')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & idContratto.Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
            End If
            myReader0.Close()

            If pagata = False Then

                par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & idRUA.Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 99, '" & dataDecorr _
                        & "', '" & dataScadenza & "', NULL,NULL,NULL,'IMPORTO TRASFERITO DA PRECEDENTE RU'," _
                        & "" & idRUA.Value _
                        & " ," & par.RicavaEsercizioCorrente & ", " _
                        & dt0.Rows(0).Item("ID_UNITA") _
                        & ", '0', '', " & idAnagrafica _
                        & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                        & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                        & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                        & "', '', '" & dataCompetDal _
                        & "', '" & dataCompetAl & "', " _
                        & "'0', " & par.IfNull(myReaderS("ID_COMPLESSO"), 0) & ", '', NULL, '', " _
                        & Year(Now) & ", '', " & par.IfNull(myReaderS("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD'," & dt0.Rows(0).Item("ID_TIPO") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderS.Close()

                Dim ID_BOLLETTA_NEW As Long = 0
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    ID_BOLLETTA_NEW = myReaderA(0)
                End If
                myReaderA.Close()

                For Each rowV As Data.DataRow In dtV.Rows

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & rowV.Item("ID_VOCE") & "," & par.VirgoleInPunti(par.IfNull(rowV.Item("IMPORTO"), 0)) & ",'" & par.PulisciStrSql(par.IfNull(rowV.Item("NOTE"), "")) & "')"
                    par.cmd.ExecuteNonQuery()

                Next

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idRUA.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F08','PAGAMENTO SPOSTATO DA RU PRECED.')"
                par.cmd.ExecuteNonQuery()

            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
            Exit Sub
        End Try
    End Sub
End Class
