Imports System.Collections

Partial Class Tab_TV_Dettagli
    Inherits UserControlSetIdMode

    Dim PAR As New CM.Global

    Dim lstTV As System.Collections.Generic.List(Of Epifani.TV)
    Dim lstEdificiTVSel As System.Collections.Generic.List(Of Epifani.Scale)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstTV = CType(HttpContext.Current.Session.Item("LSTTV"), System.Collections.Generic.List(Of Epifani.TV))
        lstEdificiTVSel = CType(HttpContext.Current.Session.Item("LSTTV_EDIFICISEL"), System.Collections.Generic.List(Of Epifani.Scale))

        Try
            If Not IsPostBack Then

                

                lstTV.Clear()
                lstEdificiTVSel.Clear()

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

                BindGrid_TV()
                CaricaDistribuzione()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

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


    'TV I_TV_DETTAGLI  DataGridTV
    Private Sub BindGrid_TV()
        Dim StringaSql As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.I_TV_DETTAGLI.ID,SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_EDIFICIO,SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_SCALA," _
                            & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as ""EDIFICIO""," _
                            & " SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE as ""SCALA"",SISCOM_MI.I_TV_DETTAGLI.DITTA_INSTALLAZIONE," _
                            & " TO_CHAR(TO_DATE(SISCOM_MI.I_TV_DETTAGLI.DATA_INSTALLAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_INSTALLAZIONE""," _
                            & "SISCOM_MI.I_TV_DETTAGLI.CENTRALINO_TV,SISCOM_MI.I_TV_DETTAGLI.IMPIANTO,SISCOM_MI.I_TV_DETTAGLI.TIPO_IMPIANTO," _
                            & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE"",SISCOM_MI.I_TV_DETTAGLI.ID_TIPO_DISTRIBUZIONE," _
                            & " (select count(*) from SISCOM_MI.I_TV_DETTAGLI_EDIFICI where  SISCOM_MI.I_TV_DETTAGLI_EDIFICI.ID_TV_DETTAGLI=SISCOM_MI.I_TV_DETTAGLI.ID) AS ""FABB_SERVITI"", " _
                            & "SISCOM_MI.I_TV_DETTAGLI.NOTE " _
              & " from  SISCOM_MI.I_TV_DETTAGLI,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE,SISCOM_MI.EDIFICI,SISCOM_MI.SCALE_EDIFICI " _
              & " where SISCOM_MI.I_TV_DETTAGLI.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_TV_DETTAGLI.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
              & " and   SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_EDIFICIO= SISCOM_MI.EDIFICI.ID (+)  " _
              & " and   SISCOM_MI.I_TV_DETTAGLI.ID_UBICAZIONE_SCALA=SISCOM_MI.SCALE_EDIFICI.ID (+)  " _
              & " order by SISCOM_MI.I_TV_DETTAGLI.ID "


        PAR.cmd.CommandText = StringaSql

        'CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = True
        myReader1 = PAR.cmd.ExecuteReader()
        While myReader1.Read
            If PAR.IfNull(myReader1("FABB_SERVITI"), 0) > 0 Then
                CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
            End If
        End While
        myReader1.Close()


        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_TV_DETTAGLI")

        DataGridTV.DataSource = ds
        DataGridTV.DataBind()

        ds.Dispose()
    End Sub

    Function ControlloCampiTV() As Boolean

        ControlloCampiTV = True


        If PAR.IfEmpty(Me.cmbEdificioTV.Text, "Null") = "Null" Or Me.cmbEdificioTV.Text = "-1" Then
            Response.Write("<script>alert('Selezionare l\'ubicazione (Edificio) del centralino!');</script>")
            ControlloCampiTV = False
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbScalaTV.Text, "Null") = "Null" Or Me.cmbScalaTV.Text = "-1" Then
            Response.Write("<script>alert('Selezionare l\'ubicazione (Scala) del centralino!');</script>")
            ControlloCampiTV = False
            Exit Function
        End If

        If Me.txtDataTV.Text = "dd/mm/YYYY" Then
            Me.txtDataTV.Text = ""
        End If

        'If PAR.IfEmpty(Me.txtDataTV.Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Inserire la data di installazione!');</script>")
        '    ControlloCampiTV = False
        '    Me.txtDataTV.Focus()
        '    Exit Function
        'End If

    End Function



    Protected Sub btn_InserisciTV_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciTV.Click
        If ControlloCampiTV() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDTV.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaTV()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateTV()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelTV.Text = ""
        txtIdComponente.Text = ""

    End Sub



    Protected Sub btn_ChiudiTV_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiTV.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelTV.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaTV()
        Dim i As Integer
        Dim RigaTV As Integer
        Dim vIdTV As Integer
        Dim ContaFabbServiti As Integer

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.TV
                RigaTV = lstTV.Count
                ContaFabbServiti = 0

                '***********FABBRICATI
                For i = 0 To CheckBoxFabb.Items.Count - 1
                    If CheckBoxFabb.Items(i).Selected = True And Str(CheckBoxFabb.Items(i).Value) > -1 Then

                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstEdificiTVSel.Count, Str(RigaTV), Str(CheckBoxFabb.Items(i).Value))
                        lstEdificiTVSel.Add(genS)
                        genS = Nothing
                        ContaFabbServiti = ContaFabbServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next

                gen = New Epifani.TV(lstTV.Count, Convert.ToInt32(Me.cmbEdificioTV.SelectedValue), RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaTV.SelectedValue, -1))), Me.cmbEdificioTV.SelectedItem.Text, Me.cmbScalaTV.SelectedItem.Text, PAR.PulisciStrSql(Me.txtDitta.Text), Me.txtDataTV.Text, Me.cmbTipoCentralinoTV.SelectedItem.Text, Me.cmbImpianto.SelectedValue.ToString, Me.cmbTipoImpiantoTV.SelectedItem.Text, Me.cmbDistribuzioneTV.SelectedItem.Text, Me.cmbDistribuzioneTV.SelectedValue, ContaFabbServiti, PAR.PulisciStrSql(Me.txtNoteTV.Text))

                DataGridTV.DataSource = Nothing
                lstTV.Add(gen)
                gen = Nothing

                DataGridTV.DataSource = lstTV
                DataGridTV.DataBind()



            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                PAR.cmd.CommandText = "insert into SISCOM_MI.I_TV_DETTAGLI " _
                                            & " (ID, ID_IMPIANTO,ID_UBICAZIONE_EDIFICIO,ID_UBICAZIONE_SCALA,DITTA_INSTALLAZIONE," _
                                            & "DATA_INSTALLAZIONE,CENTRALINO_TV,IMPIANTO,TIPO_IMPIANTO,ID_TIPO_DISTRIBUZIONE,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_I_TV_DETTAGLI.NEXTVAL,:id_impianto,:id_edificio,:id_scala,:ditta," _
                                            & ":data,:centralino,:impianto,:tipo,:id_distribuzione,:note) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbEdificioTV.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaTV.SelectedValue, -1)))))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(Me.txtDitta.Text, 300)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", PAR.AggiustaData(Me.txtDataTV.Text)))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("centralino", Me.cmbTipoCentralinoTV.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("impianto", Me.cmbImpianto.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", Me.cmbTipoImpiantoTV.SelectedItem.Text))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneTV.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNoteTV.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "TV Centralizzata")


                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_TV_DETTAGLI.CURRVAL FROM dual "
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdTV = myReader1(0)
                End If

                myReader1.Close()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxFabb.Items.Count - 1
                    If CheckBoxFabb.Items(i).Selected = True And Str(CheckBoxFabb.Items(i).Value) > -1 Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_TV_DETTAGLI_EDIFICI (ID_TV_DETTAGLI,ID_EDIFICIO) values " _
                                   & "(" & vIdTV & "," & CheckBoxFabb.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Fabbricati Serviti dalla TV Centralizzata")

                    End If
                Next

                BindGrid_TV()

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '' COMMIT
            'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            If vIdImpianto <> -1 Then
                PAR.myTrans.Rollback()
                PAR.OracleConn.Close()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdateTV()
        Dim i As Integer
        Dim ContaFabbServiti As Integer

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                ContaFabbServiti = 0

                '***********SCALE
SCALE:
                For i = 0 To lstEdificiTVSel.Count - 1
                    If lstEdificiTVSel(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstEdificiTVSel.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next

                For i = 0 To CheckBoxFabb.Items.Count - 1
                    If CheckBoxFabb.Items(i).Selected = True And Str(CheckBoxFabb.Items(i).Value) > -1 Then
                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstEdificiTVSel.Count, Str(txtIdComponente.Text), Str(CheckBoxFabb.Items(i).Value))
                        lstEdificiTVSel.Add(genS)
                        genS = Nothing
                        ContaFabbServiti = ContaFabbServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next
                '*************************

                lstTV(txtIdComponente.Text).ID_UBICAZIONE_EDIFICIO = Convert.ToInt32(Me.cmbEdificioTV.SelectedValue)
                lstTV(txtIdComponente.Text).ID_UBICAZIONE_SCALA = RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaTV.SelectedValue, -1)))
                lstTV(txtIdComponente.Text).EDIFICIO = Me.cmbEdificioTV.SelectedItem.Text
                lstTV(txtIdComponente.Text).SCALA = Me.cmbScalaTV.SelectedItem.Text

                lstTV(txtIdComponente.Text).DITTA_INSTALLAZIONE = PAR.PulisciStrSql(Me.txtDitta.Text)
                lstTV(txtIdComponente.Text).DATA_INSTALLAZIONE = Me.txtDataTV.Text

                lstTV(txtIdComponente.Text).CENTRALINO_TV = Me.cmbTipoCentralinoTV.SelectedItem.Text
                lstTV(txtIdComponente.Text).IMPIANTO = Me.cmbImpianto.SelectedItem.ToString
                lstTV(txtIdComponente.Text).TIPO_IMPIANTO = Me.cmbTipoImpiantoTV.SelectedItem.ToString

                lstTV(txtIdComponente.Text).DISTRIBUZIONE = Me.cmbDistribuzioneTV.SelectedItem.ToString
                lstTV(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE = Me.cmbDistribuzioneTV.SelectedValue

                lstTV(txtIdComponente.Text).FABB_SERVITI = ContaFabbServiti
                lstTV(txtIdComponente.Text).NOTE = PAR.PulisciStrSql(Me.txtNoteTV.Text)

                DataGridTV.DataSource = lstTV
                DataGridTV.DataBind()



            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_TV_DETTAGLI set " _
                                            & " ID_UBICAZIONE_EDIFICIO=:id_edificio,ID_UBICAZIONE_SCALA=:id_scala,DITTA_INSTALLAZIONE=:ditta," _
                                            & "DATA_INSTALLAZIONE=:data,CENTRALINO_TV=:centralino,IMPIANTO=:impianto," _
                                            & "TIPO_IMPIANTO=:tipo,ID_TIPO_DISTRIBUZIONE=:id_distribuzione,NOTE=:note " _
                                    & " where ID=" & Me.txtIDTV.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbEdificioTV.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(PAR.IfEmpty(Me.cmbScalaTV.SelectedValue, -1)))))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", Strings.Left(Me.txtDitta.Text, 300)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", PAR.AggiustaData(Me.txtDataTV.Text)))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("centralino", Me.cmbTipoCentralinoTV.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("impianto", Me.cmbImpianto.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", Me.cmbTipoImpiantoTV.SelectedItem.Text))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneTV.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNoteTV.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "TV Centralizzata")


                PAR.cmd.CommandText = "delete from SISCOM_MI.I_TV_DETTAGLI_EDIFICI where ID_TV_DETTAGLI = " & Me.txtIDTV.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxFabb.Items.Count - 1
                    If CheckBoxFabb.Items(i).Selected = True And Str(CheckBoxFabb.Items(i).Value) > -1 Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_TV_DETTAGLI_EDIFICI (ID_TV_DETTAGLI,ID_EDIFICIO) values " _
                                   & "(" & Me.txtIDTV.Text & "," & CheckBoxFabb.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Fabbricati Serviti dalla TV Centralizzata")

                    End If
                Next

                BindGrid_TV()

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


    Protected Sub btnApriTV_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriTV.Click
        Dim i, j As Integer

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDTV.Text = lstTV(txtIdComponente.Text).ID

                    Me.cmbEdificioTV.SelectedValue = PAR.IfNull(lstTV(txtIdComponente.Text).ID_UBICAZIONE_EDIFICIO, "")
                    FiltraScale()
                    Me.cmbScalaTV.SelectedValue = PAR.IfNull(lstTV(txtIdComponente.Text).ID_UBICAZIONE_SCALA, "-1")

                    Me.txtDitta.Text = PAR.IfNull(lstTV(txtIdComponente.Text).DITTA_INSTALLAZIONE, "")
                    Me.txtDataTV.Text = PAR.FormattaData(lstTV(txtIdComponente.Text).DATA_INSTALLAZIONE)

                    Me.cmbTipoCentralinoTV.SelectedValue = PAR.IfNull(lstTV(txtIdComponente.Text).CENTRALINO_TV, "")
                    Me.cmbImpianto.Items.FindByValue(PAR.IfNull(lstTV(txtIdComponente.Text).IMPIANTO, "")).Selected = True

                    Me.cmbTipoImpiantoTV.SelectedValue = PAR.IfNull(lstTV(txtIdComponente.Text).TIPO_IMPIANTO, "")

                    Me.cmbDistribuzioneTV.SelectedValue = PAR.IfNull(lstTV(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE, -1)

                    Me.txtNoteTV.Text = PAR.IfNull(lstTV(txtIdComponente.Text).NOTE, "")

                    For i = 0 To CheckBoxFabb.Items.Count - 1
                        CheckBoxFabb.Items(i).Selected = False
                    Next

                    For i = 0 To CheckBoxFabb.Items.Count - 1
                        For j = 0 To lstEdificiTVSel.Count - 1
                            If Val(lstEdificiTVSel(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxFabb.Items(i).Value = Val(lstEdificiTVSel(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxFabb.Items(i).Selected = True
                                End If
                            End If
                        Next
                    Next

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

                    '*** I_TV_DETTAGLI
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_TV_DETTAGLI where ID=" & txtIdComponente.Text
                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDTV.Text = myReader1("ID")

                        Me.cmbEdificioTV.SelectedValue = PAR.IfNull(myReader1("ID_UBICAZIONE_EDIFICIO"), "")
                        FiltraScale()
                        Me.cmbScalaTV.SelectedValue = PAR.IfNull(myReader1("ID_UBICAZIONE_SCALA"), "-1")

                        Me.txtDitta.Text = PAR.IfNull(myReader1("DITTA_INSTALLAZIONE"), "")
                        Me.txtDataTV.Text = PAR.FormattaData(PAR.IfNull(myReader1("DATA_INSTALLAZIONE"), ""))

                        Me.cmbTipoCentralinoTV.SelectedValue = PAR.IfNull(myReader1("CENTRALINO_TV"), "")
                        Me.cmbImpianto.SelectedValue = PAR.IfNull(myReader1("IMPIANTO"), "")

                        Me.cmbTipoImpiantoTV.SelectedValue = PAR.IfNull(myReader1("TIPO_IMPIANTO"), "")

                        Me.cmbDistribuzioneTV.SelectedValue = PAR.IfNull(myReader1("ID_TIPO_DISTRIBUZIONE"), -1)

                        Me.txtNoteTV.Text = PAR.IfNull(myReader1("NOTE"), "")

                    End If
                    myReader1.Close()
                    '*** 

                    '*** SCALE I_TV_DETTAGLI_EDIFICI
                    '***Azzero la lista delle scale
                    For i = 0 To CheckBoxFabb.Items.Count - 1
                        CheckBoxFabb.Items(i).Selected = False
                    Next

                    '*** check della scala salvata
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select ID_EDIFICIO from SISCOM_MI.I_TV_DETTAGLI_EDIFICI where  ID_TV_DETTAGLI = " & Me.txtIDTV.Text

                    myReader2 = PAR.cmd.ExecuteReader()

                    While myReader2.Read
                        For i = 0 To CheckBoxFabb.Items.Count - 1
                            If CheckBoxFabb.Items(i).Value = PAR.IfNull(myReader2("ID_EDIFICIO"), "-1") Then
                                CheckBoxFabb.Items(i).Selected = True
                            End If
                        Next
                    End While
                    myReader2.Close()
                    '**************************


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





    Protected Sub btnEliminaTV_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaTV.Click
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

                        lstTV.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.TV In lstTV
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridTV.DataSource = lstTV
                        DataGridTV.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_TV_DETTAGLI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_TV()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "TV Centralizzata")

                        End If
                    End If

                    txtSelTV.Text = ""
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





    Protected Sub btnAggTV_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggTV.Click
        Dim i As Integer

        Try
            Me.txtIDTV.Text = -1

            Me.cmbEdificioTV.SelectedIndex = -1

            Me.cmbScalaTV.Items.Clear()
            cmbScalaTV.Items.Add(New ListItem(" ", -1))
            Me.cmbScalaTV.SelectedIndex = -1

            Me.txtDitta.Text = ""
            Me.txtDataTV.Text = ""

            Me.cmbTipoCentralinoTV.Text = ""
            Me.cmbImpianto.Text = ""
            Me.cmbTipoImpiantoTV.Text = ""

            Me.cmbDistribuzioneTV.SelectedIndex = -1
            Me.txtNoteTV.Text = ""

            For i = 0 To CheckBoxFabb.Items.Count - 1
                CheckBoxFabb.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try


    End Sub


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggTV.Visible = False
            Me.btnEliminaTV.Visible = False
            Me.btnApriTV.Visible = False

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


    Private Sub CaricaDistribuzione()

        Try
            PAR.cmd.CommandText = "select distinct ID,DESCRIZIONE from SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE order by DESCRIZIONE asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            cmbDistribuzioneTV.Items.Add(New ListItem("", -1))
            While myReader1.Read
                cmbDistribuzioneTV.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            cmbDistribuzioneTV.SelectedValue = "-1"


        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function



    Protected Sub cmbEdificioTV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificioTV.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If Me.cmbEdificioTV.SelectedValue <> "-1" Then
            FiltraScale()
        Else
            Me.cmbScalaTV.Items.Clear()
            cmbScalaTV.Items.Add(New ListItem(" ", -1))
        End If
    End Sub

    Private Sub FiltraScale()
        Dim ds As New Data.DataSet()
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If Me.cmbEdificioTV.SelectedValue <> "-1" Then

                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Me.cmbScalaTV.Items.Clear()
                cmbScalaTV.Items.Add(New ListItem(" ", -1))

                PAR.cmd.CommandText = "select  ID, DESCRIZIONE from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO =" & Me.cmbEdificioTV.SelectedValue.ToString & " order by DESCRIZIONE asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                While myReader1.Read
                    cmbScalaTV.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
                End While
                myReader1.Close()

                If FlagConnessione = True Then
                    PAR.OracleConn.Close()
                End If
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub DataGridTV_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridTV.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_TV_Dettagli_txtSelTV').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " - SCALA: " & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('Tab_TV_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_TV_Dettagli_txtSelTV').value='Hai selezionato: " & Replace(e.Item.Cells(3).Text, "'", "\'") & " - SCALA: " & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('Tab_TV_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


End Class
