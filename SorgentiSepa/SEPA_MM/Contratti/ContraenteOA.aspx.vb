
Partial Class Contratti_ContraenteOA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then

                Unita = Request.QueryString("U")
                CODICEUnita = Request.QueryString("CODICE")
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.RiempiDListConVuoto(Me, par.OracleConn, "cmbComune", "SELECT * FROM comuni_nazioni where sigla<>'E' AND SIGLA<>'C' order by nome asc", "NOME", "COD")
                    par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni where SIGLA='C' OR sigla='E' OR SIGLA='I' order by nome asc", "NOME", "COD")


                    par.cmd.CommandText = "select COMUNI_NAZIONI.NOME,COMUNI_NAZIONI.SIGLA,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE='" & CODICEUnita & "'"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        txtIndirizzoResidenza.Text = par.IfNull(myReaderA("DESCRIZIONE"), "")
                        txtCivicoResidenza.Text = par.IfNull(myReaderA("CIVICO"), "")
                        txtComuneResidenza.Text = par.IfNull(myReaderA("NOME"), "")
                        txtProvinciaResidenza.Text = par.IfNull(myReaderA("SIGLA"), "")
                        txtCAP.Text = par.IfNull(myReaderA("CAP"), "")
                    End If
                    myReaderA.Close()

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch ex As Exception
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    par.OracleConn.Close()
                End Try

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
                txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPIva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtSoggiorno.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                Session.Add("CONTRATTOAPERTO", "1")
            End If

            txtDataNascita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataDoc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")



        Catch ex As Exception
            Me.lblErrore.Visible = True
            'par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Try
            Dim indice As Long = 0


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

                'If txtCF.Text <> "" Then
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
                'End If

                If txtComuneResidenza.Text = "" Or txtProvinciaResidenza.Text = "" Or txtIndirizzoResidenza.Text = "" Or txtCivicoResidenza.Text = "" Or txtCAP.Text = "" Then
                    lblErrore.Visible = True
                    lblErrore.Text = "Attenzione! Valorizzare tutti i campi relativi alla residenza!"
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                'If txtCF.Text <> "" Then
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
                    'End If
                End If

            End If

            'If txtPIva.Text <> "" Then
            '    If Len(txtPIva.Text) <> 11 Then
            '        lblErrore.Visible = True
            '        lblErrore.Text = "Attenzione! Partita Iva non valida!"
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '        Exit Sub
            '    End If

            '    If par.VerificaPI(txtPIva.Text) = False Then
            '        lblErrore.Visible = True
            '        lblErrore.Text = "Attenzione! Partita Iva non valida!"
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '        Exit Sub
            '    End If

            'End If

            'If txtDataNascita.Text <> "" Then
            '    If par.AggiustaData(txtDataNascita.Text) > par.AggiustaData(txtDataDoc.Text) Then
            '        lblErrore.Visible = True
            '        'lblErrore.Text = "Attenzione! La data di rilascio del documento non può essere superiore alla data di nascita!"
            '        lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere successiva alla data di nascita!"
            '        par.OracleConn.Close()
            '        Exit Sub
            '    End If
            'End If

            'If txtDataDoc.Text <> "" Then
            '    If par.AggiustaData(txtDataDoc.Text) > Format(Now, "yyyyMMdd") Then
            '        lblErrore.Visible = True
            '        lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere precedente o uguale alla data odierna!"
            '        par.OracleConn.Close()
            '        Exit Sub
            '    End If
            'End If

            If (lblErrore.Visible = False AndAlso par.IfEmpty(Me.txtNome.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCognome.Text, "Null") <> "Null") Or (par.IfEmpty(Me.txtRagione.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtPIva.Text, "Null") <> "Null") Then
                Me.lblErrore.Visible = False

                If txtCognome.Text <> "" Then
                    If txtCF.Text <> "" Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(COD_FISCALE)='" & par.PulisciStrSql(Me.txtCF.Text) & "'"
                    Else
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(COGNOME)='" & par.PulisciStrSql(Me.txtCognome.Text) & "' AND UPPER(NOME)='" & par.PulisciStrSql(Me.txtNome.Text) & "'"
                    End If

                Else
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(PARTITA_IVA)='" & par.PulisciStrSql(Me.txtPIva.Text) & "'"
                End If

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then
                    'Response.Write("<script>alert('Attenzione, questo contraente esiste già nei nostri archivi! ')</script>")
                    If myReader.Read Then
                        indice = myReader("id")
                    End If
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
                    & ", '" & par.PulisciStrSql(LTrim(RTrim(Me.txtRagione.Text))) & "','" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) & "', '" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text)) & " (" & txtProvinciaResidenza.Text & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) & "', '" & par.AggiustaData(Me.txtDataNascita.Text) & "', '" & COMUNE_NASCITA & "'" _
                    & ",'" & Me.cmbSesso.SelectedItem.Text & "','" & par.PulisciStrSql(Me.txtTel.Text) & "'," & cmbTipoDoc.SelectedItem.Value & ",'" & par.PulisciStrSql(txtNumDoc.Text) & "','" & par.AggiustaData(txtDataDoc.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtDocRilasciato.Text))) _
                    & "','" & par.PulisciStrSql(txtComuneResidenza.Text) & "','" & par.PulisciStrSql(txtProvinciaResidenza.Text) & "','" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) & "','" & par.PulisciStrSql(txtCivicoResidenza.Text) & "','" & par.PulisciStrSql(txtSoggiorno.Text) & "','" & par.PulisciStrSql(txtCAP.Text) & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select siscom_mi.seq_anagrafica.currval from dual"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        indice = myReaderA(0)
                    End If
                    myReaderA.Close()
                End If
                myReader.Close()



                'par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
                '    & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA) " _
                '    & " Values " _
                '    & "(-1, " & Unita & ", '" & Format(Now, "yyyyMMdd") & "', 0, -1, " _
                '    & "'" & par.PulisciStrSql(txtCognome.Text) & "', '" & par.PulisciStrSql(txtNome.Text) _
                '    & "', '" & par.PulisciStrSql(txtCF.Text) & "', 'X', 0,0," & indice & ")"
                'par.cmd.ExecuteNonQuery()

                'par.cmd.CommandText = "update alloggi set stato='8',assegnato='1',DATA_PRENOTATO='" & Format(Now, "yyyyMMdd") & "' where COD_ALLOGGIO='" & CODICEUnita & "'"
                'par.cmd.ExecuteNonQuery()

                'par.OracleConn.Close()
                'Session.Add("CONTRATTOAPERTO", "0")

                'Response.Write("<script>alert('Operazione Effettuata. Ora è possibile inserire il rapporto!');</script>")
                'Response.Write("<script>window.close();</script>")




                ''Response.Write("<script>alert('Attenzione, non è possibile proseguire. Funzione ancora non disponibile!');</script>")
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Redirect("Canone_OA.aspx?U=" & Unita & "&C=" & indice & "&CODICE=" & CODICEUnita)


            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Avvalorare i campi necessari alla definizione di una personafisica o giuridica!"
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")

            End If



        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Add("CONTRATTOAPERTO", "0")
        Response.Write("<script>window.close();</script>")
    End Sub

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
        Else
            lblErroreCF.Visible = False
            If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                CompletaDati(UCase(txtCF.Text))
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
            End If

            If Len(par.IfNull(myReader1("SIGLA"), "")) = 2 Then
                cmbCittadinanza.SelectedIndex = -1
                cmbCittadinanza.Items.FindByText("ITALIA").Selected = True
            Else
                cmbCittadinanza.SelectedIndex = -1
                cmbCittadinanza.Items.FindByText(par.IfNull(myReader1("NOME"), "ITALIA")).Selected = True
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
