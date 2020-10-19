Imports System.Collections

Partial Class Tab_Meteoriche_Dettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstPompeSM As System.Collections.Generic.List(Of Epifani.PompeSM)



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstPompeSM = CType(HttpContext.Current.Session.Item("LSTPOMPESM"), System.Collections.Generic.List(Of Epifani.PompeSM))

        Try
            If Not IsPostBack Then

                lstPompeSM.Clear()

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


        StringaSql = "  select ID,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,TIPO,POTENZA,PORTATA,PREVALENZA " _
                    & " from SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO " _
                    & " where SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO.ID_IMPIANTO = " & vIdImpianto _
                    & " order by SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO.MODELLO "


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_MET_POMPE_SOLLEVAMENTO")


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
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Meteoriche_Dettagli_txtSelPompe').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Meteoriche_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Meteoriche_Dettagli_txtSelPompe').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Meteoriche_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

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

                Dim gen As Epifani.PompeSM

                'gen = New Epifani.PompeSM(lstPompeSM.Count, PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)), PAR.PulisciStrSql(Me.txtMatricolaP.Text), Me.txtAnnoRealizzazioneP.Text, Me.cmbTipo.SelectedValue.ToString, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataP.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaP.Text, 0)))
                gen = New Epifani.PompeSM(lstPompeSM.Count, PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200), Me.txtMatricolaP.Text, Me.txtAnnoRealizzazioneP.Text, Me.cmbTipo.SelectedValue.ToString, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataP.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaP.Text, 0)))

                DataGrid3.DataSource = Nothing
                lstPompeSM.Add(gen)
                gen = Nothing

                DataGrid3.DataSource = lstPompeSM
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO(ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,TIPO,POTENZA,PORTATA,PREVALENZA) " _
                                    & "values (SISCOM_MI.SEQ_I_MET_POMPE_SOLLEVAMENTO.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricolaP.Text) & "','" & Me.txtAnnoRealizzazioneP.Text & "','" _
                                        & Me.cmbTipo.SelectedValue.ToString & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaP.Text, "Null")) & "," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPortataP.Text, "Null")) & "," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPrevalenzaP.Text, "Null")) & ")"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Pompe()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

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

                lstPompeSM(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200) 'PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200))
                lstPompeSM(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaP.Text 'PAR.PulisciStrSql(Me.txtMatricolaP.Text)

                lstPompeSM(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneP.Text
                lstPompeSM(txtIdComponente.Text).POTENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenzaP.Text, 0))
                lstPompeSM(txtIdComponente.Text).PORTATA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPortataP.Text, 0))
                lstPompeSM(txtIdComponente.Text).PREVALENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPrevalenzaP.Text, 0))

                lstPompeSM(txtIdComponente.Text).TIPO = Me.cmbTipo.SelectedValue.ToString

                DataGrid3.DataSource = lstPompeSM
                DataGrid3.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "update SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloP.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaP.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneP.Text & "'," _
                                            & "POTENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenzaP.Text, "Null")) & "," _
                                            & "PORTATA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPortataP.Text, "Null")) & "," _
                                            & "PREVALENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPrevalenzaP.Text, "Null")) & "," _
                                            & "TIPO='" & Me.cmbTipo.SelectedValue.ToString & "' " _
                                            & " where ID=" & Me.txtIDP.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Pompe()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

            End If

            CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

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
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDP.Text = lstPompeSM(txtIdComponente.Text).ID
                    Me.txtModelloP.Text = PAR.IfNull(lstPompeSM(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaP.Text = PAR.IfNull(lstPompeSM(txtIdComponente.Text).MATRICOLA, "")

                    Me.txtAnnoRealizzazioneP.Text = PAR.IfNull(lstPompeSM(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtPotenzaP.Text = PAR.IfNull(lstPompeSM(txtIdComponente.Text).POTENZA, "")
                    Me.txtPortataP.Text = PAR.IfNull(lstPompeSM(txtIdComponente.Text).PORTATA, "")
                    Me.txtPrevalenzaP.Text = PAR.IfNull(lstPompeSM(txtIdComponente.Text).PREVALENZA, "")
                    Me.cmbTipo.Items.FindByValue(PAR.IfNull(lstPompeSM(txtIdComponente.Text).TIPO, "")).Selected = True


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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDP.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModelloP.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricolaP.Text = PAR.IfNull(myReader1("MATRICOLA"), "")

                        Me.txtAnnoRealizzazioneP.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtPotenzaP.Text = PAR.IfNull(myReader1("POTENZA"), "")
                        Me.txtPortataP.Text = PAR.IfNull(myReader1("PORTATA"), "")
                        Me.txtPrevalenzaP.Text = PAR.IfNull(myReader1("PREVALENZA"), "")
                        Me.cmbTipo.SelectedValue = PAR.IfNull(myReader1("TIPO"), "")


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

                        lstPompeSM.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.PompeSM In lstPompeSM
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid3.DataSource = lstPompeSM
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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_MET_POMPE_SOLLEVAMENTO where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Pompe()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Pompe di Sollevamento")

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
            Me.cmbTipo.Text = ""

            Me.txtAnnoRealizzazioneP.Text = ""
            Me.txtPotenzaP.Text = ""
            Me.txtPortataP.Text = ""
            Me.txtPrevalenzaP.Text = ""

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

            Me.btnAggPompe.Visible = False
            Me.btnEliminaPompa.Visible = False
            Me.btnApriPompa.Visible = False


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
