
Partial Class MANUTENZIONI_RicercaInterventi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            vId = 0
            TipoImmobile = Session.Item("BUILDING_TYPE")
            vId = Session.Item("ID")
            Me.cmbCondominio.Items.Add(New ListItem(" ", -1))
            Me.cmbCondominio.Items.Add(New ListItem("SI", 0))
            Me.cmbCondominio.Items.Add(New ListItem("NO", 1))
            RiempiCampi()
            'CaricaIndirizzi()
            Select Case TipoImmobile
                Case "COMP"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.cmbEdificio.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.LblUniCom.Enabled = False
                    Me.LblUniImmob.Enabled = False
                    Me.LblEdificio.Enabled = False
                    Me.lblImpianti.Enabled = False
                    Me.cmbImpianti.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Int. Manutenzione su COMPLESSI"
                Case "EDIF"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.LblUniCom.Enabled = False
                    Me.LblUniImmob.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Int. Manutenzione su EDIFICI"
                    Me.lblImpianti.Enabled = False
                    Me.cmbImpianti.Enabled = False

                Case "UNI_COM"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.LblUniImmob.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Int. Manutenzione su UNITA' COMUNI"
                    Me.lblImpianti.Enabled = False
                    Me.cmbImpianti.Enabled = False

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False
                Case "UNI_IMMOB"
                    Me.cmbUnitaComune.Enabled = False
                    Me.LblUniCom.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Int. Manutenzione su UNITA' IMMOBILIARI"
                    Me.lblImpianti.Enabled = False
                    Me.cmbImpianti.Enabled = False

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False
                Case "IMPIANTI"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.LblUniImmob.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.LblUniCom.Enabled = False

                    Me.LblRicerca.Text = "Ricerca Int. Manutenzione su IMPIANTI"


            End Select

        End If


    End Sub
    Private Property TipoImmobile() As String
        Get
            If Not (ViewState("pa_tipoimmob") Is Nothing) Then
                Return CStr(ViewState("pa_tipoimmob"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("pa_tipoimmob") = value
        End Set

    End Property
    Private Property sStringasql() As String
        Get
            If Not (ViewState("par_sstrsql") Is Nothing) Then
                Return CStr(ViewState("par_sstrsql"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sstrsql") = value
        End Set

    End Property
    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property

    Private Sub RiempiCampi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            End If
            'CARICO COMBO COMPLESSI
            Dim gest As Integer
            gest = 0


            If gest > 0 Then
                par.cmd.CommandText = "SELECT distinct id,denominazione FROM SISCOM_MI.complessi_immobiliari  where substr(id,1,1)= " & gest & "order by denominazione asc"

            Else
                If Me.cmbCondominio.SelectedValue = 1 Then
                    par.cmd.CommandText = "SELECT distinct COMPLESSI_IMMOBILIARI.id,COD_COMPLESSO,COMPLESSI_IMMOBILIARI.denominazione " _
                                        & "FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.EDIFICI WHERE  COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO " _
                                        & "AND EDIFICI.CONDOMINIO = 1 order by denominazione asc"
                ElseIf Me.cmbCondominio.SelectedValue = 0 Then

                    par.cmd.CommandText = "SELECT distinct COMPLESSI_IMMOBILIARI.id,COD_COMPLESSO,COMPLESSI_IMMOBILIARI.denominazione " _
                                        & "FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.EDIFICI WHERE  COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO " _
                                        & "AND EDIFICI.CONDOMINIO = 0 order by denominazione asc"

                ElseIf Me.cmbCondominio.SelectedValue = -1 Then
                    par.cmd.CommandText = "SELECT distinct COMPLESSI_IMMOBILIARI.id,COD_COMPLESSO,COMPLESSI_IMMOBILIARI.denominazione " _
                                        & "FROM SISCOM_MI.complessi_immobiliari order by denominazione asc"
                End If
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                'cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_SERVIZI_MANU"
            myReader1 = par.cmd.ExecuteReader
            cmbTipoServizio.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoServizio.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            cmbTipoServizio.Text = "-1"
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaEdifici()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub CaricaEdifici()
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim gest As Integer = 0
        Try


            Me.cmbEdificio.Items.Clear()

            cmbEdificio.Items.Add(New ListItem(" ", -1))


            If gest <> 0 Then
                par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici where substr(id,1,1)= " & gest & " order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici WHERE EDIFICI.CONDOMINIO = " & Me.cmbCondominio.SelectedValue.ToString & " order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While


            myReader1.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            cmbEdificio.SelectedValue = "-1"
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub CaricaUnitaImmob()
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim gest As Integer = 0
        Try

            Me.cmbUnitaImmob.Items.Clear()
            Me.cmbUnitaImmob.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "  Select id, 'COD.'||COD_UNITA_IMMOBILIARE||' - SCALA '||SCALA ||' - INTERNO '||INTERNO as DESCRIZIONE from SISCOM_MI.Unita_immobiliari "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbUnitaImmob.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.cmbUnitaImmob.SelectedValue = "-1"

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub CaricaUnitaComuni()
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim gest As Integer = 0
        Try

            Me.cmbUnitaComune.Items.Clear()
            Me.cmbUnitaComune.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "  Select id, 'COD. '||COD_UNITA_COMUNE||' - '||LOCALIZZAZIONE as DESCRIZIONE from SISCOM_MI.UNITA_COMUNI "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbUnitaComune.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.cmbUnitaComune.SelectedValue = "-1"

        Catch ex As Exception

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.SelectedValue <> "-1" Then
            FiltraEdifici()
            FiltraUnitaComuni()
            FiltraImpianti()
        Else
            Me.cmbUnitaComune.Items.Clear()
            CaricaEdifici()
            Me.cmbImpianti.Items.Clear()
        End If

    End Sub
    Private Sub FiltraUnitaComuni()
        If Me.cmbComplesso.SelectedValue <> "-1" Or Me.cmbEdificio.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Try
                Me.cmbUnitaComune.Items.Clear()
                Me.cmbUnitaComune.Items.Add(New ListItem(" ", -1))
                If Me.cmbEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "  Select id, 'COD. '||COD_UNITA_COMUNE||' - '||LOCALIZZAZIONE as DESCRIZIONE from SISCOM_MI.UNITA_COMUNI where id_edificio = " & Me.cmbEdificio.SelectedValue.ToString
                ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "  Select id, 'COD. '||COD_UNITA_COMUNE||' - '||LOCALIZZAZIONE as DESCRIZIONE from SISCOM_MI.UNITA_COMUNI where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString

                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbUnitaComune.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.cmbUnitaImmob.SelectedValue = "-1"

            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()

            End Try
        End If
    End Sub
    Private Sub Filtrainterventi()
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
        End If
        Try
            Me.cmbTipoIntervento.Items.Clear()
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU where ID_TIPO_SERVIZIO = " & Me.cmbTipoServizio.SelectedValue.ToString
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1 = par.cmd.ExecuteReader
            cmbTipoIntervento.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoIntervento.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            cmbTipoIntervento.Text = "-1"
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub FiltraUnitaImmob()
        If Me.cmbEdificio.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Try
                Me.cmbUnitaImmob.Items.Clear()
                Me.cmbUnitaImmob.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "  Select id, 'COD.'||COD_UNITA_IMMOBILIARE||' - SCALA '||SCALA ||' - INTERNO '||INTERNO as DESCRIZIONE from SISCOM_MI.Unita_immobiliari where id_edificio = " & Me.cmbEdificio.SelectedValue.ToCharArray
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbUnitaImmob.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Me.cmbUnitaImmob.SelectedValue = "-1"

            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()
            End Try
        End If
    End Sub

    Private Sub FiltraEdifici()
        If Me.cmbComplesso.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbEdificio.Items.Clear()
            cmbEdificio.Items.Add(New ListItem(" ", -1))
            Try
                If gest <> 0 Then
                    par.cmd.CommandText = "SELECT distinct id,denominazione FROM SISCOM_MI.edifici where substr(id,1,1)= " & gest & " order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct id,denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"
                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()

            End Try
        End If
    End Sub
    Private Sub FiltraImpianti()
        Try


            If Me.cmbEdificio.SelectedValue = "-1" And Me.cmbComplesso.SelectedValue <> "-1" Then
                '****************************FILTRO IMPIANTI A PARTIRE DA ID COMPLESSO
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.cmbImpianti.Items.Clear()
                Me.cmbImpianti.Items.Add(New ListItem("", -1))

                par.cmd.CommandText = "SELECT DISTINCT IMPIANTI.ID, IMPIANTI.COD_IMPIANTO, IMPIANTI.DESCRIZIONE, TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA AND ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbImpianti.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " ") & " - " & par.IfNull(myReader1("TIPOLOGIA"), "") & " - cod. " & par.IfNull(myReader1("COD_IMPIANTO"), ""), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            ElseIf Me.cmbEdificio.SelectedValue <> "-1" Then
                '****************************FILTRO IMPIANTI A PARTIRE DA ID EDIFICIO
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.cmbImpianti.Items.Clear()
                Me.cmbImpianti.Items.Add(New ListItem("", -1))

                par.cmd.CommandText = "SELECT DISTINCT IMPIANTI.ID, IMPIANTI.COD_IMPIANTO, IMPIANTI.DESCRIZIONE, TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA AND ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue.ToString
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbImpianti.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " ") & " - " & par.IfNull(myReader1("TIPOLOGIA"), "") & " - cod. " & par.IfNull(myReader1("COD_IMPIANTO"), ""), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()

        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.SelectedValue <> "-1" Then
            FiltraUnitaComuni()
            FiltraUnitaImmob()
            FiltraImpianti()
        Else
            Me.cmbUnitaImmob.Items.Clear()
            If Me.cmbComplesso.SelectedValue = "-1" Then
                Me.cmbUnitaComune.Items.Clear()
            Else
                FiltraUnitaComuni()
            End If
            FiltraImpianti()
        End If

    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try

            Select Case TipoImmobile
                Case "COMP"
                    sStringasql = ""
                    RicercaPerComplessi()
                Case "EDIF"
                    sStringasql = ""
                    RicercaPerEdifici()
                Case "UNI_COM"
                    sStringasql = ""
                    RicercaPerUnComuni()
                Case "UNI_IMMOB"
                    sStringasql = ""
                    RicercaPerUnImmobiliari()
                Case "IMPIANTI"
                    sStringasql = ""
                    RicercaPerImpianti()
            End Select
            Session.Add("PED", sStringasql)
            Response.Redirect("RisultatiInterventi.aspx")


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home_Interventi.aspx""</script>")
    End Sub

    Protected Sub cmbCondominio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCondominio.SelectedIndexChanged
        Me.cmbComplesso.Items.Clear()
        Me.cmbEdificio.Items.Clear()
        RiempiCampi()
    End Sub
    Protected Sub cmbTipoServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoServizio.SelectedIndexChanged
        Me.Filtrainterventi()
    End Sub


    Private Sub RicercaPerComplessi()
        Try

            Dim bTrovato As Boolean = True

            If Me.cmbComplesso.SelectedValue <> -1 Then

                If bTrovato = True Then sStringasql = sStringasql & " AND "

                sStringasql = sStringasql & " COMPLESSI_IMMOBILIARI.Id =" & Me.cmbComplesso.SelectedValue.ToString & ""
            End If
            If Me.cmbTipoServizio.SelectedValue <> -1 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "

                bTrovato = True
                sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

            End If

            If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "

                bTrovato = True
                sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""
            End If


            sStringasql = "SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID, COMPLESSI_IMMOBILIARI.cod_complesso AS COD_IMMOBILE,COMPLESSI_IMMOBILIARI.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE   COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_COMPLESSO AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO" & sStringasql

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            'par.OracleConn.Close()

        End Try
    End Sub


    Private Sub RicercaPerEdifici()
        Try
            Dim bTrovato As Boolean = True

            If Me.cmbEdificio.SelectedValue <> -1 Then

                bTrovato = True
                sStringasql = sStringasql & "EDIFICI.Id= " & Me.cmbEdificio.SelectedValue.ToString & ""
            End If


            If Me.cmbComplesso.SelectedValue <> -1 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "

                bTrovato = True
                sStringasql = sStringasql & "EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & ""
            End If

            If Me.cmbTipoServizio.SelectedValue <> -1 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "

                bTrovato = True

                sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

            End If

            If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "

                bTrovato = True
                sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""
            End If

            sStringasql = "SELECT ROWNUM,Interventi_manutenzione.id,edifici.cod_edificio AS COD_IMMOBILE, EDIFICI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE ,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE  EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.edifici.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.id_edificio AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO  " & sStringasql


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            'par.OracleConn.Close()

        End Try
    End Sub


    Private Sub RicercaPerUnComuni()
        Try
            Dim bTrovato As Boolean = True
            '*****ASSOCIATA A COMPLESSO
            If Me.cmbComplesso.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue = "-1" Then

                If Me.cmbUnitaComune.SelectedValue <> "-1" Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "
                    sStringasql = sStringasql & "UNITA_COMUNI.ID=" & Me.cmbUnitaComune.SelectedValue.ToString & ""
                End If



                If Me.cmbTipoServizio.SelectedValue <> -1 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    bTrovato = True

                    sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    bTrovato = True
                    sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""
                End If

                sStringasql = "SELECT ROWNUM,INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  to_char(to_date(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE COMPLESSI_IMMOBILIARI.ID =" & Me.cmbComplesso.SelectedValue & "  AND COMPLESSI_IMMOBILIARI.ID= UNITA_COMUNI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO  " & sStringasql
            End If

            '*****ASSOCIATA AD EDIFICIO
            If Me.cmbEdificio.SelectedValue <> "-1" Then

                If Me.cmbUnitaComune.SelectedValue <> "-1" Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "
                    sStringasql = sStringasql & "UNITA_COMUNI.ID=" & Me.cmbUnitaComune.SelectedValue.ToString & ""
                End If

                If Me.cmbTipoServizio.SelectedValue <> -1 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "


                    sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "


                    sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""

                End If

                sStringasql = "SELECT ROWNUM,INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  to_char(to_date(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID =" & Me.cmbEdificio.SelectedValue & "  AND EDIFICI.ID= UNITA_COMUNI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO  " & sStringasql


            End If
            '*****LIBERA 
            If Me.cmbComplesso.SelectedValue = "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then

                If Me.cmbTipoServizio.SelectedValue <> -1 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "


                    sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "


                    sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""

                End If


                sStringasql = "SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE, SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE COMPLESSI_IMMOBILIARI.ID= UNITA_COMUNI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND UNITA_COMUNI.id_complesso IS NOT NULL AND UNITA_COMUNI.id_edificio IS NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO " & sStringasql & " " _
                & "UNION SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID= UNITA_COMUNI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND UNITA_COMUNI.id_complesso IS NULL AND UNITA_COMUNI.id_edificio IS NOT NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO " & sStringasql


            End If


            '    'Response.Write("<script>alert('Nessun criterio valido per la ricerca su UNITA COMUNI!')</script>")
            '    'Exit Sub
            '    sStringasql = "SELECT  ROWNUM,INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO " _
            '    & "FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE COMPLESSI_IMMOBILIARI.ID= UNITA_COMUNI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND UNITA_COMUNI.id_complesso IS NOT NULL " _
            '    & "AND UNITA_COMUNI.id_edificio IS NULL UNION SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO " _
            '    & "FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE EDIFICI.ID= UNITA_COMUNI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_COMUNE AND UNITA_COMUNI.id_complesso IS NULL AND UNITA_COMUNI.id_edificio IS NOT NULL"


            'End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            'par.OracleConn.Close()

        End Try
    End Sub


    Private Sub RicercaPerUnImmobiliari()
        Try
            Dim bTrovato As Boolean = True

            If Me.cmbUnitaImmob.SelectedValue <> "-1" And Me.cmbUnitaImmob.SelectedValue <> "" Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "

                sStringasql = sStringasql & "UNITA_IMMOBILIARI.Id=" & Me.cmbUnitaImmob.SelectedValue.ToString & ""

            End If

            If Me.cmbEdificio.SelectedValue <> "-1" Then

                If bTrovato = True Then sStringasql = sStringasql & " AND "

                sStringasql = sStringasql & "UNITA_IMMOBILIARI.ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue.ToString & ""

            End If

            If Me.cmbComplesso.SelectedValue <> "-1" Then

                If bTrovato = True Then sStringasql = sStringasql & " AND "

                sStringasql = sStringasql & "EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & ""

            End If

            If Me.cmbTipoServizio.SelectedValue <> -1 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "


                sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

            End If

            If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                If bTrovato = True Then sStringasql = sStringasql & " AND "


                sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""

            End If
            sStringasql = "SELECT ROWNUM,INTERVENTI_MANUTENZIONE.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_IMMOBILE, ('SCALA - '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA||'INTERNO - '||UNITA_IMMOBILIARI.INTERNO)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE, SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID= UNITA_IMMOBILIARI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_UNITA_IMMOBILIARE AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO " & sStringasql


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub


    Private Sub RicercaPerImpianti()
        Try
            Dim bTrovato As Boolean = True
            '*****ASSOCIATA A COMPLESSO
            If Me.cmbComplesso.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue = "-1" Then

                If Me.cmbImpianti.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue = "-1" Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    sStringasql = sStringasql & "IMPIANTI.ID=" & Me.cmbImpianti.SelectedValue.ToString & ""
                End If



                If Me.cmbTipoServizio.SelectedValue <> -1 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "


                    sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""
                End If

                sStringasql = "SELECT ROWNUM,INTERVENTI_MANUTENZIONE.ID,IMPIANTI.COD_IMPIANTO AS COD_IMMOBILE, ('TIPO - '||TIPOLOGIA_IMPIANTI.DESCRIZIONE||'DESCRIZIONE - '||IMPIANTI.DESCRIZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  to_char(to_date(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.TIPOLOGIA_IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.IMPIANTI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE COMPLESSI_IMMOBILIARI.ID =" & Me.cmbComplesso.SelectedValue & "  AND COMPLESSI_IMMOBILIARI.ID= IMPIANTI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.IMPIANTI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " & sStringasql
            End If

            '*****ASSOCIATA AD EDIFICIO
            If Me.cmbEdificio.SelectedValue <> "-1" Then

                If Me.cmbImpianti.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue = "-1" Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    sStringasql = sStringasql & "IMPIANTI.ID=" & Me.cmbImpianti.SelectedValue.ToString & ""
                End If

                If Me.cmbTipoServizio.SelectedValue <> -1 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    bTrovato = True

                    sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""

                End If

                sStringasql = "SELECT ROWNUM,INTERVENTI_MANUTENZIONE.ID,IMPIANTI.COD_IMPIANTO AS COD_IMMOBILE, ('TIPO - '||TIPOLOGIA_IMPIANTI.DESCRIZIONE||'DESCRIZIONE - '||IMPIANTI.DESCRIZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  to_char(to_date(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.EDIFICI, SISCOM_MI.IMPIANTI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID =" & Me.cmbEdificio.SelectedValue & "  AND EDIFICI.ID= IMPIANTI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.IMPIANTI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA  " & sStringasql


            End If
            '*****LIBERA 
            If Me.cmbComplesso.SelectedValue = "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then

                If Me.cmbTipoServizio.SelectedValue <> -1 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "


                    sStringasql = sStringasql & " INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO= " & Me.cmbTipoServizio.SelectedValue & ""

                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbTipoIntervento.Items.Count <> 0 Then
                    If bTrovato = True Then sStringasql = sStringasql & " AND "

                    bTrovato = True

                    sStringasql = sStringasql & "INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO = " & Me.cmbTipoIntervento.SelectedValue & ""

                End If


                sStringasql = "SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,IMPIANTI.COD_IMPIANTO AS COD_IMMOBILE, ('TIPO - '||TIPOLOGIA_IMPIANTI.DESCRIZIONE||'DESCRIZIONE - '||IMPIANTI.DESCRIZIONE)AS  DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.TIPOLOGIA_IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.IMPIANTI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE, SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE COMPLESSI_IMMOBILIARI.ID= IMPIANTI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.IMPIANTI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO AND IMPIANTI.ID_COMPLESSO IS NOT NULL AND IMPIANTI.ID_EDIFICIO IS NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " & sStringasql & " " _
                & "UNION SELECT ROWNUM, INTERVENTI_MANUTENZIONE.ID,IMPIANTI.COD_IMPIANTO AS COD_IMMOBILE, ('TIPO - '||TIPOLOGIA_IMPIANTI.DESCRIZIONE||'DESCRIZIONE - '||IMPIANTI.DESCRIZIONE)AS  DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO,  TO_CHAR(TO_DATE(INTERVENTI_MANUTENZIONE.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,INTERVENTI_MANUTENZIONE.DESCRIZIONE AS DESCRIZIONE_INTERVENTO, TIPOLOGIA_SERVIZI_MANU.DESCRIZIONE AS TIPO_SERVIZIO, TIPOLOGIA_INTERVENTI_MANU.DESCRIZIONE AS TIPO_INTERVENTO FROM SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.EDIFICI, SISCOM_MI.IMPIANTI, SISCOM_MI.INDIRIZZI, SISCOM_MI.INTERVENTI_MANUTENZIONE,SISCOM_MI.TIPOLOGIA_SERVIZI_MANU, SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU WHERE EDIFICI.ID= IMPIANTI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.IMPIANTI.ID = SISCOM_MI.INTERVENTI_MANUTENZIONE.ID_IMPIANTO AND IMPIANTI.ID_COMPLESSO IS NULL AND IMPIANTI.ID_EDIFICIO IS NOT NULL AND TIPOLOGIA_SERVIZI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_SERVIZIO AND TIPOLOGIA_INTERVENTI_MANU.ID = INTERVENTI_MANUTENZIONE.ID_TIPO_INTERVENTO AND TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " & sStringasql


            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

End Class
