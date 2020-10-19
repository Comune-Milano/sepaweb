
Partial Class Contratti_CambioIntestazione
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then

                IndiceContratto = Request.QueryString("IDC")
                IDCONNESSIONE.Value = Request.QueryString("IDT")

                par.OracleConn.Open()
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbComune", "SELECT * FROM comuni_nazioni where sigla<>'E' AND SIGLA<>'C' order by nome asc", "NOME", "COD")
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni where SIGLA='C' OR sigla='E' OR SIGLA='I' order by nome asc", "NOME", "COD")
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtComuneResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtProvinciaResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtIndirizzoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCivicoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCAP.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPIva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtSoggiorno.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")


            End If

            txtDataNascita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDoc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")



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
            Dim indice As Long = 0
            Dim VECCHI_INT As String = ""
            Dim SSS As String = ""

            lblErrore.Visible = False

            If ins.Value <> "111" Then

                If txtCAP.Text <> "" Then
                    If txtCAP.Text = "" Or Len(txtCAP.Text) <> 5 Or IsNumeric(txtCAP.Text) = False Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il CAP deve essere di 5 caratteri numerici!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If

                If txtCognome.Text = "" Or txtNome.Text = "" Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Inserire un cognome/nome e specificare un codice fiscale valido! Se trattasi di persona giuridica, inserire gli estremi del legale rappresentante!"
                    'par.cmd.Dispose()
                    'par.OracleConn.Close()
                    'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If txtCognome.Text <> "" Or txtNome.Text <> "" Then
                    If par.ControllaCF(txtCF.Text) = False Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If

                    If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If

                    If par.RicavaSesso(txtCF.Text) <> cmbSesso.SelectedItem.Text Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il sesso del contraente non corrisponde a quanto dichiarato nel codice fiscale!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If

                    If txtComuneResidenza.Text = "" Or txtProvinciaResidenza.Text = "" Or txtIndirizzoResidenza.Text = "" Or txtCivicoResidenza.Text = "" Or txtCAP.Text = "" Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Valorizzare tutti i campi relativi alla residenza!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
                        lblErrore.Text = "Attenzione! La data di nascita non corrisponde a quanto dichiarato nel codice fiscale!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                    'End If

                    If Mid(txtCF.Text, 12, 1) <> "Z" Then
                        If Mid(txtCF.Text, 12, 4) <> CmbComune.SelectedItem.Value Then
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! Il Comune di nascita non corrisponde a quanto dichiarato nel codice fiscale!"
                            'par.cmd.Dispose()
                            'par.OracleConn.Close()
                            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Exit Sub
                        End If
                    End If

                End If

                If txtPIva.Text <> "" Then
                    If Len(txtPIva.Text) <> 11 Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Partita Iva non valida!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If

                If txtDataNascita.Text <> "" Then
                    If par.AggiustaData(txtDataNascita.Text) > par.AggiustaData(txtDataDoc.Text) Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere successiva alla data di nascita!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If

                If txtDataDoc.Text <> "" Then
                    If par.AggiustaData(txtDataDoc.Text) > Format(Now, "yyyyMMdd") Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere precedente o uguale alla data odierna!"
                        'par.cmd.Dispose()
                        'par.OracleConn.Close()
                        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If


                'If par.OracleConn.State = Data.ConnectionState.Closed Then
                '    par.OracleConn.Open()
                '    par.SettaCommand(par)
                'End If

                par.OracleConn = CType(HttpContext.Current.Session.Item(IDCONNESSIONE.Value), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IDCONNESSIONE.Value), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                If (lblErrore.Visible = False AndAlso par.IfEmpty(Me.txtDocRilasciato.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDataDoc.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNumDoc.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCF.Text, "Null") <> "Null") Then
                    Me.lblErrore.Visible = False

                    If txtPIva.Text = "" Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(COD_FISCALE)='" & par.PulisciStrSql(Me.txtCF.Text) & "'"
                    Else
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(PARTITA_IVA)='" & par.PulisciStrSql(Me.txtPIva.Text) & "'"
                    End If

                    Dim va_bene As Boolean = True
                    Dim va_bene1 As Boolean = True

                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = True Then
                        If myReader.Read Then
                            indice = myReader("id")

                            If txtPIva.Text = "" Then
                                If (UCase(par.IfNull(myReader("COGNOME"), "")) <> UCase(txtCognome.Text)) Or (UCase(par.IfNull(myReader("NOME"), "")) <> UCase(txtNome.Text)) Then
                                    va_bene1 = False
                                    SSS = "Nominativo: " & UCase(par.IfNull(myReader("COGNOME"), "")) & " " & UCase(par.IfNull(myReader("NOME"), ""))
                                End If
                            Else
                                If (UCase(par.IfNull(myReader("RAGIONE_SOCIALE"), "")) <> UCase(txtRagione.Text)) Then
                                    va_bene1 = False
                                    SSS = "Ragione Sociale: " & UCase(par.IfNull(myReader("RAGIONE_SOCIALE"), ""))
                                End If
                            End If
                        End If

                        par.cmd.CommandText = "SELECT id_anagrafica FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & IndiceContratto & " and id_anagrafica=" & indice
                        Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderX1.HasRows = True Then
                            va_bene = False
                        End If
                        myReaderX1.Close()

                    Else
                        Dim COMUNE_NASCITA As String = ""
                        COMUNE_NASCITA = CmbComune.SelectedItem.Value
                        If COMUNE_NASCITA = "-1" Then
                            If Len(txtCF.Text) = 16 Then
                                COMUNE_NASCITA = Mid(txtCF.Text, 12, 4)
                            End If
                        End If

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANAGRAFICA (ID, NOME, COGNOME, COD_FISCALE, PARTITA_IVA, RAGIONE_SOCIALE, CITTADINANZA, RESIDENZA, DATA_NASCITA, COD_COMUNE_NASCITA, SESSO, TELEFONO,TIPO_DOC,NUM_DOC,DATA_DOC,RILASCIO_DOC,comune_residenza,provincia_residenza,indirizzo_residenza,civico_residenza,DOC_SOGGIORNO,CAP_RESIDENZA)" _
                        & "VALUES(SISCOM_MI.SEQ_ANAGRAFICA.NEXTVAL,'" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) & "','" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) & "', '" & par.PulisciStrSql(Me.txtCF.Text) & "', '" & par.PulisciStrSql(Me.txtPIva.Text) & "'" _
                        & ", '" & par.PulisciStrSql(Me.txtRagione.Text) & "','" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) & "', '" & par.PulisciStrSql(txtComuneResidenza.Text & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) & "', '" & par.AggiustaData(Me.txtDataNascita.Text) & "', '" & COMUNE_NASCITA & "'" _
                        & ",'" & Me.cmbSesso.SelectedItem.Text & "','" & par.PulisciStrSql(Me.txtTel.Text) & "'," & cmbTipoDoc.SelectedItem.Value & ",'" & par.PulisciStrSql(txtNumDoc.Text) & "','" & par.AggiustaData(txtDataDoc.Text) & "','" & par.PulisciStrSql(txtDocRilasciato.Text) _
                        & "','" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) & "','" & par.PulisciStrSql(txtProvinciaResidenza.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) & "','" & par.PulisciStrSql(txtCivicoResidenza.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtSoggiorno.Text))) & "','" & par.PulisciStrSql(txtCAP.Text) & "')"
                        par.cmd.ExecuteNonQuery()


                        par.cmd.CommandText = "select siscom_mi.seq_anagrafica.currval from dual"
                        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderA.Read Then
                            indice = myReaderA(0)
                        End If
                        myReaderA.Close()
                    End If
                    myReader.Close()

                    If va_bene1 = True Then

                        If va_bene = True Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ID_CONTRATTO=" & IndiceContratto
                            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            Do While myReaderX.Read
                                par.cmd.CommandText = "UPDATE SISCOM_MI.SOGGETTI_CONTRATTUALI SET COD_TIPOLOGIA_OCCUPANTE='EXINTE',DATA_FINE='" & Format(Now, "yyyyMMdd") & "' WHERE ID_CONTRATTO=" & IndiceContratto & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "UPDATE SISCOM_MI.INTESTATARI_RAPPORTO SET DATA_FINE='" & Format(Now, "yyyyMMdd") & "' WHERE ID_CONTRATTO=" & IndiceContratto & " AND ID_ANAGRAFICA=" & par.IfNull(myReaderX("ID_ANAGRAFICA"), "-1")
                                par.cmd.ExecuteNonQuery()

                            Loop
                            myReaderX.Close()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                            & "(ID_ANAGRAFICA,ID_CONTRATTO,COD_TIPOLOGIA_PARENTELA,COD_TIPOLOGIA_OCCUPANTE,COD_TIPOLOGIA_TITOLO,DATA_INIZIO,DATA_FINE) " _
                            & "VALUES (" & indice & "," & IndiceContratto & ",'NA','INTE','LEGIT','" & Format(Now, "yyyyMMdd") & "','')"
                            par.cmd.ExecuteNonQuery()


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTESTATARI_RAPPORTO (ID_CONTRATTO,ID_ANAGRAFICA,DATA_INIZIO,DATA_FINE) VALUES (" & IndiceContratto & "," & indice & ",'" & Format(Now, "yyyyMMdd") & "','29991231')"
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                & "VALUES (" & IndiceContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                & "'F38','')"
                            par.cmd.ExecuteNonQuery()


                            par.myTrans.Commit()
                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE" & IDCONNESSIONE.Value, par.myTrans)


                            'par.cmd.Dispose()
                            'par.OracleConn.Close()
                            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Response.Write("<script>alert('Operazione effettuata! Stampare il modulo nella pagina successiva e modificare l\'indirizzo delle comunicazioni!');opener.document.getElementById('form1').USCITA.value='1';opener.document.getElementById('form1').cmbintestazione.value='OK';opener.document.getElementById('form1').submit();window.open('Comunicazioni/CambioIntestazioneF.aspx?IDC=" & IndiceContratto & "','CambioIntestazione','');self.close();</script>")

                            'Response.Redirect("Comunicazioni/CambioIntestazione.aspx?IDC=" & IndiceContratto)
                        Else
                            lblErrore.Visible = True
                            lblErrore.Text = "Attenzione! Il nuovo intestatario non può essere uguale all'intestatario corrente!"
                        End If
                    Else
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il nuovo intestatario è già presente in anagrafica ma con cognome/nome/ragione sociale diverse. Verificare l'anagrafica! Nel sistema risulta " & SSS

                    End If
                End If
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Avvalorare i campi necessari alla definizione di una personafisica o giuridica!"
                'Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")

            End If



        Catch ex As Exception
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


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

    Protected Sub ImgSeleziona_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSeleziona.Click
        If Session.Item("IDANA") <> "" Then
            Try
                par.OracleConn.Open()

                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT ANAGRAFICA.* FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID=" & Session.Item("IDANA")
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

                    txtSoggiorno.Text = par.IfNull(myReader("DOC_SOGGIORNO"), "")

                    Me.txtDataNascita.Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
                    CmbComune.SelectedIndex = -1
                    Me.CmbComune.SelectedValue = par.IfNull(myReader("COD_COMUNE_NASCITA"), "-1")

                    Me.cmbSesso.SelectedIndex = -1
                    Me.cmbSesso.Items.FindByValue(par.IfNull(myReader("SESSO"), "M")).Selected = True
                    Me.txtTel.Text = par.IfNull(myReader("TELEFONO"), "")

                    cmbTipoDoc.SelectedIndex = -1
                    cmbTipoDoc.Items.FindByValue(par.IfNull(myReader("TIPO_DOC"), "0")).Selected = True

                    txtNumDoc.Text = par.IfNull(myReader("NUM_DOC"), "")
                    txtDataDoc.Text = par.FormattaData(par.IfNull(myReader("DATA_DOC"), ""))
                    txtDocRilasciato.Text = par.IfNull(myReader("RILASCIO_DOC"), "")

                End If
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                lblErrore.Visible = True
                lblErrore.Text = ex.Message

            End Try
        End If
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

    Private Sub CompletaDati(ByVal CF As String)

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
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
End Class
