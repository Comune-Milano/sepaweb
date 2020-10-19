Imports System.Collections

Partial Class TabElettricoPortineria
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstPortineria As System.Collections.Generic.List(Of Epifani.Portineria)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstPortineria = CType(HttpContext.Current.Session.Item("LSTPORTINERIA"), System.Collections.Generic.List(Of Epifani.Portineria))

        Try
            If Not IsPostBack Then

                lstPortineria.Clear()

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

                BindGrid_Portineria()
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


    'PORTINERIA I_ELE_PORTINERIA  GRID1
    Private Sub BindGrid_Portineria()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select SISCOM_MI.I_ELE_PORTINERIA.ID,QUADRO," _
                    & "DIFFERENZIALE,NORMA," _
                       & "SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                       & "ID_TIPO_DISTRIBUZIONE,NOTE " _
              & " from SISCOM_MI.I_ELE_PORTINERIA,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
              & " where SISCOM_MI.I_ELE_PORTINERIA.ID_IMPIANTO = " & vIdImpianto _
              & " and   SISCOM_MI.I_ELE_PORTINERIA.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
              & " order by SISCOM_MI.I_ELE_PORTINERIA.ID "


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_ELE_PORTINERIA")


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
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoPortineria_txtSelPortineria').value='Hai selezionato: " & e.Item.Cells(2).Text & "';document.getElementById('TabElettricoPortineria_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabElettricoPortineria_txtSelPortineria').value='Hai selezionato: " & e.Item.Cells(2).Text & "';document.getElementById('TabElettricoPortineria_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub


    Function ControlloCampiPortineria() As Boolean

        ControlloCampiPortineria = True


        If PAR.IfEmpty(Me.cmbDifferenzialeP.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Protezione Differenziale!');</script>")
            ControlloCampiPortineria = False
            Me.cmbDifferenzialeP.Focus()
            Exit Function
        End If


        'If Me.txtAnnoRealizzazioneG.Text = "dd/mm/YYYY" Then
        '    Me.txtAnnoRealizzazioneG.Text = ""
        'End If

    End Function


    Protected Sub btn_InserisciPortineria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciPortineria.Click
        If ControlloCampiPortineria() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDP.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaPortineria()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdatePortineria()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelPortineria.Text = ""
        txtIdComponente.Text = ""

    End Sub


    Protected Sub btn_ChiudiPortineria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiPortineria.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelPortineria.Text = ""
        txtIdComponente.Text = ""
    End Sub


    Private Sub SalvaPortineria()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Portineria

                gen = New Epifani.Portineria(lstPortineria.Count, Me.cmbQuadroP.SelectedValue.ToString, Me.cmbDifferenzialeP.SelectedItem.Text, Me.cmbNormaP.SelectedValue.ToString, Me.cmbDistribuzioneP.SelectedItem.Text, Me.cmbDistribuzioneP.SelectedValue, PAR.PulisciStrSql(Me.txtNoteP.Text))

                DataGrid1.DataSource = Nothing
                lstPortineria.Add(gen)
                gen = Nothing

                DataGrid1.DataSource = lstPortineria
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

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ELE_PORTINERIA (ID, ID_IMPIANTO,QUADRO,DIFFERENZIALE,NORMA,ID_TIPO_DISTRIBUZIONE,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_I_ELE_PORTINERIA.NEXTVAL,:id_impianto,:quadro,:differenziale,:norma,:id_distribuzione,:note) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quadro", Me.cmbQuadroP.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeP.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", Me.cmbNormaP.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneP.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNoteP.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Portineria()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Portineria")

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

    Private Sub UpdatePortineria()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstPortineria(txtIdComponente.Text).QUADRO = Me.cmbQuadroP.SelectedValue.ToString
                lstPortineria(txtIdComponente.Text).DIFFERENZIALE = Me.cmbDifferenzialeP.SelectedItem.Text
                lstPortineria(txtIdComponente.Text).NORMA = Me.cmbNormaP.SelectedValue.ToString
                lstPortineria(txtIdComponente.Text).DISTRIBUZIONE = Me.cmbDistribuzioneP.SelectedItem.ToString
                lstPortineria(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE = Me.cmbDistribuzioneP.SelectedValue
                lstPortineria(txtIdComponente.Text).NOTE = PAR.PulisciStrSql(Me.txtNoteP.Text)

                DataGrid1.DataSource = lstPortineria
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

                ' "ID_TIPO_DISTRIBUZIONE=" & Me.cmbDistribuzioneP.SelectedValue.ToString & "," _

                PAR.cmd.CommandText = "update SISCOM_MI.I_ELE_PORTINERIA set " _
                                            & "QUADRO=:quadro, DIFFERENZIALE=:differenziale, NORMA=:norma,ID_TIPO_DISTRIBUZIONE=:id_distribuzione,NOTE=:note " _
                                   & " where ID=" & Me.txtIDP.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quadro", Me.cmbQuadroP.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("differenziale", Me.cmbDifferenzialeP.SelectedItem.Text))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("norma", Me.cmbNormaP.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneP.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNoteP.Text, 300)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                BindGrid_Portineria()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Portineria")

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



    Protected Sub btnApriPortineria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriPortineria.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDP.Text = lstPortineria(txtIdComponente.Text).ID

                    Me.cmbQuadroP.SelectedValue = PAR.IfNull(lstPortineria(txtIdComponente.Text).QUADRO, "")
                    Me.cmbDifferenzialeP.SelectedValue = PAR.IfNull(lstPortineria(txtIdComponente.Text).DIFFERENZIALE, "")
                    Me.cmbNormaP.SelectedValue = PAR.IfNull(lstPortineria(txtIdComponente.Text).NORMA, "")

                    Me.cmbDistribuzioneP.SelectedValue = PAR.IfNull(lstPortineria(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE, -1)

                    Me.txtNoteP.Text = PAR.IfNull(lstPortineria(txtIdComponente.Text).NOTE, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_ELE_PORTINERIA where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDP.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.cmbQuadroP.SelectedValue = PAR.IfNull(myReader1("QUADRO"), "")

                        Me.cmbDifferenzialeP.Text = PAR.IfNull(myReader1("DIFFERENZIALE"), "")
                        Me.cmbNormaP.SelectedValue = PAR.IfNull(myReader1("NORMA"), "")
                        'Me.cmbDistribuzioneP.Items.FindByValue(PAR.IfNull(myReader1("ID_TIPO_DISTRIBUZIONE"), "")).Selected = True
                        Me.cmbDistribuzioneP.SelectedValue = PAR.IfNull(myReader1("ID_TIPO_DISTRIBUZIONE"), -1)

                        Me.txtNoteP.Text = PAR.IfNull(myReader1("NOTE"), "")

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



    Protected Sub btnEliminaPortineria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaPortineria.Click
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

                        lstPortineria.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Portineria In lstPortineria
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid1.DataSource = lstPortineria
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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ELE_PORTINERIA where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Portineria()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Portineria")

                        End If
                    End If

                    txtSelPortineria.Text = ""
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



    Protected Sub btnAggPortineria_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggPortineria.Click
        Try
            Me.txtIDP.Text = -1

            Me.cmbQuadroP.Text = ""
            Me.cmbDifferenzialeP.Text = ""
            Me.cmbNormaP.Text = ""
            Me.cmbDistribuzioneP.SelectedIndex = -1
            Me.txtNoteP.Text = ""

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FrmSolaLettura()
        Try

            Me.btnAggPortineria.Visible = False
            Me.btnEliminaPortineria.Visible = False
            Me.btnApriPortineria.Visible = False

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
            cmbDistribuzioneP.Items.Add(New ListItem("", -1))
            While myReader1.Read
                cmbDistribuzioneP.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            cmbDistribuzioneP.SelectedValue = "-1"


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
