
Partial Class MANUTENZIONI_FornitoreGOLD
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        Try
            If Not IsPostBack Then

                vId = 0
                vId = Session.Item("ID") 'ricavo id fornitori aggiunto alla sessione nella pagina dei risultati
                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                Me.txtConnessione.Text = CStr(lIdConnessione)
                Me.txtIdFornitori.Text = "-1"

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                Me.txtdataprocura.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                SettaggioCampi()


                If vId <> 0 Then
                    lbltitolo.Text = "Modifica Persona Giuridica per Fornitore"
                    Apriricerca()
                End If



                '*** FORM GENERALE

                Dim CTRL1 As Control
                For Each CTRL1 In Me.form1.Controls
                    If TypeOf CTRL1 Is TextBox Then
                        DirectCast(CTRL1, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL1, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL1 Is DropDownList Then
                        DirectCast(CTRL1, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL1 Is CheckBoxList Then
                        DirectCast(CTRL1, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

            End If




        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            lblErrore.Text = ex.Message

        End Try
    End Sub

    Private Sub SettaggioCampi()

        'CARICO DRL
        Dim gest As Integer
        gest = 0

        Try
            'DrLTipoR.Items.Add(New ListItem(" ", 0))
            'DrLTipoR.SelectedValue = 0

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INDIRIZZO "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()
            DrLTipoInd.Items.Add(New ListItem(" ", -1))
            DrLTipoIndA.Items.Add(New ListItem(" ", -1))
            DrLTipoIndR.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                DrLTipoInd.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
                DrLTipoIndA.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
                DrLTipoIndR.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            DrLTipoInd.SelectedValue = -1
            DrLTipoIndA.SelectedValue = -1
            DrLTipoIndR.SelectedValue = -1
            myReader1.Close()

            'par.RiempiDListConVuoto(Me, par.OracleConn, "cmbComune", "SELECT * FROM comuni_nazioni where sigla<>'E' AND SIGLA<>'C' order by nome asc", "NOME", "COD")
            'par.RiempiDListConVuoto(Me, par.OracleConn, "cmbCittadinanza", "SELECT * FROM comuni_nazioni where SIGLA='C' OR sigla='E' OR SIGLA='I' order by nome asc", "NOME", "COD")

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub
    Private Sub Apriricerca()

        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader


        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            If vId <> 0 Then
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiCampi(myReader1)
                End While
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")
            End If



        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                ' par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Dettagli Fornitore visualizzati da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID = " & vId
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiCampi(myReader1)
                End While
                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            Else
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Me.txtCognome.Text = par.IfNull(myReader1("COGNOME"), "")
        Me.txtNome.Text = par.IfNull(myReader1("NOME"), "")
        Me.txtCFR.Text = par.IfNull(myReader1("COD_FISCALE_R"), "")
        Me.txtCF.Text = par.IfNull(myReader1("COD_FISCALE"), "")
        Me.txtComuneResidenza.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
        Me.txtComuneResidenzaA.Text = par.IfNull(myReader1("COMUNE_SEDE_A"), "")
        Me.txtComuneResidenzaR.Text = par.IfNull(myReader1("COMUNE_RESIDENZA_R"), "")
        Me.txtProvinciaResidenza.Text = par.IfNull(myReader1("PR_RESIDENZA"), "")
        Me.txtProvinciaResidenzaA.Text = par.IfNull(myReader1("PR_SEDE_A"), "")
        Me.txtProvinciaResidenzaR.Text = par.IfNull(myReader1("PR_RESIDENZA_R"), "")
        Me.txtIndirizzoResidenza.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
        Me.txtIndirizzoResidenzaA.Text = par.IfNull(myReader1("INDIRIZZO_SEDE_A"), "")
        Me.txtIndirizzoResidenzaR.Text = par.IfNull(myReader1("INDIRIZZO_RESIDENZA_R"), "")
        Me.txtCivicoResidenza.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
        Me.txtCivicoResidenzaA.Text = par.IfNull(myReader1("CIVICO_SEDE_A"), "")
        Me.txtCivicoResidenzaR.Text = par.IfNull(myReader1("CIVICO_RESIDENZA_R"), "")
        Me.txtFax.Text = par.IfNull(myReader1("NUM_FAX"), "")
        Me.txtFaxA.Text = par.IfNull(myReader1("NUM_FAX_SEDE_A"), "")
        Me.txtCAP.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
        Me.txtCAPA.Text = par.IfNull(myReader1("CAP_SEDE_A"), "")
        Me.txtCAPR.Text = par.IfNull(myReader1("CAP_RESIDENZA_R"), "")
        Me.txtIban.Text = par.IfNull(myReader1("IBAN"), "")
        Me.txtTel.Text = par.IfNull(myReader1("NUM_TELEFONO"), "")
        Me.txtTelA.Text = par.IfNull(myReader1("NUM_TELEFONO_SEDE_A"), "")
        Me.txtTelR.Text = par.IfNull(myReader1("TELEFONO_R"), "")
        Me.txtnumprocura.Text = par.IfNull(myReader1("NUM_PROCURA"), "")
        If txtnumprocura.Text <> "" Then
            lblprocura.Visible = True
            txtnumprocura.Visible = True
        End If
        Me.txtdataprocura.Text = par.FormattaData((par.IfNull(myReader1("DATA_PROCURA"), "")))
        If txtdataprocura.Text <> "" Then
            lbldataprocura.Visible = True
            txtdataprocura.Visible = True
            controllodata.Enabled = True
        End If
        Me.txtragione.Text = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
        Me.txtPIva.Text = par.IfNull(myReader1("PARTITA_IVA"), "")
        Me.txtRiferimenti.Text = par.IfNull(myReader1("RIFERIMENTI"), "")
        Me.DrLTipoInd.ClearSelection()
        Me.DrLTipoInd.Items.FindByText(par.IfNull(myReader1("TIPO_INDIRIZZO_RESIDENZA"), "")).Selected = True
        Me.DrLTipoIndA.ClearSelection()
        Me.DrLTipoIndA.Items.FindByText(par.IfNull(myReader1("TIPO_INDIRIZZO_SEDE_A"), "")).Selected = True
        Me.DrLTipoIndR.ClearSelection()
        Me.DrLTipoIndR.Items.FindByText(par.IfNull(myReader1("TIPO_INDIRIZZO_RESIDENZA_R"), "")).Selected = True
        Me.DrLTipoR.ClearSelection()
        Me.DrLTipoR.Items.FindByValue(par.IfNull(myReader1("TIPO_R"), "")).Selected = True
    End Sub
    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try
            Me.ImgProcedi.Visible = False

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()

            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click

        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vId = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If
    End Sub

    Public Function ControlloCampi() As Boolean

        ControlloCampi = True
        lblErrore.Visible = False

        If vId = 0 Then
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "select * from SISCOM_MI.FORNITORI where SISCOM_MI.FORNITORI.PARTITA_IVA='" & par.PulisciStrSql(Me.txtPIva.Text) & "'"
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myRec.Read Then
                'lblErrore.Visible = True
                ' lblErrore.Text = "Attenzione...Il codice fiscale inserito è già presente nei nostri archivi."
                Response.Write("<script>alert('Attenzione...Partita IVA già presente nei nostri archivi.');</script>")
                myRec.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                ControlloCampi = False
                Exit Function
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If

        If txtCAP.Text <> "" Then
            If txtCAP.Text = "" Or Len(txtCAP.Text) <> 5 Or IsNumeric(txtCAP.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Il CAP deve essere di 5 caratteri numerici!"
                Response.Write("<script>alert('Attenzione! Il CAP deve essere di 5 caratteri numerici!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtCAPA.Text <> "" Then
            If txtCAPA.Text = "" Or Len(txtCAPA.Text) <> 5 Or IsNumeric(txtCAPA.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Il CAP deve essere di 5 caratteri numerici!"
                Response.Write("<script>alert('Attenzione! Il CAP deve essere di 5 caratteri numerici!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtCAPR.Text <> "" Then
            If txtCAPR.Text = "" Or Len(txtCAPR.Text) <> 5 Or IsNumeric(txtCAPR.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Il CAP deve essere di 5 caratteri numerici!"
                Response.Write("<script>alert('Attenzione! Il CAP deve essere di 5 caratteri numerici!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtTel.Text <> "" Then
            If IsNumeric(txtTel.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Numero telefonico errato!"
                Response.Write("<script>alert('Attenzione! Numero telefonico errato!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtTelA.Text <> "" Then
            If IsNumeric(txtTelA.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Numero telefonico errato!"
                Response.Write("<script>alert('Attenzione! Numero telefonico errato!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtTelR.Text <> "" Then
            If IsNumeric(txtTelR.Text) = False Then
                ''lblErrore.Visible = True
                ''lblErrore.Text = "Attenzione! Numero telefonico errato!"
                Response.Write("<script>alert('Attenzione! Numero telefonico errato!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtFax.Text <> "" Then
            If IsNumeric(txtFax.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Numero fax errato!"
                Response.Write("<script>alert('Attenzione! Numero fax errato!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtFaxA.Text <> "" Then
            If IsNumeric(txtFaxA.Text) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "Attenzione! Numero fax errato!"
                Response.Write("<script>alert('Attenzione! Numero fax errato!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If
        'If txtCognome.Text = "" Or txtNome.Text = "" Then
        '    lblErrore.Visible = True
        '    lblErrore.Text = "Attenzione! Inserire un cognome/nome e specificare un codice fiscale valido!"
        '    ControlloCampi = False
        '    Exit Function
        'Else
        '    lblErrore.Visible = False
        'End If

        If txtCognome.Text <> "" Or txtNome.Text <> "" Then
            If par.ControllaCF(txtCFR.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                Response.Write("<script>alert('Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!);</script>")
                ControlloCampi = False
                Exit Function
            End If

            If par.ControllaCFNomeCognome(txtCFR.Text, txtCognome.Text, txtNome.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                Response.Write("<script>alert('Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!);</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

        If txtragione.Text = "" Then
            'lblErrore.Visible = True
            'lblErrore.Text = "Attenzione! Inserire la Ragione Sociale!"
            Response.Write("<script>alert('Attenzione! Inserire la Ragione Sociale');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If txtPIva.Text <> "" Then
            If Len(txtPIva.Text) <> 11 Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Partita Iva non valida, inserire 11 cifre!"
                Response.Write("<script>alert('Attenzione! Partita Iva non valida, inserire 11 cifre!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        Else
            'lblErrore.Visible = True
            'lblErrore.Text = "Attenzione! Inserire la Partita Iva!"
            Response.Write("<script>alert('Attenzione! Inserire la Partita IVA!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If txtCF.Text <> "" And Len(txtCF.Text) <> 11 Then
            'lblErrore.Visible = True
            'lblErrore.Text = "Attenzione! Codice Fiscale non valido, inserire 11 cifre!"
            Response.Write("<script>alert('Attenzione! Codice Fiscale non valido, inserire 11 cifre!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If txtnumprocura.Text <> "" Then
            If IsNumeric(txtnumprocura.Text) = False Then
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Numero di procura errato!"
                Response.Write("<script>alert('Attenzione! Numero di procura errato!');</script>")
                ControlloCampi = False
                Exit Function
            End If
        End If

    End Function
    Private Sub Salva()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)





            'Ricavo vId fornitore
            par.cmd.CommandText = " select SISCOM_MI.SEQ_IMPIANTI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vId = myReader1(0)
            End If


            myReader1.Close()
            par.cmd.CommandText = ""

            Me.txtIdFornitori.Text = vId

            ' FORNITORI
            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI(ID, TIPO, COGNOME, NOME, COD_FISCALE, TIPO_INDIRIZZO_RESIDENZA, INDIRIZZO_RESIDENZA, CIVICO_RESIDENZA, CAP_RESIDENZA, COMUNE_RESIDENZA, PR_RESIDENZA, NUM_TELEFONO, IBAN " _
            & ", PARTITA_IVA, NUM_FAX, TIPO_INDIRIZZO_SEDE_A, INDIRIZZO_SEDE_A, CIVICO_SEDE_A, CAP_SEDE_A, COMUNE_SEDE_A, PR_SEDE_A,NUM_TELEFONO_SEDE_A, NUM_FAX_SEDE_A " _
            & ", TIPO_R, NUM_PROCURA, DATA_PROCURA, COMUNE_RESIDENZA_R, INDIRIZZO_RESIDENZA_R, CIVICO_RESIDENZA_R, TELEFONO_R, RIFERIMENTI, TIPO_INDIRIZZO_RESIDENZA_R, COD_FISCALE_R, RAGIONE_SOCIALE, PR_RESIDENZA_R ,CAP_RESIDENZA_R)" _
            & "VALUES(" & vId & ",'G','" & par.PulisciStrSql(Me.txtCognome.Text) & "','" & par.PulisciStrSql(Me.txtNome.Text) & "','" & par.PulisciStrSql(Me.txtCF.Text) & "','" & par.PulisciStrSql(Me.DrLTipoInd.SelectedItem.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtIndirizzoResidenza.Text) & "','" & par.PulisciStrSql(Me.txtCivicoResidenza.Text) & "','" & par.PulisciStrSql(Me.txtCAP.Text) & "','" & par.PulisciStrSql(Me.txtComuneResidenza.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtProvinciaResidenza.Text) & "','" & par.PulisciStrSql(Me.txtTel.Text) & "','" & par.PulisciStrSql(Me.txtIban.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtPIva.Text) & "','" & par.PulisciStrSql(Me.txtFax.Text) & "','" & par.PulisciStrSql(Me.DrLTipoIndA.SelectedItem.Text) & "','" & par.PulisciStrSql(Me.txtIndirizzoResidenzaA.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtCivicoResidenzaA.Text) & "','" & par.PulisciStrSql(Me.txtCAPA.Text) & "','" & par.PulisciStrSql(Me.txtComuneResidenzaA.Text) & "','" & par.PulisciStrSql(Me.txtProvinciaResidenzaA.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtTelA.Text) & "','" & par.PulisciStrSql(Me.txtFaxA.Text) & "','" & par.PulisciStrSql(Me.DrLTipoR.SelectedValue) & "','" & par.PulisciStrSql(Me.txtnumprocura.Text) & "'" _
            & ",'" & par.AggiustaData(Me.txtdataprocura.Text) & "','" & par.PulisciStrSql(Me.txtComuneResidenzaR.Text) & "','" & par.PulisciStrSql(Me.txtIndirizzoResidenzaR.Text) & "','" & par.PulisciStrSql(Me.txtCivicoResidenzaR.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtTelR.Text) & "','" & par.PulisciStrSql(Me.txtRiferimenti.Text) & "','" & par.PulisciStrSql(Me.DrLTipoIndR.SelectedItem.Text) & "','" & par.PulisciStrSql(Me.txtCFR.Text) & "'" _
            & ",'" & par.PulisciStrSql(Me.txtragione.Text) & "','" & par.PulisciStrSql(Me.txtProvinciaResidenzaR.Text) & "','" & par.PulisciStrSql(Me.txtCAPR.Text) & "')"


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************
            '  Response.Write("<SCRIPT>alert('Anagrafe inserita correttamente!');</SCRIPT>")


            ' COMMIT
            par.myTrans.Commit()



            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN APRI RICERCA)
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID = " & vId & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Anagrafe inserita correttamente!');</SCRIPT>")


            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub Update()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            ' FORNITORI 
            par.cmd.Parameters.Clear()

            par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI SET " _
               & "COGNOME='" & par.PulisciStrSql(Me.txtCognome.Text) & "'," _
                & "NOME='" & par.PulisciStrSql(Me.txtNome.Text) & "'," _
               & "COD_FISCALE='" & par.PulisciStrSql(Me.txtCF.Text) & "'," _
               & "TIPO_INDIRIZZO_RESIDENZA='" & par.PulisciStrSql(Me.DrLTipoInd.SelectedItem.Text) & "'," _
               & "INDIRIZZO_RESIDENZA='" & par.PulisciStrSql(Me.txtIndirizzoResidenza.Text) & "'," _
               & "CIVICO_RESIDENZA='" & par.PulisciStrSql(Me.txtCivicoResidenza.Text) & "'," _
               & "CAP_RESIDENZA='" & par.PulisciStrSql(Me.txtCAP.Text) & "'," _
               & "COMUNE_RESIDENZA='" & par.PulisciStrSql(Me.txtComuneResidenza.Text) & "'," _
               & "PR_RESIDENZA='" & par.PulisciStrSql(Me.txtProvinciaResidenza.Text) & "'," _
               & "NUM_TELEFONO='" & par.PulisciStrSql(Me.txtTel.Text) & "'," _
               & "IBAN='" & par.PulisciStrSql(Me.txtIban.Text) & "'," _
               & "PARTITA_IVA='" & par.PulisciStrSql(Me.txtPIva.Text) & "'," _
               & "NUM_FAX='" & par.PulisciStrSql(Me.txtFax.Text) & "'," _
               & "TIPO_INDIRIZZO_SEDE_A='" & par.PulisciStrSql(Me.DrLTipoIndA.SelectedItem.Text) & "'," _
               & "INDIRIZZO_SEDE_A='" & par.PulisciStrSql(Me.txtIndirizzoResidenzaA.Text) & "'," _
               & "CIVICO_SEDE_A='" & par.PulisciStrSql(Me.txtCivicoResidenzaA.Text) & "'," _
               & "CAP_SEDE_A='" & par.PulisciStrSql(Me.txtCAPA.Text) & "'," _
               & "COMUNE_SEDE_A='" & par.PulisciStrSql(Me.txtComuneResidenzaA.Text) & "'," _
               & "PR_SEDE_A='" & par.PulisciStrSql(Me.txtProvinciaResidenzaA.Text) & "'," _
               & "NUM_TELEFONO_SEDE_A='" & par.PulisciStrSql(Me.txtTelA.Text) & "'," _
               & "NUM_FAX_SEDE_A='" & par.PulisciStrSql(Me.txtFaxA.Text) & "'," _
               & "TIPO_R='" & par.PulisciStrSql(Me.DrLTipoR.SelectedValue) & "'," _
               & "NUM_PROCURA='" & par.PulisciStrSql(Me.txtnumprocura.Text) & "'," _
               & "DATA_PROCURA='" & par.AggiustaData(Me.txtdataprocura.Text) & "'," _
               & "COMUNE_RESIDENZA_R='" & par.PulisciStrSql(Me.txtComuneResidenzaR.Text) & "'," _
               & "PR_RESIDENZA_R='" & par.PulisciStrSql(Me.txtProvinciaResidenzaR.Text) & "'," _
               & "INDIRIZZO_RESIDENZA_R='" & par.PulisciStrSql(Me.txtIndirizzoResidenzaR.Text) & "'," _
               & "CIVICO_RESIDENZA_R='" & par.PulisciStrSql(Me.txtCivicoResidenzaR.Text) & "'," _
               & "CAP_RESIDENZA_R='" & par.PulisciStrSql(Me.txtCAPR.Text) & "'," _
               & "TELEFONO_R='" & par.PulisciStrSql(Me.txtTelR.Text) & "'," _
               & "RIFERIMENTI='" & par.PulisciStrSql(Me.txtRiferimenti.Text) & "'," _
               & "TIPO_INDIRIZZO_RESIDENZA_R='" & par.PulisciStrSql(Me.DrLTipoIndR.SelectedItem.Text) & "'," _
               & "COD_FISCALE_R='" & par.PulisciStrSql(Me.txtCFR.Text) & "'," _
               & "RAGIONE_SOCIALE='" & par.PulisciStrSql(Me.txtragione.Text) & "' " _
               & "WHERE ID='" & vId & "'"

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            par.myTrans.Commit() 'COMMIT


            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Anagrafe aggiornata correttamente!');</SCRIPT>")
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCFR.TextChanged
        If par.ControllaCF(UCase(txtCFR.Text)) = False Then
            lblErroreCF.Visible = True
            txtCFR.Text = ""
            txtCFR.Focus()
            'lblErrore.Visible = True
            'lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
            Response.Write("<SCRIPT>alert('Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!');</SCRIPT>")
        Else
            lblErroreCF.Visible = False
            If par.ControllaCFNomeCognome(UCase(txtCFR.Text), txtCognome.Text, txtNome.Text) = True Then
                'lblErrore.Visible = False
            Else
                'lblErrore.Visible = True
                'lblErrore.Text = "Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!"
                Response.Write("<SCRIPT>alert('Attenzione! Se viene inserito un cognome/nome specificare un codice fiscale valido!');</SCRIPT>")
            End If
        End If
    End Sub
    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property
    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property

    Protected Sub DrLTipoR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLTipoR.SelectedIndexChanged
        If DrLTipoR.Items.FindByText("PROCURATORE LEG.").Selected Then
            lblprocura.Visible = True
            lbldataprocura.Visible = True
            txtnumprocura.Visible = True
            txtdataprocura.Visible = True
            controllodata.Enabled = True
        Else
            txtnumprocura.Text = ""
            txtdataprocura.Text = ""
            lblprocura.Visible = False
            lbldataprocura.Visible = False
            txtnumprocura.Visible = False
            txtdataprocura.Visible = False
            controllodata.Enabled = False
        End If
    End Sub
End Class
