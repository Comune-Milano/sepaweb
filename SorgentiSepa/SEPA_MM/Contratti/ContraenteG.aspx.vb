
Partial Class Contratti_ContraenteG
    Inherits PageSetIdMode
    Dim par As New CM.Global

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


            If Len(txtPIVA.Text) <> 11 Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Partita Iva non inserita o non valida!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtRagione.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire un la Ragione Sociale e specificare una partita iva valida!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtCAP.Text = "" Or Len(txtCAP.Text) <> 5 Or IsNumeric(txtCAP.Text) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Il CAP della Sede Legale deve essere di 5 caratteri numerici!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtComuneResidenza.Text = "" Or txtProvinciaResidenza.Text = "" Or txtIndirizzoResidenza.Text = "" Or txtCivicoResidenza.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire tutti i valori relativi all'indirizzo della Sede Legale!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If


            If chProcuratore.Checked = True And (txtNumProcura.Text = "" Or txtDataProcura.Text = "") Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire i valori relativi alla procura!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If


            If txtCognome.Text = "" Or txtNome.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire un cognome/nome e specificare un codice fiscale valido!"
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
                lblErrore.Text = "Attenzione! Il sesso non corrisponde a quanto dichiarato nel codice fiscale!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtComuneResidenza0.Text = "" Or txtProvinciaResidenza0.Text = "" Or txtIndirizzoResidenza0.Text = "" Or txtCivicoResidenza0.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Inserire tutti i valori relativi all'indirizzo del rappresentante!"
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

            If par.AggiustaData(txtDataNascita.Text) > par.AggiustaData(txtDataDoc.Text) Then
                lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! La data di rilascio del documento non può essere superiore alla data di nascita!"
                lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere successiva alla data di nascita!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If


            If par.AggiustaData(txtDataDoc.Text) > Format(Now, "yyyyMMdd") Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! La data di rilascio del documento deve essere precedente o uguale alla data odierna!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If


            If txtNumDoc.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Specificare il numero del documento del rappresentante!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtDocRilasciato.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Specificare il luogo di rilascio del documento del rappresentante!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If txtDataDoc.Text = "" Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Specificare la data di rilascio del documento del rappresentante!"
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If
            Dim tipo_r As Integer = 0

            Dim COMUNE_NASCITA As String = ""

            If lblErrore.Visible = False And PIVA.Value = "0" Then
                Me.lblErrore.Visible = False


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(PARTITA_IVA)='" & par.PulisciStrSql(Me.txtPIVA.Text) & "'"


                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.HasRows = True Then
                    If myReader.Read Then
                        indice = myReader("id")

                        If UCase(par.IfNull(myReader("ragione_sociale"), "")) <> UCase(txtRagione.Text) Then
                            Va_Bene1 = False
                            SSS = "Ragione Sociale: " & UCase(par.IfNull(myReader("ragione_sociale"), ""))
                        End If

                        If Va_Bene1 = True Then

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


                            par.cmd.CommandText = "update siscom_mi.anagrafica set cognome='" & par.PulisciStrSql(Me.txtCognome.Text) _
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
                                                & "',rilascio_doc='" & par.PulisciStrSql(txtDocRilasciato.Text) _
                                                & "',comune_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) _
                                                & "',provincia_residenza='" & par.PulisciStrSql(LTrim(RTrim(txtProvinciaResidenza.Text))) _
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
                                        & "'" & par.PulisciStrSql(LTrim(RTrim(Me.txtPIVA.Text))) & "', '" & par.PulisciStrSql(Me.cmbCittadinanza.SelectedItem.Text) _
                                        & "', '" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text)) & " (" & LTrim(RTrim(txtProvinciaResidenza.Text)) & ") " & LTrim(RTrim(txtIndirizzoResidenza.Text)) & ", " & txtCivicoResidenza.Text) _
                                        & "', '" & par.AggiustaData(Me.txtDataNascita.Text) & "', '" & COMUNE_NASCITA _
                                        & "','" & Me.cmbSesso.SelectedItem.Text & "', '" & par.PulisciStrSql(Me.txtTel.Text) _
                                        & "', null, " & cmbTipoDoc.SelectedItem.Value & ", '" & par.PulisciStrSql(txtNumDoc.Text) _
                                        & "', '" & par.AggiustaData(txtDataDoc.Text) & "', '" & par.PulisciStrSql(LTrim(RTrim(txtDocRilasciato.Text))) _
                                        & "', '" & par.PulisciStrSql(LTrim(RTrim(txtComuneResidenza.Text))) & "', '" & par.PulisciStrSql(txtProvinciaResidenza.Text) _
                                        & "', '" & par.PulisciStrSql(LTrim(RTrim(txtIndirizzoResidenza.Text))) _
                                        & "', '" & par.PulisciStrSql(txtCivicoResidenza.Text) & "', '" & par.PulisciStrSql(txtCAP.Text) _
                                        & "', '" & par.PulisciStrSql(txtSoggiorno.Text) & "', " & tipo_r & ", '" & par.PulisciStrSql(txtNumProcura.Text) & "', " _
                                        & "'" & par.AggiustaData(Me.txtDataProcura.Text) & "', '" _
                                        & par.PulisciStrSql(txtComuneResidenza0.Text) & "', '" _
                                        & par.PulisciStrSql(txtProvinciaResidenza0.Text) & "', '" _
                                        & par.PulisciStrSql(txtIndirizzoResidenza0.Text) & "', '" & par.PulisciStrSql(txtCivicoResidenza0.Text) _
                                        & "', '" & par.PulisciStrSql(txtCAP0.Text) & "','" & par.PulisciStrSql(txtTel0.Text) & "')"


                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "select siscom_mi.seq_anagrafica.currval from dual"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        indice = myReaderA(0)
                    End If
                    myReaderA.Close()
                End If
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("CONTRATTOAPERTO", "0")

                Select Case Chiamante
                    Case "0", "4"
                        Response.Redirect("Canone_USI_Diversi.aspx?U=" & Unita & "&C=" & indice & "&CODICE=" & CODICEUnita & "&T=" & Chiamante)
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
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Valorizzare i campi necessari alla definizione di una personafisica o giuridica!"
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!')</script>")

            End If



        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


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
                        Label35.Text = "USI DIVERSI"
                    Case "1"
                        Label35.Text = "392/78"
                    Case "2"
                        Label35.Text = "431/98"
                    Case "3"
                        Label35.Text = "C. CONVENZIONATO"
                    Case "4"
                        Label35.Text = "USI DIVERSI-CONCESSIONE"
                End Select

                par.OracleConn.Open()
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbComune", "SELECT * FROM comuni_nazioni where sigla<>'E' AND SIGLA<>'C' order by nome asc", "NOME", "COD")
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni where SIGLA='C' OR sigla='E' OR SIGLA='I' order by nome asc", "NOME", "COD")

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()





                Session.Add("CONTRATTOAPERTO", "1")
            End If

            txtRagione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPIVA.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

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
        Session.Add("CONTRATTOAPERTO", "0")
        Response.Write("<script>window.close();</script>")
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

    End Sub

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

    Protected Sub txtPIVA_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPIVA.TextChanged
        par.OracleConn.Open()
        par.SettaCommand(par)




        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA WHERE UPPER(partita_iva)='" & txtPIVA.Text & "'"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read Then
            
            txtComuneResidenza.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
            txtProvinciaResidenza.Text = par.IfNull(myReader1("PROVINCIA_RESIDENZA"), "")
            txtIndirizzoResidenza.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
            txtCivicoResidenza.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
            txtCAP.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
            txtTel.Text = par.IfNull(myReader1("TELEFONO"), "")

            'MAX 23/10/2014
            If txtRagione.Text = "" Then
                txtRagione.Text = par.IfNull(myReader1("ragione_sociale"), "")
            Else
                '****** MTeresa 07/10/2016: blocco eliminato a seguito di numerose segnalazioni da parte di MM ******
                'If UCase(txtRagione.Text) <> (par.IfNull(myReader1("ragione_sociale"), "")) Then
                '    Response.Write("<script>alert('Attenzione, la partita iva inserita non corrisponde alla ragione sociale inserita. Sarà riportata la ragione sociale corretta!');</script>")
                'End If
                txtRagione.Text = par.IfNull(myReader1("ragione_sociale"), "")
            End If
            '---------------
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
