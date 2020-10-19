Imports System.Collections
Imports System.Data.OracleClient
Imports Telerik.Web.UI

Partial Class Fornitori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public classetab As String = ""
    Public tabvisibility As String = ""
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public codice_fiscale As String = "visible"
    Public modale As String = ""

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

    Public Property vIdFornitori() As Long
        Get
            If Not (ViewState("par_idFornitori") Is Nothing) Then
                Return CLng(ViewState("par_idFornitori"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idFornitori") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        errorealiquota.Visible = False
        errorecodicefornitore.Visible = False
        erroredenominazione.Visible = False
        errorecodicefiscale.Visible = False
        errorepartitaiva.Visible = False

        '------CONTROLLO VISUALIZZAZIONE IN SOLA LETTURA------'

        If Session.Item("BP_CC_L") = 1 Then
            modalitaSOLALETTURA.Value = "1"
            If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                Session.Remove("LAVORAZIONE")
            End If

        End If

        errorecodicefiscale.Visible = False
        errorepartitaiva.Visible = False
        If IsNothing(Request.QueryString("CO")) And IsNothing(Request.QueryString("RA")) Then

            If Not IsNothing(Request.QueryString("S")) Then
                'DOPO SALVA
                daInserimento.Value = "2"
            ElseIf Not IsNothing(Request.QueryString("U")) Then
                'DOPO UPDATE
                daInserimento.Value = "0"
            Else
                If Request.QueryString("ID") = "-1" Then
                    'INSERIMENTO SENZA SALVA
                    daInserimento.Value = "1"
                Else
                    'DA PAGINA ESTERNA
                    daInserimento.Value = "3"
                End If
            End If

        End If

        If Not IsNothing(Session.Item("ERROREMOD")) Then
            If Session.Item("ERROREMOD") = 1 Then
                chiudiConnessione()
                Session.Add("ERRORE", "Provenienza:" & Page.Title)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
                Exit Sub
            End If
        End If

        If Not IsNothing(Session.Item("modificaINDIBAN")) Then
            If Session.Item("modificaINDIBAN") = 1 Then
                modificheEffettuate.Value = "1"
            End If
            If Not IsNothing(Session.Item("modificaINDIBAN")) Then
                Session.Remove("modificaINDIBAN")
            End If

        End If

        'Dim Str As String
        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"
        'Response.Write(Str)
        'If Not IsNothing(Request.QueryString("CALL")) Then
        '    If Request.QueryString("CALL") = "COND" Then
        '        modale = "modal"
        '        Response.Write("<script>window.name = 'modal';</script>")
        '    End If
        'End If



        Try
            If Not IsPostBack Then
                HFGriglia.Value = CType(Tab_Indirizzi.FindControl("DataGrid3"), RadGrid).ClientID & "," _
                   & CType(Tab_IBAN.FindControl("DataGrid3"), RadGrid).ClientID

                '  HFTAB.Value = "tabServizio,tabFornitori,tabImporti,tabVarLavori,tabPenali,tabDatiAmm,tabComposizione,tabElPrezzi,tabVarImporti"
                HFAltezzaTab.Value = 400
                HFAltezzaFGriglie.Value = "480,480"
                HFTAB.Value = "tab1,tab2"
                'Hidden field con l'ID del fornitore
                IDFornitore.Value = Request.QueryString("ID")
                vIdFornitori = IDFornitore.Value
                If Session.Item("BP_CC_L") = 1 Or daInserimento.Value = "3" Then
                    CaricaDatiSolaLettura()
                    visualizzaSL()
                Else
                    '-----MAIUSCOLO-----'
                    txtCodice.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    txtCodiceFiscale.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    txtPiva.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    txtRitAcconto.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    txtDenominazione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    If IDFornitore.Value = "-1" Then
                        '------INSERIMENTO-----'
                        If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                            Session.Remove("LAVORAZIONE")
                        End If

                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_MODALITA_PAG ORDER BY DESCRIZIONE", cmbModalitaPagamento, "ID", "DESCRIZIONE", , , , , False)
                        cmbModalitaPagamento.Enabled = False
                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTO ORDER BY DESCRIZIONE", cmbCondizionePagamento, "ID", "DESCRIZIONE", , , , , False)
                        cmbCondizionePagamento.Enabled = False

                        par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_RITENUTA ORDER BY DESCRIZIONE", cmbRitenuta, "ID", "DESCRIZIONE", , , , , False)
                        cmbRitenuta.Enabled = False

                        btnElimina.Visible = False
                        modificheEffettuate.Value = "0"

                    Else
                        '---IMPOSTO VARIABILE DI SESSIONE LAVORAZIONE A 1---'
                        If Not Session.Item("LAVORAZIONE") = 1 Then
                            Session.Add("LAVORAZIONE", "1")
                        End If
                        '---------------------------------------------------'
                        If Not IsNothing(Request.QueryString("IDCONN")) Then

                            lIdConnessione = Request.QueryString("IDCONN")
                            IDConnessione.Value = lIdConnessione

                            '----CONNESSIONE APERTA TRANSAZIONE CHIUSA----'
                            If Request.QueryString("CALL") = "COND" Then
                                IDConnessione.Value = CStr(lIdConnessione)
                                par.OracleConn.Open()
                                HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                            End If

                            riprendiConnessione()
                            iniziaTransazione()
                            CaricaDati()
                        Else
                            '----CONNESSIONE CHIUSA----'
                            apriConnessione()
                            iniziaTransazione()
                            CaricaDati()
                            If Request.QueryString("CALL") = "COND" Then
                                Me.btnIndietro.Visible = False
                            End If
                        End If
                    End If
                End If
            End If


            If vIdFornitori = "-1" Then
                classetab = "tabberhide"
                tabvisibility = "hidden"

            End If
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub visualizzaSL()

        '----DISABILITO BOTTONI SALVA ED ELIMINA----'
        btnSalva.Visible = False
        btnElimina.Visible = False
        btnEsci.Visible = False
        codice_fiscale = "hidden"



        '---READONLY PER OGNI TEXTBOX---'
        For Each ctrl As Control In Me.form1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).ReadOnly = True
            End If
            If TypeOf ctrl Is RadComboBox Then
                DirectCast(ctrl, RadComboBox).Enabled = False
            End If
        Next

        cmbCondizionePagamento.Enabled = False
        cmbModalitaPagamento.Enabled = False
        cmbRitenuta.Enabled = False


    End Sub

    Protected Sub apriConnessione()
        Try

            '-------CONNESSIONE AL DB----------'
            lIdConnessione = Format(Now, "yyyyMMddHHmmss")
            IDConnessione.Value = CStr(lIdConnessione)
            par.OracleConn.Open()
            HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
            '----------------------------------'
            par.cmd = par.OracleConn.CreateCommand

        Catch ex As Exception
            If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                Session.Remove("LAVORAZIONE")
            End If

            Response.Redirect("../../pagina_home_ncp.aspx")

        End Try

    End Sub

    Protected Sub chiudiConnessione()
        If Not IsNothing(Session.Item("LAVORAZIONE")) Then
            Session.Remove("LAVORAZIONE")
        End If

        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONEFORNITORI" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        If Not IsNothing(par.myTrans) Then
            'par.cmd.Transaction = par.myTrans
            par.myTrans.Rollback()
            par.myTrans.Dispose()
        End If
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Protected Sub riprendiConnessione()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub iniziaTransazione()
        Try
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONEFORNITORI" & lIdConnessione, par.myTrans)
            'par.cmd.Transaction = par.myTrans

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub riprendiTransazione()

        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONEFORNITORI" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        'par.cmd.Transaction = par.myTrans

    End Sub

    Protected Sub chiudiTransazioneOK()

        par.myTrans.Commit()
        HttpContext.Current.Session.Remove("TRANSAZIONEFORNITORI" & lIdConnessione)

    End Sub

    Protected Sub chiudiTransazioneKO()

        par.myTrans.Rollback()
        HttpContext.Current.Session.Remove("TRANSAZIONEFORNITORI" & lIdConnessione)

    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If controllaCampi() = True Then
            If IDFornitore.Value = "-1" Then
                'Inserimento semplice
                Salva()
            Else
                'Update fornitore
                Update()
            End If
        End If
    End Sub



    Protected Sub Salva()
        Try
            apriConnessione()
            iniziaTransazione()
            Dim aliquota As String = par.PulisciStrSql(UCase(txtRitAcconto.Text))
            aliquota = Replace(aliquota, ".", ",")

            Dim idTipoModalitaPag As String = "NULL"
            Dim idTipoPagamento As String = "NULL"
            Dim idTipoRitenuta As String = "NULL"
            If cmbModalitaPagamento.SelectedValue <> "-1" Then
                idTipoModalitaPag = cmbModalitaPagamento.SelectedValue
            End If
            If cmbCondizionePagamento.SelectedValue <> "-1" Then
                idTipoPagamento = cmbCondizionePagamento.SelectedValue
            End If
            If cmbRitenuta.SelectedValue <> "-1" Then
                idTipoRitenuta = cmbRitenuta.SelectedValue
            End If

            '---------INSERIMENTO NUOVO FORNITORE----------'
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI(TIPO,ID,COD_FORNITORE,RAGIONE_SOCIALE,COD_FISCALE,PARTITA_IVA,RIT_ACCONTO,ID_TIPO_MODALITA_PAG,ID_TIPO_PAGAMENTO,ID_TIPO_RITENUTA) "
            par.cmd.CommandText = par.cmd.CommandText & " VALUES ('" & UCase(par.PulisciStrSql(TipoCollaborazione.Text)) & "',SISCOM_MI.SEQ_FORNITORI.NEXTVAL,'" & par.PulisciStrSql(UCase(txtCodice.Text)) & "','" & par.PulisciStrSql(UCase(txtDenominazione.Text)) & "','" & par.PulisciStrSql(UCase(txtCodiceFiscale.Text)) & "','" & par.PulisciStrSql(UCase(txtPiva.Text)) & "','" & par.PulisciStrSql(UCase(aliquota)) & "'," & idTipoModalitaPag & "," & idTipoPagamento & "," & idTipoRitenuta & ")"
            par.cmd.ExecuteNonQuery()
            '----------------------------------------------'
            '---------RECUPERO L'ID APPENA INSERITO--------'
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_FORNITORI.CURRVAL FROM DUAL"
            Dim ultimoIdFornitoreInserito As String = ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                ultimoIdFornitoreInserito = myReader(0)
            End If
            myReader.Close()
            IDFornitore.Value = ultimoIdFornitoreInserito
            '---------------------------------------------'
            '--------MESSAGGIO----------------------------'
            RadNotificationNote.Text = "Fornitore inserito correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()
            '---------------------------------------------'
            '------CHIUDO TRANSAZIONE-----------'
            chiudiTransazioneOK()
            '-----------------------------------'
            'par.cmd.Dispose()
            'If IsNothing(Request.QueryString("CALL")) Then
            '    Response.Write("<script>location.replace('Fornitori.aspx?S=1&ID=" & IDFornitore.Value & "&IDCONN=" & lIdConnessione & "');</script>")
            'Else
            '    modificheEffettuate.Value = "0"
            'End If
            modificheEffettuate.Value = "0"
            iniziaTransazione()
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try
    End Sub

    Protected Sub Update()
        Try
            vIdFornitori = IDFornitore.Value
            riprendiConnessione()
            riprendiTransazione()

            Dim aliquota As String = par.PulisciStrSql(UCase(txtRitAcconto.Text))
            aliquota = Replace(aliquota, ".", ",")

            Dim idTipoModalitaPag As String = "NULL"
            Dim idTipoPagamento As String = "NULL"
            Dim idTipoRitenuta As String = "NULL"
            If cmbModalitaPagamento.SelectedValue <> "-1" Then
                idTipoModalitaPag = cmbModalitaPagamento.SelectedValue
            End If
            If cmbCondizionePagamento.SelectedValue <> "-1" Then
                idTipoPagamento = cmbCondizionePagamento.SelectedValue
            End If
            If cmbRitenuta.SelectedValue <> "-1" Then
                idTipoRitenuta = cmbRitenuta.SelectedValue
            End If

            '---------UPDATE FORNITORE----------'
            par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI SET COD_FORNITORE='" & par.PulisciStrSql(UCase(txtCodice.Text)) & "', " _
                & " RAGIONE_SOCIALE='" & par.PulisciStrSql(UCase(txtDenominazione.Text)) & "', " _
                & " COD_FISCALE='" & par.PulisciStrSql(UCase(txtCodiceFiscale.Text)) & "', " _
                & " PARTITA_IVA='" & par.PulisciStrSql(UCase(txtPiva.Text)) & "', " _
                & " ID_TIPO_MODALITA_PAG=" & idTipoModalitaPag & ", " _
                & " ID_TIPO_PAGAMENTO=" & idTipoPagamento & ", " _
                & " ID_TIPO_RITENUTA=" & idTipoRitenuta & ", " _
                & " TIPO='" & UCase(par.PulisciStrSql(TipoCollaborazione.Text)) & "', " _
                & " RIT_ACCONTO='" & par.PulisciStrSql(UCase(aliquota)) _
                & "' WHERE SISCOM_MI.FORNITORI.ID='" & vIdFornitori & "'"
            par.cmd.ExecuteNonQuery()
            '-----------------------------------'

            '------CHIUDO TRANSAZIONE-----------'
            chiudiTransazioneOK()
            '-----------------------------------'
            'par.cmd.Dispose()

            '--------MESSAGGIO----------------------------'

            'Response.Write("<script>alert('Fornitore modificato correttamente');</script>")
            '---------------------------------------------'

            'par.cmd.Dispose()

            'If IsNothing(Request.QueryString("CALL")) Then
            '    Response.Write("<script>location.replace('Fornitori.aspx?U=1&ID=" & IDFornitore.Value & "&IDCONN=" & lIdConnessione & "');</script>")
            'Else
            '    modificheEffettuate.Value = "0"
            'End If

            iniziaTransazione()
            modificheEffettuate.Value = "0"
            RadNotificationNote.Text = "Fornitore modificato correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try


    End Sub

    Protected Sub CaricaDati()

        Try
            If IDFornitore.Value <> "-1" Then
                classetab = "tabbertab"
                tabvisibility = "visible"

                riprendiTransazione()

                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_MODALITA_PAG ORDER BY DESCRIZIONE", cmbModalitaPagamento, "ID", "DESCRIZIONE", , , , , False)
                cmbModalitaPagamento.Enabled = False
                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTO ORDER BY DESCRIZIONE", cmbCondizionePagamento, "ID", "DESCRIZIONE", , , , , False)
                cmbCondizionePagamento.Enabled = False
                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_RITENUTA ORDER BY DESCRIZIONE", cmbRitenuta, "ID", "DESCRIZIONE", , , , , False)
                cmbRitenuta.Enabled = False

                '----------CARICO I DATI DEL FORNITORE SELEZIONATO--------'
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID='" & IDFornitore.Value & "' FOR UPDATE NOWAIT"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    txtCodice.Text = par.IfNull(myReader("COD_FORNITORE"), "")
                    txtDenominazione.Text = par.IfNull(myReader("RAGIONE_SOCIALE"), "")
                    txtCodiceFiscale.Text = par.IfNull(myReader("COD_FISCALE"), "")
                    txtPiva.Text = par.IfNull(myReader("PARTITA_IVA"), "")
                    txtRitAcconto.Text = par.IfNull(myReader("RIT_ACCONTO"), "")
                    cmbCondizionePagamento.SelectedValue = par.IfNull(myReader("ID_TIPO_PAGAMENTO"), "-1")
                    cmbModalitaPagamento.SelectedValue = par.IfNull(myReader("ID_TIPO_MODALITA_PAG"), "-1")
                    cmbRitenuta.SelectedValue = par.IfNull(myReader("ID_TIPO_RITENUTA"), "-1")
                    TipoCollaborazione.Text = UCase(par.IfNull(myReader("TIPO"), ""))
                End If
                myReader.Close()
                par.cmd.Dispose()
                '----------------------------------------------------------'

            Else
                '------------ERRORE NEL CARICAMENTO DEI DATI------------'
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Errore caricamento dati!');", True)
                '-------------------------------------------------------'
            End If

        Catch exOracle As Oracle.DataAccess.Client.OracleException
            If exOracle.Number = 54 Then


                '---LA RISORSA è OCCUPATA DA UN ALTRO UTENTE---'
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Fornitore aperto da un altro utente. Non è possibile effettuare modifiche!');", True)
                'Response.Write("<script>alert('Fornitore aperto da un altro utente. Non è possibile effettuare modifiche!');</script>")
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID='" & IDFornitore.Value & "'"

                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then

                    txtCodice.Text = par.IfNull(myReader("COD_FORNITORE"), "")
                    txtDenominazione.Text = par.IfNull(myReader("RAGIONE_SOCIALE"), "")
                    txtCodiceFiscale.Text = par.IfNull(myReader("COD_FISCALE"), "")
                    txtPiva.Text = par.IfNull(myReader("PARTITA_IVA"), "")
                    txtRitAcconto.Text = par.IfNull(myReader("RIT_ACCONTO"), "")

                End If
                myReader.Close()
                par.cmd.Dispose()

                '----DISABILITARE I TASTI DI MODIFICA ED ELIMINAZIONE----'
                btnElimina.Visible = False
                btnSalva.Visible = False
                modificheEffettuate.Value = "0"
                modalitaSOLALETTURA.Value = "1"
                chiudiConnessione()
                visualizzaSL()

            End If

        Catch ex As Exception

            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")

        End Try

    End Sub

    Protected Sub CaricaDatiSolaLettura()
        Try
            If IDFornitore.Value <> "-1" Then
                modalitaSOLALETTURA.Value = "1"
                If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                    Session.Remove("LAVORAZIONE")
                End If

                classetab = "tabbertab"
                tabvisibility = "visible"
                par.OracleConn.Open()
                par.cmd = par.OracleConn.CreateCommand

                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_MODALITA_PAG ORDER BY DESCRIZIONE", cmbModalitaPagamento, "ID", "DESCRIZIONE", , , , , False)
                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTO ORDER BY DESCRIZIONE", cmbCondizionePagamento, "ID", "DESCRIZIONE", , , , , False)
                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_RITENUTA ORDER BY DESCRIZIONE", cmbRitenuta, "ID", "DESCRIZIONE", , , , , False)

                '----------CARICO I DATI DEL FORNITORE SELEZIONATO--------'
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID='" & IDFornitore.Value & "'"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtCodice.Text = par.IfNull(myReader("COD_FORNITORE"), "")
                    txtDenominazione.Text = par.IfNull(myReader("RAGIONE_SOCIALE"), "")
                    txtCodiceFiscale.Text = par.IfNull(myReader("COD_FISCALE"), "")
                    txtPiva.Text = par.IfNull(myReader("PARTITA_IVA"), "")
                    txtRitAcconto.Text = par.IfNull(myReader("RIT_ACCONTO"), "")
                    cmbCondizionePagamento.SelectedValue = par.IfNull(myReader("ID_TIPO_PAGAMENTO"), "-1")
                    cmbModalitaPagamento.SelectedValue = par.IfNull(myReader("ID_TIPO_MODALITA_PAG"), "-1")
                    cmbRitenuta.SelectedValue = par.IfNull(myReader("ID_TIPO_RITENUTA"), "-1")
                    TipoCollaborazione.Text = UCase(par.IfNull(myReader("TIPO"), ""))
                End If
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '----------------------------------------------------------'
            Else
                '------------ERRORE NEL CARICAMENTO DEI DATI------------'
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Errore caricamento dati!');", True)
                '-------------------------------------------------------'
            End If
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try

    End Sub

    Protected Function controllaCampi() As Boolean
        errorealiquota.Visible = False
        errorecodicefornitore.Visible = False
        erroredenominazione.Visible = False
        errorecodicefiscale.Visible = False
        errorepartitaiva.Visible = False

        '---EVENTUALI CONTROLLI DA AGGIUNGERE PER I CAMPI INSERITI---'



        Dim errore As Boolean = False
        Dim cf As String = Trim(txtCodiceFiscale.Text)
        Dim partitaIVA As String = Trim(txtPiva.Text)
        Dim codicefornitore As String = Trim(txtCodice.Text)

        Dim denominazione As String = Trim(txtDenominazione.Text)
        Dim aliquota As String = Trim(txtRitAcconto.Text)
        Dim aliquotaN As Double = 0


        If IDFornitore.Value = "-1" Then
            'INSERIMENTO, CONTROLLO CAMPI



            '---CONTROLLO CODICE FORNITORE---'

            If Len(codicefornitore) = 0 Then
                errore = True
                errorecodicefornitore.Text = "Codice fornitore obbligatorio"
                errorecodicefornitore.Visible = True
            Else

                'Dim espressioneRegolareCodice As String = "^\d{1,}$"

                'If Not Regex.IsMatch(codicefornitore, espressioneRegolareCodice) Then
                'errore = True
                'errorecodicefornitore.Text = "Codice fornitore deve essere numerico"
                'errorecodicefornitore.Visible = True
                'Else
                'CONTROLLO CODICE FORNITORE GIà PRESENTE
                apriConnessione()
                Try
                    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI WHERE COD_FORNITORE='" & par.PulisciStrSql(codicefornitore) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim codfSN As Integer = 0
                    If myReader.Read Then
                        If par.IfNull(myReader(0), 0) <> 0 Then
                            codfSN = 1
                        End If
                    End If
                    myReader.Close()

                    If codfSN = 1 Then
                        errorecodicefornitore.Text = "Codice fornitore già associato ad un altro fornitore"
                        errore = True
                        errorecodicefornitore.Visible = True
                    Else
                        errorecodicefornitore.Visible = False
                    End If
                Catch ex As Exception
                    errorecodicefornitore.Text = "Codice fornitore già associato ad un altro fornitore"
                    errore = True
                    errorecodicefornitore.Visible = True
                End Try

                par.cmd.Dispose()
                chiudiConnessione()

                'End If

            End If


            '---CONTROLLO DENOMINAZIONE OBBLIGATORIA---'
            If Len(denominazione) = 0 Then
                errore = True
                erroredenominazione.Text = "Denominazione obbligatoria"
                erroredenominazione.Visible = True

            End If


            If Len(aliquota) > 0 Then
                '---CONTROLLO ALIQUOTA---'
                Try
                    '---SOSTITUZIONE . CON , PER I VALORI DECIMALI---'
                    aliquota = Replace(aliquota, ".", ",")
                    aliquotaN = CDbl(aliquota)
                    If aliquotaN >= 0 And aliquotaN <= 100 Then
                        errorealiquota.Visible = False
                    Else
                        errorealiquota.Visible = True
                        errore = True
                    End If
                Catch ex As Exception
                    errore = True
                    errorealiquota.Visible = True
                End Try
            End If

            'apriConnessione()

            '######### CONTROLLO CODICE FISCALE #########

            If Len(cf) = 16 Then
                '---CONTROLLO CODICE FISCALE---'
                If par.ControllaCF(cf) = False Then
                    errore = True
                    errorecodicefiscale.Visible = True
                Else
                    errorecodicefiscale.Visible = False

                    '######################################################
                    ''CONTROLLO CHE IL CODICE FISCALE NON SIA STATO GIà INSERITO
                    'Try
                    '    par.cmd.CommandText = "SELECT COUNT(COD_FISCALE) FROM SISCOM_MI.FORNITORI WHERE COD_FISCALE='" & cf & "'"
                    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    Dim cfSN As Integer = 0
                    '    If myReader.Read Then
                    '        If par.IfNull(myReader(0), 0) <> 0 Then
                    '            cfSN = 1

                    '        End If
                    '    End If
                    '    myReader.Close()

                    '    If cfSN = 1 Then
                    '        errorecodicefiscale.Text = "Codice fiscale associato ad un altro fornitore"
                    '        errore = True
                    '        errorecodicefiscale.Visible = True
                    '    Else
                    '        errorecodicefiscale.Visible = False
                    '    End If

                    'Catch ex As Exception
                    '    errore = True
                    '    errorecodicefiscale.Visible = True
                    'End Try
                    '#######################################################

                End If
                '------------------------------'
            ElseIf Len(cf) = 11 Then
                '---CONTROLLO PARTITA IVA---'
                If par.ControllaPIVA(cf) = False Then
                    errore = True
                    errorecodicefiscale.Visible = True
                Else

                    errorecodicefiscale.Visible = False

                    '#########################################################
                    ''CONTROLLO CHE IL CODICE FISCALE NON SIA STATO GIà INSERITO
                    'Try
                    '    par.cmd.CommandText = "SELECT COUNT(COD_FISCALE) FROM SISCOM_MI.FORNITORI WHERE COD_FISCALE='" & cf & "'"
                    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    Dim cfSN As Integer = 0
                    '    If myReader.Read Then
                    '        If par.IfNull(myReader(0), 0) <> 0 Then
                    '            cfSN = 1

                    '        End If
                    '    End If
                    '    myReader.Close()

                    '    If cfSN = 1 Then
                    '        errorecodicefiscale.Text = "Codice fiscale associato ad un altro fornitore"
                    '        errore = True
                    '        errorecodicefiscale.Visible = True
                    '    Else
                    '        errorecodicefiscale.Visible = False
                    '    End If

                    'Catch ex As Exception
                    '    errore = True
                    '    errorecodicefiscale.Visible = True
                    'End Try
                    '##########################################################


                End If
                '------------------------------'
            ElseIf Len(cf) = 0 Then
                '---NON SONO CAMPI OBBLIGATORI---'
                errorecodicefiscale.Visible = False
            Else
                errore = True
                errorecodicefiscale.Visible = True
            End If

            '####################################

            '############## PIVA ################
            If Len(partitaIVA) = 11 Then
                '---CONTROLLO PARTITA IVA---'
                If par.ControllaPIVA(partitaIVA) = False Then
                    errore = True
                    errorepartitaiva.Visible = True
                Else
                    errorepartitaiva.Visible = False
                    '#######################################################
                    ''CONTROLLO CHE LA PIVA NON SIA STATA GIà INSERITA
                    'Try
                    '    par.cmd.CommandText = "SELECT COUNT(PARTITA_IVA) FROM SISCOM_MI.FORNITORI WHERE PARTITA_IVA='" & partitaIVA & "'"
                    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    Dim pivaSN As Integer = 0
                    '    If myReader.Read Then
                    '        If par.IfNull(myReader(0), 0) <> 0 Then
                    '            pivaSN = 1

                    '        End If
                    '    End If
                    '    myReader.Close()

                    '    If pivaSN = 1 Then
                    '        errorepartitaiva.Text = "Partita IVA associata ad un altro fornitore"
                    '        errore = True
                    '        errorepartitaiva.Visible = True
                    '    Else
                    '        errorepartitaiva.Visible = False
                    '    End If
                    'Catch ex As Exception
                    '    errore = True
                    '    errorepartitaiva.Visible = True
                    'End Try
                    '#######################################################
                End If

                '------------------------------'
            ElseIf Len(partitaIVA) = 0 Then
                '---NON SONO CAMPI OBBLIGATORI---'
                errorepartitaiva.Visible = False
            Else
                errore = True
                errorepartitaiva.Visible = True
            End If



            '#########################################################
            ''########## CONTROLLO RAGIONE SOCIALE FORNITORE #########
            'If Len(partitaIVA) = 0 And Len(cf) = 0 Then
            '    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI WHERE RAGIONE_SOCIALE='" & par.PulisciStrSql(denominazione) & "'"

            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '    Dim ragSN As Integer = 0
            '    If myReader.Read Then
            '        If par.IfNull(myReader(0), 0) <> 0 Then
            '            ragSN = 1
            '        End If

            '    End If
            '    myReader.Close()
            '    If ragSN = 1 Then
            '        If errore = False Then
            '            'erroredenominazione.Text = "Fornitore già presente"
            '            'erroredenominazione.Visible = True
            '            Response.Write("<script>alert('Attenzione: il fornitore che si vuole inserire è già presente!');</script>")
            '        End If
            '        errore = True

            '    End If


            'Else
            '    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI WHERE RAGIONE_SOCIALE='" & par.PulisciStrSql(denominazione) & "' AND (PARTITA_IVA='" & par.PulisciStrSql(partitaIVA) & "' OR COD_FISCALE='" & par.PulisciStrSql(cf) & "')"

            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '    Dim ragSN As Integer = 0
            '    If myReader.Read Then
            '        If par.IfNull(myReader(0), 0) <> 0 Then
            '            ragSN = 1
            '        End If

            '    End If
            '    myReader.Close()
            '    If ragSN = 1 Then
            '        If errore = False Then
            '            'erroredenominazione.Text = "Fornitore già presente"
            '            'erroredenominazione.Visible = True
            '            Response.Write("<script>alert('Attenzione: il fornitore che si vuole inserire è già presente!');</script>")
            '        End If
            '        errore = True
            '    End If


            'End If

            'par.cmd.Dispose()
            'chiudiConnessione()
            '#########################################################

        Else
            'MODIFICA



            '---CONTROLLO CODICE FORNITORE---'

            If Len(codicefornitore) = 0 Then
                errore = True
                errorecodicefornitore.Text = "Codice fornitore obbligatorio"
                errorecodicefornitore.Visible = True
            Else

                'Dim espressioneRegolareCodice As String = "^\d{1,}$"

                'If Not Regex.IsMatch(codicefornitore, espressioneRegolareCodice) Then
                '   errore = True
                '   errorecodicefornitore.Text = "Codice fornitore deve essere numerico"
                '   errorecodicefornitore.Visible = True
                'Else
                'CONTROLLO CODICE FORNITORE GIà PRESENTE

                riprendiConnessione()
                riprendiTransazione()

                Try
                    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI WHERE COD_FORNITORE='" & par.PulisciStrSql(codicefornitore) & "' AND ID<>'" & par.PulisciStrSql(IDFornitore.Value) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim codfSN As Integer = 0
                    If myReader.Read Then
                        If par.IfNull(myReader(0), 0) <> 0 Then
                            codfSN = 1
                        End If
                    End If
                    myReader.Close()

                    If codfSN = 1 Then
                        errorecodicefornitore.Text = "Codice fornitore già associato ad un altro fornitore"
                        errore = True
                        errorecodicefornitore.Visible = True
                    Else
                        errorecodicefornitore.Visible = False
                    End If
                Catch ex As Exception
                    errorecodicefornitore.Text = "Codice fornitore già associato ad un altro fornitore"
                    errore = True
                    errorecodicefornitore.Visible = True
                End Try

                par.cmd.Dispose()


                'End If

            End If




            '---CONTROLLO DENOMINAZIONE OBBLIGATORIA---'
            If Len(denominazione) = 0 Then
                errore = True
                erroredenominazione.Text = "Denominazione obbligatoria"
                erroredenominazione.Visible = True

            End If


            If Len(aliquota) > 0 Then
                '---CONTROLLO ALIQUOTA---'
                Try
                    '---SOSTITUZIONE . CON , PER I VALORI DECIMALI---'
                    aliquota = Replace(aliquota, ".", ",")
                    aliquotaN = CDbl(aliquota)
                    If aliquotaN >= 0 And aliquotaN <= 100 Then
                        errorealiquota.Visible = False
                    Else
                        errorealiquota.Visible = True
                        errore = True
                    End If
                Catch ex As Exception
                    errore = True
                    errorealiquota.Visible = True
                End Try
            End If


            'riprendiConnessione()
            'riprendiTransazione()


            '######### CONTROLLO CODICE FISCALE #########

            If Len(cf) = 16 Then
                '---CONTROLLO CODICE FISCALE---'
                If par.ControllaCF(cf) = False Then
                    errore = True
                    errorecodicefiscale.Visible = True
                Else

                    errorecodicefiscale.Visible = False

                    '###########################################################
                    ''CONTROLLO CHE IL CODICE FISCALE NON SIA STATO GIà INSERITO
                    'Try
                    '    par.cmd.CommandText = "SELECT COUNT(COD_FISCALE) FROM SISCOM_MI.FORNITORI WHERE COD_FISCALE='" & par.PulisciStrSql(cf) & "' AND ID<>'" & IDFornitore.Value & "'"
                    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    Dim cfSN As Integer = 0
                    '    If myReader.Read Then
                    '        If par.IfNull(myReader(0), 0) <> 0 Then
                    '            cfSN = 1

                    '        End If
                    '    End If
                    '    myReader.Close()

                    '    If cfSN = 1 Then
                    '        errorecodicefiscale.Text = "Codice fiscale associato ad un altro fornitore"
                    '        errore = True
                    '        errorecodicefiscale.Visible = True
                    '    Else
                    '        errorecodicefiscale.Visible = False
                    '    End If

                    'Catch ex As Exception
                    '    errore = True
                    '    errorecodicefiscale.Visible = True
                    'End Try
                    '###########################################################

                End If
                '------------------------------'
            ElseIf Len(cf) = 11 Then
                '---CONTROLLO PARTITA IVA---'
                If par.ControllaPIVA(cf) = False Then
                    errore = True
                    errorecodicefiscale.Visible = True
                Else

                    errorecodicefiscale.Visible = False
                    '#########################################################
                    ''CONTROLLO CHE IL CODICE FISCALE NON SIA STATO GIà INSERITO
                    'Try
                    '    par.cmd.CommandText = "SELECT COUNT(COD_FISCALE) FROM SISCOM_MI.FORNITORI WHERE COD_FISCALE='" & par.PulisciStrSql(cf) & "' AND ID<>'" & IDFornitore.Value & "'"
                    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    Dim cfSN As Integer = 0
                    '    If myReader.Read Then
                    '        If par.IfNull(myReader(0), 0) <> 0 Then
                    '            cfSN = 1

                    '        End If
                    '    End If
                    '    myReader.Close()

                    '    If cfSN = 1 Then
                    '        errorecodicefiscale.Text = "Codice fiscale associato ad un altro fornitore"
                    '        errore = True
                    '        errorecodicefiscale.Visible = True
                    '    Else
                    '        errorecodicefiscale.Visible = False
                    '    End If

                    'Catch ex As Exception
                    '    errore = True
                    '    errorecodicefiscale.Visible = True
                    'End Try
                    '#########################################################

                End If
                '------------------------------'
            ElseIf Len(cf) = 0 Then
                '---NON SONO CAMPI OBBLIGATORI---'
                errorecodicefiscale.Visible = False
            Else
                errore = True
                errorecodicefiscale.Visible = True
            End If

            '####################################

            '############## PIVA ################
            If Len(partitaIVA) = 11 Then
                '---CONTROLLO PARTITA IVA---'
                If par.ControllaPIVA(partitaIVA) = False Then
                    errore = True
                    errorepartitaiva.Visible = True
                Else

                    errorepartitaiva.Visible = False

                    '################################################
                    ''CONTROLLO CHE LA PIVA NON SIA STATA GIà INSERITA
                    'Try
                    '    par.cmd.CommandText = "SELECT COUNT(PARTITA_IVA) FROM SISCOM_MI.FORNITORI WHERE PARTITA_IVA='" & par.PulisciStrSql(partitaIVA) & "' AND ID<>'" & IDFornitore.Value & "'"
                    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    '    Dim pivaSN As Integer = 0
                    '    If myReader.Read Then
                    '        If par.IfNull(myReader(0), 0) <> 0 Then
                    '            pivaSN = 1

                    '        End If
                    '    End If
                    '    myReader.Close()

                    '    If pivaSN = 1 Then
                    '        errorepartitaiva.Text = "Partita IVA associata ad un altro fornitore"
                    '        errore = True
                    '        errorepartitaiva.Visible = True
                    '    Else
                    '        errorepartitaiva.Visible = False
                    '    End If
                    'Catch ex As Exception
                    '    errore = True
                    '    errorepartitaiva.Visible = True
                    'End Try
                    '################################################


                End If

                '------------------------------'
            ElseIf Len(partitaIVA) = 0 Then
                '---NON SONO CAMPI OBBLIGATORI---'
                errorepartitaiva.Visible = False
            Else
                errore = True
                errorepartitaiva.Visible = True
            End If

            '######################################

            '######################################
            ''########## CONTROLLO RAGIONE SOCIALE FORNITORE #########
            'If Len(partitaIVA) = 0 And Len(cf) = 0 Then
            '    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI WHERE RAGIONE_SOCIALE='" & par.PulisciStrSql(denominazione) & "' AND ID<>'" & IDFornitore.Value & "'"

            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '    Dim ragSN As Integer = 0
            '    If myReader.Read Then
            '        If par.IfNull(myReader(0), 0) <> 0 Then
            '            ragSN = 1
            '        End If

            '    End If
            '    myReader.Close()
            '    If ragSN = 1 Then
            '        If errore = False Then
            '            'erroredenominazione.Text = "Fornitore già presente"
            '            'erroredenominazione.Visible = True
            '            Response.Write("<script>alert('Attenzione: il fornitore che si vuole inserire è già presente!');</script>")
            '        End If
            '        errore = True
            '    End If
            'Else
            '    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.FORNITORI WHERE RAGIONE_SOCIALE='" & par.PulisciStrSql(denominazione) & "' AND (PARTITA_IVA='" & par.PulisciStrSql(partitaIVA) & "' OR COD_FISCALE='" & par.PulisciStrSql(cf) & "') AND ID<>'" & IDFornitore.Value & "'"
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '    Dim ragSN As Integer = 0
            '    If myReader.Read Then
            '        If par.IfNull(myReader(0), 0) <> 0 Then
            '            ragSN = 1
            '        End If

            '    End If
            '    myReader.Close()
            '    If ragSN = 1 Then
            '        If errore = False Then
            '            'erroredenominazione.Text = "Fornitore già presente"
            '            'erroredenominazione.Visible = True
            '            Response.Write("<script>alert('Attenzione: il fornitore che si vuole inserire è già presente!');</script>")
            '        End If
            '        errore = True
            '    End If
            'End If
            'par.cmd.Dispose()

            '#######################################

        End If

        Return Not errore

    End Function

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        '------ELIMINAZIONE DEL FORNITORE------'
        Try
            If confermaEliminazioneFornitore.Value = "1" Then
                If IDFornitore.Value <> "-1" Then
                    riprendiConnessione()
                    riprendiTransazione()
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID='" & IDFornitore.Value & "'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.Dispose()

                    '----CHIUSURA TRANSAZIONE E CONNESSIONE----'
                    chiudiTransazioneOK()

                    par.OracleConn.Close()
                    HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                        Session.Remove("LAVORAZIONE")
                    End If


                    '---MESSAGGIO E REDIRECT IN RICERCA---'
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Fornitore eliminato correttamente');", True)

                End If
            End If

        Catch exOracle As Oracle.DataAccess.Client.OracleException
            If exOracle.Number = 2292 Then
                'VINCOLO INTEGRITà REFERENZIALE
                '---MESSAGGIO---'
                'ELIMINAZIONE NON POSSIBILE PER I VINCOLI DI INTEGRITà REFERENZIALE
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "alert('Il fornitore selezionato non può essere eliminato');", True)


            End If

        Catch ex As Exception

            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../../Errore.aspx")
        End Try
    End Sub



    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        If confermaUscita.Value = "1" Then
            If IDFornitore.Value = "-1" Or modalitaSOLALETTURA.Value = "1" Then
                If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                    Session.Remove("LAVORAZIONE")
                End If
                Select Case Request.QueryString("CALL")
                    Case "COND"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "self.close();", True)
                    Case Else
                        Response.Redirect("../../pagina_home_ncp.aspx")
                End Select
            Else
                '----RIMUOVO VARIABILE DI SESSIONE LAVORAZIONE E REDIRECT IN HOME------'
                chiudiConnessione()
                If Not IsNothing(Session.Item("LAVORAZIONE")) Then
                    Session.Remove("LAVORAZIONE")
                End If
                If Request.QueryString("CALL") = "COND" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "self.close();", True)
                Else
                    Response.Redirect("../../pagina_home_ncp.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        If daInserimento.Value = "3" Then
            'DA PAGINA ESTERNA SONO IN SOLA LETTURA, QUINDI CHIUDO LA PAGINA APERTA
            Response.Write("<script>self.close();</script>")

        Else
            indietro.Value = "1"
            If confermaIndietro.Value = "1" Then
                If Session.Item("LAVORAZIONE") = 1 Then
                    chiudiConnessione()
                    If daInserimento.Value = "1" Or daInserimento.Value = "2" Then
                        Response.Redirect("../../pagina_home_ncp.aspx")
                    Else

                        Dim paramRicerca As String = ""
                        If IsNothing(Request.QueryString("CF")) Then
                            Response.Redirect("RicercaFornitore.aspx")
                        Else
                            paramRicerca = paramRicerca & "CF=" & Request.QueryString("CF")
                            paramRicerca = paramRicerca & "&CO=" & Request.QueryString("CO")
                            paramRicerca = paramRicerca & "&RA=" & Request.QueryString("RA")
                            paramRicerca = paramRicerca & "&PI=" & Request.QueryString("PI")
                            Response.Redirect("RisultatiFornitori.aspx?" & paramRicerca)
                        End If

                    End If
                Else
                    If Session.Item("BP_CC_L") = "1" Or modalitaSOLALETTURA.Value = "1" Then
                        Dim paramRicerca As String = ""
                        If IsNothing(Request.QueryString("CF")) Then
                            Response.Redirect("RicercaFornitore.aspx")
                        Else
                            paramRicerca = paramRicerca & "CF=" & Request.QueryString("CF")
                            paramRicerca = paramRicerca & "&CO=" & Request.QueryString("CO")
                            paramRicerca = paramRicerca & "&RA=" & Request.QueryString("RA")
                            paramRicerca = paramRicerca & "&PI=" & Request.QueryString("PI")
                            Response.Redirect("RisultatiFornitori.aspx?" & paramRicerca)
                        End If
                    Else
                        Response.Redirect("../../pagina_home_ncp.aspx")
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        tabdefault1 = ""
        tabdefault2 = ""
        Select Case txttab.Value
            Case "1"
                tabdefault1 = "tabbertabdefault"
            Case "2"
                tabdefault2 = "tabbertabdefault"
        End Select
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class