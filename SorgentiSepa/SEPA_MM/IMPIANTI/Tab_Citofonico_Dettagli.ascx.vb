'TAB ELENCO IMPIANTO CITOFONI/VIDEOCITOFONI
Imports System.Collections

Partial Class Tab_Citofonico_Dettagli
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstCitofoni As System.Collections.Generic.List(Of Epifani.Citofoni)
    Dim lstScaleSel As System.Collections.Generic.List(Of Epifani.Scale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lstCitofoni = CType(HttpContext.Current.Session.Item("LSTCITOFONI"), System.Collections.Generic.List(Of Epifani.Citofoni))
        lstScaleSel = CType(HttpContext.Current.Session.Item("LSTSCALE_SEL"), System.Collections.Generic.List(Of Epifani.Scale))


        Try
            If Not IsPostBack Then

                lstCitofoni.Clear()
                lstScaleSel.Clear()

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

                BindGrid_Citofoni()
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


    '*** CITOFONI
    Private Sub BindGrid_Citofoni()
        Dim StringaSql As String

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.I_CIT_DETTAGLI.ID,SISCOM_MI.I_CIT_DETTAGLI.TIPOLOGIA,SISCOM_MI.I_CIT_DETTAGLI.UBICAZIONE," _
                          & "SISCOM_MI.I_CIT_DETTAGLI.TASTIERA,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.DESCRIZIONE AS ""DISTRIBUZIONE""," _
                          & "SISCOM_MI.I_CIT_DETTAGLI.ID_TIPO_DISTRIBUZIONE, SISCOM_MI.I_CIT_DETTAGLI.QUANTITA," _
                       & " (select count(*) from SISCOM_MI.I_CIT_DETTAGLI_SCALE where ID_I_CIT_DETTAGLI=SISCOM_MI.I_CIT_DETTAGLI.ID) AS ""SCALE_SERVITE"" " _
                  & " from SISCOM_MI.I_CIT_DETTAGLI,SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE " _
                  & " where SISCOM_MI.I_CIT_DETTAGLI.ID_IMPIANTO = " & vIdImpianto _
                  & " and   SISCOM_MI.I_CIT_DETTAGLI.ID_TIPO_DISTRIBUZIONE=SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE.ID (+) " _
                  & " order by SISCOM_MI.I_CIT_DETTAGLI.ID "

        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        da.Fill(ds, "I_CIT_DETTAGLI")

        DataGridCI.DataSource = ds
        DataGridCI.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGridCI_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridCI.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Citofonico_Dettagli_txtSelCI').value='Hai selezionato: " & e.Item.Cells(1).Text & " - " & e.Item.Cells(2).Text & "';document.getElementById('Tab_Citofonico_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Citofonico_Dettagli_txtSelCI').value='Hai selezionato: " & e.Item.Cells(1).Text & " - " & e.Item.Cells(2).Text & "';document.getElementById('Tab_Citofonico_Dettagli_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiCI() As Boolean

        ControlloCampiCI = True

        If PAR.IfEmpty(Me.cmbTipo.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la tipologia del citofono!');</script>")
            ControlloCampiCI = False
            Me.cmbTipo.Focus()
            Exit Function
        End If

        If PAR.IfEmpty(Me.cmbUbicazione.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire l\'ubicazione del citofono!');</script>")
            ControlloCampiCI = False
            Me.cmbUbicazione.Focus()
            Exit Function
        End If

    End Function

    Protected Sub btn_InserisciCI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciCI.Click
        If ControlloCampiCI() = False Then
            txtAppareCI.Text = "1"
            Exit Sub
        End If

        If Me.txtIDCI.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaCI()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateCI()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelCI.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Protected Sub btn_ChiudiCI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiCI.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelCI.Text = ""
        txtIdComponente.Text = ""
    End Sub

    Private Sub SalvaCI()
        Dim i As Integer
        Dim RigaCitofono As Integer
        Dim vIdCI As Integer
        Dim ContaScaleServite As Integer

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Citofoni
                RigaCitofono = lstCitofoni.Count
                ContaScaleServite = 0

                '***********SCALE SERVITE
                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then

                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel.Count, Str(RigaCitofono), Str(CheckBoxScale.Items(i).Value))
                        lstScaleSel.Add(genS)
                        genS = Nothing
                        ContaScaleServite = ContaScaleServite + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next

                gen = New Epifani.Citofoni(lstCitofoni.Count, Me.cmbTipo.SelectedValue.ToString, Me.cmbUbicazione.SelectedValue.ToString, Me.cmbTastiera.SelectedValue.ToString, Me.cmbDistribuzioneC.SelectedItem.Text, Me.cmbDistribuzioneC.SelectedValue, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtQuantita.Text, 1)), ContaScaleServite)

                DataGridCI.DataSource = Nothing
                lstCitofoni.Add(gen)
                gen = Nothing

                DataGridCI.DataSource = lstCitofoni
                DataGridCI.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_CIT_DETTAGLI (ID, ID_IMPIANTO,TIPOLOGIA,UBICAZIONE,TASTIERA,ID_TIPO_DISTRIBUZIONE,QUANTITA) " _
                                    & "values (SISCOM_MI.SEQ_I_CIT_DETTAGLI.NEXTVAL,:id_impianto,:tipo,:ubicazione,:tastiera,:id_distribuzione,:quantita) "

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", Me.cmbTipo.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Me.cmbUbicazione.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tastiera", Me.cmbTastiera.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneC.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", strToNumber(PAR.IfEmpty(Me.txtQuantita.Text, 1))))


                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Dettagli Citofoni")


                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_CIT_DETTAGLI.CURRVAL FROM dual "
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdCI = myReader1(0)
                End If
                myReader1.Close()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_CIT_DETTAGLI_SCALE  (ID_I_CIT_DETTAGLI,ID_SCALA) values " _
                                   & "(" & vIdCI & "," & CheckBoxScale.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""
                    End If
                Next

                BindGrid_Citofoni()

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

    Private Sub UpdateCI()
        Dim i As Integer
        Dim ContaScaleServite As Integer

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                ContaScaleServite = 0
                '***********SCALE
