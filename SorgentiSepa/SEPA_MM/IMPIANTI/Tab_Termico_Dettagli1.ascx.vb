Imports System.Collections

Partial Class TabDettagli1
    Inherits UserControlSetIdMode
    Dim PAR As New CM.Global

    Dim lstGeneratori As System.Collections.Generic.List(Of Epifani.Generatori)
    Dim lstBruciatori As System.Collections.Generic.List(Of Epifani.Bruciatori)

    Dim NomeTabella As String

    'Dim lstGeneratori As New ArrayList
    'Shared lstGeneratori As New System.Collections.Generic.List(Of Generatori)
    'Dim Valori1(8) As Object
    'Dim gen As Generatori


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Session.Item("OPERATORE") = "" Then
        '    Response.Write("<script>top.location.href=""../LoginCENSIMENTO.aspx""</script>")
        'End If        

        lstGeneratori = CType(HttpContext.Current.Session.Item("LSTGENERATORI"), System.Collections.Generic.List(Of Epifani.Generatori))
        lstBruciatori = CType(HttpContext.Current.Session.Item("LSTBRUCIATORI"), System.Collections.Generic.List(Of Epifani.Bruciatori))


        Try
            If Not IsPostBack Then

                'Dim lstGeneratori As New System.Collections.Generic.List(Of Generatori)

                If Not String.IsNullOrEmpty(Request.QueryString("ID")) Then
                    lstGeneratori = New System.Collections.Generic.List(Of Epifani.Generatori)
                    lstBruciatori = New System.Collections.Generic.List(Of Epifani.Bruciatori)
                End If

                lstGeneratori.Clear()
                lstBruciatori.Clear()

                vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

                If CType(Me.Page.FindControl("txtTIPO_IMPIANTO"), TextBox).Text = "TR" Then
                    NomeTabella = "SISCOM_MI.I_TER_GENERATORI_TELE"
                Else
                    NomeTabella = "SISCOM_MI.GENERATORI_TERMICI"
                End If

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

                'SettaggioCampi()
                'If Session.Item("IDANA") <> "" Then
                '    vId = Session.Item("IDANA")
                '    '    vId = Request.QueryString("ID")
                '    '    Passato = Request.QueryString("Pas")

                'Else
                '    vId = -1
                'End If

                BindGrid_Bruciatori()
                BindGrid_Generatori()

            End If

            vIdImpianto = CType(Me.Page.FindControl("txtIdImpianto"), TextBox).Text

            If CType(Me.Page.FindControl("txtTIPO_IMPIANTO"), TextBox).Text = "TR" Then
                NomeTabella = "SISCOM_MI.I_TER_GENERATORI_TELE"
            Else
                NomeTabella = "SISCOM_MI.GENERATORI_TERMICI"
            End If

            'If vIdImpianto > 0 Then
            '    BindGrid_Bruciatori()
            '    BindGrid_Generatori()
            'End If

            If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
                FrmSolaLettura()
            End If

        Catch ex As Exception

        End Try

        'lstComponenti.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Conduttore1_lstComponenti');document.getElementById('Tab_Conduttore1_V1').value=obj1.options[obj1.selectedIndex].text;")
        'lstIntestatari.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Conduttore1_lstIntestatari');document.getElementById('Tab_Conduttore1_V2').value=obj1.options[obj1.selectedIndex].text;")
        'lstOspiti.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Conduttore1_lstOspiti');document.getElementById('Tab_Conduttore1_V3').value=obj1.options[obj1.selectedIndex].text;")
    End Sub

    Public Sub Disabilita_Tutto()
        'lstIntestatari.Enabled = False
        'lstComponenti.Enabled = False
        'lstOspiti.Enabled = False

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


    'BRUCIATORI GRID1
    Private Sub BindGrid_Bruciatori()
        Dim StringaSql As String


        'If vIdImpianto > 0 Then

        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans
        'End If

        'If PAR.OracleConn.State = Data.ConnectionState.Closed Then
        '    PAR.OracleConn.Open()
        'End If

        'par.SettaCommand(par)

        'If PAR.OracleConn.State = Data.ConnectionState.Open Then
        '    Response.Write("IMPOSSIBILE VISUALIZZARE")
        '    Exit Sub
        'Else
        '    PAR.OracleConn.Open()
        'End If


        'Select Case Passato
        '  Case "COMP"

        StringaSql = "select SISCOM_MI.BRUCIATORI.ID,SISCOM_MI.BRUCIATORI.MODELLO," _
                    & "SISCOM_MI.BRUCIATORI.MATRICOLA,SISCOM_MI.BRUCIATORI.NOTE," _
                       & "SISCOM_MI.BRUCIATORI.ANNO_COSTRUZIONE,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO,SISCOM_MI.BRUCIATORI.CAMPO_FUNZIONAMENTO_MAX " _
              & " from SISCOM_MI.BRUCIATORI " _
              & " where SISCOM_MI.BRUCIATORI.ID_IMPIANTO = " & vIdImpianto _
              & " order by SISCOM_MI.BRUCIATORI.MODELLO "


        PAR.cmd.CommandText = StringaSql

        'Passo il risultato della select alla DropDownList impostando Indice e Testo associato
        'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSql, PAR.OracleConn)
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)

        Dim ds As New Data.DataSet()
        da.Fill(ds, "BRUCIATORI")

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
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagli1_txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagli1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
            '            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagli1_txtmia').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('TabDettagli1_txtComponente').value='" & e.Item.Cells(2).Text & "';document.getElementById('TabDettagli1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagli1_txtmia').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagli1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagli1_txtmia').value='Hai selezionato: " & e.Item.Cells(1).Text & "';document.getElementById('TabDettagli1_txtComponente').value='" & e.Item.Cells(2).Text & "';document.getElementById('TabDettagli1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiBruciatori() As Boolean

        ControlloCampiBruciatori = True

        'If Me.cmbTabTipoComp.SelectedValue = -1 Then
        '    Response.Write("<script>alert('Selezionare il tipo di componente!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        If PAR.IfEmpty(Me.txtModello.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiBruciatori = False
            txtModello.Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazione.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazione.Text = ""
        End If

    End Function


    Protected Sub btn_InserisciBruciatori_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciBruciatori.Click

        If ControlloCampiBruciatori() = False Then
            txtAppare.Text = "1"
            Exit Sub
        End If

        If Me.txtID.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaBruciatori()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateBruciatori()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        'BindGrid_Bruciatori()

        txtmia.Text = ""
        txtIdComponente.Text = ""


    End Sub

    Protected Sub btn_ChiudiBruciatori_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiBruciatori.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtmia.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Private Sub SalvaBruciatori()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Bruciatori

                gen = New Epifani.Bruciatori(lstBruciatori.Count, PAR.PulisciStringaInvio(Me.txtModello.Text, 200), Me.txtMatricola.Text, Me.txtNote.Text, Me.txtAnnoRealizzazione.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtFunzionamento.Text, 0)), PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtFunzionamentoMax.Text, 0)))

                DataGrid1.DataSource = Nothing
                lstBruciatori.Add(gen)
                gen = Nothing

                DataGrid1.DataSource = lstBruciatori
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "insert into SISCOM_MI.BRUCIATORI (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,CAMPO_FUNZIONAMENTO,CAMPO_FUNZIONAMENTO_MAX,NOTE) " _
                                    & "values (SISCOM_MI.SEQ_BRUCIATORI.NEXTVAL," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModello.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricola.Text) & "','" & Me.txtAnnoRealizzazione.Text & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamento.Text, "Null")) & "," & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamentoMax.Text, "Null")) & ",'" & PAR.PulisciStrSql(Me.txtNote.Text) & "')"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Bruciatori()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Bruciatori")

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

    Private Sub UpdateBruciatori()

        Try
            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstBruciatori(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModello.Text, 200)
                lstBruciatori(txtIdComponente.Text).MATRICOLA = Me.txtMatricola.Text
                lstBruciatori(txtIdComponente.Text).NOTE = Me.txtNote.Text

                lstBruciatori(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazione.Text
                lstBruciatori(txtIdComponente.Text).CAMPO_FUNZIONAMENTO = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtFunzionamento.Text, 0))
                lstBruciatori(txtIdComponente.Text).CAMPO_FUNZIONAMENTO_MAX = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtFunzionamentoMax.Text, 0))

                DataGrid1.DataSource = lstBruciatori
                DataGrid1.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                PAR.cmd.CommandText = "update SISCOM_MI.BRUCIATORI set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModello.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricola.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazione.Text & "'," _
                                            & "CAMPO_FUNZIONAMENTO=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamento.Text, "Null")) & "," _
                                            & "CAMPO_FUNZIONAMENTO_MAX=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtFunzionamentoMax.Text, "Null")) & "," _
                                            & "NOTE='" & PAR.PulisciStrSql(Me.txtNote.Text) & "' " _
                                            & " where ID=" & Me.txtID.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Bruciatori()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Bruciatori")

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

    Protected Sub btnApriBruciatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriBruciatore.Click

        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppare.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else
                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtID.Text = lstBruciatori(txtIdComponente.Text).ID
                    Me.txtModello.Text = PAR.IfNull(lstBruciatori(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricola.Text = PAR.IfNull(lstBruciatori(txtIdComponente.Text).MATRICOLA, "")
                    Me.txtNote.Text = PAR.IfNull(lstBruciatori(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazione.Text = PAR.IfNull(lstBruciatori(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtFunzionamento.Text = PAR.IfNull(lstBruciatori(txtIdComponente.Text).CAMPO_FUNZIONAMENTO, "")
                    Me.txtFunzionamentoMax.Text = PAR.IfNull(lstBruciatori(txtIdComponente.Text).CAMPO_FUNZIONAMENTO_MAX, "")

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

                    PAR.cmd.CommandText = "select * from SISCOM_MI.BRUCIATORI where ID=" & txtIdComponente.Text

                    myReader1 = PAR.cmd.ExecuteReader

                    If myReader1.Read Then
                        Me.txtID.Text = PAR.IfNull(myReader1("ID"), -1)
                        Me.txtModello.Text = PAR.IfNull(myReader1("MODELLO"), "")
                        Me.txtMatricola.Text = PAR.IfNull(myReader1("MATRICOLA"), "")
                        Me.txtNote.Text = PAR.IfNull(myReader1("NOTE"), "")

                        Me.txtAnnoRealizzazione.Text = PAR.IfNull(myReader1("ANNO_COSTRUZIONE"), "")
                        Me.txtFunzionamento.Text = PAR.IfNull(myReader1("CAMPO_FUNZIONAMENTO"), "")
                        Me.txtFunzionamentoMax.Text = PAR.IfNull(myReader1("CAMPO_FUNZIONAMENTO_MAX"), "")
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

    Protected Sub btnEliminaBruciatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaBruciatore.Click
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

                        lstBruciatori.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Bruciatori In lstBruciatori
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid1.DataSource = lstBruciatori
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


                            PAR.cmd.CommandText = "delete from SISCOM_MI.BRUCIATORI where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Bruciatori()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Bruciatori")

                        End If
                    End If
                    txtmia.Text = ""
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

    Protected Sub btnAggBruciatore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggBruciatore.Click
        Try

            'If vIdImpianto <= 0 Then

            '    Response.Write("<script>alert('Salvare l\'impianto prima di procedere con l\'inserimento dei componenti!')</script>")
            '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            '    txtAppare.Text = "0"
            '    txtAppareG.Text = "0"
            'Else


            Me.txtID.Text = -1

            Me.txtModello.Text = ""
            Me.txtMatricola.Text = ""
            Me.txtNote.Text = ""
            Me.txtFunzionamento.Text = ""
            Me.txtFunzionamentoMax.Text = ""
            Me.txtAnnoRealizzazione.Text = ""
            'End If

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

    End Sub



    'GENERATORI GRID2
    Private Sub BindGrid_Generatori()
        Dim StringaSql As String


        '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
        PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        StringaSql = "select " & NomeTabella & ".ID," & NomeTabella & ".MODELLO," _
                                & NomeTabella & ".MATRICOLA," & NomeTabella & ".NOTE," _
                                & NomeTabella & ".ANNO_COSTRUZIONE," & NomeTabella & ".POTENZA," _
                                & NomeTabella & ".MARC_EFF_ENERGETICA," & NomeTabella & ".FLUIDO_TERMOVETTORE " _
              & " from " & NomeTabella _
              & " where " & NomeTabella & ".ID_IMPIANTO = " & vIdImpianto _
              & " order by " & NomeTabella & ".MODELLO "


        PAR.cmd.CommandText = StringaSql

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd)
        Dim ds As New Data.DataSet()

        If NomeTabella = "SISCOM_MI.I_TER_GENERATORI_TELE" Then
            da.Fill(ds, "I_TER_GENERATORI_TELE")
        Else
            da.Fill(ds, "GENERATORI_TERMICI")
        End If


        DataGrid2.DataSource = ds
        DataGrid2.DataBind()

        ds.Dispose()
    End Sub

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagli1_txtSelGeneratori').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagli1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabDettagli1_txtSelGeneratori').value='Hai selezionato: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('TabDettagli1_txtIdComponente').value='" & e.Item.Cells(0).Text & "'")

        End If

    End Sub

    Function ControlloCampiGeneratori() As Boolean

        ControlloCampiGeneratori = True


        If PAR.IfEmpty(Me.txtModelloG.Text, "Null") = "Null" Then
            Response.Write("<script>alert('Inserire la Marca/Modello del componente!');</script>")
            ControlloCampiGeneratori = False
            txtModelloG.Focus()
            Exit Function
        End If


        If Me.txtAnnoRealizzazioneG.Text = "dd/mm/YYYY" Then
            Me.txtAnnoRealizzazioneG.Text = ""
        End If

    End Function


    Protected Sub btn_InserisciGeneratori_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_InserisciGeneratori.Click

        If ControlloCampiGeneratori() = False Then
            txtAppareG.Text = "1"
            Exit Sub
        End If

        If Me.txtIDG.Text = "-1" Then
            'Response.Write("<script>alert('In Inserimento!')</script>")
            Me.SalvaGeneratori()
        Else
            'Response.Write("<script>alert('In Modifica!')</script>")
            Me.UpdateGeneratori()
        End If

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


        txtSelGeneratori.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Protected Sub btn_ChiudiGeneratori_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_ChiudiGeneratori.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        txtSelGeneratori.Text = ""
        txtIdComponente.Text = ""

    End Sub

    Private Sub SalvaGeneratori()
        Dim NomeSEQ As String

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                Dim gen As Epifani.Generatori

                gen = New Epifani.Generatori(lstGeneratori.Count, PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200), Me.txtMatricolaG.Text, Me.txtNoteG.Text, Me.txtAnnoRealizzazioneG.Text, PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenza.Text, 0)), Me.txtFluido.Text, Me.cmbMarcatura.SelectedValue.ToString)

                DataGrid2.DataSource = Nothing
                lstGeneratori.Add(gen)
                gen = Nothing

                DataGrid2.DataSource = lstGeneratori
                DataGrid2.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                If NomeTabella = "SISCOM_MI.I_TER_GENERATORI_TELE" Then
                    NomeSEQ = "SISCOM_MI.SEQ_I_TER_GENERATORI_TELE.NEXTVAL"
                Else
                    NomeSEQ = "SISCOM_MI.SEQ_GENERATORI_TERMICI.NEXTVAL"
                End If

                PAR.cmd.CommandText = "insert into " & NomeTabella & " (ID, ID_IMPIANTO,MODELLO,MATRICOLA,ANNO_COSTRUZIONE,POTENZA,NOTE,FLUIDO_TERMOVETTORE,MARC_EFF_ENERGETICA) " _
                                    & "values (" & NomeSEQ & "," & vIdImpianto & ",'" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200)) & "',' " _
                                        & PAR.PulisciStrSql(Me.txtMatricolaG.Text) & "','" & Me.txtAnnoRealizzazioneG.Text & "'," _
                                        & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenza.Text, "Null")) & ",'" & PAR.PulisciStrSql(Me.txtNoteG.Text) & "','" _
                                        & PAR.PulisciStrSql(Me.txtFluido.Text) & "','" & Me.cmbMarcatura.SelectedValue.ToString & "')"

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Generatori()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLIO_IMPIANTO, "Generatori")

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

    Private Sub UpdateGeneratori()

        Try

            If vIdImpianto = -1 Then
                '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                lstGeneratori(txtIdComponente.Text).MODELLO = PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200)
                lstGeneratori(txtIdComponente.Text).MATRICOLA = Me.txtMatricolaG.Text
                lstGeneratori(txtIdComponente.Text).NOTE = Me.txtNoteG.Text

                lstGeneratori(txtIdComponente.Text).ANNO_COSTRUZIONE = Me.txtAnnoRealizzazioneG.Text
                lstGeneratori(txtIdComponente.Text).POTENZA = PAR.PuntiInVirgole(PAR.IfEmpty(Me.txtPotenza.Text, 0))
                lstGeneratori(txtIdComponente.Text).FLUIDO_TERMOVETTORE = Me.txtFluido.Text
                lstGeneratori(txtIdComponente.Text).MARC_EFF_ENERGETICA = Me.cmbMarcatura.SelectedValue.ToString

                DataGrid2.DataSource = lstGeneratori
                DataGrid2.DataBind()

            Else
                '*** DOPO AVER ESEGUITO IL SALVA GENERALE DELLA SCHEDA IMPIANTO

                ' RIPRENDO LA CONNESSIONE
                PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                PAR.cmd.CommandText = "update " & NomeTabella & " set " _
                                            & "MODELLO='" & PAR.PulisciStrSql(PAR.PulisciStringaInvio(Me.txtModelloG.Text, 200)) & "'," _
                                            & "MATRICOLA='" & PAR.PulisciStrSql(Me.txtMatricolaG.Text) & "'," _
                                            & "ANNO_COSTRUZIONE='" & Me.txtAnnoRealizzazioneG.Text & "'," _
                                            & "POTENZA=" & PAR.VirgoleInPunti(PAR.IfEmpty(Me.txtPotenza.Text, "Null")) & "," _
                                            & "NOTE='" & PAR.PulisciStrSql(Me.txtNoteG.Text) & "', " _
                                            & "FLUIDO_TERMOVETTORE='" & PAR.PulisciStrSql(Me.txtFluido.Text) & "', " _
                                            & "MARC_EFF_ENERGETICA='" & Me.cmbMarcatura.SelectedValue.ToString & "' " _
                                            & " where ID=" & Me.txtIDG.Text

                PAR.cmd.ExecuteNonQuery()

                BindGrid_Generatori()

                '*** EVENTI_IMPIANTI
                PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_DETTAGLIO_IMPIANTO, "Generatori")

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

    Protected Sub btnApriGeneratore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnApriGeneratore.Click
        Try

            If txtIdComponente.Text = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtAppareG.Text = "0"
                'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
            Else


                If vIdImpianto = -1 Then
                    '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO
                    Me.txtIDG.Text = lstGeneratori(txtIdComponente.Text).ID
                    Me.txtModelloG.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).MODELLO, "")
                    Me.txtMatricolaG.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).MATRICOLA, "")
                    Me.txtNoteG.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).NOTE, "")

                    Me.txtAnnoRealizzazioneG.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).ANNO_COSTRUZIONE, "")
                    Me.txtPotenza.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).POTENZA, "")
                    Me.txtFluido.Text = PAR.IfNull(lstGeneratori(txtIdComponente.Text).FLUIDO_TERMOVETTORE, "")
                    'Me.cmbMarcatura.Items.FindByValue(PAR.IfNull(lstGeneratori(txtIdComponente.Text).MARC_EFF_ENERGETICA, "")).Selected = True
                    Me.cmbMarcatura.SelectedValue = PAR.IfNull(lstGeneratori(txtIdComponente.Text).MARC_EFF_ENERGETICA, "")

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

                    PAR.cmd.CommandText = "select * from " & NomeTabella & " where ID=" & txtIdComponente.Text

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

    Protected Sub btnEliminaGeneratore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEliminaGeneratore.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try
            If txtannullo.Text = "1" Then

                If txtIdComponente.Text = "" Then
                    Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    txtAppareG.Text = "0"
                    'document.getElementById('TabDettagli1_txtAppare').value='1';document.getElementById('USCITA').value='1'; document.getElementById('InserimentoComponenti').style.visibility='visible';
                Else

                    If vIdImpianto = -1 Then
                        '*** QUANDO MI TROVO NEL PRIMO INSERIMENTO, SENZA AVER ESEGUITO ANCORA IL SALVA SCHEDA IMPIANTO

                        lstGeneratori.RemoveAt(txtIdComponente.Text)

                        Dim indice As Integer = 0
                        For Each griglia As Epifani.Generatori In lstGeneratori
                            griglia.ID = indice
                            indice += 1
                        Next

                        DataGrid2.DataSource = lstGeneratori
                        DataGrid2.DataBind()

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


                            PAR.cmd.CommandText = "delete from " & NomeTabella & " where ID = " & txtIdComponente.Text
                            PAR.cmd.ExecuteNonQuery()
                            PAR.cmd.CommandText = ""

                            BindGrid_Generatori()

                            '*** EVENTI_IMPIANTI
                            PAR.InserisciEvento(PAR.cmd, vIdImpianto, Session.Item("ID_OPERATORE"), Epifani.TabEventi.CANCELLAZIONE_DETTAGLIO_IMPIANTO, "Generatori")

                        End If
                    End If

                    txtSelGeneratori.Text = ""
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

    Protected Sub btnAggGeneratore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAggGeneratore.Click
        Try

            'If vIdImpianto <= 0 Then
            '    'If vIdImpianto <= 0 And ControlloCampi() = False Then
            '    Response.Write("<script>alert('Salvare l\'impianto prima di procedere con l\'inserimento dei componenti!')</script>")
            '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            '    txtAppare.Text = "0"
            '    txtAppareG.Text = "0"
            'Else


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

            Me.btnAggGeneratore.Visible = False
            Me.btnEliminaGeneratore.Visible = False
            Me.btnApriGeneratore.Visible = False

            Me.btnAggBruciatore.Visible = False
            Me.btnEliminaBruciatore.Visible = False
            Me.btnApriBruciatore.Visible = False


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