
Partial Class Contratti_CambioIntUSD
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Public Property IndiceContratto() As String
        Get
            If Not (ViewState("par_IndiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_IndiceContratto"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceContratto") = value
        End Set

    End Property

    Public Property indiceAnagr() As Long
        Get
            If Not (ViewState("par_indiceAnagr") Is Nothing) Then
                Return CLng(ViewState("par_indiceAnagr"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_indiceAnagr") = value
        End Set

    End Property

    Public Property indiceAnagrOLD() As Long
        Get
            If Not (ViewState("par_indiceAnagrOLD") Is Nothing) Then
                Return CLng(ViewState("par_indiceAnagrOLD"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_indiceAnagrOLD") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then

                IndiceContratto = Request.QueryString("IDC")
                IDCONNESSIONE.Value = Request.QueryString("IDT")
                dataCessione.Value = Request.QueryString("DATARIC")
                restituzDepCauz.Value = Request.QueryString("RESTDEP")
                newDepCauz.Value = Request.QueryString("NEWDEP")
                canoneNuovo.Value = Replace(Request.QueryString("IMPC"), ".", ",")

                par.OracleConn.Open()
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbComune", "SELECT * FROM comuni_nazioni where sigla<>'E' AND SIGLA<>'C' order by nome asc", "NOME", "COD")
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni where SIGLA='C' OR sigla='E' OR SIGLA='I' order by nome asc", "NOME", "COD")

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If


            txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPIva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            txtComuneResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtProvinciaResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtIndirizzoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCivicoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCAP.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtTel.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            txtNumProcura.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataProcura.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataNascita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtNumDoc.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataDoc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtDocRilasciato.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtSoggiorno.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            txtComuneResidenza0.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtProvinciaResidenza0.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtIndirizzoResidenza0.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCivicoResidenza0.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCAP0.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtTel0.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")



        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            'Dim indice As Long = 0
            Dim SSS As String = ""
            Dim Va_Bene1 As Boolean = True

            lblErrore.Visible = False

            If ins.Value <> "111" Then

                If Len(txtPIva.Text) <> 11 Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Partita Iva non inserita o non valida!"

                    Exit Sub
                End If

                If txtRagione.Text = "" Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Inserire un la Ragione Sociale e specificare una partita iva valida!"

                    Exit Sub
                End If

                If txtCAP.Text <> "" Then
                    If Len(txtCAP.Text) <> 5 Or IsNumeric(txtCAP.Text) = False Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il CAP della Sede Legale deve essere di 5 caratteri numerici!"

                        Exit Sub
                    End If
                End If


                If txtCognome.Text = "" Or txtNome.Text = "" Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Inserire un cognome/nome e specificare un codice fiscale valido!"

                    Exit Sub
                End If

                If par.ControllaCF(txtCF.Text) = False Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"

                    Exit Sub
                End If

                If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"

                    Exit Sub
                End If

                If par.RicavaSesso(txtCF.Text) <> cmbSesso.SelectedItem.Text Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Il sesso non corrisponde a quanto dichiarato nel codice fiscale!"

                    Exit Sub
                End If


                Dim miaData As String = ""

                If Val(Mid(txtCF.Text, 10, 2)) > 40 Then
                    miaData = Format(Val(Mid(txtCF.Text, 10, 2)) - 40, "00")
                Else
                    miaData = Mid(txtCF.Text, 10, 2)
                End If

                Select Case Mid(txtCF.Text, 9, 1)
                    Case "A"
                        miaData = miaData & "/01"
                    Case "B"
                        miaData = miaData & "/02"
                    Case "C"
                        miaData = miaData & "/03"
                    Case "D"
                        miaData = miaData & "/04"
                    Case "E"
                        miaData = miaData & "/05"
                    Case "H"
                        miaData = miaData & "/06"
                    Case "L"
                        miaData = miaData & "/07"
                    Case "M"
                        miaData = miaData & "/08"
                    Case "P"
                        miaData = miaData & "/09"
                    Case "R"
                        miaData = miaData & "/10"
                    Case "S"
                        miaData = miaData & "/11"
                    Case "T"
                        miaData = miaData & "/12"
                End Select

                'If txtDataNascita.Text <> "" Then
                If Mid(txtDataNascita.Text, 1, 5) <> miaData Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! La data di nascita del contraente non corrisponde a quanto dichiarato nel codice fiscale!"

                    Exit Sub
                End If
                'End If

                If Mid(txtCF.Text, 12, 1) <> "Z" Then
                    If Mid(txtCF.Text, 12, 4) <> CmbComune.SelectedItem.Value Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il Comune di nascita del contraente non corrisponde a quanto dichiarato nel codice fiscale!"

                        Exit Sub
                    End If
                End If



                If par.AggiustaData(txtDataDoc.Text) > Format(Now, "yyyyMMdd") Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere precedente o uguale alla data odierna!"

                    Exit Sub
                End If




                Dim tipo_r As Integer = 0
                Dim COMUNE_NASCITA As String = ""

                If lblErrore.Visible = False Then
                    Me.lblErrore.Visible = False

                    'If par.OracleConn.State = Data.ConnectionState.Closed Then
                    '    par.OracleConn.Open()
                    '    par.SettaCommand(par)
                    'End If

                    par.OracleConn = CType(HttpContext.Current.Session.Item(IDCONNESSIONE.Value), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IDCONNESSIONE.Value), Oracle.DataAccess.Client.OracleTransaction)

                    If IsNothing(Session.Item("calcoloAdeguamentoInteressi")) = False Then
                        Dim listaDaEseguire As Generic.List(Of String) = Session.Item("calcoloAdeguamentoInteressi")
                        For Each stringa As String In listaDaEseguire
                            par.cmd.CommandText = stringa
                            par.cmd.ExecuteNonQuery()
                        Next
                    End If

                    par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                        & " WHERE RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND COD_TIPOLOGIA_OCCUPANTE='INTE' AND RAPPORTI_UTENZA.ID=" & IndiceContratto
                    Dim myReaderEx As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderEx.Read Then
                        indiceAnagrOLD = par.IfNull(myReaderEx("ID_ANAGRAFICA"), 0)
                    End If
                    myReaderEx.Close()

                    Dim va_bene As Boolean = True

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(PARTITA_IVA)='" & par.PulisciStrSql(Me.txtPIva.Text) & "' and UPPER(RAGIONE_SOCIALE)='" & par.PulisciStrSql(Me.txtRagione.Text) & "'"


                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If myReader.Read Then
                            indiceAnagr = myReader("id")

                            '    If UCase(par.IfNull(myReader("ragione_sociale"), "")) <> UCase(txtRagione.Text) Then
                            '        Va_Bene1 = False
                            '        SSS = "Ragione Sociale: " & UCase(par.IfNull(myReader("ragione_sociale"), ""))
                            '    End If

                        End If

                        par.cmd.CommandText = "SELECT id_anagrafica FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & IndiceContratto & " and id_anagrafica=" & indiceAnagr
                        Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderX1.HasRows = True Then
                            va_bene = False
                        End If
                        myReaderX1.Close()

                        If Va_Bene1 = True And va_bene = True Then

                            COMUNE_NASCITA = CmbComune.SelectedItem.Value
                            If COMUNE_NASCITA = "-1" Then
                                If Len(txtCF.Text) = 16 Then
                                    COMUNE_NASCITA = Mid(txtCF.Text, 12, 4)
                                End If
                            End If

                            If chProcuratore.Checked = True Then
                                tipo_r = 1
                            Else
                                tipo_r = 0
                            End If


                            par.cmd.CommandText = "update siscom_mi.anagrafica set cognome='" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) _
                                                & "',nome='" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) _
                                                & "',cod_fiscale='" & par.PulisciStrSql(Me.txtCF.Text) _
                                                & "',cittadinanza='" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) _
                                                & "',residenza='" & par.PulisciStrSql(txtComuneResidenza.Text & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) _
                                                & "',data_nascita='" & par.AggiustaData(Me.txtDataNascita.Text) _
                                                & "',cod_comune_nascita='" & COMUNE_NASCITA _
                                                & "',sesso='" & Me.cmbSesso.SelectedItem.Text _
                                                & "',telefono='" & par.PulisciStrSql(Me.txtTel.Text) _
                                                & "',tipo_doc=" & cmbTipoDoc.SelectedItem.Value _
                                                & ",num_doc='" & par.PulisciStrSql(txtNumDoc.Text) _
                                                & "',data_doc='" & par.AggiustaData(txtDataDoc.Text) _
                                                & "',rilascio_doc='" & par.PulisciStrSql(LTrim(RTrim(txtDocRilasciato.Text))) _
                                                & "',comune_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) _
                                                & "',provincia_residenza='" & par.PulisciStrSql(txtProvinciaResidenza.Text) _
                                                & "',indirizzo_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) _
                                                & "',civico_residenza='" & par.PulisciStrSql(txtCivicoResidenza.Text) _
                                                & "',cap_residenza='" & par.PulisciStrSql(txtCAP.Text) _
                                                & "',doc_soggiorno='" & par.PulisciStrSql(txtSoggiorno.Text) _
                                                & "',tipo_r=" & tipo_r _
                                                & ",num_procura='" & par.PulisciStrSql(txtNumProcura.Text) _
                                                & "',data_procura='" & par.AggiustaData(Me.txtDataProcura.Text) _
                                                & "',COMUNE_RESIDENZA_R='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza0.Text))) _
                                                & "', PROVINCIA_RESIDENZA_R='" & par.PulisciStrSql(txtProvinciaResidenza0.Text) _
                                                & "', INDIRIZZO_RESIDENZA_R='" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza0.Text))) _
                                                & "', CIVICO_RESIDENZA_R='" & par.PulisciStrSql(txtCivicoResidenza0.Text) _
                                                & "',CAP_RESIDENZA_R='" & par.PulisciStrSql(txtCAP0.Text) _
                                                & "',TELEFONO_R='" & par.PulisciStrSql(txtTel0.Text) _
                                                & "' where id=" & indiceAnagr
                            par.cmd.ExecuteNonQuery()
                        End If

                    Else

                        COMUNE_NASCITA = CmbComune.SelectedItem.Value
                        If COMUNE_NASCITA = "-1" Then
                            If Len(txtCF.Text) = 16 Then
                                COMUNE_NASCITA = Mid(txtCF.Text, 12, 4)
                            End If
                        End If


                        If chProcuratore.Checked = True Then
                            tipo_r = 1
                        Else
                            tipo_r = 0
                        End If

                        par.cmd.CommandText = "Insert into siscom_mi.ANAGRAFICA (ID, COGNOME, NOME, RAGIONE_SOCIALE, COD_FISCALE, " _
                                            & "PARTITA_IVA, CITTADINANZA, RESIDENZA, DATA_NASCITA, COD_COMUNE_NASCITA, " _
                                            & "SESSO, TELEFONO, ID_INDIRIZZO_RECAPITO, TIPO_DOC, NUM_DOC, " _
                                            & "DATA_DOC, RILASCIO_DOC, COMUNE_RESIDENZA, PROVINCIA_RESIDENZA, INDIRIZZO_RESIDENZA, " _
                                            & "CIVICO_RESIDENZA, CAP_RESIDENZA, DOC_SOGGIORNO, TIPO_R, NUM_PROCURA, " _
                                            & "DATA_PROCURA, COMUNE_RESIDENZA_R, PROVINCIA_RESIDENZA_R, INDIRIZZO_RESIDENZA_R, CIVICO_RESIDENZA_R, " _
                                            & "CAP_RESIDENZA_R,TELEFONO_R) " _
                                            & "Values " _
                                            & "(SISCOM_MI.SEQ_ANAGRAFICA.NEXTVAL, '" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) _
                                            & "', '" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) & "', '" & par.PulisciStrSql(LTrim(RTrim(Me.txtRagione.Text))) _
                                            & "', '" & par.PulisciStrSql(Me.txtCF.Text) & "', " _
                                            & "'" & par.PulisciStrSql(Me.txtPIva.Text) & "', '" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) _
                                            & "', '" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text)) & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) _
                                            & "', '" & par.AggiustaData(Me.txtDataNascita.Text) & "', '" & COMUNE_NASCITA _
                                            & "','" & Me.cmbSesso.SelectedItem.Text & "', '" & par.PulisciStrSql(LTrim(RTrim(Me.txtTel.Text))) _
                                            & "', null, " & cmbTipoDoc.SelectedItem.Value & ", '" & par.PulisciStrSql(txtNumDoc.Text) _
                                            & "', '" & par.AggiustaData(txtDataDoc.Text) & "', '" & par.PulisciStrSql(txtDocRilasciato.Text) _
                                            & "', '" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) & "', '" & par.PulisciStrSql(LTrim(RTrim(txtProvinciaResidenza.Text))) _
                                            & "', '" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) _
                                            & "', '" & par.PulisciStrSql(txtCivicoResidenza.Text) & "', '" & par.PulisciStrSql(txtCAP.Text) _
                                            & "', '" & par.PulisciStrSql(LTrim(RTrim(txtSoggiorno.Text))) & "', " & tipo_r & ", '" & par.PulisciStrSql(txtNumProcura.Text) & "', " _
                                            & "'" & par.AggiustaData(Me.txtDataProcura.Text) & "', '" _
                                            & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza0.Text))) & "', '" _
                                            & par.PulisciStrSql(txtProvinciaResidenza0.Text) & "', '" _
                                            & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza0.Text))) & "', '" & par.PulisciStrSql(txtCivicoResidenza0.Text) _
                                            & "', '" & par.PulisciStrSql(txtCAP0.Text) & "','" & par.PulisciStrSql(txtTel0.Text) & "')"


                        par.cmd.ExecuteNonQuery()


                        par.cmd.CommandText = "select siscom_mi.seq_anagrafica.currval from dual"
                        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderA.Read Then
                            indiceAnagr = myReaderA(0)
                        End If
                        myReaderA.Close()
                    End If
                    myReader.Close()

                    Dim INDICE_EX_INTE As Long = 0

                    If Va_Bene1 = True Then

                        If va_bene = True Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & IndiceContratto
                            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            Do While myReaderX.Read
                                If par.IfNull(myReaderX("COD_TIPOLOGIA_OCCUPANTE"), "-1") = "INTE" Then
                                    INDICE_EX_INTE = par.IfNull(myReaderX("ID_ANAGRAFICA"), -1)
                                End If
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SOGGETTI_CONTRATTUALI SET COD_TIPOLOGIA_OCCUPANTE='EXINTE',DATA_FINE='" & par.AggiustaData(dataCessione.Value) & "' WHERE ID_CONTRATTO=" & IndiceContratto & " AND COD_TIPOLOGIA_OCCUPANTE in ('INTE')"
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SOGGETTI_CONTRATTUALI SET COD_TIPOLOGIA_OCCUPANTE='EXCOINT',DATA_FINE='" & par.AggiustaData(dataCessione.Value) & "' WHERE ID_CONTRATTO=" & IndiceContratto & " AND COD_TIPOLOGIA_OCCUPANTE in ('COINT')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.INTESTATARI_RAPPORTO SET DATA_FINE='" & par.AggiustaData(dataCessione.Value) & "' WHERE ID_CONTRATTO=" & IndiceContratto & " AND ID_ANAGRAFICA=" & par.IfNull(myReaderX("ID_ANAGRAFICA"), "-1")
                                par.cmd.ExecuteNonQuery()

                            Loop
                            myReaderX.Close()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                            & "(ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO,DATA_INIZIO,DATA_FINE) " _
                            & "VALUES (" & indiceAnagr & "," & IndiceContratto & ",'NA','INTE','LEGIT','" & par.AggiustaData(dataCessione.Value) & "','')"
                            par.cmd.ExecuteNonQuery()


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTESTATARI_RAPPORTO (ID_CONTRATTO,ID_ANAGRAFICA,DATA_INIZIO,DATA_FINE) VALUES (" & IndiceContratto & "," & indiceAnagr & ",'" & Format(Now, "yyyyMMdd") & "','29991231')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_CESSIONI (ID_CONTRATTO,DATA_CESSIONE,IMPORTO,ID_EX_INT,ID_INT) VALUES (" & IndiceContratto & ",'" & par.AggiustaData(dataCessione.Value) & "',67," & INDICE_EX_INTE & "," & indiceAnagr & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F38','')"
                            par.cmd.ExecuteNonQuery()


                            'INSERIMENTO AVVISO PER CONDOMINI
                            par.cmd.CommandText = "SELECT cond_edifici.*,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_CONTRATTUALE.ID_CONTRATTO=" & IndiceContratto & " AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
                            myReader = par.cmd.ExecuteReader
                            Do While myReader.Read
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (2," & par.IfNull(myReader("ID_UNITA"), -1) & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & IndiceContratto & ")"

                                par.cmd.ExecuteNonQuery()
                            Loop
                            myReader.Close()


                            '30/05/2014 AGGIORNO CANONE

                            par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_CANONE_INIZIALE=" & par.VirgoleInPunti(canoneNuovo.Value) & ",PRESSO_COR='" & par.PulisciStrSql(txtRagione.Text) & "' WHERE ID=" & IndiceContratto
                            par.cmd.ExecuteNonQuery()

                            'FINE AGGIORNO CANONE

                            '05/12/2016 ELIMINO EVENTUALE ISTAT

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=" & IndiceContratto
                            myReader = par.cmd.ExecuteReader
                            If myReader.HasRows = True Then
                                If myReader.Read Then
                                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_MOTIVO=2 AND ID_CONTRATTO=" & IndiceContratto
                                    par.cmd.ExecuteNonQuery()

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F02','ELIMINATA ISTAT DI EURO " & par.IfNull(myReader("IMPORTO"), "0,00") & " IN SEGUITO A CAMBIO INTESTAZIONE')"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If
                            myReader.Close()


                            'FINE ELIMINO ISTAT

                            If restituzDepCauz.Value = "1" Then
                                RestituzioneDepCauz()
                            End If
                            If newDepCauz.Value = "1" Then
                                EmissioneNewDeposito()
                            End If
                            '************** 19/07/2017 L'imposta viene regolata dal caricamento della ricevuta
                            'CreaImpostaRegistro()

                            par.myTrans.Commit()
                            Session.Remove("calcoloAdeguamentoInteressi")
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE" & IDCONNESSIONE.Value, par.myTrans)

                            'Response.Write("<script>alert('Operazione effettuata! Stampare il modulo nella pagina successiva e se occorre, cambiare l\'indirizzo nel TAB comunicazioni!');opener.document.getElementById('form1').USCITA.value='1';opener.document.getElementById('form1').cmbintestazione.value='OK';opener.document.getElementById('form1').submit();opener.document.getElementById('form1').pressoCOR.value='" & Replace(txtRagione.Text, "'", "\'") & "';opener.document.getElementById('imgSalva').click();window.open('Comunicazioni/CambioIntestazioneF.aspx?IDC=" & IndiceContratto & "','CambioIntestazione','');self.close();</script>")
                            Response.Write("<script>alert('Operazione effettuata! Stampare il modulo nella pagina successiva e se occorre, cambiare l\'indirizzo nel TAB comunicazioni!');opener.document.getElementById('form1').USCITA.value='1';opener.document.getElementById('form1').cmbintestazione.value='OK';opener.document.getElementById('form1').pressoCOR.value='" & Replace(txtRagione.Text, "'", "\'") & "';opener.document.getElementById('imgSalva').click();window.open('Comunicazioni/CambioIntestazioneF.aspx?IDC=" & IndiceContratto & "','CambioIntestazione','');self.close();</script>")
                        Else
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! Il nuovo intestatario non può essere uguale all'intestatario corrente!"
                        End If
                    Else
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il nuovo intestatario è già presente in anagrafica ma con ragione sociale diversa. Verificare l'anagrafica! Nel sistema risulta " & SSS
                    End If
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Avvalorare i campi necessari alla definizione di una persona giuridica!"
                    Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")
                End If
            End If

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub RestituzioneDepCauz()

        Dim ID_VOCE As Integer = 0
        Dim MiaData As Date = DateAdd(DateInterval.Second, 1, Now)
        Dim idBollGest As Long = 0
        Dim esisteDeposito As Boolean = False

        'Dim idFornitore As Long = 0
        'Dim idStruttura As Long = 0
        Dim ID_BOLLETTA As Long = 0
        Dim Valori() As String
        Dim sValori = Session.Item("DATIRIMB")
        Valori = sValori.Split("#")
        Dim TIPO_PAG As String = ""
        Dim Estremi As String = ""
        Dim IdVocePFDep As Long = 0

        If Session.Item("DATIRIMB") <> "" Then
            Dim PianoF As Long = par.RicavaPianoUltimoApprovato

            Dim listaBolletteGestDaAggiornare As String = ""

            Dim interessiSuCdp As Boolean = False
            par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=61"
            If par.IfNull(par.cmd.ExecuteScalar, 0) = 1 Then
                interessiSuCdp = True
            End If

            par.cmd.CommandText = " SELECT ID  FROM SISCOM_MI.PF_VOCI WHERE ID_TIPO_UTILIZZO=2 AND ID_PIANO_FINANZIARIO = " & PianoF
            IdVocePFDep = par.IfNull(par.cmd.ExecuteScalar, 0)

            If IdVocePFDep = 0 Then
                lblErrore.Visible = True
                lblErrore.Text = "VOCE BP NON TROVATA"
            End If

            par.cmd.CommandText = "SELECT rapporti_utenza.*,UNITA_IMMOBILIARI.ID AS ID_UNITA,UNITA_IMMOBILIARI.ID_EDIFICIO,EDIFICI.ID_COMPLESSO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=UNITA_CONTRATTUALE.ID_CONTRATTO AND ID_CONTRATTO=" & IndiceContratto
            Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderR.Read Then
                Select Case Valori(4)
                    Case "1"
                        TIPO_PAG = "1"
                        Estremi = " Assegno circolare intestato a " & Valori(1) & " - " & par.IfNull(myReaderR("cod_contratto"), "")
                    Case "2", "3"
                        TIPO_PAG = "2"
                        Estremi = " Estremi: " & Valori(5) & " intestato a " & Valori(1) & " - " & par.IfNull(myReaderR("cod_contratto"), "")
                End Select

                Dim importo As Decimal = CDec(Valori(2))
                Dim importoInteressi As Decimal = -CDec(Valori(7))
                Dim importoInteressiIniziale As Decimal = importoInteressi
                Dim importoTotale As Decimal = importo + importoInteressi

                Dim IndiciGestionali As String = ""


                If interessiSuCdp Then

                    par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=60"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.HasRows = True Then
                        If myReaderA.Read Then
                            IndiciGestionali = myReaderA("VALORE")
                        End If
                    End If
                    myReaderA.Close()

                    If IndiciGestionali <> "" Then
                        par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST " _
                            & "WHERE BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST=BOL_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N' " _
                            & " AND ID_TIPO IN (" & IndiciGestionali & ") AND ID_CONTRATTO=" & IndiceContratto _
                            & " AND ID_ANAGRAFICA=" & indiceAnagrOLD
                        importoInteressi = importoInteressi + par.IfNull(par.cmd.ExecuteScalar, 0)
                    End If
                End If

                Dim importoGestionale As Decimal = 0
                If interessiSuCdp Then
                    importoGestionale = importoTotale
                Else
                    importoGestionale = importo
                End If

                If importoGestionale <> 0 Then
                    Dim notaGestionale As String = "RESTITUZ. DEPOSITO CAUZ. IN SEGUITO A VAIN"
                    If interessiSuCdp Then
                        notaGestionale = "RESTITUZ. DEPOSITO CAUZ. + INTERESSI IN SEGUITO A VAIN"
                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA, RIFERIMENTO_A," _
                        & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE, ID_EVENTO_PAGAMENTO) " _
                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & IndiceContratto & ", " & par.RicavaEsercizioCorrente & ", " & par.IfNull(myReaderR("ID_UNITA"), "NULL") & "," _
                            & Valori(0) & ",'" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', " & par.VirgoleInPunti(importoGestionale) & ", '" & Format(Now, "yyyyMMdd") & "'," _
                            & "'','',55, 'N', NULL, NULL,'" & notaGestionale & "', NULL)"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        idBollGest = myReader1(0)
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) " _
                       & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & idBollGest & ", 7, " & par.VirgoleInPunti(importo) & ", NULL, NULL)"
                    par.cmd.ExecuteNonQuery()

                    If interessiSuCdp Then
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & idBollGest & ", 15, " & par.VirgoleInPunti(importoInteressiIniziale) & ", NULL, NULL)"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.bol_bollette_gest set id_adeguamento=(select max(id) from SISCOM_MI.ADEGUAMENTO_INTERESSI WHERE ID_cONTRATTO=" & IndiceContratto & " AND IMPORTO=" & par.VirgoleInPunti(importoInteressiIniziale * -1) & ") where id=" & idBollGest
                        par.cmd.ExecuteNonQuery()
                        If listaBolletteGestDaAggiornare = "" Then
                            listaBolletteGestDaAggiornare = idBollGest
                        Else
                            listaBolletteGestDaAggiornare &= "," & idBollGest
                        End If
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA, RIFERIMENTO_DA, RIFERIMENTO_A," _
                        & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO, TIPO_APPLICAZIONE, DATA_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE, ID_EVENTO_PAGAMENTO) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL, " & IndiceContratto & ", " & par.RicavaEsercizioCorrente & ", " & par.IfNull(myReaderR("ID_UNITA"), "NULL") & "," _
                        & Valori(0) & ",'" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', " & par.VirgoleInPunti(importoInteressiIniziale) & ", '" & Format(Now, "yyyyMMdd") & "'," _
                        & "'','',57, 'N', NULL, NULL,'RESTITUZ. INTERESSI DEPOSITO CAUZ. IN SEGUITO A VAIN', NULL)"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                        Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader11.Read Then
                            idBollGest = myReader11(0)
                        End If
                        myReader11.Close()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO, IMP_PAGATO, FL_ACCERTATO) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL, " & idBollGest & ", 15, " & par.VirgoleInPunti(importoInteressiIniziale) & ", NULL, NULL)"
                        par.cmd.ExecuteNonQuery()
                    End If

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '        & "VALUES (" & IndiceContratto & ",1,'" & Format(MiaData, "yyyyMMddHHmmss") & "'," _
                    '        & "'F204','RESTITUZ. DEPOSITO CAUZ. IN SEGUITO A VAIN')"
                    'par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "select * from SISCOM_MI.TAB_GEST_REST_CREDITO WHERE DATA_INIZIO_VALIDITA<='" & Format(Now, "yyyyMMdd") & "' AND DATA_FINE_VALIDITA>='" & Format(Now, "yyyyMMdd") & "' ORDER BY data_fine_validita desc,data_inizio_validita desc"
                    'Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderX.Read Then

                    Dim HFIdVocePF As Integer = -1
                    Dim HFidVoceInteressi As Integer = -1
                    Dim HFidStruttura As Integer = -1
                    Dim HFidFornitore As Integer = -1
                    Dim HFdocRestCred As Integer = -1
                    par.RicavaBPcredito(-1, HFIdVocePF, HFidVoceInteressi, HFidStruttura, HFidFornitore, HFdocRestCred, True)
                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE id=" & idBollGest
                    par.cmd.ExecuteNonQuery()
                    'idFornitore = myReaderX("id_fornitore")
                    'idStruttura = myReaderX("id_struttura")
                    par.cmd.CommandText = "select SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA,RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO,UNITA_CONTRATTUALE.ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & IndiceContratto & " AND UNITA_CONTRATTUALE.ID_CONTRATTO (+)=RAPPORTI_UTENZA.ID AND EDIFICI.ID (+)=UNITA_CONTRATTUALE.ID_EDIFICIO"
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
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                                                & "', '29991231', NULL, " _
                                                & "NULL, '','RIMBORSO DEPOSITO CAUZIONALE'," & IndiceContratto _
                                                & " ," & par.RicavaEsercizioCorrente & ", " _
                                                & par.IfNull(myReaderR("ID_UNITA"), "NULL") _
                                                & ", '0', '', " & par.VirgoleInPunti(Valori(0)) _
                                                & ", '" & par.PulisciStrSql(Valori(1)) & "', " _
                                                & "'" & par.PulisciStrSql(par.IfNull(myReaderR("TIPO_COR"), "") & " " & par.IfNull(myReaderR("VIA_COR"), "") & ", " & par.IfNull(myReaderR("civico_COR"), "")) _
                                                & "', '" & par.PulisciStrSql(par.IfNull(myReaderR("CAP_COR"), "") _
                                                & " " & par.IfNull(myReaderR("LUOGO_COR"), "") & "(" & par.IfNull(myReaderR("SIGLA_COR"), "") & ")") & "', NULL, '" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', " _
                                                & "'1', " & myReaderR("ID_COMPLESSO") & ", '', NULL, '', " _
                                                & Year(Now) & ", '', " & myReaderR("ID_EDIFICIO") & ", NULL, NULL,'MOD', " & HFdocRestCred & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderA.Read Then
                            ID_BOLLETTA = myReaderA(0)



                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",50" _
                            & "," & par.VirgoleInPunti(importo) & ")"
                            par.cmd.ExecuteNonQuery()

                            If interessiSuCdp Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",15" _
                                & "," & par.VirgoleInPunti(importoInteressiIniziale) & ")"
                                par.cmd.ExecuteNonQuery()


                                If IndiciGestionali <> "" Then
                                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST " _
                                        & "WHERE BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST=BOL_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N' " _
                                        & " AND ID_TIPO IN (" & IndiciGestionali & ") AND ID_CONTRATTO=" & IndiceContratto _
                                        & " AND ID_ANAGRAFICA=" & indiceAnagrOLD _
                                        & " ORDER BY ID_TIPO ASC"
                                    myReaderA = par.cmd.ExecuteReader()
                                    Do While myReaderA.Read
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & myReaderA("ID_VOCE") _
                                                            & "," & par.VirgoleInPunti(myReaderA("IMPORTO")) & ")"
                                        par.cmd.ExecuteNonQuery()
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & par.IfNull(myReaderA("ID"), 0)
                                        par.cmd.ExecuteNonQuery()
                                        If listaBolletteGestDaAggiornare = "" Then
                                            listaBolletteGestDaAggiornare = par.IfNull(myReaderA("ID"), 0)
                                        Else
                                            listaBolletteGestDaAggiornare &= "," & par.IfNull(myReaderA("ID"), 0)
                                        End If
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F02', 'IMPORTO DI EURO " & myReaderA("IMPORTO") & " (" & par.PulisciStrSql(par.IfNull(myReaderA("NOTE"), "")) & ") INSERITO NEL CDP DI RESTITUZIONE DEL DEPOSITO CAUZIONALE')"
                                        par.cmd.ExecuteNonQuery()
                                        'importoInteressi = importoInteressi + par.IfNull(myReaderA("IMPORTO"), 0)
                                    Loop
                                    myReaderA.Close()
                                End If



                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F08','RIMBORSO DEPOSITO CAUZIONALE E INTERESSI')"
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F08','RIMBORSO DEPOSITO CAUZIONALE')"
                                par.cmd.ExecuteNonQuery()
                            End If

                            If listaBolletteGestDaAggiornare <> "" Then
                                par.cmd.CommandText = "update siscom_mi.bol_bollette_gest set id_bolletta=" & ID_BOLLETTA & "where id in (" & listaBolletteGestDaAggiornare & ")"
                                par.cmd.ExecuteNonQuery()
                            End If



                            '04/05/2015 INSERIMENTO CDP
                            Dim SStringa As String = ""
                            Dim vIdCDP As Long

                            par.cmd.CommandText = " select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM dual "
                            Dim myReaderP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderP.Read Then
                                vIdCDP = myReaderP(0)
                            End If
                            myReaderP.Close()

                            Dim totPag As Decimal = 0
                            'AGGIUNTA DEGLI INTERESSI
                            If interessiSuCdp Then
                                totPag = CDec(par.IfEmpty((importo) * -1, 0)) + CDec(par.IfEmpty((importoInteressi) * -1, 0))
                            Else
                                totPag = CDec(par.IfEmpty((importo) * -1, 0))
                            End If

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,CONTO_CORRENTE) " _
                            & "VALUES " _
                            & "(" & vIdCDP & "," & Format(Now, "yyyyMMdd") & ", '" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) & "'," _
                            & par.VirgoleInPunti(totPag) & "," & HFidFornitore & ",NULL,1,10,'---')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,ID_PAGAMENTO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,DATA_STAMPA,ID_STRUTTURA,DATA_CONSUNTIVAZIONE,DATA_CERTIFICAZIONE) VALUES " _
                                                & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL," & HFidFornitore & "," & IdVocePFDep & ",2," & vIdCDP & ",10,'" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) _
                                                & "','" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importo * -1) & "," & par.VirgoleInPunti(importo * -1) _
                                                & ",'" & Format(Now, "yyyyMMdd") & "'," & HFidStruttura & ",'" & Format(Now, "yyyyMMdd") & "','" & Format(Now, "yyyyMMdd") & "')"
                            par.cmd.ExecuteNonQuery()

                            If interessiSuCdp Then
                                If importoInteressi <> 0 Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,ID_PAGAMENTO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,DATA_STAMPA,ID_STRUTTURA,DATA_CONSUNTIVAZIONE,DATA_CERTIFICAZIONE) VALUES " _
                                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL," & HFidFornitore & "," & HFidVoceInteressi & ",2," & vIdCDP & ",10,'" & par.PulisciStrSql("RIMBORSO DEPOSITO CAUZIONALE ") & par.PulisciStrSql(Estremi) _
                                                    & "','" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importoInteressi * -1) & "," & par.VirgoleInPunti(importoInteressi * -1) _
                                                    & ",'" & Format(Now, "yyyyMMdd") & "'," & HFidStruttura & ",'" & Format(Now, "yyyyMMdd") & "','" & Format(Now, "yyyyMMdd") & "')"
                                    par.cmd.ExecuteNonQuery()
                                End If
                            End If




                            '****Scrittura evento EMISSIONE DEL PAGAMENTO
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCDP & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.ELENCO_BOLL_CDP (ID_BOLLETTA,ID_CDP,ID_BOLL_GEST,ID_TIPO_MODALITA_PAG,ESTREMI_IBAN_CC) VALUES (" & ID_BOLLETTA & "," & vIdCDP & ",null," & TIPO_PAG & ",'" & par.PulisciStrSql(Valori(5)) & "')"
                            par.cmd.ExecuteNonQuery()

                            Dim NUM_CDP As Long = 0
                            Dim ANNO_CDP As Long = 0

                            par.cmd.CommandText = " select * from SISCOM_MI.PAGAMENTI where id=" & vIdCDP
                            myReader1 = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                NUM_CDP = par.IfNull(myReader1("PROGR"), 0)
                                ANNO_CDP = par.IfNull(myReader1("ANNO"), 0)
                            End If
                            myReader1.Close()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_CAUZ " _
                                & " (ID_CONTRATTO,DATA_OPERAZIONE,CREDITO,INTERESSI,TIPO_PAGAMENTO,NOTE_PAGAMENTO," _
                                & " DATA_CERT_PAG,NUM_CDP,ANNO_CDP,DATA_MANDATO,NUM_MANDATO,ANNO_MANDATO,ID_BOLLETTA,IBAN_CC,id_anagrafica) " _
                                & " values (" & IndiceContratto & ",'" & Format(Now, "yyyyMMdd") & "'," _
                                & par.VirgoleInPunti(CDec(par.IfEmpty((importo) * -1, 0))) & "," &
                                par.VirgoleInPunti(CDec(par.IfEmpty((importoInteressi) * -1, 0))) & ",'" _
                                & par.PulisciStrSql(Valori(4)) & "','" & par.PulisciStrSql(Valori(6)) & "','" _
                                & Format(Now, "yyyyMMdd") & "','" & NUM_CDP & "','" & ANNO_CDP & "',NULL,NULL,NULL," _
                                & ID_BOLLETTA & ",'" & UCase(par.PulisciStrSql(Valori(5))) & "'," & indiceAnagrOLD & ")"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.STORICO_DEP_CAUZIONALE WHERE ID_CONTRATTO=" & IndiceContratto & " AND ID_ANAGRAFICA=" & indiceAnagrOLD & " AND FL_ORIGINALE=1"
                            Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lett.Read Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.STORICO_DEP_CAUZIONALE SET DATA_RESTITUZIONE='" & Format(Now, "yyyyMMdd") & "',IMPORTO_RESTITUZIONE=" & par.VirgoleInPunti(importo * -1) & " WHERE ID=" & par.IfNull(lett("ID"), 0)
                                par.cmd.ExecuteNonQuery()
                            Else
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE ( " _
                                    & " DATA, DATA_COSTITUZIONE, DATA_RESTITUZIONE,  " _
                                    & " FL_ORIGINALE, ID, ID_ANAGRAFICA,  " _
                                    & " ID_BOLLETTA, ID_CONTRATTO, IMPORTO,  " _
                                    & " IMPORTO_RESTITUZIONE, NOTE)  " _
                                    & " VALUES ((SELECT DATA_DECORRENZA FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IndiceContratto & ") /* DATA */, " _
                                    & " (SELECT DATA_DECORRENZA FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IndiceContratto & ") /* DATA_COSTITUZIONE */, " _
                                    & " '" & Format(Now, "yyyyMMdd") & "' /* DATA_RESTITUZIONE */, " _
                                    & " 1/* FL_ORIGINALE */, " _
                                    & " SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL /* ID */, " _
                                    & " " & indiceAnagrOLD & " /* ID_ANAGRAFICA */, " _
                                    & " NULL /* ID_BOLLETTA */, " _
                                    & " " & IndiceContratto & " /* ID_CONTRATTO */, " _
                                    & " " & par.VirgoleInPunti(importo * -1) & " /* IMPORTO */, " _
                                    & " " & par.VirgoleInPunti(importo * -1) & " /* IMPORTO_RESTITUZIONE */, " _
                                    & " NULL /* NOTE */ ) "
                                par.cmd.ExecuteNonQuery()
                            End If
                            lett.Close()

                            'IMPOSTO IL FLAG "FL_CAUZ_RESTITUITA" UGUALE A 1 NELLA TABELLA CHE SI CREA PER I CAMBI DELLE TIPOLOGIE CONTRATTUALI
                            par.cmd.CommandText = "update siscom_mi.GESTIONE_CAMBIO_TIPO_CONTR set FL_CAUZ_RESTITUITA =1 where id_contratto_origine=" & IndiceContratto
                            par.cmd.ExecuteNonQuery()
                        End If
                        myReaderA.Close()

                    End If
                    myReaderS.Close()

                    'End If
                    'myReaderX.Close()

                End If
            End If
            myReaderR.Close()
        End If



    End Sub

    Private Sub CreaImpostaRegistro()

        Dim idAnagrafica As Long = 0
        Dim Nominativo As String = ""
        Dim IDUNITA As Long = 0
        Dim Nome1 As String = ""
        Dim dataDecorrenza As String = ""
        Dim tipo_cor As String = ""
        Dim via_cor As String = ""
        Dim cap_cor As String = ""
        Dim luogo_cor As String = ""
        Dim civico_cor As String = ""
        Dim sigla_cor As String = ""
        Dim IDComplesso As Long = 0
        Dim IDEdificio As Long = 0
        Dim impostaDiRegistro As Decimal = 0
        Dim ID_BOLLETTA As Long = 0
        Dim iGiorniScad As Double = 10

        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=7"
        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderJ.Read Then
            iGiorniScad = CDbl(par.PuntiInVirgole(myReaderJ("VALORE")))
        End If
        myReaderJ.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND ID_CONTRATTO=" & IndiceContratto
        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader0.Read Then
            idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
            Nominativo = par.IfNull(myReader0("RAGIONE_SOCIALE"), "")
            'Nome = par.IfNull(myReader0("nome"), "")
        End If
        myReader0.Close()

        par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & IndiceContratto & ""
        myReader0 = par.cmd.ExecuteReader()
        If myReader0.Read Then
            IDUNITA = par.IfNull(myReader0("ID_UNITA"), 0)
        End If
        myReader0.Close()

        Dim DataScadenza As String = DateAdd("d", iGiorniScad, Now)

        par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & IndiceContratto & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderS.Read Then
            dataDecorrenza = par.IfNull(myReaderS("DATA_DECORRENZA"), 0)
            Nome1 = Nominativo
            'Nome2 = par.IfNull(myReaderS("PRESSO_COR"), "")
            tipo_cor = par.IfNull(myReaderS("TIPO_COR"), "")
            via_cor = par.IfNull(myReaderS("VIA_COR"), "")
            cap_cor = par.IfNull(myReaderS("CAP_COR"), "")
            luogo_cor = par.IfNull(myReaderS("LUOGO_COR"), "")
            sigla_cor = par.IfNull(myReaderS("SIGLA_COR"), "")
            civico_cor = par.IfNull(myReaderS("CIVICO_COR"), "")
            IDComplesso = par.IfNull(myReaderS("ID_COMPLESSO"), 0)
            IDEdificio = par.IfNull(myReaderS("ID_EDIFICIO"), 0)
        End If
        myReaderS.Close()

        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=1"
        myReaderJ = par.cmd.ExecuteReader()
        If myReaderJ.Read Then
            impostaDiRegistro = CDec(par.PuntiInVirgole(myReaderJ("VALORE")))
        End If
        myReaderJ.Close()

        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
            & "INDIRIZZO, CAP_CITTA, RIFERIMENTO_DA, RIFERIMENTO_A, " _
            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
            & "Values " _
            & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
            & "', '" & Format(CDate(DataScadenza), "yyyyMMdd") & "', NULL,NULL,NULL,'IMPOSTA REGISTRO PER CESSIONE'," _
            & "" & IndiceContratto _
            & " ," & par.RicavaEsercizioCorrente & ", " _
            & IDUNITA _
            & ", '0', '', " & idAnagrafica _
            & ", '" & par.PulisciStrSql(Nome1) & "', " _
            & "'" & par.PulisciStrSql(par.IfNull(tipo_cor, "") & " " & par.IfNull(via_cor, "") & ", " & par.PulisciStrSql(par.IfNull(civico_cor, ""))) _
            & "', '" & par.PulisciStrSql(par.IfNull(cap_cor, "") & " " & par.PulisciStrSql(par.IfNull(luogo_cor, "")) & "(" & par.IfNull(sigla_cor, "") & ")") _
            & "', '" & Format(Now, "yyyyMMdd") _
            & "', '" & Format(Now, "yyyyMMdd") & "', " _
            & "'0', " & IDComplesso & ", '', NULL, '', " _
            & Year(Now) & ", '', " & IDEdificio & ", NULL, NULL,'MAV',1)"
        par.cmd.ExecuteNonQuery()


        par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
        Dim myReaderVoceReg As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderVoceReg.Read Then
            ID_BOLLETTA = myReaderVoceReg(0)
        End If
        myReaderVoceReg.Close()

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",93" _
                & "," & par.VirgoleInPunti(impostaDiRegistro) & ")"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                      & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                      & "'F08','CREAZIONE NUOVA IMPOSTA DI REGISTRO IN SEGUITO A VaIn')"
        par.cmd.ExecuteNonQuery()

    End Sub

    Private Sub IntegraNewDeposito()

        Dim importoCauzione As Decimal = 0
        Dim iGiorniScad As Double = 10
        Dim idAnagrafica As Long = 0
        Dim Nominativo As String = ""
        Dim Nome As String = ""
        Dim IDUNITA As Long = 0
        Dim Nome1 As String = ""
        Dim Nome2 As String = ""
        Dim ID_BOLLETTA As Long = 0
        Dim ApplicabolloBolletta As Decimal = 0
        Dim bolloBolletta As Decimal = 0
        Dim impostaDiRegistro As Decimal = 0
        Dim idBollettaOLD As String = "NULL"
        Dim DEPOSITO As Double = 0

        importoCauzione = Format((canoneNuovo.Value / 12) * 3, "0.00")

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE (ID, ID_ANAGRAFICA, ID_CONTRATTO, DATA, IMPORTO,ID_BOLLETTA) VALUES (SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL," & indiceAnagr & "," & IndiceContratto & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importoCauzione) & ",null)"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "select SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.CURRVAL FROM DUAL"
        Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderX.Read Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV (ID_CONTRATTO,ID_STORICO_DEP,LIBRO,BOLLA,PROVENIENZA) VALUES (" & IndiceContratto & "," & myReaderX(0) & ",NULL,NULL,1)"
            par.cmd.ExecuteNonQuery()
        End If
        myReaderX.Close()

        par.cmd.CommandText = "select IMP_DEPOSITO_CAUZ FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & IndiceContratto
        myReaderX = par.cmd.ExecuteReader()
        If myReaderX.Read Then
            DEPOSITO = par.IfNull(myReaderX(0), 0)
        End If
        myReaderX.Close()

        'AGGIORNO DEPOSITO
        par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(importoCauzione + DEPOSITO) & " WHERE ID=" & IndiceContratto
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                       & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                       & "'F08','INTEGRATO DEPOSITO CAUZIONALE DI EURO " & importoCauzione & " IN SEGUITO A VaIn')"
        par.cmd.ExecuteNonQuery()




    End Sub

    Private Sub EmissioneNewDeposito()
        'ImportoCauzione = Format(Format((CanoneCorrente / 12), "0.00") * 3, "0.00")
        Dim importoCauzione As Decimal = 0
        Dim iGiorniScad As Double = 10
        Dim idAnagrafica As Long = 0
        Dim Nominativo As String = ""
        Dim Nome As String = ""
        Dim IDUNITA As Long = 0
        Dim Nome1 As String = ""
        Dim Nome2 As String = ""
        Dim ID_BOLLETTA As Long = 0
        Dim ApplicabolloBolletta As Decimal = 0
        Dim bolloBolletta As Decimal = 0
        Dim impostaDiRegistro As Decimal = 0
        Dim idBollettaOLD As String = "NULL"

        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=7"
        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderJ.Read Then
            iGiorniScad = CDbl(par.PuntiInVirgole(myReaderJ("VALORE")))
        End If
        myReaderJ.Close()

        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=0"
        myReaderJ = par.cmd.ExecuteReader()
        If myReaderJ.Read Then
            bolloBolletta = CDec(par.PuntiInVirgole(myReaderJ("VALORE")))
        End If
        myReaderJ.Close()

        par.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=25"
        myReaderJ = par.cmd.ExecuteReader()
        If myReaderJ.Read Then
            ApplicabolloBolletta = CDec(par.PuntiInVirgole(myReaderJ("VALORE")))
        End If
        myReaderJ.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_TIPO=9 AND FL_ANNULLATA=0 AND ID_BOLLETTA_STORNO IS NULL AND ID_CONTRATTO=" & IndiceContratto & " ORDER BY ID DESC"
        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderB.Read Then
            idBollettaOLD = par.IfNull(myReaderB("ID"), "NULL")
        End If
        myReaderB.Close()

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI,SISCOM_MI.ANAGRAFICA WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA=ANAGRAFICA.ID AND ID_CONTRATTO=" & IndiceContratto
        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader0.Read Then
            idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
            Nominativo = par.IfNull(myReader0("RAGIONE_SOCIALE"), "")
            'Nome = par.IfNull(myReader0("nome"), "")
        End If
        myReader0.Close()

        par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & IndiceContratto & ""
        myReader0 = par.cmd.ExecuteReader()
        If myReader0.Read Then
            IDUNITA = par.IfNull(myReader0("ID_UNITA"), 0)
        End If
        myReader0.Close()

        Dim DataScadenza As String = DateAdd("d", iGiorniScad, Now)

        importoCauzione = Format((canoneNuovo.Value / 12) * 3, "0.00")

        Dim depositoOld As Decimal = 0
        Dim dataDecorrenza As String = ""
        Dim tipo_cor As String = ""
        Dim via_cor As String = ""
        Dim cap_cor As String = ""
        Dim luogo_cor As String = ""
        Dim civico_cor As String = ""
        Dim sigla_cor As String = ""
        Dim IDComplesso As Long = 0
        Dim IDEdificio As Long = 0

        par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & IndiceContratto & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderS.Read Then
            depositoOld = par.IfNull(myReaderS("IMP_DEPOSITO_CAUZ"), 0)
            dataDecorrenza = par.IfNull(myReaderS("DATA_DECORRENZA"), 0)
            Nome1 = Nominativo
            'Nome2 = par.IfNull(myReaderS("PRESSO_COR"), "")
            tipo_cor = par.IfNull(myReaderS("TIPO_COR"), "")
            via_cor = par.IfNull(myReaderS("VIA_COR"), "")
            cap_cor = par.IfNull(myReaderS("CAP_COR"), "")
            luogo_cor = par.IfNull(myReaderS("LUOGO_COR"), "")
            sigla_cor = par.IfNull(myReaderS("SIGLA_COR"), "")
            civico_cor = par.IfNull(myReaderS("CIVICO_COR"), "")
            IDComplesso = par.IfNull(myReaderS("ID_COMPLESSO"), 0)
            IDEdificio = par.IfNull(myReaderS("ID_EDIFICIO"), 0)
        End If
        myReaderS.Close()

        'NUOVO DEPOSITO CAUZIONALE
        par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
            & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
            & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
            & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
            & "INDIRIZZO, CAP_CITTA, RIFERIMENTO_DA, RIFERIMENTO_A, " _
            & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
            & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
            & "Values " _
            & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
            & "', '" & Format(CDate(DataScadenza), "yyyyMMdd") & "', NULL,NULL,NULL,'DEPOSITO CAUZIONALE'," _
            & "" & IndiceContratto _
            & " ," & par.RicavaEsercizioCorrente & ", " _
            & IDUNITA _
            & ", '0', '', " & idAnagrafica _
            & ", '" & par.PulisciStrSql(Nome1) & "', " _
            & "'" & par.PulisciStrSql(par.IfNull(tipo_cor, "") & " " & par.IfNull(via_cor, "") & ", " & par.PulisciStrSql(par.IfNull(civico_cor, ""))) _
            & "', '" & par.PulisciStrSql(par.IfNull(cap_cor, "") & " " & par.PulisciStrSql(par.IfNull(luogo_cor, "")) & "(" & par.IfNull(sigla_cor, "") & ")") _
            & "', '" & Format(Now, "yyyyMMdd") _
            & "', '" & Format(Now, "yyyyMMdd") & "', " _
            & "'0', " & IDComplesso & ", '', NULL, '', " _
            & Year(Now) & ", '', " & IDEdificio & ", NULL, NULL,'MAV',9)"
        par.cmd.ExecuteNonQuery()


        par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
        Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderX.Read Then
            ID_BOLLETTA = myReaderX(0)
        End If
        myReaderX.Close()

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",7" _
                                    & "," & par.VirgoleInPunti(importoCauzione) & ")"
        par.cmd.ExecuteNonQuery()

        If par.VirgoleInPunti(importoCauzione) > ApplicabolloBolletta Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                           & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",96" _
                                           & "," & par.VirgoleInPunti(Format(bolloBolletta, "0.00")) & ")"
            par.cmd.ExecuteNonQuery()
        End If
        'FINE NUOVO DEPOSITO CAUZIONALE

        'SCRIVO STORICO DEPOSITO
        'par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE (ID, ID_ANAGRAFICA, ID_CONTRATTO, DATA, IMPORTO,ID_BOLLETTA) VALUES (SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL," & indiceAnagrOLD & "," & IndiceContratto & ",'" & dataDecorrenza & "'," & par.VirgoleInPunti(depositoOld) & "," & idBollettaOLD & ")"
        'par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE (ID, ID_ANAGRAFICA, ID_CONTRATTO, DATA, IMPORTO,ID_BOLLETTA,DATA_COSTITUZIONE,FL_ORIGINALE) VALUES (SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL," & indiceAnagr & "," & IndiceContratto & ",'" & Format(Now, "yyyyMMdd") & "'," & par.VirgoleInPunti(importoCauzione) & "," & ID_BOLLETTA & ",'" & Format(Now, "yyyyMMdd") & "',1)"
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "select SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.CURRVAL FROM DUAL"
        myReaderX = par.cmd.ExecuteReader()
        If myReaderX.Read Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV (ID_CONTRATTO,ID_STORICO_DEP,LIBRO,BOLLA,PROVENIENZA) VALUES (" & IndiceContratto & "," & myReaderX(0) & ",NULL,NULL,1)"
            par.cmd.ExecuteNonQuery()

        End If
        myReaderX.Close()

        'AGGIORNO DEPOSITO
        par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(importoCauzione) & " WHERE ID=" & IndiceContratto
        par.cmd.ExecuteNonQuery()

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                       & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                       & "'F08','NUOVO DEPOSITO CAUZIONALE IN SEGUITO A VaIn')"
        par.cmd.ExecuteNonQuery()

    End Sub


    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            lblErroreCF.Visible = True
            txtCF.Text = ""
            lblErrore.Visible = True
            lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
        Else
            lblErroreCF.Visible = False
            If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                CompletaDati(UCase(txtCF.Text))
                lblErrore.Visible = False
            Else
                txtCF.Text = ""
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
            End If
        End If
    End Sub

    Private Function CompletaDati(ByVal CF As String)

        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(CF, 12, 4) & "'"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read() Then

            If par.IfNull(myReader1("SIGLA"), " ") <> "E" And par.IfNull(myReader1("SIGLA"), " ") <> "C" Then
                Me.CmbComune.SelectedIndex = -1
                Me.CmbComune.SelectedValue = par.IfNull(myReader1("COD"), " ")

                cmbCittadinanza.SelectedIndex = -1
                cmbCittadinanza.SelectedItem.Text = "ITALIA"
            Else
                cmbCittadinanza.SelectedIndex = -1
                cmbCittadinanza.SelectedValue = par.IfNull(myReader1("COD"), " ")
            End If



            Dim MIADATA As String
            txtDataNascita.Text = ""
            If Val(Mid(CF, 10, 2)) > 40 Then
                MIADATA = Format(Val(Mid(CF, 10, 2)) - 40, "00")
            Else
                MIADATA = Mid(CF, 10, 2)
            End If

            Select Case Mid(CF, 9, 1)
                Case "A"
                    MIADATA = MIADATA & "/01"
                Case "B"
                    MIADATA = MIADATA & "/02"
                Case "C"
                    MIADATA = MIADATA & "/03"
                Case "D"
                    MIADATA = MIADATA & "/04"
                Case "E"
                    MIADATA = MIADATA & "/05"
                Case "H"
                    MIADATA = MIADATA & "/06"
                Case "L"
                    MIADATA = MIADATA & "/07"
                Case "M"
                    MIADATA = MIADATA & "/08"
                Case "P"
                    MIADATA = MIADATA & "/09"
                Case "R"
                    MIADATA = MIADATA & "/10"
                Case "S"
                    MIADATA = MIADATA & "/11"
                Case "T"
                    MIADATA = MIADATA & "/12"
            End Select
            If Mid(CF, 7, 1) = "0" Then
                MIADATA = MIADATA & "/200" & Mid(CF, 8, 1)
                If Format(CDate(MIADATA), "yyyyMMdd") > Format(Now, "yyyyMMdd") Then
                    MIADATA = Mid(MIADATA, 1, 6) & "19" & Mid(MIADATA, 9, 2)
                End If
            Else
                MIADATA = MIADATA & "/19" & Mid(CF, 7, 2)
            End If
            txtDataNascita.Text = MIADATA
            Me.cmbSesso.SelectedIndex = -1
            If Mid(CF, 10, 2) > 40 Then
                Me.cmbSesso.SelectedValue = "F"
            Else
                Me.cmbSesso.SelectedValue = "M"

            End If
        End If
        myReader1.Close()

        'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE (RAGIONE_SOCIALE IS NOT NULL) AND UPPER(COD_FISCALE)='" & CF & "'"
        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE RAGIONE_SOCIALE='" & UCase(par.PulisciStrSql(txtRagione.Text)) & "' AND UPPER(COD_FISCALE)='" & CF & "'"

        myReader1 = par.cmd.ExecuteReader
        If myReader1.Read Then
            txtNumDoc.Text = par.IfNull(myReader1("NUM_DOC"), "")
            txtDataDoc.Text = par.FormattaData(par.IfNull(myReader1("DATA_DOC"), ""))
            txtDocRilasciato.Text = par.IfNull(myReader1("RILASCIO_DOC"), "")
            txtSoggiorno.Text = par.IfNull(myReader1("DOC_SOGGIORNO"), "")
            If par.IfNull(myReader1("COMUNE_RESIDENZA_r"), "") <> "" Then
                txtComuneResidenza0.Text = par.IfNull(myReader1("COMUNE_RESIDENZA_r"), "")
                txtProvinciaResidenza0.Text = par.IfNull(myReader1("PROVINCIA_RESIDENZA_r"), "")
                txtIndirizzoResidenza0.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA_r"), "")
                txtCivicoResidenza0.Text = par.IfNull(myReader1("CIVICO_RESIDENZA_r"), "")
                txtCAP0.Text = par.IfNull(myReader1("CAP_RESIDENZA_r"), "")
                txtTel0.Text = par.IfNull(myReader1("TELEFONO_R"), "")
            Else
                txtComuneResidenza0.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
                txtProvinciaResidenza0.Text = par.IfNull(myReader1("PROVINCIA_RESIDENZA"), "")
                txtIndirizzoResidenza0.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
                txtCivicoResidenza0.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
                txtCAP0.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
                txtTel0.Text = par.IfNull(myReader1("TELEFONO"), "")
            End If
        End If
        myReader1.Close()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Function

    Protected Sub chProcuratore_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chProcuratore.CheckedChanged
        txtNumProcura.Visible = True
        txtDataProcura.Visible = True
        lblDataProcura.Visible = True
        lblNumeroProcura.Visible = True
    End Sub

    Protected Sub chLegale_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chLegale.CheckedChanged
        txtNumProcura.Visible = False
        txtDataProcura.Visible = False
        txtDataProcura.Text = ""
        txtNumProcura.Text = ""
        lblDataProcura.Visible = False
        lblNumeroProcura.Visible = False
    End Sub

    Protected Sub txtPIVA_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPIva.TextChanged
        par.OracleConn.Open()
        par.SettaCommand(par)

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(partita_iva)='" & UCase(txtPIva.Text) & "'"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then

            txtComuneResidenza.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
            txtProvinciaResidenza.Text = par.IfNull(myReader1("PROVINCIA_RESIDENZA"), "")
            txtIndirizzoResidenza.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
            txtCivicoResidenza.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
            txtCAP.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
            txtTel.Text = par.IfNull(myReader1("TELEFONO"), "")

            If txtRagione.Text = "" Then
                txtRagione.Text = par.IfNull(myReader1("ragione_sociale"), "")
            End If

            If par.IfNull(myReader1("tipo_r"), "0") = "0" Then
                chProcuratore.Checked = False
                chLegale.Checked = True
            Else
                chLegale.Checked = False
                chProcuratore.Checked = True
                txtNumProcura.Visible = True
                txtDataProcura.Visible = True
            End If
            txtNumProcura.Text = par.IfNull(myReader1("num_procura"), "")
            txtDataProcura.Text = par.FormattaData(par.IfNull(myReader1("DATA_PROCURA"), ""))

            txtCognome.Text = par.IfNull(myReader1("COGNOME"), "")
            txtNome.Text = par.IfNull(myReader1("NOME"), "")
            txtCF.Text = par.IfNull(myReader1("COD_FISCALE"), "")

            CompletaDati(txtCF.Text)


        End If
        myReader1.Close()

        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
End Class
