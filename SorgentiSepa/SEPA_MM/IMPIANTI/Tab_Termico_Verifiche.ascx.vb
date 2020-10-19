Imports System.Collections

Partial Class Tabverifiche
    Inherits UserControlSetIdMode

    Dim PAR As New CM.Global


    ' Dim lstVerifiche As System.Collections.Generic.List(Of CM.Verifiche)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Session.Item("OPERATORE") = "" Then
        '    Response.Write("<script>top.location.href=""../LoginCENSIMENTO.aspx""</script>")
        'End If


        ' lstVerifiche = CType(HttpContext.Current.Session.Item("LSTVERIFICHE"), System.Collections.Generic.List(Of CM.Verifiche))

        Try
            If Not IsPostBack Then

                'Dim lstGeneratori As New System.Collections.Generic.List(Of Generatori)

                '      lstVerifiche.Clear()

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

                BindGrid_Verifiche()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

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



    Private Sub BindGrid_Verifiche()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        StringaSql = "select SISCOM_MI.BRUCIATORI.ID,SISCOM_MI.BRUCIATORI.MODELLO," _
                    & "SISCOM_MI.BRUCIATORI.MATRICOLA,SISCOM_MI.BRUCIATORI.NOTE," _
                       & "SISCOM_MI.BRUCIATORI.ANNO_COSTRUZIONE,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO" _
              & " from SISCOM_MI.BRUCIATORI " _
              & " where SISCOM_MI.BRUCIATORI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.BRUCIATORI.MODELLO "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "VERIFICHE")

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()

        ds.Dispose()

    End Sub


    Protected Sub btn_ChiudiVerifica_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiVerifica.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelVerifica.Text = ""
        txtID.Text = ""

    End Sub

    Protected Sub btn_InserisciVerifica_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciVerifica.Click

        If ControlloCampiVerifica() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtID.Text = -1 Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaVerifiche()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateVerifiche()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelVerifica.Text = ""
        txtID.Text = ""


    End Sub


    Private Sub SalvaVerifiche()

        Try
            '    '    If vIdImpianto = -1 Then
            '    '        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

            '    '        'Dim gen As CM.Verifiche

            '    '        'gen = New CM.Verifiche(lstVerifiche.Count, PAR.PulisciStrSql(Me.txtModelloX.Text), PAR.PulisciStrSql(Me.txtMatricolaX.Text), PAR.PulisciStrSql(Me.txtNoteX.Text), Me.txtAnnoRealizzazioneX.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtFunzionamentoX.Text, 0)))

            '    '        'DataGridV.DataSource = Nothing
            '    '        'lstVerifiche.Add(gen)
            '    '        'gen = Nothing

            '    '        'DataGridV.DataSource = lstVerifiche
            '    '        'DataGridV.DataBind()

            '    '    Else
            '    '        '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

            '    '        ' RIPRENDO LA CONNESSIONE
            '    '        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '    '        par.SettaCommand(par)

            '    '        'RIPRENDO LA TRANSAZIONE
            '    '        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '    '        ‘‘par.cmd.Transaction = par.myTrans

            '    '        'PAR.cmd.CommandText = "insert into SISCOM_MI.BRUCIATORI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,CAMPO_FUNZIONAMENTO,NOTE) " _
            '    '        '                    & "values (SISCOM_MI.SEQ_BRUCIATORI.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(Me.txtModelloX.Text) & "',' " _
            '    '        '                        & PAR.PulisciStrSql(Me.txtMatricolaX.Text) & "','" & Me.txtAnnoRealizzazioneX.Text & "'," _
            '    '        '                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamentoX.Text, "Null")) & ",'" & PAR.PulisciStrSql(Me.txtNoteX.Text) & "')"

            '    '        'PAR.cmd.ExecuteNonQuery()

            '    '        BindGrid_Verifiche()
            '    '    End If

            '    '    CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '    '    '' COMMIT
            '    '    'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub UpdateVerifiche()

        Try
            '    If vIdImpianto = -1 Then
            '        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

            '        'lstVerifiche(txtIdVerifica.Text).MODELLO = PAR.PulisciStrSql(Me.txtModelloX.Text)
            '        'lstVerifiche(txtIdVerifica.Text).MATRICOLA = PAR.PulisciStrSql(Me.txtMatricolaX.Text)
            '        'lstVerifiche(txtIdVerifica.Text).NOTE = PAR.PulisciStrSql(Me.txtNoteX.Text)

            '        'lstVerifiche(txtIdVerifica.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneX.Text
            '        'lstVerifiche(txtIdComponente.Text).CAMPO_FUNZIONAMENTO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtFunzionamento.Text, 0))

            '        DataGridV.DataSource = lstVerifiche
            '        DataGridV.DataBind()

            '    Else
            '        '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

            '        ' RIPRENDO LA CONNESSIONE
            '        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '        par.SettaCommand(par)

            '        'RIPRENDO LA TRANSAZIONE
            '        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '        ‘‘par.cmd.Transaction = par.myTrans


            '        'PAR.cmd.CommandText = "update SISCOM_MI.BRUCIATORI set " _
            '        '                            & "MODELLO='" & PAR.PulisciStrSql(Me.txtModelloX.Text) & "'," _
            '        '                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaX.Text) & "'," _
            '        '                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneX.Text & "'," _
            '        '                            & "CAMPO_FUNZIONAMENTO=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamentoX.Text, "Null")) & "," _
            '        '                            & "NOTE='" & PAR.PulisciStrSql(Me.txtNoteX.Text) & "' " _
            '        '                            & " where ID=" & Me.txtIDX.Text

            '        'PAR.cmd.ExecuteNonQuery()

            '        BindGrid_Verifiche()
            '    End If

            '    CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
            '    '' COMMIT
            '    'par.myTrans.Commit()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnApriVerifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriVerifica.Click
        Try

            '    If txtIdVerifica.Text = "" Then
            '        Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            '        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            '        txtAppareV.Text = "0"
            '        'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            '    Else


            '        If vIdImpianto = -1 Then
            '            '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
            '            Me.txtIDX.Text = lstVerifiche(txtIdVerifica.Text).ID
            '            'Me.txtModelloX.Text = PAR.IfNull(lstVerifiche(txtIdVerifica.Text).MODELLO, "")
            '            'Me.txtMatricolaX.Text = PAR.IfNull(lstVerifiche(txtIdVerifica.Text).MATRICOLA, "")
            '            'Me.txtNoteX.Text = PAR.IfNull(lstVerifiche(txtIdVerifica.Text).NOTE, "")

            '            'Me.txtAnnoRealizzazioneX.Text = PAR.IfNull(lstVerifiche(txtIdVerifica.Text).ANNO_COSTRUZIONE, "")
            '            'Me.txtPotenza.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).POTENZA, "")
            '            'Me.txtFluido.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).FLUIDO_TERMOVETTORE, "")
            '            'Me.cmbMarcatura.Items.FindByValue(PAR.IfNull(lstGeneratori(txtIdComponente.Text).MARC_EFF_ENERGETICA, "")).Selected = True

            '        Else
            '            '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

            '            If PAR.OracleConn.State = Data.ConnectionState.Open Then
            '                Response.Write("IMPOSSIBILE VISUALIZZARE")
            '                Exit Sub
            '            Else
            '                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '                par.SettaCommand(par)
            '                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '                ‘‘par.cmd.Transaction = par.myTrans
            '            End If

            '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            '            PAR.cmd.CommandText = "select * from SISCOM_MI.GENERATORI_TERMICI where ID=" & txtIdVerifica.Text

            '            myReader1 = PAR.cmd.ExecuteReader

            '            If myReader1.Read Then
            '                Me.txtIDX.Text = PAR.IfNull(myReader1("ID"), -1)
            '                'Me.txtModelloX.Text = PAR.IfNull(myReader1("MODELLO"), "")
            '                'Me.txtMatricolaX.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
            '                'Me.txtNoteX.Text = PAR.IfNull(myReader1("NOTE"), "")

            '                'Me.txtAnnoRealizzazioneX.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
            '                ''Me.txtPotenza.Text = PAR.IfNull(myReader1("POTENZA"), "")
            '                'Me.txtFluido.Text = PAR.IfNull(myReader1("FLUIDO_TERMOVETTORE"), "")
            '                ''Me.cmbMarcatura.Items.FindByValue(PAR.IfNull(myReader1("MARC_EFF_ENERGETICA"), "")).Selected = True
            '                'Me.cmbMarcatura.SelectedValue = PAR.IfNull(myReader1("MARC_EFF_ENERGETICA"), "")
            '            End If
            '            myReader1.Close()

            '        End If
            '    End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub btnAggVerifiche_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggVerifiche.Click
        'Try

        '    'If vIdImpianto <= 0 Then
        '    '    'If vIdImpianto <= 0 And ControlloCampi() = False Then
        '    '    Response.Write("<script>alert('Salvare l\'impianto prima di procedere con l\'inserimento dei componenti!')</script>")
        '    '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        '    '    txtAppare.Text = "0"
        '    '    txtAppareG.Text = "0"
        '    'Else


        '    Me.txtIDX.Text = -1

        '    'Me.txtModelloX.Text = ""
        '    'Me.txtMatricolaX.Text = ""
        '    'Me.txtNoteX.Text = ""
        '    'Me.txtAnnoRealizzazioneX.Text = ""
        '    'Me.txtFluido.Text = ""
        '    'Me.txtPotenza.Text = ""
        '    'Me.cmbMarcatura.Text = ""
        '    'End If

        'Catch ex As Exception
        '    PAR.OracleConn.Close()

        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        'End Try

    End Sub

    Protected Sub btnEliminaVerifica_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaVerifica.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            '    If txtannulloV.Text = "1" Then

            '        If txtIdVerifica.Text = "" Then
            '            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            '            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            '            txtAppareV.Text = "0"
            '            'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            '        Else

            '            If vIdImpianto = -1 Then
            '                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

            '                lstVerifiche.RemoveAt(txtIdVerifica.Text)

            '                DataGridV.DataSource = lstVerifiche
            '                DataGridV.DataBind()

            '            Else
            '                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

            '                If PAR.OracleConn.State = Data.ConnectionState.Open Then
            '                    Response.Write("IMPOSSIBILE VISUALIZZARE")
            '                    Exit Sub
            '                Else
            '                    '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
            '                    PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '                    par.SettaCommand(par)
            '                    PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '                    ‘‘par.cmd.Transaction = par.myTrans


            '                    PAR.cmd.CommandText = "delete from SISCOM_MI.GENERATORI_TERMICI where ID = " & txtIdVerifica.Text
            '                    PAR.cmd.ExecuteNonQuery()
            '                    PAR.cmd.CommandText = ""

            '                    BindGrid_Verifiche()

            '                End If
            '            End If

            '            txtSelVerifica.Text = ""
            '            txtIdVerifica.Text = ""

            '        End If
            '        CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"

            '    End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            PAR.myTrans.Rollback()
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabVerifiche_txtSelVerifica').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('TabVerifiche_txtId').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabVerifiche_txtSelVerifica').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('TabVerifiche_txtId').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub


    Function ControlloCampiVerifica() As Boolean

        ControlloCampiVerifica = True

        'If Me.cmbTabTipoComp.SelectedValue = -1 Then
        '    Response.Write("<script>alert('Selezionare il tipo di componente!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        'If PAR.IfEmpty(Me.txtModelloX.Text, "Null") = "Null" Then
        '    Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
        '    ControlloCampiVerifica = False
        '    txtModelloX.Focus()
        '    Exit Function
        'End If


        'If Me.txtAnnoRealizzazioneX.Text = "dd/mm/YYYY" Then
        '    Me.txtAnnoRealizzazioneX.Text = ""
        'End If

    End Function




End Class
