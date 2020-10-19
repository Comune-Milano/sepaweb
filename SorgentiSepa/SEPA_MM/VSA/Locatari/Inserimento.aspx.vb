
Partial Class Contratti_Anagrafica_Inserimento
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If Request.QueryString("C") = "RisultatiRicerca" Then
            Session.Add("IDANA", "")
            Response.Redirect(Request.QueryString("C") & ".aspx?COGNOME=" & Request.QueryString("COGNOME") & "&NOME=" & Request.QueryString("NOME") & "&RAGSOCIALE=" & Request.QueryString("RAGSOCIALE") & "&CF=" & Request.QueryString("CF") & "&PIVA=" & Request.QueryString("CF"))
        Else
            If Session.Item("CONTRATTOAPERTO") = "0" Then
                Response.Redirect("Ricerca.aspx")
            Else
                Session.Add("IDANA", "")
                Response.Write("<script>window.close();</script>")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                '********CONNESSIONE E APERTURA TRANSAZIONE*********
                'par.OracleConn.Open()
                'par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
                'par.myTrans = par.OracleConn.BeginTransaction()
                '‘‘par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
                '********CONNESSIONE E APERTURA TRANSAZIONE*********

                '*****CARICAMENTO COMBO E DATI UTILI******
                'par.cmd.CommandText = "SELECT COD, NOME FROM SEPA.COMUNI_NAZIONI"
                'myReader = par.cmd.ExecuteReader()
                'CmbComune.Items.Add(New ListItem(" ", -1))

                'While myReader.Read
                '    CmbComune.Items.Add(New ListItem(par.IfNull(myReader("NOME"), " "), par.IfNull(myReader("COD"), -1)))
                'End While
                'myReader.Close()
                DAC.Value = Request.QueryString("DAC")

                par.RiempiDListConVuoto(Me, par.OracleConn, "CmbComune", "SELECT * FROM comuni_nazioni WHERE SIGLA<>'C' AND SIGLA<>'E' order by nome asc", "NOME", "COD")

                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni WHERE SIGLA='C' OR SIGLA='E' OR SIGLA='I'  order by nome asc", "NOME", "COD")
                'cmbCittadinanza.Items.FindByText("ITALIA").Selected = True

                'par.cmd.CommandText = "SELECT * FROM TIPI_INDIRIZZO"
                'myReader = par.cmd.ExecuteReader()
                'cmbTipoVia.Items.Add(New ListItem(" ", -1))
                'While myReader.Read
                '    cmbTipoVia.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                'End While
                'myReader.Close()
                'par.OracleConn.Close()
                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'txtCittadinanza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'txtResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")


                txtComuneResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtProvinciaResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtIndirizzoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCivicoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtCAP.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPIva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNumDoc.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'txtDataDoc.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtDocRilasciato.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtDataNascita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataDoc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                txtSoggiorno.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                '*****CARICAMENTO COMBO E DATI UTILI******
                If Request.QueryString("ID") <> "" And Request.QueryString("ID") <> 0 Then
                    IdSoggetto = Request.QueryString("ID")
                    Apriricerca()
                    Ricerca = "True"
                Else
                    Ricerca = "False"
                End If
                '
            End If

            nuovocomp.Value = Request.QueryString("INS")
            abusivo.value = Request.QueryString("ABS")

            If nuovocomp.value = "1" Then
                'If Session.Item("CONTRATTOAPERTO") = "0" Then
                '    Me.btnSalva.Visible = True
                '    Me.btnInserisciComponente.Visible = False
                'Else
                Me.btnSalva.Visible = False
                'Me.btnInserisciComponente.Visible = True
                'End If
            Else
            'If Session.Item("CONTRATTOAPERTO") = "1" Then
            '    Me.txtCognome.Enabled = False
            '    Me.txtNome.Enabled = False
            '    Me.txtRagione.Enabled = False
            '    Me.txtCF.Enabled = False
            '    Me.txtPIva.Enabled = False
            '    cmbCittadinanza.Enabled = False
            '    'Me.txtResidenza.Enabled = False
            '    txtComuneResidenza.Enabled = False
            '    txtProvinciaResidenza.Enabled = False
            '    txtIndirizzoResidenza.Enabled = False
            '    txtCivicoResidenza.Enabled = False
            '    txtCAP.Enabled = False
            '    Me.txtDataNascita.Enabled = False
            '    CmbComune.Enabled = False
            '    cmbSesso.Enabled = False
            '    'Me.txtTel.Enabled = False
            '    cmbTipoDoc.Enabled = False
            '    txtNumDoc.Enabled = False
            '    txtDataDoc.Enabled = False
            '    txtDocRilasciato.Enabled = False
            '    txtSoggiorno.Enabled = False
            '    btnSalva.Visible = False
            'End If

            If DAC.Value = "1" Then
                Me.txtCognome.Enabled = False
                Me.txtNome.Enabled = False
                Me.txtRagione.Enabled = True
                Me.txtCF.Enabled = False
                Me.txtPIva.Enabled = True


                cmbCittadinanza.Enabled = False
                'Me.txtResidenza.Enabled = False
                txtComuneResidenza.Enabled = True
                txtProvinciaResidenza.Enabled = True
                txtIndirizzoResidenza.Enabled = True
                txtCivicoResidenza.Enabled = True
                txtCAP.Enabled = True
                Me.txtDataNascita.Enabled = False
                CmbComune.Enabled = False
                cmbSesso.Enabled = False
                'Me.txtTel.Enabled = False
                cmbTipoDoc.Enabled = True
                txtNumDoc.Enabled = True
                txtDataDoc.Enabled = True
                txtDocRilasciato.Enabled = True
                txtSoggiorno.Enabled = True
                    'btnInserisciComponente.Visible = False
                btnSalva.Visible = True

            End If

            If Request.QueryString("LT") = "1" Then

                Me.txtCognome.Enabled = False
                Me.txtNome.Enabled = False
                Me.txtRagione.Enabled = False
                Me.txtCF.Enabled = False
                Me.txtPIva.Enabled = False
                cmbCittadinanza.Enabled = False
                'Me.txtResidenza.Enabled = False
                txtComuneResidenza.Enabled = False
                txtProvinciaResidenza.Enabled = False
                txtIndirizzoResidenza.Enabled = False
                txtCivicoResidenza.Enabled = False
                txtCAP.Enabled = False
                Me.txtDataNascita.Enabled = False
                CmbComune.Enabled = False
                cmbSesso.Enabled = False
                'Me.txtTel.Enabled = False
                cmbTipoDoc.Enabled = False
                txtNumDoc.Enabled = False
                txtDataDoc.Enabled = False
                txtDocRilasciato.Enabled = False
                txtSoggiorno.Enabled = False

                txtTel.Enabled = False
                btnSalva.Visible = False

            End If
            End If




            'If Ricerca = "True" Then
            '    Me.btnInserisciComponente.Visible = True
            'End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            'par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub
    Private Sub Apriricerca()
        Try
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            '********CONNESSIONE E APERTURA TRANSAZIONE*********
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            End If
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans
            'HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            '********CONNESSIONE E APERTURA TRANSAZIONE*********
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE ID = " & Request.QueryString("ID")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Me.txtCognome.Text = par.IfNull(myReader("COGNOME"), "")
                Me.txtNome.Text = par.IfNull(myReader("NOME"), "")
                Me.txtRagione.Text = par.IfNull(myReader("RAGIONE_SOCIALE"), "")
                Me.txtCF.Text = par.IfNull(myReader("COD_FISCALE"), "")
                Me.txtPIva.Text = par.IfNull(myReader("PARTITA_IVA"), "")


                cmbCittadinanza.SelectedIndex = -1
                cmbCittadinanza.Items.FindByText(par.IfNull(myReader("CITTADINANZA"), " ")).Selected = True

                'Me.txtResidenza.Text = par.IfNull(myReader("RESIDENZA"), "")
                txtComuneResidenza.Text = par.IfNull(myReader("comune_RESIDENZA"), "")
                txtProvinciaResidenza.Text = par.IfNull(myReader("provincia_RESIDENZA"), "")
                txtIndirizzoResidenza.Text = par.IfNull(myReader("indirizzo_RESIDENZA"), "")
                txtCivicoResidenza.Text = par.IfNull(myReader("civico_RESIDENZA"), "")
                txtCAP.Text = par.IfNull(myReader("CAP_RESIDENZA"), "")


                Me.txtDataNascita.Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))

                If par.IfNull(myReader("CITTADINANZA"), " ") = "ITALIA" Then
                    CmbComune.SelectedIndex = -1
                    CmbComune.Items.FindByValue(par.IfNull(myReader("COD_COMUNE_NASCITA"), "-1")).Selected = True
                End If
                cmbSesso.SelectedIndex = -1
                Me.cmbSesso.Items.FindByText(par.IfNull(myReader("SESSO"), "M")).Selected = True
                'Me.cmbSesso.SelectedValue = par.IfNull(myReader("SESSO"), "")

                Me.txtTel.Text = par.IfNull(myReader("TELEFONO"), "")

                cmbTipoDoc.SelectedIndex = -1
                cmbTipoDoc.Items.FindByValue(par.IfNull(myReader("TIPO_DOC"), "0")).Selected = True

                txtNumDoc.Text = par.IfNull(myReader("NUM_DOC"), "")
                txtDataDoc.Text = par.FormattaData(par.IfNull(myReader("DATA_DOC"), ""))
                txtDocRilasciato.Text = par.IfNull(myReader("RILASCIO_DOC"), "")

                txtSoggiorno.Text = par.IfNull(myReader("DOC_SOGGIORNO"), "")

                '    If par.IfNull(myReader("ID_INDIRIZZO_RECAPITO"), "") <> "" Then

                '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INDIRIZZI WHERE ID = " & par.IfNull(myReader("ID_INDIRIZZO_RECAPITO"), "")

                '    End If
                '    myReader.Close()
                '    myReader = par.cmd.ExecuteReader()
                '    If myReader.Read Then
                '        Me.txtRecapito.Text = par.IfNull(myReader("DESCRIZIONE"), "")
                '        Me.txtCivico.Text = par.IfNull(myReader("CIVICO"), "")
                '        Me.txtCap.Text = par.IfNull(myReader("CAP"), "")
                '        Me.TxtLocalità.Text = par.IfNull(myReader("LOCALITA"), "")
                '        Me.cmbComuneResid.SelectedValue = par.IfNull(myReader("COD_COMUNE"), "")

                '    End If
                '    myReader.Close()

            End If
            myReader.Close()

            'par.cmd.CommandText = "SELECT id_contratto FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_ANAGRAFICA = " & Request.QueryString("ID")
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    'Me.btnSalva.Visible = False
            '    DisattivaCampi()


            '    'Me.btnSalva.ToolTip = "Impossibile modificare i dati"
            'End If
            If Session.Item("CONTRATTOAPERTO") = "0" Then
                'DisattivaCampi()
                Me.btnSalva.Visible = True
                'Me.btnInserisciComponente.Visible = False
            Else
                Me.btnSalva.Visible = False
                'Me.btnInserisciComponente.Visible = True
            End If

            Me.Ricerca = "True"
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message

        End Try
    End Sub
    Private Sub DisattivaCampi()
        Try
            Me.txtCognome.Enabled = False
            Me.txtNome.Enabled = False
            Me.txtCF.Enabled = False
            Me.txtRagione.Enabled = False
            Me.txtPIva.Enabled = False
            Me.cmbSesso.Enabled = False
            Me.txtDataNascita.Enabled = False
            Me.cmbCittadinanza.Enabled = False
            Me.CmbComune.Enabled = False
            'Me.txtResidenza.Enabled = False
            txtComuneResidenza.Enabled = False
            txtProvinciaResidenza.Enabled = False
            txtIndirizzoResidenza.Enabled = False
            txtCivicoResidenza.Enabled = False
            txtCAP.Enabled = False

            txtSoggiorno.Enabled = False
            'cmbTipoDoc.Enabled = False
            'txtNumDoc.Enabled = False
            'txtDocRilasciato.Enabled = False
            'txtDataDoc.Enabled = False


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        SalvaDati()
    End Sub

    Private Function SalvaDati()
        If txtCF.Text <> "" Then
            If par.ControllaCF(txtCF.Text) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Codice Fiscale Errato!"

                Exit Function
            End If
            If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Codice Fiscale Errato!"

                Exit Function
            End If

            If par.RicavaSesso(txtCF.Text) <> cmbSesso.SelectedItem.Text Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Il sesso del contraente non corrisponde a quanto dichiarato nel codice fiscale!"
                Exit Function
            End If

            If txtComuneResidenza.Text = "" Or txtProvinciaResidenza.Text = "" Or txtIndirizzoResidenza.Text = "" Or txtCivicoResidenza.Text = "" Or txtCAP.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Valorizzare tutti i campi relativi alla residenza!"
                Exit Function
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
                Exit Function
            End If
            'End If

            If Mid(txtCF.Text, 12, 1) <> "Z" Then
                If Mid(txtCF.Text, 12, 4) <> CmbComune.SelectedItem.Value Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Il Comune di nascita del contraente non corrisponde a quanto dichiarato nel codice fiscale!"
                    Exit Function
                End If
            End If

        End If

        If txtTel.Text <> "" Then
            If IsNumeric(txtTel.Text) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Il numero di telefono deve essere un valore numerico!"
                Exit Function
            End If
        End If

        If PIVA.Value = "1" Then
            Exit Function
        End If

        If abusivo.Value <> "1" Then
            If txtDataNascita.Text <> "" Then
                If par.AggiustaData(txtDataNascita.Text) > par.AggiustaData(txtDataDoc.Text) Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere successiva alla data di nascita!"
                End If
            End If

            If txtDataDoc.Text <> "" Then
                If par.AggiustaData(txtDataDoc.Text) > Format(Now, "yyyyMMdd") Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere precedente alla data odierna!"
                End If
            End If
        End If

        If Me.Ricerca <> "True" Then
            Salva()
            'Ricerca = "True"
        Else
            Update()

        End If
    End Function

    Private Sub Salva()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            If (par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCF.Text, "Null") <> "Null") Or (par.IfEmpty(Me.txtRagione.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtPIva.Text, "Null") <> "Null") Then
                If par.IfEmpty(txtDataDoc.Text, "") <> "" Then
                    If par.AggiustaData(txtDataDoc.Text) < par.AggiustaData(txtDataNascita.Text) Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! La data di rilascio del documento non può essere precedente alla data di nascita!"
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If
                Me.lblErrore.Visible = False

                If txtCF.Text <> "" And txtPIva.Text <> "" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE COD_FISCALE='" & txtCF.Text & "' AND PARTITA_IVA='" & txtPIva.Text & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If nuovocomp.Value = "1" Then
                            If myReader.Read Then
                                IdSoggetto = myReader("id")
                            End If
                        Else
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! Questo nominativo esiste già nella banca dati. Ricercarlo e procedere con le eventuali modifiche!"
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        End If
                    End If
                    myReader.Close()
                End If

                If txtCF.Text <> "" And txtPIva.Text = "" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE COD_FISCALE='" & txtCF.Text & "' AND (PARTITA_IVA='' OR PARTITA_IVA IS NULL)"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If nuovocomp.Value = "1" Then
                            If myReader.Read Then
                                IdSoggetto = myReader("id")
                            End If
                        Else
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! Questo nominativo esiste già nella banca dati. Ricercarlo e procedere con le eventuali modifiche!"
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        End If

                    End If
                    myReader.Close()
                End If

                If txtCF.Text = "" And txtPIva.Text <> "" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE (COD_FISCALE='' OR COD_FISCALE IS NULL) AND PARTITA_IVA='" & txtPIva.Text & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If nuovocomp.Value = "1" Then
                            If myReader.Read Then
                                IdSoggetto = myReader("id")
                            End If
                        Else
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! Questo nominativo esiste già nella banca dati. Ricercarlo e procedere con le eventuali modifiche!"
                            myReader.Close()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        End If
                    End If
                    myReader.Close()
                End If

                If IdSoggetto = "" Then

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANAGRAFICA (ID, NOME, COGNOME, COD_FISCALE, PARTITA_IVA, RAGIONE_SOCIALE, CITTADINANZA, RESIDENZA, DATA_NASCITA, COD_COMUNE_NASCITA, SESSO, TELEFONO,TIPO_DOC,NUM_DOC,DATA_DOC,RILASCIO_DOC,comune_residenza,provincia_residenza,indirizzo_residenza,civico_residenza,DOC_SOGGIORNO,CAP_RESIDENZA)" _
                    & "VALUES(SISCOM_MI.SEQ_ANAGRAFICA.NEXTVAL,'" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) & "','" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) & "', '" & par.PulisciStrSql(Me.txtCF.Text) & "', '" & par.PulisciStrSql(LTrim(RTrim(Me.txtPIva.Text))) & "'" _
                    & ", '" & par.PulisciStrSql(LTrim(RTrim(Me.txtRagione.Text))) & "','" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) & "', '" & par.PulisciStrSql(txtComuneResidenza.Text & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & LTrim(RTrim(txtCivicoResidenza.Text))) & "', '" & par.AggiustaData(Me.txtDataNascita.Text) & "', '" & CmbComune.SelectedItem.Value & "'" _
                    & ",'" & Me.cmbSesso.SelectedItem.Text & "','" & par.PulisciStrSql(Me.txtTel.Text) & "'," & cmbTipoDoc.SelectedItem.Value & ",'" & par.PulisciStrSql(txtNumDoc.Text) & "','" & par.AggiustaData(txtDataDoc.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtDocRilasciato.Text))) _
                    & "','" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) & "','" & par.PulisciStrSql(txtProvinciaResidenza.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) & "','" & par.PulisciStrSql(txtCivicoResidenza.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtSoggiorno.Text))) & "','" & par.PulisciStrSql(txtCAP.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_ANAGRAFICA.CURRVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read() Then
                        IdSoggetto = myReader1(0)
                    End If
                    myReader1.Close()
                End If

                Ricerca = "True"
                'Response.Write("<script>alert('Dati salvati correttamente!')</script>")
                Response.Write("<script>window.opener.document.forms['form1'].elements['LBLintest'].value='" & IdSoggetto & "';window.opener.document.getElementById('btnAggComp').click();self.close();</script>")
            Else
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire i campi necessari alla definizione di una personafisica o giuridica!"
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")

            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub Update()
        Try
            If IdSoggetto = "" Then Exit Sub
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                If (par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCF.Text, "Null") <> "Null") Or (par.IfEmpty(Me.txtRagione.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtPIva.Text, "Null") <> "Null") Then
                    If par.IfEmpty(txtDataDoc.Text, "") <> "" Then
                        If par.AggiustaData(txtDataDoc.Text) < par.AggiustaData(txtDataNascita.Text) Then
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! La data di rilascio del documento non può essere precedente alla data di nascita!"
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        End If
                    End If

                    Me.lblErrore.Visible = False
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ANAGRAFICA SET NOME='" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) & "', COGNOME='" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) & "', COD_FISCALE='" & par.PulisciStrSql(Me.txtCF.Text) & "'" _
                    & ", PARTITA_IVA='" & par.PulisciStrSql(Me.txtPIva.Text) & "', RAGIONE_SOCIALE='" & par.PulisciStrSql(LTrim(RTrim(Me.txtRagione.Text))) & "',CITTADINANZA='" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) & "'" _
                    & ", RESIDENZA='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text)) & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) & "',DATA_NASCITA='" & par.AggiustaData(Me.txtDataNascita.Text) & "',COD_COMUNE_NASCITA= '" & CmbComune.SelectedItem.Value & "'" _
                    & ",SESSO='" & Me.cmbSesso.SelectedItem.Text & "',TELEFONO='" & par.PulisciStrSql(Me.txtTel.Text) _
                    & "',TIPO_DOC=" & cmbTipoDoc.SelectedItem.Value _
                    & ",NUM_DOC='" & par.PulisciStrSql(txtNumDoc.Text) _
                    & "',DATA_DOC='" & par.AggiustaData(txtDataDoc.Text) _
                    & "',RILASCIO_DOC='" & par.PulisciStrSql(LTrim(RTrim(txtDocRilasciato.Text))) _
                    & "',comune_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) _
                     & "',provincia_residenza='" & par.PulisciStrSql(txtProvinciaResidenza.Text) _
                      & "',indirizzo_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) _
                       & "',civico_residenza='" & par.PulisciStrSql(txtCivicoResidenza.Text) _
                       & "',DOC_SOGGIORNO='" & par.PulisciStrSql(txtSoggiorno.Text) _
                       & "',CAP_RESIDENZA='" & par.PulisciStrSql(txtCAP.Text) _
                    & "' WHERE ID=" & IdSoggetto
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Dati modificati correttamente!')</script>")

                    If DAC.Value = "1" Then
                        Response.Write("<script>self.close();</script>")
                    End If
                Else
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Inserire i campi necessari alla definizione di una personafisica o giuridica!"
                    Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")

                End If
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message
        End Try


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

            'Me.CmbComune.SelectedValue = par.IfNull(myReader1("COD"), " ")

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
            If Mid(CF, 10, 2) > 40 Then
                Me.cmbSesso.SelectedValue = "F"
            Else
                Me.cmbSesso.SelectedValue = "M"

            End If
        End If
        myReader1.Close()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Function
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Try
            Dim a As String
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If
            Return a
        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try

    End Function

    Private Property Ricerca() As String
        Get
            If Not (ViewState("par_Ricerca") Is Nothing) Then
                Return CStr(ViewState("par_Ricerca"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Ricerca") = value
        End Set

    End Property

    Private Property IdSoggetto() As String
        Get
            If Not (ViewState("par_IdSoggetto") Is Nothing) Then
                Return CStr(ViewState("par_IdSoggetto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdSoggetto") = value
        End Set

    End Property

   
End Class
