Imports System.Collections


Partial Class TabDettagliTELE
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstScambiatori As System.Collections.Generic.List(Of Epifani.Generatori)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstScambiatori = CType(HttpContext.Current.Session.Item("LSTSCAMBIATORI"), System.Collections.Generic.List(Of Epifani.Generatori))

        Try
            If Not IsPostBack Then

                'Dim lstGeneratori As New System.Collections.Generic.List(Of Generatori)

                If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                    lstScambiatori = New System.Collections.Generic.List(Of Epifani.Generatori)
                End If

                lstScambiatori.Clear()

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

                BindGrid_Scambiatori()

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


    'SCAMBIATORI (Uguale a GENERATORI) GRID1
    Private Sub BindGrid_Scambiatori()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.GENERATORI_TERMICI.ID,SISCOM_MI.GENERATORI_TERMICI.MODELLO," _
                    & "SISCOM_MI.GENERATORI_TERMICI.MATRICOLA,SISCOM_MI.GENERATORI_TERMICI.NOTE," _
                       & "SISCOM_MI.GENERATORI_TERMICI.ANNO_COSTRUZIONE,SISCOM_MI.GENERATORI_TERMICI.POTENZA,SISCOM_MI.GENERATORI_TERMICI.MARC_EFF_ENERGETICA,SISCOM_MI.GENERATORI_TERMICI.FLUIDO_TERMOVETTORE " _
              & " from SISCOM_MI.GENERATORI_TERMICI " _
              & " where SISCOM_MI.GENERATORI_TERMICI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.GENERATORI_TERMICI.MODELLO "


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "GENERATORI_TERMICI")


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
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliTELE_txtSelScambiatori').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliTELE_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagliTELE_txtSelScambiatori').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagliTELE_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiScambiatore() As Boolean

        ControlloCampiScambiatore = True


        If PAR.IfEmpty(Me.txtModelloG.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiScambiatore = False
            txtModelloG.Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazioneG.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazioneG.Text = ""
        End If

    End Function


    Protected Sub btn_InserisciScambiatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciScambiatore.Click
        If ControlloCampiScambiatore() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDG.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaScambiatori()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateScambiatori()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelScambiatori.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiScambiatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiScambiatore.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelScambiatori.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaScambiatori()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Generatori

                gen = New Epifani.Generatori(lstScambiatori.Count, PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200), Me.txtMatricolaG.Text, Me.txtNoteG.Text, Me.txtAnnoRealizzazioneG.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenza.Text, 0)), Me.txtFluido.Text, Me.cmbMarcatura.SelectedValue.ToString)

                DataGrid1.DataSource = Nothing
                lstScambiatori.Add(gen)
                gen = Nothing

                DataGrid1.DataSource = lstScambiatori
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into SISCOM_MI.GENERATORI_TERMICI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE,FLUIDO_TERMOVETTORE,MARC_EFF_ENERGETICA) " _
                                    & "values (SISCOM_MI.SEQ_GENERATORI_TERMICI.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricolaG.Text) & "','" & Me.txtAnnoRealizzazioneG.Text & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenza.Text, "Null")) & ",'" & PAR.PulisciStrSql(Me.txtNoteG.Text) & "','" _
                                        & PAR.PulisciStrSql(Me.txtFluido.Text) & "','" & Me.cmbMarcatura.SelectedValue.ToString & "')"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Scambiatori()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Scambiatore di calore")

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

    Private Sub UpdateScambiatori()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstScambiatori(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200)
                lstScambiatori(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaG.Text
                lstScambiatori(txtIdComponente.Text).NOTE = Me.txtNoteG.Text

                lstScambiatori(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneG.Text
                lstScambiatori(txtIdComponente.Text).POTENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenza.Text, 0))
                lstScambiatori(txtIdComponente.Text).FLUIDO_TERMOVETTORE = Me.txtFluido.Text
                lstScambiatori(txtIdComponente.Text).MARC_EFF_ENERGETICA = Me.cmbMarcatura.SelectedValue.ToString

                DataGrid1.DataSource = lstScambiatori
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "update SISCOM_MI.GENERATORI_TERMICI set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaG.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneG.Text & "'," _
                                            & "POTENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenza.Text, "Null")) & "," _
                                            & "NOTE='" & PAR.PulisciStrSql(Me.txtNoteG.Text) & "', " _
                                            & "FLUIDO_TERMOVETTORE='" & PAR.PulisciStrSql(Me.txtFluido.Text) & "', " _
                                            & "MARC_EFF_ENERGETICA='" & Me.cmbMarcatura.SelectedValue.ToString & "' " _
                                            & " where ID=" & Me.txtIDG.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Scambiatori()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Scambiatore di calore")

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

    Protected Sub btnApriScambiatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriScambiatore.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDG.Text = lstScambiatori(txtIdComponente.Text).ID
                    Me.txtModelloG.Text = PAR.IfNull(lstScambiatori(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaG.Text = PAR.IfNull(lstScambiatori(txtIdComponente.Text).MATRICOLA, "")
                    Me.txtNoteG.Text = PAR.IfNull(lstScambiatori(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazioneG.Text = PAR.IfNull(lstScambiatori(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtPotenza.Text = PAR.IfNull(lstScambiatori(txtIdComponente.Text).POTENZA, "")
                    Me.txtFluido.Text = PAR.IfNull(lstScambiatori(txtIdComponente.Text).FLUIDO_TERMOVETTORE, "")
                    'Me.cmbMarcatura.Items.FindByValue(PAR.IfNull(lstGeneratori(txtIdComponente.Text).MARC_EFF_ENERGETICA, "")).Selected = True
                    Me.cmbMarcatura.SelectedValue = PAR.IfNull(lstScambiatori(txtIdComponente.Text).MARC_EFF_ENERGETICA, "")
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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.GENERATORI_TERMICI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDG.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloG.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaG.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
                        Me.txtNoteG.Text = PAR.IfNull(myReader1("NOTE"), "")

                        Me.txtAnnoRealizzazioneG.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtPotenza.Text = PAR.IfNull(myReader1("POTENZA"), "")
                        Me.txtFluido.Text = PAR.IfNull(myReader1("FLUIDO_TERMOVETTORE"), "")
                        'Me.cmbMarcatura.Items.FindByValue(PAR.IfNull(myReader1("MARC_EFF_ENERGETICA"), "")).Selected = True
                        Me.cmbMarcatura.SelectedValue = PAR.IfNull(myReader1("MARC_EFF_ENERGETICA"), "")
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

    Protected Sub btnEliminaScambiatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaScambiatore.Click
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

                        lstScambiatori.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Generatori In lstScambiatori
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid1.DataSource = lstScambiatori
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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.GENERATORI_TERMICI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Scambiatori()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Scambiatore di calore")

                        End If
                    End If

                    txtSelScambiatori.Text = ""
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

    Protected Sub btnAggScambiatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggScambiatore.Click
        Try


            Me.txtIDG.Text = -1

            Me.txtModelloG.Text = ""
            Me.txtMatricolaG.Text = ""
            Me.txtNoteG.Text = ""
            Me.txtAnnoRealizzazioneG.Text = ""
            Me.txtFluido.Text = ""
            Me.txtPotenza.Text = ""
            Me.cmbMarcatura.Text = ""
            'End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub



    Private Sub FrmSolaLettura()
        Try

            Me.btnAggScambiatore.Visible = False
            Me.btnEliminaScambiatore.Visible = False
            Me.btnApriScambiatore.Visible = False

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
