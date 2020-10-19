
Partial Class MANUTENZIONI_RicercaServizi
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

                    Me.LblRicerca.Text = "Ricerca Servizi su COMPLESSI"
                Case "EDIF"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.LblUniCom.Enabled = False
                    Me.LblUniImmob.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Servizi su EDIFICI"

                Case "UNI_COM"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.LblUniImmob.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Servizi su UNITA' COMUNI"

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False
                Case "UNI_IMMOB"
                    Me.cmbUnitaComune.Enabled = False
                    Me.LblUniCom.Enabled = False
                    Me.LblRicerca.Text = "Ricerca Servizi su UNITA' IMMOBILIARI"

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False

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

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
            End If
            'CARICO COMBO COMPLESSI
            Dim gest As Integer
            gest = 0


            If gest > 0 Then
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari  where substr(id,1,1)= " & gest & "order by denominazione asc"

            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari order by denominazione asc"
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                'cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"
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
                par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici order by denominazione asc"

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
            par.OracleConn.Close()
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
            par.OracleConn.Close()
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
        Else
            Me.cmbUnitaComune.Items.Clear()
            CaricaEdifici()
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
                par.OracleConn.Close()
                Me.cmbUnitaImmob.SelectedValue = "-1"

            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()

            End Try
        End If
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
                par.OracleConn.Close()
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
                par.OracleConn.Close()
            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()

            End Try
        End If
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.SelectedValue <> "-1" Then
            FiltraUnitaComuni()
            FiltraUnitaImmob()
        Else
            Me.cmbUnitaImmob.Items.Clear()
            If Me.cmbComplesso.SelectedValue = "-1" Then
                Me.cmbUnitaComune.Items.Clear()
            Else
                FiltraUnitaComuni()
            End If
        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home_Interventi.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim sStringaSql As String
        Try


            Select Case TipoImmobile
                Case "COMP"
                    If Me.cmbComplesso.SelectedValue <> -1 Then
                        sStringaSql = "select ROWNUM, servizi.id, complessi_immobiliari.cod_complesso AS COD_IMMOBILE,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari, SISCOM_MI.servizi WHERE  COMPLESSI_IMMOBILIARI.Id =" & Me.cmbComplesso.SelectedValue.ToString & "  and COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.servizi.ID_COMPLESSO ORDER BY ROWNUM ASC"
                    Else
                        sStringaSql = "select ROWNUM, servizi.id, complessi_immobiliari.cod_complesso AS COD_IMMOBILE,complessi_immobiliari.denominazione,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO from SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari, SISCOM_MI.servizi WHERE   COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.servizi.ID_COMPLESSO ORDER BY ROWNUM ASC"
                    End If
                Case "EDIF"
                    If Me.cmbEdificio.SelectedValue <> -1 Then
                        sStringaSql = "SELECT ROWNUM,servizi.id,edifici.cod_edificio AS COD_IMMOBILE, EDIFICI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE  edifici.Id= " & Me.cmbEdificio.SelectedValue.ToString & "  and EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.edifici.ID = SISCOM_MI.servizi.id_edificio ORDER BY ROWNUM ASC"
                    ElseIf Me.cmbComplesso.SelectedValue <> -1 Then
                        sStringaSql = "SELECT ROWNUM,servizi.id,edifici.cod_edificio AS COD_IMMOBILE, EDIFICI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & "AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.edifici.ID = SISCOM_MI.servizi.id_edificio ORDER BY ROWNUM ASC"
                    Else
                        sStringaSql = "SELECT ROWNUM,servizi.id,edifici.cod_edificio AS COD_IMMOBILE, EDIFICI.DENOMINAZIONE, INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.edifici.ID = SISCOM_MI.servizi.id_edificio ORDER BY ROWNUM ASC"

                    End If
                Case "UNI_COM"
                    If Me.cmbComplesso.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue = "-1" Then

                        If Me.cmbUnitaComune.SelectedValue <> "-1" And Me.cmbUnitaComune.SelectedValue <> "" Then
                            sStringaSql = "SELECT ROWNUM,servizi.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE  UNITA_COMUNI.ID=" & Me.cmbUnitaComune.SelectedValue.ToString & " AND COMPLESSI_IMMOBILIARI.ID= UNITA_COMUNI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.servizi.ID_UNITA_COMUNE ORDER BY ROWNUM ASC"
                        Else
                            sStringaSql = "SELECT ROWNUM,servizi.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE COMPLESSI_IMMOBILIARI.ID = " & Me.cmbComplesso.SelectedValue.ToString & " AND COMPLESSI_IMMOBILIARI.ID= UNITA_COMUNI.ID_COMPLESSO  AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.servizi.ID_UNITA_COMUNE ORDER BY ROWNUM ASC"
                        End If

                    ElseIf cmbEdificio.SelectedValue <> "-1" Then

                        If Me.cmbUnitaComune.SelectedValue <> "-1" Then
                            sStringaSql = "SELECT ROWNUM,servizi.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE  UNITA_COMUNI.ID=" & Me.cmbUnitaComune.SelectedValue.ToString & " AND EDIFICI.ID= UNITA_COMUNI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.servizi.ID_UNITA_COMUNE ORDER BY ROWNUM ASC"
                        Else
                            sStringaSql = "SELECT ROWNUM,servizi.ID,UNITA_COMUNI.COD_UNITA_COMUNE AS COD_IMMOBILE, (UNITA_COMUNI.LOCALIZZAZIONE)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE UNITA_COMUNI.ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue.ToString & " AND EDIFICI.ID= UNITA_COMUNI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_COMUNI.ID = SISCOM_MI.servizi.ID_UNITA_COMUNE ORDER BY ROWNUM ASC"
                        End If
                    ElseIf cmbEdificio.SelectedValue = "-1" AndAlso Me.cmbComplesso.SelectedValue = "-1" AndAlso Me.cmbUnitaComune.SelectedValue = "" AndAlso Me.cmbUnitaImmob.SelectedValue = "" Then
                        Response.Write("<script>alert('Nessun criterio valido per la ricerca di Servizi su Unità Comuni!')</script>")
                        Exit Sub
                    End If
                Case "UNI_IMMOB"
                    If Me.cmbUnitaImmob.SelectedValue <> "-1" And Me.cmbUnitaImmob.SelectedValue <> "" Then
                        sStringaSql = "SELECT ROWNUM,servizi.id,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_IMMOBILE, ('SCALA - '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA||'INTERNO - '||UNITA_IMMOBILIARI.INTERNO)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi WHERE  UNITA_IMMOBILIARI.Id=" & Me.cmbUnitaImmob.SelectedValue.ToString & " AND EDIFICI.ID= UNITA_IMMOBILIARI.ID_EDIFICIO  and EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.servizi.ID_UNITA_IMMOBILIARE ORDER BY ROWNUM ASC"
                    ElseIf Me.cmbComplesso.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue = "-1" Then
                        sStringaSql = "SELECT ROWNUM,servizi.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_IMMOBILE, ('SCALA - '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA||'INTERNO - '||UNITA_IMMOBILIARI.INTERNO)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID = EDIFICI.ID_COMPLESSO AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " AND EDIFICI.ID= UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.servizi.ID_UNITA_IMMOBILIARE ORDER BY ROWNUM ASC"
                    ElseIf Me.cmbEdificio.SelectedValue <> "-1" Then
                        sStringaSql = "SELECT ROWNUM,servizi.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_IMMOBILE, ('SCALA - '||(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA||'INTERNO - '||UNITA_IMMOBILIARI.INTERNO)AS DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO, to_char(to_date(SERVIZI.DATA_INIZIO_INTERVENTO,'yyyymmdd'),'dd/mm/yyyy')AS DATA_INIZIO,SERVIZI.DESCRIZIONE AS DESCRIZIONE_INTERVENTO  FROM SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SISCOM_MI.servizi   WHERE UNITA_IMMOBILIARI.ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue.ToString & " AND EDIFICI.ID= UNITA_IMMOBILIARI.ID_EDIFICIO  AND EDIFICI.ID_INDIRIZZO_PRINCIPALE  = INDIRIZZI.ID AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.servizi.ID_UNITA_IMMOBILIARE ORDER BY ROWNUM ASC"
                    ElseIf cmbEdificio.SelectedValue = "-1" AndAlso Me.cmbComplesso.SelectedValue = "-1" AndAlso Me.cmbUnitaComune.SelectedValue = "" AndAlso Me.cmbUnitaImmob.SelectedValue = "" Then
                        Response.Write("<script>alert('Nessun criterio valido per la ricerca di Servizi su Unità Immobiliari!')</script>")
                        Exit Sub
                    End If

            End Select

            Session.Add("PED", sStringaSql)
            Response.Redirect("RisultatiServizi.aspx")
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub
End Class
