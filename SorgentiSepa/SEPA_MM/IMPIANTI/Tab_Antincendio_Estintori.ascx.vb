'*** 'TAB ELENCO ESTINTORI + Verifiche del'IMPIANTO ANTINCENDIO
Imports System.Collections

Partial Class Tab_Antincendio_Estintori
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global
    Dim bVerifica As Boolean

    Dim lstEstintori As System.Collections.Generic.List(Of Epifani.Estintori)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstEstintori = CType(HttpContext.Current.Session.Item("LSTESTINTORI"), System.Collections.Generic.List(Of Epifani.Estintori))

        Try
            If Not IsPostBack Then

                lstEstintori.Clear()

                bVerifica = False
                vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                End If
                ''''''''''''''''''''''''''

                BindGrid_Estintori()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text



            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub


    Private Property vIdImpianto() As Long
        Get
            If Not (ViewState("par_idImpianto") Is Nothing) Then
                Return CLng(ViewState("par_idImpianto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idImpianto") = value
        End Set

    End Property

    Public Property IdConnessione() As String
        Get
            If Not (ViewState("par_Connessione") Is Nothing) Then
                Return CStr(ViewState("par_Connessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Connessione") = value
        End Set

    End Property


    'CARICAMENTO GRIGLIA ESTINTORI + VERIFICHE 
    Private Sub BindGrid_Estintori()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        '& "DECODE(ES_PRESCRIZIONE,'S','SI','N','NO','P','PARZIALE') AS ""ES_PRESCRIZIONE"" " 

        'NOTA TIPO=ES (ANTINCENDIO VERIFICHE ESTINTORI)
        StringaSql = "  select SISCOM_MI.I_ANT_ESTINTORI.ID,SISCOM_MI.I_ANT_ESTINTORI.ESTINTORI, " _
                        & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                        & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                        & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                        & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                    & " from SISCOM_MI.I_ANT_ESTINTORI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                    & " where SISCOM_MI.I_ANT_ESTINTORI.ID_IMPIANTO=" & vIdImpianto _
                        & " and SISCOM_MI.I_ANT_ESTINTORI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                        & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ES' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                        & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) " _
                    & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "I_ANT_ESTINTORI")

        DataGridE.DataSource = ds
        DataGridE.DataBind()

        ds.Dispose()

    End Sub



    'GRID ESTINTORI
    Protected Sub DataGridE_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridE.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Estintori_txtSelE').value='Hai selezionato: ESTINTORI: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Antincendio_Estintori_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Estintori_txtSelE').value='Hai selezionato: ESTINTORI: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Antincendio_Estintori_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub



    Function ControlloCampi() As Boolean

        Try

            ControlloCampi = True

            ' NUMERO ESTINTORI
            If PAR.IfEmpty(Me.txtNumEstintori.Text, "Null") = "Null" Then
                Response.Write("<script>alert('Inserire il numero di estintori!');</script>")
                ControlloCampi = False
                txtNumEstintori.Focus()
                Exit Function
            End If
            If Me.txtData.Text = "dd/mm/YYYY" Then
                Me.txtData.Text = ""
            End If


            If Me.txtDataScadenza.Text = "dd/mm/YYYY" Then
                Me.txtDataScadenza.Text = ""
            End If


            If PAR.IfEmpty(Me.txtData.Text, "Null") <> "Null" Then
                ' Se la data di verifica è piena allora è obbligatorio 

                ' LA DITTA
                If PAR.IfEmpty(Me.txtDitta.Text, "Null") = "Null" Then
                    Response.Write("<script>alert('Inserire la Ditta!');</script>")
                    ControlloCampi = False
                    txtDitta.Focus()
                    Exit Function
                End If

                ' L'ESITO
                If Me.cmbEsito.SelectedValue = -1 Then
                    Response.Write("<script>alert('Inserire l\'esito!');</script>")
                    ControlloCampi = False
                    cmbEsito.Focus()
                    Exit Function
                End If

                ' se VALIDITA' è vuota defaiul 12 mesi
                If Strings.Trim(Me.txtValidita.Text) = "" Then
                    Me.txtValidita.Text = 12
                End If

                ' se PRE-ALLARME è vuota defaiul 12 mesi
                If Strings.Trim(Me.cmbPreAllarme.Text) = "" Then
                    Me.cmbPreAllarme.Text = 6
                End If

                bVerifica = True
            End If


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Function

    Protected Sub btn_InserisciE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciE.Click
        If ControlloCampi() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDE.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.Salva()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.Update()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelE.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiE.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Me.txtDitta.Enabled = True
        Me.txtNote.Enabled = True
        Me.cmbEsito.Enabled = True

        Me.txtData.Enabled = True
        Me.txtDataScadenza.Enabled = True
        Me.txtValidita.Enabled = True
        Me.cmbPreAllarme.Enabled = True

        Me.btn_InserisciE.Visible = True
        Me.ImageAllarm.Visible = True

        txtSelE.Text = ""
        txtIdComponente.Text = ""


    End Sub


    Private Sub Salva()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Estintori

                gen = New Epifani.Estintori(lstEstintori.Count, PAR.IfEmpty(Me.txtNumEstintori.Text, 0), Me.txtIDV.Text, PAR.PulisciStringaInvio(Me.txtDitta.Text, 100), Me.txtData.Text, Me.txtNote.Text, Me.cmbEsito.SelectedItem.Text, PAR.IfEmpty(Me.txtValidita.Text, 0), txtDataScadenza.Text, PAR.IfEmpty(cmbEsito.SelectedItem.Value, 0), PAR.IfEmpty(Me.cmbPreAllarme.SelectedItem.Text, 0), "ES", "")

                DataGridE.DataSource = Nothing
                lstEstintori.Add(gen)
                gen = Nothing

                DataGridE.DataSource = lstEstintori
                DataGridE.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                'INSERISCO IL NUOVO ESTINTORE
                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_ESTINTORI (ID,ID_IMPIANTO, ESTINTORI) " _
                                & " values (SISCOM_MI.SEQ_I_ANT_ESTINTORI.NEXTVAL,:id_impianto,:estintori)"

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("estintori", strToNumber(Me.txtNumEstintori.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()


                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Inserimento ESTINTORI")


                '**** Ricavo ID dell'ESTINTORE
                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_ESTINTORI.CURRVAL FROM dual "
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderI.Read Then
                    Me.txtIDE.Text = myReaderI(0)
                End If
                myReaderI.Close()
                PAR.cmd.CommandText = ""
                '**********

                GestioneVerifiche()

                BindGrid_Estintori()
            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"


            If vIdImpianto <> -1 Then PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub Update()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO


                lstEstintori(txtIdComponente.Text).ESTINTORI = Me.txtNumEstintori.Text

                lstEstintori(txtIdComponente.Text).ID_VERIFICA = txtIDV.Text
                lstEstintori(txtIdComponente.Text).DITTA = PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)
                lstEstintori(txtIdComponente.Text).NOTE = Me.txtNote.Text
                lstEstintori(txtIdComponente.Text).ESITO = PAR.IfEmpty(Me.cmbEsito.SelectedValue, -1)
                lstEstintori(txtIdComponente.Text).ESITO_DETTAGLIO = Me.cmbEsito.SelectedItem.ToString

                lstEstintori(txtIdComponente.Text).DATA = Me.txtData.Text
                lstEstintori(txtIdComponente.Text).MESI_VALIDITA = PAR.IfEmpty(Me.txtValidita.Text, 12)
                lstEstintori(txtIdComponente.Text).MESI_PREALLARME = Me.cmbPreAllarme.SelectedValue.ToString
                lstEstintori(txtIdComponente.Text).DATA_SCADENZA = Me.txtDataScadenza.Text

                lstEstintori(txtIdComponente.Text).TIPO = "ES"

                DataGridE.DataSource = lstEstintori
                DataGridE.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                PAR.cmd.CommandText = "update SISCOM_MI.I_ANT_ESTINTORI set ESTINTORI=:estintori " _
                                   & " where ID=" & Me.txtIDE.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("estintori", strToNumber(Me.txtNumEstintori.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Modifica ESTINTORI")


                GestioneVerifiche()

                BindGrid_Estintori()

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdImpianto <> -1 Then PAR.myTrans.Rollback()

            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnAggE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggE.Click
        Try


            Me.txtIDE.Text = -1
            Me.txtIDV.Text = -1

            Me.txtNumEstintori.Text = ""

            Me.txtDitta.Text = ""
            Me.txtNote.Text = ""
            Me.cmbEsito.SelectedValue = ""

            Me.txtData.Text = "" 'Format(Now(), "dd/MM/yyyy")
            Me.txtDataScadenza.Text = ""

            Me.txtValidita.Text = ""
            Me.cmbPreAllarme.SelectedValue = ""
            Me.ImageAllarm.Visible = False
            'CalcolaAllarme()


        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnApriE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriE.Click

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO



                    Me.txtIDE.Text = lstEstintori(txtIdComponente.Text).ID
                    Me.txtNumEstintori.Text = PAR.IfNull(lstEstintori(txtIdComponente.Text).ESTINTORI, "")

                    Me.txtIDV.Text = lstEstintori(txtIdComponente.Text).ID_VERIFICA
                    Me.txtDitta.Text = PAR.IfNull(lstEstintori(txtIdComponente.Text).DITTA, "")
                    Me.txtNote.Text = PAR.IfNull(lstEstintori(txtIdComponente.Text).NOTE, "")


                    If PAR.IfNull(lstEstintori(txtIdComponente.Text).ESITO_DETTAGLIO, "") <> "" Then
                        Me.cmbEsito.SelectedValue = lstEstintori(txtIdComponente.Text).ESITO
                    Else
                        Me.cmbEsito.SelectedValue = ""
                    End If


                    Me.txtData.Text = PAR.FormattaData(lstEstintori(txtIdComponente.Text).DATA)
                    Me.txtDataScadenza.Text = PAR.FormattaData(lstEstintori(txtIdComponente.Text).DATA_SCADENZA)

                    If PAR.IfNull(lstEstintori(txtIdComponente.Text).MESI_VALIDITA, 0) = 0 Then
                        Me.txtValidita.Text = ""
                    Else
                        Me.txtValidita.Text = PAR.IfNull(lstEstintori(txtIdComponente.Text).MESI_VALIDITA, "")
                    End If

                    If PAR.IfNull(lstEstintori(txtIdComponente.Text).MESI_PREALLARME, 0) = 0 Then
                        Me.cmbPreAllarme.SelectedValue = ""
                    Else
                        Me.cmbPreAllarme.SelectedValue = lstEstintori(txtIdComponente.Text).MESI_PREALLARME
                    End If

                Else
                    '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                    If PAR.OracleConn.State = Data.ConnectionState.Open Then
                        Response.Write("IMPOSSIBILE VISUALIZZARE")
                        Exit Sub
                    Else
                        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)
                        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                    End If

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select SISCOM_MI.I_ANT_ESTINTORI.ID,SISCOM_MI.I_ANT_ESTINTORI.ESTINTORI," _
                                            & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                            & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                        & " from SISCOM_MI.I_ANT_ESTINTORI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                        & " where SISCOM_MI.I_ANT_ESTINTORI.ID = " & txtIdComponente.Text _
                                            & " and SISCOM_MI.I_ANT_ESTINTORI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                            & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ES' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                            & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) "

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then


                        Me.txtIDE.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtNumEstintori.Text = PAR.IfNull(myReader1("ESTINTORI"), "")

                        Me.txtIDV.Text = PAR.IfNull(myReader1("ID_VERIFICA"), -1)
                        Me.txtDitta.Text = PAR.IfNull(myReader1("DITTA"), "")
                        Me.txtNote.Text = PAR.IfNull(myReader1("NOTE"), "")

                        If PAR.IfNull(myReader1("ESITO_DETTAGLIO"), "") <> "" Then
                            Me.cmbEsito.SelectedValue = PAR.IfNull(myReader1("ESITO"), "")
                        Else
                            Me.cmbEsito.SelectedValue = ""
                        End If

                        Me.txtData.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA"), ""))
                        Me.txtDataScadenza.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_SCADENZA"), ""))

                        If PAR.IfNull(myReader1("MESI_VALIDITA"), 0) = 0 Then
                            Me.txtValidita.Text = ""
                        Else
                            Me.txtValidita.Text = PAR.IfNull(myReader1("MESI_VALIDITA"), "")
                        End If

                        If PAR.IfNull(myReader1("MESI_PREALLARME"), 0) = 0 Then
                            Me.cmbPreAllarme.SelectedValue = ""
                        Else
                            Me.cmbPreAllarme.SelectedValue = PAR.IfNull(myReader1("MESI_PREALLARME"), "")
                        End If

                    End If
                    myReader1.Close()

                End If

                CalcolaAllarme()

            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdImpianto <> -1 Then
                PAR.myTrans.Rollback()
            End If
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnEliminaE_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaE.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppare.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else
                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO


                        lstEstintori.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Estintori In lstEstintori
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridE.DataSource = lstEstintori
                        DataGridE.DataBind()

                    Else
                        '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO
                        If PAR.OracleConn.State = Data.ConnectionState.Open Then
                            Response.Write("IMPOSSIBILE VISUALIZZARE")
                            Exit Sub
                        Else
                            '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                            PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            ‘‘par.cmd.Transaction = par.myTrans


                            PAR.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_VERIFICHE where TIPO='ES' and ID_COMPONENTE = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_ESTINTORI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""


                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, " ESTINTORI e relativa verifica periodica")

                            BindGrid_Estintori()

                        End If
                    End If
                    txtSelE.Text = ""
                    txtIdComponente.Text = ""

                End If
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub



    Private Sub FrmSolaLettura()
        Try

            Me.btnAggE.Visible = False
            Me.btnEliminaE.Visible = False
            Me.btnApriE.Visible = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
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



    Protected Sub txtData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData.TextChanged
        CalcolaAllarme()
    End Sub

    Protected Sub txtValidita_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValidita.TextChanged
        CalcolaAllarme()
    End Sub

    Protected Sub cmbPreAllarme_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPreAllarme.SelectedIndexChanged
        CalcolaAllarme()
    End Sub

    Sub CalcolaAllarme()
        Dim GiorniTrascorsi As Integer
        Dim GiorniPreAllarme As Integer
        Dim MesiValidita As Integer
        Dim Data1 As String
        Dim Data2 As String

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Me.ImageAllarm.Visible = False
        If Strings.Trim(txtData.Text) = "" Or Strings.Len(Strings.Trim(txtData.Text)) < 9 Then Exit Sub

        Me.ImageAllarm.Visible = True

        If PAR.IfEmpty(Me.txtValidita.Text, "Null") = "Null" Then
            txtValidita.Text = "12"
        End If


        MesiValidita = Int(PAR.IfEmpty(txtValidita.Text, 12))

        Data1 = txtData.Text
        If Strings.Len(Data1) < 10 Then
            txtData.Text = Format(Now(), "dd/MM/yyyy")
            Data1 = txtData.Text
        End If

        Data2 = DateAdd(DateInterval.Month, Int(txtValidita.Text), CDate(PAR.FormattaData(Data1)))

        GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(PAR.FormattaData(Data2)), CDate(Now.ToString("dd/MM/yyyy")))
        GiorniPreAllarme = PAR.IfEmpty(cmbPreAllarme.SelectedItem.Value, 1) * 30

        If GiorniTrascorsi > 0 Then
            ' ROSSO
            Me.ImageAllarm.Visible = True
            Me.ImageAllarm.ToolTip = "SCADUTA"
            Me.ImageAllarm.ImageUrl = "../IMPIANTI/Immagini/Semaforo_Rosso.png"
        ElseIf GiorniTrascorsi >= -GiorniPreAllarme And GiorniTrascorsi <= 0 Then
            ' pre allarme GIALLO  mesi prima la scadenza
            Me.ImageAllarm.Visible = True
            Me.ImageAllarm.ToolTip = "IN SCADENZA"
            Me.ImageAllarm.ImageUrl = "../IMPIANTI/Immagini/Semaforo_Giallo.png"
        Else
            'NIENTE
            Me.ImageAllarm.Visible = False
        End If

        Me.txtDataScadenza.Text = Data2

    End Sub

    Sub GestioneVerifiche()

        If bVerifica = False Then Exit Sub

        If Me.txtIDV.Text = -1 Then


            ' SETTO TUTTE LE VERIFICHE PRECEDENTI COME STORICO
            'PAR.cmd.CommandText = "update SISCOM_MI.IMPIANTI_VERIFICHE set FL_STORICO='S' " _
            '                   & " where TIPO='ID' and ID_IMPIANTO=" & vIdImpianto

            'PAR.cmd.ExecuteNonQuery()
            'PAR.cmd.Parameters.Clear()
            'PAR.cmd.CommandText = ""


            'INSERISCO LA VERIFICA NUOVA
            PAR.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO,ID_COMPONENTE) " _
                            & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico,:id_componente )"

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ES"))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", PAR.AggiustaData(Me.txtData.Text)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNote.Text, 4000)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(Me.cmbEsito.SelectedValue)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", ""))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", Me.cmbEsito.SelectedItem.ToString))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(txtValidita.Text)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(cmbPreAllarme.SelectedValue)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", PAR.AggiustaData(Me.txtDataScadenza.Text)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_componente", Me.txtIDE.Text))

            PAR.cmd.ExecuteNonQuery()
            PAR.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifica periodica ESTINTORI")

        Else


            PAR.cmd.CommandText = "update SISCOM_MI.IMPIANTI_VERIFICHE " _
                                            & " set ID_IMPIANTO=:id_impianto, TIPO=:tipo, DITTA=:ditta,DATA=:data,NOTE=:note," _
                                            & "     ESITO=:esito,ES_PRESCRIZIONE=:prescrizione,ESITO_DETTAGLIO=:esitodettaglio," _
                                            & "     MESI_VALIDITA=:validita,MESI_PREALLARME=:preallarme,DATA_SCADENZA=:data_scadenza,FL_STORICO=:fl_storico " _
                                   & " where ID=" & Me.txtIDV.Text

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ES"))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", PAR.AggiustaData(Me.txtData.Text)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNote.Text, 4000)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(Me.cmbEsito.SelectedValue)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", ""))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", Me.cmbEsito.SelectedItem.ToString))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(txtValidita.Text)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(cmbPreAllarme.SelectedValue)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", PAR.AggiustaData(Me.txtDataScadenza.Text)))
            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))

            PAR.cmd.ExecuteNonQuery()
            PAR.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Modifica Verifica periodica ESTINTORI")
        End If


    End Sub

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


End Class