SCALE:

                For i = 0 To lstScaleSel.Count - 1
                    If lstScaleSel(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstScaleSel.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next

                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                        Dim genS As Epifani.Scale
                        genS = New Epifani.Scale(lstScaleSel.Count, Str(txtIdComponente.Text), Str(CheckBoxScale.Items(i).Value))
                        lstScaleSel.Add(genS)
                        genS = Nothing
                        ContaScaleServite = ContaScaleServite + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next
                '*************************

                lstCitofoni(txtIdComponente.Text).TIPOLOGIA = Me.cmbTipo.SelectedValue.ToString
                lstCitofoni(txtIdComponente.Text).UBICAZIONE = Me.cmbUbicazione.SelectedValue.ToString
                lstCitofoni(txtIdComponente.Text).TASTIERA = Me.cmbTastiera.SelectedValue.ToString

                lstCitofoni(txtIdComponente.Text).DISTRIBUZIONE = Me.cmbDistribuzioneC.SelectedItem.ToString
                lstCitofoni(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE = Me.cmbDistribuzioneC.SelectedValue

                lstCitofoni(txtIdComponente.Text).QUANTITA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtQuantita.Text, 1))

                lstCitofoni(txtIdComponente.Text).SCALE_SERVITE = ContaScaleServite

                DataGridCI.DataSource = lstCitofoni
                DataGridCI.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                PAR.cmd.CommandText = "update SISCOM_MI.I_CIT_DETTAGLI set " _
                                            & "TIPOLOGIA=:tipo,UBICAZIONE=:ubicazione, " _
                                            & "TASTIERA=:tastiera,ID_TIPO_DISTRIBUZIONE=:id_distribuzione,QUANTITA=:quantita " _
                                   & " where ID=" & Me.txtIDCI.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", Me.cmbTipo.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ubicazione", Me.cmbUbicazione.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tastiera", Me.cmbTastiera.SelectedValue.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_distribuzione", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbDistribuzioneC.SelectedValue))))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", strToNumber(PAR.IfEmpty(Me.txtQuantita.Text, 1))))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Citofoni o Videocitofoni")


                PAR.cmd.CommandText = "delete from SISCOM_MI.I_CIT_DETTAGLI_SCALE where ID_I_CIT_DETTAGLI= " & Me.txtIDCI.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""

                For i = 0 To CheckBoxScale.Items.Count - 1
                    If CheckBoxScale.Items(i).Selected = True And Str(CheckBoxScale.Items(i).Value) > -1 Then
                        PAR.cmd.CommandText = "insert into SISCOM_MI.I_CIT_DETTAGLI_SCALE  (ID_I_CIT_DETTAGLI,ID_SCALA) values " _
                                   & "(" & Me.txtIDCI.Text & "," & CheckBoxScale.Items(i).Value & ")"

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""
                    End If
                Next

                BindGrid_Citofoni()

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

    Protected Sub btnApriCI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriCI.Click
        Dim i, j As Integer

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareCI.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDCI.Text = lstCitofoni(txtIdComponente.Text).ID

                    Me.cmbTipo.SelectedValue = PAR.IfNull(lstCitofoni(txtIdComponente.Text).TIPOLOGIA, "")
                    Me.cmbUbicazione.SelectedValue = PAR.IfNull(lstCitofoni(txtIdComponente.Text).UBICAZIONE, "")
                    Me.cmbTastiera.SelectedValue = PAR.IfNull(lstCitofoni(txtIdComponente.Text).TASTIERA, "")

                    Me.cmbDistribuzioneC.SelectedValue = PAR.IfNull(lstCitofoni(txtIdComponente.Text).ID_TIPO_DISTRIBUZIONE, -1)

                    Me.txtQuantita.Text = PAR.IfNull(lstCitofoni(txtIdComponente.Text).QUANTITA, "")

                    For i = 0 To CheckBoxScale.Items.Count - 1
                        CheckBoxScale.Items(i).Selected = False
                    Next

                    For i = 0 To CheckBoxScale.Items.Count - 1
                        For j = 0 To lstScaleSel.Count - 1
                            If Val(lstScaleSel(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxScale.Items(i).Value = Val(lstScaleSel(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxScale.Items(i).Selected = True
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

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                    PAR.cmd.CommandText = "select * from SISCOM_MI.I_CIT_DETTAGLI where ID=" & txtIdComponente.Text
                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then

                        Me.txtIDCI.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.cmbTipo.SelectedValue = PAR.IfNull(myReader1("TIPOLOGIA"), "")
                        Me.cmbUbicazione.SelectedValue = PAR.IfNull(myReader1("UBICAZIONE"), "")
                        Me.cmbTastiera.SelectedValue = PAR.IfNull(myReader1("TASTIERA"), "")
                        Me.cmbDistribuzioneC.SelectedValue = PAR.IfNull(myReader1("ID_TIPO_DISTRIBUZIONE"), -1)
                        Me.txtQuantita.Text = PAR.IfNull(myReader1("QUANTITA"), "")

                    End If
                    myReader1.Close()

                    '*** SCALE I_CIT_DETTAGLI_SCALE
                    '***Azzero la lista delle scale
                    For i = 0 To CheckBoxScale.Items.Count - 1
                        CheckBoxScale.Items(i).Selected = False
                    Next

                    '*** check della scala salvata
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select ID_SCALA from SISCOM_MI.I_CIT_DETTAGLI_SCALE where  ID_I_CIT_DETTAGLI = " & Me.txtIDCI.Text

                    myReader2 = PAR.cmd.ExecuteReader()

                    While myReader2.Read
                        For i = 0 To CheckBoxScale.Items.Count - 1
                            If CheckBoxScale.Items(i).Value = PAR.IfNull(myReader2("ID_SCALA"), "-1") Then
                                CheckBoxScale.Items(i).Selected = True
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

    Protected Sub btnEliminaCI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaCI.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareCI.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstCitofoni.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Citofoni In lstCitofoni
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridCI.DataSource = lstCitofoni
                        DataGridCI.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_CIT_DETTAGLI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Citofoni()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Citofoni o Videocitofoni")

                        End If
                    End If

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

    Protected Sub btnAggCI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggCI.Click
        Dim i As Integer

        Try
            Me.txtIDCI.Text = -1

            Me.cmbTipo.Text = ""
            Me.cmbUbicazione.Text = ""
            Me.cmbTastiera.Text = ""
            Me.cmbDistribuzioneC.SelectedIndex = -1
            Me.txtQuantita.Text = 1

            For i = 0 To CheckBoxScale.Items.Count - 1
                CheckBoxScale.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub
    '*************



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

    Private Sub FrmSolaLettura()
        Try

            Me.btnAggCI.Visible = False
            Me.btnEliminaCI.Visible = False
            Me.btnApriCI.Visible = False


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

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function


    Private Sub CaricaDistribuzione()

        Try
            PAR.cmd.CommandText = "select distinct ID,DESCRIZIONE from SISCOM_MI.TIPO_DITRIBUZIONE_I_ELE order by DESCRIZIONE asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            cmbDistribuzioneC.Items.Add(New ListItem("", -1))
            While myReader1.Read
                cmbDistribuzioneC.Items.Add(New ListItem(PAR.IfNull(myReader1("DESCRIZIONE"), " "), PAR.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            cmbDistribuzioneC.SelectedValue = "-1"


        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

End Class
