
Partial Class MANUTENZIONI_Int_Manutenzione
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

            Select Case TipoImmobile
                Case "COMP"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.DrLEdificio.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.LBLINSEDIFICIO.Enabled = False
                    Me.LBLINSUNICOM.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False
                    Me.cmbImpianti.Enabled = False
                    Me.lblImpianti.Enabled = False

                Case "EDIF"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.cmbUnitaComune.Enabled = False
                    Me.cmbImpianti.Enabled = False
                    Me.lblImpianti.Enabled = False

                    Me.LBLINSUNICOM.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False
                Case "UNI_COM"
                    Me.cmbUnitaImmob.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False
                    Me.cmbImpianti.Enabled = False
                    Me.lblImpianti.Enabled = False

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False
                Case "UNI_IMMOB"
                    Me.cmbUnitaComune.Enabled = False
                    Me.LBLINSUNICOM.Enabled = False
                    Me.cmbImpianti.Enabled = False
                    Me.lblImpianti.Enabled = False

                    'Me.cmbComplesso.Enabled = False
                    'Me.DrLEdificio.Enabled = False
                Case "IMPIANTI"
                    Me.cmbUnitaComune.Enabled = False
                    Me.LBLINSUNIIMMOB.Enabled = False
                    Me.cmbUnitaImmob.Enabled = False
                    Me.LBLINSUNICOM.Enabled = False


            End Select
            If vId <> 0 Then
                Apriricerca()
            Else
                Me.LblRiepilogo.Visible = False
            End If

            Session.Item("LAVORAZIONE") = 1
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

    End Sub

    Private Sub Apriricerca()

        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim IDINTERVENTO As String = ""
        Try
            If vId <> 0 Then
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                Select Case TipoImmobile
                    Case "COMP"
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.cmbComplesso.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("ID_TIPO_SERVIZIO"), "-1")
                            IDINTERVENTO = par.IfNull(myReader1("ID_TIPO_INTERVENTO"), "-1")
                            Me.cmbArticolo.SelectedValue = par.IfNull(myReader1("ID_ARTICOLO"), "-1")
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
                        myReader1.Close()

                        CaricaTabMillesimali()
                        If vIdMillesimo <> "Null" Then
                            Me.RdbMillesimali.SelectedValue = vIdMillesimo
                        End If
                    Case "EDIF"
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.DrLEdificio.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("ID_TIPO_SERVIZIO"), "-1")
                            IDINTERVENTO = par.IfNull(myReader1("ID_TIPO_INTERVENTO"), "-1")
                            Me.cmbArticolo.SelectedValue = par.IfNull(myReader1("ID_ARTICOLO"), -1)
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
                        myReader1.Close()

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
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE ID = " & vId

                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.cmbUnitaComune.SelectedValue = par.IfNull(myReader1("ID_UNITA_COMUNE"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("ID_TIPO_SERVIZIO"), "-1")
                            IDINTERVENTO = par.IfNull(myReader1("ID_TIPO_INTERVENTO"), "-1")
                            Me.cmbArticolo.SelectedValue = par.IfNull(myReader1("ID_ARTICOLO"), -1)
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
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.cmbUnitaImmob.SelectedValue = par.IfNull(myReader1("ID_UNITA_IMMOBILIARE"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("ID_TIPO_SERVIZIO"), "-1")
                            IDINTERVENTO = par.IfNull(myReader1("ID_TIPO_INTERVENTO"), "-1")
                            Me.cmbArticolo.SelectedValue = par.IfNull(myReader1("ID_ARTICOLO"), -1)
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

                    Case "IMPIANTI"

                        Me.cmbImpianti.Items.Add(New ListItem("", -1))
                        par.cmd.CommandText = "SELECT DISTINCT IMPIANTI.ID, IMPIANTI.COD_IMPIANTO, IMPIANTI.DESCRIZIONE, TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA"
                        myReader1 = par.cmd.ExecuteReader
                        While myReader1.Read
                            cmbImpianti.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " ") & " - " & par.IfNull(myReader1("TIPOLOGIA"), "") & " - cod. " & par.IfNull(myReader1("COD_IMPIANTO"), ""), par.IfNull(myReader1("id"), -1)))
                        End While
                        myReader1.Close()
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INTERVENTI_MANUTENZIONE WHERE ID = " & vId
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            Me.cmbImpianti.SelectedValue = par.IfNull(myReader1("ID_IMPIANTO"), -1)
                            Me.cmbTipoServizio.SelectedValue = par.IfNull(myReader1("ID_TIPO_SERVIZIO"), "-1")
                            IDINTERVENTO = par.IfNull(myReader1("ID_TIPO_INTERVENTO"), "-1")
                            Me.cmbArticolo.SelectedValue = par.IfNull(myReader1("ID_ARTICOLO"), -1)
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

                        par.cmd.CommandText = "SELECT  INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.IMPIANTI, SISCOM_MI.EDIFICI, SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.IMPIANTI.ID=254 AND EDIFICI.ID = SISCOM_MI.IMPIANTI.ID_EDIFICIO AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID UNION SELECT  INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, INDIRIZZI.CAP, INDIRIZZI.LOCALITA FROM SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.IMPIANTI.ID= " & Me.cmbImpianti.SelectedValue & " AND COMPLESSI_IMMOBILIARI.ID = SISCOM_MI.IMPIANTI.ID_COMPLESSO AND COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            Me.LblRiepilogo.Text = "IMPIANTO SITO IN " & par.IfNull(myReader1("INDIRIZZO"), " - - ") & ", CIVICO " & par.IfNull(myReader1("CIVICO"), " - - ") & ", LOCALITA " & par.IfNull(myReader1("LOCALITA"), " - - ") & ", CAP " & par.IfNull(myReader1("CAP"), " - - ")
                        End If

                End Select

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Filtrainterventi()
                Me.cmbTipoIntervento.SelectedValue = IDINTERVENTO
                Me.cmbUnitaImmob.Enabled = False
                Me.cmbComplesso.Enabled = False
                Me.DrLEdificio.Enabled = False
                Me.cmbUnitaComune.Enabled = False
                Me.cmbImpianti.Enabled = False
                Me.LBLINSCOMP.Enabled = False
                Me.LBLINSEDIFICIO.Enabled = False
                Me.LBLINSUNICOM.Enabled = False
                Me.LBLINSUNIIMMOB.Enabled = False
                Me.lblImpianti.Enabled = False
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()

        End Try
    End Sub
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

    'Private Sub ApriCorrelateEdifici()

    '    'CaricaListBox()
    '    'Try
    '    '    If par.OracleConn.State = Data.ConnectionState.Closed Then
    '    '        par.OracleConn.Open()
    '    '        par.SettaCommand(par)
    '    '    End If

    '    '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.INT_MANUTENZIONE_EDIFICI WHERE ID_INTERVENTO =" & vId
    '    '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '    '    While myReader.Read
    '    '        Me.ListEdifci.Items.FindByValue(myReader.Item("ID_EDIFICIO")).Selected = True
    '    '        Selezionati = 1
    '    '    End While
    '    '    myReader.Close()

    '    '    par.OracleConn.Close()
    '    'Catch ex As Exception
    '    '    Me.lblErrore.Visible = True
    '    '    lblErrore.Text = ex.Message
    '    'End Try

    'End Sub
    Private Sub ApriMillesimaliCorr()
        CaricaTabMillesimali()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            par.OracleConn.Close()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Private Sub CaricaInterventi()
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
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_INTERVENTI_MANU"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1 = par.cmd.ExecuteReader
            cmbTipoIntervento.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoIntervento.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            cmbTipoIntervento.Text = "-1"
            myReader1.Close()
            par.OracleConn.Close()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
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

    Private Sub RiempiCampi()

        Try


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






                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPOLOGIA_SERVIZI_MANU"
                myReader1 = par.cmd.ExecuteReader
                cmbTipoServizio.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbTipoServizio.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
                cmbTipoServizio.Text = "-1"
                myReader1.Close()


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ARTICOLI_MANU"
                myReader1 = par.cmd.ExecuteReader
                cmbArticolo.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbArticolo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), -1)))
                End While
                cmbArticolo.Text = "-1"
                myReader1.Close()
            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
            End Try

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()            'CaricaInterventi()
            CaricaEdifici()

        Catch ex As Exception

            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try

    End Sub
    Private Sub CaricaEdifici()

        '*********************APERTURA CONNESSIONE**********************
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
            par.OracleConn.Close()
        End Try
    End Sub
    Private Sub CaricaUnitaImmob()

        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
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

        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
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
        If Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "UNI_IMMOB" Then
            FiltraEdifici()
            Me.cmbUnitaImmob.Items.Add(New ListItem(" SELEZIONARE UN EDIFICIO ", -1))
            FiltraUnitaComuni()
        ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "UNI_COM" Then
            FiltraEdifici()
            FiltraUnitaComuni()
        ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "IMPIANTI" Then
            FiltraEdifici()
            FiltraImpianti()
        ElseIf Me.cmbComplesso.SelectedValue <> "-1" And TipoImmobile = "EDIF" Then
            FiltraEdifici()
        ElseIf Me.cmbUnitaComune.SelectedValue <> "-1" And TipoImmobile = "COMP" Then
            CaricaTabMillesimali()

            '*****Sub per caricare la listbox degli edifici associati, è STATA COMMENTATA PER CARICARE LE TABELLE MILLESIMALI 25/06/2009******
            'CaricaListBox()
        Else
            Me.cmbUnitaComune.Items.Clear()
            CaricaEdifici()
        End If

    End Sub
    '********************LISTBOX PER CARICARE GLI EDIFICI ASSOCIATI AL COMPLESSO*******************
    ''Private Sub CaricaListBox()
    ''    Try
    ''        If Me.cmbComplesso.SelectedValue <> "-1" And Me.TipoImmobile = "COMP" Then
    ''            If par.OracleConn.State = Data.ConnectionState.Closed Then
    ''                par.OracleConn.Open()
    ''                par.SettaCommand(par)
    ''            End If
    ''            Me.ListEdifci.Items.Clear()
    ''            par.cmd.CommandText = "select  edifici.id  ,('COD. '||edifici.cod_edificio ||' - - '||edifici.denominazione) as DESCRIZIONE from SISCOM_MI.edifici where edifici.id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " order by edifici.cod_edificio asc"
    ''            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    ''            While myReader.Read
    ''                ListEdifci.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
    ''            End While
    ''            myReader.Close()
    ''            If Me.ListEdifci.Items.Count > 0 Then
    ''                Me.LblEdificiAssociati.Visible = True
    ''                Me.btnSelezionaTutto.Visible = True
    ''            Else
    ''                Me.LblEdificiAssociati.Visible = False
    ''                Me.btnSelezionaTutto.Visible = False

    ''            End If
    ''            myReader.Close()

    ''        Else
    ''            Me.ListEdifci.Items.Clear()
    ''            Me.LblEdificiAssociati.Visible = False
    ''            Me.btnSelezionaTutto.Visible = False
    ''        End If
    ''        '300000046
    ''        par.OracleConn.Close()
    ''        '& Me.DLRComplessi.SelectedValue.ToString & 
    ''    Catch ex As Exception
    ''        Me.LblErrore.Visible = True
    ''        LblErrore.Text = ex.Message
    ''    End Try
    ''End Sub
    Private Sub CaricaTabMillesimali()
        Try
            If (Me.cmbComplesso.SelectedValue <> "-1" And Me.TipoImmobile = "COMP") Or (Me.DrLEdificio.SelectedValue <> "-1" And Me.TipoImmobile = "EDIF") Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.RdbMillesimali.Items.Clear()
                If Me.TipoImmobile = "COMP" Then
                    par.cmd.CommandText = "SELECT TABELLE_MILLESIMALI.ID,TABELLE_MILLESIMALI.COD_TIPOLOGIA,TIPOLOGIA_MILLESIMALE.DESCRIZIONE, TABELLE_MILLESIMALI.DESCRIZIONE_TABELLA  FROM SISCOM_MI.TABELLE_MILLESIMALI, SISCOM_MI.TIPOLOGIA_MILLESIMALE WHERE ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " AND TABELLE_MILLESIMALI.COD_TIPOLOGIA=TIPOLOGIA_MILLESIMALE.COD"
                ElseIf TipoImmobile = "EDIF" Then
                    'par.cmd.CommandText = "SELECT DISTINCT TABELLE_MILLESIMALI.ID,(TABELLE_MILLESIMALI.COD_TIPOLOGIA||' - - '||TABELLE_MILLESIMALI.DESCRIZIONE||' - - '||TABELLE_MILLESIMALI.DESCRIZIONE_TABELLA||' - - '||EDIFICI.COD_EDIFICIO||' - - '||EDIFICI.denominazione) AS DESCRIZIONE FROM SISCOM_MI.VALORI_MILLESIMALI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TABELLE_MILLESIMALI,SISCOM_MI.EDIFICI WHERE VALORI_MILLESIMALI.ID_unita_immobiliare=UNITA_IMMOBILIARI.ID AND VALORI_MILLESIMALI.id_tabella=TABELLE_MILLESIMALI.ID AND UNITA_IMMOBILIARI.id_Edificio=" & Me.DrLEdificio.SelectedValue.ToString & " AND EDIFICI.ID=TABELLE_MILLESIMALI.id_edificio"
                    par.cmd.CommandText = "SELECT DISTINCT TABELLE_MILLESIMALI.ID,TABELLE_MILLESIMALI.COD_TIPOLOGIA,TABELLE_MILLESIMALI.DESCRIZIONE , TABELLE_MILLESIMALI.DESCRIZIONE_TABELLA , EDIFICI.COD_EDIFICIO , EDIFICI.denominazione FROM SISCOM_MI.VALORI_MILLESIMALI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TABELLE_MILLESIMALI,SISCOM_MI.EDIFICI WHERE VALORI_MILLESIMALI.ID_unita_immobiliare=UNITA_IMMOBILIARI.ID AND VALORI_MILLESIMALI.id_tabella=TABELLE_MILLESIMALI.ID AND UNITA_IMMOBILIARI.id_Edificio=" & Me.DrLEdificio.SelectedValue.ToString & " AND EDIFICI.ID=TABELLE_MILLESIMALI.id_edificio"

                End If
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader.Read
                '    RdbMillesimali.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
                'End While

                If Me.TipoImmobile = "COMP" Then
                    While myReader.Read

                        RdbMillesimali.Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COD_TIPOLOGIA"), ""), 14) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE_TABELLA"), ""), 50), par.IfNull(myReader("ID"), -1)))
                    End While
                Else
                    While myReader.Read
                        RdbMillesimali.Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader("COD_TIPOLOGIA"), ""), 14) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE_TABELLA"), ""), 50) & " " & par.MiaFormat(par.IfNull(myReader("COD_EDIFICIO"), ""), 10) & "&nbsp;&nbsp;&nbsp;&nbsp;" & par.MiaFormat(par.IfNull(myReader("DENOMINAZIONE"), ""), 35), par.IfNull(myReader("ID"), -1)))
                    End While
                End If

                myReader.Close()
                If Me.cmbReversibile.SelectedValue = 1 Then
                    Me.RdbMillesimali.Visible = True
                    lblTitle.Visible = True
                    Me.btnDettaglio.Visible = True
                Else
                    Me.RdbMillesimali.Visible = False
                    Me.btnDettaglio.Visible = False
                    lblTitle.Visible = False


                End If

                par.OracleConn.Close()
            Else
                Me.RdbMillesimali.Items.Clear()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try




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

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
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
                par.OracleConn.Close()
            End Try
        End If
    End Sub
    Private Sub FiltraImpianti()
        Try


            If Me.DrLEdificio.SelectedValue = "-1" And Me.cmbComplesso.SelectedValue <> "-1" Then
                '****************************FILTRO IMPIANTI A PARTIRE DA ID COMPLESSO

                '*********************APERTURA CONNESSIONE**********************
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


            ElseIf Me.DrLEdificio.SelectedValue <> "-1" Then
                '****************************FILTRO IMPIANTI A PARTIRE DA ID EDIFICIO
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.cmbImpianti.Items.Clear()
                Me.cmbImpianti.Items.Add(New ListItem("", -1))

                par.cmd.CommandText = "SELECT DISTINCT IMPIANTI.ID, IMPIANTI.COD_IMPIANTO, IMPIANTI.DESCRIZIONE, TIPOLOGIA_IMPIANTI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.IMPIANTI, SISCOM_MI.TIPOLOGIA_IMPIANTI WHERE TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA AND ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToString
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

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If vId = 0 Then
            Me.salva()
        Else
            Update()
        End If

    End Sub
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
            If txtDatFine.Text <> "" Then

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

                Select Case TipoImmobile
                    Case "COMP"
                        If Me.cmbComplesso.SelectedValue <> -1 AndAlso Me.cmbComplesso.SelectedValue.ToString <> "" Then
                            par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_INTERVENTI_MANUTENZIONE.NEXTVAL FROM dual "
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                vId = myReader1(0)
                            End If
                            myReader1.Close()
                            par.cmd.CommandText = ""
                            If Me.cmbReversibile.SelectedValue = 1 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_COMPLESSO, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA, ID_TABELLA_MILLESIMALE)" _
                                & "VALUES (" & vId & ", " & Me.cmbComplesso.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.IfEmpty(par.VirgoleInPunti(Me.txtCosto.Text), "Null") & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null,'" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "'," & par.IfEmpty(RdbMillesimali.SelectedValue.ToString, "Null") & " )"
                            Else

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_COMPLESSO, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                                & "VALUES (" & vId & ", " & Me.cmbComplesso.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.IfEmpty(par.VirgoleInPunti(Me.txtCosto.Text), "Null") & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null,'" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "')"
                            End If
                            par.cmd.ExecuteNonQuery()

                            ' '' ''++++++++CHECk EDIFICI ASSOCIATI è STATA TOLTA PER ASSOCIARE LE TABELLE MILLESIMALI 25/06/2009******
                            ''If Me.ListEdifci.Items.Count > 0 Then

                            ''    For Each o As Object In ListEdifci.Items
                            ''        Dim item As System.Web.UI.WebControls.ListItem
                            ''        item = CType(o, System.Web.UI.WebControls.ListItem)
                            ''        If item.Selected Then
                            ''            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INT_MANUTENZIONE_EDIFICI (ID_INTERVENTO,ID_EDIFICIO ) VALUES (" & vId & "," & item.Value & ")"
                            ''            par.cmd.ExecuteNonQuery()
                            ''        End If
                            ''    Next
                            ''End If
                            ' '' ''+++++++++fine nuova check

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere un Complesso Immobiliare!');</SCRIPT>")
                            Exit Sub

                        End If
                    Case "EDIF"
                        If Me.DrLEdificio.SelectedValue <> -1 AndAlso Me.DrLEdificio.SelectedValue.ToString <> "" Then
                            par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_INTERVENTI_MANUTENZIONE.NEXTVAL FROM dual "
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                vId = myReader1(0)
                            End If
                            myReader1.Close()
                            par.cmd.CommandText = ""
                            If Me.cmbReversibile.SelectedValue = 0 Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_EDIFICIO, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                                & "VALUES (" & vId & ", " & Me.DrLEdificio.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            Else
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_EDIFICIO, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA, ID_TABELLA_MILLESIMALE)" _
                                & "VALUES (" & vId & ", " & Me.DrLEdificio.SelectedValue.ToString & ", " _
                                & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                                & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "', " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "'," & par.IfEmpty(RdbMillesimali.SelectedValue.ToString, "Null") & " )"

                            End If
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('scegliere un Edifcio!');</SCRIPT>")
                            Exit Sub

                        End If
                    Case "UNI_COM"
                        If Me.cmbUnitaComune.SelectedValue <> "" And Me.cmbUnitaComune.Items.Count > 0 Then
                            par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_INTERVENTI_MANUTENZIONE.NEXTVAL FROM dual "
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                vId = myReader1(0)
                            End If
                            myReader1.Close()
                            par.cmd.CommandText = ""
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_UNITA_COMUNE, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                            & "VALUES (" & vId & ", " & Me.cmbUnitaComune.SelectedValue.ToString & ", " _
                            & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                            & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "' , " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere una Unità Comune!');</SCRIPT>")
                            Exit Sub

                        End If
                    Case "UNI_IMMOB"
                        If Me.cmbUnitaImmob.SelectedValue <> "" AndAlso Me.cmbUnitaImmob.Items.Count > 0 Then
                            par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_INTERVENTI_MANUTENZIONE.NEXTVAL FROM dual "
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                vId = myReader1(0)
                            End If
                            myReader1.Close()
                            par.cmd.CommandText = ""
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_UNITA_IMMOBILIARE, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                            & "VALUES (" & vId & ", " & Me.cmbUnitaImmob.SelectedValue.ToString & ", " _
                            & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                            & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "' , " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            par.cmd.ExecuteNonQuery()

                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere una Unità Immobiliare!');</SCRIPT>")
                            Exit Sub

                        End If
                    Case "IMPIANTI"
                        If Me.cmbImpianti.SelectedValue <> "" AndAlso Me.cmbImpianti.Items.Count <> 0 Then
                            par.cmd.CommandText = " SELECT SISCOM_MI.SEQ_INTERVENTI_MANUTENZIONE.NEXTVAL FROM dual "
                            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1.Read Then
                                vId = myReader1(0)
                            End If
                            myReader1.Close()
                            par.cmd.CommandText = ""
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INTERVENTI_MANUTENZIONE (ID, ID_IMPIANTO, ID_TIPO_INTERVENTO, ID_TIPO_SERVIZIO, ID_ARTICOLO, DESCRIZIONE, COSTO, DATA_ORDINE, DATA_INIZIO_INTERVENTO, DATA_FINE_INTERVENTO, REVERSIBILE, COSTO_REVERSIBILE, ID_TABELLA, NUM_DOCUMENTO, NUM_FATTURA)" _
                            & "VALUES (" & vId & ", " & Me.cmbImpianti.SelectedValue.ToString & ", " _
                            & "" & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & ", '" & par.PulisciStrSql(Me.txtNote.Text) & "', " & par.VirgoleInPunti(Me.txtCosto.Text) & ",'" & par.AggiustaData(Me.txtDataOrdine.Text) & "'," _
                            & "'" & par.AggiustaData(Me.TxtDataInizio.Text) & "', '" & par.AggiustaData(Me.txtDatFine.Text) & "' , " & Me.cmbReversibile.SelectedValue.ToString & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", Null, '" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', '" & par.PulisciStrSql(Me.txtNumFattura.Text) & "' )"
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = ""
                            Response.Write("<SCRIPT>alert('Scegliere un Ipianto!');</SCRIPT>")
                            Exit Sub

                        End If

                End Select



                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Me.LBLINSCOMP.Enabled = False
                Me.LBLINSEDIFICIO.Enabled = False
                Me.LBLINSUNICOM.Enabled = False
                Me.LBLINSUNIIMMOB.Enabled = False
                Me.cmbComplesso.Enabled = False
                Me.DrLEdificio.Enabled = False
                Me.cmbUnitaImmob.Enabled = False
                Me.cmbUnitaComune.Enabled = False
                Me.lblImpianti.Enabled = False
                Me.cmbImpianti.Enabled = False

            Else
                Response.Write("<SCRIPT>alert('Il campo COSTO deve essere avvalorato!');</SCRIPT>")

            End If

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
                If CDbl(par.IfEmpty(Me.txtCostoRevers.Text, "0")) > CDbl(txtCosto.Text) Then
                    Response.Write("<SCRIPT>alert('Il Costo Reversibile deve essere minore del Costo!');</SCRIPT>")
                    Exit Sub
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
                Else : IdMillesimo = par.IfEmpty(Me.RdbMillesimali.SelectedValue.ToString, "Null")
                End If
                par.cmd.CommandText = "UPDATE SISCOM_MI.INTERVENTI_MANUTENZIONE  SET ID_TIPO_INTERVENTO = " & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue.ToString) & " , ID_TIPO_SERVIZIO = " & RitornaNullSeMenoUno(Me.cmbTipoServizio.SelectedValue.ToString) & ", ID_ARTICOLO = " & RitornaNullSeMenoUno(Me.cmbArticolo.SelectedValue.ToString) & " , DESCRIZIONE = '" & par.PulisciStrSql(Me.txtNote.Text) & "', COSTO = " & par.VirgoleInPunti(Me.txtCosto.Text) & ", DATA_ORDINE = '" & par.AggiustaData(Me.txtDataOrdine.Text) & "', DATA_INIZIO_INTERVENTO = '" & par.AggiustaData(Me.TxtDataInizio.Text) & "', DATA_FINE_INTERVENTO = '" & par.AggiustaData(Me.txtDatFine.Text) & "',REVERSIBILE=" & Me.cmbReversibile.SelectedValue.ToString & ", COSTO_REVERSIBILE=" & par.IfEmpty(par.VirgoleInPunti(Me.txtCostoRevers.Text), "Null") & ", ID_TABELLA=Null, NUM_DOCUMENTO='" & par.PulisciStrSql(Me.txtNumDoc.Text) & "', NUM_FATTURA='" & par.PulisciStrSql(Me.txtNumFattura.Text) & "',ID_TABELLA_MILLESIMALE=" & IdMillesimo & " WHERE ID = " & vId
                par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = "DELETE FROM SISCOM_MI.INT_MANUTENZIONE_EDIFICI WHERE ID_INTERVENTO = " & vId
                'par.cmd.ExecuteNonQuery()
                ''++++++++NUOVA CHECk+++++++++++++++
                'If Me.ListEdifci.Items.Count > 0 Then

                '    For Each o As Object In ListEdifci.Items
                '        Dim item As System.Web.UI.WebControls.ListItem
                '        item = CType(o, System.Web.UI.WebControls.ListItem)
                '        If item.Selected Then
                '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.INT_MANUTENZIONE_EDIFICI (ID_INTERVENTO,ID_EDIFICIO ) VALUES (" & vId & "," & item.Value & ")"
                '            par.cmd.ExecuteNonQuery()
                '        End If
                '    Next
                'End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            Else
                Response.Write("<SCRIPT>alert('ATTENZIONE! Controllare i dati.');</SCRIPT>")

            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try

    End Sub

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

    Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
        If Me.DrLEdificio.SelectedValue <> "-1" Then
            CaricaTabMillesimali()
            FiltraUnitaComuni()
            FiltraUnitaImmob()
            FiltraImpianti()
        ElseIf Me.DrLEdificio.SelectedValue = "-1" And TipoImmobile = "UNI_IMMOB" Then
            Me.cmbUnitaImmob.Items.Clear()
            Me.cmbUnitaImmob.Items.Add(New ListItem(" SELEZIONARE UN EDIFICIO ", -1))
            If Me.cmbComplesso.SelectedValue = "-1" Then
                Me.cmbUnitaComune.Items.Clear()
            Else
                FiltraUnitaComuni()
            End If
        ElseIf Me.DrLEdificio.SelectedValue = "-1" And TipoImmobile = "IMPIANTI" Then
            FiltraImpianti()
        Else
            Me.RdbMillesimali.Items.Clear()
            Me.lblMillesimali.Visible = False
            Me.btnDettaglio.Visible = False
            Me.RdbMillesimali.Visible = False
            Me.lblTitle.Visible = False
        End If
    End Sub

    Protected Sub imgUscita_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUscita.Click
        Response.Write("<script>document.location.href=""pagina_home_Interventi.aspx""</script>")
        Session.Item("LAVORAZIONE") = 0
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
    Protected Sub cmbTipoServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoServizio.SelectedIndexChanged
        Me.Filtrainterventi()
    End Sub
    Private Sub Filtrainterventi()

        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
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


    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbReversibile.SelectedIndexChanged
        If Me.cmbReversibile.SelectedValue = 1 Then
            Me.lblRevers.Visible = True
            Me.txtCostoRevers.Visible = True
            Me.LBLeuro.Visible = True
            Me.txtCostoRevers.Text = ""
            ' Me.RdbMillesimali.Visible = True
            If Me.RdbMillesimali.Items.Count > 0 Then
                Me.lblMillesimali.Visible = True
                Me.btnDettaglio.Visible = True
                Me.RdbMillesimali.Visible = True
                lblTitle.Visible = True
            End If
        Else
            Me.lblRevers.Visible = False
            Me.txtCostoRevers.Visible = False
            Me.LBLeuro.Visible = False

            Me.txtCostoRevers.Text = ""
            Me.RdbMillesimali.Visible = False
            Me.lblMillesimali.Visible = False
            Me.btnDettaglio.Visible = False
            Me.lblTitle.Visible = False
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



    Protected Sub RdbMillesimali_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbMillesimali.SelectedIndexChanged
        vIdMillesimo = Me.RdbMillesimali.SelectedValue.ToString
    End Sub

    Protected Sub btnDettaglio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDettaglio.Click
        If Me.RdbMillesimali.Items.Count > 0 AndAlso Me.RdbMillesimali.SelectedValue <> "" Then
            Response.Write("<script>window.open('DettaglioEdificio.aspx?ID=" & RdbMillesimali.SelectedValue.ToString & "','VARIAZIONI', 'resizable=no, width=550, height=250');</script>")

        End If
    End Sub


End Class
