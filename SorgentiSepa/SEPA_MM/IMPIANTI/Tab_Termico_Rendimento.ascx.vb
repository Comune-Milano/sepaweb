
Partial Class Tab_Termico_Rendimento
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstControlloRendimento As System.Collections.Generic.List(Of Epifani.ControlloRendimento)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstControlloRendimento = CType(HttpContext.Current.Session.Item("LSTCONTROLLORENDIMENTO"), System.Collections.Generic.List(Of Epifani.ControlloRendimento))

        Try
            If Not IsPostBack Then

                If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                    lstControlloRendimento = New System.Collections.Generic.List(Of Epifani.ControlloRendimento)
                End If

                lstControlloRendimento.Clear()


                vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

                ' CONNESSIONE DB
                IdConnessione = CType(Me.Page.FindControl("txtConnessione"), TextBox).Text

                If PAR.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘‘par.cmd.Transaction = par.myTrans
                End If
                ''''''''''''''''''''''''''

                BindGrid_Controlli()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception

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

    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property


    'CONTROLLI GRID1
    Private Sub BindGrid_Controlli()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select ID,to_char(to_date(DATA_ESAME,'yyyymmdd'),'dd/mm/yyyy') as ""DATA_ESAME""," _
                    & "ESECUTORE,TEMP_FUMI," _
                       & "TEMP_AMB,O2," _
                       & "CO2,BACHARACH," _
                       & "CO,RENDIMENTO," _
                       & "TIRAGGIO " _
              & " from SISCOM_MI.RENDIMENTO_TERMICI " _
              & " where SISCOM_MI.RENDIMENTO_TERMICI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.RENDIMENTO_TERMICI.DATA_ESAME "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "RENDIMENTO_TERMICI")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        ds.Dispose()

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Rendimento_txtSelControlli').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Termico_Rendimento_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Rendimento_txtSelControlli').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('Tab_Termico_Rendimento_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampi() As Boolean

        ControlloCampi = True

        If PAR.IfEmpty(Me.txtData.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la data di controllo');</script>")
            ControlloCampi = False
            txtData.Focus()
            Exit Function
        End If


        If Me.txtData.Text = "dd/mm/YYYY" Then
            Me.txtData.Text = ""
        End If

    End Function


    Protected Sub btn_InserisciControllo_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciControllo.Click

        If ControlloCampi() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtID.Text = -1 Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.Salva()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.Update()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelControlli.Text = ""
        txtIdComponente.Text = ""


    End Sub

    Protected Sub btn_ChiudiControllo_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiControllo.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelControlli.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Private Sub Salva()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.ControlloRendimento

                gen = New Epifani.ControlloRendimento(lstControlloRendimento.Count, Me.txtData.Text, Me.txtEsecutore.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtTempiFumi.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtTempiAmb.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtO2.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtCO2.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtBacharach.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtCO.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtRendimento.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtTiraggio.Text, 0)))

                DataGrid1.DataSource = Nothing
                lstControlloRendimento.Add(gen)
                gen = Nothing

                DataGrid1.DataSource = lstControlloRendimento
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO


                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "insert into SISCOM_MI.RENDIMENTO_TERMICI (ID,ID_IMPIANTO, TEMP_FUMI, TEMP_AMB,O2,CO2," _
                                                                    & "BACHARACH,CO,RENDIMENTO,TIRAGGIO,DATA_ESAME,ESECUTORE) " _
                                & " values (SISCOM_MI.SEQ_RENDIMENTO_TERMICI.NEXTVAL,:id_impianto,:tempi_fumi,:tempi_amb,:o2,:co2,:bacharach,:co," _
                                         & ":rendimento,:tiraggio,:data_esame,:esecutore)"

                'PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", "SISCOM_MI.SEQ_RENDIMENTO_I_TERMICI.NEXTVAL"))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tempi_fumi", strToNumber(Me.txtTempiFumi.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tempi_amb", strToNumber(Me.txtTempiAmb.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("o2", strToNumber(Me.txtO2.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("co2", strToNumber(Me.txtCO2.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("bacharach", strToNumber(Me.txtBacharach.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("co", strToNumber(Me.txtCO.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rendimento", strToNumber(Me.txtRendimento.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tiraggio", strToNumber(Me.txtTiraggio.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_esame", PAR.AggiustaData(Me.txtData.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esecutore", Strings.Left(Me.txtEsecutore.Text, 100)))


                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()




                '& "values (SISCOM_MI.SEQ_BRUCIATORI.NEXTVAL" & vIdImpianto & ",'" & CType(TabGenerale.FindControl("RBList_Tipologia"), RadioButtonList).SelectedValue.ToString & "'," _
                '                                     & RitornaNullSeMenoUno(Me.cmbTipoUso.SelectedValue.ToString) & "," & RitornaNullSeMenoUno((CType(TabGenerale.FindControl("cmbCombustibile"), DropDownList).SelectedValue.ToString)) & ",'" _
                '                                     & PAR.PulisciStrSql(CType(TabGenerale.FindControl("cmbTipoSerbatoio"), DropDownList).SelectedItem.Text) & "'," _
                '                                     & PAR.VirgoleInPunti(PAR.IfEmpty(CType(TabGenerale.FindControl("txtCapacita"), TextBox).Text, "Null")) & "," _
                '                                     & PAR.VirgoleInPunti(PAR.IfEmpty(CType(TabGenerale.FindControl("txtPotenza"), TextBox).Text, "Null")) & "," _
                '                                     & PAR.VirgoleInPunti(PAR.IfEmpty(CType(TabGenerale.FindControl("txtConsumo"), TextBox).Text, "Null")) & ",'" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbLibretto"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbConformita"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbDecreto"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbLicenzaUTF"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbISPESL"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbTrattamentoAcqua"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & CType(Tab_Termico_Certificazioni.FindControl("cmbContEnergia"), DropDownList).SelectedValue.ToString & "','" _
                '                                     & PAR.AggiustaData(CType(Tab_Termico_Certificazioni.FindControl("txtDataISPESL"), TextBox).Text) & "')"


                'PAR.cmd.CommandText = "insert into SISCOM_MI.BRUCIATORI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,CAMPO_FUNZIONAMENTO,NOTE) " _
                '                    & "values (SISCOM_MI.SEQ_BRUCIATORI.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(Me.txtModello.Text) & "',' " _
                '                        & PAR.PulisciStrSql(Me.txtMatricola.Text) & "','" & Me.txtAnnoRealizzazione.Text & "'," _
                '                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamento.Text, "Null")) & ",'" & PAR.PulisciStrSql(Me.txtNote.Text) & "')"

                'PAR.cmd.ExecuteNonQuery()

                BindGrid_Controlli()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Controlli del rendimento di combustione")

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub Update()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstControlloRendimento(txtIdComponente.Text).DATA_ESAME = Me.txtData.Text
                lstControlloRendimento(txtIdComponente.Text).ESECUTORE = Me.txtEsecutore.Text

                lstControlloRendimento(txtIdComponente.Text).TEMP_FUMI = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtTempiFumi.Text, 0))

                lstControlloRendimento(txtIdComponente.Text).TEMP_AMB = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtTempiAmb.Text, 0))
                lstControlloRendimento(txtIdComponente.Text).O2 = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtO2.Text, 0))
                lstControlloRendimento(txtIdComponente.Text).CO2 = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtCO2.Text, 0))
                lstControlloRendimento(txtIdComponente.Text).BACHARACH = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtBacharach.Text, 0))
                lstControlloRendimento(txtIdComponente.Text).CO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtCO.Text, 0))
                lstControlloRendimento(txtIdComponente.Text).RENDIMENTO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtRendimento.Text, 0))
                lstControlloRendimento(txtIdComponente.Text).TIRAGGIO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtTiraggio.Text, 0))

                DataGrid1.DataSource = lstControlloRendimento
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                'PAR.cmd.CommandText = "update SISCOM_MI.BRUCIATORI set " _
                '                            & "MODELLO='" & PAR.PulisciStrSql(Me.txtModello.Text) & "'," _
                '                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricola.Text) & "'," _
                '                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazione.Text & "'," _
                '                            & "CAMPO_FUNZIONAMENTO=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamento.Text, "Null")) & "," _
                '                            & "NOTE='" & PAR.PulisciStrSql(Me.txtNote.Text) & "' " _
                '                            & " where ID=" & Me.txtID.Text

                'PAR.cmd.ExecuteNonQuery()


                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = "update SISCOM_MI.RENDIMENTO_TERMICI set ID_IMPIANTO=:id_impianto, TEMP_FUMI=:tempi_fumi, TEMP_AMB=:tempi_amb,O2=:o2,CO2=:co2," _
                                                                    & "BACHARACH=:bacharach,CO=:co,RENDIMENTO=:rendimento,TIRAGGIO=:tiraggio,DATA_ESAME=:data_esame,ESECUTORE=:esecutore " _
                                   & " where ID=" & Me.txtID.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tempi_fumi", strToNumber(Me.txtTempiFumi.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tempi_amb", strToNumber(Me.txtTempiAmb.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("o2", strToNumber(Me.txtO2.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("co2", strToNumber(Me.txtCO2.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("bacharach", strToNumber(Me.txtBacharach.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("co", strToNumber(Me.txtCO.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rendimento", strToNumber(Me.txtRendimento.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tiraggio", strToNumber(Me.txtTiraggio.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_esame", PAR.AggiustaData(Me.txtData.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esecutore", Strings.Left(Me.txtEsecutore.Text, 100)))


                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Controlli()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Controlli del rendimento di combustione")

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnApriControllo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriControllo.Click

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtID.Text = lstControlloRendimento(txtIdComponente.Text).ID
                    Me.txtData.Text = PAR.FormattaData(PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).DATA_ESAME, ""))
                    Me.txtEsecutore.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).ESECUTORE, "")

                    Me.txtTempiFumi.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).TEMP_FUMI, "")
                    Me.txtTempiAmb.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).TEMP_AMB, "")
                    Me.txtO2.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).O2, "")
                    Me.txtCO2.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).CO2, "")
                    Me.txtBacharach.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).BACHARACH, "")
                    Me.txtCO.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).CO, "")
                    Me.txtRendimento.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).RENDIMENTO, "")
                    Me.txtTiraggio.Text = PAR.IfNull(lstControlloRendimento(txtIdComponente.Text).TIRAGGIO, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.RENDIMENTO_TERMICI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtID.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtData.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_ESAME"), ""))
                        Me.txtEsecutore.Text = PAR.IfNull(myReader1("ESECUTORE"), "")

                        Me.txtTempiFumi.Text = PAR.IfNull(myReader1("TEMP_FUMI"), "")
                        Me.txtTempiAmb.Text = PAR.IfNull(myReader1("TEMP_AMB"), "")
                        Me.txtO2.Text = PAR.IfNull(myReader1("O2"), "")
                        Me.txtCO2.Text = PAR.IfNull(myReader1("CO2"), "")
                        Me.txtBacharach.Text = PAR.IfNull(myReader1("BACHARACH"), "")
                        Me.txtCO.Text = PAR.IfNull(myReader1("CO"), "")
                        Me.txtRendimento.Text = PAR.IfNull(myReader1("RENDIMENTO"), "")
                        Me.txtTiraggio.Text = PAR.IfNull(myReader1("TIRAGGIO"), "")

                    End If
                    myReader1.Close()

                End If
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

    Protected Sub btnEliminaControllo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaControllo.Click
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

                        lstControlloRendimento.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.ControlloRendimento In lstControlloRendimento
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid1.DataSource = lstControlloRendimento
                        DataGrid1.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.RENDIMENTO_TERMICI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Controlli()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Controlli del rendimento di combustione")

                        End If
                    End If
                    txtSelControlli.Text = ""
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

    Protected Sub btnAggControllo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggControllo.Click
        Try


            Me.txtID.Text = -1

            Me.txtData.Text = ""
            Me.txtEsecutore.Text = ""
            Me.txtTempiFumi.Text = ""
            Me.txtTempiAmb.Text = ""
            Me.txtO2.Text = ""
            Me.txtCO2.Text = ""
            Me.txtBacharach.Text = ""
            Me.txtCO.Text = ""
            Me.txtRendimento.Text = ""
            Me.txtTiraggio.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

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

    Private Sub FrmSolaLettura()
        Try
            Me.btnAggControllo.Visible = False
            Me.btnEliminaControllo.Visible = False
            Me.btnApriControllo.Visible = False

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



End Class
