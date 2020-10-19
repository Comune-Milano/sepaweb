Imports System.Collections

'TAB ELENCO IMPIANTO ELETTRICO DEI BOX
Partial Class TabElettricoBox
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstBox As System.Collections.Generic.List(Of Epifani.Box)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstBox = CType(HttpContext.Current.Session.Item("LSTBOX"), System.Collections.Generic.List(Of Epifani.Box))

        Try
            If Not IsPostBack Then

                lstBox.Clear()

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

                BindGrid_Box()
                CaricaDistribuzione()

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


    'I_ELE_BOX  GRID1
    Private Sub BindGrid_Box()
        Dim StringaSql As String

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_ELE_BOX.ID,SUP_9_AUTO,QUADRO,DIFFERENZIALE," _
                       & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                       & "ID_TIPO_DISTRIBUZIONE,PULSANTE_SGANCIO,PRATICA_VVF,VERIFICA," _
                       & "MESSA_TERRA, SCARICHE_ATMOSFERICHE,SCARICATORI,NOTE " _
              & " from SISCOM_MI.I_ELE_BOX,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
              & " where SISCOM_MI.I_ELE_BOX.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ELE_BOX.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
              & " order by SISCOM_MI.I_ELE_BOX.ID "

        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ELE_BOX")

        DataGridBox.DataSource = ds
        DataGridBox.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGridBox_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridBox.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoBox_txtSelBox').value='Hai selezionato: " & e.Item.Cells(3).Text & "';document.getElementById('TabElettricoBox_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoBox_txtSelBox').value='Hai selezionato: " & e.Item.Cells(3).Text & "';document.getElementById('TabElettricoBox_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    Function ControlloCampiBox() As Boolean

        ControlloCampiBox = True

        If PAR.IfEmpty(Me.cmbDifferenzialeBox.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Protezione Differenziale!');</script>")
            ControlloCampiBox = False
            Me.cmbDifferenzialeBox.Focus()
            Exit Function
        End If

    End Function

    Protected Sub btn_InserisciBox_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciBox.Click
        If ControlloCampiBox() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDBox.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaBox()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateBox()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelBox.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiBox_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiBox.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelBox.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Private Sub SalvaBox()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Box

                gen = New Epifani.Box(lstBox.Count, Me.cmbAutoBox.SelectedValue.ToString, Me.cmbQuadroBox.SelectedValue.ToString, Me.cmbDifferenzialeBox.SelectedItem.Text, Me.cmbDistribuzioneBox.SelectedItem.Text, Me.cmbDistribuzioneBox.SelectedValue, Me.cmbSgancioBox.SelectedValue.ToString, Me.cmbPraticaBox.SelectedValue.ToString, Me.cmbVerificaBox.SelectedValue.ToString, Me.cmbMessaTerraBox.SelectedValue.ToString, Me.cmbScaricoBox.SelectedValue.ToString, Me.cmbScaricatoriBox.SelectedValue.ToString, PAR.PulisciStrSql(Me.txtNoteBox.Text))

                DataGridBox.DataSource = Nothing
                lstBox.Add(gen)
                gen = Nothing

                DataGridBox.DataSource = lstBox
                DataGridBox.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_BOX (ID, ID_IMPIANTO,SUP_9_AUTO,QUADRO,DIFFERENZIALE," _
                                                            & "ID_TIPO_DISTRIBUZIONE,PULSANTE_SGANCIO,PRATICA_VVF," _
                                                            & "VERIFICA,MESSA_TERRA,SCARICHE_ATMOSFERICHE,SCARICATORI,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_BOX.NEXTVAL,:id_impianto,:auto,:quadro,:differenziale," _
                                        & ":id_distribuzione,:sgancio,:pratica,:verifiche,:messaterra,:scariche,:scaricatori,:note) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("auto", Me.cmbAutoBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quadro", Me.cmbQuadroBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeBox.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneBox.SelectedValue))))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("sgancio", Me.cmbSgancioBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pratica", Me.cmbPraticaBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("verifiche", Me.cmbVerificaBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("messaterra", Me.cmbMessaTerraBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scariche", Me.cmbScaricoBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scaricatori", Me.cmbScaricatoriBox.SelectedValue.ToString))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNoteBox.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Impianti Elettrici Box")


                BindGrid_Box()

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

    Private Sub UpdateBox()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstBox(txtIdComponente.Text).SUP_9_AUTO = Me.cmbAutoBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).QUADRO = Me.cmbQuadroBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).DIFFERENZIALE = Me.cmbDifferenzialeBox.SelectedItem.Text
                lstBox(txtIdComponente.Text).DISTRIBUZIONE = Me.cmbDistribuzioneBox.SelectedItem.ToString
                lstBox(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE = Me.cmbDistribuzioneBox.SelectedValue

                lstBox(txtIdComponente.Text).PULSANTE_SGANCIO = Me.cmbSgancioBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).PRATICA_VVF = Me.cmbPraticaBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).VERIFICA = Me.cmbVerificaBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).MESSA_TERRA = Me.cmbMessaTerraBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).SCARICHE_ATMOSFERICHE = Me.cmbScaricoBox.SelectedValue.ToString
                lstBox(txtIdComponente.Text).SCARICATORI = Me.cmbScaricatoriBox.SelectedValue.ToString

                lstBox(txtIdComponente.Text).NOTE = PAR.PulisciStrSql(Me.txtNoteBox.Text)

                DataGridBox.DataSource = lstBox
                DataGridBox.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_ELE_BOX set " _
                                            & "SUP_9_AUTO=:auto,QUADRO=:quadro, DIFFERENZIALE=:differenziale,ID_TIPO_DISTRIBUZIONE=:id_distribuzione," _
                                            & "PULSANTE_SGANCIO=:sgancio,PRATICA_VVF=:pratica,VERIFICA=:verifica,MESSA_TERRA=:messaterra," _
                                            & "SCARICHE_ATMOSFERICHE=:scariche,SCARICATORI=:scaricatori,NOTE=:note " _
                                   & " where ID=" & Me.txtIDBox.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("auto", Me.cmbAutoBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quadro", Me.cmbQuadroBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeBox.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneBox.SelectedValue))))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("sgancio", Me.cmbSgancioBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pratica", Me.cmbPraticaBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("verifica", Me.cmbVerificaBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("messaterra", Me.cmbMessaTerraBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scariche", Me.cmbScaricoBox.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scaricatori", Me.cmbScaricatoriBox.SelectedValue.ToString))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNoteBox.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Impianti Elettrici Box")

                BindGrid_Box()

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


    Protected Sub btnApriBox_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriBox.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDBox.Text = lstBox(txtIdComponente.Text).ID

                    Me.cmbAutoBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).SUP_9_AUTO, "")
                    Me.cmbQuadroBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).QUADRO, "")

                    Me.cmbDifferenzialeBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).DIFFERENZIALE, "")
                    Me.cmbDistribuzioneBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE, -1)

                    Me.cmbSgancioBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).PULSANTE_SGANCIO, "")
                    Me.cmbPraticaBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).PRATICA_VVF, "")
                    Me.cmbVerificaBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).VERIFICA, "")

                    Me.cmbMessaTerraBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).MESSA_TERRA, "")
                    Me.cmbScaricoBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).SCARICHE_ATMOSFERICHE, "")
                    Me.cmbScaricatoriBox.SelectedValue = PAR.IfNull(lstBox(txtIdComponente.Text).SCARICATORI, "")

                    Me.txtNoteBox.Text = PAR.IfNull(lstBox(txtIdComponente.Text).NOTE, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ELE_BOX where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDBox.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.cmbAutoBox.SelectedValue = PAR.IfNull(myReader1("SUP_9_AUTO"), "")
                        Me.cmbQuadroBox.SelectedValue = PAR.IfNull(myReader1("QUADRO"), "")
                        Me.cmbDifferenzialeBox.Text = PAR.IfNull(myReader1("DIFFERENZIALE"), "")
                        Me.cmbDistribuzioneBox.SelectedValue = PAR.IfNull(myReader1("ID_TIPO_DISTRIBUZIONE"), -1)

                        Me.cmbSgancioBox.SelectedValue = PAR.IfNull(myReader1("PULSANTE_SGANCIO"), "")
                        Me.cmbPraticaBox.SelectedValue = PAR.IfNull(myReader1("PRATICA_VVF"), "")
                        Me.cmbVerificaBox.SelectedValue = PAR.IfNull(myReader1("VERIFICA"), "")
                        Me.cmbMessaTerraBox.SelectedValue = PAR.IfNull(myReader1("MESSA_TERRA"), "")
                        Me.cmbScaricoBox.SelectedValue = PAR.IfNull(myReader1("SCARICHE_ATMOSFERICHE"), "")
                        Me.cmbScaricatoriBox.SelectedValue = PAR.IfNull(myReader1("SCARICATORI"), "")

                        Me.txtNoteBox.Text = PAR.IfNull(myReader1("NOTE"), "")

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

    Protected Sub btnEliminaBox_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaBox.Click
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

                        lstBox.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Box In lstBox
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridBox.DataSource = lstBox
                        DataGridBox.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ELE_BOX where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Box()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Impianti Elettrici Box")

                        End If
                    End If

                    txtSelBox.Text = ""
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

    Protected Sub btnAggBox_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggBox.Click
        Try
            Me.txtIDBox.Text = -1

            Me.cmbAutoBox.Text = ""
            Me.cmbQuadroBox.Text = ""
            Me.cmbDifferenzialeBox.Text = ""
            Me.cmbDistribuzioneBox.SelectedIndex = -1

            Me.cmbSgancioBox.Text = ""
            Me.cmbPraticaBox.Text = ""
            Me.cmbVerificaBox.Text = ""
            Me.cmbMessaTerraBox.Text = ""
            Me.cmbScaricoBox.Text = ""
            Me.cmbScaricoBox.Text = ""
            Me.cmbScaricatoriBox.Text = ""

            Me.txtNoteBox.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggBox.Visible = False
            Me.btnEliminaBox.Visible = False
            Me.btnApriBox.Visible = False

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
            cmbDistribuzioneBox.Items.Add(New ListItem("", -1))
            While myReader1.Read
                cmbDistribuzioneBox.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            cmbDistribuzioneBox.SelectedValue = "-1"


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


End Class
