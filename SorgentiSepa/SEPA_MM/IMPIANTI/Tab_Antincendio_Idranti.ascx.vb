'*** 'TAB ELENCO IDRANTI/NASPI + Verifiche del'IMPIANTO ANTINCENDIO
Imports System.Collections

Partial Class Tab_Antincendio_Idranti
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global
    Dim bVerifica As Boolean

    Dim lstIdranti As System.Collections.Generic.List(Of Epifani.Idranti)
    Dim lstPianiSel As System.Collections.Generic.List(Of Epifani.Scale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstIdranti = CType(HttpContext.Current.Session.Item("LSTIDRANTI"), System.Collections.Generic.List(Of Epifani.Idranti))
        lstPianiSel = CType(HttpContext.Current.Session.Item("LSTPIANIIDRANTI_SEL"), System.Collections.Generic.List(Of Epifani.Scale))

        Try
            If Not IsPostBack Then

                lstIdranti.Clear()
                lstPianiSel.Clear()

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

                BindGrid_Idranti()

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


    'CARICAMENTO GRIGLIA IDRANTI + VERIFICHE 
    Private Sub BindGrid_Idranti()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        '& "DECODE(ES_PRESCRIZIONE,'S','SI','N','NO','P','PARZIALE') AS ""ES_PRESCRIZIONE"" " 

        'NOTA TIPO=ID (ANTINCENDIO VERIFICHE IDRANTI)
        StringaSql = "  select SISCOM_MI.I_ANT_IDRANTI.ID,(select count(*) from SISCOM_MI.I_ANT_IDRANTI_PIANI where  SISCOM_MI.I_ANT_IDRANTI_PIANI.ID_ANT_IDRANTI=SISCOM_MI.I_ANT_IDRANTI.ID) AS ""PIANI""," _
                        & " SISCOM_MI.I_ANT_IDRANTI.DIAMETRO, SISCOM_MI.I_ANT_IDRANTI.NUM_IDRANTI, SISCOM_MI.I_ANT_IDRANTI.LOCALIZZAZIONE, " _
                        & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                        & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                        & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                        & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                        & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                    & " from SISCOM_MI.I_ANT_IDRANTI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                    & " where SISCOM_MI.I_ANT_IDRANTI.ID_IMPIANTO=" & vIdImpianto _
                        & " and SISCOM_MI.I_ANT_IDRANTI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                        & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ID' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                        & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) " _
                    & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "I_ANT_IDRANTI")

        DataGridI.DataSource = ds
        DataGridI.DataBind()

        ds.Dispose()

    End Sub



    'GRID idranti
    Protected Sub DataGridI_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridI.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Idranti_txtSelI').value='Hai selezionato: IDRANTI: " & e.Item.Cells(3).Text & " - DIAMETRO: " & e.Item.Cells(2).Text & "';document.getElementById('Tab_Antincendio_Idranti_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Antincendio_Idranti_txtSelI').value='Hai selezionato: IDRANTI: " & e.Item.Cells(3).Text & " - DIAMETRO: " & e.Item.Cells(2).Text & "';document.getElementById('Tab_Antincendio_Idranti_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

  

    Function ControlloCampi() As Boolean

        Try

            ControlloCampi = True


            ' NUMERO IDRANTI
            If PAR.IfEmpty(Me.txtNumIdranti.Text, "Null") = "Null" Then
                Response.Write("<script>alert('Inserire il numero di idranti!');</script>")
                ControlloCampi = False
                txtNumIdranti.Focus()
                Exit Function
            End If

            'PIANO???

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

    Protected Sub btn_InserisciI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciI.Click
        If ControlloCampi() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDI.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.Salva()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.Update()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelI.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiI.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Me.txtDitta.Enabled = True
        Me.txtNote.Enabled = True
        Me.cmbEsito.Enabled = True

        Me.txtData.Enabled = True
        Me.txtDataScadenza.Enabled = True
        Me.txtValidita.Enabled = True
        Me.cmbPreAllarme.Enabled = True

        Me.btn_InserisciI.Visible = True
        Me.ImageAllarm.Visible = True

        txtSelI.Text = ""
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

                Dim gen As Epifani.Idranti
                RigaPiano = lstIdranti.Count
                ContaPianiServiti = 0

                '***********PIANI SERVITI
                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then

                        Dim genS As Epifani.Scale
                        If Strings.Len(Strings.Trim(Str(CheckBoxPiano.Items(i).Value))) = 1 Then
                            genS = New Epifani.Scale(lstPianiSel.Count, Str(RigaPiano), "0" & Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        Else
                            genS = New Epifani.Scale(lstPianiSel.Count, Str(RigaPiano), Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        End If

                        lstPianiSel.Add(genS)
                        genS = Nothing
                        ContaPianiServiti = ContaPianiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next

                gen = New Epifani.Idranti(lstIdranti.Count, ContaPianiServiti, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtDiametro.Text, 0)), PAR.IfEmpty(Me.txtNumIdranti.Text, 0), PAR.PulisciStringaInvio(Me.txtLocalizzazione.Text, 200), Me.txtIDV.Text, PAR.PulisciStringaInvio(Me.txtDitta.Text, 100), Me.txtData.Text, Me.txtNote.Text, Me.cmbEsito.SelectedItem.Text, PAR.IfEmpty(Me.txtValidita.Text, 0), txtDataScadenza.Text, PAR.IfEmpty(cmbEsito.SelectedItem.Value, 0), PAR.IfEmpty(Me.cmbPreAllarme.SelectedItem.Text, 0), "ID", "")

                DataGridI.DataSource = Nothing
                lstIdranti.Add(gen)
                gen = Nothing

                DataGridI.DataSource = lstIdranti
                DataGridI.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                'INSERISCO IL NUOVO IDRANTE


                PAR.cmd.CommandText = "insert into SISCOM_MI.I_ANT_IDRANTI (ID,ID_IMPIANTO, LOCALIZZAZIONE, NUM_IDRANTI,DIAMETRO) " _
                                & " values (SISCOM_MI.SEQ_I_ANT_IDRANTI.NEXTVAL,:id_impianto,:localizzazione,:num_idranti,:diametro)"

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("localizzazione", PAR.PulisciStringaInvio(Me.txtLocalizzazione.Text, 200)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_idranti", strToNumber(Me.txtNumIdranti.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("diametro", strToNumber(Me.txtDiametro.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()


                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Inserimento IDRANTI/NASPI")


                '**** Ricavo ID dell'IDRANTE
                PAR.cmd.CommandText = " select SISCOM_MI.SEQ_I_ANT_IDRANTI.CURRVAL FROM dual "
                Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderI.Read Then
                    Me.txtIDI.Text = myReaderI(0)
                End If
                myReaderI.Close()
                PAR.cmd.CommandText = ""
                '**********

                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then
                        StringaSQL = "insert into SISCOM_MI.I_ANT_IDRANTI_PIANI  (ID_ANT_IDRANTI,COD_LIVELLO_PIANO) values "


                        If Strings.Len(CheckBoxPiano.Items(i).Value) = 1 Then
                            sCodPiano = "0" & CheckBoxPiano.Items(i).Value
                        Else
                            sCodPiano = CheckBoxPiano.Items(i).Value
                        End If

                        StringaSQL = StringaSQL & "(" & Me.txtIDI.Text & ",'" & sCodPiano & "')"

                        PAR.cmd.CommandText = StringaSQL

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""
                    End If
                Next

                GestioneVerifiche()

                BindGrid_Idranti()
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
                For i = 0 To lstPianiSel.Count - 1
                    If lstPianiSel(i).DENOMINAZIONE_EDIFICIO = Str(txtIdComponente.Text) Then
                        lstPianiSel.RemoveAt(i)
                        GoTo SCALE
                    End If
                Next

                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then
                        Dim genS As Epifani.Scale

                        If Strings.Len(Strings.Trim(Str(CheckBoxPiano.Items(i).Value))) = 1 Then
                            genS = New Epifani.Scale(lstPianiSel.Count, Str(txtIdComponente.Text), "0" & Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        Else
                            genS = New Epifani.Scale(lstPianiSel.Count, Str(txtIdComponente.Text), Strings.Trim(Str(CheckBoxPiano.Items(i).Value)))
                        End If

                        lstPianiSel.Add(genS)
                        genS = Nothing
                        ContaPianiServiti = ContaPianiServiti + 1
                        CType(Me.Page.FindControl("cmbComplesso"), DropDownList).Enabled = False
                    End If
                Next
                '*************************

                lstIdranti(txtIdComponente.Text).DIAMETRO = PAR.PuntiInVirgole(Me.txtDiametro.Text)
                lstIdranti(txtIdComponente.Text).NUM_IDRANTI = Me.txtNumIdranti.Text
                lstIdranti(txtIdComponente.Text).LOCALIZZAZIONE = PAR.PulisciStringaInvio(Me.txtLocalizzazione.Text, 200)

                lstIdranti(txtIdComponente.Text).ID_VERIFICA = txtIDV.Text
                lstIdranti(txtIdComponente.Text).DITTA = PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)
                lstIdranti(txtIdComponente.Text).NOTE = Me.txtNote.Text
                lstIdranti(txtIdComponente.Text).ESITO = PAR.IfEmpty(Me.cmbEsito.SelectedValue, -1)
                lstIdranti(txtIdComponente.Text).ESITO_DETTAGLIO = Me.cmbEsito.SelectedItem.ToString

                lstIdranti(txtIdComponente.Text).DATA = Me.txtData.Text
                lstIdranti(txtIdComponente.Text).MESI_VALIDITA = PAR.IfEmpty(Me.txtValidita.Text, 12)
                lstIdranti(txtIdComponente.Text).MESI_PREALLARME = PAR.IfEmpty(Me.cmbPreAllarme.SelectedValue.ToString, 0)
                lstIdranti(txtIdComponente.Text).DATA_SCADENZA = Me.txtDataScadenza.Text

                lstIdranti(txtIdComponente.Text).TIPO = "ID"
                lstIdranti(txtIdComponente.Text).PIANI = ContaPianiServiti

                DataGridI.DataSource = lstIdranti
                DataGridI.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()


                PAR.cmd.CommandText = "update SISCOM_MI.I_ANT_IDRANTI set LOCALIZZAZIONE=:localizzazione, NUM_IDRANTI=:num_idranti,DIAMETRO=:diametro " _
                                   & " where ID=" & Me.txtIDI.Text


                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("localizzazione", PAR.PulisciStringaInvio(Me.txtLocalizzazione.Text, 200)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("num_idranti", strToNumber(Me.txtNumIdranti.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("diametro", strToNumber(Me.txtDiametro.Text)))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Modifica IDRANTI/NASPI")

                PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_IDRANTI_PIANI where ID_ANT_IDRANTI= " & Me.txtIDI.Text
                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.CommandText = ""


                For i = 0 To CheckBoxPiano.Items.Count - 1
                    If CheckBoxPiano.Items(i).Selected = True And Str(CheckBoxPiano.Items(i).Value) > -1 Then
                        StringaSQL = "insert into SISCOM_MI.I_ANT_IDRANTI_PIANI  (ID_ANT_IDRANTI,COD_LIVELLO_PIANO) values "


                        If Strings.Len(CheckBoxPiano.Items(i).Value) = 1 Then
                            sCodPiano = "0" & CheckBoxPiano.Items(i).Value
                        Else
                            sCodPiano = CheckBoxPiano.Items(i).Value
                        End If

                        StringaSQL = StringaSQL & "(" & Me.txtIDI.Text & ",'" & sCodPiano & "')"

                        PAR.cmd.CommandText = StringaSQL

                        PAR.cmd.ExecuteNonQuery()
                        PAR.cmd.CommandText = ""

                        '*** EVENTI_IMPIANTI
                        PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Piani dell'idrante")

                    End If
                Next

                GestioneVerifiche()

                BindGrid_Idranti()

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


    Protected Sub btnAggI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggI.Click
        Dim i As Integer
        Try


            Me.txtIDI.Text = -1
            Me.txtIDV.Text = -1

            Me.txtDiametro.Text = ""
            Me.txtNumIdranti.Text = ""
            Me.txtLocalizzazione.Text = ""

            Me.txtDitta.Text = ""
            Me.txtNote.Text = ""
            Me.cmbEsito.SelectedValue = ""

            Me.txtData.Text = "" 'Format(Now(), "dd/MM/yyyy")
            Me.txtDataScadenza.Text = ""

            Me.txtValidita.Text = ""
            Me.cmbPreAllarme.SelectedValue = ""
            Me.ImageAllarm.Visible = False
            'CalcolaAllarme()

            For i = 0 To CheckBoxPiano.Items.Count - 1
                CheckBoxPiano.Items(i).Selected = False
            Next

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnApriI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriI.Click
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
                        For j = 0 To lstPianiSel.Count - 1
                            If Val(lstPianiSel(j).DENOMINAZIONE_EDIFICIO) = txtIdComponente.Text Then
                                If CheckBoxPiano.Items(i).Value = Val(lstPianiSel(j).DENOMINAZIONE_SCALA) Then
                                    CheckBoxPiano.Items(i).Selected = True
                                End If
                            End If
                        Next
                    Next

                    Me.txtIDI.Text = lstIdranti(txtIdComponente.Text).ID
                    Me.txtDiametro.Text = PAR.IfNull(lstIdranti(txtIdComponente.Text).DIAMETRO, "")
                    Me.txtNumIdranti.Text = PAR.IfNull(lstIdranti(txtIdComponente.Text).NUM_IDRANTI, "")
                    Me.txtLocalizzazione.Text = PAR.IfNull(lstIdranti(txtIdComponente.Text).LOCALIZZAZIONE, "")

                    Me.txtIDV.Text = lstIdranti(txtIdComponente.Text).ID_VERIFICA
                    Me.txtDitta.Text = PAR.IfNull(lstIdranti(txtIdComponente.Text).DITTA, "")
                    Me.txtNote.Text = PAR.IfNull(lstIdranti(txtIdComponente.Text).NOTE, "")


                    If PAR.IfNull(lstIdranti(txtIdComponente.Text).ESITO_DETTAGLIO, "") <> "" Then
                        Me.cmbEsito.SelectedValue = lstIdranti(txtIdComponente.Text).ESITO
                    Else
                        Me.cmbEsito.SelectedValue = ""
                    End If


                    Me.txtData.Text = PAR.FormattaData(lstIdranti(txtIdComponente.Text).DATA)
                    Me.txtDataScadenza.Text = PAR.FormattaData(lstIdranti(txtIdComponente.Text).DATA_SCADENZA)

                    If PAR.IfNull(lstIdranti(txtIdComponente.Text).MESI_VALIDITA, 0) = 0 Then
                        Me.txtValidita.Text = ""
                    Else
                        Me.txtValidita.Text = PAR.IfNull(lstIdranti(txtIdComponente.Text).MESI_VALIDITA, "")
                    End If

                    If PAR.IfNull(lstIdranti(txtIdComponente.Text).MESI_PREALLARME, 0) = 0 Then
                        Me.cmbPreAllarme.SelectedValue = ""
                    Else
                        Me.cmbPreAllarme.SelectedValue = lstIdranti(txtIdComponente.Text).MESI_PREALLARME
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

                    PAR.cmd.CommandText = "select SISCOM_MI.I_ANT_IDRANTI.ID,SISCOM_MI.I_ANT_IDRANTI.DIAMETRO,SISCOM_MI.I_ANT_IDRANTI.NUM_IDRANTI,SISCOM_MI.I_ANT_IDRANTI.LOCALIZZAZIONE," _
                                            & " SISCOM_MI.IMPIANTI_VERIFICHE.ID AS ""ID_VERIFICA"",SISCOM_MI.IMPIANTI_VERIFICHE.DITTA," _
                                            & " TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                            & "SISCOM_MI.IMPIANTI_VERIFICHE.NOTE,SISCOM_MI.IMPIANTI_VERIFICHE.ESITO_DETTAGLIO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_VALIDITA," _
                                            & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                            & "SISCOM_MI.IMPIANTI_VERIFICHE.ESITO,SISCOM_MI.IMPIANTI_VERIFICHE.MESI_PREALLARME,SISCOM_MI.IMPIANTI_VERIFICHE.TIPO,SISCOM_MI.IMPIANTI_VERIFICHE.ES_PRESCRIZIONE " _
                                        & " from SISCOM_MI.I_ANT_IDRANTI,SISCOM_MI.IMPIANTI_VERIFICHE " _
                                        & " where SISCOM_MI.I_ANT_IDRANTI.ID = " & txtIdComponente.Text _
                                            & " and SISCOM_MI.I_ANT_IDRANTI.ID=SISCOM_MI.IMPIANTI_VERIFICHE.ID_COMPONENTE (+) " _
                                            & " and (SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='ID' or SISCOM_MI.IMPIANTI_VERIFICHE.TIPO is null) " _
                                            & " and (SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N' or SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO is null) "

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then


                        Me.txtIDI.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtDiametro.Text = PAR.IfNull(myReader1("DIAMETRO"), "")
                        Me.txtNumIdranti.Text = PAR.IfNull(myReader1("NUM_IDRANTI"), "")
                        Me.txtLocalizzazione.Text = PAR.IfNull(myReader1("LOCALIZZAZIONE"), "")

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

                    '*** PIANI I_ANT_IDRANTI_PIANI
                    '***Azzero la lista deli piani
                    For i = 0 To CheckBoxPiano.Items.Count - 1
                        CheckBoxPiano.Items(i).Selected = False
                    Next

                    '*** check del piano salvato
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    PAR.cmd.CommandText = "select COD_LIVELLO_PIANO from SISCOM_MI.I_ANT_IDRANTI_PIANI where  ID_ANT_IDRANTI= " & Me.txtIDI.Text

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

    Protected Sub btnEliminaI_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaI.Click
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


                        lstIdranti.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Idranti In lstIdranti
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGridI.DataSource = lstIdranti
                        DataGridI.DataBind()

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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_VERIFICHE where TIPO='ID' and ID_COMPONENTE = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            PAR.cmd.CommandText = "delete from SISCOM_MI.I_ANT_IDRANTI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""


                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, " IDRANTI/NASPI e relativa verifica periodica")

                            BindGrid_Idranti()

                        End If
                    End If
                    txtSelI.Text = ""
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

            Me.btnAggI.Visible = False
            Me.btnEliminaI.Visible = False
            Me.btnApriI.Visible = False


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

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ID"))
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

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_componente", Me.txtIDI.Text))

            PAR.cmd.ExecuteNonQuery()
            PAR.cmd.Parameters.Clear()

            '*** EVENTI_IMPIANTI
            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifica periodica IDRANTI/NASPI")

        Else


            PAR.cmd.CommandText = "update SISCOM_MI.IMPIANTI_VERIFICHE " _
                                            & " set ID_IMPIANTO=:id_impianto, TIPO=:tipo, DITTA=:ditta,DATA=:data,NOTE=:note," _
                                            & "     ESITO=:esito,ES_PRESCRIZIONE=:prescrizione,ESITO_DETTAGLIO=:esitodettaglio," _
                                            & "     MESI_VALIDITA=:validita,MESI_PREALLARME=:preallarme,DATA_SCADENZA=:data_scadenza,FL_STORICO=:fl_storico " _
                                   & " where ID=" & Me.txtIDV.Text

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

            PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "ID"))
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
            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Modifica Verifica periodica dell'IDRANTI/NASPI")
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
