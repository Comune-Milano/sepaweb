
Partial Class MANUTENZIONI_Ut_Tab_Millesimali
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            End If

            If Not IsPostBack Then

                RiempiCampi()
                vId = Request.QueryString("ID")
                If vId <> "" And vId <> 0 Then
                    ApriRicerca()
                End If
                Session.Item("LAVORAZIONE") = 1

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Public Property vId() As String
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CStr(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property
    Private Sub RiempiCampi()
        Try

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            ''*********************CARICO COMBO COMPLESSI*********************
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                'cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"
            '*********************COMBO TIPOLOGIA UTENZA**********************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UTENZA"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbTipoUtenze.Items.Add(New ListItem(" ", -1))

            While myReader.Read

                cmbTipoUtenze.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("COD"), -1)))
            End While
            myReader.Close()

            '*********************COMBO FORNITORE**********************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA_FORNITORI"
            myReader = par.cmd.ExecuteReader()
            cmbFornitore.Items.Add(New ListItem(" ", -1))

            While myReader.Read
                cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            myReader.Close()


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaEdifici()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub Apriricerca()
        Dim scriptblock As String

        Try

            If vId <> "" And vId <> 0 Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE

                End If

                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dt As New Data.DataTable
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UTENZE WHERE ID = " & vId & " FOR UPDATE NOWAIT"

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)
                If Request.QueryString("CHIAMA") = "EDIF" Then
                    If Request.QueryString("IDEDIF") <> 0 Then
                        Me.DrLEdificio.SelectedValue = Request.QueryString("IDEDIF")
                    End If
                End If
                If Request.QueryString("IDCOMP") <> 0 Then
                    Me.cmbComplesso.SelectedValue = Request.QueryString("IDCOMP")
                End If

                Me.cmbFornitore.SelectedValue = dt.Rows(0).Item("ID_FORNITORE").ToString
                Me.cmbTipoUtenze.SelectedValue = dt.Rows(0).Item("COD_TIPOLOGIA").ToString
                Me.txtContatore.Text = dt.Rows(0).Item("CONTATORE").ToString
                Me.txtContratto.Text = dt.Rows(0).Item("CONTRATTO").ToString
                Me.txtDescrizione.Text = dt.Rows(0).Item("DESCRIZIONE").ToString

                '*************************RIEMPIO COMBO COMPLESSI IMMOBILIARI A PARTIRE DA EDIFICIO********************
                If Request.QueryString("IDEDIF") <> 0 Then
                    par.cmd.CommandText = "SELECT id_complesso FROM SISCOM_MI.edifici WHERE ID = " & Request.QueryString("IDEDIF")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Me.cmbComplesso.SelectedValue = myReader(0)
                    End If
                    myReader.Close()
                End If
                ''*********************TENGO LA CONNESSIONE APERTA FINO A FINE MODIFICA**********************

                Session.Item("LAVORAZIONE") = "1"

                ''*********************CHIUSURA CONNESSIONE**********************
                'par.OracleConn.Close()
                'par.cmd.Dispose()
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Unità Immobiliare aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                ApriFrmWithDBLock()
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If



        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try

    End Sub
    Private Sub CaricaEdifici()
        Try

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.DrLEdificio.Items.Clear()

            DrLEdificio.Items.Add(New ListItem(" ", -1))
            If Session("PED2_ESTERNA") = "1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID order by denominazione asc"

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()
            DrLEdificio.SelectedValue = "-1"


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub ApriFrmWithDBLock()

        If vId <> "" And vId <> 0 Then

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

            End If

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UTENZE WHERE ID = " & vId & " FOR UPDATE NOWAIT"

            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)
            If Request.QueryString("CHIAMA") = "EDIF" Then
                If Request.QueryString("IDEDIF") <> 0 Then
                    Me.DrLEdificio.SelectedValue = Request.QueryString("IDEDIF")
                End If
            End If
            If Request.QueryString("IDCOMP") <> 0 Then
                Me.cmbComplesso.SelectedValue = Request.QueryString("IDCOMP")
            End If

            Me.cmbFornitore.SelectedValue = dt.Rows(0).Item("ID_FORNITORE").ToString
            Me.cmbTipoUtenze.SelectedValue = dt.Rows(0).Item("COD_TIPOLOGIA").ToString
            Me.txtContatore.Text = dt.Rows(0).Item("CONTATORE").ToString
            Me.txtContratto.Text = dt.Rows(0).Item("CONTRATTO").ToString
            Me.txtDescrizione.Text = dt.Rows(0).Item("DESCRIZIONE").ToString

            '*************************RIEMPIO COMBO COMPLESSI IMMOBILIARI A PARTIRE DA EDIFICIO********************
            If Request.QueryString("IDEDIF") <> 0 Then
                par.cmd.CommandText = "SELECT id_complesso FROM SISCOM_MI.edifici WHERE ID = " & Request.QueryString("IDEDIF")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.cmbComplesso.SelectedValue = myReader(0)
                End If
                myReader.Close()
            End If


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbFornitore.Enabled = False
            Me.cmbTipoUtenze.Enabled = False
            Me.txtContatore.ReadOnly = True
            Me.txtContratto.ReadOnly = True
            Me.txtDescrizione.ReadOnly = True
        End If
    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Session.Add("LAVORAZIONE", "0")
        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove("CONNESSIONE")
        par.cmd.Dispose()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        '*******************RICHIAMO CONNESSIONE RIMASTA APERTA*********************************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        If Me.cmbFornitore.SelectedValue <> "-1" AndAlso Me.cmbTipoUtenze.SelectedValue <> "-1" AndAlso par.IfEmpty(Me.txtContratto.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDescrizione.Text, "Null") <> "Null" Then

            par.cmd.CommandText = "UPDATE SISCOM_MI.UTENZE SET ID_FORNITORE = " & Me.cmbFornitore.SelectedValue.ToString & ", COD_TIPOLOGIA = '" & Me.cmbTipoUtenze.SelectedValue.ToString & "', CONTATORE = '" & par.PulisciStrSql(Me.txtContatore.Text.ToUpper.ToString) & "' , CONTRATTO = '" & par.PulisciStrSql(Me.txtContratto.Text) & "', DESCRIZIONE = '" & par.PulisciStrSql(Me.txtDescrizione.Text.ToUpper.ToString) & "' where utenze.id =" & vId
            par.cmd.ExecuteNonQuery()
            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
        Else
            Response.Write("<SCRIPT>alert('Riempire tutti i campi obbligatori!');</SCRIPT>")

            Exit Sub
        End If

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Session.Item("LAVORAZIONE") = "0"
        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.OracleConn.Close()
        HttpContext.Current.Session.Remove("CONNESSIONE")
        par.cmd.Dispose()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Page.Dispose()
        If Request.QueryString("CHIAMA") = "EDIF" Then
            Response.Redirect("RisultaiUtenzeEdifici.aspx?CHIAMA=" & Request.QueryString("CHIAMA") & "&ID=" & cmbComplesso.SelectedValue.ToString)
        Else
            Response.Redirect("RisultaiUtenzeEdifici.aspx?CHIAMA=" & Request.QueryString("CHIAMA") & "&ID=" & Request.QueryString("IDCOMPCHIAMA") & "&TIPOLOGIA=" & Request.QueryString("TIPOLOGIA") & "&FORNITORE=" & Request.QueryString("FORNITORE") & "&CONT=" & Request.QueryString("CONT") & "&CONTR=" & Request.QueryString("CONTR"))

        End If
    End Sub
End Class
