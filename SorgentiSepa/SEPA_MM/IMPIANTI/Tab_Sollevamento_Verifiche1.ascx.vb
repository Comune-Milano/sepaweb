'*** TAB SOLLEVAMENTO (VERIFICHE PERIODICHE BIENNALI)
Imports System.Collections

Partial Class Tab_Sollevamento_Verifiche1
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global


    Dim lstVerifiche As System.Collections.Generic.List(Of Epifani.VerificheImpianti)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstVerifiche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE"), System.Collections.Generic.List(Of Epifani.VerificheImpianti))
        Try
            If Not IsPostBack Then
                If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                    lstVerifiche = New System.Collections.Generic.List(Of Epifani.VerificheImpianti)
                End If
                lstVerifiche.Clear()

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

                BindGrid_VerifichePB()
                BindGrid_VerificheSTORICO()

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


    'CARICAMENTO GRIGLIA VERIFICHE PB GRID1
    Private Sub BindGrid_VerifichePB()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        '& "DECODE(ES_PRESCRIZIONE,'S','SI','N','NO','P','PARZIALE') AS ""ES_PRESCRIZIONE"" " 

        'NOTA TIPO=PB (PERIODICHE BIENNALI)
        StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                    & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                    & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PB'" _
                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='N'" _
                    & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "IMPIANTI_VERIFICHE")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        ds.Dispose()

    End Sub

    'CARICAMENTO GRIGLIA STORICO VERIFICHE BIENNALI GRID2
    Private Sub BindGrid_VerificheSTORICO()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        '& "DECODE(ES_PRESCRIZIONE,'S','SI','N','NO','P','PARZIALE') AS ""ES_PRESCRIZIONE"" " 

        'NOTA TIPO=PB (PERIODICHE BIENNALI)
        StringaSql = "  select ID,DITTA,TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA""," _
                                & "NOTE,ESITO_DETTAGLIO,MESI_VALIDITA," _
                                & "TO_CHAR(TO_DATE(SISCOM_MI.IMPIANTI_VERIFICHE.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_SCADENZA""," _
                                & "ESITO,MESI_PREALLARME,TIPO,ES_PRESCRIZIONE " _
                    & " from SISCOM_MI.IMPIANTI_VERIFICHE " _
                    & " where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto _
                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PB'" _
                    & " and SISCOM_MI.IMPIANTI_VERIFICHE.FL_STORICO='S'" _
                    & " order by SISCOM_MI.IMPIANTI_VERIFICHE.ID "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "IMPIANTI_VERIFICHE")

        DataGrid2.DataSource = ds
        DataGrid2.DataBind()

        ds.Dispose()

    End Sub

    'GRID 1
    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Sollevamento_Verifiche1_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Sollevamento_Verifiche1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Sollevamento_Verifiche1_txtSel1').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Sollevamento_Verifiche1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
        End If

    End Sub

    'GRID 2
    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Sollevamento_Verifiche1_txtSel2').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Sollevamento_Verifiche1_txtIdComponente2').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('Tab_Sollevamento_Verifiche1_txtSel2').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('Tab_Sollevamento_Verifiche1_txtIdComponente2').value='" & e.Item.Cells(0).Text & "'")

        End If
    End Sub


    Function ControlloCampiPB() As Boolean
        Dim myReaderTMP1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReaderTMP2 As Oracle.DataAccess.Client.OracleDataReader
        Dim DataStorico As String
        Dim GiorniTrascorsi As Integer

        Try

            ControlloCampiPB = True
            GiorniTrascorsi = 0

            If Me.txtData.Text = "dd/mm/YYYY" Then
                Me.txtData.Text = ""
            End If

            If Me.txtDataScadenza.Text = "dd/mm/YYYY" Then
                Me.txtDataScadenza.Text = ""
            End If


            If PAR.IfEmpty(Me.txtDitta.Text, "Null") = "Null" Then
                Response.Write("<script>alert('Inserire la Ditta!');</script>")
                ControlloCampiPB = False
                txtDitta.Focus()
                Exit Function
            End If

            If PAR.IfEmpty(Me.txtData.Text, "Null") = "Null" Then
                Response.Write("<script>alert('Inserire la Data!');</script>")
                ControlloCampiPB = False
                txtData.Focus()
                Exit Function
            End If


            'If Me.cmbEsito.SelectedValue = -1 Then
            '    Response.Write("<script>alert('Inserire l\'esito!');</script>")
            '    ControlloCampiPB = False
            '    cmbEsito.Focus()
            '    Exit Function
            'End If

            If vIdImpianto > -1 Then
                ' CONTROLLO SULLA DATA (deve essere maggiore o uguale dell'ultima verifica inserita)

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                If Strings.Len(Strings.Trim(txtIdComponente.Text)) > 0 Then
                    PAR.cmd.CommandText = "select max(ID) from SISCOM_MI.IMPIANTI_VERIFICHE where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PB' and SISCOM_MI.IMPIANTI_VERIFICHE.ID<>" & txtIdComponente.Text
                Else
                    PAR.cmd.CommandText = "select max(ID) from SISCOM_MI.IMPIANTI_VERIFICHE where SISCOM_MI.IMPIANTI_VERIFICHE.ID_IMPIANTO = " & vIdImpianto & " and SISCOM_MI.IMPIANTI_VERIFICHE.TIPO='PB' "
                End If

                myReaderTMP1 = PAR.cmd.ExecuteReader()

                If myReaderTMP1.Read Then

                    PAR.cmd.CommandText = "select DATA from SISCOM_MI.IMPIANTI_VERIFICHE where SISCOM_MI.IMPIANTI_VERIFICHE.ID = " & PAR.IfNull(myReaderTMP1(0), -1)
                    myReaderTMP2 = PAR.cmd.ExecuteReader()

                    If myReaderTMP2.Read Then
                        DataStorico = PAR.IfNull(myReaderTMP2("DATA"), "")
                        GiorniTrascorsi = DateDiff(DateInterval.Day, CDate(PAR.FormattaData(Me.txtData.Text)), CDate(PAR.FormattaData(DataStorico)))
                    End If
                    myReaderTMP2.Close()

                End If
                myReaderTMP1.Close()

                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = ""

            End If

            If GiorniTrascorsi > 0 Then
                Response.Write("<script>alert('La data deve essere maggiore o uguale all\'ultima inserita!');</script>")
                ControlloCampiPB = False
                Me.txtData.Focus()
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

    Protected Sub btn_Inserisci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Inserisci.Click

        If ControlloCampiPB() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtIDPB.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaPB()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdatePB()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSel1.Text = ""
        txtIdComponente.Text = ""


    End Sub

    Protected Sub btn_Chiudi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Me.txtDitta.Enabled = True
        Me.txtNote.Enabled = True
        Me.cmbEsitoPB.Enabled = True
        Me.cmbPrescrizionePB.Enabled = True

        Me.txtData.Enabled = True
        Me.txtDataScadenza.Enabled = True
        Me.txtValidita.Enabled = True
        Me.cmbPreAllarme.Enabled = True

        Me.btn_Inserisci.Visible = True
        Me.ImageAllarm.Visible = True

        txtSel1.Text = ""
        txtIdComponente.Text = ""

        txtSel2.Text = ""
        txtIdComponente2.Text = ""

    End Sub


    Private Sub SalvaPB()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.VerificheImpianti

                gen = New Epifani.VerificheImpianti(lstVerifiche.Count, PAR.PulisciStringaInvio(Me.txtDitta.Text, 100), Me.txtData.Text, Me.txtNote.Text, Me.cmbEsitoPB.SelectedItem.Text, PAR.IfEmpty(Me.txtValidita.Text, 0), txtDataScadenza.Text, cmbEsitoPB.SelectedValue, Me.cmbPreAllarme.SelectedItem.Text, "PB", Me.cmbPrescrizionePB.SelectedItem.ToString)

                DataGrid1.DataSource = Nothing
                lstVerifiche.Add(gen)
                gen = Nothing

                DataGrid1.DataSource = lstVerifiche
                DataGrid1.DataBind()
                Me.btnAgg.Visible = False
                Me.btnApri.Visible = True

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.Parameters.Clear()

                ' SETTO TUTTE LE VERIFICHE PRECEDENTI COME STORICO
                PAR.cmd.CommandText = "update SISCOM_MI.IMPIANTI_VERIFICHE set FL_STORICO='S' " _
                                   & " where TIPO='PB' and ID_IMPIANTO=" & vIdImpianto

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()
                PAR.cmd.CommandText = ""

                'INSERISCO LA VERIFICA NUOVA
                PAR.cmd.CommandText = "insert into SISCOM_MI.IMPIANTI_VERIFICHE (ID,ID_IMPIANTO, TIPO, DITTA,DATA,NOTE,ESITO,ES_PRESCRIZIONE,ESITO_DETTAGLIO,MESI_VALIDITA,MESI_PREALLARME,DATA_SCADENZA,FL_STORICO) " _
                                & " values (SISCOM_MI.SEQ_IMPIANTI_VERIFICHE.NEXTVAL,:id_impianto,:tipo,:ditta,:data,:note,:esito,:prescrizione,:esitodettaglio,:validita,:preallarme,:data_scadenza,:fl_storico )"


                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "PB"))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", PAR.AggiustaData(Me.txtData.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNote.Text, 4000)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(Me.cmbEsitoPB.SelectedValue)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", Me.cmbPrescrizionePB.SelectedItem.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", Me.cmbEsitoPB.SelectedItem.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(txtValidita.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(cmbPreAllarme.SelectedValue)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", PAR.AggiustaData(Me.txtDataScadenza.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))

                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Verifiche Periodiche Biennali")

                BindGrid_VerifichePB()
                BindGrid_VerificheSTORICO()
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

    Private Sub UpdatePB()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstVerifiche(txtIdComponente.Text).DITTA = PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)
                lstVerifiche(txtIdComponente.Text).NOTE = Me.txtNote.Text
                lstVerifiche(txtIdComponente.Text).ESITO = Me.cmbEsitoPB.SelectedValue
                lstVerifiche(txtIdComponente.Text).ESITO_DETTAGLIO = Me.cmbEsitoPB.SelectedItem.ToString

                lstVerifiche(txtIdComponente.Text).DATA = Me.txtData.Text
                lstVerifiche(txtIdComponente.Text).MESI_VALIDITA = PAR.IfEmpty(Me.txtValidita.Text, 12)
                lstVerifiche(txtIdComponente.Text).MESI_PREALLARME = Me.cmbPreAllarme.SelectedValue.ToString
                lstVerifiche(txtIdComponente.Text).DATA_SCADENZA = Me.txtDataScadenza.Text

                lstVerifiche(txtIdComponente.Text).TIPO = "PB"

                lstVerifiche(txtIdComponente.Text).ES_PRESCRIZIONE = Me.cmbPrescrizionePB.SelectedItem.ToString

                DataGrid1.DataSource = lstVerifiche
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

                PAR.cmd.CommandText = "update SISCOM_MI.IMPIANTI_VERIFICHE " _
                                            & " set ID_IMPIANTO=:id_impianto, TIPO=:tipo, DITTA=:ditta,DATA=:data,NOTE=:note," _
                                            & "     ESITO=:esito,ES_PRESCRIZIONE=:prescrizione,ESITO_DETTAGLIO=:esitodettaglio," _
                                            & "     MESI_VALIDITA=:validita,MESI_PREALLARME=:preallarme,DATA_SCADENZA=:data_scadenza,FL_STORICO=:fl_storico " _
                                   & " where ID=" & Me.txtIDPB.Text

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", vIdImpianto))

                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo", "PB"))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ditta", PAR.PulisciStringaInvio(Me.txtDitta.Text, 100)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", PAR.AggiustaData(Me.txtData.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", Strings.Left(Me.txtNote.Text, 4000)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esito", Convert.ToInt32(Me.cmbEsitoPB.SelectedValue)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prescrizione", Me.cmbPrescrizionePB.SelectedItem.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("esitodettaglio", Me.cmbEsitoPB.SelectedItem.ToString))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("validita", Convert.ToInt32(txtValidita.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("preallarme", Convert.ToInt32(cmbPreAllarme.SelectedValue)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", PAR.AggiustaData(Me.txtDataScadenza.Text)))
                PAR.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_storico", "N"))


                PAR.cmd.ExecuteNonQuery()
                PAR.cmd.Parameters.Clear()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Verifiche Periodiche Biennali")

                BindGrid_VerifichePB()
                BindGrid_VerificheSTORICO()

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


    Protected Sub btnAgg_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAgg.Click
        Try


            Me.txtIDPB.Text = -1

            Me.txtDitta.Text = ""
            Me.txtNote.Text = ""
            Me.cmbEsitoPB.SelectedValue = "1"
            Me.cmbPrescrizionePB.Text = ""

            Me.txtData.Text = Format(Now(), "dd/MM/yyyy")
            Me.txtValidita.Text = 24
            Me.cmbPreAllarme.SelectedValue = "1"
            CalcolaAllarme()



        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnApri_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApri.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else

                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                    Me.txtIDPB.Text = lstVerifiche(txtIdComponente.Text).ID
                    Me.txtDitta.Text = PAR.IfNull(lstVerifiche(txtIdComponente.Text).DITTA, "")
                    Me.txtNote.Text = PAR.IfNull(lstVerifiche(txtIdComponente.Text).NOTE, "")
                    Me.cmbEsitoPB.SelectedValue = PAR.IfNull(lstVerifiche(txtIdComponente.Text).ESITO, "0")

                    Me.txtData.Text = PAR.FormattaData(lstVerifiche(txtIdComponente.Text).DATA)
                    Me.txtDataScadenza.Text = PAR.FormattaData(lstVerifiche(txtIdComponente.Text).DATA_SCADENZA)

                    Me.txtValidita.Text = PAR.IfNull(lstVerifiche(txtIdComponente.Text).MESI_VALIDITA, "24")
                    Me.cmbPreAllarme.SelectedValue = PAR.IfNull(lstVerifiche(txtIdComponente.Text).MESI_PREALLARME, "1")

                    Me.cmbPrescrizionePB.SelectedValue = PAR.IfNull(lstVerifiche(txtIdComponente.Text).ES_PRESCRIZIONE, "")


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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI_VERIFICHE where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDPB.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.txtDitta.Text = PAR.IfNull(myReader1("DITTA"), "")
                        Me.txtNote.Text = PAR.IfNull(myReader1("NOTE"), "")
                        Me.cmbEsitoPB.SelectedValue = PAR.IfNull(myReader1("ESITO"), "0")

                        Me.txtData.Text = PAR.FormattaData(myReader1("DATA"))
                        Me.txtDataScadenza.Text = PAR.FormattaData(myReader1("DATA_SCADENZA"))
                        Me.txtValidita.Text = PAR.IfNull(myReader1("MESI_VALIDITA"), "24")
                        Me.cmbPreAllarme.SelectedValue = PAR.IfNull(myReader1("MESI_PREALLARME"), "1")
                        'me.cmbPrescrizionePB.Items.FindByText (---).se

                        Me.cmbPrescrizionePB.SelectedValue = PAR.IfNull(myReader1("ES_PRESCRIZIONE"), "")

                    End If
                    myReader1.Close()

                End If

                CalcolaAllarme()


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

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtannullo.Text = "1" Then

                If txtIdComponente2.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppare.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else
                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstVerifiche.RemoveAt(txtIdComponente2.Text)

                        DataGrid1.DataSource = lstVerifiche
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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.IMPIANTI_VERIFICHE where ID = " & txtIdComponente2.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Verifiche Periodiche Biennali")

                            BindGrid_VerifichePB()
                            BindGrid_VerificheSTORICO()

                        End If
                    End If
                    txtSel2.Text = ""
                    txtIdComponente2.Text = ""

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

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Try

            If txtIdComponente2.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto > -1 Then
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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.IMPIANTI_VERIFICHE where ID=" & txtIdComponente2.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtIDPB.Text = PAR.IfNull(myReader1("ID"), -1)

                        Me.txtDitta.Text = PAR.IfNull(myReader1("DITTA"), "")
                        Me.txtNote.Text = PAR.IfNull(myReader1("NOTE"), "")
                        Me.cmbEsitoPB.SelectedValue = PAR.IfNull(myReader1("ESITO"), "0")

                        Me.txtData.Text = PAR.FormattaData(myReader1("DATA"))
                        Me.txtDataScadenza.Text = PAR.FormattaData(myReader1("DATA_SCADENZA"))
                        Me.txtValidita.Text = PAR.IfNull(myReader1("MESI_VALIDITA"), "12")
                        Me.cmbPreAllarme.SelectedValue = PAR.IfNull(myReader1("MESI_PREALLARME"), "1")

                        Me.cmbPrescrizionePB.SelectedValue = PAR.IfNull(myReader1("ES_PRESCRIZIONE"), "")

                    End If
                    myReader1.Close()

                    Me.txtDitta.Enabled = False
                    Me.txtNote.Enabled = False
                    Me.cmbEsitoPB.Enabled = False
                    Me.cmbPrescrizionePB.Enabled = False

                    Me.txtData.Enabled = False
                    Me.txtDataScadenza.Enabled = False
                    Me.txtValidita.Enabled = False
                    Me.cmbPreAllarme.Enabled = False

                    Me.btn_Inserisci.Visible = False
                    Me.ImageAllarm.Visible = False
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


    Private Sub FrmSolaLettura()
        Try

            Me.btnAgg.Visible = False
            Me.btnElimina.Visible = False
            Me.btnApri.Visible = False


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



End Class
