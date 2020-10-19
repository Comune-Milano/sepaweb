
Partial Class MANUTENZIONI_Servizio_Manut
    Inherits PageSetIdMode
    Dim par As New CM.Global

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
    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
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
    Private Property vIdMillesimo() As String
        Get
            If Not (ViewState("par_idMillesimo") Is Nothing) Then
                Return CStr(ViewState("par_idMillesimo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idMillesimo") = value
        End Set

    End Property


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

            Select Case TipoImmobile
                Case "COMP"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.DrLEdificio.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.LBLINSEDIFICIO.Enabled = False
                    Me.LBLINSUNICOM.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False

                Case "EDIF"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.cmbUnitaComune.Enabled = False

                    Me.LBLINSUNICOM.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False
                Case "UNI_COM"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False
                Case "UNI_IMMOB"
                    Me.cmbUnitaComune.Enabled = False
                    Me.LBLINSUNICOM.Enabled = False
                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False

            End Select
            If vId <> 0 Then
                Apriricerca()
            Else
                Me.LblRiepilogo.Visible = False
            End If

        End If
        txtindietro.Text = txtindietro.Text - 1

        TxtDataInizio.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataOrdine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDatFine.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

        TxtDataInizio.Attributes.Add("onfocus", "javascript:selectText(this);")
        txtDataOrdine.Attributes.Add("onfocus", "javascript:selectText(this);")
        txtDatFine.Attributes.Add("onfocus", "javascript:selectText(this);")

        TxtDataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataOrdine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDatFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        Session.Item("LAVORAZIONE") = 1

    End Sub
    Private Sub RiempiCampi()
        'Apro la CONNESSIONE E LA TRANSAZIONE con il DB
        If par.OracleConn.State = Data.ConnectionState.Closed Then

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
        End If
        'CARICO COMBO COMPLESSI
        Dim gest As Integer
        gest = 0
        Try
            If gest > 0 Then
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari  where substr(id,1,1)= " & gest & "order by denominazione asc"

            Else
                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari order by denominazione asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

                'cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()
            cmbComplesso.SelectedValue = "-1"


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_UTENZA"
            myReader1 = par.cmd.ExecuteReader
            cmbTipoServizio.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoServizio.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            cmbTipoServizio.Text = "-1"
            myReader1.Close()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        'CaricaInterventi()
        CaricaEdifici()



    End Sub
    Private Sub CaricaEdifici()
        If par.OracleConn.State = Data.ConnectionState.Closed Then

            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim gest As Integer = 0
        Try


            Me.DrLEdificio.Items.Clear()

            DrLEdificio.Items.Add(New ListItem(" ", -1))


            If gest <> 0 Then
                par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici where substr(id,1,1)= " & gest & " order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While


            myReader1.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            DrLEdificio.SelectedValue = "-1"
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
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
        End Try
    End Sub
    Private Sub Apriricerca()
        'Apro la CONNESSIONE E LA TRANSAZIONE con il DB
        If par.OracleConn.State = Data.ConnectionState.Open Then
            Response.Write("IMPOSSIBILE VISUALIZZARE")
            Exit Sub
        Else
            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)
        End If
        Try
            If vId <> 0 Then
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                Select Case TipoImmobile
                    Case "COMP"
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SERVIZI WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("COD_TIPOLOGIA"), "-1")
                            Me.txtNote.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                            Me.txtCosto.Text = par.IfNull(myReader1("COSTO"), "")
                            Me.TxtDataInizio.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                            Me.txtDatFine.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                            Me.txtDataOrdine.Text = par.FormattaData(par.IfNull(myReader1("DATA_ORDINE"), ""))
                            Me.cmbReversibile.SelectedValue = par.IfNull(myReader1("REVERSIBILE"), 0)
                            If Me.cmbReversibile.SelectedValue = "1" Then
                                Me.txtCostoRevers.Visible = True
                                Me.lblRevers.Visible = True
                            End If
                            Me.txtCostoRevers.Text = par.IfNull(myReader1("COSTO_REVERSIBILE"), "")
                            Me.txtNumDoc.Text = par.IfNull(myReader1("NUM_DOCUMENTO"), "")
                            Me.txtNumFattura.Text = par.IfNull(myReader1("NUM_FATTURA"), "")
                            vIdMillesimo = par.IfEmpty(myReader1.Item("ID_TABELLA_MILLESIMALE").ToString, "Null")

                        End If
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID = " & Me.cmbComplesso.SelectedValue.ToString & " AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.LblRiepilogo.Text = "COMPLESSO IMMOBILIARE SITO IN " & par.IfNull(myReader1("INDIRIZZO"), " - - ") & ", CIVICO " & par.IfNull(myReader1("CIVICO"), " - - ") & ", LOCALITA " & par.IfNull(myReader1("LOCALITA"), " - - ") & ", CAP " & par.IfNull(myReader1("CAP"), " - - ")
                        End If
                        CaricaTabMillesimali()
                        If vIdMillesimo <> "Null" Then
                            Me.RdbMillesimali.SelectedValue = vIdMillesimo
                        End If

                        'ApriCorrelateEdifici()
                    Case "EDIF"
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SERVIZI WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("COD_TIPOLOGIA"), "-1")
                            Me.txtNote.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                            Me.txtCosto.Text = par.IfNull(myReader1("COSTO"), "")
                            Me.TxtDataInizio.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                            Me.txtDatFine.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                            Me.txtDataOrdine.Text = par.FormattaData(par.IfNull(myReader1("DATA_ORDINE"), ""))
                            Me.cmbReversibile.SelectedValue = par.IfNull(myReader1("REVERSIBILE"), 0)
                            If Me.cmbReversibile.SelectedValue = "1" Then
                                Me.txtCostoRevers.Visible = True
                                Me.lblRevers.Visible = True
                            End If

                            Me.txtCostoRevers.Text = par.IfNull(myReader1("COSTO_REVERSIBILE"), "")
                            Me.txtNumDoc.Text = par.IfNull(myReader1("NUM_DOCUMENTO"), "")
                            Me.txtNumFattura.Text = par.IfNull(myReader1("NUM_FATTURA"), "")
                            vIdMillesimo = par.IfEmpty(myReader1.Item("ID_TABELLA_MILLESIMALE").ToString, "Null")

                        End If
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI WHERE EDIFICI.ID = " & Me.DrLEdificio.SelectedValue.ToString & " AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.LblRiepilogo.Text = "EDIFICIO SITO IN " & par.IfNull(myReader1("INDIRIZZO"), " - - ") & ", CIVICO " & par.IfNull(myReader1("CIVICO"), " - - ") & ", LOCALITA " & par.IfNull(myReader1("LOCALITA"), " - - ") & ", CAP " & par.IfNull(myReader1("CAP"), " - - ")
                        End If
                        CaricaTabMillesimali()
                        If vIdMillesimo <> "Null" Then
                            Me.RdbMillesimali.SelectedValue = vIdMillesimo
                        End If


                    Case "UNI_COM"
                        Me.cmbUnitaComune.Items.Add(New ListItem(" ", -1))
                        par.cmd.CommandText = "  Select id, 'COD. '||COD_UNITA_COMUNE||' - '||LOCALIZZAZIONE as DESCRIZIONE from SISCOM_MI.UNITA_COMUNI "
                        myReader1 = par.cmd.ExecuteReader()
                        While myReader1.Read
                            cmbUnitaComune.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
                        End While
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SERVIZI WHERE ID = " & vId

                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.cmbUnitaComune.SelectedValue = par.IfNull(myReader1("ID_UNITA_COMUNE"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("COD_TIPOLOGIA"), "-1")
                            Me.txtNote.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                            Me.txtCosto.Text = par.IfNull(myReader1("COSTO"), "")
                            Me.TxtDataInizio.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                            Me.txtDatFine.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                            Me.txtDataOrdine.Text = par.FormattaData(par.IfNull(myReader1("DATA_ORDINE"), ""))
                            Me.cmbReversibile.SelectedValue = par.IfNull(myReader1("REVERSIBILE"), 0)
                            If Me.cmbReversibile.SelectedValue = "1" Then
                                Me.txtCostoRevers.Visible = True
                                Me.lblRevers.Visible = True
                            End If

                            Me.txtCostoRevers.Text = par.IfNull(myReader1("COSTO_REVERSIBILE"), "")
                            Me.txtNumDoc.Text = par.IfNull(myReader1("NUM_DOCUMENTO"), "")
                            Me.txtNumFattura.Text = par.IfNull(myReader1("NUM_FATTURA"), "")
                        End If
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT ID_EDIFICIO, ID_COMPLESSO FROM SISCOM_MI.UNITA_COMUNI WHERE ID = " & Me.cmbUnitaComune.SelectedValue.ToString
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            If par.IfNull(myReader1("ID_COMPLESSO"), "-1") <> "-1" Then
                                par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID = " & myReader1("ID_COMPLESSO") & " AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID"
                            Else
                                par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI WHERE EDIFICI.ID = " & myReader1("ID_EDIFICIO") & " AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID"
                            End If
                        End If
                        myReader1.Close()
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.LblRiepilogo.Text = "UNITA COMUNE SITA IN " & par.IfNull(myReader1("INDIRIZZO"), " - - ") & ", CIVICO " & par.IfNull(myReader1("CIVICO"), " - - ") & ", LOCALITA " & par.IfNull(myReader1("LOCALITA"), " - - ") & ", CAP " & par.IfNull(myReader1("CAP"), " - - ")
                        End If

                    Case "UNI_IMMOB"
                        Me.cmbUnitaImmob.Items.Add(New ListItem(" ", -1))
                        par.cmd.CommandText = "  Select id, 'COD.'||COD_UNITA_IMMOBILIARE||' - SCALA '||SCALA ||' - INTERNO '||INTERNO as DESCRIZIONE from SISCOM_MI.Unita_immobiliari"
                        myReader1 = par.cmd.ExecuteReader()
                        While myReader1.Read
                            cmbUnitaImmob.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("id"), -1)))
                        End While
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SERVIZI WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.cmbUnitaImmob.SelectedValue = par.IfNull(myReader1("ID_UNITA_IMMOBILIARE"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("COD_TIPOLOGIA"), "-1")
                            Me.txtNote.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                            Me.txtCosto.Text = par.IfNull(myReader1("COSTO"), "")
                            Me.TxtDataInizio.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                            Me.txtDatFine.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                            Me.txtDataOrdine.Text = par.FormattaData(par.IfNull(myReader1("DATA_ORDINE"), ""))
                            Me.cmbReversibile.SelectedValue = par.IfNull(myReader1("REVERSIBILE"), 0)
                            If Me.cmbReversibile.SelectedValue = "1" Then
                                Me.txtCostoRevers.Visible = True
                                Me.lblRevers.Visible = True
                            End If

                            Me.txtCostoRevers.Text = par.IfNull(myReader1("COSTO_REVERSIBILE"), "")
                            Me.txtNumDoc.Text = par.IfNull(myReader1("NUM_DOCUMENTO"), "")
                            Me.txtNumFattura.Text = par.IfNull(myReader1("NUM_FATTURA"), "")
                        End If
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT  INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI WHERE EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO AND SISCOM_MI.UNITA_IMMOBILIARI.ID =" & Me.cmbUnitaImmob.SelectedValue.ToString & " AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.LblRiepilogo.Text = "UNITA IMMOBILIARE SITA IN " & par.IfNull(myReader1("INDIRIZZO"), " - - ") & ", CIVICO " & par.IfNull(myReader1("CIVICO"), " - - ") & ", LOCALITA " & par.IfNull(myReader1("LOCALITA"), " - - ") & ", CAP " & par.IfNull(myReader1("CAP"), " - - ")
                        End If
                End Select

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                myReader1.Close()

                Me.cmbUnitaImmob.Enabled = False
                Me.cmbComplesso.Enabled = False
                Me.DrLEdificio.Enabled = False
                Me.cmbUnitaComune.Enabled = False
                Me.LBLINSCOMP.Enabled = False
                Me.LBLINSEDIFICIO.Enabled = False
                Me.LBLINSUNICOM.Enabled = False
                Me.LBLINSUNIIMMOB.Enabled = False

            End If
        Catch ex As Exception
            par.OracleConn.Close()

        End Try
    End Sub
    'Private Sub ApriCorrelateEdifici()

    '    CaricaListBox()
    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SERVIZI_EDIFICI WHERE ID_SERVIZIO =" & vId
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '        While myReader.Read
    '            Me.ListEdifci.Items.FindByValue(myReader.Item("ID_EDIFICIO")).Selected = True
    '            Selezionati = 1
    '        End While
    '        myReader.Close()

    '        par.OracleConn.Close()
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '    End Try

    'End Sub
    Private Sub CaricaListBox()
        Try
            If Me.cmbComplesso.SelectedValue <> "-1" And Me.TipoImmobile = "COMP" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.ListEdifci.Items.Clear()
                par.cmd.CommandText = "select  edifici.id  ,('COD. '||edifici.cod_edificio ||' - - '||edifici.denominazione) as DESCRIZIONE from SISCOM_MI.edifici where edifici.id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by edifici.cod_edificio asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()
                If Me.ListEdifci.Items.Count > 0 Then
                    Me.LblEdificiAssociati.Visible = True
                    Me.btnSelezionaTutto.Visible = True
                Else
                    Me.LblEdificiAssociati.Visible = False
                    Me.btnSelezionaTutto.Visible = False

                End If
                myReader.Close()

            Else
                Me.ListEdifci.Items.Clear()
                Me.LblEdificiAssociati.Visible = False
                Me.btnSelezionaTutto.Visible = False
            End If
            '300000046
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '& Me.DLRComplessi.SelectedValue.ToString & 
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Try
            Dim a As String
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If
            Return a
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Function
    Private Sub salva()

        Try

            If TxtDataInizio.Text = "dd/Mm/YYYY" Then
                TxtDataInizio.Text = ""
            End If

            If txtDatFine.Text = "dd/Mm/YYYY" Then
                txtDatFine.Text = ""
            End If
            If txtDataOrdine.Text = "dd/Mm/YYYY" Then
                txtDataOrdine.Text = ""
            End If
            If par.IfEmpty(txtDatFine.Text, "Null") <> "Null" Then

                If par.AggiustaData(TxtDataInizio.Text) > par.AggiustaData(txtDatFine.Text) Then
                    Response.Write("<SCRIPT>alert('La Data Fine non può essere precedente alla Data Inizio!');</SCRIPT>")
                    Me.txtDatFine.Text = ""
                    Exit Sub
                End If
            End If

            '****APRO LA CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then

                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '****VERIFICO IL TIPO DI INSERIMENTO DA FARE
            If Me.txtCosto.Text <> "" Then
                If CDbl(par.IfEmpty(Me.txtCostoRevers.Text, "0")) > CDbl(txtCosto.Text) Then
                    Response.Write("<SCRIPT>alert('Il Costo Reversibile deve essere minore del Costo!');</SCRIPT>")
                    Exit Sub
                End If
            End If

            If Me.txtCosto.Text <> "" Then

                par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_SERVIZI.NEXTVAL FROM dual "
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    vId = myReader1(0)
                End If
                myReader1.Close()
                par.cmd.CommandText = ""
                Select Case TipoImmobile
                    Case "COMP"
                        If Me.cmbComplesso.SelectedValue <> -1 Then
                            If Me.cmbReversibile.SelectedValue = 1 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI (ID, ID_COMPLESSO, COD_TIPOLOGIA,DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA, ID_TABELLA_MILLESIMALE)" _
                                & "VALUES (" & vId & ", " & Me.cmbComplesso.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.IfEmpty(par.VirgoleInPunti(Me.txtCosto.Text), "Null") & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null,'" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "'," & par.IfEmpty(RdbMillesimali.SelectedValue.ToString, "Null") & "  )"
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI (ID, ID_COMPLESSO, COD_TIPOLOGIA,DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                                & "VALUES (" & vId & ", " & Me.cmbComplesso.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.IfEmpty(par.VirgoleInPunti(Me.txtCosto.Text), "Null") & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null,'" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "')"

                            End If

                            par.cmd.ExecuteNonQuery()

                            ' '' ''++++++++CHECk EDIFICI ASSOCIATI è STATA TOLTA PER ASSOCIARE LE TABELLE MILLESIMALI 25/06/2009******
                            'If Me.ListEdifci.Items.Count > 0 Then

                            '    For Each o As Object In ListEdifci.Items
                            '        Dim item As System.Web.UI.WebControls.ListItem
                            '        item = CType(o, System.Web.UI.WebControls.ListItem)
                            '        If item.Selected Then
                            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI_EDIFICI (ID_SERVIZIO,ID_EDIFICIO ) VALUES (" & vId & "," & item.Value & ")"
                            '            par.cmd.ExecuteNonQuery()
                            '        End If
                            '    Next
                            'End If
                            '+++++++++finenuova(check)

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere un Complesso Immobiliare!');</SCRIPT>")
                            Exit Sub
                        End If
                    Case "EDIF"
                        If Me.DrLEdificio.SelectedValue <> -1 Then
                            If Me.cmbReversibile.SelectedValue = 0 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI (ID, ID_EDIFICIO, COD_TIPOLOGIA,DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                                & "VALUES (" & vId & ", " & Me.DrLEdificio.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI (ID, ID_EDIFICIO, COD_TIPOLOGIA,DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA, ID_TABELLA_MILLESIMALE)" _
                                & "VALUES (" & vId & ", " & Me.DrLEdificio.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "'," & par.IfEmpty(RdbMillesimali.SelectedValue.ToString, "Null") & "   )"

                            End If
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('scegliere un Edifcio!');</SCRIPT>")
                            Exit Sub

                        End If
                    Case "UNI_COM"
                        If Me.cmbUnitaComune.SelectedValue <> -1 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI (ID, ID_UNITA_COMUNE, COD_TIPOLOGIA,DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                            & "VALUES (" & vId & ", " & Me.cmbUnitaComune.SelectedValue.ToString & ", " _
                            & "" & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                            & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere una Unità Comune!');</SCRIPT>")
                            Exit Sub

                        End If
                    Case "UNI_IMMOB"
                        If Me.cmbUnitaImmob.SelectedValue <> -1 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI (ID, ID_UNITA_IMMOBILIARE, COD_TIPOLOGIA,DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                            & "VALUES (" & vId & ", " & Me.cmbUnitaImmob.SelectedValue.ToString & ", " _
                            & "" & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                            & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere una Unità Immobiliare!');</SCRIPT>")
                            Exit Sub

                        End If
                End Select


                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                Me.LBLINSCOMP.Enabled = False
                Me.LBLINSEDIFICIO.Enabled = False
                Me.LBLINSUNICOM.Enabled = False
                Me.LBLINSUNIIMMOB.Enabled = False
                Me.cmbComplesso.Enabled = False
                Me.DrLEdificio.Enabled = False
                Me.cmbUnitaImmob.Enabled = False
                Me.cmbUnitaComune.Enabled = False
            Else
                Response.Write("<SCRIPT>alert('Il campo COSTO deve essere avvalorato!');</SCRIPT>")

            End If


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub Update()
        Dim DataInizio As String
        Dim DataFine As String
        Dim DataOrdine As String
        Try
            Dim IdMillesimo As String


            If TxtDataInizio.Text = "dd/Mm/YYYY" Then
                DataInizio = ""
            End If

            If txtDatFine.Text = "dd/Mm/YYYY" Then
                DataFine = ""
            End If
            If txtDataOrdine.Text = "dd/Mm/YYYY" Then
                DataOrdine = ""
            End If
            If par.IfEmpty(txtDatFine.Text, "Null") <> "Null" Then

                If par.AggiustaData(TxtDataInizio.Text) > par.AggiustaData(txtDatFine.Text) Then
                    Response.Write("<SCRIPT>alert('La Data Fine non può essere precedente alla Data Inizio!');</SCRIPT>")
                    Me.txtDatFine.Text = ""
                    Exit Sub
                End If
            End If

            If (Me.cmbComplesso.SelectedValue <> "-1" Or Me.DrLEdificio.SelectedValue <> "-1" Or Me.cmbUnitaImmob.SelectedValue <> "-1" Or Me.cmbUnitaComune.SelectedValue <> "-1") AndAlso par.IfEmpty(Me.txtCosto.Text, "Null") <> "Null" Then

                If Me.txtCosto.Text <> "" Then
                    If CDbl(par.IfEmpty(Me.txtCostoRevers.Text, "0")) > CDbl(txtCosto.Text) Then
                        Response.Write("<SCRIPT>alert('Il Costo Reversibile deve essere minore del Costo!');</SCRIPT>")
                        Exit Sub
                    End If
                End If
                If par.OracleConn.State = Data.ConnectionState.Closed Then

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
                    'par.SettaCommand(par)
                    'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
                    '‘‘par.cmd.Transaction = par.myTrans
                End If
                If cmbReversibile.SelectedValue = 0 Then
                    IdMillesimo = "Null"
                Else : IdMillesimo = Me.RdbMillesimali.SelectedValue.ToString
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.SERVIZI  SET  COD_TIPOLOGIA = " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", DESCRIZIONE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', COSTO = " & par.IfEmpty(par.VirgoleInPunti(Me.txtCosto.Text), "Null") & ", DATA_ORDINE = '" & par.AggiustaData(Me.txtDataOrdine.Text) & "', DATA_INIZIO_INTERVENTO = '" & par.AggiustaData(Me.TxtDataInizio.Text) & "', DATA_FINE_INTERVENTO = '" & par.AggiustaData(Me.txtDatFine.Text) & "',REVERSIBILE=" & Me.cmbReversibile.SelectedValue.ToString & ", COSTO_REVERSIBILE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", ID_TABELLA=Null, NUM_DOCUMENTO='" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', NUM_FATTURA='" & par.PulisciStrSql(Me.txtNumFattura.Text) & "', ID_TABELLA_MILLESIMALE=" & IdMillesimo & "  WHERE ID = " & vId
                par.cmd.ExecuteNonQuery()
                ''par.cmd.CommandText = "DELETE FROM SISCOM_MI.SERVIZI_EDIFICI WHERE ID_SERVIZIO = " & vId
                ''par.cmd.ExecuteNonQuery()

                ' ''++++++++NUOVA CHECk+++++++++++++++

                ''If Me.ListEdifci.Items.Count > 0 Then

                ''    For Each o As Object In ListEdifci.Items
                ''        Dim item As System.Web.UI.WebControls.ListItem
                ''        item = CType(o, System.Web.UI.WebControls.ListItem)
                ''        If item.Selected Then
                ''            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SERVIZI_EDIFICI (ID_SERVIZIO,ID_EDIFICIO ) VALUES (" & vId & "," & item.Value & ")"
                ''            par.cmd.ExecuteNonQuery()
                ''        End If
                ''    Next
                ''End If
                ' ''+++++++++fine nuova check

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            Else
                Response.Write("<SCRIPT>alert('ATTENZIONE!Controllare i dati.');</SCRIPT>")

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "UNI_IMMOB" Then
            FiltraEdifici()
            If Me.cmbUnitaImmob.Items.Count = 0 Then
                Me.cmbUnitaImmob.Items.Add(New ListItem(" SELEZIONARE UN EDIFICIO ", -1))
            End If
            FiltraUnitaComuni()
        ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "UNI_COM" Then
            FiltraEdifici()
            FiltraUnitaComuni()
        ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "EDIF" Then
            FiltraEdifici()
        ElseIf Me.cmbUnitaComune.SelectedValue <> "-1" And TipoImmobile = "COMP" Then
            CaricaTabMillesimali()
        Else
            Me.cmbUnitaComune.Items.Clear()
            CaricaEdifici()
        End If
    End Sub
    Private Sub FiltraUnitaComuni()
        If Me.cmbComplesso.SelectedValue <> "-1" Or Me.DrLEdificio.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Try
                Me.cmbUnitaComune.Items.Clear()
                Me.cmbUnitaComune.Items.Add(New ListItem(" ", -1))
                If Me.DrLEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "  Select id, 'COD. '||COD_UNITA_COMUNE||' - '||LOCALIZZAZIONE as DESCRIZIONE from SISCOM_MI.UNITA_COMUNI where id_edificio = " & Me.DrLEdificio.SelectedValue.ToString
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
            End Try
        End If
    End Sub
    Private Sub FiltraUnitaImmob()
        If Me.DrLEdificio.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Try
                Me.cmbUnitaImmob.Items.Clear()
                Me.cmbUnitaImmob.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "  Select id, 'COD.'||COD_UNITA_IMMOBILIARE||' - SCALA '||SCALA ||' - INTERNO '||INTERNO as DESCRIZIONE from SISCOM_MI.Unita_immobiliari where id_edificio = " & Me.DrLEdificio.SelectedValue.ToCharArray
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
            Me.DrLEdificio.Items.Clear()
            DrLEdificio.Items.Add(New ListItem(" ", -1))
            Try
                If gest <> 0 Then
                    par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici where substr(id,1,1)= " & gest & " order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct id,(denominazione||' - -Cod.'||COD_EDIFICIO) AS denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"
                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try
        End If
    End Sub


    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        If Selezionati = "" Then
            Selezionati = 1
        Else
            Selezionati = ""
        End If
        Dim a As Integer
        Dim i As Integer = 0
        If Selezionati <> "" Then
            a = ListEdifci.Items.Count.ToString
            While i < a
                Me.ListEdifci.Items(i).Selected = True
                i = i + 1
            End While
        Else
            a = ListEdifci.Items.Count.ToString
            While i < a
                Me.ListEdifci.Items(i).Selected = False
                i = i + 1
            End While
        End If
    End Sub

    Protected Sub cmbReversibile_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbReversibile.SelectedIndexChanged
        If Me.cmbReversibile.SelectedValue = 1 Then
            Me.lblRevers.Visible = True
            Me.txtCostoRevers.Visible = True
            Me.txtCostoRevers.Text = ""
            Me.RdbMillesimali.Visible = True
            If Me.RdbMillesimali.Items.Count > 0 Then
                Me.LblMillesimali.Visible = True
            End If
        Else
            Me.lblRevers.Visible = False
            Me.txtCostoRevers.Visible = False
            Me.txtCostoRevers.Text = ""
            Me.RdbMillesimali.Visible = False
            Me.LblMillesimali.Visible = False

        End If

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If vId = 0 Then
            Me.salva()
        Else
            Update()
        End If

    End Sub

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Response.Write("<script>document.location.href=""pagina_home_Interventi.aspx""</script>")
        Session.Item("LAVORAZIONE") = 0
    End Sub
    Private Sub CaricaTabMillesimali()
        Try
            If (Me.cmbComplesso.SelectedValue <> "-1" And Me.TipoImmobile = "COMP") Or (Me.DrLEdificio.SelectedValue <> "-1" And Me.TipoImmobile = "EDIF") Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.RdbMillesimali.Items.Clear()
                If Me.TipoImmobile = "COMP" Then
                    par.cmd.CommandText = "SELECT TABELLE_MILLESIMALI.ID,(TIPOLOGIA_MILLESIMALE.DESCRIZIONE ||' - - '|| TABELLE_MILLESIMALI.DESCRIZIONE_TABELLA) AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE WHERE ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " AND TABELLE_MILLESIMALI.COD_TIPOLOGIA=TIPOLOGIA_MILLESIMALE.COD"
                ElseIf TipoImmobile = "EDIF" Then
                    par.cmd.CommandText = "SELECT TABELLE_MILLESIMALI.ID,(TIPOLOGIA_MILLESIMALE.DESCRIZIONE ||' - - '|| TABELLE_MILLESIMALI.DESCRIZIONE_TABELLA) AS DESCRIZIONE FROM SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE WHERE ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToString & " AND TABELLE_MILLESIMALI.COD_TIPOLOGIA=TIPOLOGIA_MILLESIMALE.COD"
                End If
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    RdbMillesimali.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                End While
                myReader.Close()
                If Me.cmbReversibile.SelectedValue = 1 Then
                    Me.RdbMillesimali.Visible = True
                Else
                    Me.RdbMillesimali.Visible = False
                End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                If Me.RdbMillesimali.Items.Count > 0 AndAlso Me.cmbReversibile.SelectedValue = 1 Then
                    Me.LblMillesimali.Visible = True
                End If

            Else
                Me.RdbMillesimali.Items.Clear()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try




    End Sub


    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        If Me.DrLEdificio.SelectedValue <> "-1" Then
            CaricaTabMillesimali()
            FiltraUnitaComuni()
            FiltraUnitaImmob()
        ElseIf Me.DrLEdificio.SelectedValue = "-1" And TipoImmobile = "UNI_IMMOB" Then
            Me.cmbUnitaImmob.Items.Clear()
            Me.cmbUnitaImmob.Items.Add(New ListItem(" SELEZIONARE UN EDIFICIO ", -1))
            If Me.cmbComplesso.SelectedValue = "-1" Then
                Me.cmbUnitaComune.Items.Clear()
            Else
                FiltraUnitaComuni()
                Me.RdbMillesimali.Items.Clear()

            End If
        End If

    End Sub

End Class
