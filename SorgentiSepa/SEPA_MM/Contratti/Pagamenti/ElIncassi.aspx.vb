
Partial Class Contratti_Pagamenti_ElIncassi
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            idContratto.Value = Request.QueryString("IDCONT")
            vIdAnagrafica.Value = Request.QueryString("IDANA")
            vIdConnessione.Value = Request.QueryString("IDCONN")
            CaricaIncassi()
            If Request.QueryString("SL") = 1 Then
                Me.tblBtnElInca.Style.Value += " display:none; "
                SoloLett.Value = 1
            Else
                SoloLett.Value = 0
            End If
        Else
            If flReload.Value = 1 Then
                CaricaIncassi()
                flReload.Value = 0
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "opener.validNavigation=true;opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();", True)

                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "x", "validNavigation=true;", True)


            End If
        End If
    End Sub

    Private Sub CaricaIncassi()
        Try
            Dim errore As String = ""
            connData.apri(False)
            par.cmd.CommandText = "SELECT   INCASSI_EXTRAMAV.ID, id_operatore, Getdataora (data_ora) AS data_ora, " _
                                & "         OPERATORI.operatore, " _
                                & "         TIPO_PAG_PARZ.descrizione|| (CASE WHEN NVL (TIPO_PAG_PARZ.ID, 0) = 5 THEN ' num. ' || numero_assegno ELSE '' END) AS tipo_pagamento, " _
                                & "         motivo_pagamento , data_ora as ordDataOra,Getdata (data_pagamento) AS data_pagamento,Getdata(riferimento_da) AS riferimento_da,Getdata(riferimento_a) AS riferimento_a, " _
                                & "		 TRIM (TO_CHAR(importo,'9G999G999G990D99')) AS importo,TRIM (TO_CHAR(importo_incassato,'9G999G999G990D99')) AS importo_incassato,TRIM (TO_CHAR(importo_eccedenza,'9G999G999G990D99')) AS importo_eccedenza,FL_ANNULLATA,(CASE WHEN ID_TIPO_PAG IN (11,12) THEN 0 ELSE 1 END) AS EDITDELABLE " _
                                & "    FROM SISCOM_MI.INCASSI_EXTRAMAV, OPERATORI, SISCOM_MI.TIPO_PAG_PARZ " _
                                & "    WHERE INCASSI_EXTRAMAV.id_operatore = OPERATORI.ID " _
                                & "    AND TIPO_PAG_PARZ.ID = INCASSI_EXTRAMAV.id_tipo_pag " _
                                & "    AND INCASSI_EXTRAMAV.id_contratto =" & idContratto.Value _
                                & "ORDER BY ordDataOra DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            dgvIncassi.DataSource = dt
            dgvIncassi.DataBind()


            If dt.Rows.Count > 0 Then
                CtrlIncassoSuRicla(dt.Rows(0).Item("ID"), idBollettaRic.Value, errore)
            End If
            Me.lblTitolo.Text = "Elenco Incassi Manuali Registrati"
            connData.chiudi(False)

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - CaricaIncassi - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try

    End Sub

    Private Function CtrlIncassoSuRicla(ByVal IdIncasso As Integer, ByRef idBollettaRic As Integer, ByRef errore As String) As Boolean
        CtrlIncassoSuRicla = False
        errore = "CtrlIncassoSuRicla"

        par.cmd.CommandText = "select id_Bolletta_Ric from siscom_mi.bol_bollette where id in (select id_bolletta from siscom_mi.bol_bollette_voci where id in (select id_voce_bolletta from siscom_mi.bol_bollette_voci_pagamenti where id_incasso_Extramav=" & IdIncasso & "))"
        idBollettaRic = par.IfNull(par.cmd.ExecuteScalar, 0)
        idBollettaRic = 0

        errore = ""
        CtrlIncassoSuRicla = True
    End Function
    Protected Sub dgvIncassi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvIncassi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('txtmia').value='Hai selezionato il pagamento avvenuto il " & e.Item.Cells(1).Text.Replace("'", "\'").Replace("&nbsp;", "") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text & "';document.getElementById('flAnnullata').value='" & e.Item.Cells(par.IndDGC(dgvIncassi, "FL_ANNULLATA")).Text.Replace("&nbsp;", "") & "';document.getElementById('flEditDelable').value='" & e.Item.Cells(par.IndDGC(dgvIncassi, "EDITDELABLE")).Text.Replace("&nbsp;", "") & "';")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnEdit').click();")

            If e.Item.Cells(par.IndDGC(dgvIncassi, "FL_ANNULLATA")).Text = 1 Then
                e.Item.BackColor = Drawing.Color.Red
            End If

        End If

    End Sub
    Private Function isModdableImporti() As Boolean
        isModdableImporti = True
        par.cmd.CommandText = "select ID_TIPO_PAG from SISCOM_MI.incassi_extramav where id = " & idSelected.Value
        Dim idTipoGest As String
        idTipoGest = par.IfNull(par.cmd.ExecuteScalar, 0)
        'SE TIPO INCASSO DA ANNULLARE DERIVA DA UNA GESTIONALE ALLORA VERIFICO SE OPERATORE AUTORIZZATO A SALTARE CONTROLLO DATA ORA OPERAZIONE NELL'ANNO IN CORSO
        If par.IfNull(idTipoGest, "") = "11" Or par.IfNull(idTipoGest, "") = "12" Then
            par.cmd.CommandText = "select FL_ANNULLA_INCA_GEST from operatori where id = " & Session.Item("id_operatore")
            Dim OpAutorizzato As Integer = 0
            OpAutorizzato = par.cmd.ExecuteScalar
            If OpAutorizzato = 1 Then
                isModdableImporti = True
                Exit Function

            Else
                par.cmd.CommandText = "select id_tipo_pag from siscom_mi.incassi_extramav where id = " & idSelected.Value
                Dim idtipoPag As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If idtipoPag <> 8 Then

                    Dim dataEvento As String = ""
                    par.cmd.CommandText = "select data_ora from siscom_mi.incassi_extramav where id = " & idSelected.Value
                    dataEvento = par.cmd.ExecuteScalar
                    dataEvento = dataEvento.Substring(0, 4)
                    If dataEvento <> Format(Now, "yyyy") Then
                        isModdableImporti = False
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Non è possibile annullare incassi registrati in anni precedenti!');", True)
                        Exit Function
                    End If
                End If

            End If

        Else
            par.cmd.CommandText = "select FL_ANNULLA_INCA_GEST from operatori where id = " & Session.Item("id_operatore")
            Dim OpAutorizzato As Integer = 0
            OpAutorizzato = par.cmd.ExecuteScalar
            If OpAutorizzato = 1 Then
                isModdableImporti = True
                Exit Function

            Else
                par.cmd.CommandText = "select id_tipo_pag from siscom_mi.incassi_extramav where id = " & idSelected.Value
                Dim idtipoPag As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If idtipoPag <> 8 Then

                    Dim dataEvento As String = ""
                    par.cmd.CommandText = "select data_ora from siscom_mi.incassi_extramav where id = " & idSelected.Value
                    dataEvento = par.cmd.ExecuteScalar
                    dataEvento = dataEvento.Substring(0, 4)
                    If dataEvento <> Format(Now, "yyyy") Then
                        isModdableImporti = False
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Non è possibile annullare incassi registrati in anni precedenti!');", True)
                        Exit Function
                    End If
                End If
            End If


        End If


    End Function

    Protected Sub btnDelete_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        If confAnnullo.Value = "1" Then
            connData.apri(True)
            Try
                If idSelected.Value <> "0" Then
                    If flAnnullata.Value = 0 Then
                        If dataOpSupDataBlocco() = True Then
                            If isModdableImporti() = True Then '<punto g) sepa ote 25
                                If IsGestionale() = False Then
                                    If isIncModificabile() = True Then
                                        If EliminaPagamento(True) = True Then
                                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('IL PAGAMENTO SELEZIONATO E\' STATO ANNULLATO!');opener.validNavigation=true;opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();if (opener.opener.document.getElementById('AGGBOLL')){opener.opener.document.getElementById('AGGBOLL').value=1;opener.opener.document.getElementById('form1').submit();}", True)
                                        Else
                                            connData.chiudi(False)
                                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Anomalia in fase di annullo!Operazione interrotta!');", True)
                                        End If
                                    End If
                                Else
                                    'ANNULLO INCASSO EFFETTUATO DA UNA GESTIONALE
                                    'Segnalazione n.
                                    If AnnullaIncaGestionale(idSelected.Value) = False Then
                                        connData.chiudi(False)
                                    Else
                                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('IL PAGAMENTO SELEZIONATO E\' STATO ANNULLATO!');opener.validNavigation=true;opener.document.getElementById('flReload').value=1;opener.document.getElementById('form1').submit();if (opener.opener.document.getElementById('AGGBOLL')){opener.opener.document.getElementById('AGGBOLL').value=1;opener.opener.document.getElementById('form1').submit();}", True)


                                    End If
                                End If
                            End If
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('IL PAGAMENTO E\' GIA\' STATO ANNULLATO!\nImpossibile procedere!');", True)
                    End If
                    connData.chiudi(True)

                    idSelected.Value = 0
                    CaricaIncassi()
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Nessun pagamento selezionato.\nImpossibile procedere!');", True)
                End If

            Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: Proprieta - btnDelete_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
        End If
    End Sub








    Private Function AnnullaIncaGestionale(ByVal idIncasso As Integer) As Boolean
        AnnullaIncaGestionale = True
        par.cmd.CommandText = "select FL_ANNULLA_INCA_GEST from operatori where id = " & Session.Item("id_operatore")
        Dim OpAutorizzato As Integer = 0
        OpAutorizzato = par.cmd.ExecuteScalar
        Dim ImpIncasso As Decimal = 0
        Dim ImpRipartito As Decimal = 0
        Dim TotBBVP As Decimal
        Dim idBollGest As Integer = 0
        Dim totGestInca As Decimal = 0

        Dim idContrGest As Integer = 0
        Dim idUiGest As Integer = 0
        Dim idAnagGest As Integer = 0
        Dim rifDaGest As String = ""
        Dim rifAGest As String = ""
        Dim dataPagGest As String = ""
        Dim dataValGest As String = ""

        Dim idNewGest As Double = 0

        'note evento
        Dim noteEvento As String = ""
        If OpAutorizzato = 1 Then

            par.cmd.CommandText = "select id,round(nvl(importo,0),2) as importo,id_bolletta_gest,round(nvl(importo_incassato,0),2) as importo_incassato " _
                & " from siscom_mi.incassi_extramav where id =  " & idIncasso

            Dim readInca As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If readInca.Read Then
                ImpIncasso = par.IfNull(readInca("importo"), 0)
                ImpRipartito = par.IfNull(readInca("importo_incassato"), 0)
                idBollGest = par.IfNull(readInca("id_bolletta_gest"), 0)
            End If
            readInca.Close()

            'info gestionale di partenza
            par.cmd.CommandText = "select id_contratto,id_unita,id_anagrafica,riferimento_da,riferimento_a,data_pagamento,data_valuta from siscom_mi.bol_bollette_gest  where id = " & idBollGest
            readInca = par.cmd.ExecuteReader
            If readInca.Read Then
                idContrGest = par.IfNull(readInca("id_contratto"), 0)
                idUiGest = par.IfNull(readInca("id_unita"), 0)
                idAnagGest = par.IfNull(readInca("id_anagrafica"), 0)
                rifDaGest = par.IfNull(readInca("riferimento_da"), "")
                rifAGest = par.IfNull(readInca("riferimento_a"), "")
                dataPagGest = par.IfNull(readInca("data_pagamento"), 0)
                dataValGest = par.IfNull(readInca("data_valuta"), 0)

            End If
            readInca.Close()

            'VERIFICA DEL REALE RIPARTITO
            par.cmd.CommandText = "select round(sum(nvl(importo_pagato,0)),2) as tot_bbvp from siscom_mi.bol_bollette_voci_pagamenti bbvp,siscom_mi.bol_bollette_voci bv,siscom_mi.t_voci_bolletta tv " _
                    & " where bbvp.id_voce_bolletta = bv.id and bv.id_voce = tv.id and tv.fl_no_report = 0 and bbvp.id_incasso_extramav = " & idIncasso & ""
            TotBBVP = par.IfNull(par.cmd.ExecuteScalar, 0)

            If ImpRipartito <> TotBBVP Then
                'Se quello che trovo in BBVP è diverso da quello che avevo scritto su incassi extramav...may be fault
                ImpRipartito = TotBBVP
            End If

            'TROVO LA GESTIONALE CREATA PER DIFFERENZA SE NON TUTTO IL CREDITO é STATO UTILIZZATO NELL'INCASSO
            If ImpIncasso <> ImpRipartito Then
                par.cmd.CommandText = "select round(nvl(IMPORTO_TOTALE,0),2) as importo from siscom_mi.bol_bollette_gest where id_evento_pagamento = " & idIncasso
                totGestInca = par.cmd.ExecuteScalar
            End If

            Dim impNewGest As Decimal = 0


            '/*/*/**/*/*/*/ CORE FUNCTION
            If ImpIncasso = ImpRipartito Then
                'caso semplice annullo l'intero importo e creo una nuova gestionale identica a quella utilizzata
                impNewGest = ImpIncasso
            ElseIf ImpIncasso = (ImpRipartito + Math.Abs(totGestInca)) Then
                'in questo caso creo una gestionale dell'importo ripartito
                impNewGest = ImpRipartito
            ElseIf ImpIncasso > (ImpRipartito + Math.Abs(totGestInca)) Then
                'in questo caso creo una gestionale dell'importo iniziale...may be fault
                impNewGest = ImpIncasso
            Else
                'non posso intervenire via sofware, occorre una verifica "umana" per capire cosa è successo sulla partita contabile
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Impossibile annullare questo incasso derivante da gestionale!/nContattare il supporto tecnico.');", True)
                impNewGest = 0
            End If

            If impNewGest <> 0 Then
                If EliminaPagamento(False) = True Then
                    If CreaNewGest(idIncasso, idContrGest, idUiGest, idAnagGest, rifDaGest, rifAGest, dataPagGest, dataValGest, impNewGest, idNewGest) = True Then
                        'ANNULLO INCASSO derivante da scrittura gestionale  TIPO -  DATA _EMISSIONE - IMPORTO GEST. Importo ripristinato ImpNewGest
                        par.cmd.CommandText = "select (select acronimo from siscom_mi.tipo_bollette_gest where id = id_tipo)||' del '||getdata(data_emissione)||' €.'||importo_totale   from siscom_mi.bol_bollette_gest where id = " & idBollGest
                        noteEvento = par.cmd.ExecuteScalar
                        noteEvento = "TIPO: " & noteEvento & ". IMPORTO RIPRISTINATO €." & Format(impNewGest * -1, "##,##0.00")
                        ScriviEventiGestRu(idContrGest, "F318", noteEvento)

                        'Creazione credito gestionale TIPO -  DATA _EMISSIONE €.impNewGest derivante da annullo incasso
                        par.cmd.CommandText = "select (select acronimo from siscom_mi.tipo_bollette_gest where id = id_tipo)||' DEL: '||getdata(data_emissione)||' €.'||importo_totale   from siscom_mi.bol_bollette_gest where id = " & idNewGest
                        noteEvento = par.cmd.ExecuteScalar
                        noteEvento = "TIPO: " & noteEvento
                        ScriviEventiGestRu(idContrGest, "F319", noteEvento)



                    End If
                End If

            End If

        Else
            AnnullaIncaGestionale = False
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Operatore non autorizzato ad eliminare un incasso derivante da scrittura gestionale!');", True)
            Exit Function
        End If

    End Function
    Private Function CreaNewGest(ByVal idInca As Integer, ByVal idContratto As Integer, ByVal idUI As Integer, ByVal idAnagGest As Integer,
                                 ByVal rifDa As String, ByVal rifA As String, ByVal dataPag As String, ByVal dataVal As String, ByVal importo As Decimal, ByRef idNewGest As Double) As Boolean
        CreaNewGest = False
        Dim NoteGest As String = ""
        par.cmd.CommandText = "SELECT siscom_mi.SEQ_BOL_BOLLETTE_GEST.NEXTVAL FROM DUAL"
        idNewGest = par.cmd.ExecuteScalar

        NoteGest = "Ripristinato credito gestionale - Anullo incasso"

        par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE,ID_EVENTO_PAGAMENTO ) " _
                            & "VALUES (" & idNewGest & "," & idContratto & "," & par.RicavaEsercizioCorrente() & "," & idUI & "," & idAnagGest & "," _
                            & "'" & rifDa & "','" & rifA & "'," & par.VirgoleInPunti(importo * (-1)) & "," _
                            & "'" & Format(Now, "yyyyMMdd") & "','" & par.IfEmpty(dataPag, Format(Now, "dd/MM/yyyy")) & "'," _
                            & "'" & par.IfEmpty(dataVal, Format(Now, "dd/MM/yyyy")) & "'" _
                            & ",100,'N',NULL,'" & NoteGest & "'," & idInca & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (siscom_mi.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idNewGest & ",711," & par.VirgoleInPunti(importo * -1) & ")"
        par.cmd.ExecuteNonQuery()


        CreaNewGest = True

    End Function
    Private Function ScriviEventiGestRu(ByVal idContratto As Integer, ByVal tipo As String, ByVal noteEv As String) As Boolean
        ScriviEventiGestRu = False

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (MOTIVAZIONE, COD_EVENTO, DATA_ORA, ID_OPERATORE, ID_CONTRATTO) " _
                            & "VALUES ( '" & par.PulisciStrSql(noteEv) & "'/* MOTIVAZIONE */," _
                            & " '" & tipo & "'/* COD_EVENTO */," _
                            & " '" & Format(Now, "yyyyMMddHHmmss") & "'/* DATA_ORA */," _
                            & " " & Session.Item("id_operatore") & "/* ID_OPERATORE */," _
                            & " " & idContratto & "/* ID_CONTRATTO */ )"
        par.cmd.ExecuteNonQuery()
        ScriviEventiGestRu = True
    End Function
    Private Function dataOpSupDataBlocco() As Boolean
        dataOpSupDataBlocco = True

        par.cmd.CommandText = "select ID_TIPO_PAG from SISCOM_MI.incassi_extramav where id = " & idSelected.Value
        Dim idTipoGest As String
        idTipoGest = par.IfNull(par.cmd.ExecuteScalar, 0)
        'SE TIPO INCASSO DA ANNULLARE DERIVA DA UNA GESTIONALE ALLORA VERIFICO SE OPERATORE AUTORIZZATO A SALTARE CONTROLLO DATA ORA OPERAZIONE NELL'ANNO IN CORSO
        If par.IfNull(idTipoGest, "") = "11" Or par.IfNull(idTipoGest, "") = "12" Then
            par.cmd.CommandText = "select FL_ANNULLA_INCA_GEST from operatori where id = " & Session.Item("id_operatore")
            Dim OpAutorizzato As Integer = 0
            OpAutorizzato = par.cmd.ExecuteScalar
            If OpAutorizzato = 1 Then
                dataOpSupDataBlocco = True
                Exit Function
            Else
                par.cmd.CommandText = "select id_tipo_pag from siscom_mi.incassi_extramav where id = " & idSelected.Value
                Dim idtipoPag As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If idtipoPag <> 8 Then
                    par.cmd.CommandText = "select substr(data_ora,1,8) as data_ora from siscom_mi.incassi_extramav where id = " & idSelected.Value
                    Dim dataOperazione As String = par.IfNull(par.cmd.ExecuteScalar, "19000101")
                    par.cmd.CommandText = "select nvl(valore,'19000101') from siscom_mi.parametri_bolletta where id = 48"
                    Dim dataBlocco As String = par.IfNull(par.cmd.ExecuteScalar, "19000101")
                    If dataOperazione < dataBlocco Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Incasso non cancellabile perchè avvenuto prima della data di blocco!');", True)
                        dataOpSupDataBlocco = False
                    End If
                End If

            End If
        Else


            par.cmd.CommandText = "select FL_ANNULLA_INCA_GEST from operatori where id = " & Session.Item("id_operatore")
            Dim OpAutorizzato As Integer = 0
            OpAutorizzato = par.cmd.ExecuteScalar
            If OpAutorizzato = 1 Then
                dataOpSupDataBlocco = True
                Exit Function

            Else
                par.cmd.CommandText = "select id_tipo_pag from siscom_mi.incassi_extramav where id = " & idSelected.Value
                Dim idtipoPag As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If idtipoPag <> 8 Then
                    par.cmd.CommandText = "select substr(data_ora,1,8) as data_ora from siscom_mi.incassi_extramav where id = " & idSelected.Value
                    Dim dataOperazione As String = par.IfNull(par.cmd.ExecuteScalar, "19000101")
                    par.cmd.CommandText = "select nvl(valore,'19000101') from siscom_mi.parametri_bolletta where id = 48"
                    Dim dataBlocco As String = par.IfNull(par.cmd.ExecuteScalar, "19000101")
                    If dataOperazione < dataBlocco Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Incasso non cancellabile perchè avvenuto prima della data di blocco!');", True)
                        dataOpSupDataBlocco = False
                    End If
                End If
            End If

           

        End If



    End Function

    Private Function IsGestionale() As Boolean
        IsGestionale = False
        Dim idTipoPag As Integer = 0
        par.cmd.CommandText = "select ID_TIPO_PAG from SISCOM_MI.incassi_extramav where id = " & idSelected.Value
        idTipoPag = par.IfNull(par.cmd.ExecuteScalar, 0)
        'tipi pagamento 11 e 12 sono incassi gestionali MT docet!
        If par.IfNull(idTipoPag, "") = "11" Or par.IfNull(idTipoPag, "") = "12" Then
            IsGestionale = True
        End If
    End Function
    Private Function isIncModificabile() As Boolean
        isIncModificabile = True
        'par.cmd.CommandText = "select FL_ANNULLA_INCA_GEST from operatori where id = " & Session.Item("id_operatore")
        'Dim OpAutorizzato As Integer = 0
        'OpAutorizzato = par.cmd.ExecuteScalar

        par.cmd.CommandText = "select id from SISCOM_MI.bol_bollette_gest where id_evento_pagamento = " & idSelected.Value & " and tipo_applicazione <> 'N'"
        Dim countApplicati As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If countApplicati <> 0 Then
            'vedo se incasso da gest è stato annullato
            Dim cntAnnullo As Integer = 0
            par.cmd.CommandText = "select count(*) from siscom_mi.incassi_extramav where id_bolletta_gest = " & countApplicati & " and fl_annullata = 0"

            cntAnnullo = par.cmd.ExecuteScalar
            If cntAnnullo <> 0 Then
                'If OpAutorizzato = 0 Then
                isIncModificabile = False
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Il pagamento scelto non può essere annullato\n" _
                                                                                                & "perchè il credito gestionale maturato a seguito dello stesso,\n" _
                                                                                                & "è stato utilizzato e ripartito su altre partite contabili!');", True)

                'End If
            Else
                isIncModificabile = True
            End If

        End If
    End Function
    Private Function EliminaPagamento(ByVal eliminaGestIncaParziale As Boolean) As Boolean
        'VOCI BOLLETTE
        EliminaPagamento = False
        If AnnullaIncassiAttribuiti(idSelected.Value) = False Then
            Exit Function
        End If
        Dim idMor As Integer = 0

        par.cmd.CommandText = "update SISCOM_MI.incassi_extramav set fl_annullata = 1 where id = " & idSelected.Value
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "insert into siscom_mi.ANNULLO_INCASSI_EXTRAMAV (id_incasso,id_operatore,data_ora,note) values " _
                            & "( " & idSelected.Value & ", " & Session.Item("ID_OPERATORE") & "," & Format(Now, "yyyyMMddHHmmss") & ",'')"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "select data_pagamento from SISCOM_MI.incassi_extramav where id = " & idSelected.Value
        Dim dataPagIncasso As String = par.cmd.ExecuteScalar
        par.cmd.CommandText = "SELECT id_voce_bolletta,sum(nvl(importo_pagato,0)) as importo_pagato FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI WHERE ID_INCASSO_EXTRAMAV = " & idSelected.Value & " group by id_voce_bolletta"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable()
        da.Fill(dt)
        For Each r As Data.DataRow In dt.Rows
            idMor = 0
            par.cmd.CommandText = "update SISCOM_MI.bol_bollette_voci set imp_pagato = (nvl(imp_pagato,0) + " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & "),importo_riclassificato_pagato = (nvl(importo_riclassificato_pagato,0) + (case when nvl(importo_riclassificato,0) >0 then " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & " else  0 end)) where id = " & r.Item("id_voce_bolletta")
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                    & "(" & r.Item("id_voce_bolletta") & ",'" & dataPagIncasso & "'," & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & ",2," & idSelected.Value & ")"
            par.cmd.ExecuteNonQuery()

            '13/02/2015 Aggiungo condizione di annullo pagamento bolletta tipo=4 (MOROSITA)
            CtrlIncassoSuRicla(idSelected.Value, idMor, False)
            If idMor > 0 Then
                par.cmd.CommandText = "UPDATE siscom_mi.bol_Bollette_voci set imp_pagato = (nvl(imp_pagato,0) + " & par.VirgoleInPunti(r.Item("importo_pagato") * -1) & ") where id_voce = 150 and id_bolletta=" & idMor
                par.cmd.ExecuteNonQuery()
            End If
            'FINE 13/02/2015 Aggiungo condizione di annullo pagamento bolletta tipo=4 (MOROSITA)
        Next


        'DATE BOLLETTE
        ' #DATABOLL A seguito trigger per gestione data_pagamento della bolletta
        ' commento la precedente gestione, perchè non sufficiente a recuperare il dato corretto
        '' ''par.cmd.CommandText = "select max(id) from siscom_mi.incassi_extramav where id_contratto = (select id_contratto from siscom_mi.incassi_extramav bi where bi.id = " & idSelected.Value & " ) and fl_annullata = 0"
        '' ''Dim maxIdinc As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        '' ''Dim DataValuta As String = ""
        '' ''Dim DataPagamento As String = ""

        '' ''If maxIdinc > 0 Then
        '' ''    par.cmd.CommandText = "select data_registrazione,data_pagamento from siscom_mi.incassi_extramav where id = (select max(id) from siscom_mi.incassi_extramav ia where ia.id_contratto = (select id_contratto from siscom_mi.incassi_extramav bi where bi.id = " & maxIdinc & " ) and fl_annullata = 0)"
        '' ''    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        '' ''    If lettore.Read Then
        '' ''        DataValuta = par.IfNull(lettore("data_registrazione"), "")
        '' ''        DataPagamento = par.IfNull(lettore("data_pagamento"), "")
        '' ''    End If
        '' ''    lettore.Close()
        '' ''End If


        par.cmd.CommandText = "select id,nvl(importo_pagato,0) as importo_pagato from SISCOM_MI.bol_bollette where id in (SELECT ID_BOLLETTA FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE " _
                & " ID in (select distinct id_voce_bolletta from SISCOM_MI.bol_bollette_voci_pagamenti where id_incasso_extramav  = " & idSelected.Value & "))"
        da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        dt = New Data.DataTable
        da.Fill(dt)

        For Each rBolletta As Data.DataRow In dt.Rows
            ' #DATABOLL 
            'If par.IfNull(rBolletta.Item("IMPORTO_PAGATO"), 0) = 0 Then
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_PAGAMENTO = null,data_valuta = null,OPERATORE_PAG = NULL,DATA_INS_PAGAMENTO = NULL,FL_PAG_PARZ = (case when NVL(FL_PAG_PARZ,0)>0 then (NVL(FL_PAG_PARZ,0)  - 1) else 0 end) WHERE ID = " & par.IfNull(rBolletta.Item("id"), 0)
            'Else
            'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET OPERATORE_PAG = '',DATA_INS_PAGAMENTO = '', FL_PAG_PARZ = (case when NVL(FL_PAG_PARZ,0)>0 then (NVL(FL_PAG_PARZ,0)  - 1) else 0 end), " _
            '                    & "DATA_PAGAMENTO = '" & DataPagamento & "' , data_valuta = '" & DataValuta & "'  WHERE ID = " & par.IfNull(rBolletta.Item("id"), 0)
            'End If
            par.cmd.ExecuteNonQuery()


            '13/02/2015 Aggiorno date pagamento bolletta tipo=4 (MOROSITA)
            par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id in (select id_bolletta_ric from siscom_mi.bol_bollette b2 where b2.id= " & par.IfNull(rBolletta.Item("id"), 0) & ")"
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable()
            da2.Fill(dt2)
            For Each rBollettaRic As Data.DataRow In dt2.Rows
                ' #DATABOLL 
                'If par.IfNull(rBollettaRic.Item("IMPORTO_PAGATO"), 0) = 0 Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_PAGAMENTO = null,OPERATORE_PAG = '',DATA_INS_PAGAMENTO = '',FL_PAG_PARZ = (case when NVL(FL_PAG_PARZ,0)>0 then (NVL(FL_PAG_PARZ,0)  - 1) else 0 end) WHERE ID = " & par.IfNull(rBollettaRic.Item("id"), 0)
                'Else
                'par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET OPERATORE_PAG = '',DATA_INS_PAGAMENTO = '', FL_PAG_PARZ = (case when NVL(FL_PAG_PARZ,0)>0 then (NVL(FL_PAG_PARZ,0)  - 1) else 0 end), " _
                '                        & "DATA_PAGAMENTO = '" & DataPagamento & "' , data_valuta = '" & DataValuta & "'  WHERE ID = " & par.IfNull(rBollettaRic.Item("id"), 0)
                'End If
                par.cmd.ExecuteNonQuery()

            Next
            '13/02/2015 Fine Aggiorno date pagamento bolletta tipo=4 (MOROSITA)
        Next


        If eliminaGestIncaParziale Then
            par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_voci_gest where ID_BOLLETTA_GEST = (select id from siscom_mi.bol_bollette_gest where id_evento_pagamento = " & idSelected.Value & " and tipo_applicazione = 'N')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "delete from SISCOM_MI.bol_bollette_gest where id_evento_pagamento = " & idSelected.Value & " and tipo_applicazione = 'N'"
            par.cmd.ExecuteNonQuery()
        End If


        EliminaPagamento = True
    End Function

    Private Function AnnullaIncassiAttribuiti(ByVal IncassoExtramav As String) As Boolean
        AnnullaIncassiAttribuiti = False
        par.cmd.CommandText = "select nvl(ID_INCASSO_NON_ATTR,0) from SISCOM_MI.INCASSI_ATTRIBUITI where ID_INCASSO_EXTRAMAV = " & IncassoExtramav
        Dim idNonattrib As Integer = 0
        idNonattrib = par.IfNull(par.cmd.ExecuteScalar, 0)
        If idNonattrib <> 0 Then
            'annulla tutti i record di incassi_attribuiti relativi all'incasso extramav
            'in questione. Riazzera il flag dell'assegno che era stato 
            'utilizzato per l'incasso
            Dim resNonAttribuiti As Decimal = 0
            par.cmd.CommandText = "select sum(nvl(importo,0)) as tot from SISCOM_MI.incassi_attribuiti where id_incasso_extramav = " & IncassoExtramav
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                resNonAttribuiti = par.IfNull(lettore(0), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "UPDATE (SELECT * FROM SISCOM_MI.INCASSI_ATTRIBUITI " _
                                & " WHERE ID_INCASSO_EXTRAMAV=" & IncassoExtramav & ") AA SET AA.FL_ANNULLATO=1 "
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.INCASSI_NON_ATTRIBUIBILI " _
                                & " SET FL_ATTRIBUITO=0,DATA_ATTRIBUZIONE=NULL,importo_residuo = importo_residuo+" & par.VirgoleInPunti(resNonAttribuiti) _
                                & " WHERE ID IN (SELECT DISTINCT ID_INCASSO_NON_ATTR FROM SISCOM_MI.INCASSI_ATTRIBUITI WHERE ID_INCASSO_EXTRAMAV=" & IncassoExtramav & ")"
            par.cmd.ExecuteNonQuery()
        End If

        AnnullaIncassiAttribuiti = True
    End Function




    Protected Sub btnVisualizza_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Session.Item("MODINCA") = 1 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "document.getElementById('btnEdit').click();", True)
            Session.Remove("MODINCA")
        Else
            idSelected.Value = 0
        End If
    End Sub
End Class
