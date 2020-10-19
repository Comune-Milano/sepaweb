
Partial Class GestioneAutonoma_TabModelliBC
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Public Property vIdModB() As String
        Get
            If Not (ViewState("par_vIdModB") Is Nothing) Then
                Return CStr(ViewState("par_vIdModB"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdModB") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Controllo modifica campi nel form
                Dim CTRL As Control
                For Each CTRL In Me.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    End If
                Next
                'FINE DEL CICLO


                txtDataProvv.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv2.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv3.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv4.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv5.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv6.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtScadSrv7.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                Me.txtDecorrenza.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                Me.txtFirmatari.Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this);")

                RiempiCampi()
                If CType(Me.Page, Object).vIdGestAutonoma <> "" Then
                    Cerca()
                End If
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabModB"

        End Try

    End Sub
    Public Sub RiempiCampi()
        Try
            If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Or DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue <> "-1" Then
                'LISTA DELLO STATO DELL SERVIZIO RICHIESTO
                Me.cmbStatoAutor.Items.Clear()
                Me.cmbStatoAutor.Items.Add(New ListItem(" ", -1))
                Me.cmbStatoAutor.Items.Add(New ListItem("AUTORIZZATA", 1))
                Me.cmbStatoAutor.Items.Add(New ListItem("RESPINTA", 0))
                Me.cmbStatoAutor.SelectedValue = "-1"

                Me.CaricChkListServizi()

                Me.lblAffitto.Text = DirectCast(Me.Page.FindControl("lblBollettato"), Label).Text
                Me.lblSpese.Text = "0,00"
                Me.lblCompetenze.Text = (Me.lblAffitto.Text + Me.lblSpese.Text)
                Me.lblRifFinanz.Text = Format(Now, "dd/MM/yyyy")
                Me.lblPercAbusive.Text = DirectCast(Me.Page.FindControl("lblPercAbusiv"), Label).Text
                Me.lblPercMor.Text = DirectCast(Me.Page.FindControl("lblPercentuale"), Label).Text

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                If Not (CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                End If
                'CALCOLO NUMERO E MQ ALLOGGI
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL'AND EDIFICI.ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL'AND EDIFICI.ID_COMPLESSO =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    Me.lblAlloggi.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblAlloggi.Text = 0
                End If
                'INQUILINI E LBL AFFITTUARI
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE WHERE (SISCOM_MI.GETSTATOCONTRATTO(SISCOM_MI.RAPPORTI_UTENZA.ID)='IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(SISCOM_MI.RAPPORTI_UTENZA.ID)='IN CORSO (S.T.)') AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_EDIFICIO=" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT COUNT(RAPPORTI_UTENZA.ID) FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI WHERE (SISCOM_MI.GETSTATOCONTRATTO(SISCOM_MI.RAPPORTI_UTENZA.ID)='IN CORSO' OR SISCOM_MI.GETSTATOCONTRATTO(SISCOM_MI.RAPPORTI_UTENZA.ID)='IN CORSO (S.T.)') AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_EDIFICIO=EDIFICI.ID AND EDIFICI.ID_COMPLESSO = " & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    txtInquilini.Text = par.IfEmpty(myReader(0), 0)
                End If
                Me.lblAffittuari.Text = Me.txtInquilini.Text


                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL'AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'AL'AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.lblMqAlloggi.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblMqAlloggi.Text = 0
                End If

                'CALCOLO NUMERO E MQ NEGOZI
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N'AND EDIFICI.ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N'AND EDIFICI.ID_COMPLESSO =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.lblNegozi.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblNegozi.Text = 0
                End If

                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N'AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = 'N'AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.lblMqNegozi.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblMqNegozi.Text = 0
                End If

                'CALCOLO NUMERO E MQ DIVERSI
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'N' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' AND EDIFICI.ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT COUNT(UNITA_IMMOBILIARI.ID) AS NUM_ALLOGGI FROM SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'N' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' AND EDIFICI.ID_COMPLESSO =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.lblDiversi.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblDiversi.Text = 0
                End If
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'N' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT SUM(VALORE) AS MQ_ALLOGGI FROM SISCOM_MI.DIMENSIONI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'N' AND UNITA_IMMOBILIARI.COD_TIPOLOGIA <> 'AL' AND DIMENSIONI.COD_TIPOLOGIA = 'SUP_NETTA' AND DIMENSIONI.ID_UNITA_IMMOBILIARE = UNITA_IMMOBILIARI.ID AND EDIFICI.ID_COMPLESSO =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    Me.lblMqDiversi.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblMqDiversi.Text = 0
                End If
                'NUMERO OCC. ABUSIVE
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(ID_UNITA) AS SENZA_TITOLO FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID= UNITA_CONTRATTUALE.ID_EDIFICIO AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = 'NONE' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID = " & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT COUNT(ID_UNITA) AS SENZA_TITOLO FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID= UNITA_CONTRATTUALE.ID_EDIFICIO AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC = 'NONE' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID_COMPLESSO = " & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    Me.lblOccAbus.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblOccAbus.Text = 0
                End If
                'NUMERO SENZA TITOLO
                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(ID_UNITA) AS O_A FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID= UNITA_CONTRATTUALE.ID_EDIFICIO AND SISCOM_MI.Getstatocontratto(RAPPORTI_UTENZA.ID)='IN CORSO S.T.' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID = " & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT COUNT(ID_UNITA) AS O_A FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI, SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID= UNITA_CONTRATTUALE.ID_EDIFICIO AND SISCOM_MI.Getstatocontratto(RAPPORTI_UTENZA.ID)='IN CORSO S.T.' AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID_COMPLESSO = " & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    Me.lblSenzTitle.Text = par.IfNull(myReader(0), 0)
                Else
                    Me.lblSenzTitle.Text = 0
                End If

                'PROPRIETARI SI O PROPRIETARI NO

                If DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT DECODE(COD_LIVELLO_POSSESSO,'FABBRICATO','NO','SI') AS PROPRIETARI FROM SISCOM_MI.EDIFICI WHERE ID =" & DirectCast(Me.Page.FindControl("cmbEdificio"), DropDownList).SelectedValue
                Else
                    par.cmd.CommandText = "SELECT DECODE(COD_LIVELLO_POSSESSO,'COMPLESSO','NO','SI') AS PROPRIETARI FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID =" & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
                End If
                myReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    Me.lblPropriet.Text = myReader(0)
                End If


            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabModB"
        End Try

    End Sub
    Public Sub CaricChkListServizi()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If Not (CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If

            Me.chkListServizi.Items.Clear()
            'CARICA LA LISTA DELLE CHECKBOX
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AUTOGESTIONI_TAB_SERVIZI ORDER BY ID ASC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                chkListServizi.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            myReader.Close()

            'RIABILITO TUTTI I CAMBI DEL DIV PROVVEDIMENTO
            Me.chkListServizi.Enabled = True
            Me.chkListServizi.ForeColor = Drawing.Color.Black
            Me.cmbStatoAutor.Enabled = True
            Dim Ctrl As Control
            For Each Ctrl In Me.Controls
                If TypeOf Ctrl Is TextBox Then
                    DirectCast(Ctrl, TextBox).Enabled = True
                End If
            Next

            If CType(Me.Page, Object).vIdGestAutonoma <> "" Then


                'SE IDMODB = 0 SIGNIFICA CHE è UN INSERIMENTO O UNA AGGIUNTA DI UN MODELLO B 
                If Me.txtidModB.Value = 0 Then

                    par.cmd.CommandText = "SELECT ID_SERVIZIO,DATA_SCADENZA FROM SISCOM_MI.AUTOGESTIONI_SERVIZI,SISCOM_MI.AUTOGESTIONI_PROV WHERE ID_MOD_B = AUTOGESTIONI_PROV.ID AND AUTOGESTIONI_PROV.ID_AUTOGESTIONE = " & CType(Me.Page, Object).vIdGestAutonoma
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        chkListServizi.Items(myReader("ID_SERVIZIO") - 1).Selected = True
                        If txtidModB.Value = 0 Or Me.cmbStatoAutor.SelectedValue = 1 Then
                            chkListServizi.Items(myReader("ID_SERVIZIO") - 1).Enabled = False
                            Select Case myReader("ID_SERVIZIO") - 1
                                Case 0
                                    Me.txtScadSrv1.Enabled = False
                                    Me.txtScadSrv1.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))
                                Case 1
                                    Me.txtScadSrv2.Enabled = False
                                    Me.txtScadSrv2.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                                Case 2
                                    Me.txtScadSrv3.Enabled = False
                                    Me.txtScadSrv3.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                                Case 3
                                    Me.txtScadSrv4.Enabled = False
                                    Me.txtScadSrv4.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                                Case 4
                                    Me.txtScadSrv5.Enabled = False
                                    Me.txtScadSrv5.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                                Case 5
                                    Me.txtScadSrv6.Enabled = False
                                    Me.txtScadSrv6.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                                Case 6
                                    Me.txtScadSrv7.Enabled = False
                                    Me.txtScadSrv7.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                            End Select
                        Else
                            chkListServizi.Items(myReader("ID_SERVIZIO") - 1).Enabled = True
                            Select Case myReader("ID_SERVIZIO") - 1
                                Case 0
                                    Me.txtScadSrv1.Enabled = True
                                Case 1
                                    Me.txtScadSrv2.Enabled = True
                                Case 2
                                    Me.txtScadSrv3.Enabled = True
                                Case 3
                                    Me.txtScadSrv4.Enabled = True
                                Case 4
                                    Me.txtScadSrv5.Enabled = True
                                Case 5
                                    Me.txtScadSrv6.Enabled = True
                                Case 6
                                    Me.txtScadSrv7.Enabled = True
                            End Select
                        End If
                    Loop

                Else
                    'SE IDMODB è DIVERSO DA 0 ALLORA SIGNIFICA CHE SI VUOLE MODIFICARE UN MODELLO B E I SERVIZI AD ESSO ASSOCIATO
                    par.cmd.CommandText = "SELECT ID_SERVIZIO FROM SISCOM_MI.AUTOGESTIONI_SERVIZI,SISCOM_MI.AUTOGESTIONI_PROV WHERE ID_MOD_B = AUTOGESTIONI_PROV.ID AND AUTOGESTIONI_PROV.ID_AUTOGESTIONE = " & CType(Me.Page, Object).vIdGestAutonoma & " AND ID_MOD_B = " & txtidModB.Value
                    myReader = par.cmd.ExecuteReader()

                    Do While myReader.Read
                        chkListServizi.Items(myReader("ID_SERVIZIO") - 1).Selected = True
                        Select Case myReader("ID_SERVIZIO") - 1
                            Case 0
                                Me.txtScadSrv1.Enabled = True
                            Case 1
                                Me.txtScadSrv2.Enabled = True
                            Case 2
                                Me.txtScadSrv3.Enabled = True
                            Case 3
                                Me.txtScadSrv4.Enabled = True
                            Case 4
                                Me.txtScadSrv5.Enabled = True
                            Case 5
                                Me.txtScadSrv6.Enabled = True
                            Case 6
                                Me.txtScadSrv7.Enabled = True
                        End Select

                    Loop

                    par.cmd.CommandText = "SELECT ID_SERVIZIO, DATA_SCADENZA FROM SISCOM_MI.AUTOGESTIONI_SERVIZI,SISCOM_MI.AUTOGESTIONI_PROV WHERE ID_MOD_B = AUTOGESTIONI_PROV.ID AND AUTOGESTIONI_PROV.ID_AUTOGESTIONE = " & CType(Me.Page, Object).vIdGestAutonoma & " AND ID_MOD_B <> " & txtidModB.Value
                    myReader = par.cmd.ExecuteReader()
                    Do While myReader.Read
                        chkListServizi.Items(myReader("ID_SERVIZIO") - 1).Enabled = False
                        Select Case myReader("ID_SERVIZIO") - 1
                            Case 0
                                Me.txtScadSrv1.Enabled = False
                                Me.txtScadSrv1.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))
                            Case 1
                                Me.txtScadSrv2.Enabled = False
                                Me.txtScadSrv2.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                            Case 2
                                Me.txtScadSrv3.Enabled = False
                                Me.txtScadSrv3.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                            Case 3
                                Me.txtScadSrv4.Enabled = False
                                Me.txtScadSrv4.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                            Case 4
                                Me.txtScadSrv5.Enabled = False
                                Me.txtScadSrv5.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                            Case 5
                                Me.txtScadSrv6.Enabled = False
                                Me.txtScadSrv6.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                            Case 6
                                Me.txtScadSrv7.Enabled = False
                                Me.txtScadSrv7.Text = par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), ""))

                        End Select
                    Loop




                End If

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabModB"
        End Try

    End Sub

    Protected Sub imgBtnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAnnulla.Click
        Me.TextBox2.Value = 1
        txtidModB.Value = 0
        vIdModB = ""
        Puliscicampi()
        RiempiCampi()
    End Sub

    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If vIdModB <> "" Then
            Update()
        Else
            Salva()
        End If

    End Sub
    Private Sub Salva()
        Try
            If Replace(Me.Percentuale.Value, "%", "") >= 60 Then
                'SALVO PRIMA LA GESTIONE AUTONOMA E CREO UN ID IN TRANSAZIONE
                If CType(Me.Page, Object).vIdGestAutonoma = "" Then
                    CType(Me.Page, Object).SalvaDaSottoTab()
                End If

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                'SALVO IL MODELLO B E I SERVIZI SCELTI
                'ID DEL MODELLO B
                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_AUTOGESTIONI_PROV.NEXTVAL FROM DUAL"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    vIdModB = myReader1(0).ToString
                End If
                myReader1.Close()
                Dim Propriet As Integer = 0
                If Me.lblPropriet.Text = "SI" Then
                    Propriet = 1
                End If
                'INSERIMENTO DEL MODELLO B
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_PROV" _
                & "(ID,ID_AUTOGESTIONE,NUM_PROV,DATA_PROV,DATA_DEC,FL_AUTORIZZATA,PERC_FIRMATARI,PERC_ABUSIVI,PERC_MOROSITA,NUM_ALLOGGI," _
                & "NUM_NEGOZI,NUM_DIVERSI,SUP_ALLOGGI,SUP_NEGOZI,SUP_DIVERSI,NUM_AFFITTUARI,NUM_SENZA_TITOLO,NUM_OA,NUM_FIRMATARI,FL_PROPIETARI,DATA_RIF_FINANZIARIO, MOROSITA,COMPETENZE,AFFITTO,SPESE)" _
                & "VALUES (" & vIdModB & "," & CType(Me.Page, Object).vIdGestAutonoma & ",'" & par.PulisciStrSql(Me.txtNumProvv.Text) & "'," & par.IfEmpty(par.AggiustaData(Me.txtDataProvv.Text), "Null") & "," & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & "," & Me.cmbStatoAutor.SelectedValue & "," _
                & "" & par.IfEmpty(par.VirgoleInPunti(Percentuale.Value), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Replace(Me.lblPercAbusive.Text, "%", "")), "Null") & "," & par.IfEmpty(par.VirgoleInPunti(Replace(Me.lblPercMor.Text, "%", "")), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblAlloggi.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblNegozi.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblDiversi.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblMqAlloggi.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblMqNegozi.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblMqDiversi.Text), "Null") & "," _
                & " " & par.IfEmpty(par.VirgoleInPunti(Me.lblAffittuari.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblSenzTitle.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblOccAbus.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.txtFirmatari.Text), "Null") & ", " & Propriet & ", " & par.IfEmpty(par.AggiustaData(Me.lblRifFinanz.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblMorosita.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblCompetenze.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblAffitto.Text), "Null") & ", " & par.IfEmpty(par.VirgoleInPunti(Me.lblSpese.Text), "Null") & ")"
                par.cmd.ExecuteNonQuery()

                'INSERIMENTO DEI SERVIZI RICHIESTI DAL MODELLO B NELLA LISTA DEI SERVIZI ASSOCIATI AL MODELLO
                For Each o As Object In chkListServizi.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)

                    If item.Selected And item.Enabled = True Then
                        Select Case item.Value
                            Case 1
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv1.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 2
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv2.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 3
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv3.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 4
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv4.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 5
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv5.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 6
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv6.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 7
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(par.IfEmpty(Me.txtScadSrv7.Text, "")), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                        End Select
                    End If
                Next
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_AUTOGESTIONE (ID_AUTOGESTIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdGestAutonoma & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F42','')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************
                '***************************************************************************

                'par.myTrans.Commit()
                Me.TextBox2.Value = 1
                Cerca()
                Puliscicampi()
                RiempiCampi()
                CType(Me.Page.FindControl("TabServizi1"), Object).Cerca()
            Else
                Response.Write("<script>alert('La Percentuale dei firmatari è inferiore al 60%!');</script>")
                Me.TextBox2.Value = 2
                Me.Percentuale.Value = ""

            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabModB"
        End Try

    End Sub
    Private Sub Puliscicampi()
        Dim ctrl As Control
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Text = ""
            End If
            If TypeOf ctrl Is Label Then
                DirectCast(ctrl, Label).Text = ""
            End If
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Items.Clear()
            End If
        Next
        Me.chkListServizi.Items.Clear()

    End Sub
    Public Sub Cerca()
        Try
            If Not String.IsNullOrEmpty(CType(Me.Page, Object).vIdGestAutonoma.ToString) Then
                QUERY = "SELECT ID,NUM_PROV, TO_CHAR(TO_DATE(DATA_PROV,'yyyymmdd'),'dd/mm/yyyy') AS DATA_PROV,TO_CHAR(TO_DATE(DATA_DEC,'yyyymmdd'),'dd/mm/yyyy') AS DATA_DEC, DECODE(FL_AUTORIZZATA,0,'NO',1,'SI','-1','DA DEFINIRE') AS FL_AUTORIZZATA FROM SISCOM_MI.AUTOGESTIONI_PROV WHERE ID_AUTOGESTIONE = " & CType(Me.Page, Object).vIdGestAutonoma
                BindGrid()

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message
        End Try

    End Sub
    Private Sub BindGrid()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = QUERY
            Dim da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "CONVOCAZIONI")

            DataGridModBeC.DataSource = ds
            DataGridModBeC.DataBind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
        End Try
    End Sub
    Private Property QUERY() As String
        Get
            If Not (ViewState("par_QUERY") Is Nothing) Then
                Return CStr(ViewState("par_QUERY"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_QUERY") = value
        End Set

    End Property
    Private Sub Update()
        Try
            If Replace(Me.txtPercentualeFirm.Text, "%", "") >= 60 Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
                par.cmd.CommandText = "UPDATE SISCOM_MI.AUTOGESTIONI_PROV SET NUM_PROV = '" & par.PulisciStrSql(Me.txtNumProvv.Text) & "', DATA_PROV = " & par.IfEmpty(par.AggiustaData(Me.txtDataProvv.Text), "Null") & ",DATA_DEC = " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ",FL_AUTORIZZATA= " & Me.cmbStatoAutor.SelectedValue & ", PERC_FIRMATARI= " & par.IfEmpty(par.VirgoleInPunti(Me.txtPercentualeFirm.Text), "Null") & ", NUM_FIRMATARI = " & par.IfEmpty(par.VirgoleInPunti(Me.txtFirmatari.Text), "Null") & " WHERE ID = " & vIdModB
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.AUTOGESTIONI_SERVIZI WHERE ID_AUTOGESTIONE = " & CType(Me.Page, Object).vIdGestAutonoma & " AND ID_MOD_B = " & vIdModB
                par.cmd.ExecuteNonQuery()

                'INSERIMENTO DEI SERVIZI RICHIESTI DAL MODELLO B NELLA LISTA DEI SERVIZI ASSOCIATI AL MODELLO
                For Each o As Object In chkListServizi.Items
                    Dim item As System.Web.UI.WebControls.ListItem
                    item = CType(o, System.Web.UI.WebControls.ListItem)

                    If item.Selected And item.Enabled = True Then
                        Select Case item.Value
                            Case 1
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv1.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 2
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv2.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 3
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv3.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 4
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv4.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 5
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv5.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 6
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv6.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                            Case 7
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.AUTOGESTIONI_SERVIZI (ID_AUTOGESTIONE,ID_SERVIZIO,FL_AUTORIZZATA,DATA_DECORRENZA,DATA_SCADENZA,ID_MOD_B) " _
                                & "VALUES (" & CType(Me.Page, Object).vIdGestAutonoma & ", " & item.Value & ", " & Me.cmbStatoAutor.SelectedValue & ", " & par.IfEmpty(par.AggiustaData(Me.txtDecorrenza.Text), "Null") & ", " & par.IfEmpty(par.AggiustaData(Me.txtScadSrv7.Text), "Null") & "," & vIdModB & ")"
                                par.cmd.ExecuteNonQuery()
                        End Select
                    End If
                Next
                '****************MYEVENT*****************
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_AUTOGESTIONE (ID_AUTOGESTIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & CType(Me.Page, Object).vIdGestAutonoma & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F44','')"
                par.cmd.ExecuteNonQuery()
                '*******************************FINE****************************************
                '***************************************************************************

                Me.TextBox2.Value = 1
                Cerca()
                Puliscicampi()
                RiempiCampi()
                CType(Me.Page.FindControl("TabServizi1"), Object).Cerca()

                vIdModB = ""
                Me.txtidModB.Value = "0"
            Else
                Response.Write("<script>alert('La Percentuale dei firmatari è inferiore al 60%!');</script>")
                Me.TextBox2.Value = 2

            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabModB"
        End Try

    End Sub

    Protected Sub DataGridModBeC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridModBeC.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabModelliBC1_txtmia').value='Hai selezionato il modulo con provvedimento: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('TabModelliBC1_txtidModB').value='" & e.Item.Cells(0).Text & "'")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('TabModelliBC1_txtmia').value='Hai selezionato il modulo con provvedimento: " & e.Item.Cells(1).Text.Replace("'", "\'") & "';document.getElementById('TabModelliBC1_txtidModB').value='" & e.Item.Cells(0).Text & "'")
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If Me.txtidModB.Value <> "0" Then
            Try
                Me.vIdModB = txtidModB.Value
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSGA" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.AUTOGESTIONI_PROV WHERE ID = '" & Me.txtidModB.Value & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblAlloggi.Text = par.IfNull(myReader1("NUM_ALLOGGI"), "")
                    Me.lblNegozi.Text = par.IfNull(myReader1("NUM_NEGOZI"), "")
                    Me.lblDiversi.Text = par.IfNull(myReader1("NUM_DIVERSI"), "")

                    Me.lblMqAlloggi.Text = par.IfNull(myReader1("SUP_ALLOGGI"), "")
                    Me.lblMqNegozi.Text = par.IfNull(myReader1("SUP_NEGOZI"), "")
                    Me.lblMqDiversi.Text = par.IfNull(myReader1("SUP_DIVERSI"), "")

                    Me.lblAffittuari.Text = par.IfNull(myReader1("NUM_AFFITTUARI"), "")
                    Me.lblSenzTitle.Text = par.IfNull(myReader1("NUM_SENZA_TITOLO"), "")
                    Me.lblOccAbus.Text = par.IfNull(myReader1("NUM_OA"), "")
                    Me.txtFirmatari.Text = par.IfNull(myReader1("NUM_FIRMATARI"), "")

                    Me.txtPercentualeFirm.Text = par.IfNull(myReader1("PERC_FIRMATARI"), "")
                    Me.lblPercAbusive.Text = par.IfNull(myReader1("PERC_ABUSIVI"), "")
                    Me.lblPercMor.Text = par.IfNull(myReader1("PERC_MOROSITA"), "")

                    Me.cmbStatoAutor.SelectedValue = par.IfNull(myReader1("FL_AUTORIZZATA"), "-1")
                    Me.txtDecorrenza.Text = par.FormattaData(par.IfNull(myReader1("DATA_DEC"), ""))
                    Me.txtDataProvv.Text = par.FormattaData(par.IfNull(myReader1("DATA_PROV"), ""))
                    Me.txtNumProvv.Text = par.IfNull(myReader1("NUM_PROV"), "")
                    Me.lblRifFinanz.Text = par.FormattaData(par.IfNull(myReader1("DATA_RIF_FINANZIARIO"), ""))
                    Me.lblMorosita.Text = par.IfNull(myReader1("MOROSITA"), "")
                    Me.lblCompetenze.Text = par.IfNull(myReader1("COMPETENZE"), "")
                    Me.lblAffitto.Text = par.IfNull(myReader1("AFFITTO"), "")
                    Me.lblSpese.Text = par.IfNull(myReader1("SPESE"), "")
                    If par.IfNull(myReader1("FL_PROPIETARI"), "") = 1 Then
                        Me.lblPropriet.Text = "SI"
                    Else
                        Me.lblPropriet.Text = "NO"
                    End If

                    Me.TextBox2.Value = 2
                End If
                CaricChkListServizi()
                If Me.cmbStatoAutor.SelectedValue = 1 Then
                    Me.cmbStatoAutor.Enabled = False
                    Me.chkListServizi.Enabled = False
                    Dim Ctrl As Control
                    For Each Ctrl In Me.Controls
                        If TypeOf Ctrl Is TextBox Then
                            DirectCast(Ctrl, TextBox).Enabled = False
                        End If
                    Next
                End If
            Catch ex As Exception
                CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
                CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " TabConvocazione"
            End Try

        End If
    End Sub
End Class
