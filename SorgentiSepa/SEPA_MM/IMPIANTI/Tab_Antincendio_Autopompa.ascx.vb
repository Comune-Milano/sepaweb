'*** 'TAB ELENCO ATTACCO AUTOPOMPA del'IMPIANTO ANTINCENDIO
Imports System.Collections

Partial Class Tab_Antincendio_Autopompa
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global
    Dim bVerifica As Boolean

    Dim lstAutoPompa As System.Collections.Generic.List(Of Epifani.AutoPompa)
    Dim lstPianiAutoPompaSel As System.Collections.Generic.List(Of Epifani.Scale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstAutoPompa = CType(HttpContext.Current.Session.Item("LSTAUTOPOMPA"), System.Collections.Generic.List(Of Epifani.AutoPompa))
        lstPianiAutoPompaSel = CType(HttpContext.Current.Session.Item("LSTPIANIAUTOPOMPA_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

        Try
            If Not IsPostBack Then

                lstAutoPompa.Clear()
                lstPianiAutoPompaSel.Clear()

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

                BindGrid_AutoPompa()

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


    'CARICAMENTO GRIGLIA ATTACCO AUTOPOMPA
    Private Sub BindGrid_AutoPompa()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        '& "DECODE(ES_PRESCRIZIONE,'S','SI','N','NO','P','PARZIALE') AS ""ES_PRESCRIZIONE"" " 

        StringaSql = "  select SISCOM_MI.I_ANT_AUTOPOMPA.ID," _
                        & " (select count(*) from SISCOM_MI.I_ANT_AUTOPOMPA_PIANI where  SISCOM_MI.I_ANT_AUTOPOMPA_PIANI.ID_ANT_AUTOPOMPA=SISCOM_MI.I_ANT_AUTOPOMPA.ID) AS ""PIANI""," _
                        & " SISCOM_MI.I_ANT_AUTOPOMPA.BOCCA_COLLEGAMENTO " _
                    & " from SISCOM_MI.I_ANT_AUTOPOMPA " _
                    & " where SISCOM_MI.I_ANT_AUTOPOMPA.ID_IMPIANTO=" & vIdImpianto _
                    & " order by PIANI "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "I_ANT_AUTOPOMPA")

        DataGridAUTOP.DataSource = ds
        DataGridAUTOP.DataBind()

        ds.Dispose()

    End Sub



    'GRID AUTOPOMPA
    Protected Sub DataGridAUTOP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAUTOP.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Autopompa_txtSelAUTOP').value='Hai selezionato: PIANI: " & e.Item.Cells(1).Text & " - BOCCA DI COLLEGAMENTO: " & e.Item.Cells(2).Text & "';document.getElementById('Tab_Antincendio_Autopompa_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Autopompa_txtSelAUTOP').value='Hai selezionato: PIANI: " & e.Item.Cells(1).Text & " - BOCCA DI COLLEGAMENTO: " & e.Item.Cells(2).Text & "';document.getElementById('Tab_Antincendio_Autopompa_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub



    Function ControlloCampi() As Boolean

        Try

            ControlloCampi = True

            ' BOCCA AUTOPOMPA
            If Me.cmbBocca.SelectedValue = "" Then
                Response.Write("<script>alert('Inserire se ha la bocca di collegamento!');</script>")
                ControlloCampi = False
                cmbBocca.Focus()
                Exit Function
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

    Protected Sub btn_InserisciAUTOP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciAUTOP.Click
        If ControlloCampi() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDAUTOP.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.Salva()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.Update()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelAUTOP.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiAUTOP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiAUTOP.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        Me.cmbBocca.Enabled = True

        Me.btn_InserisciAUTOP.Visible = True

        txtSelAUTOP.Text = ""
        txtIdComponente.Text = ""


    End Sub


    Private Sub Salva()
        Dim i As Integer
        Dim RigaPiano As Integer
        Dim ContaPianiServiti As Integer
        Dim sCodPiano As String
        Dim StringaSQL As String

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.AutoPompa
                RigaPiano = lstAutoPompa.Count
                ContaPianiServiti = 0

                '***********PIANI SERVITI
                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then

                        Dim genS As Epifani.Scale

                        If Strings.Len(Strings.Trim(Str(CheckBoxPiano.Items(i).Value))) = 1 Then
                            genS = New Epifani.Scale(lstPianiAutoPompaSel.Count, Str(RigaPiano), "0" & Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        Else
                            genS = New Epifani.Scale(lstPianiAutoPompaSel.Count, Str(RigaPiano), Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        End If
                        lstPianiAutoPompaSel.Add(genS)
                        genS = Nothing
                        ContaPianiServiti = ContaPianiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next

                gen = New Epifani.AutoPompa(lstAutoPompa.Count, ContaPianiServiti, Me.cmbBocca.SelectedValue.ToString)

                DataGridAUTOP.DataSource = Nothing
                lstAutoPompa.Add(gen)
                gen = Nothing

                DataGridAUTOP.DataSource = lstAutoPompa
                DataGridAUTOP.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                'INSERISCO IL NUOVO ATTACCO AUTOPOMPA

                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_AUTOPOMPA (ID,ID_IMPIANTO, BOCCA_COLLEGAMENTO) " _
                                & " values (SISCOM_MI.SEQ_I_ANT_AUTOPOMPA.NEXTVAL,:id_impianto,:bocca)"

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("bocca", Me.cmbBocca.SelectedValue.ToString))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()


                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Inserimento ATTACCO AUTOPOMPA")


                '**** Ricavo ID dell'ATTACCO AUTOPOMPA
                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_AUTOPOMPA.CURRVAL FROM dual "
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderI.Read Then
                    Me.txtIDAUTOP.Text = myReaderI(0)
                End If
                myReaderI.Close()
                PAR.cmd.CommandText = ""
                '**********

                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then

                        StringaSQL = "insert into SISCOM_MI.I_ANT_AUTOPOMPA_PIANI  (ID_ANT_AUTOPOMPA,COD_LIVELLO_PIANO) values "

                        If Strings.Len(CheckBoxPiano.Items(i).Value) = 1 Then
                            sCodPiano = "0" & CheckBoxPiano.Items(i).Value
                        Else
                            sCodPiano = CheckBoxPiano.Items(i).Value
                        End If

                        StringaSQL = StringaSQL & "(" & Me.txtIDAUTOP.Text & ",'" & sCodPiano & "')"

                        PAR.cmd.CommandText = StringaSQL

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                    End If
                Next

                If CheckBoxPiano.Items.Count > 0 Then
                    '*** EVENTI_IMPIANTI
                    PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Piani dell'Attacco Autopompa")
                End If

                BindGrid_AutoPompa()
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
        Dim i As Integer
        Dim ContaPianiServiti As Integer
        Dim sCodPiano As String
        Dim StringaSQL As String

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO


                ContaPianiServiti = 0
                '***********SCALE
SCALE:
                For i = 0 To lstPianiAutoPompaSel.Count - 1
                    If lstPianiAutoPompaSel(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstPianiAutoPompaSel.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next

                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then
                        Dim genS As Epifani.Scale

                        If Strings.Len(Strings.Trim(Str(CheckBoxPiano.Items(i).Value))) = 1 Then
                            genS = New Epifani.Scale(lstPianiAutoPompaSel.Count, Str(txtIdComponente.Text), "0" & Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        Else
                            genS = New Epifani.Scale(lstPianiAutoPompaSel.Count, Str(txtIdComponente.Text), Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        End If

                        lstPianiAutoPompaSel.Add(genS)
                        genS = Nothing
                        ContaPianiServiti = ContaPianiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next
                '*************************

                lstAutoPompa(txtIdComponente.Text).BOCCA_COLLEGAMENTO = Me.cmbBocca.SelectedValue.ToString
                lstAutoPompa(txtIdComponente.Text).PIANI = ContaPianiServiti

                DataGridAUTOP.DataSource = lstAutoPompa
                DataGridAUTOP.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                PAR.cmd.CommandText = "update SISCOM_MI.I_ANT_AUTOPOMPA set BOCCA_COLLEGAMENTO=:bocca " _
                                   & " where ID=" & Me.txtIDAUTOP.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("bocca", Me.cmbBocca.SelectedValue.ToString))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Modifica ATTACCO AUTOPOMPA")

                PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_AUTOPOMPA_PIANI where ID_ANT_AUTOPOMPA= " & Me.txtIDAUTOP.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""


                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then
                        StringaSQL = "insert into SISCOM_MI.I_ANT_AUTOPOMPA_PIANI  (ID_ANT_AUTOPOMPA,COD_LIVELLO_PIANO) values "

                        If Strings.Len(CheckBoxPiano.Items(i).Value) = 1 Then
                            sCodPiano = "0" & CheckBoxPiano.Items(i).Value
                        Else
                            sCodPiano = CheckBoxPiano.Items(i).Value
                        End If

                        StringaSQL = StringaSQL & "(" & Me.txtIDAUTOP.Text & ",'" & sCodPiano & "')"

                        PAR.cmd.CommandText = StringaSQL
                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                    End If
                Next

                If CheckBoxPiano.Items.Count > 0 Then
                    '*** EVENTI_IMPIANTI
                    PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Piani dell'Attacco Autopompa")
                End If

                BindGrid_AutoPompa()

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


    Protected Sub btnAggAUTOP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggAUTOP.Click
        Dim i As Integer
        Try


            Me.txtIDAUTOP.Text = -1
            Me.cmbBocca.SelectedValue = ""

            For i = 0 To CheckBoxPiano.Items.Count - 1
                CheckBoxPiano.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnApriAUTOP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriAUTOP.Click
        Dim i, j As Integer
        Dim sCodPiano As String

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                    For i = 0 To CheckBoxPiano.Items.Count - 1
                        CheckBoxPiano.Items(i).Selected = False
                    Next

                    For i = 0 To CheckBoxPiano.Items.Count - 1
                        For j = 0 To lstPianiAutoPompaSel.Count - 1
                            If Val(lstPianiAutoPompaSel(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxPiano.Items(i).Value = Val(lstPianiAutoPompaSel(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxPiano.Items(i).Selected = True
                                End If
                            End If
                        Next
                    Next

                    Me.txtIDAUTOP.Text = lstAutoPompa(txtIdComponente.Text).ID
                    Me.cmbBocca.SelectedValue = lstAutoPompa(txtIdComponente.Text).BOCCA_COLLEGAMENTO

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

                    PAR.cmd.CommandText = "select SISCOM_MI.I_ANT_AUTOPOMPA.ID,SISCOM_MI.I_ANT_AUTOPOMPA.BOCCA_COLLEGAMENTO," _
                                            & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                            & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                        & " from SISCOM_MI.I_ANT_AUTOPOMPA,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                        & " where SISCOM_MI.I_ANT_AUTOPOMPA.ID = " & txtIdComponente.Text _
                                            & " and SISCOM_MI.I_ANT_AUTOPOMPA.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                            & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ID' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                            & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) "

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDAUTOP.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.cmbBocca.SelectedValue = PAR.IfNull(myReader1("BOCCA_COLLEGAMENTO"), "")

                    End If
                    myReader1.Close()

                    '*** PIANI I_ANT_AUTOPOMPA_PIANI
                    '***Azzero la lista deli piani
                    For i = 0 To CheckBoxPiano.Items.Count - 1
                        CheckBoxPiano.Items(i).Selected = False
                    Next

                    '*** check del piano salvato
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select COD_LIVELLO_PIANO from SISCOM_MI.I_ANT_AUTOPOMPA_PIANI where  ID_ANT_AUTOPOMPA= " & Me.txtIDAUTOP.Text

                    myReader2 = PAR.cmd.ExecuteReader()

                    While myReader2.Read
                        For i = 0 To CheckBoxPiano.Items.Count - 1
                            If Strings.Len(CheckBoxPiano.Items(i).Value) = 1 Then
                                sCodPiano = "0" & CheckBoxPiano.Items(i).Value
                            Else
                                sCodPiano = CheckBoxPiano.Items(i).Value
                            End If

                            If sCodPiano = PAR.IfNull(myReader2("COD_LIVELLO_PIANO"), "-1") Then
                                CheckBoxPiano.Items(i).Selected = True
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

            If vIdImpianto <> -1 Then
                PAR.myTrans.Rollback()
            End If
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnEliminaAUTOP_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaAUTOP.Click
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

                        lstAutoPompa.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.AutoPompa In lstAutoPompa
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridAUTOP.DataSource = lstAutoPompa
                        DataGridAUTOP.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_AUTOPOMPA where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""


                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "ATTACCO AUTOPOMPA")

                            BindGrid_AutoPompa()

                        End If
                    End If
                    txtSelAUTOP.Text = ""
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

            Me.btnAggAUTOP.Visible = False
            Me.btnEliminaAUTOP.Visible = False
            Me.btnApriAUTOP.Visible = False


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


End Class
