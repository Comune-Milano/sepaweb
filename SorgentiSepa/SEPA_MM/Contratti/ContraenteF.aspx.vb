
Partial Class Contratti_ContraenteF
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property Unita() As String
        Get
            If Not (ViewState("par_Unita") Is Nothing) Then
                Return CStr(ViewState("par_Unita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Unita") = value
        End Set

    End Property

    Public Property CODICEUnita() As String
        Get
            If Not (ViewState("par_CODICEUnita") Is Nothing) Then
                Return CStr(ViewState("par_CODICEUnita"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_CODICEUnita") = value
        End Set

    End Property


    Public Property Chiamante() As String
        Get
            If Not (ViewState("par_Chiamante") Is Nothing) Then
                Return CStr(ViewState("par_Chiamante"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Chiamante") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then

                Unita = Request.QueryString("U")
                CODICEUnita = Request.QueryString("CODICE")
                Chiamante = Request.QueryString("T")
                Select Case Chiamante
                    Case "0"
                        label25.text = "USI DIVERSI"
                    Case "1"
                        LABEL25.TEXT = "392/78"
                    Case "2"
                        Label25.Text = "431/98"
                    Case "3"
                        Label25.Text = "C. CONVENZIONATO"
                End Select

                par.OracleConn.Open()
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbComune", "SELECT * FROM comuni_nazioni where sigla<>'E' AND SIGLA<>'C' order by nome asc", "NOME", "COD")
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni where SIGLA='C' OR sigla='E' OR SIGLA='I' order by nome asc", "NOME", "COD")

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                'cmbCittadinanza.Items.FindByText("ITALIA").Selected = True
                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'txtResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtComuneResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtProvinciaResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtIndirizzoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCivicoResidenza.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCAP.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")


                txtSoggiorno.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                Session.Add("CONTRATTOAPERTO", "1")
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
        Session.Add("CONTRATTOAPERTO", "0")
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            Dim indice As Long = 0
            Dim SSS As String = ""
            Dim Va_Bene1 As Boolean = True

            lblErrore.Visible = False

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If txtCAP.Text <> "" Then
                If txtCAP.Text = "" Or Len(txtCAP.Text) <> 5 Or IsNumeric(txtCAP.Text) = False Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Il CAP deve essere di 5 caratteri numerici!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
            End If

            If txtCognome.Text = "" Or txtNome.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire un cognome/nome e specificare un codice fiscale valido!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtCognome.Text <> "" Or txtNome.Text <> "" Then

                If InStr(txtCognome.Text, "|") > 0 Or InStr(txtCognome.Text, "&") > 0 Or InStr(txtCognome.Text, "/") > 0 Or InStr(txtCognome.Text, "javascript:void(0)" ) > 0 Or InStr(txtCognome.Text, "1") > 0 Or InStr(txtCognome.Text, "2") > 0 Or InStr(txtCognome.Text, "3") > 0 Or InStr(txtCognome.Text, "9") > 0 Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Non sono ammessi caratteri numerici o speciali nel campo cognome!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If InStr(txtNome.Text, "|") > 0 Or InStr(txtNome.Text, "&") > 0 Or InStr(txtNome.Text, "/") > 0 Or InStr(txtNome.Text, "javascript:void(0)" ) > 0 Or InStr(txtNome.Text, "1") > 0 Or InStr(txtNome.Text, "2") > 0 Or InStr(txtNome.Text, "3") > 0 Or InStr(txtNome.Text, "9") > 0 Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Non sono ammessi caratteri numerici o speciali nel campo nome!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If par.ControllaCF(txtCF.Text) = False Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If par.ControllaCFNomeCognome(txtCF.Text, txtCognome.Text, txtNome.Text) = False Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If par.RicavaSesso(txtCF.Text) <> cmbSesso.SelectedItem.Text Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Il sesso del contraente non corrisponde a quanto dichiarato nel codice fiscale!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                If txtComuneResidenza.Text = "" Or txtProvinciaResidenza.Text = "" Or txtIndirizzoResidenza.Text = "" Or txtCivicoResidenza.Text = "" Or txtCAP.Text = "" Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Valorizzare tutti i campi relativi alla residenza!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
                'End If

                If Mid(txtCF.Text, 12, 1) <> "Z" Then
                    If Mid(txtCF.Text, 12, 4) <> CmbComune.SelectedItem.Value Then
                        lblErrore.Visible = True
                        lblErrore.Text = "Attenzione! Il Comune di nascita del contraente non corrisponde a quanto dichiarato nel codice fiscale!"
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Exit Sub
                    End If
                End If

            End If

            If txtDataNascita.Text <> "" Then
                If par.AggiustaData(txtDataNascita.Text) > par.AggiustaData(txtDataDoc.Text) Then
                    lblErrore.Visible = True
                    'lblErrore.Text = "Attenzione! La data di rilascio del documento non può essere superiore alla data di nascita!"
                    lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere successiva alla data di nascita!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
            End If

            If txtDataDoc.Text <> "" Then
                If par.AggiustaData(txtDataDoc.Text) > Format(Now, "yyyyMMdd") Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere precedente o uguale alla data odierna!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If
            End If

            Dim tipo_r As Integer = 0

            Dim COMUNE_NASCITA As String = ""

            If (lblErrore.Visible = False AndAlso par.IfEmpty(Me.txtDocRilasciato.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDataDoc.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNumDoc.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCF.Text, "Null") <> "Null") Then
                Me.lblErrore.Visible = False

                If txtCognome.Text <> "" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE (RAGIONE_SOCIALE='' OR RAGIONE_SOCIALE IS NULL) AND  UPPER(COD_FISCALE)='" & par.PulisciStrSql(Me.txtCF.Text) & "'"
                End If

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then
                    'Response.Write("<script>alert('Attenzione, questo contraente esiste già nei nostri archivi! ')</script>")
                    If myReader.Read Then
                        indice = myReader("id")


                        If (UCase(par.IfNull(myReader("COGNOME"), "")) <> UCase(txtCognome.Text)) Or (UCase(par.IfNull(myReader("NOME"), "")) <> UCase(txtNome.Text)) Then
                            va_bene1 = False
                            SSS = "Nominativo: " & UCase(par.IfNull(myReader("COGNOME"), "")) & " " & UCase(par.IfNull(myReader("NOME"), ""))
                        End If


                        If Va_Bene1 = True Then

                            COMUNE_NASCITA = CmbComune.SelectedItem.Value
                            If COMUNE_NASCITA = "-1" Then
                                If Len(txtCF.Text) = 16 Then
                                    COMUNE_NASCITA = Mid(txtCF.Text, 12, 4)
                                End If
                            End If


                            par.cmd.CommandText = "update siscom_mi.anagrafica set cognome='" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) _
                                                & "',nome='" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) _
                                                & "',cod_fiscale='" & par.PulisciStrSql(Me.txtCF.Text) _
                                                & "',cittadinanza='" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) _
                                                & "',residenza='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text)) & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) _
                                                & "',data_nascita='" & par.AggiustaData(Me.txtDataNascita.Text) _
                                                & "',cod_comune_nascita='" & COMUNE_NASCITA _
                                                & "',sesso='" & Me.cmbSesso.SelectedItem.Text _
                                                & "',telefono='" & par.PulisciStrSql(Me.txtTel.Text) _
                                                & "',tipo_doc=" & cmbTipoDoc.SelectedItem.Value _
                                                & ",num_doc='" & par.PulisciStrSql(txtNumDoc.Text) _
                                                & "',data_doc='" & par.AggiustaData(txtDataDoc.Text) _
                                                & "',rilascio_doc='" & par.PulisciStrSql(txtDocRilasciato.Text) _
                                                & "',comune_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) _
                                                & "',provincia_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtProvinciaResidenza.Text))) _
                                                & "',indirizzo_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) _
                                                & "',civico_residenza='" & par.PulisciStrSql(txtCivicoResidenza.Text) _
                                                & "',cap_residenza='" & par.PulisciStrSql(txtCAP.Text) _
                                                & "',doc_soggiorno='" & par.PulisciStrSql(txtSoggiorno.Text) _
                                                & "' where id=" & indice
                            par.cmd.ExecuteNonQuery()
                        End If
                    
                    End If



                Else

                    COMUNE_NASCITA = CmbComune.SelectedItem.Value
                    If COMUNE_NASCITA = "-1" Then
                        If Len(txtCF.Text) = 16 Then
                            COMUNE_NASCITA = Mid(txtCF.Text, 12, 4)
                        End If
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.ANAGRAFICA (ID, NOME, COGNOME, COD_FISCALE, PARTITA_IVA, RAGIONE_SOCIALE, CITTADINANZA, RESIDENZA, DATA_NASCITA, COD_COMUNE_NASCITA, SESSO, TELEFONO,TIPO_DOC,NUM_DOC,DATA_DOC,RILASCIO_DOC,comune_residenza,provincia_residenza,indirizzo_residenza,civico_residenza,DOC_SOGGIORNO,CAP_RESIDENZA)" _
                    & "VALUES(SISCOM_MI.SEQ_ANAGRAFICA.NEXTVAL,'" & par.PulisciStrSql(LTrim(RTrim(Me.txtNome.Text))) & "','" & par.PulisciStrSql(LTrim(RTrim(Me.txtCognome.Text))) & "', '" & par.PulisciStrSql(Me.txtCF.Text) & "', ''" _
                    & ", '','" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) & "', '" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text)) & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) & "', '" & par.AggiustaData(Me.txtDataNascita.Text) & "', '" & COMUNE_NASCITA & "'" _
                    & ",'" & Me.cmbSesso.SelectedItem.Text & "','" & par.PulisciStrSql(Me.txtTel.Text) & "'," & cmbTipoDoc.SelectedItem.Value & ",'" & par.PulisciStrSql(txtNumDoc.Text) & "','" & par.AggiustaData(txtDataDoc.Text) & "','" & par.PulisciStrSql(txtDocRilasciato.Text) _
                    & "','" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) & "','" & par.PulisciStrSql(LTrim(RTrim(txtProvinciaResidenza.Text))) & "','" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) & "','" & par.PulisciStrSql(txtCivicoResidenza.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtSoggiorno.Text))) & "','" & par.PulisciStrSql(txtCAP.Text) & "')"
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "select siscom_mi.seq_anagrafica.currval from dual"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        indice = myReaderA(0)
                    End If
                    myReaderA.Close()
                End If
                myReader.Close()

                If Va_Bene1 = True Then

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Add("CONTRATTOAPERTO", "0")

                    Select Case Chiamante
                        Case "0"
                            Response.Redirect("Canone_USI_Diversi.aspx?U=" & Unita & "&C=" & indice & "&CODICE=" & CODICEUnita)
                        Case "1"
                            Response.Redirect("Canone392.aspx?U=" & Unita & "&C=" & indice & "&CODICE=" & CODICEUnita)
                        Case "2"
                            Response.Redirect("Canone_431.aspx?U=" & Unita & "&C=" & indice & "&CODICE=" & CODICEUnita)
                        Case "3"
                            Response.Redirect("Canone_CON.aspx?U=" & Unita & "&C=" & indice & "&CODICE=" & CODICEUnita)
                    End Select


                Else
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Add("CONTRATTOAPERTO", "0")
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Il nuovo intestatario è già presente in anagrafica ma con cognome e/o nome diverso. Verificare l'anagrafica! Nel sistema risulta " & SSS


                End If






            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Avvalorare i campi necessari alla definizione di una persona fisica!"
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")

            End If



        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
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
                txtCF.Text = ""
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

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE (RAGIONE_SOCIALE='' OR RAGIONE_SOCIALE IS NULL) AND UPPER(COD_FISCALE)='" & CF & "'"
        myReader1 = par.cmd.ExecuteReader
        If myReader1.Read Then
            txtNumDoc.Text = par.IfNull(myReader1("NUM_DOC"), "")
            txtDataDoc.Text = par.FormattaData(par.IfNull(myReader1("DATA_DOC"), ""))
            txtDocRilasciato.Text = par.IfNull(myReader1("RILASCIO_DOC"), "")
            txtSoggiorno.Text = par.IfNull(myReader1("DOC_SOGGIORNO"), "")
            txtComuneResidenza.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
            txtProvinciaResidenza.Text = par.IfNull(myReader1("PROVINCIA_RESIDENZA"), "")
            txtIndirizzoResidenza.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
            txtCivicoResidenza.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
            txtCAP.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
            txtTel.Text = par.IfNull(myReader1("TELEFONO"), "")
        End If
        myReader1.Close()

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
End Class
