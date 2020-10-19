'GESTIONE LOTTI

Imports System.Collections
Imports Telerik.Web.UI

Partial Class LOTTI_GestioneLotto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sel As Integer = 0
    Dim vista As Integer = 0
    Public sValoreFiliale As String
    Public sValoreServizi As String



    Public sValoreEsercizio As String
    Public sValoreComplesso As String
    Public formPadre As String

    Public sValoreTipoLotto As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0


        If Not IsPostBack Then
            HFGriglia.Value = tabcomplessi.ClientID
            HFAltezzaSottratta.Value = 500
            Try

                sValoreEsercizio = UCase(Request.QueryString("EF"))
                sValoreFiliale = UCase(Request.QueryString("FI"))
                sValoreComplesso = UCase(Request.QueryString("CO"))
                sValoreServizi = UCase(Request.QueryString("SE"))

                formPadre = UCase(Request.QueryString("TIPO"))

                Me.txtTIPO_LOTTO.Value = UCase(Request.QueryString("T"))


                vId = 0
                vId = Session.Item("ID")


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                End If


                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"

                SettaggioCampi()

                If vId <> 0 Then
                    Me.btnINDIETRO.Visible = True
                    VisualizzaDati()
                    txtindietro.Text = 0
                Else
                    Session.Add("ID", 0)
                    Me.btnINDIETRO.Visible = False
                    txtindietro.Text = 1
                End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

                If Session.Item("BP_LO_L") = "1" Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                End If

            Catch ex As Exception
                Me.lblErrore.Visible = True
                par.OracleConn.Close()
                lblErrore.Text = ex.Message

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

            End Try
        End If
        HiddenID.Value = vId
    End Sub

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property vId() As Long
        Get
            If Not (ViewState("par_idLotto") Is Nothing) Then
                Return CLng(ViewState("par_idLotto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idLotto") = value
        End Set

    End Property


    Private Sub SettaggioCampi()
        'CARICO COMBO 
        Dim FlagConnessione As Boolean
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
        Dim dt As New Data.DataTable()
        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            Me.cmbfiliale.Items.Clear()
            ' Me.cmbfiliale.Items.Add(New ListItem(" ", -1))

            Me.TipoStruttura.Value = ""

            If Session.Item("LIVELLO") = "1" Then
                par.cmd.CommandText = "select TAB_FILIALI.ID, NOME || ' - ' || DESCRIZIONE || ' ' || CIVICO || ' ' || LOCALITA AS test from SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI " _
                                   & " where  TAB_FILIALI.ID in (select distinct(ID_STRUTTURA) from SISCOM_MI.PF_VOCI_STRUTTURA) " _
                                   & "   and  SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID " _
                                   & " order by NOME asc"
            Else
                par.cmd.CommandText = "select TAB_FILIALI.ID, NOME || ' - ' || DESCRIZIONE || ' ' || CIVICO || ' ' || LOCALITA AS test from SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI " _
                                   & " where  TAB_FILIALI.ID=" & Session.Item("ID_STRUTTURA") _
                                   & "   and  SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID" _
                                   & " order by NOME asc"
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbfiliale, "ID", "test", True)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                '  Me.cmbfiliale.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & "  -  " & par.IfNull(myReader1("DESCRIZIONE"), "") & " " & par.IfNull(myReader1("CIVICO"), "") & " " & par.IfNull(myReader1("LOCALITA"), ""), par.IfNull(myReader1("ID"), -1)))

                If Session.Item("LIVELLO") <> "1" Then
                    Me.cmbfiliale.SelectedValue = par.IfNull(myReader1("ID"), -1)
                    Me.cmbfiliale.Enabled = False
                End If
            End While


            ' Me.lstcomplessi.Items.Clear()

            If cmbfiliale.SelectedValue <> "-1" Then 'nel caso in cui lo abbia già selezionato per un operatore
                par.cmd.CommandText = "select  TAB_FILIALI.ID_TIPO_ST,TAB_SERVIZI.ID, TAB_SERVIZI.DESCRIZIONE " _
                                   & " from SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                                   & " where SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID = " & Me.cmbfiliale.SelectedValue.ToString _
                                   & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read()
                    Me.lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                    Me.TipoStruttura.Value = par.IfNull(myReader2("ID_TIPO_ST"), "0")

                End While
                myReader2.Close()
            End If


            '******PEPPE MODIFY 21102010 NON è POSSIBILE INSERIRE UN NUOVO LOTTO QUANDO L'ESERCIZIO FINANZIARIO è NUOVO
            'ESERCIZIO FINANZIARIO
            'Dim Idcorrente As Long = par.RicavaEsercizioCorrente
            Me.cmbesercizio.Items.Clear()
            'Me.cmbesercizio.Items.Add(New ListItem(" ", -1))
            cmbesercizio.Enabled = False
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE ,PF_MAIN.ID_STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.PF_MAIN " _
                               & " where T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO "

            If vId = 0 Then
                par.cmd.CommandText = par.cmd.CommandText & "   and PF_MAIN.ID_STATO<3 "
            End If

            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY ID DESC "


            ' par.caricaComboTelerik(par.cmd.CommandText, cmbesercizio, "ID", "DESCRIZIONE", True)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                'If par.IfNull(myReader1("id_stato"), -1) <> 5 Then
                Me.cmbesercizio.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE"), " "), par.IfNull(myReader1("ID"), -1)))
                ' End If
            End While
            Me.cmbesercizio.SelectedValue = -1
            myReader1.Close()


            'TIPO IMPIANTO


            'par.cmd.CommandText = "select * from SISCOM_MI.TIPOLOGIA_IMPIANTI order by DESCRIZIONE ASC"
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbTipoImpianto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            'End While
            'myReader1.Close()
            par.caricaComboTelerik("select COD,DESCRIZIONE from SISCOM_MI.TIPOLOGIA_IMPIANTI order by DESCRIZIONE ASC", cmbTipoImpianto, "COD", "DESCRIZIONE", False)
            Dim item1 As New RadComboBoxItem
            item1.Value = -1
            item1.Text = "TUTTI"
            cmbTipoImpianto.Items.Add(item1)
            cmbTipoImpianto.ClearSelection()
            If vId = 0 Then
                ' Me.txtDescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
                '  Me.txtTIPO_LOTTO.Value = par.IfNull(myReader1("TIPO"), "E")
                ' Me.cmbfiliale.ClearSelection()
                '  Me.cmbfiliale.Items.FindByValue(par.IfNull(myReader1("ID_FILIALE"), 0)).Selected = True
                Me.cmbfiliale.Enabled = False
                ' Me.cmbtipo.ClearSelection()
                ' Me.cmbtipo.Items.FindByValue(par.IfNull(myReader1("TIPO"), "")).Selected = True
                Me.cmbTipoImpianto.SelectedValue = "-1"
                '  Me.cmbTipoImpianto.Enabled = False
                'ricarico la lista dei servizi in base alla filiale
                If Me.cmbfiliale.SelectedValue <> "-1" Then

                    Me.lstservizi.Items.Clear()
                    par.cmd.CommandText = "select  TAB_FILIALI.ID_TIPO_ST,SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE " _
                                       & " from SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                                       & " where SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO " _
                                       & "   and SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE " _
                                       & "   and SISCOM_MI.TAB_FILIALI.ID = " & Me.cmbfiliale.SelectedValue.ToString _
                                       & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"


                    myReaderT = par.cmd.ExecuteReader()
                    While myReaderT.Read()
                        Me.lstservizi.Items.Add(New ListItem(par.IfNull(myReaderT("DESCRIZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))
                        Me.TipoStruttura.Value = par.IfNull(myReaderT("ID_TIPO_ST"), "2")
                    End While
                    myReaderT.Close()

                    If lstservizi.Items.Count = 0 Then
                        Me.lblnoservizi.Visible = True
                    Else
                        Me.lblnoservizi.Visible = False
                    End If
                End If
                Dim servizi As String
                'carico tutti i complessi per filiali e servizi non assegnati oltre a quelli assegnati per questo lotto
                If Me.cmbfiliale.SelectedValue <> "-1" And Me.lstservizi.SelectedValue <> "-1" Then
                    Dim s As Integer
                    servizi = ""

                    For s = 0 To lstservizi.Items.Count - 1
                        If lstservizi.Items(s).Selected = True Then

                            If servizi = "" Then
                                servizi = lstservizi.Items(s).Value
                            Else
                                servizi = servizi & "," & lstservizi.Items(s).Value
                            End If
                        End If
                    Next

                    If servizi = "" Then
                        servizi = "-1"
                    End If

                    Dim TipoStruttura1 As String = ""

                    Select Case TipoStruttura.Value
                        Case "0"    'FILIALE AMMINISTRATIVA
                            If Me.txtTIPO_LOTTO.Value = "E" Then
                                'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                                TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                            Else
                                TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                            End If

                        Case "1"    'FILIALE TECNICA
                            If Me.txtTIPO_LOTTO.Value = "E" Then
                                'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                                TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
                            Else
                                TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
                            End If

                        Case "2"    'UFFICIO CENTRALE
                            TipoStruttura1 = ""
                    End Select










                    ' Me.lstcomplessi.Items.Clear()

                    If Me.cmbTipoImpianto.SelectedItem.Value = "-1" Then


                        If Me.txtTIPO_LOTTO.Value = "E" Then
                            par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID, " _
                                                     & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                                     & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio, '0' AS SELEZIONATO  " _
                                                 & " from SISCOM_MI.EDIFICI " _
                                                 & " where EDIFICI.ID<>1  " & TipoStruttura1 & "  " _
                                                 & " order by DENOMINAZIONE ASC"
                        Else

                            par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
                                                        & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                                        & "'' AS condominio, '0' AS SELEZIONATO " _
                                              & " from SISCOM_MI.IMPIANTI " _
                                              & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
                                              & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
                                                                         & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                         & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                         & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
                                                                         & "   and LOTTI.ID<>" & vId _
                                                                         & "   and ID_IMPIANTO IS NOT NULL " _
                                                                         & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
                                               & " order by DENOMINAZIONE ASC"

                        End If


                    Else


                        If Me.txtTIPO_LOTTO.Value = "E" Then
                            par.cmd.CommandText = "select distinct(SISCOM_MI.EDIFICI.ID)," _
                                                     & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                                     & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio,  '0' AS SELEZIONATO " _
                                                 & " from SISCOM_MI.EDIFICI " _
                                                 & " where EDIFICI.ID<>1 " & TipoStruttura1 & "  " _
                                                 & "   and EDIFICI.ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "') " _
                                                 & " order by DENOMINAZIONE ASC"



                        Else
                            par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
                                                          & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                                          & "'' AS condominio, '0' AS SELEZIONATO " _
                                                & " from SISCOM_MI.IMPIANTI " _
                                                & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
                                                & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
                                                                           & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                           & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                           & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
                                                                           & "   and LOTTI.ID<>" & vId _
                                                                           & "   and ID_IMPIANTO IS NOT NULL " _
                                                                           & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
                                                & "  and IMPIANTI.COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "' " _
                                                & " order by DENOMINAZIONE ASC"

                        End If


                    End If



                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)


                End If

                DataGridLstcomplessi.DataSource = dt
                DataGridLstcomplessi.DataBind()
                Session.Add("dtComp", dt)

                If DataGridLstcomplessi.Items.Count = 0 Then
                    Me.LblNoResult.Visible = True
                Else
                    Me.LblNoResult.Visible = False
                End If



                If Me.txtTIPO_LOTTO.Value = "E" Then



                    AddSelectedComplessi()
                Else


                    AddSelectedImpianti()

                End If


            End If









            '****************************************************************+


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        Dim dt As New Data.DataTable()

        Try
            Me.txtDescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

            Me.txtTIPO_LOTTO.Value = par.IfNull(myReader1("TIPO"), "E")

            Me.cmbfiliale.ClearSelection()
            Me.cmbfiliale.Items.FindItemByValue(par.IfNull(myReader1("ID_FILIALE"), 0)).Selected = True
            Me.cmbfiliale.Enabled = False

            Me.cmbtipo.ClearSelection()
            Me.cmbtipo.Items.FindByValue(par.IfNull(myReader1("TIPO"), "")).Selected = True

            Me.cmbesercizio.ClearSelection()
            Me.cmbesercizio.Items.FindItemByValue(par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), "")).Selected = True
            Me.cmbesercizio.Enabled = False

            ' Me.cmbTipoImpianto.ClearSelection()

            Me.cmbTipoImpianto.SelectedValue = "-1"


            '  Me.cmbTipoImpianto.Enabled = False


            'ricarico la lista dei servizi in base alla filiale
            If Me.cmbfiliale.SelectedValue <> "-1" Then

                Me.lstservizi.Items.Clear()
                par.cmd.CommandText = "select  TAB_FILIALI.ID_TIPO_ST,SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE " _
                                   & " from SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                                   & " where SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID = " & Me.cmbfiliale.SelectedValue.ToString _
                                   & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"


                myReaderT = par.cmd.ExecuteReader()
                While myReaderT.Read()
                    Me.lstservizi.Items.Add(New ListItem(par.IfNull(myReaderT("DESCRIZIONE"), " "), par.IfNull(myReaderT("ID"), -1)))
                    Me.TipoStruttura.Value = par.IfNull(myReaderT("ID_TIPO_ST"), "2")
                End While
                myReaderT.Close()

                If lstservizi.Items.Count = 0 Then
                    Me.lblnoservizi.Visible = True
                Else
                    Me.lblnoservizi.Visible = False
                End If
            End If


            'setto i SERVIZI
            par.cmd.CommandText = "select * FROM SISCOM_MI.LOTTI_SERVIZI where ID_LOTTO = " & vId
            myReaderT = par.cmd.ExecuteReader()

            While myReaderT.Read()
                If par.IfNull(myReaderT("ID_SERVIZIO"), 0) <> 0 Then 'seleziono i servizi che sono stati salvati nei lotti
                    par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_SERVIZI where SISCOM_MI.TAB_SERVIZI.ID= " & myReaderT("ID_SERVIZIO")
                    Dim myReaderT2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderT2.Read Then
                        'Me.lstservizi.Items.FindByText(par.IfNull(myReaderT2("DESCRIZIONE"), "")).Selected = True
                    End If
                    myReaderT2.Close()
                End If

            End While
            myReaderT.Close()
            '********************************

            Dim servizi As String = ""
            'carico tutti i complessi per filiali e servizi non assegnati oltre a quelli assegnati per questo lotto
            If Me.cmbfiliale.SelectedValue <> "-1" And Me.lstservizi.SelectedValue <> "-1" Then
                Dim s As Integer
                servizi = ""

                For s = 0 To lstservizi.Items.Count - 1
                    If lstservizi.Items(s).Selected = True Then

                        If servizi = "" Then
                            servizi = lstservizi.Items(s).Value
                        Else
                            servizi = servizi & "," & lstservizi.Items(s).Value
                        End If
                    End If
                Next

                If servizi = "" Then
                    servizi = "-1"
                End If

                Dim TipoStruttura1 As String = ""

                Select Case TipoStruttura.Value
                    Case "0"    'FILIALE AMMINISTRATIVA
                        If Me.txtTIPO_LOTTO.Value = "E" Then
                            'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                            TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                        Else
                            TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                        End If

                    Case "1"    'FILIALE TECNICA
                        If Me.txtTIPO_LOTTO.Value = "E" Then
                            'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                            TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
                        Else
                            TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
                        End If

                    Case "2"    'UFFICIO CENTRALE
                        TipoStruttura1 = ""
                End Select










                ' Me.lstcomplessi.Items.Clear()

                If Me.cmbTipoImpianto.SelectedItem.Value = "-1" Then


                    If Me.txtTIPO_LOTTO.Value = "E" Then
                        par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID, " _
                                                 & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                                 & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio, '0' AS SELEZIONATO  " _
                                             & " from SISCOM_MI.EDIFICI " _
                                             & " where EDIFICI.ID<>1  " & TipoStruttura1 & "  " _
                                             & " order by DENOMINAZIONE ASC"
                    Else

                        par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
                                                    & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                                    & "'' AS condominio, '0' AS SELEZIONATO " _
                                          & " from SISCOM_MI.IMPIANTI " _
                                          & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
                                          & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
                                                                     & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                     & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                     & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
                                                                     & "   and LOTTI.ID<>" & vId _
                                                                     & "   and ID_IMPIANTO IS NOT NULL " _
                                                                     & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
                                           & " order by DENOMINAZIONE ASC"

                    End If


                Else


                    If Me.txtTIPO_LOTTO.Value = "E" Then
                        par.cmd.CommandText = "select distinct(SISCOM_MI.EDIFICI.ID)," _
                                                 & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                                 & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio,  '0' AS SELEZIONATO " _
                                             & " from SISCOM_MI.EDIFICI " _
                                             & " where EDIFICI.ID<>1 " & TipoStruttura1 & "  " _
                                             & "   and EDIFICI.ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "') " _
                                             & " order by DENOMINAZIONE ASC"



                    Else
                        par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
                                                      & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                                      & "'' AS condominio, '0' AS SELEZIONATO " _
                                            & " from SISCOM_MI.IMPIANTI " _
                                            & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
                                            & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
                                                                       & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                       & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                       & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
                                                                       & "   and LOTTI.ID<>" & vId _
                                                                       & "   and ID_IMPIANTO IS NOT NULL " _
                                                                       & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
                                            & "  and IMPIANTI.COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "' " _
                                            & " order by DENOMINAZIONE ASC"

                    End If


                End If






                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)


            End If





            If ((servizi <> "-1") Or (servizi = "-1")) Then
                par.cmd.CommandText = "select * from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & vId
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtConfronto As New Data.DataTable
                da.Fill(dtConfronto)
                Dim r As Data.DataRow



                If Me.txtTIPO_LOTTO.Value = "E" Then


                    For Each riga As Data.DataRow In dtConfronto.Rows
                        r = Nothing
                        If Not String.IsNullOrEmpty(riga.Item("ID_EDIFICIO")) Then
                            If dt.Select("ID = " & riga.Item("ID_EDIFICIO")).Length > 0 Then
                                r = dt.Select("ID = " & riga.Item("ID_EDIFICIO"))(0)
                                If Not IsNothing(r) Then
                                    r.Item("SELEZIONATO") = 1
                                Else
                                    r.Item("SELEZIONATO") = 0
                                End If
                            End If
                        End If

                    Next



                    DataGridLstcomplessi.DataSource = dt
                    DataGridLstcomplessi.DataBind()
                    Session.Add("dtComp", dt)

                    If DataGridLstcomplessi.Items.Count = 0 Then
                        Me.LblNoResult.Visible = True
                    Else
                        Me.LblNoResult.Visible = False
                    End If

                    AddSelectedComplessi()


                Else


                    For Each riga As Data.DataRow In dtConfronto.Rows
                        r = Nothing
                        If Not String.IsNullOrEmpty(riga.Item("ID_IMPIANTO")) Then
                            If dt.Select("ID = " & riga.Item("ID_IMPIANTO")).Length > 0 Then
                                r = dt.Select("ID = " & riga.Item("ID_IMPIANTO"))(0)
                                If Not IsNothing(r) Then
                                    r.Item("SELEZIONATO") = 1
                                Else
                                    r.Item("SELEZIONATO") = 0
                                End If
                            End If
                        End If

                    Next





                    DataGridLstcomplessi.DataSource = dt
                    DataGridLstcomplessi.DataBind()
                    Session.Add("dtComp", dt)

                    If DataGridLstcomplessi.Items.Count = 0 Then
                        Me.LblNoResult.Visible = True
                    Else
                        Me.LblNoResult.Visible = False
                    End If

                    AddSelectedImpianti()


                End If

            End If







        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub



    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()


        Try

            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vId <> 0 Then

                'NON SERVE più, 01/07/2011 il lotto può essere modificato
                'par.cmd.CommandText = "select count(distinct ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI where ID_LOTTO=" & vId
                'myReader1 = par.cmd.ExecuteReader()

                'If myReader1.Read() = True Then
                '    If par.IfNull(myReader1(0), -1) > 1 Then

                '        myReader1.Close()
                '        ' LEGGO IL RECORD IN SOLO LETTURA
                '        par.cmd.CommandText = "select * from SISCOM_MI.LOTTI where ID=" & vId
                '        myReader1 = par.cmd.ExecuteReader()

                '        If myReader1.Read() Then
                '            RiempiCampi(myReader1)
                '        End If
                '        myReader1.Close()

                '        CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                '        FrmSolaLettura()
                '        Exit Sub
                '    End If
                'End If
                'myReader1.Close()
                '**************************


                par.cmd.CommandText = "select * from SISCOM_MI.LOTTI  where ID=" & vId & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()


                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                Session.Add("LAVORAZIONE", "1")
            End If
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Lotti visualizzata da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.LOTTI where ID=" & vId
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()
            Else
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Public Function ControlloCampi() As Boolean
        Dim FlagConnessione As Boolean = False

        Try
            ControlloCampi = True
            lblErrore.Visible = False

            If vId = 0 Then
                'in INSERIMENTO
                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                par.cmd.CommandText = "select SISCOM_MI.LOTTI.DESCRIZIONE from SISCOM_MI.LOTTI " _
                                   & " where SISCOM_MI.LOTTI.DESCRIZIONE LIKE '" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtDescrizione.Text, 50)) & "' " _
                                   & "   and SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue

                Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myRec.Read Then
                    Response.Write("<script>alert('Attenzione...Descrizione già presente nei nostri archivi.');</script>")
                    myRec.Close()

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                    ControlloCampi = False
                    Exit Function

                End If
                myRec.Close()

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            End If


            If cmbfiliale.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare la struttura!');</script>")
                ControlloCampi = False
                cmbfiliale.Focus()
                Exit Function
            End If

            If serviziSelezionati() = False Then
                Response.Write("<script>alert('Selezionare almeno un servizio!');</script>")
                ControlloCampi = False
                Exit Function
            End If

            If txtDescrizione.Text = "" Then
                Response.Write("<script>alert('Inserire una descrizione!');</script>")
                ControlloCampi = False
                txtDescrizione.Focus()
                Exit Function
            End If

            If cmbesercizio.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare un esercizio finanziario !');</script>")
                ControlloCampi = False
                cmbesercizio.Focus()
                Exit Function
            End If

            If complessiSelezionati() = False Then
                Response.Write("<script>alert('Selezionare almeno un complesso!');</script>")
                ControlloCampi = False
                Exit Function
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Function


    Private Sub Salva()

        Try

            par.cmd.CommandText = ""

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            ' '' Ricavo vId di LOTTO.ID
            par.cmd.CommandText = " select SISCOM_MI.SEQ_LOTTI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vId = myReader1(0)
            End If
            myReader1.Close()
            par.cmd.CommandText = ""


            'NON è PIU' VALIDO QUESTO CONTROLLO (05/07/2011) ora un EDIFICIO può APPARTENERE a LOTTI diversi con lo stesso esercizio finanziario
            'Dim c As Integer
            ''controllo se il complesso ora l'edificio è già stato assegnato allo stesso servizio con stesso esercizio in un altro lotto
            'For c = 0 To Me.lstservizi.Items.Count - 1
            '    If Me.lstservizi.Items(c).Selected = True And Str(lstservizi.Items(c).Value) > -1 Then
            '        If controlla_servizio(lstservizi.Items(c).Value) = False Then
            '            Me.lstservizi.Items(c).Selected = False


            '            ' COMMIT
            '            par.myTrans.Rollback()
            '            par.cmd.CommandText = ""
            '            par.cmd.Parameters.Clear()

            '            'CREO LA TRANSAZIONE
            '            par.myTrans = par.OracleConn.BeginTransaction()
            '            ‘‘par.cmd.Transaction = par.myTrans
            '            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            '            Exit Sub
            '        End If
            '    End If
            'Next


            'INSERIMENTO IN LOTTI
            par.cmd.CommandText = "insert into SISCOM_MI.LOTTI " _
                                 & "        (ID, ID_FILIALE, TIPO, DESCRIZIONE, ID_ESERCIZIO_FINANZIARIO,COD_TIPO_IMPIANTO)" _
                                 & " values (" & vId & "," _
                                                & par.PulisciStrSql(Me.cmbfiliale.SelectedValue) & ",'" & Me.txtTIPO_LOTTO.Value & "','" _
                                                & par.PulisciStrSql(par.PulisciStringaInvio(UCase(Me.txtDescrizione.Text), 200)) & "'," _
                                                & Me.cmbesercizio.SelectedValue & ",'" _
                                                & par.PulisciStrSql(Me.cmbTipoImpianto.SelectedValue) & "')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            ' INSERIMENTO IN LOTTI_SERVIZI
            Dim s As Integer
            For s = 0 To lstservizi.Items.Count - 1
                If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then
                    par.cmd.CommandText = "insert into SISCOM_MI.LOTTI_SERVIZI(ID_LOTTO, ID_SERVIZIO)" _
                                       & " values (" & vId & "," & Me.lstservizi.Items(s).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If
            Next

            'INSERIMENTO LOTTI PATRIMONIO

            'Dim j As Integer




            'Dim I As Integer = 0
            Dim dt As Data.DataTable = Session.Item("dtComp")


            For Each row As Data.DataRow In dt.Rows
                If row.Item("SELEZIONATO") = 1 And Str(row.Item(0).ToString()) Then

                    If Me.txtTIPO_LOTTO.Value = "E" Then
                        par.cmd.CommandText = "insert into SISCOM_MI.LOTTI_PATRIMONIO (ID_LOTTO, ID_EDIFICIO)" _
                                           & " values (" & vId & "," & row.Item(0).ToString() & ")"
                    Else
                        par.cmd.CommandText = "insert into SISCOM_MI.LOTTI_PATRIMONIO (ID_LOTTO, ID_IMPIANTO)" _
                                            & " values (" & vId & "," & row.Item(0).ToString() & ")"
                    End If

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If

            Next








            ''****Scrittura evento LOTTO
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"

            par.cmd.ExecuteNonQuery()
            '****************************************************


            ' COMMIT
            par.myTrans.Commit()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.LOTTI  where ID = " & vId & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()


            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")


            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Private Sub Update()

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            'LOTTI
            par.cmd.CommandText = " update SISCOM_MI.LOTTI set " _
                                       & "ID_FILIALE=" & par.PulisciStrSql(Me.cmbfiliale.SelectedValue) & "," _
                                       & "DESCRIZIONE='" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtDescrizione.Text, 200)) & "'," _
                                       & "ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue & "," _
                                       & "COD_TIPO_IMPIANTO='" & par.PulisciStrSql(Me.cmbTipoImpianto.SelectedValue) & "' " _
                                & " where ID='" & vId & "'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            ' LOTTI_SERVIZI
            par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_SERVIZI where ID_LOTTO = " & vId
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            Dim s As Integer
            For s = 0 To lstservizi.Items.Count - 1
                If lstservizi.Items(s).Selected = True Then
                    par.cmd.CommandText = "insert into SISCOM_MI.LOTTI_SERVIZI(ID_LOTTO, ID_SERVIZIO)" _
                                       & " values (" & vId & "," & Me.lstservizi.Items(s).Value & ")"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If
            Next





            'LOTTI_PATRIMONIO
            par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & vId
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""







            Dim dt As Data.DataTable = Session.Item("dtComp")


            For Each row As Data.DataRow In dt.Rows
                If row.Item("SELEZIONATO") = 1 And Str(row.Item(0).ToString()) Then

                    If Me.txtTIPO_LOTTO.Value = "E" Then
                        par.cmd.CommandText = "insert into SISCOM_MI.LOTTI_PATRIMONIO (ID_LOTTO, ID_EDIFICIO)" _
                                           & " values (" & vId & "," & row.Item(0).ToString() & ")"
                    Else
                        par.cmd.CommandText = "insert into SISCOM_MI.LOTTI_PATRIMONIO (ID_LOTTO, ID_IMPIANTO)" _
                                            & " values (" & vId & "," & row.Item(0).ToString() & ")"
                    End If

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                End If

            Next







            If Me.txtTIPO_LOTTO.Value = "E" Then
                par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI where (ID_LOTTO,ID_EDIFICIO) not in (select ID_LOTTO,ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO)"
            Else
                par.cmd.CommandText = "delete from SISCOM_MI.LOTTI_PATRIMONIO_IMPORTI where (ID_LOTTO,ID_IMPIANTO) not in (select ID_LOTTO,ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO)"
            End If

            par.cmd.ExecuteNonQuery()

            ''****Scrittura evento LOTTO
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_LOTTI (ID_LOTTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vId & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','')"

            par.cmd.ExecuteNonQuery()
            '****************************************************


            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
        End Try
        Return a

    End Function

    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function



    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try

            Me.btnSalva.Visible = False
            DataGridLstcomplessi.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            DataGridLstcomplessi.DataBind()
            ' Me.btnEliminaComplesso.Visible = False

            Me.lstservizi.Enabled = False


            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                End If
            Next


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    'ESERCIZIO
    Protected Sub cmbesercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbesercizio.SelectedIndexChanged
        Dim FlagConnessione As Boolean

        Try
            'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            'If cmbesercizio.SelectedValue <> "-1" Then

            '    FlagConnessione = False
            '    If par.OracleConn.State = Data.ConnectionState.Closed Then
            '        par.OracleConn.Open()
            '        par.SettaCommand(par)

            '        FlagConnessione = True
            '    End If


            '    'Svuoto gli oggetti contenenti i dati associati alla precedente filiale per poi ricaricarli 
            '    Me.lstservizi.Items.Clear()
            '    Me.lstcomplessi.Items.Clear()

            '    tabcomplessi.DataBind()

            '    par.cmd.CommandText = "select  SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE " _
            '                       & " from SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
            '                       & " where SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO " _
            '                       & "   and SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE " _
            '                       & "   and SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString _
            '                       & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"

            '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            '    myReader2 = par.cmd.ExecuteReader()
            '    While myReader2.Read()
            '        Me.lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
            '    End While

            '    myReader2.Close()
            'End If

            'Me.txtcomplessi.Value = ""

            'If FlagConnessione = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    'FILIALE
    Protected Sub cmbfiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfiliale.SelectedIndexChanged
        Dim FlagConnessione As Boolean

        Try

            txtcomplessi.Value = "" 'altrimenti si vede la lista
            txtedifici.Value = "" 'altrimenti si vede la lista
            'lblcomplesso.Text = "" 'altrimenti potrebbe visualizzarsi in tabella un complesso non corrispondente

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            If cmbfiliale.SelectedValue <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If


                'Svuoto gli oggetti contenenti i dati associati alla precedente filiale per poi ricaricarli 
                Me.lstservizi.Items.Clear()
                Me.lstcomplessi.Items.Clear()
                Me.tabcomplessi.DataBind()

                'par.cmd.CommandText = "SELECT  TAB_FILIALI.ID_TIPO_ST,SISCOM_MI.TAB_SERVIZI.ID, SISCOM_MI.TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                '& "WHERE SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO AND SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE AND SISCOM_MI.TAB_FILIALI.ID = " & cmbfiliale.SelectedValue.ToString & " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"

                par.cmd.CommandText = "select  TAB_FILIALI.ID_TIPO_ST,TAB_SERVIZI.ID, TAB_SERVIZI.DESCRIZIONE " _
                                   & " from SISCOM_MI.TAB_SERVIZI, SISCOM_MI.TAB_FILIALI, SISCOM_MI.SERVIZI_FILALE_ALER " _
                                   & " where SISCOM_MI.TAB_SERVIZI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_SERVIZIO " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID = SISCOM_MI.SERVIZI_FILALE_ALER.ID_FILIALE " _
                                   & "   and SISCOM_MI.TAB_FILIALI.ID = " & Me.cmbfiliale.SelectedValue.ToString _
                                   & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read()
                    Me.lstservizi.Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
                    Me.TipoStruttura.Value = par.IfNull(myReader2("ID_TIPO_ST"), "0")
                End While
                myReader2.Close()

            End If

            Me.txtcomplessi.Value = ""

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub

    'SERVIZIO
    Protected Sub lstservizi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstservizi.SelectedIndexChanged
        'Dim FlagConnessione As Boolean = False
        'Dim i As Integer
        'Dim servizi As String
        'Dim dt As New Data.DataTable()

        Try

            '    'controllo se il complesso è già stato assegnato allo stesso servizio con stesso esercizio in un altro lotto
            '    'For c = 0 To lstservizi.Items.Count - 1
            '    '    If lstservizi.Items(c).Selected = True And Str(lstservizi.Items(c).Value) > -1 Then
            '    '        If controlla_servizio(lstservizi.Items(c).Value) = False Then
            '    '            lstservizi.Items(c).Selected = False
            '    '            Exit Sub
            '    '        End If
            '    '    End If
            '    'Next

            '    FlagConnessione = False
            '    If par.OracleConn.State = Data.ConnectionState.Closed Then
            '        par.OracleConn.Open()
            '        par.SettaCommand(par)

            '        FlagConnessione = True
            '    End If

            '    If Me.cmbfiliale.SelectedValue <> "-1" And lstservizi.SelectedValue <> "-1" Then
            '        Dim s As Integer
            '        servizi = ""

            '        For s = 0 To lstservizi.Items.Count - 1
            '            If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then

            '                If servizi = "" Then
            '                    servizi = lstservizi.Items(s).Value
            '                Else
            '                    servizi = servizi & "," & lstservizi.Items(s).Value
            '                End If
            '            End If
            '        Next

            '        If servizi = "" Then
            '            servizi = "-1"
            '        End If








            '        Dim TipoStruttura1 As String = ""

            '        Select Case TipoStruttura.Value
            '            Case "0"    'FILIALE AMMINISTRATIVA
            '                If Me.txtTIPO_LOTTO.Value = "E" Then
            '                    'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
            '                    TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
            '                Else
            '                    TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
            '                End If

            '            Case "1"    'FILIALE TECNICA
            '                If Me.txtTIPO_LOTTO.Value = "E" Then
            '                    'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
            '                    TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
            '                Else
            '                    TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
            '                End If

            '            Case "2"    'UFFICIO CENTRALE
            '                TipoStruttura1 = ""
            '        End Select




            'If cmbTipoImpianto.SelectedItem.Value = "-1" Then

            '    If Me.txtTIPO_LOTTO.Value = "E" Then
            '        par.cmd.CommandText = "select distinct(SISCOM_MI.EDIFICI.ID)," _
            '                                 & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
            '                                 & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio, '0' AS SELEZIONATO  " _
            '                             & " from SISCOM_MI.EDIFICI " _
            '                             & " where EDIFICI.ID<>1  " & TipoStruttura1 & "  " _
            '                             & " order by DENOMINAZIONE ASC"
            '    Else
            '        par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
            '                                    & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
            '                                    & "'' AS condominio, '0' AS SELEZIONATO  " _
            '                          & " from SISCOM_MI.IMPIANTI " _
            '                          & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
            '                          & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
            '                                                     & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
            '                                                     & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
            '                                                     & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
            '                                                     & "   and LOTTI.ID<>" & vId _
            '                                                     & "   and ID_IMPIANTO IS NOT NULL " _
            '                                                     & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
            '                           & " order by DENOMINAZIONE ASC"

            '    End If

            'Else



            '    If Me.txtTIPO_LOTTO.Value = "E" Then
            '        par.cmd.CommandText = "select distinct(SISCOM_MI.EDIFICI.ID)," _
            '                                 & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
            '                                 & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio, '0' AS SELEZIONATO  " _
            '                             & " from SISCOM_MI.EDIFICI " _
            '                             & " where EDIFICI.ID<>1 " & TipoStruttura1 & "  " _
            '                             & "   and EDIFICI.ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "') " _
            '                             & " order by DENOMINAZIONE ASC"
            '    Else
            '        par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
            '                                      & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
            '                                      & "'' AS condominio, '0' AS SELEZIONATO " _
            '                            & " from SISCOM_MI.IMPIANTI " _
            '                            & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
            '                            & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
            '                                                       & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
            '                                                       & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
            '                                                       & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
            '                                                       & "   and LOTTI.ID<>" & vId _
            '                                                       & "   and ID_IMPIANTO IS NOT NULL " _
            '                                                       & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
            '                             & "  and IMPIANTI.COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "' " _
            '                             & " order by DENOMINAZIONE ASC"

            '    End If

            'End If


            ''Me.cmbTipoImpianto.SelectedValue = -1
            ''cmbTipoImpianto_SelectedIndexChanged(sender, e)


            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'da.Fill(dt)


            ''End If





            'If servizi <> "-1" Then
            '    par.cmd.CommandText = "select * from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & vId
            '    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '    Dim dtConfronto As New Data.DataTable
            '    da.Fill(dtConfronto)
            '    Dim r As Data.DataRow



            ''If Me.txtTIPO_LOTTO.Value = "E" Then


            'For Each riga As Data.DataRow In dtConfronto.Rows
            '    r = Nothing
            '    If Not String.IsNullOrEmpty(riga.Item("ID_EDIFICIO")) Then
            '        If dt.Select("ID = " & riga.Item("ID_EDIFICIO")).Length > 0 Then
            '            r = dt.Select("ID = " & riga.Item("ID_EDIFICIO"))(0)
            '            If Not IsNothing(r) Then
            '                r.Item("SELEZIONATO") = 1
            '            Else
            '                r.Item("SELEZIONATO") = 0
            '            End If
            '        End If
            '    End If

            'Next



            'DataGridLstcomplessi.DataSource = dt
            'DataGridLstcomplessi.DataBind()
            'Session.Add("dtComp", dt)

            'If DataGridLstcomplessi.Items.Count = 0 Then
            '    Me.LblNoResult.Visible = True
            'Else
            '    Me.LblNoResult.Visible = False
            'End If

            ''AddSelectedComplessi()


            ''Else


            'For Each riga As Data.DataRow In dtConfronto.Rows
            '    r = Nothing
            '    If Not String.IsNullOrEmpty(riga.Item("ID_IMPIANTO")) Then
            '        If dt.Select("ID = " & riga.Item("ID_IMPIANTO")).Length > 0 Then
            '            r = dt.Select("ID = " & riga.Item("ID_IMPIANTO"))(0)
            '            If Not IsNothing(r) Then
            '                r.Item("SELEZIONATO") = 1
            '            Else
            '                r.Item("SELEZIONATO") = 0
            '            End If
            '        End If
            '    End If

            'Next





            'DataGridLstcomplessi.DataSource = dt
            'DataGridLstcomplessi.DataBind()
            'Session.Add("dtComp", dt)

            'If DataGridLstcomplessi.Items.Count = 0 Then
            '    Me.LblNoResult.Visible = True
            'Else
            '    Me.LblNoResult.Visible = False
            'End If

            ''    AddSelectedImpianti()


            '  End If

            ' End If



            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.lstcomplessi.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " ") & par.IfNull(myReader1("CONDOMINIO"), " "), par.IfNull(myReader1("ID"), -1)))

            'End While
            'myReader1.Close()

            'If lstcomplessi.Items.Count = 0 Then
            '    Me.LblNoResult.Visible = True

            'Else
            '    Me.LblNoResult.Visible = False
            'End If

            'If servizi <> "-1" Then

            '    par.cmd.CommandText = "select * from SISCOM_MI.LOTTI_PATRIMONIO where ID_LOTTO = " & vId
            '    myReader1 = par.cmd.ExecuteReader
            '    While myReader1.Read
            '        If par.IfNull(myReader1("ID_EDIFICIO"), 0) <> 0 Then

            '            For i = 0 To Me.lstcomplessi.Items.Count() - 1
            '                If lstcomplessi.Items(i).Value = myReader1("ID_EDIFICIO") Then
            '                    Me.lstcomplessi.Items.FindByValue(myReader1("ID_EDIFICIO")).Selected = True
            '                End If
            '            Next

            '            AddSelectedComplessi()

            '        ElseIf par.IfNull(myReader1("ID_IMPIANTO"), 0) <> 0 Then
            '            For i = 0 To Me.lstcomplessi.Items.Count() - 1
            '                If lstcomplessi.Items(i).Value = myReader1("ID_IMPIANTO") Then
            '                    Me.lstcomplessi.Items.FindByValue(myReader1("ID_IMPIANTO")).Selected = True
            '                End If
            '            Next
            '            AddSelectedImpianti()
            '        End If
            '    End While
            '    myReader1.Close()
            'End If


            ' End If





            'If FlagConnessione = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If

        Catch ex As Exception

            'If FlagConnessione = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Public Function serviziSelezionati() As Boolean
        Try
            'Dim s As Integer
            'serviziSelezionati = False
            'For s = 0 To Me.lstservizi.Items.Count() - 1
            '    If Me.lstservizi.Items(s).Selected = True Then
            serviziSelezionati = True
            '        Exit For
            '    End If
            'Next

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Public Function controlla_servizio(ByVal servizio) As Boolean
        Dim FlagConnessione As Boolean = False
        Try

            'FlagConnessione = False
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            '    FlagConnessione = True
            'End If

            'controlla_servizio = True
            'Dim i As Integer
            'For i = 0 To lstcomplessi.Items.Count - 1
            '    If lstcomplessi.Items(i).Selected = True And Str(lstcomplessi.Items(i).Value) > -1 Then

            '        par.cmd.CommandText = " select SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO " _
            '                            & " from   SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI_SERVIZI,SISCOM_MI.LOTTI" _
            '                            & " where  SISCOM_MI.LOTTI_PATRIMONIO.ID_COMPLESSO=" & lstcomplessi.Items(i).Value _
            '                            & "   and  SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=" & servizio _
            '                            & "   and  SISCOM_MI.LOTTI.ID!=" & vId _
            '                            & "   and  SISCOM_MI.LOTTI.ID_ESERCIZIO_FINANZIARIO=" & cmbesercizio.SelectedValue _
            '                            & "   and  SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID " _
            '                            & "   and  SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID"

            '        Dim myreader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myreader1.Read Then
            '            Response.Write("<script>alert('Attenzione!\nPer questo servizio i complessi disponibili sono già stati selezionati in altri lotti con lo stesso esercizio finanziario');</script>")
            '            controlla_servizio = False

            '            myreader1.Close()

            '            If FlagConnessione = True Then
            '                par.cmd.Dispose()
            '                par.OracleConn.Close()
            '                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '            End If
            '            Exit Function
            '        End If
            '        myreader1.Close()

            '    End If
            'Next

            'If FlagConnessione = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function



    'COMPLESSO
    'Protected Sub tabcomplessi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles tabcomplessi.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------       
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        ' e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this; this.style.backgroundColor='red';document.getElementById('txtseledifici').value='Hai selezionato: " & Replace(Replace(Replace(e.Item.Cells(1).Text, "'", "\'"), vbCr, " "), vbLf, " ") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
    '        '   e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this; this.style.backgroundColor='red';document.getElementById('txtseledifici').value='Hai selezionato: " & Replace(Replace(Replace(e.Item.Cells(1).Text, "'", "\'"), vbCr, " "), vbLf, " ") & "';document.getElementById('txtIdComponente').value='" & e.Item.Cells(0).Text & "'")
    '    End If





    'End Sub




    Protected Sub BtnConfermaComplessoTutti_Click(sender As Object, e As System.EventArgs) Handles BtnConfermaComplessoTutti.Click
        ' Dim I As Integer

        '  For I = 0 To Me.lstcomplessi.Items.Count() - 1
        'Me.lstcomplessi.Items(I).Selected = True
        ' Next

        'cmbTipoImpianto_SelectedIndexChanged(sender, e)
        '  Me.cmbTipoImpianto.SelectedItem.Value = -1
        Try

            cmbTipoImpianto_SelectedIndexChanged(sender, e)


            sel = 1

            If DataGridLstcomplessi.Items.Count > 0 Then

                Dim I As Integer = 0
                Dim dt As Data.DataTable = Session.Item("dtComp")


                If Selezionati.Value = 0 Then
                    For Each row As Data.DataRow In dt.Rows
                        row.Item("SELEZIONATO") = 1

                    Next


                    Selezionati.Value = 1
                Else
                    For Each row As Data.DataRow In dt.Rows
                        row.Item("SELEZIONATO") = 0
                    Next


                    Selezionati.Value = 0
                End If



                Session.Item("dtComp") = dt
                DataGridLstcomplessi.DataSource = Session.Item("dtComp")
                DataGridLstcomplessi.DataBind()
                Me.cmbTipoImpianto.SelectedValue = -1
                cmbTipoImpianto_SelectedIndexChanged(sender, e)

            End If



            If Me.txtTIPO_LOTTO.Value = "E" Then    'MARY: TOGLIERE COMMENTI PER AGGIORNARE
                ' AddSelectedComplessi()
                'Me.txtcomplessi.Value = "2"
                sel = 0
            Else
                sel = 0
            End If
            Dim script As String = "function f(){$find(""" + RadWindowLotto.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub


    Protected Sub BtnConfermacomplesso_Click(sender As Object, e As System.EventArgs) Handles BtnConfermaComplesso.Click


        'Me.cmbTipoImpianto.SelectedItem.Value = "-1"
        'cmbTipoImpianto_SelectedIndexChanged(sender, e)



        ' Dim K As Integer = 0
        Dim r As Data.DataRow
        Dim ds As Data.DataTable = Session.Item("dtComp")
        Try
            For K As Integer = 0 To DataGridLstcomplessi.Items.Count - 1
                If DirectCast(DataGridLstcomplessi.Items(K).Cells(1).FindControl("ChkSelezione"), CheckBox).Checked = False Then
                    'dt.Rows(i).Item("CHECKED") = 0
                    r = ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text)(0)
                    r.Item("SELEZIONATO") = 0
                Else
                    'dt.Rows(i).Item("CHECKED") = 1
                    r = ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text)(0)
                    r.Item("SELEZIONATO") = 1
                End If

            Next
            'Label3.Text = "0"
            Session.Item("dtComp") = ds

            If Me.txtTIPO_LOTTO.Value = "E" Then
                AddSelectedComplessi()

            Else
                AddSelectedImpianti()
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        Catch ex As Exception


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub AddSelectedComplessi()

        Dim str1 As String
        Dim FlagConnessione As Boolean = False

        Try

            str1 = ""


            tabcomplessi.Visible = True



            Dim I As Integer = 0
            Dim ds As Data.DataTable = Session.Item("dtComp")


            For Each row As Data.DataRow In ds.Rows
                If row.Item("SELEZIONATO") = 1 Then

                    If Strings.Len(str1) = 0 Then
                        str1 = row.Item(0).ToString()

                    Else
                        If ds.Rows.Count() > 1000 Then
                            str1 = str1 & ") OR EDIFICI.ID IN (-1"
                        End If
                        str1 = str1 & "," & row.Item(0).ToString()

                    End If


                End If

            Next






            If Strings.Len(str1) = 0 Then
                str1 = "-1"
            End If




            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            par.cmd.CommandText = "select SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE " _
                                & "from SISCOM_MI.EDIFICI where SISCOM_MI.EDIFICI.ID in (" & str1 & ")  order by EDIFICI.denominazione asc"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dp As New Data.DataSet()

            da.Fill(dp, "EDIFICI")

            tabcomplessi.CurrentPageIndex = 0
            tabcomplessi.DataSource = dp
            tabcomplessi.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)

            Session.Add("dsComp", dp)

            da.Dispose()
            dp.Dispose()



            If tabcomplessi.Items.Count <> 0 Then

            End If




            If (sel = 0) Then
                Me.txtcomplessi.Value = "1"

            Else
                Me.txtcomplessi.Value = "2"
            End If




            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub AddSelectedImpianti()
        ' Dim I As Integer
        Dim str1 As String
        Dim FlagConnessione As Boolean = False

        Try

            str1 = ""


            tabcomplessi.Visible = True

            ' If complessiSelezionati() = True Then

            Dim I As Integer = 0
            Dim ds As Data.DataTable = Session.Item("dtComp")


            For Each row As Data.DataRow In ds.Rows
                If row.Item("SELEZIONATO") = 1 Then

                    If Strings.Len(str1) = 0 Then
                        str1 = row.Item(0).ToString()

                    Else
                        If ds.Rows.Count() > 1000 Then
                            str1 = str1 & ") OR IMPIANTI.ID IN (-1"
                        End If
                        str1 = str1 & "," & row.Item(0).ToString()

                    End If


                End If

            Next

            If Strings.Len(str1) = 0 Then
                str1 = "-1"
            End If




            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID,(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) as DENOMINAZIONE " _
                                & "from SISCOM_MI.IMPIANTI where SISCOM_MI.IMPIANTI.ID in (" & str1 & ")  order by denominazione asc"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dp As New Data.DataSet()

            da.Fill(dp, "IMPIANTI")

            tabcomplessi.CurrentPageIndex = 0
            tabcomplessi.DataSource = dp
            tabcomplessi.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
            Session.Add("dsComp", dp)
            da.Dispose()
            dp.Dispose()

            If (sel = 0) Then
                Me.txtcomplessi.Value = "1"

            Else
                Me.txtcomplessi.Value = "2"
            End If


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Public Function complessiSelezionati() As Boolean


        Try

            complessiSelezionati = False


            Dim dt As Data.DataTable = Session.Item("dtComp")


            For Each row As Data.DataRow In dt.Rows
                If row.Item("SELEZIONATO") = 1 Then

                    complessiSelezionati = True

                End If

            Next



        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function



    Protected Sub cmbTipoImpianto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoImpianto.SelectedIndexChanged
        Dim FlagConnessione As Boolean = False

        Dim servizi As String
        Dim dt As New Data.DataTable()
        'Dim dk As Data.DataTable = Session.Item("dtvista")
        Dim ds As Data.DataTable = Session.Item("dtComp")
        Dim z As Data.DataRow




        Try
            'tabcomplessi.DataBind()




            For K As Integer = 0 To DataGridLstcomplessi.Items.Count - 1
                If DirectCast(DataGridLstcomplessi.Items(K).Cells(1).FindControl("ChkSelezione"), CheckBox).Checked = False Then
                    If ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text).Length > 0 Then
                        z = ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text)(0)
                        z.Item("SELEZIONATO") = 0
                    End If
                Else
                    If ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text).Length > 0 Then
                        'dt.Rows(i).Item("CHECKED") = 1
                        z = ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text)(0)
                        z.Item("SELEZIONATO") = 1
                    End If
                End If
            Next
            'Label3.Text = "0"
            Session.Item("dtComp") = ds





            DataGridLstcomplessi.CurrentPageIndex = 0










            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            If Me.cmbfiliale.SelectedValue <> "-1" And Me.lstservizi.SelectedValue <> "-1" Then  'carico tutti i complessi per filiali e servizi non assegnati oltre a quelli assegnati per questo lotto
                Dim s As Integer
                servizi = ""

                For s = 0 To lstservizi.Items.Count - 1
                    If lstservizi.Items(s).Selected = True And Str(lstservizi.Items(s).Value) > -1 Then

                        If servizi = "" Then
                            servizi = lstservizi.Items(s).Value
                        Else
                            servizi = servizi & "," & lstservizi.Items(s).Value
                        End If
                    End If
                Next

                If servizi = "" Then
                    servizi = "-1"
                End If

                Dim TipoStruttura1 As String = ""

                Select Case TipoStruttura.Value
                    Case "0"    'FILIALE AMMINISTRATIVA
                        If Me.txtTIPO_LOTTO.Value = "E" Then
                            'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE=" & cmbfiliale.SelectedValue.ToString & " "
                            TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                        Else
                            TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & Me.cmbfiliale.SelectedValue.ToString & ") "
                        End If

                    Case "1"    'FILIALE TECNICA
                        If Me.txtTIPO_LOTTO.Value = "E" Then
                            'TipoStruttura1 = " AND COMPLESSI_IMMOBILIARI.ID_FILIALE IN (SELECT ID FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID_TECNICA=" & cmbfiliale.SelectedValue.ToString & ") "
                            TipoStruttura1 = " and EDIFICI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
                        Else
                            TipoStruttura1 = " and IMPIANTI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 and ID_TECNICA=" & Me.cmbfiliale.SelectedValue.ToString & ")) "
                        End If

                    Case "2"    'UFFICIO CENTRALE
                        TipoStruttura1 = ""
                End Select




                If cmbTipoImpianto.SelectedItem.Value = "-1" Then

                    For K As Integer = 0 To DataGridLstcomplessi.Items.Count - 1
                        If DirectCast(DataGridLstcomplessi.Items(K).Cells(1).FindControl("ChkSelezione"), CheckBox).Checked = False Then
                            If ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text).Length > 0 Then
                                z = ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text)(0)
                                z.Item("SELEZIONATO") = 0
                            End If
                        Else
                            If ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text).Length > 0 Then
                                'dt.Rows(i).Item("CHECKED") = 1
                                z = ds.Select("id = " & DataGridLstcomplessi.Items(K).Cells(par.IndRDGC(DataGridLstcomplessi, "ID")).Text)(0)
                                z.Item("SELEZIONATO") = 1
                            End If
                        End If
                    Next
                    'Label3.Text = "0"
                    Session.Item("dtComp") = ds
                    DataGridLstcomplessi.DataSource = Session.Item("dtComp")
                    DataGridLstcomplessi.DataBind()

                Else



                    If Me.txtTIPO_LOTTO.Value = "E" Then
                        par.cmd.CommandText = "select distinct(SISCOM_MI.EDIFICI.ID)," _
                                                 & "(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) AS DENOMINAZIONE, " _
                                                 & "(CASE WHEN condominio=1 THEN '- Edificio in Condominio' ELSE '' END) AS condominio,  '0' AS SELEZIONATO " _
                                             & " from SISCOM_MI.EDIFICI " _
                                             & " where EDIFICI.ID<>1 " & TipoStruttura1 & "  " _
                                             & "   and EDIFICI.ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI where COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "') " _
                                             & " order by DENOMINAZIONE ASC"
                    Else
                        par.cmd.CommandText = "select SISCOM_MI.IMPIANTI.ID," _
                                                      & "(SISCOM_MI.IMPIANTI.COD_IMPIANTO||'-'||SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||SISCOM_MI.IMPIANTI.COD_TIPOLOGIA) AS DENOMINAZIONE, " _
                                                      & "'' AS condominio, '0' AS SELEZIONATO  " _
                                            & " from SISCOM_MI.IMPIANTI " _
                                            & " where IMPIANTI.ID<>1  " & TipoStruttura1 & "  " _
                                            & "   and IMPIANTI.ID NOT IN  (select ID_IMPIANTO from SISCOM_MI.LOTTI_PATRIMONIO,SISCOM_MI.LOTTI,SISCOM_MI.LOTTI_SERVIZI " _
                                                                       & " where SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                       & "   and SISCOM_MI.LOTTI_PATRIMONIO.ID_LOTTO=SISCOM_MI.LOTTI.ID  " _
                                                                       & "   and LOTTI_SERVIZI.ID_SERVIZIO in (" & servizi & ") " _
                                                                       & "   and LOTTI.ID<>" & vId _
                                                                       & "   and ID_IMPIANTO IS NOT NULL " _
                                                                       & "   and LOTTI.ID_ESERCIZIO_FINANZIARIO=" & Me.cmbesercizio.SelectedValue & " ) " _
                                            & "  and IMPIANTI.COD_TIPOLOGIA='" & cmbTipoImpianto.SelectedItem.Value & "' " _
                                            & " order by DENOMINAZIONE ASC"

                    End If





                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)
                    If ((servizi <> "-1") Or (servizi = "-1")) Then

                        Dim r As Data.DataRow





                        For Each riga As Data.DataRow In ds.Rows
                            r = Nothing
                            If Not String.IsNullOrEmpty(riga.Item("id")) Then
                                If dt.Select("id = " & riga.Item("id")).Length > 0 Then
                                    r = dt.Select("id = " & riga.Item("id"))(0)
                                    If Not IsNothing(r) Then
                                        If riga.Item("SELEZIONATO") = 1 Then
                                            r.Item("SELEZIONATO") = 1
                                        Else
                                            r.Item("SELEZIONATO") = 0
                                        End If
                                    End If
                                End If
                            End If

                        Next







                    End If



                    Session.Item("dtDev") = dt
                    DataGridLstcomplessi.DataSource = dt

                    ' dk = DataGridLstcomplessi.DataSource

                    DataGridLstcomplessi.DataBind()



                End If







            End If










            ' DataGridLstcomplessi.DataSource = Session.Item("dtvista")


            Me.txtcomplessi.Value = "2"









            '   AddSelectedComplessi()








            '    ElseIf par.IfNull(myReader1("ID_IMPIANTO"), 0) <> 0 Then
            '        For i = 0 To Me.lstcomplessi.Items.Count() - 1
            '            If lstcomplessi.Items(i).Value = myReader1("ID_IMPIANTO") Then
            '                Me.lstcomplessi.Items.FindByValue(myReader1("ID_IMPIANTO")).Selected = True
            '            End If
            '        Next
            '        AddSelectedImpianti()
            '    End If
            'End While
            'myReader1.Close()
            'End If
            'End If


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Dim script As String = "function f(){$find(""" + RadWindowLotto.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub




    Protected Sub DataGridLstcomplessi_PageIndexChanged1(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles DataGridLstcomplessi.PageIndexChanged

        If e.NewPageIndex >= 0 Then


            Me.txtcomplessi.Value = "2"
            DataGridLstcomplessi.CurrentPageIndex = e.NewPageIndex

            AggiustaCompSessione()

            If Me.cmbTipoImpianto.SelectedValue = "-1" Then
                DataGridLstcomplessi.DataSource = Session.Item("dtComp")
                DataGridLstcomplessi.DataBind()

            Else
                DataGridLstcomplessi.DataSource = Session.Item("dtDev")
                DataGridLstcomplessi.DataBind()
            End If

        End If

    End Sub



    Public Sub AggiustaCompSessione()

        If Me.cmbTipoImpianto.SelectedValue = "-1" Then
            Dim dt As Data.DataTable = Session.Item("dtComp")
            Dim r As Data.DataRow
            For i As Integer = 0 To DataGridLstcomplessi.Items.Count - 1
                If DirectCast(DataGridLstcomplessi.Items(i).Cells(1).FindControl("ChkSelezione"), CheckBox).Checked = False Then
                    'dt.Rows(i).Item("CHECKED") = 0
                    r = dt.Select("id = " & DataGridLstcomplessi.Items(i).Cells(0).Text)(0)
                    r.Item("SELEZIONATO") = 0
                Else
                    'dt.Rows(i).Item("CHECKED") = 1
                    r = dt.Select("id = " & DataGridLstcomplessi.Items(i).Cells(0).Text)(0)
                    r.Item("SELEZIONATO") = 1
                End If

            Next
            'Label3.Text = "0"
            Session.Item("dtComp") = dt
        Else
            Dim dt As Data.DataTable = Session.Item("dtDev")
            Dim r As Data.DataRow
            For i As Integer = 0 To DataGridLstcomplessi.Items.Count - 1
                If DirectCast(DataGridLstcomplessi.Items(i).Cells(1).FindControl("ChkSelezione"), CheckBox).Checked = False Then
                    'dt.Rows(i).Item("CHECKED") = 0
                    r = dt.Select("id = " & DataGridLstcomplessi.Items(i).Cells(0).Text)(0)
                    r.Item("SELEZIONATO") = 0
                Else
                    'dt.Rows(i).Item("CHECKED") = 1
                    r = dt.Select("id = " & DataGridLstcomplessi.Items(i).Cells(0).Text)(0)
                    r.Item("SELEZIONATO") = 1
                End If

            Next
            'Label3.Text = "0"
            Session.Item("dtDev") = dt
        End If
    End Sub





    Protected Sub tabcomplessi_PageIndexChanged1(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles tabcomplessi.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            ' Me.txtcomplessi.Value = "2"
            tabcomplessi.CurrentPageIndex = e.NewPageIndex
            tabcomplessi.DataSource = Session.Item("dsComp")
            tabcomplessi.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        End If
    End Sub

    Protected Sub btnINDIETRO_Click(sender As Object, e As System.EventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")


            sValoreEsercizio = UCase(Request.QueryString("EF"))
            sValoreFiliale = UCase(Request.QueryString("FI"))
            sValoreComplesso = UCase(Request.QueryString("CO"))
            sValoreServizi = UCase(Request.QueryString("SE"))


            formPadre = UCase(Request.QueryString("TIPO"))


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")


            If txtindietro.Text = 1 Then
                Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
            Else
                Response.Write("<script>location.replace('RisultatiLotti.aspx?FI=" & sValoreFiliale _
                                                                          & "&EF=" & sValoreEsercizio _
                                                                          & "&CO=" & sValoreComplesso _
                                                                          & "&SE=" & sValoreServizi _
                                                                          & "&T=" & Me.txtTIPO_LOTTO.Value _
                                                                          & "&TIPO=RICERCA_LOTTI');</script>")

            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            caricamento.Visible = True

            If ControlloCampi() = False Then
                Exit Sub
            End If

            If vId = 0 Then
                Me.Salva()
            Else
                Me.Update()
            End If

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Private Sub LOTTI_GestioneLotto_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Session.Item("LIVELLO") <> "1" Then

            Me.cmbfiliale.Enabled = False
        End If
    End Sub
End Class