Imports System.Collections

Partial Class Tab_Termico_Pompe
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstPompe As System.Collections.Generic.List(Of Epifani.Pompe)
    Dim lstPompeS As System.Collections.Generic.List(Of Epifani.PompeS)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstPompe = CType(HttpContext.Current.Session.Item("LSTPOMPE"), System.Collections.Generic.List(Of Epifani.Pompe))
        lstPompeS = CType(HttpContext.Current.Session.Item("LSTPOMPES"), System.Collections.Generic.List(Of Epifani.PompeS))

        Try
            If Not IsPostBack Then


                If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                    lstPompe = New System.Collections.Generic.List(Of Epifani.Pompe)
                    lstPompeS = New System.Collections.Generic.List(Of Epifani.PompeS)


                End If


                lstPompe.Clear()
                lstPompeS.Clear()

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

                BindGrid_Pompe()
                BindGrid_PompeS()

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

    'POMPE GRID3
    Private Sub BindGrid_Pompe()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO," _
                    & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MATRICOLA,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.NOTE," _
                       & "SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.POTENZA " _
              & " from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI " _
              & " where SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI.MODELLO "


       
        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "POMPE_CIRCOLAZIONE_TERMICI")


        DataGrid3.DataSource = ds
        DataGrid3.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Pompe_txtSelPompe').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Termico_Pompe_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Pompe_txtSelPompe').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Termico_Pompe_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiPompe() As Boolean

        ControlloCampiPompe = True


        If PAR.IfEmpty(Me.txtModelloP.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiPompe = False
            txtModelloP.Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazioneP.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazioneP.Text = ""
        End If

    End Function

    Protected Sub btn_InserisciPompe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciPompe.Click
        If ControlloCampiPompe() = False Then
            txtAppareP.Text = "1"
            Exit Sub
        End If

        If Me.txtIDP.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaPompe()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdatePompe()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelPompe.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiPompe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiPompe.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelPompe.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaPompe()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Pompe

                gen = New Epifani.Pompe(lstPompe.Count, PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200), Me.txtMatricolaP.Text, Me.txtNoteP.Text, Me.txtAnnoRealizzazioneP.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0)))

                DataGrid3.DataSource = Nothing
                lstPompe.Add(gen)
                gen = Nothing

                DataGrid3.DataSource = lstPompe
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_POMPE_CIRCOLAZIONE_TERMICI.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricolaP.Text) & "','" & Me.txtAnnoRealizzazioneP.Text & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaP.Text, "Null")) & ",'" & PAR.PulisciStrSql(Me.txtNoteP.Text) & "')"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Pompe()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Circolazione")

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

    Private Sub UpdatePompe()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstPompe(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)
                lstPompe(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaP.Text
                lstPompe(txtIdComponente.Text).NOTE = Me.txtNoteP.Text

                lstPompe(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneP.Text
                lstPompe(txtIdComponente.Text).POTENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0))

                DataGrid3.DataSource = lstPompe
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "update SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaP.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneP.Text & "'," _
                                            & "POTENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaP.Text, "Null")) & "," _
                                            & "NOTE='" & PAR.PulisciStrSql(Me.txtNoteP.Text) & "' " _
                                            & " where ID=" & Me.txtIDP.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Pompe()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Pompe di Circolazione")

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

    Protected Sub btnApriPompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriPompa.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareP.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDP.Text = lstPompe(txtIdComponente.Text).ID
                    Me.txtModelloP.Text = PAR.IfNull(lstPompe(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaP.Text = PAR.IfNull(lstPompe(txtIdComponente.Text).MATRICOLA, "")
                    Me.txtNoteP.Text = PAR.IfNull(lstPompe(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazioneP.Text = PAR.IfNull(lstPompe(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtPotenzaP.Text = PAR.IfNull(lstPompe(txtIdComponente.Text).POTENZA, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDP.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloP.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaP.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
                        Me.txtNoteP.Text = PAR.IfNull(myReader1("NOTE"), "")

                        Me.txtAnnoRealizzazioneP.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtPotenzaP.Text = PAR.IfNull(myReader1("POTENZA"), "")

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

    Protected Sub btnEliminaPompa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaPompa.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareP.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else
                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstPompe.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Pompe In lstPompe
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid3.DataSource = lstPompe
                        DataGrid3.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.POMPE_CIRCOLAZIONE_TERMICI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Pompe()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Pompe di Circolazione")

                        End If
                    End If
                    txtSelPompe.Text = ""
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

    Protected Sub btnAggPompe_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggPompe.Click
        Try

            Me.txtIDP.Text = -1

            Me.txtModelloP.Text = ""
            Me.txtMatricolaP.Text = ""
            Me.txtNoteP.Text = ""
            Me.txtAnnoRealizzazioneP.Text = ""
            Me.txtPotenzaP.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub


    'POMPE GRID4 (pompe di sollevamento
    Private Sub BindGrid_PompeS()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ID,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MODELLO," _
                          & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MATRICOLA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ANNO_COSTRUZIONE," _
                          & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.POTENZA,SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.PORTATA," _
                          & "SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.PREVALENZA" _
              & " from SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO " _
              & " where SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO.MODELLO "


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        'Dim dt As New Data.DataTable

        'da.Fill(dt)
        da.Fill(ds, "I_TER_POMPE_SOLLEVAMENTO")


        DataGrid4.DataSource = ds
        DataGrid4.DataBind()

        ds.Dispose()
    End Sub





    Function ControlloCampiPompeS() As Boolean

        ControlloCampiPompeS = True


        If PAR.IfEmpty(Me.txtModelloS.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiPompeS = False
            txtModelloS.Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazioneS.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazioneS.Text = ""
        End If

    End Function

    Protected Sub btn_InserisciPompeS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciPompeS.Click
        If ControlloCampiPompeS() = False Then
            txtAppareP.Text = "1"
            Exit Sub
        End If

        If Me.txtIDS.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaPompeS()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdatePompeS()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelPompeS.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiPompeS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiPompeS.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelPompeS.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaPompeS()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.PompeS

                gen = New Epifani.PompeS(lstPompeS.Count, PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200), Me.txtMatricolaS.Text, Me.txtAnnoRealizzazioneS.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaS.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataS.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaS.Text, 0)), "")

                DataGrid4.DataSource = Nothing
                lstPompeS.Add(gen)
                gen = Nothing

                DataGrid4.DataSource = lstPompeS
                DataGrid4.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,PORTATA,PREVALENZA) " _
                                    & "values (SISCOM_MI.SEQ_I_TER_POMPE_SOLLEVAMENTO.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricolaS.Text) & "','" & Me.txtAnnoRealizzazioneS.Text & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaS.Text, "Null")) & "," & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPortataS.Text, "Null")) & "," & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPrevalenzaS.Text, "Null")) & ")"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_PompeS()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento Acque Meteoriche")

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

    Private Sub UpdatePompeS()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstPompeS(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)
                lstPompeS(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaS.Text

                lstPompeS(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneS.Text
                lstPompeS(txtIdComponente.Text).POTENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaS.Text, 0))
                lstPompeS(txtIdComponente.Text).PORTATA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataS.Text, 0))
                lstPompeS(txtIdComponente.Text).PREVALENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaS.Text, 0))

                DataGrid4.DataSource = lstPompeS
                DataGrid4.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "update SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloS.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaS.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneS.Text & "'," _
                                            & "POTENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaS.Text, "Null")) & "," _
                                            & "PORTATA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPortataS.Text, "Null")) & "," _
                                            & "PREVALENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPrevalenzaS.Text, "Null")) _
                                            & " where ID=" & Me.txtIDS.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_PompeS()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento Acque Meteoriche")

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

    Protected Sub btnApriPompaS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriPompaS.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareS.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDS.Text = lstPompeS(txtIdComponente.Text).ID
                    Me.txtModelloS.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaS.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).MATRICOLA, "")

                    Me.txtAnnoRealizzazioneS.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtPotenzaS.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).POTENZA, "")
                    Me.txtPortataS.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).PORTATA, "")
                    Me.txtPrevalenzaS.Text = PAR.IfNull(lstPompeS(txtIdComponente.Text).PREVALENZA, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDS.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloS.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaS.Text = PAR.IfNull(myReader1("MATRICOLA"), "")

                        Me.txtAnnoRealizzazioneS.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtPotenzaS.Text = PAR.IfNull(myReader1("POTENZA"), "")
                        Me.txtPortataS.Text = PAR.IfNull(myReader1("PORTATA"), "")
                        Me.txtPrevalenzaS.Text = PAR.IfNull(myReader1("PREVALENZA"), "")

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

    Protected Sub btnEliminaPompaS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaPompaS.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareS.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else
                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstPompeS.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.PompeS In lstPompeS
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid4.DataSource = lstPompeS
                        DataGrid4.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_TER_POMPE_SOLLEVAMENTO where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_PompeS()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento Acque Meteoriche")

                        End If
                    End If
                    txtSelPompeS.Text = ""
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

    Protected Sub btnAggPompeS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggPompeS.Click
        Try

            Me.txtIDS.Text = -1

            Me.txtModelloS.Text = ""
            Me.txtMatricolaS.Text = ""
            Me.txtAnnoRealizzazioneS.Text = ""
            Me.txtPotenzaS.Text = ""
            Me.txtPortataS.Text = ""
            Me.txtPrevalenzaS.Text = ""


        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggPompe.Visible = False
            Me.btnEliminaPompa.Visible = False
            Me.btnApriPompa.Visible = False

            Me.btnAggPompeS.Visible = False
            Me.btnEliminaPompaS.Visible = False
            Me.btnApriPompaS.Visible = False


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

    Protected Sub DataGrid4_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid4.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Pompe_txtSelPompeS').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Termico_Pompe_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Termico_Pompe_txtSelPompeS').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Termico_Pompe_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub



End Class
